# Zentraler Feldtestbericht v0.1.0 / Central field-test report v0.1.0

## Entscheidung und Umfang / Decision and scope

Stand: 2026-09-18. Preset: Project Statistics Governance v0.1.0.
Technische Zusammenstellung und Quellenpruefung: Codex.
Owner und fachlicher Reviewer: Thorsten Hindermann (@hindermath).

**Technische Empfehlung: `ReleaseAccepted`. Fachliche Abnahme: `Accepted`.**
Die Empfehlung gilt ausschliesslich fuer das unveraenderte v0.1.0-Paket im
unten belegten Drei-Repository-Feldtest. Sie ist keine stabile Releasefreigabe,
Community-Einreichung, Produktabnahme oder Freigabe weiterer Rollouts.
Dieser Bericht ist der kanonische, fachlich abgenommene Abschlussbericht.
Fruehere lokale Pilotberichte bleiben datierte Einzelbelege, auch wenn sie
inzwischen ueberholte Aussagen zu noch ausstehenden Lieferungen enthalten.

As of 2026-09-18, the technical recommendation is `ReleaseAccepted` for the
unchanged v0.1.0 package in the three documented pilots. Human acceptance is
`Accepted`. This canonical cross-repository report does not promote a stable
release, submit to the community catalog, accept a product or authorize more
rollouts. Earlier pilot reports remain historical evidence, not current status.

Ziel ist **reproduzierbare Projekttransparenz**: Textbestand, Artefaktmix und
sichtbare Git-Aktivitaet mit nachpruefbarer Quellenbindung. Die Repositories
dienen Infrastruktur, Beispielen und KI-gestuetzten Ausbildungsinhalten,
insbesondere fuer die vier IHK-IT-Ausbildungsberufe; sie sind keine produktiven
Anwendungen. Sichere Entwicklung soll frueh vermittelt werden. Statistikwerte
messen weder Lernerfolg, Personenleistung, Softwarequalitaet noch tatsaechliche
KI-Arbeitszeit oder Zeitersparnis. Referenz-Modellrechnungen bleiben in den
Pilotkonfigurationen ausgeschaltet.

The purpose is reproducible project transparency, not a productivity claim.
These infrastructure/example/training repositories support early secure
development education, especially the four German IHK IT training occupations.
They are not production applications. Counts do not measure learning outcomes,
individual performance, software quality, actual AI work time or time savings.
Reference estimates remain disabled in the pilot configurations.

## Version und Nachweisbindung / Version and evidence binding

- [Pre-Release v0.1.0](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/releases/tag/v0.1.0)
- Paket-Commit / package commit: `7e824ca8de11212aefdc5b05d7d05637f5343dab`
- Release-Asset / release asset: `project-statistics-governance-v0.1.0.zip`
- ZIP SHA-256: `d8ad7d5eef920f50b629121b64ba8123c22ec4f6dadd14da5cd826d1c50f420a`
- [Manifest mit vollstaendigen Hashes / full hash manifest](manifest.json)
- Gemeinsame Testsuite / shared test suite SHA-256:
  `6c3f215e53b7724f6c37d8422d10d51043c3cc2c4afb0d0af0cd73757b4e7d1a`

Tag-Aufloesung und ZIP wurden am Berichtsdatum erneut geprueft. GitHub meldet
`prerelease=true`, `immutable=false`: Ein Tagname allein garantiert keine
Unveraenderlichkeit. Commit und ZIP-Hash sind die Bindung dieses Berichts.
Git mit vollstaendiger Historie, PowerShell 7 und Spec Kit >=0.12.8 bleiben
offengelegte Voraussetzungen; Bash delegiert an dieselbe PowerShell-Engine.
Die Piloten verwenden Spec Kit 0.12.8. Kein Paketcode, Tag oder Release wurde
fuer diesen Bericht geaendert.

