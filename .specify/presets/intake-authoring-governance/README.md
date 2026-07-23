# Intake Authoring Governance Preset

Optional, stackable intake-authoring governance for GitHub Spec Kit. Version
`0.1.1` turns prompts, pasted planning text, and explicitly ordered UTF-8 text
files into exactly one traceable Markdown intake plus one JSON receipt.

*Optional, stackable intake-authoring governance for GitHub Spec Kit. Version
`0.1.1` turns prompts, pasted plans, and ordered UTF-8 text files into one
traceable Markdown intake plus one JSON receipt.*

Recommended priority: `64`, after Agent Parity (`60`) and before Intake Review
(`65`), Autonomous Run (`70`), and Parallel Autonomous Run (`80`). Spec Kit
`>=0.8.3` is required.

## Why This Preset Exists / Warum dieses Preset existiert

Planungen beginnen oft als Chattext, Notiz oder mehrere Dateien. Direktes
Specify aus solchen Quellen kann Scope, Nicht-Ziele, Nachweise oder
Berechtigungen vermischen. Dieses Preset erzeugt zuerst einen lesbaren,
hashgebundenen Intake. Das getrennte Intake-Review-Preset entscheidet danach,
ob der Intake wirklich bereit ist.

*Plans often begin as chat text, notes, or several files. Running Specify
directly can mix scope, non-goals, evidence, and authority. This preset first
creates a readable, hash-bound intake. The separate Intake Review preset then
decides whether that intake is actually ready.*

Creating an intake starts no review, feature, implementation, campaign, commit,
push, pull request, or merge.

## Commands / Befehle

- `$speckit-intake-create`: erzeugt genau einen Intake und ein Receipt aus
  direktem Text, eingefuegter Planung und/oder geordneten Textdateien.
- `$speckit-intake-create-status`: prueft Receipt, Ziel, lokale Quellen und
  Prompt-Zustand read-only.

*`$speckit-intake-create` creates exactly one intake and receipt.
`$speckit-intake-create-status` verifies provenance and freshness without
writing.*

## Install

```bash
specify preset add \
  --from https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/archive/refs/tags/v0.1.1.zip \
  --priority 64
specify preset list
specify preset info intake-authoring-governance
specify preset resolve
```

The preset is optional. Installation alone does not require authoring or make
Intake Review mandatory. Repository policy chooses when either gate applies.

## Quick Start / Schnellstart

### Direct prompt / Direkter Prompt

```text
$speckit-intake-create Erstelle aus folgendem Text einen Intake mit dem Titel "Audit logging hardening": Die Anwendung soll Audit-Ereignisse nachvollziehbar speichern, aber keine Passwoerter, Tokens oder vollstaendigen Request-Bodies protokollieren.
```

### Pasted agent plan / Eingefuegte Agentenplanung

```text
$speckit-intake-create Wandle die folgende gemeinsam erarbeitete Planung in einen Spec-Kit-Intake um. Speichere das Ergebnis als intakes/order-import-validation.md.

[paste the planning text here]
```

### One source file / Eine Quelldatei

```text
$speckit-intake-create Nutze planning/order-import-notes.md als einzige Quelle und erzeuge genau einen Intake.
```

### Ordered files / Geordnete Dateien

```text
$speckit-intake-create Nutze in dieser Reihenfolge docs/problem.txt, docs/security-boundaries.md und docs/acceptance.yaml. Keine Datei hat stillen Vorrang. Frage bei Widerspruechen nach.
```

### Mixed sources / Gemischte Quellen

```text
$speckit-intake-create Nutze planning/baseline.md, danach den folgenden Zusatztext und zuletzt tests/expected-cases.txt. Der Zusatztext ist eine neue Stakeholder-Entscheidung, aber keine pauschale Ueberschreibregel: [text]
```

## Output / Ausgabe

The generic fallback writes:

```text
intakes/<slug>.md
specs/intake-authoring-receipts/<slug>.json
```

A repository profile may select another target, for example a root-level
`Lastenheft_*.md` or a learning-unit path. The receipt location remains stable
unless repository policy explicitly changes it.

The intake contains:

- identity and audience;
- purpose, current state, and target state;
- scope and non-goals;
- atomic requirements;
- security, privacy, accessibility, platform, language, and evidence rules;
- dependencies, risks, expected artifacts, and measurable acceptance;
- assumptions and open decisions;
- copy-ready Specify and Autonomous prompts.

## Clarification / Klaerung

The command asks only questions whose answers change scope, security, delivery,
acceptance, or another material contract. It asks exactly one at a time and no
more than five per pass.

Bleiben danach wesentliche Fragen offen, wird die Arbeit nicht verworfen. Das
Preset speichert einen klar markierten Entwurf:

```text
Status: NeedsClarification
Prompt state: Blocked
BLOCKED - DO NOT RUN
```

The blocked prompt sections contain canonical command IDs and open decision
IDs, but no executable invocation line. A later authorized update can complete
the same intake without losing provenance.

## Existing Files And Updates / Vorhandene Dateien und Updates

