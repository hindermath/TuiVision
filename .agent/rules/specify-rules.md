# TuiVision Development Guidelines

Auto-generated from all feature plans. Last updated: 2026-07-17

## Active Technologies
- C# 14 / .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest 4.0.1 and .NET BCL; no new package (036-wave6-tvfm-showcase-remediation)
- Existing source-controlled fixtures copied into process- or test-owned temporary controlled roots; no database, service, host profile, shell, process, PTY, network, or arbitrary-user storage (036-wave6-tvfm-showcase-remediation)

## Project Structure

```text
src/
tests/
examples/
docs/guides/examples/
```

## Commands

```bash
dotnet restore
dotnet build --configuration Release
dotnet test
dotnet format --verify-no-changes
docfx docfx.json
```

## Code Style

C# 14 / .NET 10: Follow standard conventions

## Recent Changes
- 036-wave6-tvfm-showcase-remediation: Added C# 14 / .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest 4.0.1 and .NET BCL; no new package

## Spec-Kit-Modell-Routing / Spec Kit Model Routing

- Modellwahl ist operative Agenten-Routing-Guidance, keine Feature-Anforderung. Modellnamen nicht in `spec.md`, `plan.md`, `tasks.md` oder einzelne Feature-Specs schreiben; diese Artefakte muessen reproduzierbar bleiben, auch wenn Modellnamen wechseln oder ein anderer KI-Agent verwendet wird.
- Der jeweilige Agent soll diese Empfehlungen auf seine aktuell verfuegbaren Modelle abbilden; keine feste Anbieter- oder Modellbindung ableiten.
- Fuer Spec-Kit-Spezifikation, Klaerung, Planung, Tasks und Analyse (`/speckit-specify`, `/speckit-clarify`, `/speckit-plan`, `/speckit-tasks`, `/speckit-analyze`; je nach Agent auch `/speckit.specify` usw.) das staerkste verfuegbare Frontier-Reasoning-/Coding-Modell bevorzugen.
- Fuer vollstaendige, lang laufende `/speckit-implement`-Laeufe das staerkste verfuegbare Long-Running-Agent-Modell bevorzugen; das Frontier-Modell nutzen, wenn maximale Urteilsguete wichtiger ist als Laufzeitstabilitaet.
- Fuer fokussierte Reviews oder CI-Fixes ein coding-optimiertes Modell bevorzugen.
- Fuer triviale Bereinigung, Formatierung oder risikoarme mechanische Edits ist ein schnelles kleines Coding-Modell akzeptabel.

*Model choice is operational agent-routing guidance, not a feature requirement. Do not pin model names in `spec.md`, `plan.md`, `tasks.md`, or individual feature specs; those artifacts must stay reproducible even when model names change or another AI agent is used. Each agent should map these recommendations to its currently available models; do not derive a fixed vendor or model requirement. For Spec-Kit specification, clarification, planning, task generation, and analysis (`/speckit-specify`, `/speckit-clarify`, `/speckit-plan`, `/speckit-tasks`, `/speckit-analyze`; or `/speckit.specify` etc. depending on the agent surface), prefer the strongest available frontier reasoning/coding model. For complete long-running `/speckit-implement` runs, prefer the strongest available long-running agent model; use the frontier model when maximum judgment quality is more important than runtime stability. For focused review or CI fixes, prefer a coding-optimized model. For trivial cleanup, formatting, or low-risk mechanical edits, a fast small coding model is acceptable.*

## Autonome Spec-Kit-Läufe / Autonomous Spec-Kit Runs

- Vollständig delegierte Spec-Kit-Läufe folgen `docs/spec-kit-autonomous-runbook.md` und verwenden den projektgebundenen Skill `$speckit-autonomous`.
- Der aktuelle Benutzerauftrag bestimmt `LocalImplementation`, `PublishPR` oder `MergeAndSync`; allgemeine Autonomie erteilt keine stillschweigende Remote-Schreib- oder Merge-Berechtigung.
- Evidence entsteht vor der Implementierung. Iterative Stufen laufen bis zur definierten Konvergenz, ein vertikaler Slice kommt vor breiter Wiederholung und gemeinsame Schreiber bleiben serialisiert.
- Scope-Firewall, triggerbasierte Validierung und eine kurze Retrospektive sind Pflichtbestandteile jedes autonomen Laufs.

