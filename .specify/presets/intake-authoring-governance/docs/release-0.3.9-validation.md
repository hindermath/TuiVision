# Patch 0.3.9: Dokumentationskorrektur / Documentation correction

## Veröffentlichungsabschluss / Publication closeout

| Feld / Field | Wert / Value | Bedeutung / Meaning |
|---|---|---|
| `candidate_status` | `HistoricalCandidate` | Ursprünglicher Vorbereitungsstand / original preparation state |
| `release_status` | `Published` | Reguläres Release, geprüft zum angegebenen Zeitpunkt / regular release checked as of the timestamp |

Tag: [v0.3.9](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/tree/v0.3.9). Release-Commit / release commit:
`33845858eff1cc6aa766012650491d53e1a88447`. [GitHub Release](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/releases/tag/v0.3.9), veröffentlicht / published:
`2026-10-23T21:29:33.0000000+00:00`. Nachgeprüft / rechecked: `2026-10-24T10:55:39.4679740+00:00`.

[Unverändertes Tag-Archiv / unchanged tag archive](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/archive/refs/tags/v0.3.9.zip)
mit SHA-256 / with SHA-256: `5fdec1175224a30f4c2e10f549351fe71bfd06722f4390596625d45ed4733ffb`.
[Maschinenlesbarer Status und Quellen / machine-readable status and sources](release-0.3.9-publication.json).

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

DE und EN nennen dieselbe Generatorversion 0.3.9. Die ausgelieferte Vorlage bindet diese Version; beide Validatoren behalten den bekannten Vorgänger 0.3.8 ausdrücklich bei. Der Lifecycle-Test prüft dessen Akzeptanz zusätzlich, während unbekannte Versionen und alte Schemakombinationen gesperrt bleiben.

*German and English agree on generator 0.3.9. The shipped template binds that version; both validators explicitly retain known 0.3.8 receipts. Lifecycle regression adds that compatibility case while unknown versions and invalid legacy-schema combinations still block.*

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

Vergleich / comparison: [v0.3.5 → v0.3.9](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/compare/f90e707444237d768623f457f85d15a707c3476e...33845858eff1cc6aa766012650491d53e1a88447).

Ausgang / from: f90e707444237d768623f457f85d15a707c3476e. Ziel / to: 33845858eff1cc6aa766012650491d53e1a88447.

### v0.3.6: Validatorverhalten / Validator behavior

DE: **Vorher:** Idle, laufende Serien ohne weiteren Kandidaten und verschachtelte Git-Repositories mussten einheitlich behandelt werden. **Änderung:** Der gemeinsame Validator und positive/negative Fixtures stimmen diese Zustände ab. Mehrdeutigkeit, ungültige Pfade, Hashes und Zustände bleiben blockiert. **Nutzen:** Zusammen installierte Intake-Presets bewerten denselben Projektzustand konsistent; es startet kein nachgelagerter Lauf.

<div lang="en">

EN: **Before:** Idle, running series with no additional candidate and nested Git repositories needed consistent handling. **Change:** The shared validator and positive/negative fixtures align these states. Ambiguity and invalid paths, hashes or states remain blocked. **Benefit:** Combined Intake presets assess the same project state consistently; they start no downstream run.

</div>

Quellen / sources: [scripts/validate-intake-governance-config.py](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/blob/5530868f0de63b8fae19781f69086befefe4f965/scripts/validate-intake-governance-config.py), [tests/test-intake-governance-config.ps1](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/blob/5530868f0de63b8fae19781f69086befefe4f965/tests/test-intake-governance-config.ps1).

### v0.3.9: Kompatibilität / Compatibility

DE: **Vorher:** Die bekannten 0.3.8-Receipts fehlten in der ausdrücklichen Vorgängerfreigabe. **Änderung:** Beide Validatoren akzeptieren bekannte Schema-2-Receipts von 0.3.8 zusätzlich. Die neue Vorlage bindet 0.3.9; unbekannte Versionen bleiben blockiert. **Nutzen:** Bestehende gültige Receipts bleiben nutzbar. Dies ist eine echte Kompatibilitätskorrektur in 0.3.9, nicht nur ein Textupdate.

<div lang="en">

