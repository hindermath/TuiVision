// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text.RegularExpressions;
using TuiVision.Serialization;

namespace TuiVision.Controls;

/// <summary>
/// Löst ausschließlich vertrauenswürdig registrierte symbolische Formschlüssel auf.
///
/// Resolves only trusted, explicitly registered symbolic form keys.
/// </summary>
public sealed partial class FormRuntimeRegistry
{
    private readonly Dictionary<string, Type> _types = new(StringComparer.Ordinal);
    private readonly Dictionary<string, RuntimeEntry> _controls = new(StringComparer.Ordinal);
    private readonly Dictionary<string, RuntimeEntry> _bindings = new(StringComparer.Ordinal);
    private readonly Dictionary<string, RuntimeEntry> _converters = new(StringComparer.Ordinal);
    private readonly Dictionary<string, RuntimeEntry> _validators = new(StringComparer.Ordinal);

    /// <summary>Registriert einen erlaubten symbolischen Feldtyp. / Registers an allowed symbolic field type.</summary>
    /// <param name="key">Der sichere Schlüssel. / The safe key.</param>
    /// <param name="runtimeType">Der vertrauenswürdige Runtime-Typ. / The trusted runtime type.</param>
    /// <param name="replace">Ob ein vorhandener Eintrag ersetzt werden darf. / Whether an existing entry may be replaced.</param>
    public void RegisterType(string key, Type runtimeType, bool replace = false)
    {
        ArgumentNullException.ThrowIfNull(runtimeType);
        Register(_types, key, runtimeType, replace, "type");
    }

    /// <summary>Registriert eine Control-Fabrik oder Beschreibung. / Registers a control factory or descriptor.</summary>
    /// <param name="key">Der sichere Schlüssel. / The safe key.</param>
    /// <param name="typeKey">Der zugehörige Feldtyp. / The associated field type.</param>
    /// <param name="runtimeValue">Der vertrauenswürdige Runtime-Wert. / The trusted runtime value.</param>
    /// <param name="replace">Ob ein vorhandener Eintrag ersetzt werden darf. / Whether an existing entry may be replaced.</param>
    public void RegisterControl(string key, string typeKey, object runtimeValue, bool replace = false) =>
        RegisterRuntime(_controls, key, typeKey, runtimeValue, replace, "control");

    /// <summary>Registriert ein typsicheres Binding. / Registers a type-safe binding.</summary>
    /// <param name="key">Der sichere Schlüssel. / The safe key.</param>
    /// <param name="typeKey">Der zugehörige Feldtyp. / The associated field type.</param>
    /// <param name="runtimeValue">Der vertrauenswürdige Runtime-Wert. / The trusted runtime value.</param>
    /// <param name="replace">Ob ein vorhandener Eintrag ersetzt werden darf. / Whether an existing entry may be replaced.</param>
    public void RegisterBinding(string key, string typeKey, object runtimeValue, bool replace = false) =>
        RegisterRuntime(_bindings, key, typeKey, runtimeValue, replace, "binding");

    /// <summary>Registriert einen kultur-expliziten Konverter. / Registers an explicit-culture converter.</summary>
    /// <param name="key">Der sichere Schlüssel. / The safe key.</param>
    /// <param name="typeKey">Der zugehörige Feldtyp. / The associated field type.</param>
    /// <param name="runtimeValue">Der vertrauenswürdige Runtime-Wert. / The trusted runtime value.</param>
    /// <param name="replace">Ob ein vorhandener Eintrag ersetzt werden darf. / Whether an existing entry may be replaced.</param>
    public void RegisterConverter(string key, string typeKey, object runtimeValue, bool replace = false) =>
        RegisterRuntime(_converters, key, typeKey, runtimeValue, replace, "converter");

    /// <summary>Registriert einen Validator. / Registers a validator.</summary>
    /// <param name="key">Der sichere Schlüssel. / The safe key.</param>
    /// <param name="typeKey">Der zugehörige Feldtyp. / The associated field type.</param>
    /// <param name="runtimeValue">Der vertrauenswürdige Runtime-Wert. / The trusted runtime value.</param>
    /// <param name="replace">Ob ein vorhandener Eintrag ersetzt werden darf. / Whether an existing entry may be replaced.</param>
    public void RegisterValidator(string key, string typeKey, object runtimeValue, bool replace = false) =>
        RegisterRuntime(_validators, key, typeKey, runtimeValue, replace, "validator");

