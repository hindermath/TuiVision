# Feature Specification: GSDB-Spec-Kit-Intensivpruefung

**Feature Branch**: `046-gsdb-spec-kit-intensive-review`
**Created**: 2026-08-30
**Status**: Draft - Clarification gates passed
**Binding Input**: `requirements/intakes/active/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md`
**Accepted Review**: Series review `67d89984-7536-4bab-bc51-02ef8d1edec4` (`Ready`)
**Delivery Boundary**: Evidence and applicability review only; no product hardening or formal compliance claim
**Delivery Authority**: `MergeAndSync` for this repository's non-empty Feature-046 delivery, subject to current run authority and exact-head convergence gates

## Zweck / Purpose

Feature 046 fuehrt eine unabhaengige, intensive Bestandsaufnahme von TuiVision
gegen die Generische Secure-Development Basis (GSDB) durch. Der Review umfasst
die vollstaendige Richtlinie, das Baseline-Manifest, den erzeugten Sammelband,
alle 157 stabilen Kontroll-IDs in den zwoelf Einzelchecklisten, alle
mitgeltenden Dokumente, den Lernpfad, den MSL-Status, die sprachspezifischen
Secure-Coding-Regeln, beide Constitution-Oberflaechen, alle installierten
Governance-Presets und die vorhandene Repository-Evidenz. Das Ergebnis ist eine
zeitpunktbezogene, nachvollziehbare Bewertung. Es ist keine Zertifizierung,
Rechtsberatung oder formale Compliance-Freigabe.

Feature 046 performs an independent, intensive inventory of TuiVision against
the Generic Secure Development Baseline (GSDB). The review covers the complete
guideline, baseline manifest, generated compendium, all 157 stable control IDs
in the twelve individual checklists, every related document, the learning
path, memory-safe-language status, language-specific secure-coding rules, both
constitution surfaces, every installed governance preset, and existing
repository evidence. The result is a traceable point-in-time assessment. It is
not certification, legal advice, or a formal compliance approval.

## Clarifications

### Session 2026-08-30

- Q: Welche Delivery-Autoritaet gilt fuer Feature 046? / Which delivery authority applies to Feature 046? → A: `MergeAndSync` erlaubt Commit, Push, Pull Request, Merge und Branch-Bereinigung nur fuer die nicht leere Lieferung dieses Repositorys unter der aktuellen Run-Autoritaet und den Remote-Konvergenz-Gates. Der enge Admin-Bypass gilt nur nach gruenen technischen Gates, ohne actionable Review-Threads und wenn Human Approval die einzige offene Regel ist. / `MergeAndSync` permits commit, push, pull request, merge, and branch cleanup only for this repository's non-empty delivery under current run authority and remote convergence gates. The narrow admin bypass applies only after green technical gates, with no actionable review threads, and when Human Approval is the sole open rule.
- Q: Welche Kardinalitaet ist fest und wie werden weitere Checkpoints bestimmt? / Which cardinality is fixed and how are additional checkpoints determined? → A: Genau 157 kanonische Kontrollen sind fest. Weitere Quellen-, Sprach-, Preset-, Constitution-, Governance- und Evidence-Checkpoints werden deterministisch aus dem akzeptierten Snapshot inventarisiert, nicht durch eine erfundene feste Gesamtzahl. / Exactly 157 canonical controls are fixed. Additional source, language, preset, constitution, governance, and evidence checkpoints are inventoried deterministically from the accepted snapshot, not through an invented fixed total.
- Q: Blockieren `Open` oder `FollowUp` die Abnahme und wird Folgearbeit erzeugt? / Do `Open` or `FollowUp` block acceptance, and is follow-up work created? → A: Beide sind wahrheitsgetreue Audit-Ergebnisse und bestehen die Abnahme bei vollstaendigen Pflichtfeldern; Finding-abgeleitete Folgearbeit wird beschrieben, aber in Feature 046 nicht als Intake, Issue, Branch oder Feature erzeugt. / Both are truthful audit outcomes and satisfy acceptance when required fields are complete; finding-derived follow-up work is described but is not created as an intake, issue, branch, or feature in Feature 046.
- Q: Wann ist `AlreadySatisfied` zulaessig? / When is `AlreadySatisfied` allowed? → A: Nur aktuelle, direkte, fuer den revalidierten Repository-Snapshot reproduzierbare Evidence darf `AlreadySatisfied` stuetzen; historische Claims, Vorlagen und blosse Dateiexistenz genuegen nicht. / Only current, direct evidence reproducible for the revalidated repository snapshot may support `AlreadySatisfied`; historical claims, templates, and mere file existence are insufficient.
- Q: Wie gelten historische Quellen und Qualitaetsgates? / How do historical sources and quality gates apply? → A: Historische Quellbaeume sind `N/A`, sofern keine konkrete GSDB-Frage eine read-only Konsultation erfordert. Dokumentations-, A11Y- und Security-Gates skalieren proportional zu den tatsaechlich geaenderten Evidence-Oberflaechen. / Historical source trees are `N/A` unless a concrete GSDB question requires read-only consultation. Documentation, accessibility, and security gates scale proportionally to the evidence surfaces actually changed.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - GSDB ohne Luecken erfassen (Priority: P1)

Security-Reviewer und Maintainer koennen fuer jede der 157 kanonischen
Kontrollen und fuer jede weitere relevante GSDB-Pruefflaeche genau eine
aktuelle Entscheidung finden. Keine Checkliste, Richtlinienflaeche oder
mitgeltende Quelle wird still ausgelassen.

*Security reviewers and maintainers can find exactly one current decision for
each of the 157 canonical controls and every other relevant GSDB review
surface. No checklist, policy area, or related source is silently omitted.*

**Why this priority**: Eine intensive Pruefung ist nur belastbar, wenn die
Quellmenge vollstaendig und mechanisch nachvollziehbar ist.

**Independent Test**: Ein Abgleich weist 157 eindeutige Quell-IDs und 157
eindeutige Bewertungszeilen nach. Zusaetzliche Inventare werden deterministisch
aus dem akzeptierten Snapshot abgeleitet und zeigen jede GSDB-Quelle, jedes
Sprachprofil, jede Constitution und jedes installierte Preset.

**Acceptance Scenarios**:

