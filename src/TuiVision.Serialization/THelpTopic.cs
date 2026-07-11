// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Navigierbare Querverweis-Markierung innerhalb eines Hilfethemas.
///
/// Navigable cross-reference marker inside a help topic.
/// </summary>
/// <param name="TargetContext">Der Zielkontext. / The target context.</param>
/// <param name="Label">Das sichtbare Label. / The visible label.</param>
/// <param name="Offset">Der Textoffset. / The text offset.</param>
/// <param name="Length">Die Textlaenge. / The text length.</param>
public readonly record struct THelpCrossReference(int TargetContext, string Label, int Offset, int Length);

/// <summary>
/// Laufzeitmodell fuer ein einzelnes Hilfethema.
///
/// Runtime model for a single help topic.
/// </summary>
public sealed class THelpTopic : ITStreamSerializable
{
    private readonly List<string> _paragraphs = [];
    private readonly List<THelpCrossReference> _crossReferences = [];

    /// <summary>
    /// Initialisiert ein neues Hilfethema.
    ///
    /// Initializes a new help topic.
    /// </summary>
    /// <param name="context">Die numerische Kontext-ID. / The numeric context identifier.</param>
    /// <param name="title">Der Thementitel. / The topic title.</param>
    public THelpTopic(int context, string title)
    {
        Context = context;
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    /// <summary>
    /// Die numerische Kontext-ID.
    ///
    /// The numeric context identifier.
    /// </summary>
    public int Context { get; }

    /// <summary>
    /// Der Thementitel.
    ///
    /// The topic title.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Die Textabschnitte des Themas.
    ///
    /// The text paragraphs of the topic.
    /// </summary>
    public IReadOnlyList<string> Paragraphs => _paragraphs.AsReadOnly();

    /// <summary>
    /// Die Querverweise des Themas.
    ///
    /// The topic's cross references.
    /// </summary>
    public IReadOnlyList<THelpCrossReference> CrossReferences => _crossReferences.AsReadOnly();

    /// <summary>
    /// Fuegt einen Absatz hinzu.
    ///
    /// Adds a paragraph.
    /// </summary>
    /// <param name="paragraph">Der Absatztext. / The paragraph text.</param>
    public void AddParagraph(string paragraph)
    {
        _paragraphs.Add(paragraph ?? string.Empty);
    }

    /// <summary>
    /// Fuegt einen Querverweis hinzu.
    ///
    /// Adds a cross reference.
    /// </summary>
    /// <param name="reference">Der Querverweis. / The cross reference.</param>
    public void AddCrossReference(THelpCrossReference reference)
    {
        _crossReferences.Add(reference);
    }

    /// <summary>
    /// Schreibt das Thema in einen Archiv-Writer.
    ///
    /// Writes the topic to an archive writer.
    /// </summary>
    /// <param name="writer">Der Ziel-Writer. / The destination writer.</param>
    public void SaveTo(TBinaryArchiveWriter writer)
    {
        writer.WriteInt32(Context);
        writer.WriteString(Title);
        writer.WriteInt32(_paragraphs.Count);
        foreach (string paragraph in _paragraphs)
        {
            writer.WriteString(paragraph);
        }

        writer.WriteInt32(_crossReferences.Count);
        foreach (THelpCrossReference reference in _crossReferences)
        {
            writer.WriteInt32(reference.TargetContext);
            writer.WriteString(reference.Label);
            writer.WriteInt32(reference.Offset);
            writer.WriteInt32(reference.Length);
        }
    }

    /// <summary>
    /// Laedt ein Thema aus einem Archiv-Reader.
    ///
    /// Loads a topic from an archive reader.
    /// </summary>
    /// <param name="reader">Der Quell-Reader. / The source reader.</param>
    /// <returns>Das geladene Thema. / The loaded topic.</returns>
    public static THelpTopic LoadFrom(TBinaryArchiveReader reader)
    {
        THelpTopic topic = new(reader.ReadInt32(), reader.ReadString());
        int paragraphCount = reader.ReadInt32();
        if (paragraphCount < 0)
        {
            throw new InvalidDataException("Help-topic paragraph count must be non-negative.");
        }

        for (int index = 0; index < paragraphCount; index++)
        {
            topic.AddParagraph(reader.ReadString());
        }

        int referenceCount = reader.ReadInt32();
        if (referenceCount < 0)
        {
            throw new InvalidDataException("Help-topic reference count must be non-negative.");
        }

        for (int index = 0; index < referenceCount; index++)
        {
            topic.AddCrossReference(new THelpCrossReference(
                reader.ReadInt32(),
                reader.ReadString(),
                reader.ReadInt32(),
                reader.ReadInt32()));
        }

        return topic;
    }

    internal static THelpTopic ReadFrom(ipstream stream)
    {
        THelpTopic topic = new(stream.ReadInt32(), stream.ReadString());
        int paragraphCount = stream.ReadInt32();
        if (paragraphCount < 0)
        {
            throw new InvalidDataException("Help-topic paragraph count must be non-negative.");
        }

        for (int index = 0; index < paragraphCount; index++)
        {
            topic.AddParagraph(stream.ReadString());
        }

        int referenceCount = stream.ReadInt32();
        if (referenceCount < 0)
        {
            throw new InvalidDataException("Help-topic reference count must be non-negative.");
        }

        for (int index = 0; index < referenceCount; index++)
        {
            topic.AddCrossReference(new THelpCrossReference(
                stream.ReadInt32(),
                stream.ReadString(),
                stream.ReadInt32(),
                stream.ReadInt32()));
        }

        return topic;
    }

    internal static void WriteTo(opstream stream, THelpTopic topic)
    {
        stream.WriteInt32(topic.Context);
        stream.WriteString(topic.Title);
        stream.WriteInt32(topic._paragraphs.Count);
        foreach (string paragraph in topic._paragraphs)
        {
            stream.WriteString(paragraph);
        }

        stream.WriteInt32(topic._crossReferences.Count);
        foreach (THelpCrossReference reference in topic._crossReferences)
        {
            stream.WriteInt32(reference.TargetContext);
            stream.WriteString(reference.Label);
            stream.WriteInt32(reference.Offset);
            stream.WriteInt32(reference.Length);
        }
    }
}
