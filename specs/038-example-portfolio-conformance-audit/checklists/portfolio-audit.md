# Requirements-Quality Checklist: Example Portfolio Audit

**Purpose / Zweck**: Formal reviewer checklist for the completeness, clarity,
consistency, measurability, and boundary quality of the Feature 038 audit
requirements; this is not an implementation test plan. / Formelle
Reviewer-Checkliste für Vollständigkeit, Klarheit, Konsistenz, Messbarkeit und
Grenzen der Audit-Anforderungen von Feature 038; dies ist kein
Implementierungstestplan.
**Created / Erstellt**: 2026-08-09
**Feature**: [spec.md](../spec.md)
**Assessment / Bewertung**: Assessed against the clarified specification after
the focused requirements-writing repair in FR-014, FR-028, and FR-033. Only
explicitly evidenced passes are checked. / Gegen die geklärte Spezifikation
nach der fokussierten Anforderungsreparatur in FR-014, FR-028 und FR-033
bewertet. Nur ausdrücklich belegte Punkte sind abgehakt.

## Portfolio Baseline and Matrix / Portfolio-Grundmenge und Matrix

**Optimal review / Optimale Prüfung**: Recalculate the totals from the named
lists, compare every required field and vocabulary literally, and treat any
missing, duplicate, unknown, or silently absorbed entry as a requirements gap.
/ Summen aus den Namenslisten neu bilden, alle Pflichtfelder und Vokabulare
wörtlich vergleichen und jeden fehlenden, doppelten, unbekannten oder still
aufgenommenen Eintrag als Anforderungslücke behandeln.

- [x] CHK001 Sind exakt 37 Einträge mit der Aufteilung 25 Original-, 10 Wave-5-, 1 Wave-6-Beispiel und genau 1 `A11yFramework` gefordert und alle 37 Namen einzeln genannt? / Are exactly 37 entries required with the 25/10/1/1 split and all 37 names individually listed? [Completeness, Spec §Verbindliche Portfolio-Grundmenge, FR-003, SC-001]
- [x] CHK002 Sind Rolle und Sondergrenze von `A11yFramework` eindeutig als `SupplementalControl` mit historischer Relation `N/A`, aber fortbestehender Lern-, A11Y- und Proof-Prüfung definiert? / Are the role and special boundary of `A11yFramework` unambiguously defined? [Clarity, Spec §Klärungen, §Binding Portfolio Baseline, FR-004]
- [x] CHK003 Ist späterer Portfolio-Drift als blockierend definiert, ohne neue, fehlende, doppelte oder falsch zugeordnete Projekte still in die Grundmenge aufzunehmen? / Is later portfolio drift defined as blocking rather than silently changing the baseline? [Coverage, Spec §Binding Portfolio Baseline, §Edge Cases]
- [x] CHK004 Sind Pflichtfelder, genau eine `PortfolioRole`, genau eine `PrimaryDisposition` und genau ein erlaubter Wert je Prüfdimension vollständig und widerspruchsfrei festgelegt? / Are mandatory fields and the exactly-one role, disposition, and dimensional-status rules complete and consistent? [Completeness, Consistency, FR-004–FR-008, SC-002]

## Source Hierarchy, Historical Purpose, and Learning Value / Quellenhierarchie, historischer Zweck und Lernwert

**Optimal review / Optimale Prüfung**: Read the hierarchy top-down, ensure
lower-ranked evidence cannot become product authority, and require a per-entry
trace from stable source IDs to a newly worded intent and learning goal. / Die
Hierarchie von oben nach unten lesen, nachgeordnete Evidence nicht zur
Produktnorm werden lassen und je Eintrag eine Relation von stabilen Source-IDs
zu neu formuliertem Zweck und Lernziel verlangen.

