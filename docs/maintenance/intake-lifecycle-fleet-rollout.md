# Intake-Lifecycle-Flottenrollout / Intake lifecycle fleet rollout

Datum: 2026-09-13. Owner: Repository Maintainer (Thorsten Hindermann).

DE: Die vorhandenen Intake-Presets werden auf Authoring 0.3.2, Review 0.2.2
und Sequencing 0.2.4 aktualisiert. Die optionalen Profilbindungen verwenden
dieselben Releases. Die Standard-Achtermatrix und andere Presets bleiben erhalten.
Die Installation fuehrt keine Intakes aus und verschiebt keine fachlichen Dateien.
Completed-Ziele gehoeren ins konfigurierte Archiv; ausfuehrbare Ziele in die
aktive Collection. Physischer Aktivbestand und aktive Serienziele werden getrennt
gezaehlt. Historische Receipts brauchen eine eindeutige Archiv-/Hashbindung.

EN: Existing intake presets are updated to Authoring 0.3.2, Review 0.2.2 and
Sequencing 0.2.4. Optional profiles bind the same releases; the standard eight
and other presets remain unchanged. Installation neither executes intakes nor
moves requirements. Completed members belong in the configured archive and
executable members in the active collection. Physical active inventory and active
series membership are separate counts. Historical receipts require unique
archive/hash evidence.

## Evidence und Dokumentationsauswirkung / Evidence and documentation impact

[intake-lifecycle-fleet-rollout.json](intake-lifecycle-fleet-rollout.json) erfasst
Paketbasis, lokale Erweiterungen und Pruefergebnisse. Technische Gates, Review-
Befunde und der exakte PR-Head werden vor Merge geprueft. Der ausdrueckliche
Auftrag umfasst MergeAndSync mit Admin-Bypass nach technischen Gates; er
behauptet keine unabhaengige menschliche Freigabe und keine Produktkonformitaet.

Entscheidung: UpdateRequired. Zielgruppen: Maintainer und Agenten; Leserpfad:
aktuelle Preset-/Agent-Guidance, dieses Dokument, JSON, PR-Gates. Kanonische
Quellen: veroeffentlichte Preset-Tags und bestehende optionale Profile. DE/EN
stehen gemeinsam hier; Darstellung ist textorientiert und ohne Farbabhaengigkeit.
Distribution: repository-lokale Presets und Evidence, verwaltete Profilkopien.
Die Level-0-Profilkonfiguration wird separat ueber den geprueften Home-Runtime-
Sync verteilt; Preset-Verzeichnisse werden nicht nach Home kopiert. Statistik
folgt der vorhandenen Repository-Konfiguration. Re-Evaluation bei Versions-,
Profil-, Lifecycle- oder lokaler Erweiterungsdrift.

The linked JSON records released package provenance, local extensions and
validation results. Technical gates, review findings and the exact PR head are
checked before the authorized MergeAndSync/admin-bypass delivery. This does not
claim independent human approval or product conformity. Documentation impact is
UpdateRequired, owned by the maintainer, with colocated German/English text.
Reassess after version, profile, lifecycle or local-extension changes.
