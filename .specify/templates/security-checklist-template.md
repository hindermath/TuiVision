# Sicherheits-Checkliste / Security Checklist: [PROJECT / FEATURE]

**Projekt / Project**: [Level-2 project name]
**Sprache / Language**: [Primary language from Level-2 registry]
**Prüfer / Reviewer**: [Name]
**Datum / Date**: [YYYY-MM-DD]
**Constitution-Referenz / Constitution Reference**: Principle XII (A.8.28)

<!--
  Diese Checkliste wird bei Code-Reviews für sicherheitsrelevante Änderungen verwendet.
  Wähle den sprachspezifischen Abschnitt passend zum Level-2-Projekt.
  Nicht zutreffende Punkte mit N/A markieren und begründen.

  This checklist is used during code reviews for security-relevant changes.
  Select the language-specific section matching the Level-2 project.
  Mark non-applicable items as N/A with justification.
-->

## Allgemein / General (alle Sprachen / all languages)

### Eingabeverarbeitung / Input Handling

- [ ] Alle externen Eingaben an Trust Boundaries validiert und bereinigt
- [ ] Keine Verwendung von `eval()`, `Invoke-Expression` oder Äquivalenten auf nicht vertrauenswürdigem Input
- [ ] Ausgabe-Encoding für den jeweiligen Kontext (HTML, SQL, Shell, URL) angewendet

### Kryptografie / Cryptography

- [ ] Aktuelle Algorithmen verwendet (AES-256, SHA-256+, Ed25519, RSA >= 3072)
- [ ] Keine veralteten Algorithmen (MD5, SHA-1 für Signaturen, DES, RC4) ohne explizite Risikobegründung
- [ ] Zufallszahlen aus kryptografisch sicherer Quelle (`SecureRandom`, `/dev/urandom`, `RandomNumberGenerator`)

### Fehlerbehandlung / Error Handling

- [ ] Keine Stack-Traces, Connection-Strings oder interner Zustand an Endbenutzer exponiert
- [ ] Fehlerpfade fallen in sicheren Zustand zurück (Deny by Default)
- [ ] Sicherheitsrelevante Fehler werden geloggt (ohne Secrets im Log)

### Authentifizierung & Autorisierung / Authentication & Authorization

- [ ] Authentifizierung an zentraler Stelle implementiert (Middleware, Filter)
- [ ] Autorisierung prüft Least Privilege (minimale Berechtigungen)
- [ ] Session-/Token-Management folgt aktuellen Best Practices

### Abhängigkeiten / Dependencies

- [ ] Keine neuen Abhängigkeiten mit bekannten kritischen CVEs
- [ ] Abhängigkeiten aus verifizierten Registries bezogen
- [ ] Lock-File aktualisiert und committed

### Konfiguration / Configuration

- [ ] Keine Secrets im Quellcode oder in Git-tracked Konfigurationsdateien
- [ ] Secrets in plattformgeeignetem Secret-Store (Key Vault, Keychain, CI-Secrets)

---

## C# / .NET — Spezifische Prüfpunkte / C# / .NET Specific Checks

- [ ] SQL-Queries parametrisiert (kein String-Concatenation)
- [ ] Anti-Forgery-Tokens in allen Formularen (`[ValidateAntiForgeryToken]`)
- [ ] Output-Encoding in Razor Views (`@Html.Encode()` oder automatisches Encoding aktiv)
- [ ] HTTPS-Redirect konfiguriert (`UseHttpsRedirection()`)
- [ ] CORS restriktiv konfiguriert (keine Wildcard `*` in Produktion)
- [ ] Deserialisierung sicher konfiguriert (kein `BinaryFormatter`, `TypeNameHandling.None` bei JSON)
- [ ] `IDataProtectionProvider` für Verschlüsselung at rest verwendet
- [ ] Dependency Injection für sicherheitsrelevante Services
- [ ] `dotnet list package --vulnerable` ohne kritische Ergebnisse

## C / C89 (cc65) — Spezifische Prüfpunkte / C / C89 Specific Checks

- [ ] Bounds-Checking auf allen Buffer-Operationen
- [ ] Kein `gets()`, kein ungeprüftes `sprintf()` / `strcpy()`
- [ ] Integer-Overflow-Guards bei arithmetischen Operationen
- [ ] `const`-Annotationen für schreibgeschützte Daten
- [ ] Externer Input in dedizierten Modulen mit geprüften Buffer-APIs isoliert
- [ ] Globaler veränderlicher Zustand minimiert

## SQL — Spezifische Prüfpunkte / SQL Specific Checks

- [ ] Nur parametrisierte Statements (keine String-Konkatenation)
- [ ] Least-Privilege-Rollen auf Schema-Ebene (getrennte Lese-/Schreibrollen)
- [ ] Kein dynamisches SQL aus nicht vertrauenswürdigem Input
- [ ] Row-Level Security wo vom DBMS unterstützt
- [ ] Stored Procedures oder parametrisierte Views als API-Grenzen

## Bash — Spezifische Prüfpunkte / Bash Specific Checks

- [ ] Alle Variablen in Anführungszeichen (`"$var"`)
- [ ] Kein `eval` auf nicht vertrauenswürdigem Input
- [ ] `--` End-of-Options-Sentinel bei externen Kommandos
- [ ] `set -euo pipefail` am Script-Anfang
- [ ] Positionsargumente und Umgebungsvariablen am Script-Eintritt validiert

## PowerShell — Spezifische Prüfpunkte / PowerShell Specific Checks

- [ ] `Set-StrictMode -Version Latest` aktiv
- [ ] Parameter mit `[ValidateNotNullOrEmpty()]`, `[ValidatePattern()]` etc. validiert
- [ ] Kein `Invoke-Expression` auf nicht vertrauenswürdigem Input
- [ ] `$ErrorActionPreference = 'Stop'` gesetzt
- [ ] Subprocess-Aufrufe mit `-NoProfile` (Windows)

---

## Ergebnis / Result

| Kategorie / Category | Bestanden / Passed | Nicht bestanden / Failed | N/A |
|---|---|---|---|
| Allgemein / General | | | |
| Sprachspezifisch / Language-specific | | | |
| **Gesamt / Total** | | | |

**Freigabe / Approval**: [ ] Freigegeben / Approved | [ ] Nacharbeit erforderlich / Rework required

**Anmerkungen / Notes**:

[Freitext für Anmerkungen / Free text for notes]

<!-- EN: security-checklist-template.md
[DE-Zusammenfassung: Sprachspezifische Sicherheits-Checkliste fuer Code-Reviews, abgeleitet aus Constitution Principle XII.]
-->
