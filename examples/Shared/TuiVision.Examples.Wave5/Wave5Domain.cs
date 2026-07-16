// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;

namespace TuiVision.Examples.Wave5;

/// <summary>Unterstützte Rechneroperationen. / Supported calculator operations.</summary>
public enum Tp7CalculatorOperation
{
    /// <summary>Addition. / Addition.</summary>
    Add,
    /// <summary>Subtraktion. / Subtraction.</summary>
    Subtract,
    /// <summary>Multiplikation. / Multiplication.</summary>
    Multiply,
    /// <summary>Division. / Division.</summary>
    Divide
}

/// <summary>Deterministischer Zustand des TP7-Rechnerbeispiels. / Deterministic state of the TP7 calculator example.</summary>
public sealed class Tp7CalculatorState
{
    private decimal? _pendingValue;
    private Tp7CalculatorOperation? _pendingOperation;
    private string _entry = "0";
    private bool _replaceEntry = true;

    /// <summary>Letzter gültiger Wert. / Last valid value.</summary>
    public decimal DisplayValue { get; private set; }

    /// <summary>Sichtbarer invariant formatierter Wert. / Visible invariant formatted value.</summary>
    public string DisplayText => DisplayValue.ToString("0.############################", CultureInfo.InvariantCulture);

    /// <summary>Aktueller Status. / Current status.</summary>
    public string Status { get; private set; } = "ready";

    /// <summary>Ob die letzte Aktion abgelehnt wurde. / Whether the last action was rejected.</summary>
    public bool Rejected { get; private set; }

    /// <summary>Verarbeitet eine kurze Rechnersequenz. / Processes a short calculator sequence.</summary>
    /// <param name="sequence">Ziffern und `+ - * / = C Back Sign`. / Digits and `+ - * / = C Back Sign`.</param>
    public void ApplySequence(string sequence)
    {
        ArgumentNullException.ThrowIfNull(sequence);
        foreach (char token in sequence)
        {
            if (char.IsAsciiDigit(token))
            {
                EnterDigit(token);
                continue;
            }

            switch (token)
            {
                case '.':
                    EnterDecimalPoint();
                    break;
                case '+':
                    SetOperation(Tp7CalculatorOperation.Add);
                    break;
                case '-':
                    SetOperation(Tp7CalculatorOperation.Subtract);
                    break;
                case '*':
                    SetOperation(Tp7CalculatorOperation.Multiply);
                    break;
                case '/':
                    SetOperation(Tp7CalculatorOperation.Divide);
                    break;
                case '=':
                    Evaluate();
                    break;
                case 'C':
                case 'c':
                    Clear();
                    break;
                case 'B':
                case 'b':
                    Backspace();
                    break;
                case 'S':
                case 's':
                    ToggleSign();
                    break;
                default:
                    Status = $"rejected token={token}";
                    Rejected = true;
                    break;
            }
        }
    }

    /// <summary>Löscht den Rechnerzustand. / Clears calculator state.</summary>
    public void Clear()
    {
        DisplayValue = 0;
        _pendingValue = null;
        _pendingOperation = null;
        _entry = "0";
        _replaceEntry = true;
        Rejected = false;
        Status = "cleared";
    }

    private void Backspace()
    {
        if (_replaceEntry)
        {
            return;
        }

        _entry = _entry.Length <= 1 || (_entry.Length == 2 && _entry[0] == '-')
            ? "0"
            : _entry[..^1];
        DisplayValue = decimal.Parse(_entry, CultureInfo.InvariantCulture);
        Rejected = false;
        Status = $"entry={_entry}";
    }

    private void ToggleSign()
    {
        DisplayValue = -DisplayValue;
        _entry = DisplayText;
        _replaceEntry = false;
        Rejected = false;
        Status = $"entry={_entry}";
    }

    private void EnterDigit(char digit)
    {
        _entry = _replaceEntry || _entry == "0" ? digit.ToString() : _entry + digit;
        _replaceEntry = false;
        DisplayValue = decimal.Parse(_entry, CultureInfo.InvariantCulture);
        Rejected = false;
        Status = $"entry={_entry}";
    }

    private void EnterDecimalPoint()
    {
        if (_replaceEntry)
        {
            _entry = "0";
            _replaceEntry = false;
        }

        if (!_entry.Contains('.'))
        {
            _entry += ".";
        }

        Status = $"entry={_entry}";
    }

    private void SetOperation(Tp7CalculatorOperation operation)
    {
        _pendingValue = DisplayValue;
        _pendingOperation = operation;
        _replaceEntry = true;
        Rejected = false;
        Status = $"pending={operation}";
    }

    private void Evaluate()
    {
        if (_pendingValue is not decimal left || _pendingOperation is not Tp7CalculatorOperation operation)
        {
            Status = "rejected incomplete operation";
            Rejected = true;
            return;
        }

        decimal right = DisplayValue;
        if (operation == Tp7CalculatorOperation.Divide && right == 0)
        {
            // Der letzte gültige linke Wert bleibt sichtbar; ein Fehler darf keinen ungültigen Ergebniszustand publizieren.
            // The last valid left value stays visible; an error must not publish an invalid result state.
            DisplayValue = left;
            _entry = DisplayText;
            _pendingValue = null;
            _pendingOperation = null;
            _replaceEntry = true;
            Rejected = true;
            Status = "rejected division by zero";
            return;
        }

        DisplayValue = operation switch
        {
            Tp7CalculatorOperation.Add => left + right,
            Tp7CalculatorOperation.Subtract => left - right,
            Tp7CalculatorOperation.Multiply => left * right,
            Tp7CalculatorOperation.Divide => left / right,
            _ => throw new InvalidOperationException("Unknown calculator operation.")
        };
        _entry = DisplayText;
        _pendingValue = null;
        _pendingOperation = null;
        _replaceEntry = true;
        Rejected = false;
        Status = $"result={DisplayText}";
    }
}

