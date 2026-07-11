# Bedrohungsmodell / Threat Model: TuiVision

**Stand / Current as of**: 2026-07-11
**Methode / Method**: STRIDE with CIA impact and selected CAPEC patterns
**Scope**: Local terminal UI framework, tests, examples, repository tooling, CI

## Schutzwerte / Assets

- Integrität von Terminalzustand, Events, Commands, View-Hierarchie und
  gerenderten Zellen.
- Integrität und Verfügbarkeit serialisierter Ressourcen, Hilfe- und
  Dialogdaten sowie lokaler Dateipfade.
- Vertraulichkeit von Credentials, Tokens, lokalen Nutzerdaten und
  Agent-/CI-Konfiguration.
- Integrität von Quellcode, Dependencies, Workflows, Releases und Evidence.

*Assets include terminal state, event and view integrity, serialized resources,
local paths, credentials, source, dependencies, workflows, releases, and
evidence.*

## Vertrauensgrenzen / Trust Boundaries

```text
Terminal input -> Console driver -> Core events -> Controls/application
Local path/resource -> Validation -> Serialization/file controls
NuGet/npm/GitHub Actions -> Restore/CI -> Build/test/release evidence
Agent/tool input -> Repository sandbox -> Review/commit/PR
Generated output -> Temporary/ignored storage -> Evidence summary
```

Es gibt keine Produkt-Web-API, keine Authentifizierung, keinen Cloud-Service,
keine Datenbank und keine Runtime-KI. Diese fehlenden Grenzen begründen ASVS-,
Zero-Trust-, C3A-, C5-, DPIA- und AI-SBOM-`N/A`, nicht aber ein allgemeines
Security-`N/A`.

## STRIDE, CIA und CAPEC / STRIDE, CIA, and CAPEC

| Bereich / Area | STRIDE/CIA | Relevante Angriffsmuster / Relevant patterns | Mitigation und Evidenz / Mitigation and evidence | Restrisiko / Residual risk |
|---|---|---|---|---|
| Terminal-/Eventeingabe | Tampering, DoS; I/A | CAPEC-10 Buffer Overflow, CAPEC-20 Input Data Manipulation | Begrenzte managed Buffer, Eventvalidierung, Tests in Core/Controls/Drivers | Low |
| Datei-/Ressourcenpfade | Tampering, Information Disclosure; C/I/A | CAPEC-126 Path Traversal, CAPEC-153 Input Data Manipulation | Pfadvalidierung, kontrollierte Fixtures, sichere Ablehnung, keine beliebigen Dateiinhalte als Proof | Low |
| Serialisierte Daten | Tampering, DoS; I/A | CAPEC-130 Excessive Allocation, CAPEC-153 | Truncated/trailing/unknown/cyclic rejection tests, Typregistrierung | Low |
| Fehlermeldungen/Output | Information Disclosure; C | CAPEC-215 Fuzzing for sensitive output | Keine Secrets/Stack-Traces in nutzerseitiger Ausgabe, Secret-Scans | Low |
| Repository-Scripts | Tampering, Elevation via workflow; I | CAPEC-15 Command Delimiters, CAPEC-126 | Strikte Argument-/Pfadprüfung, Preview, isolierter Commit, Paritätstests | Low after 016 |
| Dependencies/Actions | Tampering, Spoofing; I/A | CAPEC-438 Supply Chain | Immutable Action-SHAs, Package-Audit, CycloneDX, Update-Automation | Low; provenance follow-up |
| Agent-/CI-Grenze | Information Disclosure, Tampering; C/I | CAPEC-37 Retrieve Embedded Sensitive Data | Kein Agent-State in Git, Secret-Scan, Review, begrenzte Evidence | Medium human-owned sandbox controls |

## Risikobehandlung / Risk Treatment

- Critical/High: vor Merge beheben und beweisen oder Merge blockieren.
- Medium: begrenzt beheben oder mit Owner und konkreter Grenze dokumentieren.
- Human-only: nicht durch Agentenevidenz schließen.
- Modell neu bewerten bei Netzwerk-/Cloud-/Auth-/Runtime-AI-Scope, neuer
  Persistenz, neuen Paketen, Sicherheitsvorfall oder relevanter Architektur.

*Critical/high risks block merge. Medium risks are remediated or explicitly
owned. Human-only controls are not closed by agent evidence. Re-evaluate on
new network, cloud, auth, runtime AI, persistence, package, incident, or
architecture scope.*
