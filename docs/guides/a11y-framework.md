# A11Y-Framework / Accessibility Framework

Feature 023 ergänzt eine kleine, textbasierte A11Y-Schicht für TuiVision. Sie
hilft Tastaturnutzenden, automatisierten Prüfungen und späteren
Assistive-Technik-Adaptern, ohne bereits eine native Betriebssystem-Brücke zu
behaupten.

Feature 023 adds a small, text-based accessibility layer to TuiVision. It helps
keyboard users, automated checks and future assistive-technology adapters
without claiming that a native operating-system bridge already exists.

## Start / Launch

```bash
dotnet run --project examples/A11yFramework/A11yFramework.csproj
```

## Bedienung / Operation

| Taste / Key | Wirkung / Effect |
|---|---|
| `Tab` | Fokus vorwärts / Move focus forward |
| `Shift+Tab` | Fokus rückwärts / Move focus backward |
| `F10`, Pfeile, `Enter` | Menü öffnen und bedienen / Open and operate the menu |
| Menü `Kontrast / Contrast` | High Contrast explizit umschalten / Explicitly toggle high contrast |
| `Help -> Description` | Zweisprachige Textbeschreibung öffnen / Open the bilingual text description |
| `Ctrl+Q` | Anwendung beenden / Quit the application |

## Fokus und Widget-Texte / Focus and Widget Text

`IAccessibleWidget` ist bewusst opt-in. Ein Control implementiert den Vertrag
nur, wenn es eine stabile Bezeichnung, eine sinnvolle Beschreibung und seine
aktuelle Fokusfähigkeit wahrheitsgetreu liefern kann. Nicht migrierte Views
bleiben nutzbar; ihr Fokus-Payload enthält kein erfundenes Label.

`IAccessibleWidget` is deliberately opt-in. A control implements the contract
only when it can truthfully provide a stable label, a useful description and
its current focus capability. Non-migrated views remain usable; their focus
payload contains no invented label.

Der vorhandene `cmFocusChanged`-Broadcast bleibt der einzige Shell-Pfad. Ein
typisierter Snapshot ergänzt Ziel, Text und Fokusfähigkeit. Auch Fokuswechsel
in verschachtelten Desktop-Gruppen werden bis zur Shell weitergereicht.

The existing `cmFocusChanged` broadcast remains the only shell path. A typed
snapshot adds target, text and focus capability. Focus transitions in nested
desktop groups are also propagated to the shell.

## Strukturierte Shortcuts / Structured Shortcuts

`TMenuBar` und `TStatusLine` stellen ausführbare Shortcuts als unveränderliche
Werte bereit. Eine Abfrage führt keinen Befehl aus. Separatoren, deaktivierte
Einträge, leere Commands und Statushinweise ohne expliziten Tastencode werden
nicht als ausführbare Fähigkeit gemeldet.

`TMenuBar` and `TStatusLine` expose executable shortcuts as immutable values.
Querying them executes no command. Separators, disabled entries, empty commands
and status hints without an explicit key code are not reported as executable
capabilities.

## High Contrast

`TColorScheme.HighContrast` ordnet semantische Rollen wie normalen Text,
Hervorhebung, Auswahl und Status konkreten Farben zu. Das Schema wird bewusst
aktiviert; ohne Aktivierung bleibt das bisherige Standardschema erhalten. Die
Referenz-App zeigt den Schemanamen zusätzlich als Text, damit Farbe nie die
einzige Information trägt.

`TColorScheme.HighContrast` maps semantic roles such as normal text, emphasis,
selection and status to concrete colours. The scheme is activated explicitly;
without activation, the existing default scheme remains in use. The reference
app also shows the scheme name as text so colour is never the only information.

## Nachweis / Proof

Die automatisierten Tests kombinieren reale App-Loop-Ereignisse mit konkretem
Zustand, View-Typ und gerenderten Buffer-Zellen. Eine separate Tastaturmatrix
prüft jede inventarisierte fokussierbare Control-Familie. Jede Tastenklasse hat
einen Proof oder ein begründetes `N/A`.

The automated tests combine real app-loop events with concrete state, view type
and rendered buffer cells. A separate keyboard matrix checks every inventoried
focusable control family. Every key class has a proof or a justified `N/A`.

## Grenze / Boundary

Native AT-SPI-, NSAccessibility- oder UI-Automation-Brücken, Sprachausgabe, die
Migration aller Controls und eine vollständige WCAG-Konformitätsbehauptung für
Terminals gehören nicht zu Feature 023. Die App nennt diese Grenze sichtbar als
`native bridge unavailable`.

Native AT-SPI, NSAccessibility or UI Automation bridges, speech output, migration
of every control and a complete terminal WCAG conformance claim are not part of
Feature 023. The app visibly states this boundary as `native bridge unavailable`.
