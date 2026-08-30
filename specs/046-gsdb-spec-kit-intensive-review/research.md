# Forschung: GSDB-Spec-Kit-Intensivprüfung / Research: GSDB Spec Kit Intensive Review

**Feature**: `046-gsdb-spec-kit-intensive-review`

**Datum / Date**: 2026-08-30
**Status**: Planungsgrundlage, keine Auditfeststellung / Planning basis, not an audit finding

## 1. Entscheidungsrahmen / Decision frame

### Entscheidung / Decision

Die Umsetzung erzeugt einen einmaligen, evidenzbasierten Review-Snapshot. Eine kanonische JSON-Datei ist die einzige Quelle für alle Auswertungen. Maschinenlesbare JSON-Projektionen für Quellen, Kontrollen, Sprachen, Preset/Governance, Evidenzfamilien und Summary sowie lesbare Markdown-Dateien werden deterministisch daraus projiziert. Der Review verändert weder Produktcode noch Laufzeitverhalten, APIs, Abhängigkeiten, Projekte, Beispiele, Workflows, Provider-Einstellungen oder Geheimnisse.

The implementation produces one evidence-based review snapshot. One canonical JSON file is the sole source for every projection. Machine-readable JSON projections for sources, controls, languages, preset/governance, evidence families, and summary, plus human-readable Markdown files, are generated deterministically from it. The review changes no product code, runtime behavior, API, dependency, project, example, workflow, provider setting, or secret.

### Begründung / Rationale

Eine kanonische Quelle verhindert widersprüchliche Zählungen und Aussagen. Die Projektionen bleiben für Menschen, Screenreader, Braillezeilen und Textbrowser prüfbar. Der enge Schreibumfang trennt Prüfung und spätere Abhilfe kausal.

A canonical source prevents conflicting counts and statements. The projections remain reviewable by people, screen readers, Braille displays, and text browsers. The narrow write scope keeps assessment and later remediation causally separate.

### Verworfene Alternativen / Rejected alternatives

- Mehrere unabhängig gepflegte Tabellen: verworfen wegen Drift- und Zählrisiko.
- Ein neues Skript oder Validator-Projekt: verworfen, weil ein vorhandenes MSTest-Projekt die Prüfung ohne neue Projekt- oder Abhängigkeitsfläche aufnehmen kann.
- Übernahme positiver Aussagen aus Feature 016, 044 oder 045: verworfen; diese Artefakte sind nur Evidenzquellen und Muster.
- Sofortige Reparaturen: verworfen; Feststellungen dürfen ausschließlich dokumentierte Folgearbeit auslösen.

## 2. Akzeptierter Eingabesnapshot / Accepted input snapshot

Die folgenden Eingaben sind bindend und müssen beim Implementierungsstart über ihre jeweils vorhandene Bindungsquelle erneut geprüft werden:

The following inputs are binding and must be checked again at implementation start through their respective available binding source:

- Feature-Spezifikation, Klärungsbericht und beide abgeschlossenen Checklisten unter `specs/046-gsdb-spec-kit-intensive-review/`.
- Bindendes Intake `requirements/intakes/active/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md` sowie akzeptierter Review, Manifest und Receipt.
- Projektverfassung `constitution.md` und aktive Spec-Kit-Verfassung `.specify/memory/constitution.md`.
- Aktive Preset-Registry `.specify/presets/.registry` und die zugehörigen installierten Preset-Dateien.
- Vollständiger physischer GSDB-Quellenbestand unter `docs/secure-development/`.
- Relevante Evidenz aus Features 016, 044 und 045.

