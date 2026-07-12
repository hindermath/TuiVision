// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Shared;

/// <summary>
/// Zeichnet den kurzen Laufzeitzustand der Wave-3-Beispiele.
///
/// Draws the short runtime state of Wave-3 examples.
/// </summary>
internal sealed class Wave3StatusLine : TStatusLine
{
    public Wave3StatusLine(TRect bounds, string message) : base(bounds) => Message = message;

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

        // Explizite Zellen machen den Status zugleich sichtbar und treiberunabhaengig pruefbar.
        // Explicit cells make status both visible and verifiable across drivers.
        for (int x = 0; x < Size.X; x++)
        {
            buffer.TrySetCell(Origin.X + x, Origin.Y, new TConsoleCell(' ', ConsoleColor.Black, ConsoleColor.Cyan));
        }

        string text = Message.Length <= Size.X ? Message : Message[..Size.X];
        buffer.WriteText(Origin.X, Origin.Y, text.AsSpan(), ConsoleColor.Yellow, ConsoleColor.Cyan);
    }
}

/// <summary>
/// Gemeinsame Präsentations- und Smoke-Orchestrierung der Wave-3-Beispiele.
/// Fachliche Editor-, Hilfe-, Ressourcen- und Compilerlogik bleibt in den
/// vorhandenen Framework-Komponenten.
///
/// Shared presentation and smoke orchestration for Wave-3 examples. Editor,
/// help, resource, and compiler domain logic remains in existing framework components.
/// </summary>
public abstract class Wave3Application : TApplication
{
    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private bool _quitIssued;
    private TView? _descriptionView;
    private TView? _resultView;

    /// <summary>Initialisiert die gemeinsame Anwendungsshell. / Initializes the shared application shell.</summary>
    protected Wave3Application(TRect bounds, bool headless) : base(bounds) => _headless = headless;

    /// <summary>Letzter sichtbarer Haupttyp. / Last visible main view kind.</summary>
    public string LastVisibleComponentKind { get; protected set; } = string.Empty;

    /// <summary>Letzte stabile Proof-Region. / Last stable proof region.</summary>
    public TRect LastVisibleRegion { get; protected set; }

    /// <summary>Letzter Statuszeilentext. / Last status-line text.</summary>
    public string LastStatusMessage { get; protected set; } = string.Empty;

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

        if (_headless && !_quitIssued)
        {
            _quitIssued = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave3StatusLine(bounds, "Wave 3: ready | Help -> Description | ^Q Quit");

    /// <summary>Aktualisiert den sichtbaren kurzen Zustand. / Updates the visible short state.</summary>
    protected void SetWave3Status(string example, string state)
    {
        LastStatusMessage = $"{example}: {state} | Help -> Description | ^Q Quit";
        if (StatusLine is Wave3StatusLine status)
        {
            status.SetMessage(LastStatusMessage);
        }
    }

    /// <summary>Merkt eine konkrete Hauptansicht als Proof-Ziel. / Records a concrete main view as proof target.</summary>
    protected void SetWave3Visible(TView view, string kind)
    {
        LastVisibleComponentKind = kind;
        LastVisibleRegion = Desktop is null ? default : Wave3Runtime.ScreenRegion(Desktop, view);
    }

    /// <summary>Zeigt eine zweisprachige Beschreibung. / Shows a bilingual description.</summary>
    protected void ShowWave3Description(string example, string body)
    {
        if (Desktop is null)
        {
            return;
        }

        if (_descriptionView?.Owner == Desktop)
        {
            Desktop.Remove(_descriptionView);
        }

        TRect region = Wave3Runtime.MainRegion(Desktop, 66, 13);
        TWindow window = new($"{example} Description", region.A.X, region.A.Y, region.Width, region.Height);
        window.Insert(new TStaticText(new TRect(2, 2, Math.Max(3, region.Width - 2), Math.Max(3, region.Height - 1)), body));
        Desktop.Insert(window);
        _descriptionView = window;
        SetWave3Visible(window, "TWindow");
        SetWave3Status(example, "description visible");
    }

    /// <summary>Zeigt einen ersetzbaren sichtbaren Ergebnisbereich. / Shows a replaceable visible result area.</summary>
    protected void ShowWave3Result(string example, string title, string body, string status)
    {
        if (Desktop is null)
        {
            return;
        }

        if (_resultView?.Owner == Desktop)
        {
            Desktop.Remove(_resultView);
        }

        TRect region = Wave3Runtime.MainRegion(Desktop, 66, 14);
        TWindow window = new(title, region.A.X, region.A.Y, region.Width, region.Height);
        window.Insert(new TStaticText(new TRect(2, 2, Math.Max(3, region.Width - 2), Math.Max(3, region.Height - 1)), body));
        Desktop.Insert(window);
        Desktop.SetFocus(window);
        _resultView = window;
        SetWave3Visible(window, "TWindow");
        SetWave3Status(example, status);
    }
}

/// <summary>Hilfen für wiederholte Wave-3-Präsentation. / Helpers for repeated Wave-3 presentation.</summary>
public static class Wave3Runtime
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