Existing targets are immutable by default. To update one, name the exact target
and explicitly authorize the update in the current request. The command hashes
the old target and receipt before writing and records both in `supersedes`.

```text
$speckit-intake-create Aktualisiere intakes/order-import-validation.md ausdruecklich auf Basis von planning/new-decision.md. Bewahre den bisherigen Scope, soweit die neue Entscheidung ihn nicht konkret aendert, und zeichne das alte Receipt auf.
```

General autonomy, an earlier delivery mode, or write permission elsewhere is
not update authority for an intake.

### Legacy adoption / Uebernahme bestehender Intakes

An existing intake created before this preset has no truthful receipt to
supersede. With explicit current update authority, use provenance mode
`LegacyAdoption`. Record the prior normalized target hash as an ordered source,
plus either its exact Git blob or a clearly bounded snapshot-only proof. Keep
the same target path and never fabricate an earlier receipt.

Ein bestehender Intake aus der Zeit vor diesem Preset besitzt kein
wahrheitsgemaesses Vorgänger-Receipt. Mit ausdruecklicher aktueller
Update-Autoritaet wird `LegacyAdoption` verwendet. Der vorherige normalisierte
Zielhash wird als geordnete Quelle und zusammen mit dem exakten Git-Blob oder
einer klar begrenzten Snapshot-Evidence festgehalten. Der Zielpfad bleibt
unveraendert; ein frueheres Receipt wird nicht erfunden.

## Source Contract / Quellenvertrag

- Sources are processed in the order supplied.
- No later source silently overrides an earlier one.
- Files must decode as strict UTF-8 and contain no NUL bytes.
- One UTF-8 BOM is removed and line endings are normalized only for hashing.
- PDF, DOCX, archives, images, and other binary containers are rejected.
- Large inputs are never silently truncated. The command stops and asks for a
  smaller or split source set when context cannot be handled safely.
- Source content is never executed as code or a command.
- Repository-local sources use relative paths. Explicit external sources use a
  safe label and snapshot hash, not a private absolute path.
- Credentials, private keys, access tokens, and unnecessary personal data
  block authoring instead of being copied into the intake.

## Language And Accessibility / Sprache und Barrierefreiheit

Repository guidance controls the language. Without a project rule, the
dominant source language is retained. Explanatory content targets CEFR B2.

Generated Markdown uses semantic headings, descriptive text, labelled tables,
stable reading order, and information that does not depend on color or visual
position. Applicable WCAG 2.2 Level AA expectations remain part of the intake,
including when the intake describes a later CLI, document, or user interface.

## Prompt Safety / Prompt-Sicherheit

The Specify prompt binds the exact saved intake and explicitly stops before
implementation or remote writes.

The Autonomous prompt uses the same intake and exactly one delivery mode:

- `LocalImplementation` by default;
- `PublishPR` only with explicit current authority;
- `MergeAndSync` only with explicit current authority.

No generated prompt infers bypass, secret access, provider administration,
check cancellation, or permission to start the next feature.

The visible invocation matches the active agent surface. The receipt also
stores portable command IDs:

```text
speckit.specify
speckit.autonomous
```

## Receipt / Receipt

The JSON receipt records normalized source and target hashes, source order,
profile, language policy, decisions, open IDs, question count, agent syntax,
delivery authority, prompt state, supersession, and the exact next action.

`ReadyForReview` means only that authoring is internally consistent. It is not
an Intake Review result. `NeedsClarification` means the saved document is a
blocked draft.

## Status / Statuspruefung

```text
$speckit-intake-create-status specs/intake-authoring-receipts/order-import-validation.json
```

Status is read-only and reports one of:

- `Current`
- `SourceDrift`
- `TargetDrift`
- `NeedsClarification`
- `InvalidReceipt`
- `Missing`

Inline and external sources are snapshot-only after creation. Status reports
that proof boundary rather than pretending to re-read an unavailable path.

## Review Handoff / Uebergabe an Review

After a valid `ReadyForReview` result, the command reports exactly one next
action:

```text
$speckit-intake-review intakes/order-import-validation.md
```

It does not run that command. Intake Review stays independent and may still
return `NeedsClarification`, `NeedsRemediation`, or `Rejected`.

## Composition / Komposition

Recommended optional stack:

```text
Agent Parity Governance             60
Intake Authoring Governance         64
Intake Review Governance            65
Autonomous Run Governance           70
Parallel Autonomous Run Governance  80
```

The public standard eight-preset profile remains unchanged. Projects can
install Authoring without Review, and Presets 7/8 remain usable without either
optional intake preset.

## Validation

Read-only validators are included for Bash and PowerShell:

```bash
bash scripts/validate-intake-authoring-receipt.sh \
  --receipt specs/intake-authoring-receipts/example.json \
  --repo .
```

```powershell
pwsh -NoProfile -File scripts/validate-intake-authoring-receipt.ps1 `
  -Receipt specs/intake-authoring-receipts/example.json `
  -Repo .
```

The validators grant no authoring, review, implementation, or remote authority.

## License

MIT License. See `LICENSE`.
