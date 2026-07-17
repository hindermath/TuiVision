// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Wave5;

/// <summary>Funktionales TP7-Editorbeispiel. / Functional TP7 editor example.</summary>
public sealed class Tp7EditApp : Wave5Application
{
    /// <summary>Zeigt einen begrenzten Edit-Aktionsstatus. / Shows a bounded edit-action status.</summary>
    public const ushort CmEditAction = 32203;

    /// <summary>Zeigt einen begrenzten Search-Aktionsstatus. / Shows a bounded search-action status.</summary>
    public const ushort CmSearchAction = 32204;

    /// <summary>Fordert Safe-Close an. / Requests safe close.</summary>
    public const ushort CmRequestClose = 32201;

    /// <summary>Speichert in das kontrollierte Ziel. / Saves to the controlled target.</summary>
    public const ushort CmSaveOwned = 32202;

    /// <summary>Initialisiert den Editor. / Initializes the editor.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    /// <param name="allowedOutputDirectory">Optionales test-eigenes Ziel. / Optional test-owned target.</param>
    public Tp7EditApp(TRect bounds, bool headless = false, string? allowedOutputDirectory = null) : base(bounds, headless)
    {
        AllowedOutputDirectory = allowedOutputDirectory is null ? null : Path.GetFullPath(allowedOutputDirectory);
        TRect region = new(2, 1, Math.Max(14, Desktop!.Size.X - 2), Math.Max(7, Desktop.Size.Y - 2));
        Editor = new TFileEditor(new TRect(1, 1, Math.Max(3, region.Width - 1), Math.Max(3, region.Height - 2)));
        Editor.LoadText("TP7 editor fixture\nReady.", markClean: true);
        EditWindow = new TEditWindow(region, Editor, "TP7 Edit");
        Desktop.Insert(EditWindow);
        Desktop.SetFocus(EditWindow);
        EditWindow.SetFocus(Editor);
        TrackVisibleView(EditWindow, nameof(TEditWindow));
        FocusedControlKind = EditWindow.Current?.GetType().Name ?? string.Empty;
        SetStatus("Tp7Edit", "ready");
        Editor.Changed += (_, _) => SetStatus("Tp7Edit", Editor.Modified ? "modified" : "clean");
    }

    /// <summary>Optionales kontrolliertes Ziel. / Optional controlled target.</summary>
    public string? AllowedOutputDirectory { get; }

    /// <summary>Echter Dateieditor. / Real file editor.</summary>
    public TFileEditor Editor { get; }

    /// <summary>Sichtbares Editorfenster. / Visible editor window.</summary>
    public TEditWindow EditWindow { get; }

    /// <summary>Typ des initial fokussierten Editorcontrols. / Type of the initially focused editor control.</summary>
    public string FocusedControlKind { get; }

    /// <summary>Letztes Safe-Close-Ergebnis. / Last safe-close result.</summary>
    public bool CloseAccepted { get; private set; }

    /// <summary>Ob das Fenster nach Close noch vorhanden war. / Whether the window remained after close.</summary>
    public bool WindowPresentAfterCloseAttempt { get; private set; }

    /// <summary>Letztes Save-Ergebnis. / Last save result.</summary>
    public bool SaveAccepted { get; private set; }

    /// <summary>Letzte beobachtete Konfliktart. / Last observed conflict kind.</summary>
    public TFileSaveConflictKind? LastConflictKind { get; private set; }

