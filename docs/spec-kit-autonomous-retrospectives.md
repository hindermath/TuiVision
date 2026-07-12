# Retrospektiven autonomer Spec-Kit-Läufe

Dieses Ledger sammelt nur Erkenntnisse, die den autonomen Ablauf verbessern.
Feature-Fakten bleiben in der jeweiligen `pr-evidence.md`. Eine Regel wird erst
projektweit verallgemeinert, wenn sie wiederholt belegt ist. Fehler bei
Korrektheit, Sicherheit oder Evidence dürfen sofort korrigiert werden.

This ledger contains only findings that improve autonomous delivery. Feature
facts stay in the related `pr-evidence.md`. A preference becomes a project-wide
rule only after repeated evidence. Correctness, security, and evidence defects
may be corrected immediately.

## Entscheidungsmodell / Decision Model

| Entscheidung | Bedeutung |
|---|---|
| `FeatureSpecific` | Bleibt beim betroffenen Feature |
| `RunbookClarification` | Präzisiert einen bestehenden Ablaufvertrag |
| `SkillCorrection` | Korrigiert die ausführbare Agenten-Orchestrierung |
| `TemplateCorrection` | Verhindert den Fehler bei neuen Artefakten |
| `AgentPolicyCorrection` | Muss auf allen gepflegten Agentenflächen gelten |
| `ValidationAutomation` | Benötigt einen deterministischen automatisierten Nachweis |
| `PresetFollowUp` | Ist potenziell projektübergreifend und wird Home Baseline übergeben |
| `NoPromotion` | Liefert keine belastbare allgemeine Regel |

## 018 Editor, Help and Resources Hardening

