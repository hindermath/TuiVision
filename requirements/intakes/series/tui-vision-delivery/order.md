# Lastenheft-Abarbeitungsreihenfolge / Requirements Processing Order

Diese Datei ist die menschenlesbare Root-Ansicht der kanonischen Intake-Serie. Sie wird aus `requirements/intakes/series/tui-vision-delivery/manifest.json` abgeleitet und nicht unabhängig gepflegt.

*This file is the human-readable root view of the canonical intake series. It is derived from `requirements/intakes/series/tui-vision-delivery/manifest.json` and is not maintained independently.*

## Serienstatus / Series Status

Alle zehn Einträge sind `Completed`. Es gibt derzeit keinen `Eligible`-Intake und keinen implizit autorisierten nächsten Feature-Lauf.

*All ten entries are `Completed`. There is currently no `Eligible` intake and no implicitly authorized next feature run.*

## Zuletzt abgeschlossen / Most Recently Completed

1. `requirements/intakes/archive/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.046-gsdb-spec-kit-intensive-review.md`
   - geliefert durch Feature `046-gsdb-spec-kit-intensive-review` und PR `#164`
   - Status: `Completed`
   - unabhängige GSDB-Intensivprüfung ohne Produktänderung

## Weitere abgeschlossene Einträge / Other Completed Entries

2. `requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md`
3. `requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md`
4. `requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md`
5. `requirements/intakes/active/Lastenheft_Constitution_Change.md`
6. `requirements/intakes/active/Lastenheft_Source-Reference-Policy.md`
7. `requirements/intakes/active/Lastenheft_Transactional-Form-Model.md`
8. `requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md`
9. `requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md`
10. `requirements/intakes/archive/Lastenheft_RL-SE-Checklist-Selbstpruefung.045-rl-se-checklist-self-review.md`

Die übrigen neun Einträge bleiben mit unveränderten Hashes, Rollen, Wurzeln
und Abhängigkeiten im kanonischen Manifest dokumentiert.

*The other nine entries remain documented in the canonical manifest with
unchanged hashes, roles, roots, and dependencies.*

## Nicht ausführbarer Backlog / Non-Executable Backlog

`requirements/intakes/backlog/Lastenheft_Optional-NuGet-Package.md` besitzt den Lifecycle `DeferredOptional`. Er wird von `next` nicht angeboten und blockiert keinen aktiven Intake.

## Nächste Aktion / Next Action

`$speckit-intake-series-status` und `$speckit-intake-series-next` dürfen den Zustand ausschließlich read-only prüfen. Es wird kein neues Feature gestartet.
