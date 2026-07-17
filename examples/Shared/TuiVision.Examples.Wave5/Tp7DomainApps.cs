// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;
using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Drivers.Console;

namespace TuiVision.Examples.Wave5;

/// <summary>
/// Rendert eine fokussierbare, textorientierte Matrix aus einer Zustandsquelle.
/// / Renders a focusable text-first matrix from a state provider.
/// </summary>
internal sealed class Wave5GridView : TView
{
    private readonly Func<IReadOnlyList<string>> _lineProvider;

    /// <summary>Initialisiert die Matrixansicht. / Initializes the matrix view.</summary>
    /// <param name="bounds">Sichtbare Grenzen. / Visible bounds.</param>
    /// <param name="lineProvider">Aktuelle Textzeilen. / Current text lines.</param>
    public Wave5GridView(TRect bounds, Func<IReadOnlyList<string>> lineProvider) : base(bounds)
    {
        _lineProvider = lineProvider ?? throw new ArgumentNullException(nameof(lineProvider));
        Options |= TViewOptions.Selectable;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        for (int y = 0; y < Size.Y; y++)
        {
            buffer.WriteText(Origin.X, Origin.Y + y, new string(' ', Size.X).AsSpan());
        }

        IReadOnlyList<string> lines = _lineProvider();
        for (int y = 0; y < Math.Min(Size.Y, lines.Count); y++)
        {
            string line = lines[y];
            buffer.WriteText(
                Origin.X,
                Origin.Y + y,
                line.AsSpan(0, Math.Min(Size.X, line.Length)));
        }
    }
}

/// <summary>Interaktives TP7-ASCII-Tabellenbeispiel. / Interactive TP7 ASCII table example.</summary>
public sealed class Tp7AsciiTableApp : Wave5Application
{
    private const byte ScanHome = 0x47;
    private const byte ScanUp = 0x48;
    private const byte ScanPageUp = 0x49;
    private const byte ScanLeft = 0x4B;
    private const byte ScanRight = 0x4D;
    private const byte ScanEnd = 0x4F;
    private const byte ScanDown = 0x50;
    private const byte ScanPageDown = 0x51;

    private readonly Wave5GridView _grid;

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
        _grid = CreateGrid(BuildAsciiLines);
        ShowView(_grid, nameof(Wave5GridView), BuildAsciiText());
        FocusedControlKind = nameof(Wave5GridView);
        SetStatus("Tp7AsciiTable", "ready");
    }

    /// <summary>ASCII-Zustand. / ASCII state.</summary>
    public Tp7AsciiState Ascii { get; }

    /// <summary>Anzahl sichtbarer Byte-Zellen. / Number of visible byte cells.</summary>
    public int GridCellCount => 256;

    /// <summary>Anfänglicher fokussierter Control-Typ. / Initial focused control type.</summary>
    public string FocusedControlKind { get; }

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

        if (@event.What == TEventKind.KeyDown
            && TryMapAsciiKey(@event.KeyDown.ScanCode, out int? directSelection, out int delta))
        {
            LastSelectionRejected = directSelection.HasValue
                ? !Ascii.Select(directSelection.Value)
                : false;
            if (!directSelection.HasValue)
            {
                Ascii.Move(delta);
            }

            RefreshAscii();
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    /// <inheritdoc />
    protected override string BuildDescriptionText() =>
        """
        Historischer Lernzweck: Die Byte-Tabelle macht alle 256 Werte und die aktuelle Auswahl sichtbar.
        Historical learning purpose: the byte table exposes all 256 values and the current selection.
        Tastatur: Pfeiltasten bewegen eine Zelle, PageUp/PageDown springen vier Zeilen, Home/End wählen die Grenze, F1 öffnet diese Hilfe.
        Keyboard: arrows move one cell, PageUp/PageDown jump four rows, Home/End select a boundary, and F1 opens this help.
        Die moderne Abweichung ist ein kompaktes 16x16-Textgitter statt der historischen 32-Spalten-Anordnung.
        The modern deviation is a compact 16x16 text grid instead of the historical 32-column arrangement.
        Die Auswahl bleibt strikt auf 0 bis 255 begrenzt; ungültige Direktwerte verändern den letzten gültigen Zustand nicht.
        Selection stays strictly within 0 through 255; invalid direct values do not change the last valid state.
        Der App-Loop-Smoke beweist Fokus, Navigation, Status und Zellen, nicht die Schriftform eines Host-Terminals.
        The app-loop smoke proves focus, navigation, status, and cells, not a host terminal's glyph shape.
        """;

    private void RefreshAscii()
    {
        string state = LastSelectionRejected
            ? $"rejected code; kept={Ascii.Code} hex={Ascii.Hex}"
            : $"code={Ascii.Code} hex={Ascii.Hex}";
        _grid.DrawView();
        LastVisibleText = BuildAsciiText();
        SetStatus("Tp7AsciiTable", state);
    }

    private Wave5GridView CreateGrid(Func<IReadOnlyList<string>> provider)
    {
        int right = Math.Max(2, Desktop?.Size.X ?? 2);
        int bottom = Math.Max(2, Desktop?.Size.Y ?? 2);
        return new Wave5GridView(new TRect(0, 0, right, bottom), provider);
    }

    private IReadOnlyList<string> BuildAsciiLines()
    {
        List<string> lines =
        [
            "TP7 ASCII Table",
            $"Selected: {Ascii.Code:D3} / {Ascii.Hex} / {Ascii.Character}"
        ];
        for (int row = 0; row < 16; row++)
        {
            lines.Add(string.Concat(Enumerable.Range(row * 16, 16).Select(
                code => code == Ascii.Code
                    ? $">{code:X2}"
                    : $" {code:X2}")));
        }

        return lines;
    }

    private string BuildAsciiText() =>
        $"TP7 ASCII Table\nCode: {Ascii.Code} Hex: {Ascii.Hex} Char: {Ascii.Character}\nBoundary: 0..255";

    private static bool TryMapAsciiKey(byte scanCode, out int? directSelection, out int delta)
    {
        directSelection = scanCode switch
        {
            ScanHome => 0,
            ScanEnd => 255,
            _ => null
        };
        delta = scanCode switch
        {
            ScanLeft => -1,
            ScanRight => 1,
            ScanUp => -16,
            ScanDown => 16,
            ScanPageUp => -64,
            ScanPageDown => 64,
            _ => 0
        };
        return directSelection.HasValue || delta != 0;
    }
}

