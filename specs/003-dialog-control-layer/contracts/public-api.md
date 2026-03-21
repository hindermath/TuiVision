# Public API Contracts: Dialog-/Control-Schicht (003-dialog-control-layer)

**Phase**: 1 — Design & Contracts
**Date**: 2026-03-21
**Module**: `TuiVision.Controls`

> Diese Datei definiert die öffentlichen API-Signaturen aller 13 neuen Klassen dieser
> Portierungsphase. Sie dient als Vertrag zwischen Spec, Tests und Implementierung.
> Alle Signaturen sind in C# 14 / .NET 10 formuliert.
>
> This file defines the public API signatures of all 13 new classes in this porting phase.
> It serves as a contract between spec, tests, and implementation.
> All signatures are in C# 14 / .NET 10.

---

## TStringList

```csharp
namespace TuiVision.Controls;

public sealed class TStringList
{
    public TStringList();

    public int Count { get; }
    public string this[int index] { get; }

    public void Add(string item);
    public void Clear();
}
```

**Invarianten**: `Count >= 0`; Indexer wirft `ArgumentOutOfRangeException` bei ungültigem Index.

---

## TButtonFlags

```csharp
namespace TuiVision.Controls;

[Flags]
public enum TButtonFlags : byte
{
    bfNormal      = 0x00,
    bfDefault     = 0x01,
    bfLeftJustify = 0x02
}
```

---

## TScrollBar

```csharp
namespace TuiVision.Controls;

public class TScrollBar : TView
{
    public TScrollBar(TRect bounds, bool horizontal = false);

    public int Value  { get; private set; }
    public int Max    { get; private set; }
    public int PgStep { get; private set; }
    public int ArStep { get; set; }
    public bool Horizontal { get; }

    public void SetParams(int value, int min, int max, int pgStep, int arStep);
    public void SetLimit(int max);
    public void ScrollTo(int value);

    public override void Draw();
    public override void HandleEvent(ref TEvent ev);
}
```

---

## TScroller

```csharp
namespace TuiVision.Controls;

public abstract class TScroller : TView
{
    protected TScroller(TRect bounds);

    public TPoint Delta { get; protected set; }
    public TPoint Limit { get; protected set; }
    public TScrollBar? HScrollBar { get; }
    public TScrollBar? VScrollBar { get; }

    public void SetScrollBars(TScrollBar? hBar, TScrollBar? vBar);
    public void ScrollTo(TPoint delta);
    protected void SetLimit(TPoint limit);

    public override void Draw();
    public override void HandleEvent(ref TEvent ev);
}
```

---

## TStaticText

```csharp
namespace TuiVision.Controls;

public class TStaticText : TView
{
    public TStaticText(TRect bounds, string text);

    public string Text { get; }

    public override void Draw();
    // HandleEvent: no-op (never focused)
}
```

---

## TLabel

```csharp
namespace TuiVision.Controls;

public class TLabel : TStaticText
{
    public TLabel(TRect bounds, string text, TView? link);

    public TView? Link { get; }
    public bool   Light { get; private set; }

    public override void Draw();
    public override void HandleEvent(ref TEvent ev);
}
```

---

## TButton

```csharp
namespace TuiVision.Controls;

public class TButton : TView
{
    public TButton(TRect bounds, string title, ushort command, TButtonFlags flags);

    public string       Title     { get; }
    public ushort       Command   { get; }
    public TButtonFlags Flags     { get; }
    public bool         AmDefault { get; internal set; }

    public override void Draw();
    public override void HandleEvent(ref TEvent ev);
    public override TViewOptions GetOptions();
}
```

---

## TInputLine

```csharp
namespace TuiVision.Controls;

public class TInputLine : TView
{
    public TInputLine(TRect bounds, int maxLen);

    public string Data       { get; set; }
    public int    MaxLen     { get; }
    public int    CurPos     { get; private set; }
    public int    FirstPos   { get; private set; }
    public bool   InsertMode { get; private set; }

    public override void Draw();
    public override void HandleEvent(ref TEvent ev);
    public override TViewOptions GetOptions();
}
```

