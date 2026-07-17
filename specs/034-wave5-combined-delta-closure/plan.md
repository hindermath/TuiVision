# Implementierungsplan: Wave-5 Combined Delta Closure

**Branch**: `034-wave5-combined-delta-closure` | **Datum**: 2026-07-17 | **Spezifikation**: [spec.md](spec.md)
**Eingabe**: Feature-Spezifikation aus `specs/034-wave5-combined-delta-closure/spec.md`

## Zusammenfassung / Summary

Feature 034 rekonstruiert den gemeinsam geprüften Produktdelta der funktionalen
Wave-5-Lieferung in PR #93 und der sichtbaren Showcase-Lieferung in PR #96.
Ein geschlossener JSON-Datensatz verbindet exakt 15 historische Quellenrollen,
sechs Consumer, zehn Beispiele, zehn Funktionsproofs, zehn Showcase-Abschlüsse
und zehn Guide-/Launch-Pfade. Ein neuer test-only MSTest-Validator prüft
Cardinalities, Provenienz, Beziehungen, Entscheidungen, Findings und
Abschlusszustände fail-closed.

*Feature 034 reconstructs the reviewed Wave-5 product delta from PRs #93 and
#96. A closed JSON dataset and one test-only MSTest validator prove exact
provenance, cardinalities, relationships, decisions, findings, and closure
states without changing product or example code.*

Der reviewte Feature-Head hält Wave 5 und Wave 6 gesperrt. Erst nach grünen
lokalen, remoten und Exact-Head-Gates sowie dem Feature-Merge darf ein
nicht rekursiver Evidence-Closeout Wave 5 auf `Closed` und Wave 6 auf
`EligibleForIntake` setzen. Bei diesem Ergebnis wird Lastenheft 20 für ein
späteres Feature 035 abgeleitet, aber Feature 035 wird nicht gestartet.

## Technischer Kontext / Technical Context

**Language/Version**: C# 14 / .NET 10 für test-only Validierung; JSON und Markdown für Evidence
**Primary Dependencies**: BCL `System.Text.Json`, MSTest 4.0.1, vorhandene Repository-Skripte und Workflows; keine neue Dependency
**Storage**: Source-controlled, geschlossene JSON- und Markdown-Evidence; temporäre Dateien nur im Testverzeichnis
**Tests**: bestehendes `tests/TuiVision.Examples.SmokeTests`, vollständige Release-Tests, Coverlet, DocFX, Playwright/Axe
**Zielplattformen**: macOS lokal sowie GitHub-hosted Ubuntu, macOS und Windows
**Project Type**: C#/.NET-Terminal-UI-Framework mit read-only Audit-Feature
**Leistungsziel**: deterministische Prüfung von 15/6/10/10/10 Beziehungen und zehn kombinierten Zeilen ohne Netzwerkzugriff im Unit-Test
**Grenzen**: null Produkt-, Runtime-, API-, Dependency-, Projekt-, Beispiel-, Framework- oder historische Änderungen; Build-Zähler vor jedem `dotnet build` oder `dotnet test`
**Umfang**: ein Closure-JSON, eine test-only Validator-Klasse, ein lesbarer Abschlussbericht, Feature-Evidence, Status-/Reihenfolgepflege und optional ein kausaler Closeout

## Verfassungsprüfung / Constitution Check

*Gate vor Research bestanden; nach Design erneut bestanden.*

| Gate | Entscheidung und Evidence |
|---|---|
| Level-2-Umgebung | Bestehendes C#/.NET-10-Projekt mit MSTest, DocFX/Axe, fünf Agent-Oberflächen und Statistik |
| Memory-safe Sprache | Nur C# wird ergänzt; Pascal, C/C++ und externe Quellen bleiben read-only |
| Security-first | Geschlossenes Schema, ordinale IDs, exakte Sets, SHA-256-/Git-Pins, kontrollierte Pfade und fail-closed Ablehnung |
| NIST SSDF / CWE Top 25 | Anwendbar auf Evidence-Integrität, Fehlklassifikation, Parsergrenzen, Scope und Review |
| OWASP ASVS | `N/A`: kein Web-, HTTP-, Auth-, Session- oder Service-Flow |
| SBOM / VEX / SLSA / OpenSSF | Bestehende Supply-Chain-Gates gelten; keine neue Dependency oder Distribution löst neue Artefakte aus |
| AI-SBOM | `N/A`: AI bleibt Entwicklungswerkzeug; keine Runtime-KI wird geliefert |
| STRIDE / CIA / CAPEC | Evidence-Tampering, Auslassung, Duplikate und falsche Freigabe werden im Validator behandelt; keine neue Produktbedrohungsanalyse |
| S-ADR / arc42 / Zero Trust / SAMM | `N/A`: keine Architektur-, Trust-, Netzwerk-, Deployment- oder Servicegrenze ändert sich |
| BSI C3A / BSI C5 | `N/A`: kein Cloud-Service, Providervertrag oder Shared-Responsibility-Modell |
| NIS2 / CRA / EU AI Act / DORA | `N/A`: keine neue regulatorische Rolle oder Produktverteilung |
| A11Y | Zehn Tastatur-, Fokus-, Status-, Description-, Layout- und text-first Nachweise werden kombiniert geprüft |
| Cross-platform | Linux, macOS und Windows sind anwendbar; Skriptparität bleibt `N/A`, da kein Skript geändert wird |
| Agent parity | Alle fünf gepflegten Agent-Oberflächen werden gemeinsam synchronisiert |
| Autonomous Run | v0.2.2 steuert Evidence-first, Konvergenz, Authority, Exact Head, Review, Closeout und Retrospektive |
| Statistik | `docs/project-statistics.md` wird erst am finalen Kandidaten aktualisiert |

