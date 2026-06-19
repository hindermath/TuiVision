# Mitgeltende Dokumente / Related Documents

**Stand / Date:** 2026-06-19
**Zielgruppe / Audience:** Fachinformatiker*innen in Ausbildung, Entwickler*innen, Reviewer und KI-Agenten / IT specialist apprentices, developers, reviewers, and AI agents

## Zweck / Purpose

**DE:** Dieser Ordner enthaelt die mitgeltenden Dokumente zur generischen Richtlinie Sichere Entwicklung. Die Dokumente sind als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Sie konkretisieren die Richtlinie und die zwoelf Checklisten, ohne einen Organisations- oder QISMS-Bezug vorauszusetzen.

**EN:** This folder contains the related documents for the generic Secure Development Guideline. The documents support training, review, and hardening work. They refine the guideline and the twelve checklists without assuming a specific organization or QISMS system.

## Dokumentenliste / Document List

| Dokument / Document | Rolle / Role |
|---|---|
| [Gebrauch_kryptografischer_Massnahmen.md](Gebrauch_kryptografischer_Massnahmen.md) | Mindestvorgaben fuer Kryptografie / Minimum cryptography rules |
| [Kompetenzprofile_und_Schulungsplan_Sichere-Entwicklung.md](Kompetenzprofile_und_Schulungsplan_Sichere-Entwicklung.md) | Rollen, Lernziele und Schulungsplan / Roles, learning goals, and training plan |
| [Leitlinie_Sichere-Programmierung.md](Leitlinie_Sichere-Programmierung.md) | Sprachuebergreifende sichere Programmierung / Cross-language secure programming |
| [Richtlinie_Secure-Development-Life-Cycle.md](Richtlinie_Secure-Development-Life-Cycle.md) | Sicherer Entwicklungsprozess / Secure development process |
| [Checkliste_Secure-Development-Life-Cycle.md](Checkliste_Secure-Development-Life-Cycle.md) | Kompakte SDLC-Pruefung / Compact SDLC review |
| [Richtlinie_Changemanagement.md](Richtlinie_Changemanagement.md) | Sichere Aenderungen / Secure changes |
| [Richtlinie_Dienstleister-und-Lieferantenbeziehungen.md](Richtlinie_Dienstleister-und-Lieferantenbeziehungen.md) | Lieferanten- und Dienstleistersteuerung / Supplier and service-provider control |
| [Richtlinie_Testmanagement.md](Richtlinie_Testmanagement.md) | Testplanung, Nachweise und Freigaben / Test planning, evidence, and release gates |
| [Richtlinie_Zugangssteuerung.md](Richtlinie_Zugangssteuerung.md) | Identitaeten, Rollen und Berechtigungen / Identities, roles, and permissions |
| [Datenschutzleitlinie.md](Datenschutzleitlinie.md) | Datenschutz in Entwicklungsprojekten / Privacy in development projects |
| [Leitlinie_Sicheres-Softwaredesign.md](Leitlinie_Sicheres-Softwaredesign.md) | Sicheres Design und Architekturentscheidungen / Secure design and architecture decisions |
| [BCM-Notfallhandbuch.md](BCM-Notfallhandbuch.md) | Betriebsstabilitaet und Notfallfaehigkeit / Resilience and emergency readiness |
| [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.pdf](THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.pdf) | CISA-Referenz zu Memory-Safe Roadmaps / CISA reference on memory-safe roadmaps |
| [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.sha256](THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.sha256) | Integritaetsnachweis fuer die lokale PDF-Kopie / Integrity evidence for the local PDF copy |

## Abgleich mit Spec-Kit-Presets / Alignment With Spec Kit Presets

**DE:** Die Dokumente duerfen den sechs Governance-Presets nicht widersprechen. Bei neuen Spec-Kit-Laeufen gelten sie als fachliche Eingabe fuer die Anwendbarkeitspruefung. Nicht anwendbare Punkte werden als `N/A` mit kurzer Begruendung dokumentiert.

**EN:** The documents must not conflict with the six governance presets. In new Spec Kit runs, they are input for applicability review. Non-applicable items are recorded as `N/A` with a short rationale.

| Preset | Relevante Dokumente / Relevant Documents |
|---|---|
| `security-governance` | Kryptografie, sichere Programmierung, SDLC, Lieferanten, Zugang, CISA Memory-Safe Roadmaps |
| `architecture-governance` | Sicheres Softwaredesign, BCM, SDLC, Changemanagement |
| `isaqb-architecture-governance` | Sicheres Softwaredesign, BCM, Testmanagement |
| `a11y-governance` | Alle Markdown-Dokumente: DE/EN, CEFR B2, WCAG 2.2 AA |
| `agent-parity-governance` | Agenten-Dateien, Templates und Skript-Hinweise synchron halten |
| `cross-platform-governance` | Bash-/PowerShell-Paritaet und dokumentierte Bedienbarkeit |

## Pflege / Maintenance

**DE:** Aenderungen an diesen Dokumenten werden zusammen mit Richtlinie, Checklisten, Sammelband, Agenten-Dateien und betroffenen Skripten geprueft. Projektspezifische Nachweise bleiben in `docs/security/` des jeweiligen Projekts.

**EN:** Changes to these documents are reviewed together with the guideline, checklists, compendium, agent files, and affected scripts. Project-specific evidence stays in each project's `docs/security/` folder.
