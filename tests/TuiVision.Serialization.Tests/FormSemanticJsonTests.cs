// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Serialization;

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Prüft den geschlossenen JSON-Vertrag für deklarative Formsemantik.
///
/// Verifies the closed JSON contract for declarative form semantics.
/// </summary>
[TestClass]
public sealed class FormSemanticJsonTests
{
    /// <summary>Prüft den deterministischen Roundtrip. / Verifies the deterministic round trip.</summary>
    [TestMethod]
    public void Test_FormSemanticJson_RoundTripsDeterministically()
    {
        TFormSemanticDocument first = TFormSemanticJson.Deserialize(ValidJson);
        string json = TFormSemanticJson.Serialize(first);
        TFormSemanticDocument second = TFormSemanticJson.Deserialize(json);

        Assert.AreEqual(1, second.Version);
        Assert.AreEqual("customer", second.RootForm);
        Assert.AreEqual(2, second.Forms.Count);
        Assert.AreEqual(json, TFormSemanticJson.Serialize(second));
    }

    /// <summary>Prüft atomare Ablehnung fehlerhafter Definitionen. / Verifies atomic rejection of malformed definitions.</summary>
    [TestMethod]
    [DataRow("{\"version\":2,\"form\":\"a\",\"forms\":[]}")]
    [DataRow("{\"version\":1,\"form\":\"a\",\"unknown\":true,\"forms\":[]}")]
    [DataRow("{\"version\":1,\"version\":1,\"form\":\"a\",\"forms\":[]}")]
    [DataRow("{\"version\":1,\"form\":\"missing\",\"forms\":[]}")]
    [DataRow("{\"version\":1,\"form\":\"a\",\"forms\":[{\"form\":\"a\",\"fields\":[],\"children\":[{\"child\":\"b\",\"form\":\"missing\"}]}]}")]
    [DataRow("{\"version\":1,\"form\":\"System.String\",\"forms\":[]}")]
    [DataRow("{\"version\":1,\"form\":\"a\",\"forms\":[{\"form\":\"a\",\"fields\":[{\"field\":\"x\",\"control\":\"c\",\"type\":\"t\",\"binding\":\"b\",\"converter\":\"v\",\"validators\":[\"same\",\"same\"]}],\"children\":[]}]} ")]
    [DataRow("{\"version\":1,\"form\":\"a\",\"forms\":[{\"form\":\"a\",\"fields\":[],\"children\":[]}]} trailing")]
    [DataRow("{\"version\":1,\"form\":\"a\",\"forms\":[{\"form\":\"a\",\"fields\":[],\"children\":[{\"child\":\"b\",\"form\":\"b\"}]},{\"form\":\"b\",\"fields\":[],\"children\":[{\"child\":\"a\",\"form\":\"a\"}]}]}")]
    [DataRow("{\"version\":1,\"form\":\"a\",\"forms\":[{\"form\":\"a\",\"fields\":[{\"field\":\"x\",\"control\":\"c\",\"type\":\"t\",\"binding\":\"b\",\"converter\":\"v\",\"validators\":[]},{\"field\":\"x\",\"control\":\"c\",\"type\":\"t\",\"binding\":\"b\",\"converter\":\"v\",\"validators\":[]}],\"children\":[]}]}")]
    public void Test_FormSemanticJson_RejectsMalformedInput(string json) =>
        Assert.ThrowsExactly<InvalidDataException>(() => TFormSemanticJson.Deserialize(json));

    /// <summary>Prüft Größen-, Tiefen- und eindeutige Child-Besitzgrenzen. / Verifies size, depth, and unique child ownership limits.</summary>
    [TestMethod]
    public void Test_FormSemanticJson_RejectsResourceLimitsAndSharedChildren()
    {
        string oversized = new('x', 262_145);
        Assert.ThrowsExactly<InvalidDataException>(() => TFormSemanticJson.Deserialize(oversized));

        List<TFormSemanticDefinition> deepForms = [];
        for (int index = 0; index < 33; index++)
        {
            string key = $"f{index}";
            TFormSemanticChild[] children = index == 32
                ? []
                : [new TFormSemanticChild($"c{index}", $"f{index + 1}")];
            deepForms.Add(new TFormSemanticDefinition(key, [], children));
        }

        TFormSemanticDocument tooDeep = new(1, "f0", deepForms);
        Assert.ThrowsExactly<InvalidDataException>(() => TFormSemanticJson.Serialize(tooDeep));

        TFormSemanticDocument sharedChild = new(
            1,
            "root",
            [
                new TFormSemanticDefinition(
                    "root",
                    [],
                    [new TFormSemanticChild("left", "leaf"), new TFormSemanticChild("right", "leaf")]),
                new TFormSemanticDefinition("leaf", [], [])
            ]);
        Assert.ThrowsExactly<InvalidDataException>(() => TFormSemanticJson.Serialize(sharedChild));
    }

    private const string ValidJson = """
        {
          "version": 1,
          "form": "customer",
          "forms": [
            {
              "form": "customer",
              "fields": [
                {"field":"name","control":"input","type":"text","binding":"customer-name","converter":"identity","validators":["required"]}
              ],
              "children": [{"child":"address","form":"address"}]
            },
            {
              "form": "address",
              "fields": [
                {"field":"city","control":"input","type":"text","binding":"address-city","converter":"identity","validators":[]}
              ],
              "children": []
            }
          ]
        }
        """;
}
