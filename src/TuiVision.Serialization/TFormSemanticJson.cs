// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace TuiVision.Serialization;

/// <summary>
/// Liest und schreibt die geschlossene JSON-Formsemantik fail-closed.
///
/// Reads and writes the closed JSON form semantics fail-closed.
/// </summary>
public static partial class TFormSemanticJson
{
    private const int CurrentVersion = 1;
    private const int MaxBytes = 262_144;
    private const int MaxItems = 4096;
    private const int MaxDepth = 32;

    /// <summary>
    /// Deserialisiert und validiert ein vollständiges Dokument atomar.
    ///
    /// Deserializes and validates one complete document atomically.
    /// </summary>
    /// <param name="json">Der JSON-Text. / The JSON text.</param>
    /// <returns>Das vollständig validierte Dokument. / The fully validated document.</returns>
    /// <exception cref="InvalidDataException">Der Text oder seine Semantik ist ungültig. / The text or its semantics is invalid.</exception>
    public static TFormSemanticDocument Deserialize(string json)
    {
        ArgumentNullException.ThrowIfNull(json);
        if (Encoding.UTF8.GetByteCount(json) > MaxBytes)
        {
            throw Invalid("Document exceeds the 262144-byte limit.");
        }

        try
        {
            using JsonDocument document = JsonDocument.Parse(
                json,
                new JsonDocumentOptions
                {
                    AllowTrailingCommas = false,
                    CommentHandling = JsonCommentHandling.Disallow,
                    MaxDepth = MaxDepth
                });
            JsonElement root = document.RootElement;
            RequireObject(root, "document", "version", "form", "forms");
            int version = RequiredInt(root, "version", "document");
            if (version != CurrentVersion)
            {
                throw Invalid($"Unsupported form semantic version '{version}'.");
            }

            string rootForm = RequiredKey(root, "form", "document");
            JsonElement formsElement = RequiredArray(root, "forms", "document");
            int itemCount = formsElement.GetArrayLength();
            if (itemCount == 0 || itemCount > MaxItems)
            {
                throw Invalid("Form count must be between 1 and 4096.");
            }

            List<TFormSemanticDefinition> forms = new(itemCount);
            foreach (JsonElement formElement in formsElement.EnumerateArray())
            {
                forms.Add(ParseForm(formElement, ref itemCount));
            }

            TFormSemanticDocument result = new(version, rootForm, forms);
            ValidateGraph(result);
            return result;
        }
        catch (JsonException exception)
        {
            throw Invalid("Malformed form semantic JSON.", exception);
        }
    }

