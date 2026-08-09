# Autonome Retrospektive: Feature 038 / Autonomous Retrospective: Feature 038

## Entscheidung / Decision

`Promote`

Drei reproduzierbare, providerneutrale Evidence-Integritätsfehler qualifizieren
sich nach je einem deterministischen Auftreten für eine portable
`PresetFollowUp`-Übergabe: unversionierte Lieferdateien müssen in
Whitespace-Gates einbezogen werden, ein Prozess-Exitcode 0 darf keinen
semantisch gestoppten Modelllauf als abgeschlossen markieren, und Pre-Merge-
sowie Post-Merge-Gates benötigen getrennte, miteinander gebundene
Exact-Head-Snapshots. Diese Entscheidung ändert weder das installierte Preset
noch die Home Baseline und erteilt keine Veröffentlichungs-, Merge- oder
Bypass-Autorität.

*Three reproducible, provider-neutral evidence-integrity defects qualify after
one deterministic occurrence each for a portable `PresetFollowUp` handoff:
untracked delivery files must be included in whitespace gates, process exit
code zero must not mark a semantically stopped model run as complete, and
pre-merge and post-merge gates need separate, linked exact-head snapshots. This
decision changes neither the installed preset nor Home Baseline and grants no
publication, merge, or bypass authority.*

## Laufidentität / Run Identity

| Feld / Field | Wert / Value |
|---|---|
| Feature und Quellrevision / Feature and source revision | Feature 038, `038-example-portfolio-conformance-audit`; geliefert durch PR #144 als Merge-Commit `b59a3fe46e3868728be3557df7f367b8ab832db1` |
| Delivery-Evidence / Delivery evidence | `b59a3fe46e3868728be3557df7f367b8ab832db1:specs/038-example-portfolio-conformance-audit/pr-evidence.md` (Git-Blob `a5c53ad9c523880c509f83918ee4d586d7bb7618`) sowie die Merge-Eltern `92efcf6f2db832b33026ef83077c3e6d361abd79` und `ca0cdf413187efd4710a6bf6436f1863c67bcdcd` |
| Delivery-Modus / Delivery mode | `MergeAndSync` |
| Remote-Ergebnis / Remote result | PR #144 gemergt; `HEAD`, lokales `main` und `origin/main` stehen auf `b59a3fe46e3868728be3557df7f367b8ab832db1`; der alte Feature-Branch ist lokal und unter `origin` nicht mehr vorhanden |
| Voraussetzung / Prerequisite | Projekt-Guard PR #145 wurde als `92efcf6f2db832b33026ef83077c3e6d361abd79` gemergt und über Feature 038 integriert |
| Unterbrechungen und Fortsetzungen / Interruptions and resumes | Fail-closed Stopp bei T211/210 von 225 Aufgaben wegen zwei projektspezifischer DocFX-Links; Fortsetzung nach PR #143. Späterer Delivery-Block durch den TuiVision-Alignment-Guard; Fortsetzung nach PR #145. Kein impliziter Produkt- oder Scope-Edit. |
| Zustand beim Retrospektivstart / State at retrospective start | Der in PR #144 enthaltene State bleibt bei `stage=Publish`, `status=Active` und offenem Closeout; der geroutete Retrospektivaufruf markierte ausschließlich seine eigene Phase lokal als `Running`. Der State wird in dieser Retrospektive nicht geändert. |

## Beobachtungsübersicht / Observation Summary

