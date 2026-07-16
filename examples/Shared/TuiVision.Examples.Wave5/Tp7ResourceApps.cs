// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Examples.Wave5;

/// <summary>Funktionales TP7-Ressourcenanzeigebeispiel. / Functional TP7 resource display example.</summary>
public sealed class Tp7ResourceDemoApp : Wave5Application
{
    /// <summary>Lädt Resource-Bytes aus `Message.Info`. / Loads resource bytes from `Message.Info`.</summary>
    public const ushort CmLoadResources = 32401;

    /// <summary>Initialisiert die Ressourcenanzeige. / Initializes the resource display.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    public Tp7ResourceDemoApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        ShowContent("TP7 Resource Demo", "TP7 Resource Demo\nExact named menu, status and dialog records");
        SetStatus("Tp7ResourceDemo", "ready");
    }

    /// <summary>Ob der letzte Load vollständig war. / Whether the last load was complete.</summary>
    public bool LoadSucceeded { get; private set; }

    /// <summary>Ob eine Ablehnung ohne veröffentlichtes Teilmodell blieb. / Whether rejection published no partial model.</summary>
    public bool RejectedWithoutPartialModel { get; private set; }

    /// <summary>Geladener Dialogtitel. / Loaded dialog title.</summary>
    public string DialogTitle { get; private set; } = string.Empty;

    /// <summary>Geladenes Menülabel. / Loaded menu label.</summary>
    public string MenuLabel { get; private set; } = string.Empty;

    /// <summary>Geladenes Statuslabel. / Loaded status label.</summary>
    public string StatusLabel { get; private set; } = string.Empty;

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmLoadResources)
        {
            LoadResources(@event.Message.Info as byte[]);
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private void LoadResources(byte[]? bytes)
    {
        LoadSucceeded = false;
        RejectedWithoutPartialModel = false;
        try
        {
            if (bytes is null)
            {
                throw new InvalidDataException("Resource bytes are missing.");
            }

            TRecordRegistry registry = CreateRegistry();
            using MemoryStream stream = new(bytes, writable: false);
            TResourceFile resources = TResourceFile.Load(stream, registry);
            TDialogDescriptionRecord dialog = resources.Get<TDialogDescriptionRecord>("Dialog")
                ?? throw new InvalidDataException("Dialog resource is missing.");
            TMenuDescriptionRecord menu = resources.Get<TMenuDescriptionRecord>("Menu")
                ?? throw new InvalidDataException("Menu resource is missing.");
            TStatusLineDescriptionRecord status = resources.Get<TStatusLineDescriptionRecord>("Status")
                ?? throw new InvalidDataException("Status resource is missing.");

            // Erst nach vollständiger exakter Auflösung werden sichtbare Felder veröffentlicht.
            // Visible fields are published only after complete exact resolution.
            DialogTitle = dialog.Title;
            MenuLabel = menu.Items.Single(item => item.Id == "demo").Label;
            StatusLabel = status.Definitions[0].Items[0].Label;
            LoadSucceeded = true;
            ShowContent(
                "TP7 Resource Demo",
                $"TP7 Resource Dialog: {DialogTitle}\nMenu: {MenuLabel}\nStatus: {StatusLabel}\nExact keys: Dialog, Menu, Status");
            SetStatus("Tp7ResourceDemo", "loaded exact records");
        }
        catch (Exception exception) when (exception is InvalidDataException or EndOfStreamException or KeyNotFoundException)
        {
            DialogTitle = string.Empty;
            MenuLabel = string.Empty;
            StatusLabel = string.Empty;
            RejectedWithoutPartialModel = true;
            ShowContent(
                "TP7 Resource Rejection",
                $"TP7 resources rejected\nReason: {exception.GetType().Name}\nNo partial model: true");
            SetStatus("Tp7ResourceDemo", "rejected atomically");
        }
    }

    internal static TRecordRegistry CreateRegistry()
    {
        TRecordRegistry registry = new();
        TResourceFile.RegisterBuiltInTypes(registry);
        return registry;
    }
}

/// <summary>Funktionales TP7-Ressourcengeneratorbeispiel. / Functional TP7 resource generator example.</summary>
public sealed class Tp7ResourceGeneratorApp : Wave5Application
{
    /// <summary>Erzeugt eine allowlist-basierte Ressource. / Generates an allowlist-based resource.</summary>
    public const ushort CmGenerate = 32402;

