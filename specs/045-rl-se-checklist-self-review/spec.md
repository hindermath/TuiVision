# Feature Specification: RL-SE-/Checklist-Selbstpruefung

**Feature Branch**: `045-rl-se-checklist-self-review`
**Created**: 2026-08-30
**Status**: Draft - Specify quality gates passed
**Binding Input**: `requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md`
**Accepted Review**: Series review `5e9620e8-9c49-44f9-84a3-fd3aa659facc` (`Ready`)
**Delivery Boundary**: Audit documentation only; no automatic hardening or remote action

## Zweck / Purpose

Feature 045 fuehrt eine vollstaendige, nachvollziehbare Selbstpruefung von
TuiVision gegen die Richtlinie Sichere Entwicklung (RL-SE), alle 157 stabilen
Kontrollen aus `CL-01` bis `CL-12`, die mitgeltenden Dokumente, beide lokalen
Constitution-Oberflaechen und alle zwoelf aktivierten Governance-Presets durch.
Das Ergebnis zeigt ehrlich, was gilt, was aktuell belegt ist und wo eine
Entscheidung oder spaetere Arbeit fehlt. Es ist keine formale Zertifizierung
und keine pauschale Sicherheitsfreigabe.

Feature 045 performs a complete, traceable TuiVision self-review against the
Secure Development Guideline, all 157 stable controls from `CL-01` through
`CL-12`, the related documents, both local constitution surfaces, and all
twelve enabled governance presets. The result states honestly what applies,
what current evidence proves, and where a decision or later work is missing.
It is neither a formal certification nor a blanket security approval.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Alle 157 Kontrollen lueckenlos pruefen (Priority: P1)

Security-Reviewer und Maintainer koennen jede kanonische `CL-XX-NN`-Kontrolle
genau einer aktuellen TuiVision-Entscheidung zuordnen, ohne fehlende oder
doppelte Zeilen suchen zu muessen.

*Security reviewers and maintainers can map each canonical `CL-XX-NN` control
to exactly one current TuiVision decision without searching for missing or
duplicate rows.*

**Why this priority**: Vollstaendige Kontrollabdeckung ist die Grundlage jeder
belastbaren Aussage. Eine Teilmenge wuerde Luecken unsichtbar machen.

**Independent Test**: Ein mechanischer Vergleich meldet 157 eindeutige
Quell-IDs, 157 eindeutige Ergebniszeilen, null fehlende, null doppelte und null
unbekannte IDs sowie die erwarteten Kapitelzahlen.

**Acceptance Scenarios**:

1. **Given** die zwoelf kanonischen Einzelchecklisten, **When** die Kontroll-IDs mit der Ergebnis-Matrix verglichen werden, **Then** ist jede der 157 IDs genau einmal vorhanden.
2. **Given** eine Ergebniszeile, **When** ihre Pflichtfelder geprueft werden, **Then** sind Status, Begruendung, Evidenz, Owner, Follow-up, Prioritaet, Restrisiko und Re-Evaluation-Trigger konkret und nicht leer.
3. **Given** eine positive Aussage, **When** ein Reviewer den Nachweis verfolgt, **Then** fuehrt sie zu aktueller, direkt stuetzender Repository-Evidenz und nicht nur zu einer Vorlage oder alten Behauptung.

---

### User Story 2 - Governance-Drift als Beobachtung sichtbar machen (Priority: P1)

Maintainer koennen Unterschiede zwischen Richtlinie, Manifest, Checklisten,
Constitutions, Preset-Registry und bestehender Security-Evidenz als
nachpruefbare Beobachtungen sehen, ohne dass der Prueflauf diese Unterschiede
still korrigiert.

*Maintainers can see differences among the guideline, manifest, checklists,
constitutions, preset registry, and existing security evidence as verifiable
observations without the review silently correcting them.*

**Why this priority**: Governance-Drift beeinflusst, welche Fassung und welche
Pflichten ein Audit bewertet. Eine stille Reparatur wuerde Scope und Freigabe
vermischen.

**Independent Test**: Jede festgestellte Abweichung nennt beide verglichenen
Quellen, den beobachteten Unterschied, moegliche Auswirkung, Owner,
Prioritaet, Restrisiko, naechste pruefbare Aktion und Trigger; der Diff enthaelt
keine automatische Korrektur dieser Quellen.

**Acceptance Scenarios**:

1. **Given** widerspruechliche Versions- oder Preset-Angaben, **When** der Review abgeschlossen wird, **Then** bleibt die Abweichung als Finding sichtbar und wird nicht als still erledigt dargestellt.
2. **Given** eine historische positive Aussage aus Feature 016, **When** sie gegen den aktuellen Stand geprueft wird, **Then** wird sie nur bei weiterhin direkter Evidenz `AlreadySatisfied`.
3. **Given** eine Abweichung ausserhalb der Audit-Autoritaet, **When** eine Folgeaktion noetig ist, **Then** wird sie als `Open` oder `FollowUp` beschrieben, ohne ein neues Feature oder Intake anzulegen.

