# Assurance-Revalidierung / Assurance Revalidation

## Ergebnis und Grenze / Result and Boundary

**DE:** Die technische Revalidierung vom 2026-09-08 beseitigt die dokumentierte
Baseline-Versionsabweichung und vervollstaendigt den Assurance-Evidence-Vertrag.
Alle vier technischen Gates sowie der strengste Gesamtstatus sind Ready.
Dieses Ergebnis bewertet die Integritaet des Gate-Satzes, nicht die Wirksamkeit
aller fachlichen Kontrollen. Die 157 Kontrollen behalten ihre fachlichen Dispositionen: 13 AlreadySatisfied, 65 Applicable, 38 N/A, 36 Open und 5 FollowUp. Es wird keine
Risikoakzeptanz, Zertifizierung, Rechts-, C5-, Pilot-, Projekt- oder
Releasefreigabe abgeleitet. Fuer den aktuellen nichtkommerziellen Ausbildungs-
und Beispielscope sind C5, CRA und formale Produktkonformitaet `N/A`; die
historischen Kontrollwerte bleiben als Zeitpunktaufnahme unveraendert.

**EN:** The technical revalidation dated 2026-09-08 resolves the recorded
baseline version drift and completes the Assurance evidence contract. All four
technical gates and the strictest overall status are Ready. This result
assesses gate-set integrity, not the effectiveness of every domain control.
The 157 controls retain their domain dispositions: 13 AlreadySatisfied, 65 Applicable, 38 N/A, 36 Open, and 5 FollowUp. No risk acceptance, certification, legal, C5,
pilot, project, or release approval is inferred. C5, CRA, and formal product
conformity are `N/A` for the current non-commercial training and example scope;
the historical control values remain an unchanged point-in-time snapshot.

## Aenderung / Change

**DE:**

- Die verwaltete Secure-Development-Baseline wurde aus der Level-0-Quelle
  synchronisiert. Das Repository bindet jetzt Baseline und Richtlinie 3.2.0,
  Sammelband, CL-09 und CL-12 2.2.0, SDLC-Richtlinie 1.2.0 sowie die
  Verzahnungskarte 1.4.0.
- Der frühere blockierte Migrations-Gate-Satz bleibt unter
  archive/2026-09-07-assurance-migration/ als historische Evidence der
  damaligen Abweichung erhalten.
- Die aktiven Gates bewerten nur den technischen Evidence-Vertrag. Fachliche
  Kontrolldispositionen werden nicht zu erfüllten Zuständen hochgestuft.
- Die aktuelle Scope-Entscheidung behandelt GitHub, CI, NuGet und
  Artefakthosting als Entwicklungs- und Lieferinfrastruktur, nicht als
  Produkt-Cloud-Runtime. Eine regulatorische Scope-Pruefung ist fuer
  2026-12-31 vorgemerkt.

**EN:**

- The managed secure-development baseline was synchronized from the Level-0
  source. The repository now binds baseline and guideline 3.2.0, compendium,
  CL-09, and CL-12 2.2.0, SDLC guideline 1.2.0, and integration map 1.4.0.
- The former blocked migration gate set is retained under
  archive/2026-09-07-assurance-migration/; it remains historical evidence of the earlier drift.
- Active gates assess the technical evidence-contract layer. In the domain
  source [rl-se-self-review.json](rl-se-self-review.json), the earlier
  baseline-related SHA-256 bindings EVD-020/EVD-033 and the current field-test
  binding EVD-038 were refreshed; all 157 control dispositions and their review
  boundaries remain unchanged and are not promoted to fulfilled states.
- The current scope decision treats GitHub, CI, NuGet, and artifact hosting as
  development and delivery infrastructure rather than product cloud runtime.
  Regulatory scope is scheduled for re-evaluation on 2026-12-31.
## Gate-Nachweise / Gate Evidence

