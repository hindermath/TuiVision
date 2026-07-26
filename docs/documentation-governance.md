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
2. Zielgruppen und Dokumentfamilien bestimmen.
3. Genau eine der vier Entscheidungen wählen.
4. Pflichtfelder und Evidence ergänzen.
5. Quellen statt generierter Ableitungen ändern.
6. Passende Link-, Renderer-, A11Y-, Plattform- und Fachprüfungen ausführen.
7. Entscheidung im Pull Request erneut prüfen.

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
