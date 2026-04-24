# Sicherheits-Querschnittskonzepte / Security Cross-Cutting Concepts

**Projekt / Project**: [Level-2 project name]
**Erstellt / Created**: [YYYY-MM-DD]
**Letzte Aktualisierung / Last Updated**: [YYYY-MM-DD]
**Constitution-Referenz / Constitution Reference**: Principle XIII (A.8.27)
**arc42-Abschnitt / arc42 Section**: 8 (Cross-Cutting Concepts)

<!--
  Dieses Dokument beschreibt die sicherheitsrelevanten Querschnittskonzepte
  des Projekts nach arc42 Section 8. Es wird auf Projektebene gepflegt und
  bei architektonisch relevanten Änderungen aktualisiert.

  This document describes the security-relevant cross-cutting concepts
  of the project following arc42 Section 8. It is maintained at project
  level and updated on architecturally relevant changes.
-->

## 1. Authentifizierungsstrategie / Authentication Strategy

### Technologie / Technology

[z. B. ASP.NET Core Identity, JWT Bearer Tokens, OAuth 2.0, API Keys]

### Authentifizierungs-Flow / Authentication Flow

```text
[ASCII-Diagramm des Authentifizierungs-Flows]

Beispiel / Example:

  Client --> [Login-Endpoint] --> Identity Provider
                                       |
                                  JWT Token
                                       |
  Client --> [API-Request + Bearer Token] --> Middleware --> Controller
```

### Entscheidungen / Decisions

- [Verweis auf ADR, z. B. ADR-001: JWT statt Session-Cookies / Reference to ADR]

---

## 2. Autorisierungsmodell / Authorization Model

### Modelltyp / Model Type

[RBAC (Role-Based Access Control) / ABAC (Attribute-Based Access Control) / Capability-based]

### Rollen und Berechtigungen / Roles and Permissions

| Rolle / Role | Berechtigungen / Permissions | Kontext / Context |
|---|---|---|
| [z. B. Admin] | [Lesen, Schreiben, Löschen] | [Alle Ressourcen] |
| [z. B. User] | [Lesen, eigene Daten schreiben] | [Eigene Ressourcen] |
| [z. B. Service] | [Lesen] | [API-zu-API-Kommunikation] |

### Policy-Enforcement-Punkte / Policy Enforcement Points

[Wo im System wird Autorisierung durchgesetzt? Middleware, Controller-Attribute, DB-Ebene?]

---

## 3. Verschlüsselung / Encryption

### In Transit / In Transit

