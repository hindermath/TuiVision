# CLAUDE.md

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

[Verbindliche Detailregeln / Required detailed rules](docs/agent-guidance.md):
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

[Featureverlauf und technische Referenzen / Feature history and technical references](docs/agent-feature-context.md)

## Commands

```bash
# Restore, build, and test (full validation cycle)
dotnet restore
dotnet build --configuration Release
dotnet test

# Run tests for a specific project
dotnet test tests/TuiVision.Core.Tests/

# Run a single test method
dotnet test --filter "FullyQualifiedName~TestMethodName"

# Build generated docs plus Playwright + axe accessibility smoke tests
cd tests/web-a11y
npm install
npx playwright install chromium
npm run test:docfx

# After every DocFX regeneration, rerun the matching A11y smoke check
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

## Historical Source Reference Policy

- Fuer jede Spec-Kit-Feature-Implementierung, die historisch abgeleitetes Turbo-Vision-Verhalten portiert, erweitert, testet, dokumentiert oder korrigiert, muessen die relevanten historischen Implementierungsdateien unter `tv203s/` als Read-only-Referenz geprueft werden. Das betrifft mindestens passende `.c`- und `.cc`-Dateien.
- Falls Typen, Konstanten, Makros, Datenlayout, Vererbung, Funktionssignaturen oder Kontext nur ueber Deklarationen klar werden, muessen auch die relevanten C/C++-Header wie `.h`, `.hpp` oder `.hh` geprueft werden.
- `tv203s/` wird nicht veraendert. Die C#-Portierung bleibt eine moderne, idiomatische Umsetzung der historischen Absicht und keine mechanische 1:1-Uebersetzung.
- `spec.md`, `plan.md`, `tasks.md`, Guides, PR-Evidence oder Architektur-/Security-Nachweise muessen bei relevanten Features festhalten, welcher historische Zweck uebernommen wird und welche wesentlichen nutzer- oder API-sichtbaren Abweichungen bewusst sind.
- Wenn ein Feature keinen historischen `tv203s`-Bezug hat, genuegt ein kurzes `N/A` mit Begruendung in Plan, Aufgaben oder Evidence.

- For every Spec-Kit feature implementation that ports, extends, tests, documents, or fixes historically derived Turbo Vision behavior, review the relevant historical implementation files under `tv203s/` as read-only reference. This includes at least the matching `.c` and `.cc` files.
- When types, constants, macros, data layout, inheritance, function signatures, or context are only clear from declarations, also review the relevant C/C++ headers such as `.h`, `.hpp`, or `.hh`.
- Do not modify `tv203s/`. The C# port remains a modern idiomatic implementation of the historical intent, not a mechanical line-by-line translation.
- For relevant features, `spec.md`, `plan.md`, `tasks.md`, guides, PR evidence, or architecture/security evidence must record which historical intent is followed and which material user-visible or API-visible deviations are intentional.
- If a feature has no historical `tv203s` relevance, a short `N/A` rationale in plan, tasks, or evidence is sufficient.

## Branching Convention

Feature branches use either the agent-prefixed form `codex/<feature-description>` (or another supported agent prefix such as `claude/`, `agy/`, legacy-compatible `gemini/`, `copilot/`, `opencode/`) or the numbered Spec-Kit form `NNN-short-description` when the Spec-Kit workflow creates the branch. CI runs on pushes to `main`, `master`, `codex/**`, `claude/**`, `agy/**`, legacy-compatible `gemini/**`, `opencode/**`, and `copilot/**` branches.
When a dedicated feature branch has implemented the requirements of a Lastenheft, rename that file to `Lastenheft_<topic>.<feature-branch>.md` so the delivered scope stays traceable.

## Agent File Synchronization Policy

- When active feature context, implementation-plan guidance, or other shared AI-agent instructions change, update the following files together when affected:
  - `AGENTS.md`
  - `CLAUDE.md`
  - `GEMINI.md`
  - `.github/copilot-instructions.md`
  - `.github/agents/copilot-instructions.md`
- Do not leave shared guidance synchronized in only one of these files.
- If an agent-specific file needs intentional divergence, document the reason in the same change.

## Pflichtenheft Next-Step Marker

- Maintain a prominent `>>> NAECHSTER SCHRITT <<<` marker in `Pflichtenheft.md`.
- The marker MUST point to the currently highest-priority open work item in the prioritized rest-work section and MUST be moved whenever progress changes the effective next step.

## Level-2-Umgebungsregister / Level-2 Environment Registry

- Die zentrale `constitution.md` enthält das verbindliche Level-2 Project Environment Registry.
- Spec-Kit-Pläne und Claude-Arbeit in Level-2-Projekten müssen die passende Registry-Zeile als verbindlichen Kontext für Runtime, Build/Test, A11Y, Statistik und Agentenflächen verwenden.
- Änderungen an einer Level-2-Runtime, Toolchain oder Statistik-Basis müssen `constitution.md`, `.specify/memory/constitution.md` und betroffene KI-Agenten-Dateien gemeinsam prüfen.

*The central `constitution.md` contains the binding Level-2 Project Environment Registry. Spec-Kit plans and Claude work in Level-2 projects must use the matching registry row as binding context for runtime, build/test, A11Y, statistics, and agent surfaces. Changes to Level-2 runtime, toolchain, or statistics baselines require a joint review of `constitution.md`, `.specify/memory/constitution.md`, and affected AI-agent files.*

## Sicherheitsdokumentation / Security Documentation (XII–XVIII Extensions)

- Jedes Level-2-Projekt MUSS die folgenden Sicherheitsdokumente pflegen, basierend auf den Templates in `.specify/templates/`:
  - **Bedrohungsmodell / Threat Model** (`threat-model-template.md`) — STRIDE-Methodik, Trust Boundaries, Risikobewertung, CAPEC-Referenzen (Prinzip XIII + XVII)
  - **Security Architecture Decision Records (S-ADR)** (`adr-template.md`) — architektonische Sicherheitsentscheidungen mit Compliance-Nachweis (Prinzip XIII)
  - **arc42 Section 8 Sicherheits-Querschnittskonzepte** (`arc42-security-template.md`) — Authentifizierung, Autorisierung, Verschlüsselung, Eingabevalidierung, Fehlerbehandlung, Logging, Abhängigkeiten, Deployment (Prinzip XIII)
  - **Sicherheits-Checkliste / Security Checklist** (`security-checklist-template.md`) — sprachspezifische Code-Review-Checkliste (Prinzip XII)
  - **Abhängigkeits-Audit / Dependency Audit** (`dependency-audit-template.md`) — CVE-Tracking, Lizenz-Compliance, Supply-Chain-Sicherheit (Prinzip XII)
  - **Sicherheits-Qualitätsszenarien / Security Quality Scenarios** (`security-quality-scenarios-template.md`) — iSAQB CPSA-F Qualitätsszenario-Methodik (Prinzip XII + XIII, SHOULD)
  - **ASVS-Verifikation / ASVS Verification** (`asvs-verification-template.md`) — OWASP ASVS Level, Scope und Evidenz (Prinzip XV, Web-/API-Projekte MUST)
  - **Supply-Chain-Evidenz / Supply Chain Evidence** (`supply-chain-evidence-template.md`) — SBOM, AI-SBOM, VEX, SLSA, OpenSSF Scorecard (Prinzip XVI, releasefähige Projekte MUST; AI-SBOM nur bei KI-Runtime-/Produktkomponenten)
  - **Zero-Trust-Anwendbarkeit / Zero Trust Applicability** (`zero-trust-applicability-template.md`) — NIST SP 800-207-Bewertung (Prinzip XVIII, verteilte Systeme SHOULD)
  - **SAMM-Bewertung / SAMM Assessment** (`samm-assessment-template.md`) — OWASP SAMM Reifegrad und Verbesserungsplan (Prinzip XVIII, langlebige Projekte SHOULD)
- Projektspezifische Instanzen werden in `docs/security/` gepflegt; S-ADRs als einzelne Dateien in `docs/security/adr/`.

*Every Level-2 project MUST maintain security documents based on templates in `.specify/templates/`: threat model (STRIDE+CAPEC), S-ADRs, arc42 Section 8 security concepts, security checklist, dependency audit, security quality scenarios (SHOULD), ASVS verification (web/API MUST), supply-chain evidence (release-capable MUST; AI-SBOM when AI runtime/product components apply), Zero Trust applicability note (distributed systems SHOULD), and SAMM assessment (long-lived projects SHOULD). Project-specific instances live in `docs/security/`; S-ADRs in `docs/security/adr/`. See `constitution.md`, Principles XII–XVIII for authoritative requirements.*

## Sicherheitsstandards & Anwendbarkeit / Security Standards & Applicability

- Vor jeder Level-2-Aufgabe die anwendbaren Sicherheitsstandards aus `constitution.md`, Prinzipien XIV-XVIII bestimmen und explizit benennen.
- `NIST SSDF` und `CWE Top 25` gelten immer für Level-2-Arbeit.
- `OWASP ASVS` gilt für Web-, API-, HTTP- und authentifizierte Dienste; der gewählte ASVS-Level muss benannt werden.
- `SBOM` gilt für releasefähige oder verteilbare Artefakte; `VEX`, wenn bekannte Schwachstellen in ausgelieferten oder geprüften Komponenten bewertet werden müssen.
- `AI-SBOM` gilt projektartabhängig bei KI-Modellen, KI-Diensten, Trainings-/Embedding-Daten, Inferenz-Infrastruktur oder KI-Runtime-Komponenten im ausgelieferten oder betriebenen System; reine Entwicklungswerkzeug-Nutzung wird als `N/A` mit Toolchain-Begründung dokumentiert.
- `SLSA` gilt als Soll-Vorgabe für CI/CD- oder veröffentlichte Artefakte; `Zero Trust` ist für verteilte, servicebasierte, cloudnahe oder remote-verwaltete Systeme explizit zu prüfen.
- `CAPEC` soll in Bedrohungsmodellen für die risikoreichsten Angriffswege verwendet werden; `OWASP SAMM` soll für langlebige Projekte/Workspaces in Verbesserungspläne einfließen.
- `OWASP Cheat Sheet Series`, `OWASP Proactive Controls` und bei öffentlichen OSS-Repositories oder kritischen Abhängigkeiten `OpenSSF Scorecard` sind als ergänzende Referenzen zu berücksichtigen.
- Nichtanwendbarkeit immer als `N/A` mit kurzer Begründung dokumentieren; keine stillschweigende Auslassung.

*At the start of every Level-2 task, determine and name the applicable security standards from `constitution.md`, Principles XIV-XVIII. `NIST SSDF` and `CWE Top 25` always apply. `OWASP ASVS` applies to web/API/HTTP/auth-bearing services; `SBOM` applies to releasable or distributable artefacts; `AI-SBOM` applies when AI models, AI services, datasets, inference infrastructure, or AI runtime components are part of the released or operated system; `VEX` applies when known vulnerabilities in shipped/evaluated components need a disposition statement. `SLSA` is the target model for CI/CD and published artefacts; `Zero Trust` must be explicitly evaluated for distributed, service-based, cloud, or remotely managed systems. `CAPEC`, `OWASP SAMM`, `OWASP Cheat Sheet Series`, `OWASP Proactive Controls`, and `OpenSSF Scorecard` are supporting references where relevant. Record non-applicability as `N/A` with justification rather than omitting it silently.*

## GitHub/GitLab CLI First / GitHub/GitLab CLI zuerst

Für GitHub-Repositories zuerst die authentifizierte `gh` CLI für mögliche Schreibaktionen und Live-Repository-Operationen verwenden, einschließlich PR-/Issue-Kommentaren, PR-Statusprüfungen, Review-Follow-up, Workflow-Prüfung und Merge-/Statusabfragen. GitHub-Connector-Tools hauptsächlich für strukturierte Read-only-Inspektion oder Fälle nutzen, in denen die CLI nicht geeignet ist.

Für GitLab-Repositories die authentifizierte `glab` CLI zuerst für gleichwertige Aktionen verwenden. Bekanntermaßen fehlschlagende Connector-Schreibwege nicht wiederholt versuchen, wenn `gh`/`glab` die Aufgabe direkt erledigen kann.

For GitHub repositories, use the authenticated `gh` CLI first for feasible write actions and live repository operations, including PR/issue comments, PR status checks, review follow-up, workflow inspection, and merge/status queries. Use GitHub connector tools mainly for structured read-only inspection or when the CLI is not suitable.

For GitLab repositories, use the authenticated `glab` CLI first for equivalent actions. Do not repeatedly try connector write paths that are known to fail when `gh`/`glab` can perform the task directly.

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

<!-- source-reference-policy:begin -->
## Drei-Achsen-Quellenpolicy / Three-Axis Source Reference Policy

- Aktuelle TuiVision-Vertraege sind die Produktnorm. `magiblot/tvision` wird
  zuerst als moderne, nicht normative Designreferenz am Commit
  `57b6f56b38e0ee75240a80a10ee0e11470c24693` und Tree
  `96dd03873955689ff0a79f6c8107a8148fe1ebd6` geprueft; Borland und `tv203s/`
  bestimmen historische Absicht. Free Vision und Terminal.GUI sind unabhaengige
  Vergleiche, `TVDEMOS`, `TVFM` und Beispiele Consumer-Evidence.
- Der Workflow endet mit genau einer Entscheidung: `AdoptModernization`,
  `PreserveHistoricalIntent`, `IntentionalTuiVisionDeviation` oder `N/A`.
  Quellenrang allein loest keinen Konflikt und Magiblot erzwingt keine
  C++-Vererbung, Speicherstruktur oder Quelltextform.
- Externe Checkouts bleiben untracked; Quellkopie oder Vendorisierung ist
  ausgeschlossen. Der Lizenzstatus ist `MultipartNotRepositoryWideMIT`.
- Die Policy gilt `Prospective`. Re-Evaluation erfolgt nur bei geaendertem
  TuiVision-Produktvertrag, neuem freigegebenem Magiblot-Pin oder materiell
  neuer Consumer-Evidence. Bewegliche Branches sind keine Evidence.

*Current TuiVision contracts remain normative. The exact Magiblot revision is
reviewed first for modern design, while historical and consumer evidence retain
their distinct roles. The policy is prospective and preserves the no-copy and
multipart-license boundaries.*
<!-- source-reference-policy:end -->

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
