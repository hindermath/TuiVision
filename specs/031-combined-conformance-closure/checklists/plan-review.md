# Plan Review Checklist: Feature 031

**Purpose**: Führt eine umsetzungsnahe Review der Planartefakte durch und gibt
für jeden Punkt einen konkreten Durchführungshinweis.
**Created**: 2026-07-16
**Feature**: [plan.md](../plan.md)

## Datenfluss und Referenz-Slice / Data Flow and Reference Slice

- [x] PR001 Sind alle Eingabedateien aus 024, 025, 026, 028, 029 und 030
  konkret benannt?
  - Durchführungshinweis: Vergleiche die Binding-Input-Tabelle mit Lastenheft
    und Spec FR-001; ergänze fehlende strukturierte oder finale Evidence-Pfade.
- [x] PR002 Ist `C001/W5-001/TGO001/MB001/F001` als vollständiger
  Referenz-Slice definiert?
  - Durchführungshinweis: Prüfe, ob Hash, Relation, Proof, Consumer,
    Disposition, Finding-Closure und negative Duplikatgrenze zusammen geplant
    sind.
- [x] PR003 Ist die Ausweitung vom Referenz-Slice auf alle geschlossenen Mengen
  deterministisch?
  - Durchführungshinweis: Prüfe ID-Reihenfolge, exakte erwartete Mengen und
    das Verbot unbekannter Zusatzzeilen im Datenmodell.
- [x] PR004 Werden Vorgängerhashes vor der Nutzung ihrer Inhalte geprüft?
  - Durchführungshinweis: Verfolge `AcceptedInput` bis zur Teststrategie und
    stelle sicher, dass Hashfehler vor semantischer Acceptance stoppen.

## Validator und Negative Matrix / Validator and Negative Matrix

- [x] PR005 Ist die neue Testklasse klar vom Produktcode getrennt?
  - Durchführungshinweis: Prüfe Projektpfad, Dependencies und Source-Diff-
    Firewall.
- [x] PR006 Sind alle öffentlichen Testmethoden und nicht trivialen
  Validatorblöcke für XML- beziehungsweise didaktische Kommentare eingeplant?
  - Durchführungshinweis: Ergänze den Compile-Surface-Check vor dem ersten
    Red-Lauf und die moderate Kommentarregel.
- [x] PR007 Prüft die negative Matrix jede relevante geschlossene Menge?
  - Durchführungshinweis: Simuliere fehlenden Contract, doppelten Consumer,
    unbekannte Observation, injiziertes Finding, nicht leeren Owner,
    Hardening-Intake und verfrühte Wave-Freigabe.
- [x] PR008 Bleibt externe Netzwerkverfügbarkeit aus der deterministischen
  Unit-Test-Grenze heraus?
  - Durchführungshinweis: Trenne lokalen Git-/Hash-Proof von CI-Manifest- und
    Schema-Proof.
- [x] PR009 Werden bestehende Auditvalidatoren wiederverwendet, ohne private
  Helper breit zu kopieren?
  - Durchführungshinweis: Prüfe den geplanten gemeinsamen Filter und begrenze
    die neue Klasse auf 031-Cross-File-Beziehungen.

## Wave-Zustand und Marker / Wave State and Markers

- [x] PR010 Ist der Feature-Head-Zustand eindeutig blockiert?
  - Durchführungshinweis: Suche in Plan, Contract, Quickstart und Gate-Datei
    nach vorzeitigem `Eligible`.
- [x] PR011 Ist der Post-Merge-Zustand eindeutig `Eligible` beziehungsweise
  `ConditionallyReady`?
  - Durchführungshinweis: Vergleiche Lastenheft, Spec FR-025/026 und
    WaveTransition.
- [x] PR012 Ist die Marker-Consumer-Suche eine explizite Aufgabe vor dem
  Closeout?
  - Durchführungshinweis: Plane `rg` über Tests, Pflichtenheft, Reihenfolge,
    fünf Agentenflächen, Gate- und Evidence-Dateien.
