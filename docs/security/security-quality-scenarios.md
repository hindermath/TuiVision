# Sicherheits-Qualitätsszenarien / Security Quality Scenarios

**Stand / Current as of**: 2026-07-11

| ID | Stimulus und Umgebung / Stimulus and environment | Erwartete Reaktion / Expected response | Messbarer Nachweis / Measurable evidence |
|---|---|---|---|
| QS-01 | Ungültige Terminal-/Eventeingabe im lokalen Prozess / Invalid terminal or event input | Sichere Ablehnung oder dokumentierter Fallback ohne Crash oder Secret-Ausgabe | Core-, Controls- und Driver-Tests PASS |
| QS-02 | Truncated, trailing, unknown oder cyclic Serialization-Payload | Explizite Ausnahme/Ablehnung ohne unbounded allocation | Serialization negative tests PASS |
| QS-03 | Ungültiger oder manipulierter Datei-/Ressourcenpfad | Keine unbeabsichtigte Dateiinhalt-Lese-/Schreiboperation | File/dialog tests and controlled fixtures PASS |
| QS-04 | Rename-Script erhält untracked, unsafe oder unvollständigen Input | Non-zero vor Mutation; Preview ändert nichts | Bash/PowerShell disposable-repo contract tests PASS |
| QS-05 | Rename mit fremden staged Änderungen | Rename-Commit enthält nur alte/neue Lastenheft-Pfade | Index/commit isolation assertions PASS |
| QS-06 | Dependency- oder Workflow-Änderung | Vulnerability review, immutable Action-SHA und SBOM-Pfad sind prüfbar | Package checks, workflow scan, CycloneDX JSON PASS |
| QS-07 | Security-Finding mit Critical/High | Merge stoppt bis Remediation und Proof vorliegen | Finding ledger has zero unresolved Critical/High |
| QS-08 | Human-only Rechts-/Providerentscheidung | Keine automatische Compliance-Aussage; Owner und Trigger bleiben sichtbar | Complete `Open` row in control/evidence ledger |
| QS-09 | Security-Dokumentation wird geändert | Semantisches text-first HTML ohne axe-Verstoß | DocFX 0 errors and web-A11Y PASS |
| QS-10 | Generierte Evidence entsteht lokal/CI | Ausgabe bleibt temporär oder CI-Artefakt und untracked | Final tracked/generated-output scan PASS |

Die Szenarien werden bei neuen Trust Boundaries, Incident, Paket-/Toolwechsel,
Cloud/Auth/Runtime-AI-Scope oder geänderter Release-Pipeline fortgeschrieben.

*Update these scenarios when trust boundaries, incidents, packages/tools,
cloud/auth/runtime-AI scope, or the release pipeline change.*
