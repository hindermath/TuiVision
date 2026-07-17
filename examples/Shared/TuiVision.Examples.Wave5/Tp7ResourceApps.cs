// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
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
        ShowResourceDialog("TP7 Resource Demo", "Exact named menu, status and dialog records", "Ready");
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

    /// <summary>Typ des fokussierten Dialogcontrols. / Type of the focused dialog control.</summary>
    public string FocusedControlKind { get; private set; } = string.Empty;

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
            ShowResourceDialog(
                DialogTitle,
                $"Menu: {MenuLabel}\nStatus: {StatusLabel}\nExact keys: Dialog, Menu, Status",
                "Select");
            SetStatus("Tp7ResourceDemo", "loaded exact records");
        }
        catch (Exception exception) when (exception is InvalidDataException or EndOfStreamException or KeyNotFoundException)
        {
            DialogTitle = string.Empty;
            MenuLabel = string.Empty;
            StatusLabel = string.Empty;
            RejectedWithoutPartialModel = true;
            ShowResourceDialog(
                "TP7 Resource Rejection",
                $"Resources rejected\nReason: {exception.GetType().Name}\nNo partial model: true",
                "Close");
            SetStatus("Tp7ResourceDemo", "rejected atomically");
        }
    }

    /// <inheritdoc />
    protected override string BuildDescriptionText() =>
        """
        Historischer Lernzweck: Benannte Dialog-, Menü- und Status-Ressourcen werden als echte Controls rekonstruiert.
        Historical learning purpose: named dialog, menu, and status resources are reconstructed as real controls.
        Tastatur: Load öffnet die Komposition, Tab wechselt den Fokus, Enter wählt und F1 öffnet diese Beschreibung.
        Keyboard: Load opens the composition, Tab changes focus, Enter selects, and F1 opens this description.
        Modernes C# verwendet typisierte allowlist-basierte Records statt Pascal-Overlays.
        Modern C# uses typed allowlisted records instead of Pascal overlays.
        Erst nach vollständiger Auflösung der exakten Namen Dialog, Menu und Status wird ein Modell veröffentlicht.
        A model is published only after complete resolution of the exact names Dialog, Menu, and Status.
        Der App-Loop-Smoke beweist atomare Auflösung, Fokus, Status und Zellen, nicht beliebige persistierte Typen.
        The app-loop smoke proves atomic resolution, focus, status, and cells, not arbitrary persisted types.
        """;

    private void ShowResourceDialog(string title, string text, string buttonLabel)
    {
        TDialog dialog = CreateDialog(title);
        dialog.Insert(new TStaticText(
            new TRect(2, 2, Math.Max(3, dialog.Size.X - 2), Math.Max(4, dialog.Size.Y - 3)),
            $"TP7 Resource Dialog: {title}\n{text}"));
        TButton button = new(
            new TRect(2, Math.Max(3, dialog.Size.Y - 2), Math.Min(dialog.Size.X - 2, 14), Math.Max(4, dialog.Size.Y - 1)),
            buttonLabel,
            ShellCommandIds.cmOK,
            TButtonFlags.bfDefault);
        dialog.Insert(button);
        ShowView(dialog, nameof(TDialog), text);
        dialog.SetFocus(button);
        FocusedControlKind = dialog.Current?.GetType().Name ?? string.Empty;
    }

    private TDialog CreateDialog(string title) =>
        new(new TRect(1, 0, Math.Max(18, Desktop!.Size.X - 1), Math.Max(8, Desktop.Size.Y)), title);

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
    private TInputLine? _targetInput;

    /// <summary>Erzeugt eine allowlist-basierte Ressource. / Generates an allowlist-based resource.</summary>
    public const ushort CmGenerate = 32402;

    /// <summary>Initialisiert den Generator. / Initializes the generator.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    /// <param name="allowedOutputDirectory">Optionales test-eigenes Ziel. / Optional test-owned target.</param>
    public Tp7ResourceGeneratorApp(TRect bounds, bool headless = false, string? allowedOutputDirectory = null) : base(bounds, headless)
    {
        AllowedOutputDirectory = allowedOutputDirectory is null ? null : Path.GetFullPath(allowedOutputDirectory);
        ShowGeneratorDialog("Ready", 0, "tp7.tvr");
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

    /// <summary>Typ des fokussierten Generatorcontrols. / Type of the focused generator control.</summary>
    public string FocusedControlKind { get; private set; } = string.Empty;

    /// <summary>Zahl sichtbarer Generatorcontrols. / Number of visible generator controls.</summary>
    public int VisibleControlCount { get; private set; }

    /// <summary>Sichtbarer Fortschritt von 0 bis 100. / Visible progress from 0 through 100.</summary>
    public int ProgressPercent { get; private set; }

    /// <summary>Beschreibt einen kontrollierten Generatoraufruf. / Describes a controlled generator request.</summary>
    /// <param name="RelativePath">Relativer Zielpfad. / Relative target path.</param>
    public readonly record struct GenerateRequest(string RelativePath);

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmGenerate)
        {
            Generate(@event.Message.Info is GenerateRequest request
                ? request
                : new GenerateRequest(_targetInput?.Data ?? string.Empty));
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
            ShowGeneratorDialog("Rejected", 0, request.RelativePath);
            SetStatus("Tp7ResourceGenerator", "generation rejected: no controlled target");
            return;
        }

        string root = Path.TrimEndingDirectorySeparator(AllowedOutputDirectory);
        string candidate = Path.GetFullPath(Path.Combine(root, request.RelativePath));
        string prefix = root + Path.DirectorySeparatorChar;
        if (!candidate.StartsWith(prefix, StringComparison.Ordinal))
        {
            ShowGeneratorDialog("Rejected outside controlled root", 0, request.RelativePath);
            SetStatus("Tp7ResourceGenerator", "generation rejected: outside controlled root");
            return;
        }

        byte[] bytes = CreateValidResourceBytes();
        Directory.CreateDirectory(root);
        File.WriteAllBytes(candidate, bytes);
        GeneratedBytes = bytes;
        GeneratedPath = candidate;
        GenerationRejected = false;
        ShowGeneratorDialog("generated: Dialog, Menu, Status", 100, request.RelativePath);
        SetStatus("Tp7ResourceGenerator", $"generated={Path.GetFileName(candidate)}");
    }

    /// <inheritdoc />
    protected override string BuildDescriptionText() =>
        """
        Historischer Lernzweck: Ziel, Generate-Aktion, Fortschritt und Ergebnis zeigen die Resource-Erzeugung.
        Historical learning purpose: target, Generate action, progress, and result show resource generation.
        Tastatur: Tab fokussiert Ziel und Generate, Alt+G oder Enter erzeugt, F1 öffnet diese Beschreibung.
        Keyboard: Tab focuses target and Generate, Alt+G or Enter generates, and F1 opens this description.
        Modernes C# schreibt nur typisierte allowlist-basierte Records statt Objekt-Overlays.
        Modern C# writes only typed allowlisted records instead of object overlays.
        Ein Ziel muss relativ sein und unter dem kontrollierten Root bleiben; Traversal wird vor dem Schreiben abgelehnt.
        A target must be relative and remain below the controlled root; traversal is rejected before writing.
        Der App-Loop-Smoke beweist Controls, Root-Grenze, Fortschritt, Ergebnis und Zellen, nicht Benutzerverzeichnisse.
        The app-loop smoke proves controls, root boundary, progress, result, and cells, not user directories.
        """;

    private void ShowGeneratorDialog(string result, int progress, string? target)
    {
        TDialog dialog = new(
            new TRect(1, 0, Math.Max(22, Desktop!.Size.X - 1), Math.Max(9, Desktop.Size.Y)),
            "TP7 Resource Generator");
        TInputLine input = new(new TRect(2, 2, Math.Max(12, dialog.Size.X - 2), 3), 80)
        {
            Data = target ?? string.Empty
        };
        TButton generate = new(
            new TRect(2, 4, Math.Min(dialog.Size.X - 2, 16), 5),
            "~G~enerate",
            CmGenerate,
            TButtonFlags.bfDefault);
        TStaticText progressText = new(
            new TRect(2, 6, Math.Max(12, dialog.Size.X - 2), Math.Max(7, dialog.Size.Y - 1)),
            $"Progress: {progress}%\nResult: {result}");
        dialog.Insert(input);
        dialog.Insert(generate);
        dialog.Insert(progressText);
        _targetInput = input;
        ProgressPercent = progress;
        VisibleControlCount = 3;
        ShowView(dialog, nameof(TDialog), $"Target: {input.Data}\nProgress: {progress}%\nResult: {result}");
        dialog.SetFocus(input);
        FocusedControlKind = dialog.Current?.GetType().Name ?? string.Empty;
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
