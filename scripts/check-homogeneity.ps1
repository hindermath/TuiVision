# check-homogeneity.ps1 — Workspace Homogeneity Guardian Compliance Scanner v1.0 (PowerShell)
# FR-001 through FR-006, FR-016-FR-019

[CmdletBinding()]
param(
    [Alias('WorkspaceName')]
    [string]$TargetDir = $(if ($env:HOME) { $env:HOME } else { $env:USERPROFILE }),
    [switch]$Json,
    [switch]$DryRun,
    [string]$ApplyPatch = '',
    [switch]$NoPatch,
    [switch]$FailFast,
    [switch]$Yes
)

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$LibDir    = Join-Path $ScriptDir 'lib'

# Source all hg-*.ps1 libs
Get-ChildItem -Path $LibDir -Filter 'hg-*.ps1' -ErrorAction SilentlyContinue | ForEach-Object {
    . $_.FullName
}

# --json takes precedence
if ($Json) { $VerbosePreference = 'SilentlyContinue' }

$HomeDir = if ($env:HOME) { $env:HOME } else { $env:USERPROFILE }
$TargetDir = $TargetDir.Trim()
if ($TargetDir -eq '~') {
    $TargetDir = $HomeDir
} elseif ($TargetDir.StartsWith('~/') -or $TargetDir.StartsWith('~\')) {
    $TargetDir = Join-Path $HomeDir $TargetDir.Substring(2)
} elseif (-not [IO.Path]::IsPathRooted($TargetDir) -and
    -not (Test-Path -LiteralPath $TargetDir -PathType Container)) {
    $homeCandidate = Join-Path $HomeDir $TargetDir
    if (Test-Path -LiteralPath $homeCandidate -PathType Container) {
        $TargetDir = $homeCandidate
    }
}
$TargetDir = $TargetDir.TrimEnd(
    [IO.Path]::DirectorySeparatorChar,
    [IO.Path]::AltDirectorySeparatorChar
)
if (-not (Test-Path -LiteralPath $TargetDir -PathType Container)) {
    Write-Error "FATAL: target directory not found: $TargetDir"
    exit 2
}
$TargetDir = (Resolve-Path -LiteralPath $TargetDir).Path

# Check ripgrep availability
if (-not (Get-Command rg -ErrorAction SilentlyContinue)) {
    Write-Error "FATAL: ripgrep (rg) not found"
    exit 2
}

# ─── Apply-Patch Mode ────────────────────────────────────────────────────────
if ($ApplyPatch) {
    if (-not (Test-Path $ApplyPatch)) {
        Write-Error "FATAL: patch file not found: $ApplyPatch"
        exit 2
    }
    $content = Get-Content $ApplyPatch
    Write-Host "Patch-Datei: $ApplyPatch"
    Write-Host ""
    Write-Host "Vorgeschlagene Aenderungen:"
    $content | Select-String '^\#\#|^- |^\*\*' | Select-Object -First 30 | ForEach-Object { Write-Host $_.Line }
    Write-Host ""

    $answer = if ($Yes) { 'j' } else { Read-Host "Patch anwenden? [j/N]" }
    if ($answer -match '^[jJyY]') {
        $patchCount = 0
        $targetFile = ''
        foreach ($line in $content) {
            if ($line -match '### Target: (.+)') { $targetFile = $Matches[1] }
            if ($line -match '^\+\+\+ (.+)' -and $targetFile) {
                Add-Content -Path $targetFile -Value $Matches[1]
                $patchCount++
            }
        }
        git -C $TargetDir add -A 2>$null
        git -C $TargetDir commit -m "chore: apply memory-patch -- $patchCount entries updated" 2>$null
        Write-Host "Patch angewendet: $patchCount Eintraege, git-Commit erstellt."
    } else {
        Write-Host "Patch abgebrochen."
    }
    exit 0
}

# ─── Scan State ──────────────────────────────────────────────────────────────
$Failures  = [System.Collections.Generic.List[string]]::new()
$Warnings  = [System.Collections.Generic.List[string]]::new()
$DirTotal  = @{}
$DirPass   = @{}
$ScanDirs  = [System.Collections.Generic.List[string]]::new()
$TotalChecks = 0
$TotalPass   = 0

$RequiredFiles = @('AGENTS.md','CLAUDE.md','GEMINI.md','README.md','STATS.md','constitution.md')

# Per-level counters
$L0Total = 0; $L0Pass = 0
$L1Total = 0; $L1Pass = 0
$L2Total = 0; $L2Pass = 0
$CurrentLevel = 0

function Emit-Result {
    param([string]$Status, [string]$FilePath, [string]$Message, [string]$Dir)
    $script:TotalChecks++
    $script:DirTotal[$Dir] = ($script:DirTotal[$Dir] ?? 0) + 1

    # Per-level tracking
    switch ($script:CurrentLevel) {
        0 { $script:L0Total++; if ($Status -eq 'PASS') { $script:L0Pass++ } }
        1 { $script:L1Total++; if ($Status -eq 'PASS') { $script:L1Pass++ } }
        2 { $script:L2Total++; if ($Status -eq 'PASS') { $script:L2Pass++ } }
    }

    if ($Status -eq 'PASS') {
        $script:TotalPass++
        $script:DirPass[$Dir] = ($script:DirPass[$Dir] ?? 0) + 1
        if (-not $Json -and $VerbosePreference -ne 'SilentlyContinue') {
            Write-Host ("  {0,-4} {1,-40} {2}" -f '✓', $FilePath, $Message)
        }
    } elseif ($Status -eq 'WARN') {
        $ws = Split-Path $Dir -Leaf
        $script:Warnings.Add("$($script:CurrentLevel)|${ws}|${FilePath}|${Message}")
        if (-not $Json) {
            Write-Host ("  {0,-4} {1,-40} WARN: {2}" -f 'WARN', $FilePath, $Message)
        }
    } elseif ($Status -eq 'FAIL') {
        $ws = Split-Path $Dir -Leaf
        $script:Failures.Add("$($script:CurrentLevel)|${ws}|${FilePath}|${Message}")
        if (-not $Json) {
            Write-Host ("  {0,-4} {1,-40} FAIL: {2}" -f '✗', $FilePath, $Message)
        }
        if ($FailFast) {
            Write-Error "FAIL-FAST: aborted at first FAIL"
            exit 1
        }
    }
}

function Test-EnGuidance {
    param([string]$File)
    if (-not (Test-Path $File)) { return $false }
    $content = Get-Content $File -Raw -ErrorAction SilentlyContinue
    if ($content -match '<!-- EN:') { return $true }

    $bil = Invoke-HgCheckBilingual -FilePath $File
    if ($bil -and $bil.Status -eq 'PASS') { return $true }

    if ($content -match '(?im)^#{1,6}\s+.+\s+/\s+.*(Shared|Environment|Registry|Security|Secure|Architecture|Documentation|Standards|Workflow|Maintenance|Notes|Description|Accessibility|For Apprentices|Spec[- ]Kit|Governance|Guidelines|Instructions|tooling)') {
        return $true
    }

    if (($content -match '(?i)(Gemeinsame|Barrierefreiheit|Sichere|Sicherheits|Umgebungsregister|Hinweise|Beschreibung|deutsch)') -and
        ($content -match '(?i)(Shared|Accessibility|Secure|Security|Environment|Notes|Description|English|englisch)')) {
        return $true
    }

    return $false
}

function Check-EnGuidance {
    param([string]$Dir, [string]$File)
    $full = Join-Path $Dir $File
    if (-not (Test-Path $full)) { return }
    if (Test-EnGuidance -File $full) {
        Emit-Result 'PASS' $File 'EN guidance present' $Dir
    } else {
        Emit-Result 'FAIL' $File 'EN guidance missing' $Dir
    }
}

function Check-ReadmeSections {
    param([string]$Dir)
    $full = Join-Path $Dir 'README.md'
    if (-not (Test-Path $full)) { return }
    $content = Get-Content $full -Raw -ErrorAction SilentlyContinue

    if ($content -match '(?im)^## .*Barrierefreiheit') {
        Emit-Result 'PASS' 'README.md' 'A11Y section' $Dir
    } else {
        Emit-Result 'FAIL' 'README.md' 'A11Y section missing' $Dir
    }
    if ($content -match '(?im)^## .*Spec-kit') {
        Emit-Result 'PASS' 'README.md' 'Spec-kit section' $Dir
    } else {
        Emit-Result 'FAIL' 'README.md' 'Spec-kit section missing' $Dir
    }
    if ($content -match '(?im)^## .*(Azubis|Auszubildende)') {
        Emit-Result 'PASS' 'README.md' 'Azubis section' $Dir
    } else {
        Emit-Result 'FAIL' 'README.md' 'Azubis section missing' $Dir
    }
}

function Check-AnsiInScripts {
    param([string]$Dir)
    $scriptsDir = Join-Path $Dir 'scripts'
    if (-not (Test-Path $scriptsDir)) { return }
    # ANSI escape scan: actual ESC byte, \033[, \e[ (PS1 uses .NET regex)
    # Exclude the scanner scripts themselves (they contain the patterns as literals in comments/code)
    $pattern = '\x1b\[|\\033\[|\\e\['
    $found = Get-ChildItem -Path $scriptsDir -File -Recurse -ErrorAction SilentlyContinue |
        Where-Object { $_.Name -notmatch '^check-homogeneity\.(ps1|sh)$' } |
        Where-Object { (Get-Content $_.FullName -Raw -ErrorAction SilentlyContinue) -match $pattern } |
        Select-Object -First 1
    if ($null -eq $found) {
        Emit-Result 'PASS' 'scripts/' 'no ANSI codes in scripts/' $Dir
    } else {
        Emit-Result 'FAIL' 'scripts/' "ANSI escape codes found: $($found.Name)" $Dir
    }
}

function Check-EditorconfigCsharp {
    param([string]$Dir)
    $slnFiles = @(Get-ChildItem -Path $Dir -Filter '*.sln' -Depth 0 -ErrorAction SilentlyContinue)
    if ($slnFiles.Count -gt 0) {
        if (Test-Path (Join-Path $Dir '.editorconfig')) {
            Emit-Result 'PASS' '.editorconfig' '.editorconfig present (C# project)' $Dir
        } else {
            Emit-Result 'FAIL' '.editorconfig' '.editorconfig missing (C# project)' $Dir
        }
    }
}

function Check-WorkflowYml {
    param([string]$Dir)
    $yml = Join-Path $Dir '.github/workflows/homogeneity-check.yml'
    if (Test-Path $yml) {
        Emit-Result 'PASS' '.github/workflows/homogeneity-check.yml' 'file present' $Dir
    } else {
        Emit-Result 'FAIL' '.github/workflows/homogeneity-check.yml' 'file missing' $Dir
    }
}

function Check-CopilotInstructions {
    param([string]$Dir)
    $f = Join-Path $Dir '.github/copilot-instructions.md'
    if (Test-Path $f) {
        Emit-Result 'PASS' '.github/copilot-instructions.md' 'file present' $Dir
    } else {
        Emit-Result 'FAIL' '.github/copilot-instructions.md' 'file missing' $Dir
    }
}

function Check-StatisticsProfile {
    param([string]$Dir)

    $ledger = Join-Path $Dir 'docs/project-statistics.md'
    $config = Join-Path $Dir 'docs/project-statistics.config.json'
    $renderer = Join-Path $Dir 'scripts/render-project-statistics.ps1'
    if (-not (Test-Path $ledger)) {
        return
    }
    if (-not (Test-Path $config)) {
        Emit-Result 'FAIL' 'docs/project-statistics.config.json' `
            'ASCII Statistics Profile 2 configuration missing' $Dir
        return
    }
    if (-not (Test-Path $renderer)) {
        Emit-Result 'FAIL' 'scripts/render-project-statistics.ps1' `
            'ASCII Statistics Profile 2 renderer missing' $Dir
        return
    }
    if (-not (Get-Command pwsh -ErrorAction SilentlyContinue)) {
        Emit-Result 'FAIL' 'pwsh' `
            'PowerShell 7 required by ASCII Statistics Profile 2 renderer' $Dir
        return
    }

    & pwsh -NoProfile -File $renderer -Repo $Dir -CheckOnly -Json *> $null
    switch ($LASTEXITCODE) {
        0 {
            Emit-Result 'PASS' 'docs/project-statistics.md' `
                'ASCII Statistics Profile 2 current' $Dir
        }
        1 {
            Emit-Result 'FAIL' 'docs/project-statistics.md' `
                'ASCII Statistics Profile 2 drift' $Dir
        }
        2 {
            Emit-Result 'FAIL' 'docs/project-statistics.md' `
                'ASCII Statistics Profile 2 validation or tooling error' $Dir
        }
        default {
            Emit-Result 'FAIL' 'docs/project-statistics.md' `
                "ASCII Statistics Profile 2 unexpected renderer exit $LASTEXITCODE" $Dir
        }
    }
}

function Check-AntigravityIntegration {
    param([string]$Dir)

    $agyManifest = Join-Path $Dir '.specify/integrations/agy.manifest.json'
    if (Test-Path $agyManifest) {
        Emit-Result 'PASS' '.specify/integrations/agy.manifest.json' 'Antigravity Spec-Kit integration present' $Dir
    } else {
        Emit-Result 'FAIL' '.specify/integrations/agy.manifest.json' 'Antigravity Spec-Kit integration missing' $Dir
    }

    $legacyManifest = Join-Path $Dir '.specify/integrations/gemini.manifest.json'
    $legacyCommands = Join-Path $Dir '.gemini/commands'
    $legacyGeminiPath = if (Test-Path $legacyManifest) {
        '.specify/integrations/gemini.manifest.json'
    } elseif (Test-Path $legacyCommands) {
        '.gemini/commands'
    } else {
        $null
    }
    if ($legacyGeminiPath) {
        Emit-Result 'FAIL' $legacyGeminiPath 'legacy Gemini Spec-Kit integration present' $Dir
    } else {
        Emit-Result 'PASS' '.gemini/commands' 'legacy Gemini Spec-Kit integration absent' $Dir
    }

    $skills = Get-ChildItem -Path (Join-Path $Dir '.agents/skills') -Directory -Filter 'speckit-*' -ErrorAction SilentlyContinue
    if ($skills) {
        Emit-Result 'PASS' '.agents/skills' 'Antigravity/Codex Spec-Kit skills present' $Dir
    } else {
        Emit-Result 'FAIL' '.agents/skills' 'Antigravity/Codex Spec-Kit skills missing' $Dir
    }

    $gitignore = Join-Path $Dir '.gitignore'
    $content = if (Test-Path $gitignore) { Get-Content $gitignore -Raw } else { '' }
    if ($content.Contains('.agents/*') -and
        $content.Contains('!.agents/skills/') -and
        $content.Contains('!.agents/skills/**')) {
        Emit-Result 'PASS' '.gitignore' 'surgical .agents skills allowlist' $Dir
    } else {
        Emit-Result 'FAIL' '.gitignore' 'surgical .agents skills allowlist missing' $Dir
    }
}

# ─── Header ──────────────────────────────────────────────────────────────────
if (-not $Json) {
    Write-Host "Workspace Homogeneity Guardian — check-homogeneity v1.0"
    Write-Host "Scan-Startpunkt: $TargetDir"
    Write-Host ('=' * 54)
    Write-Host ""
}

# ─── Main Scan ───────────────────────────────────────────────────────────────
$scanResults = Invoke-HgScan -TargetDir $TargetDir
$WorkspacesCount = 0
$ProjectsCount   = 0

foreach ($entry in $scanResults) {
    $dir   = $entry.Path
    $level = $entry.Level
    $script:CurrentLevel = $level

    $ScanDirs.Add($dir)
    $DirTotal[$dir] = 0
    $DirPass[$dir]  = 0

    if ($level -eq 1) { $WorkspacesCount++ }
    if ($level -eq 2) { $ProjectsCount++ }

    if (-not $Json) {
        Write-Host "[Level $level] ${dir}/"
    }

    # Required files
    foreach ($f in $RequiredFiles) {
        $full = Join-Path $dir $f
        if (Test-Path $full) {
            Emit-Result 'PASS' $f 'file present' $dir

            # Bilingual
            $bil = Invoke-HgCheckBilingual -FilePath $full
            if ($bil) { Emit-Result $bil.Status $f $bil.Message $dir }

            # A11Y
            Invoke-HgCheckA11y -FilePath $full | ForEach-Object {
                Emit-Result $_.Status $f $_.Message $dir
            }

            # Secrets
            Invoke-HgScanFileSecrets -FilePath $full | ForEach-Object {
                Emit-Result $_.Status $f $_.Message $dir
            }
        } else {
            Emit-Result 'FAIL' $f 'file missing' $dir
        }
    }
    Check-StatisticsProfile -Dir $dir

    # README.md content checks (A11Y, Spec-kit, Azubis sections)
    Check-ReadmeSections -Dir $dir

    # copilot-instructions.md presence (Level 0 and 1)
    if ($level -le 1) {
        Check-CopilotInstructions -Dir $dir
    }

    # EN guidance checks (Level 0 and 1 agent/governance files)
    if ($level -le 1) {
        foreach ($enFile in @('AGENTS.md','CLAUDE.md','GEMINI.md','constitution.md')) {
            Check-EnGuidance -Dir $dir -File $enFile
        }
        Check-EnGuidance -Dir $dir -File '.github/copilot-instructions.md'
    }

    # homogeneity-check.yml presence (all levels)
    Check-WorkflowYml -Dir $dir
    Check-AntigravityIntegration -Dir $dir

    # ANSI escape scan in scripts/ (Level 0 only)
    if ($level -eq 0) {
        Check-AnsiInScripts -Dir $dir
    }

    # Hook check
    if ($level -ge 1) {
        $hook = Invoke-HgCheckHook -Dir $dir
        if ($hook) { Emit-Result $hook.Status '.git/hooks/pre-push' $hook.Message $dir }
    }

    if ($level -eq 0) {
        $canonicalHook = Join-Path $dir 'scripts/hooks/pre-push'
        if (Test-Path $canonicalHook) {
            Emit-Result 'PASS' 'scripts/hooks/pre-push' 'canonical hook present' $dir
        } else {
            Emit-Result 'WARN' 'scripts/hooks/pre-push' 'canonical hook missing' $dir
        }
        $scriptReference = Join-Path $dir 'scripts/render-script-reference.ps1'
        if (Test-Path -LiteralPath $scriptReference -PathType Leaf) {
            & $scriptReference -Repo $dir -CheckOnly *> $null
            if ($LASTEXITCODE -eq 0) {
                Emit-Result 'PASS' 'docs/scripts/reference.md' `
                    'script catalog complete and current' $dir
            } else {
                Emit-Result 'FAIL' 'docs/scripts/reference.md' `
                    'script catalog incomplete or generated documentation drifted' $dir
            }
        }
    }

    # Level 0: Git Scope Isolation checks (GIT-SCOPE-001, GIT-SCOPE-002)
    if ($level -eq 0) {
        $homeDir2 = $(if ($env:HOME) { $env:HOME } else { $env:USERPROFILE })
        $gitconfigD2 = Join-Path $homeDir2 '.gitconfig.d'
        $gitconfig2  = Join-Path $homeDir2 '.gitconfig'
        if (-not (Test-Path $gitconfigD2)) {
            Emit-Result 'WARN' '~/.gitconfig.d/' `
                '~/.gitconfig.d/ fehlt — Scope-Isolierung nicht konfiguriert / missing — scope isolation not configured' `
                $dir
        } elseif (-not ((Get-Content $gitconfig2 -ErrorAction SilentlyContinue) |
                Select-String -SimpleMatch 'gitdir:~/home-baseline-source/' -Quiet)) {
            Emit-Result 'WARN' '~/.gitconfig' `
                'includeIf fuer home-baseline-source nicht gefunden / not found for home-baseline-source' `
                $dir
        }
    }

    # .editorconfig for C# Level-2 (FR-REV-E02)
    if ($level -eq 2) {
        Check-EditorconfigCsharp -Dir $dir
    }

    # Deps + speckit for projects
    if ($level -eq 2) {
        Invoke-HgCheckDeps -Dir $dir | ForEach-Object {
            Emit-Result $_.Status '*.csproj' $_.Message $dir
        }
        $specFile = Get-ChildItem -Path (Join-Path $dir 'specs') -Filter 'spec.md' -Recurse -Depth 3 -ErrorAction SilentlyContinue | Select-Object -First 1
        if ($specFile) {
            $sk = Invoke-HgCheckSpeckit -SpecFile $specFile.FullName
            if ($sk) { Emit-Result $sk.Status 'specs/spec.md' $sk.Message $dir }
        }
    }

    if (-not $Json) { Write-Host "" }
}

# ─── Summary ─────────────────────────────────────────────────────────────────
$OverallScore = if ($TotalChecks -gt 0) { [int](($TotalPass * 100) / $TotalChecks) } else { 0 }

if ($Json) {
    $L0Score = if ($L0Total -gt 0) { [int](($L0Pass * 100) / $L0Total) } else { 0 }
    $L1Score = if ($L1Total -gt 0) { [int](($L1Pass * 100) / $L1Total) } else { 0 }
    $L2Score = if ($L2Total -gt 0) { [int](($L2Pass * 100) / $L2Total) } else { 0 }

    $obj = [PSCustomObject]@{
        score      = $OverallScore
        by_level   = [PSCustomObject]@{ '0' = $L0Score; '1' = $L1Score; '2' = $L2Score }
        failures   = @($Failures | ForEach-Object {
            $parts = $_ -split '\|', 4
            [PSCustomObject]@{ file = $parts[2]; check = $parts[3]; level = [int]$parts[0]; workspace = $parts[1] }
        })
        warnings   = @($Warnings | ForEach-Object {
            $parts = $_ -split '\|', 4
            [PSCustomObject]@{ file = $parts[2]; check = $parts[3]; level = [int]$parts[0]; workspace = $parts[1] }
        })
    }
    $obj | ConvertTo-Json -Compress
} else {
    Write-Host ('=' * 54)
    Write-Host "COMPLIANCE SUMMARY"
    Write-Host ""

    foreach ($d in $ScanDirs) {
        $lt = $DirTotal[$d] ?? 0
        $lp = $DirPass[$d] ?? 0
        $ls = if ($lt -gt 0) { [int](($lp * 100) / $lt) } else { 0 }
        $filled = [int]($ls * 10 / 100)
        $bar = ('#' * $filled) + ('.' * (10 - $filled))
        $shortName = $d -replace [regex]::Escape($(if ($env:HOME) { $env:HOME } else { $env:USERPROFILE })), '~'
        Write-Host ("{0,-30} [{1}] {2,3} %  ({3}/{4} checks)" -f $shortName, $bar, $ls, $lp, $lt)
    }

    Write-Host ""
    Write-Host ("Overall: $OverallScore %  |  Workspaces: $WorkspacesCount  |  Projects: $ProjectsCount")

    $fc = $Failures.Count; $wc = $Warnings.Count
    Write-Host ""
    if ($fc -gt 0) {
        Write-Host "Exit code: 1 ($fc FAIL, $wc WARN)"
    } elseif ($wc -gt 0) {
        Write-Host "Exit code: 0 (0 FAIL, $wc WARN)"
    } else {
        Write-Host "Exit code: 0 (all checks passed)"
    }
}

# Post-scan writes are independent of the selected output format.
if (-not $DryRun) {
    $statsFile = Join-Path $HomeDir 'STATS.md'
    try {
        Invoke-HgWriteStats -StatsFile $statsFile -Score $OverallScore -Dirs ([string[]]$ScanDirs)
    } catch {
        Write-Error "FATAL: could not update ${statsFile}: $($_.Exception.Message)"
        exit 2
    }
    if (-not $Json) {
        Write-Host "STATS.md updated: $statsFile"
    }
}

# GitHub Step Summary
if ($env:GITHUB_STEP_SUMMARY) {
    $summary = @("## Homogeneity Check Report", "", "| Level | Datei | Check | Status |", "|---|---|---|---|")
    foreach ($f in $Failures) {
        $parts = $f -split '\|', 4
        $summary += "| $($parts[0]) | $($parts[2]) | $($parts[3]) | ✗ |"
    }
    foreach ($w in $Warnings) {
        $parts = $w -split '\|', 4
        $summary += "| $($parts[0]) | $($parts[2]) | $($parts[3]) | WARN |"
    }
    $summary += ""; $summary += "**Score: ${OverallScore}% (${TotalPass}/${TotalChecks} checks passed)**"
    $summary | Add-Content -Path $env:GITHUB_STEP_SUMMARY
}

# Exit code: 1 only on FAIL (not WARN) — per contract
if ($Failures.Count -gt 0) { exit 1 }
exit 0
