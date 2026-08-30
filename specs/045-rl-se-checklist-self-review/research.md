# Forschung und Entscheidungen: RL-SE-Selbstprüfung / Research and Decisions: RL-SE Self-Review

**Feature**: `045-rl-se-checklist-self-review`
**Datum / Date**: 2026-08-30
**Planungs-HEAD / Planning HEAD**: `6bf24ca6d18f83e0c54e9e00f50aba36fff2739c`

## R01 – Kanonische Kontrollmenge / Canonical Control Set

**Entscheidung / Decision**: Jede Überschrift `#### CL-XX-NN` in den zwölf
Einzelchecklisten ist genau eine Kontrollidentität. Die planungsgeprüfte Menge
umfasst 157 eindeutige IDs mit den Kapitelzahlen
`12/13/15/10/13/11/12/13/17/17/12/12`.

**Begründung / Rationale**: Die Einzelchecklisten sind laut Spezifikation die
kanonische ID-Quelle. Manifest, Richtlinie und Sammelband sind wichtige
Vergleichsquellen, besitzen aber aktuell Versionsdrift.

**Verworfene Alternative / Rejected alternative**: Eine Zeile je Kapitel ist
zu grob; eine Zeile je Markdown-Bullet würde erklärenden Text fälschlich als
Kontrolle behandeln.

## R02 – Einachsiges Feature-Statusmodell / Single Feature Status Model

**Entscheidung / Decision**: Ergebniszeilen erlauben ausschließlich
`Applicable`, `AlreadySatisfied`, `N/A`, `Open` und `FollowUp`. Die zweiachsige
generische Baseline bleibt als Quellkontext dokumentiert, wird aber nicht in
die Ergebniswerte gemischt.

**Begründung / Rationale**: Das akzeptierte Feature-Modell kann aktuelle
Evidence, Nichtanwendbarkeit, offene Autorität und bewusste spätere Arbeit
unterscheiden. Gemischte Werte würden Statussummen und Abnahme mehrdeutig
machen.

**Verworfene Alternative / Rejected alternative**: `Fulfilled` oder
`Not Assessed` als zusätzlicher Ergebnisstatus würde FR-004 verletzen.

## R03 – Kanonisches JSON und Markdown-Projektionen / Canonical JSON and Markdown Projections

**Entscheidung / Decision**: `rl-se-self-review.json` ist die
maschinenlesbare Quelle des neuen Auditprodukts. Semantische Markdown-Dateien
projizieren dieselben Entscheidungen für Lernende, Maintainer und Auditoren.

**Begründung / Rationale**: JSON erlaubt geschlossene Werte, exakte
Kardinalitäten, Hashes und Relationen. Markdown liefert den erforderlichen
text-first Leserpfad. Der Validator prüft beide gegeneinander, sodass keine
zweite Wahrheit entsteht.

**Verworfene Alternative / Rejected alternative**: Nur Markdown wäre für
positive und negative Schema-, Relations- und Freshness-Prüfung zu fragil.
Nur JSON wäre für CEFR-B2- und assistive Leser ungeeignet.

## R04 – Datierte Evidence statt Überschreiben von Feature 016 / Dated Evidence Instead of Overwriting Feature 016

**Entscheidung / Decision**: Die neue Evidence liegt unter
`docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/`.
`docs/security/control-assessment.md` bleibt als Feature-016-Ausgangsnachweis
unverändert; `docs/security/README.md` erhält später einen neuen Leserlink.

**Begründung / Rationale**: Die alte Matrix ist ein wichtiger Zeitvergleich.
Ein Überschreiben würde Herkunft und Freshness verwischen.

**Verworfene Alternative / Rejected alternative**: In-place-Aktualisierung der
016-Matrix würde historische Verteilung und damalige Proof-Grenzen verlieren.

## R05 – Evidence-Freshness / Evidence Freshness

**Entscheidung / Decision**: Jede Evidence-Referenz führt Pfad, normalisierten
SHA-256, Beobachtungszeit, Evidenzart, direkte oder begrenzte Beweisrolle,
Ergebnis, Proof-Grenze und Re-Evaluation-Trigger. Bindende Input-Hashes werden
im Audit-Snapshot gespeichert.

**Begründung / Rationale**: Ein existierender Pfad beweist weder Aktualität
noch die konkrete Aussage. Hash und Proof-Grenze machen spätere Drift sichtbar.

**Verworfene Alternative / Rejected alternative**: Ein Reviewdatum allein
erkennt nachträgliche Dateiänderungen nicht.

## R06 – Statusbezogene Evidenzregeln / Status-Specific Evidence Rules

