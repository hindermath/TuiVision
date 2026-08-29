# Data Model: Documentation and Publishing Closure

## `GeneralGuideRecord`

- `TopicId`: einer der sieben verbindlichen Guide-Slugs.
- `Path`: kanonischer Markdown-Pfad.
- `Purpose`, `Prerequisites`, `Procedure`, `ArchitectureNote`, `Exercise`.
- `GermanFirst`, `EnglishSecond`, `TextFirst`: erfüllte Sprach- und A11Y-Gates.
- `Evidence`: Link-, DocFX-, Axe- oder Textbrowser-Nachweis.

## `ExampleLearningRecord`

- `Project`: genau eines der 38 Beispielprojekte.
- `GuidePath`: genau ein vorhandener Detail-Guide.
- `LearningGoal`, `Prerequisites`, `Launch`, `Operation`.
- `ArchitectureNote`, `Exercise`.
- `Decision`: `GuideAdequate`, `MatrixCompletesContract` oder
  `AcceptedBoundary`.

Jedes Projekt und jeder Guide darf genau einmal primär vorkommen. `Shared` ist
kein ausführbares Beispielprojekt und gehört nicht in die 38-Zeilen-Matrix.

## `RequirementClosureRecord`

- `RequirementId`: genau eine der 27 Abstimmungs-IDs.
- `BaselineStatus`: unveränderter Status aus der Abstimmung von 2026-07-26.
- `Decision`: `Closed` oder `AcceptedBoundary`.
- `EvidencePath`: aktuelle, überprüfbare Repository-Evidence.
- `Rationale`, `ResidualBoundary`, `ReevaluationTrigger`.

## `PublishingProofRecord`

- `Gate`: Release-CS1591, DocFX, Axe, Lynx, Pages oder Exact-Head-CI.
- `Scope`, `Command`, `Result`, `Head`, `EvidencePath`.
- `GeneratedOutputTracked`: muss immer `false` bleiben.

## Beziehungen / Relations

- Die sieben `GeneralGuideRecord`-Einträge bilden den Einstiegspfad.
- Die 38 `ExampleLearningRecord`-Einträge vertiefen diesen Pfad.
- Die 27 `RequirementClosureRecord`-Einträge verweisen auf beide Ebenen und
  vorhandene Governance-/Feature-Evidence.
- `PublishingProofRecord` belegt, dass alle erreichbaren Dokumente auf dem
  gelieferten Head rendern und zugänglich bleiben.
