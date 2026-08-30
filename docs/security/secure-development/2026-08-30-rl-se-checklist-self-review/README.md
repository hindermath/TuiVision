# RL-SE- und Checklist-Selbstpruefung / RL-SE and Checklist Self-Review

## Deutsch

Status: **Validierungskandidat (`Passed`)**. Dies ist eine repository-lokale,
nicht zertifizierende Selbstpruefung. Sie ist keine formale Freigabe, kein
QISMS-Nachweis und keine pauschale Aussage zur Rechts- oder
Compliance-Konformitaet. Die einzige fachliche Datenquelle ist
`rl-se-self-review.json`; die Markdown-Dateien sind text-first Projektionen.

### Leserpfad

1. [Kontrollentscheidungen](control-assessment.md) enthalten alle 157
   Kontroll-IDs mit Status, Evidence oder Luecke, Owner, Risiko, Follow-up und
   Trigger.
2. [Preset-Bewertung](preset-assessment.md) dokumentiert die zwoelf installierten
   Governance-Presets.
3. [Governance-Beobachtungen](governance-observations.md) zeigen unreparierten
   Drift mit `repairPerformed=false`.
4. [Menschliche Grenzen](human-boundaries.md) trennen Recht, Organisation,
   Provider, Secrets, Plattformen und Freigaben mit `agentMayClose=false`.
5. [Validierungsnachweise](validation-evidence.md) dokumentieren Tests und Gates.

Status wird immer ausgeschrieben; Farbe, Position, Bild oder Zeigergeste tragen
keine alleinige Bedeutung. Tabellen besitzen semantische Ueberschriften und
bleiben in Braillezeile, Screenreader, Tastaturnavigation und Textbrowser
linear lesbar.

### Begriffe

- **MSL**: Minimum Security Level, das kleinste festgelegte Sicherheitsniveau.
- **SSDF**: Secure Software Development Framework des NIST.
- **CWE**: Common Weakness Enumeration, ein Katalog von Schwachstellenarten.
- **ASVS**: Application Security Verification Standard der OWASP.
- **SBOM**: Software Bill of Materials, eine Komponentenliste fuer Software.
- **VEX**: Vulnerability Exploitability eXchange, eine begruendete Aussage zur
  Ausnutzbarkeit bekannter Schwachstellen.
- **SLSA**: Supply-chain Levels for Software Artifacts, Reifegrade fuer
  Lieferkettenintegritaet.
- **SAMM**: Software Assurance Maturity Model der OWASP.
- **CAPEC**: Common Attack Pattern Enumeration and Classification.
- **Zero Trust**: Zugriffe werden nicht allein wegen Standort oder Netzgrenze
  vertraut, sondern fortlaufend und minimal berechtigt geprueft.
- **BSI C3A/C5**: BSI-Kriterien fuer Cybersicherheit beziehungsweise
  Cloud-Dienste; dieser Audit erteilt keine BSI-Bestaetigung.
- **Spec-Kit**: Repository-Workflow aus Intake, Spezifikation, Plan, Aufgaben,
  Evidence und kontrollierten Phasen. Ein Checkpoint bindet Fortschritt, er
  erweitert keine Autoritaet.

## English

Status: **validation candidate (`Passed`)**. This is a repository-local,
non-certifying self-review. It is not formal approval, QISMS evidence, or a
blanket legal or compliance statement. The sole domain data source is
`rl-se-self-review.json`; the Markdown files are text-first projections.

### Reader path

1. [Control decisions](control-assessment.md) contain all 157 control IDs with
   status, evidence or gap, owner, risk, follow-up, and trigger.
2. [Preset assessment](preset-assessment.md) records all twelve installed
   governance presets.
3. [Governance observations](governance-observations.md) expose unrepaired drift
   with `repairPerformed=false`.
4. [Human boundaries](human-boundaries.md) separate law, organisation,
   providers, secrets, platforms, and approvals with `agentMayClose=false`.
5. [Validation evidence](validation-evidence.md) records tests and gates.

Status is always written out; colour, position, images, or pointer gestures do
not carry unique meaning. Tables have semantic headings and remain linear for
Braille displays, screen readers, keyboard navigation, and text browsers.

### Terms

- **MSL**: Minimum Security Level, the smallest defined security level.
- **SSDF**: NIST Secure Software Development Framework.
- **CWE**: Common Weakness Enumeration, a catalogue of weakness types.
- **ASVS**: OWASP Application Security Verification Standard.
- **SBOM**: Software Bill of Materials, a software component inventory.
- **VEX**: Vulnerability Exploitability eXchange, a reasoned statement about
  exploitability of known vulnerabilities.
- **SLSA**: Supply-chain Levels for Software Artifacts, maturity levels for
  supply-chain integrity.
- **SAMM**: OWASP Software Assurance Maturity Model.
- **CAPEC**: Common Attack Pattern Enumeration and Classification.
- **Zero Trust**: Access is not trusted merely because of location or a network
  boundary; it is checked continuously with least privilege.
- **BSI C3A/C5**: German BSI criteria for cyber security and cloud services;
  this audit grants no BSI attestation.
- **Spec-Kit**: A repository workflow of intake, specification, plan, tasks,
  evidence, and controlled phases. A checkpoint binds progress and grants no
  additional authority.
