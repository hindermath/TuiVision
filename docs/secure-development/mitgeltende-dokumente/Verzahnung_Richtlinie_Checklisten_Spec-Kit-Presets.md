# Verzahnung Richtlinie, Checklisten und Spec-Kit-Presets

**Stand / Date:** 2026-06-20
**Zielgruppe / Audience:** Fachinformatiker*innen in Ausbildung, Entwickler*innen, Reviewer und KI-Agenten / IT specialist apprentices, developers, reviewers, and AI agents

## Zweck / Purpose

**DE:** Diese Datei zeigt, wie die mitgeltenden Dokumente zur Richtlinie Sichere Entwicklung, zu den zwölf Checklisten und zu den sechs GitHub-Spec-Kit-Governance-Presets passen. Sie ist eine Lese- und Prüfbrücke. Sie ersetzt keine projektspezifischen Nachweise.

**EN:** This file shows how the related documents connect to the Secure Development Guideline, the twelve checklists, and the six GitHub Spec Kit governance presets. It is a reading and review bridge. It does not replace project-specific evidence.

## Wie diese Datei genutzt wird / How To Use This File

**DE:** Nutze diese Datei zu Beginn eines Spec-Kit-Laufs oder Reviews. Wähle die relevanten Zeilen aus, entscheide `Applicable`, `N/A` oder `Open`, und verweise auf konkrete Evidenz. Für Auszubildende gilt: Erst verstehen, dann prüfen, dann dokumentieren.

**EN:** Use this file at the start of a Spec Kit run or review. Select the relevant rows, decide `Applicable`, `N/A`, or `Open`, and link concrete evidence. For apprentices: understand first, then review, then document.

## Statuslogik / Status Logic

| Status | Bedeutung / Meaning |
|---|---|
| `Applicable` | Der Punkt gilt für den aktuellen Lauf und braucht Evidenz. / The item applies to the current run and needs evidence. |
| `N/A` | Der Punkt gilt nicht; die Begründung ist dokumentiert. / The item does not apply; the rationale is documented. |
| `Open` | Der Punkt gilt oder ist unklar; Folgearbeit ist nötig. / The item applies or is unclear; follow-up is needed. |

## Zentrale Mapping-Matrix / Central Mapping Matrix

| Mitgeltendes Dokument | Richtlinienbezug | Checklistenbezug | Spec-Kit-Preset | Typische Evidenz |
|---|---|---|---|---|
| `Gebrauch_kryptografischer_Massnahmen.md` | Kryptografische Mindestvorgaben, sichere Programmierung | CL_03, CL_08, CL_09 | `security-governance`, `architecture-governance` | Krypto-Review, S-ADR, Threat Model, Code-Review, Testnachweis |
| `Kompetenzprofile_und_Schulungsplan_Sichere-Entwicklung.md` | Qualifikation, Schulung, didaktische Nachvollziehbarkeit | CL_08, CL_09, CL_10, CL_12 | `a11y-governance`, `agent-parity-governance`, `security-governance` | Lernzielnotiz, Review-Protokoll, Aufgabenliste, Schulungsnachweis |
| `Leitlinie_Sichere-Programmierung.md` | Secure Coding, MSL, Fehlerbehandlung, Eingaben, Abhängigkeiten | CL_01, CL_05, CL_08, CL_09 | `security-governance` | Secure-Coding-Check, Sprachprofil, Dependency-Audit, MSL-Entscheidung |
| `Leitlinie_Sichere-Entwicklungs-Sandbox.md` | Sandbox-Freigabe, MSL-Toolchains, KI-Agenten, Mounts, Netzwerk, Public-Readiness | CL_05, CL_09, CL_10, CL_12 | `security-governance`, `architecture-governance`, `a11y-governance`, `cross-platform-governance`, `agent-parity-governance` | Sandbox-Freigabe, Isolationsnachweis, MSL-Support-Matrix, SBOM/Scan, Netzwerkentscheidung, Lastenheft |
| `Richtlinie_Secure-Development-Life-Cycle.md` | SDLC, Spezifikation, Planung, Umsetzung, Freigabe | CL_01 bis CL_12 | alle sechs Presets | `spec.md`, `plan.md`, `tasks.md`, Review- und Abschlussnotiz |
| `Checkliste_Secure-Development-Life-Cycle.md` | Kompakter SDLC-Review | CL_01 bis CL_12 | alle sechs Presets | Ausgefüllte SDLC-Kurzprüfung mit Status und Evidenzpfad |
| `Richtlinie_Changemanagement.md` | Nachvollziehbare Änderungen, Freigaben, Tests | CL_06, CL_08, CL_10, CL_12 | `agent-parity-governance`, `cross-platform-governance`, `security-governance` | Commit, PR/MR, Review, Testlauf, Änderungsnotiz |
| `Richtlinie_Dienstleister-und-Lieferantenbeziehungen.md` | Dependencies, Dienste, Lieferkette, Cloud-/Provider-Abhängigkeiten | CL_01, CL_05, CL_07 | `security-governance`, `architecture-governance` | Dependency-Audit, SBOM, VEX, C3A/C5-Entscheidung, Lieferantenbewertung |
| `Richtlinie_Testmanagement.md` | Teststrategie, Sicherheits- und A11Y-Tests | CL_02, CL_04, CL_08, CL_10 | `security-governance`, `a11y-governance`, `cross-platform-governance` | Testplan, CI-Ergebnis, Coverage, A11Y-Smoke-Test, `N/A`-Begründung |
| `Richtlinie_Zugangssteuerung.md` | Identitäten, Rollen, Least Privilege, Secrets | CL_02, CL_03, CL_08, CL_10, CL_12 | `security-governance`, `architecture-governance`, `agent-parity-governance` | Berechtigungsmatrix, Secret-Scan, Rollenentscheidung, Sandbox-Konfiguration |
| `Datenschutzleitlinie.md` | Datenschutz, Testdaten, Logs, DPIA | CL_01, CL_08, CL_11 | `security-governance`, `architecture-governance`, `a11y-governance` | Datenflussnotiz, DPIA, Logging-Review, Testdatenkonzept |
| `Leitlinie_Sicheres-Softwaredesign.md` | Trust Boundaries, Defense in Depth, S-ADR, sichere Konfiguration | CL_02, CL_04, CL_08 | `architecture-governance`, `isaqb-architecture-governance`, `security-governance` | S-ADR, arc42 Abschnitt 8, Threat Model, Qualitäts- oder Risikoszenario |
| `BCM-Notfallhandbuch.md` | Wiederanlauf, Abhängigkeiten, Betriebsstabilität | CL_02, CL_05, CL_10 | `architecture-governance`, `isaqb-architecture-governance`, `security-governance` | Backup-/Restore-Notiz, CI/CD-Wiederanlauf, Provider-Risiko, Runbook |
| `THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.*` | MSL-Präferenz, sichere Sprachwahl, Migrationsplanung | CL_01, CL_05, CL_08, CL_09 | `security-governance`, `architecture-governance`, `a11y-governance` | MSL-Entscheidung, Nicht-MSL-Begründung, Roadmap, Lernnotiz |

