# Evidence-Qualitaetsaudit 044-046 / Evidence Quality Audit 044-046

## Ergebnis / Result

Der Audit bewertet 12 Feature-044-Kontrollen und 157 Kontrollzeilen aus 045/046. Er erfasst 10 deduplizierte Findings: 2 High, 5 Medium und 3 Low.

The audit assesses 12 Feature 044 controls and 157 control rows from 045/046. It records 10 deduplicated findings: 2 High, 5 Medium, and 3 Low.

## Provenienz / Provenance

Von 988 Referenzen sind 916 am behaupteten Commit hashgleich, 66 fehlen dort und 6 weichen im Hash ab. Die semantische Stichprobe umfasst 50 verifizierbare Referenzen.

Of 988 references, 916 match their claimed commit and hash, 66 are missing there, and 6 have a different hash. The semantic sample covers 50 verifiable references.

## Findings

| ID | Severity | Category | Status | Deutsch | English |
|---|---|---|---|---|---|
| `EQA001` | High | EvidenceDefect | Open | Feature 046 verwendet fuer alle 157 Kontrollen dieselbe generische Open-Bewertung ohne Einzel-Evidence. | Feature 046 uses the same generic Open assessment for all 157 controls without item-level evidence. |
| `EQA002` | Medium | EvidenceDefect | Open | Konkrete technische Hinweise erscheinen nicht im kanonischen Observation-Register von Feature 046. | Concrete technical observations do not appear in Feature 046's canonical observation register. |
| `EQA003` | Medium | EvidenceDefect | Open | Feature 045 uebernimmt alle 157 Statuswerte aus Feature 016 mit tautologischer Aenderungserklaerung. | Feature 045 retains all 157 Feature 016 statuses with a tautological change explanation. |
| `EQA004` | Medium | EvidenceDefect | Open | Der manuelle Feature-046-Inhaltsreview weist null Sekunden Dauer aus. | The Feature 046 manual content review records an elapsed duration of zero seconds. |
| `EQA005` | Low | TechnicalFinding | OpenBoundary | Das Example-Smoke-Testprojekt besitzt keinen Coverlet-Collector. | The example smoke-test project has no Coverlet collector. |
| `EQA006` | Low | TechnicalFinding | OpenBoundary | MSTest 4.3.2 blieb trotz dokumentierter 4.3.3-Verfuegbarkeit unveraendert. | MSTest 4.3.2 remained unchanged despite documented 4.3.3 availability. |
| `EQA007` | Medium | GovernanceFinding | Open | Eine GitHub-Action verwendet weiterhin eine veraenderliche Referenz. | One GitHub Action still uses a mutable reference. |
| `EQA008` | Low | Informational | OpenBoundary | Node 26.7 lag ausserhalb der deklarierten LTS-Engine, ohne dass ein Produktfehler bewiesen wurde. | Node 26.7 was outside the declared LTS engine without proving a product defect. |
| `EQA009` | Medium | GovernanceFinding | Open | Die drei Haupt-PRs besitzen keine eingereichte unabhaengige fachliche Review. | The three primary pull requests have no submitted independent substantive review. |
| `EQA010` | High | EvidenceDefect | Open | 72 von 988 Feature-046-Referenzen sind am behaupteten Commit nicht hashgleich verifizierbar. | Seventy-two of 988 Feature 046 references cannot be hash-verified at the claimed commit. |

## Grenze / Boundary

Feature 047 behebt keinen Befund und erzeugt keinen Folge-Intake. Die alten Abschlusszustaende bleiben unveraendert.

Feature 047 remediates no finding and creates no follow-up intake. Earlier completion states remain unchanged.
