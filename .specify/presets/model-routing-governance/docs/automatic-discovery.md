# Automatische Adapter: Model Routing Governance 0.2.6

[English](automatic-discovery.EN.md) · [Nutzungsleitfaden](user-guide.md)

Das Paket unterstützt die Erkennung für `agy`, Claude, Copilot und OpenCode.
Die bisherigen Codex-Regeln sowie historische `agy`-Muster, Profile und Aufrufe
bleiben erhalten. Beispiele ändern keine installierten Presets. Installation,
lokale Bindungsänderungen und Rollout benötigen jeweils einen eigenen Auftrag.

## Voraussetzungen und Status

PowerShell 7 und die ausgewählte CLI müssen vorhanden sein. Für Claude und
Copilot wird zusätzlich Node.js benötigt. Gib mit `-SdkModulePath` den absoluten
JavaScript-Einstieg einer bereits installierten, vertrauenswürdigen offiziellen
SDK-Version an: `@anthropic-ai/claude-agent-sdk` oder `@github/copilot-sdk`.
Dieser Pfad wird als Code importiert; niemals eine Datei aus untrusted Quellen
oder einen Modellvorschlag dafür übernehmen. Kein automatisches npm/npx,
keine SDK-, CLI- oder Preset-Installation.

```text
bash scripts/resolve-model-routing.sh -Action Status -Harness Claude -SdkModulePath /absolute/sdk/entry.mjs
bash scripts/resolve-model-routing.sh -Action Status -Harness Copilot -SdkModulePath /absolute/sdk/entry.js
```

Unter Windows denselben Resolver mit `pwsh -NoProfile -File
scripts/resolve-model-routing.ps1` und den gleichen Parametern aufrufen.
Die Beispiele enthalten Platzhalter, keine funktionierenden SDK-Pfade.

Claude liest `supportedModels()` über einen ruhenden Streaming-Eingang. Es
wird kein Nutzerprompt gesendet. Persistenz, optionale Settings, Tools und MCP
werden für diesen Metadatenpfad deaktiviert. Copilot startet nur den
SDK-Steuerprozess mit der ausgewählten installierten CLI und liest
`listModels()`; keine Inferenz-Session wird angelegt. Unpassende SDK-/CLI-
Schnittstellen blockieren. Der SDK-Unterprozess hat ein Limit von 25 Sekunden.

OpenCode verwendet einen **bereits laufenden** lokalen Server. Benenne den
verbundenen Provider ausdrücklich; der Resolver startet keinen Server und
ändert keine Serverkonfiguration:

```text
bash scripts/resolve-model-routing.sh -Action Status -Harness OpenCode -OpenCodeServerUrl http://127.0.0.1:4096 -OpenCodeProvider anthropic
```

Die URL darf nur einen HTTP-Ursprung mit `127.0.0.1` oder `[::1]` enthalten.
Pfade, Zugangsdaten, Query, Fragment und Redirects sind gesperrt. Der Resolver
liest zuerst `GET /api/provider` und `GET /api/model`, ohne Proxy. Unterstützt
werden die OpenCode-2.2.5-Hüllen mit `data[]`, exakten `providerID`-Werten,
aktivierten Modellen und bestätigten `variants[]`. Wenn die aktuelle API nicht
verfügbar ist oder HTML liefert, wird ausschließlich `GET /provider` als
Legacy-Fallback geprüft. Unbekannte Schemata bleiben blockiert. Optional übernimmt er
`OPENCODE_SERVER_PASSWORD` und `OPENCODE_SERVER_USERNAME` aus der Umgebung;
Standard-Benutzer ist `opencode`. Zugangsdaten niemals in URL oder Git ablegen.
Mit gesetztem Passwort ist `-TrustOpenCodeLoopbackEndpoint` bei jedem Aufruf
erforderlich. Damit gibst du den ausgewaehlten lokalen HTTP-Endpunkt bewusst
frei; HTTP-Loopback bestaetigt nicht die Identitaet des lauschenden Prozesses.
Ohne diese Freigabe wird kein Passwort gesendet. Ohne Passwort bleibt die
Erkennung unveraendert.
HTTP-Antworten sind auf 4 MiB und zeitlich begrenzt. Kein Cache-Refresh.

`Status` und `Refresh` führen keine Modellinferenz aus. Der Resolver schreibt
bei Status keine Profile oder Projektdateien. Die gestarteten Hersteller-CLIs
können eigene Logs, Caches oder Anmeldemetadaten pflegen; vollständige
Nullschreibwerte im Home-Verzeichnis werden nicht zugesichert. Claude liest
auf diesem Pfad keine optionalen lokalen Settings; das ist keine vollständige
Nachbildung einer individuell angepassten interaktiven Sitzung.

## Auswahl und Ergebnisse

