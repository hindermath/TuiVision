// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;
using TuiVision.Core;

namespace TuiVision.Drivers.Console;

/// <summary>
/// Benennt das Ergebnis einer kontrollierten Terminalbeobachtung.
///
/// Names the outcome of a controlled terminal observation.
/// </summary>
public enum TerminalEmulationOutcome
{
    /// <summary>Die Beobachtung wurde vollständig angewendet. / The observation was applied completely.</summary>
    Accepted,

    /// <summary>Die Beobachtung war ungültig und wurde atomar abgelehnt. / The observation was invalid and rejected atomically.</summary>
    Rejected,

    /// <summary>Die Beobachtung liegt außerhalb des vereinbarten Subsets. / The observation is outside the accepted subset.</summary>
    Unsupported
}

/// <summary>
/// Benennt den Lebenszyklus einer Terminal-Sitzung.
///
/// Names the lifecycle of a terminal session.
/// </summary>
public enum TerminalSessionLifecycle
{
    /// <summary>Die Sitzung nimmt Beobachtungen an. / The session accepts observations.</summary>
    Active,

    /// <summary>Die Sitzung wurde kontrolliert geschlossen. / The session was closed in a controlled manner.</summary>
    Closed,

    /// <summary>Die Sitzung wurde freigegeben. / The session was disposed.</summary>
    Disposed
}

/// <summary>
/// Beschreibt das veröffentlichte Ergebnis einer Terminalbeobachtung.
///
/// Describes the published result of a terminal observation.
/// </summary>
/// <param name="Outcome">Akzeptanzstatus. / Acceptance outcome.</param>
/// <param name="Command">Kanonischer Befehlsname oder <c>None</c>. / Canonical command name or <c>None</c>.</param>
/// <param name="StateChanged">Gibt an, ob Zustand veröffentlicht wurde. / Indicates whether state was published.</param>
/// <param name="StatusText">Textorientierte Begründung. / Text-first rationale.</param>
public readonly record struct TerminalEmulationResult(
    TerminalEmulationOutcome Outcome,
    string Command,
    bool StateChanged,
    string StatusText);

/// <summary>
/// Verwaltet einen begrenzten, vollständig beobachtbaren Terminalzustand ohne
/// Hostprozess, Shell, PTY oder Änderung des Host-Terminals.
///
/// Manages a bounded, fully observable terminal state without a host process,
/// shell, PTY, or host-terminal mutation.
/// </summary>
public sealed class TerminalSession : IDisposable
{
    /// <summary>Maximale Zahl verdrängter Zellen. / Maximum number of evicted cells.</summary>
    public const int MaximumHistoryCells = 4096;

    /// <summary>Maximale Länge einer Steuerfolge. / Maximum control-sequence length.</summary>
    public const int MaximumSequenceLength = 64;

    /// <summary>Maximale Zahl numerischer Parameter. / Maximum number of numeric parameters.</summary>
    public const int MaximumParameterCount = 4;

    /// <summary>Größter zulässiger Parameterwert. / Largest accepted parameter value.</summary>
    public const int MaximumParameterValue = 9999;

    private readonly Queue<TConsoleCell> _history = new();
    private TConsoleBuffer _visibleBuffer;
    private int _cursorX;
    private int _cursorY;
    private bool _pendingWrap;

    /// <summary>
    /// Erstellt eine aktive Sitzung mit fester sichtbarer Größe.
    ///
    /// Creates an active session with a fixed visible size.
    /// </summary>
    /// <param name="width">Positive Breite in Zellen. / Positive width in cells.</param>
    /// <param name="height">Positive Höhe in Zellen. / Positive height in cells.</param>
    /// <exception cref="ArgumentOutOfRangeException">Eine Abmessung ist nicht positiv. / A dimension is not positive.</exception>
    public TerminalSession(int width, int height)
    {
        ValidateDimensions(width, height);
        _visibleBuffer = new TConsoleBuffer(width, height);
        Lifecycle = TerminalSessionLifecycle.Active;
        StatusText = "Ready / Bereit";
        ActiveProfileId = "built-in";
        ActiveFontId = TerminalProfile.BuiltInFontId;
    }

