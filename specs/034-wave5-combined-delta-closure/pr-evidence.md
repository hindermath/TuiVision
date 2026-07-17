# Autonomous Run Evidence: Wave-5 Combined Delta Closure

## Laufidentität / Run Identity

| Feld | Wert |
|---|---|
| Feature | `034-wave5-combined-delta-closure` |
| Branch | `034-wave5-combined-delta-closure` |
| Binding intake | `Lastenheft_19_Wave5-Combined-Delta-Closure.md` |
| Baseline | `4dbfa39511f774af5b0c79ac6c5518dd058f664c` |
| Intake delivery | PR #98, merge `4dbfa39511f774af5b0c79ac6c5518dd058f664c` |
| Delivery mode | `MergeAndSync` |
| Authority | Aktueller Benutzerauftrag für Implementierung, Commit, Push, PR, engen Human-Approval-Bypass, Merge und Main-Sync |
| Run ID | `9beccdb7-7c58-4e5e-a7df-40ec427aac50` |
| Interruption policy | Keine geplante Unterbrechung; unerwarteter Abbruch benötigt Status und expliziten Resume |

## Scope

### Enthalten / Included

- exakte Produktdateimengen der PRs #93 und #96;
- PR #94/#97 als kausale Closeouts und PR #95 als Prompt-Metadaten;
- 15 historische Source-Rollen, sechs Consumer, zehn Beispiele, zehn
  Funktionsproofs, zehn Showcase-Abschlüsse und zehn Guide-/Launch-Pfade;
- zehn kombinierte Entscheidungen und qualitätsbezogene Dimensionen;
- test-only Closure-Validator und positive/negative Evidence-Prüfung;
- erforderliche Status-, Reihenfolge-, Statistik-, Agent- und
  Lastenheft-Oberflächen;
- Exact-Head-Delivery und, nur falls kausal erforderlich, ein Evidence-only
  Closeout.

### Ausgeschlossen / Excluded

- Runtime- oder öffentliches Verhalten;
- API-Signaturen, Dependencies, Pakete, Projekte oder Solution;
- Produkt-, Framework- oder Beispielcode;
- Remediation eines Candidate Findings;
- Änderungen unter `TVDEMOS/`, `TVFM/`, `tv203s/` oder externen Checkouts;
- generierte DocFX-Ausgabe, API-YAML, Caches, Logs, Credentials oder
  Testergebnisse;
- Start von Wave 6, Feature 035 oder dem Post-Wave-6-Portfolio-Audit.

## Single-writer- und Versionsgrenzen / Single-writer and Version Boundaries

Serialisiert werden:

- `pr-evidence.md`
- `wave5-combined-delta.json`
- `wave5-closure.md`
- `tasks.md`
- `autonomous-run-state.json`
- `Directory.Build.props`
- `docs/project-statistics.md`
- `Pflichtenheft.md`
- `Lastenheft_Abarbeitungsreihenfolge.md`
- alle fünf Agent-Oberflächen
- archiviertes Lastenheft und ein möglicher kausaler Closeout.

Die Version ist `1.34.<patch>.<build>`. Vor jedem einzelnen
`dotnet build` oder `dotnet test` wird nur der manuelle Build-Zähler genau
einmal erhöht. `dotnet run` wird nach einem expliziten Build ausschließlich
mit `--no-build` ausgeführt.

## Preflight

| Prüfung | Ergebnis | Evidence / Grenze |
|---|---|---|
| Branch | Pass | exakt `034-wave5-combined-delta-closure` |
| Feature-Metadaten | Pass | `.specify/feature.json` zeigt auf `specs/034-wave5-combined-delta-closure` |
| Baseline-Ancestry | Pass | `4dbfa395...` ist aktueller `HEAD` und Ausgangspunkt |
| Intake-PR | Pass | PR #98 ist `MERGED`; Merge-Commit entspricht der Baseline |
| Worktree-Ownership | Pass | nur Feature-034-Artefakte, Feature-Metadaten und geplante Agent-Kontexte |
| `specify check` | Pass | Exit 0; Antigravity, Claude, Codex, Junie und OpenCode verfügbar |
| Prerequisites | Pass | Feature-Pfad plus Research, Datenmodell, Contract, Quickstart und Tasks erkannt |
| Presets | Pass | Security 0.6.0/10, Architecture 0.5.0/20, iSAQB 0.2.0/30, A11Y 0.4.0/40, Cross-Platform 0.2.0/50, Agent Parity 0.3.0/60, Autonomous Run 0.2.2/70 |
| Checklists | Pass | 101 vollständig, 0 unvollständig |
| Bash state validator | Pass | `Analyze`, `Active`, 0/164 |
| PowerShell state validator | Pass | `Analyze`, `Active`, 0/164 |
| Gate requirements | Pass | UTF-8 JSON; SHA-256 wird am Tasks-Checkpoint gebunden |
| Ignore-Regeln | Pass | `_site/`, `api/*`, `*.log`, Preset-Cache und Build-/Testausgabe sind ignoriert |

