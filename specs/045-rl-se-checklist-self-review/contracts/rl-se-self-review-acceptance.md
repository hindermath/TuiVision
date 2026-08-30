# Abnahmevertrag: RL-SE-Selbstprüfung / Acceptance Contract: RL-SE Self-Review

**Feature**: `045-rl-se-checklist-self-review`
**Datum / Date**: 2026-08-30

## 1. Scope-Vertrag / Scope Contract

Feature 045 darf ausschließlich Audit-, Security-, Feature-, Statistik- und
test-only Validator-Evidence ändern. Es darf einen C#-Validator und
kontrollierte Fixtures im vorhandenen Testprojekt ergänzen, ohne Projektdatei,
Paket oder Produktreferenz zu ändern.

*Feature 045 may change audit, security, feature, statistics, and test-only
validator evidence only.*

Feature 045 darf nicht ändern:

```text
Produkt-Runtime oder Produktverhalten
Public API oder XML-Dokumentation
Dependencies, Packages, Tools, Projekt- oder Solution-Dateien
Beispiele oder historische Quellen
Constitutions, Presets, Baseline, Richtlinie, Sammelband oder Checklisten
Workflows, Provider-Einstellungen, Secrets oder Organisationsregeln
automatisch erzeugte Folge-Intakes, Issues, Branches oder Features
```

Ein solcher Diff ist `BlockedScopeViolation` und wird nicht als
Auditbeobachtung nachträglich akzeptiert.

## 2. Herkunfts- und Snapshot-Vertrag / Provenance and Snapshot Contract

- Intake, Ready-Review, `spec.md`, `clarification-report.md`, Requirements-
  und Audit-Readiness-Checkliste müssen vor Implementierung hashgleich zu den
  akzeptierten Inputs sein.
- Der Audit-Snapshot führt vollständigen Git-HEAD, UTC-Zeitpunkt und
  normalisierte SHA-256-Werte aller bindenden Quellen.
- Baseline, Richtlinie, Sammelband, zwölf Einzelchecklisten, Mapping, beide
  Constitutions, Registry, zwölf Preset-Artefakte, Feature-016-Evidence und
  die kanonische Feature-044-Sandbox-Bewertung samt Feature-Evidence werden
  getrennt gehasht.
- Hashdrift öffnet die betroffene Bewertung erneut; kein Hash wird still
  ersetzt.

## 3. Kontrollinventar-Vertrag / Control Inventory Contract

Der neue Datensatz enthält genau eine Zeile für jede kanonische Überschrift:

| Kapitel / Chapter | Controls |
|---|---:|
| CL-01 | 12 |
| CL-02 | 13 |
| CL-03 | 15 |
| CL-04 | 10 |
| CL-05 | 13 |
| CL-06 | 11 |
| CL-07 | 12 |
| CL-08 | 13 |
| CL-09 | 17 |
| CL-10 | 17 |
| CL-11 | 12 |
| CL-12 | 12 |
| **Gesamt / Total** | **157** |

Mechanische Abnahme verlangt:

- 157 Quell-IDs und 157 Ergebniszeilen;
- null fehlende, doppelte oder unbekannte IDs;
- exakte Kapitelzahlen;
- ordinal stabile Reihenfolge;
- Quellpfad und Quellüberschrift passen zur ID.

## 4. Statusvertrag / Status Contract

Jede Kontrollzeile verwendet genau einen Wert:

```text
Applicable
AlreadySatisfied
N/A
Open
FollowUp
```

Aliases, kombinierte Werte und die generischen Baseline-Implementierungswerte
sind als Ergebnisstatus verboten.

Jede Zeile enthält mindestens:

```text
ControlId
SourcePath
SourceHeading
Title
Status
Rationale
EvidenceIds oder EvidenceGap
Owner
Reviewer
ReviewDate
FollowUp
Priority
ResidualRisk
ReevaluationTrigger
HumanOnly
ExternalOnly
Feature016Status
Feature016EvidenceId
ChangeExplanation
```

Kein Feld ist leer. `None` ist nur mit statusbezogener Begründung erlaubt.

## 5. Statusbezogener Evidence-Vertrag / Status-Specific Evidence Contract

- `AlreadySatisfied` braucht direkte, aktuelle, existierende und die Aussage
  tatsächlich stützende Repository-Evidence.
- `Applicable` braucht konkrete Evidence oder eine sichtbare Lücke sowie eine
  sichere Folgeaktion.
