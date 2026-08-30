# Schnellstart für die spätere Umsetzung / Quickstart for Later Implementation

**Feature**: `045-rl-se-checklist-self-review`
**Branch**: `045-rl-se-checklist-self-review`

Dieses Dokument beschreibt die geplante Reihenfolge nach erfolgreicher
Plan-Review-, Tasks- und Analyze-Phase. Es führt jetzt keinen Befehl aus und
erteilt keine zusätzliche Git-, Remote-, Provider-, Secret-, Governance-
Reparatur- oder Folgefeature-Autorität.

*This document describes the planned sequence after plan review, tasks, and
Analyze. It executes no command now and grants no additional authority.*

## 1. Eintritt und Autorität prüfen / Verify Entry and Authority

1. Der Branch muss `045-rl-se-checklist-self-review` sein.
2. Der autonome State muss weiterhin Run-ID
   `0290a195-0405-43e1-9b94-64535ea9b386`, Stage passend zur aktiven Phase,
   Status `Active` und den gespeicherten Delivery-Modus `MergeAndSync` zeigen.
   Dieser Modus ist historische Evidence und keine fortdauernde Berechtigung.
3. Jede spätere Commit-, Push-, PR-, Merge-, Bypass- oder Cleanup-Operation
   verlangt eine aktuelle ausdrückliche Autorisierung. `MergeAndSync` erweitert
   auch dann nicht den Audit-only-Scope der Selbstprüfung.
4. Die fünf im Plan genannten Feature-Input-Hashes und der sechste gebundene
   Hash des `Ready`-Reviews müssen übereinstimmen.
5. Git-HEAD, Diff, geschützte Wurzeln, Registry, beide Constitutions,
   Baseline, Feature-016- und Feature-044-Evidence müssen vor der ersten
   Änderung erfasst werden.
6. Drift an bindenden Feature-Artefakten stoppt die Umsetzung bis zur
   erneuten Plan-/Analyze-Bewertung.

*Verify branch, autonomous state, authority, accepted hashes, HEAD, diff,
registry, constitutions, baseline, and Feature 016/044 evidence. Drift stops
work.*

## 2. Evidence zuerst anlegen / Create Evidence First

Vor dem ersten Validator- oder breiten Audit-Edit entstehen:

- `specs/045-rl-se-checklist-self-review/pr-evidence.md` mit Authority,
  Accepted Hashes, Planning-HEAD, Protected Roots, Command-Ledger,
  Validierungsstatus `Not Run`, Human-only-Grenzen und Scope-Firewall;
- die datierte Evidence-Struktur unter
  `docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/`;
- ein parsefähiges `rl-se-self-review.json` im Zustand
  `EvidenceSkeletonCreated`, aber ohne bestandenen Auditclaim;
- alle benannten Markdown-Projektionen mit klarer Kennzeichnung, dass die
  Detailprüfung noch nicht abgeschlossen ist;
- die Fixture-Verzeichnisse für positiven Slice und isolierte Negativfälle.

Kein frühes Artefakt darf Build, Test, Coverage, DocFX, A11Y, Remote Review,
Merge, Synchronisierung, formale Freigabe oder Compliance als bestanden
darstellen.

## 3. Bindende Quellen revalidieren / Revalidate Binding Sources

Verwende vorhandene Repository-Validatoren zuerst:

```bash
repo_root="$(git rev-parse --show-toplevel)"
bash .specify/presets/intake-sequencing-governance/scripts/validate-intake-series-manifest.sh \
  --file requirements/intakes/series/tui-vision-delivery/manifest.json --repo "$repo_root"
bash .specify/presets/intake-sequencing-governance/scripts/validate-intake-series-receipt.sh \
  --file requirements/intakes/series/tui-vision-delivery/receipt.json --repo "$repo_root"
bash .specify/presets/intake-review-governance/scripts/validate-intake-review-result.sh \
  --result requirements/intakes/series/tui-vision-delivery/intake-review-result.json \
  --repo "$repo_root"
bash .specify/presets/autonomous-run-governance/scripts/validate-autonomous-run-state.sh \
  --state specs/045-rl-se-checklist-self-review/autonomous-run-state.json
```