| ID | Kurzbefund / Short finding | Entscheidung / Decision |
|---|---|---|
| `AR-038-01` | Gewöhnliches `git diff --check` übersieht unversionierte Lieferdateien. / Ordinary `git diff --check` omits untracked delivery files. | `Promote` |
| `AR-038-02` | Exitcode 0 wurde trotz semantischem Stop bei 210/225 als `Completed` gewertet. / Exit code zero became `Completed` despite a semantic stop at 210/225. | `Promote` |
| `AR-038-03` | Ein Ein-Head-Validator kann Pre-Merge- und Post-Merge-Gates nicht gleichzeitig wahrheitsgetreu vor dem Merge abschließen. / A single-head validator cannot truthfully complete pre-merge and post-merge gates together before merge. | `Promote` |
| `AR-038-04` | Der Feature-037-/Active-Intake-Guard war TuiVision-spezifisch. / The Feature-037/active-intake guard was TuiVision-specific. | `RejectProjectSpecific` |
| `AR-038-05` | Die zwei DocFX-Verzeichnislinks waren projektspezifische Dokumentationspflege. / The two DocFX directory links were project-specific maintenance. | `RejectProjectSpecific` |
| `AR-038-06` | Portfolio-, Fixture- und Intake-Kardinalitäten sind akzeptierte TuiVision-Verträge. / Portfolio, fixture, and intake cardinalities are accepted TuiVision contracts. | `RejectProjectSpecific` |

## Beobachtungen / Observations

### AR-038-01 — Unversionierte Lieferdateien im Whitespace-Gate / Untracked delivery files in the whitespace gate

| Feld / Field | Bewertung / Assessment |
|---|---|
| Quelle und unveränderliche Evidence / Source and immutable evidence | Feature 038; `b59a3fe46e3868728be3557df7f367b8ab832db1:specs/038-example-portfolio-conformance-audit/pr-evidence.md`, Abschnitte „Abschluss-Analyze“ und „Retrospektiv-Eingaben“. Der erste Abschluss-Analyze fand drei nachgestellte Leerzeichen im noch unversionierten Closure-Intake; der wiederholte Pass prüfte danach 75/75 unversionierte Lieferdateien pfadweise. |
| Beobachtung und Fehlergrenze / Observation and failure boundary | `git diff --check` prüft den normalen Git-Diff, nicht aber aufzunehmende unversionierte Pfade. Ein positives Whitespace- oder Scope-Gate war deshalb beweisbar falsch, bis jeder beabsichtigte Lieferpfad zusätzlich geprüft wurde. / `git diff --check` checks the ordinary Git diff but not intended untracked delivery paths. A positive whitespace or scope gate could therefore be false until every intended delivery path was checked separately. |
| Artefaktart / Artifact kind | `script requirement` und `checklist` |
| Projektspezifische Ausschlüsse / Project-specific exclusions | Name und Inhalt des Closure-Intakes, die Zahl 75, TuiVision-Allowlist, Receipt-Hash und Feature-038-Pfade werden nicht portiert. / The closure intake name and content, count 75, TuiVision allowlist, receipt hash, and Feature 038 paths are excluded. |
| Providerneutrale Zielregel / Provider-neutral target rule | Vor einem positiven Diff-/Whitespace-Gate muss der Lauf die vollständige beabsichtigte Liefermenge bestimmen und sowohl versionierte Änderungen als auch jeden nicht ignorierten, aufzunehmenden unversionierten Pfad deterministisch prüfen. Fehlt eine eindeutige Liefermenge, stoppt das Gate fail-closed. / Before a positive diff/whitespace gate, determine the complete intended delivery set and deterministically check tracked changes plus every non-ignored untracked path intended for delivery. If the set is ambiguous, fail closed. |
| Auftreten und Konfidenz / Occurrences and confidence | `1` deterministischer Feldfund plus `1` synthetische Reproduktion; `High` |
| Permission- und Evidence-Risiko / Permission and evidence risk | Kein zusätzliches Schreibrecht ist nötig. Eine automatische Stage-Operation wäre dagegen unzulässig, weil sie Indexzustand ändern könnte; die Prüfung muss read-only oder mit isoliertem temporärem Index arbeiten. Ein ausgelassener Pfad erzeugt falsche Pass-Evidence. / No extra write authority is needed. Automatically staging files would mutate index state and is not allowed; use read-only enumeration or an isolated temporary index. Omitting a path creates false pass evidence. |
| Reproduzierbarer Test / Reproducible test | In einem temporären Git-Repository eine unversionierte Datei mit nachgestellten Leerzeichen anlegen. `git diff --check` liefert 0; `git diff --no-index --check /dev/null <path>` liefert 3 und benennt den Defekt. Danach eine saubere unversionierte Datei prüfen und nur den erwarteten No-Index-Diffstatus 1 ohne Whitespace-Diagnose akzeptieren. / Create an untracked file with trailing whitespace in a temporary repository. The ordinary check returns 0, while the path-wise no-index check returns 3 and reports the defect. A clean untracked file must produce only the expected no-index diff status 1 without a whitespace diagnostic. |
| Entscheidung / Decision | `Promote` |

