# Data Model: Sandbox Secure Development Hardening

## `SandboxAssessment`

- `schemaVersion`: `1.0`.
- `project`: `TuiVision`.
- `sandboxRepository`, `sandboxCommit`, `reviewDate`.
- `recommendation`: genau einer von `ApprovedWithBoundaries`,
  `ConditionallyUsable`, `NotApproved`, `NeedsDecision`.
- `nextSafeAction`, `owner`, `reviewer`, `residualRisk`.
- `sourceHashes`: eindeutige relative Sandbox-Pfade mit SHA-256.

## `ControlDecision`

- `controlId`: genau `CL-12-01` bis `CL-12-12`.
- `applicability`: `Applicable`, `N/A` oder `Open`.
- `implementationStatus`: `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled`
  oder `Not Assessed`.
- `rationale`, `evidence`, `owner`, `reviewer`, `reviewDate`.
- `residualRisk`, `followUp`, `reevaluationTrigger`.

`N/A` benötigt eine sachliche Begründung und einen Trigger. `Open` benötigt
Owner, konkrete Folgeaktion und Trigger. `Fulfilled` ist bei `Open` unzulässig.

## `MountDecision`

- `mountRole`: portable eindeutige Rolle ohne absoluten Hostpfad.
- `containerTarget`, `purpose`.
- `access`: `ReadOnly`, `ReadWrite`, `NamedVolume` oder `NotMounted`.
- `allowedContent`, `excludedContent`, `evidence`, `reevaluationTrigger`.

## `ExecutionDecision`

- `checkId`: eindeutiger Checkname.
- `location`: `Sandbox`, `LocalHost`, `CI`, `NotPermitted` oder `Open`.
- `proofLevel`: `StaticVerified`, `PracticallyVerified`, `PlatformVerified`
  oder `NotVerified`.
- `command`, `evidence`, `proofBoundary`, `reevaluationTrigger`.

`PlatformVerified` benötigt eine benannte Plattform. `NotPermitted` darf
keinen ausführbaren Befehl enthalten. `Open` darf nicht als verifiziert gelten.

## Beziehungen / Relations

- Ein `SandboxAssessment` besitzt genau zwölf `ControlDecision`-Einträge.
- Mount- und Execution-Entscheidungen liefern Evidence für die Kontrollen,
  ersetzen aber keine Human-Freigabe.
- Die Recommendation muss mit offenen Kontrollen vereinbar sein.
  `ApprovedWithBoundaries` ist bei offenen formellen Freigaben unzulässig.