Diese vier Befehle sind read-only. Der kombinierte Alignment-Wrapper wird hier
nicht verwendet: Er startet zusätzlich einen Renderer und erkennt die
akzeptierte Spec-Bezeichnung `Binding Input` derzeit nicht. Der Audit ändert
weder Wrapper noch akzeptierte Spezifikation.

Ermittle anschließend read-only:

```bash
rg -n '^#### CL-[0-9]{2}-[0-9]{2}' \
  docs/secure-development/checklisten/CL_*.md
git diff --name-only -- src examples tv203s TVDEMOS TVFM
git status --short
```

Akzeptanz: genau 157 eindeutige Quell-IDs, Kapitelzahlen
`12/13/15/10/13/11/12/13/17/17/12/12`, null doppelte und null unbekannte IDs.
Die Extraktion und Hashberechnung wird im Evidence-Ledger dokumentiert. Die
Quellen werden nicht geändert.

## 4. Build-Counter-Grenze / Build Counter Boundary

Vor jedem einzelnen späteren `dotnet build` oder `dotnet test`:

1. ermittle den Feature-Commit-Count;
2. erhöhe den manuellen Build-Counter genau einmal;
3. setze `Version`, `AssemblyVersion` und `FileVersion` gemeinsam auf
   `1.45.<FeatureCommitCount>.<Build>`;
4. prüfe, dass die drei Werte identisch sind;
5. führe genau den einen geplanten Build- oder Testbefehl aus.

Ein `dotnet test` mit implizitem Build braucht keine zweite Erhöhung. Restore,
Format, reine Scans, DocFX und NPM erhöhen den Counter nicht, sofern sie keinen
expliziten `dotnet build` oder `dotnet test` ausführen. Vor Commit oder Push
wird der Patch-Anteil auf den dann geltenden Feature-Commit-Count ausgerichtet.

## 5. Vollständige Testoberfläche prüfen / Check the Complete Test Surface

Lege die Validator-Testklasse und Fixture-Ladewege an, ohne bereits alle
Invarianten zu implementieren. Kompiliere dann die gesamte betroffene
Testassembly. Ein Compiler-, Fixture-Pfad- oder bestehender Testfehler ist kein
erwartetes Red und stoppt die Slice-Arbeit.

*Establish the complete test surface first. Compilation, fixture path, or
existing-test failures are not accepted as semantic red proof.*

## 6. Repräsentativen Slice rot/grün beweisen / Prove the Representative Slice Red/Green

1. Starte den fokussierten Test für `CL-01-01` gegen das Audit-Skelett.
2. Der erwartete Fehler ist ausschließlich der stabile Code für eine fehlende
   oder unvollständige Kontrollzeile.
3. Ergänze die vollständige Quellidentität, alle Pflichtfelder,
   Feature-016-Vergleich, aktuelle Evidence-Freshness, Statusentscheidung,
   `security-governance`-Preset-Bezug, die begrenzte Feature-044-Sandbox-
   Evidence und Markdown-Projektion.
4. Wiederhole den fokussierten Test. Er muss JSON, Evidence-Relation, Status,
   Preset-Relation, Projektion und Scope grün beweisen.
5. Ergänze die isolierten Negativ-Fixtures für den Slice. Jede verletzt genau
   eine Invariante und erwartet einen stabilen `RLSE###`-Code.

Erst nach grünem Slice und grünen Negativfällen beginnt die breite
157-Zeilen-Bearbeitung.

## 7. Kapitelweise Kontrollbewertung / Assess Controls Chapter by Chapter

Bearbeite in fester Reihenfolge:

1. `CL-01` Standards und Anwendbarkeit;
2. `CL-02` Architektur;
3. `CL-03` Kryptografie;
4. `CL-04` Bedrohungsmodellierung;
5. `CL-05` Lieferkette und Build-Integrität;
6. `CL-06` Schwachstellenoffenlegung;
7. `CL-07` CRA;
8. `CL-08` Sicherheits-Code-Review;
9. `CL-09` KI-Codeerzeugung;
10. `CL-10` Entwicklungsumgebung;
11. `CL-11` Datenschutz-Folgenabschätzung;
12. `CL-12` agentische Sandbox.

