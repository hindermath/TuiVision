// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;
using System.Reflection;
using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Examples.FormTransaction;

/// <summary>
/// Demonstriert das additive Transactional Form Model an Kunde und Adresse.
///
/// Demonstrates the additive Transactional Form Model with customer and address data.
/// </summary>
public sealed class FormTransactionApp : TApplication
{
    /// <summary>Ändert alle sichtbaren Eingaben. / Edits all visible inputs.</summary>
    public const ushort CmEdit = 24201;
    /// <summary>Prüft und akzeptiert nach In-memory-Persistenz. / Submits and accepts after in-memory persistence.</summary>
    public const ushort CmSubmitAccept = 24202;
    /// <summary>Verwirft die aktuelle Transaktion. / Rejects the current transaction.</summary>
    public const ushort CmReject = 24203;
    /// <summary>Erzeugt einen synchronen Validierungsfehler. / Produces a synchronous validation failure.</summary>
    public const ushort CmInvalid = 24204;
    /// <summary>Demonstriert einen abgebrochenen Submit. / Demonstrates a cancelled submit.</summary>
    public const ushort CmCancel = 24205;
    /// <summary>Demonstriert ein veraltetes Async-Ergebnis. / Demonstrates a stale async result.</summary>
    public const ushort CmStale = 24206;
    /// <summary>Zeigt die zweisprachige Beschreibung. / Shows the bilingual description.</summary>
    public const ushort CmDescription = 24207;

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly Dictionary<string, object?> _persistedValues = new(StringComparer.Ordinal);
    private bool _quitIssued;
    private ValidationMode _validationMode;

    /// <summary>Erstellt die sichtbare, vollständig lokale Lerndemo. / Creates the visible, fully local learning demo.</summary>
    /// <param name="bounds">Die Bildschirmgrenzen. / The screen bounds.</param>
    /// <param name="headless">Ob ein deterministisches Eventskript verwendet wird. / Whether a deterministic event script is used.</param>
    public FormTransactionApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        Model = new CustomerModel
        {
            Name = "Ada",
            Credit = 12.5m,
            Address = new AddressModel { City = "London" }
        };

        CultureInfo culture = CultureInfo.GetCultureInfo("de-DE");
        FormValueConverter<string, decimal> decimalConverter = new(
            (value, selectedCulture) => FormConversionResult<string>.Success(value.ToString("0.00", selectedCulture)),
            (value, selectedCulture) => decimal.TryParse(value, NumberStyles.Number, selectedCulture, out decimal parsed)
                ? FormConversionResult<decimal>.Success(parsed)
                : FormConversionResult<decimal>.Failure("decimal", "Credit must use a decimal number."));

        NameField = FormField<string>.FromProperty("Name", Model, item => item.Name)
            .AddValidator(value => string.IsNullOrWhiteSpace(value)
                ? new FormValidationError("required", "Name is required.")
                : null)
            .AddAsyncValidator(ValidateAvailabilityAsync);
        CreditField = FormField<string>.FromProperty(
            "Credit", Model, item => item.Credit, decimalConverter, culture)
            .AddValidator(value => string.IsNullOrWhiteSpace(value)
                ? new FormValidationError("required", "Credit is required.")
                : null);
        CityField = FormField<string>.FromProperty("City", Model.Address, item => item.City)
            .AddValidator(value => string.IsNullOrWhiteSpace(value)
                ? new FormValidationError("required", "City is required.")
                : null);

        Session = new FormSession("Customer");
        Session.AddField(NameField);
        Session.AddField(CreditField);
        FormSession address = new("Address");
        address.AddField(CityField);
        Session.AddChild(address);

        Definition = LoadDefinition();
        CreateRegistry().Resolve(Definition);

