# maintain-workspace-storage(1)

## NAME

`maintain-workspace-storage` - inventarisiert und bereinigt verifizierte
Workspace-Buildausgaben und Caches / inventories and reclaims verified
workspace build outputs and caches

## SYNOPSIS

```bash
bash scripts/maintain-workspace-storage.sh [OPTIONEN]
```

```powershell
pwsh -NoProfile -File scripts/maintain-workspace-storage.ps1 [OPTIONEN]
```

## DESCRIPTION

Das Skript verarbeitet ausschließlich aktive Level-2-Repositories aus dem
lokalen Register. Ein Repo-Kandidat muss im Repo liegen, darf kein Symlink
sein und muss von Git als ignoriert und nicht getrackt nachgewiesen werden.
Das Standardprofil `safe` berücksichtigt sieben Tage Aufbewahrung. Sind
weniger als 15 Prozent des Dateisystems frei, wird Pressure Mode aktiviert und
sichere Kandidaten dürfen unabhängig vom Alter bereinigt werden.

*The script processes only active Level-2 repositories from the local
registry. Every repository candidate must remain inside its repository, must
not be a symlink, and must be proven ignored and untracked by Git. The default
`safe` profile keeps outputs for seven days. Below 15 percent free filesystem
space, pressure mode makes safe candidates eligible regardless of age.*

Das Profil `deep` nimmt zusätzlich wiederherstellbare Dependency-Caches auf
und erfordert in einem echten Lauf `--confirm-deep-cleanup` beziehungsweise
`-ConfirmDeepCleanup`. `none` ist ein explizites No-op-Profil. `check-only`
inventarisiert, Dry-run/WhatIf zeigt den Plan und nur ein bestätigter echter
Lauf führt Providerbefehle oder Löschungen aus.

Providerwarnungen werden im privaten, atomaren JSON-Bericht festgehalten und
haben Exitcode `0`. Aufruf-, Policy-, Pfad- und Betriebsfehler haben Exitcode
`2`. Der Bericht liegt standardmäßig unter
`~/.home-baseline/reports/workspace-storage-<RUN-ID>.json` und erhält nur
Benutzerzugriff.

## NON-MSL ADAPTERS

`C64Projects/cc65` verwendet absichtlich C89 und 6502-Assembly für 8-Bit-
Kompatibilität. Safe führt nur vorab geprüfte native `make ... clean`-Gruppen
für Hostwerkzeuge, Laufzeitbibliotheken, generierte Dokumentation und
Regression-Arbeitsverzeichnisse aus. Sample- und Targettest-Nachweise wie
Disk-Images, Zielbinärdateien, Maps, Labels und Debuglisten bleiben erhalten.
`make zap` wird nie verwendet; Root-`make clean` gehört nur zu `deep`. Weicht
dessen Vorschau von den deklarierten Pfaden ab, protokolliert Deep die
Scope-Warnung und fällt auf die einzeln geprüften Safe-Gruppen zurück.

*`make zap` is never used, and root `make clean` belongs to `deep` only. If its
preview exceeds the declared paths, Deep records the scope warning and falls
back to the individually proven Safe groups.*

`CLionProjects/tvision` bleibt wegen Quell-/ABI-Kompatibilität C++14. Ein
CMake-Buildverzeichnis wird nur akzeptiert, wenn sein `CMakeCache.txt` auf
eine Repo-interne Quelle mit getrackter `CMakeLists.txt` zeigt. Verschachtelte
`_deps`-Builds werden dedupliziert. Der echte Lauf verwendet zunächst
`cmake --build <DIR> --target clean` und entfernt danach den verifizierten
Buildbaum.

*The curated non-MSL adapters preserve each repository's constitutional
justification and compensating controls. Unknown non-MSL repositories are
reported but never handled by generic deletion rules.*

## CACHE AND CONTAINER BOUNDARIES

- npm: `verify` normal, `clean --force` nur in Pressure Mode oder `deep`.
- NuGet: HTTP-, Plugin- und Temp-Caches in Safe; Global Packages nur `deep`.
- Go: Build-/Testcache in Pressure Mode, Module Cache nur `deep`.
- Homebrew: nativer `brew cleanup`, in Pressure Mode/Deep mit `--prune=all`.
- Container: ausschließlich dangling images; keine Volumes, kein `--all`, kein
  `system prune`.
- Podman auf macOS: Socket wird pro Lauf aus `podman machine inspect`
  ermittelt und als existierender Unix-Socket verifiziert. TCP-Fallbacks und
  gespeicherte Hostpfade sind unzulässig.

Cargo-, Maven-, Gradle- und Swift-Dependency-Stores bleiben in Version 1
unberührt.

## OPTIONS

| Bash | PowerShell | Wirkung / Effect |
|---|---|---|
| `--check-only` | `-CheckOnly` | Nur inventarisieren / Inventory only |
| `--dry-run` | `-WhatIf` | Bereinigungsplan zeigen / Preview cleanup plan |
| `--profile safe\|deep\|none` | `-CleanupProfile Safe\|Deep\|None` | Profil wählen / Select profile |
| `--confirm-deep-cleanup` | `-ConfirmDeepCleanup` | Echten Deep-Lauf bestätigen / Confirm update Deep run |
| `--home-dir PATH` | `-HomeDir PATH` | Alternatives Home / Alternative home |
| `--registry PATH` | `-RegistryPath PATH` | Alternatives Register / Alternative registry |
| `--policy PATH` | `-PolicyPath PATH` | Alternative Policy / Alternative policy |
| `--result-file PATH` | `-ResultFile PATH` | Privater Bericht / Private report |
| `--run-id UUID` | `-RunId UUID` | Laufkorrelation / Run correlation |

## EXAMPLES

```bash
bash scripts/maintain-workspace-storage.sh --check-only
bash scripts/maintain-workspace-storage.sh --dry-run --profile safe
bash scripts/maintain-workspace-storage.sh --profile deep --confirm-deep-cleanup
```

```powershell
pwsh -NoProfile -File scripts/maintain-workspace-storage.ps1 -CheckOnly
pwsh -NoProfile -File scripts/maintain-workspace-storage.ps1 -WhatIf -CleanupProfile Safe
pwsh -NoProfile -File scripts/maintain-workspace-storage.ps1 -CleanupProfile Deep -ConfirmDeepCleanup
```

## SEE ALSO

`maintain-agentic-workspace(1)`,
`scripts/config/workspace-storage-maintenance.json`,
`docs/maintenance/README.md`