## Run Gates

| Phase | Versuch | Ergebnis | Evidence | Nächste Aktion |
|---|---:|---|---|---|
| Preflight | 1 | Pass | sauberer Feature-Ausgangspunkt und Scope-Ownership | Specify |
| Specify | 1 | Pass | `spec.md`, Requirements-Checklist | Clarify |
| Clarify | 2 | Pass | keine formale Frage; zweiter fokussierter Pass konvergiert | Checklists |
| Checklists | 1 | Pass | fünf Requirements- plus zwei Plan-Checklists, 101/101 | Plan |
| Plan | 1 | Pass | Plan, Research, Datenmodell, Contract, Quickstart | Tasks |
| Plan review | 1 | Pass | 36/36 Planprüfpunkte, keine offene Aktion | Tasks |
| Tasks | 1 | Pass | 164 eindeutige, serialisierte Tasks und 13 Gate-Anforderungen | Analyze |
| Analyze | 1 | Remediated | drei Task-Präzisierungen: Live-PR-Dateimenge, PTY `--no-build`, Showcase-Folge aus Feature-035-Delta | Analyze erneut |
| Analyze | 2 | Pass | 73 Requirements/SC abgedeckt; 0 Critical/High/Medium, 0 Constitution-Konflikte, 0 unmapped Tasks | Implement |
| Implement | 1 | Pass | Closure-Datensatz, test-only Validator und Abschluss-Evidence | Validate |
| Validate | 1 | Pass | lokale, interaktive und vollständige Gates | exakten Kandidaten bilden |
| Publish / Review | 1 | Open | Feature-PR und Exact-Head-Evidence | nach lokalem Kandidaten |
| MergeAndSync | 1 | Open | Feature-Merge und möglicher kausaler Closeout | nach Review-Konvergenz |

## Analyze-Konvergenz / Analyze Convergence

| Metrik | Ergebnis |
|---|---:|
| Funktionale Anforderungen | 40/40 abgedeckt |
| Governance-Anforderungen | 7/7 abgedeckt |
| Verfassungsanforderungen | 13/13 ausgerichtet |
| Erfolgskriterien | 13/13 abgedeckt |
| Tasks | 164 eindeutige IDs |
| Unmapped Requirements | 0 |
| Unmapped Tasks | 0 |
| Critical / High / Medium nach Remediation | 0 / 0 / 0 |
| Constitution-Konflikte | 0 |
| Unaufgelöste Marker | 0 |

## Verbindliche Provenienz / Binding Provenance

### PR-Dateimengen / PR File Sets

| PR | Rolle | Basis | Head | Merge | Pfade | Sortierter Set-SHA-256 | Ergebnis |
|---:|---|---|---|---|---:|---|---|
| 93 | FunctionalProduct | `269c54f` | `cf274c6` | `e74c33d` | 77 | `359196f832566eb58d16c2da5ce1d9586c19f2268ada6cf9815cfcf9ed5a13a0` | Pass |
| 94 | FunctionalCloseout | `e74c33d` | `4cf4554` | `355f8ec` | 6 | `d0988de3cdb9157dd81c41e07d4ad541b40639aef68022497b84ba9f6b799083` | Pass |
| 95 | PromptMetadata | `355f8ec` | `c0b0217` | `5df2ec3` | 6 | `0fb7cd7ee2bbf65430af28a01f2cbba8277eea773a2bde83e84a4eb392489254` | Pass |
| 96 | ShowcaseProduct | `5df2ec3` | `8921bd3` | `d476e63` | 50 | `d43daa4b9ecb8c90f844fa4661d185dd3abc5d862162578b409f8cd55befff85` | Pass |
| 97 | ShowcaseCloseout | `d476e63` | `0481f46` | `1e99765` | 12 | `c446663546a51ba6df21612e82e699e8397cb2bda5a4787cfc3839040ee46288` | Pass |