        TRect region = MainRegion(66, 16);
        FormWindow = new TWindow("Customer form transaction", region.A.X, region.A.Y, region.Width, region.Height);
        FormWindow.Insert(new TStaticText(
            new TRect(2, 2, Math.Max(3, region.Width - 2), 4),
            "Edit locally, submit a snapshot, persist, then accept. / Lokal bearbeiten, Snapshot pruefen, persistieren, dann akzeptieren."));
        FormWindow.Insert(new TStaticText(new TRect(2, 5, 16, 6), "Name:"));
        NameInput = new TInputLine(new TRect(16, 5, Math.Max(22, region.Width - 3), 6), 40);
        FormWindow.Insert(NameInput);
        FormWindow.Insert(new TStaticText(new TRect(2, 7, 16, 8), "Credit / Betrag:"));
        CreditInput = new TInputLine(new TRect(16, 7, Math.Max(22, region.Width - 3), 8), 20);
        FormWindow.Insert(CreditInput);
        FormWindow.Insert(new TStaticText(new TRect(2, 9, 16, 10), "Address.City:"));
        CityInput = new TInputLine(new TRect(16, 9, Math.Max(22, region.Width - 3), 10), 40);
        FormWindow.Insert(CityInput);
        FormWindow.Insert(new TStaticText(
            new TRect(2, 12, Math.Max(3, region.Width - 2), 14),
            "Commands: Edit | Submit+Accept | Reject | Invalid | Cancel | Stale"));

