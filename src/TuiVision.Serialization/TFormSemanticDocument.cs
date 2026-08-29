// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Beschreibt eine sichere, versionierte Formularsemantik ohne Runtime-Werte.
///
/// Describes safe, versioned form semantics without runtime values.
/// </summary>
public sealed record TFormSemanticDocument
{
    /// <summary>
    /// Erstellt ein semantisches Formulardokument.
    ///
    /// Creates a semantic form document.
    /// </summary>
    /// <param name="version">Die Formatversion. / The format version.</param>
    /// <param name="rootForm">Der Schlüssel des Wurzelformulars. / The root-form key.</param>
    /// <param name="forms">Die Formulardefinitionen. / The form definitions.</param>
    public TFormSemanticDocument(int version, string rootForm, IEnumerable<TFormSemanticDefinition> forms)
    {
        Version = version;
        RootForm = rootForm ?? string.Empty;
        Forms = forms?.ToArray() ?? [];
    }

    /// <summary>Die Formatversion. / The format version.</summary>
    public int Version { get; }

    /// <summary>Der Schlüssel des Wurzelformulars. / The root-form key.</summary>
    public string RootForm { get; }

    /// <summary>Die Formulardefinitionen. / The form definitions.</summary>
    public IReadOnlyList<TFormSemanticDefinition> Forms { get; }
}

/// <summary>
/// Beschreibt Felder und Child-Beziehungen eines Formulars.
///
/// Describes the fields and child relations of one form.
/// </summary>
public sealed record TFormSemanticDefinition
{
    /// <summary>Erstellt eine Formulardefinition. / Creates a form definition.</summary>
    /// <param name="key">Der Formschlüssel. / The form key.</param>
    /// <param name="fields">Die Felder. / The fields.</param>
    /// <param name="children">Die Child-Beziehungen. / The child relations.</param>
    public TFormSemanticDefinition(
        string key,
        IEnumerable<TFormSemanticField> fields,
        IEnumerable<TFormSemanticChild> children)
    {
        Key = key ?? string.Empty;
        Fields = fields?.ToArray() ?? [];
        Children = children?.ToArray() ?? [];
    }

    /// <summary>Der Formschlüssel. / The form key.</summary>
    public string Key { get; }

    /// <summary>Die Felder. / The fields.</summary>
    public IReadOnlyList<TFormSemanticField> Fields { get; }

    /// <summary>Die Child-Beziehungen. / The child relations.</summary>
    public IReadOnlyList<TFormSemanticChild> Children { get; }
}

/// <summary>
/// Beschreibt ausschließlich sichere Registry-Schlüssel eines Feldes.
///
/// Describes only the safe registry keys of one field.
/// </summary>
public sealed record TFormSemanticField
{
    /// <summary>Erstellt eine Felddefinition. / Creates a field definition.</summary>
    /// <param name="key">Der Feldschlüssel. / The field key.</param>
    /// <param name="control">Der Control-Registry-Schlüssel. / The control registry key.</param>
    /// <param name="type">Der sichere Typ-Registry-Schlüssel. / The safe type registry key.</param>
    /// <param name="binding">Der Binding-Registry-Schlüssel. / The binding registry key.</param>
    /// <param name="converter">Der Converter-Registry-Schlüssel. / The converter registry key.</param>
    /// <param name="validators">Die Validator-Registry-Schlüssel. / The validator registry keys.</param>
    public TFormSemanticField(
        string key,
        string control,
        string type,
        string binding,
        string converter,
        IEnumerable<string> validators)
    {
        Key = key ?? string.Empty;
        Control = control ?? string.Empty;
        Type = type ?? string.Empty;
        Binding = binding ?? string.Empty;
        Converter = converter ?? string.Empty;
        Validators = validators?.ToArray() ?? [];
    }

    /// <summary>Der Feldschlüssel. / The field key.</summary>
    public string Key { get; }

    /// <summary>Der Control-Schlüssel. / The control key.</summary>
    public string Control { get; }

    /// <summary>Der sichere Typ-Schlüssel. / The safe type key.</summary>
    public string Type { get; }

    /// <summary>Der Binding-Schlüssel. / The binding key.</summary>
    public string Binding { get; }

    /// <summary>Der Converter-Schlüssel. / The converter key.</summary>
    public string Converter { get; }

    /// <summary>Die Validator-Schlüssel. / The validator keys.</summary>
    public IReadOnlyList<string> Validators { get; }
}

/// <summary>
/// Verknüpft einen Child-Namen mit einer Formulardefinition.
///
/// Connects a child name to a form definition.
/// </summary>
/// <param name="Key">Der Child-Schlüssel. / The child key.</param>
/// <param name="Form">Der referenzierte Formschlüssel. / The referenced form key.</param>
public sealed record TFormSemanticChild(string Key, string Form);
