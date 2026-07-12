# Lastenheft 09: Pre-Wave-5-Konformitätsabschluss

## 0. Dokumentstatus

**Vorgesehener Spec-Kit-Branch:** `027-pre-wave5-conformance-closure`

**Verbindlicher Zeitpunkt:** nach dem vollständig gemergten Feature
`024-tv203-freevision-conformance-audit`; die findings-basierten Features 025
und 026 bleiben wegen leerer Owner-Mengen unterdrückt.

**Lieferart:** Revalidation, Integrationsevidence und formelle Wave-5-
Gateentscheidung ohne Runtime- oder API-Änderung.

**Kanonische Sprache:** Deutsch; englische Erklärungen folgen als zweiter Block.

*This requirements document defines the mandatory Feature 027 closure after
the merged Feature 024 audit. Features 025 and 026 remain suppressed because
their accepted finding sets are empty. The feature revalidates integration and
release evidence without changing runtime behavior or public APIs.*

---

## 1. Ausgangslage

Feature 024 hat 151 historische `.cc`-Implementierungsdateien, 119 gepflegte
produktive C#-Dateien, 176 exportierte öffentliche Typen und 48 Framework-
Verträge in 16 Domänen maschinenprüfbar inventarisiert. Die Entscheidungen
lauten 13-mal `Aligned`, 34-mal `IntentionalModernization` und einmal
`ConsciouslyOmitted`; `BehavioralDrift`, `EvidenceGap` und Findings stehen
jeweils bei 0.

Das reine Audit darf Wave 5 nicht allein freigeben. Feature 027 muss den
gemergten Stand erneut prüfen, die leeren Finding-Mengen bestätigen, alle
Integrations- und Release-Gates ausführen und erst danach die formelle
Pre-Wave-5-Sperre aufheben.

*Feature 024 created a machine-verifiable inventory and found no drift or
evidence gap. Feature 027 independently reruns the merged evidence, full
integration, and release gates before Wave 5 may start.*

## 2. Verbindliche Eingaben

1. `Lastenheft_08_TV203-FreeVision-Conformance-Audit.024-tv203-freevision-conformance-audit.md`
2. alle Artefakte unter `specs/024-tv203-freevision-conformance-audit/`
3. Feature-PR `hindermath/TuiVision#62`, Merge
   `5c0a4d7cd0dfc633b8d30bd416c0cbf183c84d39`
4. Closeout-PR `hindermath/TuiVision#63`, Merge
   `f3fd98fcb6ee1eaf9957abd9bd6cb346fd7d20e4`
5. Retrospektiv-PR `hindermath/TuiVision#64` und dessen gemergter `main`-Stand
6. `docs/porting-status.md`, `Pflichtenheft.md`,
   `Lastenheft_Abarbeitungsreihenfolge.md`, `AGENTS.md` und Constitution
7. Borland/`tv203s/` als read-only Primärreferenz und der in 024 gepinnte
   offizielle Free-Vision-Commit
   `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` nur als sekundäre Evidence.

Die historischen Entscheidungen aus 024 dürfen nicht still umgeschrieben
werden. Eine legitime Baselineänderung benötigt eine sichtbare Audit-Revision
mit neuem Proof und erneuter Reviewentscheidung.

## 3. Ziele

1. Beweisen, dass Auditdaten, Inventare, Contracts, Quellenhashes und Proof-
   Referenzen auf dem gemergten Stand weiterhin vollständig und eindeutig sind.
2. Beweisen, dass keine Produkt-, API-, Paket-, Beispiel- oder historische
   Source-Änderung zwischen Auditbaseline und Closure unbemerkt blieb.
3. Die exakten Owner-Mengen `Core025 = 0` und `ComponentData026 = 0` erneut
   bestätigen; keine leeren Ersatzfeatures erzeugen.
4. Vollständige Release-, Coverage-, DocFX-, A11Y-, Secret-, Scope- und Remote-
   Evidence auf dem Closure-Head liefern.
5. Wave 5 nur nach vollständig bestandenem Gate formell freigeben und den
   nächsten Intake eindeutig benennen.

## 4. Scope

### 4.1 Im Scope

- read-only Prüfung der produktiven Frameworkmodule, Beispiele und historischen
  Quellen gegen die 024-Baseline
