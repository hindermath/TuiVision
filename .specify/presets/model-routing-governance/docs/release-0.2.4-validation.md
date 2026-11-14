# v0.2.4: Discovery-Kompatibilität / Discovery compatibility

DE: Dieser lokale, unveröffentlichte Kandidat ergänzt aktuelle `agy`-Namen,
normalisiert begrenzte Claude-CLI-Hilfe, bindet bei Copilot den tatsächlich
erkannten Reasoning-Schalter und liest die OpenCode-2.2.5-API vor dem
Legacy-Fallback. Claude, Copilot und OpenCode validieren und verwenden außerdem
eine zuvor ausdrücklich gespeicherte Modellauswahl erneut. Codex-Verhalten und
historische `agy`-Auflösungen bleiben unverändert. Mehrdeutige, veraltete oder
unbekannte Ergebnisse blockieren weiterhin.

EN: This local, unpublished candidate adds current `agy` names, normalizes
bounded Claude CLI help, binds Copilot's actually detected reasoning flag and
reads the OpenCode 2.2.5 API before the legacy fallback. Claude, Copilot and
OpenCode also validate and reuse a previously explicit stored model selection.
Codex behavior and historical `agy` resolution remain unchanged. Ambiguous,
stale or unknown results still fail closed.

Documentation Impact **UpdateRequired**, owner Thorsten Hindermann. Canonical
sources are the resolver, additive adapters, transport tests, paired discovery
guides and this record. Reader path: README → paired user guide → paired
automatic-discovery guide → this validation record. Audience: preset operators
and maintainers. German first with equivalent English; CEFR B2 and applicable
WCAG 2.2 AA. Distribution class: local source candidate. Home sync is not
required or authorized. Re-evaluate on SDK, CLI or OpenCode API drift.

## Lokale Prüfung / Local verification

DE: Die deterministische Suite bestand am 2026-11-04 unter macOS: Bash- und
PowerShell-Discovery, 193 additive Adapter-/Legacy-Paritätsprüfungen, 184
Auswahlprüfungen, 41 plattformneutrale Smoke-Vertragsprüfungen,
Ergebnistransport, sieben Node-/SDK-/HTTP-Tests,
Dokumentations- und Release-Verträge, PSScriptAnalyzer 1.25.0, `git diff
--check` und Gitleaks. Die OpenCode-Fixture prüft beide Reasoning-Formen:
`--variant` und `provider/model#variant`.

EN: The deterministic suite passed on macOS on 2026-11-04: Bash and PowerShell
discovery, 193 additive adapter/legacy parity checks, 184 selection checks, 41
platform-neutral smoke-contract checks, result transport, seven Node/SDK/HTTP
tests, documentation and release contracts, PSScriptAnalyzer 1.25.0, `git diff
--check`, and Gitleaks. The
OpenCode fixture covers both reasoning forms: `--variant` and
`provider/model#variant`.

DE: Der ausdrücklich angeforderte Folgelauf verwendete ausschließlich die
vorhandenen interaktiven CLI-Anmeldungen. API-Key-Umgebungsvariablen wurden für
die Kindprozesse entfernt. Gepinnte SDKs dienten nur der lokalen
Katalogabfrage. Während des Laufs wurden reale CLI-Transportgrenzen korrigiert:
Claude-MCP-Argumentende, Copilot-JSONL, JSON-kodierte Harnessargumente,
OpenCode-Varianten und geerbtes `PWD`. Die folgende Tabelle enthält den jeweils
abschließenden grünen Lauf; jeder blieb einschließlich Discovery, Preflight und
Aufgabe unter 120 Sekunden.

EN: The explicitly requested follow-up used only existing interactive CLI
sign-ins. API-key environment variables were removed from child processes.
Pinned SDKs served only local catalogue discovery. Real CLI transport limits
were corrected during the run: Claude MCP argument termination, Copilot JSONL,
JSON-encoded harness arguments, OpenCode variants, and inherited `PWD`. The
table records each final green run; discovery, preflight and task stayed below
120 seconds per harness.

