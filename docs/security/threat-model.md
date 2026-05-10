# Bedrohungsmodell / Threat Model: TuiVision

**Projekt / Project**: TuiVision (Level-2)
**Datum / Date**: 2026-04-24
**Status**: Stub — mit projektspezifischen Inhalten zu befuellen / Stub — to be populated
**Template-Quelle / Template Source**: `.specify/templates/threat-model-template.md`

<!--
  Dieses Dokument ist ein Stub. Die vollstaendige Struktur findet sich im
  Template unter .specify/templates/threat-model-template.md. Bei der Befuellung das Template als Vorlage
  verwenden.

  This document is a stub. The complete structure can be found in the
  template at .specify/templates/threat-model-template.md. Use the template as a guide when populating.
-->

[Zu befuellen / To be populated — see template]

## 011-port-wave2-examples

### Assets / Schutzwerte

- Lokale Dateisystem-Metadaten, die `demo` fuer Standarddialoge anzeigt.
- Strukturierte `dlgdsn`-Beschreibungen und Quellfixtures.
- Clipboard-Testzustand ueber `ManagedClipboard`.
- Textorientierte Smoke-Test-Ausgaben und Guides.

### Trust Boundaries / Vertrauensgrenzen

Die Feature-Arbeit bleibt lokal. Es gibt keine Netzwerk-, Web-, Auth-,
Remote-Service- oder Datenbankgrenze. Relevante Grenzen sind lokale Pfade,
persistierte Dialogbeschreibungen und isolierte Zwischenablagezustaende.

The feature remains local. It introduces no network, web, auth, remote service,
or database boundary. Relevant boundaries are local paths, persisted dialog
descriptions, and isolated clipboard states.

### STRIDE/CAPEC Notes

| Kategorie / Category | Bewertung / Assessment |
|---|---|
| Spoofing | N/A fuer lokale Beispiele ohne Identitaetssystem / N/A for local examples without identity system |
| Tampering | Strukturierte Dialogbeschreibungen werden validiert und fehlerhafte Varianten sichtbar abgelehnt. / Structured dialog descriptions are validated and invalid variants are visibly rejected. |
| Repudiation | N/A; keine Audit- oder Benutzerkontenfunktion. / N/A; no audit or user-account feature. |
| Information Disclosure | Fehlermeldungen bleiben textorientiert und enthalten keine Secrets, Tokens oder Stack-Traces. / Messages stay text-first and contain no secrets, tokens, or stack traces. |
| Denial of Service | Fortschritt und Smoke-Flows sind deterministisch und verwenden keine unbounded background work. / Progress and smoke flows are deterministic and use no unbounded background work. |
| Elevation of Privilege | N/A; keine privilegierten Operationen. / N/A; no privileged operations. |

### Outcome

Das Restrisiko ist fuer Welle 2 akzeptiert, weil die Beispiele keine neue
externe Angriffsoberflaeche schaffen und die relevanten lokalen Eingaben durch
Tests und sichtbare Ablehnungszustaende abgedeckt werden.

The residual risk is accepted for wave 2 because the examples create no new
external attack surface and the relevant local inputs are covered by tests and
visible rejection states.

## 012-interactive-wave2-demos

### Assets / Schutzwerte

- Sichtbare Beispielzustandsmeldungen und Menue-/Command-Pfade.
- Source-controlled `DlgDsn`-Fixtures und lokale `Demo`-Metadatenpfade.
- Session-only Eingabe-/History-Zustand in `InpLis`.
- Smoke-Test-Evidence fuer app-loop-basierte Bedienpfade.

### Trust Boundaries / Vertrauensgrenzen

012 bleibt lokal. Es gibt keine neue Netzwerk-, Web-, Auth-, Remote-Service-
oder Datenbankgrenze. Die bestehenden lokalen Grenzen aus 011 bleiben gueltig:
Pfadmetadaten, Fixture-Namen, strukturierte Dialogbeschreibungen und
Clipboard-Fallbacks.

012 remains local. It introduces no new network, web, auth, remote service, or
database boundary. The existing local boundaries from 011 remain valid: path
metadata, fixture names, structured dialog descriptions, and clipboard
fallbacks.

### Outcome

Restrisiko bleibt niedrig. Primaere Beweise laufen jetzt ueber `app.Run()` mit
injizierten Commands; direkte Hilfsmethoden zaehlen nur als Setup oder
ergaenzende Assertions.

Residual risk remains low. Primary proof now runs through `app.Run()` with
injected commands; direct helpers count only as setup or supplemental
assertions.