- [x] CHK005 Ist die sechsstufige Quellenhierarchie vollständig geordnet und sind akzeptierte Pins, ausgeschlossene bewegliche Upstreams und die Nicht-Überschreibungsregel ausdrücklich festgelegt? / Is the complete six-level hierarchy ordered with fixed pins, moving-upstream exclusions, and non-override semantics? [Completeness, Spec §Bindende Quellenhierarchie, FR-009–FR-010]
- [x] CHK006 Sind die historischen Autoritäten für Waves 1–4 (`tv203s/` plus Borland-Dokumentation) und Waves 5–6 (`TVDEMOS/`, `TVFM/`) sowie die nötige Einbeziehung zusammengehöriger Implementierungs- und Headerdateien beschrieben? / Are historical authorities and related implementation/header evidence specified? [Coverage, Spec §Bindende Quellenhierarchie, §Edge Cases]
- [x] CHK007 Ist akzeptierte TuiVision-Produktsemantik gegenüber Free Vision, Terminal.GUI und magiblot/tvision als alleinige aktuelle Norm abgegrenzt und sind Vergleichsquellen nur sekundäre Meinungen bei vergleichbarer Verantwortung? / Is accepted TuiVision semantics authoritative while comparison sources remain bounded secondary opinions? [Clarity, Consistency, Spec §Bindende Quellenhierarchie, FR-012–FR-013]
- [x] CHK008 Sind je Beispiel ein in eigenen Worten formulierter historischer Demonstrationszweck und ein nachvollziehbares Lernziel verlangt, ohne Kopie, mechanische Übersetzung oder Vendorisierung? / Are per-example historical purpose and learning value required in original wording without copying or mechanical translation? [Completeness, Spec §User Story 2, FR-011, SC-003]
- [x] CHK009 Ist eindeutig, dass moderne idiomatische Abweichungen erhalten bleiben und Unterschiede in Aussehen, Layout, API, Vererbung, Speicherlayout oder Quelltext allein weder Gap noch Finding begründen? / Is it clear that idiomatic modernization remains and structural or visual differences alone are not findings? [Clarity, Spec §User Story 2, FR-013, FR-031]

## Framework Use, Visible Interaction, and Real-Path Proof / Framework-Nutzung, sichtbare Bedienung und Real-Path-Proof

**Optimal review / Optimale Prüfung**: Require one defined framework decision
per entry, then trace every interactive claim from the normal entry point to
visible feedback and the named primary-proof boundary; helper-only language is
insufficient. / Je Eintrag genau eine definierte Framework-Entscheidung
verlangen und jede Interaktionsaussage vom normalen Einstieg bis zur sichtbaren
Rückmeldung und benannten Primary-Proof-Grenze verfolgen; Helper-only-Aussagen
reichen nicht.

- [x] CHK010 Sind die vier Framework-Entscheidungen vollständig, gegenseitig abgrenzbar und mit einer eindeutigen Bedeutung für vorhandene Nutzung, kleinen Fix, bewusste Abweichung oder gesonderte Härtung definiert? / Are all four framework decisions complete, mutually distinguishable, and defined? [Clarity, Spec FR-014]
- [x] CHK011 Ist festgelegt, dass wiederverwendbare Verantwortung nicht dauerhaft lokale Sonderlogik bleiben darf und `SmallFrameworkFix` beziehungsweise `FollowUpHardening` im Audit nur Findings, keine Sofortimplementierung, auslösen? / Are reusable responsibility and deferred framework remediation boundaries explicit? [Consistency, Spec FR-015–FR-016]
- [x] CHK012 Sind für interaktiv gedachte Beispiele sichtbarer Startzweck, Hauptbedienung und sichtbare Rückmeldung über die tatsächlichen Menü-, StatusLine-, Tastatur-, Command- oder begründeten Mauspfade vollständig gefordert? / Are visible first-screen purpose, primary interaction, and visible feedback requirements complete? [Completeness, Spec §User Story 3, FR-017]
- [x] CHK013 Sind tastaturerreichbare `Help -> Description`-Erklärung und Tastaturfallback für relevante Mausoperationen ohne unbestimmte Ausnahme festgelegt? / Are keyboard-reachable description and mouse-operation fallback requirements specified? [Coverage, Spec FR-018]
- [x] CHK014 Ist Primary Proof objektiv als `app.Run()` oder äquivalenter realer Application-Loop plus konkreter Zustand, View-Identität und sichtbarer Buffer-/Cell-Nachweis definiert? / Is primary proof objectively defined by the real loop and all three proof layers? [Measurability, Spec FR-019, SC-004]
- [x] CHK015 Sind direkte Helper vollständig klassifiziert und ist eine Primary-Proof-Einstufung nur nach dem bestehenden, begründungspflichtigen Helper-Vertrag zulässig? / Are helper classifications and the exceptional primary-proof boundary explicit? [Clarity, Spec FR-020, §Edge Cases]
- [x] CHK016 Sind risikoproportionale Negativ-, Rejection-, Safe-Close-, Fallback- und Small-Terminal-Anforderungen für die benannten Hochrisikoverantwortungen abgedeckt? / Are risk-proportional negative and fallback scenario requirements covered for the named responsibilities? [Coverage, Spec §User Story 3, FR-021]

