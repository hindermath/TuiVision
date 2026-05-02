// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls.Tests;

internal static class StandardDialogTestSupport
{
    public static void AssertKeyboardReachable(StandardDialogFlowState state)
    {
        Assert.IsTrue(state.KeyboardReachable);
    }

    public static void AssertHasValidationMessage(StandardDialogFlowState state, string code)
    {
        Assert.IsTrue(state.ValidationMessages.Any(message => string.Equals(message.Code, code, StringComparison.Ordinal)));
    }

    public static DialogDescription CreateMinimalDescription() => new(
        "minimal",
        1,
        "Minimal",
        [
            new DialogControlDescription("name", DialogControlRoles.InputLine, "Name", "Ada"),
            new DialogControlDescription("ok", DialogControlRoles.Button, "OK")
        ],
        ["name", "ok"],
        [new DialogCommandBinding(ShellCommandIds.cmOK, "ok", "confirm", "Enter")]);
}
