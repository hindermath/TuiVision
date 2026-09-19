# prove-project-statistics-context(1)

## Name

`prove-project-statistics-context.ps1` — Statistik-Kontext pruefen /
verify the statistics context.

## Aufruf / Synopsis

```powershell
pwsh -NoProfile -File scripts/prove-project-statistics-context.ps1 -Repo . -EvidenceDirectory /tmp/tuivision-proof
```

## Voraussetzungen / Requirements

Sauberer vollstaendiger Git-Checkout, installierte 14er-Matrix, vorhandener
Snapshot, Git, PowerShell 7 und Spec Kit 0.12.8. Bash wird auf Unix geprueft.
Das Ergebnisverzeichnis muss neu und ausserhalb des Repositorys liegen.

Requires a clean complete checkout, fourteen presets, an existing snapshot,
Git, PowerShell 7 and Spec Kit 0.12.8. Unix also tests Bash. The new evidence
directory must be outside the repository.

## Verhalten und Exitcodes / Behavior and exit codes

Prueft Paket-/Matrix-Hashes, installierte Oberflaechen, Fixtures, aktuellen
Status und Nullschreiben. Eine lokale temporaere Kopie prueft wiederholtes
Update und LF/CRLF/BOM; sie wird anschliessend entfernt. Ergebnisdateien
enthalten exakten Head, Messrevision, Stichtag, Befehle, Plattform und Hashes.
Exit 0 bezeichnet Erfolg, ungleich 0 eine fehlende Voraussetzung oder einen
Prueffehler. Kein automatischer Download, Commit, Push, Merge oder Freigabe.

Verifies package/profile hashes, installed surfaces, fixtures, current status
and unchanged source files. An isolated local clone tests idempotency and
encoding parity and is removed afterwards. Evidence binds the exact head,
measurement revision, cutoff, commands, platform and hashes. Exit 0 is success;
nonzero indicates missing requirements or a failed check. No automatic network
fetch, Git delivery or human acceptance. Checked-file hashes are not a system
I/O trace.
