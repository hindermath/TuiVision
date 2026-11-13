Set-StrictMode -Version Latest

function Resolve-HBForgejoBaseUrl {
    param(
        [Parameter(Mandatory)][string]$Platform,
        [string]$ConfiguredUrl = ''
    )

    $resolvedUrl = switch ($Platform) {
        'codeberg' {
            if ($ConfiguredUrl) {
                throw '-ForgejoUrl cannot be combined with -Platform codeberg.'
            }
            'https://codeberg.org'
        }
        'forgejo' {
            if (-not $ConfiguredUrl) {
                throw '-Platform forgejo requires -ForgejoUrl https://HOST.'
            }
            $ConfiguredUrl
        }
        default { throw "Not a Forgejo platform: $Platform" }
    }

    $uri = $null
    if (-not [uri]::TryCreate($resolvedUrl, [System.UriKind]::Absolute, [ref]$uri) `
        -or $uri.Scheme -cne 'https' `
        -or -not $uri.Host `
        -or $uri.UserInfo `
        -or $uri.Query `
        -or $uri.Fragment `
        -or $uri.AbsolutePath -notin @('', '/') `
        -or $resolvedUrl -match '[\x00-\x20\x7f"\\]') {
        throw 'The Forgejo base URL must be one HTTPS origin without credentials, path, or control characters.'
    }
    $resolvedUrl.TrimEnd('/')
}

function Get-HBForgejoCredential {
    param([Parameter(Mandatory)][string]$BaseUrl)

    $helper = ((& git config --get-all credential.helper 2>$null) | Out-String).Trim()
    if (-not $helper) {
        throw 'No secure Git credential helper is configured. Use Keychain, Git Credential Manager, or an institution-approved Linux secret store.'
    }

    $credentialLines = @("url=$BaseUrl", '') | & git credential fill
    if ($LASTEXITCODE -ne 0) {
        throw 'Forgejo credentials could not be read through the Git credential helper.'
    }

    $username = ''
    $token = ''
    foreach ($line in $credentialLines) {
        if ($line -like 'username=*') { $username = $line.Substring(9) }
        if ($line -like 'password=*') { $token = $line.Substring(9) }
    }
    if (-not $username -or -not $token) {
        throw 'Username or token is missing from the Git credential helper.'
    }

    [pscustomobject]@{ Username = $username; Token = $token }
}

function Approve-HBForgejoCredential {
    param(
        [Parameter(Mandatory)][string]$BaseUrl,
        [Parameter(Mandatory)]$AuthenticationData
    )

    @(
        "url=$BaseUrl"
        "username=$($AuthenticationData.Username)"
        "password=$($AuthenticationData.Token)"
        ''
    ) | & git credential approve
    if ($LASTEXITCODE -ne 0) {
        throw 'Git credential helper did not accept the Forgejo credential.'
    }
}

function Invoke-HBForgejoApi {
    param(
        [Parameter(Mandatory)][ValidateSet('GET','POST','DELETE')][string]$Method,
        [Parameter(Mandatory)][string]$BaseUrl,
        [Parameter(Mandatory)][string]$Path,
        [Parameter(Mandatory)]$AuthenticationData,
        [string]$Body = ''
    )

    $parameters = @{
        Uri               = "$BaseUrl$Path"
        Method            = $Method
        Headers           = @{
            Accept        = 'application/json'
            Authorization = "token $($AuthenticationData.Token)"
        }
        SkipHttpErrorCheck = $true
    }
    if ($Body) {
        $parameters.ContentType = 'application/json'
        $parameters.Body = $Body
    }

    try {
        $response = Invoke-WebRequest @parameters
    } catch {
        throw "Forgejo API is not reachable at ${BaseUrl}: $($_.Exception.Message)"
    }

    [pscustomobject]@{
        StatusCode = [int]$response.StatusCode
        Content    = [string]$response.Content
    }
}

function Get-HBForgejoErrorMessage {
    param([string]$Action, [int]$StatusCode)

    switch ($StatusCode) {
        401 { "$Action`: token invalid or expired (HTTP 401)." }
        403 { "$Action`: permission or repository creation denied (HTTP 403)." }
        404 { "$Action`: API disabled, URL incorrect, or repository not found (HTTP 404)." }
        409 { "$Action`: repository conflict (HTTP 409)." }
        default { "$Action failed (HTTP $StatusCode)." }
    }
}

