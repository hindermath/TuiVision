# Lernpfad Sichere Entwicklung für Lehrjahr 1 bis 3 / Secure Development Learning Path for Training Years 1 to 3

**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Stand / Date:** 2026-07-10
**Zielgruppe / Audience:** Fachinformatik-Auszubildende, ausbildende Personen und Reviewer / IT specialist apprentices, instructors, and reviewers

## Grundsatz: Sicherheit ab Tag 1 / Principle: Security From Day One

**DE:** Sichere Entwicklung beginnt vor dem ersten Repository-Zugriff und mit der ersten Programmieraufgabe. Lernende müssen nicht sofort jede Norm beherrschen. Sie müssen aber von Anfang an sicher arbeiten, Unsicherheit melden und Entscheidungen nachvollziehbar dokumentieren.

**EN:** Secure development starts before the first repository access and with the first programming task. Learners do not need to master every standard immediately. They must work safely from the beginning, report uncertainty, and document decisions traceably.

## Rollenregel / Role Rule

- Die Anwendbarkeit richtet sich nach dem Projekt, nicht nach dem Wissensstand der lernenden Person.
- Fortgeschrittene Kontrollen werden nicht allein wegen des Lehrjahrs als `N/A` markiert.
- Bei fortgeschrittenen Kontrollen übernimmt eine ausbildende, projektverantwortliche oder Security-Rolle die Entscheidung und erklärt sie der lernenden Person.
- Jede Lernaufgabe erzeugt einen kleinen Nachweis: Commit, Review, Test, kurze Begründung oder Reflexion.

## Einstiegsprofil Lehrjahr 1 / Year 1 Starter Profile

Das Einstiegsprofil bündelt die vollständigen Checklisten in zwölf verständliche Praxispunkte. / The starter profile groups the complete checklists into twelve understandable practice items.

| Nr. | Praxispunkt | Erwarteter Nachweis | Bezug |
|---:|---|---|---|
| 1 | Ziel, Scope und `N/A` verstehen und begründen. | kurze Scope- und N/A-Notiz | CL-01-01, CL-01-11, CL-01-12 |
| 2 | Nur in einer freigegebenen Arbeitsumgebung arbeiten. | Preflight oder Umgebungsnotiz | CL-10-01 bis CL-10-03 |
| 3 | KI-Agenten nur in der freigegebenen Sandbox starten. | Sandbox-ID, Mount- und Werkzeugnachweis | CL-12-01 bis CL-12-04 |
| 4 | Secrets, Token und personenbezogene Daten schützen. | Secret-Scan und kurze Datenklassifikation | CL-08-07, CL-09-07, CL-10-06, CL-10-07 |
| 5 | Eingaben validieren und Ausgaben passend codieren. | positiver und negativer Test | CL-08-01, CL-08-02 |
| 6 | Fehler sicher behandeln und Logs sauber halten. | Fehlerpfadtest und Log-Review | CL-08-08, CL-08-09 |
| 7 | Abhängigkeiten bewusst auswählen und sperren. | Quelle, Lockfile, CVE- und Lizenznotiz | CL-05-06, CL-05-07, CL-05-09, CL-05-11 |
| 8 | Keine eigene Kryptografie bauen. | verwendete Plattform-API oder `N/A` | CL-03-04, CL-03-06, CL-03-08, CL-03-11 |
| 9 | Normal-, Fehler- und Grenzfälle testen. | Testausgabe und Review | CL-08-12, CL-09-09 |
| 10 | Wichtige Daten und einfache Vertrauensgrenzen benennen. | kleine Datenfluss- oder Schutzbedarfsnotiz | CL-02-01, CL-04-01, CL-04-02 |
| 11 | KI-Ergebnisse und nichttriviale Logik menschlich prüfen. | Review-Kommentar und didaktischer Kommentarbedarf | CL-09-02, CL-09-17, CL-12-07 |
| 12 | Ergebnis, Evidenz, Restrisiko und nächsten Schritt dokumentieren. | ausgefüllte Nachweisinstanz | alle anwendbaren CL-IDs |

## Lehrjahr 1: Grundlagen / Year 1: Foundations

