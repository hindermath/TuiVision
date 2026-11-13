# Lieferung und Feldtest / Delivery and field test

> Historischer Liefernachweis für v0.1.0, kein aktueller Installations- oder
> Freigabeauftrag für spätere Pakete. Version, Hash, Assets und damalige
> Autorität unten gelten ausschließlich für diesen Stand. Für aktuelle Nutzung
> [Deutsch](user-guide.md), [English](user-guide.EN.md) und die README lesen.
>
> Historical v0.1.0 delivery evidence, not current installation instructions or
> release authority for later packages. Versions, hashes, assets and authority
> below belong only to that state. Use the current paired guides and README.

Diese Anleitung erklaert, was bei einer Lieferung entschieden, geprueft und
unveraendert bewahrt wird. Sie richtet sich an Lernende, Entwickler und
Maintainer. Fuer Installation und taegliche Nutzung zuerst die
[README](../README.md), fuer Zaehllogik die [Methodik](methodology.md) lesen.

This guide explains delivery decisions, checks and preserved evidence for
learners, developers and maintainers. Start with the README for installation
and daily use, and the methodology for counting rules.

## Inhalt / Contents

- [Entscheidungen und Autoritaet / Decisions and authority](#authority)
- [Version und Lieferkette / Version and supply chain](#binding)
- [Abgeschlossener Feldtest / Completed field test](#field)
- [Regulaere Veroeffentlichung / Regular publication](#publication)
- [Projektinstallation und spaeterer Rollout / Project installation and rollout](#rollout)
- [Community-Einreichung / Community submission](#community)
- [Historie und Dokumentationsstand / History and documentation](#history)
- [Dokumentationsauswirkung / Documentation impact](#impact)

<a id="authority"></a>
## Entscheidungen und Autoritaet / Decisions and authority

Owner und fachlicher Reviewer ist Thorsten Hindermann (@hindermath).
Erfolgreiche Automatisierung ist technische Evidence, keine automatische
menschliche Entscheidung. Die folgenden Schritte bleiben getrennt:

Owner and human reviewer: Thorsten Hindermann (@hindermath). Successful
automation supplies technical evidence, not automatic human approval.
Keep these decisions separate:

| Schritt / stage | Inhalt / content | Was daraus nicht folgt / what it does not grant |
| --- | --- | --- |
| Technische Pruefung / technical checks | Deterministische Tests, Plattform- und Hashnachweise / fixtures, platforms and hashes | Keine menschliche Abnahme / no human acceptance |
| Feldtest-Abnahme / field acceptance | Fachliche Sichtung der realen Piloten und Grenzen / human review of real pilots and limits | Keine automatische Release-Promotion / no automatic release promotion |
| Regulaere Veroeffentlichung / regular publication | Geprueftes Paket fuer regulaere Nutzung freigeben / approve the tested package for regular use | Kein Katalogeintrag und kein Rollout / no catalog entry or rollout |
| Community-Aufnahme / catalog acceptance | Upstream-Issue, automatisierter PR und Maintainer-Merge / upstream issue, generated PR and maintainer merge | Keine Installation in Zielprojekten / no target installation |
| Projekt-Rollout / project rollout | Benannte Projekte mit eigener Policy und Lieferautoritaet / named projects with their own delivery authority | Keine automatische Legacy-Migration / no automatic legacy replacement |

Der urspruengliche Auftrag umfasste das MIT-Paket, Pre-Release und drei
Pilotlieferungen. Am 2026-09-18 wurden der zentrale Feldtest fachlich abgenommen
und PR #3 geliefert. Danach genehmigte der Owner separat den Plan zur regulaeren
Veroeffentlichung des unveraenderten v0.1.0 samt ausfuehrlicher Dokumentation.
Diese neue Autoritaet ersetzt keine Historie und erweitert nicht den Rollout-Scope.

The original scope covered the MIT package, pre-release and three pilots.
On 2026-09-18, the central field report was accepted and PR #3 delivered.
The owner then separately approved regular publication of unchanged v0.1.0
and expanded documentation. This later authority preserves historical decisions
and does not expand rollout scope.

Dokumentationslieferungen verwenden MergeAndSync: pruefen, committen, pushen,
PR pruefen, gruene Checks am exakten Head bestaetigen, diesen Head mergen und
`main` lokal/remote synchronisieren. Ein genehmigter administrativer
Review-Bypass darf nur eine formale Review-Sperre umgehen, niemals fehlende,
laufende oder fehlgeschlagene technische Checks.

Documentation uses MergeAndSync: validate, commit, push, review the PR, verify
green checks on the exact head, merge that head and sync main. Authorized admin
review bypass may address a formal review restriction only; never bypass
missing, running or failed technical checks.

<a id="binding"></a>
## Version und Lieferkette / Version and supply chain

| Bindung / binding | Wert / value |
| --- | --- |
| Preset ID | `project-statistics-governance` |
| Version / tag | `v0.1.0` |
| Release ID | `388698117` |
| Paket-Quellcommit / package source | `7e824ca8de11212aefdc5b05d7d05637f5343dab` |
| Annotiertes Tag-Objekt / annotated tag | `fe5243020993787065c469b263f7198f9c345a23` |
| ZIP SHA-256 | `d8ad7d5eef920f50b629121b64ba8123c22ec4f6dadd14da5cd826d1c50f420a` |

Die regulaere Freigabe verwendet dasselbe Paket wie der Feldtest, nicht einen
neuen Export von `main`. Die spaeteren Dokumentationscommits sind kein Grund,
den Tag auf einen neueren Commit zu verschieben. Auch keine vermeintlich
identischen Assets neu hochladen: Namen, IDs, Bytezahlen und Hashes erhalten.

Regular approval uses the field-tested package, not a fresh export of main.
Later documentation commits do not justify moving the tag. Do not re-upload
even supposedly identical assets; preserve names, IDs, lengths and hashes.

Die fuenf bestehenden Assets haben unterschiedliche Aufgaben:
The five existing assets serve different purposes:

| Asset | Zweck / purpose |
| --- | --- |
| `project-statistics-governance-v0.1.0.zip` | Installierbares, tag-identisches Paket / installable, tag-identical package |
| `SHA256SUMS` | Pruefsummen der urspruenglichen Nachweisdateien / checksums for original release files |
| `release-evidence.json` | Historische Quellen-/Lieferbindung der Erstveroeffentlichung / original release provenance |
| `native-ci-evidence-v0.1.0.tar.gz` | Urspruengliche native Paket-CI / original native package CI |
| `project-statistics-governance-v0.1.0.cdx.json` | Quellpaket-SBOM, nicht Inventar aller Host-Tools / source package SBOM, not all host tools |

Release-Metadaten sind aenderbar; bei Erstellung war GitHubs
Immutable-Release-Schutz nicht aktiv. Dieser Vorgang schaltet ihn nicht um.
Integritaet wird hier durch festgehaltene Quellen und Hashes geprueft, nicht
durch eine unzutreffende Unveraenderlichkeitsbehauptung. Ein reguläres Release
ist keine neue Major-Version und keine pauschale Zusage unveraenderlicher APIs.

Release metadata can change; immutable-release protection was not enabled.
This operation does not change that setting. Integrity relies on verified
source/hash bindings, not an unsupported immutability claim. Regular release
status does not create a new major version or promise unchanging APIs.

<a id="field"></a>
## Abgeschlossener Feldtest / Completed field test

Der [kanonische Bericht](field-tests/v0.1.0/report.md) und sein
[Manifest](field-tests/v0.1.0/manifest.json) belegen die drei Piloten:

The canonical report and manifest document these three pilots:

| Pilot | Umgebung / environment | Gelieferter PR / delivered PR |
| --- | --- | --- |
| Home Baseline | Skript-Infrastruktur / script infrastructure | [#301](https://github.com/hindermath/home-baseline/pull/301) |
| TinyCalc | .NET-Beispielanwendung / .NET example | [#86](https://github.com/hindermath/TinyCalc/pull/86) |
| ABS-DD Sandbox | Container-Entwicklungsumgebung / container development | [#73](https://github.com/hindermath/absdd-image-sandbox/pull/73) |

Technische Empfehlung: `ReleaseAccepted`. Getrennte fachliche Abnahme:
`Accepted`, 2026-09-18. [Entscheidungsnachweis](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/pull/3#issuecomment-5733063715)
und [gemergter zentraler PR #3](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/pull/3).
Die 66 archivierten Ergebnisdateien und deren Originalbytes bleiben erhalten.
Die lokalen Pilottexte sind historische Belege und keine automatische Anzeige
des heutigen Release-Status.

Technical recommendation: `ReleaseAccepted`; separate human acceptance:
`Accepted` on 2026-09-18, recorded in the linked decision and merged report PR.
The 66 archived result files retain their original bytes. Local pilot texts are
historical evidence, not a live release-status display.

Insbesondere bleiben Homes historische Encoding-Messung und spaetere
Liefermessung unterscheidbar. TinyCalcs Legacy-Ausschluesse und die Unterschiede
zwischen UTC und Europe/Berlin sind keine vergleichbaren Gleichheitsgarantien.
Windows-Nachweise ersetzen keine nicht ausgefuehrten Unix-Shell-Faelle.
Read-only ist ueber die geprueften Dateien und Git-Zustaende belegt, nicht
als systemweiter I/O-Trace. Vollstaendige Details stehen im Feldbericht.

Keep Home's historical encoding test separate from its later delivery check.
TinyCalc's legacy exclusions and UTC/local-day boundaries are not identical
measurement contracts. Windows proof does not cover unexecuted Unix shell cases.
Read-only proof covers checked files and Git state, not all system I/O. The
field report preserves the complete details.

Das Tracking ist abgeschlossen:
[Preset #1](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/issues/1),
[Home #298](https://github.com/hindermath/home-baseline/issues/298),
[TinyCalc #84](https://github.com/hindermath/TinyCalc/issues/84),
[Sandbox #70](https://github.com/hindermath/absdd-image-sandbox/issues/70).
Dies ist keine Produkt-, C5-, Konformitaets- oder Zertifizierungsabnahme.

The linked tracking issues are complete. This does not accept a product or
grant C5, conformity or certification decisions.

<a id="publication"></a>
## Regulaere Veroeffentlichung / Regular publication

Die Owner-Freigabe gilt fuer ein regulaeres v0.1.0. Die
[Release-Seite](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/releases/tag/v0.1.0)
ist die Quelle fuer den tatsaechlich umgesetzten Metadatenstatus. Der Lieferablauf:

The owner approved regular v0.1.0 publication. The release page is authoritative
for actual metadata status. Delivery procedure:

1. Release-Metadaten vorab sichern; alle fuenf Assets und Tag-ZIP herunterladen.
   Tag-Objekt, Zielcommit, Dateigroessen und SHA-256 pruefen.
2. README/Lieferdokumentation und zweisprachige Release-Notes aktualisieren.
   Die Feldtest-Abnahme ist abgeschlossen, nicht mehr ausstehend. Historische
   Paket-Nachweise werden nicht umgeschrieben.
3. Dokumentationsbeispiele isoliert testen; bestehenden Fixture-/Lifecycle-
   Tests und nativer CI folgen. Dokumentations-PR am gruenen exakten Head liefern.
4. Bestehendes Release aktualisieren: Titel `Project Statistics Governance v0.1.0`,
   `prerelease=false`, `draft=false`, `Latest`. Tag und Assets nicht veraendern.
5. Metadaten und alle erneut heruntergeladenen Dateien gegen den gesicherten
   Vorher-Stand pruefen. Main-CI und sauberes lokal/remote `0/0` bestaetigen.

1. Save original release metadata; download all five assets and the tag ZIP.
   Check tag object, target commit, file lengths and SHA-256 values.
2. Update current docs and bilingual release notes to completed field acceptance;
   preserve historical package evidence.
3. Test examples in isolation and run fixtures, lifecycle and native CI.
   Deliver the documentation PR at its green exact head.
4. Update the existing release title/status as listed above, preserving tag/assets.
5. Redownload and compare files and metadata; confirm main CI and clean local/remote sync.

Bei Hashabweichungen, geaendertem Tag oder fehlender Autoritaet stoppen.
Keine Ersatzpakete erzeugen und nicht die erwarteten Hashes an ein unbekanntes
Paket anpassen. Bei einem API-Fehler zuerst den Live-Status lesen, bevor erneut
geschrieben wird. Ein gelieferter Doku-PR bei gescheiterter Metadatenumstellung
ist ein Teilabschluss und wird als solcher berichtet, nicht als fertiges Release.

Stop on hash/tag drift or missing authority. Never rebuild replacement assets
or adjust expected hashes to an unknown package. After API errors, inspect live
state before retrying writes. Delivered docs with a failed metadata promotion
are partial completion, not a completed regular release.

<a id="rollout"></a>
## Projektinstallation und spaeterer Rollout / Project installation and rollout

Verfuegbarkeit ist keine Installationsfreigabe. Das optionale zentrale
14-Preset-Profil wurde in drei Piloten erprobt; daraus folgt keine pauschale
Aenderung aller Projekte mit anderen Presets. Ein spaeterer eigener Rollout-Plan
muss Zielrepos, Ist-Versionen, Profil, Ausnahmen, Pruefungen und Lieferautoritaet
benennen. Bei identischer Installation reicht eine Verifikation statt Neuinstallation.

Availability grants no installation authority. The optional central fourteen-
preset profile was tested in three pilots, not authorized for every repository
with other presets. A separate rollout plan must identify targets, installed
versions, profiles, exceptions, checks and delivery authority. An identical
installation needs verification rather than forced reinstallation.

Installation, Initialisierung eines Statistik-Kontexts und Ersatz einer
vorhandenen Statistik sind drei verschiedene Schritte. Die Anleitung erteilt
keine automatische Legacy-Migration. Fuer Lernende bleiben bestehende
Projektvorgaben, eigene Forks und institutionelle Git-Referenzen massgeblich.

Installation, context initialization and replacement of existing statistics
are distinct operations. No automatic legacy migration is authorized. Existing
project rules, learners' forks and institutional Git references remain applicable.

<a id="community"></a>
## Community-Einreichung / Community submission

Die regulaere Veroeffentlichung ist vom Spec-Kit-Community-Katalog unabhaengig.
Am 2026-09-18 war noch Autonomous Run Governance v0.4.4 ueber
[Issue #4522](https://github.com/github/spec-kit/issues/4522) und
[PR #4586](https://github.com/github/spec-kit/pull/4586) offen. Vor einer neuen
Einreichung deren aktuellen Abschluss **und** die uebernommene Katalogversion
pruefen; dieser datierte Hinweis ist kein dauerhafter Live-Status.

Regular publication is independent of the community catalog. On 2026-09-18,
the linked Autonomous Run v0.4.4 submission was still open. Recheck issue/PR
completion and the actual catalog version before advancing; this dated note
is not a live status feed.

Danach nur mit eigenem Auftrag das offizielle Preset-Issue-Formular verwenden.
Kein direkter Katalog-PR, kein paralleles Submission-Issue, keine automatische
Label-Anfrage. Diese Lieferung fuehrt keine dieser Aktionen aus. Ein
Community-Katalogeintrag ist keine Voraussetzung fuer die direkte URL-Installation.

Then, only under separate authority, use the official preset issue form.
Do not create a direct catalog PR, concurrent submission or automatic label
request. This delivery performs none of those actions. Direct URL installation
does not require catalog acceptance.

<a id="history"></a>
## Historie und Dokumentationsstand / History and documentation

| Datum / date | Entscheidung oder Ereignis / decision or event |
| --- | --- |
| 2026-09-14 | Erstes v0.1.0-Pre-Release mit Paket-/CI-Nachweisen / initial pre-release with package/CI evidence |
| 2026-09-18 | Drei Pilotlieferungen und zentraler Feldtest fachlich abgenommen, PR #3 gemergt / pilots and central field report accepted and delivered |
| 2026-09-18 | Separater Plan fuer regulaere Freigabe und ausfuehrliche Dokumentation genehmigt / separate regular-publication and documentation plan authorized |

Die damalige README und Lieferdokumentation auf `main` beschrieben den fortgeschriebenen
v0.1.0-Freigabestand. Im unveraenderten v0.1.0-Tag-ZIP bleiben README, Liefertext und
urspruengliche Release-Evidence historisch. Neue Release-Notes verlinken deshalb
den abgenommenen Bericht commitgebunden ausserhalb des alten ZIPs. Weder
Tagverschiebung noch neuer Paketexport ist fuer diese Metadaten-Promotion noetig.

At that time, main-branch guidance tracked v0.1.0 approval. The unchanged v0.1.0 tag ZIP retains
historical README, delivery text and original evidence. Updated release notes
link the accepted report at its exact later commit outside that ZIP. Metadata
promotion requires neither moving the tag nor exporting a new package.

<a id="impact"></a>
## Dokumentationsauswirkung / Documentation impact

`UpdateRequired`. Owner: Thorsten Hindermann. Zielgruppen: Lernende, Entwickler,
Maintainer und Reviewer. Leserpfade: README fuer Nutzung; dieser Liefervertrag
fuer Freigaben; Methodik/Schema fuer fachliche Details; Feldbericht/Manifest fuer
historische Evidence. Kanonische Quelle ist dieses Produktrepository; der
Release-Status wird in GitHub-Metadaten gefuehrt. Dokumentklasse: ActiveSemantic,
Original-Evidence als unveraenderte historische Snapshots. DE zuerst/EN danach,
Navigation per Inhaltsverzeichnis, Tabellen ohne Farbzwang, Codebloecke mit
Sprachkennung. Plattformnachweis: isolierte Beispiele und native Paket-CI,
keine erfundene Plattformabnahme. Distribution: aktuelle Repository-Dokumente
und Release-Notes, **kein** Home-Runtime-Sync. Kein Paket-, Profil-, Schema- oder
API-Wechsel. Re-Evaluation bei Quellen-, Methoden-, Versions- oder Autoritaets-
wechsel; neue Evidence ergaenzen, statt alte Ergebnisbytes zu ueberschreiben.

`UpdateRequired`, owner Thorsten Hindermann. Audience: learners, developers,
maintainers, reviewers. README guides use; this guide governs delivery;
methodology/schema define details; report/manifest preserve field evidence.
Canonical source: this product repository; live release status: GitHub metadata.
Active semantic docs with immutable historical evidence bytes. German first,
English second; navigable contents, text tables, language-tagged code blocks.
Proof comes from isolated examples and native CI, never invented platform
acceptance. Current repository docs and release notes only, no Home Runtime
sync or package/profile/schema/API changes. Reevaluate on source, method,
version or authority changes; append evidence rather than rewrite history.
