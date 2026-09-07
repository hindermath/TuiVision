# v0.1.1: Feldtest-Korrektur / Field-test correction

## Problem und Loesung / Problem and solution

SDA-FT-001 korrigiert erzeugte Validatorpfade fuer vier Agenten.
SDA-FT-002 verwendet deterministische Snapshots aus sortierten relativen
Pfaden und rohen SHA-256-Dateihashes. BOM/CRLF-Normalisierung bleibt allein
dem semantischen Quellenvergleich vorbehalten.

*Fix generated validator paths and make read-only snapshots deterministic.
Keep raw-byte change detection distinct from semantic source normalization.*

## Herkunft und Grenzen / Provenance and boundaries

Kanonische Quelle: `hindermath/home-baseline`, Commit `4fcbc59`, unter
`specs/spec-kit-presets/secure-development-assurance-governance/`.
Alle acht geaenderten Paketdateien stimmen mit der dort unabhaengig
geprueften Quelle ueberein. Der CI-Workflow verwendet dieselben Befehle mit
Paket-Root-Pfaden. Dokumentationsauswirkung: `UpdateRequired`.

*The eight changed package files match the independently reviewed canonical
source. CI uses the same tests with package-root paths. Documentation is
updated with the patch; no evidence schema, API or approval semantics change.*

## Nachweise und Risiken / Evidence and risks

- Lokales macOS: Validatorvertrag, Snapshot-Mutationen, vier Gates in beiden
  Shells und acht generierte Agentenoberflaechen bestanden.
- PowerShell ScriptAnalyzer 1.25.0: keine Findings.
- Native Linux/macOS/Windows-CI muss vor Merge bestehen.
- Generator-Body-Paritaet wird fuer alle acht Oberflaechen geprueft.
- Windows-Testprozesse nutzen den ermittelten absoluten Programmpfad;
  Hidden-Enumeration und Byte-Mutationen werden getrennt geprueft.
- Der normalisierte Bash-Hash entfernt CR, die natives Windows-jq nach der
  fachlichen Normalisierung erneut einfuegt. Rohe Snapshots bleiben bytegenau.
- Alle JSON-Abfragen nutzen binaere Ausgabe, wenn unterstuetzt; altes
  POSIX-jq bleibt ueber eine exakte LF-Probe kompatibel. Fehlerhafte oder
  nicht unterstuetzte native CRLF-Ausgabe blockiert mit klarer Diagnose.
- Keine automatische Erfuellung menschlicher Entscheidungen.
- v0.1.0 bleibt unveraendert; v0.1.1 ist ein Feldtest-Kandidat, noch kein
  bestandener TinyCalc-Feldtest und keine Community-Einreichung.

*Local contracts and regression checks pass. Native CI is a pre-merge gate.
Human decisions stay open. The new immutable patch does not claim general
release acceptance or completion of the two planned TinyCalc field tests.*

*Generated-body parity is checked on all eight surfaces. Test processes use
resolved executable paths; Windows hidden-file enumeration is kept separate
from byte mutations. The runbook records a CLI 0.12.11 multi-agent uninstall
limitation without claiming complete removal or changing the CLI.*

*The normalized Bash hash removes CR bytes reintroduced by native Windows jq
after semantic normalization. Raw read-only snapshots remain byte-exact.*

*Central binary-output routing covers every JSON query, including xargs.
Legacy POSIX LF output remains supported; faulty or unsupported native CRLF
output fails closed. Tests normalize console transport only, not evidence
bytes. The same three CI platforms run once per PR head and on main.*

Der letzte native Stand besteht unter Linux und Windows; die neue Legacy-LF-
Fixture blockiert noch auf macOS. Isolierte Capability-Proben und begrenzte
synthetische Diagnosen ersetzen weder den Ursachenbeleg noch das Plattform-Gate.

*The last native head passes Linux and Windows but remains blocked by the new
legacy-LF fixture on macOS. Probe isolation and bounded synthetic diagnostics
do not substitute for a proven cause or the native platform gate.*

Owner-Freigabe: MergeAndSync, Admin-Bypass nur fuer formale Merge-Regeln,
niemals fuer fehlgeschlagene materielle Gates.

*Owner authority permits formal-rule-only admin bypass, never a technical,
security, accessibility, substantive-review or evidence failure.*