Nach jedem Kapitel prüft der Validator Quellmenge, Reihenfolge, Pflichtfelder,
Statusregeln, Evidence-Relationen, Feature-016-Vergleich und Markdown-Parität.
Eine positive Aussage ohne aktuelle direkte Evidence wird herabgestuft oder
bleibt offen; sie wird nicht durch Annahmen ergänzt.

## 8. Presets und Drift einfrieren / Freeze Presets and Drift

1. Erzeuge exakt zwölf Preset-Datensätze aus `.specify/presets/.registry` und
   den zwölf `preset.yml`-Artefakten.
2. Prüfe alle Preset-Fassungen und Manifest-Hashes.
3. Bestätige, verwerfe oder präzisiere die bekannten Driftkandidaten:
   Baseline-Versionen, Constitution 1.17.0/1.18.1, Preset-Zahlen und
   Feature-016-Freshness und Feature-044-Sandbox-Grenzen.
4. Jede bestätigte Beobachtung führt beide Quellen, Auswirkung, Owner,
   Priorität, Restrisiko, Aktion und Trigger.
5. `repairPerformed` bleibt `false`; keine Governance-Quelle wird geändert.

## 9. Human-only-Grenzen prüfen / Review Human-Only Boundaries

Prüfe getrennt:

- Rechts- und regulatorische Rolle;
- Organisations- und Freigabeentscheidungen;
- GitHub-/Provider-Einstellungen und externe Scans;
- Secrets und reale Host-/Sandbox-Konfiguration;
- Windows-/Linux-/WSL- oder andere Plattformfakten;
- formale Audit-, QISMS-, Zertifizierungs- und Compliance-Aussagen.

Ohne befugte veröffentlichbare Evidence bleibt der Punkt `Open`, `FollowUp`
oder faktisch begründet `N/A`. Der Agent darf ihn nicht schließen.

## 10. Negative Fixtures vollständig ausführen / Run the Complete Negative Fixture Set

Die Fixture-Menge umfasst mindestens:

```text
wrong-total-count
wrong-chapter-count
duplicate-control-id
unknown-control-id
invalid-status
invalid-priority
empty-required-field
already-satisfied-without-direct-current-evidence
na-without-factual-rationale-or-trigger
open-without-owner-action-risk-or-trigger
follow-up-without-bounded-later-work
missing-or-wrong-preset
unauthorised-human-claim
governance-observation-with-repair-claim
absolute-or-private-path
markdown-projection-drift
protected-root-scope-violation
```

Jede Fixture muss atomar scheitern, genau den erwarteten Primärcode liefern
und keine Datei schreiben.

## 11. Lokale Validierungsleiter / Local Validation Ladder

Die spätere Reihenfolge ist:

```text
1. Accepted-input, intake-review, run-state, registry and baseline revalidation
2. git diff --check and exact protected-root/API/dependency/project scans
3. focused CL-01-01 positive and negative development tests
4. complete RL-SE positive and isolated-negative development tests
5. separate vulnerable and deprecated package scans
6. read-only immutable workflow-reference inventory and classification
7. dotnet format --verify-no-changes
8. secret scan and prohibited-path/content scan
9. bilingual CEFR-B2, terminology, semantic Markdown and text-first review
10. docs/project-statistics.md Profile-2 update
11. docfx docfx.json
12. Playwright/Axe DocFX smoke plus UTF-8 lynx spot check
13. xmllint validation of coverlet.runsettings
14. one final complete Release+Coverlet solution test containing both validator methods
15. staged primary-candidate check, commit, and committed-candidate scope/integrity scan
16. current-authority check, push/PR, and remote checks/reviews for the same feature HEAD
17. exact-head PreMerge gate evidence and outer evidence validation
18. renewed current-authority check, authorized feature merge, branch cleanup, and clean main sync
19. post-merge Lastenheft rename plus atomic series transition
20. trigger-proportional validation and exactly one evidence-only closeout PR
21. authorized closeout merge and final clean main verification
```

Die Supply-Chain-Befehle bleiben getrennt, weil `dotnet list package`
`--vulnerable` und `--deprecated` nicht in einem Aufruf akzeptiert. Die
Workflow-Inventur ist ein Audit: vorhandene bewegliche Referenzen werden als
Finding erfasst, nicht in diesem Feature repariert.

Die finalen Gate-Befehle sind bereits vorhandene Repository- oder
Toolchain-Oberflächen; nur Testdatei, Fixtures und Evidence werden durch
spätere Tasks neu angelegt:

