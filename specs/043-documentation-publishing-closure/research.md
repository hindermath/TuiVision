# Research: Documentation and Publishing Closure

## Bestandsaufnahme / Inventory

- Die Pflichtenheft-Abstimmung enthält genau 27 Einträge mit der Owner-Gruppe
  `DocumentationAndPublishing`.
- Sieben ausdrücklich benannte allgemeine Guides fehlen:
  `getting-started`, `architecture`, vier Konzept-Guides und `first-dialog`.
- Das Repository enthält 38 Beispielprojekte und 38 zugeordnete Guides:
  36 unter `docs/guides/examples/` sowie die eigenständigen Guides für
  `A11yFramework` und `FormTransaction`.
- Der DocFX-TOC führt die Beispiel-Guides bereits einzeln, aber noch keinen
  zusammenhängenden Einstiegspfad für neue Lernende.
- `docs/guides/multi-mac-workflow.md` verwendet operativ bereits `agy`.
  Verbleibende `gemini/**`-Nennungen in Branchfiltern sind ausdrücklich
  Legacy-Kompatibilität; `GEMINI.md` bleibt die Antigravity-Kontextfläche.

## Entscheidungen / Decisions

### D1 - Zentrale Lernvertragsmatrix statt pauschaler Umschreibung

Die 38 vorhandenen Guides bleiben fachliche Detailquellen. Ein neuer
Lernpfad-Guide ordnet jedem Beispiel Lernziel, Voraussetzungen, Start,
Bedienung, Architekturhinweis und Übung zu. Dadurch werden echte Lücken
geschlossen, ohne 38 bereits bewährte Dokumente nur stilistisch umzubauen.

### D2 - Unveränderliche Abstimmungsbasis

Die historische Abstimmung unter
`specs/requirements-reconciliation-20260726/` wird nicht rückwirkend
umgeschrieben. Feature 043 führt stattdessen eine aktuelle, eindeutige
Closure-Matrix mit allen 27 IDs, Entscheidung, Evidence und Grenze.

### D3 - Keine neue Validator-Skriptfamilie

Die Abnahme nutzt vorhandene strukturierte Tabellen, DocFX-Linkprüfung,
Markdown-Review, Bash-/PowerShell-Governancevalidatoren und Remote-Gates. Ein
neues Skript wäre für diese einmalige Dokumentations-Closure unverhältnismäßig
und würde zusätzliche Cross-Platform-Parität erzeugen.

### D4 - Dokumentations-Publishing vollständig auslösen

Die neuen Guides und die TOC-Navigation lösen DocFX, Playwright/Axe und einen
Textbrowser-Gegencheck aus. Öffentliche Signaturen und XML-Kommentare bleiben
unverändert; Release-CS1591 wird über den vorhandenen Release-Build geprüft.

### D5 - Historische Abweichungen auffindbar machen

Ein kurzer Wegweiser erklärt die Quellenrangfolge und verlinkt vorhandene
Portierungs-, Konformitäts- und Feature-Evidence. Er behauptet keine neue
historische Prüfung und kopiert keine externe Quelle.

### D6 - Agent-Parität bleibt unverändert

Die gepflegten Agent-Oberflächen und Toolchain-Registries sind bereits auf
`agy` ausgerichtet. Feature 043 dokumentiert diese Prüfung als
`NoUpdateRequired`; es ändert keine gemeinsame Agent-Regel und keine
`.specify/templates/`.

## Verworfen / Rejected

- Vollständige Neufassung jedes Beispiel-Guides: hoher Churn ohne zusätzlichen
  Produkt- oder Lernnachweis.
- Neue historische Source-Audits: außerhalb des akzeptierten Scopes.
- Generierte DocFX-Ausgabe einchecken: `_site/` und `api/*.yml` bleiben
  Build-Artefakte.
- Neue Dokumentationsabhängigkeit oder neuer Link-Checker: vorhandene Toolchain
  reicht aus.
