# CI-Exitcodes / CI exit codes

Documentation Impact: **UpdateRequired**. Owner: Thorsten Hindermann.

## Deutsch

Im Adapter-Workflow fehlte nach `test-live-smoke-contract.ps1` und
`test-execution-policy.ps1` die unmittelbare Prüfung von `$LASTEXITCODE`.
Ein späterer erfolgreicher Prozess konnte einen Fehler verdecken. Eine grüne
CI belegte deshalb nicht zuverlässig den Erfolg dieser beiden Tests.

Die Quellenkorrektur beendet den Schritt unmittelbar nach jedem dieser Tests
mit dessen Fehlercode. Ein zusätzlicher Offline-Test liest den tatsächlichen
Workflow-Block, kontrolliert alle unmittelbaren nativen Test-Prüfungen und führt
ihn mit temporären Stubs aus. Er prüft einen erfolgreichen Lauf sowie für die
beiden betroffenen Tests jeweils Exitcode 23. Entfernen der jeweiligen Prüfung
muss den ursprünglichen fälschlich erfolgreichen Lauf reproduzieren. Die echten
Adaptertests laufen weiterhin unverändert in ihrer eigenen CI.

Kanonische Quellen sind `.github/workflows/adapter-discovery.yml` und
`tests/test-ci-exit-codes.ps1`; die Dokumentations-CI führt den Regressionstest
auf macOS, Linux und Windows aus. Leserpfad: README → dieser Nachweis → Workflow.
Zielgruppe: Preset-Maintainer und CI-Reviewende. Textorientierte Ausgabe,
gleichwertiges Englisch und bestehende Laufzeitverträge bleiben erhalten.
Distribution: nur Preset-Quelle/CI, kein Home-Sync und kein Rollout. Die
veröffentlichten Tags bleiben unverändert. Neu bewerten bei neuen Testaufrufen,
Umordnung oder geänderter Shell-Fehlerbehandlung. Patch-Veröffentlichung und
Katalogaktualisierung bleiben getrennte Schritte.

## English

The adapter workflow did not check `$LASTEXITCODE` immediately after
`test-live-smoke-contract.ps1` and `test-execution-policy.ps1`. A later successful
process could mask their failures, so green CI did not reliably prove that
both tests had passed.

The source correction exits immediately with each failing test's code. A new
offline regression reads the actual workflow block, checks every immediate
native-test guard and replays it with temporary stubs. It verifies success and
exit code 23 injected separately into each affected test. Removing each guard
must reproduce the original false success. Real adapter tests continue
unchanged in their own CI.

Canonical sources, CI integration and reader navigation are listed above.
Audiences are preset maintainers and CI reviewers. Text-oriented output,
equivalent languages and runtime contracts are preserved. Distribution is
preset source/CI only, with no Home sync or rollout. Published tags remain
unchanged. Re-evaluate on new test calls, ordering or shell error semantics.
Patch publication and catalog updates remain separate steps.
