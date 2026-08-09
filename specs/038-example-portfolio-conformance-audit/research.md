# Forschung und Planungsentscheidungen / Research and Planning Decisions

## R1 – Bindende Eintrittsgrenze / Binding entry boundary

**Entscheidung / Decision**: Verwende ausschließlich die vier im
`autonomous-run-state.json` gebundenen Intake-/Review-/Serienartefakte und den
Feature-037-Abschluss als Eintrittsgrenze. Der Planungs-HEAD ist
`01c4759ca9883b78914affecfd8cfb224789654b`.

**Begründung / Rationale**: Die vier SHA-256-Werte stimmen mit dem Run-State
überein. Feature 037 belegt Wave 6 als `Closed`, den Portfolioaudit als
`Eligible` und null Candidate Findings beziehungsweise Product Decisions.

*Use only the four hash-bound intake/review/series artifacts and Feature 037
closure as entry evidence. Their hashes match the run state, and Feature 037
proves the required closed/eligible state.*

## R2 – Exakte Portfolio-Menge / Exact portfolio population

**Entscheidung / Decision**: Die Grundmenge besteht aus den 37 direkten
`examples/*/*.csproj`-Projekten. Die zwei Projekte unter `examples/Shared/`
sind gemeinsame Implementierungsassemblies und keine zusätzlichen
Portfolioeinträge.

**Evidence**: Die sortierte direkte Projektliste enthält 37 Pfade und hat
SHA-256 `cb2f6568b70f2a62cd529250777e849dd2cd026c05732df81733b2fc3d177333`.

**Verworfene Alternative / Rejected alternative**: Alle 39 rekursiv gefundenen
`.csproj`-Dateien als Portfolio zählen. Das würde interne Shared-Assemblies als
gelieferte Beispiele fehlklassifizieren.

## R3 – Stabile ExampleIds / Stable ExampleIds

**Entscheidung / Decision**: Vergib `EX001`–`EX037` nach bindender Wave-
Reihenfolge und innerhalb jeder Wave alphabetisch. IDs werden nach Auditbeginn
nicht aus Dateisystemreihenfolge neu berechnet.

**Begründung / Rationale**: Eine explizite, menschenlesbare Reihenfolge bleibt
stabil, selbst wenn eine Shell anders sortiert. Portfolio-Drift blockiert,
statt bestehende IDs still zu verschieben.

## R4 – Eine kanonische JSON-Wahrheitsquelle / One canonical JSON source

**Entscheidung / Decision**: `example-portfolio-audit.json` ist die einzige
strukturierte Wahrheitsquelle. Neun fachliche Markdown-Projektionen plus
`pr-evidence.md` ergeben exakt zehn vollständige text-first
Markdown-Evidence-Familien.

**Begründung / Rationale**: Mehrere unabhängig gepflegte JSON-Dateien würden
atomare Updates und reziproke Relationen unnötig erschweren. Ein Datensatz
erlaubt fail-closed Validierung; Markdown bleibt für Menschen vollständig.

## R5 – Historische Source-IDs / Historical source IDs

**Entscheidung / Decision**: Neue historische IDs verwenden die Präfixe
`TV203-E`, `TVDEMOS-E` und `TVFM-E` mit dreistelliger, nach normalisiertem
relativem Pfad ordinal sortierter Nummer. Unveränderliche akzeptierte
TuiVision-Vorgängerevidence verwendet entsprechend `BASE-E001+`. Pfad und
SHA-256 sind Pflicht.

**Begründung / Rationale**: Authority-Präfix und Hash verhindern, dass gleiche
Dateinamen aus verschiedenen historischen Bäumen vermischt werden. Header und
Implementierungsdateien können gemeinsam referenziert werden.

## R6 – Akzeptierte Vergleichspins / Accepted comparison pins

**Entscheidung / Decision**: Keine externe Quelle wird erneut abgerufen.
Folgende lokale Evidence bleibt bindend:

