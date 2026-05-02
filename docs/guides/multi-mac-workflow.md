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
- `node` 24 LTS und `npm` fuer `tests/web-a11y/`
- Playwright + `@axe-core/playwright` als lokaler DocFX-A11y-Pruefpfad
- `lynx` als textbasierter Browser-Gegencheck

## Voraussetzungen

1. Repository ist lokal auf beiden Systemen ausgecheckt.
2. Auf beiden Systemen sind `gh`, `specify`, `codex`, `claude`, `copilot` und `gemini` installiert; `gh`, `codex`, `claude`, `copilot` und `gemini` sind authentifiziert.
3. GitHub-Remote `origin` zeigt auf `https://github.com/hindermath/TuiVision.git`.
4. .NET SDK 10 ist auf beiden Systemen verfuegbar (`dotnet --info`).
5. Node `24.x` LTS und `npm` sind auf beiden Systemen verfuegbar.
6. `lynx` ist auf beiden Systemen installiert.
7. Im Repo wurde unter `tests/web-a11y/` einmalig `npm install` und `npx playwright install chromium` ausgefuehrt.

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
node --version
npm --version
lynx -version
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
- `node --version` liefert eine gueltige LTS-Ausgabe, bevorzugt `24.x`.
- `npm --version` liefert eine gueltige Ausgabe.
- `lynx -version` liefert eine gueltige Ausgabe.
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

Nach jedem erfolgreichen DocFX-Neubau direkt den A11y-Pruefpfad ausfuehren:

```bash
cd tests/web-a11y
npm run test:docfx
cd ../..
```

Die Regel dazu ist verbindlich: Ein DocFX-Neubau ist in diesem Repository erst
dann abgeschlossen, wenn der passende Playwright-plus-axe-Smoke-Test ebenfalls
erfolgreich war. `lynx` ist der zusaetzliche textuelle Gegencheck fuer Braille-
nahe und Screenreader-nahe Lesbarkeit. Fuer einen `lynx`-Check den lokalen
Server in einem zweiten Terminal starten:

```bash
cd tests/web-a11y
npm run serve:docfx
```

Danach im anderen Terminal zum Beispiel:

```bash
lynx -dump http://127.0.0.1:8123/index.html
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
6. Nach jedem docfx-Neubau den A11y-Smoke-Test unter `tests/web-a11y/` ausfuehren; optional `lynx`-Dump pruefen.
7. Ergebnis + Doku in denselben Commit aufnehmen.

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
5. Nach jedem erfolgreichen DocFX-Neubau: `cd tests/web-a11y && npm run test:docfx` erfolgreich.
6. `lynx`-Gegencheck bei relevanten Doku-/Navigationsaenderungen mit betrachtet.
7. Doku aktualisiert (API, Guides, Beispiel-Guide, falls betroffen).
8. PR erstellt oder aktualisiert.
9. Branch ist auf `origin` gepusht.

## Installation der A11y-Voraussetzungen

Die folgenden Schritte werden einmal pro Mac benoetigt, damit die HTML-Doku
nach `Programmierung #include<everyone>` reproduzierbar geprueft werden kann.

The following steps are needed once per Mac so the HTML documentation can be
checked reproducibly according to `Programmierung #include<everyone>`.

### Node 24 LTS und lynx

```bash
brew install node@24
brew install lynx
echo 'export PATH="/opt/homebrew/opt/node@24/bin:$PATH"' >> ~/.zshrc
source ~/.zshrc
node --version
lynx -version
```

### Playwright und axe fuer dieses Repository

```bash
cd /pfad/zu/TuiVision/tests/web-a11y
npm install
npx playwright install chromium
```

### Regel fuer kuenftige DocFX-Laeufe

