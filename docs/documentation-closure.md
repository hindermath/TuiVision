# Dokumentations- und Publishing-Abschluss / Documentation and Publishing Closure

## Zweck / Purpose

Diese Matrix schließt genau die 27 Einträge, die der unveränderliche
Pflichtenheft-Abgleich vom 26. Juli 2026 der Gruppe
`DocumentationAndPublishing` zugeordnet hat. `Closed` bedeutet, dass aktuelle
Evidence die Anforderung erfüllt. `AcceptedBoundary` hält eine bewusst
begrenzte, überprüfbare Regel fest; sie ist kein stiller Erfolg.

*This matrix closes exactly the 27 entries assigned to
`DocumentationAndPublishing` by the immutable requirements reconciliation of
26 July 2026. `Closed` means current evidence satisfies the requirement.
`AcceptedBoundary` records a deliberate, reviewable limit and is not a silent
success.*

## Closure-Matrix

| ID | Baseline | Entscheidung | Evidence | Begründung und Grenze / Rationale and boundary | Wiederbewertung / Re-evaluation |
|---|---|---|---|---|---|
| `PF-LS-007` | `PartiallySatisfied` | `Closed` | [Einstieg](guides/getting-started.md), [Beispiel-Lernpfade](guides/example-learning-paths.md) | DE: Einstieg und Verwendung bilden einen geordneten Lernpfad. EN: onboarding and use form an ordered learning path. | neuer Lernpfad oder neues Beispiel / new learner path or example |
| `PF-LS-009` | `PartiallySatisfied` | `AcceptedBoundary` | `specs/015-didactic-comment-hardening/pr-evidence.md`, [Dokumentations-Governance](documentation-governance.md) | DE: XML dokumentiert APIs; selektive Inline-Kommentare erklären nicht triviale Logik. Kein globales Kommentieren jeder Methode. EN: XML documents APIs; selective inline comments explain non-trivial logic. | neue oder geänderte nicht triviale Logik / new or changed non-trivial logic |
| `PF-LS-010` | `PartiallySatisfied` | `Closed` | [Multi-Mac-Workflow](guides/multi-mac-workflow.md), [Antigravity-Migration](maintenance/antigravity-cli-migration.md) | DE: `gh`, Spec Kit und aktive Agent-CLIs sind für beide Macs dokumentiert; `agy` ersetzt operative Gemini-Aufrufe. EN: required tools and active agents are documented for both Macs. | Agent-, Mac- oder Toolchain-Wechsel / agent, Mac, or toolchain change |
| `M-15` | `PartiallySatisfied` | `Closed` | [Einstieg](guides/getting-started.md), [Beispiel-Lernpfade](guides/example-learning-paths.md) | DE: allgemeine Nutzung und alle 38 Beispiele sind auffindbar. EN: general use and all 38 examples are discoverable. | Beispielinventar ändert sich / example inventory changes |
| `M-17` | `PartiallySatisfied` | `Closed` | [Dokumentations-Governance](documentation-governance.md), [Beispiel-Lernpfade](guides/example-learning-paths.md) | DE: Zielgruppe, Leserpfad, Sprache, Evidence und nächste Aktion sind vereinheitlicht. EN: audience, reader path, language, evidence, and next action are standardized. | Governance oder Lernvertrag ändert sich / governance or learning contract changes |
| `M-19` | `PartiallySatisfied` | `AcceptedBoundary` | `specs/015-didactic-comment-hardening/pr-evidence.md`, `AGENTS.md` | DE: wartungsrelevante nicht triviale Logik wird selektiv geprüft; triviales „Was“ bleibt kommentararm. EN: maintainability-relevant logic is reviewed selectively; obvious code stays concise. | neue zentrale Flows oder Proof-Helper / new central flows or proof helpers |
| `M-21` | `PartiallySatisfied` | `Closed` | [Multi-Mac-Workflow](guides/multi-mac-workflow.md) | DE: Start, Wechsel, Build, Docs, Agenten, PR und Troubleshooting sind reproduzierbar beschrieben. EN: start, handoff, build, docs, agents, PR, and troubleshooting are reproducible. | Plattform- oder CLI-Änderung / platform or CLI change |
| `PF-QA-014` | `Open` | `Closed` | [Dokumentations-Governance](documentation-governance.md), `specs/043-documentation-publishing-closure/pr-evidence.md` | DE: didaktischer Review und Renderer-/A11Y-Gates sind Merge-Kriterien. EN: didactic review and renderer/accessibility gates are merge criteria. | Release- oder Dokumentationspolicy ändert sich / release or documentation policy changes |
| `PF-QA-016` | `Open` | `Closed` | `specs/015-didactic-comment-hardening/pr-evidence.md`, [Secure-Code-Review](secure-development/checklisten/CL_08_Sicherheits-Code-Review.md) | DE: Quellcode-Review deckt Dokumentationswert und sichere Grenzen ab. EN: source review covers documentation value and secure boundaries. | Quellcode- oder Reviewregel ändert sich / source or review rule changes |
| `PF-QA-023` | `PartiallySatisfied` | `Closed` | [Multi-Mac-Workflow](guides/multi-mac-workflow.md), [Antigravity-Migration](maintenance/antigravity-cli-migration.md) | DE: beide macOS-Systeme und zusätzliche Linux-/Windows-Gates sind beschrieben; aktive Google-CLI ist `agy`. EN: both Macs and added Linux/Windows gates are documented; the active Google CLI is `agy`. | unterstützte Plattform oder Agent ändert sich / supported platform or agent changes |
| `PF-DOC-002` | `PartiallySatisfied` | `Closed` | [Architektur](guides/architecture.md), [Architekturartefakte](architecture/architecture-vision.md) | DE: Einstieg, Schichten, Runtime-Pfad und vertiefende Architektur sind verlinkt. EN: onboarding, layers, runtime path, and deeper architecture are linked. | Architektur- oder Migrationsgrenze ändert sich / architecture or migration boundary changes |
| `PF-DOC-003` | `PartiallySatisfied` | `Closed` | [Historische Abweichungen](guides/historical-deviations.md), [Porting-Status](porting-status.md), `CHANGELOG.md` | DE: Fortschritt und bewusste Abweichungen sind auffindbar. EN: progress and intentional deviations are discoverable. | neue Produktabweichung / new product deviation |
| `PF-DOC-004` | `Open` | `Closed` | [Dokumentations-Governance](documentation-governance.md), [Einstieg](guides/getting-started.md), [Beispiel-Lernpfade](guides/example-learning-paths.md) | DE: allgemeine und beispielbezogene Lernpfade nutzen denselben Lehrvertrag. EN: general and example paths use one teaching contract. | neue Dokumentfamilie / new document family |
| `PF-DOC-005` | `PartiallySatisfied` | `AcceptedBoundary` | `specs/015-didactic-comment-hardening/pr-evidence.md`, `AGENTS.md` | DE: APIs erhalten XML; nicht triviale Logik wird auf didaktischen Kommentarwert geprüft. Globales Kommentieren bleibt ausgeschlossen. EN: APIs receive XML; non-trivial logic is reviewed for didactic value. | API/XML oder nicht triviale Logik ändert sich / API, XML, or non-trivial logic changes |
| `PF-DOC-006` | `PartiallySatisfied` | `Closed` | [Multi-Mac-Workflow](guides/multi-mac-workflow.md), [Antigravity-Migration](maintenance/antigravity-cli-migration.md) | DE: `codex`, `claude`, `copilot`, `agy`, `gh` und Spec Kit sind operativ beschrieben. EN: active agents, GitHub CLI, and Spec Kit are documented operationally. | Toolchain-Registry ändert sich / toolchain registry changes |
| `PF-DOC-007` | `Open` | `Closed` | [Getting Started](guides/getting-started.md) | DE: kanonischer Einstieg vorhanden. EN: canonical onboarding exists. | Einstieg oder Voraussetzungen ändern sich / onboarding or prerequisites change |
| `PF-DOC-008` | `Open` | `Closed` | [Architektur](guides/architecture.md) | DE: kanonischer Architekturüberblick vorhanden. EN: canonical architecture overview exists. | Schichten oder Abhängigkeiten ändern sich / layers or dependencies change |
| `PF-DOC-009` | `Open` | `Closed` | [Event-Loop](guides/concepts/event-loop.md) | DE: Event-, Command-, Draw- und Proof-Pfad erklärt. EN: event, command, draw, and proof flow is explained. | Dispatch- oder Loop-Vertrag ändert sich / dispatch or loop contract changes |
| `PF-DOC-010` | `Open` | `Closed` | [View-Hierarchie](guides/concepts/view-hierarchy.md) | DE: Owner, Fokus, Z-Reihenfolge und Puffer erklärt. EN: ownership, focus, Z order, and buffers are explained. | View-/Fokus-Vertrag ändert sich / view or focus contract changes |
| `PF-DOC-011` | `Open` | `Closed` | [Koordinatensystem](guides/concepts/coordinate-system.md) | DE: lokale/globale Punkte und inklusive/exklusive Bounds erklärt. EN: local/global points and inclusive/exclusive bounds are explained. | Geometrie- oder Hit-Test-Vertrag ändert sich / geometry or hit-test contract changes |
| `PF-DOC-012` | `Open` | `Closed` | [Serialisierung](guides/concepts/serialization.md) | DE: Registry, Grenzen und atomare Ablehnung erklärt. EN: registry, bounds, and atomic rejection are explained. | Persistenzformat ändert sich / persistence format changes |
| `PF-DOC-013` | `Open` | `Closed` | [Erster Dialog](guides/tutorials/first-dialog.md) | DE: bestehender modaler Tutorial-Pfad ist schrittweise dokumentiert. EN: the existing modal tutorial path is documented step by step. | Dialog- oder Tutorial-Vertrag ändert sich / dialog or tutorial contract changes |
| `PF-DOC-015` | `PartiallySatisfied` | `Closed` | [Getting Started](guides/getting-started.md), [Beispiel-Lernpfade](guides/example-learning-paths.md) | DE: allgemeine Guides führen zu Konzepten, Tutorial und erst danach Beispielen. EN: general guides lead to concepts, tutorial, and then examples. | Navigationsreihenfolge ändert sich / navigation order changes |
| `PF-AC-008` | `PartiallySatisfied` | `Closed` | [Dokumentations-Governance](documentation-governance.md), `specs/043-documentation-publishing-closure/pr-evidence.md` | DE: die neue Gesamtnavigation und die 7/38/27-Abnahme belegen den Standard. EN: the new navigation and 7/38/27 acceptance prove the standard. | neuer learner-facing Scope / new learner-facing scope |
| `PF-AC-009` | `Open` | `AcceptedBoundary` | `AGENTS.md`, [Dokumentations-Governance](documentation-governance.md), `specs/043-documentation-publishing-closure/pr-evidence.md` | DE: alle in 043 neu erstellten Lerntexte sind bilingual. Bereits reviewte normative Artefakte werden nicht rückwirkend übersetzt und dadurch hash-invalidiert; bei der nächsten autorisierten Änderung gilt Inline-Bilingualität oder `.EN.md`. EN: all new 043 learner text is bilingual; reviewed normative artifacts are translated only during an authorized update. | aktives Anforderungsartefakt wird fachlich geändert / active requirement artifact is materially updated |
| `PF-AC-011` | `PartiallySatisfied` | `AcceptedBoundary` | `specs/015-didactic-comment-hardening/pr-evidence.md`, `AGENTS.md` | DE: der selektive Code-Dokumentationsstandard ist nachgewiesen; vollständige Kommentardichte ist kein Qualitätsziel. EN: the selective code-documentation standard is proven; total comment density is not a quality target. | zentrale nicht triviale Logik ändert sich / central non-trivial logic changes |
| `PF-AC-013` | `PartiallySatisfied` | `Closed` | [Multi-Mac-Workflow](guides/multi-mac-workflow.md), [Antigravity-Migration](maintenance/antigravity-cli-migration.md) | DE: lokale Kernabläufe, Spec Kit und aktive Agenten sind für beide Macs dokumentiert; Remote-OS-Gates ergänzen den Nachweis. EN: local workflows, Spec Kit, and active agents are documented for both Macs, with remote OS gates. | Mac-, Agent-, Spec-Kit- oder CI-Änderung / Mac, agent, Spec Kit, or CI change |

## Summen / Totals

- `Closed`: 22
- `AcceptedBoundary`: 5
- Gesamt / Total: 27
- Offene oder doppelte IDs / Open or duplicate IDs: 0

Keine Entscheidung verändert die Abstimmungsdatei von 2026-07-26. Neue
Produkt-, API- oder Beispielbefunde müssen in einem eigenen Intake bewertet
werden.

*No decision rewrites the reconciliation snapshot from 2026-07-26. New
product, API, or example findings require a separate intake.*
