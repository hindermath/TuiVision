# Gemeinsame Testbasis / Paired validation basis

[Umgesetzte Erweiterungen und Grenzen / Implemented hardening and limits](validation-hardening.md)

Documentation impact: `UpdateRequired`. Owner: model-routing and autonomous-run
preset maintainers. Audience: operators/reviewers. Canonical source: routing
preset `tests/live/`; consumer integration: autonomous-run preset. Distribution:
preset source only, no Home sync. Raw reports remain local and ignored.

## Aufruf / Invocation

DE: Fuer Routing- und Ausfuehrungsaenderungen ist die gekoppelte Pruefung der
Standard: zuerst Smoke, danach der normale autonome Phasenrunner. Beide Stufen
laufen auch nach einem Fehler in der ersten. Der Gesamtstatus ist nur bestanden,
wenn beide bestehen; Retries bleiben als `PASS_WITH_RETRY` sichtbar.

EN: Paired validation is the default for routing/execution changes: smoke first,
then the production autonomous phase runner, including after a first-stage
failure. Acceptance requires both stages to pass; retries remain visible.

Run from routing source with PowerShell 7 and the actual clean consumer path:

```powershell
pwsh -NoProfile -File tests/live/invoke-model-routing-validation.ps1 -ConfirmLiveRun -MaxRetries 3 -AutonomousRunnerSource ../spec-kit-preset-autonomous-run-governance
```

DE: Drei Retries bedeuten einen Erstversuch plus drei weitere Versuche je
Harness und Stufe, keine Wiederholungen des gesamten Testpaares. Erfolgreiche
Harnesses werden innerhalb derselben Stufe nicht wiederholt. Modell und
Provider bleiben gleich. Standard: null Retries. Echte Modellaufrufe bleiben
manuell, ausserhalb der normalen CI, mit vorhandenen interaktiven Anmeldungen.
Einzelaufrufe bleiben fuer Diagnose verfuegbar. Keine Installation oder Rollout.

EN: Three retries mean an initial attempt plus three further attempts per
harness and stage, not reruns of the whole pair. Successful harnesses are not
repeated within a stage. Model/provider stay unchanged. Default is zero retries.
Live calls remain manual outside ordinary CI, using existing interactive
sign-ins. Individual diagnostic commands remain available. No install/rollout.

## Abnahme und Evidence / Acceptance and evidence

DE: Beide Stufen verwenden die versionierte Modellmatrix und externe
.NET-10-Abnahme. Die normale Integration prueft auch `COMPLETED`, Ergebnis-
vertrag und Payload-Hash. Discovery, Preflight und Aufgabe teilen 120 Sekunden
je Harness; externe Build-/Run-Pruefungen haben eigene begrenzte Zeiten.
Jede Stufe verwendet eigene temporaere SDKs, Profile und Git-Repositories.

EN: Both stages use the versioned model matrix and external .NET 10 acceptance.
Production integration also checks `COMPLETED`, result contract and payload
hash. Discovery/preflight/task share 120 seconds per harness; external build/run
have separate bounded times. Stages have isolated temporary SDKs/profiles/repos.

DE: `.test-results/model-routing-smoke/validation-pair-*.json` bindet beide
Quellcommits, Einzelberichte mit SHA-256, Harnessstatus und Versuchszahlen.
Einzelberichte enthalten Laufzeiten, Exitcodes, Cleanup und begrenzte redigierte
Diagnosen. Der konsumierende Quellstand muss sauber und unveraendert bleiben.
Historische Berichte werden nicht ersetzt. Versionierte Evidence wird bewusst
aktualisiert; technischer Erfolg erteilt keine Release-/Rollout-Freigabe.

EN: The ignored combined report binds both commits, individual report hashes,
harness status and attempts. Individual reports contain timings, exits, cleanup
and bounded redacted diagnostics. Consumer source must stay clean and unchanged.
History is preserved. Technical acceptance grants no release/rollout authority.

## Wiederholungspflicht / Repeat triggers

DE: Beide Stufen nach Aenderungen an Resolver, Discovery, Auswahlpolicy,
Profilrendering, Ergebnis-/Argumenttransport, Ausfuehrungspolicy, Rollenrechten,
SDK-/CLI-Kompatibilitaet, Modellmatrix oder normalem Phasenrunner wiederholen.
Reine Dokumentation und historische Evidence loesen keinen Live-Lauf aus.
Offline-Vertraege laufen vorher auf der CI-Plattformmatrix. Bei Testdefekten
zuerst einen modellfreien Reproduzierer verwenden.

EN: Repeat both stages after changes to resolver/discovery, selection policy,
profile rendering, result/argument transport, execution policy/permissions,
SDK/CLI compatibility, model matrix or production runner. Docs/history alone
do not trigger live calls. Run offline contracts first on the CI matrix;
reproduce test defects without models before spending further quota.

