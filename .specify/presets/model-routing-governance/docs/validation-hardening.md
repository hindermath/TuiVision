# Erweiterte Testbasis / Extended validation

Documentation impact: `UpdateRequired`. Owner: routing/runner maintainers.
Audience: operators and reviewers. Canonical source: tests/live and versioned
tests/fixtures/integration-feature. Distribution: preset source, no Home sync.

## Diagnose / Diagnostics

DE: Fehlgeschlagene Discovery-, Seed-, Status- und Refresh-Prozessantworten
werden vor dem Cleanup separat gespeichert. Ein fehlendes `reason` maskiert
den Fehler nicht mehr. Timeout-Ausnahmen behalten Prozessmetadaten und bereits
verfuegbare Ausgabe; noch offene Ausgabekanaele werden sichtbar als nicht
verfuegbar markiert. Phasenergebnisse erlauben nur `outcome`, `blockedReason`,
`phaseId` und bekannte boolesche Felder. Text wird redigiert und begrenzt;
keine vollstaendigen Gespraechsverlaeufe oder Umgebungsvariablen.

EN: Failed discovery, seed, status and refresh process responses are retained
before cleanup. Missing `reason` no longer masks the failure. Timeout exceptions
retain process metadata and completed output; still-open streams are explicitly
marked unavailable. Phase diagnostics allow only outcome, blockedReason, phaseId
and known booleans. Text is redacted and bounded, not full transcripts or env.

## Feature-Fixture

DE: Die normale Integration erhaelt einen vollstaendigen synthetischen Feature-
Satz: Spec, Plan, Aufgabe T001, Research, Datenmodell, Quickstart, Checkliste,
Constitution, AGENTS und einen normalen Feature-Zustand mit Artefakt-Hashes.
Eine temporaere Feature-Branch samt lokalem Baseline-Commit macht die Annahmen
sichtbar. Fixture-lokale Bash/PowerShell-Pruefer pruefen die vorhandenen Dateien;
sie ersetzen keine produktiven Spec-Kit-Gates. Erwartet werden genau ein
paketfreies C#-Projekt, echte Ausfuehrung und der normale Ergebnisvertrag.

EN: Production integration receives a complete synthetic feature: spec, plan,
T001, research, data model, quickstart, readiness checklist, constitution, AGENTS
and normal feature state with artifact hashes. A temporary feature branch and
local baseline commit expose the assumptions. Fixture-local Bash/PowerShell
checkers validate actual files, not unrelated production gates. One package-free
C# project, execution and the normal result contract remain required.

## Compiler-Cleanup

DE: Kindprozesse deaktivieren unnoetige dauerhafte Build-Server. Zusaetzlich
beobachtet der Runner die Prozessabstammung waehrend der Ausfuehrung. Er beendet
nur erfasste VBCSCompiler mit erneut passender PID, Startzeit, Programm- und
Compilerpfad. Linux-dotnet-Hosts werden ueber ihren DLL-Argumentpfad erkannt.
Globale PID-Differenzen oder pauschales `dotnet`-Beenden sind ausgeschlossen.
Nicht beweisbarer Besitz berechtigt nicht zum Beenden. Cleanup-Zahlen werden
berichtet; Identitaetskonflikte oder fehlgeschlagene Stops blockieren Abnahme.

EN: Child processes disable unnecessary persistent build servers. The runner
also observes ancestry while invocations are alive and stops only recorded
VBCSCompiler instances after rechecking PID/start time/executable/compiler path.
Linux dotnet hosts are recognised by their compiler DLL argument. No global PID
diff or blanket dotnet kill is used. Unproven ownership never grants termination.
Counts are reported; identity conflicts or failed stops prevent acceptance.

## Lesende Rollen / Read-only roles

```powershell
pwsh -NoProfile -File tests/live/invoke-model-routing-validation.ps1 -ConfirmLiveRun -MaxRetries 3 -IncludeReadOnlyProbe -AutonomousRunnerSource ../spec-kit-preset-autonomous-run-governance
```

