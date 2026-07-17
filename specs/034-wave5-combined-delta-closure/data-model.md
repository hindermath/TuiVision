# Datenmodell: Wave-5 Combined Delta Closure

## 1. ClosureRun

Ein Root-Objekt beschreibt den unabhängigen Abschluss.

| Feld | Regel |
|---|---|
| `schemaVersion` | exakt `1.0` |
| `runId` | exakt `034-wave5-combined-delta-closure` |
| `baselineCommit` | synchroner `main`-Commit beim Laufstart |
| `deliveryMode` | exakt `MergeAndSync` |
| `featureHeadDecision` | `ReadyForMerge` oder `Blocked` |
| `featureHeadWave5State` | `BlockedPendingCausalClosure` |
| `featureHeadWave6State` | `BlockedPendingCausalClosure` |
| `postMergeWave5Target` | exakt `Closed` |
| `postMergeWave6Target` | exakt `EligibleForIntake` |
| Reviewfelder | Owner, Reviewer, Datum, Evidence, Ergebnis, Risiko, Follow-up, Trigger |

Zustandsfolge:

```text
ActiveAudit
  -> Blocked                    bei verletzter Pflichtinvariante
  -> ReadyForMerge              nach vollständigen Feature-Head-Gates
  -> MergedPendingCloseout      nach Feature-Merge
  -> Completed                  nach kausalem Evidence-Closeout
```

## 2. ProductDelta

| Feld | Regel |
|---|---|
| `prNumber` | eine der Nummern 93 bis 97 |
| `role` | `FunctionalProduct`, `FunctionalCloseout`, `PromptMetadata`, `ShowcaseProduct`, `ShowcaseCloseout` |
| `baseCommit` / `headCommit` / `mergeCommit` | exakte vollständige Git-IDs |
| `filePaths` | eindeutige, sortierte, erwartete Menge |
| `productPaths` | nur für PR #93 und #96 |
| `result` | `Pass` nur bei exakter Übereinstimmung |

Die autoritative Produktmenge ist ausschließlich die Vereinigung der
`productPaths` aus PR #93 und #96.

## 3. AcceptedInput

| Feld | Regel |
|---|---|
| `featureId` | `032` oder `033` |
| `path` | eindeutiger existierender Repository-Pfad |
| `sha256` | lowercase SHA-256 mit 64 Hex-Zeichen |
| `role` | Source-, Consumer-, Function-, Showcase-, Guide- oder Closeout-Evidence |
| `result` | `Pass` nur bei aktuellem Hash |
| `reevaluationTrigger` | Pfad- oder Inhaltsänderung |

## 4. HistoricalSourceRole

Exakt 15 Zeilen:

- `TVDEMO.PAS`
- `DEMOCMDS.PAS`
- `DEMOSTRS.PAS`
- `GADGETS.PAS`
- `TVEDIT.PAS`
- `TVHC.PAS`
- `HELPFILE.PAS`
- `DEMOHELP.PAS`
- `TVRDEMO.PAS`
- `GENRDEMO.PAS`
- `ASCIITAB.PAS`
- `CALC.PAS`
- `CALENDAR.PAS`
- `PUZZLE.PAS`
- `MOUSEDLG.PAS`

Jede Zeile enthält Pfad, Rolle, Feature-032-Ziel, Git-Blob am Merge und am
Audit-Head, Modernisierungsgrenze und reziproke Consumer-/Beispiel-IDs.

## 5. ConsumerGroup

Exakt sechs Zeilen `W5-001` bis `W5-006`.

| Feld | Regel |
|---|---|
| `consumerId` | geschlossene eindeutige ID |
| `sourcePaths` | nicht leere Teilmenge der 15 Quellen |
| `exampleIds` | nicht leere Teilmenge der zehn Beispiele |
| `frameworkDecision` | akzeptiertes Feature-032-Vokabular |
| `frameworkContracts` | nicht leer |
| `functionalProofIds` | reziprok |
| `residualRisk` / `trigger` | vollständig |

## 6. FunctionalProof

Exakt zehn Zeilen, eine pro Beispiel. Jede bindet Feature-032-Test,
App-Loop-Rolle, Zustand, View, Cells, Safety-Grenze, Ergebnis und Evidence.