- `N/A` braucht faktische Nichtanwendbarkeit, Owner, Restrisiko und Trigger.
  Zeitmangel, fehlende Autorität oder fehlende Evidence sind keine alleinige
  Begründung.
- `Open` braucht Owner, konkrete Aktion, Priorität, Restrisiko und Trigger.
- `FollowUp` braucht eine benannte spätere Arbeitsgrenze. Es erzeugt keine
  neue Intake-, Issue-, Branch- oder Feature-Datei.
- Human-/External-only-Evidence ohne befugten Nachweis darf nie als erfüllt
  oder freigegeben gezählt werden.

## 6. Freshness-Vertrag / Freshness Contract

Jede Evidence-Referenz enthält repository-relativen Pfad, SHA-256,
Beobachtungszeit, Typ, Direktheit, Freshness, Resultat, Proof-Grenze und
Re-Evaluation-Trigger.

Ein Pfad allein ist kein positiver Nachweis. Eine Vorlage, ein früherer
Feature-Claim, eine leere Datei, eine veraltete Hashbindung oder ein lokaler
nicht reproduzierbarer Zustand darf `AlreadySatisfied` nicht tragen.

Absolute private Pfade, Home-Verzeichnisse, Tokens, Session-/Logdaten,
Credentials, produktive Daten und nicht veröffentlichbare Plattformdetails
sind verboten. Die entstehende Beweislücke bleibt sichtbar.

## 7. Feature-016-Vergleichsvertrag / Feature 016 Comparison Contract

- `docs/security/control-assessment.md` und
  `specs/016-secure-development-hardening/pr-evidence.md` bleiben unverändert.
- Jede neue Kontrollzeile nennt den historischen 016-Status und erklärt
  nachvollziehbar Beibehaltung oder Änderung.
- Die historische Verteilung 65/13/38/36/5 wird nur als Vergleich gezeigt.
- Neue Statussummen werden ausschließlich aus dem validierten Feature-045-
  Datensatz berechnet.

## 8. Preset-Vertrag / Preset Contract

Der Datensatz enthält exakt zwölf Preset-Zeilen:

| Preset | Version |
|---|---:|
| `security-governance` | 0.6.2 |
| `architecture-governance` | 0.5.2 |
| `isaqb-architecture-governance` | 0.2.2 |
| `a11y-governance` | 0.4.3 |
| `cross-platform-governance` | 0.2.2 |
| `agent-parity-governance` | 0.4.2 |
| `model-routing-governance` | 0.1.4 |
| `intake-authoring-governance` | 0.3.1 |
| `intake-review-governance` | 0.2.1 |
| `intake-sequencing-governance` | 0.2.3 |
| `autonomous-run-governance` | 0.4.1 |
| `parallel-autonomous-run-governance` | 0.2.6 |

Jede Zeile führt Manifest- und Artefakthash, Prüfpunkte, Status, Begründung,
Evidence, Owner, Reviewer, Reviewdatum, Follow-up, Priorität, Restrisiko,
Trigger und Human-only-Wert. Nicht ausgelöste Script-, Parallel- oder
Remote-Aspekte erhalten begründetes `N/A`, keine Auslassung.

Die Feature-044-Empfehlung `ConditionallyUsable` darf nur mit ihren offenen
Freigabe-, Provider-, Netzwerk-, Lifecycle- und Plattformgrenzen als
Eingangsevidence verwendet werden. Sie ist keine formale aktuelle Freigabe.

## 9. Governance-Beobachtungsvertrag / Governance Observation Contract

Mindestens diese Kandidaten werden geprüft:

- Manifest 3.1.0 gegen Richtlinie/Sammelband 3.2.0 und abweichende
  Einzelchecklisten;
- `constitution.md` 1.17.0 gegen
  `.specify/memory/constitution.md` 1.18.1;
- sechs/sieben/historisch acht Presets gegen zwölf aktivierte Registry-
  Presets;
- Feature-016-Status und Evidence gegen den aktuellen Snapshot.

Jede bestätigte Beobachtung nennt mindestens zwei Quellen, Unterschied,
Auswirkung, Owner, Reviewer, Priorität, Restrisiko, Aktion und Trigger.
`repairPerformed` ist exakt `false`. Keine Governance-Quelle wird durch dieses
Feature korrigiert.

## 10. Human-only-Vertrag / Human-Only Contract

Rechts-, Organisations-, Provider-, Secret-, reale Plattform- und formale
Freigabefragen erhalten eine verantwortliche menschliche Rolle und eine
getrennte technische Proof-Grenze. `agentMayClose` ist ohne befugte aktuelle
Evidence `false`.