/// <summary>Interaktives TP7-Kalenderbeispiel. / Interactive TP7 calendar example.</summary>
public sealed class Tp7CalendarApp : Wave5Application
{
    private const byte ScanUp = 0x48;
    private const byte ScanPageUp = 0x49;
    private const byte ScanLeft = 0x4B;
    private const byte ScanRight = 0x4D;
    private const byte ScanDown = 0x50;
    private const byte ScanPageDown = 0x51;

    private readonly Wave5GridView _grid;

    /// <summary>Verschiebt den Monat um den `Info`-Integer. / Moves the month by the `Info` integer.</summary>
    public const ushort CmMoveMonth = 32511;

    /// <summary>Initialisiert den Kalender mit einer festen Fixture. / Initializes the calendar with a fixed fixture.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    public Tp7CalendarApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        Calendar = new Tp7CalendarState(new DateOnly(2026, 12, 1));
        SelectedDay = 1;
        _grid = CreateGrid(BuildCalendarLines);
        ShowView(_grid, nameof(Wave5GridView), BuildCalendarText());
        FocusedControlKind = nameof(Wave5GridView);
        SetStatus("Tp7Calendar", "ready");
    }

    /// <summary>Kalenderzustand. / Calendar state.</summary>
    public Tp7CalendarState Calendar { get; }

    /// <summary>Ausgewählter Tag im dargestellten Monat. / Selected day in the displayed month.</summary>
    public int SelectedDay { get; private set; }

    /// <summary>Anfänglicher fokussierter Control-Typ. / Initial focused control type.</summary>
    public string FocusedControlKind { get; }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command
            && @event.Message.Command == CmMoveMonth
            && @event.Message.Info is int delta)
        {
            Calendar.MoveMonth(delta);
            ClampSelectedDay();
            RefreshCalendar();
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.KeyDown)
        {
            int dayDelta = @event.KeyDown.ScanCode switch
            {
                ScanLeft => -1,
                ScanRight => 1,
                ScanUp => -7,
                ScanDown => 7,
                _ => 0
            };
            int monthDelta = @event.KeyDown.ScanCode switch
            {
                ScanPageUp => -1,
                ScanPageDown => 1,
                _ => 0
            };
            if (dayDelta != 0 || monthDelta != 0)
            {
                if (monthDelta != 0)
                {
                    Calendar.MoveMonth(monthDelta);
                    ClampSelectedDay();
                }
                else
                {
                    SelectedDay = Math.Clamp(
                        SelectedDay + dayDelta,
                        1,
                        DateTime.DaysInMonth(Calendar.SelectedMonth.Year, Calendar.SelectedMonth.Month));
                }

                RefreshCalendar();
                @event.Clear();
                return;
            }
        }

        base.HandleEvent(@event);
    }

    /// <inheritdoc />
    protected override string BuildDescriptionText() =>
        """
        Historischer Lernzweck: Der Monatskalender verbindet eine sichtbare Tagesmatrix mit Monatsnavigation.
        Historical learning purpose: the month calendar combines a visible day matrix with month navigation.
        Tastatur: Pfeiltasten wählen einen Tag, PageUp und PageDown wechseln den Monat, F1 öffnet diese Hilfe.
        Keyboard: arrows select a day, PageUp and PageDown change the month, and F1 opens this help.
        Die moderne Abweichung verwendet eine feste Fixture und eine explizite Tagesauswahl statt Host-Datum und Gebietsschema.
        The modern deviation uses a fixed fixture and explicit day selection instead of host date and locale.
        Host-Datum und Host-Gebietsschema beeinflussen weder Startmonat noch sichtbare Bezeichnungen.
        Host date and host locale affect neither the initial month nor visible labels.
        Der App-Loop-Smoke beweist Tages- und Monatsnavigation, Fokus, Status und Zellen; er beweist keine Host-Kalenderintegration.
        The app-loop smoke proves day and month navigation, focus, status, and cells; it does not prove host calendar integration.
        """;

    private Wave5GridView CreateGrid(Func<IReadOnlyList<string>> provider)
    {
        int right = Math.Max(2, Desktop?.Size.X ?? 2);
        int bottom = Math.Max(2, Desktop?.Size.Y ?? 2);
        return new Wave5GridView(new TRect(0, 0, right, bottom), provider);
    }

    private IReadOnlyList<string> BuildCalendarLines()
    {
        DateOnly month = Calendar.SelectedMonth;
        int dayCount = DateTime.DaysInMonth(month.Year, month.Month);
        int mondayOffset = ((int)month.DayOfWeek + 6) % 7;
        List<string> lines =
        [
            "TP7 Calendar",
            $"Month: {Calendar.Label}  Selected: {Calendar.Label}-{SelectedDay:D2}",
            " Mo Tu We Th Fr Sa Su"
        ];
        for (int week = 0; week < 6; week++)
        {
            string row = string.Empty;
            for (int column = 0; column < 7; column++)
            {
                int day = (week * 7) + column - mondayOffset + 1;
                row += day is < 1 || day > dayCount
                    ? "   "
                    : day == SelectedDay
                        ? $">{day:D2}"
                        : $" {day:D2}";
            }

            lines.Add(row);
        }

        return lines;
    }

    private string BuildCalendarText() =>
        $"TP7 Calendar\nMonth: {Calendar.Label}\nSelected: {Calendar.Label}-{SelectedDay:D2}";

    private void ClampSelectedDay() =>
        SelectedDay = Math.Min(
            SelectedDay,
            DateTime.DaysInMonth(Calendar.SelectedMonth.Year, Calendar.SelectedMonth.Month));

    private void RefreshCalendar()
    {
        _grid.DrawView();
        LastVisibleText = BuildCalendarText();
        SetStatus("Tp7Calendar", $"date={Calendar.Label}-{SelectedDay:D2}");
    }
}