| Harness | Bindung | Live-Ergebnis |
|---|---|---|
| `agy` | automatisch `claude-opus-5-5-high`, `high` | `PASS`: Discovery 2,736 s, Preflight 4,514 s, Aufgabe 53,399 s; Agent-Exitcode 0, externe Abnahme grün. Die `command(*)`-Freigabe galt nur sitzungsgebunden im temporären Sandboxlauf. |
| Claude | automatisch `opus`, `high` | `PASS`: Discovery 3,591 s, Preflight 4,757 s, Aufgabe 93,792 s; Agent-Exitcode 0, externe Abnahme grün. Authentifizierung: `claude.ai`. |
| Codex | automatisch `gpt-6.1-sol`, `high` | `PASS`: Discovery 4,283 s, Preflight 5,978 s, Aufgabe 48,320 s; Agent-Exitcode 0, externe Abnahme grün. |
| Copilot | exakter Test-Override `claude-opus-5.5`, `high`; Fast `claude-haiku-5.5` | `PASS`: Discovery 4,368 s, Preflight 6,957 s, Aufgabe 45,364 s; Agent-Exitcode 0, externe Abnahme grün. Die SDK-Katalogabfrage nutzte die interaktive Copilot-CLI-Anmeldung. Der Override löst die absichtlich fail-closed Mehrdeutigkeit von vier Opus-Versionen nur für diesen Test. |
| OpenCode | exakter Test-Override `openai/gpt-6.1-terra`, `high`; Fast `openai/gpt-6-luna` | `PASS`: Discovery 1,357 s, Preflight 3,550 s, Aufgabe 101,295 s; Agent-Exitcode 0, externe Abnahme grün. OpenCode 2.2.5 nutzte `provider/model#variant` und die interaktive Provideranmeldung. |

All five final runs contain exactly one C# project, no `PackageReference`, a
successful `dotnet build --no-restore`, exit code 0 and normalized output
exactly `MODEL_ROUTING_OK`. The five-harness functional live proof is therefore
green. Copilot correctly stays fail-closed on a new profile until its four
equal-rank Opus models are resolved by explicit selection or a separately
approved deterministic policy. This evidence does not authorize delivery.

## Gespeicherte Auswahl / Stored selection

DE: Der ergänzende Live-Nachweis erzeugte am 2026-11-04 je ein temporäres
Profil mit den bekannten exakten IDs und wiederholte anschließend reale
Discovery ohne Modellparameter. Claude meldete `opus`/`haiku`, Copilot
`claude-opus-5.5`/`claude-haiku-5.5` und OpenCode
`openai/gpt-6.1-terra`/`openai/gpt-6-luna`; alle drei Folgestatusläufe waren
`Aligned`. Copilot und OpenCode behielten vor und nach Status denselben
SHA-256-Profilhash. Der Copilot-C#-Lauf verwendete danach das gespeicherte
Implementierungsprofil `claude-opus-5.5`, `high` über die vorhandene
interaktive Anmeldung. Preflight: 8,725 s; Aufgabe: 42,025 s; Agent-Exitcode 0.
Die unabhängige Abnahme fand genau ein Projekt, keine `PackageReference`, einen
erfolgreichen Build und Lauf sowie exakt `MODEL_ROUTING_OK`. Es wurden keine
API-Key-Umgebungsvariablen an den Kindprozess weitergegeben.

EN: The additional live proof created one temporary profile per harness with
the known exact IDs on 2026-11-04, then repeated real discovery without model
parameters. Claude reported `opus`/`haiku`, Copilot
`claude-opus-5.5`/`claude-haiku-5.5`, and OpenCode
`openai/gpt-6.1-terra`/`openai/gpt-6-luna`; all three follow-up status calls
were `Aligned`. Copilot and OpenCode retained the same SHA-256 profile hash
before and after status. The Copilot C# run then used the stored implementation
profile `claude-opus-5.5`, `high` through the existing interactive sign-in.
Preflight: 8.725 s; task: 42.025 s; agent exit code 0. Independent acceptance
found exactly one project, no `PackageReference`, a successful build and run,
and exactly `MODEL_ROUTING_OK`. No API-key environment variables were passed to
the child process.

