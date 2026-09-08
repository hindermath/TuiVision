# Assurance-Revalidierung / Assurance Revalidation

## Ergebnis und Grenze / Result and Boundary

**DE:** Die technische Revalidierung vom 2026-09-08 beseitigt die dokumentierte
Baseline-Versionsabweichung und vervollstaendigt den Assurance-Evidence-Vertrag.
Alle vier technischen Gates sowie der strengste Gesamtstatus sind Ready.
Dieses Ergebnis bewertet die Integritaet des Gate-Satzes, nicht die Wirksamkeit
aller fachlichen Kontrollen. Alle 157 fachlichen Kontrollen bleiben Open; ebenso bleiben die menschlichen Entscheidungsgrenzen offen. Es wird keine
Risikoakzeptanz, Zertifizierung, Rechts-, C5-, Pilot-, Projekt- oder
Releasefreigabe abgeleitet.

**EN:** The technical revalidation dated 2026-09-08 resolves the recorded
baseline version drift and completes the Assurance evidence contract. All four
technical gates and the strictest overall status are Ready. This result
assesses gate-set integrity, not the effectiveness of every domain control.
All 157 domain controls remain Open, as do the human decision boundaries. No risk acceptance, certification, legal, C5,
pilot, project, or release approval is inferred.

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

**EN:**

- The managed secure-development baseline was synchronized from the Level-0
  source. The repository now binds baseline and guideline 3.2.0, compendium,
  CL-09, and CL-12 2.2.0, SDLC guideline 1.2.0, and integration map 1.4.0.
- The former blocked migration gate set is retained under
  archive/2026-09-07-assurance-migration/; it remains historical evidence of the earlier drift.
- Active gates assess the technical evidence-contract layer. The unchanged
  domain source is [gsdb-spec-kit-intensive-review.json](gsdb-spec-kit-intensive-review.json); its
  values are not promoted to fulfilled states.
- TuiVision's GSDB context additionally receives the previously missing
  evidence-matrix.md and all four machine-readable gate files.

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
Infrastrukturänderung, spätestens am 2026-09-15, ist neu zu bewerten.

**EN:**

- Decision: UpdateRequired.
- Canonical source: Level-0 secure-development baseline plus this repository's
  unchanged domain-assessment source.
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
  and no later than 2026-09-15.

## Naechste Aktion / Next Action

**DE:** pilotAuthorization, projectAcceptance und generalRelease bleiben
Open, bis ausdrücklich befugte menschliche Evidence vorliegt. Nach jedem
Re-Evaluation-Trigger sind alle Gates erneut zu prüfen.

**EN:** Keep pilotAuthorization, projectAcceptance, and generalRelease Open
until explicitly authorised human evidence exists. Re-run all gates whenever a
re-evaluation trigger occurs.
