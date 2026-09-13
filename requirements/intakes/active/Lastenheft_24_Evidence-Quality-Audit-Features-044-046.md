<!-- intake-authoring:begin -->
# Lastenheft 24: Evidence-Qualitaetsaudit fuer Features 044 bis 046

**Dokumenttyp:** Eigenstaendiger Spec-Kit-Intake
**Status:** ReadyForReview
**Projekt:** TuiVision
**Zielgruppe:** Maintainer, Security-Reviewer, Auszubildende und KI-Agenten

## Zweck / Purpose

Feature 047 prueft unabhaengig, ob die fachlichen Aussagen, offenen Grenzen und
Null-Finding-Schluesse aus Features 044, 045 und 046 durch konkrete Evidence
getragen werden. Der Lauf wiederholt nicht pauschal die drei Implementierungen,
sondern bewertet deren Evidence-Qualitaet, Sichtbarkeit und Aussagegrenzen.

*Feature 047 independently checks whether the conclusions, open boundaries,
and zero-finding outcomes from Features 044, 045, and 046 are supported by
concrete evidence. It reviews evidence quality without broadly repeating the
three implementations.*

## Ausgangslage / Current State

- Feature 044 bewertet zwoelf Sandbox-Kontrollen und endet mit
  `ConditionallyUsable`; sechs Kontrollen bleiben `Open`.
- Feature 045 bewertet 157 RL-SE-Kontrollen mit `65 Applicable`,
  `13 AlreadySatisfied`, `38 N/A`, `36 Open` und `5 FollowUp`. Alle 157
  Statuswerte entsprechen der Feature-016-Baseline.
- Feature 046 inventarisiert 37 Quellen und 988 Evidence-Referenzen, bewertet
  aber alle 157 Kontrollen als `Open` und meldet zugleich null kanonische
  Beobachtungen und null umsetzbare Findings.
- Die drei Laufzustaende sind technisch `Completed`; dieser Status beweist
  Prozessintegritaet, nicht automatisch fachliche Vollstaendigkeit.
- Die Serie `tui-vision-delivery` ist abgeschlossen und bleibt unveraendert.

## Verbindliche Eingaben / Binding Inputs

- Die archivierten Intakes der Features 044, 045 und 046.
- Alle Feature-Artefakte unter `specs/044-*`, `specs/045-*` und `specs/046-*`.
- Die kanonischen Security-Evidence-Datensaetze und Leserprojektionen der drei
  Features.
- PRs 159/160, 162/163 und 164/165 samt Commit-, Check- und Review-Evidence.
- Die aktuellen GSDB-Quellen, Constitution, installierten Presets und
  repository-lokalen Nachweise nur soweit sie eine konkrete Aussage pruefen.

## Scope

1. Prozess-, Datei-, Hash-, Test-, Review- und Delivery-Evidence getrennt von
   fachlicher Aussagekraft bewerten.
2. Alle zwoelf Feature-044-Kontrollen erneut gegen ihre konkrete Evidence und
   offene Grenze pruefen.
3. Fuer alle 157 Kontroll-IDs die Feature-045- und Feature-046-Entscheidung
   gegen die kanonische Kontrolle und aktuelle direkte Evidence vergleichen.
4. Feature-045-Governance-Beobachtungen, Follow-ups und Human Boundaries auf
   Vollstaendigkeit, Deduplizierung und Sichtbarkeit pruefen.
5. Feature-046-Sprachprofile, Presets, Governance-Checkpoints,
   Evidence-Familien und Agentenflaechen mechanisch schliessen und
   risikobasiert semantisch pruefen.
6. Coverlet-Collector, MSTest-Freshness, beweglichen Workflow-Tag,
   Node-LTS-Abweichung und den Null-Sekunden-Review als verbindliche
   Finding-Kandidaten bewerten.
7. Bestaetigte Probleme in einem stabilen Finding-Register dokumentieren.

## Nicht-Ziele / Non-Goals

- Keine Aenderung bestehender Feature-044/045/046-Artefakte.
- Keine Runtime-, API-, Paket-, Projekt-, Beispiel- oder Workflow-Aenderung.
- Keine automatische Security-, Governance- oder Produktabstellung.
- Keine neue Zertifizierung, Rechts-, Provider- oder Organisationsfreigabe.
- Keine neuen Remediation-Intakes, Issues oder Folgefeatures in diesem Lauf.
- Keine Aenderung der abgeschlossenen Intake-Serie und kein erfundenes
  `Eligible`-Ziel.

## Bewertungsmodell / Assessment Model

Jede untersuchte Aussage erhaelt genau eine Entscheidung:

- `Supported`: Aktuelle direkte Evidence traegt die Aussage.
- `OpenBoundary`: Die offene Grenze ist korrekt und vollstaendig beschrieben.
- `EvidenceGap`: Evidence oder Aussagegrenze reicht nicht aus.
- `Contradicted`: Primaerquelle oder aktueller Zustand widerspricht der Aussage.
- `Duplicate`: Ein anderes Finding deckt denselben Sachverhalt vollstaendig ab.
- `Superseded`: Eine spaetere nachweisbare Aenderung hat die Aussage abgeloest.

