# Intake Authoring Project Profile

## Identity

- Profile ID:
- Applies when:
- Target path rule:
- Language rule:
- Declared learner audience:
- Assumed prior knowledge:
- First-use terminology rule:
- Text-first dependency, status, decision, and next-action rule:
- URL-source rule:
- Series target and ordering rule:
- Requirements-governance configuration and portable role rule:
- Active-inventory rule: `DirectoryStrict` or `SeriesManifest`
- Existing-name and legacy-alias preservation rule:
- Archive and tombstone rule:

## Required Sections

List additions to the portable intake core. A profile cannot remove identity,
scope, non-goals, atomic requirements, governance, evidence, measurable
acceptance, assumptions, or copy-ready prompt sections.

## Naming And Ordering

Define title, filename, identifier, predecessor, and series-order rules. State
how ambiguity blocks authoring. A profile may lower URL and crawl limits but
cannot enable HTTP, authentication, private targets, cross-origin crawling, or
physical purge.

For a consolidated requirements collection, declare one BCP-47 documentation
language independently of implementation language and operating-system locale.
Resolve the four portable roles, six collection paths, canonical index, order
view, intake pattern, and bounded legacy aliases. Existing names remain unless
separate, current rename authority is present.

Use `DirectoryStrict` when the active directory contains only active intakes.
Use `SeriesManifest` for an established flat or mixed layout whose
hash-validated Series manifest defines series membership separately from physical active inventory. Neither
mode permits manually maintained counts as evidence.

## Quality Gates

Define repository-specific security, privacy, WCAG 2.2 AA, CEFR B2, platform,
learning, evidence, and delivery-authority requirements. Profiles cannot weaken
the preset's source, secret, overwrite, or authority protections.

When a project declares learner-facing content, record the audience and prior
knowledge explicitly. Do not assume Spec Kit experience by default. Require
technical and workflow terms to be explained on first use, and require ordered
text for dependencies, status, decisions, and next actions even when diagrams
are also present.

## Nutzungshilfe / Reader help

DE: Einen klar begrenzten Arbeitsauftrag mit Herkunft und Änderungsrechten vorbereiten. Diese Vorlage ist ein auszufüllender Starter, kein bestandener Nachweis. Vorhandene passende Evidence weiterverwenden.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/blob/main/docs/user-guide.md).

EN: Prepare a bounded work request with provenance and change authority. This template is a starter to complete, not passed evidence. Reuse suitable existing evidence.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
