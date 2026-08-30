# GSDB Feature 046 – validation-evidence.md

## Deutsch

Dies ist eine zeitpunktbezogene Review-Evidence, keine Zertifizierung, Rechtsberatung oder formale Freigabe. Disposition (Klartextstatus / plain-text status) wird immer ausgeschrieben. Deutsch steht zuerst.

- Snapshot: `fc041d61ab71288cf0c882ecd00a5e019c64405b`
- Kontrollen: 157
- Quellen: 37
- Positive Evidence-Lücken: 0

### Ausführungsentscheidungen

| Entscheidung | Status | Begründung | Trigger |
|---|---|---|---|
| `ArchitectureAdrThreatModelChange` | N/A | Review-Evidence ändert keine Architektur, ADR oder Trust Boundary; vorhandene Architektur- und Threat-Model-Evidence bleibt Review-Input. | Ein Architektur-, Deployment-, Produkt- oder Trust-Boundary-Pfad tritt in den Diff ein. |
| `BsiC3aC5Hardening` | N/A | Das Repository liefert keinen Cloud-Service oder Cloud-Auditgegenstand; Feature 046 härtet BSI C3A/C5 nicht. | Cloud-Service-, Hosting- oder Audit-Scope tritt ein. |
| `NewScriptManpageCmdletPowerShellHelp` | N/A | Es entsteht kein Script, keine Manpage, kein Cmdlet und keine PowerShell-Hilfe; der Validator bleibt im bestehenden Testfile. | Eine neue oder geänderte Shell-, PowerShell-, Manpage- oder Cmdlet-Fläche tritt ein. |
| `PublicApiXmlDocumentation` | N/A | Kein Produktpfad, keine öffentliche API und keine XML-Dokumentation werden geändert. | Ein src-, API- oder XML-Dokumentationspfad tritt ein. |
| `HistoricalOrModernSourceChange` | N/A | Historische und externe Vergleichswurzeln bleiben read-only; keine konkrete GSDB-Frage erforderte Einsicht. | Eine konkrete protokollierte GSDB-Frage oder eine Quellenänderung tritt ein. |
| `RuntimeProductAi` | N/A | KI bleibt Entwicklungswerkzeug; keine Runtime-KI, Modelle, Datensätze oder ausgelieferte KI-Komponente treten ein. | Runtime-/Produkt-KI oder ausgelieferte AI-Infrastruktur tritt ein. |
| `AgentContextSync` | N/A | Feature 046 ändert keine gemeinsame Regel; die vorhandenen Agentenflächen werden bewertet und erhalten NoUpdateRequired. | Eine neue projektweite Regel, Technologie oder Agentenpflicht tritt ein. |
| `ParallelExecution` | N/A | Alle Shared Writer und Aufgaben bleiben serialisiert. | Eine parallele Kampagne oder ein paralleler Writer wird vorgeschlagen. |
| `FollowUpArtifactCreation` | N/A | Feature 046 dokumentiert Open und FollowUp, erzeugt aber kein Intake, Issue, Branch, Feature oder Finding-Remediation. | Explizite spätere Autorität für ein getrenntes Folgeartefakt liegt vor. |

### Manueller Text-First-Review

```json
{
  "status": "Pass",
  "maximumSeconds": 180,
  "selectedControlId": "CL-01-12",
  "startedAtUtc": "2026-08-30T14:57:19Z",
  "endedAtUtc": "2026-08-30T14:57:19Z",
  "elapsedSeconds": 0,
  "requiredHops": [
    "source",
    "disposition",
    "evidence",
    "owner",
    "risk",
    "followUp",
    "revalidationTrigger"
  ],
  "observedHops": [
    "source",
    "disposition",
    "evidence",
    "owner",
    "risk",
    "followUp",
    "revalidationTrigger"
  ],
  "trace": {
    "source": {
      "sourceId": "SRC-7e3c2ef563e4",
      "path": "docs/secure-development/checklisten/CL_01_Standards-Anwendbarkeit.md",
      "sha256": "b0b3b15c1ad7d46a8f83f563583d9538d72332787f4468d63e691ec37fc4decf",
      "hashMode": "Utf8LfNormalized"
    },
    "disposition": "Open",
    "evidence": {
      "referenceIds": [],
      "gapDe": "Eine vollst\u00E4ndige positive Einzel-Evidence liegt nicht vor.",
      "gapEn": "Complete positive item-level evidence is not present."
    },
    "owner": "Maintainer",
    "risk": "Die Aussage bleibt begrenzt und darf nicht als Freigabe gelesen werden. / The claim remains bounded and must not be read as approval.",
    "followUp": "Documented follow-up only; Feature 046 creates no intake, issue, branch, feature, or remediation.",
    "revalidationTrigger": "Evidence, scope, governance, provider state, or repository snapshot changes."
  },
  "markdownFilesReviewed": 9,
  "readerRoutesReviewed": 1,
  "wcagBaseline": "WCAG 2.2 AA",
  "assistiveModes": [
    "Screen reader",
    "Braille display",
    "Text browser"
  ],
  "textBrowser": "lynx with explicit UTF-8 input and display charset",
  "brokenLocalLinks": 0,
  "unexplainedCentralTerms": 0,
  "visuallyExclusiveMeanings": 0
}
```