Bestaetigte Probleme erhalten stabile `EQA###`-IDs, Kategorie, Schweregrad,
Ursprungsfeature, Claim, Evidence, Owner, Restrisiko und
Re-Evaluation-Trigger. Zulassige Kategorien sind `EvidenceDefect`,
`GovernanceFinding`, `TechnicalFinding`, `HumanBoundary` und `Informational`.

## Pruefmethode / Review Method

- Alle 12 Feature-044-Kontrollen und alle 157 Kontroll-Crosswalk-Zeilen werden
  vollstaendig semantisch bewertet.
- Alle 10 Sprachprofile, 12 Presets, 46 Governance-Checkpoints und 12
  Evidence-Familien aus Feature 046 werden vollstaendig geprueft.
- Alle 123 Agentenflaechen und 988 Evidence-Referenzen werden mechanisch auf
  Bestand, Hash, Eindeutigkeit und Zugehoerigkeit geprueft.
- Claim-Relevanz wird fuer jeden Finding-Kandidaten und fuer eine
  deterministische Stichprobe von mindestens 50 Evidence-Referenzen manuell
  geprueft.
- Mindestens eine Agentenflaeche je Familie und Typ sowie jede erkannte
  Anomalie wird semantisch geprueft.
- Ein Validator berechnet Summen aus dem Datensatz. Er darf weder null Findings
  noch eine bestimmte Dispositionsverteilung fest einprogrammieren.

## Erwartete Artefakte / Expected Artifacts

- `finding-register.json` als kanonische Finding-Quelle.
- `control-crosswalk.json` fuer alle 157 Kontroll-IDs.
- `audit-report.md` als German-first/English-second Leserprojektion.
- `pr-evidence.md` mit Hashes, Befehlen, Stichprobe, Review- und Proof-Grenzen.
- Ein kompakter test-only Validator mit positiven und negativen Fixtures.
- Eine Retrospektive mit `NoPromotion` oder begruendetem `PresetFollowUp`.

## Akzeptanzkriterien / Acceptance Criteria

1. Alle 12 Sandbox-Kontrollen und alle 157 Crosswalk-Zeilen sind genau einmal
   enthalten und besitzen genau eine Qualitaetsentscheidung.
2. Jede Feature-046-Nebenmenge entspricht ihrer aktuellen Kardinalitaet;
   fehlende, doppelte oder fremde Eintraege werden abgelehnt.
3. Jeder konkrete Hinweis aus PR-Evidence besitzt eine nachvollziehbare
   Entscheidung und kann nicht aus der Summary verschwinden.
4. Null positive Evidence-Luecken gelten bei null positiven Aussagen nicht als
   Qualitaetsbeweis, sondern werden als nicht aussagekraeftige Metrik markiert.
5. Finding-Zahlen werden ausschliesslich aus dem Register berechnet.
6. Der manuelle Review dokumentiert echte Quelle-Claim-Evidence-Entscheidungs-
   Traces; eine reine Dauerangabe ist kein Qualitaetsnachweis.
7. Technische Findings in den geprueften Features blockieren den Auditabschluss
   nicht. Unvollstaendige Feature-047-Evidence oder ungeklärte Auditentscheidung
   blockiert fail-closed.
8. Mindestens eine unabhaengige fachliche Review-Bewertung ist fuer eine spaetere
   Remote-Lieferung erforderlich; Admin-Bypass ersetzt dieses Gate nicht.
9. Es entstehen keine Produktabstellung und kein Folge-Intake.

<!-- intake-authoring:prompts -->
## Kopierbare Spec-Kit-Prompts / Copy-Ready Spec Kit Prompts

### Specify

<!-- spec-kit-command-id: speckit.specify -->
```text
$speckit-specify Use requirements/intakes/active/Lastenheft_24_Evidence-Quality-Audit-Features-044-046.md as the binding intake. Create Feature 047 as an independent evidence-quality audit of Features 044, 045, and 046. Preserve the completed intake series and all existing feature evidence unchanged. Do not implement, commit, push, create a pull request, merge, remediate findings, or create follow-up intakes.
```

### Autonomous

<!-- spec-kit-command-id: speckit.autonomous -->
```text
$speckit-autonomous Execute the complete autonomous Spec Kit run for Feature 047 using requirements/intakes/active/Lastenheft_24_Evidence-Quality-Audit-Features-044-046.md as the binding intake. Delivery mode: LocalImplementation. Independently assess the evidence quality of Features 044, 045, and 046, produce a canonical finding register and 157-control crosswalk, and validate them without preselecting a zero-finding outcome. Preserve all audited artifacts, product code, workflows, packages, examples, and the completed intake series. Do not push, create or merge a pull request, use bypass authority, remediate findings, create follow-up intakes, or start another feature.
```
<!-- intake-authoring:end -->
