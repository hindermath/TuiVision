# Secure Development Assurance – TuiVision

## Auftrag und Quelle / Authority and Source

Thorsten hat die Integration als fünftes und letztes Ziel des seriellen
Fünf-Repository-Rollouts freigegeben: Assurance **v0.1.3**, aktiviert mit Priorität
15. Die zwölf bisherigen Pakete, Registermetadaten und Profile 8–12 bleiben
unverändert; der globale Default wird nicht umgestellt. Neu ist ausschließlich
das benannte Profil `secure-development-assurance-thirteen-governance-presets`.

*Thorsten authorized this fifth and final target of the serial rollout:
Assurance v0.1.3, enabled at priority 15. Preserve all twelve existing packages,
registry metadata and profiles 8–12; keep the global default. Add only the named
thirteen-preset profile.*

- [Produktquelle / product source](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance).
- [Unveränderliches Tag-ZIP / immutable tag ZIP](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/archive/refs/tags/v0.1.3.zip).
- Tag-Commit: `0d03aa9ebe8f74a26e331815bca5609fb48d7a14`.
- ZIP SHA-256: `9023b442b4d82e25bee5a7fe9b73efb7f591a4f265f54061ae6e4a56b9b5c75f`.
- Voraussetzung / prerequisite: `security-governance >=0.6.1`; vorhanden / installed: v0.6.2.
- [Upstream-Einreichung / upstream submission #4455](https://github.com/github/spec-kit/issues/4455): kein Beleg einer automatischen Katalogaufnahme / not proof of catalog acceptance.

Nur das veröffentlichte GitHub-Preset ist Produktquelle. Die installierte Kopie
wird nicht lokal gepatcht. Das explizite Prerelease-Pin ist unabhängig von
GitHubs „Latest“. Die fünf Agenten-Anleitungen sind gemeinsam aktualisiert;
ihre alte Scaffold-first-Anweisung wurde durch diese Quellenregel ersetzt.

*The published standalone preset is the sole product source; no local vendor
patches. The explicit prerelease pin is independent of GitHub Latest. All five
agent guides replace the obsolete scaffold-first instruction together.*

## Bedienung / Usage

Sicherer Einstieg ab Repository-Wurzel / safe entry from repository root:

```bash
bash .specify/presets/secure-development-assurance-governance/scripts/validate-secure-development-assurance.sh status
```

```powershell
pwsh -NoProfile -File .specify/presets/secure-development-assurance-governance/scripts/validate-secure-development-assurance.ps1 -Action Status
```

Agenten-Befehle / agent commands:

- `$speckit-secure-development-status [<evidence-dir>]`: ausschließlich lesend;
  ohne Argument der lexikografisch neueste Kontext. / Read-only; defaults to the
  lexicographically latest context.
- `$speckit-secure-development-review <baseline|delta|closure|image-impact> <context-id> <training|mixed|development>`:
  nur nach ausdrücklichem Review-Auftrag für den Kontext. / Requires explicit
  review authority for the named context.

[Vollständige Paketdokumentation](../../.specify/presets/secure-development-assurance-governance/README.md)
und [Vertrag](../../.specify/presets/secure-development-assurance-governance/templates/secure-development-evidence-contract.md)
beschreiben alle Statuswerte, Voraussetzungen, Wiederholungen und Grenzen.
Andere Agentenflächen verwenden die Punktnotation `speckit.secure-development-status`.

*See the full package README and contract linked above for all states,
prerequisites, recovery steps and limitations. Other agents use dot notation.*

## Evidence und Grenzen / Evidence and Boundaries

Der ausgewählte Kontext ist `2026-08-30-rl-se-checklist-self-review`.
Die neue [Evidence-Matrix](../security/secure-development/2026-08-30-rl-se-checklist-self-review/evidence-matrix.md)
erschließt seine vorhandenen 157 Kontrollbewertungen. Einzige ausdrücklich
genehmigte Ausnahme sind [drei technische Hashkorrekturen](assurance-technical-rebinding.md)
im Bewertungs-JSON; alle übrigen Bytes und Bewertungen bleiben unverändert.
Die Matrix erteilt keine neue Bewertung.
Die früher dokumentierte Prüfung der zwölf Presets bleibt historische Evidence.

`baseline.json`, `deltas/*.json`, `closure.json` und `image-impact.json` fehlen
weiterhin im vollständigen Assurance-Vertrag. Deshalb bleibt Status `Blocked`,
Exitcode 2, nicht „fehlgeschlagene Installation“. Für die vier Gates sowie
`technicalValidation`, `pilotAuthorization`, `projectAcceptance` und
`generalRelease` werden keine fehlenden Ergebnisse oder Freigaben erfunden.
Eine spätere fachliche Migration erfordert einen separaten Auftrag.

*The selected context is the RL-SE self-review dated 2026-08-30. Its new matrix
indexes 157 existing controls. Only three explicitly authorized technical hash
fields change, with old/new provenance; every other byte and all assessments
remain unchanged. The old twelve-preset assessment remains historical
evidence. Missing gate JSONs keep status Blocked (exit 2), not installation
failure. No gate result or human decision is fabricated. A later substantive
migration requires separate authority.*

### C5-Abgrenzung / C5 Boundary

`CL-02-13 Cloud-Compliance-Assurance` bleibt eine projektbezogene Bewertung;
sein historisches `N/A` wird nur wiedergegeben, nicht neu bestätigt. Das Preset
prüft Evidence-Bindung und Konsistenz, keinen vollständigen C5-Katalog.
Weder Installation noch `Ready` bedeuten C5-Konformität, Testatreife oder
Zertifizierung. Keine Richtlinien-, Checklist-, Baseline- oder Freigabeänderung.

*CL-02-13 remains a project-specific assessment. Its historical N/A is copied,
not newly confirmed. The preset validates evidence bindings and consistency,
not all C5 criteria; neither installation nor Ready means C5 conformity,
attestation readiness or certification. No policy/checklist/baseline/approval
changes.*

## Prüfung und Lieferung / Verification and Delivery

Paketdateien werden vollständig gegen das verifizierte ZIP verglichen;
Vorher-Snapshots schützen bestehende Dateien, zwölf Registereinträge und ältere
Profile. Pakettests laufen isoliert, Projektstatus strikt lesend unter Bash
und PowerShell. Bestehende Produkt-CI und die Abdeckungsschwelle von mindestens
70 % je Framework-Modul bleiben technische Liefergates. Der manuelle
Buildzähler steigt nur für den tatsächlich ausgeführten .NET-Testlauf.
Keine Produktlogik, Runtime, Abhängigkeit oder öffentliche API wird verändert.
Diese Governance-Integration portiert kein Verhalten aus `tv203s`.

Delivery Mode: `MergeAndSync`; ausdrücklich genehmigter Admin-Bypass nur für
die formale Reviewer-Hürde nach grüner Technik und bearbeiteten Befunden.
Kein Home-Sync, kein neuer Release, kein Community-Issue und kein weiterer
Flotten-Rollout. Die Ergebniszahlen stehen nachfolgend.

*Verify the complete package against the ZIP and preserve existing files,
twelve registry entries and older profiles. Isolated package tests, read-only
status in both shells, existing CI and >=70% coverage per framework module
remain gates. Increment the manual build counter for the actual .NET test.
No product logic, runtime, dependency, API or tv203s behavior port is changed.
MergeAndSync/admin authority covers only the formal reviewer barrier after
technical success and addressed findings. No Home sync, release, community
submission or additional rollout; results follow below.*

### Historischer Zwischenstand vor Zusatzfreigabe / Historical Blocker

Dieser Abschnitt hält den Zustand vor Thorstens zusätzlicher Freigabe fest;
der aktuelle Prüfnachweis folgt danach. / This section retains the pre-approval
state; current verification follows it.

Installation und Matrixprüfung bestanden: 13 Presets entsprechen exakt dem
Profil; 3374 vorher vorhandene Dateien außerhalb der genehmigten Pfade sind
bytegleich. Alle zwölf bisherigen Registry-Einträge und Profile sind erhalten.
Beide Statusläufe melden die fehlende `baseline.json`, Exitcode 2.
Die isolierten Paket-Vertrags-/Negativtests einschließlich Bash-/PowerShell-
Parität sind bestanden. Die beiden neuen Dokumente bestehen die lokale
Link-/Fragmentprüfung ohne Fehler. / Isolated package contract/negative tests
and Bash/PowerShell parity passed; both new documents pass offline link checks.

Der lokale Release-Testlauf mit Coverage (`Directory.Build.props` Build 519)
ist **nicht bestanden**: `Test_VerticalSliceIsValid`, `Test_ChapterDraftIsValid`
und `Test_CompleteAuditIsValid` melden `RLSE007` wegen alter Hashbindungen.
Betroffen sind ausschließlich Evidence `EVD-002` (`.specify/presets/.registry`)
und `EVD-015` (`AGENTS.md`) im historischen `rl-se-self-review.json`.
Beide Originalhashes stimmen mit dem Vorher-Snapshot überein; die Drift stammt
aus der genehmigten Integration, nicht aus der neuen Matrix. Die historische
Registry-Bindung steht außerdem in `reviewSnapshot.baselineSourceHashes`.

Coverage-Berichte liegen für alle fünf Framework-Module vor. Die jeweils
höchste gemessene modulbezogene Line-Coverage in diesen Berichten ist:
Core 92,96 %, Controls 86,95 %, Serialization 90,47 %, Compatibility 80,55 %,
Drivers.Console 89,18 %. Diese Zahlen machen den fehlgeschlagenen Testlauf
nicht zu einem bestandenen Gate. Beim Smoke-Testprojekt ist kein eigener
XPlat-Collector vorhanden; es gehört nicht zu den fünf Coverage-Gatemodulen.

Die bestehenden Evidence-Dateien und Tests wurden **nicht** verändert.
Kein PR, Push oder Merge für TuiVision; der zentrale Registry-Eintrag bleibt
beim Zwölferprofil. Nötig ist Thorstens Freigabe für die eng begrenzte technische
Neubindung der zwei Quellen einschließlich konsistenter Bindungsfelder und
nachvollziehbarer Alt-/Neu-Provenienz, ohne Änderung der 157 Bewertungen,
Reviewentscheidungen oder Human-only-Grenzen. Anschließend erneut testen;
kein technischer Admin-Bypass.

*Installation, exact thirteen-preset composition and preservation of 3374
pre-existing files passed. Both status commands block on missing baseline.json.
The Release test at manual Build 519 failed three RL-SE evidence tests (RLSE007):
EVD-002 binds the previous registry, EVD-015 the previous AGENTS.md. The former
hashes match the before-snapshot; the new matrix did not cause the drift. The
registry hash also appears in the historical review snapshot. Per-module
coverage is reported above but does not turn the failed suite into a pass.
Historical evidence and tests remain unchanged. No TuiVision PR/push/merge;
central registration remains twelve. Obtain explicit authority for technical
rebinding with old/new provenance, preserving all 157 assessments and human
decisions, then rerun tests. Never bypass this technical gate.*

### Aktueller Prüfnachweis nach Freigabe / Current Verification After Approval

Thorsten hat die technische Neubindung am 2026-09-07 ausdrücklich genehmigt.
Genau drei Hashfelder wurden gemäß [Alt-/Neu-Provenienz](assurance-technical-rebinding.md)
korrigiert; alle übrigen Evidence-Bytes sind identisch. 3373 andere geschützte
Bestandsdateien sowie die zwölf bisherigen Presets/Registry-Einträge und
älteren Profile bleiben unverändert. Das öffentliche v0.1.3-Paket ist bytegleich.

Der vollständige Release-Testlauf mit Coverage bei Build **520** ist bestanden
(Exitcode 0), einschließlich aller drei zuvor blockierten RL-SE-Tests. Keine
Teständerung, keine Abschwächung. Die fünf Framework-Coverage-Module liegen
oberhalb 70 %. Bash-/PowerShell-Vertrags- und Negativtests sind ebenfalls
bestanden. Die Dokumentationslinks und Text-first-Darstellung wurden geprüft.
Der separate Assurance-Status bleibt in beiden Shells `Blocked`, da echte
Gate-JSONs fehlen; erfolgreiche Produkttests erteilen keine fachliche Freigabe.

*Thorsten explicitly authorized rebinding on 2026-09-07. Exactly three hashes
changed with old/new provenance; all other evidence bytes, 3373 protected
files, twelve presets/registry entries and older profiles are unchanged.
The public v0.1.3 package matches byte-for-byte. The full Release coverage run
at Build 520 passed (exit 0), including all three previously failing RL-SE
tests, with no test modifications. All five coverage modules exceed 70%.
Package parity/negative tests passed; documentation is text-first and links
were checked. Separate Assurance status remains Blocked on missing genuine
gate JSONs; product test success grants no domain approval.*

## Dokumentationsauswirkung / Documentation Impact

`UpdateRequired`. Owner: Thorsten Hindermann / Repository-Maintainer.
Zielgruppen: Maintainer, KI-Agenten und Security-Review; Leserpfad: README und
Agenten-Anleitung → dieser Integrationsnachweis → Paket-README oder Evidence-
Matrix → ausdrücklich beauftragte nächste Aktion. Quellen: öffentliches Paket,
lokale Profilmatrix und historisches Bewertungs-JSON mit dokumentierter
technischer Hashkorrektur. Dokumentklasse:
Bedienung und Integrations-Evidence. DE/EN im selben Dokument, textorientierte
Beispiele und Tabellen ohne farbabhängige Aussage. Repository-lokal, kein
Home-Sync. Re-Evaluation bei Paket-, Profil-, Evidence-, Baseline- oder
Runtime-Änderung. Constitution und frühere Anforderungen bleiben unverändert.

*UpdateRequired; owner Thorsten Hindermann/repository maintainer. Readers are
maintainers, agents and security reviewers. Follow README/agent guide to this
record, then the package README or evidence matrix and explicitly authorized
next action. Sources are the public package, local profile matrix and historical
JSON with documented technical hash corrections. Bilingual, text-first, repository-local usage and integration
evidence; no Home sync. Reevaluate upon package, profile, evidence, baseline or
runtime change. Preserve constitution and historical requirements.*
