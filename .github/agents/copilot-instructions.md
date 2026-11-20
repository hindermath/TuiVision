# TuiVision Development Guidelines

<!-- MANUAL ADDITIONS START -->

## Verbindlicher Einstieg / Required starting point

DE: Arbeite im Repository TuiVision. Prüfe die Projektkonstitution und die nachstehenden
Build-, Test- und Freigaberegeln. Verwende `TuiVision.sln` für die projektweiten Prüfungen.
Halte gemeinsame Regeln in allen fünf Agenten-Dateien synchron und dokumentiere Abweichungen.
Bei Level-2-Arbeit die anwendbaren Sicherheitsstandards nennen, `N/A` begründen und die
Nachweise im Plan und in den Aufgaben festhalten. Sichere Eingabegrenzen, minimale
Berechtigungen und geschützte Zugangsdaten sind verbindlich. Prüfe anwendbare WCAG 2.2 AA
Kriterien und dokumentiere offene Nachweise. Pflege die Projektstatistik nach den markierten Regeln.

EN: Work in the TuiVision repository. Read the project constitution and the build,
test and delivery rules below. Use `TuiVision.sln` for solution-wide checks.
Keep shared rules in sync across all five agent files and document differences.
Name the applicable security standards for Level 2 work, justify `N/A`, and record
evidence in plans and tasks. Validate trust boundaries, apply least privilege and
protect credentials. Check applicable WCAG 2.2 AA criteria and record gaps in evidence.
Maintain project statistics according to the marked rules.

[Verbindliche Detailregeln / Required detailed rules](../../docs/agent-guidance.md):
Lies vor der Arbeit die Abschnitte zu deiner Aufgabe, insbesondere vor Änderungen an Code,
Architektur, Agentenwerkzeugen oder Spec-Kit-Presets.
Read the sections relevant to your task before work, especially before changing code,
architecture, agent tooling or Spec Kit presets.

Die aktuellen TuiVision-Verträge sind maßgeblich. Nutze `tv203s/` als schreibgeschützte
historische Referenz und halte die Drei-Achsen-Quellenpolicy bei abgeleitetem Verhalten ein.
Beachte den Coverage-Grenzwert von 70 Prozent und den DocFX-A11Y-Nachweis, wo anwendbar.
Current TuiVision contracts are authoritative. Treat `tv203s/` as a read-only historical
reference and apply the three-axis source policy to derived behavior. Observe the 70
percent coverage gate and the DocFX accessibility checks where applicable.

[Featureverlauf und technische Referenzen / Feature history and technical references](../../docs/agent-feature-context.md)

<!-- MANUAL ADDITIONS END -->

## Project Structure

```text
src/
tests/
examples/
docs/guides/examples/
```

## Commands

```bash
dotnet restore
dotnet build --configuration Release
dotnet test
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
dotnet test tests/TuiVision.Examples.SmokeTests/
cd tests/web-a11y && npm install && npx playwright install chromium && npm run test:docfx
docfx docfx.json && cd tests/web-a11y && npm run test:docfx
```

## Freigegebenes Assurance-Profil / Approved Assurance Profile

Die erhaltene Assurance-Basis ist das ausdruecklich freigegebene 13er-Profil mit
`secure-development-assurance-governance` v0.1.10, aktiviert mit Priorität 15.
Die bisherigen zwölf Presets und älteren Profile bleiben unverändert.
`status` ist strikt lesend; `review` benötigt einen ausdrücklich benannten
Kontext und Auftrag. Keine automatische GSDB-/C5-Prüfung oder Freigabe.
Produktquelle ist ausschließlich das veröffentlichte eigenständige GitHub-Repo.
Bedienung, Paketbindung, Evidence-Lücken und Grenzen:
`docs/maintenance/secure-development-assurance-integration.md`.

*The preserved assurance base is the explicitly approved thirteen-preset profile with
Assurance v0.1.3, enabled at priority 15. Preserve the existing twelve presets
and older profiles. Status is read-only; review requires an explicit context
and authority. No automatic GSDB/C5 assessment or approval. The published
standalone GitHub repository is the only product source. See the integration
record above for usage, provenance, evidence gaps and boundaries.*

## Intake lifecycle preset rollout / Intake-Lifecycle-Preset-Rollout