Die versionierte Quelle `scripts/adapters/selection-policy.json` filtert zuerst
bestätigte Fähigkeiten und verwendet danach deklarierte Präferenzstufen.
Die Reihenfolge ist eine betriebliche Heuristik, kein Benchmark oder
anbieterübergreifender Qualitätsnachweis. Weder Listenposition noch eine höhere
Versionsnummer allein entscheiden. Codex bleibt unverändert. Bei `agy` bleiben
historische Muster erhalten; zusätzlich werden aktuelle
`claude-opus-*-high`- und `gemini-*-argon-high`-Namen erkannt. Fast verwendet
weiterhin `gemini-*-flash-medium`.

Claude, Copilot und OpenCode geben das strukturierte Phasenergebnis auf der
Standardausgabe zurück. `invoke-harness-with-result.ps1` erhält die native
CLI-Argumentliste als ein JSON-kodiertes Array. Für Copilot extrahiert er aus
dem nativen JSONL-Ereignisstrom die letzte `assistant.message` als Abschluss.
Anschließend akzeptiert er nur ein
erfolgreiches JSON-Objekt und schreibt es atomar an den vom autonomen Runner
vorgegebenen `{outputFile}`-Pfad. Die Ergebnisanweisung verbietet nur, diese
Runner-Ergebnisdatei selbst zu schreiben; autorisierte Projektänderungen der
Aufgabe bleiben erlaubt. Dadurch funktionieren auch lesende Rollen,
ohne dem Harness Dateischreibrechte zu erteilen. Leere, fehlerhafte oder
nicht strukturierte Ausgabe erzeugt keine Ergebnisdatei.

| Rolle | Automatische Präferenzstufen, in Reihenfolge |
|---|---|
| Drei anspruchsvolle Rollen | `opus` → Claude Opus → GPT ohne mini/nano → o-Serie → Gemini Pro → `sonnet` → Claude Sonnet → genau ein sonstiges geeignetes Modell |
| `fast-mechanical` | `haiku` → Claude Haiku → GPT mini/nano → Gemini Flash → `sonnet` → Claude Sonnet → gewähltes Frontier-Modell wiederverwenden |

Die erste nicht leere Stufe muss genau einen geeigneten Kandidaten enthalten.
Mehrere Versionen derselben Stufe blockieren mit Kandidaten-IDs und passendem
Auswahlparameter; die Stufe wird nicht übersprungen. Ein Modell darf alle vier
Rollen übernehmen. Fehlendes Haiku ist kein Blocker und die Wiederverwendung
verspricht keine Kosten- oder Zeitersparnis.

- Alle neuen Adapter verwenden ausschließlich gemeldete IDs. Sonstige Familien
  können über eindeutige Fähigkeitsauswahl oder exakte Auswahl verwendet werden;
  es gibt keine Opus-/Haiku-Erlaubnisliste.
- Claude bevorzugt gelieferte `opus`-/`sonnet`-/`haiku`-Aliase vor vollen IDs.
  `default`, `auto` und `opusplan` sind bei Claude/Copilot ausgeschlossen, da sie
  keine exakte Modellbindung darstellen.
- `[1m]`-Einträge werden erkannt, aber wegen möglicher Kostenfolgen nur nach
  ausdrücklicher exakter Auswahl verwendet.
- Copilot benötigt ausdrücklich `policy.state=enabled`. Fehlende, unbekannte,
  unkonfigurierte oder deaktivierte Policy blockiert den Kandidaten.
- OpenCode berücksichtigt nur den benannten verbundenen Provider. Die
  Modellkennung bleibt `provider/model`; kein Anbieterwechsel bei Fehlern.
- Frontier benötigt bestätigtes `high`. Fast verwendet bestätigtes `medium`,
  sonst `low`, sonst bei ausdrücklich fehlender Reasoning-Unterstützung
  `default` ohne Effort-/Variant-Argument. Andernfalls wird eine bestätigte
  Stufe aus `high`, `xhigh`, `max` verwendet; keine niedrigere Stufe wird erfunden.
  Fehlende Metadaten schließen den Kandidaten aus. Gibt es für eine Rolle keinen
  geeigneten Kandidaten, bleibt der Lauf blockiert.

`-FrontierModel` und `-FastModel` benennen bei Bedarf exakte gemeldete IDs,
auch für neue Familien. Bei OpenCode gehört der Provider zur ID. Fähigkeiten,
Anmeldung, Copilot-Policy und Providerbindung werden damit nicht umgangen.
Beispiel mit synthetischen IDs, keine Zusage tatsächlicher Verfügbarkeit:

```text
bash scripts/resolve-model-routing.sh -Action Status -Harness Copilot -SdkModulePath /absolute/sdk/entry.js -FrontierModel gpt-99 -FastModel gpt-99-mini
```

