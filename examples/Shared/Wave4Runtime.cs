// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Shared;

/// <summary>
/// Zeichnet den kurzen Laufzeitzustand der Wave-4-Beispiele.
/// Draws the short runtime state of the Wave-4 examples.
/// </summary>
internal sealed class Wave4StatusLine : TStatusLine
{
    public Wave4StatusLine(TRect bounds, string message) : base(bounds) => Message = message;

    public string Message { get; private set; }

    public void SetMessage(string message)
    {
        Message = message ?? string.Empty;
        DrawView();
    }

    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0)
        {
            return;
        }

        // Explizite Zellen halten Status und Fallback auch ohne Host-Screenshot beweisbar.
        // Explicit cells keep status and fallback provable without a host screenshot.
        for (int x = 0; x < Size.X; x++)
        {
            buffer.TrySetCell(Origin.X + x, Origin.Y, new TConsoleCell(' ', ConsoleColor.Black, ConsoleColor.Cyan));
        }

        string text = Message.Length <= Size.X ? Message : Message[..Size.X];
        buffer.WriteText(Origin.X, Origin.Y, text.AsSpan(), ConsoleColor.Yellow, ConsoleColor.Cyan);
    }
}

/// <summary>
/// Gemeinsame Präsentations- und Smoke-Orchestrierung der Wave-4-Beispiele.
/// Terminal-, Mapping-, Font-, Profil- und Hostlogik bleibt in den
/// Feature-021-Framework-Komponenten.
///
/// Shared presentation and smoke orchestration for Wave-4 examples. Terminal,
/// mapping, font, profile, and host logic remains in Feature-021 framework components.
/// </summary>
public abstract class Wave4Application : TApplication
{
    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private TView? _descriptionView;

    /// <summary>Initialisiert die gemeinsame Anwendungsshell. / Initializes the shared application shell.</summary>
    protected Wave4Application(TRect bounds, bool headless) : base(bounds) => _headless = headless;

    /// <summary>Letzter sichtbarer Haupttyp. / Last visible main view kind.</summary>
    public string LastVisibleComponentKind { get; protected set; } = string.Empty;

    /// <summary>Letzte stabile Proof-Region. / Last stable proof region.</summary>
    public TRect LastVisibleRegion { get; protected set; }

    /// <summary>Letzter sichtbarer Status. / Last visible status.</summary>
    public string LastStatusMessage { get; protected set; } = string.Empty;

    /// <summary>Ob der Headless-App-Loop den kontrollierten Quit erzeugt hat. / Whether the headless app loop issued controlled quit.</summary>
    public bool QuitIssued { get; private set; }

    /// <summary>Fügt deterministische App-Loop-Ereignisse hinzu. / Adds deterministic app-loop events.</summary>
    public void QueueEvents(IEnumerable<TEvent> events)
    {
        foreach (TEvent @event in events)
        {
            _scriptedEvents.Enqueue(@event);
        }
    }

    /// <inheritdoc />
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && _scriptedEvents.Count > 0)
        {
            @event = _scriptedEvents.Dequeue();
            return;
        }

        if (_headless && !QuitIssued)
        {
            QuitIssued = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave4StatusLine(bounds, "Wave 4: ready | Help -> Description | ^Q Quit");

    /// <summary>Aktualisiert den sichtbaren kurzen Zustand. / Updates the visible short state.</summary>
    protected void SetWave4Status(string example, string state)
    {
        LastStatusMessage = $"{example}: {state} | Help -> Description | ^Q Quit";
        if (StatusLine is Wave4StatusLine status)
        {
            status.SetMessage(LastStatusMessage);
        }
    }

    /// <summary>Merkt eine konkrete Hauptansicht als Proof-Ziel. / Records a concrete main view as proof target.</summary>
    protected void SetWave4Visible(TView view, string kind)
    {
        LastVisibleComponentKind = kind;
        LastVisibleRegion = Desktop is null ? default : Wave4Runtime.ScreenRegion(Desktop, view);
    }

    /// <summary>Zeigt eine zweisprachige Beschreibung. / Shows a bilingual description.</summary>
    protected void ShowWave4Description(string example, string body)
    {
        if (Desktop is null)
        {
            return;
        }

        if (_descriptionView?.Owner == Desktop)
        {
            Desktop.Remove(_descriptionView);
        }

        TRect region = Wave4Runtime.MainRegion(Desktop, 66, 13);
        TWindow window = new($"{example} Description", region.A.X, region.A.Y, region.Width, region.Height);
        window.Insert(new TStaticText(new TRect(2, 2, Math.Max(3, region.Width - 2), Math.Max(3, region.Height - 1)), body));
        Desktop.Insert(window);
        Desktop.SetFocus(window);
        _descriptionView = window;
        SetWave4Visible(window, "TWindow");
        SetWave4Status(example, "Description visible");
    }
}

/// <summary>Hilfen für wiederholte Wave-4-Präsentation. / Helpers for repeated Wave-4 presentation.</summary>
public static class Wave4Runtime
{
    /// <summary>Erstellt das gemeinsame Hilfemenü. / Creates the shared Help menu.</summary>
    public static TMenuItem HelpMenu(ushort descriptionCommand, TMenuItem? next = null) =>
        new("~H~ilfe / ~H~elp", 0, next, new TMenuItem("~B~eschreibung / ~D~escription", descriptionCommand));

    /// <summary>Ermittelt eine begrenzte Hauptregion. / Resolves a bounded main region.</summary>
    public static TRect MainRegion(TGroup desktop, int width, int height)
    {
        int actualWidth = Math.Clamp(width, 12, Math.Max(12, desktop.Size.X - 4));
        int actualHeight = Math.Clamp(height, 6, Math.Max(6, desktop.Size.Y - 3));
        return new TRect(2, 1, 2 + actualWidth, 1 + actualHeight);
    }

    /// <summary>Konvertiert lokale View-Bounds in Bildschirmkoordinaten. / Converts local view bounds to screen coordinates.</summary>
    public static TRect ScreenRegion(TGroup owner, TView view)
    {
        TRect bounds = view.GetBounds();
        return new TRect(
            owner.Origin.X + bounds.A.X,
            owner.Origin.Y + bounds.A.Y,
            owner.Origin.X + bounds.B.X,
            owner.Origin.Y + bounds.B.Y);
    }
}
