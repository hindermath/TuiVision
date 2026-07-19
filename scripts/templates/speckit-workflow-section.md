## Spec-kit-Workflow

Neue Features in diesem Workspace werden nach dem **Specification-Driven Development (SDD)**-Workflow entwickelt.
Der Workflow verwendet das `speckit`-CLI-Tool (GitHub Copilot Skill).

Schritte für ein neues Feature:

1. **Spezifikation erstellen** — `speckit specify "Feature-Name"` → `specs/{branch}/spec.md`
2. **Klärungsfragen** — `speckit clarify` → offene Fragen in `spec.md` beantworten
3. **Implementierungsplan** — `speckit plan` → `specs/{branch}/plan.md`
4. **Aufgabenliste** — `speckit tasks` → `specs/{branch}/tasks.md`
5. **Implementieren** — `speckit implement` → Aufgaben aus `tasks.md` abarbeiten
6. **Validieren** — `bash ~/scripts/check-homogeneity.sh "$PWD"` oder
   `pwsh ~/scripts/check-homogeneity.ps1 -TargetDir "$PWD"` → Compliance-Score prüfen

Alle Spec-Artefakte werden im Branch-Verzeichnis `specs/{branch}/` gespeichert und versioniert.

### Governance-Presets

Registrierte Level-0-, Level-1- und Level-2-Repositories installieren
Spec-Kit-Governance-Presets aus der zentralen Matrix
`scripts/config/spec-kit-governance-presets.json`. Das Standard-Set dieser
Workspace-Familie ist:

| Preset-ID | Name | Version | Priorität |
|---|---|---:|---:|
| `security-governance` | Security Governance | `v0.6.1` | `10` |
| `architecture-governance` | Architecture Governance | `v0.5.1` | `20` |
| `isaqb-architecture-governance` | iSAQB Architecture Governance | `v0.2.1` | `30` |
| `a11y-governance` | A11Y Governance | `v0.4.1` | `40` |
| `cross-platform-governance` | Cross-Platform Governance | `v0.2.1` | `50` |
| `agent-parity-governance` | Agent Parity Governance | `v0.4.0` | `60` |
| `autonomous-run-governance` | Autonomous Run Governance | `v0.3.1` | `70` |
| `parallel-autonomous-run-governance` | Parallel Autonomous Run Governance | `v0.2.2` | `80` |

`autonomous-run-governance` ist Teil der Standard-Achtermatrix. Vollständige
autonome Läufe bleiben ausdrücklich delegationspflichtig. `LocalImplementation` ist der
sichere Default; Installation erteilt keine Remote-, Merge- oder Bypass-Rechte.
`speckit.autonomous-status` prueft einen Lauf read-only,
`speckit.autonomous-stop` pausiert kooperativ am naechsten sicheren Grenzpunkt,
und `speckit.autonomous-resume` ist fuer `PausedByUser` verpflichtend. Ein
gespeicherter Delivery-Modus ist keine aktuelle Berechtigung. Nach Preset- oder
Governance-Drift werden neue zwingende Korrektheits-, Sicherheits-,
Berechtigungs- und Evidenzregeln minimal mit akzeptierten Plan-, Task- und
Checklist-Artefakten abgeglichen; Effizienzpraeferenzen loesen keine
rueckwirkende Neugenerierung aus.

`parallel-autonomous-run-governance` startet durch seine Installation keine
Kampagne. Ausdruecklich delegierte Kampagnen verwenden getrennte Worktrees,
maximal drei gleichzeitig aktive Worker, optionale agentenneutrale
Runner-Metadaten, kooperatives Stop/Resume und eine providergebundene,
fortsetzbare Konsolidierung. `Completed` folgt erst nach Synchronisation,
manifestdeklarierten Post-Merge-Aktionen und Abschlussvalidierung. Reale
Kampagnen setzen in jedem Worker-Repository ein installiertes und aktiviertes
`autonomous-run-governance >=0.2.2` voraus. Preset 7 mit Prioritaet `70`
liefert den Worker-Lebenszyklus; Preset 8 mit Prioritaet `80` koordiniert die
Kampagne. Ein fehlendes, deaktiviertes oder zu altes Preset 7 beendet den
Preflight vor dem Worker-Start. `requireAutonomousPreset: false` ist nur fuer
isolierte interne Fixtures zulaessig und kein Produktionsmodus.