**Post-Design-Recheck**: Bestanden. Das Design ergänzt ausschließlich
Feature-Evidence und test-only Validierung. Es benötigt keine Ausnahme und
keinen Complexity Waiver.

## Autonomer Ausführungsvertrag / Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: aktueller Benutzerauftrag
**Evidence path**: `specs/034-wave5-combined-delta-closure/pr-evidence.md`
**Referenz-Slice**: Exakte PR-#93/#96-Pins, alle 15 Source-Blobs und eine
vollständige `Tp7Calculator`-Zeile mit `W5-005`, Funktionsproof,
Showcase-Proof, Guide/Launch, Dimensionsstatus, `AcceptedAsIs` und je einem
fehlenden sowie ungültigen Negativfall vor der restlichen Matrix.
**Konvergenz**: keine materielle Clarify-Frage; alle Checklists bestanden;
Plan-Review ohne offene Aktion; Analyze ohne Critical/High und ohne
undisponiertes Medium; alle Tasks und Gates bestanden; null umsetzbare
Review-Threads.
**Single-writer-Dateien**: `pr-evidence.md`, `wave5-combined-delta.json`,
`wave5-closure.md`, `Directory.Build.props`, `docs/project-statistics.md`,
`Pflichtenheft.md`, `Lastenheft_Abarbeitungsreihenfolge.md`, fünf
Agent-Oberflächen, Run-State, Tasks, archiviertes Lastenheft und kausaler
Closeout.
**Scope-Firewall**: Jede Produkt-, API-, Dependency-, Projekt-, Beispiel-,
Framework-, historische oder externe Source-Lücke stoppt 034 oder wird
finding-basiert disponiert. Sie wird nicht in diesem Feature behoben.
**Unterbrechung**: keine geplante Unterbrechung. Ein echter Abbruch erfordert
read-only Status und danach expliziten Authority-revalidierten Resume.
**Remote-Abschluss**: Exakten Kandidaten committen und pushen, PR erstellen,
Gate-Mapping und Reviews konvergieren, eng begrenzten Human-Approval-Bypass nur
unter den genehmigten Bedingungen verwenden, mergen, Branch löschen und
`main` synchronisieren. Ein Evidence-Closeout entsteht nur für Fakten, die
vor dem Merge nicht wahrheitsgemäß vorliegen konnten.

## Evidence- und Datenarchitektur / Evidence and Data Architecture

### Bindende Eingaben / Binding Inputs

| Lieferung | Bindende Rolle | Prüfung |
|---|---|---|
| PR #93 / Feature 032 | Basis `269c54f...`, Head `cf274c6...`, Merge `e74c33d...`, Funktionsdelta | exakte Git-Pins und erwartete Dateimenge |
| PR #94 | kausaler Feature-032-Closeout | Evidence, nicht Produktdelta |
| PR #95 | kopierbare Prompt-Metadaten | ausdrücklich nicht Produktdelta |
| PR #96 / Feature 033 | Basis `5df2ec3...`, Head `8921bd3...`, Merge `d476e63...`, Showcase-Delta | exakte Git-Pins und erwartete Dateimenge |
| PR #97 | kausaler Feature-033-Closeout | Evidence, nicht Produktdelta |
| Feature-032-Evidence | 15 Quellen, sechs Consumer, zehn Funktionsproofs | strukturierte Eingabebeziehungen |
| Feature-033-Evidence | zehn Showcase-Zeilen, Layouts, Guides und Smokes | sichtbare Abschlussbeziehungen |

