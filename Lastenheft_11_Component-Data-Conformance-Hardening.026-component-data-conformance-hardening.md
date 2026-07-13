# Lastenheft 11: Component and Data Conformance Hardening

## 0. Dokumentstatus

**Vorgesehener Spec-Kit-Branch:** `026-component-data-conformance-hardening`

**Verbindliche Reihenfolge:** nach vollständig gemergtem Feature 025, vor
Feature 028, Wave 5 und Wave 6

**Lieferart:** begrenzte Komponenten-, Validation-, Datei- und
Ressourcen-Härtung für vier akzeptierte Findings

**Startgrenze:** Dieses Lastenheft darf heute nicht autonom ausgeführt werden.
Es wird erst nach dem Abschluss und Main-Sync von Feature 025 zum nächsten
Intake.

*Feature 026 runs only after Feature 025 is merged and main is synchronized. It
hardens four accepted component and data findings. This requirements document
must not be executed autonomously today.*

---

## 1. Ausgangslage

Audit-Revision 2 ordnet vier Findings der Komponenten-/Datenhärtung zu:

- `F010` / `C019`: Dialoge schließen auf nicht abschließende Commands und der
  Standardpfad validiert Kinder nicht vollständig.
- `F011` / `C021`: `TInputLine` und `TValidator` besitzen keinen integrierten
  Runtime-Vertrag.
- `F012` / `C023`: File-Dialog-Ergebnisse beweisen keine mode-abhängige
  Pfadvalidierung.
- `F013` / `C026`: Named Resources beweisen noch keine sichere,
  rekonstruierbare Menü-, StatusLine- und Dialogkomposition.

Diese Flows bauen auf dem in 025 korrigierten Fokus-, Event-, Modal-, Command-
und Lifecycle-Vertrag auf. Feature 026 darf diese Foundation verwenden, aber
nicht erneut als lokale Komponenten-Sonderlösung implementieren.

*Revision 2 assigns four findings to component and data hardening. The work
depends on the corrected focus, event, modal, command, and lifecycle contracts
from Feature 025. Feature 026 must reuse that foundation rather than recreating
it locally.*

## 2. Verbindliche Eingaben

1. alle final gemergten Feature-025-Artefakte und Tests
2. Feature-024 Revision-2 `conformance-audit.json`, `findings.md`,
   `consumer-readiness-review.md` und `pre-wave5-gate.md`
3. relevante `tv203s/`-Quellen, insbesondere `tdialog.cc`, `tinputli.cc`,
   `tfiledia.cc` und zugehörige Header, read-only
4. gepinnte Free-Vision-Quellen `FV006`, `FV007`, `FV010`, `FV012`
5. relevante Consumer-Flows in `TVDEMOS/` und `TVFM/`, read-only
6. aktuelle Constitution, Agent-Guidance und lokale Preset-Matrix

## 3. Ziele

1. Dialogabschluss auf explizite Completion-Commands begrenzen.
2. Kindvalidierung, Fokus-Veto und zustandserhaltende Ablehnung als
   durchgehenden realen Pfad liefern.
3. `TInputLine` und `TValidator` modern, typsicher und testbar integrieren.
4. File-Dialog-Resultate operationstypisch validieren, ohne versteckte I/O-
   Entscheidungen oder Datenverlust.
5. Einen sicheren, allowlist-basierten modernen Ressourcenvertrag für
   rekonstruierbare UI-Komposition beweisen.
6. Alle vier Findings schließen, ohne Wave-Anwendungen oder destruktive
   Dateimanager-Workflows zu implementieren.

## 4. Scope

### 4.1 Im Scope

- relevante Komponenten in `src/TuiVision.Controls/`
- relevante Ressourcen- und Persistenzgrenzen in
  `src/TuiVision.Serialization/`
- vorhandene Tests in Controls- und Serialization-Testprojekten
- kleine additive API-Erweiterungen für Validatoren, File-Resultate oder sichere
  UI-Beschreibungen
- XML-Dokumentation, Guides, A11Y- und Evidence-Artefakte
- Feature-024-Findingstatus nach bestandenem Proof

### 4.2 Nicht im Scope

- `TVDEMOS/`, `TVFM/`, `tv203s/` oder Free Vision verändern oder portieren
- Wave-5-/Wave-6-Anwendungen, File Manager, Copy, Move, Delete, Trash oder
  Provider-Integration implementieren
- beliebige Runtime-Typaktivierung aus Persistenzdaten
- historische binäre Ressourcenformate 1:1 kompatibel machen
- unkontrollierte Reflection, Assembly-Scanning oder polymorphe
  Deserialisierung aus nicht vertrauenswürdigen Daten
- Dialoge oder Controls visuell neu gestalten
- Core-Verträge aus 025 erneut duplizieren
- neue Runtime-Abhängigkeiten oder breite Serialization-Neuarchitektur
- Breaking API Changes ohne `ProductDecision`

