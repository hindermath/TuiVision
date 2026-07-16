# Plan Quality Checklist: Feature 031

**Purpose**: Prüft, ob Plan und Designartefakte den bindenden Closure-Scope
vollständig, testbar und ohne Produktänderung umsetzen.
**Created**: 2026-07-16
**Feature**: [plan.md](../plan.md)

## Architektur und Scope / Architecture and Scope

- [x] PQ001 Ist die Umsetzung vollständig auf Feature-Evidence und test-only
  Validation begrenzt? [Plan §Summary, Scope]
- [x] PQ002 Sind neue Projekte, Packages, Runtime-Abhängigkeiten und
  Produktionscode ausgeschlossen? [Plan §Technical Context]
- [x] PQ003 Sind historische, Consumer- und externe Quellen read-only und
  außerhalb des Kandidaten? [Plan §Constitution Check]
- [x] PQ004 Ist ein reproduziertes Produktproblem als Stop und externer Intake
  statt als lokale Remediation geplant? [Plan §Scope firewall]
- [x] PQ005 Ist `.specify/templates/` ohne portable Defekt-Evidence
  ausgeschlossen? [Spec §FR-030, Plan §Constitution Check]

## Daten- und Evidence-Modell / Data and Evidence Model

- [x] PQ006 Binden `AcceptedInput`-Zeilen jede verwendete Vorgängerdatei durch
  Pfad und SHA-256? [Data Model §2]
- [x] PQ007 Sind 48 Contract-, 13 Consumer-, 96 Observation- und 13
  Finding-Zeilen als geschlossene Mengen modelliert? [Data Model §4-7]
- [x] PQ008 Sind die drei leeren Ownergruppen von der null Finding-/Intake-
  Aussage unterscheidbar? [Data Model §8]
- [x] PQ009 Sind externe Git-, Lizenz-, Manifest- und Source-Hash-Grenzen
  vollständig modelliert? [Data Model §3]
- [x] PQ010 Sind Governance- und Validation-Evidence mit vollständigen
  Reviewfeldern modelliert? [Data Model §9-10]
- [x] PQ011 Sind reziproke Beziehungen zwischen Vorgänger- und Closure-Zeilen
  vorgesehen? [Plan §Closure Dataset]

## Teststrategie / Test Strategy

- [x] PQ012 Beginnt die Umsetzung mit einem repräsentativen Red-Slice?
  [Plan §Phase A]
- [x] PQ013 Bleibt der neue Validator im vorhandenen Drivers-Testprojekt und
  nutzt nur bestehende BCL-/Testabhängigkeiten? [Research §4]
- [x] PQ014 Werden bestehende 024/028/029/030-Validatoren mit dem neuen
  Validator gemeinsam ausgeführt? [Research §5]
- [x] PQ015 Deckt die negative Matrix fehlende, doppelte, unbekannte,
  widersprüchliche, nicht leere Owner- und verfrühte Wave-Zustände ab?
  [Plan §Test-only Validator]
- [x] PQ016 Ist vor dem Red-Lauf die Compile-Oberfläche einschließlich
  öffentlicher XML-Dokumentation der Testmethoden eingeplant? [Gap]
- [x] PQ017 Vermeidet der Plan netzwerkabhängige externe Checkouts als
  Unit-Test-Pflicht und behält dennoch aktuellen lokalen Provenance-Proof?
  [Research §6]

## Delivery und Kausalität / Delivery and Causality

- [x] PQ018 Bleiben beide Waves auf dem Feature-Head gesperrt? [Research §7]
- [x] PQ019 Ist genau ein nicht rekursiver Evidence-Closeout vorgesehen?
  [Research §8]
- [x] PQ020 Ist der Closeout single-commit-capable und frei von Anforderungen
  an seine eigene Remote-Identität? [Contract §Delivery]
- [x] PQ021 Werden alle Marker-Consumer vor dem Closeout gesucht und auf den
  zweistufigen Zustandsvertrag geprüft? [Gap]
- [x] PQ022 Kann der Closure-Validator sowohl den blockierten Feature-Head als
  auch den kausal bewiesenen Post-Merge-Zustand unterscheiden? [Gap]
- [x] PQ023 Ist der Admin-Bypass auf grüne Technik, null Threads und nur Human
  Approval begrenzt? [Plan §Remote closeout]
- [x] PQ024 Werden doppelte Push-/PR-Workflows als Noise behandelt und
  PR-Kontext-Gates bevorzugt? [Autonomous Runbook]

## Validierung und Versionierung / Validation and Versioning

- [x] PQ025 Sind targeted, full Release, Coverage, Format, DocFX/A11Y,
  Security, Scope, Agent-Parität und drei Plattformen eingeplant? [Plan
  §Validation Strategy]
- [x] PQ026 Sind alle fünf Coverage-Assemblies und die 70-Prozent-Grenze
  benannt? [Contract §Validation]
- [x] PQ027 Ist eine Build-Counter-Erhöhung pro einzelnem `dotnet build` oder
  `dotnet test` vorgesehen? [Quickstart §4]
- [x] PQ028 Wird der nummerierte Branch vor Commit und Push auf
  `1.31.<patch>.<build>` ausgerichtet? [Gap]
- [x] PQ029 Sind Exact-Head-Anforderungen als versionierte Gate-Datei vor
  Implementierung vorhanden? [autonomous-gate-requirements.json]
- [x] PQ030 Sind lokale `pwsh`-/Gitleaks-Lücken durch ehrliche Remote-Evidence
  statt Schein-Pass behandelbar? [Plan §Validation Strategy]

## Notes

- Durchführung: Jede Gap-Zeile wird vor Freigabe der Tasks in Plan, Research,
  Contract oder Quickstart korrigiert.
- Ergebnis 2026-07-16: 30/30 erfüllt. Ergänzt wurden Compile-Surface/öffentliche
  XML-Dokumentation, duale Marker-Validierung, explizite Marker-Consumer-Suche
  und vollständige `1.31.<patch>.<build>`-Versionierungsgrenzen.
