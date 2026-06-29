# Mitgeltende Dokumente / Related Documents

**Stand / Date:** 2026-06-26
**Zielgruppe / Audience:** Fachinformatiker*innen in Ausbildung, Entwickler*innen, Reviewer und KI-Agenten / IT specialist apprentices, developers, reviewers, and AI agents

## Zweck / Purpose

**DE:** Dieser Ordner enthaelt die mitgeltenden Dokumente zur generischen Richtlinie Sichere Entwicklung. Die Dokumente sind als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Sie konkretisieren die Richtlinie und die zwoelf Checklisten, ohne eine konkrete Organisation oder ein konkretes Managementsystem vorauszusetzen.

**EN:** This folder contains the related documents for the generic Secure Development Guideline. The documents support training, review, and hardening work. They refine the guideline and the twelve checklists without assuming a specific organization or management system.

## Dokumentenliste / Document List

| Dokument / Document | Rolle / Role |
|---|---|
| [Gebrauch_kryptografischer_Massnahmen.md](Gebrauch_kryptografischer_Massnahmen.md) | Mindestvorgaben fuer Kryptografie / Minimum cryptography rules |
| [Kompetenzprofile_und_Schulungsplan_Sichere-Entwicklung.md](Kompetenzprofile_und_Schulungsplan_Sichere-Entwicklung.md) | Rollen, Lernziele und Schulungsplan / Roles, learning goals, and training plan |
| [Leitlinie_Sichere-Programmierung.md](Leitlinie_Sichere-Programmierung.md) | Sprachuebergreifende sichere Programmierung / Cross-language secure programming |
| [Leitlinie_Sichere-Entwicklungs-Sandbox.md](Leitlinie_Sichere-Entwicklungs-Sandbox.md) | Sichere Entwicklungs-Sandbox fuer KI-Agenten und MSL-basierte Level-2-Projekte / Secure development sandbox for AI agents and MSL-based level-2 projects |
| [Richtlinie_Secure-Development-Life-Cycle.md](Richtlinie_Secure-Development-Life-Cycle.md) | Sicherer Entwicklungsprozess / Secure development process |
| [Checkliste_Secure-Development-Life-Cycle.md](Checkliste_Secure-Development-Life-Cycle.md) | Kompakte SDLC-Pruefung / Compact SDLC review |
| [Richtlinie_Changemanagement.md](Richtlinie_Changemanagement.md) | Sichere Aenderungen / Secure changes |
| [Richtlinie_Dienstleister-und-Lieferantenbeziehungen.md](Richtlinie_Dienstleister-und-Lieferantenbeziehungen.md) | Lieferanten- und Dienstleistersteuerung / Supplier and service-provider control |
| [Richtlinie_Testmanagement.md](Richtlinie_Testmanagement.md) | Testplanung, Nachweise und Freigaben / Test planning, evidence, and release gates |
| [Richtlinie_Zugangssteuerung.md](Richtlinie_Zugangssteuerung.md) | Identitaeten, Rollen und Berechtigungen / Identities, roles, and permissions |
| [Datenschutzleitlinie.md](Datenschutzleitlinie.md) | Datenschutz in Entwicklungsprojekten / Privacy in development projects |
| [Leitlinie_Sicheres-Softwaredesign.md](Leitlinie_Sicheres-Softwaredesign.md) | Sicheres Design und Architekturentscheidungen / Secure design and architecture decisions |
| [BCM-Notfallhandbuch.md](BCM-Notfallhandbuch.md) | Betriebsstabilitaet und Notfallfaehigkeit / Resilience and emergency readiness |
| [Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md](Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md) | Mapping zwischen Richtlinie, Checklisten, mitgeltenden Dokumenten und Spec-Kit-Presets / Mapping between guideline, checklists, related documents, and Spec Kit presets |
| [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.pdf](THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.pdf) | CISA-Referenz zu Memory-Safe Roadmaps / CISA reference on memory-safe roadmaps |
| [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.EN.md](THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.EN.md) | Extrahierte englische Markdown-Arbeitsfassung / Extracted English Markdown working copy |
| [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.DE.md](THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.DE.md) | Deutsche CEFR-B2-Lernfassung fuer Auszubildende / German CEFR-B2 learning version for apprentices |
| [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.sha256](THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.sha256) | Integritaetsnachweis fuer die lokale PDF-Kopie / Integrity evidence for the local PDF copy |

