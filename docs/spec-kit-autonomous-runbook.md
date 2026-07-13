# Autonome Spec-Kit-Läufe / Autonomous Spec-Kit Runs

## Zweck

Dieses Runbook standardisiert vollständige autonome Spec-Kit-Läufe in
TuiVision. Es verbindet die vorhandenen Einzelbefehle zu einem prüfbaren
Ablauf, ohne Feature-Scope, Governance-Entscheidungen oder Remote-Rechte still
zu erweitern.

This runbook standardizes complete autonomous Spec-Kit runs in TuiVision. It
connects the existing commands into a verifiable workflow without silently
expanding feature scope, governance decisions, or remote authority.

## Grundsätze / Principles

1. **Evidence first**: `pr-evidence.md` entsteht vor der ersten
   Implementierungsänderung und wird während des Laufs gepflegt.
2. **Konvergenz statt Wiederholungszahl**: Clarify, Checklists, Analyze und
   Remote Review enden bei einem definierten Qualitätszustand.
3. **Vertikaler Referenz-Slice**: Bei wiederholbaren Änderungen wird zuerst ein
   repräsentativer Slice samt Test und Evidence abgeschlossen.
4. **Test-first Proof**: Ein prüfbarer Vertrag erhält zuerst einen
   beobachtbaren roten oder fehlenden Nachweis, danach die kleinste Umsetzung.
5. **Scope-Firewall**: Nicht akzeptierte Runtime-, Design-, Paritäts- oder
   Proof-Arbeit wird als Follow-up dokumentiert.
6. **Single writer**: Gemeinsame Evidence-, Versions-, Statistik-, Workflow-
   und Agent-Dateien werden nur seriell bearbeitet.
7. **Trigger-basierte Validierung**: Der geänderte Scope bestimmt die
   Prüftiefe; nicht ausgelöste Gates werden mit Begründung dokumentiert.
8. **Lokaler und remoter Nachweis**: Ein Delivery-Lauf ist erst nach dem
   vereinbarten lokalen oder remoten Abschluss fertig.

1. **Evidence first**: Create `pr-evidence.md` before the first implementation
   change and maintain it throughout the run.
2. **Convergence instead of iteration counts**: Clarify, checklists, Analyze,
   and remote review end at a defined quality state.
3. **Vertical reference slice**: Complete one representative slice with its
   test and evidence before spreading a repeated change.
4. **Test-first proof**: Establish an observable failing or missing proof for a
   testable contract before implementing the smallest correction.
5. **Scope firewall**: Record unaccepted runtime, design, parity, or proof work
   as follow-up work.
6. **Single writer**: Edit shared evidence, version, statistics, workflow, and
   agent files serially.
7. **Trigger-based validation**: Let touched scope determine test depth and
   justify every gate that is not triggered.
8. **Local and remote proof**: Delivery is complete only after the agreed local
   or remote closeout.

## Delivery-Modi / Delivery Modes

| Modus | Erlaubter Abschluss |
|---|---|
| `LocalImplementation` | Lokale Implementierung und Evidence; kein Push und kein PR |
| `PublishPR` | Commit, Push, PR und autorisierte CI-/Review-Nacharbeit; kein Merge |
| `MergeAndSync` | Voller PR-Abschluss, autorisierter Merge, Branch-Bereinigung und lokaler `main`-Sync |

Remote-Schreibrechte oder Merge-Autorität müssen aus dem aktuellen
Benutzerauftrag hervorgehen. Ein allgemeiner Wunsch nach Autonomie reicht
nicht als Merge-Freigabe. Erforderliche Human-Approval-Regeln dürfen nur bei
ausdrücklich delegierter Policy und mit dokumentiertem Bypass behandelt
werden.

Remote write or merge authority must come from the current user request. A
general request for autonomy is not sufficient merge approval. Required human
approval rules may be bypassed only under an explicitly delegated policy, with
the bypass recorded.

## Ablauf / Workflow

