# hg-secrets.ps1 — Secret-Pattern-Erkennung (FR-003) — PowerShell
# Matched value wird IMMER als [REDACTED] ausgegeben

$HG_SECRET_FILENAMES = @(
    'password', 'passwd', 'secret', 'credential', 'token',
    'apikey', 'api_key', 'private_key', 'id_rsa', 'id_dsa',
    'id_ecdsa', 'id_ed25519', '.env', '.netrc', '.htpasswd'
)

$HG_SECRET_PATTERN = 'ghp_[A-Za-z0-9]{36}|ghs_[A-Za-z0-9]{36}|github_pat_[A-Za-z0-9_]{82}|sk-[A-Za-z0-9]{48}|AKIA[A-Z0-9]{16}|AIzaSy[A-Za-z0-9_-]{33}|-----BEGIN (RSA |EC |OPENSSH |DSA )?PRIVATE KEY-----'

function Invoke-HgScanFileSecrets {
    param([string]$FilePath)

    if (-not (Test-Path $FilePath)) { return }

    # Check filename
    $basename = [IO.Path]::GetFileNameWithoutExtension($FilePath).ToLower()
    foreach ($pattern in $HG_SECRET_FILENAMES) {
        if ($basename -match $pattern) {
            [PSCustomObject]@{
                Status  = 'FAIL'
                File    = $FilePath
                Line    = 0
                Message = 'secret-filename-detected [REDACTED]'
            }
            return
        }
    }

    # Check content
    $secretMatches = Select-String -Path $FilePath -Pattern $HG_SECRET_PATTERN -ErrorAction SilentlyContinue
    foreach ($m in $secretMatches) {
        [PSCustomObject]@{
            Status  = 'FAIL'
            File    = $FilePath
            Line    = $m.LineNumber
            Message = 'secret-pattern-detected [REDACTED]'
        }
    }
}

function Invoke-HgCheckSecrets {
    param([string]$Dir)

    $files = Get-ChildItem -Path $Dir -File -Recurse -Depth 3 -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -notmatch [regex]::Escape('.git') }

    foreach ($f in $files) {
        Invoke-HgScanFileSecrets -FilePath $f.FullName
    }
}