Jede bindende Datei erhält einen relativen Pfad und SHA-256. Der Validator
berechnet den Hash vor der Nutzung neu. Git-Objekte und die autoritativen
PR-Dateimengen werden zusätzlich über Git/GitHub-Evidence festgehalten.

### Closure-Datensatz / Closure Dataset

`wave5-combined-delta.json` enthält:

1. Run-Identität, Ausgangscommit, Feature-Head-Wave-Zustände,
   Post-Merge-Ziele und vollständige Reviewfelder;
2. PR-Pins und klassifizierte Dateimengen für #93 bis #97;
3. bindende Eingabepfade und SHA-256;
4. exakt 15 Source-Rollen mit unverändertem Git-Blob;
5. exakt sechs Consumer-Zeilen mit reziproken Source- und Beispielbezügen;
6. exakt zehn Funktionsproofs, zehn Showcase-Abschlüsse und zehn
   Guide-/Launch-Pfade;
7. exakt zehn kombinierte Beispielzeilen mit allen geforderten Dimensionen;
8. null oder mehr `W5D###`-Findings, Product Decisions und Ownergruppen;
9. sieben Preset-Governance-Zeilen und deklarierte Validierungsgates;
10. einen kausalen Wave-Übergang.

### Test-only Validator / Test-only Validator

`Wave5CombinedDeltaClosureTests.cs` nutzt nur BCL-JSON-APIs und vorhandene
Repository-Root-Helfer. Er prüft:

- Existenz, Root-Schema, ordinale IDs und erlaubte Vokabulare;
- exakte PR-/Input-/Source-Provenienz;
- vollständige 15/6/10/10/10-Cardinalities und reziproke Beziehungen;
- eine vollständige Referenzzeile und danach alle zehn Beispielzeilen;
- App-Loop-, View-, Fokus-, Status-, Description-, Layout-, Cell-,
  Boundary- und Guide-Nachweise;
- genau eine Hauptentscheidung je Beispiel;
- `Gap`-/Finding-/Owner-/Product-Decision-Regeln;
- Framework-Ownership für `Wave5Application`, `Wave5ConsoleHost`,
  `Wave5StatusLine`, `Wave5GridView` und lokale Zustandsmodelle;
- Governance-Metadaten, Feature-Head-Sperre und kausalen Abschluss;
- fehlende, doppelte, unbekannte, widersprüchliche, hash-driftende sowie
  LF-/CRLF-äquivalente Negativfälle.

Negativfälle mutieren geparste Daten in Memory oder in einem test-eigenen
Temp-Verzeichnis. Es werden keine separaten getrackten Fehler-Fixtures
angelegt. Der Validator komponiert die akzeptierten 032/033-Verträge, kopiert
aber deren vollständige Testlogik nicht.

## Implementierungsphasen / Implementation Phases

### Phase A - Foundation und test-first Referenz-Slice

1. `pr-evidence.md`, Gate-Anforderungen und Run-State vor Implementierung
   vollständig initialisieren.
2. Bindende Pfade, SHA-256, PR-Pins und Source-Blobs einfrieren.
3. Minimalen Datensatz mit Run, Inputs, 15 Sources, sechs Consumern und
   `Tp7Calculator` anlegen.
4. Validator mit Existenz-, Provenienz-, Calculator- und zwei Negativtests
   ergänzen.
5. Erwarteten Red-Nachweis vor den neun fehlenden Beispielzeilen ausführen.

### Phase B - Vollständige kombinierte Matrix

1. Zehn Funktionsproofs und zehn Showcase-Abschlüsse abgleichen.
2. Zehn Guides/Launch-Pfade und zehn kombinierte Beispielzeilen ergänzen.
3. Normalstart, primäre Aktion, F1/Description, Fokus/Status und `Ctrl+Q`
   je Beispiel prüfen.
4. Framework-Ownership und lokale Sonderlogik entscheiden.
5. Findings/Product Decisions leer bestätigen oder vollständig disponieren.
6. Negative Cardinality-, Beziehung-, Entscheidung-, Gap-, Finding-, Pin-,
   Hash- und Zeilenende-Fälle vervollständigen.

### Phase C - Governance und lokale Gates

1. Sieben Preset-Entscheidungen und Validierungszeilen vervollständigen.
2. `wave5-closure.md` und Feature-Head-Sperre erstellen.
3. Alle zehn kontrollierten `--smoke`-Starts und normalen PTY-Pfade prüfen.
4. Targeted, full Release, Coverage, Format, DocFX/Axe, UTF-8, Security,
   Scope und Agent-Parität ausführen.