**Lernziele:**

- sichere Standardarbeitsweise anwenden;
- vertrauenswürdige und nicht vertrauenswürdige Eingaben unterscheiden;
- Secrets und personenbezogene Daten erkennen;
- einfache sichere APIs, Tests und Reviews nutzen;
- `Applicable`, `N/A`, `Open` sowie den Umsetzungsstatus korrekt dokumentieren;
- bei Unsicherheit stoppen und Unterstützung anfordern.

**Geeignete Übungen:** kleines Konsolenprogramm, Dateiimport, einfache Web- oder API-Eingabe, Dependency-Update, Secret-Scan, negativer Test und Sandbox-Preflight.

**Abschlusskriterium:** Die lernende Person kann jeden Punkt des Einstiegsprofils in eigenen Worten erklären und mindestens einen nachvollziehbaren Nachweis erzeugen.

## Lehrjahr 2: Aufbau / Year 2: Intermediate

**Lernziele:**

- STRIDE und einfache Risikobewertung anwenden;
- Trust Boundaries und Defense in Depth in Architekturentscheidungen nutzen;
- Kryptografie, Authentifizierung, Autorisierung und sichere Konfiguration prüfen;
- SBOM, VEX, reproduzierbare Builds und CI/CD-Schutz verstehen;
- Schwachstellen sicher triagieren und nachverfolgen;
- A11Y und Datenschutz in Tests und Reviews integrieren.

**Geeignete Übungen:** Threat Model, S-ADR, SBOM-Erzeugung, Dependency-Audit, CI-Sicherheitsprüfung, Rollenprüfung und Wiederherstellungstest.

**Abschlusskriterium:** Die lernende Person kann einen nichttrivialen Prüfbereich unter Anleitung vollständig mit Evidenz und Restrisiko dokumentieren.

## Lehrjahr 3: Vertiefung / Year 3: Advanced

**Lernziele:**

- Sicherheitsarchitektur und Qualitätsziele zusammenführen;
- regulatorische Anwendbarkeit begründet prüfen;
- CRA, C3A, C5, SLSA, SAMM und Zero Trust projektbezogen einordnen;
- Risikoakzeptanz, Ausnahmen, Incident-Evidence und BCM bewerten;
- vollständige Spec-Kit- und Checklisten-Nachweise reviewen;
- jüngere Lernende bei sicheren Entscheidungen unterstützen.

**Geeignete Übungen:** vollständiger Härtungslauf, Cloud-Assurance-Entscheidung, CRA-N/A- oder Scope-Analyse, Wiederanlaufübung, Security-Review und Abschlusspräsentation.

**Abschlusskriterium:** Die lernende Person kann einen Prüfbereich planen, durchführen, erklären und zur menschlichen Freigabe vorlegen.

## Bewertung / Assessment

| Stufe | Bedeutung |
|---|---|
| Beobachtet | Die lernende Person kann den Zweck mit Unterstützung erklären. |
| Angeleitet | Die lernende Person führt den Schritt mit Anleitung aus. |
| Selbstständig | Die lernende Person führt den Schritt korrekt aus und dokumentiert Evidenz. |
| Reviewfähig | Die lernende Person kann fremde Nachweise prüfen und Rückfragen stellen. |

Noten oder Zertifikate werden nicht automatisch aus Checklistenstatus abgeleitet. Die ausbildende Person bewertet Verständnis, Anwendung und Reflexion gemeinsam.

## Barrierefreiheit / Accessibility

- Erklärungen verwenden CEFR-B2-Sprache und führen Abkürzungen ein.
- Tabellen werden durch einleitenden Text erklärt.
- Status wird nicht nur durch Farbe oder Symbole vermittelt.
- Übungen sind mit Tastatur, Screenreader, Braille-Zeile und Textausgabe nutzbar, soweit auf das Artefakt anwendbar.

## Versionshistorie / Version History

| Version | Datum | Änderung |
|---|---|---|
| 1.0.0 | 2026-07-10 | Lernpfad und Einstiegsprofil für sichere-Entwicklung-Basis 3.0.0 eingeführt. |
