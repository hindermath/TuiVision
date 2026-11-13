# Nutzungsleitfaden: model-routing-governance

<!-- current-package: 0.2.9 -->

Paketkennung: **v0.2.9**. [Patch und Veröffentlichungsnachweis](release-0.2.9-validation.md). Die Release-Seite und der externe Archivnachweis belegen die Veröffentlichung, nicht ein Quellcommit. Frühere Versionsnachweise unten bleiben historisch.

<span lang="en">Package identity: v0.2.9. The release page and external archive evidence prove publication, not a source commit. Earlier version records below remain historical.</span>

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](release-0.1.6-validation.md).

Der unten verlinkte Basispaket-Nachweis bleibt historisch; dieses Paket nimmt die Quellenkorrekturen als neue Version auf.

[English](user-guide.EN.md) · [Preset-Referenz](../README.md)

Historisches Basispaket **v0.1.6**. Den tatsächlich veröffentlichten Stand auf der [Release-Seite](https://github.com/hindermath/spec-kit-preset-model-routing-governance/releases) prüfen. Dieser Leitfaden erteilt keine Merge-, Release- oder Projektfreigabe.

Inhalt: [Start](#start) · [Beispiel](#example) · [Begriffe](#terms) · [Routing](#routing) · [Fehlerhilfe](#help) · [Referenzen](#references)

<a id="start"></a>

## Start und Zielgruppe

Stabile Aufgabenrollen an ausdrücklich eingerichtete lokale Profile binden; die Grenzen dieses Releases sichtbar halten.

Für Auszubildende aller vier IHK-IT-Berufe: Fachinformatiker*innen (Anwendungsentwicklung, Systemintegration, Daten- und Prozessanalyse, Digitale Vernetzung), IT-System-Elektroniker*innen, Kaufleute für Digitalisierungsmanagement und Kaufleute für IT-System-Management. Ebenso für Entwickelnde ohne Governance-Spezialwissen. Du musst zum Einstieg keine Hashes bearbeiten oder Modell-IDs erfinden. Technische Bindungen mit der Betreuung prüfen.

Nutze ein bereits mit Spec Kit initialisiertes Ziel-Repository. Lies zuerst Voraussetzungen und Installation in der Preset-Referenz; ersetze Beispielpfade. Eine Vorlage ist ein Starter, kein ausgefüllter Nachweis. Installation ergänzt Anweisungen und Dateien; sie startet kein Feature und erteilt keine Freigabe.

Eingabe: Benenne den vorhandenen Harness und ob du nur Status oder einen lokalen Refresh möchtest.

<a id="example"></a>

## Ein vollständiges Beispiel

1. Beschreibe den begrenzten Auftrag und vorhandene Dateien. Scope nicht still erweitern.
2. Nutze das folgende Beispiel im Agentenchat. Es ist ein Prompt, kein Shell-Befehl:

~~~text
$speckit-model-routing-status — nur vorhandene Bindung und gemeldete Grenzen erklären; keine Modelle aufrufen, kein Refresh und kein Anbieterwechsel.
~~~

3. Status oder konkreter Blocker mit nächster Aktion. Das Paket unterstützt zusätzlich Claude, Copilot und OpenCode. Voraussetzungen und Grenzen stehen unter [Automatische Adapter](automatic-discovery.md).
4. Prüfe Ergebnis und dokumentierte nächste Aktion. Änderungen, Reparatur, Feature-Ausführung und Lieferung brauchen jeweils die passende Autorität. Ein grüner technischer Check ist keine menschliche Abnahme.

Lesende Installationsprüfung im initialisierten Ziel-Repository:

~~~text
specify preset info model-routing-governance
specify preset resolve model-routing-policy-template
~~~

Resolve zeigt die ausgewählte Vorlagenquelle; es füllt die Vorlage nicht aus und führt sie nicht aus.

<a id="terms"></a>

## Begriffe und Nachweise

- **Harness:** Lokale Agenten-Integration; Anmeldung und Verfügbarkeit sind kein Qualitätsnachweis.
- **Routing-Rolle:** Aufgabenklasse, keine bestimmte Modell-ID und keine Freigabe.
- **Preflight:** Vorprüfung vor Ausführung; eine Modellprobe kann Kontingent verbrauchen, auch wenn sie keine Projektdateien ändert.

Wo der bestehende Vertrag diese Werte verwendet: Applicable bedeutet anwendbar, nicht bestanden; N/A braucht einen Grund; Open braucht die dokumentierte nächste Aktion und zuständige Person. Diese Erklärung ergänzt keine Schema-Felder. Echte historische Evidence erhalten; Hash oder Freigabe niemals nur zum Bestehen einer Prüfung ändern.

<a id="routing"></a>

## Stabiles Routing und ausdrückliche Aktionen

| Command | Deklarierte Rolle |
|---|---|
| `speckit.model-routing-status` | `fast-mechanical` |
| `speckit.model-routing-refresh` | `frontier-reasoning` |

Rollen beschreiben Aufgaben, keinen Modellnamen und keine Qualitätsgarantie. Maßgeblich ist model-routing.json; allgemeine Beispiele wie coding-review ersetzen keine stärkere Command-Anforderung. Der bestehende autonome Runner verwendet die stärkste überlappende Rolle, unabhängig von der Installationspriorität. Die Installation eines Rollenvertrags stellt keinen laufenden Chat um und richtet keine persönlichen Profile ein.

Eine modellfreie Skriptprüfung und eine Agentenbewertung sind verschiedene Tätigkeiten. Modellaufrufe, auch Zugangs-Preflights, können trotz read-only-Verhalten Kontingent verbrauchen. Diese Dokumentation beauftragt keine Probe und keinen Anbieterwechsel. Sie enthält keine neue dynamische Routing-Implementierung.

Paketversion, Schema-Version und historischer Feldtest sind verschiedene Angaben. Eine frühere Abnahme gilt nur für ihren belegten Scope und Quellenstand.

<a id="help"></a>

## Fehlerhilfe und sichere nächste Aktion

Bei Claude und Copilot den vorhandenen SDK-Einstiegspunkt angeben. Bei OpenCode einen bereits laufenden Loopback-Server und einen verbundenen Provider benennen. Fehlende Metadaten oder mehrere passende Modelle blockieren; siehe [Fehlerhilfe für neue Adapter](automatic-discovery.md).

Fehlt ein Command oder eine Vorlage, zuerst preset info und das unten verlinkte Manifest prüfen. Mindestwerkzeuge und aktive Installation prüfen. Unklarheit mit der Betreuung klären; weder Policy ändern, Validatoren abschalten noch einen nicht ausgeführten Test behaupten. Eine Schutzsperre bleibt eine Sperre.

Diese Sprach- und Zugänglichkeitsrunde zielt auf CEFR B2 und anwendbare WCAG 2.2 AA. Statische Tests bescheinigen das Ziel nicht. Nicht tatsächlich ausgeführte Screenreader-, Braille- und Nutzendentests bleiben ungeprüft.

<a id="references"></a>

## Vorlagen und Vertiefung

- [`model-routing-policy-template`](../templates/model-routing-policy-template.json)
- [`model-routing-profiles-template`](../templates/model-routing-profiles-template.json)

Diese Namen bezeichnen verfügbare Starter; sie sind kein Auftrag, jede Datei anzulegen. Vorhandene passende Evidence weiterverwenden. JSON-Feldnamen bleiben technische Bezeichner, keine zu übersetzenden Schlüssel.

- [Manifest und Voraussetzungen](../preset.yml)
- [Stabiler Command-Rollenvertrag](../model-routing.json)
- [Dokumentationsumfang, Sichtung und Prüfgrenzen](documentation-closeout.md)
