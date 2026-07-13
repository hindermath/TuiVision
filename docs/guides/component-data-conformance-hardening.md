# Komponenten- und Datenkonformität / Component and Data Conformance

## Zweck / Purpose

Feature 026 schließt vier Findings des TV203-/Free-Vision-Audits. Dialoge
beenden sich nur über ausdrückliche Abschlussbefehle, Eingabefelder können
Validatoren in drei Phasen nutzen, Dateidialoge liefern geschlossene
mode-abhängige Ergebnisse, und benannte Ressourcen rekonstruieren nur
allowlist-basierte UI-Beschreibungen.

Feature 026 closes four findings from the TV203/Free Vision audit. Dialogs
complete only through explicit completion commands, input lines can use
validators in three phases, file dialogs return closed mode-aware outcomes,
and named resources reconstruct only allowlisted UI descriptions.

Die historische Verantwortung bleibt erhalten. Pointer-Transfer, binäre
Borland-Ressourcen und beliebige Runtime-Typaktivierung werden nicht kopiert.
Die Umsetzung verwendet unveränderliche Results, primitive Records und
vollständige Validierung vor sichtbarer Zustandsänderung.

The historical responsibility is retained. Pointer transfer, binary Borland
resources, and arbitrary runtime type activation are not copied. The
implementation uses immutable results, primitive records, and complete
validation before visible state changes.

## Vertragsfluss / Contract Flow

```text
Benutzereingabe oder Persistenzdaten / User input or persisted data
                         |
                         v
       begrenzte Klassifikation und Validierung / bounded validation
                         |
              +----------+----------+
              |                     |
              v                     v
     Accepted / akzeptiert   Rejected / abgelehnt
              |                     |
              v                     v
  atomare Veröffentlichung   Zustand, Fokus und Daten bleiben erhalten
  atomic publication         state, focus, and data stay preserved
```

Dieser Ablauf ist text-first lesbar. Wesentliche Bedeutung hängt nicht von
Farbe, Layout oder Pointerbedienung ab. Das folgt dem Leitgedanken
`Programmierung #include<everyone>`.

This flow is readable in text-first environments. Essential meaning does not
depend on color, layout, or pointer operation. It follows the principle
`Programming #include<everyone>`.

## Dialogabschluss und Kindvalidierung / Dialog Completion and Child Validation

`TDialog` unterscheidet Abschlussbefehle von normalen Commands. `cmOK`,
`cmYes` und `cmNo` prüfen die Kinder in stabiler View-Reihenfolge. Die erste
Ablehnung beendet den Lauf, liefert ein `TValidationResult` und fokussiert das
betroffene Control. `cmCancel` verwirft keine Eingabeprüfung, weil Abbruch
keine inhaltliche Zustimmung ist. Abgeleitete Dialoge können die begrenzte
Completion-Klassifikation erweitern.

`TDialog` distinguishes completion commands from normal commands. `cmOK`,
`cmYes`, and `cmNo` validate children in stable view order. The first rejection
stops the walk, returns a `TValidationResult`, and focuses the affected control.
`cmCancel` does not run content validation because cancellation is not content
acceptance. Derived dialogs can extend the bounded completion classifier.

Normale Help-, Anwendungs- oder unbekannte Commands bleiben für den Owner
verfügbar. Ein fehlgeschlagener Abschluss schließt den Dialog nicht und
überschreibt keinen zuvor gültigen Zustand.

Normal help, application, or unknown commands remain available to the owner.
A failed completion does not close the dialog or overwrite previously valid
state.

## Validator-Lebenszyklus / Validator Lifecycle

Ein `TInputLine`-Validator ist optional. Ohne Validator bleibt das bestehende
Verhalten erhalten. Mit Validator gelten drei getrennte Phasen:

| Phase | Zweck / Purpose | Ablehnungsgrenze / Rejection boundary |
|---|---|---|
| `Edit` | Kandidaten vor Mutation prüfen / Check candidates before mutation | Text, Cursor, Auswahl und Offset bleiben unverändert / Text, cursor, selection, and offset remain unchanged |
| `FocusLoss` | Fokuswechsel prüfen / Check focus transition | Fokus bleibt beim Eingabefeld / Focus stays on the input line |
| `Acceptance` | Dialogbestätigung prüfen / Check dialog acceptance | Dialog bleibt offen und meldet den Fehler als Text / Dialog remains open and reports the error as text |

Bereichsvalidatoren dürfen sinnvolle Zwischeneingaben während `Edit` zulassen
und erst bei Fokusverlust oder Bestätigung streng prüfen. Filtervalidatoren
lehnen dagegen bereits syntaktisch ungültige Edit-Kandidaten ab.

Range validators may allow meaningful intermediate input during `Edit` and
apply strict checks on focus loss or acceptance. Filter validators reject
syntactically invalid edit candidates immediately.

## Dateidialog-Ergebnisse / File Dialog Outcomes

