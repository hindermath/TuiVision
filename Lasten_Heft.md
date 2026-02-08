# TuiVision

Portierung des Turbo Vision C/C++ Source nach C#/.Net 10



## Anforderung

- Im Ordner tv203s befindet sich der Sourcecode des Turbo Vision Frameworks 2.03 von Borland/Ingres.
- Die Quelle ist https://sourceforge.net/projects/tvision/files/DOS_Win32/2.0.3/
- Das Turbo Vision Framework soll nach C#/.Net 10 portiert werden.

## Technische Anforderungen

- Es soll ein lokales git-Repository angelegt werden
- eine .gitignore die JetBrains IDEs, C#, .Net, VS Code und Visual Studio ignoriert
- das git Repo soll auf github veröffentlicht werden unter dem URL https://github.com/hindermath/TuiVision.git
- zu Verwenden sind C#/.Net 10
- mit docfx für die Dokumentation der API
- mit MSTest für die Unit Tests
- Es soll eine Projektstruktur erstellt werden, die den Best Practices für C#/.Net Projekten entspricht.
- Es sollen Unit Tests für die portierten Klassen und Methoden erstellt werden.

### Optinale Anforderungen

- Es soll ein CI/CD Workflow auf Github Actions erstellt werden, der die Unit Tests ausführt
- Es soll ein Nuget-Paket erstellt werden, das die Portierung des Frameworks enthält und veröffentlicht werden kann auf nuget.org. Das ist optional, aber wünschenswert.
- Es soll eine Dokumentation erstellt werden, die die Nutzung des portierten Frameworks beschreibt.
- Zu prüfen ist auch die Möglichkeit, die Portierung so zu gestalten, dass sie plattformübergreifend ist, also auf Windows, Linux und macOS lauffähig ist.
- Es soll geprüft werden, ob es möglich ist, die Portierung so zu gestalten, dass sie auch in WebAssembly (Blazor) lauffähig ist.
- Es soll geprüft werden, ob es Sinn macht den Port mit Hilfe des NuGet-Pakets Terminal.GUI zu erstellen, um die plattformübergreifende Nutzung zu erleichtern.
- Für weitere Quellen gibt es hier Free Vision, ein Pascal Port für den Free Pascal Compiler unter dem URL https://github.com/fpc/FPCSource/tree/main/packages/fv

## Ziel

- Es soll ein Pflichtenheft zur Portierung erstellt werden für diesen Port.
- Entsprechende Unit Tests und möglichen Tests zur Qualitäts-kontrolle sollen enthalten sein.
- Die vorhandenen und identifizierten Beispiel-Programme sollen unter Nutzung des Framework ebenfalls portiert werden. 