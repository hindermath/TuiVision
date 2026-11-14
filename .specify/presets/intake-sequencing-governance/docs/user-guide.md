# Nutzungsleitfaden: intake-sequencing-governance

<!-- current-package: 0.2.14 -->

Paketkennung: **v0.2.14**. [Vorbereitung und Veröffentlichungsgrenze](release-0.2.14-validation.md). Die Release-Seite und der externe Archivnachweis belegen die Veröffentlichung, nicht dieser Vorbereitungsstand. Frühere Versionsnachweise unten bleiben historisch.

<span lang="en">Package identity: v0.2.14. The release page and external archive evidence prove publication, not this preparation snapshot. Earlier version records below remain historical.</span>

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](release-0.2.10-validation.md).

Der unten verlinkte Basispaket-Nachweis bleibt historisch; dieses Paket nimmt die Quellenkorrekturen als neue Version auf.

[English](user-guide.EN.md) · [Preset-Referenz](../README.md)

Historisches Basispaket **v0.2.10**. Den tatsächlich veröffentlichten Stand auf der [Release-Seite](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/releases) prüfen. Dieser Leitfaden erteilt keine Merge-, Release- oder Projektfreigabe.

Inhalt: [Start](#start) · [Beispiel](#example) · [Begriffe](#terms) · [Routing](#routing) · [Fehlerhilfe](#help) · [Referenzen](#references)

<a id="start"></a>

## Start und Zielgruppe

Abhängigkeiten und Reihenfolge vorhandener Arbeitsaufträge erklären, ohne ihre Arbeit zu starten.

Für Auszubildende aller vier IHK-IT-Berufe: Fachinformatiker*innen (Anwendungsentwicklung, Systemintegration, Daten- und Prozessanalyse, Digitale Vernetzung), IT-System-Elektroniker*innen, Kaufleute für Digitalisierungsmanagement und Kaufleute für IT-System-Management. Ebenso für Entwickelnde ohne Governance-Spezialwissen. Du musst zum Einstieg keine Hashes bearbeiten oder Modell-IDs erfinden. Technische Bindungen mit der Betreuung prüfen.

Nutze ein bereits mit Spec Kit initialisiertes Ziel-Repository. Lies zuerst Voraussetzungen und Installation in der Preset-Referenz; ersetze Beispielpfade. Eine Vorlage ist ein Starter, kein ausgefüllter Nachweis. Installation ergänzt Anweisungen und Dateien; sie startet kein Feature und erteilt keine Freigabe.

Eingabe: Benenne vorhandene Intakes A und B und begründe, warum B A benötigt.

<a id="example"></a>

## Ein vollständiges Beispiel

1. Beschreibe den begrenzten Auftrag und vorhandene Dateien. Scope nicht still erweitern.
2. Nutze das folgende Beispiel im Agentenchat. Es ist ein Prompt, kein Shell-Befehl:

~~~text
$speckit-intake-series-create Schlage genau eine Serie A vor B vor. Abhängigkeit begründen und vor Erstellung freigeben lassen; danach Status und Next nur berichten.
~~~

3. Serienmanifest, lesbare Reihenfolge und Receipt erklären Ziele, Kanten und Status. Next nennt Kandidaten oder Blocker, startet aber keinen Lauf.
4. Prüfe Ergebnis und dokumentierte nächste Aktion. Änderungen, Reparatur, Feature-Ausführung und Lieferung brauchen jeweils die passende Autorität. Ein grüner technischer Check ist keine menschliche Abnahme.

Lesende Installationsprüfung im initialisierten Ziel-Repository:

~~~text
specify preset info intake-sequencing-governance
specify preset resolve intake-series-policy-template
~~~

Resolve zeigt die ausgewählte Vorlagenquelle; es füllt die Vorlage nicht aus und führt sie nicht aus.

<a id="terms"></a>

## Begriffe und Nachweise

- **DAG:** Gerichteter Abhängigkeitsgraph ohne Kreis: ein Ziel kann nicht sein eigener Vorgänger werden.
- **Eligible:** Nach Serienregeln startfähig; noch keine Ausführungsfreigabe.
- **Idle:** Gültiger leerer Zustand ohne eigene aktive Ziele, keine Einladung für erfundene Platzhalter.

Wo der bestehende Vertrag diese Werte verwendet: Applicable bedeutet anwendbar, nicht bestanden; N/A braucht einen Grund; Open braucht die dokumentierte nächste Aktion und zuständige Person. Diese Erklärung ergänzt keine Schema-Felder. Echte historische Evidence erhalten; Hash oder Freigabe niemals nur zum Bestehen einer Prüfung ändern.

<a id="routing"></a>

## Stabiles Routing und ausdrückliche Aktionen

| Command | Deklarierte Rolle |
|---|---|
| `speckit.intake-series-create` | `frontier-reasoning` |
| `speckit.intake-series-read` | `fast-mechanical` |
| `speckit.intake-series-update` | `frontier-reasoning` |
| `speckit.intake-series-delete` | `frontier-reasoning` |
| `speckit.intake-series-status` | `fast-mechanical` |
| `speckit.intake-series-next` | `fast-mechanical` |

Rollen beschreiben Aufgaben, keinen Modellnamen und keine Qualitätsgarantie. Maßgeblich ist model-routing.json; allgemeine Beispiele wie coding-review ersetzen keine stärkere Command-Anforderung. Der bestehende autonome Runner verwendet die stärkste überlappende Rolle, unabhängig von der Installationspriorität. Die Installation eines Rollenvertrags stellt keinen laufenden Chat um und richtet keine persönlichen Profile ein.

Eine modellfreie Skriptprüfung und eine Agentenbewertung sind verschiedene Tätigkeiten. Modellaufrufe, auch Zugangs-Preflights, können trotz read-only-Verhalten Kontingent verbrauchen. Diese Dokumentation beauftragt keine Probe und keinen Anbieterwechsel. Sie enthält keine neue dynamische Routing-Implementierung.

Dokumentations-Zielkombination: Authoring **0.3.10**, Review **0.2.7**, Sequencing **0.2.11**. Diese Kandidatenkombination ist kein behaupteter Feldtest. Verifizierte Tests werden im PR getrennt aufgeführt.

<a id="help"></a>

## Fehlerhilfe und sichere nächste Aktion

Bei RIG017 Status und Ablage unter genehmigter Update-Autorität klären. Bindende Vorgänger nicht mit einer bloßen bevorzugten Lieferreihenfolge verwechseln.

Fehlt ein Command oder eine Vorlage, zuerst preset info und das unten verlinkte Manifest prüfen. Mindestwerkzeuge und aktive Installation prüfen. Unklarheit mit der Betreuung klären; weder Policy ändern, Validatoren abschalten noch einen nicht ausgeführten Test behaupten. Eine Schutzsperre bleibt eine Sperre.

Diese Sprach- und Zugänglichkeitsrunde zielt auf CEFR B2 und anwendbare WCAG 2.2 AA. Statische Tests bescheinigen das Ziel nicht. Nicht tatsächlich ausgeführte Screenreader-, Braille- und Nutzendentests bleiben ungeprüft.

<a id="references"></a>

## Vorlagen und Vertiefung

- [`intake-series-policy-template`](../templates/intake-series-policy-template.json)
- [`intake-series-manifest-template`](../templates/intake-series-manifest-template.json)
- [`intake-series-idle-manifest-template`](../templates/intake-series-idle-manifest-template.json)
- [`intake-series-receipt-template`](../templates/intake-series-receipt-template.json)
- [`intake-series-operation-template`](../templates/intake-series-operation-template.json)
- [`intake-series-tombstone-template`](../templates/intake-series-tombstone-template.json)
- [`intake-series-order-template`](../templates/intake-series-order-template.md)
- [`intake-series-checklist-template`](../templates/intake-series-checklist-template.md)
- [`intake-series-runbook-template`](../templates/intake-series-runbook-template.md)
- [`intake-series-field-validation-summary`](../templates/field-validation-summary.md)

Diese Namen bezeichnen verfügbare Starter; sie sind kein Auftrag, jede Datei anzulegen. Vorhandene passende Evidence weiterverwenden. JSON-Feldnamen bleiben technische Bezeichner, keine zu übersetzenden Schlüssel.

- [Manifest und Voraussetzungen](../preset.yml)
- [Stabiler Command-Rollenvertrag](../model-routing.json)
- [Dokumentationsumfang, Sichtung und Prüfgrenzen](documentation-closeout.md)
