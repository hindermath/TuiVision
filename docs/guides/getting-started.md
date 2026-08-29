# Einstieg in TuiVision / Getting Started with TuiVision

## Deutsch

### Lernziel

Nach diesem Einstieg kannst du das Repository bauen, eine sichtbare
TuiVision-Anwendung starten und den Weg von der Anwendungsschleife bis zu Views,
Koordinaten und Serialisierung einordnen.

### Voraussetzungen

- Git und .NET SDK 10
- ein Terminal mit mindestens 80 Spalten und 25 Zeilen
- ein lokaler Checkout des Repositories

Prüfe die Umgebung im Repository-Wurzelverzeichnis:

```bash
dotnet --version
dotnet restore
dotnet build --configuration Release
```

### Erste Anwendung starten

```bash
dotnet run --project examples/Desklogo
```

`Desklogo` zeigt eine echte TuiVision-Anwendung mit Desktop, Statuszeile und
Hilfe. Öffne `Help -> Description` mit der Tastatur. Beende die Anwendung mit
`Ctrl+Q`. Wenn dein Terminal eine Fähigkeit nicht unterstützt, muss die
Anwendung einen textlichen Fallback zeigen statt Erfolg zu behaupten.

### Empfohlener Lernpfad

1. [Architektur](architecture.md): Schichten und Verantwortungen verstehen.
2. [Event-Loop](concepts/event-loop.md): Ereignisse, Commands und Zeichnen
   verfolgen.
3. [View-Hierarchie](concepts/view-hierarchy.md): Owner, Fokus und Z-Reihenfolge
   verstehen.
4. [Koordinatensystem](concepts/coordinate-system.md): lokale und globale
   Positionen sicher umrechnen.
5. [Serialisierung](concepts/serialization.md): gespeicherte Daten strikt laden.
6. [Erster Dialog](tutorials/first-dialog.md): einen vorhandenen Dialogschritt
   ausführen und untersuchen.
7. [Beispiel-Lernpfade](example-learning-paths.md): das passende nächste
   Beispiel anhand eines Lernziels auswählen.

### Übung und Abschluss

Starte `Desklogo` einmal normal und einmal mit engerem Terminalfenster. Notiere,
welche Informationen immer als Text erhalten bleiben. Danach ist der sichere
nächste Schritt der Event-Loop-Guide; ändere noch keinen Framework-Code.

## English

### Learning goal

After this introduction, you can build the repository, launch a visible
TuiVision application, and relate the application loop to views, coordinates,
and serialization.

### Prerequisites

- Git and .NET SDK 10
- a terminal with at least 80 columns and 25 rows
- a local checkout of the repository

Run the environment and build commands shown above from the repository root.

### Launch the first application

Run the `Desklogo` command above. The application uses a real desktop, status
line, and help surface. Open `Help -> Description` from the keyboard and quit
with `Ctrl+Q`. An unsupported terminal capability must produce a truthful text
fallback instead of a false success claim.

### Recommended learning path

Follow the seven linked guides in order. They move from architecture through
event dispatch and layout to persistence, then reuse an existing tutorial
dialog and the complete example catalog.

### Exercise and completion

Launch `Desklogo` once normally and once in a smaller terminal. Record which
information remains available as text. Continue with the event-loop guide
before changing framework code.
