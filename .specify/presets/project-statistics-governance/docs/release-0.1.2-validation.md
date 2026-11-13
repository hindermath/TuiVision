# Patch 0.1.2: Dokumentationskorrektur / Documentation correction

## Veröffentlichungsabschluss / Publication closeout

| Feld / Field | Wert / Value | Bedeutung / Meaning |
|---|---|---|
| `candidate_status` | `HistoricalCandidate` | Ursprünglicher Vorbereitungsstand / original preparation state |
| `release_status` | `Published` | Reguläres Release, geprüft zum angegebenen Zeitpunkt / regular release checked as of the timestamp |

Tag: [v0.1.2](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/tree/v0.1.2). Release-Commit / release commit:
`454554bafda574c1153e6be319c30e9787ac3867`. [GitHub Release](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/releases/tag/v0.1.2), veröffentlicht / published:
`2026-10-23T21:29:33.0000000+00:00`. Nachgeprüft / rechecked: `2026-10-24T10:55:51.0769380+00:00`.

[Unverändertes Tag-Archiv / unchanged tag archive](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/archive/refs/tags/v0.1.2.zip)
mit SHA-256 / with SHA-256: `6cc233b56033b4b7dc0abd831666a6f3b0f5b70b0c9b9a395c8dc78c9549bb95`.
[Maschinenlesbarer Status und Quellen / machine-readable status and sources](release-0.1.2-publication.json).

Das Paket ist veröffentlicht. Der folgende historische Vorbereitungstext
behält seinen damaligen Prüfbereich; seine Statuswörter gelten nicht als
aktueller Veröffentlichungsstand. Dieser spätere Dokumentationsnachlauf auf
main ändert weder Tag noch ZIP. Er ersetzt keine technische, fachliche oder
menschliche Freigabe und behauptet keine Consumer-Installation oder Katalogaufnahme.

<div lang="en">

The package is published. The historical preparation text below retains its
original assessment scope; its status words do not describe current publication.
This later documentation follow-up on main changes neither tag nor ZIP.
It replaces no technical, professional or human approval and claims no consumer
installation or catalogue acceptance.

</div>

