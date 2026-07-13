# Audit-Findings / Audit Findings

## Revision-2-Ergebnis / Revision 2 Result

Deutsch: Die isolierte Vertragsprüfung von Feature 024 war formal vollständig,
hat aber mehrere vorhandene Proofs zu stark bewertet. Die zusätzliche
Verbraucherprüfung gegen `TVDEMOS/` und `TVFM/` zeigt acht bestätigte
Verhaltensabweichungen und fünf Nachweislücken. Die 13 Findings sind
reproduzierbar, genau einem Vertrag zugeordnet und bilden die verbindliche
Quelle für die Lastenhefte 025 und 026.

English: Feature 024's isolated contract review was formally complete but gave
several existing proofs too much weight. The additional consumer review against
`TVDEMOS/` and `TVFM/` identifies eight confirmed behavioral drifts and five
evidence gaps. The 13 findings are reproducible, map to exactly one contract,
and are the binding source for requirements documents 025 and 026.

| Measure | Count |
|---|---:|
| Total contracts | 48 |
| `Aligned` | 7 |
| `IntentionalModernization` | 27 |
| `ConsciouslyOmitted` | 1 |
| `BehavioralDrift` | 8 |
| `EvidenceGap` | 5 |
| Total findings | 13 |
| Critical | 0 |
| High | 9 |
| Medium | 4 |
| Low | 0 |
| `Core025` | 9 |
| `ComponentData026` | 4 |

## Finding-Ledger

Die vollständigen Reproduktionen, Quellpfade, Akzeptanzgrenzen und Nicht-Ziele
stehen maschinenprüfbar in `conformance-audit.json`. Diese Tabelle ist die
lesbare Review-Sicht.

*Complete reproductions, source paths, acceptance boundaries, and non-goals are
machine-checkable in `conformance-audit.json`. This table is the readable review
view.*

| Finding | Contract | Decision | Severity | Scope | Kurzbeobachtung / Short observation | Disposition |
|---|---|---|---|---|---|---|
| `F001` | `C004` | `BehavioralDrift` | Medium | Both | `CreateMouse` accepts composite event masks | `Core025` |
| `F002` | `C008` | `BehavioralDrift` | High | Both | Focus loss has no validator-owned veto | `Core025` |
| `F003` | `C009` | `BehavioralDrift` | High | Both | Group state propagation marks all children focused or disabled | `Core025` |
| `F004` | `C013` | `BehavioralDrift` | High | Both | Application loop lacks bounded pending-event and idle lifecycle | `Core025` |
| `F005` | `C014` | `EvidenceGap` | High | Both | Desktop stack operations needed by consumers are absent or unproved | `Core025` |
| `F006` | `C015` | `BehavioralDrift` | High | Both | Default close and modal execution do not complete the lifecycle | `Core025` |
| `F007` | `C017` | `EvidenceGap` | Medium | Both | Command enablement has no shared context-refresh contract | `Core025` |
| `F008` | `C034` | `BehavioralDrift` | High | Both | Real keyboard ingress bypasses canonical translation | `Core025` |
| `F009` | `C036` | `EvidenceGap` | High | Wave6 | Generic tracked drag and keyboard-equivalent drop proof are missing | `Core025` |
| `F010` | `C019` | `BehavioralDrift` | High | Both | Dialogs close on unrelated commands and skip child validation | `ComponentData026` |
| `F011` | `C021` | `BehavioralDrift` | High | Both | Input lines cannot integrate validators | `ComponentData026` |
| `F012` | `C023` | `EvidenceGap` | Medium | Both | File-dialog results lack mode-aware path validation proof | `ComponentData026` |
| `F013` | `C026` | `EvidenceGap` | Medium | Both | Named UI-resource composition is absent or unproved | `ComponentData026` |

## Feature-025-Schließung / Feature 025 Closure

Deutsch: Feature 025 schließt `F001` bis `F009` durch dokumentierte rote
Grenzen, additive Implementierungen und grüne Tests über die realen Framework-
Pfade. Keine Zeile wurde nur durch Dokumentation geschlossen. Die ursprünglichen
Beobachtungen bleiben für Feature 028 unverändert prüfbar.

English: Feature 025 closes `F001` through `F009` with documented red
boundaries, additive implementations, and green tests through the real
framework paths. No row was closed by documentation alone. The original
observations remain unchanged for Feature 028 revalidation.

| Finding | State | Evidence | Documentation-only |
|---|---|---|---|
| `F001` | `Closed` | Build 215 red; Build 216 green; `TEvent.CreateMouse` real factory | No |
| `F002` | `Closed` | Build 219 red; Build 220 green; `TGroup.TrySetFocus` | No |
| `F003` | `Closed` | Build 221 red; Build 222 green; Group state matrix | No |
| `F004` | `Closed` | Build 223 red; Build 225 green; real `Run` and `GetEvent` | No |
| `F005` | `Closed` | Build 228 red; Build 229 green; Desktop stack operations | No |
| `F006` | `Closed` | Build 230 red; Builds 232-233 green; close/modal lifecycle | No |
| `F007` | `Closed` | Build 226 red; Build 227 green; shared command context | No |
| `F008` | `Closed` | Build 217 red; Build 218 green; canonical console ingress | No |
| `F009` | `Closed` | Build 234 red; Build 236 green; pointer/keyboard drag session | No |

## Proof-Grenze / Proof Boundary

Deutsch: Vorhandene Tests bleiben nützliche Teilnachweise. Sie schließen ein
Finding jedoch nicht, wenn sie normalisierte Ereignisse direkt einspeisen,
nur ein Signal statt des sichtbaren Endzustands prüfen oder einen abgeleiteten
Test-Hook aufrufen, den der Produktionspfad nicht verwendet. Ein Finding wird
erst durch den in 025 oder 026 geforderten realen Pfad geschlossen und durch
Feature 028 unabhängig erneut bestätigt.

English: Existing tests remain useful partial evidence. They do not close a
finding when they inject normalized events directly, assert only a signal
rather than the visible final state, or invoke a derived test hook that the
production path does not use. A finding closes only through the real path
required by 025 or 026 and then receives independent Feature-028 revalidation.

## Re-Evaluierungsgrenze / Re-evaluation Boundary

Deutsch: Die ursprüngliche Null-Finding-Entscheidung bleibt als historische
Ausführungsevidence nachvollziehbar, ist aber für die Zukunftsplanung
superseded. Neue Produktlogik, API-Änderungen, Consumer-Funde oder ein
fehlschlagender Proof öffnen den betroffenen Vertrag erneut.

English: The original zero-finding decision remains traceable execution
history, but it is superseded for forward planning. New product logic, API
changes, consumer findings, or a failing proof reopen the affected contract.