1. **Given** die zwoelf kanonischen Einzelchecklisten, **When** Quell- und Ergebnismenge verglichen werden, **Then** ist jede der 157 IDs genau einmal vorhanden und fehlende, doppelte oder unbekannte IDs stehen jeweils bei null.
2. **Given** Richtlinie, Manifest, Sammelband und mitgeltende Dokumente, **When** die Quellenabdeckung geprueft wird, **Then** besitzt jede Quelle eine sichtbare Review-Disposition oder eine begruendete Quellgrenze.
3. **Given** eine Bewertungszeile, **When** ihre Pflichtfelder geprueft werden, **Then** sind Disposition, Begruendung, Evidenzpfad, Owner, Follow-up, Re-Evaluation-Trigger und Restrisiko konkret und nicht leer.

---

### User Story 2 - Vorhandene Evidenz unabhaengig bewerten (Priority: P1)

Reviewer koennen erkennen, welche Anforderungen aktuelle direkte Evidence
stuetzt und welche Aussagen aus frueheren Features nur Eingangsmaterial sind.
Feature 045 wird nicht ungeprueft als positives Ergebnis uebernommen.

*Reviewers can distinguish requirements supported by current direct evidence
from statements in earlier features that are only input material. Feature 045
is not copied as a positive result without renewed review.*

**Why this priority**: Dateiexistenz, eine Vorlage oder ein frueherer Status
beweisen nicht automatisch den aktuellen Zustand.

**Independent Test**: Eine Stichprobe kann von der GSDB-Quelle ueber die
aktuelle Entscheidung bis zum direkt stuetzenden Repository-Pfad verfolgt
werden. Fehlende oder veraltete Evidence fuehrt nie zu `AlreadySatisfied`.

**Acceptance Scenarios**:

1. **Given** eine fruehere positive Bewertung, **When** Evidence-Freshness, Scope und direkte Aussagekraft geprueft werden, **Then** bleibt `AlreadySatisfied` nur bei weiterhin belastbarer aktueller Evidence zulaessig.
2. **Given** ein existierender Evidence-Pfad, **When** er nur eine Vorlage, Policy oder unbelegte Behauptung enthaelt, **Then** wird der Punkt nicht positiv abgeschlossen.
3. **Given** widerspruechliche Versions-, Preset- oder Constitution-Angaben, **When** der Review sie erkennt, **Then** bleibt die Abweichung als bewerteter Befund sichtbar und wird nicht still repariert.

---

### User Story 3 - MSL, Sprachregeln und Human-Grenzen trennen (Priority: P1)

Maintainer koennen den C#-MSL-Status von den weiterhin noetigen Regeln fuer
sichere API-, Datei-, Prozess-, Serialisierungs- und Dependency-Nutzung
trennen. Menschliche, rechtliche, organisatorische, Provider-, Secret- und
Plattformentscheidungen bleiben bei befugten Rollen.

*Maintainers can separate the C# memory-safe-language status from the secure
API, file, process, serialization, and dependency rules that still apply.
Human, legal, organisational, provider, secret, and platform decisions remain
with authorised roles.*

**Why this priority**: Speichersicherheit ist ein wichtiger Schutz, aber kein
Ersatz fuer Secure Coding oder befugte Freigaben.

**Independent Test**: Die Sprachmatrix behandelt C#/.NET, Bash, PowerShell und
TypeScript/JavaScript nach Repository-Nutzung. Nicht aktive Sprachprofile und
historische C/C++-Quellen erhalten eine begruendete Grenze und einen Trigger.
Human-only-Punkte sind ohne befugte Evidence nie `AlreadySatisfied`.

**Acceptance Scenarios**:

1. **Given** C# als primaere Sprache, **When** die MSL-Regel geprueft wird, **Then** ist C# als erlaubte MSL dokumentiert und die sprachspezifische Secure-Coding-Pruefung bleibt trotzdem anwendbar.
2. **Given** Bash-, PowerShell- oder TypeScript/JavaScript-Werkzeuge im Repository, **When** die Sprachprofile geprueft werden, **Then** werden ihre relevanten Regeln und Cross-Platform-Grenzen sichtbar bewertet.
3. **Given** eine Entscheidung mit Rechts-, Organisations-, Provider-, Secret-, realer Plattform- oder formaler Freigabeautoritaet, **When** keine befugte publizierbare Evidence vorliegt, **Then** bleibt sie `Open`, `FollowUp` oder fachlich begruendet `N/A`, niemals positiv bestanden.

---

### User Story 4 - Ergebnis inklusiv nachvollziehen (Priority: P2)

Auszubildende ab dem ersten Lehrjahr, Maintainer und Auditoren koennen Status,
Evidence, Risiko und Folgeaktion ohne farb-, bild- oder layoutabhaengige
Bedeutung verstehen.

*First-year apprentices, maintainers, and auditors can understand status,
evidence, risk, and follow-up without meaning that depends on colour, images,
or layout.*

**Why this priority**: Audit-Evidence ist nur nutzbar, wenn unterschiedliche
Lesergruppen sie selbststaendig und mit assistiver Technik verfolgen koennen.

**Independent Test**: Eine text-first Pruefung bestaetigt semantische
Ueberschriften, beschreibende Links, DE-first/EN-second Erklaerungen auf
CEFR-B2-Niveau und kurze Erklaerungen wichtiger Fachbegriffe.

**Acceptance Scenarios**:

1. **Given** eine Status- oder Risikoaussage, **When** sie ohne Farbe und visuelles Layout gelesen wird, **Then** bleibt die vollstaendige Bedeutung erhalten.
2. **Given** ein Fachbegriff wie MSL, SBOM, VEX, SLSA, ASVS, CAPEC, C3A oder C5, **When** er erstmals erscheint, **Then** wird er kurz erklaert oder mit einer beschreibenden Lernquelle verbunden.
3. **Given** ein zusaetzliches Diagramm oder Bild, **When** Screenreader, Braille-Display oder Textbrowser genutzt werden, **Then** steht eine gleichwertige Textbeschreibung bereit.

### Edge Cases

- Die Einzelchecklisten liefern nicht exakt 157 eindeutige IDs: Der Review
  stoppt die Vollstaendigkeitsaussage und dokumentiert die Baseline-Abweichung.
