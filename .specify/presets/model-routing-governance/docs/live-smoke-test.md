# Plattformneutraler Model-Routing-Live-Smoke-Test

Der kanonische Live-Test liegt unter `tests/live/`. Er verwendet auf macOS,
Windows und Ubuntu/WSL2 denselben PowerShell-7-Runner. Der Lauf ist bewusst
manuell: Er nutzt vorhandene interaktive Harness-Anmeldungen, kann
Modellkontingent verbrauchen und schreibt temporaere Projekte. Normale CI fuehrt
nur den vollstaendig offline arbeitenden Vertragstest aus.

English: [Platform-neutral live smoke test](live-smoke-test.EN.md).

## Wann ist der Test erforderlich?

Der Test wird nach Aenderungen an Resolver, Bash-Wrapper, Adaptern,
Auswahlpolicy, Profilrendering, Ergebnistransport, SDK-/CLI-Kompatibilitaet oder
der Akzeptanzmatrix wiederholt. Die verbindlichen Pfadmuster stehen in
`tests/live/model-routing-smoke.defaults.json`. Reine Dokumentations- und
historische Evidence-Aenderungen loesen keinen Live-Lauf aus.

## Voraussetzungen und Autoritaet

- PowerShell 7.3 oder neuer, Node.js/npm, Git und .NET SDK 10;
- `agy`, `claude`, `codex`, `copilot` und `opencode` im `PATH`;
- vorhandene interaktive Anmeldung in jedem Harness beziehungsweise Provider;
- ein sauberer Kandidaten-Worktree;
- ausdrueckliche Autoritaet fuer Modellnutzung und temporaere Schreibzugriffe.

API-Key-Umgebungsvariablen werden aus Kindprozessen entfernt. Die gepinnten
Claude- und Copilot-SDKs werden ohne Installationsskripte in ein temporaeres
Verzeichnis geladen. OpenCode verwendet einen temporaer authentifizierten
Loopback-Server. Der Lauf erteilt keine Push-, PR-, Merge-, Release-,
Installations-, Home-Sync- oder Rollout-Autoritaet.

## Aufruf

Strikter Lauf ohne Retry:

```powershell
pwsh -NoProfile -File tests/live/invoke-model-routing-smoke.ps1 -ConfirmLiveRun
```

Ausdruecklich bis zu drei Retries je Harness erlauben:

```powershell
pwsh -NoProfile -File tests/live/invoke-model-routing-smoke.ps1 -ConfirmLiveRun -MaxRetries 3
```

Ohne `-ConfirmLiveRun` blockiert das Skript vor SDK-, Server- oder
Harness-Aktivitaet. Ein Retry verwendet ein frisches temporaeres Git-Repository
und exakt dieselbe Modell-/Providerbindung. Ein erfolgreicher spaeterer Versuch
wird als `PASS_WITH_RETRY`, nicht als unauffaelliges `PASS`, ausgewiesen.

## Versionierte Auswahl und lokale Abweichung

Die Defaults pruefen Claude `opus`/`haiku`, Copilot
`claude-opus-5.5`/`claude-haiku-5.5` und OpenCode
`openai/gpt-6.1-terra`/`openai/gpt-6-luna`. Codex und `agy` bleiben
vollstaendig automatisch. Die IDs sind Akzeptanzerwartungen, keine Zusage fuer
jedes Konto.

Eine abweichende lokale Auswahl wird zum Beispiel als
`tests/live/model-routing-smoke.local.json` gespeichert und explizit uebergeben:

```json
{
  "schemaVersion": "1.0",
  "selections": {
    "Copilot": {
      "frontierModel": "exact-frontier-id",
      "fastModel": "exact-fast-id"
    }
  }
}
```

```powershell
pwsh -NoProfile -File tests/live/invoke-model-routing-smoke.ps1 `
  -ConfirmLiveRun `
  -SelectionFile tests/live/model-routing-smoke.local.json
```

Die lokale Datei ist git-ignoriert. Der Bericht kennzeichnet den Lauf als
`local-override`; er darf nicht stillschweigend als Nachweis der versionierten
Defaults verwendet werden.

## Abnahme und Ergebnis

Alle Harnesses erhalten denselben Auftrag: eine paketfreie .NET-10-App erzeugen,
ausfuehren und exakt `MODEL_ROUTING_OK` pruefen. Claude, Copilot und OpenCode
muessen nach einmaliger exakter Auswahl einen parameterlosen `Status` und
`Refresh` mit `Aligned` und bytegleichem Profil bestehen. Anschliessend prueft
der Runner unabhaengig genau ein C#-Projekt, fehlende `PackageReference`, Build,
Run, Exitcode und Ausgabe.

