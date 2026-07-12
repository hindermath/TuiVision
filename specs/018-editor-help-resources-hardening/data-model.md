# Data Model: Editor, Help, and Resources Hardening

## Existing Entities Reused

### EditorSession

Existing `TEditor`/`TFileEditor` state: text, cursor, selection, modified flag,
path, line-ending mode, disk snapshot, and pending save/close decision.

Transitions: `clean -> modified -> save-pending | close-pending | conflict-
pending -> clean | modified | closed`. A cancellation or failure returns to
`modified` without losing text or identity.

### RuntimeHelpGraph

Existing `THelpFile` containing `THelpIndex` and `THelpTopic` objects. Topics
have unique numeric contexts, paragraphs, and zero or more cross-references.
Every accepted reference target resolves within the graph.

### ResourceCatalog

Existing `TResourceFile` exact ordinal key-to-object mapping and registry-backed
persistence. Keys differing only by case remain distinct.

## New Planning Entities

### HelpSourceDocument

- Ordered source lines
- Optional source name for diagnostics
- Topic declarations and body lines
- Strict UTF-8 stream input or an already decoded string
- No published runtime state until validation succeeds

### HelpSourceTopic

- One or more symbolic names
- Runtime title equal to the first declared symbol
- One numeric context per symbol
- Ordered paragraph/preformatted segments
- Inline unresolved references with visible text and target symbol
- Declaration line/column

Rules:
- Symbol and context uniqueness are global.
- First explicit number sets that symbol; subsequent symbols in the same
  declaration increment unless explicitly numbered.
- A topic needs at least one valid symbol and may have empty body text.
- All references resolve before runtime model publication.

### HelpSourceReference

- Visible text
- Target symbol
- Source line and column
- Final paragraph-relative offset and length
- Resolved numeric context after symbol resolution

Transition: `parsed -> pending -> resolved` or `parsed -> pending -> error`.

### HelpCompilerDiagnostic

- Severity (`Error` for v1 accepted failures)
- Stable code
- Human-readable German-first/English-second message where user-facing
- Source name, line, and column

Diagnostic codes distinguish malformed topic, duplicate symbol/context,
invalid number, malformed reference, unresolved reference, invalid UTF-8, and
input limit.

### HelpCompilationResult

- Success flag derived from zero error diagnostics
- Complete `THelpFile` only on success
- Read-only symbol-to-context map only on success
- Ordered diagnostics

Invariant: a result with any error exposes no help model or partial symbol map.

### LocalizedResourceRequest

- Non-empty exact base key
- Non-empty requested language tag
- Ordered distinct fallback tags
- Neutral fallback enabled by the fixed contract
- Requested runtime type

Candidate sequence: `<base>.<requested>`, each `<base>.<fallback>` in supplied
order excluding duplicates, then `<base>`.

### LocalizedResourceResult

- Found flag
- Matched exact key when found
- Typed value, including an empty but valid value
- Attempted keys in deterministic order

Invariant: missing is represented by `Found == false`, not by comparing value
content with empty/default.

## Validation Limits

- Reject null input and invalid symbol/key/tag shapes before mutation.
- Parse iteratively; do not recurse over source nesting.
- Apply bounded integer parsing and reject overflow.
- Preserve source order for deterministic diagnostics and model production.
- Defaults are 1,048,576 decoded source characters, 16,384 characters per line,
  and 10,000 topics; configured limits must be positive.
- Existing stream/resource readers retain explicit truncation, trailing-data,
  unknown-type, cycle, duplicate/count, and atomic-load boundaries.
