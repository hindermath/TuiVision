// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.TvEdit;

/// <summary>
/// Sichtbare Wave-3-Editoranwendung auf Basis der wiederverwendbaren Editor-Komponenten.
///
/// Visible Wave-3 editor application based on the reusable editor components.
/// </summary>
public sealed class TvEditApp : Wave3Application
{
    /// <summary>Beschreibung anzeigen. / Show description.</summary>
    public const ushort CmDescription = 19001;
    /// <summary>Safe-Close anfordern. / Request safe close.</summary>
    public const ushort CmRequestClose = 19002;
    /// <summary>In den explizit erlaubten Test-Root speichern. / Save inside the explicitly allowed test root.</summary>
    public const ushort CmSaveOwned = 19003;

    private readonly string? _allowedOutputDirectory;

    /// <summary>Initialisiert die Editoranwendung. / Initializes the editor application.</summary>
    public TvEditApp(TRect bounds, bool headless = false, string? allowedOutputDirectory = null) : base(bounds, headless)
    {
        _allowedOutputDirectory = allowedOutputDirectory is null ? null : Path.GetFullPath(allowedOutputDirectory);
        TRect region = Wave3Runtime.MainRegion(Desktop!, 68, 17);
        Editor = new TFileEditor(new TRect(1, 1, Math.Max(2, region.Width - 1), Math.Max(2, region.Height - 2)));
        Editor.LoadText("TvEdit learner buffer\nReady for keyboard editing.", markClean: true);
        EditWindow = new TEditWindow(region, Editor, "TvEdit - controlled learner buffer");
        Desktop!.Insert(EditWindow);
        Desktop.SetFocus(EditWindow);
        SetWave3Visible(EditWindow, "TEditWindow");
        SetWave3Status("TvEdit", "buffer=embedded clean");
        Editor.Changed += (_, _) => SetWave3Status("TvEdit", Editor.Modified ? "buffer=embedded modified" : "buffer=embedded clean");
    }

    /// <summary>Der echte Dateieditor. / The real file editor.</summary>
    public TFileEditor Editor { get; }

    /// <summary>Das sichtbare Editorfenster. / The visible editor window.</summary>
    public TEditWindow EditWindow { get; }

    /// <summary>Ob eine Safe-Close-Entscheidung angefordert wurde. / Whether a safe-close decision was requested.</summary>
    public bool CloseDecisionRequested { get; private set; }

    /// <summary>Ob das letzte Safe-Close akzeptiert wurde. / Whether the last safe close was accepted.</summary>
    public bool CloseAccepted { get; private set; }

    /// <summary>Fensterzustand direkt nach der Close-Entscheidung. / Window state immediately after the close decision.</summary>
    public bool WindowPresentAfterCloseAttempt { get; private set; }

    /// <summary>Ob der letzte Save-Pfad abgelehnt wurde. / Whether the last save path was rejected.</summary>
    public bool LastSaveRejected { get; private set; }

    /// <summary>Letzter akzeptierter Save-Pfad. / Last accepted save path.</summary>
    public string? LastSavePath { get; private set; }

    /// <summary>Text für den Beschreibungspfad. / Text for the description path.</summary>
    public string DescriptionText =>
        "TvEdit description: Der echte TFileEditor zeigt Puffer, Cursor und Modified-State; " +
        "Schliessen braucht bei Aenderungen eine ausdrueckliche Entscheidung. / " +
        "The real TFileEditor shows buffer, cursor, and modified state; changed content requires an explicit close decision.";

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuBar bar = new(bounds)
        {
            Menu = new TMenuItem(
                "~D~atei / ~F~ile",
                0,
                Wave3Runtime.HelpMenu(CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit)),
                new TMenuItem("~S~chliessen / ~C~lose", CmRequestClose))
        };
        return bar;
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmDescription)
        {
            ShowWave3Description("TvEdit", DescriptionText);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmRequestClose)
        {
            bool discard = @event.Message.Info is true;
            CloseDecisionRequested = true;
            EditWindow.ConfirmDiscard = () => discard;
            CloseAccepted = EditWindow.RequestClose();
            WindowPresentAfterCloseAttempt = EditWindow.Owner is not null;
            SetWave3Status("TvEdit", CloseAccepted ? "close accepted" : "close rejected: unsaved changes");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmSaveOwned)
        {
            SaveOwned(@event.Message.Info as string);
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private void SaveOwned(string? relativePath)
    {
        LastSaveRejected = true;
        if (_allowedOutputDirectory is null || string.IsNullOrWhiteSpace(relativePath) || Path.IsPathRooted(relativePath))
        {
            SetWave3Status("TvEdit", "save rejected: no owned target");
            return;
        }

        string root = Path.TrimEndingDirectorySeparator(_allowedOutputDirectory);
        string candidate = Path.GetFullPath(Path.Combine(root, relativePath));
        string prefix = root + Path.DirectorySeparatorChar;
        // Der Root-Praefix ist die Proof-Grenze: ein gueltiger Dateiname darf den test-eigenen Baum nie verlassen.
        // The root prefix is the proof boundary: a valid file name must never leave the test-owned tree.
        if (!candidate.StartsWith(prefix, StringComparison.Ordinal))
        {
            SetWave3Status("TvEdit", "save rejected: outside owned root");
            return;
        }

        Directory.CreateDirectory(root);
        if (!Editor.SaveAs(candidate))
        {
            SetWave3Status("TvEdit", "save rejected: overwrite decision required");
            return;
        }

        LastSaveRejected = false;
        LastSavePath = candidate;
        SetWave3Status("TvEdit", $"saved={Path.GetFileName(candidate)} clean");
    }
}
