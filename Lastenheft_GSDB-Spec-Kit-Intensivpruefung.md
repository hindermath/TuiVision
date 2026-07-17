<!-- gsdb-spec-kit-intensivpruefung:start -->
# Lastenheft GSDB-Spec-Kit-Intensivpruefung fuer TuiVision

**Stand / Date:** 2026-07-04

## Zweck / Purpose

**DE:** Dieses Lastenheft bereitet einen spaeteren, manuell gestarteten
Spec-Kit-Lauf vor. Ziel ist eine intensive Pruefung von TuiVision gegen
die Generische Secure-Development Basis (GSDB), die Projekt-Constitution, die
installierten Governance-Presets und die projektspezifischen Nachweise.

**EN:** This requirements document prepares a later manually started Spec Kit
run. The goal is an intensive review of TuiVision against the Generic
Secure Development Baseline (GSDB), the project constitution, installed
governance presets, and project-specific evidence.

## Ausgangslage / Context

- Die GSDB liegt im Projekt unter `docs/secure-development/`.
- Projektspezifische Nachweise liegen bevorzugt unter `docs/security/`.
- Der GSDB-Preflight dient nur der Vorbereitung und ersetzt keinen
  Spec-Kit-Lauf.
- Human-only-Punkte duerfen nicht als erledigt behauptet werden.

## Scope

- GSDB-Richtlinie, alle 12 Einzelchecklisten, Sammelband und mitgeltende
  Dokumente pruefen.
- MSL-Status und sprachspezifische Secure-Coding-Regeln pruefen.
- Preset-Installation und Preset-zu-CL-Mapping pruefen.
- Projektspezifische Evidenz unter `docs/security/` oder gleichwertigen
  Nachweisorten pruefen.
- Alle Ergebnisse mit `Applicable`, `N/A` oder `Open` dokumentieren.

## Nicht-Ziele / Non-Goals

- Kein automatisches Starten eines Spec-Kit-Laufs.
- Keine formale Freigabe, keine Secret-Rotation, keine Providerfreigabe und
  keine Branch-Protection-Aenderung durch einen Agenten.
- Keine pauschale Behauptung, dass das Projekt sicher ist.

## Erwartete Artefakte / Expected Artefacts

- Aktualisierte Spec-Kit-Artefakte fuer den GSDB-Prueflauf.
- GSDB-Evidenzmatrix mit Status, Begruendung, Evidenzpfad, Owner,
  Follow-up, Re-Evaluation-Trigger und Restrisiko.
- Dokumentierte `N/A`-Entscheidungen mit kurzer Begruendung.
- Liste offener Punkte fuer spaetere Haertung.

## Akzeptanzkriterien / Acceptance Criteria

- Jeder relevante GSDB-Pruefpunkt ist sichtbar behandelt.
- Keine Checkliste wird stillschweigend ausgelassen.
- Offene Punkte enthalten Owner, Follow-up und Re-Evaluation-Trigger.
- Human-only-Punkte sind als Human-only, `Open` oder `N/A` dokumentiert.
- Das Ergebnis verweist auf konkrete Evidenzpfade.

## Kopierbarer Spec-Kit Specify Prompt

```text
$speckit-specify Fuehre eine intensive GSDB-Pruefung fuer dieses Repository durch. Nutze docs/secure-development/, constitution.md, .specify/memory/constitution.md, docs/security/, Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md und die installierten Governance-Presets als Pruefgrundlagen. Starte keine formale Freigabe und behaupte keine Human-only-Punkte als erledigt. Erstelle eine Feature-Spezifikation, die alle relevanten GSDB-Pruefpunkte mit Applicable, N/A oder Open behandelt. Dokumentiere je Pruefpunkt Begruendung, Evidenzpfad, Owner, Follow-up, Re-Evaluation-Trigger und Restrisiko. Wenn ein Punkt nicht anwendbar ist, dokumentiere N/A mit kurzer Begruendung. Wenn Evidenz fehlt, dokumentiere Open mit konkreter Nacharbeit.
```

## Kopierbarer Autonomous-Prompt / Copyable Autonomous Prompt

```text
$speckit-autonomous Use `Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md` as the
binding intake for a complete autonomous GSDB evidence review in
MergeAndSync mode.

Start only from clean synchronized main and determine the next free Spec Kit
feature number. Run the complete lifecycle from Specify through repeated
Clarify, domain checklists, Plan, plan review, Tasks, repeated Analyze,
Implement, validation, repository delivery, and retrospective.

Review the complete GSDB policy, all twelve checklists, the checklist
collection, governing documents, constitution, installed governance presets,
and existing repository evidence. Give every relevant checkpoint an explicit
Applicable, AlreadySatisfied, N/A, Open, or FollowUp disposition with
rationale, evidence path, owner, follow-up, re-evaluation trigger, and
residual risk. Never convert missing evidence or a human-only decision into a
Pass.

Keep this as an evidence and applicability review. Do not silently perform
product hardening, change provider or organization settings, rotate secrets,
claim legal or audit approval, or weaken existing gates. Create only non-empty
finding-derived follow-up intakes; do not create empty branches or pull
requests.

Run all documentation, security, parity, and repository checks triggered by
the actual changes. Use remote authority only for this repository's non-empty
feature delivery. Merge only after applicable checks and reviews converge,
then return to clean synchronized main.
```
<!-- gsdb-spec-kit-intensivpruefung:end -->
