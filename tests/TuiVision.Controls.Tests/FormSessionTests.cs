// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;
using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Prüft die vollständigen Feld-, Binding-, Async- und Child-Verträge.
///
/// Verifies the complete field, binding, async, and child contracts.
/// </summary>
[TestClass]
public sealed class FormSessionTests
{
    /// <summary>Prüft Dirty, Equality, ChangeSet, Accept und Reject. / Verifies dirty, equality, change set, accept, and reject.</summary>
    [TestMethod]
    public void Test_FieldSession_TracksAndMovesBaselineDeterministically()
    {
        FormField<string> name = new("Name", "Ada", StringComparer.OrdinalIgnoreCase);
        FormField<int> age = new("Age", 36);
        FormSession session = new("Customer");
        session.AddField(name);
        session.AddField(age);

        name.Value = "ADA";
        age.Value = 37;

        Assert.IsFalse(name.IsModified);
        Assert.IsTrue(age.IsModified);
        Assert.AreEqual(1, session.GetChangeSet().Changes.Count);
        Assert.AreEqual("Age", session.GetChangeSet().Changes[0].Name);

        session.AcceptChanges();
        Assert.AreEqual(37, age.OriginalValue);
        age.Value = 38;
        session.RejectChanges();
        Assert.AreEqual(37, age.Value);
        Assert.IsFalse(session.IsModified);
    }

    /// <summary>Prüft Submit ohne Commit und Accept mit POCO-Binding. / Verifies submit without commit and accept with POCO binding.</summary>
    [TestMethod]
    public async Task Test_Submit_ValidatesSnapshotButAcceptUpdatesModel()
    {
        Customer model = new() { Name = "Ada" };
        FormField<string> name = FormField<string>.FromProperty("Name", model, item => item.Name);
        name.AddValidator(value => string.IsNullOrWhiteSpace(value)
            ? new FormValidationError("required", "Name is required.")
            : null);
        FormSession session = new("Customer");
        session.AddField(name);
        name.Value = "Augusta";

        FormSubmitResult submit = await session.SubmitAsync();

        Assert.AreEqual(FormSubmitStatus.Success, submit.Status);
        Assert.AreEqual("Ada", model.Name);
        Assert.AreEqual("Ada", name.OriginalValue);
        session.AcceptChanges();
        Assert.AreEqual("Augusta", model.Name);
        Assert.AreEqual("Augusta", name.OriginalValue);

        name.Value = "";
        FormSubmitResult invalid = await session.SubmitAsync();
        Assert.AreEqual(FormSubmitStatus.ValidationFailed, invalid.Status);
        Assert.AreEqual("required", invalid.Errors[0].Code);
    }

    /// <summary>Prüft bestmögliches Setter-Rollback. / Verifies best-effort setter rollback.</summary>
    [TestMethod]
    public void Test_Accept_SetterFailureRollsBackInReverseWithoutMovingBaselines()
    {
        RollbackModel model = new() { First = "one", Second = "two" };
        model.Log.Clear();
        FormField<string> first = FormField<string>.FromProperty("First", model, item => item.First);
        FormField<string> second = FormField<string>.FromProperty("Second", model, item => item.Second);
        FormSession session = new("Rollback");
        session.AddField(first);
        session.AddField(second);
        first.Value = "changed";
        second.Value = "throw";

        FormBindingCommitException error = Assert.ThrowsExactly<FormBindingCommitException>(session.AcceptChanges);

        Assert.AreEqual("one", model.First);
        Assert.AreEqual("two", model.Second);
        Assert.AreEqual("one", first.OriginalValue);
        Assert.AreEqual("two", second.OriginalValue);
        Assert.AreEqual("Second", error.FieldName);
        CollectionAssert.AreEqual(
            new[] { "set:First=changed", "set:Second=throw", "set:Second=two", "set:First=one" },
            model.Log);
    }

