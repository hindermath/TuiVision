# Agent Parity Governance Preset

Version: `0.4.0`
Status: published, standard governance preset
Priority: `60`
Requires: Spec-Kit `>=0.8.0` (uses the `wrap` and `append` composition
strategies introduced in `0.8.x`).

## Zweck / Purpose

Dieses Standard-Preset verhindert stillen Drift zwischen KI-Agenten-Dateien.
Gemeinsame Regeln sollen atomar auf allen gepflegten Agent-Flächen landen,
absichtliche Abweichungen sollen dokumentiert sein, und Modell-Routing soll
agentenneutral bleiben.

*This standard preset prevents silent drift between AI-agent guidance files.
Shared rules should land atomically on every maintained agent surface,
intentional deviations should be documented, and model routing should remain
agent-neutral.*

Das Qualitätsziel ist reproduzierbare Governance: Feature-Spezifikationen
sollen fachliche Anforderungen enthalten, aber keine flüchtigen
Provider-Modellnamen oder agentenspezifischen Betriebsentscheidungen.

*The quality goal is reproducible governance: feature specifications should
contain product requirements, not volatile provider model names or
agent-specific operating decisions.*

## Zielgruppen / Audience

Dieses Preset eignet sich, wenn:

- ein Projekt mehr als eine Agent-Guidance-Datei pflegt;
- Teams gemeinsame Regeln in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
  Copilot-Dateien oder ähnlichen Flächen synchron halten möchten;
- Modell-Routing gewünscht ist, ohne Feature-Artefakte an einen Anbieter oder
  Modellnamen zu binden;
- Agent-Flächen Teil des Contributor-Vertrags sind.

*Use this preset when a project maintains more than one agent guidance file,
when teams need shared rules synchronized across AGENTS, Claude, Gemini,
Copilot, or similar surfaces, when model routing is desired without binding
feature artifacts to a provider or model name, or when agent surfaces are part
of the contributor contract.*

## Lieferumfang / What It Provides

- Addenda für Constitution, Spec, Plan, Tasks und Agent-Guidance
- Wrapper für `speckit.specify`, `speckit.plan` und `speckit.tasks`
- `agent-parity-checklist-template` als überprüfbares Artefakt
- agentenneutrales Modell-Routing nach Arbeitstyp
- Regeln für atomare Änderungen, Template-Synchronisierung und dokumentierte
  Abweichungen
- auditfähige Spec-Kit-Run-Evidence-Felder für Anwendbarkeit, `N/A`-Begründung,
  Reviewer, Evidence-Pfad, Restrisiko und Follow-up

*The preset provides addenda for the main Spec-Kit artifacts and agent
guidance, wraps the normal Specify/Plan/Tasks flow, supplies an agent-parity
checklist, adds agent-neutral model routing by work type, and defines atomic
change, template synchronization, and intentional-deviation rules.*

## Nicht enthalten / What It Does Not Provide

Das Preset wählt keinen konkreten Anbieter, kein konkretes Modell, kein
Reasoning-Level und keinen Agenten für einen einzelnen Lauf aus. Es führt keine
Agenten aus und erteilt keine Repository-, Merge-, Bypass-, Deployment-,
Cancellation-, Secret- oder Provider-Administrationsrechte.

*The preset does not choose a concrete provider, model, reasoning level, or
agent for an individual run. It does not execute agents and grants no
repository, merge, bypass, deployment, cancellation, secret, or
provider-administration authority.*

## Voraussetzungen / Prerequisites

1. kompatible GitHub Spec-Kit CLI;
2. gültige Spec-Kit-Integration im Ziel-Repository;
3. deklarierte Liste gepflegter Agent-Flächen;
4. versionierte Constitution und Agent-Guidance;
5. ein Teamverständnis, welche Regeln gemeinsam und welche absichtlich
   agentenspezifisch sind.

*Before installation, use a compatible GitHub Spec-Kit CLI, a valid Spec-Kit
integration, a declared list of maintained agent surfaces, versioned
constitution and agent guidance, and a team understanding of which rules are
shared and which are intentionally agent-specific.*

## Installation / Installation

### Veröffentlichter Tag / Published Tag

```bash
specify preset add \
  --from https://github.com/hindermath/spec-kit-preset-agent-parity-governance/archive/refs/tags/v0.4.0.zip \
  --priority 60
specify preset info agent-parity-governance
```

### Entwicklungs-Checkout / Development Checkout

```bash
specify preset add --dev /path/to/agent-parity-governance --priority 60
specify preset info agent-parity-governance
```

### Empfohlene Kombination / Recommended Composition

| Priority | Preset |
|---:|---|
| 10 | `security-governance` |
| 20 | `architecture-governance` |
| 30 | `isaqb-architecture-governance` |
| 40 | `a11y-governance` |
| 50 | `cross-platform-governance` |
| 60 | `agent-parity-governance` |

Dieses Preset kommt nach den fachlichen Governance-Presets, damit deren
gemeinsame Regeln auf allen Agent-Flächen synchron gehalten werden.

*This preset runs after the domain governance presets so their shared rules
can be synchronized across all maintained agent surfaces.*

## Quellkapitel / Source Chapter

- `IX. Agent Guidance Parity & Template Synchronization`

## Regeln / Rules

- Projekt deklariert die gepflegten Agent-Flächen.
- Gemeinsame Regeln werden atomar auf alle diese Flächen angewendet.
- Änderungen an Templates und `.specify/memory/constitution.md` werden
  zusammen geprüft.
- Absichtliche Abweichungen werden in derselben Änderung dokumentiert.
- Modellwahl ist Routing-Guidance, keine Feature-Anforderung.
- Keine Provider-Modellnamen in `spec.md`, `plan.md`, `tasks.md` oder
  Feature-Spezifikationen.