EN: **Before:** Known 0.3.8 receipts were missing from the explicit predecessor allowlist. **Change:** Both validators explicitly retain known schema-2 receipts from 0.3.8. The new template binds 0.3.9; unknown versions still block. **Benefit:** Existing valid receipts stay usable. This is an actual 0.3.9 compatibility correction, not merely a text update.

</div>

Quellen / sources: [scripts/validate-intake-authoring-receipt.ps1](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/blob/33845858eff1cc6aa766012650491d53e1a88447/scripts/validate-intake-authoring-receipt.ps1), [scripts/validate-intake-authoring-receipt.sh](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/blob/33845858eff1cc6aa766012650491d53e1a88447/scripts/validate-intake-authoring-receipt.sh), [tests/test-intake-authoring-lifecycle.ps1](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/blob/33845858eff1cc6aa766012650491d53e1a88447/tests/test-intake-authoring-lifecycle.ps1).

### v0.3.9: Bedienbarkeit und Dokumentation / Usability and documentation

DE: **Vorher:** Ein kurzer README-Einstieg erklärte die vollständigen Leserpfade und Grenzen noch nicht gleichwertig. **Änderung:** Deutsch-/Englisch-Leitfäden, Command-/Vorlagen-Erklärungen und Dokumentationsregressionen wurden ergänzt. Frühere Einstiegsverbesserungen und der letzte Patch werden getrennt ausgewiesen. **Nutzen:** Lernende finden Voraussetzungen, Beispiel, Ergebnis und Fehlerhilfe. Neue Textklarheit ist keine neue Runtime-Funktion oder menschliche Abnahme.

<div lang="en">

EN: **Before:** The short README entry did not yet explain the complete reader journey and limits equally. **Change:** German/English guides, command/template explanations and documentation regressions were added. Earlier entry-point improvements and the final patch are reported separately. **Benefit:** Learners can find prerequisites, an example, its result and troubleshooting. Clearer text is not a new runtime feature or human acceptance.

</div>

Quellen / sources: [docs/user-guide.md](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/blob/33845858eff1cc6aa766012650491d53e1a88447/docs/user-guide.md), [docs/user-guide.EN.md](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/blob/33845858eff1cc6aa766012650491d53e1a88447/docs/user-guide.EN.md), [tests/test-documentation-closeout.ps1](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/blob/33845858eff1cc6aa766012650491d53e1a88447/tests/test-documentation-closeout.ps1).

### Zwischenstände / Intermediate versions

| Tag | Commit | Änderungsspanne / Change range |
|---|---|---|
| [v0.3.6](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/tree/v0.3.6) | 5530868f0de63b8fae19781f69086befefe4f965 | [v0.3.5 → v0.3.6](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/compare/v0.3.5...v0.3.6) |
| [v0.3.7](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/tree/v0.3.7) | dbf135e89ba583fb27294f84e487cf1a5826604f | [v0.3.6 → v0.3.7](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/compare/v0.3.6...v0.3.7) |
| [v0.3.8](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/tree/v0.3.8) | 40e609e359ef0b4337b3f0e1781708dbeedc6eb9 | [v0.3.7 → v0.3.8](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/compare/v0.3.7...v0.3.8) |
| [v0.3.9](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/tree/v0.3.9) | 33845858eff1cc6aa766012650491d53e1a88447 | [v0.3.8 → v0.3.9](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/compare/v0.3.8...v0.3.9) |

Zugehörige PRs / related PRs: [#13](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/pull/13), [#12](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/pull/12), [#11](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/pull/11), [#14](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/pull/14), [#16](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/pull/16), [#15](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/pull/15), [#18](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/pull/18), [#17](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/pull/17).

### Nachweise und Grenzen / Evidence and limits

Der Quellcode der verlinkten Tests belegt ihre Implementierung, nicht ihre Ausführung. Für den früheren exakten PR-Head b1a113fd8c50909719f78683d34672158692a5d6 sind 0 erfolgreiche Check-Runs erneut lesend nachgewiesen. Details und native Runner-Namen stehen im JSON-Nachweis; [damaliger PR](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/pull/18). Das ist historische Evidence, kein Pass des neuen Dokumentations-Heads.

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
