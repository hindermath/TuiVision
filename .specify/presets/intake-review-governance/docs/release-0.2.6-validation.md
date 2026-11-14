# v0.2.6: Dokumentation / Documentation

## Veröffentlichungsabschluss / Publication closeout

| Feld / Field | Wert / Value | Bedeutung / Meaning |
|---|---|---|
| `candidate_status` | `HistoricalCandidate` | Ursprünglicher Vorbereitungsstand / original preparation state |
| `release_status` | `Published` | Reguläres Release, geprüft zum angegebenen Zeitpunkt / regular release checked as of the timestamp |

Tag: [v0.2.6](https://github.com/hindermath/spec-kit-preset-intake-review-governance/tree/v0.2.6). Release-Commit / release commit:
`4838ab546d4f6f6c892152469cf336297e303384`. [GitHub Release](https://github.com/hindermath/spec-kit-preset-intake-review-governance/releases/tag/v0.2.6), veröffentlicht / published:
`2026-10-23T21:29:32.0000000+00:00`. Nachgeprüft / rechecked: `2026-10-24T10:55:41.5308760+00:00`.

[Unverändertes Tag-Archiv / unchanged tag archive](https://github.com/hindermath/spec-kit-preset-intake-review-governance/archive/refs/tags/v0.2.6.zip)
mit SHA-256 / with SHA-256: `fa02a2fa1ed44b2041ecd209f0ad1f96ec32b40bf5f3a867e58fd7012e3aa90b`.
[Maschinenlesbarer Status und Quellen / machine-readable status and sources](release-0.2.6-publication.json).

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

Vergleich / comparison: [v0.2.3 → v0.2.6](https://github.com/hindermath/spec-kit-preset-intake-review-governance/compare/91462b4f47e64b9585c0f7d440a80449d7eb64c0...4838ab546d4f6f6c892152469cf336297e303384).

Ausgang / from: 91462b4f47e64b9585c0f7d440a80449d7eb64c0. Ziel / to: 4838ab546d4f6f6c892152469cf336297e303384.

### v0.2.4: Validatorverhalten / Validator behavior

DE: **Vorher:** Idle, laufende Serien ohne weiteren Kandidaten und verschachtelte Git-Repositories mussten einheitlich behandelt werden. **Änderung:** Der gemeinsame Validator und positive/negative Fixtures stimmen diese Zustände ab. Mehrdeutigkeit, ungültige Pfade, Hashes und Zustände bleiben blockiert. **Nutzen:** Zusammen installierte Intake-Presets bewerten denselben Projektzustand konsistent; es startet kein nachgelagerter Lauf.

<div lang="en">

EN: **Before:** Idle, running series with no additional candidate and nested Git repositories needed consistent handling. **Change:** The shared validator and positive/negative fixtures align these states. Ambiguity and invalid paths, hashes or states remain blocked. **Benefit:** Combined Intake presets assess the same project state consistently; they start no downstream run.

</div>

Quellen / sources: [scripts/validate-intake-governance-config.py](https://github.com/hindermath/spec-kit-preset-intake-review-governance/blob/f0f9b3536a58f8c90a79119565af8190809a4bdb/scripts/validate-intake-governance-config.py), [tests/test-intake-governance-config.ps1](https://github.com/hindermath/spec-kit-preset-intake-review-governance/blob/f0f9b3536a58f8c90a79119565af8190809a4bdb/tests/test-intake-governance-config.ps1).

### v0.2.6: Bedienbarkeit und Dokumentation / Usability and documentation

DE: **Vorher:** Ein kurzer README-Einstieg erklärte die vollständigen Leserpfade und Grenzen noch nicht gleichwertig. **Änderung:** Deutsch-/Englisch-Leitfäden, Command-/Vorlagen-Erklärungen und Dokumentationsregressionen wurden ergänzt. Frühere Einstiegsverbesserungen und der letzte Patch werden getrennt ausgewiesen. **Nutzen:** Lernende finden Voraussetzungen, Beispiel, Ergebnis und Fehlerhilfe. Neue Textklarheit ist keine neue Runtime-Funktion oder menschliche Abnahme.

<div lang="en">

EN: **Before:** The short README entry did not yet explain the complete reader journey and limits equally. **Change:** German/English guides, command/template explanations and documentation regressions were added. Earlier entry-point improvements and the final patch are reported separately. **Benefit:** Learners can find prerequisites, an example, its result and troubleshooting. Clearer text is not a new runtime feature or human acceptance.

</div>

Quellen / sources: [docs/user-guide.md](https://github.com/hindermath/spec-kit-preset-intake-review-governance/blob/4838ab546d4f6f6c892152469cf336297e303384/docs/user-guide.md), [docs/user-guide.EN.md](https://github.com/hindermath/spec-kit-preset-intake-review-governance/blob/4838ab546d4f6f6c892152469cf336297e303384/docs/user-guide.EN.md), [tests/test-documentation-closeout.ps1](https://github.com/hindermath/spec-kit-preset-intake-review-governance/blob/4838ab546d4f6f6c892152469cf336297e303384/tests/test-documentation-closeout.ps1).

### Zwischenstände / Intermediate versions

| Tag | Commit | Änderungsspanne / Change range |
|---|---|---|
| [v0.2.4](https://github.com/hindermath/spec-kit-preset-intake-review-governance/tree/v0.2.4) | f0f9b3536a58f8c90a79119565af8190809a4bdb | [v0.2.3 → v0.2.4](https://github.com/hindermath/spec-kit-preset-intake-review-governance/compare/v0.2.3...v0.2.4) |
| [v0.2.5](https://github.com/hindermath/spec-kit-preset-intake-review-governance/tree/v0.2.5) | bbf63e79becf11bfd6002352b493c12700d970ec | [v0.2.4 → v0.2.5](https://github.com/hindermath/spec-kit-preset-intake-review-governance/compare/v0.2.4...v0.2.5) |
| [v0.2.6](https://github.com/hindermath/spec-kit-preset-intake-review-governance/tree/v0.2.6) | 4838ab546d4f6f6c892152469cf336297e303384 | [v0.2.5 → v0.2.6](https://github.com/hindermath/spec-kit-preset-intake-review-governance/compare/v0.2.5...v0.2.6) |

Zugehörige PRs / related PRs: [#10](https://github.com/hindermath/spec-kit-preset-intake-review-governance/pull/10), [#9](https://github.com/hindermath/spec-kit-preset-intake-review-governance/pull/9), [#8](https://github.com/hindermath/spec-kit-preset-intake-review-governance/pull/8), [#12](https://github.com/hindermath/spec-kit-preset-intake-review-governance/pull/12), [#11](https://github.com/hindermath/spec-kit-preset-intake-review-governance/pull/11), [#13](https://github.com/hindermath/spec-kit-preset-intake-review-governance/pull/13).

### Nachweise und Grenzen / Evidence and limits

Der Quellcode der verlinkten Tests belegt ihre Implementierung, nicht ihre Ausführung. Für den früheren exakten PR-Head 77e21a0131d131f69b2741e5f2fca7715b3bcbaf sind 0 erfolgreiche Check-Runs erneut lesend nachgewiesen. Details und native Runner-Namen stehen im JSON-Nachweis; [damaliger PR](https://github.com/hindermath/spec-kit-preset-intake-review-governance/pull/13). Das ist historische Evidence, kein Pass des neuen Dokumentations-Heads.

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
