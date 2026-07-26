# validate-documentation-impact(1)

## Name

`validate-documentation-impact` - prüft Evidence für
Dokumentationsauswirkungen / validates Documentation Impact evidence

## Synopsis

```bash
bash scripts/validate-documentation-impact.sh --evidence FILE
```

```powershell
pwsh -NoProfile -File scripts/validate-documentation-impact.ps1 -Evidence FILE
```

## Description

Der Validator akzeptiert genau `UpdateRequired`, `NoUpdateRequired`,
`GeneratedUpdate` und `FollowUp`. Er prüft Struktur und Proof-Grenzen, aber
nicht die semantische Wahrheit einer Dokumentationsaussage.

*The validator accepts exactly the four documented decisions. It checks
structure and proof boundaries, not the semantic truth of documentation.*

## Exit Status

- `0`: gültig / valid
- `1`: Evidence ungültig / invalid evidence
- `2`: Aufruf- oder Toolingfehler / usage or tooling error

## Files

`scripts/tests/documentation-impact/fixtures/`
: Portable positive and negative contract fixtures for both test runners.