### AR-038-02 — Semantische Phasenvervollständigung / Semantic phase completion

| Feld / Field | Bewertung / Assessment |
|---|---|
| Quelle und unveränderliche Evidence / Source and immutable evidence | Feature 038; `b59a3fe46e3868728be3557df7f367b8ab832db1:specs/038-example-portfolio-conformance-audit/autonomous-run-state.json` (Git-Blob `94f6718d4a040b08aa42fe51da3a409292968310`) bindet `implement.out.txt` über SHA-256 `e8672c9c8d82310ab7862bd3244ab9339e5dc95e13d65765282e74ab66e8a32d`, setzt die Phase aber auf `Completed` und Exitcode 0. Das gehashte Resultat meldet ausdrücklich „210/225“, `Blocked` und keine Delivery-Aktion. Der Wrapper in `b59a3fe46e3868728be3557df7f367b8ab832db1:.specify/presets/autonomous-run-governance/scripts/invoke-autonomous-model-phase.ps1` setzt nach Exitcode 0 ohne Ergebnisprädikat `Completed`. |
| Beobachtung und Fehlergrenze / Observation and failure boundary | Prozess- und Fachstatus widersprachen sich. Der Prozess beendete sich erfolgreich, weil das Modell korrekt einen sicheren Stopp berichtete; der Wrapper interpretierte diesen Transporterfolg fälschlich als fachlichen Phasenabschluss. / Process and domain status conflicted. The process exited successfully because the model correctly reported a safe stop; the wrapper incorrectly interpreted transport success as semantic phase completion. |
| Artefaktart / Artifact kind | `script requirement`, `evidence structure` und `runbook` |
| Projektspezifische Ausschlüsse / Project-specific exclusions | T211, 210/225, DocFX, Versionszähler und konkrete TuiVision-Gates werden nicht Teil des portablen Vertrags. / T211, 210/225, DocFX, version counters, and specific TuiVision gates are excluded. |
| Providerneutrale Zielregel / Provider-neutral target rule | Eine geroutete Phase darf erst `Completed` werden, wenn Prozess-Exitcode und ein maschinenlesbares Ergebnisprädikat übereinstimmen. Mindestens `outcome`, `taskCompleted/taskTotal`, `blockedReason`, erforderliche Gate-Aussage und Ergebnis-Hash müssen validiert werden. Fehlende, widersprüchliche oder nur prose-basierte Completion-Evidence führt zu `Blocked` oder `NeedsRevalidation`, nie zu `Completed`. / A routed phase becomes `Completed` only when process exit status and a machine-readable result predicate agree. Validate at least outcome, task counts, blocked reason, required gate assertion, and result hash. Missing, conflicting, or prose-only completion evidence yields `Blocked` or `NeedsRevalidation`, never `Completed`. |
| Auftreten und Konfidenz / Occurrences and confidence | `1` deterministischer Feldfund mit content-addressed Ergebnis; `High` |
| Permission- und Evidence-Risiko / Permission and evidence risk | Ein falsch positives `Completed` kann Folgephasen und spätere Delivery-Aktionen freischalten, obwohl eine neue Benutzerautorität erforderlich ist. Das ist zugleich Permission- und Evidence-Integritätsrisiko. / A false `Completed` can unlock dependent phases and later delivery actions although new user authority is required. This is both a permission and evidence-integrity risk. |
| Reproduzierbarer Test / Reproducible test | Einen Fixture-Runner Exitcode 0 zurückgeben lassen, aber ein strukturiertes Resultat mit `outcome=Blocked`, `taskCompleted=2`, `taskTotal=3` schreiben lassen. Erwartung: Wrapper speichert Hash und Resultat, setzt Phase und Lauf fail-closed auf `Blocked` oder `NeedsRevalidation` und startet keine abhängige Phase. Kontrollfall: Exitcode 0 plus `outcome=Completed` und 3/3 darf abschließen. / Use a fixture runner that exits zero but writes `outcome=Blocked`, 2/3 tasks. The wrapper must preserve the result and fail closed; only a matching completed 3/3 fixture may complete. |
| Entscheidung / Decision | `Promote` |

