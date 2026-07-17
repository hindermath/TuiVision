// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.Wave6;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft explizite Wave-6-Dateioperationen und ihre Recovery-Grenzen.
///
/// Verifies explicit Wave-6 file operations and their recovery boundaries.
/// </summary>
[TestClass]
public sealed class Wave6FileOperationTests
{
    /// <summary>Prüft, dass Abbruch und fremde Intents nichts schreiben. / Verifies cancel and foreign intents write nothing.</summary>
    [TestMethod]
    public void Wave6Operation_Cancel_And_Foreign_Intent_Do_Not_Mutate()
    {
        using OperationFixture fixture = new();
        using ControlledFileWorkspace workspace = new(fixture.Root);
        byte[] before = File.ReadAllBytes(fixture.Source);
        Wave6OperationIntent intent = workspace.PrepareOperation(Wave6OperationKind.Copy, "source.txt", "copy.txt");

        Wave6OperationResult canceled = workspace.Execute(intent, confirmed: false);
        Wave6OperationIntent foreign = intent with { OperationId = Guid.NewGuid() };
        Wave6OperationResult rejected = workspace.Execute(foreign, confirmed: true);

        Assert.AreEqual(Wave6OperationState.Canceled, canceled.State);
        Assert.AreEqual(Wave6OperationState.Rejected, rejected.State);
        CollectionAssert.AreEqual(before, File.ReadAllBytes(fixture.Source));
        Assert.IsFalse(File.Exists(Path.Combine(fixture.Root, "copy.txt")));
    }

    /// <summary>Prüft Copy, Rename und Einmal-Autorisierung. / Verifies copy, rename and one-shot authorization.</summary>
    [TestMethod]
    public void Wave6Operation_Copy_And_Rename_Are_Confirmed_And_OneShot()
    {
        using OperationFixture fixture = new();
        using ControlledFileWorkspace workspace = new(fixture.Root);
        Wave6OperationIntent copy = workspace.PrepareOperation(Wave6OperationKind.Copy, "source.txt", "copy.txt");

        Wave6OperationResult copied = workspace.Execute(copy, confirmed: true);
        Wave6OperationResult replay = workspace.Execute(copy, confirmed: true);
        Wave6OperationIntent rename = workspace.PrepareOperation(Wave6OperationKind.Rename, "copy.txt", "renamed.txt");
        Wave6OperationResult renamed = workspace.Execute(rename, confirmed: true);

        Assert.AreEqual(Wave6OperationState.Completed, copied.State);
        Assert.AreEqual(100, copied.ProgressPercent);
        Assert.AreEqual(Wave6OperationState.Rejected, replay.State);
        Assert.AreEqual(Wave6OperationState.Completed, renamed.State);
        Assert.IsFalse(File.Exists(Path.Combine(fixture.Root, "copy.txt")));
        Assert.IsTrue(File.Exists(Path.Combine(fixture.Root, "renamed.txt")));
    }

    /// <summary>Prüft Konflikt-, Gleichheits- und Traversalgrenzen. / Verifies conflict, equality and traversal boundaries.</summary>
    [TestMethod]
    public void Wave6Operation_Rejects_Unsafe_Targets()
    {
        using OperationFixture fixture = new();
        File.WriteAllText(Path.Combine(fixture.Root, "exists.txt"), "exists");
        using ControlledFileWorkspace workspace = new(fixture.Root);

        Assert.ThrowsExactly<InvalidOperationException>(() =>
            workspace.PrepareOperation(Wave6OperationKind.Copy, "source.txt", "source.txt"));
        Assert.ThrowsExactly<IOException>(() =>
            workspace.PrepareOperation(Wave6OperationKind.Copy, "source.txt", "exists.txt"));
        Assert.ThrowsExactly<InvalidOperationException>(() =>
            workspace.PrepareOperation(Wave6OperationKind.Copy, "source.txt", "../outside.txt"));
    }

    /// <summary>Prüft Revalidierung nach Quelländerung und Entfernung. / Verifies revalidation after source change and removal.</summary>
    [TestMethod]
    public void Wave6Operation_Rejects_Stale_Or_Removed_Source()
    {
        using OperationFixture fixture = new();
        using ControlledFileWorkspace workspace = new(fixture.Root);
        Wave6OperationIntent stale = workspace.PrepareOperation(Wave6OperationKind.Copy, "source.txt", "stale.txt");
        File.AppendAllText(fixture.Source, "-changed");

        Wave6OperationResult staleResult = workspace.Execute(stale, confirmed: true);
        Wave6OperationIntent removed = workspace.PrepareOperation(Wave6OperationKind.Delete, "source.txt");
        File.Delete(fixture.Source);
        Wave6OperationResult removedResult = workspace.Execute(removed, confirmed: true);

        Assert.AreEqual(Wave6OperationState.Rejected, staleResult.State);
        Assert.AreEqual("stale-source", staleResult.ErrorCode);
        Assert.AreEqual(Wave6OperationState.Rejected, removedResult.State);
        Assert.AreEqual("revalidation", removedResult.ErrorCode);
    }

