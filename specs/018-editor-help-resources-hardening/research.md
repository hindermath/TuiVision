# Research: Editor, Help, and Resources Hardening

## R1 - Existing foundation

**Decision**: Reuse Feature 004 editor, file, help, stream, registry, and
resource contracts. Add integration proof before changing those classes.

**Rationale**: Current tests already prove isolated editing, safe close,
external file conflicts, help lookup/navigation, exact resource keys, shared
references, cycle rejection, truncation, and trailing-data rejection. The
intake asks for application-readiness gaps, not another broad implementation.

**Alternatives considered**: Rebuild the Feature 004 surface; rejected as
duplicative and outside scope.

## R2 - Historical help compiler intent

**Decision**: Adopt the historical `.topic` declaration and inline
`{text[:alias]}` cross-reference concepts, including forward references, but
compile to the current managed `THelpFile` model.

**Rationale**: `tv203s/contrib/tvision/examples/tvhc/tvhc.cc`, `tvhc.h`, and
`demohelp.txt` define the learner-visible source intent. The managed runtime
already has a suitable topic/index/reference model.

**Alternatives considered**: Byte-compatible `.HLP` output; rejected because
Feature 004 explicitly accepts behavioral/conceptual compatibility. Markdown
or JSON as the sole grammar; rejected because it would weaken historical intent
and make the later `tvhc` example less representative.

## R3 - Unresolved references

**Decision**: Resolve forward references, but reject any reference still
unresolved at end of compilation.

**Rationale**: Historical TVHC warned and emitted usable output. The binding
018 intake explicitly requires invalid cross-references to remain visible,
testable errors and forbids examples from hiding them. Rejection is the safer
modern deviation and is documented as `IntentionalDeviation`.

**Alternatives considered**: Historical warning-only behavior; rejected because
it can publish an apparently valid model with a known broken reference.

## R4 - Atomic publication

**Decision**: Return either a complete model or diagnostics with no model;
persist only successful results through existing stream/resource APIs.

**Rationale**: This separates parse validation from output side effects and
prevents malformed input or destination failures from exposing partial state.

**Alternatives considered**: Mutate a `THelpFile` supplied by the caller;
rejected because rollback is error-prone and obscures proof boundaries.

## R5 - i18n boundary

**Decision**: Add deterministic resource-language selection over existing
TResourceFile keys, not GNU gettext, ambient host locale, catalog installation,
or charset conversion.

**Rationale**: The intake asks for resource-name, language variant, fallback,
and error semantics before the example. Gettext/catalog deployment and codepage
conversion overlap later terminal/charset work and would add platform/tool
dependencies. A resource lookup contract is portable and composable.

**Alternatives considered**: Bind GNU gettext; rejected due to dependency,
deployment, and charset scope. Read `LANG` implicitly; rejected because tests
and applications need deterministic explicit selection.

## R6 - Language key convention

**Decision**: Exact ordinal key `<baseKey>.<languageTag>`, then caller-provided
fallback tags in order, then neutral `<baseKey>`.

**Rationale**: It preserves Feature 004 case-sensitive exact keys and makes
selection inspectable. Callers retain policy control without global state.

**Alternatives considered**: Nested resource catalogs; rejected as unnecessary
new persistence structure. Automatic culture-parent expansion; rejected because
implicit ordering can surprise callers and obscure evidence.

## R7 - Validation strategy

**Decision**: Test first in Serialization for compiler/lookup, then Controls
integration. Run full Release and canonical coverage after focused tests.

**Rationale**: Shared framework behavior changes and public APIs have broad
blast radius. XML/API additions also trigger DocFX and web A11Y.

**Alternatives considered**: Focused tests only; rejected for shared runtime
code. Example smoke tests; rejected because Wave-3 examples are out of scope.

## R8 - Governance applicability

**Decision**: Record six-preset evidence in feature-local `pr-evidence.md` and
reuse repository security documents unless a trigger changes.

**Rationale**: Parser/persistence boundaries require proportional security and
architecture review, while ASVS, cloud, AI runtime, script parity, and new
supply-chain artifacts remain trigger-based `N/A`.

**Alternatives considered**: Generate every governance template; rejected as
non-proportional evidence churn without a triggering boundary.
