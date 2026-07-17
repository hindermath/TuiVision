// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Examples.Wave6;

string sourceRoot = Path.Combine(AppContext.BaseDirectory, "Fixtures");
string workspaceRoot = Path.Combine(Path.GetTempPath(), $"tuivision-tvfm-{Guid.NewGuid():N}");
CopyFixture(sourceRoot, workspaceRoot);
using ControlledFileWorkspace workspace = new(workspaceRoot, ownsRoot: true);

bool headless = args.Contains("--smoke", StringComparer.Ordinal);
int width = TryReadConsoleDimension(() => Console.WindowWidth, 80);
int height = TryReadConsoleDimension(() => Console.WindowHeight, 25);
Tp7FileManagerApp app = new(workspace, new TRect(0, 0, width, height), headless);
if (headless)
{
    app.QueueEvents(
    [
        TEvent.CreateCommand(Tp7FileManagerApp.CmNavigateFirstDirectory),
        TEvent.CreateCommand(Tp7FileManagerApp.CmPreviewText)
    ]);
}

app.Run();

static int TryReadConsoleDimension(Func<int> read, int fallback)
{
    try
    {
        int value = read();
        return value > 0 ? value : fallback;
    }
    catch
    {
        return fallback;
    }
}

static void CopyFixture(string source, string target)
{
    Directory.CreateDirectory(target);
    foreach (string directory in Directory.EnumerateDirectories(source, "*", SearchOption.AllDirectories))
    {
        Directory.CreateDirectory(Path.Combine(target, Path.GetRelativePath(source, directory)));
    }

    foreach (string file in Directory.EnumerateFiles(source, "*", SearchOption.AllDirectories))
    {
        string destination = Path.Combine(target, Path.GetRelativePath(source, file));
        Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
        File.Copy(file, destination);
    }
}
