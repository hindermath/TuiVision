# Feature Specification: Sandbox Secure Development Hardening

**Branch**: `044-sandbox-secure-development-hardening`
**Status**: Accepted for autonomous delivery
**Input**: `requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md`
**Review**: Series review `847bce5c-98b0-4461-b2a7-c1b5bc9d83dc`

## Zweck / Purpose

Feature 044 entscheidet nachvollziehbar, ob und unter welchen Grenzen TuiVision
mit der öffentlichen `absdd-image-sandbox` bearbeitet werden kann. Der Lauf
ordnet die zwölf CL-12-Kontrollen, Mounts, Schreibrechte, Agentenzustände,
Secrets, Toolchains, Netzgrenzen und Nachweise konkreten Verantwortungsorten
zu. Er schafft keine pauschale Freigabe und ändert weder TuiVision-Runtime noch
das Sandbox-Image.

Feature 044 makes an evidence-backed decision on whether and under which
boundaries TuiVision can be worked on with the public `absdd-image-sandbox`.
The run maps all twelve CL-12 controls, mounts, write permissions, agent state,
secrets, toolchains, network boundaries, and evidence to clear ownership. It
does not grant blanket approval and changes neither the TuiVision runtime nor
the sandbox image.

## User Stories

### US1 - Einen sicheren Arbeitsraum auswählen (P1)

Lernende und Maintainer können erkennen, welche TuiVision-Pfade in die Sandbox
gehören, welche Pfade schreibbar sein dürfen und welche Hostdaten ausgeschlossen
bleiben.

**Independent test**: Eine textbasierte Matrix nennt für jeden benötigten Mount
Quelle als portable Rolle, Ziel, Zweck, Zugriffsart, Ausschluss und
Neubewertungsauslöser, ohne private absolute Hostpfade zu speichern.

### US2 - Die Toolchain ehrlich bewerten (P1)

Entwickler*innen können unterscheiden, welche Build-, Test-, Dokumentations-,
SBOM- und Agentenprüfungen statisch nachgewiesen, praktisch ausgeführt oder nur
für CI beziehungsweise einen Host vorgesehen sind.

**Independent test**: Jede relevante Prüfung besitzt genau einen Ort
`Sandbox`, `LocalHost`, `CI`, `NotPermitted` oder `Open`, einen Evidence-Pfad
und eine klare Proof-Grenze.

### US3 - CL-12 vollständig prüfen (P1)

Security-Reviewer erhalten für `CL-12-01` bis `CL-12-12` je genau eine
Anwendbarkeits- und Umsetzungsentscheidung mit Owner, Restrisiko und nächster
sicherer Aktion.

**Independent test**: Der deterministische Validator akzeptiert genau zwölf
eindeutige Kontrollzeilen und lehnt fehlende, doppelte, unbelegte oder
widersprüchliche Zeilen ab.

### US4 - Secrets und Agentenzustand getrennt halten (P1)

Agenten und Lernende erhalten konkrete Regeln, die Tokens, Profile, Sessions,
Caches und private Hostpfade vom versionierten Projekt und von allgemeinen
Projekt-Mounts trennen.

**Independent test**: Die Dokumentation enthält zulässige Speicherrollen,
verbotene Inhalte, Redaction-Grenzen und sichere nächste Aktionen; Secret- und
Repository-Scans bleiben unauffällig.

### US5 - Offene Grenzen ohne falsche Freigabe behandeln (P2)

Maintainer können fehlende Image-, Plattform-, Provider- oder Human-Evidence
als `Open` weiterführen, ohne sie als erfüllt oder als Produktfehler von
TuiVision darzustellen.

**Independent test**: Jede offene Entscheidung nennt Owner, Folgeaktion,
Restrisiko und Neubewertungsauslöser; nur reale, nicht leere Findings dürfen
einen späteren Intake begründen.

## Functional Requirements

- **FR-001**: Der Lauf MUSS die lokale read-only Vergleichsbasis
  `hindermath/absdd-image-sandbox` mit Repository, Default-Branch, exaktem
  Commit und Hashes der entscheidenden Konfigurationsdateien belegen.
- **FR-002**: Der Lauf MUSS für `CL-12-01` bis `CL-12-12` genau eine
  Anwendbarkeit aus `Applicable`, `N/A` oder `Open` und genau einen
  Umsetzungsstatus aus `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled` oder
  `Not Assessed` dokumentieren.
