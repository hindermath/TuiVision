# Autonomous Run Evidence: Wave-5 Combined Delta Closure

## Laufidentität / Run Identity

| Feld | Wert |
|---|---|
| Feature | `034-wave5-combined-delta-closure` |
| Branch | `034-wave5-combined-delta-closure` |
| Binding intake | `Lastenheft_19_Wave5-Combined-Delta-Closure.md` |
| Baseline | `4dbfa39511f774af5b0c79ac6c5518dd058f664c` |
| Intake delivery | PR #98, merge `4dbfa39511f774af5b0c79ac6c5518dd058f664c` |
| Delivery mode | `MergeAndSync` |
| Authority | Aktueller Benutzerauftrag für Implementierung, Commit, Push, PR, engen Human-Approval-Bypass, Merge und Main-Sync |
| Run ID | `9beccdb7-7c58-4e5e-a7df-40ec427aac50` |
| Interruption policy | Keine geplante Unterbrechung; unerwarteter Abbruch benötigt Status und expliziten Resume |

## Scope

### Enthalten / Included

- exakte Produktdateimengen der PRs #93 und #96;
- PR #94/#97 als kausale Closeouts und PR #95 als Prompt-Metadaten;
- 15 historische Source-Rollen, sechs Consumer, zehn Beispiele, zehn
  Funktionsproofs, zehn Showcase-Abschlüsse und zehn Guide-/Launch-Pfade;
- zehn kombinierte Entscheidungen und qualitätsbezogene Dimensionen;
- test-only Closure-Validator und positive/negative Evidence-Prüfung;
- erforderliche Status-, Reihenfolge-, Statistik-, Agent- und
  Lastenheft-Oberflächen;
- Exact-Head-Delivery und, nur falls kausal erforderlich, ein Evidence-only
  Closeout.

### Ausgeschlossen / Excluded

- Runtime- oder öffentliches Verhalten;
- API-Signaturen, Dependencies, Pakete, Projekte oder Solution;
- Produkt-, Framework- oder Beispielcode;
- Remediation eines Candidate Findings;
- Änderungen unter `TVDEMOS/`, `TVFM/`, `tv203s/` oder externen Checkouts;
- generierte DocFX-Ausgabe, API-YAML, Caches, Logs, Credentials oder
  Testergebnisse;
- Start von Wave 6, Feature 035 oder dem Post-Wave-6-Portfolio-Audit.

## Single-writer- und Versionsgrenzen / Single-writer and Version Boundaries

Serialisiert werden:

- `pr-evidence.md`
- `wave5-combined-delta.json`
- `wave5-closure.md`
- `tasks.md`
- `autonomous-run-state.json`
- `Directory.Build.props`
- `docs/project-statistics.md`
- `Pflichtenheft.md`
- `Lastenheft_Abarbeitungsreihenfolge.md`
- alle fünf Agent-Oberflächen
- archiviertes Lastenheft und ein möglicher kausaler Closeout.

Die Version ist `1.34.<patch>.<build>`. Vor jedem einzelnen
`dotnet build` oder `dotnet test` wird nur der manuelle Build-Zähler genau
einmal erhöht. `dotnet run` wird nach einem expliziten Build ausschließlich
mit `--no-build` ausgeführt.

## Preflight

| Prüfung | Ergebnis | Evidence / Grenze |
|---|---|---|
| Branch | Pass | exakt `034-wave5-combined-delta-closure` |
| Feature-Metadaten | Pass | `.specify/feature.json` zeigt auf `specs/034-wave5-combined-delta-closure` |
| Baseline-Ancestry | Pass | `4dbfa395...` ist aktueller `HEAD` und Ausgangspunkt |
| Intake-PR | Pass | PR #98 ist `MERGED`; Merge-Commit entspricht der Baseline |
| Worktree-Ownership | Pass | nur Feature-034-Artefakte, Feature-Metadaten und geplante Agent-Kontexte |
| `specify check` | Pass | Exit 0; Antigravity, Claude, Codex, Junie und OpenCode verfügbar |
| Prerequisites | Pass | Feature-Pfad plus Research, Datenmodell, Contract, Quickstart und Tasks erkannt |
| Presets | Pass | Security 0.6.0/10, Architecture 0.5.0/20, iSAQB 0.2.0/30, A11Y 0.4.0/40, Cross-Platform 0.2.0/50, Agent Parity 0.3.0/60, Autonomous Run 0.2.2/70 |
| Checklists | Pass | 101 vollständig, 0 unvollständig |
| Bash state validator | Pass | `Analyze`, `Active`, 0/164 |
| PowerShell state validator | Pass | `Analyze`, `Active`, 0/164 |
| Gate requirements | Pass | UTF-8 JSON; SHA-256 wird am Tasks-Checkpoint gebunden |
| Ignore-Regeln | Pass | `_site/`, `api/*`, `*.log`, Preset-Cache und Build-/Testausgabe sind ignoriert |

## Run Gates

