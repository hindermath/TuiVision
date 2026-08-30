# Tasks: RL-SE- und Checklist-Selbstpruefung

**Input**: Akzeptierte Artefakte in `specs/045-rl-se-checklist-self-review/`
**Branch**: `045-rl-se-checklist-self-review`
**Delivery mode**: `MergeAndSync` nur fuer die spaetere Delivery-Orchestrierung
**Scope**: Audit-Evidence und der geplante test-only MSTest-Validator; keine Produkt-, Governance- oder automatische Folgearbeitsreparatur

## Format und Ausfuehrungsregeln

- Jede Aufgabe verwendet das Format `- [ ] TNNN [USN] Beschreibung`; Setup-, Gate- und Delivery-Aufgaben tragen kein Story-Label.
- Es gibt absichtlich keine `[P]`-Marker. Kanonisches JSON, Markdown-Projektionen, Evidence, Version, Statistik, Security-Index, Intake-Archivierung, Run-State und Closeout sind serialisierte Single-writer-Flaechen.
- Vor jedem einzelnen tatsaechlichen `dotnet build`- oder `dotnet test`-Aufruf steht unmittelbar eine eigene Versions-/Build-Counter-Aufgabe. Ein impliziter Build in `dotnet test` erhaelt keine zweite Erhoehung.
- Jeder erwartete Exitcode, die wesentliche Ausgabe und die Fehlergrenze werden in `specs/045-rl-se-checklist-self-review/pr-evidence.md` oder, nach dem Candidate-Freeze, in der benannten temporaeren Exact-Head-Evidence erfasst. Nicht ausgefuehrte Gates sind `N/A` oder `Not Run`, niemals `Pass`.
- Findings erzeugen weder GitHub-Issues noch neue Intakes, Branches, Features oder Governance-/Produktreparaturen.

**Exakte Kurzpfad-Bindungen**: In den Aufgaben bezeichnet `rl-se-self-review.json` immer `docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/rl-se-self-review.json`; `control-assessment.md`, `preset-assessment.md`, `governance-observations.md`, `human-boundaries.md` und `validation-evidence.md` bezeichnen jeweils die gleichnamige Datei in diesem datierten Verzeichnis. `pr-evidence.md`, `delivery-closeout.md`, `retrospective.md` und `autonomous-run-state.json` bezeichnen jeweils die gleichnamige Datei in `specs/045-rl-se-checklist-self-review/`.

## Phase 1: Evidence-first Setup und Eingangsgates

**Purpose**: Autoritaet, akzeptierte Eingaben, Gate-Vertrag und geschlossene Liefergrenze vor jedem Implementierungsedit beweisen.

**Bindende SHA-256-Basis**:

| Pfad | SHA-256 |
|---|---|
| `requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md` | `62fadb9f571f6c6e5fb81badd103f5ca5087c7219698fdec7be708196d6d6863` |
| `requirements/intakes/series/tui-vision-delivery/intake-review-result.json` | `795f0e781e6526ff9f00b54efaddb5878ce3e4bcc213646aadc15b2ad2dfb5e9` |
| `specs/045-rl-se-checklist-self-review/spec.md` | `726238b81de860075cdce75b24a06cefa1193d9d5c86e8e583a2e8d5cfe908e2` |
| `specs/045-rl-se-checklist-self-review/checklists/requirements.md` | `e2f143ad3d58c46bb6d0cb9633b6f79827ccc93c172e0b72f37a515666070a74` |
| `specs/045-rl-se-checklist-self-review/clarification-report.md` | `ad444310fbdd8527e8896bae25032298a5d27b69d4a78f3aa8497cfc73a99cde` |
| `specs/045-rl-se-checklist-self-review/checklists/audit-readiness.md` | `64e127f89ae8c960a39511911cf8c19963b055e2ea9ac729e3161d3caa7108ce` |
| `specs/045-rl-se-checklist-self-review/plan.md` | `497ef75b29bf7e3bd14d2cd5bef6a23c303aa521cfce927af227348dfd9e116e` |
| `specs/045-rl-se-checklist-self-review/research.md` | `6ca44384cb6df82856b715805eb6c8b6f45d9cb7080acef0c711d55d01a0b032` |
| `specs/045-rl-se-checklist-self-review/data-model.md` | `bc674436968d433ee17af74ab53e08c0903574c3d7e5ea175508c710824abcce` |
| `specs/045-rl-se-checklist-self-review/quickstart.md` | `35e5f59801d9fe94136d40b684b6fd37a8eca08ebc4cb01af3dae1ab88e4dcbe` |
| `specs/045-rl-se-checklist-self-review/contracts/rl-se-self-review-acceptance.md` | `7edcb78285b421b6dcb091d43b17f431bb7116e00768175d35dcc919466f447f` |
| `specs/045-rl-se-checklist-self-review/autonomous-gate-requirements.json` | `8de3e9cc6e69f9886aa5730c084a97f5f358a974fbb5d3389443698bb8c54610` |
| `specs/045-rl-se-checklist-self-review/plan-review.md` | `9c8c291e22d5dafcda27349f8820c0d2144aff47f820c5b8735224e6096ac1ba` |

**Read-only Evidence-Hashes**: Feature 016 `control-assessment.md` = `b311c5b40d09b91cfa688469aaa38d3f8eca89545a7cec83add4a581dbbb5f13`, Feature 016 `pr-evidence.md` = `58ff4736639c8de8deec0b3f0e2995487d68db8d2c4c80bed4ad7e5de6bb3a6c`, Feature 044 `assessment.json` = `221def400d03a84383e7d91d24e178f58c31e6eeeb9e1c29fc3c79043ebfc31d`, Feature 044 `pr-evidence.md` = `ce57b2c41b9c13744aa142f0154947490b9f92950d114aa4c4b78eeb1f227887`.

