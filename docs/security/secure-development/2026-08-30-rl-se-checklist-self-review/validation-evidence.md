# Validierungsnachweise / Validation Evidence

## Deutsch

Fachlicher Auditstatus: `Passed`. Format, Supply Chain, Scope, Secret-Scan,
DocFX, A11Y, Textbrowser und Coverage folgen als lokale Delivery-Gates.

## English

Domain audit status: `Passed`. Formatting, supply chain, scope, secret scan,
DocFX, accessibility, text browser, and coverage follow as local delivery
gates.

| Nachweis / Evidence | Status | Grenze / Boundary |
|---|---|---|
| Vertical slice | Not Run | `CL-01-01` missing by design |
| Complete audit | Passed | 2/2 focused tests; 157 controls, 12 presets, projections and 20 fail-closed fixtures |
| Negative fixtures | Not Run | Validator incomplete |
| Vertical slice Red | Passed as expected | Non-zero; stable primary code `RLSE003`; only `CL-01-01` absent |
| Vertical slice Green | Passed | 1/1; schema, fields, evidence, preset, projection and path boundary |
| Negative fixtures | Passed | 20 deterministic inputs; `RLSE001` through `RLSE012`; no writes |

Der fokussierte Komplettbefehl lief mit Exitcode `0`: 2/2 Tests bestanden.
Alle 157 eindeutigen Kontrollen, die Kapitelzahlen
`12/13/15/10/13/11/12/13/17/17/12/12`, exakt fuenf Statuswerte, alle
Pflichtfelder, zwoelf Presets, JSON-/Markdown-Paritaet und die atomare
Negativablehnung sind belegt. / The focused complete command exited `0`: 2/2
tests passed. It proves all 157 unique controls, the exact chapter counts,
exactly five statuses, all mandatory fields, twelve presets, JSON/Markdown
parity, and atomic negative rejection.

## Kapitelvalidierung / Chapter validation

| Grenze / Boundary | Kapitelzahl / Chapter count | Ergebnis / Result |
|---|---:|---|
| CL-01 | 12 | Passed; 12 eindeutige Zeilen / 12 unique rows |
| CL-02 | 13 | Passed; 13 eindeutige Zeilen / 13 unique rows |
| CL-03 | 15 | Passed; 15 eindeutige Zeilen / 15 unique rows |
| CL-04 | 10 | Passed; 10 eindeutige Zeilen / 10 unique rows |
| CL-05 | 13 | Passed; 13 eindeutige Zeilen / 13 unique rows |
| CL-06 | 11 | Passed; 11 eindeutige Zeilen / 11 unique rows |
| CL-07 | 12 | Passed; 12 eindeutige Zeilen / 12 unique rows |
| CL-08 | 13 | Passed; 13 eindeutige Zeilen / 13 unique rows |
| CL-09 | 17 | Passed; 17 eindeutige Zeilen / 17 unique rows |
| CL-10 | 17 | Passed; 17 eindeutige Zeilen / 17 unique rows |
| CL-11 | 12 | Passed; 12 eindeutige Zeilen / 12 unique rows |
| CL-12 | 12 | Passed; 12 eindeutige Zeilen / 12 unique rows |

## Governance- und Human-Grenzen / Governance and human boundaries

Deutsch: Exakt zwoelf installierte Presets wurden mit den festgelegten
Versionen und aktuellen Hashes bewertet. Fuenf Governance-Beobachtungen bleiben
mit `repairPerformed=false` sichtbar; sie erzeugen weder Reparatur, Issue,
Branch, Intake noch Folgefeature. Architektur-, iSAQB- und Drei-Achsen-
Quellenpolicy sind `N/A`, weil weder Produktvertrag, Trust Boundary,
historischer Zweck noch Magiblot-Pin geaendert wurde. Es entstanden keine ADRs,
Views oder Threat-Modelle. Die Lizenzgrenze bleibt
`MultipartNotRepositoryWideMIT`. Neu bewertet wird bei einer Aenderung von
Produktvertrag, Trust Boundary, historischem Zweck oder freigegebenem
Magiblot-Pin.

