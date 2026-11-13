#Requires -Version 7
<#
.SYNOPSIS
Gemeinsame stabile MSL-Versionen pruefen und pflegen.
Check and maintain shared stable MSL toolchains.
.DESCRIPTION
Release-Metadaten, Paketquelle und aktive CLI werden getrennt bewertet.
Release metadata, package source and active CLI are evaluated separately.
Projekt-Pins bleiben erhalten; alle Proben laufen ausserhalb des Projekts.
Project pins are preserved; probes run outside the project directory.
#>
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function ConvertTo-HBMslVersion {
    param([AllowEmptyString()][string]$Text)
    # Paketrevisionen und Build-Hashes sind keine neuen Sprachversionen.
    # Package revisions and build hashes are not language versions.
    $match = [regex]::Match($Text, '(?<![\d.])(?<v>\d+(?:\.\d+){0,3})(?![\d.])')
    if (-not $match.Success) { return $null }
    $parts = @($match.Groups['v'].Value.Split('.') | ForEach-Object { [int]$_ })
    while ($parts.Count -lt 4) { $parts += 0 }
    return [version]::new($parts[0], $parts[1], $parts[2], $parts[3])
}

function Get-HBMslReleaseVersion {
    param([Parameter(Mandatory)][string]$Provider, [Parameter(Mandatory)]$Payload)
    $versions = switch ($Provider) {
        'dotnet' {
            foreach ($item in $Payload['releases-index']) {
                if ($item['support-phase'] -in @('active', 'maintenance') -and
                    [string]$item['latest-sdk'] -match '^\d+\.\d+\.\d+$') {
                    [string]$item['latest-sdk']
                }
            }
        }
        'go' {
            foreach ($item in $Payload) {
                if ($item['stable'] -eq $true -and $item['version'] -match '^go(\d+\.\d+(?:\.\d+)?)$') {
                    $Matches[1]
                }
            }
        }
        'java' { [string]$Payload['most_recent_feature_release'] }
        'java-patch' {
            foreach ($item in $Payload) {
                $metadata = if ($item.Contains('version')) { $item['version'] } else { $item['version_data'] }
                if ($metadata['semver'] -match '^(\d+\.\d+\.\d+)') { $Matches[1] }
            }
        }
        'python' {
            foreach ($item in $Payload) {
                if ($item['is_published'] -eq $true -and $item['pre_release'] -eq $false -and
                    $item['name'] -match '^Python (\d+\.\d+\.\d+)$' -and
                    [datetimeoffset]$item['release_date'] -le [datetimeoffset]::UtcNow) {
                    $Matches[1]
                }
            }
        }
        'rust' {
            $block = [regex]::Match([string]$Payload, '(?ms)^\[pkg\.rust\]\s*\r?\n(?<b>.*?)(?=^\[|\z)')
            $match = [regex]::Match($block.Groups['b'].Value, '(?m)^version\s*=\s*"(\d+\.\d+\.\d+)')
            if ($match.Success) { $match.Groups[1].Value }
        }
        'swift' {
            foreach ($item in $Payload) {
                if ($item['name'] -match '^(?:Swift )?(\d+\.\d+(?:\.\d+)?)$') { $Matches[1] }
            }
        }
        default { throw 'Unbekannter Release-Provider / unknown release provider.' }
    }
    $latest = @($versions | Where-Object { $_ -and $_.Length -le 32 -and $_ -match '^\d+(?:\.\d+){0,3}$' } |
        Sort-Object { ConvertTo-HBMslVersion $_ } -Descending)
    if ($latest.Count -eq 0) { throw 'Keine stabile Version / no stable release.' }
    return [string]$latest[0]
}

function Get-HBMslRemoteData {
    param([Parameter(Mandatory)][string]$Uri, [switch]$Text)
    $url = [uri]$Uri
    $allowed = @('builds.dotnet.microsoft.com', 'dotnetcli.blob.core.windows.net', 'go.dev',
        'api.adoptium.net', 'www.python.org', 'static.rust-lang.org', 'www.swift.org', 'formulae.brew.sh')
    if ($url.Scheme -ne 'https' -or $url.Host -notin $allowed -or $url.UserInfo) {
        throw 'Release-Quelle nicht erlaubt / release source not allowed.'
    }
    for ($attempt = 0; $attempt -lt 2; $attempt++) {
        try {
            $response = Invoke-WebRequest -Uri $url -TimeoutSec 15 -MaximumRedirection 0
            if ($response.RawContentLength -gt 8MB) { throw 'Release-Metadaten zu gross / metadata too large.' }
            $content = if ($response.Content -is [byte[]]) {
                [Text.UTF8Encoding]::new($false, $true).GetString($response.Content)
            } else { [string]$response.Content }
            if ($Text) { return $content }
            return ConvertFrom-Json -InputObject $content -AsHashtable -Depth 40
        } catch {
            if ($attempt -eq 1) { throw 'Release-Quelle nicht lesbar / release source unavailable.' }
        }
    }
}

