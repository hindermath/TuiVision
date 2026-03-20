# Quickstart: View-System Phase 3 — TGroup verwenden

**Branch**: `001-view-system-tgroup` | **Phase**: 1 — Design | **Date**: 2026-03-16

---

## Voraussetzungen / Prerequisites

- .NET 10 SDK installiert
- TuiVision-Projekt gebaut: `dotnet build --configuration Release`
- Alle bestehenden Tests grün: `dotnet test`

---

## Beispiel 1: TGroup mit zwei Kind-Views erstellen

```csharp
// Eine Gruppe erstellt einen Container für mehrere Views.
// A group creates a container for multiple views.
var groupBounds = new TRect(0, 0, 80, 25);
var group = new TGroup(groupBounds);

// Kind-Views definieren und einfügen.
// Define and insert child views.
var topBar   = new TView(new TRect(0, 0, 80, 1));
var mainArea = new TView(new TRect(0, 1, 80, 25));

group.Insert(topBar);
group.Insert(mainArea);

// Owner wurde automatisch gesetzt.
// Owner was set automatically.
Debug.Assert(topBar.Owner == group);
Debug.Assert(mainArea.Owner == group);
```

---

## Beispiel 2: Fokus setzen und wechseln

```csharp
// Auswählbare Views benötigen das Selectable-Flag.
// Selectable views require the Selectable flag.
topBar.Options   |= TViewOptions.Selectable;
mainArea.Options |= TViewOptions.Selectable;

// Direkter Fokuswechsel.
// Direct focus change.
group.SetFocus(mainArea);
Debug.Assert(mainArea.GetState(TViewState.Focused));
Debug.Assert(!topBar.GetState(TViewState.Focused));

// Tab-Navigation (zirkulär).
// Tab navigation (circular).
group.SelectNext(forward: true);
Debug.Assert(topBar.GetState(TViewState.Focused)); // zurück zur ersten View
```

---

## Beispiel 3: Eigene View mit Draw() ableiten

```csharp
// Eigene TView-Unterklasse mit Zeichenlogik.
// Custom TView subclass with drawing logic.
public sealed class LabelView : TView
{
    private readonly string _text;

    public LabelView(TRect bounds, string text) : base(bounds)
    {
        _text = text;
    }

    public override void Draw()
    {
        // Puffer der Eigentümer-Gruppe über Owner abrufen.
        // Retrieve the owner group's buffer via Owner.
        if (Owner?._buffer is not { } buffer)
            return;

        buffer.WriteText(
            Origin.X, Origin.Y,
            _text.AsSpan(),
            ConsoleColor.White,
            ConsoleColor.DarkBlue);
    }
}

// Verwendung / Usage:
var label = new LabelView(new TRect(1, 1, 20, 2), "Hallo TuiVision");
group.Insert(label);
group.DrawView(); // ruft Draw() auf allen sichtbaren Kind-Views auf
```

---

## Beispiel 4: Draw-Lock für Batch-Operationen

```csharp
// Mehrere Operationen ohne Zwischen-Redraws.
// Multiple operations without intermediate redraws.
group.LockDraw();

group.Remove(topBar);
group.Insert(new TView(new TRect(0, 0, 80, 2)));
group.SetFocus(mainArea);

// UnlockDraw() triggert genau einen DrawView()-Aufruf.
// UnlockDraw() triggers exactly one DrawView() call.
group.UnlockDraw();
```

---

## Beispiel 5: Tastaturereignis dispatchen

```csharp
// Ein KeyDown-Ereignis wird an die fokussierte Kind-View gesendet.
// A KeyDown event is sent to the focused child view.
var keyEvent = TEvent.CreateKeyDown(
    charCode: 'a',
    scanCode: 0x1E,
    keyCode: 0,
    shiftState: 0,
    rawScanCode: 0x1E);

group.HandleEvent(keyEvent);
// group.Current empfängt das Ereignis in der Focused-Phase.
// group.Current receives the event in the Focused phase.
```

---

## Build & Test ausführen

```bash
# Vollständiger Validierungszyklus
dotnet restore
dotnet build --configuration Release
dotnet test

# Nur Controls-Tests
dotnet test tests/TuiVision.Controls.Tests/

# Einzelnen Test ausführen
dotnet test --filter "FullyQualifiedName~TGroupTests"
```
