# Dokumentations-Governance / Documentation Governance

## Zweck / Purpose

Eine **Dokumentationsauswirkung** beschreibt, ob eine technische oder
fachliche Änderung Dokumentation ändern muss. Die Entscheidung wird im
Feature, in den Aufgaben und im Pull Request festgehalten. So hängt aktuelle
Dokumentation nicht vom Gedächtnis einzelner Personen oder Agenten ab.

*A **documentation impact** states whether a technical or professional change
must update documentation. The decision is recorded in the feature, tasks, and
pull request so current documentation does not depend on individual memory.*

## Vier Entscheidungen / Four Decisions

| Entscheidung | Wann? | Pflichtnachweis |
|---|---|---|
| `UpdateRequired` | Eine aktuelle Aussage, Anleitung oder Schnittstelle ändert sich. | Betroffene Dokumente und Validierung im selben PR |
| `NoUpdateRequired` | Geprüfte Dokumentationsflächen bleiben sachlich richtig. | Kurze Begründung und geprüfter Bereich |
| `GeneratedUpdate` | Dokumente werden aus einer Quelle erzeugt. | Kanonische Quelle, Renderer und neu erzeugte Ableitungen |
| `FollowUp` | Die notwendige Arbeit überschreitet den genehmigten Scope. | Owner, Risiko, Frist, Wiedervorlage, Evidence und Scope-Grund |

*The same four outcomes distinguish current updates, justified no-change,
generated output, and bounded later work. Security, usage, or breaking-change
documentation needs explicit accepted-risk evidence before it may be deferred.*

## Ebenen und Verantwortung / Levels and Ownership

| Ebene | Verantwortung | Beispiele | Prüfpunkt |
|---|---|---|---|
| Level 0 | Gemeinsame Regeln und wiederverwendbare Abläufe | Constitution, zentrale Templates, Flottenregister | Gilt die Regel für alle registrierten Repositories? |
| Level 1 | Zusammensetzung eines Arbeitsbereichs | Workspace-README, gemeinsame Betriebsanleitung | Stimmen enthaltene Projekte und gemeinsame Commands? |
| Level 2 | Produkt- und Laufzeitwahrheit | API, Build/Test, Bedienung, A11Y, Plattformgrenzen | Stimmt die Aussage mit Code und validiertem Verhalten überein? |

Die Ebenen kopieren nicht pauschal denselben Text. Jede Information bleibt bei
ihrer **Source of Truth**, also ihrer verbindlichen Quelle. Andere Dokumente
verlinken oder werden deterministisch daraus erzeugt.

*The levels do not copy identical text everywhere. Information remains with
its **source of truth**, its authoritative source. Other documents link to it
or are generated deterministically.*

## Ownership-Matrix

| Dokumentfamilie | Source of Truth | Owner | Trigger | Ableitung | Validator / Review | Wiedervorlage |
|---|---|---|---|---|---|---|
| Normative Governance | `constitution.md` | Level-0 Maintainer | Neue verbindliche Regel | `.specify/memory/constitution.md` synchron | Homogeneity und Review | Bei jeder Governance-Änderung |
| Spec-Kit Workflow | `.specify/templates/` | Spec-Kit Maintainer | Neuer Pflichtnachweis | Agent-/Command-Oberflächen | `specify check`, Paritätsprüfung | Bei Preset- oder Template-Update |
| Skriptreferenz | `scripts/config/script-catalog.json` und Skripte | Script Maintainer | Skript hinzugefügt/geändert | `docs/scripts/*.md` | `render-script-reference.*` | Bei jeder Skriptänderung |
| Projektstatistik | `docs/project-statistics.config.json` und Git-Historie | Repository Maintainer | Feature-/Lieferabschluss | `docs/project-statistics.md` | `render-project-statistics.*` | Nach jedem Feature |
| Produktdokumentation | Level-2-Code und validiertes Verhalten | Produktteam | Runtime, API, Command oder UX ändert sich | README, Guides, API-Doku | Produkt-, Link- und A11Y-Gates | Vor Merge und Release |
| Lernmaterial | Blueprint, Register und Rahmenlehrplan-Mapping | Lehrende und Maintainer | Lernziel oder Referenz ändert sich | Unit- und ZIP-Pakete | Lernpaket- und A11Y-Gates | Vor Veröffentlichung |
| Security Evidence | `docs/security/` und reale Gates | Security Owner | Trust-, Release-, Cloud- oder Risikogrenze ändert sich | Checklists, ADR, PR-Evidence | Security Review | Nach Trigger oder Frist |

## Durchführung / Procedure

1. Geänderte Pfade und benannte Flows erfassen.
2. Zielgruppen bestimmen: Lernende, tägliche Nutzende,
   Maintainer/KI-Agenten oder Prüfung/Fehleranalyse. Zusätzlich mindestens einen
   betroffenen Leserpfad benennen, zum Beispiel Einstieg, Voraussetzungen,
   Vertiefung und nächste sichere Aktion.
3. Kanonische Quelle, Owner, Dokumentklasse und Navigationseinfluss bestimmen.
4. Genau eine der vier Entscheidungen wählen.
5. Sprachstrategie, Sprachpartner, Plattform- und Beispielnachweis ergänzen.
6. Repository-spezifische Distributionsklasse und Sync-Bedarf festhalten. Die
   Home Baseline verwendet beispielsweise `homeRuntime`, `sourceOnly` und
   `machineLocal`; andere Repositories behalten ihren eigenen Vertrag.