function Invoke-HBMslProcess {
    param([Parameter(Mandatory)][string]$Command, [string[]]$Arguments = @(),
        [Parameter(Mandatory)][string]$Directory, [int]$TimeoutSeconds = 15)
    $resolved = Get-Command $Command -CommandType Application -ErrorAction SilentlyContinue | Select-Object -First 1
    if (-not $resolved) { return @{ ExitCode = 127; Output = ''; Executable = ''; TimedOut = $false } }
    if ($IsWindows -and $Command -in @('python', 'python3') -and $resolved.Source -like '*\WindowsApps\*') {
        return @{ ExitCode = 127; Output = ''; Executable = $resolved.Source; TimedOut = $false }
    }
    $start = [Diagnostics.ProcessStartInfo]::new()
    $start.FileName = $resolved.Source
    $start.WorkingDirectory = $Directory
    $start.UseShellExecute = $false
    $start.RedirectStandardInput = $true
    $start.RedirectStandardOutput = $true
    $start.RedirectStandardError = $true
    foreach ($argument in $Arguments) { $start.ArgumentList.Add($argument) }
    $process = [Diagnostics.Process]::new()
    $process.StartInfo = $start
    try {
        [void]$process.Start()
        $process.StandardInput.Close()
        $stdout = $process.StandardOutput.ReadToEndAsync()
        $stderr = $process.StandardError.ReadToEndAsync()
        $timedOut = -not $process.WaitForExit($TimeoutSeconds * 1000)
        if ($timedOut) {
            $process.Kill($true)
            if (-not $process.WaitForExit(10000)) { throw 'Prozessbaum nicht beendet / process tree not terminated.' }
        }
        $output = $stdout.GetAwaiter().GetResult() + $stderr.GetAwaiter().GetResult()
        return @{ ExitCode = $(if ($timedOut) { 124 } else { $process.ExitCode });
            Output = $output; Executable = $resolved.Source; TimedOut = $timedOut }
    } finally { $process.Dispose() }
}

function Invoke-HBMslAction {
    param([string]$Command, [string[]]$Arguments, [string]$Directory, [switch]$Preview)
    $label = "$Command $($Arguments -join ' ')"
    if ($Preview) { Write-Host "DRY-RUN: $label"; return }
    Write-Host "MSL ACTION: $label"
    $result = Invoke-HBMslProcess -Command $Command -Arguments $Arguments -Directory $Directory -TimeoutSeconds 1800
    if ($result.Output) {
        $display = [regex]::Replace($result.Output, '(?m)^\d+%: Downloaded[^\r\n]*\r?\n?', '')
        Write-Host ($display.Replace([Environment]::GetFolderPath('UserProfile'), '~'))
    }
    if ($result.ExitCode -ne 0) { throw "Paketaktion fehlgeschlagen / package action failed: $Command ($($result.ExitCode))." }
}

function Test-HBMslPowerShellHealth {
    param([string]$Directory)
    $code = @'
$ErrorActionPreference='Stop'
Add-Type -TypeDefinition 'public static class HBEngineProbe { public static int Value => 1; }'
if([HBEngineProbe]::Value -ne 1){exit 1}
'@
    $result = Invoke-HBMslProcess pwsh @('-NoProfile', '-NonInteractive', '-Command', $code) $Directory
    return $result.ExitCode -eq 0
}

function Get-HBMslStatus {
    param([AllowEmptyString()][string]$Active, [AllowEmptyString()][string]$Installed,
        [AllowEmptyString()][string]$Source, [AllowEmptyString()][string]$Latest)
    $latestVersion = ConvertTo-HBMslVersion $Latest
    if (-not $latestVersion) { return 'Unknown' }
    if ($Installed -and (ConvertTo-HBMslVersion $Installed) -ge $latestVersion -and
        (-not $Active -or (ConvertTo-HBMslVersion $Active) -lt $latestVersion)) { return 'PathMismatch' }
    if ($Active -and (ConvertTo-HBMslVersion $Active) -ge $latestVersion) { return 'Current' }
    if (-not $Source) { return 'Unknown' }
    if ((ConvertTo-HBMslVersion $Source) -lt $latestVersion) { return 'SourceBehind' }
    if (-not $Active) { return 'Missing' }
    return 'UpdateAvailable'
}

function Get-HBMslProbe {
    param([hashtable]$Definition, [string]$Platform, [string]$Directory)
    $command = if ($Platform -eq 'Windows' -and $Definition['id'] -eq 'python') { 'python' } else { $Definition['command'] }
    $result = Invoke-HBMslProcess -Command $command -Arguments $Definition['args'] -Directory $Directory
    $version = ''
    if ($result.ExitCode -eq 0 -and $result.Output -match $Definition['versionPattern']) { $version = $Matches['version'] }
    $prerelease = $result.Output -match '(?i)\d+(?:\.\d+){0,3}[-.]?(alpha|beta|rc|preview|nightly|dev|ea)|Python \d+\.\d+\.\d+[ab]\d+'
    if ($prerelease) { $version = '' }
    # Ein funktionsfaehiger Compiler allein belegt weder javac noch Cargo.
    # A working primary command does not establish javac or Cargo availability.
    foreach ($companion in $Definition['companions']) {
        $probe = Invoke-HBMslProcess -Command $companion['command'] -Arguments $companion['args'] -Directory $Directory
        if ($probe.ExitCode -ne 0) { $version = '' }
        elseif ($probe.Output -match '(?i)\d+(?:\.\d+){0,3}[-.]?(alpha|beta|rc|preview|nightly|dev|ea)') { $version = '' }
        elseif ($companion['sameVersion'] -eq $true -and $probe.Output -match $Definition['versionPattern'] -and $version -and
            (ConvertTo-HBMslVersion $Matches['version']) -ne (ConvertTo-HBMslVersion $version)) { $version = '' }
        elseif ($companion['minimumPrimaryMinor'] -eq $true -and $version) {
            $primary = ConvertTo-HBMslVersion $version
            if ($probe.Output -notmatch $Definition['versionPattern']) { $version = '' }
            elseif ((ConvertTo-HBMslVersion $Matches['version']) -lt [version]::new($primary.Major, $primary.Minor, 0, 0)) { $version = '' }
        }
    }
    return @{ Version = $version; Executable = $result.Executable; Prerelease = $prerelease }
}

