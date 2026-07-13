# Detailed Plan Review: Component and Data Conformance Hardening

**Purpose**: Challenge implementation feasibility and acceptance boundaries
before task generation.
**Created**: 2026-07-13

## F010 Dialog Completion

- [x] PR001 Is command classification evaluated only after child dispatch leaves the command unconsumed? [Plan §Slice A]
  - Durchführungshinweis: Bestehenden `TDialog.HandleEvent`-Ablauf lesen und sicherstellen, dass verbrauchte Kinderevents nicht erneut als Abschluss wirken.
- [x] PR002 Does `cmCancel` bypass content validation while other default completion commands validate? [Contract §Dialog Completion Matrix]
  - Durchführungshinweis: Turbo Vision, Free Vision, Clarification und Contract auf dieselbe Ausnahme prüfen.
- [x] PR003 Can a derived dialog extend completion without overriding the whole event loop or using global mutable state? [Research §R2]
  - Durchführungshinweis: Geplanten geschützten virtuellen Hook auf Sichtbarkeit, Default und Override-Kompatibilität prüfen.
- [x] PR004 Can ordered recursive validation identify and focus the first rejecting descendant? [Data Model §1–2]
  - Durchführungshinweis: Bestehende private Kindliste, Owner-Kette und Feature-025-Fokus-API prüfen; benötigte minimale Snapshot-/Fokus-Hilfe in Tasks aufnehmen.

## F011 Validator Integration

- [x] PR005 Does default edit behavior permit intermediate text while final phases remain strict? [Research §R3]
  - Durchführungshinweis: `TRangeValidator(10,20)` als Gegenbeispiel verwenden und jede Edit-Entscheidung darauf prüfen.
- [x] PR006 Is a candidate edit validated before data, cursor, viewport, insert mode, or selection state mutates? [Research §R4]
  - Durchführungshinweis: Alle InputLine-Editzweige einschließlich Paste, Cut, Delete, Backspace und Overwrite inventarisieren.
- [x] PR007 Does focus rejection use `CanReleaseFocus` exactly once before mutation? [Plan §Slice B]
  - Durchführungshinweis: Feature-025-Tests und `TGroup.TrySetFocusCore` gegen den geplanten Validatoraufruf prüfen.
- [x] PR008 Is rejection text observable without converting normal invalid input into an exception? [Data Model §1]
  - Durchführungshinweis: Result-Modell und A11Y-Evidence auf bool-only oder exception-only Lücken prüfen.

## F012 File Outcomes

- [x] PR009 Does the new outcome cover all modes without breaking existing positional `TFileDecisionResult` construction? [Research §R5]
  - Durchführungshinweis: Bestehende Aufrufe und Tests per `rg` suchen; neue Typen/Enum-Werte nur additiv planen.
- [x] PR010 Can existing `ConfirmDecision` reject without returning stale success or closing? [Contract §File Outcome Matrix]
  - Durchführungshinweis: `LastDecision`, neue `LastOutcome`, Projektion und Close-Reihenfolge für zwei aufeinanderfolgende Versuche simulieren.
- [x] PR011 Are invalid `Path.GetFullPath` and wildcard exceptions converted to typed rejection at the dialog boundary? [Plan §Slice C]
  - Durchführungshinweis: Aktuelle Exception-Catches inventarisieren und alle plattformrelevanten Argument-/Path-/I/O-Fehler klassifizieren.
- [x] PR012 Is an existing Save target only a caller-decision outcome, never an overwrite? [Contract §File Outcome Matrix]
  - Durchführungshinweis: Nach allen `File.*`-Schreiboperationen im geplanten Scope suchen und deren Abwesenheit als Gate festhalten.

## F013 Resource Composition

- [x] PR013 Do records contain primitives/stable IDs rather than runtime objects or CLR type names? [Data Model §4–6]
  - Durchführungshinweis: Jedes Record-Feld auf Pointer-, Owner-, Type-, Delegate- oder Reflection-Abhängigkeit prüfen.
- [x] PR014 Can menu parent references be validated for unknown parent, duplicate ID, cycle, depth, and deterministic sibling order? [Plan §Slice D]
  - Durchführungshinweis: Positive Baum-, Forest-, Separator- und fünf negative Graphfälle in Tasks vorsehen.
- [x] PR015 Can status definitions preserve first-match order and reject invalid ranges/commands? [Data Model §5]
  - Durchführungshinweis: Bestehende `TStatusDef`-Semantik und neue flache Records paarweise abgleichen.
- [x] PR016 Does `TResourceFile.Load` enforce entry/payload bounds and complete-stream validation before returning? [Data Model §6]
  - Durchführungshinweis: Aktuellen Reader-Ablauf auf negative/zu große Längen, truncation, trailing und candidate publication prüfen.
- [x] PR017 Are dialog, menu, and status records registered explicitly as built-ins while unknown types still fail? [Plan §Slice D]
  - Durchführungshinweis: `RegisterBuiltInTypes`, Record-Registrierung und unknown-type Test als eine Allowlist-Kette prüfen.

## Cross-Cutting Closure

- [x] PR018 Are historical hashes and pinned commit reproducible and external sources untracked? [Research §R9]
  - Durchführungshinweis: Commit und vier SHA-256-Werte gegen Feature-024-Ledger prüfen; `/tmp` und Git-Status kontrollieren.
- [x] PR019 Did the standard agent-context script update all generated surfaces, and were stale marker blocks corrected atomically? [Plan §Constitution Check]
  - Durchführungshinweis: Vier Skriptziele, vier `SPECKIT`-Marker und die separate Root-Copilot-Datei vergleichen.
- [x] PR020 Are Feature 028 and both Wave blocks preserved through audit, marker, archive, and statistics closeout? [Plan §Validation Strategy]
  - Durchführungshinweis: Abschlussdateien und Reihenfolge inventarisieren; keine Wave-Implementierungsaufgabe zulassen.
- [x] PR021 Are no placeholders, unresolved clarification markers, Constitution violations, or hidden delivery assumptions left? [Readiness]
  - Durchführungshinweis: Alle 026-Artefakte nach Markern durchsuchen und Delivery-Autorität nur in Plan/Evidence akzeptieren.

## Review Result

- [x] PR022 Every execution hint was applied. The one discovered stale-result risk was remediated by the additive `FileDecisionKind.Rejected` projection, and the standard script's stale marker boundary was corrected across maintained agent context files. [Readiness]
