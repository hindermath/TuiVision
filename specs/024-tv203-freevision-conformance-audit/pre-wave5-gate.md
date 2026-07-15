# Pre-Wave-5-Gate / Pre-Wave-5 Gate

## Gesamtentscheidung / Aggregate Decision

| Gate | State | Rationale / Begründung |
|---|---|---|
| Feature 024 Revision 2 | Pass | `TVDEMOS/` and `TVFM/` consumer review produced 13 reproducible findings |
| Feature 027 historical closure | Superseded | Merge `35414af` remains valid execution history, but its zero-finding premise no longer governs future work |
| Feature 025 | Closed | Nine `Core025` findings are remediated through real-path red/green proof; Feature 028 must revalidate them |
| Feature 026 | Closed | Four `ComponentData026` findings are remediated through real-path red/green proof; Feature 028 must revalidate them |
| Feature 028 | ReadyForTerminalGuiAudit | All 13 findings, seven integrated slices, 13 consumer groups, governance, local validation, and the three-platform CI definition are independently reconciled |
| Feature 029 | Required | Audit all 48 contracts against pinned Terminal.GUI v1.9.0 before any Wave release |
| Wave 5 | BlockedPendingTerminalGuiAudit | Do not start until Feature 029, any finding-derived hardening, and its independent closure merge |
| Wave 6 | BlockedPendingTerminalGuiAudit | Do not start before Wave 5; reuse both audit layers and re-evaluate actual Wave-5 deltas |

Deutsch: Feature 027 wurde auf seiner damaligen Datenbasis korrekt ausgeführt.
Die spätere Verbraucherprüfung zeigt jedoch, dass normalisierte Testereignisse,
Signalprüfungen und isolierte Vertragsnachweise wichtige reale Pfade nicht
abdeckten. Deshalb wird nicht rückwirkend am historischen Merge geändert;
stattdessen öffnet Revision 2 das Gate mit 13 Findings geordnet wieder.

English: Feature 027 was executed correctly against its then-current dataset.
The later consumer review shows that normalized test events, signal assertions,
and isolated contract proofs missed important real paths. The historical merge
is not rewritten; Revision 2 reopens the gate in a controlled way with 13
findings.

## Eigentümermengen / Owner Sets

| Downstream owner | Accepted findings | Decision | Binding intake |
|---|---:|---|---|
| `Core025` | 9 | Closed | `specs/025-core-runtime-conformance-hardening/pr-evidence.md` |
| `ComponentData026` | 4 | Closed | `specs/026-component-data-conformance-hardening/pr-evidence.md` |
| `Closure028` | 13 findings / 7 slices / 13 consumers | ReadyForTerminalGuiAudit | `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json` |
| `AcceptedFollowUp` | 0 | None | No unowned framework follow-up |
| `ProductDecision` | 0 | None | No unresolved breaking-contract decision in Revision 2 |

## Blockierregeln / Blocking Rules

- Every `Critical` or `High` finding blocks Feature 028 and both example waves.
- Every accepted finding must be closed by a named test and evidence row, not by
  an implementation claim.
- `ProductDecision` blocks autonomous behavioral modification until the owner
  records an explicit decision.
- Feature 025 runs before Feature 026 because dialog, validation, and file flows
  depend on corrected focus, event, modal, and command-state behavior.
- Feature 028 is mandatory even when implementation later shows that one
  finding can be closed by stronger evidence without a runtime change.
- Wave 5 and Wave 6 source trees remain read-only references during 025, 026,
  and 028; no example port is part of conformance hardening.

## Messbarer Vertrag für 028 / Measurable Contract for 028

Feature 028 must:

1. read the final merged outputs of 025 and 026 and reconcile all 13 stable
   finding IDs without silently changing the original observations;
2. validate reciprocal audit relations, exact finding cardinality, real input
   ingress, focus and state propagation, idle and modal lifecycle, desktop
   stack, command-state, drag, dialog validation, file selection, and resource
   composition boundaries;
3. rerun targeted tests, full Release tests, the canonical per-assembly coverage
   gate, formatting, DocFX/A11Y when triggered, security scans, and the supported
   remote OS matrix;
4. perform one read-only Wave-5 and Wave-6 consumer mapping after remediation so
   no local substitute remains necessary for a shared framework contract;
5. release Wave 5 only after every accepted finding is `Closed` or a reviewed
   `ProductDecision`; record Wave 6 as conditionally ready and require
   re-evaluation after Wave-5 implementation deltas.

## Feature-028-Abschluss / Feature 028 Closure

Deutsch: Feature 028 hat alle 13 Findings unveraendert mit den gemergten
Features 025/026 abgeglichen, sieben reale Integrationspfade erneut ausgefuehrt
und sechs Wave-5- sowie sieben Wave-6-Consumer-Gruppen read-only bewertet.
Zwoelf Gruppen verwenden das bestehende Framework. Nur die destruktive
`FILECOPY.PAS`-/`TRASH.PAS`-Politik bleibt als nicht blockierendes
`FollowUpHardening` fuer Wave 6. Der Gate-Zustand ist daher
`ReadyForTerminalGuiAudit`, nicht `WaveReady`.

English: Feature 028 reconciled all 13 findings with merged Features 025/026,
re-executed seven real integration paths, and reviewed six Wave-5 plus seven
Wave-6 consumer groups read-only. Twelve groups use the existing framework.
Only destructive `FILECOPY.PAS`/`TRASH.PAS` policy remains a non-blocking Wave-6
follow-up. The gate is therefore `ReadyForTerminalGuiAudit`, not `WaveReady`.

## Restrestrisiko / Residual Risk

Deutsch: Die kombinierte Prüfung reduziert das Risiko, dass eine einzelne
Demo-Welle lokale Ersatzlogik erzwingt. Sie beweist nicht im Voraus jede spätere
Anwendungsentscheidung. Wave 5 muss weiterhin test-first portiert werden; seine
Framework-Deltas werden vor Wave 6 erneut gegen dieses Gate gelesen.

English: The combined review reduces the risk that one demo wave needs local
substitute logic. It cannot prove every later application decision in advance.
Wave 5 must still be ported test-first, and its framework deltas must be read
against this gate again before Wave 6.
