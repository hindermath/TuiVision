# Lastenheft: RL-SE-/Checklist-Selbstpruefung

**Repository:** TuiVision
**Dokumenttyp:** Spec-Kit Intake / Lastenheft
**Status:** vorbereitet fuer separaten RL-SE-/CL-Selbstpruefungslauf
**Stand:** 2026-06-29

## 1. Zweck

Dieses Lastenheft beschreibt einen spaeteren Spec-Kit-Prueflauf gegen die
generische Secure-Development-Basis. Ziel ist nicht sofortige Haertung, sondern
eine nachvollziehbare Selbstpruefung, ob TuiVision die Anforderungen aus
Richtlinie Sichere Entwicklung, Checklisten, Sammelband, mitgeltenden
Dokumenten und Governance-Presets behandelt, begruendet oder als offen markiert.

Das Lastenheft startet keinen Spec-Kit-Lauf und erzeugt keine
projektspezifischen `docs/security/`-Nachweise. Diese entstehen erst im spaeter
bewusst gestarteten Lauf.

## 2. Ausgangslage

TuiVision ist ein Repository in der Level-1-/Level-2-Arbeitsstruktur.
Sichere Entwicklung ist fuer heutige, geopolitisch angespannte
Softwareentwicklung ein Muss. Deshalb wird die Selbstpruefung unabhaengig davon
vorbereitet, ob die Primaersprache als Memory-Safe Language (MSL) erkannt wird.

MSL-Status bleibt ein Pruefpunkt. Er ersetzt aber keine Pruefung von APIs,
I/O, Authentifizierung, Autorisierung, Kryptografie, Logging,
Abhaengigkeiten, Build-/Release-Pfaden oder agentischer Entwicklung.

## 3. Pruefgrundlagen

Der spaetere Spec-Kit-Lauf muss mindestens diese Grundlagen beruecksichtigen:

- `docs/secure-development/Richtlinie_Sichere-Entwicklung.md`
- `docs/secure-development/Checklistensammelband_Sichere-Entwicklung.md`
- `docs/secure-development/checklisten/CL_01_*.md` bis `CL_12_*.md`
- `docs/secure-development/mitgeltende-dokumente/`
- `docs/secure-development/mitgeltende-dokumente/Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md`
- `constitution.md` und `.specify/memory/constitution.md`, soweit vorhanden
- installierte Governance-Presets, soweit sie Projekt-Policy sind
- vorhandene Spec-Kit-Artefakte, `docs/security/`, Tests, CI und Review-Notizen

## 4. Zielbild des spaeteren Prueflaufs

Der spaetere Lauf erzeugt eine Evidenz- und Anwendbarkeitsmatrix. Jeder
relevante Pruefpunkt erhaelt einen Status:

- `Applicable`: gilt fuer TuiVision und braucht Evidenz oder Umsetzung.
- `AlreadySatisfied`: ist im aktuellen Stand bereits nachweisbar erfuellt.
- `N/A`: gilt fuer diesen Lauf nicht und braucht eine kurze Begruendung.
- `Open`: gilt, ist aber noch nicht ausreichend geklaert oder belegt.
- `FollowUp`: fachlich relevant, aber bewusst ausserhalb dieses Laufs.

Jeder Pruefpunkt braucht mindestens Begruendung, Evidenzpfad oder Open-Markierung,
Owner, Follow-up und Re-Evaluation-Trigger.

## 5. Scope

Im spaeteren Prueflauf werden insbesondere behandelt:

- Standards-Anwendbarkeit, MSL-Status und sprachspezifische Secure-Coding-Regeln
- Eingabevalidierung, Trust Boundaries, Fehlerbehandlung und sichere Ausgabe
- Authentifizierung, Autorisierung, Rollen, Secrets und Konfiguration
- Datei-, Netzwerk-, Datenbank-, UI-, CLI-, API- oder Prozessgrenzen
- Dependency-, Supply-Chain-, SBOM-, AI-SBOM-, VEX- und SLSA-Punkte
- BSI C3A/C5, NIS2, CRA, EU AI Act und DORA nur bei fachlicher Anwendbarkeit
- A11Y/WCAG 2.2 AA, DE-first/EN-second, CEFR B2 und didaktische Kommentare
- Sandbox, agentische Entwicklung, Toolchain und Spec-Kit-Governance

## 6. Abgrenzung

- Keine automatische Haertung des Repositorys.
- Kein Sammellauf ueber mehrere Repositories.
- Keine Feature-Branch-Erzeugung durch dieses Lastenheft.
- Keine erfundene formale Freigabe, kein QISMS-/Audit-Claim ohne Evidenz.
- Keine Repo-Sichtbarkeit, Branch-Protection, Secrets, Provider oder Modelle konfigurieren.
- Keine echte Kundendaten, produktiven Tokens oder privaten Pfade dokumentieren.

## 7. Mindestanforderungen an den spaeteren Spec-Kit-Lauf

