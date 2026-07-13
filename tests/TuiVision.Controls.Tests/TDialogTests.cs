// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TDialog"/> als modalen Koordinator.
///
/// Tests for <see cref="TDialog"/> as a modal coordinator.
/// </summary>
[TestClass]
public sealed class TDialogTests
{
    /// <summary>
    /// Prüft owner-gebundene modale Ausführung, Ergebnis, temporäre Einfügung,
    /// Event-Isolation, Cleanup und Fokuswiederherstellung.
    ///
    /// Verifies owner-scoped modal execution, result, temporary insertion,
    /// event isolation, cleanup, and focus restoration.
    /// </summary>
    [TestMethod]
    public void TDialog_F006_ExecuteModalIsolatesAndRestoresOwnershipAndFocus()
    {
        TGroup owner = new(new TRect(0, 0, 40, 12));
        CountingView previous = new(new TRect(0, 0, 8, 2));
        owner.Insert(previous);
        owner.SetFocus(previous);
        TestDialog dialog = new(new TRect(5, 2, 25, 8), "Modal");
        dialog.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmOK));

        ushort result = owner.ExecuteModal(dialog);

        Assert.AreEqual(ShellCommandIds.cmOK, result);
        Assert.AreEqual(0, previous.CommandCount, "Modal events must not escape to owner siblings.");
        Assert.IsNull(dialog.Owner);
        Assert.IsFalse(dialog.GetState(TViewState.Modal));
        Assert.AreSame(previous, owner.Current);
    }

    /// <summary>
    /// Prüft erlaubte Verschachtelung unter dem aktiven Dialog und die Ablehnung
    /// eines zweiten direkten modalen Kinds desselben Owners.
    ///
    /// Verifies allowed nesting below the active dialog and rejection of a second
    /// direct modal child on the same owner.
    /// </summary>
    [TestMethod]
    public void TDialog_F006_NestingIsChildScopedAndSameOwnerReentryIsRejected()
    {
        TGroup owner = new(new TRect(0, 0, 40, 12));
        TestDialog child = new(new TRect(2, 1, 18, 7), "Child");
        child.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmOK));
        NestedDialog parent = new(new TRect(1, 1, 30, 10), child);

        ushort result = owner.ExecuteModal(parent);

        Assert.AreEqual(ShellCommandIds.cmCancel, result);
        Assert.AreEqual(ShellCommandIds.cmOK, parent.NestedResult);
        Assert.IsTrue(parent.SameOwnerRejected);
        Assert.IsNull(parent.Owner);
        Assert.IsNull(child.Owner);
    }

    /// <summary>
    /// Prüft Cleanup und Fokusrestaurierung bei Exception sowie kontrollierten
    /// Abbruch, wenn der Owner während der modalen Ausführung herunterfährt.
    ///
    /// Verifies cleanup and focus restoration on exception plus controlled abort
    /// when the owner shuts down during modal execution.
    /// </summary>
    [TestMethod]
    public void TDialog_F006_ExceptionAndShutdownAlwaysCleanModalState()
    {
        TGroup owner = new(new TRect(0, 0, 40, 12));
        CountingView previous = new(new TRect(0, 0, 8, 2));
        owner.Insert(previous);
        owner.SetFocus(previous);
        ThrowingDialog throwing = new(new TRect(1, 1, 20, 8), "Throw");

        Assert.ThrowsExactly<InvalidOperationException>(() => owner.ExecuteModal(throwing));
        Assert.IsNull(throwing.Owner);
        Assert.IsFalse(throwing.GetState(TViewState.Modal));
        Assert.AreSame(previous, owner.Current);

        ShutdownDialog shutdown = new(new TRect(1, 1, 20, 8), "Shutdown");
        ushort result = owner.ExecuteModal(shutdown);
        Assert.AreEqual(ShellCommandIds.cmCancel, result);
        Assert.IsNull(shutdown.Owner);
        Assert.IsFalse(shutdown.GetState(TViewState.Modal));
        Assert.IsNull(previous.Owner);
    }
    /// <summary>
    /// Prüft, dass Escape den Dialog mit <c>cmCancel</c> beendet und ein Dialog ohne fokussierbares Kind stabil bleibt.
    ///
    /// Verifies that Escape closes the dialog with <c>cmCancel</c> and that a dialog without a focusable child stays stable.
    /// </summary>
    [TestMethod]
    public void TDialog_Run_EscapeReturnsCancelWithoutFocusableChild()
    {
        TestDialog dialog = new(new TRect(0, 0, 20, 6), "Test");
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(scanCode: 0x01));
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 24, 10));

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmCancel, result);
        Assert.IsNull(dialog.Current);
    }

    /// <summary>
    /// Prüft Fokus-Wrap-around via Tab und Shift-Tab.
    ///
    /// Verifies focus wrap-around via Tab and Shift-Tab.
    /// </summary>
    [TestMethod]
    public void TDialog_HandleEvent_TabNavigationWrapsAround()
    {
        TestDialog dialog = CreateDialogWithTwoButtons(out TButton okButton, out TButton cancelButton);
        dialog.SetFocus(cancelButton);

        TEvent forward = ControlEventFactory.CreateKeyDown(charCode: '\t', scanCode: 0x0F);
        dialog.HandleEvent(forward);

        Assert.AreSame(okButton, dialog.Current);
        Assert.AreEqual(TEventKind.Nothing, forward.What);

        TEvent backward = ControlEventFactory.CreateKeyDown(charCode: '\t', scanCode: 0x0F, shiftState: 0x0001);
        dialog.HandleEvent(backward);

        Assert.AreSame(cancelButton, dialog.Current);
        Assert.AreEqual(TEventKind.Nothing, backward.What);
    }

    /// <summary>
    /// Prüft, dass Mausereignisse außerhalb eines modalen Dialogs ignoriert werden.
    ///
    /// Verifies that mouse events outside a modal dialog are ignored.
    /// </summary>
    [TestMethod]
    public void TDialog_HandleEvent_MouseOutsideDialogIsIgnored()
    {
        TestDialog dialog = CreateDialogWithTwoButtons(out _, out _);

        TEvent @event = ControlEventFactory.CreateMouseDown(30, 30);
        dialog.HandleEvent(@event);

        Assert.AreEqual(TEventKind.Nothing, @event.What);
    }

    /// <summary>
    /// Prüft, dass ein Cancel-Button als Rückgabewert durchgereicht wird.
    ///
    /// Verifies that a cancel button result is returned by the dialog.
    /// </summary>
    [TestMethod]
    public void TDialog_Run_CancelButtonReturnsCancelCommand()
    {
        TestDialog dialog = CreateDialogWithTwoButtons(out _, out TButton cancelButton);
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C));
        dialog.SetFocus(cancelButton);

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmCancel, result);
    }

    /// <summary>
    /// Prüft, dass Enter auf einem nicht konsumierenden Kind den Default-Button aktiviert.
    ///
    /// Verifies that Enter on a non-consuming child activates the default button.
    /// </summary>
    [TestMethod]
    public void TDialog_Run_EnterActivatesDefaultButtonWhenChildDoesNotConsume()
    {
        TestDialog dialog = new(new TRect(0, 0, 30, 8), "Default");
        TInputLine inputLine = new(new TRect(2, 2, 12, 3), 10);
        TButton okButton = new(new TRect(2, 4, 10, 5), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        dialog.Insert(inputLine);
        dialog.Insert(okButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 40, 12));
        dialog.SetFocus(inputLine);
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C));

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmOK, result);
        Assert.AreSame(inputLine, dialog.Current);
    }

    /// <summary>
    /// Prüft, dass ein Kind-Control den Enter-Key konsumieren kann, ohne den Default-Button auszulösen.
    ///
    /// Verifies that a child control can consume Enter without triggering the default button.
    /// </summary>
    [TestMethod]
    public void TDialog_Run_ChildControlCanConsumeEnterBeforeDefaultButton()
    {
        TestDialog dialog = new(new TRect(0, 0, 30, 8), "Consume");
        EnterConsumingView consumer = new(new TRect(2, 2, 12, 3));
        TButton okButton = new(new TRect(2, 4, 10, 5), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        dialog.Insert(consumer);
        dialog.Insert(okButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 40, 12));
        dialog.SetFocus(consumer);
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C));
        dialog.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmCancel));

        ushort result = dialog.Run();

        Assert.AreEqual(1, consumer.EnterCount);
        Assert.AreEqual(ShellCommandIds.cmCancel, result);
    }

    // -----------------------------------------------------------------------
    // T019: Dialog validation tests (RED – fail until T021 adds Valid() hook)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob Valid(ushort) false den Dialog offen hält (kein Schließen bei Ablehnung).
    ///
    /// Verifies that Valid(ushort) returning false keeps the dialog open.
    /// </summary>
    [TestMethod]
    public void TDialog_Valid_RejectsCloseWhenValidReturnsFalse()
    {
        RejectingDialog dialog = new(new TRect(0, 0, 20, 6), "Reject");
        TButton okButton = new(new TRect(2, 2, 10, 3), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        dialog.Insert(okButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 30, 10));

        // Befehl cmOK direkt senden – Valid() gibt false zurück, CloseDialog wird NICHT aufgerufen.
        // Send cmOK directly – Valid() returns false, CloseDialog must NOT be called.
        TEvent okCmd = ControlEventFactory.CreateCommand(ShellCommandIds.cmOK);
        dialog.HandleEvent(okCmd);

        Assert.AreEqual(1, dialog.ValidCallCount,
            "The real completion path must invoke the overridable validation hook once.");
    }

    /// <summary>
    /// Prüft, ob Valid(ushort) true den Dialog schließt und den Ergebniswert setzt.
    ///
    /// Verifies that Valid(ushort) returning true closes the dialog with the expected result.
    /// </summary>
    [TestMethod]
    public void TDialog_Valid_AcceptsCloseWhenValidReturnsTrue()
    {
        TestDialog dialog = new(new TRect(0, 0, 20, 6), "Accept");
        TButton okButton = new(new TRect(2, 2, 10, 3), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        dialog.Insert(okButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 30, 10));
        dialog.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmOK));
        dialog.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmCancel)); // Fallback

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmOK, result,
            "Dialog must close with cmOK when Valid() returns true.");
    }

    /// <summary>
    /// Stellt sicher, dass das bestehende Escape/OK-Verhalten nicht regressiert.
    ///
    /// Ensures that the existing Escape/OK behavior is not regressed.
    /// </summary>
    [TestMethod]
    public void TDialog_ExistingModalBehavior_NotRegressed()
    {
        TestDialog dialog = new(new TRect(0, 0, 30, 8), "Regression");
        TButton okButton = new(new TRect(2, 2, 10, 3), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        TButton cancelButton = new(new TRect(12, 2, 24, 3), "Cancel", ShellCommandIds.cmCancel, TButtonFlags.bfNormal);
        dialog.Insert(okButton);
        dialog.Insert(cancelButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 40, 12));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(scanCode: 0x01)); // Escape

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmCancel, result,
            "Existing Escape→cmCancel behavior must not be regressed by Valid() hook introduction.");
    }

    /// <summary>
    /// Prüft, dass Hilfe-, Anwendungs- und unbekannte Commands nicht als
    /// Dialogabschluss konsumiert werden.
    ///
    /// Verifies that help, application, and unknown commands are not consumed as
    /// dialog completion.
    /// </summary>
    [TestMethod]
    public void TDialog_F010_NonCompletionCommandsRemainOpenAndUnconsumed()
    {
        TDialog dialog = new(new TRect(0, 0, 20, 6), "Commands");
        ushort[] commands = [ShellCommandIds.cmHelp, 0x7001, ushort.MaxValue];

        foreach (ushort command in commands)
        {
            TEvent @event = ControlEventFactory.CreateCommand(command);

            dialog.HandleEvent(@event);

            Assert.AreEqual(TEventKind.Command, @event.What, $"Command {command} must remain available to its owner.");
        }
    }

    /// <summary>
    /// Prüft die stabile Kindreihenfolge, die erste Ablehnung und den Fokuspfad
    /// über den echten Completion-Command.
    ///
    /// Verifies stable child order, first rejection, and focus routing through the
    /// real completion command.
    /// </summary>
    [TestMethod]
    public void TDialog_F010_AcceptanceStopsAtFirstRejectionAndFocusesTarget()
    {
        TDialog dialog = new(new TRect(0, 0, 30, 8), "Validate");
        ValidationProbe first = new(new TRect(1, 1, 8, 2), isValid: true, "first");
        ValidationProbe rejected = new(new TRect(1, 2, 8, 3), isValid: false, "invalid value");
        ValidationProbe skipped = new(new TRect(1, 3, 8, 4), isValid: true, "skipped");
        dialog.Insert(first);
        dialog.Insert(rejected);
        dialog.Insert(skipped);
        dialog.SetFocus(first);

        TEvent @event = ControlEventFactory.CreateCommand(ShellCommandIds.cmOK);
        dialog.HandleEvent(@event);

        Assert.AreEqual(1, first.ValidationCount);
        Assert.AreEqual(1, rejected.ValidationCount);
        Assert.AreEqual(0, skipped.ValidationCount);
        Assert.AreSame(rejected, dialog.Current);
        Assert.IsFalse(dialog.LastValidationResult.IsValid);
        Assert.AreEqual("invalid value", dialog.LastValidationResult.Message);
        Assert.AreSame(rejected, dialog.LastValidationResult.Target);
        Assert.AreEqual(TEventKind.Nothing, @event.What);
    }

    /// <summary>
    /// Prüft, dass Cancel keine Inhaltsvalidierung auslöst.
    ///
    /// Verifies that Cancel does not invoke content validation.
    /// </summary>
    [TestMethod]
    public void TDialog_F010_CancelBypassesContentValidation()
    {
        TestDialog dialog = new(new TRect(0, 0, 20, 6), "Cancel");
        ValidationProbe rejected = new(new TRect(1, 1, 8, 2), isValid: false, "invalid value");
        dialog.Insert(rejected);
        dialog.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmCancel));

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmCancel, result);
        Assert.AreEqual(0, rejected.ValidationCount);
    }

    /// <summary>
    /// Prüft einen begrenzten abgeleiteten Completion-Command ohne versteckte
    /// Event-Loop-Überschreibung.
    ///
    /// Verifies one bounded derived completion command without a hidden event-loop
    /// override.
    /// </summary>
    [TestMethod]
    public void TDialog_F010_DerivedClassifierExtendsCompletionSet()
    {
        const ushort customCompletion = 0x7100;
        ExtendedCompletionDialog dialog = new(new TRect(0, 0, 20, 6), customCompletion);
        dialog.Enqueue(ControlEventFactory.CreateCommand(customCompletion));

        ushort result = dialog.Run();

        Assert.AreEqual(customCompletion, result);
    }

    /// <summary>
    /// Prüft die Validator-Ablehnung einer Eingabe über den echten Dialogpfad
    /// einschließlich Fokus und text-first Meldung.
    ///
    /// Verifies input-validator rejection through the real dialog path, including
    /// focus and a text-first message.
    /// </summary>
    [TestMethod]
    public void TDialog_F011_InputValidationRejectsAcceptanceWithAccessibleEvidence()
    {
        TDialog dialog = new(new TRect(0, 0, 24, 7), "Range");
        TInputLine input = new(new TRect(1, 1, 8, 2), 8)
        {
            Data = "5",
            Validator = new TRangeValidator(10, 20)
        };
        dialog.Insert(input);
        dialog.SetFocus(input);

        dialog.HandleEvent(ControlEventFactory.CreateCommand(ShellCommandIds.cmOK));

        Assert.AreSame(input, dialog.Current);
        Assert.AreSame(input, dialog.LastValidationResult.Target);
        Assert.AreEqual(TValidationPhase.Acceptance, dialog.LastValidationResult.Phase);
        StringAssert.Contains(dialog.LastValidationResult.Message!, "ungültig");
    }

    private static TestDialog CreateDialogWithTwoButtons(out TButton okButton, out TButton cancelButton)
    {
        TestDialog dialog = new(new TRect(0, 0, 30, 8), "Test");
        okButton = new TButton(new TRect(2, 2, 10, 3), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        cancelButton = new TButton(new TRect(12, 2, 24, 3), "Cancel", ShellCommandIds.cmCancel, TButtonFlags.bfNormal);
        dialog.Insert(okButton);
        dialog.Insert(cancelButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 40, 12));
        return dialog;
    }

    private sealed class TestDialog : TDialog
    {
        private readonly Queue<TEvent> _events = new();

        public TestDialog(TRect bounds, string? title) : base(bounds, title)
        {
        }

        public void Enqueue(TEvent @event) => _events.Enqueue(@event);

        protected override void GetEvent(out TEvent @event)
        {
            @event = _events.Count > 0 ? _events.Dequeue() : TEvent.CreateCommand(ShellCommandIds.cmCancel);
        }
    }

    private sealed class ExtendedCompletionDialog : TDialog
    {
        private readonly Queue<TEvent> _events = new();
        private readonly ushort _customCompletion;

        public ExtendedCompletionDialog(TRect bounds, ushort customCompletion) : base(bounds, "Extended") =>
            _customCompletion = customCompletion;

        public void Enqueue(TEvent @event) => _events.Enqueue(@event);

        protected override bool IsCompletionCommand(ushort command) =>
            command == _customCompletion || base.IsCompletionCommand(command);

        protected override void GetEvent(out TEvent @event) =>
            @event = _events.Count > 0 ? _events.Dequeue() : TEvent.CreateCommand(ShellCommandIds.cmCancel);
    }

    private sealed class ValidationProbe : TView
    {
        private readonly bool _isValid;
        private readonly string _message;

        public ValidationProbe(TRect bounds, bool isValid, string message) : base(bounds)
        {
            _isValid = isValid;
            _message = message;
            Options |= TViewOptions.Selectable;
        }

        public int ValidationCount { get; private set; }

        public override TValidationResult Validate(TValidationPhase phase)
        {
            ValidationCount++;
            return _isValid
                ? TValidationResult.Accepted(phase)
                : TValidationResult.Rejected(phase, _message, this);
        }
    }

    /// <summary>
    /// Dialog, dessen Valid()-Methode stets <c>false</c> zurückgibt und Aufrufe zählt.
    ///
    /// Dialog whose Valid() method always returns <c>false</c> and counts calls.
    /// </summary>
    private sealed class RejectingDialog : TDialog
    {
        public RejectingDialog(TRect bounds, string? title) : base(bounds, title) { }

        /// <summary>
        /// Anzahl der Valid-Aufrufe. / Number of Valid invocations.
        /// </summary>
        public int ValidCallCount { get; private set; }

        /// <summary>
        /// Gibt stets <c>false</c> zurück, um das Schließen zu verhindern.
        ///
        /// Always returns <c>false</c> to prevent closing.
        /// </summary>
        /// <param name="command">Die Befehl-ID. / The command ID.</param>
        /// <returns><c>false</c></returns>
        protected override bool Valid(ushort command)
        {
            ValidCallCount++;
            return false;
        }
    }

    private sealed class EnterConsumingView : TView
    {
        public EnterConsumingView(TRect bounds) : base(bounds)
        {
            Options = TViewOptions.Selectable;
        }

        public int EnterCount { get; private set; }

        public override void HandleEvent(TEvent @event)
        {
            base.HandleEvent(@event);
            if (@event.What == TEventKind.KeyDown && (@event.KeyDown.ScanCode == 0x1C || @event.KeyDown.CharCode == '\r'))
            {
                EnterCount++;
                @event.Clear(this);
            }
        }
    }

    private sealed class CountingView : TView
    {
        public CountingView(TRect bounds) : base(bounds) => Options |= TViewOptions.Selectable;

        public int CommandCount { get; private set; }

        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.Command)
            {
                CommandCount++;
            }

            base.HandleEvent(@event);
        }
    }

    private sealed class NestedDialog : TDialog
    {
        private readonly TestDialog _child;
        private bool _handled;

        public NestedDialog(TRect bounds, TestDialog child) : base(bounds, "Parent") => _child = child;

        public ushort NestedResult { get; private set; }

        public bool SameOwnerRejected { get; private set; }

        protected override void GetEvent(out TEvent @event)
        {
            if (!_handled)
            {
                _handled = true;
                NestedResult = ExecuteModal(_child);
                try
                {
                    Owner!.ExecuteModal(new TestDialog(new TRect(2, 2, 12, 6), "Peer"));
                }
                catch (InvalidOperationException)
                {
                    SameOwnerRejected = true;
                }
            }

            @event = TEvent.CreateCommand(ShellCommandIds.cmCancel);
        }
    }

    private sealed class ThrowingDialog(TRect bounds, string title) : TDialog(bounds, title)
    {
        protected override void GetEvent(out TEvent @event) =>
            throw new InvalidOperationException("Deterministic modal failure.");
    }

    private sealed class ShutdownDialog(TRect bounds, string title) : TDialog(bounds, title)
    {
        private bool _shutdown;

        protected override void GetEvent(out TEvent @event)
        {
            if (!_shutdown)
            {
                _shutdown = true;
                Owner!.ShutDown();
            }

            @event = TEvent.CreateNone();
        }
    }
}