Intake, Review, Manifest und Receipt werden über `acceptedArtifacts` gebunden. Für Feature-Artefakte gilt die neueste abgeschlossene Routing-Ergebnisdatei: Zuerst muss ihr im Run-State gespeicherter `resultSha256` stimmen, danach ihr `payloadSha256`. Ein selbst als Routing-Payload gebundener Reviewbericht darf die post-remediation Hashes seiner ausdrücklich gelisteten Artefakte attestieren; so bindet `plan-review-1` den korrigierten Plan und seine Designbeilagen, ohne den älteren `plan-1`-Result-Envelope umzuschreiben. Ein älterer Payload derselben Datei, etwa `specify-1` vor `clarify-1`, ist nur Historie. Artefakte ohne Routing-Payload oder Review-Attestation werden mit ihrem aktuellen Hash inventarisiert und durch den späteren exakten Kandidaten-Commit gebunden; der Run-State darf dafür keine nicht vorhandene Hashbindung vortäuschen.

Intake, review, manifest, and receipt are bound through `acceptedArtifacts`. Feature artifacts use the latest completed routing result: first its `resultSha256` recorded in run state must match, then its `payloadSha256`. A review report that is itself a bound routing payload may attest the post-remediation hashes of the artifacts it explicitly lists; this lets `plan-review-1` bind the corrected plan and design companions without rewriting the older `plan-1` result envelope. An older payload for the same file, such as `specify-1` before `clarify-1`, is historical only. Artifacts without a routing payload or review attestation are inventoried with their current hash and bound by the later exact candidate commit; the run state must not be treated as containing a hash it does not contain.

Der beim Planen beobachtete Feature-HEAD ist `fc041d61ab71288cf0c882ecd00a5e019c64405b`; der fokussierte Review läuft danach als `plan-review-1`. Weder Phase noch HEAD sind eine spätere Gate-Abkürzung. Implementierungs- und Liefergates müssen den jeweils aktuellen Run-State und exakten HEAD erfassen.

The feature HEAD observed during planning is `fc041d61ab71288cf0c882ecd00a5e019c64405b`; the focused review then runs as `plan-review-1`. Neither phase nor HEAD is a shortcut for later gates. Implementation and delivery gates must capture the current run state and exact HEAD at that time.

## 3. GSDB-Quelleninventar / GSDB source inventory

### Entscheidung / Decision

Das Quelleninventar wird aus dem physischen Bestand und dem Manifestabschluss gebildet. Jede physische Datei erscheint genau einmal als Quelle; mehrere Manifestrollen werden am selben Datensatz zusammengeführt. Textdateien erhalten einen normalisierten LF-SHA-256, Binärdateien einen Raw-Byte-SHA-256. Der PDF-Hash wird zusätzlich gegen die verwaltete `.sha256`-Datei geprüft.

The source inventory is derived from the physical tree and manifest closure. Every physical file appears exactly once as a source; multiple manifest roles are merged into that record. Text files receive a normalized-LF SHA-256 and binary files a raw-byte SHA-256. The PDF hash is also checked against the managed `.sha256` file.

### Planungsbeobachtung / Planning observation

Der akzeptierte Snapshot enthält derzeit 37 physische Dateien: 34 Markdown-Dateien, eine JSON-Datei, eine PDF-Datei und eine SHA-Datei. Die Zahl 37 ist eine beobachtete Snapshot-Eigenschaft, keine fest codierte Vertragszahl. Der Validator leitet sie bei jeder Prüfung neu ab.

The accepted snapshot currently contains 37 physical files: 34 Markdown files, one JSON file, one PDF file, and one SHA file. The number 37 is an observed snapshot property, not a hard-coded contractual count. The validator derives it anew on every validation.

Das Manifest meldet Baseline `3.1.0`. In Quelldokumenten und zentralen Zuordnungsflächen kommen abweichende Versionsangaben vor. Diese Abweichungen werden als zu prüfende Evidenz erfasst; der Plan erklärt sie weder vorab zum Fehler noch zur Unbedenklichkeit.

The manifest reports baseline `3.1.0`. Source documents and central mapping surfaces contain differing version declarations. These differences are captured as evidence to assess; the plan neither pre-classifies them as defects nor as harmless.

## 4. Kanonische Kontrollmenge / Canonical control set

### Entscheidung / Decision

