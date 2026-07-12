# Plan Review Checklist: A11Y Framework

Each item includes the execution hint used during autonomous review.

- [X] **Contract ownership**: Trace each new type to Core or Controls and reject reverse dependencies.
- [X] **Focus uniqueness**: Follow `SetFocus -> CurrentChanged -> cmFocusChanged` and prove same-target no-op.
- [X] **Compatibility**: Locate every current `cmFocusChanged` consumer and require legacy payload tolerance where needed.
- [X] **Shortcut truth**: Trace menu/status item state and exclude separators, zero commands and disabled entries.
- [X] **Keyboard completeness**: Build a concrete selectable-family inventory and require Proof/N/A in every key column.
- [X] **Contrast semantics**: Map every semantic colour role to a concrete cell assertion and text status.
- [X] **Reference slice**: Require app-loop, concrete state, exact view identity and rendered cell regions.
- [X] **Docs workflow**: Inspect triggers and failure semantics in `pages.yml`; do not add duplicate CI work.
- [X] **Public docs**: Compile Release with CS1591 and run DocFX/Axe after XML/guide changes.
- [X] **Historical honesty**: Record no-direct-equivalent and never infer modern accessibility from Turbo Vision sources.
- [X] **Scope scan**: Search final diff for native bridges, new packages, Wave changes and generated output.
- [X] **Delivery truth**: Keep remote reviewer unavailability distinct from successful review and use narrow bypass only under authorization.

## Review Result

All hints were executed against the plan and repository surfaces. No critical,
high or unresolved medium planning defect remains.
