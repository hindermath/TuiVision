# Data Model

## AuditFinding

`findingId`, `category`, `severity`, `titleDe`, `titleEn`, `status`,
`sourceFeatures`, `claims`, `evidencePaths`, `owner`, `residualRisk`,
`reevaluationTrigger`, `remediationBoundary`.

## ControlCrosswalkRow

`controlId`, Feature-045-Status, Feature-046-Disposition, Evidence-Anzahlen,
genau eine `qualityDecision`, `rationale`, `findingIds` und `evidencePaths`.

## SandboxControlReview

`controlId`, Applicability, Implementation-Status, genau eine
`qualityDecision`, Evidence, Owner und Finding-Referenzen.

## InventoryProof

Erwartete und tatsaechliche Kardinalitaet, mechanisches Ergebnis, Stichprobe,
Semantikentscheidung und Beweisgrenze.

All counts are derived from arrays. Findings and crosswalk rows are separate so
one systemic defect can explain many rows without inflating the finding count.

