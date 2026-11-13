---
name: speckit-plan
description: 'Spec-kit workflow command: speckit-plan'
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: security-governance v0.7.5:speckit.plan.md
---

Before continuing, apply the Security Governance preset:

- plan explicit MSL applicability or non-MSL justification work when relevant
- plan explicit secure-development verification work
- plan dependency and supply-chain evidence updates where relevant
- surface security review checkpoints instead of leaving them implicit

Before continuing, apply the Architecture Governance preset:

- plan explicit architecture evidence work
- plan threat-model and ADR updates when boundaries, integrations, or flows
  change
- surface Zero Trust and SAMM work explicitly when relevant
- surface BSI C3A cloud autonomy work explicitly for cloud services and
  provider-dependent deployments

Before continuing, apply the iSAQB Architecture Governance preset:

- plan explicit architecture work products where the feature changes
  structure, interfaces, quality attributes, runtime behavior, or
  deployment
- plan updates to architecture views under `docs/architecture/`
- plan ADRs for architecturally significant decisions
- plan risk and technical-debt review for trade-offs or unresolved
  constraints
- if security-relevant architecture is affected, also plan the
  secure-architecture evidence from `architecture-governance`

Before continuing, apply the A11Y Governance preset:

- plan accessibility review work explicitly
- plan bilingual content work explicitly
- include CLI accessibility checks where user-facing terminal output is changed

Before continuing, apply the Cross-Platform Governance preset:

- plan paired Bash + PowerShell script work as a single unit
- plan the man-page, the bilingual PowerShell help block, and the
  `Verb-Noun` Cmdlet alongside the script
- plan manual verification on at least one target OS per variant
- plan implementation discipline checks (Bash quoting, `set -euo
  pipefail`, `Set-StrictMode -Version Latest`, `-NoProfile`) and the
  parity-checklist artefact

Before continuing, apply the Agent Parity Governance preset:

- plan an atomic update across all maintained agent surfaces
- plan synchronised updates to project templates and the local
  `.specify/memory/constitution.md`
- plan a parity-verification artefact for the change

# Command Template: `/speckit.plan`

Use this command to produce an implementation plan from an approved specification.

## Required Actions

1. Populate technical context with real stack details.
2. Execute the Constitution Check gates explicitly:
   - branching and PR flow
   - .NET 10 + C# 14.0 toolchain alignment
   - architecture/layer boundaries
   - iSAQB/arc42 architecture evidence under `docs/architecture/`
   - bilingual CEFR B2 documentation scope
   - XML documentation + DocFX regeneration scope
   - Red-Green-Refactor testing scope
   - coverage gate (`>=70%` minimum, `>=80%` target)
   - NuGet dependency currency and pinning exceptions
   - serialization/data conventions
3. Document concrete project structure for this feature.
4. Record justified exceptions in Complexity Tracking.
5. Complete the Autonomous Execution Contract with delivery authority,
   evidence-first setup, one representative vertical slice, convergence gates,
   shared single-writer files, trigger-based validation, scope firewall, and
   remote closeout when delegated.

<!-- source-reference-policy:begin -->
6. Apply the source-reference policy when history or modernization is material:
   current TuiVision contracts are normative; inspect `magiblot/tvision` first
   at commit `57b6f56b38e0ee75240a80a10ee0e11470c24693`, tree
   `96dd03873955689ff0a79f6c8107a8148fe1ebd6`, as a non-normative modern
   design reference; then inspect historical and material consumer sources.
   Record exactly `AdoptModernization`, `PreserveHistoricalIntent`,
   `IntentionalTuiVisionDeviation`, or `N/A`. Source rank alone never resolves
   conflicts. Keep external sources uncopied and record
   `MultipartNotRepositoryWideMIT`. Apply this `Prospective`; re-evaluate only
   for a changed contract, new approved pin, or materially new consumer
   evidence. Moving branches are not evidence.
<!-- source-reference-policy:end -->

## Validation Checklist

- No gate is left unresolved without rationale.
- Test, coverage, dependency, and documentation impacts are planned before implementation.
- Architecture evidence and justified `N/A` decisions are planned before implementation.
- Evidence exists before implementation and each iterative stage has a measurable stop condition.


Audit-ready evidence requirement:

- Ensure this plan wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
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

- Ensure this plan wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
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

- Ensure this plan wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
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

- Ensure this plan wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
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

- Ensure this plan wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
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

- Ensure this plan wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
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
