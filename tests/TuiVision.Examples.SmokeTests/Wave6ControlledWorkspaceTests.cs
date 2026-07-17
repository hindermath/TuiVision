// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;
using TuiVision.Examples.Wave6;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft die kontrollierte Dateisystemgrenze von Wave 6.
///
/// Verifies the controlled Wave-6 filesystem boundary.
/// </summary>
[TestClass]
public sealed class Wave6ControlledWorkspaceTests
{
    /// <summary>Prüft stabile Wurzel- und Unterverzeichnislisten. / Verifies stable root and child directory listings.</summary>
    [TestMethod]
    public void Wave6Workspace_Lists_And_Navigates_Only_Below_Root()
    {
        using WorkspaceFixture fixture = new();
        using ControlledFileWorkspace workspace = new(fixture.Root);

        Wave6DirectorySnapshot root = workspace.List();
        Wave6DirectorySnapshot child = workspace.List("docs");

        CollectionAssert.AreEqual(new[] { "data", "docs", "README.txt" }, root.Entries.Select(entry => entry.Name).ToArray());
        CollectionAssert.AreEqual(new[] { "docs/lesson.txt" }, child.Entries.Select(entry => entry.RelativePath).ToArray());
        Assert.ThrowsExactly<InvalidOperationException>(() => workspace.List("../"));
        Assert.ThrowsExactly<InvalidOperationException>(() => workspace.List(Path.GetPathRoot(fixture.Root)!));
    }

    /// <summary>Prüft begrenzte Textvorschau und ungültiges UTF-8. / Verifies bounded text preview and invalid UTF-8.</summary>
    [TestMethod]
    public void Wave6Workspace_TextPreview_Is_Bounded_And_Explicit()
    {
        using WorkspaceFixture fixture = new();
        File.WriteAllText(Path.Combine(fixture.Root, "long.txt"), string.Join('\n', Enumerable.Repeat("line", 100)), Encoding.UTF8);
        File.WriteAllBytes(Path.Combine(fixture.Root, "invalid.txt"), [0x66, 0x80, 0x6f]);
        using ControlledFileWorkspace workspace = new(fixture.Root);

        Wave6PreviewResult limited = workspace.PreviewText("long.txt");
        Wave6PreviewResult invalid = workspace.PreviewText("invalid.txt");

        Assert.IsTrue(limited.Truncated);
        Assert.HasCount(ControlledFileWorkspace.PreviewLineLimit, limited.Content.Split('\n'));
        Assert.IsTrue(invalid.InvalidUtf8);
        StringAssert.Contains(invalid.Status, "invalid UTF-8");
    }

    /// <summary>Prüft Lebenszyklus und fehlende Wurzel. / Verifies lifecycle and missing root.</summary>
    [TestMethod]
    public void Wave6Workspace_Rejects_Missing_And_Disposed_Root()
    {
        string missing = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Assert.ThrowsExactly<DirectoryNotFoundException>(() => new ControlledFileWorkspace(missing));

        using WorkspaceFixture fixture = new();
        ControlledFileWorkspace workspace = new(fixture.Root);
        workspace.Dispose();

        Assert.IsTrue(workspace.IsDisposed);
        Assert.ThrowsExactly<ObjectDisposedException>(() => workspace.List());
    }

    /// <summary>Prüft, dass ein verlinktes Segment keine Wurzelauthorität erbt. / Verifies that a linked segment does not inherit root authority.</summary>
    [TestMethod]
    public void Wave6Workspace_Rejects_Linked_Path_Segments()
    {
        using WorkspaceFixture fixture = new();
        string outside = Path.Combine(Path.GetTempPath(), $"tuivision-wave6-outside-{Guid.NewGuid():N}");
        Directory.CreateDirectory(outside);
        File.WriteAllText(Path.Combine(outside, "secret.txt"), "outside");
        string link = Path.Combine(fixture.Root, "linked");
        try
        {
            try
            {
                Directory.CreateSymbolicLink(link, outside);
            }
            catch (Exception exception) when (exception is UnauthorizedAccessException or IOException or PlatformNotSupportedException)
            {
                Assert.Inconclusive($"Symbolic-link creation is unavailable: {exception.GetType().Name}");
            }

            using ControlledFileWorkspace workspace = new(fixture.Root);
            Assert.ThrowsExactly<InvalidOperationException>(() => workspace.List("linked"));
            Assert.ThrowsExactly<InvalidOperationException>(() => workspace.PreviewText("linked/secret.txt"));
        }
        finally
        {
            if (Directory.Exists(link) || File.Exists(link))
            {
                Directory.Delete(link);
            }

            Directory.Delete(outside, recursive: true);
        }
    }

    /// <summary>Prüft Filter, Sortierung und Metadaten stabil. / Verifies stable filtering, sorting and metadata.</summary>
    [TestMethod]
    public void Wave6Workspace_Filters_Sorts_And_Reports_Metadata()
    {
        using WorkspaceFixture fixture = new();
        File.WriteAllText(Path.Combine(fixture.Root, "small.txt"), "1");
        File.WriteAllText(Path.Combine(fixture.Root, "large.txt"), "123456");
        using ControlledFileWorkspace workspace = new(fixture.Root);

        Wave6DirectorySnapshot filtered = workspace.List(filter: "*.txt", sort: Wave6Sort.Size);
        Wave6DirectorySnapshot empty = workspace.List(filter: "*.missing");

        CollectionAssert.AreEqual(
            new[] { "data", "docs", "small.txt", "README.txt", "large.txt" },
            filtered.Entries.Select(entry => entry.Name).ToArray());
        Assert.HasCount(2, empty.Entries);
        Assert.AreEqual(1, filtered.Entries.Single(entry => entry.Name == "small.txt").Size);
        StringAssert.Contains(empty.Status, "entries=2");
    }

