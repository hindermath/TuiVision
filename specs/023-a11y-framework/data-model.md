# Data Model: A11Y Framework

## AccessibleWidget

| Field | Type | Rule |
|---|---|---|
| AccessibleLabel | string | required, nonblank, concise visible-purpose text |
| AccessibleDescription | string? | optional explanatory text |
| CanReceiveFocus | bool | current capability, not a design-time promise |

## FocusAnnouncement

| Field | Type | Rule |
|---|---|---|
| Target | framework view reference | exact focused target |
| AccessibleLabel | string? | present only for valid opt-in widget text |
| AccessibleDescription | string? | optional snapshot |
| CanReceiveFocus | bool | snapshot at event creation |

Lifecycle: created only after an actual `Current` transition; immutable for the
broadcast; never persisted.

## AccessibleShortcut

| Field | Type | Rule |
|---|---|---|
| KeyCode | unsigned key value | nonzero executable key |
| DisplayText | string | nonblank text-first description |
| Command | integer command | executable command, zero excluded |
| Source | string | stable provider-local source identity |

Duplicate keys are retained as separate source-qualified rows. Separators and
disabled/non-executable entries do not become rows.

## KeyboardCoverageEntry

| Field | Type | Rule |
|---|---|---|
| ControlFamily | string | stable test inventory identity |
| Tab / ShiftTab / Arrows / Enter / Shortcut | Proof or N/A | each required |
| Rationale | string | mandatory for N/A |
| Evidence | test name/path | mandatory |

## ColorScheme

Semantic roles: `Background`, `Text`, `Emphasis`, `SelectionBackground`,
`SelectionText`, `StatusBackground`, `StatusText`. Each role has a concrete
console colour. `HighContrast` is named and immutable.

## GovernanceDecision

`Applicable`, `N/A` or `Open`, plus preset/version/checkpoint, rationale,
evidence path, owner, reviewer, review date, result, residual risk, follow-up
and re-evaluation trigger.
