---
name: speckit-tasks
description: Break down implementation plans into actionable task lists.
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: preset:security-governance
---

# Speckit Tasks Skill

Before continuing, apply the Security Governance preset:

- convert MSL applicability and justification needs into explicit tasks
- convert security obligations into explicit tasks
- include evidence-production tasks under `docs/security/`
- avoid leaving secure-development work as undocumented assumptions

Before continuing, apply the Architecture Governance preset:

- convert architecture obligations into explicit tasks
- include `docs/security/` evidence updates
- add BSI C3A cloud autonomy applicability tasks when cloud services or
  provider-dependent deployments are in scope
- do not leave threat-modeling or ADR work implicit

Before continuing, apply the iSAQB Architecture Governance preset:

- convert architecture goals, quality scenarios, views, ADRs, risks, and
  technical debt into explicit tasks
- include concrete evidence-production tasks under `docs/architecture/`
- add architecture-review tasks for significant structure, interface,
  runtime, or deployment changes
- if security-relevant architecture is affected, include the corresponding
  secure-architecture tasks from `architecture-governance`

Before continuing, apply the A11Y Governance preset:

- convert accessibility expectations into explicit tasks
- convert bilingual delivery work into explicit tasks
- do not leave A11Y or language review implicit

Before continuing, apply the Cross-Platform Governance preset:

- add explicit tasks for both `*.sh` and `*.ps1` variants in the same
  change
- add tasks for the Unix man-page and the bilingual PowerShell help
  block
- add a task to expose the PowerShell variant as a Cmdlet with an
  approved `Verb-Noun` name
- add a parity-verification task using the script-parity checklist

Before continuing, apply the Agent Parity Governance preset:

- add explicit tasks to update every maintained agent surface in the
  same change
- add tasks to propagate shared rules into project templates and the
  local constitution mirror
- add a parity-verification task using the agent-parity checklist

# Command Template: `/speckit.tasks`

Use this command to generate an executable task list from `plan.md` and `spec.md`.

## Required Actions

1. Organize tasks by user story for independent delivery.
2. Include Red-Green-Refactor test tasks before implementation tasks.
3. Include documentation tasks:
   - bilingual updates (German block first, then English)
   - XML documentation completeness
   - `docfx docfx.json` run when API/XML docs changed
   - architecture evidence under `docs/architecture/` when structure, interfaces,
     quality attributes, runtime behavior, deployment, or technical debt changes
4. Include coverage and dependency tasks:
   - coverage evidence for `>=70%` minimum and `>=80%` target tracking
   - `dotnet list package --outdated` review and update tasks
5. Include PR preparation task (purpose, touched projects, test evidence, config/API impact).
6. Create evidence before implementation, schedule one representative vertical
   slice first, and use test-first proof for observable contracts.
7. Group tasks by reviewable outcomes instead of individual evidence cells;
   serialize all shared evidence, version, statistics, workflow, and agent-file
   writes.
8. Include trigger-based validation and only the remote closeout tasks allowed
   by the plan's delivery mode.

## Validation Checklist

- Every code change has corresponding tests.
- Documentation and governance tasks are present.
- Task ordering supports incremental, verifiable delivery.
- Coverage and dependency currency tasks are explicitly scheduled.
- Architecture evidence and `N/A` rationale tasks are explicitly scheduled.
- Task count is proportional to reviewable outcomes, and every iterative or remote phase has a clear completion gate.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.

## Nutzungshilfe / Reader help

DE: Gemeinsame Regeln auf tatsächlich gepflegten Agentenflächen inhaltlich gleich halten. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-agent-parity-governance/blob/main/docs/user-guide.md).

EN: Keep shared rules semantically equivalent across actually maintained agent surfaces. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-agent-parity-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.

## Nutzungshilfe / Reader help

DE: Dieselbe fachliche Funktion auf benannten Plattformen prüfen, ohne nicht ausgeführte Tests zu behaupten. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-cross-platform-governance/blob/main/docs/user-guide.md).

EN: Check the same functional behaviour on named platforms without claiming unperformed tests. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-cross-platform-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.

## Nutzungshilfe / Reader help

DE: Nutzbare Oberflächen und verständliche zweisprachige Dokumentation von Anfang an einplanen. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-a11y-governance/blob/main/docs/user-guide.md).

EN: Plan usable interfaces and understandable bilingual documentation from the start. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-a11y-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.

## Nutzungshilfe / Reader help

DE: Systemaufbau und Architekturentscheidungen mit wenigen passenden Vorlagen erklären. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-isaqb-architecture-governance/blob/main/docs/user-guide.md).

EN: Explain system structure and architecture decisions with a few suitable templates. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-isaqb-architecture-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.

## C3A-/C5-Evidence-Vertrag / C3A/C5 evidence contract

DE: Bei anwendbarem C3A alle 30 Gruppen aus `c3a-criteria-catalog` v1.0
sichtbar halten; je ausgewaehltem C-/AC-Identifier Evidence und Variante mit
Begruendung erfassen. SI ist Erlaeuterung, kein bestandener Kontrollpunkt.
N/A braucht Begruendung; Open braucht Grund, Owner, Aktion und Wiedervorlage.
Providerbehauptungen von unabhaengiger Pruefung und Kundenpflichten trennen.
C5 Typ 1 ist zeitpunktbezogen und kein Wirksamkeitsnachweis ueber einen
Zeitraum. Typ 2 braucht Pruefzeitraum und Wirksamkeitsbewertung; Unknown bleibt
eine offene Luecke. C5 erfuellt C3A nicht automatisch. Keine Evidence erfinden,
kein Audit oder Zertifikat behaupten und historische Nachweise nicht umschreiben.

