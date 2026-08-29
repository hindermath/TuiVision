# Feature Specification: Transactional Form Model

**Branch**: `042-transactional-form-model`

**Status**: Accepted for local implementation

**Input**: `requirements/intakes/active/Lastenheft_Transactional-Form-Model.md`
**Review**: Series review `339a343c-4973-4c86-a6f9-03ae6290a210`

## Purpose / Zweck

Feature 042 liefert Issue #154 vollständig als optionale, additive
Transaktionsschicht. Felder und verschachtelte Sessions verwalten Baseline,
Dirty-State, Change-Sets, sync/async Submit-Validierung, typsicheres Binding,
kultur-explizite Konverter und sichere deklarative Formsemantik. Existing
Views, Controls und Eventpfade bleiben kompatibel.

## User stories

### US1 – Werte als lokale Transaktion bearbeiten (P1)

Eine Anwendung bearbeitet mehrere Felder, sieht exakt die Abweichungen und kann
alle Änderungen gemeinsam akzeptieren oder verwerfen.

**Independent test**: Felder und Parent/Child-Session ohne UI ändern; Dirty,
Change-Set, Accept und Reject einschließlich benutzerdefinierter Equality
prüfen.

### US2 – Erst nach Persistenz binden (P1)

Eine Anwendung validiert und persistiert ein unveränderliches Change-Set,
bevor `AcceptChanges()` typsichere Setter ausführt und Baselines verschiebt.

**Independent test**: Submit lässt POCO/Baseline unverändert; Accept wendet
Bindings an; Setterfehler rollen frühere Setter rückwärts zurück.

### US3 – Langsame Prüfungen sicher ausführen (P1)

Submit-time Async-Validatoren prüfen einen Snapshot. Cancellation,
Parallelaufruf und Änderungen während der Prüfung ergeben eindeutige Zustände.

**Independent test**: kontrollierte Tasks beweisen Erfolg, Fehler,
Cancellation, Concurrent-Submit-Ablehnung und `Stale` bei Revisionsdrift.

### US4 – Formsemantik sicher laden (P2)

Eine Anwendung lädt eine versionierte JSON-Definition, deren Schlüssel nur
über eine vertrauenswürdige Registry aufgelöst werden.

**Independent test**: Roundtrip und gültige Registry bestehen; Version,
Unknown Key/Property, Duplikat, Referenz, Typkonflikt, Größe, Tiefe und Zyklus
werden ohne partielles Modell abgelehnt.

### US5 – Sichtbaren Kundenworkflow lernen (P2)

Lernende bedienen `FormTransaction` durch den echten App-Loop und beobachten
Dirty, Validierung, Persistenz, Accept/Reject, Cancellation und Stale-Status.

**Independent test**: injizierte Commands laufen durch `app.Run()` und belegen
Zustand, View-Tree, sichtbare Cells, StatusLine und Help Description.

## Requirements

- **FR-001**: Öffentliche `IFormField`, `IFormField<T>`, `FormField<T>` und
  `FormSession` implementieren Phase 1 und 2 vollständig.
- **FR-002**: Dirty-State folgt Baseline plus expliziter Equality; Change-Sets
  sind unveränderlich und stabil geordnet.
- **FR-003**: Sync- und ausschließlich submit-time Async-Validatoren liefern
  frameworkneutrale Fehler; Submit verändert POCO und Baseline nicht.
- **FR-004**: POCO-Binding verwendet direkte Property-Ausdrücke. Bidirektionale
  Konverter erhalten eine explizite Kultur.
- **FR-005**: `AcceptChanges()` führt Setter atomar bestmöglich aus, rollt bei
  Fehler rückwärts zurück und verschiebt erst danach alle Baselines.
- **FR-006**: Parent und Child-Sessions sind eine rekursiv atomare Grenze;
  Zyklen und Mehrfachbesitz werden abgelehnt.
- **FR-007**: Ein Submit validiert einen stabilen Snapshot. Drift liefert
  `Stale`; Cancellation wird propagiert; parallele Submits werden abgelehnt.
- **FR-008**: `TInputLine`-Integration erfolgt additiv über einen Adapter;
  nichttransaktionale Controls bleiben unverändert.
- **FR-009**: JSON enthält nur geschlossene Form-/Field-/Control-/Type-/Binding-
  /Converter-/Validator-/Child-Semantik und keine ausführbaren CLR-Inhalte.
- **FR-010**: Parser und Registry lehnen alle im Intake genannten malformed
  Fälle atomar und begrenzt ab.
- **FR-011**: `examples/FormTransaction` erfüllt den sichtbaren, text-first
  Run-Loop- und Help-/Status-Vertrag.
- **FR-012**: Public APIs sind vollständig bilingual dokumentiert; didaktische
  Kommentare erklären Snapshot-, Rollback- und Parsergrenzen.

## Success criteria

- Alle vier Issue-Phasen sind ausführbar und getestet; keine Platzhalter.
- Targeted Unit-/Integration-/Example-Smokes sind grün.
- Voller Release-Build/Test, fünf Coverage-Gates >=70 %, Format, DocFX 0/0,
  Axe 2/2, Lynx und Governance-Parität bestehen.
- Source disposition: `IntentionalTuiVisionDeviation` für die neue
  Transaktionssemantik; `PreserveHistoricalIntent` für Dialog-/Input-
  Kompatibilität; `AdoptModernization` für additive Komposition und sichere
  Registry.

## Non-goals

Kein 5250-Protokoll, kein DDS, keine allgemeine WPF-Binding-Engine, keine
Live-Async-Validierung, keine Datenbank oder Netzwerkverbindung, keine
Reflection- oder Script-Pfade aus JSON und kein Umbau ordinary controls.