- Manifest, Richtlinie, Sammelband oder Einzelchecklisten nennen verschiedene
  Versionen: Alle Angaben bleiben mit Quelle, Auswirkung und Trigger sichtbar;
  keine Fassung wird ohne Governance-Entscheidung still bevorzugt.
- Ein Pfad existiert, stuetzt aber nur einen Teil der Aussage: Die Bewertung
  darf hoechstens die direkt belegte Grenze positiv darstellen; der Rest bleibt
  `Open` oder `FollowUp`.
- Ein Punkt ist anwendbar, darf aber in diesem Review nicht gehaertet werden:
  Er wird nicht als `N/A` versteckt.
- Ein Human-only-Punkt wirkt technisch plausibel: Ohne befugte Evidence bleibt
  er offen oder nachgelagert; Agentenplausibilitaet ist kein Nachweis.
- Ein Language-Rule-Profil existiert, die Sprache wird aber nur in read-only
  Historienquellen genutzt: Die Nichtanwendbarkeit nennt diese Grenze und den
  Trigger fuer eine spaetere aktive Kompilierung, Aenderung oder Auslieferung.
- `Follow-up` ist nicht erforderlich: Das Feld verwendet ausdruecklich `None`
  mit statusbezogener Begruendung und bleibt nie leer.
- Eine Remote-, Plattform- oder Provider-Pruefung wurde nicht ausgefuehrt: Sie
  darf nicht als bestanden oder `AlreadySatisfied` berichtet werden.
- Ein vollstaendig dokumentierter Befund endet in `Open` oder `FollowUp`: Diese
  wahrheitsgetreue Disposition blockiert die Abnahme nicht allein durch ihre
  Existenz; unvollstaendige Pflichtfelder oder ein unbelegter positiver Claim
  blockieren sie weiterhin.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Der Lauf MUSS die binding Intake-Datei, ihren akzeptierten
  normalisierten Hash und den hashgleichen `Ready`-Serienreview als Herkunft
  dokumentieren. Branch, Feature-Verzeichnis und autonomer Run-State bleiben
  im Specify-Schreibumfang unveraendert.
- **FR-002**: Die Lesereihenfolge MUSS mit der zentralen Verzahnungsdatei
  beginnen und danach die vollstaendige GSDB-Quellmenge, beide Constitutions,
  installierte Presets und projektspezifische Evidence einbeziehen.
- **FR-003**: Der Review MUSS Richtlinie, `baseline-manifest.json`, generierten
  Sammelband, alle zwoelf Einzelchecklisten, alle im Manifest registrierten
  mitgeltenden Dokumente, Lernpfad, verwaltete MSL-Referenzen und die beiden
  GSDB-README-Flaechen abdecken. Eine stille Auslassung ist unzulaessig.
- **FR-004**: Die kanonische Kontrollmenge MUSS exakt alle 157 eindeutigen
  `CL-XX-NN`-IDs aus den Einzelchecklisten enthalten. Fehlende, doppelte oder
  unbekannte IDs blockieren die Aussage einer vollstaendigen Pruefung.
- **FR-005**: Jede der 157 Kontrollen und jeder zusaetzliche relevante
  Richtlinien-, Dokument-, Sprach-, Preset-, Constitution- oder Evidence-
  Checkpoint MUSS genau eine Disposition aus `Applicable`,
  `AlreadySatisfied`, `N/A`, `Open` oder `FollowUp` erhalten. `Pass`,
  kombinierte Werte und stille Auslassungen sind verboten. Nur die 157
  kanonischen Kontrollen besitzen eine feste Kardinalitaet. Die weiteren
  Checkpoints MUESSEN deterministisch aus dem akzeptierten Snapshot abgeleitet
  werden: aus registrierten Manifestquellen, tatsaechlich vorhandenen oder in
  der GSDB geregelten Sprachprofilen, der aktivierten Preset-Registry, beiden
  Constitution-Oberflaechen, aktueller Run-Governance und den inventarisierten
  Evidence-Familien. Inventarregeln, Snapshot-Bezug und Ergebnisanzahl MUESSEN
  nachvollziehbar sein; eine erfundene feste Gesamtzahl ist unzulaessig.
- **FR-006**: Die zweiachsigen GSDB-Quellbegriffe `Applicable`, `N/A`, `Open`
  sowie `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled`, `Not Assessed`
  MUESSEN als Quellkontext erhalten bleiben. Sie duerfen die eine verbindliche
  Feature-Disposition aus FR-005 nicht ersetzen oder einen positiven Claim
  ohne direkte Evidence erzeugen.
- **FR-007**: Jede Bewertungszeile MUSS ID oder stabile Identitaet, Quellpfad,
  Titel, genau eine Disposition, Begruendung, konkrete Evidence oder explizite
  Beweisluecke, Owner, Follow-up, Re-Evaluation-Trigger und Restrisiko
  enthalten. Reviewer, Pruefdatum und Snapshot-Bezug MUESSEN fuer die
  Gesamtbewertung nachvollziehbar sein.
- **FR-008**: `AlreadySatisfied` MUSS aktuelle, direkte, zum bewerteten
  und revalidierten Repository-Snapshot gehoerende, dort reproduzierbar
  nachvollziehbare Evidence besitzen. Vorlage, Policy, Dateiexistenz,
  fruehere Feature-Behauptung, historische Evidence ohne aktuelle
  Aussagekraft oder lokaler nicht reproduzierbarer Zustand genuegen nicht.
- **FR-009**: `Applicable` MUSS begruenden, warum der Punkt gilt, welche
  Evidence oder Bewertung erwartet wird und welches Restrisiko bis zur
  abschliessenden Einordnung besteht.
- **FR-010**: `N/A` MUSS technische oder fachliche Nichtanwendbarkeit,
  Evidence fuer die Systemgrenze, Owner, Follow-up, Trigger und Restrisiko
  nennen. Fehlende Zeit, Evidence oder Autoritaet sind keine
  Nichtanwendbarkeit.
- **FR-011**: `Open` MUSS die Beweisluecke oder ungeklaerte Entscheidung,
  Owner, sichere Folgeaktion, Trigger und Restrisiko nennen. Fehlende Evidence
  und Human-only-Entscheidungen duerfen nie als positiver Status erscheinen.
