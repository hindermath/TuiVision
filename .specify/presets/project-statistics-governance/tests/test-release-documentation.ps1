#Requires -Version 7.3
<#
.SYNOPSIS
    Release-Dokumentation pruefen / Test release documentation.
.DESCRIPTION
    DE: Statische Vertragspruefung und bei Statistics eigene Offline-Doubles.
    Keine Modellaufrufe, echte Downloads, Installation oder Provider-Aktionen.
    EN: Static contracts and owned offline doubles for Statistics. No model
    calls, real downloads, installations or provider operations.
.EXAMPLE
    pwsh -NoProfile -File tests/test-release-documentation.ps1
#>
[CmdletBinding()]
param()
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$metadata=[IO.File]::ReadAllText((Join-Path $root 'preset.yml'))
$id=[regex]::Match($metadata,'(?m)^  id: "([^"]+)"').Groups[1].Value
$version=[regex]::Match($metadata,'(?m)^  version: "([^"]+)"').Groups[1].Value
$readme=[IO.File]::ReadAllText((Join-Path $root 'README.md'))
$priorities=@{'secure-development-assurance-governance'=15;'intake-authoring-governance'=64;'autonomous-run-governance'=70;'parallel-autonomous-run-governance'=80;'project-statistics-governance'=90}
$assertions=0
function Assert-Documentation {
    param([bool]$Condition,[string]$Message)
    if(-not $Condition){throw "FAIL: $Message"}
    $script:assertions++
}
$url="https://github.com/hindermath/spec-kit-preset-$id/archive/refs/tags/v$version.zip"
$line="specify preset add --from $url --priority $($priorities[$id])"
Assert-Documentation ([regex]::IsMatch($readme,'(?m)^\s*'+[regex]::Escape($line)+'\r?$')) 'exact release install command on one line'
Assert-Documentation (Test-Path -LiteralPath (Join-Path $root "docs/release-$version-validation.md")) 'current release preparation record exists'
switch($id){
'project-statistics-governance' {
        Assert-Documentation (-not $readme.Contains('d8ad7d5eef920f50b629121b64ba8123c22ec4f6dadd14da5cd826d1c50f420a')) 'old archive hash never presented as current'
        Assert-Documentation (-not $readme.Contains('v0.1.0.zip') -and -not $readme.Contains('v0.1.1.zip')) 'current download filenames and URLs agree'
        Assert-Documentation ($readme.Contains('nicht vorausgesetzt') -and $readme.Contains('are not assumed')) 'uploaded checksum assets are not falsely promised'
        $bashBlocks=@([regex]::Matches($readme,'(?s)\x60{3}bash\r?\n(.*?)\r?\n\x60{3}'))
        $psBlocks=@([regex]::Matches($readme,'(?s)\x60{3}powershell\r?\n(.*?)\r?\n\x60{3}'))
        $bash=@($bashBlocks|Where-Object {$_.Groups[1].Value.Contains("expected_sha256=")})[0].Groups[1].Value
        $ps=@($psBlocks|Where-Object {$_.Groups[1].Value.Contains('$ExpectedSha256 =')})[0].Groups[1].Value
        $temp=Join-Path ([IO.Path]::GetTempPath()) ('statistics-doc-'+[guid]::NewGuid().ToString('N'))
        [void][IO.Directory]::CreateDirectory($temp)
        $savedTemp=$env:TMPDIR
        try {
            $env:TMPDIR=$temp
            foreach($case in @('match','mismatch','missing')){
                $expected='a'*64;$actual=if($case -ceq 'mismatch'){'b'*64}else{$expected}
                $psCase=$ps;$bashCase=$bash
                if($case -cne 'missing'){
                    $psCase=$psCase.Replace('REPLACE_WITH_V0_1_2_SOURCE_ARCHIVE_SHA256_FROM_RELEASE_NOTES',$expected)
                    $bashCase=$bashCase.Replace('REPLACE_WITH_V0_1_2_SOURCE_ARCHIVE_SHA256_FROM_RELEASE_NOTES',$expected)
                }
                # The real README snippets run with explicit download/hash doubles.
                # This tests the guard, never a fabricated future release hash.
                $psPrefix='$ErrorActionPreference="Stop"; function Invoke-WebRequest { param($Uri,$OutFile) }; function Get-FileHash { param($LiteralPath,$Algorithm) [pscustomobject]@{Hash="'+$actual+'"} };'
                $psFile=Join-Path $temp ($case+'.ps1')
                [IO.File]::WriteAllText($psFile,$psPrefix+[Environment]::NewLine+$psCase)
                $null=& pwsh -NoProfile -File $psFile 2>&1
                $code=$LASTEXITCODE
                Assert-Documentation (($code -eq 0) -eq ($case -ceq 'match')) "PowerShell checksum example: $case"
                if(-not $IsWindows){
                    $bashPrefix="curl() { return 0; }; shasum() { printf '%s  fixture\n' '"+$actual+"'; };"
                    $bashFile=Join-Path $temp ($case+'.sh')
                    [IO.File]::WriteAllText($bashFile,'set -euo pipefail'+[Environment]::NewLine+$bashPrefix+[Environment]::NewLine+$bashCase)
                    $null=& bash $bashFile 2>&1
                    $code=$LASTEXITCODE
                    Assert-Documentation (($code -eq 0) -eq ($case -ceq 'match')) "Bash checksum example: $case"
                }
            }
        } finally {$env:TMPDIR=$savedTemp;Remove-Item -LiteralPath $temp -Recurse -Force}
    }
}
"PASS: $assertions release-documentation assertions; no model calls or network downloads."
exit 0
