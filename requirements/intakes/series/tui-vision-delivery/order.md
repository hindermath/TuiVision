# Lastenheft-Abarbeitungsreihenfolge / Requirements Processing Order

Diese Datei ist die menschenlesbare Root-Ansicht der kanonischen
Intake-Serie. Sie wird aus
`requirements/intakes/series/tui-vision-delivery/manifest.json` erzeugt und
nicht unabhängig bearbeitet.

*This file is the human-readable root view of the canonical intake series. It
is generated from the series manifest and is not maintained independently.*

## Bevorzugter nächster Intake / Preferred Next Intake

1. `requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md`
   - reserviert noch kein Feature; Feature `038` wird nicht automatisch angelegt
   - Status: `Eligible`
   - startet nicht automatisch

## Abgeschlossene harte Voraussetzung / Completed Hard Prerequisite

2. `requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md`
   - geliefert durch Feature `037-wave6-combined-delta-closure` und PR `#139`
   - Status: `Completed`
   - erfüllt die harte Voraussetzung des Portfolioaudits

## Weitere unabhängige aktive Intakes / Other Independent Active Intakes

3. `requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md`
4. `requirements/intakes/active/Lastenheft_Constitution_Change.md`
5. `requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md`
6. `requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md`
7. `requirements/intakes/active/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md`

Diese Intakes sind unabhängige Wurzeln. Ihre Reihenfolge ist eine bevorzugte
Serialisierung gemeinsamer Governance- und Dokumentationsoberflächen, keine
erfundene fachliche Abhängigkeit.

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

Der Portfolioaudit wird erst durch einen ausdrücklichen autonomen oder
manuellen Spec-Kit-Auftrag gestartet. `.specify/feature.json` bleibt bis dahin
auf dem abgeschlossenen Feature 037; Feature 038 wird nicht implizit erzeugt.