| Phase | Versuch | Ergebnis | Evidence | Nächste Aktion |
|---|---:|---|---|---|
| Preflight | 1 | Pass | sauberer Feature-Ausgangspunkt und Scope-Ownership | Specify |
| Specify | 1 | Pass | `spec.md`, Requirements-Checklist | Clarify |
| Clarify | 2 | Pass | keine formale Frage; zweiter fokussierter Pass konvergiert | Checklists |
| Checklists | 1 | Pass | fünf Requirements- plus zwei Plan-Checklists, 101/101 | Plan |
| Plan | 1 | Pass | Plan, Research, Datenmodell, Contract, Quickstart | Tasks |
| Plan review | 1 | Pass | 36/36 Planprüfpunkte, keine offene Aktion | Tasks |
| Tasks | 1 | Pass | 164 eindeutige, serialisierte Tasks und 13 Gate-Anforderungen | Analyze |
| Analyze | 1 | Remediated | drei Task-Präzisierungen: Live-PR-Dateimenge, PTY `--no-build`, Showcase-Folge aus Feature-035-Delta | Analyze erneut |
| Analyze | 2 | Pass | 73 Requirements/SC abgedeckt; 0 Critical/High/Medium, 0 Constitution-Konflikte, 0 unmapped Tasks | Implement |
| Implement | 1 | Open | Closure-Datensatz, test-only Validator und Abschluss-Evidence | Foundation |
| Validate | 1 | Open | lokale, interaktive und vollständige Gates | nach Implementierung |
| Publish / Review | 1 | Open | Feature-PR und Exact-Head-Evidence | nach lokalem Kandidaten |
| MergeAndSync | 1 | Open | Feature-Merge und möglicher kausaler Closeout | nach Review-Konvergenz |

## Analyze-Konvergenz / Analyze Convergence

| Metrik | Ergebnis |
|---|---:|
| Funktionale Anforderungen | 40/40 abgedeckt |
| Governance-Anforderungen | 7/7 abgedeckt |
| Verfassungsanforderungen | 13/13 ausgerichtet |
| Erfolgskriterien | 13/13 abgedeckt |
| Tasks | 164 eindeutige IDs |
| Unmapped Requirements | 0 |
| Unmapped Tasks | 0 |
| Critical / High / Medium nach Remediation | 0 / 0 / 0 |
| Constitution-Konflikte | 0 |
| Unaufgelöste Marker | 0 |

## Verbindliche Provenienz / Binding Provenance

Die exakten GitHub-, Git-, Dateimengen- und Hash-Ergebnisse werden in
`wave5-combined-delta.json` und den folgenden Evidence-Abschnitten ergänzt.
Bis zu ihrer vollständigen Verifikation bleibt jede Abschlussaussage
`Open`.

## Referenz-Slice / Reference Slice

`Tp7Calculator` bindet zuerst PR-Pins, alle 15 Source-Blobs, `W5-005`,
Funktionsproof, Showcase-Proof, Guide/Launch und die kombinierte Entscheidung.
Der erste Red-Lauf darf ausschließlich an den planmäßig fehlenden Restzeilen
scheitern.

## Framework- und Proof-Review / Framework and Proof Review

Die Entscheidungen für `Wave5Application`, `Wave5ConsoleHost`,
`Wave5StatusLine`, `Wave5GridView`, lokale Zustandsmodelle und alle
Primary-Proofs werden nach der vollständigen Matrix eingetragen. Reine
Beispielkomposition ist zulässig; Framework-Ersatz oder unabhängige
Wiederverwendung erzeugt ein Candidate Finding.

## Governance

Vollständige Governance-Zeilen stehen im Closure-Datensatz. Die akzeptierte
Planungsgrenze lautet:

- NIST SSDF, CWE Top 25, Evidence-Integrität, Secrets, Supply Chain,
  A11Y, Plattformen, Agent-Parität und Autonomous Delivery: `Applicable`;
- ASVS, neue SBOM/VEX/SLSA-Artefakte, AI-SBOM, regulatorische Rollen,
  S-ADR, arc42-Security-Update, Zero Trust, SAMM, BSI C3A und BSI C5:
  triggerbasiert `N/A`;
- Skriptparität: `N/A`, solange kein `.sh` oder `.ps1` geändert wird;
- `.specify/templates/`: `N/A`, solange das Feature keine Templates ändert.

## Validierung / Validation

| Gate | Ergebnis | Command / Evidence |
|---|---|---|
| Static candidate | Open | finaler staged Kandidat |
| Feature-034 closure | Open | targeted Release |
| Wave-5 TP7 | Open | targeted Release |
| zehn controlled smokes | Open | `dotnet run --no-build ... -- --smoke` |
| zehn normale PTY-Pfade | Open | `dotnet run --no-build ...` |
| Full Release | Open | `dotnet test TuiVision.sln --configuration Release` |
| Coverage | Open | kanonischer Coverlet-Lauf |
| Format | Open | `dotnet format TuiVision.sln --verify-no-changes` |
| DocFX / A11Y | Open | DocFX plus Playwright/Axe |
| Security / Supply Chain | Open | lokale und Provider-Gates |
| Agent parity | Open | expliziter Repository-Root |
| Ubuntu / macOS / Windows | Open | PR-Kontext |
| Exact head | Open | Gate-Evidence-Validator |
| Reviews | Open | Kommentare, Reviewer und GraphQL-Threads |

## Abschlussgrenze / Completion Boundary

Der Feature-Head darf höchstens `ReadyForMerge` behaupten:

```text
Wave 5: BlockedPendingCausalClosure
Wave 6: BlockedPendingCausalClosure
```

Nur nach dem tatsächlichen Feature-Merge darf ein notwendiger Evidence-only
Closeout `Closed` und `EligibleForIntake` setzen. Feature 035 wird dabei nur
reserviert und nicht gestartet.