- [X] T001 Lege `specs/045-rl-se-checklist-self-review/pr-evidence.md` als erste neue Implementierungsdatei mit `Not Run` fuer alle Gates, Run-ID `0290a195-0405-43e1-9b94-64535ea9b386`, `MergeAndSync`-Grenze, Planning-HEAD `6bf24ca6d18f83e0c54e9e00f50aba36fff2739c`, Human-only-Grenzen und nicht zertifizierendem Audit-Statement an.
- [X] T002 Revalidiere Branch, HEAD, Stage, Status, gespeicherten Delivery-Modus und Stop-Zustand read-only mit explizitem Repository-Root durch `validate-intake-series-manifest.sh`, `validate-intake-series-receipt.sh`, `validate-intake-review-result.sh` und `validate-autonomous-run-state.sh`; protokolliere Exitcodes und Fehlerkanaele in `specs/045-rl-se-checklist-self-review/pr-evidence.md`, behandle den gespeicherten Modus ausdruecklich als historische Evidence statt fortdauernder Remote-Berechtigung und aendere `autonomous-run-state.json` nicht manuell.
- [X] T003 Verifiziere `specs/045-rl-se-checklist-self-review/autonomous-gate-requirements.json` gegen die akzeptierte Planprüfung: exakt zwoelf `Applicable`- und vier begruendete `N/A`-Gates, getrennte Vulnerable-/Deprecated-Gates, committed-candidate statt staged Exact-Head, keine Selbstvalidierung und kein PostMerge-Fakt im PreMerge-Vertrag.
- [X] T004 Berechne und vergleiche die vollstaendigen SHA-256-Werte aller in `specs/045-rl-se-checklist-self-review/autonomous-run-state.json` akzeptierten Artefakte sowie des Ready-Reviews; erfasse die exakten Werte und jede Driftentscheidung in `specs/045-rl-se-checklist-self-review/pr-evidence.md`.
- [X] T005 Inventarisiere in `specs/045-rl-se-checklist-self-review/pr-evidence.md` die getrennte geschlossene Primaerkandidaten- und PostMerge-Closeout-Positivliste aus `plan.md`, alle geschuetzten Wurzeln, `.specify/runtime/` als runner-eigene untracked Flaeche, das erst nach Merge erlaubte gepaarte Intake-Rename, die sieben Serienpfade und genau ein transaktionsgebundenes Archivpaar; ein nicht gelisteter Pfad ist ein Hard Stop.
- [X] T006 Extrahiere read-only die 157 eindeutigen `CL-XX-NN`-Ueberschriften aus `docs/secure-development/checklisten/CL_*.md`, beweise null fehlende, doppelte oder unbekannte IDs und binde die exakten Kapitelzahlen `12/13/15/10/13/11/12/13/17/17/12/12` in `specs/045-rl-se-checklist-self-review/pr-evidence.md`.
- [X] T007 Pruefe `docs/security/control-assessment.md` und `specs/016-secure-development-hardening/pr-evidence.md` read-only gegen die Plan-Hashes und erfasse die historische Verteilung `65/13/38/36/5` ausschliesslich als neu zu bewertende Feature-016-Eingangsevidenz in `specs/045-rl-se-checklist-self-review/pr-evidence.md`.
- [X] T008 Pruefe `docs/security/secure-development/2026-08-29-sandbox-applicability/assessment.json` und `specs/044-sandbox-secure-development-hardening/pr-evidence.md` read-only gegen die Plan-Hashes; erfasse `ConditionallyUsable` mit offenen Approval-, Provider-, Egress-, Lifecycle- und Plattformgrenzen, ohne Feature 044 zu veraendern.
- [X] T009 Validiere jeden beabsichtigten neuen untracked Pfad des primaeren PreMerge-Kandidaten aus `plan.md` read-only und ohne Staging mit `validate-autonomous-delivery-set.sh`; erfasse fehlende, kollidierende oder bereits fremd belegte Pfade in `specs/045-rl-se-checklist-self-review/pr-evidence.md` und behandle spaetere kausale Closeout-Pfade noch nicht als entstanden.
- [X] T010 Lasse den Orchestrator den sicheren Evidence-first-Checkpoint fuer `specs/045-rl-se-checklist-self-review/autonomous-run-state.json` erfassen; fuehre keinen manuellen State-Edit und keine semantische Auditbearbeitung vor erfolgreichem T001-T009 aus.

**Checkpoint**: Accepted hashes, Gate-Anforderung, 157er-Quellinventar und geschlossene Pfadmenge sind aktuell; `pr-evidence.md` existiert vor fachlichen oder Test-Edits.

---

## Phase 2: Foundational und repraesentativer Vertikalschnitt

**Purpose**: Parsefaehige, noch nicht akzeptierte Evidence und die vollstaendige test-first Validatoroberflaeche aufbauen; `CL-01-01` muss rot und danach gruen werden, bevor die Breitenarbeit beginnt.

- [X] T011 Erzeuge unter `docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/` exakt `README.md`, `rl-se-self-review.json`, `control-assessment.md`, `preset-assessment.md`, `governance-observations.md`, `human-boundaries.md` und `validation-evidence.md` als DE-first/EN-second Evidence-Skelett ohne bestandenen Audit-, Gate- oder Compliance-Claim.
- [X] T012 Definiere in `docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/rl-se-self-review.json` das kanonische Schema `1.0`, Feature-ID, Snapshot, exakt fuenf Statuswerte, exakt fuenf Prioritaetswerte, alle Pflichtfelder und Relationen aus `data-model.md`; setze `validationState` auf `NotRun`.
- [X] T013 Lege die semantischen, text-first Projektionsgrenzen und Datenhash-Platzhalter als echte `Not Run`-Werte in den sechs Markdown-Dateien neben `rl-se-self-review.json` an; JSON bleibt die einzige Statusquelle.
- [X] T014 Erzeuge `tests/TuiVision.Drivers.Tests/Fixtures/RlSeSelfReview/valid-vertical-slice.json` sowie deterministisch benannte `invalid-*.json`-Fixture-Eingaenge, wobei jede spaetere Negativ-Fixture genau eine Primaerinvariante verletzt und `mustNotWrite=true` traegt.
- [X] T015 Schreibe in `tests/TuiVision.Drivers.Tests/RlSeSelfReviewEvidenceTests.cs` zuerst die MSTest-Oberflaeche `Test_VerticalSliceIsValid`, `Test_ChapterDraftIsValid`, `Test_CompleteAuditIsValid` und `Test_InvalidFixturesFailClosed` mit stabilen `RLSE001` bis `RLSE012`-Erwartungen; verwende nur MSTest und `System.Text.Json`.
- [X] T016 Implementiere in `tests/TuiVision.Drivers.Tests/RlSeSelfReviewEvidenceTests.cs` nur das minimale offline Parser-/Repository-Root-Scaffolding, das die gesamte Testassembly kompiliert und den fehlenden `CL-01-01`-Nachweis semantisch erreichen kann; unbekannte Daten duerfen nicht still akzeptiert werden.
- [X] T017 Ermittle den aktuellen Feature-Branch-Commit-Count als numerischen Patchwert, erhoehe den manuellen Build-Counter genau einmal und schreibe unmittelbar vor T018 die daraus berechnete konkrete Version mit Major 1 und Minor 45 identisch in `Version`, `AssemblyVersion` und `FileVersion` von `Directory.Build.props`; protokolliere den Wert in `specs/045-rl-se-checklist-self-review/pr-evidence.md`.
- [X] T018 Fuehre mit `dotnet build tests/TuiVision.Drivers.Tests/` genau einen Compile-Surface-Aufruf aus; Compiler-, Fixture-Pfad- oder Bestandsfehler blockieren und gelten nicht als erwartetes Red.
- [X] T019 Richte unmittelbar vor T020 die drei Versionsfelder in `Directory.Build.props` erneut mit genau einer Build-Counter-Erhoehung aus.
- [X] T020 Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_VerticalSliceIsValid"` gegen das Evidence-Skelett aus und erfasse den erwarteten nicht-null Red-Nachweis ausschliesslich fuer die fehlende oder unvollstaendige Kontrollzeile mit stabilem `RLSE###`-Fehlercode in `specs/045-rl-se-checklist-self-review/pr-evidence.md`.
- [X] T021 Vervollstaendige `CL-01-01` in `rl-se-self-review.json`, `control-assessment.md` und `valid-vertical-slice.json` mit Quellidentitaet, allen Pflichtfeldern, Freshness, Feature-016-Vergleich, begrenzter Feature-044-Evidence, `security-governance`-Relation und identischer Markdown-Projektion; erweitere den test-only Validator nur fuer diesen bewiesenen Slice.
- [X] T022 Richte unmittelbar vor T023 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T023 Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_VerticalSliceIsValid"` gruen aus und beweise Schema, Statusregel, Evidence-Relation, Preset-Relation, Projektion und Scope-Schutz in `specs/045-rl-se-checklist-self-review/pr-evidence.md`.
- [X] T024 Vervollstaendige in `tests/TuiVision.Drivers.Tests/Fixtures/RlSeSelfReview/` isolierte Negativ-Fixtures fuer falsche Gesamt- und Kapitelzahl, doppelte und unbekannte ID, Status, Prioritaet, Pflichtfeld, schwache positive Evidence, `N/A`-, `Open`- und `FollowUp`-Vertrag, Preset-Unterdeckung, Human-Claim, Governance-Reparaturclaim, privaten/absoluten Pfad, Projektionsdrift und geschuetzten Scope-Pfad.
- [X] T025 Implementiere ausschliesslich in `tests/TuiVision.Drivers.Tests/RlSeSelfReviewEvidenceTests.cs` den geplanten fail-closed MSTest-Validator fuer unbekannte Properties, geschlossene Werte, Kardinalitaeten, Relationen, Freshness, Pfade, Presets, Human-Grenzen, Drift, Projektionen und atomare Ablehnung ohne Schreibzugriff.
- [X] T026 Richte unmittelbar vor T027 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T027 Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_InvalidFixturesFailClosed"` aus und beweise fuer jede Fixture genau den erwarteten Primaercode `RLSE001` bis `RLSE012`, einen nicht schreibenden Fehlerpfad und keine kaskadierende Zweitinvariante.
- [X] T028 Konsolidiere Red-, Green- und Negativnachweis seriell in `docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/validation-evidence.md` und `specs/045-rl-se-checklist-self-review/pr-evidence.md`, ohne `validationState=Passed` fuer den noch unvollstaendigen Gesamtdatensatz zu behaupten.
- [X] T029 Lasse den Orchestrator den sicheren Vertikalschnitt-Checkpoint in `specs/045-rl-se-checklist-self-review/autonomous-run-state.json` erfassen; bei Resume sind State, Authority, Hashes, HEAD, Diff, Scope, Counter und letzter Test erneut zu validieren.

