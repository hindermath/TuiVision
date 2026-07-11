// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.Desklogo;

/// <summary>
/// Minimale TApplication-Unterklasse für das Desklogo-Beispiel.
/// Verwendet einen benutzerdefinierten Desktop, der ein ASCII-Logo darstellt.
///
/// Minimal TApplication subclass for the Desklogo example.
/// Uses a custom desktop that renders an ASCII logo.
/// </summary>
public class DesklogoApp : TApplication
{
    /// <summary>Help-Description-Befehl. / Help description command.</summary>
    public const ushort CmDescription = 17001;

    // Headless-Modus für automatisierte Smoke-Tests.
    // Headless mode for automated smoke tests.
    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private bool _headlessEventFired;
    private TView? _descriptionView;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="DesklogoApp"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="DesklogoApp"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">
    /// Wenn <c>true</c>, liefert <see cref="GetEvent"/> sofort einen Quit-Befehl
    /// und ermöglicht so das in-process Testen ohne Konsoleninteraktion.
    ///
    /// When <c>true</c>, <see cref="GetEvent"/> immediately delivers a quit command,
    /// enabling in-process testing without console interaction.
    /// </param>
    public DesklogoApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        LogoDesktop = (DesklogoDesktop)Desktop!;
        LastVisibleComponentKind = nameof(DesklogoDesktop);
        LastVisibleRegion = Wave1Runtime.ScreenRegion(this, LogoDesktop);
        SetStatus(LogoDesktop.Size.X < LogoDesktop.LogoLines.Max(line => line.Length) ? "logo clipped" : "embedded logo ready");
    }

    /// <summary>Der sichtbare Logo-Desktop. / The visible logo desktop.</summary>
    public DesklogoDesktop LogoDesktop { get; }

    /// <summary>Letzter sichtbarer Komponententyp. / Last visible component type.</summary>
    public string LastVisibleComponentKind { get; private set; }

    /// <summary>Letzte stabile sichtbare Region. / Last stable visible region.</summary>
    public TRect LastVisibleRegion { get; private set; }

    /// <summary>Letzter Statuszeilentext. / Last status-line message.</summary>
    public string LastStatusMessage { get; private set; } = string.Empty;

    /// <summary>Fuegt deterministische App-Loop-Ereignisse hinzu. / Adds deterministic app-loop events.</summary>
    public void QueueEvents(IEnumerable<TEvent> events)
    {
        foreach (TEvent @event in events)
        {
            _scriptedEvents.Enqueue(@event);
        }
    }

    /// <summary>
    /// Erstellt den benutzerdefinierten <see cref="DesklogoDesktop"/> für die Anwendungsshell.
    ///
    /// Creates the custom <see cref="DesklogoDesktop"/> for the application shell.
    /// </summary>
    /// <param name="bounds">Die Grenzen des Desktops. / The bounds of the desktop.</param>
    /// <returns>
    /// Eine neue <see cref="DesklogoDesktop"/>-Instanz.
    /// A new <see cref="DesklogoDesktop"/> instance.
    /// </returns>
    protected override TDesktop InitDesktop(TRect bounds) => new DesklogoDesktop(bounds);

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds) => new(bounds)
    {
        Menu = Wave1Runtime.HelpMenu(CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit))
    };

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave1StatusLine(bounds, Wave1Runtime.Status("Desklogo", "embedded logo"));

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmDescription)
        {
            ShowDescription();
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    /// <summary>
    /// Ruft das nächste Ereignis ab.
    /// Im Headless-Modus wird beim ersten Aufruf ein Quit-Befehl zurückgegeben.
    ///
    /// Retrieves the next event.
    /// In headless mode, a quit command is returned on the first call.
    /// </summary>
    /// <param name="event">Das abgerufene Ereignis. / The retrieved event.</param>
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && _scriptedEvents.Count > 0)
        {
            @event = _scriptedEvents.Dequeue();
            return;
        }

        // Headless-Pfad: einmaliges Quit-Signal für den Smoke-Test / Headless path: one-time quit signal for smoke test
        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    private void ShowDescription()
    {
        if (Desktop is null)
        {
            return;
        }

        if (_descriptionView?.Owner == Desktop)
        {
            Desktop.Remove(_descriptionView);
        }

        const string description =
            "Desklogo description: Das eingebettete Logo ersetzt die historischen Generatorwerkzeuge und bleibt bei kleinen Terminals kontrolliert abgeschnitten. / " +
            "The embedded logo replaces the historical generator tools and remains safely clipped on small terminals.";
        _descriptionView = Wave1Runtime.CreateDescriptionWindow(Desktop, "Desklogo", description);
        if (_descriptionView is null)
        {
            return;
        }

        Desktop.Insert(_descriptionView);
        LastVisibleComponentKind = "TWindow";
        LastVisibleRegion = Wave1Runtime.ScreenRegion(Desktop, _descriptionView);
        SetStatus("description visible");
    }

    private void SetStatus(string state)
    {
        LastStatusMessage = Wave1Runtime.Status("Desklogo", state);
        Wave1Runtime.SetStatus(StatusLine, LastStatusMessage);
    }

    #region Lösung Übung 2 – Menüleiste hinzufügen / Solution Exercise 2 – Add a menu bar
    /*
     * Überschreibe InitMenuBar() in DesklogoApp, um eine Menüleiste mit einem Untermenü hinzuzufügen.
     * Override InitMenuBar() in DesklogoApp to add a menu bar with one submenu.
     *
     * protected override TMenuBar InitMenuBar(TRect bounds)
     * {
     *     return new TMenuBar(bounds,
     *         new TSubMenu("~D~esklogo", 0x2100,
     *             new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit, kbAltX)));
     * }
     */
    #endregion
}
