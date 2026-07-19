# Secure CaseTracker Feldtest / Secure CaseTracker Field Test

Datum / Date: `2026-07-18` bis `2026-07-19`
Kampagne / Campaign: `91c2c1a0-1526-479a-b8a3-e36a7d15d2b1`
Preset-Linie im Feld / Preset line in the field: `0.1.2`, Schema `1.0`
Erkenntnis-Promotion / Findings promotion: `0.2.0`, Schema `1.1`

## Ergebnis / Result

Der native Entwicklungsfeldtest ist operativ abgeschlossen:

- `24/24` autonome Worker erreichten `ReadyForMerge`.
- Alle 24 Entwicklungs-PRs wurden mit ihrem erwarteten exakten Head gemergt.
- Alle sechs Sprach-Repositories bestanden ihre lokalen Hauptbranch-Gates.
- Sechs separate Closeout-PRs archivierten die Units 00 bis 03 und
  aktualisierten die Abarbeitungsreihenfolge.
- Alle sechs lokalen Repositories stehen sauber auf `main == origin/main`.

*The native development field test is operationally complete: all 24 workers
reached `ReadyForMerge`, all 24 development PRs were merged at their exact
expected heads, all six language repositories passed their local main-branch
gates, six closeout PRs archived Units 00 through 03, and every local
repository is clean at `main == origin/main`.*

## Umfang / Scope

| Sprache / Language | Repository | Units | Worker |
|---|---|---:|---|
| C# | `securecasetracker-csharp` | 00-03 | 4 |
| Go | `securecasetracker-go` | 00-03 | 4 |
| Java | `securecasetracker-java` | 00-03 | 4 |
| Python | `securecasetracker-python` | 00-03 | 4 |
| Rust | `securecasetracker-rust` | 00-03 | 4 |
| Swift | `securecasetracker-swift` | 00-03 | 4 |
| Gesamt / Total | 6 MSL-Repositories | 24 Units | 24 |

Topologie war `Pipeline`. Je Sprache galt `Unit 00 -> 01 -> 02 -> 03`; zwischen
den Units wurden deklarierte, gehashte Handoffs verwendet. Die konfigurierte
und beobachtete maximale Parallelitaet betrug jeweils `3`.

*The topology was `Pipeline`. Each language followed `Unit 00 -> 01 -> 02 ->
03` with declared hashed handoffs. Configured and observed maximum concurrency
were both `3`.*

## Umgebung und Runner / Environment and Runner

| Feld / Field | Nachweis / Evidence |
|---|---|
| Host | macOS `26.5.2` (`25F84`) |
| PowerShell | `7.6.3` |
| GitHub CLI | `2.96.0` |
| Codex CLI | `0.144.6` beim Closeout / at closeout |
| Kampagnenprofil | `native-codex-field` |
| Runner | `codex exec --ephemeral`, nativer Entwicklungs-Override |
| Manifest-Schema | `1.0` |
| State-Schema | `1.0` |

Das Kampagnenmanifest schrieb keinen Modellnamen und keine Reasoning-Stufe
fest. Die lokale Codex-Konfiguration zeigte beim Closeout `gpt-5.6-sol` und
`xhigh`. Das ist eine beobachtete lokale Herkunft, kein persistierter
Kampagnenvertrag und keine Modellvorgabe des Presets.

*The campaign did not prescribe a model or reasoning effort. The local Codex
configuration observed at closeout contained `gpt-5.6-sol` and `xhigh`. This is
local provenance, not a persisted campaign contract or preset requirement.*

## Stop, Status und Resume / Stop, Status, and Resume

Die geforderte Bedienfolge wurde im laufenden Feldtest ausgefuehrt:

1. `Stop` setzte eine kooperative Stop-Anforderung; kein Prozess wurde beendet.
2. `Status` las den persistierten Zustand ohne Aenderung.
3. `Resume` setzte die Kampagne nach Revalidierung fort.
4. Ein zweiter gleichzeitiger `Resume`-Versuch wurde durch den Campaign-Lock
   abgewiesen.

