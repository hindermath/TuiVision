# resolve-model-routing(1)

## NAME

resolve-model-routing - inspect or refresh local Spec Kit model bindings

## SYNOPSIS

```text
pwsh -NoProfile -File scripts/resolve-model-routing.ps1 -Action Status [-Harness Codex]
bash scripts/resolve-model-routing.sh -Action Refresh -Harness Antigravity
```

## DESCRIPTION

`Status` is read-only. `Refresh` writes only the selected local profile after
a known, unique mapping passes discovery and validation. Unknown or ambiguous
models fail closed. Neither action changes a repository or grants delivery
authority.

## EXIT STATUS

- `0`: aligned or refreshed successfully
- `2`: refresh required or needs clarification
- `3`: blocked by missing harness, discovery, model, or reasoning evidence
