// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;
using System.IO.Enumeration;
using System.Text;

namespace TuiVision.Examples.Wave6;

/// <summary>
/// Bindet alle Dateizugriffe an eine explizite Lernwurzel.
///
/// Binds all file access to an explicit learning root.
/// </summary>
public sealed class ControlledFileWorkspace : IDisposable
{
    /// <summary>Maximal gelesene Vorschaugröße. / Maximum preview size.</summary>
    public const int PreviewByteLimit = 4096;
    /// <summary>Maximale Textzeilen. / Maximum text lines.</summary>
    public const int PreviewLineLimit = 80;
    /// <summary>Maximale Suchtiefe. / Maximum search depth.</summary>
    public const int SearchDepthLimit = 8;
    /// <summary>Maximal geprüfte Dateien. / Maximum inspected files.</summary>
    public const int SearchFileLimit = 256;
    /// <summary>Maximale Trefferzahl. / Maximum result count.</summary>
    public const int SearchResultLimit = 100;

    private static readonly UTF8Encoding StrictUtf8 = new(false, true);
    private static readonly UTF8Encoding ReplacementUtf8 = new(false, false);
    private readonly bool _ownsRoot;
    private readonly StringComparison _pathComparison;
    private readonly HashSet<Guid> _consumedOperations = [];
    private readonly Dictionary<Guid, Wave6OperationIntent> _preparedOperations = [];

    /// <summary>Initialisiert die kontrollierte Wurzel. / Initializes the controlled root.</summary>
    /// <param name="rootPath">Explizite Wurzel. / Explicit root.</param>
    /// <param name="ownsRoot">Löscht die Wurzel beim Dispose. / Deletes the root on dispose.</param>
    public ControlledFileWorkspace(string rootPath, bool ownsRoot = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(rootPath);
        RootPath = Path.TrimEndingDirectorySeparator(Path.GetFullPath(rootPath));
        if (!Directory.Exists(RootPath))
        {
            throw new DirectoryNotFoundException($"Controlled root does not exist: {RootPath}");
        }

        if (IsReparsePoint(RootPath))
        {
            throw new InvalidOperationException("The controlled root must not be a symbolic link or reparse point.");
        }

        _ownsRoot = ownsRoot;
        _pathComparison = OperatingSystem.IsWindows()
            ? StringComparison.OrdinalIgnoreCase
            : StringComparison.Ordinal;
    }

    /// <summary>Kanonische kontrollierte Wurzel. / Canonical controlled root.</summary>
    public string RootPath { get; }

    /// <summary>Zeigt an, ob der Workspace beendet ist. / Indicates whether the workspace is disposed.</summary>
    public bool IsDisposed { get; private set; }

    /// <summary>Listet ein kontrolliertes Verzeichnis. / Lists a controlled directory.</summary>
    public Wave6DirectorySnapshot List(
        string relativeDirectory = "",
        string filter = "*",
        Wave6Sort sort = Wave6Sort.Name,
        bool descending = false)
    {
        string directory = Resolve(relativeDirectory, requireExisting: true);
        if (!Directory.Exists(directory))
        {
            throw new DirectoryNotFoundException($"Controlled directory does not exist: {relativeDirectory}");
        }

        string acceptedFilter = string.IsNullOrWhiteSpace(filter) ? "*" : filter;
        if (acceptedFilter.Length > 128
            || acceptedFilter.IndexOfAny([Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar]) >= 0)
        {
            throw new ArgumentException("Filter must be one bounded file-name expression.", nameof(filter));
        }

        IEnumerable<Wave6DirectoryEntry> entries = Directory
            .EnumerateFileSystemEntries(directory)
            .Where(path => !IsReparsePoint(path))
            .Select(CreateEntry)
            .Where(entry => entry.Kind == Wave6EntryKind.Directory
                || FileSystemName.MatchesSimpleExpression(acceptedFilter, entry.Name, ignoreCase: OperatingSystem.IsWindows()));

        IOrderedEnumerable<Wave6DirectoryEntry> ordered = entries
            .OrderBy(entry => entry.Kind == Wave6EntryKind.Directory ? 0 : 1)
            .ThenBy(entry => entry, EntryComparer(sort, descending));
        Wave6DirectoryEntry[] materialized = ordered.ToArray();
        string relative = ToRelative(directory);
        string status = materialized.Length == 0
            ? $"empty path={DisplayRelative(relative)} filter={acceptedFilter}"
            : $"listed path={DisplayRelative(relative)} entries={materialized.Length} filter={acceptedFilter}";
        return new Wave6DirectorySnapshot(relative, materialized, acceptedFilter, sort, descending, status);
    }

