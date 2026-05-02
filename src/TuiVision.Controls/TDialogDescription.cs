// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Unterstuetzte Rollen fuer design-time Dialog-Controls.
///
/// Supported roles for design-time dialog controls.
/// </summary>
public static class DialogControlRoles
{
    /// <summary>Statischer Text. / Static text.</summary>
    public const string StaticText = "static-text";

    /// <summary>Eingabezeile. / Input line.</summary>
    public const string InputLine = "input-line";

    /// <summary>Schaltflaeche. / Button.</summary>
    public const string Button = "button";

    /// <summary>Listenfeld. / List box.</summary>
    public const string ListBox = "list-box";

    /// <summary>Kontrollkaestchen. / Check box.</summary>
    public const string CheckBox = "check-box";
}

/// <summary>
/// Design-time Beschreibung eines Controls in einem Dialog.
///
/// Design-time description of one control in a dialog.
/// </summary>
public sealed record DialogControlDescription
{
    /// <summary>
    /// Erstellt eine Control-Beschreibung.
    ///
    /// Creates a control description.
    /// </summary>
    /// <param name="controlId">Die Control-ID. / The control identifier.</param>
    /// <param name="role">Die Control-Rolle. / The control role.</param>
    /// <param name="label">Die Beschriftung. / The label.</param>
    /// <param name="initialValue">Der Anfangswert. / The initial value.</param>
    /// <param name="canFocus">Ob das Control fokussierbar ist. / Whether the control can receive focus.</param>
    public DialogControlDescription(string controlId, string role, string label, string? initialValue = null, bool canFocus = true)
    {
        ControlId = controlId ?? string.Empty;
        Role = role ?? string.Empty;
        Label = label ?? string.Empty;
        InitialValue = initialValue;
        CanFocus = canFocus;
    }

    /// <summary>Die Control-ID. / The control identifier.</summary>
    public string ControlId { get; }

    /// <summary>Die Control-Rolle. / The control role.</summary>
    public string Role { get; }

    /// <summary>Die Beschriftung. / The label.</summary>
    public string Label { get; }

    /// <summary>Der Anfangswert. / The initial value.</summary>
    public string? InitialValue { get; }

    /// <summary>Ob das Control fokussierbar ist. / Whether the control can receive focus.</summary>
    public bool CanFocus { get; }
}

/// <summary>
/// Design-time Befehlsbindung fuer einen Dialog.
///
/// Design-time command binding for a dialog.
/// </summary>
public sealed record DialogCommandBinding
{
    /// <summary>
    /// Erstellt eine Befehlsbindung.
    ///
    /// Creates a command binding.
    /// </summary>
    /// <param name="commandId">Die Command-ID. / The command identifier.</param>
    /// <param name="targetControlId">Die optionale Ziel-Control-ID. / The optional target control identifier.</param>
    /// <param name="meaning">Die Bedeutung. / The meaning.</param>
    /// <param name="keyboardTrigger">Der Tastaturausloeser. / The keyboard trigger.</param>
    public DialogCommandBinding(ushort commandId, string? targetControlId, string meaning, string keyboardTrigger)
    {
        CommandId = commandId;
        TargetControlId = targetControlId;
        Meaning = meaning ?? string.Empty;
        KeyboardTrigger = keyboardTrigger ?? string.Empty;
    }

    /// <summary>Die Command-ID. / The command identifier.</summary>
    public ushort CommandId { get; }

    /// <summary>Die optionale Ziel-Control-ID. / The optional target control identifier.</summary>
    public string? TargetControlId { get; }

    /// <summary>Die Bedeutung. / The meaning.</summary>
    public string Meaning { get; }

    /// <summary>Der Tastaturausloeser. / The keyboard trigger.</summary>
    public string KeyboardTrigger { get; }
}

/// <summary>
/// Design-time Dialogbeschreibung fuer den Dialog-Designer.
///
/// Design-time dialog description for the dialog designer.
/// </summary>
public sealed record DialogDescription
{
    /// <summary>
    /// Erstellt eine Dialogbeschreibung.
    ///
    /// Creates a dialog description.
    /// </summary>
    /// <param name="descriptionId">Die Beschreibungs-ID. / The description identifier.</param>
    /// <param name="version">Die Modellversion. / The model version.</param>
    /// <param name="title">Der Dialogtitel. / The dialog title.</param>
    /// <param name="controls">Die Controls. / The controls.</param>
    /// <param name="navigationOrder">Die Navigationsreihenfolge. / The navigation order.</param>
    /// <param name="commandBindings">Die Befehlsbindungen. / The command bindings.</param>
    public DialogDescription(
        string descriptionId,
        int version,
        string title,
        IEnumerable<DialogControlDescription> controls,
        IEnumerable<string> navigationOrder,
        IEnumerable<DialogCommandBinding> commandBindings)
    {
        DescriptionId = descriptionId ?? string.Empty;
        Version = version;
        Title = title ?? string.Empty;
        Controls = controls?.ToArray() ?? [];
        NavigationOrder = navigationOrder?.ToArray() ?? [];
        CommandBindings = commandBindings?.ToArray() ?? [];
    }

    /// <summary>Die Beschreibungs-ID. / The description identifier.</summary>
    public string DescriptionId { get; }

    /// <summary>Die Modellversion. / The model version.</summary>
    public int Version { get; }

    /// <summary>Der Dialogtitel. / The dialog title.</summary>
    public string Title { get; }

    /// <summary>Die Control-Beschreibungen. / The control descriptions.</summary>
    public IReadOnlyList<DialogControlDescription> Controls { get; }

    /// <summary>Die Navigationsreihenfolge. / The navigation order.</summary>
    public IReadOnlyList<string> NavigationOrder { get; }

    /// <summary>Die Befehlsbindungen. / The command bindings.</summary>
    public IReadOnlyList<DialogCommandBinding> CommandBindings { get; }
}