## Verbesserungsvorschlaege / Recommended follow-ups

DE: Noch nicht umgesetzt: fehlgeschlagene Discovery-Antworten redigiert sichern
und fehlendes `reason` robust behandeln; testeigene Compiler-Daemons automatisch
und besitzgeprueft aufraeumen; lesende Rollen separat live mit negativem
Schreibversuch pruefen; native Windows-/Ubuntu-/WSL2-Live-Nachweise sammeln.
macOS-Evidence ersetzt diese Plattformen nicht. Keine neuen Modell-Fallbacks.
Re-Evaluation bei der naechsten Routingaenderung.

EN: Not yet implemented: retain redacted failed discovery responses and handle
missing `reason`; automatically clean up compiler daemons after proving test
ownership; separately live-test read-only roles with negative writes; gather
native Windows/Ubuntu/WSL2 evidence. macOS evidence does not cover those
platforms. No new model fallbacks. Reevaluate at the next routing change.

Details: [Smoke DE](live-smoke-test.md), [Smoke EN](live-smoke-test.EN.md),
[Production execution](execution-integration.md).

## Erster gekoppelter Lauf / First paired run

DE: 2026-11-04, Model-Routing `fb7e59f48ffd0debba36962c8becbd71fa3d3014`,
Autonomous-Runner `3410e621c04a5cb63b91ef900ff8cb9125354590`, maximal drei
Retries je Harness und Stufe. Smoke: alle fuenf `PASS`, Versuch 1. Normaler
Runner: agy/Codex/Copilot `PASS`, Versuch 1; Claude `PASS_WITH_RETRY`, Versuch 2;
OpenCode `FAILED` nach vier Versuchen mit semantisch abgelehntem `Blocked`-
Ergebnis. Es wurde dort kein C#-Projekt angelegt. Der konkrete `blockedReason`
ist im begrenzten Bericht nicht gespeichert; fehlende Feature-Artefakte sind
daher eine Hypothese, kein nachgewiesener Befund. Gesamtstatus `FAILED`.

EN: On 2026-11-04, source commits above were checked with three retries allowed
per harness/stage. Smoke: all five passed on attempt one. Production: agy,
Codex and Copilot passed on attempt one, Claude passed on attempt two, and
OpenCode failed all four attempts with a rejected `Blocked` phase result and
no C# project. Its detailed blocked reason was not retained; missing feature
artifacts are a hypothesis, not a proven cause. Overall `FAILED`.

DE: Beide Einzelberichte melden erfolgreichen Cleanup und unveraendertes HEAD,
Git-Status und permanentes Profil. Der konsumierende Quellstand blieb ebenfalls
unveraendert. Ein uebriger Test-Compiler wurde anschliessend beendet. Der
Gesamtbericht bleibt unveraendert lokal, SHA-256:
`4560c7b6106e7e9a27e55933fc1dcab2ba99f912979b6c8343c1e9a25bdee627`.
Sein natives `exitCode`-Feld fuer die zweite Stufe wurde faelschlich als 0
erfasst, weil eine PowerShell-Closure den automatischen Wert verschattete;
Einzelbericht, Gesamtstatus `FAILED` und finaler Prozessexit 1 waren korrekt.
Die Erfassung wurde danach mit einem modellfreien Exitcode-7-Test korrigiert.
Keine nachtraegliche Aenderung der historischen JSON-Evidence, kein neuer
Live-Lauf ueber die freigegebene Retry-Grenze hinaus.

EN: Both stage reports passed cleanup and HEAD/status/permanent-profile checks;
consumer source was unchanged. One remaining compiler was stopped afterwards.
The local combined report is preserved with the hash above. Its native second-
stage exit-code field incorrectly read 0 due to PowerShell closure shadowing;
the individual report, overall `FAILED` and final process exit 1 were correct.
Exit capture was then fixed and tested offline with a native exit code 7.
Historical JSON was not rewritten and no extra live run exceeded the retry cap.

DE: Weitere priorisierte Verbesserung: Bei fehlgeschlagenen Phasen auch
redigierte `outcome`/`blockedReason` sichern. Fuer die normale Integration einen
vollstaendigen minimalen akzeptierten Feature-Satz vorbereiten, statt Agenten
nur im Prompt zu erklaeren, dass es sich um einen Integrationstest handelt.
Damit lassen sich echte Ausfuehrungsfehler besser von Artefakt-Gates trennen.

EN: Next priorities: retain redacted phase `outcome`/`blockedReason` on failures,
and prepare a complete minimal accepted feature fixture instead of relying only
on prompt instructions. This separates execution problems from artifact gates.