    /// <summary>Beschreibt eine kontrollierte Save-Entscheidung. / Describes a controlled save decision.</summary>
    /// <param name="RelativePath">Relativer Pfad. / Relative path.</param>
    /// <param name="AcceptConflict">Konflikt akzeptieren. / Accept conflict.</param>
    public readonly record struct SaveRequest(string RelativePath, bool AcceptConflict);

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds) =>
        new(bounds)
        {
            Menu = new TMenuItem(
                "~F~ile",
                0,
                new TMenuItem(
                    "~E~dit",
                    0,
                    new TMenuItem(
                        "~S~earch",
                        0,
                        new TMenuItem(
                            "~H~elp",
                            0,
                            null,
                            new TMenuItem("~D~escription", CmDescription)),
                        new TMenuItem(
                            "~F~ind",
                            CmSearchAction,
                            new TMenuItem("~R~eplace", ShellCommandIds.cmReplace))),
                    new TMenuItem(
                        "~U~ndo",
                        CmEditAction,
                        new TMenuItem(
                            "~C~opy",
                            ShellCommandIds.cmCopy,
                            new TMenuItem("~P~aste", ShellCommandIds.cmPaste)))),
                new TMenuItem(
                    "~S~ave",
                    CmSaveOwned,
                    new TMenuItem("~C~lose", CmRequestClose)))
        };

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmRequestClose)
        {
            bool discard = @event.Message.Info is true;
            EditWindow.ConfirmDiscard = () => discard;
            CloseAccepted = EditWindow.RequestClose();
            WindowPresentAfterCloseAttempt = EditWindow.Owner is not null;
            SetStatus("Tp7Edit", CloseAccepted ? "close accepted" : "close rejected: unsaved changes");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmSaveOwned)
        {
            SaveOwned(@event.Message.Info is SaveRequest request ? request : default);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmEditAction)
        {
            SetStatus("Tp7Edit", "Edit menu action");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmSearchAction)
        {
            SetStatus("Tp7Edit", "Search menu action");
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    /// <inheritdoc />
    protected override string BuildDescriptionText() =>
        """
        Historischer Lernzweck: Der Editor verbindet File-, Edit- und Search-Menüs mit einem fokussierten Textpuffer.
        Historical learning purpose: the editor connects File, Edit, and Search menus with a focused text buffer.
        Tastatur: Menümnemoniken, Texttasten, Save, Close, Find, Replace, Tab und F1 sind erreichbar.
        Keyboard: menu mnemonics, text keys, Save, Close, Find, Replace, Tab, and F1 are reachable.
        Modernes C# nutzt TFileEditor und ausdrückliche Safe-Close- sowie Konfliktentscheidungen.
        Modern C# uses TFileEditor and explicit safe-close and conflict decisions.
        Speichern ist nur unter dem kontrollierten Root erlaubt; Traversal und implizites Überschreiben werden abgelehnt.
        Saving is allowed only below the controlled root; traversal and implicit overwrite are rejected.
        Der App-Loop-Smoke beweist Fokus, Änderung, Entscheidung, Status und Zellen, nicht beliebige Benutzerdateien.
        The app-loop smoke proves focus, editing, decisions, status, and cells, not arbitrary user files.
        """;

    private void SaveOwned(SaveRequest request)
    {
        SaveAccepted = false;
        LastConflictKind = null;
        if (AllowedOutputDirectory is null
            || string.IsNullOrWhiteSpace(request.RelativePath)
            || Path.IsPathRooted(request.RelativePath))
        {
            SetStatus("Tp7Edit", "save rejected: no controlled target");
            return;
        }

        string root = Path.TrimEndingDirectorySeparator(AllowedOutputDirectory);
        string candidate = Path.GetFullPath(Path.Combine(root, request.RelativePath));
        string prefix = root + Path.DirectorySeparatorChar;
        // Der Präfixvergleich ist die kontrollierte Proof-Grenze; relative Segmente dürfen den Test-Root nicht verlassen.
        // The prefix comparison is the controlled proof boundary; relative segments must not leave the test root.
        if (!candidate.StartsWith(prefix, StringComparison.Ordinal))
        {
            SetStatus("Tp7Edit", "save rejected: outside controlled root");
            return;
        }

        Directory.CreateDirectory(root);
        Editor.ConfirmOverwrite = conflict =>
        {
            LastConflictKind = conflict.Kind;
            return request.AcceptConflict;
        };
        SaveAccepted = Editor.SaveAs(candidate);
        SetStatus(
            "Tp7Edit",
            SaveAccepted
                ? $"saved={Path.GetFileName(candidate)}"
                : $"save rejected: conflict={LastConflictKind}");
    }
}
