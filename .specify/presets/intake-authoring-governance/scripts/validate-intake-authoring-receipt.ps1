<#
.SYNOPSIS
Prueft ein Intake-Authoring-Receipt, sein Ziel und lokale Quellen read-only.

Validates an intake-authoring receipt, its target, and local sources read-only.
.DESCRIPTION
Prueft Schema 1.0 und 1.1, normalisierte SHA-256-Werte, striktes UTF-8,
Entscheidungen, Prompt-Zustand, Delivery Authority und Update-Provenienz.
Die Pruefung veraendert keine Datei und erteilt keine weitere Berechtigung.

Validates schemas 1.0 and 1.1, normalized SHA-256 values, strict UTF-8,
decisions, prompt state, delivery authority, and update provenance. It changes
no file and grants no additional authority.
.PARAMETER Receipt
Pfad zum JSON-Receipt. Path to the JSON receipt.
.PARAMETER Repo
Repository-Wurzel fuer relative Pfade. Repository root for relative paths.
.EXAMPLE
pwsh -NoProfile -File scripts/validate-intake-authoring-receipt.ps1 -Receipt specs/intake-authoring-receipts/example.json -Repo .
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$Receipt,
    [string]$Repo = '.'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Test-IntakeAuthoringReceipt {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$Receipt,
        [Parameter(Mandatory)][string]$Repo
    )

    $Errors = [System.Collections.Generic.List[string]]::new()
    $RepoRoot = [IO.Path]::GetFullPath($Repo)

    function Get-HBProperty([object]$Object, [string]$Name) {
        if ($null -eq $Object) { return $null }
        $Property = $Object.PSObject.Properties[$Name]
        if ($null -eq $Property) { return $null }
        return $Property.Value
    }

    function Get-HBText([object]$Object, [string]$Name, [string]$Label) {
        $Value = Get-HBProperty $Object $Name
        if ($Value -is [DateTime]) {
            return $Value.ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ss.FFFFFFFZ', [Globalization.CultureInfo]::InvariantCulture)
        }
        if ($Value -is [DateTimeOffset]) {
            return $Value.UtcDateTime.ToString('yyyy-MM-ddTHH:mm:ss.FFFFFFFZ', [Globalization.CultureInfo]::InvariantCulture)
        }
        if ($Value -isnot [string] -or [string]::IsNullOrWhiteSpace($Value)) {
            $Errors.Add("${Label}.${Name} must be a non-empty string")
            return ''
        }
        return $Value.Trim()
    }

    function Test-HBRelativePath([string]$Value) {
        if ([IO.Path]::IsPathRooted($Value)) { return $false }
        return -not ($Value -split '[\\/]' | Where-Object { $_ -eq '..' })
    }

    function Get-HBNormalizedText([string]$Path) {
        $Bytes = [IO.File]::ReadAllBytes($Path)
        $Offset = if ($Bytes.Length -ge 3 -and $Bytes[0] -eq 0xEF -and $Bytes[1] -eq 0xBB -and $Bytes[2] -eq 0xBF) { 3 } else { 0 }
        $Utf8 = [Text.UTF8Encoding]::new($false, $true)
        $Text = $Utf8.GetString($Bytes, $Offset, $Bytes.Length - $Offset)
        if ($Text.Contains([char]0)) { throw 'binary NUL detected' }
        return $Text.Replace("`r`n", "`n").Replace("`r", "`n")
    }

    function Get-HBNormalizedHash([string]$Path) {
        $Utf8 = [Text.UTF8Encoding]::new($false, $true)
        $Bytes = $Utf8.GetBytes((Get-HBNormalizedText $Path))
        return [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($Bytes)).ToLowerInvariant()
    }

    function Get-HBNormalizedHashFromBytes([byte[]]$Bytes) {
        $Offset = if ($Bytes.Length -ge 3 -and $Bytes[0] -eq 0xEF -and $Bytes[1] -eq 0xBB -and $Bytes[2] -eq 0xBF) { 3 } else { 0 }
        $Utf8Strict = [Text.UTF8Encoding]::new($false, $true)
        $Text = $Utf8Strict.GetString($Bytes, $Offset, $Bytes.Length - $Offset)
        if ($Text.Contains([char]0)) { throw 'binary NUL detected' }
        $Normalized = $Text.Replace("`r`n", "`n").Replace("`r", "`n")
        $Utf8 = [Text.UTF8Encoding]::new($false, $true)
        return [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($Utf8.GetBytes($Normalized))).ToLowerInvariant()
    }

    function Get-HBGitBlobNormalizedHash([string]$ObjectId) {
        $StartInfo = [Diagnostics.ProcessStartInfo]::new()
        $StartInfo.FileName = 'git'
        $StartInfo.UseShellExecute = $false
        $StartInfo.RedirectStandardOutput = $true
        $StartInfo.RedirectStandardError = $true
        foreach ($Argument in @('-C', $RepoRoot, 'cat-file', 'blob', $ObjectId)) {
            [void]$StartInfo.ArgumentList.Add($Argument)
        }
        $Process = [Diagnostics.Process]::new()
        $Process.StartInfo = $StartInfo
        [void]$Process.Start()
        $Buffer = [IO.MemoryStream]::new()
        try {
            $Process.StandardOutput.BaseStream.CopyTo($Buffer)
            $ErrorText = $Process.StandardError.ReadToEnd()
            $Process.WaitForExit()
            if ($Process.ExitCode -ne 0) {
                throw "git cat-file failed: $ErrorText"
            }
            return Get-HBNormalizedHashFromBytes $Buffer.ToArray()
        } finally {
            $Buffer.Dispose()
            $Process.Dispose()
        }
    }

    function Test-HBDigest([string]$Value, [string]$Label) {
        if ($Value -notmatch '^[0-9a-f]{64}$') {
            $Errors.Add("${Label} must be a lowercase SHA-256")
        }
    }

    function Test-HBSecret([string]$Text) {
        $Patterns = @(
            '-----BEGIN (?:RSA |EC |OPENSSH )?PRIVATE KEY-----',
            '\bAKIA[0-9A-Z]{16}\b',
            '\bgh[pousr]_[A-Za-z0-9]{20,}\b',
            '\bgithub_pat_[A-Za-z0-9_]{20,}\b'
        )
        foreach ($Pattern in $Patterns) {
            if ($Text -match $Pattern) { return $true }
        }
        return $false
    }

    if (-not (Test-Path -LiteralPath $Receipt -PathType Leaf)) {
        throw "Receipt file not found: $Receipt"
    }
    try {
        $ReceiptText = Get-HBNormalizedText $Receipt
        $Data = $ReceiptText | ConvertFrom-Json
    } catch {
        throw "Invalid receipt: $($_.Exception.Message)"
    }
    if (Test-HBSecret $ReceiptText) {
        $Errors.Add('receipt contains a credential or private key pattern')
    }

    $SchemaVersion = Get-HBProperty $Data 'schemaVersion'
    if ($SchemaVersion -notin @('1.0', '1.1')) {
        $Errors.Add('schemaVersion must be 1.0 or 1.1')
    }
    $ReceiptId = Get-HBText $Data 'receiptId' 'receipt'
    $Guid = [Guid]::Empty
    if (-not [Guid]::TryParse($ReceiptId, [ref]$Guid) -or $Guid -eq [Guid]::Empty) {
        $Errors.Add('receiptId must be a non-zero UUID')
    }

    $Generator = Get-HBProperty $Data 'generator'
    if ($null -eq $Generator) {
        $Errors.Add('generator must be an object')
        $Generator = [pscustomobject]@{}
    }
    if ((Get-HBText $Generator 'preset' 'generator') -ne 'intake-authoring-governance') {
        $Errors.Add('generator.preset is invalid')
    }
    $GeneratorVersion = Get-HBText $Generator 'version' 'generator'
    $ExpectedGenerator = if ($SchemaVersion -eq '1.0') { '0.1.0' } elseif ($SchemaVersion -eq '1.1') { '0.1.1' } else { '' }
    if ($ExpectedGenerator -and $GeneratorVersion -ne $ExpectedGenerator) {
        $Errors.Add("generator.version must be $ExpectedGenerator for schema $SchemaVersion")
    }

    $CreatedAt = Get-HBText $Data 'createdAt' 'receipt'
    $Timestamp = [DateTimeOffset]::MinValue
    if (-not $CreatedAt.EndsWith('Z') -or -not [DateTimeOffset]::TryParse($CreatedAt, [ref]$Timestamp)) {
        $Errors.Add('createdAt must be an ISO-8601 UTC timestamp')
    }

    $Status = Get-HBText $Data 'status' 'receipt'
    if ($Status -notin @('ReadyForReview', 'NeedsClarification')) {
        $Errors.Add('status is invalid')
    }

    $Target = Get-HBProperty $Data 'target'
    if ($null -eq $Target) {
        $Errors.Add('target must be an object')
        $Target = [pscustomobject]@{}
    }
    $TargetPathText = Get-HBText $Target 'path' 'target'
    $TargetHash = Get-HBText $Target 'normalizedSha256' 'target'
    Test-HBDigest $TargetHash 'target.normalizedSha256'
    if ($TargetPathText -and -not (Test-HBRelativePath $TargetPathText)) {
        $Errors.Add('target.path must be repository-relative')
    }
    $TargetPath = Join-Path $RepoRoot $TargetPathText
    $TargetText = ''
    if ($TargetPathText -and -not (Test-Path -LiteralPath $TargetPath -PathType Leaf)) {
        $Errors.Add("target missing: $TargetPathText")
    } elseif ($TargetPathText) {
        try {
            $Actual = Get-HBNormalizedHash $TargetPath
            $TargetText = Get-HBNormalizedText $TargetPath
        } catch {
            $Errors.Add("target ${TargetPathText}: $($_.Exception.Message)")
            $Actual = ''
        }
        if ($Actual -and $Actual -ne $TargetHash) {
            $Errors.Add("target hash drift: $TargetPathText")
        }
        if ($TargetText -and (Test-HBSecret $TargetText)) {
            $Errors.Add('target contains a credential or private key pattern')
        }
    }

    $Sources = @(Get-HBProperty $Data 'sources')
    if ($Sources.Count -eq 0) { $Errors.Add('sources must be a non-empty array') }
    $BlockedExtensions = @('.7z', '.doc', '.docx', '.gif', '.gz', '.jpeg', '.jpg', '.pdf', '.png', '.tar', '.webp', '.xls', '.xlsx', '.zip')
    for ($Index = 0; $Index -lt $Sources.Count; $Index++) {
        $Source = $Sources[$Index]
        $Label = "sources[$Index]"
        if ((Get-HBProperty $Source 'order') -ne ($Index + 1)) {
            $Errors.Add("${Label}.order must be $($Index + 1)")
        }
        $Kind = Get-HBText $Source 'kind' $Label
        if ($Kind -notin @('Inline', 'Pasted', 'File')) { $Errors.Add("${Label}.kind is invalid") }
        [void](Get-HBText $Source 'label' $Label)
        $Location = Get-HBText $Source 'location' $Label
        if ($Location -notin @('Repository', 'SnapshotOnly', 'ExternalSnapshot')) {
            $Errors.Add("${Label}.location is invalid")
        }
        $PathText = Get-HBText $Source 'path' $Label
        $SourceHash = Get-HBText $Source 'normalizedSha256' $Label
        Test-HBDigest $SourceHash "${Label}.normalizedSha256"
        [void](Get-HBText $Source 'gitBlob' $Label)
        if ($Location -eq 'Repository') {
            if ($Kind -ne 'File') { $Errors.Add("${Label}: Repository location requires File kind") }
            if ($PathText -eq 'N/A' -or -not (Test-HBRelativePath $PathText)) {
                $Errors.Add("${Label}.path must be repository-relative")
                continue
            }
            if ($PathText -eq $TargetPathText) {
                $Errors.Add("${Label}.path cannot be the generated target")
            }
            $SourcePath = Join-Path $RepoRoot $PathText
            if ([IO.Path]::GetExtension($SourcePath).ToLowerInvariant() -in $BlockedExtensions) {
                $Errors.Add("${Label}.path uses a known binary/document extension")
            }
            if (-not (Test-Path -LiteralPath $SourcePath -PathType Leaf)) {
                $Errors.Add("source missing: $PathText")
            } else {
                try {
                    $Actual = Get-HBNormalizedHash $SourcePath
                    $SourceText = Get-HBNormalizedText $SourcePath
                } catch {
                    $Errors.Add("source ${PathText}: $($_.Exception.Message)")
                    $Actual = ''
                    $SourceText = ''
                }
                if ($Actual -and $Actual -ne $SourceHash) {
                    $Errors.Add("source hash drift: $PathText")
                }
                if ($SourceText -and (Test-HBSecret $SourceText)) {
                    $Errors.Add("source contains a credential or private key pattern: $PathText")
                }
            }
        } else {
            if ($PathText -ne 'N/A') { $Errors.Add("${Label}.path must be N/A for snapshot sources") }
            if ($Kind -eq 'File' -and $Location -ne 'ExternalSnapshot') {
                $Errors.Add("${Label}: external File must use ExternalSnapshot")
            }
            if ($Kind -in @('Inline', 'Pasted') -and $Location -ne 'SnapshotOnly') {
                $Errors.Add("${Label}: inline or pasted source must use SnapshotOnly")
            }
        }
    }

    [void](Get-HBText $Data 'profile' 'receipt')
    [void](Get-HBText $Data 'languagePolicy' 'receipt')

    $Decisions = @(Get-HBProperty $Data 'decisions')
    $DecisionIds = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
    $OpenFromDecisions = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
    for ($Index = 0; $Index -lt $Decisions.Count; $Index++) {
        $Decision = $Decisions[$Index]
        $Label = "decisions[$Index]"
        $DecisionId = Get-HBText $Decision 'id' $Label
        if ($DecisionId -notmatch '^IAD[0-9]{3,}$' -or -not $DecisionIds.Add($DecisionId)) {
            $Errors.Add("${Label}.id must be a unique IAD### identifier")
        }
        $DecisionStatus = Get-HBText $Decision 'status' $Label
        if ($DecisionStatus -notin @('Answered', 'Open')) { $Errors.Add("${Label}.status is invalid") }
        [void](Get-HBText $Decision 'question' $Label)
        [void](Get-HBText $Decision 'evidence' $Label)
        $Answer = Get-HBProperty $Decision 'answer'
        if ($DecisionStatus -eq 'Answered' -and ($Answer -isnot [string] -or [string]::IsNullOrWhiteSpace($Answer))) {
            $Errors.Add("${Label}.answer is required for Answered")
        }
        if ($DecisionStatus -eq 'Open') { [void]$OpenFromDecisions.Add($DecisionId) }
    }

    $OpenIds = @(Get-HBProperty $Data 'openDecisionIds')
    $OpenUnique = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
    foreach ($OpenId in $OpenIds) {
        if ($OpenId -isnot [string]) { $Errors.Add('openDecisionIds must be a string array'); continue }
        if (-not $OpenUnique.Add($OpenId)) { $Errors.Add('openDecisionIds must be unique') }
    }
    if ($OpenUnique.Count -ne $OpenFromDecisions.Count -or @($OpenUnique | Where-Object { -not $OpenFromDecisions.Contains($_) }).Count) {
        $Errors.Add('openDecisionIds must match Open decisions exactly')
    }

    $QuestionCount = Get-HBProperty $Data 'questionCount'
    $QuestionCountValue = [long]::MinValue
    if ($QuestionCount -is [bool] -or
        -not [long]::TryParse([string]$QuestionCount, [ref]$QuestionCountValue) -or
        $QuestionCountValue -lt 0 -or $QuestionCountValue -gt 5) {
        $Errors.Add('questionCount must be an integer from 0 through 5')
    }

    $Surface = Get-HBProperty $Data 'agentSurface'
    if ($null -eq $Surface) {
        $Errors.Add('agentSurface must be an object')
        $Surface = [pscustomobject]@{}
    }
    if ((Get-HBText $Surface 'specifyCanonicalId' 'agentSurface') -ne 'speckit.specify') {
        $Errors.Add('agentSurface.specifyCanonicalId is invalid')
    }
    if ((Get-HBText $Surface 'autonomousCanonicalId' 'agentSurface') -ne 'speckit.autonomous') {
        $Errors.Add('agentSurface.autonomousCanonicalId is invalid')
    }
    $SpecifyInvocation = Get-HBText $Surface 'specifyInvocation' 'agentSurface'
    $AutonomousInvocation = Get-HBText $Surface 'autonomousInvocation' 'agentSurface'
    if ($SpecifyInvocation.Contains("`n") -or $SpecifyInvocation.Contains("`r")) {
        $Errors.Add('specifyInvocation must be one line')
    }
    if ($AutonomousInvocation.Contains("`n") -or $AutonomousInvocation.Contains("`r")) {
        $Errors.Add('autonomousInvocation must be one line')
    }

    $Authority = Get-HBText $Data 'deliveryAuthority' 'receipt'
    if ($Authority -notin @('LocalImplementation', 'PublishPR', 'MergeAndSync')) {
        $Errors.Add('deliveryAuthority is invalid')
    }
    $AuthorityEvidence = Get-HBText $Data 'authorityEvidence' 'receipt'
    if ($Authority -in @('PublishPR', 'MergeAndSync') -and $AuthorityEvidence.ToLowerInvariant().StartsWith('default')) {
        $Errors.Add('remote delivery authority needs explicit evidence')
    }

    $PromptState = Get-HBText $Data 'promptState' 'receipt'
    if ($PromptState -notin @('Enabled', 'Blocked')) { $Errors.Add('promptState is invalid') }
    if ($Status -eq 'ReadyForReview' -and $OpenIds.Count) { $Errors.Add('ReadyForReview cannot contain open decisions') }
    if ($Status -eq 'ReadyForReview' -and $PromptState -ne 'Enabled') { $Errors.Add('ReadyForReview requires Enabled prompts') }
    if ($Status -eq 'NeedsClarification' -and $OpenIds.Count -eq 0) { $Errors.Add('NeedsClarification requires open decisions') }
    if ($Status -eq 'NeedsClarification' -and $PromptState -ne 'Blocked') { $Errors.Add('NeedsClarification requires Blocked prompts') }

    $Supersedes = Get-HBProperty $Data 'supersedes'
    if ($null -eq $Supersedes) {
        $Errors.Add('supersedes must be an object')
        $Supersedes = [pscustomobject]@{}
    }
    $OldReceipt = Get-HBText $Supersedes 'receiptPath' 'supersedes'
    $OldTargetHash = Get-HBText $Supersedes 'targetNormalizedSha256' 'supersedes'
    $UpdateAuthorized = Get-HBProperty $Data 'updateAuthorized'
    if ($UpdateAuthorized -isnot [bool]) { $Errors.Add('updateAuthorized must be boolean') }
    if (($OldReceipt -eq 'N/A') -ne ($OldTargetHash -eq 'N/A')) {
        $Errors.Add('supersedes fields must both be N/A or both be populated')
    }
    if ($SchemaVersion -eq '1.0') {
        if ($OldReceipt -eq 'N/A') {
            if ($UpdateAuthorized -eq $true) { $Errors.Add('new intake cannot claim updateAuthorized') }
        } else {
            if ($UpdateAuthorized -ne $true) { $Errors.Add('supersession requires updateAuthorized') }
            if (-not (Test-HBRelativePath $OldReceipt)) {
                $Errors.Add('supersedes.receiptPath must be repository-relative')
            } elseif (-not (Test-Path -LiteralPath (Join-Path $RepoRoot $OldReceipt) -PathType Leaf)) {
                $Errors.Add("superseded receipt missing: $OldReceipt")
            }
            Test-HBDigest $OldTargetHash 'supersedes.targetNormalizedSha256'
        }
    } else {
        $ProvenanceMode = Get-HBText $Data 'provenanceMode' 'receipt'
        if ($ProvenanceMode -notin @('New', 'Supersession', 'LegacyAdoption')) {
            $Errors.Add('provenanceMode is invalid')
        }
        $UpdateEvidence = Get-HBText $Data 'updateAuthorityEvidence' 'receipt'
        $Legacy = Get-HBProperty $Data 'legacyAdoption'
        if ($null -eq $Legacy) {
            $Errors.Add('legacyAdoption must be an object')
            $Legacy = [pscustomobject]@{}
        }
        $LegacyType = Get-HBText $Legacy 'evidenceType' 'legacyAdoption'
        $LegacyHash = Get-HBText $Legacy 'priorTargetNormalizedSha256' 'legacyAdoption'
        $LegacyBlob = Get-HBText $Legacy 'priorGitBlob' 'legacyAdoption'

        switch ($ProvenanceMode) {
            'New' {
                if ($UpdateAuthorized -eq $true) { $Errors.Add('new intake cannot claim updateAuthorized') }
                if ($OldReceipt -ne 'N/A' -or $OldTargetHash -ne 'N/A') {
                    $Errors.Add('New provenance cannot supersede an intake')
                }
                if ($LegacyType -ne 'N/A' -or $LegacyHash -ne 'N/A' -or $LegacyBlob -ne 'N/A') {
                    $Errors.Add('New provenance cannot contain legacy-adoption evidence')
                }
                if ($UpdateEvidence -ne 'N/A') {
                    $Errors.Add('New provenance requires updateAuthorityEvidence N/A')
                }
            }
            'Supersession' {
                if ($UpdateAuthorized -ne $true) { $Errors.Add('supersession requires updateAuthorized') }
                if ($UpdateEvidence -eq 'N/A') { $Errors.Add('supersession requires updateAuthorityEvidence') }
                if ($OldReceipt -eq 'N/A') {
                    $Errors.Add('Supersession requires a prior receipt')
                } elseif (-not (Test-HBRelativePath $OldReceipt)) {
                    $Errors.Add('supersedes.receiptPath must be repository-relative')
                } elseif (-not (Test-Path -LiteralPath (Join-Path $RepoRoot $OldReceipt) -PathType Leaf)) {
                    $Errors.Add("superseded receipt missing: $OldReceipt")
                }
                Test-HBDigest $OldTargetHash 'supersedes.targetNormalizedSha256'
                if ($LegacyType -ne 'N/A' -or $LegacyHash -ne 'N/A' -or $LegacyBlob -ne 'N/A') {
                    $Errors.Add('Supersession cannot contain legacy-adoption evidence')
                }
            }
            'LegacyAdoption' {
                if ($UpdateAuthorized -ne $true) { $Errors.Add('legacy adoption requires updateAuthorized') }
                if ($UpdateEvidence -eq 'N/A') { $Errors.Add('legacy adoption requires updateAuthorityEvidence') }
                if ($OldReceipt -ne 'N/A' -or $OldTargetHash -ne 'N/A') {
                    $Errors.Add('LegacyAdoption cannot invent a superseded receipt')
                }
                if ($LegacyType -notin @('GitBlob', 'SnapshotOnly')) {
                    $Errors.Add('legacyAdoption.evidenceType is invalid')
                }
                Test-HBDigest $LegacyHash 'legacyAdoption.priorTargetNormalizedSha256'
                $SourceHashes = @($Sources | ForEach-Object { Get-HBProperty $_ 'normalizedSha256' })
                if ($LegacyHash -notin $SourceHashes) {
                    $Errors.Add('legacy target hash must occur in the source inventory')
                }
                if ($LegacyType -eq 'SnapshotOnly' -and $LegacyBlob -ne 'N/A') {
                    $Errors.Add('SnapshotOnly legacy adoption requires priorGitBlob N/A')
                } elseif ($LegacyType -eq 'GitBlob') {
                    if ($LegacyBlob -notmatch '^(?:[0-9a-f]{40}|[0-9a-f]{64})$') {
                        $Errors.Add('legacyAdoption.priorGitBlob must be a Git object id')
                    } else {
                        try {
                            $BlobHash = Get-HBGitBlobNormalizedHash $LegacyBlob
                            if ($BlobHash -ne $LegacyHash) {
                                $Errors.Add('legacy Git blob does not match prior target hash')
                            }
                        } catch {
                            $Errors.Add("legacy Git blob cannot be verified: $($_.Exception.Message)")
                        }
                    }
                }
            }
        }
    }

    $NextAction = Get-HBText $Data 'nextAction' 'receipt'
    if ($Status -eq 'ReadyForReview') {
        if (-not $NextAction.Contains('speckit-intake-review') -or -not $NextAction.Contains($TargetPathText)) {
            $Errors.Add('ReadyForReview nextAction must name Intake Review and the target')
        }
    } elseif ($NextAction.Contains('speckit-intake-review')) {
        $Errors.Add('NeedsClarification cannot hand off to Intake Review')
    }

    if ($TargetText) {
        $Markers = @(
            '<!-- intake-authoring:begin -->',
            '<!-- intake-authoring:prompts -->',
            '<!-- spec-kit-command-id: speckit.specify -->',
            '<!-- spec-kit-command-id: speckit.autonomous -->',
            '<!-- intake-authoring:end -->'
        )
        foreach ($Marker in $Markers) {
            if ([regex]::Matches($TargetText, [regex]::Escape($Marker)).Count -ne 1) {
                $Errors.Add("target must contain marker exactly once: $Marker")
            }
        }
        $SpecifyPattern = '(?m)^' + [regex]::Escape($SpecifyInvocation) + '(?:\s|$)'
        $AutonomousPattern = '(?m)^' + [regex]::Escape($AutonomousInvocation) + '(?:\s|$)'
        if ($Status -eq 'ReadyForReview') {
            if ($TargetText.Contains('BLOCKED - DO NOT RUN')) { $Errors.Add('enabled target cannot contain BLOCKED - DO NOT RUN') }
            if ($TargetText -notmatch $SpecifyPattern) { $Errors.Add('enabled target lacks rendered Specify invocation') }
            if ($TargetText -notmatch $AutonomousPattern) { $Errors.Add('enabled target lacks rendered Autonomous invocation') }
            if (-not $TargetText.Contains($TargetPathText)) { $Errors.Add('enabled prompts must name the exact target path') }
            if (-not $TargetText.Contains($Authority)) { $Errors.Add('enabled Autonomous prompt must name deliveryAuthority') }
        } else {
            if (-not $TargetText.Contains('BLOCKED - DO NOT RUN')) { $Errors.Add('blocked target must contain BLOCKED - DO NOT RUN') }
            if ($TargetText -match $SpecifyPattern) { $Errors.Add('blocked target contains executable Specify invocation') }
            if ($TargetText -match $AutonomousPattern) { $Errors.Add('blocked target contains executable Autonomous invocation') }
            foreach ($OpenId in $OpenIds) {
                if (-not $TargetText.Contains($OpenId)) { $Errors.Add("blocked target does not name open decision: $OpenId") }
            }
        }
    }

    if ($Errors.Count) {
        foreach ($Message in $Errors) { [Console]::Error.WriteLine("ERROR: $Message") }
        return 2
    }

    Write-Host "PASS: intake authoring $ReceiptId is current ($Status, $($Sources.Count) sources, $TargetPathText)"
    return 0
}

$ExitCode = Test-IntakeAuthoringReceipt -Receipt $Receipt -Repo $Repo
exit $ExitCode