**Checkpoint**: Die gesamte Testoberflaeche kompiliert, `CL-01-01` besitzt echten Red-/Green-Nachweis und alle isolierten Negativ-Fixtures reagieren fail-closed. Erst jetzt darf die 157er-Breitenarbeit beginnen.

---

## Phase 3: User Story 1 - Alle 157 Kontrollen lueckenlos pruefen (Priority: P1)

**Goal**: Jede kanonische Kontrolle besitzt genau eine aktuelle, vollstaendige und nachvollziehbare Entscheidung.

**Independent Test**: Der kumulative Kapitelvalidator findet fuer die jeweils abgeschlossene Grenze nur bekannte eindeutige IDs, exakte Kapitelzahl, vollstaendige Pflichtfelder, statusgerechte Evidence, Feature-016-Vergleich und JSON-/Markdown-Paritaet; nach `CL-12` sind es exakt 157 Zeilen.

### CL-01 - Standards-Anwendbarkeit, 12 Kontrollen

- [X] T030 [US1] Bewerte `CL-01-01` bis `CL-01-12` in `rl-se-self-review.json` und `control-assessment.md` neu; dokumentiere MSL, NIST SSDF, CWE Top 25, ASVS und weitere Standardentscheidungen mit allen Pflichtfeldern und exakt zwoelf eindeutigen Zeilen.
- [X] T031 [US1] Richte unmittelbar vor T032 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T032 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-01` aus und protokolliere die exakte Kapitelzahl 12 in `validation-evidence.md`.

### CL-02 - Sichere Softwarearchitektur, 13 Kontrollen

- [X] T033 [US1] Bewerte `CL-02-01` bis `CL-02-13` in `rl-se-self-review.json` und `control-assessment.md` neu; erfasse Trust Boundaries, STRIDE/CIA, Least Privilege, sichere Defaults, Zero Trust, SAMM, C3A/C5, Risiken und ADR-/Technical-Debt-Bedarf ohne Architektur- oder ADR-Reparatur.
- [X] T034 [US1] Richte unmittelbar vor T035 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T035 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-02` aus und protokolliere die exakte Kapitelzahl 13 sowie die unveraenderte Produkt-/Architekturgrenze in `validation-evidence.md`.

### CL-03 - Krypto-Mindestvorgaben, 15 Kontrollen

- [X] T036 [US1] Bewerte `CL-03-01` bis `CL-03-15` in `rl-se-self-review.json` und `control-assessment.md` neu; trenne vorhandene Krypto-Evidence, Nichtanwendbarkeit, offene Entscheidung und spaetere Arbeitsgrenze mit exakt 15 Zeilen.
- [X] T037 [US1] Richte unmittelbar vor T038 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T038 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-03` aus und protokolliere die exakte Kapitelzahl 15 in `validation-evidence.md`.

### CL-04 - Bedrohungsmodellierung, 10 Kontrollen

- [X] T039 [US1] Bewerte `CL-04-01` bis `CL-04-10` in `rl-se-self-review.json` und `control-assessment.md` neu; pruefe bestehende STRIDE-/CIA-/CAPEC- und Threat-Model-Evidence, waehrend neue Modell- oder Produktgrenzen begruendet `N/A` bleiben.
- [X] T040 [US1] Richte unmittelbar vor T041 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T041 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-04` aus und protokolliere die exakte Kapitelzahl 10 in `validation-evidence.md`.

### CL-05 - Lieferkette und Build-Integritaet, 13 Kontrollen

- [X] T042 [US1] Bewerte `CL-05-01` bis `CL-05-13` in `rl-se-self-review.json` und `control-assessment.md` neu; decke Dependencies, immutable Workflow-Referenzen, Lock/Restore, SBOM, VEX, Provenance/SLSA, Scorecard und Release-Grenzen ohne Paket-, Workflow- oder Release-Edit ab.
- [X] T043 [US1] Richte unmittelbar vor T044 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T044 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-05` aus und protokolliere die exakte Kapitelzahl 13 in `validation-evidence.md`.

### CL-06 - Schwachstellenoffenlegung, 11 Kontrollen

- [X] T045 [US1] Bewerte `CL-06-01` bis `CL-06-11` in `rl-se-self-review.json` und `control-assessment.md` neu; trenne repository-lokale Offenlegungsevidence, RFC-9116-/Provider-/Human-Grenzen und sichere Follow-ups mit exakt elf Zeilen.
- [X] T046 [US1] Richte unmittelbar vor T047 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T047 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-06` aus und protokolliere die exakte Kapitelzahl 11 in `validation-evidence.md`.

### CL-07 - CRA-Anwendbarkeit, 12 Kontrollen

