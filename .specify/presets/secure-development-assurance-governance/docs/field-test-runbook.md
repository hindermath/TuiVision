# Feldtest-Runbook / Field-Test Runbook

## Ziel / Goal

Dieses Runbook erprobt ein unveränderliches Release-ZIP von Secure Development
Assurance Governance in einem kontrollierten Spec-Kit-Projekt. Der Feldtest
bereitet eine mögliche spätere Community-Einreichung vor, löst sie aber nicht
aus.

*This runbook evaluates an immutable release archive in a controlled Spec Kit
project. It prepares evidence for a possible later Community submission but
does not create one.*

## Schutzgrenzen / Safety Boundaries

- Keine produktionsnahe Freigabe allein aufgrund dieses Tests.
- Keine echten Secrets, Kundendaten oder unnötigen personenbezogenen Daten.
- Keine automatische Änderung von Richtlinie, Checklisten oder Baseline.
- Keine Ableitung menschlicher Freigaben aus technischen Ergebnissen.
- Keine Aussage zu C5-Konformität, Testatreife oder Zertifizierung.
- Keine Community-Einreichung ohne getrennten ausdrücklichen Auftrag.

## Voraussetzungen / Preconditions

1. Spec Kit `>=0.8.3` ist installiert.
2. `security-governance >=0.6.1` ist im Testprojekt aktiv.
3. Das veröffentlichte Tag-ZIP und sein dokumentierter SHA-256 sind bekannt.
4. Das Testprojekt besitzt ein gültiges Secure-Development-Manifest.
5. Bash, PowerShell 7 und `jq` sind für den Paritätstest verfügbar.
6. Test-Owner und technischer Reviewer sind benannt.

## Phase 1: Paket und Installation

1. Release-ZIP herunterladen.
2. SHA-256 mit der Release-Evidence vergleichen.
3. Preset mit Priorität `15` installieren.
4. `specify preset info`, `specify preset resolve` und `specify check`
   ausführen.
5. Prüfen, dass genau die beiden Befehle Status und Review erscheinen.
6. `pwsh -NoProfile -File tests/test-installed-surfaces.ps1 -ArchiveUrl <https-tag-zip>`
   aus der Preset-Quelle ausführen: acht erzeugte Agentenoberflächen müssen
   vorhandene Validatoren aufrufen und bei fehlender Evidence mit Exitcode 2
   blockieren. Fehlende Skripte sind kein bestandener Negativfall.

*Execute the generated-surface test against the published archive. All eight
surfaces must invoke existing validators; a missing script is not a valid
negative evidence result.*

## Phase 2: Positiver Kontext

1. Vollständigen Kontext mit vier Gates erstellen.
2. Manifest, Version und Dokumenthashes korrekt binden.
3. Einen anwendbaren oder begründet nicht anwendbaren
   `CL-02-13`-bezogenen Cloud-Assurance-Nachweis referenzieren.
4. Alle anwendbaren Pflichtpunkte auf `Fulfilled` setzen.
5. Status unter Bash und PowerShell ausführen.
6. Bytegleiche fachliche Statuszeilen und Exitcode `0` bestätigen.
7. Vorher-/Nachher-Hashes aller Evidence-Dateien vergleichen, um read-only
   nachzuweisen.
8. Den Dateibestand einschließlich versteckter Dateien über normalisierte
   relative Pfade ordinal sortieren und die tatsächlichen rohen SHA-256-Werte
   vergleichen. Ergänzungen, Löschungen und Umbenennungen sind Änderungen.

*Compare deterministically ordered relative paths and actual raw-byte hashes,
including hidden files. Do not normalize away a write when proving read-only.*

## Phase 3: Bewusst blockierter Kontext

Mindestens folgende Fehler einzeln prüfen:

- Manifest-Hashdrift;
- fehlende Checkliste;
- doppelte Checklisten-ID;
- falsche Dokumentversion;
- Dokument-Hashdrift;
- Sammelbanddrift;
- `Ready` bei offenem Pflichtpunkt;
- `N/A` ohne Begründung;
- abgelaufener Review;
- unvollständiges akzeptiertes Risiko;
- fehlende Security-Governance-Voraussetzung;
- unzulässige C5-/ISO-Zertifizierungsbehauptung;
- fehlendes Runbook;
- unvollständige Image-Impact-Felder.

Jeder Fall muss mit Exitcode `2` blockieren und eine textuelle Ursache nennen.

## Phase 4: Vier Gate-Reviews

Führe für `baseline`, `delta`, `closure` und `image-impact` je einen
kontrollierten Review aus. Prüfe:

- genau das benannte Gate wird bewertet;
- die richtige Betriebsart wird angewendet;
- das Runbook-Gate greift;
- keine Baseline-Quelle wird verändert;
- keine menschliche Entscheidung wird automatisch erfüllt.

## Phase 5: Plattformparität

1. Gleiche LF-Fixture unter Bash und PowerShell prüfen.
2. Inhaltlich gleiche CRLF-Fixture prüfen.
3. Inhaltlich gleiche UTF-8-BOM-Fixture prüfen.
4. Gleiche Ergebnisart und gleichen Exitcode bestätigen.
5. Eine echte Inhaltsänderung muss auf beiden Plattformen als Drift blockieren.