    /// <summary>Unabhängiger Snapshot der sichtbaren Zellen. / Independent snapshot of the visible cells.</summary>
    public TConsoleBuffer VisibleBuffer => _visibleBuffer.Clone();

    /// <summary>Aktuelle, stets begrenzte Cursorposition. / Current cursor position, always within bounds.</summary>
    public TPoint Cursor => new((short)_cursorX, (short)_cursorY);

    /// <summary>Aktuelle Vordergrundfarbe. / Current foreground color.</summary>
    public ConsoleColor Foreground { get; private set; } = ConsoleColor.Gray;

    /// <summary>Aktuelle Hintergrundfarbe. / Current background color.</summary>
    public ConsoleColor Background { get; private set; } = ConsoleColor.Black;

    /// <summary>Snapshot des begrenzten FIFO-Verlaufs. / Snapshot of the bounded FIFO history.</summary>
    public IReadOnlyList<TConsoleCell> History => _history.ToArray();

    /// <summary>Zahl der empfangenen BEL-Hinweise. / Number of received BEL notices.</summary>
    public int NoticeCount { get; private set; }

    /// <summary>Textorientierter letzter Status. / Text-first most recent status.</summary>
    public string StatusText { get; private set; }

    /// <summary>Aktueller Lebenszyklus. / Current lifecycle.</summary>
    public TerminalSessionLifecycle Lifecycle { get; private set; }

    /// <summary>Aktive Profilkennung. / Active profile identifier.</summary>
    public string ActiveProfileId { get; private set; }

    /// <summary>Aktiver Zeichensatz. / Active character set.</summary>
    public TerminalCharset ActiveCharset { get; private set; } = TerminalCharset.Unicode;

    /// <summary>Aktive effektive Fontkennung. / Active effective font identifier.</summary>
    public string ActiveFontId { get; private set; }

    /// <summary>Capability-Zustand der aktiven Präsentation. / Capability state of the active presentation.</summary>
    public TerminalProfileCapabilityState PresentationCapability { get; private set; } = TerminalProfileCapabilityState.Enabled;

    /// <summary>
    /// Validiert und verarbeitet Text oder eine begrenzte Steuerfolge.
    ///
    /// Validates and processes text or a bounded control sequence.
    /// </summary>
    /// <param name="input">Kontrollierte Beobachtung. / Controlled observation.</param>
    /// <returns>Akzeptanz-, Ablehnungs- oder Unsupported-Ergebnis. / Accepted, rejected, or unsupported result.</returns>
    public TerminalEmulationResult Write(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (Lifecycle != TerminalSessionLifecycle.Active)
        {
            return Reject("Lifecycle", "Closed or disposed sessions reject input. / Geschlossene oder freigegebene Sitzungen lehnen Eingabe ab.");
        }

        ParseResult parsed = Parse(input);
        if (parsed.Outcome != TerminalEmulationOutcome.Accepted)
        {
            StatusText = parsed.StatusText;
            return new TerminalEmulationResult(parsed.Outcome, parsed.Command, false, parsed.StatusText);
        }

        // Erst die vollständige Operationsliste macht die Beobachtung atomar.
        // Only the complete operation list makes the observation atomic.
        foreach (TerminalOperation operation in parsed.Operations)
        {
            Apply(operation);
        }

        StatusText = parsed.Operations.Count == 0
            ? "Accepted empty observation. / Leere Beobachtung akzeptiert."
            : "Accepted terminal observation. / Terminalbeobachtung akzeptiert.";
        return new TerminalEmulationResult(
            TerminalEmulationOutcome.Accepted,
            parsed.Command,
            parsed.Operations.Count > 0,
            StatusText);
    }

