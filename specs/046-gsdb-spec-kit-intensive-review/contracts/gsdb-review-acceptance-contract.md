# Akzeptanzvertrag: GSDB-Spec-Kit-Intensivprüfung / Acceptance Contract: GSDB Spec Kit Intensive Review

**Vertrags-ID / Contract ID**: `GSDB-046-ACCEPTANCE-1`

**Datum / Date**: 2026-08-30
**Normative Eingabe / Normative input**: akzeptierte Feature-046-Spezifikation und bindender autonomer Laufzustand

## 1. Vertragszweck / Contract purpose

Dieser Vertrag definiert die prüfbaren Ein- und Ausgaben des evidenz-only Reviews. Er ist kein Produkt-API-Vertrag und führt keine Runtime-Komponente ein.

This contract defines the testable inputs and outputs of the evidence-only review. It is not a product API contract and introduces no runtime component.

## 2. Eingabevertrag / Input contract

Die Implementierung akzeptiert ausschließlich einen Repository-Snapshot, der alle folgenden Bedingungen erfüllt:

The implementation accepts only a repository snapshot satisfying all of these conditions:

1. Branch, Feature-ID, Lauf-ID und aktuelle Phase stimmen mit `autonomous-run-state.json` überein.
2. Bindendes Intake, Review, Manifest und Receipt stimmen mit `acceptedArtifacts` überein. Feature-Artefakte mit Routing-Payload stimmen mit der neuesten abgeschlossenen, im Run-State per `resultSha256` gebundenen Ergebnisdatei und deren `payloadSha256` überein. Ein selbst als Routing-Payload gebundener Reviewbericht darf die post-remediation Hashes ausdrücklich gelisteter Artefakte attestieren und hat für diese Pfade Vorrang vor älteren Phasen-Payloads.
3. Beide abgeschlossenen Checklisten enthalten keine offenen Pflichtpunkte. Artefakte ohne Routing-Payload oder Review-Attestation werden mit aktuellem Hash inventarisiert und spätestens durch den exakten Kandidaten-Commit gebunden; eine nicht vorhandene Run-State-Hashbindung darf nicht behauptet werden.
4. Die aktive Preset-Registry ist lesbar; alle aktivierten Einträge verweisen auf vorhandene installierte Presets.
5. Der physische GSDB-Baum unter `docs/secure-development/` und der eindeutige Manifestabschluss sind lesbar.
6. Relevante Evidenz aus Features 016, 044 und 045 ist lesbar, wird aber nicht als positive Entscheidung voreingestellt.
7. Das Delivery-Set enthält keine unautorisierten Produkt-, Runtime-, API-, Abhängigkeits-, Projekt-, Beispiel-, Workflow-, Provider-, Secret- oder historische Quelländerungen.

Bei einem Verstoß schlägt der Validator oder das zugehörige Gate geschlossen fehl. Er darf fehlende Eingaben nicht optimistisch ersetzen.

On violation, the validator or related gate fails closed. It must not optimistically replace missing input.

## 3. Ausgabevertrag / Output contract

### 3.1 Kanonische Ausgabe / Canonical output

Genau eine kanonische fachliche Quelle wird dauerhaft geschrieben:

Exactly one canonical substantive source is written durably:

```text
docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review/gsdb-spec-kit-intensive-review.json
```

Sie entspricht dem Modell in `data-model.md`, ist gültiges UTF-8-JSON, endet mit LF und enthält keine Kommentare, Secrets oder absoluten lokalen Pfade.

It conforms to `data-model.md`, is valid UTF-8 JSON, ends with LF, and contains no comments, secrets, or absolute local paths.

### 3.2 Lesbare Projektionen / Human-readable projections

Vor den lesbaren Dateien werden deterministische maschinenlesbare JSON-Projektionen für Quellen, Kontrollen, Sprachen, Preset/Governance, Evidenzfamilien und Summary erzeugt. Jede verweist über den normalisierten SHA-256 auf dieselbe kanonische Datei und enthält nur ihre sortierte fachliche Teilmenge.

Before human-readable files, deterministic machine-readable JSON projections are generated for sources, controls, languages, preset/governance, evidence families, and summary. Each points through normalized SHA-256 to the same canonical file and contains only its sorted substantive subset.

Alle in `data-model.md` definierten Markdown-Projektionen werden ausschließlich aus dem kanonischen JSON erzeugt. Sie sind Deutsch zuerst, Englisch danach, CEFR-B2, text-first und bytegenau reproduzierbar.

Every Markdown projection defined in `data-model.md` is generated only from canonical JSON. It is German first, English second, CEFR-B2, text-first, and byte-for-byte reproducible.

### 3.3 Validierungsoberfläche / Validation surface

