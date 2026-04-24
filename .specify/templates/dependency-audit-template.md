# Abhängigkeits-Audit / Dependency Audit: [PROJECT]

**Projekt / Project**: [Level-2 project name]
**Sprache / Language**: [Primary language from Level-2 registry]
**Prüfer / Reviewer**: [Name]
**Datum / Date**: [YYYY-MM-DD]
**Constitution-Referenz / Constitution Reference**: Principle XII (A.8.28), Principle XIII (A.8.27)

<!--
  Dieses Dokument erfasst den Audit-Status aller direkten und transitiven
  Abhängigkeiten des Projekts. Es wird vor jedem Release und mindestens
  monatlich aktualisiert. Nicht zutreffende Abschnitte mit N/A markieren.

  This document captures the audit status of all direct and transitive
  dependencies of the project. It is updated before each release and at
  least monthly. Mark non-applicable sections as N/A.
-->

## 1. Audit-Werkzeuge / Audit Tools

| Sprache / Language | Werkzeug / Tool | Befehl / Command | Letzte Ausführung / Last Run |
|---|---|---|---|
| C# / .NET | `dotnet list package --vulnerable` | `dotnet list package --vulnerable --include-transitive` | [YYYY-MM-DD] |
| C / C89 (cc65) | [manuell / manual] | [Beschreibung / Description] | [YYYY-MM-DD] |
| SQL | N/A | N/A | N/A |
| Bash | ShellCheck + manuell | `shellcheck *.sh` | [YYYY-MM-DD] |
| PowerShell | PSScriptAnalyzer + manuell | `Invoke-ScriptAnalyzer -Path .` | [YYYY-MM-DD] |

## 2. Direkte Abhängigkeiten / Direct Dependencies

<!--
  Liste aller direkten Abhängigkeiten mit Version, Lizenz und
  bekannten Schwachstellen. Sortiert nach Risiko (höchstes zuerst).

  List all direct dependencies with version, license, and known
  vulnerabilities. Sorted by risk (highest first).
-->

| Paket / Package | Version | Lizenz / License | Registry | CVEs | Risiko / Risk | Status |
|---|---|---|---|---|---|---|
| [z. B. Newtonsoft.Json] | [13.0.3] | [MIT] | [nuget.org] | [Keine / None] | [Niedrig / Low] | [OK] |
| [z. B. System.Text.Json] | [8.0.0] | [MIT] | [nuget.org] | [Keine / None] | [Niedrig / Low] | [OK] |

## 3. Transitive Abhängigkeiten mit bekannten CVEs / Transitive Dependencies with Known CVEs

<!--
  Nur transitive Abhängigkeiten mit bekannten Schwachstellen auflisten.
  Für jede CVE: Schweregrad, ob sie im Kontext des Projekts ausnutzbar ist,
  und geplante Mitigation.

  List only transitive dependencies with known vulnerabilities.
  For each CVE: severity, whether exploitable in project context,
  and planned mitigation.
-->

| Paket / Package | Version | CVE | CVSS | Ausnutzbar / Exploitable | Mitigation | Frist / Deadline |
|---|---|---|---|---|---|---|
| [Beispiel] | [1.2.3] | [CVE-YYYY-NNNNN] | [7.5 Hoch] | [Ja/Nein + Begründung] | [Update / Workaround / Akzeptiert] | [YYYY-MM-DD] |

## 4. Lizenz-Compliance / License Compliance

<!--
  Prüfe alle Abhängigkeiten auf Lizenzkompatibilität mit dem Projekt.
  Copyleft-Lizenzen (GPL, AGPL) erfordern besondere Aufmerksamkeit.

  Check all dependencies for license compatibility with the project.
  Copyleft licenses (GPL, AGPL) require special attention.
-->

### Erlaubte Lizenzen / Allowed Licenses

- MIT, Apache-2.0, BSD-2-Clause, BSD-3-Clause, ISC, Zlib, Unlicense

### Eingeschränkt erlaubte Lizenzen / Restricted Licenses

- LGPL-2.1, LGPL-3.0 (nur dynamisches Linking / dynamic linking only)
- MPL-2.0 (Dateigrenzen beachten / file boundary rules apply)

### Verbotene Lizenzen / Forbidden Licenses

- GPL-2.0, GPL-3.0, AGPL-3.0 (ohne explizite Genehmigung / without explicit approval)
- Proprietäre Lizenzen ohne schriftliche Vereinbarung / Proprietary licenses without written agreement

