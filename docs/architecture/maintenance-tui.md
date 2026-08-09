# Wartungs-TUI-Architektur / Maintenance TUI Architecture

## Zweck / Purpose

Die Wartungs-TUI ist eine barrierearme Auswahl- und Darstellungsschicht vor
dem vorhandenen Ein-Kommando-Wartungssystem. Sie verschiebt keine
Wartungslogik aus Bash, PowerShell oder dem gemeinsamen Python-Vertragskern.

*The maintenance TUI is an accessible selection and presentation layer in
front of the existing one-command maintenance system. It moves no maintenance
logic out of Bash, PowerShell, or the shared Python contract core.*

## Bausteine / Building Blocks

| Baustein | Verantwortung | Bewusste Grenze |
|---|---|---|
| Bash-/PowerShell-Wrapper | Terminal erkennen, UI auswählen, Cache sicher bereitstellen | Keine zweite Engine nach Prozessstart |
| .NET-10-TUI | Auswahl, Erklärung, Bestätigung, typisierte Argumente, Live-Anzeige | Keine Git-, Paket-, Sync- oder Providerlogik |
| Plain-Assistent | Lineare Auswahl bei fehlender TUI-Fähigkeit | Dieselben Kombinationen und Standardwerte |
| Wartungs-Engine | Freshness, Barrieren, Registry, Propagation, Presets, Toolchain | Keine UI-Abhängigkeit |
| JSONL-Ereignisse | Advisory Live-Status mit Run-ID und Sequenz | Kein Ersatz für Bericht oder Exitcode |
| Atomarer Bericht | Kanonischer, finalisierter Laufnachweis am vorgebundenen Run-Pfad | Muss mit Prozess und einem vorhandenen Abschlussereignis übereinstimmen |
| Inhaltsadressierter Cache | Exakten lokalen TUI-Build wiederverwenden | Keine fremde Plattform, Teilpublikation oder Binärdatei in Git |

## Laufsequenz / Runtime Sequence

```text
Aufruf / invocation
  -> UI-Routing vor Engine-Initialisierung
  -> genau eine validierte Auswahl
  -> erklärender, nicht ausgeführter Befehlsstring
  -> Standard-Nein-Bestätigung für Update
  -> genau ein interner Headless-Prozess
  -> advisory JSONL-Live-Status
  -> vorgebundenen Bericht + optionales Abschlussereignis + Prozess-Exit abgleichen
  -> kanonischen Exitcode unverändert zurückgeben
```

Die textuelle Darstellung ist verbindlich. Tabellen, Farbe und Live-Layout
sind zusätzliche Darstellungen derselben Informationen. Unbekannte
Gesamtmengen bleiben unbekannt; es wird kein Fortschrittsprozentsatz erfunden.

*The textual projection is binding. Tables, color, and live layout are
additional views of the same information. Unknown totals remain unknown; the
UI never invents a completion percentage.*

## Vertrauensgrenzen / Trust Boundaries

1. **Terminaltext:** Fremde Pfade und Meldungen werden vor Spectre-Markup
   maskiert.
2. **Prozessargumente:** Die TUI verwendet
   `ProcessStartInfo.ArgumentList`; der sichtbare Befehl ist nur Erklärung.
3. **Ereignisse:** Jede Zeile wird strikt gegen Schema, Run-ID und Sequenz
   geprüft. Ein Fehler degradiert nur die Darstellung.
4. **Bericht:** Der erwartete Pfad wird vor Prozessstart aus Home-Verzeichnis
   und Run-ID gebildet. Nur eine passende Run-ID und `finalized: true` sind
   kanonisch; es gibt keine Suche nach der neuesten Berichtsdatei.
5. **Cache:** Quellhash, Plattform, Metadaten und vollständige atomare
   Publikation müssen gemeinsam stimmen.
6. **Autorität:** Die UI-Bestätigung erlaubt genau einen lokalen
   Engine-Prozess. Sie erlaubt keine Zielrepository- oder Adminaktion.

## Abbruch und Abschluss / Interruption and Completion

Das erste `Ctrl+C` fordert genau einen kontrollierten Engine-Abbruch an. Weitere
Signale starten keinen zweiten Prozess, senden kein zweites Engine-Signal und
lösen keine automatische Bereinigung aus. Ein ungültiger Eventdatensatz setzt
die Anzeige dauerhaft auf `EVENT_STREAM_DEGRADED`; er verändert weder
Engine-Zustand noch Abschlussbewertung.

Die Schlussansicht wird aus finalisiertem Bericht und Prozess-Exitcode
abgeleitet. Sie hält Mutationsbarriere, Repository-Zählung, Preset-Phase,
Lease-Befund, Bericht, Log und nächste Aktion als linearen, kopierbaren Text
bereit. Fehlt das optionale Abschlussereignis nach einer Event-Degradierung,
bleibt ein gültiger vorgebundener Bericht maßgeblich. Widerspricht ein
vorhandenes Abschlussereignis dem Bericht oder Exitcode, entsteht weiterhin
`RESULT_MISMATCH`. Nicht berichtete Werte bleiben ausdrücklich unbekannt.

*The first `Ctrl+C` requests exactly one controlled engine interruption. Later
signals neither start a second process, send a second engine signal, nor trigger
automatic cleanup. Invalid event input permanently marks the display as
`EVENT_STREAM_DEGRADED` without changing engine state or result evaluation.
The final view derives from the finalized report and process exit and keeps all
reported boundaries and next actions copyable. A missing optional completion
event does not override a valid pre-bound report after event degradation. A
present contradictory completion event still produces `RESULT_MISMATCH`;
unknown values remain unknown.*

## Plattform und Fallback / Platform and Fallback

Unterstützte Cache-IDs sind `macos-arm64`, `macos-x64`, `linux-arm64`,
`linux-x64`, `windows-arm64` und `windows-x64`. Restore und Publish verwenden
die eingecheckten Lockfiles. Bei fehlendem SDK, Buildfehler, fremdem Cache,
`TERM=dumb` oder ungeeignetem Terminal startet vor der Engine der
Plain-Assistent. Nach Engine-Start gibt es keinen Fallback und keinen
automatischen Retry.

*Supported cache IDs cover macOS, Linux, and Windows on arm64 and x64. Restore
and publish use committed lock files. Missing SDK, build failure, foreign
cache, `TERM=dumb`, or unsuitable terminal capability selects the plain
assistant before engine start. No fallback or automatic retry exists after
engine start.*

## Testvertrag / Test Contract

- MSTest und Spectre.Console.Testing prüfen Auswahl, Markup, Layout und
  lineare Textalternative.
- Fake-Prozesse prüfen Exitcodes `0`, `1`, `2`, `3` und `130` ohne echte
  Wartung.
- Python-Fixtures prüfen Bash-/PowerShell-Routing und den Eventwriter.
- Eine isolierte Home-Runtime-Fixture prüft null, ein und mehrere Argumente
  unter `/bin/bash` einschließlich macOS-System-Bash 3.2.
- macOS, Ubuntu und Windows führen Locked Restore, Build und Tests gegen den
  exakten PR-Head aus.

<!-- EN: docs/architecture/maintenance-tui.md
[DE-Zusammenfassung: Architektur, Grenzen, Fallbacks und Testvertrag der Wartungs-TUI.]
-->