function Set-HBMslWindowsPath {
    param([hashtable]$Definition, [string]$Latest, [string]$Directory)
    $command = if ($Definition['id'] -eq 'python') { 'python' } else { $Definition['command'] }
    $candidates = @(Get-Command $command -All -CommandType Application -ErrorAction SilentlyContinue | ForEach-Object { $_.Source })
    if ($Definition['id'] -eq 'python') {
        $version = ConvertTo-HBMslVersion $Latest
        $python = Invoke-HBMslProcess py @("-$($version.Major).$($version.Minor)", '-c', 'import sys; print(sys.executable)') $Directory
        if ($python.ExitCode -eq 0 -and (Test-Path -LiteralPath $python.Output.Trim() -PathType Leaf)) { $candidates += $python.Output.Trim() }
    }
    foreach ($candidate in $candidates | Select-Object -Unique) {
        if ($candidate -like '*\WindowsApps\*') { continue }
        $probe = Invoke-HBMslProcess $candidate $Definition['args'] $Directory
        if ($probe.ExitCode -ne 0 -or $probe.Output -notmatch $Definition['versionPattern']) { continue }
        if ((ConvertTo-HBMslVersion $Matches['version']) -lt (ConvertTo-HBMslVersion $Latest)) { continue }
        $bin = Split-Path -Parent $candidate
        $old = [Environment]::GetEnvironmentVariable('Path', 'User')
        $parts = @($bin) + @($old -split ';' | Where-Object { $_ -and $_ -ne $bin })
        $updated = ($parts | Select-Object -Unique) -join ';'
        if ($old -ne $updated) {
            $backupRoot = Join-Path ([Environment]::GetFolderPath('UserProfile')) '.home-baseline/toolchains'
            [void][IO.Directory]::CreateDirectory($backupRoot)
            [IO.File]::WriteAllText((Join-Path $backupRoot "user-path-$([datetime]::UtcNow.ToString('yyyyMMddHHmmssfff')).txt"), [string]$old)
            [Environment]::SetEnvironmentVariable('Path', $updated, 'User')
        }
        $env:PATH = "$bin;$env:PATH"
        return
    }
}

function Expand-HBMslPackage {
    param([string]$Template, [string]$Version)
    $number = ConvertTo-HBMslVersion $Version
    if (-not $number) { throw 'Ungueltige Zielversion / invalid target version.' }
    $name = $Template.Replace('{major}', [string]$number.Major).Replace('{minor}', [string]$number.Minor)
    if ($name -notmatch '^[A-Za-z0-9][A-Za-z0-9@._+/-]*$' -or $name.Contains('..')) { throw 'Ungueltige Paket-ID / invalid package ID.' }
    return $name
}

function Get-HBMslPackageState {
    param([hashtable]$Definition, [hashtable]$Registry, [string]$Platform, [string]$Latest, [string]$Directory)
    if ($Platform -ne 'Windows' -and (Get-Command brew -ErrorAction SilentlyContinue)) {
        $formula = @($Registry['formulae'] | Where-Object { $_['mslToolchain'] -eq $Definition['id'] })
        if ($formula.Count -ne 1) { throw 'MSL-Formelzuordnung fehlt / missing MSL formula mapping.' }
        $formulaName = Expand-HBMslPackage $formula[0]['name'] $Latest
        $info = Invoke-HBMslProcess -Command brew -Arguments @('info', '--json=v2', '--formula', $formulaName) -Directory $Directory
        if ($info.ExitCode -ne 0) { throw 'Homebrew-Metadaten fehlen / Homebrew metadata unavailable.' }
        $data = ConvertFrom-Json -InputObject $info.Output -AsHashtable
        $item = @($data['formulae'])[0]
        $canonicalName = Expand-HBMslPackage $item['name'] $Latest
        $fresh = Get-HBMslRemoteData -Uri "https://formulae.brew.sh/api/formula/$canonicalName.json"
        $installed = @($item['installed'] | ForEach-Object { $_['version'] } | Sort-Object { ConvertTo-HBMslVersion $_ } -Descending)
        return @{ Manager = 'brew'; Package = $canonicalName;
            Source = [string]$fresh['versions']['stable']; Installed = $(if ($installed.Count) { [string]$installed[0] } else { '' }) }
    }
    if ($Platform -eq 'Windows') {
        $package = @($Registry['packages'] | Where-Object { $_['mslToolchain'] -eq $Definition['id'] })
        if ($package.Count -ne 1) { throw 'MSL-WinGet-Zuordnung fehlt / missing MSL WinGet mapping.' }
        $name = Expand-HBMslPackage $package[0]['id'] $Latest
        $info = Invoke-HBMslProcess -Command winget -Arguments @('show', '--id', $name, '--exact', '--source', 'winget',
            '--versions', '--accept-source-agreements', '--disable-interactivity') -Directory $Directory -TimeoutSeconds 30
        $versions = @([regex]::Matches($info.Output, '(?m)^\s*(\d+(?:\.\d+){1,3})\s*$') |
            ForEach-Object { $_.Groups[1].Value } | Sort-Object { ConvertTo-HBMslVersion $_ } -Descending)
        $source = if ($info.ExitCode -eq 0 -and $versions.Count) { [string]$versions[0] } else { '' }
        $list = Invoke-HBMslProcess winget @('list', '--id', $name, '--exact', '--source', 'winget',
            '--accept-source-agreements', '--disable-interactivity') $Directory 30
        $match = [regex]::Match($list.Output, [regex]::Escape($name) + '\s+(?<v>\d+(?:\.\d+){0,3})')
        $installed = if ($list.ExitCode -eq 0 -and $match.Success) { $match.Groups['v'].Value } else { '' }
        if ($Definition['id'] -eq 'rust' -and $source) { $source = $Latest } # rustup verwaltet einen eigenen Stable-Kanal.
        if ($Definition['id'] -eq 'rust' -and $installed) { $installed = (Get-HBMslProbe $Definition $Platform $Directory).Version }
        return @{ Manager = 'winget'; Package = $name; Source = $source; Installed = $installed }
    }
    if ($Definition['id'] -eq 'swift') {
        return @{ Manager = 'swiftly'; Package = 'swiftly'; Source = $Latest; Installed = '' }
    }
    $name = Expand-HBMslPackage $Definition['aptPackage'] $Latest
    $info = Invoke-HBMslProcess -Command 'apt-cache' -Arguments @('policy', $name) -Directory $Directory
    $candidate = [regex]::Match($info.Output, '(?m)^\s*Candidate:\s*(?:\d+:)?(?<v>\d+(?:\.\d+){0,3})')
    $installed = [regex]::Match($info.Output, '(?m)^\s*Installed:\s*(?:\d+:)?(?<v>\d+(?:\.\d+){0,3})')
    return @{ Manager = 'apt'; Package = $name; Source = $candidate.Groups['v'].Value; Installed = $installed.Groups['v'].Value }
}