### AR-038-03 — Lebenszyklusgebundene Exact-Head-Evidence / Lifecycle-bound exact-head evidence

| Feld / Field | Bewertung / Assessment |
|---|---|
| Quelle und unveränderliche Evidence / Source and immutable evidence | Feature 038; `b59a3fe46e3868728be3557df7f367b8ab832db1:specs/038-example-portfolio-conformance-audit/autonomous-gate-requirements.json` (Git-Blob `1c5e0c2e7a058de36105bae3ef2ad90038b9454b`) enthält `GATE-038-10` für den geprüften Feature-Head vor dem Merge und `GATE-038-11` für Merge, Branchlöschung und Main-Sync danach. `b59a3fe46e3868728be3557df7f367b8ab832db1:.specify/presets/autonomous-run-governance/scripts/validate-autonomous-gate-evidence.sh` verlangt für jede deklarierte Gate-ID genau eine Primary-Zeile und für alle Zeilen denselben `reviewedHead`. Der tatsächliche Merge ist `b59a3fe46e3868728be3557df7f367b8ab832db1`, sein Feature-Eltern-Head ist `ca0cdf413187efd4710a6bf6436f1863c67bcdcd`. |
| Beobachtung und Fehlergrenze / Observation and failure boundary | Der vollständige Validator konnte vor dem Merge nicht wahrheitsgetreu bestehen, weil `GATE-038-11` noch nicht eingetreten war. Nach dem Merge existiert ein anderer Git-Head als der zuvor geprüfte Feature-Head. Ein einziges `reviewedHead`-Feld bildet diese kausale Zweiphasigkeit nicht verlustfrei ab. / The complete validator could not truthfully pass before merge because `GATE-038-11` had not happened. After merge, the Git head differs from the reviewed feature head. One `reviewedHead` field cannot losslessly represent both causal phases. |
| Artefaktart / Artifact kind | `evidence structure`, `script requirement` und `template` |
| Projektspezifische Ausschlüsse / Project-specific exclusions | Gate-IDs 038-10/11, GitHub-Commandtokens, PR-Nummern, Branchnamen und TuiVision-Mergepolitik werden nicht portiert. / Gate IDs, GitHub command tokens, PR numbers, branch names, and TuiVision merge policy are excluded. |
| Providerneutrale Zielregel / Provider-neutral target rule | Gate-Anforderungen erhalten eine explizite Lebenszyklusphase, etwa `PreMerge` oder `PostMerge`. Der Pre-Merge-Snapshot validiert nur zu diesem Zeitpunkt erfüllbare Gates gegen den geprüften Kandidaten-Head. Der Post-Merge-Snapshot bindet den Merge-/Default-Branch-Head kryptografisch an denselben Requirements-Hash und den früheren Kandidaten-Head und validiert nur Post-Merge-Gates. Kein Snapshot darf noch nicht eingetretene Fakten als `Pass` darstellen. / Gate requirements declare a lifecycle phase. Validate pre-merge gates against the reviewed candidate head and post-merge gates against a later snapshot that binds the merge/default-branch head to the same requirements hash and prior candidate head. No snapshot may mark future facts as passed. |
| Auftreten und Konfidenz / Occurrences and confidence | `1` deterministischer Feldfund plus `1` synthetische Validator-Reproduktion; `High` |
| Permission- und Evidence-Risiko / Permission and evidence risk | Ein erzwungener Gesamt-Pass vor Merge würde zukünftige Fakten erfinden; ein nur nach Merge erzeugter Ein-Head-Pass kann den tatsächlich geprüften Kandidaten verschleiern. Die Aufteilung erteilt keine Merge-Autorität, sondern macht vorhandene Autorität und zeitliche Evidence prüfbar. / A forced full pass before merge invents future facts; a single-head pass produced only after merge can obscure the actually reviewed candidate. Splitting evidence grants no merge authority; it makes existing authority and temporal evidence auditable. |
| Reproduzierbarer Test / Reproducible test | Temporäre Requirements mit je einem `PreMerge`- und `PostMerge`-Gate erstellen. Der bestehende Vollmengenvalidator lehnt den ehrlichen Pre-Merge-Snapshot wegen der fehlenden Post-Merge-Primary-Zeile ab. Der Zielvalidator muss den Pre-Merge-Snapshot allein akzeptieren, einen vorgezogenen Post-Merge-Pass ablehnen und später einen zweiten Snapshot nur akzeptieren, wenn Requirements-Hash, Kandidaten-Head und Merge-Abstammung gebunden sind. / Create temporary requirements with one gate per lifecycle phase. The current all-in-one validator rejects truthful pre-merge evidence because the post-merge primary row is absent. The target validator accepts the scoped first snapshot, rejects premature post-merge success, and later accepts the second snapshot only with bound requirements hash, candidate head, and merge ancestry. |
| Entscheidung / Decision | `Promote` |