---

### User Story 3 - Menschliche und agentische Grenzen ehrlich trennen (Priority: P1)

Reviewer koennen technische Repository-Evidenz von Entscheidungen trennen,
die Recht, Organisation, Provider, Secrets, reale Plattformen oder formale
Freigaben betreffen.

*Reviewers can distinguish technical repository evidence from decisions about
law, organisation, providers, secrets, real platforms, or formal approvals.*

**Why this priority**: Ein Agent darf fehlende menschliche Autoritaet oder
externe Evidenz nicht durch plausible Formulierungen ersetzen.

**Independent Test**: Jeder Human-only- oder External-only-Punkt ist `Open`
oder begruendet `N/A`/`FollowUp`, nennt eine verantwortliche menschliche Rolle
und behauptet weder Ausfuehrung noch Freigabe.

**Acceptance Scenarios**:

1. **Given** ein rechtlicher, organisatorischer oder Provider-Pruefpunkt ohne aktuelle Freigabe, **When** er klassifiziert wird, **Then** bleibt er sichtbar offen und besitzt eine sichere Folgeaktion.
2. **Given** Sandbox-, Agenten-, Netzwerk-, Mount-, Secret- oder Toolchain-Evidence, **When** sie bewertet wird, **Then** bleiben statische Konfiguration, praktische Ausfuehrung, Plattformnachweis und Human-Freigabe getrennte Proof-Grenzen.
3. **Given** Entwicklungs-KI ohne Runtime-/Produkt-KI, **When** AI-SBOM geprueft wird, **Then** wird die Entscheidung mit konkreter Systemgrenzen-Evidenz und Re-Evaluation-Trigger begruendet.

---

### User Story 4 - Audit-Ergebnis inklusiv verstehen (Priority: P2)

Auszubildende ab dem ersten Lehrjahr, Maintainer und Auditoren koennen Status,
Risiko, Evidenz und Folgeaktion ohne farb- oder layoutabhaengige Bedeutung
lesen und die wichtigsten Fachbegriffe verstehen.

*First-year apprentices, maintainers, and auditors can understand status,
risk, evidence, and follow-up without colour- or layout-only meaning and can
understand the main specialist terms.*

**Why this priority**: Das Ergebnis ist nur nutzbar, wenn unterschiedliche
Lesergruppen es selbststaendig nachvollziehen koennen.

**Independent Test**: Eine text-first Pruefung bestaetigt semantische
Ueberschriften und Tabellen, beschreibende Links, DE-first/EN-second Inhalt,
CEFR-B2-Niveau sowie kurze Erklaerungen fuer MSL, SBOM, VEX, SLSA, ASVS,
CAPEC, C3A/C5 und weitere erstmals verwendete Fachbegriffe.

**Acceptance Scenarios**:

1. **Given** ein Status oder Risiko, **When** die Darstellung ohne Farbe gelesen wird, **Then** bleibt die vollstaendige Bedeutung erhalten.
2. **Given** ein Fachbegriff bei erster Verwendung, **When** ein neuer Lernender ihn liest, **Then** findet er eine kurze Erklaerung oder einen beschreibenden Link zur Lernfassung.
3. **Given** ein Bild, Diagramm oder Scan-Auszug als Zusatznachweis, **When** assistive Technik genutzt wird, **Then** steht eine gleichwertige Textbeschreibung bereit.

### Edge Cases

- Die Quellmenge enthaelt weniger oder mehr als 157 eindeutige IDs oder eine
  Kontroll-ID ist doppelt: Der Review stoppt mit einem Baseline-Finding und
  behauptet keine vollstaendige Abdeckung.
- Eine Quellversion widerspricht Manifest, Sammelband oder Constitution: Beide
  Angaben bleiben mit Pfad und Auswirkung sichtbar; keine wird still zur
  Wahrheit erklaert.
- Ein Evidenzpfad existiert, belegt aber die Aussage nicht direkt oder ist nur
  eine leere Vorlage: Die Zeile darf nicht `AlreadySatisfied` sein.
- Ein Evidence-Link zeigt auf private absolute Pfade, Tokens, Sitzungsdaten
  oder nicht reproduzierbare lokale Zustaende: Solche Inhalte werden nicht
  uebernommen; die Beweisluecke bleibt `Open`.
- Ein Punkt gilt fuer TuiVision, darf aber in diesem Audit nicht geaendert
  werden: Er wird `Applicable`, `Open` oder `FollowUp`, nicht `N/A`.