Nur die zwölf GSDB-Checklistenüberschriften definieren die Kontrollmenge. Sie ergeben exakt 157 eindeutige Kontroll-IDs. Die bindende Kapitelverteilung derselben Kontrollmenge ist `12/13/15/10/13/11/12/13/17/17/12/12`. Alle Nicht-Kontroll-Inventare bleiben dynamisch. Kapitelzahlen werden aus den Kontrollzeilen neu berechnet und gegen diese akzeptierte Kontrollinvariante geprüft.

Only the headings in the twelve GSDB checklists define the control set. They produce exactly 157 unique control IDs. The binding partition of that same control set is `12/13/15/10/13/11/12/13/17/17/12/12`. Every non-control inventory remains dynamic. Chapter counts are recalculated from the control rows and checked against this accepted control invariant.

### Planungsbeobachtung / Planning observation

Die derzeit abgeleiteten Kapitelzahlen sind `12, 13, 15, 10, 13, 11, 12, 13, 17, 17, 12, 12`. Ihre Summe ist 157. Der Validator berechnet sie unabhängig aus den Quellzeilen und vergleicht sie mit der akzeptierten Verteilung; eine Umverteilung bei weiterhin 157 Zeilen schlägt geschlossen fehl.

The currently derived chapter counts are `12, 13, 15, 10, 13, 11, 12, 13, 17, 17, 12, 12`. Their sum is 157. The validator independently recalculates them from source rows and compares them with the accepted partition; a redistribution that still totals 157 fails closed.

Jede Kontrolle erhält genau eine zulässige Disposition und vollständige Evidenzfelder. Positive Dispositionen wie `Applicable` oder `AlreadySatisfied` benötigen aktuelle, direkt nachvollziehbare Evidenz. Aussagen aus Feature 016 werden neu geprüft, nicht übernommen.

Every control receives exactly one allowed disposition and complete evidence fields. Positive dispositions such as `Applicable` or `AlreadySatisfied` require current, directly traceable evidence. Statements from Feature 016 are reassessed, not copied.

## 5. Sprachabdeckung / Language coverage

### Entscheidung / Decision

Sprachprofile werden aus drei Quellen vereinigt: explizite GSDB-Sprachregeln, Verfassungs-/Preset-Anforderungen und tatsächlich versionierte Repository-Dateien. Profile unterscheiden `Active`, `ReadOnlyHistorical` und `AbsentRuleProfile`. Die Anzahl wird aus dieser Vereinigung abgeleitet.

Language profiles are the union of explicit GSDB language rules, constitution/preset obligations, and actually tracked repository files. Profiles distinguish `Active`, `ReadOnlyHistorical`, and `AbsentRuleProfile`. Their count is derived from that union.

Historische C/C++-Bestände bleiben schreibgeschützt und sind für die normale GSDB-Prüfung `N/A`. Eine begrenzte Einsicht ist nur erlaubt, wenn eine konkrete Kontrollfrage ohne sie nicht beantwortet werden kann; Pfad, Frage und Ergebnis müssen dann protokolliert werden. SQL und andere nicht aktive Regelprofile bleiben sichtbar, damit eine fehlende Anwendung nicht mit fehlender Prüfung verwechselt wird.

Historical C/C++ trees remain read-only and are `N/A` for the normal GSDB review. A bounded consultation is allowed only if a concrete control question cannot be answered otherwise; path, question, and result must then be recorded. SQL and other inactive rule profiles stay visible so non-application is not confused with omission.

## 6. Preset- und Governance-Inventar / Preset and governance inventory

### Entscheidung / Decision

Die Presetliste wird ausschließlich aus allen aktivierten Einträgen in `.specify/presets/.registry` abgeleitet. Für jeden Eintrag werden ID, installierte Version, Priorität, Pfad, Registry-Hash, zugehörige Planpflichten, Agenten-Parität und aktuelle Disposition gespeichert. Es gibt keine hart codierte Presetanzahl.

The preset list is derived exclusively from all enabled entries in `.specify/presets/.registry`. Each record stores ID, installed version, priority, path, registry hash, related planning obligations, agent parity, and current disposition. There is no hard-coded preset count.

