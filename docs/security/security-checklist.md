# Sicherheits-Checkliste / Security Checklist: TuiVision

**Projekt / Project**: TuiVision (Level-2)
**Datum / Date**: 2026-04-24
**Status**: Stub — mit projektspezifischen Inhalten zu befuellen / Stub — to be populated
**Template-Quelle / Template Source**: `.specify/templates/security-checklist-template.md`

<!--
  Dieses Dokument ist ein Stub. Die vollstaendige Struktur findet sich im
  Template unter .specify/templates/security-checklist-template.md. Bei der Befuellung das Template als Vorlage
  verwenden.

  This document is a stub. The complete structure can be found in the
  template at .specify/templates/security-checklist-template.md. Use the template as a guide when populating.
-->

## 010-standard-dialogs-designer

Datum: 2026-05-02. Scope: lokale Standarddialoge, Dateipfad-
Validierung, Dialogbeschreibung und persistierte Dialogbeschreibung. Keine
Netzwerk-, Web-, Authentifizierungs- oder Datenbankgrenze wurde eingefuehrt.

Date: 2026-05-02. Scope: local standard dialogs, file-path validation, dialog
description, and persisted dialog description. No network, web,
authentication, or database boundary was introduced.

| Standard | Status | Begruendung / Rationale |
|---|---|---|
| NIST SSDF | anwendbar | Sichere Erzeugung, Review und Testnachweise fuer lokale Eingaben und Persistenzdaten sind erforderlich. / Secure generation, review, and test evidence for local input and persisted data are required. |
| CWE Top 25 | anwendbar | Pfadbehandlung und Deserialisierung wurden gegen fehlerhafte Eingaben geprueft. / Path handling and deserialization were reviewed against malformed input. |
| OWASP ASVS | N/A | Kein Web/API/Auth-System. / No web/API/auth system. |
| Zero Trust | N/A | Keine Service- oder Identitaetsgrenze. / No service or identity boundary. |
| CAPEC | N/A | Keine externe Angriffsoberflaeche ausser lokalen Dateien/Persistenzdaten. / No external attack surface except local files/persisted data. |
| OWASP SAMM | anwendbar als Leitfaden | Proportionaler Secure-Coding-Review wurde dokumentiert. / Proportional secure-coding review was documented. |
| OWASP Cheat Sheet Series / Proactive Controls | anwendbar als Leitfaden | Validierung vor Nutzung, sichere Fehlerbehandlung und keine vertraulichen Daten in Meldungen. / Validate before use, safe error handling, and no secrets in messages. |
| OpenSSF Scorecard | N/A fuer Feature | Keine neue Abhaengigkeit oder Repo-weite Release-Aenderung. / No new dependency or repository-wide release change. |
| CRA awareness | anwendbar als Hinweis | Lokale Bibliotheksfunktion; Release-Nachweise bleiben beim Release-Prozess. / Local library function; release evidence remains with release process. |
| SBOM/VEX/SLSA | release-gebunden | Fuer diese Feature-Arbeit keine Artefaktfreigabe; bei Release nachziehen. / No artifact release in this feature work; update on release. |

### Secure-Coding-Review

- Datei-/Pfadvalidierung bleibt nicht-destruktiv: `TFileDialog` liefert
  `TFileDecisionResult` fuer `Open`, `Select` und `SaveTarget`, fuehrt aber
  kein Lesen, Schreiben, Loeschen oder Ueberschreiben von Dateiinhalt aus.
- Manuelle Pfade werden mit `Path.GetFullPath` normalisiert; fehlende, stale
  oder nicht lesbare Metadaten liefern textorientierte Fallback-Zustaende.
- Persistierte Dialogbeschreibungen werden vor Runtime-Erzeugung validiert.
  Truncated Payloads, Restdaten, nicht unterstuetzte Formatversionen,
  Runtime-State und semantisch ungueltige Records werden abgelehnt.
- Fehlermeldungen enthalten keine Stack-Traces, Secrets oder
  Verbindungszeichenketten.

English summary: file and path handling is non-destructive, persisted dialog
input is validated before runtime creation, malformed records are rejected, and
user-facing messages stay text-first without leaking internal state.

## 011-port-wave2-examples

Datum: 2026-05-08. Scope: lokale Controls-/Dialog-Beispiele,
Headless-Smoke-Tests, strukturierte `dlgdsn`-Beschreibung, Dateisystem-
Metadaten in `demo` und keine neue Netzwerk-, Web-, Auth-, Service- oder
Datenbankgrenze.

Date: 2026-05-08. Scope: local controls/dialog examples, headless smoke tests,
structured `dlgdsn` descriptions, file-system metadata in `demo`, and no new
network, web, auth, service, or database boundary.

