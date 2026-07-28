[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$PresetRoot = Split-Path -Parent (Split-Path -Parent $PSCommandPath)
$Validator = Join-Path $PresetRoot 'scripts/validate-intake-governance-config.py'
$Root = Join-Path ([System.IO.Path]::GetTempPath()) ("intake-governance-" + [guid]::NewGuid())
New-Item -ItemType Directory -Path $Root | Out-Null

function Write-JsonFixture {
    param([string]$Name, [hashtable]$Data)
    $Path = Join-Path $Root $Name
    $Data | ConvertTo-Json -Depth 16 | Set-Content -LiteralPath $Path -Encoding utf8NoBOM
    return $Path
}

function Get-NormalizedSha256 {
    param([string]$Path)
    $Text = [IO.File]::ReadAllText($Path, [Text.UTF8Encoding]::new($false, $true))
    $Text = $Text.Replace("`r`n", "`n").Replace("`r", "`n")
    $Bytes = [Text.UTF8Encoding]::new($false).GetBytes($Text)
    return [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($Bytes)).ToLowerInvariant()
}

function Invoke-Fixture {
    param([string]$Path, [int]$ExpectedExit, [string]$ExpectedText)
    $Output = & python3 $Validator --config $Path --repo $Root --json 2>&1
    $Exit = $LASTEXITCODE
    if ($Exit -ne $ExpectedExit) {
        throw "Expected exit $ExpectedExit, got ${Exit}: $Output"
    }
    if (($Output -join "`n") -notmatch [regex]::Escape($ExpectedText)) {
        throw "Expected '$ExpectedText': $Output"
    }
}

function New-BaseConfig {
    param(
        [string]$Language = 'de-DE',
        [string]$NamingProfile = 'de',
        [string]$Index = 'Pflichtenheft.md',
        [string]$Pattern = 'Lastenheft_<slug>.md',
        [string]$Order = 'Lastenheft_Abarbeitungsreihenfolge.md'
    )
    return @{
        schemaVersion = '2.0'
        documentationLanguage = $Language
        artifactNaming = @{
            profile = $NamingProfile
            canonicalIndex = $Index
            intakePattern = $Pattern
            orderView = $Order
        }
        roles = @{
            'requirements-index' = $Index
            'requirements-intake' = 'requirements/intakes/active'
            'intake-order' = $Order
            'requirements-baseline' = 'requirements/baseline'
        }
        collections = @{
            baseline = 'requirements/baseline'
            active = 'requirements/intakes/active'
            archive = 'requirements/intakes/archive'
            backlog = 'requirements/intakes/backlog'
            history = 'requirements/intakes/history'
            seriesManifest = 'requirements/intakes/series/manifest.json'
        }
        legacyArtifactNames = @()
    }
}

try {
    foreach ($Directory in @(
        'requirements/intakes/active',
        'requirements/intakes/archive',
        'requirements/intakes/backlog',
        'requirements/intakes/history',
        'requirements/intakes/series',
        'requirements/baseline'
    )) {
        New-Item -ItemType Directory -Path (Join-Path $Root $Directory) -Force | Out-Null
    }
    Set-Content -LiteralPath (Join-Path $Root 'Pflichtenheft.md') -Value '# Index' -Encoding utf8NoBOM
    Set-Content -LiteralPath (Join-Path $Root 'Lastenheft_Abarbeitungsreihenfolge.md') -Value '# Order' -Encoding utf8NoBOM
    $Target = Join-Path $Root 'requirements/intakes/active/Lastenheft_Beispiel.md'
    Set-Content -LiteralPath $Target -Value '# Beispiel' -Encoding utf8NoBOM
    $Manifest = @{
        schemaVersion = '1.0'
        documentType = 'IntakeSeriesManifest'
        orderedTargets = @(@{
            path = 'requirements/intakes/active/Lastenheft_Beispiel.md'
            role = 'Primary'
            normalizedSha256 = Get-NormalizedSha256 $Target
            status = 'Eligible'
        })
        roots = @('requirements/intakes/active/Lastenheft_Beispiel.md')
        dependencies = @()
    }
    $Manifest | ConvertTo-Json -Depth 12 |
        Set-Content -LiteralPath (Join-Path $Root 'requirements/intakes/series/manifest.json') -Encoding utf8NoBOM

    $Base = New-BaseConfig
    Invoke-Fixture (Write-JsonFixture 'de.json' $Base) 0 '"outcome": "Aligned"'

    $NestedRepository = Join-Path $Root 'nested-repository'
    New-Item -ItemType Directory -Path (Join-Path $NestedRepository '.git') -Force |
        Out-Null
    Set-Content -LiteralPath (Join-Path $NestedRepository 'Pflichtenheft.md') `
        -Value '# Nested repository index' -Encoding utf8NoBOM
    Invoke-Fixture (Write-JsonFixture 'nested-repository.json' $Base) 0 '"outcome": "Aligned"'

    $NestedDirectory = Join-Path $Root 'ordinary-directory'
    New-Item -ItemType Directory -Path $NestedDirectory -Force | Out-Null
    Set-Content -LiteralPath (Join-Path $NestedDirectory 'Pflichtenheft.md') `
        -Value '# Invalid duplicate index' -Encoding utf8NoBOM
    Invoke-Fixture (Write-JsonFixture 'nested-duplicate.json' $Base) 2 'RIG013'
    Remove-Item -LiteralPath $NestedDirectory -Recurse -Force

    $Bilingual = New-BaseConfig -Language 'de-DE' -NamingProfile 'de'
    Invoke-Fixture (Write-JsonFixture 'bilingual.json' $Bilingual) 0 '"documentationLanguage": "de-DE"'

    $Explicit = New-BaseConfig -Language 'de-DE' -NamingProfile 'explicit'
    Invoke-Fixture (Write-JsonFixture 'explicit.json' $Explicit) 0 '"profile": "explicit"'

    $HistoricalInFlatLayout = Join-Path $Root 'requirements/intakes/active/Lastenheft_Historisch.md'
    Set-Content -LiteralPath $HistoricalInFlatLayout -Value '# Historisch' -Encoding utf8NoBOM
    Invoke-Fixture (Write-JsonFixture 'directory-strict.json' $Base) 2 'RIG013'
    $ManifestInventory = $Base.Clone()
    $ManifestInventory.inventoryMode = 'SeriesManifest'
    Invoke-Fixture (Write-JsonFixture 'series-manifest-inventory.json' $ManifestInventory) 0 '"inventoryMode": "SeriesManifest"'
    Remove-Item -LiteralPath $HistoricalInFlatLayout

    $ManifestPath = Join-Path $Root 'requirements/intakes/series/manifest.json'
    $SavedManifest = Get-Content -Raw -LiteralPath $ManifestPath
    $IdleManifest = @{
        schemaVersion = '1.0'
        documentType = 'IntakeSeriesManifest'
        status = 'Idle'
        orderedTargets = @()
        roots = @()
        dependencies = @()
    }
    $IdleManifest | ConvertTo-Json -Depth 12 |
        Set-Content -LiteralPath $ManifestPath -Encoding utf8NoBOM
    Invoke-Fixture (Write-JsonFixture 'idle-series.json' $ManifestInventory) 0 '"eligibleCandidate": "N/A"'

    $InvalidIdleManifest = $IdleManifest.Clone()
    $InvalidIdleManifest.orderedTargets = @($Manifest.orderedTargets[0])
    $InvalidIdleManifest.roots = @('requirements/intakes/active/Lastenheft_Beispiel.md')
    $InvalidIdleManifest | ConvertTo-Json -Depth 12 |
        Set-Content -LiteralPath $ManifestPath -Encoding utf8NoBOM
    Invoke-Fixture (Write-JsonFixture 'idle-with-target.json' $ManifestInventory) 2 'RIG017'

    $InvalidEmptyManifest = $IdleManifest.Clone()
    $InvalidEmptyManifest.status = 'Ready'
    $InvalidEmptyManifest | ConvertTo-Json -Depth 12 |
        Set-Content -LiteralPath $ManifestPath -Encoding utf8NoBOM
    Invoke-Fixture (Write-JsonFixture 'ready-without-target.json' $ManifestInventory) 2 'RIG014'
    Set-Content -LiteralPath $ManifestPath -Value $SavedManifest -Encoding utf8NoBOM

    $Schema1 = $Base.Clone()
    $Schema1.schemaVersion = '1.0'
    Invoke-Fixture (Write-JsonFixture 'schema1.json' $Schema1) 1 'MigrationRequired'

    $Ambiguous = $Base.Clone()
    $Ambiguous.documentationLanguage = 'und'
    Invoke-Fixture (Write-JsonFixture 'ambiguous.json' $Ambiguous) 1 'NeedsClarification'

    $BadProfile = $Base.Clone()
    $BadProfile.artifactNaming = $Base.artifactNaming.Clone()
    $BadProfile.artifactNaming.profile = 'fr'
    Invoke-Fixture (Write-JsonFixture 'profile.json' $BadProfile) 2 'RIG005'

    $Traversal = $Base.Clone()
    $Traversal.roles = $Base.roles.Clone()
    $Traversal.roles.'requirements-index' = '../outside.md'
    Invoke-Fixture (Write-JsonFixture 'traversal.json' $Traversal) 2 'RIG004'

    $Duplicate = $Base.Clone()
    $Duplicate.collections = $Base.collections.Clone()
    $Duplicate.collections.archive = $Duplicate.collections.active
    Invoke-Fixture (Write-JsonFixture 'duplicate.json' $Duplicate) 2 'RIG007'

    Set-Content -LiteralPath $ManifestPath -Value '{}' -Encoding utf8NoBOM
    Invoke-Fixture (Write-JsonFixture 'empty-manifest.json' $Base) 2 'RIG014'
    Set-Content -LiteralPath $ManifestPath -Value $SavedManifest -Encoding utf8NoBOM

    $BadHashManifest = $Manifest.Clone()
    $BadHashManifest.orderedTargets = @($Manifest.orderedTargets[0].Clone())
    $BadHashManifest.orderedTargets[0].normalizedSha256 = '0' * 64
    $BadHashManifest | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $ManifestPath -Encoding utf8NoBOM
    Invoke-Fixture (Write-JsonFixture 'hash-drift.json' $Base) 2 'RIG015'

    $MultipleEligible = $Manifest.Clone()
    $MultipleEligible.orderedTargets = @(
        $Manifest.orderedTargets[0],
        @{
            path = 'requirements/intakes/active/Lastenheft_Zweites.md'
            role = 'OrderedMember'
            normalizedSha256 = '0' * 64
            status = 'Eligible'
        }
    )
    $Second = Join-Path $Root 'requirements/intakes/active/Lastenheft_Zweites.md'
    Set-Content -LiteralPath $Second -Value '# Zweites' -Encoding utf8NoBOM
    $MultipleEligible.orderedTargets[1].normalizedSha256 = Get-NormalizedSha256 $Second
    $MultipleEligible | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $ManifestPath -Encoding utf8NoBOM
    Invoke-Fixture (Write-JsonFixture 'multiple-eligible.json' $Base) 2 'RIG017'

    Write-Output 'PASS: requirements intake governance fixtures'
}
finally {
    Remove-Item -LiteralPath $Root -Recurse -Force -ErrorAction SilentlyContinue
}
