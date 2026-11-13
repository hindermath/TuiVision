# User guide: intake-authoring-governance

<!-- current-package: 0.3.13 -->

Package identity: **v0.3.13**. [Preparation and publication boundary](release-0.3.13-validation.md). The release page, not this preparation snapshot, proves publication and the external archive hash. Earlier version records below remain historical.

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](release-0.3.9-validation.md).

The linked base-package evidence remains historical; this package includes the source corrections as a new version.

[Deutsch](user-guide.md) · [Preset reference](../README.md)

Historical base package **v0.3.9**. Verify the actually published version on the [release page](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/releases). This guide grants no merge, release or project authority.

Contents: [Start](#start) · [Example](#example) · [Terms](#terms) · [Routing](#routing) · [Troubleshooting](#help) · [References](#references)

<a id="start"></a>

## Start and audience

Prepare a bounded work request with provenance and change authority.

For apprentices in all four IHK IT occupations: IT specialists (application development, systems integration, data/process analysis, digital networking), IT systems electronics technicians, digitalisation management specialists and IT systems management specialists. Also for developers without specialist governance knowledge. You do not need to edit hashes or invent model IDs to ask for the next step. Ask the maintainer to check technical bindings.

Use an already initialized Spec Kit target repository. Read the prerequisites and installation instructions in the preset reference first; replace example paths. A template is a starter, not completed evidence. Installation adds instructions and files; it does not start a feature or grant approval.

Input: Name one exact source, operation, target and project policy.

<a id="example"></a>

## One complete example

1. Describe the bounded task and existing files. Do not silently widen scope.
2. Use the following example in the agent chat. It is a prompt, not a shell command:

~~~text
$speckit-intake-create Propose exactly one intake from planning/order-import-notes.md. Name target and receipt paths; obtain my approval before writing.
~~~

3. After separately authorized execution: intake, provenance receipt and matching status output. A finished intake is not permission for a feature run.
4. Check the result and recorded next action. Changes, repair, feature execution and delivery each need their applicable authority. A passing technical check is not human acceptance.

Read-only installation check in the initialized target repository:

~~~text
specify preset info intake-authoring-governance
specify preset resolve intake-governance-config-template
~~~

Resolve reports the selected template source; it does not fill in or execute that template.

<a id="terms"></a>

## Terms and evidence

- **Intake:** Bounded work request before feature planning.
- **Receipt:** Record of source, target, operation and bindings, not approval for later work.
- **Tombstone:** Record of logical deletion rather than silently removing history.

Where the existing contract uses these values: Applicable means relevant, not passed; N/A needs a reason; Open needs the recorded next action and responsible person. This explanation adds no fields to existing schemas. Keep genuine historical evidence; never edit a hash or approval just to make a check pass.

<a id="routing"></a>

## Stable routing and explicit actions

| Command | Declared role |
|---|---|
| `speckit.intake-create` | `frontier-reasoning` |
| `speckit.intake-read` | `fast-mechanical` |
| `speckit.intake-update` | `frontier-reasoning` |
| `speckit.intake-delete` | `frontier-reasoning` |
| `speckit.intake-create-status` | `fast-mechanical` |

Roles describe tasks, not a model name or quality guarantee. Read the actual model-routing.json contract; generic examples such as coding-review do not override a stronger command requirement. The existing autonomous runner retains the strongest overlapping role, independently of installation priority. Merely installing a role contract does not switch a live chat or configure personal profiles.

A model-free script check and an agent assessment are different activities. Model calls, including model-access preflights, can consume quota even when they are read-only. No probe or provider switch is authorized by this documentation. No new dynamic-routing implementation is included.

Documentation target combination: Authoring **0.3.10**, Review **0.2.7**, Sequencing **0.2.11**. This candidate combination is not a claimed field test. Verified tests are listed separately in the PR.

<a id="help"></a>

## Troubleshooting and safe next action

Do not rewrite a receipt just to hide source/hash drift. Relevant PowerShell checks also need Python; a missing suitable Python is not a passing test.

If a command or template is missing, inspect preset info and the manifest below. Check the documented minimum tools and enabled installation. Resolve ambiguity with the maintainer; do not change policy, disable validators or claim an unperformed test. A safety blocker remains a blocker.

This language/accessibility pass targets CEFR B2 and applicable WCAG 2.2 AA. Static tests do not certify that target. Screen-reader, Braille and representative-user tests not actually performed remain unverified.

<a id="references"></a>

## Templates and deeper reference

- [`intake-governance-config-template`](../templates/intake-governance-config-template.json)
- [`intake-governance-migration-journal-template`](../templates/intake-governance-migration-journal-template.json)
- [`intake-authoring-policy-template`](../templates/intake-authoring-policy-template.json)
- [`intake-authoring-receipt-template`](../templates/intake-authoring-receipt-template.json)
- [`intake-authoring-series-template`](../templates/intake-authoring-series-template.json)
- [`intake-authoring-operation-template`](../templates/intake-authoring-operation-template.json)
- [`intake-authoring-tombstone-template`](../templates/intake-authoring-tombstone-template.json)
- [`intake-authoring-intake-template`](../templates/intake-template.md)
- [`intake-authoring-profile-template`](../templates/project-profile-template.md)
- [`intake-authoring-runbook-template`](../templates/intake-authoring-runbook-template.md)
- [`intake-authoring-field-validation-summary`](../templates/field-validation-summary.md)

These names identify available starters; they are not an instruction to create every file. Reuse existing suitable evidence. JSON field names remain technical identifiers, not translated keys.

- [Manifest and prerequisites](../preset.yml)
- [Stable command-role contract](../model-routing.json)
- [Documentation scope, review and test limits](documentation-closeout.md)