`TFileDialogOutcome` trennt Navigation, Filterwechsel, Open, Save,
Overwrite-Entscheidung, Auswahl, Ablehnung und Abbruch. Der Dialog prüft nur
Pfad- und Metadaten. Er liest oder schreibt keinen Dateiinhalt und führt keine
destruktive Operation aus.

`TFileDialogOutcome` separates navigation, filter changes, open, save,
overwrite decisions, selection, rejection, and cancellation. The dialog checks
only paths and metadata. It does not read or write file content and performs no
destructive operation.

Ein vorhandenes Save-Ziel liefert `OverwriteDecisionRequired`; die spätere
Inhaltsentscheidung bleibt beim Caller. Ein fehlender Open-Pfad, ein Save-Ziel
ohne vorhandenen Elternordner, ein falscher Zieltyp oder ungültige
Pfad-/Wildcard-Syntax liefert `Rejected` mit stabilem Code und lesbarer
Meldung. History und Close-Zustand werden erst nach akzeptiertem Ergebnis
veröffentlicht.

An existing save target returns `OverwriteDecisionRequired`; the later content
decision remains with the caller. A missing open path, a save target without an
existing parent, a wrong target kind, or invalid path/wildcard syntax returns
`Rejected` with a stable code and readable message. History and close state are
published only after an accepted outcome.

## Sichere benannte UI-Ressourcen / Safe Named UI Resources

`TResourceFile` behält exakte, case-sensitive Keys. Dialog-, Menü- und
StatusLine-Beschreibungen enthalten nur primitive Werte und unveränderliche
Listen. Die Registry akzeptiert ausschließlich bekannte Typ-IDs. Erst nach
vollständigem Parsen und semantischer Validierung erzeugen Controls-Adapter und
Factories bestehende `TMenuBar`-, `TStatusLine`- oder Dialogmodelle.

`TResourceFile` retains exact, case-sensitive keys. Dialog, menu, and status-line
descriptions contain only primitive values and immutable lists. The registry
accepts known type IDs only. Controls adapters and factories create existing
`TMenuBar`, `TStatusLine`, or dialog models only after complete parsing and
semantic validation.

Die Grenzen sind 4.096 Ressourcen, 4 MiB pro Payload, 4.096 UI-Einträge und
Menütiefe 16. Unbekannte Typen oder Versionen, abgeschnittene oder nachlaufende
Daten, doppelte Keys, ungültige Referenzen, Zyklen, Bereiche oder Commands
werden atomar abgelehnt. Persistierte CLR-Typnamen, Delegates, Owner,
Reflection-Metadaten oder Aktivierungsanweisungen sind nicht Teil des Formats.

Limits are 4,096 resources, 4 MiB per payload, 4,096 UI items, and menu depth
16. Unknown types or versions, truncated or trailing data, duplicate keys,
invalid references, cycles, ranges, or commands are rejected atomically.
Persisted CLR type names, delegates, owners, reflection metadata, or activation
instructions are not part of the format.

## Historische Einordnung / Historical Context

Die passenden C++-Dateien unter `tv203s/` sind die schreibgeschützte
Primärquelle. Free Vision am gepinnten Commit bestätigt als zweite Meinung die
Verantwortung für Dialogvalidierung, Eingabevalidierung, Dateimodi und benannte
Ressourcen. Kein C++- oder Pascal-Code wurde kopiert oder vendort.

Matching C++ files under `tv203s/` are the read-only primary source. Free Vision
at the pinned commit corroborates responsibility for dialog validation, input
validation, file modes, and named resources as a second opinion. No C++ or
Pascal code was copied or vendored.

Bewusste Modernisierungen sind managed Results, explizite Phasen,
plattformneutrale Pfade, primitive Records und eine geschlossene Allowlist.
Historische Binärformate, Raw Pointer, versteckte Datei-I/O und Reflection-
Aktivierung bleiben außerhalb dieses Features.

Deliberate modernizations are managed results, explicit phases,
platform-neutral paths, primitive records, and a closed allowlist. Historical
binary formats, raw pointers, hidden file I/O, and reflection activation remain
outside this feature.

## Verifikation / Verification

```bash
dotnet test tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj --configuration Release --filter "FullyQualifiedName~Description|FullyQualifiedName~F010|FullyQualifiedName~F011|FullyQualifiedName~F012"
dotnet test tests/TuiVision.Serialization.Tests/TuiVision.Serialization.Tests.csproj --configuration Release --filter "FullyQualifiedName~Resource|FullyQualifiedName~Description"
```

Der maschinenlesbare Abschluss steht in
`specs/024-tv203-freevision-conformance-audit/conformance-audit.json`. Die
vollständige Red-/Green-, Governance- und Restrisiko-Evidence steht in
`specs/026-component-data-conformance-hardening/pr-evidence.md`.

The machine-readable closure is stored in
`specs/024-tv203-freevision-conformance-audit/conformance-audit.json`. Complete
red/green, governance, and residual-risk evidence is stored in
`specs/026-component-data-conformance-hardening/pr-evidence.md`.
