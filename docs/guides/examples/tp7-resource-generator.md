# TP7 Resource Generator: kontrollierte Ausgabe / Controlled Output

## Zweck / Purpose

`Tp7ResourceGenerator` übernimmt den Generatorzweck aus
`TVDEMOS/GENRDEMO.PAS`: eine kleine, reproduzierbare Ressourcendatei für das
Anzeige-Beispiel erzeugen.

`Tp7ResourceGenerator` retains the generator purpose from
`TVDEMOS/GENRDEMO.PAS`: create a small, reproducible resource file for the
display example.

## Start / Launch

```bash
dotnet run --project examples/Tp7ResourceGenerator
```

Der kontrollierte Entry-Point-Smoke verwendet:

The controlled entry-point smoke uses:

```bash
dotnet run --no-build --configuration Release \
  --project examples/Tp7ResourceGenerator -- --smoke
```

## Sicherheitsgrenze / Security Boundary

Der Generator kennt nur die drei eingebauten Record-Typen und schreibt nur in
ein ausdrücklich übergebenes, test-eigenes Ausgabeverzeichnis. Absolute Pfade
und Pfad-Traversal werden abgelehnt.

The generator knows only the three built-in record types and writes only to an
explicitly supplied, test-owned output directory. Absolute paths and path
traversal are rejected.

## Proof

Ein reales `TDialog` zeigt Ziel-`TInputLine`, fokussierbaren Generate-Button,
Fortschritt, Ergebnis und Ablehnung. Tab wechselt den Fokus, Alt+G oder Enter
erzeugt innerhalb des kontrollierten Roots, und F1 beziehungsweise
`Help -> Description` erklärt die Grenze.

A real `TDialog` shows the target `TInputLine`, focusable Generate button,
progress, result, and rejection. Tab changes focus, Alt+G or Enter generates
inside the controlled root, and F1 or `Help -> Description` explains the
boundary.

Der primäre Smoke führt `app.Run()` aus, prüft Controls, Fokus, 100-Prozent-
Fortschritt, Bytes und kontrollierten Pfad und lädt dieselben Bytes mit
`Tp7ResourceDemo`. Die `48x16`-Fixture verwendet keine Benutzerdaten.

The primary smoke runs `app.Run()`, verifies controls, focus, 100 percent
progress, bytes, and the controlled path, then loads the same bytes with
`Tp7ResourceDemo`. The `48x16` fixture uses no user data.
