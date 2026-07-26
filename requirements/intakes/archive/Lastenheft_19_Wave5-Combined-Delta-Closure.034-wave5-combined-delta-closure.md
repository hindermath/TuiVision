# Lastenheft 19: Wave-5 Combined Delta Closure

**Dokumentstatus:** Verbindliche Spec-Kit-Eingabedatei für Feature 034
**Vorgesehene Feature-Nummer:** 034
**Vorgesehener Branch:** `034-wave5-combined-delta-closure`
**Liefermodus:** `MergeAndSync`
**Verbindliche Reihenfolge:** nach den vollständig gemergten Features 032 und
033, vor Wave 6
**Audittyp:** read-only Produktdelta- und Evidence-Abschluss
**Historische Quelle:** `TVDEMOS/`, read-only
**Externe Vergleichsquellen:** nur bei einer neuen reproduzierbaren
Wave-5-Frage, read-only

*This is the binding Spec Kit intake for Feature 034. It independently reviews
the combined product delta delivered by Features 032 and 033 before Wave 6.
The audit does not remediate product code. It may add feature evidence and a
deterministic test-only closure validator.*

---

## 0. Ziel / Goal

Feature 034 prüft, ob die funktionale Wave-5-Portierung aus Feature 032 und die
Showcase-Remediation aus Feature 033 gemeinsam einen konsistenten,
nachvollziehbaren und wartbaren Abschluss bilden.

Der Audit rekonstruiert das tatsächliche Produktdelta aus den beiden
Feature-PRs. Er prüft pro Beispiel historische Absicht, moderne C#-Funktion,
sichtbare Interaktion, Framework-Nutzung, Proof-Qualität, Dokumentation, A11Y,
Plattform- und Sicherheitsgrenzen.

*Feature 034 verifies that the functional Wave-5 port and the subsequent
showcase remediation form one coherent, maintainable, and reviewable delivery.
It reconstructs the actual product delta and does not infer acceptance from
the current repository state alone.*

## 1. Verbindliche Provenienz / Binding Provenance

Der Produktdelta wird ausschließlich aus diesen beiden Lieferungen gebildet:

| Stufe | PR | Basis | Geprüfter Head | Merge |
|---|---:|---|---|---|
| Feature 032, funktionale Portierung | #93 | `269c54f5f882c69e21f46f97d3e89a938bfb568f` | `cf274c61968fdc5422d3c1cf16ed5488ad5d37ad` | `e74c33d256ebbf2cf8e6a78f2548ee6e3f6cf3d6` |
| Feature 033, Showcase-Remediation | #96 | `5df2ec3cef5dd0e534abe630a8d472e0e5bc0236` | `8921bd3f9e354b38835528442f950f53c9d925f0` | `d476e63ccfc053a9a2be1a51eb6d43a875c57384` |

PR #94 und PR #97 enthalten kausale Abschluss-Evidence. PR #95 standardisiert
kopierbare Intake-Prompts und ist kein Bestandteil des Produktdeltas.

Ein pauschaler Diff von der Feature-032-Basis bis zum aktuellen `main` ist
nicht autoritativ, weil er Abschluss-, Prompt- und Governance-Änderungen
vermischen würde. Maßgeblich sind die exakten Dateilisten und geprüften Heads
der PRs #93 und #96.

*The authoritative product delta is the union of the exact reviewed file sets
from PRs #93 and #96. Closeout and prompt-only changes are supporting evidence,
not product changes.*

## 2. Verbindlicher Umfang / Binding Scope

Der Audit umfasst genau:

- 15 historische `TVDEMOS/*.PAS`-Quellen und ihre Feature-032-Rollen;
- sechs Consumer-Gruppen `W5-001` bis `W5-006`;
- zehn startbare `Tp7*`-Beispiele;
- zehn funktionale Primary-Proof-Zeilen aus Feature 032;
- zehn Showcase-Delta-zu-Abschluss-Zuordnungen aus Feature 033;
- zehn Guides, Startpfade und sichtbare Beschreibungen;
- die gemeinsame Wave-5-Beispielassembly und ihre lokale Sonderlogik;
- die vollständigen lokalen, Remote- und Exact-Head-Gates.

Die zehn Beispiele sind `Tp7Demo`, `Tp7Edit`, `Tp7Help`,
`Tp7ResourceDemo`, `Tp7ResourceGenerator`, `Tp7AsciiTable`,
`Tp7Calculator`, `Tp7Calendar`, `Tp7Puzzle` und `Tp7MouseDialog`.