| Verbindung / Connection | Protokoll / Protocol | Zertifikatsquelle / Certificate Source |
|---|---|---|
| Client → API | HTTPS (TLS 1.2+) | [Let's Encrypt / Managed Cert / Self-signed (dev only)] |
| API → Datenbank | [TLS / lokale Socket] | [Managed / N/A] |
| API → Externer Dienst | HTTPS | [CA-verified] |

### At Rest / At Rest

| Daten / Data | Verschlüsselung / Encryption | Schlüsselverwaltung / Key Management |
|---|---|---|
| [z. B. Benutzerdaten] | [AES-256 via IDataProtectionProvider] | [DPAPI / Azure Key Vault] |
| [z. B. Backups] | [Dateisystem-Verschlüsselung] | [OS-Level] |

### Algorithmen / Algorithms (per Principle XII)

- Erlaubt / Allowed: AES-256, RSA >= 3072, SHA-256+, Ed25519
- Verboten / Forbidden: MD5, SHA-1 (Signaturen), DES, RC4 (ohne Risikobegründung)

---

## 4. Eingabevalidierung / Input Validation Strategy

### Trust Boundaries

[Verweis auf Threat Model oder Trust-Boundary-Diagramm]

### Validierungsschichten / Validation Layers

```text
Schicht / Layer          Verantwortung / Responsibility
---------------------------------------------------------------
1. API-Gateway / Edge    Formatvalidierung, Rate Limiting, WAF
2. Controller / Handler  Geschäftsregelvalidierung, Typprüfung
3. Service / Domain      Domänenlogik-Constraints
4. Data Access           Parametrisierte Queries, Schema-Constraints
```

### Validierungsregeln / Validation Rules

| Eingabe / Input | Validierung / Validation | Schicht / Layer |
|---|---|---|
| [z. B. Benutzer-ID] | [UUID-Format, existiert in DB] | [Controller + DA] |
| [z. B. Freitext-Suche] | [Max-Länge, HTML-Encoding] | [Controller] |

---

## 5. Fehlerbehandlung & Informationspreisgabe / Error Handling & Information Disclosure

### Fehlerbehandlungsstrategie / Error Handling Strategy

- **Produktion / Production**: Generische Fehlermeldungen an Endbenutzer, detaillierte Logs intern
- **Entwicklung / Development**: Stack-Traces im Developer-Modus erlaubt
- **Umschaltung / Toggle**: [z. B. `ASPNETCORE_ENVIRONMENT`, `.env`-basiert]

### Verbotene Informationen in Fehlerantworten / Forbidden Information in Error Responses

- Stack-Traces
- Connection-Strings
- Interne Pfade
- SQL-Fehlermeldungen
- Versionsnummern interner Komponenten

---

## 6. Logging & Audit Trail

### Was wird geloggt / What Is Logged

| Ereignis / Event | Log-Level | Enthält / Contains | Enthält NICHT / Does NOT Contain |
|---|---|---|---|
| Erfolgreicher Login | Information | Benutzer-ID, Zeitstempel | Passwort, Token |
| Fehlgeschlagener Login | Warning | Benutzer-ID, IP, Zeitstempel | Passwort |
| Autorisierungsfehler | Warning | Ressource, Benutzer-ID | Session-Daten |
| Unbehandelte Ausnahme | Error | Fehlertyp, Korrelations-ID | Stack-Trace (nur intern) |

### DSGVO / GDPR

- Personenbezogene Daten in Logs: [Welche, wie lange aufbewahrt, wie gelöscht]
- Log-Retention: [z. B. 90 Tage]

---

## 7. Abhängigkeitsverwaltung / Dependency Management

### Paketquellen / Package Sources

| Registry | Verwendung / Usage | Verifiziert / Verified |
|---|---|---|
| [nuget.org / npmjs.com / crates.io] | [Produktionsabhängigkeiten] | [Ja] |

### Audit-Prozess / Audit Process

- Tool: [z. B. `dotnet list package --vulnerable`, `npm audit`, `cargo audit`]
- Frequenz: [z. B. vor jedem Release, wöchentlich]
- Verweis auf Dependency Audit: [Link zu `docs/security/dependency-audit.md`]

### Lock-Files

- [ ] Lock-File committed und aktuell
- Datei: [z. B. `packages.lock.json`, `package-lock.json`]

---

## 8. Deployment-Sicherheit / Deployment Security

### Sicherheitszonen / Security Zones

```text
[ASCII-Diagramm der Deployment-Architektur mit Sicherheitszonen]

Beispiel / Example:

  +------ DMZ ------+    +------ Intern ------+
  |  Load Balancer   | -> |   App-Server       |
  |  WAF / Reverse   |    |   (Kestrel/IIS)    |
  |  Proxy           |    |                    |
  +------------------+    |   +----------+     |
                          |   | Datenbank|     |
                          |   +----------+     |
                          +--------------------+
```

### Gehärtete Konfiguration / Hardened Configuration

- [ ] Debug-Endpunkte in Produktion deaktiviert
- [ ] Verbose-Fehlerausgabe in Produktion deaktiviert
- [ ] Nicht benötigte Ports/Dienste geschlossen
- [ ] HTTPS erzwungen

---

## Referenzen / References

- Constitution Principle XII: Secure Code Generation (ISO 27002:2022 A.8.28)
- Constitution Principle XIII: Secure Software Architecture (ISO 27002:2022 A.8.27)
- arc42: [arc42.org](https://arc42.org)
- iSAQB CPSA-F Curriculum: Quality Attributes and Cross-Cutting Concepts

<!-- EN: arc42-security-template.md
[DE-Zusammenfassung: arc42 Section 8 Template fuer sicherheitsrelevante Querschnittskonzepte, iSAQB-aligned.]
-->