EN: For applicable C3A, retain all 30 groups from `c3a-criteria-catalog` v1.0;
record exact selected C/AC IDs, variant rationale and attributable evidence.
SI informs interpretation, not another passed control. N/A requires rationale;
Open requires reason, owner, action and reevaluation. Distinguish provider
claims, independent review and customer responsibilities. C5 Type 1 is
point-in-time assurance, not sustained operating effectiveness. Type 2 requires
audit period and effectiveness assessment; Unknown remains an open gap.
C5 does not automatically satisfy C3A. Do not fabricate evidence, claim an
audit/certification or rewrite historical records.

## Datenschutz und regulatorische Architektur / Privacy and regulatory architecture

DE: Die projektspezifische Anwendbarkeit von DS-GVO, KI-VO, CRA, NIS2 und
DORA wird durch Security-Evidence gefuehrt; hier keine zweite Rechtsentscheidung
erfinden. Bei installiertem Security-Preset dessen Detailvorlagen nutzen,
sonst gleichwertige projektgefuehrte Nachweise verlinken. Beispielprogramm,
Entwicklungswerkzeuge und Organisation getrennt betrachten; AI-SBOM: N/A
und Ausbildungszweck sind keine allgemeine regulatorische Ausnahme.
Privacy by Design/Default: Datenminimierung, Zweck, Empfaenger, Regionen,
Speicherbegrenzung, Loeschung und Betroffenenrechte in Datenfluessen abbilden.
KI-Tool-/Produktgrenzen, Prompt-, Logging- und Telemetriepfade pruefen.
NIS2-/DORA-relevante Dienstleisterabhaengigkeit, getestete Wiederherstellung,
Verfuegbarkeit, Konzentrationsrisiko und Exit-Faehigkeit rollenbezogen planen.
C3A/C5-Nachweise ersetzen keinen Datenschutz-, NIS2- oder DORA-Nachweis.
EN: Project Security evidence owns GDPR, AI Act, CRA, NIS2 and DORA
applicability; do not create a competing legal decision. Use Security detail
templates when installed, otherwise equivalent project-owned records.
Assess sample product, development tools and organisation separately;
AI-SBOM: N/A and education are not blanket regulatory exemptions.
Map privacy by design/default, minimisation, purpose, recipients, regions,
retention, deletion and subject rights to data flows. Review AI/tool boundaries
and prompt/log/telemetry paths. Plan role-specific supplier dependencies,
tested recovery, availability, concentration risk and exit capability for
NIS2/DORA-related scope. C3A/C5 evidence does not replace regulatory evidence.

- Applicability record / exact scope / owner / review date:
- Personal-data inventory / synthetic-data decision:
- Data flow / purpose / trust boundary / receiver / region:
- Retention / deletion / defaults / subject-rights interface:
- AI/tool usage boundary / prompt and logging safeguards:
- Supplier dependency / tested recovery / exit / concentration risk:
- Legal Open finding / qualified reviewer / next action / due date:

## Nutzungshilfe / Reader help

DE: Sicherheitsentscheidungen zur Systemstruktur nachvollziehbar machen, getrennt von der codebezogenen Security-Prüfung. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-architecture-governance/blob/main/docs/user-guide.md).

EN: Make system-structure security decisions traceable, separately from code-level security verification. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-architecture-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.

## Regulatorischer Evidence-Vertrag / Regulatory evidence contract

DE: DS-GVO, KI-VO, CRA, NIS2 und DORA getrennt fuer Beispielprogramm,
Entwicklungswerkzeuge und nutzende Organisation pruefen. Rechtliche Rollen,
Land, datierte Rechtsquelle und Anwendungszeitpunkt nennen; direkte Pflichten
von vertraglichen Kunden-/Lieferkettenanforderungen trennen. Unbekannt bleibt
Open mit Owner, Aktion und Frist. N/A braucht Begruendung und Trigger.
AI-SBOM: N/A entscheidet nicht ueber DS-GVO oder KI-VO. Ausbildung ist keine
allgemeine Ausnahme. Security fuehrt die Anwendbarkeit; Architecture
referenziert diese Entscheidung fuer Datenfluesse, Schutz und Resilienz.
Technische Pruefung ist keine Rechtsfreigabe. Historische Evidence erhalten.
EN: Assess GDPR, AI Act, CRA, NIS2 and DORA separately for the sample product,
development tooling and operating organisation. Record roles, jurisdiction,
dated legal source and application date; separate direct and contractual
duties. Unknown remains Open with owner, action and due date; N/A needs
rationale and trigger. AI-SBOM: N/A does not decide GDPR/AI Act applicability.
Education is not a blanket exemption. Security owns applicability;
Architecture links decisions to data flows, safeguards and resilience.
Technical validation grants no legal approval; preserve historical evidence.

Use regulatory-applicability-template as the index and link
gdpr-applicability-template, ai-act-applicability-template,
cra-applicability-template, nis2-applicability-template and
dora-applicability-template where relevant. An unfilled record is not evidence.

## Nutzungshilfe / Reader help

DE: Sichere Entwicklung planen und begründen, ohne eine bestandene Sicherheitsprüfung vorzutäuschen. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-security-governance/blob/main/docs/user-guide.md).

EN: Plan and explain secure development without claiming a passed security assessment. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-security-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