```bash
dotnet list TuiVision.sln package --vulnerable --include-transitive
dotnet list TuiVision.sln package --deprecated --include-transitive
git grep -n -E '^[[:space:]-]*uses:[[:space:]]+' -- .github/workflows
dotnet format --verify-no-changes
bash scripts/scan-agent-secrets.sh --fail-on-high "$repo_root"
docfx docfx.json
(cd tests/web-a11y && npm run test:docfx)
lynx -dump -assume_charset=utf-8 -display_charset=utf-8 \
  _site/docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/README.html
xmllint --noout coverlet.runsettings
dotnet test TuiVision.sln --configuration Release \
  --collect:"XPlat Code Coverage" --settings coverlet.runsettings \
  --logger "console;verbosity=detailed"
```

Vor dem letzten `dotnet test` gilt die Build-Counter-Sequenz aus Abschnitt 4.
Die Test-Tasks müssen die Methoden `Test_CompleteAuditIsValid` und
`Test_InvalidFixturesFailClosed` exakt anlegen, damit ihr Auftreten im
detaillierten Volltest-Log mechanisch prüfbar ist.

Jeder vor dem Candidate-Freeze tatsächlich ausgeführte Befehl erhält Exitcode,
Plattform/Runner, wesentliche Ausgabe und Fehlergrenze in `pr-evidence.md`.
Der finale Volltest und spätere Exact-Head-/Remote-Befehle werden ohne
getrackten Post-Freeze-Edit im untracked Log beziehungsweise in der temporären
PreMerge-Evidence erfasst. Ein übersprungenes Gate erhält `N/A`, Begründung und
Trigger; es wird nie als Pass gezählt.

## 12. Scope-Firewall prüfen / Check the Scope Firewall

Der fachliche Abschlussdiff muss null Änderungen an diesen Flächen zeigen:

```text
src/
examples/
tv203s/
TVDEMOS/
TVFM/
*.sln
*.csproj
Directory.Packages.props
.config/dotnet-tools.json
.github/workflows/
.specify/presets/
constitution.md
.specify/memory/constitution.md
docs/secure-development/
docs/security/control-assessment.md
docs/security/secure-development/2026-08-29-sandbox-applicability/
specs/016-secure-development-hardening/pr-evidence.md
specs/044-sandbox-secure-development-hardening/pr-evidence.md
```

Der primäre PreMerge-Kandidat erlaubt nur die namentlich geplanten Feature-045-
Artefakte ohne manuellen State-Edit, den bereits runner-erzeugten Zeiger
`.specify/feature.json` ohne weiteren manuellen Edit, die neue datierte
Evidence, `docs/security/README.md`, `docs/project-statistics.md`, die benannte
C#-Testdatei und Fixture-Wurzel sowie `Directory.Build.props`.

Erst nach dem tatsächlichen Feature-Merge darf der getrennte Closeout-Kandidat
das gepaarte Lastenheft-Rename, die sieben bestehenden Dateien unter
`requirements/intakes/series/tui-vision-delivery/`, genau ein neues
Manifest-/Receipt-Archivpaar unter der festen Serien-ID, `delivery-closeout.md`,
`retrospective.md`, `tasks.md`, die postmerge aktualisierte
`docs/project-statistics.md` und die ausschließlich runner-owned
Terminalprojektion des Run-State enthalten. Die konkrete Operations-ID wird
vor dem Write aus dem transaktionalen Serienplan gebunden. Ein anderer Pfad ist
ein Hard Stop. Runner-Ausgaben unter `.specify/runtime/` bleiben untracked und
werden im Worktree-Status getrennt klassifiziert, nie in einen Kandidaten
aufgenommen.

## 13. Shared Writes serialisieren / Serialize Shared Writes

Bearbeite diese Flächen niemals parallel:

- `rl-se-self-review.json` und alle Projektionen;
- `pr-evidence.md` und `delivery-closeout.md`;
- `autonomous-gate-requirements.json` und Gate-Evidence;
- `Directory.Build.props`;
- `docs/security/README.md`;
- `docs/project-statistics.md`;
- Intake-Archivierung und Delivery-State.

## 14. Exakten HEAD belegen / Prove the Exact HEAD