## Abgleich mit Spec-Kit-Presets / Alignment With Spec Kit Presets

**DE:** Die Dokumente duerfen den sechs Governance-Presets nicht widersprechen. Bei neuen Spec-Kit-Laeufen gelten sie als fachliche Eingabe fuer die Anwendbarkeitspruefung. Nicht anwendbare Punkte werden als `N/A` mit kurzer Begruendung dokumentiert.

**EN:** The documents must not conflict with the six governance presets. In new Spec Kit runs, they are input for applicability review. Non-applicable items are recorded as `N/A` with a short rationale.

**DE:** Die Datei [Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md](Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md) ist die zentrale Lesefuehrung. Sie zeigt fuer jedes mitgeltende Dokument den Richtlinienbezug, die passenden Checklisten, die betroffenen Presets und typische Evidenzpfade. Nutze sie zuerst, wenn ein Spec-Kit-Lauf oder Review vorbereitet wird.

**EN:** The file [Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md](Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md) is the central reading guide. For each related document, it shows the guideline relation, matching checklists, affected presets, and typical evidence paths. Use it first when preparing a Spec Kit run or review.

| Preset | Relevante Dokumente / Relevant Documents |
|---|---|
| `security-governance` | Kryptografie, sichere Programmierung, Entwicklungs-Sandbox, SDLC, Lieferanten, Zugang, CISA Memory-Safe Roadmaps |
| `architecture-governance` | Sicheres Softwaredesign, Entwicklungs-Sandbox, BCM, SDLC, Changemanagement |
| `isaqb-architecture-governance` | Sicheres Softwaredesign, BCM, Testmanagement |
| `a11y-governance` | Alle Markdown-Dokumente: DE/EN, CEFR B2, WCAG 2.2 AA |
| `agent-parity-governance` | Agenten-Dateien, Sandbox-Guidance, Templates und Skript-Hinweise synchron halten |
| `cross-platform-governance` | Bash-/PowerShell-Paritaet, Sandbox-Bedienbarkeit und dokumentierte Bedienbarkeit |

## Didaktische Mindeststruktur / Teaching Minimum Structure

**DE:** Jedes mitgeltende Dokument soll fuer Auszubildende ab dem ersten Lehrjahr und fuer Entwickler*innen ohne Sicherheits-Spezialwissen nutzbar sein. Es soll mindestens erklaeren: Zweck, Wann relevant?, Was wird geprueft?, typische Nachweise, `N/A`-Regel und Bezug zu Checklisten und Presets.

**EN:** Each related document should be usable by first-year apprentices and developers without security specialist knowledge. It should at least explain: purpose, when relevant, what is reviewed, typical evidence, `N/A` rule, and relation to checklists and presets.

## Swift als MSL / Swift as an MSL

**DE:** Swift ist als Memory-Safe Language (MSL) zu behandeln. Die CISA-Unterlage nennt Swift im Appendix als MSL. In Spec-Kit-Laeufen bedeutet das: Swift erfuellt die MSL-Sprachwahl, braucht aber weiterhin sprachspezifische Secure-Coding-Pruefung, zum Beispiel Force-Unwraps, validierte Eingaben, sichere Dateizugriffe und sichere Keychain-/CryptoKit-Nutzung.

**EN:** Swift is treated as a memory-safe language (MSL). The CISA document lists Swift in the appendix as an MSL. In Spec Kit runs this means: Swift satisfies the MSL language choice, but still needs language-specific secure-coding review, for example force unwraps, validated input, safe file access, and safe Keychain/CryptoKit use.

## Pflege / Maintenance

**DE:** Aenderungen an diesen Dokumenten werden zusammen mit Richtlinie, Checklisten, Sammelband, Agenten-Dateien und betroffenen Skripten geprueft. Projektspezifische Nachweise bleiben in `docs/security/` des jeweiligen Projekts.

**EN:** Changes to these documents are reviewed together with the guideline, checklists, compendium, agent files, and affected scripts. Project-specific evidence stays in each project's `docs/security/` folder.