*The audit covers exactly 15 historical sources, six consumer groups, ten
examples, ten functional proof rows, ten showcase closures, ten guides, and
the shared Wave-5 composition.*

## 3. Harte Grenzen / Hard Boundaries

- Keine Änderung von Runtime- oder öffentlichem Verhalten.
- Keine Änderung öffentlicher APIs oder XML-Verträge.
- Keine Dependency-, Paket-, Projekt- oder Solution-Erweiterung.
- Keine Änderung an Beispiel- oder Framework-Produktcode.
- Keine erneute Pascal-Portierung und keine breite Framework-Revision.
- Keine Änderung unter `TVDEMOS/`, `TVFM/`, `tv203s/` oder externen Checkouts.
- Keine automatische Behebung eines Findings innerhalb von Feature 034.
- Kein Start von Wave 6 oder des Post-Wave-6-Portfolio-Audits.
- Kein neues Preset-Release ohne reproduzierbaren providerneutralen Defekt.

Erlaubt sind Feature-Artefakte, Evidence, Status- und Reihenfolgedokumente sowie
ein deterministischer test-only Closure-Validator im bestehenden
Smoke-Test-Projekt.

*Product and historical sources remain unchanged. The only executable addition
allowed is deterministic test-only closure validation.*

## 4. Entscheidungsmodell / Decision Model

Jede kombinierte Beispielzeile erhält genau eine Hauptentscheidung:

- `AcceptedAsIs`
- `AcceptedIntentionalDeviation`
- `CandidateFinding`
- `ProductDecision`

Jede Prüfdimension verwendet genau einen dieser Werte:

- `Pass`
- `IntentionalDeviation`
- `Gap`
- `N/A`

Ein `AcceptedAsIs` darf kein `Gap` enthalten. Ein
`AcceptedIntentionalDeviation` benötigt die historische Absicht, die moderne
C#-Begründung, sichtbare Auswirkung, Restrisiko und einen
Wiederbewertungsauslöser.

Ein `CandidateFinding` benötigt eine reproduzierbare Lücke, Evidence, Owner und
Finding-ID. Eine `ProductDecision` stoppt den Lauf. Unterschiede im
Quelltextstil gegenüber Pascal sind allein kein Finding.

*One primary disposition applies to each example. Dimension values remain
separate, and source-style differences alone cannot create a finding.*

## 5. Kombinierte Example-Matrix / Combined Example Matrix

Die Evidence enthält genau zehn Zeilen mit mindestens diesen Feldern:

`ExampleId`, `HistoricalSourceIds`, `ConsumerId`, `Feature032Proof`,
`Feature033Proof`, `CurrentEntryPoint`, `VisibleFirstScreen`,
`PrimaryInteractionPaths`, `FrameworkComponents`, `LocalSpecialLogic`,
`FrameworkDecision`, `BehaviorStatus`, `InteractionStatus`, `LayoutStatus`,
`ProofStatus`, `DocumentationStatus`, `A11YStatus`, `PlatformStatus`,
`SafetyBoundaryStatus`, `PrimaryDisposition`, `FindingIds`, `EvidencePath`,
`Owner`, `Reviewer`, `ReviewDate`, `ResidualRisk`, `ReevaluationTrigger`.

Zusätzliche exakte Matrizen prüfen:

1. 15 Quellenrollen;
2. sechs Consumer-Zuordnungen;
3. zehn funktionale Proof-Zeilen;
4. zehn Showcase-Delta-zu-Abschluss-Zuordnungen;
5. zehn Guide-, Launch- und Bedienpfade.

Fehlende, doppelte oder unbekannte Beziehungen blockieren den Abschluss.

*Every required matrix has exact cardinality. Missing, duplicate, or unknown
relations fail closed.*

## 6. Framework-Usage- und Sonderlogik-Gate

Der Audit prüft besonders:

- `Wave5Application`
- `Wave5ConsoleHost`
- `Wave5StatusLine`
- `Wave5GridView`
- die öffentlichen Beispielanwendungen und ihre Zustandsmodelle

Gemeinsame reine Beispielkomposition darf unter `examples/Shared/` bleiben.
Logik, die Framework-Verhalten ersetzt oder mehreren unabhängigen
Beispielwellen nutzen würde, muss als reproduzierbares Finding bewertet
werden. Feature 034 verschiebt diese Logik nicht.

