# Multi-Mac Workflow (MacBook Air M2 + Mac mini M4 Pro)

## Zweck
Dieser Guide beschreibt einen reproduzierbaren Entwicklungsablauf fuer TuiVision
auf zwei macOS-Systemen:

- MacBook Air M2
- Mac mini M4 Pro

Verwendete Tools:

- `gh` (GitHub CLI, authentifiziert)
- `specify` (GitHub Spec-Kit CLI, installiert)
- `codex` (Codex CLI, authentifiziert)
- `claude` (Claude Code CLI, authentifiziert)
- `copilot` (Copilot CLI, authentifiziert)
- `gemini` (Gemini CLI, authentifiziert)

## Voraussetzungen

1. Repository ist lokal auf beiden Systemen ausgecheckt.
2. Auf beiden Systemen sind `gh`, `specify`, `codex`, `claude`, `copilot` und `gemini` installiert; `gh`, `codex`, `claude`, `copilot` und `gemini` sind authentifiziert.
3. GitHub-Remote `origin` zeigt auf `https://github.com/hindermath/TuiVision.git`.
4. .NET SDK 10 ist auf beiden Systemen verfuegbar (`dotnet --info`).

## Einmaliger Check pro System

```bash
gh auth status
gh --version
specify --version || specify --help
specify check
codex --version || codex --help
claude --version || claude --help
copilot --version || copilot --help
gemini --version || gemini --help
dotnet --version || dotnet --help
git remote -v
dotnet --list-sdks
```

Erwartung:

- `gh auth status` zeigt einen aktiven Login.
- `gh --version` liefert eine gueltige Versionsausgabe.
- `specify --version` (oder alternativ `specify --help`) liefert eine gueltige Ausgabe.
- `specify check` bestaetigt, dass alle fuer Spec-Kit benoetigten Tools installiert sind.
- `codex --version` (oder alternativ `codex --help`) liefert eine gueltige Ausgabe.
- `claude --version` (oder alternativ `claude --help`) liefert eine gueltige Ausgabe.
- `copilot --version` (oder alternativ `copilot --help`) liefert eine gueltige Ausgabe.
- `gemini --version` (oder alternativ `gemini --help`) liefert eine gueltige Ausgabe.
- `git remote -v` zeigt `origin` auf das TuiVision-Repository.
- `dotnet --list-sdks` zeigt eine installierte .NET-10-Umgebung.

## Start eines Arbeitstags (egal auf welchem Mac)

```bash
git checkout main
git pull --ff-only origin main
dotnet --list-sdks
specify --version || specify --help
specify check
```

Wenn an einem Feature gearbeitet wird:

```bash
git checkout -b <agent>/<kurze-beschreibung>
```

## Entwicklungsschleife

```bash
dotnet restore
dotnet build
dotnet test
```

Wenn sich oeffentliche API oder XML-Kommentare geaendert haben, danach docfx neu erzeugen:

```bash
if [[ -f "docfx.json" ]]; then
  dotnet tool update --global docfx || dotnet tool install --global docfx
  export PATH="$PATH:$HOME/.dotnet/tools"
  docfx docfx.json
else
  echo "docfx.json nicht gefunden - docfx-Schritt uebersprungen."
fi
```

Dokumentation aktualisieren, wenn Code angepasst wurde:

- API-/XML-Kommentare
- Guides unter `docs/guides/`
- Beispiel-Guides unter `docs/guides/examples/`

## KI-Agenten-Workflow (lokal)

Beispielaufrufe:

```bash
direnv allow
codex
```

```bash
direnv allow
claude
```

```bash
direnv allow
copilot
```

```bash
direnv allow
gemini
```

Projektlokale Einstellung:
`CODEX_HOME` wird in diesem Repository ueber `.envrc` auf `./.codex` gesetzt.
Wenn `direnv` nicht verwendet wird, starte Codex stattdessen ueber
`./scripts/codex-local.sh`.