Preparation is not publication evidence. No tag, release page, archive,
installation, Home sync or rollout is created by this candidate.

DE: Kuenftige Routing-, Adapter-, Profilrendering- oder Transportaenderungen
verwenden den [plattformneutralen manuellen Live-Smoke-Test](live-smoke-test.md).
Seine versionierten Defaults definieren die wiederholbare Akzeptanzmatrix;
lokale JSON-Berichte bleiben unversioniert, bis ein separates Evidence-Update
ausdruecklich autorisiert ist.

EN: Future routing, adapter, profile-rendering or transport changes use the
[platform-neutral manual live smoke test](live-smoke-test.EN.md). Its versioned
defaults define the repeatable acceptance matrix; local JSON reports remain
unversioned until a separate evidence update is explicitly authorized.

## Diagnostischer Lauf / Diagnostic run

DE: Der genau einmal autorisierte Diagnoselauf mit `-MaxRetries 0` wurde am
2026-11-04 auf Commit `b45664cb860a8c442041f524a6c23d1d2ee73d36`
ausgefuehrt. Er endete als `INFRA_ERROR`, weil die Projektinventur eine noch
geoeffnete `.dotnet`-Lockdatei des OpenCode-Versuchs hashen wollte. Der spaete
Fehler zeigte zusaetzlich, dass der Abschlussbericht bereits gesammelte
Harness-Ergebnisse nicht behalten hatte. Der Runner schliesst deshalb
`.dotnet` als SDK-Cache aus, behandelt nicht lesbare Hashes fehlertolerant,
uebernimmt abgeschlossene Harness-Berichte fortlaufend und redigiert den
temporaeren Pfad auch bei globalen Fehlern. Diese Nachbesserung ist durch 41
Offline-Vertragspruefungen abgedeckt. Gemaess der Einmal-Grenze wurde der
Live-Lauf nicht wiederholt; der lokale Fehlerbericht blieb unveraendert.

EN: The single authorised diagnostic run with `-MaxRetries 0` ran on
2026-11-04 at commit `b45664cb860a8c442041f524a6c23d1d2ee73d36`. It ended
as `INFRA_ERROR` because project inventory attempted to hash an open `.dotnet`
lock file from the OpenCode attempt. The late failure also showed that the
final report had not retained already collected harness results. The runner
therefore excludes `.dotnet` as SDK cache, tolerates unreadable hashes,
incrementally retains completed harness reports, and redacts the temporary
path for global failures. Forty-one offline contract assertions cover this
hardening. The live run was not repeated under the single-run boundary, and
the local failure report remains unchanged.

[Strukturierte Vorbereitung / Structured preparation](release-0.2.4-preparation.json).

## Timeout- und Berichtskorrektur / Timeout and report correction

DE: Der lokale Lauf auf `3345063` verwendete versionierte Defaults und
maximal drei Retries je Harness. Codex bestand im ersten Versuch, Claude und
Copilot im dritten. `agy` erzeugte wegen verweigertem `write_file` kein
Projekt. OpenCode scheiterte viermal bei der externen Ausfuehrung mit Exitcode
131 und fehlender Runtimeconfig. Der Bericht enthaelt alle 15 Versuche und
bestaetigt vollstaendigen Cleanup sowie unveraendertes HEAD, Git-Status und
permanentes Profil. Bei drei OpenCode-Diagnosen war ein manueller Eingriff
zum Schliessen lokaler .NET-Diagnose-Pipes notwendig. Dieser Lauf ist daher
kein Nachweis fuer einen unbeaufsichtigten Runner. Anschliessend wurde die
Inventur auf regulaere Dateien anhand von `UnixStat.ItemType` begrenzt;
ein Offline-Test mit echter FIFO prueft den Schutz gegen diesen Haenger.
Prozessausgabe und Prozessende teilen sich jetzt eine Deadline; abgeschlossene
Versuche werden sofort atomar auf Platte gesichert. Keine Routingpolicy wurde
geaendert. Die abschliessende FIFO-Korrektur wurde offline, nicht in einem
weiteren Providerlauf geprueft.