```bash
cd /pfad/zu/TuiVision
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Kurz gesagt:

- `docfx` erzeugt die HTML-Doku neu.
- `npm run test:docfx` prueft die erzeugten Seiten mit Playwright und
  `@axe-core/playwright`.
- `lynx` bleibt der zweite, rein textuelle Blick fuer sehbehinderte Lernende
  und fuer textorientierte Review-Pfade.

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

## Phase-8-Gate-Closure-Review / Phase-8 Gate Closure Review

Dieser Abschnitt haelt fest, wie die Kompatibilitaetsfrage fuer das
Eingangstor-Closure-Paket von `006-close-phase8-gate` bewertet wurde.

*(This section records how compatibility was evaluated for the entrance-gate
closure package of `006-close-phase8-gate`.)*

### Review-Status 2026-03-27

| Umgebung / Environment | Status fuer das Closure-Paket | Begruendung / Rationale |
|---|---|---|
| **MacBook Air M2** | Primaere Multi-Mac-Baseline | Der repo-weite Gate-Befehlssatz (`build`, `test`, `format`, bedingtes `docfx`, getrennte Coverage-Laeufe) bleibt auf der primaeren macOS-Entwicklungsumgebung der massgebliche Ablauf. |
| **Mac mini M4 Pro** | Sekundaere Multi-Mac-Baseline | Dieselbe Managed-.NET-10-Befehlsfolge bleibt ohne Plattformsplit reproduzierbar; das Closure-Paket fuehrt keinen neuen macOS-spezifischen Codepfad ein. |
| **Linux (Ubuntu 24.04 / WSL)** | Kein zusaetzlicher Rerun erforderlich | Fuer die Aufgaben `T022` bis `T034` wurden nur Coverage-, Build/Test-, Format-, Doku- und Proof-Artefakte finalisiert; die letzte runtime-nahe Linux-Evidence bleibt der dokumentierte Phase-7-PASS. |
| **Windows/WSL (Ubuntu 24.04)** | Kein zusaetzlicher Rerun erforderlich | Gleiche Begruendung wie fuer Linux: kein neuer plattformspezifischer Runtime- oder Terminalpfad in der Gate-Abschlussarbeit nach `T021`. |

### Phase-8-Review-Fazit / Phase-8 Review Conclusion

- Fuer das eigentliche Eingangstor sind die repo-weiten Qualitaetsnachweise
  (`dotnet build --configuration Release`, `dotnet test`,
  `dotnet format --verify-no-changes`, `docfx docfx.json`, Coverlet je
  Gate-Assembly) die autoritativen Artefakte.
- Linux- und Windows/WSL-Ausfuehrungen bleiben fuer spaetere
  runtime-/portabilitaetswirksame Aenderungen verpflichtend, wurden fuer die
  reinen Gate-Abschlussaufgaben dieses Pakets aber reviewbar als nicht
  zusaetzlich erforderlich dokumentiert.

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

## Welle-1-Beispiele: Startnachweise / Wave-1 Examples: Launch Evidence

Dieser Abschnitt haelt fest, wie die vier Wave-1-Beispiele auf den unterstuetzten
Plattformen bewertet wurden (Branch `007-port-wave1-examples`, 2026-03-28).

*(This section records how the four Wave-1 examples were evaluated on supported
platforms — branch `007-port-wave1-examples`, 2026-03-28.)*

### Review-Status 2026-03-28 / Review Status 2026-03-28

| Umgebung / Environment | Status | Begruendung / Rationale |
|---|---|---|
| **MacBook Air M2** | Primaere Multi-Mac-Baseline | `dotnet build --configuration Release`, `dotnet test tests/TuiVision.Examples.SmokeTests/` (41 Tests), `dotnet test` (314 Tests gesamt) und `dotnet format --verify-no-changes` auf dem primaeren Entwicklungssystem fehlerfrei. |
| **Mac mini M4 Pro** | Sekundaere Multi-Mac-Baseline | Dieselbe Managed-.NET-10-Befehlsfolge reproduzierbar; kein neuer plattformspezifischer Codepfad in den Wave-1-Beispielen. |
| **Linux (Ubuntu 24.04 / WSL)** | Kein zusaetzlicher Rerun erforderlich | Die Wave-1-Implementierung nutzt ausschliesslich `System.Console` und Framework-Abstraktionen; `videomode` erkennt fehlende `SetWindowSize`-Unterstuetzung auf Linux und zeigt den dokumentierten sichtbaren Fallback. |
| **Windows/WSL (Ubuntu 24.04)** | Kein zusaetzlicher Rerun erforderlich | Gleiche Begruendung wie fuer Linux; `videomode` wuerde unter nativem Windows `SetWindowSize` aufrufen koennen, setzt aber nicht voraus, dass die Aktion gelingt. |

### Videomode-Faehigkeitsverhalten / Videomode Capability Behaviour

`videomode` erkennt zur Laufzeit, ob `Console.SetWindowSize()` auf dem aktuellen
Terminal unterstuetzt wird:

- **Echte Groessenaenderung** (`RealTransition`): Wird ausgefuehrt, wenn das Terminal
  die API unterstuetzt (typischerweise native Windows-Konsole oder kompatible Emulatoren).
- **Sichtbarer Fallback** (`VisibleFallback`): Wird angezeigt, wenn die Groessenaenderung
  nicht moeglich ist (macOS Terminal, Linux-Terminals, WSL, CI-Umgebungen).

*(The videomode example detects at runtime whether `Console.SetWindowSize()` is supported:
real transition executes when the API works; a visible fallback is shown otherwise — this
is documented behaviour and not an error.)*

### Smoke-Testbefehl fuer Wave-1 / Smoke Test Command for Wave 1

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
```

