# Schnellstart für die spätere Umsetzung / Quickstart for Later Implementation

Dieses Dokument beschreibt die geplante Reihenfolge nach einer erfolgreich
abgeschlossenen Plan-Review-, Tasks- und Analyze-Phase. Es führt jetzt keinen
Befehl aus und erteilt keine Commit-, Push-, PR-, Merge- oder
Folgefeature-Autorität.

*This document describes the planned order after successful plan review, tasks,
and analyze phases. It executes no command now and grants no commit, push, pull
request, merge, or follow-up-feature authority.*

## 1. Eintritt prüfen / Verify entry

1. Branch muss `038-example-portfolio-conformance-audit` sein.
2. Der Autonomous-State muss die aktuell revalidierte `MergeAndSync`-Autorität
   und unveränderte Metadaten abgeschlossener Routing-Phasen enthalten; nur die
   aktive Wiederholungsphase darf fortgeschrieben werden.
3. Die vier akzeptierten Input-Hashes müssen mit dem State übereinstimmen.
4. Feature 037 muss weiterhin `Closed`/`Eligible` mit null Candidate Findings
   und null Product Decisions belegen.
5. Die direkte Projektmenge muss exakt den 37 Zeilen im Abnahmevertrag
   entsprechen. Zwei Projekte unter `examples/Shared/` sind keine
   Portfoliozeilen.
6. Jeder Drift stoppt vor einer Änderung.

*Verify branch, authority, accepted hashes, Feature 037 closure, and the exact
37-project population. Any drift stops before changes.*

## 2. Evidence zuerst anlegen / Create evidence first

Vor dem ersten Validator-Edit müssen folgende Flächen existieren:

- `example-portfolio-audit.json` als parsefähiges, absichtlich noch nicht
  abnahmegültiges `NotAssessed`-Skelett;
- neun fachliche Markdown-Projektionen plus `pr-evidence.md`, zusammen exakt
  zehn Markdown-Evidence-Familien;
- `pr-evidence.md` mit Authority, Hashes, Protected Roots und
  `Not Assessed`-Gates;
- das reviewte `autonomous-gate-requirements.json`;
- die spätere Fixture-Struktur.

Zu diesem Zeitpunkt darf keine Evidence einen bestandenen Build, Test, Remote-
oder Closure-Fakt behaupten.

*Before the first validator edit, create the dataset skeleton, all readable
evidence families, local run evidence, gate requirements, and fixture layout.
No evidence may claim a passing build, test, remote, or closure fact yet.*

## 3. Build-Counter vor jedem Build/Test / Build counter before every build/test

Vor jedem einzelnen späteren `dotnet build` oder `dotnet test`:

1. ermittle den aktuellen Commit-Count des Feature-Branches;
2. erhöhe den manuellen Build-Counter genau einmal;
3. setze `Version`, `AssemblyVersion` und `FileVersion` gemeinsam auf
   `1.38.<CommitCount>.<Build>`;
4. prüfe, dass ausschließlich die Versionszeile als erlaubte
   `Directory.Build.props`-Änderung entsteht;
5. starte erst dann den einen Build- oder Testbefehl.

Ein `dotnet test`, das implizit baut, braucht keinen zweiten Counter-Schritt.
Restore, Format, DocFX, NPM und reine Scans erhöhen den Counter nicht.

## 4. Repräsentativen Slice rot/grün liefern / Deliver the representative slice red/green

1. Prüfe, dass die komplette Testassembly kompilieren kann.
2. Starte den fokussierten `EX036 Tp7FileManager`-Akzeptanztest. Erwartetes Rot
   ist ausschließlich eine fehlende oder unvollständige Auditzeile.
3. Ergänze Source- und Evidence-Relationen, historische Absicht, Lernziel,
   sichtbare Bedienung, Frameworkentscheidung, Proof, Dokumentation, A11Y,
   Plattform, Disposition, Review, Risiko und Trigger.
4. Führe den fokussierten Test erneut aus. Er muss einschließlich aller
   Gegenrelationen grün werden.
5. Stoppe bei einem realen Finding, einer Product Decision, unklarer Ownership
   oder geschütztem Pfaddelta. Keine Produktkorrektur ist erlaubt.

*Prove the complete `EX036 Tp7FileManager` slice with a semantic red followed by
green. Stop on a real finding, product decision, unclear ownership, or protected
path change; do not remediate product behavior.*

## 5. Validator und Fixtures verbreitern / Broaden validator and fixtures

Führe die Fixture-Kategorien in dieser Reihenfolge ein:

1. JSON/Schema/Baseline;
2. Inventar und Rollen;
3. Source-/Evidence-Reziprozität;
4. Status, `N/A`, Frameworkentscheidung und Disposition;
5. `EF001+`, Deduplication und Primary Owner;
6. Owner-DAG, nicht leere Intakes und genau ein Closure;
7. Governance, Scope und unerlaubte Remote-/Konformitätsclaims.

Jede Fixture verletzt nur eine Primärinvariante und erwartet den im
Validatorvertrag benannten stabilen `EPA###`-Code.

## 6. 37 Zeilen waveweise prüfen / Review 37 rows wave by wave

Nach grünem Vertikalschnitt wird in folgender Reihenfolge gearbeitet:

