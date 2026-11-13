# v0.2.13: Korrekte Release-Beschreibung / Accurate release description

Documentation Impact: **UpdateRequired**. Owner: Thorsten Hindermann.

## Deutsch

Das README von v0.2.12 behauptet im aktuellen Paketabschnitt noch, die
Sequencing-Logik sei gegenüber 0.2.7 unverändert. Das widerspricht den in
0.2.12 ausgelieferten Pfad-, Symlink-, Abschlussstatus- und Archivkettenprüfungen.
v0.2.13 korrigiert diese Aussage und den veralteten Patch-Verweis gemeinsam
auf Deutsch und Englisch. Paketversion, Installationsbefehl und aktuelle
Leserpfade werden fortgeschrieben. Historische Nachweise bleiben erhalten.

Skripte, Commands, Templates, Rollen, Priorität und Schemas bleiben bytegleich
zu v0.2.12. Die Receipt-Generatorversion bleibt 0.2.12; sie ist von der
Paketversion getrennt. Die neue Regression verwirft das tatsächliche README
des Tags v0.2.12 sowie getrennte DE-/EN-Negativfälle.

Vorbereitungsstand: `candidate_status = Prepared`,
`release_status = NotEstablishedAtPreparation`. Merge, Veröffentlichung und
Katalogeinreichung stehen aus; die Vorbereitung belegt keine Veröffentlichung.

## English

The current package section of the v0.2.12 README incorrectly says sequencing
behavior is unchanged from 0.2.7, despite the path, symlink, completion-state
and archive-chain checks shipped in 0.2.12. v0.2.13 corrects this claim and the
stale patch link in both languages and updates package/install reader paths.
Historical evidence is preserved.

Scripts, commands, templates, roles, priority and schemas remain byte-identical
to v0.2.12. Receipt generator version 0.2.12 remains separate from package
version. A regression rejects the actual v0.2.12 README and separate German
and English negative cases. Preparation does not prove merge or publication.

Canonical sources: README, preset.yml and documentation tests. Audience:
operators, catalog reviewers and maintainers. Navigation: README and paired
user guides. Distribution: preset source/package; no Home sync in this task.
Re-evaluate after source review, publication and any security-contract change.

[Structured preparation](release-0.2.13-preparation.json)
