# Aufgaben: Beispielportfolio-Konformitätsaudit / Tasks: Example Portfolio Conformance Audit

**Eingaben / Inputs**: Akzeptierte Artefakte unter `specs/038-example-portfolio-conformance-audit/`, der hashgebundene Intake `requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md` und die im `autonomous-run-state.json` gebundene Review-/Serienlinie.

**Liefermodus / Delivery mode**: `MergeAndSync` für den ausdrücklich autorisierten Resume. Die Implementierungsaufgaben bereiten den geprüften Delivery-Kandidaten vor; Commit, Push, Pull Request, Review-Konvergenz, Merge, Branchbereinigung und `main`-Synchronisierung erfolgen anschließend als autonome Delivery-Phase. Provider-Administration und der Start eines Folgefeatures bleiben ausgeschlossen. Ein eng begrenzter Admin-Bypass ist nur bei grünen technischen Pflichtgates, null umsetzbaren Threads und ausschließlich offener Human Approval erlaubt.

**Organisation / Organization**: Alle Aufgaben sind dependency-geordnet und seriell. Es gibt bewusst keinen `[P]`-Marker: Der Lauf autorisiert keine Parallel-Autonomie, und kein paralleler Writer darf `example-portfolio-audit.json`, seine Markdown-Projektionen, `pr-evidence.md`, `Directory.Build.props`, `docs/project-statistics.md`, Intake-Ausgaben oder `autonomous-run-state.json` berühren.

**Build-Counter-Regel / Build-counter rule**: Vor jedem einzelnen `dotnet build` oder `dotnet test` steht eine eigene unmittelbar vorhergehende Aufgabe, die `Version`, `AssemblyVersion` und `FileVersion` in `Directory.Build.props` auf `1.38.<aktueller-Branch-Commit-Count>.<inkrementierter-Build>` ausrichtet. Ein implizit bauendes `dotnet test` erhält keinen zweiten Counter-Schritt; Restore, Format, DocFX, NPM und reine Scans erhöhen den Counter nicht.

## Phase 1: Preflight, Autorität und Tasks-/Analyze-Konvergenz / Preflight, authority, and tasks/analyze convergence

**Zweck / Purpose**: Die unveränderliche Eintrittslinie und den read-only Produktscope fail-closed bestätigen, bevor Evidence oder Implementierung geändert wird. / Confirm the immutable entry lineage and read-only product scope fail-closed before evidence or implementation changes.

- [X] T001 Branch `038-example-portfolio-conformance-audit`, Feature-Pfad und aktuelle `MergeAndSync`-Autorität gegen `specs/038-example-portfolio-conformance-audit/autonomous-run-state.json` prüfen; Delivery bleibt auf Feature 038 und die begrenzte Bypass-Grenze beschränkt, Provider-Administration und Folgefeature-Start bleiben ausgeschlossen.
- [X] T002 Run-ID, akzeptierte Artefakte, `balanced-v1`/`fail-closed`-Routing und alle vorhandenen Routing-Phasen in `specs/038-example-portfolio-conformance-audit/autonomous-run-state.json` auf unveränderte Metadaten prüfen; Drift setzt `NeedsRevalidation` und stoppt.
- [X] T003 Die vier akzeptierten SHA-256-Werte gegen Intake, `Ready`-Review, `Eligible`-Serienmanifest und `Ready`-Receipt unter `requirements/intakes/` prüfen und das Ergebnis später in `specs/038-example-portfolio-conformance-audit/pr-evidence.md` referenzierbar festhalten.
- [X] T004 Feature-037-Evidence auf Wave 6 `Closed`, Portfolioaudit `Eligible`, null Candidate Findings und null Product Decisions sowie die akzeptierten Dataset-/Closeout-Hashes prüfen; jede Abweichung stoppt vor Schreibzugriffen.
- [X] T005 Die direkte Menge `examples/*/*.csproj` ordinal ermitteln, exakt 37 Pfade und SHA-256 `cb2f6568b70f2a62cd529250777e849dd2cd026c05732df81733b2fc3d177333` verlangen und die zwei Projekte unter `examples/Shared/` als Nicht-Portfolioassemblies ausschließen.
- [X] T006 Die abgeschlossenen Checklisten `requirements.md`, `portfolio-audit.md`, `plan-quality.md` und `plan-review.md` erneut auf vollständige Haken, `PASS`, 46 eindeutige EPA-Codes und null offenen implementierungswirksamen Fund prüfen.
- [X] T007 Einen exakten Write-Allowlist-/Protected-Root-Baselinevergleich für Feature-038-Artefakte, den einen test-only Validator samt Fixtures, bedingte Intake-Ausgaben, `Directory.Build.props` und `docs/project-statistics.md` erstellen; `src/`, `examples/`, `tv203s/`, `TVDEMOS/`, `TVFM/`, Public API, Projekt-/Paketdateien, Dependencies, externe Checkouts und generierte Ausgaben als geschützt festhalten.
- [X] T008 `specify check` ausführen und nur lokale Version-/Preset-Evidence erfassen; ein Konfigurations-, Preset- oder Feature-Identitätsdrift stoppt.
- [X] T009 `.specify/scripts/bash/check-prerequisites.sh --json` ausführen und exakt `specs/038-example-portfolio-conformance-audit` sowie `research.md`, `data-model.md`, `contracts/` und `quickstart.md` als verfügbare Eingaben verlangen.
- [X] T010 `specs/038-example-portfolio-conformance-audit/autonomous-gate-requirements.json` auf Schema `1.0`, lückenlose `GATE-038-01` bis `GATE-038-11`, Required-Scope-, Command-Token-, Runner-, Rationale- und Triggerfelder prüfen.
- [X] T011 `GATE-038-10` Remote Exact-Head und `GATE-038-11` Merge/Closeout als `Applicable` bestätigen; ihre Provider-Evidence darf erst für den exakten geprüften Delivery-Head erzeugt und nie aus lokalen Ergebnissen abgeleitet werden.
- [X] T012 Diese `tasks.md` auf lückenlose IDs, vollständige FR-/CR-/SC-/IAC-Zuordnung, alle 37 EX-Zeilen, alle 46 EPA-Fixtures, alle zwölf Presets und die Single-Writer-Regel prüfen.
- [X] T013 Nach abgeschlossener Tasks-Phase `/speckit.analyze` über `spec.md`, `plan.md`, `research.md`, `data-model.md`, Verträge, Checklisten und `tasks.md` ausführen; Critical, High und undisponierte Medium Findings blockieren Implementierung.
- [X] T014 Ausschließlich task-/planbezogene Analyze-Funde ohne Scopeausweitung beheben, Analyze bis zur Konvergenz wiederholen und erst danach den State seriell auf die nächste Implementierungsgrenze setzen und mit Bash- sowie PowerShell-Statevalidator prüfen; abgeschlossene Routing-Phasen bleiben byte-inhaltlich erhalten, nur die neu gestartete aktive Analyze-Wiederholungsphase darf ihre eigenen Metadaten und Ergebnisbindung fortschreiben.

**Checkpoint**: Hashlinie, Portfolio-Baseline, Autorität, Gates, Tasks und Analyze sind konvergent; noch existiert kein Validator- oder Produktedit. / Lineage, population, authority, gates, tasks, and Analyze converge; no validator or product edit exists yet.

## Phase 2: Evidence-Foundation vor Implementierung / Evidence foundation before implementation

**Zweck / Purpose**: Alle kanonischen und reviewbaren Evidence-Flächen ehrlich als `NotAssessed` anlegen, bevor der erste Test-/Validator-Edit erfolgt. / Create every canonical and reviewable evidence surface honestly as `NotAssessed` before the first test/validator edit.

