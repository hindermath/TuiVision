# Unix Man-Page Template (section 1)

## Spec-Kit Run Evidence

- Feature / Spec ID:
- Spec-Kit phase: [specify / plan / tasks / implement / review / release]
- Branch / commit / PR:
- Run date:
- Evidence owner:
- Reviewer:
- Standards / criteria checked: Bash/PowerShell parity, Unix man-page completeness, PowerShell comment-based help, Verb-Noun Cmdlet discipline, dry-run/WhatIf equivalence
- Decision: [Applicable / N/A / Open]
- Evidence path:
- N/A rationale, if not applicable:
- Open follow-up owner and trigger:
- Re-evaluation trigger:
- Certification-readiness note: Use this record to prove that paired scripts, help surfaces, dry-run behavior, exit codes, and platform checks were reviewed in the current Spec-Kit run.

## Audit Evidence Matrix

| Checkpoint / control reference | Applicability | Evidence produced or linked | Result | Residual risk / rationale |
| --- | --- | --- | --- | --- |
| Spec-Kit run scope is identified | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| Standard-specific criteria are mapped | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| Evidence artefact path is recorded | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| N/A decisions are justified | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| Open findings have owner and trigger | [Applicable / N/A / Open] | | [OK / Open / N/A] | |

> Save as `docs/man/<name>.1`. Render with `man docs/man/<name>.1` or
> `mandoc docs/man/<name>.1 | less`.

```groff
.TH <NAME> 1 "YYYY-MM-DD" "<project> <version>" "User Commands"

.SH NAME
<name> \- short one-line description

.SH SYNOPSIS
.B <name>
.RI [ options ]
.RI [ arguments ]

.SH DESCRIPTION
A short paragraph explaining what the command does, when to use it,
and what its effect is on the system. Avoid implementation detail;
describe observable behaviour.

.SH OPTIONS
.TP
.BR \-h ", " \-\-help
Show help and exit.
.TP
.BR \-\-dry\-run
Show what would happen without making changes. Mirrors the PowerShell
`-WhatIf` switch.
.TP
.BI \-\-option= value
Description of the option.

.SH ENVIRONMENT
.TP
.B HOME
User home directory; required.
.TP
.B PATH
Required executables: `git`, `gh`, …

.SH EXIT STATUS
.TP
.B 0
Success.
.TP
.B 1
Generic error.
.TP
.B 2
Misuse of arguments.

.SH EXAMPLES
.TP
Preview a change:
.B <name> --dry-run --option=value
.TP
Apply the change:
.B <name> --option=value

.SH SEE ALSO
.BR <related-command> (1),
.BR git (1)

.SH AUTHORS
<author name> <author@example>
```

## Notes

- Keep the synopsis line under 78 columns where possible.
- Mirror the option list with the PowerShell variant; matching
  arguments must produce matching behaviour.
- For projects that ship bilingual documentation, a German companion
  may live at `docs/man/<name>.1.de` or be embedded in the
  `.SH DESCRIPTION` section as a second paragraph (DE first,
  EN second).
