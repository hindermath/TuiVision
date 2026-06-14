# Bedrohungsmodell / Threat Model: [FEATURE / PROJECT]

## Spec-Kit-Laufnachweis / Spec-Kit Run Evidence

- Feature / Spec-ID:
- Spec-Kit-Phase / Spec-Kit phase: [specify / plan / tasks / implement / review / release]
- Branch / Commit / PR:
- Datum des Laufs / Run date:
- Evidenzverantwortliche*r / Evidence owner:
- Reviewende Person / Reviewer:
- Gepruefte Standards/Kriterien / Standards or criteria checked: STRIDE, CAPEC, ISO 27001/27002 A.8.27, NIST SSDF, secure architecture risk evidence
- Entscheidung / Decision: [Applicable / N/A / Open]
- Evidenzpfad / Evidence path:
- N/A-Begruendung, falls nicht anwendbar / N/A rationale, if not applicable:
- Offene Folgeaktion mit Owner und Ausloeser / Open follow-up owner and trigger:
- Ausloeser fuer Neubewertung / Re-evaluation trigger:
- Belastbarkeitsnotiz / Assurance note: Diese Datei dokumentiert den konkreten Spec-Kit-Lauf als interne Audit- und Zertifizierungsvorbereitung. Sie ersetzt keine externe Auditierung, Rechtsberatung oder formale Zertifizierung.

## Audit-Evidenzmatrix / Audit Evidence Matrix

| Pruefpunkt / Checkpoint | Anwendbarkeit / Applicability | Erzeugte oder verlinkte Evidenz / Evidence produced or linked | Ergebnis / Result | Restrisiko oder Begruendung / Residual risk or rationale |
|---|---|---|---|---|
| Spec-Kit-Lauf und Scope sind identifiziert / Spec-Kit run and scope are identified | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| Standard- oder Kriterienbezug ist zugeordnet / Standard or criteria mapping is recorded | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| Evidenzpfad ist dokumentiert / Evidence path is documented | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| Nichtanwendbarkeit ist begruendet / Non-applicability is justified | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| Offene Punkte haben Owner und Ausloeser / Open findings have owner and trigger | [Applicable / N/A / Open] | | [OK / Open / N/A] | |

**Projekt / Project**: [Level-2 project name]
**Feature-Branch**: `[###-feature-name]`
**Erstellt / Created**: [DATE]
**Status**: Entwurf / Draft
**Constitution-Referenz / Constitution Reference**: Principle XIII (A.8.27), Principle XII (A.8.28)

## Geltungsbereich / Scope

[Beschreibe, welches System, Feature oder welche Komponente dieses Bedrohungsmodell abdeckt.]

*[Describe which system, feature, or component this threat model covers.]*

---

## Asset-Inventar & CIA-Matrix / Asset Inventory & CIA Matrix

<!--
  Liste alle relevanten Funktionalitäten und Datenwerte auf.
  Bewerte jedes Asset nach Confidentiality (Vertraulichkeit), Integrity (Integrität)
  und Availability (Verfügbarkeit): H = Hoch, M = Mittel, N = Niedrig, - = Nicht anwendbar.
  Der CIA-Wert bestimmt den Schutzbedarf und leitet die STRIDE-Prioritäten.

  List all relevant functionalities and data assets.
  Rate each asset for Confidentiality, Integrity, and Availability:
  H = High, M = Medium, L = Low, - = Not applicable.
  The CIA rating determines protection needs and guides STRIDE priorities.
-->

| Asset / Funktionalität | Beschreibung / Description | Vertraulichkeit / Confidentiality | Integrität / Integrity | Verfügbarkeit / Availability | Gesamt-Schutzbedarf / Overall |
|---|---|:---:|:---:|:---:|:---:|
| [z. B. Benutzerdaten / User data] | [Personenbezogene Daten, PII] | H | H | M | **H** |
| [z. B. Authentifizierungs-Token / Auth token] | [Session- und JWT-Tokens] | H | H | M | **H** |
| [z. B. Konfiguration / Configuration] | [API-Keys, Verbindungszeichenketten] | H | M | L | **H** |
| [z. B. Audit-Log / Audit log] | [Sicherheitsereignisse, Änderungsprotokoll] | M | H | M | **H** |
| [z. B. Öffentliche API / Public API] | [Read-only Endpunkte] | - | M | H | **M** |

**Legende / Legend**: H = Hoch/High · M = Mittel/Medium · L = Niedrig/Low · - = Nicht anwendbar/Not applicable

> **Hinweis / Note**: Assets mit H in Confidentiality oder Integrity erfordern mindestens Defense-in-Depth
> (Prinzip XIII) und sind bevorzugt in der STRIDE-Analyse abzudecken.
> *Assets rated H in Confidentiality or Integrity require at least Defense-in-Depth (Principle XIII)
> and should be prioritised in the STRIDE analysis.*

---

## Trust Boundaries / Vertrauensgrenzen