/// <summary>Deterministischer ASCII-Auswahlzustand. / Deterministic ASCII selection state.</summary>
public sealed class Tp7AsciiState
{
    /// <summary>Initialisiert die Auswahl. / Initializes the selection.</summary>
    /// <param name="code">Startcode von 0 bis 255. / Initial code from 0 to 255.</param>
    public Tp7AsciiState(int code = 65) => Select(code);

    /// <summary>Ausgewählter Bytewert. / Selected byte value.</summary>
    public int Code { get; private set; }

    /// <summary>Hexadezimale Darstellung. / Hexadecimal representation.</summary>
    public string Hex => Code.ToString("X2", CultureInfo.InvariantCulture);

    /// <summary>Druckbares Zeichen oder Kontrolllabel. / Printable character or control label.</summary>
    public string Character => Code is >= 32 and <= 126 ? ((char)Code).ToString() : $"CTRL-{Code:D3}";

    /// <summary>Verschiebt die Auswahl begrenzt. / Moves the selection within bounds.</summary>
    /// <param name="delta">Schrittweite. / Step delta.</param>
    public void Move(int delta) => Code = Math.Clamp(Code + delta, 0, 255);

    /// <summary>Wählt einen gültigen Code. / Selects a valid code.</summary>
    /// <param name="code">Bytewert. / Byte value.</param>
    /// <returns>`true` bei Annahme. / `true` when accepted.</returns>
    public bool Select(int code)
    {
        if (code is < 0 or > 255)
        {
            return false;
        }

        Code = code;
        return true;
    }
}

/// <summary>Deterministischer Monatszustand. / Deterministic month state.</summary>
public sealed class Tp7CalendarState
{
    /// <summary>Initialisiert eine feste Monats-Fixture. / Initializes a fixed month fixture.</summary>
    /// <param name="initialMonth">Datum, das auf Tag 1 normalisiert wird. / Date normalized to day 1.</param>
    public Tp7CalendarState(DateOnly initialMonth) =>
        SelectedMonth = new DateOnly(initialMonth.Year, initialMonth.Month, 1);

    /// <summary>Aktuell ausgewählter Monat. / Currently selected month.</summary>
    public DateOnly SelectedMonth { get; private set; }

    /// <summary>Invariant sichtbarer Monat. / Invariant visible month.</summary>
    public string Label => SelectedMonth.ToString("yyyy-MM", CultureInfo.InvariantCulture);

    /// <summary>Verschiebt den Monat mit Jahreswechsel. / Moves the month across year boundaries.</summary>
    /// <param name="delta">Monatsdifferenz. / Month delta.</param>
    public void MoveMonth(int delta) => SelectedMonth = SelectedMonth.AddMonths(delta);
}

/// <summary>Deterministischer 4x4-Schiebepuzzlezustand. / Deterministic 4x4 sliding-puzzle state.</summary>
public sealed class Tp7PuzzleState
{
    private readonly int[] _tiles = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 0, 15];

    /// <summary>Aktuelle unveränderliche Ansicht der Steine. / Current read-only tile view.</summary>
    public IReadOnlyList<int> Tiles => _tiles;

    /// <summary>Anzahl akzeptierter Züge. / Accepted move count.</summary>
    public int MoveCount { get; private set; }

    /// <summary>Ob der letzte Zug abgelehnt wurde. / Whether the last move was rejected.</summary>
    public bool LastMoveRejected { get; private set; }

    /// <summary>Bewegt einen zum Leerfeld benachbarten Stein. / Moves a tile adjacent to the blank.</summary>
    /// <param name="tile">Steinwert 1 bis 15. / Tile value 1 through 15.</param>
    /// <returns>`true` bei einem gültigen Zug. / `true` for a valid move.</returns>
    public bool MoveTile(int tile)
    {
        int tileIndex = Array.IndexOf(_tiles, tile);
        int blankIndex = Array.IndexOf(_tiles, 0);
        bool adjacent = tileIndex >= 0
            && (Math.Abs((tileIndex % 4) - (blankIndex % 4)) + Math.Abs((tileIndex / 4) - (blankIndex / 4)) == 1);
        if (!adjacent)
        {
            LastMoveRejected = true;
            return false;
        }

        (_tiles[tileIndex], _tiles[blankIndex]) = (_tiles[blankIndex], _tiles[tileIndex]);
        MoveCount++;
        LastMoveRejected = false;
        return true;
    }

    /// <summary>Formatiert das feste Board textorientiert. / Formats the fixed board as text.</summary>
    /// <returns>Vier Zeilen mit vier Feldern. / Four rows with four fields.</returns>
    public string FormatBoard()
    {
        string[] rows = new string[4];
        for (int row = 0; row < 4; row++)
        {
            rows[row] = string.Join(' ', _tiles.Skip(row * 4).Take(4).Select(value => value == 0 ? "__" : value.ToString("D2", CultureInfo.InvariantCulture)));
        }

        return string.Join('\n', rows);
    }
}
