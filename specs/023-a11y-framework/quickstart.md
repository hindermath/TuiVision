# Quickstart: A11Y Framework

## Fokus und Text / Focus and Text

Implementiere `IAccessibleWidget` nur, wenn ein Control eine stabile Bezeichnung
und Beschreibung liefern kann. Der Fokus-Broadcast enthält dann diese Texte.

Implement `IAccessibleWidget` only when a control can provide a stable label and
description. The focus broadcast then carries those texts.

## Shortcuts

Frage `IAccessibleShortcutProvider.GetAccessibleShortcuts()` ab, um Menü- oder
Statusaktionen darzustellen. Die Abfrage führt keine Aktion aus.

Query `IAccessibleShortcutProvider.GetAccessibleShortcuts()` to present menu or
status actions. Querying never executes an action.

## High Contrast

Aktiviere `TColorScheme.HighContrast` explizit an der Anwendung. Zeige den
aktiven Modus zusätzlich als Text und verlasse dich nicht nur auf Farben.

Apply `TColorScheme.HighContrast` explicitly to the application. Also display
the active mode as text and do not rely on colour alone.

## Referenznachweis / Reference proof

```bash
dotnet run --project examples/A11yFramework/A11yFramework.csproj
```

Die App unterstützt Tab, Shift+Tab, F10/Pfeile/Enter, einen direkten
Kontrast-Shortcut, Help -> Description und einen deterministischen Quit-Pfad.

The app supports Tab, Shift+Tab, F10/arrows/Enter, a direct contrast shortcut,
Help -> Description and a deterministic quit path.