### AR-038-04 — TuiVision-Intake-Alignment-Guard / TuiVision intake-alignment guard

| Feld / Field | Bewertung / Assessment |
|---|---|
| Quelle und unveränderliche Evidence / Source and immutable evidence | Feature 038 und Voraussetzung PR #145; `92efcf6f2db832b33026ef83077c3e6d361abd79^1:scripts/validate-requirements-intake-alignment.mjs` verlangte sieben aktive Intakes und ausschließlich Feature 037. `92efcf6f2db832b33026ef83077c3e6d361abd79:scripts/validate-requirements-intake-alignment.mjs` ersetzte dies durch TuiVision-eigene Receipt-, Review-, Series- und Feature-Bindung. PR #145 wurde als `92efcf6f2db832b33026ef83077c3e6d361abd79` gemergt. |
| Beobachtung und Fehlergrenze / Observation and failure boundary | Der veraltete Repository-Guard blockierte den autorisierten Feature-038-Zustand korrekt fail-closed, war aber fachlich an den vorherigen Projektzustand hart gebunden. Die Reparatur benötigte einen separaten projektspezifischen PR. / The stale repository guard failed closed on the authorized Feature 038 state but was semantically hardcoded to the prior project state. Repair required a separate project-specific PR. |
| Artefaktart / Artifact kind | `project-specific implementation` |
| Projektspezifische Ausschlüsse / Project-specific exclusions | Feature 037/038, sieben beziehungsweise acht aktive Intakes, 28 Archiv-Intakes, konkrete Lastenheftpfade, TuiVision-Series und PR #145. / All named feature IDs, counts, paths, series, and PR #145 are excluded. |
| Providerneutrale Zielregel / Provider-neutral target rule | Projektvalidatoren sollen aktuelle Autorität aus akzeptierten, gehashten Intake-/Review-/Series-/State-Artefakten ableiten und bei Widerspruch fail-closed stoppen. Diese allgemeine Regel ist bereits im Preset-Vertrag vorhanden; die konkrete TuiVision-Implementierung bleibt lokal. / Project validators should derive current authority from accepted hashed artifacts and fail closed on mismatch. The generic rule already exists in the preset contract; the TuiVision implementation remains local. |
| Auftreten und Konfidenz / Occurrences and confidence | `1` projektspezifischer Feldfund; `High` |
| Permission- und Evidence-Risiko / Permission and evidence risk | Die Korrektur außerhalb des autorisierten Feature-038-Diffs erforderte ausdrücklich neue, begrenzte Autorität. Eine Übernahme nach Home Baseline würde TuiVision-Semantik und Pfade unzulässig exportieren. / The out-of-scope correction required explicit bounded authority. Exporting it to Home Baseline would improperly carry project semantics and paths. |
| Reproduzierbarer Test / Reproducible test | Temporäre TuiVision-Fixtures mit sieben Series-Zielen plus einem separat authorisierten, ungeprüften `ReadyForReview`-Intake verwenden. Der alte Guard scheitert; der korrigierte lokale Guard akzeptiert nur den gültigen Fall und lehnt fehlende, veraltete oder fälschlich zur Serie gehörende Receipts ab. / Use temporary TuiVision fixtures with seven series targets plus one separately authored unreviewed intake. The old guard fails; the corrected local guard accepts only the valid state and rejects missing, stale, or falsely series-bound receipts. |
| Entscheidung / Decision | `RejectProjectSpecific` |

