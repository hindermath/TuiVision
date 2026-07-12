# TvEdit Beispiel / TvEdit Example

## Deutsch

`TvEdit` zeigt den echten `TFileEditor` in einem `TEditWindow`. Start:

```bash
dotnet run --project examples/TvEdit
```

Die Tastatur bearbeitet den eingebetteten Lernpuffer. Die Statuszeile meldet
Pufferidentität, `clean` oder `modified` sowie das Ergebnis einer
Safe-Close-Entscheidung. `Help -> Description` erklärt den sichtbaren Vertrag.

Dateinachweise verwenden nur einen vom Test angelegten temporären Ordner.
Normaler Start durchsucht oder öffnet keine Benutzerdateien. Pfade außerhalb
des erlaubten Ordners und Überschreiben ohne Entscheidung werden abgelehnt.

Historisch bleibt der Zweck von `tvedit.cc` erhalten: Der kleine
Anwendungsrahmen macht den wiederverwendbaren Editor sichtbar. Die moderne
Variante bevorzugt kontrollierte Lern- und Testdaten.

Barrierefreiheit: Bearbeitungs- und Entscheidungszustände stehen als Text in
Editor und Statuszeile. Alle Hauptpfade sind per Tastatur erreichbar.

## English

`TvEdit` shows the real `TFileEditor` inside a `TEditWindow`. Launch it with:

```bash
dotnet run --project examples/TvEdit
```

Keyboard input edits the embedded learner buffer. The status line reports the
buffer identity, `clean` or `modified`, and the safe-close decision result.
`Help -> Description` explains the visible contract.

File proof uses only a temporary directory created by the test. Normal startup
does not discover or open user files. Paths outside the allowed directory and
overwrites without an explicit decision are rejected.

The historical purpose of `tvedit.cc` remains: a small application shell makes
the reusable editor visible. The modern example prefers controlled learner and
test data.

Accessibility: Editing and decision states are text in the editor and status
line. All primary paths are keyboard reachable.

## Nachweis / Proof

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~TvEdit"
```