| Phase | Ergebnis | Gate |
|---|---|---|
| 0. Preflight | Werkzeug-, Branch-, Intake-, Preset- und Governance-Stand | Kein materieller Konflikt; Delivery-Modus festgelegt |
| 1. Specify | Feature-Spec und Requirements-Checkliste | Scope, Ausschlüsse, Entscheidungen und messbare Kriterien vollständig |
| 2. Clarify | Eingearbeitete planungswirksame Antworten | Keine verbleibende materielle Frage |
| 3. Feature-Checklists | Durchgeführte fachliche Prüflisten | Jeder Punkt erfüllt oder explizit disponiert |
| 4. Plan | Plan, Research, Datenmodell, Quickstart und Contracts | Evidence-, Slice-, Validierungs- und Delivery-Strategie festgelegt |
| 5. Plan-Review | Geprüfte und korrigierte Planungsartefakte | Keine offene umsetzungswirksame Anmerkung |
| 6. Tasks | Abhängige, stabile und reviewbare Tasks | Anforderungen abgedeckt; gemeinsame Schreiber serialisiert |
| 7. Analyze | Artefaktübergreifende Konsistenz | Keine offenen Critical/High-Funde; Medium disponiert |
| 8. Implement | Code, Tests, Doku, Evidence und abgeschlossene Tasks | Alle Tasks erfüllt oder zulässig konditional dokumentiert |
| 9. Validate | Lokale, scope-proportionale Nachweise | Alle ausgelösten Gates grün |
| 10. Deliver | Je nach Modus lokaler Abschluss, PR oder Merge/Sync | Keine actionable Review-Threads; Endzustand bewiesen |

Zwischen den Phasen darf der Agent logische Git-Checkpoints verwenden, wenn
der Delivery-Modus dies erlaubt. Ein Checkpoint wird erst nach bestandenem Gate
erstellt. Er ersetzt keine Evidence und keine Task-Abnahme.

The agent may use logical Git checkpoints between phases when the delivery mode
allows it. Create a checkpoint only after its gate passes. A checkpoint does
not replace evidence or task acceptance.

## Konvergenzkriterien / Convergence Criteria

### Clarify

Keine verbleibende Frage darf `plan.md`, Task-Zuschnitt, Validierungsstrategie,
Akzeptanz oder Scope materiell ändern. Reine Stilpräferenzen beenden den Lauf
nicht.

No remaining question may materially change `plan.md`, task shaping,
validation strategy, acceptance, or scope. Style preferences do not keep the
loop open.

### Checklists

Alle Punkte sind erfüllt oder besitzen eine ausdrücklich akzeptierte
Disposition mit Begründung. Durchführungshinweise werden ausgeführt, bevor
die Liste erneut bewertet wird.

Every item passes or has an explicitly accepted disposition with rationale.
Execute each review instruction before evaluating the checklist again.

### Analyze

Es bleiben keine `CRITICAL`- oder `HIGH`-Funde. Jeder `MEDIUM`-Fund ist behoben
oder mit Begründung, Owner und Grenze akzeptiert. Bereits akzeptierte
LOW-Stilfunde werden nicht in jedem Lauf erneut ausgegeben.

No `CRITICAL` or `HIGH` finding remains. Every `MEDIUM` finding is remediated or
accepted with rationale, owner, and boundary. Do not repeat accepted low-style
findings in every pass.

### Implement

Alle Tasks sind abgeschlossen oder besitzen ein im Task-Vertrag erlaubtes,
evidenzbasiertes konditionales Ergebnis. Alle ausgelösten Validierungen sind
grün; Scope- und Generated-Output-Diffs sind sauber.

All tasks are complete or have an evidence-backed conditional outcome allowed
by the task contract. All triggered validation passes, and scope plus generated
output diffs are clean.

### Remote Review

Alle erforderlichen Checks sind grün und es gibt keine offene umsetzbare
Review-Konversation. Quota-Ausfälle oder nicht verfügbare Reviewer werden als
fehlender Review, nicht als bestandener Review dokumentiert.

All required checks pass and no actionable review conversation remains. Record
quota failures or unavailable reviewers as missing reviews, not successful
reviews.

## Evidence-Vertrag / Evidence Contract

Die Vorlage `.specify/templates/autonomous-run-evidence-template.md` bildet den
Mindestumfang. Feature-spezifische Matrizen dürfen ergänzt werden. Mindestens
zu dokumentieren sind:

