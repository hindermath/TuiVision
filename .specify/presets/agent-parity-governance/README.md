# Agent Parity Governance Preset

<!-- current-package: 0.4.6 -->

Paketkennung: **v0.4.6**. [Vorbereitung und Veröffentlichungsgrenze](docs/release-0.4.6-validation.md). Die Release-Seite und der externe Archivnachweis belegen die Veröffentlichung, nicht dieser Vorbereitungsstand. Frühere Versionsnachweise unten bleiben historisch.

<span lang="en">Package identity: v0.4.6. The release page and external archive evidence prove publication, not this preparation snapshot. Earlier version records below remain historical.</span>

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](docs/release-0.4.4-validation.md).

Der unten verlinkte Basispaket-Nachweis bleibt historisch; dieses Paket nimmt die Quellenkorrekturen als neue Version auf.

<span lang="en">The linked base-package evidence remains historical; this package includes the source corrections as a new version.</span>

**Leserpfad / Reader path:** [Deutsch: Einstieg, Begriffe und Fehlerhilfe](docs/user-guide.md) · [English: start, terms and troubleshooting](docs/user-guide.EN.md).

Historisches Basispaket **v0.4.4**. Der [Release-Stand](https://github.com/hindermath/spec-kit-preset-agent-parity-governance/releases) wird getrennt geprüft; ein Commit auf main ist noch keine Veröffentlichung oder Installation.

*Historical base package v0.4.4. The release page records actual publication separately; a main-branch commit is not publication or installation.*


Patch **0.4.4** liefert die verbesserte zweisprachige Einstiegs- und
Kompositionsdokumentation. Verhalten und Berechtigungsgrenzen bleiben erhalten.
[Release-Nachweis / Release record](docs/release-0.4.4-validation.md).

*Patch 0.4.4 packages the improved bilingual getting-started and composition
guide. Behavior and authority boundaries remain unchanged.*

Version: `0.4.5`
Status: Veröffentlichung über die Release-Seite prüfen / verify publication on the release page
Priority: `60`
Requires: Spec-Kit `>=0.8.0` (uses the `wrap` and `append` composition
strategies introduced in `0.8.x`).

Seit Version `0.4.1` behandelt das Preset einen projektspezifischen Lernenden- und
Barrierefreiheitsvertrag als gemeinsame Regel. Zielgruppe, Vorwissen,
Sprachreihenfolge, Lesbarkeit, Begriffserklärungen und textorientierte
Informationswege müssen auf allen gepflegten Agenten- und Template-Flächen
semantisch gleich bleiben.

*Since version `0.4.1`, the preset treats a project-specific learner and accessibility contract
as shared guidance. Audience, prior knowledge, language order, readability,
term explanations, and text-first information paths must remain semantically
equivalent across maintained agent and template surfaces.*

## Zweck / Purpose

### Einstieg und Arbeitsweise / Getting Started and Workflow

Nach Installation im initialisierten Zielprojekt ergänzt das
[Manifest](preset.yml) Specify/Plan/Tasks mit `wrap` sowie ihre
Dokumentvorlagen und Constitution mit `append`. Es synchronisiert dadurch
keine vorhandenen Guidance-Dateien automatisch und startet keine Agenten.
Die eigenständige Checkliste und das Agent-Guidance-Addendum müssen bewusst
verwendet werden; ein registrierter Starter ist noch kein Nachweis.

*After installation in an initialized target project, the [manifest](preset.yml)
wraps Specify/Plan/Tasks and appends to their templates and constitution.
This does not automatically synchronize existing guidance files or launch
agents. Use the standalone checklist and guidance addendum deliberately;
a registered starter is not evidence.*

Beispiel: Eine gemeinsame Logging-Regel ändert sich. Benenne in Specify alle
vom Projekt gepflegten Agenten-Dateien. Plane atomare Updates und mit
`agent-parity-checklist-template` den Nachweis
`docs/agent-governance/logging-parity.md`. Tasks nennt alle betroffenen
Dateien, Template-/Constitution-Prüfung und semantischen Vergleich. Halte
beabsichtigte Unterschiede fest; lege keine ungenutzten Agenten-Dateien nur
für dieses Beispiel an. Keine konkreten Modellnamen in Feature-Anforderungen.

*Example: a shared logging rule changes. Name all project-maintained agent
files in Specify. Plan atomic updates and use `agent-parity-checklist-template`
for `docs/agent-governance/logging-parity.md`. Tasks names affected files,
template/constitution checks and semantic comparison. Record intentional
differences; do not create unused agent files just for this example. Keep
concrete model names out of feature requirements.*

Vorhandene Evidence aktualisieren. `Applicable` bedeutet nicht „abgenommen“;
`N/A` braucht Grund, `Open` Owner und Folgeaktion. Zeichenidentität allein
belegt keine fachliche Parität. Konkrete Modellbindung bleibt bei
[Model Routing Governance](https://github.com/hindermath/spec-kit-preset-model-routing-governance),
nicht bei einem automatisch gestarteten Agenten.

*Update existing evidence. Applicable is not accepted; justify N/A and assign
an owner/follow-up to Open. Byte equality alone does not establish semantic
parity. Concrete local model binding belongs to the linked Model Routing
preset; parity installation starts no agent.*

Priorität ist Auflösungsrang: kleinere Zahlen haben höhere Priorität.
`append` ergänzt, `wrap` verschachtelt; `replace` und Projekt-Overrides können
gleichnamige Beiträge verdrängen. Individuelle Reihenfolgen sind nicht
pauschal geprüft. Prüfe `specify preset resolve plan-template` und die
erzeugten Agentenflächen, statt aus Priorität auf Ausführung zu schließen.

*Priority is resolution precedence: lower numbers have higher precedence.
Append adds, wrap nests; replace and project overrides can hide same-name
contributions. Custom orderings are not universally tested. Inspect
plan-template resolution and generated agent surfaces, not an assumed schedule.*

Leserpfad / Reader path: [Installation](#installation--installation) →
[Regeln / Rules](#regeln--rules) → [Prüfung / Verification](#prüfung--verification) →
[Fehlersuche / Troubleshooting](#fehlersuche--troubleshooting).

Dieses Standard-Preset unterstützt die Prüfung auf Drift zwischen KI-Agenten-Dateien.
Gemeinsame Regeln sollen atomar auf allen gepflegten Agent-Flächen landen,
absichtliche Abweichungen sollen dokumentiert sein, und Modell-Routing soll
agentenneutral bleiben.

*This standard preset supports checking drift between AI-agent guidance files.
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
specify preset add --from https://github.com/hindermath/spec-kit-preset-agent-parity-governance/archive/refs/tags/v0.4.6.zip --priority 60
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
| 10 | [security-governance](https://github.com/hindermath/spec-kit-preset-security-governance) |
| 20 | [architecture-governance](https://github.com/hindermath/spec-kit-preset-architecture-governance) |
| 30 | [isaqb-architecture-governance](https://github.com/hindermath/spec-kit-preset-isaqb-architecture-governance) |
| 40 | [a11y-governance](https://github.com/hindermath/spec-kit-preset-a11y-governance) |
| 50 | [cross-platform-governance](https://github.com/hindermath/spec-kit-preset-cross-platform-governance) |
| 60 | [agent-parity-governance](https://github.com/hindermath/spec-kit-preset-agent-parity-governance) |

Priorität 60 ist die empfohlene Kompositionsposition nach den fachlichen
Governance-Presets, kein automatisch ausgeführter Synchronisationsschritt.

*Priority 60 is the recommended composition position after domain governance
presets, not an automatically executed synchronization step.*

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

Die folgende Übersicht erklärt typische Rollen. Maßgeblich bleibt der
konkrete Command-Vertrag; ein fachliches Intake- oder Assurance-Review kann
deshalb frontier-reasoning verlangen statt coding-review. Installation prüft
oder synchronisiert keine bestehenden Agenten-Dateien automatisch.

*The list explains typical roles. Actual command contracts remain authoritative:
semantic intake/assurance review may require frontier-reasoning rather than
coding-review. Installation does not automatically verify or synchronize
existing agent files.*

- `frontier-reasoning` für semantische Intake-Entscheidungen sowie Specify,
  Clarify, Plan, Tasks und Analyze.
- `long-running-implementation` für vollständige Implementierungsläufe.
- `coding-review` für Sicherheits-, Architektur-, A11Y- und Abschlussreviews.
- `fast-mechanical` für Read, Status, Next und risikoarme mechanische Arbeit.
- `script-only` für Stop und deterministische Validatoren ohne Modell.
- Jedes Preset deklariert seine Kommandos in `model-routing.json`. Bei mehreren
  Wrappern gewinnt die stärkste Rolle. Konkrete Modelle bleiben lokal.
- Nicht-parallele autonome Läufe wechseln Modelle nur zwischen Prozessen an
  validierten Phasengrenzen. Lokale Profile arbeiten `fail-closed`.

*Stable roles are `frontier-reasoning`, `long-running-implementation`,
`coding-review`, `fast-mechanical`, and `script-only`. Every preset declares
its command roles in `model-routing.json`; the strongest applicable wrapper
role wins while concrete models stay local. Non-parallel autonomous runs
change models only between processes at validated phase boundaries. Local
profiles fail closed.*

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
specify preset resolve agent-parity-checklist-template
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

## Version 0.4.2 / Version 0.4.2

`v0.4.2` veröffentlicht den agentenneutralen `model-routing.json`-Vertrag für
die Komposition mit Model Routing Governance. Konkrete Modellnamen bleiben
maschinenlokal und werden nicht zwischen Agentenflächen kopiert.

*`v0.4.2` publishes the agent-neutral `model-routing.json` contract for
composition with Model Routing Governance. Concrete model names remain local
to each machine and are not copied across agent surfaces.*

## Version 0.4.1 / Version 0.4.1

`v0.4.1` ergänzt die semantische Parität für projektdeklarierte Lernenden- und
Barrierefreiheitsverträge.

*`v0.4.1` adds semantic parity for project-declared learner and accessibility
contracts.*

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
