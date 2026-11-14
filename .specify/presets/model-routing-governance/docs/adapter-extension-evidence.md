# Adapter-Erweiterung / Adapter extension

## Dokumentationsentscheidung / Documentation decision

Documentation Impact: **UpdateRequired**. Owner: Thorsten Hindermann.
DE: Kanonische Quellen sind Resolver, neue Adapter, Auswahlpolicy und Manifest.
Betroffen: README, beide Nutzungsleitfäden, beide Adapterleitfäden, Command,
Agent-Addendum, Policy-Vorlage, Manpage und Paketvorbereitung. Navigation:
README → Nutzungsleitfaden → Adapterleitfaden → Quellen/Prüfgrenzen.
Zielgruppe: alle vier IHK-IT-Berufe, alle Fachinformatik-Fachrichtungen und
Entwickelnde ohne Spezialwissen. Dokumentklasse: technische Bedienung und
Entwicklungsevidence; Deutsch zuerst, gleichwertiges Englisch, CEFR B2 und
anwendbare WCAG 2.2 AA. Distribution: Preset-Quellkandidat mit beauftragtem
MergeAndSync/Admin-Bypass erst nach grünen exakten Head-Checks. Kein Home-Sync,
keine Verbraucherinstallation. Re-Evaluation bei Quell-, SDK-/CLI-, Policy-
oder API-Drift und vor Veröffentlichung. Kein Ablaufdiagramm erforderlich:
der bestehende lineare Status-/Refresh-Ablauf bleibt unverändert.

EN: Canonical sources are resolver, new adapters, selection policy and manifest.
Affected documents cover README, paired user/adapter guides, command, agent
addendum, policy starter, manpage and package preparation. Reader path: README →
user guide → adapter guide → sources/verification limits. Audience includes all
four IHK IT occupations, every IT-specialist track and non-specialist developers.
Technical operating documentation and development evidence; German first,
equivalent English, B2 and applicable WCAG 2.2 AA. Source candidate delivery
uses authorized MergeAndSync/admin bypass only after exact-head green checks.
No Home sync or consumer installation. Re-evaluate on source,
SDK/CLI, policy or API drift and before publication. No flow diagram is needed
because the existing linear status/refresh flow remains unchanged.

## Prüfung / Verification

DE: Lokal am 2026-11-03 unter macOS mit PowerShell 7.6.9 und Node.js 26.13.0
bestanden: Bash-Syntax und bestehender Discovery-Test, 75 neue Adapter-/Legacy-
Assertions, 119 Prüfungen zur breiten Auswahl, 102 Dokumentations- und
64 Release-/Historien-Assertions sowie alle
7 Node-SDK-/Transporttests. PSScriptAnalyzer 1.25.0 meldet für das neue
Adaptermodul keine Warnungen/Fehler. `git diff --check` und Gitleaks sind grün.
Die HTTP-Fixture benötigt eine lokale Listen-Berechtigung; dafür wurde nur der
Testlauf mit Loopback-Server außerhalb der eingeschränkten Sandbox ausgeführt.

EN: Local checks passed on macOS on 2026-11-03 with PowerShell 7.6.9 and
Node.js 26.13.0: Bash syntax and existing discovery test, 75 new adapter/legacy
assertions, 119 broad-selection assertions, 102 documentation and
64 release/history assertions, all 7 Node
SDK/transport tests. PSScriptAnalyzer 1.25.0 reports no warnings/errors for the
new adapter module. `git diff --check` and Gitleaks pass. The HTTP fixture needs
local listen permission; only the loopback test run used execution outside the
restricted sandbox.

DE: Die Tests verwenden ausschließlich temporäre Profile und synthetische
Metadaten. Der Legacy-Vergleich liest den Resolver aus Ausgangscommit
`b35ee208959ec133e51ad557820a393b9bdddbf4` und vergleicht Profile (ohne
Zeitstempel), Status, Exitcodes und Mehrdeutigkeitsfehler für Codex/Antigravity.
Neue Fälle prüfen vier Rollen, Status ohne Schreiben, WhatIf, atomar erhaltene
Profile bei Blockern, fehlende Fähigkeiten, IDs, doppelte/leere Listen und
Copilot-Policies. Node-Tests prüfen SDK-Initialisierung und Schließen ohne
Inferenz; Transporttests prüfen die echte PowerShell-/Node-Verkettung und
einen lokalen HTTP-Fixture-Server einschließlich Providerfilter, Redirect-
Sperre, URL-Grenzen und unterdrückten Herstellerfehlern.

