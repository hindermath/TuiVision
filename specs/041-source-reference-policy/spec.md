# Feature Specification: Source Reference Policy

**Branch**: `041-source-reference-policy`

**Status**: Accepted for local implementation
**Binding intake**: `requirements/intakes/active/Lastenheft_Source-Reference-Policy.md`

## Purpose / Zweck

Feature 041 führt eine prospektive Drei-Achsen-Quellenpolicy ein. Akzeptierte
TuiVision-Verträge bleiben Produktnorm, der gepinnte Magiblot-Stand wird zuerst
als moderne Designreferenz geprüft, und Borland/`tv203s` bleiben die Referenz
für historische Absicht. Die Policy wird maschinenlesbar und in allen
ausgelösten Governance-Flächen konsistent.

*Feature 041 introduces a prospective three-axis source policy. Accepted
TuiVision contracts remain the product norm, the pinned Magiblot revision is
reviewed first as a modern design reference, and Borland/`tv203s` remain the
reference for historical intent. The policy is machine-readable and
consistent across every triggered governance surface.*

## User Stories / Nutzungsszenarien

### US1 – Quellenrollen eindeutig anwenden (P1)

Als Feature-Autor möchte ich vor einer historisch berührten Änderung wissen,
welche Quelle welche Frage beantwortet, damit moderne Architekturideen und
historische Absicht nicht mit der aktuellen Produktnorm verwechselt werden.

**Acceptance**: Alle fünf Rollen und die Reihenfolge TuiVision → Magiblot →
`tv203s` → materielle Vergleiche/Consumer sind dokumentiert und validiert.

### US2 – Konflikte nachvollziehbar entscheiden (P1)

Als Reviewer möchte ich genau eine benannte Disposition sehen, damit ein
Quellenkonflikt nicht still durch Rang, Sprache oder Vererbung gelöst wird.

**Acceptance**: Jede relevante Entscheidung ist `AdoptModernization`,
`PreserveHistoricalIntent`, `IntentionalTuiVisionDeviation` oder `N/A`.

### US3 – Externe Provenienz sicher begrenzen (P1)

Als Maintainer möchte ich einen exakten, read-only Pin und eine No-Copy-Grenze,
damit bewegliche Branches, unklare Lizenzbehauptungen und Vendorisierung nicht
unbemerkt Evidence werden.

**Acceptance**: Commit, Tree, Multipart-Lizenzgrenze, externer Checkout und
separater Pin-Update-Review werden fail-closed geprüft.

## Functional Requirements / Funktionale Anforderungen

- **FR-001**: Eine kanonische JSON-Policy definiert Rollen, Workflow, Pin,
  Dispositionen, Konfliktregeln, Provenienz und Re-Evaluation-Trigger.
- **FR-002**: Portable Bash- und PowerShell-Einstiege verwenden denselben
  deterministischen Validator und liefern gleiche Diagnosecodes.
- **FR-003**: Positive Policy und kontrollierte Ein-Ursachen-Negativ-Fixtures
  belegen Pin-, Branch-, Dispositions-, Prospektivitäts-, No-Copy- und
  Schemafehler.
- **FR-004**: Constitution, Pflichtenheft, Spec-/Plan-/Tasks-Anweisungen und
  gepflegte Agent-Flächen enthalten dieselbe Policy ohne widersprechende
  historische Altregel.
- **FR-005**: Bestehende abgeschlossene Features und Evidence bleiben
  unverändert; nur drei benannte Trigger lösen Re-Evaluation aus.
- **FR-006**: Produktcode, API, Beispiele, Pakete, Dependencies und historische
  Quellen bleiben unverändert.

## Success Criteria / Erfolgskriterien

- Beide Validatoren akzeptieren die kanonische Policy und lehnen jede Fixture
  mit dem erwarteten stabilen `SRP###`-Code ab.
- Alle dreizehn deklarierten Governance-Flächen enthalten den vollständigen
  Marker, exakten Pin und die vier Dispositionen.
- Homogeneity, Agent-Parität, Format-/Diff-, DocFX-, Axe- und Lynx-Gates sind
  grün; Produkt- und historische Roots zeigen null Delta.

## Historical source decision / Historische Quellenentscheidung

`AdoptModernization` für den Governance-Workflow selbst: Feature-030-Evidence
belegt den modernen Pin. `PreserveHistoricalIntent` bleibt die Rolle von
Borland/`tv203s`. Es wird kein historischer Produktvertrag geändert; externe
Quellen bleiben read-only und werden nicht kopiert.
