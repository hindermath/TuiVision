# hg-patch.ps1 — memory-patch.md Generator (FR-020/021) — PowerShell

function Add-HgPatchContentSafely {
    param(
        [Parameter(Mandatory)][string]$Root,
        [Parameter(Mandatory)][string]$Target,
        [Parameter(Mandatory)][AllowEmptyString()][string]$Content
    )

    $python = Get-Command python3 -ErrorAction Stop
    $program = @'
import os, pathlib, stat, sys, uuid
root = pathlib.Path(sys.argv[1]).resolve(strict=True)
target = pathlib.Path(sys.argv[2]).resolve(strict=True)
relative = pathlib.Path(os.path.relpath(target, root))
if relative.is_absolute() or ".." in relative.parts or not relative.parts:
    raise SystemExit("unsafe patch target")
addition = (os.environ["HB_PATCH_CONTENT"] + "\n").encode("utf-8")
if len(addition) > 1024 * 1024:
    raise OSError("patch content exceeds byte limit")
if os.name == "nt":
    current = root
    for component in relative.parts:
        current /= component
        item = os.lstat(current)
        if stat.S_ISLNK(item.st_mode) or (
            getattr(item, "st_file_attributes", 0)
            & getattr(stat, "FILE_ATTRIBUTE_REPARSE_POINT", 0)
        ):
            raise OSError("patch target contains a link or reparse point")
    if not stat.S_ISREG(item.st_mode):
        raise OSError("patch target is not a regular file")
    with target.open("rb") as source:
        existing = source.read(8 * 1024 * 1024 + 1)
    if len(existing) > 8 * 1024 * 1024:
        raise OSError("patch target exceeds byte limit")
    temporary_path = target.parent / f".{target.name}.{uuid.uuid4().hex}.tmp"
    try:
        with temporary_path.open("xb") as stream:
            stream.write(existing + addition)
            stream.flush()
            os.fsync(stream.fileno())
        os.chmod(temporary_path, item.st_mode & 0o777)
        current = root
        for component in relative.parts[:-1]:
            current /= component
            checked = os.lstat(current)
            if stat.S_ISLNK(checked.st_mode) or (
                getattr(checked, "st_file_attributes", 0)
                & getattr(stat, "FILE_ATTRIBUTE_REPARSE_POINT", 0)
            ):
                raise OSError("patch parent changed during validation")
        os.replace(temporary_path, target)
    finally:
        temporary_path.unlink(missing_ok=True)
    raise SystemExit(0)
flags = os.O_RDONLY | getattr(os, "O_DIRECTORY", 0) | getattr(os, "O_NOFOLLOW", 0)
parent = os.open(root, flags)
temporary = None
try:
    for component in relative.parts[:-1]:
        child = os.open(component, flags, dir_fd=parent)
        os.close(parent)
        parent = child
    leaf = relative.parts[-1]
    metadata = os.stat(leaf, dir_fd=parent, follow_symlinks=False)
    if not stat.S_ISREG(metadata.st_mode):
        raise OSError("patch target is not a regular file")
    source = os.open(leaf, os.O_RDONLY | getattr(os, "O_NOFOLLOW", 0), dir_fd=parent)
    try:
        opened = os.fstat(source)
        if (opened.st_dev, opened.st_ino) != (metadata.st_dev, metadata.st_ino):
            raise OSError("patch target changed during validation")
        existing = bytearray()
        while chunk := os.read(source, 65536):
            existing.extend(chunk)
            if len(existing) > 8 * 1024 * 1024:
                raise OSError("patch target exceeds byte limit")
    finally:
        os.close(source)
    temporary = f".{leaf}.{uuid.uuid4().hex}.tmp"
    descriptor = os.open(temporary, os.O_WRONLY | os.O_CREAT | os.O_EXCL | getattr(os, "O_NOFOLLOW", 0), metadata.st_mode & 0o777, dir_fd=parent)
    try:
        payload = bytes(existing) + addition
        offset = 0
        while offset < len(payload):
            offset += os.write(descriptor, payload[offset:])
        os.fsync(descriptor)
    finally:
        os.close(descriptor)
    os.replace(temporary, leaf, src_dir_fd=parent, dst_dir_fd=parent)
    temporary = None
    os.fsync(parent)
finally:
    if temporary is not None:
        try: os.unlink(temporary, dir_fd=parent)
        except OSError: pass
    os.close(parent)
'@
    $previousContent = [Environment]::GetEnvironmentVariable('HB_PATCH_CONTENT')
    try {
        [Environment]::SetEnvironmentVariable('HB_PATCH_CONTENT', $Content)
        & $python.Source -c $program $Root $Target
        if ($LASTEXITCODE -ne 0) {
            throw 'Patch target changed or could not be replaced safely.'
        }
    } finally {
        [Environment]::SetEnvironmentVariable('HB_PATCH_CONTENT', $previousContent)
    }
}

