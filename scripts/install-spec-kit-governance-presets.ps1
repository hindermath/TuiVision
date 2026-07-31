<#
.SYNOPSIS
Installiert die zentral konfigurierten GitHub-Spec-Kit-Governance-Presets.

Installs the centrally configured GitHub Spec Kit governance presets.

.DESCRIPTION
Liest die Preset-Matrix aus scripts/config/spec-kit-governance-presets.json und
installiert oder prueft die dort konfigurierten Preset-Versionen in einem oder
mehreren Level-0-, Level-1- oder Level-2-Spec-Kit-Repositories. Die Matrix ist
die einzige Stelle, an der Preset-Versionen
und Prioritaeten gepflegt werden. Nutze -WhatIf fuer eine Vorschau und -Force,
um vorhandene Presets aus der aktuellen Matrix neu zu installieren.

Reads the preset matrix from scripts/config/spec-kit-governance-presets.json and
installs or checks the configured preset versions in one or more level-0,
level-1, or level-2 Spec Kit repositories.
The matrix is the only place where preset versions and priorities are maintained.
Use -WhatIf to preview actions and -Force to reinstall existing presets from the
current matrix.

.PARAMETER Repo
Ziel-Repository. Kann mehrfach uebergeben werden. Standard ist das aktuelle
Verzeichnis.

Target repository. Can be passed multiple times. Defaults to the current
directory.

.PARAMETER PresetConfig
Pfad zur Preset-Matrix als JSON.

Path to the preset matrix JSON.

.PARAMETER Force
Vorhandene Presets zuerst entfernen und danach die konfigurierten Versionen
installieren.

Remove existing presets first, then install the configured versions.

.PARAMETER CheckOnly
Prueft IDs, Versionen, Prioritaeten, Aktivstatus und Preset-Verzeichnisse
exakt gegen die Matrix, ohne Dateien zu schreiben.

Validates IDs, versions, priorities, enabled state, and preset directories
exactly against the matrix without writing files.

.EXAMPLE
pwsh -NoProfile -File scripts/install-spec-kit-governance-presets.ps1 -WhatIf

.EXAMPLE
pwsh -NoProfile -File scripts/install-spec-kit-governance-presets.ps1 -CheckOnly

.EXAMPLE
pwsh -NoProfile -File scripts/install-spec-kit-governance-presets.ps1 -Repo ~/SecureCaseTrackerProjects/SecureCaseTracker-CSharp -Force
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [string[]]$Repo = @($PWD.Path),
    [string]$PresetConfig = '',
    [switch]$Force,
    [switch]$CheckOnly
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
if (-not $PresetConfig) {
    $PresetConfig = Join-Path $ScriptDir 'config/spec-kit-governance-presets.json'
}
$LegacyPresetConfig = Join-Path $ScriptDir 'config/spec-kit-autonomous-governance-presets.json'

function Resolve-HBPath {
    param([string]$Path)
    if ($Path -like '~/*') {
        return (Join-Path $HOME $Path.Substring(2))
    }
    return $Path
}

function Test-PresetInstalled {
    param(
        [string]$Repository,
        [string]$PresetId
    )

    Push-Location $Repository
    try {
        $listOutput = (specify preset list 2>$null | Out-String)
        return $listOutput -match "\($([regex]::Escape($PresetId))\)"
    } finally {
        Pop-Location
    }
}

function Invoke-PresetCommand {
    param(
        [string]$Repository,
        [string[]]$Arguments,
        [string]$Description
    )

    if ($PSCmdlet.ShouldProcess($Repository, $Description)) {
        Push-Location $Repository
        try {
            & specify @Arguments
            if ($LASTEXITCODE -ne 0) {
                throw "specify $($Arguments -join ' ') failed in ${Repository}"
            }
        } finally {
            Pop-Location
        }
    }
}

function Normalize-PresetMarkdown {
    param([string]$Repository)

    $presetDir = Join-Path $Repository '.specify/presets'
    if (-not (Test-Path $presetDir)) {
        return $false
    }

    $changed = $false
    $markdownFiles = Get-ChildItem $presetDir -Recurse -File -Filter '*.md' |
        Where-Object { $_.FullName -notmatch '[/\\]\.cache[/\\]' }

    foreach ($file in $markdownFiles) {
        $text = [System.IO.File]::ReadAllText($file.FullName)
        $normalized = [regex]::Replace($text, '[ \t]+(?=\r?\n)', '')
        $normalized = [regex]::Replace($normalized, '[ \t]+\z', '')
        if ($normalized -ne $text) {
            if ($PSCmdlet.ShouldProcess($file.FullName, 'normalize preset markdown whitespace')) {
                $utf8NoBom = [System.Text.UTF8Encoding]::new($false)
                [System.IO.File]::WriteAllText($file.FullName, $normalized, $utf8NoBom)
            }
            $changed = $true
        }
    }

    if ($changed) {
        Write-Host '  normalisiert: Markdown-Whitespace in .specify/presets'
    }
    return $changed
}