**Entscheidung / Decision**:

- `AlreadySatisfied` braucht mindestens eine aktuelle direkte Evidence-
  Referenz, keine reine Vorlage und keinen alten Claim.
- `Applicable` beschreibt eine geltende Pflicht und nennt konkrete Evidence
  oder eine explizite Lücke sowie die nächste sichere Aktion.
- `N/A` braucht faktische Nichtanwendbarkeit, Restrisiko und Trigger; Zeit,
  Autoritätsmangel oder fehlende Evidence reichen nicht.
- `Open` braucht Owner, Priorität, Risiko, konkrete Folgeaktion und Trigger.
- `FollowUp` braucht eine benannte spätere Arbeitsgrenze, erzeugt aber kein
  Folgeartefakt.

**Begründung / Rationale**: So kann kein positiver oder entfallender Claim aus
einem leeren Feld entstehen.

## R07 – Feature 016 als Eingangsevidenz / Feature 016 as Input Evidence

**Entscheidung / Decision**: Die Feature-016-Matrix mit 157 Zeilen und der
historischen Verteilung 65 `Applicable`, 13 `AlreadySatisfied`, 38 `N/A`, 36
`Open`, 5 `FollowUp` wird zeilenweise verglichen. Der alte Status wird nie
automatisch übernommen.

**Begründung / Rationale**: Seit 016 haben sich Baseline-, Constitution-,
Preset-, Sandbox- und Repository-Evidence geändert. Alte Ergebnisse sind
wertvoll, aber nicht automatisch frisch.

**Verworfene Alternative / Rejected alternative**: Kopieren und nur neue
Dateipfade ergänzen würde SC-004 nicht erfüllen.

## R08 – Governance-Drift bleibt unrepariert / Governance Drift Remains Unrepaired

**Entscheidung / Decision**: Bestätigte Unterschiede werden als eigene
`GovernanceObservation` mit zwei oder mehr Quellen, Beobachtung, Auswirkung,
Owner, Priorität, Restrisiko, Folgeaktion und Trigger dokumentiert. Die
betroffenen Governance-Dateien bleiben read-only.

**Begründung / Rationale**: Die Selbstprüfung besitzt Evidence-Autorität, aber
keine Policy-Reparaturautorität. Eine stille Korrektur würde Audit und
Governance-Änderung vermischen.

**Verworfene Alternative / Rejected alternative**: Constitution, Manifest,
Mapping oder Preset-Registry automatisch zu synchronisieren überschreitet den
Scope.

## R09 – Zwölf Preset-Datensätze / Twelve Preset Records

**Entscheidung / Decision**: Die Registry-Menge von zwölf aktivierten Presets
ist die aktuelle Auditmenge. Jedes Preset erhält genau einen Datensatz mit ID,
Version, Prüfpunkten, Status, Evidenz, Owner, Reviewer, Restrisiko, Follow-up
und Trigger.

**Begründung / Rationale**: Die Mapping-Datei nennt sechs oder sieben Presets,
Constitution-Text historisch acht. Nur die aktuelle Registry liefert die
vollständige installierte Menge; die Abweichung bleibt ein Finding.

**Verworfene Alternative / Rejected alternative**: Nur die Presets mit
`speckit.plan`-Wrapper zu prüfen würde Routing-, Intake- und Parallel-
Governance still auslassen.

## R10 – Human-only- und External-only-Grenzen / Human-Only and External-Only Boundaries

**Entscheidung / Decision**: Recht, Organisation, Provider, Secrets, reale
Plattform, formale Freigabe und nicht reproduzierbare externe Fakten bleiben
eigenständige Grenzen. Ohne befugte Evidence werden sie `Open`, `FollowUp`
oder faktisch begründet `N/A`, nie `AlreadySatisfied`.

**Begründung / Rationale**: Repository-Evidence kann keine menschliche oder
externe Autorität ersetzen.

## R11 – Plattformneutraler Testvalidator / Platform-Neutral Test Validator

**Entscheidung / Decision**: Ein neuer test-only MSTest-Validator im
vorhandenen `TuiVision.Drivers.Tests`-Projekt liest JSON und Markdown mit
`System.Text.Json` und Standardbibliothek. Er prüft den vollständigen Datensatz
und kontrollierte fehlerhafte Fixtures. Projekt- und Paketdateien bleiben
unverändert.

**Begründung / Rationale**: Dieses Testprojekt enthält bereits
Repository-Evidence-Validatoren. Der Ansatz ist plattformneutral, offline,
deterministisch und nutzt vorhandene Abhängigkeiten.