- [X] T015 `specs/038-example-portfolio-conformance-audit/pr-evidence.md` als DE-first/EN-second Evidence anlegen und Authority, Baseline, akzeptierte Hashes, Protected Roots sowie lokale und Remote-Gate-Platzhalter ohne vorzeitigen Erfolgsclaim dokumentieren.
- [X] T016 `specs/038-example-portfolio-conformance-audit/example-portfolio-audit.json` als UTF-8-/Unicode-NFC-, schema-versioniertes, ordinal sortiertes und parsefähiges, aber ausdrücklich nicht abnahmegültiges `NotAssessed`-Skelett anlegen.
- [X] T017 `specs/038-example-portfolio-conformance-audit/example-portfolio-source-manifest.md` als vollständige text-first Projektion mit Source-ID-, Authority-, Pin-/Hash-, No-Copy- und Rückrelationsspalten anlegen.
- [X] T018 `specs/038-example-portfolio-conformance-audit/example-portfolio-inventory.md` mit dem expliziten `EX001`–`EX037`-Vertrag, 25/10/1/1-Aufteilung und `A11yFramework=SupplementalControl` als noch nicht bewertete Projektion anlegen.
- [X] T019 `specs/038-example-portfolio-conformance-audit/example-conformance-matrix.md` mit allen FR-005-Feldern, zehn getrennten Dimensionsentscheidungen, Frameworkentscheidung, Disposition, Review und Restrisiko als `NotAssessed`-Sicht anlegen.
- [X] T020 `specs/038-example-portfolio-conformance-audit/example-framework-usage-review.md` mit den vier Frameworkentscheidungen, Reuse-Grenze, lokaler Sonderlogik und Finding-/ProductDecision-Stopps anlegen.
- [X] T021 `specs/038-example-portfolio-conformance-audit/example-proof-and-platform-review.md` mit App-Loop-/State-/View-/Cell-, Helper-, Negativ-/Fallback-, Terminal- und Plattformspalten anlegen.
- [X] T022 `specs/038-example-portfolio-conformance-audit/example-learning-a11y-review.md` mit Guide-, Lernziel-, DE/EN-, CEFR-B2-, WCAG-2.2-AA-, Tastatur-, Screenreader-, Braille-, Textbrowser-, High-Contrast- und Small-Terminal-Spalten anlegen.
- [X] T023 `specs/038-example-portfolio-conformance-audit/example-portfolio-findings.md` als ehrliches, noch nicht eingefrorenes Finding-Ledger mit vollständigem FR-027-Vertrag und ohne vorweggenommene `EF`-IDs anlegen.
- [X] T024 `specs/038-example-portfolio-conformance-audit/example-remediation-handoff.md` mit vier noch nicht bewerteten Owner-Gruppen, DAG-/Topologievertrag und genau einem geplanten, unnummerierten unabhängigen Closure anlegen, ohne Intake zu starten.
- [X] T025 `specs/038-example-portfolio-conformance-audit/example-portfolio-gate.md` mit `GATE-038-01`–`GATE-038-11`, getrennten Applicability-/Implementation-Feldern und ehrlichem `Not Assessed` anlegen.
- [X] T026 Im kanonischen Dataset und Quellenmanifest die vier Intake-Hashes, Feature-037-Hashes, akzeptierten Feature-024/029/030-Pins, 37-Pfadmengenhash sowie deterministische Regeln für `TV203-E`, `TVDEMOS-E`, `TVFM-E`, `BASE-E` und `EVD001+` erfassen.
- [X] T027 In Dataset, Gate und `pr-evidence.md` alle zwölf Presets und benannten Standards mit `Not Assessed`, Rationale, EvidencePath, Owner, Reviewer, ResidualRisk, ReevaluationTrigger und FollowUp vorstrukturieren; kein `N/A` als umgesetzt markieren.
- [X] T028 Die neun fachlichen Markdown-Projektionen plus `pr-evidence.md` als exakt zehn Markdown-Evidence-Familien gegen die eine JSON-Wahrheitsquelle, alle relativen Links und den Evidence-before-code-Zeitpunkt prüfen und bestätigen, dass noch kein Build-, Test-, Remote- oder Konformitätserfolg vorweggenommen wird.

**Checkpoint**: Kanonisches Skelett, neun fachliche Markdown-Projektionen und lokale `pr-evidence` existieren vor dem ersten Validator-Edit. / The canonical skeleton, nine domain Markdown projections, and local `pr-evidence` exist before the first validator edit.

## Phase 3: User Stories 1–3 – Repräsentativer EX036-Vertikalschnitt (P1) / Representative EX036 vertical slice

**Ziel / Goal**: `EX036 Tp7FileManager` beweist als einziger erster Slice das vollständige Source-/Evidence-/Entscheidungs-/Proof-Modell semantisch rot und anschließend grün. / `EX036 Tp7FileManager` proves the complete source/evidence/decision/proof model with a semantic red and then green.

**Unabhängiger Test / Independent test**: Der fokussierte Test kompiliert vollständig, scheitert zunächst nur an fehlendem/unvollständigem `EX036` und besteht danach mit allen Gegenrelationen und kontrollierten Dateigrenzen.

- [X] T029 [US1-US3] Den fokussierten `EX036`-Akzeptanztest zuerst in `tests/TuiVision.Examples.SmokeTests/ExamplePortfolioAuditIntegrityTests.cs` anlegen; er verlangt TVFM-/Feature-037-Relationen, zehn Dimensionen, sichtbare Interaktion, State/View/Cell-Proof, Guide, A11Y, Plattform, Review, Risiko und Trigger.
- [X] T030 [US1-US3] Eine minimale interne Validator-Kompilationsoberfläche in `tests/TuiVision.Examples.SmokeTests/ExamplePortfolioAuditIntegrityTests.cs` ergänzen, die den expliziten Repository-Root und strukturierte Diagnosen unterstützt, aber den fehlenden Slice noch nicht akzeptiert.
- [X] T031 [US1-US3] Die neue Testlogik auf didaktischen Kommentarwert prüfen und nur die nicht offensichtliche gemeinsame Vorwärts-/Rückrelationsgrenze kurz DE-first/EN-second kommentieren.
- [X] T032 [US1-US3] Unmittelbar vor T033 den Build-Counter einmal erhöhen und alle drei Versionsfelder in `Directory.Build.props` auf `1.38.<CommitCount>.<Build>` ausrichten.
- [X] T033 [US1-US3] `dotnet build tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release` ausführen und ausschließlich die vollständig kompilierbare Testoberfläche als Voraussetzung für semantisches Rot belegen.
- [X] T034 [US1-US3] Unmittelbar vor T035 den Build-Counter erneut einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T035 [US1-US3] Den fokussierten Release-Test mit `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~Tp7FileManager"` als erwartetes semantisches Rot ausführen; nur `EPA010` oder ein engerer EX036-Slicefehler ist akzeptabel.
- [X] T036 [US1-US3] Exitcode und alleinige Red-Ursache in `specs/038-example-portfolio-conformance-audit/pr-evidence.md` dokumentieren; Restore-, Compile-, Infrastruktur- oder Fremdtestfehler verwerfen den Red-Nachweis.
- [X] T037 [US1-US3] `tests/TuiVision.Examples.SmokeTests/Fixtures/ExamplePortfolioAudit/valid-vertical-slice.json` mit dem vollständigen EX036-Baseline-, TVFM-, Feature-035/036/037-, Entry-, Guide-, Smoke- und kontrollierten Dateipfad-Slice anlegen.
- [X] T038 [US1-US3] `EX036 Tp7FileManager` in `example-portfolio-audit.json` mit eigener historischer Zweck-/Lernzielbeschreibung, genau 24 TVFM-Source-Relationen, Entry-Point, Frameworkkomponenten und lokaler Sonderlogik vollständig ergänzen.
- [X] T039 [US1-US3] Für EX036 genau eine Frameworkentscheidung und Disposition sowie alle zehn Dimensionen zu Verhalten, Interaktion, Proof, Dokumentation, A11Y, Plattform und vier Source-Relationen mit Evidence, Review, Risiko und Trigger abschließen; bei Finding, ProductDecision oder unklarer Ownership fail-closed stoppen.
- [X] T040 [US1-US3] EX036 atomar in Source-Manifest, Inventory, Conformance-, Framework-, Proof-/Platform-, Learning-/A11Y- und lokale Evidence-Projektionen spiegeln und jede Vorwärts-/Rückrelation prüfen.
- [X] T041 [US1-US3] Unmittelbar vor T042 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T042 [US1-US3] Den fokussierten EX036-Release-Test aus T035 erneut ausführen und vollständiges Grün für Source-/Evidence-Reziprozität, kontrollierte Dateigrenze, App-Loop/State/View/Cell, Status, Description, A11Y und Plattformfallback verlangen.
- [X] T043 [US1-US3] EX036-Green mit Testzahl, Exitcode und Proof-Grenze in `pr-evidence.md` erfassen und den sicheren Vertikalschnitt-Checkpoint im State nur nach erfolgreicher State-Validierung setzen.
- [X] T044 [US1] Den vollständigen Portfolio-Akzeptanztest in `ExamplePortfolioAuditIntegrityTests.cs` test-first ergänzen; der derzeitige 1/37-Datensatz muss kontrolliert unvollständig bleiben.
- [X] T045 [US1] Unmittelbar vor T046 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T046 [US1] Den fokussierten Portfolio-Test mit `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~Portfolio"` als erwartetes 1/37-Rot ausführen.
- [X] T047 [US1] Das erwartete Vollmengen-Rot und die fehlenden `EX001`–`EX035`/`EX037`-Grenzen in `pr-evidence.md` festhalten, ohne eine fehlende Zeile als Produktdefekt zu interpretieren.

**Checkpoint**: EX036 ist ein vollständig grüner vertikaler Slice; die Vollmenge ist bewusst semantisch rot. / EX036 is a complete green vertical slice; the full population is intentionally semantic red.

## Phase 4: Deterministischer Validator und 46 Ein-Ursachen-Fixtures / Deterministic validator and 46 one-cause fixtures