DE: Nach Smoke und normaler Integration folgt eine dritte Stufe mit dem echten
Fast-Profil jedes Harnesses. Der Agent soll eine vorbereitete Datei lesen und
einen absichtlich unzulaessigen Schreibwerkzeug-Aufruf versuchen, ohne Shell-
Umgehung. Erfolg verlangt Lese-/Verweigerungsbestaetigung und extern geprueftes
Fehlen von forbidden.txt. Diese Grenze wird im Bericht benannt: Die Abwesenheit
ist extern bewiesen, der konkrete Werkzeugversuch beruht auf Agentenaussage;
das ist kein Nachweis einer Betriebssystem-Sandbox. Fehlende oder abweichende
Antworten sind echte Fehlversuche. Exakte Modellauswahl bleibt unveraendert.

EN: An optional third stage uses each harness's actual fast profile to read a
prepared file and attempt one intentionally denied file-tool write, with no
shell bypass. Success requires read/denial attestation and externally verified
absence of forbidden.txt. File absence is external evidence; the attempted
tool denial is agent-attested, not an OS sandbox proof. Missing/mismatched
results fail honestly. No model/provider fallback is introduced.

## Plattformen / Platforms

DE: Offline-Tests und modellfreie .NET-Build/Run-Regressionen laufen auf der
GitHub-Matrix macOS, Ubuntu und Windows mit gepinntem setup-dotnet und SDK 10.
Native Provider-Live-Tests verlangen separate interaktive Anmeldungen auf der
jeweiligen Plattform. Der aktuelle Arbeitsplatz ist macOS; Windows/WSL sind
hier nicht verfuegbar. Eine Podman-VM ersetzt keinen nativen Windows-/WSL-Lauf;
Host-Anmeldedaten werden nicht in Container kopiert. Native Live-Nachweise
bleiben dort offen, bis passende angemeldete Umgebungen verfuegbar sind.

EN: Offline contracts and model-free .NET build/run run on the GitHub macOS,
Ubuntu and Windows matrix with pinned setup-dotnet/SDK 10. Native provider live
tests require interactive sign-ins on each platform. This host is macOS, not
Windows/WSL. A Podman VM is not native Windows/WSL evidence; host credentials
are not copied into containers. Those native live results remain open until
authenticated environments are available.

Offline commands: tests/test-validation-hardening.ps1,
tests/test-live-smoke-dotnet.ps1 -VerifyCompilerCleanup,
tests/test-execution-policy.ps1 and tests/test-paired-validation.ps1.
Reevaluate after CLI, permission, schema or fixture changes.

## Ergebnis am 2026-11-04 / Results on 2026-11-04

DE: Erweitert gepruefter Code: `15cc55c9d32e4e4c832d11a21f2fbe5403e6628a`,
normaler Runner `3410e621c04a5cb63b91ef900ff8cb9125354590`; maximal drei
Retries je Harness/Stufe. Smoke und normale Integration bestanden bei allen
fuenf Harnesses jeweils im ersten Versuch. Die vollstaendige Feature-Fixture
bestand damit insbesondere auch mit OpenCode, das vorher `Blocked` meldete.

EN: Tested code commits are listed above; at most three retries per stage and
harness were allowed. Smoke and production integration each passed all five
harnesses on attempt one. The complete feature fixture also passed OpenCode,
which had returned `Blocked` in the earlier run.

| Read-only fast profile | Ergebnis / Result | Versuche / Attempts |
|---|---|---:|
| agy | FAILED: Headless `read_file` denied, no JSON output | 4 |
| Claude | FAILED: extra prose violates JSON-only result protocol | 4 |
| Codex | PASS | 1 |
| Copilot | FAILED: read=true, writeDenied=false | 4 |
| OpenCode | PASS_WITH_RETRY | 3 |

DE: Bei keinem Versuch wurde `forbidden.txt` im gesicherten Inventar gefunden.
Die drei Fehlbefunde beweisen daher keinen erfolgreichen unerlaubten Write;
sie erfuellen den strengeren Lese-/Verweigerungs-/Ergebnisvertrag aber nicht.
Gesamtstatus inklusive optionaler dritter Stufe: `FAILED`. Die gezielte
Behebung dieser neuen Grenzen bleibt als Folgearbeit offen; Rechte wurden
nicht erweitert und keine Fehlversuche in Erfolge umgewertet.

EN: No recorded attempt inventory contained forbidden.txt. The three failures
therefore do not establish a successful forbidden write, but do not meet the
read/denial/result contract. Including the optional third stage, overall status
is `FAILED`. Resolving these newly exposed limits remains follow-up work; no
permissions were broadened or failures reclassified as passes.

