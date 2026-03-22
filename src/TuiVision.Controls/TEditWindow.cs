// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls.Internal;
using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Nicht-modaler Editor-Host mit Rahmen, Titel und Safe-Close-Koordination.
///
/// Non-modal editor host with frame, title, and safe-close coordination.
/// </summary>
public class TEditWindow : FramedHostView
{
    /// <summary>
    /// Initialisiert ein neues Editorfenster.
    ///
    /// Initializes a new editor window.
    /// </summary>
    /// <param name="bounds">Die Bounds des Hosts. / The host bounds.</param>
    /// <param name="editor">Der eingebettete Editor. / The embedded editor.</param>
    /// <param name="title">Der Fenstertitel. / The window title.</param>
    public TEditWindow(TRect bounds, TEditor editor, string title = "Editor") : base(bounds, title)
    {
        Editor = editor ?? throw new ArgumentNullException(nameof(editor));
        Indicator = new TIndicator(GetIndicatorBounds(), editor);
        Insert(Editor);
        Insert(Indicator);
        SetFocus(Editor);
        LayoutChildren();
        Editor.Changed += (_, _) =>
        {
            Indicator.DrawView();
            DrawView();
        };
    }

    /// <summary>
    /// Der eingebettete Editor.
    ///
    /// The embedded editor.
    /// </summary>
    public TEditor Editor { get; }

    /// <summary>
    /// Die angezeigte Statusleiste des Fensters.
    ///
    /// The displayed window indicator.
    /// </summary>
    public TIndicator Indicator { get; }

    /// <summary>
    /// Optionale Callback-Funktion fuer Verwerfungsentscheidungen.
    ///
    /// Optional callback for discard decisions.
    /// </summary>
    public Func<bool>? ConfirmDiscard { get; set; }

    /// <summary>
    /// Verarbeitet Editorbefehle auf Fenster-Ebene.
    ///
    /// Processes editor commands on the window level.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            if (TryExecuteCommand(@event.Message.Command))
            {
                @event.Clear(this);
                return;
            }
        }

        base.HandleEvent(@event);
    }

    /// <summary>
    /// Liefert die Editor-Status-Hints fuer Shell-Menues und Statuszeilen.
    ///
    /// Returns the editor status hints for shell menus and status lines.
    /// </summary>
    /// <returns>Die Hint-Kette. / The hint chain.</returns>
    public override TStatusItem? GetStatusHints() => Editor.GetStatusHints();

    /// <summary>
    /// Aktualisiert das Layout nach Groessenaenderungen.
    ///
    /// Updates the layout after size changes.
    /// </summary>
    protected override void OnBoundsChanged()
    {
        base.OnBoundsChanged();
        LayoutChildren();
    }

    /// <summary>
    /// Prueft das Safe-Close-Verhalten des Editors.
    ///
    /// Checks the editor's safe-close behaviour.
    /// </summary>
    /// <returns><c>true</c>, wenn Schliessen erlaubt ist. / <c>true</c> if closing is allowed.</returns>
    protected override bool CanClose() => Editor.CanClose(ConfirmDiscard);

    private bool TryExecuteCommand(ushort command)
    {
        switch (command)
        {
            case ShellCommandIds.cmClose:
                return RequestClose();
            case ShellCommandIds.cmUndo:
                return Editor.Undo();
            case ShellCommandIds.cmCopy:
                Editor.CopySelection();
                return true;
            case ShellCommandIds.cmCut:
                Editor.CutSelection();
                return true;
            case ShellCommandIds.cmPaste:
                Editor.PasteClipboard();
                return true;
            default:
                return false;
        }
    }

    private void LayoutChildren()
    {
        Editor.Locate(new TRect(1, 1, Math.Max(2, Size.X - 1), Math.Max(2, Size.Y - 2)));
        Indicator.Locate(GetIndicatorBounds());
    }

    private TRect GetIndicatorBounds()
    {
        return new TRect(1, Math.Max(1, Size.Y - 2), Math.Max(2, Size.X - 1), Math.Max(2, Size.Y - 1));
    }
}
