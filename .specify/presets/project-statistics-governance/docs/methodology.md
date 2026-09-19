# Messmethodik / Measurement methodology

## Vertrag / Contract

`project-transparency/1` ist ein eigener versionierter Vertrag, nicht eine
stillschweigende Aenderung von ASCII Statistics Profile 2. Vorhandene Profile
bleiben kanonisch, waehrend Pilotkontexte getrennt ausgewertet werden.

`project-transparency/1` is a separate versioned contract, not a silent change
to ASCII Statistics Profile 2. Existing profiles stay authoritative while
pilot contexts are evaluated separately.

## Bestand / Inventory

Quelle ist der rekursive Git-Baum der vollstaendigen Quellrevision. Textdateien
haben keine NUL-Bytes. Zeilen werden wie im bisherigen Renderer per LF,
ersatzweise CR gezaehlt, einschliesslich einer letzten nicht abgeschlossenen
Zeile. Leere Dateien und eine reine UTF-8-BOM haben null Zeilen.
CRLF und LF liefern dieselbe Zeilenzahl. Dateiinhalt kommt aus Git-Objekten,
nicht aus der Arbeitskopie. Es werden keine Git-Filter ausgefuehrt.

Inventory comes from the complete revision's recursive Git tree. A NUL byte
classifies a blob as binary. Count LF, otherwise CR, including a final
unterminated line. Empty and BOM-only files have zero lines. LF and CRLF
give equal line counts. Read Git blobs, not working files; never run filters.

Die sieben Kategorien Production, Tests, Documentation, Scripts,
Configuration, DataMedia und Other uebernehmen die Profile-2-Zuordnung.
Explizite Kategorie-Overrides gewinnen. Ausdrueckliche Ausschluesse verwenden
PowerShell-Wildcards ohne Beachtung der Gross-/Kleinschreibung.
Der aktuelle Ausgabekontext sowie das bestehende Statistik-Ledger und STATS.md
werden nicht gezaehlt. Ausschluesse muessen im Projekt begruendet werden.

The seven categories preserve Profile-2 classification. Explicit overrides
win. Exclusions use case-insensitive PowerShell wildcards. Exclude the active
output context, the existing statistics ledger and STATS.md. Project owners
must justify exclusions.

Symlinks und Submodule werden nicht verfolgt, Binaerdateien nicht als Text
gezaehlt. Git-LFS-Pointer bleiben Pointer-Text; LFS-Inhalte werden nicht geladen.
Uebernommener Code bleibt enthalten, wenn das Projekt ihn nicht ausschliesst.
Steuerzeichen in Git-Pfaden sind nicht unterstuetzt und blockieren.

Do not follow symlinks or submodules or count binary blobs as text. Git LFS
pointers remain pointer text; never fetch LFS content. Imported code remains
included unless explicitly excluded. Control characters in Git paths are
unsupported and block instead of silently truncating coverage.

Shallow- und Partial-/Promisor-Klone blockieren; es erfolgt kein automatisches
Nachladen fehlender Git-Objekte. Shallow and partial/promisor clones block;
missing Git objects are never fetched automatically.

## Aenderungen und Zeitraum / Changes and period

Bruttovolumen ist hinzugefuegte plus entfernte Zeilen aus Nicht-Merge-Commits.
Git-Rename-Erkennung ist auf 50 Prozent festgelegt. Reine Umbenennungen liefern
kein Volumen; bei Ausschluss des alten oder neuen Pfades wird der gesamte
Rename-Datensatz ausgeschlossen. Git-Binaer-Diffs liefern kein Textvolumen.
Massgeblich ist das Committer-Datum in der konfigurierten Zeitzone.

Gross volume is added plus removed lines from non-merge commits. Rename
detection is fixed at 50 percent. Pure renames add no volume; excluding either
old or new path excludes the entire rename record. Binary diffs add no text
volume. Use the committer timestamp in the configured time zone.

Das Fenster beginnt am Sonntag der Stichtagswoche minus W-1 Wochen und endet
einschliesslich Stichtag. Default W=52, Zeitzone UTC. Aktivtage sind eindeutige
Tage mit positivem Bruttovolumen im Fenster, nicht Arbeitstage oder Stunden.
Ohne expliziten Stichtag gilt das Datum des ersten relevanten Commits aus
Git log; bei fehlenden Textaenderungen das Committer-Datum der Quellrevision.
Bestand bezieht sich immer auf die Quellrevision, nicht auf eine historische
Rekonstruktion am frei gewaehlten Aktivitaets-Stichtag.

The window starts on Sunday of the cutoff week minus W-1 weeks and ends
inclusively at the cutoff. Defaults: W=52 and UTC. Active days are distinct
dates with positive gross volume, not working days or hours. Without an
explicit cutoff use the first relevant Git-log commit date, falling back to
the source revision's committer date. Inventory always belongs to the source
revision; a chosen activity cutoff does not reconstruct historical inventory.

## Nachweis und Aktualitaet / Evidence and freshness

Snapshot und generierter Abschnitt werden deterministisch als UTF-8 ohne BOM
und mit LF erzeugt. Hashes binden normalisierte Konfiguration, Messkern und
generierten Abschnitt. Die Quellrevision bindet rohe Git-Objekte.
Status wiederholt die Messung mit der gespeicherten Revision und dem
gespeicherten Stichtag und vergleicht zusaetzlich gegen die aktuellen Quellen.
Unversionierte lokale Aenderungen gehoeren nicht zur Messung; Update blockiert
bei unsauberer Arbeitskopie. Ein neuer Kalendertag allein erzeugt keine Drift.

The snapshot and generated section use deterministic UTF-8 without BOM and LF.
Hashes bind normalized configuration, measurement code and generated output;
the revision binds raw Git objects. Status replays the recorded revision and
cutoff, then separately checks the current sources. Uncommitted changes are
not measured; Update blocks a dirty worktree. Date changes alone cause no drift.

Eigene Ausgaben werden auch aus der Eingabeidentitaet ausgeschlossen.
Bei identischen Eingaben wird die bestehende Quellbindung behalten; reine
Statistik-Commits erzeugen keine selbstreferenzielle Update-Schleife.
Ein Abbruch zwischen Berichts- und Snapshot-Schreiben wird als Drift erkannt,
nicht als vollstaendige Transaktion behauptet.

Exclude own outputs from input identity. Retain the source binding for
identical inputs; output-only commits cannot cause an update loop. A failure
between report and snapshot writes is detected as drift, not claimed to be
an atomic transaction.

## Modellrechnungen und Grenzen / Estimates and limitations

Optionale Referenz: Textbestand / (Aktivtage * Referenzzeilen pro Tag).
Keine Daten oder Nenner null: nicht berechenbar. Referenzwerte sind frei
gewaehlte Annahmen, keine Benchmarks oder gemessene menschliche Leistung.
Das Modell vermischt Gesamtbestand und Zeitraum-Aktivitaet und ist deshalb
ausdruecklich kein kausaler Vergleich und keine KI-Zeitersparnis.
Standardmaessig werden keine Referenzszenarien angezeigt.

Optional reference: inventory / (active days * reference lines per day).
Missing data or zero denominator: not calculable. References are chosen
assumptions, not benchmarks or measured human performance. The model combines
total inventory and windowed activity: it is not causal and does not measure
AI time savings. Reference scenarios are hidden by default.

Phasenwerte sind manuelle Angaben, keine automatisch verifizierten Abschluesse.
Keine Personenranglisten, Noten, Sicherheitsfreigaben oder Zertifizierungen.

Phase values are manual declarations, not automatically verified completions.
No rankings of people, grades, security approvals or certifications.