Der Audit darf keine Zertifizierung, QISMS-, Rechtskonformitäts-, Provider-
Assurance- oder pauschale Sicherheitsfreigabe behaupten.

## 11. Standards- und Architekturvertrag / Standards and Architecture Contract

- NIST SSDF und CWE Top 25 sind für den Level-2-Audit immer `Applicable`.
- MSL, C#-Secure-Coding, ASVS, SBOM, VEX, SLSA, CAPEC, AI-SBOM, Zero Trust,
  SAMM, OpenSSF Scorecard, CRA, NIS2, DORA, EU AI Act, BSI C3A und C5 werden
  ohne stille Auslassung bewertet.
- MSL reduziert keinen I/O-, Fehler-, API-, Dependency-, Supply-Chain- oder
  Agenten-Prüfumfang.
- Produktarchitektur und Trust Boundaries ändern sich nicht. Neue ADRs,
  Architekturansichten, Threat-Model- oder arc42-Reparaturen sind für die
  Feature-Ausführung `N/A` mit Trigger.
- Source-reference disposition ist exakt `N/A`; Lizenzgrenze
  `MultipartNotRepositoryWideMIT` bleibt dokumentiert.

## 12. A11Y- und Sprachvertrag / Accessibility and Language Contract

- Leserflächen sind Deutsch zuerst, Englisch danach und ungefähr CEFR B2.
- Semantische Überschriften, Tabellenheader, Listen und beschreibende Links
  sind Pflicht.
- Status, Priorität, Risiko, Abhängigkeit und nächste Aktion sind vollständig
  als Text vorhanden.
- Farbe, Layout, Bild, Symbol oder Pointer-Interaktion darf keine alleinige
  Bedeutung tragen.
- Fach- und Spec-Kit-Begriffe werden beim ersten Auftreten erklärt oder mit
  beschreibendem Lernlink verbunden.
- Zusätzliche Diagramme oder Bilder besitzen eine gleichwertige bilinguale
  Textbeschreibung.
- DocFX, Playwright/Axe und text-first Spot-Check bestehen, weil neue
  Security-Markdown-Evidence ausgeliefert wird.

## 13. Validatorvertrag / Validator Contract

Der test-only MSTest-Validator:

- nutzt nur vorhandenes MSTest und `System.Text.Json`;
- ändert keine `.csproj`, `.sln` oder Package-Datei;
- arbeitet offline und erhält den Repository-Root explizit;
- lehnt unbekannte Properties, Werte, Relationen und Pfade fail-closed ab;
- validiert JSON und Markdown-Parität;
- schreibt bei Erfolg oder Fehler keine Auditdatei;
- liefert stabile `RLSE001` bis `RLSE012`-Fehlercodes;
- führt einen vollständigen positiven Datensatz und isolierte Negativ-Fixtures
  aus.

Ein neues Script ist nicht erforderlich. Entsteht dennoch ein script-shaped
Diff, werden Bash und PowerShell mit Help, Manpage, Cmdlet, sicherer
Implementierungsdisziplin und OS-Parität gemeinsam Pflicht.

## 14. Vertikalschnitt-Vertrag / Vertical Slice Contract

`CL-01-01` ist vor jeder breiten Wiederholung vollständig zu liefern.

Der Slice umfasst:

- kanonische Quelle und Hash;
- alle Kontrollpflichtfelder;
- Evidence-Freshness und Proof-Grenze;
- Feature-016-Vergleich;
- verknüpfte `security-governance`-Evidence;
- JSON-/Markdown-Parität;
- erwartetes semantisches Red, grünen Positivlauf und isolierte Negativfälle.

Andere Kontrollen dürfen erst danach kapitelweise ergänzt werden.

## 15. Validierungs- und Build-Counter-Vertrag / Validation and Build Counter Contract

- Jeder einzelne `dotnet build`- oder `dotnet test`-Aufruf erhält genau eine
  vorherige Build-Counter-Erhöhung.
- Alle drei Versionsfelder bleiben identisch und verwenden
  `1.45.<FeatureCommitCount>.<Build>`.
- Fokussierte Positiv-/Negativtests sind Red/Green-Entwicklungsnachweis. Am
  finalen Kandidaten ersetzt genau ein vollständiger Release-Coverage-Lauf
  getrennte Volltest-, Positiv-, Negativ- und Coverage-Wiederholungen; sein Log
  muss beide benannten Auditvalidatormethoden enthalten.
