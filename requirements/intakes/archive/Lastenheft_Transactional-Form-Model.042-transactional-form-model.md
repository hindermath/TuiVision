<!-- intake-authoring:begin -->
# Lastenheft: Transactional Form Model

**Status:** ReadyForReview

**Quelle / Source:** GitHub Issue #154, „Feature concept: transactional form model inspired by IBM 5250“

**Zielgruppe / Audience:** TuiVision-Anwendungsentwickler, Framework-Maintainer, Reviewer und Lernende / application developers, framework maintainers, reviewers, and learners

**Vorausgesetztes Wissen / Assumed prior knowledge:** Grundkenntnisse in C#, Dialogen und Datenmodellen; Transaktion, Snapshot und Registry werden hier erklärt. / Basic C#, dialog, and data-model knowledge; transaction, snapshot, and registry are explained here.
**Profil / Profile:** `level2-lastenheft`

## Zweck / Purpose

TuiVision erhält eine optionale, additive Formularschicht nach der Idee
strukturierter IBM-5250-Felder: Werte werden lokal bearbeitet, Änderungen
deterministisch erfasst, gemeinsam validiert und erst nach erfolgreicher
externer Persistenz ausdrücklich akzeptiert. Das klassische Event-, View- und
Command-Modell bleibt unverändert die UI-Grundlage.

*TuiVision gains an optional, additive form layer inspired by structured IBM
5250 fields: values are edited locally, changes are tracked deterministically,
validated together, and accepted only after successful external persistence.
The classic event, view, and command model remains the UI foundation.*

## Produktentscheidung / Product decision

Die in Issue #154 skizzierten Phasen 1 bis 4 werden gemeinsam in einem Feature
geliefert. „Gemeinsam“ bedeutet eine zusammenhängende, abgenommene
Vertragsoberfläche; es erlaubt eine interne Implementierungsreihenfolge von
Kernfeldern über Binding und Async-Validierung bis zur deklarativen Semantik.
Keine Phase darf als nicht implementierter Platzhalter enden.

*All four phases proposed in Issue #154 are delivered as one accepted feature.
They may be implemented incrementally inside that feature, but no phase may
remain a placeholder.*

## Architektur und Begriffe / Architecture and terms

- Ein **Formularfeld** besitzt aktuellen Wert, Baseline, Gleichheitsregel,
  Dirty-State und frameworkneutrale Validierungsfehler.
- Eine **FormSession** ist die atomare Grenze für Felder und Child-Sessions.
- Ein **Change-Set** ist eine unveränderliche Liste der von der Baseline
  abweichenden Werte.
- Ein **Snapshot** ist der stabile, versionierte Wertebestand, gegen den ein
  Submit-Lauf prüft. Spätere Änderungen gehören nicht zu diesem Lauf.
- Eine **Runtime-Registry** ordnet freigegebene, harmlose Schlüssel explizit
  zu Feldtypen, Control-Fabriken, Bindings, Konvertern und Validatoren zu.
  Unbekannte Schlüssel führen zu einem atomaren Fehler.

*A form field owns current and baseline values. A FormSession is the atomic
boundary. A change set is immutable, a snapshot freezes one submit attempt,
and the runtime registry resolves only explicitly approved safe keys.*

## Quellenentscheidung / Source-reference decision

Issue #154 und dieses Lastenheft bestimmen die neue Transaktionssemantik.
Weder Magiblot noch `tv203s` besitzen dafür normative Formverträge. Für die
Integration mit `TDialog`, `TInputLine`, Fokus, Events und Commands gilt die
Quellenpolicy: aktuellen TuiVision-Vertrag lesen; Magiblot am freigegebenen Pin
zuerst für moderne Integrationsideen prüfen; `tv203s` danach für historische
Kompatibilitätskontrolle prüfen. Die Formtransaktion wird als
`IntentionalTuiVisionDeviation` beziehungsweise TuiVision-spezifische
Erweiterung dokumentiert.

*Issue #154 and this intake define the transaction semantics. Magiblot is
reviewed first for modern integration ideas and `tv203s` afterwards for
historical compatibility. The form transaction itself is a documented
TuiVision-specific extension.*

## Funktionale Anforderungen / Functional requirements

### Phase 1 – Felder und Baseline

- **FR-001:** Die öffentliche API MUSS `IFormField`, `IFormField<T>` und
  `FormField<T>` mit stabilem Namen, typisiertem Wert, Baseline,
  `IsModified`, Validierungszustand, `AcceptChanges()` und `RejectChanges()`
  bereitstellen.
- **FR-002:** `IsModified` MUSS aus aktueller Baseline, aktuellem Wert und
  einem explizit festlegbaren `IEqualityComparer<T>` deterministisch folgen.
  Die Rückkehr zum Baseline-Wert MUSS Dirty-State ohne Sonderflag löschen.