## 7. ShowcaseClosure

Exakt zehn Zeilen, eine pro Beispiel. Jede bindet Feature-033-Test,
Main-View, Fokus, StatusLine, Description, Keyboard, constrained Layout,
Cells, Ergebnis und Evidence.

## 8. GuideLaunchPath

Exakt zehn Zeilen, eine pro Beispiel.

| Feld | Regel |
|---|---|
| `exampleId` | reziprok zu einer CombinedExampleRow |
| `projectPath` | existierendes `examples/Tp7*`-Projekt |
| `guidePath` | existierende learner-facing Anleitung |
| `normalCommand` | normaler Start ohne versteckten Smoke-Modus |
| `smokeCommand` | kontrollierter `--smoke`-Start |
| `primaryAction` | konkrete Bedienung |
| `descriptionPath` | `F1` oder `Help -> Description` |
| `exitPath` | `Ctrl+Q` |

## 9. CombinedExampleRow

Exakt zehn Zeilen in der Reihenfolge:

1. `Tp7Demo`
2. `Tp7Edit`
3. `Tp7Help`
4. `Tp7ResourceDemo`
5. `Tp7ResourceGenerator`
6. `Tp7AsciiTable`
7. `Tp7Calculator`
8. `Tp7Calendar`
9. `Tp7Puzzle`
10. `Tp7MouseDialog`

Pflichtfelder verbinden historische Quellen, Consumer, Funktionsproof,
Showcase-Proof, Guide/Launch, Einstieg, ersten sichtbaren Zustand, primäre
Bedienung, Fokus, Status, Description, Beendigung, Framework-Komponenten,
lokale Sonderlogik, Safety-Grenze, Evidence, Risiko und Trigger.

Dimensionen:

- `behavior`
- `interaction`
- `layout`
- `proof`
- `documentation`
- `a11y`
- `platform`
- `security`
- `frameworkReuse`

Jede Dimension ist genau `Pass`, `IntentionalDeviation`, `Gap` oder `N/A`.
Jede Zeile besitzt genau eine Hauptentscheidung:

- `AcceptedAsIs`
- `AcceptedIntentionalDeviation`
- `CandidateFinding`
- `ProductDecision`

Eine akzeptierte Zeile darf kein `Gap` enthalten.

## 10. CandidateFinding

| Feld | Regel |
|---|---|
| `findingId` | stabile eindeutige ID `W5D###` |
| `exampleIds` / `consumerIds` | nicht leer |
| `category` | Verhalten, Interaktion, Proof, Dokumentation, A11Y, Plattform oder Framework-Wiederverwendung |
| `reproduction` | konkrete reproduzierbare Beobachtung |
| `evidencePaths` | nicht leer |
| `owner` | nicht leer |
| `followUpBoundary` | außerhalb Feature 034 |
| `reevaluationTrigger` | nicht leer |

Stilistische Pascal-/C#-Unterschiede allein sind kein Finding.

## 11. GovernanceDecision

Eine vollständige Zeile pro relevantem Checkpoint der sieben installierten
Presets. `Applicability` ist `Applicable`, `N/A` oder `Open`; `N/A` benötigt
Begründung und Trigger, `Open` zusätzlich Owner und konkretes Follow-up.

## 12. ValidationEvidence

Eine Zeile pro deklariertem Gate mit ID, Scope, Command, Plattform,
Candidate-Head, Ergebnis, Metrik, Evidence, Failure-Boundary, Owner, Reviewer
und Trigger. Erlaubte Ergebnisse sind `Pass`, `Fail`, `N/A`, `Open` oder
`TrackedExternally`.

## 13. WaveTransition

```text
Feature head:
  Wave 5 = BlockedPendingCausalClosure
  Wave 6 = BlockedPendingCausalClosure

Nach geprüftem Feature-Merge und vollständigem Closeout:
  Wave 5 = Closed
  Wave 6 = EligibleForIntake
```

Ohne vollständigen Closeout ist nur der gesperrte Zustand gültig. Ein
teilweiser Closeout, ein finaler Marker ohne Merge-Evidence oder ein
gemischter Zustand ist ungültig. Der finale Zustand startet weder Feature 035
noch Wave 6.