    /// <summary>
    /// Prüft alle symbolischen Verweise vollständig, bevor ein Runtime-Modell freigegeben wird.
    ///
    /// Validates every symbolic reference before publishing a runtime model.
    /// </summary>
    /// <param name="document">Das bereits strukturell gelesene Dokument. / The structurally parsed document.</param>
    /// <returns>Das erst nach Gesamtprüfung veröffentlichte Runtime-Modell. / The runtime model published only after complete validation.</returns>
    /// <exception cref="InvalidDataException">Ein Schlüssel fehlt oder besitzt einen Typkonflikt. / A key is missing or has a type conflict.</exception>
    public ResolvedFormSemanticDocument Resolve(TFormSemanticDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);

        // Der Serializer erzwingt zuerst den geschlossenen, azyklischen Strukturvertrag.
        // Serialization first enforces the closed, acyclic structural contract.
        _ = TFormSemanticJson.Serialize(document);
        List<string> errors = [];
        foreach (TFormSemanticDefinition form in document.Forms)
        {
            foreach (TFormSemanticField field in form.Fields)
            {
                if (!_types.ContainsKey(field.Type))
                {
                    errors.Add($"Unknown type key '{field.Type}' for field '{form.Key}.{field.Key}'.");
                }

                ValidateEntry(_controls, field.Control, field.Type, "control", form.Key, field.Key, errors);
                ValidateEntry(_bindings, field.Binding, field.Type, "binding", form.Key, field.Key, errors);
                ValidateEntry(_converters, field.Converter, field.Type, "converter", form.Key, field.Key, errors);
                foreach (string validator in field.Validators)
                {
                    ValidateEntry(_validators, validator, field.Type, "validator", form.Key, field.Key, errors);
                }
            }
        }

        if (errors.Count != 0)
        {
            throw new InvalidDataException(string.Join(" ", errors));
        }

        List<ResolvedFormSemanticDefinition> resolvedForms = document.Forms
            .Select(form => new ResolvedFormSemanticDefinition(
                form.Key,
                form.Fields.Select(field => new ResolvedFormSemanticField(
                    field.Key,
                    _types[field.Type],
                    _controls[field.Control].Value,
                    _bindings[field.Binding].Value,
                    _converters[field.Converter].Value,
                    field.Validators.Select(key => _validators[key].Value))),
                form.Children))
            .ToList();
        return new ResolvedFormSemanticDocument(document, resolvedForms);
    }

    private void RegisterRuntime(
        IDictionary<string, RuntimeEntry> target,
        string key,
        string typeKey,
        object runtimeValue,
        bool replace,
        string category)
    {
        ArgumentNullException.ThrowIfNull(runtimeValue);
        ValidateKey(typeKey, nameof(typeKey));
        if (!_types.ContainsKey(typeKey))
        {
            throw new InvalidOperationException($"Register type key '{typeKey}' before registering a {category}.");
        }

        Register(target, key, new RuntimeEntry(typeKey, runtimeValue), replace, category);
    }

    private static void ValidateEntry(
        IReadOnlyDictionary<string, RuntimeEntry> entries,
        string key,
        string expectedType,
        string category,
        string form,
        string field,
        ICollection<string> errors)
    {
        if (!entries.TryGetValue(key, out RuntimeEntry? entry))
        {
            errors.Add($"Unknown {category} key '{key}' for field '{form}.{field}'.");
        }
        else if (!string.Equals(entry.TypeKey, expectedType, StringComparison.Ordinal))
        {
            errors.Add(
                $"{category} key '{key}' uses type '{entry.TypeKey}', but field '{form}.{field}' requires '{expectedType}'.");
        }
    }

    private static void Register<TValue>(
        IDictionary<string, TValue> target,
        string key,
        TValue value,
        bool replace,
        string category)
    {
        ValidateKey(key, nameof(key));
        if (!replace && target.ContainsKey(key))
        {
            throw new InvalidOperationException($"Duplicate {category} key '{key}'.");
        }

        target[key] = value;
    }

    private static void ValidateKey(string key, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(key) || !SafeKeyPattern().IsMatch(key))
        {
            throw new ArgumentException(
                "Registry keys must match ^[a-z][a-z0-9-]{0,63}$.",
                parameterName);
        }
    }

    [GeneratedRegex("^[a-z][a-z0-9-]{0,63}$", RegexOptions.CultureInvariant)]
    private static partial Regex SafeKeyPattern();

    private sealed record RuntimeEntry(string TypeKey, object Value);
}