- **FR-003:** `RejectChanges()` MUSS den Feldwert auf die unveränderte Baseline
  zurücksetzen. `AcceptChanges()` MUSS den aktuellen Wert erst nach erfolgreichem
  Apply zur neuen Baseline machen.
- **FR-004:** `FormSession` MUSS Felder in stabiler Einfügereihenfolge
  verwalten, eindeutige Namen erzwingen, Dirty-/Validierungszustand aggregieren
  und ein unveränderliches `FormChangeSet` erzeugen.

### Phase 2 – Validierung und Submit

- **FR-005:** Synchrone Validatoren MÜSSEN frameworkneutrale, text-first
  Fehlerobjekte liefern und ausschließlich gegen den übergebenen Wert oder
  Session-Snapshot arbeiten.
- **FR-006:** Validierung und Change-Set-Erstellung MÜSSEN ohne Rendering,
  Terminal oder Event-Loop testbar sein.
- **FR-007:** Ein Submit MUSS alle Felder und Child-Sessions rekursiv prüfen.
  Fehler verhindern Erfolg; weder POCO noch Baseline werden dabei verändert.
- **FR-008:** Gewöhnliche Controls und bestehende Event-/Command-Pfade DÜRFEN
  ohne FormSession weder neues Verhalten noch neue Pflichtkonfiguration
  erhalten.

### Phase 3 – typsicheres Binding und Konverter

- **FR-009:** POCO-Binding MUSS über direkte Property-Ausdrücke wie
  `model => model.Name` typ- und refactoringsicher eingerichtet werden. String-
  Property-Pfade und frei ausgeführte Reflection sind nicht Teil der API.
- **FR-010:** Binding liest den Startwert in das Feld, aktualisiert das Modell
  aber erst durch `AcceptChanges()` nach erfolgreicher externer Persistenz.
  `SubmitAsync()` selbst DARF kein Modell verändern.
- **FR-011:** Bidirektionale Konverter MÜSSEN ihren Kulturkontext explizit
  erhalten. Implizite Current-Culture-Abhängigkeit und stiller Fallback sind
  verboten; Konvertierungsfehler werden als Validierungsfehler sichtbar.
- **FR-012:** Wenn ein Modell-Setter beim Accept fehlschlägt, MUSS die Session
  bereits ausgeführte Setter bestmöglich in umgekehrter Reihenfolge
  zurückrollen. Baselines bleiben unverändert. Ein Ergebnis oder eine
  Ausnahme MUSS die Vertragsgrenze möglicher Setter-Nebeneffekte benennen.

### Phase 4 – Async, Child-Sessions und deklarative Semantik

- **FR-013:** Asynchrone Validatoren DÜRFEN ausschließlich submit-time laufen.
  `SubmitAsync()` MUSS `CancellationToken` akzeptieren und synchrone sowie
  asynchrone Fehler deterministisch zusammenführen.
- **FR-014:** `SubmitAsync()` MUSS einen stabilen Snapshot validieren. Wird
  währenddessen ein Wert geändert, MUSS das Ergebnis als veraltet markiert
  werden und DARF nicht als persistierbare Freigabe gelten.
- **FR-015:** Parallele Submit-Aufrufe auf derselben Session MÜSSEN
  deterministisch abgelehnt oder eindeutig serialisiert werden; die gewählte
  Regel MUSS öffentlich dokumentiert und getestet sein.
- **FR-016:** Child-Sessions MÜSSEN rekursiv atomar teilnehmen. Change-Set,
  Validierung, Reject und Accept behandeln Eltern und Kinder als eine
  Transaktionsgrenze. Zyklen und Mehrfachbesitz sind verboten.
- **FR-017:** Persistierte Formsemantik MUSS als versioniertes JSON mit
  geschlossenem Schema vorliegen. Zulässig sind nur Schlüssel für Form, Feld,
  Control, sicheren Typ, Binding, Converter, Validator und Child-Beziehung.
- **FR-018:** JSON DARF keine CLR-Typnamen, Assemblies, Property-Pfade,
  Methodennamen, Skripte oder sonstige ausführbare Inhalte enthalten. Alle
  Runtime-Bausteine werden über eine vorab befüllte Allowlist-Registry
  aufgelöst.
- **FR-019:** Unbekannte Versionen oder Registry-Schlüssel, unbekannte
  Properties, Duplikate, ungültige Referenzen, Typkonflikte, übergroße Eingaben,
  übermäßige Tiefe und Zyklen MÜSSEN atomar und ohne partielles Modell
  abgelehnt werden.
- **FR-020:** Serialisierung und Deserialisierung MÜSSEN einen deterministischen
  Roundtrip für die unterstützte Formsemantik liefern. Runtime-Werte und
  benutzerspezifische Daten sind nicht Bestandteil dieser Definitionsdatei.