function Set-HBMslManagedFile {
    param([string]$Path, [string]$Content, [string]$Marker)
    if (Test-Path -LiteralPath $Path) {
        $item = Get-Item -LiteralPath $Path -Force
        if ($item.PSIsContainer -or $item.LinkType -or -not (Get-Content -LiteralPath $Path -Raw).Contains($Marker)) {
            throw 'Nicht verwaltete Zieldatei / unmanaged target file.'
        }
        if ((Get-Content -LiteralPath $Path -Raw) -eq $Content) { return }
        Copy-HBMslBackup $Path
    }
    [void][IO.Directory]::CreateDirectory((Split-Path -Parent $Path))
    Write-HBMslTextFile $Path $Content
}

function Write-HBMslTextFile {
    param([string]$Path, [string]$Content)
    # A concurrently starting shell must see either complete profile version.
    $temporary = "$Path.$([guid]::NewGuid().ToString('N')).tmp"
    try {
        $options = [IO.FileStreamOptions]::new()
        $options.Mode = [IO.FileMode]::CreateNew
        $options.Access = [IO.FileAccess]::Write
        $options.Share = [IO.FileShare]::None
        $originalMode = $null
        if (-not $IsWindows) {
            if (Test-Path -LiteralPath $Path) { $originalMode = [IO.File]::GetUnixFileMode($Path) }
            $options.UnixCreateMode = [IO.UnixFileMode]::UserRead -bor [IO.UnixFileMode]::UserWrite
        }
        $stream = [IO.FileStream]::new($temporary, $options)
        try {
            $bytes = [Text.UTF8Encoding]::new($false).GetBytes($Content)
            $stream.Write($bytes, 0, $bytes.Length)
            $stream.Flush($true)
        } finally {
            $stream.Dispose()
        }
        if (-not $IsWindows -and $null -ne $originalMode) {
            [IO.File]::SetUnixFileMode($temporary, $originalMode)
        }
        [IO.File]::Move($temporary, $Path, $true)
    } finally { if (Test-Path -LiteralPath $temporary) { [IO.File]::Delete($temporary) } }
}

function Copy-HBMslBackup {
    param([string]$Path)
    $backup = "$Path.hb-backup-$([datetime]::UtcNow.ToString('yyyyMMddHHmmssfff'))"
    $options = [IO.FileStreamOptions]::new()
    $options.Mode = [IO.FileMode]::CreateNew
    $options.Access = [IO.FileAccess]::Write
    $options.Share = [IO.FileShare]::None
    if (-not $IsWindows) {
        $options.UnixCreateMode = [IO.UnixFileMode]::UserRead -bor [IO.UnixFileMode]::UserWrite
    }
    $source = [IO.File]::Open($Path, [IO.FileMode]::Open, [IO.FileAccess]::Read, [IO.FileShare]::Read)
    try {
        $destination = [IO.FileStream]::new($backup, $options)
        try {
            $source.CopyTo($destination)
            $destination.Flush($true)
        } finally { $destination.Dispose() }
    } finally { $source.Dispose() }
}

function Set-HBMslUnixPath {
    param([string]$UserHome, [string]$Directory, [switch]$PassThru, [switch]$Preview)
    $changed = $false
    $bin = Join-Path $UserHome '.home-baseline/toolchains/bin'
    $marker = '# BEGIN HOME-BASELINE MSL PATH'
    $block = @'
# BEGIN HOME-BASELINE MSL PATH
# Benutzer-CLI fuer stabile MSL / User CLI for stable MSL toolchains.
export SWIFTLY_HOME_DIR="$HOME/.home-baseline/toolchains/swiftly"
export SWIFTLY_BIN_DIR="$SWIFTLY_HOME_DIR/bin"
case "$PATH" in
  "$HOME/.home-baseline/toolchains/bin":*) ;;
  *)
    # macOS path_helper kann einen geerbten Eintrag nach hinten verschieben.
    # Restore precedence and remove duplicate entries without external tools.
    _hb_msl_rest="$PATH:"
    _hb_msl_path=""
    while [ -n "$_hb_msl_rest" ]; do
      _hb_msl_part="${_hb_msl_rest%%:*}"
      _hb_msl_rest="${_hb_msl_rest#*:}"
      if [ "$_hb_msl_part" != "$HOME/.home-baseline/toolchains/bin" ]; then
        _hb_msl_path="$_hb_msl_path:$_hb_msl_part"
      fi
    done
    export PATH="$HOME/.home-baseline/toolchains/bin$_hb_msl_path"
    unset _hb_msl_rest _hb_msl_path _hb_msl_part
    ;;