/// <summary>
/// Enthält ein vollständig und atomar aufgelöstes Formsemantik-Dokument.
///
/// Contains a completely and atomically resolved form-semantics document.
/// </summary>
public sealed class ResolvedFormSemanticDocument
{
    /// <summary>Erstellt das bereits geprüfte Runtime-Modell. / Creates the already validated runtime model.</summary>
    /// <param name="source">Das sichere Quelldokument. / The safe source document.</param>
    /// <param name="forms">Die aufgelösten Formen. / The resolved forms.</param>
    internal ResolvedFormSemanticDocument(
        TFormSemanticDocument source,
        IEnumerable<ResolvedFormSemanticDefinition> forms)
    {
        Source = source;
        Forms = forms.ToArray();
        RootForm = Forms.Single(form => string.Equals(form.Key, source.RootForm, StringComparison.Ordinal));
    }

    /// <summary>Das validierte symbolische Quelldokument. / The validated symbolic source document.</summary>
    public TFormSemanticDocument Source { get; }

    /// <summary>Die Root-Form. / The root form.</summary>
    public ResolvedFormSemanticDefinition RootForm { get; }

    /// <summary>Alle aufgelösten Formen in stabiler Reihenfolge. / All resolved forms in stable order.</summary>
    public IReadOnlyList<ResolvedFormSemanticDefinition> Forms { get; }
}

/// <summary>Enthält aufgelöste Felder und sichere Child-Referenzen. / Contains resolved fields and safe child references.</summary>
public sealed class ResolvedFormSemanticDefinition
{
    /// <summary>Erstellt eine aufgelöste Form. / Creates a resolved form.</summary>
    /// <param name="key">Der Formschlüssel. / The form key.</param>
    /// <param name="fields">Die aufgelösten Felder. / The resolved fields.</param>
    /// <param name="children">Die validierten Child-Referenzen. / The validated child references.</param>
    internal ResolvedFormSemanticDefinition(
        string key,
        IEnumerable<ResolvedFormSemanticField> fields,
        IEnumerable<TFormSemanticChild> children)
    {
        Key = key;
        Fields = fields.ToArray();
        Children = children.ToArray();
    }

    /// <summary>Der Formschlüssel. / The form key.</summary>
    public string Key { get; }

    /// <summary>Die aufgelösten Felder. / The resolved fields.</summary>
    public IReadOnlyList<ResolvedFormSemanticField> Fields { get; }

    /// <summary>Die sicheren Child-Referenzen. / The safe child references.</summary>
    public IReadOnlyList<TFormSemanticChild> Children { get; }
}

/// <summary>Enthält die vertrauenswürdig aufgelösten Bausteine eines Feldes. / Contains the trusted resolved components of one field.</summary>
public sealed class ResolvedFormSemanticField
{
    /// <summary>Erstellt ein aufgelöstes Feld. / Creates a resolved field.</summary>
    /// <param name="key">Der Feldschlüssel. / The field key.</param>
    /// <param name="fieldType">Der erlaubte Runtime-Typ. / The allowed runtime type.</param>
    /// <param name="control">Die Control-Fabrik oder Beschreibung. / The control factory or descriptor.</param>
    /// <param name="binding">Das vertrauenswürdige Binding. / The trusted binding.</param>
    /// <param name="converter">Der vertrauenswürdige Konverter. / The trusted converter.</param>
    /// <param name="validators">Die vertrauenswürdigen Validatoren. / The trusted validators.</param>
    internal ResolvedFormSemanticField(
        string key,
        Type fieldType,
        object control,
        object binding,
        object converter,
        IEnumerable<object> validators)
    {
        Key = key;
        FieldType = fieldType;
        Control = control;
        Binding = binding;
        Converter = converter;
        Validators = validators.ToArray();
    }

    /// <summary>Der Feldschlüssel. / The field key.</summary>
    public string Key { get; }

    /// <summary>Der erlaubte Runtime-Typ. / The allowed runtime type.</summary>
    public Type FieldType { get; }

    /// <summary>Die Control-Fabrik oder Beschreibung. / The control factory or descriptor.</summary>
    public object Control { get; }

    /// <summary>Das vertrauenswürdige Binding. / The trusted binding.</summary>
    public object Binding { get; }

    /// <summary>Der vertrauenswürdige Konverter. / The trusted converter.</summary>
    public object Converter { get; }

    /// <summary>Die vertrauenswürdigen Validatoren. / The trusted validators.</summary>
    public IReadOnlyList<object> Validators { get; }
}
