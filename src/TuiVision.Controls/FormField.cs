// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace TuiVision.Controls;

/// <summary>
/// Implementiert ein typsicheres Feld mit Baseline, Dirty-State und Validatoren.
///
/// Implements a type-safe field with baseline, dirty state, and validators.
/// </summary>
/// <typeparam name="T">Der Feldtyp. / The field type.</typeparam>
public sealed class FormField<T> : IFormField<T>, IFormFieldRuntime
{
    private readonly IEqualityComparer<T> _comparer;
    private readonly List<Func<T, FormValidationError?>> _validators = [];
    private readonly List<Func<T, CancellationToken, ValueTask<FormValidationError?>>> _asyncValidators = [];
    private readonly FormBinding? _binding;
    private T _value;
    private T _originalValue;
    private FormValidationError[] _validationErrors = [];
    private long _revision;

    /// <summary>Erstellt ein ungebundenes Formularfeld. / Creates an unbound form field.</summary>
    /// <param name="name">Der sessionslokale Name. / The session-local name.</param>
    /// <param name="value">Der Anfangs- und Baseline-Wert. / The initial and baseline value.</param>
    /// <param name="comparer">Die optionale Gleichheitsregel. / The optional equality rule.</param>
    public FormField(string name, T value, IEqualityComparer<T>? comparer = null)
        : this(name, value, comparer, null)
    {
    }

