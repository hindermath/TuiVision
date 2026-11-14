# v0.2.10: Dokumentation / Documentation

## Veröffentlichungsabschluss / Publication closeout

| Feld / Field | Wert / Value | Bedeutung / Meaning |
|---|---|---|
| `candidate_status` | `HistoricalCandidate` | Ursprünglicher Vorbereitungsstand / original preparation state |
| `release_status` | `Published` | Reguläres Release, geprüft zum angegebenen Zeitpunkt / regular release checked as of the timestamp |

Tag: [v0.2.10](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/tree/v0.2.10). Release-Commit / release commit:
`ccb6c223c51ba685f650f2ce5238299cb73b1f31`. [GitHub Release](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/releases/tag/v0.2.10), veröffentlicht / published:
`2026-10-23T21:29:31.0000000+00:00`. Nachgeprüft / rechecked: `2026-10-24T10:55:43.7164120+00:00`.

[Unverändertes Tag-Archiv / unchanged tag archive](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/archive/refs/tags/v0.2.10.zip)
mit SHA-256 / with SHA-256: `0da5d751a89b9ae68a75c2f590b449599d98d4a0ed7fe4c2bcd935e90a197678`.
[Maschinenlesbarer Status und Quellen / machine-readable status and sources](release-0.2.10-publication.json).

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

Quellkandidat zur gesammelten Sichtung, nicht durch diesen Text veröffentlicht.
*Source candidate for collected review; this text does not publish a release.*

[Dokumentationsumfang und Prüfgrenzen / Scope and test limits](documentation-closeout.md)

Bestehende Tags und historische Nachweise bleiben unverändert. Technische und fachliche Freigaben sind getrennt.
*Existing tags and historical evidence stay unchanged. Technical and human acceptance are separate.*
<!-- historical-candidate:end -->

## Verbesserungsprofil / Improvement profile

Vergleich / comparison: [v0.2.6 → v0.2.10](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/compare/8aa137fcc95faf4e145ee408a1ee3790cac1873a...ccb6c223c51ba685f650f2ce5238299cb73b1f31).

Ausgang / from: 8aa137fcc95faf4e145ee408a1ee3790cac1873a. Ziel / to: ccb6c223c51ba685f650f2ce5238299cb73b1f31.

### v0.2.7: Validatorverhalten / Validator behavior

DE: **Vorher:** Idle, laufende Serien ohne weiteren Kandidaten und verschachtelte Git-Repositories mussten einheitlich behandelt werden. **Änderung:** Der gemeinsame Validator und positive/negative Fixtures stimmen diese Zustände ab. Mehrdeutigkeit, ungültige Pfade, Hashes und Zustände bleiben blockiert. **Nutzen:** Zusammen installierte Intake-Presets bewerten denselben Projektzustand konsistent; es startet kein nachgelagerter Lauf.

<div lang="en">

EN: **Before:** Idle, running series with no additional candidate and nested Git repositories needed consistent handling. **Change:** The shared validator and positive/negative fixtures align these states. Ambiguity and invalid paths, hashes or states remain blocked. **Benefit:** Combined Intake presets assess the same project state consistently; they start no downstream run.

</div>

Quellen / sources: [scripts/validate-intake-governance-config.py](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/74f6b343b79c93f6fcbfe9db1c111e9eb3aeeee8/scripts/validate-intake-governance-config.py), [tests/test-intake-governance-config.ps1](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/74f6b343b79c93f6fcbfe9db1c111e9eb3aeeee8/tests/test-intake-governance-config.ps1).

### v0.2.10: Kompatibilität / Compatibility

DE: **Vorher:** Paket-, Generator- und Schema-Versionen durften nicht verwechselt werden. **Änderung:** Die ausgelieferte Receipt-Vorlage nennt Generator 0.2.10. Frühere Releases 0.2.7 bis 0.2.9 banden ihre eigenen Generatorstände; das ist keine neue Schema-Version. **Nutzen:** Die Herkunft neuer Receipts bleibt überprüfbar, ohne historische Receipts umzuschreiben.