**Ziel / Goal**: Jede Validatorregel wird test-first über stabile `EPA###`-Diagnosen bewiesen; jede Fixture verletzt genau eine Primärinvariante. / Every validator rule is proved test-first with stable `EPA###` diagnostics; each fixture violates exactly one primary invariant.

- [X] T048 [US1] Die fünf Schema-/Baseline-Fixtures `malformed-json-syntax.json` (`EPA001`), `malformed-unknown-schema.json` (`EPA002`), `malformed-wrong-feature-or-mode.json` (`EPA003`), `malformed-intake-hash.json` (`EPA004`) und `malformed-project-set-hash.json` (`EPA005`) samt parameterisierten Tests zuerst unter `tests/TuiVision.Examples.SmokeTests/Fixtures/ExamplePortfolioAudit/` anlegen.
- [X] T049 [US1] Unmittelbar vor T050 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T050 [US1] Nur die EPA001–EPA005-Tests per `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~SchemaBaseline"` als erwartetes Red ausführen.
- [X] T051 [US1] Explizite `System.Text.Json`-Optionen, Schema-/Feature-/DeliveryMode-, Hash- und Projektmengenprüfung mit atomarer Ablehnung und stabilen EPA001–EPA005-Diagnosen implementieren.
- [X] T052 [US1] Unmittelbar vor T053 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T053 [US1] Die EPA001–EPA005-Gruppe mit demselben fokussierten Testfilter grün ausführen und die eine Fehlerursache je Fixture belegen.
- [X] T054 [US1] Die fünf Inventar-Fixtures `malformed-missing-example.json` (`EPA010`), `malformed-duplicate-example.json` (`EPA011`), `malformed-unknown-example.json` (`EPA012`), `malformed-role-wave.json` (`EPA013`) und `malformed-a11y-history.json` (`EPA014`) samt Tests zuerst anlegen.
- [X] T055 [US1] Unmittelbar vor T056 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T056 [US1] Nur die EPA010–EPA014-Tests per `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~Inventory"` als erwartetes Red ausführen.
- [X] T057 [US1] Exakte EX001–EX037-, Name-, Rolle-, Wave-, A11yFramework- und Portfolio-Drift-Regeln mit stabilen EPA010–EPA014-Diagnosen implementieren.
- [X] T058 [US1] Unmittelbar vor T059 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T059 [US1] Die EPA010–EPA014-Gruppe mit demselben fokussierten Testfilter grün ausführen.
- [X] T060 [US2] Die neun Relations-/Pfad-Fixtures `malformed-missing-source.json` (`EPA020`), `malformed-duplicate-source-path.json` (`EPA021`), `malformed-source-hash.json` (`EPA022`), `malformed-orphan-source.json` (`EPA023`), `malformed-nonreciprocal-source.json` (`EPA024`), `malformed-moving-upstream.json` (`EPA025`), `malformed-missing-evidence.json` (`EPA030`), `malformed-nonreciprocal-evidence.json` (`EPA031`) und `malformed-protected-evidence-path.json` (`EPA032`) samt Tests zuerst anlegen.
- [X] T061 [US2] Unmittelbar vor T062 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T062 [US2] Nur die EPA020–EPA032-Relationsgruppe per `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~Relations"` als erwartetes Red ausführen.
- [X] T063 [US2] Eindeutige Authority-/Pfad-/Hash-/Pin-Regeln, akzeptierte Upstream-Pins, kontrollierte Evidence-Pfade und reziproke Source-/Evidence-Relationen mit EPA020–EPA032 implementieren.
- [X] T064 [US2] Unmittelbar vor T065 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T065 [US2] Die EPA020–EPA032-Relationsgruppe mit demselben Filter grün ausführen.
- [X] T066 [US1-US3] Die sieben Entscheidungs-Fixtures `malformed-unknown-dimension.json` (`EPA040`), `malformed-na-without-rationale.json` (`EPA041`), `malformed-pass-without-evidence.json` (`EPA042`), `malformed-multiple-disposition.json` (`EPA043`), `malformed-accepted-with-gap.json` (`EPA044`), `malformed-gap-without-finding.json` (`EPA045`) und `malformed-framework-decision.json` (`EPA046`) samt Tests zuerst anlegen.
- [X] T067 [US1-US3] Unmittelbar vor T068 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T068 [US1-US3] Nur die EPA040–EPA046-Tests per `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~Decisions"` als erwartetes Red ausführen.
- [X] T069 [US1-US3] Geschlossene Status-/Framework-/Disposition-Vokabulare, zehn `DimensionDecision`-Objekte, begründetes `N/A`, Pass-Evidence und Gap-/ProductDecision-Konsistenz mit EPA040–EPA046 implementieren.
- [X] T070 [US1-US3] Unmittelbar vor T071 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T071 [US1-US3] Die EPA040–EPA046-Gruppe mit demselben Filter grün ausführen.
- [X] T072 [US4] Die sieben Finding-Fixtures `malformed-finding-id-gap.json` (`EPA050`), `malformed-duplicate-dedup-key.json` (`EPA051`), `malformed-split-root-cause.json` (`EPA052`), `malformed-finding-example-link.json` (`EPA053`), `malformed-multiple-primary-owner.json` (`EPA054`), `malformed-unknown-primary-owner.json` (`EPA055`) und `malformed-incomplete-finding.json` (`EPA056`) samt Tests zuerst anlegen.
- [X] T073 [US4] Unmittelbar vor T074 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T074 [US4] Nur die EPA050–EPA056-Tests per `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~Findings"` als erwartetes Red ausführen.
- [X] T075 [US4] Lückenlose EF001+-IDs, kontrollierte Deduplication Keys, reziproke Example-/Finding-Links, genau einen geschlossenen Primary Owner und vollständige Reproduktions-/Proof-/Reviewfelder mit EPA050–EPA056 implementieren.
- [X] T076 [US4] Unmittelbar vor T077 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T077 [US4] Die EPA050–EPA056-Gruppe mit demselben Filter grün ausführen.
- [X] T078 [US5] Die sieben DAG-/Handoff-Fixtures `malformed-owner-cycle.json` (`EPA060`), `malformed-empty-owner-intake.json` (`EPA061`), `malformed-missing-owner-intake.json` (`EPA062`), `malformed-preassigned-feature-number.json` (`EPA063`), `malformed-closure-count.json` (`EPA064`), `malformed-closure-order.json` (`EPA065`) und `malformed-started-followup.json` (`EPA066`) samt Tests zuerst anlegen.
- [X] T079 [US5] Unmittelbar vor T080 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T080 [US5] Nur die EPA060–EPA066-Tests per `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~Handoff"` als erwartetes Red ausführen.
- [X] T081 [US5] Owner-Kantenableitung `Owner(B) -> Owner(A)`, Same-Owner-/Dublettenregeln, Topologiesortierung, Emitted/Suppressed-Kardinalität, unnummerierte Intakes, genau einen letzten Closure und leere `StartedFeatureIds` mit EPA060–EPA066 implementieren.
- [X] T082 [US5] Unmittelbar vor T083 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T083 [US5] Die EPA060–EPA066-Gruppe mit demselben Filter grün ausführen.
- [X] T084 [US4-US5] Die sechs Governance-/Authority-Fixtures `malformed-governance-omission.json` (`EPA070`), `malformed-na-implementation.json` (`EPA071`), `malformed-open-without-owner.json` (`EPA072`), `malformed-remote-claim.json` (`EPA080`), `malformed-premature-conformance.json` (`EPA081`) und `malformed-product-decision-ready.json` (`EPA082`) samt Tests zuerst anlegen.
- [X] T085 [US4-US5] Unmittelbar vor T086 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T086 [US4-US5] Nur die EPA070–EPA082-Tests per `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~GovernanceAuthority"` als erwartetes Red ausführen.
- [X] T087 [US4-US5] Vollständige Governancefelder, `N/A=Not Assessed`, `Open`-Pflichten, authority-bound `MergeAndSync`-Claims, ProductDecision-Blockade und Verbot vorzeitiger Portfolio-Konformität mit EPA070–EPA082 implementieren.
- [X] T088 [US4-US5] Unmittelbar vor T089 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T089 [US4-US5] Die EPA070–EPA082-Gruppe mit demselben Filter grün ausführen.
- [X] T090 [US1-US5] Validatorpfade mit `/` normalisieren und absolute Pfade, `..`, NUL, ausbrechende Symlinks, übergroße Dateien/Sammlungen/Strings sowie HOME-, CWD-, Locale-, Uhrzeit-, Netzwerk-, Zufalls- und Parallelreihenfolge-Abhängigkeit fail-closed ablehnen; Diagnosen dürfen keine Secrets oder Umgebungswerte ausgeben.
- [X] T091 [US1-US5] Statisch und im Testcode nachweisen, dass exakt 46 Fixture-Dateien und 46 eindeutige erwartete EPA-Codes vorhanden sind und jede Fixture genau eine Primärinvariante gegenüber ihrer gültigen Basis verletzt.
- [X] T092 [US1-US5] Den finalen Validator-Kommentarpass auf höchstens die drei geplanten Warum-Grenzen beschränken: Reziprozität, EF-ID-Vergabe nach Root-Cause-Freeze und exakte Evidence-Grenze für `MergeAndSync`-Remote-/Closure-Claims; DE-first/EN-second und CEFR-B2.
- [X] T093 [US1-US5] Unmittelbar vor T094 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T094 [US1-US5] Alle 46 Negativ-Fixtures gemeinsam per `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~Malformed"` grün ausführen und exakt einen erwarteten stabilen Code je Fixture verlangen.
- [X] T095 [US1-US5] Die sieben test-first Red-/Green-Gruppen, 46/46-Ein-Ursachen-Matrix, Determinismus- und Sicherheitsgrenzen in `pr-evidence.md` erfassen, ohne das noch unvollständige 37-Zeilen-Portfolio als bestanden zu markieren.