    /// <summary>Prüft kultur-explizite bidirektionale Konvertierung. / Verifies explicit-culture bidirectional conversion.</summary>
    [TestMethod]
    public void Test_Converter_UsesExplicitCultureAndReportsFailure()
    {
        Customer model = new() { Credit = 12.5m };
        CultureInfo culture = CultureInfo.GetCultureInfo("de-DE");
        FormValueConverter<string, decimal> converter = new(
            (value, selectedCulture) => FormConversionResult<string>.Success(value.ToString("0.00", selectedCulture)),
            (value, selectedCulture) => decimal.TryParse(value, NumberStyles.Number, selectedCulture, out decimal parsed)
                ? FormConversionResult<decimal>.Success(parsed)
                : FormConversionResult<decimal>.Failure("decimal", "Invalid decimal value."));
        FormField<string> credit = FormField<string>.FromProperty(
            "Credit", model, item => item.Credit, converter, culture);
        FormSession session = new("Customer");
        session.AddField(credit);

        Assert.AreEqual("12,50", credit.Value);
        credit.Value = "13,75";
        session.AcceptChanges();
        Assert.AreEqual(13.75m, model.Credit);

        credit.Value = "not-a-number";
        FormBindingCommitException error = Assert.ThrowsExactly<FormBindingCommitException>(session.AcceptChanges);
        Assert.AreEqual("decimal", error.ValidationError?.Code);
        Assert.AreEqual(13.75m, model.Credit);
    }