esac
# END HOME-BASELINE MSL PATH
'@ + "`n"
    $profiles = if ($env:SHELL -match 'zsh$') { @('.zprofile', '.zshrc') } else { @('.profile', '.bashrc') }
    foreach ($profileName in $profiles) {
        $path = Join-Path $UserHome $profileName
        $old = if (Test-Path -LiteralPath $path) {
            if ((Get-Item -LiteralPath $path -Force).LinkType) { throw 'Profil ist ein Link / profile is a link.' }
            [IO.File]::ReadAllText($path)
        } else { '' }
        $startCount = [regex]::Matches($old, '(?m)^# BEGIN HOME-BASELINE MSL PATH\r?$').Count
        $endCount = [regex]::Matches($old, '(?m)^# END HOME-BASELINE MSL PATH\r?$').Count
        if ($startCount -gt 1 -or $startCount -ne $endCount) { throw 'Profilblock widerspruechlich / conflicting profile block.' }
        if ($startCount -eq 1 -and [regex]::Matches($old, '(?ms)^# BEGIN HOME-BASELINE MSL PATH\r?\n.*?^# END HOME-BASELINE MSL PATH\r?\n?').Count -ne 1) {
            throw 'Profilblock-Reihenfolge widerspruechlich / conflicting profile block order.'
        }
        $updated = if ($startCount) {
            # Shell dollars are literal content, never .NET replacement tokens.
            [regex]::Replace($old, '(?ms)^# BEGIN HOME-BASELINE MSL PATH\r?\n.*?^# END HOME-BASELINE MSL PATH\r?\n?',
                [Text.RegularExpressions.MatchEvaluator]{ param($match) $block })
        } else { $old.TrimEnd() + "`n`n" + $block }
        if ($old -ne $updated) {
            $changed = $true
            if ($Preview) {
                Write-Host "PROFILE DRIFT: ~/$profileName (Swiftly/CLI activation)"
            } else {
                if (Test-Path -LiteralPath $path) { Copy-HBMslBackup $path }
                Write-HBMslTextFile $path $updated
            }
        }
    }
    if (-not $Preview) {
        $env:PATH = (@($bin) + @($env:PATH.Split([IO.Path]::PathSeparator) | Where-Object { $_ -ne $bin })) -join [IO.Path]::PathSeparator
    }
    if ($PassThru) { return $changed }
}

function Set-HBMslUnixPython {
    param([string]$Version, [string]$UserHome, [string]$Directory)
    $number = ConvertTo-HBMslVersion $Version
    $command = Get-Command "python$($number.Major).$($number.Minor)" -CommandType Application -ErrorAction Stop | Select-Object -First 1
    $probe = Invoke-HBMslProcess $command.Source @('--version') $Directory
    if ($probe.ExitCode -ne 0 -or $probe.Output -notmatch 'Python (\d+\.\d+\.\d+)' -or
        (ConvertTo-HBMslVersion $Matches[1]) -lt $number) { throw 'Python-Ziel nicht bestaetigt / Python target not verified.' }
    $quoteEscape = @'
'"'"'
'@
    $literal = "'" + $command.Source.Replace("'", $quoteEscape) + "'"
    $content = "#!/bin/sh`n# HOME-BASELINE MSL PYTHON LAUNCHER`nexec $literal `"`$@`"`n"
    $path = Join-Path $UserHome '.home-baseline/toolchains/bin/python3'
    Set-HBMslManagedFile $path $content 'HOME-BASELINE MSL PYTHON LAUNCHER'
    Invoke-HBMslAction chmod @('u+x', $path) $Directory
    Set-HBMslUnixPath $UserHome $Directory
}