The tag target and downloaded ZIP were checked again on the report date.
GitHub reports a pre-release without immutable-release protection. This report
therefore binds the exact commit and ZIP hash, not merely a tag name. Complete
Git history, PowerShell 7 and Spec Kit >=0.12.8 remain explicit dependencies;
Bash delegates to the same engine. The pilots use Spec Kit 0.12.8. This report
does not change package code, tags or releases.

## Gelieferte Piloten / Delivered pilots

Alle drei PRs sind gemergt. Werte beziehen sich auf den expliziten Messstichtag
2026-09-18 und die Quellenrevision im Manifest, nicht auf beliebige spaetere Heads.

All three PRs are merged. Values bind the explicit 2026-09-18 cutoff and the
measurement revisions in the manifest, not arbitrary later branch heads.

| Pilot / pilot | Feldtest-PR / field PR | Textdateien / files | Textzeilen / lines | UTC-Aktivtage / days |
| --- | --- | ---: | ---: | ---: |
| Home Baseline, Skript-Infrastruktur / script infrastructure | [#301](https://github.com/hindermath/home-baseline/pull/301) | 3236 | 696778 | 111 |
| TinyCalc, .NET-Anwendung / .NET application | [#86](https://github.com/hindermath/TinyCalc/pull/86) | 1374 | 215686 | 80 |
| ABS-DD Sandbox, Container-Projekt / container project | [#73](https://github.com/hindermath/absdd-image-sandbox/pull/73) | 1308 | 233683 | 70 |

| Pilot | Messquelle / measured source | Gepruefter PR-Head / reviewed head | Merge |
| --- | --- | --- | --- |
| Home | `e2767650c263` | `fab6c48591ac` | `72f8d37af007` |
| TinyCalc | `fac1335c592b` | `cee985a101c7` | `d372cc95d768` |
| Sandbox | `95b12394f358` | `4a02ca2368c8` | `41eb4b30b3cd` |

Vollstaendige SHA-Werte stehen im Manifest. Die synthetischen PR-Test-Merges
der unten verlinkten CI-Laeufe enthalten jeweils den geprueften PR-Head als
Elterncommit und haben denselben Dateibaum wie dieser Head; sie werden nicht
mit dem spaeteren Merge-Commit verwechselt. Jeder archivierte Delivery-Check
meldet `CURRENT`, `reproducible=true`, Exitcode 0 und null gepruefte
Inhaltsaenderungen. Home und TinyCalc wurden fuer diesen Bericht zusaetzlich
lokal read-only geprueft. Die Sandbox wurde nicht erneut gestartet; dort gelten
die archivierten nativen Checks und der dokumentierte Post-Merge-Abschluss.

Full SHAs are in the manifest. Each synthetic PR test merge includes the
reviewed head as a parent and has the same file tree; it is distinct from the
final merge commit. Archived delivery checks report `CURRENT`, reproducibility,
exit code 0 and no checked content changes. Home and TinyCalc also passed a
fresh local read-only check. The sandbox was not restarted for this report;
its native evidence and recorded post-merge closeout are the applicable proof.

### Historische Messung und Legacy-Vergleich / Historical and legacy comparison

Home trennt bewusst zwei Nachweise: Der native Encoding-Feldtest verwendet
Pilot-Commit `af78a4accdaea059728a53e6b113d543e35fb030` und Messquelle
`e1d6d36e444cc252a0321572e70da8874de81fe3` zum 2026-09-16
(3234 Dateien, 696425 Zeilen, 110 UTC-Aktivtage). Der separate Delivery-Schritt
prueft die obige spaetere Messquelle. Die Encoding-Tests werden deshalb nicht
als erneute Vollpruefung der spaeteren Messung ausgegeben. Bei TinyCalc und
Sandbox ist die native Messquelle identisch mit der Delivery-Messquelle.

Home deliberately separates its historical native encoding pilot (2026-09-16,
3234 files, 696425 lines, 110 UTC active days) from the later delivery freshness
check. Encoding results are not relabeled as a full retest of the later
measurement. TinyCalc and Sandbox use the same source in both checks.

Die bestehenden Statistiken bleiben kanonisch und unveraendert in ihrer
Methodik. Home: 696778 Zeilen / 113 Europe-Berlin-Aktivtage; Sandbox:
233683 / 73. TinyCalc: 206466 / 83 statt 215686 / 80 im Preset-Piloten.
TinyCalcs Legacy-Ausschluesse fuer Intake-, Evidence- und Versionsdateien
erklaeren die Differenz von 9220 Zeilen. UTC versus Europe/Berlin erklaert
unterschiedliche Aktivtaggrenzen. Verschiedene Auswahlregeln werden nicht
durch eine Konfigurationsaenderung kuenstlich angeglichen.

Legacy statistics remain canonical under their existing methodology. Home and
Sandbox line counts agree; local-time activity days differ. TinyCalc's legacy
count is 9220 lines lower because its exclusions differ. These are selection
and timezone differences, not a failed same-contract parity test. No migration
or artificial alignment of the configurations is included.

## Plattformen und Testfaelle / Platforms and test cases

| Pilot | Lokaler Feldnachweis / local proof | Native CI / native CI |
| --- | --- | --- |
| Home | macOS, PowerShell 7.6.6; 67 Assertions, 8 Encoding-, 6 Raw-Blob-Faelle | [35337035206](https://github.com/hindermath/home-baseline/actions/runs/35337035206) |
| TinyCalc | macOS, PowerShell 7.6.6; 67 / 8 / 6 | [35343235438](https://github.com/hindermath/TinyCalc/actions/runs/35343235438) |
| Sandbox | Linux aarch64 im Podman-Container, PowerShell 7.6.4; 67 / 8 / 6 | [35348466728](https://github.com/hindermath/absdd-image-sandbox/actions/runs/35348466728) |

Jeder verlinkte native Lauf ist erfolgreich: Ubuntu 24.04 mit 67 Assertions,
8 Encoding-Faellen (Bash und PowerShell), Windows 2022 mit 61 Assertions und
4 Encoding-Faellen (PowerShell); jeweils 6 Raw-Blob-Faelle. PowerShell ist
7.6.5, ausser Sandbox/Windows mit 7.6.6. Die sechs Unix-spezifischen Assertions
werden unter Windows nicht als ausgefuehrt gezaehlt. Die eigenstaendige
[Release-CI](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/actions/runs/34852050586)
ergaenzt native macOS-/Linux-/Windows-Paket- und Installationsnachweise.

Each linked native run succeeded: Ubuntu has 67 assertions and 8 encoding
cases across Bash and PowerShell; Windows has 61 assertions and 4 PowerShell
encoding cases. Both have 6 raw-blob cases. Unix-only assertions are not counted
as Windows coverage. PowerShell versions are recorded above and in the
manifest. The separate release CI adds native three-platform package and
installation evidence; local tests are not substituted for native Windows.

| Pruefung / check | Erwartung / expected | Belegtes Ergebnis / observed |
| --- | --- | --- |
| Init, Update, Wiederholung / repeated update | Exit 0, deterministische Ausgabe / deterministic output | bestanden / passed |
| Status der gelieferten Messung / delivered status | Exit 0, CURRENT, reproducible, changed=false | alle drei / all three |
| LF/CRLF, mit/ohne BOM / with/without BOM | gleiche fachliche Entscheidung / same semantic decision, Exit 0 | 8 Unix / 4 Windows je Pilot / per pilot |
| Raw-Git-Blobs und Filter / raw Git blobs and filters | 6 Faelle ohne Filterverfaelschung / cases without filter distortion | je nativer Plattform / per native platform |
| Wiederholtes Init, Dirty Tree, ungueltige Revision/Datum/Schema/Pfade / invalid inputs | Blockierung / blocked, Exit 2 | bestanden / passed |
| Shallow-/Promisor-Repo; fehlendes/veraltetes PowerShell / missing/old PowerShell | Blockierung / blocked, Exit 2 | bestanden; Shell-Voraussetzungen Unix / shell prerequisites Unix |
| Symlink-Ausbruch / symlink escape | Blockierung / blocked, Exit 2 | Unix bestanden / passed |
| Quellen-, Zeitfenster-, Berichts-, Snapshot- oder Coverage-Drift / drift | Exit 1 | bestanden / passed |
| Rename, Loeschung, Merge, Binaerdateien, Zeitzonen / Git counting boundaries | definierte Methodik / defined methodology | Fixtures bestanden / passed |
| Nenner null / zero denominator | nicht berechenbar, keine erfundene Zeit / not calculable | bestanden / passed |
| Read-only-Status / read-only status | gleiche gepruefte Hashes und Git-Zustaende / unchanged checked hashes and Git states | bestanden / passed |

Die Tabellen verdichten die vorhandene Testsuite, behaupten keine vollstaendige
Sicherheitspruefung. Read-only ist durch die im Workflow geprueften Dateien,
Hashes und Git-Zustaende belegt, nicht durch einen systemweiten I/O-Trace.
Disable/Enable, Remove mit Erhalt verfasster Berichte und erneute Installation
werden in der isolierten Paket-Lifecycle-Suite geprueft. Die Pilotinstallation
nutzt das zentrale 14-Preset-Profil; Quellenbindung und 26 Paketdateien wurden
in den Installations-/Pilotnachweisen geprueft. Das Preset startet weder
einen Produkt-Spec-Kit-Lauf noch eine Legacy-Migration.

The table summarizes the existing suite, not an exhaustive security audit.
Read-only proof covers the workflow's checked files, hashes and Git state,
not all operating-system I/O. Isolated lifecycle tests cover disable/enable,
removal preserving authored reports and reinstall. Pilot installation uses
the central 14-preset profile with source binding and 26 package files checked
in installation/pilot evidence. Installation starts no product feature run.

Die PR-Statusabfragen zeigen Home 22 erfolgreiche Check-Eintraege, TinyCalc
25 erfolgreiche und einen bedingt uebersprungenen `claude`-Eintrag, Sandbox
13 erfolgreiche. Diese Zahlen enthalten teils Push-/PR-Doppelmeldungen und
sind keine Anzahl unabhaengiger Tests. TinyCalcs
[Build/Test-Lauf](https://github.com/hindermath/TinyCalc/actions/runs/35343235443)
belegt Restore, Release-Build, Tests und nichtinteraktiven TUI-Smoke unter
Windows und Ubuntu. Eine neue menschliche TUI- oder Produktabnahme folgt
daraus nicht.

PR status entries show 22 successful for Home, 25 successful plus one
conditional `claude` skip for TinyCalc, and 13 successful for Sandbox. These
include duplicate push/PR reporting and are not independent test counts.
TinyCalc also passed restore, Release build, tests and noninteractive TUI smoke
on Windows and Ubuntu. This is not a new human UI or product acceptance.

## Quellenarchiv und Nachpruefung / Evidence archive and verification

Die 66 Dateien unter [evidence/](evidence/) sind bytegetreue Downloads der drei
oben verlinkten CI-Laeufe: native Ergebnis- und Delivery-JSONs, Encoding-
Entscheidungen und Fixture-Logs. `.gitattributes` verhindert LF-/CRLF-Normalisierung.
Das Manifest erfasst SHA-256 und Bytezahl jeder Datei. Native Payload-, Suite-,
Workflow- und Snapshot-Hashes wurden gegen die gebundenen Git-Blobs geprueft;
Entscheidungs- und Log-Hashes gegen die archivierten Originalbytes. CI-Artefakte
koennen ablaufen; diese versionierte Kopie bewahrt die verwendeten Ergebnisse.

The 66 archived files are byte-preserving downloads from the linked CI runs.
Git attributes prevent line-ending normalization. The manifest records each
file's SHA-256 and size. Native payload, suite, workflow and snapshot hashes
were checked against bound Git blobs; decisions and logs against archived
bytes. The versioned archive preserves results beyond CI artifact retention.

Lokale/ergänzende Quellen / local and supporting sources:

- [Home-Pilot samt historischen Nachweisen / historical Home pilot](https://github.com/hindermath/home-baseline/tree/fab6c48591ac08d8deb5e9e9cf69772ceef78c1d/docs/project-statistics-pilot)
- [TinyCalc-Pilot und Legacy-Erklaerung / TinyCalc pilot and legacy comparison](https://github.com/hindermath/TinyCalc/tree/cee985a101c75b5034c20b4f72ab6801384a6e69/docs/project-statistics-pilot)
- [Sandbox-Pilot / Sandbox pilot](https://github.com/hindermath/absdd-image-sandbox/tree/4a02ca2368c83d02bf5b601ee7fb581ec7dab87a/docs/project-statistics-pilot)
- [Sandbox Post-Merge-Abschluss / post-merge closeout](https://github.com/hindermath/absdd-image-sandbox/pull/73#issuecomment-5730711440)

## Findings und Restrisiken / Findings and residual risks

1. **Zweistufige Lieferung:** Konfiguration und fachliche Dokumente zuerst
   committen, dann die generierten Statistiken nachfuehren. Spaetere gemessene
   Dateien koennen Drift erzeugen; die Blockierung ist beabsichtigt.
2. **Vergleichsgrenzen:** UTC-Aktivtage, Legacy-Ausschluesse und Homes getrennte
   historische/neue Messung muessen bei jeder Auswertung sichtbar bleiben.
3. **Umgebungsgrenzen:** Windows deckt Unix-Shell-Faelle nicht ab. Der Sandbox-
   VM-Startabbruch betraf die Lebensdauer der Hostsitzung, nicht die Messengine.
   Die dokumentierte Fortsetzung hielt die Startsitzung offen und stoppte
   anschliessend geordnet; keine Volumes wurden fuer den Feldtest geloescht.
4. **Lieferkette und Nachweisgrenzen:** Hash-Bindung bleibt erforderlich, weil
   der Release nicht immutable ist. Archivierte CI-Ausgaben sind technische
   Evidence, kein Zertifikat und kein systemweiter Read-only-Beweis.
5. **Abnahmegrenze:** Die drei Pilotlieferungen ersetzen nicht die getrennte
   fachliche Sichtung dieses zentralen Berichts. Kein produktiver Einsatz,
   keine C5-, Konformitaets-, Zertifizierungs- oder Produktfreigabe wird behauptet.

1. Commit configuration/authored evidence before regenerating statistics;
   later measured-file changes intentionally trigger drift.
2. Preserve timezone, selection and historical/current measurement boundaries.
3. Windows does not cover Unix shell cases. The Sandbox startup interruption
   concerned host-session lifetime; the documented continuation kept that
   session alive until an orderly stop, without deleting volumes.
4. Keep hash verification because the release is not immutable. CI records are
   technical evidence, not certification or an operating-system-wide I/O proof.
5. Pilot deliveries do not replace human review of this report. No production,
   C5, conformity, certification or product approval is claimed.

Innerhalb dieses begrenzten Feldtests wurde kein Paketfehler nachgewiesen, der
einen Patch vor der fachlichen Sichtung erfordert. Daraus folgt die oben
genannte technische Empfehlung, keine pauschale Fehlerfreiheitsbehauptung.

No package defect requiring a patch before human review was established in
this bounded field test. This supports the stated technical recommendation,
not a claim that the package is free of all defects.

## Getrennte fachliche Abnahme / Separate human acceptance

Status: **Accepted**. Reviewer: **@hindermath**. Datum: **2026-09-18**.
[Entscheidungsnachweis / decision record](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/pull/3#issuecomment-5733063715).
Abgenommener Berichts-Head / accepted report head:
`abe5a8761ae4fe6445b782a14e9570a75bd5c29b`.

Thorsten Hindermann bestaetigte nach Vorlage des Berichts in der Codex-Sitzung:
„Ich bestätige anschließend die fachliche Abnahme und Lieferfreigabe!“
Codex dokumentiert diese Nutzerentscheidung; sie ist kein unabhaengiges
GitHub-Review. Dieser Folgecommit erfasst nur die Abnahme. Paket, Messungen
und archivierte Ergebnisbytes bleiben unveraendert. Die finale Lieferung
benoetigt erneut gruene Checks und einen Merge am exakt geprueften Head.

- [x] Zweck und Darstellung als reproduzierbare Projekttransparenz akzeptiert.
- [x] Quellenbindung, Methodikunterschiede, Plattformgrenzen und Restrisiken gesichtet.
- [x] Technische Empfehlung ausschliesslich fuer diesen v0.1.0-Feldtest akzeptiert.
- [x] Keine Produktivitaets-, Lernleistungs-, Produkt- oder Zertifizierungsfreigabe abgeleitet.
- [x] Lieferung des zentralen Berichts am benannten PR-Head ausdruecklich freigegeben.

The owner explicitly confirmed human acceptance and delivery authorization
after receiving this report. The dated decision record above binds the accepted
head and records the user's conversation approval, not an independent GitHub
review. This follow-up only records acceptance; package code, measurements
and original evidence remain unchanged. Delivery still requires fresh green
checks and an exact-head merge. Acceptance is not inferred from earlier pilot
approvals or technical tests.

Abschluss-Tracking / closeout tracking: [Preset #1](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/issues/1),
[Home #298](https://github.com/hindermath/home-baseline/issues/298),
[TinyCalc #84](https://github.com/hindermath/TinyCalc/issues/84),
[Sandbox #70](https://github.com/hindermath/absdd-image-sandbox/issues/70).
Die fachliche Abnahme ist erteilt; die Issues werden erst nach bestaetigtem
Merge des zentralen Berichts mit Liefernachweis geschlossen. Stabile
Veroeffentlichung, Community-Einreichung und Legacy-Migration bleiben separate
Entscheidungen.

Human acceptance is recorded; close tracking only after the central report's
merge is verified and linked as delivery evidence. Stable publication, community
submission and legacy migration remain separate decisions.

## Dokumentationsauswirkung / Documentation impact

`UpdateRequired`. Owner: Thorsten Hindermann. Zielgruppen: Lernende,
Maintainer und Reviewer. Kanonische Quelle: dieser Bericht, mit Manifest,
unveraenderten archivierten Ergebnissen und gebundenen externen Git-Quellen.
Leserpfad: README / Lieferung -> Feldbericht -> Manifest -> Evidence / Pilot.
Dokumentklasse: ActiveSemantic; Evidence als historische Ergebnis-Snapshots.
Sprache: DE zuerst, EN danach im selben Dokument; keine separate Sprachdatei.
Plattform- und Beispielnachweis: oben begrenzt ausgewiesen. Tabellen behalten
Textwerte und benoetigen keine Farbe. Distribution: Repository-Dokumentation;
keine Aenderung am veroeffentlichten Paket, kein Home-Runtime-Sync.
Wiedervorlage: fachliche Entscheidung sowie Aenderung von Version, Methodik,
Konfiguration oder Quellen. Neue Ergebnisse ergaenzen die Historie, statt die
Originalbytes zu ueberschreiben.

`UpdateRequired`, owner Thorsten Hindermann. Audience: learners, maintainers,
reviewers. This report is canonical, backed by the manifest, original archived
results and bound Git sources. Navigation leads from README/delivery to report,
manifest and evidence. Active semantic documentation with historical evidence;
German then English in one document. Text tables do not depend on color.
Repository documentation only; no published-package change or Home Runtime
sync. Reevaluate on human decision or version/method/configuration/source
changes; append new results rather than replacing original evidence bytes.
