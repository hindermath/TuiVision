# Lastenheft-Abarbeitungsreihenfolge / Requirements Processing Order

Diese Ansicht wird deterministisch aus dem kanonischen Manifest [manifest.json](requirements/intakes/series/tui-vision-delivery/manifest.json)
und dem T056-gesperrten Nachweis erzeugt. Sie startet keinen Feature-Lauf.

*This view is rendered deterministically from the canonical manifest and the
T056-locked evidence. It does not start a feature run.*

<!-- linked-intake-evidence:begin -->
## Verlinkter Ausführungsnachweis / Linked Execution Evidence

- Manifest SHA-256: `6c086db5c8ed9cd33e221210e68edd99546efb9acbe098d863fb18201ffb5ae9`
- Projektions-SHA-256: `ee742b9d641546faf154e1aa27a98b14029ada3d120892d4590449aaab65efb7`
- Umfang: `10` Serienzuordnungen, `6` unveränderte Abhängigkeiten

| Position | Intake | Status | Spec-Kit-Feature | Direkte eingehende Abhängigkeiten |
|---:|---|---|---|---|
| 1 | [Lastenheft_22_Wave6-Combined-Delta-Closure.md](requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md) | `Completed` | [037-wave6-combined-delta-closure](specs/037-wave6-combined-delta-closure/) | — |
| 2 | [Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md](requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md) | `Completed` | [038-example-portfolio-conformance-audit](specs/038-example-portfolio-conformance-audit/) | [Lastenheft_22_Wave6-Combined-Delta-Closure.md](requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md) · `HardCompletionGate` · binding=`true` |
| 3 | [Lastenheft_Example-Portfolio-Closure.md](requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md) | `Completed` | [039-example-portfolio-closure](specs/039-example-portfolio-closure/) | [Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md](requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md) · `HardCompletionGate` · binding=`true` |
| 4 | [Lastenheft_Constitution_Change.md](requirements/intakes/active/Lastenheft_Constitution_Change.md) | `Completed` | [040-constitution-governance-closure](specs/040-constitution-governance-closure/) | — |
| 5 | [Lastenheft_Source-Reference-Policy.md](requirements/intakes/active/Lastenheft_Source-Reference-Policy.md) | `Completed` | [041-source-reference-policy](specs/041-source-reference-policy/) | [Lastenheft_Constitution_Change.md](requirements/intakes/active/Lastenheft_Constitution_Change.md) · `SharedWriterSerialization` · binding=`false` |
| 6 | [Lastenheft_Transactional-Form-Model.md](requirements/intakes/active/Lastenheft_Transactional-Form-Model.md) | `Completed` | [042-transactional-form-model](specs/042-transactional-form-model/) | [Lastenheft_Source-Reference-Policy.md](requirements/intakes/active/Lastenheft_Source-Reference-Policy.md) · `HardCompletionGate` · binding=`true`<br>[Lastenheft_Example-Portfolio-Closure.md](requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md) · `HardCompletionGate` · binding=`true` |
| 7 | [Lastenheft_23_Documentation-Publishing-Closure.md](requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md) | `Completed` | [043-documentation-publishing-closure](specs/043-documentation-publishing-closure/) | [Lastenheft_Transactional-Form-Model.md](requirements/intakes/active/Lastenheft_Transactional-Form-Model.md) · `PreferredSerialOrder` · binding=`false` |
| 8 | [Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md](requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md) | `Completed` | [044-sandbox-secure-development-hardening](specs/044-sandbox-secure-development-hardening/) | — |
| 9 | [Lastenheft_RL-SE-Checklist-Selbstpruefung.045-rl-se-checklist-self-review.md](requirements/intakes/archive/Lastenheft_RL-SE-Checklist-Selbstpruefung.045-rl-se-checklist-self-review.md) | `Completed` | [045-rl-se-checklist-self-review](specs/045-rl-se-checklist-self-review/) | — |
| 10 | [Lastenheft_GSDB-Spec-Kit-Intensivpruefung.046-gsdb-spec-kit-intensive-review.md](requirements/intakes/archive/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.046-gsdb-spec-kit-intensive-review.md) | `Completed` | [046-gsdb-spec-kit-intensive-review](specs/046-gsdb-spec-kit-intensive-review/) | — |

### Zuletzt abgeschlossen / Latest Completion

Latest completion: [046-gsdb-spec-kit-intensive-review](specs/046-gsdb-spec-kit-intensive-review/) at position `10` (`Completed`).

Feature 046 bleibt an seiner kanonischen Position. Die Aktualitätsaussage
ändert die Manifestreihenfolge nicht.

*Feature 046 remains at its canonical position. Recency does not reorder the
manifest.*

### Getrennter Backlog / Separated Backlog

[Lastenheft_Optional-NuGet-Package.md](requirements/intakes/backlog/Lastenheft_Optional-NuGet-Package.md) — lifecycle `DeferredOptional`; active=`false`.

Der optionale Eintrag ist weder aktive Tabellenzeile noch Abhängigkeitsendpunkt.

*The optional item is neither an active row nor a dependency endpoint.*
<!-- linked-intake-evidence:end -->

## Nächste Aktion / Next Action

`$speckit-intake-series-status` und `$speckit-intake-series-next` prüfen den
Zustand ausschließlich read-only. Es gibt keinen implizit autorisierten
nächsten Feature-Lauf.
