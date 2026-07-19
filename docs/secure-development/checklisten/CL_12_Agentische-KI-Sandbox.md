<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 12 – Agentische KI in Sandbox-Umgebungen / Agentic AI in Sandbox Environments

**Dokument-ID / Document ID:** CL-12
**Version / Version:** 2.2.0
**Baseline-Version / Baseline version:** 3.2.0
**Dokumenttyp / Document type:** Kanonische, wiederverwendbare Pruefvorlage / Canonical reusable review template

### Nachweisinstanz / Evidence Instance

Diese Datei ist eine wiederverwendbare Vorlage. Ausgefüllte Projektnachweise werden unter `docs/security/secure-development/<datum>-<scope>/` abgelegt und nennen Projekt, Scope, Prüfdatum, Baseline-Version, verantwortliche Person und Reviewer. / This file is a reusable template. Completed project evidence is stored under `docs/security/secure-development/<date>-<scope>/` and names project, scope, review date, baseline version, responsible person, and reviewer.

### Zweck / Purpose

**DE:** Diese Checkliste prüft, ob agentische KI-Werkzeuge wie OpenCode oder Codex sicher, nachvollziehbar und nur in freigegebenen Sandbox-Umgebungen eingesetzt werden. Sie ergänzt die Regeln zur KI-Codeerzeugung, weil Agenten selbständig Dateien ändern, Befehle ausführen und Projektkontext lesen können.

**EN:** This checklist verifies that agentic AI tools such as OpenCode or Codex are used securely, traceably, and only in approved sandbox environments. It extends the rules for AI-assisted code generation because agents can change files, run commands, and read project context.

### Geltungsbereich / Scope

**DE:** Pflicht für jedes Projekt, in dem agentische KI für Analyse, Spezifikation, Planung, Refaktorierung, Tests oder Codeänderungen eingesetzt wird. Die Checkliste gilt auch für Sandbox-Umgebungen, die noch in der Feinabstimmung sind.

**EN:** Mandatory for every project that uses agentic AI for analysis, specification, planning, refactoring, tests, or code changes. The checklist also applies to sandbox environments that are still being tuned.

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung, Abschnitt „Agentische KI in Sandbox-Umgebungen" (ab Version 2.2.0)
- Leitlinie Sichere Entwicklungs-Sandbox
- CL_KI-Codeerzeugung
- CL_Sichere-Entwicklungsumgebung
- GitHub Spec Kit und projektlokale Spec-Kit-Artefakte
- Governance-Presets: `security-governance`, `architecture-governance`, `isaqb-architecture-governance`, `a11y-governance`, `cross-platform-governance`, `agent-parity-governance`
- ISO/IEC 27001:2022 Annex A: A.5.23, A.8.25, A.8.28, A.8.31
- NIST AI Risk Management Framework (AI RMF 1.0)
- OWASP Top 10 for Large Language Model Applications
- Beispielhafte Sandbox-Referenz: `absdd-image-sandbox`

#### URL-/Ablageverweise / URLs and Storage Locations

**DE:** Diese Links helfen beim Review. Projekt- oder organisationsinterne Dokumente koennen als lokale Arbeitskopie oder als Verweis auf den festgelegten Ablageort ergaenzt werden.

**EN:** These links help during reviews. Project or organization-internal documents can be added as local working copies or references to the defined storage location.

