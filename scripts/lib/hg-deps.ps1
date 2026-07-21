# hg-deps.ps1 — Bezahlte NuGet-Paket-Erkennung (FR-016) — PowerShell

$HG_PAID_VENDORS = @(
    'DevExpress\.', 'Telerik\.', 'Syncfusion\.', 'Infragistics\.',
    'MESCIUS\.', 'ComponentOne\.', 'GrapeCity\.', 'Actipro\.'
)

function Invoke-HgCheckDeps {
    param([string]$Dir)

    $csprojFiles = Get-ChildItem -Path $Dir -Filter '*.csproj' -Recurse -Depth 5 -ErrorAction SilentlyContinue

    foreach ($file in $csprojFiles) {
        $lines = Get-Content -Path $file.FullName -ErrorAction SilentlyContinue
        $lineNum = 0
        foreach ($line in $lines) {
            $lineNum++
            foreach ($vendor in $HG_PAID_VENDORS) {
                if ($line -match "<PackageReference Include=`"${vendor}") {
                    $package = [regex]::Match($line, 'Include="([^"]*)"').Groups[1].Value
                    [PSCustomObject]@{
                        Status  = 'WARN'
                        File    = "$($file.FullName):${lineNum}"
                        Message = "paid-dependency-detected: ${package}"
                    }
                }
            }
        }
    }
}