**Checkpoint**: Der Validator ist deterministisch und alle 46 Negativklassen sind grün; die breite Portfolio-Population bleibt der nächste getrennte Slice. / The validator is deterministic and all 46 negative classes are green; broad portfolio population remains the next separate slice.

## Phase 5: User Stories 1–3 – Waveweiser Review aller 37 Zeilen / Wave-wise review of all 37 rows

**Ziel / Goal**: Jede verbleibende Zeile wird einzeln gegen historische Absicht, aktuelle Semantik, Framework, Interaktion, Proof, Dokumentation, A11Y und Plattform bewertet und nach jeder Wave atomar projiziert. / Every remaining row is reviewed individually, then each wave is atomically projected.

**Zeilenvertrag / Row contract**: Jede Zeilenaufgabe vervollständigt die exakten Entry-/Guide-/Authority-/Evidence-Pfade aus `contracts/example-portfolio-audit-acceptance.md`, alle FR-005-Felder, genau eine Rolle/Frameworkentscheidung/Disposition, zehn Dimensionen, reziproke Source-/Evidence-IDs, eigene Zweck-/Lerntexte, Review, Restrisiko und Trigger. Vergleichsquellen sind nur bei fachlich gleicher Verantwortung relevant; reine Optik-/API-/Layout-/Vererbungs-/Speicher-/Quelltextunterschiede sind keine Findings.

### Phase 5a: Wave 1 / Wave 1

- [X] T096 [US1-US3] `EX001 Desklogo` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T097 [US1-US3] `EX002 MsgCls` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T098 [US1-US3] `EX003 Tutorial` einschließlich `tvguid01`–`tvguid16`-Intent und eindeutiger Lern-/Proof-Grenze als vollständige kanonische Zeile reviewen.
- [X] T099 [US1-US3] `EX004 Videomode` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T100 [US1-US3] Wave-1-Checkpoint: EX001–EX004, Quellen, Evidence, zehn Dimensionen, Guide-/A11Y-/Plattformentscheidungen und Markdown-Projektionen atomar konsistent machen und den sicheren Stop-Punkt dokumentieren.

### Phase 5b: Wave 2 / Wave 2

- [X] T101 [US1-US3] `EX005 Clipboard` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T102 [US1-US3] `EX006 Demo` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T103 [US1-US3] `EX007 DlgDsn` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T104 [US1-US3] `EX008 DynTxt` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T105 [US1-US3] `EX009 InpLis` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T106 [US1-US3] `EX010 ListVi` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T107 [US1-US3] `EX011 ProgBa` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T108 [US1-US3] `EX012 Sdlg` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T109 [US1-US3] `EX013 Sdlg2` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T110 [US1-US3] `EX014 TCombo` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T111 [US1-US3] `EX015 TProgB` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T112 [US1-US3] Wave-2-Checkpoint: EX005–EX015, Quellen, Evidence, zehn Dimensionen, sichtbare Bedienung, Guide-/A11Y-/Plattformentscheidungen und Projektionen atomar konsistent machen und den sicheren Stop-Punkt dokumentieren.

### Phase 5c: Wave 3 / Wave 3

- [X] T113 [US1-US3] `EX016 BHelp` einschließlich der begrenzten historischen `.tch`-Abweichung als vollständige kanonische Zeile reviewen.
- [X] T114 [US1-US3] `EX017 HelpDemo` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T115 [US1-US3] `EX018 I18n` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T116 [US1-US3] `EX019 TvEdit` mit Safe-Close-/Datei-/Editorrisiken als vollständige kanonische Zeile reviewen.
- [X] T117 [US1-US3] `EX020 TvHc` mit kontrollierter Help-Compiler-/Persistenzgrenze als vollständige kanonische Zeile reviewen.
- [X] T118 [US1-US3] Wave-3-Checkpoint: EX016–EX020, Quellen, Evidence, zehn Dimensionen, Risiko-Negativpfade und Projektionen atomar konsistent machen und den sicheren Stop-Punkt dokumentieren.

### Phase 5d: Wave 4 / Wave 4

- [X] T119 [US1-US3] `EX021 Cyrillic` mit Unicode-/Charset-/Plattformgrenzen als vollständige kanonische Zeile reviewen.
- [X] T120 [US1-US3] `EX022 ETerm` mit Terminal-/Fallbackgrenzen als vollständige kanonische Zeile reviewen.
- [X] T121 [US1-US3] `EX023 Fonts` mit Font-/Breiten-/Fallbackgrenzen als vollständige kanonische Zeile reviewen.
- [X] T122 [US1-US3] `EX024 Terminal` mit App-Loop-, Terminalsession- und Small-Terminal-Proof als vollständige kanonische Zeile reviewen.
- [X] T123 [US1-US3] `EX025 XTerm` mit XTerm-/Farb-/Capabilitygrenzen als vollständige kanonische Zeile reviewen.
- [X] T124 [US1-US3] Wave-4-Checkpoint: EX021–EX025, Quellen, Evidence, zehn Dimensionen, Unicode-/Charset-/Terminal-/Plattformentscheidungen und Projektionen atomar konsistent machen und den sicheren Stop-Punkt dokumentieren.

### Phase 5e: Wave 5 / Wave 5

- [X] T125 [US1-US3] `EX026 Tp7AsciiTable` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T126 [US1-US3] `EX027 Tp7Calculator` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T127 [US1-US3] `EX028 Tp7Calendar` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T128 [US1-US3] `EX029 Tp7Demo` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T129 [US1-US3] `EX030 Tp7Edit` mit kontrollierter Datei-/Safe-Close-Grenze als vollständige kanonische Zeile reviewen.
- [X] T130 [US1-US3] `EX031 Tp7Help` mit Help-/Ressourcen-/Fallbackgrenze als vollständige kanonische Zeile reviewen.
- [X] T131 [US1-US3] `EX032 Tp7MouseDialog` mit realem Mauspfad und Tastaturfallback als vollständige kanonische Zeile reviewen.
- [X] T132 [US1-US3] `EX033 Tp7Puzzle` als vollständige kanonische Zeile und in allen betroffenen Projektionen reviewen.
- [X] T133 [US1-US3] `EX034 Tp7ResourceDemo` mit Ressourcen-/Persistenzgrenze als vollständige kanonische Zeile reviewen.
- [X] T134 [US1-US3] `EX035 Tp7ResourceGenerator` mit kontrollierten Fixtures und Outputgrenzen als vollständige kanonische Zeile reviewen.
- [X] T135 [US1-US3] Wave-5-Checkpoint: EX026–EX035, TVDEMOS-Quellen, akzeptierte Features 032/033/034, zehn Dimensionen, Risiko-Proofs und Projektionen atomar konsistent machen und den sicheren Stop-Punkt dokumentieren.

### Phase 5f: Supplemental Control / Zusätzliche Kontrolle

- [X] T136 [US1-US3] `EX037 A11yFramework` genau einmal als `SupplementalControl` mit leeren HistoricalSourceIds, begründetem `HistoricalRelation=N/A` samt Trigger und vollständiger Learning-/A11Y-/Proof-Prüfung reviewen.
- [X] T137 [US1-US3] Supplemental-Checkpoint: EX037 und alle betroffenen Source-/Evidence-/Learning-/A11Y-/Proof-Projektionen atomar konsistent machen und den sicheren Stop-Punkt dokumentieren.
- [X] T138 [US1-US3] Die vollständige Grundmenge EX001–EX037 gegen alle 37 direkten Projektpfade, Namen, Rollen, Waves, Entry-Points, Guides und akzeptierten Evidence-Basen prüfen; jede unbekannte, fehlende oder doppelte Zeile blockiert als Portfolio-Drift.
- [X] T139 [US1-US3] Alle Source-, Evidence- und Zeilenrückrelationen sowie die neun fachlichen Markdown-Projektionen und lokale `pr-evidence` atomar aus `example-portfolio-audit.json` synchronisieren; kein verwaister Knoten und keine konkurrierende Wahrheit ist zulässig.
- [X] T140 [US1-US3] Unmittelbar vor T141 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T141 [US1-US3] `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&(FullyQualifiedName~Portfolio|FullyQualifiedName~Relations)"` ausführen und 37/37, EX001–EX037, 25/10/1/1 sowie vollständige Reziprozität grün verlangen.
- [X] T142 [US1-US3] Exakte Zeilenzahl, Source-/Evidence-Zahlen, Set-Hash, Testzahl und verbleibende ehrliche `N/A`-Grenzen in `pr-evidence.md` erfassen und den State am Broad-Review-Checkpoint validieren.

