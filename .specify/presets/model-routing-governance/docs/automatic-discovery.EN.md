# Automatic adapters: Model Routing Governance 0.2.6

[Deutsch](automatic-discovery.md) · [User guide](user-guide.EN.md)

The package supports discovery for `agy`, Claude, Copilot and OpenCode.
Existing Codex rules and historical `agy` patterns, profiles and invocations
remain unchanged. Examples do not change installed presets. Installation,
local binding changes and rollout each require separate authorization.

## Prerequisites and status

PowerShell 7 and the selected CLI must already exist. Claude and Copilot also
need Node.js. Supply `-SdkModulePath` with the absolute JavaScript entry of an
already installed, trusted official SDK: `@anthropic-ai/claude-agent-sdk` or
`@github/copilot-sdk`. That file is imported as code: never use an untrusted
file or a model-proposed path. No automatic npm/npx, SDK, CLI or preset install.

```text
bash scripts/resolve-model-routing.sh -Action Status -Harness Claude -SdkModulePath /absolute/sdk/entry.mjs
bash scripts/resolve-model-routing.sh -Action Status -Harness Copilot -SdkModulePath /absolute/sdk/entry.js
```

On Windows invoke the same resolver with `pwsh -NoProfile -File
scripts/resolve-model-routing.ps1` and the same parameters. Example SDK paths
are placeholders and must be replaced.

Claude reads `supportedModels()` with idle streaming input. No user prompt is
submitted. Persistence, optional settings, tools and MCP are disabled for this
metadata path. Copilot starts only the SDK control process using the selected
installed CLI, then reads `listModels()` without creating an inference session.
Incompatible SDK/CLI interfaces block discovery. The SDK subprocess has a
25-second timeout.

OpenCode uses an **already running** local server. Explicitly select a connected
provider. The resolver never starts a server or changes its configuration:

```text
bash scripts/resolve-model-routing.sh -Action Status -Harness OpenCode -OpenCodeServerUrl http://127.0.0.1:4096 -OpenCodeProvider anthropic
```

The URL must be an HTTP origin with `127.0.0.1` or `[::1]`. Paths, credentials,
queries, fragments and redirects are rejected. The resolver first reads
`GET /api/provider` and `GET /api/model`, without a proxy. It supports the
OpenCode 2.2.5 `data[]` envelopes, exact `providerID` values, enabled active
models and confirmed `variants[]`. If the current API is unavailable or
returns HTML, only `GET /provider` is tried as a legacy fallback. Unknown
schemas remain blocked. Optional server credentials come from
`OPENCODE_SERVER_PASSWORD` and `OPENCODE_SERVER_USERNAME`; the default username
is `opencode`. Never put credentials in URLs or Git.
With a password set, pass `-TrustOpenCodeLoopbackEndpoint` on each invocation.
This explicitly trusts the selected local HTTP endpoint; loopback HTTP does
not authenticate the listening process. No password is sent without this
choice. Discovery without a password is unchanged. HTTP responses are limited
to 4 MiB and bounded by timeouts. No cache refresh.

Neither `Status` nor `Refresh` performs model inference. Status writes no
profiles or project files through the resolver. Vendor CLIs may maintain their
own logs, caches or authentication metadata; complete zero writes throughout
the home directory are not guaranteed. Claude does not read optional local
settings on this path; it does not fully reproduce a customized interactive
session.

## Selection and results

The versioned source `scripts/adapters/selection-policy.json` filters confirmed
capabilities first, then uses declared preference tiers. This is an operating
heuristic, not a benchmark or cross-provider quality ranking. Neither discovery
order nor a higher version number alone selects a model. Codex stays unchanged.
Historical `agy` patterns remain valid; current `claude-opus-*-high` and
`gemini-*-argon-high` names are added. Fast still uses
`gemini-*-flash-medium`.

Claude, Copilot and OpenCode return the structured phase result on standard
output. `invoke-harness-with-result.ps1` receives the native CLI argument list
as one JSON-encoded array. For Copilot it extracts the last
`assistant.message` from the native JSONL event stream as the final response, then accepts only a successful JSON object
and atomically writes it to the autonomous runner's `{outputFile}` path. The
result instruction only prohibits writing that runner result file directly;
task-authorized project changes remain permitted. This
also supports read-only roles without granting the harness file-write access.
Empty, failed or unstructured output creates no result file.

| Role | Automatic preference tiers, in order |
|---|---|
| Three demanding roles | `opus` → Claude Opus → GPT excluding mini/nano → o-series → Gemini Pro → `sonnet` → Claude Sonnet → exactly one other capable model |
| `fast-mechanical` | `haiku` → Claude Haiku → GPT mini/nano → Gemini Flash → `sonnet` → Claude Sonnet → reuse the selected frontier model |

The first nonempty tier must contain exactly one eligible candidate. Multiple
versions in that tier block with candidate IDs and the relevant selection
parameter; the tier is not skipped. One model may serve all four roles.
Missing Haiku is not a blocker; reuse does not promise lower cost or latency.

