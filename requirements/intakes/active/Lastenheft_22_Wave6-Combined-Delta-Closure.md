<!-- intake-authoring:begin -->
# Lastenheft 22: Wave-6 Combined Delta Closure

## Dokumentstatus / Document Status

- Status: `ReadyForReview`
- Vorgesehene Feature-ID: `037-wave6-combined-delta-closure`
- Delivery Authority: `LocalImplementation`
- Bevorzugter nächster Intake: ja

*Status: ready for review. Reserved feature ID:
`037-wave6-combined-delta-closure`. Delivery authority is local implementation.
This is the preferred next intake.*

## Ziel / Goal

Das tatsächliche kombinierte Wave-6-Delta aus Feature 035 und Feature 036 wird
unabhängig, reproduzierbar und read-only geprüft. Die Prüfung entscheidet, ob
Wave 6 geschlossen werden kann und ob der nachgelagerte Portfolio-Audit
startfähig wird.

*Independently and reproducibly audit the actual combined Wave-6 delta from
Features 035 and 036. The audit decides whether Wave 6 can close and whether
the later portfolio audit becomes eligible.*

## Verbindliche Eingaben / Binding Inputs

- `specs/035-wave6-tvfm-functional-porting/`
- `specs/036-wave6-tvfm-showcase-remediation/`
- Feature- und Closeout-PR-Evidence der beiden Lieferstufen
- historische Quellen unter `TVFM/` sowie relevante `tv203s/`-Referenzen
- aktuelle Guides, Tests, Framework-Usage- und Showcase-Matrizen

Historische und externe Vergleichsquellen bleiben read-only. Exakte Commit-,
Datei- und Quellhash-Provenienz ist vor der Bewertung erneut zu prüfen.

## Anforderungen / Requirements

1. Rekonstruiere ausschließlich das Produktdelta der beiden Wave-6-Features;
   fremde Metadatenänderungen gehören nicht zur fachlichen Vergleichsbasis.
2. Prüfe Funktions-, Interaktions-, Layout-, Fokus-, StatusLine-, Hilfe-,
   A11Y-, Plattform-, Sicherheits- und Proof-Verträge gemeinsam.
3. Weise echte `app.Run()`-, View-, Fokus-, Dialog-, Dateiworkspace- und
   Buffer-/Cell-Pfade nach.
4. Prüfe, dass lokale Beispielsonderlogik keine wiederverwendbare
   Framework-Funktion verdeckt oder dupliziert.
5. Gib jedem geprüften Vertragsbereich genau eine Entscheidung:
   `AcceptedAsIs`, `AcceptedIntentionalDeviation`, `CandidateFinding` oder
   `ProductDecision`.
6. Ein Finding benötigt eine reproduzierbare Verhaltens-, Interaktions-,
   Proof-, Dokumentations-, A11Y-, Plattform- oder Framework-Lücke.
   Quelltextunterschiede allein reichen nicht.
7. Findings erhalten stabile `W6D###`-IDs, Evidence, Owner und
   Wiederbewertungsauslöser. Sie werden nicht innerhalb dieses Audits behoben.
8. Ein deterministischer test-only Closure-Validator muss positive
   Kardinalitäten und negative Fixtures prüfen.
9. Bei null Findings und null Produktentscheidungen wird Wave 6 als `Closed`
   und der Post-Wave-6-Portfolioaudit als `Eligible` markiert.
10. Ein Post-Merge-Closeout ist nur zulässig, wenn kausale Merge-Fakten nicht
    wahrheitsgemäß auf dem geprüften Feature-Head stehen konnten.

## Nicht-Ziele / Non-Goals

- keine Runtime-, API-, Paket-, Projekt- oder Beispieländerung
- keine automatische Finding-Behebung
- kein Start des Post-Wave-6-Audits
- keine neue vollständige Fremdframeworkprüfung
- keine Änderung unter `tv203s/`, `TVDEMOS/` oder `TVFM/`

## Validierung / Validation

- deterministischer Closure-Validator und negative Fixtures
- zielgerichtete Wave-6-Smokes und vollständige Release-Tests
- kanonisches Fünf-Assembly-Coverage-Gate
- Format-, DocFX-, Axe-, Secret-, Supply-Chain- und Agent-Paritätsgates
- Ubuntu-, macOS- und Windows-Evidence
- Exact-Head-Evidence unmittelbar vor einem Merge

Der manuelle Build-Zähler wird vor jedem einzelnen `dotnet build` oder
`dotnet test` erhöht. Ein unerwartetes Finding, eine Produktentscheidung,
Provenienzdrift oder ein Pflichtgate-Fehler stoppt den Lauf.

## Abnahme / Acceptance

- Das kombinierte Delta ist vollständig und ohne doppelte Ownership geprüft.
- Jede Entscheidung besitzt aktuelle Evidence und Restrisiko.
- Wave 6 und der Portfolioaudit erhalten genau einen wahrheitsgemäßen Status.
- Feature 038 oder ein Remediation-Feature wird nicht automatisch gestartet.

<!-- intake-authoring:prompts -->
## Kopierbare Spec-Kit-Prompts / Copyable Spec Kit Prompts

<!-- spec-kit-command-id: speckit.specify -->
```text
$speckit-specify Use requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md as the binding intake for Feature 037. Create or update only specs/037-wave6-combined-delta-closure. Preserve the read-only audit scope, exact provenance, stop boundaries, validation, and acceptance rules. Do not implement, commit, push, create a pull request, merge, start the portfolio audit, or change product behavior.
```

<!-- spec-kit-command-id: speckit.autonomous -->
```text
$speckit-autonomous Execute the complete autonomous Spec Kit run for Feature 037 using requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md as the binding intake. Delivery mode: LocalImplementation. Run all useful Specify, Clarify, Checklist, Plan, Tasks, Analyze, Implement, validation, and retrospective stages to convergence. Preserve the read-only audit scope and stop on any finding, product decision, provenance drift, or mandatory gate failure. Do not push, create or merge a pull request, use bypass authority, start another feature, or change runtime, API, dependency, project, or example behavior.
```
<!-- intake-authoring:end -->
