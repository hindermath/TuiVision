## Spec Kit model discovery and routing

- Keep concrete models in a local, non-versioned profile. Git stores only
  provider-neutral roles, adapters, and selection policy.
- Resolve the current agent surface explicitly. Never switch providers or
  harnesses merely because another executable is installed.
- Use `Enumerate`, `EnumerateNames`, `ValidateCandidate`, or `ConfiguredOnly`
  according to the adapter's proven capability. Do not claim enumeration where
  the harness offers only candidate validation.
- Refresh only known, unique mappings after the stable resolver's metadata
  checks and explicit local authority. Refresh does not run the rendered
  model-access preflight. That later execution can consume quota and needs its
  applicable run authority. Missing capabilities and ambiguous tiers block.
- New adapters filter capabilities before broad family preferences. Exact
  discovered IDs may use `-FrontierModel`/`-FastModel`; they never bypass policy
  or capability checks. One model may serve several roles. Codex/agy selection
  remains unchanged; no automatic newest-version guessing.
- All five harnesses have automatic paths in candidate 0.2.0. Claude/Copilot need an explicitly supplied installed SDK; OpenCode needs an existing loopback server and a selected connected provider. Fail closed on missing prerequisites; never install or switch providers implicitly.

DE: Refresh bindet nur eindeutige Metadaten unter lokaler Änderungsautorität.
Eine Modellzugangs-Probe wird dabei nicht ausgeführt. Ein späterer
Runner-Preflight kann Kontingent verbrauchen. Neue automatische Adapter benötigen vorhandene SDKs bzw. einen ausdrücklich benannten lokalen OpenCode-Server und Provider. Codex und Antigravity bleiben unverändert. Keine stille Ersatzwahl.
- `script-only` never starts a model process. Model routing grants no delivery,
  remote, secret, provider, bypass, or administrator authority.

*Keep concrete models in a local, non-versioned profile. Resolve only the
current agent surface, never switch providers silently, and fail closed for
missing capabilities or ambiguous mappings. Model routing grants no execution or remote
authority.*
