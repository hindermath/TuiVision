<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 05 – Lieferkette und Build-Integritaet / Supply Chain and Build Integrity

**Dokument-ID / Document ID:** CL-05
**Version / Version:** 2.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Dokumenttyp / Document type:** Kanonische, wiederverwendbare Pruefvorlage / Canonical reusable review template

### Nachweisinstanz / Evidence Instance

Diese Datei ist eine wiederverwendbare Vorlage. Ausgefüllte Projektnachweise werden unter `docs/security/secure-development/<datum>-<scope>/` abgelegt und nennen Projekt, Scope, Prüfdatum, Baseline-Version, verantwortliche Person und Reviewer. / This file is a reusable template. Completed project evidence is stored under `docs/security/secure-development/<date>-<scope>/` and names project, scope, review date, baseline version, responsible person, and reviewer.

### Zweck / Purpose

**DE:** Diese Checkliste prüft, ob das Projekt seine Lieferkette transparent
hält (SBOM, VEX), die Build-Integrität nachweist (SLSA) und die Pflege der
Abhängigkeiten sicher steuert.

**EN:** This checklist verifies that the project keeps its supply chain
transparent (SBOM, VEX), proves build integrity (SLSA), and manages
dependency upkeep safely.

### Geltungsbereich / Scope

**DE:** Pflicht für alle releasefähigen oder verteilbaren Artefakte. Empfohlen
für interne Bibliotheken, sobald sie von mehreren Teams genutzt werden.

**EN:** Mandatory for all releasable or distributable artefacts. Recommended
for internal libraries as soon as more than one team uses them.

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung
- ISO/IEC 27002:2022 A.5.21, A.8.30
- CycloneDX, SPDX (SBOM-Formate)
- CSAF (VEX-Format)
- SLSA Framework
- OpenSSF Scorecard
- G7 „Software Bill of Materials for AI – Minimum Elements" (2026)

#### URL-/Ablageverweise / URLs and Storage Locations

**DE:** Diese Links helfen beim Review. Projekt- oder organisationsinterne Dokumente koennen als lokale Arbeitskopie oder als Verweis auf den festgelegten Ablageort ergaenzt werden.

**EN:** These links help during reviews. Project or organization-internal documents can be added as local working copies or references to the defined storage location.