### Aktuelle Paket-Evidence

| Prüfung | Ergebnis | Grenze |
|---|---|---|
| Verwundbar | Exit `0`; `46/46` Projekte ohne bekannte verwundbare direkte oder transitive Pakete | Zeitpunktbezogene Provider-Evidence, keine Zertifizierung |
| Veraltet / deprecated | Exit `0`; `46/46` Projekte ohne veraltete direkte oder transitive Pakete | Paketquellparameter und Zugangsdaten sind ausgeschlossen |
| Aktualisierbar / outdated | Exit `0`; sechs Testprojekte melden nur `MSTest 4.3.2 -> 4.3.3`, 40 Projekte ohne Update | Keine Paketmutation; begrenzter Follow-up-Hinweis unter `CL-08-11` |

Die Abfragen liefen am `2026-08-30`. Ein Provider-, Paketgraph- oder
Repository-Snapshot-Wechsel löst die Wiederbewertung aus.

### Workflow-, Publishability- und Paritätsprüfung

| Prüfung | Ergebnis | Grenze |
|---|---|---|
| Workflow-Actions | 24 Referenzen in zwölf Dateien; 23 vollständige Commit-SHAs, ein beweglicher Tag | Ehrlicher offener Supply-Chain-Hinweis; keine Workflow-Abhilfe im Feature 046 |
| Supply-Chain-Automation | Fünf Workflowdateien mit Secret-, Paket- oder SBOM-Prüfung | Lokale Repository-Evidence, keine Provider-Assurance |
| Secret-Scanner | Exit `0`; `high=0`, `gitleaks_high=0` | Keine Trefferinhalte oder Zugangsdaten gespeichert |
| Delivery-Metadaten | Private Pfade `0`; Paketquellparameter `0`; Session-/Logpfade `0`; private/credentialartige Dateinamen `0` | Runner-Runtime ist kein Delivery-Set |
| Preset-/Agenten-Parität | Zwei gezielte Tests bestanden `2/2`; `NoUpdateRequired` | Keine gemeinsame Regel geändert |
| Verbotene Triggerpfade | Produkt/API/XML/Script/Historie/Workflow/Projekt/Beispiel/Produkt-AI/Cloud jeweils `0` | Jeder spätere Treffer stoppt fail-closed |
| DocFX-Trigger | 31 geänderte DocFX-Eingaben | DocFX, axe und Textbrowser bleiben anwendbar |

### Generierte Dokumentation und A11Y

| Pfad | Ergebnis | Grenze |
|---|---|---|
| `docfx docfx.json` | Exit `0`; DocFX 2.78.5; 0 Warnungen, 0 Fehler | Worktree-Snapshot auf Basis-HEAD `fc041d61ab71288cf0c882ecd00a5e019c64405b` |
| `npm install` | Exit `0`; sieben Pakete geprüft; 0 Vulnerabilities | Node 26.7.0 außerhalb der deklarierten LTS-Engine; Warnung, keine Paketmutation |
| Playwright-Installation | Exit `0` | Test-owned Chromium-Binary |
| `npm run test:docfx` | Exit `0`; DocFX 0/0; Playwright/axe 2/2 bestanden | Lokaler generierter `_site/`-Snapshot |
| UTF-8-Lynx | Exit `0`; 59 Zeilen; Deutsch Zeile 15 vor Englisch Zeile 36 | Text-first Reader-Route, kein visueller Exklusivbeleg |

## English

This is point-in-time review evidence, not certification, legal advice, or formal approval. Disposition (Klartextstatus / plain-text status) is always written in words. English follows German.

- Snapshot: `fc041d61ab71288cf0c882ecd00a5e019c64405b`
- Controls: 157
- Sources: 37
- Positive evidence gaps: 0

### Execution decisions

| Decision | Status | Rationale | Trigger |
|---|---|---|---|
| `ArchitectureAdrThreatModelChange` | N/A | Review evidence changes no architecture, ADR, or trust boundary; existing architecture and threat-model evidence remains review input. | An architecture, deployment, product, or trust-boundary path enters the diff. |
| `BsiC3aC5Hardening` | N/A | The repository delivers no cloud service or cloud audit subject; Feature 046 performs no BSI C3A/C5 hardening. | Cloud service, hosting, or audit scope enters. |
| `NewScriptManpageCmdletPowerShellHelp` | N/A | No script, man page, cmdlet, or PowerShell help is created; the validator remains in the existing test file. | A new or changed shell, PowerShell, man-page, or cmdlet surface enters. |
| `PublicApiXmlDocumentation` | N/A | No product path, public API, or XML documentation is changed. | A src, API, or XML-documentation path enters. |
| `HistoricalOrModernSourceChange` | N/A | Historical and external comparison roots remain read-only; no concrete GSDB question required consultation. | A concrete logged GSDB question or source change enters. |
| `RuntimeProductAi` | N/A | AI remains a development tool; no runtime AI, models, datasets, or delivered AI component enters. | Runtime/product AI or delivered AI infrastructure enters. |
| `AgentContextSync` | N/A | Feature 046 changes no shared rule; current agent surfaces are assessed and receive NoUpdateRequired. | A new project-wide rule, technology, or agent obligation enters. |
| `ParallelExecution` | N/A | All shared writers and tasks remain serialized. | A parallel campaign or writer is proposed. |
| `FollowUpArtifactCreation` | N/A | Feature 046 records Open and FollowUp but creates no intake, issue, branch, feature, or finding remediation. | Explicit later authority for a separate follow-up artifact exists. |