Alle bereits fertigen Worker blieben erhalten. Es entstand weder ein
doppelter Worker-Abschluss noch ein zweiter Merge.

*The required command sequence was exercised. Stop was cooperative, Status was
read-only, the first Resume continued after revalidation, and a concurrent
second Resume was rejected by the campaign lock. Completed work was preserved
without duplicate worker completion or merge.*

## Entwicklungs-PRs / Development PRs

| Worker | PR | Exakter Head / Exact head | Merge-Commit |
|---|---:|---|---|
| `csharp-unit-00` | C# #3 | `6974f8b86651e4740ade32528bb8e6c7e5840939` | `0d9de726a60fa2e1881629449d26118362ab33e8` |
| `csharp-unit-01` | C# #4 | `492ba7d9c440ea4039e9bb808f6861acdaffd0a3` | `699fa9f8df50bb037eaef5d8a6955d329e81940e` |
| `csharp-unit-02` | C# #5 | `bb6cf7ff50c1f88fc09ad1286484b6ddd410f11d` | `eb54c88aaec7057e70f343b7d457067389b53e34` |
| `csharp-unit-03` | C# #6 | `d3615607f4c3292b2d1a80045add169e103271ba` | `23c0cf16a2178251901c4d10923ed489060f47c0` |
| `go-unit-00` | Go #3 | `7d7b0d6fb68fb2eb6f69c48a82215c7c029d420d` | `e908d0ce5535c7c33a497e25f93ae25dc7c4960d` |
| `go-unit-01` | Go #4 | `15c92d176f0536e3343da2c11c7313bb5e8aa233` | `7bfddd14f3c583a202d6ca320dc9936acee2ab75` |
| `go-unit-02` | Go #5 | `56768b642bc727587d0d809e64e82929b8556e72` | `a91e934d48886895479ad28e5e3f8059d80e681a` |
| `go-unit-03` | Go #6 | `31ba2c393feb24503067063048048f371a92e6d7` | `a4c73de3f02af14a6859fb016402795ec36dfecf` |
| `java-unit-00` | Java #3 | `2e5bfff9b51c18befa5bfb4768e81d9af4a4a9e7` | `6deceaaf38e00c7e62cc0a435635002f4663aa3e` |
| `java-unit-01` | Java #4 | `0ee426e163895c9bcb37b1ea6a9e0a233b3aa9fb` | `dad12f2670effaa99b4f060587403b1d874b282f` |
| `java-unit-02` | Java #5 | `2335c81be8345ac5f9e9cb1a65bf136b7b472986` | `72e43eaf7dd31a67546376efcf47565ca56c8d9e` |
| `java-unit-03` | Java #6 | `e580de341eddb9442833ca22cdd3193be13bd2ad` | `026e295859dd853ff40cbc9b3a93fbfa2051f6af` |
| `python-unit-00` | Python #3 | `88fa148b13e6199c7b394f3c988c8aaed99d4fd7` | `7e68ae9f30ec13f35e7594e063091fcb61ae68d6` |
| `python-unit-01` | Python #4 | `6a43d817bd4fe36f1e29d9588bebad95cbe50337` | `8f58e44d4180e7d7135ecc148801a07c1cc917a9` |
| `python-unit-02` | Python #5 | `1fcbca0b45d612d01bd1b96941e97a202f9763a9` | `0167287e36c50d4b78f94169dde52c09751db4a1` |
| `python-unit-03` | Python #6 | `4b8a18997df5dd4b2c5a15ea53cf95a836abd25f` | `12eaef7466c5371db6d03a0d5451145d09b81661` |
| `rust-unit-00` | Rust #3 | `834b9aaf5efe33e634f243126239e6961f2d9351` | `419a55c49ffb62a69ba95c70260d626997845058` |
| `rust-unit-01` | Rust #4 | `463d7a195a90132d83972efa6e41366d051ff1b8` | `388ce4e80fa2a01efbdc7e279b475e72442ef243` |
| `rust-unit-02` | Rust #5 | `c179ef52d2d574c5c1a6196b81f30003ae721047` | `ed683f6e9d424697626520d60d6af74bac46bb5a` |
| `rust-unit-03` | Rust #6 | `3c3d5055d0e6b8a3b7b7ab68749e6ba0a8a233fd` | `cb6452f2831a3dfefc5072504742910dbf27639b` |
| `swift-unit-00` | Swift #3 | `9b54a7dc5a1af6575a44efd25b5dac8c26961a5d` | `8178ebef4d5354cd456f6ed5a09a4937f6cd18dc` |
| `swift-unit-01` | Swift #4 | `120228d88e2b493c5f2884c8ac17940b4d953880` | `d85eb0d8239c9454dbba8621583414b2ab60bdaf` |
| `swift-unit-02` | Swift #5 | `ea97a172849635c5d5228ae715dbbbb75a078468` | `30a930970294818d10ef6dd1ac9dd94449c06ec2` |
| `swift-unit-03` | Swift #6 | `351d7b52e9e921731ecc0a9dcd781e96010d3d27` | `ec117bae9d6402471075d022a8975e5228761ed0` |