7. Quellen statt generierter Ableitungen ändern.
8. Link-, Renderer-, A11Y-, Plattform- und Fachprüfungen ausführen.
9. Evidence und Re-Evaluation-Trigger festhalten und im Pull Request prüfen.

*Record the affected audience and at least one concrete reader path, canonical
source and owner, navigation impact, document class, language strategy and
partner, platform and example proof, repository-specific distribution and sync
need, evidence, and reevaluation trigger. Keep semantic review separate from
deterministic validation.*

## Leserpfade und Progressive Disclosure / Reader Paths and Progressive Disclosure

**Progressive Disclosure** bedeutet: Der erste Einstieg zeigt Zweck,
Voraussetzungen, Sicherheitsgrenzen und genau eine sichere nächste Aktion.
Vertiefende Erklärungen folgen über beschreibende Links. Ein Leserpfad muss
Voraussetzungen, Reihenfolge, tiefe Referenzen und nächste Aktion nennen.

Große Dokumente werden nach Aufgabe und Zielgruppe getrennt, wenn eine
gemeinsame Datei Orientierung oder Sprachpflege erschwert. In diesem Fall
bleibt Deutsch der primäre Einstieg und eine nach Repository-Regel benannte
englische Partnerdatei, zum Beispiel `README.EN.md`, bietet einen inhaltlich
gleichwertigen Pfad. Beide Dateien verlinken gegenseitig.
Kurze Dokumente dürfen Deutsch zuerst und Englisch danach enthalten.
Repository-eigene Homogeneity-Prüfungen müssen deklarierte Partnerdateien als
Paar erkennen. Können sie das noch nicht, entsteht ein Tooling-Follow-up statt
einer künstlichen Sprachduplizierung innerhalb jeder Partnerdatei.

*Progressive disclosure keeps purpose, prerequisites, safety boundaries, and
one safe next action at the first entry. Detailed explanation follows through
descriptive links. Split large documents by task and audience when one file
harms orientation or language maintenance; paired language files remain
semantically equivalent and link to each other. Repository homogeneity checks
must recognize declared language pairs; otherwise record a tooling follow-up
instead of duplicating both languages inside every partner file.*

## Source und Distribution / Source and Distribution

Jedes Repository benennt seine eigene kanonische Quelle und den zugehörigen
Default-Branch. Eine getrennte Runtime-, Installations- oder Deployment-Kopie
gilt nur, wenn der Repository-eigene Vertrag sie ausdrücklich definiert.
Dokumentation darf weder Level-0-Pfade noch Distributionsklassen allein wegen
ähnlicher Dateinamen auf andere Repositories übertragen.

Für die Home Baseline gelten zusätzlich diese lokalen Klassen:

- `homeRuntime`: kanonisch in Level-0 ändern und nach Lieferung
  manifestgesteuert nach `~/` synchronisieren;
- `sourceOnly`: direkt aus der Level-0-Quelle lesen; kein Home-Sync;
- `machineLocal`: nur lokal halten; keine implizite Remote-Autorität.

Konkrete Home-Runtime-Mitglieder werden ausschließlich im Level-0-Repository
Home Baseline aus `scripts/config/home-sync-manifest.json` abgeleitet. Dieser
Pfad ist in anderen Repositories keine lokale Datei. Andere Repositories
behalten ihre eigenen Begriffe sowie Build-, Installations-, Deployment- oder
Lernreihenverträge. Eine Dokumentationsänderung erweitert keine technische
Propagations- oder Runtime-Zielmenge.

*Each repository names its own canonical source and default branch. A separate
runtime, installation, or deployment copy exists only when the repository's
own contract defines it. The Home Baseline additionally uses `homeRuntime`,
`sourceOnly`, and `machineLocal`; these terms and its paths are not universal.
Its `scripts/config/home-sync-manifest.json` path belongs to the Level-0 Home
Baseline repository and is not a local path in other repositories.
Documentation changes do not expand technical propagation or runtime targets.*

Deterministische Validatoren prüfen Struktur, Pfade, Hashes und Pflichtfelder.
Sie können nicht beweisen, dass ein Satz fachlich wahr ist. Dafür bleiben
Review und ausführbare Nachweise erforderlich.

*Deterministic validators check structure, paths, hashes, and required fields.
They cannot prove that a statement is professionally true; review and
executable evidence remain necessary.*

## Portable Testdaten / Portable Test Data

Die positiven und negativen Vertragsbeispiele liegen unter
`scripts/tests/documentation-impact/fixtures/`. Dieser Pfad ist Bestandteil
des kanonischen Wartungspakets. Dadurch bleiben die kopierten Test-Runner in
Level-1-/Level-2-Repositories ausfuehrbar und haengen nicht von einem
historischen Feature-Verzeichnis der Level-0-Quelle ab.

*Positive and negative contract fixtures live under
`scripts/tests/documentation-impact/fixtures/`. The canonical maintenance
package includes this path, so copied test runners remain executable without
depending on a historical Level-0 feature directory.*