**Checkpoint**: Alle 37 Zeilen und ihre Relationen sind vollständig reviewt; Findings sind noch nicht nummeriert oder übergeben. / All 37 rows and relations are fully reviewed; findings are not yet numbered or handed off.

## Phase 6: User Story 4 – Findings, Deduplizierung und genau ein Primary Owner (P1) / Findings, deduplication, and exactly one primary owner

**Ziel / Goal**: Nur reproduzierbare Lücken werden nach Root Cause dedupliziert, lückenlos als `EF001+` eingefroren und genau einem Primary Owner zugeordnet. / Only reproducible gaps are deduplicated by root cause, frozen contiguously as `EF001+`, and assigned exactly one primary owner.

**Unabhängiger Test / Independent test**: Jede Gap-Dimension hat genau ein reziprokes Finding oder einen blockierenden ProductDecision; Keys, Owner und IDs sind eindeutig und deterministisch.

- [X] T143 [US4] Alle zehn Dimensionsentscheidungen aller 37 Zeilen nach `Gap`, `IntentionalDeviation`, `Pass` und begründetem `N/A` sammeln; jede Gap-Beobachtung muss reproduzierbare TuiVision-Evidence oder einen blockierenden ProductDecision besitzen.
- [X] T144 [US4] Reine Stil-, Optik-, Layout-, API-Form-, Typ-, Vererbungs-, Speicher-, Sprach- oder Quelltextpräferenzen ohne Nutzer-/Lern-/A11Y-/Plattform-/Framework-/Proof-Lücke begründet verwerfen und nicht in Follow-up überführen.
- [X] T145 [US4] Reproduzierbare Beobachtungen vor ID-Vergabe nach `<primary-owner>:<dimension>:<ascii-kebab-root-cause>` gruppieren, gleiche Ursachen über alle ExampleIds vereinigen und Freitext/Beispielnamen aus dem Key ausschließen.
- [X] T146 [US4] Für jede Root Cause genau einen Primary Owner nach der festen Ursachenregel `FrameworkReuse`, sonst `BehaviorInteraction`, sonst `ProofPlatform`, sonst `LearningA11Y` bestimmen; sekundäre Auswirkungen separat halten und bei Mehrdeutigkeit stoppen.
- [X] T147 [US4] Finding-Abhängigkeiten vollständig festhalten; Reproduktion, HistoricalIntent, CurrentBehavior, MissingBehaviorOrProof, SourceRelations, Risk, RequiredRedProof, RequiredRealPathGreenProof, API-/A11Y-/PlatformImpact, Owner, Reviewer, ReviewDate, ResidualRisk und Trigger vervollständigen.
- [X] T148 [US4] Erst nach Root-Cause-Freeze nach fester Owner-Reihenfolge und ordinalem DeduplicationKey sortieren und lückenlos `EF001+` vergeben; null Findings bleiben eine explizit zulässige eingefrorene Menge.
- [X] T149 [US4] FindingIds zwischen allen betroffenen Portfoliozeilen und Findings reziprok synchronisieren und jeden DeduplicationKey exakt einem Finding zuordnen.
- [X] T150 [US4] ProductDecision, nicht reproduzierbares Finding, unklare Owner-Zuordnung oder nicht behebbare Evidence-/Security-/Validierungsintegrität als harten Stop prüfen; kein Finding in Feature 038 beheben.
- [X] T151 [US4] Eingefrorene Findings, verworfene Nicht-Funde, Cross-Cutting-Auswirkungen und Risiken atomar in `example-portfolio-findings.md`, Matrix-, Framework-, Proof-/Platform-, Learning-/A11Y- und `pr-evidence.md`-Sichten projizieren.
- [X] T152 [US4] Unmittelbar vor T153 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T153 [US4] `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~Findings"` ausführen und lückenlose IDs, eindeutige Keys, genau einen Owner, vollständige Proof-/Reviewfelder und reziproke Links grün verlangen.
- [X] T154 [US4] Findinganzahl, EF-ID-Bereich, Deduplication-Gruppen, Ownerverteilung, Blockerfreiheit und Testresultat in `pr-evidence.md` erfassen und den Findings-Freeze als sicheren State-Checkpoint validieren.

## Phase 7: User Story 5 – Owner-DAG, nicht leere Intakes und genau ein Closure (P1) / Owner DAG, non-empty intakes, and exactly one closure

**Ziel / Goal**: Nur tatsächlich nicht leere Owner-Gruppen erzeugen unnummerierte Remediation-Intakes; danach folgt genau ein unnummerierter, unabhängiger Closure, und nichts wird gestartet. / Only genuinely non-empty owner groups emit unnumbered remediation intakes; exactly one unnumbered independent closure follows, and none is started.

**Unabhängiger Test / Independent test**: Der DAG ist azyklisch, alle Findings erscheinen genau einmal, leere Gruppen sind unterdrückt und der Closure ist der letzte von allen emittierten Gruppen abhängige Pfad.

- [X] T155 [US5] Aus Finding-Abhängigkeiten die kollabierten Cross-Owner-Kanten `Owner(B) -> Owner(A)` ableiten, Same-Owner-Abhängigkeiten intern halten und eine zyklusfreie Topologiesortierung mit fester Owner-Tie-Break-Reihenfolge erzeugen.
- [X] T156 [US5] Für `FrameworkReuse` genau dann `requirements/intakes/active/Lastenheft_Example-Portfolio-FrameworkReuse-Remediation.md` mit allen Findings, Dependencies und Red-/Real-Path-Green-Anforderungen sowie genau einem Schema-2.0-Receipt unter `specs/intake-authoring-receipts/` erzeugen, wenn die finale Gruppe nicht leer ist; sonst `Suppressed` dokumentieren und weder Intake noch Receipt erzeugen.
- [X] T157 [US5] Für `BehaviorInteraction` genau dann `requirements/intakes/active/Lastenheft_Example-Portfolio-BehaviorInteraction-Remediation.md` vollständig und unnummeriert samt genau einem Schema-2.0-Receipt unter `specs/intake-authoring-receipts/` erzeugen, wenn die finale Gruppe nicht leer ist; sonst `Suppressed` ohne Datei oder Receipt.
- [X] T158 [US5] Für `ProofPlatform` genau dann `requirements/intakes/active/Lastenheft_Example-Portfolio-ProofPlatform-Remediation.md` vollständig und unnummeriert samt genau einem Schema-2.0-Receipt unter `specs/intake-authoring-receipts/` erzeugen, wenn die finale Gruppe nicht leer ist; sonst `Suppressed` ohne Datei oder Receipt.
- [X] T159 [US5] Für `LearningA11Y` genau dann `requirements/intakes/active/Lastenheft_Example-Portfolio-LearningA11Y-Remediation.md` vollständig und unnummeriert samt genau einem Schema-2.0-Receipt unter `specs/intake-authoring-receipts/` erzeugen, wenn die finale Gruppe nicht leer ist; sonst `Suppressed` ohne Datei oder Receipt.
- [X] T160 [US5] Prüfen, dass jede finale FindingId genau einer emittierten Owner-Gruppe angehört, jede leere Gruppe datei- und receiptlos bleibt und keine Intake-/Receipt-Paarung leer, dupliziert, hashinkonsistent oder mit einer Feature-Nummer versehen ist.
- [X] T161 [US5] Exakt einmal `requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md` als unnummerierten unabhängigen Closure und genau ein zugehöriges Schema-2.0-Receipt unter `specs/intake-authoring-receipts/` erzeugen, von allen tatsächlich emittierten Remediation-Intakes abhängig machen und vollständige Konformität/Lernreife ausschließlich diesem späteren Intake vorbehalten.
- [X] T162 [US5] `OrderedIntakePaths` topologisch mit dem Closure exakt zuletzt festlegen, `StartedFeatureIds` leer halten und jede Branch-/Run-/PR-/Merge-Startbehauptung ausschließen.
- [X] T163 [US5] Jeden tatsächlich erzeugten Remediation-/Closure-Intake und sein genau zugeordnetes Schema-2.0-Receipt mit den vorhandenen Bash- und PowerShell-Intake-Authoring-Artefakt- und Receipt-Validatoren gegen `requirements/intake-governance-config.json`, Zielhash, Promptzustand, Agent-Surface, Handoff und Level-2-Profil prüfen; unterdrückte Intake- und Receipt-Pfade müssen nachweislich fehlen.
- [X] T164 [US5] Owner-DAG, vier Emitted/Suppressed-Zeilen, Intake-Pfade, Closure-Abhängigkeiten und ehrlichen Gatezustand atomar in Dataset, `example-remediation-handoff.md`, `example-portfolio-gate.md` und `pr-evidence.md` synchronisieren.
- [X] T165 [US5] Unmittelbar vor T166 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T166 [US5] `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests&FullyQualifiedName~Handoff"` ausführen und DAG, 0–4 nicht leere Intakes, Suppression, genau einen letzten Closure und null gestartete Features grün verlangen.
- [X] T167 [US5] Handoff-/Closure-Kardinalität, Topologie, unterdrückte Gruppen, Testresultat und sicheren Handoff-Freeze in `pr-evidence.md` und State erfassen; kein Folgefeature starten.

