// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Schweregrad einer textorientierten Standarddialog-Validierung.
///
/// Severity of a text-oriented standard-dialog validation.
/// </summary>
public enum StandardDialogValidationSeverity
{
    /// <summary>Hinweis. / Informational message.</summary>
    Info,

    /// <summary>Warnung. / Warning.</summary>
    Warning,

    /// <summary>Fehler. / Error.</summary>
    Error
}

/// <summary>
/// Textorientierte Validierungsmeldung fuer Standarddialoge.
///
/// Text-oriented validation message for standard dialogs.
/// </summary>
/// <param name="Code">Stabiler Meldungscode. / Stable message code.</param>
/// <param name="Text">Lesbarer Meldungstext. / Readable message text.</param>
/// <param name="Severity">Der Schweregrad. / The severity.</param>
public readonly record struct StandardDialogValidationMessage(
    string Code,
    string Text,
    StandardDialogValidationSeverity Severity)
{
    /// <summary>
    /// Erstellt eine Fehlermeldung.
    ///
    /// Creates an error message.
    /// </summary>
    /// <param name="code">Der Meldungscode. / The message code.</param>
    /// <param name="text">Der Meldungstext. / The message text.</param>
    /// <returns>Die Validierungsmeldung. / The validation message.</returns>
    public static StandardDialogValidationMessage Error(string code, string text) =>
        new(code, text, StandardDialogValidationSeverity.Error);

    /// <summary>
    /// Erstellt eine Warnmeldung.
    ///
    /// Creates a warning message.
    /// </summary>
    /// <param name="code">Der Meldungscode. / The message code.</param>
    /// <param name="text">Der Meldungstext. / The message text.</param>
    /// <returns>Die Validierungsmeldung. / The validation message.</returns>
    public static StandardDialogValidationMessage Warning(string code, string text) =>
        new(code, text, StandardDialogValidationSeverity.Warning);

    /// <summary>
    /// Erstellt eine Informationsmeldung.
    ///
    /// Creates an informational message.
    /// </summary>
    /// <param name="code">Der Meldungscode. / The message code.</param>
    /// <param name="text">Der Meldungstext. / The message text.</param>
    /// <returns>Die Validierungsmeldung. / The validation message.</returns>
    public static StandardDialogValidationMessage Info(string code, string text) =>
        new(code, text, StandardDialogValidationSeverity.Info);
}
