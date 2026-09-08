# TuiVision GSDB Evidence-Matrix / Evidence Matrix

## Aktuelle Assurance-Revalidierung / Current Assurance Revalidation

**DE:** Am 2026-09-08 wurde die repository-lokale Secure-Development-Baseline
auf Version 3.2.0 synchronisiert und der technische Evidence-Vertrag des
13. Presets erneut geprüft. Baseline, Delta, Closure und Image Impact sind
Ready; der strengste technische Gesamtstatus ist Ready. Das ist keine
fachliche Neubewertung der 157 Kontrollpunkte. Alle 157 fachlichen Kontrollen bleiben Open; ebenso bleiben die menschlichen Entscheidungsgrenzen offen.
Pilotfreigabe, Projektabnahme und allgemeine Freigabe bleiben Open.

**EN:** On 2026-09-08, the repository-local secure-development baseline was
synchronized to version 3.2.0 and the thirteenth preset's technical evidence
contract was revalidated. Baseline, delta, closure, and image impact are
Ready; the strictest technical overall status is Ready. This is not a new
domain assessment of the 157 controls. All 157 domain controls remain Open, as do the human decision boundaries. Pilot
authorization, project acceptance, and general release remain Open.

Current machine-readable gates are baseline.json,
deltas/2026-09-08-assurance-revalidation.json, closure.json, and
image-impact.json. The superseded blocked migration gates remain under
archive/2026-09-07-assurance-migration/. See assurance-revalidation.md and
assurance-validation.json for scope and executable proof.

## Fachliche Matrix / Domain Matrix

| Scope | Canonical evidence | Current domain state |
|---|---|---|
| 157 controls | [control-assessment.md](control-assessment.md) and [control-projection.json](control-projection.json) | Open (157) |
| Sources | [source-inventory.md](source-inventory.md) and [source-projection.json](source-projection.json) | 37 point-in-time sources |
| Evidence families | [evidence-family-assessment.md](evidence-family-assessment.md) and [evidence-family-projection.json](evidence-family-projection.json) | Open (12) |
| Preset governance | [preset-governance-assessment.md](preset-governance-assessment.md) and [preset-governance-projection.json](preset-governance-projection.json) | historical twelve-preset review remains Open |
| Human boundaries | [human-boundaries.md](human-boundaries.md) | all human decisions remain Open |
| Validation summary | [validation-evidence.md](validation-evidence.md), [summary.md](summary.md), and [summary-projection.json](summary-projection.json) | point-in-time evidence, not approval |

## Lesereihenfolge / Reading Order

**DE:** Beginne mit dieser Matrix, lies danach den technischen Gate-Satz und
wechsle fuer einzelne fachliche Kontrollwerte in control-assessment.md oder
control-projection.json. Die Ready-Aussage gilt nur fuer Vollstaendigkeit,
Integritaet und Aktualitaet des Assurance-Evidence-Vertrags.

**EN:** Start with this matrix, then read the technical gate set, and use
control-assessment.md or control-projection.json for individual domain
control values. Ready applies only to completeness, integrity, and currency
of the Assurance evidence contract.