DE: TuiVision verwendet Authoring 0.3.2 (Prioritaet 64), Review 0.2.2 (65)
und Sequencing 0.2.4 (66). Abgeschlossene Serienmitglieder bleiben im Archiv;
die abgeschlossene Lieferreihe hat keine ausfuehrbaren Ziele. Historische
Receipts bleiben unveraendert und benoetigen einen eindeutigen Archivnachweis.
Die projektspezifische Requirements-Pruefung bleibt zusaetzlich verbindlich.
Release- und Pruefnachweise: `docs/maintenance/intake-lifecycle-preset-rollout.md`.

EN: TuiVision uses Authoring 0.3.2 (priority 64), Review 0.2.2 (65), and
Sequencing 0.2.4 (66). Completed members remain archived; the completed delivery
series has no executable targets. Historical receipts remain unchanged and need
unique archive evidence. Project-specific requirements validation remains
mandatory. Release and validation evidence:
`docs/maintenance/intake-lifecycle-preset-rollout.md`.

<!-- statistics-profile-2-guidance:begin -->
## Statistikprofil 2 / Statistics Profile 2

- Verbindlich sind `docs/project-statistics.config.json` und der markierte Profil-2-Block in `docs/project-statistics.md`; aktualisieren mit `render-project-statistics.*`.
- Profil 2 zeigt exakte KPI, Artefaktmix, 52-Wochen-Aktivitaet, Wochen- und kumulatives Volumen, belastbare Phasen oder Monatsfallback sowie Speedup-Vergleiche.
- Nur ASCII verwenden: Heatmap `0..4`, `-` fuer noch nicht abgelaufene Tage und Gauges `#`/`.`; jedes Textdiagramm bleibt hoechstens 100 Zeichen breit.
- Jede Grafik braucht genaue Zahlen und eine bilinguale CEFR-B2-Textalternative, Deutsch zuerst und Englisch danach.
- Methodik v2 wertet Git-getrackten Text und Bruttoaenderungen aus Nicht-Merge-Commits aus; Ledger, `STATS.md` und Binaerdaten bleiben ausgeschlossen.
- Referenzen dieses Repositories: `80` Zeilen/Arbeitstag konservativ und `125` Zeilen/Arbeitstag Thorsten-Solo. Speedup bleibt Lieferdichte, keine Stoppuhr- oder Personenbewertung.
- Dieser Vertrag ersetzt aeltere Visualisierungsvorgaben; historische Ledger-Eintraege und archivierte Profil-1-Diagramme bleiben unveraendert.
- Gemeinsame Aenderungen werden synchron in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `.github/agents/copilot-instructions.md` gepflegt.

*Profile 2 is governed by the JSON configuration and generated marker block. Use ASCII `0..4`, `-`, and `#`/`.`, exact values, German-first bilingual CEFR-B2 alternatives, and a 100-character chart limit. Methodology v2 excludes the ledger, `STATS.md`, and binaries. This repository uses manual references of `80` and `125` lines per workday. Speedup describes delivery density, not stopwatch or personal performance. This contract supersedes older visualization rules while retaining historical entries and archived Profile 1 charts.*
<!-- statistics-profile-2-guidance:end -->

<!-- learner-a11y-baseline:start -->
## Hinweise zur Lernenden- und A11Y-Basis / Notes on the Learner and A11Y Baseline

- Verbindliche Zielgruppen ab dem ersten Ausbildungsjahr sind
  Fachinformatiker*innen, Kaufleute für IT-System-Management und Kaufleute für
  Digitalisierungsmanagement.
- Lern-, Bedien-, Governance- und Spec-Kit-Inhalte stehen auf Deutsch zuerst
  und Englisch danach, verwenden ungefähr CEFR B2 und erklären Fachbegriffe
  beim ersten Auftreten.
- Spec-Kit-Erfahrung wird nicht vorausgesetzt. Befehle, Artefakte, Zustände und
  Übergänge werden beim ersten Gebrauch verständlich eingeführt.
- Abhängigkeiten, Zustände und Entscheidungen erhalten eine vollständige
  textorientierte Erklärung; eine ausschließlich visuelle Darstellung genügt
  nicht.
- `Programmierung #include<everyone>` und WCAG 2.2 Level AA gelten als
  verbindliche Prüfbasis, soweit die Kriterien auf das Artefakt anwendbar sind.

*The binding audience starts in the first training year and includes IT
specialist apprentices and both IT management occupations. Learner, usage,
governance, and Spec Kit content is German-first/English-second at about CEFR
B2, explains technical terms at first use, assumes no prior Spec Kit
experience, and never relies on visual-only dependency, state, or decision
information. `Programmierung #include<everyone>` and WCAG 2.2 Level AA are the
review baseline wherever applicable.*
<!-- learner-a11y-baseline:end -->