    /// <summary>Initialisiert den Generator. / Initializes the generator.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    /// <param name="allowedOutputDirectory">Optionales test-eigenes Ziel. / Optional test-owned target.</param>
    public Tp7ResourceGeneratorApp(TRect bounds, bool headless = false, string? allowedOutputDirectory = null) : base(bounds, headless)
    {
        AllowedOutputDirectory = allowedOutputDirectory is null ? null : Path.GetFullPath(allowedOutputDirectory);
        ShowContent("TP7 Resource Generator", "TP7 Resource Generator\nAllowlisted records and controlled output");
        SetStatus("Tp7ResourceGenerator", "ready");
    }

    /// <summary>Optionales kontrolliertes Ziel. / Optional controlled target.</summary>
    public string? AllowedOutputDirectory { get; }

    /// <summary>Letzte erzeugte Bytes. / Last generated bytes.</summary>
    public byte[]? GeneratedBytes { get; private set; }

    /// <summary>Letzter erzeugter Pfad. / Last generated path.</summary>
    public string? GeneratedPath { get; private set; }

    /// <summary>Ob die letzte Anforderung abgelehnt wurde. / Whether the last request was rejected.</summary>
    public bool GenerationRejected { get; private set; }

    /// <summary>Beschreibt einen kontrollierten Generatoraufruf. / Describes a controlled generator request.</summary>
    /// <param name="RelativePath">Relativer Zielpfad. / Relative target path.</param>
    public readonly record struct GenerateRequest(string RelativePath);

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmGenerate)
        {
            Generate(@event.Message.Info is GenerateRequest request ? request : default);
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private void Generate(GenerateRequest request)
    {
        GenerationRejected = true;
        GeneratedBytes = null;
        GeneratedPath = null;
        if (AllowedOutputDirectory is null
            || string.IsNullOrWhiteSpace(request.RelativePath)
            || Path.IsPathRooted(request.RelativePath))
        {
            SetStatus("Tp7ResourceGenerator", "generation rejected: no controlled target");
            return;
        }

        string root = Path.TrimEndingDirectorySeparator(AllowedOutputDirectory);
        string candidate = Path.GetFullPath(Path.Combine(root, request.RelativePath));
        string prefix = root + Path.DirectorySeparatorChar;
        if (!candidate.StartsWith(prefix, StringComparison.Ordinal))
        {
            SetStatus("Tp7ResourceGenerator", "generation rejected: outside controlled root");
            return;
        }

        byte[] bytes = CreateValidResourceBytes();
        Directory.CreateDirectory(root);
        File.WriteAllBytes(candidate, bytes);
        GeneratedBytes = bytes;
        GeneratedPath = candidate;
        GenerationRejected = false;
        ShowContent(
            "TP7 Resource Generator",
            $"TP7 resources generated\nFile: {Path.GetFileName(candidate)}\nRecords: Dialog, Menu, Status\nAllowlist only");
        SetStatus("Tp7ResourceGenerator", $"generated={Path.GetFileName(candidate)}");
    }

    private static byte[] CreateValidResourceBytes()
    {
        TRecordRegistry registry = Tp7ResourceDemoApp.CreateRegistry();
        TResourceFile resources = new(registry);
        resources.Put(
            "Dialog",
            new TDialogDescriptionRecord(
                PersistedDialogRepresentation.CurrentFormatVersion,
                "tp7-resource-dialog",
                1,
                "TP7 Resource Dialog",
                [new TDialogControlDescriptionRecord("ok", "button", "OK", null, true)],
                ["ok"],
                [new TDialogCommandBindingRecord(32410, "ok", "accept", "Enter")]));
        resources.Put(
            "Menu",
            new TMenuDescriptionRecord(
                TMenuDescriptionRecord.CurrentFormatVersion,
                [
                    new TMenuItemDescriptionRecord("demo", null, 0, "~D~emo", 0, 100, false),
                    new TMenuItemDescriptionRecord("open", "demo", 0, "~O~pen", 32411, 101, false)
                ]));
        resources.Put(
            "Status",
            new TStatusLineDescriptionRecord(
                TStatusLineDescriptionRecord.CurrentFormatVersion,
                [
                    new TStatusDefinitionDescriptionRecord(
                        0,
                        999,
                        0,
                        [new TStatusItemDescriptionRecord("~F1~ Help", 32412, 0x3B00, false)])
                ]));
        using MemoryStream stream = new();
        resources.Save(stream);
        return stream.ToArray();
    }
}