Spec-Kit-Voraussetzung:
`specify` muss auf beiden Macs installiert sein, damit `specify`-Laeufe und
Spec-Kit-Updates auf beiden Systemen ohne Umwege moeglich bleiben. Vor
Spec-Kit-Arbeiten sollte `specify check` bestaetigen, dass alle benoetigten
Tools installiert sind.

Installation oder Update von `specify`:

```bash
# Erstinstallation aus dem aktuellen main-Stand
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git

# Upgrade auf einen konkreten Release-Tag
uv tool install specify-cli --force --from git+https://github.com/github/spec-kit.git@vX.Y.Z
```

Den aktuellen Release-Tag vor einem gezielten Upgrade unter
`https://github.com/github/spec-kit/releases` pruefen.

Project-local setting:
`CODEX_HOME` is set in this repository via `.envrc` to `./.codex`.
If `direnv` is not in use, start Codex through `./scripts/codex-local.sh`
instead.

Empfehlung fuer Sessions:

1. Vor Session-Beginn `git status` pruefen.
2. Aufgabenbezug klar in der Session formulieren (Datei/Abschnitt/Testziel).
3. Wenn die Session Spec-Kit-Artefakte oder Workflow-Updates betrifft: `specify --version || specify --help` und `specify check` ausfuehren.
4. Nach Session: `dotnet build` und `dotnet test` ausfuehren.
5. Wenn API-/XML-Kommentare geaendert wurden: den obigen docfx-Schritt ausfuehren.
6. Ergebnis + Doku in denselben Commit aufnehmen.

## Commit und Push

```bash
git status
git add -A
git commit -m "Kurzbeschreibung der Aenderung"
git push -u origin <agent>/<kurze-beschreibung>
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
git checkout <agent>/<kurze-beschreibung>
git pull --ff-only origin <agent>/<kurze-beschreibung>
```

## Abschluss einer Aufgabe

1. `dotnet build` erfolgreich.
2. `dotnet test` erfolgreich.
3. Bei Spec-Kit-Arbeiten: `specify --version` oder `specify --help` sowie `specify check` auf dem verwendeten Mac erfolgreich.
4. Bei API-/XML-Kommentar-Aenderungen: docfx-Schritt erfolgreich (falls `docfx.json` im Projektwurzelverzeichnis vorhanden).
5. Doku aktualisiert (API, Guides, Beispiel-Guide, falls betroffen).
6. PR erstellt oder aktualisiert.
7. Branch ist auf `origin` gepusht.

## Phase-7-Kompatibilitaetsnachweis / Phase-7 Compatibility Evidence

Dieser Abschnitt dokumentiert die fuer `M-07` erforderlichen Kompatibilitaetspruefungen fuer
die drei Zielumgebungen zusaetzlich zum primaeren Multi-Mac-Workflow.

*(This section documents the M-07 required compatibility checks for the three target environments
in addition to the primary Multi-Mac workflow.)*

### Pruefbefehlssatz / Validation Command Set

Auf jeder Zielumgebung ausfuehren / Execute on each target environment:

```bash
dotnet restore
dotnet build --configuration Release
dotnet test tests/TuiVision.Drivers.Tests/ --configuration Release
dotnet test --configuration Release
dotnet format --verify-no-changes
```

### Umgebungen und Ergebnisse / Environments and Results

| Umgebung / Environment | Validierungsmodus | Befehle / Commands | Ergebnis / Result |
|---|---|---|---|
| **MacBook Air M2** | lokal / local | Vollstaendiger Pruefbefehlssatz | PASS — primaere Entwicklungsumgebung |
| **Mac mini M4 Pro** | lokal / local | Vollstaendiger Pruefbefehlssatz | PASS — sekundaere Entwicklungsumgebung |
| **Linux (Ubuntu 24.04 / WSL)** | manuell / manual | `dotnet build --configuration Release && dotnet test tests/TuiVision.Drivers.Tests/` | PASS — manuell ausgefuehrt; noch kein CI-Gate |
| **Windows/WSL (Ubuntu 24.04)** | manuell / manual | `dotnet build --configuration Release && dotnet test tests/TuiVision.Drivers.Tests/` | PASS — manuell ausgefuehrt; noch kein CI-Gate |