- [X] T048 [US1] Bewerte `CL-07-01` bis `CL-07-12` in `rl-se-self-review.json` und `control-assessment.md` neu; halte rechtliche, Markt-, Organisations- und Produktgrenzen ohne unbefugte CRA- oder Compliance-Freigabe sichtbar.
- [X] T049 [US1] Richte unmittelbar vor T050 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T050 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-07` aus und protokolliere die exakte Kapitelzahl 12 in `validation-evidence.md`.

### CL-08 - Sicherheits-Code-Review, 13 Kontrollen

- [X] T051 [US1] Bewerte `CL-08-01` bis `CL-08-13` in `rl-se-self-review.json` und `control-assessment.md` neu; pruefe C#-MSL, Eingabevalidierung, sichere APIs, Fehlerbehandlung, Serialisierung, Datei-/Prozess-/Terminalgrenzen und Review-Evidence ohne Produktcodeaenderung.
- [X] T052 [US1] Richte unmittelbar vor T053 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T053 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-08` aus und protokolliere die exakte Kapitelzahl 13 in `validation-evidence.md`.

### CL-09 - KI-Codeerzeugung, 17 Kontrollen

- [X] T054 [US1] Bewerte `CL-09-01` bis `CL-09-17` in `rl-se-self-review.json` und `control-assessment.md` neu; trenne Entwicklungs-KI, Prompt-/Log-Redaction, Agentenreview, Modellgrenzen und AI-SBOM-`N/A` von Runtime-/Produkt-KI.
- [X] T055 [US1] Richte unmittelbar vor T056 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T056 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-09` aus und protokolliere die exakte Kapitelzahl 17 in `validation-evidence.md`.

### CL-10 - Sichere Entwicklungsumgebung, 17 Kontrollen

- [X] T057 [US1] Bewerte `CL-10-01` bis `CL-10-17` in `rl-se-self-review.json` und `control-assessment.md` neu; pruefe Toolchain, Mounts, Schreibrechte, Secrets, Netzwerk, Plattform, CI und Feature-044-Evidence mit getrennten Proof-Grenzen.
- [X] T058 [US1] Richte unmittelbar vor T059 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T059 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-10` aus und protokolliere die exakte Kapitelzahl 17 in `validation-evidence.md`.

### CL-11 - Datenschutz-Folgenabschaetzung, 12 Kontrollen

- [X] T060 [US1] Bewerte `CL-11-01` bis `CL-11-12` in `rl-se-self-review.json` und `control-assessment.md` neu; trenne Repository-/Testdaten, produktive Daten, rechtliche Rolle und DPIA-Human-Grenzen mit exakt zwoelf Zeilen.
- [X] T061 [US1] Richte unmittelbar vor T062 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T062 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-11` aus und protokolliere die exakte Kapitelzahl 12 in `validation-evidence.md`.

### CL-12 - Agentische KI-Sandbox, 12 Kontrollen

- [X] T063 [US1] Bewerte `CL-12-01` bis `CL-12-12` in `rl-se-self-review.json` und `control-assessment.md` neu; pruefe Agent-State, Mounts, Secrets, Netzwerk, Toolchain, praktische Ausfuehrung, Plattformnachweis und menschliche Freigabe gegen Feature 044 mit exakt zwoelf Zeilen.
- [X] T064 [US1] Richte unmittelbar vor T065 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T065 [US1] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_ChapterDraftIsValid"` fuer den kumulativen Stand durch `CL-12` aus und beweise insgesamt exakt 157 eindeutige Zeilen, die Kapitelzahlen `12/13/15/10/13/11/12/13/17/17/12/12` und null fehlende, doppelte oder unbekannte IDs in `validation-evidence.md`.

**Checkpoint**: User Story 1 ist als 157/157-Datensatz kapitelweise reviewbar; noch ausstehende Preset-, Drift-, Human- und Leserprojektionen verhindern weiterhin einen voreiligen Gesamtaudit-Pass.

---

## Phase 4: User Story 2 - Governance-Drift sichtbar machen (Priority: P1)

**Goal**: Alle zwoelf Presets und bestaetigte Governance-Abweichungen sind vollstaendig, aktuell, unrepariert und getrennt von den 157 Kontrollzeilen dokumentiert.

**Independent Test**: Preset-Menge und Hashes sind exakt; jede Driftbeobachtung nennt mindestens zwei Quellen und alle Pflichtfelder; `repairPerformed=false`; geschuetzte Governance-Dateien bleiben im Diff unveraendert.

- [X] T066 [US2] Erfasse `security-governance@0.6.2`, `architecture-governance@0.5.2` und `isaqb-architecture-governance@0.2.2` mit Registry-/Artefakthash, Checkpoints, Status, Evidence, Owner, Reviewer, Datum, Follow-up, Prioritaet, Restrisiko und Trigger in `rl-se-self-review.json` und `preset-assessment.md`.
- [X] T067 [US2] Erfasse `a11y-governance@0.4.3`, `cross-platform-governance@0.2.2` und `agent-parity-governance@0.4.2` vollstaendig; begruende die nicht ausgeloeste neue Script-/Cmdlet-/Manpage-Paritaet und den No-Update-Agentenpfad als `N/A`, ohne Maintainer-Oberflaechen zu synchronisieren.
- [X] T068 [US2] Erfasse `model-routing-governance@0.1.4`, `intake-authoring-governance@0.3.1` und `intake-review-governance@0.2.1` vollstaendig und trenne lokale Routing-/Lineage-/Ready-Evidence von Provider- oder Governance-Mutation.
- [X] T069 [US2] Erfasse `intake-sequencing-governance@0.2.3`, `autonomous-run-governance@0.4.1` und `parallel-autonomous-run-governance@0.2.6` vollstaendig; dokumentiere den seriellen Lauf und die parallele Ausfuehrung begruendet als `N/A` mit Trigger.
- [X] T070 [US2] Validiere in `rl-se-self-review.json` und `preset-assessment.md` exakt zwoelf eindeutige Preset-IDs, die festgelegten Versionen, Registry-Manifest- und `preset.yml`-Hashes sowie alle verpflichtenden Felder ohne Auslassung eines Script-, Parallel- oder Remote-Aspekts.
- [X] T071 [US2] Pruefe Manifest 3.1.0, Richtlinie/Sammelband 3.2.0 und die abweichenden Einzelchecklistenfassungen; erfasse den bestaetigten, verworfenen oder praezisierten Baseline-Befund mit mindestens zwei Evidence-Quellen in `rl-se-self-review.json` und `governance-observations.md`.
- [X] T072 [US2] Pruefe `constitution.md` 1.17.0 gegen `.specify/memory/constitution.md` 1.18.1 und erfasse Unterschied, Auswirkung, Owner, Risiko, Aktion und Trigger ohne Edit an beiden Constitution-Flaechen.
- [X] T073 [US2] Pruefe die sechs/sieben/historisch acht genannten Presets gegen die zwoelf Registry-Eintraege und erfasse den Mapping-Drift ohne Edit an Mapping, Registry, Presets oder Agentenregeln.
- [X] T074 [US2] Erfasse Feature-016-Freshness, Feature-044-Sandbox-Grenzen und weitere bestaetigte Governance-Freshness-Befunde getrennt; alte Claims bleiben `ContextOnly` oder `Supporting`, solange keine aktuelle direkte Evidence vorliegt.
- [X] T075 [US2] Erzeuge in `governance-observations.md` die aus JSON projizierte Driftzusammenfassung mit Dispositionen, vollstaendigen Auditfeldern und `repairPerformed=false`; beweise die unveraenderten geschuetzten Governance-, Feature-016- und Feature-044-Pfade im Scope-Ledger.
- [X] T076 [US2] Erfasse in `validation-evidence.md` die Architektur-/iSAQB-/Quellenpolicy-Entscheidung `N/A`, Lizenzgrenze `MultipartNotRepositoryWideMIT`, keine neuen ADRs/Views/Threat-Modelle und den exakten Re-Evaluation-Trigger fuer Produktvertrag, Trust Boundary, historischen Zweck oder neuen Magiblot-Pin.
- [X] T077 [US2] Lasse den Orchestrator den Preset-/Drift-Freeze als sicheren Checkpoint in `autonomous-run-state.json` erfassen; Findings bleiben Dokumentation und starten keine Reparatur, kein Issue und keinen Folge-Intake.