## Documentation, Accessibility, and Platforms / Dokumentation, Accessibility und Plattformen

**Optimal review / Optimale Prüfung**: Inspect requirements by reader path,
not by visual presentation: guide, keyboard, assistive text, small-terminal,
and operating-system boundaries each need evidence or a justified `N/A` with a
trigger. / Anforderungen nach Leserpfad statt Optik prüfen: Guide, Tastatur,
assistiver Text, Small Terminal und Betriebssystemgrenzen benötigen jeweils
Evidence oder begründetes `N/A` mit Trigger.

- [x] CHK017 Sind je Beispiel Guide-Pflicht und Übereinstimmung von Start, Bedienung, Lernziel, Fehlerbildern, Übungen, Quellen und Tests vollständig beschrieben? / Are per-example guide content and consistency requirements complete? [Completeness, Spec FR-022]
- [x] CHK018 Sind German-first/English-second, ungefähr CEFR-B2, Zielgruppe erstes Ausbildungsjahr und text-first Verständlichkeit als prüfbare Inhaltsanforderungen festgelegt? / Are bilingual, CEFR-B2, learner-level, and text-first content requirements specified? [Clarity, Spec §Zielgruppe und Lernkontext, FR-023–FR-024, SC-009]
- [x] CHK019 Decken die Anforderungen Fokus, Shortcuts, High Contrast, textbasierte Rückmeldung sowie Tastatur-, Screenreader-, Braille- und Textbrowserpfade ohne farb- oder layoutgebundene Kernaussage ab? / Do accessibility requirements cover all named interaction and assistive paths? [Coverage, Spec FR-024–FR-025, CR-013]
- [x] CHK020 Sind kleine Terminals, Unicode, Charset, Breite, Farbe, Terminalfähigkeiten sowie Windows-, macOS-, Linux- und begründete WSL-Grenzen mit Evidence oder ehrlichem Fallback gefordert? / Are terminal and platform dimensions fully specified with evidence or honest fallback? [Completeness, Spec §Edge Cases, FR-025, Documentation Impact]
- [x] CHK021 Ist für Datei- und Persistenz-Proof die Nutzung kontrollierter Fixtures oder test-eigener temporärer Verzeichnisse verbindlich und beliebiger Nutzerdatenzugriff ausgeschlossen? / Is controlled test data required for file and persistence proof? [Security, Coverage, Spec FR-026, §Edge Cases]

## Finding Integrity, Deduplication, and Ownership / Finding-Integrität, Deduplizierung und Ownership

**Optimal review / Optimale Prüfung**: For each allowed `Gap`, follow the
relation to one complete `EF` record or a blocking decision; group by root
cause before assigning exactly one bounded Primary Owner. / Für jedes erlaubte
`Gap` die Relation zu genau einem vollständigen `EF`-Datensatz oder einer
blockierenden Entscheidung verfolgen; vor der Zuordnung genau eines
abgegrenzten Primary Owner nach Ursache gruppieren.