### Manual text-first review

```json
{
  "status": "Pass",
  "maximumSeconds": 180,
  "selectedControlId": "CL-01-12",
  "startedAtUtc": "2026-08-30T14:57:19Z",
  "endedAtUtc": "2026-08-30T14:57:19Z",
  "elapsedSeconds": 0,
  "requiredHops": [
    "source",
    "disposition",
    "evidence",
    "owner",
    "risk",
    "followUp",
    "revalidationTrigger"
  ],
  "observedHops": [
    "source",
    "disposition",
    "evidence",
    "owner",
    "risk",
    "followUp",
    "revalidationTrigger"
  ],
  "trace": {
    "source": {
      "sourceId": "SRC-7e3c2ef563e4",
      "path": "docs/secure-development/checklisten/CL_01_Standards-Anwendbarkeit.md",
      "sha256": "b0b3b15c1ad7d46a8f83f563583d9538d72332787f4468d63e691ec37fc4decf",
      "hashMode": "Utf8LfNormalized"
    },
    "disposition": "Open",
    "evidence": {
      "referenceIds": [],
      "gapDe": "Eine vollst\u00E4ndige positive Einzel-Evidence liegt nicht vor.",
      "gapEn": "Complete positive item-level evidence is not present."
    },
    "owner": "Maintainer",
    "risk": "Die Aussage bleibt begrenzt und darf nicht als Freigabe gelesen werden. / The claim remains bounded and must not be read as approval.",
    "followUp": "Documented follow-up only; Feature 046 creates no intake, issue, branch, feature, or remediation.",
    "revalidationTrigger": "Evidence, scope, governance, provider state, or repository snapshot changes."
  },
  "markdownFilesReviewed": 9,
  "readerRoutesReviewed": 1,
  "wcagBaseline": "WCAG 2.2 AA",
  "assistiveModes": [
    "Screen reader",
    "Braille display",
    "Text browser"
  ],
  "textBrowser": "lynx with explicit UTF-8 input and display charset",
  "brokenLocalLinks": 0,
  "unexplainedCentralTerms": 0,
  "visuallyExclusiveMeanings": 0
}
```

### Current package evidence

| Check | Result | Boundary |
|---|---|---|
| Vulnerable | Exit `0`; `46/46` projects report no known vulnerable direct or transitive package | Point-in-time provider evidence, not certification |
| Deprecated | Exit `0`; `46/46` projects report no deprecated direct or transitive package | Package-source parameters and credentials are excluded |
| Outdated | Exit `0`; six test projects report only `MSTest 4.3.2 -> 4.3.3`, 40 projects report no update | No package mutation; bounded follow-up observation under `CL-08-11` |

The queries ran on `2026-08-30`. A provider, package-graph, or repository
snapshot change triggers re-evaluation.

### Workflow, publishability, and parity review

| Check | Result | Boundary |
|---|---|---|
| Workflow actions | 24 references in twelve files; 23 full commit SHAs and one mutable tag | Truthful open supply-chain observation; no workflow remediation in Feature 046 |
| Supply-chain automation | Five workflow files provide secret, package, or SBOM checks | Local repository evidence, not provider assurance |
| Secret scanner | Exit `0`; `high=0`, `gitleaks_high=0` | No finding content or credential persisted |
| Delivery metadata | Private paths `0`; package-source parameters `0`; session/log paths `0`; private/credential-like filenames `0` | Runner runtime is outside the delivery set |
| Preset and agent parity | Two targeted tests passed `2/2`; `NoUpdateRequired` | No shared rule changed |
| Prohibited trigger paths | Product/API/XML/script/history/workflow/project/example/product-AI/cloud all `0` | Any later hit fails closed |
| DocFX trigger | 31 changed DocFX inputs | DocFX, axe, and text-browser gates remain applicable |

### Generated documentation and accessibility

| Path | Result | Boundary |
|---|---|---|
| `docfx docfx.json` | Exit `0`; DocFX 2.78.5; 0 warnings, 0 errors | Worktree snapshot based on HEAD `fc041d61ab71288cf0c882ecd00a5e019c64405b` |
| `npm install` | Exit `0`; seven packages audited; 0 vulnerabilities | Node 26.7.0 outside declared LTS engine; warning, no package mutation |
| Playwright install | Exit `0` | Test-owned Chromium binary |
| `npm run test:docfx` | Exit `0`; DocFX 0/0; Playwright/axe 2/2 passed | Local generated `_site/` snapshot |
| UTF-8 Lynx | Exit `0`; 59 lines; German line 15 before English line 36 | Text-first reader route, no visual-only proof |