Agenten-Parität erhält ein eigenes, referenziell geschlossenes Inventar. Es vereinigt den TuiVision-Level-2-Eintrag, Agentenschlüssel der aktivierten Registry und tatsächlich versionierte Guidance-, Command-, Prompt-, Skill- und Agent-Definitionen. Nicht vorhandene persönliche Junie- oder andere Agentenzustände werden weder erfunden noch gelesen. Anzahl, Agentenfamilien und Flächentypen werden aus dem Snapshot abgeleitet.

Agent parity has its own referentially closed inventory. It unites the TuiVision Level-2 entry, agent keys from the enabled registry, and actually tracked guidance, command, prompt, skill, and agent definitions. Absent personal Junie or other agent state is neither invented nor read. Count, agent families, and surface types are derived from the snapshot.

### Planungsbeobachtung / Planning observation

Die aktuelle Registry enthält 12 aktivierte Presets:

The current registry contains 12 enabled presets:

| Priorität / Priority | Preset | Installierte Version / Installed version |
|---:|---|---|
| 10 | `security-governance` | `0.6.2` |
| 20 | `architecture-governance` | `0.5.2` |
| 30 | `isaqb-architecture-governance` | `0.2.2` |
| 40 | `a11y-governance` | `0.4.3` |
| 50 | `cross-platform-governance` | `0.2.2` |
| 60 | `agent-parity-governance` | `0.4.2` |
| 61 | `model-routing-governance` | `0.1.4` |
| 64 | `intake-authoring-governance` | `0.3.1` |
| 65 | `intake-review-governance` | `0.2.1` |
| 66 | `intake-sequencing-governance` | `0.2.3` |
| 70 | `autonomous-run-governance` | `0.4.1` |
| 80 | `parallel-autonomous-run-governance` | `0.2.6` |

Diese 12 sind eine Planungsbeobachtung. Die Implementierung muss die Registry erneut lesen, die Anzahl ableiten und jede Abweichung sichtbar machen. Die ältere Presetliste in `constitution.md` und die aktuellere Liste in `.specify/memory/constitution.md` werden als getrennte Governance-Evidenz bewertet, nicht stillschweigend harmonisiert.

These 12 are a planning observation. Implementation must reread the registry, derive the count, and expose any difference. The older preset list in `constitution.md` and the newer list in `.specify/memory/constitution.md` are assessed as separate governance evidence and are not silently reconciled.

## 7. Evidenzfamilien / Evidence families

### Entscheidung / Decision

Evidenzfamilien verwenden deklarative Selektoren und speichern die daraus abgeleiteten sortierten Treffer, Zählungen und Hashes. Der akzeptierte Pflichtdomänenkatalog umfasst Security, Architektur, Barrierefreiheit, Feature-Evidenz 016/044/045, Workflows und Lieferkette, Tests und Coverage, Dokumentationspipeline, Governance/Agenten, Intake/Autonomie sowie Repository-Konfiguration. Zusätzliche aktive Preset- oder Governance-Pflichten erweitern die Menge deterministisch; die Familienanzahl ist nicht fest.

Evidence families use declarative selectors and store the derived sorted matches, counts, and hashes. The accepted mandatory domain catalog covers security, architecture, accessibility, Feature 016/044/045 evidence, workflows and supply chain, tests and coverage, the documentation pipeline, governance/agents, intake/autonomy, and repository configuration. Additional active preset or governance obligations extend the set deterministically; the family count is not fixed.

Selektoren dürfen sich überlappen; jede Evidenzreferenz bleibt pfadgenau. Eine Familie ist kein Beweis für eine positive Kontrollentscheidung. Sie ist nur ein überprüfbarer Such- und Referenzrahmen.

Selectors may overlap; every evidence reference remains path-specific. A family is not proof of a positive control decision. It is only a reviewable search and reference frame.

## 8. Wiederverwendung aus 016, 044 und 045 / Reuse from 016, 044, and 045

