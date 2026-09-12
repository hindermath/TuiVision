# Verlinkte Intake-Evidence / Linked Intake Evidence

## Entscheidung / Decision

TuiVision erzeugt die Root-Ansicht und die Serienansicht aus derselben
kanonischen Projektion. Das Manifest bleibt die fachliche Quelle; die
Feature-Zuordnung und der getrennte Backlog werden durch die für Feature 032
übernommene, T056-gesperrte Fixture belegt.

*TuiVision renders the root and series views from one canonical projection.
The manifest remains the professional source; the Feature 032 fixture locked
by T056 proves feature mapping and backlog separation.*

## Datenfluss / Data Flow

1. `requirements/intakes/series/tui-vision-delivery/manifest.json` liefert zehn
   geordnete Ziele und sechs Abhängigkeiten.
2. `scripts/tests/linked-intake-evidence/tuivision-exact.json` bindet jedes Ziel
   an genau ein abgeschlossenes Feature 037–046 und Feature 046 gesondert als
   jüngsten Abschluss an Position 10.
3. `scripts/render-requirements-intake-governance.mjs` prüft Manifesthash,
   Zuordnungen, Feature-State, Kanten und den `DeferredOptional`-Backlog.
4. Derselbe Lauf erzeugt `Lastenheft_Abarbeitungsreihenfolge.md` und
   `requirements/intakes/series/tui-vision-delivery/order.md`; nur die relativen
   Linkziele unterscheiden sich.

*The renderer validates the manifest, exact feature proofs, dependencies, and
the deferred backlog before it emits both views. Only link relativity differs.*

## Abgrenzung / Boundary

Die Änderung berührt keine Runtime, Assembly, öffentliche API, Konsole,
Abhängigkeit oder Paketierung. Historische `tv203s`- und Drei-Achsen-
Quellenprüfung ist deshalb `N/A`: Es wird kein historisch abgeleitetes
Produktverhalten portiert, erweitert oder korrigiert.

*No runtime, assembly, public API, console behavior, dependency, or package is
changed. Historical and three-axis source review is therefore not applicable.*

## Dokumentationsauswirkung / Documentation Impact

- Entscheidung: `GeneratedUpdate`
- Kanonische Quellen: Serienmanifest und T056-gesperrte Exact-Fixture
- Generierte Ausgaben: beide Abarbeitungsreihenfolgen
- Zielgruppen: Maintainer, Reviewer und Spec-Kit-Operatoren
- Navigation: Root-Ansicht und serienlokale Ansicht verlinken Intake und Feature
- Sprachregel: Deutsch zuerst, Englisch danach; CEFR-B2
- Distribution: `sourceOnly`; Home-Sync: `false`
- Owner: TuiVision-Maintainer
- Re-Evaluation: Manifest-, Fixture-, Feature-Proof- oder Rendereränderung

Die fünf Agentenflächen `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
`.github/copilot-instructions.md` und
`.github/agents/copilot-instructions.md` wurden geprüft. Agent-Parity ist für
diese repositorylokale Projektion `N/A (NoUpdateRequired)`, weil keine neue
gemeinsame Arbeitsregel, kein neuer Befehl und keine Routingsemantik entsteht.

*All five agent surfaces were reviewed. Agent-parity editing is not applicable
because this repository-local projection introduces no shared operating rule,
command, or routing semantic.*