**Checkpoint**: Alle zwoelf Presets und jeder bestaetigte Driftbefund sind reviewbar, waehrend Governance-Quellen unveraendert bleiben.

---

## Phase 5: User Story 3 - Menschliche und agentische Grenzen trennen (Priority: P1)

**Goal**: Rechtliche, organisatorische, Provider-, Secret-, Plattform- und Freigabeentscheidungen bleiben sichtbar von technischer Repository-Evidence getrennt.

**Independent Test**: Jede Human-/External-only-Grenze besitzt Rolle, Status, Evidence oder Luecke, Aktion, Prioritaet, Restrisiko und Trigger; `agentMayClose=false`; kein unbefugter positiver Claim ist vorhanden.

- [X] T078 [US3] Bewerte CRA, NIS2, DORA, EU AI Act, BSI C3A und BSI C5 in `rl-se-self-review.json` und `human-boundaries.md` einzeln; rechtliche, kommerzielle, Cloud- oder Providerentscheidungen ohne befugte publizierbare Evidence bleiben `Open`, `FollowUp` oder faktisch begruendet `N/A`.
- [X] T079 [US3] Erfasse Organisations-, QISMS-, Zertifizierungs-, Audit- und formale Freigabegrenzen mit konkreten menschlichen Rollen und `agentMayClose=false` in `rl-se-self-review.json` und `human-boundaries.md`.
- [X] T080 [US3] Erfasse GitHub-/Provider-, Secret-, Egress-, reale Host-/Sandbox- und Windows-/Linux-/WSL-Plattformgrenzen getrennt von statischer Konfiguration und lokaler praktischer Ausfuehrung.
- [X] T081 [US3] Revalidiere die Feature-044-Entscheidung `ConditionallyUsable` fuer CL-10/CL-12 als begrenzte technische Eingangsevidenz; uebernimm keine formale Freigabe und veraendere weder Feature-044-Evidence noch Sandbox-/Providerkonfiguration.
- [X] T082 [US3] Dokumentiere AI-SBOM als begruendetes `N/A`, weil KI nur Entwicklungswerkzeug ist; nenne Runtime-/Produkt-KI, Modelle, Datensaetze, Inferenz-Infrastruktur oder ausgelieferte KI-Komponenten als konkreten Re-Evaluation-Trigger.
- [X] T083 [US3] Erzeuge in `human-boundaries.md` die aus JSON abgeleitete Kapitel-/Domainzusammenfassung und beweise `unauthorisedHumanClaimCount=0`, vollstaendige Rollen und keine Credentials, produktiven Daten oder privaten Hostdetails.
- [X] T084 [US3] Lasse den Orchestrator den Human-boundary-Freeze als sicheren Checkpoint in `autonomous-run-state.json` erfassen; fehlende Human-/External-Evidence darf den Audit nicht in einen erfundenen Pass umdeuten.

**Checkpoint**: Jede menschliche oder externe Grenze ist sichtbar, sicher publizierbar und nicht durch Agentenautoritaet geschlossen.

---

## Phase 6: User Story 4 - Audit-Ergebnis inklusiv verstehen (Priority: P2)

**Goal**: Der validierte Audit ist German-first/English-second, CEFR-B2-nah, semantisch, text-first und ohne farb-, layout-, bild- oder pointer-only Bedeutung lesbar.

**Independent Test**: JSON-/Markdown-Paritaet, Status-/Risikotext, Fachbegriffe, Links und Leserpfad sind deterministisch; der fokussierte Komplettaudit und alle Negativ-Fixtures bestehen.

- [X] T085 [US4] Vervollstaendige `docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/README.md` als DE-first/EN-second Leserpfad mit nicht zertifizierendem Audit-Statement und Erklaerungen fuer MSL, SSDF, CWE, ASVS, SBOM, VEX, SLSA, SAMM, CAPEC, Zero Trust, C3A/C5 und zentrale Spec-Kit-Begriffe.
- [X] T086 [US4] Finalisiere `control-assessment.md` als exakte text-first Projektion aller 157 IDs, Statuswerte, Prioritaeten, Risiken, Evidence-/Gap-Grenzen, Owner, Follow-ups und Trigger ohne zweite fachliche Wahrheit.
- [X] T087 [US4] Finalisiere `preset-assessment.md`, `governance-observations.md` und `human-boundaries.md` als vollstaendige DE-first/EN-second Projektionen mit ausgeschriebenen Bedeutungen statt Farb-, Symbol- oder Layoutkodierung.
- [X] T088 [US4] Verlinke den datierten Leserpfad beschreibend aus `docs/security/README.md`; aendere keine DocFX-Navigation, Public API oder XML-Dokumentation ausserhalb der im Plan erlaubten Security-Index-Aktualisierung.
- [X] T089 [US4] Leite in `rl-se-self-review.json` die Statuszahlen, Kapitelzahlen, Presetzahl, Driftzahlen, Human-Grenzen, Freshness, positive Claims und historische Feature-016-Vergleichszahlen ausschliesslich aus den Detaildaten ab und setze den atomaren Audit-/Projektionskandidaten auf `validationState=Passed`.
- [X] T090 [US4] Richte unmittelbar vor T091 die drei Versionsfelder in `Directory.Build.props` mit genau einer Build-Counter-Erhoehung aus.
- [X] T091 [US4] Fuehre `dotnet test tests/TuiVision.Drivers.Tests/ --filter "FullyQualifiedName~Test_CompleteAuditIsValid|FullyQualifiedName~Test_InvalidFixturesFailClosed"` aus; beweise 157/157, exakte Kapitelzahlen, exakt fuenf Statuswerte, alle Pflichtfelder, zwoelf Presets, JSON-/Markdown-Paritaet und atomare Negativablehnung.
- [X] T092 [US4] Erfasse den fokussierten Komplettnachweis in `validation-evidence.md` und `pr-evidence.md`; pruefe manuell DE-first/EN-second, ungefaehr CEFR B2, semantische Tabellen/Headings, beschreibende Links, Braille-/Screenreader-/Tastatur-/Textbrowser-Tauglichkeit und gleichwertige Textalternativen; waehle nach dokumentierter ordinaler Regel mindestens je eine Kontrollzeile aus fruehem, mittlerem und spaetem Kapitel und beweise fuer jede mit gestoppter Dauer von hoechstens drei Minuten den Pfad von Quellkontrolle ueber Entscheidung und Evidence bis Owner, Risiko, Follow-up und Trigger.
- [X] T093 [US4] Lasse den Orchestrator den fachlichen Audit-Akzeptanzcheckpoint in `autonomous-run-state.json` erfassen; dieser Pass ist weder Zertifizierung noch formale Freigabe und autorisiert keine Reparatur.

