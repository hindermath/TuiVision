# Autonomous Run Governance v0.4.4

Stand / Date: 2026-09-12. Owner: Thorsten Hindermann.

Die bestehende, noch offene v0.4.3-Integration wird auf v0.4.4 fortgeschrieben. Das unveraenderliche Release bindet im Staged-Modus Pfade, regulaere Dateitypen und Bytes direkt an den Git-Index. Umbenennungen werden als Loeschung plus Hinzufuegung inventarisiert; Symlinks, Gitlinks und Konflikteintraege bleiben blockierend. Die historische Whitespace-Regel aus v0.4.3 bleibt erhalten. Standardmaessige Ablehnung bleibt bestehen; Prioritaet und andere Presets bleiben erhalten. Die Installation startet keinen autonomen Lauf.

The pending v0.4.3 integration advances to v0.4.4. The immutable release binds staged paths, regular-file types, and bytes directly to the Git index. Renames are inventoried as a deletion plus an addition; symlinks, gitlinks, and conflicted entries remain blocking. The v0.4.3 historical-whitespace rule remains in force. Default rejection remains in force; priority and other presets are preserved. Installation starts no autonomous run.

Source: https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/releases/tag/v0.4.4

Documentation Impact: UpdateRequired. Audience: maintainers and agents. Reader path: preset registry and configuration to this integration record. Canonical product source: the standalone release above. Integration owner: Thorsten Hindermann. ActiveSemantic, German first / English second. Text-only guidance; no colour-dependent meaning. No application runtime, build or MSL decision changes. Repository-local integration; no Home distribution from this repository. Reevaluate on release, validator-contract or local evidence changes.

Validation: published package installation, preserved preset registry entries, release evidence-integrity regression suite, Bash syntax, exact staged candidate and repository CI.

Delivery authority: MergeAndSync; admin bypass only for formal merge protection after material checks pass and all review threads are resolved. Existing runs retain their own explicit authority; no historical-whitespace exception is granted by this update.