- All new adapters use only discovered IDs. Other families can use unique
  capability selection or exact selection; there is no Opus/Haiku allowlist.
- Claude prefers returned `opus`/`sonnet`/`haiku` aliases over full IDs.
  `default`, `auto` and `opusplan` are excluded for Claude/Copilot because they
  are not exact model bindings.
- `[1m]` entries are inventoried, but potential cost changes require explicit
  exact selection.
- Copilot requires explicit `policy.state=enabled`. Missing, unknown,
  unconfigured or disabled policy excludes a candidate.
- OpenCode uses only the selected connected provider. Model IDs remain
  `provider/model`; errors never cause a provider switch.
- Frontier requires confirmed `high`. Fast uses confirmed `medium`, then `low`,
  then `default` without an effort/variant argument when lack of reasoning
  support is explicit. Otherwise a confirmed level from `high`, `xhigh`, `max`
  is used; a lower level is never invented. Missing metadata excludes that
  candidate. A role without an eligible candidate remains blocked.

Use `-FrontierModel` and `-FastModel` with exact discovered IDs when needed,
including for new families. OpenCode IDs include the provider. These choices
cannot bypass capabilities, authentication, Copilot policy or provider binding.
Example with synthetic IDs, not promised availability:

```text
bash scripts/resolve-model-routing.sh -Action Status -Harness Copilot -SdkModulePath /absolute/sdk/entry.js -FrontierModel gpt-99 -FastModel gpt-99-mini
```

After a successful refresh, Claude, Copilot and OpenCode automatically reuse
those deliberately selected models on later status/refresh calls. The stored
schema-2.0 profile is accepted only when frontier, implementation and review
contain exactly the same model ID and reasoning level, the fast profile is
unambiguous, and the current catalogue still reports every model as active,
policy-compliant and eligible for the stored reasoning level. A parameter on
the current call takes precedence for its role; the other role may still come
from the profile. With no stored selection, the existing fail-closed policy
continues to apply.

If a stored model disappears or its policy or reasoning capability changes,
the call blocks and preserves the profile byte for byte. It never silently
switches to a newer model. An explicitly supplied valid replacement may replace
the stale selection for that role. This reuse does not apply to Codex or `agy`;
their behavior remains unchanged.

Exit code `0` means aligned, `2` means refresh required, `3` means blocked.
With no profile, status proposes a binding and returns 2. Ambiguity, duplicate
IDs, empty inventories and incompatible interfaces return 3 and preserve an
existing profile. `-WhatIf` writes nothing. After explicit local authority,
repeat the command with `-Action Refresh`. Profile schema remains 2.0.
Codex/Antigravity retain their existing `generatedBy`; new profiles use
`model-routing-governance/0.2.0`. New profiles also bind the selection policy
through `selectionPolicySha256`. A changed or missing hash causes
`RefreshRequired` without modifying a profile during status.

## Execution requires its own authority

Discovery proves neither usable quota nor successful model access. Refresh
only renders four profiles, including a later access preflight, without running
them. A subsequent runner must check its own authority, worktree boundary and
result file.

New profiles use native CLI flags: Claude `--effort`; Copilot prefers
`--reasoning-effort` and uses `--effort` only when that legacy spelling is
detected; OpenCode uses either a detected `--variant` flag or the
`provider/model#variant` form documented by 2.2.5. CLI help is captured from stdout and
stderr in a bounded child process, stripped of ANSI/control sequences and then
checked for whole flag tokens. Copilot stays blocked if neither spelling is
present. Preflight requests `MODEL_READY`; Claude disables tools/MCP,
and explicitly separates the positional prompt from the variadic MCP
configuration; Copilot restricts tools to reading and uses `--silent` to keep
diagnostic banners off machine-readable standard output, while OpenCode uses `plan`. Fast profiles also use
plan/read modes. Implementation profiles use Claude `acceptEdits`, OpenCode
`build`, or Copilot's existing permission checks. No bypass or blanket tool
approval. Vendor modes are **not OS sandboxes**. In particular, OpenCode agents,
plugins, hooks and local settings remain a separate trust boundary to inspect
before execution. The resolver neither executes these profiles nor installs
isolation.

## Sources and verification limits

- [Claude Agent SDK](https://code.claude.com/docs/en/agent-sdk/typescript)
- [Claude CLI](https://code.claude.com/docs/en/cli-reference)
- [Claude model identifiers](https://code.claude.com/docs/en/model-config)
- [Copilot SDK](https://docs.github.com/en/copilot/how-tos/copilot-sdk/troubleshooting/compatibility)
- [Copilot CLI](https://docs.github.com/en/copilot/reference/copilot-cli-reference/cli-command-reference)
- [OpenCode Server](https://opencode.ai/docs/server/)
- [OpenCode CLI](https://dev.opencode.ai/docs/cli)
- [Tests, documentation decision and open evidence](adapter-extension-evidence.md)

Targets: CEFR B2 and applicable WCAG 2.2 AA. Text commands and explanations
carry the information; color is not required. Static checks do not replace
screen-reader, Braille or full accessibility acceptance.