Der ausführbare Vertrag liegt ausschließlich als test-only C#-Code im bestehenden Projekt `tests/TuiVision.Drivers.Tests`. Er benutzt vorhandenes MSTest und `System.Text.Json`. Neue Projekte, Pakete oder Skripte sind nicht zulässig.

The executable contract resides only as test-only C# code in the existing `tests/TuiVision.Drivers.Tests` project. It uses existing MSTest and `System.Text.Json`. New projects, packages, or scripts are prohibited.

## 4. Fachliche Invarianten / Substantive invariants

### AC-001 – Kontrollabschluss / Control closure

- Das kanonische Array enthält exakt 157 eindeutige `CL-XX-NN`-Kontrollen.
- Die Kontrollmenge entspricht exakt den Überschriften der zwölf GSDB-Checklisten.
- Die unabhängig berechnete Kapitelverteilung entspricht exakt `12/13/15/10/13/11/12/13/17/17/12/12`; eine Umverteilung bei weiterhin 157 Zeilen ist ungültig.
- Jede Kontrolle besitzt genau eine erlaubte Disposition und alle Pflichtfelder.
- Kapitel-, Dispositions- und Gesamtzahlen werden aus dem Array berechnet.
- Die zweiachsigen Quellwerte für Anwendbarkeit und Erfüllung bleiben getrennt erhalten und ersetzen nie die Feature-Disposition.

### AC-001A – Einheitlicher Dispositionskatalog / Uniform disposition catalog

- Für Kontrollen und alle zusätzlichen Checkpoints sind ausschließlich `Applicable`, `AlreadySatisfied`, `N/A`, `Open` und `FollowUp` erlaubt.
- `Pass`, kombinierte Werte, leere Werte und domänenspezifische Ersatzcodes sind ungültig.

### AC-002 – Keine weiteren festen Inventarzahlen / No other fixed inventory counts

- Quellen-, Sprach-, Preset-, Agentenflächen-, Governance-, Evidenz- und Beobachtungszahlen werden aus dem akzeptierten Snapshot oder den kanonischen Arrays abgeleitet.
- Die beim Planen beobachteten 37 GSDB-Dateien und 12 aktivierten Presets sind Vergleichswerte, keine Validatorliterale.

### AC-003 – Quellenabschluss / Source closure

- Jede physische GSDB-Datei erscheint genau einmal.
- Manifestrollen derselben Datei werden zusammengeführt.
- Physischer Baum und eindeutiger Manifestabschluss stimmen überein.
- Text- und Binärhashregeln sind getrennt; die verwaltete PDF-Prüfsumme wird geprüft.

### AC-004 – Unabhängige positive Disposition / Independent positive disposition

- `Applicable`, `AlreadySatisfied` oder eine andere positive Aussage benötigt aktuelle Feature-046-Evidenz.
- Eine Aussage aus Feature 016, 044 oder 045 allein ist kein ausreichender positiver Beleg.
- Fehlende oder widersprüchliche Evidenz führt zu einer nicht positiven, ehrlich begründeten Disposition.

### AC-005 – Sprachabdeckung / Language coverage

- Sprachprofile sind die Vereinigung aus expliziten Regeln und tatsächlicher Repository-Aktivität.
- Aktive, historische und abwesende Profile bleiben unterscheidbar.
- Historische Quellen bleiben read-only `N/A`, sofern keine konkrete protokollierte Kontrollfrage eine begrenzte Einsicht erfordert.

### AC-006 – Aktuelle Presets und Agenten-Parität / Current presets and agent parity

- Jeder aktuell aktivierte Registry-Eintrag erscheint genau einmal mit tatsächlicher installierter Version und Priorität.
- Registry-Anzahl und Versionen werden neu abgeleitet.
- Alle projektgeführten Agentenflächen werden als eigenes, referenziell geschlossenes Inventar aus Level-2-Verfassung, Registry-Agentenschlüsseln und tatsächlich versionierten Guidance-/Command-/Prompt-/Skill-/Agent-Pfaden abgeleitet. Persönlicher oder nicht vorhandener Agentenzustand wird nicht inventarisiert.
- Beide Verfassungen und alle abgeleiteten projektgeführten Agentenflächen werden als getrennte Evidenz geprüft.
- Keine stille Aktualisierung, Promotion oder Harmonisierung erfolgt im Review.

### AC-007 – Evidenzfamilien / Evidence families

- Jede Familie hat deklarative Include-/Exclude-Selektoren, sortierte Treffer und einen reproduzierbaren Aggregathash.
- Evidenzreferenzen sind pfad- und hashgenau und benennen ihre Beweisgrenze.
- Eine Familie allein beweist keine positive Kontrolle.

### AC-008 – Projektionstreue / Projection fidelity

