# Planungsqualitätscheckliste / Plan Quality Checklist: GSDB-Spec-Kit-Intensivprüfung

**Zweck / Purpose**: Anforderungen an die Qualität und Umsetzungsreife der Feature-046-Planung vor `speckit.tasks` prüfen. / Check requirements for Feature 046 planning quality and implementation readiness before `speckit.tasks`.

**Datum / Date**: 2026-08-30

**Phase**: `plan-review-1`

**Gegenstand / Subject**: Planungsqualität, nicht Umsetzungsevidence. / Planning quality, not implementation evidence.

## Kanonischer Umfang / Canonical Scope

- [x] **PQ-001** Der Plan verlangt exakt 157 eindeutige `CL-XX-NN`-Kontrollen und genau eine Bewertungszeile je ID. / The plan requires exactly 157 unique `CL-XX-NN` controls and exactly one assessment row per ID.
- [x] **PQ-002** Die Kapitelverteilung `12/13/15/10/13/11/12/13/17/17/12/12` ist eine separat geprüfte Invariante derselben Kontrollmenge. / The chapter partition `12/13/15/10/13/11/12/13/17/17/12/12` is a separately checked invariant of the same control set.
- [x] **PQ-003** Alle Nicht-Kontroll-Inventare werden aus dokumentierten Snapshot-Regeln abgeleitet; beobachtete 37 Quellen und 12 Presets sind keine Validatorliterale. / Every non-control inventory is derived from documented snapshot rules; the observed 37 sources and 12 presets are not validator literals.
- [x] **PQ-004** Ausschließlich `Applicable`, `AlreadySatisfied`, `N/A`, `Open` und `FollowUp` sind als Feature-Dispositionen erlaubt. / Only `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, and `FollowUp` are allowed feature dispositions.
- [x] **PQ-005** Jede Bewertungsart besitzt stabile Identität, Titel/Bezeichnung, Quellenbezug, Disposition, Begründung, Evidence oder Beweislücke, Owner, Follow-up, Revalidierungstrigger und Restrisiko. / Every assessment type has stable identity, title/label, source reference, disposition, rationale, evidence or evidence gap, owner, follow-up, revalidation trigger, and residual risk.

## Dynamische Inventare und Evidenz / Dynamic Inventories and Evidence

- [x] **PQ-006** Sprachprofile werden aus Regelquellen, Verfassungs-/Preset-Pflichten und tracked Dateityp-/Shebang-Detektoren geschlossen; unbekannte codeartige Treffer schlagen fail-closed fehl. / Language profiles are closed from rule sources, constitution/preset obligations, and tracked file-type/shebang detectors; unknown code-like matches fail closed.
- [x] **PQ-007** Jeder aktivierte Registry-Eintrag, seine aktuelle Version, Priorität und Hashbindung wird dynamisch geprüft. / Every enabled registry entry, its current version, priority, and hash binding is checked dynamically.
- [x] **PQ-008** Agentenflächen besitzen ein eigenes referenziell geschlossenes Inventar aus Level-2-Verfassung, Registry-Agentenschlüsseln und tracked Guidance-/Command-/Prompt-/Skill-/Agent-Pfaden. / Agent surfaces have their own referentially closed inventory from the Level-2 constitution, registry agent keys, and tracked guidance/command/prompt/skill/agent paths.
- [x] **PQ-009** Governance-Checkpoints schließen beide Verfassungen, Presets, Agenten-Parität, Modell-Routing, Intake/Serie, Version, Delivery und Run-State ohne feste Anzahl. / Governance checkpoints close both constitutions, presets, agent parity, model routing, intake/series, version, delivery, and run state without a fixed count.
- [x] **PQ-010** Evidenzfamilien entstehen aus dem Pflichtdomänenkatalog plus zusätzlichen aktiven Preset-/Governance-Pflichten; fehlende Pflichtfamilien werden abgelehnt. / Evidence families derive from the mandatory domain catalog plus additional active preset/governance obligations; missing mandatory families are rejected.
- [x] **PQ-011** Positive Aussagen werden unabhängig gegen aktuelle, direkte und snapshotgebundene Feature-046-Evidence revalidiert; Features 016/044/045 sind nie allein ausreichend. / Positive statements are independently revalidated against current, direct, snapshot-bound Feature 046 evidence; Features 016/044/045 are never sufficient alone.
- [x] **PQ-012** Lokale, Remote-, Human-, Provider- und Rechts-/Organisationsbelege bleiben getrennte Nachweisgrenzen. / Local, remote, human, provider, and legal/organizational evidence remain separate proof boundaries.

## Validator, Projektionen und Negativpfade / Validator, Projections, and Negative Paths

- [x] **PQ-013** Ein repräsentativer Red/Green-Schnitt geht dem vollständigen Datensatz voraus. / A representative red/green slice precedes the complete dataset.
- [x] **PQ-014** Der Validator prüft Schema, Pflichtfelder, Referenzen, Inventarabschluss, Sortierung, Hashing, Summen und Projektionen deterministisch. / The validator deterministically checks schema, required fields, references, inventory closure, ordering, hashing, summaries, and projections.
- [x] **PQ-015** Negativ-Fixtures decken Kontrollen, Kapitelverteilung, Dispositionen, Pflichtfelder, Evidence, Proof Boundaries, Quellen, Presets, Agentenflächen, Sprachen, Governance, Evidenzfamilien, Summen, Sortierung, Projektionen und Routing-Bindungen ab. / Negative fixtures cover controls, chapter partition, dispositions, required fields, evidence, proof boundaries, sources, presets, agent surfaces, languages, governance, evidence families, summaries, ordering, projections, and routing bindings.
- [x] **PQ-016** Maschinenlesbare und Markdown-Projektionen stammen allein aus dem kanonischen JSON und werden bytegenau verglichen. / Machine-readable and Markdown projections derive only from canonical JSON and are compared byte for byte.
- [x] **PQ-017** Projektion-Payload-Hashes schließen ihr eigenes Hashfeld aus; der Hashgraph bleibt azyklisch. / Projection payload hashes exclude their own hash field; the hash graph remains acyclic.

## Governance, Gates und Lieferung / Governance, Gates, and Delivery

- [x] **PQ-018** Intake-/Serienartefakte nutzen `acceptedArtifacts`; Feature-Artefakte nutzen Routing-Payloads oder die gebundene post-remediation Review-Attestation. / Intake/series artifacts use `acceptedArtifacts`; feature artifacts use routing payloads or the bound post-remediation review attestation.
- [x] **PQ-019** Der Scope-Firewall erlaubt nur Planung, datierte Evidence, einen test-only Validator samt Fixtures, Navigation, Statistik, Version und kausal spätere Closeout-/Intake-/Serienflächen. / The scope firewall permits only planning, dated evidence, one test-only validator with fixtures, navigation, statistics, version, and causally later closeout/intake/series surfaces.
- [x] **PQ-020** Produkt-, Runtime-, API-, Dependency-, Paket-, Projekt-, Beispiel-, Workflow-, Provider-, Secret-Rotations-, historische Quellen- und Finding-Abhilfeänderungen bleiben verboten. / Product, runtime, API, dependency, package, project, example, workflow, provider, secret-rotation, historical-source, and finding-remediation changes remain prohibited.
- [x] **PQ-021** Der finale Release-/Coverlet-Lauf erfolgt auf einem sauberen committed candidate mit prospektivem Patch und vor Commit erhöhtem Buildzähler; jeder weitere Build/Test startet einen neuen Kandidatenzyklus. / The final Release/Coverlet run executes on a clean committed candidate with prospective patch and pre-commit build increment; every later build/test starts a new candidate cycle.
- [x] **PQ-022** `Version`, `AssemblyVersion` und `FileVersion` bleiben identisch; Patch entspricht nach Commit der tatsächlichen HEAD-Commitzahl. / `Version`, `AssemblyVersion`, and `FileVersion` remain identical; after commit, Patch equals the actual HEAD commit count.
- [x] **PQ-023** Remote- und Review-Gates gelten nur für den exakten Kandidaten-HEAD. / Remote and review gates apply only to the exact candidate HEAD.
- [x] **PQ-024** Human Approval kann höchstens ein nachweislich nicht verfügbares Remote-Gate ersetzen und verlangt vollständiges lokales Grün, null technische Findings/Review-Threads/Scope-Verstöße, allein offene Human Approval sowie Gate, Person, Zeit, Begründung, Grenze und Ablauf. / Human Approval may replace at most one demonstrably unavailable remote gate and requires complete local green status, zero technical findings/review threads/scope violations, Human Approval as the sole open rule, plus gate, person, time, rationale, boundary, and expiry.
- [x] **PQ-025** DocFX, Playwright/axe und Textbrowser folgen dem tatsächlichen DocFX-Input-Trigger; `N/A` benötigt maschinenlesbare aktuelle Begründung. / DocFX, Playwright/axe, and the text browser follow the actual DocFX-input trigger; `N/A` needs a current machine-readable rationale.

## Inklusive und kausale Fertigstellung / Inclusive and Causal Completion

- [x] **PQ-026** Leserartefakte sind deutsch zuerst, englisch danach, ungefähr CEFR B2, semantisch und text-first auf WCAG-2.2-AA-Basis. / Reader artifacts are German first, English second, approximately CEFR B2, semantic, and text-first on a WCAG 2.2 AA baseline.
- [x] **PQ-027** Mergeabhängige Fakten, Intake-Archiv, Serienübergang, finale Statistik und Retrospektive entstehen erst nach Merge und Main-Synchronisierung. / Merge-dependent facts, intake archive, series transition, final statistics, and retrospective are created only after merge and main synchronization.
- [x] **PQ-028** Der Intake-Übergang nutzt den vorhandenen paarigen Rename- und Intake-Sequencing-Ablauf mit erneuter Manifest-/Receipt-/Review-Validierung. / The intake transition uses the existing paired rename and intake-sequencing flow with renewed manifest/receipt/review validation.
- [x] **PQ-029** Statistikprofil 2 bleibt reproduzierbar, chronologisch, ASCII-only und bilingual textalternativ; der Gesamtstatistikblock bleibt der letzte Top-Level-Abschnitt. / Statistics Profile 2 remains reproducible, chronological, ASCII-only, and bilingually text-alternative; the overall statistics block remains the final top-level section.
- [x] **PQ-030** Retrospektive und Closeout enthalten keine nachträgliche Finding-Abhilfe oder Statuskosmetik. / Retrospective and closeout contain no later finding remediation or status cosmetics.

## Konvergenz / Convergence

Alle 30 Planungsqualitätsanforderungen sind nach gezielter Korrektur erfüllt. Offene Critical-, High- oder Medium-Planungsdefekte: `0`. Der Plan ist bereit für `speckit.tasks`; diese Checkliste behauptet keine Implementierungs- oder Delivery-Gate-Erfüllung.

All 30 planning-quality requirements are satisfied after targeted correction. Open Critical, High, or Medium planning defects: `0`. The plan is ready for `speckit.tasks`; this checklist claims no implementation or delivery-gate completion.
