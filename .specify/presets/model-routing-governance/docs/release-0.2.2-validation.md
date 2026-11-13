# v0.2.2: Strukturierter Harness-Rückkanal / Structured harness result transport

DE: Claude, Copilot und OpenCode liefern ihre Abschlussantwort auf der
Standardausgabe. Der autonome Runner erwartet dagegen eine separate JSON-Datei.
Ein paketinterner Adapter erfasst jetzt ausschließlich erfolgreiche JSON-
Objektausgabe, schreibt sie atomar an `{outputFile}` und gibt sie zusätzlich für
das normale Phasenprotokoll zurück. Fehlerhafte, leere oder nicht als JSON-Objekt
lesbare Ausgabe erzeugt keine Ergebnisdatei. Das funktioniert auch für lesende
Rollen, da der Harness selbst keine Datei schreiben muss.

EN: Claude, Copilot and OpenCode return their final answer on standard output,
while the autonomous runner requires a separate JSON result file. A package
adapter now captures successful JSON-object-only output, writes it atomically to
`{outputFile}`, and also returns it for the ordinary phase log. Failed, empty or
non-object JSON output creates no result file. Read-only roles work because the
harness itself does not write a file.

Codex and `agy` profiles and their runner arguments remain unchanged. The new
adapter is used only by Claude, Copilot and OpenCode profiles. Their prompts
request the autonomous phase-result template as the only standard-output
content. The downstream runner retains its existing semantic result validator.

Documentation Impact **UpdateRequired**, owner Thorsten Hindermann. Canonical
sources are the adapter, its profile generator and transport fixture. Reader
path: README → this record → automatic discovery guides. German first,
equivalent English, CEFR B2 and applicable WCAG 2.2 AA. Distribution is the
preset package; named consumers update only after a published, hash-verified
release. Re-evaluate when harness output formats or runner result contracts
change. No account access, SDK installation, inference or personal-profile
refresh is part of validation.

This file prepares v0.2.2; it is not publication evidence. The release page,
exact tag commit and independently verified archive establish publication.
[Structured preparation](release-0.2.2-preparation.json).
