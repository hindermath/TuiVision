# Level-2 Lastenheft Authoring Profile

## Identity

- Profile ID: `level2-lastenheft`
- Applies when: An active Level-2 requirements document is prepared for one later Spec Kit feature.
- Target path rule: Preserve the repository-owned `Lastenheft_*.md` path and processing order.
- Language rule: German first and English second, CEFR B2.

## Required Sections

Preserve purpose, current and target state, scope, non-goals, atomic
requirements, quality and governance boundaries, dependencies, risks,
expected artifacts, evidence, measurable acceptance, assumptions, and
ordering. Every active intake ends with exactly one copy-ready Specify prompt
and one copy-ready Autonomous prompt. Creating or reviewing an intake starts
no feature.

## Naming And Ordering

Preserve stable filenames, predecessor gates, and archived feature suffixes.
Ordering conflicts are material and must not be guessed.

## Quality Gates

Apply the repository C#/.NET secure-coding profile, including validated inputs, parameterized persistence access, safe serialization, output encoding, and dependency evidence.

Apply repository security, privacy, architecture, A11Y, agent-parity,
cross-platform, statistics, and evidence rules. Use text-first WCAG 2.2 AA
where applicable. The Autonomous prompt defaults to `LocalImplementation`
and grants no remote, bypass, secret, provider, or follow-up-feature authority.

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
