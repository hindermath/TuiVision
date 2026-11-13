# v0.1.7: Stabiles Routing / Stable routing

## Veröffentlichungsabschluss / Publication closeout

| Feld / Field | Wert / Value | Bedeutung / Meaning |
|---|---|---|
| `candidate_status` | `HistoricalCandidate` | Ursprünglicher Vorbereitungsstand / original preparation state |
| `release_status` | `Published` | Reguläres Release, geprüft zum angegebenen Zeitpunkt / regular release checked as of the timestamp |

Tag: [v0.1.7](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/tree/v0.1.7). Release-Commit / release commit:
`0f47996be3b613b3ab26206b606f68e3acb44c30`. [GitHub Release](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/releases/tag/v0.1.7), veröffentlicht / published:
`2026-10-23T21:41:13.0000000+00:00`. Nachgeprüft / rechecked: `2026-10-24T10:55:20.8876630+00:00`.

[Unverändertes Tag-Archiv / unchanged tag archive](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/archive/refs/tags/v0.1.7.zip)
mit SHA-256 / with SHA-256: `0b1352fad73970c6bc12d77bc4b687dde8db36a02ee1f73ed97289bb16134b35`.
[Maschinenlesbarer Status und Quellen / machine-readable status and sources](release-0.1.7-publication.json).

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

Status: zur Sichtung vorbereitet, nicht veröffentlicht.
*Status: prepared for review, not published.*

## Änderung / Change

Aufbauend auf dem getrennt gemergten Dokumentations-PR #9 ergänzt dieser Patch
den fehlenden Schema-1.0-Vertrag: Status `fast-mechanical`, Review
`frontier-reasoning`, Standardrolle `frontier-reasoning`, `fail-closed`.
Manifestregistrierung macht den Vertrag als Vorlage auflösbar. Keine Modell-ID,
neue Auswahlheuristik oder Wiederaufnahme der verworfenen Routing-Entwicklung.

*Building on separately merged documentation PR #9, this patch adds the missing
schema-1.0 contract: status fast-mechanical, review frontier-reasoning, default
frontier-reasoning and fail-closed. Manifest registration makes it resolvable
as a template. No model IDs, new heuristics or cancelled routing development.*

Direkte Validatoren und die vier Gate-Regeln bleiben unverändert. Fachliches
Review, technische Validierung und menschliche Freigaben bleiben getrennt.
Geroutete Agentenarbeit braucht ausdrücklich konfigurierte lokale Profile;
Installation allein startet und migriert nichts.

*Direct validators and the four gate rules are unchanged. Assessment, technical
validation and human approval remain separate. Routed work needs explicitly
configured local profiles; installation starts or migrates nothing.*

## Dokumentationsauswirkung / Documentation Impact

- Entscheidung / Decision: `UpdateRequired`.
- Owner: Thorsten Hindermann.
- Kanonische Quellen / Canonical sources: Manifest, Routing-Vertrag, beide Commands.
- Leserpfad / Reader path: README → Zusammenspiel/Routing → Commands → Fehlerhilfe.
- Zielgruppe / Audience: Auszubildende aller vier IHK-IT-Berufe und aller vier
  Fachinformatik-Fachrichtungen; Entwickelnde ohne Governance-Spezialwissen.
- DE zuerst, gleichwertiges EN; CEFR B2 und anwendbare WCAG 2.2 AA als Ziel,
  keine automatische Sprach- oder Barrierefreiheitsabnahme.
- Quellen-/Hilfedokumentation mit erhaltenen Ankern; kein generierter Text.
- Distribution: Preset-Quelle und Release-Kandidat, kein Home-Runtime-Sync.
- Re-Evaluation nach Review, geändertem Head oder gesondertem Release-Auftrag.

*The README and commands explain the canonical manifest/routing contract for
IT apprentices and non-specialist developers: German first, equivalent English,
B2 and applicable WCAG 2.2 AA targets, not automated acceptance. Existing anchors
remain. Source/preset-candidate distribution only; no Home Runtime sync.
Re-evaluate after review, head changes or separate publication authority.*

## Prüfvertrag / Validation contract