- Ein Punkt wurde in Feature 016 oder 044 bereits bewertet: Das alte Ergebnis
  ist Eingangsevidenz, aber kein automatischer Nachweis fuer den aktuellen
  Stand.
- Ein Control braucht keine weitere Aktion: Das Pflichtfeld `Follow-up` nennt
  ausdruecklich `None` mit statusbezogener Begruendung; es bleibt nie leer.
- Ein Preset ist installiert, aber fuer einen Teilaspekt dieses seriellen
  Auditlaufs nicht anwendbar: Der Teilaspekt erhaelt ein begruendetes `N/A` mit
  Owner, Restrisiko und Trigger statt stiller Auslassung.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Der Lauf MUSS die akzeptierte Intake-Datei und den
  hashgleichen `Ready`-Review als bindende Herkunft dokumentieren. Branch,
  Feature-Verzeichnis und autonomer Laufzustand duerfen durch die
  Auditbearbeitung nicht manuell umgeschrieben werden.
- **FR-002**: Die zentrale Verzahnungsdatei MUSS als erste fachliche
  Lesefuehrung verwendet werden. Danach MUESSEN Richtlinie, Baseline-Manifest,
  Sammelband, alle zwoelf Einzelchecklisten, alle mitgeltenden Dokumente,
  Lernpfad, bestehende Security-/Architecture-/A11Y-Evidenz, CI, Tests,
  Spec-Kit-Artefakte und Review-Notizen einbezogen werden.
- **FR-003**: Die Quellmenge MUSS exakt alle 157 eindeutigen Kontrollen aus
  `CL-01` bis `CL-12` abdecken. Fehlende, doppelte oder unbekannte IDs
  blockieren die Vollstaendigkeitsaussage.
- **FR-004**: Jede Kontrolle MUSS genau einen und nur einen Status aus
  `Applicable`, `AlreadySatisfied`, `N/A`, `Open` oder `FollowUp` erhalten.
  Andere Statuswerte, kombinierte Werte oder stille Auslassungen sind verboten.
- **FR-005**: Begriffe der zweiachsigen generischen Baseline wie `Fulfilled`
  oder `Not Assessed` duerfen als Quellkontext erlaeutert werden, duerfen aber
  die fuenf verbindlichen Ergebnisstatus weder ersetzen noch erweitern.
- **FR-006**: Jede der 157 Zeilen MUSS Kontroll-ID, Quellpfad, Titel, Status,
  Begruendung (`Rationale`), konkrete Evidenz (`Evidence`) oder explizite
  Beweisluecke, Owner, Follow-up, Prioritaet (`Priority`), Restrisiko
  (`Residual Risk`) und Re-Evaluation-Trigger enthalten.
  Alle Felder muessen nicht leer und fuer den gewaehlten Status plausibel sein.
- **FR-007**: Die Prioritaet MUSS genau `Critical`, `High`, `Medium`, `Low`
  oder `None` verwenden. `None` ist nur mit einer ausdruecklichen Begruendung
  zulaessig und bedeutet nicht, dass ein Pflichtfeld ausgelassen wurde.
- **FR-008**: `AlreadySatisfied` MUSS aktuelle, direkte und in diesem
  Repository nachvollziehbare Evidenz besitzen. Vorlage, Policy, vergangene
  Feature-Behauptung, nicht reproduzierbarer lokaler Zustand oder blosser
  Existenznachweis genuegt nicht.
- **FR-009**: `Applicable` MUSS zeigen, warum der Punkt fuer TuiVision gilt,
  welche Evidenz oder Entscheidung der Audit erwartet und welches Restrisiko
  bis zur Bewertung verbleibt.
- **FR-010**: `N/A` MUSS eine technische oder fachliche Nichtanwendbarkeit,
  einen Owner, ein Restrisiko und einen konkreten Trigger fuer erneute Pruefung
  nennen. Mangelnde Zeit, fehlende Evidenz oder fehlende Autoritaet sind keine
  Nichtanwendbarkeit.
- **FR-011**: `Open` MUSS Owner, konkrete Folgeaktion, Prioritaet,
  Restrisiko und Re-Evaluation-Trigger nennen. Human-only- und
  External-only-Grenzen muessen sichtbar sein.
- **FR-012**: `FollowUp` MUSS begruenden, warum der relevante Punkt ausserhalb
  dieses Auditlaufs liegt, und eine beschreibende spaetere Arbeitsgrenze
  nennen. Der Lauf darf daraus weder Intake noch Branch noch Feature erzeugen.
- **FR-013**: Der Lauf MUSS den bestehenden 157-Control-Nachweis aus Feature
  016 und die Sandbox-Evidenz aus Feature 044 als Ausgangsevidenz neu gegen
  den aktuellen Stand pruefen; bestehende Statuswerte duerfen nicht
  ungeprueft kopiert werden.