## Phase 6: Preset-Komposition

1. Vollständiges 13-Preset-Profil installieren.
2. Auflösen und `specify check` ausführen.
3. Assurance-Preset deaktivieren und erneut prüfen.
4. Assurance-Preset reaktivieren und erneut prüfen.
5. Assurance-Preset entfernen und bestehende Profile 8 bis 12 prüfen.
6. Aus dem Tag-ZIP erneut installieren.

### Bekannte CLI-Grenze / Known CLI limitation

Mit Spec Kit 0.12.11 und mehreren Agenten kann `preset remove` zwei erzeugte
Claude-Skills zuruecklassen, wenn Codex die aktive Integration ist. Deshalb
nach der Entfernung auch `.claude/skills/speckit-secure-development-status/`
und `.claude/skills/speckit-secure-development-review/` kontrollieren.
Ein gruener Matrixcheck beweist nicht, dass diese Dateien entfernt wurden.
Die verwaisten Skills haben keinen installierten Validator und sind nicht
benutzbar. Fuer eine echte Deinstallation nur die nach Herkunft, Hash und
lokalen Aenderungen geprueften generierten Dateien gezielt entfernen;
eigene Anpassungen bewahren. Eine Wiederinstallation erzeugt sie erneut.
Diese CLI-Grenze getrennt im Feldbericht auffuehren; kein impliziter
Erfolgsnachweis und keine automatische CLI-Aenderung.

*With Spec Kit 0.12.11, multi-agent removal can leave two generated Claude
skills when Codex is the active integration. Check both named directories;
an exact preset matrix does not prove their removal. The orphaned skills
cannot run without their validators. For an actual uninstall, remove only
verified generated files after checking origin, hashes and local changes;
preserve user edits. Reinstallation generates the files again. Report this
CLI limitation separately rather than claiming complete removal or silently
patching the CLI.*

## Phase 7: Ergebnis

Der Feldbericht enthält:

- Release-URL, Tag und ZIP-SHA-256;
- Testprojekt und anonymisierten Scope;
- Betriebssysteme und Shell-Versionen;
- Spec-Kit- und Security-Governance-Version;
- alle Testfälle mit Ergebnis und Exitcode;
- Abweichungen zwischen Bash und PowerShell;
- offene Findings;
- Restrisiken;
- Empfehlung `ReleaseAccepted`, `PatchRequired` oder `Blocked`.

`PatchRequired` erzeugt eine neue Version, zum Beispiel `v0.1.1`. Das
veröffentlichte Tag `v0.1.0` bleibt unverändert.

## Abbruchkriterien / Stop Conditions

Der Feldtest wird abgebrochen, wenn Secrets sichtbar werden, eine Baseline
unerwartet verändert wird, menschliche Freigaben implizit gesetzt werden, ein
Validator widersprüchliche Ergebnisse liefert oder die Testumgebung nicht
mehr kontrolliert ist.

## Kontextbindung und Risikotypen / Context Binding and Risk Types

Ab v0.1.3 muss ein Review genau ein Verzeichnis
`<YYYY-MM-DD>-<context-id>` finden. Die ID wird vollständig und mit
Groß-/Kleinschreibung verglichen: `foo` bezeichnet nicht `bar-foo`.
Mehrere datierte Verzeichnisse derselben ID blockieren; es wird nicht
stillschweigend das neueste gewählt. Die `contextId` und `mode` der
ausgewählten Gate-Evidence müssen exakt dem Auftrag entsprechen.
Bei Abweichung: Exitcode 2, keine Erfolgsmeldung, keine Evidence-Änderung.
Vor Wiederholung den gewünschten Kontext und Auftrag fachlich klären;
keine Evidence allein zum Bestehen des Validators umbenennen oder freigeben.

Wenn `acceptedRisks` vorhanden ist, muss es ein JSON-Array sein.
Ein einzelnes Risiko steht in `[{...}]`, nicht in `{...}`.
`null`, boolesche Werte, Zahlen und Zeichenketten sind unzulässig.
Ein leeres Array ist zulässig, jedoch nicht bei `ReadyWithAcceptedRisks`.
Fehlende optionale `acceptedRisks` bleiben bei anderen Ergebnissen zulässig.
Die vorhandenen Pflichtfelder jedes Risikos gelten unverändert.

*From v0.1.3, review requires exactly one dated directory with the full,
case-sensitive context ID. Suffix matches and duplicate dated contexts are
rejected. Evidence contextId and mode must exactly match the request.
Failure returns exit 2 without success output or evidence writes.
Clarify the intended context before retrying; never rename or approve evidence
merely to satisfy validation. When present, acceptedRisks must be a JSON
array; one risk uses [{...}], not {...}. Null, booleans, numbers and strings
are invalid. Empty arrays are allowed except for ReadyWithAcceptedRisks.
Other outcomes may omit the optional field. Risk metadata requirements and
all human decision boundaries remain unchanged.*