## Transaktionsablauf / Transaction flow

1. Controls übertragen Benutzeränderungen in ihre `FormField<T>`-Instanzen.
2. `SubmitAsync()` friert Werte, Revision und Child-Struktur als Snapshot ein.
3. Sync- und Async-Validatoren prüfen nur diesen Snapshot.
4. Die Anwendung persistiert ein erfolgreiches, nicht veraltetes Change-Set
   selbst, zum Beispiel in einem In-memory-Repository.
5. Erst nach bestätigter Persistenz ruft die Anwendung `AcceptChanges()` auf.
6. Binding-Setter werden transaktional bestmöglich angewandt; erst danach
   werden alle Baselines gemeinsam fortgeschrieben.
7. `RejectChanges()` verwirft lokale Änderungen rekursiv und stellt die
   bestehende Baseline wieder her.

*Submit validates but does not commit. The application persists the immutable
change set and calls AcceptChanges only after persistence succeeds.*

## TuiVision-Integration / TuiVision integration

- **FR-021:** Die Formschicht MUSS durch Komposition oder Adapter mit
  `TDialog`, `TInputLine`, Fokus, Events und Commands arbeiten. Sie DARF die
  bestehende View-Vererbung nicht aufbrechen und ordinary controls nicht zur
  Teilnahme zwingen.
- **FR-022:** Submit-, Accept- und Reject-Commands MÜSSEN auf vorhandenen
  Command-/Event-Pfaden erreichbar sein; kein versteckter Nebenkanal ist
  zulässig.
- **FR-023:** Sichtbare Validierungs- und Statusmeldungen MÜSSEN text-first,
  tastaturerreichbar und unabhängig von Farbe verständlich sein.

## Beispiel / Example

- **FR-024:** `examples/FormTransaction` MUSS ein sichtbares Kundenformular
  mit verschachtelter Adresse liefern.
- **FR-025:** Das Beispiel MUSS Dirty-State, typisiertes POCO-Binding,
  kultur-explizite Konverter, synchrone und asynchrone Validierung, Change-Set,
  In-memory-Persistenz, Accept, Reject, Cancellation und veraltete Async-
  Ergebnisse demonstrieren.
- **FR-026:** Die deklarative Formdefinition MUSS als eingebettetes,
  source-controlled JSON vorliegen. Datenbank, Netzwerk, Prozessstart,
  beliebige Benutzerdateien und persistente Benutzerhistorie sind verboten.
- **FR-027:** Das Beispiel MUSS ein sichtbares Hauptformular, eine echte
  `TStatusLine` und tastaturerreichbares `Help -> Description` besitzen.
  Primärnachweis läuft durch `app.Run()` und kombiniert konkreten Zustand,
  View-Tree und Buffer-/Cell-Evidence.

## Tests und Qualität / Tests and quality

- **FR-028:** Unit- und Integrationstests MÜSSEN Dirty-, Equality-, Accept-,
  Reject-, Change-Set-, POCO-Binding- und Setter-Rollback-Verträge abdecken.
- **FR-029:** Tests MÜSSEN Kultur- und Konvertierungsfehler sowie Async-Erfolg,
  Fehler, Cancellation, Parallelaufruf und Snapshot-Drift abdecken.
- **FR-030:** Tests MÜSSEN verschachtelte Transaktionen, JSON-Roundtrip,
  unbekannte/duplizierte/zyklische/malformed Eingaben und atomare Ablehnung
  ohne partielles Modell abdecken.
- **FR-031:** Regressionstests MÜSSEN unveränderte nichttransaktionale Controls
  und einen echten Beispiel-Run-Loop nachweisen.
- **FR-032:** Öffentliche APIs benötigen vollständige bilinguale XML-
  Dokumentation. Nicht triviale Transaktions-, Snapshot-, Rollback- und
  Parserlogik wird auf didaktischen Kommentarwert geprüft.

## Nicht-Ziele / Non-goals

- Keine 5250-Emulation, kein 5250-Datenstrom und keine DDS-Display-Files.
- Kein Ersatz des Turbo-Vision-Eventmodells und keine Formpflicht für Controls.
- Keine WPF-ähnliche allgemeine Binding-Engine, kein String-Property-Graph und
  keine frei zugängliche Reflection- oder Expression-Ausführung.
- Keine Live-Async-Validierung während der Eingabe; Async läuft nur bei Submit.
- Keine Datenbank, kein Netzwerk, kein Service, keine neue Runtime-Abhängigkeit.
- Keine willkürlichen ausführbaren Inhalte oder CLR-Metadaten im JSON.

## Sicherheit und Robustheit / Security and robustness