EN: The local run at `3345063` used versioned defaults and at most three
retries per harness. Codex passed on attempt one; Claude and Copilot passed on
attempt three. `agy` produced no project because `write_file` was denied.
OpenCode failed external execution four times with exit code 131 and a missing
runtimeconfig. The report retains all 15 attempts and confirms cleanup and
unchanged HEAD, Git status and permanent profile. Three OpenCode diagnostics
required manual intervention to close local .NET diagnostic pipes, so this
run does not prove unattended operation. Inventory was subsequently restricted
to regular files using `UnixStat.ItemType`; an offline test with a real FIFO
covers this hang. Output draining and process exit now share a deadline;
completed attempts are immediately checkpointed atomically. Routing policy
was unchanged. The final FIFO correction was checked offline, not in another
provider run.

## Erfolgreicher Abschlusslauf / Successful final run

DE: Der abschliessende Live-Test am 2026-11-04 pruefte Commit
`0699fa50aed88e316cebe37ec39a5f2de90195af` mit versionierten Defaults,
interaktiven Anmeldungen und `-MaxRetries 3`. Alle fuenf Harnesses bestanden
im ersten Versuch; kein Retry und kein manueller Eingriff waren erforderlich.
Die physischen Temp-Pfade verhindern den reproduzierten macOS-Pfadwechsel
zwischen `/var` und `/private/var`. Die sitzungsgebundene agy-Dateifreigabe
gilt nur fuer den temporaeren Worktree. Die Routingpolicy bleibt unveraendert.

EN: The final live test on 2026-11-04 checked commit
`0699fa50aed88e316cebe37ec39a5f2de90195af` with versioned defaults,
interactive sign-ins and `-MaxRetries 3`. All five harnesses passed on their
first attempt, without retries or manual intervention. Physical temporary
paths prevent the reproduced macOS switch between `/var` and `/private/var`.
The session-only agy file permission covers only the temporary worktree.
Routing policy remains unchanged.

| Harness | Modell / Model | Reasoning | Discovery (s) | Preflight (s) | Aufgabe / Task (s) | Ergebnis / Result |
|---|---|---|---:|---:|---:|---|
| agy | `claude-opus-5-5-high` | high | 3.715 | 5.692 | 36.269 | PASS |
| Claude | `opus` | high | 17.355 | 6.740 | 46.382 | PASS |
| Codex | `gpt-6.1-sol` | high | 5.665 | 12.807 | 82.084 | PASS |
| Copilot | `claude-opus-5.5` | high | 33.657 | 13.540 | 36.327 | PASS |
| OpenCode | `openai/gpt-6.1-terra` | high | 5.117 | 6.008 | 53.112 | PASS |

DE: Agent, externer Build und externer Lauf lieferten jeweils Exitcode 0;
die Ausgabe war exakt `MODEL_ROUTING_OK`. Discovery, Preflight und Aufgabe
blieben je Harness unter 120 Sekunden. Claude, Copilot und OpenCode behielten
bei parameterlosem Status/Refresh bytegleiche Profile. Alle fuenf
Cleanup-/Unveraendertheitspruefungen waren erfolgreich; verbliebene
Test-Compiler-Daemons wurden anschliessend beendet. 46 Offline-Smoke-Vertraege,
der modellfreie .NET-Build/Rebuild/Run-Regressionstest, 73 Dokumentations- und
64 Release-Pruefungen sowie PSScriptAnalyzer bestanden. Gitleaks fand keine
Leaks. Der vollstaendige Bericht bleibt lokal und ist ueber SHA-256 gebunden:
`c11c7d4e16678514131b71a9d0299b9202e162fa9c05677b9b262d7280ddc153`.
Dies ist Test-Evidence, keine Release- oder Rollout-Freigabe.

