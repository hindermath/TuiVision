// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TInputLine"/> und die Editierlogik.
///
/// Tests for <see cref="TInputLine"/> and its editing logic.
/// </summary>
[TestClass]
public sealed class TInputLineTests
{
    /// <summary>
    /// Prüft Text-Eingabe, Cursorbewegung, Insert/Overwrite und Löschen.
    ///
    /// Verifies text entry, cursor movement, insert/overwrite, and deletion.
    /// </summary>
    [TestMethod]
    public void TInputLine_HandleEvent_SupportsEditingAndCursorMovement()
    {
        TInputLine inputLine = new(new TRect(0, 0, 6, 1), 6);

        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'A'));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'B'));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'C'));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x4B));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x52));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'X'));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x53));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x0E));

        Assert.AreEqual("AB", inputLine.Data);
        Assert.AreEqual(2, inputLine.CurPos);
        Assert.IsFalse(inputLine.InsertMode);
    }

    /// <summary>
    /// Prüft horizontales Scrolling bei Eingaben über die sichtbare Breite hinaus.
    ///
    /// Verifies horizontal scrolling when the input exceeds the visible width.
    /// </summary>
    [TestMethod]
    public void TInputLine_HandleEvent_LongInputAdvancesViewport()
    {
        TInputLine inputLine = new(new TRect(0, 0, 4, 1), 10);
        TGroup owner = ControlTestContext.AttachToOwner(inputLine, new TRect(0, 0, 8, 3));

        foreach (char ch in "ABCDE")
        {
            inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: ch));
        }

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        Assert.AreEqual(2, inputLine.FirstPos);
        ControlBufferAssert.AssertRowEquals(buffer, 0, "CDE     ");
    }

    /// <summary>
    /// Prüft den Randfall <c>MaxLen = 0</c>.
    ///
    /// Verifies the <c>MaxLen = 0</c> boundary case.
    /// </summary>
    [TestMethod]
    public void TInputLine_HandleEvent_MaxLenZeroRejectsInput()
    {
        TInputLine inputLine = new(new TRect(0, 0, 5, 1), 0);

        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'A'));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x4D));

        Assert.AreEqual(string.Empty, inputLine.Data);
        Assert.AreEqual(0, inputLine.CurPos);
        Assert.AreEqual(0, inputLine.FirstPos);
    }

    /// <summary>
    /// Prüft, dass Strg+C den aktuellen Text in die Zwischenablage kopiert.
    ///
    /// Verifies that Ctrl+C copies the current text to the clipboard.
    /// </summary>
    [TestMethod]
    public void TInputLine_HandleEvent_CtrlC_CopiesTextToClipboard()
    {
        ManagedClipboard.Clear();
        TInputLine inputLine = new(new TRect(0, 0, 10, 1), 20);
        foreach (char ch in "Hello")
        {
            inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: ch));
        }

        inputLine.HandleEvent(ControlEventFactory.CreateCtrlC());

        Assert.AreEqual("Hello", ManagedClipboard.GetText());
    }

    /// <summary>
    /// Prüft, dass Strg+X den Text ausschneidet und in die Zwischenablage kopiert.
    ///
    /// Verifies that Ctrl+X cuts the text and copies it to the clipboard.
    /// </summary>
    [TestMethod]
    public void TInputLine_HandleEvent_CtrlX_CutsTextToClipboard()
    {
        ManagedClipboard.Clear();
        TInputLine inputLine = new(new TRect(0, 0, 10, 1), 20);
        foreach (char ch in "Hello")
        {
            inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: ch));
        }

        inputLine.HandleEvent(ControlEventFactory.CreateCtrlX());

        Assert.AreEqual("Hello", ManagedClipboard.GetText());
        Assert.AreEqual(string.Empty, inputLine.Data);
    }

    /// <summary>
    /// Prüft, dass Strg+V Text aus der Zwischenablage einfügt.
    ///
    /// Verifies that Ctrl+V pastes text from the clipboard.
    /// </summary>
    [TestMethod]
    public void TInputLine_HandleEvent_CtrlV_PastesFromClipboard()
    {
        ManagedClipboard.Clear();
        ManagedClipboard.SetText("World");
        TInputLine inputLine = new(new TRect(0, 0, 10, 1), 20);

        inputLine.HandleEvent(ControlEventFactory.CreateCtrlV());

        Assert.AreEqual("World", inputLine.Data);
    }

    /// <summary>
    /// Prüft die optionale Validatorbindung und das unveränderte Verhalten ohne
    /// Validator.
    ///
    /// Verifies optional validator attachment and unchanged behavior without a
    /// validator.
    /// </summary>
    [TestMethod]
    public void TInputLine_F011_ValidatorIsOptionalAndNoValidatorRemainsCompatible()
    {
        TInputLine inputLine = new(new TRect(0, 0, 8, 1), 8);

        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'A'));

        Assert.AreEqual("A", inputLine.Data);
        Assert.IsNull(inputLine.Validator);
        Assert.IsTrue(inputLine.Validate(TValidationPhase.Acceptance).IsValid);
    }

    /// <summary>
    /// Prüft permissive Bereichs-Zwischeneingabe, strikte Syntaxprüfung und die
    /// getrennten finalen Phasen.
    ///
    /// Verifies permissive range intermediate input, strict syntax checking, and
    /// distinct final phases.
    /// </summary>
    [TestMethod]
    public void TInputLine_F011_EditFocusAndAcceptanceUseDistinctValidatorPhases()
    {
        TInputLine rangeInput = new(new TRect(0, 0, 8, 1), 8)
        {
            Validator = new TRangeValidator(10, 20)
        };

        rangeInput.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: '1'));

        Assert.AreEqual("1", rangeInput.Data);
        Assert.IsTrue(rangeInput.LastValidationResult.IsValid);
        Assert.AreEqual(TValidationPhase.Edit, rangeInput.LastValidationResult.Phase);
        Assert.IsFalse(rangeInput.Validate(TValidationPhase.FocusLoss).IsValid);

        rangeInput.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: '0'));
        Assert.IsTrue(rangeInput.Validate(TValidationPhase.Acceptance).IsValid);

        TInputLine syntaxInput = new(new TRect(0, 0, 8, 1), 8)
        {
            Validator = new TFilterValidator("0123456789")
        };
        syntaxInput.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'x'));

        Assert.AreEqual(string.Empty, syntaxInput.Data);
        Assert.IsFalse(syntaxInput.LastValidationResult.IsValid);
        Assert.AreEqual(TValidationPhase.Edit, syntaxInput.LastValidationResult.Phase);
        StringAssert.Contains(syntaxInput.LastValidationResult.Message!, "ungültig");
    }

    /// <summary>
    /// Prüft, dass alle abgelehnten Editvarianten den vollständigen Zustand und
    /// eine nichtleere Auswahl erhalten.
    ///
    /// Verifies that every rejected edit variant preserves complete state and a
    /// non-empty selection.
    /// </summary>
    [TestMethod]
    public void TInputLine_F011_RejectedEditsPreserveNonEmptySelectionAndState()
    {
        ManagedClipboard.SetText("ZZ");
        TEvent[] edits =
        [
            ControlEventFactory.CreateKeyDown(charCode: 'X'),
            ControlEventFactory.CreateCtrlV(),
            ControlEventFactory.CreateCtrlX(),
            ControlEventFactory.CreateKeyDown(scanCode: 0x53),
            ControlEventFactory.CreateKeyDown(scanCode: 0x0E)
        ];

        foreach (TEvent edit in edits)
        {
            TInputLine input = CreateRejectingInput(selectionStart: 1, selectionEnd: 4);
            AssertRejectedEditPreserves(input, edit, 1, 4);
        }
    }

    /// <summary>
    /// Prüft den gleichen atomaren Vertrag für eine kollabierte Auswahl.
    ///
    /// Verifies the same atomic contract for a collapsed selection.
    /// </summary>
    [TestMethod]
    public void TInputLine_F011_RejectedEditsPreserveCollapsedSelectionAndState()
    {
        TInputLine input = CreateRejectingInput(selectionStart: 3, selectionEnd: 3);

        AssertRejectedEditPreserves(input, ControlEventFactory.CreateKeyDown(scanCode: 0x0E), 3, 3);
    }

    /// <summary>
    /// Prüft Fokus-Veto und bestätigende Dialogvalidierung über die realen
    /// Produktionspfade.
    ///
    /// Verifies focus veto and affirmative dialog validation through real
    /// production paths.
    /// </summary>
    [TestMethod]
    public void TInputLine_F011_FocusAndDialogAcceptancePreserveInvalidInput()
    {
        TDialog dialog = new(new TRect(0, 0, 24, 7), "Input");
        TInputLine input = new(new TRect(1, 1, 8, 2), 8)
        {
            Data = "5",
            Validator = new TRangeValidator(10, 20)
        };
        TButton other = new(new TRect(1, 3, 9, 4), "Other", 0x7200, TButtonFlags.bfNormal);
        dialog.Insert(input);
        dialog.Insert(other);
        dialog.SetFocus(input);

        Assert.AreEqual(TFocusTransitionResult.Rejected, dialog.TrySetFocus(other));
        Assert.AreSame(input, dialog.Current);
        Assert.AreEqual(TValidationPhase.FocusLoss, input.LastValidationResult.Phase);

        TEvent accept = ControlEventFactory.CreateCommand(ShellCommandIds.cmOK);
        dialog.HandleEvent(accept);

        Assert.AreSame(input, dialog.Current);
        Assert.AreEqual("5", input.Data);
        Assert.AreEqual(TValidationPhase.Acceptance, input.LastValidationResult.Phase);
        Assert.AreSame(input, dialog.LastValidationResult.Target);
        Assert.IsFalse(dialog.LastValidationResult.IsValid);
    }

    private static TInputLine CreateRejectingInput(int selectionStart, int selectionEnd)
    {
        TInputLine input = new(new TRect(0, 0, 3, 1), 12)
        {
            Data = "ABCDE",
            Validator = new RejectEditValidator()
        };
        input.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x4F));
        input.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x52));
        input.SetSelection(selectionStart, selectionEnd);
        return input;
    }

    private static void AssertRejectedEditPreserves(TInputLine input, TEvent edit, int selectionStart, int selectionEnd)
    {
        string data = input.Data;
        int cursor = input.CurPos;
        int viewport = input.FirstPos;
        bool insertMode = input.InsertMode;

        input.HandleEvent(edit);

        Assert.AreEqual(data, input.Data);
        Assert.AreEqual(cursor, input.CurPos);
        Assert.AreEqual(viewport, input.FirstPos);
        Assert.AreEqual(insertMode, input.InsertMode);
        Assert.AreEqual(selectionStart, input.SelectionStart);
        Assert.AreEqual(selectionEnd, input.SelectionEnd);
        Assert.IsFalse(input.LastValidationResult.IsValid);
    }

    private sealed class RejectEditValidator : TValidator
    {
        public override bool IsValid(string input) => true;

        public override TValidationResult Validate(string input, TValidationPhase phase, TView target) =>
            phase == TValidationPhase.Edit
                ? TValidationResult.Rejected(phase, "Edit ungültig. / Edit invalid.", target)
                : TValidationResult.Accepted(phase);
    }
}