- **Richtlinie Sichere Entwicklung / Secure Development Guideline:** [lokale Arbeitsfassung in diesem Repository / local working copy in this repository](../Richtlinie_Sichere-Entwicklung.md)
- **Leitlinie Sichere Entwicklungs-Sandbox:** [mitgeltendes Dokument fuer Sandbox-Profile / related document for sandbox profiles](../mitgeltende-dokumente/Leitlinie_Sichere-Entwicklungs-Sandbox.md)
- **Checklisten-Index / Checklist index:** [Übersicht aller Checklisten / overview of all checklists](../README.md)
- **GitHub Spec Kit Presets:** [offizielle Spec-Kit-Preset-Referenz / official Spec Kit preset reference](https://github.github.com/spec-kit/reference/presets.html)
- **Spec-Kit Community Presets:** [Community-Presets im GitHub-Spec-Kit-Repository / community presets in the GitHub Spec Kit repository](https://github.com/github/spec-kit/blob/main/docs/community/presets.md)
- **Beispielhafte Sandbox-Referenz:** `absdd-image-sandbox` als oeffentlichkeitsfaehiges Ausbildungsprofil / as a public-ready training profile

### Bewertung und Dokumentation / Assessment and Documentation

**DE:** Jeder Prüfpunkt bekommt genau einen Wert je Statusachse. Die Begründung nennt den geprüften Projektpfad, die Sandbox-Art, das eingesetzte Werkzeug und die relevante Evidenz.

**EN:** Each checklist item gets exactly one value per status axis. The explanation names the checked project path, sandbox type, tool, and relevant evidence.

- **Anwendbarkeit / Applicability:** `Applicable`, `N/A` oder `Open`.
- **Umsetzungsstatus / Implementation status:** `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed`.

**Pflichtfelder je Prüfpunkt / Required fields per item:** Anwendbarkeit, Umsetzungsstatus, Lernstufe, verantwortliche Rolle, Begründung, Evidenzpfad oder Link, Restrisiko, Neubewertungs-Trigger und nächste Maßnahme mit Zieltermin.

### Durchführungshinweise / Implementation Guidance

**DE:** Prüfe reale Artefakte, nicht nur Absichtserklärungen. Geeignete Nachweise sind Sandbox-Dokumentation, Container- oder VM-Konfiguration, Mount-Liste, Tool-Konfiguration, Spec-Kit-Verzeichnis, Preset-Liste, Preset-Versionen und Prioritäten, Preset-Update-Protokoll, Pull Request, Review-Protokoll und Ticket.

**EN:** Check real artefacts, not only intent statements. Suitable evidence includes sandbox documentation, container or VM configuration, mount list, tool configuration, Spec-Kit directory, preset list, preset versions and priorities, preset update record, pull request, review record, and ticket.

**DE:** Jeder Prüfpunkt muss deshalb drei Fragen beantworten: Was bedeutet die Anforderung im Projektalltag? Was ist konkret zu tun oder zu entscheiden? Welcher Nachweis zeigt das Ergebnis? Verwende Standard-IDs, Toolnamen und Abkürzungen nur zusammen mit einer kurzen Erklärung in Alltagssprache. Wenn ein Punkt für Auszubildende oder neue Teammitglieder nicht selbsterklärend ist, ergänze eine kurze Erklärung in der Begründung.

**EN:** Each item must therefore answer three questions: What does the requirement mean in daily project work? What exactly must be done or decided? Which evidence shows the result? Use standard IDs, tool names, and abbreviations only together with a short plain-language explanation. If an item is not self-explanatory for apprentices or new team members, add a short explanation in the rationale.

### Beispiel / Example

**DE:** Ein Projekt nutzt OpenCode oder Codex in einer Container-Sandbox. Das Projektverzeichnis ist als Host-Mount eingebunden. Agentendaten, Caches und Zugangsdaten liegen in getrennten Sandbox-Speichern. Spec Kit ist im Projekt initialisiert, alle acht Governance-Presets sind installiert, und der Pull Request enthält menschliches Review.

**EN:** A project uses OpenCode or Codex in a container sandbox. The project directory is mounted from the host. Agent data, caches, and credentials are stored in separate sandbox storage. Spec Kit is initialized in the project, all eight governance presets are installed, and the pull request contains human review.

### A11Y-Hinweise / A11Y Notes

**DE:** Evidenz muss textlich verständlich sein. Screenshots brauchen eine kurze Beschreibung. Linktexte müssen das Ziel beschreiben. Statuswerte dürfen nicht nur über Farbe dargestellt werden.

**EN:** Evidence must be understandable as text. Screenshots need a short description. Link text must describe the target. Status values must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [Agentische KI / Agentic AI](#cl-12-glossar-agentische-ki)
- [Sandbox](#cl-12-glossar-sandbox)
- [Container](#cl-12-glossar-container)
- [VM / Virtuelle Maschine](#cl-12-glossar-vm)
- [MicroVM](#cl-12-glossar-microvm)
- [Host-Mount](#cl-12-glossar-host-mount)
- [Isolationsnachweis / Isolation Evidence](#cl-12-glossar-isolation-evidence)
- [Egress-Policy](#cl-12-glossar-egress-policy)
- [Secret Store](#cl-12-glossar-secret-store)
- [Gepinnte Werkzeuge und Modelle / Pinned Tools and Models](#cl-12-glossar-pinned-tools)
- [Provider](#cl-12-glossar-provider)
- [LLM / Large Language Model](#cl-12-glossar-llm)
- [Spec Kit](#cl-12-glossar-spec-kit)
- [SDD / Specification-Driven Development](#cl-12-glossar-sdd)
- [Preset](#cl-12-glossar-preset)
- [Nachweis-Matrix / Evidence Matrix](#cl-12-glossar-nachweis-matrix)
- [Menschliche Prüfung / Human Review](#cl-12-glossar-human-review)
- [Audit-Spur / Audit Trail](#cl-12-glossar-audit-trail)
- [Re-Validierung / Re-Validation](#cl-12-glossar-revalidation)

### Checkliste / Checklist

#### CL-12-01: Initialfreigabe der Sandbox / Initial Sandbox Approval

- **DE:** Die agentische KI läuft nur in einer freigegebenen Sandbox. Diese darf als Container, VM oder gleichwertige Isolation auf dem Entwicklungsrechner laufen. Nicht zulässig ist der Agentenprozess außerhalb der freigegebenen Isolation mit unbeschränktem Zugriff auf Host, Home-Verzeichnis, gemeinsam genutzte oder produktionsnahe Ressourcen. Die Freigabe stammt von einer zuständigen Security-, KI-, Projekt- oder Ausbildungsverantwortung.
- **EN:** Agentic AI runs only in an approved sandbox. The sandbox may run as a container, VM, or equivalent isolation on the development workstation. The prohibited case is the agent process outside approved isolation with unrestricted access to the host, home directory, shared, or production-near resources. Approval comes from a responsible security, AI, project, or training role.
- **Akzeptanz / Acceptance:** Freigabe-Notiz nennt Sandbox-Typ (gemäß RL-Typologie), technischen Identifikator (zum Beispiel Image-Digest oder VM-Snapshot-Hash), genehmigte Werkzeuge und Modelle, genehmigte Mount-Liste, verantwortliche Person und Ablaufdatum. / Approval note states sandbox type (per guideline typology), technical identifier (for example image digest or VM snapshot hash), approved tools and models, approved mount list, responsible person, and expiry date.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Pfad, Link, Ticket oder Konfiguration nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name path, link, ticket, or configuration. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-12-02: Begrenzte Host-Mounts / Limited Host Mounts

- **DE:** Codeänderungen erfolgen nur über ausdrücklich gemountete Host-Verzeichnisse. Der Agent hat keinen allgemeinen Zugriff auf Home-Verzeichnis, Desktop, Downloads oder andere projektfremde Pfade.
- **EN:** Code changes happen only through explicitly mounted host directories. The agent has no general access to the home directory, desktop, downloads, or unrelated paths.
- **Akzeptanz / Acceptance:** Mount-Liste nennt Host-Pfad, Sandbox-Pfad, Zweck und Schreibrechte. / Mount list names host path, sandbox path, purpose, and write permissions.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Mount-Konfiguration oder Betriebsdokument nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name mount configuration or operations document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-12-03: Trennung von Agentendaten und Projektcode / Separation of Agent Data and Project Code

- **DE:** Agentendaten, Sitzungen, Caches und lokale Werkzeugzustände liegen nicht im Projekt-Repository. Sie werden in getrennten Sandbox-Speichern oder nicht versionierten lokalen Bereichen gehalten.
- **EN:** Agent data, sessions, caches, and local tool state are not stored in the project repository. They are kept in separate sandbox storage or unversioned local areas.
- **Akzeptanz / Acceptance:** Repository-Status zeigt keine Agenten-Caches oder privaten Sitzungsdaten. / Repository status shows no agent caches or private session data.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Git-Status, Ignore-Regeln oder Speicherbeschreibung nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name Git status, ignore rules, or storage description. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-12-04: Schutz von Geheimnissen / Secrets Protection

- **DE:** Secrets, API-Keys, Token, Kundendaten und personenbezogene Daten werden nicht in Prompts, Logs, Screenshots oder Projektdateien übernommen. Secret-Dateien sind lokal geschützt und werden nicht committed. Geheimnisse werden über einen Secret Store oder geschützte Umgebungsvariablen in die Sandbox eingebracht, nicht als Klartext-Datei im Projektartefakt.
- **EN:** Secrets, API keys, tokens, customer data, and personal data are not copied into prompts, logs, screenshots, or project files. Secret files are locally protected and not committed. Secrets are injected into the sandbox through a secret store or protected environment variables, not as plain-text files within the project artefact.
- **Akzeptanz / Acceptance:** Secret-Schutz ist dokumentiert; Git-Status und Secret-Scan (zum Beispiel `gitleaks`, `trufflehog`) sind unauffällig; Injektionsweg ist im Sicherheitsdokument benannt. / Secret protection is documented; Git status and secret scan (for example `gitleaks`, `trufflehog`) show no findings; the injection path is named in the security document.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Scan-Ergebnis, `.gitignore`, Ticket oder Freigabe nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name scan result, `.gitignore`, ticket, or approval. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-12-05: Genehmigte und gepinnte Werkzeuge und Modelle / Approved and Pinned Tools and Models

- **DE:** Eingesetzte agentische KI-Werkzeuge (zum Beispiel OpenCode, Codex), Provider, Modelle und Konfigurationen sind im KI-Werkzeug-Inventar der Organisation geführt und freigegeben. Werkzeug-Versionen, Container-Image-Digests und Modell-Identifikatoren sind reproduzierbar gepinnt. Selbst-aktualisierende Mechanismen sind deaktiviert.
- **EN:** Agentic AI tools in use (for example OpenCode, Codex), providers, models, and configurations are listed in the Organisation AI tool inventory and approved. Tool versions, container image digests, and model identifiers are reproducibly pinned. Self-updating mechanisms are disabled.
- **Akzeptanz / Acceptance:** Inventar-Eintrag, Versionsausgabe (`tool --version`), Image-Digest und Modell-IDs sind dokumentiert; im Werkzeug ist kein Auto-Update aktiv; die KI-Lieferkettentransparenz nach CL_KI-Codeerzeugung Prüfpunkt 15 ist berücksichtigt. / Inventory entry, version output (`tool --version`), image digest, and model IDs are documented; auto-update is disabled in the tool; AI supply-chain transparency per CL_KI-Codeerzeugung item 15 is taken into account.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Freigabe, Konfiguration oder Versionsausgabe nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name approval, configuration, or version output. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-12-06: GitHub Spec Kit und Governance-Presets / GitHub Spec Kit and Governance Presets

- **DE:** Wenn agentische KI Feature-Implementierungen oder andere strukturierte Entwicklungsarbeit übernimmt, ist GitHub Spec Kit im Projekt initialisiert. Feature-Implementierungen folgen dem Spec-Driven-Development-Ablauf (SDD): `/speckit.constitution`, `/speckit.specify`, `/speckit.clarify`, `/speckit.plan`, `/speckit.checklist`, `/speckit.tasks`, `/speckit.analyze`, `/speckit.implement`. Die acht Governance-Presets sind installiert oder eine begründete Ausnahme ist dokumentiert. Bei dauerhafter Nutzung ist `.specify/presets/` Teil der Projekt-Policy; lokale Caches wie `.specify/presets/.cache/` werden nicht versioniert. Wenn die Spec-Kit-Artefakte die anwendbaren Prüfpunkte vollständig abdecken, gelten sie als gleichwertige Nachweisführung zur separaten manuellen Ausfüllung. Die Installation der Autonomous-Presets startet keinen Lauf und erteilt keine Remote-, Merge-, Bypass-, Secret- oder Provider-Rechte.
- **EN:** If agentic AI performs feature implementations or other structured development work, GitHub Spec Kit is initialized in the project. Feature implementations follow the spec-driven development (SDD) flow: `/speckit.constitution`, `/speckit.specify`, `/speckit.clarify`, `/speckit.plan`, `/speckit.checklist`, `/speckit.tasks`, `/speckit.analyze`, `/speckit.implement`. The eight governance presets are installed or a justified exception is documented. For permanent use, `.specify/presets/` is part of the project policy; local caches such as `.specify/presets/.cache/` are not versioned. If the Spec Kit artefacts fully cover the applicable review points, they count as equivalent evidence instead of separate manual completion. Installing the autonomous presets starts no run and grants no remote, merge, bypass, secret, or provider authority.
- **Akzeptanz / Acceptance:** Spec-Kit-Artefakte zum Feature (`specs/<feature>/spec.md`, `plan.md`, `tasks.md`), Checklisten- oder Analyseergebnis, Preset-Liste (zum Beispiel `specify preset list`) sowie dokumentierte Preset-Versionen und Prioritäten sind vorhanden; alternativ liegt ein genehmigtes Ausnahme-Ticket vor. Detailprüfungen wie `specify preset info ...` oder `specify preset resolve ...` liegen bei Prüfbedarf vor. Eine Nachweis-Matrix oder ein eindeutiger Verweis ordnet die anwendbaren CL-Prüfpunkte den Spec-Kit-Artefakten, Review-Nachweisen und Preset-Nachweisen zu. / Spec Kit artefacts for the feature (`specs/<feature>/spec.md`, `plan.md`, `tasks.md`), checklist or analysis result, preset list (for example `specify preset list`), and documented preset versions and priorities are available; alternatively an approved exception ticket exists. Detail checks such as `specify preset info ...` or `specify preset resolve ...` are available when needed for audit. An evidence matrix or clear reference maps the applicable CL review points to the Spec Kit artefacts, review evidence, and preset evidence.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Spec-Kit-Artefakte, SDD-Checklisten- oder Analyseergebnis, Nachweis-Matrix, Preset-Liste, Preset-Versionen und Prioritäten, Projekt-Policy-Pfad `.specify/presets/`, Cache-Ausschluss oder Ticket nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name Spec Kit artefacts, SDD checklist or analysis result, evidence matrix, preset list, preset versions and priorities, project policy path `.specify/presets/`, cache exclusion, or ticket. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-12-07: Menschliche Prüfung / Human Review

- **DE:** Agentische Änderungen werden vor Commit und Merge durch Menschen geprüft. Sicherheitskritische Logik wird zusätzlich nach dem Vier-Augen-Prinzip geprüft.
- **EN:** Agentic changes are reviewed by humans before commit and merge. Security-critical logic is also reviewed under the four-eyes rule.
- **Akzeptanz / Acceptance:** Pull Request, Review oder Freigabeprotokoll zeigt Prüfumfang und Ergebnis. / Pull request, review, or approval record shows review scope and result.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _PR, Review-Protokoll oder Ticket nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name PR, review record, or ticket. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-12-08: Audit-Spur und Nachvollziehbarkeit / Audit Trail and Traceability

- **DE:** Die Agentennutzung ist nachvollziehbar. Dokumentiert werden Werkzeug, Sandbox-Typ, Sandbox-Identifikator (zum Beispiel Image-Digest oder VM-Snapshot-Hash), Projektpfad, Zeitraum, verantwortliche Person, Ziel der Änderung, Review-Ergebnis und relevante Spec-Kit-Artefakte.
- **EN:** Agent use is traceable. Document the tool, sandbox type, sandbox identifier (for example image digest or VM snapshot hash), project path, time period, responsible person, purpose of the change, review result, and relevant Spec-Kit artefacts.
- **Akzeptanz / Acceptance:** Audit-Eintrag oder Projektakte enthält alle Mindestangaben einschließlich des technischen Sandbox-Identifikators. / Audit entry or project record contains all minimum information including the technical sandbox identifier.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Audit-Eintrag, Projektakte, Ticket oder PR nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name audit entry, project record, ticket, or PR. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-12-09: Sandbox-Typologie und Isolationsnachweis / Sandbox Typology and Isolation Evidence

- **DE:** Die Sandbox gehört zu einem der in der Richtlinie definierten Typen: Container-Sandbox, MicroVM-Sandbox, klassische VM-Sandbox oder dedizierter Workstation-Host. Andere Isolationsmechanismen sind zulässig, wenn ein vergleichbares Schutzniveau im Sicherheits- oder Architekturdokument der Sandbox nachgewiesen ist.
- **EN:** The sandbox belongs to one of the types defined in the guideline: container sandbox, microVM sandbox, classic VM sandbox, or dedicated workstation host. Other isolation mechanisms are allowed if a comparable protection level is evidenced in the sandbox security or architecture document.
- **Akzeptanz / Acceptance:** Sandbox-Typ ist benannt; technischer Isolationsmechanismus (zum Beispiel nicht-privilegierte Container-Ausführung, hardware-unterstützte Virtualisierung) und Schutzniveau-Begründung sind dokumentiert. / Sandbox type is named; technical isolation mechanism (for example unprivileged container execution, hardware-assisted virtualization) and protection-level reasoning are documented.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Architektur- oder Sicherheitsdokument der Sandbox nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name sandbox architecture or security document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-12-10: Netzwerkrestriktion / Network Restriction

- **DE:** Netzwerkzugriffe der Sandbox sind auf die nötigen Ziele beschränkt: genehmigte Modell-Endpunkte, genehmigte Paketregistries, projektbezogene VCS-Endpunkte. Andere ausgehende Verbindungen sind blockiert oder dokumentiert begründet.
- **EN:** Sandbox network access is restricted to the required targets: approved model endpoints, approved package registries, project-related VCS endpoints. Other outbound connections are blocked or documented with reasoning.
- **Akzeptanz / Acceptance:** Egress-Policy ist beschrieben (Allow-List, Proxy oder begründete Offenheit mit Ablaufdatum); Konfiguration ist nachvollziehbar (zum Beispiel Compose-/Pod-Network-Block, Firewall-Regel). / Egress policy is described (allow-list, proxy, or documented openness with expiry date); configuration is traceable (for example Compose/Pod network block, firewall rule).
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Netzwerkkonfiguration, Proxy-Regel, Sicherheitsentscheid nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name network configuration, proxy rule, or security decision. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-12-11: Re-Validierungsstand und Lebenszyklus / Re-Validation Status and Lifecycle

- **DE:** Die letzte Freigabe ist höchstens zwölf Monate alt. Außerplanmäßige Re-Validierungen nach Änderungen an Werkzeug-Versionen, Modell-Liste, Mount-Liste, Provider-Auswahl oder Netzwerkrichtlinie sind nachweisbar. Ein Entzug der Freigabe ist mit Datum, Grund und Wiederinbetriebnahme-Bedingung dokumentiert, falls eingetreten.
- **EN:** The latest approval is at most twelve months old. Unscheduled re-validations after changes to tool versions, model list, mount list, provider selection, or network policy are evidenced. A withdrawal of approval, if it occurred, is documented with date, reason, and re-entry condition.
- **Akzeptanz / Acceptance:** Freigabedatum und Ablaufdatum sind dokumentiert; bei Pilotbetrieb ist der Pilotcharakter benannt; Re-Validierungs- und Entzugsereignisse sind im Sicherheitsregister oder Projektakte vermerkt. / Approval date and expiry date are documented; for pilot operation the pilot status is named; re-validation and withdrawal events are recorded in the security register or project record.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Freigabe-Notiz, Re-Validierungsprotokoll oder Sicherheitsregister-Eintrag nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name approval note, re-validation record, or security-register entry. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-12-12: Preset-Aktualisierung und Inhaltsabdeckung / Preset Updates and Content Coverage

- **DE:** Die acht Spec-Kit-Governance-Presets werden mindestens quartalsweise und anlassbezogen auf Aktualität geprüft. Anlassbezogen ist die Prüfung nötig, wenn sich Preset-Kataloge, Preset-Inhalte, Prioritäten, projektlokale Overrides oder RL-/CL-Dokumentation zu Spec Kit ändern. Preset-Änderungen werden nicht automatisch übernommen, sondern mit Quelle, Version, Priorität, wirksamer Auflösung und Review-Ergebnis dokumentiert.
- **EN:** The eight Spec Kit governance presets are checked for currency at least quarterly and when triggered by a relevant change. A triggered check is required when preset catalogs, preset contents, priorities, project-local overrides, or RL/CL documentation about Spec Kit change. Preset changes are not applied automatically; they are documented with source, version, priority, effective resolution, and review result.
- **Preset-zu-CL-Mapping / Preset-to-CL mapping:** `security-governance` -> CL_01, CL_05, CL_06, CL_07, CL_08, CL_09; `architecture-governance` -> CL_02, CL_04; `isaqb-architecture-governance` -> CL_02; `a11y-governance` -> A11Y-Hinweise im Sammelband, CL_06 bei Advisories, CL_09 bei didaktischen Kommentaren, CL_10 bei CLI-/Tooling-Artefakten; `cross-platform-governance` -> CL_08 und CL_10; `agent-parity-governance` -> CL_12, AGENTS.md und Spec-Kit-Templates; `autonomous-run-governance` -> CL_09 und CL_12; `parallel-autonomous-run-governance` -> CL_09 und CL_12.
- **Akzeptanz / Acceptance:** `specify preset list`, `specify preset info ...` und relevante `specify preset resolve ...`-Nachweise liegen vor; Preset-Quelle oder Katalogeintrag wurde vor Installation oder Aktualisierung geprüft; Update- oder Ausnahme-Ticket nennt Review-Datum, verantwortliche Person und betroffene RL-/CL-Prüfpunkte. Die Mapping-Prüfung nennt Preset-Versionen, Prioritäten, wirksame Auflösung, Evidence-Matrix, offene Mapping-Lücken und begründete N/A-Entscheidungen. Sie zeigt, ob die Spec-Kit-Dokumentation die separate Checklisten-Ausfüllung vollständig ersetzt oder welche Prüfpunkte separat nachzudokumentieren sind. / `specify preset list`, `specify preset info ...`, and relevant `specify preset resolve ...` evidence are available; the preset source or catalog entry was reviewed before installation or update; the update or exception ticket names the review date, responsible person, and affected RL/CL review points. The mapping check names preset versions, priorities, effective resolution, evidence matrix, open mapping gaps, and justified N/A decisions. It shows whether the Spec Kit documentation fully replaces separate checklist completion or which review points need separate follow-up documentation.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Preset-Liste, Info-/Resolve-Ausgabe, Update-Protokoll, Mapping, PR oder Ausnahme-Ticket nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name preset list, info/resolve output, update record, mapping, PR, or exception ticket. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

### Akzeptanzkriterien gesamt / Overall Acceptance

**DE:** Erfüllt, wenn agentische KI nur in formell freigegebenen Sandbox-Umgebungen läuft, der Sandbox-Typ und der Isolationsnachweis dokumentiert sind, Host-Mounts und Netzwerkzugriffe begrenzt sind, Werkzeuge und Modelle reproduzierbar gepinnt sind, Secrets über einen Secret Store oder geschützte Umgebungsvariablen eingebracht werden, Feature-Implementierungen über Spec Kit mit SDD-Ablauf und nachweisbar dokumentierten Governance-Presets gesteuert werden oder eine Ausnahme dokumentiert ist, Preset-Aktualität und Inhaltsabdeckung geprüft sind, eine vollständige Nachweis-Matrix die anwendbaren Prüfpunkte abdeckt oder offene Punkte separat dokumentiert, menschliches Review nachweisbar erfolgt und die Freigabe gültig (höchstens zwölf Monate alt) ist.

**EN:** Fulfilled when agentic AI runs only in formally approved sandbox environments, sandbox type and isolation evidence are documented, host mounts and network access are limited, tools and models are reproducibly pinned, secrets are injected via a secret store or protected environment variables, feature implementations are controlled through Spec Kit with the SDD flow and governance presets documented with evidence or an exception is documented, preset currency and content coverage are checked, a complete evidence matrix covers the applicable review points or open points are documented separately, human review is evidenced, and the approval is valid (no older than twelve months).

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-12-glossar-agentische-ki"></a>

#### Agentische KI / Agentic AI

- **DE:** Agentische KI kann mehrere Schritte selbst planen und Werkzeuge ausführen, zum Beispiel Dateien ändern oder Befehle starten. Darum braucht sie klare Grenzen und Review.
- **EN:** Agentic AI can plan several steps and run tools itself, for example changing files or starting commands. Therefore it needs clear limits and review.

<a id="cl-12-glossar-sandbox"></a>

#### Sandbox

- **DE:** Eine Sandbox ist eine begrenzte Umgebung für riskante oder kontrollpflichtige Arbeiten. Sie trennt Agent, Werkzeuge und Projektzugriffe vom normalen Arbeitsplatz.
- **EN:** A sandbox is a limited environment for risky or controlled work. It separates agent, tools, and project access from the normal workstation.

<a id="cl-12-glossar-container"></a>

#### Container

- **DE:** Ein Container ist eine isolierte Laufzeitumgebung für Prozesse und Abhängigkeiten. Er ist leichter als eine VM, aber nicht automatisch eine vollständige Sicherheitsgrenze.
- **EN:** A container is an isolated runtime environment for processes and dependencies. It is lighter than a VM but not automatically a complete security boundary.

<a id="cl-12-glossar-vm"></a>

#### VM / Virtuelle Maschine

- **DE:** Eine VM ist ein virtueller Computer mit eigenem Betriebssystem. Sie kann stärkere Trennung bieten als ein einfacher Container.
- **EN:** A VM is a virtual computer with its own operating system. It can provide stronger separation than a simple container.

<a id="cl-12-glossar-microvm"></a>

#### MicroVM

- **DE:** Eine MicroVM ist eine besonders schlanke virtuelle Maschine. Sie wird genutzt, wenn kurze Startzeit und stärkere Isolation kombiniert werden sollen.
- **EN:** A MicroVM is a very lightweight virtual machine. It is used when fast startup and stronger isolation should be combined.

<a id="cl-12-glossar-host-mount"></a>

#### Host-Mount

- **DE:** Ein Host-Mount bindet ein Verzeichnis des Host-Systems in eine Sandbox oder einen Container ein. Er muss begrenzt sein, weil darüber Dateien verändert werden können.
- **EN:** A host mount connects a host-system directory into a sandbox or container. It must be limited because files can be changed through it.

<a id="cl-12-glossar-isolation-evidence"></a>

#### Isolationsnachweis / Isolation Evidence

- **DE:** Ein Isolationsnachweis zeigt, welche technische Trennung wirklich aktiv ist. Beispiele sind Container-/VM-Konfiguration, Netzwerkregeln oder Sicherheitsprofile.
- **EN:** Isolation evidence shows which technical separation is actually active. Examples are container or VM configuration, network rules, or security profiles.

<a id="cl-12-glossar-egress-policy"></a>

#### Egress-Policy

- **DE:** Eine Egress-Policy regelt ausgehende Netzwerkverbindungen. Sie legt fest, welche Ziele eine Sandbox oder Anwendung erreichen darf.
- **EN:** An egress policy controls outbound network connections. It defines which destinations a sandbox or application may reach.

<a id="cl-12-glossar-secret-store"></a>

#### Secret Store

- **DE:** Ein Secret Store speichert Geheimnisse wie Passwörter, API-Schlüssel oder Tokens geschützt. Geheimnisse sollen nicht im Code, in Logs oder in Tickets stehen.
- **EN:** A secret store protects secrets such as passwords, API keys, or tokens. Secrets should not be in code, logs, or tickets.

<a id="cl-12-glossar-pinned-tools"></a>

#### Gepinnte Werkzeuge und Modelle / Pinned Tools and Models

- **DE:** Gepinnt bedeutet, dass Versionen festgelegt sind. Dadurch ist später nachvollziehbar, mit welchem Werkzeug, Modell oder Image gearbeitet wurde.
- **EN:** Pinned means that versions are fixed. This makes it traceable later which tool, model, or image was used.

<a id="cl-12-glossar-provider"></a>

#### Provider

- **DE:** Ein Provider ist ein externer oder interner Dienstanbieter, zum Beispiel für Cloud, KI-Modell, Paketregistry oder Identitätsdienst.
- **EN:** A provider is an external or internal service provider, for example for cloud, AI model, package registry, or identity service.

<a id="cl-12-glossar-llm"></a>

#### LLM / Large Language Model

- **DE:** Ein LLM ist ein großes Sprachmodell. Es erzeugt Text oder Code aus Prompts, versteht aber nicht automatisch Projektregeln oder Sicherheitsanforderungen.
- **EN:** An LLM is a large language model. It generates text or code from prompts but does not automatically understand project rules or security requirements.

<a id="cl-12-glossar-spec-kit"></a>

#### Spec Kit

- **DE:** Spec Kit ist ein werkzeuggestützter Ablauf für spezifikationsgetriebene Entwicklung. Es erzeugt und nutzt Markdown-Artefakte wie Spezifikation, Plan, Aufgaben und Analysen.
- **EN:** Spec Kit is a tool-supported flow for specification-driven development. It creates and uses Markdown artefacts such as specification, plan, tasks, and analyses.

<a id="cl-12-glossar-sdd"></a>

#### SDD / Specification-Driven Development

- **DE:** SDD bedeutet spezifikationsgetriebene Entwicklung. Erst werden Anforderungen, Plan und Prüfpunkte dokumentiert, danach wird umgesetzt.
- **EN:** SDD means specification-driven development. Requirements, plan, and review points are documented first; implementation follows afterwards.

<a id="cl-12-glossar-preset"></a>

#### Preset

- **DE:** Ein Preset ist eine vordefinierte Regel- oder Vorlagensammlung. Bei Spec Kit steuert es Governance, Prüfpunkte und erwartete Artefakte.
- **EN:** A preset is a predefined set of rules or templates. In Spec Kit it controls governance, review points, and expected artefacts.

<a id="cl-12-glossar-nachweis-matrix"></a>

#### Nachweis-Matrix / Evidence Matrix

- **DE:** Eine Nachweis-Matrix verbindet Prüfpunkte mit konkreten Belegen. Sie zeigt, welches Dokument, Ticket oder Artefakt welchen Prüfpunkt abdeckt.
- **EN:** An evidence matrix connects review points with concrete evidence. It shows which document, ticket, or artefact covers which review item.

<a id="cl-12-glossar-human-review"></a>

#### Menschliche Prüfung / Human Review

- **DE:** Menschliche Prüfung bedeutet, dass eine verantwortliche Person das Ergebnis bewertet. Bei KI-Ergebnissen ersetzt diese Prüfung kein Tool automatisch.
- **EN:** Human review means that a responsible person assesses the result. For AI outputs, this review is not automatically replaced by a tool.

<a id="cl-12-glossar-audit-trail"></a>

#### Audit-Spur / Audit Trail

- **DE:** Eine Audit-Spur zeigt nachvollziehbar, wer was wann getan oder entschieden hat. Sie hilft bei Prüfung, Fehlersuche und Verantwortung.
- **EN:** An audit trail traceably shows who did or decided what and when. It helps with review, troubleshooting, and accountability.

<a id="cl-12-glossar-revalidation"></a>

#### Re-Validierung / Re-Validation

- **DE:** Re-Validierung ist eine erneute Prüfung nach Zeitablauf oder Änderung. Sie stellt sicher, dass eine frühere Freigabe noch passt.
- **EN:** Re-validation is a renewed review after time passes or changes occur. It ensures that an earlier approval still fits.

### Versionshistorie / Version History

- **Version 1.0 (2026-05-07):** Erstfassung zur agentischen KI-Nutzung in Sandbox-Umgebungen / Initial version for agentic AI use in sandbox environments
- **Version 1.1 (2026-05-14):** Prüfpunkte 1, 4, 5 und 8 mit Sandbox-Identifikator, Secret-Store-Pflicht und reproduzierbarem Pinning präzisiert; drei neue Prüfpunkte ergänzt: 9 Sandbox-Typologie und Isolationsnachweis, 10 Netzwerkrestriktion, 11 Re-Validierungsstand und Lebenszyklus; Mitgeltende Dokumente um ISO/IEC 27001:2022 Annex A, NIST AI RMF und OWASP LLM Top 10 ergänzt; synchron mit Richtlinie Sichere Entwicklung v2.2.0. / Refined checklist items 1, 4, 5, and 8 with sandbox identifier, secret-store requirement, and reproducible pinning; added three new checklist items: 9 sandbox typology and isolation evidence, 10 network restriction, 11 re-validation status and lifecycle; extended related documents with ISO/IEC 27001:2022 Annex A, NIST AI RMF, and OWASP LLM Top 10; synchronized with Richtlinie Sichere Entwicklung v2.2.0.
- **Version 1.2 (2026-05-19):** Prüfpunkt 1 erweitert: Die Initialfreigabe der Sandbox kann neben der oder dem Security-Verantwortlichen (Security-Verantwortliche*r) auch durch die oder den KI-Verantwortlichen erteilt werden; synchron mit Richtlinie Sichere Entwicklung v2.3.0. / Extended checklist item 1: initial sandbox approval may be granted not only by the Information Security Officer (Security-Verantwortliche*r) but also by the AI Officer (KI-Verantwortliche*r); synchronized with Richtlinie Sichere Entwicklung v2.3.0.
- **Version 1.3 (2026-05-19):** Prüfpunkt 5 um einen Querverweis auf die KI-Lieferkettentransparenz (CL_KI-Codeerzeugung Prüfpunkt 15) erweitert; synchron mit Richtlinie Sichere Entwicklung v2.4.0. / Extended checklist item 5 with a cross-reference to AI supply-chain transparency (CL_KI-Codeerzeugung item 15); synchronized with Richtlinie Sichere Entwicklung v2.4.0.
- **Version 1.4 (2026-05-22):** Prüfpunkt 6 präzisiert: Feature-Implementierungen mit agentischer KI folgen dem Spec-Kit-SDD-Ablauf und weisen Spec-Kit-Artefakte, Analyse- oder Checklisten-Ergebnisse sowie die Governance-Presets nach; synchron mit Richtlinie Sichere Entwicklung v2.5.0. / Refined checklist item 6: feature implementations with agentic AI follow the Spec Kit SDD flow and evidence Spec Kit artefacts, analysis or checklist results, and the governance presets; synchronized with Richtlinie Sichere Entwicklung v2.5.0.
- **Version 1.5 (2026-06-12):** Prüfpunkt 6 präzisiert: Preset-Liste, Preset-Versionen und Prioritäten sind als Nachweise zu führen; `.specify/presets/` ist bei dauerhafter Nutzung Projekt-Policy, lokale Preset-Caches werden nicht versioniert; synchron mit Richtlinie Sichere Entwicklung v2.6.0. / Refined checklist item 6: preset list, preset versions, and priorities must be evidenced; `.specify/presets/` is project policy for permanent use, local preset caches are not versioned; synchronized with Richtlinie Sichere Entwicklung v2.6.0.
- **Version 1.6 (2026-06-14):** Prüfpunkt 12 ergänzt: Spec-Kit-Governance-Presets werden mindestens quartalsweise und anlassbezogen auf Aktualität, wirksame Auflösung und Abdeckung durch RL-/CL-Prüfpunkte geprüft; synchron mit Richtlinie Sichere Entwicklung v2.7.0. / Added checklist item 12: Spec Kit governance presets are checked at least quarterly and on relevant changes for currency, effective resolution, and coverage by RL/CL review points; synchronized with Richtlinie Sichere Entwicklung v2.7.0.
- **Version 1.7 (2026-06-14):** Prüfpunkt 6 und 12 präzisiert: Vollständige Spec-Kit-Artefakte mit Nachweis-Matrix gelten als gleichwertige Nachweisführung und können die separate manuelle Checklisten-Ausfüllung ersetzen; synchron mit Richtlinie Sichere Entwicklung v2.8.0. / Refined checklist items 6 and 12: complete Spec Kit artefacts with an evidence matrix count as equivalent evidence and may replace separate manual checklist completion; synchronized with Richtlinie Sichere Entwicklung v2.8.0.
- **Version 1.8 (2026-06-15):** Prüfpunkt 12 mit erweitertem Preset-zu-CL-Mapping, Preset-Versionen, Prioritäten, wirksamer Auflösung und offenen Mapping-Lücken präzisiert; synchron mit Richtlinie Sichere Entwicklung v2.9.0. / Refined item 12 with extended preset-to-CL mapping, preset versions, priorities, effective resolution, and open mapping gaps; synchronized with Richtlinie Sichere Entwicklung v2.9.0.
- **Version 1.9 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.10 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.
- **Version 1.11 (2026-06-26):** Mitgeltende Leitlinie Sichere Entwicklungs-Sandbox und Referenzprofil `absdd-image-sandbox` ergaenzt; MSL-basierte Level-2-Entwicklung mit KI-Agenten als Sandbox-Zielbild eingeordnet. / Added related Secure Development Sandbox Guideline and `absdd-image-sandbox` reference profile; positioned MSL-based level-2 development with AI agents as the sandbox target picture.

---

- **Version 2.0.0 (2026-07-10):** Einheitliches zweiachsiges Statusmodell, stabile CL-IDs, Lernstufe, Rollen-, Evidenz-, Restrisiko- und Neubewertungsfelder sowie klare Trennung zwischen Vorlage und Projektnachweis eingeführt; synchron mit sichere-Entwicklung-Basis 3.0.0. / Added the unified two-axis status model, stable CL IDs, learning-stage, role, evidence, residual-risk, and re-evaluation fields, plus clear separation between template and project evidence; synchronized with secure-development baseline 3.0.0.
- **Version 2.1.0 (2026-07-17):** Verbindliches Siebenerprofil einschließlich `autonomous-run-governance` aufgenommen; Installation von autonomer Ausführung und Remote-Berechtigungen abgegrenzt; synchron mit sichere-Entwicklung-Basis 3.1.0. / Added the binding seven-preset profile including `autonomous-run-governance`; separated installation from autonomous execution and remote authority; synchronized with secure-development baseline 3.1.0.
- **Version 2.2.0 (2026-07-19):** Verbindliches Achterprofil einschließlich `parallel-autonomous-run-governance` aufgenommen; Installation von Kampagnenstart und Remote-Berechtigungen abgegrenzt; synchron mit sichere-Entwicklung-Basis 3.2.0. / Added the binding eight-preset profile including `parallel-autonomous-run-governance`; separated installation from campaign start and remote authority; synchronized with secure-development baseline 3.2.0.