- **FR-014**: Aktuelle Governance-Diskrepanzen MUESSEN als auditierbare
  Beobachtungen mit verglichenen Quellen, Auswirkung, Owner, Prioritaet,
  Restrisiko, Folgeaktion und Trigger erfasst werden. Sie duerfen keine stille
  Scope-Erweiterung oder automatische Reparatur ausloesen.
- **FR-015**: Der Audit MUSS mindestens die bereits sichtbaren
  Versionsunterschiede zwischen `baseline-manifest.json`, Richtlinie,
  Sammelband und Einzelchecklisten pruefen; die abweichenden Versionen der
  beiden Constitution-Dateien pruefen; und die Preset-Zahl/-Fassung in der
  Verzahnungsdatei gegen die aktuelle Registry mit zwoelf aktivierten Presets
  pruefen.
- **FR-016**: Die Primaersprache MUSS als C#/.NET und damit als Sprache auf der
  Constitution-MSL-Erlaubnisliste bewertet werden. Diese MSL-Entscheidung darf
  Eingabevalidierung, sichere APIs, Fehlerbehandlung, Serialisierung,
  Datei-/Prozess-/Terminalgrenzen, Dependencies oder Supply Chain nicht von
  der Pruefung ausnehmen.
- **FR-017**: NIST SSDF und CWE Top 25 MUESSEN fuer den Level-2-Review immer
  behandelt werden. ASVS, SBOM, VEX, AI-SBOM, SLSA, SAMM, CAPEC, Zero Trust
  und OpenSSF Scorecard MUESSEN jeweils eine evidenzbasierte Entscheidung aus
  dem fuenfteiligen Statusmodell erhalten.
- **FR-018**: Supply-Chain- und Release-Punkte MUESSEN Dependencies,
  unveraenderliche Workflow-Referenzen, Lock-/Restore-Reproduzierbarkeit,
  SBOM, bekannte Schwachstellen/VEX, Herkunft/Provenance/SLSA,
  Offenlegungsweg und relevante Scorecard-Evidence abdecken, ohne einen
  Release oder externe Abfrage vorzutäuschen.
- **FR-019**: Regulatorische Punkte MUESSEN CRA, NIS2, DORA, EU AI Act sowie
  BSI C3A und C5 einzeln behandeln. Rechtliche, organisatorische, kommerzielle
  oder Provider-Entscheidungen ohne befugte menschliche Evidenz bleiben
  sichtbar `Open`, `FollowUp` oder begruendet `N/A`.
- **FR-020**: Architekturpunkte MUESSEN Trust Boundaries, STRIDE/CIA,
  relevante CAPEC-Muster, Least Privilege, sichere Defaults, Daten-/Datei-/UI-
  /CLI-/Prozessgrenzen, Deployment, technische Schulden und vorhandene
  Security-/Architecture-Risiken pruefen. Dieser Audit aendert keine
  Architektur und erzeugt keine ADR-Reparatur.
- **FR-021**: Sandbox- und agentische Grenzen MUESSEN Mounts, Schreibrechte,
  Hostdaten, Agentenzustand, Secrets, Netzwerk, Toolchain, Prompt-/Log-
  Redaction, statische Evidence, praktische Ausfuehrung, Plattformnachweis und
  menschliche Freigabe getrennt bewerten.
- **FR-022**: Agenten-Governance MUSS die gepflegten Agentenoberflaechen,
  gemeinsame Regeln, Template-/Constitution-Synchronitaet, Modell-Routing,
  autonome Autoritaet und den seriellen statt parallelen Lauf pruefen.
  Festgestellte Paritaetsabweichungen werden nur dokumentiert.
- **FR-023**: Alle zwoelf aktivierten Presets MUESSEN mit ID, aktueller
  Version, anwendbaren Pruefpunkten, Evidenzpfad, Owner, Restrisiko und Trigger
  sichtbar behandelt werden. Ein Feature-spezifisch nicht ausgeloester
  Script-, Parallel- oder Remote-Aspekt braucht ein begruendetes `N/A` und darf
  nicht entfallen.
- **FR-024**: Die Ergebnisdokumentation MUSS German-first/English-second,
  ungefaehr CEFR B2, semantisch strukturiert, text-first und fuer
  Tastatur-, Screenreader-, Braille- und Textbrowser-Nutzung geeignet sein.
  WCAG 2.2 AA ist fuer anwendbare Kriterien die Basis.
- **FR-025**: Fachbegriffe MUESSEN bei erster Verwendung kurz erklaert oder
  mit einem beschreibenden Link auf eine Lernfassung verbunden werden.
  Status, Prioritaet und Risiko duerfen nicht allein durch Farbe, Position
  oder Symbole vermittelt werden.