    /// <summary>Prüft Hex-Grenze, Offset und druckbare Zeichen. / Verifies hex bounds, offsets and printable characters.</summary>
    [TestMethod]
    public void Wave6Workspace_HexPreview_Is_Bounded_And_TextFirst()
    {
        using WorkspaceFixture fixture = new();
        File.WriteAllBytes(
            Path.Combine(fixture.Root, "large.bin"),
            Enumerable.Range(0, ControlledFileWorkspace.PreviewByteLimit + 5).Select(value => (byte)value).ToArray());
        using ControlledFileWorkspace workspace = new(fixture.Root);

        Wave6PreviewResult preview = workspace.PreviewHex("large.bin");

        Assert.AreEqual(Wave6ViewerDecision.Hex, preview.Decision);
        Assert.AreEqual(ControlledFileWorkspace.PreviewByteLimit, preview.BytesRead);
        Assert.IsTrue(preview.Truncated);
        StringAssert.StartsWith(preview.Content, "00000000");
        StringAssert.Contains(preview.Content, "0123456789");
    }

    /// <summary>Prüft begrenzte Suche und geschlossene Viewerwahl. / Verifies bounded search and closed viewer selection.</summary>
    [TestMethod]
    public void Wave6Workspace_Searches_And_Selects_Only_Internal_Viewers()
    {
        using WorkspaceFixture fixture = new();
        using ControlledFileWorkspace workspace = new(fixture.Root);

        Wave6SearchResult result = workspace.Search("", "*.txt");
        using CancellationTokenSource canceled = new();
        canceled.Cancel();
        Wave6SearchResult partial = workspace.Search("", "*", canceled.Token);

        CollectionAssert.AreEqual(new[] { "README.txt", "docs/lesson.txt" }, result.Matches.ToArray());
        Assert.IsFalse(result.Canceled);
        Assert.IsTrue(partial.Canceled);
        Assert.AreEqual(Wave6ViewerDecision.Text, workspace.DecideViewer("README.txt"));
        Assert.AreEqual(Wave6ViewerDecision.Hex, workspace.DecideViewer("data/sample.bin"));
        File.WriteAllText(Path.Combine(fixture.Root, "unknown.zzz"), "unknown");
        Assert.AreEqual(Wave6ViewerDecision.Fallback, workspace.DecideViewer("unknown.zzz"));
    }

    /// <summary>Prüft Suchgrenzen für Tiefe, Dateien und Treffer. / Verifies search limits for depth, files and results.</summary>
    [TestMethod]
    public void Wave6Workspace_Search_Enforces_Depth_File_And_Result_Limits()
    {
        using WorkspaceFixture depthFixture = new();
        string current = depthFixture.Root;
        for (int depth = 1; depth <= ControlledFileWorkspace.SearchDepthLimit + 1; depth++)
        {
            current = Path.Combine(current, $"d{depth}");
            Directory.CreateDirectory(current);
            File.WriteAllText(Path.Combine(current, $"depth-{depth}.hit"), "depth");
        }

        using ControlledFileWorkspace depthWorkspace = new(depthFixture.Root);
        Wave6SearchResult depthResult = depthWorkspace.Search("", "depth-*.hit");

        using WorkspaceFixture fileFixture = new();
        for (int index = 0; index < ControlledFileWorkspace.SearchFileLimit + 10; index++)
        {
            File.WriteAllText(Path.Combine(fileFixture.Root, $"visited-{index:D3}.nomatch"), "visited");
        }

        using ControlledFileWorkspace fileWorkspace = new(fileFixture.Root);
        Wave6SearchResult files = fileWorkspace.Search("", "*.absent");

        using WorkspaceFixture resultFixture = new();
        for (int index = 0; index < ControlledFileWorkspace.SearchResultLimit + 10; index++)
        {
            File.WriteAllText(Path.Combine(resultFixture.Root, $"result-{index:D3}.hit"), "result");
        }

        using ControlledFileWorkspace resultWorkspace = new(resultFixture.Root);
        Wave6SearchResult results = resultWorkspace.Search("", "result-*.hit");

        CollectionAssert.Contains(depthResult.Matches.ToArray(), "d1/d2/d3/d4/d5/d6/d7/d8/depth-8.hit");
        CollectionAssert.DoesNotContain(depthResult.Matches.ToArray(), "d1/d2/d3/d4/d5/d6/d7/d8/d9/depth-9.hit");
        Assert.IsTrue(depthResult.LimitReached);
        Assert.AreEqual(ControlledFileWorkspace.SearchFileLimit, files.VisitedFiles);
        Assert.IsTrue(files.LimitReached);
        Assert.HasCount(ControlledFileWorkspace.SearchResultLimit, results.Matches);
        Assert.IsTrue(results.LimitReached);
    }

    private sealed class WorkspaceFixture : IDisposable
    {
        public WorkspaceFixture()
        {
            Root = Path.Combine(Path.GetTempPath(), $"tuivision-wave6-test-{Guid.NewGuid():N}");
            Directory.CreateDirectory(Path.Combine(Root, "docs"));
            Directory.CreateDirectory(Path.Combine(Root, "data"));
            File.WriteAllText(Path.Combine(Root, "README.txt"), "root");
            File.WriteAllText(Path.Combine(Root, "docs", "lesson.txt"), "lesson");
            File.WriteAllBytes(Path.Combine(Root, "data", "sample.bin"), [0, 1, 2, 3]);
        }

        public string Root { get; }

        public void Dispose() => Directory.Delete(Root, recursive: true);
    }
}