- **Richtlinie Sichere Entwicklung / Secure Development Guideline:** [lokale Arbeitsfassung in diesem Repository / local working copy in this repository](../Richtlinie_Sichere-Entwicklung.md)
- **Verfassung / Constitution:** [lokale Arbeitskopie der Verfassung / local working copy of the constitution](../../../constitution.md), [Verfassung im GitHub-Repository home-baseline / constitution in the home-baseline GitHub repository](https://github.com/hindermath/home-baseline/blob/main/constitution.md)
- **Checklisten-Index / Checklist index:** [Übersicht aller Checklisten / overview of all checklists](../README.md)
- **Leitlinie fuer sichere Programmierung / Secure coding guideline:** dieser Leitfaden oder eine projektspezifische gleichwertige Leitlinie / this guide or an equivalent project-specific guideline
- **Secure coding guideline:** this guide or an equivalent project-specific guideline
- **CISA Memory Safe Roadmaps:** [lokale PDF-Kopie des CISA-Dokuments / local PDF copy of the CISA document](../mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.pdf), [EN-Markdown](../mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.EN.md), [DE-Lernfassung](../mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.DE.md), [CISA-Webseite zum Dokument / CISA webpage for the document](https://www.cisa.gov/resources-tools/resources/case-memory-safe-roadmaps)
- **ISO/IEC 27001:2022:** [offizielle ISO-Webseite zur ISO/IEC 27001:2022 / official ISO webpage for ISO/IEC 27001:2022](https://www.iso.org/standard/27001)
- **ISO/IEC 27002:2022:** [offizielle ISO-Webseite zur ISO/IEC 27002:2022 / official ISO webpage for ISO/IEC 27002:2022](https://www.iso.org/standard/75652.html)
- **NIST SSDF SP 800-218:** [NIST-Veröffentlichung SP 800-218 Secure Software Development Framework / NIST publication SP 800-218 Secure Software Development Framework](https://csrc.nist.gov/publications/detail/sp/800-218/final)
- **NIST Zero Trust SP 800-207:** [NIST-Veröffentlichung SP 800-207 Zero Trust Architecture / NIST publication SP 800-207 Zero Trust Architecture](https://csrc.nist.gov/publications/detail/sp/800-207/final)
- **OWASP ASVS:** [OWASP-Projektseite Application Security Verification Standard / OWASP project page Application Security Verification Standard](https://owasp.org/www-project-application-security-verification-standard/)
- **OWASP Cheat Sheet Series:** [OWASP Cheat Sheet Series Projektseite / OWASP Cheat Sheet Series project page](https://cheatsheetseries.owasp.org/)
- **OWASP Proactive Controls:** [OWASP Proactive Controls Projektseite / OWASP Proactive Controls project page](https://owasp.org/www-project-proactive-controls/)
- **OWASP SAMM:** [OWASP SAMM Projektseite / OWASP SAMM project page](https://owaspsamm.org/)
- **OWASP Top 10 for LLM Applications:** [OWASP Top 10 for LLM Applications Projektseite / OWASP Top 10 for LLM Applications project page](https://owasp.org/www-project-top-10-for-large-language-model-applications/)
- **CWE Top 25:** [MITRE CWE Top 25 Übersicht / MITRE CWE Top 25 overview](https://cwe.mitre.org/top25/)
- **CAPEC:** [MITRE CAPEC Katalog / MITRE CAPEC catalogue](https://capec.mitre.org/)
- **CycloneDX:** [CycloneDX SBOM-Standard Projektseite / CycloneDX SBOM standard project page](https://cyclonedx.org/)
- **SPDX:** [SPDX SBOM-Standard Projektseite / SPDX SBOM standard project page](https://spdx.dev/)
- **CSAF/VEX:** [OASIS CSAF und VEX Dokumentation / OASIS CSAF and VEX documentation](https://oasis-open.github.io/csaf-documentation/)
- **SLSA:** [SLSA Supply-chain Levels for Software Artifacts Projektseite / SLSA project page](https://slsa.dev/)
- **OpenSSF Scorecard:** [OpenSSF Scorecard Projektseite / OpenSSF Scorecard project page](https://scorecard.dev/)
- **RFC 9116 security.txt:** [RFC 9116 zu security.txt / RFC 9116 for security.txt](https://www.rfc-editor.org/rfc/rfc9116)
- **NIST AI Risk Management Framework:** [NIST AI Risk Management Framework Webseite / NIST AI Risk Management Framework webpage](https://www.nist.gov/itl/ai-risk-management-framework)
- **EU Cyber Resilience Act:** [EU-Amtsblatt zum Cyber Resilience Act / EU Official Journal for the Cyber Resilience Act](https://eur-lex.europa.eu/eli/reg/2024/2847/oj)
- **BSI TR-02102:** [BSI-Webseite zur Technischen Richtlinie TR-02102 / BSI webpage for Technical Guideline TR-02102](https://www.bsi.bund.de/DE/Themen/Unternehmen-und-Organisationen/Standards-und-Zertifizierung/Technische-Richtlinien/TR-nach-Thema-sortiert/tr02102/tr-02102.html)

### Bewertung und Dokumentation / Assessment and Documentation

**DE:** Jeder Prüfpunkt bekommt genau einen Wert je Statusachse. Schreibe die Begründung so, dass eine neue Kollegin oder ein neuer Kollege den Entscheid später ohne Rückfrage versteht.

**EN:** Each checklist item gets exactly one value per status axis. Write the explanation so that a new team member can understand the decision later without asking again.

- **Anwendbarkeit / Applicability:** `Applicable`, `N/A` oder `Open`.
- **Umsetzungsstatus / Implementation status:** `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed`.

**Pflichtfelder je Prüfpunkt / Required fields per item:** Anwendbarkeit, Umsetzungsstatus, Lernstufe, verantwortliche Rolle, Begründung, Evidenzpfad oder Link, Restrisiko, Neubewertungs-Trigger und nächste Maßnahme mit Zieltermin.

### Durchführungshinweise / Implementation Guidance

**DE:** Nutze diese Checkliste nicht als reine Ja/Nein-Liste. Sie ist ein Arbeits- und Auditdokument. Prüfe jeden Punkt gegen reale Artefakte: Code, Pull Request, Architekturdiagramm, Build-Log, Scan-Ergebnis, Ticket, Betriebsdokumentation oder Freigabeprotokoll. Wenn ein Nachweis noch fehlt, setze den Umsetzungsstatus auf `Not Assessed` oder `Not Fulfilled` und lege eine konkrete Folgeaufgabe an.

**EN:** Do not use this checklist as a simple yes/no list. It is a working and audit document. Check each item against real artefacts: code, pull request, architecture diagram, build log, scan result, ticket, operations document, or approval record. If evidence is missing, set the implementation status to `Not Assessed` or `Not Fulfilled` and create a concrete follow-up task.

**DE:** Schreibe kurze, klare Begründungen. Vermeide Abkürzungen ohne Erklärung. Wenn ein Punkt technisch schwierig ist, beschreibe den aktuellen Stand, das Risiko und den nächsten machbaren Schritt.

**EN:** Write short and clear explanations. Avoid unexplained abbreviations. If an item is technically difficult, describe the current state, the risk, and the next feasible step.

**DE:** Jeder Prüfpunkt muss deshalb drei Fragen beantworten: Was bedeutet die Anforderung im Projektalltag? Was ist konkret zu tun oder zu entscheiden? Welcher Nachweis zeigt das Ergebnis? Verwende Standard-IDs, Toolnamen und Abkürzungen nur zusammen mit einer kurzen Erklärung in Alltagssprache. Wenn ein Punkt für Auszubildende oder neue Teammitglieder nicht selbsterklärend ist, ergänze eine kurze Erklärung in der Begründung.

**EN:** Each item must therefore answer three questions: What does the requirement mean in daily project work? What exactly must be done or decided? Which evidence shows the result? Use standard IDs, tool names, and abbreviations only together with a short plain-language explanation. If an item is not self-explanatory for apprentices or new team members, add a short explanation in the rationale.

### Beispiel / Example

**DE:** Ein .NET-Release enthaelt eine CycloneDX-SBOM, eine signierte Build-Provenance und einen Lizenzbericht. Eine bekannte CVE in einer Bibliothek ist per VEX als „nicht betroffen" begründet, weil die verwundbare Funktion nicht genutzt wird.

**EN:** A .NET release contains a CycloneDX SBOM, signed build provenance, and a licence report. A known CVE in a library is explained by VEX as "not affected" because the vulnerable function is not used.

### A11Y-Hinweise / A11Y Notes

**DE:** Beim Ausfüllen dieser Checkliste müssen alle Nachweise auch textlich verständlich sein. Verweise sollen beschreibende Linktexte haben. Screenshots, Diagramme oder Scan-Auszüge brauchen eine kurze Textbeschreibung. Der Status darf nicht nur über Farbe erkennbar sein.

**EN:** When this checklist is filled in, all evidence must also be understandable as text. References should use descriptive link text. Screenshots, diagrams, or scan extracts need a short text description. The status must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [SBOM / Software Bill of Materials](#cl-05-glossar-sbom)
- [AI-SBOM / ML-BOM](#cl-05-glossar-ai-sbom-ml-bom)
- [CycloneDX](#cl-05-glossar-cyclonedx)
- [SPDX](#cl-05-glossar-spdx)
- [VEX / Vulnerability Exploitability eXchange](#cl-05-glossar-vex)
- [CSAF](#cl-05-glossar-csaf)
- [SLSA](#cl-05-glossar-slsa)
- [Provenance / Herkunftsnachweis](#cl-05-glossar-provenance)
- [Attestation / Bescheinigung](#cl-05-glossar-attestation)
- [Sigstore / Cosign](#cl-05-glossar-sigstore-cosign)
- [in-toto](#cl-05-glossar-in-toto)
- [CVE](#cl-05-glossar-cve)
- [CVSS](#cl-05-glossar-cvss)
- [EPSS](#cl-05-glossar-epss)
- [KEV](#cl-05-glossar-kev)
- [NVD](#cl-05-glossar-nvd)
- [OpenSSF Scorecard](#cl-05-glossar-openssf-scorecard)
- [Secret Store](#cl-05-glossar-secret-store)

### Checkliste / Checklist

#### CL-05-01: SBOM-Format und -Erzeugung / SBOM Format and Generation

- **DE:** SBOM wird automatisiert im Build erzeugt, in CycloneDX oder SPDX
  abgelegt und mit dem Release-Artefakt verbunden.
  Konkrete Werkzeuge je Sprache und Plattform:
  - Sprachunabhängig: `syft` (Anchore) – `syft packages dir:. -o cyclonedx-json`,
    `cdxgen` (OWASP) – Multi-Sprach-CycloneDX-Generator.
  - .NET: `dotnet CycloneDX` – `dotnet CycloneDX --json --output ./sbom`.
  - Java/Maven: `cyclonedx-maven-plugin` – `mvn cyclonedx:makeAggregateBom`.
  - Java/Gradle: `cyclonedx-gradle-plugin` – `./gradlew cyclonedxBom`.
  - Node.js: `@cyclonedx/cdxgen` – `cdxgen -t nodejs -o sbom.json`.
  - Python: `cyclonedx-py` – `cyclonedx-py -p -o sbom.xml`,
    `pip-audit --format=cyclonedx-json`.
  - Go: `cyclonedx-gomod` – `cyclonedx-gomod app -output sbom.xml`.
  - Rust: `cargo-cyclonedx` – `cargo cyclonedx`.
  - Container: `syft <image>:<tag> -o spdx-json`,
    `docker sbom <image>:<tag>` (Docker Desktop).
  Verbindung zum Release-Artefakt:
  - SBOM-Datei mit Release-Tag versionieren: `release/v1.2.3/sbom.cdx.json`.
  - SBOM in OCI-Registry als Attestation anhängen: `cosign attest --type cyclonedx`.
  - SBOM-Hash in Release-Notes oder GitHub Release Asset.
  CI-Integration:
  - GitHub Actions: `anchore/sbom-action` oder `CycloneDX/gh-dotnet-generate-sbom`.
  - Git CI: SBOM-Job mit `syft` und Artifact-Upload.
- **EN:** SBOM is generated automatically in the build, stored in CycloneDX
  or SPDX, and linked to the release artefact.
  Concrete tools per language and platform:
  - Language-independent: `syft` (Anchore) – `syft packages dir:. -o cyclonedx-json`,
    `cdxgen` (OWASP) – multi-language CycloneDX generator.
  - .NET: `dotnet CycloneDX` – `dotnet CycloneDX --json --output ./sbom`.
  - Java/Maven: `cyclonedx-maven-plugin` – `mvn cyclonedx:makeAggregateBom`.
  - Java/Gradle: `cyclonedx-gradle-plugin` – `./gradlew cyclonedxBom`.
  - Node.js: `@cyclonedx/cdxgen` – `cdxgen -t nodejs -o sbom.json`.
  - Python: `cyclonedx-py` – `cyclonedx-py -p -o sbom.xml`,
    `pip-audit --format=cyclonedx-json`.
  - Go: `cyclonedx-gomod` – `cyclonedx-gomod app -output sbom.xml`.
  - Rust: `cargo-cyclonedx` – `cargo cyclonedx`.
  - Container: `syft <image>:<tag> -o spdx-json`,
    `docker sbom <image>:<tag>` (Docker Desktop).
  Linking to the release artefact:
  - Version SBOM file with release tag: `release/v1.2.3/sbom.cdx.json`.
  - Attach SBOM as attestation in OCI registry: `cosign attest --type cyclonedx`.
  - SBOM hash in release notes or GitHub Release asset.
  CI integration:
  - GitHub Actions: `anchore/sbom-action` or `CycloneDX/gh-dotnet-generate-sbom`.
  - Git CI: SBOM job with `syft` and artifact upload.
- **Akzeptanz / Acceptance:** SBOM-Datei (CycloneDX 1.5+ oder SPDX 2.3+) je
  Release vorhanden, im Repo oder als Release-Asset; CI-Job-Log zeigt
  erfolgreiche Erzeugung; SBOM mit Artefakt-Hash verknüpft.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-02: SBOM-Inhalt / SBOM Content

- **DE:** Die SBOM listet direkte und transitive Abhängigkeiten mit Name,
  Version, Lizenz und (wenn möglich) Hash.
  Pflichtfelder pro Komponente:
  - `name` (z. B. `log4j-core`).
  - `version` (z. B. `2.17.1`, exakt, keine Range).
  - `purl` (Package URL, z. B. `pkg:maven/org.apache.logging.log4j/log4j-core@2.17.1`).
  - `cpe` (Common Platform Enumeration, z. B.
    `cpe:2.3:a:apache:log4j:2.17.1:*:*:*:*:*:*:*`).
  - `hashes` (z. B. SHA-256 oder SHA-512 des Artefakts).
  - `licenses` (SPDX-Identifier wie `Apache-2.0`, `MIT`, `GPL-3.0-only`).
  - `supplier` und `originator` für Compliance-Nachweise (CRA, ISO 27036).
  - `dependencies`-Graph für transitive Beziehungen.
  Validierung der Vollständigkeit:
  - Tool `cyclonedx validate --input-file sbom.json` prüft Schema-Konformität.
  - Tool `sbomqs` (Interlynk) bewertet Qualität: `sbomqs score sbom.json`.
  - NTIA Minimum Elements Test: Author, Component Name, Version, Unique
    Identifier, Dependency Relationship, Author of SBOM, Timestamp.
  - Stichprobenprüfung: zufällig 5–10 Komponenten gegen `Pipfile.lock`,
    `package-lock.json` oder `pom.xml` querprüfen.
  Häufige Lücken vermeiden:
  - Native-Bibliotheken (Glibc, OpenSSL) in Container-SBOM ergänzen
    (`syft` mit `--scope all-layers`).
  - Build-Tools und Test-Abhängigkeiten kennzeichnen, aber nicht
    fälschlich als Runtime-Dependencies listen.
- **EN:** The SBOM lists direct and transitive dependencies with name,
  version, licence, and (where possible) hash.
  Mandatory fields per component:
  - `name` (e.g. `log4j-core`).
  - `version` (e.g. `2.17.1`, exact, no ranges).
  - `purl` (Package URL, e.g. `pkg:maven/org.apache.logging.log4j/log4j-core@2.17.1`).
  - `cpe` (Common Platform Enumeration, e.g.
    `cpe:2.3:a:apache:log4j:2.17.1:*:*:*:*:*:*:*`).
  - `hashes` (e.g. SHA-256 or SHA-512 of the artefact).
  - `licenses` (SPDX identifiers like `Apache-2.0`, `MIT`, `GPL-3.0-only`).
  - `supplier` and `originator` for compliance evidence (CRA, ISO 27036).
  - `dependencies` graph for transitive relationships.
  Completeness validation:
  - Tool `cyclonedx validate --input-file sbom.json` checks schema
    conformance.
  - Tool `sbomqs` (Interlynk) scores quality: `sbomqs score sbom.json`.
  - NTIA Minimum Elements test: author, component name, version, unique
    identifier, dependency relationship, SBOM author, timestamp.
  - Sample check: cross-check 5–10 random components against `Pipfile.lock`,
    `package-lock.json`, or `pom.xml`.
  Avoid common gaps:
  - Add native libraries (Glibc, OpenSSL) to container SBOM
    (`syft` with `--scope all-layers`).
  - Mark build tools and test dependencies, but do not list them as runtime
    dependencies by mistake.
- **Akzeptanz / Acceptance:** Stichprobe je Release prüft Vollständigkeit
  (mindestens NTIA Minimum Elements); `sbomqs score >= 7.0`; SBOM enthält
  `purl` oder `cpe` für 100 % der Komponenten.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-03: VEX bei bekannten Schwachstellen / VEX for Known Vulnerabilities

- **DE:** Wenn Komponenten bekannte CVEs haben, wird ein VEX-Dokument (CSAF)
  erzeugt, das die Ausnutzbarkeit erklärt (z. B. „nicht betroffen").
  VEX-Formate und -Werkzeuge:
  - **CSAF 2.0** (OASIS) – maschinenlesbares JSON-Format mit `csaf-validator`.
  - **OpenVEX** – kompaktes JSON-Format von OpenSSF, Tool `vexctl create`.
  - **CycloneDX VEX** – integriert in CycloneDX-Format.
  - **SPDX VEX** – als Annotation im SPDX-Dokument.
  Pflicht-Status-Werte je CVE:
  - `not_affected` (nicht betroffen, mit Begründung).
  - `affected` (betroffen, ohne Mitigation).
  - `fixed` (behoben in Version X).
  - `under_investigation` (in Untersuchung, max. 30 Tage).
  Begründungen für `not_affected` (CSAF `justification`):
  - `component_not_present` (Komponente gar nicht enthalten).
  - `vulnerable_code_not_present` (verwundbarer Code-Pfad nicht enthalten).
  - `vulnerable_code_not_in_execute_path` (Code nicht erreichbar).
  - `vulnerable_code_cannot_be_controlled_by_adversary`.
  - `inline_mitigations_already_exist` (Mitigation vorhanden).
  Beispiel-Workflow:
  - `vexctl create --product "pkg:maven/org.apache/log4j-core@2.17.1"
    --vuln "CVE-2021-44228" --status "not_affected"
    --justification "vulnerable_code_not_in_execute_path"`.
  - VEX-Datei mit SBOM verknüpfen und im Release-Asset hochladen.
  - Bei `affected`: Mitigation oder Workaround in `action_statement`.
  Verknüpfung mit Scanner:
  - Trivy, Grype, Dependency-Track lesen VEX und unterdrücken
    Falsch-Positive automatisch.
- **EN:** When components have known CVEs, a VEX document (CSAF) is created
  that explains exploitability (e.g. "not affected").
  VEX formats and tools:
  - **CSAF 2.0** (OASIS) – machine-readable JSON format with `csaf-validator`.
  - **OpenVEX** – compact JSON format from OpenSSF, tool `vexctl create`.
  - **CycloneDX VEX** – integrated in CycloneDX format.
  - **SPDX VEX** – as annotation in the SPDX document.
  Mandatory status values per CVE:
  - `not_affected` (not affected, with justification).
  - `affected` (affected, no mitigation).
  - `fixed` (fixed in version X).
  - `under_investigation` (under investigation, max. 30 days).
  Justifications for `not_affected` (CSAF `justification`):
  - `component_not_present`.
  - `vulnerable_code_not_present`.
  - `vulnerable_code_not_in_execute_path`.
  - `vulnerable_code_cannot_be_controlled_by_adversary`.
  - `inline_mitigations_already_exist`.
  Example workflow:
  - `vexctl create --product "pkg:maven/org.apache/log4j-core@2.17.1"
    --vuln "CVE-2021-44228" --status "not_affected"
    --justification "vulnerable_code_not_in_execute_path"`.
  - Link the VEX file to the SBOM and upload as release asset.
  - For `affected`: mitigation or workaround in `action_statement`.
  Scanner integration:
  - Trivy, Grype, Dependency-Track read VEX and automatically suppress
    false positives.
- **Akzeptanz / Acceptance:** VEX-Datei (CSAF 2.0 oder OpenVEX) vorhanden,
  alle bekannten CVEs aus dem Scanner-Bericht haben einen Status mit
  Begründung; oder N/A, wenn keine bekannten CVEs vorliegen, mit Verweis
  auf den letzten Scan-Report.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-04: SLSA-Provenance / SLSA Provenance

- **DE:** Für jedes Release liegt eine SLSA-Provenance-Datei vor (Build-System,
  Quelle, Eingaben, Hashes). Das Ziel im SLSA-v1.2-Build-Track ist benannt.
  SLSA-Build-Level und Anforderungen:
  - **Build L1**: Provenance ist vorhanden und beschreibt Artefakt, Quelle und
    Build.
  - **Build L2**: Provenance ist signiert und stammt aus einem gehosteten
    Build-Dienst mit nachvollziehbarer Identität.
  - **Build L3**: Die Build-Plattform ist gehärtet. Sie trennt Builds stärker
    voneinander und erzeugt nicht fälschbare Provenance.
  Werkzeuge zur Provenance-Erzeugung:
  - **GitHub Actions**: `slsa-framework/slsa-github-generator` (offiziell,
    für hohe SLSA-Build-Level geeignet).
  - **Git CI**: `slsa-provenance-action` oder eigene Builder mit `cosign`.
  - **Container**: `cosign attest --type slsaprovenance --predicate ...`.
  - **in-toto**: `in-toto-attestation` für allgemeine Build-Belege.
  Pflichtfelder im Provenance-Statement (in-toto v1):
  - `_type`: `https://in-toto.io/Statement/v1`.
  - `subject[].name`, `subject[].digest.sha256`.
  - `predicateType`: `https://slsa.dev/provenance/v1`.
  - `predicate.buildDefinition.buildType` (z. B.
    `https://github.com/actions/runner/...`).
  - `predicate.runDetails.builder.id` (z. B. URL zum Builder).
  - `predicate.runDetails.metadata.invocationId`.
  Verifikation beim Konsumenten:
  - `cosign verify-attestation --type slsaprovenance --certificate-identity ...`.
  - `slsa-verifier verify-artifact <artefact> --provenance-path <provenance>
    --source-uri <repo>`.
- **EN:** Each release has a SLSA provenance file (build system, source,
  inputs, hashes). The target in the SLSA v1.2 Build track is named.
  SLSA Build levels and requirements:
  - **Build L1**: provenance exists and describes the artefact, source, and
    build.
  - **Build L2**: provenance is signed and comes from a hosted build service
    with a traceable identity.
  - **Build L3**: the build platform is hardened. It separates builds more
    strongly and creates non-forgeable provenance.
  Tools for provenance generation:
  - **GitHub Actions**: `slsa-framework/slsa-github-generator` (official,
    suitable for high SLSA Build levels).
  - **Git CI**: `slsa-provenance-action` or custom builders with `cosign`.
  - **Container**: `cosign attest --type slsaprovenance --predicate ...`.
  - **in-toto**: `in-toto-attestation` for general build evidence.
  Mandatory fields in the provenance statement (in-toto v1):
  - `_type`: `https://in-toto.io/Statement/v1`.
  - `subject[].name`, `subject[].digest.sha256`.
  - `predicateType`: `https://slsa.dev/provenance/v1`.
  - `predicate.buildDefinition.buildType` (e.g.
    `https://github.com/actions/runner/...`).
  - `predicate.runDetails.builder.id` (e.g. URL of the builder).
  - `predicate.runDetails.metadata.invocationId`.
  Verification by the consumer:
  - `cosign verify-attestation --type slsaprovenance --certificate-identity ...`.
  - `slsa-verifier verify-artifact <artefact> --provenance-path <provenance>
    --source-uri <repo>`.
- **Akzeptanz / Acceptance:** Provenance-Datei (in-toto v1 + SLSA Provenance
  v1) je Release-Artefakt; Ziel-Level (z. B. SLSA Build L2 oder Build L3)
  explizit dokumentiert; Konsumenten-Verifikation in Release-Notes oder
  `verify.sh` beigelegt.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-05: Reproduzierbare Builds / Reproducible Builds

- **DE:** Wo möglich werden reproduzierbare Builds angestrebt (gleicher Input
  → gleicher Output). Abweichungen sind dokumentiert.
  Voraussetzungen für Reproducible Builds:
  - Fixierte Toolchain-Versionen (Compiler, Linker, Build-Tools).
  - Deterministische Zeitstempel: `SOURCE_DATE_EPOCH` (Reproducible Builds
    Standard).
  - Fixierte Eingaben: Lock-Dateien, Container-Image-Pinning per Digest
    (`@sha256:...`).
  - Stabile Dateireihenfolge in Archiven (z. B. `tar --sort=name`).
  - Keine eingebetteten Build-Pfade oder Hostnamen.
  Sprach- und Plattform-Werkzeuge:
  - **Java/Maven**: `reproducible-build-maven-plugin`,
    `<project.build.outputTimestamp>2026-01-01T00:00:00Z</project.build.outputTimestamp>`.
  - **Go**: `-trimpath -ldflags="-buildid="` für deterministische Builds.
  - **Rust**: `--remap-path-prefix`, `RUSTFLAGS="-C codegen-units=1"`.
  - **Node.js/npm**: `npm ci` (statt `npm install`) für lockfile-genaue
    Installation.
  - **Container**: `kaniko` oder `buildah` mit `--source-date-epoch`,
    `docker buildx` mit `--provenance=true`.
  Verifikation:
  - Build zweimal in unterschiedlichen Umgebungen ausführen, Hashes vergleichen.
  - Tool `diffoscope` für detaillierten Unterschiedsbericht bei Abweichung.
  - [Reproducible-Builds Test-Suite](https://reproducible-builds.org/tools/).
  Dokumentation von Abweichungen:
  - Falls reproducible nicht erreicht: Begründung im Release-Report
    (z. B. eingebettetes JIT-Cache, externes Time-Stamping).
  - Plan zur Verbesserung mit Zieltermin.
- **EN:** Where possible, reproducible builds are targeted (same input → same
  output). Deviations are documented.
  Prerequisites for reproducible builds:
  - Pinned toolchain versions (compiler, linker, build tools).
  - Deterministic timestamps: `SOURCE_DATE_EPOCH` (Reproducible Builds
    standard).
  - Pinned inputs: lock files, container image pinning by digest
    (`@sha256:...`).
  - Stable file order in archives (e.g. `tar --sort=name`).
  - No embedded build paths or hostnames.
  Language- and platform-specific tools:
  - **Java/Maven**: `reproducible-build-maven-plugin`,
    `<project.build.outputTimestamp>2026-01-01T00:00:00Z</project.build.outputTimestamp>`.
  - **Go**: `-trimpath -ldflags="-buildid="` for deterministic builds.
  - **Rust**: `--remap-path-prefix`, `RUSTFLAGS="-C codegen-units=1"`.
  - **Node.js/npm**: `npm ci` (instead of `npm install`) for lockfile-exact
    installation.
  - **Container**: `kaniko` or `buildah` with `--source-date-epoch`,
    `docker buildx` with `--provenance=true`.
  Verification:
  - Run the build twice in different environments and compare hashes.
  - Tool `diffoscope` for detailed difference report on deviation.
  - [Reproducible Builds test suite](https://reproducible-builds.org/tools/).
  Documenting deviations:
  - If reproducibility cannot be achieved: justification in the release
    report (e.g. embedded JIT cache, external time-stamping).
  - Improvement plan with target date.
- **Akzeptanz / Acceptance:** Reproducible-Build-Status im Release-Bericht
  (`reproducible: yes/partial/no`); bei `partial`/`no` konkrete Abweichungen
  und Verbesserungsplan; mindestens zwei Build-Hashes für Vergleich
  dokumentiert.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-06: Verifizierte Registries / Verified Registries

- **DE:** Abhängigkeiten kommen ausschließlich aus geprüften Registries
  (z. B. interner Proxy, offizielle Registries mit Prüfung).
  Empfohlene interne Proxy-Lösungen:
  - **Sonatype Nexus Repository** – Multi-Sprach-Proxy für Maven, npm, PyPI,
    NuGet, Docker.
  - **JFrog Artifactory** – Enterprise-Repository mit Vulnerability-Scan
    (Xray).
  - **Azure Artifacts** / **GitHub Packages** / **Git Package Registry**
    – Cloud-Optionen mit Zugriffskontrolle.
  - **Verdaccio** (npm) – schlanker self-hosted npm-Proxy.
  Registry-Konfigurationen je Sprache:
  - **npm**: `.npmrc` mit `registry=https://nexus.example.org/repository/npm-proxy/`,
    `always-auth=true`, kein direkter Zugriff auf `registry.npmjs.org`.
  - **Maven**: `settings.xml` mit `<mirrorOf>*</mirrorOf>` auf interner Nexus.
  - **PyPI**: `pip.conf` mit `index-url = https://nexus.example.org/repository/pypi-proxy/simple`.
  - **NuGet**: `nuget.config` mit `<clear />` und nur internem Feed.
  - **Docker**: `~/.docker/config.json` mit `auths` für interne Registry;
    `daemon.json` mit `registry-mirrors`.
  - **Cargo**: `.cargo/config.toml` mit `[source.crates-io] replace-with = "internal"`.
  Sicherheitsmerkmale verifizierter Registries:
  - Signaturprüfung (Maven Central GPG, npm Package Signatures, Sigstore).
  - Vulnerability-Scan beim Eintreten in den Proxy (z. B. Nexus IQ, Xray).
  - Quarantäne neuer Pakete (Cooldown-Zeit von 24–72 h gegen
    Typosquatting/Dependency-Confusion).
  - Audit-Log aller Downloads.
  Schutz gegen Dependency Confusion:
  - Interne Pakete in eigenem Scope (`@internal/*`) und Scope-Routing
    auf privaten Feed.
  - npm: `publishConfig.registry` für interne Pakete.
- **EN:** Dependencies come only from verified registries (e.g. internal
  proxy, official registries with verification).
  Recommended internal proxy solutions:
  - **Sonatype Nexus Repository** – multi-language proxy for Maven, npm,
    PyPI, NuGet, Docker.
  - **JFrog Artifactory** – enterprise repository with vulnerability scan
    (Xray).
  - **Azure Artifacts** / **GitHub Packages** / **Git Package Registry**
    – cloud options with access control.
  - **Verdaccio** (npm) – lightweight self-hosted npm proxy.
  Registry configurations per language:
  - **npm**: `.npmrc` with `registry=https://nexus.example.org/repository/npm-proxy/`,
    `always-auth=true`, no direct access to `registry.npmjs.org`.
  - **Maven**: `settings.xml` with `<mirrorOf>*</mirrorOf>` to internal Nexus.
  - **PyPI**: `pip.conf` with `index-url = https://nexus.example.org/repository/pypi-proxy/simple`.
  - **NuGet**: `nuget.config` with `<clear />` and internal feed only.
  - **Docker**: `~/.docker/config.json` with `auths` for internal registry;
    `daemon.json` with `registry-mirrors`.
  - **Cargo**: `.cargo/config.toml` with `[source.crates-io] replace-with = "internal"`.
  Security features of verified registries:
  - Signature checking (Maven Central GPG, npm Package Signatures, Sigstore).
  - Vulnerability scan on ingestion (e.g. Nexus IQ, Xray).
  - Quarantine of new packages (24–72 h cooldown against
    typosquatting/dependency confusion).
  - Audit log of all downloads.
  Protection against dependency confusion:
  - Internal packages in their own scope (`@internal/*`) with scope routing
    to a private feed.
  - npm: `publishConfig.registry` for internal packages.
- **Akzeptanz / Acceptance:** Registry-Liste in `docs/security/supply-chain-evidence.md`
  mit Proxy-URL, Auth-Methode, Scan-Tool und Quarantäne-Zeit; Konfigurationsdatei
  je Sprache (`.npmrc`, `settings.xml`, `pip.conf`, `nuget.config`) im Repo;
  Pre-Push-Hook prüft, dass keine externen Registries verwendet werden.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-07: Lock-Dateien / Lock Files

- **DE:** Lock- oder Pin-Dateien (z. B. `package-lock.json`, `poetry.lock`,
  `Cargo.lock`, `pom.xml` mit fixierten Versionen) sind im Repository
  eingecheckt.
  Lock-Dateien je Ökosystem:
  - **npm**: `package-lock.json` (npm) oder `yarn.lock` (Yarn) oder
    `pnpm-lock.yaml` (pnpm).
  - **Python**: `poetry.lock` (Poetry), `Pipfile.lock` (pipenv),
    `requirements.txt` mit `pip-compile` (pip-tools, Hash-Pinning),
    `uv.lock` (uv).
  - **Rust**: `Cargo.lock` (für Binaries committen, für Libraries optional).
  - **Go**: `go.sum` (Cryptographic Hashes für `go.mod`-Module).
  - **Java/Maven**: `pom.xml` mit fixierten Versionen, optional
    `dependency-lock-maven-plugin`.
  - **Java/Gradle**: `gradle/dependency-locks/*.lockfile` mit
    `dependencyLocking { lockAllConfigurations() }`.
  - **.NET/NuGet**: `packages.lock.json` mit `<RestorePackagesWithLockFile>true</RestorePackagesWithLockFile>`.
  - **Ruby**: `Gemfile.lock`.
  - **PHP/Composer**: `composer.lock`.
  - **Container**: `Dockerfile` mit `FROM image:tag@sha256:digest`.
  Hash-Pinning (zusätzliche Sicherheit):
  - npm: `package-lock.json` enthält `integrity`-Felder (SHA-512).
  - pip: `pip install --require-hashes -r requirements.txt`.
  - Maven: `<dependencyManagement>` mit `<checksum>` über
    `pgp-maven-plugin`.
  CI-Erzwingung:
  - `npm ci` (statt `npm install`) bricht ab, wenn `package-lock.json` und
    `package.json` divergieren.
  - `pip install --no-deps -r requirements.txt --require-hashes` schlägt
    fehl bei Hash-Mismatch.
  - GitHub Actions Workflow mit `npm ci`/`yarn install --frozen-lockfile`.
  - Pre-Commit-Hook: prüft, dass Lock-Datei vorhanden und nicht aus
    `.gitignore` ausgeschlossen ist.
- **EN:** Lock or pin files (e.g. `package-lock.json`, `poetry.lock`,
  `Cargo.lock`, `pom.xml` with pinned versions) are committed to the
  repository.
  Lock files per ecosystem:
  - **npm**: `package-lock.json` (npm), `yarn.lock` (Yarn), or
    `pnpm-lock.yaml` (pnpm).
  - **Python**: `poetry.lock` (Poetry), `Pipfile.lock` (pipenv),
    `requirements.txt` with `pip-compile` (pip-tools, hash pinning),
    `uv.lock` (uv).
  - **Rust**: `Cargo.lock` (commit for binaries, optional for libraries).
  - **Go**: `go.sum` (cryptographic hashes for `go.mod` modules).
  - **Java/Maven**: `pom.xml` with pinned versions, optionally
    `dependency-lock-maven-plugin`.
  - **Java/Gradle**: `gradle/dependency-locks/*.lockfile` with
    `dependencyLocking { lockAllConfigurations() }`.
  - **.NET/NuGet**: `packages.lock.json` with `<RestorePackagesWithLockFile>true</RestorePackagesWithLockFile>`.
  - **Ruby**: `Gemfile.lock`.
  - **PHP/Composer**: `composer.lock`.
  - **Container**: `Dockerfile` with `FROM image:tag@sha256:digest`.
  Hash pinning (additional security):
  - npm: `package-lock.json` contains `integrity` fields (SHA-512).
  - pip: `pip install --require-hashes -r requirements.txt`.
  - Maven: `<dependencyManagement>` with `<checksum>` via
    `pgp-maven-plugin`.
  CI enforcement:
  - `npm ci` (instead of `npm install`) fails when `package-lock.json` and
    `package.json` diverge.
  - `pip install --no-deps -r requirements.txt --require-hashes` fails on
    hash mismatch.
  - GitHub Actions workflow with `npm ci`/`yarn install --frozen-lockfile`.
  - Pre-commit hook: checks that the lock file exists and is not excluded
    by `.gitignore`.
- **Akzeptanz / Acceptance:** Lock-Datei je Sprache im Repo vorhanden, im
  Build-Prozess strikt verwendet (`npm ci`, `--frozen-lockfile`,
  `--require-hashes`); CI-Job-Log zeigt erfolgreichen Strict-Mode-Install.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-08: Automatisierte Aktualisierungen / Automated Updates

- **DE:** Renovate, Dependabot oder ein gleichwertiges Werkzeug ist aktiv
  und ein Verfahren zur Bewertung der Pull Requests ist beschrieben.
  Verfügbare Werkzeuge:
  - **Dependabot** (GitHub-nativ, kostenlos) – `.github/dependabot.yml`.
  - **Renovate** (Mend, OSS) – `renovate.json` mit feingranularer
    Steuerung (Schedules, Grouping, Auto-Merge).
  - **Git Dependency Scanning** + Auto-Merge-Regeln.
  - **Snyk** – kommerziell, mit CVE-Datenbank-Integration.
  Empfohlene Konfiguration (Dependabot):
  ```yaml
  version: 2
  updates:
    - package-ecosystem: "npm"
      directory: "/"
      schedule:
        interval: "weekly"
      open-pull-requests-limit: 10
      groups:
        prod-deps:
          dependency-type: "production"
        dev-deps:
          dependency-type: "development"
      labels:
        - "dependencies"
        - "security"
  ```
  Empfohlene Konfiguration (Renovate):
  - `extends: ["config:recommended"]` + `:dependencyDashboard`.
  - `schedule: ["before 5am on monday"]` für vorhersehbare Update-Fenster.
  - `automergeType: "branch"` für Patch-Updates mit grünem CI.
  - `vulnerabilityAlerts.enabled: true` für sofortige Sicherheits-PRs.
  Triage-Verfahren:
  - **Patch-Updates** (z. B. `1.2.3 → 1.2.4`): Auto-Merge bei grünem CI.
  - **Minor-Updates** (`1.2.x → 1.3.0`): manueller Review, kurzer
    Smoke-Test.
  - **Major-Updates** (`1.x → 2.0`): geplanter Slot mit Refactoring-Zeit.
  - **Security-Updates**: SLA z. B. 24 h Critical, 7 Tage High,
    30 Tage Medium.
  - PR-Reviewer aus CODEOWNERS, Stakeholder bei kritischen Libraries.
  Schutz vor Supply-Chain-Angriffen:
  - Renovate `prConcurrentLimit: 5` gegen Update-Flut.
  - `minimumReleaseAge: "3 days"` (Cooldown gegen kompromittierte Releases).
  - `gitAuthor: "renovate[bot] <renovate@example.org>"` mit GPG-Signatur.
- **EN:** Renovate, Dependabot, or an equivalent tool is active, and a
  process to triage the pull requests is described.
  Available tools:
  - **Dependabot** (GitHub-native, free) – `.github/dependabot.yml`.
  - **Renovate** (Mend, open source) – `renovate.json` with fine-grained
    control (schedules, grouping, auto-merge).
  - **Git Dependency Scanning** + auto-merge rules.
  - **Snyk** – commercial, with CVE-database integration.
  Recommended configuration (Dependabot):
  ```yaml
  version: 2
  updates:
    - package-ecosystem: "npm"
      directory: "/"
      schedule:
        interval: "weekly"
      open-pull-requests-limit: 10
      groups:
        prod-deps:
          dependency-type: "production"
        dev-deps:
          dependency-type: "development"
      labels:
        - "dependencies"
        - "security"
  ```
  Recommended configuration (Renovate):
  - `extends: ["config:recommended"]` + `:dependencyDashboard`.
  - `schedule: ["before 5am on monday"]` for predictable update windows.
  - `automergeType: "branch"` for patch updates with green CI.
  - `vulnerabilityAlerts.enabled: true` for immediate security PRs.
  Triage process:
  - **Patch updates** (e.g. `1.2.3 → 1.2.4`): auto-merge on green CI.
  - **Minor updates** (`1.2.x → 1.3.0`): manual review, short smoke test.
  - **Major updates** (`1.x → 2.0`): planned slot with refactoring time.
  - **Security updates**: SLA e.g. 24 h critical, 7 days high,
    30 days medium.
  - PR reviewers from CODEOWNERS, stakeholders for critical libraries.
  Protection against supply-chain attacks:
  - Renovate `prConcurrentLimit: 5` against update floods.
  - `minimumReleaseAge: "3 days"` (cooldown against compromised releases).
  - `gitAuthor: "renovate[bot] <renovate@example.org>"` with GPG signature.
- **Akzeptanz / Acceptance:** Konfigurationsdatei (`dependabot.yml` oder
  `renovate.json`) im Repository, dokumentiertes Triage-Verfahren mit
  SLAs für Patch/Minor/Major/Security, Auto-Merge-Regeln nur bei grünem
  CI, Cooldown-Zeit konfiguriert.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-09: CVE-Überwachung / CVE Monitoring

- **DE:** Eine Stelle (z. B. Dependency Track, OSV-Scanner, GitHub Advanced
  Security) überwacht laufend bekannte Schwachstellen der Abhängigkeiten.
  Werkzeuge zur CVE-Überwachung:
  - **OWASP Dependency-Track** (self-hosted) – konsumiert SBOMs, integriert
    NVD/OSV, dashboard-basiert.
  - **OSV-Scanner** (Google) – CLI-Tool, Lockfile-basiert,
    `osv-scanner --lockfile=package-lock.json`.
  - **GitHub Advanced Security / Dependabot Alerts** – integriert in GitHub.
  - **Git Dependency Scanning** – integriert in Git CI.
  - **Trivy** (Aqua Security) – Container, Filesystem, Repos.
  - **Grype** (Anchore) – `grype dir:.` oder `grype <image>`.
  - **Snyk** – kommerziell, sehr gute Sprach- und Container-Abdeckung.
  - **JFrog Xray** – Enterprise-Lösung mit Artifactory-Integration.
  Datenquellen für Schwachstellen:
  - **NVD** (NIST National Vulnerability Database) – offizielle CVE-Quelle.
  - **OSV.dev** (Google) – ökosystemspezifische Vulnerabilities.
  - **GHSA** (GitHub Security Advisories).
  - **CISA KEV** (Known Exploited Vulnerabilities Catalog) – aktiv ausgenutzte CVEs.
  - **EPSS** (Exploit Prediction Scoring System) für Priorisierung.
  Reaktionsfristen (SLA-Beispiel):
  - **Critical (CVSS ≥ 9.0)**: Patch oder Mitigation in 24 h.
  - **High (CVSS 7.0–8.9)**: Patch in 7 Tagen.
  - **Medium (CVSS 4.0–6.9)**: Patch in 30 Tagen.
  - **Low (CVSS < 4.0)**: nächster Release-Zyklus.
  - Bei CISA-KEV-Eintrag: sofortige Eskalation, unabhängig von CVSS.
  CI-Integration:
  - GitHub Actions: `actions/checkout@v4` + `osv-scanner-action`,
    `aquasecurity/trivy-action`.
  - Git CI: `dependency_scanning` Template.
  - Pre-Push-Hook: `osv-scanner` blockiert Push bei `CRITICAL`.
- **EN:** A tool (e.g. Dependency Track, OSV-Scanner, GitHub Advanced
  Security) continuously monitors known dependency vulnerabilities.
  CVE monitoring tools:
  - **OWASP Dependency-Track** (self-hosted) – consumes SBOMs, integrates
    NVD/OSV, dashboard-based.
  - **OSV-Scanner** (Google) – CLI tool, lockfile-based,
    `osv-scanner --lockfile=package-lock.json`.
  - **GitHub Advanced Security / Dependabot Alerts** – integrated in GitHub.
  - **Git Dependency Scanning** – integrated in Git CI.
  - **Trivy** (Aqua Security) – container, filesystem, repos.
  - **Grype** (Anchore) – `grype dir:.` or `grype <image>`.
  - **Snyk** – commercial, very good language and container coverage.
  - **JFrog Xray** – enterprise solution with Artifactory integration.
  Vulnerability data sources:
  - **NVD** (NIST National Vulnerability Database) – official CVE source.
  - **OSV.dev** (Google) – ecosystem-specific vulnerabilities.
  - **GHSA** (GitHub Security Advisories).
  - **CISA KEV** (Known Exploited Vulnerabilities Catalog) – actively exploited CVEs.
  - **EPSS** (Exploit Prediction Scoring System) for prioritisation.
  Response SLAs (example):
  - **Critical (CVSS ≥ 9.0)**: patch or mitigation within 24 h.
  - **High (CVSS 7.0–8.9)**: patch within 7 days.
  - **Medium (CVSS 4.0–6.9)**: patch within 30 days.
  - **Low (CVSS < 4.0)**: next release cycle.
  - On CISA KEV entry: immediate escalation regardless of CVSS.
  CI integration:
  - GitHub Actions: `actions/checkout@v4` + `osv-scanner-action`,
    `aquasecurity/trivy-action`.
  - Git CI: `dependency_scanning` template.
  - Pre-push hook: `osv-scanner` blocks push on `CRITICAL`.
- **Akzeptanz / Acceptance:** Werkzeug benannt und konfiguriert (z. B.
  Dependency-Track URL oder OSV-Scanner-Job), Reaktionsfristen pro CVSS-Stufe
  in `docs/security/dependency-audit.md` dokumentiert; CI-Job-Log zeigt
  laufenden Scan; CISA-KEV-Watchlist aktiv.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-10: OpenSSF Scorecard / OpenSSF Scorecard

- **DE:** Für öffentliche OSS-Repositories oder kritische Abhängigkeiten ist
  der Scorecard-Wert dokumentiert; ein Mindestwert ist als Schwelle gesetzt.
  OpenSSF Scorecard – Werkzeuge:
  - CLI: `scorecard --repo=github.com/owner/repo --format=json`.
  - GitHub Action: `ossf/scorecard-action` mit Wochenlauf, Ergebnis als
    SARIF in Code Scanning.
  - Online: [OpenSSF Scorecard Viewer](https://scorecard.dev/viewer/?uri=github.com/owner/repo).
  - API: `https://api.securityscorecards.dev/projects/github.com/<owner>/<repo>`.
  Scorecard-Checks (Auswahl, jeweils 0–10 Punkte):
  - **Maintained** – aktive Entwicklung in den letzten 90 Tagen.
  - **Code-Review** – Code-Reviews durch zweite Person.
  - **Branch-Protection** – Schutz von `main`/`master`.
  - **Token-Permissions** – minimale `GITHUB_TOKEN`-Rechte in CI.
  - **Vulnerabilities** – keine offenen Vulnerabilities.
  - **Dependency-Update-Tool** – Dependabot/Renovate aktiv.
  - **Pinned-Dependencies** – Actions per SHA gepinnt, nicht per Tag.
  - **SAST** – statische Analyse aktiv (CodeQL, Semgrep).
  - **Signed-Releases** – Releases mit Cosign/GPG signiert.
  - **License** – Lizenz vorhanden und SPDX-konform.
  Empfohlene Schwellwerte:
  - Eigene OSS-Repos: Score ≥ 8.0 (Streben nach 9.0).
  - Kritische Abhängigkeiten: Score ≥ 7.0, sonst Audit oder Ersatz.
  - Optionale Abhängigkeiten: Score ≥ 5.0.
  - Scorecard-Wert in `dependency-audit.md` mit Datum dokumentieren.
  Eskalation bei niedrigem Score:
  - Score < 5.0: Suche nach Alternative oder Fork mit eigener Pflege.
  - Score < 3.0 (kritisch): Eskalation an Security-Team, möglicher Ersatz.
- **EN:** For public OSS repositories or critical dependencies, the
  Scorecard value is documented and a minimum value is set as threshold.
  OpenSSF Scorecard tools:
  - CLI: `scorecard --repo=github.com/owner/repo --format=json`.
  - GitHub Action: `ossf/scorecard-action` with weekly run, result as
    SARIF in code scanning.
  - Online: [OpenSSF Scorecard Viewer](https://scorecard.dev/viewer/?uri=github.com/owner/repo).
  - API: `https://api.securityscorecards.dev/projects/github.com/<owner>/<repo>`.
  Scorecard checks (selection, 0–10 points each):
  - **Maintained** – active development in the last 90 days.
  - **Code-Review** – code reviews by a second person.
  - **Branch-Protection** – protection of `main`/`master`.
  - **Token-Permissions** – minimal `GITHUB_TOKEN` permissions in CI.
  - **Vulnerabilities** – no open vulnerabilities.
  - **Dependency-Update-Tool** – Dependabot/Renovate active.
  - **Pinned-Dependencies** – actions pinned by SHA, not by tag.
  - **SAST** – static analysis active (CodeQL, Semgrep).
  - **Signed-Releases** – releases signed with Cosign/GPG.
  - **License** – present and SPDX-conformant.
  Recommended thresholds:
  - Own OSS repos: score ≥ 8.0 (aim for 9.0).
  - Critical dependencies: score ≥ 7.0, otherwise audit or replace.
  - Optional dependencies: score ≥ 5.0.
  - Document the Scorecard value with date in `dependency-audit.md`.
  Escalation on low score:
  - Score < 5.0: look for an alternative or fork with own maintenance.
  - Score < 3.0 (critical): escalate to security team, possible replacement.
- **Akzeptanz / Acceptance:** Scorecard-Wert (mit Datum) und Mindestschwelle
  in `docs/security/dependency-audit.md` für jede kritische Abhängigkeit;
  GitHub Action `ossf/scorecard-action` für eigene OSS-Repos aktiv;
  Eskalationsverfahren bei Unterschreitung dokumentiert.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-11: Lizenz-Compliance / Licence Compliance

- **DE:** Die SBOM wird auf erlaubte Lizenzen geprüft. Verbotene oder
  unklare Lizenzen werden vor Release ersetzt oder durch Rechtsabstimmung
  freigegeben.
  Werkzeuge zur Lizenzprüfung:
  - **`license-checker`** (npm) – `license-checker --json --out licenses.json`.
  - **`pip-licenses`** (Python) – `pip-licenses --format=json`.
  - **`license-maven-plugin`** – `mvn license:aggregate-third-party-report`.
  - **`cargo-deny`** (Rust) – Lizenz-Allowlist in `deny.toml`.
  - **`dotnet list package --include-transitive`** + Skript für SPDX-Mapping.
  - **`syft`** + **`grype`** – SBOM enthält SPDX-Lizenzen.
  - **FOSSA**, **Black Duck**, **JFrog Xray** – kommerziell.
  Empfohlene Lizenz-Klassifikation:
  - **Erlaubt (Allowlist)** – permissive, kompatibel:
    `MIT`, `Apache-2.0`, `BSD-2-Clause`, `BSD-3-Clause`, `ISC`, `Zlib`,
    `Unlicense`, `CC0-1.0`.
  - **Bedingt erlaubt (Review)** – Copyleft, prüfbar:
    `LGPL-2.1`, `LGPL-3.0`, `MPL-2.0`, `EPL-2.0` (nur dynamisch gelinkt).
  - **Verboten (Denylist)** – starkes Copyleft, kommerziell unverträglich:
    `GPL-2.0`, `GPL-3.0`, `AGPL-3.0`, `SSPL-1.0`, `BUSL-1.1`, `Commons Clause`,
    proprietäre Lizenzen ohne klare Nutzungsrechte.
  - **Unklar** – `UNKNOWN`, fehlende SPDX-ID: zwingend manuell klären.
  Allowlist-Konfiguration (Beispiel `cargo-deny`):
  ```toml
  [licenses]
  unlicensed = "deny"
  allow = ["MIT", "Apache-2.0", "BSD-3-Clause", "ISC", "Unicode-DFS-2016"]
  copyleft = "deny"
  confidence-threshold = 0.93
  ```
  Rechtsabstimmungs-Workflow:
  - Bei Verstoß: Ticket „Legal Review" mit SPDX-ID, Komponentenname,
    Verwendungskontext (statisch/dynamisch gelinkt, modifiziert/unmodifiziert).
  - Freigabe-Dokument vom Rechtsteam mit Geltungsdauer (z. B. ein Jahr).
  - Ablage in `docs/security/license-exceptions.md`.
- **EN:** The SBOM is checked for permitted licences. Forbidden or unclear
  licences are replaced before release or cleared with legal review.
  Licence checking tools:
  - **`license-checker`** (npm) – `license-checker --json --out licenses.json`.
  - **`pip-licenses`** (Python) – `pip-licenses --format=json`.
  - **`license-maven-plugin`** – `mvn license:aggregate-third-party-report`.
  - **`cargo-deny`** (Rust) – licence allowlist in `deny.toml`.
  - **`dotnet list package --include-transitive`** + script for SPDX mapping.
  - **`syft`** + **`grype`** – SBOM contains SPDX licences.
  - **FOSSA**, **Black Duck**, **JFrog Xray** – commercial.
  Recommended licence classification:
  - **Allowed (allowlist)** – permissive, compatible:
    `MIT`, `Apache-2.0`, `BSD-2-Clause`, `BSD-3-Clause`, `ISC`, `Zlib`,
    `Unlicense`, `CC0-1.0`.
  - **Conditional (review)** – copyleft, reviewable:
    `LGPL-2.1`, `LGPL-3.0`, `MPL-2.0`, `EPL-2.0` (dynamic linking only).
  - **Forbidden (denylist)** – strong copyleft, commercially incompatible:
    `GPL-2.0`, `GPL-3.0`, `AGPL-3.0`, `SSPL-1.0`, `BUSL-1.1`, `Commons Clause`,
    proprietary licences without clear usage rights.
  - **Unclear** – `UNKNOWN`, missing SPDX ID: must be clarified manually.
  Allowlist configuration (example `cargo-deny`):
  ```toml
  [licenses]
  unlicensed = "deny"
  allow = ["MIT", "Apache-2.0", "BSD-3-Clause", "ISC", "Unicode-DFS-2016"]
  copyleft = "deny"
  confidence-threshold = 0.93
  ```
  Legal review workflow:
  - On violation: ticket "Legal Review" with SPDX ID, component name,
    usage context (statically/dynamically linked, modified/unmodified).
  - Release document from legal team with validity period (e.g. one year).
  - Storage in `docs/security/license-exceptions.md`.
- **Akzeptanz / Acceptance:** Lizenz-Bericht je Release im SPDX/CycloneDX-Format
  vorhanden, Allowlist/Denylist in `deny.toml` oder gleichwertiger
  Konfiguration; alle Komponenten haben gültige SPDX-IDs; Ausnahmen mit
  Rechtsfreigabe in `docs/security/license-exceptions.md` dokumentiert.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-12: Geheimnisse im Build / Secrets in Build

- **DE:** Build-Pipelines verwenden Secret-Stores; Secrets erscheinen nicht
  in Logs oder Artefakten. Pre-Commit- und CI-Scans laufen.
  Secret-Stores in CI/CD:
  - **GitHub Actions Secrets** + **Environments** mit Approval-Schutz.
  - **Git CI Variables** mit `protected` und `masked` Flags.
  - **Azure Key Vault** + **Azure DevOps Variable Groups**.
  - **HashiCorp Vault** mit OIDC-Auth aus CI heraus.
  - **AWS Secrets Manager** + IAM-Role für Build-Runner.
  - **OIDC-Federation** (statt langlebiger Tokens):
    `permissions: id-token: write` in GitHub Actions.
  Secret-Scanner (Pre-Commit und CI):
  - **gitleaks** – `gitleaks detect --source . --verbose`.
  - **trufflehog** – `trufflehog filesystem . --only-verified`.
  - **detect-secrets** (Yelp) – `detect-secrets scan --baseline .secrets.baseline`.
  - **GitHub Secret Scanning** + **Push Protection** (kostenfrei für Public,
    GitHub Advanced Security für Private Repos).
  - **Git Secret Detection** Template.
  Pre-Commit-Hook (`.pre-commit-config.yaml`):
  ```yaml
  repos:
    - repo: https://github.com/gitleaks/gitleaks
      rev: v8.18.0
      hooks:
        - id: gitleaks
    - repo: https://github.com/Yelp/detect-secrets
      rev: v1.4.0
      hooks:
        - id: detect-secrets
          args: ['--baseline', '.secrets.baseline']
  ```
  Log-Schutz:
  - GitHub Actions maskiert Secrets automatisch in Logs.
  - Eigene Logs: keine Variablen-Dumps (`set` in Bash, `Get-ChildItem env:`
    in PowerShell) ausgeben.
  - `--silent` oder `--no-progress` für Tools, die Tokens in URLs ausgeben.
  Artefakt-Schutz:
  - Container-Layer prüfen: `dive <image>` oder `trivy image --scanners secret`.
  - Build-Artefakte vor Veröffentlichung: `trufflehog filesystem dist/`.
  - `.dockerignore` und `.gitignore` mit `**/secrets/`, `*.pem`, `*.key`,
    `.env*`.
- **EN:** Build pipelines use secret stores; secrets do not appear in logs
  or artefacts. Pre-commit and CI scans are running.
  Secret stores in CI/CD:
  - **GitHub Actions Secrets** + **Environments** with approval protection.
  - **Git CI Variables** with `protected` and `masked` flags.
  - **Azure Key Vault** + **Azure DevOps Variable Groups**.
  - **HashiCorp Vault** with OIDC auth from CI.
  - **AWS Secrets Manager** + IAM role for build runner.
  - **OIDC federation** (instead of long-lived tokens):
    `permissions: id-token: write` in GitHub Actions.
  Secret scanners (pre-commit and CI):
  - **gitleaks** – `gitleaks detect --source . --verbose`.
  - **trufflehog** – `trufflehog filesystem . --only-verified`.
  - **detect-secrets** (Yelp) – `detect-secrets scan --baseline .secrets.baseline`.
  - **GitHub Secret Scanning** + **Push Protection** (free for public,
    GitHub Advanced Security for private repos).
  - **Git Secret Detection** template.
  Pre-commit hook (`.pre-commit-config.yaml`):
  ```yaml
  repos:
    - repo: https://github.com/gitleaks/gitleaks
      rev: v8.18.0
      hooks:
        - id: gitleaks
    - repo: https://github.com/Yelp/detect-secrets
      rev: v1.4.0
      hooks:
        - id: detect-secrets
          args: ['--baseline', '.secrets.baseline']
  ```
  Log protection:
  - GitHub Actions automatically masks secrets in logs.
  - Custom logs: do not dump variables (`set` in bash, `Get-ChildItem env:`
    in PowerShell).
  - `--silent` or `--no-progress` for tools that print tokens in URLs.
  Artefact protection:
  - Inspect container layers: `dive <image>` or `trivy image --scanners secret`.
  - Build artefacts before publishing: `trufflehog filesystem dist/`.
  - `.dockerignore` and `.gitignore` with `**/secrets/`, `*.pem`, `*.key`,
    `.env*`.
- **Akzeptanz / Acceptance:** Secret-Store-Konfiguration (z. B. GitHub
  Environments, Azure Key Vault) dokumentiert; Pre-Commit-Hook mit
  `gitleaks` oder `detect-secrets` aktiv; CI-Scan-Job mit Ergebnis-Bericht
  pro Release; keine Secrets in Logs oder Artefakten (verifiziert mit
  `trufflehog`).
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-05-13: KI-Komponenten in der SBOM / AI Components in the SBOM

- **DE:** Bindet ein release- oder verteilbares Artefakt ein KI-Modell
  oder einen KI-Dienst ein, wird diese KI-Abhängigkeit in der SBOM bzw.
  im Lieferketten-Evidenz-Dokument geführt. CycloneDX bildet dies über
  Komponenten des Typs `machine-learning-model` bzw. `service` ab
  (ML-BOM). Erfasst werden Anbieter, Modell- oder Dienst-Identifikator,
  Version oder Endpunkt sowie ein Verweis auf Model Card oder AI-SBOM
  des Anbieters. Für fremdbezogene Modelle wird keine eigene Modell-
  oder Trainingsdaten-SBOM erzeugt; maßgeblich ist die
  Anbieter-Transparenz (siehe CL_KI-Codeerzeugung, Prüfpunkt 15).
  Bezugsrahmen ist die G7-Leitlinie „Software Bill of Materials for AI –
  Minimum Elements" (2026).
- **EN:** If a release or distributable artefact embeds an AI model or
  AI service, this AI dependency is recorded in the SBOM or the
  supply-chain evidence document. CycloneDX represents this through
  components of type `machine-learning-model` or `service` (ML-BOM).
  Recorded are the provider, model or service identifier, version or
  endpoint, and a link to the provider's model card or AI-SBOM. For
  externally sourced models no own model or training-data SBOM is
  generated; the provider transparency is authoritative (see
  CL_KI-Codeerzeugung, item 15). The reference framework is the G7
  guideline "Software Bill of Materials for AI – Minimum Elements"
  (2026).
- **Akzeptanz / Acceptance:** Enthält das Artefakt eine KI-Abhängigkeit,
  ist sie in der SBOM als KI-Komponente geführt und auf die
  Anbieter-Transparenzquelle verwiesen; zusätzlich ist der Nachweis im
  Lieferketten-Evidenzdokument oder in einem gleichwertigen Spec-Kit-
  Artefakt referenziert; andernfalls „nicht anwendbar" mit Begründung. /
  If the artefact contains an AI dependency, it is recorded in the SBOM as
  an AI component with a link to the provider transparency source; the
  evidence is also referenced in the supply-chain evidence document or in
  an equivalent Spec Kit artefact; otherwise "not applicable" with
  justification.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

### Akzeptanzkriterien gesamt / Overall Acceptance

**DE:** Erfüllt, wenn alle Punkte abgeschlossen sind und SBOM, VEX (oder
N/A), SLSA-Provenance, Lock-Dateien sowie Scorecard-Werte je Release
auffindbar sind. KI-Komponenten und Preset-Nachweise sind im
Lieferketten-Evidenzdokument verlinkt, wenn sie anwendbar sind.

**EN:** Fulfilled when every item is closed and SBOM, VEX (or N/A), SLSA
provenance, lock files, and Scorecard values are findable per release. AI
components and preset evidence are linked in the supply-chain evidence
document where applicable.

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-05-glossar-sbom"></a>

#### SBOM / Software Bill of Materials

- **DE:** Eine SBOM ist eine Stückliste für Software. Sie nennt Komponenten, Versionen und oft Lizenzen, damit Risiken und Schwachstellen schneller gefunden werden.
- **EN:** An SBOM is a bill of materials for software. It lists components, versions, and often licences so that risks and vulnerabilities can be found faster.

<a id="cl-05-glossar-ai-sbom-ml-bom"></a>

#### AI-SBOM / ML-BOM

- **DE:** Eine AI-SBOM oder ML-BOM beschreibt KI-Bestandteile, zum Beispiel Modelle, Datensätze, Frameworks und externe Dienste. Sie macht KI-Lieferketten nachvollziehbar.
- **EN:** An AI-SBOM or ML-BOM describes AI parts, for example models, datasets, frameworks, and external services. It makes AI supply chains traceable.

<a id="cl-05-glossar-cyclonedx"></a>

#### CycloneDX

- **DE:** CycloneDX ist ein Standardformat für SBOMs. Es wird häufig genutzt, um Komponenten, Abhängigkeiten und Sicherheitsinformationen maschinenlesbar zu dokumentieren.
- **EN:** CycloneDX is a standard format for SBOMs. It is often used to document components, dependencies, and security information in a machine-readable way.

<a id="cl-05-glossar-spdx"></a>

#### SPDX

- **DE:** SPDX ist ein Standardformat für SBOMs und Lizenzinformationen. Es hilft, Software-Komponenten maschinenlesbar zu beschreiben.
- **EN:** SPDX is a standard format for SBOMs and licence information. It helps describe software components in a machine-readable way.

<a id="cl-05-glossar-vex"></a>

#### VEX / Vulnerability Exploitability eXchange

- **DE:** VEX erklärt, ob eine bekannte Schwachstelle in einem konkreten Produkt wirklich ausnutzbar ist. Das verhindert unnötige Alarmarbeit.
- **EN:** VEX explains whether a known vulnerability is actually exploitable in a specific product. This prevents unnecessary alert handling.

<a id="cl-05-glossar-csaf"></a>

#### CSAF

- **DE:** CSAF ist ein maschinenlesbares Format für Sicherheitsmeldungen. Es kann Hinweise zu Produkten, Schwachstellen, Bewertungen und Maßnahmen enthalten.
- **EN:** CSAF is a machine-readable format for security advisories. It can contain information on products, vulnerabilities, ratings, and actions.

<a id="cl-05-glossar-slsa"></a>

#### SLSA

- **DE:** SLSA beschreibt Schutzstufen für Software-Lieferketten. Es geht vor allem um nachvollziehbare Builds, Herkunftsnachweise und Schutz gegen Manipulation.
- **EN:** SLSA describes protection levels for software supply chains. It mainly covers traceable builds, provenance evidence, and protection against tampering.

<a id="cl-05-glossar-provenance"></a>

#### Provenance / Herkunftsnachweis

- **DE:** Provenance beschreibt, woher ein Artefakt kommt und wie es gebaut wurde. Dazu gehören Quelle, Build-Umgebung, Zeit und verwendete Abhängigkeiten.
- **EN:** Provenance describes where an artefact comes from and how it was built. This includes source, build environment, time, and used dependencies.

<a id="cl-05-glossar-attestation"></a>

#### Attestation / Bescheinigung

- **DE:** Eine Attestation ist ein signierter Nachweis über eine Aussage, zum Beispiel wer ein Artefakt gebaut hat oder welche Prüfung gelaufen ist.
- **EN:** An attestation is signed evidence for a statement, for example who built an artefact or which check was run.

<a id="cl-05-glossar-sigstore-cosign"></a>

#### Sigstore / Cosign

- **DE:** Sigstore ist ein Werkzeug-Ökosystem für Signaturen und Nachweise. Cosign wird oft genutzt, um Container-Images oder Artefakte zu signieren.
- **EN:** Sigstore is a tool ecosystem for signatures and evidence. Cosign is often used to sign container images or artefacts.

<a id="cl-05-glossar-in-toto"></a>

#### in-toto

- **DE:** in-toto ist ein Rahmenwerk für Lieferketten-Nachweise. Es dokumentiert, welche Schritte ein Artefakt durchlaufen hat und wer dafür verantwortlich war.
- **EN:** in-toto is a framework for supply-chain evidence. It documents which steps an artefact went through and who was responsible.

<a id="cl-05-glossar-cve"></a>

#### CVE

- **DE:** Eine CVE ist eine weltweit eindeutige Kennung für eine bekannte Schwachstelle. Sie hilft, dieselbe Schwachstelle in Tools, Tickets und Meldungen eindeutig zu benennen.
- **EN:** A CVE is a globally unique identifier for a known vulnerability. It helps name the same vulnerability clearly in tools, tickets, and advisories.

<a id="cl-05-glossar-cvss"></a>

#### CVSS

- **DE:** CVSS ist ein Bewertungssystem für die Schwere von Schwachstellen. Es beschreibt technische Eigenschaften und liefert einen Zahlenwert.
- **EN:** CVSS is a rating system for vulnerability severity. It describes technical properties and provides a numeric score.

<a id="cl-05-glossar-epss"></a>

#### EPSS

- **DE:** EPSS schätzt, wie wahrscheinlich eine Schwachstelle bald ausgenutzt wird. Es ergänzt CVSS, weil hohe Schwere nicht immer hohe Ausnutzungswahrscheinlichkeit bedeutet.
- **EN:** EPSS estimates how likely a vulnerability is to be exploited soon. It complements CVSS because high severity does not always mean high exploit likelihood.

<a id="cl-05-glossar-kev"></a>

#### KEV

- **DE:** KEV steht für Known Exploited Vulnerabilities. Der Katalog enthält Schwachstellen, die nachweislich bereits ausgenutzt wurden.
- **EN:** KEV means Known Exploited Vulnerabilities. The catalogue contains vulnerabilities that have already been exploited in practice.

<a id="cl-05-glossar-nvd"></a>

#### NVD

- **DE:** Die NVD ist eine öffentliche Schwachstellendatenbank. Sie enthält Informationen zu CVEs, Bewertungen und betroffenen Produkten.
- **EN:** The NVD is a public vulnerability database. It contains information on CVEs, ratings, and affected products.

<a id="cl-05-glossar-openssf-scorecard"></a>

#### OpenSSF Scorecard

- **DE:** OpenSSF Scorecard ist ein Werkzeug, das öffentliche Repositories auf Sicherheitspraktiken prüft, zum Beispiel Branch-Schutz, Abhängigkeiten und CI-Konfiguration.
- **EN:** OpenSSF Scorecard is a tool that checks public repositories for security practices, for example branch protection, dependencies, and CI configuration.

<a id="cl-05-glossar-secret-store"></a>

#### Secret Store

- **DE:** Ein Secret Store speichert Geheimnisse wie Passwörter, API-Schlüssel oder Tokens geschützt. Geheimnisse sollen nicht im Code, in Logs oder in Tickets stehen.
- **EN:** A secret store protects secrets such as passwords, API keys, or tokens. Secrets should not be in code, logs, or tickets.

### Versionshistorie / Version History

- **Version 1.0 (2026-04-27):** Erstfassung / Initial version
- **Version 1.1 (2026-04-27):** Erweiterte Durchführungshinweise, Quellen-URLs, Statusfelder und Beispiele / Extended guidance, source URLs, status fields, and examples
- **Version 1.2 (2026-04-30):** SLSA v1.2 Build-Track präzisiert / Clarified SLSA v1.2 Build track
- **Version 1.3 (2026-05-19):** Prüfpunkt 13 „KI-Komponenten in der SBOM" ergänzt; Mitgeltende Dokumente um die G7-Leitlinie „Software Bill of Materials for AI – Minimum Elements" (2026) erweitert; synchron mit Richtlinie Sichere Entwicklung v2.4.0. / Added checklist item 13 "AI Components in the SBOM"; extended related documents with the G7 guideline "Software Bill of Materials for AI – Minimum Elements" (2026); synchronized with Richtlinie Sichere Entwicklung v2.4.0.
- **Version 1.4 (2026-06-15):** Prüfpunkt 13 um Lieferketten-Evidenz und gleichwertige Spec-Kit-Preset-Nachweise präzisiert; synchron mit Richtlinie Sichere Entwicklung v2.9.0. / Refined item 13 with supply-chain evidence and equivalent Spec Kit preset evidence; synchronized with Richtlinie Sichere Entwicklung v2.9.0.

- **Version 1.5 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.6 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.

---

- **Version 2.0.0 (2026-07-10):** Einheitliches zweiachsiges Statusmodell, stabile CL-IDs, Lernstufe, Rollen-, Evidenz-, Restrisiko- und Neubewertungsfelder sowie klare Trennung zwischen Vorlage und Projektnachweis eingeführt; synchron mit sichere-Entwicklung-Basis 3.0.0. / Added the unified two-axis status model, stable CL IDs, learning-stage, role, evidence, residual-risk, and re-evaluation fields, plus clear separation between template and project evidence; synchronized with secure-development baseline 3.0.0.