- **FR-003**: Jede CL-12-Zeile MUSS Begründung, Evidence, Owner, Reviewer,
  Prüfdatum, Restrisiko, Folgeaktion und Neubewertungsauslöser enthalten.
- **FR-004**: Die Mount-Matrix MUSS portable Rollen statt privater Hostpfade,
  Containerziel, Zweck, Zugriffsart und ausdrücklich ausgeschlossene Daten
  nennen.
- **FR-005**: Projektcode DARF nur über einen ausdrücklich ausgewählten
  TuiVision-Checkout schreibbar sein. Home-Verzeichnis, Desktop, Downloads,
  fremde Projekte, Credential Stores und allgemeine Agentenzustände DÜRFEN
  nicht Teil dieses Projekt-Mounts sein.
- **FR-006**: Agentensitzungen, Caches und Konfiguration MÜSSEN in getrennten,
  nicht versionierten Volumes oder gleichwertigen lokalen Bereichen liegen.
  Secrets DÜRFEN nicht in Prompt, Log, Screenshot, Evidence oder Projektdatei
  übernommen werden.
- **FR-007**: Die Execution-Matrix MUSS Build, Test, Formatierung, DocFX,
  text-first A11Y, SBOM/Dependency-Prüfung, Secret-Scan und Agent-Parität einem
  belegten Ausführungsort mit Proof-Grenze zuordnen.
- **FR-008**: Statische Konfigurationsprüfung, praktischer Containerlauf und
  plattformspezifische Akzeptanz MÜSSEN getrennte Evidence-Stufen bleiben.
- **FR-009**: Nicht praktisch ausgeführte Image-, Netzwerk-, Provider-,
  Plattform- oder Human-Freigaben MÜSSEN als `Open` oder begründetes `N/A`
  dokumentiert werden und DÜRFEN nicht aus statischer Evidence abgeleitet
  werden.
- **FR-010**: Die Empfehlung MUSS genau eines der Ergebnisse
  `ApprovedWithBoundaries`, `ConditionallyUsable`, `NotApproved` oder
  `NeedsDecision` verwenden und die nächste sichere Aktion nennen.
- **FR-011**: Projektspezifische Security-Evidence MUSS unter
  `docs/security/secure-development/2026-08-29-sandbox-applicability/` liegen;
  Feature-Lauf- und Delivery-Evidence bleibt unter `specs/044-*`.
- **FR-012**: Lern- und Anwenderdokumentation MUSS German-first/English-second,
  ungefähr CEFR-B2, semantisch strukturiert und text-first nutzbar sein.
- **FR-013**: Ein neuer Validator MUSS nur Struktur, Kardinalität, erlaubte
  Werte, portable Pfade und Pflichtfelder prüfen. Er DARF fachliche Wahrheit,
  Image-Freigabe oder Plattformausführung nicht vortäuschen.
- **FR-014**: Wenn ein neuer script-shaped Validator entsteht, MÜSSEN Bash und
  PowerShell, Unix-Manpage, zweisprachige PowerShell-Hilfe, approved
  `Verb-Noun`-Cmdlet und positive wie negative Fixtures gemeinsam geliefert
  werden.
- **FR-015**: Die Lieferung DARF keine Runtime-, öffentliche API-, Dependency-,
  Paket-, Projekt-, Beispiel-, Sandbox-Image- oder externe Repositoryänderung
  enthalten.
- **FR-016**: Nur ein reproduzierbarer, nicht leerer und TuiVision-eigener
  Befund DARF als Folge-Intake vorgeschlagen werden. Der Lauf DARF kein
  Folgefeature starten.

## Governance Applicability

- **Security Governance v0.6.2**: NIST SSDF, CWE Top 25, Secret-Schutz,
  Dependency-Audit, SBOM-Grenzen und Supply-Chain-Evidence sind anwendbar.
  ASVS ist `N/A`, weil kein Web-, API- oder Authentifizierungsvertrag geändert
  wird. VEX, SLSA, OpenSSF Scorecard, AI-SBOM, NIS2, CRA, EU AI Act und DORA
  werden triggerbasiert bewertet; dieses Feature erzeugt keine neue
  Produkt- oder Releasebehauptung.
- **Architecture Governance v0.5.2** und **iSAQB Architecture Governance
  v0.2.2**: Trust Boundaries zwischen Host, Projekt-Mount, Volumes, Netzwerk
  und Container sind anwendbar. STRIDE/CIA/CAPEC und Security-Qualitätsszenarien
  werden auf diese Grenze begrenzt. BSI C3A/C5, Zero Trust und neue S-ADR sind
  `N/A`, solange kein Cloud-, Deployment- oder Produktvertrag geändert wird.
