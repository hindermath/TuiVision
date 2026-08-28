# Data Model: Transactional Form Model

- `FormField<T>`: Name, Value, OriginalValue, comparer, revision, validators,
  validation errors and optional typed binding.
- `FormSession`: ordered fields, ordered child sessions, adapters, structure
  revision and one active submit lease.
- `FormSnapshot`: immutable flattened field snapshots plus revision stamps.
- `FormChangeSet`: immutable ordered changes with dotted child paths.
- `FormSubmitResult`: Success, ValidationFailed or Stale plus errors/change set.
- `TFormSemanticDocument`: version, root form and closed form definitions.
- `FormRuntimeRegistry`: trusted key maps for controls, types, bindings,
  converters and validators.