    /// <summary>
    /// Serialisiert ein validiertes Dokument deterministisch.
    ///
    /// Serializes a validated document deterministically.
    /// </summary>
    /// <param name="document">Das Dokument. / The document.</param>
    /// <returns>Der deterministische JSON-Text. / The deterministic JSON text.</returns>
    /// <exception cref="InvalidDataException">Das Dokument ist semantisch ungültig. / The document is semantically invalid.</exception>
    public static string Serialize(TFormSemanticDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        ValidateGraph(document);
        ValidateItemLimit(document);
        using MemoryStream stream = new();
        using (Utf8JsonWriter writer = new(stream, new JsonWriterOptions { Indented = true }))
        {
            writer.WriteStartObject();
            writer.WriteNumber("version", document.Version);
            writer.WriteString("form", document.RootForm);
            writer.WriteStartArray("forms");
            foreach (TFormSemanticDefinition form in document.Forms)
            {
                writer.WriteStartObject();
                writer.WriteString("form", form.Key);
                writer.WriteStartArray("fields");
                foreach (TFormSemanticField field in form.Fields)
                {
                    writer.WriteStartObject();
                    writer.WriteString("field", field.Key);
                    writer.WriteString("control", field.Control);
                    writer.WriteString("type", field.Type);
                    writer.WriteString("binding", field.Binding);
                    writer.WriteString("converter", field.Converter);
                    writer.WriteStartArray("validators");
                    foreach (string validator in field.Validators)
                    {
                        writer.WriteStringValue(validator);
                    }

                    writer.WriteEndArray();
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
                writer.WriteStartArray("children");
                foreach (TFormSemanticChild child in form.Children)
                {
                    writer.WriteStartObject();
                    writer.WriteString("child", child.Key);
                    writer.WriteString("form", child.Form);
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        if (stream.Length > MaxBytes)
        {
            throw Invalid("Document exceeds the 262144-byte limit.");
        }

        return Encoding.UTF8.GetString(stream.ToArray());
    }

    private static TFormSemanticDefinition ParseForm(JsonElement element, ref int itemCount)
    {
        RequireObject(element, "form", "form", "fields", "children");
        string key = RequiredKey(element, "form", "form");
        JsonElement fieldsElement = RequiredArray(element, "fields", $"form '{key}'");
        JsonElement childrenElement = RequiredArray(element, "children", $"form '{key}'");
        itemCount += fieldsElement.GetArrayLength() + childrenElement.GetArrayLength();
        if (itemCount > MaxItems)
        {
            throw Invalid("Document exceeds the 4096-item limit.");
        }

        List<TFormSemanticField> fields = [];
        foreach (JsonElement fieldElement in fieldsElement.EnumerateArray())
        {
            fields.Add(ParseField(fieldElement, ref itemCount));
        }

        List<TFormSemanticChild> children = [];
        foreach (JsonElement childElement in childrenElement.EnumerateArray())
        {
            RequireObject(childElement, "child", "child", "form");
            children.Add(new TFormSemanticChild(
                RequiredKey(childElement, "child", "child"),
                RequiredKey(childElement, "form", "child")));
        }

        return new TFormSemanticDefinition(key, fields, children);
    }

    private static TFormSemanticField ParseField(JsonElement element, ref int itemCount)
    {
        RequireObject(element, "field", "field", "control", "type", "binding", "converter", "validators");
        string key = RequiredKey(element, "field", "field");
        JsonElement validatorsElement = RequiredArray(element, "validators", $"field '{key}'");
        itemCount += validatorsElement.GetArrayLength();
        if (itemCount > MaxItems)
        {
            throw Invalid("Document exceeds the 4096-item limit.");
        }

        List<string> validators = [];
        HashSet<string> uniqueValidators = new(StringComparer.Ordinal);
        foreach (JsonElement validator in validatorsElement.EnumerateArray())
        {
            if (validator.ValueKind != JsonValueKind.String)
            {
                throw Invalid($"Validator key in field '{key}' must be a string.");
            }

            string value = ValidateKey(validator.GetString(), "validator");
            if (!uniqueValidators.Add(value))
            {
                throw Invalid($"Duplicate validator '{value}' in field '{key}'.");
            }

            validators.Add(value);
        }

        return new TFormSemanticField(
            key,
            RequiredKey(element, "control", $"field '{key}'"),
            RequiredKey(element, "type", $"field '{key}'"),
            RequiredKey(element, "binding", $"field '{key}'"),
            RequiredKey(element, "converter", $"field '{key}'"),
            validators);
    }

    private static void ValidateGraph(TFormSemanticDocument document)
    {
        if (document.Version != CurrentVersion)
        {
            throw Invalid($"Unsupported form semantic version '{document.Version}'.");
        }

        string root = ValidateKey(document.RootForm, "root form");
        Dictionary<string, TFormSemanticDefinition> forms = new(StringComparer.Ordinal);
        foreach (TFormSemanticDefinition form in document.Forms)
        {
            string formKey = ValidateKey(form.Key, "form");
            if (!forms.TryAdd(formKey, form))
            {
                throw Invalid($"Duplicate form '{formKey}'.");
            }

            HashSet<string> members = new(StringComparer.Ordinal);
            foreach (TFormSemanticField field in form.Fields)
            {
                string fieldKey = ValidateKey(field.Key, "field");
                if (!members.Add(fieldKey))
                {
                    throw Invalid($"Duplicate member '{fieldKey}' in form '{formKey}'.");
                }

                _ = ValidateKey(field.Control, "control");
                _ = ValidateKey(field.Type, "type");
                _ = ValidateKey(field.Binding, "binding");
                _ = ValidateKey(field.Converter, "converter");
                HashSet<string> validators = new(StringComparer.Ordinal);
                foreach (string validator in field.Validators)
                {
                    string validatorKey = ValidateKey(validator, "validator");
                    if (!validators.Add(validatorKey))
                    {
                        throw Invalid($"Duplicate validator '{validatorKey}' in field '{fieldKey}'.");
                    }
                }
            }

            foreach (TFormSemanticChild child in form.Children)
            {
                string childKey = ValidateKey(child.Key, "child");
                _ = ValidateKey(child.Form, "child form");
                if (!members.Add(childKey))
                {
                    throw Invalid($"Duplicate member '{childKey}' in form '{formKey}'.");
                }
            }
        }

        if (!forms.ContainsKey(root))
        {
            throw Invalid($"Unknown root form '{root}'.");
        }

        HashSet<string> referencedForms = new(StringComparer.Ordinal);
        foreach (TFormSemanticDefinition form in forms.Values)
        {
            foreach (TFormSemanticChild child in form.Children)
            {
                if (!forms.ContainsKey(child.Form))
                {
                    throw Invalid($"Child '{child.Key}' references unknown form '{child.Form}'.");
                }

                if (!referencedForms.Add(child.Form))
                {
                    throw Invalid($"Form '{child.Form}' has more than one child owner.");
                }
            }
        }

        // Drei Farben machen Back-Edges und unbenutzte Definitionen sichtbar,
        // ohne je ein partielles Runtime-Modell zu veröffentlichen.
        // Three colours expose back edges and unused definitions without ever
        // publishing a partial runtime model.
        Dictionary<string, byte> colors = forms.Keys.ToDictionary(key => key, _ => (byte)0, StringComparer.Ordinal);
        int visited = Visit(root, forms, colors, 1);
        if (visited != forms.Count)
        {
            throw Invalid("Every form definition must be reachable from the root form.");
        }
    }

    private static int Visit(
        string key,
        IReadOnlyDictionary<string, TFormSemanticDefinition> forms,
        IDictionary<string, byte> colors,
        int depth)
    {
        if (depth > MaxDepth)
        {
            throw Invalid("Child graph depth exceeds the 32-level limit.");
        }

        if (colors[key] == 1)
        {
            throw Invalid($"Cyclic child reference reaches form '{key}'.");
        }

        if (colors[key] == 2)
        {
            return 0;
        }

        colors[key] = 1;
        int count = 1;
        foreach (TFormSemanticChild child in forms[key].Children)
        {
            count += Visit(child.Form, forms, colors, depth + 1);
        }

        colors[key] = 2;
        return count;
    }

    private static void ValidateItemLimit(TFormSemanticDocument document)
    {
        int itemCount = document.Forms.Count;
        foreach (TFormSemanticDefinition form in document.Forms)
        {
            itemCount += form.Fields.Count + form.Children.Count;
            foreach (TFormSemanticField field in form.Fields)
            {
                itemCount += field.Validators.Count;
            }

            if (itemCount > MaxItems)
            {
                throw Invalid("Document exceeds the 4096-item limit.");
            }
        }
    }

    private static void RequireObject(JsonElement element, string context, params string[] allowed)
    {
        if (element.ValueKind != JsonValueKind.Object)
        {
            throw Invalid($"{context} must be an object.");
        }

        HashSet<string> expected = new(allowed, StringComparer.Ordinal);
        HashSet<string> seen = new(StringComparer.Ordinal);
        foreach (JsonProperty property in element.EnumerateObject())
        {
            if (!expected.Contains(property.Name))
            {
                throw Invalid($"Unknown property '{property.Name}' in {context}.");
            }

            if (!seen.Add(property.Name))
            {
                throw Invalid($"Duplicate property '{property.Name}' in {context}.");
            }
        }

        foreach (string required in expected)
        {
            if (!seen.Contains(required))
            {
                throw Invalid($"Missing property '{required}' in {context}.");
            }
        }
    }

    private static JsonElement RequiredArray(JsonElement element, string name, string context)
    {
        JsonElement value = element.GetProperty(name);
        if (value.ValueKind != JsonValueKind.Array)
        {
            throw Invalid($"Property '{name}' in {context} must be an array.");
        }

        return value;
    }

    private static int RequiredInt(JsonElement element, string name, string context)
    {
        JsonElement value = element.GetProperty(name);
        if (!value.TryGetInt32(out int result))
        {
            throw Invalid($"Property '{name}' in {context} must be an integer.");
        }

        return result;
    }

    private static string RequiredKey(JsonElement element, string name, string context)
    {
        JsonElement value = element.GetProperty(name);
        if (value.ValueKind != JsonValueKind.String)
        {
            throw Invalid($"Property '{name}' in {context} must be a string key.");
        }

        return ValidateKey(value.GetString(), name);
    }

    private static string ValidateKey(string? value, string role)
    {
        if (string.IsNullOrEmpty(value) || !SafeKey().IsMatch(value))
        {
            throw Invalid($"Invalid {role} key '{value}'.");
        }

        return value;
    }

    private static InvalidDataException Invalid(string message, Exception? inner = null) =>
        new($"Form semantic JSON is invalid: {message}", inner);

    [GeneratedRegex("^[a-z][a-z0-9-]{0,63}$", RegexOptions.CultureInvariant)]
    private static partial Regex SafeKey();
}