function Install-HBMslSwift {
    param([string]$Version, [string]$Platform, [hashtable]$Registry, [string]$Directory,
        [string]$UserHome, [switch]$AllowAdminPrompts)
    $state = Join-Path $UserHome '.home-baseline/toolchains/swiftly'
    $env:SWIFTLY_HOME_DIR = $state
    $env:SWIFTLY_BIN_DIR = Join-Path $state 'bin'
    $swiftly = Get-Command swiftly -ErrorAction SilentlyContinue | Select-Object -First 1
    if (-not $swiftly -and (Test-Path -LiteralPath (Join-Path $env:SWIFTLY_BIN_DIR 'swiftly'))) {
        $swiftly = Get-Command (Join-Path $env:SWIFTLY_BIN_DIR 'swiftly')
    }
    if (-not $swiftly -and $Platform -eq 'Linux') {
        if (-not $AllowAdminPrompts) { throw 'DEFERRED_ADMIN_REQUIRED' }
        $helper = Join-Path $PSScriptRoot 'linux-maintenance-hardening.py'
        $config = Join-Path $Directory 'cli.json'
        $Registry | ConvertTo-Json -Depth 30 | Set-Content -LiteralPath $config -Encoding utf8NoBOM
        $arch = if ([Runtime.InteropServices.RuntimeInformation]::OSArchitecture -eq 'Arm64') { 'aarch64' } else { 'x86_64' }
        $probe = Invoke-HBMslProcess python3 @($helper, 'swift-contract', '--registry', $config,
            '--os-release', '/etc/os-release', '--architecture', $arch) $Directory
        if ($probe.ExitCode -ne 0) { throw 'Swiftly-Plattform nicht freigegeben / unsupported Swiftly platform.' }
        $contract = ConvertFrom-Json $probe.Output -AsHashtable
        $archive = Join-Path $Directory 'swiftly.tar.gz'
        Invoke-WebRequest -Uri $contract['url'] -OutFile $archive -TimeoutSec 120
        if ((Get-FileHash -LiteralPath $archive -Algorithm SHA256).Hash.ToLowerInvariant() -ne $contract['sha256']) { throw 'Swiftly SHA-256 mismatch.' }
        $binary = Join-Path $Directory 'swiftly-bootstrap'
        Invoke-HBMslAction python3 @($helper, 'extract-swiftly', '--archive', $archive, '--output', $binary) $Directory
        Invoke-HBMslAction $binary @('init', '--assume-yes', '--skip-install', '--no-modify-profile', '--platform', $contract['swiftlyPlatform']) $Directory
        $swiftly = Get-Command (Join-Path $env:SWIFTLY_BIN_DIR 'swiftly')
    }
    if (-not $swiftly) { throw 'Swiftly fehlt / Swiftly is missing.' }
    if (-not (Test-Path -LiteralPath (Join-Path $state 'config.json'))) {
        Invoke-HBMslAction $swiftly.Source @('init', '--assume-yes', '--skip-install', '--no-modify-profile') $Directory
    }
    # Konkreten Snapshot installieren; kein zweites "latest" waehrend desselben Laufs.
    # Install the concrete snapshot, not another moving "latest" in the same run.
    $post = Join-Path $Directory 'swift-post-install.sh'
    Invoke-HBMslAction $swiftly.Source @('install', $Version, '--assume-yes', '--post-install-file', $post) $Directory
    if ((Test-Path -LiteralPath $post) -and (Get-Item -LiteralPath $post).Length -gt 0) {
        if (-not $AllowAdminPrompts) { throw 'DEFERRED_ADMIN_REQUIRED' }
        Invoke-HBMslAction sudo @('--', 'bash', $post) $Directory
    }
    Invoke-HBMslAction $swiftly.Source @('use', '--global-default', $Version, '--assume-yes') $Directory
    $bin = Join-Path $UserHome '.home-baseline/toolchains/bin'
    foreach ($command in @('swift', 'swiftc', 'sourcekit-lsp')) {
        # Nur Swift-Kommandos exponieren; Swiftlys clang-Proxies bleiben privat.
        # Expose only Swift commands; keep Swiftly's clang proxies private.
        $content = @'
#!/bin/sh
# HOME-BASELINE MSL SWIFT LAUNCHER
export SWIFTLY_HOME_DIR="$HOME/.home-baseline/toolchains/swiftly"
export SWIFTLY_BIN_DIR="$SWIFTLY_HOME_DIR/bin"
if command -v swiftly >/dev/null 2>&1; then
  exec swiftly run __COMMAND__ "$@"
fi
exec "$SWIFTLY_BIN_DIR/swiftly" run __COMMAND__ "$@"
'@.Replace('__COMMAND__', $command) + "`n"
        $path = Join-Path $bin $command
        Set-HBMslManagedFile $path $content 'HOME-BASELINE MSL SWIFT LAUNCHER'
        Invoke-HBMslAction chmod @('u+x', $path) $Directory
    }
    Set-HBMslUnixPath $UserHome $Directory
}

