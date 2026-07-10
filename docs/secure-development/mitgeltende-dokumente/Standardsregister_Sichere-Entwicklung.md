# Standardsregister Sichere Entwicklung / Secure Development Standards Register

**Version / Version:** 1.0.0
**Stand / Date:** 2026-07-10
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** mindestens quartalsweise und bei neuen Fassungen / at least quarterly and when new versions are published

## Zweck / Purpose

**DE:** Dieses Register verhindert, dass verschiedene Fassungen eines Standards vermischt werden. Es nennt die in der sicheren-Entwicklung-Basis verwendete Fassung, die offizielle Quelle und den letzten fachlichen Prüftag. Es ersetzt keine Normtexte und keine Rechtsberatung.

**EN:** This register prevents mixing different versions of a standard. It names the version used by the secure-development baseline, the official source, and the last technical review date. It does not replace standards or legal advice.

## Statuslogik / Status Logic

- `Current`: in dieser Basis verwendete aktuelle Fassung.
- `Pinned`: bewusst verwendete, versionierte Fassung; Wechsel braucht Migration.
- `Reference`: ergänzender Bezugsrahmen ohne vollständige Übernahme.
- `Applicability`: projektbezogene Anwendbarkeit muss entschieden werden.

## Register

| Standard oder Regelwerk | Verwendete Fassung | Status | Typische Anwendung | Offizielle Quelle | Geprüft |
|---|---|---|---|---|---|
| ISO/IEC 27001 | 2022 | `Pinned` | Managementsystem und dokumentierte Information | [ISO/IEC 27001](https://www.iso.org/standard/27001) | 2026-07-10 |
| ISO/IEC 27002 | 2022 | `Pinned` | Controls für sichere Entwicklung | [ISO/IEC 27002](https://www.iso.org/standard/75652.html) | 2026-07-10 |
| NIST SSDF SP 800-218 | 1.1 | `Current` | sicherer Entwicklungslebenszyklus | [NIST SP 800-218](https://csrc.nist.gov/publications/detail/sp/800-218/final) | 2026-07-10 |
| NIST Zero Trust SP 800-207 | Final | `Reference` | verteilte und identitätsbasierte Systeme | [NIST SP 800-207](https://csrc.nist.gov/publications/detail/sp/800-207/final) | 2026-07-10 |
| NIST AI RMF | 1.0 | `Reference` | KI-Risikomanagement | [NIST AI RMF](https://www.nist.gov/itl/ai-risk-management-framework) | 2026-07-10 |
| CWE Top 25 | jeweils im Prüflauf aktueller Jahrgang | `Current` | häufige gefährliche Schwächen | [MITRE CWE Top 25](https://cwe.mitre.org/top25/) | 2026-07-10 |
| OWASP ASVS | 5.0.0 | `Pinned` | Web-, API- und Auth-Verifikation | [OWASP ASVS](https://owasp.org/www-project-application-security-verification-standard/) | 2026-07-10 |
| OWASP Top 10 Web | 2025 | `Current` | Awareness und Review-Fokus | [OWASP Top Ten](https://owasp.org/www-project-top-ten/) | 2026-07-10 |
| OWASP Top 10 for LLM Applications | 2025 | `Pinned` | LLM- und KI-Anwendungsrisiken | [OWASP GenAI LLM Top 10](https://genai.owasp.org/llm-top-10/) | 2026-07-10 |
| OWASP SAMM | 2 | `Reference` | Reifegrad und Verbesserungsplan | [OWASP SAMM](https://owaspsamm.org/) | 2026-07-10 |
| OWASP Cheat Sheet Series | laufend | `Current` | konkrete Implementierungshilfe | [OWASP Cheat Sheets](https://cheatsheetseries.owasp.org/) | 2026-07-10 |
| SLSA | 1.2 | `Pinned` | Build-Provenienz und Integrität | [SLSA](https://slsa.dev/) | 2026-07-10 |
| CycloneDX | im Tooling unterstützte aktuelle Fassung | `Current` | SBOM und ML-BOM | [CycloneDX](https://cyclonedx.org/) | 2026-07-10 |
| SPDX | im Tooling unterstützte aktuelle Fassung | `Current` | SBOM und Lizenzen | [SPDX](https://spdx.dev/) | 2026-07-10 |
| BSI TR-02102 | jeweils aktuelle Teilfassung | `Current` | Kryptografie und TLS | [BSI TR-02102](https://www.bsi.bund.de/DE/Themen/Unternehmen-und-Organisationen/Standards-und-Zertifizierung/Technische-Richtlinien/TR-nach-Thema-sortiert/tr02102/tr-02102.html) | 2026-07-10 |
| BSI C3A | veröffentlichte BSI-Fassung | `Applicability` | Cloud-Autonomie und digitale Souveränität | [BSI Cloud Computing](https://www.bsi.bund.de/DE/Themen/Unternehmen-und-Organisationen/Informationen-und-Empfehlungen/Empfehlungen-nach-Angriffszielen/Cloud-Computing/cloud-computing_node.html) | 2026-07-10 |
| BSI C5 | jeweils aktuelle BSI-Fassung | `Applicability` | Cloud-Assurance und Testate | [BSI C5](https://www.bsi.bund.de/DE/Themen/Unternehmen-und-Organisationen/Informationen-und-Empfehlungen/Empfehlungen-nach-Angriffszielen/Cloud-Computing/Kriterienkatalog-C5/kriterienkatalog-c5_node.html) | 2026-07-10 |
| EU Cyber Resilience Act | Verordnung (EU) 2024/2847 | `Applicability` | Produkte mit digitalen Elementen | [EUR-Lex CRA](https://eur-lex.europa.eu/eli/reg/2024/2847/oj) | 2026-07-10 |
| EU AI Act | Verordnung (EU) 2024/1689 | `Applicability` | KI-Systeme und bestimmte KI-Nutzungen | [EUR-Lex AI Act](https://eur-lex.europa.eu/eli/reg/2024/1689/oj) | 2026-07-10 |
| NIS2 | Richtlinie (EU) 2022/2555 | `Applicability` | wichtige und besonders wichtige Einrichtungen | [EUR-Lex NIS2](https://eur-lex.europa.eu/eli/dir/2022/2555/oj) | 2026-07-10 |
| DORA | Verordnung (EU) 2022/2554 | `Applicability` | digitaler Finanzsektor | [EUR-Lex DORA](https://eur-lex.europa.eu/eli/reg/2022/2554/oj) | 2026-07-10 |
| G7 Software Bill of Materials for AI | Minimum Elements, 2026 | `Reference` | KI-Lieferkettentransparenz | Anbieter- oder Behördenquelle im projektspezifischen Nachweis | 2026-07-10 |
| CISA Memory-Safe Roadmaps | 2023/2024-Arbeitsgrundlage der lokalen PDF | `Reference` | MSL-Entscheidung und Roadmap | [CISA Memory Safe Roadmaps](https://www.cisa.gov/resources-tools/resources/case-memory-safe-roadmaps) | 2026-07-10 |

## OWASP-LLM-Nummern / OWASP LLM Numbering

**DE:** Diese Basis verwendet ausschließlich die 2025-Nummern. Beispiele: `LLM02:2025 Sensitive Information Disclosure`, `LLM05:2025 Improper Output Handling`, `LLM06:2025 Excessive Agency` und `LLM09:2025 Misinformation`. Ältere Nummern dürfen nur mit explizitem Versionssuffix zitiert werden.

**EN:** This baseline uses only the 2025 numbering. Examples: `LLM02:2025 Sensitive Information Disclosure`, `LLM05:2025 Improper Output Handling`, `LLM06:2025 Excessive Agency`, and `LLM09:2025 Misinformation`. Older numbering may only be cited with an explicit version suffix.

## Pflege und Nachweis / Maintenance and Evidence

- Änderungen werden gegen offizielle Primärquellen geprüft.
- Ein Versionswechsel nennt betroffene Richtlinienabschnitte, CL-IDs und Migrationsbedarf.
- Projektspezifische Entscheidungen dokumentieren `Applicable`, `N/A` oder `Open` und einen Evidenzpfad.
- ISO-Inhalte werden nur über Kontroll- oder Klauselnummern referenziert; lizenzierte Normtexte werden nicht kopiert.

## Statusbedeutung / Status Meaning

- `Current`: Diese Fassung ist die aktuelle fachliche Arbeitsgrundlage.
- `Pinned`: Diese konkrete Fassung ist bewusst festgelegt; ein Wechsel braucht eine Migrationsprüfung.
- `Applicability`: Die Quelle wird nur verwendet, wenn Projektart oder Rechtskontext sie auslösen.
- `Reference`: Die Quelle unterstützt Entscheidungen, ist aber kein automatisch verbindlicher Kontrollkatalog.

*`Current` is the active technical baseline. `Pinned` is deliberately fixed and requires migration review. `Applicability` depends on project or legal scope. `Reference` supports decisions but is not automatically binding.*

## Aktualisierungsprozess / Update Process

1. Primärquelle und Veröffentlichungsdatum prüfen.
2. Unterschiede zur registrierten Fassung beschreiben.
3. Betroffene Richtlinienabschnitte, CL-IDs, Vorlagen und Presets ermitteln.
4. Kompatibilität, Lernwirkung und Migrationsbedarf bewerten.
5. Register, Dokumente, Generatornachweis und Versionshistorie gemeinsam aktualisieren.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** Standards ändern sich. Deshalb steht hinter einer Regel immer eine Fassung. Die Nummer ist wichtig: Derselbe Code wie `LLM02` kann in einer anderen Ausgabe eine andere Bedeutung haben. Prüfe immer Name, Jahr und Quelle gemeinsam.

**EN:** Standards change. Therefore every rule has a version. The number matters: the same code such as `LLM02` can mean something different in another edition. Always check name, year, and source together.

## Versionshistorie / Version History

| Version | Datum | Änderung |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Register für sichere-Entwicklung-Basis 3.0.0. |
