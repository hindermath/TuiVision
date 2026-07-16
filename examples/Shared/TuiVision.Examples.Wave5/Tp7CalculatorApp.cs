// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Examples.Wave5;

/// <summary>Funktionales TP7-Rechnerbeispiel. / Functional TP7 calculator example.</summary>
public sealed class Tp7CalculatorApp : Wave5Application
{
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

        base.HandleEvent(@event);
    }

    private void RenderState()
    {
        ShowContent("TP7 Calculator", $"TP7 Calculator\nDisplay: {Calculator.DisplayText}\nStatus: {Calculator.Status}\nKeyboard core path");
        SetStatus("Tp7Calculator", Calculator.Status);
    }
}
