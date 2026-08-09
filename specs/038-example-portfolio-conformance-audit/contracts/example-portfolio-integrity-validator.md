# Validatorvertrag: Auditintegrität / Validator Contract: Audit Integrity

## 1. Zweck und Grenze / Purpose and Boundary

Der spätere Validator prüft ausschließlich die Integrität des kanonischen
Feature-038-Datensatzes, seiner kontrollierten Fixtures und seiner
repository-lokalen Relationen. Er prüft keine historische oder externe Quelle
über das Netzwerk und ändert kein Produkt, Beispiel oder Evidence-Artefakt.

*The later validator checks only the integrity of the canonical Feature-038
dataset, controlled fixtures, and repository-local relations. It never reads a
historical or external source over the network and changes no product, example,
or evidence artifact.*

## 2. Platzierung und API-Grenze / Placement and API Boundary

- Testklasse: `tests/TuiVision.Examples.SmokeTests/ExamplePortfolioAuditIntegrityTests.cs`
- Fixtures: `tests/TuiVision.Examples.SmokeTests/Fixtures/ExamplePortfolioAudit/`
- Parser: `System.Text.Json` mit expliziten Optionen und geschlossenen Enums
- Root: expliziter, vom Test aufgelöster Repository-Root; kein Vertrauen in
  aktuelles Arbeitsverzeichnis, `HOME`, Locale, Uhrzeit oder Netzwerk
- Ergebnis: strukturierte Liste stabiler Fehlercodes plus verständlicher
  Textdiagnose; keine Teilannahme nach einem Fehler
- Determinismus: `StringComparer.Ordinal`, sortierte IDs/Pfade, Unicode-NFC,
  keine Zufallswerte oder Parallelreihenfolge

*The test resolves an explicit repository root, uses closed JSON contracts and
ordinal ordering, and returns stable text diagnostics. It has no working-directory,
home-directory, locale, clock, network, random, or parallel-order dependency.*

## 3. Positive Akzeptanztests / Positive Acceptance Tests

| Testgrenze / Test boundary | Erwartung / Expectation |
|---|---|
| Schema und Baseline | Schema `1.0`, Feature-ID, `MergeAndSync`, vier Intake-Hashes, Feature-037-Evidence und 37-Pfad-Hash sind exakt. |
| Vertikalschnitt `EX036` | Vollständige TVFM-, Entry-, Guide-, Smoke-, Framework-, Decision-, A11Y-, Plattform- und Review-Relationen. |
| Portfolio | Exakt `EX001`–`EX037`, Namen/Rollen/Waves gemäß Abnahmevertrag, keine unbekannte direkte Projektzeile. |
| Quellen | Eindeutige IDs/Pfade/Hashes, richtige Authority, No-Copy-Grenze und reziproke `ExampleIds`. |
| Evidence | Eindeutige IDs, kontrollierte Pfade, vorhandene Baseline-Pfade, klare Proof-Grenze und Rückrelation. |
| Entscheidungen | Genau eine Frameworkentscheidung, Disposition und ein Statusobjekt je Dimension; konsistente Gap-Regeln. |
| Findings | Leere Menge oder lückenlose `EF001+`; eindeutige Keys, genau ein Primary Owner, vollständige Proof-/Review-Felder. |
| Deduplizierung | Eine Ursache → ein Finding; alle betroffenen Beispiele enthalten; keine künstliche Trennung nach Beispielname. |
| Owner-DAG | Nur nicht leere Owner, vollständige Findings, azyklische Kanten und gültige topologische Reihenfolge. |
| Handoff | Exakt ein Intake je nicht leerer Gruppe, leere Gruppen unterdrückt, exakt ein Closure als letzter Knoten. |
| Governance | Zwölf Presets, Standardsentscheidungen, getrennte Applicability/Implementation und vollständige Evidence-/Triggerfelder. |
| Gate | Kein ProductDecision, keine Scopeverletzung, kein unerlaubter Remote-/Merge-Claim und keine vorweggenommene Konformität. |
| Markdown-Projektionen | Neun fachliche Projektionen plus `pr-evidence.md` ergeben exakt zehn Familien, nennen den kanonischen Datensatz und enthalten keine widersprechende Kardinalität oder Gateaussage. |

## 4. Kontrollierte fehlerhafte Fixtures / Controlled Malformed Fixtures

