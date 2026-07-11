<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 11 – Datenschutz-Folgenabschaetzung (DSGVO Art. 35) / Data Protection Impact Assessment (GDPR Art. 35)

**Dokument-ID / Document ID:** CL-11
**Version / Version:** 2.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Dokumenttyp / Document type:** Kanonische, wiederverwendbare Pruefvorlage / Canonical reusable review template

### Nachweisinstanz / Evidence Instance

Diese Datei ist eine wiederverwendbare Vorlage. Ausgefüllte Projektnachweise werden unter `docs/security/secure-development/<datum>-<scope>/` abgelegt und nennen Projekt, Scope, Prüfdatum, Baseline-Version, verantwortliche Person und Reviewer. / This file is a reusable template. Completed project evidence is stored under `docs/security/secure-development/<date>-<scope>/` and names project, scope, review date, baseline version, responsible person, and reviewer.

### Zweck / Purpose

**DE:** Diese Checkliste sichert eine vollständige, nachvollziehbare
Datenschutz-Folgenabschätzung (Data Protection Impact Assessment, DPIA)
gemäß DSGVO Art. 35 für Neuentwicklungen und wesentliche Änderungen, die
personenbezogene Daten (pbD) verarbeiten. Sie ist mitgeltend zur
Richtlinie „Sichere Entwicklung" und zur Datenschutzleitlinie der Organisation.

**EN:** This checklist ensures a complete, traceable Data Protection
Impact Assessment (DPIA) per GDPR Art. 35 for new developments and
material changes processing personal data. It accompanies the guideline
"Secure Development" and the data-protection guideline.

### Geltungsbereich / Scope

**DE:** Pflicht für jedes Vorhaben, das personenbezogene Daten (pbD) im
Sinne der Datenschutz-Grundverordnung (DSGVO, Verordnung (EU) 2016/679)
verarbeitet. Die Schwellwertanalyse (Abschnitt 1) ist immer
durchzuführen. Liegt ein voraussichtlich hohes Risiko vor, sind alle
weiteren Abschnitte verpflichtend. Aktualisierung bei wesentlichen
Änderungen (neue Datenkategorien, neue Empfänger, neue Drittlands-
übermittlung, neue Technologie, neuer Zweck).

**EN:** Mandatory for every project that processes personal data under
the General Data Protection Regulation (GDPR, Regulation (EU) 2016/679).
The threshold analysis (section 1) is always required. If a high risk
is likely, all further sections are mandatory. Update on material
changes (new data categories, new recipients, new third-country
transfers, new technology, new purpose).

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung
- Datenschutzleitlinie der Organisation
- DSGVO (Verordnung (EU) 2016/679), Art. 5, 25, 28, 30, 32, 35, 36
- ISO/IEC 27701:2019 Privacy Information Management
- ISO/IEC 29134:2017 Guidelines for Privacy Impact Assessment
- Bundesdatenschutzgesetz (BDSG) und Niedersächsisches Datenschutzgesetz
  (NDSG), soweit anwendbar
- WP 248 rev.01 — Leitlinien zur Datenschutz-Folgenabschätzung (Art. 29-
  Datenschutzgruppe, vom EDSA bestätigt)
- CL_Bedrohungsmodellierung (Verbindung Bedrohungsmodell ↔ DPIA)

#### URL-/Ablageverweise / URLs and Storage Locations

**DE:** Diese Links helfen beim Review. Projekt- oder organisationsinterne Dokumente koennen als lokale Arbeitskopie oder als Verweis auf den festgelegten Nachweisspeicher ergaenzt werden. Wenn ein interner Nachweisspeicher existiert, wird der Verweis in der finalen Dokumentenlenkung ergänzt.

**EN:** These links help during reviews. Project or organization-internal documents can be added as local working copies or references to the defined evidence location. If an internal evidence location exists, the reference is added during final document control.

