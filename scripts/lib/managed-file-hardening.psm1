Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Get-HBManagedPath {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$Root,
        [Parameter(Mandatory)][string]$RelativePath,
        [switch]$CreateParent,
        [switch]$AllowMissingParent
    )

    if ([string]::IsNullOrWhiteSpace($RelativePath) `
        -or [IO.Path]::IsPathRooted($RelativePath) `
        -or $RelativePath -match '^[A-Za-z]:[/\\]' `
        -or $RelativePath -match '^[/\\]{2}' `
        -or $RelativePath -match '(^|/)\.\.(/|$)' `
        -or $RelativePath -match '(^|/)-' `
        -or $RelativePath -match '\\' `
        -or $RelativePath -match '[\x00-\x1f]') {
        throw 'Unsafe managed relative path / Unsicherer verwalteter relativer Pfad.'
    }

    $rootItem = Get-Item -LiteralPath $Root -Force -ErrorAction Stop
    if (-not $rootItem.PSIsContainer `
        -or $rootItem.LinkType `
        -or ($rootItem.Attributes -band [IO.FileAttributes]::ReparsePoint)) {
        throw "Managed root must be a regular directory: $Root"
    }
    $rootFull = [IO.Path]::GetFullPath($rootItem.FullName)
    $targetFull = [IO.Path]::GetFullPath((Join-Path $rootFull $RelativePath))
    $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
    $prefix = $rootFull.TrimEnd([IO.Path]::DirectorySeparatorChar, [IO.Path]::AltDirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
    if (-not $targetFull.StartsWith($prefix, $comparison)) {
        throw "Managed path escapes root: $RelativePath"
    }

    $parts = @($RelativePath -split '/' | Where-Object { $_ })
    $current = $rootFull
    for ($index = 0; $index -lt $parts.Count; $index++) {
        $current = Join-Path $current $parts[$index]
        $item = Get-Item -LiteralPath $current -Force -ErrorAction SilentlyContinue
        $isLeaf = $index -eq ($parts.Count - 1)
        if ($item) {
            if ($item.LinkType -or ($item.Attributes -band [IO.FileAttributes]::ReparsePoint)) {
                throw "Managed path contains a link or reparse point: $RelativePath"
            }
            if (-not $isLeaf -and -not $item.PSIsContainer) {
                throw "Managed parent is not a directory: $RelativePath"
            }
            if ($isLeaf -and $item.PSIsContainer) {
                throw "Managed file destination is a directory: $RelativePath"
            }
        } elseif (-not $isLeaf) {
            if (-not $CreateParent -and -not $AllowMissingParent) {
                throw "Managed parent does not exist: $RelativePath"
            }
            if ($CreateParent) {
                $null = [IO.Directory]::CreateDirectory($current)
                $created = Get-Item -LiteralPath $current -Force
                if ($created.LinkType -or ($created.Attributes -band [IO.FileAttributes]::ReparsePoint)) {
                    throw "Managed parent became a link or reparse point: $RelativePath"
                }
            }
        }
    }
    return $targetFull
}

function Get-HBManagedDirectoryPath {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$Root,
        [Parameter(Mandatory)][string]$RelativePath
    )

    if ([string]::IsNullOrWhiteSpace($RelativePath) `
        -or [IO.Path]::IsPathRooted($RelativePath) `
        -or $RelativePath -match '^[A-Za-z]:[/\\]' `
        -or $RelativePath -match '^[/\\]{2}' `
        -or $RelativePath -match '(^|/)\.\.(/|$)' `
        -or $RelativePath -match '(^|/)-' `
        -or $RelativePath -match '\\' `
        -or $RelativePath -match '[\x00-\x1f]') {
        throw 'Unsafe managed relative directory path / Unsicherer verwalteter relativer Verzeichnispfad.'
    }

    $rootItem = Get-Item -LiteralPath $Root -Force -ErrorAction Stop
    if (-not $rootItem.PSIsContainer `
        -or $rootItem.LinkType `
        -or ($rootItem.Attributes -band [IO.FileAttributes]::ReparsePoint)) {
        throw "Managed root must be a regular directory: $Root"
    }
    $rootFull = [IO.Path]::GetFullPath($rootItem.FullName)
    $targetFull = [IO.Path]::GetFullPath((Join-Path $rootFull $RelativePath))
    $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
    $prefix = $rootFull.TrimEnd([IO.Path]::DirectorySeparatorChar, [IO.Path]::AltDirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
    if (-not $targetFull.StartsWith($prefix, $comparison)) {
        throw "Managed directory escapes root: $RelativePath"
    }
    $current = $rootFull
    foreach ($component in @($RelativePath -split '/' | Where-Object { $_ })) {
        $current = Join-Path $current $component
        $item = Get-Item -LiteralPath $current -Force -ErrorAction Stop
        if (-not $item.PSIsContainer `
            -or $item.LinkType `
            -or ($item.Attributes -band [IO.FileAttributes]::ReparsePoint)) {
            throw "Managed directory contains a link, reparse point, or non-directory: $RelativePath"
        }
    }
    return $targetFull
}