**Checkpoint**: Findings und Handoff sind final, aber keine Remediation und kein Closure-Feature wurde begonnen. / Findings and handoff are final, but no remediation or closure feature has started.

## Phase 8: Governance, Architektur, Security, A11Y und Agentenparität / Governance, architecture, security, A11Y, and agent parity

**Zweck / Purpose**: Alle zwölf installierten Presets und benannten Standards ohne stille Auslassung mit getrennter Applicability und Implementation abschließen. / Close all twelve installed presets and named standards without silent omission, separating applicability from implementation.

- [X] T168 `security-governance` v0.6.2/Priority 10 als `Applicable` für NIST SSDF, CWE Top 25, Scope-, Eingabe- und Evidence-Integrität vollständig in Dataset/Gate/`pr-evidence.md` entscheiden.
- [X] T169 `architecture-governance` v0.5.2/Priority 20 als `Applicable` für Scope-/Integritätsgrenzen führen und Trust-/Cloud-/Serviceänderungen proportional `N/A` mit Trigger dokumentieren.
- [X] T170 `isaqb-architecture-governance` v0.2.2/Priority 30 als `Applicable` für Framework-Reuse, Qualitätsrisiken, Trade-offs und Technical-Debt-Handoff einschließlich finalem Architekturreview abschließen.
- [X] T171 `a11y-governance` v0.4.3/Priority 40 als `Applicable` für Lern-, Dokumentations-, Tastatur-, text-first- und WCAG-2.2-AA-Prüfung abschließen.
- [X] T172 `cross-platform-governance` v0.2.2/Priority 50 als `Applicable` für Plattform-/Terminalaudit führen; Script-Paar, Manpage, PowerShell-Hilfe, Cmdlet, Dry-run/WhatIf und Script-Paritätscheck als nicht ausgelöstes `N/A` mit script-shaped Trigger dokumentieren.
- [X] T173 `agent-parity-governance` v0.4.2/Priority 60 als `Applicable` für die gemeinsame Status-/Guidance-Entscheidung abschließen und einseitige Agentenänderungen verbieten.
- [X] T174 `model-routing-governance` v0.1.4/Priority 61 als `Applicable` abschließen, vorhandene providerneutrale Routing-Metadaten erhalten und keine Provider-Modellnamen in Feature-Anforderungen oder Tasks einführen.
- [X] T175 `intake-authoring-governance` v0.3.1/Priority 64 als `Applicable` für den bindenden Intake und jede erzeugte unnummerierte, nicht leere Remediation-/Closure-Intake-plus-Schema-2.0-Receipt-Paarung abschließen; Allowlist, Modell, Prompt-/Handoff-Metadaten und normalisierte Zielhashes dürfen nicht still fehlen.
- [X] T176 `intake-review-governance` v0.2.1/Priority 65 als `Applicable` für das hashgleiche `Ready`-Review mit null Findings, Fragen und akzeptierten Risiken abschließen.
- [X] T177 `intake-sequencing-governance` v0.2.3/Priority 66 als `Applicable` für `Eligible`, erfülltes HardCompletionGate, Owner-DAG und topologische Intake-Reihenfolge abschließen.
- [X] T178 `autonomous-run-governance` v0.3.6/Priority 70 als `Applicable` für `MergeAndSync`, aktuelle Authority-Revalidierung, State-, Stop-/Resume- und fail-closed Phasengrenzen abschließen.
- [X] T179 `parallel-autonomous-run-governance` v0.2.6/Priority 80 als `N/A`/`Not Assessed` mit Rationale „keine Campaign-Autorität“ und ausdrücklichem Re-Evaluation-Trigger abschließen.
- [X] T180 C# als MSL-Allowlist-Sprache und die test-only Parser-/Datei-I/O-Änderung gegen sichere C#/.NET-Regeln, explizite Eingabevalidierung, NIST SSDF und CWE-20/22/400/502/703 prüfen; Defense in Depth, Least Privilege, Fail-Safe Defaults, Attack Surface Reduction und Separation of Concerns als implementiert oder mit konkretem Fund belegen.
- [X] T181 OWASP ASVS, SBOM, VEX, SLSA, AI-SBOM, OpenSSF Scorecard, NIS2, CRA, EU AI Act und DORA mit den akzeptierten `N/A`-Rationalen, `Not Assessed`, ResidualRisk und Triggern abschließen; AI bleibt ausschließlich Entwicklungs-/Agentenwerkzeug.
- [X] T182 STRIDE/CIA/CAPEC, S-ADR, arc42 Security, Security Quality Scenarios, Zero Trust, OWASP SAMM, BSI C3A/C5 und neue allgemeine Dateien unter `docs/security/`/`docs/architecture/` als triggerbasiertes `N/A` abschließen, sofern kein ProductDecision oder signifikanter tatsächlicher Fund diese Grenze geändert hat; andernfalls stoppen und den ausgelösten Evidence-Follow-up disponieren.
- [X] T183 `docs/accessibility/` als `NoUpdateRequired`/triggerbasiertes `N/A` bestätigen, sofern keine portfolioübergreifende A11Y-Lücke vorliegt; jede tatsächliche Lücke bleibt Finding-/Remediation-Evidence und wird nicht in Feature 038 behoben.
- [X] T184 Für jede Governance-/Standardszeile `Applicability`, `Implementation`, `Rationale`, `EvidencePath`, `Owner`, `Reviewer`, `ResidualRisk`, `ReevaluationTrigger` und `FollowUp` vervollständigen; `Open` verlangt Owner und konkreten Follow-up, `N/A` verlangt `Not Assessed`.
- [X] T185 Die Agent-Paritätsentscheidung gegen `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`, `.specify/templates/` und `.specify/memory/constitution.md` prüfen; ohne neue portable Regel `NoUpdateRequired`, bei Trigger nur atomare vollständige Parität.
- [X] T186 Genau eine Documentation-Impact-Entscheidung `UpdateRequired` mit Zielgruppen, Familien, Reader Paths, Source/Owner, Navigation, Dokumentklasse, Sprachpartner, Plattform-/Beispielproof, Distribution, Home-Sync, Evidence und Trigger in Gate und `pr-evidence.md` abschließen.
- [X] T187 XML/Public API, CLI-Hilfe, Screenshots, Beispiel-Guide-Korrekturen, Script-Dokumentation und globale Navigation nur bei tatsächlichem Diff-/Finding-Trigger ändern; andernfalls jeweils begründetes `N/A`/`NoUpdateRequired` dokumentieren.

## Phase 9: Lokale Validierungsleiter / Local validation ladder

**Zweck / Purpose**: Alle anwendbaren lokalen Gates in fester Reihenfolge prüfen; jeder Build-/Testbefehl hat seinen eigenen Counter-Schritt. / Validate every applicable local gate in fixed order; every build/test command has its own counter step.