<!-- BEGIN spec-kit-diagrams-completion -->
## Mermaid und Spec-Kit-Abschlussbericht / Mermaid and Spec Kit completion report

Neue Lastenhefte enthalten bei hilfreichen Abläufen, Zuständen oder Abhängigkeiten
lesbaren Mermaid-Quelltext im Markdown und eine gleichwertige Textalternative.
Bei einfachen Inhalten die Nichtanwendung kurz begründen. Diagramme bilden die
verbindlichen Text-/Manifestquellen ab; Farbe allein trägt keine Bedeutung.
Nach jedem vollständig abgeschlossenen Spec-Kit-Feature-Lauf den vollständigen
Ergebnisbericht im Chat anzeigen und im Feature-Verzeichnis als
`completion-report.md` versionieren. Einzelne Planungs-/Status-/Review-Kommandos
lösen keinen solchen Bericht aus; blockierte oder pausierte Läufe als
Zwischenbericht kennzeichnen. Vorlage: `.specify/templates/completion-report-template.md`;
Regel: `docs/spec-kit-diagrams-and-completion-reports.md`.
Ergebnis, Tests, Dokumentation, Git-gebundene Umfangszahlen, Verlauf und Restpunkte
belegen. Programmlogik von generierter Evidence, Git-Wandzeit von aktiver
Arbeitszeit und bestandene von ausgefallenen Prüfungen unterscheiden. Finale
Merge-/Sync-Evidence im Chat und bestehenden Closeout-Nachweis ergänzen; keine
zusätzlichen Commits allein für selbstreferenzielle Berichts-/Statistikwerte.
Diese Projektregel und lokale Vorlagen bei Spec-Kit-Updates erhalten.

*New intakes use readable Mermaid Markdown and equivalent text alternatives for
useful workflows, states or dependencies; justify omission for simple content.
Diagrams reflect authoritative text/manifests and never rely on color alone.
After each completed feature run, show the full outcome report in chat and
version completion-report.md in the feature directory using the shared template
and rule above. Individual planning/status/review commands do not trigger it;
paused/blocked runs receive interim reports. Evidence outcomes, tests, docs,
Git-bound counts, delivery history and remaining work. Distinguish code from
volume generated as evidence, elapsed from active time, and passed from failed
checks. Add final merge/sync proof in chat and existing closeout evidence, without
commits solely for self-referential counts or IDs. Preserve local rules/templates
across Spec Kit updates.*
<!-- END spec-kit-diagrams-completion -->

<!-- project-statistics-rollout:begin -->
## Statistik-Preset und Pflege / Statistics preset and maintenance

Das aktuelle Projektprofil ist `project-statistics-fourteen-governance-presets`:
bestehende 13er-Basis plus Statistik v0.1.0 bei Prioritaet 90. Globale Defaults
bleiben erhalten; die operative lokale Zuordnung folgt erst nach Lieferung.
`docs/project-statistics/config.json` steuert den getrennten UTC-/52-Wochen-Kontext.
Profil 2 bleibt kanonisch; Referenzmodelle im neuen Kontext bleiben aus.
Nach abgeschlossenem Feature/Implementierungsabschnitt Inhalte zuerst committen,
Update ausdruecklich beauftragen und vorab im Dry-Run pruefen. Beide Statistiken
aus derselben Inhaltsrevision pflegen, Ausgaben committen und lesend verifizieren.
Keine Personen-, Lernleistungs-, Qualitaets-, Sicherheits- oder KI-Produktivitaetsbewertung.
Bedienung: `docs/project-statistics/README.md`; Quellen und Abnahme:
`docs/maintenance/project-statistics-rollout-v010.md`. Kein automatischer
Spec-Kit-Lauf und keine Commit-/Push-/Merge- oder menschliche Freigabe durch das Preset.

The current profile adds statistics v0.1.0 at priority 90 to the unchanged
thirteen-preset base. Preserve global defaults and defer operational assignment
until delivery. Keep legacy Profile 2 authoritative; use the separate UTC context
with reference models disabled. After feature/implementation completion, commit
content first, preview an expressly authorized update, render both contexts,
commit outputs and verify read-only status. Statistics do not rate people,
learning, quality, security or AI productivity. See the repository-relative
usage and rollout records. Installation grants no feature or delivery authority.
<!-- project-statistics-rollout:end -->
