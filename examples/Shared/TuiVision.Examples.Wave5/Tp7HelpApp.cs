// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;
using TuiVision.Serialization;

namespace TuiVision.Examples.Wave5;

/// <summary>Funktionales TP7-Hilfecompiler- und Viewerbeispiel. / Functional TP7 help compiler and viewer example.</summary>
public sealed class Tp7HelpApp : Wave5Application
{
    private const string ValidSource =
        ".topic Welcome=101\nWelcome\nOpen {Next:Next}.\n.topic Next=102\nNext topic";
    private const string InvalidSource =
        ".topic Broken=101\nOpen {Missing:Missing}.";
    private readonly THelpSourceCompiler _compiler = new();
    private readonly THelpFile _helpFile;

    /// <summary>Kompiliert eine ungültige Quelle. / Compiles an invalid source.</summary>
    public const ushort CmCompileInvalid = 32301;

    /// <summary>Zeigt den bekannten Kontext. / Shows the known context.</summary>
    public const ushort CmShowKnownContext = 32302;

    /// <summary>Zeigt einen unbekannten Kontext. / Shows an unknown context.</summary>
    public const ushort CmShowUnknownContext = 32303;

    /// <summary>Initialisiert das Hilfebeispiel. / Initializes the help example.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    public Tp7HelpApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        LastCompilation = _compiler.Compile(ValidSource, "tp7-valid.topic");
        _helpFile = LastCompilation.HelpFile
            ?? throw new InvalidOperationException("The built-in TP7 help source must compile.");
        ShowContent("TP7 Help", "TP7 Help\nCompiled topics: 2\nContext: 101 Welcome");
        SetStatus("Tp7Help", "compiled valid source");
    }

    /// <summary>Letztes Compilerergebnis. / Last compiler result.</summary>
    public THelpCompilationResult? LastCompilation { get; private set; }

    /// <summary>Ob eine Ablehnung ohne Teilmodell blieb. / Whether rejection left no partial model.</summary>
    public bool LastRejectedWithoutPartialModel { get; private set; }

    /// <summary>Letzter sichtbarer Kontext. / Last visible context.</summary>
    public int CurrentContext { get; private set; } = 101;

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmCompileInvalid)
        {
            LastCompilation = _compiler.Compile(InvalidSource, "tp7-invalid.topic");
            LastRejectedWithoutPartialModel = !LastCompilation.Success
                && LastCompilation.HelpFile is null
                && LastCompilation.Symbols.Count == 0;
            string code = LastCompilation.Diagnostics.Count == 0
                ? "THC000"
                : LastCompilation.Diagnostics[0].Code;
            ShowContent(
                "TP7 Help Rejection",
                $"TP7 Help rejected\nDiagnostic: {code}\nNo partial model: {LastRejectedWithoutPartialModel}");
            SetStatus("Tp7Help", $"rejected {code}");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmShowKnownContext)
        {
            ShowContext(101);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmShowUnknownContext)
        {
            ShowContext(999);
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private void ShowContext(int context)
    {
        CurrentContext = context;
        int width = Math.Clamp(62, 14, Math.Max(14, Desktop!.Size.X - 4));
        int height = Math.Clamp(15, 7, Math.Max(7, Desktop.Size.Y - 3));
        THelpWindow window = new(new TRect(2, 1, 2 + width, 1 + height), _helpFile, context);
        ShowView(window, nameof(THelpWindow), $"context={context}");
        string title = window.Viewer.CurrentTopic?.Title ?? "No help available";
        SetStatus("Tp7Help", $"context={context} topic={title}");
    }
}