    /// <summary>
    /// Ändert die sichtbare Größe und erhält die links-oben liegende Schnittmenge.
    ///
    /// Changes the visible size and preserves the top-left intersection.
    /// </summary>
    /// <param name="width">Positive neue Breite. / Positive new width.</param>
    /// <param name="height">Positive neue Höhe. / Positive new height.</param>
    /// <exception cref="ArgumentOutOfRangeException">Eine Abmessung ist nicht positiv. / A dimension is not positive.</exception>
    public void Resize(int width, int height)
    {
        ValidateDimensions(width, height);
        if (width == _visibleBuffer.Width && height == _visibleBuffer.Height)
        {
            return;
        }

        TConsoleBuffer resized = new(width, height);
        int copyWidth = Math.Min(width, _visibleBuffer.Width);
        int copyHeight = Math.Min(height, _visibleBuffer.Height);
        for (int y = 0; y < copyHeight; y++)
        {
            for (int x = 0; x < copyWidth; x++)
            {
                resized[x, y] = _visibleBuffer[x, y];
            }
        }

        _visibleBuffer = resized;
        _cursorX = Math.Clamp(_cursorX, 0, width - 1);
        _cursorY = Math.Clamp(_cursorY, 0, height - 1);
        _pendingWrap = false;
        StatusText = "Session resized with top-left preservation. / Sitzung mit links-oberer Erhaltung skaliert.";
    }

    /// <summary>
    /// Übernimmt ein vollständig validiertes effektives Profil als beobachtbare Präsentationsmetadaten.
    ///
    /// Applies a fully validated effective profile as observable presentation metadata.
    /// </summary>
    /// <param name="profile">Validiertes Profil. / Validated profile.</param>
    public void ApplyProfile(TerminalProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);
        if (Lifecycle != TerminalSessionLifecycle.Active)
        {
            return;
        }

