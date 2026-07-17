// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Wave5;

/// <summary>Funktionales TP7-Rechnerbeispiel. / Functional TP7 calculator example.</summary>
public sealed class Tp7CalculatorApp : Wave5Application
{
    private const ushort FirstButtonCommand = 32600;
    private static readonly (string Label, string? Token)[] ButtonDefinitions =
    [
        ("7", "7"), ("8", "8"), ("9", "9"), ("/", "/"),
        ("4", "4"), ("5", "5"), ("6", "6"), ("*", "*"),
        ("1", "1"), ("2", "2"), ("3", "3"), ("-", "-"),
        ("0", "0"), (".", "."), ("=", "="), ("+", "+"),
        ("C", "C"), ("Back", "B"), ("Sign", "S"), ("F1", null)
    ];

    private readonly List<TButton> _calculatorButtons = [];

    /// <summary>Verarbeitet eine Rechnersequenz aus `Message.Info`. / Processes a calculator sequence from `Message.Info`.</summary>
    public const ushort CmApplySequence = 32001;

    /// <summary>Initialisiert das Rechnerbeispiel. / Initializes the calculator example.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    public Tp7CalculatorApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        Calculator = new Tp7CalculatorState();
        RenderState();
    }

    /// <summary>Rechnerzustand. / Calculator state.</summary>
    public Tp7CalculatorState Calculator { get; }

    /// <summary>Zahl der realen fokussierbaren Rechnerbuttons. / Number of real focusable calculator buttons.</summary>
    public int CalculatorButtonCount => _calculatorButtons.Count;

    /// <summary>Typ des aktuell fokussierten Rechnercontrols. / Type of the currently focused calculator control.</summary>
    public string FocusedControlKind { get; private set; } = string.Empty;

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmApplySequence)
        {
            Calculator.ApplySequence(@event.Message.Info as string ?? string.Empty);
            RenderState();
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command
            && @event.Message.Command is >= FirstButtonCommand
                and < FirstButtonCommand + 19)
        {
            int index = @event.Message.Command - FirstButtonCommand;
            Calculator.ApplySequence(ButtonDefinitions[index].Token ?? string.Empty);
            RenderState();
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.KeyDown && TryMapKey(@event.KeyDown, out string? sequence))
        {
            Calculator.ApplySequence(sequence);
            RenderState();
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    /// <inheritdoc />
    protected override string BuildDescriptionText() =>
        """
        Historischer Lernzweck: Display und fokussierbare Tasten zeigen den klassischen Rechnerfluss.
        Historical learning purpose: the display and focusable buttons show the classic calculator flow.
        Tastatur: Ziffern, Punkt, + - * /, Enter oder =, C, Backspace, S für Vorzeichen, Tab und F1.
        Keyboard: digits, decimal point, + - * /, Enter or =, C, Backspace, S for sign, Tab, and F1.
        Die Umsetzung bleibt modernes C# mit decimal-Zustand statt einer Pascal-Objektkopie.
        The implementation remains modern C# with decimal state rather than a Pascal object copy.
        Division durch null veröffentlicht kein ungültiges Ergebnis und bewahrt den letzten gültigen Wert.
        Division by zero publishes no invalid result and preserves the last valid value.
        Der App-Loop-Smoke beweist Zustand, Fokus, Status und Zellen; er beweist keine Host-Terminaldarstellung.
        The app-loop smoke proves state, focus, status, and cells; it does not prove host-terminal rendering.
        """;

    private void RenderState()
    {
        if (Desktop is null)
        {
            return;
        }

        int right = Math.Max(27, Desktop.Size.X - 1);
        int bottom = Math.Max(9, Desktop.Size.Y);
        TDialog dialog = new(new TRect(1, 0, right, bottom), "TP7 Calculator");
        dialog.Insert(new TStaticText(
            new TRect(2, 1, Math.Max(3, dialog.Size.X - 2), 2),
            $"Display: {Calculator.DisplayText}"));

        _calculatorButtons.Clear();
        int availableWidth = Math.Max(24, dialog.Size.X - 4);
        int columnWidth = Math.Max(6, availableWidth / 4);
        for (int index = 0; index < ButtonDefinitions.Length; index++)
        {
            int row = index / 4;
            int column = index % 4;
            int left = 2 + (column * columnWidth);
            int top = 3 + row;
            ushort command = index == ButtonDefinitions.Length - 1
                ? CmDescription
                : (ushort)(FirstButtonCommand + index);
            TButton button = new(
                new TRect(left, top, Math.Min(dialog.Size.X - 2, left + columnWidth - 1), top + 1),
                ButtonDefinitions[index].Label,
                command,
                TButtonFlags.bfNormal);
            dialog.Insert(button);
            _calculatorButtons.Add(button);
        }

        string visibleText = $"TP7 Calculator\nDisplay: {Calculator.DisplayText}\nStatus: {Calculator.Status}";
        ShowView(dialog, nameof(TDialog), visibleText);
        // Der Dialog fokussiert bewusst die erste echte Taste; so beweist der Smoke nicht nur den Container.
        // The dialog deliberately focuses the first real button so the smoke proves more than the container.
        dialog.SetFocus(_calculatorButtons[0]);
        FocusedControlKind = dialog.Current?.GetType().Name ?? string.Empty;
        SetStatus("Tp7Calculator", Calculator.Status);
    }

    private static bool TryMapKey(TKeyDownEvent keyDown, out string sequence)
    {
        sequence = keyDown.ScanCode switch
        {
            0x0E => "B",
            0x1C => "=",
            _ => keyDown.CharCode.ToString()
        };

        return sequence.Length == 1
            && "0123456789.+-*/=CcSsBb".Contains(sequence[0], StringComparison.Ordinal);
    }
}