**Verworfene Alternative / Rejected alternative**: Ein neues Projekt oder
Paket wäre unnötig. Reine Einmalbefehle würden stabile Negativfälle und
Fehlercodes nicht ausreichend beweisen.

## R12 – Keine neue Script-Oberfläche / No New Script Surface

**Entscheidung / Decision**: Neue script-shaped Validierung ist `N/A`, weil
der MSTest-Validator alle geforderten Positiv- und Negativfälle portabel
abdeckt. Daher entstehen kein Bash-/PowerShell-Paar, keine Manpage, kein Cmdlet
und kein Script-Paritätsartefakt.

**Re-Evaluation-Trigger / Re-evaluation trigger**: Sobald die Umsetzung ein
neues oder geändertes `.sh`/`.ps1` benötigt, gelten beide Varianten, sichere
Shell-Disziplin, Help, Manpage, Cmdlet und OS-Paritätsnachweis gemeinsam.

## R13 – Repräsentativer Vertikalschnitt / Representative Vertical Slice

**Entscheidung / Decision**: `CL-01-01` ist der erste vollständige Slice. Er
verbindet Quellinventar, Pflichtfelder, Freshness, Feature-016-Vergleich,
`security-governance`-Preset und Markdown-Projektion. Ein erwartetes Red wegen
fehlender/ungültiger Slice-Evidence geht dem grünen Lauf voraus.

**Begründung / Rationale**: Der Control deckt die immer anwendbaren NIST-SSDF-
und CWE-Top-25-Prüffelder ab und berührt damit die fachliche Kernlogik des
Audits.

**Verworfene Alternative / Rejected alternative**: Sofort 157 Zeilen zu
schreiben würde Schema- und Relationsfehler breit vervielfachen.

## R14 – Positive und negative Validation / Positive and Negative Validation

**Entscheidung / Decision**: Der positive Datensatz beweist alle
Kardinalitäten, Relationen und Projektionen. Jede Negativ-Fixture verletzt
genau eine Primärinvariante und erwartet einen stabilen `RLSE###`-Fehlercode.
Abgedeckt werden mindestens falsche Gesamt-/Kapitelzahl, doppelte/unbekannte
ID, ungültiger Status/Priorität, leeres Pflichtfeld, schwache positive
Evidence, fehlender N/A-/Open-/FollowUp-Vertrag, Preset-Unterdeckung,
unbefugter Human-Claim, absoluter/privater Pfad, Projektionsdrift und
unerlaubter Scope-Pfad.

**Begründung / Rationale**: Ein Happy Path allein beweist nicht, dass der
Validator fail-closed reagiert.

## R15 – A11Y- und Sprachstrategie / Accessibility and Language Strategy

**Entscheidung / Decision**: Leserflächen sind Deutsch zuerst und Englisch
danach, ungefähr CEFR B2, semantisch und text-first. Status, Priorität und
Risiko stehen als Wörter im Text. MSL, SSDF, CWE, ASVS, SBOM, VEX, SLSA,
SAMM, CAPEC, Zero Trust, C3A/C5 und Spec-Kit-Begriffe werden beim ersten
Gebrauch erklärt oder mit einem beschreibenden Lernlink verbunden.

**Begründung / Rationale**: Das Ergebnis richtet sich auch an Lernende im
ersten Ausbildungsjahr und muss ohne Farbe, Maus oder visuelles Layout
verständlich bleiben.

## R16 – Architektur- und Quellenpolicy / Architecture and Source Reference Policy

**Entscheidung / Decision**: Neue Architekturansichten, ADRs, Threat-Model-
Änderungen und Source-reference-Arbeit sind für die Feature-Ausführung `N/A`.
Bestehende Evidence wird geprüft, aber keine Produktsemantik, Trust Boundary
oder historische Absicht geändert. Die Quellenentscheidung ist genau `N/A`;
die Lizenzgrenze bleibt `MultipartNotRepositoryWideMIT`.

**Re-Evaluation-Trigger / Re-evaluation trigger**: Produktvertrag,
historisch abgeleitete Semantik, genehmigter Magiblot-Pin oder materiell neue
Consumer-Evidence tritt in den Änderungsumfang.

## R17 – Triggerbasierte Validierung und Build Counter / Trigger-Based Validation and Build Counter

