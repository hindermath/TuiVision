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

## Korrigierter Patchstand / Corrected patch releases

Die Flottenreviews deckten physische Collection-Aliase und Receipt-Pfadfluchten
auf. Die zentral korrigierten Releases sind Authoring 0.3.4, Review 0.2.3 und
Sequencing 0.2.6. Negative Tests reproduzierten die Befunde vor der Korrektur;
anschliessend bestehen die nativen Release-Suiten auf macOS, Linux und Windows.
Die aktuellen Profile, Source-Locks und vorhandenen Bootstrap-/Agent-Vorlagen
verwenden diese Versionen. Die mitgelieferten verschachtelten Workflows sind
Quellmetadaten der Presets und aktivieren keine Verbraucher-Jobs; die technischen
PR-Gates stammen aus den Root-Workflows des jeweiligen Verbraucher-Repositories.

Fleet review found physical collection aliases and receipt path escapes. The
centrally corrected releases are Authoring 0.3.4, Review 0.2.3 and Sequencing 0.2.6.
Shipped JSON templates are parsed and their generator versions checked in native CI. Negative tests reproduced the findings before correction; native release suites
then pass on macOS, Linux and Windows. Existing profiles, source locks and
bootstrap/agent templates bind these versions. Packaged nested workflows are
preset-source metadata, not consumer jobs; consumer PR gates use root workflows.

Documentation Impact remains UpdateRequired. Re-evaluation includes portability,
physical path aliases, source/hash bindings and local overlays. Project lifecycle
inventory findings remain separate from successful package/regression checks.