Nach einem erfolgreichen Refresh verwenden Claude, Copilot und OpenCode diese
ausdrücklich gewählten Modelle bei späteren Status-/Refresh-Aufrufen automatisch
wieder. Das gespeicherte Schema-2.0-Profil gilt nur, wenn Frontier,
Implementation und Review exakt dieselbe Modell-ID und Reasoning-Stufe tragen,
das Fast-Profil eindeutig ist und der aktuelle Katalog alle Modelle weiterhin
aktiv, policy-konform und mit der gespeicherten Reasoning-Stufe meldet. Ein
aktuell angegebener Parameter hat je Rolle Vorrang; der andere Wert darf aus dem
Profil kommen. Ohne gespeicherte Auswahl gilt weiterhin die bestehende
fail-closed Policy.

Fehlt ein gespeichertes Modell oder hat sich seine Policy beziehungsweise
Reasoning-Fähigkeit geändert, blockiert der Lauf und erhält die Profildatei
bytegleich. Es gibt keinen stillen Wechsel auf ein neueres Modell. Ein
ausdrücklich angegebener gültiger Ersatz darf die veraltete Auswahl dieser Rolle
ersetzen. Diese Wiederverwendung gilt nicht für Codex oder `agy`; deren Verhalten
bleibt unverändert.

`0` bedeutet ausgerichtet, `2` bedeutet Refresh erforderlich, `3` bedeutet
blockiert. Ohne Profil zeigt Status einen Vorschlag mit Exitcode 2. Mehrdeutige
Treffer, doppelte IDs, leere Listen oder inkompatible Schnittstellen ergeben
Exitcode 3 und erhalten ein bestehendes Profil. `-WhatIf` schreibt nichts.
Nach ausdrücklicher lokaler Freigabe denselben Aufruf mit `-Action Refresh`
wiederholen. Das Schema bleibt 2.0. `generatedBy` bleibt für Codex/Antigravity
unverändert; neue Profile tragen `model-routing-governance/0.2.0`. Zusätzlich
bindet `selectionPolicySha256` die Auswahlpolicy. Ein anderer oder fehlender
Hash ergibt `RefreshRequired`, ohne das Profil beim Status zu ändern.

## Ausführung bleibt ein eigener Auftrag

Discovery beweist keine nutzbare Quote und keinen erfolgreichen Modellzugriff.
Refresh erzeugt lediglich vier Profile einschließlich eines späteren
Zugangs-Preflights. Er führt diesen nicht aus. Ein späterer Runner muss seine
eigene Autorität, Arbeitsverzeichnisgrenze und Ergebnisdatei prüfen.

Neue Profile verwenden die native CLI: Claude `--effort`, Copilot bevorzugt
`--reasoning-effort` und verwendet `--effort` nur als erkannte Legacy-Form,
OpenCode verwendet entweder den erkannten Schalter `--variant` oder die von
2.2.5 dokumentierte Form `provider/model#variant`. Die CLI-Hilfe wird in einem begrenzten Kindprozess aus
stdout und stderr erfasst, von ANSI-/Steuerzeichen bereinigt und anschließend
auf ganze Flag-Tokens geprüft. Fehlt bei Copilot beides, bleibt Discovery
blockiert. Der Preflight bittet um `MODEL_READY`; Claude deaktiviert
Tools/MCP und trennt den positionalen Prompt ausdrücklich von der variadischen
MCP-Konfiguration, Copilot begrenzt Tools auf Lesen und hält mit `--silent`
Diagnosebanner von der maschinenlesbaren Standardausgabe fern, OpenCode nutzt `plan`.
Fast-Profile verwenden ebenfalls Plan-/Lesemodi. Implementierungsprofile
verwenden Claude `acceptEdits`, OpenCode `build` beziehungsweise Copilots
bestehende Berechtigungsprüfung. Kein Bypass und keine pauschale Toolfreigabe.
Hersteller-Modi sind **keine Betriebssystem-Sandbox**; insbesondere OpenCode-
Agenten, Plugins, Hooks und lokale Einstellungen müssen vor Ausführung als
eigener Vertrauensbereich geprüft werden. Der Resolver führt diese Profile
nicht aus und installiert keine Isolation.

## Quellen und Prüfgrenzen

- [Claude Agent SDK](https://code.claude.com/docs/en/agent-sdk/typescript)
- [Claude CLI](https://code.claude.com/docs/en/cli-reference)
- [Claude-Modellkennungen](https://code.claude.com/docs/en/model-config)
- [Copilot SDK](https://docs.github.com/en/copilot/how-tos/copilot-sdk/troubleshooting/compatibility)
- [Copilot CLI](https://docs.github.com/en/copilot/reference/copilot-cli-reference/cli-command-reference)
- [OpenCode Server](https://opencode.ai/docs/server/)
- [OpenCode CLI](https://dev.opencode.ai/docs/cli)
- [Tests, Dokumentationsentscheidung und offene Nachweise](adapter-extension-evidence.md)

Ziel: CEFR B2, anwendbare WCAG 2.2 AA. Textbefehle und Fehlergründe tragen die
Information; Farben sind nicht erforderlich. Statische Prüfungen ersetzen keine
Screenreader-, Braille- oder vollständige Barrierefreiheitsabnahme.