- **FR-012**: `FollowUp` MUSS den anwendbaren, aber ausserhalb dieses Reviews
  liegenden Arbeitsbedarf mit Owner, beschreibender Folgeaktion, Trigger und
  Restrisiko benennen. Der Lauf darf daraus kein Intake, Issue, Feature oder
  Branch erzeugen. Dasselbe Erzeugungsverbot gilt fuer finding-abgeleitete
  Folgearbeit unabhaengig von ihrer Prioritaet.
- **FR-013**: Die Feature-016-, Feature-044- und Feature-045-Evidence MUSS als
  Ausgangsmaterial gegen aktuelle Primarquellen, Snapshot und direkte
  Aussagekraft neu bewertet werden. Statuswerte duerfen nicht ungeprueft
  kopiert werden.
- **FR-014**: Versions-, Inhalts-, Constitution-, Preset- oder Evidence-Drift
  MUSS als eigenstaendiger Checkpoint mit vollstaendigen Pflichtfeldern
  erfasst werden. Dieser Review repariert keine Governance-Quelle still.
- **FR-015**: Die Primaersprache MUSS als .NET 10/C# und damit als Sprache auf
  der Constitution-MSL-Erlaubnisliste bewertet werden. MSL bedeutet hier
  Speichersicherheit durch die Sprachlaufzeit; sie ersetzt keine sichere API-
  Nutzung, Eingabevalidierung, Fehlerbehandlung, Serialisierung, Datei- oder
  Prozessgrenze, Kryptopruefung und Dependency-Evidence.
- **FR-016**: Das C#/.NET-Sprachprofil MUSS mindestens parametrisierte
  Datenzugriffe, kontextgerechte Ausgabe, Autorisierung, Eingabevalidierung,
  sichere Deserialisierung, HTTP-Timeout-/SSRF-Grenzen, eingeschraenkte
  Dateipfade und Secret-Behandlung pruefen, soweit der jeweilige Codepfad im
  Repository vorkommt.
- **FR-017**: Bash und PowerShell MUESSEN als aktive Werkzeugsprachen
  bewertet werden. Der Review umfasst Eingabevalidierung, Quoting,
  End-of-options, Strict Mode, verbotene dynamische Ausfuehrung,
  temporaere Dateien, Fehlerverhalten sowie funktionale und Hilfe-Paritaet.
- **FR-018**: TypeScript/JavaScript MUSS fuer den vorhandenen Web-A11Y-
  Testpfad bewertet werden. Laufzeiteingaben, dynamische Codeausfuehrung,
  Dateipfade, Netzgrenzen, Secrets und Lock-/Dependency-Evidence werden nach
  tatsaechlicher Nutzung geprueft.
- **FR-019**: C/C++ und weitere vorhandene oder in der Regelvorlage genannte
  Sprachprofile MUESSEN eine sichtbare Anwendbarkeitsentscheidung erhalten.
  Read-only historische Quellen sind keine aktive TuiVision-
  Implementierung und ihre Quellbaeume sind fuer Feature 046 grundsaetzlich
  `N/A`. Nur wenn eine konkrete GSDB-Frage ohne sie nicht belastbar beantwortet
  werden kann, duerfen die genau benoetigten historischen Dateien read-only
  konsultiert und als begrenzte Kontext-Evidence dokumentiert werden. Eine
  spaetere Aenderung, Kompilierung oder Auslieferung ist der Re-Evaluation-
  Trigger. SQL und nicht vorhandene Runtime-Sprachen werden nicht still
  ausgelassen.
- **FR-020**: NIST SSDF und CWE Top 25 MUESSEN fuer den Level-2-Review immer
  behandelt werden. OWASP ASVS, SBOM, VEX, AI-SBOM, SLSA, SAMM, CAPEC, Zero
  Trust, OWASP Cheat Sheets/Proactive Controls und OpenSSF Scorecard MUESSEN
  jeweils eine evidenzbasierte Disposition erhalten.
- **FR-021**: Regulatorische und Assurance-Punkte MUESSEN CRA, NIS2, DORA,
  EU AI Act, Datenschutz/DPIA, BSI C3A und BSI C5 einzeln behandeln.
  Rechtliche, organisatorische, kommerzielle oder Provider-Entscheidungen
  ohne befugte Evidence bleiben `Open`, `FollowUp` oder begruendet `N/A`.
- **FR-022**: Architekturpunkte MUESSEN Trust Boundaries, CIA, STRIDE,
  relevante CAPEC-Muster, Defense in Depth, Least Privilege, sichere
  Defaults, Angriffsoberflaeche, Konfiguration, Daten-/Datei-/UI-/CLI-/
  Prozessgrenzen, Deployment, technische Schulden und Restrisiken pruefen.
  Der Review aendert keine Architektur und erzeugt keine ADR-Reparatur.
- **FR-023**: Supply-Chain-Punkte MUESSEN Dependency-Inventar,
  Restore-/Lock-Reproduzierbarkeit, unveraenderliche Workflow-Referenzen,
  SBOM, bekannte Schwachstellen/VEX, Provenance/SLSA, Malware-/Secret-Scans,
  Offenlegungsweg und relevante Scorecard-Evidence abdecken.
- **FR-024**: Entwicklungs-KI MUSS von Runtime-/Produkt-KI getrennt werden.
  Fuer reine Entwicklungs-KI ist AI-SBOM `N/A` mit Systemgrenzen-Evidence;
  Runtime-/Produkt-KI, Modelle, Datensaetze, Inferenz-Infrastruktur oder
  ausgelieferte KI-Komponenten loesen eine Neubewertung aus.
- **FR-025**: Sandbox- und agentische Punkte MUESSEN Mounts, Schreibrechte,
  Hostdaten, Agentenzustand, Secrets, Netzwerk, Toolchain, Modell-Routing,
  Prompt-/Log-Redaction, praktische Plattform-Evidence, Freigabe und
  Lebenszyklus getrennt behandeln.
- **FR-026**: Beide Constitution-Oberflaechen, der TuiVision-Level-2-Eintrag,
  gemeinsame Agentenflaechen, Templates und aktuelle Run-Governance MUESSEN
  auf Anwendbarkeit, Synchronitaet und Drift geprueft werden. Abweichungen
  werden dokumentiert, nicht in diesem Review behoben.