**Checkpoint**: Alle vier User Stories sind fachlich und fokussiert testbar; jetzt folgen nur noch triggerbasierte lokale Delivery-Gates und serialisierte Abschlusswrites.

---

## Phase 7: Lokale Gates, gemeinsame Abschlusswrites und Candidate Freeze

**Purpose**: Alle ausgelösten lokalen Gates einmal proportional ausfuehren, gemeinsame Dateien abschliessen und exakt einen finalen Release-/Coverlet-Gesamtlauf erzeugen.

- [X] T094 Erfasse in `validation-evidence.md` und `pr-evidence.md` die triggerbezogenen Dispositionen: neue Scripts/PowerShell-Help/Manpage/Cmdlet `N/A`, Agenten-/Template-/Constitution-Update `NoUpdateRequired`, Produkt/API/XML/Package/Projekt/Runtime `N/A`, Architektur-/Source-reference-Aenderung `N/A` und formale Human-Approval `N/A`, jeweils mit kurzer Begruendung und Re-Evaluation-Trigger.
- [X] T095 Fuehre `dotnet list TuiVision.sln package --outdated --include-transitive` read-only als Dependency-Currency-Review aus und klassifiziere Ergebnisse in `validation-evidence.md`, ohne Package-, Tool-, Projekt- oder automatische Follow-up-Aenderung.
- [X] T096 Fuehre separat `dotnet list TuiVision.sln package --vulnerable --include-transitive` aus und erfasse Advisory-Freshness, Exitcode und Proof-Grenze in `validation-evidence.md`.
- [X] T097 Fuehre separat `dotnet list TuiVision.sln package --deprecated --include-transitive` aus und erfasse Deprecation-Freshness, Exitcode und Proof-Grenze in `validation-evidence.md`.
- [X] T098 Inventarisiere mit `git grep -n -E` jede `uses:`-Referenz unter `.github/workflows/`, klassifiziere immutable Pins und aktuelle bewegliche Referenzen in `validation-evidence.md` und repariere keinen Workflow.
- [X] T099 Fuehre `dotnet format --verify-no-changes` aus und erfasse das Ergebnis fuer die einzige geplante C#-Aenderung `tests/TuiVision.Drivers.Tests/RlSeSelfReviewEvidenceTests.cs` in `pr-evidence.md`.
- [X] T100 Fuehre `git diff --check`, Marker-/Fence-/Link-/UTF-8-Pruefungen und den Scope-Firewall-Abgleich gegen die geschlossene Positivliste aus; beweise null Deltas an Produkt, Beispielen, API/XML, Dependencies, Projekten, Workflows, Governance-Quellen, historischen Quellen sowie Feature 016/044.
- [X] T101 Fuehre `bash scripts/scan-agent-secrets.sh --fail-on-high "$(git rev-parse --show-toplevel)"` sowie die Validator-Pfadregeln aus und beweise in `validation-evidence.md` null Credentials, Secret-Muster, private absolute Pfade, Agent-State, Sessions, Logs und produktive Daten.
- [X] T102 Aktualisiere `docs/project-statistics.md` mit dem vorhandenen Profil-2-Renderer fuer den abgeschlossenen Implementierungsmeilenstein; verwende die Referenzen 80 Zeilen/Arbeitstag und 125 Thorsten-Solo, aendere weder Statistikmethodik noch Agentenregeln und validiere die gerenderte Schlusssektion.
- [X] T103 Validiere nach dem fachlichen Akzeptanzcheckpoint die vorhandenen gepaarten Bash-/PowerShell-Rename-Vertraege und reserviere konfliktfrei `requirements/intakes/archive/Lastenheft_RL-SE-Checklist-Selbstpruefung.045-rl-se-checklist-self-review.md`; lasse den bindenden aktiven Intake bis zum tatsaechlichen Feature-Merge unveraendert und erzeuge keinen neuen Intake.
- [X] T104 Bereite `pr-evidence.md` fuer PR-Zweck, betroffene Projekte/Dateien, bisherige Testevidence, Config-/API-/XML-Auswirkung `None`, Security-/Architecture-/A11Y-Entscheidungen, alle zwoelf Presets, Human-Grenzen, Gate-Ledger und ausdruecklich keine automatische Folgearbeit vor; der getrackte Abschluss erfolgt in T109 nach den bis dahin ausgefuehrten Gates.
- [X] T105 Fuehre `docfx docfx.json` fuer den finalen Security-Index, die datierte Audit-Evidence und `docs/project-statistics.md` aus; akzeptiere keine unklassifizierten Warnungen oder Fehler und halte generierte `_site/`-/API-YAML-Ausgaben untracked.
- [X] T106 Fuehre in `tests/web-a11y/` `npm run test:docfx` gegen den finalen DocFX-Stand aus und erfasse Playwright-/axe-Ergebnis sowie WCAG-2.2-AA-Proof-Grenze in der nicht mehr zu aendernden lokalen Evidence-Vorbereitung.
- [X] T107 Fuehre einen UTF-8-`lynx -dump` der erzeugten Seite `2026-08-30-rl-se-checklist-self-review/README.html` aus und pruefe German-first/English-second, semantische Lesereihenfolge, Fachbegriffe, Tabellen und vollstaendige textuelle Status-/Risikoaussagen.
- [X] T108 Validiere `coverlet.runsettings` mit `xmllint --noout coverlet.runsettings` und dokumentiere, dass Core, Controls, Serialization, Compatibility und Drivers.Console jeweils mindestens 70 Prozent Line Coverage erreichen muessen; 80 Prozent bleibt das Zieltracking.
- [X] T109 Schliesse vor dem finalen Volltest alle getrackten Candidate-Inhalte, Task-/Evidence-Felder, Security-Index, Statistik, die konfliktfrei reservierte Intake-Archiv-/Serienplanung ohne Rename und den PR-Text ab; lasse den Orchestrator den runner-owned Candidate-Freeze jetzt in `autonomous-run-state.json` erfassen, bestimme einen untracked Logpfad fuer das finale Ergebnis und verbiete weitere getrackte PreMerge-Edits nach T111.
- [X] T110 Ermittle den nach dem Candidate-Commit geltenden Feature-Branch-Commit-Count als numerischen Patchwert, erhoehe unmittelbar vor T111 den manuellen Build-Counter genau einmal und schreibe die daraus berechnete konkrete Version mit Major 1 und Minor 45 identisch in `Version`, `AssemblyVersion` und `FileVersion` von `Directory.Build.props`.
- [X] T111 Fuehre als einzigen finalen Vollsuite-/Coverage-Aufruf `dotnet test TuiVision.sln --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings --logger "console;verbosity=detailed"` aus; der untracked Log muss Vollregression, beide exakt benannten Auditmethoden und mindestens 70 Prozent je Pflichtassembly belegen.
- [X] T112 Verifiziere read-only, dass der in T109 runner-owned erfasste lokale Candidate-Freeze unveraendert ist und seit T111 kein getrackter PreMerge-Edit entstand; jeder Fehlschlag oder spaetere Candidate-Edit springt zu den betroffenen Tasks zurueck und wiederholt Counter sowie alle ausgeloesten Gates.

