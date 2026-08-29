// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Definiert die explizite Synchronisation zwischen einem gewöhnlichen Control
/// und einem Formularfeld.
///
/// Defines explicit synchronization between an ordinary control and a form field.
/// </summary>
public interface IFormControlAdapter
{
    /// <summary>Das gebundene Feld. / The bound field.</summary>
    IFormField Field { get; }

    /// <summary>Überträgt den Control-Wert in das Feld. / Pulls the control value into the field.</summary>
    void PullFromControl();

    /// <summary>Überträgt den Feldwert in das Control. / Pushes the field value into the control.</summary>
    void PushToControl();
}

/// <summary>
/// Bindet eine vorhandene <see cref="TInputLine"/> opt-in an ein String-Feld.
///
/// Binds an existing <see cref="TInputLine"/> to a string field on an opt-in basis.
/// </summary>
public sealed class FormInputLineAdapter : IFormControlAdapter
{
    private readonly TInputLine _inputLine;
    private readonly FormField<string> _field;

    /// <summary>Erstellt den Adapter und zeigt den aktuellen Feldwert. / Creates the adapter and displays the current field value.</summary>
    /// <param name="inputLine">Das gewöhnliche Eingabefeld. / The ordinary input control.</param>
    /// <param name="field">Das Formularfeld. / The form field.</param>
    public FormInputLineAdapter(TInputLine inputLine, FormField<string> field)
    {
        _inputLine = inputLine ?? throw new ArgumentNullException(nameof(inputLine));
        _field = field ?? throw new ArgumentNullException(nameof(field));
        PushToControl();
    }

    /// <inheritdoc />
    public IFormField Field => _field;

    /// <inheritdoc />
    public void PullFromControl() => _field.Value = _inputLine.Data;

    /// <inheritdoc />
    public void PushToControl() => _inputLine.Data = _field.Value;
}