- **FR-027**: Alle im revalidierten Snapshot aktivierten Presets aus der
  lokalen Registry MUESSEN mit ID, Version, Prioritaet, Review-Verpflichtung,
  Evidence, Owner, Follow-up, Trigger und Restrisiko behandelt werden. Die
  aktuelle Beobachtung von zwoelf Eintraegen ist zu revalidieren und keine
  erfundene feste Abnahmekardinalitaet. Installation allein ist kein Nachweis
  der inhaltlichen Erfuellung.
- **FR-028**: Der Review MUSS die vorhandenen Evidence-Familien unter
  `docs/security/`, `docs/architecture/`, `docs/accessibility/`, Feature-
  Evidence, CI-/Workflow-Dateien, Tests, Coverage-, DocFX-/A11Y-Nachweise und
  Security-Selbstpruefungen nach Aktualitaet und Aussagegrenze inventarisieren.
- **FR-029**: Das Ergebnis MUSS eine Kontrollmatrix, Quellabdeckung,
  Sprachprofilbewertung, Preset-Bewertung, Governance-Beobachtungen,
  Human-/External-Grenzen und eine Summary mit Statuszahlen enthalten. Diese
  Artefakte werden erst in spaeteren Phasen erzeugt; Specify schreibt nur
  `spec.md` und die Requirements-Qualitaetscheckliste.
- **FR-030**: Dauerhafte projektspezifische Review-Evidence SOLL unter
  `docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review/`
  liegen. Feature- und Delivery-Evidence bleibt unter `specs/046-*`.
- **FR-031**: Learner-facing Erklaerungen MUESSEN German-first/English-second,
  ungefaehr CEFR B2, semantisch strukturiert und text-first sein. WCAG 2.2 AA
  ist fuer anwendbare Kriterien die Basis; Status und Risiko duerfen nicht
  nur durch Farbe, Symbol, Position oder Bild vermittelt werden.
- **FR-032**: Das Abschlussbild DARF keine formale Sicherheit, Konformitaet,
  Zertifizierung, Rechtsfreigabe, QISMS-Reife oder Provider-Assurance
  behaupten. Es beschreibt ausschliesslich den belegten Repository-Snapshot
  und seine Grenzen.
- **FR-033**: Der Lauf DARF keine Produkt-, Runtime-, Public-API-, Dependency-,
  Paket-, Projekt-, Beispiel-, Workflow-, Provider-, Secret-Rotations- oder
  Repository-Einstellungs-Aenderung enthalten. Er darf keine privaten Pfade,
  Credentials, Sessions oder produktiven Daten erfassen.
- **FR-034**: Die ausdrueckliche aktuelle Feature-046-Autoritaet ist
  `MergeAndSync`. Sie erlaubt Commit, Push, einen nicht leeren Pull Request,
  policy-konformen Merge und Feature-Branch-Bereinigung ausschliesslich fuer
  dieses Repository, unter der aktuellen Run-Autoritaet und fuer den exakt
  validierten Head nach erfolgreicher Remote-Konvergenz. Ein enger Admin-
  Bypass ist nur erlaubt, wenn alle technischen Gates gruen sind, keine
  actionable Review-Threads verbleiben und Human Approval die einzige offene
  Regel ist; technische, Security-, Scope- oder Review-Gates duerfen niemals
  umgangen werden. Diese Autoritaet erlaubt keine Produkt- oder Governance-
  Haertung, Provider-/Organisationseinstellungen, Secret-Rotation, formale
  Freigabe-Claims, automatische Folge-Intake-Erzeugung oder Remote-Aktionen
  ausserhalb dieses Repositorys. Die Clarify-Phase selbst fuehrt keine dieser
  Delivery-Aktionen aus.
- **FR-035**: Nicht ausgefuehrte Remote-, Plattform-, Human- oder Provider-
  Gates duerfen nicht als bestanden gelten. Dokumentations-, A11Y- und
  Security-Gates MUESSEN proportional zu den tatsaechlich geaenderten
  Evidence-Oberflaechen bestimmt und mit Ergebnis und Scope erfasst werden.
  DocFX- und zugehoerige Web-A11Y-Pruefungen sind erforderlich, wenn ihre
  Quell-, Navigations-, API- oder generierten Oberflaechen geaendert werden;
  Security-Validatoren sind erforderlich, wenn ihre Evidence- oder
  Strukturformate betroffen sind. Nicht betroffene Produkt-Build-, Runtime-
  oder Plattformgates duerfen weder pauschal verlangt noch als bestanden
  behauptet werden.
- **FR-036**: `Open` und `FollowUp` sind wahrheitsgetreue Audit-Ergebnisse und
  blockieren die Feature-Abnahme nicht allein durch ihre Existenz. Abnahme
  erfordert vollstaendige Pflichtfelder, korrekte Dispositionen, belegte
  positive Claims, vollstaendige Inventare und alle fuer die tatsaechlichen
  Aenderungen geltenden Gates; verschwiegene, widerspruechliche oder
  unvollstaendige Findings blockieren sie.

### GSDB-Quellinventar / GSDB Source Inventory

Die Einzelchecklisten sind die kanonische Kontrollquelle. Manifest und
Sammelband sind Kontroll- und Konsistenzflaechen, nicht automatisch die
hoeherrangige Wahrheit. Genau 157 Kontroll-IDs sind fest. Alle weiteren
Inventare werden fuer den akzeptierten Snapshot deterministisch erzeugt und
weisen ihre tatsaechliche Anzahl aus.

*The individual checklists are the canonical control source. The manifest and
compendium are consistency and review surfaces, not automatically the
higher-ranking truth. Exactly 157 control IDs are fixed. Every additional
inventory is derived deterministically for the accepted snapshot and reports
its actual count.*