*Fully delegated Spec-Kit runs follow `docs/spec-kit-autonomous-runbook.md` and use the repository-local `$speckit-autonomous` skill. The current user request determines `LocalImplementation`, `PublishPR`, or `MergeAndSync`; general autonomy does not grant implicit remote write or merge authority. Create evidence before implementation, iterate to defined convergence, prove a vertical slice before broad rollout, serialize shared writers, protect scope, validate by trigger, and record a short retrospective. One manual build-counter increment covers exactly one explicit build or test invocation. Validation helpers receive an explicit repository root and pass only when both exit status and error channel are clean.*

## Spec-Kit Governance Presets

If this project installs governance presets, keep this section synchronized
with `.specify/presets/` and generated agent command files. C#/.NET Level-2
projects default to all seven home-baseline presets unless a justified exception
is documented: `security-governance`, `architecture-governance`,
`isaqb-architecture-governance`, `a11y-governance`,
`cross-platform-governance`, and `agent-parity-governance`.

`autonomous-run-governance` v0.2.2 mit Priorität 70 ist aus dem öffentlichen
Tag-ZIP installiert. Der projektgebundene Codex-Skill `$speckit-autonomous`
bleibt an seinem einzelnen Pfad als bewusster lokaler Override bestehen, weil
er TuiVision-spezifische Nummerierungs-, Build-Zähler-, DocFX-/A11Y- und
historische Source-Verträge ergänzt. Preset-Command, Retrospektiv-Skill,
Projekt-Runbook und Adoption-Evidence bleiben die portablen und gemeinsamen
Nachweisflächen. Version 0.2.2 behält deklarierte Acceptance-Gates, exakte
HEAD-Evidence, Status, Stop, Resume und validierten Laufzustand bei. Nach
Preset- oder Governance-Drift gleicht Resume neue zwingende Korrektheits-,
Sicherheits-, Berechtigungs- und Evidenzregeln mit akzeptierten Plan-, Task- und
Checklist-Artefakten ab; nur anwendbare fehlende Regeln werden in-place ergänzt
und erneut analysiert, reine Effizienzpräferenzen bleiben retrospektiv. Grüne
Namen, Validator und Bypass ersetzen weder technischen Nachweis noch Remote-
oder Merge-Berechtigung. `PausedByUser` wird nie stillschweigend fortgesetzt;
unsicher beendete Operationen bleiben `NeedsRevalidation`. Die lesbare Skill-Überschrift `Deliver` ist kein Run-State-Wert; für Remote-Closeout gelten `Publish`, `Review` oder `MergeAndSync`.

*`autonomous-run-governance` v0.2.2 at priority 70 is installed from the public
tag ZIP. Keep the project-owned Codex `$speckit-autonomous` skill at its single
path as an intentional local override because it adds TuiVision numbering,
build-counter, DocFX/A11Y, and historical-source contracts. The preset command,
retrospective skill, project runbook, and adoption evidence remain the portable
and shared proof surfaces. Version 0.2.2 retains declared acceptance gates,
exact-HEAD evidence, status, stop, resume, and validated run state. After preset
or governance drift, resume compares new mandatory correctness, security,
permission, and evidence rules with accepted Plan, Tasks, and checklist
artifacts; only applicable missing rules are amended in place and analyzed
again, while efficiency-only guidance stays retrospective. Green names, the
validator, and bypass grant neither technical proof nor remote or merge
authority. `PausedByUser` is never resumed silently, and uncertain operations
remain `NeedsRevalidation`.*

## Antigravity-CLI-Übergang / Antigravity CLI Transition

- Aktive Google-Agentenoberfläche ist Antigravity CLI mit Befehl `agy` und
  Spec-Kit-Integration `agy`.
- `GEMINI.md` und `~/.gemini/antigravity-cli/` bleiben
  Antigravity-kompatible Oberflächen.
- Direkte `gemini`-Befehle sind nur historische oder ausdrücklich benötigte
  Enterprise-/API-Kompatibilität und keine lokale Pflicht.

*The active Google agent surface is Antigravity CLI through the `agy` command
and Spec Kit `agy` integration. `GEMINI.md` and
`~/.gemini/antigravity-cli/` remain Antigravity-compatible surfaces. Direct
`gemini` commands are historical or explicitly required enterprise/API
compatibility, not a local requirement.*

<!-- MANUAL ADDITIONS START -->
<!-- MANUAL ADDITIONS END -->
