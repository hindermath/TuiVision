# Nutzungsleitfaden: project-statistics-governance

<!-- current-package: 0.1.4 -->

Paketkennung: **v0.1.4**. [Vorbereitung und Veröffentlichungsgrenze](release-0.1.4-validation.md). Die Release-Seite und der externe Archivnachweis belegen die Veröffentlichung, nicht dieser Vorbereitungsstand. Frühere Versionsnachweise unten bleiben historisch.

<span lang="en">Package identity: v0.1.4. The release page and external archive evidence prove publication, not this preparation snapshot. Earlier version records below remain historical.</span>

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](release-0.1.2-validation.md).

Der unten verlinkte Basispaket-Nachweis bleibt historisch; dieses Paket nimmt die Quellenkorrekturen als neue Version auf.

[English](user-guide.EN.md) · [Preset-Referenz](../README.md)

Historisches Basispaket **v0.1.2**. Den tatsächlich veröffentlichten Stand auf der [Release-Seite](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/releases) prüfen. Dieser Leitfaden erteilt keine Merge-, Release- oder Projektfreigabe.

Inhalt: [Start](#start) · [Beispiel](#example) · [Begriffe](#terms) · [Routing](#routing) · [Fehlerhilfe](#help) · [Referenzen](#references)

<a id="start"></a>

## Start und Zielgruppe

Reproduzierbare Projekttransparenz aus Git-Daten zeigen, nicht KI-Produktivität messen.

Für Auszubildende aller vier IHK-IT-Berufe: Fachinformatiker*innen (Anwendungsentwicklung, Systemintegration, Daten- und Prozessanalyse, Digitale Vernetzung), IT-System-Elektroniker*innen, Kaufleute für Digitalisierungsmanagement und Kaufleute für IT-System-Management. Ebenso für Entwickelnde ohne Governance-Spezialwissen. Du musst zum Einstieg keine Hashes bearbeiten oder Modell-IDs erfinden. Technische Bindungen mit der Betreuung prüfen.

Nutze ein bereits mit Spec Kit initialisiertes Ziel-Repository. Lies zuerst Voraussetzungen und Installation in der Preset-Referenz; ersetze Beispielpfade. Eine Vorlage ist ein Starter, kein ausgefüllter Nachweis. Installation ergänzt Anweisungen und Dateien; sie startet kein Feature und erteilt keine Freigabe.

Eingabe: Benenne Repository, Konfiguration und getrennten Berichtspfad; vorhandene Statistik erhalten.

<a id="example"></a>

## Ein vollständiges Beispiel

1. Beschreibe den begrenzten Auftrag und vorhandene Dateien. Scope nicht still erweitern.
2. Nutze das folgende Beispiel im Agentenchat. Es ist ein Prompt, kein Shell-Befehl:

~~~text
$speckit-statistics-status — vorhandene Messung, Quellenbindung und nächste Aktion prüfen. Keine Initialisierung, Messung oder Berichtsschreibung starten.
~~~

3. Aktualität und Git-/Konfigurationsbindung sind sichtbar. Ein Messwert ist weder Qualitätsurteil noch gemessene Zeitersparnis oder Lernerfolg.
4. Prüfe Ergebnis und dokumentierte nächste Aktion. Änderungen, Reparatur, Feature-Ausführung und Lieferung brauchen jeweils die passende Autorität. Ein grüner technischer Check ist keine menschliche Abnahme.

Lesende Installationsprüfung im initialisierten Ziel-Repository:

~~~text
specify preset info project-statistics-governance
specify preset resolve project-statistics-contract
~~~

Resolve zeigt die ausgewählte Vorlagenquelle; es füllt die Vorlage nicht aus und führt sie nicht aus.

<a id="terms"></a>

## Begriffe und Nachweise

- **Provenienz:** Nachvollziehbare Herkunft von Git-Stand, Konfiguration und Werkzeugversion.
- **Snapshot:** Gebundener Messstand, keine automatisch aktuelle Live-Messung.
- **Referenzszenario:** Optionale Modellannahme, standardmäßig aus; keine gemessene Arbeitszeit.

Wo der bestehende Vertrag diese Werte verwendet: Applicable bedeutet anwendbar, nicht bestanden; N/A braucht einen Grund; Open braucht die dokumentierte nächste Aktion und zuständige Person. Diese Erklärung ergänzt keine Schema-Felder. Echte historische Evidence erhalten; Hash oder Freigabe niemals nur zum Bestehen einer Prüfung ändern.

<a id="routing"></a>

## Stabiles Routing und ausdrückliche Aktionen

| Command | Deklarierte Rolle |
|---|---|
| `speckit.statistics-init` | `frontier-reasoning` |
| `speckit.statistics-status` | `fast-mechanical` |
| `speckit.statistics-update` | `frontier-reasoning` |

Rollen beschreiben Aufgaben, keinen Modellnamen und keine Qualitätsgarantie. Maßgeblich ist model-routing.json; allgemeine Beispiele wie coding-review ersetzen keine stärkere Command-Anforderung. Der bestehende autonome Runner verwendet die stärkste überlappende Rolle, unabhängig von der Installationspriorität. Die Installation eines Rollenvertrags stellt keinen laufenden Chat um und richtet keine persönlichen Profile ein.

Eine modellfreie Skriptprüfung und eine Agentenbewertung sind verschiedene Tätigkeiten. Modellaufrufe, auch Zugangs-Preflights, können trotz read-only-Verhalten Kontingent verbrauchen. Diese Dokumentation beauftragt keine Probe und keinen Anbieterwechsel. Sie enthält keine neue dynamische Routing-Implementierung.

Paketversion, Schema-Version und historischer Feldtest sind verschiedene Angaben. Eine frühere Abnahme gilt nur für ihren belegten Scope und Quellenstand.

<a id="help"></a>

## Fehlerhilfe und sichere nächste Aktion

Fehlende Prüfsumme oder flache Git-Historie klären, nicht übergehen. Interne Messformatversion, Paketversion und historischer v0.1.0-Feldtest haben unterschiedliche Aufgaben.

Fehlt ein Command oder eine Vorlage, zuerst preset info und das unten verlinkte Manifest prüfen. Mindestwerkzeuge und aktive Installation prüfen. Unklarheit mit der Betreuung klären; weder Policy ändern, Validatoren abschalten noch einen nicht ausgeführten Test behaupten. Eine Schutzsperre bleibt eine Sperre.

Diese Sprach- und Zugänglichkeitsrunde zielt auf CEFR B2 und anwendbare WCAG 2.2 AA. Statische Tests bescheinigen das Ziel nicht. Nicht tatsächlich ausgeführte Screenreader-, Braille- und Nutzendentests bleiben ungeprüft.

<a id="references"></a>

## Vorlagen und Vertiefung

- [`project-statistics-contract`](../templates/project-statistics-contract.md)

Diese Namen bezeichnen verfügbare Starter; sie sind kein Auftrag, jede Datei anzulegen. Vorhandene passende Evidence weiterverwenden. JSON-Feldnamen bleiben technische Bezeichner, keine zu übersetzenden Schlüssel.

- [Manifest und Voraussetzungen](../preset.yml)
- [Stabiler Command-Rollenvertrag](../model-routing.json)
- [Dokumentationsumfang, Sichtung und Prüfgrenzen](documentation-closeout.md)