## 5. Finding-Anforderungen

### R-026-001: Explizite Dialog-Completion (`F010`, `C019`)

Nur akzeptierte Abschlussbefehle wie OK, Cancel, Yes oder No dürfen einen Dialog
beenden. Anwendungscommands, Navigation, Help und unbekannte Commands bleiben im
Dialog oder werden geordnet weitergeleitet. Der konkrete erlaubte Satz muss
zentral, testbar und erweiterbar sein, ohne jeden Command als Abschluss zu
behandeln.

### R-026-002: Kindvalidierung und Ablehnung (`F010`, `C019`)

Vor bestätigendem Abschluss validiert der Dialog seine relevanten Kinder in
deterministischer Reihenfolge. Das erste ablehnende Control behält oder erhält
Fokus; Eingabedaten, Dialogzustand und sichtbare Fehlerevidence bleiben
erhalten. Cancel darf ohne inhaltliche Validierung schließen, sofern kein
separater Safe-Close-Vertrag greift.

Tests müssen den Produktionspfad ausführen. Ein abgeleiteter Testtyp darf keine
nicht virtuelle Methode verstecken und damit einen Pfad beweisen, den der
Dialog selbst nie aufruft.

### R-026-003: Moderne Validator-Integration (`F011`, `C021`)

`TInputLine` erhält einen expliziten optionalen Validator-Vertrag. Dieser muss
mindestens Edit-/Syntaxprüfung, Transfer oder Commit, Focus-Loss und Dialog-
Acceptance unterscheiden können, soweit der vorhandene Validatorstil dies
erfordert. Die Lösung darf einen kleineren modernen Vertrag wählen, muss aber
die historische Absicht und bewusste Abweichungen dokumentieren.

Validator-Ablehnung darf Text, Auswahl, Cursor und vorher gültigen Zustand nicht
teilweise zerstören. Fehlerbeschreibung und Fokuspfad müssen text-first und für
assistive Nutzung verständlich sein.

### R-026-004: Mode-abhängige File-Dialog-Ergebnisse (`F012`, `C023`)

File-Dialog-Flows unterscheiden mindestens:

- Verzeichnisnavigation
- Wildcard-/Filtereingabe
- bestehendes Open-Ziel
- fehlendes Open-Ziel
- neues Save-Ziel
- vorhandenes Save-Ziel mit nachgelagerter expliziter Overwrite-Entscheidung
- ungültigen manuellen Pfad
- Cancel

Die Dialoggrenze liefert einen typisierten, erklärbaren Result- oder
Rejection-Zustand. Tests verwenden nur Source-Fixtures oder temporäre
Verzeichnisse, lesen keine beliebigen Benutzerdaten und führen keine
destruktiven Operationen aus.

### R-026-005: Sichere Named-Resource-Komposition (`F013`, `C026`)

Named Resources müssen eine rekonstruierbare, sichere Komposition für die von
den Consumern benötigten Menü-, StatusLine- und Dialogstrukturen ermöglichen.
Die Lösung darf moderne unveränderliche Beschreibungsmodelle, Builder oder
registrierte Records verwenden. Sie muss nicht das historische Objektgraph-
Format kopieren.

Erforderlich sind exakte, case-sensitive Keys, allowlist-basierte Typen,
Versionierung, klare Eigentumsgrenzen und deterministische Ablehnung von
unknown type, unsupported version, truncation, trailing data, duplicate key,
invalid command reference und unzulässigem Graph. Keine beliebige Reflection-
Aktivierung ist zulässig.

## 6. Consumer-Grenze

| Consumer evidence | Framework responsibility | Excluded application work |
|---|---|---|
| `TVDEMOS/TVDEMO.PAS` | dialog completion and validation | demo port and visual composition |
| `TVDEMOS/TVEDIT.PAS` | file-result and safe decision boundaries | editor application port |
| `TVDEMOS/TVRDEMO.PAS`, `GENRDEMO.PAS` | named UI-resource composition | historical generator recreation |
| `TVFM/GLOBALS.PAS`, `COLORS.PAS` | validator-aware dialog acceptance | file-manager dialogs and business rules |
| `TVFM/TVFM.PAS` | named resource lookup and composition | file-manager application |
| `TVFM/FILECOPY.PAS`, `TRASH.PAS` | none in this feature | copy, delete, trash, confirmation policy |

## 7. Test- und Proof-Vertrag

1. Jedes Finding beginnt mit einem fehlschlagenden Produktionspfad-Test.
2. Dialogtests laufen über echte Commands und den normalen `HandleEvent`-
   Abschlussweg.
