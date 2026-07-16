// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Drivers.Console;

namespace TuiVision.Examples.Wave5;

/// <summary>Funktionales TP7-ASCII-Tabellenbeispiel. / Functional TP7 ASCII table example.</summary>
public sealed class Tp7AsciiTableApp : Wave5Application
{
    /// <summary>Verschiebt die Auswahl um den `Info`-Integer. / Moves the selection by the `Info` integer.</summary>
    public const ushort CmMove = 32501;

    /// <summary>Wählt den Code aus dem `Info`-Integer. / Selects the code from the `Info` integer.</summary>
    public const ushort CmSelect = 32502;

    /// <summary>Initialisiert die ASCII-Tabelle. / Initializes the ASCII table.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    public Tp7AsciiTableApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        Ascii = new Tp7AsciiState();
        ShowContent("TP7 ASCII Table", $"TP7 ASCII Table\nCode: {Ascii.Code} Hex: {Ascii.Hex} Char: {Ascii.Character}");
        SetStatus("Tp7AsciiTable", "ready");
    }

    /// <summary>ASCII-Zustand. / ASCII state.</summary>
    public Tp7AsciiState Ascii { get; }

    /// <summary>Ob die letzte direkte Auswahl abgelehnt wurde. / Whether the last direct selection was rejected.</summary>
    public bool LastSelectionRejected { get; private set; }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command
            && @event.Message.Command is CmMove or CmSelect
            && @event.Message.Info is int value)
        {
            if (@event.Message.Command == CmMove)
            {
                Ascii.Move(value);
                LastSelectionRejected = false;
            }
            else
            {
                LastSelectionRejected = !Ascii.Select(value);
            }

            RefreshAscii();
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private void RefreshAscii()
    {
        string state = LastSelectionRejected
            ? $"rejected code; kept={Ascii.Code} hex={Ascii.Hex}"
            : $"code={Ascii.Code} hex={Ascii.Hex}";
        ShowContent(
            "TP7 ASCII Table",
            $"TP7 ASCII Table\nCode: {Ascii.Code} Hex: {Ascii.Hex} Char: {Ascii.Character}\nBoundary: 0..255");
        SetStatus("Tp7AsciiTable", state);
    }
}

/// <summary>Funktionales TP7-Kalenderbeispiel. / Functional TP7 calendar example.</summary>
public sealed class Tp7CalendarApp : Wave5Application
{
    /// <summary>Verschiebt den Monat um den `Info`-Integer. / Moves the month by the `Info` integer.</summary>
    public const ushort CmMoveMonth = 32511;

    /// <summary>Initialisiert den Kalender mit einer festen Fixture. / Initializes the calendar with a fixed fixture.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    public Tp7CalendarApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        Calendar = new Tp7CalendarState(new DateOnly(2026, 12, 1));
        ShowContent("TP7 Calendar", $"TP7 Calendar\nMonth: {Calendar.Label}");
        SetStatus("Tp7Calendar", "ready");
    }

    /// <summary>Kalenderzustand. / Calendar state.</summary>
    public Tp7CalendarState Calendar { get; }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command
            && @event.Message.Command == CmMoveMonth
            && @event.Message.Info is int delta)
        {
            Calendar.MoveMonth(delta);
            ShowContent("TP7 Calendar", $"TP7 Calendar\nMonth: {Calendar.Label}\nFixed fixture navigation");
            SetStatus("Tp7Calendar", $"month={Calendar.Label}");
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }
}

/// <summary>Funktionales TP7-Puzzlebeispiel. / Functional TP7 puzzle example.</summary>
public sealed class Tp7PuzzleApp : Wave5Application
{
    /// <summary>Bewegt den Stein aus dem `Info`-Integer. / Moves the tile from the `Info` integer.</summary>
    public const ushort CmMoveTile = 32521;

    /// <summary>Initialisiert das feste Puzzle. / Initializes the fixed puzzle.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    public Tp7PuzzleApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        Puzzle = new Tp7PuzzleState();
        ShowContent("TP7 Puzzle", $"TP7 Puzzle\n{Puzzle.FormatBoard()}");
        SetStatus("Tp7Puzzle", "ready");
    }

    /// <summary>Puzzlezustand. / Puzzle state.</summary>
    public Tp7PuzzleState Puzzle { get; }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command
            && @event.Message.Command == CmMoveTile
            && @event.Message.Info is int tile)
        {
            bool accepted = Puzzle.MoveTile(tile);
            ShowContent("TP7 Puzzle", $"TP7 Puzzle\n{Puzzle.FormatBoard()}\nMoves: {Puzzle.MoveCount}");
            SetStatus("Tp7Puzzle", accepted ? $"moved={tile}" : $"rejected tile={tile}");
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }
}