- Der Validator rendert jede erwartete maschinenlesbare und lesbare Projektion im Speicher.
- Quellen-, Kontroll-, Sprach-, Preset/Governance-/Agentenflächen-, Evidenzfamilien- und Summary-JSON tragen denselben kanonischen Hash und einen korrekten normalisierten Payload-Hash, der sein eigenes Hashfeld ausschließt.
- Versionierte Bytes stimmen exakt mit den erwarteten Bytes überein.
- Markdown enthält keine unabhängige Zählung oder Aussage außerhalb des JSON.

### AC-009 – Scope-Firewall / Scope firewall

- Keine Produkt-, Runtime-, API-, Abhängigkeits-, Projekt-, Beispiel- oder Workflowänderung.
- Keine Provider-Einstellung, Geheimnisrotation, geheime Ausgabe oder Folgefeature-Implementierung.
- Historische Quellen sind kein Schreibziel.
- Befunde sind nur dokumentierte Beobachtungen mit `implementedInFeature046=false`.

### AC-010 – Barrierefreiheit / Accessibility

- Deutsch zuerst, Englisch danach, CEFR-B2.
- Semantische Überschriften und Tabellen; Klartextlabels für Codes.
- Keine wesentliche Bedeutung allein durch Farbe, Layout oder Pointer-Aktion.
- Conditional DocFX/axe/Textbrowser-Nachweise folgen dem tatsächlichen Diff-Trigger.

### AC-011 – Nachweisgrenzen / Proof boundaries

- Lokale direkte Evidenz behauptet keine Remote-, Provider-, Rechts- oder menschliche Eigenschaft.
- Remote- und Merge-Aussagen benötigen exact-head Provider-Evidenz.
- Ein Human-Approval-Bypass ersetzt höchstens ein benanntes, nachweislich nicht verfügbares Remote-Gate nach vollständigem lokalem technischem Grün, null umsetzbaren technischen Befunden, null actionable Review-Threads, null Scope-Verstößen und Human Approval als einziger offener Regel. Der Beleg nennt Gate, autorisierte Person, Zeitpunkt, Begründung, Evidence-Grenze und Ablaufzeitpunkt.
- Die Planphase erteilt keine Ausnahmegenehmigung.

### AC-012 – Determinismus / Determinism

- Ordinale case-sensitive Sortierung.
- Repository-relative Slash-Pfade.
- LF-normalisierte Texthashes und Raw-Byte-Binärhashes.
- Stabiler Renderer und stabile Diagnostikcodes.
- Zweiter Lauf über unveränderten Snapshot erzeugt identische fachliche Bytes.

## 5. Validator-Diagnostik / Validator diagnostics

Fehler verwenden stabile Codes und eine kurze deutsch-englische Meldung. Mindestens folgende Kategorien sind vorgesehen:

Errors use stable codes and a short German-English message. At least these categories are planned:

| Code | Bedingung / Condition |
|---|---|
| `GSDB046_INPUT_HASH_MISMATCH` | Bindender Eingabehash stimmt nicht. / Binding input hash differs. |
| `GSDB046_SOURCE_CLOSURE_MISMATCH` | Physik und Manifestabschluss unterscheiden sich. |
| `GSDB046_SOURCE_DUPLICATE` | Physischer Pfad ist doppelt. |
| `GSDB046_CONTROL_COUNT` | Kontrollzahl ist nicht 157. |
| `GSDB046_CONTROL_DUPLICATE` | Kontroll-ID ist doppelt. |
| `GSDB046_CONTROL_CHAPTER_COUNTS` | Kapitelverteilung weicht von der akzeptierten 157er Partition ab. |
| `GSDB046_DISPOSITION_UNKNOWN` | Dispositionscode ist unbekannt. |
| `GSDB046_REQUIRED_FIELD_MISSING` | Ein Pflichtfeld fehlt oder ist leer. |
| `GSDB046_POSITIVE_EVIDENCE_MISSING` | Positive Aussage hat keine aktuelle Evidenz. |
| `GSDB046_REFERENCE_DANGLING` | Referenz zeigt auf kein kanonisches Ziel. |
| `GSDB046_PRESET_REGISTRY_DRIFT` | Presetliste entspricht nicht der aktiven Registry. |
| `GSDB046_AGENT_SURFACE_CLOSURE` | Agentenflächen entsprechen nicht Verfassung, Registry und tracked paths. |
| `GSDB046_INVENTORY_OMISSION` | Sprach-, Governance- oder Evidence-Familien-Pflicht fehlt. |
| `GSDB046_ROUTING_BINDING_STALE` | Ein älterer oder widersprüchlicher Routing-Payload wird als aktuell verwendet. |
| `GSDB046_SUMMARY_DRIFT` | Gespeicherte und berechnete Summen unterscheiden sich. |
| `GSDB046_PROJECTION_DRIFT` | Markdown entspricht nicht dem Renderer. |
| `GSDB046_HASH_MISMATCH` | Datei- oder Aggregathash stimmt nicht. |
| `GSDB046_SCOPE_VIOLATION` | Delivery-Set enthält verbotene Änderung. |