Jede Fixture verletzt genau eine Primärinvariante. Die Feature-038-Abnahmemenge
enthält exakt die folgenden 46 Fixtures. Weitere Experimente gehören außerhalb
dieser kanonischen Menge und dürfen ihre Kardinalität nicht verändern.

*Each fixture violates exactly one primary invariant. The Feature-038
acceptance set contains exactly the following 46 fixtures. Additional
experiments stay outside this canonical set and cannot change its cardinality.*

| Fixture | Erwarteter Fehlercode / Expected error code | Verletzte Invariante / Violated invariant |
|---|---|---|
| `malformed-json-syntax.json` | `EPA001` | JSON ist syntaktisch ungültig; atomare Ablehnung. |
| `malformed-unknown-schema.json` | `EPA002` | Unbekannte Schema-Version. |
| `malformed-wrong-feature-or-mode.json` | `EPA003` | Falsche Feature-ID oder nicht autorisierter Delivery Mode. |
| `malformed-intake-hash.json` | `EPA004` | Akzeptierter Hash stimmt nicht. |
| `malformed-project-set-hash.json` | `EPA005` | 37-Projekt-Baseline driftet. |
| `malformed-missing-example.json` | `EPA010` | Eine der 37 Zeilen fehlt. |
| `malformed-duplicate-example.json` | `EPA011` | ID oder Name ist doppelt. |
| `malformed-unknown-example.json` | `EPA012` | Unbekannte Zeile wurde aufgenommen. |
| `malformed-role-wave.json` | `EPA013` | Rolle oder Wave widerspricht dem exakten Vertrag. |
| `malformed-a11y-history.json` | `EPA014` | `A11yFramework` wird historisch gewertet oder besitzt unbegründetes `N/A`. |
| `malformed-missing-source.json` | `EPA020` | Portfoliozeile verweist auf unbekannte Source-ID. |
| `malformed-duplicate-source-path.json` | `EPA021` | Authority/Pfad ist doppelt oder kollidiert. |
| `malformed-source-hash.json` | `EPA022` | Hashformat oder gebundener Hash ist falsch. |
| `malformed-orphan-source.json` | `EPA023` | Quelle besitzt keine gültige Gegenrelation. |
| `malformed-nonreciprocal-source.json` | `EPA024` | Vorwärts-/Rückwärtsrelation widerspricht sich. |
| `malformed-moving-upstream.json` | `EPA025` | Ungepinnter Branch/Release oder neuer externer Pfad tritt ein. |
| `malformed-missing-evidence.json` | `EPA030` | Evidence-ID oder Pfad fehlt. |
| `malformed-nonreciprocal-evidence.json` | `EPA031` | Evidence-Gegenrelation fehlt. |
| `malformed-protected-evidence-path.json` | `EPA032` | Neu erzeugte Evidence zielt in eine geschützte Produkt-/Quellenwurzel. |
| `malformed-unknown-dimension.json` | `EPA040` | Unbekannter Dimensionsstatus. |
| `malformed-na-without-rationale.json` | `EPA041` | `N/A` ohne Begründung oder Trigger. |
| `malformed-pass-without-evidence.json` | `EPA042` | `Pass` ohne Evidence. |
| `malformed-multiple-disposition.json` | `EPA043` | Keine exakt eine Hauptentscheidung. |
| `malformed-accepted-with-gap.json` | `EPA044` | Akzeptierte Disposition enthält Gap. |
| `malformed-gap-without-finding.json` | `EPA045` | Gap hat weder Finding noch ProductDecision-Stop. |
| `malformed-framework-decision.json` | `EPA046` | Small fix/hardening wird ohne Finding akzeptiert. |
| `malformed-finding-id-gap.json` | `EPA050` | `EF001+` ist nicht lückenlos. |
| `malformed-duplicate-dedup-key.json` | `EPA051` | Eine Ursache wurde auf mehrere Findings verteilt. |
| `malformed-split-root-cause.json` | `EPA052` | Gleiche Ursache nutzt unterschiedliche Keys. |
| `malformed-finding-example-link.json` | `EPA053` | Finding-/Example-Relation ist nicht reziprok. |
| `malformed-multiple-primary-owner.json` | `EPA054` | Finding hat nicht genau einen Primary Owner. |
| `malformed-unknown-primary-owner.json` | `EPA055` | Owner liegt außerhalb des geschlossenen Vokabulars. |
| `malformed-incomplete-finding.json` | `EPA056` | Reproduktion, Red/Green, Risiko, Review oder Trigger fehlt. |
| `malformed-owner-cycle.json` | `EPA060` | Owner-DAG enthält Zyklus. |
| `malformed-empty-owner-intake.json` | `EPA061` | Leere Owner-Gruppe erzeugt Intake. |
| `malformed-missing-owner-intake.json` | `EPA062` | Nicht leere Owner-Gruppe hat keinen Intake. |
| `malformed-preassigned-feature-number.json` | `EPA063` | Follow-up nimmt Feature-Nummer vorweg. |
| `malformed-closure-count.json` | `EPA064` | Kein oder mehrere Closure-Intakes. |
| `malformed-closure-order.json` | `EPA065` | Closure steht nicht exakt zuletzt oder hängt nicht von allen emittierten Gruppen ab. |
| `malformed-started-followup.json` | `EPA066` | Handoff behauptet gestarteten Branch/Run. |
| `malformed-governance-omission.json` | `EPA070` | Preset/Standard oder Pflichtfeld fehlt. |
| `malformed-na-implementation.json` | `EPA071` | `N/A` ist fälschlich als umgesetzt markiert. |
| `malformed-open-without-owner.json` | `EPA072` | `Open` fehlt Owner, Follow-up oder Trigger. |
| `malformed-remote-claim.json` | `EPA080` | Lokaler Datensatz behauptet Push, PR, Merge oder Remote-Erfolg. |
| `malformed-premature-conformance.json` | `EPA081` | Feature 038 erklärt bereits volle Konformität/Lernreife. |
| `malformed-product-decision-ready.json` | `EPA082` | ProductDecision wird mit abgeschlossenem Gate kombiniert. |