| Quelle / Source | Wiederverwendbares Muster / Reusable pattern | Verbotene Übernahme / Forbidden carry-over |
|---|---|---|
| Feature 016 | 157er Kontrollmatrix, Eigentümer-/Risiko-/Evidenzfelder | Kontrollstatus, offene Punkte oder Erfüllungsbehauptungen |
| Feature 044 | Trennung statischer, praktischer und Plattform-/Provider-Evidenz | `ConditionallyUsable` oder andere positive Sandbox-Aussagen |
| Feature 045 | Kanonisches JSON, deterministische Projektionen, LF-/Raw-Hashing, test-only MSTest-Validator, exact-head-Gates | Dispositionen, Bestandsurteile, Promotion oder Abschlussfolgerungen |

Jede positive Aussage wird aus dem Feature-046-Snapshot neu begründet. Frühere Aussagen dürfen als zu prüfende Evidenz verlinkt werden, aber nicht als Entscheidungsvoreinstellung dienen.

Every positive statement is newly justified from the Feature 046 snapshot. Earlier statements may be linked as evidence to assess but cannot pre-seed a decision.

## 9. Validatoroberfläche / Validator surface

### Entscheidung / Decision

Der Validator wird als neue test-only C#-Datei im bestehenden Projekt `tests/TuiVision.Drivers.Tests` geplant, mit Fixtures in einem zugehörigen Unterordner. Er verwendet vorhandenes MSTest und `System.Text.Json`. Es werden weder `.csproj` noch Pakete noch neue Projekte oder Skripte geändert.

The validator is planned as a new test-only C# file in the existing `tests/TuiVision.Drivers.Tests` project, with fixtures in a related subdirectory. It uses existing MSTest and `System.Text.Json`. No `.csproj`, package, new project, or script is changed.

Der vorhandene Feature-045-Validator ist ein Strukturmuster. Feature 046 erhält eine eigene Validator-Datei und eigene Fixtures, damit Datensätze, Regeln und Fehlermeldungen unabhängig bleiben. Der Validator prüft Schema, Eindeutigkeit, Sortierung, Referenzintegrität, 157 Kontrollen, abgeleitete Summen, Quellabschluss, Registry-Abgleich, Projektionstreue, Hashing und Fail-closed-Verhalten.

The existing Feature 045 validator is a structural pattern. Feature 046 receives its own validator file and fixtures so datasets, rules, and diagnostics remain independent. The validator checks schema, uniqueness, ordering, referential integrity, 157 controls, derived summaries, source closure, registry reconciliation, projection fidelity, hashing, and fail-closed behavior.

## 10. Dokumentation und Barrierefreiheit / Documentation and accessibility

Alle neuen menschlich lesbaren Artefakte sind Deutsch zuerst und Englisch danach auf CEFR-B2-Niveau. Tabellen benötigen semantische Überschriften und dürfen keine Bedeutung nur über Farbe oder Layout vermitteln. Jeder maschinenlesbare Code hat eine ausgeschriebene Textbezeichnung. Die Reader-Route beginnt bei `docs/security/README.md` und führt zum datierten Review-Verzeichnis.

All new human-readable artifacts are German first and English second at CEFR-B2 level. Tables require semantic headers and may not convey meaning by color or layout alone. Every machine-readable code has an expanded text label. The reader route starts at `docs/security/README.md` and leads to the dated review directory.

Da das geplante Verzeichnis unter der dokumentierten Sicherheitsroute liegt, werden DocFX-Erzeugung sowie Playwright/axe und der Textbrowser-Smoke im Implementierungslauf als voraussichtlich anwendbar geplant. Falls der tatsächliche Delivery-Diff nachweislich keine DocFX-Eingabe berührt, darf die Bedingung als `N/A` mit maschinenlesbarer Begründung enden. Öffentliche API/XML-Änderungen sind unzulässig und würden den Lauf stoppen.

Because the planned directory is under the documented security route, DocFX generation, Playwright/axe, and text-browser smoke are planned as expected applicable during implementation. If the actual delivery diff demonstrably touches no DocFX input, the condition may end as `N/A` with a machine-readable rationale. Public API/XML changes are prohibited and stop the run.

## 11. Direkte und externe Evidenzgrenzen / Direct and external evidence boundaries

