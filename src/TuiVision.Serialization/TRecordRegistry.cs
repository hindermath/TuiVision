// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Registry fuer stabile Typ-IDs ueber Archiv- und Stream-Fabriken.
///
/// Registry for stable type identifiers across archive and stream factories.
/// </summary>
public sealed class TRecordRegistry
{
    private readonly Dictionary<string, Func<TBinaryArchiveReader, object>> _archiveFactories = new(StringComparer.Ordinal);
    private readonly Dictionary<string, Func<ipstream, object>> _streamFactories = new(StringComparer.Ordinal);
    private readonly Dictionary<Type, StreamWriterRegistration> _streamWriters = new();

    /// <summary>
    /// Registriert einen Archiv-Record.
    ///
    /// Registers an archive record.
    /// </summary>
    /// <typeparam name="T">Der Record-Typ. / The record type.</typeparam>
    /// <param name="typeId">Die stabile Typ-ID. / The stable type identifier.</param>
    /// <param name="factory">Die Archivfabrik. / The archive factory.</param>
    public void Register<T>(string typeId, Func<TBinaryArchiveReader, T> factory)
        where T : notnull
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(typeId);
        ArgumentNullException.ThrowIfNull(factory);

        if (!_archiveFactories.TryAdd(typeId, reader => factory(reader)!))
        {
            throw new InvalidOperationException($"Type id '{typeId}' is already registered.");
        }
    }

    /// <summary>
    /// Registriert einen Stream-Typ mit gemeinsamer Referenzsemantik.
    ///
    /// Registers a stream type with shared-reference semantics.
    /// </summary>
    /// <typeparam name="T">Der Laufzeittyp. / The runtime type.</typeparam>
    /// <param name="typeId">Die stabile Typ-ID. / The stable type identifier.</param>
    /// <param name="factory">Die Stream-Fabrik. / The stream factory.</param>
    /// <param name="writer">Der Stream-Writer. / The stream writer.</param>
    public void RegisterStream<T>(string typeId, Func<ipstream, T> factory, Action<opstream, T> writer)
        where T : class
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(typeId);
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(writer);

        if (_streamFactories.ContainsKey(typeId) || _streamWriters.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException($"Stream registration for '{typeId}' already exists.");
        }

        _streamFactories[typeId] = stream => factory(stream);
        _streamWriters[typeof(T)] = new StreamWriterRegistration(
            typeId,
            (stream, value) => writer(stream, (T)value));
    }

    /// <summary>
    /// Erstellt einen Archiv-Record aus einer Typ-ID.
    ///
    /// Creates an archive record from a type identifier.
    /// </summary>
    /// <param name="typeId">Die Typ-ID. / The type identifier.</param>
    /// <param name="reader">Der Archiv-Reader. / The archive reader.</param>
    /// <returns>Das erzeugte Objekt. / The created object.</returns>
    public object Create(string typeId, TBinaryArchiveReader reader)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(typeId);
        ArgumentNullException.ThrowIfNull(reader);

        if (!_archiveFactories.TryGetValue(typeId, out Func<TBinaryArchiveReader, object>? factory))
        {
            throw new KeyNotFoundException($"No factory registered for type id '{typeId}'.");
        }

        return factory(reader);
    }

    internal object CreateStream(string typeId, ipstream stream)
    {
        if (!_streamFactories.TryGetValue(typeId, out Func<ipstream, object>? factory))
        {
            throw new InvalidDataException($"No stream factory registered for type id '{typeId}'.");
        }

        return factory(stream);
    }

    internal StreamWriterRegistration ResolveWriter(Type type)
    {
        if (!_streamWriters.TryGetValue(type, out StreamWriterRegistration? registration))
        {
            throw new InvalidDataException($"No stream writer registered for type '{type.FullName}'.");
        }

        return registration;
    }

    internal sealed record StreamWriterRegistration(string TypeId, Action<opstream, object> Writer);
}
