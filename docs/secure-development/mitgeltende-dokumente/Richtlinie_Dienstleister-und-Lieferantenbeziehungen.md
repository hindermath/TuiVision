# Richtlinie Dienstleister- und Lieferantenbeziehungen / Service Provider and Supplier Relationship Policy

**Stand / Date:** 2026-06-19

## Zweck / Purpose

**DE:** Diese Richtlinie beschreibt, wie externe Komponenten, Dienste, Bibliotheken und Dienstleister sicher bewertet werden. Sie ist auch fuer Ausbildungsprojekte relevant, weil fast jedes Projekt Abhaengigkeiten nutzt.

**EN:** This policy describes how external components, services, libraries, and service providers are assessed safely. It also matters for training projects because almost every project uses dependencies.

## Mindestpruefung / Minimum Review

| Bereich / Area | Frage / Question |
|---|---|
| Herkunft | Kommt die Abhaengigkeit aus einer vertrauenswuerdigen Quelle? / Does the dependency come from a trusted source? |
| Pflege | Wird sie aktiv gepflegt? / Is it actively maintained? |
| Schwachstellen | Gibt es bekannte kritische CVEs? / Are there known critical CVEs? |
| Lizenz | Ist die Lizenz fuer das Projekt geeignet? / Is the license suitable for the project? |
| Daten | Werden personenbezogene oder vertrauliche Daten verarbeitet? / Are personal or confidential data processed? |
| Betrieb | Entsteht eine Cloud-, Provider- oder Lieferkettenabhaengigkeit? / Does it create cloud, provider, or supply-chain dependency? |

## Ausbildungsregeln / Training Rules

- **DE:** Auszubildende sollen Dependencies nicht nur installieren, sondern Zweck, Risiko und Pflegezustand verstehen.
- **EN:** Apprentices should not only install dependencies, but understand purpose, risk, and maintenance state.
- **DE:** Neue externe Dienste werden mit `Applicable`, `N/A` oder `Open` bewertet.
- **EN:** New external services are assessed as `Applicable`, `N/A`, or `Open`.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** Eine Dependency ist fremder Code im eigenen Projekt. Sie spart Arbeit, bringt aber auch Risiko mit. Deshalb wird geprüft: Woher kommt sie? Wird sie gepflegt? Ist sie verwundbar? Welche Daten sieht sie?

**EN:** A dependency is external code in your own project. It saves work, but also brings risk. Therefore review: where does it come from? Is it maintained? Is it vulnerable? Which data can it see?

## Prüf- und Evidenzhinweise / Review and Evidence Notes

- **DE:** `Applicable`, sobald neue Bibliotheken, Container-Images, APIs, Cloud-Dienste, KI-Dienste oder Provider-Abhängigkeiten genutzt werden.
- **EN:** `Applicable` when new libraries, container images, APIs, cloud services, AI services, or provider dependencies are used.
- **DE:** Typische Evidenz: Dependency-Audit, SBOM, VEX, Lizenznotiz, C3A-/C5-Entscheidung, AI-SBOM- oder `N/A`-Begründung.
- **EN:** Typical evidence: dependency audit, SBOM, VEX, license note, C3A/C5 decision, AI-SBOM or `N/A` rationale.

## Preset-Bezug / Preset Alignment

- `security-governance`: Dependency Audit, SBOM, VEX, CRA, regulatorische Anwendbarkeit.
- `architecture-governance`: Provider-Abhaengigkeiten, Cloud-Autonomie, BSI C3A/C5.