function New-HBForgejoRepository {
    param(
        [Parameter(Mandatory)][string]$BaseUrl,
        [Parameter(Mandatory)][string]$RepositoryName,
        [string]$Description = ''
    )

    $credential = Get-HBForgejoCredential -BaseUrl $BaseUrl
    $userResponse = Invoke-HBForgejoApi -Method GET -BaseUrl $BaseUrl -Path '/api/v1/user' -AuthenticationData $credential
    if ($userResponse.StatusCode -ne 200) {
        throw (Get-HBForgejoErrorMessage -Action 'Validate sign-in' -StatusCode $userResponse.StatusCode)
    }
    $owner = ($userResponse.Content | ConvertFrom-Json).login
    if (-not $owner) { throw 'Forgejo username is missing from the API response.' }

    $encodedOwner = [uri]::EscapeDataString($owner)
    $encodedRepo = [uri]::EscapeDataString($RepositoryName)
    $repoResponse = Invoke-HBForgejoApi -Method GET -BaseUrl $BaseUrl -Path "/api/v1/repos/$encodedOwner/$encodedRepo" -AuthenticationData $credential
    $created = $false
    if ($repoResponse.StatusCode -eq 404) {
        $body = @{
            name           = $RepositoryName
            description    = $Description
            private        = $true
            auto_init      = $false
            default_branch = 'main'
        } | ConvertTo-Json -Compress
        $repoResponse = Invoke-HBForgejoApi -Method POST -BaseUrl $BaseUrl -Path '/api/v1/user/repos' -AuthenticationData $credential -Body $body
        if ($repoResponse.StatusCode -ne 201) {
            throw (Get-HBForgejoErrorMessage -Action 'Create repository' -StatusCode $repoResponse.StatusCode)
        }
        $created = $true
    } elseif ($repoResponse.StatusCode -ne 200) {
        throw (Get-HBForgejoErrorMessage -Action 'Check repository' -StatusCode $repoResponse.StatusCode)
    }

    $repository = $repoResponse.Content | ConvertFrom-Json
    if (-not $repository.name -or -not $repository.clone_url -or -not $repository.html_url) {
        throw 'Incomplete Forgejo repository response.'
    }
    $baseUri = [uri]$BaseUrl
    $cloneUri = [uri]$repository.clone_url
    $htmlUri = [uri]$repository.html_url
    $expectedPath = "/$owner/$RepositoryName"
    $repositoryOwner = if ($repository.owner) { [string]$repository.owner.login } else { '' }
    if ($baseUri.Scheme -ne 'https' `
        -or $repositoryOwner -cne $owner `
        -or [string]$repository.name -cne $RepositoryName `
        -or $repository.private -ne $true `
        -or $cloneUri.Scheme -ne 'https' `
        -or $cloneUri.Authority -cne $baseUri.Authority `
        -or $cloneUri.UserInfo `
        -or $cloneUri.Query `
        -or $cloneUri.Fragment `
        -or $cloneUri.AbsolutePath.TrimEnd('/') -cne "${expectedPath}.git" `
        -or $htmlUri.Scheme -ne 'https' `
        -or $htmlUri.Authority -cne $baseUri.Authority `
        -or $htmlUri.UserInfo `
        -or $htmlUri.Query `
        -or $htmlUri.Fragment `
        -or $htmlUri.AbsolutePath.TrimEnd('/') -cne $expectedPath) {
        throw 'Forgejo repository does not match the private authenticated target.'
    }
    Approve-HBForgejoCredential -BaseUrl $BaseUrl -AuthenticationData $credential

    [pscustomobject]@{
        Owner     = $owner
        Name      = $repository.name
        CloneUrl  = $repository.clone_url
        HtmlUrl   = $repository.html_url
        Created   = $created
    }
}

function Remove-HBForgejoRepository {
    param(
        [Parameter(Mandatory)][string]$BaseUrl,
        [Parameter(Mandatory)][string]$Owner,
        [Parameter(Mandatory)][string]$RepositoryName
    )

    $credential = Get-HBForgejoCredential -BaseUrl $BaseUrl
    $encodedOwner = [uri]::EscapeDataString($Owner)
    $encodedRepo = [uri]::EscapeDataString($RepositoryName)
    $response = Invoke-HBForgejoApi -Method DELETE -BaseUrl $BaseUrl -Path "/api/v1/repos/$encodedOwner/$encodedRepo" -AuthenticationData $credential
    if ($response.StatusCode -ne 204) {
        throw (Get-HBForgejoErrorMessage -Action 'Delete repository' -StatusCode $response.StatusCode)
    }
}
