# Cross-Platform Governance Preset

<!-- current-package: 0.2.6 -->

Paketkennung: **v0.2.6**. [Vorbereitung und Veröffentlichungsgrenze](docs/release-0.2.6-validation.md). Die Release-Seite und der externe Archivnachweis belegen die Veröffentlichung, nicht dieser Vorbereitungsstand. Frühere Versionsnachweise unten bleiben historisch.

<span lang="en">Package identity: v0.2.6. The release page and external archive evidence prove publication, not this preparation snapshot. Earlier version records below remain historical.</span>

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](docs/release-0.2.4-validation.md).

Der unten verlinkte Basispaket-Nachweis bleibt historisch; dieses Paket nimmt die Quellenkorrekturen als neue Version auf.

<span lang="en">The linked base-package evidence remains historical; this package includes the source corrections as a new version.</span>

**Leserpfad / Reader path:** [Deutsch: Einstieg, Begriffe und Fehlerhilfe](docs/user-guide.md) · [English: start, terms and troubleshooting](docs/user-guide.EN.md).

Historisches Basispaket **v0.2.4**. Der [Release-Stand](https://github.com/hindermath/spec-kit-preset-cross-platform-governance/releases) wird getrennt geprüft; ein Commit auf main ist noch keine Veröffentlichung oder Installation.

*Historical base package v0.2.4. The release page records actual publication separately; a main-branch commit is not publication or installation.*


Patch **0.2.4** liefert die verbesserte zweisprachige Einstiegs- und
Kompositionsdokumentation. Verhalten und Berechtigungsgrenzen bleiben erhalten.
[Release-Nachweis / Release record](docs/release-0.2.4-validation.md).

*Patch 0.2.4 packages the improved bilingual getting-started and composition
guide. Behavior and authority boundaries remain unchanged.*

Version: `0.2.5`
Status: Veröffentlichung über die Release-Seite prüfen / verify publication on the release page
Priority: `50`
Requires: Spec-Kit `>=0.8.0` (uses the `wrap` and `append` composition
strategies introduced in `0.8.x`).

## Zweck / Purpose

### Einstieg und Arbeitsweise / Getting Started and Workflow

Installiere im initialisierten Zielprojekt. Das [Manifest](preset.yml)
ergänzt Specify/Plan/Tasks mit `wrap` und Dokumentvorlagen/Constitution mit
`append`. Es erzeugt weder automatisch eine zweite Skriptvariante noch
Manpages, Hilfe oder native Testergebnisse. Nenne relevante Starter und
Zielpfade ausdrücklich; das separate Agent-Guidance-Addendum wird nicht durch
seine Registrierung zum Nachweis synchroner Agenten-Dateien.

*Install in an initialized target project. The [manifest](preset.yml) wraps
Specify/Plan/Tasks and appends to document templates/constitution. It does not
automatically produce a second script variant, man pages, help or native test
results. Explicitly name starters and output paths; registering the separate
agent-guidance addendum does not prove synchronized agent files.*

Beispiel für `check-example.sh` und `check-example.ps1`: Plane mit
`script-parity-checklist-template` eine Checkliste unter
`docs/cross-platform/check-example.md`, mit `man-page-template` die
`docs/man/check-example.1` und mit `powershell-help-template` die Hilfe im
PowerShell-Skript. Specify benennt Plattformen und Verhalten, Plan die
Paritätsfälle, Tasks die Implementierung, Hilfe und nativen Tests.

*For check-example.sh/check-example.ps1, use `script-parity-checklist-template`
for `docs/cross-platform/check-example.md`, `man-page-template` for
`docs/man/check-example.1`, and `powershell-help-template` for script help.
Specify names platforms and behavior, Plan parity scenarios, Tasks
implementation, documentation and native tests. Outputs are examples only.*

Vorhandene Evidence gezielt aktualisieren. Eine Syntaxprüfung ist kein
nativer Plattformnachweis; unverfügbare Plattformen als nicht geprüft
ausweisen. `N/A` braucht Begründung, `Open` Owner und Folgeaktion. Eine
leere Checkliste belegt keine Parität.

*Update existing evidence deliberately. Syntax parsing is not native platform
proof; report unavailable platforms as untested. Justify N/A and assign an
owner/follow-up to Open. An empty checklist does not prove parity.*

Kleinere Prioritätszahlen haben höhere Auflösungspriorität, nicht frühere
Testausführung. `append` ergänzt, `wrap` verschachtelt; `replace` und lokale
Overrides können gleichnamige Inhalte verdrängen. Prüfe individuelle
Kombinationen über `specify preset resolve plan-template` und die erzeugten
Agentenflächen; Priorität ersetzt keine Tests oder Freigaben.

*Lower numbers mean higher resolution precedence, not earlier execution.
Append adds, wrap nests; replace and local overrides can hide same-name
content. Inspect custom combinations through plan-template resolution and
generated agent surfaces. Priority replaces neither testing nor approval.*

Leserpfad / Reader path: [Installation](#installation--installation) →
[Evidenzvorlagen / Evidence Templates](#evidenzvorlagen--evidence-templates) →
[Prüfung / Verification](#prüfung--verification) →
[Fehlersuche / Troubleshooting](#fehlersuche--troubleshooting).

Dieses Standard-Preset stellt sicher, dass scriptförmige Werkzeuge
plattformübergreifend gedacht, dokumentiert und geprüft werden. Bash ist die
natürliche macOS/Linux-Fläche; PowerShell 7 ist die Windows-Fläche und häufig
zusätzlich eine portable Alternative.

*This standard preset ensures that script-shaped tools are designed,
documented, and checked cross-platform. Bash is the natural macOS/Linux
surface; PowerShell 7 is the Windows surface and often an additional portable
alternative.*

Das Qualitätsziel ist Parität: dieselbe fachliche Funktion, vergleichbare
Dry-Run-/WhatIf-Semantik, robuste Fehlerbehandlung und passende Dokumentation
für beide Varianten.

*The quality goal is parity: the same functional behavior, comparable dry-run
or WhatIf semantics, robust error handling, and suitable documentation for
both variants.*

## Zielgruppen / Audience

Dieses Preset eignet sich, wenn:

- Repositories Shell- oder Automationswerkzeuge für mehr als ein Betriebssystem
  bereitstellen;
- macOS-, Linux- und Windows-Mitwirkende dieselben Workflows nutzen sollen;
- Script-Tools als echte Produktoberfläche mit Dokumentation behandelt werden;
- Lernende den Unterschied zwischen OS-naher Umsetzung und fachlicher Parität
  nachvollziehen sollen.

*Use this preset when repositories ship shell or automation tools for more
than one operating system, when macOS, Linux, and Windows contributors should
use the same workflows, when scripts are treated as a documented product
surface, or when learners should understand the difference between platform
implementation and functional parity.*

## Lieferumfang / What It Provides

- Addenda für Constitution, Spec, Plan, Tasks und Agent-Guidance
- Wrapper für `speckit.specify`, `speckit.plan` und `speckit.tasks`
- Templates für Script-Parity-Checkliste, Unix-Manpage und bilinguale
  PowerShell-Hilfe
- auditfähige Spec-Kit-Run-Evidence-Felder für Anwendbarkeit, `N/A`-Begründung,
  Reviewer, Evidence-Pfad, Restrisiko und Follow-up

*The preset provides addenda for the main Spec-Kit artifacts and agent
guidance, wraps the normal Specify/Plan/Tasks flow, and supplies templates for
script parity checklists, Unix man pages, and bilingual PowerShell help.*

## Nicht enthalten / What It Does Not Provide

Das Preset schreibt keine Skripte um, führt keine Plattformtests aus und
garantiert keine Verfügbarkeit von macOS-, Linux- oder Windows-Runnern. Es
erteilt keine Repository-, Merge-, Deployment- oder Provider-
Administrationsrechte.

*The preset does not rewrite scripts, execute platform tests, or guarantee
availability of macOS, Linux, or Windows runners. It grants no repository,
merge, deployment, or provider-administration authority.*

## Voraussetzungen / Prerequisites

1. kompatible GitHub Spec-Kit CLI;
2. gültige Spec-Kit-Integration im Ziel-Repository;
3. versionierte Constitution und Agent-Guidance;
4. Zielplattformen und unterstützte Shells sind benannt;
5. Dokumentationsorte sind geklärt, standardmäßig `docs/man/` und
   `docs/cross-platform/`.

*Before installation, use a compatible GitHub Spec-Kit CLI, a valid Spec-Kit
integration, versioned constitution and agent guidance, named target
platforms and supported shells, and clear documentation locations.*

## Installation / Installation

### Veröffentlichter Tag / Published Tag

```bash
specify preset add --from https://github.com/hindermath/spec-kit-preset-cross-platform-governance/archive/refs/tags/v0.2.6.zip --priority 50
specify preset info cross-platform-governance
```

### Entwicklungs-Checkout / Development Checkout

```bash
specify preset add --dev /path/to/cross-platform-governance --priority 50
specify preset info cross-platform-governance
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

Priorität 50 kombiniert dieses Preset mit A11Y in der empfohlenen
Komposition; sie definiert keine zeitliche Ausführung der Tests.

*Priority 50 combines this preset with A11Y in the recommended composition;
it does not define a chronological test schedule.*

## Quellkapitel / Source Chapter

- `II. Cross-Platform Parity & Documentation`

## Regeln / Rules

- verpflichtende Paarungsregel: `*.sh` für macOS/Linux, `*.ps1` für Windows
  und als portable Alternative
- Definition of Done: gepaartes Skript, Manpage, bilinguale Hilfe und
  genehmigter `Verb-Noun` Cmdlet-Name
- Bash-Disziplin: `set -euo pipefail`, Variablen quoten, keine unsicheren
  Option-Übergänge
- PowerShell-Disziplin: `Set-StrictMode -Version Latest`, `-NoProfile`, kein
  `Invoke-Expression` auf nicht vertrauenswürdigem Input
- Plattform-Grenzen: Bash 3.x als macOS-Default, leeres `$env:HOME` unter
  Windows, Pfad- und Encoding-Unterschiede
- Dry-Run- und `-WhatIf`-Parität

## Preset-Strategie / Preset Strategy

- Cross-Platform-Governance an `constitution-template`, `spec-template`,
  `plan-template` und `tasks-template` anhängen
- ein eigenständiges Agent-Guidance-Addendum bereitstellen
- `speckit.specify`, `speckit.plan` und `speckit.tasks` mit einem
  Cross-Platform-Workflow umhüllen
- Starter-Templates für Paritätscheckliste, Unix-Manpage und PowerShell-Hilfe
  liefern

## Evidenzvorlagen / Evidence Templates

- `script-parity-checklist-template`
- `man-page-template`
- `powershell-help-template`

Standard-Evidence-Orte / Default evidence locations:

- Manpages / man pages: `docs/man/<name>.1`
- Paritätschecklisten / parity checklists: `docs/cross-platform/` oder
  neben dem Skript / or alongside the script

## Prüfung / Verification

```bash
specify preset list
specify preset info cross-platform-governance
specify preset resolve script-parity-checklist-template
specify check
```

Prüfe zusätzlich, ob neue oder geänderte Skripte entweder eine begründete
Ein-Plattform-Ausnahme oder eine Bash-/PowerShell-Parität mit Evidence haben.

*Also verify that new or changed scripts have either a justified
single-platform exception or Bash/PowerShell parity with evidence.*

## Fehlersuche / Troubleshooting

### Nur ein Skript existiert / Only One Script Exists

Dokumentiere, ob die zweite Plattformvariante erforderlich ist. Wenn ja, muss
sie in Tasks auftauchen; wenn nein, braucht die Ausnahme eine Begründung.

### Dry-Run driftet / Dry-Run Drifts

Vergleiche Bash `--dry-run` und PowerShell `-WhatIf`. Beide müssen dieselben
fachlichen Änderungen simulieren.

### Dokumentation fehlt / Documentation Is Missing

Bash-Skripte brauchen eine Unix-Manpage. PowerShell-Skripte brauchen
vollständige bilinguale comment-based help und einen passenden `Verb-Noun`
Cmdlet-Namen.

## Sicherheit und Grenzen / Safety and Boundaries

- Installation adds parity rules, templates, and wrapped Spec-Kit command
  guidance; it does not rewrite scripts or execute platform tests by itself.
- Cross-platform support claims require actual Bash and PowerShell evidence.
- The preset does not grant repository, remote, merge, deployment, or
  provider-administration authority.

## Version 0.2.2 / Version 0.2.2

`v0.2.2` veröffentlicht den agentenneutralen `model-routing.json`-Vertrag für
die Komposition mit Model Routing Governance; Plattformregeln bleiben
unverändert.

*`v0.2.2` publishes the agent-neutral `model-routing.json` contract for
composition with Model Routing Governance; platform rules are unchanged.*

## Version 0.2.1 / Version 0.2.1

`v0.2.1` ergänzt read-only Check-Parität, Root-Dateipfadtests, explizit
begrenzte native Overrides und eine klare Trennung von OS- und Providerbelegen.

*`v0.2.1` adds read-only check parity, root-file path tests, explicitly scoped
native overrides, and clear separation of operating-system and provider proof.*

## Version 0.2.0 / Version 0.2.0

`v0.2.0` ergänzt auditfähige Spec-Kit-Run-Evidence-Felder, damit generierte
Markdown-Dokumente und Checklisten Anwendbarkeit, `N/A`-Begründung, Reviewer,
Evidence-Pfad, Restrisiko und Follow-up pro standardsrelevantem Spec-Kit-Lauf
aufzeichnen können.

*`v0.2.0` adds audit-ready Spec-Kit run evidence fields so generated Markdown
documents and checklists can record applicability, `N/A` rationale, reviewer,
evidence path, residual risk, and follow-up per standards-relevant Spec-Kit
run.*

## Kompatibilitäts- und Sicherheitsübersicht / Compatibility and Safety Summary

- kompatibel mit Spec-Kit `>=0.8.0`
- priorisiert als Stufe `50`
- ergänzt A11Y durch plattformübergreifende Script- und Hilfe-Parität
- keine automatischen Plattform-, Remote-, Merge- oder Provider-Aktionen
- Paritätsbehauptungen bleiben projektbezogene Evidence

## License

MIT. See `LICENSE`.