- bindender Intake, Scope und Ausschlüsse,
- Delivery-Modus und Autoritätsquelle,
- Entscheidungen, Follow-ups und Restrisiken,
- Artefakt- und Checklist-Konvergenz,
- historische Quellen und bewusste Abweichungen oder `N/A`,
- Governance-Anwendbarkeit mit Evidence-Pfad,
- jeder Validierungsbefehl, Ergebnis und nicht ausgelöste Trigger,
- Remote-Checks, Review-Threads, PR, Merge und lokaler Sync,
- eine kurze Retrospektive für die nächste Runbook-Verbesserung.

The template `.specify/templates/autonomous-run-evidence-template.md` defines
the minimum. Feature-specific matrices may extend it. Record at least the
binding intake, scope, delivery authority, decisions, convergence, historical
intent, governance applicability, validation, remote state, and a short
retrospective.

## Task-Zuschnitt / Task Shaping

- Tasks werden nach unabhängig reviewbaren Ergebnissen, nicht nach einzelnen
  Tabellenzellen geschnitten.
- Eine Matrix darf blockweise geprüft werden, wenn jede Zeile weiterhin eine
  eindeutige Entscheidung und Evidence besitzt.
- Der erste fachliche Slice enthält Test, Implementierung und Proof.
- Tasks mit derselben Zieldatei oder gemeinsamen Evidence-Schreibern laufen
  seriell. Ein `[P]`-Marker ist dann unzulässig.
- Testaufgaben stehen vor der zugehörigen Implementierung, wenn ein roter oder
  fehlender Vertrag beobachtbar ist.
- Vor dem ersten roten Testlauf wird die geplante Compile-Oberfläche vollständig
  geprüft: Imports, öffentliche XML-Dokumentation, Test-Harness-Helfer,
  Fokus-/Ownership-Assertionen und die Assembly-Identität gelinkter Quellen.
- Unabhängige negative Fälle dürfen als vollständige projektlokale Red-Matrix
  gebündelt werden, wenn jede erwartete Fehlergrenze einzeln benannt bleibt und
  keine Datei- oder Evidence-Ownership vermischt wird.
- Wird dieselbe Quelldatei in mehrere Assemblies gelinkt, darf ein
  Cross-Projekt-Test keine gemeinsame CLR-Typidentität annehmen. Er nutzt
  öffentliche Verträge, Zustandsdelegaten oder eine bewusst gemeinsame
  kompilierte Assembly, sofern die Architektur das erlaubt.
- Remote-Tasks werden nur für `PublishPR` oder `MergeAndSync` erzeugt.
- Jede Remote- oder Delivery-Task nennt den konkreten Repository-Evidence-Pfad,
  der ihr Abnahmeergebnis aufnimmt. Ein nur implizites "Evidence ergänzen"
  reicht für Analyze und Resume nicht aus.
- Remote-Gate-Fakten werden vor dem Merge tatsächlich geprüft, aber nicht auf
  denselben Feature-Head committed, wenn dieser Commit den geprüften Head und
  damit die Aussage sofort entwerten würde. Solche selbstinvalidierenden sowie
  echte Post-Merge-Fakten erhalten genau einen benannten Closeout-Evidence-Pfad.

- Shape tasks around independently reviewable outcomes, not individual table
  cells.
- A matrix may be reviewed in blocks while every row still receives one clear
  decision and evidence.
- Include test, implementation, and proof in the first domain slice.
- Serialize tasks that touch the same file or shared evidence writer. Such
  tasks must not use `[P]`.
- Put test tasks before implementation when a failing or missing contract is
  observable.
- Before the first red test batch, review the complete compile surface:
  imports, public XML documentation, test-harness helpers, focus/ownership
  assertions, and the assembly identity of linked sources.
- Independent negative cases may form one complete project-local red matrix
  when every expected failure boundary remains explicit and file/evidence
  ownership is not mixed.
- When one source file is linked into several assemblies, cross-project proof
  must not assume one CLR type identity. Use public contracts, state delegates,
  or an intentionally shared compiled assembly when architecture permits.
