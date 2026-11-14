# Nutzungsleitfaden: cross-platform-governance

<!-- current-package: 0.2.6 -->

Paketkennung: **v0.2.6**. [Vorbereitung und Veröffentlichungsgrenze](release-0.2.6-validation.md). Die Release-Seite und der externe Archivnachweis belegen die Veröffentlichung, nicht dieser Vorbereitungsstand. Frühere Versionsnachweise unten bleiben historisch.

<span lang="en">Package identity: v0.2.6. The release page and external archive evidence prove publication, not this preparation snapshot. Earlier version records below remain historical.</span>

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](release-0.2.4-validation.md).

Der unten verlinkte Basispaket-Nachweis bleibt historisch; dieses Paket nimmt die Quellenkorrekturen als neue Version auf.

[English](user-guide.EN.md) · [Preset-Referenz](../README.md)

Historisches Basispaket **v0.2.4**. Den tatsächlich veröffentlichten Stand auf der [Release-Seite](https://github.com/hindermath/spec-kit-preset-cross-platform-governance/releases) prüfen. Dieser Leitfaden erteilt keine Merge-, Release- oder Projektfreigabe.

Inhalt: [Start](#start) · [Beispiel](#example) · [Begriffe](#terms) · [Routing](#routing) · [Fehlerhilfe](#help) · [Referenzen](#references)

<a id="start"></a>

## Start und Zielgruppe

Dieselbe fachliche Funktion auf benannten Plattformen prüfen, ohne nicht ausgeführte Tests zu behaupten.

Für Auszubildende aller vier IHK-IT-Berufe: Fachinformatiker*innen (Anwendungsentwicklung, Systemintegration, Daten- und Prozessanalyse, Digitale Vernetzung), IT-System-Elektroniker*innen, Kaufleute für Digitalisierungsmanagement und Kaufleute für IT-System-Management. Ebenso für Entwickelnde ohne Governance-Spezialwissen. Du musst zum Einstieg keine Hashes bearbeiten oder Modell-IDs erfinden. Technische Bindungen mit der Betreuung prüfen.

Nutze ein bereits mit Spec Kit initialisiertes Ziel-Repository. Lies zuerst Voraussetzungen und Installation in der Preset-Referenz; ersetze Beispielpfade. Eine Vorlage ist ein Starter, kein ausgefüllter Nachweis. Installation ergänzt Anweisungen und Dateien; sie startet kein Feature und erteilt keine Freigabe.

Eingabe: Benenne Skript, Zielplattformen und vorhandene Bash-/PowerShell-Varianten.

<a id="example"></a>

## Ein vollständiges Beispiel

1. Beschreibe den begrenzten Auftrag und vorhandene Dateien. Scope nicht still erweitern.
2. Nutze das folgende Beispiel im Agentenchat. Es ist ein Prompt, kein Shell-Befehl:

~~~text
Nutze script-parity-checklist-template für check-example.sh und check-example.ps1. Plane docs/cross-platform/check-example.md, Manpage und bilinguale PowerShell-Hilfe samt nativen Tests.
~~~

3. Gleiches Verhalten, Vorschau, Fehlercodes und Hilfen sind vergleichbar dokumentiert. Begründete native Ausnahmen und nicht geprüfte Plattformen bleiben sichtbar.
4. Prüfe Ergebnis und dokumentierte nächste Aktion. Änderungen, Reparatur, Feature-Ausführung und Lieferung brauchen jeweils die passende Autorität. Ein grüner technischer Check ist keine menschliche Abnahme.

Lesende Installationsprüfung im initialisierten Ziel-Repository:

~~~text
specify preset info cross-platform-governance
specify preset resolve script-parity-checklist-template
~~~

Resolve zeigt die ausgewählte Vorlagenquelle; es füllt die Vorlage nicht aus und führt sie nicht aus.

<a id="terms"></a>

## Begriffe und Nachweise

- **Parität:** Fachlich gleiches Verhalten; nicht identischer Quelltext.
- **Manpage:** Lokal lesbare Unix-Handbuchseite, meist mit man aufrufbar.
- **WhatIf / Dry-Run:** Vorschau geplanter Änderungen; die genauen Seiteneffekte des jeweiligen Tools prüfen.

Wo der bestehende Vertrag diese Werte verwendet: Applicable bedeutet anwendbar, nicht bestanden; N/A braucht einen Grund; Open braucht die dokumentierte nächste Aktion und zuständige Person. Diese Erklärung ergänzt keine Schema-Felder. Echte historische Evidence erhalten; Hash oder Freigabe niemals nur zum Bestehen einer Prüfung ändern.

<a id="routing"></a>

## Stabiles Routing und ausdrückliche Aktionen

| Command | Deklarierte Rolle |
|---|---|
| `speckit.specify` | `frontier-reasoning` |
| `speckit.plan` | `frontier-reasoning` |
| `speckit.tasks` | `frontier-reasoning` |

Rollen beschreiben Aufgaben, keinen Modellnamen und keine Qualitätsgarantie. Maßgeblich ist model-routing.json; allgemeine Beispiele wie coding-review ersetzen keine stärkere Command-Anforderung. Der bestehende autonome Runner verwendet die stärkste überlappende Rolle, unabhängig von der Installationspriorität. Die Installation eines Rollenvertrags stellt keinen laufenden Chat um und richtet keine persönlichen Profile ein.

Eine modellfreie Skriptprüfung und eine Agentenbewertung sind verschiedene Tätigkeiten. Modellaufrufe, auch Zugangs-Preflights, können trotz read-only-Verhalten Kontingent verbrauchen. Diese Dokumentation beauftragt keine Probe und keinen Anbieterwechsel. Sie enthält keine neue dynamische Routing-Implementierung.

Paketversion, Schema-Version und historischer Feldtest sind verschiedene Angaben. Eine frühere Abnahme gilt nur für ihren belegten Scope und Quellenstand.

<a id="help"></a>

## Fehlerhilfe und sichere nächste Aktion

Eine Syntaxprüfung ersetzt keinen nativen Windows-/Linux-/macOS-Test. Für eine Ein-Plattform-Ausnahme Grund und Scope angeben; keine vorhandene Pflichtprüfung entfernen.

Fehlt ein Command oder eine Vorlage, zuerst preset info und das unten verlinkte Manifest prüfen. Mindestwerkzeuge und aktive Installation prüfen. Unklarheit mit der Betreuung klären; weder Policy ändern, Validatoren abschalten noch einen nicht ausgeführten Test behaupten. Eine Schutzsperre bleibt eine Sperre.

Diese Sprach- und Zugänglichkeitsrunde zielt auf CEFR B2 und anwendbare WCAG 2.2 AA. Statische Tests bescheinigen das Ziel nicht. Nicht tatsächlich ausgeführte Screenreader-, Braille- und Nutzendentests bleiben ungeprüft.

<a id="references"></a>

## Vorlagen und Vertiefung

- [`script-parity-checklist-template`](../templates/script-parity-checklist-template.md)
- [`man-page-template`](../templates/man-page-template.md)
- [`powershell-help-template`](../templates/powershell-help-template.md)

Diese Namen bezeichnen verfügbare Starter; sie sind kein Auftrag, jede Datei anzulegen. Vorhandene passende Evidence weiterverwenden. JSON-Feldnamen bleiben technische Bezeichner, keine zu übersetzenden Schlüssel.

- [Manifest und Voraussetzungen](../preset.yml)
- [Stabiler Command-Rollenvertrag](../model-routing.json)
- [Dokumentationsumfang, Sichtung und Prüfgrenzen](documentation-closeout.md)
