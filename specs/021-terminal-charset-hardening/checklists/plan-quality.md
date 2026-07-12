# Plan Quality Checklist: Terminal and Charset Hardening

**Purpose**: Review the complete design package before task generation.
**Reviewed**: 2026-07-12

- [X] Does component ownership keep session, parser, charset, font, and profile logic in Drivers.Console while Controls only projects the public contract? [Plan Structure]
- [X] Does the plan avoid a Drivers-to-Compatibility dependency and reuse existing key translation only at its public boundary? [Plan Structure]
- [X] Is the terminal grammar explicitly bounded by commands, sequence length, parameter count, and value range? [Research Decisions 3-4]
- [X] Are atomic rejection, stream recovery, and exactly-once state publication first-class requirements? [Research Decision 4, Contract]
- [X] Are scroll, history, resize, reset, lifecycle, and BEL semantics deterministic and measurable? [Research Decision 5, Data Model]
- [X] Are Unicode, KOI8-R, U+FFFD, and unsupported codepage boundaries explicit? [Research Decision 6, Charset Contract]
- [X] Is the only accepted font-fixture shape exact and free of host installation or generator execution? [Research Decision 7, Font Contract]
- [X] Is the JSON profile schema closed, atomic, defaulted, and observable without ad-hoc parsing? [Research Decision 8, Profile Contract]
- [X] Does the vertical slice start with evidence, compile-surface review, and one bounded failing Driver matrix? [Plan Vertical Slice]
- [X] Does primary Controls proof require a real app loop, concrete session state, view identity, status, and rendered cells? [Plan Proof Design]
- [X] Are deterministic, Remote-CI, and physical-host evidence classes kept separate? [Research Decision 11, Host Contract]
- [X] Are all named historical source families read-only and prevented from causing host mutation? [Plan Historical Source Review]
- [X] Does governance cover all six presets without inventing cloud, web, supply-chain, AI, or script artifacts? [Plan Governance Matrix]
- [X] Are shared evidence, version, statistics, workflow, and agent edits serialized? [Plan Autonomous Delivery]
- [X] Do remote tasks have one exact acceptance ledger, causal closeout rules, and bounded bypass policy? [Plan Autonomous Delivery]
- [X] Are build-counter increments and commit/push alignment explicit and trigger-correct? [Plan Constitution Check, Validation Matrix]

**Result**: 16/16 passed. Ready for executable plan review.