        Session.AttachAdapter(new FormInputLineAdapter(NameInput, NameField));
        Session.AttachAdapter(new FormInputLineAdapter(CreditInput, CreditField));
        Session.AttachAdapter(new FormInputLineAdapter(CityInput, CityField));
        Desktop!.Insert(FormWindow);
        Desktop.SetFocus(FormWindow);
        SetVisible(FormWindow, "TWindow");
        SetStatus("ready; embedded JSON resolved; Help -> Description");
    }

    /// <summary>Die gebundene Kundeninstanz. / The bound customer instance.</summary>
    public CustomerModel Model { get; }

    /// <summary>Die atomare Root-Session. / The atomic root session.</summary>
    public FormSession Session { get; }

    /// <summary>Das Namensfeld. / The name field.</summary>
    public FormField<string> NameField { get; }

    /// <summary>Das Betragsfeld mit kultur-explizitem Konverter. / The credit field with explicit-culture converter.</summary>
    public FormField<string> CreditField { get; }

    /// <summary>Das Feld der verschachtelten Adresse. / The nested address field.</summary>
    public FormField<string> CityField { get; }

    /// <summary>Die geladene deklarative Semantik. / The loaded declarative semantics.</summary>
    public TFormSemanticDocument Definition { get; }

    /// <summary>Das sichtbare Formularfenster. / The visible form window.</summary>
    public TWindow FormWindow { get; }

    /// <summary>Die sichtbare Namenseingabe. / The visible name input.</summary>
    public TInputLine NameInput { get; }

    /// <summary>Die sichtbare Betragseingabe. / The visible credit input.</summary>
    public TInputLine CreditInput { get; }

    /// <summary>Die sichtbare Stadteingabe. / The visible city input.</summary>
    public TInputLine CityInput { get; }

    /// <summary>Der letzte sichtbare Status. / The latest visible status.</summary>
    public string LastStatusMessage { get; private set; } = string.Empty;

    /// <summary>Der letzte sichtbare View-Typ. / The latest visible view kind.</summary>
    public string LastVisibleComponentKind { get; private set; } = string.Empty;

    /// <summary>Die letzte sichtbare Proof-Region. / The latest visible proof region.</summary>
    public TRect LastVisibleRegion { get; private set; }

    /// <summary>Das letzte Submit-Ergebnis. / The latest submit result.</summary>
    public FormSubmitStatus? LastSubmitStatus { get; private set; }

    /// <summary>Ob Cancellation sichtbar beobachtet wurde. / Whether cancellation was visibly observed.</summary>
    public bool CancellationObserved { get; private set; }

    /// <summary>Ob ein veraltetes Ergebnis sichtbar beobachtet wurde. / Whether a stale result was visibly observed.</summary>
    public bool StaleObserved { get; private set; }

    /// <summary>Die nach erfolgreichem Submit in-memory persistierten Werte. / Values persisted in memory after successful submit.</summary>
    public IReadOnlyDictionary<string, object?> PersistedValues => _persistedValues;

    /// <summary>Die zweisprachige Beschreibung. / The bilingual description.</summary>
    public string DescriptionText =>
        "FormTransaction description: Felder bleiben lokal, Submit prueft einen stabilen Snapshot, externe Persistenz kommt vor Accept. / " +
        "Fields remain local, submit validates a stable snapshot, and external persistence precedes accept.";

    /// <summary>Fügt deterministische App-Loop-Ereignisse hinzu. / Adds deterministic app-loop events.</summary>
    /// <param name="events">Die Ereignisse. / The events.</param>
    public void QueueEvents(IEnumerable<TEvent> events)
    {
        foreach (TEvent @event in events)
        {
            _scriptedEvents.Enqueue(@event);
        }
    }

    /// <inheritdoc />
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && _scriptedEvents.Count > 0)
        {
            @event = _scriptedEvents.Dequeue();
            return;
        }

        if (_headless && !_quitIssued)
        {
            _quitIssued = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds) => new(bounds)
    {
        Menu = new TMenuItem(
            "~F~orm",
            0,
            new TMenuItem(
                "~H~elp",
                0,
                new TMenuItem("E~x~it", ShellCommandIds.cmQuit),
                new TMenuItem("~D~escription", CmDescription)),
            new TMenuItem("~E~dit", CmEdit, null,
                new TMenuItem("~S~ubmit + Accept", CmSubmitAccept, null,
                    new TMenuItem("~R~eject", CmReject, null,
                        new TMenuItem("~I~nvalid", CmInvalid, null,
                            new TMenuItem("~C~ancel", CmCancel, null,
                                new TMenuItem("S~t~ale", CmStale)))))))
    };

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new FormTransactionStatusLine(bounds, "FormTransaction: ready | Help -> Description | ^Q Quit");

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What != TEventKind.Command)
        {
            base.HandleEvent(@event);
            return;
        }

        switch (@event.Message.Command)
        {
            case CmEdit:
                NameInput.Data = "Augusta";
                CreditInput.Data = "13,75";
                CityInput.Data = "Paris";
                FormChangeSet changes = Session.GetChangeSet();
                SetStatus($"dirty={Session.IsModified}; changes={changes.Changes.Count}; model={Model.Name}");
                break;
            case CmSubmitAccept:
                SubmitAndAccept();
                break;
            case CmReject:
                Session.RejectChanges();
                SetStatus($"rejected; dirty={Session.IsModified}; model={Model.Name}/{Model.Address.City}");
                break;
            case CmInvalid:
                NameInput.Data = string.Empty;
                RunSubmit();
                break;
            case CmCancel:
                RunCancelledSubmit();
                break;
            case CmStale:
                _validationMode = ValidationMode.Stale;
                NameInput.Data = "Drift";
                RunSubmit();
                _validationMode = ValidationMode.Normal;
                break;
            case CmDescription:
                ShowDescription();
                break;
            default:
                base.HandleEvent(@event);
                return;
        }

        @event.Clear();
    }

    private void SubmitAndAccept()
    {
        _validationMode = ValidationMode.Normal;
        FormSubmitResult result = Session.SubmitAsync().GetAwaiter().GetResult();
        LastSubmitStatus = result.Status;
        if (result.Status != FormSubmitStatus.Success)
        {
            SetStatus($"submit={result.Status}; errors={result.Errors.Count}");
            return;
        }

        // Die Demo bildet die externe Persistenz bewusst vor Accept ab.
        // The demo deliberately models external persistence before Accept.
        foreach (FormChange change in result.ChangeSet.Changes)
        {
            _persistedValues[change.Name] = change.CurrentValue;
        }

        Session.AcceptChanges();
        SetStatus($"accepted; persisted={_persistedValues.Count}; model={Model.Name}/{Model.Address.City}");
    }

    private void RunSubmit()
    {
        FormSubmitResult result = Session.SubmitAsync().GetAwaiter().GetResult();
        LastSubmitStatus = result.Status;
        StaleObserved |= result.Status == FormSubmitStatus.Stale;
        SetStatus($"submit={result.Status}; errors={result.Errors.Count}; dirty={Session.IsModified}");
    }

    private void RunCancelledSubmit()
    {
        _validationMode = ValidationMode.Normal;
        using CancellationTokenSource source = new();
        source.Cancel();
        try
        {
            _ = Session.SubmitAsync(source.Token).GetAwaiter().GetResult();
        }
        catch (OperationCanceledException)
        {
            CancellationObserved = true;
            SetStatus("submit=Cancelled; model and baseline unchanged");
        }
    }

    private async ValueTask<FormValidationError?> ValidateAvailabilityAsync(
        string value,
        CancellationToken cancellationToken)
    {
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();
        if (_validationMode == ValidationMode.Stale)
        {
            NameField.Value = $"{value}*";
        }

        return string.Equals(value, "reserved", StringComparison.OrdinalIgnoreCase)
            ? new FormValidationError("available", "The selected name is unavailable.")
            : null;
    }

    private void ShowDescription()
    {
        TRect region = MainRegion(66, 13);
        TWindow window = new("FormTransaction Description", region.A.X, region.A.Y, region.Width, region.Height);
        window.Insert(new TStaticText(
            new TRect(2, 2, Math.Max(3, region.Width - 2), Math.Max(3, region.Height - 1)),
            DescriptionText));
        Desktop!.Insert(window);
        Desktop.SetFocus(window);
        SetVisible(window, "TWindow");
        SetStatus("description visible; Help -> Description");
    }

    private void SetStatus(string state)
    {
        LastStatusMessage = $"FormTransaction: {state} | Help -> Description | ^Q Quit";
        if (StatusLine is FormTransactionStatusLine status)
        {
            status.SetMessage(LastStatusMessage);
        }
    }

    private void SetVisible(TView view, string kind)
    {
        LastVisibleComponentKind = kind;
        TRect bounds = view.GetBounds();
        LastVisibleRegion = new TRect(
            Desktop!.Origin.X + bounds.A.X,
            Desktop.Origin.Y + bounds.A.Y,
            Desktop.Origin.X + bounds.B.X,
            Desktop.Origin.Y + bounds.B.Y);
    }

    private TRect MainRegion(int width, int height)
    {
        int actualWidth = Math.Clamp(width, 12, Math.Max(12, Desktop!.Size.X - 4));
        int actualHeight = Math.Clamp(height, 6, Math.Max(6, Desktop.Size.Y - 3));
        return new TRect(2, 1, 2 + actualWidth, 1 + actualHeight);
    }

    private static TFormSemanticDocument LoadDefinition()
    {
        Assembly assembly = typeof(FormTransactionApp).Assembly;
        string resourceName = assembly.GetManifestResourceNames().Single(
            name => name.EndsWith("form-transaction.json", StringComparison.Ordinal));
        using Stream stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidDataException("Embedded form semantics are unavailable.");
        using StreamReader reader = new(stream);
        return TFormSemanticJson.Deserialize(reader.ReadToEnd());
    }

    private static FormRuntimeRegistry CreateRegistry()
    {
        FormRuntimeRegistry registry = new();
        registry.RegisterType("text", typeof(string));
        registry.RegisterControl("input", "text", new object());
        registry.RegisterBinding("customer-name", "text", new object());
        registry.RegisterBinding("customer-credit", "text", new object());
        registry.RegisterBinding("address-city", "text", new object());
        registry.RegisterConverter("identity", "text", new object());
        registry.RegisterConverter("decimal-de", "text", new object());
        registry.RegisterValidator("required", "text", new object());
        registry.RegisterValidator("available", "text", new object());
        return registry;
    }

    private enum ValidationMode
    {
        Normal,
        Stale
    }
}