English: Exactly twelve installed presets were assessed with the fixed versions
and current hashes. Five governance observations remain visible with
`repairPerformed=false`; they create no repair, issue, branch, intake, or
follow-up feature. Architecture, iSAQB, and three-axis source-reference policy
are `N/A` because no product contract, trust boundary, historical purpose, or
Magiblot pin changed. No ADR, view, or threat model was created. The licence
boundary remains `MultipartNotRepositoryWideMIT`. Reassessment is triggered by
a change to the product contract, trust boundary, historical purpose, or an
approved Magiblot pin.

Deutsch: Sieben Human-/External-only-Domaenen bleiben bei konkret benannten
Rollen und besitzen `agentMayClose=false`. CRA, NIS2, DORA, EU AI Act, BSI C3A
und BSI C5 sind getrennte Rechts-, Organisations- oder Providerfragen ohne
befugte publizierbare Freigabe. QISMS, Zertifizierung und formale Auditfreigabe
bleiben ebenfalls offen. Feature 044 ist nur begrenzte technische
`ConditionallyUsable`-Eingangsevidence. AI-SBOM ist fuer reine Entwicklungs-KI
`N/A`; Runtime-/Produkt-KI, Modelle, Datensaetze, Inferenz-Infrastruktur oder
ausgelieferte KI-Komponenten loesen eine Neubewertung aus. Die abgeleitete Zahl
unbefugter positiver Human-Claims ist null.

English: Seven human/external-only domains remain with specifically named roles
and have `agentMayClose=false`. CRA, NIS2, DORA, the EU AI Act, BSI C3A, and BSI
C5 are separate legal, organisational, or provider questions without authorised
publishable approval. QISMS, certification, and formal audit approval also
remain open. Feature 044 is bounded technical `ConditionallyUsable` input
evidence only. AI SBOM is `N/A` for development-only AI; runtime or product AI,
models, datasets, inference infrastructure, or delivered AI components trigger
reassessment. The derived unauthorised positive human-claim count is zero.

Die Checkpoints T077 und T084 werden durch die runner-owned Phasenauswertung
gebunden; `autonomous-run-state.json` wurde nicht manuell geaendert. / The T077
and T084 checkpoints are bound by the runner-owned phase evaluation;
`autonomous-run-state.json` was not edited manually.

## Inklusive manuelle Stichprobe / Inclusive manual sample

Deutsch: Die Dokumente sind German-first/English-second, ungefaehr CEFR B2,
verwenden semantische Ueberschriften und Tabellen sowie beschreibende Links.
Status, Risiko und Grenzen werden ausgeschrieben. Es gibt keine alleinige
Farb-, Layout-, Bild-, Pointer- oder Tastaturmausbedeutung. Die lineare
Textansicht ist fuer Braillezeilen, Screenreader, Tastatur und Textbrowser
gleichwertig. Die ordinale Regel waehlt die erste Zeile aus einem fruehen, die
sechste aus einem mittleren und die letzte aus dem spaeten Kapitel. Jede
deterministische `jq`-Verfolgung dauerte unter einer Sekunde und damit deutlich
weniger als drei Minuten:

| Kontrolle | Quelle -> Entscheidung -> Evidence -> Verantwortung |
|---|---|
| `CL-01-01` | `CL_01_Standards-Anwendbarkeit.md` -> `Applicable` -> vier aktuelle Pfade -> Maintainer, kein pauschaler Erfuellungsclaim, Follow-up und Architektur-/Dependency-/Runtime-/Governance-Trigger |
| `CL-06-06` | `CL_06_Schwachstellenoffenlegung.md` -> `Applicable` -> fuenf aktuelle Pfade -> Maintainer, begrenztes Risiko, Follow-up und Dependency-/Release-/Disclosure-/Runtime-/Regulatory-Trigger |
| `CL-12-12` | `CL_12_Agentische-KI-Sandbox.md` -> `AlreadySatisfied` -> vier aktuelle Pfade einschliesslich Feature 044 -> Maintainer, niedriges Snapshot-Risiko, kein offenes Follow-up und Agent-/Umgebungs-/Sandbox-Trigger |

