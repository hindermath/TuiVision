# Bedrohungsmodell / Threat Model: [FEATURE / PROJECT]

**Projekt / Project**: [Level-2 project name]
**Feature-Branch**: `[###-feature-name]`
**Erstellt / Created**: [DATE]
**Status**: Entwurf / Draft
**Constitution-Referenz / Constitution Reference**: Principle XIII (A.8.27), Principle XII (A.8.28)

## Geltungsbereich / Scope

[Beschreibe, welches System, Feature oder welche Komponente dieses Bedrohungsmodell abdeckt.]

*[Describe which system, feature, or component this threat model covers.]*

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