Bezug / Tracking: [#342: Veröffentlichungsstatus](https://github.com/hindermath/home-baseline/issues/342)
und / and [#343: Verbesserungsprofile](https://github.com/hindermath/home-baseline/issues/343).

## Historischer Validierungsstand / Historical validation state

Unveränderter ursprünglicher Text / unchanged original text:

<!-- historical-candidate:start -->

**Vorbereitung zur Sichtung, nicht veröffentlicht / Prepared for review, not published.**

Download-URL und Dateiname verwenden 0.1.2. Der erwartete SHA-256 wird aus dem getrennten Release-Nachweis übernommen, nicht fälschlich aus 0.1.0 oder aus demselben ZIP. Es werden keine hochgeladenen SHA256SUMS-Assets behauptet. Beide Shell-Beispiele stoppen bei fehlender oder abweichender Prüfsumme.

*Download URL and filename use 0.1.2. Expected SHA-256 comes from the separate release record, not 0.1.0 or the same ZIP. Uploaded SHA256SUMS assets are not promised. Both shell examples reject absent or mismatching checksums.*

Documentation Impact: `UpdateRequired`. Owner: Thorsten Hindermann.
Zielgruppe: IT-Auszubildende aller vier IHK-IT-Berufe und Fachinformatik-Fachrichtungen
sowie Entwickelnde ohne Governance-Spezialwissen. DE zuerst, EN danach; B2 und
anwendbare WCAG 2.2 AA bleiben Zielmaßstab, keine durch Tests erteilte Abnahme.
Leserpfad: README → aktueller Ablauf → Fehlerhilfe → dieser Nachweis.
Kanonische Quellen: README, verlinkte Erstlaufanleitung soweit vorhanden,
preset.yml und tests/test-release-documentation.ps1. Bestehende Anker erhalten.

Dieser Patch behebt den für dieses stabile Preset bestätigten Dokumentations-
widerspruch. Keine neue Routing-Entwicklung, keine neuen Gates oder Berechtigungen.
Bei Authoring werden nur aktuelle Generator-Metadaten gebunden und bekannte
0.3.8-Receipts ausdrücklich erhalten; Schema- und Negativprüfungen bleiben streng.
Bei Statistics bleiben Methodik und internes Mess-Evidence-Format unverändert;
die Paketversion ist nicht mit deren historischer Formatversion gleichzusetzen.

Geplant: neue Dokumentationsregression, einschlägige bestehende lokale Tests
und konfigurierte native PR-CI. Tatsächliche Ergebnisse und Grenzen stehen im PR.
Dieser Vorbereitungstext behauptet keine noch nicht abgeschlossenen Prüfungen.
Exakter Head und Diff sind fachlich zu sichten. Kein Merge und keine Veröffentlichung
durch diesen Auftrag. Keine neuen Tags oder ZIPs; alte Releases bleiben unverändert.

Nach späterer gesonderter Freigabe: Merge, neues Tag-ZIP, SHA-256 und exakter
Release-Commit extern in den Release-Notes binden, Paket in einem isolierten
Testprojekt prüfen. Statistics darf die Prüfsumme seines eigenen ZIPs nicht
in dasselbe ZIP einbetten; fehlende externe Hash-Evidence bleibt blockierend.

English: This candidate fixes this stable preset's confirmed documentation
inconsistency. Audience includes the four IHK IT occupations/all IT-specialist
tracks and non-specialist developers. German first, equivalent English, B2 and
applicable WCAG 2.2 AA remain targets, not automated certification. README,
first-run guide where present, manifest and documentation regression are canonical.
Authoring binds current generator metadata while explicitly retaining known
0.3.8 schema-2 receipts; negative/schema gates remain strict. Statistics package
version does not change its historical internal measurement-evidence format.

Actual local tests, native CI, exact head and remaining limits are recorded in
the PR, not presumed here. Human diff review is still required. No merge, release,
tag/archive changes, model calls, installations into consumers, Home Runtime
sync, rollout or community submission. All require separate authority.
A later release must externally bind its actual archive hash and commit and
verify that archive in an isolated project; existing archives remain unchanged.

Distribution: preset source/documentation plus candidate metadata only.
Keine zentrale Profil-/Source-Lock-Änderung oder Installation in Level 0/Piloten.
Re-Evaluation nach Review, geändertem Head oder gesondert beauftragter Veröffentlichung.
<!-- historical-candidate:end -->

## Verbesserungsprofil / Improvement profile

Vergleich / comparison: [v0.1.0 → v0.1.2](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/compare/7e824ca8de11212aefdc5b05d7d05637f5343dab...454554bafda574c1153e6be319c30e9787ac3867).

Ausgang / from: 7e824ca8de11212aefdc5b05d7d05637f5343dab. Ziel / to: 454554bafda574c1153e6be319c30e9787ac3867.

### v0.1.1: Feldnachweise / Field evidence

DE: **Vorher:** Der ursprüngliche Paketstand enthielt noch nicht die spätere zusammengeführte Feldtestdokumentation. **Änderung:** Native Linux-/Windows-Feldnachweise für v0.1.0 und LF/CRLF/BOM-Varianten wurden gesammelt; sie sind historische Evidence für genau diesen Scope. **Nutzen:** Anwendende können Messgrenzen und Plattformnachweise nachlesen; dies beweist keinen neuen Feldtest für 0.1.2. Die Messmethodik bleibt unverändert.

<div lang="en">

EN: **Before:** The original package did not yet contain the later consolidated field-test documentation. **Change:** Native Linux/Windows field evidence for v0.1.0 and LF/CRLF/BOM variants was consolidated; it remains historical evidence for that exact scope. **Benefit:** Users can inspect measurement limits and platform evidence; this does not prove a new field test for 0.1.2. Measurement methodology is unchanged.

</div>

Quellen / sources: [docs/field-tests/v0.1.0/report.md](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/blob/5effc8a87d73b88949601951ae0933372c90ed34/docs/field-tests/v0.1.0/report.md), [docs/field-tests/v0.1.0/manifest.json](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/blob/5effc8a87d73b88949601951ae0933372c90ed34/docs/field-tests/v0.1.0/manifest.json), [docs/delivery.md](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/blob/5effc8a87d73b88949601951ae0933372c90ed34/docs/delivery.md).

### v0.1.2: Bedienbarkeit und Dokumentation / Usability and documentation

DE: **Vorher:** Archivprüfung und Paketversion mussten klar vom historischen Messformat getrennt werden. **Änderung:** Kopierbare Download-/SHA-256-Schritte erklären die externe Prüfung. Paket 0.1.2 ändert nicht automatisch das historische Formatfeld im Snapshot. **Nutzen:** Die Prüfung bleibt nachvollziehbar, ohne einen selbstreferenziellen ZIP-Hash oder gemessene KI-Produktivität zu behaupten.

<div lang="en">

EN: **Before:** Archive verification and package version needed clearer separation from the historical measurement format. **Change:** Copyable download/SHA-256 steps explain external verification. Package 0.1.2 does not automatically change the historical snapshot format field. **Benefit:** Verification is inspectable without implying a self-referential ZIP hash or measured AI productivity.

</div>

Quellen / sources: [README.md](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/blob/454554bafda574c1153e6be319c30e9787ac3867/README.md), [tests/test-release-documentation.ps1](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/blob/454554bafda574c1153e6be319c30e9787ac3867/tests/test-release-documentation.ps1).

### v0.1.2: Bedienbarkeit und Dokumentation / Usability and documentation

DE: **Vorher:** Ein kurzer README-Einstieg erklärte die vollständigen Leserpfade und Grenzen noch nicht gleichwertig. **Änderung:** Deutsch-/Englisch-Leitfäden, Command-/Vorlagen-Erklärungen und Dokumentationsregressionen wurden ergänzt. Frühere Einstiegsverbesserungen und der letzte Patch werden getrennt ausgewiesen. **Nutzen:** Lernende finden Voraussetzungen, Beispiel, Ergebnis und Fehlerhilfe. Neue Textklarheit ist keine neue Runtime-Funktion oder menschliche Abnahme.

<div lang="en">

EN: **Before:** The short README entry did not yet explain the complete reader journey and limits equally. **Change:** German/English guides, command/template explanations and documentation regressions were added. Earlier entry-point improvements and the final patch are reported separately. **Benefit:** Learners can find prerequisites, an example, its result and troubleshooting. Clearer text is not a new runtime feature or human acceptance.

</div>

Quellen / sources: [docs/user-guide.md](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/blob/454554bafda574c1153e6be319c30e9787ac3867/docs/user-guide.md), [docs/user-guide.EN.md](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/blob/454554bafda574c1153e6be319c30e9787ac3867/docs/user-guide.EN.md), [tests/test-documentation-closeout.ps1](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/blob/454554bafda574c1153e6be319c30e9787ac3867/tests/test-documentation-closeout.ps1).

### Zwischenstände / Intermediate versions

| Tag | Commit | Änderungsspanne / Change range |
|---|---|---|
| [v0.1.1](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/tree/v0.1.1) | 5effc8a87d73b88949601951ae0933372c90ed34 | [v0.1.0 → v0.1.1](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/compare/v0.1.0...v0.1.1) |
| [v0.1.2](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/tree/v0.1.2) | 454554bafda574c1153e6be319c30e9787ac3867 | [v0.1.1 → v0.1.2](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/compare/v0.1.1...v0.1.2) |

Zugehörige PRs / related PRs: [#6](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/pull/6), [#5](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/pull/5), [#4](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/pull/4), [#3](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/pull/3), [#8](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/pull/8), [#7](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/pull/7).

### Nachweise und Grenzen / Evidence and limits

Der Quellcode der verlinkten Tests belegt ihre Implementierung, nicht ihre Ausführung. Für den früheren exakten PR-Head 3b048577eee5c723f60bbd1a084415330d0fcdf0 sind 0 erfolgreiche Check-Runs erneut lesend nachgewiesen. Details und native Runner-Namen stehen im JSON-Nachweis; [damaliger PR](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/pull/8). Das ist historische Evidence, kein Pass des neuen Dokumentations-Heads.

<div lang="en">

Linked test code proves implementation, not execution. 0 successful check runs were rechecked for the earlier exact PR head; see the JSON for native runner names and links. These are historical results, not passes for the new documentation head.

</div>

Historische Feldtests behalten ihre eigene Version. Ein besserer Leserpfad ist keine neue Runtime-Funktion. Die Fachverträge, Rollen, Prioritäten und menschlichen Entscheidungsrechte bleiben in diesem Nachlauf unverändert. Keine neue Routing-Entwicklung, Modellprobe, Produkt-, Risiko-, Konformitäts-, WCAG- oder Zertifizierungsfreigabe.

<div lang="en">

Historical field tests retain their own version. A clearer reader path is not a new runtime feature. This follow-up changes no professional contracts, roles, priorities or human decision rights. It grants no new routing development, model probe, product, risk, compliance, WCAG or certification acceptance.

</div>

### Dokumentationsauswirkung / Documentation impact

UpdateRequired. Owner: Thorsten Hindermann. Quelle / source: unveränderliche Tags, echte Release-/Archivnachweise und verlinkte Testläufe / immutable tags, actual release/archive evidence and linked check runs. Leserpfad / reader path: README → DE/EN-Leitfaden → dieser Nachweis → Quellen und nächste sichere Aktion / this record → sources and next safe action. Zielgruppe / audience: alle vier IHK-IT-Berufe, alle vier Fachinformatik-Fachrichtungen und Entwickelnde ohne Spezialwissen / all four IT occupations and IT specialist disciplines, plus non-specialist developers. Deutsch zuerst, gleichwertiges Englisch, CEFR B2 und anwendbare WCAG 2.2 AA sind Prüfziele, keine automatische Abnahme / German first, equivalent English, B2 and applicable WCAG 2.2 AA are review targets, not automatic acceptance. Distribution: source-only; kein Home-Sync / no Home sync. Re-Evaluation: Tag-, Release-, Evidence-, Quellen- oder Scope-Drift / tag, release, evidence, source or scope drift.

Neue lokale Prüfungen und native CI werden im neuen PR am tatsächlichen Head nachgewiesen. Nächste Aktion: gesammelte menschliche Sichtung; vorher kein Merge. / New local checks and native CI are recorded in the new PR at its actual head. Next action: collected human review; no merge beforehand.
