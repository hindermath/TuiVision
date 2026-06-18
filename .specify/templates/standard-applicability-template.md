# Standard-Anwendbarkeit / Standard Applicability Evidence

## Spec-Kit-Laufnachweis / Spec-Kit Run Evidence

- Feature / Spec-ID:
- Spec-Kit-Phase / Spec-Kit phase: [specify / plan / tasks / implement / review / release]
- Branch / Commit / PR:
- Datum des Laufs / Run date:
- Evidenzverantwortliche*r / Evidence owner:
- Reviewende Person / Reviewer:
- Projekt oder System / Project or system:
- Evidenzort / Evidence location:
- Belastbarkeitsnotiz / Assurance note: Diese Datei dokumentiert den konkreten Spec-Kit-Lauf als interne Audit- und Zertifizierungsvorbereitung. Sie ersetzt keine externe Zertifizierung, Rechtsbewertung oder formale Attestierung.

## Standard-Anwendbarkeitsmatrix / Standards Applicability Matrix

| Standard oder Regelwerk / Standard or regulation | Anwendbarkeit / Applicability | Auslöser oder Scope / Trigger or scope | Erforderliche Evidenz / Evidence required | Evidenzpfad / Evidence path | Ergebnis / Result | N/A-Begründung oder offenes Follow-up / N/A rationale or open follow-up |
| --- | --- | --- | --- | --- | --- | --- |
| ISO 27001/27002 Secure-Development-Controls | [Applicable / N/A / Open] | Sichere Architektur, sichere Programmierung, Logging, Lieferanten- oder Release-Arbeit / Secure architecture, secure coding, logging, supplier or release work | Kontrollzugeordnete Review-Evidenz / Control-mapped review evidence | | [OK / Open / N/A] | |
| NIST SSDF SP 800-218 | [Applicable / N/A / Open] | Alle Level-2-Secure-Development-Arbeiten / All Level-2 secure-development work | Practice-/Task-Zuordnung und Umsetzungsevidenz / Practice/task mapping and implementation evidence | | [OK / Open / N/A] | |
| CWE Top 25 | [Applicable / N/A / Open] | Alle Implementierungs- und Review-Arbeiten / All implementation and review work | Relevante CWE-Zuordnung und Mitigationsnotizen / Relevant CWE mapping and mitigation notes | | [OK / Open / N/A] | |
| OWASP ASVS | [Applicable / N/A / Open] | Web-, API- oder Authentifizierungsdienste / Web, API, or authentication-bearing services | ASVS-Level, Requirement-IDs und Prüfergebnis / ASVS level, requirement IDs, and verification result | | [OK / Open / N/A] | |
| SBOM | [Applicable / N/A / Open] | Release-fähige oder auslieferbare Artefakte / Release-capable or distributable artefacts | Maschinenlesbare SBOM und Review-Notiz / Machine-readable SBOM and review note | | [OK / Open / N/A] | |
| AI-SBOM / G7-BSI-Mindestelemente | [Applicable / N/A / Open] | KI-Runtime-/Produktkomponente, KI-Service, Modell, Datensatz oder Inferenz-Infrastruktur / AI runtime/product component, AI service, model, dataset, or inference infrastructure | Sieben-Cluster-AI-SBOM-Evidenz oder Entwicklungswerkzeug-N/A-Begründung / Seven-cluster AI-SBOM evidence or development-tool N/A rationale | | [OK / Open / N/A] | |
| VEX | [Applicable / N/A / Open] | Bekannte Schwachstelle in ausgelieferter oder bewerteter Komponente / Known vulnerability in shipped or evaluated component | Aussage zu betroffen, nicht betroffen, mitigiert oder in Prüfung / Affected, not affected, mitigated, or under investigation statement | | [OK / Open / N/A] | |
| SLSA | [Applicable / N/A / Open] | CI/CD-gebaute oder veröffentlichte Artefakte / CI/CD-built or published artefacts | Provenance, Attestierung und Build-Integritätsnotizen / Provenance, attestation, and build integrity notes | | [OK / Open / N/A] | |
| OpenSSF Scorecard | [Applicable / N/A / Open] | Öffentliches OSS oder wichtige externe Abhängigkeit / Public OSS or high-impact external dependency | Scorecard-Ausgabe und geprüfte Findings / Scorecard output and reviewed findings | | [OK / Open / N/A] | |
| CRA | [Applicable / N/A / Open] | EU-Marktprodukt mit digitalen Elementen, Schwachstellenbehandlung oder Konformitätsscope / EU-market product with digital elements, vulnerability handling, or conformity scope | Anwendbarkeitsentscheidung, technische Dokumentation, SBOM-/Vulnerability-Evidenz / Applicability decision, technical documentation, SBOM/vulnerability evidence | | [OK / Open / N/A] | |
| NIS2 | [Applicable / N/A / Open] | Wesentliche oder wichtige Einrichtung, regulierte Kunden-/Lieferkette, Sektorpflicht / Essential or important entity, regulated customer or supply chain, sector obligation | Risiko-, Incident-, Lieferketten- und Governance-Evidenz / Risk-management, incident, supply-chain, and governance evidence | | [OK / Open / N/A] | |
| EU AI Act | [Applicable / N/A / Open] | KI-Runtime-/Produktkomponente oder reguliertes KI-System / AI runtime/product component or regulated AI system | KI-Klassifikation, Dokumentation/Logging, AI-SBOM-Querverweis / AI classification, documentation/logging, AI-SBOM cross-reference | | [OK / Open / N/A] | |
| DORA | [Applicable / N/A / Open] | Finanzunternehmen, IKT-Drittdienstleister oder Finanzsektor-Abhängigkeit / Financial entity, ICT third-party service, or financial-sector dependency | IKT-Risiko-, Incident-, Drittanbieter- und Audit-Evidenz / ICT risk, incident, third-party, and audit evidence | | [OK / Open / N/A] | |
| BSI C3A / C5-Querverweis | [Applicable / N/A / Open] | Cloud-Service-Auswahl, Betrieb, Provider-Abhängigkeit oder Assurance Review / Cloud service selection, operation, provider dependency, or assurance review | Link auf Architecture-Governance-C3A-/C5-Records / Link architecture-governance C3A/C5 records | | [OK / Open / N/A] | |

## Entscheidungsregeln / Decision Rules

- `Applicable` erfordert Evidenz oder eine Aufgabe, die vor Release oder Übergabe Evidenz erzeugt / `Applicable` requires evidence or a task that produces evidence before release or handover.
- `N/A` erfordert eine kurze Begründung und einen Auslöser für Neubewertung / `N/A` requires a short rationale and a re-evaluation trigger.
- `Open` erfordert Owner, Follow-up und Auslöser oder Datum / `Open` requires an owner, a follow-up, and a trigger or date.
- Lass keine Standards stillschweigend weg, wenn sie im Spec-Kit-Lauf betrachtet wurden / Do not silently omit standards from this matrix when they were considered during the Spec-Kit run.

## Querverweise / Cross-References

- `spec.md`:
- `plan.md`:
- `tasks.md`:
- Sicherheits-Checkliste / Security checklist:
- Dependency-Audit / Dependency audit:
- Supply-Chain-Evidenz / Supply-chain evidence:
- Regulatorische Anwendbarkeit / Regulatory applicability:
- Architecture-C3A-/C5-Record / Architecture C3A/C5 record:
