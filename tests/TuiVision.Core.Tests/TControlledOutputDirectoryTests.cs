// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Core.Tests;

/// <summary>
/// Prüft die private Ausgabe-Capability. / Verifies the private output capability.
/// </summary>
[TestClass]
public sealed class TControlledOutputDirectoryTests
{
    /// <summary>
    /// Lehnt Traversal und verlinkte Zwischenverzeichnisse ab.
    ///
    /// Rejects traversal and linked intermediate directories.
    /// </summary>
    [TestMethod]
    public void Test_ResolveNewFile_Rejects_Traversal_And_Linked_Parent()
    {
        using TControlledOutputDirectory output = TControlledOutputDirectory.Create();
        Assert.ThrowsExactly<InvalidOperationException>(() => output.ResolveNewFile("../escape.txt"));

        string outside = Directory.CreateTempSubdirectory("tuivision-outside-").FullName;
        try
        {
            string link = Path.Combine(output.RootPath, "linked");
            try
            {
                Directory.CreateSymbolicLink(link, outside);
            }
            catch (Exception exception) when (exception is UnauthorizedAccessException or IOException or PlatformNotSupportedException)
            {
                Assert.Inconclusive($"Symbolic links are unavailable: {exception.GetType().Name}");
                return;
            }

            Assert.ThrowsExactly<InvalidOperationException>(() => output.ResolveNewFile("linked/escape.txt"));
            Assert.IsFalse(File.Exists(Path.Combine(outside, "escape.txt")));
        }
        finally
        {
            Directory.Delete(outside, recursive: true);
        }
    }

    /// <summary>
    /// Lehnt ein verlinktes Blatt ab und entfernt nur die eigene Wurzel.
    ///
    /// Rejects a linked leaf and removes only its owned root.
    /// </summary>
    [TestMethod]
    public void Test_ResolveNewFile_Rejects_Linked_Leaf_And_Dispose_Is_Owned()
    {
        string outsideDirectory = Directory.CreateTempSubdirectory("tuivision-dangling-").FullName;
        string outside = Path.Combine(outsideDirectory, "not-created.txt");
        string root;
        using (TControlledOutputDirectory output = TControlledOutputDirectory.Create())
        {
            root = output.RootPath;
            string link = Path.Combine(root, "leaf.txt");
            try
            {
                File.CreateSymbolicLink(link, outside);
            }
            catch (Exception exception) when (exception is UnauthorizedAccessException or IOException or PlatformNotSupportedException)
            {
                Assert.Inconclusive($"Symbolic links are unavailable: {exception.GetType().Name}");
                return;
            }

            Assert.ThrowsExactly<InvalidOperationException>(() => output.ResolveNewFile("leaf.txt"));
        }

        Assert.IsFalse(Directory.Exists(root));
        Assert.IsFalse(File.Exists(outside));
        Directory.Delete(outsideDirectory);
    }
}