- **FR-026**: Dauerhafte projektspezifische Ergebnis-Evidenz SOLL im spaeteren
  Implementierungsabschnitt unter
  `docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/`
  liegen. Feature- und Delivery-Evidenz bleibt unter `specs/045-*`. In der
  Specify-Phase werden ausser `spec.md` und `checklists/requirements.md` keine
  dieser Evidence-Dateien erzeugt oder geaendert.
- **FR-027**: Das Abschlussbild MUSS Statuszahlen, offene und nachgelagerte
  Risiken, Human-only-Grenzen, Governance-Beobachtungen, Evidence-Freshness
  und Re-Evaluation-Trigger zusammenfassen, ohne einen QISMS-, Audit-,
  Zertifizierungs- oder Compliance-Claim zu erfinden.
- **FR-028**: Der Lauf DARF keine automatische Repository-Haertung, Runtime-,
  API-, Dependency-, Paket-, Projekt-, Beispiel- oder Produktverhaltensaenderung
  enthalten. Er darf keine Secrets, privaten Pfade oder produktiven Daten
  erfassen.
- **FR-029**: Der Lauf DARF keine Remote-Aktion, Provider-/Repository-
  Konfiguration, Branch-Erzeugung oder -Wechsel, automatische Freigabe,
  Intake-Erzeugung oder Folgefeature-Erzeugung ausfuehren.
- **FR-030**: Falls Build-, Test-, Format-, Dokumentations- oder A11Y-Befehle
  spaeter durch reine Auditdokumentation ausgeloest werden, MUESSEN ihre
  Ergebnisse als Evidence erfasst werden. Nicht ausgefuehrte Plattform- oder
  Remote-Pruefungen duerfen nicht als bestanden gelten.

### Kontrollinventar / Control Inventory

Die kanonische Quellmenge wird nach `CL-XX-NN` aus den Einzelchecklisten
bestimmt. Die folgenden Zahlen sind die Specify-Akzeptanzbasis und werden im
Audit erneut mechanisch geprueft.

*The canonical source set is derived from the `CL-XX-NN` headings in the
individual checklists. The following counts are the Specify acceptance
baseline and will be checked mechanically again during the audit.*

| Kapitel / Chapter | Thema / Topic | Controls |
|---|---|---:|
| CL-01 | Standards-Anwendbarkeit / Standards applicability | 12 |
| CL-02 | Sichere Softwarearchitektur / Secure software architecture | 13 |
| CL-03 | Krypto-Mindestvorgaben / Cryptographic minimum requirements | 15 |
| CL-04 | Bedrohungsmodellierung / Threat modelling | 10 |
| CL-05 | Lieferkette und Build-Integritaet / Supply chain and build integrity | 13 |
| CL-06 | Schwachstellenoffenlegung / Vulnerability disclosure | 11 |
| CL-07 | CRA-Anwendbarkeit / CRA applicability | 12 |
| CL-08 | Sicherheits-Code-Review / Security code review | 13 |
| CL-09 | KI-Codeerzeugung / AI code generation | 17 |
| CL-10 | Sichere Entwicklungsumgebung / Secure development environment | 17 |
| CL-11 | Datenschutz-Folgenabschaetzung / Data protection impact assessment | 12 |
| CL-12 | Agentische KI-Sandbox / Agentic AI sandbox | 12 |
| **Gesamt / Total** | | **157** |

### Governance Applicability

| Preset | Version | Verpflichtung dieses Features / Feature obligation |
|---|---:|---|
| `security-governance` | 0.6.2 | Vollstaendiger RL-SE-, Standards-, MSL-, Supply-Chain- und Regulatory-Review |
| `architecture-governance` | 0.5.2 | Bestehende Trust Boundaries, STRIDE/CAPEC, Zero Trust, SAMM, C3A/C5 und Security-Evidence pruefen; keine Architektur aendern |
| `isaqb-architecture-governance` | 0.2.2 | Architekturziele, Risiken, Qualitaetsszenarien und ADR-Bedarf pruefen; neue Architekturartefakte nur als Finding benennen |
| `a11y-governance` | 0.4.3 | Auditdokumente DE-first/EN-second, CEFR B2, text-first und WCAG-2.2-AA-orientiert liefern |
| `cross-platform-governance` | 0.2.2 | Bestehende script-shaped Evidence und Plattformparitaet pruefen; neue Scripts/Cmdlets/Manpages sind in diesem Feature `N/A` |
| `agent-parity-governance` | 0.4.2 | Gepflegte Agenten-, Template- und Constitution-Oberflaechen auf Drift pruefen; nicht automatisch synchronisieren |
| `model-routing-governance` | 0.1.4 | Lokale Routing-Evidence und fail-closed Phasenzuordnung pruefen; keine Modell-/Providerkonfiguration aendern |
| `intake-authoring-governance` | 0.3.1 | Binding Intake und Lineage lesen; keine Intake-Mutation oder Neuerstellung |
| `intake-review-governance` | 0.2.1 | Hashgleichen `Ready`-Review als Startgate pruefen; keine Reparatur |
| `intake-sequencing-governance` | 0.2.3 | Bestehende Serienreihenfolge und Eligibility als Evidence lesen; keine Serie aendern |
| `autonomous-run-governance` | 0.4.1 | Authority, Run-State, Phase Evidence und fail-closed Gates einhalten; kein manueller State-Eingriff |
| `parallel-autonomous-run-governance` | 0.2.6 | `N/A` fuer die Ausfuehrung, weil dieses Feature seriell laeuft; Installations- und N/A-Evidence bleibt sichtbar |

