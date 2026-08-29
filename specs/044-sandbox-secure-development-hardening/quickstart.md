# Quickstart: Sandbox Applicability Evidence

## 1. Kanonische Evidence prüfen / Validate Canonical Evidence

```bash
bash scripts/validate-sandbox-applicability.sh \
  --evidence docs/security/secure-development/2026-08-29-sandbox-applicability/assessment.json \
  --repo-root "$PWD"
```

```powershell
pwsh -NoProfile -File scripts/validate-sandbox-applicability.ps1 `
  -Evidence docs/security/secure-development/2026-08-29-sandbox-applicability/assessment.json `
  -RepositoryRoot (Get-Location)
```

Beide Befehle bleiben read-only und liefern dieselbe Entscheidung und denselben
Exitcode. Ein Pass bestätigt nur den JSON-Vertrag.

*Both commands are read-only and return the same decision and exit code. A pass
confirms only the JSON contract.*

## 2. Negative Fixtures ausführen / Run Negative Fixtures

```bash
python3 -m unittest scripts.tests.test_sandbox_applicability -v
```

## 3. Statische Sandbox-Referenz prüfen / Check Static Sandbox Reference

Die externe Vergleichskopie bleibt read-only. Prüfe Commit und Compose-Syntax,
ohne `.env`-Inhalte auszugeben:

```bash
git -C "$SANDBOX_CHECKOUT" status --short --branch
git -C "$SANDBOX_CHECKOUT" rev-parse HEAD
(cd "$SANDBOX_CHECKOUT" && podman-compose --env-file .env.example config --quiet)
```

`SANDBOX_CHECKOUT` ist eine lokale Rolle und wird nicht in Evidence aufgelöst.

*`SANDBOX_CHECKOUT` is a local role and is not resolved into versioned evidence.*

## 4. Ergebnis lesen / Read the Result

Beginne mit
`docs/security/secure-development/2026-08-29-sandbox-applicability/README.md`.
Die nächste sichere Aktion steht dort und in `assessment.json`. Ein `Open`
bleibt offen; es ist kein bestandener Nachweis.
