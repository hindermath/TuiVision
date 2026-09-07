# v0.1.2: Risiko-ID- und JSON-Wurzel-Pruefung / Accepted-risk ID and JSON-root validation

## Problem

In v0.1.1 akzeptiert Bash akzeptierte Risiken ohne `id`, waehrend PowerShell
sie ablehnt. PowerShell entpackt ausserdem ein einteiliges ID-Array in der
Helper-Pipeline. Ein unabhaengiger Review zeigte zusaetzlich, dass Bash mehrere
JSON-Wurzeln als Datenstrom akzeptiert und ein nachgestelltes Teilobjekt die
fehlende ID der ersten Wurzel verdecken kann.

*In v0.1.1, the Bash validator accepts accepted-risk entries without `id`,
while PowerShell rejects them. PowerShell also unwraps a singleton ID array
through its helper pipeline. An independent candidate review additionally
showed that Bash accepts multiple JSON roots as a stream, allowing a trailing
partial object to mask the first root's missing ID.*

## Loesung / Solution

- Beide Shells verlangen kanonische, skalare und nicht leere Risiko-IDs.
- PowerShell prueft den Rohwert vor der Pipeline-Konvertierung.
- Die gemeinsame Bash-Gate-Grenze akzeptiert genau eine JSON-Wurzel.
- Gueltige Unicode-IDs erhalten keine neue Format-, Trim- oder
  Normalisierungsvorgabe.
- v0.1.0 und v0.1.1 bleiben unveraendert; v0.1.2 folgt erst nach nativen
  Plattform-Gates und fachlichem Review.

*Require canonical, scalar, nonblank accepted-risk IDs in both shells. Inspect
the raw PowerShell property before pipeline conversion. Require exactly one
complete JSON root at the shared Bash gate boundary. Preserve valid Unicode
IDs without new formatting, trimming, or normalization rules. Keep v0.1.0 and
v0.1.1 immutable and release v0.1.2 only after all material gates pass.*

## Risiko und Authority / Risk and authority

Die Aenderung bleibt auf Validatorgrenzen und Regressionsevidenz beschraenkt.
Keine neue Abhaengigkeit, Produkt-, Schema-, Baseline-, globale Helper- oder
menschliche Freigabeaenderung. Admin-Bypass gilt nur fuer formale Regeln.

*The change is limited to validator input boundaries and regression evidence.
It adds no dependency and changes no product code, schema, baseline, global
helper, or human decision. MergeAndSync is authorized; admin bypass may cover
formal merge rules only, never technical, security, accessibility, review, or
evidence failures.*

## Testplan / Test plan

- Rot-Nachweis fuer fehlende ID und mehrere JSON-Wurzeln vor der Korrektur.
- Vollstaendige Cross-Shell-Vertragssuite.
- Installationstest aller erzeugten Oberflaechen.
- Bash-Syntax- und PSScriptAnalyzer-Pruefung.
- Linux, macOS und Windows muessen am exakt geprueften PR-Head bestehen.

*The red/green evidence covers nine invalid ID representations and two
concatenated JSON roots through status and review. Valid controls, raw evidence
bytes, and all prior contracts must remain intact.*
