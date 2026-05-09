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
    private static readonly HashSet<string> KnownFixtureNames = new(StringComparer.Ordinal)
    {
        "valid.tvdialog",
        "malformed.tvdialog",
        "incomplete.tvdialog",
        "duplicate-control.tvdialog",
        "invalid-navigation.tvdialog"
    };

    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public DlgDsnApp(TRect bounds, bool headless = false) : base(bounds) => _headless = headless;

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
        return $"dlgdsn: rendered {dialog.Title}";
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
            return "dlgdsn: rejected fixture-name";
        }

        string marker = File.ReadAllText(FixturePath(safeFileName)).Trim();
        return marker switch
        {
            "malformed" => RejectMalformed(),
            "incomplete" => RejectDescription("incomplete", new DialogDescription("incomplete", 1, string.Empty, [], [], [])),
            "duplicate-control" => RejectDescription("duplicate-control", new DialogDescription(
                "duplicate",
                1,
                "Duplicate",
                [
                    new DialogControlDescription("name", DialogControlRoles.InputLine, "Name"),
                    new DialogControlDescription("name", DialogControlRoles.Button, "OK")
                ],
                ["name"],
                [new DialogCommandBinding(ShellCommandIds.cmOK, "name", "confirm", "Enter")])),
            "invalid-navigation" => RejectDescription("invalid-navigation", new DialogDescription(
                "invalid-navigation",
                1,
                "Invalid Navigation",
                [new DialogControlDescription("name", DialogControlRoles.InputLine, "Name")],
                ["missing"],
                [new DialogCommandBinding(ShellCommandIds.cmOK, "name", "confirm", "Enter")])),
            _ => "dlgdsn: rejected unknown"
        };
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
        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }
}