EN: Agent, external build and external run each returned exit code 0, with
output exactly `MODEL_ROUTING_OK`. Discovery, preflight and task stayed under
120 seconds per harness. Claude, Copilot and OpenCode retained byte-identical
profiles after parameterless status/refresh. All five cleanup/invariance
checks passed; remaining test compiler daemons were stopped afterwards.
The 46 offline smoke contracts, model-free .NET build/rebuild/run regression,
73 documentation and 64 release checks, and PSScriptAnalyzer passed. Gitleaks
found no leaks. The full report remains local, bound by the SHA-256 above.
This is test evidence, not release or rollout approval.

## Gekoppelte Fast-Profil-Abnahme / Paired fast-profile acceptance

DE: Am 2026-11-05 pruefte der gekoppelte Runner Commit
`0a092d8f3f892f9dc1c6161c1b88c09dfc2da820` mit versionierten Defaults und
`-MaxRetries 3`. Die Read-only-Probe 2.1 bestand fuer alle fuenf Harnesses im
ersten Versuch: agy `gemini-3.8-flash-medium`, Claude `haiku`, Codex
`gpt-6-luna`, Copilot `claude-haiku-5.5` und OpenCode
`openai/gpt-6-luna`, jeweils mit Reasoning `medium`. Extern geprueft wurden
erfolgreiches Lesen, das Fehlen von `forbidden.txt`, die effektiven
Werkzeuggrenzen und vollstaendiger Cleanup. Damit sind die lesenden
Fast-Profile abgenommen.

EN: On 2026-11-05 the paired runner checked commit
`0a092d8f3f892f9dc1c6161c1b88c09dfc2da820` with versioned defaults and
`-MaxRetries 3`. Read-only probe 2.1 passed all five harnesses on attempt one:
agy `gemini-3.8-flash-medium`, Claude `haiku`, Codex `gpt-6-luna`, Copilot
`claude-haiku-5.5`, and OpenCode `openai/gpt-6-luna`, all at `medium`
reasoning. Independent checks covered successful reading, absence of
`forbidden.txt`, effective tool boundaries, and complete cleanup. This accepts
the read-only fast profiles.

DE: Die C#-Smoke- und normale Preset-Stufe bestanden fuer Claude, Codex,
Copilot und OpenCode. Codex benoetigte im normalen Pfad drei Retries. agys
Frontier-Modell war in beiden Stufen vor der Aufgabe extern mit HTTP 429
`RESOURCE_EXHAUSTED` blockiert; der Anbieter meldete ein individuelles
Kontingentlimit. Deshalb bleibt die gekoppelte Gesamtwertung `FAILED`, obwohl
die hier geaenderten Fast-Profile vollstaendig bestanden. Repository-HEAD,
Status, permanentes Profil, temporaere Artefakte und Consumer-Quelle blieben
unveraendert. Die lokalen Berichte sind mit SHA-256 im gekoppelten Bericht
gebunden. Keine Release- oder Rollout-Freigabe wird daraus abgeleitet.

EN: The C# smoke and normal preset stages passed for Claude, Codex, Copilot,
and OpenCode. Codex required three retries in the normal path. agy's frontier
model was externally blocked before the task in both stages with HTTP 429
`RESOURCE_EXHAUSTED`; the provider reported an individual quota limit. The
paired overall result therefore remains `FAILED`, although the changed fast
profiles passed completely. Repository HEAD/status, permanent profile,
temporary artifacts, and consumer source remained unchanged. The paired
report binds the local reports by SHA-256. This grants no release or rollout
authority.