<div lang="en">

EN: **Before:** Package, generator and schema versions must not be confused. **Change:** The shipped receipt template names generator 0.2.10. Earlier releases 0.2.7–0.2.9 bound their own generator versions; this introduces no new schema. **Benefit:** New receipt provenance is verifiable without rewriting historical receipts.

</div>

Quellen / sources: [templates/intake-series-receipt-template.json](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/ccb6c223c51ba685f650f2ce5238299cb73b1f31/templates/intake-series-receipt-template.json), [tests/test-template-integrity.ps1](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/ccb6c223c51ba685f650f2ce5238299cb73b1f31/tests/test-template-integrity.ps1).

### v0.2.10: Bedienbarkeit und Dokumentation / Usability and documentation

DE: **Vorher:** Ein kurzer README-Einstieg erklärte die vollständigen Leserpfade und Grenzen noch nicht gleichwertig. **Änderung:** Deutsch-/Englisch-Leitfäden, Command-/Vorlagen-Erklärungen und Dokumentationsregressionen wurden ergänzt. Frühere Einstiegsverbesserungen und der letzte Patch werden getrennt ausgewiesen. **Nutzen:** Lernende finden Voraussetzungen, Beispiel, Ergebnis und Fehlerhilfe. Neue Textklarheit ist keine neue Runtime-Funktion oder menschliche Abnahme.

<div lang="en">

EN: **Before:** The short README entry did not yet explain the complete reader journey and limits equally. **Change:** German/English guides, command/template explanations and documentation regressions were added. Earlier entry-point improvements and the final patch are reported separately. **Benefit:** Learners can find prerequisites, an example, its result and troubleshooting. Clearer text is not a new runtime feature or human acceptance.

</div>

Quellen / sources: [docs/user-guide.md](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/ccb6c223c51ba685f650f2ce5238299cb73b1f31/docs/user-guide.md), [docs/user-guide.EN.md](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/ccb6c223c51ba685f650f2ce5238299cb73b1f31/docs/user-guide.EN.md), [tests/test-documentation-closeout.ps1](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/ccb6c223c51ba685f650f2ce5238299cb73b1f31/tests/test-documentation-closeout.ps1).

### Zwischenstände / Intermediate versions

| Tag | Commit | Änderungsspanne / Change range |
|---|---|---|
| [v0.2.7](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/tree/v0.2.7) | 74f6b343b79c93f6fcbfe9db1c111e9eb3aeeee8 | [v0.2.6 → v0.2.7](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/compare/v0.2.6...v0.2.7) |
| [v0.2.8](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/tree/v0.2.8) | be69290486175fc8256338200b342bf0e33cbb09 | [v0.2.7 → v0.2.8](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/compare/v0.2.7...v0.2.8) |
| [v0.2.9](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/tree/v0.2.9) | 0165082b3146ca2bd6834303129e802e83daa78d | [v0.2.8 → v0.2.9](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/compare/v0.2.8...v0.2.9) |
| [v0.2.10](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/tree/v0.2.10) | ccb6c223c51ba685f650f2ce5238299cb73b1f31 | [v0.2.9 → v0.2.10](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/compare/v0.2.9...v0.2.10) |

Zugehörige PRs / related PRs: [#11](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/pull/11), [#10](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/pull/10), [#12](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/pull/12), [#14](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/pull/14), [#13](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/pull/13), [#15](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/pull/15).

### Nachweise und Grenzen / Evidence and limits

Der Quellcode der verlinkten Tests belegt ihre Implementierung, nicht ihre Ausführung. Für den früheren exakten PR-Head 026f9e7908ff64db9aa18d50c3a5de487f70bcc3 sind 0 erfolgreiche Check-Runs erneut lesend nachgewiesen. Details und native Runner-Namen stehen im JSON-Nachweis; [damaliger PR](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/pull/15). Das ist historische Evidence, kein Pass des neuen Dokumentations-Heads.

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
