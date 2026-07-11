// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Serialization;

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Gemeinsame Testhilfen fuer Serialisierungs- und Stream-Komponenten.
///
/// Shared test helpers for serialization and stream components.
/// </summary>
public static class SerializationTestSupport
{
    /// <summary>
    /// Erstellt eine Registry mit einem einfachen Test-Record fuer Archiv-Tests.
    ///
    /// Creates a registry with a simple test record for archive tests.
    /// </summary>
    /// <returns>Die konfigurierte Registry. / The configured registry.</returns>
    public static TRecordRegistry CreateArchiveRegistry()
    {
        TRecordRegistry registry = new();
        registry.Register("test-record", TestRecord.LoadFrom);
        return registry;
    }

    /// <summary>
    /// Erstellt eine Registry mit Stream-Registrierungen fuer Graph-Tests.
    ///
    /// Creates a registry with stream registrations for graph tests.
    /// </summary>
    /// <returns>Die konfigurierte Registry. / The configured registry.</returns>
    public static TRecordRegistry CreateStreamRegistry()
    {
        TRecordRegistry registry = CreateArchiveRegistry();
        // Paar und Knoten trennen Shared-Reference-Beweis von Zyklus-Ablehnung in derselben stabilen Testform.
        // Pair and node separate shared-reference proof from cycle rejection in one stable test shape.
        registry.RegisterStream(
            "graph-node",
            stream =>
            {
                string name = stream.ReadString();
                GraphNode? child = stream.ReadObject<GraphNode>();
                return new GraphNode(name) { Child = child };
            },
            (stream, node) =>
            {
                stream.WriteString(node.Name);
                stream.WriteObject(node.Child);
            });
        registry.RegisterStream(
            "graph-pair",
            stream =>
            {
                GraphNode? left = stream.ReadObject<GraphNode>();
                GraphNode? right = stream.ReadObject<GraphNode>();
                return new GraphPair(left, right);
            },
            (stream, pair) =>
            {
                stream.WriteObject(pair.Left);
                stream.WriteObject(pair.Right);
            });

        return registry;
    }

    /// <summary>
    /// Einfacher Archiv-Record fuer Serializer-Regressionen.
    ///
    /// Simple archive record for serializer regressions.
    /// </summary>
    /// <param name="Id">Die Test-ID. / The test identifier.</param>
    /// <param name="Text">Der Testtext. / The test text.</param>
    public sealed record TestRecord(int Id, string Text) : ITStreamSerializable
    {
        /// <summary>
        /// Laedt einen Record aus dem Reader.
        ///
        /// Loads a record from the reader.
        /// </summary>
        /// <param name="reader">Der Reader mit den Nutzdaten. / The reader containing the payload.</param>
        /// <returns>Der geladene Record. / The loaded record.</returns>
        public static TestRecord LoadFrom(TBinaryArchiveReader reader)
        {
            return new TestRecord(reader.ReadInt32(), reader.ReadString());
        }

        /// <summary>
        /// Schreibt den Record in den Writer.
        ///
        /// Writes the record to the writer.
        /// </summary>
        /// <param name="writer">Der Ziel-Writer. / The target writer.</param>
        public void SaveTo(TBinaryArchiveWriter writer)
        {
            writer.WriteInt32(Id);
            writer.WriteString(Text);
        }
    }

    /// <summary>
    /// Einfacher Knoten fuer Referenz- und Zyklus-Tests.
    ///
    /// Simple node for reference and cycle tests.
    /// </summary>
    public sealed class GraphNode
    {
        /// <summary>
        /// Erstellt einen neuen Graphknoten.
        ///
        /// Creates a new graph node.
        /// </summary>
        /// <param name="name">Der Knotentext. / The node text.</param>
        public GraphNode(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Der Knotentext.
        ///
        /// The node text.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Optionale Kind-Referenz.
        ///
        /// Optional child reference.
        /// </summary>
        public GraphNode? Child { get; set; }
    }

    /// <summary>
    /// Einfaches Referenzpaar fuer Shared-Reference-Tests.
    ///
    /// Simple reference pair for shared-reference tests.
    /// </summary>
    /// <param name="Left">Linke Referenz. / Left reference.</param>
    /// <param name="Right">Rechte Referenz. / Right reference.</param>
    public sealed record GraphPair(GraphNode? Left, GraphNode? Right);
}