function Invoke-HBMslMaintenance {
    [CmdletBinding()]
    param([Parameter(Mandatory)][hashtable]$CliRegistry, [Parameter(Mandatory)][hashtable]$PackageRegistry,
        [ValidateSet('Darwin', 'Linux', 'Windows')][string]$Platform,
        [switch]$CompareOnly, [switch]$Preview, [switch]$SkipUpgrade, [switch]$AllowAdminPrompts,
        [string]$UserHome = [Environment]::GetFolderPath('UserProfile'))
    $definitions = @($CliRegistry['mslToolchains'])
    $ids = @($definitions | ForEach-Object { $_['id'] } | Sort-Object)
    if ($definitions.Count -ne 6 -or @(Compare-Object $ids @('dotnet', 'go', 'java', 'python', 'rust', 'swift')).Count) {
        throw 'Genau sechs MSL-Vertraege erforderlich / exactly six MSL contracts required.'
    }
    $directory = Join-Path ([IO.Path]::GetTempPath()) ([IO.Path]::GetRandomFileName())
    [void][IO.Directory]::CreateDirectory($directory)
    $saved = @{}
    foreach ($name in @('PATH', 'HOMEBREW_NO_AUTO_UPDATE', 'HOMEBREW_NO_ANALYTICS', 'HOMEBREW_NO_INSTALL_CLEANUP', 'SWIFTLY_HOME_DIR', 'SWIFTLY_BIN_DIR', 'LC_ALL')) {
        $saved[$name] = [Environment]::GetEnvironmentVariable($name, 'Process')
    }
    $env:HOMEBREW_NO_AUTO_UPDATE = '1'
    $env:HOMEBREW_NO_ANALYTICS = '1'
    # PowerShell may itself be hosted by the .NET runtime being upgraded.
    # Keep the running runtime's files until a later separate cleanup.
    $env:HOMEBREW_NO_INSTALL_CLEANUP = '1'
    $env:LC_ALL = 'C'
    $items = [Collections.Generic.List[object]]::new()
    $checkedAt = [datetimeoffset]::UtcNow.ToString('o')
    $deferred = $false
    try {
        if (-not $CompareOnly -and -not $SkipUpgrade) {
            if ($Platform -ne 'Windows' -and (Get-Command brew -ErrorAction SilentlyContinue)) {
                Invoke-HBMslAction brew @('update') $directory -Preview:$Preview
            } elseif ($Platform -eq 'Linux' -and $AllowAdminPrompts) {
                Invoke-HBMslAction sudo @('--', 'apt-get', 'update') $directory -Preview:$Preview
            } elseif ($Platform -eq 'Windows' -and ($AllowAdminPrompts -or $Preview)) {
                Invoke-HBMslAction winget @('source', 'update', '--name', 'winget', '--disable-interactivity') $directory -Preview:$Preview
            }
        }
        foreach ($definition in $definitions) {
            $item = [ordered]@{ toolchainId = $definition['id']; installedVersion = ''; activeVersion = '';
                sourceVersion = ''; latestStableVersion = ''; executable = ''; sourceUrl = $definition['releaseUri'];
                packageId = ''; packageManager = '';
                checkedAt = $checkedAt; status = 'Unknown'; changed = $false; environmentChanged = $false;
                failureClass = ''; nextAction = 'Release-Quelle und CLI pruefen / check release source and CLI.' }
            $phase = 'Probe'
            try {
                $probe = Get-HBMslProbe $definition $Platform $directory
                $item.activeVersion = $probe.Version
                if ($probe['Prerelease']) { $item.failureClass = 'PrereleaseActive' }
                $item.executable = $probe.Executable.Replace($UserHome, '~')
                $phase = 'ReleaseMetadata'
                $payload = Get-HBMslRemoteData $definition['releaseUri'] -Text:($definition['provider'] -eq 'rust')
                $latest = Get-HBMslReleaseVersion $definition['provider'] $payload
                if ($definition['id'] -eq 'java') {
                    $assets = Get-HBMslRemoteData "https://api.adoptium.net/v3/assets/latest/$latest/hotspot?architecture=x64&image_type=jdk&os=windows&vendor=eclipse"
                    $latest = Get-HBMslReleaseVersion 'java-patch' $assets
                }
                $item.latestStableVersion = $latest
                $phase = 'PackageMetadata'
                $state = Get-HBMslPackageState $definition $PackageRegistry $Platform $latest $directory
                $item.packageId = $state.Package
                $item.packageManager = $state.Manager
                if ($definition['id'] -eq 'swift' -and $Platform -ne 'Windows') { $state.Source = $latest }
                $item.installedVersion = if ($definition['id'] -eq 'swift') { $probe.Version } else { $state.Installed }
                $item.activeVersion = $probe.Version
                $item.executable = $probe.Executable.Replace($UserHome, '~')
                $item.sourceVersion = $state.Source
                $item.status = Get-HBMslStatus $probe.Version $state.Installed $state.Source $latest
                $needsSwift = $definition['id'] -eq 'swift' -and $Platform -ne 'Windows' -and
                    -not $probe.Executable.StartsWith((Join-Path $UserHome '.home-baseline/toolchains/bin') + [IO.Path]::DirectorySeparatorChar)
                $activationOnly = $false
                if ($needsSwift -and $item.status -eq 'Current') { $item.status = 'PathMismatch' }
                if ($definition['id'] -eq 'swift' -and $Platform -ne 'Windows' -and
                    $item.status -eq 'Current') {
                    $phase = 'Activation'
                    $item.environmentChanged = Set-HBMslUnixPath $UserHome $directory -PassThru -Preview:($CompareOnly -or $Preview)
                    if ($item.environmentChanged) {
                        $activationOnly = $true
                        if ($CompareOnly) { $item.status = 'PathMismatch' }
                        elseif ($Preview) { $item.status = 'Planned' }
                    }
                }
                $canInstall = $state.Source -and (ConvertTo-HBMslVersion $state.Source) -ge (ConvertTo-HBMslVersion $latest)
                if ($item.status -ne 'Current' -and -not $activationOnly -and $canInstall -and -not $CompareOnly -and
                    (-not $SkipUpgrade -or -not $probe.Version)) {
                    if ($Preview) {
                        Write-Host "DRY-RUN: MSL $($definition['id']) -> $latest via $($state.Manager)/$($state.Package)"
                        $item.status = 'Planned'
                    } else {
                        $phase = 'Installation'
                        switch ($state.Manager) {
                            'brew' {
                                $verb = if ($state.Installed) { 'upgrade' } else { 'install' }
                                Invoke-HBMslAction brew @($verb, '--formula', $state.Package) $directory
                            }
                            'winget' {
                                if (-not $AllowAdminPrompts) { throw 'DEFERRED_ADMIN_REQUIRED' }
                                $installed = Invoke-HBMslProcess winget @('list', '--id', $state.Package, '--exact', '--source', 'winget', '--accept-source-agreements', '--disable-interactivity') $directory 30
                                $verb = if ($installed.ExitCode -eq 0 -and $installed.Output.Contains($state.Package)) { 'upgrade' } else { 'install' }
                                Invoke-HBMslAction winget @($verb, '--id', $state.Package, '--exact', '--source', 'winget', '--accept-package-agreements', '--accept-source-agreements', '--disable-interactivity') $directory
                                $env:PATH = [Environment]::GetEnvironmentVariable('Path', 'User') + ';' + [Environment]::GetEnvironmentVariable('Path', 'Machine')
                                if ($definition['id'] -eq 'rust') {
                                    Invoke-HBMslAction rustup @('update', 'stable') $directory
                                    Invoke-HBMslAction rustup @('default', 'stable') $directory
                                }
                                Set-HBMslWindowsPath $definition $latest $directory
                            }
                            'apt' {
                                if (-not $AllowAdminPrompts) { throw 'DEFERRED_ADMIN_REQUIRED' }
                                $packages = @('--', 'apt-get', 'install', '-y', '--', $state.Package)
                                if ($definition['id'] -eq 'rust') { $packages += 'cargo' }
                                Invoke-HBMslAction sudo $packages $directory
                            }
                        }
                        if ($definition['id'] -eq 'swift' -and $Platform -ne 'Windows') {
                            Install-HBMslSwift $latest $Platform $CliRegistry $directory $UserHome -AllowAdminPrompts:$AllowAdminPrompts
                        }
                        if ($definition['id'] -eq 'python' -and $Platform -ne 'Windows') {
                            Set-HBMslUnixPython $latest $UserHome $directory
                        }
                        $item.changed = $true
                        $probe = Get-HBMslProbe $definition $Platform $directory
                        $state = Get-HBMslPackageState $definition $PackageRegistry $Platform $latest $directory
                        $item.activeVersion = $probe.Version
                        $item.installedVersion = if ($definition['id'] -eq 'swift') { $probe.Version } else { $state.Installed }
                        $item.executable = $probe.Executable.Replace($UserHome, '~')
                        $item.status = Get-HBMslStatus $probe.Version $item.installedVersion $item.sourceVersion $latest
                    }
                }
                if ($definition['id'] -eq 'dotnet' -and $state.Manager -eq 'brew') {
                    # Homebrew PowerShell references SDK packs through opt/dotnet.
                    # A running interpreter alone does not prove those references.
                    $phase = 'MaintenanceEngine'
                    if (-not (Test-HBMslPowerShellHealth $directory)) {
                        if ($Preview) { $item.status = 'Planned' }
                        elseif ($CompareOnly -or $SkipUpgrade) { throw 'PowerShell-Referenzen defekt / broken PowerShell references.' }
                        else {
                            $engine = Invoke-HBMslProcess brew @('list', '--formula', '--versions', 'powershell') $directory
                            if ($engine.ExitCode -ne 0) { throw 'PowerShell-Reparaturquelle fehlt / no PowerShell repair source.' }
                            Invoke-HBMslAction brew @('upgrade', '--formula', 'powershell') $directory
                            $item.changed = $true
                            if (-not (Test-HBMslPowerShellHealth $directory)) { throw 'PowerShell-Reparatur unbestaetigt / PowerShell repair not verified.' }
                        }
                    }
                }
                $item.nextAction = switch ($item.status) {
                    'Current' { 'N/A' }
                    'SourceBehind' { 'Paketquelle liegt zurueck; spaeter erneut pruefen / package source behind; check again later.' }
                    'PathMismatch' { 'CLI-Aufloesung korrigieren / correct CLI resolution.' }
                    'Planned' { 'Vorschau geprueft; Wartung ausfuehren / review preview, then run maintenance.' }
                    default { 'MSL-Wartung oder Paketquelle pruefen / check MSL maintenance or package source.' }
                }
            } catch {
                if ($_.Exception.Message -eq 'DEFERRED_ADMIN_REQUIRED') {
                    $deferred = $true
                    $item.status = 'DeferredAdminRequired'
                    $item.nextAction = 'Mit aktueller Admin-Prompt-Autoritaet erneut ausfuehren / rerun with current admin-prompt authority.'
                } else {
                    $item.status = if ($item.latestStableVersion) { 'Failed' } else { 'Unknown' }
                    $item.failureClass = "${phase}Failed"
                    $item.nextAction = "MSL ${phase} pruefen; keine vollstaendige Aktualitaet bestaetigt / check MSL ${phase}; currency not established."
                }
            }
            Write-Host ("MSL {0}: active={1}; source={2}; stable={3}; {4}" -f $item.toolchainId,
                $item.activeVersion, $item.sourceVersion, $item.latestStableVersion, $item.status)
            if ($item.status -ne 'Current') { Write-Host $item.nextAction }
            $items.Add([pscustomobject]$item)
        }
        $remaining = @($items | Where-Object { $_.status -ne 'Current' } | ForEach-Object { $_.toolchainId })
        $exitCode = if ($deferred -and $Platform -eq 'Windows') { 75 } elseif ($remaining.Count) { 1 } else { 0 }
        return [ordered]@{ schemaVersion = '1.0'; platform = $Platform;
            mode = $(if ($CompareOnly) { 'compare-only' } elseif ($Preview) { 'dry-run' } else { 'update' });
            overallStatus = $(if ($remaining.Count) { 'PARTIAL' } else { 'SUCCESS' }); exitCode = $exitCode;
            items = @(); remainingRequired = $remaining; optionalDrift = @();
            nextAction = $(if ($remaining.Count) { 'Offene MSL-Befunde beheben / resolve MSL findings.' } else { 'N/A' });
            mslToolchains = @($items.ToArray()) }
    } finally {
        foreach ($name in $saved.Keys) { [Environment]::SetEnvironmentVariable($name, $saved[$name], 'Process') }
        # Temp-Evidence wird auch nach einer Vorschau entfernt; keine Produktmutation.
        # Clean temporary evidence after preview as well; no product mutation.
        Remove-Item -LiteralPath $directory -Recurse -Force -WhatIf:$false -Confirm:$false
    }
}

Export-ModuleMember -Function ConvertTo-HBMslVersion, Get-HBMslReleaseVersion, Get-HBMslStatus, Invoke-HBMslMaintenance