| Quellgruppe / Source group | Verbindlicher Umfang / Required scope |
|---|---|
| Kernbasis / Core baseline | `README.md`, `Richtlinie_Sichere-Entwicklung.md`, `baseline-manifest.json`, `Checklistensammelband_Sichere-Entwicklung.md` |
| Kontrollen / Controls | Alle zwoelf Dateien unter `docs/secure-development/checklisten/` und alle 157 stabilen IDs |
| Mitgeltende Dokumente / Related documents | Alle im revalidierten Manifest-Snapshot registrierten Dokumente einschliesslich zentraler Verzahnung, Standardsregister, SDLC, Design, Programmierung, Sandbox, Test, Change, Zugriff, Datenschutz, Krypto, Lieferanten und BCM |
| Lernen und MSL / Learning and MSL | Lernpfad, MSL-Referenzdateien, PDF-Hash und Constitution-MSL-Regeln |
| Projekt-Governance / Project governance | `constitution.md`, `.specify/memory/constitution.md`, TuiVision-Level-2-Kontext, Preset-Registry und autonome Run-Evidence |
| Projekt-Evidence / Project evidence | `docs/security/`, gleichwertige Architektur-/A11Y-Nachweise, Features 016/044/045, CI, Tests, Coverage, DocFX und A11Y-Smokes |

### Kontrollinventar / Control Inventory

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

Die Tabelle zeigt die beim Specify-Snapshot beobachteten Presets. Planung und
Review MUESSEN die aktivierte Registry erneut lesen, ID und Version erfassen
und die tatsaechliche Anzahl verwenden.

*The table shows the presets observed at the Specify snapshot. Planning and
review MUST read the enabled registry again, record ID and version, and use
the actual count.*

| Preset | Version | Verpflichtung dieses Features / Feature obligation |
|---|---:|---|
| `security-governance` | 0.6.2 | Vollstaendiger GSDB-, MSL-, Sprachregel-, Standards-, Supply-Chain- und Regulatory-Review |
| `architecture-governance` | 0.5.2 | Bestehende Trust Boundaries, STRIDE/CAPEC, Zero Trust, SAMM, C3A/C5 und Security-Evidence pruefen; keine Architektur aendern |
| `isaqb-architecture-governance` | 0.2.2 | Architekturziele, Qualitaetsszenarien, Risiken, Debt und ADR-Bedarf pruefen; Reparatur nur als Finding benennen |
| `a11y-governance` | 0.4.3 | Ergebnis DE-first/EN-second, CEFR B2, text-first und WCAG-2.2-AA-orientiert liefern |
| `cross-platform-governance` | 0.2.2 | Bestehende Bash-/PowerShell- und Plattformparitaet pruefen; keine neue Script-Flaeche erzeugen |
| `agent-parity-governance` | 0.4.2 | Agenten-, Template- und Constitution-Oberflaechen auf Drift pruefen; nicht synchronisieren |
| `model-routing-governance` | 0.1.4 | Lokale fail-closed Routing-Evidence pruefen; keine Modell- oder Providerkonfiguration aendern |
| `intake-authoring-governance` | 0.3.1 | Binding Intake und Lineage lesen; keine Intake-Mutation oder Neuerstellung |
| `intake-review-governance` | 0.2.1 | Hashgleichen `Ready`-Review als Gate pruefen; keine Reparatur |
| `intake-sequencing-governance` | 0.2.3 | Serienreihenfolge und Eligibility lesen; keine Serie aendern |
| `autonomous-run-governance` | 0.4.1 | Authority, Run-State und Phasen-Gates einhalten; keinen Runner-State manuell schreiben |
| `parallel-autonomous-run-governance` | 0.2.6 | Installation und Applicability pruefen; Ausfuehrung ist `N/A`, weil Feature 046 seriell laeuft |

### Bekannte Startbeobachtungen / Known Starting Observations

Diese Punkte sind keine vorweggenommenen Endbefunde. Der intensive Review muss
sie gegen den aktuellen Snapshot bestaetigen, verwerfen oder genauer
klassifizieren.

*These items are not predetermined final findings. The intensive review must
confirm, reject, or refine them against the current snapshot.*

- Das Manifest nennt Baseline 3.1.0, die Richtlinie und der erzeugte
  Sammelband nennen 3.2.0; mehrere Einzelchecklisten nennen 3.0.0, waehrend
  `CL-09` und `CL-12` 3.2.0 nennen.
- `constitution.md` nennt Version 1.17.0, waehrend
  `.specify/memory/constitution.md` Version 1.18.1 und neuere
  Governance-Aussagen enthaelt.
- Die zentrale Verzahnungsdatei beschreibt ein aelteres sechs-/sieben-Preset-
  Modell; die lokale Registry enthaelt zwoelf aktivierte Presets.
- GSDB-README und Constitution verlangen den Check-Modus von
  `build-secure-development-docs.*`; die dort genannten Bash-/PowerShell-
  Skripte sind im aktuellen Repository-Snapshot nicht vorhanden. Der Review
  muss diese fehlende Reproduzierbarkeits-Evidence klassifizieren und darf sie
  weder als bestandenen Generator-Gate melden noch innerhalb dieses Features
  nachimplementieren.
- Feature 045 stellt eine aktuelle 157-Zeilen-Selbstpruefung bereit. Feature
  046 muss ihre Evidence-Freshness und Unabhaengigkeit trotzdem pro
  Checkpoint pruefen und darf ihre positiven Aussagen nicht automatisch
  uebernehmen.

### Constitution Requirements

- **CR-001**: Der TuiVision-Level-2-Eintrag ist bindender Kontext: .NET 10/C#,
  MSTest, assembly-spezifische Coverlet-Gates, DocFX sowie Playwright/axe und
  text-first Dokumentationspruefung.
- **CR-002**: NIST SSDF und CWE Top 25 sind immer anwendbar. Alle weiteren
  Standards erhalten eine evidenzbasierte Feature-Disposition ohne stille
  Auslassung.
- **CR-003**: C# ist eine erlaubte MSL. Die MSL-Einstufung ersetzt keine
  sprachspezifische Secure-Coding-, Architektur-, I/O-, Dependency-,
  Supply-Chain- oder Agentenpruefung.
- **CR-004**: Dieses Feature aendert keine Architekturziele, Schnittstellen,
  Runtime, Deployment-Topologie oder Trust Boundary. Neue ADRs,
  Threat-Model-Aenderungen und Zero-Trust-Architekturaenderungen sind fuer die
  Ausfuehrung `N/A`; bestehende Evidence und Luecken bleiben Review-Scope.
- **CR-005**: BSI C3A und C5 werden fuer bestehende Cloud- oder
  Providerabhaengigkeiten geprueft. Ohne neue Cloud-/Deployment-Grenze ist
  Architekturhaertung `N/A`; Provider-Assurance bleibt ohne befugte Evidence
  offen.
