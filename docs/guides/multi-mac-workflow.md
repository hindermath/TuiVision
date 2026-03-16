# Multi-Mac Workflow (MacBook Air M2 + Mac mini M4 Pro)

## Zweck
Dieser Guide beschreibt einen reproduzierbaren Entwicklungsablauf fuer TuiVision
auf zwei macOS-Systemen:

- MacBook Air M2
- Mac mini M4 Pro

Verwendete Tools:

- `gh` (GitHub CLI, authentifiziert)
- `codex` (Codex CLI, authentifiziert)

## Voraussetzungen

1. Repository ist lokal auf beiden Systemen ausgecheckt.
2. Auf beiden Systemen sind `gh` und `codex` installiert und authentifiziert.
3. GitHub-Remote `origin` zeigt auf `https://github.com/hindermath/TuiVision.git`.
4. .NET SDK 10 ist auf beiden Systemen verfuegbar (`dotnet --info`).

## Einmaliger Check pro System

```bash
gh auth status
gh --version
codex --version || codex --help
codex --help
git remote -v
dotnet --info
```

Erwartung:

- `gh auth status` zeigt einen aktiven Login.
- `gh --version` liefert eine gueltige Versionsausgabe.
- `codex --version` (oder alternativ `codex --help`) liefert eine gueltige Ausgabe.
- `codex --help` liefert die CLI-Hilfe ohne Fehler.
- `git remote -v` zeigt `origin` auf das TuiVision-Repository.
- `dotnet --info` zeigt eine installierte .NET-10-Umgebung.

## Start eines Arbeitstags (egal auf welchem Mac)

```bash
git checkout main
git pull --ff-only origin main
dotnet --info
```

Wenn an einem Feature gearbeitet wird:

```bash
git checkout -b codex/<kurze-beschreibung>
```

## Entwicklungsschleife

```bash
dotnet restore
dotnet build
dotnet test
```

Wenn sich oeffentliche API oder XML-Kommentare geaendert haben, danach docfx neu erzeugen:

```bash
if [[ -f "docs/docfx.json" ]]; then
  dotnet tool update --global docfx || dotnet tool install --global docfx
  export PATH="$PATH:$HOME/.dotnet/tools"
  docfx docs/docfx.json
else
  echo "docs/docfx.json nicht gefunden - docfx-Schritt uebersprungen."
fi
```

Dokumentation aktualisieren, wenn Code angepasst wurde:

- API-/XML-Kommentare
- Guides unter `docs/guides/`
- Beispiel-Guides unter `docs/guides/examples/`

## Codex-Workflow (lokal)

Beispielaufruf:

```bash
direnv allow
codex
```

Projektlokale Einstellung:
`CODEX_HOME` wird in diesem Repository ueber `.envrc` auf `./.codex` gesetzt.
Wenn `direnv` nicht verwendet wird, starte Codex stattdessen ueber
`./scripts/codex-local.sh`.

Project-local setting:
`CODEX_HOME` is set in this repository via `.envrc` to `./.codex`.
If `direnv` is not in use, start Codex through `./scripts/codex-local.sh`
instead.

Empfehlung fuer Sessions:

1. Vor Session-Beginn `git status` pruefen.
2. Aufgabenbezug klar in der Session formulieren (Datei/Abschnitt/Testziel).
3. Nach Session: `dotnet build` und `dotnet test` ausfuehren.
4. Wenn API-/XML-Kommentare geaendert wurden: den obigen docfx-Schritt ausfuehren.
5. Ergebnis + Doku in denselben Commit aufnehmen.

## Commit und Push

```bash
git status
git add -A
git commit -m "Kurzbeschreibung der Aenderung"
git push -u origin codex/<kurze-beschreibung>
```

## Pull Request mit gh

```bash
gh pr create --fill
gh pr view --web
```

Nach Review-Anpassungen:

```bash
git add -A
git commit -m "Review-Feedback umgesetzt"
git push
```

## Wechsel zwischen den beiden Macs

Vor dem Wechsel (auf System A):

```bash
git status
git push
```

Nach dem Wechsel (auf System B):

```bash
git fetch origin
git checkout codex/<kurze-beschreibung>
git pull --ff-only origin codex/<kurze-beschreibung>
```

## Abschluss einer Aufgabe

1. `dotnet build` erfolgreich.
2. `dotnet test` erfolgreich.
3. Bei API-/XML-Kommentar-Aenderungen: docfx-Schritt erfolgreich (falls `docs/docfx.json` vorhanden).
4. Doku aktualisiert (API, Guides, Beispiel-Guide, falls betroffen).
5. PR erstellt oder aktualisiert.
6. Branch ist auf `origin` gepusht.

## Troubleshooting

### `gh auth status` zeigt kein Login
```bash
gh auth login
```

### `docfx` wird nicht gefunden
```bash
if [[ -f "docs/docfx.json" ]]; then
  dotnet tool update --global docfx || dotnet tool install --global docfx
  export PATH="$PATH:$HOME/.dotnet/tools"
  docfx docs/docfx.json
else
  echo "docs/docfx.json nicht gefunden."
fi
```

### Branch ist auf beiden Macs unterschiedlich
```bash
git fetch origin
git status
git pull --ff-only
```

### Unerwartete lokale Aenderungen
```bash
git status
git diff
```
Dann bewusst entscheiden: committen, stashen oder verwerfen.