function Read-HBManagedText {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$Root,
        [Parameter(Mandatory)][string]$RelativePath
    )
    $path = Get-HBManagedPath -Root $Root -RelativePath $RelativePath
    if (-not (Test-Path -LiteralPath $path -PathType Leaf)) {
        throw "Managed input does not exist: $RelativePath"
    }
    return [IO.File]::ReadAllText($path, [Text.UTF8Encoding]::new($false, $true))
}

function Write-HBManagedTextAtomic {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$Root,
        [Parameter(Mandatory)][string]$RelativePath,
        [Parameter(Mandatory)][AllowEmptyString()][string]$Content,
        [Text.Encoding]$Encoding = [Text.UTF8Encoding]::new($false),
        [switch]$CreateParent
    )
    $target = Get-HBManagedPath -Root $Root -RelativePath $RelativePath -CreateParent:$CreateParent
    $parent = Split-Path -Parent $target
    $temporary = Join-Path $parent ('.' + [IO.Path]::GetFileName($target) + '.' + [Guid]::NewGuid().ToString('N') + '.tmp')
    $stream = $null
    $writer = $null
    try {
        $stream = [IO.FileStream]::new(
            $temporary,
            [IO.FileMode]::CreateNew,
            [IO.FileAccess]::Write,
            [IO.FileShare]::None,
            65536,
            [IO.FileOptions]::WriteThrough
        )
        $writer = [IO.StreamWriter]::new($stream, $Encoding)
        $stream = $null
        $writer.Write($Content)
        $writer.Flush()
        $writer.Dispose()
        $writer = $null

        $target = Get-HBManagedPath -Root $Root -RelativePath $RelativePath -CreateParent:$CreateParent
        [IO.File]::Move($temporary, $target, $true)
    } finally {
        if ($writer) { $writer.Dispose() }
        if ($stream) { $stream.Dispose() }
        if (Test-Path -LiteralPath $temporary) {
            Remove-Item -LiteralPath $temporary -Force
        }
    }
}

function Copy-HBManagedFileAtomic {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$SourceRoot,
        [Parameter(Mandatory)][string]$SourceRelativePath,
        [Parameter(Mandatory)][string]$Root,
        [Parameter(Mandatory)][string]$RelativePath,
        [switch]$CreateParent
    )

    $source = Get-HBManagedPath -Root $SourceRoot -RelativePath $SourceRelativePath
    if (-not (Test-Path -LiteralPath $source -PathType Leaf)) {
        throw "Managed source does not exist: $SourceRelativePath"
    }
    $target = Get-HBManagedPath -Root $Root -RelativePath $RelativePath -CreateParent:$CreateParent
    $parent = Split-Path -Parent $target
    $temporary = Join-Path $parent ('.' + [IO.Path]::GetFileName($target) + '.' + [Guid]::NewGuid().ToString('N') + '.tmp')
    try {
        [IO.File]::Copy($source, $temporary, $false)
        $source = Get-HBManagedPath -Root $SourceRoot -RelativePath $SourceRelativePath
        if (-not (Test-Path -LiteralPath $source -PathType Leaf)) {
            throw "Managed source changed during copy: $SourceRelativePath"
        }
        $target = Get-HBManagedPath -Root $Root -RelativePath $RelativePath -CreateParent:$CreateParent
        [IO.File]::Move($temporary, $target, $true)
    } finally {
        if (Test-Path -LiteralPath $temporary) {
            Remove-Item -LiteralPath $temporary -Force
        }
    }
}

