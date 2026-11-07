// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;
using System.Text;

namespace TuiVision.Controls;

internal static class TFileSystemDisplayText
{
    internal static string Escape(string value)
    {
        StringBuilder result = new(value.Length);
        foreach (Rune rune in value.EnumerateRunes())
        {
            UnicodeCategory category = Rune.GetUnicodeCategory(rune);
            if (rune.Value == '\\')
            {
                result.Append("\\\\");
            }
            else if (category is UnicodeCategory.Format
                     or UnicodeCategory.Control
                     or UnicodeCategory.LineSeparator
                     or UnicodeCategory.ParagraphSeparator)
            {
                result.Append(rune.IsBmp ? "\\u" : "\\U")
                    .Append(rune.Value.ToString(rune.IsBmp ? "X4" : "X8", CultureInfo.InvariantCulture));
            }
            else
            {
                result.Append(rune.ToString());
            }
        }

        return result.ToString();
    }
}