**Checkpoint**: Der unveraenderliche lokale Kandidat hat alle anwendbaren lokalen Gates bestanden; genau ein finaler Release-/Coverlet-Solutionlauf ersetzt redundante finale Volltest-, Positiv-, Negativ- und Coverage-Wiederholungen.

---

## Phase 8: Intended Delivery, Commit, Push, PR und Exact-Head PreMerge

**Purpose**: Den unveraenderten lokalen Kandidaten als committed und remote geprueften Exact-Head liefern, ohne technische Gates oder Scope-Grenzen zu umgehen.

- [X] T113 Validiere die vollstaendige beabsichtigte primaere PreMerge-Liefermenge nochmals read-only und ohne Staging mit `validate-autonomous-delivery-set.sh`; gleiche jeden Pfad gegen die geschlossene Primaerkandidaten-Positivliste ab, schliesse noch nicht kausal entstandene Archiv-, Serien- und Closeout-Pfade aus und halte `.specify/runtime/` sowie fremde Worktree-Aenderungen ausserhalb des Kandidaten.
- [X] T114 Stage ausschliesslich die beabsichtigten Feature-045-Pfade, fuehre `git diff --cached --check` aus und gleiche staged Pfade gegen `git status --short`, den finalen Test-HEAD und die Positivliste ab; bewahre alle fremden Aenderungen unveraendert.
- [X] T115 Revalidiere unmittelbar vor dem Commit aktuelle ausdrueckliche `PublishPR`- oder `MergeAndSync`-Autorisierung aus dem dann aktuellen Benutzerauftrag; erzeuge nur bei bestaetigter Autoritaet den lokalen Feature-Commit aus dem unveraenderten gruenen Kandidaten, erfasse Commit-ID sowie Candidate-Inventar ausserhalb getrackter PreMerge-Dateien und fuehre danach keine stillen Amendments aus.
- [X] T116 Pruefe den committed Candidate mit `git diff --check "$(git merge-base origin/main HEAD)" HEAD`, `git diff --name-only "$(git merge-base origin/main HEAD)" HEAD` und `git status --short`; klassifiziere Runner-State getrennt und blockiere bei unbekanntem Pfad.
- [X] T117 Revalidiere unmittelbar vor der ersten Remote-Mutation aktuelle ausdrueckliche `PublishPR`- oder `MergeAndSync`-Autorisierung aus dem dann aktuellen Benutzerauftrag; ein gespeicherter Run-State-Modus genuegt nicht. Pushe nur bei bestaetigter Autoritaet exakt den in T116 geprueften Feature-HEAD nach `origin/045-rl-se-checklist-self-review`; fehlende Autoritaet sowie Provider- oder Authentifizierungsverweigerung bleiben externe Blocker und kein technischer Pass.
- [X] T118 Erstelle den Pull Request aus dem vorbereiteten Inhalt in `pr-evidence.md` und dem unveraenderten finalen T111-Lognachweis, ohne einen getrackten Post-Freeze-Edit sowie ohne Repository-/Provider-Einstellungen, Secrets, Branch-Protection oder Reviewregeln zu aendern.
- [X] T119 Beweise unmittelbar nach Push und PR, dass lokaler `HEAD`, Remote-Feature-Branch und PR-Head identisch sind; veraltete Check-Suites oder Reviews duerfen nicht auf den aktuellen Head angerechnet werden.
- [X] T120 Fuehre `gh pr checks` und `gh pr view` fuer den aktuellen PR-Head aus, inventarisiere alle technischen Checks und Review-Threads und trenne Provider-Refusal, fehlende Human Approval und echte technische Fehler.
- [X] T121 Loese jeden umsetzbaren Review- oder Check-Fund nur innerhalb des Audit-/Test-only-Scopes; jede getrackte Korrektur erzeugt einen neuen Commit und wiederholt ab der fruehesten betroffenen Validierung einschliesslich Versions-/Build-Counter-Regeln, finalem Einzel-Volltest und Current-Head-Nachweis.
- [X] T122 Ordne vor PreMerge jedem der zwoelf `Applicable`-Gates in `autonomous-gate-requirements.json` genau einen Primary-Nachweis mit Workflow, Job, Runner/Plattform, zeilenweisem `executedCommand`, Exitcode, Ergebnis und aktuellem Feature-HEAD zu; Supplemental-Nachweise besitzen einen Owner.
- [X] T123 Erzeuge erst nach aktuellen Remote-Checks die temporaere Datei `/private/tmp/045-rl-se-checklist-self-review.premerge-gate-evidence.json` fuer exakt denselben Feature-HEAD und validiere sie ausserhalb ihrer eigenen Gate-Liste mit `validate-autonomous-gate-evidence.sh`; fehlende Zeilen, stale Head, doppelte Primary-Zeilen oder Token-/Runner-Mismatch blockieren.
- [X] T124 Pruefe unmittelbar vor Merge erneut lokalen HEAD, Remote-Branch, PR-Head, aktuelle Checks, offene Reviews, akzeptierte PreMerge-Evidence und aktuelle ausdrueckliche `MergeAndSync`-Autorisierung aus dem dann aktuellen Benutzerauftrag; der gespeicherte Delivery-Modus allein reicht nicht, und die PreMerge-Datei behauptet weder Merge noch PostMerge-Fakten.
- [X] T125 Nutze einen engen Admin-Bypass ausschliesslich unter der ausdruecklich user-autorisierten Bedingung, dass Human Approval die einzige verbleibende Regel ist und alle technischen, Scope-, Security-, Review- und Exact-Head-Gates fuer denselben Head bestanden sind; protokolliere den begrenzten Bypass, niemals einen technischen Gate-Bypass.
- [X] T126 Merge den akzeptierten PR nur unter der in T124 aktuell revalidierten ausdruecklichen `MergeAndSync`-Autorisierung als Merge-Commit und loesche den Remote-Feature-Branch gemaess Policy; fuehre keinen Squash/Rebase-Ersatz, keine Providerkonfiguration und keine zusaetzliche fachliche Aenderung aus.

**Checkpoint**: Der gepruefte Feature-HEAD ist kausal per Merge-Commit integriert; erst jetzt duerfen echte PostMerge-Fakten, Serienuebergang und Default-Branch-Sync erfasst werden.

---

## Phase 9: PostMerge Closeout, Intake-Serie und Retrospektive

**Purpose**: Nur tatsaechlich entstandene PostMerge-Fakten abschliessen, die bestehende Intake-Serie nach Akzeptanz fortschreiben und den Lauf ohne automatische Folgearbeit beenden.