- Generate remote tasks only for `PublishPR` or `MergeAndSync`.
- Every remote or delivery task names the exact repository evidence path that
  records its acceptance result. An implicit "record evidence" is insufficient
  for analysis and resume.
- Verify remote gates before merge, but do not commit their current-head facts
  onto that same feature branch when the commit would immediately invalidate
  the reviewed head and its claim. Route such self-invalidating facts and true
  post-merge facts to one named closeout evidence path.
- Keep a causally required closeout evidence-only and single-commit-capable. Do
  not require its own PR URL, reviewed-head result, or merge commit inside that
  same repository file; verify those terminal facts externally so closeout does
  not recurse or retrigger itself.
- When both push and pull-request events start equivalent workflow sets, use the
  pull-request-context result as the delivery gate and record duplicate runs as
  operational noise. Do not cancel or suppress them unless the repository owns
  an explicit safe workflow/concurrency contract.

## Validierungsmatrix / Validation Matrix

| Änderung | Pflichtnachweis |
|---|---|
| Jede Änderung | `git diff --check`, Scope-Diff, Platzhalter- und Generated-Output-Prüfung |
| Finaler Lieferkandidat | Beabsichtigte Dateien stage'n, `git diff --cached --check` ausführen und Staging-Inventar gegen den Repository-Status prüfen; keine beabsichtigte ungetrackte oder unstaged Datei darf außerhalb des Kandidaten bleiben |
| Formatierbarer Code | Repository-Formatprüfung |
| Begrenzter Quell- oder Testcode | Gezielte Release-Tests |
| Gemeinsame Runtime- oder breite Proof-Logik | Voller Release-Testlauf und kanonisches Coverage Gate |
| XML/API/DocFX/Navigation/Guide | `docfx docfx.json`, danach `tests/web-a11y` |
| Bash-/PowerShell-Skript | Syntax, Vertragsparität und relevante OS-Matrix |
| Sichtbare TUI | Echter App-Loop, konkreter Zustand, View-Tree und Buffer-/Cell-Proof |
| Historisch abgeleitetes Verhalten | Relevante `.c`/`.cc`- und bei Bedarf Header-Prüfung unter `tv203s/` |

Vor jedem `dotnet build` oder `dotnet test` wird der manuelle Build-Zähler
erhöht. Eine Erhöhung autorisiert genau einen expliziten Build- oder Testaufruf;
mehrere Aufrufe dürfen nicht hinter derselben Erhöhung verkettet werden.
Statische Prüfungen laufen zuerst; fachlich zusammengehörige Tests werden
innerhalb eines Aufrufs gebündelt, damit weitere Befehle neue Evidence statt
nur Zähler-Churn erzeugen.

Repository-Prüfhelfer erhalten den Repository-Root immer als expliziten
Parameter. Ein Ergebnis gilt nur dann als erfolgreich, wenn der erwartete
Exitcode vorliegt und der Fehlerkanal weder einen PowerShell-ErrorRecord noch
`command not found` oder eine gleichwertige fatale Signatur enthält. Ein
Exitcode 0 darf einen solchen Fehler nicht verdecken; der Helfer gilt dann als
fehlgeschlagen und wird begrenzt nachgehärtet.

`git diff --check` allein prüft keine ungetrackten Dateien. Vor einem Commit
wird deshalb nur der beabsichtigte Lieferkandidat gestaged, mit
`git diff --cached --check` geprüft und sein Pfadinventar mit dem Repository-
Status verglichen. Fremde Änderungen werden nicht aufgenommen. Im Modus
`LocalImplementation` muss eine gleichwertige Einzeldatei- oder temporäre
Indexprüfung alle neuen Dateien erfassen und den vorherigen Indexzustand
wiederherstellen.

Increment the manual build counter before every `dotnet build` or `dotnet test`.
One increment authorizes exactly one explicit build or test invocation; do not
chain multiple invocations behind the same increment. Run static checks first
and batch related tests inside one invocation so additional commands produce
new evidence rather than counter churn.

Pass the repository root explicitly to repository validation helpers. Accept a
result only when the expected exit code is present and the error channel has no
PowerShell error record, `command not found` message, or equivalent fatal
signature. A zero exit code cannot hide such an error; treat the helper as
failed and route it to bounded remediation.

