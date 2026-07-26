# Lastenheft 16: Gemeinsamer Pre-Wave-5-/Wave-6-Konformitätsabschluss

## 0. Dokumentstatus

**Vorgesehener Spec-Kit-Branch:** `031-combined-conformance-closure`

**Verbindliche Reihenfolge:** nach dem gemergten Feature
`030-tv203-magiblot-evolution-audit`, vor Wave 5 und Wave 6

**Lieferart:** unabhängige Revalidierung ohne neue Runtime-Funktion

*Feature 031 independently revalidates the combined TV203, Free Vision,
Terminal.GUI, and magiblot evidence before either example wave may start.*

## 1. Zweck / Purpose

Der Lauf bestätigt unabhängig, dass alle akzeptierten Verträge und Consumer,
alle 48 `TGO*`- und 48 `MB*`-Beobachtungen sowie die null kanonischen
`CF*`-Findings vollständig, widerspruchsfrei und auf dem gemergten Stand
nachweisbar bleiben.

*The run independently confirms that all accepted contracts and consumers,
all 48 TGO and 48 MB observations, and the zero canonical CF findings remain
complete, consistent, and reproducible on the merged baseline.*

## 2. Verbindliche Eingaben / Binding Inputs

- Feature 024 contract and historical audit
- Features 025 and 026 hardening evidence
- Feature 028 independent prior closure
- Feature 029 Terminal.GUI audit and handoff
- Feature 030 magiblot audit and combined dispositions
- Read-only `tv203s/`, `TVDEMOS/`, and `TVFM/`

## 3. Anforderungen / Requirements

1. Revalidate exact source identities and hashes from Features 029 and 030.
2. Revalidate exactly 48 accepted contracts and 13 consumer groups.
3. Revalidate exactly 48 TGO and 48 MB observations with one disposition each.
4. Confirm zero canonical CF findings and zero non-empty owner groups.
5. Confirm that no hardening Lastenheft was suppressed incorrectly.
6. Run targeted audit validators, full Release tests, canonical coverage,
   DocFX/A11Y, secret, scope, agent-parity, and exact-head remote gates.
7. Change no runtime, public API, dependency, package, example, consumer,
   historical, or external source.
8. Set Wave 5 to `Eligible` only after every gate passes on the reviewed
   merged candidate.
9. Keep Wave 6 at most `ConditionallyReady` until Wave 5 completes and its
   delta review passes.

## 4. Stop-Grenzen / Stop Boundaries

Stop on pin drift, missing or duplicate relation, unresolved observation,
unexpected CF finding, product decision, ambiguous owner, failing mandatory
gate, or any requested product remediation.

## 5. Akzeptanz / Acceptance

- All combined evidence cardinalities and hashes match.
- Zero open finding or product decision remains.
- All triggered local and remote gates pass.
- Wave and next-intake markers are synchronized.
- The final repository is clean on synchronized `main`.