    /// <summary>Erstellt eine begrenzte Textvorschau. / Creates a bounded text preview.</summary>
    public Wave6PreviewResult PreviewText(string relativePath)
    {
        string path = ResolveFile(relativePath);
        byte[] bytes = ReadBounded(path, out bool byteTruncated);
        bool invalidUtf8 = false;
        string text;
        try
        {
            text = StrictUtf8.GetString(bytes);
        }
        catch (DecoderFallbackException)
        {
            invalidUtf8 = true;
            text = ReplacementUtf8.GetString(bytes);
        }

        string[] lines = NormalizeLineEndings(text).Split('\n');
        bool lineTruncated = lines.Length > PreviewLineLimit;
        string content = string.Join('\n', lines.Take(PreviewLineLimit));
        bool truncated = byteTruncated || lineTruncated;
        string status = $"text path={ToRelative(path)} bytes={bytes.Length}"
            + (invalidUtf8 ? " invalid UTF-8 replaced" : string.Empty)
            + (truncated ? " truncated" : string.Empty);
        return new Wave6PreviewResult(
            ToRelative(path),
            Wave6ViewerDecision.Text,
            content,
            bytes.Length,
            truncated,
            invalidUtf8,
            status);
    }

    /// <summary>Erstellt eine begrenzte Hexvorschau. / Creates a bounded hex preview.</summary>
    public Wave6PreviewResult PreviewHex(string relativePath)
    {
        string path = ResolveFile(relativePath);
        byte[] bytes = ReadBounded(path, out bool truncated);
        StringBuilder content = new();
        for (int offset = 0; offset < bytes.Length; offset += 16)
        {
            ReadOnlySpan<byte> row = bytes.AsSpan(offset, Math.Min(16, bytes.Length - offset));
            string hex = string.Join(' ', row.ToArray().Select(value => value.ToString("X2", CultureInfo.InvariantCulture)));
            string printable = new(row.ToArray().Select(value => value is >= 32 and <= 126 ? (char)value : '.').ToArray());
            content.Append(offset.ToString("X8", CultureInfo.InvariantCulture))
                .Append("  ")
                .Append(hex.PadRight(47))
                .Append("  ")
                .Append(printable);
            if (offset + row.Length < bytes.Length)
            {
                content.AppendLine();
            }
        }

        string status = $"hex path={ToRelative(path)} bytes={bytes.Length}" + (truncated ? " truncated" : string.Empty);
        return new Wave6PreviewResult(
            ToRelative(path),
            Wave6ViewerDecision.Hex,
            content.ToString(),
            bytes.Length,
            truncated,
            false,
            status);
    }

    /// <summary>Sucht begrenzt unterhalb eines Verzeichnisses. / Searches within bounded limits below a directory.</summary>
    public Wave6SearchResult Search(string relativeDirectory, string pattern, CancellationToken cancellationToken = default)
    {
        string start = Resolve(relativeDirectory, requireExisting: true);
        if (!Directory.Exists(start))
        {
            throw new DirectoryNotFoundException($"Controlled search directory does not exist: {relativeDirectory}");
        }

        string acceptedPattern = string.IsNullOrWhiteSpace(pattern) ? "*" : pattern;
        if (acceptedPattern.Length > 128
            || acceptedPattern.IndexOfAny([Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar]) >= 0)
        {
            throw new ArgumentException("Search pattern must be one bounded file-name expression.", nameof(pattern));
        }

        List<string> matches = [];
        int visited = 0;
        bool canceled = false;
        bool limitReached = false;
        Queue<(string Path, int Depth)> queue = new();
        queue.Enqueue((start, 0));
        while (queue.Count > 0)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                canceled = true;
                break;
            }

