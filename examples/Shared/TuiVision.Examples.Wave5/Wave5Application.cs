// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Wave5;

internal sealed class Wave5StatusLine : TStatusLine
{
    public Wave5StatusLine(TRect bounds, string message) : base(bounds) => Message = message;

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

        for (int x = 0; x < Size.X; x++)
        {
            buffer.TrySetCell(Origin.X + x, Origin.Y, new TConsoleCell(' ', ConsoleColor.Black, ConsoleColor.Cyan));
        }

        string text = Message.Length <= Size.X ? Message : Message[..Size.X];
        buffer.WriteText(Origin.X, Origin.Y, text.AsSpan(), ConsoleColor.Yellow, ConsoleColor.Cyan);
    }
}

/// <summary>
/// Gemeinsame, deterministische Anwendungsshell der funktionalen Wave-5-Beispiele.
///
/// Shared deterministic application shell for the functional Wave-5 examples.
/// </summary>
public abstract class Wave5Application : TApplication
{
    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private TView? _visibleView;

    /// <summary>Initialisiert die gemeinsame Anwendungsshell. / Initializes the shared application shell.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den kontrollierten Smoke-Pfad. / Enables the controlled smoke path.</param>
    protected Wave5Application(TRect bounds, bool headless) : base(bounds) => _headless = headless;

    /// <summary>Letzter sichtbarer Haupttyp. / Last visible main view type.</summary>
    public string LastVisibleComponentKind { get; protected set; } = string.Empty;

    /// <summary>Letzte stabile Proof-Region. / Last stable proof region.</summary>
    public TRect LastVisibleRegion { get; protected set; }

    /// <summary>Letzter sichtbarer Status. / Last visible status.</summary>
    public string LastStatusMessage { get; protected set; } = string.Empty;

    /// <summary>Letzter sichtbarer Inhalt. / Last visible content.</summary>
    public string LastVisibleText { get; protected set; } = string.Empty;

    /// <summary>Ob der kontrollierte Quit erzeugt wurde. / Whether controlled quit was issued.</summary>
    public bool QuitIssued { get; private set; }

    /// <summary>Fügt deterministische Ereignisse hinzu. / Adds deterministic events.</summary>
    /// <param name="events">Ereignisse für den App-Loop. / Events for the app loop.</param>
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
        new Wave5StatusLine(bounds, "Wave 5 Stage 1 | Ctrl+Q Quit");

    /// <summary>
    /// Ersetzt den sichtbaren Hauptinhalt und hält seine Bildschirmregion als
    /// stabilen Cell-Proof fest.
    ///
    /// Replaces the visible main content and records its screen region as a
    /// stable cell-proof boundary.
    /// </summary>
    /// <param name="title">Fenstertitel. / Window title.</param>
    /// <param name="text">Sichtbarer Inhalt. / Visible content.</param>
    protected void ShowContent(string title, string text)
    {
        if (Desktop is null)
        {
            return;
        }

        int width = Math.Clamp(68, 14, Math.Max(14, Desktop.Size.X - 4));
        int height = Math.Clamp(16, 7, Math.Max(7, Desktop.Size.Y - 3));
        TWindow window = new(title, 2, 1, width, height);
        window.Insert(new TStaticText(new TRect(2, 2, Math.Max(3, width - 2), Math.Max(3, height - 1)), text));
        ShowView(window, nameof(TWindow), text);
    }

    /// <summary>Ersetzt den Hauptinhalt durch eine konkrete View. / Replaces the main content with a concrete view.</summary>
    /// <param name="view">Neue View. / New view.</param>
    /// <param name="kind">Stabiler Typname. / Stable type name.</param>
    /// <param name="visibleText">Textorientierter Zustand. / Text-first state.</param>
    protected void ShowView(TView view, string kind, string visibleText)
    {
        if (Desktop is null)
        {
            return;
        }

        if (_visibleView?.Owner == Desktop)
        {
            Desktop.Remove(_visibleView);
        }

        if (view.Owner != Desktop)
        {
            Desktop.Insert(view);
        }

        Desktop.SetFocus(view);
        TrackVisibleView(view, kind);
        LastVisibleText = visibleText;
    }

    /// <summary>Merkt eine bereits eingefügte konkrete View als Proof-Ziel. / Records an already inserted concrete view as the proof target.</summary>
    /// <param name="view">Sichtbare View. / Visible view.</param>
    /// <param name="kind">Stabiler Typname. / Stable type name.</param>
    protected void TrackVisibleView(TView view, string kind)
    {
        if (Desktop is null)
        {
            return;
        }

        _visibleView = view;
        LastVisibleComponentKind = kind;
        LastVisibleRegion = ScreenRegion(Desktop, view);
    }

    /// <summary>Aktualisiert die textorientierte Statuszeile. / Updates the text-first status line.</summary>
    /// <param name="example">Beispielname. / Example name.</param>
    /// <param name="state">Kurzer Zustand. / Short state.</param>
    protected void SetStatus(string example, string state)
    {
        LastStatusMessage = $"{example}: {state} | Ctrl+Q Quit";
        if (StatusLine is Wave5StatusLine statusLine)
        {
            statusLine.SetMessage(LastStatusMessage);
        }
    }

    private static TRect ScreenRegion(TGroup owner, TView view)
    {
        TRect bounds = view.GetBounds();
        return new TRect(
            owner.Origin.X + bounds.A.X,
            owner.Origin.Y + bounds.A.Y,
            owner.Origin.X + bounds.B.X,
            owner.Origin.Y + bounds.B.Y);
    }
}

/// <summary>Startet ein Wave-5-Beispiel über den normalen oder kontrollierten CLI-Pfad. / Starts a Wave-5 example through the normal or controlled CLI path.</summary>
public static class Wave5ConsoleHost
{
    /// <summary>Startet die angegebene Anwendung. / Starts the supplied application.</summary>
    /// <param name="args">CLI-Argumente; `--smoke` aktiviert den kontrollierten Exit. / CLI arguments; `--smoke` enables controlled exit.</param>
    /// <param name="factory">Anwendungsfabrik. / Application factory.</param>
    public static void Run(string[] args, Func<TRect, bool, Wave5Application> factory)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentNullException.ThrowIfNull(factory);
        bool headless = args.Contains("--smoke", StringComparer.Ordinal);
        int width;
        int height;
        try
        {
            width = Console.WindowWidth;
            height = Console.WindowHeight;
        }
        catch
        {
            width = 80;
            height = 25;
        }

        if (width <= 0)
        {
            width = 80;
        }

        if (height <= 0)
        {
            height = 25;
        }

        factory(new TRect(0, 0, width, height), headless).Run();
    }
}