### Bekannte Startbeobachtungen / Known Starting Observations

Diese Punkte sind noch keine abgeschlossenen Befunde. Sie muessen im Audit
gegen die dann aktuelle Repository-Evidenz bestaetigt, verworfen oder genauer
klassifiziert werden.

*These items are not completed findings yet. The audit must confirm, reject,
or refine them against the then-current repository evidence.*

- `docs/secure-development/baseline-manifest.json` nennt Baseline 3.1.0,
  waehrend Richtlinie und Sammelband 3.2.0 nennen; mehrere Einzelchecklisten
  nennen 3.0.0, `CL-09` und `CL-12` nennen 3.2.0.
- `constitution.md` nennt Version 1.17.0, waehrend
  `.specify/memory/constitution.md` Version 1.18.1 und zusaetzliche aktuelle
  Policy-Inhalte nennt.
- Die Verzahnungsdatei spricht je nach Abschnitt von sechs oder sieben
  Presets; `.specify/presets/.registry` enthaelt zwoelf aktivierte Presets.
- `docs/security/control-assessment.md` enthaelt 157 Zeilen aus Feature 016
  mit den bisherigen Zahlen 65 `Applicable`, 13 `AlreadySatisfied`, 38 `N/A`,
  36 `Open` und 5 `FollowUp`; diese Aussagen entstanden vor den oben genannten
  Governance- und Baseline-Aenderungen und muessen auf Freshness geprueft
  werden.

### Constitution Requirements

- **CR-001**: Der TuiVision-Eintrag im Level-2-Projektregister ist bindender
  Kontext: .NET 10/C#, MSTest, Coverlet-Gates, DocFX plus Playwright/axe und
  text-first Dokumentationspruefung.
- **CR-002**: NIST SSDF und CWE Top 25 sind immer anwendbar; jede weitere
  Standardentscheidung braucht Evidenz oder begruendetes `N/A`, `Open` oder
  `FollowUp`.
- **CR-003**: C# ist als MSL erlaubt. Die MSL-Einstufung ersetzt keine sichere
  Code-, Architektur-, I/O-, Dependency-, Supply-Chain- oder Agentenpruefung.
- **CR-004**: Architektur- und Security-Evidence unter `docs/architecture/`
  und `docs/security/` wird geprueft. Da dieses Feature keine Struktur,
  Schnittstelle, Runtime, Deployment-Topologie oder Trust Boundary aendert,
  sind neue ADRs, Threat-Model-Aenderungen und Zero-Trust-Architekturaenderungen
  fuer die Ausfuehrung `N/A`; beobachtete Evidenzluecken bleiben Findings.
- **CR-005**: BSI C3A/C5 wird fuer Cloud- oder Providerabhaengigkeit geprueft.
  Ohne solchen Produkt-/Deployment-Scope ist die Feature-Ausfuehrung `N/A`
  mit Trigger; bestehende Governance-Aussagen werden dennoch auditiert.
- **CR-006**: Das Feature erzeugt keine distributable Produktkomponente.
  Bestehende SBOM-/VEX-/SLSA-/Provenance-Evidence bleibt dennoch Teil des
  Repository-Selbstreviews.
- **CR-007**: KI wird in diesem Feature nur als Entwicklungswerkzeug genutzt
  und ist nicht Teil des ausgelieferten oder betriebenen TuiVision-Systems.
  AI-SBOM ist fuer die Feature-Ausfuehrung `N/A` mit Re-Evaluation bei
  Runtime-/Produkt-KI, Modellen, Datensaetzen, Inferenz-Infrastruktur oder
  ausgelieferten KI-Komponenten.
- **CR-008**: Statistikmethodik und gemeinsame Agentenregeln werden nicht
  geaendert. Deshalb ist eine synchronisierte Statistik-/Agentenaktualisierung
  in der Specify-Phase `N/A`; entdeckte Drift wird als Beobachtung erfasst.