- Read-only Package-Freshness prüft verwundbare und veraltete Komponenten in
  getrennten Befehlen. Workflow-Referenzen werden vollständig inventarisiert
  und als Auditfakten klassifiziert, ohne Paket-, Tool- oder Workflow-Edit.
- Jede Pflichtassembly erreicht mindestens 70 % Line Coverage; 80 % bleibt
  Ziel.
- Nicht ausgeführte Gates sind `N/A` oder `Not Run`, nie `Pass`.
- Public-API-, Package-, Projekt-, Runtime- oder geschützter Governance-Diff
  blockiert statt zusätzliche Tests zu rechtfertigen.

## 16. Shared-Write- und Retention-Vertrag / Shared-Write and Retention Contract

JSON, Markdown-Projektionen, `pr-evidence.md`, Gate-Dateien, Version,
Security-Index, Statistik, Intake-Archivierung und Delivery-Closeout sind
serialisierte Single-writer-Flächen.

Generierte `_site`-, API-YAML-, TestResults-, Coverage-, Log-, Cache-,
temporäre JSON-, Scan- und Gate-Ausgaben bleiben untracked und werden nach
Auswertung entfernt.

## 17. Exact-Head- und Delivery-Vertrag / Exact-Head and Delivery Contract

- `autonomous-gate-requirements.json` ist vor Implementierung reviewed.
- Staged-Integrität wird vor Commit gegen eine geschlossene Positivliste
  geprüft; das maschinenlesbare Gate prüft danach den committed Candidate.
- Der im Run-State gespeicherte Modus `MergeAndSync` ist historische Evidence,
  keine fortdauernde Berechtigung. Nach grünen lokalen Delivery-Eintrittsgates
  dürfen Commit, Push und PR nur bei aktueller ausdrücklicher `PublishPR`- oder
  `MergeAndSync`-Autorisierung erfolgen. Remote-Checks und Reviews müssen
  demselben Head zugeordnet sein, bevor PreMerge-Evidence akzeptiert wird.
- PreMerge-Evidence wird temporär am exakten finalen Feature-HEAD erzeugt und
  außerhalb ihrer eigenen Gate-Liste mit dem vorhandenen Validator geprüft.
- Weder der Evidence-Validator selbst noch PostMerge-Fakten sind als
  `Applicable` in der PreMerge-Anforderung enthalten.
- PreMerge-Evidence behauptet keine Merge- oder PostMerge-Fakten.
- PostMerge-Evidence wird nur erzeugt, wenn der Merge tatsächlich erfolgt ist.
- Merge, Branchbereinigung und Synchronisierung setzen nach vollständiger
  PreMerge-Konvergenz eine erneut aktuelle ausdrückliche `MergeAndSync`-
  Autorisierung voraus; der gespeicherte Modus erweitert den Audit-Scope nicht.
- Das Lastenheft wird constitution-konform erst nach dem tatsächlichen
  Feature-Merge gepaart archiviert. Danach werden Serienübergang,
  Retrospektive, runner-owned Terminalprojektion und dauerhafte kausale Fakten
  über genau einen proportional validierten Evidence-only-Closeout-PR
  geliefert. Dieser Closeout behauptet keine eigene PR-, Head- oder
  Merge-Identität in sich.
- Kein technischer Gate-Bypass, keine Provider-Konfiguration und keine
  formale Human-/Compliance-Freigabe ist autorisiert.

## 18. Abschlussvertrag / Completion Contract

Fachliche Audit-Evidence ist vollständig, wenn:

- 157/157 Kontrollzeilen und exakte Kapitelzahlen validiert sind;
- alle Pflichtfelder und statusbezogenen Regeln erfüllt sind;
- jeder positive Claim direkte aktuelle Evidence besitzt;
- exakt zwölf Presets vollständig bewertet sind;
- Governance-Drift sichtbar und unrepariert dokumentiert ist;
- alle Human-/External-only-Grenzen ehrlich bleiben;
- Positiv- und Negativvalidatoren bestehen;
- alle ausgelösten lokalen Qualitätsgates bestehen;
- null Scope-Verletzungen, private Pfade, Secrets, unbefugte Claims oder
  automatische Folgeartefakte vorhanden sind;
- Exact-Head-Evidence für den tatsächlichen Delivery-Kandidaten validiert ist.

Delivery ist erst nach aktuellem Review, autorisiertem Merge und sauberem
`main == origin/main` abgeschlossen. Weder Audit- noch Delivery-Abschluss ist
eine Zertifizierung oder Governance-Reparatur.
