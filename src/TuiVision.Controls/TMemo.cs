// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Speicherbasierter Editor ohne Dateibindung.
///
/// Memory-backed editor without file attachment.
/// </summary>
public sealed class TMemo : TEditor
{
    /// <summary>
    /// Initialisiert ein neues Memo.
    ///
    /// Initializes a new memo.
    /// </summary>
    /// <param name="bounds">Die Bounds des Memos. / The memo bounds.</param>
    public TMemo(TRect bounds) : base(bounds)
    {
    }
}
