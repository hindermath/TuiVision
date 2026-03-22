// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Eingabezeile fuer Dateipfade mit History-Bucket-Anbindung.
///
/// Input line for file paths with history bucket integration.
/// </summary>
public sealed class TFileInputLine : TInputLine
{
    private readonly THistory _history;

    /// <summary>
    /// Initialisiert eine neue Dateipfad-Eingabezeile.
    ///
    /// Initializes a new file-path input line.
    /// </summary>
    /// <param name="bounds">Die Bounds des Controls. / The control bounds.</param>
    /// <param name="maxLen">Die maximale Laenge. / The maximum length.</param>
    /// <param name="historyId">Die History-ID. / The history identifier.</param>
    public TFileInputLine(TRect bounds, int maxLen, string historyId) : base(bounds, maxLen)
    {
        _history = new THistory(historyId);
    }

    /// <summary>
    /// Die History-ID des Controls.
    ///
    /// The control's history identifier.
    /// </summary>
    public string HistoryId => _history.HistoryId;

    /// <summary>
    /// Uebernimmt den aktuellen Text in die History.
    ///
    /// Commits the current text into history.
    /// </summary>
    public void CommitToHistory() => _history.Add(Data);

    /// <summary>
    /// Holt einen History-Wert anhand seines Bucketslots.
    ///
    /// Recalls a history value by bucket slot.
    /// </summary>
    /// <param name="index">Der Slot-Index. / The slot index.</param>
    /// <returns>Der gefundene Wert oder <c>null</c>. / The found value or <c>null</c>.</returns>
    public string? RecallAt(int index = 0)
    {
        IReadOnlyList<string> values = _history.Recall();
        if (index < 0 || index >= values.Count)
        {
            return null;
        }

        Data = values[index];
        return Data;
    }
}