### AR-038-05 — DocFX-Verzeichnislinks / DocFX directory links

| Feld / Field | Bewertung / Assessment |
|---|---|
| Quelle und unveränderliche Evidence / Source and immutable evidence | Feature 038; `b59a3fe46e3868728be3557df7f367b8ab832db1:specs/038-example-portfolio-conformance-audit/pr-evidence.md`, Abschnitt „Fail-closed Stop bei T211“, bindet die zwei vorher vorhandenen `InvalidFileLink`-Warnungen und die separate Korrektur durch PR #143/Merge `3ff07383bcf081862a17e781b34de888f887ed8d`. |
| Beobachtung und Fehlergrenze / Observation and failure boundary | Zwei TuiVision-Links lagen außerhalb der Feature-038-Allowlist. DocFX meldete sie deterministisch; der autonome Lauf stoppte korrekt und nahm keine eigenmächtige Scope-Erweiterung vor. / Two TuiVision links were outside the Feature 038 allowlist. DocFX reported them deterministically; the run stopped correctly and did not expand scope without authority. |
| Artefaktart / Artifact kind | `project-specific implementation` |
| Projektspezifische Ausschlüsse / Project-specific exclusions | `docs/secure-development/README.md`, die beiden Verzeichnisziele, DocFX-Navigation und PR #143. / The file, directory targets, DocFX navigation, and PR #143 are excluded. |
| Providerneutrale Zielregel / Provider-neutral target rule | Warnungsfreie Pflichtgates und der Authority-Stopp funktionierten bereits wie vorgesehen; keine neue portable Regel ist nötig. / Warning-free mandatory gates and the authority stop already worked as intended; no new portable rule is needed. |
| Auftreten und Konfidenz / Occurrences and confidence | `1` projektspezifischer Feldfund; `High` |
| Permission- und Evidence-Risiko / Permission and evidence risk | Automatische Linkreparatur hätte die Write-Allowlist verletzt. Der separate autorisierte PR hielt Evidence und Berechtigungsgrenze korrekt. / Automatic repair would have violated the write allowlist. The separately authorized PR preserved evidence and permission boundaries. |
| Reproduzierbarer Test / Reproducible test | Den dokumentierten DocFX-Stand vor PR #143 bauen und genau zwei `InvalidFileLink`-Warnungen erwarten; danach den Merge-Stand bauen und null Warnungen erwarten. Das ist ein TuiVision-Regressionstest, kein Preset-Test. / Build the documented pre-PR state and expect two warnings, then the merged state and expect none. This is a TuiVision regression test, not a preset test. |
| Entscheidung / Decision | `RejectProjectSpecific` |

