# Quickstart: Wave-5 Combined Delta Closure

## Zweck / Purpose

Dieser Quickstart beschreibt die reproduzierbare Prüfung von Feature 034.
Er ändert keine Beispiele und startet Wave 6 nicht.

*This quickstart describes the reproducible Feature-034 audit. It changes no
example and does not start Wave 6.*

## 1. Ausgangszustand / Starting State

```bash
git switch 034-wave5-combined-delta-closure
git status --short --branch
jq . .specify/feature.json
bash .specify/presets/autonomous-run-governance/scripts/validate-autonomous-run-state.sh \
  --state specs/034-wave5-combined-delta-closure/autonomous-run-state.json
```

Erwarteter Feature-Pfad:

```text
specs/034-wave5-combined-delta-closure
```

## 2. Bindende Vorgänger / Binding Predecessors

Prüfe diese Artefakte read-only:

```text
specs/032-wave5-tp7-functional-porting/
specs/033-wave5-tp7-showcase-remediation/
```

Verifiziere PR #93 und #96 als Produktlieferungen, PR #94 und #97 als
Closeouts sowie PR #95 als Nicht-Produkt-Metadatenarbeit.

## 3. Referenz-Slice / Reference Slice

Der erste Slice bindet:

- beide Produkt-PR-Pins und ihre Dateimengen;
- alle 15 `TVDEMOS/*.PAS`-Blobs;
- die Consumergruppe `W5-005`;
- `Tp7Calculator` mit Funktion, Showcase, Guide, Launch und Entscheidung;
- einen fehlenden und einen ungültigen Negativfall.

Der erste targeted Test muss erwartungsgemäß rot sein, solange die restlichen
neun Beispielzeilen fehlen. Danach wird die vollständige Matrix grün.

## 4. Targeted Proof

Vor jedem `dotnet build` oder `dotnet test`:

1. aktuelle Branch-Commitanzahl bestimmen;
2. alle drei Versionsfelder auf `1.34.<patch>.<build>` ausrichten;
3. nur den manuellen Build-Zähler genau einmal erhöhen.

Targeted Scope:

```text
Wave5CombinedDeltaClosureTests
Wave5FunctionalSmokeMatrixTests
Wave5ShowcaseSmokeMatrixTests
Tp7CalculatorSmokeTests
Tp7ApplicationSmokeTests
Tp7ResourceSmokeTests
Tp7DomainSmokeTests
```

## 5. Entry-Point-Prüfung / Entry-Point Review

Für alle zehn Projekte:

```bash
dotnet run --no-build --configuration Release --project examples/Tp7<Name> -- --smoke
```

Prüfe zusätzlich im normalen PTY-Modus:

1. erster sichtbarer Zustand;
2. eine primäre Tastaturaktion;
3. Fokus oder Statusänderung;
4. `F1` / Description;
5. `Ctrl+Q`.

## 6. Vollständige Gates / Full Gates

```text
git diff --check
dotnet format TuiVision.sln --verify-no-changes
targeted Release tests
full Release tests
canonical Coverlet coverage
docfx docfx.json
Playwright/Axe
UTF-8 text-first review
secret, supply-chain and scope scans
agent parity
```

Jeder Command, Build-Zähler, Scope, Ergebnis und Failure-Boundary wird in
`pr-evidence.md` erfasst.

## 7. Feature-Head-Grenze / Feature-head Boundary

Vor dem Merge ist nur dieser Zustand zulässig:

```text
Feature decision: ReadyForMerge
Wave 5: BlockedPendingCausalClosure
Wave 6: BlockedPendingCausalClosure
```

## 8. Remote-Lieferung / Remote Delivery

1. exakten Kandidaten stagen und prüfen;
2. PR erstellen;
3. Gate-Evidence auf den reviewten Head abbilden;
4. alle umsetzbaren Threads schließen;
5. fehlende Reviewer ehrlich dokumentieren;
6. nur unter der delegierten engen Policy mergen;
7. Branch löschen und lokales `main` synchronisieren.

## 9. Kausaler Abschluss / Causal Closeout

Nur nach dem Feature-Merge darf ein Evidence-only Closeout festhalten:

```text
Wave 5: Closed
Wave 6: EligibleForIntake
Next intake: Lastenheft_20_Wave6-TVFM-Functional-Porting.md
Feature 035: reserved, not started
Stage: Retrospective
Status: Completed
nextExactAction: N/A
```

Der Closeout benötigt keine Änderung an Test- oder Produktlogik und startet
keinen weiteren autonomen Lauf.