function Invoke-HgGeneratePatch {
    param(
        [string]$StatsFile,
        [string[]]$Issues = @()
    )

    $currentScore = 0
    $prevScore    = 0

    if (Test-Path $StatsFile) {
        $scoreLines = Select-String -Path $StatsFile -Pattern 'Overall Score: \*\*(\d+)'
        if ($scoreLines.Count -ge 1) {
            $currentScore = [int]$scoreLines[-1].Matches.Groups[1].Value
        }
        if ($scoreLines.Count -ge 2) {
            $prevScore = [int]$scoreLines[-2].Matches.Groups[1].Value
        }
    }

    $triggered = $false
    $triggerReason = ''

    # Trigger 1: new repo
    if ($prevScore -eq 0 -and $currentScore -gt 0) {
        $triggered = $true; $triggerReason = 'new-repo-detected'
    }

    # Trigger 2: score delta >= 10
    $delta = [Math]::Abs($currentScore - $prevScore)
    if ($prevScore -gt 0 -and $delta -ge 10) {
        $triggered = $true
        $triggerReason = ($triggerReason + ",score-delta-$delta").TrimStart(',')
    }

    if (-not $triggered) { return }

    $specsDir = Join-Path $(if ($env:HOME) { $env:HOME } else { $env:USERPROFILE }) 'specs'
    $activeFeature = Get-ChildItem -Path $specsDir -Directory -Filter '001-*' -ErrorAction SilentlyContinue | Select-Object -First 1
    $patchFile = if ($activeFeature) { Join-Path $activeFeature.FullName 'memory-patch.md' } else { Join-Path $specsDir 'memory-patch.md' }

    $lines = @(
        "# memory-patch.md",
        "",
        "**Generated**: $(Get-Date -Format 'yyyy-MM-dd HH:mm')",
        "**Trigger**: $triggerReason",
        "**Score**: $currentScore % (prev: $prevScore %)",
        "",
        "---",
        ""
    )

    $entryCount = 0
    foreach ($issue in $Issues) {
        $parts     = $issue -split ':', 2
        $filepath  = $parts[0]
        $msg       = $parts[1]

        $routing = switch -Regex ($filepath) {
            'CLAUDE\.md|AGENTS\.md|GEMINI\.md|copilot' { 'agent_file'; break }
            'README\.md'                                { 'readme';     break }
            'constitution'                              { 'constitution'; break }
            default                                     { 'agent_file' }
        }

        $entryCount++
        $lines += @(
            "## MemoryPatchEntry $entryCount",
            "",
            "- **File**: ``$filepath``",
            "- **Issue**: $msg",
            "- **Routing**: $routing",
            "- **Action**: Review and fix the reported issue",
            "",
            "### Target: $filepath",
            "",
            "+++ [Review needed: $msg]",
            "",
            "---",
            ""
        )
    }

    $lines += "**Total entries**: $entryCount"
    $lines | Set-Content $patchFile -Encoding UTF8

    if ($entryCount -gt 0) {
        Write-Host "memory-patch.md: $patchFile ($entryCount entries)"
    }
}