Typische Flächen sind `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
`.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`,
`.cursorrules`, `.windsurfrules`, `JUNIE.md` oder andere projektspezifische
Instruktionsdateien. Das Preset verlangt keinen bestimmten Anbieter- oder
Agentenmix.

*Common surfaces include `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
`.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`,
`.cursorrules`, `.windsurfrules`, `JUNIE.md`, or other project-specific
instruction files. The preset does not require a specific vendor or agent mix.*

## Modell-Routing / Model Routing

- Starke Reasoning-/Coding-Modelle für `speckit.specify`,
  `speckit.clarify`, `speckit.plan`, `speckit.tasks` und `speckit.analyze`.
- Long-running-Agent-Modell für vollständige lange
  `speckit.implement`-Läufe.
- Coding-optimierte Modelle für fokussierte Reviews oder CI-Fixes.
- Kleine schnelle Coding-Modelle für triviale mechanische Änderungen.
- Der jeweils verwendete Agent ordnet diese Kategorien seinen aktuell besten
  verfügbaren Modellen selbst zu.

*Use strong reasoning/coding models for specification, clarification,
planning, tasks, and analysis; long-running agent models for complete
implementation runs; coding-optimized models for reviews or CI fixes; and
small fast coding models for trivial mechanical edits. The active agent maps
these categories to its currently best available models.*

## Preset-Strategie / Preset Strategy

- Parity-Governance an `constitution-template`, `spec-template`,
  `plan-template` und `tasks-template` anhängen
- ein eigenständiges Agent-Guidance-Addendum bereitstellen
- `speckit.specify`, `speckit.plan` und `speckit.tasks` mit einem
  Parity-Workflow umhüllen
- eine Paritätscheckliste als Starter-Artefakt liefern

## Evidenzvorlagen / Evidence Templates

- `agent-parity-checklist-template`

Standard-Evidence-Ort: projektdefiniert, häufig `docs/agent-governance/` oder
neben den geänderten Guidance-Dateien.

*Default evidence location: project-defined; commonly
`docs/agent-governance/` or alongside the changed guidance files.*

## Prüfung / Verification

```bash
specify preset list
specify preset info agent-parity-governance
specify preset resolve
specify check
```

Prüfe zusätzlich, ob dieselbe gemeinsame Regel auf allen deklarierten
Agent-Flächen vorhanden ist und ob Modellnamen aus Feature-Artefakten
herausgehalten wurden.

*Also verify that the same shared rule exists on every declared agent surface
and that model names stayed out of feature artifacts.*

## Fehlersuche / Troubleshooting

### Eine Agent-Datei fehlt / One Agent File Is Missing

Prüfe zuerst, ob die Datei zur deklarierten Projektfläche gehört. Wenn ja,
muss die gemeinsame Änderung nachgezogen werden; wenn nein, dokumentiere die
Nichtzuständigkeit.

### Modellnamen stehen in Specs / Model Names Are in Specs

Entferne sie aus `spec.md`, `plan.md`, `tasks.md` und Feature-Spezifikationen.
Modell-Routing gehört in Agent-Guidance oder Run-Konfiguration, nicht in
reproduzierbare Feature-Anforderungen.

### Agenten brauchen verschiedene Details / Agents Need Different Details

Halte die gemeinsame Regel identisch und dokumentiere agentenspezifische
Abweichungen ausdrücklich in derselben Änderung.

## Einsatz / When to Use

- any project that maintains more than one AI-agent guidance file
- any team that wants atomic, auditable changes to shared agent instructions
- any project where AI-agent surfaces are part of the contributor contract
- any project that wants model routing guidance without pinning a
  provider-specific model in specs, plans, or tasks

## Nicht verwenden / When Not to Use

- projects with only one agent guidance file and no plans to add another
- one-off prototypes without long-term agent contributors

## Sicherheit und Grenzen / Safety and Boundaries

- Model selection is routing guidance, not a feature requirement.
- Do not write provider-specific model names into `spec.md`, `plan.md`,
  `tasks.md`, or feature specifications.
- Installation does not grant repository, remote, merge, bypass, deployment,
  cancellation, secret, or provider-administration authority.

## Version 0.4.0 / Version 0.4.0

`v0.4.0` ergänzt Flottenabschluss-Nachweise über alle Repository-Ebenen,
Pfadabgrenzung vor Staging, Parität generierter Agentenbefehle und
geheimnisfreie Runner-/Statusmetadaten mit Kampagnen-Fallback.

*`v0.4.0` adds fleet-completion evidence across repository levels, path
separation before staging, generated-agent command parity, and secret-free
runner/status metadata with campaign fallback.*

## Version 0.3.0 / Version 0.3.0

`v0.3.0` ergänzt auditfähige Spec-Kit-Run-Evidence-Felder, damit generierte
Markdown-Dokumente und Checklisten Anwendbarkeit, `N/A`-Begründung, Reviewer,
Evidence-Pfad, Restrisiko und Follow-up pro standardsrelevantem Spec-Kit-Lauf
aufzeichnen können.

*`v0.3.0` adds audit-ready Spec-Kit run evidence fields so generated Markdown
documents and checklists can record applicability, `N/A` rationale, reviewer,
evidence path, residual risk, and follow-up per standards-relevant Spec-Kit
run.*

## Kompatibilitäts- und Sicherheitsübersicht / Compatibility and Safety Summary

- kompatibel mit Spec-Kit `>=0.8.0`
- priorisiert als Stufe `60`
- hält Modell-Routing agentenneutral und aus Feature-Artefakten heraus
- keine automatischen Agent-, Remote-, Merge- oder Provider-Aktionen
- Paritätsbehauptungen bleiben projektbezogene Evidence

## License

MIT. See `LICENSE`.