| Klasse / Class | Zulässige Aussage / Allowed claim | Grenze / Boundary |
|---|---|---|
| `LocalDirect` | Dateiinhalt, Hash, lokaler Test, Coverage, Formatierung, lokaler DocFX/A11Y-Lauf | Beweist keine Remote- oder Provider-Eigenschaft |
| `RemoteObserved` | Exakter Commit, Check-Run, Merge-Status oder synchronisierter Branch | Nur durch aktuelle Hosting-Provider-Evidenz |
| `HumanApproval` | Enger, ausdrücklich dokumentierter Ausnahmeentscheid | Keine technische Gate-Ersetzung vor grünem lokalen Zustand |
| `ProviderBoundary` | Organisation, Schutzregel, Secret Store, externe Richtlinie | Nur beobachten/dokumentieren; keine Provider-Schreiboperation |
| `LegalOrganizational` | Rechts-, Datenschutz-, Betriebs- oder Freigabeentscheidung | Als menschliche Grenze dokumentieren, nicht technisch behaupten |

Ein Human-Approval-Bypass darf später nur für genau ein nachweislich nicht verfügbares Remote-Gate nach vollständigem lokalem technischen Grün, null umsetzbaren technischen Befunden, null actionable Review-Threads, null Scope-Verstößen und Human Approval als einziger offener Regel erwogen werden. Gate, autorisierte Person, Zeitstempel, Begründung, Evidence-Grenze und Ablaufzeitpunkt sind Pflicht. Diese Planphase autorisiert keinen Bypass.

A Human-Approval bypass may later be considered only for one demonstrably unavailable remote gate after complete local technical green status, zero actionable technical findings, zero actionable review threads, zero scope violations, and Human Approval as the sole open rule. Gate, authorized person, timestamp, rationale, evidence boundary, and expiry are mandatory. This planning phase authorizes no bypass.

## 12. Lieferung und Kausalität / Delivery and causality

Der akzeptierte Liefermodus ist `MergeAndSync`. Implementierung, Commit, Push, Pull Request, Merge, Synchronisierung, Provider-Schreibzugriff und Intake-Übergang gehören nicht zu dieser Planphase. Der spätere Implementierungslauf muss Pre-Merge- und Post-Merge-Evidenz an exakten HEADs prüfen. Änderungen an gemeinsam beschriebenen Dateien werden serialisiert.

The accepted delivery mode is `MergeAndSync`. Implementation, commit, push, pull request, merge, synchronization, provider writes, and intake transition are outside this planning phase. The later implementation run must validate pre-merge and post-merge evidence at exact HEADs. Writes to shared files are serialized.

Merge-abhängige Fakten wie finaler Check-Status, tatsächlicher Merge-Commit, synchronisierter Hauptbranch, Intake-Archivierung, Serienübergang, endgültige Statistik und Retrospektive werden erst kausal danach geschrieben. Wenn dafür ein Closeout-Änderungssatz nötig ist, bleibt er evidenz-only und enthält keine Produkt- oder Reparaturänderung.

Merge-dependent facts such as final check status, actual merge commit, synchronized main branch, intake archiving, series transition, final statistics, and retrospective are written only causally afterward. If a closeout change set is required, it remains evidence-only and contains no product or remediation change.

## 13. Verbleibende Unsicherheiten / Remaining uncertainties

Es gibt keine materielle Planungsunklarheit. Welche Kontroll-, Sprach-, Preset-, Governance- oder Evidenzdispositionen gelten, ist bewusst noch offen und wird erst während der evidenzbasierten Umsetzung entschieden. Abweichungen, die Produktänderungen oder andere verbotene Maßnahmen verlangen, werden als dokumentierte Folgearbeit festgehalten und blockieren keine ehrliche Snapshot-Aussage; sie werden in Feature 046 nicht behoben.

There is no material planning ambiguity. The applicable control, language, preset, governance, and evidence dispositions intentionally remain open until evidence-based implementation. Differences that require product changes or another prohibited action are recorded as documented follow-up work and do not block an honest snapshot statement; they are not repaired in Feature 046.