- **CR-009**: Source-reference disposition ist `N/A`, weil weder historisch
  abgeleitetes TuiVision-Verhalten noch Kompatibilitaets- oder
  Modernisierungssemantik geaendert wird. `tv203s/` und externe Checkouts
  bleiben read-only; Re-Evaluation erfolgt bei produktsemantischem Scope.

### Documentation Impact Decision

**Decision**: `UpdateRequired` fuer die spaetere auditierte Evidence-Lieferung;
in der Specify-Phase entstehen ausschliesslich diese Spezifikation und ihre
Requirements-Qualitaetscheckliste.

- **Audiences**: Auszubildende ab dem ersten Lehrjahr, Maintainer,
  Security-Reviewer und Auditoren.
- **Documentation families**: Security-Governance, RL-SE-Kontrollmatrix,
  Feature-/Delivery-Evidence und Security-Index.
- **Reader path**: `docs/security/README.md` zur datierten
  Selbstpruefungs-Evidenz und von dort zu Kontroll- und Finding-Details.
- **Canonical source and owner**: Die 157 Einzelkontrollen unter
  `docs/secure-development/checklisten/` sind die Quellmenge; die
  projektspezifische Matrix gehoert dem TuiVision-Maintainer mit
  Security-Review.
- **Navigation impact**: Spaeterer Link im Security-Index; keine
  DocFX-Navigation in Specify.
- **Document class**: Oeffentlich nutzbare, nicht zertifizierende
  Audit-/Selbstpruefungs-Evidenz.
- **Language strategy and partner**: DE-first/EN-second, CEFR B2; beide
  Sprachspuren werden gemeinsam geprueft.
- **Platform/example proof**: `N/A`; keine Runtime-, Plattform- oder
  Beispielaenderung. Reine Dokumentationsgates gelten, wenn spaetere Artefakte
  sie ausloesen.
- **Distribution class**: Repository-lokale, sicher publizierbare Markdown-
  Evidence ohne Secrets oder private Pfade.
- **Home-sync need**: `No`; das Feature aendert keine gemeinsame Policy.
  Gefundene Policy-Drift wird nicht automatisch synchronisiert.
- **Evidence**: `specs/045-rl-se-checklist-self-review/` und die spaetere
  datierte Evidence unter `docs/security/secure-development/`.
- **Re-evaluation trigger**: Aenderung der 157-Control-Baseline, Constitution,
  Preset-Matrix, Runtime-/Produktgrenze, Distribution, Dependencies,
  Agenten-/Sandboxmodell oder regulatorischen Anwendbarkeit.

### Key Entities

- **Control Assessment**: Eine der 157 stabilen Kontrollen mit Quellidentitaet,
  genau einem erlaubten Status und allen Audit-Pflichtfeldern.
- **Evidence Reference**: Ein aktueller Repository-Pfad oder eine explizite
  Beweisluecke, die eine konkrete Aussage stuetzt oder begrenzt.
- **Governance Observation**: Ein nachpruefbarer Unterschied zwischen zwei
  Governance-Oberflaechen mit Auswirkung und ohne automatische Reparatur.
- **Human-only Boundary**: Eine Entscheidung, die rechtliche,
  organisatorische, Provider-, Secret-, Plattform- oder Freigabeautoritaet
  benoetigt.
- **Review Summary**: Kontrollierte Gesamtzahlen, Findings, offene Risiken,
  Restrisiken und Trigger ohne Compliance-Claim.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 157 von 157 kanonischen Kontroll-IDs besitzen genau eine
  Ergebniszeile; fehlende, doppelte und unbekannte IDs stehen jeweils bei null.
- **SC-002**: Die Kapitelabdeckung entspricht exakt
  `12/13/15/10/13/11/12/13/17/17/12/12` fuer `CL-01` bis `CL-12`.
- **SC-003**: 100 Prozent der Ergebniszeilen verwenden genau einen der fuenf
  erlaubten Statuswerte und enthalten alle acht geforderten Auditfelder neben
  Identitaet und Titel.
- **SC-004**: 100 Prozent der `AlreadySatisfied`-Aussagen verweisen auf
  aktuelle direkte Evidenz; leere, fehlende oder nur behauptende Evidence
  fuehrt zu null positiven Claims.
- **SC-005**: 100 Prozent der `N/A`, `Open` und `FollowUp`-Zeilen enthalten
  statusgerechte Begruendung, Owner, Follow-up, Prioritaet, Restrisiko und
  Re-Evaluation-Trigger.