### AR-038-06 — Repository-Kardinalitäten / Repository cardinalities

| Feld / Field | Bewertung / Assessment |
|---|---|
| Quelle und unveränderliche Evidence / Source and immutable evidence | Feature 038; `b59a3fe46e3868728be3557df7f367b8ab832db1:specs/038-example-portfolio-conformance-audit/spec.md`, `tasks.md`, `pr-evidence.md` und `example-portfolio-audit.json` binden unter anderem 37 Portfoliozeilen, 46 kanonische Fixtures, 138 Source- und 128 Evidence-Knoten. PR #145 bindet sieben Series-Ziele und den achten separat authorisierten Intake. |
| Beobachtung und Fehlergrenze / Observation and failure boundary | Die exakten Mengen waren absichtlich akzeptierte Feature- und Repository-Verträge. Sie lieferten Drift-Nachweis, sind aber weder ein allgemeiner Preset-Fehler noch portable Standardwerte. / The exact counts were intentionally accepted feature and repository contracts. They provide drift evidence but are neither a generic preset defect nor portable defaults. |
| Artefaktart / Artifact kind | `project-specific implementation`, `checklist` und `evidence structure` |
| Projektspezifische Ausschlüsse / Project-specific exclusions | Sämtliche Zahlen, EPA-/EX-IDs, Pfadmengenhashes, Waves, Sourcefamilien und Ownergruppen von Feature 038. / All counts, IDs, path-set hashes, waves, source families, and owner groups are excluded. |
| Providerneutrale Zielregel / Provider-neutral target rule | Ein portables System darf projektspezifische Kardinalitäten nur aus akzeptierten Artefakten validieren und nicht als globale Preset-Konstanten übernehmen. Diese bestehende Ableitungsregel genügt. / A portable system may validate project cardinalities from accepted artifacts but must not turn them into global preset constants. The existing derivation rule is sufficient. |
| Auftreten und Konfidenz / Occurrences and confidence | `1` Feature mit mehreren gebundenen Mengen; `High` |
| Permission- und Evidence-Risiko / Permission and evidence risk | Exportierte Zahlen würden in anderen Repositories falsche Blocker oder falsche Passes erzeugen und könnten unberechtigte Artefaktänderungen provozieren. / Exported counts would create false blockers or false passes elsewhere and could prompt unauthorized artifact edits. |
| Reproduzierbarer Test / Reproducible test | Zwei temporäre Projekte mit unterschiedlichen, jeweils im lokalen Acceptance-Artefakt deklarierten Mengen validieren. Beide müssen mit demselben portablen Mechanismus bestehen; eine fest eingebaute TuiVision-Zahl muss scheitern. / Validate two temporary projects with different locally declared cardinalities. Both must pass through the same portable mechanism; any built-in TuiVision count must fail the portability test. |
| Entscheidung / Decision | `RejectProjectSpecific` |

## Portable PresetFollowUp-Übergabe / Portable PresetFollowUp Handoff

Diese Retrospektive empfiehlt ausschließlich folgende portable Folgearbeit; sie
führt sie nicht aus:

1. `AR-038-01`: Read-only Ermittlung der beabsichtigten Liefermenge und
   Whitespace-Prüfung aller versionierten und unversionierten Lieferpfade,
   einschließlich Bash-/PowerShell-Parität und sauberer Exitcode-Semantik.
2. `AR-038-02`: Versioniertes strukturiertes Phasenergebnis mit validierbarem
   Completion-Predicate; Exitcode 0 bleibt notwendig, ist aber nicht
   hinreichend.
3. `AR-038-03`: Lebenszyklusfeld und zwei kryptografisch gebundene
   Exact-Head-Evidence-Snapshots für Pre-Merge und Post-Merge.

*This retrospective recommends only the three portable follow-ups above. It
does not implement them. Any future preset change needs its own authority,
temporary-project tests, Bash/PowerShell parity, review, and publication
decision. This file is not authority to change or publish Home Baseline.*