    /// <summary>Prüft Target-Revalidierung nach Link-Austausch. / Verifies target revalidation after link replacement.</summary>
    [TestMethod]
    public void Wave6Operation_Revalidates_Target_Link_Before_Mutation()
    {
        using OperationFixture fixture = new();
        string staging = Path.Combine(fixture.Root, "staging");
        Directory.CreateDirectory(staging);
        string outside = Path.Combine(Path.GetTempPath(), $"tuivision-wave6-target-{Guid.NewGuid():N}");
        Directory.CreateDirectory(outside);
        using ControlledFileWorkspace workspace = new(fixture.Root);
        Wave6OperationIntent intent = workspace.PrepareOperation(Wave6OperationKind.Copy, "source.txt", "staging/copy.txt");

        try
        {
            Directory.Delete(staging);
            try
            {
                Directory.CreateSymbolicLink(staging, outside);
            }
            catch (Exception exception) when (exception is UnauthorizedAccessException or IOException or PlatformNotSupportedException)
            {
                Assert.Inconclusive($"Symbolic-link creation is unavailable: {exception.GetType().Name}");
            }

            Wave6OperationResult result = workspace.Execute(intent, confirmed: true);

            Assert.AreEqual(Wave6OperationState.Rejected, result.State);
            Assert.AreEqual("revalidation", result.ErrorCode);
            Assert.IsFalse(File.Exists(Path.Combine(outside, "copy.txt")));
        }
        finally
        {
            if (Directory.Exists(staging) || File.Exists(staging))
            {
                Directory.Delete(staging);
            }

            Directory.Delete(outside, recursive: true);
        }
    }

    /// <summary>Prüft unbekannte Operationen als Recovery-Grenze. / Verifies unknown operations as a recovery boundary.</summary>
    [TestMethod]
    public void Wave6Operation_Rejects_Unknown_Kind_Without_Mutation()
    {
        using OperationFixture fixture = new();
        using ControlledFileWorkspace workspace = new(fixture.Root);
        Wave6OperationIntent intent = workspace.PrepareOperation((Wave6OperationKind)999, "source.txt");

        Wave6OperationResult result = workspace.Execute(intent, confirmed: true);

        Assert.AreEqual(Wave6OperationState.Rejected, result.State);
        Assert.AreEqual("unknown-operation", result.ErrorCode);
        Assert.AreEqual("NoMutation", result.RecoveryBoundary);
        Assert.IsTrue(File.Exists(fixture.Source));
    }

    /// <summary>Prüft Löschen und portablen Schreibschutz. / Verifies deletion and portable read-only state.</summary>
    [TestMethod]
    public void Wave6Operation_Deletes_And_Toggles_ReadOnly()
    {
        using OperationFixture fixture = new();
        using ControlledFileWorkspace workspace = new(fixture.Root);
        Wave6OperationIntent set = workspace.PrepareOperation(Wave6OperationKind.SetReadOnly, "source.txt");
        Assert.AreEqual(Wave6OperationState.Completed, workspace.Execute(set, confirmed: true).State);
        Assert.IsTrue(File.GetAttributes(fixture.Source).HasFlag(FileAttributes.ReadOnly));

        Wave6OperationIntent clear = workspace.PrepareOperation(Wave6OperationKind.ClearReadOnly, "source.txt");
        Assert.AreEqual(Wave6OperationState.Completed, workspace.Execute(clear, confirmed: true).State);
        Assert.IsFalse(File.GetAttributes(fixture.Source).HasFlag(FileAttributes.ReadOnly));

        Wave6OperationIntent delete = workspace.PrepareOperation(Wave6OperationKind.Delete, "source.txt");
        Assert.AreEqual(Wave6OperationState.Completed, workspace.Execute(delete, confirmed: true).State);
        Assert.IsFalse(File.Exists(fixture.Source));
    }

    private sealed class OperationFixture : IDisposable
    {
        public OperationFixture()
        {
            Root = Path.Combine(Path.GetTempPath(), $"tuivision-wave6-operation-{Guid.NewGuid():N}");
            Directory.CreateDirectory(Root);
            Source = Path.Combine(Root, "source.txt");
            File.WriteAllText(Source, "source");
        }

        public string Root { get; }
        public string Source { get; }

        public void Dispose()
        {
            if (File.Exists(Source))
            {
                File.SetAttributes(Source, FileAttributes.Normal);
            }

            Directory.Delete(Root, recursive: true);
        }
    }
}
