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
  v
Sdlg2App.GetEvent()
  |
  | returns cmQuit after deterministic public scenario assertions
  v
TApplication.Run()
  |
  | exits in process, no shell process and no timing dependency
  v
Assertions verify visible state
```

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

