# Supply-Chain-Evidenz / Supply Chain Evidence: [PROJECT]

## Spec-Kit-Laufnachweis / Spec-Kit Run Evidence

- Feature / Spec-ID:
- Spec-Kit-Phase / Spec-Kit phase: [specify / plan / tasks / implement / review / release]
- Branch / Commit / PR:
- Datum des Laufs / Run date:
- Evidenzverantwortliche*r / Evidence owner:
- Reviewende Person / Reviewer:
- Gepruefte Standards/Kriterien / Standards or criteria checked: SBOM, AI-SBOM, VEX, SLSA provenance, OpenSSF Scorecard, CRA applicability where relevant
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
**Artefakt / Artefact scope**: [Release / package / container / binary / docs]
**Reviewer**: [Name]
**Datum / Date**: [YYYY-MM-DD]
**Constitution-Referenz / Constitution Reference**: Principle XIV, Principle XVI

<!--
  Dieses Dokument buendelt die Evidenz fuer SBOM, AI-SBOM, VEX,
  Provenance/SLSA und gegebenenfalls OpenSSF Scorecard in einem Ort.

  This document consolidates evidence for SBOM, AI-SBOM, VEX,
  provenance/SLSA, and, where relevant, OpenSSF Scorecard in one location.
-->

## 1. Geltungsbereich / Applicability

| Thema / Topic | Gilt / Applies | Evidenz-Ort / Evidence Location | Status |
|---|---|---|---|
| SBOM | [Ja/Nein] | [Link / Pfad] | [OK/Open/N/A] |
| AI-SBOM / G7-BSI Minimum Elements | [Ja/Nein] | [Link / Pfad] | [OK/Open/N/A] |
| VEX | [Ja/Nein] | [Link / Pfad] | |
| Provenance / SLSA | [Ja/Nein] | [Link / Pfad] | |
| OpenSSF Scorecard | [Ja/Nein] | [Link / Pfad] | |

## 2. SBOM / Software Bill of Materials

| Artefakt / Artefact | Format | Datei / File | Erzeugt am / Generated | Tool |
|---|---|---|---|---|
| [z. B. Release ZIP] | [SPDX / CycloneDX] | [Link] | [YYYY-MM-DD] | [Tool] |

## 3. AI-SBOM / AI Software Bill of Materials

**Anwendbarkeit / Applicability**: [Gilt / N/A]

**Begruendung / Rationale**: [KI nur Entwicklungswerkzeug / keine KI im ausgelieferten oder betriebenen System / KI-Runtime-Komponente vorhanden / andere Begruendung]

| G7/BSI Cluster | Evidenz / Evidence | Status |
|---|---|---|
| Metadata | [System, Anbieter, Versionen, Verantwortliche] | [OK/Open/N/A] |
| System-level properties | [Systemzweck, AI-Funktion, Betriebsmodus] | |
| Models | [Modellname, Version, Anbieter, Lizenz/Nutzung] | |
| Datasets | [Trainings-, Fine-Tuning-, Embedding- oder RAG-Daten, soweit bekannt] | |
| Infrastructure | [Inference-Provider, Hosting, Runtime, Deployment-Region] | |
| Security properties | [Zugriff, Logging, Datenfluesse, Schutzmassnahmen, Abuse Controls] | |
| Key performance indicators | [Qualitaets-, Sicherheits- oder Betriebs-KPIs] | |

Hinweis / Note: Wenn KI nur als Entwicklungswerkzeug genutzt wird und keine
KI-Komponente Teil des ausgelieferten oder betriebenen Systems ist, `AI-SBOM`
als `N/A` mit kurzer Toolchain-Begruendung dokumentieren.

## 4. VEX / Vulnerability Exploitability eXchange

| Komponente / Component | Schwachstelle / Vulnerability | Status | Begründung / Rationale | Referenz |
|---|---|---|---|---|
| | [CVE / GHSA] | [affected / not_affected / fixed / under_investigation] | | |

## 5. Provenance / SLSA

| Artefakt / Artefact | Build Source | Provenance / Attestation | SLSA-Ziel / Target | Status |
|---|---|---|---|---|
| | | [Link] | [L1/L2/...] | |

## 6. OpenSSF Scorecard

| Repository / Dependency | Gilt / Applies | Ergebnis / Result | Datum / Date | Aktion / Action |
|---|---|---|---|---|
| | [Ja/Nein] | | | |

## 7. Offene Punkte / Open Findings

| ID | Thema / Topic | Finding | Risiko / Risk | Aktion / Action | Owner | Ziel / Target |
|---|---|---|---|---|---|---|
| S001 | | | | | | |

## Referenzen / References

- Constitution Principle XIV: Secure Development Standards & Applicability Matrix
- Constitution Principle XVI: Supply-Chain Transparency & Build Integrity
- BSI / G7: Software Bill of Materials (SBOM) for Artificial Intelligence - Minimum Elements
- CISA SBOM: [cisa.gov/sbom](https://www.cisa.gov/sbom)
- SLSA: [slsa.dev](https://slsa.dev/)
- OpenSSF Scorecard: [openssf.org/projects/scorecard](https://openssf.org/projects/scorecard/)

<!-- EN: supply-chain-evidence-template.md
[DE-Zusammenfassung: Template fuer SBOM-, AI-SBOM-, VEX-, Provenance/SLSA- und OpenSSF-Scorecard-Evidenz an einer zentralen Stelle.]
-->