**Feature:** `018-editor-help-resources-hardening`  
**Feature-PR:** [#42](https://github.com/hindermath/TuiVision/pull/42)  
**Closeout-PR:** [#43](https://github.com/hindermath/TuiVision/pull/43)

| Beobachtung | Entscheidung | Umsetzung oder Grenze |
|---|---|---|
| Vier Remote-Tasks nannten keinen konkreten Evidence-Pfad. Analyze erkannte dadurch eine echte Resume- und Abnahmelücke. | `SkillCorrection`, `TemplateCorrection`, `AgentPolicyCorrection`, `PresetFollowUp` | Runbook, Skill, Task-Template und fünf Agentenflächen verlangen jetzt den exakten Repository-Evidence-Pfad. Diese Evidence-Korrektur wird sofort übernommen. |
| Negative Serialization-Grenzen wurden in zwei fokussierten Rot-Grün-Zyklen entdeckt. Beide Zyklen fanden echte Defekte, erhöhten aber den Build-Counter mehrfach. | `PresetFollowUp` mit `ObserveAgain` | Noch keine allgemeine Bündelungsregel. Feature 019 muss zeigen, ob eine vollständige projektlokale Red-Matrix vor dem ersten Green sicher und effizienter ist. |
| Copilot konnte wegen Nutzerquota nicht reviewen; alle Pflichtchecks, Claude und GraphQL-Threads waren grün. Nur Human Approval blockierte. | `RunbookClarification`, keine neue Regel | Das bestehende Runbook behandelt fehlende Reviewer korrekt als fehlenden Review und begrenzt den Admin-Bypass auf die offene Human-Approval-Regel. |
| Post-Merge-Fakten konnten erst nach dem Feature-Merge wahrheitsgemäß feststehen. | `NoPromotion` | Der vorhandene Evidence-only-Closeout-Vertrag funktionierte wie vorgesehen; PR #43 blieb nicht leer und änderte keine Runtime. |

### Nächster Prüfschritt / Next Check

Feature 019 prüft erneut, ob Remote-Tasks vollständige Evidence-Pfade tragen
und ob ein gebündelter Red-Proof ohne Verlust der test-first Nachvollziehbarkeit
weniger Build-Zyklen benötigt. Erst dann darf die Effizienzpräferenz zum
allgemeinen Ablaufvertrag werden.

Feature 019 rechecks exact evidence paths on remote tasks and whether a grouped
red proof can reduce build cycles without weakening test-first traceability.
Only repeated evidence may promote that efficiency preference.

## 019 Wave-3 Visual Component Porting

**Feature:** `019-wave3-visual-component-porting`
**Feature-PR:** [#45](https://github.com/hindermath/TuiVision/pull/45)
**Closeout-PR:** [#46](https://github.com/hindermath/TuiVision/pull/46)

| Beobachtung | Entscheidung | Umsetzung oder Grenze |
|---|---|---|
| Die gebündelten Help- sowie I18n/TvHc-Red-Matrizen lokalisierten die fehlenden Implementierungstypen mit je einem erwarteten Lauf und wurden danach mit je einem grünen Lauf geschlossen. Damit ist die 018-Beobachtung zum zweiten Mal bestätigt. | `RunbookClarification`, `SkillCorrection`, `TemplateCorrection`, `AgentPolicyCorrection`, `PresetFollowUp` mit `Promote` | Runbook, Skill, Task-Template und Agentenflächen erlauben vollständige projektlokale Red-Matrizen bei expliziten Einzelgrenzen und gemeinsamer Ownership. |
| Der TvEdit-Slice lief vor vollständiger Prüfung von Imports, öffentlichen XML-Dokumentationen, Harness-Helfern sowie Fokus-/Ownership-Assertionen. Daraus entstanden vermeidbare Compile- und Harness-Zyklen. | `SkillCorrection`, `TemplateCorrection`, `PresetFollowUp` mit `Promote` | Vor dem ersten Red-Befehl ist jetzt ein Compile-Surface-Check Pflicht. Er ändert keine test-first-Reihenfolge, sondern vervollständigt den geplanten roten Vertrag. |
| `Wave3Runtime.cs` wird in fünf Beispiel-Assemblies gelinkt. Der erste Matrix-Helfer behandelte die fünf Basistypkopien irrtümlich als eine CLR-Typidentität. | `RunbookClarification`, `SkillCorrection`, `TemplateCorrection`, `PresetFollowUp` mit `Promote` | Cross-Projekt-Proof nutzt bei mehrfach gelinktem Quellcode öffentliche Verträge oder Zustandsdelegaten. Eine gemeinsame Assembly ist nur bei bewusster Architekturentscheidung zulässig. |
| Alle Remote-Tasks trugen den exakten 019-Evidence-Pfad; Resume und Closeout blieben eindeutig. | `NoPromotion` | Die in 018 sofort korrigierte Evidence-Regel ist im zweiten Lauf bestätigt und benötigt keine weitere lokale Änderung. |
| Copilot war erneut wegen Nutzerquota nicht verfügbar; grüne Pflichtchecks, Claude und null GraphQL-Threads ließen nur Human Approval offen. | `NoPromotion` | Die vorhandene Bypass-Grenze funktionierte für Feature- und Closeout-PR ohne Ausweitung. |

### Nächster Prüfschritt / Next Check

Feature 020 prüft, ob der Compile-Surface-Check vor dem ersten Red-Lauf
vollständig durchgeführt wird und ob gebündelte negative Fälle weiterhin
weniger administrative Builds benötigen, ohne Fehlergrenzen oder Ownership zu
verwischen. Die Linked-Source-Regel wird nur ausgelöst, wenn Feature 020 solche
Quellen tatsächlich berührt.

Feature 020 verifies that the compile-surface check happens before the first
red batch and that grouped negative cases still reduce administrative builds
without hiding failure boundaries or ownership. The linked-source rule is
triggered only if Feature 020 actually touches such source composition.

## 020 Mouse Support and Interaction

**Feature:** `020-mouse-support-interaction`
**Feature-PR:** [#48](https://github.com/hindermath/TuiVision/pull/48)
**Closeout-PR:** [#49](https://github.com/hindermath/TuiVision/pull/49)

| Beobachtung | Entscheidung | Umsetzung oder Grenze |
|---|---|---|
| Der Compile-Surface-Check lief vor dem ersten Driver-Red-Batch; Imports, XML-Dokumentation, Harness, Fokus/Ownership und die nicht ausgelöste Linked-Source-Grenze waren vollständig benannt. | `NoPromotion` | Die in 019 korrigierte Regel ist im nächsten Feldlauf bestätigt. Linked Source war `N/A`, weil 020 keine Quelldatei in mehrere Assemblies linkt. |
| Die vollständige Driver-Red-Matrix hielt malformed, range, phase, recovery und click-boundary als getrennte erwartete Fehler bei gemeinsamer Projekt-Ownership sichtbar. | `NoPromotion` | Die bereits nach 018/019 promovierte Bündelungsregel ist ein drittes Mal bestätigt und benötigt keine weitere lokale Änderung. |
| Der Commit mit PR-URL und geprüftem Remote-Stand änderte den Feature-Head. Dadurch wurden die gerade dokumentierten Check-/Thread-Fakten sofort historisch und alle Pflichtchecks liefen erneut. | `RunbookClarification`, `SkillCorrection`, `TemplateCorrection`, `AgentPolicyCorrection`, `PresetFollowUp` mit `Promote` | Runbook, Skill, Tasks-/Evidence-Template und alle fünf Agentenflächen verlangen jetzt: Gates vor dem Merge prüfen, aber selbstinvalidierende Reviewed-Head-Fakten in genau einen benannten kausalen Closeout-Pfad legen. |
| Die aktuelle Session war macOS, aber headless mit `TERM=dumb`; physische Terminal-Evidence wurde als `NotRun` statt als Pass geführt. | `FeatureSpecific` | Host-Vertrag und deterministische Injection sind grün. Physische macOS/Linux/WSL-Diversität bleibt eine ehrliche Host-Evidence-Grenze und wird nicht als allgemeine Automationsleistung behauptet. |
| Copilot war wegen Nutzerquota erneut nicht verfügbar; Claude und alle Pflichtchecks waren grün, GraphQL meldete null Threads und nur Human Approval blieb offen. | `NoPromotion` | Die bestehende Berechtigungs- und Bypass-Grenze funktionierte bei Feature- und Closeout-PR unverändert. |

### Nächster Prüfschritt / Next Check

Feature 021 muss Remote-Checks und Threads vor dem Merge prüfen, darf deren
aktuellen Head-Stand aber nicht durch einen weiteren Feature-Commit selbst
entwerten. Tasks müssen den einen Closeout-Evidence-Pfad bereits vor dem Remote-
Abschluss benennen. Ein Skript wird erst erwogen, wenn eine stackneutrale,
deterministische Erkennung dieses Zustands nachgewiesen ist.

Feature 021 must verify remote checks and threads before merge without
invalidating that reviewed head through another feature commit. Tasks must name
the single closeout evidence path before remote delivery. A script remains
deferred until a stack-neutral deterministic detector is proven.

## 022 Wave-4 Visual Component Porting

**Feature:** `022-wave4-visual-component-porting`
**Feature-PR:** [#53](https://github.com/hindermath/TuiVision/pull/53)
**Closeout-PR:** [#54](https://github.com/hindermath/TuiVision/pull/54)

| Beobachtung | Entscheidung | Umsetzung oder Grenze |
|---|---|---|
| Der Closeout blieb genau ein Evidence-Commit, weil seine eigene PR-URL, sein Checkstand und sein Merge nicht zurück in dieselbe Datei geschrieben wurden. Damit ist die in 020/021 entstandene Self-Invalidation-Grenze positiv bestätigt. | `RunbookClarification`, `SkillCorrection`, `TemplateCorrection`, `AgentPolicyCorrection`, `PresetFollowUp` mit `Promote` | Runbook, Skill, Tasks-/Evidence-Template und fünf Agentenflächen verlangen jetzt einen evidence-only, single-commit-fähigen Closeout ohne rekursive Selbstreferenz. Terminale Closeout-Fakten werden extern geprüft. |
| Bei Feature- und Closeout-PR starteten Push und PR jeweils gleichartige Workflow-Sätze. Dasselbe Verhalten war bereits in 021 sichtbar. | `RunbookClarification`, `SkillCorrection`, `TemplateCorrection`, `AgentPolicyCorrection`, `PresetFollowUp` mit `Promote` | Der PR-Kontext ist der Delivery-Gate-Satz; Push-Duplikate werden als operatives Rauschen dokumentiert. Abbruch oder Workflow-Unterdrückung bleibt ohne expliziten sicheren Concurrency-Vertrag unzulässig. |
| Der erste Coverage-Aufruf teilte den mehrteiligen Collector-Namen in zwei Argumente und stoppte vor Testausführung. | `PresetFollowUp` mit `ObserveAgain` | Noch keine neue Regel: Repository-Dokumentation und AGENTS nennen die korrekte Quotierung bereits. Feature 023 soll zeigen, ob Command-/Skill-Metadaten eine argv-sichere Darstellung benötigen. |
| Die neutrale Wave-4-DTO-Matrix lief durch echte App-Loops, setzte aber den separaten Primary-Proof-Harness-Marker zunächst nicht. | `PresetFollowUp` mit `ObserveAgain` | Der Test wurde lokal mit einer konkreten View-Tree-App-Loop-Assertion korrigiert. Eine generische Harness-API-Änderung wartet auf einen zweiten unabhängigen Fund. |
| Copilot war erneut wegen Nutzerquota nicht verfügbar; Claude, Pflichtchecks und null GraphQL-Threads ließen nur Human Approval offen. | `NoPromotion` | Die bestehende Review- und enge Admin-Bypass-Grenze funktionierte bei Feature- und Closeout-PR unverändert. |

### Nächster Prüfschritt / Next Check

Feature 023 prüft, ob Closeout weiterhin ohne Selbstreferenz in einem Commit
bleibt und doppelte Workflow-Sätze korrekt klassifiziert werden. Coverage-argv
und Primary-Proof-Marker bleiben Beobachtungen, bis ein zweiter Feldlauf ihre
Verallgemeinerung rechtfertigt.

Feature 023 verifies that closeout remains one commit without self-reference
and that duplicate workflow sets are classified correctly. Coverage argv and
the primary-proof marker remain observations until a second field run justifies
promotion.

## 023 A11Y Framework

**Feature:** `023-a11y-framework`
**Feature-PR:** [#56](https://github.com/hindermath/TuiVision/pull/56)
**Closeout-PR:** [#57](https://github.com/hindermath/TuiVision/pull/57)

| Beobachtung | Entscheidung | Umsetzung oder Grenze |
|---|---|---|
| Ein Shell-Schritt führte zwei explizite `dotnet test`-Aufrufe nach nur einer Build-Zählererhöhung aus. Beide Tests waren fachlich korrekt, die Versions-Evidence verletzte aber den Repository-Vertrag. | `RunbookClarification`, `SkillCorrection`, `TemplateCorrection`, `AgentPolicyCorrection`, `PresetFollowUp` mit `Promote` | Eine Erhöhung autorisiert jetzt genau einen expliziten Build- oder Testaufruf. Fachlich zusammengehörige Tests werden innerhalb dieses einen Aufrufs gebündelt; Shell-Verkettung mehrerer Aufrufe ist unzulässig. |
| `check-homogeneity.ps1` und die Bash-Variante fanden ihre `scripts/lib/hg-*`-Helfer nicht, meldeten Fehler im Fehlerkanal, liefen aber mit Exitcode 0 weiter. Der PowerShell-Aufruf verwendete außerdem den HOME-Default statt eines expliziten Repository-Roots. | `ValidationAutomation`, `SkillCorrection`, `TemplateCorrection`, `AgentPolicyCorrection`, `PresetFollowUp` mit `Promote` | Beide Wrapper brechen bei fehlenden Helfern jetzt mit Exitcode 2 ab. Der autonome Vertrag verlangt expliziten Repo-Root sowie Prüfung von Exitcode und Fehlerkanal; Exitcode 0 darf ErrorRecords oder `command not found` nicht verdecken. Die vollständigen Helfer bleiben Paket-/Deployment-Eigentum der Home Baseline. |
| Der A11Y-Referenz-Slice brauchte für Popup-Menüs eine eigene Auswahlrolle, damit High Contrast die Auswahl semantisch ersetzt, während die historische Default-Palette unverändert bleibt. | `FeatureSpecific` | Die Rolle und Regressionstests bleiben im Feature. Daraus entsteht keine allgemeine autonome Ablaufregel. |
| Der Closeout blieb erneut genau ein Evidence-Commit ohne eigene PR-URL, Reviewed-Head-Aussage oder Merge-Commit in seiner Datei. Push- und PR-Kontexte liefen doppelt; der PR-Kontext blieb der Delivery-Gate-Satz. | `NoPromotion` | Die in 020 bis 022 gehärteten Closeout- und Workflow-Duplikatregeln sind erneut bestätigt. |
| Copilot war wegen Nutzerquota nicht verfügbar; Claude, Pflichtchecks und null GraphQL-Threads ließen ausschließlich Human Approval offen. | `NoPromotion` | Der enge, ausdrücklich autorisierte Admin-Bypass blieb auf diese eine Branch-Protection-Regel begrenzt. |

### Paketübergabe / Package Handoff

Die beiden promovierten Regeln werden mit Skill-, Runbook-, Tasks-, Evidence-
und Agent-Metadaten an das Home-Baseline-Workitem `023-a11y-framework`
übergeben. Der Homogeneity-Deploymentvertrag muss dort zusätzlich beweisen,
dass Wrapper und Helper-Bibliotheken gemeinsam installiert werden und dass
Bash und PowerShell bei fehlenden Helfern identisch fail-closed reagieren.

The two promoted rules are handed to the Home Baseline work item
`023-a11y-framework` together with skill, runbook, tasks, evidence, and agent
metadata. The homogeneity deployment contract must also prove that wrappers
and helper libraries are installed together and that Bash and PowerShell both
fail closed when helpers are missing.

## 024 TV203 and Free Vision Conformance Audit

**Feature:** `024-tv203-freevision-conformance-audit`
**Feature-PR:** [#62](https://github.com/hindermath/TuiVision/pull/62)
**Closeout-PR:** [#63](https://github.com/hindermath/TuiVision/pull/63)

| Beobachtung | Entscheidung | Umsetzung oder Grenze |
|---|---|---|
| Der Home-Baseline-PowerShell-Scanner lieferte trotz 108 `PropertyNotFoundException`-Zeilen Exitcode 0 und nominales JSON. Ursache waren skalare oder leere `Select-String`-Resultate, deren `.Count` direkt gelesen wurde. | `ValidationAutomation`, `PresetFollowUp` mit `Promote` | Home-Baseline-Commit `db2bd86` normalisiert die Resultate array-sicher. Zero-/One-/Many-Fixtures sowie der vollständige PowerShell- und Bash-Lauf liefern parsebares JSON und einen leeren Fehlerkanal. Die portable Preset-Regel war bereits korrekt und wird nicht erweitert. |
| Das Audit erzeugte weder `BehavioralDrift` noch `EvidenceGap`; die Finding-Mengen für 025 und 026 blieben leer. | `NoPromotion` | Die No-empty-work-Regel verhinderte zwei inhaltslose Features und PRs. Nur das verpflichtende Closure-Feature 027 wird fortgesetzt. |
| Feature- und Closeout-PR hielten alle technischen Gates ein; Copilot blieb ein Quota-bedingt fehlender Review, GraphQL meldete null Threads und nur Human Approval erforderte den eng autorisierten Bypass. | `NoPromotion` | Berechtigungs-, Reviewer- und Bypass-Regeln funktionieren unverändert und werden nicht ausgeweitet. |
| Der Closeout blieb ein einzelner Evidence-Commit ohne Selbstreferenz; doppelte Push-/PR-Workflow-Sätze wurden nicht abgebrochen. | `NoPromotion` | Die bereits promovierten Closeout- und Duplicate-Run-Regeln sind erneut bestätigt. |

### Nächster Prüfschritt / Next Check

Feature 027 prüft, ob der korrigierte Home-Baseline-Helfer im nächsten
autonomen Preflight ohne Error Record bleibt und ob dem Preset tatsächlich
Command-, Checklist- oder Template-Sprache fehlt. Nur dann entsteht eine neue
Preset-Version oder ein weiterer Upstream-Beitrag.

Feature 027 rechecks the corrected Home-Baseline helper during autonomous
preflight and determines whether command, checklist, or template language is
actually missing from the preset. Only such evidence justifies a new preset
version or another upstream contribution.

## 027 Pre-Wave-5 Conformance Closure

**Feature:** `027-pre-wave5-conformance-closure`

**Feature-PR:** [#66](https://github.com/hindermath/TuiVision/pull/66)

| Beobachtung | Entscheidung | Umsetzung oder Grenze |
|---|---|---|
| Der in 024 korrigierte Home-Baseline-Scanner liefert in PowerShell und Bash jeweils Exitcode 0, genau ein parsebares JSON-Dokument und einen leeren Fehlerkanal. | `ValidationAutomation`, `PresetFollowUp` mit `NoPromotion` | Die Korrektur ist unabhängig bestätigt. Die portable Preset-Regel war bereits vollständig; Payload und Version bleiben unverändert. |
| Exakte Audit-, Integrations-, Coverage-, Dokumentations-, A11Y-, Security- und Scope-Gates konvergierten ohne Produktänderung. | `NoPromotion` | Bestehende Readiness-, Evidence- und Stop-Grenzen waren ausreichend. Es fehlt kein Command-, Skill-, Template-, Checklist- oder Skriptvertrag. |
| Leere Findings hielten 025 und 026 unterdrückt; nur der verpflichtende 027-Abschluss wurde geliefert. | `NoPromotion` | Die No-empty-work-Regel vermeidet weiterhin inhaltslose Branches und Pull Requests. |
| Alle technischen PR-Checks und Claude waren grün, GraphQL meldete null Threads, Copilot blieb quota-bedingt nicht verfügbar und nur Human Approval blockierte. | `NoPromotion` | Der genehmigte Admin-Bypass blieb auf genau diese eine Branch-Protection-Regel begrenzt. |
| Die Remote-Fakten werden in einem nicht rekursiven Evidence-Closeout dokumentiert. | `NoPromotion` | Der Closeout schreibt weder seine eigene PR-URL noch seinen eigenen Merge zurück und bleibt dadurch single-commit-fähig. |
| Der erste Closeout-Head änderte den Gate-Marker von `Blocked` auf `Eligible`, während ein ausführbarer Audit-Test noch den Vorzustand verlangte. Lokale Docs-/A11Y-Prüfung erkannte diese Kopplung nicht; Linux und macOS CI stoppten korrekt. | `ValidationAutomation`, `PresetFollowUp` mit `Promote` | Status- oder Evidence-only Änderungen müssen vor dem Skip von Runtime-Tests nach bestehenden Validatoren suchen, die geänderte Marker oder Dateien lesen. Betroffene Validatoren werden im selben Commit aktualisiert und gezielt ausgeführt. |

### Abschluss / Closure

Feature 027 bestätigt die Kernverträge, findet aber eine portable
Validation-Trigger-Lücke für statuslesende ausführbare Tests. Das
Home-Baseline-PR #60 liefert diesen Follow-up als öffentliches Preset v0.1.1;
der versionierte GitHub-ZIP ist geprüft und Issue `github/spec-kit#3479`
enthält den nicht blockierenden Upstream-Hinweis. Wave 5 ist der nächste Intake.

Feature 027 validates the core contracts but finds one portable validation
trigger gap for executable tests that read status evidence. Home-Baseline PR
#60 delivers the follow-up as public preset v0.1.1, verified through its
versioned GitHub ZIP. Issue `github/spec-kit#3479` carries the non-blocking
upstream update. Wave 5 is the next domain intake.