English: The documents are German-first/English-second, approximately CEFR B2,
and use semantic headings and tables plus descriptive links. Status, risk, and
boundaries are written out. No meaning depends only on colour, layout, an
image, a pointer, or mouse input. The linear text view is equivalent for
Braille displays, screen readers, keyboard use, and text browsers. The ordinal
rule selects the first row from an early chapter, the sixth from a middle
chapter, and the final row from the late chapter. Each deterministic `jq` trace
took under one second, well below three minutes. The table records the complete
source-to-trigger path.

## Trigger-Dispositionen / Trigger dispositions

| Flaeche / Surface | Disposition | Begruendung / Rationale | Neubewertung / Reassessment |
|---|---|---|---|
| Neue Scripts, PowerShell-Help, Manpage, Cmdlet | N/A | Kein scriptfoermiger Diff / No script-shaped diff | Bei neuem oder geaendertem Script, Help, Manpage oder Cmdlet / On a new or changed script, help, manpage, or cmdlet |
| Agenten, Templates, Constitution | NoUpdateRequired | Audit liest Governance nur; keine Vertragsmutation / Audit reads governance only; no contract mutation | Bei Agent-, Template-, Constitution- oder Vertragsaenderung / On an agent, template, constitution, or contract change |
| Produkt, Runtime, Public API/XML, Pakete, Projekte | N/A | Durch Audit-/Test-only-Scope ausgeschlossen / Excluded by audit/test-only scope | Sobald eine dieser Flaechen in den Scope tritt / When any such surface enters scope |
| Architektur und Quellenpolicy | N/A | Kein Produktvertrag, Trust Boundary, historischer Zweck oder Magiblot-Pin geaendert / No product contract, trust boundary, historical purpose, or Magiblot pin changed | Bei einer solchen Aenderung / On such a change |
| Formale Human-Approval | N/A | Repository-Evidence darf keine Freigabe erteilen / Repository evidence cannot grant approval | Bei befugter publizierbarer Human-Evidence / On authorised publishable human evidence |

## Supply-Chain-Gates / Supply-chain gates

- T095 Dependency Currency: Exitcode `0`, Quellenfreshness 2026-08-30. Das
  Top-Level-Testpaket MSTest `4.3.2` besitzt `4.3.3` als neuere Version; mehrere
  transitive Test-/Telemetry-/Coverage-Pakete melden neuere Versionen. Produkt-
  und Beispielprojekte melden keine Top-Level-Updates. Der Audit klassifiziert
  dies als read-only Currency-Finding und aendert weder Paket, Tool, Projekt
  noch Follow-up-Artefakt. Ein privater Feed-Identifier aus der Toolausgabe
  wird absichtlich nicht in Evidence kopiert.
- T095 dependency currency: exit code `0`, source freshness 2026-08-30. The
  top-level test package MSTest `4.3.2` has `4.3.3` available; several
  transitive test, telemetry, and coverage packages report newer versions.
  Product and example projects report no top-level updates. This is a read-only
  currency finding and changes no package, tool, project, or follow-up
  artifact. A private feed identifier from tool output is intentionally not
  copied into evidence.
- T096 Vulnerability Review: Exitcode `0`, Advisory-Freshness 2026-08-30; fuer
  alle 51 Solution-Projekte wurden aus den aktuell erreichbaren Quellen keine
  verwundbaren direkten oder transitiven Pakete gemeldet. Der Nachweis ist ein
  zeitgebundener NuGet-Advisory-Snapshot, keine Garantie gegen unbekannte oder
  spaeter publizierte Schwachstellen; private Feed-Identifier bleiben redigiert.
- T096 vulnerability review: exit code `0`, advisory freshness 2026-08-30; no
  vulnerable direct or transitive package was reported for any of the 51
  solution projects from the currently reachable sources. This is a
  time-bounded NuGet advisory snapshot, not a guarantee against unknown or
  later advisories; private feed identifiers remain redacted.
- T097 Deprecation Review: Exitcode `0`, Freshness 2026-08-30; fuer alle 51
  Solution-Projekte wurden aus den aktuell erreichbaren Quellen keine
  veralteten direkten oder transitiven Pakete gemeldet. Die Proof-Grenze ist
  der aktuelle NuGet-Metadatensnapshot; private Feed-Identifier bleiben
  redigiert und es erfolgte keine Paketmutation.
