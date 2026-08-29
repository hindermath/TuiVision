# Lastenheft-Abarbeitungsreihenfolge / Requirements Processing Order

Diese Datei ist die menschenlesbare Root-Ansicht der kanonischen
Intake-Serie. Sie wird aus
`requirements/intakes/series/tui-vision-delivery/manifest.json` erzeugt und
nicht unabhängig bearbeitet.

*This file is the human-readable root view of the canonical intake series. It
is generated from the series manifest and is not maintained independently.*

## Bevorzugter nächster Intake / Preferred Next Intake

1. `requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md`
   - unabhängiger Security-Hardening-Intake nach dem Dokumentationsabschluss
   - Status: `Eligible`
   - startet nicht automatisch

## Abgeschlossene harte Voraussetzung / Completed Hard Prerequisite

2. `requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md`
   - geliefert durch Feature `037-wave6-combined-delta-closure` und PR `#139`
   - Status: `Completed`
   - erfüllt die harte Voraussetzung des Portfolioaudits

3. `requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md`
   - geliefert durch Feature `038-example-portfolio-conformance-audit` und PR `#144`
   - Status: `Completed`
   - erfüllt die harte Voraussetzung des unabhängigen Portfolio-Closures

4. `requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md`
   - unabhängig als `PortfolioConformantAndLearningReady` abgeschlossen
   - Status: `Completed`
   - erfüllt die Produktvoraussetzung der späteren Formtransaktion

5. `requirements/intakes/active/Lastenheft_Constitution_Change.md`
   - CC-01 bis CC-07 als vollständig `AlreadySatisfied` revalidiert
   - Status: `Completed`
   - gemeinsame Schreibflächen für die Quellenpolicy sind freigegeben

6. `requirements/intakes/active/Lastenheft_Source-Reference-Policy.md`
   - durch Feature `041-source-reference-policy` lokal vollständig geliefert
   - Status: `Completed`
   - harte Policy-Voraussetzung des Transactional Form Model ist erfüllt

7. `requirements/intakes/active/Lastenheft_Transactional-Form-Model.md`
   - Issue #154 und die genehmigten Phasen 1 bis 4 sind durch Feature
     `042-transactional-form-model` lokal vollständig geliefert
   - Status: `Completed`

8. `requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md`
   - geliefert durch Feature `043-documentation-publishing-closure` und PR `#157`
   - Status: `Completed`
   - schließt Guides, Publishing-Nachweis und Dokumentations-Reconciliation

## Weitere unabhängige aktive Intakes / Other Independent Active Intakes

9. `requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md`
10. `requirements/intakes/active/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md`

Die Sicherheits- und Governance-Intakes bleiben unabhängige Wurzeln. Die
Reihenfolge des Sandbox-Hardenings ist eine Auswahlentscheidung und keine
neu erfundene harte Produktabhängigkeit.

## Nicht ausführbarer Backlog / Non-Executable Backlog

`requirements/intakes/backlog/Lastenheft_Optional-NuGet-Package.md` besitzt
den Lifecycle `DeferredOptional`. Er wird von `next` nicht angeboten und
blockiert keinen aktiven Intake.

## Nächste Aktion / Next Action

Vor einem Feature-Start:

```text
$speckit-intake-series-status
$speckit-intake-series-next
```

Das Sandbox-Security-Hardening ist nur als nächster Intake freigegeben;
kein Specify-, Implementierungs- oder Remote-Schritt wird implizit autorisiert.
