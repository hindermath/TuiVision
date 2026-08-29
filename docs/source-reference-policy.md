# Quellenreferenz-Policy / Source Reference Policy

<!-- source-reference-policy:begin -->
## Drei Achsen und fünf Rollen / Three axes and five roles

1. Akzeptierte TuiVision-Anforderungen, Spezifikationen, öffentliche Verträge
   und Tests sind die Norm für aktuelle Produktsemantik.
2. `magiblot/tvision` wird am freigegebenen Commit
   `57b6f56b38e0ee75240a80a10ee0e11470c24693` und Tree
   `96dd03873955689ff0a79f6c8107a8148fe1ebd6` zuerst als moderne
   Designreferenz geprüft. Es ist keine semantische Norm.
3. Borland-Dokumentation und `tv203s/` erklären historische Absicht,
   ursprüngliches Verhalten und Kompatibilitätsgrenzen.
4. Free Vision und Terminal.GUI sind unabhängige Vergleichsmeinungen.
5. `TVDEMOS/`, `TVFM/` und TuiVision-Beispiele sind Consumer-Evidence.

*Accepted TuiVision contracts define current product semantics. The pinned
Magiblot revision is reviewed first for modern design ideas but is not
normative. Borland and `tv203s/` define historical intent. Free Vision and
Terminal.GUI remain independent comparisons, while demos and examples provide
consumer evidence.*

## Verbindlicher Workflow / Required workflow

Bei materiell historisch berührten Änderungen gilt: aktuellen TuiVision-
Vertrag lesen; relevante Magiblot-Dateien am Pin prüfen; passende `tv203s`-
Implementierungen und erforderliche Header prüfen; Consumer und unabhängige
Implementierungen nur bei materieller Relevanz hinzuziehen; danach genau eine
Entscheidung dokumentieren: `AdoptModernization`,
`PreserveHistoricalIntent`, `IntentionalTuiVisionDeviation` oder `N/A`.

*For materially history-related changes, read the current TuiVision contract,
review relevant Magiblot files at the approved pin, review matching `tv203s`
implementations and required headers, consult consumers or independent
implementations when material, and record exactly one named disposition.*

## Konflikte, Provenienz und Gültigkeit / Conflicts, provenance, and validity

Quellenrang allein löst keinen Konflikt. Ein bestehender TuiVision-Vertrag gilt
bis zu seiner ausdrücklich genehmigten Änderung. Magiblot darf die moderne
Implementierungsform inspirieren, erzwingt aber keine C++-Vererbung, kein
Speicherlayout und keine Quelltextform. Materielle historische Abweichungen
werden sichtbar begründet.

Externe Checkouts bleiben außerhalb des verfolgten Repositorys. Gespeichert
werden nur Pin, Tree, geprüfte Pfade, Hashes, eigene Kurzfassungen und
Permalinks. Kopie, Übersetzung oder Vendorisierung externer Quellen ist nicht
Teil dieser Policy. Der Lizenzkontext ist `MultipartNotRepositoryWideMIT` und
wird nicht pauschal als repositoryweites MIT dargestellt.

Die Policy gilt `Prospective`. Abgeschlossene Evidence wird nicht rückwirkend
geöffnet. Re-Evaluation erfolgt nur bei `ChangedTuiVisionProductContract`,
`NewApprovedMagiblotPin` oder `MateriallyNewConsumerEvidence`. Ein neuer Pin
benötigt einen getrennten read-only Provenienz- und Delta-Review; bewegliche
Branches sind keine Evidence.

*Source rank alone never resolves a conflict. External checkouts stay outside
the tracked repository, source copying is excluded, and the multipart license
context is not simplified to repository-wide MIT. The policy is prospective;
only a changed product contract, a newly approved pin, or materially new
consumer evidence triggers re-evaluation.*
<!-- source-reference-policy:end -->

## Maschinenlesbare Norm / Machine-readable contract

`requirements/source-reference-policy.json` ist die kanonische
maschinenlesbare Fassung. Die Bash- und PowerShell-Einstiege unter `scripts/`
prüfen diese Datei und alle verpflichtenden Governance-Flächen fail-closed.

*The JSON policy is the canonical machine-readable form. The Bash and
PowerShell entry points validate it and every required governance surface
fail-closed.*