/// <summary>Interaktives TP7-Puzzlebeispiel. / Interactive TP7 puzzle example.</summary>
public sealed class Tp7PuzzleApp : Wave5Application
{
    private const byte ScanEnter = 0x1C;
    private const byte ScanUp = 0x48;
    private const byte ScanLeft = 0x4B;
    private const byte ScanRight = 0x4D;
    private const byte ScanDown = 0x50;

    private readonly Wave5GridView _grid;
    private int _focusedIndex;

    /// <summary>Bewegt den Stein aus dem `Info`-Integer. / Moves the tile from the `Info` integer.</summary>
    public const ushort CmMoveTile = 32521;

    /// <summary>Initialisiert das feste Puzzle. / Initializes the fixed puzzle.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    public Tp7PuzzleApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        Puzzle = new Tp7PuzzleState();
        _focusedIndex = Array.IndexOf(Puzzle.Tiles.ToArray(), 15);
        _grid = CreateGrid(BuildPuzzleLines);
        ShowView(_grid, nameof(Wave5GridView), BuildPuzzleText());
        FocusedControlKind = nameof(Wave5GridView);
        SetStatus("Tp7Puzzle", "ready");
    }

    /// <summary>Puzzlezustand. / Puzzle state.</summary>
    public Tp7PuzzleState Puzzle { get; }

    /// <summary>Anzahl sichtbarer Board-Zellen. / Number of visible board cells.</summary>
    public int GridCellCount => 16;

    /// <summary>Aktuell fokussierter Stein; null steht für das Leerfeld. / Currently focused tile; zero represents the blank.</summary>
    public int FocusedTile => Puzzle.Tiles[_focusedIndex];

    /// <summary>Anfänglicher fokussierter Control-Typ. / Initial focused control type.</summary>
    public string FocusedControlKind { get; }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command
            && @event.Message.Command == CmMoveTile
            && @event.Message.Info is int tile)
        {
            MoveTile(tile);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.KeyDown)
        {
            int columnDelta = @event.KeyDown.ScanCode switch
            {
                ScanLeft => -1,
                ScanRight => 1,
                _ => 0
            };
            int rowDelta = @event.KeyDown.ScanCode switch
            {
                ScanUp => -1,
                ScanDown => 1,
                _ => 0
            };
            if (columnDelta != 0 || rowDelta != 0)
            {
                int row = _focusedIndex / 4;
                int column = _focusedIndex % 4;
                _focusedIndex = (Math.Clamp(row + rowDelta, 0, 3) * 4)
                    + Math.Clamp(column + columnDelta, 0, 3);
                RefreshPuzzle($"focus={FocusedTile}");
                @event.Clear();
                return;
            }

            if (@event.KeyDown.ScanCode == ScanEnter || @event.KeyDown.CharCode == '\r')
            {
                MoveTile(FocusedTile);
                @event.Clear();
                return;
            }
        }

        base.HandleEvent(@event);
    }

    /// <inheritdoc />
    protected override string BuildDescriptionText() =>
        """
        Historischer Lernzweck: Das 4x4-Schiebepuzzle macht Fokus, Leerfeld und gültige Nachbarschaftszüge sichtbar.
        Historical learning purpose: the 4x4 sliding puzzle exposes focus, the blank, and valid adjacent moves.
        Tastatur: Pfeiltasten wählen einen Stein, Enter versucht den Zug, F1 öffnet diese Hilfe.
        Keyboard: arrows select a tile, Enter attempts the move, and F1 opens this help.
        Die moderne Abweichung nutzt eine deterministische Startbelegung statt einer zufälligen Mischung.
        The modern deviation uses a deterministic initial board instead of random shuffling.
        Nur zum Leerfeld benachbarte Steine bewegen sich; eine Ablehnung bewahrt das vollständige Board.
        Only tiles adjacent to the blank move; rejection preserves the complete board.
        Der App-Loop-Smoke beweist Fokus, gültigen Zug, Ablehnung, Status und Zellen, nicht eine zufällige Lösbarkeitsanalyse.
        The app-loop smoke proves focus, a valid move, rejection, status, and cells, not randomized solvability analysis.
        """;

    private Wave5GridView CreateGrid(Func<IReadOnlyList<string>> provider)
    {
        int right = Math.Max(2, Desktop?.Size.X ?? 2);
        int bottom = Math.Max(2, Desktop?.Size.Y ?? 2);
        return new Wave5GridView(new TRect(0, 0, right, bottom), provider);
    }

    private IReadOnlyList<string> BuildPuzzleLines()
    {
        List<string> lines =
        [
            "TP7 Puzzle",
            $"Focused tile: {(FocusedTile == 0 ? "__" : FocusedTile.ToString("D2", CultureInfo.InvariantCulture))}"
        ];
        lines.AddRange(Puzzle.FormatBoard().Split('\n'));
        lines.Add($"Moves: {Puzzle.MoveCount}  Last rejected: {Puzzle.LastMoveRejected}");
        return lines;
    }

    private string BuildPuzzleText() =>
        $"TP7 Puzzle\n{Puzzle.FormatBoard()}\nFocused: {FocusedTile}\nMoves: {Puzzle.MoveCount}";

    private void MoveTile(int tile)
    {
        int blankIndex = Array.IndexOf(Puzzle.Tiles.ToArray(), 0);
        bool accepted = Puzzle.MoveTile(tile);
        if (accepted)
        {
            _focusedIndex = blankIndex;
        }

        RefreshPuzzle(accepted ? $"moved={tile}" : $"rejected tile={tile}");
    }

    private void RefreshPuzzle(string state)
    {
        _grid.DrawView();
        LastVisibleText = BuildPuzzleText();
        SetStatus("Tp7Puzzle", state);
    }
}