    private FormField(string name, T value, IEqualityComparer<T>? comparer, FormBinding? binding)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Contains('.'))
        {
            throw new ArgumentException("Field name must be non-empty and must not contain '.'.", nameof(name));
        }

        Name = name;
        _value = value;
        _originalValue = value;
        _comparer = comparer ?? EqualityComparer<T>.Default;
        _binding = binding;
    }

    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public T Value
    {
        get => _value;
        set
        {
            if (_comparer.Equals(_value, value))
            {
                return;
            }

            _value = value;
            _revision++;
        }
    }

    /// <inheritdoc />
    public T OriginalValue => _originalValue;

    /// <inheritdoc />
    public object? UntypedValue => Value;

    /// <inheritdoc />
    public object? UntypedOriginalValue => OriginalValue;

    /// <inheritdoc />
    public bool IsModified => !_comparer.Equals(_originalValue, _value);

    /// <inheritdoc />
    public bool IsValid => _validationErrors.Length == 0;

    /// <inheritdoc />
    public IReadOnlyList<FormValidationError> ValidationErrors => _validationErrors;

    long IFormFieldRuntime.Revision => _revision;

    bool IFormFieldRuntime.HasBinding => _binding is not null;

    FormSession? IFormFieldRuntime.Owner { get; set; }

    /// <summary>
    /// Erstellt ein Feld aus einer direkten, beschreibbaren POCO-Property.
    ///
    /// Creates a field from a direct writable POCO property.
    /// </summary>
    /// <typeparam name="TModel">Der POCO-Typ. / The POCO type.</typeparam>
    /// <param name="name">Der Feldname. / The field name.</param>
    /// <param name="model">Die Modellinstanz. / The model instance.</param>
    /// <param name="property">Der direkte Property-Ausdruck. / The direct property expression.</param>
    /// <param name="comparer">Die optionale Gleichheitsregel. / The optional equality rule.</param>
    /// <returns>Das gebundene Feld. / The bound field.</returns>
    public static FormField<T> FromProperty<TModel>(
        string name,
        TModel model,
        Expression<Func<TModel, T>> property,
        IEqualityComparer<T>? comparer = null)
        where TModel : class
    {
        DirectFormBinding<TModel, T> binding = new(model, property);
        return new FormField<T>(name, binding.InitialValue, comparer, binding);
    }

    /// <summary>
    /// Erstellt ein Feld aus einer Property und einem kultur-expliziten Konverter.
    ///
    /// Creates a field from a property and an explicit-culture converter.
    /// </summary>
    /// <typeparam name="TModel">Der POCO-Typ. / The POCO type.</typeparam>
    /// <typeparam name="TProperty">Der Property-Typ. / The property type.</typeparam>
    /// <param name="name">Der Feldname. / The field name.</param>
    /// <param name="model">Die Modellinstanz. / The model instance.</param>
    /// <param name="property">Der direkte Property-Ausdruck. / The direct property expression.</param>
    /// <param name="converter">Der bidirektionale Konverter. / The bidirectional converter.</param>
    /// <param name="culture">Die explizite Kultur. / The explicit culture.</param>
    /// <param name="comparer">Die optionale Feldgleichheit. / The optional field equality.</param>
    /// <returns>Das gebundene Feld. / The bound field.</returns>
    public static FormField<T> FromProperty<TModel, TProperty>(
        string name,
        TModel model,
        Expression<Func<TModel, TProperty>> property,
        IFormValueConverter<T, TProperty> converter,
        CultureInfo culture,
        IEqualityComparer<T>? comparer = null)
        where TModel : class
    {
        ConvertedFormBinding<TModel, T, TProperty> binding = new(model, property, converter, culture);
        return new FormField<T>(name, binding.InitialValue, comparer, binding);
    }

    /// <summary>Fügt einen synchronen Validator hinzu. / Adds a synchronous validator.</summary>
    /// <param name="validator">Der Validator. / The validator.</param>
    /// <returns>Dieses Feld für fluente Konfiguration. / This field for fluent configuration.</returns>
    public FormField<T> AddValidator(Func<T, FormValidationError?> validator)
    {
        _validators.Add(validator ?? throw new ArgumentNullException(nameof(validator)));
        return this;
    }

    /// <summary>Fügt einen submit-time Async-Validator hinzu. / Adds a submit-time async validator.</summary>
    /// <param name="validator">Der Validator. / The validator.</param>
    /// <returns>Dieses Feld für fluente Konfiguration. / This field for fluent configuration.</returns>
    public FormField<T> AddAsyncValidator(
        Func<T, CancellationToken, ValueTask<FormValidationError?>> validator)
    {
        _asyncValidators.Add(validator ?? throw new ArgumentNullException(nameof(validator)));
        return this;
    }

    /// <inheritdoc />
    public void AcceptChanges()
    {
        EnsureStandaloneMutation();
        if (_binding is null)
        {
            _originalValue = _value;
            return;
        }

        object? captured = _binding.CaptureModelValue();
        try
        {
            _binding.ApplyModelValue(_value);
        }
        catch (Exception exception)
        {
            List<Exception> rollbackErrors = [];
            try
            {
                _binding.RestoreModelValue(captured);
            }
            catch (Exception rollbackException)
            {
                rollbackErrors.Add(rollbackException);
            }

            throw new FormBindingCommitException(
                Name,
                exception,
                rollbackErrors,
                exception is FormBindingConversionException conversion ? conversion.Error : null);
        }

        _originalValue = _value;
    }

    /// <inheritdoc />
    public void RejectChanges()
    {
        EnsureStandaloneMutation();
        ((IFormFieldRuntime)this).RejectBaseline();
    }

    FormFieldSnapshot IFormFieldRuntime.Capture(string path) =>
        new(this, path, _originalValue, _value, _revision);

    bool IFormFieldRuntime.IsSnapshotModified(FormFieldSnapshot snapshot) =>
        !_comparer.Equals((T)snapshot.OriginalValue!, (T)snapshot.CurrentValue!);

    async ValueTask<IReadOnlyList<FormValidationError>> IFormFieldRuntime.ValidateSnapshotAsync(
        FormFieldSnapshot snapshot,
        CancellationToken cancellationToken)
    {
        T value = (T)snapshot.CurrentValue!;
        List<FormValidationError> errors = [];
        foreach (Func<T, FormValidationError?> validator in _validators)
        {
            FormValidationError? error = validator(value);
            if (error is not null)
            {
                errors.Add(error with { FieldName = snapshot.Path });
            }
        }

        foreach (Func<T, CancellationToken, ValueTask<FormValidationError?>> validator in _asyncValidators)
        {
            cancellationToken.ThrowIfCancellationRequested();
            FormValidationError? error = await validator(value, cancellationToken).ConfigureAwait(false);
            if (error is not null)
            {
                errors.Add(error with { FieldName = snapshot.Path });
            }
        }

        return errors;
    }

    void IFormFieldRuntime.PublishErrors(IReadOnlyList<FormValidationError> errors) =>
        _validationErrors = errors.ToArray();

    object? IFormFieldRuntime.CaptureModelValue() => _binding?.CaptureModelValue();

    void IFormFieldRuntime.ApplyModelValue() => _binding?.ApplyModelValue(_value);

    void IFormFieldRuntime.RestoreModelValue(object? value) => _binding?.RestoreModelValue(value);

    FormValidationError? IFormFieldRuntime.GetBindingValidationError(Exception exception) =>
        exception is FormBindingConversionException conversion
            ? conversion.Error with { FieldName = Name }
            : null;

    void IFormFieldRuntime.AcceptBaseline() => _originalValue = _value;

    void IFormFieldRuntime.RejectBaseline()
    {
        Value = _originalValue;
        _validationErrors = [];
    }

    private void EnsureStandaloneMutation()
    {
        if (((IFormFieldRuntime)this).Owner is not null)
        {
            throw new InvalidOperationException(
                "Accept or reject the owning root session to preserve the atomic form boundary.");
        }
    }

    private static (Func<TModel, TProperty> Getter, Action<TModel, TProperty> Setter) CompileProperty<TModel, TProperty>(
        Expression<Func<TModel, TProperty>> expression)
        where TModel : class
    {
        ArgumentNullException.ThrowIfNull(expression);
        if (expression.Body is not MemberExpression member
            || member.Expression != expression.Parameters[0]
            || member.Member is not PropertyInfo property
            || property.GetMethod is null
            || property.SetMethod is null
            || property.GetIndexParameters().Length != 0)
        {
            throw new ArgumentException(
                "Binding expression must select one direct readable and writable property.",
                nameof(expression));
        }

        ParameterExpression value = Expression.Parameter(typeof(TProperty), "value");
        Action<TModel, TProperty> setter;
        try
        {
            setter = Expression.Lambda<Action<TModel, TProperty>>(
                Expression.Assign(member, value),
                expression.Parameters[0],
                value).Compile();
        }
        catch (ArgumentException exception)
        {
            throw new ArgumentException("Binding property must support assignment.", nameof(expression), exception);
        }

        return (expression.Compile(), setter);
    }

    private sealed class DirectFormBinding<TModel, TProperty> : FormBinding
        where TModel : class
    {
        private readonly TModel _model;
        private readonly Func<TModel, TProperty> _getter;
        private readonly Action<TModel, TProperty> _setter;

        public DirectFormBinding(TModel model, Expression<Func<TModel, TProperty>> expression)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            (_getter, _setter) = CompileProperty(expression);
            InitialValue = _getter(_model);
        }

        public TProperty InitialValue { get; }

        public override object? CaptureModelValue() => _getter(_model);

        public override void ApplyModelValue(object? fieldValue) => _setter(_model, (TProperty)fieldValue!);

        public override void RestoreModelValue(object? value) => _setter(_model, (TProperty)value!);
    }

    private sealed class ConvertedFormBinding<TModel, TField, TProperty> : FormBinding
        where TModel : class
    {
        private readonly TModel _model;
        private readonly Func<TModel, TProperty> _getter;
        private readonly Action<TModel, TProperty> _setter;
        private readonly IFormValueConverter<TField, TProperty> _converter;
        private readonly CultureInfo _culture;

        public ConvertedFormBinding(
            TModel model,
            Expression<Func<TModel, TProperty>> expression,
            IFormValueConverter<TField, TProperty> converter,
            CultureInfo culture)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            (_getter, _setter) = CompileProperty(expression);
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _culture = culture ?? throw new ArgumentNullException(nameof(culture));
            FormConversionResult<TField> initial = _converter.ConvertToField(_getter(_model), _culture);
            if (!initial.IsSuccess)
            {
                throw new ArgumentException(initial.Error?.Message ?? "Initial binding conversion failed.", nameof(converter));
            }

            InitialValue = initial.Value!;
        }

        public TField InitialValue { get; }

        public override object? CaptureModelValue() => _getter(_model);

        public override void ApplyModelValue(object? fieldValue)
        {
            FormConversionResult<TProperty> result = _converter.ConvertToModel((TField)fieldValue!, _culture);
            if (!result.IsSuccess)
            {
                throw new FormBindingConversionException(
                    result.Error ?? new FormValidationError("conversion", "Binding conversion failed."));
            }

            _setter(_model, result.Value!);
        }

        public override void RestoreModelValue(object? value) => _setter(_model, (TProperty)value!);
    }
}