- [x] PR013 Akzeptieren Tests den Post-Merge-Zustand nur mit kausaler
  Closeout-Evidence?
  - Durchführungshinweis: Ergänze einen dualen Testvertrag: ohne Closeout
    blocked; mit vollständigem Closeout exakt Eligible/ConditionallyReady.
- [x] PR014 Verändert der Closeout keine Runtime-, API-, Dependency- oder
  Testlogik?
  - Durchführungshinweis: Begrenze Closeout-Pfade auf Evidence, State, Tasks,
    Marker, Agenten, Statistik und Archiv.

## Governance und Agent-Parität / Governance and Agent Parity

- [x] PR015 Sind alle sieben Preset-Versionen korrekt?
  - Durchführungshinweis: Vergleiche Plan und Spec mit `specify preset list`.
- [x] PR016 Sind ASVS, Supply Chain, AI-SBOM, Regulierung, Zero Trust, SAMM,
  BSI C3A und BSI C5 triggerbasiert disponiert?
  - Durchführungshinweis: Prüfe je `N/A` Begründung und Re-Evaluationsauslöser.
- [x] PR017 Werden alle fünf Agentenflächen gemeinsam aktualisiert?
  - Durchführungshinweis: Vergleiche Planstruktur und Agent-Policy; ergänze die
    root Copilot-Datei, die das Standard-Update-Skript nicht bearbeitet.
- [x] PR018 Bleiben `.specify/templates/` und Presets ohne portable Defekt-
  Evidence unverändert?
  - Durchführungshinweis: Prüfe Scope-Firewall und Retrospektiventscheidung.

## Gates, Version und Remote-Abschluss / Gates, Version, and Remote Closeout

- [x] PR019 Decken die Gate-Requirements jeden bindenden Lastenheft-Nachweis
  ab?
  - Durchführungshinweis: Mappe Requirements 1-9 auf stabile `G031-*`-IDs.
- [x] PR020 Enthält jedes anwendbare Gate konkrete Command-Tokens?
  - Durchführungshinweis: Prüfe, dass leere Tokens nur bei begründetem
    Provider-Fact-`N/A` vorkommen.
- [x] PR021 Ist die Branch-Versionierung vor jedem Build/Test sowie vor
  Commit/Push explizit?
  - Durchführungshinweis: Ergänze `1.31.<patch>.<build>` in Plan und Tasks-
    Vorgaben; ein Counter deckt genau einen Befehl.
- [x] PR022 Ist die Exact-Head-Evidence temporär und selbst nicht Teil des
  Kandidaten?
  - Durchführungshinweis: Prüfe Plan, Gate-Contract und Scope-Scan.
- [x] PR023 Ist Review-Konvergenz getrennt von technischem Gate-Proof?
  - Durchführungshinweis: Prüfe GraphQL-Threads, Kommentare, Claude/Copilot
    und fehlende Reviewer als eigene Provider-Fakten.
- [x] PR024 Ist der Human-Approval-only Bypass eng und nachvollziehbar?
  - Durchführungshinweis: Verlange grüne technische Gates, null umsetzbare
    Threads und keine andere offene Regel.
- [x] PR025 Endet der Lauf ohne Start eines Folgefeatures?
  - Durchführungshinweis: Prüfe Tasks und Abschlussvertrag auf `main`-Sync,
    Wave-Marker und ausdrücklich keinen Feature-032-Branch.

## Notes

- Jeder Durchführungshinweis wird vor `/speckit-tasks` ausgeführt.
- Ein offener Punkt mit Plan- oder Acceptance-Auswirkung blockiert Tasks.
- Ergebnis 2026-07-16: 25/25 Durchführungshinweise abgeschlossen. Alle fünf
  Agentenflächen enthalten denselben aktiven 031-Kontext; der Feature-Validator
  wird vor Merge closeout-fähig und der Closeout bleibt evidence-only.