/// <summary>Interaktives TP7-Mausdialogbeispiel. / Interactive TP7 mouse-dialog example.</summary>
public sealed class Tp7MouseDialogApp : Wave5Application
{
    private const byte ScanEnter = 0x1C;
    private const byte ScanUp = 0x48;
    private const byte ScanLeft = 0x4B;
    private const byte ScanRight = 0x4D;
    private const byte ScanDown = 0x50;

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
        RefreshMouse("ready");
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

    /// <summary>Anfänglicher fokussierter Control-Typ. / Initial focused control type.</summary>
    public string FocusedControlKind { get; private set; } = string.Empty;

    /// <summary>Anzahl echter Dialog-Controls. / Number of real dialog controls.</summary>
    public int VisibleControlCount { get; private set; }

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

        if (@event.What == TEventKind.KeyDown)
        {
            if (@event.KeyDown.CharCode == ' ')
            {
                ButtonsReversed = !ButtonsReversed;
                RefreshMouse($"buttons-reversed={ButtonsReversed}");
                @event.Clear();
                return;
            }

            int delayDelta = @event.KeyDown.ScanCode switch
            {
                ScanRight or ScanUp => 1,
                ScanLeft or ScanDown => -1,
                _ => 0
            };
            if (delayDelta != 0)
            {
                DoubleClickDelayStep = Math.Clamp(DoubleClickDelayStep + delayDelta, 1, 10);
                RefreshMouse($"delay-step={DoubleClickDelayStep}");
                @event.Clear();
                return;
            }

            if (@event.KeyDown.ScanCode == ScanEnter || @event.KeyDown.CharCode == '\r')
            {
                Activate("Keyboard fallback");
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

    /// <inheritdoc />
    protected override string BuildDescriptionText() =>
        """
        Historischer Lernzweck: Der Dialog zeigt Mauseinstellungen und eine sichtbare Klick-Aktivierung.
        Historical learning purpose: the dialog shows mouse settings and visible click activation.
        Tastatur: Tab wechselt Controls, Space dreht die lokale Button-Reihenfolge, Pfeiltasten ändern die Verzögerungsstufe, Enter aktiviert, F1 öffnet diese Hilfe.
        Keyboard: Tab changes controls, Space reverses the local button order, arrows change the delay step, Enter activates, and F1 opens this help.
        Die moderne Abweichung hält alle Einstellungen lokal und capability-bewusst statt den Host zu verändern.
        The modern deviation keeps every setting local and capability-aware instead of mutating the host.
        Die Capability entscheidet ehrlich über den optionalen Mauspfad; der vollständige Tastaturfallback bleibt immer verfügbar.
        Capability honestly controls the optional mouse path; the complete keyboard fallback always remains available.
        Der App-Loop-Smoke beweist Controls, Fokus, Capability, Aktivierung, Status und Zellen; er beweist keine Host-Konfigurationsänderung.
        The app-loop smoke proves controls, focus, capability, activation, status, and cells; it does not prove host configuration changes.
        """;

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
        if (Desktop is null)
        {
            return;
        }

        int right = Math.Max(30, Desktop.Size.X - 1);
        int bottom = Math.Max(11, Desktop.Size.Y);
        TDialog dialog = new(new TRect(1, 0, right, bottom), "TP7 Mouse Dialog");
        TCheckBoxes settings = new(
            new TRect(2, 2, Math.Max(20, dialog.Size.X - 2), 4),
            ["Reverse buttons"]);
        settings.Value = ButtonsReversed ? 1u : 0u;
        TScrollBar delay = new(
            new TRect(2, 5, Math.Max(12, dialog.Size.X - 2), 6),
            horizontal: true);
        delay.SetParams(DoubleClickDelayStep, 1, 10, 2, 1);
        TButton activate = new(
            new TRect(2, 7, Math.Min(dialog.Size.X - 2, 24), 8),
            "~A~ctivate / ~A~ktivieren",
            CmKeyboardActivate,
            TButtonFlags.bfDefault);
        TStaticText details = new(
            new TRect(2, 1, Math.Max(3, dialog.Size.X - 2), Math.Max(9, dialog.Size.Y - 1)),
            $"Capability: {Capability}\n" +
            $"Delay step: {DoubleClickDelayStep}\n" +
            $"Keyboard: Space, arrows, Enter\n" +
            $"Activation: {LastActivation}\nHost mutation: false");
        dialog.Insert(details);
        dialog.Insert(settings);
        dialog.Insert(delay);
        dialog.Insert(activate);

        string visibleText =
            $"TP7 Mouse Dialog\nCapability: {Capability}\nActivation: {LastActivation}\n" +
            $"Delay step: {DoubleClickDelayStep}\nButtons reversed: {ButtonsReversed}\nHost mutation: false";
        ShowView(dialog, nameof(TDialog), visibleText);
        dialog.SetFocus(settings);
        FocusedControlKind = dialog.Current?.GetType().Name ?? string.Empty;
        VisibleControlCount = 4;
        SetStatus("Tp7MouseDialog", state);
    }
}
