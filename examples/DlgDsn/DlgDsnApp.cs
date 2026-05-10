// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Examples.DlgDsn;

/// <summary>
/// Dynamisches Dialog-Designer-Beispiel mit Headless-Seam.
///
/// Dynamic dialog designer example with a headless seam.
/// </summary>
public sealed class DlgDsnApp : TApplication
{
    /// <summary>Laden/Rendern-Befehl. / Load/render command.</summary>
    public const ushort CmLoadRender = 12200;

    /// <summary>Aenderungsbefehl. / Change command.</summary>
    public const ushort CmChange = 12201;

    /// <summary>Malformed-Ablehnungsbefehl. / Malformed rejection command.</summary>
    public const ushort CmRejectMalformed = 12202;

    /// <summary>Ungueltige-Beschreibung-Ablehnungsbefehl. / Invalid-description rejection command.</summary>
    public const ushort CmRejectInvalidDescription = 12203;

    private static readonly HashSet<string> KnownFixtureNames = new(StringComparer.Ordinal)
    {
        "valid.tvdialog",
        "malformed.tvdialog",
        "incomplete.tvdialog",
        "duplicate-control.tvdialog",
        "invalid-navigation.tvdialog"
    };

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
    private TStaticText? _visibleView;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public DlgDsnApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        SetVisibleState(
            "DlgDsn: load, render, change, and reject dialog descriptions\n" +
            "Commands use source-controlled fixtures only. Ctrl+Q quits.");
    }

    /// <summary>
    /// Sichtbare Zustandsfolge seit Start.
    ///
    /// Visible state sequence since startup.
    /// </summary>
    public IReadOnlyList<string> VisibleHistory => _visibleHistory;

    /// <summary>
    /// Letzter sichtbarer Textzustand.
    ///
    /// Last visible text state.
    /// </summary>
    public string VisibleText { get; private set; } = string.Empty;

    /// <summary>
    /// Fuegt deterministische Ereignisse fuer den Headless-Lauf hinzu.
    ///
    /// Adds deterministic events for the headless run.
    /// </summary>
    /// <param name="events">Die Ereignisse. / The events.</param>
    public void QueueEvents(IEnumerable<TEvent> events)
    {
        foreach (TEvent @event in events)
        {
            _scriptedEvents.Enqueue(@event);
        }
    }

    /// <summary>
    /// Erstellt die gueltige Beispielbeschreibung.
    ///
    /// Creates the valid example description.
    /// </summary>
    /// <returns>Die Dialogbeschreibung. / The dialog description.</returns>
    public DialogDescription CreateValidDescription() => new(
        "wave2-dialog",
        1,
        "Runtime dialog",
        [
            new DialogControlDescription("name", DialogControlRoles.InputLine, "Name", "Ada"),
            new DialogControlDescription("ok", DialogControlRoles.Button, "OK")
        ],
        ["name", "ok"],
        [new DialogCommandBinding(ShellCommandIds.cmOK, "ok", "confirm", "Enter")]);

    /// <summary>
    /// Laedt eine gueltige Fixture ueber einen Serialization-Roundtrip.
    ///
    /// Loads a valid fixture through a serialization roundtrip.
    /// </summary>
    /// <param name="fileName">Der Fixture-Dateiname. / The fixture file name.</param>
    /// <returns>Die geladene Beschreibung. / The loaded description.</returns>
    public DialogDescription LoadFixture(string fileName)
    {
        string path = FixturePath(fileName);
        string marker = File.ReadAllText(path).Trim();
        if (!string.Equals(marker, "valid", StringComparison.Ordinal))
        {
            throw new InvalidDataException($"Fixture '{fileName}' is not valid.");
        }

        TRecordRegistry registry = new();
        TDialogDescriptionRecord.Register(registry);
        TRecordSerializer serializer = new(registry);
        TDialogDescriptionRecord record = DialogDescriptionPersistenceAdapter.ToRecord(CreateValidDescription());
        byte[] data = serializer.Serialize(PersistedDialogRepresentation.TypeId, record);
        TDialogDescriptionRecord restored = serializer.Deserialize<TDialogDescriptionRecord>(data);
        return DialogDescriptionPersistenceAdapter.FromRecord(restored);
    }

    /// <summary>
    /// Erzeugt einen sichtbaren Runtime-Dialog-Zustand.
    ///
    /// Creates a visible runtime dialog state.
    /// </summary>
    /// <param name="description">Die Beschreibung. / The description.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string RenderDescription(DialogDescription description)
    {
        TDialog dialog = DialogDescriptionFactory.CreateRuntimeDialog(description);
        return SetVisibleState($"dlgdsn: rendered {dialog.Title}");
    }

    /// <summary>
    /// Wendet eine einfache Aenderung am ersten Eingabefeld an.
    ///
    /// Applies a simple change to the first input control.
    /// </summary>
    /// <param name="description">Die Ausgangsbeschreibung. / The source description.</param>
    /// <param name="value">Der neue Wert. / The new value.</param>
    /// <returns>Die geaenderte Beschreibung. / The modified description.</returns>
    public DialogDescription ApplySimpleChange(DialogDescription description, string value)
    {
        DialogControlDescription[] controls = description.Controls
            .Select((control, index) => index == 0
                ? new DialogControlDescription(control.ControlId, control.Role, control.Label, value, control.CanFocus)
                : control)
            .ToArray();

        return new DialogDescription(
            description.DescriptionId,
            description.Version,
            description.Title,
            controls,
            description.NavigationOrder,
            description.CommandBindings);
    }

    /// <summary>
    /// Wendet eine sichtbare Beispielaenderung an.
    ///
    /// Applies a visible sample change.
    /// </summary>
    /// <param name="value">Der neue Wert. / The new value.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ApplyVisibleChange(string value)
    {
        DialogDescription modified = ApplySimpleChange(CreateValidDescription(), value);
        return SetVisibleState($"dlgdsn: changed {modified.Controls[0].ControlId}={modified.Controls[0].InitialValue}");
    }

    /// <summary>
    /// Laedt eine ungueltige Fixture und liefert eine sichtbare Ablehnung.
    ///
    /// Loads an invalid fixture and returns a visible rejection.
    /// </summary>
    /// <param name="fileName">Der Fixture-Dateiname. / The fixture file name.</param>
    /// <returns>Der sichtbare Ablehnungszustand. / The visible rejection state.</returns>
    public string TryLoadFixture(string fileName)
    {
        if (!TryValidateFixtureName(fileName, out string safeFileName))
        {
            return SetVisibleState("dlgdsn: rejected fixture-name");
        }

        string marker = File.ReadAllText(FixturePath(safeFileName)).Trim();
        return marker switch
        {
            "malformed" => SetVisibleState(RejectMalformed()),
            "incomplete" => SetVisibleState(RejectDescription("incomplete", new DialogDescription("incomplete", 1, string.Empty, [], [], []))),
            "duplicate-control" => SetVisibleState(RejectDescription("duplicate-control", new DialogDescription(
                "duplicate",
                1,
                "Duplicate",
                [
                    new DialogControlDescription("name", DialogControlRoles.InputLine, "Name"),
                    new DialogControlDescription("name", DialogControlRoles.Button, "OK")
                ],
                ["name"],
                [new DialogCommandBinding(ShellCommandIds.cmOK, "name", "confirm", "Enter")]))),
            "invalid-navigation" => SetVisibleState(RejectDescription("invalid-navigation", new DialogDescription(
                "invalid-navigation",
                1,
                "Invalid Navigation",
                [new DialogControlDescription("name", DialogControlRoles.InputLine, "Name")],
                ["missing"],
                [new DialogCommandBinding(ShellCommandIds.cmOK, "name", "confirm", "Enter")]))),
            _ => SetVisibleState("dlgdsn: rejected unknown")
        };
    }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem dialogItems =
            new("~L~oad/render", CmLoadRender,
            new TMenuItem("~C~hange", CmChange,
            new TMenuItem("Reject ~m~alformed", CmRejectMalformed,
            new TMenuItem("Reject ~i~nvalid", CmRejectInvalidDescription))));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~D~lgDsn", 0, new TMenuItem("E~x~it", ShellCommandIds.cmQuit), dialogItems)
        };
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmLoadRender:
                    RenderDescription(LoadFixture("valid.tvdialog"));
                    @event.Clear();
                    return;
                case CmChange:
                    ApplyVisibleChange(@event.Message.Info as string ?? "Grace");
                    @event.Clear();
                    return;
                case CmRejectMalformed:
                    TryLoadFixture("malformed.tvdialog");
                    @event.Clear();
                    return;
                case CmRejectInvalidDescription:
                    TryLoadFixture(@event.Message.Info as string ?? "incomplete.tvdialog");
                    @event.Clear();
                    return;
            }
        }

        base.HandleEvent(@event);
    }

    private static string FixturePath(string fileName)
    {
        string safeFileName = ValidateFixtureName(fileName);
        string local = Path.Combine(AppContext.BaseDirectory, "Fixtures", safeFileName);
        if (File.Exists(local))
        {
            return local;
        }

        DirectoryInfo? current = new(AppContext.BaseDirectory);
        while (current is not null)
        {
            string candidate = Path.Combine(current.FullName, "examples", "DlgDsn", "Fixtures", safeFileName);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            current = current.Parent;
        }

        return local;
    }

    private static string ValidateFixtureName(string fileName)
    {
        if (TryValidateFixtureName(fileName, out string safeFileName))
        {
            return safeFileName;
        }

        throw new InvalidDataException($"Fixture name '{fileName}' is not allowed.");
    }

    private static bool TryValidateFixtureName(string fileName, out string safeFileName)
    {
        safeFileName = fileName;
        if (string.IsNullOrWhiteSpace(fileName)
            || Path.IsPathRooted(fileName)
            || !string.Equals(fileName, Path.GetFileName(fileName), StringComparison.Ordinal)
            || !KnownFixtureNames.Contains(fileName))
        {
            safeFileName = string.Empty;
            return false;
        }

        return true;
    }

    private static string RejectMalformed()
    {
        TRecordRegistry registry = new();
        TDialogDescriptionRecord.Register(registry);
        TRecordSerializer serializer = new(registry);
        try
        {
            _ = serializer.Deserialize<TDialogDescriptionRecord>([0x01, 0x02, 0x03]);
        }
        catch (InvalidDataException)
        {
            return "dlgdsn: rejected malformed";
        }

        return "dlgdsn: accepted malformed";
    }

    private static string RejectDescription(string label, DialogDescription description)
    {
        DialogDescriptionValidationResult result = DialogDescriptionValidator.Validate(description);
        return result.IsValid ? $"dlgdsn: accepted {label}" : $"dlgdsn: rejected {label}";
    }

    /// <inheritdoc />
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && _scriptedEvents.Count > 0)
        {
            @event = _scriptedEvents.Dequeue();
            return;
        }

        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    private string SetVisibleState(string text)
    {
        VisibleText = text;
        _visibleHistory.Add(text);
        if (Desktop is null)
        {
            return VisibleText;
        }

        if (_visibleView?.Owner == Desktop)
        {
            Desktop.Remove(_visibleView);
        }

        int width = Math.Max(1, Desktop.Size.X - 2);
        int height = Math.Max(1, Math.Min(Desktop.Size.Y - 2, 6));
        _visibleView = new TStaticText(new TRect(1, 1, 1 + width, 1 + height), VisibleText);
        Desktop.Insert(_visibleView);
        return VisibleText;
    }
}