- T097 deprecation review: exit code `0`, freshness 2026-08-30; no deprecated
  direct or transitive package was reported for any of the 51 solution
  projects from the currently reachable sources. The proof boundary is the
  current NuGet metadata snapshot; private feed identifiers remain redacted and
  no package was changed.

## Workflow-Referenzen / Workflow references

T098 inventarisierte mit dem vorgeschriebenen `git grep -n -E` exakt 23
`uses:`-Zeilen. 22 Referenzen sind auf einen vollstaendigen 40-stelligen
Commit-SHA fixiert. Eine bestehende Referenz,
`.github/workflows/requirements-intake-governance.yml` mit
`actions/checkout@v4`, ist beweglich. Der Befund bleibt dokumentierter,
unreparierter Workflow-Drift; kein Workflow wurde geaendert. Trigger fuer eine
Neubewertung sind Workflow-, Action- oder Governance-Aenderungen. / T098 used
the required inventory command and found exactly 23 `uses:` lines. Twenty-two
references are pinned to full 40-character commit SHAs. The existing
`actions/checkout@v4` reference in the requirements-intake workflow is moving.
This remains documented, unrepaired workflow drift; no workflow was changed.
Workflow, action, or governance changes trigger reassessment.

## Scope- und Textintegritaet / Scope and text integrity

T100 bestand: `git diff --check`, Konfliktmarker-, Fence-, relative Link- und
UTF-8-Pruefung melden null Fehler. Die geschlossene Positivliste enthaelt nur
Feature-045-Artefakte, den runner-erzeugten Feature-Zeiger, den datierten
Auditordner, Security-Index, Testvalidator/Fixtures und
`Directory.Build.props`; die Statistik folgt in T102. Der untracked
`.specify/runtime/`-Baum bleibt runner-owned und ausserhalb des Kandidaten.
Die explizite Negativpruefung ergab null Deltas in Produkt, Beispielen,
Public API/XML, Dependencies, Projekten, Workflows, Presets, Constitutions,
RL-SE-/historischen Quellen sowie Feature-016-/044-Evidence. / T100 passed all
diff, marker, fence, link, UTF-8, allowlist, and protected-path checks. The
runner-owned runtime tree remains untracked and excluded.

T101 Secret-/Path-Gate bestand mit Exitcode `0`: Gitleaks meldete keine
Secrets im aktuellen Git-Diff, `high=0`; die bestehende lokale `.claude`-
Konfiguration ist ein ausserhalb des Kandidaten liegender `medium`-Hinweis,
fuenf weitere Agentenverzeichnisse sind `low`. Die zusaetzlichen
Validator-Pfadregeln und der explizite Kandidatenscan fanden null Credentials,
Secret-Muster, private absolute Pfade, Agent-State-Inhalte, Sessiondaten, Logs
oder produktive Daten. Die C#-Zeichenfolge `/Users/` ist ausschliesslich eine
Fail-closed-Regel, kein privater Pfad. / T101 passed with exit code `0`, no
gitleaks finding in the diff and `high=0`; the existing local `.claude` notice
is outside the candidate. Additional path rules found no delivered credential,
private path, agent state, session, log, or production data.

## Finaler lokaler Gate-Blocker / Final local gate blocker

T111 ist nicht bestanden. Zwei Startversuche erreichten wegen verbotener
MSBuild-Named-Pipes weder Restore noch Test. Der danach serialisierte
Release-/Coverlet-Lauf erreichte Build und Teststart, aber der VSTest-
Datacollector konnte seinen lokalen TCP-Listener in der Sandbox nicht binden
(`SocketException (13)`) und brach vor dem ersten Test ab. Deshalb liegen kein
vollstaendiger Regressionserfolg und keine aktuellen fuenf Assembly-Coverages
vor. Der Blocker ist ausserhalb des Audit-/Test-only-Scopes; Runsettings,
Testinfrastruktur oder Sandbox werden nicht umgangen oder veraendert. T111 und
T112 bleiben offen. / T111 did not pass. The serialized Release/Coverlet run
reached build and test startup, but the VSTest data collector could not bind
its local TCP listener in the sandbox and aborted before the first test. No
full-regression or five-assembly coverage claim is made. The blocker is outside
the audit/test-only scope and is not bypassed.