        ActiveProfileId = profile.ProfileId;
        ActiveCharset = profile.Charset;
        ActiveFontId = profile.EffectiveFontId;
        PresentationCapability = profile.CapabilityState;
        Foreground = profile.Foreground;
        Background = profile.Background;
        StatusText = $"Profile {profile.ProfileId}: {profile.Charset}, {profile.EffectiveFontId}, {profile.CapabilityState}.";
    }

    /// <summary>
    /// Setzt sichtbaren Zustand, Verlauf, Attribute und Parsergrenze vollständig zurück.
    ///
    /// Fully resets visible state, history, attributes, and parser boundary.
    /// </summary>
    public void Reset()
    {
        if (Lifecycle == TerminalSessionLifecycle.Disposed)
        {
            return;
        }

        _visibleBuffer.Clear();
        _history.Clear();
        _cursorX = 0;
        _cursorY = 0;
        _pendingWrap = false;
        Foreground = ConsoleColor.Gray;
        Background = ConsoleColor.Black;
        NoticeCount = 0;
        ActiveProfileId = "built-in";
        ActiveCharset = TerminalCharset.Unicode;
        ActiveFontId = TerminalProfile.BuiltInFontId;
        PresentationCapability = TerminalProfileCapabilityState.Enabled;
        StatusText = "Session reset. / Sitzung zurückgesetzt.";
    }

    /// <summary>
    /// Schließt die Eingabe idempotent und verwirft vorübergehenden Parserzustand.
    ///
    /// Closes input idempotently and discards transient parser state.
    /// </summary>
    public void Close()
    {
        if (Lifecycle != TerminalSessionLifecycle.Active)
        {
            return;
        }

        _pendingWrap = false;
        Lifecycle = TerminalSessionLifecycle.Closed;
        StatusText = "Session closed. / Sitzung geschlossen.";
    }

    /// <summary>
    /// Beendet die Sitzung bei Capability-Verlust mit derselben sicheren Cleanup-Grenze.
    ///
    /// Ends the session on capability loss using the same safe cleanup boundary.
    /// </summary>
    public void LoseCapability()
    {
        Close();
        StatusText = "Capability lost; session closed. / Capability verloren; Sitzung geschlossen.";
    }

    /// <summary>Gibt die Sitzung idempotent frei. / Disposes the session idempotently.</summary>
    public void Dispose()
    {
        if (Lifecycle == TerminalSessionLifecycle.Disposed)
        {
            return;
        }

        Close();
        _pendingWrap = false;
        Lifecycle = TerminalSessionLifecycle.Disposed;
        StatusText = "Session disposed. / Sitzung freigegeben.";
    }

    private static void ValidateDimensions(int width, int height)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be positive.");
        }

        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(height), "Height must be positive.");
        }
    }

    private static ParseResult Parse(string input)
    {
        List<TerminalOperation> operations = new();
        string command = "Text";

        for (int index = 0; index < input.Length; index++)
        {
            char current = input[index];
            if (current != '\x1b')
            {
                operations.Add(ControlOperation(current));
                continue;
            }

            if (index + 1 >= input.Length)
            {
                return ParseResult.Rejected("Escape", "Incomplete escape sequence. / Unvollständige Escape-Folge.");
            }

            if (input[index + 1] == 'c')
            {
                operations.Add(new TerminalOperation(OperationKind.FullReset, '\0', 'c', Array.Empty<int>()));
                command = "FullReset";
                index++;
                continue;
            }

            if (input[index + 1] != '[')
            {
                return ParseResult.Unsupported("Escape", "Escape sequence is outside the supported subset. / Escape-Folge liegt außerhalb des unterstützten Subsets.");
            }

            int finalIndex = index + 2;
            while (finalIndex < input.Length && (input[finalIndex] < '@' || input[finalIndex] > '~'))
            {
                if ((finalIndex - index) + 1 > MaximumSequenceLength)
                {
                    return ParseResult.Rejected("CSI", "Control sequence exceeds 64 characters. / Steuerfolge überschreitet 64 Zeichen.");
                }

                finalIndex++;
            }

            if (finalIndex >= input.Length)
            {
                return ParseResult.Rejected("CSI", "Incomplete CSI sequence. / Unvollständige CSI-Folge.");
            }

            int sequenceLength = (finalIndex - index) + 1;
            if (sequenceLength > MaximumSequenceLength)
            {
                return ParseResult.Rejected("CSI", "Control sequence exceeds 64 characters. / Steuerfolge überschreitet 64 Zeichen.");
            }

            char final = input[finalIndex];
            string body = input[(index + 2)..finalIndex];
            ParseResult csi = ParseCsi(final, body);
            if (csi.Outcome != TerminalEmulationOutcome.Accepted)
            {
                return csi;
            }

            operations.AddRange(csi.Operations);
            command = $"CSI {final}";
            index = finalIndex;
        }

        return ParseResult.Accepted(command, operations);
    }

    private static TerminalOperation ControlOperation(char value) => value switch
    {
        '\a' => new TerminalOperation(OperationKind.Bell, value, '\0', Array.Empty<int>()),
        '\b' => new TerminalOperation(OperationKind.Backspace, value, '\0', Array.Empty<int>()),
        '\t' => new TerminalOperation(OperationKind.Tab, value, '\0', Array.Empty<int>()),
        '\r' => new TerminalOperation(OperationKind.CarriageReturn, value, '\0', Array.Empty<int>()),
        '\n' => new TerminalOperation(OperationKind.LineFeed, value, '\0', Array.Empty<int>()),
        _ => new TerminalOperation(OperationKind.Text, char.IsSurrogate(value) ? '\uFFFD' : value, '\0', Array.Empty<int>())
    };

    private static ParseResult ParseCsi(char final, string body)
    {
        if (final is not ('A' or 'B' or 'C' or 'D' or 'H' or 'f' or 'J' or 'K' or 'm'))
        {
            return ParseResult.Unsupported($"CSI {final}", "CSI command is outside the supported subset. / CSI-Befehl liegt außerhalb des unterstützten Subsets.");
        }

        string[] parts = body.Length == 0 ? Array.Empty<string>() : body.Split(';');
        if (parts.Length > MaximumParameterCount)
        {
            return ParseResult.Rejected($"CSI {final}", "CSI has more than four parameters. / CSI hat mehr als vier Parameter.");
        }

        int[] parameters = new int[parts.Length];
        for (int index = 0; index < parts.Length; index++)
        {
            if (parts[index].Length == 0)
            {
                parameters[index] = 0;
                continue;
            }

            if (!int.TryParse(parts[index], NumberStyles.None, CultureInfo.InvariantCulture, out int value) ||
                value > MaximumParameterValue)
            {
                return ParseResult.Rejected($"CSI {final}", "CSI parameter is invalid or exceeds 9,999. / CSI-Parameter ist ungültig oder größer als 9.999.");
            }

            parameters[index] = value;
        }

        int maximumForCommand = final switch
        {
            'H' or 'f' => 2,
            'm' => MaximumParameterCount,
            _ => 1
        };
        if (parameters.Length > maximumForCommand)
        {
            return ParseResult.Rejected($"CSI {final}", "CSI command has too many parameters. / CSI-Befehl hat zu viele Parameter.");
        }

        if (final is 'J' or 'K' && parameters.Any(value => value is < 0 or > 2))
        {
            return ParseResult.Unsupported($"CSI {final}", "Erase mode is unsupported. / Löschmodus wird nicht unterstützt.");
        }

        if (final == 'm' && !parameters.All(IsSupportedSgr))
        {
            return ParseResult.Unsupported("CSI m", "Color attribute is unsupported. / Farbattribut wird nicht unterstützt.");
        }

        return ParseResult.Accepted(
            $"CSI {final}",
            new[] { new TerminalOperation(OperationKind.Csi, '\0', final, parameters) });
    }

    private static bool IsSupportedSgr(int value) =>
        value == 0 ||
        value is >= 30 and <= 37 ||
        value is >= 40 and <= 47 ||
        value is >= 90 and <= 97 ||
        value is >= 100 and <= 107;

    private void Apply(TerminalOperation operation)
    {
        switch (operation.Kind)
        {
            case OperationKind.Text:
                WriteGlyph(operation.Value);
                break;
            case OperationKind.Bell:
                NoticeCount++;
                StatusText = "BEL notice recorded in-process. / BEL-Hinweis in-process erfasst.";
                break;
            case OperationKind.Backspace:
                _pendingWrap = false;
                _cursorX = Math.Max(0, _cursorX - 1);
                break;
            case OperationKind.Tab:
                _pendingWrap = false;
                _cursorX = Math.Min(_visibleBuffer.Width - 1, ((_cursorX / 8) + 1) * 8);
                break;
            case OperationKind.CarriageReturn:
                _pendingWrap = false;
                _cursorX = 0;
                break;
            case OperationKind.LineFeed:
                _pendingWrap = false;
                MoveDownOrScroll();
                break;
            case OperationKind.FullReset:
                Reset();
                break;
            case OperationKind.Csi:
                ApplyCsi(operation.Command, operation.Parameters);
                break;
        }
    }

    private void WriteGlyph(char glyph)
    {
        if (_pendingWrap)
        {
            _cursorX = 0;
            MoveDownOrScroll();
            _pendingWrap = false;
        }

        _visibleBuffer[_cursorX, _cursorY] = new TConsoleCell(glyph, Foreground, Background);
        if (_cursorX == _visibleBuffer.Width - 1)
        {
            _pendingWrap = true;
        }
        else
        {
            _cursorX++;
        }
    }

    private void MoveDownOrScroll()
    {
        if (_cursorY < _visibleBuffer.Height - 1)
        {
            _cursorY++;
            return;
        }

        ScrollUp();
        _cursorY = _visibleBuffer.Height - 1;
    }

    private void ScrollUp()
    {
        for (int x = 0; x < _visibleBuffer.Width; x++)
        {
            AddHistory(_visibleBuffer[x, 0]);
        }

        for (int y = 1; y < _visibleBuffer.Height; y++)
        {
            for (int x = 0; x < _visibleBuffer.Width; x++)
            {
                _visibleBuffer[x, y - 1] = _visibleBuffer[x, y];
            }
        }

        for (int x = 0; x < _visibleBuffer.Width; x++)
        {
            _visibleBuffer[x, _visibleBuffer.Height - 1] = TConsoleCell.Empty;
        }
    }

    private void AddHistory(TConsoleCell cell)
    {
        _history.Enqueue(cell);
        while (_history.Count > MaximumHistoryCells)
        {
            _history.Dequeue();
        }
    }

    private void ApplyCsi(char command, IReadOnlyList<int> parameters)
    {
        _pendingWrap = false;
        int FirstOrDefaultOne() => parameters.Count == 0 || parameters[0] == 0 ? 1 : parameters[0];

        switch (command)
        {
            case 'A':
                _cursorY = Math.Max(0, _cursorY - FirstOrDefaultOne());
                break;
            case 'B':
                _cursorY = Math.Min(_visibleBuffer.Height - 1, _cursorY + FirstOrDefaultOne());
                break;
            case 'C':
                _cursorX = Math.Min(_visibleBuffer.Width - 1, _cursorX + FirstOrDefaultOne());
                break;
            case 'D':
                _cursorX = Math.Max(0, _cursorX - FirstOrDefaultOne());
                break;
            case 'H':
            case 'f':
                int row = parameters.Count == 0 || parameters[0] == 0 ? 1 : parameters[0];
                int column = parameters.Count < 2 || parameters[1] == 0 ? 1 : parameters[1];
                _cursorY = Math.Clamp(row - 1, 0, _visibleBuffer.Height - 1);
                _cursorX = Math.Clamp(column - 1, 0, _visibleBuffer.Width - 1);
                break;
            case 'J':
                EraseDisplay(parameters.Count == 0 ? 0 : parameters[0]);
                break;
            case 'K':
                EraseLine(parameters.Count == 0 ? 0 : parameters[0]);
                break;
            case 'm':
                ApplySgr(parameters.Count == 0 ? new[] { 0 } : parameters);
                break;
        }
    }

    private void EraseDisplay(int mode)
    {
        for (int y = 0; y < _visibleBuffer.Height; y++)
        {
            for (int x = 0; x < _visibleBuffer.Width; x++)
            {
                bool erase = mode switch
                {
                    0 => y > _cursorY || (y == _cursorY && x >= _cursorX),
                    1 => y < _cursorY || (y == _cursorY && x <= _cursorX),
                    2 => true,
                    _ => false
                };
                if (erase)
                {
                    _visibleBuffer[x, y] = TConsoleCell.Empty;
                }
            }
        }
    }

    private void EraseLine(int mode)
    {
        for (int x = 0; x < _visibleBuffer.Width; x++)
        {
            bool erase = mode switch
            {
                0 => x >= _cursorX,
                1 => x <= _cursorX,
                2 => true,
                _ => false
            };
            if (erase)
            {
                _visibleBuffer[x, _cursorY] = TConsoleCell.Empty;
            }
        }
    }

    private void ApplySgr(IReadOnlyList<int> parameters)
    {
        foreach (int parameter in parameters)
        {
            if (parameter == 0)
            {
                Foreground = ConsoleColor.Gray;
                Background = ConsoleColor.Black;
            }
            else if (parameter is >= 30 and <= 37)
            {
                Foreground = AnsiColor(parameter - 30, bright: false);
            }
            else if (parameter is >= 40 and <= 47)
            {
                Background = AnsiColor(parameter - 40, bright: false);
            }
            else if (parameter is >= 90 and <= 97)
            {
                Foreground = AnsiColor(parameter - 90, bright: true);
            }
            else
            {
                Background = AnsiColor(parameter - 100, bright: true);
            }
        }
    }

    private static ConsoleColor AnsiColor(int index, bool bright)
    {
        ConsoleColor[] dark =
        {
            ConsoleColor.Black, ConsoleColor.DarkRed, ConsoleColor.DarkGreen, ConsoleColor.DarkYellow,
            ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta, ConsoleColor.DarkCyan, ConsoleColor.Gray
        };
        ConsoleColor[] light =
        {
            ConsoleColor.DarkGray, ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow,
            ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.White
        };
        return (bright ? light : dark)[index];
    }

    private TerminalEmulationResult Reject(string command, string text)
    {
        StatusText = text;
        return new TerminalEmulationResult(TerminalEmulationOutcome.Rejected, command, false, text);
    }

    private enum OperationKind
    {
        Text,
        Bell,
        Backspace,
        Tab,
        CarriageReturn,
        LineFeed,
        FullReset,
        Csi
    }

    private readonly record struct TerminalOperation(
        OperationKind Kind,
        char Value,
        char Command,
        IReadOnlyList<int> Parameters);

    private sealed record ParseResult(
        TerminalEmulationOutcome Outcome,
        string Command,
        string StatusText,
        IReadOnlyList<TerminalOperation> Operations)
    {
        public static ParseResult Accepted(string command, IReadOnlyList<TerminalOperation> operations) =>
            new(TerminalEmulationOutcome.Accepted, command, string.Empty, operations);

        public static ParseResult Rejected(string command, string text) =>
            new(TerminalEmulationOutcome.Rejected, command, text, Array.Empty<TerminalOperation>());

        public static ParseResult Unsupported(string command, string text) =>
            new(TerminalEmulationOutcome.Unsupported, command, text, Array.Empty<TerminalOperation>());
    }
}