**Entscheidung / Decision**: Test-only C# löst fokussierte Red/Green-Läufe,
Format und am finalen Kandidaten genau einen vollständigen Release-Lauf mit
kanonischem Coverage-Collector aus. Dieser eine Lauf beweist Vollsuite,
positiven Komplettaudit, negative Fixtures und die fünf Assembly-Grenzen;
separate finale Wiederholungen sind nicht erforderlich. Security-Markdown
löst DocFX, Playwright/Axe und text-first Review aus. Vulnerable- und
Deprecated-Package-Scans bleiben getrennte Befehle; Workflow-Referenzen werden
read-only inventarisiert und als Auditfinding bewertet. Public API, Paket,
Projektdatei oder Runtime-Diff ist ein Hard Stop. Vor jedem einzelnen
`dotnet build`/`dotnet test` werden alle drei Versionsfelder auf
`1.45.<FeatureCommitCount>.<incrementedBuild>` ausgerichtet.

**Begründung / Rationale**: Gate-Aufwand folgt dem tatsächlichen Diff. Kein
nicht ausgeführtes Gate wird als bestanden dargestellt.

## R18 – Exact-Head Delivery Evidence / Exact-Head Delivery Evidence

**Entscheidung / Decision**: `autonomous-gate-requirements.json` wird in der
Planphase erstellt. Staged-Integrität ist ein Vor-Commit-Prozessgate; die
maschinenlesbare PreMerge-Anforderung prüft den committed Candidate. Sie darf
weder ihren eigenen Evidence-Validator noch PostMerge-Fakten als enthaltenes
Gate verlangen. Spätere PreMerge-/PostMerge-Evidence bleibt temporär unter
`/private/tmp/`; der vorhandene autonome Gate-Validator prüft die fertige
PreMerge-Datei als äußeren Abschluss gegen den exakten HEAD. Dauerhafte
Delivery-Fakten werden erst nach ihrer Entstehung in `delivery-closeout.md`
geschrieben. Weil Lastenheft-Archivierung, Serienübergang, Retrospektive und
runner-owned Terminalprojektion erst nach dem Feature-Merge wahr sein können,
werden sie in genau einem proportional validierten Evidence-only-Closeout-PR
geliefert. Der Closeout schreibt seine eigene PR-, Head- oder Merge-Identität
nicht rekursiv in sich.

**Begründung / Rationale**: Eine getrackte PreMerge-Evidence würde durch ihren
eigenen Commit den geprüften HEAD entwerten. Diese Planung behauptet keinen
bereits bestandenen Exact-Head-Nachweis.

## R19 – MergeAndSync ist kein Audit-Scope / MergeAndSync Is Not Audit Scope

**Entscheidung / Decision**: `MergeAndSync` ist der im Run-State gespeicherte
Delivery-Modus, aber keine fortdauernde Berechtigung. Nach allen lokalen
Delivery-Eintrittsgates sind Commit, Push und PR nur bei aktueller
ausdrücklicher `PublishPR`- oder `MergeAndSync`-Autorisierung zulässig. Die
PreMerge-Evidence darf erst nach aktuellen Remote-Checks und Review-Threads für
denselben Head akzeptiert werden; Merge, Cleanup und Sync verlangen danach
erneut aktuelle ausdrückliche `MergeAndSync`-Autorisierung. Weder Modus noch
Autorisierung erlauben Härtung, Governance-Reparatur, Folge-Intake-Erstellung,
Provider-Konfiguration, Secret-Verwendung, technische Gate-Bypässe oder
formale Compliance-Freigabe.

**Begründung / Rationale**: Delivery-Autorität und fachliche
Änderungsautorität sind getrennte Verträge.

## R20 – Feature 044 als Sandbox-Eingangsevidenz / Feature 044 as Sandbox Input Evidence

**Entscheidung / Decision**: Die kanonische Bewertung
`docs/security/secure-development/2026-08-29-sandbox-applicability/assessment.json`
und `specs/044-sandbox-secure-development-hardening/pr-evidence.md` werden
getrennt hashgebunden und für CL-10/CL-12 neu bewertet. `ConditionallyUsable`
ist eine begrenzte historische technische Entscheidung, keine aktuelle
formale Freigabe. Offene Approval-, Provider-, Egress-, Lifecycle- und
Plattformgrenzen bleiben sichtbar.

**Begründung / Rationale**: FR-013 verlangt ausdrücklich die neue Prüfung von
Feature 016 und Feature 044. Eine reine Feature-016-Bindung würde die jüngste
Sandbox-Evidence und ihre Human-/External-only-Grenzen auslassen.

**Verworfene Alternative / Rejected alternative**: Die Feature-044-Empfehlung
pauschal als frischen positiven Claim zu übernehmen würde die dokumentierten
Proof-Grenzen und die Audit-only-Autorität verletzen.