| Evidence | Unveränderlicher Pin / Immutable pin | Lokaler Evidence-Hash / Local evidence hash |
|---|---|---|
| Feature 024 Free Vision | Commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2`; IDs `FV001`–`FV015` | Manifest `9eaa10e086e100882bf3e69924dd136e71ac81d227da9687a05008ef99ceb132`; Dataset `ff4184630820156ca9ed48e53f5b2655200d1d25930c3c6b264e9ecb07e1d986` |
| Feature 029 Terminal.GUI | Tag `v1.9.0`, Tag-Objekt `4b812e44798f2c7567afec50ba9a9293b6beb6de`, Commit `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3`; IDs `TGSR001`–`TGSR025` | Manifest `0607e02091c1d23ce1388380dc40d7c6aa284e047b8bcb74eff0567655c6cb14`; Dataset `3c2a9610fe01add81724f19836191b2d79fe411593e4785ef34dcc2a6602556f` |
| Feature 030 magiblot/tvision | Commit `57b6f56b38e0ee75240a80a10ee0e11470c24693`, Tree `96dd03873955689ff0a79f6c8107a8148fe1ebd6`; IDs `MBSR001`–`MBSR050` | Manifest `f0d71237833abed68fabe8e748a112998c20a21c6fecdcc699d909cb037c15ec`; Dataset `741dac6cb2453362f4386b156fc803e5d5461b151eac753949df6e6e7722ce00` |
| Feature 030 kombinierte Findings | 96 entschiedene Beobachtungen, null kanonische Findings | `b7759740651e3db4448be835cf16d51ddaa9d7bc13002b57a9dec6e46cacc34e` |
| Feature 037 Wave-6-Closure | Wave 6 `Closed`, Audit `Eligible` | Dataset `64d1eb57171453706a20c8948741c0344476366ab4e1f78978e8433a6e957af7`; Closeout `c2b3e3836b13082d696361220aeaaccda2cbd11e02fef832e6ab54d6c97df806` |

## R7 – Vergleichsrelationen / Comparison relations

**Entscheidung / Decision**: Die drei Vergleichsrelationen verwenden dieselbe
Dimensionsform wie historische Relationen: `Pass`, `IntentionalDeviation`,
`Gap` oder `N/A`. `N/A` braucht Begründung und Trigger. Ein Vergleich darf nur
bei fachlich vergleichbarer Verantwortung `Pass` oder
`IntentionalDeviation` erhalten.

**Verworfene Alternative / Rejected alternative**: Jede Portfoliozeile an jede
Vergleichsquelle binden. Das würde Abwesenheit fachlicher Verantwortung als
Defekt missdeuten.

## R8 – Repräsentativer Vertikalschnitt / Representative vertical slice

**Entscheidung / Decision**: `EX036 Tp7FileManager` ist der erste Slice.

**Begründung / Rationale**: Er verbindet 24 kontrollierte TVFM-Quellen,
Feature-035/036-Produkt-Evidence, Feature-037-Closure, Datei- und
Persistenzgrenzen, App-Loop, Fokus, Status, Description, Zellen, A11Y,
Small-Terminal- und Plattformfallbacks. Damit übt ein Slice fast alle
Relationsarten aus, ohne Produktänderung zu verlangen.

**Verworfene Alternative / Rejected alternative**: Mit dem alphabetisch ersten
Eintrag beginnen. Das wäre einfach, würde aber Handoff-, Datei- und
Plattformrisiken nicht früh genug prüfen.

## R9 – Red-Green-Grenze / Red-green boundary

**Entscheidung / Decision**: Der erste erwartete Red-Lauf ist ein vollständig
kompilierender fokussierter Test, der nur wegen des fehlenden `EX036`-Slices
fehlschlägt. Nach dessen Grün wird die 37-Zeilen-Vollständigkeitsprüfung rot und
anschließend waveweise grün gemacht.

**Begründung / Rationale**: Ein Kompilations-, Restore- oder Infrastrukturfehler
beweist keine fehlende Auditfunktion. Semantische Red-Ergebnisse halten die
Beweisgrenze eindeutig.

## R10 – Deterministischer Validator / Deterministic validator

**Entscheidung / Decision**: Verwende eine interne C#-Validatorroutine im
vorhandenen MSTest-Projekt. Sie nimmt den Repository-Root explizit entgegen,
verwendet `System.Text.Json`, `StringComparer.Ordinal`, stabile Sortierung und
kontrollierte Fixture-Pfade.

**Begründung / Rationale**: C#/.NET ist bereits vorhanden und memory-safe. Ein
neues Skript würde unnötig Bash-/PowerShell-, Manpage- und Cmdlet-Governance
auslösen; eine neue Library oder ein neues Testprojekt wäre ebenfalls
unverhältnismäßig.

## R11 – Fehlerhafte Fixtures / Malformed fixtures

**Entscheidung / Decision**: Quellenkontrollierte JSON-Fixtures mutieren genau
eine Invariante je Datei. Kategorien sind Syntax/Schema, Inventar,
Relationen, Dimensionen/Disposition, Findings/Dedup/Owner, DAG/Handoff,
Governance/Gate und Pfadgrenzen.

**Begründung / Rationale**: Ein Fehler je Fixture ergibt stabile Diagnosen und
verhindert, dass ein früher Parserfehler spätere Integritätsfehler verdeckt.

## R12 – Finding-Deduplizierung / Finding deduplication

**Entscheidung / Decision**: Dedupliziere nach
`<primary-owner>:<dimension>:<root-cause-slug>`. Der Slug ist kontrolliertes
ASCII-Kebab-Case; Beobachtungstext und Beispielname sind nicht Teil des Keys.
Erst der eingefrorene, sortierte Root-Cause-Satz erhält `EF001+`.

**Begründung / Rationale**: Derselbe Defekt in mehreren Beispielen bleibt ein
Finding. Freitext oder Beispielname im Schlüssel würde künstliche Duplikate
erzeugen.

## R13 – Primary-Owner-Regel / Primary-owner rule

**Entscheidung / Decision**: Die Ursache entscheidet, nicht die sichtbare
Folgewirkung:

1. fehlende oder umgangene wiederverwendbare Framework-Verantwortung → `FrameworkReuse`;
2. sonst Beispielverhalten, Fokus, Command, Interaktion oder Rückmeldung → `BehaviorInteraction`;
3. sonst Real-Path-Proof, Terminal, Fallback oder Plattform-Evidence → `ProofPlatform`;
4. sonst Guide, Lernwert, text-first A11Y oder didaktische Konsistenz → `LearningA11Y`.

Mehrere Auswirkungen stehen in `SecondaryImpacts`. Bleibt die Ursache zwischen
zwei Ownern unklar, stoppt der Lauf; sie wird nicht autonom geraten.

## R14 – Handoff und Closure / Handoff and closure

**Entscheidung / Decision**: Erzeuge 0 bis 4 Remediation-Intakes, exakt einen
je nicht leerer Owner-Gruppe, in topologischer Reihenfolge. Erzeuge danach
immer genau einen unabhängigen Closure-Intake.

Eine Finding-Abhängigkeit `A` setzt `B` voraus erzeugt bei verschiedenen
Primary Ownern die Kante `Owner(B) -> Owner(A)`; Same-Owner-Abhängigkeiten
bleiben intern und doppelte Kanten werden kollabiert. Bei mehreren freien
Knoten entscheidet die feste Owner-Reihenfolge aus R13 deterministisch.

*A finding dependency where A requires B creates `Owner(B) -> Owner(A)` when
their primary owners differ. Same-owner dependencies remain internal,
duplicate edges collapse, and the fixed R13 owner order breaks topological
ties deterministically.*

**Begründung / Rationale**: Auch ein Audit ohne Findings braucht einen
unabhängigen Closure, der die endgültige Konformitätsaussage trennt. Leere
Remediation-Features liefern keinen Wert und bleiben unterdrückt.

## R15 – Evidence vor Implementierung / Evidence before implementation

**Entscheidung / Decision**: Evidence-Skelett, Gate-Anforderungen, Authority,
Protected Roots und aktuelle Hashes werden vor dem ersten Validator-Edit
angelegt. Alle Umsetzungs- und Gatewerte beginnen ehrlich als `Not Assessed`.

**Begründung / Rationale**: So kann der autonome Lauf nach Unterbrechung
rekonstruiert werden, ohne spätere Erfolgsaussagen rückwirkend zu erfinden.

## R16 – Dokumentations- und A11Y-Trigger / Documentation and A11Y triggers

**Entscheidung / Decision**: Feature-lokale Markdown-Dateien erhalten immer
Text-, Sprach-, Link- und UTF-8-Review. Die am Abschluss verpflichtende Änderung
von `docs/project-statistics.md` löst DocFX, Playwright/Axe und UTF-8-Lynx aus.
XML/Public-API-Dokumentation bleibt `N/A`.

## R17 – Architektur- und Security-Evidence / Architecture and security evidence

**Entscheidung / Decision**: Feature-lokale Governance- und
Framework-Usage-Evidence ist ausreichend, solange keine Produkt-, Trust-
Boundary-, Dependency- oder Architekturentscheidung entsteht. Allgemeine
Dateien unter `docs/security/`, `docs/architecture/` und
`docs/accessibility/` bleiben triggerbasiert `N/A`.

## R18 – Aktuelle Delivery-Autorität / Current delivery authority

**Entscheidung / Decision**: Der fortgesetzte Lauf besitzt ausdrückliche
`MergeAndSync`-Autorität für Exact-Head-Evidence, Review-Konvergenz, Merge,
Branchbereinigung und `main`-Synchronisierung. Ein kausaler evidence-only
Closeout ist nur bei einer echten Post-Merge-Evidence-Lücke zulässig. Der Lauf
darf keine Folgefeatures starten oder vollständige Portfolio-Konformität
erklären.

**Begründung / Rationale**: Die aktuelle Nutzeranweisung erteilt Remote-
Delivery-Autorität ausdrücklich; Folgefeature- und Provider-Administration
bleiben trotzdem ausgeschlossen.
