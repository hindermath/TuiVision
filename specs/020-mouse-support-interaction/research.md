# Research: Mouse Support and Interaction Hardening

## R1 - Canonical Event Contract

**Decision**: Keep `TEvent`, `TEventKind.MouseDown/MouseMove/MouseUp`, and
`TMouseEvent` unchanged as the only UI mouse model.

**Rationale**: Core and Controls already consume this contract. A host-specific
payload outside the driver would duplicate semantics and leak protocol details.

**Rejected**: A second event hierarchy or example-local mouse DTOs.

## R2 - First Host Protocol

**Decision**: Parse only complete SGR-1006 reports (`CSI < Cb ; Cx ; Cy M/m`)
for left press, pressed movement, and release.

**Rationale**: SGR uses decimal coordinates, has an explicit release terminator,
and is widely available in modern Unix-like terminals and Windows Terminal/WSL.

**Rejected**: X10 byte coordinates, wheel/hover coverage, arbitrary CSI parsing,
or complete emulator compatibility.

## R3 - Host Capability

**Decision**: Classify SGR input as `Enabled`, `Disabled`, or `Unsupported`.
Enable only for interactive macOS/Linux or WSL sessions with a usable terminal;
keep redirected/headless and native Windows Console paths unsupported.

**Rationale**: Explicit states prevent half-enabled input and false portability claims.

## R4 - Atomic Validation

**Decision**: Bound sequence length and numeric fields, require complete syntax,
validate one-based host coordinates before zero-based conversion, clamp only to
the current buffer contract, and reject invalid phase transitions without output.

**Rationale**: Raw terminal input is untrusted. Partial publication risks ghost
clicks, duplicate commands, and stale drag state.

## R5 - Double Click

**Decision**: Mark only the second left press as double-click when it has the
same cell and target identity and occurs within 500 ms on a monotonic timeline.
Controls supplies a point-to-target-key delegate; Driver stores only the stable
key and never references a Controls type.

**Rationale**: This matches historical same-position/tick intent while making
time deterministic and preventing cross-target clicks from combining.

**Rejected**: Wall-clock time, release-based classification, or spatial tolerance.

## R6 - Focus and Hit Routing

**Decision**: On mouse down, `TGroup` selects one topmost visible eligible hit
target and transfers focus through `SetFocus` before normal target handling.

**Rationale**: Current broadcast-to-all behavior can activate an older covered
view and does not update group ownership of focus.

## R7 - Coordinate Ownership

**Decision**: Global/local conversion traverses the owner chain.

**Rationale**: Canonical mouse coordinates are screen-global; subtracting only
the immediate origin is incorrect for nested desktop/window/control trees.

## R8 - Single Drag Contract

**Decision**: Drag only movable `TWindow` instances from their top title row.
Clamp within owner bounds, commit on release, and cancel on Escape, capability
loss, disable/removal, or shutdown.

**Rationale**: Window move already has a keyboard contract and proves the full
press-move-release lifecycle without importing scrollbar or selection scope.

## R9 - Keyboard and A11Y

**Decision**: Preserve Tab/commands and `Ctrl+F5` plus arrows for all required
operations, and expose capability and interaction state as text.

**Rationale**: Mouse augments rather than gates the UI.

## R10 - Historical Intent

**Decision**: Review `tevent.cc`, `tmouse.cc`, `tview.cc`, `twindow.cc`,
`unix/xtermmouse.cc`, and matching event/view/window/mouse headers read-only.

**Rationale**: These sources define polling, double-click, hit, and window-move
intent; the managed implementation remains event-driven and bounded.

## R11 - Validation Boundary

**Decision**: Use deterministic raw-sequence injection for parser/state CI and
real app-loop proof for UI behavior. Record physical host checks separately.

**Rationale**: CI can prove contracts without a pointing device but cannot claim
an unexecuted terminal/backend observation.

## R12 - Governance and Delivery

**Decision**: Apply NIST/CWE, proportional STRIDE/CIA/CAPEC, iSAQB, A11Y,
cross-platform host review, and agent parity. Keep unrelated cloud, web,
supply-chain, AI, regulation, and script checkpoints trigger-based `N/A`.
Use authorized `MergeAndSync`; every remote task writes the feature evidence.

## R13 - Generic Promotion Boundary

**Decision**: Record autonomous-run observations in feature evidence. Apply a
generic workflow change only after merge on a separate non-empty retrospective PR.

**Rationale**: Feature implementation must not absorb preset productization.