Die GitHub-API und lokale Git-Historie stimmen bei Basis, Head, Merge und
sortierter Pfadmenge überein. Der Datensatz enthält die vollständigen
Pfadlisten; der SHA-256 wird über UTF-8, ordinal sortiert, mit genau einem
abschließenden LF berechnet.

### Bindende Eingaben / Binding Inputs

| Feature | Rolle | Pfad | SHA-256 | Ergebnis |
|---|---|---|---|---|
| 032 | FunctionalEvidence | `specs/032-wave5-tp7-functional-porting/pr-evidence.md` | `97f84e968ef7717b507baef6737053aea3b8193b2f6183769a1718cd7e52b258` | Pass |
| 032 | FunctionalCloseout | `specs/032-wave5-tp7-functional-porting/delivery-closeout.md` | `eec79e1aaee4bd960c18a62c6f509e91541e7c5cb24d6e4cb36902698ae4558f` | Pass |
| 032 | FunctionalRetrospective | `specs/032-wave5-tp7-functional-porting/retrospective.md` | `199933d44e0f2ada06b3497cf7f8db9cf0cfffb49bf8b7c324f61a2544d1328d` | Pass |
| 033 | ShowcaseEvidence | `specs/033-wave5-tp7-showcase-remediation/pr-evidence.md` | `46539e658964f1e6b5ca5ba86b35069cfc046f80c14a29fbf3342ebe8c5261a6` | Pass |
| 033 | ShowcaseCloseout | `specs/033-wave5-tp7-showcase-remediation/delivery-closeout.md` | `42e63501a4244e25697b5c44b4fe601cdb2d4dc09e709d9ad2aa3d6ac14e7178` | Pass |
| 033 | ShowcaseRetrospective | `specs/033-wave5-tp7-showcase-remediation/retrospective.md` | `c75caf7948ffb4c08009d7527be55aeb2fd6e63ff3e694256f44baf9411b95d8` | Pass |
| 033 | ShowcaseDesign | `specs/033-wave5-tp7-showcase-remediation/plan.md` | `fe016cd8f6acfb90ef78490412cdd654c988772bf94229394dfff8cd197306c8` | Pass |

### Historische Blobs / Historical Blobs

Alle 15 in `wave5-combined-delta.json` gespeicherten Git-Blobs stimmen am
Feature-032-Merge `e74c33d` und am aktuellen Head überein:

```text
TVDEMO 9aa23c8    DEMOCMDS daf053e    DEMOSTRS e1abf0f
GADGETS 26314f3   TVEDIT 56279ad      TVHC a5bbbf1
HELPFILE de79123  DEMOHELP 013dd90    TVRDEMO a5e2bd3
GENRDEMO 2ee61b1 ASCIITAB 2b22640    CALC d4d3565
CALENDAR 3667ae8 PUZZLE 30da629      MOUSEDLG 5929db0
```

Der C#-Validator rekonstruiert die Git-Blob-ID aus Header und Rohbytes. SHA-1
ist dabei das Git-Dateiformat und keine neue Sicherheitsentscheidung.

### Foundation-Cardinalities

| Menge | Aktuell | Ziel | Status |
|---|---:|---:|---|
| Product deltas | 5 | 5 | Pass |
| Historical sources | 15 | 15 | Pass |
| Consumers | 6 | 6 | Pass |
| Functional proofs | 10 | 10 | Pass |
| Showcase closures | 10 | 10 | Pass |
| Guide/launch rows | 10 | 10 | Pass |
| Combined example rows | 10 | 10 | Pass |
| Governance rows | 7 | 7 | Metadaten vollständig; lokale Applicability geprüft |
| Validation rows | 13 | 13 | acht lokale Pass, vier Remote Open, ein N/A |