## Swift und Memory-Safe Languages / Swift and Memory-Safe Languages

**DE:** Swift ist eine Memory-Safe Language. Die CISA-Unterlage nennt Swift im Appendix als MSL und beschreibt Swift als Sprache, die Apple 2014 veröffentlicht hat und die besonders für iOS-, watchOS- und macOS-Entwicklung genutzt wird. In dieser Workspace-Governance ist Swift daher auf der MSL-Erlaubnisliste. Trotzdem gilt: Swift ersetzt keine sichere API-Nutzung, keine Eingabevalidierung, keine sichere Fehlerbehandlung und keine Dependency-Prüfung.

**EN:** Swift is a memory-safe language. The CISA document lists Swift in the appendix as an MSL and describes it as a language Apple released in 2014 that is mainly used for iOS, watchOS, and macOS development. In this workspace governance, Swift is therefore on the MSL allow-list. Still, Swift does not replace secure API use, input validation, safe error handling, or dependency review.

## Mindestprüffragen für Spec-Kit-Läufe / Minimum Review Questions for Spec Kit Runs

| Frage / Question | Erwartung / Expectation |
|---|---|
| Welche mitgeltenden Dokumente sind berührt? / Which related documents are touched? | Zeilen aus der Mapping-Matrix auswählen und begründen. / Select and justify rows from the mapping matrix. |
| Welche CLs sind betroffen? / Which CLs are affected? | CL-Nummern im `plan.md`, `tasks.md` oder Review nennen. / Name CL numbers in `plan.md`, `tasks.md`, or review. |
| Welche Presets liefern passende Artefakte? / Which presets provide matching artefacts? | Preset-Liste, Versionen und relevante Templates dokumentieren. / Document preset list, versions, and relevant templates. |
| Wo liegt die Evidenz? / Where is the evidence? | Pfad, Link, PR, Test, Scan oder `docs/security/`-Artefakt nennen. / Name path, link, PR, test, scan, or `docs/security/` artefact. |
| Gibt es `N/A`? / Are there `N/A` decisions? | Kurze technische oder fachliche Begründung notieren. / Record a short technical or functional rationale. |

## Lastenheft als Spec-Kit-Intake / Requirements Document as Spec Kit Intake

**DE:** Ein Lastenheft, das spaeter direkt fuer `/speckit-specify` genutzt wird, soll die spaetere Pruefung bereits vorbereiten. Es nennt Zweck, Ausgangslage, Zielbild, Scope, Nicht-Ziele, Anforderungen, erwartete Artefakte, Akzeptanzkriterien und einen kopierbaren Prompt. Wenn Sicherheits-, Architektur-, A11Y-, Supply-Chain- oder Agenten-Governance betroffen ist, muss der Prompt `Applicable`, `N/A`, `Open`, Evidenzpfad und Begruendungspflicht ausdruecklich verlangen.

**EN:** A requirements document that will later be used directly with `/speckit-specify` should prepare the later review already. It names purpose, context, target state, scope, non-goals, requirements, expected artefacts, acceptance criteria, and a copyable prompt. If security, architecture, accessibility, supply chain, or agent governance is affected, the prompt must explicitly require `Applicable`, `N/A`, `Open`, evidence paths, and rationale duties.

## Didaktische Regel / Teaching Rule

**DE:** Für Auszubildende ab dem ersten Lehrjahr muss jeder Prüfpunkt in Alltagssprache erklärbar sein. Wenn ein Begriff wie `SBOM`, `VEX`, `SLSA`, `Threat Model`, `MSL`, `C3A` oder `C5` verwendet wird, muss das Dokument oder der Spec-Kit-Lauf einen kurzen erklärenden Satz oder einen Link auf die Lernfassung enthalten.

**EN:** For first-year apprentices, every review item must be explainable in everyday language. If a term such as `SBOM`, `VEX`, `SLSA`, `Threat Model`, `MSL`, `C3A`, or `C5` is used, the document or Spec Kit run must include a short explanation or link to the learning version.
