# Abnahmevertrag: Wave-5 Combined Delta Closure

## 1. Eingabevertrag / Input Contract

1. PR #93 und #96 sind die einzigen autoritativen Wave-5-Produktlieferungen.
2. PR #94 und #97 sind kausale Closeouts; PR #95 ist Prompt-Metadatenarbeit.
3. Basis-, Head-, Merge- und Dateimengen werden vor der Abschlussaussage
   exakt verifiziert.
4. Bindende Vorgängerdateien werden durch Pfad und SHA-256 gebunden.
5. `TVDEMOS/`, `TVFM/`, `tv203s/` und externe Quellen bleiben read-only.

## 2. Cardinality-Vertrag / Cardinality Contract

- exakt 15 HistoricalSourceRole-Zeilen;
- exakt sechs ConsumerGroup-Zeilen `W5-001` bis `W5-006`;
- exakt zehn FunctionalProof-Zeilen;
- exakt zehn ShowcaseClosure-Zeilen;
- exakt zehn GuideLaunchPath-Zeilen;
- exakt zehn CombinedExampleRow-Zeilen;
- genau eine Hauptentscheidung je Beispiel;
- keine fehlenden, doppelten, unbekannten oder verwaisten IDs.

LF und CRLF müssen dasselbe Ergebnis liefern.

## 3. Kombinierter Beispielvertrag / Combined Example Contract

Jede der zehn Zeilen verbindet:

1. historische Quelle und Consumer;
2. Feature-032-Funktionsproof;
3. Feature-033-Showcase-Proof;
4. normalen Einstieg und ersten sichtbaren Zustand;
5. primäre Tastaturbedienung;
6. Fokus, reale StatusLine und F1/Description;
7. kontrolliertes `Ctrl+Q`;
8. Framework-Komponenten und lokale Sonderlogik;
9. App-Loop-, View-, Buffer-/Cell- und constrained-layout Proof;
10. Guide, Plattform- und Safety-Grenze.

## 4. Entscheidungsvertrag / Decision Contract

Hauptentscheidungen sind ausschließlich:

- `AcceptedAsIs`
- `AcceptedIntentionalDeviation`
- `CandidateFinding`
- `ProductDecision`

Dimensionswerte sind ausschließlich `Pass`, `IntentionalDeviation`, `Gap`
oder `N/A`.

- Eine akzeptierte Zeile enthält keinen `Gap`.
- `AcceptedIntentionalDeviation` erklärt historische Absicht, moderne
  C#-Begründung, sichtbare Auswirkung, Risiko und Trigger.
- `CandidateFinding` besitzt `W5D###`, Reproduktion, Evidence, Owner und
  Follow-up-Grenze.
- `ProductDecision` stoppt den Lauf ohne Remediation.
- Quelltextstil allein erzeugt kein Finding.

## 5. Framework-Vertrag / Framework Contract

`Wave5Application`, `Wave5ConsoleHost`, `Wave5StatusLine`,
`Wave5GridView` und lokale Zustandsmodelle dürfen didaktische
Beispielkomposition enthalten. Ersetzen sie Framework-Verhalten oder wären
sie für unabhängige Beispielwellen wiederverwendbar, ist ein Candidate Finding
erforderlich. Feature 034 verschiebt oder behebt diese Logik nicht.

## 6. Proof-Vertrag / Proof Contract

Primary-Proofs führen `app.Run()` oder einen gleichwertigen realen
Anwendungsloop aus und verbinden Zustand, konkrete View-Identität, Fokus,
Status, Description und sichtbare Cells. Direkte Helfer sind nur
`SupplementalProof` oder `SetupOnly`.

Alle zehn Beispiele bestehen:

- kontrollierten `--smoke`-Start;
- normalen PTY-Start;
- mindestens eine primäre Aktion;
- F1/Description;
- kontrolliertes `Ctrl+Q`.

## 7. Sicherheits- und A11Y-Vertrag / Safety and A11Y Contract

- Datei-, Resource- und Help-Pfade bleiben kontrolliert und fail-closed.
- Keine versteckte Host-, Locale-, Zeit- oder Netzwerkabhängigkeit ist
  zulässig.
- Maus bleibt optional; Tastaturpfade sind vollständig.
- Fokus, Status und Description bleiben text-first nachweisbar.
- Learner-facing Dokumentation ist German-first/English-second, CEFR-B2,
  semantisch und WCAG-2.2-AA-orientiert.

## 8. Validierungsvertrag / Validation Contract

- Positive und negative Closure-Tests bestehen.
- Relevante TP7-Funktions- und Showcase-Smokes bestehen.
- Full Release und das kanonische Fünf-Assembly-Coverage-Gate bestehen.
- Format, DocFX, Playwright/Axe, UTF-8, Secrets, Supply Chain, Scope und
  Agent-Parität bestehen.
- Ubuntu, macOS und Windows führen den tatsächlichen Release-Scope am
  reviewten Head aus.
- Exact-Head-Evidence ordnet jedes Gate zu Workflow, Job, Plattform,
  Command, Head und Ergebnis zu.
- Fehlende Reviews bleiben fehlend; null umsetzbare Threads bleiben offen.

## 9. Wave-Vertrag / Wave Contract

1. Der reviewte Feature-Head hält beide Waves
   `BlockedPendingCausalClosure`.
2. Vollständiger Feature-Head-Erfolg ergibt `ReadyForMerge`, nicht vorzeitige
   Wave-Freigabe.
3. Nach Feature-Merge setzt ein Evidence-only Closeout Wave 5 auf `Closed`
   und Wave 6 auf `EligibleForIntake`.
4. Der Closeout darf Lastenheft 20 und Feature 035 reservieren, startet aber
   weder Branch noch Implementierung.
5. Findings erzeugen nur nicht leere, deduplizierte ownerbezogene Intakes.
6. Eine Product Decision verhindert Wave-6-Freigabe.
7. Der Closeout ändert keine Test- oder Produktlogik.

## 10. Liefervertrag / Delivery Contract

- Delivery-Modus ist `MergeAndSync`.
- Ein enger Bypass betrifft ausschließlich Human Approval, nachdem alle
  technischen Gates grün und alle umsetzbaren Threads geschlossen sind.
- Feature-PR und ein gegebenenfalls notwendiger Closeout-PR sind nicht leer.
- Der Lauf endet mit vollständigen Tasks, `Retrospective`, `Completed`,
  `nextExactAction: N/A` und sauberem `main == origin/main`.