/// <summary>Ein kleines POCO für die verzögerte Bindung. / A small POCO for deferred binding.</summary>
public sealed class CustomerModel
{
    /// <summary>Der Kundenname. / The customer name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Der explizit konvertierte Betrag. / The explicitly converted credit value.</summary>
    public decimal Credit { get; set; }

    /// <summary>Die verschachtelte Adresse. / The nested address.</summary>
    public AddressModel Address { get; set; } = new();
}

/// <summary>Ein verschachteltes Adress-POCO. / A nested address POCO.</summary>
public sealed class AddressModel
{
    /// <summary>Die Stadt. / The city.</summary>
    public string City { get; set; } = string.Empty;
}

internal sealed class FormTransactionStatusLine : TStatusLine
{
    public FormTransactionStatusLine(TRect bounds, string message) : base(bounds) => Message = message;

    private string Message { get; set; }

    public void SetMessage(string message)
    {
        Message = message;
        DrawView();
    }

    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0)
        {
            return;
        }

        for (int x = 0; x < Size.X; x++)
        {
            buffer.TrySetCell(Origin.X + x, Origin.Y, new TConsoleCell(' ', ConsoleColor.Black, ConsoleColor.Cyan));
        }

        string text = Message.Length <= Size.X ? Message : Message[..Size.X];
        buffer.WriteText(Origin.X, Origin.Y, text.AsSpan(), ConsoleColor.Yellow, ConsoleColor.Cyan);
    }
}
