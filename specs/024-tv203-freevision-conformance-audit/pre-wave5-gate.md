# Pre-Wave-5-Gate / Pre-Wave-5 Gate

## Gesamtentscheidung / Aggregate Decision

| Gate | State | Rationale / Begründung |
|---|---|---|
| Feature 024 Revision 2 | Pass | `TVDEMOS/` and `TVFM/` consumer review produced 13 reproducible findings |
| Feature 027 historical closure | Superseded | Merge `35414af` remains valid execution history, but its zero-finding premise no longer governs future work |
| Feature 025 | Required | Nine `Core025` findings must be remediated and proven |
| Feature 026 | Required | Four `ComponentData026` findings must be remediated and proven |
| Feature 028 | Required | Independent combined closure after 025 and 026 |
| Wave 5 | Blocked | Do not start until Feature 028 passes and merges |
| Wave 6 | Blocked | Do not start before Wave 5; reuse the combined closure evidence and re-evaluate Wave-5 deltas |

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
| `Core025` | 9 | Required | `Lastenheft_10_Core-Runtime-Conformance-Hardening.md` |
| `ComponentData026` | 4 | Required | `Lastenheft_11_Component-Data-Conformance-Hardening.md` |
| `Closure028` | Required | Mandatory | `Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.md` |
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

## Restrestrisiko / Residual Risk

Deutsch: Die kombinierte Prüfung reduziert das Risiko, dass eine einzelne
Demo-Welle lokale Ersatzlogik erzwingt. Sie beweist nicht im Voraus jede spätere
Anwendungsentscheidung. Wave 5 muss weiterhin test-first portiert werden; seine
Framework-Deltas werden vor Wave 6 erneut gegen dieses Gate gelesen.

English: The combined review reduces the risk that one demo wave needs local
substitute logic. It cannot prove every later application decision in advance.
Wave 5 must still be ported test-first, and its framework deltas must be read
against this gate again before Wave 6.