EN: Tests use temporary profiles and synthetic metadata only. Legacy comparison
reads the resolver from the base commit and compares profiles (excluding
timestamps), status, exit codes and ambiguity errors for Codex/Antigravity.
New cases cover four roles, status without writes, WhatIf, preserved profiles
on blockers, missing capabilities, IDs, duplicate/empty inventories and Copilot
policies. Node tests check SDK initialization/cleanup without inference.
Transport tests exercise the real PowerShell/Node chain and a local HTTP
fixture server, including provider filtering, redirect denial, URL boundaries
and suppressed vendor errors.

```text
bash tests/test-model-routing-discovery.sh
pwsh -NoProfile -File tests/test-additive-adapters.ps1
pwsh -NoProfile -File tests/test-broad-model-selection.ps1
node --test tests/test-sdk-bridge.mjs tests/test-adapter-transports.mjs
pwsh -NoProfile -File tests/test-documentation-closeout.ps1
pwsh -NoProfile -File tests/test-release-status.ps1
```

DE: Ergebnisse und Plattformen stehen im PR; keine vorweggenommenen CI-Pässe.
Live-Anmeldung, tatsächliche SDK-/CLI-Kombinationen, Kontingent, Modellzugriff,
spätere Runner-Ausführung, Screenreader/Braille und vollständige AA-Abnahme
bleiben ohne eigenen Nachweis offen. Die Fixtures beweisen den Adaptervertrag,
nicht die Verfügbarkeit eines realen Kontos. Keine SDKs werden installiert.

EN: Actual outcomes and platforms belong in the PR; no assumed CI passes.
Live authentication, actual SDK/CLI combinations, quota, model access,
subsequent runner execution, screen-reader/Braille and full AA acceptance remain
open without separate evidence. Fixtures establish the adapter contract, not
real-account availability. Tests install no SDKs.

DE: Der Folgeauftrag erweitert die Auswahl auf GPT, o-Serie, Gemini, Sonnet und
sonstige geeignete Modelle. Präferenzstufen sind keine Qualitätsrangliste.
Fähigkeiten werden vor Präferenzen gefiltert; exakte IDs lösen Mehrdeutigkeit,
ohne Copilot-Policy oder Providerbindung zu umgehen. Ein Modell darf mehrere
Rollen übernehmen; fehlendes Haiku blockiert nicht. Neue Tests prüfen diese
Fälle, Listenreihenfolge, hohe Effort-Stufen ohne erfundene Absenkung,
ausgeschlossene Switching-Aliase und den Hash der Auswahlpolicy. Die frühere
Negativannahme „unbekannte Familie immer blockieren“ wurde durch den stärkeren
Vertrag „nur geeignete gemeldete IDs; Mehrdeutigkeit blockiert“ ersetzt.

EN: The follow-up extends selection to GPT, o-series, Gemini, Sonnet and other
capable models. Preference tiers are not quality rankings. Capability filtering
precedes preferences; exact IDs resolve ambiguity without bypassing Copilot
policy or provider binding. One model may serve several roles; missing Haiku
does not block. Tests cover these cases, inventory order, high effort without
invented reductions, excluded switching aliases and policy-hash binding. The
earlier negative assumption "unknown family always blocks" is replaced with
"eligible discovered IDs only; ambiguity blocks".

## Sicherheitsprüfung / Security review

DE: NIST SSDF/CWE dienen als Entwicklungsbasis. Geprüfte Grenzen: dynamischer
SDK-Code nur über ausdrücklich benannte lokale Datei; Metadaten sind nicht
vertrauenswürdig; keine Shell-Auswertung von Modellkennungen; feste Argument-
Arrays; keine automatische Abhängigkeitseinrichtung; HTTP ausschließlich auf
explizitem Loopback ohne Proxy/Redirect; Antwort-/Zeitlimits; keine Tokens in
Git, Modellprofilen oder Herstellerfehlern. Kryptografie ist nicht neu
eingeführt. Lieferketten-/Release-Evidence bleibt außerhalb dieses PR-Auftrags.
Hersteller-Planmodi ersetzen keine OS-Isolation. Nur synthetische Tests sind
kein vollständiger Security- oder Zugangs-Pass.

EN: NIST SSDF/CWE provide the development baseline. Reviewed boundaries:
dynamic SDK code only through an explicit local file; untrusted metadata;
no shell evaluation of model IDs; fixed argument arrays; no dependency
installation; explicit loopback-only HTTP without proxies/redirects; response
and time limits; no tokens in Git, profiles or vendor-error output. No new
cryptography. Supply-chain/release evidence is outside this PR's authority.
Vendor plan modes do not replace OS isolation. Synthetic tests are not full
security or access acceptance.