## 5. Test-first Sequenz / Test-first Sequence

1. Evidence-Skelett und Gate-Anforderungen existieren.
2. Validator und Testassembly kompilieren vollständig.
3. Der fokussierte `EX036`-Test scheitert ausschließlich mit `EPA010` oder
   einem engeren Slice-Fehler, weil die Zeile noch nicht vollständig ist.
4. `valid-vertical-slice.json` macht den Slice grün.
5. Die Vollmengenprüfung scheitert kontrolliert an 1/37.
6. Waveweise Population macht 37/37 grün.
7. Jede malformed Fixture wird einzeln und dann als Matrix geprüft.
8. Erst danach folgen bestehende Smokes, vollständige Release-Tests und Coverage.

## 6. Pfad- und Sicherheitsregeln / Path and Security Rules

- Normalisiere relative Pfade mit `/`; lehne absolute Pfade, `..`, NUL und
  Pfade außerhalb des expliziten Repository-Roots ab.
- Folge für Audit-Fixtures keinen Symlinks aus dem kontrollierten Root hinaus.
- Verwende keine beliebigen Nutzerdaten, Home-Verzeichnisse, Temp-Pfade aus
  Evidence oder externe Checkouts. Test-eigene temporäre Verzeichnisse sind nur
  für kontrollierte Dateipfade zulässig und werden nicht im Dataset persistiert.
- Begrenze Dateigröße, Sammlungslängen und Stringlängen proportional zur
  37-Zeilen-Domäne, damit fehlerhafte Daten nicht unbeschränkt Ressourcen binden.
- Fehlerdiagnosen enthalten Pfad/ID und Regel, aber keine Credentials,
  Umgebungswerte oder Stacktrace als Nutzer-Evidence.

## 7. Didaktische Kommentarprüfung / Didactic Comment Review

Kommentare sind nur für drei nicht offensichtliche Grenzen geplant:

1. warum Vorwärts- und Rückwärtsrelationen gemeinsam geprüft werden;
2. warum Findings erst nach Root-Cause-Freeze lückenlose IDs erhalten;
3. warum `MergeAndSync` nur exakt belegte Remote-/Closure-Claims akzeptiert.

Jeder solche Kommentar ist kurz, Deutsch zuerst/Englisch danach und erklärt
das Warum statt den sichtbaren Code zu wiederholen.

## 8. Nichtziele / Non-goals

Der Validator testet keine Produktfunktion neu, ersetzt keine bestehenden
Smokes, berechnet keine fachliche Disposition, entscheidet keinen Primary Owner
aus Freitext, generiert keine Remediation automatisch aus unsicheren Daten und
startet keinen Build, Push, PR, Merge oder Folgefeature.
