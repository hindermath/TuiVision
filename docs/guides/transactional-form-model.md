# Transactional Form Model

## Deutsch

Das Transactional Form Model ist eine optionale Schicht für Eingabeabläufe,
bei denen mehrere Werte erst gemeinsam geprüft, extern gespeichert und danach
in ein Anwendungsmodell übernommen werden sollen. Vorhandene `TInputLine`-,
`TDialog`-, Event- und Command-Verträge bleiben unverändert. Eine Anwendung
nimmt nur teil, wenn sie Felder und eine `FormSession` anlegt.

### Ablauf

```text
ordinary Controls
       |
       | explizite Adapter
       v
FormField<T> + Child-Sessions
       |
       | SubmitAsync(): stabiler Snapshot, sync + async prüfen
       v
FormSubmitResult + unveränderliches Change-Set
       |
       | Anwendung persistiert erfolgreich
       v
AcceptChanges(): Setter anwenden, dann Baselines verschieben
```

`SubmitAsync()` verändert weder das gebundene POCO noch die Baseline. Ändert
sich während einer asynchronen Prüfung ein Feld oder die Session-Struktur,
lautet das Ergebnis `Stale`. Das alte Prüfergebnis wird dann nicht als neuer
Feldstatus veröffentlicht. Cancellation wird an den Aufrufer weitergegeben;
ein zweiter paralleler Submit wird abgelehnt.

`AcceptChanges()` gehört hinter die erfolgreiche externe Persistenz. Die
Session erfasst zuerst die bisherigen Property-Werte und ruft danach die
Setter in stabiler Feldreihenfolge auf. Bei einem Fehler versucht sie den
Rollback in umgekehrter Reihenfolge. Die Baselines bleiben unverändert. Ein
Setter kann jedoch Nebeneffekte außerhalb seiner Property auslösen; diese
Grenze meldet `FormBindingCommitException` ausdrücklich.

### Binding, Konverter und Child-Sessions

`FormField<T>.FromProperty(...)` akzeptiert nur einen direkten les- und
schreibbaren Property-Ausdruck. Zeichenkettenpfade und aus JSON geladene
CLR-Typen oder Methoden gibt es nicht. Ein anderer Property-Typ benötigt einen
bidirektionalen `IFormValueConverter<TField,TModel>` und eine explizite
`CultureInfo`.

Eine Child-Session besitzt genau einen Parent. Felder besitzen genau eine
Session. Submit, Accept und Reject erfolgen an der Root-Session und umfassen
den vollständigen Baum. Damit kann eine verschachtelte Adresse nicht getrennt
vom Kundenformular übernommen werden.

### Deklarative Semantik

`TFormSemanticJson` speichert nur diese symbolischen Angaben:

- Formatversion, Root-Form und Formdefinitionen;
- Feld-, Control-, Typ-, Binding-, Converter- und Validator-Schlüssel;
- benannte Child-Referenzen.

Das Format erlaubt keine CLR-Typnamen, Property-Pfade, Methoden oder
ausführbaren Inhalte. Es begrenzt Größe, Item-Anzahl und Tiefe, lehnt unbekannte
Properties, Duplikate, ungültige Referenzen, Mehrfachbesitz und Zyklen ab und
liefert nie ein partielles Dokument. `FormRuntimeRegistry` löst die Schlüssel
anschließend ausschließlich gegen vertrauenswürdig im C#-Code registrierte
Werte und passende Typ-Schlüssel auf.

### Beispiel starten

```bash
dotnet run --project examples/FormTransaction
dotnet test tests/TuiVision.Controls.Tests/ --filter "FullyQualifiedName~FormSessionTests"
dotnet test tests/TuiVision.Serialization.Tests/ --filter "FullyQualifiedName~FormSemanticJsonTests"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~FormTransactionSmokeTests"
```

Das Beispiel nutzt ein eingebettetes, source-controlled JSON, eine
In-memory-Persistenz und synthetische Kundendaten. Es liest keine beliebigen
Benutzerdateien und verwendet weder Datenbank noch Netzwerk. Die Commands
zeigen Dirty-State und Change-Set, synchrone und asynchrone Prüfung, Accept,
Reject, Cancellation und `Stale`. `Help -> Description` erklärt den Ablauf
textorientiert.

## English

The Transactional Form Model is an optional layer for input workflows where
several values must be validated together, stored externally, and only then
applied to an application model. Existing `TInputLine`, `TDialog`, event, and
command contracts remain unchanged. An application participates only when it
creates fields and a `FormSession`.

### Flow

```text
ordinary controls
       |
       | explicit adapters
       v
FormField<T> + child sessions
       |
       | SubmitAsync(): stable snapshot, sync + async validation
       v
FormSubmitResult + immutable change set
       |
       | application persists successfully
       v
AcceptChanges(): apply setters, then advance baselines
```

`SubmitAsync()` changes neither the bound POCO nor the baseline. If a field or
the session structure changes during asynchronous validation, the result is
`Stale`. The old validation result is not published as new field state.
Cancellation is propagated to the caller, and a second concurrent submit is
rejected.

Call `AcceptChanges()` after successful external persistence. The session
first captures the previous property values and then invokes setters in stable
field order. On failure, it attempts rollback in reverse order. Baselines stay
unchanged. A setter can still cause side effects outside its property;
`FormBindingCommitException` explicitly reports this boundary.

### Binding, converters, and child sessions

`FormField<T>.FromProperty(...)` accepts only a direct readable and writable
property expression. There are no string property paths and no CLR types or
methods loaded from JSON. A different property type requires a bidirectional
`IFormValueConverter<TField,TModel>` and an explicit `CultureInfo`.

A child session has exactly one parent. A field belongs to exactly one
session. Submit, accept, and reject run on the root session and cover the whole
tree. A nested address therefore cannot be accepted separately from its
customer form.

### Declarative semantics

`TFormSemanticJson` stores only these symbolic values:

- format version, root form, and form definitions;
- field, control, type, binding, converter, and validator keys;
- named child references.

The format permits no CLR type names, property paths, methods, or executable
content. It limits size, item count, and depth; rejects unknown properties,
duplicates, invalid references, shared ownership, and cycles; and never
returns a partial document. `FormRuntimeRegistry` then resolves keys only
against trusted values registered in C# code with matching type keys.

### Run the example

```bash
dotnet run --project examples/FormTransaction
dotnet test tests/TuiVision.Controls.Tests/ --filter "FullyQualifiedName~FormSessionTests"
dotnet test tests/TuiVision.Serialization.Tests/ --filter "FullyQualifiedName~FormSemanticJsonTests"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~FormTransactionSmokeTests"
```

The example uses embedded source-controlled JSON, in-memory persistence, and
synthetic customer data. It reads no arbitrary user files and uses neither a
database nor a network. Its commands show dirty state and change sets,
synchronous and asynchronous validation, accept, reject, cancellation, and
`Stale`. `Help -> Description` explains the flow in text-first form.
