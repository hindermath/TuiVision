# Clipboard Beispiel / Clipboard Example

## Deutsch

`Clipboard` portiert das historische Zwischenablage-Beispiel aus
`tv203s/contrib/tvision/examples/clipboard/`. Der verwaltete Pfad zeigt Copy,
Cut und Paste ueber `ManagedClipboard`. Ein isolierter oder nicht verfuegbarer
Clipboard-Zustand wird sichtbar gemeldet und nicht still uebersprungen.

Erwarteter Pfad: Text setzen, kopieren, ausschneiden, wieder einfuegen und den
Fallback-Zustand pruefen.

Barrierefreiheit: Alle Nachweise sind text-first und keyboard-first. Farbe oder
Maus ist nicht erforderlich.

Akzeptierte Einschraenkung: siehe `docs/architecture/architecture-risks.md`.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Clipboard"
dotnet run --project examples/Clipboard
```

## English

`Clipboard` ports the historical clipboard example from
`tv203s/contrib/tvision/examples/clipboard/`. The managed path shows copy, cut,
and paste through `ManagedClipboard`. An isolated or unavailable clipboard state
is reported visibly instead of being skipped silently.

Expected path: set text, copy, cut, paste back, and check the fallback state.

Accessibility: all proof is text-first and keyboard-first. Color or mouse input
is not required.

Accepted limitation: see `docs/architecture/architecture-risks.md`.