- **A11Y Governance v0.4.3**: Voll anwendbar für zweisprachige,
  CEFR-B2-gerechte, text-first Security- und Betriebsdokumentation.
- **Cross-Platform Governance v0.2.2**: Voll anwendbar, falls der
  deterministische Validator geliefert wird; Bash/PowerShell-Parität,
  Manpage, Cmdlet und gleiche Exitcodes sind dann Pflicht.
- **Agent Parity Governance v0.4.2**: Agentenspeicher und Guidance werden
  geprüft. Maintained agent surfaces ändern sich nur bei einer gemeinsamen
  neuen Regel; `.specify/templates/` bleiben ohne Templateänderung `N/A`.
- **Intake Authoring v0.3.1**, **Intake Review v0.2.1** und **Intake Sequencing
  v0.2.3**: Aktuelle Intake-Herkunft, `Ready`-Review und `Eligible`-Status sind
  anwendbare Start-Evidence. Feature 044 startet oder erfindet keinen Intake.
- **Model Routing v0.1.4**: Die lokale Codex-Zuordnung muss `Aligned` bleiben;
  konkrete Modellnamen und Hostverfügbarkeit werden nicht versioniert.
- **Autonomous Run v0.4.1**: Laufzustand, Authority, Evidence-first,
  Exact-Head-Gates und Merge-/Sync-Abschluss sind anwendbar.
- **Parallel Autonomous Run v0.2.6**: `N/A`, weil genau ein Feature seriell
  bearbeitet wird.
- **Historical source policy**: `N/A`; das Feature ändert oder bewertet kein
  historisch abgeleitetes Produktverhalten. `tv203s/` bleibt read-only.

## Success Criteria

- **SC-001**: Genau zwölf eindeutige Zeilen decken `CL-12-01` bis `CL-12-12`
  mit vollständigen Pflichtfeldern ab.
- **SC-002**: Jede benötigte Mount- und Execution-Rolle besitzt genau eine
  nachvollziehbare Entscheidung; keine versionierte Datei enthält einen
  privaten absoluten Hostpfad oder ein Secret.
- **SC-003**: Positive und negative Validator-Fixtures bestehen mit identischem
  Bash-/PowerShell-Ergebnis; fehlende, doppelte und unzulässige Werte werden
  abgelehnt.
- **SC-004**: Die finale Empfehlung verwendet genau einen erlaubten Status und
  nennt Grenzen, Restrisiko und nächste sichere Aktion.
- **SC-005**: Alle ausgelösten Dokumentations-, Link-, A11Y-, Secret-,
  Supply-Chain-, Agent-Paritäts- und Remote-Gates bestehen auf dem gelieferten
  Head.
- **SC-006**: Der finale Diff enthält keine ausführbare TuiVision-Produktlogik,
  API-, Dependency-, Paket-, Projekt-, Beispiel-, Sandbox-Image- oder externe
  Repositoryänderung.
- **SC-007**: `Open`-Entscheidungen werden nicht als Erfolg gezählt und besitzen
  Owner, Folgeaktion und Neubewertungsauslöser.

## Assumptions

- Die lokale Vergleichskopie der Sandbox ist sauber und entspricht beim
  Preflight `origin/main` auf Commit
  `7adaeac18ca259726468a2fe1d1fd028b895e09c`.
- TuiVision ist ein C#/.NET-Projekt und verwendet damit eine speichersichere
  Primärsprache; sichere APIs, Dateigrenzen und Abhängigkeiten bleiben trotzdem
  prüfpflichtig.
- Ein statischer Nachweis kann Konfiguration und Dokumentation prüfen, aber
  keinen realen Linux-, Windows-/WSL-, Provider- oder Organisationsnachweis
  ersetzen.
- Der letzte ausdrückliche Benutzerauftrag setzt `MergeAndSync` und einen eng
  begrenzten Approval-Bypass als Delivery-Autorität.

## Non-Goals

Keine technische Änderung des Sandbox-Images, kein breites TuiVision-Hardening,
keine Runtime- oder API-Änderung, keine neue Dependency, kein Agentenlogin,
kein Provideraufruf, keine Speicherung lokaler Credentials, keine pauschale
Organisationsfreigabe und kein automatischer Start eines Folgefeatures.