5. Pflichtenheft, Reihenfolge, Agent-Kontexte, Lastenheft-Marker und Statistik
   auf den reviewbaren Feature-Head ausrichten, ohne Wave 6 zu starten.

### Phase D - Exact Head, Delivery und kausaler Closeout

1. Version ausrichten, exakte beabsichtigte Dateien stagen und prüfen.
2. Commit, Push und Feature-PR; Gates und Reviews auf dem exakten Head
   konvergieren.
3. Nach Merge lokal auf synchrones `main` zurückkehren.
4. Nur falls kausal erforderlich einen Evidence-only Closeout erstellen:
   Wave 5 `Closed`, Wave 6 `EligibleForIntake`, Lastenheft 20 und reserviertes
   Feature 035, aber kein Feature-035-Branch und keine Implementierung.
5. Closeout ohne Test- oder Produktlogik validieren, mergen und erneut
   synchronisieren.

## Validierungsstrategie / Validation Strategy

| Gate | Geplanter Nachweis |
|---|---|
| Static candidate | `git diff --check`, staged inventory, Placeholder-, Protected- und Generated-Path-Scan |
| State | Bash- und PowerShell-State-Validatoren an logischen Checkpoints |
| Targeted closure | Release-Filter für `Wave5CombinedDeltaClosureTests` plus bestehende Wave-5-Matrizen |
| TP7 regression | alle `Tp7*`, `Wave5Functional` und `Wave5Showcase` Smokes |
| Entry points | zehn `--smoke`-Starts und zehn normale PTY-Pfade mit Aktion, F1 und `Ctrl+Q` |
| Full regression | vollständiges `dotnet test TuiVision.sln --configuration Release` |
| Coverage | kanonischer Coverlet-Lauf und mindestens 70 Prozent für fünf Assemblies |
| Format | `dotnet format TuiVision.sln --verify-no-changes` |
| Dokumentation | `docfx docfx.json`, danach Playwright/Axe und UTF-8/Text-first-Review |
| Security/scope | Secret-, Gitleaks-, Supply-Chain-, Dependency-/Project-, Protected-Path- und Generated-Output-Prüfung |
| Agent parity | lokale Homogenität und Remote-Bash-/PowerShell-Parität |
| Plattform | PR-Kontext auf Ubuntu, macOS und Windows mit echtem Release-Body |
| Exact head | temporäre Gate-Evidence gegen committed Requirements und reviewten PR-Head |
| Review | GraphQL-Threads, Kommentare, Reviewergebnisse und ehrliche Providergrenzen |

Vor jedem `dotnet build` oder `dotnet test` werden alle drei Versionsfelder
auf `1.34.<patch>.<build>` gesetzt und nur der manuelle Build-Zähler genau
einmal erhöht. Vor Commit oder Push werden die Felder erneut ausgerichtet,
ohne den Build-Zähler zu erhöhen, sofern kein weiterer Build/Test lief.

## Projektstruktur / Project Structure

```text
specs/034-wave5-combined-delta-closure/
├── autonomous-gate-requirements.json
├── autonomous-run-state.json
├── checklists/
│   ├── decision-followup.md
│   ├── framework-proof.md
│   ├── governance-a11y.md
│   ├── plan-quality.md
│   ├── plan-review.md
│   ├── provenance-cardinality.md
│   └── requirements.md
├── contracts/
│   └── wave5-combined-delta-acceptance.md
├── data-model.md
├── delivery-closeout.md
├── plan.md
├── pr-evidence.md
├── quickstart.md
├── research.md
├── retrospective.md
├── spec.md
├── tasks.md
├── wave5-closure.md
└── wave5-combined-delta.json

tests/TuiVision.Examples.SmokeTests/
└── Wave5CombinedDeltaClosureTests.cs
```

Gemeinsame Statusoberflächen: `Directory.Build.props`, `Pflichtenheft.md`,
`Lastenheft_Abarbeitungsreihenfolge.md`, `docs/project-statistics.md`,
`AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
`.github/copilot-instructions.md` und
`.github/agents/copilot-instructions.md`.

**Strukturentscheidung**: Alle neue ausführbare Logik bleibt in genau einer
test-only Klasse des bestehenden Smoke-Test-Projekts. Feature-lokale Evidence
trägt den Audit; gemeinsame Dateien ändern sich nur für Version, Status,
Reihenfolge, Statistik und Agent-Parität.

## Komplexitätsverfolgung / Complexity Tracking

Keine Verfassungsverletzung, neue Dependency, neues Projekt oder
produktionsseitige Abstraktion ist geplant.