`tests/test-stable-routing.ps1` prüft beide Commands, Registrierung, Rollen und
die isolierte Komposition mit Model Routing v0.1.5 (Commit
`accae165176dc948b95a22fb68650c7db62c4079`) und Autonomous Run v0.4.5
(`8026572a37f967d2603f8a942d5bfd1ccbf2dca2`). Beide Quellen sind unveränderlich
gebunden. Nur synthetische Runner-Profile; keine Modellaufrufe. Fehlende
Verträge, falsche Rollen und schwächere Überlagerungen werden geprüft.
Ein eigenes Versions-Double beantwortet nur `--version`; auch eine lokal
angemeldete Harness-CLI wird für die Fixture-Discovery nicht verwendet.
Die bestehenden Assurance- und Agentenflächen-Regressionen bleiben Pflicht.
Tatsächliche Ergebnisse und native Plattformgrenzen werden im PR dokumentiert.

*The routing test checks coverage, registration, roles and isolated composition
against the immutable stable commits above. Only synthetic profiles are used;
no model calls. An owned version-only double replaces even the native harness
version query; fixture discovery never uses a signed-in CLI. Missing contracts,
wrong roles and weaker overlaps are tested.
Existing Assurance and installed-surface regressions remain required. The PR
records actual results and native platform limits, not presumed passes.*

## Liefergrenze / Delivery boundary

Nur Commit, Push und separater PR zur Sichtung. Kein Merge, Tag, Release,
Profil-/Quellenbindungs-Rollout, Home Runtime Sync oder Community-Einreichung.
Bestehende ZIPs bleiben unverändert. Ein späteres Release braucht einen eigenen
Auftrag und die Prüfung des tatsächlichen Tag-ZIPs samt SHA-256.

*Commit, push and separate review PR only. No merge, tag, release, consumer
profile/source-lock update, Home Runtime sync or community submission. Existing
ZIPs remain unchanged. A later release requires separate authority and actual
tag-archive/SHA-256 verification.*
<!-- historical-candidate:end -->

## Verbesserungsprofil / Improvement profile

Vergleich / comparison: [v0.1.3 → v0.1.7](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/compare/0d03aa9ebe8f74a26e331815bca5609fb48d7a14...0f47996be3b613b3ab26206b606f68e3acb44c30).

Ausgang / from: 0d03aa9ebe8f74a26e331815bca5609fb48d7a14. Ziel / to: 0f47996be3b613b3ab26206b606f68e3acb44c30.

### v0.1.5: Validatorverhalten / Validator behavior

DE: **Vorher:** Aktive Dokumentmetadaten mussten von historischen oder rein erzählenden Angaben unterschieden werden. **Änderung:** Aktive Versions-/Quellenbindungen werden strenger geprüft. Unpassende aktuelle Metadaten blockieren; historische Aussagen bleiben begrenzt. **Nutzen:** Veraltete aktuelle Evidence wird nicht versehentlich als passend behandelt.

<div lang="en">

EN: **Before:** Active document metadata needed to be distinguished from historical or narrative text. **Change:** Active version/source bindings are checked more strictly. Mismatched current metadata blocks; historical statements retain their scope. **Benefit:** Stale active evidence is not accidentally accepted.

</div>

Quellen / sources: [scripts/validate-secure-development-assurance.ps1](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/55be0314488f1aeedc396f652cb247f9e5678817/scripts/validate-secure-development-assurance.ps1), [scripts/validate-secure-development-assurance.sh](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/55be0314488f1aeedc396f652cb247f9e5678817/scripts/validate-secure-development-assurance.sh), [tests/test-secure-development-assurance.ps1](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/55be0314488f1aeedc396f652cb247f9e5678817/tests/test-secure-development-assurance.ps1).

### v0.1.7: Integration

DE: **Vorher:** Die zwei Assurance-Commands hatten keinen ausdrücklich registrierten stabilen Routing-Vertrag. **Änderung:** Schema 1.0 bindet Status an fast-mechanical und Review an frontier-reasoning, mit fail-closed und isolierten Kompositionstests. Die Vorbereitung 0.1.6 in PR #9 erhielt keinen eigenen Tag; sie ist in 0.1.7 enthalten. **Nutzen:** Agentenarbeit kann den stabilen gemeinsamen Vertrag auflösen; direkte Validatoren bleiben modellfrei und erteilen keine menschliche Freigabe.