/// <summary>Funktionales TP7-Mausdialogbeispiel. / Functional TP7 mouse-dialog example.</summary>
public sealed class Tp7MouseDialogApp : Wave5Application
{
    /// <summary>Aktiviert den vollständigen Tastaturpfad. / Activates the complete keyboard path.</summary>
    public const ushort CmKeyboardActivate = 32531;

    /// <summary>Wechselt die lokale Button-Reihenfolge. / Toggles the local button order.</summary>
    public const ushort CmToggleButtons = 32532;

    /// <summary>Ändert die lokale Doppelklickstufe um den `Info`-Integer. / Changes the local double-click step by the `Info` integer.</summary>
    public const ushort CmAdjustDelay = 32533;

    /// <summary>Initialisiert den lokalen Mausdialogzustand. / Initializes the local mouse-dialog state.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    public Tp7MouseDialogApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        ShowContent("TP7 Mouse Dialog", "TP7 Mouse Dialog\nCapability: Unsupported\nKeyboard path available");
        SetStatus("Tp7MouseDialog", "ready");
    }

    /// <summary>Zuletzt beobachtete Capability. / Last observed capability.</summary>
    public ConsoleMouseCapabilityState Capability { get; private set; } = ConsoleMouseCapabilityState.Unsupported;

    /// <summary>Lokale Doppelklickstufe von 1 bis 10. / Local double-click step from 1 through 10.</summary>
    public int DoubleClickDelayStep { get; private set; } = 5;

    /// <summary>Lokale, nicht auf den Host geschriebene Button-Reihenfolge. / Local button order not written to the host.</summary>
    public bool ButtonsReversed { get; private set; }

    /// <summary>Letzter erfolgreicher Aktivierungspfad. / Last successful activation path.</summary>
    public string LastActivation { get; private set; } = string.Empty;

    /// <summary>Capability zum Zeitpunkt der letzten Aktivierung. / Capability at the last activation.</summary>
    public ConsoleMouseCapabilityState LastActivationCapability { get; private set; } = ConsoleMouseCapabilityState.Unsupported;

    /// <summary>Ob eine Mausinteraktion noch aktiv ist. / Whether a mouse interaction remains active.</summary>
    public bool InteractionActive { get; private set; }

    /// <summary>Hostzustand wurde nie verändert. / Host state was never changed.</summary>
    public bool HostMutationPerformed => false;

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Broadcast
            && @event.Message.Command == ShellCommandIds.cmMouseCapabilityChanged
            && @event.Message.Info is ConsoleMouseCapabilityState capability)
        {
            Capability = capability;
            if (capability != ConsoleMouseCapabilityState.Enabled)
            {
                // Capability-Verlust beendet nur den lokalen Interaktionszustand; Hosteinstellungen bleiben unangetastet.
                // Capability loss ends only local interaction state; host settings remain untouched.
                InteractionActive = false;
            }

            if (string.IsNullOrEmpty(LastActivation))
            {
                RefreshMouse($"capability={capability}");
            }

            base.HandleEvent(@event);
            return;
        }

        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmKeyboardActivate:
                    Activate("Keyboard fallback");
                    @event.Clear();
                    return;
                case CmToggleButtons:
                    ButtonsReversed = !ButtonsReversed;
                    RefreshMouse($"buttons-reversed={ButtonsReversed}");
                    @event.Clear();
                    return;
                case CmAdjustDelay when @event.Message.Info is int delta:
                    DoubleClickDelayStep = Math.Clamp(DoubleClickDelayStep + delta, 1, 10);
                    RefreshMouse($"delay-step={DoubleClickDelayStep}");
                    @event.Clear();
                    return;
            }
        }

        if (@event.What == TEventKind.MouseDown)
        {
            if (Capability == ConsoleMouseCapabilityState.Enabled)
            {
                InteractionActive = !@event.Mouse.DoubleClick;
                Activate(@event.Mouse.DoubleClick ? "Mouse double-click" : "Mouse press");
            }
            else
            {
                InteractionActive = false;
                RefreshMouse($"mouse unavailable; capability={Capability}");
            }

            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private void Activate(string path)
    {
        LastActivation = path;
        LastActivationCapability = Capability;
        if (path == "Mouse double-click")
        {
            InteractionActive = false;
        }

        RefreshMouse(path);
    }

    private void RefreshMouse(string state)
    {
        ShowContent(
            "TP7 Mouse Dialog",
            $"TP7 Mouse Dialog\nCapability: {Capability}\nActivation: {LastActivation}\n" +
            $"Delay step: {DoubleClickDelayStep}\nButtons reversed: {ButtonsReversed}\nHost mutation: false");
        SetStatus("Tp7MouseDialog", state);
    }
}