            (string directory, int depth) = queue.Dequeue();
            IEnumerable<string> entries = Directory.EnumerateFileSystemEntries(directory).Order(StringComparer.Ordinal);
            foreach (string entry in entries)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    canceled = true;
                    break;
                }

                if (IsReparsePoint(entry))
                {
                    continue;
                }

                if (Directory.Exists(entry))
                {
                    if (depth < SearchDepthLimit)
                    {
                        queue.Enqueue((entry, depth + 1));
                    }
                    else
                    {
                        limitReached = true;
                    }

                    continue;
                }

                visited++;
                if (visited > SearchFileLimit)
                {
                    visited = SearchFileLimit;
                    limitReached = true;
                    queue.Clear();
                    break;
                }

                if (FileSystemName.MatchesSimpleExpression(acceptedPattern, Path.GetFileName(entry), OperatingSystem.IsWindows()))
                {
                    matches.Add(ToRelative(entry));
                    if (matches.Count >= SearchResultLimit)
                    {
                        limitReached = true;
                        queue.Clear();
                        break;
                    }
                }
            }
        }

        matches.Sort(StringComparer.Ordinal);
        string status = canceled
            ? $"search canceled visited={visited} matches={matches.Count}"
            : $"search complete visited={visited} matches={matches.Count}" + (limitReached ? " limit reached" : string.Empty);
        return new Wave6SearchResult(matches, visited, canceled, limitReached, status);
    }

    /// <summary>Wählt einen internen Viewer. / Selects an internal viewer.</summary>
    public Wave6ViewerDecision DecideViewer(string relativePath)
    {
        _ = ResolveFile(relativePath);
        string extension = Path.GetExtension(relativePath);
        return extension.ToLowerInvariant() switch
        {
            ".txt" or ".md" or ".json" or ".xml" or ".csv" or ".log" => Wave6ViewerDecision.Text,
            ".bin" or ".dat" or ".pal" or ".tvr" => Wave6ViewerDecision.Hex,
            _ => Wave6ViewerDecision.Fallback
        };
    }

    /// <summary>Bereitet eine explizite Dateiaktion vor. / Prepares an explicit file action.</summary>
    public Wave6OperationIntent PrepareOperation(
        Wave6OperationKind kind,
        string sourceRelativePath,
        string? targetRelativePath = null)
    {
        string source = ResolveFile(sourceRelativePath);
        string? target = null;
        if (kind is Wave6OperationKind.Copy or Wave6OperationKind.Rename)
        {
            if (string.IsNullOrWhiteSpace(targetRelativePath))
            {
                throw new ArgumentException("Copy and rename require a target.", nameof(targetRelativePath));
            }

            target = Resolve(targetRelativePath, requireExisting: false);
            if (Path.GetFullPath(source).Equals(Path.GetFullPath(target), _pathComparison))
            {
                throw new InvalidOperationException("Source and target must differ.");
            }

            if (File.Exists(target) || Directory.Exists(target))
            {
                throw new IOException("Target already exists; silent overwrite is forbidden.");
            }

            string? parent = Path.GetDirectoryName(target);
            if (string.IsNullOrEmpty(parent) || !Directory.Exists(parent))
            {
                throw new DirectoryNotFoundException("Target parent does not exist.");
            }
        }

        FileInfo info = new(source);
        Wave6OperationIntent intent = new(
            Guid.NewGuid(),
            kind,
            ToRelative(source),
            target is null ? null : ToRelative(target),
            info.Length,
            info.LastWriteTimeUtc,
            Wave6OperationState.AwaitingDecision,
            "explicit decision required");
        _preparedOperations.Add(intent.OperationId, intent);
        return intent;
    }

    /// <summary>Entscheidet und führt eine vorbereitete Aktion aus. / Decides and executes a prepared action.</summary>
    public Wave6OperationResult Execute(Wave6OperationIntent intent, bool confirmed)
    {
        ArgumentNullException.ThrowIfNull(intent);
        ThrowIfDisposed();
        // Die ID allein ist keine Berechtigung: Nur das unveränderte, von diesem
        // Workspace vorbereitete Intent darf genau einmal entschieden werden.
        // The ID alone grants no authority: only the unchanged intent prepared
        // by this workspace may be decided exactly once.
        if (intent.State != Wave6OperationState.AwaitingDecision
            || !_preparedOperations.TryGetValue(intent.OperationId, out Wave6OperationIntent? prepared)
            || prepared != intent
            || !_consumedOperations.Add(intent.OperationId))
        {
            return Result(intent, Wave6OperationState.Rejected, 0, "intent-state", "NoMutation");
        }

        if (!confirmed)
        {
            return Result(intent, Wave6OperationState.Canceled, 0, null, "NoMutation");
        }

        string source;
        string? target;
        try
        {
            source = ResolveFile(intent.SourceRelativePath);
            target = intent.TargetRelativePath is null ? null : Resolve(intent.TargetRelativePath, requireExisting: false);
            FileInfo current = new(source);
            if (current.Length != intent.SourceLength || current.LastWriteTimeUtc != intent.SourceLastWriteUtc)
            {
                return Result(intent, Wave6OperationState.Rejected, 0, "stale-source", "NoMutation");
            }

            if (target is not null && (File.Exists(target) || Directory.Exists(target)))
            {
                return Result(intent, Wave6OperationState.Rejected, 0, "target-conflict", "NoMutation");
            }
        }
        catch (Exception exception) when (IsExpectedFileException(exception))
        {
            return Result(intent, Wave6OperationState.Rejected, 0, "revalidation", "NoMutation");
        }

        try
        {
            switch (intent.Kind)
            {
                case Wave6OperationKind.Copy:
                    File.Copy(source, target!, overwrite: false);
                    break;
                case Wave6OperationKind.Rename:
                    File.Move(source, target!, overwrite: false);
                    break;
                case Wave6OperationKind.Delete:
                    File.Delete(source);
                    break;
                case Wave6OperationKind.SetReadOnly:
                    File.SetAttributes(source, File.GetAttributes(source) | FileAttributes.ReadOnly);
                    break;
                case Wave6OperationKind.ClearReadOnly:
                    File.SetAttributes(source, File.GetAttributes(source) & ~FileAttributes.ReadOnly);
                    break;
                default:
                    return Result(intent, Wave6OperationState.Rejected, 0, "unknown-operation", "NoMutation");
            }

            return Result(intent, Wave6OperationState.Completed, 100, null, "CompletedStateIsAuthoritative");
        }
        catch (Exception exception) when (IsExpectedFileException(exception))
        {
            return Result(intent, Wave6OperationState.Failed, 0, exception.GetType().Name, "InspectSourceAndTarget");
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (IsDisposed)
        {
            return;
        }

        IsDisposed = true;
        if (_ownsRoot && Directory.Exists(RootPath))
        {
            Directory.Delete(RootPath, recursive: true);
        }
    }

    private Wave6DirectoryEntry CreateEntry(string path)
    {
        FileAttributes attributes = File.GetAttributes(path);
        bool isDirectory = attributes.HasFlag(FileAttributes.Directory);
        FileSystemInfo info = isDirectory ? new DirectoryInfo(path) : new FileInfo(path);
        return new Wave6DirectoryEntry(
            ToRelative(path),
            Path.GetFileName(path),
            isDirectory ? Wave6EntryKind.Directory : Wave6EntryKind.File,
            isDirectory ? null : ((FileInfo)info).Length,
            info.LastWriteTimeUtc,
            attributes.HasFlag(FileAttributes.ReadOnly));
    }

    private static IComparer<Wave6DirectoryEntry> EntryComparer(Wave6Sort sort, bool descending)
    {
        IComparer<Wave6DirectoryEntry> comparer = Comparer<Wave6DirectoryEntry>.Create((left, right) =>
        {
            int result = sort switch
            {
                Wave6Sort.Size => Nullable.Compare(left.Size, right.Size),
                Wave6Sort.Modified => left.LastWriteUtc.CompareTo(right.LastWriteUtc),
                _ => StringComparer.Ordinal.Compare(left.Name, right.Name)
            };
            return result != 0 ? result : StringComparer.Ordinal.Compare(left.Name, right.Name);
        });
        return descending
            ? Comparer<Wave6DirectoryEntry>.Create((left, right) => comparer.Compare(right, left))
            : comparer;
    }

    private string ResolveFile(string relativePath)
    {
        string path = Resolve(relativePath, requireExisting: true);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Controlled file does not exist.", relativePath);
        }

        return path;
    }

    private string Resolve(string relativePath, bool requireExisting)
    {
        ThrowIfDisposed();
        relativePath ??= string.Empty;
        if (Path.IsPathRooted(relativePath))
        {
            throw new InvalidOperationException("Only workspace-relative paths are accepted.");
        }

        string fullPath = Path.GetFullPath(Path.Combine(RootPath, relativePath));
        string prefix = RootPath + Path.DirectorySeparatorChar;
        if (!fullPath.Equals(RootPath, _pathComparison) && !fullPath.StartsWith(prefix, _pathComparison))
        {
            throw new InvalidOperationException("Path leaves the controlled root.");
        }

        // Jeder bereits existierende Abschnitt wird geprüft, weil ein sicherer
        // Endpfad trotzdem durch ein verlinktes Zwischenverzeichnis führen kann.
        // Every existing segment is checked because a safe-looking final path
        // can still pass through a linked intermediate directory.
        string relative = Path.GetRelativePath(RootPath, fullPath);
        string current = RootPath;
        if (relative != ".")
        {
            foreach (string segment in relative.Split(
                [Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar],
                StringSplitOptions.RemoveEmptyEntries))
            {
                current = Path.Combine(current, segment);
                if ((File.Exists(current) || Directory.Exists(current)) && IsReparsePoint(current))
                {
                    throw new InvalidOperationException("Symbolic links and reparse points are outside the learning boundary.");
                }
            }
        }

        if (requireExisting && !File.Exists(fullPath) && !Directory.Exists(fullPath))
        {
            throw new FileNotFoundException("Controlled path does not exist.", relativePath);
        }

        return fullPath;
    }

    private string ToRelative(string path)
    {
        string relative = Path.GetRelativePath(RootPath, path);
        return relative == "." ? string.Empty : relative.Replace(Path.DirectorySeparatorChar, '/');
    }

    private static string DisplayRelative(string relative) => string.IsNullOrEmpty(relative) ? "." : relative;

    private static byte[] ReadBounded(string path, out bool truncated)
    {
        byte[] buffer = new byte[PreviewByteLimit + 1];
        using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, FileOptions.SequentialScan);
        int count = 0;
        while (count < buffer.Length)
        {
            int read = stream.Read(buffer, count, buffer.Length - count);
            if (read == 0)
            {
                break;
            }

            count += read;
        }

        truncated = count > PreviewByteLimit;
        return buffer[..Math.Min(count, PreviewByteLimit)];
    }

    private static string NormalizeLineEndings(string value) =>
        value.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');

    private static bool IsReparsePoint(string path)
    {
        try
        {
            return File.GetAttributes(path).HasFlag(FileAttributes.ReparsePoint);
        }
        catch (FileNotFoundException)
        {
            return false;
        }
    }

    private static bool IsExpectedFileException(Exception exception) =>
        exception is IOException
            or UnauthorizedAccessException
            or InvalidOperationException
            or ArgumentException
            or NotSupportedException;

    private static Wave6OperationResult Result(
        Wave6OperationIntent intent,
        Wave6OperationState state,
        int progress,
        string? error,
        string recovery) =>
        new(
            intent.OperationId,
            state,
            new[] { intent.SourceRelativePath }
                .Concat(intent.TargetRelativePath is null ? [] : [intent.TargetRelativePath])
                .ToArray(),
            progress,
            error,
            recovery);

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(IsDisposed, this);
}
