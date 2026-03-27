// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Prozesslokale Text-Zwischenablage als managed Ersatz für <c>osclipboard.cc</c>
/// und <c>win32clip.cc</c>.
///
/// Process-local text clipboard as a managed replacement for <c>osclipboard.cc</c>
/// and <c>win32clip.cc</c>.
/// </summary>
public static class ManagedClipboard
{
    private static readonly AsyncLocal<string?> ClipboardText = new();

    /// <summary>
    /// Speichert Text in der Zwischenablage.
    ///
    /// Stores text in the clipboard.
    /// </summary>
    /// <param name="text">Der Textinhalt. / The text content.</param>
    public static void SetText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        ClipboardText.Value = text;
    }

    /// <summary>
    /// Liest den aktuellen Zwischenablageninhalt.
    ///
    /// Reads the current clipboard content.
    /// </summary>
    /// <returns>Der Textinhalt oder <c>null</c>. / The text content or <c>null</c>.</returns>
    public static string? GetText() => ClipboardText.Value;

    /// <summary>
    /// Leert die Zwischenablage.
    ///
    /// Clears the clipboard.
    /// </summary>
    public static void Clear() => ClipboardText.Value = null;
}
