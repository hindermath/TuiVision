# Assurance-Revalidierung / Assurance Revalidation

## Ergebnis und Grenze / Result and Boundary

**DE:** Die technische Revalidierung vom 2026-09-08 beseitigt die dokumentierte
Baseline-Versionsabweichung und vervollstaendigt den Assurance-Evidence-Vertrag.
Alle vier technischen Gates sowie der strengste Gesamtstatus sind Ready.
Dieses Ergebnis bewertet die Integritaet des Gate-Satzes, nicht die Wirksamkeit
aller fachlichen Kontrollen. Die 157 Kontrollen behalten ihre fachlichen Dispositionen: 13 AlreadySatisfied, 65 Applicable, 38 N/A, 36 Open und 5 FollowUp. Es wird keine
Risikoakzeptanz, Zertifizierung, Rechts-, C5-, Pilot-, Projekt- oder
Releasefreigabe abgeleitet.

**EN:** The technical revalidation dated 2026-09-08 resolves the recorded
baseline version drift and completes the Assurance evidence contract. All four
technical gates and the strictest overall status are Ready. This result
assesses gate-set integrity, not the effectiveness of every domain control.
The 157 controls retain their domain dispositions: 13 AlreadySatisfied, 65 Applicable, 38 N/A, 36 Open, and 5 FollowUp. No risk acceptance, certification, legal, C5,
pilot, project, or release approval is inferred.

## Aenderung / Change

- The managed secure-development baseline was synchronized from the Level-0
  source. The repository now binds baseline and guideline 3.2.0, compendium,
  CL-09, and CL-12 2.2.0, SDLC guideline 1.2.0, and integration map 1.4.0.
- The former blocked migration gate set is retained under
  archive/2026-09-07-assurance-migration/; it remains historical evidence of the earlier drift.
- Active gates assess the technical evidence-contract layer. The unchanged
  domain source is [rl-se-self-review.json](rl-se-self-review.json); its
  values are not promoted to fulfilled states.
## Gate-Nachweise / Gate Evidence

| Gate | Outcome | Evidence boundary |
|---|---|---|
| Baseline | Ready | Manifest version, all controlled document versions, and normalized SHA-256 bindings |
| Delta | Ready | Bounded baseline/evidence correction; no product or runtime change |
| Closure | Ready | Technical validation only; three human approval decisions stay Open |
| Image Impact | Ready | Documentation/evidence-only delta; no image, package, network, mount, or secret change |

The installed Bash and PowerShell validators are executed for every gate and
for overall status. assurance-validation.json records commands, exit codes,
gate hashes, parity, and the read-only preservation check.

## Documentation Impact

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

Keep pilotAuthorization, projectAcceptance, and generalRelease Open
until explicitly authorised human evidence exists. Re-run all gates whenever a
re-evaluation trigger occurs.
