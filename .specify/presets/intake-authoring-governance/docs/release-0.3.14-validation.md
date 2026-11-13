# Standalone-Archivauflösung 0.3.14 / Standalone archive resolution 0.3.14

<!-- current-package: 0.3.14 -->

<div lang="de">

## Deutsch

Diese Patch-Version korrigiert die Suche nach unveränderten archivierten
Intakes, die eigenständig abgeschlossen wurden und nicht zu einer Serie
gehören. Die vorherige Fassung erlaubte eigenständige Receipts, suchte deren
Archivdateien aber ausschließlich unter den Serienmitgliedern.

Die Suche bleibt an die konfigurierte Archivsammlung, die physische
Repository-Grenze, den Dateinamen und den exakten normalisierten SHA-256
gebunden. Genau ein Treffer ist erforderlich. Eine bestehende gleichnamige
Serienbindung darf nicht durch freie Archivsuche umgangen werden. An eine
Serie gebundene Receipts verwenden weiterhin ausschließlich diese Serie.
Historische Receipts werden nicht umgeschrieben. Schema, Intake-Lifecycle,
Lieferautorität und Ausführungsrechte ändern sich nicht.

Der konkrete Standalone-Fall aus TuiVision scheitert vor der Korrektur mit
RIG018 und wird danach auf die vorhandene bytegleiche Archivdatei aufgelöst.
Die Regression deckt zusätzlich fehlende Serienmitgliedschaft, Mehrdeutigkeit
und Schreibfreiheit ab. Die vollständigen bestehenden Bash-/PowerShell- und
Reader-Path-Prüfungen müssen am veröffentlichten PR-Head auf Linux, macOS und
Windows erfolgreich sein. Abgebrochene lokale Volltests sind kein Pass.

[Vorbereitung](release-0.3.14-preparation.json) ist kein Veröffentlichungsnachweis.
Erst der [GitHub Release v0.3.14](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/releases/tag/v0.3.14)
und die danach bytegeprüften Tag- und Archivnachweise belegen die Lieferung.
Alte Tags und Nachweise bleiben erhalten. Die neue Vorlage bindet 0.3.14;
beide Validatoren akzeptieren 0.3.13 zusätzlich als bekannte Schema-2-Version.

</div>

<div lang="en">

## English

This patch fixes lookup of unchanged archived intakes that were completed
independently and are not members of a series. The previous version accepted
standalone receipts but searched only the series members for their archives.

Lookup remains bound to the configured archive collection, the physical
repository boundary, the file name and the exact normalized SHA-256. Exactly
one match is required. An existing same-name series binding cannot be bypassed
through archive discovery. Series-bound receipts still use only their series.
Historical receipts are not rewritten. The schema, intake lifecycle, delivery
authority and execution permissions remain unchanged.

The concrete standalone TuiVision case fails with RIG018 before the fix and
resolves to the existing byte-identical archive afterwards. Regression tests
also cover missing series membership, ambiguity and zero writes. Existing
Bash/PowerShell and reader-path checks must pass on Linux, macOS and Windows
at the published PR head. Interrupted local full suites are not passes.

[Preparation](release-0.3.14-preparation.json) is not publication evidence. The
[GitHub release v0.3.14](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/releases/tag/v0.3.14)
and subsequent byte-verified tag/archive records establish delivery. Old tags
and records remain. The new template binds 0.3.14; both validators additionally
accept 0.3.13 as a known schema-2 generator version.

</div>

Documentation Impact: UpdateRequired. Owner: Thorsten Hindermann.
Readers: preset users and maintainers -> README -> matching DE/EN user guide
-> this source/runtime and publication boundary. Canonical source:
resolve-intake-archive-target.py, lifecycle tests, preset.yml and generator
bindings. CEFR B2; applicable WCAG 2.2 AA; text-only links and equivalent
language partners. Distribution: new immutable patch archive, explicit
Level-0 binding, manifest-bound Home runtime and the nine authorised public
Level-2 consumers. No Level-1 rollout, Spec Kit feature run, model call or
new risk acceptance. Practical screen-reader/Braille tests are not claimed.
Re-evaluate after source, archive, caller, binding or lifecycle changes.
