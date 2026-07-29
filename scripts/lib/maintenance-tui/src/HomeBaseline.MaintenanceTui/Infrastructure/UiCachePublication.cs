namespace HomeBaseline.MaintenanceTui.Infrastructure;

public static class UiCachePublication
{
    public static bool IsComplete(
        string cacheDirectory,
        string entryAssemblyName,
        string metadataName)
    {
        if (!Directory.Exists(cacheDirectory))
        {
            return false;
        }

        return File.Exists(Path.Combine(cacheDirectory, entryAssemblyName)) &&
               File.Exists(Path.Combine(cacheDirectory, metadataName));
    }

    public static bool IsComplete(
        string cacheDirectory,
        string entryAssemblyName,
        string metadataName,
        string expectedFingerprint,
        string expectedPlatform)
    {
        if (!IsComplete(cacheDirectory, entryAssemblyName, metadataName))
        {
            return false;
        }

        try
        {
            using var document = System.Text.Json.JsonDocument.Parse(
                File.ReadAllText(Path.Combine(cacheDirectory, metadataName)));
            var root = document.RootElement;
            return root.TryGetProperty("schemaVersion", out var schema) &&
                   schema.ValueKind == System.Text.Json.JsonValueKind.Number &&
                   schema.TryGetInt32(out var schemaVersion) &&
                   schemaVersion == 1 &&
                   root.TryGetProperty("fingerprint", out var fingerprint) &&
                   fingerprint.ValueKind == System.Text.Json.JsonValueKind.String &&
                   string.Equals(
                       fingerprint.GetString(),
                       expectedFingerprint,
                       StringComparison.Ordinal) &&
                   root.TryGetProperty("platform", out var platform) &&
                   platform.ValueKind == System.Text.Json.JsonValueKind.String &&
                   string.Equals(
                       platform.GetString(),
                       expectedPlatform,
                       StringComparison.Ordinal);
        }
        catch (Exception exception) when (
            exception is System.Text.Json.JsonException or
            IOException or
            UnauthorizedAccessException)
        {
            return false;
        }
    }

    public static void PublishAtomically(string temporaryDirectory, string finalDirectory)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(temporaryDirectory);
        ArgumentException.ThrowIfNullOrWhiteSpace(finalDirectory);
        if (Directory.Exists(finalDirectory))
        {
            throw new IOException("Final cache directory already exists.");
        }

        Directory.Move(temporaryDirectory, finalDirectory);
    }
}
