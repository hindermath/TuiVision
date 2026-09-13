# Lifecycle-Patch 0.2.4 / Lifecycle Patch 0.2.4

Dokumentationsentscheidung: `UpdateRequired`. Owner: Preset-Maintainer.
Quelle: dieses eigenstaendige Preset-Repository. Zielgruppen: Maintainer und
Agenten; Leserpfad: README → Runbook/Checkliste → Validator → gezielte Korrektur.
Dokumentklasse: Produktvertrag und Test-Evidence; Deutsch zuerst, Englisch danach.
Navigation: README und Dokumentationsindex verlinken diesen Nachweis.
Distribution: versioniertes Preset-Paket; kein allgemeiner Home-Sync.
Re-Evaluation: bei Aenderung von Collection-, Manifest- oder Receipt-Vertraegen.

*Documentation impact is UpdateRequired. The standalone preset repository is
the source; its maintainer owns this product contract and test evidence.
README and documentation index link to it. Distribution uses a versioned preset
package, with no general Home sync. Reevaluate on lifecycle contract changes.*

## Abgeschlossene Serien / Completed Series

`Completed`-Mitglieder liegen in der konfigurierten Archivsammlung. Noch
nicht abgeschlossene Serienmitglieder liegen in der aktiven Sammlung;
Backlog und History sind keine ausfuehrbaren Serienquellen. Eine laufende
Serie darf archivierte Vorgaenger enthalten. Eine abgeschlossene Serie
behaelt ihre Mitglieder und hat null `Eligible`-Ziele (`eligibleCandidate: N/A`).

`activeIntakeCount` zaehlt die physischen passenden Dateien direkt in der
aktiven Sammlung; das zusaetzliche Feld `activeSeriesTargetCount` zaehlt nur
aktive Serienmitglieder. `seriesTargetCount` umfasst auch archivierte Mitglieder.
`SeriesManifest` erlaubt eigenstaendige aktive Intakes ausserhalb der Serie.
Ein fehlendes leeres Aktivverzeichnis zaehlt dort als null; ein Dateipfad statt
eines Verzeichnisses bleibt ungueltig. `DirectoryStrict` verlangt das aktive
Verzeichnis und gleicht dessen Bestand mit den aktiven Serienmitgliedern ab.

Die Pruefung meldet Status-/Ablagewidersprueche als `RIG017`, verschiebt aber
keine Dateien. Eine Korrektur benoetigt einen eigenen Aenderungsauftrag.

*Completed members belong to the configured archive. Non-completed members
belong to the active collection; backlog and history are not executable
sources. Active series may retain archived predecessors. Completed series
retain all members and expose no eligible candidate. Physical active files,
active series members, and all series members have separate counts.
SeriesManifest permits standalone active intakes and an absent empty active
directory. DirectoryStrict requires that directory and compares its contents
with active series members. RIG017 reports lifecycle mismatches without moving
files or granting repair authority.*

## Pruefung / Validation

`tests/test-intake-governance-config.ps1` prueft beide oeffentlichen Wrapper:
gemischte und abgeschlossene Serien, eigenstaendige aktive Intakes, getrennte
Bestandszahlen, fehlende Verzeichnisse, falsche Ablage, unvollstaendige Serien,
deutsche/englische Namen, LF/CRLF, identisches JSON und unveraenderte Dateien.
Die vorhandenen fachlichen Validator-Suiten bleiben verpflichtend.
Der Workflow `lifecycle-validation.yml` fuehrt die Suiten auf macOS, Linux
und Windows am PR-Head aus. Ein geplanter oder ausstehender Lauf ist kein Pass.

*Both wrappers cover valid and invalid lifecycle states, separate inventory
counts, real clean-checkout behavior, German/English names, LF/CRLF, identical
JSON, and zero writes. Existing domain suites remain mandatory. Native CI binds
these commands to the PR head on macOS, Linux and Windows; pending is not passed.*

## Sicherheits- und Release-Grenzen / Security and Release Boundaries

NIST SSDF und CWE Top 25: Eingaben und Datei-I/O pruefen; keine automatische
Migration. Supply Chain/SLSA: exakten Commit, MIT-Lizenz, Paket-Hash und
Testlaeufe an das Release binden. Keine neuen Drittanbieterbibliotheken;
Laufzeitbestand: Python-Standardbibliothek und PowerShell 7. AI-SBOM: N/A,
KI dient nur als Entwicklungswerkzeug. ASVS/Zero Trust/C3A/C5: N/A, kein
Webdienst oder Cloud-Produktbetrieb. Regulatorischer Produkt-Scope unveraendert.
Release erst nach gruenen Tests und erforderlicher PR-Freigabe. Keine neue
Bypass-Autoritaet; keine Flotteninstallation. TuiVision ist der einzige erste
Rollout-Verbraucher. Version 0.2.4 ist bis zur Veroeffentlichung ein Kandidat.

*Validate input and file I/O under SSDF/CWE guidance. Bind release provenance,
MIT license, package hash and tests to the exact commit. No new third-party
library is introduced. AI is development tooling only; no web/cloud runtime or
regulatory product scope is added. Publish only after successful tests and
required review. No bypass or fleet authority is inferred. TuiVision is the
only initial rollout target; 0.2.4 remains a candidate until publication.*
## Lokaler Nachweis / Local Evidence

2026-09-13, macOS: Konfigurationssuite und vorhandene fachliche Regressionen bestanden (Exit 0). Bash und PowerShell wurden ueber ihre oeffentlichen Wrapper geprueft. Native Linux-/Windows-Nachweise werden durch den PR-Workflow gebunden.

*2026-09-13, macOS: configuration and existing domain regression suites passed (exit 0). Both public shell wrappers were exercised. Native Linux/Windows proof is supplied by the PR workflow.*