- **SC-006**: Alle zwoelf aktivierten Governance-Presets, beide Constitutions,
  Baseline-Manifest, Richtlinie, Sammelband, zwoelf Einzelchecklisten,
  mitgeltenden Dokumente, MSL, Supply Chain, Regulatory, A11Y, Sandbox und
  Agenten-Grenzen sind ohne stille Auslassung in der Review-Evidence sichtbar.
- **SC-007**: Jede bestaetigte Governance-Diskrepanz nennt beide Quellen,
  Auswirkung und vollstaendige Auditfelder; keine wird durch eine nicht
  autorisierte Datei- oder Policy-Aenderung verdeckt.
- **SC-008**: Alle Human-only- und External-only-Punkte bleiben sichtbar und
  null davon werden ohne befugte Evidenz als erfuellt oder freigegeben gezaehlt.
- **SC-009**: Ein text-first Review findet null farb-, bild-, layout- oder
  pointer-only Bedeutungen und null unerlaeuterte zentrale Fachbegriffe.
- **SC-010**: Der Feature-Diff enthaelt null Runtime-, API-, Dependency-,
  Paket-, Projekt-, Beispiel-, Produktverhaltens-, Provider- oder
  Remote-Aenderungen und null automatisch erzeugte Folge-Intakes oder Features.
- **SC-011**: Reviewer koennen eine beliebige Kontrollzeile innerhalb von drei
  Minuten von Quellkontrolle ueber Entscheidung und Evidence bis zu Owner,
  Risiko, Follow-up und Trigger nachvollziehen.
- **SC-012**: Alle fuer die spaetere reine Auditdokumentation ausgeloesten
  lokalen Qualitaetsgates bestehen; nicht ausgefuehrte Remote-, Plattform- oder
  Human-Gates werden nicht als bestanden berichtet.

## Assumptions

- Die akzeptierte Intake und der `Ready`-Serienreview bleiben waehrend der
  Specify-Phase hashgleich; Drift blockiert die Phasenfreigabe.
- Die zwoelf Einzelchecklisten sind die kanonische ID-Quelle. Manifest,
  Sammelband und bestehende Matrix sind wichtige Vergleichsevidenz, koennen
  aber selbst Drift enthalten.
- Die vorhandene Feature-016-Matrix ist wertvolle Ausgangsevidenz, wird jedoch
  wegen spaeterer Baseline-, Constitution-, Preset- und Sandbox-Aenderungen
  nicht automatisch als aktuell angenommen.
- TuiVision bleibt im Audit ein .NET-10-/C#-Terminal-UI-Framework ohne neue
  Web-, API-, Auth-, Cloud-, Datenbank-, Runtime-KI- oder Providergrenze.
- Owner werden als verantwortliche Rollen benannt; persoenliche Freigaben
  werden nur mit vorhandener, nicht sensibler Evidence behauptet.
- `None` in Prioritaet oder Follow-up ist ein ausdruecklicher Wert mit
  Begruendung und niemals ein leeres oder ausgelassenes Pflichtfeld.

## Dependencies

- Binding Intake und aktueller Intake-Review muessen lesbar und hashgleich sein.
- Baseline-Manifest, Richtlinie, Sammelband, alle zwoelf Einzelchecklisten und
  alle dort registrierten mitgeltenden Dokumente muessen lesbar sein.
- Beide Constitution-Dateien, die Preset-Registry, autonome Lauf-Evidence und
  bestehende TuiVision-Security-/Architecture-/A11Y-Evidenz muessen fuer den
  read-only Vergleich verfuegbar sein.
- Fehlende externe, Provider-, Plattform- oder Human-Evidence ist eine
  dokumentierte Proof-Grenze und kein Grund, Fakten zu erfinden.

## Non-Goals

- Keine automatische Security-, Governance-, Architektur- oder
  Repository-Haertung.
- Keine Runtime-, Public-API-, Dependency-, Paket-, Projekt-, Beispiel- oder
  Produktverhaltensaenderung.
- Keine Korrektur von Manifest, Richtlinie, Checklisten, Constitutions,
  Presets, Agentenoberflaechen oder bestehender Evidence allein aufgrund einer
  Beobachtung.
- Keine echte Kunden-/Produktivdaten-, Token-, Credential-, Session- oder
  private-Pfad-Evidence.
- Keine Provider-, Repository-, Branch-Protection-, Secret-, Modell-,
  Sandbox-Image- oder Organisationskonfiguration.
- Kein Multi-Repository- oder Parallel-Campaign-Lauf.
- Kein Commit, Push, Pull Request, Merge, Bypass oder andere Remote-Aktion als
  Bestandteil der fachlichen Selbstpruefung.
- Keine automatische Intake-, Issue-, Branch- oder Folgefeature-Erzeugung.
- Keine erfundene Rechtsbewertung, formale Freigabe, Zertifizierung oder
  QISMS-/Compliance-Behauptung.