Nach lokaler Konvergenz:

1. Stage ausschließlich den beabsichtigten Kandidaten.
2. Prüfe `git diff --cached --check` und gleiche staged Pfade gegen
   `git status --short` und die geschlossene Positivliste ab.
3. Richte Version und Build-Counter für den letzten vollständigen
   Release-Coverage-Test aus, führe ihn aus und committe den unveränderten
   grünen Kandidaten. Jede Korrektur startet diese Sequenz erneut.
4. Prüfe den committed Candidate mit
   `git diff --check "$(git merge-base origin/main HEAD)" HEAD`,
   `git diff --name-only "$(git merge-base origin/main HEAD)" HEAD` und
   `git status --short`.
5. Push und PR erfolgen erst nach diesen lokalen Eintrittsgates. Ordne aktuelle
   Remote-Checks und Review-Threads exakt dem vollständigen Feature-HEAD zu.
6. Schreibe danach temporäre PreMerge-Evidence nach
   `/private/tmp/045-rl-se-checklist-self-review.premerge-gate-evidence.json`.
7. Validiere sie außerhalb ihrer eigenen Gate-Liste gegen
   `autonomous-gate-requirements.json` und exakt diesen HEAD mit dem
   vorhandenen autonomen Gate-Validator:

   ```bash
   bash .specify/presets/autonomous-run-governance/scripts/validate-autonomous-gate-evidence.sh \
     --requirements specs/045-rl-se-checklist-self-review/autonomous-gate-requirements.json \
     --evidence /private/tmp/045-rl-se-checklist-self-review.premerge-gate-evidence.json \
     --head "$(git rev-parse HEAD)"
   ```
8. Behaupte keinen PostMerge-Fakt in PreMerge-Evidence.

Die Existenz und das Bestehen dieser Evidence werden erst nach der tatsächlichen
Ausführung behauptet, nicht durch Plan oder Tasks vorweggenommen.

## 15. MergeAndSync-Abschluss / MergeAndSync Closeout

Nach grünen lokalen Eintrittsgates und aktueller ausdrücklicher
`PublishPR`- oder `MergeAndSync`-Autorisierung darf die Delivery-Orchestrierung
Commit, Push und PR ausführen. Erst wenn anschließend alle PreMerge-Gates,
aktuellen Remote-Checks und Reviews für denselben HEAD konvergiert sind und
aktuelle ausdrückliche `MergeAndSync`-Autorisierung erneut vorliegt, darf sie:

1. technische Findings beheben und alle betroffenen Gates am neuen HEAD
   wiederholen;
2. ohne technischen Gate-Bypass mergen;
3. den Remote-Feature-Branch gemäß Policy bereinigen;
4. `main` fast-forward-sicher synchronisieren;
5. sauberes `main == origin/main` und die erforderliche temporäre
   Schema-2.0-PostMerge-Evidence prüfen;
6. erst jetzt das Lastenheft gepaart archivieren und die Intake-Serie atomar
   fortschreiben, ohne den nächsten Intake zu starten;
7. dauerhafte Delivery-Fakten, Retrospektive und runner-owned
   Terminalprojektion in genau einem triggerproportional validierten
   Evidence-only-Closeout-PR liefern, ohne dessen eigene PR-, Head- oder
   Merge-Identität rekursiv zu behaupten;
8. den Closeout-PR nur unter erneut aktueller ausdrücklicher Autorität mergen,
   Branches bereinigen und abschließend read-only `main == origin/main`, State,
   Serie und null automatische Folgearbeit beweisen.

Provider-Konfiguration, Secrets, Rechtsfreigabe, Governance-Reparatur und
automatische Folge-Intakes bleiben außerhalb dieser Sequenz.

## 16. Sichere Stop- und Resume-Grenzen / Safe Stop and Resume Boundaries

Sichere Stopps liegen nach Plan, Evidence-Skelett, Vertikalschnitt, jedem
Kapitel, Preset-/Drift-Freeze, Human-boundary-Freeze, lokaler Validierung und
PreMerge-Evidence. Eine Unterbrechung verlangt Revalidierung von State,
Authority, Accepted Hashes, HEAD, Diff, Scope, Build Counter und letztem Gate.
Nur der Orchestrator ändert den Run-State an einer Phasengrenze.