Die JSON-Definition ist eine Trust Boundary. Parser und Registry arbeiten
fail-closed, prüfen Größe und Tiefe, lösen ausschließlich bekannte Schlüssel
auf und veröffentlichen nie ein partielles Modell. Validatoren erhalten
Snapshots statt mutierbarer Session-Interna. Cancellation, konkurrierende
Submits und Setterfehler dürfen weder halbe Baselines noch stillen POCO-Drift
erzeugen.

*The JSON definition is a trust boundary. Parsing and registry resolution fail
closed, validators see snapshots, and cancellation, concurrent submits, or
setter failures never publish partial baselines silently.*

## Accessibility und Lernwert / Accessibility and learning value

Nutzertexte und Guides sind deutsch zuerst, englisch danach auf CEFR-B2-
Niveau. Zustände, Fehler, Abhängigkeiten und Entscheidungen müssen in Textform
vorliegen und dürfen nicht nur durch Farbe, Fokusrahmen oder Mausinteraktion
verständlich sein. Das Beispiel erklärt Transaktion, Snapshot, Change-Set,
Binding, Konverter und Registry beim ersten Gebrauch.

## Abhängigkeiten und Reihenfolge / Dependencies and order

Harte Voraussetzungen sind die abgeschlossene Example Portfolio Closure und
die wirksame Source Reference Policy. Das Transactional Form Model soll vor
der Documentation-Publishing-Closure geliefert werden. Nachgelagerte Security-
und Governance-Prüfungen bleiben in ihrer Reihenfolge erhalten.

*Example Portfolio Closure and Source Reference Policy are hard prerequisites.
This feature is preferred before Documentation Publishing Closure.*

## Abnahmekriterien / Acceptance criteria

- **AC-001:** Alle Anforderungen der Phasen 1 bis 4 sind in einer öffentlichen,
  kohärenten API umgesetzt; es bleiben keine Platzhalter.
- **AC-002:** Submit verändert weder POCO noch Baseline; Accept folgt erst auf
  erfolgreiche externe Persistenz und behandelt Parent/Children atomar.
- **AC-003:** Async-Snapshot-Drift, Cancellation, Parallelaufruf und
  Setter-Rollback sind deterministisch nachgewiesen.
- **AC-004:** JSON-Registry und Parser lehnen alle genannten Fehler atomar ab;
  die eingebettete Beispieldefinition besteht den Roundtrip.
- **AC-005:** Das Beispiel demonstriert alle zehn benannten Verhaltensbereiche
  sichtbar durch den echten Run-Loop und bleibt text-first bedienbar.
- **AC-006:** Bestehende nichttransaktionale Controls bleiben kompatibel.
- **AC-007:** Release-Build und vollständige Tests bestehen; Core, Controls,
  Serialization, Compatibility und Drivers.Console erreichen jeweils
  mindestens 70 Prozent Line Coverage.
- **AC-008:** `dotnet format --verify-no-changes`, DocFX ohne Warnung/Fehler,
  Playwright/Axe, Lynx und Bash-/PowerShell-Governance-Parität bestehen.
- **AC-009:** Source-review Evidence nennt TuiVision-Vertrag, exakten
  Magiblot-Pin, relevante `tv203s`-Dateien und genau eine Policy-Disposition.

## Entscheidungen und offene Fragen / Decisions and open questions

Alle materiellen Produktfragen sind durch Issue #154 und den genehmigten Plan
beantwortet. Die konkrete API-Namensfeinheit darf im Spec-/Plan-Review
idiomatisch präzisiert werden, solange die obigen Verträge unverändert bleiben.
Es gibt keine offene Frage, die Specify oder lokale Implementierung blockiert.
Delivery Authority ist `LocalImplementation`; Commit, Push, PR, Merge, Bypass,
Provider-Administration und Secret-Zugriff sind nicht autorisiert.

<!-- intake-authoring:prompts -->
## Copy-Ready Spec Kit Prompts

<!-- spec-kit-command-id: speckit.specify -->
### Specify

```text
$speckit-specify requirements/intakes/active/Lastenheft_Transactional-Form-Model.md. Binde exakt diesen reviewten Intake, Issue #154 und die wirksame Quellenpolicy. Liefere alle Phasen 1 bis 4 in einer kohärenten Spezifikation; implementiere nichts und führe keine Remote-Schreibaktion aus.
```

<!-- spec-kit-command-id: speckit.autonomous -->
### Autonomous

```text
$speckit-autonomous requirements/intakes/active/Lastenheft_Transactional-Form-Model.md mit Delivery-Authority LocalImplementation. Implementiere Phase 1 bis 4, das Beispiel FormTransaction und alle lokalen Gates. Commit, Push, PR, Merge, Bypass, Provider-Administration und Secret-Zugriff sind nicht autorisiert.
```

<!-- intake-authoring:end -->
