# v0.2.6: Verbindliche CI-Exitcodes / Binding CI exit codes

Documentation Impact: **UpdateRequired**. Owner: Preset maintainers.

## Deutsch

Vorbereitung, kein Veröffentlichungsnachweis: `candidate_status = Prepared`,
`release_status = NotEstablishedAtPreparation`. Erst Release-Seite, exakter
Tag-Commit und extern geprüfter ZIP-Hash belegen die Veröffentlichung.

Patch v0.2.6 prüft in den GitHub-Actions-Schritten den Exitcode jedes
PowerShell-Testprozesses unmittelbar. Damit kann ein später erfolgreich
ausgeführter Befehl einen zuvor fehlgeschlagenen Test nicht mehr verdecken.
Negativtests injizieren Fehler und weisen nach, dass die Workflows den
ursprünglichen Fehlercode zuverlässig weitergeben.

Resolver, Adapter, Modellauswahl, Profile und die Semantik von Codex und `agy`
bleiben bytegleich zu v0.2.5. Der Patch ändert ausschließlich die
CI-Auswertungsgrenze und ihre Regressionstests; er erteilt keine Modell-,
Installations- oder Rolloutfreigabe. Kanonische Quellen sind die Workflows,
`tests/test-ci-exit-codes.ps1` und `preset.yml`. Zielgruppen sind Maintainer und
Reviewer; Distribution ist die Preset-Quelle. Re-Evaluation erfolgt bei
Änderungen an Workflows, Testprozessen oder Paketversion.

## English

Preparation only, not publication evidence: `candidate_status = Prepared` and
`release_status = NotEstablishedAtPreparation`. Publication requires the
release page, exact tag commit and an externally verified ZIP hash.

Patch v0.2.6 checks the exit code of every PowerShell test process immediately
inside the GitHub Actions steps. A later successful command can no longer mask
an earlier failed test. Negative tests inject failures and prove that the
workflows reliably propagate the original non-zero exit code.

Resolvers, adapters, model selection, profiles, and Codex/`agy` semantics are
byte-identical to v0.2.5. The patch changes only the CI evaluation boundary and
its regression tests; it grants no model, installation, or rollout authority.
Canonical sources are the workflows, `tests/test-ci-exit-codes.ps1`, and
`preset.yml`. Audiences are maintainers and reviewers; distribution is the
preset source. Re-evaluate when workflows, test processes, or the package
version change.

[Structured preparation](release-0.2.6-preparation.json) ·
[CI exit-code integrity](ci-exit-code-integrity.md)