Pro Beispiel bleibt die vorhandene Framework-Entscheidung
`UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder
`FollowUpHardening` nachvollziehbar. Der Audit erfindet keine neue
Framework-Entscheidung ohne belegte Abweichung.

*Shared pedagogical composition may remain example-local. Reusable behavior
that replaces a framework contract becomes a finding, not an audit-time fix.*

## 7. Funktions- und Interaktionsabgleich

Für jedes Beispiel muss der Feature-032-Kernfluss über die Feature-033-
Oberfläche weiterhin erreichbar und beweisbar sein:

- normaler Release-Start;
- sichtbarer Zweck im ersten Frame;
- mindestens ein primärer Tastatur- oder Command-Pfad;
- nachvollziehbarer Fokus- und Statuswechsel;
- `F1` beziehungsweise `Help -> Description`;
- kontrollierte Beendigung über `Ctrl+Q`;
- vorhandener constrained-layout Proof;
- negative oder Fallback-Grenze, soweit für das Beispiel relevant.

Primäre Evidence führt `app.Run()` oder den gleichwertigen realen
Anwendungsloop aus. Direkte Helfer bleiben `SetupOnly` oder
`SupplementalProof`.

*Functional behavior and showcase interaction are reviewed as one flow through
the real application loop.*

## 8. Historische und externe Quellen / Historical and External Sources

Die 15 `TVDEMOS/*.PAS`-Dateien bleiben die historische Absichtsreferenz. Ihre
Git-Blobs müssen zwischen dem Feature-032-Merge und dem Audit-Head unverändert
sein.

Free Vision, Terminal.GUI v1.9.0 und `magiblot/tvision` wurden durch Features
029 bis 031 bereits gepinnt und geprüft. Feature 034 öffnet diese Vergleiche
nicht erneut. Eine gezielte read-only Konsultation ist nur zulässig, wenn eine
konkrete Wave-5-Beobachtung eine neue reproduzierbare Frage aufwirft.

*Historical sources remain authoritative for intent. Previously completed
external comparisons are not repeated without a new concrete question.*

## 9. Test-only Closure-Validator

Der Validator prüft mindestens:

- exakte Cardinality aller fünf Matrizen;
- eindeutige IDs und Beziehungen;
- ausschließlich erlaubte Dimensions- und Dispositionswerte;
- kein `Gap` ohne Finding-ID, Evidence und Owner;
- keine akzeptierte Zeile mit offenem `Gap`;
- keine `ProductDecision` als stiller Erfolg;
- exakte PR-Basis-, Head- und Merge-Pins;
- unveränderte 15 historischen Quellblobs;
- vorhandene Beispiel-, Guide-, Projekt- und Proof-Pfade;
- LF-/CRLF-stabile Auswertung.

Negative Fixtures decken fehlende, doppelte, unbekannte, widersprüchliche und
verfrühte Abschlussdaten ab. Jeder erwartete Fehler bleibt einzeln benannt.

*The validator rejects incomplete, duplicate, unknown, contradictory, drifted,
or prematurely accepted closure data.*

## 10. Governance und Evidence

Vor der ersten test-only Änderung entstehen `pr-evidence.md`,
`autonomous-run-state.json` und `autonomous-gate-requirements.json`.

Die Evidence dokumentiert alle sieben installierten Presets:

- `security-governance` v0.6.0;
- `architecture-governance` v0.5.0;
- `isaqb-architecture-governance` v0.2.0;
- `a11y-governance` v0.4.0;
- `cross-platform-governance` v0.2.0;
- `agent-parity-governance` v0.3.0;
- `autonomous-run-governance` v0.2.2.

Nicht ausgelöste Security-, Supply-Chain-, Cloud-, Regulierungs-,
Cross-Platform-Skript- oder API-Gates erhalten ein begründetes `N/A` mit
Wiederbewertungsauslöser. Alle Agent-Kontexte werden gemeinsam geprüft.

*Governance decisions remain explicit, evidence-backed, and trigger-based.*

## 11. Validierung / Validation

Erforderlich sind:

1. `specify check` und vollständige Spec-Kit-Konvergenz;
2. gezielte Closure-Validator- und Negativtests;
3. alle relevanten Wave-5-Smoke-Suites;
4. zehn kontrollierte `--smoke`-Starts;
5. zehn normale PTY-Starts mit erstem Frame, primärer Aktion, `F1` und
   `Ctrl+Q`;
6. `git diff --check`;
7. `dotnet format TuiVision.sln --verify-no-changes`;
8. vollständige Release-Tests;
9. kanonisches Coverlet-Gate für fünf Framework-Assemblies;
10. `docfx docfx.json` und Playwright/Axe;
11. UTF-8- und Text-first-Prüfung;
12. Secret-, Supply-Chain- und Agent-Paritätsprüfung;
13. Linux-, macOS- und Windows-Gates;
14. temporäre Exact-Head-Evidence unmittelbar vor Merge.

Vor jedem einzelnen `dotnet build` oder `dotnet test` wird der manuelle
Build-Zähler genau einmal erhöht.

*Validation covers deterministic closure proof, all ten applications, full
repository gates, three platforms, and exact reviewed-head evidence.*

## 12. Ergebnisregeln / Outcome Rules

### 12.1 Sauberer Abschluss

Bei null `CandidateFinding` und null `ProductDecision`:

- wird Wave 5 als `Closed` markiert;
- wird Wave 6 zu `EligibleForIntake`;
- entsteht `Lastenheft_20_Wave6-TVFM-Functional-Porting.md`;
- wird Feature 035 reserviert, aber nicht gestartet;
- wird eine spätere Showcase-Stufe erst aus tatsächlichen Feature-035-Deltas
  abgeleitet.

### 12.2 Candidate Findings

Reproduzierbare Findings erhalten stabile IDs `W5D001` fortlaufend. Sie werden
nach tatsächlicher Ownership dedupliziert. Nur nicht leere
Hardening-Lastenhefte werden erzeugt. Wave 6 bleibt blockiert und ihre spätere
Feature-Nummer verschiebt sich entsprechend.

### 12.3 Product Decision

Eine `ProductDecision` stoppt den autonomen Lauf. Es entsteht weder eine
automatische Remediation noch ein Wave-6-Intake.

*A clean result closes Wave 5 and authorizes only the Wave-6 intake. Findings
produce non-empty owner work; product decisions stop the run.*

## 13. Delivery und Retrospektive

Der Feature-PR wird erst nach vollständiger lokaler Validierung veröffentlicht.
Pflichtchecks, Reviews und actionable Threads müssen konvergieren. Ein enger
Admin-Bypass ist nur zulässig, wenn alle technischen Gates grün sind, null
umsetzbare Threads offen sind und ausschließlich Human Approval blockiert.

Ein kausaler Evidence-Closeout-PR entsteht nur, wenn Post-Merge-Fakten nicht
wahrheitsgemäß im geprüften Feature-Head stehen konnten. Er bleibt
evidence-only und nicht rekursiv.

Preset-Promotion erfolgt nur bei einem reproduzierbaren providerneutralen
Defekt. Andernfalls lautet die Retrospektive `NoPromotion`, ohne Leerbranch
oder Leer-PR.

*Delivery uses reviewed exact-head evidence, a non-recursive causal closeout
only when required, and no artificial preset release.*

## 14. Abnahmekriterien / Acceptance Criteria

Feature 034 ist nur abgeschlossen, wenn:

1. alle exakten Provenienz-Pins stimmen;
2. 15 Quellen, sechs Consumer und zehn Beispiele vollständig zugeordnet sind;
3. alle zehn kombinierten Zeilen genau eine zulässige Hauptentscheidung haben;
4. jeder offene `Gap` ein reproduzierbares Finding besitzt;
5. Funktion, Showcase, Framework-Nutzung, Proof, Guide, A11Y und Plattform
   gemeinsam geprüft sind;
6. historische und externe Quellen unverändert bleiben;
7. Produkt- und Beispielcode unverändert bleiben;
8. alle lokalen und Remote-Gates konvergiert sind;
9. Wave 5 genau eine belegte Abschlussentscheidung erhält;
10. Wave 6 nicht automatisch gestartet wird.

*Completion requires exact provenance, complete cardinalities, one disposition
per example, no unevidenced gap, unchanged product sources, converged gates,
and no automatic Wave-6 start.*

## 15. Optimaler Specify-Prompt / Recommended Specify Prompt

```text
$speckit-specify Use
`Lastenheft_19_Wave5-Combined-Delta-Closure.md` as the binding intake for
Feature 034.

Create exactly `specs/034-wave5-combined-delta-closure` on branch
`034-wave5-combined-delta-closure`. Do not create Feature 035 or start Wave 6.

Specify a read-only combined delta audit for the exact reviewed product file
sets from PR #93 at head cf274c61968fdc5422d3c1cf16ed5488ad5d37ad and
PR #96 at head 8921bd3f9e354b38835528442f950f53c9d925f0. Treat PRs #94 and
#97 as closeout evidence and PR #95 as non-product metadata.

Require exact coverage of 15 TVDEMOS sources, six W5 consumer groups, ten
Tp7 examples, ten functional proof rows, ten showcase closures, and ten
guide/launch paths. Give every example exactly one AcceptedAsIs,
AcceptedIntentionalDeviation, CandidateFinding, or ProductDecision
disposition and separate Pass, IntentionalDeviation, Gap, or N/A dimension
statuses.

Allow only feature artifacts, evidence, status updates, and deterministic
test-only closure validation. Do not change runtime behavior, APIs,
dependencies, projects, examples, framework code, historical sources, or
external comparison sources. Do not remediate findings inside the audit.

If the verified result has zero CandidateFinding and zero ProductDecision,
close Wave 5 and derive Lastenheft 20 for a later Feature-035 Wave-6
functional intake without starting it. Otherwise create only non-empty,
finding-derived owner intakes or stop on ProductDecision.
```

## 16. Optimaler Autonomous-Prompt / Recommended Autonomous Prompt

```text
$speckit-autonomous Execute the complete autonomous Spec Kit run for Feature
034 using `Lastenheft_19_Wave5-Combined-Delta-Closure.md` as the binding
intake. Delivery mode: MergeAndSync.

Start from clean synchronized main after the Lastenheft-19 intake PR, Feature
032 plus closeout, and Feature 033 plus closeout are fully merged. Create
exactly branch `034-wave5-combined-delta-closure` and feature directory
`specs/034-wave5-combined-delta-closure`. Do not create Feature 035, start
Wave 6, or start the post-Wave-6 portfolio audit during this run.

Run Specify, repeated Clarify, all useful provenance, matrix, framework-usage,
proof, security, A11Y, and closure checklists, Plan, plan-review remediation,
Tasks, repeated Analyze, Implement, validation, delivery, and retrospective
to convergence. Create run state, gate requirements, and pr-evidence before
the first test-only implementation change.

Reconstruct the authoritative product delta only from PR #93 head
cf274c61968fdc5422d3c1cf16ed5488ad5d37ad and PR #96 head
8921bd3f9e354b38835528442f950f53c9d925f0. Keep PRs #94 and #97 as
closeout evidence and exclude PR #95 from the product delta.

Audit exactly 15 historical source roles, six consumer groups, ten examples,
ten functional proofs, ten showcase closures, and ten guide/launch paths.
Give every example exactly one AcceptedAsIs, AcceptedIntentionalDeviation,
CandidateFinding, or ProductDecision disposition. Use only Pass,
IntentionalDeviation, Gap, or N/A for dimensions.

Review Wave5Application, Wave5ConsoleHost, Wave5StatusLine, Wave5GridView,
and all example-local special logic for improper framework duplication.
Differences from Pascal source style alone are not findings. Reopen Free
Vision, Terminal.GUI, or magiblot evidence only for a new reproducible
Wave-5 question.

Do not change runtime behavior, APIs, dependencies, packages, projects,
example or framework product code, TVDEMOS, TVFM, tv203s, or external
checkouts. Permit only feature artifacts, evidence, required status and
guidance updates, and a deterministic test-only closure validator. Do not
remediate findings inside Feature 034.

Validate targeted positive and negative closure tests, all relevant Wave-5
smokes, all ten controlled --smoke starts, ten normal PTY interaction paths,
full Release tests, canonical five-assembly coverage, formatting, DocFX,
Playwright/Axe, text-first UTF-8, Linux/macOS/Windows, agent parity, secrets,
supply chain, reviews, and temporary exact-head evidence. Increment the
manual build counter before every individual dotnet build or dotnet test.

If zero CandidateFinding and zero ProductDecision remain, mark Wave 5 Closed,
mark Wave 6 EligibleForIntake, and create
Lastenheft_20_Wave6-TVFM-Functional-Porting.md for a later Feature 035 without
starting it. If CandidateFindings remain, create only deduplicated non-empty
owner intakes and keep Wave 6 blocked. Stop on ProductDecision.

Commit, push, create the non-empty feature PR, converge mandatory checks and
actionable review threads, validate the exact reviewed head, merge under the
authorized narrow policy, perform a causal evidence-only closeout only when
truthfully required, delete obsolete branches, return to clean synchronized
main, and record the retrospective. Promote no preset change without a
reproducible provider-neutral defect.
```