- **CR-006**: Das Feature erzeugt kein neues distributables Produktartefakt.
  Bestehende SBOM-, VEX-, SLSA-, Provenance- und Release-Evidence bleibt
  trotzdem Teil des GSDB-Reviews.
- **CR-007**: KI wird in Feature 046 nur als Entwicklungswerkzeug eingesetzt.
  AI-SBOM ist fuer den Feature-Output `N/A`; die in FR-024 genannten
  Produkt-/Runtime-Aenderungen sind der Trigger.
- **CR-008**: Statistikmethodik und gemeinsame Agentenregeln werden nicht
  geaendert. Statistik- und Agenten-Synchronisierung ist deshalb fuer die
  Ausfuehrung `N/A`; beobachtete Drift wird dokumentiert.
- **CR-009**: Die Source-reference disposition ist `N/A`, weil keine
  historische Produktsemantik, Kompatibilitaet oder Modernisierung geaendert
  wird. `tv203s/` und andere historische Quellbaeume werden nicht pauschal
  inventarisiert. Nur eine konkrete GSDB-Frage darf die gezielte read-only
  Konsultation genau benoetigter Dateien ausloesen; geaenderter Produktvertrag,
  aktiver Quellen-Scope oder Auslieferung loest Re-Evaluation aus.

### Documentation Impact Decision

**Decision**: `UpdateRequired` fuer die spaetere GSDB-Review-Evidence. In der
Specify-Phase entstehen ausschliesslich diese Spezifikation und ihre
Requirements-Qualitaetscheckliste.

- **Audiences**: Auszubildende ab dem ersten Lehrjahr, Maintainer,
  Security-Reviewer und Auditoren.
- **Documentation families**: GSDB-Kontrollmatrix, Security-Governance,
  Sprach-/Preset-Bewertung, Feature- und Delivery-Evidence.
- **Reader path**: `docs/security/README.md` zur datierten GSDB-Evidence und
  von dort zu Kontrollmatrix, Quellen, Sprachen, Presets und Human-Grenzen.
- **Canonical source and owner**: Die zwoelf Einzelchecklisten sind die
  kanonische Kontrollquelle; die projektspezifische Bewertung gehoert dem
  TuiVision-Maintainer mit Security-Review.
- **Navigation impact**: Spaeterer Link im Security-Index; keine
  DocFX-Navigation in Specify.
- **Document class**: Oeffentlich nutzbare, nicht zertifizierende
  Review-Evidence.
- **Language strategy and partner**: DE-first/EN-second, CEFR B2; beide
  Sprachspuren werden gemeinsam geprueft.
- **Platform/example proof**: `N/A` fuer Produktverhalten; reine
  Dokumentations-, A11Y- und Security-Gates gelten proportional, wenn die
  spaeter tatsaechlich geaenderten Evidence-Oberflaechen sie ausloesen.
- **Distribution class**: Repository-lokale, sicher publizierbare Markdown-
  und strukturierte Evidence ohne Secrets oder private Pfade.
- **Home-sync need**: `No`; gefundene Policy-Drift wird nicht automatisch
  synchronisiert.
- **Evidence**: `specs/046-gsdb-spec-kit-intensive-review/` und spaetere
  datierte Evidence unter `docs/security/secure-development/`.
- **Re-evaluation trigger**: Aenderung von GSDB-Baseline, Constitution,
  Preset-Registry, Sprache, Runtime-/Produktgrenze, Distribution,
  Dependencies, Agenten-/Sandboxmodell oder regulatorischer Anwendbarkeit.

### Key Entities

- **GSDB Checkpoint**: Eine kanonische Kontrolle oder weitere stabile
  Richtlinien-, Dokument-, Sprach-, Preset-, Constitution- oder Evidence-
  Pruefflaeche mit genau einer Disposition und vollstaendigen Pflichtfeldern.
- **Evidence Reference**: Ein aktueller Repository-Pfad oder eine explizite
  Beweisluecke mit Snapshot- und Aussagegrenze.
- **Source Inventory Entry**: Eine GSDB- oder Governance-Quelle mit Version,
  Rolle, Abdeckung und beobachteter Drift.
- **Language Profile Assessment**: MSL- und Secure-Coding-Anwendbarkeit fuer
  eine im Repository aktive, historische oder nicht vorhandene Sprache.
- **Human-only Boundary**: Eine Entscheidung, die rechtliche,
  organisatorische, Provider-, Secret-, Plattform- oder Freigabeautoritaet
  benoetigt.
- **Review Summary**: Nachpruefbare Statuszahlen, Findings, Restrisiken,
  Follow-ups und Trigger ohne formalen Compliance-Claim.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 157 von 157 kanonischen Kontroll-IDs besitzen genau eine
  Bewertungszeile; fehlende, doppelte und unbekannte IDs stehen jeweils bei
  null.
- **SC-002**: Die Kapitelabdeckung entspricht exakt
  `12/13/15/10/13/11/12/13/17/17/12/12` fuer `CL-01` bis `CL-12`.
- **SC-003**: 100 Prozent aller relevanten Checkpoints verwenden genau eine
  der fuenf erlaubten Dispositionen und enthalten Begruendung, Evidence,
  Owner, Follow-up, Re-Evaluation-Trigger und Restrisiko. Die zusaetzlichen
  Checkpoints stimmen vollstaendig mit den dokumentierten deterministischen
  Inventarregeln und deren Snapshot-Ergebnis ueberein.
- **SC-004**: 100 Prozent der `AlreadySatisfied`-Aussagen verweisen auf
  aktuelle direkte Evidence; fehlende oder nicht tragfaehige Evidence erzeugt
  null positive Claims.
- **SC-005**: 100 Prozent der `N/A`, `Open` und `FollowUp`-Zeilen besitzen
  statusgerechte Begruendung und alle Pflichtfelder. Human-only-Entscheidungen
  ohne befugte Evidence erzeugen null positive Claims. Vollstaendige `Open`-
  und `FollowUp`-Zeilen gelten als akzeptierte wahrheitsgetreue Ergebnisse und
  erzeugen in Feature 046 null neue Intakes, Issues, Branches oder Features.
