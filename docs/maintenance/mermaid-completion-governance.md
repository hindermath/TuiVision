# Dokumentationsentscheidung / Documentation decision

- ID: DOC-MERMAID-REPORT-20260913
- Entscheidung / decision: UpdateRequired
- Quelle / source: ausdruecklich beauftragter Plan fuer Mermaid und Abschlussberichte;
  docs/spec-kit-diagrams-and-completion-reports.md ist die gemeinsame Regelquelle.
- Owner: Repository-Maintainer / repository maintainer.
- Zielgruppen / audiences: Maintainer, Lernende, Agenten / maintainers, learners, agents.
- Leserpfad / reader path: README → gemeinsame Regel → Lastenheft-Profil und
  Berichtsvorlage → Feature-Bericht und Evidence / rule, templates, report, evidence.
- Dokumentklasse / class: normative Governance und Vorlagen / governance and templates.
- Betroffene Dokumente / affected docs: Constitution-Paar, fuenf Agentenflaechen,
  Spec-/Plan-/Tasks-/Intake-Profil, Berichtsvorlage, Navigation; vorhandene lokale
  Erweiterungen bleiben erhalten / local extensions are preserved.
- Sprache / language: Deutsch zuerst, Englisch danach; bilingual in one document.
- Plattform/A11Y / platform and accessibility: lesbarer Mermaid-Quelltext, lokale
  Renderpruefung, Textalternative, Labels ohne reine Farbcodierung; keine Produkt-UI.
- Distribution: Regeln/Vorlagen sourceOnly; zentrale Agent-Anweisungen und
  Constitution homeRuntime nach bestehendem Home-Sync-Manifest. Level-2 hat keinen Home-Sync.
- Evidence: PR-Diff, lokale Inhalts-/Paritaets-/Mermaid-Pruefung und Repository-CI.
- Statistik / statistics: bestehender Renderer und Ledger; dessen erzeugter Block
  folgt dem vorhandenen GeneratedUpdate-Vertrag, keine zweite Entscheidung fuer diese Regel.
- Wiedervorlage / reevaluation: geaenderte Diagrammquelle, Berichtsdaten, Agent-Oberflaeche,
  Spec-Kit-Update oder aendernde Distribution / source, evidence, agent, updater or distribution changes.
- Nicht anwendbar / N/A: Produkt-TDD, Runtime-Coverage, manuelle UI-Pruefung und
  Security-Funktionsfreigabe, da ausschliesslich Governance-Markdown geaendert wird.
  Vorhandene Validatoren und erforderliche CI-Gates werden trotzdem geprueft.
- Restrisiko / residual risk: Agenten muessen den Bericht tatsächlich anzeigen;
  versionierte Regeln sind keine automatische UI-Ausfuehrung / guidance is not a UI execution hook.

Profilbindung / profile binding: .specify/templates/intake-authoring-level2-lastenheft-profile.md
Bootstrap-Quellen / bootstrap sources: alle vorhandenen lokalen Agent-Templates tragen dieselbe Regel; lokale Erweiterungen erhalten. / Existing local agent source templates contain the same rule; extensions preserved.
