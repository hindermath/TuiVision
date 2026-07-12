namespace TuiVision.Controls;

/// <summary>
/// Ordnet semantische UI-Rollen konkreten Konsolenfarben zu.
///
/// Maps semantic UI roles to concrete console colours.
/// </summary>
public sealed record TColorScheme
{
    /// <summary>
    /// Erstellt ein unveränderliches Farbschema.
    ///
    /// Creates an immutable colour scheme.
    /// </summary>
    /// <param name="name">Der stabile Schemaname. / The stable scheme name.</param>
    /// <param name="background">Der normale Hintergrund. / The normal background.</param>
    /// <param name="text">Der normale Text. / The normal text.</param>
    /// <param name="emphasis">Hervorgehobener Text. / Emphasised text.</param>
    /// <param name="selectionBackground">Der Auswahlhintergrund. / The selection background.</param>
    /// <param name="selectionText">Der Auswahltext. / The selection text.</param>
    /// <param name="popupSelectionBackground">Der Popup-Auswahlhintergrund. / The popup selection background.</param>
    /// <param name="statusBackground">Der Statushintergrund. / The status background.</param>
    /// <param name="statusText">Der Statustext. / The status text.</param>
    /// <exception cref="ArgumentException">
    /// Wird bei einem leeren Namen ausgelöst. / Thrown for a blank name.
    /// </exception>
    public TColorScheme(
        string name,
        ConsoleColor background,
        ConsoleColor text,
        ConsoleColor emphasis,
        ConsoleColor selectionBackground,
        ConsoleColor selectionText,
        ConsoleColor popupSelectionBackground,
        ConsoleColor statusBackground,
        ConsoleColor statusText)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("A colour scheme requires a name.", nameof(name));
        }

        Name = name;
        Background = background;
        Text = text;
        Emphasis = emphasis;
        SelectionBackground = selectionBackground;
        SelectionText = selectionText;
        PopupSelectionBackground = popupSelectionBackground;
        StatusBackground = statusBackground;
        StatusText = statusText;
    }

    /// <summary>Das bestehende Standardschema. / The existing default scheme.</summary>
    public static TColorScheme Default { get; } = new(
        "Default", ConsoleColor.Cyan, ConsoleColor.Black, ConsoleColor.Yellow,
        ConsoleColor.Blue, ConsoleColor.White, ConsoleColor.DarkBlue, ConsoleColor.Cyan, ConsoleColor.Yellow);

    /// <summary>Das kontrastreiche, explizit aktivierte Schema. / The explicitly activated high-contrast scheme.</summary>
    public static TColorScheme HighContrast { get; } = new(
        "HighContrast", ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Yellow,
        ConsoleColor.White, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Black);

    /// <summary>Der stabile Schemaname. / The stable scheme name.</summary>
    public string Name { get; }

    /// <summary>Der normale Hintergrund. / The normal background.</summary>
    public ConsoleColor Background { get; }

    /// <summary>Der normale Text. / The normal text.</summary>
    public ConsoleColor Text { get; }

    /// <summary>Hervorgehobener Text. / Emphasised text.</summary>
    public ConsoleColor Emphasis { get; }

    /// <summary>Der Auswahlhintergrund. / The selection background.</summary>
    public ConsoleColor SelectionBackground { get; }

    /// <summary>Der Auswahltext. / The selection text.</summary>
    public ConsoleColor SelectionText { get; }

    /// <summary>Der Auswahlhintergrund in Popup-Menüs. / The selection background in popup menus.</summary>
    public ConsoleColor PopupSelectionBackground { get; }

    /// <summary>Der Statushintergrund. / The status background.</summary>
    public ConsoleColor StatusBackground { get; }

    /// <summary>Der Statustext. / The status text.</summary>
    public ConsoleColor StatusText { get; }
}