1. `EX001`–`EX004` Wave 1;
2. `EX005`–`EX015` Wave 2;
3. `EX016`–`EX020` Wave 3;
4. `EX021`–`EX025` Wave 4;
5. `EX026`–`EX035` Wave 5;
6. `EX037 A11yFramework` als `SupplementalControl`.

Nach jeder Gruppe müssen Mengen-, Source-, Evidence-, Decision- und
Markdown-Projektionen konsistent sein. `EX036` bleibt der bereits akzeptierte
Slice und wird nicht neu erfunden.

*Review each wave in fixed ID order. After every group, cardinality, source,
evidence, decision, and Markdown projections must agree.*

## 7. Findings einfrieren / Freeze findings

1. Verwerfe Stil- oder Strukturbeobachtungen ohne reproduzierbare Lücke.
2. Gruppiere echte Gaps zuerst nach Root Cause.
3. Weise genau einen Primary Owner anhand der Ursachenregel zu.
4. Sortiere Owner und Deduplication Keys deterministisch.
5. Vergib erst jetzt lückenlos `EF001+`.
6. Prüfe reziproke Example-/Finding-Relationen, Red-/Green-Anforderungen,
   Risiko, Dependencies, Review und Trigger.
7. Stoppe bei nicht reproduzierbarem Finding oder unklarer Ownership.

## 8. Handoff und Closure erzeugen / Create handoff and closure

- Erzeuge genau einen unnummerierten Remediation-Intake je nicht leerer
  Owner-Gruppe.
- Dokumentiere leere Gruppen als `Suppressed`, ohne Datei.
- Leite für jede Finding-Abhängigkeit A setzt B voraus die Cross-Owner-Kante
  `Owner(B) -> Owner(A)` ab; Same-Owner-Abhängigkeiten bleiben intern.
- Sortiere die emittierten Intakes topologisch, nutze bei Gleichstand die feste
  Owner-Reihenfolge und lehne Zyklen ab.
- Erzeuge danach exakt einen unnummerierten Closure-Intake als letzten Knoten.
- Starte keinen Intake, Branch oder Folgefeature.

*Emit one unnumbered intake per non-empty owner group, suppress empty groups,
order the emitted nodes topologically, and append exactly one unnumbered
independent closure. Start none of them.*

## 9. Lokale Validierungsleiter / Local validation ladder

Nach Implementierungs-, Tasks- und Analyze-Konvergenz ist die geplante Leiter:

```text
1. specify/checklist/analyze convergence
2. git diff --check and protected-root/API/dependency scans
3. secret and dependency status
4. twelve-preset and model-routing checks
5. dotnet format --verify-no-changes
6. focused ExamplePortfolioAuditIntegrityTests
7. complete TuiVision.Examples.SmokeTests Release suite
8. complete TuiVision.sln Release suite
9. canonical five-assembly Coverlet gate
10. project statistics
11. DocFX
12. Playwright/Axe
13. UTF-8 Lynx text review
14. generated-output, agent-parity, and final governance status
15. exact-head delivery, review, merge, and main synchronization
```

Vor jedem Schritt 6 bis 9 mit `dotnet build` oder `dotnet test` gilt ein eigener
Build-Counter-Schritt. Tatsächlich ausgeführte Commands, Exitcode und relevante
Fehlerausgabe werden in `pr-evidence.md` festgehalten. Ein übersprungenes Gate
braucht `N/A`, Begründung und Trigger.

## 10. Geschützte Wurzeln prüfen / Check protected roots

Der Abschlussdiff muss null Änderungen an folgenden Flächen zeigen:

```text
src/
examples/
tv203s/
TVDEMOS/
TVFM/
*.csproj und *.sln
NuGet-/Dependency-/Lock-Dateien
Public-API- und XML-Dokumentationsflächen
externe Checkouts und generierter DocFX-Output
```

`Directory.Build.props` darf nur die vorgeschriebene, ausgerichtete Version
enthalten. Der test-only Validator, kontrollierte Fixtures, Feature-Evidence,
bedingte Intake-Ausgaben und Abschlussstatistik sind die einzigen geplanten
fachlichen Schreibflächen.

## 11. Sichere Stop- und Resume-Grenzen / Safe stop and resume boundaries

Sichere Stopps liegen nach Plan, Vertikalschnitt, jeder Wave-Gruppe,
Finding-Freeze, Handoff-Freeze und lokaler Validierung. Bei `PausedByUser` ist
explizites Resume nötig. Eine unerwartete Unterbrechung setzt
`NeedsRevalidation`; Hashes, HEAD, Diff, Scope, Routing, Counter und letzter
Gatezustand werden erneut geprüft, bevor Arbeit fortgesetzt wird.

*Safe stops exist after plan, vertical slice, each wave group, finding freeze,
handoff freeze, and local validation. Deliberate pause requires explicit resume;
unexpected interruption requires complete revalidation.*

## 12. Delivery-Abschluss / Delivery completion

Der Auditstatus darf nur `AuditCompleteNoFindings` oder
`AuditCompleteWithRemediation` sein. Delivery endet erst nach Exact-Head-
Evidence, Review-Konvergenz, Merge, Branchbereinigung und sauberem
`main == origin/main`. Vollständige Portfolio-Konformität und der Start eines
Folgefeatures bleiben unautorisiert.

*Delivery may state only that the audit, handoff, and authorized feature
delivery are complete. It cannot claim full portfolio conformance or a started
follow-up feature.*