| Gate | Ergebnis / Outcome | Evidence-Grenze / Evidence boundary |
|---|---|---|
| Baseline | Ready | Manifestversion, alle gelenkten Dokumentversionen und normalisierte SHA-256-Bindungen / Manifest version, all controlled document versions, and normalized SHA-256 bindings |
| Delta | Ready | Begrenzte Baseline-/Evidence-Korrektur; keine Produkt- oder Runtime-Änderung / Bounded baseline/evidence correction; no product or runtime change |
| Closure | Ready | Nur technische Validierung; drei menschliche Freigabeentscheidungen bleiben Open / Technical validation only; three human approval decisions stay Open |
| Image Impact | Ready | Nur Dokumentations-/Evidence-Delta; keine Image-, Paket-, Netzwerk-, Mount- oder Secret-Änderung / Documentation/evidence-only delta; no image, package, network, mount, or secret change |

**DE:** Die installierten Bash- und PowerShell-Validatoren werden für jedes
Gate und den Gesamtstatus ausgeführt. assurance-validation.json erfasst Befehle,
Exitcodes, Gate-Hashes, Parität und die Read-only-Erhaltungsprüfung.

**EN:** The installed Bash and PowerShell validators are executed for every gate
and for overall status. assurance-validation.json records commands, exit codes,
gate hashes, parity, and the read-only preservation check.

## Dokumentationsauswirkung / Documentation Impact

**DE:** Entscheidung: UpdateRequired. Kanonische Quellen sind die
Secure-Development-Baseline aus Level 0 und die repository-lokale fachliche
Bewertungsquelle. Owner ist die TuiVision-Repository-Maintainerrolle. Betroffen
sind Baseline-Referenzen, aktive Gate-JSONs, Evidence-Matrix,
Revalidierungsbericht, Validierungsreceipt und Projektstatistik. Der Leserpfad
führt von der Matrix über die Gates zur fachlichen Quelle. Die Evidence ist
repository-lokal, DE-zuerst/EN-danach, textorientiert und benötigt keinen
Home-Sync. Nach jeder relevanten Quellen-, Scope-, Produkt- oder
Infrastrukturänderung, spätestens am 2027-09-08, ist neu zu bewerten.

**EN:**

- Decision: UpdateRequired.
- Canonical source: Level-0 secure-development baseline plus this repository's
  domain-assessment source with technically refreshed baseline bindings and
  unchanged control dispositions.
- Owner: TuiVision repository maintainer role.
- Affected documents: managed baseline references, active gate JSON files,
  evidence matrix, revalidation report, validation receipt, and project
  statistics.
- Reader paths: maintainer and security reviewer start at evidence-matrix.md,
  continue to the gate JSON files, then follow links to the domain source.
- Navigation and class: context-local governed evidence; no public landing-page
  or product API navigation changes.
- Language and accessibility: German first, English second; status is always
  expressed in text and does not rely on colour or visual-only cues.
- Platform evidence: macOS Bash and PowerShell validator parity locally;
  repository PR checks remain the delivery gate.
- Distribution and Home sync: repository-local Level-2 evidence;
  NoHomeSyncRequired.
- Re-evaluation: immediately after any baseline, evidence, product,
  architecture, dependency, workflow, distribution, image, or scope change,
  and no later than 2027-09-08.

## Naechste Aktion / Next Action

**DE:** pilotAuthorization, projectAcceptance und generalRelease bleiben
Open. Die Community-Einreichung `github/spec-kit#4455` und alle fuenf
Projektfeldtests werden vor einer zentralen v0.1.3-Preset-Entscheidung
abgewartet. Nach jedem Re-Evaluation-Trigger sind alle Gates erneut zu prüfen.

**EN:** Keep pilotAuthorization, projectAcceptance, and generalRelease Open.
Wait for community submission `github/spec-kit#4455` and all five project field
tests before a central v0.1.3 preset decision. Re-run all gates whenever a
re-evaluation trigger occurs.