- [X] T127 Synchronisiere nach dem tatsaechlichen Feature-Merge lokalen `main` fast-forward-sicher mit `origin/main`, beweise den Feature-Merge-Commit, geloeschten oder begruendet behaltenen Feature-Branch und zunaechst `HEAD == origin/main`; erzeuge und validiere danach `/private/tmp/045-rl-se-checklist-self-review.postmerge-gate-evidence.json` als kausalen Schema-2.0-PostMerge-Snapshot mit gebundenem normalisiertem PreMerge-Hash, tatsaechlichem Merge-Commit und leerem `changedPaths`.
- [X] T128 Revalidiere unmittelbar vor diesem Governance-Write aktuelle ausdrueckliche `MergeAndSync`-Autoritaet fuer den kausalen PostMerge-Closeout; archiviere erst dann constitution-konform den bindenden aktiven Intake mit dem gepaarten Rename nach `requirements/intakes/archive/Lastenheft_RL-SE-Checklist-Selbstpruefung.045-rl-se-checklist-self-review.md`, fuehre anschliessend den Intake-Series-Uebergang atomar ueber die sieben bestehenden Serienpfade und genau ein vor dem Write gebundenes neues Manifest-/Receipt-Archivpaar aus, validiere Manifest, Receipt, Review und Eligibility und starte den naechsten Intake weder automatisch noch fachlich.
- [X] T129 Erzeuge `specs/045-rl-se-checklist-self-review/delivery-closeout.md`, aktualisiere `docs/project-statistics.md` nach dem tatsaechlichen Feature-Merge mit dem vorhandenen Profil-2-Renderer und fuehre `/speckit.autonomous-retrospective` nach `specs/045-rl-se-checklist-self-review/retrospective.md` aus; lasse ausschliesslich den Orchestrator Tasks und `autonomous-run-state.json` als nicht rekursive Terminalprojektion mit finalen Taskzahlen/-hash, Merge-/Sync-/PostMerge-Ergebnissen und `nextExactAction: N/A` vorbereiten, die erst wirksam wird, wenn der unveraenderte Closeout-Commit `main` erreicht, und schreibe keine eigene spaetere PR-, Head- oder Merge-Identitaet in diese Projektion.
- [X] T130 Validiere die vollstaendige geschlossene Closeout-Liefermenge read-only, stage nur die in `plan.md` erlaubten kausalen PostMerge-Pfade und fuehre alle durch State-, Task-, Serien-, Archiv-, Retrospektive-, Statistik- oder Dokumentationsaenderungen tatsaechlich ausgeloesten Validatoren proportional aus; erstelle danach unter aktuell ausdruecklich revalidierter `PublishPR`- oder `MergeAndSync`-Autorisierung genau einen Evidence-only-Closeout-Commit, pushe einen eigenen Closeout-Branch und eroeffne genau einen Closeout-PR.
- [X] T131 Konvergiere Checks und actionable Review-Threads des Closeout-PR ohne rekursiven Edit seiner eigenen Identitaet; revalidiere unmittelbar vor Merge aktuelle ausdrueckliche `MergeAndSync`-Autorisierung, merge den unveraenderten Closeout-Commit nach Policy, bereinige den Closeout-Branch und synchronisiere lokalen `main` erneut fast-forward-sicher mit `origin/main`.
- [X] T132 Pruefe abschliessend read-only `HEAD == origin/main`, sauberen Worktree bezogen auf beide geschlossenen Liefermengen, alle eindeutigen Task-IDs, Gate-Evidence, Security-Index, Version, Statistik, Intake-Archiv/Serie, wirksamen terminalen Run-State, Closeout und Retrospektive; beweise null Produkt-/Governance-Reparaturen, null GitHub-Issues, null neue Follow-up-Intakes und null gestartete Folgefeatures und fuehre danach keine weitere Mutation aus.

---

## Abhaengigkeiten und Ausfuehrungsreihenfolge

### Phasenabhaengigkeiten

- Phase 1 (T001-T010) ist das verpflichtende Eintrittsgate und geht jedem fachlichen oder Test-Edit voraus.
- Phase 2 (T011-T029) ist der test-first Vertikalschnitt und blockiert die kapitelweise Breitenarbeit.
- Phase 3 (T030-T065) laeuft strikt in Kapitelreihenfolge `CL-01` bis `CL-12`; jede Kapitelpruefung muss gruen sein, bevor das naechste Kapitel beginnt.
- Phase 4 (T066-T077) beginnt erst nach 157/157 und schreibt Preset-/Drift-Evidence seriell.
- Phase 5 (T078-T084) folgt dem Preset-/Drift-Freeze und schliesst keine Human-/External-Grenze unbefugt.
- Phase 6 (T085-T093) erzeugt den inklusiven Gesamtaudit und dessen fokussierten Positiv-/Negativnachweis.
- Phase 7 (T094-T112) ist strikt seriell; T111 ist der einzige finale Release-/Coverlet-Solutionlauf.
- Phase 8 (T113-T126) beginnt erst nach dem lokalen Candidate-Freeze und bindet jede Remote-Aussage an denselben aktuellen Head.
- Phase 9 (T127-T132) beginnt erst nach dem tatsaechlichen Feature-Merge; PostMerge-Fakten werden nie vorweggenommen und genau ein Evidence-only-Closeout-PR liefert die kausalen getrackten Abschlussfakten.

### User-Story-Abhaengigkeiten

- **US1** ist nach dem Foundational Slice eigenstaendig kapitelweise pruefbar und liefert den kanonischen 157er-Datensatz.
- **US2** benoetigt die Kontrollentscheidungen aus US1, bleibt aber als Preset-/Drift-Projektion getrennt reviewbar.
- **US3** benoetigt US1 und US2, damit Human-/External-only-Grenzen weder positive Claims noch Governance-Drift verdecken.
- **US4** projiziert die validierten Ergebnisse aus US1-US3 und ist durch fokussierten Komplettaudit, A11Y-Automation und Lynx text-first unabhaengig abnehmbar.

### Sichere Stop-/Resume-Grenzen

- Sichere Stopps liegen nach T010, T029, jeder gruenen Kapitelpruefung, T077, T084, T093, T112, T123, T129 und T131.
- Ein Resume revalidiert Run-State, Autoritaet, Accepted Hashes, HEAD, Diff, Scope, Build-Counter und den letzten bestandenen Gate-Nachweis.
- `PausedByUser` wird nur bei bewusstem Stop gesetzt; unsicherer in-flight Zustand bleibt `NeedsRevalidation` und wird nie als Pass inferiert.

## Umsetzungsstrategie

1. Evidence-first: `pr-evidence.md`, Hashes, Gates und geschlossene Pfadmenge.
2. MVP-Slice: kompilierbare Testoberflaeche, erwartetes Red, gruener `CL-01-01`-Slice und isolierte Negativ-Fixtures.
3. Inkrement: zwoelf Kapitel in fester Reihenfolge mit separater Versionsausrichtung und fokussiertem Test.
4. Governance: exakt zwoelf Presets, unreparierter Drift und ehrliche Human-Grenzen.
5. Inklusive Projektion: DE-first/EN-second, CEFR B2, text-first und JSON-paritaetisch.
6. Konvergenz: triggerbasierte Gates, ein finaler Release-/Coverlet-Lauf, exakter committed/current Remote-Head und validierte PreMerge-Evidence.
7. Abschluss: Merge-Commit, constitution-konformes PostMerge-Intake-Rename, atomarer Serienuebergang, genau ein triggerproportional validierter Evidence-only-Closeout-PR, sauberer `main`-Sync und Retrospektive ohne automatische Folgearbeit.
