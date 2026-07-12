// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Drivers.Console;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.Terminal;

/// <summary>
/// Sichtbare Wave-4-Terminaldemo auf Basis der kontrollierten Feature-021-Sitzung.
/// Visible Wave-4 terminal demo based on the controlled Feature-021 session.
/// </summary>
public sealed class TerminalApp : Wave4Application
{
    /// <summary>Schreibt die sichtbare Probe. / Writes the visible sample.</summary>
    public const ushort CmWriteSample = 22001;
    /// <summary>Führt eine unterstützte Cursoraktion aus. / Performs a supported cursor action.</summary>
    public const ushort CmCursorAction = 22002;
    /// <summary>Führt eine atomar abgelehnte Aktion aus. / Performs an atomically rejected action.</summary>
    public const ushort CmRejectedAction = 22003;
    /// <summary>Schreibt die unabhängige Recovery-Eingabe. / Writes independent recovery input.</summary>
    public const ushort CmWriteRecovery = 22004;
    /// <summary>Setzt die Sitzung kontrolliert zurück. / Resets the session in a controlled manner.</summary>
    public const ushort CmReset = 22005;
    /// <summary>Zeigt die Beschreibung. / Shows the description.</summary>
    public const ushort CmDescription = 22006;

    private readonly bool _capabilityAvailable;
    private readonly TerminalProfile _profile;

    /// <summary>Initialisiert die kontrollierte Terminaldemo. / Initializes the controlled terminal demo.</summary>
    public TerminalApp(TRect bounds, bool headless = false, bool capabilityAvailable = true) : base(bounds, headless)
    {
        _capabilityAvailable = capabilityAvailable;
        TerminalProfileParseResult parsed = TerminalProfile.Parse(
            "{\"ProfileId\":\"wave4-terminal\",\"Charset\":\"Unicode\",\"FontId\":\"built-in-8x16\",\"Foreground\":\"Gray\",\"Background\":\"Black\"}",
            [TerminalProfile.BuiltInFontId],
            capabilityAvailable);
        _profile = parsed.Profile ?? throw new InvalidOperationException(parsed.Reason);

        TRect region = Wave4Runtime.MainRegion(Desktop!, 70, 18);
        Session = new TerminalSession(Math.Max(1, region.Width), Math.Max(1, region.Height - 1));
        Session.ApplyProfile(_profile);
        LastOutcome = Session.Write("Wave4 terminal ready").Outcome;
        TerminalView = new TTerminalView(region, Session);
        Desktop!.Insert(TerminalView);
        Desktop.SetFocus(TerminalView);
        SetWave4Visible(TerminalView, "TTerminalView");
        UpdateStatus(capabilityAvailable ? "ready" : "fallback Unsupported");
    }

    /// <summary>Kontrollierte Driver-Sitzung. / Controlled Driver session.</summary>
    public TerminalSession Session { get; }

    /// <summary>Sichtbare Terminalprojektion. / Visible terminal projection.</summary>
    public TTerminalView TerminalView { get; }

    /// <summary>Letztes Emulationsergebnis. / Last emulation outcome.</summary>
    public TerminalEmulationOutcome LastOutcome { get; private set; }

    /// <summary>Ob eine Ablehnung vor der Recovery beobachtet wurde. / Whether a rejection was observed before recovery.</summary>
    public bool RejectionObserved { get; private set; }

    /// <summary>Text des Beschreibungspfads. / Description-path text.</summary>
    public string DescriptionText =>
        "Terminal description: Die Demo nutzt eine kontrollierte In-Process-Sitzung; kein Prozess, keine Shell und kein PTY werden gestartet. / " +
        "The demo uses a controlled in-process session; no process, shell, or PTY is started.";

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuBar bar = new(bounds)
        {
            Menu = new TMenuItem(
                "~T~erminal",
                0,
                Wave4Runtime.HelpMenu(CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit)),
                new TMenuItem("~P~robe / ~S~ample", CmWriteSample))
        };
        return bar;
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmWriteSample:
                    Apply(Session.Write(" Wave4 sample"), "sample accepted");
                    break;
                case CmCursorAction:
                    Apply(Session.Write("\u001b[2;4H"), "cursor action accepted");
                    break;
                case CmRejectedAction:
                    TerminalEmulationResult rejected = Session.Write("\u001b[10000A");
                    LastOutcome = rejected.Outcome;
                    RejectionObserved = rejected.Outcome == TerminalEmulationOutcome.Rejected;
                    UpdateStatus("rejected atomically");
                    break;
                case CmWriteRecovery:
                    Apply(Session.Write(" recovered"), "recovery accepted");
                    break;
                case CmReset:
                    Session.Reset();
                    Session.ApplyProfile(_profile);
                    Apply(Session.Write(_capabilityAvailable ? "Wave4 reset" : "Unsupported fallback"),
                        _capabilityAvailable ? "reset" : "fallback Unsupported");
                    break;
                case CmDescription:
                    ShowWave4Description("Terminal", DescriptionText);
                    @event.Clear();
                    return;
                default:
                    base.HandleEvent(@event);
                    return;
            }

            TerminalView.DrawView();
            SetWave4Visible(TerminalView, "TTerminalView");
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private void Apply(TerminalEmulationResult result, string state)
    {
        LastOutcome = result.Outcome;
        UpdateStatus(state);
    }

    private void UpdateStatus(string state) =>
        SetWave4Status(
            "Terminal",
            $"{state} capability={Session.PresentationCapability} cursor={Session.Cursor.X},{Session.Cursor.Y} outcome={LastOutcome}");
}
