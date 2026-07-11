#Requires -Version 7
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

try {
    $data = [Console]::In.ReadToEnd() | ConvertFrom-Json
    $cwd = if ($data.workspace.current_dir) { $data.workspace.current_dir } else { $data.cwd }
    $directory = if ($cwd) { Split-Path -Leaf $cwd.TrimEnd('/', '\') } else { '?' }
    $model = if ($data.model.display_name) { $data.model.display_name } elseif ($data.model.id) { $data.model.id } else { 'model:?' }
    $parts = [System.Collections.Generic.List[string]]::new()
    $parts.Add("agy $($data.version ?? '?')")
    $parts.Add([string]$model)
    $parts.Add([string]($data.agent_state ?? 'state:?'))
    $parts.Add($directory)
    if ($data.vcs.branch) {
        $dirty = if ($data.vcs.dirty) { '*' } else { '' }
        $parts.Add("git:$($data.vcs.branch)$dirty")
    }
    if ($null -ne $data.context_window.remaining_percentage) {
        $parts.Add(('ctx:{0:N0}%' -f [double]$data.context_window.remaining_percentage))
    }
    $parts.Add($(if ($data.sandbox.enabled) { 'sandbox:on' } else { 'sandbox:off' }))
    Write-Output ($parts -join ' | ')
} catch {
    Write-Output 'agy | status unavailable'
}
