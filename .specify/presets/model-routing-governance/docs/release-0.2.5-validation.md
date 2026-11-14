# v0.2.5: Dokumentierter Paketstand / Documented package state

Documentation Impact: **UpdateRequired**. Owner: Thorsten Hindermann.

## Deutsch

Issue [github/spec-kit#5240](https://github.com/github/spec-kit/issues/5240)
wurde blockiert, weil die Dokumentationsbestätigung wegen widersprüchlicher
Veröffentlichungsaussagen offen blieb. v0.2.4 war als reguläres GitHub-Release
veröffentlicht, während README und Adapterleitfäden es weiterhin als
Entwicklungskandidaten beschrieben.

Patch v0.2.5 korrigiert README und die DE-/EN-Leserpfade. Paketkennung,
Veröffentlichung und Installation sind getrennte Fakten: Die Paketkennung
bezeichnet die Quellen, die verlinkte GitHub-Release-Seite belegt die tatsächliche
Veröffentlichung, und eine Installation benötigt einen eigenen Nachweis.
Historische Vorbereitungs- und Veröffentlichungsnachweise bleiben unverändert.

Kanonische Quellen sind Manifest, README und die Sprachpartner unter `docs/`.
Leserpfad: README → Nutzungsleitfaden → Adapterleitfaden → dieser Patch-Nachweis.
Zielgruppen bleiben Maintainer, Lernende und Entwickelnde ohne Spezialwissen.
Die Dokumente sind textorientiert, Deutsch zuerst mit gleichwertigem Englisch;
CEFR B2 und anwendbare WCAG 2.2 AA bleiben Prüfziele. Automatisierte Link-,
Sprachpartner- und Vertragsprüfungen ersetzen keine vollständige menschliche
Sprach- oder Hilfsmittelabnahme.

Eine Regression prüft alle fünf aktuellen Einstiegstexte auf veraltete
Kandidaten-/Nichtveröffentlichungs-Aussagen, einschließlich negativer Fixtures.
Die Laufzeitdateien, Commands, Templates, Rollen, Priorität 61 und
Berechtigungsgrenzen bleiben bytegleich zu v0.2.4. Die Test-Kohorte ändert nur
ihren Model-Routing-Eintrag; sie ist keine Installationsmatrix.

Umfang: nur Preset-Quelle, Patch-Veröffentlichung und Aktualisierung des
bestehenden Katalog-Issues. Distribution: `sourceOnly`; kein Home-Sync,
Verbraucher-Rollout oder Änderung zentraler Installationsmatrizen/Locks.
Re-Evaluation bei neuem Paketstand, geändertem Leserpfad oder Maintainer-Befund.
Der Vorbereitungsnachweis belegt keine vorweggenommene Veröffentlichung;
Release-Seite und separat verifiziertes Tag-Archiv liefern deren Nachweis.

## English

Issue #5240 was blocked because the documentation confirmation remained open:
v0.2.4 had a regular GitHub release, while its README and adapter guides still
called it a development candidate. Patch v0.2.5 corrects those current reader
paths in both languages. Package identity names the source; the linked GitHub
release records actual publication; installation requires separate evidence.
Historical preparation and publication records remain unchanged.

Canonical sources are the manifest, README and paired guides. Reader path:
README → user guide → adapter guide → this patch record. Audiences remain
maintainers, apprentices and non-specialist developers. Text-oriented content,
German first and equivalent English, B2 and applicable WCAG 2.2 AA remain
review targets. Automated link, language-pair and contract checks do not prove
full human language or assistive-technology acceptance.

A regression checks all five current entry documents for stale candidate and
unpublished statements, with negative fixtures. Runtime files, commands,
templates, roles, priority 61 and authority boundaries remain byte-identical
to v0.2.4. Only Model Routing changes in the test-only cohort fixture.

Scope is the preset source, one patch release and the existing catalog issue.
Distribution is source-only: no Home sync, consumer rollout or central
installation-matrix/lock change. Re-evaluate on package, reader-path or
maintainer-feedback changes. Preparation does not prove publication; the release
page and independently verified tag archive provide publication evidence.

## CI-Nachtrag / CI follow-up

DE: Ubuntu brach bei der vollständigen PSScriptAnalyzer-1.25.0-Prüfung zweimal
intern ab: zuerst mit NullReferenceException, danach wegen mehrfacher dynamischer
Module in derselben Assembly. Die CI analysiert deshalb jede Datei in einem
eigenen Prozess. Regelwerk, Dateimenge, Befunde und Fehler bleiben verbindlich;
keine Ausnahme wird verschluckt. Diese Änderung betrifft nur die Paket-CI.

EN: Ubuntu's full PSScriptAnalyzer 1.25.0 pass failed internally twice, first
with NullReferenceException and then because multiple dynamic modules shared
one assembly. CI now analyzes each file in a separate process. The rule set,
file coverage, findings and error gates remain unchanged; no exception is
suppressed. This change affects package CI only.

Primary implementation precedent:
https://github.com/microsoft/hve-core/blob/main/scripts/linting/Invoke-PSScriptAnalyzer.ps1