Diagnostik nennt keine Secret-Inhalte und keine absoluten Benutzerpfade.

Diagnostics expose no secret contents or absolute user paths.

## 6. Gate-Vertrag / Gate contract

Die Datei `autonomous-gate-requirements.json` ist der maschinenlesbare Gate-Vertrag. Er verlangt:

The file `autonomous-gate-requirements.json` is the machine-readable gate contract. It requires:

- akzeptierte Input- und Laufbindung;
- targeted Red/Green und vollständige Validator-/Negativtests;
- Release-Test und assembly-spezifische 70-%-Coverage;
- Formatierung, Supply Chain, Workflow-Integrität und Secret-Scans;
- Scope-/Delivery-Set-Validierung;
- anwendbare DocFX-, axe- und Textbrowser-Prüfung;
- aktuelle Preset-/Agenten-Parität;
- committed-candidate, remote exact-head, MergeAndSync und Post-Merge-Sync;
- kausalen Intake-/Serien-/Statistik-/Retrospektivabschluss.

Gate-Evidenz nennt Befehl, Exitcode, Commit, Zeit, Belegpfad und relevante Zählungen. `N/A` ist nur mit dem vorab definierten Trigger und aktueller Begründung zulässig.

Gate evidence states command, exit code, commit, time, evidence path, and relevant counts. `N/A` is allowed only with the predefined trigger and a current rationale.

## 7. Akzeptanzszenarien / Acceptance scenarios

### Szenario A – Vollständiger Snapshot / Complete snapshot

Gegeben ist ein unveränderter akzeptierter Snapshot. Wenn Inventare abgeleitet, alle 157 Kontrollen unabhängig bewertet, JSON geschrieben und Projektionen erzeugt werden, dann sind alle lokalen Validatorprüfungen grün und ein zweiter Lauf erzeugt identische fachliche Bytes.

Given an unchanged accepted snapshot, when inventories are derived, all 157 controls are independently assessed, JSON is written, and projections are generated, all local validator checks pass and a second run produces identical substantive bytes.

### Szenario B – Frühere positive Aussage ohne neue Evidenz / Earlier positive statement without new evidence

Gegeben ist eine positive Feature-016-/044-/045-Aussage. Wenn sie ohne aktuelle Feature-046-Evidenz als positive Disposition verwendet wird, dann schlägt der Validator mit `GSDB046_POSITIVE_EVIDENCE_MISSING` fehl.

### Szenario C – Registry ändert sich / Registry changes

Gegeben ist eine geänderte aktive Preset-Registry. Wenn das JSON noch die beim Planen beobachtete Liste enthält, dann schlägt der Registry-Abgleich fehl. Es gibt keine hart codierte Erwartung von 12.

### Szenario D – Markdown manuell verändert / Markdown changed manually

Gegeben ist gültiges kanonisches JSON. Wenn eine Projektion manuell geändert oder eine Zahl darin separat gepflegt wird, dann schlägt der bytegenaue Projektionsvergleich fehl.

### Szenario E – Externe Grenze / External boundary

Gegeben ist eine Kontrollfrage, die nur Organisation, Provider oder menschliche Freigabe beantworten kann. Dann dokumentiert der Review die Grenze und behauptet keine lokale Erfüllung.

### Szenario F – Feststellung verlangt Produktänderung / Finding requires product change

Gegeben ist ein belegter Befund mit möglicher Produktabhilfe. Dann wird ein Folgehinweis dokumentiert, `implementedInFeature046` bleibt `false`, und das Delivery-Set enthält keine Abhilfe.

### Szenario G – Human-Approval-Ausnahme / Human approval exception

Gegeben sind vollständiges lokales technisches Grün, null umsetzbare technische Befunde, null actionable Review-Threads, null Scope-Verstöße und Human Approval als einzige offene Regel. Nur wenn exakt ein benanntes Remote-Gate nachweislich nicht verfügbar ist und eine autorisierte Person die eng begründete Ausnahme mit Zeitpunkt, Evidence-Grenze und Ablaufzeitpunkt freigibt, darf dieses Gate als `HumanApproval` begrenzt ersetzt werden. Andere Gates bleiben unverändert.

## 8. Vertragserfüllung / Contract fulfillment

Der Vertrag ist erst erfüllt, wenn fachliche Invarianten, technische Gates, exact-head-Lieferung und kausaler Closeout vollständig belegt sind. Ein grüner lokaler Validator allein ist notwendig, aber nicht hinreichend für `MergeAndSync`-Abschluss.

The contract is fulfilled only when substantive invariants, technical gates, exact-head delivery, and causal closeout are fully evidenced. A green local validator alone is necessary but not sufficient for `MergeAndSync` completion.