- [x] CHK022 Sind lückenlose `EF001+`-Kennzeichnung und alle Beobachtungs-, Reproduktions-, Intent-, Verhaltens-, Relations-, Risiko-, Proof-, Impact-, Review- und Re-Evaluationsfelder vollständig genannt? / Are contiguous IDs and every required finding field completely specified? [Completeness, Spec FR-027]
- [x] CHK023 Ist für jede Gap-Dimension genau die Relation zu einem Finding oder einem expliziten blockierenden `ProductDecision` gefordert? / Is every gap required to map to a finding or explicit blocking product decision? [Traceability, Spec FR-030, SC-005]
- [x] CHK024 Ist ursachenbezogene Deduplizierung über einen `DeduplicationKey` zu genau einem Finding mit allen betroffenen `ExampleIds` eindeutig gefordert? / Is root-cause deduplication into one finding with all affected examples unambiguous? [Clarity, Spec §User Story 4, FR-029, SC-006]
- [x] CHK025 Sind die vier Primary-Owner-Werte mit exklusiven Verantwortungsgrenzen definiert und klar vom administrativen Feld `Owner` getrennt? / Are Primary Owner boundaries defined and distinct from the administrative `Owner` field? [Clarity, Spec FR-028]
- [x] CHK026 Ist genau ein Primary Owner pro Finding verlangt, während Querschnittswirkungen nur als sekundäre Auswirkungen dokumentiert werden? / Is single primary ownership required while cross-cutting effects remain secondary? [Consistency, Spec FR-028]
- [x] CHK027 Sind Beobachtungen ohne reproduzierbare TuiVision-Lücke und persönliche oder strukturelle Präferenzen ausdrücklich von Findings und Follow-ups ausgeschlossen? / Are non-reproducible observations and preferences excluded from findings and follow-ups? [Boundary, Spec §User Story 4, FR-031]
- [x] CHK028 Sind `ProductDecision`, nicht reproduzierbares Finding, unklares Inventar oder Ownership und nicht behebbare Evidence-, Security- oder Validierungsintegrität als fail-closed Stopps definiert? / Are all named product-decision and integrity conditions specified as fail-closed stops? [Exception Flow, Spec FR-032, §Edge Cases]

## Remediation Handoff and Independent Closure / Remediation-Handoff und unabhängiger Closure

**Optimal review / Optimale Prüfung**: Derive owner groups only from final
deduplicated findings, reject empty outputs and cycles, and keep audit,
remediation, and final closure as three distinct authority boundaries. / Owner-
Gruppen nur aus finalen deduplizierten Findings ableiten, leere Ausgaben und
Zyklen ablehnen und Audit, Remediation sowie finalen Closure als drei getrennte
Autoritätsgrenzen behandeln.

- [x] CHK029 Ist „nicht leere Owner-Gruppe“ messbar als mindestens ein finales, dedupliziertes und per `PrimaryOwner` zugeordnetes Finding definiert? / Is a non-empty owner group measurably defined? [Measurability, Spec FR-033]
- [x] CHK030 Ist genau ein unnummeriertes Remediation-Lastenheft je nicht leerer Owner-Gruppe mit Finding-IDs, Abhängigkeiten und erforderlichen Red-/Real-Path-Green-Proofs verlangt, während leere Gruppen vollständig unterdrückt werden? / Is exactly one complete intake required per non-empty group while empty groups are suppressed? [Completeness, Spec FR-033–FR-034, SC-007]
- [x] CHK031 Sind dependency-geordnete, azyklische Folgearbeit, das Verbot vorweggenommener Feature-Nummern und das Verbot eines automatischen Folgefeature-Starts konsistent beschrieben? / Are ordering, no-preassigned-number, and no-auto-start boundaries consistent? [Consistency, Spec §User Story 5, FR-034, FR-036]
- [x] CHK032 Ist genau ein unabhängiger Portfolio-Closure nach allen nicht leeren Remediation-Gruppen verlangt und ausschließlich dieser zur späteren vollständigen Konformitäts- und Lernreifeaussage berechtigt? / Is exactly one final independent closure required and solely authorized to declare conformance? [Clarity, Spec FR-035, SC-013]

