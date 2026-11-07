// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Reflection;
using TuiVision.Examples.Wave6;

namespace TuiVision.Examples.SmokeTests;

internal static class Wave6WorkspaceTestFactory
{
    internal static ControlledFileWorkspace Open(string rootPath)
    {
        ConstructorInfo constructor = typeof(ControlledFileWorkspace).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            [typeof(string), typeof(bool)],
            modifiers: null)
            ?? throw new InvalidOperationException("Wave-6 test constructor was not found.");
        return (ControlledFileWorkspace)constructor.Invoke([rootPath, false]);
    }
}
