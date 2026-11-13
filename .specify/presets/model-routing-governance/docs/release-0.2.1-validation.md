# v0.2.1: PowerShell-Testkompatibilität / PowerShell test compatibility

DE: Die Level-0-Installation von v0.2.0 meldete zwei strikte PSScriptAnalyzer-
Warnungen in Testdateien: `$profile` kollidiert mit PowerShells automatischer
Variable `$PROFILE`. Beide lokalen Testvariablen heißen jetzt `$profileData`.
Die Tests bleiben inhaltlich gleich. Resolver, Discovery, Modellauswahl,
CLI-Argumente, Profilformat und generierte Profile ändern sich nicht.
Insbesondere bleiben Codex und `agy` unverändert. Generatorversion 0.2.0 für
neue Profile und Legacy-Generatorversion 0.1.2 bleiben bewusst erhalten.

EN: Strict Level-0 installation analysis of v0.2.0 found two test-file warnings:
`$profile` collides with PowerShell's automatic `$PROFILE` variable. Both test-
local variables are now `$profileData`. Test behavior is unchanged. Resolver,
discovery, selection, CLI arguments, profile format and generated profiles are
unchanged, including Codex/agy. New-profile generator version 0.2.0 and legacy
generator 0.1.2 are intentionally retained.

DE: Documentation Impact **UpdateRequired**, Owner Thorsten Hindermann. Quelle:
zwei Testdateien und Paketmanifest. Leserpfad README → dieser Nachweis →
Nutzungsleitfäden. Zielgruppe Maintainer und Lernende aller vier IHK-IT-Berufe;
DE zuerst, gleichwertiges EN, CEFR B2, anwendbare WCAG 2.2 AA. Distribution:
Preset-Paket; kein direkter Home-Sync. Neu prüfen bei Paket-/Test-/Analyzer-
Änderung. Es wird kein Diagramm benötigt, da kein Ablauf geändert wird.

EN: Documentation Impact **UpdateRequired**, owner Thorsten Hindermann.
Sources are the two tests and package manifest. Reader path: README → this
record → user guides. Audience: maintainers and all four IHK IT occupations;
German first, equivalent English, B2 and applicable WCAG 2.2 AA. Distribution:
preset package; no direct Home sync. Re-evaluate on package/test/analyzer drift.
No diagram is needed because execution flow is unchanged.

DE: Dies ist Paketvorbereitung, kein Veröffentlichungsnachweis. Der bestehende
Tag v0.2.0 und sein Archiv bleiben unverändert. Aktuelle Veröffentlichung wird
durch Release-Seite, exakten Tag-Commit und extern verifiziertes Archiv belegt.
Der [Nachweis 0.1.6](release-0.1.6-validation.md) bleibt historische Evidence.

EN: This is preparation, not publication evidence. Existing v0.2.0 tag/archive
remain unchanged. Actual publication requires the release page, exact tag
commit and independently verified archive. Earlier publication evidence remains
historical. [Structured preparation](release-0.2.1-preparation.json).

Checks: all adapter/selection/transport/documentation/history regressions plus
strict PowerShell analysis of every package-owned PowerShell file using the
consumer's pinned settings. Actual outcomes belong in the PR/release notes.
No real account/model access, SDK installation or profile refresh.
