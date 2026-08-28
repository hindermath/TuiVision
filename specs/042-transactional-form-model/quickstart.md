# Quickstart: Transactional Form Model

1. Create fields directly or from typed property expressions.
2. Add fields and optional Child-Sessions to one `FormSession`.
3. Attach opt-in `FormInputLineAdapter` instances for UI controls.
4. Call `SubmitAsync()`. Persist only a successful, non-stale ChangeSet.
5. Call `AcceptChanges()` after persistence, or `RejectChanges()` to restore
   the baseline.
6. Load declarative semantics only through a pre-populated
   `FormRuntimeRegistry`; never treat JSON keys as CLR names.