3. Fokus- und Validation-Tests verwenden den in 025 gelieferten Veto-Vertrag.
4. File-Tests verwenden kontrollierte Temp-Verzeichnisse, Betriebssystem-
   neutrale Pfade und positive wie negative Fälle.
5. Ressourcen-Proofs enthalten Roundtrip, Identity/Key, Version, malformed
   Input, unbekannte Typen und kein-partial-state Verhalten.
6. UI-bezogene Tests prüfen konkrete Zustände, View-Tree sowie relevante
   Buffer/Cell- oder text-first Evidence.
7. Kein Test darf ein historisches oder Consumer-Quellverzeichnis verändern.

## 8. A11Y, Sicherheit und Dokumentation

- Validation und Rejection müssen per Tastatur vollständig erreichbar und
  verständlich sein.
- Fokus landet nach Ablehnung auf dem relevanten Control; sichtbarer Text und
  strukturierte A11Y-Evidence widersprechen sich nicht.
- Public APIs erhalten vollständige XML-Kommentare; jede API-/XML-/Guide-
  Änderung löst DocFX plus `tests/web-a11y` und text-first Review aus.
- Ressourceninput ist untrusted data und wird fail-closed, größenbegrenzt und
  ohne beliebige Codeaktivierung verarbeitet.
- Kein Web/Auth-, Cloud-, Provider-, Produkt-AI- oder regulatorischer Trigger
  wird durch die Featureabsicht erwartet; jede reale Scope-Änderung muss die
  Governance-Entscheidung erneut öffnen.

## 9. Evidence-Artefakte

Feature 026 muss mindestens liefern:

- vollständige Feature-Artefakte unter
  `specs/026-component-data-conformance-hardening/`
- `pr-evidence.md` mit je einer Zeile für `F010` bis `F013`
- Dialog-Completion- und Validation-Matrix
- Validator-Lifecycle- und Rejection-Matrix
- File-Dialog-Mode-Matrix mit Temp-Fixture-Grenze
- Resource-Type-/Version-/Malformed-Input-Matrix
- historische und Free-Vision-Relation je Finding
- API-, Security-, A11Y-, Plattform- und Follow-up-Entscheidungen
- aktualisierte Auditentscheidungen erst nach bestandenem Proof
- Agent-Parität, Pflichtenheft, Reihenfolge, Guides und Projektstatistik

## 10. Akzeptanzkriterien

1. `F010` bis `F013` sind jeweils mit realem Red-/Green-Proof geschlossen.
2. Unverwandte Commands schließen keinen Dialog.
3. Ungültige Kinder blockieren bestätigenden Abschluss und behalten Zustand.
4. `TInputLine` kann einen Validator über den Produktionspfad verwenden.
5. File-Dialog-Ergebnisse unterscheiden alle geforderten Modi und Ablehnungen.
6. Named UI Resources sind sicher rekonstruierbar oder ein gleichwertiger,
   explizit begründeter moderner Vertrag ist bewiesen.
7. Keine destruktive Wave-6-Dateioperation und keine Beispielportierung ist im
   Diff.
8. Targeted und full Release Tests, Coverage, Format, DocFX/A11Y und alle
   ausgelösten Governance-/Plattform-Gates bestehen.
9. Feature 028 ist danach der einzige nächste Intake; beide Waves bleiben
   blockiert.

## 11. Stop-Grenzen

Der Lauf stoppt bei Breaking-API-Bedarf, unklarer Datenformat- oder
Kompatibilitätsentscheidung, beliebiger Runtime-Typaktivierung, notwendiger
destruktiver Produktpolicy, nicht behebbaren Pflichtchecks oder einer
Scope-Ausweitung auf Wave-Anwendungen.

## 12. Kopierbarer autonomer Intake-Prompt

```text
$speckit-autonomous Use
`Lastenheft_11_Component-Data-Conformance-Hardening.md` as the binding intake
for Feature `026-component-data-conformance-hardening`.

Start only from clean synchronized main after Feature 025 is merged. Verify and
reuse the final 025 focus, lifecycle, modal, and command contracts. Preserve the
exact finding scope F010-F013 from Feature-024 Revision 2.

Do not port or modify TVDEMOS, TVFM, tv203s, or external Free Vision sources.
Do not implement destructive file-manager workflows, broad serialization
redesign, arbitrary runtime type activation, or Wave 5/6 examples. Breaking
public-contract or data-format decisions require ProductDecision and stop.

Run the complete Spec Kit lifecycle, all useful optional clarification and
review passes, and repeated Analyze to convergence. Implement test-first through
real dialog commands, focus-veto, validator, file-mode, and safe resource
roundtrip/rejection paths. Maintain XML/DocFX/A11Y, security, coverage,
version/build-counter, governance, agent parity, evidence, remote review/merge,
main sync, and retrospective. Finish with Feature 028 as the only next intake
and keep both example waves blocked.
```