## Governance, Local Authority, and Protected Roots / Governance, lokale Autorität und geschützte Wurzeln

**Optimal review / Optimale Prüfung**: Compare every preset and gate with its
applicability, rationale, evidence, owner/reviewer, risk, trigger, and authority
limit; then compare the allowed audit surfaces against the exact protected-root
deny-list. / Jedes Preset und Gate mit Anwendbarkeit, Begründung, Evidence,
Owner/Reviewer, Risiko, Trigger und Autoritätsgrenze vergleichen; danach die
erlaubten Auditflächen gegen die exakte Deny-Liste geschützter Wurzeln prüfen.

- [x] CHK033 Sind alle zwölf installierten Presets mit Version, Priorität, `Applicable`/`N/A`-Entscheidung und proportionaler Feature-038-Grenze dokumentiert? / Are all twelve presets documented with version, priority, applicability, and proportional boundary? [Completeness, Spec §Aktuelle Preset-Anwendbarkeit]
- [x] CHK034 Ist für jeden Governance-Checkpoint die Trennung von Applicability und Umsetzung samt Begründung, Evidence, Owner, Reviewer, Restrisiko, Re-Evaluationsauslöser und Follow-up gefordert? / Is the complete governance decision record specified? [Completeness, Spec CR-003–CR-019, SC-011]
- [x] CHK035 Ist `MergeAndSync` eindeutig auf genau Feature 038 begrenzt, mit exakter Delivery-, enger Bypass-, Provider-/Upstream- und Folgefeature-Grenze sowie fail-closed State-/Resume-Regeln? / Is MergeAndSync authority precisely bounded with exact delivery, narrow bypass, provider, upstream, follow-up, and fail-closed state rules? [Authority, Spec §Autonomous-Run Applicability, FR-045–FR-046]
- [x] CHK036 Sind `src/`, `examples/`, `tv203s/`, `TVDEMOS/`, `TVFM/`, externe Quellen, Public API und Dependencies als geschützte Änderungsflächen genannt und Teständerungen auf Audit-Integritätsvalidatoren begrenzt? / Are all protected roots and non-root surfaces explicit, with tests limited to audit validators? [Boundary, Spec FR-039–FR-040, §Out of Scope]
- [x] CHK037 Sind alle Evidence-Familien, JSON-/Markdown-Rollen, bidirektionale Relationen sowie dokumentationsabhängige DocFX-, Axe-, Tastatur- und UTF-8-Lynx-Trigger vollständig definiert? / Are evidence families, formats, relation integrity, and documentation triggers complete? [Completeness, Spec FR-037–FR-043, Documentation Impact]
- [x] CHK038 Trennen die Anforderungen lokale Gates, Remote Exact-Head, Merge/Sync und den nur bei Bedarf zulässigen kausalen Closeout, ohne Remote-Erfolg oder vollständige Konformität vorwegzunehmen? / Do requirements separate local gates, exact-head delivery, merge/sync, and conditional causal closeout without premature success or conformance claims? [Consistency, Spec FR-048–FR-049, §Acceptance Gates]

## Notes / Hinweise

- Current assessment: 38 of 38 requirements-quality items have explicit
  evidence in `spec.md`; no implementation behavior was assessed. / Aktuelle
  Bewertung: 38 von 38 Anforderungsqualitäts-Punkten sind in `spec.md`
  ausdrücklich belegt; Implementierungsverhalten wurde nicht bewertet.
- The existing [requirements.md](requirements.md) remains unchanged and is a
  separate accepted readiness checklist. / Die bestehende
  [requirements.md](requirements.md) bleibt unverändert und ist eine separate
  akzeptierte Readiness-Checkliste.