Die ursprünglichen sechs Presets sind seit 2026-05-04 und
`autonomous-run-governance` v0.2.2 ist seit 2026-07-17 im `github/spec-kit`
Community-Katalog enthalten. Registrierte Level-0-, Level-1- und
Level-2-Repositories verwenden standardmäßig alle acht Presets, sofern keine
begründete Ausnahme dokumentiert ist. Nach Installation oder Update prüfen:
`install-spec-kit-governance-presets.* --check-only` / `-CheckOnly`,
`specify preset list`,
`specify preset info <id>` und bei Template-Fragen `specify preset resolve
<template>`. `.specify/presets/` wird committed, `.specify/presets/.cache/`
nicht. Alle acht Presets erzeugen oder verlangen audit-ready Spec-Kit-Run-Evidenz mit `Applicable` / `N/A` / `Open`, Begruendung, Evidenzpfad, Reviewer, Restrisiko und Follow-up.
`parallel-autonomous-run-governance` v0.2.2 ist eigenstaendig veroeffentlicht
und wurde mit `github/spec-kit#3591` fuer den Community-Katalog eingereicht.
Bei jeder Preset-Version oder Prioritätsänderung zuerst die zentrale Matrix
aktualisieren und danach README-Tabellen, Constitution, Agenten-Dateien und
Templates gemeinsam prüfen.

---

## Spec-kit Workflow

New features in this workspace are developed following the **Specification-Driven Development (SDD)** workflow.
The workflow uses the `speckit` CLI tool (GitHub Copilot Skill).

Steps for a new feature:

1. **Create specification** — `speckit specify "Feature Name"` → `specs/{branch}/spec.md`
2. **Clarification questions** — `speckit clarify` → answer open questions in `spec.md`
3. **Implementation plan** — `speckit plan` → `specs/{branch}/plan.md`
4. **Task list** — `speckit tasks` → `specs/{branch}/tasks.md`
5. **Implement** — `speckit implement` → work through tasks in `tasks.md`
6. **Validate** — `bash ~/scripts/check-homogeneity.sh "$PWD"` or
   `pwsh ~/scripts/check-homogeneity.ps1 -TargetDir "$PWD"` → check compliance score

All spec artefacts are stored and versioned in the branch directory `specs/{branch}/`.

### Governance Presets

Registered level-0, level-1, and level-2 repositories install Spec Kit
governance presets from the central matrix
`scripts/config/spec-kit-governance-presets.json`. The standard set for this
workspace family is:

| Preset ID | Name | Version | Priority |
|---|---|---:|---:|
| `security-governance` | Security Governance | `v0.6.1` | `10` |
| `architecture-governance` | Architecture Governance | `v0.5.1` | `20` |
| `isaqb-architecture-governance` | iSAQB Architecture Governance | `v0.2.1` | `30` |
| `a11y-governance` | A11Y Governance | `v0.4.1` | `40` |
| `cross-platform-governance` | Cross-Platform Governance | `v0.2.1` | `50` |
| `agent-parity-governance` | Agent Parity Governance | `v0.4.0` | `60` |
| `autonomous-run-governance` | Autonomous Run Governance | `v0.3.1` | `70` |
| `parallel-autonomous-run-governance` | Parallel Autonomous Run Governance | `v0.2.2` | `80` |

`autonomous-run-governance` is part of the standard eight-preset matrix.
Complete autonomous runs still require explicit delegation. `LocalImplementation` is the safe default;
installation grants no remote, merge, or bypass authority.
`speckit.autonomous-status` inspects a run read-only,
`speckit.autonomous-stop` pauses cooperatively at the next safe boundary, and
`speckit.autonomous-resume` is mandatory for `PausedByUser`. A recorded delivery
mode is not current authority.
After preset or governance drift, new mandatory correctness, security,
permission, and evidence-integrity rules are minimally reconciled with accepted
Plan, Tasks, and checklist artifacts; efficiency preferences do not trigger
retroactive regeneration.

Installing `parallel-autonomous-run-governance` does not start a campaign.
Explicitly delegated campaigns use separate worktrees, at most three active
workers, optional agent-neutral runner metadata, cooperative stop/resume, and
provider-gated resumable consolidation. `Completed` follows only after
synchronization, manifest-declared post-merge actions, and final validation.
Real campaigns require installed and enabled
`autonomous-run-governance >=0.2.2` in every worker repository. Preset 7 at
priority `70` supplies the worker lifecycle; Preset 8 at priority `80`
coordinates the campaign. A missing, disabled, or outdated Preset 7 fails
preflight before any worker starts. `requireAutonomousPreset: false` is valid
only for isolated internal fixtures and is not a production mode.

The original six presets have been in the `github/spec-kit` community catalog
since 2026-05-04, and `autonomous-run-governance` v0.2.2 was verified there on
2026-07-17. Registered level-0, level-1, and level-2 repositories default to
all eight presets unless a justified exception is documented. After install or
update, verify with `install-spec-kit-governance-presets.* --check-only` /
`-CheckOnly`, `specify preset list`, `specify preset info <id>`, and for template questions
`specify preset resolve <template>`. Commit `.specify/presets/`, but not
`.specify/presets/.cache/`. All eight presets produce or require audit-ready Spec-Kit run evidence with `Applicable` / `N/A` / `Open`, rationale, evidence path, reviewer, residual risk, and follow-up.
`parallel-autonomous-run-governance` v0.2.2 is published independently and was
submitted to the community catalog as `github/spec-kit#3591`.
For every preset version or priority change, update the central matrix first,
then review README tables, constitution, agent guidance files, and templates
together.
