// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Persistiertes Laufzeitmodell fuer kontextbasierte Hilfe.
///
/// Persisted runtime model for context-based help.
/// </summary>
public sealed class THelpFile : ITStreamSerializable
{
    private readonly List<THelpTopic> _topics = [];

    /// <summary>
    /// Initialisiert eine leere Hilfedatei.
    ///
    /// Initializes an empty help file.
    /// </summary>
    public THelpFile()
    {
        Index = new THelpIndex();
    }

    /// <summary>
    /// Der Kontextindex der Hilfedatei.
    ///
    /// The help file's context index.
    /// </summary>
    public THelpIndex Index { get; }

    /// <summary>
    /// Die geladenen Hilfethemen.
    ///
    /// The loaded help topics.
    /// </summary>
    public IReadOnlyList<THelpTopic> Topics => _topics.AsReadOnly();

    /// <summary>
    /// Fuegt ein Thema hinzu und aktualisiert den Index.
    ///
    /// Adds a topic and updates the index.
    /// </summary>
    /// <param name="topic">Das hinzuzufuegende Thema. / The topic to add.</param>
    public void AddTopic(THelpTopic topic)
    {
        ArgumentNullException.ThrowIfNull(topic);
        Index.Add(topic.Context, _topics.Count);
        _topics.Add(topic);
    }

    /// <summary>
    /// Versucht ein Thema per Kontext zu finden.
    ///
    /// Tries to find a topic by context.
    /// </summary>
    /// <param name="context">Die Kontext-ID. / The context identifier.</param>
    /// <returns>Das gefundene Thema oder <c>null</c>. / The found topic or <c>null</c>.</returns>
    public THelpTopic? Lookup(int context)
    {
        if (!Index.TryGetPosition(context, out int position))
        {
            return null;
        }

        return position >= 0 && position < _topics.Count ? _topics[position] : null;
    }

    /// <summary>
    /// Liefert ein Thema oder einen Fallback fuer fehlende Kontexte.
    ///
    /// Returns a topic or a fallback for missing contexts.
    /// </summary>
    /// <param name="context">Die Kontext-ID. / The context identifier.</param>
    /// <returns>Ein vorhandenes Thema oder der Fallback. / An existing topic or the fallback.</returns>
    public THelpTopic GetTopicOrFallback(int context)
    {
        return Lookup(context) ?? CreateFallback(context);
    }

    /// <summary>
    /// Schreibt die Hilfedatei in einen Archiv-Writer.
    ///
    /// Writes the help file to an archive writer.
    /// </summary>
    /// <param name="writer">Der Ziel-Writer. / The destination writer.</param>
    public void SaveTo(TBinaryArchiveWriter writer)
    {
        writer.WriteInt32(_topics.Count);
        foreach (THelpTopic topic in _topics)
        {
            topic.SaveTo(writer);
        }
    }

    /// <summary>
    /// Speichert die Hilfedatei auf Platte.
    ///
    /// Saves the help file to disk.
    /// </summary>
    /// <param name="path">Der Zielpfad. / The destination path.</param>
    public void Save(string path)
    {
        using FileStream stream = File.Create(path);
        using TBinaryArchiveWriter writer = new(stream);
        SaveTo(writer);
    }

    /// <summary>
    /// Laedt eine Hilfedatei aus einem Archiv-Reader.
    ///
    /// Loads a help file from an archive reader.
    /// </summary>
    /// <param name="reader">Der Quell-Reader. / The source reader.</param>
    /// <returns>Die geladene Hilfedatei. / The loaded help file.</returns>
    public static THelpFile LoadFrom(TBinaryArchiveReader reader)
    {
        THelpFile helpFile = new();
        int topicCount = reader.ReadInt32();
        for (int index = 0; index < topicCount; index++)
        {
            helpFile.AddTopic(THelpTopic.LoadFrom(reader));
        }

        return helpFile;
    }

    /// <summary>
    /// Laedt eine Hilfedatei aus einem Pfad.
    ///
    /// Loads a help file from a path.
    /// </summary>
    /// <param name="path">Der Quellpfad. / The source path.</param>
    /// <returns>Die geladene Hilfedatei. / The loaded help file.</returns>
    public static THelpFile Load(string path)
    {
        using FileStream stream = File.OpenRead(path);
        using TBinaryArchiveReader reader = new(stream);
        return LoadFrom(reader);
    }

    /// <summary>
    /// Registriert Stream-Serialisierung fuer Hilfedatei und Hilfethemen.
    ///
    /// Registers stream serialization for help files and help topics.
    /// </summary>
    /// <param name="registry">Die Ziel-Registry. / The target registry.</param>
    public static void RegisterRecords(TRecordRegistry registry)
    {
        ArgumentNullException.ThrowIfNull(registry);
        registry.Register("help-topic", THelpTopic.LoadFrom);
        registry.RegisterStream("help-topic", THelpTopic.ReadFrom, THelpTopic.WriteTo);
        registry.Register("help-file", LoadFrom);
        registry.RegisterStream(
            "help-file",
            stream =>
            {
                THelpFile helpFile = new();
                int topicCount = stream.ReadInt32();
                for (int index = 0; index < topicCount; index++)
                {
                    helpFile.AddTopic(stream.ReadObject<THelpTopic>() ?? throw new InvalidDataException("Help topic payload is missing."));
                }

                return helpFile;
            },
            (stream, helpFile) =>
            {
                stream.WriteInt32(helpFile._topics.Count);
                foreach (THelpTopic topic in helpFile._topics)
                {
                    stream.WriteObject(topic);
                }
            });
    }

    private static THelpTopic CreateFallback(int context)
    {
        THelpTopic fallback = new(context, "Help not found");
        fallback.AddParagraph($"No help topic is available for context {context}.");
        return fallback;
    }
}