Erwartetes Ergebnis: 41 Tests bestanden, 0 Fehler, 0 uebersprungen.
*(Expected result: 41 tests passed, 0 failed, 0 skipped.)*

---

## Controls-Revision: Laufzeitnachweise / Controls-Revision Runtime Evidence

Dieser Abschnitt haelt fest, wie die Controls-Revision auf den unterstuetzten
Plattformen bewertet wurde (Branch `008-controls-revision`, 2026-03-29).

*(This section records how the Controls revision was evaluated on supported
platforms — branch `008-controls-revision`, 2026-03-29.)*

### Ergebnis-Uebersicht 2026-03-29 / Result Summary 2026-03-29

| Umgebung / Environment | Status | Begruendung / Rationale |
|---|---|---|
| **MacBook Air M2** | Primaere Baseline | `dotnet build --configuration Release` (0 Fehler, 0 Warnungen), `dotnet test` (342 Tests gesamt, 0 Fehler), `dotnet format --verify-no-changes` (sauber), `docfx docfx.json` (0 Fehler) ausgefuehrt; `TuiVision.Controls`-Zeilenabdeckung 84,4 %. |
| **Mac mini M4 Pro** | Sekundaere Multi-Mac-Baseline | Dieselbe Managed-.NET-10-Befehlsfolge reproduzierbar; kein neuer plattformspezifischer Codepfad in der Controls-Revision. |
| **Linux (Ubuntu 24.04 / WSL)** | Kein gesonderter Rerun erforderlich | Die Controls-Revision greift ausschliesslich auf verwaltete .NET-Typen zu; kein P/Invoke, keine nativen Abhaengigkeiten, kein plattformspezifischer Verhaltenszweig eingefuehrt. |
| **Windows/WSL (Ubuntu 24.04)** | Kein gesonderter Rerun erforderlich | Gleiche Begruendung wie fuer Linux. |

### Validierte Verhaltensszenarien / Validated Behaviour Scenarios

Die folgenden Szenarien wurden durch MSTest-Abdeckung in `tests/TuiVision.Controls.Tests/` bestaetigt:

#### Menueverhalten / Menu Behaviour
- Top-Level-Wraparound (links vom ersten Eintrag → letzter Eintrag).
- Untermenü-Wraparound (unten vom letzten Untermenü-Eintrag → erster Eintrag).
- Deaktivierte Eintraege und Trennzeilen werden beim Navigieren uebersprungen.
- Fokussierter Eintrag wird visuell hervorgehoben (Weiss auf Blau).
- Enter bestaetigt und sendet den Befehl.
- Escape schliesst das Menue ohne Befehl.

#### Statuszeilen-Kontexterneuerung / Status-Line Context Refresh
- Erste passende `TStatusDef`-Definition gewinnt bei ueberlappenden Bereichen.
- Neutraler Leer-Zustand wenn keine Definition zum aktuellen Kontext passt.
- Fokuswechsel aktualisiert die Statuszeile auf die neue Kontextdefinition.
- Kompatibilitaets-Fallback auf `GetStatusHints()` wenn keine Definitionen konfiguriert.

#### Fenster-Schliessen und Verschieben (SC-003) / Window Close and Move (SC-003)
- Schliess-Affordanz (×) sichtbar bei `WindowFlags.Close`.
- Ctrl+W schliesst das Fenster.
- Escape schliesst nur wenn kein Kind-View das Ereignis verbraucht hat (bewachtes Escape).
- Ctrl+F5 startet den Verschiebe-Modus; Pfeiltasten zeigen Vorschauposition.
- Enter uebernimmt die neue Position.
- Escape stellt die Ausgangsposition wieder her.

#### Dialog-Validierung / Dialog Validation
- `Valid(ushort command)` blockiert Schliessen wenn `false`.
- Akzeptierte Schliessanfragen liefern das modale Ergebnis.

### SC-003-Wiederholungsmatrix / SC-003 Repeated-Run Matrix

Die folgenden Szenarien wurden in 20 aufeinanderfolgenden `dotnet test`-Durchlaeufen
auf dem MacBook Air M2 ohne eine einzige Fehlerausnahme bestaetigt:

| Szenario | Durchlaeufe | Erste-Versuch-Erfolgsrate |
|---|---|---|
| TWindow_CtrlW_ClosesWindow | 20 / 20 | 100 % |
| TWindow_Escape_ClosesWhenNotConsumedByChild | 20 / 20 | 100 % |
| TWindow_Escape_DoesNotCloseWhenConsumedByChild | 20 / 20 | 100 % |
| TWindow_CtrlF5_EntersMoveMode | 20 / 20 | 100 % |
| TWindow_MoveMode_EscapeRestoresOriginalPosition | 20 / 20 | 100 % |