1. Aktuellen Repository-Stand lesen und bereits erledigte Punkte nicht neu umsetzen.
2. Die Verzahnungsdatei zuerst nutzen, um Richtlinie, CLs, Presets und Evidenzpfade zuzuordnen.
3. Alle relevanten Pruefpunkte als `Applicable`, `AlreadySatisfied`, `N/A`, `Open` oder `FollowUp` klassifizieren.
4. Fuer `Applicable` und `AlreadySatisfied` konkrete Evidenzpfade benennen.
5. Fuer `N/A` und `FollowUp` kurze technische oder fachliche Begruendung erfassen.
6. Fuer `Open` Owner, Follow-up, Prioritaet, Risiko und Re-Evaluation-Trigger festhalten.
7. Positive Aussagen zur Einhaltung nur mit konkreter Evidenz treffen.
8. Human-only-Punkte sichtbar abgrenzen und nicht als erledigt behaupten.
9. Ergebnis als auditfaehige, fuer Auszubildende verstaendliche Markdown-Dokumentation ablegen.

## 8. Erwartete Ergebnisartefakte

| Artefakt | Erwartung |
|---|---|
| Spec-Kit `spec.md` | Ziel, Scope, Nicht-Ziele, Pruefgrundlagen und Statuslogik dokumentiert |
| Spec-Kit `plan.md` | Pruefstrategie, Evidenzpfade, Standards und Presets nachvollziehbar |
| Spec-Kit `tasks.md` | Konkrete Pruef-, Dokumentations- und Follow-up-Aufgaben ableitbar |
| Evidenzmatrix | `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, `FollowUp` mit Begruendung |
| `docs/security/` | Projektspezifische Nachweise oder begruendete N/A-/Open-Eintraege |
| Abschlussnotiz | Ergebnis, offene Risiken, Restrisiken und Re-Evaluation-Trigger |

## 9. Akzeptanzkriterien

- Alle relevanten Punkte aus Richtlinie, Sammelband, CL_01 bis CL_12 und mitgeltenden Dokumenten sind sichtbar behandelt.
- Kein relevanter Governance-Preset-Pruefpunkt wurde stillschweigend ausgelassen.
- Jeder ausgelassene Punkt ist als `N/A` oder `FollowUp` begruendet.
- Jeder offene Punkt ist als `Open` mit Owner, Folgeaktion und Re-Evaluation-Trigger dokumentiert.
- Jede positive Aussage verweist auf konkrete Evidenz.
- Das Projekt bleibt nach der Pruefung baubar, testbar und fuer Lernende nachvollziehbar.

## 10. Optimaler Specify-Prompt / Optimal Specify Prompt

```text
$speckit-specify
Nutze Lastenheft_RL-SE-Checklist-Selbstpruefung.md als verbindlichen Intake fuer einen separaten RL-SE-/Checklist-Selbstpruefungslauf in TuiVision.
Starte keinen Sammellauf ueber mehrere Repositories, erzeuge keine automatische Haertung und befuelle keine docs/security/-Nachweise ohne konkrete Spec-Kit-Aufgabe.
Erstelle eine fokussierte Feature-Spezifikation, die docs/secure-development/, Richtlinie_Sichere-Entwicklung.md, Checklistensammelband_Sichere-Entwicklung.md, CL_01 bis CL_12, mitgeltende Dokumente, Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md, constitution.md, .specify/memory/constitution.md und installierte Governance-Presets als Pruefgrundlagen beruecksichtigt.
Dokumentiere jeden relevanten Pruefpunkt als Applicable, AlreadySatisfied, N/A, Open oder FollowUp mit Begruendung, Evidenzpfad, Owner, Follow-up, Re-Evaluation-Trigger und Restrisiko.
Behandle sichere Entwicklung als Must-have. MSL-Status ist ein Pruefpunkt, aber keine Voraussetzung fuer diesen Selbstpruefungslauf.
```

## 11. Optimaler Autonomous-Prompt / Optimal Autonomous Prompt

```text
$speckit-autonomous Use `Lastenheft_RL-SE-Checklist-Selbstpruefung.md` as the
binding intake for a complete autonomous RL-SE and checklist self-review in
MergeAndSync mode.

Start only from clean synchronized main and determine the next free Spec Kit
feature number. Execute Specify, repeated Clarify, all useful security and
governance checklists, Plan, plan review, Tasks, repeated Analyze, Implement,
validation, repository delivery, and retrospective to convergence.

Use the secure-development policy, checklist collection, CL_01 through CL_12,
all governing documents, the policy-to-checklist-to-preset mapping,
constitution, installed presets, existing specs, tests, CI, and repository
evidence as binding review sources. Classify every relevant checkpoint as
Applicable, AlreadySatisfied, N/A, Open, or FollowUp. Record rationale,
evidence path, owner, concrete follow-up, priority, residual risk, and
re-evaluation trigger. Positive claims require concrete evidence.

Keep the run audit-focused. Do not perform broad automatic hardening, combine
multiple repositories, configure provider settings, expose private paths or
tokens, or claim human-only approval. Generate only non-empty,
finding-derived follow-up intakes and stop on destructive or policy-level
ambiguity.

Run all validation triggered by the resulting evidence and documentation.
Use remote authority only for this repository's non-empty feature delivery.
Merge only after applicable checks and reviews converge, then return to clean
synchronized main.
```
