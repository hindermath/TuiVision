# Serialisierung, Records und Ablehnung / Serialization, Records, and Rejection

## Deutsch

### Lernziel und Voraussetzungen

Dieser Guide setzt den [Architekturüberblick](../architecture.md) voraus. Du
lernst, warum gespeicherte Daten eine Vertrauensgrenze sind und warum ein
teilweise geladenes Modell nicht veröffentlicht werden darf.

### Vertrag

`TuiVision.Serialization` verwendet geschlossene Record-Registries und
deterministische Binärarchive. Ein Writer schreibt bekannte Werte in stabiler
Reihenfolge. Ein Reader prüft Längen, Typen, Referenzen und das vollständige
Verbrauchen des Payloads. Ressourcen-Schlüssel bleiben exakt und
case-sensitive.

```text
untrusted bytes -> bounded reader -> known type registry -> complete model
                         | invalid
                         v
                    explicit rejection
```

Die rechte Seite entsteht erst nach vollständiger Validierung. Truncated Data,
Trailing Data, unbekannte Typen, negative Längen, ungültige Referenzen und
Zyklen werden ausdrücklich abgelehnt. Stream-Ownership wird über `leaveOpen`
bewusst festgelegt.

### Sichere Anwendung

- Verwende eine neue, vertrauenswürdige Registry für den erwarteten Vertrag.
- Lade keine CLR-Typen, Methoden oder Skripte aus Nutzdaten.
- Begrenze Größe, Tiefe und Anzahl, wenn ein Format variable Strukturen erlaubt.
- Behandle Fehlermeldungen als Teil des Ablehnungsvertrags, nicht als
  Erfolgspfad.

### Übung

Starte `examples/Tp7ResourceDemo` und führe danach den dokumentierten
malformed-input Smoke aus. Erkläre, warum der nächste gültige Ladevorgang
unabhängig bleiben muss. Nächster Schritt: [Erster Dialog](../tutorials/first-dialog.md).

## English

### Learning goal and contract

Stored data is a trust boundary. TuiVision uses closed record registries and
deterministic binary archives. Readers validate lengths, types, references,
and complete payload consumption before publishing a model. Resource keys are
exact and case-sensitive.

Truncated or trailing data, unknown types, negative lengths, invalid
references, and cycles are rejected explicitly. Stream ownership is chosen
through `leaveOpen`; data cannot select CLR types, methods, or scripts.

### Exercise

Launch `Tp7ResourceDemo`, then run its malformed-input smoke. Explain why the
next valid load must remain independent, and continue with the first-dialog
tutorial.
