# Supply-Chain-Evidenz / Supply Chain Evidence: [PROJECT]

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
