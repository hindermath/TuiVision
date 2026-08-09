## Spec Kit model discovery and routing

- Keep concrete models in a local, non-versioned profile. Git stores only
  provider-neutral roles, adapters, and selection policy.
- Resolve the current agent surface explicitly. Never switch providers or
  harnesses merely because another executable is installed.
- Use `Enumerate`, `EnumerateNames`, `ValidateCandidate`, or `ConfiguredOnly`
  according to the adapter's proven capability. Do not claim enumeration where
  the harness offers only candidate validation.
- Automatically refresh only known and unique mappings after a successful
  read-only preflight. Unknown or ambiguous mappings are `Blocked`.
- `script-only` never starts a model process. Model routing grants no delivery,
  remote, secret, provider, bypass, or administrator authority.

*Keep concrete models in a local, non-versioned profile. Resolve only the
current agent surface, never switch providers silently, and fail closed for
unknown or ambiguous mappings. Model routing grants no execution or remote
authority.*
