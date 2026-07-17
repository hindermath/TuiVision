# Research: Wave-5 Combined Delta Closure

## Entscheidung 1: PR-Dateimengen statt Basis-bis-main-Diff

**Entscheidung**: Der Produktdelta ist die Vereinigung der reviewten
Dateimengen von PR #93 und PR #96. PR #94 und #97 sind Closeout-Evidence;
PR #95 ist Prompt-Metadatenarbeit.

**Begründung**: Ein breiter Basis-bis-`main`-Diff würde kausale Abschlüsse und
spätere Metadaten fälschlich als Produktänderungen zählen.

**Alternativen**:

- Gesamtdiff ab Feature-032-Basis: verworfen wegen fremder Metadaten.
- Nur aktuelle Pfade prüfen: verworfen, weil die reviewte Herkunft verloren
  ginge.

## Entscheidung 2: Geschlossener JSON-Datensatz plus lesbarer Bericht

**Entscheidung**: `wave5-combined-delta.json` ist die maschinenprüfbare
Wahrheitsquelle; `wave5-closure.md` und `pr-evidence.md` erklären Ergebnis und
Grenzen für Menschen.

**Begründung**: JSON ermöglicht exakte Sets, Referenzen und Mutationsprüfungen.
Markdown hält die Entscheidung text-first und reviewbar.

**Alternativen**:

- Nur Markdown-Tabellen: verworfen, weil doppelte oder fehlende Beziehungen
  schwer robust zu prüfen sind.
- Daten in C# hardcodieren: verworfen, weil der Abschluss dann keine
  eigenständige Evidence-Oberfläche hätte.

## Entscheidung 3: Validator im bestehenden Example-Smoke-Projekt

**Entscheidung**: `Wave5CombinedDeltaClosureTests.cs` wird unter
`tests/TuiVision.Examples.SmokeTests` ergänzt.

**Begründung**: Dort liegen bereits Funktions- und Showcase-Proofs sowie die
benötigten Beispielreferenzen. Kein neues Projekt oder Paket ist nötig.

**Alternativen**:

- Standalone-Skript: verworfen wegen zusätzlicher Bash-/PowerShell-Parität.
- Produktionsbibliothek: verworfen, weil Auditlogik kein Runtime-Vertrag ist.

## Entscheidung 4: Komposition statt Kopie bestehender Tests

**Entscheidung**: Der neue Validator prüft die neuen Beziehungen und läuft
zusammen mit den vorhandenen Feature-032-/033-Smokes. Er kopiert deren
vollständige App-Loop-Logik nicht.

**Begründung**: So bleibt die Ownership klar: bestehende Tests beweisen die
Einzelschicht, Feature 034 beweist Vollständigkeit und Konsistenz zwischen den
Schichten.

**Alternativen**:

- Alle App-Flows erneut im Closure-Test ausführen: verworfen als Duplikation.
- Nur auf Vorgängerzusammenfassungen vertrauen: verworfen, weil die
  Kreuzzuordnung ungeprüft bliebe.

## Entscheidung 5: In-Memory-Mutationen für Negativtests

**Entscheidung**: Negativtests mutieren geparste JSON-Daten oder eine
test-eigene temporäre Kopie.

**Begründung**: Das liefert isolierte Failure-Proofs ohne viele getrackte
Fehler-Fixtures und erlaubt denselben Test für LF und CRLF.

**Alternativen**:

- Eine Datei pro Fehler: verworfen wegen Wartungs- und Reviewrauschen.
- String-Ersetzungen: verworfen, weil strukturierte APIs vorhanden sind.

## Entscheidung 6: Source-Provenienz über Git-Blobs

**Entscheidung**: Die 15 Pascal-Quellen werden durch Pfad und Git-Blob am
Feature-032-Merge sowie am aktuellen Audit-Head gebunden.

**Begründung**: Feature 033 hat die stabilen Blobs bereits ermittelt. Git-Blobs
beweisen Bytegleichheit unabhängig vom Zeilenende des Evidence-Dokuments.

**Alternativen**:

- Nur Pfade und Anzahl: verworfen, weil Inhalt driftet könnte.
- Quellen in das Feature kopieren: verworfen; `TVDEMOS/` bleibt read-only.

## Entscheidung 7: Framework-Ownership beobachtbar entscheiden

**Entscheidung**: `Wave5Application`, `Wave5ConsoleHost`,
`Wave5StatusLine`, `Wave5GridView` und Zustandsmodelle werden anhand ihrer
Verantwortung bewertet, nicht anhand des Speicherorts.

**Begründung**: Didaktische Beispielkomposition darf lokal bleiben.
Dupliziert sie aber Framework-Verhalten oder wäre sie für unabhängige Wellen
wiederverwendbar, entsteht ein Candidate Finding.

**Alternativen**:

- Alles unter `examples/Shared` automatisch akzeptieren: verworfen.
- Jede gemeinsame Helper-Klasse ins Framework verschieben: verworfen als
  pauschale Revision.

## Entscheidung 8: Externe Zweitmeinungen nur problembezogen

**Entscheidung**: Free Vision, Terminal.GUI und magiblot/tvision werden nur
bei einer neuen reproduzierbaren Wave-5-Frage konsultiert.

**Begründung**: Die Quellen wurden bereits tief auditiert. Feature 034 prüft
den gelieferten Wave-5-Delta, nicht erneut das gesamte Framework.

**Alternativen**:

- Vollständige Re-Audits: verworfen wegen Scope-Ausweitung.
- Nie erneut konsultieren: verworfen, weil eine konkrete neue Frage von einer
  unabhängigen Zweitmeinung profitieren kann.

## Entscheidung 9: Dualer kausaler Wave-Zustand

**Entscheidung**: Ohne vollständigen `delivery-closeout.md` bleiben Wave 5 und
Wave 6 `BlockedPendingCausalClosure`. Mit exaktem reviewtem Head, grünen Gates
und Feature-Merge sind nur `Closed` und `EligibleForIntake` zulässig.

**Begründung**: Der Feature-Head kann seinen zukünftigen Merge nicht
wahrheitsgemäß behaupten. Die ausführbare Regel muss dennoch vor dem Merge
reviewt sein.

**Alternativen**:

- Wave 5 im Feature-PR schließen: verworfen als zeitlich falsch.
- Tests im Closeout ändern: verworfen, weil der Closeout evidence-only bleibt.

## Entscheidung 10: Full Gates trotz read-only Produktumfang

**Entscheidung**: Targeted Tests, TP7-Smokes, Full Release, Coverage, Format,
DocFX/Axe, Security, Agent-Parität, drei Plattformen und Exact Head bleiben
verbindlich.

**Begründung**: Feature 034 ist die formelle Wave-Freigabe. Test-only
Closure-Code kann veraltete Proofs oder Plattformabweichungen sichtbar machen.

**Alternativen**:

- Nur Closure-Validator: verworfen, weil er Produktregressionen nicht selbst
  ausführt.

## Entscheidung 11: Keine Preset-Promotion ohne portablen Defekt

**Entscheidung**: Die Retrospektive endet standardmäßig mit `NoPromotion`.
Eine Promotion benötigt einen reproduzierbaren providerneutralen Defekt.

**Begründung**: Feature-spezifische Daten oder Reviewer-Quota sind keine
allgemeine Preset-Lücke.

**Alternativen**:

- Jede Optimierung promoten: verworfen wegen Überanpassung und Churn.
