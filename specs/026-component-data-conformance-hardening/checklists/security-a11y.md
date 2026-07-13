# Security and Accessibility Requirements Checklist

**Purpose**: Review whether Feature 026 defines fail-closed data boundaries,
keyboard-complete rejection, and accessible documentation requirements.
**Created**: 2026-07-13
**Feature**: [spec.md](../spec.md)

## Validation and State Safety

- [x] CHK001 Are affirmative completion, cancel, unrelated commands, invalid children, and first-rejection behavior all specified? [Coverage, Spec §FR-001–FR-004]
  - Durchführungshinweis: Die Dialog-Szenarien als positive, alternate, rejection und cancel Matrix lesen und jede fehlende Zustandsaussage markieren.
- [x] CHK002 Does rejection preserve dialog, text, selection, cursor, prior valid state, focus, and visible error evidence? [State Integrity, Spec §FR-003, §FR-007]
  - Durchführungshinweis: Alle genannten Zustandsdimensionen in Requirements und Success Criteria abgleichen; implizite Begriffe nicht als Proof akzeptieren.
- [x] CHK003 Are validator phases explicit enough to prevent acceptance or focus-loss bypass? [Security, Spec §FR-005–FR-007]
  - Durchführungshinweis: Edit, Fokusverlust und Acceptance einzeln gegen User Story, FR und SC prüfen.

## File Boundary

- [x] CHK004 Are navigation, wildcard/filter, existing/missing Open, new/existing Save, invalid path, and Cancel requirements all present? [Coverage, Spec §FR-008–FR-010]
  - Durchführungshinweis: Die acht Modi aus dem Lastenheft wortgleich gegen FR-008 und SC-005 zählen.
- [x] CHK005 Is overwrite a separate caller decision, with no hidden open/write/delete/move behavior at the dialog boundary? [Safety, Spec §FR-009]
  - Durchführungshinweis: File-Result-, Acceptance- und Assumption-Text auf implizite Dateioperationen oder TOCTOU-Versprechen prüfen.
- [x] CHK006 Are tests confined to source fixtures and managed temporary directories, excluding arbitrary user and historical data? [Privacy, Spec §FR-010]
  - Durchführungshinweis: Test- und Scope-Abschnitte auf Datenquelle, Ownership, Cleanup und read-only Grenzen prüfen.

## Resource Boundary

- [x] CHK007 Is the resource contract closed, versioned, case-sensitive, allowlisted, ownership-aware, and command-validating? [Security, Spec §FR-011–FR-014]
  - Durchführungshinweis: Jede Vertrauenseigenschaft einzeln markieren und gegen die Key- sowie Reconstruction-Entitäten prüfen.
- [x] CHK008 Are unknown type/version, truncation, trailing data, duplicate key, invalid command, graph, size, and depth failures specified as atomic? [Malformed Input, Spec §FR-013]
  - Durchführungshinweis: Die negative Matrix aus Intake und SC-006 zählen; Partial-State-Verbot bei jedem Fehler sicherstellen.
- [x] CHK009 Are reflection activation, assembly scanning, polymorphic deserialization, and persisted type execution explicitly prohibited? [Attack Surface, Spec §FR-014]
  - Durchführungshinweis: Out-of-Scope und Resource Requirements auf identische Trust-Grenzen und fehlende Hintertüren prüfen.

## Accessibility and Documentation

- [x] CHK010 Are all rejection and validation flows keyboard complete with deterministic focus and text-first feedback? [A11Y, Spec §FR-022]
  - Durchführungshinweis: Pointer-only Pfade ausschließen und Fokus-, Fehlertext- sowie Assistive-Evidence in Stories, FR und SC abgleichen.
- [x] CHK011 Are DE-first/EN-second XML and learner-facing documentation plus DocFX/Axe/text-first triggers explicit? [Documentation, Spec §FR-021, §CR-002–CR-003]
  - Durchführungshinweis: Öffentliche API-, XML-, Guide- und Navigationsänderungen jeweils dem vollständigen Dokumentationsgate zuordnen.
- [x] CHK012 Are didactic inline comments selective and reason-focused rather than a blanket commenting requirement? [Maintainability, Spec §FR-023]
  - Durchführungshinweis: Kommentarregel auf nicht triviale Logik, Warum/Trade-off/Randbedingung und moderates bestehendes Agent-Guidance-Modell prüfen.

## Review Result

- [x] CHK013 Every review instruction was executed; no material security, data-safety, A11Y, or documentation ambiguity remains before planning. [Readiness]