- Ausführung und gegebenenfalls test-only Härtung vorhandener 024-Evidence-
  Validatoren, falls nur der Closure-Nachweis selbst unvollständig ist
- ein separates `specs/027-pre-wave5-conformance-closure/closure-evidence.md`
- Spec-, Plan-, Task-, Checklist- und PR-Evidence für 027
- Aktualisierung von Pflichtenheft, Reihenfolge, Agent-Kontext und Statistik
  erst nach bestandenem lokalen Gate
- vollständiger autorisierter `MergeAndSync`-Abschluss mit kausalem Closeout
  nur wenn post-merge Fakten nicht vorher wahr sein können

### 4.2 Nicht im Scope

- Runtime- oder öffentliches Verhalten ändern
- API-Signaturen, Pakete oder Abhängigkeiten ändern
- Findings neu interpretieren oder still als behoben markieren
- Feature 025 oder 026 mit leerem Scope anlegen
- neue Beispiele portieren oder Wave 5 beginnen
- visuelle Komponenten überarbeiten
- Free Vision oder andere externe Quellen vendoren oder kopieren
- `tv203s/`, `TVDEMOS/` oder `TVFM/` ändern
- breite Framework-, Test- oder Workflow-Revision
- einen Upstream-Preset-Beitrag ohne reproduzierbare portable Lücke behaupten.

*Feature 027 may strengthen closure evidence and tests only. Product behavior,
public APIs, dependencies, examples, historical sources, and Wave 5 remain out
of scope.*

## 5. Revalidation-Vertrag

### R-PW5-001: Unveränderte Auditidentität

Der Closure-Lauf bestätigt die 024-Run-ID, Schema-/Contractversion, alle 16
Domänen, 48 Contract-IDs, 15 Free-Vision-Source-Records und die gepinnte
Quellenidentität. Kein Contract darf verschwinden, dupliziert oder ohne
Audit-Revision umklassifiziert werden.

### R-PW5-002: Live-Inventare

Die vorhandenen Evidence-Tests müssen auf dem Closure-Head erneut exakt 151
historische Ledgerzeilen, 119 gepflegte produktive C#-Dateien und 176
exportierte öffentliche Typen bestätigen. Eine Abweichung blockiert Closure,
bis Ursache und zulässiger Umgang reviewt sind.

### R-PW5-003: Entscheidungen und Findings

Die Entscheidungsmengen bleiben 13 `Aligned`, 34
`IntentionalModernization`, 1 `ConsciouslyOmitted`, 0 `BehavioralDrift` und 0
`EvidenceGap`. Das Finding-Ledger bleibt exakt leer. Jede Abweichung erzeugt
keine automatische Runtime-Reparatur, sondern stoppt Feature 027 und öffnet
eine sichtbare Audit-Revision.

### R-PW5-004: Proof-Referenzen

Alle 94 konkreten `path::method`-Referenzen müssen weiterhin existieren. Die
fokussierte Audit-Suite muss malformed JSON, unbekannte IDs, Duplikate,
Inventardrift, Quellenhashdrift, Finding-Routing und Gatezustand weiterhin
deterministisch ablehnen.

### R-PW5-005: Baseline-Diff

Zwischen dem geprüften 024-Produktstand und Closure werden Änderungen in
`src/`, `examples/`, Projekt-/Paketmetadaten, öffentlichen APIs und
historischen Quellen explizit aufgelistet. Evidence-, Closeout- und
Retrospektivänderungen gelten nicht als Produktdrift. Jede andere Änderung
benötigt eine reviewte Re-Evaluierung.

### R-PW5-006: Full Gates

Closure verlangt:

- fokussierte 024-Conformance-Tests
- vollständige Release-Tests
- kanonische Coverlet-Gates mit mindestens 70 Prozent je Gate-Assembly
- `git diff --check` und `dotnet format --verify-no-changes`
- DocFX mit 0 Fehlern und ohne neue Warnung
- Playwright/Axe sowie UTF-8-Lynx auf repräsentativen Seiten
- Secret-, Generated-Output-, Dependency-, API-, Runtime-, Example- und
  Historical-Source-Scans
- grüne erforderliche Remote-Checks und null umsetzbare Review-Threads.

## 6. Stop- und Routingregeln

Feature 027 stoppt vor einer Wave-5-Freigabe bei:

- neuem `Critical`- oder `High`-Finding
- beliebigem ungeklärtem `BehavioralDrift` oder `EvidenceGap`
- nicht leerer `Core025`- oder `ComponentData026`-Menge
- geänderter öffentlicher API oder Produktlogik ohne Audit-Revision
- fehlgeschlagenem Release-, Coverage-, Dokumentations-, A11Y-, Secret- oder
  Remote-Pflichtgate
- notwendiger menschlicher Produkt-, Lizenz-, Provider- oder Breaking-Change-
  Entscheidung.

Ein Stop erzeugt keine stillschweigende Scope-Erweiterung. Evidence benennt
Ursache, Owner, Reproduktionsweg und nächsten autorisierten Schritt.

## 7. Governance und Autonomie

Alle sieben installierten Preset-Schichten werden mit ihren aktuellen lokalen
Versionen auf Anwendbarkeit geprüft. Trigger-basierte `N/A`-Entscheidungen aus
024 bleiben nur gültig, wenn der tatsächliche Closure-Diff ihre Voraussetzungen
nicht verändert.

Der autonome Lauf verwendet `MergeAndSync`. Remote-Schreib-, Merge- und Bypass-
Rechte folgen ausschließlich der ausdrücklichen Kampagnenautorität. Ein enger
Admin-Bypass ist nur zulässig, wenn alle technischen Checks grün sind, null
umsetzbare Threads verbleiben und allein Human Approval blockiert.

Der in Home-Baseline-Commit `db2bd86` korrigierte PowerShell-Homogeneity-Pfad
wird im 027-Preflight erneut mit explizitem Repository-Root, Exitcode,
parsebarem JSON und leerem Fehlerkanal geprüft. Eine Preset-Änderung entsteht
nur, wenn Command, Checklist oder Template danach noch eine portable Lücke hat.

## 8. Evidence und Abschluss

`closure-evidence.md` dokumentiert mindestens:

- Baseline- und Closure-SHAs
- Dataset-, Inventory-, Decision-, Finding-, Source- und Proof-Zählungen
- Produkt-/API-/Dependency-/Example-/Historical-Diffentscheidung
- jeden lokalen und Remote-Befehl mit Ergebnis und Proof-Grenze
- Governance-Anwendbarkeit, Owner, Reviewer, Datum und Re-Evaluierungstrigger
- 025-/026-Suppression und 027-Gateentscheidung
- Wave-5-Status und nächsten Intake
- Reviewverfügbarkeit, Threads, Bypass, Merge und Main-Sync
- Retrospektiv- und Home-Baseline-Handoff-Entscheidung.

Nach bestandenem Closure-Gate:

1. `Pflichtenheft.md` markiert den Pre-Wave-5-Abschluss als erledigt.
2. `Lastenheft_Abarbeitungsreihenfolge.md` nennt Wave 5 als nächsten Intake.
3. alle fünf Agentenflächen erhalten denselben abgeschlossenen 027-Kontext.
4. das Lastenheft wird branch-suffigiert archiviert.
5. Wave 5 darf als Feature 028 vorbereitet werden, wird in 027 aber nicht
   begonnen.

## 9. Erfolgskriterien

- **SC-PW5-001:** Alle 16 Domänen, 48 Contracts, 151/119/176 Inventare, 15
  externe Quellenrecords und 94 Proof-Referenzen sind erneut maschinenprüfbar.
- **SC-PW5-002:** Entscheidungen bleiben 13/34/1/0/0 und Findings exakt 0.
- **SC-PW5-003:** 025 und 026 bleiben ohne Branch, Verzeichnis und PR.
- **SC-PW5-004:** Alle lokalen und Remote-Pflichtgates bestehen.
- **SC-PW5-005:** Der Closure-Diff enthält keine Produkt-, API-, Dependency-,
  Example- oder Historical-Source-Änderung.
- **SC-PW5-006:** Wave 5 wird erst nach bestandenem 027-Merge freigegeben und
  bleibt bis dahin sichtbar blockiert.
- **SC-PW5-007:** Alle erzeugten Erklärungen sind deutsch zuerst, englisch
  danach, CEFR-B2 und text-first zugänglich.

*Success means unchanged, machine-verifiable audit cardinalities, all local and
remote gates passing, no empty remediation features, no product-scope drift,
and a formal Wave-5 release only after Feature 027 merges.*