## CI- und Review-Klassifikation / CI and Review Classification

Vor der Konsolidierung wurden `88` eindeutige fehlgeschlagene Workflow-Runs
genau einmal mit `--failed` neu gestartet. Attempt 2 enthielt `160/160` Jobs
mit null Schritten und exakt der bekannten GitHub-Provider-Ablehnung zu
Account-Zahlung oder Spending-Limit; es gab keine Ausnahme und keinen
technischen Testfehler. Diese Jobs sind feldtestspezifisch `N/A`, nicht
bestanden.

Die sechs Closeout-PRs erzeugten weitere `60/60` nullschrittige Jobs mit exakt
derselben Provider-Ablehnung. Copilot pruefte jeden aktuellen Closeout-Head und
erzeugte keinen aktuellen Review-Thread. Der enge Billing-Override endet mit
diesem Closeout und wird nicht in Preset `0.2.0` uebernommen.

*Before consolidation, 88 unique failed workflow runs were rerun once with
`--failed`. Attempt 2 contained 160 of 160 zero-step jobs with the exact known
GitHub provider rejection and no technical failure. The six closeout PRs added
60 of 60 identical zero-step jobs. These are field-specific external `N/A`,
not passing evidence. The narrow billing override expires here and is not a
general `0.2.0` rule.*

## Hauptbranch-Gates / Main-Branch Gates

| Sprache / Language | Lokale Abschlusspruefung / Local final validation |
|---|---|
| C# | Restore, Build, Format, 83 Tests, CLI, Vulnerability, Homogeneity, Secrets, PSScriptAnalyzer |
| Go | Format, Build, Race Tests, Vet, `govulncheck`, Homogeneity, Secrets, PSScriptAnalyzer |
| Java | Maven Verify, 173 Tests, Checkstyle, SBOM, Homogeneity, Secrets, PSScriptAnalyzer |
| Python | Compile, 165 Tests, `pip-audit`, CLI, Homogeneity, Secrets, PSScriptAnalyzer |
| Rust | Format, Clippy, Tests, Build, Rustdoc, Metadata, Homogeneity, Secrets, PSScriptAnalyzer |
| Swift | Build, 37 Tests, Dependency Graph, Homogeneity, Secrets, PSScriptAnalyzer |

## Closeout / Closeout