DE: Alle drei Stufen meldeten erfolgreichen Cleanup und unveraenderte Quellen
sowie permanentes Profil. Keine Compiler-Daemons mussten nach dem Live-Lauf
manuell beendet werden. Ein gesonderter modellfreier Test erzwang einen
Compiler-Server und belegte Tracking und Stop (tracked=1, stopped=1, failed=0).
Ein zwischenzeitlich fehlgeschlagener modellfreier Test hinterliess seinen
Temp-Stamm; dessen Cleanup wurde nachgeholt und der Test so korrigiert, dass
ein fehlender Besitznachweis die urspruengliche Ausnahme nicht mehr verdeckt.

EN: All three live stages passed cleanup and source/profile invariance checks.
No manual compiler termination was required after the live run. A separate
model-free test forced a compiler server and proved tracked=1, stopped=1,
failed=0. An intermediate failed model-free test left its temporary root;
cleanup was completed and exception handling corrected so a missing ownership
observation cannot mask the original failure.

DE: Die native Offline-CI bestand zunaechst unter macOS und Ubuntu. Windows
meldete eine Analyzer-Aliaswarnung fuer `ps` im Unix-Zweig. Danach wurde das
native Programm ausdruecklich aufgeloest und erneut modellfrei geprueft.
Fehlende Read-only-JSON-Felder erhalten nun eine klare Diagnose statt einer
sekundaeren Property-Ausnahme. Diese Nachbesserungen wurden offline geprueft;
es gab keinen zusaetzlichen Providerlauf ueber die Retry-Grenze hinaus.
Aktuelle native CI-Ergebnisse sind commitbezogen im PR pruefbar.

EN: Native offline CI initially passed macOS/Ubuntu. Windows flagged `ps` as
an alias in the Unix branch; native executable resolution was made explicit
and checked again without models. Missing read-only JSON fields now produce
clear diagnostics instead of secondary property errors. These final corrections
were checked offline, without exceeding the provider retry cap. Native CI
results remain bound to each PR commit.

Combined report SHA-256:
`7cf0d2a78db3c4b3c3f67c545f82b291a0ca6531252b7164e905bfa650ff2585`.
Raw evidence remains local; historical reports are unchanged. No merge,
release, installation, Home sync or rollout was performed.

DE: Eine weitere native Windows-Pruefung erreichte danach alle neuen
Vertragstests, scheiterte aber beim Loeschen des gesperrten temporaeren
.NET-Projektverzeichnisses. Der Cleanup wiederholt nun hoechstens fuenf
Sekunden und akzeptiert nur direkte, nicht verlinkte Test-Stammverzeichnisse
mit bekanntem GUID-Namensschema unter dem physischen Temp-Verzeichnis. Eine
urspruengliche Testausnahme wird nicht durch einen Cleanup-Fehler ersetzt.
Der modellfreie Cold-SDK-Test hat separat 90 Sekunden je Prozess; Live-
Harness-Budgets bleiben unveraendert. Native Nachpruefung erfolgt in CI.

EN: A subsequent Windows run reached all new contract tests but failed while
deleting a locked temporary .NET project directory. Cleanup now retries for at
most five seconds and accepts only direct, non-linked GUID-named test roots
under the physical temp directory. Cleanup cannot replace the original test
exception. The model-free cold-SDK test separately allows 90 seconds per
process; live harness budgets are unchanged. Native verification remains in CI.

DE: Die native Nachpruefung von Code-Commit
`26b1c449a5bf93d5f46a1d246d549e8016392d69` bestand anschliessend auf macOS 15,
Ubuntu 24.04 und Windows 2022, einschliesslich der modellfreien .NET-10-
Regression und der neuen Diagnose-/Fixture-Vertraege. Das ist Offline-Evidence;
native Provider-Live-Nachweise fuer Windows/Ubuntu/WSL2 bleiben offen.

EN: The subsequent native matrix passed code commit
`26b1c449a5bf93d5f46a1d246d549e8016392d69` on macOS 15, Ubuntu 24.04 and
Windows 2022, including model-free .NET 10 regression and new diagnostic/
fixture contracts. This is offline evidence, not Windows/Ubuntu/WSL2 provider
live evidence. This final documentation receipt changes no tested code.

[Native CI run / Nativer CI-Lauf](https://github.com/hindermath/spec-kit-preset-model-routing-governance/actions/runs/41926306717).
