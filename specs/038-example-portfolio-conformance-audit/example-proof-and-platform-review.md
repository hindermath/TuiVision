# Proof- und Plattformreview / Proof and Platform Review

Kanonische Wahrheit: [example-portfolio-audit.json](example-portfolio-audit.json).

Alle 37 Zeilen besitzen ausführbare Smoke-Evidence für echten
`app.Run()`- oder gleichwertigen Application-Loop, konkreten Zustand,
View-Identität, sichtbare Textzellen, Status und `Help -> Description`.
Risikoangemessene Negativpfade belegen Datei-/Safe-Close-, Help-/Resource-,
Unicode-/Charset-/Font-, Terminal-, Maus- und Small-Terminal-Grenzen.

*All 37 rows have executable smoke evidence for a real application loop,
concrete state, view identity, rendered text cells, status, and Help ->
Description. Risk-proportional negative paths cover the relevant boundaries.*

| Bereich / Area | Aktuelle Evidence / Current evidence | Ehrliche Grenze / Honest boundary |
|---|---|---|
| macOS | Lokaler akzeptierter Vorgänger- und später exakter Feature-038-Lauf | Aktuelle lokale Plattform |
| Windows | Akzeptierte Vorgängerevidence | Kein neuer Feature-038-Remote-Claim |
| Linux | Akzeptierte Vorgängerevidence | Kein neuer Feature-038-Remote-Claim |
| WSL / Terminal | Dokumentierte Capability-/Fallbackgrenzen | Nicht als nativ oder aktuell ausgeführt umgedeutet |
| Dateipfade | Source-controlled Fixtures oder test-eigene Temp-Verzeichnisse | Keine beliebigen Nutzerdaten |
| Externe Prozesse | Nicht Teil der Proof-Pfade | Kein Shell-/PTY-/Service-Start |

Darum ist `PlatformStatus=IntentionalDeviation` in allen 37 Zeilen ehrlich,
bis die spätere Delivery-Phase exakte Remote-Head-Evidence erzeugt. Diese
Grenze ist kein Produktdefekt.

*PlatformStatus remains an intentional deviation until the later delivery
phase produces exact-head remote evidence. This is not a product defect.*
