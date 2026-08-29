# Public API Contract: Transactional Form Model

## Controls contracts

- `IFormField` and `IFormField<T>` expose name, current/baseline value,
  modification state, validation state and explicit accept/reject operations.
- `FormField<T>` supports explicit equality, synchronous validators,
  submit-time asynchronous validators and direct typed property-expression
  binding. Once a field belongs to a session, the root session owns commit and
  rollback.
- `FormValueConverter<TField,TModel>` converts in both directions with an
  explicitly supplied `CultureInfo`; conversion failures are typed validation
  results, not ambient-culture fallbacks.
- `FormSession` owns ordered fields, adapters and recursively owned child
  sessions. `GetChangeSet()`, `SubmitAsync()`, `AcceptChanges()` and
  `RejectChanges()` operate on the complete root transaction.
- `FormInputLineAdapter` is opt-in composition. An ordinary `TInputLine`
  remains nontransactional until an application attaches the adapter.
- `FormRuntimeRegistry` resolves only pre-registered symbolic type, control,
  binding, converter and validator keys. `ResolvedFormSemanticDocument`
  exposes the fully checked immutable resolution result.

## Submit and commit contract

`SubmitAsync()` captures a stable recursive snapshot and runs synchronous plus
submit-time asynchronous validation. It never updates a bound POCO and never
advances a baseline. A concurrent submit is rejected; cancellation propagates;
revision drift produces `FormSubmitStatus.Stale` and does not publish obsolete
errors.

The application persists the successful `FormChangeSet` externally and calls
`AcceptChanges()` only afterward. Accept applies bound setters in stable order
and advances every baseline only after all setters succeed. On setter failure,
already attempted setters are restored in reverse order as far as their own
contracts permit. `FormBindingCommitException` identifies the failing field,
the primary error and any rollback errors; arbitrary setter side effects remain
an explicit application boundary.

## Serialization contract

`TFormSemanticDocument`, `TFormSemanticDefinition`, `TFormSemanticField` and
`TFormSemanticChild` represent version-1 form semantics. `TFormSemanticJson`
uses deterministic `System.Text.Json` output and accepts only the closed
allowlist `version`, `form`, `forms`, `fields`, `children`, `field`, `control`,
`type`, `binding`, `converter` and `validators`.

The parser rejects unknown or duplicate properties, unsupported versions,
duplicate keys, invalid or unknown references, shared child ownership, type
conflicts, cycles, unreachable forms, payloads over 256 KiB, graphs deeper than
32 and documents over 4,096 semantic items. CLR type names, property paths,
method names and executable content have no JSON contract. Parsing and registry
resolution return either one complete model or an exception; no partial model
is observable.
