# Laufzeitsicht Welle 2 / Runtime View Wave 2

## Normaler Start

```text
Reviewer
  |
  | dotnet run --project examples/Sdlg2
  v
Program.cs
  |
  | creates bounds from console size
  v
Sdlg2App : TApplication
  |
  | initializes menu/status/desktop through TApplication
  v
Managed console driver
  |
  | normal event loop until quit
  v
Clean shutdown
```

## Headless-Smoke-Start

```text
MSTest smoke method
  |
  | new Sdlg2App(DefaultBounds(), headless: true)
  | QueueEvents(command sequence)
  v
Sdlg2App.GetEvent()
  |
  | returns queued command events, then cmQuit
  v
TApplication.Run()
  |
  | dispatches the same command route as normal runtime
  | exits in process, no shell process and no timing dependency
  v
Assertions verify visible state
```

012 note: Every Wave-2 primary smoke follows this queued command model. Startup
plus quit remains useful as a liveness check, but primary assertions verify
state produced by command dispatch inside `Run()`.

## Scrollbarer Dialog: `sdlg2`

```text
Sdlg2 smoke
  |
  | creates Sdlg2App
  | creates horizontal + vertical scroll flow
  v
TScrollGroup
  |
  | SetLimit(30, 20)
  | ScrollTo(7, 6)
  | SetFocus(control outside viewport)
  v
Visible state
  |
  | Delta, scrollbar values, focused label and bounds are asserted
```

## Dynamischer Dialog: `dlgdsn`

```text
DlgDsn smoke
  |
  | creates or loads structured description fixture
  v
DialogDescriptionPersistenceAdapter
  |
  | maps persisted record to Controls model
  v
DialogDescriptionValidator
  |
  | accepts valid description
  | rejects malformed, incomplete, duplicate-control, invalid-navigation cases
  v
DialogDescriptionFactory
  |
  | creates runtime TDialog for the valid description
  v
Visible state and rejection messages
```

## 013 Runtime View Addendum

Deutsch: 013 ergaenzt in jedem Welle-2-Beispiel dasselbe Laufzeitmodell:
sichtbare Hauptkomponente, echte `TStatusLine`-Rueckmeldung und
`Help -> Description`. Die primaeren Smokes injizieren Commands in `app.Run()`
und pruefen konkrete Zustandswerte, View-Baum-Typen sowie BackBuffer-Regionen.
Die neue Hilfsdatei unter `examples/Shared/` bleibt beispielintern und erzeugt
keine neue Runtime-Abhaengigkeit.

English: 013 adds the same runtime model to each Wave 2 example: visible main
component, real `TStatusLine` feedback, and `Help -> Description`. Primary
smokes inject commands into `app.Run()` and verify concrete state values,
view-tree types, and back-buffer regions. The new helper file under
`examples/Shared/` remains example-internal and creates no new runtime
dependency.