## Referenz-Slice / Reference Slice

`Tp7Calculator` bindet zuerst PR-Pins, alle 15 Source-Blobs, `W5-005`,
Funktionsproof, Showcase-Proof, Guide/Launch und die kombinierte Entscheidung.
Der erste Red-Lauf darf ausschließlich an den planmäßig fehlenden Restzeilen
scheitern.

Vor dem Red-Lauf wurden Imports, MSTest-4-APIs, `System.Text.Json.Nodes`,
Repository-Root-Ermittlung, öffentliche XML-Summaries, strukturierte
Mutationshelfer und didaktische Kommentare geprüft. Die neue Klasse verwendet
keine Produktreferenz über die bereits vorhandene Smoke-Test-Projektgrenze
hinaus. Negative Fälle arbeiten auf `DeepClone()`-Daten und ändern keine
getrackte Evidence.

### Erwarteter Red-Nachweis / Expected Red Proof

| Command | Version | Ergebnis | Akzeptierte Grenze |
|---|---|---|---|
| `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter FullyQualifiedName~Wave5CombinedDeltaClosureTests.Test_CombinedExampleMatrixIsComplete` | `1.34.1.348` | Expected Red, 0/1, Exit 1 | Vollständige Kompilation; ausschließlich `The functional proof set is missing, duplicated, or unknown` wegen 1/10 Zeilen |

Der Error-Channel enthält einen erwarteten MSTest-Assertionpfad und keine
PowerShell-ErrorRecord-, `command not found`-, Restore-, Compile- oder
Produktfehlersignatur.

### Green-Nachweis / Green Proof

| Command | Version | Ergebnis | Grenze |
|---|---|---|---|
| `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter FullyQualifiedName~Wave5CombinedDeltaClosureTests` | `1.34.1.349` | Pass, 9/9, Exit 0 | Provenienz, 10/10/10/10-Matrix, Framework/Governance, Findings/Waves, PR-/Input-/Source-/Consumer-Mutationen, unvollständige Proofs und LF/CRLF |

Der Error-Channel blieb leer. Die Pins werden unabhängig von den JSON-Werten
geprüft; eine gemeinsame Änderung von Daten und Selbsthash wird abgelehnt.

## Kombinierte Entscheidungen / Combined Decisions

Die lesbare Matrix steht in `wave5-closure.md`; der normative Datensatz bleibt
`wave5-combined-delta.json`.

| Entscheidung | Anzahl |
|---|---:|
| `AcceptedAsIs` | 0 |
| `AcceptedIntentionalDeviation` | 10 |
| `CandidateFinding` | 0 |
| `ProductDecision` | 0 |
| offene `Gap`-Dimensionen | 0 |

Alle zehn modernen C#-Beispiele behalten den historischen Lernzweck. Die
bewussten Abweichungen betreffen deterministische Zustände, kontrollierte
Datei-/Resource-Pfade, UTF-8-Modelle, kompakte Layouts oder ehrliche
Terminal-/Mausgrenzen; reine Quelltextunterschiede sind kein Finding.

### Finding- und Folgeentscheidung / Finding and Follow-up Decision

| Menge | Anzahl | Ergebnis |
|---|---:|---|
| `W5D###` Candidate Findings | 0 | keine reproduzierbare Verhaltens-, Interaktions-, Proof-, Dokumentations-, A11Y-, Plattform- oder Wiederverwendungslücke |
| Product Decisions | 0 | kein menschlicher Produktentscheid erforderlich |
| Owner-Gruppen | 0 | keine nicht leere Remediation-Ownership |
| Hardening-Intakes | 0 | kein leeres oder vermutetes Lastenheft |
| automatisch gestartete Features | 0 | Feature 035 bleibt unangelegt und ungestartet |

Der Feature-Head behält `BlockedPendingCausalClosure` für Wave 5 und Wave 6.
Die Zielwerte `Closed` und `EligibleForIntake` werden erst nach dem
tatsächlichen Feature-Merge in einem nicht leeren kausalen Closeout gesetzt.

## Framework- und Proof-Review / Framework and Proof Review