- [X] T188 `git diff --check` ausführen und Whitespace-/Patchintegrität in `pr-evidence.md` erfassen.
- [X] T189 Den finalen Pfaddiff gegen die Allowlist prüfen und null Änderungen unter `src/`, `examples/`, `tv203s/`, `TVDEMOS/`, `TVFM/`, Beispiel-Guides sowie null ungepinnte externe Source-/Checkout-Änderung verlangen.
- [X] T190 Public-API-, XML-Dokumentations-, `*.csproj`-/`*.sln`-, Paket-, Lock-, Dependency-, Runtime- und Framework-Deltas sowie unerlaubte `Directory.Build.props`-Änderungen explizit scannen und null fachliches Delta verlangen.
- [X] T191 `bash scripts/scan-agent-secrets.sh` und `pwsh -NoProfile -File scripts/scan-agent-secrets.ps1` gegen den tatsächlichen Diff ausführen und null Secret-/Credential-/Agent-History-/Log-/SQLite-Fund verlangen.
- [X] T192 Den verlangten `dotnet list package --outdated`-Review mit dem exakten Solution-Befehl `dotnet list TuiVision.sln package --outdated` read-only ausführen, aktuelle Ergebnisse dokumentieren und wegen Feature-Scope keine Dependency aktualisieren; Netzwerk-/Registry-Nichtverfügbarkeit ehrlich als `Open` oder `N/A` mit Trigger führen.
- [X] T193 Beide `install-spec-kit-governance-presets.* --check-only`/`-CheckOnly`-Pfade gegen das Zwölf-Preset-Profil `scripts/config/spec-kit-model-routing-governance-presets.json` ausführen; zusätzlich bestätigen, dass dessen acht Kernpresets exakt mit `scripts/config/spec-kit-governance-presets.json` übereinstimmen, und Agent-/Template-Parität ohne Schreibmodus prüfen.
- [X] T194 Model-Routing-Status mit den vorhandenen Bash-/PowerShell-Resolvern read-only prüfen und unveränderte Policy, FallbackPolicy, Rollen und Phasenmetadaten im State bestätigen.
- [X] T195 `dotnet format --verify-no-changes` ausführen und Format-Gate ohne Build-Counter-Schritt dokumentieren.
- [X] T196 Unmittelbar vor T197 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T197 Den vollständigen targeted Auditvalidator mit `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release --filter "FullyQualifiedName~ExamplePortfolioAuditIntegrityTests"` ausführen und positive Invarianten plus 46/46 Negative Fixtures grün verlangen.
- [X] T198 Targeted-Testzahl, 37/37, Relations-/Finding-/Handoff-Zahlen, 46/46-Fixtures, Exitcode und Dauer in `pr-evidence.md` dokumentieren.
- [X] T199 Unmittelbar vor T200 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T200 Das vollständige Beispiel-Smokeprojekt mit `dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj --configuration Release` ausführen und die bestehende Real-Path-Evidence vollständig grün verlangen.
- [X] T201 Smoke-Testzahl, Plattform, Exitcode und relevante Grenzen in `pr-evidence.md` dokumentieren.
- [X] T202 Unmittelbar vor T203 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T203 Die vollständige Regression mit `dotnet test TuiVision.sln --configuration Release` ausführen und alle Repositorytests grün verlangen.
- [X] T204 Volle Testzahl, Exitcode und etwaige Skips mit erforderlicher Tracking-Referenz in `pr-evidence.md` dokumentieren; unbegründete gate-relevante Skips blockieren.
- [X] T205 `coverlet.runsettings` mit `xmllint --noout coverlet.runsettings` prüfen, sofern `xmllint` verfügbar ist; Nichtverfügbarkeit mit Rationale und Trigger erfassen.
- [X] T206 Unmittelbar vor T207 den Build-Counter einmal erhöhen und die drei Versionsfelder ausrichten.
- [X] T207 Das kanonische Coverage-Gate mit `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings` aus dem Repository-Root ausführen.
- [X] T208 Line Coverage für `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` jeweils mindestens 70 % verlangen, das 80-%-Ziel separat verfolgen und assembly-spezifische Zahlen/Reportpfade in `pr-evidence.md` festhalten.
- [X] T209 Alle GATE-038-01–07-Resultate, tatsächlichen Commands, Exitcodes, Evidence-Pfade, Residual Risks und Failure Boundaries im Dataset, Gate und `pr-evidence.md` aktualisieren; kein lokales Ergebnis als Remote Exact-Head ausgeben.

## Phase 10: Statistik, Dokumentation/A11Y und Delivery-Kandidat / Statistics, documentation/A11Y, and delivery candidate

**Zweck / Purpose**: Den abgeschlossenen lokalen Implementierungsmeilenstein statistisch erfassen, ausgelöste Doku-/A11Y-Gates ausführen und den Audit wahrheitsgemäß lokal schließen. / Record the completed local implementation milestone, run triggered documentation/A11Y gates, and close the audit truthfully and locally.

- [X] T210 `docs/project-statistics.md` einmal seriell nach Statistikprofil 2 mit 80 Zeilen/Arbeitstag manuell und 125 Zeilen/Arbeitstag Thorsten-Solo auf den abgeschlossenen Feature-038-Implementierungsmeilenstein aktualisieren; Methodik und Agent-Guidance unverändert lassen.
- [X] T211 `docfx docfx.json` wegen der Statistikänderung ausführen und 0 Warnungen/Fehler sowie keine verfolgten generierten `api/*.yml`- oder `_site/`-Ausgaben verlangen.
- [X] T212 Unter `tests/web-a11y` `npm run test:docfx` gegen den frisch erzeugten lokalen DocFX-Stand ausführen und Playwright/Axe-Ergebnisse für WCAG 2.2 AA dokumentieren.
- [X] T213 Die geänderten publizierten Seiten mit UTF-8-Lynx text-first prüfen und keine Ersatzzeichen, verlorene Überschriften, unverständliche Tabellen oder nur visuell erkennbare Status-/Entscheidungspfade zulassen.
- [X] T214 Alle neuen/änderten Markdown-Dateien auf Deutsch zuerst/Englisch danach, ungefähr CEFR-B2, Erstdefinition technischer/Spec-Kit-Begriffe, semantische Überschriften/Tabellen/Listen, korrekte Umlaute/`ß`, getaggte Codeblöcke, funktionierende Links und nicht farb-/layoutabhängige Kernaussagen prüfen.
- [X] T215 Je EX001–EX037 die dokumentierte Windows-, macOS-, Linux- und begründete WSL-/Terminalgrenze gegen aktuelle lokale macOS-Evidence und akzeptierte Vorgängerevidence prüfen; nicht lokal ausgeführte Plattformen nicht als neu bestanden behaupten.
- [X] T216 Den finalen Gitstatus und Diff auf `_site/`, generierte API-YAMLs, TestResults, Coverage-Ausgaben, Caches, Logs, temporäre Dateien, Agent-History, Secrets und andere Generated Outputs scannen und nur erlaubte Source-Evidence verfolgen.
- [X] T217 GATE-038-08 und GATE-038-09 mit DocFX-/Axe-/Lynx-, Security-/Dependency-/Supply-Chain-, Agent-Paritäts-, Routing-, Governance- und Generated-Output-Evidence abschließen; jede nicht ausgelöste Teilprüfung erhält `N/A`, Rationale und Trigger.
- [X] T218 Alle zwölf Presetzeilen, alle Standardsentscheidungen, FR-/CR-/SC-/IAC-Abdeckung, Checklisten und lokalen GATE-038-01–09 auf `Fulfilled` beziehungsweise ehrliches `N/A` prüfen; GATE-038-10/11 bleiben bis zur Delivery `Not Assessed`, Critical/High/undisponierte Medium Findings bleiben null.
- [X] T219 Einen finalen nicht implementierenden Analyze-/Konsistenzpass über Dataset, neun fachliche Projektionen, `pr-evidence.md`, erzeugte Intakes, Gate, Statistik und Tasks ausführen; nur Evidence-Konsistenz korrigieren, keine Findings beheben oder Scope erweitern.
- [X] T220 Abschließend `git diff --check`, exakte Allowlist-/Protected-Root-, Public-API-, Dependency-, Projekt-, Paket-, externe Source- und Generated-Output-Prüfung wiederholen und den Reviewed-Local-HEAD nur lokal in Evidence erfassen.
- [X] T221 `PortfolioGate` abhängig vom finalen Finding-Satz genau auf `AuditCompleteNoFindings` oder `AuditCompleteWithRemediation` setzen; niemals `PortfolioConformantAndLearningReady`, Remote-Erfolg, Merge oder Post-Merge-Fakt behaupten.
- [X] T222 `example-portfolio-gate.md`, `example-remediation-handoff.md`, `example-portfolio-findings.md`, `pr-evidence.md` und `example-portfolio-audit.json` final atomar synchronisieren und Audit-, offene Remediation- und späteren unabhängigen Closure-Status text-first unterscheiden.
- [X] T223 Die Eingaben für die getrennte autonome Retrospektivphase vorbereiten und ausschließlich reproduzierbares providerneutrales Lernen als möglichen `PresetFollowUp` kennzeichnen; die tatsächliche Klassifikation erfolgt erst nach Implementierung und Delivery im gerouteten `speckit.autonomous-retrospective`-Prozess.
- [X] T224 Den Implementierungsstate seriell auf einen nachweisbaren, noch nicht als gemergt behaupteten Delivery-Kandidaten setzen, Tasks-Pfad/-Hash sowie `completed=total` aktualisieren und mit Bash- und PowerShell-Statevalidator prüfen; Run-ID, acceptedArtifacts und Metadaten abgeschlossener Routing-Phasen erhalten.
- [X] T225 Bestätigen, dass alle 225 Implementierungsaufgaben abgeschlossen oder durch einen expliziten fail-closed Stop blockiert sind, neun fachliche Markdown-Projektionen plus `pr-evidence.md` vollständig sind und alle 37 Zeilen sowie exakt 46 kanonische Fixtures erfasst sind; anschließend den exakten Kandidaten an die autorisierte autonome Commit-/Push-/PR-/Review-/Merge-/Sync-Delivery übergeben, ohne ein Folgefeature zu starten.

## Abhängigkeiten und Ausführungsreihenfolge / Dependencies and execution order