    /// <summary>Prüft Stale, Cancellation und parallele Submit-Ablehnung. / Verifies stale, cancellation, and concurrent-submit rejection.</summary>
    [TestMethod]
    public async Task Test_SubmitAsync_HandlesDriftCancellationAndConcurrency()
    {
        FormField<string> field = new("Name", "Ada");
        TaskCompletionSource<bool> entered = new(TaskCreationOptions.RunContinuationsAsynchronously);
        TaskCompletionSource<bool> release = new(TaskCreationOptions.RunContinuationsAsynchronously);
        field.AddAsyncValidator(async (value, token) =>
        {
            entered.SetResult(true);
            await release.Task.WaitAsync(token);
            return null;
        });
        FormSession session = new("Customer");
        session.AddField(field);
        field.Value = "Augusta";

        Task<FormSubmitResult> first = session.SubmitAsync();
        await entered.Task;
        await Assert.ThrowsExactlyAsync<InvalidOperationException>(() => session.SubmitAsync());
        field.Value = "Countess";
        release.SetResult(true);
        Assert.AreEqual(FormSubmitStatus.Stale, (await first).Status);

        FormField<string> cancellable = new("Name", "Ada");
        cancellable.AddAsyncValidator(async (_, token) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, token);
            return null;
        });
        FormSession cancelling = new("Cancel");
        cancelling.AddField(cancellable);
        using CancellationTokenSource source = new();
        Task<FormSubmitResult> cancelled = cancelling.SubmitAsync(source.Token);
        source.Cancel();
        await Assert.ThrowsExactlyAsync<TaskCanceledException>(async () => await cancelled);
    }

    /// <summary>Prüft atomare Child-Sessions, Cycle und Adapter. / Verifies atomic child sessions, cycles, and adapter behavior.</summary>
    [TestMethod]
    public async Task Test_ChildrenAndInputAdapter_AreRecursiveAndOptIn()
    {
        FormField<string> city = new("City", "London");
        FormSession address = new("Address");
        address.AddField(city);
        FormSession customer = new("Customer");
        customer.AddChild(address);
        TInputLine input = new(new TRect(0, 0, 20, 1), 20);
        customer.AttachAdapter(new FormInputLineAdapter(input, city));

        input.Data = "Paris";
        FormSubmitResult result = await customer.SubmitAsync();

        Assert.AreEqual(FormSubmitStatus.Success, result.Status);
        Assert.AreEqual("Address.City", result.ChangeSet.Changes[0].Name);
        customer.RejectChanges();
        Assert.AreEqual("London", input.Data);
        Assert.ThrowsExactly<InvalidOperationException>(() => address.AddChild(customer));
        Assert.ThrowsExactly<InvalidOperationException>(() => new FormSession("Other").AddChild(address));
    }

    /// <summary>Prüft Async-Fehlerpublikation und atomaren Root-Besitz. / Verifies async error publication and atomic root ownership.</summary>
    [TestMethod]
    public async Task Test_AsyncValidationAndOwnership_AreDeterministic()
    {
        FormField<string> field = new("Name", "Ada");
        field.AddAsyncValidator((value, _) => ValueTask.FromResult<FormValidationError?>(
            value == "reserved" ? new FormValidationError("available", "Name is unavailable.") : null));
        FormSession child = new("Child");
        child.AddField(field);
        FormSession root = new("Root");
        root.AddChild(child);
        field.Value = "reserved";

        FormSubmitResult result = await root.SubmitAsync();

        Assert.AreEqual(FormSubmitStatus.ValidationFailed, result.Status);
        Assert.AreEqual("Child.Name", result.Errors[0].FieldName);
        Assert.AreEqual("available", field.ValidationErrors[0].Code);
        await Assert.ThrowsExactlyAsync<InvalidOperationException>(() => child.SubmitAsync());
        Assert.ThrowsExactly<InvalidOperationException>(() => new FormSession("Other").AddField(field));
        Assert.ThrowsExactly<InvalidOperationException>(field.AcceptChanges);
        Assert.ThrowsExactly<InvalidOperationException>(field.RejectChanges);
    }

    /// <summary>Prüft direkte Property-Ausdrücke und unveränderte ordinary Controls. / Verifies direct property expressions and unchanged ordinary controls.</summary>
    [TestMethod]
    public async Task Test_BindingExpressionsAndOrdinaryInputLine_StayExplicit()
    {
        NestedCustomer model = new();
        Assert.ThrowsExactly<ArgumentException>(() =>
            FormField<string>.FromProperty("City", model, item => item.Address.City));

        TInputLine ordinary = new(new TRect(0, 0, 20, 1), 20) { Data = "ordinary" };
        FormField<string> field = new("Name", "Ada");
        FormSession session = new("Customer");
        session.AddField(field);
        field.Value = "Augusta";

        FormSubmitResult result = await session.SubmitAsync();
        session.AcceptChanges();

        Assert.AreEqual(FormSubmitStatus.Success, result.Status);
        Assert.AreEqual("ordinary", ordinary.Data);
    }

    /// <summary>Prüft sichere Registry-Schlüssel und Typkonflikte. / Verifies safe registry keys and type conflicts.</summary>
    [TestMethod]
    public void Test_RuntimeRegistry_RejectsUnknownAndConflictingKeysAtomically()
    {
        FormRuntimeRegistry registry = CreateRegistry();
        TFormSemanticDocument document = TFormSemanticJson.Deserialize(ValidJson);

        ResolvedFormSemanticDocument resolved = registry.Resolve(document);
        Assert.AreSame(document, resolved.Source);
        Assert.AreEqual("customer", resolved.RootForm.Key);
        Assert.AreEqual(typeof(string), resolved.RootForm.Fields[0].FieldType);
        Assert.AreEqual(1, resolved.RootForm.Fields[0].Validators.Count);

        FormRuntimeRegistry missing = CreateRegistry(includeRequired: false);
        Assert.ThrowsExactly<InvalidDataException>(() => missing.Resolve(document));

        FormRuntimeRegistry conflict = CreateRegistry();
        conflict.RegisterType("number", typeof(int));
        conflict.RegisterValidator("required", "number", new object(), replace: true);
        Assert.ThrowsExactly<InvalidDataException>(() => conflict.Resolve(document));
    }

    private static FormRuntimeRegistry CreateRegistry(bool includeRequired = true)
    {
        FormRuntimeRegistry registry = new();
        registry.RegisterType("text", typeof(string));
        registry.RegisterControl("input", "text", new object());
        registry.RegisterBinding("customer-name", "text", new object());
        registry.RegisterConverter("identity", "text", new object());
        if (includeRequired)
        {
            registry.RegisterValidator("required", "text", new object());
        }

        return registry;
    }

    private const string ValidJson = """
        {
          "version": 1,
          "form": "customer",
          "forms": [
            {
              "form": "customer",
              "fields": [
                {
                  "field": "name",
                  "control": "input",
                  "type": "text",
                  "binding": "customer-name",
                  "converter": "identity",
                  "validators": ["required"]
                }
              ],
              "children": []
            }
          ]
        }
        """;

    private sealed class Customer
    {
        public string Name { get; set; } = string.Empty;

        public decimal Credit { get; set; }
    }

    private sealed class RollbackModel
    {
        private string _first = string.Empty;
        private string _second = string.Empty;

        public List<string> Log { get; } = [];

        public string First
        {
            get => _first;
            set
            {
                Log.Add($"set:First={value}");
                _first = value;
            }
        }

        public string Second
        {
            get => _second;
            set
            {
                Log.Add($"set:Second={value}");
                if (value == "throw")
                {
                    throw new InvalidOperationException("Synthetic setter failure.");
                }

                _second = value;
            }
        }
    }

    private sealed class NestedCustomer
    {
        public Address Address { get; } = new();
    }

    private sealed class Address
    {
        public string City { get; set; } = string.Empty;
    }
}
