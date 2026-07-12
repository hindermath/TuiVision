namespace TuiVision.Core;

/// <summary>
/// Beschreibt einen ausführbaren Tastaturkurzbefehl ohne ihn auszuführen.
///
/// Describes an executable keyboard shortcut without executing it.
/// </summary>
public readonly record struct TAccessibleShortcut
{
    /// <summary>
    /// Erstellt eine validierte, unveränderliche Shortcut-Beschreibung.
    ///
    /// Creates a validated, immutable shortcut description.
    /// </summary>
    /// <param name="keyCode">Der nichtleere Tastencode. / The non-zero key code.</param>
    /// <param name="displayText">Der textbasierte Hinweis. / The text-based hint.</param>
    /// <param name="command">Der ausführbare Befehl. / The executable command.</param>
    /// <param name="source">Die stabile Quelle. / The stable source.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird bei einem leeren Tasten- oder Befehlscode ausgelöst.
    /// Thrown for a zero key or command code.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Wird bei leerem Anzeigetext oder leerer Quelle ausgelöst.
    /// Thrown for blank display text or source.
    /// </exception>
    public TAccessibleShortcut(ushort keyCode, string displayText, ushort command, string source)
    {
        if (keyCode == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(keyCode), "A shortcut requires a non-zero key code.");
        }

        if (string.IsNullOrWhiteSpace(displayText))
        {
            throw new ArgumentException("A shortcut requires display text.", nameof(displayText));
        }

        if (command == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(command), "A shortcut requires a non-zero command.");
        }

        if (string.IsNullOrWhiteSpace(source))
        {
            throw new ArgumentException("A shortcut requires a source.", nameof(source));
        }

        KeyCode = keyCode;
        DisplayText = displayText;
        Command = command;
        Source = source;
    }

    /// <summary>Der Tastencode. / The key code.</summary>
    public ushort KeyCode { get; }

    /// <summary>Der textbasierte Hinweis. / The text-based hint.</summary>
    public string DisplayText { get; }

    /// <summary>Der auszuführende Befehl. / The command to execute.</summary>
    public ushort Command { get; }

    /// <summary>Die stabile Quelle des Shortcuts. / The stable shortcut source.</summary>
    public string Source { get; }
}

/// <summary>
/// Stellt ausführbare Shortcuts als schreibgeschützte Beschreibungen bereit.
///
/// Provides executable shortcuts as read-only descriptions.
/// </summary>
public interface IAccessibleShortcutProvider
{
    /// <summary>
    /// Gibt die aktuell ausführbaren Shortcuts zurück, ohne Aktionen auszulösen.
    ///
    /// Returns the currently executable shortcuts without triggering actions.
    /// </summary>
    /// <returns>Eine schreibgeschützte Momentaufnahme. / A read-only snapshot.</returns>
    IReadOnlyList<TAccessibleShortcut> GetAccessibleShortcuts();
}