function Test-PresetMatrix {
    param([string]$Path)

    $data = Get-Content $Path -Raw | ConvertFrom-Json
    if ([int]$data.schemaVersion -ne 1) {
        throw "unsupported schemaVersion in ${Path}"
    }

    $presets = @($data.presets)
    if ($presets.Count -eq 0) {
        throw "presets must be a non-empty list in ${Path}"
    }

    $ids = [System.Collections.Generic.HashSet[string]]::new()
    $priorities = [System.Collections.Generic.HashSet[int]]::new()
    foreach ($preset in $presets) {
        foreach ($property in @('id', 'version', 'priority', 'repository', 'archiveUrl')) {
            if ($null -eq $preset.$property -or [string]::IsNullOrWhiteSpace([string]$preset.$property)) {
                throw "preset is missing ${property} in ${Path}"
            }
        }
        if (-not $ids.Add([string]$preset.id)) {
            throw "duplicate preset id $($preset.id) in ${Path}"
        }
        if (-not $priorities.Add([int]$preset.priority)) {
            throw "duplicate priority $($preset.priority) in ${Path}"
        }
    }

    return $data
}

function Test-RepositoryPresetMatrix {
    param(
        [string]$Repository,
        [object]$Matrix
    )

    $registryPath = Join-Path $Repository '.specify/presets/.registry'
    if (-not (Test-Path $registryPath -PathType Leaf)) {
        throw "Preset-Registry fehlt / preset registry missing: ${registryPath}"
    }

    $registry = Get-Content $registryPath -Raw | ConvertFrom-Json
    $actualProperties = @($registry.presets.PSObject.Properties)
    $expectedItems = @($Matrix.presets)
    $actualIds = @($actualProperties.Name | Sort-Object)
    $expectedIds = @($expectedItems.id | Sort-Object)
    if (($actualIds -join "`n") -ne ($expectedIds -join "`n")) {
        throw "Preset-IDs entsprechen nicht exakt der Matrix / preset IDs do not exactly match the matrix: ${Repository}"
    }

    foreach ($preset in $expectedItems) {
        $id = [string]$preset.id
        $actual = $registry.presets.$id
        $expectedVersion = ([string]$preset.version).TrimStart('v')
        $actualVersion = ([string]$actual.version).TrimStart('v')
        if ($actualVersion -ne $expectedVersion) {
            throw "${id}: Version ${actualVersion} != ${expectedVersion}"
        }
        if ([int]$actual.priority -ne [int]$preset.priority) {
            throw "${id}: Prioritaet / priority $($actual.priority) != $($preset.priority)"
        }
        if ($actual.enabled -ne $true) {
            throw "${id}: nicht aktiv / not enabled"
        }
        $manifestPath = Join-Path $Repository ".specify/presets/${id}/preset.yml"
        if (-not (Test-Path $manifestPath -PathType Leaf)) {
            throw "${id}: preset.yml fehlt / missing"
        }
    }

    Write-Host "  OK: $($expectedItems.Count) Presets entsprechen exakt der Matrix / presets exactly match the matrix"
}

if ($CheckOnly -and ($Force -or $WhatIfPreference)) {
    throw '-CheckOnly ist nicht mit -Force oder -WhatIf kombinierbar / cannot be combined'
}

if (-not (Get-Command specify -ErrorAction SilentlyContinue)) {
    throw 'specify CLI nicht gefunden / specify CLI not found'
}

$PresetConfig = Resolve-HBPath $PresetConfig
if (-not (Test-Path $PresetConfig)) {
    throw "Preset-Konfiguration nicht gefunden / preset config not found: ${PresetConfig}"
}

$matrix = Test-PresetMatrix -Path $PresetConfig
$defaultPresetConfig = Join-Path $ScriptDir 'config/spec-kit-governance-presets.json'
if (
    ([System.IO.Path]::GetFullPath($PresetConfig) -eq [System.IO.Path]::GetFullPath($defaultPresetConfig)) -and
    (Test-Path $LegacyPresetConfig)
) {
    $legacyMatrix = Test-PresetMatrix -Path $LegacyPresetConfig
    $canonicalJson = @($matrix.presets) | ConvertTo-Json -Depth 8 -Compress
    $legacyJson = @($legacyMatrix.presets) | ConvertTo-Json -Depth 8 -Compress
    if ($canonicalJson -ne $legacyJson) {
        throw 'deprecated autonomous preset matrix differs from the canonical matrix'
    }
}
foreach ($repoItem in $Repo) {
    $repository = Resolve-HBPath $repoItem
    if (-not (Test-Path (Join-Path $repository '.git'))) {
        throw "kein Git-Repository / not a Git repository: ${repository}"
    }
    if (-not (Test-Path (Join-Path $repository '.specify'))) {
        throw "Spec Kit ist nicht initialisiert / Spec Kit is not initialized: ${repository}"
    }

    Write-Host "## $repository"
    if ($CheckOnly) {
        Test-RepositoryPresetMatrix -Repository $repository -Matrix $matrix
        continue
    }

    $changed = $false
    foreach ($preset in $matrix.presets) {
        $id = [string]$preset.id
        $version = [string]$preset.version
        $priority = [string]$preset.priority
        $archiveUrl = [string]$preset.archiveUrl
        $installed = Test-PresetInstalled -Repository $repository -PresetId $id

        if ($Force -and $installed) {
            Invoke-PresetCommand -Repository $repository -Arguments @('preset', 'remove', $id) -Description "remove $id"
            $changed = $true
            $installed = $false
        }

        if ($installed) {
            Write-Host "  vorhanden: $id"
            continue
        }

        Invoke-PresetCommand -Repository $repository -Arguments @('preset', 'add', '--from', $archiveUrl, '--priority', $priority) -Description "install $id $version"
        $changed = $true
    }

    if (Normalize-PresetMarkdown -Repository $repository) {
        $changed = $true
    }

    if (-not $changed) {
        Write-Host '  unveraendert: alle konfigurierten Presets vorhanden'
    }
}
