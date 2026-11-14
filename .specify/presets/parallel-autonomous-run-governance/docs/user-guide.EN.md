# User guide: parallel-autonomous-run-governance

<!-- current-package: 0.2.13 -->

Package identity: **v0.2.13**. [Preparation and publication boundary](release-0.2.13-validation.md). The release page, not this preparation snapshot, proves publication and the external archive hash. Earlier version records below remain historical.

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](release-0.2.8-validation.md).

The linked base-package evidence remains historical; this package includes the source corrections as a new version.

[Deutsch](user-guide.md) · [Preset reference](../README.md)

Historical base package **v0.2.8**. Verify the actually published version on the [release page](https://github.com/hindermath/spec-kit-preset-parallel-autonomous-run-governance/releases). This guide grants no merge, release or project authority.

Contents: [Start](#start) · [Example](#example) · [Terms](#terms) · [Routing](#routing) · [Troubleshooting](#help) · [References](#references)

<a id="start"></a>

## Start and audience

Coordinate multiple authorized feature runs with isolated working copies.

For apprentices in all four IHK IT occupations: IT specialists (application development, systems integration, data/process analysis, digital networking), IT systems electronics technicians, digitalisation management specialists and IT systems management specialists. Also for developers without specialist governance knowledge. You do not need to edit hashes or invent model IDs to ask for the next step. Ask the maintainer to check technical bindings.

Use an already initialized Spec Kit target repository. Read the prerequisites and installation instructions in the preset reference first; replace example paths. A template is a starter, not completed evidence. Installation adds instructions and files; it does not start a feature or grant approval.

Input: Name the campaign, workers, shared write targets, delivery mode and local runners.

<a id="example"></a>

## One complete example

1. Describe the bounded task and existing files. Do not silently widen scope.
2. Use the following example in the agent chat. It is a prompt, not a shell command:

~~~text
$speckit-parallel-autonomous-status — read the campaign, report worker blockers and the next action only. Do not start workers or consolidate.
~~~

3. Campaign state, isolated workers, dependencies and consolidation boundary are visible. Parallel workers do not authorize concurrent writes to the same file.
4. Check the result and recorded next action. Changes, repair, feature execution and delivery each need their applicable authority. A passing technical check is not human acceptance.

Read-only installation check in the initialized target repository:

~~~text
specify preset info parallel-autonomous-run-governance
specify preset resolve parallel-campaign-template
~~~

Resolve reports the selected template source; it does not fill in or execute that template.

<a id="terms"></a>

## Terms and evidence

- **Worker:** Controlled single feature run, not an agent with unlimited authority.
- **Worktree:** Separate Git working copy for a defined task.
- **Konsolidierung:** Ordered integration of verified worker results under its own authority.

Where the existing contract uses these values: Applicable means relevant, not passed; N/A needs a reason; Open needs the recorded next action and responsible person. This explanation adds no fields to existing schemas. Keep genuine historical evidence; never edit a hash or approval just to make a check pass.

<a id="routing"></a>

## Stable routing and explicit actions

| Command | Declared role |
|---|---|
| `speckit.parallel-autonomous` | `frontier-reasoning` |
| `speckit.parallel-autonomous-status` | `fast-mechanical` |
| `speckit.parallel-autonomous-stop` | `script-only` |
| `speckit.parallel-autonomous-resume` | `frontier-reasoning` |
| `speckit.parallel-autonomous-consolidate` | `coding-review` |

Roles describe tasks, not a model name or quality guarantee. Read the actual model-routing.json contract; generic examples such as coding-review do not override a stronger command requirement. The existing autonomous runner retains the strongest overlapping role, independently of installation priority. Merely installing a role contract does not switch a live chat or configure personal profiles.

A model-free script check and an agent assessment are different activities. Model calls, including model-access preflights, can consume quota even when they are read-only. No probe or provider switch is authorized by this documentation. No new dynamic-routing implementation is included.

Documentation target combination: Autonomous **0.4.6**, Parallel **0.2.8**. Parallel's runtime minimum remains Autonomous **>=0.2.2**. Current examples do not claim a new live campaign test.

<a id="help"></a>

## Troubleshooting and safe next action

If a worker lacks Autonomous or its result is stale, do not treat it as complete. Explicitly serialize shared write targets.

If a command or template is missing, inspect preset info and the manifest below. Check the documented minimum tools and enabled installation. Resolve ambiguity with the maintainer; do not change policy, disable validators or claim an unperformed test. A safety blocker remains a blocker.

This language/accessibility pass targets CEFR B2 and applicable WCAG 2.2 AA. Static tests do not certify that target. Screen-reader, Braille and representative-user tests not actually performed remain unverified.

<a id="references"></a>

## Templates and deeper reference

- [`parallel-campaign-template`](../templates/parallel-campaign-template.json)
- [`parallel-runner-profiles-template`](../templates/parallel-runner-profiles-template.json)
- [`parallel-runner-profiles-examples`](../templates/parallel-runner-profiles-examples.json)
- [`parallel-campaign-state-template`](../templates/parallel-campaign-state-template.json)
- [`parallel-worker-result-template`](../templates/parallel-worker-result-template.json)
- [`parallel-autonomous-runbook-template`](../templates/parallel-autonomous-runbook-template.md)
- [`parallel-autonomous-evidence-template`](../templates/parallel-autonomous-evidence-template.md)
- [`parallel-autonomous-retrospective-template`](../templates/parallel-autonomous-retrospective-template.md)

These names identify available starters; they are not an instruction to create every file. Reuse existing suitable evidence. JSON field names remain technical identifiers, not translated keys.

- [Manifest and prerequisites](../preset.yml)
- [Stable command-role contract](../model-routing.json)
- [Documentation scope, review and test limits](documentation-closeout.md)