| Standard | Status | Begruendung / Rationale |
|---|---|---|
| NIST SSDF | anwendbar | Tests werden vor Implementierung angelegt, Eingaben werden validiert, und Nachweise bleiben reviewbar. / Tests are created before implementation, inputs are validated, and evidence remains reviewable. |
| CWE Top 25 | anwendbar | Lokale Pfade, strukturierte Beschreibungen und Clipboard-Fallbacks werden gegen fehlerhafte Eingaben geprueft. / Local paths, structured descriptions, and clipboard fallbacks are checked against invalid input. |
| STRIDE | anwendbar proportional | Spoofing/Auth/Remote-Aspekte sind N/A; Tampering/DoS/Information Disclosure fuer lokale Daten werden begrenzt. / Spoofing/auth/remote aspects are N/A; tampering/DoS/information disclosure for local data are bounded. |
| CAPEC | anwendbar proportional | Keine externe Angriffsoberflaeche; lokale malformed-input-Faelle bleiben relevant. / No external attack surface; local malformed-input cases remain relevant. |
| OWASP ASVS | N/A | Kein Web/API/Auth-System. / No web/API/auth system. |
| SBOM/VEX/SLSA | release-gebunden | Keine Feature-lokale Artefaktfreigabe. / No feature-local artifact release. |

### Secure-Coding-Review

- `demo` nutzt Dateisystem-Metadaten, Wildcards, manuelle Pfade und
  Abbruch-/Invalid-Zustaende, fuehrt aber kein Dateiinhalt-I/O aus.
- `dlgdsn` validiert strukturierte Beschreibungen vor Runtime-Erzeugung und
  weist malformed, incomplete, duplicate-control und invalid-navigation sichtbar
  zurueck.
- Clipboard-Fallbacks werden als sichtbarer Zustand behandelt und nicht still
  uebersprungen.
- Fortschrittsbeispiele verwenden deterministische Schritte statt
  unkontrollierter Timer oder Sleeps.

English summary: wave-2 examples keep local data handling non-destructive,
validate structured dialog input before rendering, expose clipboard fallback
states, and keep progress flows deterministic.

## 012-interactive-wave2-demos

Datum: 2026-05-10. Scope: interaktive lokale Wave-2-Beispiele, Menue- und
Command-Dispatch, app-loop-basierte Smoke-Tests, Guide-/Evidence-Updates.
Keine neue Netzwerk-, Web-, Auth-, Service-, Datenbank- oder Paketgrenze wurde
eingefuehrt.

Date: 2026-05-10. Scope: interactive local Wave 2 examples, menu and command
dispatch, app-loop smoke tests, guide/evidence updates. No network, web, auth,
service, database, or package boundary was introduced.

| Standard | Status | Begruendung / Rationale |
|---|---|---|
| NIST SSDF | anwendbar | Test-first App-Loop-Smokes und Review-Evidence sichern die lokale Runtime-Aenderung ab. / Test-first app-loop smokes and review evidence cover the local runtime change. |
| CWE Top 25 | anwendbar | Relevante lokale Eingaben bleiben Pfade, Fixtures und Clipboard-Fallbacks; keine neue kritische Kategorie. / Relevant local inputs remain paths, fixtures, and clipboard fallbacks; no new critical category. |
| OWASP ASVS | N/A | Kein Web/API/Auth-System. / No web/API/auth system. |
| Zero Trust | N/A | Keine Service- oder Identitaetsgrenze. / No service or identity boundary. |
| SBOM/VEX/SLSA | release-gebunden | Keine neue Abhaengigkeit und keine Feature-lokale Artefaktfreigabe. / No new dependency and no feature-local artifact release. |

English summary: 012 changes local example command dispatch and documentation,
keeps file/fixture handling bounded, and adds no new dependency or external
trust boundary.

## 013-wave2-visual-component-remediation

Datum: 2026-05-30. Scope: lokale sichtbare Welle-2-Beispielkomponenten,
Statuszeile, `Help -> Description`, app-loop-Smokes mit Buffer-Nachweis und
Guide-/Evidence-Aktualisierung. Keine neue Netzwerk-, Web-, Auth-, Service-,
Datenbank-, Paket- oder KI-Runtime-Grenze wurde eingefuehrt.

Date: 2026-05-30. Scope: local visible Wave 2 example components, status line,
`Help -> Description`, app-loop smokes with buffer proof, and guide/evidence
updates. No new network, web, auth, service, database, package, or AI runtime
boundary was introduced.

| Standard | Status | Begruendung / Rationale |
|---|---|---|
| NIST SSDF | anwendbar | Test-first Smokes, Evidence und Review-Notizen decken die lokale Aenderung ab. / Test-first smokes, evidence, and review notes cover the local change. |
| CWE Top 25 | anwendbar | Lokale Inputs bleiben Pfade, Fixtures, Commands und Clipboard-Fallbacks. / Local inputs remain paths, fixtures, commands, and clipboard fallbacks. |
| OWASP ASVS | N/A | Kein Web/API/Auth-System. / No web/API/auth system. |
| Zero Trust | N/A | Keine Service- oder Identitaetsgrenze. / No service or identity boundary. |
| AI-SBOM | N/A | Keine ausgelieferte Produkt-/Runtime-KI. / No shipped product/runtime AI. |
