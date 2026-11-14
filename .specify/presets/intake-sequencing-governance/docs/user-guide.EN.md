# User guide: intake-sequencing-governance

<!-- current-package: 0.2.14 -->

Package identity: **v0.2.14**. [Preparation and publication boundary](release-0.2.14-validation.md). The release page, not this preparation snapshot, proves publication and the external archive hash. Earlier version records below remain historical.

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](release-0.2.10-validation.md).

The linked base-package evidence remains historical; this package includes the source corrections as a new version.

[Deutsch](user-guide.md) · [Preset reference](../README.md)

Historical base package **v0.2.10**. Verify the actually published version on the [release page](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/releases). This guide grants no merge, release or project authority.

Contents: [Start](#start) · [Example](#example) · [Terms](#terms) · [Routing](#routing) · [Troubleshooting](#help) · [References](#references)

<a id="start"></a>

## Start and audience

Explain dependencies and order of existing work requests without starting their work.

For apprentices in all four IHK IT occupations: IT specialists (application development, systems integration, data/process analysis, digital networking), IT systems electronics technicians, digitalisation management specialists and IT systems management specialists. Also for developers without specialist governance knowledge. You do not need to edit hashes or invent model IDs to ask for the next step. Ask the maintainer to check technical bindings.

Use an already initialized Spec Kit target repository. Read the prerequisites and installation instructions in the preset reference first; replace example paths. A template is a starter, not completed evidence. Installation adds instructions and files; it does not start a feature or grant approval.

Input: Name existing intakes A and B and explain why B needs A.

<a id="example"></a>

## One complete example

1. Describe the bounded task and existing files. Do not silently widen scope.
2. Use the following example in the agent chat. It is a prompt, not a shell command:

~~~text
$speckit-intake-series-create Propose exactly one series with A before B. Explain the dependency and obtain approval before creation; then report status and next only.
~~~

3. Manifest, readable order and receipt explain targets, edges and status. Next reports candidates or blockers but starts no run.
4. Check the result and recorded next action. Changes, repair, feature execution and delivery each need their applicable authority. A passing technical check is not human acceptance.

Read-only installation check in the initialized target repository:

~~~text
specify preset info intake-sequencing-governance
specify preset resolve intake-series-policy-template
~~~

Resolve reports the selected template source; it does not fill in or execute that template.

<a id="terms"></a>

## Terms and evidence

- **DAG:** Directed dependency graph without cycles: a target cannot become its own predecessor.
- **Eligible:** Eligible under series rules, not execution approval.
- **Idle:** Valid empty state without active targets, not a reason to invent placeholders.

Where the existing contract uses these values: Applicable means relevant, not passed; N/A needs a reason; Open needs the recorded next action and responsible person. This explanation adds no fields to existing schemas. Keep genuine historical evidence; never edit a hash or approval just to make a check pass.

<a id="routing"></a>

## Stable routing and explicit actions

| Command | Declared role |
|---|---|
| `speckit.intake-series-create` | `frontier-reasoning` |
| `speckit.intake-series-read` | `fast-mechanical` |
| `speckit.intake-series-update` | `frontier-reasoning` |
| `speckit.intake-series-delete` | `frontier-reasoning` |
| `speckit.intake-series-status` | `fast-mechanical` |
| `speckit.intake-series-next` | `fast-mechanical` |

Roles describe tasks, not a model name or quality guarantee. Read the actual model-routing.json contract; generic examples such as coding-review do not override a stronger command requirement. The existing autonomous runner retains the strongest overlapping role, independently of installation priority. Merely installing a role contract does not switch a live chat or configure personal profiles.

A model-free script check and an agent assessment are different activities. Model calls, including model-access preflights, can consume quota even when they are read-only. No probe or provider switch is authorized by this documentation. No new dynamic-routing implementation is included.

Documentation target combination: Authoring **0.3.10**, Review **0.2.7**, Sequencing **0.2.11**. This candidate combination is not a claimed field test. Verified tests are listed separately in the PR.

<a id="help"></a>

## Troubleshooting and safe next action

Resolve RIG017 state/storage mismatches under authorized update authority. Do not confuse binding prerequisites with a preferred delivery order.

If a command or template is missing, inspect preset info and the manifest below. Check the documented minimum tools and enabled installation. Resolve ambiguity with the maintainer; do not change policy, disable validators or claim an unperformed test. A safety blocker remains a blocker.

This language/accessibility pass targets CEFR B2 and applicable WCAG 2.2 AA. Static tests do not certify that target. Screen-reader, Braille and representative-user tests not actually performed remain unverified.

<a id="references"></a>

## Templates and deeper reference

- [`intake-series-policy-template`](../templates/intake-series-policy-template.json)
- [`intake-series-manifest-template`](../templates/intake-series-manifest-template.json)
- [`intake-series-idle-manifest-template`](../templates/intake-series-idle-manifest-template.json)
- [`intake-series-receipt-template`](../templates/intake-series-receipt-template.json)
- [`intake-series-operation-template`](../templates/intake-series-operation-template.json)
- [`intake-series-tombstone-template`](../templates/intake-series-tombstone-template.json)
- [`intake-series-order-template`](../templates/intake-series-order-template.md)
- [`intake-series-checklist-template`](../templates/intake-series-checklist-template.md)
- [`intake-series-runbook-template`](../templates/intake-series-runbook-template.md)
- [`intake-series-field-validation-summary`](../templates/field-validation-summary.md)

These names identify available starters; they are not an instruction to create every file. Reuse existing suitable evidence. JSON field names remain technical identifiers, not translated keys.

- [Manifest and prerequisites](../preset.yml)
- [Stable command-role contract](../model-routing.json)
- [Documentation scope, review and test limits](documentation-closeout.md)