### Phasenabhängigkeiten / Phase dependencies

- Phase 1 blockiert jede Schreibarbeit: Hash-, State-, Portfolio-, Scope- und Analyze-Konvergenz müssen zuerst bestehen.
- Phase 2 hängt von Phase 1 ab und muss vollständig vor jedem Test-/Validator-Edit abgeschlossen sein.
- Phase 3 hängt von Phase 2 ab; EX036 muss semantisch rot und vollständig grün sein, bevor Validatorbreite oder andere Portfoliozeilen folgen.
- Phase 4 hängt vom grünen EX036-Slice ab und liefert die deterministische Integritätsgrenze für alle späteren Daten.
- Phase 5 hängt von Phase 4 ab und läuft strikt Wave 1 → Wave 2 → Wave 3 → Wave 4 → Wave 5 → EX037; EX036 bleibt der bereits akzeptierte Wave-6-Slice.
- Phase 6 beginnt erst nach 37/37 Broad Review und friert Findings vor jeder Intake-Ausgabe ein.
- Phase 7 hängt vom Finding-Freeze ab; Intakes werden erst aus finalen nicht leeren Gruppen erzeugt, der Closure immer zuletzt.
- Phase 8 hängt von finalem Audit/Handoff ab und schließt Governance triggerbasiert, ohne Produktremediation.
- Phase 9 beginnt erst nach Governance-Abschluss und führt die lokale Validierungsleiter in der angegebenen Reihenfolge aus.
- Phase 10 hängt von allen lokalen Implementierungs-/Validierungsgates ab und endet beim geprüften Delivery-Kandidaten; Remote Delivery und die getrennte Retrospektive folgen außerhalb der Implementierungs-Taskliste.

### Sichere Stop-Grenzen / Safe stop boundaries

- Sichere Stopps liegen nach T014, T028, T043, T095, jedem Wave-Checkpoint T100/T112/T118/T124/T135/T137, T154, T167, T209 und T224.
- `PausedByUser` benötigt explizites Resume. Unterbrechung oder unbekanntes Operationsergebnis setzt `NeedsRevalidation`; Hashes, HEAD, Diff, Scope, Routing, Counter und letzter Gatezustand werden vor Fortsetzung geprüft.
- Portfolio-Drift, ProductDecision, nicht reproduzierbares Finding, unklare Ownership, Owner-Zyklus, unerlaubter Pfaddiff oder nicht behebbare Evidence-/Security-/Validierungsintegrität stoppt hart.

### Parallelität / Parallelism

Keine Aufgabe trägt `[P]`. Auch leseseitig unabhängige Reviews werden in diesem autorisierten Lauf seriell abgeschlossen, weil Dataset, Projektionen, Evidence, Version, Statistik, Intake-Ausgaben und State gemeinsame Single-Writer-Flächen sind.

## Anforderungszuordnung / Requirement traceability

### Funktionale Anforderungen / Functional requirements

| Anforderungen | Aufgaben |
|---|---|
| FR-001, FR-002 | T003–T004, T026 |
| FR-003, FR-004 | T005, T018, T054–T059, T096–T142 |
| FR-005, FR-006, FR-007, FR-008 | T016, T019, T066–T071, T096–T142 |
| FR-009, FR-010, FR-011, FR-012, FR-013 | T004, T026, T038, T060–T065, T096–T139 |
| FR-014, FR-015, FR-016 | T020, T039, T066–T071, T096–T151 |
| FR-017, FR-018, FR-019, FR-020, FR-021 | T021, T029, T039–T043, T096–T151 |
| FR-022, FR-023, FR-024, FR-025, FR-026 | T022, T037–T043, T096–T142, T171, T210–T215 |
| FR-027, FR-028, FR-029, FR-030, FR-031, FR-032 | T023, T072–T077, T143–T154 |
| FR-033, FR-034, FR-035, FR-036 | T024, T078–T083, T155–T167 |
| FR-037, FR-038 | T015–T028, T139, T222 |
| FR-039, FR-040 | T007, T029–T095, T188–T191, T216, T220 |
| FR-041 | T008–T014, T138–T142, T152–T154, T165–T167, T188–T209, T218–T220 |
| FR-042 | T210–T214, T217 |
| FR-043 | T185, T191–T194, T216–T217 |
| FR-044 | T032, T034, T041, T045, T049, T052, T055, T058, T061, T064, T067, T070, T073, T076, T079, T082, T085, T088, T093, T140, T152, T165, T196, T199, T202, T206 |
| FR-045, FR-046 | T001–T002, T010–T014, T043, T142, T154, T167, T174, T178–T179, T218, T224–T225 |
| FR-047 | T223 |
| FR-048, FR-049 | T011, T164, T209, T217–T225 |

### Verfassungsanforderungen / Constitution requirements

| Anforderungen | Aufgaben |
|---|---|
| CR-001, CR-002 | T007, T029–T095, T180, T188–T215 |
| CR-003 | T168, T180, T184, T191–T209 |
| CR-004, CR-005, CR-006, CR-007, CR-008 | T181, T184, T217–T218 |
| CR-009, CR-010, CR-011, CR-012 | T169–T170, T182–T184, T218 |
| CR-013, CR-014, CR-015 | T022, T031, T092, T171, T183–T184, T210–T217 |
| CR-016 | T172, T184, T187 |
| CR-017 | T173–T174, T185, T193–T194, T217–T218 |
| CR-018 | T210–T214 |
| CR-019 | T027, T168–T187, T209, T217–T218 |

### Erfolgskriterien / Success criteria

| Kriterien | Aufgaben |
|---|---|
| SC-001 | T005, T018, T054–T059, T096–T142 |
| SC-002 | T019, T066–T071, T096–T142 |
| SC-003 | T017, T026, T060–T065, T138–T142 |
| SC-004 | T020–T022, T029–T043, T096–T142 |
| SC-005, SC-006 | T072–T077, T143–T154 |
| SC-007 | T078–T083, T155–T167 |
| SC-008 | T007, T188–T191, T216, T220 |
| SC-009 | T015–T025, T171, T210–T215 |
| SC-010 | T006, T013–T014, T188–T220 |
| SC-011 | T027, T168–T187, T209, T217–T218 |
| SC-012 | T001–T002, T014, T043, T142, T154, T167, T224 |
| SC-013 | T024–T025, T161–T167, T209, T217–T225 |

### Intake-Akzeptanzkriterien / Intake acceptance criteria

Die Nummern `IAC-01`–`IAC-11` entsprechen `Lastenheft 15`, Abschnitt 14, Punkte 1–11.

| Kriterium | Aufgaben |
|---|---|
| IAC-01 – alle 25 Originalbeispiele genau einmal | T005, T096–T124, T138–T142 |
| IAC-02 – alle gelieferten Wave-5-/Wave-6-Beispiele genau einmal | T038–T043, T125–T135, T138–T142 |
| IAC-03 – A11yFramework genau einmal als SupplementalControl | T018, T054–T059, T136–T142 |
| IAC-04 – vollständige Zeile und genau eine Hauptentscheidung | T019, T066–T071, T096–T142 |
| IAC-05 – jede Gap zu Finding oder ProductDecision-Stop | T143–T154 |
| IAC-06 – deduplizierte Findings mit genau einem Primary Owner | T072–T077, T145–T154 |
| IAC-07 – nur nicht leere Owner-Gruppen erzeugen Intakes | T078–T083, T155–T160, T163–T167 |
| IAC-08 – genau ein unabhängiger Closure zuletzt | T161–T167 |
| IAC-09 – null Runtime/API/Dependency/Beispiel/historische Source-Änderung | T007, T188–T191, T216, T220 |
| IAC-10 – idiomatisches C# bleibt ohne reproduzierbare Lücke erhalten | T096–T151, T180 |
| IAC-11 – nach autorisiertem Merge lokales main gleich origin/main | T011, T218, T221–T225 plus autonome Exact-Head-/Merge-/Sync-Delivery nach T225 |

## Umsetzungsstrategie / Implementation strategy

1. **MVP / Vertikalschnitt**: Phasen 1–3 liefern ein evidence-first, semantisch rot/grün bewiesenes `EX036` ohne Produktänderung.
2. **Integritätsbasis**: Phase 4 macht alle 46 kontrollierten Fehlerklassen deterministisch grün.
3. **Inkrementeller Portfolio-Review**: Phase 5 liefert jede Wave als eigenen sicheren Review-Slice und endet bei exakt 37/37.
4. **Disposition und Übergabe**: Phasen 6–7 frieren nur reale Findings ein und erzeugen ausschließlich notwendige, unnummerierte Folgeintakes plus einen Closure.
5. **Konvergenz und Delivery**: Phasen 8–10 schließen Governance, Regression, Coverage, Dokumentation/A11Y, Statistik und den Delivery-Kandidaten; danach liefert `MergeAndSync` genau Feature 038, ohne Folgefeature-Autorität.
