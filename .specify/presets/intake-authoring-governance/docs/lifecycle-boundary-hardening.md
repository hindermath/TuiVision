# Lifecycle-Pfadgrenzen / Lifecycle path boundaries

Release: 0.3.4. Date: 2026-09-13. Documentation Impact: UpdateRequired.
Owner: preset maintainer. Readers: preset consumers and maintainers. Canonical
sources: scripts, executable fixtures and preset.yml; DE/EN colocated here.
Navigation: README and existing validator manpages. Distribution: preset package.

DE: Verschiedene Collection-Namen muessen verschiedene physische Pfade bezeichnen
und innerhalb des Repositories bleiben. Aktive Ziele duerfen sich nicht physisch
ins Archiv aufloesen. Unbekannte Serien- und Zielzustaende werden als RIG017
abgewiesen. Lexikalisch verschachtelte Sammlungen (etwa aktive Wurzel '.' und ein
Unterarchiv) bleiben unter den bisherigen Ausschlussregeln zulaessig.

EN: Different collection names must resolve to distinct in-repository locations.
Active targets cannot physically resolve into the archive. Unknown series and
target states return RIG017. Lexically nested collections, such as active root
'.' and a nested archive, retain the existing exclusion semantics.

DE: Authoring prueft bestehende wie aufgeloeste Receipt-Dateibindungen vor dem
Lesen. Der PowerShell-Wrapper benutzt dazu den internen Python-Pfadresolver;
beide Shells pruefen Datei- und Elternverzeichnis-Symlinks. Historische Receipts
werden nicht umgeschrieben. Fremde Windows-Laufwerkspfade bleiben ungueltig.

EN: Authoring checks existing and resolved receipt file bindings before reading.
The PowerShell wrapper uses the internal Python path resolver; both wrappers
reject file and parent-directory symlink escapes. Historical receipts remain
unchanged. Foreign Windows drive paths are invalid.

Die Regression wurde vor der Korrektur reproduziert: eine aktive Collection als
Alias des Archivs ergab Aligned/Eligible; ein externes Receipt-Ziel mit identischem
Inhalt ergab Pass. Neue Fixtures erwarten RIG007 beziehungsweise Exit 2. Die
bestehenden Lifecycle-, DE/EN-, LF/CRLF- und read-only-Paritaetstests bleiben aktiv.

Before the correction, an active collection aliasing the archive returned
Aligned/Eligible and an external same-content receipt target returned Pass.
New fixtures require RIG007 or exit 2 respectively. Existing lifecycle, DE/EN,
LF/CRLF and read-only parity tests remain active.

Pruefnachweis: native Lifecycle-validation-CI fuer den exakten Release-Head auf
macOS, Linux und Windows. Die im ZIP enthaltene .github/workflows-Datei ist
Quellmetadatum fuer dieses Preset-Repository; Verbraucher fuehren ihre eigenen
Root-Workflows aus. Die verschachtelte Kopie aktiviert keinen Verbraucher-Job.

Proof: native lifecycle-validation CI on the exact release head across macOS,
Linux and Windows. The packaged .github/workflows file is source metadata for
this preset repository. Consumers use their own root workflows; the nested copy
does not register a consumer job. Reevaluate on path, lifecycle or overlay changes.

DE: Dieser Folgepatch repariert eine fehlerhafte Versionsersetzung in der Receipt-JSON-Vorlage. Die CI parst jetzt alle ausgelieferten JSON-Vorlagen und gleicht Generatorversionen gegen preset.yml ab.

EN: This follow-up repairs a faulty version substitution in the receipt JSON template. CI now parses every shipped JSON template and checks generator versions against preset.yml.

DE: Dieser Folgepatch repariert eine fehlerhafte Versionsersetzung in der Receipt-JSON-Vorlage. Die CI parst jetzt alle ausgelieferten JSON-Vorlagen und gleicht Generatorversionen gegen preset.yml ab.

EN: This follow-up repairs a faulty version substitution in the receipt JSON template. CI now parses every shipped JSON template and checks generator versions against preset.yml.
