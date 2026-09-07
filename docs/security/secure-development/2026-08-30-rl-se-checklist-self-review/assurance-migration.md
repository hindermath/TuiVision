# Assurance-Evidence-Migration / Assurance Evidence Migration

## Deutsch

Technische Prüfung: 2026-09-07T21:04:59Z. Kontext: rl-se-checklist-self-review. Ergebnis: **Blocked**.
Der Auftrag erlaubt technische Aufbereitung vorhandener Bewertungen und
Lückenprüfung, keine fachliche Freigabe, Baseline-Korrektur oder Risikoakzeptanz.
Die neuen vier Gate-Nachweise sind explizite **gesperrte Arbeitsstände**.
Sie dokumentieren, was fehlt; ihre Existenz ist kein positiver Prüfnachweis.

### Quellen und Abbildung

- Unveränderte kanonische Quelle: [rl-se-self-review.json](rl-se-self-review.json).
- SHA-256: 2f807cce16dcd0643e01d1e637638758435cee6c962cadf92296ce81af160b49.
- Genau 157 Kontrollbewertungen sind in baseline.json über JSON-Pointer gebunden.
- Vorhandene Zwei-Achsen-Statuswerte werden wörtlich übernommen. TuiVision:
  AlreadySatisfied wird historisch zu Applicable/Fulfilled, Applicable zu
  Applicable/Not Assessed, FollowUp zu Applicable/Not Assessed,
  Open zu Open/Not Assessed und N/A zu N/A/Not Assessed abgebildet.
- sourceStatus hält den Altstatus fest. Das ist keine Neubewertung.
  MIGRATION-FRESHNESS hält die noch fehlende aktuelle Wirksamkeitsprüfung offen.
- Die ursprünglichen Reviewer, Reviewdaten, Rollen, Risiken und Begründungen
  bleiben in der Quelle erhalten. Codex ist nur als heutiger technischer Prüfer
  benannt, ausdrücklich nicht als unabhängiger menschlicher Reviewer.
- reviewDue ist die vom technischen Prüfer gesetzte Wiedervorlage nach sieben
  Tagen, keine fachliche Gültigkeitsverlängerung und kein menschlich zugesagter
  Termin. Quellen-/Scope-Änderungen verlangen sofortige erneute Prüfung.
- development bezeichnet ausschließlich die technische Repository-Arbeit,
  keinen Trainings-, Pilot- oder Produktbetrieb. Ablauf steht in diesem Dokument.

### Tatsächliche Quellenlücken

- Richtlinie_Sichere-Entwicklung.md: VersionDrift, Manifest 3.1.0, Datei / file 3.2.0.
- Checklistensammelband_Sichere-Entwicklung.md: VersionDrift, Manifest 2.1.0, Datei / file 2.2.0.
- checklisten/CL_09_KI-Codeerzeugung.md: VersionDrift, Manifest 2.1.0, Datei / file 2.2.0.
- checklisten/CL_12_Agentische-KI-Sandbox.md: VersionDrift, Manifest 2.1.0, Datei / file 2.2.0.
- mitgeltende-dokumente/Richtlinie_Secure-Development-Life-Cycle.md: VersionDrift, Manifest 1.1.0, Datei / file 1.2.0.
- Checklistensammelband_Sichere-Entwicklung.md: BaselineVersionDrift, Manifest 3.1.0, Datei / file 3.2.0.

Referenzprüfung: HashMatch: 45.
Alle Einzelpfade und Alt-/Ist-Hashes stehen in
[assurance-source-inspection.json](assurance-source-inspection.json).
Dateiexistenz und Hashgleichheit sind keine Wirksamkeitsnachweise. Fragmente,
externe URLs sowie fachliche Gültigkeit wurden nicht dadurch bestätigt.
Manifest und kontrollierte Dokumente bleiben unverändert. Abweichende
Baseline-Versionen werden weder hochgesetzt noch stillschweigend akzeptiert.

### Nächste Schritte und Entscheidungsgrenzen

1. Owner entscheidet über die dokumentierte Manifest-/Dokumentdrift.
2. Historische Kontrollnachweise einschließlich CL-02-13 anhand aktueller
   Quellen und des ursprünglichen Scopes fachlich prüfen; offene Kontrollen
   nicht allein wegen vorhandener Dateien als erfüllt einstufen.
