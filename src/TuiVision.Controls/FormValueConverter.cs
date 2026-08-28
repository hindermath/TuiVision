// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;

namespace TuiVision.Controls;

/// <summary>
/// Enthält Erfolg oder einen frameworkneutralen Konvertierungsfehler.
///
/// Contains either success or a framework-neutral conversion error.
/// </summary>
/// <typeparam name="T">Der Zieltyp. / The target type.</typeparam>
public sealed class FormConversionResult<T>
{
    private FormConversionResult(bool isSuccess, T? value, FormValidationError? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    /// <summary>Ob die Konvertierung erfolgreich war. / Whether conversion succeeded.</summary>
    public bool IsSuccess { get; }

    /// <summary>Der konvertierte Wert. / The converted value.</summary>
    public T? Value { get; }

    /// <summary>Der optionale Fehler. / The optional error.</summary>
    public FormValidationError? Error { get; }

    /// <summary>Erstellt ein erfolgreiches Ergebnis. / Creates a successful result.</summary>
    /// <param name="value">Der Wert. / The value.</param>
    /// <returns>Das Ergebnis. / The result.</returns>
    public static FormConversionResult<T> Success(T value) => new(true, value, null);

    /// <summary>Erstellt ein Fehlerergebnis. / Creates a failure result.</summary>
    /// <param name="code">Der Fehlercode. / The error code.</param>
    /// <param name="message">Die Meldung. / The message.</param>
    /// <returns>Das Ergebnis. / The result.</returns>
    public static FormConversionResult<T> Failure(string code, string message) =>
        new(false, default, new FormValidationError(code, message));
}

/// <summary>
/// Definiert eine kultur-explizite bidirektionale Konvertierung.
///
/// Defines an explicit-culture bidirectional conversion.
/// </summary>
/// <typeparam name="TField">Der Feldtyp. / The field type.</typeparam>
/// <typeparam name="TModel">Der Modelltyp. / The model type.</typeparam>
public interface IFormValueConverter<TField, TModel>
{
    /// <summary>Konvertiert Modell zu Feld. / Converts model to field.</summary>
    /// <param name="value">Der Modellwert. / The model value.</param>
    /// <param name="culture">Die explizite Kultur. / The explicit culture.</param>
    /// <returns>Das Ergebnis. / The result.</returns>
    FormConversionResult<TField> ConvertToField(TModel value, CultureInfo culture);

    /// <summary>Konvertiert Feld zu Modell. / Converts field to model.</summary>
    /// <param name="value">Der Feldwert. / The field value.</param>
    /// <param name="culture">Die explizite Kultur. / The explicit culture.</param>
    /// <returns>Das Ergebnis. / The result.</returns>
    FormConversionResult<TModel> ConvertToModel(TField value, CultureInfo culture);
}

/// <summary>
/// Erstellt einen Konverter aus zwei typsicheren Funktionen.
///
/// Creates a converter from two type-safe functions.
/// </summary>
/// <typeparam name="TField">Der Feldtyp. / The field type.</typeparam>
/// <typeparam name="TModel">Der Modelltyp. / The model type.</typeparam>
public sealed class FormValueConverter<TField, TModel> : IFormValueConverter<TField, TModel>
{
    private readonly Func<TModel, CultureInfo, FormConversionResult<TField>> _toField;
    private readonly Func<TField, CultureInfo, FormConversionResult<TModel>> _toModel;

    /// <summary>Erstellt einen bidirektionalen Konverter. / Creates a bidirectional converter.</summary>
    /// <param name="toField">Modell-zu-Feld. / Model-to-field.</param>
    /// <param name="toModel">Feld-zu-Modell. / Field-to-model.</param>
    public FormValueConverter(
        Func<TModel, CultureInfo, FormConversionResult<TField>> toField,
        Func<TField, CultureInfo, FormConversionResult<TModel>> toModel)
    {
        _toField = toField ?? throw new ArgumentNullException(nameof(toField));
        _toModel = toModel ?? throw new ArgumentNullException(nameof(toModel));
    }

    /// <inheritdoc />
    public FormConversionResult<TField> ConvertToField(TModel value, CultureInfo culture) =>
        _toField(value, culture ?? throw new ArgumentNullException(nameof(culture)));

    /// <inheritdoc />
    public FormConversionResult<TModel> ConvertToModel(TField value, CultureInfo culture) =>
        _toModel(value, culture ?? throw new ArgumentNullException(nameof(culture)));
}

internal sealed class FormBindingConversionException : InvalidOperationException
{
    public FormBindingConversionException(FormValidationError error) : base(error.Message) => Error = error;

    public FormValidationError Error { get; }
}

internal abstract class FormBinding
{
    public abstract object? CaptureModelValue();

    public abstract void ApplyModelValue(object? fieldValue);

    public abstract void RestoreModelValue(object? value);
}