Das 120-Sekunden-Budget umfasst Discovery, Profilwiederverwendung, Preflight und
Agentenaufgabe je Versuch. Build und Run besitzen jeweils ein separates
30-Sekunden-Limit. Die Anwendung und Paketwiederherstellung erhalten keine
Paketquelle; Providerzugriffe bleiben fuer den Harness erforderlich.

Berichte liegen lokal unter `.test-results/model-routing-smoke/`. Das aktuelle
Berichtsschema `1.1` enthaelt Commit, Plattform, Konfigurationshash, alle
Versuche, Modelle, Reasoning, Dauern, Exitcodes, Ausgabe und Cleanup-Nachweis.
Historische Berichte mit Schema `1.0` bleiben lesbar. Moegliche Endzustaende sind
`PASS`, `PASS_WITH_RETRY`, `FAILED`, `BLOCKED`, `TIMEOUT` und `INFRA_ERROR`.
Die Berichte sind git-ignoriert. Eine Zusammenfassung wird nur nach
ausdruecklicher Autoritaet in Release-Evidence uebernommen.
Der Prozess endet fuer `PASS` und `PASS_WITH_RETRY` mit Exitcode 0, andernfalls
mit Exitcode 1.

### Begrenzte Fehlerdiagnose

Prozessende und das Lesen geerbter Ausgabekanaele teilen sich dasselbe
Zeitbudget. Server-Cleanup wartet hoechstens zwei Sekunden je Schritt.
Nach jedem abgeschlossenen Versuch wird der Bericht atomar auf Platte
geschrieben; ein spaeterer Haenger darf fruehere Ergebnisse nicht verlieren.
Die Inventur oeffnet ausschliesslich regulaere Dateien: Unix-Pipes, Sockets
und symbolische Links werden vor dem Hashen ausgeschlossen.

Temporaerer Stamm, Prozessarbeitsverzeichnis und `PWD` verwenden physisch
aufgeloeste Pfade. Insbesondere duerfen `/var` und `/private/var` unter macOS
nicht zwischen Builds wechseln, weil dabei .NET-Runtime-Dateien verloren
gehen koennen. `agy` erhaelt neben `command(*)` nur fuer den temporaeren
Worktree `write_file(<physischer Pfad>)`; diese Freigabe schliesst Lesen ein
und gilt ausschliesslich fuer die Sitzung.

Der modellfreie Regressionstest dafuer lautet:
`pwsh -NoProfile -File tests/test-live-smoke-dotnet.ps1` (.NET SDK 10).

Bei einem fehlgeschlagenen Versuch sichert der Runner vor dem Cleanup
redigiertes stdout/stderr fuer Preflight, Agent, Build und Run, das geparste
boolesche Agentenergebnis sowie ein relatives Projektinventar. Fuer `.csproj`,
`Program.cs`, `runtimeconfig.json` und `deps.json` wird begrenzter Textinhalt
aufgenommen. Andere Dateien erhalten nur Pfad, Groesse und SHA-256; `.git`,
`.specify`, `obj` und uebrige `bin`-Artefakte bleiben ausgeschlossen.

Bearer-/Basic-Autorisierung, API-Key-, Token- und Passwortmuster, bekannte
temporaere Zugangswerte sowie Home-, Repository- und Temp-Pfade werden
redigiert oder normalisiert. stdout/stderr sind auf jeweils 16 KiB,
ausgewaehlte Textdateien auf 32 KiB und die gesamte Diagnose eines Versuchs auf
128 KiB begrenzt. Trunkierung wird sichtbar markiert. Vollstaendige
Umgebungen, Anbieterlogs, Gespraechsverlaeufe und temporaere Worktrees werden
nicht aufbewahrt.

Bei Exitcode 131 zuerst `diagnostics.processes.run.stderr`, die erfasste
.NET-Version/Runtime-Liste und die Runtimeconfig-/Deps-Metadaten vergleichen.
Bei `expected one project, found 0` zuerst Agent-stderr, `agentResult` und das
Projektinventar pruefen. Ein Diagnosebericht ist noch keine Reparatur- oder
Delivery-Autoritaet.

## Dokumentationsentscheidung

`UpdateRequired`, Owner Thorsten Hindermann. Kanonische Quellen sind Runner,
Akzeptanzmatrix, dieses Dokument und der englische Sprachpartner. Zielgruppe
sind Preset-Betreibende und Maintainer. Distribution: Preset-Quelle; kein
Home-Sync. Neu bewerten bei SDK-, CLI-, Provider-, Modell- oder
Authentifizierungsdrift.