<div lang="en">

EN: **Before:** The two Assurance commands lacked an explicitly registered stable routing contract. **Change:** Schema 1.0 binds status to fast-mechanical and review to frontier-reasoning, with fail-closed behavior and isolated composition tests. The 0.1.6 preparation in PR #9 received no separate tag; it is included in 0.1.7. **Benefit:** Agent work can resolve the shared stable contract; direct validators remain model-free and grant no human approval.

</div>

Quellen / sources: [model-routing.json](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/0f47996be3b613b3ab26206b606f68e3acb44c30/model-routing.json), [preset.yml](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/0f47996be3b613b3ab26206b606f68e3acb44c30/preset.yml), [tests/test-stable-routing.ps1](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/0f47996be3b613b3ab26206b606f68e3acb44c30/tests/test-stable-routing.ps1).

### v0.1.7: Bedienbarkeit und Dokumentation / Usability and documentation

DE: **Vorher:** Ein kurzer README-Einstieg erklärte die vollständigen Leserpfade und Grenzen noch nicht gleichwertig. **Änderung:** Deutsch-/Englisch-Leitfäden, Command-/Vorlagen-Erklärungen und Dokumentationsregressionen wurden ergänzt. Frühere Einstiegsverbesserungen und der letzte Patch werden getrennt ausgewiesen. **Nutzen:** Lernende finden Voraussetzungen, Beispiel, Ergebnis und Fehlerhilfe. Neue Textklarheit ist keine neue Runtime-Funktion oder menschliche Abnahme.

<div lang="en">

EN: **Before:** The short README entry did not yet explain the complete reader journey and limits equally. **Change:** German/English guides, command/template explanations and documentation regressions were added. Earlier entry-point improvements and the final patch are reported separately. **Benefit:** Learners can find prerequisites, an example, its result and troubleshooting. Clearer text is not a new runtime feature or human acceptance.

</div>

Quellen / sources: [docs/user-guide.md](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/0f47996be3b613b3ab26206b606f68e3acb44c30/docs/user-guide.md), [docs/user-guide.EN.md](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/0f47996be3b613b3ab26206b606f68e3acb44c30/docs/user-guide.EN.md), [tests/test-documentation-closeout.ps1](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/0f47996be3b613b3ab26206b606f68e3acb44c30/tests/test-documentation-closeout.ps1).

### Zwischenstände / Intermediate versions

| Tag | Commit | Änderungsspanne / Change range |
|---|---|---|
| [v0.1.4](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/tree/v0.1.4) | 9b2a45776e6bd09a3ebecb575ad76da833c8b61d | [v0.1.3 → v0.1.4](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/compare/v0.1.3...v0.1.4) |
| [v0.1.5](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/tree/v0.1.5) | 55be0314488f1aeedc396f652cb247f9e5678817 | [v0.1.4 → v0.1.5](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/compare/v0.1.4...v0.1.5) |
| [v0.1.7](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/tree/v0.1.7) | 0f47996be3b613b3ab26206b606f68e3acb44c30 | [v0.1.5 → v0.1.7](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/compare/v0.1.5...v0.1.7) |

Zugehörige PRs / related PRs: [#6](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/pull/6), [#5](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/pull/5), [#7](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/pull/7), [#11](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/pull/11), [#10](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/pull/10), [#9](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/pull/9).

### Nachweise und Grenzen / Evidence and limits

Der Quellcode der verlinkten Tests belegt ihre Implementierung, nicht ihre Ausführung. Für den früheren exakten PR-Head 85f589a607490f6238ba4bfecaac6b47ce2e02e4 sind 0 erfolgreiche Check-Runs erneut lesend nachgewiesen. Details und native Runner-Namen stehen im JSON-Nachweis; [damaliger PR](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/pull/11). Das ist historische Evidence, kein Pass des neuen Dokumentations-Heads.

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