---

## TCluster (abstract)

```csharp
namespace TuiVision.Controls;

public abstract class TCluster : TView
{
    protected TCluster(TRect bounds, string[] strings);

    public string[] Items { get; }
    public uint     Value { get; set; }
    public int      Sel   { get; protected set; }

    protected abstract bool Mark(int item);
    protected abstract void Press(int item);

    public override void Draw();
    public override void HandleEvent(ref TEvent ev);
    public override TViewOptions GetOptions();
}
```

---

## TCheckBoxes

```csharp
namespace TuiVision.Controls;

public sealed class TCheckBoxes : TCluster
{
    public TCheckBoxes(TRect bounds, string[] strings);

    protected override bool Mark(int item);   // (Value & (1u << item)) != 0
    protected override void Press(int item);  // Value ^= (1u << item)
}
```

---

## TRadioButtons

```csharp
namespace TuiVision.Controls;

public sealed class TRadioButtons : TCluster
{
    public TRadioButtons(TRect bounds, string[] strings);

    protected override bool Mark(int item);   // Value == (uint)item
    protected override void Press(int item);  // Value = (uint)item
}
```

---

## TListViewer (abstract)

```csharp
namespace TuiVision.Controls;

public abstract class TListViewer : TView
{
    protected TListViewer(TRect bounds, int numCols,
                          TScrollBar? vScrollBar, TScrollBar? hScrollBar);

    public int NumCols      { get; }
    public int TopItem      { get; protected set; }
    public int FocusedItem  { get; protected set; }
    public TScrollBar? VScrollBar { get; }
    public TScrollBar? HScrollBar { get; }

    public abstract int    GetNumItems();
    public abstract string GetText(int item, int maxChars);

    public void FocusItem(int item);
    public void SelectItem(int item);

    public override void Draw();
    public override void HandleEvent(ref TEvent ev);
    public override TViewOptions GetOptions();
}
```

---

## TListBox

```csharp
namespace TuiVision.Controls;

public class TListBox : TListViewer
{
    public TListBox(TRect bounds, int numCols,
                    TScrollBar? vScrollBar, TScrollBar? hScrollBar = null);

    public TStringList? List      { get; set; }
    public int          Selection { get; private set; }

    public override int    GetNumItems();
    public override string GetText(int item, int maxChars);
}
```

**Vertragsregel / Contract rule**: Ein Doppelklick bestätigt den angeklickten
Eintrag als Auswahl, führt aber innerhalb dieses Feature-Umfangs nicht zu einem
separaten zusätzlichen Command-Ereignis.

---

## TDialog

```csharp
namespace TuiVision.Controls;

public class TDialog : TGroup
{
    public TDialog(TRect bounds, string? title);

    public string? Title { get; }

    /// <summary>
    /// Öffnet den Dialog modal und blockiert synchron bis zum Schließen.
    /// Gibt die Command-ID zurück, mit der der Dialog geschlossen wurde.
    /// Die Escape-Taste schließt den Dialog standardmäßig mit <c>cmCancel</c>,
    /// sofern kein Kind-Control das Escape-Ereignis vorher konsumiert.
    ///
    /// Opens the dialog modally and blocks synchronously until closed.
    /// Returns the command ID with which the dialog was closed.
    /// The Escape key closes the dialog with <c>cmCancel</c> by default unless
    /// a child control consumes the Escape event first.
    /// </summary>
    public ushort Run();

    public void CloseDialog(ushort command);

    public override void Draw();
    public override void HandleEvent(ref TEvent ev);
    public override TViewOptions GetOptions();
}
```

---

## Vordefinierte Command-IDs / Predefined Command IDs

```csharp
// In ShellCommandIds.cs (existing) — zu ergänzen / to be extended:
public static class CommandIds
{
    public const ushort cmOK     = 10;
    public const ushort cmCancel = 11;
    public const ushort cmYes    = 12;
    public const ushort cmNo     = 13;
}
```
