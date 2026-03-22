// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// In-Memory-History mit Buckets pro History-ID.
///
/// In-memory history with buckets per history identifier.
/// </summary>
public sealed class THistory
{
    private static readonly Dictionary<string, List<string>> Buckets = new(StringComparer.Ordinal);

    /// <summary>
    /// Initialisiert einen neuen History-Bucket-Zugriff.
    ///
    /// Initializes a new history bucket accessor.
    /// </summary>
    /// <param name="historyId">Die Bucket-ID. / The bucket identifier.</param>
    public THistory(string historyId)
    {
        HistoryId = historyId ?? throw new ArgumentNullException(nameof(historyId));
    }

    /// <summary>
    /// Die zugeordnete Bucket-ID.
    ///
    /// The associated bucket identifier.
    /// </summary>
    public string HistoryId { get; }

    /// <summary>
    /// Fuegt einen History-Eintrag hinzu.
    ///
    /// Adds a history entry.
    /// </summary>
    /// <param name="value">Der zu speichernde Wert. / The value to store.</param>
    public void Add(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        List<string> bucket = GetBucket(HistoryId);
        bucket.RemoveAll(existing => string.Equals(existing, value, StringComparison.Ordinal));
        bucket.Insert(0, value);
    }

    /// <summary>
    /// Liefert die Eintraege der Bucket-ID in Most-Recent-First-Reihenfolge.
    ///
    /// Returns the bucket entries in most-recent-first order.
    /// </summary>
    /// <returns>Die History-Eintraege. / The history entries.</returns>
    public IReadOnlyList<string> Recall() => GetBucket(HistoryId).AsReadOnly();

    /// <summary>
    /// Loescht alle Buckets. Nur fuer Tests und kontrollierte Resets gedacht.
    ///
    /// Clears all buckets. Intended only for tests and controlled resets.
    /// </summary>
    public static void ClearAll() => Buckets.Clear();

    private static List<string> GetBucket(string historyId)
    {
        if (!Buckets.TryGetValue(historyId, out List<string>? bucket))
        {
            bucket = [];
            Buckets.Add(historyId, bucket);
        }

        return bucket;
    }
}