| Bereich | Entscheidung | Review-Ergebnis |
|---|---|---|
| `Wave5Application` | `ExampleComposition` | delegiert Event-Loop, Desktop, Fokus, Status und Views an bestehende TuiVision-Verträge |
| `Wave5ConsoleHost` | `ExampleComposition` | kapselt nur `--smoke`, Terminalgröße und ehrlichen `80x25`-Fallback |
| `Wave5StatusLine` | `ExampleComposition` | bleibt eine echte `TStatusLine`; keine zweite Status-/Command-Schicht |
| `Wave5GridView` | `ExampleComposition` | bleibt eine fokussierbare `TView` über dem vorhandenen Draw-/Buffer-Vertrag |

Die lokalen Zustandsmodelle sind jeweils nur für einen Lernzweck zuständig.
Keine Logik ersetzt wiederverwendbare Events, Commands, Fokus, Desktop,
Editor, Help, Resources, Dialoge, Rendering oder Treiber.

Proof-Rollen:

- `PrimaryProof`: echte `app.Run()`-Pfade mit Zustand, View-Identität, Fokus,
  Status, F1-Description und Buffer-/Cell-Assertions;
- `SetupOnly`: testeigene temporäre Roots, Fixtures und injizierte Capability;
- `SupplementalProof`: direkte Vorabinspektion, niemals alleinige Abnahme.

Kontrollierte Grenzen: Editor- und Generatorpfade bleiben unter testeigenen
Roots; Resource- und Help-Modelle verarbeiten nur kontrollierte Eingaben;
Calendar und Puzzle sind zeit-, locale- und zufallsunabhängig; Mouse verändert
keine Hostkonfiguration. `Wave5ConsoleHost` liest ausschließlich die
Terminalgröße mit dokumentiertem Fallback. Es gibt keine Netzwerk-, Service-
oder versteckte Prozessabhängigkeit.

Free Vision, Terminal.GUI und magiblot/tvision wurden nicht erneut pauschal
auditiert, weil keine neue reproduzierbare Wave-5-Frage entstand.

## Governance

Vollständige Governance-Zeilen stehen im Closure-Datensatz. Die akzeptierte
Planungsgrenze lautet:

- NIST SSDF, CWE Top 25, Evidence-Integrität, Secrets, Supply Chain,
  A11Y, Plattformen, Agent-Parität und Autonomous Delivery: `Applicable`;
- ASVS, neue SBOM/VEX/SLSA-Artefakte, AI-SBOM, regulatorische Rollen,
  S-ADR, arc42-Security-Update, Zero Trust, SAMM, BSI C3A und BSI C5:
  triggerbasiert `N/A`;
- Skriptparität: `N/A`, solange kein `.sh` oder `.ps1` geändert wird;
- `.specify/templates/`: `N/A`, solange das Feature keine Templates ändert.

## Validierung / Validation

| Gate | Ergebnis | Command / Evidence |
|---|---|---|
| Static candidate | Pass | 16 beabsichtigte Pfade; `git diff --cached --check`, JSON-, Markdown-, UTF-8-, Scope-, Placeholder- und Zeilenendenscans bestanden; keine unstaged oder unversionierten Pfade |
| Feature-034 closure | Pass | gehärteter Validator 10/10 bei `1.34.1.350`; Red 0/1 bei `.348`, Green 9/9 bei `.349` |
| Wave-5 TP7 | Pass | Feature 034 plus `Tp7*`, `Wave5Functional` und `Wave5Showcase`: 54/54 bei `1.34.1.351` |
| zehn controlled smokes | Pass | 10/10 mit `dotnet run --no-build --configuration Release --project examples/Tp7* -- --smoke` |
| zehn normale PTY-Pfade | Pass | 10/10 mit erstem Frame, primärer Aktion, F1-Description und `Ctrl+Q`; Transkripte nur unter `/tmp` |
| Full Release | Pass | Build 0 Warnungen/0 Fehler bei `1.34.1.352`; Tests 836/836 bei `1.34.1.353` |
| Coverage | Pass | bei `1.34.1.354`: Core 92,96 %, Controls 86,66 %, Serialization 90,01 %, Compatibility 80,55 %, Drivers.Console 89,18 % |
| Format | Pass | `dotnet format TuiVision.sln --verify-no-changes`, Exit 0 |
| DocFX / A11Y | Pass | 341 Modelle, 0 Warnungen/0 Fehler; Playwright/Axe 2/2 |
| Text-first / Markdown | Pass | UTF-8, semantische Struktur, Code-Fences und Lynx für drei repräsentative Seiten bestanden |
| Security / Supply Chain | Pass lokal, Provider Open | Agent-Secret-Scan High 0; Gitleaks für exakten neuen Scope und Git-Historie ohne Treffer; NuGet ohne bekannte Schwachstellen; `npm audit --omit=dev` 0 |
| Agent parity | Pass | fünf Agent-Oberflächen bytegleich für den Feature-034-Block, SHA-256 `0527bcd18d51d39b058b039e6d490ccb7b720bf5ed84fa12e0706182baf70953` |
| Ubuntu / macOS / Windows | Open | PR-Kontext |
| Exact head | Open | Gate-Evidence-Validator |
| Reviews | Open | Kommentare, Reviewer und GraphQL-Threads |

