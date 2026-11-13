# Patch 0.1.5: Aktive Metadaten / Active Metadata

Documentation Impact: `UpdateRequired`. Owner: Thorsten Hindermann.
Zielgruppen: Preset-Nutzende und Maintainer. Leserpfad: README →
Evidence-Vertrag → Manpage → dieser Release-Nachweis. Kanonische Quellen:
beide Validatoren und `tests/test-secure-development-assurance.ps1`.
Dokumentklasse: Bedienung, Vertragspräzisierung und Release-Evidence;
Sprachpartner hier DE zuerst, EN danach. Distribution: Preset-Quellrepository
und neues Tag-ZIP; keine Änderung historischer Releases oder Evidence.
Re-Evaluation bei Parser-, Metadaten-, Schema- oder Verbraucheränderung.

0.1.4 konnte passende Teiltexte im Dokument als Versionsnachweis akzeptieren,
obwohl die aktive Version falsch war. Hashbindung allein verhindert das nicht:
auch ein inkonsistentes Dokument kann korrekt gehasht sein. 0.1.5 prüft in
beiden Shells den exakten aktiven Wert und die Eindeutigkeit von Version,
Checklisten-Dokument-ID und Sammelband-Baseline. Die Richtlinie wird in DE
und EN abschnittsgebunden an ihrer jeweils ersten Metadatentabelle geprüft.
Alte oder nur einsprachige Dokumente benötigen die dokumentierte Struktur.
Diese Korrektur ist strenger als 0.1.4; ein bislang grünes inkonsistentes
Dokument kann jetzt berechtigt blockieren. Historische Nachweise bleiben
unverändert; bei erforderlicher Migration neue Evidence anlegen.

Regressionen binden alle manipulierten Dokumente neu an gültige Hashes.
47 Negativfälle je Shell decken acht Felder und sechs Tabellen-/Abschnitts-
grenzen ab. Beide Shells müssen Exit 2 mit richtigem Pfad und Feld liefern;
Byte-Snapshots belegen unveränderte Evidence. Historien, normale Textnennungen,
BOM/CRLF und die bisherigen Gate-/Installationsprüfungen bleiben positiv.
Der erste neue Test reproduzierte gegen die alte Implementierung den
False-positive „stale-body“. Tatsächliche Ergebnisse, exakter Head, native
CI, Release-Archivhash und Level-0-Rollout werden im PR dokumentiert.

Audience: users and maintainers. README, contract and manpage explain the
canonical validators and tests; German/English guidance is maintained
together. Version 0.1.5 replaces substring checks with exact, unique active
metadata checks. Both guideline language sections are required. Previously
accepted inconsistent or monolingual sources may now block: migrate sources
explicitly and create fresh evidence while preserving historical records.
This does not alter hash normalization, evidence schema or human authority.

Each negative fixture refreshes valid hash bindings before validation, so a
hash failure cannot hide a metadata false positive. Forty-seven cases per shell
check path/field diagnostics and read-only snapshots. Existing history,
narrative, BOM/CRLF, gate and generated-installation contracts remain covered.
Run native macOS Bash first, then PowerShell parity and configured native CI.
PR evidence records actual outcomes; this procedure alone asserts no pass.
The first full Windows CI run of the expanded suite reached the old fifteen-
minute job limit and was cancelled (run 39443623598). It is not a passing gate.
The package workflow now allows thirty minutes; assertions and test scope
are unchanged. Fresh three-platform CI on the updated head is required.

Delivery authority permits package publication and a Level-0 rollout only.
Other consumers and catalog submission remain separate. Home Runtime need
must follow the Level-0 distribution manifest, not the package version alone.

DE: Eingebettete Checklisten-Metadaten im Sammelband behalten ihre eigene Baseline; nur der Sammelband-Header wird gegen die Manifest-Baseline geprüft. Doppelte Baseline-Felder innerhalb eines eingebetteten Blocks bleiben unzulässig. Die Positivfixture bildet zwölf eingebettete Blöcke mit bewusst abweichender eigener Baseline ab. / EN: Embedded checklist blocks retain their own baseline; the compendium header alone binds the manifest baseline. Duplicate baselines inside an embedded block remain invalid. The positive fixture includes twelve embedded blocks with intentionally different baselines.