## Validierung / Validation

- Die Skill- und installierten Command-/Template-Regeln wurden vollständig
  gelesen; Feature-Spezifikation, Plan, Tasks, Checklisten, Gate-Anforderungen,
  PR-Evidence, content-addressed Routing-Ausgabe und Merge-Historie wurden
  ausschließlich lesend ausgewertet.
- `HEAD`, lokales `main` und `origin/main` wurden als identischer Merge-Commit
  `b59a3fe46e3868728be3557df7f367b8ab832db1` bestätigt; dessen Eltern binden
  Guard-Merge `92efcf6f2db832b33026ef83077c3e6d361abd79` und Feature-Head
  `ca0cdf413187efd4710a6bf6436f1863c67bcdcd`.
- Der temporäre Whitespace-Test reproduzierte Exitcode 0 für gewöhnliches
  `git diff --check` und Exitcode 3 mit Diagnose für die pfadweise
  No-Index-Prüfung derselben unversionierten Datei.
- Der temporäre Gate-Test reproduzierte, dass die vollständige Requirements-
  Datei einen ehrlichen Pre-Merge-Snapshot wegen der fehlenden Post-Merge-
  Primary-Evidence ablehnt.
- Die vier Accepted-Artifact-Hashes und der Tasks-Hash stimmen bytegenau mit
  dem State überein; alle 225 Tasks und 144 Checklist-Punkte sind geschlossen.
- Die installierten Bash- und PowerShell-Statevalidatoren bestanden denselben
  unveränderten laufenden State mit `stage=Publish`, `status=Active` und
  225/225 Tasks.
- Die temporären Fixtures wurden anschließend entfernt. Produktverhalten,
  akzeptierte Artefakte, Run-State, Agent-Anleitung und Presets blieben
  unverändert.

*The evidence review was read-only. The merge ancestry and synchronized local
refs were verified. Temporary tests reproduced both the untracked-whitespace
gap and the all-in-one lifecycle-gate failure, and their fixtures were removed.
No product, accepted artifact, run-state, agent-guidance, or preset surface was
changed.*

## Abschluss / Outcome

- Promotete Regeln / Promoted rules: `AR-038-01`, `AR-038-02`, `AR-038-03`
- Offene Beobachtungen / Pending observations: `None`
- Abgelehnte Projektdetails / Rejected project details: `AR-038-04`,
  `AR-038-05`, `AR-038-06`
- Lokale nicht leere Korrektur / Local non-empty correction:
  `specs/038-example-portfolio-conformance-audit/retrospective.md`
- Geänderte Oberflächen / Changed surfaces: nur diese Retrospektive / this
  retrospective only
- Portable Übergabe / Portable handoff: die drei Empfehlungen im Abschnitt
  `Portable PresetFollowUp-Übergabe`; keine Home-Baseline-Änderung und keine
  Veröffentlichung / the three recommendations in the handoff section; no Home
  Baseline change and no publication
- Resume-State-Qualität / Resume-state quality: `NeedsImprovement`; der
  content-addressed Implementierungsstopp war rekonstruierbar, wurde vom
  Wrapper aber fälschlich als `Completed` markiert, und der gemergte State hält
  den tatsächlichen Delivery-Closeout nicht fest
- Nächstes Feld-Gate / Next field gate: drei providerneutrale temporäre
  Projekt-Fixtures müssen vor jeder künftigen Promotion gemeinsam bestehen:
  unversionierter Whitespace, semantisch geblockter Exitcode-0-Runner und
  gebundene Pre-/Post-Merge-Evidence; danach ist ein unabhängiger realer
  `MergeAndSync`-Lauf erforderlich

Es wird kein leerer Branch oder Pull Request erstellt, kein Folgefeature
gestartet und keine Veröffentlichung vorgenommen.

*No empty branch or pull request is created, no follow-up feature is started,
and nothing is published.*
