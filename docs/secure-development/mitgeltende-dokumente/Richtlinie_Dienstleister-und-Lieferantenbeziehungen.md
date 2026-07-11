# Richtlinie Dienstleister- und Lieferantenbeziehungen / Service Provider and Supplier Relationship Policy

**Stand / Date:** 2026-07-10
**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

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

## Lebenszyklusprüfung / Lifecycle Review

1. **Vor Auswahl / Before selection:** Zweck, Alternativen, Herkunft, Pflege, Lizenz, Datenzugriff, Sicherheitsnachweise und Exit-Möglichkeit prüfen.
2. **Bei Einführung / Onboarding:** Version pinnen, minimale Rechte vergeben, Datenflüsse und Subprozessoren dokumentieren, Verantwortungen festlegen.
3. **Im Betrieb / During use:** CVEs, Änderungen, Ausfälle, Testate, Nutzungsumfang und Abhängigkeiten regelmäßig prüfen.
4. **Beim Ende / Exit:** Datenexport und -löschung, Secret-Rotation, Rechteentzug, Ersatzkomponente und Archivnachweise festlegen.

## Cloud- und KI-Dienste / Cloud and AI Services

Bei Cloud-Diensten werden C3A-/C5-Anwendbarkeit, Datenstandort, Shared Responsibility, Provider Lock-in, Subprozessoren, Incident-Evidence und Exit getestet. Bei KI-Diensten werden zusätzlich Prompt-/Trainingsdatennutzung, Modell-/Provider-Version, Telemetrie, AI-SBOM-Anwendbarkeit und EU-AI-Act-Scope bewertet. / For cloud services, assess C3A/C5 applicability, data location, shared responsibility, provider lock-in, subprocessors, incident evidence, and exit. For AI services, also assess prompt/training-data use, model/provider version, telemetry, AI-SBOM applicability, and EU AI Act scope.

## Mindestnachweis je Abhängigkeit / Minimum Evidence per Dependency

Name und Version, Zweck, Quelle, Owner, Lizenz, CVE-Stand, Datenzugriff, Berechtigungen, Update-/Exit-Weg, Entscheidung und nächster Prüftermin. Kleine Bibliotheken können gruppiert im Dependency Audit oder in der SBOM nachgewiesen werden. / Record name and version, purpose, source, owner, licence, CVE state, data access, permissions, update/exit path, decision, and next review date. Small libraries may be grouped in the dependency audit or SBOM.

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


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