| Sprache / Language | PR | Exakter Head / Exact head | Merge-Commit |
|---|---|---|---|
| C# | [#7](https://github.com/hindermath/securecasetracker-csharp/pull/7) | `2a8bde15af1185704182220e0cb8b47763050c67` | `ee862a9d77383e28c92cd26b9d10dcd33fbe0b35` |
| Go | [#7](https://github.com/hindermath/securecasetracker-go/pull/7) | `b9047b9363b01ff7987b191941d23ba52b80d3ff` | `00f1f9396a00c6a2085f16eefae86c5ac7721165` |
| Java | [#7](https://github.com/hindermath/securecasetracker-java/pull/7) | `754dfd1451a91e87f50a8890c3c6fb7c51147236` | `9cb6806d8d821264b0cb17e434bd8676ad529e18` |
| Python | [#7](https://github.com/hindermath/securecasetracker-python/pull/7) | `cb595a495460e65437df15cb00d87607b5e5833d` | `ede58a55ce8330ac7d2ac5d3bd0ccdc2d9d6cc7e` |
| Rust | [#7](https://github.com/hindermath/securecasetracker-rust/pull/7) | `81179c26b767104cf37efa37328de80449dd28bc` | `be3391a1cc8d56445ecd0ea7e6d88d4f1c1bafc3` |
| Swift | [#7](https://github.com/hindermath/securecasetracker-swift/pull/7) | `40bade45b6e1fb89c1b43bd885e473ddb22d578c` | `155df23f2081062b94bf5ef04e07ab9e2f550569` |

Die Closeout-Branches archivierten alle vier bearbeiteten Lastenhefte mit den
urspruenglichen Feature-Branch-Namen und aktualisierten
`Lastenheft_Abarbeitungsreihenfolge.md`.

## V1-Abweichungen und V2-Folgen / V1 Deviations and V2 Consequences

1. `Consolidate` behandelte den leeren Pipeline-Auswahlwert als ungueltigen
   Pflichtparameter. V2 macht Auswahl nur fuer Alternativen verpflichtend.
2. V1 checkpointete 21 Merges, konnte einen Teilmerge aber nicht sicher
   fortsetzen.
3. `--delete-branch` loeschte nach Swift Unit 00 die gestapelte Basis und
   GitHub schloss PR #4 automatisch. Die verbleibenden Swift-PRs wurden nach
   exakter Head-Pruefung kontrolliert auf `main` umgebased und gemergt.
4. Der historische V1-State bleibt absichtlich bei `MergeFailed`, 21 `Merged`
   und 3 `ReadyForMerge`. Er wird nicht nachtraeglich manipuliert. GitHub und
   die Closeout-Commits sind der Abschlussnachweis.
5. V1 haette `Completed` vor Lastenheft-Archivierung, Synchronisation und
   Abschlussvalidierung gesetzt. V2 fuehrt manifestdeklarierte
   `postMergeActions` ein.
6. `rename-lastenheft.ps1` akzeptierte einen Root-Dateinamen nur mit
   vorangestelltem `./`; dieser Pfadfall wurde als separater Wartungsbefund
   dokumentiert.

*V2 makes pipeline selection optional, resumes verified partial merges, adopts
externally merged exact heads, revalidates direct and stacked PR bases, checks
stop requests between consolidation operations, and delays `Completed` until
declared closeout and final validation succeed.*

## Integritaetsnachweis / Integrity Evidence

Die unveraenderliche Ausgangskopie liegt lokal unter:

`~/.specify/parallel-runs/secure-casetracker-native-field-20260718/evidence/pre-consolidation-20260719T073505Z`

Sie umfasst Manifest, Runner, State, Ergebnisse, Handoffs, Logs, PR-/Check-
Snapshots und SHA-256-Manifest. SHA-256 des Hash-Manifests:

`2cf9abbf9e5eff55c0ccf516920b7b7c99ee0cbf6b41fcfc1d415faac230186d`

Die Closeout-CI-Klassifikation liegt unter
`evidence/closeout-20260719/closeout-ci-audit.json`.

## Override-Ende / Override Expiry

Thorsten Hindermann autorisierte die native macOS-Ausfuehrung und den engen
Null-Schritt-Billing-Override ausdruecklich fuer diesen Entwicklungsfeldtest.
Beide Ausnahmen enden mit diesem Closeout. Spaetere Secure-Trader-Kampagnen
kehren ohne neuen ausdruecklichen Auftrag zu Container-First und normalen
Provider-Gates zurueck.

*Thorsten Hindermann explicitly authorized native macOS execution and the
narrow zero-step billing override for this development field test. Both expire
with this closeout. Later Secure Trader campaigns return to Container-First and
normal provider gates unless separately authorized.*
