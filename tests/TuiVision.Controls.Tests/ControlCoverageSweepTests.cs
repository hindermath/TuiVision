// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Ergänzende Sweep-Tests für Randfälle und Rendering-Zustände über mehrere Controls hinweg.
///
/// Supplemental sweep tests for boundary cases and rendering states across multiple controls.
/// </summary>
[TestClass]
public sealed class ControlCoverageSweepTests
{
    /// <summary>
    /// Prüft, dass deaktivierte Buttons keine Commands auslösen.
    ///
    /// Verifies that disabled buttons do not dispatch commands.
    /// </summary>
    [TestMethod]
    public void TButton_HandleEvent_DisabledButtonDoesNotDispatchCommand()
    {
        RecordingGroup owner = new(new TRect(0, 0, 20, 6));
        owner.SetState(TViewState.Exposed, true);
        owner.SetState(TViewState.Selected, true);
        TButton button = new(new TRect(1, 1, 10, 2), "OK", ShellCommandIds.cmOK, TButtonFlags.bfNormal);
        button.SetState(TViewState.Disabled, true);
        owner.Insert(button);
        owner.SetFocus(button);

        TEvent @event = ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C);
        owner.HandleEvent(@event);

        Assert.AreEqual(0, owner.LastCommand);
        Assert.AreEqual(TEventKind.KeyDown, @event.What);
    }

    /// <summary>
    /// Prüft linksbündiges Button-Rendering innerhalb des Rahmens.
    ///
    /// Verifies left-justified button rendering inside the frame.
    /// </summary>
    [TestMethod]
    public void TButton_Draw_LeftJustifiedButtonAlignsCaptionToInnerLeft()
    {
        TButton button = new(new TRect(1, 1, 10, 2), "Go", ShellCommandIds.cmOK, TButtonFlags.bfLeftJustify);
        TGroup owner = ControlTestContext.AttachToOwner(button, new TRect(0, 0, 14, 4));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        ControlBufferAssert.AssertTextAt(buffer, 1, 1, "[Go");
    }

    /// <summary>
    /// Prüft, dass der Setter von <see cref="TInputLine.Data"/> auf <see cref="TInputLine.MaxLen"/> begrenzt.
    ///
    /// Verifies that the <see cref="TInputLine.Data"/> setter clamps to <see cref="TInputLine.MaxLen"/>.
    /// </summary>
    [TestMethod]
    public void TInputLine_DataSetter_ClampsToMaxLen()
    {
        TInputLine inputLine = new(new TRect(0, 0, 5, 1), 3)
        {
            Data = "ABCDE"
        };

        Assert.AreEqual("ABC", inputLine.Data);
        Assert.AreEqual(0, inputLine.CurPos);
        Assert.AreEqual(0, inputLine.FirstPos);
    }

    /// <summary>
    /// Prüft, dass der Dialog seinen Rahmen und Titel in den Owner-Puffer zeichnet.
    ///
    /// Verifies that the dialog draws its frame and title into the owner buffer.
    /// </summary>
    [TestMethod]
    public void TDialog_Draw_RendersFrameAndTitleIntoOwnerBuffer()
    {
        TDialog dialog = new(new TRect(1, 1, 15, 6), "Dlg");
        TGroup owner = ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 20, 10));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        ControlBufferAssert.AssertCharacterAt(buffer, 1, 1, '+');
        ControlBufferAssert.AssertTextAt(buffer, 2, 1, " Dlg ");
        ControlBufferAssert.AssertCharacterAt(buffer, 1, 5, '+');
    }

    /// <summary>
    /// Prüft, dass ein direktes Command-Ereignis den Dialog mit derselben Command-ID schließt.
    ///
    /// Verifies that a direct command event closes the dialog with the same command ID.
    /// </summary>
    [TestMethod]
    public void TDialog_Run_DirectCommandEventClosesWithMatchingResult()
    {
        PassiveDialog dialog = new(new TRect(0, 0, 20, 6), "Cmd");
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 24, 10));
        dialog.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmYes));

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmYes, result);
    }

    // -------------------------------------------------------------------------
    // T003 + T020: Widget acceptance-slice and traceability tests
    // -------------------------------------------------------------------------

    /// <summary>
    /// Prüft, dass TComboBox instanziierbar ist und die erwarteten Eigenschaften hat.
    ///
    /// Verifies that TComboBox is instantiable and has the expected properties.
    /// </summary>
    [TestMethod]
    public void Widget_TComboBox_IsInstantiableAndHasExpectedProperties()
    {
        TStringList choices = new(["A", "B"]);
        TComboBox combo = new(new TRect(0, 0, 20, 1), 20, choices, "combo-sweep");

        Assert.IsNotNull(combo.Choices);
        Assert.IsFalse(combo.DropDownOpen);
        Assert.AreEqual(-1, combo.SelectedIndex);
    }

    /// <summary>
    /// Prüft, dass TProgressBar instanziierbar ist und die erwarteten Eigenschaften hat.
    ///
    /// Verifies that TProgressBar is instantiable and has the expected properties.
    /// </summary>
    [TestMethod]
    public void Widget_TProgressBar_IsInstantiableAndHasExpectedProperties()
    {
        TProgressBar bar = new(new TRect(0, 0, 10, 1), 0, 100);

        Assert.AreEqual(0, bar.Min);
        Assert.AreEqual(100, bar.Max);
        Assert.AreEqual(0, bar.Value);
        Assert.AreEqual(ProgressBarState.Running, bar.BarState);
    }

    /// <summary>
    /// Prüft, dass TParamText eine TView-Unterklasse ist.
    ///
    /// Verifies that TParamText is a TView subclass.
    /// </summary>
    [TestMethod]
    public void Widget_TParamText_IsInstantiableAsView()
    {
        TParamText pt = new(new TRect(0, 0, 20, 1), "template");

        Assert.IsInstanceOfType<TView>(pt);
    }

    /// <summary>
    /// Prüft, dass ManagedClipboard.HasText korrekt funktioniert.
    ///
    /// Verifies that ManagedClipboard.HasText works correctly.
    /// </summary>
    [TestMethod]
    public void Widget_ManagedClipboard_HasTextIndicator()
    {
        ManagedClipboard.Clear();
        Assert.IsFalse(ManagedClipboard.HasText);

        ManagedClipboard.SetText("test");
        Assert.IsTrue(ManagedClipboard.HasText);

        ManagedClipboard.Clear();
        Assert.IsFalse(ManagedClipboard.HasText);
    }

    /// <summary>
    /// Nachverfolgbarkeitskommentar: Das Beispiel 'clipboard' ist ein nachgelagerter Konsument
    /// der ManagedClipboard- und TInputLine-Clipboard-Integration.
    /// Das Beispiel soll ManagedClipboard nutzen statt eine lokale Lösung zu implementieren.
    ///
    /// Traceability comment: The 'clipboard' example is a downstream consumer of the
    /// ManagedClipboard and TInputLine clipboard integration.
    /// The example should use ManagedClipboard instead of re-implementing locally.
    /// </summary>
    [TestMethod]
    public void Widget_TraceabilityComment_ClipboardExampleConsumer()
    {
        // The 'clipboard' example consumes ManagedClipboard.SetText/GetText/HasText
        // and TInputLine Ctrl+C/X/V — verified by confirming the contract surface exists.
        ManagedClipboard.Clear();
        ManagedClipboard.SetText("clip-trace");
        Assert.AreEqual("clip-trace", ManagedClipboard.GetText());
        ManagedClipboard.Clear();
        Assert.IsFalse(ManagedClipboard.HasText);
    }

    /// <summary>
    /// Nachverfolgbarkeitskommentar: Das Beispiel 'dyntxt' ist ein nachgelagerter Konsument
    /// von TParamText als bounded TView-Unterklasse.
    ///
    /// Traceability comment: The 'dyntxt' example is a downstream consumer of
    /// TParamText as a bounded TView subclass.
    /// </summary>
    [TestMethod]
    public void Widget_TraceabilityComment_DyntxtExampleConsumer()
    {
        // The 'dyntxt' example consumes TParamText as a bounded TView subclass.
        TParamText pt = new(new TRect(0, 0, 20, 1), "Value: {0}");
        pt.SetValues("dyntxt-trace");
        string formatted = pt.Format("dyntxt-trace");
        Assert.AreEqual("Value: dyntxt-trace", formatted);
    }

    /// <summary>
    /// Nachverfolgbarkeitskommentar: Das Beispiel 'inplis' ist ein nachgelagerter Konsument
    /// von TStringList (inkl. IEnumerable&lt;string&gt;-Konstruktor) und TListBox.
    ///
    /// Traceability comment: The 'inplis' example is a downstream consumer of
    /// TStringList (including IEnumerable&lt;string&gt; constructor) and TListBox.
    /// </summary>
    [TestMethod]
    public void Widget_TraceabilityComment_InplisExampleConsumer()
    {
        // The 'inplis' example consumes TStringList (IEnumerable constructor) and TListBox.
        TStringList list = new(["Alpha", "Beta", "Gamma"]);
        TListBox lb = new(new TRect(0, 0, 10, 3), 1, null) { List = list };
        Assert.AreEqual(3, lb.GetNumItems());
    }

    /// <summary>
    /// Nachverfolgbarkeitskommentar: Das Beispiel 'listvi' ist ein nachgelagerter Konsument
    /// von TListViewer, TListBox und TScrollBar.
    ///
    /// Traceability comment: The 'listvi' example is a downstream consumer of
    /// TListViewer, TListBox, and TScrollBar.
    /// </summary>
    [TestMethod]
    public void Widget_TraceabilityComment_ListviExampleConsumer()
    {
        // The 'listvi' example consumes TListViewer/TListBox/TScrollBar navigation.
        TScrollBar vBar = new(new TRect(0, 0, 1, 3));
        TListBox lb = new(new TRect(0, 0, 10, 3), 1, vBar) { List = new TStringList(["X", "Y", "Z"]) };
        lb.FocusItem(2);
        Assert.AreEqual(2, lb.FocusedItem);
    }

    /// <summary>
    /// Nachverfolgbarkeitskommentar: Das Beispiel 'progba' ist ein nachgelagerter Konsument
    /// von TProgressBar mit Running/Completed/Canceled-Zustandsübergängen.
    ///
    /// Traceability comment: The 'progba' example is a downstream consumer of
    /// TProgressBar with Running/Completed/Canceled state transitions.
    /// </summary>
    [TestMethod]
    public void Widget_TraceabilityComment_ProgbaExampleConsumer()
    {
        // The 'progba' example consumes TProgressBar with SetValue/Complete/Cancel.
        TProgressBar pb = new(new TRect(0, 0, 10, 1), 0, 50);
        pb.SetValue(25);
        Assert.AreEqual(25, pb.Value);
        Assert.AreEqual(ProgressBarState.Running, pb.BarState);
    }

    /// <summary>
    /// Nachverfolgbarkeitskommentar: Das Beispiel 'tcombo' ist ein nachgelagerter Konsument
    /// von TComboBox mit Dropdown-Auswahl und optionaler History-Anbindung.
    ///
    /// Traceability comment: The 'tcombo' example is a downstream consumer of
    /// TComboBox with dropdown selection and optional history integration.
    /// </summary>
    [TestMethod]
    public void Widget_TraceabilityComment_TcomboExampleConsumer()
    {
        // The 'tcombo' example consumes TComboBox with OpenDropDown/SelectIndex/CommitToHistory.
        TComboBox combo = new(new TRect(0, 0, 20, 1), 20, new TStringList(["A", "B"]));
        combo.OpenDropDown();
        combo.SelectIndex(0);
        Assert.AreEqual("A", combo.Data);
        Assert.IsFalse(combo.DropDownOpen);
    }

    /// <summary>
    /// Nachverfolgbarkeitskommentar: Das Beispiel 'tprogb' ist ein nachgelagerter Konsument
    /// von TProgressBar mit numerischem Bereich und deterministischem Fortschritt.
    ///
    /// Traceability comment: The 'tprogb' example is a downstream consumer of
    /// TProgressBar with a numeric range and determinate progress.
    /// </summary>
    [TestMethod]
    public void Widget_TraceabilityComment_TprogbExampleConsumer()
    {
        // The 'tprogb' example consumes TProgressBar.Min/Max/Value/BarState/Draw.
        TProgressBar pb = new(new TRect(0, 0, 20, 1), 0, 100);
        Assert.AreEqual(0, pb.Min);
        Assert.AreEqual(100, pb.Max);
        pb.Complete();
        Assert.AreEqual(ProgressBarState.Completed, pb.BarState);
    }

    private sealed class RecordingGroup : TGroup
    {
        public RecordingGroup(TRect bounds) : base(bounds)
        {
        }

        public ushort LastCommand { get; private set; }

        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.Command)
            {
                LastCommand = @event.Message.Command;
                @event.Clear(this);
                return;
            }

            base.HandleEvent(@event);
        }
    }

    private sealed class PassiveDialog : TDialog
    {
        private readonly Queue<TEvent> _events = new();

        public PassiveDialog(TRect bounds, string? title) : base(bounds, title)
        {
        }

        public void Enqueue(TEvent @event) => _events.Enqueue(@event);

        protected override void GetEvent(out TEvent @event)
        {
            @event = _events.Count > 0 ? _events.Dequeue() : TEvent.CreateCommand(ShellCommandIds.cmCancel);
        }
    }
}