### Lizenz-Abweichungen / License Exceptions

| Paket / Package | Lizenz / License | Begründung / Justification | Genehmigt von / Approved by |
|---|---|---|---|
| | | | |

## 5. Registry-Verifizierung / Registry Verification

<!--
  Stelle sicher, dass alle Pakete aus verifizierten Registries stammen.
  Keine Pakete aus nicht vertrauenswürdigen Quellen.

  Ensure all packages come from verified registries.
  No packages from untrusted sources.
-->

| Registry | URL | Verifiziert / Verified | Anmerkungen / Notes |
|---|---|---|---|
| NuGet | nuget.org | [Ja/Nein] | [Offizielle Microsoft-Registry] |
| npm | npmjs.com | [Ja/Nein] | [Nur falls JS/TS-Abhängigkeiten] |
| crates.io | crates.io | [Ja/Nein] | [Nur falls Rust-Abhängigkeiten] |
| PyPI | pypi.org | [Ja/Nein] | [Nur falls Python-Abhängigkeiten] |

- [ ] Keine privaten oder gespiegelten Registries ohne dokumentierte Begründung
- [ ] Package-Signaturen geprüft (wo verfügbar)
- [ ] Typosquatting-Check durchgeführt (Paketnamen visuell geprüft)

## 6. Lock-File-Status / Lock File Status

| Datei / File | Vorhanden / Exists | Committed | Aktuell / Current | Letztes Update / Last Update |
|---|---|---|---|---|
| [z. B. packages.lock.json] | [Ja/Nein] | [Ja/Nein] | [Ja/Nein] | [YYYY-MM-DD] |
| [z. B. package-lock.json] | [Ja/Nein] | [Ja/Nein] | [Ja/Nein] | [YYYY-MM-DD] |

## 7. Supply-Chain-Sicherheit / Supply Chain Security

<!--
  Bewertung der Supply-Chain-Risiken nach Principle XIII.

  Assessment of supply chain risks per Principle XIII.
-->

### Prüfpunkte / Checkpoints

- [ ] Alle Abhängigkeiten aus verifizierten Registries (siehe Abschnitt 5)
- [ ] Lock-Files committed und aktuell (siehe Abschnitt 6)
- [ ] Keine Abhängigkeiten mit bekannten kritischen CVEs ohne Mitigation-Plan
- [ ] Lizenz-Compliance geprüft (siehe Abschnitt 4)
- [ ] Typosquatting-Risiko bewertet
- [ ] Verwaiste Pakete identifiziert (kein Maintainer seit > 12 Monaten)
- [ ] CI/CD-Pipeline prüft Abhängigkeiten automatisch

### Verwaiste Pakete / Abandoned Packages

| Paket / Package | Letztes Update / Last Update | Alternatives Paket / Alternative | Aktion / Action |
|---|---|---|---|
| | | | [Ersetzen / Beobachten / Akzeptieren] |

## 8. Audit-Ergebnis / Audit Result

| Kategorie / Category | Status | Anmerkungen / Notes |
|---|---|---|
| Kritische CVEs / Critical CVEs | [0 / N] | |
| Hohe CVEs / High CVEs | [0 / N] | |
| Lizenz-Konflikte / License Conflicts | [0 / N] | |
| Verwaiste Pakete / Abandoned Packages | [0 / N] | |
| Lock-File-Status | [OK / Veraltet] | |
| **Gesamtergebnis / Overall Result** | [Bestanden / Nicht bestanden] | |

**Freigabe / Approval**: [ ] Freigegeben / Approved | [ ] Nacharbeit erforderlich / Rework required

**Nächstes Audit / Next Audit**: [YYYY-MM-DD]

## Referenzen / References

- Constitution Principle XII: Secure Code Generation (ISO 27002:2022 A.8.28)
- Constitution Principle XIII: Secure Software Architecture (ISO 27002:2022 A.8.27)
- OWASP Dependency-Check: [owasp.org/www-project-dependency-check](https://owasp.org/www-project-dependency-check/)
- NIST NVD: [nvd.nist.gov](https://nvd.nist.gov/)
- SPDX License List: [spdx.org/licenses](https://spdx.org/licenses/)

<!-- EN: dependency-audit-template.md
[DE-Zusammenfassung: Template fuer Abhaengigkeits-Audits mit CVE-Tracking, Lizenz-Compliance und Supply-Chain-Sicherheit, abgeleitet aus Constitution Principle XII und XIII.]
-->