function Write-HBManagedTextSetAtomic {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$Root,
        [Parameter(Mandatory)][System.Collections.IDictionary]$ContentByRelativePath,
        [Text.Encoding]$Encoding = [Text.UTF8Encoding]::new($false)
    )

    $staged = [Collections.Generic.List[object]]::new()
    try {
        foreach ($relative in $ContentByRelativePath.Keys) {
            $target = Get-HBManagedPath -Root $Root -RelativePath ([string]$relative)
            $parent = Split-Path -Parent $target
            $temporary = Join-Path $parent ('.' + [IO.Path]::GetFileName($target) + '.' + [Guid]::NewGuid().ToString('N') + '.stage')
            $stream = [IO.FileStream]::new($temporary, [IO.FileMode]::CreateNew, [IO.FileAccess]::Write, [IO.FileShare]::None)
            try {
                $writer = [IO.StreamWriter]::new($stream, $Encoding)
                $stream = $null
                try { $writer.Write([string]$ContentByRelativePath[$relative]) } finally { $writer.Dispose() }
            } finally {
                if ($stream) { $stream.Dispose() }
            }
            $staged.Add([pscustomobject]@{
                Relative = [string]$relative
                Target = $target
                Temporary = $temporary
                Backup = Join-Path $parent ('.' + [IO.Path]::GetFileName($target) + '.' + [Guid]::NewGuid().ToString('N') + '.backup')
                HadOriginal = Test-Path -LiteralPath $target -PathType Leaf
                Published = $false
            })
        }

        foreach ($item in $staged) {
            $item.Target = Get-HBManagedPath -Root $Root -RelativePath $item.Relative
        }
        foreach ($item in $staged) {
            if ($item.HadOriginal) { [IO.File]::Move($item.Target, $item.Backup, $false) }
            try {
                [IO.File]::Move($item.Temporary, $item.Target, $false)
                $item.Published = $true
            } catch {
                if ($item.HadOriginal -and (Test-Path -LiteralPath $item.Backup)) {
                    [IO.File]::Move($item.Backup, $item.Target, $false)
                }
                throw
            }
        }
        foreach ($item in $staged) {
            if (Test-Path -LiteralPath $item.Backup) { Remove-Item -LiteralPath $item.Backup -Force -ErrorAction SilentlyContinue }
        }
    } catch {
        for ($index = $staged.Count - 1; $index -ge 0; $index--) {
            $item = $staged[$index]
            if ($item.Published -and (Test-Path -LiteralPath $item.Target)) {
                Remove-Item -LiteralPath $item.Target -Force
            }
            if ($item.HadOriginal -and (Test-Path -LiteralPath $item.Backup)) {
                [IO.File]::Move($item.Backup, $item.Target, $false)
            }
        }
        throw
    } finally {
        foreach ($item in $staged) {
            if (Test-Path -LiteralPath $item.Temporary) { Remove-Item -LiteralPath $item.Temporary -Force }
            if (Test-Path -LiteralPath $item.Backup) { Remove-Item -LiteralPath $item.Backup -Force }
        }
    }
}

Export-ModuleMember -Function Get-HBManagedPath, Get-HBManagedDirectoryPath, Read-HBManagedText, Copy-HBManagedFileAtomic, Write-HBManagedTextAtomic, Write-HBManagedTextSetAtomic