`git diff --check` alone does not inspect untracked files. Before a commit,
stage only the intended delivery candidate, run `git diff --cached --check`,
and compare its path inventory with repository status. Do not include unrelated
changes. For `LocalImplementation`, use an equivalent per-file or temporary-
index check that covers new files and restores the prior index state.

## Unterbrechung und Wiederaufnahme / Interruption and Resume

Vor einer Unterbrechung werden Task-Checkboxen, Evidence, letzter bestandener
Gate-Zustand und nächster konkreter Schritt aktualisiert. Bei Wiederaufnahme
werden Git-Stand, `.specify/feature.json`, Branch, Checklisten und Governance
erneut geprüft; bestandene Phasen werden nicht ohne Drift-Hinweis neu erzeugt.

Before an interruption, update task checkboxes, evidence, the last passing gate,
and the next concrete action. On resume, recheck Git state,
`.specify/feature.json`, branch, checklists, and governance. Do not regenerate
completed phases unless drift is found.

## Lokale Ownership und Updates / Local Ownership and Updates

Der Skill `.agents/skills/speckit-autonomous/`, dieses Runbook und die
autonomen Template-Ergänzungen sind projektgepflegte Dateien. Die Dateien
`.specify/integrations/*.manifest.json` dokumentieren dagegen die installierte
Vendor-Provenienz und erhalten keine manuell erfundenen Hashes für lokale
Anpassungen. Nach jedem Spec-Kit- oder Preset-Update müssen `specify check`,
Skill-Validierung, Template-Diff und Agent-Parität erneut geprüft werden.

The skill `.agents/skills/speckit-autonomous/`, this runbook, and the autonomous
template additions are project-owned. Files under
`.specify/integrations/*.manifest.json` describe installed vendor provenance;
do not add invented hashes for local customizations. After every Spec-Kit or
preset update, rerun `specify check`, skill validation, template diff review,
and agent parity.

## Remote-Abschluss / Remote Closeout

Ein `MergeAndSync`-Lauf endet mit:

1. grünen verpflichtenden Checks,
2. null offenen actionable Review-Threads,
3. dokumentierten fehlenden oder nicht verfügbaren Reviews,
4. autorisiertem Merge und dokumentiertem Bypass, falls erforderlich,
5. gelöschtem oder bewusst behaltenem Remote-Featurebranch,
6. lokalem `main` mit `HEAD == origin/main`, sauberem Working Tree und Prune,
7. einem Evidence-only-Closeout-PR nur dann, wenn Post-Merge-Fakten vorher nicht
   wahrheitsgemäß dokumentiert werden konnten oder der Evidence-Commit den
   geprüften Head und damit seine eigene Aussage sofort entwerten würde.

A `MergeAndSync` run ends with passing required checks, no actionable review
threads, documented unavailable reviews, an authorized merge, branch cleanup,
a clean local `main` equal to `origin/main`, and an evidence-only closeout PR
only when post-merge facts could not truthfully be recorded earlier or the
evidence commit would invalidate the reviewed head it claims to describe.

## Laufende Verfeinerung / Continuous Refinement

Jeder autonome Lauf ergänzt in seiner Feature-Evidence kurz:

- was den Lauf beschleunigt hat,
- welche Wiederholung keinen neuen Nachweis brachte,
- wo Task-Zuschnitt oder Single-writer-Regeln verbessert werden können,
- welcher lokale oder Remote-Blocker erneut auftrat,
- welche konkrete Runbook-, Skill- oder Template-Änderung empfohlen wird.

Nach mindestens einem weiteren Lauf werden nur wiederholt belegte Muster in
dieses Runbook oder in die projektweiten Templates übernommen. Feature-
spezifische Besonderheiten bleiben in der jeweiligen Feature-Evidence.

After each autonomous run, record what accelerated delivery, which repetition
added no evidence, where task shaping can improve, which blocker recurred, and
which concrete workflow change is recommended. Promote only repeated patterns
to this runbook or project templates; keep feature-specific details in feature
evidence.
