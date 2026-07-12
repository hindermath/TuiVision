# Contracts: Editor, Help, and Resources Hardening

## Help Source Contract

The reusable compiler accepts a bounded line-oriented source:

```text
.topic Overview=100, Home
Overview text with {details:Details}.

.topic Details=200
 Detailed preformatted line
```

- `.topic` begins a topic and declares one or more symbols.
- The first declared symbol is the runtime topic title.
- Multiple symbols on one declaration are materialized as equivalent runtime
  topics with distinct contexts because the current persisted model indexes one
  context per topic; content and primary title remain identical.
- `Symbol=number` assigns an explicit non-negative context.
- A symbol without a number receives the next available sequential context from
  the preceding assignment in that declaration.
- Body lines without leading whitespace form wrappable paragraphs; leading-
  whitespace lines remain preformatted.
- `{visible text}` targets a symbol matching the visible text; `{visible
  text:Target}` uses an alias.
- Forward references are accepted during parsing but all targets must resolve
  before success.
- Stream input is decoded as strict UTF-8; invalid byte sequences are rejected
  with a source diagnostic. String input is treated as already decoded text.
- Default limits are 1 MiB of decoded source, 16,384 characters per line, and
  10,000 topics; applications may configure positive alternatives.
- Duplicate symbols/contexts, invalid numbers, malformed directives/references,
  unresolved targets, or exceeded limits produce diagnostics and no model.
- Stable error families are `THC001` no topic, `THC002` malformed placement or
  directive, `THC003` invalid context, `THC004` duplicate symbol, `THC005`
  duplicate context, `THC006` malformed reference, `THC007` unresolved target,
  `THC008` invalid UTF-8, and `THC009` exceeded limit.

## Compilation Result Contract

- Success: complete `THelpFile`, complete read-only symbol map, no errors.
- Failure: ordered diagnostics with stable code and source location, no help
  model, no partial symbol map.
- Compiling the same text twice produces logically identical topics, contexts,
  references, symbol mappings, and diagnostic order.
- Persistence is performed only after success through existing TuiVision
  stream/resource APIs and shared built-in registrations.

## Localized Resource Lookup Contract

For base key `Menu.File`, requested language `de-DE`, fallbacks `de`, `en`, the
attempted exact keys are:

```text
Menu.File.de-DE
Menu.File.de
Menu.File.en
Menu.File
```

- Search uses ordinal case-sensitive `TResourceFile` keys.
- Duplicate candidate tags are removed while preserving first occurrence.
- The first existing candidate of the requested type wins.
- Missing and empty valid values are distinguishable.
- Invalid base keys or language tags throw argument errors before lookup; a
  well-formed request that finds no candidate returns a non-throwing missing
  result.
- The result records the matched key and attempted sequence for proof and
  diagnostics without reading ambient locale state. On success, attempted keys
  stop at the matched candidate; on missing, they contain the full sequence.

## Editor/File End-to-End Contract

- Open, edit, search/replace, and save keep buffer, modified state, path,
  line-ending mode, title, and shell command state coherent.
- Safe close requires save/discard/cancel; cancel preserves the session.
- External changes require an explicit overwrite decision.
- Save cancellation or failure leaves content and modified state intact.
- Deterministic proof uses isolated temporary files only.

## Runtime Help Contract

- A compiled or persisted known context opens in `THelpViewer`/`THelpWindow`.
- A valid reference navigates to its target and back restores the previous
  topic.
- Missing contexts use bounded fallback content.
- Invalid persisted graph/reference input is rejected before presentation.

## Framework Decision Contract

Every area uses exactly one primary decision:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

Evidence records area, existing surface, historical source, decision,
rationale, change, positive/negative proof, validation, residual risk, and
follow-up trigger.