<!--
  Zeichne ein ASCII-Diagramm der Systemarchitektur mit markierten Trust Boundaries.
  Jede Grenze, die von externem oder nicht vertrauenswürdigem Input überquert wird,
  muss in der STRIDE-Analyse berücksichtigt werden.

  Draw an ASCII diagram of the system architecture with marked trust boundaries.
  Every boundary crossed by external or untrusted input must be addressed in the
  STRIDE analysis.
-->

```text
[ASCII-Diagramm der Architektur mit Trust Boundaries]

Beispiel / Example:

  +-----------+    TB1    +-------------+    TB2    +----------+
  | Benutzer  | --------> | API-Gateway | --------> | Datenbank|
  | / User    |  HTTPS    | / API GW    |  intern   | / DB     |
  +-----------+           +-------------+           +----------+
                               |
                          TB3  |
                               v
                          +---------+
                          | Externer|
                          | Dienst  |
                          +---------+

TB = Trust Boundary / Vertrauensgrenze
```

## STRIDE-Analyse / STRIDE Analysis

<!--
  Für jede Trust Boundary und jede Komponente, die externen Input verarbeitet,
  die STRIDE-Kategorien durchgehen.

  For each trust boundary and each component processing external input,
  walk through the STRIDE categories.
-->

### TB1: [Boundary-Name, z. B. Benutzer → API / User → API]

| STRIDE-Kategorie | Bedrohung / Threat | Wahrscheinlichkeit / Likelihood | Auswirkung / Impact | Risiko / Risk | Mitigation | Status |
|---|---|---|---|---|---|---|
| **S**poofing | [Identitätsvortäuschung / Identity spoofing] | [Hoch/Mittel/Niedrig] | [Hoch/Mittel/Niedrig] | [H/M/N] | [z. B. JWT-Authentifizierung] | [Offen/Mitigiert] |
| **T**ampering | [Datenmanipulation / Data tampering] | | | | | |
| **R**epudiation | [Abstreitbarkeit / Repudiation] | | | | | |
| **I**nformation Disclosure | [Informationspreisgabe / Info disclosure] | | | | | |
| **D**enial of Service | [Dienstverweigerung / DoS] | | | | | |
| **E**levation of Privilege | [Rechteausweitung / Privilege escalation] | | | | | |

### TB2: [Boundary-Name]

| STRIDE-Kategorie | Bedrohung / Threat | Wahrscheinlichkeit / Likelihood | Auswirkung / Impact | Risiko / Risk | Mitigation | Status |
|---|---|---|---|---|---|---|
| **S**poofing | | | | | | |
| **T**ampering | | | | | | |
| **R**epudiation | | | | | | |
| **I**nformation Disclosure | | | | | | |
| **D**enial of Service | | | | | | |
| **E**levation of Privilege | | | | | | |

## CAPEC-Referenzen / CAPEC References

<!--
  Verweise auf relevante CAPEC-Angriffsmuster fuer die risikoreichsten
  Angriffswege. Keine Vollstaendigkeit erforderlich; Fokus auf realistische
  Angriffstechniken.

  Reference relevant CAPEC attack patterns for the highest-risk attack paths.
  Exhaustiveness is not required; focus on realistic attacker techniques.
-->

| Risk / Flow | CAPEC-ID | Angriffsmuster / Attack Pattern | Bezug / Relevance | Mitigation Link |
|---|---|---|---|---|
| [z. B. Login] | [CAPEC-XXX] | | | |

## Risikobewertung / Risk Assessment

<!--
  Zusammenfassung aller identifizierten Risiken, sortiert nach Schweregrad.
  Summary of all identified risks, sorted by severity.
-->

| ID | Risiko / Risk | Schweregrad / Severity | Mitigation | Verantwortlich / Owner | Frist / Deadline |
|---|---|---|---|---|---|
| R001 | | [Kritisch/Hoch/Mittel/Niedrig] | | | |
| R002 | | | | | |

## Residualrisiken / Residual Risks

<!--
  Risiken, die nach Mitigation verbleiben und bewusst akzeptiert werden.
  Risks that remain after mitigation and are consciously accepted.
-->

| ID | Residualrisiko / Residual Risk | Begründung für Akzeptanz / Acceptance Rationale |
|---|---|---|
| | | |

## Referenzen / References

- Constitution Principle XII: Secure Code Generation (ISO 27002:2022 A.8.28)
- Constitution Principle XIII: Secure Software Architecture (ISO 27002:2022 A.8.27)
- Constitution Principle XVII: Threat Modeling & Attack Pattern Coverage
- OWASP Threat Modeling: [owasp.org/www-community/Threat_Modeling](https://owasp.org/www-community/Threat_Modeling)
- STRIDE: Microsoft Threat Modeling methodology
- CAPEC: [capec.mitre.org](https://capec.mitre.org/)

<!-- EN: threat-model-template.md
[DE-Zusammenfassung: Template fuer Bedrohungsmodelle basierend auf STRIDE-Methodik, ISO 27002 A.8.26/A.8.27.]
-->