### Vorgehen fuer Linux / Procedure for Linux

```bash
# Ubuntu 24.04 (nativ oder WSL)
sudo apt-get update
sudo apt-get install -y dotnet-sdk-10.0
git clone https://github.com/hindermath/TuiVision.git
cd TuiVision
git checkout 005-driver-consolidation-m07
dotnet restore
dotnet build --configuration Release
dotnet test tests/TuiVision.Drivers.Tests/ --configuration Release
```

Erwartetes Ergebnis: Alle Treibertests bestehen ohne plattformspezifische Fehler.
*(Expected result: All driver tests pass without platform-specific failures.)*

### Vorgehen fuer Windows/WSL / Procedure for Windows/WSL

```powershell
# Windows-Vorbereitung: WSL mit Ubuntu 24.04 installieren
wsl --install -d Ubuntu-24.04
```

```bash
# In WSL-Shell / In WSL shell
sudo apt-get update
sudo apt-get install -y dotnet-sdk-10.0
git clone https://github.com/hindermath/TuiVision.git
cd TuiVision
git checkout 005-driver-consolidation-m07
dotnet restore
dotnet build --configuration Release
dotnet test tests/TuiVision.Drivers.Tests/ --configuration Release
```

Erwartetes Ergebnis: Alle Treibertests bestehen. WSL vermittelt Linux-Kompatibilitaet fuer den
Windows-Nachweis. Native Windows-Konsolen-APIs werden im verwalteten Modell durch
`System.Console` abstrahiert.

*(Expected result: All driver tests pass. WSL provides Linux compatibility for the Windows proof.
Native Windows Console APIs are abstracted by System.Console in the managed model.)*

### Kompatibilitaetskaviats / Compatibility Caveats

- DOS-, QNX4-, QNXrtp-, WinGR- und X11-Plattformen werden bewusst nicht unterstuetzt;
  dies ist in `docs/porting-status.md` dokumentiert.
- Mauseingabe auf Linux/WSL benoetigt einen kompatiblen Terminal-Emulator mit
  Mausereignis-Unterstuetzung; Testlaeuf ueber CI ohne Display-Server schliessen diesen
  Pfad explizit aus.
- Farb- und Unicode-Ausgabe haengt vom Host-Terminal ab; funktionale Tests validieren
  Pufferzustand statt Terminal-spezifisches Rendering.

*(DOS, QNX4, QNXrtp, WinGR, and X11 platforms are consciously not supported; documented in
docs/porting-status.md. Mouse input on Linux/WSL requires a compatible terminal emulator with
mouse-event support; CI runs without a display server explicitly exclude this path.)*

---

## Troubleshooting

### `gh auth status` zeigt kein Login
```bash
gh auth login
```

### `docfx` wird nicht gefunden
```bash
if [[ -f "docfx.json" ]]; then
  dotnet tool update --global docfx || dotnet tool install --global docfx
  export PATH="$PATH:$HOME/.dotnet/tools"
  docfx docfx.json
else
  echo "docfx.json nicht gefunden."
fi
```

### `specify` wird nicht gefunden
```bash
specify --help
```

Wenn der Befehl nicht aufloest, GitHub Spec-Kit CLI auf dem betroffenen Mac
installieren oder die lokale Shell-/PATH-Konfiguration korrigieren:

```bash
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
```

### `specify check` meldet fehlende Tools
```bash
specify check
```

Fehlende Abhaengigkeiten auf dem betroffenen Mac nachinstallieren und den Check
erneut ausfuehren, bis alle erforderlichen Tools als verfuegbar gemeldet werden.
Wenn zusaetzlich ein CLI-Update noetig ist, den passenden Release-Tag unter
`https://github.com/github/spec-kit/releases` pruefen und danach zum Beispiel:

```bash
uv tool install specify-cli --force --from git+https://github.com/github/spec-kit.git@vX.Y.Z
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
