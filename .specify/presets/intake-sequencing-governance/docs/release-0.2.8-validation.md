# Patch 0.2.8: Installation / Installation

Documentation Impact: `UpdateRequired`. Owner: Thorsten Hindermann.
Zielgruppe: Preset-Nutzer und Katalog-Maintainer. Einstieg: README,
Installationsabschnitt und dieser verlinkte Nachweis. Kanonische Quellen:
README und preset.yml; Dokumentklasse: Release-/Bedienungsnachweis.
Distribution: Preset-Archiv; kein Home-Runtime-Sync oder Flotten-Rollout.
Wiedervorlage bei Aenderung des Installations- oder Katalogvertrags.

Der Patch ersetzt den mehrzeiligen Installationsbefehl durch eine direkt
auswertbare Zeile fuer das unveraenderliche v0.2.8-Tag-ZIP. Manifest und
Receipt-Vorlage binden 0.2.8. Der bestehende Integritaetstest prueft die exakte
Installationszeile unter LF/CRLF. Ausfuehrbare Validatoren, Routing, Commands,
Prioritaet 66 und Schemas bleiben unveraendert. Historische Evidence und
v0.2.7 werden nicht umgeschrieben.

Readers: preset users and catalog maintainers through the README installation
section and this linked release record. Canonical sources are README and
preset.yml. Distribution is the preset archive; no Home Runtime sync or fleet
rollout is included. Reevaluate when installation or catalog contracts change.

This patch changes only installation documentation, release metadata, the
receipt template version, and the existing integrity regression test. Runtime
validators, routing, commands, priority, and schemas remain unchanged.
Native macOS/Linux/Windows lifecycle CI must pass at the exact release-PR head;
final commit, run links, and archive SHA-256 are recorded in the release notes.
No product, risk, certification, or downstream execution approval is implied.