Der kanonische Coverage-Lauf führte alle 836 Tests erneut erfolgreich aus.
`xmllint --noout coverlet.runsettings` war zuvor erfolgreich. Die fünf
Assembly-Werte stammen aus den jeweiligen Package-Zeilen des
Coverlet-Berichts und überschreiten einzeln die 70-Prozent-Grenze.

Der korrigierte Checklist-Lauf meldete für alle sieben Feature-Checklists
jeweils null offene Einträge. Ein erster Shell-Zähler behandelte die leere
`rg -c`-Ausgabe fälschlich nicht als `0`; die korrigierte Ausführung
normalisierte diesen Wert und änderte keine Datei.

Der vorhandene Homogeneity-Wrapper bleibt wegen fehlender historischer
`scripts/lib/hg-*.sh`-Bausteine mit Exit 2 nicht ausführbar. Dieser bekannte
Repository-Scanner-Grenzfall wird nicht als Pass umgedeutet. Die für Feature
034 maßgebliche explizite Fünf-Oberflächen-Parität wurde separat
deterministisch nachgewiesen. Ein anfänglich zu breiter Gitleaks- und
Markdown-Scope wurde auf die tatsächlich neuen oder geänderten Pfade
begrenzt; dabei entstand kein Produktbefund.

## Exakter Kandidat / Exact Candidate

Der vorgesehene Feature-Kandidat enthält genau 16 Pfade:

```text
.github/agents/copilot-instructions.md
.github/copilot-instructions.md
AGENTS.md
CLAUDE.md
Directory.Build.props
GEMINI.md
Lastenheft_19_Wave5-Combined-Delta-Closure.md
  -> Lastenheft_19_Wave5-Combined-Delta-Closure.034-wave5-combined-delta-closure.md
Lastenheft_Abarbeitungsreihenfolge.md
Pflichtenheft.md
docs/project-statistics.md
specs/034-wave5-combined-delta-closure/autonomous-run-state.json
specs/034-wave5-combined-delta-closure/pr-evidence.md
specs/034-wave5-combined-delta-closure/tasks.md
specs/034-wave5-combined-delta-closure/wave5-closure.md
specs/034-wave5-combined-delta-closure/wave5-combined-delta.json
tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs
```

Die Lastenheft-Zeile ist eine inhaltsgleiche Git-Rename-Operation. Der Scope
enthält genau einen test-only C#-Validator, Feature-Evidence, Status- und
Guidance-Flächen sowie die Branch-Version. Produkt-, Beispiel-, API-,
Dependency-, Projekt-, historische und generierte Pfade sind nicht enthalten.
`git diff --cached --check` war erfolgreich; staged, unstaged und unversioniert
wurden getrennt geprüft. Es blieben keine unstaged oder unversionierten Pfade.

## Abschlussgrenze / Completion Boundary

Der Feature-Head darf höchstens `ReadyForMerge` behaupten:

```text
Wave 5: BlockedPendingCausalClosure
Wave 6: BlockedPendingCausalClosure
```

Nur nach dem tatsächlichen Feature-Merge darf ein notwendiger Evidence-only
Closeout `Closed` und `EligibleForIntake` setzen. Feature 035 wird dabei nur
reserviert und nicht gestartet.