- **SC-006**: Kernbasis, 12 Einzelchecklisten, Sammelband, alle im
  revalidierten Manifest registrierten Dokumente, alle deterministisch
  gefundenen Lern-/MSL-Referenzen, Constitution-, Preset-, Governance- und
  Repository-Evidence-Oberflaechen sind ohne stille Auslassung im
  Review-Inventar sichtbar; nur die 157 Kontroll-IDs sind als feste
  Ergebniskardinalitaet vorausgesetzt.
- **SC-007**: C#/.NET, Bash, PowerShell, TypeScript/JavaScript, historische
  C/C++-Quellen, SQL und alle weiteren im deterministischen Sprachinventar
  gefundenen Regelprofile besitzen eine sichtbare Profilentscheidung und
  einen Re-Evaluation-Trigger. Historische Quellbaeume bleiben `N/A`, sofern
  keine dokumentierte konkrete GSDB-Frage ihre read-only Konsultation ausloest.
- **SC-008**: Jede bestaetigte Governance- oder Versionsabweichung nennt beide
  Quellen, Auswirkung und vollstaendige Pflichtfelder; null Abweichungen
  werden durch nicht autorisierte Reparaturen verdeckt.
- **SC-009**: Ein text-first Review findet null farb-, bild-, layout- oder
  pointer-only Bedeutungen und null unerlaeuterte zentrale Fachbegriffe.
- **SC-010**: Reviewer koennen eine beliebige Kontrollzeile innerhalb von drei
  Minuten von Quelle ueber Disposition und Evidence bis zu Owner, Risiko,
  Follow-up und Trigger nachvollziehen.
- **SC-011**: Der Feature-Diff enthaelt null Produkt-, Runtime-, API-,
  Dependency-, Paket-, Projekt-, Beispiel-, Workflow-, Provider-, Secret-
  Rotations- oder Repository-Einstellungs-Aenderungen und null automatisch
  erzeugte Folge-Intakes oder Features.
- **SC-012**: Alle durch die spaetere reine Evidence-Lieferung ausgeloesten
  proportionalen Dokumentations-, A11Y- und Security-Gates bestehen. Fuer die
  autorisierte nicht leere `MergeAndSync`-Lieferung konvergieren alle fuer den
  exakten Remote-Head geltenden technischen und Review-Gates; nicht
  ausgefuehrte Plattform-, Provider- oder Human-Gates werden nicht als
  bestanden berichtet.
- **SC-013**: Commit, Push, Pull Request, Merge und Branch-Bereinigung erfolgen
  nur unter aktueller Feature-046-Run-Autoritaet fuer dieses Repository. Ein
  Admin-Bypass kommt nur bei gruenen technischen Gates, null actionable
  Review-Threads und Human Approval als einziger offener Regel zum Einsatz;
  alle anderen Bypass-, Provider-/Organisations-, Secret-, formalen Freigabe-
  und repositoryfremden Remote-Aktionen stehen bei null.

## Assumptions

- Binding Intake, akzeptierter Review und autonomer Run-State bleiben waehrend
  der jeweiligen Planungsphase inhaltlich und hashbezogen gueltig; materielle
  Drift blockiert die Phasenfreigabe.
- Die zwoelf Einzelchecklisten sind die kanonische ID-Quelle. Manifest,
  Richtlinie und Sammelband sind wichtige Vergleichsquellen und koennen selbst
  Drift enthalten.
- Feature 045 ist aktuelle, umfangreiche Eingangsevidenz, aber Feature 046
  bleibt eine unabhaengige Intensivpruefung und uebernimmt keine Disposition
  ohne erneute Evidence-Freshness-Pruefung.
- TuiVision bleibt ein .NET-10-/C#-Terminal-UI-Framework ohne neue Web-, API-,
  Auth-, Cloud-, Datenbank-, Runtime-KI- oder Providergrenze.
- Owner werden als verantwortliche Rollen benannt. Persoenliche oder externe
  Freigaben werden nur mit vorhandener, sicher publizierbarer Evidence
  behauptet.
- `None` in einem Follow-up ist ein ausdruecklicher Wert mit Begruendung und
  niemals ein leeres Pflichtfeld.
- Die aktuelle Benutzeranweisung und der aktive Run-State autorisieren
  `MergeAndSync` fuer Feature 046. Diese Delivery-Autoritaet erweitert weder
  den evidence-only Fachscope noch die Berechtigung fuer Produkt-, Provider-
  oder Folgearbeiten.

## Dependencies

- Binding Intake, akzeptierter `Ready`-Serienreview und Feature-046-Run-State
  muessen lesbar und konsistent sein.
- GSDB-Quellinventar, beide Constitutions, Preset-Registry und bestehende
  Security-/Architecture-/A11Y-/CI-/Test-Evidence muessen fuer den read-only
  Vergleich verfuegbar sein.
- Fehlende externe, Provider-, Plattform- oder Human-Evidence ist eine
  dokumentierte Proof-Grenze und kein Grund, Fakten zu erfinden.

## Non-Goals

- Keine Produkt-, Runtime-, Public-API-, Dependency-, Paket-, Projekt-,
  Beispiel-, Workflow- oder Produktverhaltensaenderung.
- Keine Security-, Governance-, Architektur-, Preset-, Constitution-,
  Agenten-, Evidence- oder Repository-Haertung innerhalb des Reviews.
- Keine Korrektur von Richtlinie, Manifest, Sammelband, Checklisten,
  mitgeltenden Dokumenten oder bestehenden Evidence-Dateien allein aufgrund
  einer Beobachtung.
- Keine Provider-, Organisations-, Branch-Protection-, Secret-Rotations-,
  Modell-, Sandbox-Image-, Remote- oder Repository-Konfiguration.
- Keine echten Kunden-/Produktivdaten, Credentials, Tokens, Sessions oder
  privaten lokalen Pfade als Evidence.
- Keine Git- oder Remote-Aktion ausserhalb der autorisierten nicht leeren
  Feature-046-Lieferung dieses Repositorys; kein technischer Gate-Bypass und
  kein Admin-Bypass ausserhalb der engen Human-Approval-Bedingung.
- Keine automatische Intake-, Issue-, Branch-, Follow-up- oder Feature-
  Erzeugung aufgrund eines Findings.
- Keine Rechtsberatung, formale Freigabe, Zertifizierung, QISMS- oder
  Compliance-Behauptung.