3. Delta und neun Image-Prüfpunkte mit wirklichen kontextspezifischen Nachweisen
   versehen. Not Assessed bedeutet: im heutigen Auftrag nicht erneut geprüft.
   Es widerruft keine historische Prüfung und behauptet kein Image-Problem.
4. technicalValidation, pilotAuthorization, projectAcceptance und generalRelease
   bleiben getrennt Open, bis der jeweilige Nachweis ausdrücklich vorliegt.
   Historische Abnahme eines anderen Scopes wird nicht auf diese Gates übertragen.

### Wiederholbarer Ablauf

Von der Repository-Wurzel zuerst status mit diesem expliziten Verzeichnis
ausführen. Danach review jeweils für baseline, delta, closure und image-impact,
mit Kontext rl-se-checklist-self-review und Betriebsart development. Anschließend erneut status.
Die installierten Skripte stehen unter
.specify/presets/secure-development-assurance-governance/scripts/.
Die vollständigen Befehle, Ergebnisse und Exitcodes werden in
assurance-validation.json dokumentiert. Exit 0 kann bei v0.1.3 nur die
Vertragsprüfung bedeuten: das ausgegebene outcome Blocked bleibt maßgeblich.
Kein pauschaler erfolgreicher Gate- oder Freigabeclaim aus dem Exitcode.

### Dokumentationsauswirkung

UpdateRequired. Owner: Repository-Maintainer. Zielgruppen: Maintainer und
Reviewer. Leserpfad: dieser Bericht, Quelleninspektion, Gate-JSONs, unveränderte
Evidence-Matrix und Altquelle. Quelle ist der Kontext, Produktquelle das
installierte öffentliche Preset v0.1.3. DE zuerst, EN danach; text-first.
Source-only, kein Home-Sync oder Rollout. Keine API-, UI-, Runtime- oder
Produktlogikänderung; DocFX und TDD-Produktänderung hierfür N/A.
Ausnahme zur Dokumentationsarbeit: vollständiger lokaler Release-/Coverage-Test
gemäß TuiVision-Merge-Gate. Dafür ausschließlich Buildzähler 520 auf 521 in den
drei Versionsfeldern von Directory.Build.props; alle anderen Bestandsbytes
bleiben unverändert. Der Bericht assurance-coverage.json dokumentiert den Lauf.
Re-Evaluation bei Evidence-/Scope-Änderung. SSDF/CWE bleiben Bezugsrahmen;
die historische Anwendbarkeit anderer Standards wird nicht neu entschieden.
Keine vollständige C5-Prüfung, Testat- oder Zertifizierungsaussage.

## English

This is a technical, source-bound migration of 157 historical assessments,
not a new effectiveness review. All four gates are explicitly blocked working
records. Historical source bytes, reviewer identities, dates, risks and
decisions remain unchanged. JSON pointers bind each control to its source;
sourceStatus preserves the original state. TuiVision's AlreadySatisfied maps
historically to Applicable/Fulfilled, Applicable and FollowUp to Applicable/
Not Assessed, Open to Open/Not Assessed, and N/A to N/A/Not Assessed.
MIGRATION-FRESHNESS prevents a migration from becoming a positive gate result.

Codex records only the present automated technical inspection. Its reviewDue
is a seven-day technical reinspection limit set by the inspecting agent, not
renewed domain validity or a human commitment. Source/scope changes require
immediate reinspection. The development mode is repository-internal evidence
work only, with no training, pilot or product operation. The source report
lists actual version mismatches and reference existence/hash checks. It does
not verify anchors, external sources or control effectiveness. Baseline sources
and all installed presets stay unchanged.

Next: the owner resolves version drift; review historical source freshness and
scope, including CL-02-13; supply genuine delta and image evidence; record each
technical/pilot/project/general decision separately. All four decisions remain
Open. All nine image fields remain Not Assessed for this inspection, without
revoking earlier results. No human acceptance from another scope is transferred.

From the repository root, run explicit-context status, each of the four gate
reviews with the full context ID and development mode, and status again. The
validation receipt records complete commands, results and exit codes. v0.1.3
may return exit 0 for a structurally valid record with outcome Blocked: this is
not a passed substantive gate. Documentation impact: UpdateRequired, repository
maintainer owned, German-first and text-first source-only evidence. No Home
sync, preset changes, API/UI change or new C5 assurance claim. TuiVision's
mandatory full Release/coverage regression was run locally; only its three
Directory.Build.props version fields advance build counter 520 to 521.
Every other existing byte stays unchanged; see assurance-coverage.json.
Reevaluate when evidence or scope changes.