- **Richtlinie Sichere Entwicklung / Secure Development Guideline:** [lokale Arbeitsfassung in diesem Repository / local working copy in this repository](../Richtlinie_Sichere-Entwicklung.md)
- **Checklisten-Index / Checklist index:** [Übersicht aller Checklisten / overview of all checklists](../README.md)
- **CL_Bedrohungsmodellierung:** [lokale Arbeitskopie der Checkliste Bedrohungsmodellierung / local working copy of the threat-modeling checklist](CL_04_Bedrohungsmodellierung.md)
- **DSGVO (EUR-Lex):** [Verordnung (EU) 2016/679 / Regulation (EU) 2016/679](https://eur-lex.europa.eu/eli/reg/2016/679/oj)
- **EDSA / EDPB:** [Europäischer Datenschutzausschuss / European Data Protection Board](https://edpb.europa.eu/)
- **WP 248 rev.01 (DPIA-Leitlinien):** [WP 248 rev.01 Leitlinien zur Datenschutz-Folgenabschätzung / WP 248 rev.01 Guidelines on DPIA](https://ec.europa.eu/newsroom/article29/items/611236)
- **Zuständige Datenschutzaufsichtsbehörde / Competent supervisory authority:** projektspezifisch bestimmen; relevante nationale oder regionale DPIA-Listen im Projekt verlinken. / Determine per project; link relevant national or regional DPIA lists in the project.
- **CNIL PIA-Tool (Open Source):** [CNIL Open-Source Privacy-Impact-Assessment-Tool / CNIL open-source PIA tool](https://www.cnil.fr/en/open-source-pia-software-helps-carry-out-data-protection-impact-assessment)
- **ENISA Empfehlungen Datenschutz und Sicherheit:** [ENISA Datenschutz- und Sicherheitsleitfäden / ENISA privacy and security guidelines](https://www.enisa.europa.eu/topics/data-protection)
- **ISO/IEC 27701:2019 (Webseite):** [offizielle ISO-Webseite zu ISO/IEC 27701 / official ISO webpage for ISO/IEC 27701](https://www.iso.org/standard/71670.html)
- **ISO/IEC 29134:2017 (Webseite):** [offizielle ISO-Webseite zu ISO/IEC 29134 / official ISO webpage for ISO/IEC 29134](https://www.iso.org/standard/62289.html)

### Bewertung und Dokumentation / Assessment and Documentation

**DE:** Jeder Prüfpunkt bekommt genau einen Wert je Statusachse. Schreibe die
Begründung so, dass eine neue Kollegin oder ein neuer Kollege den
Entscheid später ohne Rückfrage versteht.

**EN:** Each checklist item gets exactly one value per status axis. Write the
explanation so that a new team member can understand the decision
later without asking again.

- **Erfüllt / Fulfilled:** Die Anforderung ist umgesetzt und es gibt
  einen prüfbaren Nachweis.
- **Nicht erfüllt / Not fulfilled:** Die Anforderung ist noch nicht
  umgesetzt oder der Nachweis fehlt. Es muss eine Aufgabe mit
  Verantwortlicher Person und Termin geben.
- **Nicht anwendbar / Not applicable:** Die Anforderung passt nicht zum
  Projekt. Das ist erlaubt, aber nur mit kurzer Begründung.

**Pflichtfelder je Prüfpunkt / Required fields per item:** Anwendbarkeit, Umsetzungsstatus, Lernstufe, verantwortliche Rolle, Begründung, Evidenzpfad oder Link, Restrisiko, Neubewertungs-Trigger und nächste Maßnahme mit Zieltermin.
Begründung, Evidenzpfad oder Link, nächste Maßnahme mit Verantwortlicher
Person und Zieltermin.

### Durchführungshinweise / Implementation Guidance

**DE:** Nutze diese Checkliste nicht als reine Ja/Nein-Liste. Sie ist
ein Arbeits- und Auditdokument. Prüfe jeden Punkt gegen reale
Artefakte: Verarbeitungsverzeichnis-Eintrag, DPIA-Bericht, Stellungnahme
der oder des Datenschutzbeauftragten, Auftragsverarbeitungsvertrag,
Datenflussdiagramm, Architektur-Entscheidung (S-ADR), Bedrohungsmodell
oder Freigabeprotokoll. Wenn ein Nachweis noch fehlt, markiere den Punkt
als „nicht erfüllt" und lege eine konkrete Folgeaufgabe an.

**EN:** Do not use this checklist as a simple yes/no list. It is a
working and audit document. Check each item against real artefacts:
record of processing activity, DPIA report, opinion of the data
protection officer, processor agreement, data flow diagram,
architecture decision (S-ADR), threat model, or approval record. If
evidence is missing, mark the item as "not fulfilled" and create a
concrete follow-up task.

**DE:** Schreibe kurze, klare Begründungen. Vermeide Abkürzungen ohne
Erklärung. Wenn ein Punkt rechtlich oder fachlich schwierig ist,
beschreibe den aktuellen Stand, das Risiko und den nächsten machbaren
Schritt.

**EN:** Write short and clear explanations. Avoid unexplained
abbreviations. If an item is legally or technically difficult, describe
the current state, the risk, and the next feasible step.

**DE:** Die DPIA wird zusammen mit dem Bedrohungsmodell geführt: Die
DPIA betrachtet Risiken aus Sicht der Betroffenen, das Bedrohungsmodell
betrachtet Risiken aus Sicht der Organisation und ihrer Schutzziele. Beide
Sichten werden konsistent gehalten und gemeinsam aktualisiert.

**EN:** The DPIA runs alongside the threat model: the DPIA looks at
risks from the data subject's perspective, the threat model looks at
risks from the Organisation perspective and its protection goals. Both views
are kept consistent and updated together.

**DE:** Jeder Prüfpunkt muss deshalb drei Fragen beantworten: Was bedeutet die Anforderung im Projektalltag? Was ist konkret zu tun oder zu entscheiden? Welcher Nachweis zeigt das Ergebnis? Verwende Standard-IDs, Toolnamen und Abkürzungen nur zusammen mit einer kurzen Erklärung in Alltagssprache. Wenn ein Punkt für Auszubildende oder neue Teammitglieder nicht selbsterklärend ist, ergänze eine kurze Erklärung in der Begründung.

**EN:** Each item must therefore answer three questions: What does the requirement mean in daily project work? What exactly must be done or decided? Which evidence shows the result? Use standard IDs, tool names, and abbreviations only together with a short plain-language explanation. If an item is not self-explanatory for apprentices or new team members, add a short explanation in the rationale.

### Beispiel / Example

**DE:** Eine neue Web-Anwendung verwaltet Schulungsanmeldungen für
Mitarbeitende externer Forschungseinrichtungen. Sie verarbeitet Name, dienstliche
E-Mail-Adresse, Institutszugehörigkeit, optional Allergiehinweise.
Die Schwellwertanalyse zeigt: keine besonderen Kategorien gemäß Art. 9
DSGVO im Pflichtteil, optionale Allergiehinweise (Gesundheitsdaten)
sind sensibel; insgesamt mittleres Risiko, aber wegen besonderer
Kategorien wird eine DPIA erstellt. Die DPIA dokumentiert:
Verarbeitungszweck (Schulungsorganisation), Rechtsgrundlage
(Art. 6 Abs. 1 lit. b DSGVO für Vertragsdurchführung, Art. 9 Abs. 2
lit. a für Einwilligung zu Allergiehinweisen), Datenkategorien,
Empfänger (Schulungsleitung, Catering), Speicherfristen, Maßnahmen
(Verschlüsselung at-rest und in-transit, getrennte Speicherung der
Allergiehinweise, Pseudonymisierung im Catering-Export, Löschung 90
Tage nach Schulung). Konsultation der oder des Datenschutzbeauftragten
erfolgt vor Go-live.

**EN:** A new web application manages training registrations for staff
from external research institutes. It processes name, work email, institute
affiliation, optionally allergy notes. The threshold analysis shows: no
special categories per Art. 9 GDPR in the mandatory part, optional
allergy notes (health data) are sensitive; overall medium risk but
because of special categories a DPIA is performed. The DPIA documents:
purpose (training organisation), legal basis (Art. 6(1)(b) GDPR for
contract performance, Art. 9(2)(a) for consent to allergy notes), data
categories, recipients (training lead, catering), retention periods,
measures (encryption at-rest and in-transit, separate storage for
allergy notes, pseudonymisation in the catering export, deletion 90
days after the training). The data protection officer is consulted
before go-live.

### A11Y-Hinweise / A11Y Notes

**DE:** Beim Ausfüllen dieser Checkliste müssen alle Nachweise auch
textlich verständlich sein. Verweise sollen beschreibende Linktexte
haben. Datenflussdiagramme und Risikomatrizen brauchen eine kurze
Textbeschreibung. Der Status darf nicht nur über Farbe erkennbar sein.

**EN:** When this checklist is filled in, all evidence must also be
understandable as text. References should use descriptive link text.
Data flow diagrams and risk matrices need a short text description. The
status must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [DSGVO / GDPR](#cl-11-glossar-dsgvo-gdpr)
- [DPIA / DSFA](#cl-11-glossar-dpia-dsfa)
- [Personenbezogene Daten / Personal Data](#cl-11-glossar-personal-data)
- [Verarbeitung / Processing](#cl-11-glossar-processing)
- [Betroffene Person / Data Subject](#cl-11-glossar-data-subject)
- [Risikobewertung / Risk Rating](#cl-11-glossar-risk-rating)
- [Maßnahme / Mitigation](#cl-11-glossar-mitigation)
- [DSB / DPO](#cl-11-glossar-dpo-dsb)
- [Aufsichtsbehörde / Supervisory Authority](#cl-11-glossar-supervisory-authority)
- [Verzeichnis der Verarbeitungstätigkeiten / RoPA](#cl-11-glossar-ropa)
- [Auftragsverarbeitung / DPA](#cl-11-glossar-dpa-avv)
- [Datenschutz durch Technikgestaltung / Privacy by Design](#cl-11-glossar-privacy-by-design)
- [Betroffenenrechte / Data Subject Rights](#cl-11-glossar-data-subject-rights)
- [Evidenz / Evidence](#cl-11-glossar-evidenz)

### Checkliste / Checklist

#### CL-11-01: Schwellwertanalyse / Threshold Analysis

- **DE:** Zu Projektbeginn wird eine dokumentierte Schwellwertanalyse
  durchgeführt. Sie prüft die in DSGVO Art. 35 Abs. 3 genannten Fälle:
  systematische und umfassende Bewertung persönlicher Aspekte
  einschließlich Profiling, umfangreiche Verarbeitung besonderer
  Kategorien gemäß Art. 9 (Gesundheits-, biometrische,
  Gewerkschaftsdaten u. a.) oder von Art. 10 (strafrechtliche
  Verurteilungen), systematische Überwachung öffentlich zugänglicher
  Bereiche. Zusätzlich wird die Positivliste der zuständigen
  Aufsichtsbehörde geprüft (im jeweiligen Projekt- oder Sitzland: zuständige Datenschutzaufsichtsbehörde). Die
  Analyse berücksichtigt die WP-248-rev.01-Kriterien (neun Kriterien
  des EDSA): Bewerten/Scoring, automatisierte Entscheidung mit
  rechtlicher Wirkung, systematische Überwachung, sensible Daten oder
  hochpersönliche Daten, umfangreiche Verarbeitung, Abgleich oder
  Verkettung von Datensätzen, schutzbedürftige Personen,
  innovative Nutzung neuer Technologien, Verarbeitungen, die
  Betroffene an der Wahrnehmung von Rechten oder der Nutzung von
  Diensten hindern. Werden zwei oder mehr dieser Kriterien erfüllt,
  ist in der Regel eine DPIA durchzuführen.
- **EN:** At project start a documented threshold analysis is
  performed. It checks the cases listed in GDPR Art. 35(3): systematic
  and extensive evaluation of personal aspects including profiling,
  large-scale processing of special categories per Art. 9 (health,
  biometric, union data, etc.) or Art. 10 (criminal convictions),
  systematic monitoring of publicly accessible areas. The positive
  list of the competent supervisory authority in the relevant project or seat jurisdiction is also reviewed. The analysis considers the WP 248
  rev.01 criteria (nine EDPB criteria): evaluation/scoring, automated
  decision-making with legal effect, systematic monitoring, sensitive
  or highly personal data, large-scale processing, matching or
  combining datasets, vulnerable subjects, innovative use of new
  technologies, processing preventing subjects from exercising rights
  or using a service. Two or more criteria typically trigger a DPIA.
- **Akzeptanz / Acceptance:** Schwellwertanalyse als datiertes
  Dokument im Sicherheits- oder Datenschutz-Dokumentenbestand des
  Projekts; klares Ergebnis (DPIA erforderlich / nicht erforderlich)
  mit Begründung. / Threshold analysis as a dated document in the
  project's security or data-protection document set; clear outcome
  (DPIA required / not required) with justification.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, Schwellwertanalyse-Dokument oder Stellungnahme nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, threshold analysis document, or opinion. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-11-02: Systematische Beschreibung der Verarbeitung / Systematic Description of Processing

- **DE:** Die DPIA enthält eine systematische Beschreibung der geplanten
  Verarbeitungsvorgänge gemäß DSGVO Art. 35 Abs. 7 lit. a. Mindestinhalte:
  Verantwortliche\*r und gegebenenfalls gemeinsam Verantwortliche
  (Art. 26), Zwecke der Verarbeitung einschließlich der berechtigten
  Interessen (sofern Art. 6 Abs. 1 lit. f), Kategorien personenbezogener
  Daten und Kategorien Betroffener, Empfänger oder Empfängerkategorien
  (auch in Drittländer), geplante Speicherfristen, beteiligte Systeme
  und Schnittstellen, Datenflüsse mit Trust Boundaries (Verbindung zur
  Bedrohungsmodellierung), eingesetzte Technologien (insbesondere
  innovative oder neuartige Technologien wie KI/Machine Learning,
  biometrische Verfahren, Tracking). Die Beschreibung wird mit dem
  Eintrag im Verzeichnis von Verarbeitungstätigkeiten (Art. 30)
  konsistent gehalten.
- **EN:** The DPIA contains a systematic description of the planned
  processing per GDPR Art. 35(7)(a). Minimum content: controller and
  any joint controllers (Art. 26), purposes of processing including
  legitimate interests (if Art. 6(1)(f)), categories of personal data
  and data subjects, recipients or categories of recipients (including
  third countries), planned retention periods, systems and interfaces
  involved, data flows with trust boundaries (link to threat
  modeling), technologies used (especially innovative or novel
  technologies such as AI/machine learning, biometrics, tracking).
  The description is kept consistent with the record of processing
  activities (Art. 30).
- **Akzeptanz / Acceptance:** Vollständige Beschreibung mit allen
  genannten Mindestinhalten; Querverweis auf Verarbeitungsverzeichnis
  und auf Datenflussdiagramm. / Complete description with all listed
  minimum content; cross-reference to record of processing and data
  flow diagram.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, DPIA-Bericht oder Verzeichnis-Eintrag nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, DPIA report, or RoPA entry. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-11-03: Notwendigkeits- und Verhältnismäßigkeitsbewertung / Necessity and Proportionality Assessment

- **DE:** Bewertung der Notwendigkeit und Verhältnismäßigkeit der
  Verarbeitung gemäß DSGVO Art. 35 Abs. 7 lit. b. Geprüft werden:
  Rechtsgrundlage gemäß Art. 6 (und ggf. Art. 9, 10), Spezifizität und
  Eindeutigkeit der Zwecke (Zweckbindung, Art. 5 Abs. 1 lit. b),
  Datenminimierung (Art. 5 Abs. 1 lit. c), Richtigkeit (Art. 5 Abs. 1
  lit. d), Speicherbegrenzung mit konkreten Löschfristen (Art. 5
  Abs. 1 lit. e), Information der Betroffenen (Art. 13/14),
  Maßnahmen zur Wahrung der Betroffenenrechte (Art. 12–22),
  internationale Übermittlungen (Art. 44 ff., Standardvertragsklauseln,
  Angemessenheitsbeschluss). Wenn weniger eingriffsintensive
  Alternativen möglich sind (zum Beispiel Pseudonymisierung,
  Aggregation, lokale Verarbeitung statt Cloud-Übermittlung), werden
  sie bewertet und das Ergebnis dokumentiert.
- **EN:** Assessment of necessity and proportionality per GDPR Art.
  35(7)(b). Reviews: legal basis per Art. 6 (and Art. 9, 10 where
  applicable), specificity and clarity of purposes (purpose limitation,
  Art. 5(1)(b)), data minimisation (Art. 5(1)(c)), accuracy
  (Art. 5(1)(d)), storage limitation with concrete deletion periods
  (Art. 5(1)(e)), information to data subjects (Art. 13/14), measures
  to safeguard data subject rights (Art. 12–22), international
  transfers (Art. 44 ff., SCCs, adequacy decision). If less intrusive
  alternatives are possible (for example pseudonymisation, aggregation,
  local processing instead of cloud transfer), they are assessed and
  the outcome is documented.
- **Akzeptanz / Acceptance:** Dokumentierte Bewertung mit Rechts-
  grundlage, Zwecken, Datenminimierungs-Argument und Löschfristen je
  Datenkategorie. / Documented assessment with legal basis, purposes,
  data-minimisation argument, and deletion periods per data category.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, DPIA-Bericht oder Rechtsmemorandum nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, DPIA report, or legal memo. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-11-04: Risikobewertung für Betroffene / Risk Assessment for Data Subjects

- **DE:** Bewertung der Risiken für die Rechte und Freiheiten
  natürlicher Personen gemäß DSGVO Art. 35 Abs. 7 lit. c. Für jeden
  Verarbeitungsschritt werden die typischen Datenschutzrisiken
  betrachtet: unrechtmäßiger Zugriff (Vertraulichkeitsverlust),
  unbefugte Veränderung (Integritätsverlust), Datenverlust oder
  Nichtverfügbarkeit (Verfügbarkeitsverlust). Folgen für Betroffene
  werden konkret beschrieben (Diskriminierung, Identitätsdiebstahl,
  finanzielle Verluste, Rufschädigung, Verlust der Kontrolle über
  eigene Daten, Beeinträchtigung der Wahrnehmung von Rechten).
  Eintrittswahrscheinlichkeit und Schwere werden in einer Risikomatrix
  klassifiziert (zum Beispiel niedrig, mittel, hoch). Die Bewertung
  greift auf das Bedrohungsmodell zurück und ergänzt es um die
  Betroffenen-Sicht. Werkzeuge: CNIL PIA-Tool, ISO/IEC 29134-konforme
  Vorlagen, ENISA-Methodik.
- **EN:** Assessment of risks to the rights and freedoms of natural
  persons per GDPR Art. 35(7)(c). For each processing step the typical
  privacy risks are considered: unlawful access (loss of
  confidentiality), unauthorised alteration (loss of integrity), loss
  or unavailability of data (loss of availability). Consequences for
  data subjects are described concretely (discrimination, identity
  theft, financial loss, reputational harm, loss of control over their
  own data, impairment of exercising rights). Likelihood and severity
  are classified in a risk matrix (for example low, medium, high). The
  assessment draws on the threat model and adds the data-subject
  perspective. Tooling: CNIL PIA tool, templates aligned with ISO/IEC
  29134, ENISA methodology.
- **Akzeptanz / Acceptance:** Vollständige Risikomatrix je Verarbeitung
  mit Eintrittswahrscheinlichkeit, Schwere, betroffenem Schutzziel und
  konkreter Folgebeschreibung. / Complete risk matrix per processing
  step with likelihood, severity, affected protection goal, and
  concrete consequence description.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, Risikomatrix oder DPIA-Bericht nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, risk matrix, or DPIA report. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-11-05: Geplante Abhilfemaßnahmen / Planned Mitigation Measures

- **DE:** Zu jedem identifizierten Risiko werden geplante Abhilfe-
  maßnahmen gemäß DSGVO Art. 35 Abs. 7 lit. d dokumentiert. Sie
  umfassen technische und organisatorische Maßnahmen (TOM) gemäß
  Art. 32 sowie spezifische datenschutzfreundliche Maßnahmen (Privacy
  Enhancing Technologies, PET): Verschlüsselung at-rest und in-transit,
  Pseudonymisierung, Anonymisierung, k-Anonymität, differential
  privacy, Zugriffskontrolle nach Least Privilege, Protokollierung,
  Auftragsverarbeitungsverträge gemäß Art. 28, Maßnahmen zur Wahrung
  der Betroffenenrechte (Auskunfts-, Berichtigungs-, Löschungs-,
  Datenübertragbarkeits-Schnittstellen), Schulung der beteiligten
  Personen. Restrisiken werden bewertet und mit Risikoeigner\*in
  versehen. Ist das Restrisiko trotz Maßnahmen weiterhin hoch, ist die
  Aufsichtsbehörde gemäß Art. 36 vor Verarbeitungsbeginn zu
  konsultieren.
- **EN:** For every identified risk, planned mitigation measures are
  documented per GDPR Art. 35(7)(d). They cover technical and
  organisational measures (TOM) per Art. 32 plus specific privacy-
  enhancing technologies (PET): encryption at-rest and in-transit,
  pseudonymisation, anonymisation, k-anonymity, differential privacy,
  least-privilege access control, logging, processor agreements per
  Art. 28, data-subject-rights mechanisms (access, rectification,
  erasure, portability), training of the personnel involved. Residual
  risks are assessed and assigned a risk owner. If the residual risk
  remains high despite the measures, the supervisory authority must be
  consulted before processing starts (Art. 36).
- **Akzeptanz / Acceptance:** Maßnahmenliste mit Zuordnung zum Risiko,
  Verantwortlicher Person, Umsetzungsstand und Restrisiko-Bewertung;
  Verbindung zu S-ADR oder Bedrohungsmodell-Maßnahmen. / Mitigation
  list mapped to risks, with owner, implementation status, and
  residual-risk rating; linked to S-ADR or threat-model mitigations.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, Maßnahmenliste oder S-ADR nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, mitigation list, or S-ADR. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-11-06: Konsultation der oder des Datenschutzbeauftragten / Consultation of the Data Protection Officer

- **DE:** Die DPIA wird gemäß DSGVO Art. 35 Abs. 2 unter Beteiligung
  der Datenschutzrolle erstellt. Die
  Stellungnahme der oder des DSB wird in die DPIA-Dokumentation
  aufgenommen. Sie enthält mindestens: Beurteilung der DPIA-Methodik,
  Beurteilung der Vollständigkeit, Empfehlungen zu Maßnahmen,
  Hinweise auf weitergehenden Beratungs- oder Konsultationsbedarf,
  abschließende Bewertung (zum Beispiel keine Bedenken, Bedenken mit
  Auflagen, erhebliche Bedenken). Wenn die Empfehlung der oder des DSB
  nicht oder nicht vollständig umgesetzt wird, wird dies begründet
  dokumentiert.
- **EN:** The DPIA is prepared with involvement of the Organisation data
  protection officer (DPO) per GDPR Art. 35(2). The DPO opinion is
  included in the DPIA documentation. It contains at least: assessment
  of DPIA methodology, completeness assessment, recommendations on
  measures, indication of any further advice or consultation needs,
  final assessment (for example no concerns, concerns with conditions,
  significant concerns). If the DPO recommendation is not or not fully
  implemented, the reasons are documented.
- **Akzeptanz / Acceptance:** Datierte und unterschriebene Stellungnahme
  der oder des DSB als Anlage zur DPIA; Antwortdokument bei
  Abweichungen. / Dated and signed DPO opinion attached to the DPIA;
  response document where there are deviations.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, DSB-Stellungnahme nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, DPO opinion. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-11-07: Konsultation der Aufsichtsbehörde / Consultation of the Supervisory Authority

- **DE:** Wenn die DPIA trotz geplanter Maßnahmen ein verbleibendes
  hohes Risiko ergibt, wird die zuständige Aufsichtsbehörde gemäß
  DSGVO Art. 36 vor Verarbeitungsbeginn konsultiert. Für die Organisation ist
  dies die zuständige Datenschutzaufsichtsbehörde des Projekt- oder Sitzlandes. Die Konsultation umfasst die DPIA, die Beschreibung
  der Verarbeitung, die Aufgabenverteilung zwischen Verantwortlichen
  und Auftragsverarbeitern, geplante Schutzmaßnahmen und alle
  weiteren von der Behörde angeforderten Informationen. Die Antwort
  der Aufsichtsbehörde wird in der DPIA-Dokumentation abgelegt; die
  Verarbeitung beginnt erst nach Klärung.
- **EN:** If the DPIA shows residual high risk despite planned measures,
  the competent supervisory authority is consulted before processing
  starts per GDPR Art. 36. For the organisation this is the competent data protection supervisory authority of the project or seat jurisdiction. The consultation
  includes the DPIA, the processing description, the allocation of
  responsibilities between controllers and processors, planned
  protective measures, and any further information requested by the
  authority. The authority's response is filed with the DPIA
  documentation; processing only begins after the matter is settled.
- **Akzeptanz / Acceptance:** Konsultationsanfrage und Antwort der
  Aufsichtsbehörde dokumentiert; Verarbeitung erst nach abgeschlossener
  Konsultation aufgenommen. / Consultation request and authority
  response documented; processing only started after the consultation
  is closed.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, Schriftverkehr mit Aufsichtsbehörde nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, correspondence with the authority. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-11-08: Verzeichnis der Verarbeitungstätigkeiten / Record of Processing Activities

- **DE:** Jede Verarbeitung personenbezogener Daten wird im zentralen
  Verzeichnis der Verarbeitungstätigkeiten (Records of Processing
  Activities, RoPA) der Organisation gemäß DSGVO Art. 30 geführt.
  Mindestinhalte: Name und Kontaktdaten der verantwortlichen Stelle,
  Zwecke der Verarbeitung, Kategorien Betroffener, Kategorien
  personenbezogener Daten, Empfängerkategorien, Drittlandsübermittlungen
  einschließlich Garantien, geplante Löschfristen, allgemeine
  Beschreibung der TOM gemäß Art. 32. Die DPIA ist mit dem RoPA-Eintrag
  verknüpft, sodass die Auditspur eindeutig ist. Werkzeuge: projektspezifisches
  internes RoPA-System; alternativ DSGVO-konforme Drittsysteme; in
  Spezialfällen tabellarische Vorlagen mit Versionierung im definierten Nachweisspeicher.
- **EN:** Every processing of personal data is registered in the Organisation
  central record of processing activities (RoPA) per GDPR Art. 30.
  Minimum content: name and contact details of the controller,
  purposes of processing, categories of data subjects, categories of
  personal data, categories of recipients, third-country transfers
  with safeguards, planned deletion periods, general description of
  technical and organisational measures per Art. 32. The DPIA is
  linked to the RoPA entry to keep the audit trail unambiguous.
  Tooling: project-specific internal RoPA system; otherwise GDPR-compliant
  third-party systems; in special cases tabular templates with
  versioning in the defined evidence location.
- **Akzeptanz / Acceptance:** RoPA-Eintrag mit allen Mindestinhalten
  und Verknüpfung zur DPIA; mindestens jährliche Überprüfung. / RoPA
  entry with all minimum content and link to the DPIA; at least
  annual review.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, RoPA-Eintrag nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, RoPA entry. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-11-09: Auftragsverarbeitung / Processor Agreements

- **DE:** Werden externe Auftragsverarbeiter eingebunden (zum Beispiel
  Cloud-Dienste, externe Entwicklungspartner, Hosting-Provider,
  Logging-Dienste, Mailversand), liegt vor Verarbeitungsbeginn ein
  Auftragsverarbeitungsvertrag (Data Processing Agreement, DPA) gemäß
  DSGVO Art. 28 Abs. 3 vor. Mindestinhalte: Gegenstand und Dauer der
  Verarbeitung, Art und Zweck, Datenkategorien, Pflichten und Rechte
  des Verantwortlichen, Vertraulichkeit, Sicherheitsmaßnahmen gemäß
  Art. 32, Sub-Auftragsverarbeiter (mit Genehmigungsregelung),
  Unterstützung bei Betroffenenrechten, Unterstützung bei Sicherheits-
  und Meldepflichten gemäß Art. 32–36, Löschung oder Rückgabe nach
  Vertragsende, Auditrechte. Bei Drittlandsübermittlungen werden
  geeignete Garantien sichergestellt (Angemessenheitsbeschluss,
  Standardvertragsklauseln plus Transfer Impact Assessment, BCR).
- **EN:** When external processors are involved (for example cloud
  services, external development partners, hosting providers, logging
  services, mail dispatch), a data processing agreement (DPA) per GDPR
  Art. 28(3) is in place before processing starts. Minimum content:
  subject matter and duration of processing, nature and purpose,
  categories of data, controller's obligations and rights,
  confidentiality, security measures per Art. 32, sub-processors (with
  authorisation regime), assistance with data-subject rights,
  assistance with security and notification duties per Art. 32–36,
  deletion or return after contract end, audit rights. For third-
  country transfers, appropriate safeguards are ensured (adequacy
  decision, SCCs plus Transfer Impact Assessment, BCRs).
- **Akzeptanz / Acceptance:** Unterschriebener DPA je Auftrags-
  verarbeiter; Liste der Auftragsverarbeiter und Sub-Auftragsverarbeiter
  aktuell und im definierten Nachweisspeicher abgelegt. / Signed DPA per processor; list of
  processors and sub-processors current and stored in the defined evidence location.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, DPA-Dokument nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, DPA document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-11-10: Datenschutz durch Technikgestaltung und Voreinstellungen / Privacy by Design and by Default

- **DE:** Die Verarbeitung wird von Beginn an datenschutzfreundlich
  gestaltet (DSGVO Art. 25 Abs. 1, Privacy by Design) und mit
  datenschutzfreundlichen Voreinstellungen ausgeliefert (Art. 25 Abs. 2,
  Privacy by Default). Konkrete Anforderungen: Datenminimierung in
  Eingaben und Speicherung, Pseudonymisierung wo möglich, kürzeste
  zweckdienliche Speicherfristen als Default, Zugriff nur für die
  benötigten Rollen, datenschutzkonforme Logging-Strategie, klare
  Trennung zwischen Stamm-, Verkehrs- und Inhaltsdaten,
  altersangemessene Voreinstellungen wo Minderjährige betroffen sind.
  Die acht Architekturprinzipien aus dem Abschnitt „Sichere
  Softwarearchitektur" der Richtlinie Sichere Entwicklung werden um diese
  datenschutzspezifischen Punkte ergänzt; relevante Entscheidungen
  werden als S-ADR mit Datenschutz-Bezug festgehalten.
- **EN:** Processing is designed in a privacy-friendly way from the
  start (GDPR Art. 25(1), Privacy by Design) and shipped with
  privacy-friendly defaults (Art. 25(2), Privacy by Default). Concrete
  requirements: data minimisation in input and storage, pseudonymisation
  where possible, shortest fit-for-purpose retention as default, access
  only for required roles, privacy-compliant logging strategy, clear
  separation between identity, traffic, and content data, age-
  appropriate defaults where minors are involved. The eight
  architecture principles in the "Secure Software Architecture"
  section of the secure-development guideline are extended by these
  privacy-specific items; relevant decisions are recorded as
  privacy-related S-ADRs.
- **Akzeptanz / Acceptance:** Privacy-by-Design-Maßnahmen sind in
  Architektur und Default-Konfiguration umgesetzt; entsprechende S-ADRs
  liegen vor. / Privacy-by-design measures implemented in architecture
  and default configuration; corresponding S-ADRs in place.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, S-ADR oder Architektur-Dokument nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, S-ADR, or architecture document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-11-11: Betroffenenrechte / Data Subject Rights

- **DE:** Die Wahrnehmung der Betroffenenrechte gemäß DSGVO Art. 12–22
  wird technisch und organisatorisch sichergestellt. Geprüft werden:
  transparente Information bei Erhebung (Art. 13/14), Auskunftsrecht
  (Art. 15), Recht auf Berichtigung (Art. 16), Recht auf Löschung
  („Recht auf Vergessenwerden", Art. 17), Recht auf Einschränkung
  (Art. 18), Mitteilungspflicht bei Berichtigung/Löschung (Art. 19),
  Recht auf Datenübertragbarkeit (Art. 20), Widerspruchsrecht
  (Art. 21), keine ausschließlich automatisierte Entscheidung mit
  rechtlicher Wirkung ohne Schutzvorkehrungen (Art. 22).
  Implementierung: Selfservice-Portal für Auskunft und Export, klare
  Kontaktstelle für formlose Anfragen, dokumentierter Workflow zur
  Bearbeitung mit Einhaltung der Frist (Monat, mit zweimonatiger
  Verlängerungsoption gemäß Art. 12 Abs. 3), Identitätsprüfung der
  Antragsstellenden, Lösch- und Sperrkonzept für jede Datenkategorie.
- **EN:** The exercise of data-subject rights per GDPR Art. 12–22 is
  ensured technically and organisationally. Reviews: transparent
  information at collection (Art. 13/14), right of access (Art. 15),
  rectification (Art. 16), erasure ("right to be forgotten", Art. 17),
  restriction (Art. 18), notification of recipients (Art. 19),
  portability (Art. 20), objection (Art. 21), no solely automated
  decision with legal effect without safeguards (Art. 22).
  Implementation: self-service portal for access and export, clear
  contact point for informal requests, documented handling workflow
  meeting the deadline (one month, with two-month extension per Art.
  12(3)), identity verification of requesters, erasure and blocking
  concept per data category.
- **Akzeptanz / Acceptance:** Dokumentierte Prozesse und Schnittstellen
  für jedes Betroffenenrecht; Frist- und Identitätsprüfung im
  Workflow. / Documented processes and interfaces for every data-
  subject right; deadline and identity check in the workflow.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, Prozessdokument oder Self-Service-Spezifikation nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, process document, or self-service specification. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-11-12: Fortschreibung und Aktualisierung / Maintenance and Update

- **DE:** Die DPIA ist ein lebendes Dokument. Sie wird mindestens
  jährlich auf Aktualität geprüft und bei jeder wesentlichen Änderung
  der Verarbeitung außerplanmäßig aktualisiert. Wesentliche Änderungen
  sind insbesondere: neue Datenkategorien, neue Empfänger oder neue
  Drittlandsübermittlungen, neue Auftragsverarbeiter, Wechsel der
  Rechtsgrundlage, neue Zwecke, Einsatz neuer Technologien (zum
  Beispiel KI/Machine-Learning, Verschlüsselungs- oder Anonymisierungs-
  verfahren), Erweiterung des Verarbeitungsumfangs (Skalierung), neue
  Schwellwertanalyse-relevante Erkenntnisse aus Vorfällen, Audits oder
  Schwachstellen. Die Versionshistorie der DPIA wird in der
  Dokumentation geführt; jede Version wird mit Datum, Autor\*in und
  Stellungnahme der oder des DSB versehen.
- **EN:** The DPIA is a living document. It is reviewed at least
  annually and updated outside the regular cycle on any material change
  of the processing. Material changes include new data categories, new
  recipients or third-country transfers, new processors, change of legal
  basis, new purposes, use of new technologies (for example AI/machine
  learning, encryption or anonymisation), expansion of the processing
  scope (scaling), new threshold-analysis-relevant findings from
  incidents, audits, or vulnerabilities. The DPIA version history is
  kept in the documentation; each version carries date, author, and
  DPO opinion.
- **Akzeptanz / Acceptance:** DPIA mit aktueller Versionsangabe;
  jährliche Überprüfung dokumentiert; Auslöser für außerplanmäßige
  Aktualisierungen festgelegt. / DPIA with current version; annual
  review documented; triggers for unplanned updates defined.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket, Versionshistorie der DPIA nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, DPIA version history. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-11-glossar-dsgvo-gdpr"></a>

#### DSGVO / GDPR

- **DE:** Die DSGVO ist die EU-Datenschutz-Grundverordnung. Sie schützt personenbezogene Daten und verlangt klare Zwecke, Rechtsgrundlagen, Schutzmaßnahmen und Nachweise.
- **EN:** The GDPR is the EU General Data Protection Regulation. It protects personal data and requires clear purposes, legal bases, safeguards, and evidence.

<a id="cl-11-glossar-dpia-dsfa"></a>

#### DPIA / DSFA

- **DE:** Eine DSFA oder DPIA prüft vorab, ob eine Verarbeitung personenbezogener Daten hohe Risiken für betroffene Personen hat und welche Gegenmaßnahmen nötig sind.
- **EN:** A DPIA checks in advance whether processing personal data creates high risks for data subjects and which safeguards are needed.

<a id="cl-11-glossar-personal-data"></a>

#### Personenbezogene Daten / Personal Data

- **DE:** Personenbezogene Daten sind Informationen, die sich auf eine identifizierte oder identifizierbare Person beziehen, zum Beispiel Name, Kennung, IP-Adresse oder Standortdaten.
- **EN:** Personal data is information relating to an identified or identifiable person, for example name, identifier, IP address, or location data.

<a id="cl-11-glossar-processing"></a>

#### Verarbeitung / Processing

- **DE:** Verarbeitung meint jeden Umgang mit personenbezogenen Daten, zum Beispiel Erheben, Speichern, Anzeigen, Verändern, Übermitteln oder Löschen.
- **EN:** Processing means any handling of personal data, for example collecting, storing, displaying, changing, transmitting, or deleting.

<a id="cl-11-glossar-data-subject"></a>

#### Betroffene Person / Data Subject

- **DE:** Eine betroffene Person ist die Person, auf die sich personenbezogene Daten beziehen. Sie hat nach DSGVO bestimmte Rechte.
- **EN:** A data subject is the person to whom personal data relates. Under the GDPR, this person has specific rights.

<a id="cl-11-glossar-risk-rating"></a>

#### Risikobewertung / Risk Rating

- **DE:** Eine Risikobewertung ordnet ein Risiko nach Wahrscheinlichkeit und Auswirkung ein. Daraus folgt, welche Maßnahmen zuerst umgesetzt werden müssen.
- **EN:** A risk rating classifies a risk by likelihood and impact. It shows which measures must be implemented first.

<a id="cl-11-glossar-mitigation"></a>

#### Maßnahme / Mitigation

- **DE:** Eine Maßnahme reduziert ein Risiko. Sie kann technisch, organisatorisch oder prozessbezogen sein und braucht meist einen Nachweis.
- **EN:** A mitigation reduces a risk. It can be technical, organisational, or process-related and usually needs evidence.

<a id="cl-11-glossar-dpo-dsb"></a>

#### DSB / DPO

- **DE:** DSB oder DPO bezeichnet die Datenschutzbeauftragte oder den Datenschutzbeauftragten. Diese Rolle berät und kontrolliert Datenschutzfragen unabhängig.
- **EN:** DSB or DPO is the data protection officer. This role advises on and monitors data protection topics independently.

<a id="cl-11-glossar-supervisory-authority"></a>

#### Aufsichtsbehörde / Supervisory Authority

- **DE:** Eine Aufsichtsbehörde überwacht die Einhaltung des Datenschutzrechts. In bestimmten Fällen muss sie informiert oder konsultiert werden.
- **EN:** A supervisory authority monitors compliance with data protection law. In certain cases it must be informed or consulted.

<a id="cl-11-glossar-ropa"></a>

#### Verzeichnis der Verarbeitungstätigkeiten / RoPA

- **DE:** Das Verzeichnis der Verarbeitungstätigkeiten beschreibt, welche personenbezogenen Daten für welche Zwecke verarbeitet werden und welche Rollen beteiligt sind.
- **EN:** The record of processing activities describes which personal data is processed for which purposes and which roles are involved.

<a id="cl-11-glossar-dpa-avv"></a>

#### Auftragsverarbeitung / DPA

- **DE:** Auftragsverarbeitung liegt vor, wenn ein Dienstleister personenbezogene Daten im Auftrag verarbeitet. Dann braucht es klare vertragliche Regeln und Kontrollen.
- **EN:** Processor processing exists when a service provider processes personal data on behalf of another party. Clear contract rules and controls are then required.

<a id="cl-11-glossar-privacy-by-design"></a>

#### Datenschutz durch Technikgestaltung / Privacy by Design

- **DE:** Privacy by Design bedeutet, Datenschutz bereits beim Entwurf einzuplanen, zum Beispiel durch Datensparsamkeit, Löschkonzepte und sichere Voreinstellungen.
- **EN:** Privacy by Design means planning data protection during design, for example through data minimisation, deletion concepts, and secure defaults.

<a id="cl-11-glossar-data-subject-rights"></a>

#### Betroffenenrechte / Data Subject Rights

- **DE:** Betroffenenrechte sind Rechte von Personen an ihren Daten, zum Beispiel Auskunft, Berichtigung, Löschung, Einschränkung und Widerspruch.
- **EN:** Data subject rights are people’s rights regarding their data, for example access, correction, deletion, restriction, and objection.

<a id="cl-11-glossar-evidenz"></a>

#### Evidenz / Evidence

- **DE:** Evidenz ist ein prüfbarer Nachweis. Das kann ein Link, Ticket, Scan-Bericht, Pull Request, Protokoll, Architekturdiagramm oder Dokument sein.
- **EN:** Evidence is verifiable proof. It can be a link, ticket, scan report, pull request, record, architecture diagram, or document.

### Versionshistorie / Version History

- **Version 1.0 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.1 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.

---

- **Version 2.0.0 (2026-07-10):** Einheitliches zweiachsiges Statusmodell, stabile CL-IDs, Lernstufe, Rollen-, Evidenz-, Restrisiko- und Neubewertungsfelder sowie klare Trennung zwischen Vorlage und Projektnachweis eingeführt; synchron mit sichere-Entwicklung-Basis 3.0.0. / Added the unified two-axis status model, stable CL IDs, learning-stage, role, evidence, residual-risk, and re-evaluation fields, plus clear separation between template and project evidence; synchronized with secure-development baseline 3.0.0.
