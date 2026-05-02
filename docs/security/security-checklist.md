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