*(All five SC-003 close/move scenarios passed in every one of 20 consecutive `dotnet test` runs on MacBook Air M2.)*
### 009-controls-widgets-and-collections Repeated-Run Matrix

Die folgenden Szenarien wurden in 20 aufeinanderfolgenden `dotnet test`-Durchlaeufen
auf dem MacBook Air M2 ohne eine einzige Fehlerausnahme bestaetigt:

| Szenario | Durchlaeufe | Erste-Versuch-Erfolgsrate |
|---|---|---|
| WidgetAcceptance_ListViewer_FocusNavigation | 20 / 20 | 100 % |
| WidgetAcceptance_ComboBox_DropDownCycle | 20 / 20 | 100 % |
| WidgetAcceptance_ProgressBar_StateTransition | 20 / 20 | 100 % |
| WidgetAcceptance_ParamText_ValueRefresh | 20 / 20 | 100 % |
| WidgetAcceptance_ManagedClipboard_CutAndPaste | 20 / 20 | 100 % |
| WidgetAcceptance_History_SessionRecall | 20 / 20 | 100 % |
| WidgetAcceptance_AllWidgets_FirstRedraw | 20 / 20 | 100 % |

*(All seven widget/collection acceptance scenarios passed in every one of 20 consecutive `dotnet test` runs on MacBook Air M2.)*

### Testbefehl fuer Widgets und Collections / Test Command for Widgets and Collections

```bash
dotnet test tests/TuiVision.Controls.Tests/ --filter "FullyQualifiedName~WidgetAcceptance" --configuration Release
```

Erwartetes Ergebnis: 7 Tests bestanden, 0 Fehler, 0 uebersprungen.
*(Expected result: 7 tests passed, 0 failed, 0 skipped.)*

---

## Troubleshooting
## 009-controls-widgets-and-collections – Validation Evidence

Dieser Abschnitt dokumentiert die Laufzeitnachweise fuer `009-controls-widgets-and-collections`
auf den unterstuetzten Plattformen (Branch `009-controls-widgets-and-collections`, 2026-03-30).

*(This section documents the runtime evidence for `009-controls-widgets-and-collections`
on supported platforms — branch `009-controls-widgets-and-collections`, 2026-03-30.)*

| Host | Build | Tests | Coverage | Notes |
|------|-------|-------|----------|-------|
| MacBook Air M2 | PASS | 268 PASS, 0 FAIL | 84.78 % (Controls) | Primary path |
| Mac mini M4 Pro | pending | pending | pending | Secondary Mac |
| Linux / WSL Ubuntu 24.04 | pending | pending | pending | Supplemental |
| Windows 10/11 WSL2 | pending | pending | pending | Supplemental |

Validation to be recorded when quality gates run in T024/T025.

*(Validierung wird aufgezeichnet, sobald die Qualitaetstore in T024/T025 durchlaufen werden.)*

## 010-standard-dialogs-designer - Validation Evidence

Dieser Abschnitt dokumentiert die lokalen Nachweise fuer
`010-standard-dialogs-designer` auf dem primaeren Multi-Mac-Pfad am
2026-05-02.

This section documents local evidence for `010-standard-dialogs-designer` on
the primary Multi-Mac path on 2026-05-02.

| Host | Build | Controls | Serialization | Notes |
|------|-------|----------|---------------|-------|
| MacBook Air M2 | PASS (`dotnet build --configuration Release`) | PASS, 281 Tests | PASS, 18 Tests | Keyboard-only, text-first, non-destructive file decisions, persisted-input rejection |
| Mac mini M4 Pro | pending | pending | pending | Secondary Mac |
| Linux / WSL Ubuntu 24.04 | pending | pending | pending | Supplemental |
| Windows 10/11 WSL2 | pending | pending | pending | Supplemental |

Additional MacBook Air M2 evidence: `dotnet test` passed with 439 tests, the
gate-relevant Cobertura files reported `Core` 89.11 %, `Controls` 82.85 %,
`Serialization` 87.00 %, `Compatibility` 80.95 %, and `Drivers.Console`
76.76 % line coverage, `docfx docfx.json` passed, and `npm run test:docfx`
passed with 2/2 Playwright/axe smoke tests. `dotnet format
--verify-no-changes` passed after the repository line-ending rule was aligned
to LF and the five MSTest settings files were normalized to UTF-8 without BOM.

A11Y proof: standard-dialog flow tests assert keyboard reachability through
`StandardDialogFlowState.KeyboardReachable`; validation and fallback states are
plain text (`StandardDialogValidationMessage`, `FallbackMessage`,
`FallbackReason`). No mouse-only acceptance path was added.

Security proof: file dialogs return caller-visible decisions only and do not
perform file content I/O. Persisted dialog descriptions reject truncated,
trailing, unsupported-version, runtime-state, and semantic invalid input before
runtime dialog creation.

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
