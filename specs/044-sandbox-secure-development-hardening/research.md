# Research: Sandbox Secure Development Hardening

## Bestandsaufnahme / Inventory

- TuiVision verwendet .NET 10 und C# als speichersichere Primärsprache. Der
  Repository-Scope ist Level 2; die Produkt-, Test-, Dokumentations- und
  Governancepfade sind bereits über CI und Feature 016 abgesichert.
- Feature 016 führt alle 157 Secure-Development-Kontrollen. Die CL-12-Zeilen
  markieren technische Repositorykontrollen überwiegend als erfüllt und
  formelle Freigabe, Provider, Netzwerk und Lifecycle als `Open`.
- Die lokale read-only Vergleichskopie von `hindermath/absdd-image-sandbox`
  ist sauber und entspricht `origin/main` auf Commit
  `7adaeac18ca259726468a2fe1d1fd028b895e09c`.
- Der Sandbox-Stack verwendet ein digest-gepinntes .NET-10-Basisimage,
  nicht privilegierte Laufzeit, `no-new-privileges`, `cap_drop: ALL`, getrennte
  Agenten-Volumes, read-only .NET-Konfiguration und gepinnte Toolversionen.
- `compose.yml` kann über `RIDER_PROJECTS_DIR` einen breiten Projekt-Root
  mounten. Für TuiVision muss diese Rolle auf genau den gewählten Checkout
  zeigen; ein allgemeiner Rider-Projektordner wäre für den hier geprüften
  Auftrag zu breit.
- Der Container besitzt bewusst freien Egress. Codex schränkt Netzwerk im
  workspace-write-Modus zusätzlich ein, doch die allgemeine Compose-Grenze ist
  keine technische Allowlist.
- Die formelle Sandbox-Freigabe, Datenklassifikation, Providerfreigabe und
  Ablaufdaten sind im Sandbox-Repository weiterhin human-owned und `Open`.

## Referenzbindung / Reference Binding

| Artefakt / Artefact | SHA-256 |
|---|---|
| Sandbox `Dockerfile` | `93028759aed4b87cc8989cdf7dc0650515af33ff8b74bbfdcc90989964db57ef` |
| Sandbox `compose.yml` | `e54d5335d21b23fd8628ff9a6c184b17e8b9e70b47110c27ce2c75c74b878d3a` |
| Sandbox `README.md` | `5dd0ab7f091eeb7cc9da9129dc692172466b1046a586e7a8dd997eb763396498` |
| Sandbox Toolchain-Smoke | `09c5cd78f6939633366d8fe0f7a27cfa0a520ef13e9b8a8948d3e4023f977072` |

Diese Hashes belegen die geprüfte Referenz, nicht eine TuiVision-Abhängigkeit.
Die Sandbox wird weder vendort noch als Paket eingebunden.

*These hashes bind the reviewed reference; they do not create a TuiVision
dependency. The sandbox is neither vendored nor added as a package.*

## Entscheidungen / Decisions

### D1 - `ConditionallyUsable` statt pauschaler Freigabe

TuiVision kann in der Sandbox für nicht sensible Entwicklungs- und
Lernaufgaben verwendet werden, wenn genau der TuiVision-Checkout gemountet,
Agentenzustand getrennt und Secrets nur über genehmigte lokale Mechanismen
bereitgestellt werden. Formelle Sandbox-, Provider- und Datenfreigaben bleiben
vor echter Nutzung erforderlich.

### D2 - Portable Rollen statt Hostpfade

Evidence verwendet Rollen wie `TuiVisionCheckout`, `SandboxCheckout`,
`AgentStateVolume`, `BuildCacheVolume` und `AuditMetadataDirectory`. Absolute
Pfade unter einem persönlichen Home-Verzeichnis sind lokale Konfiguration und
werden nicht versioniert.

### D3 - Drei Evidence-Stufen

`StaticVerified`, `PracticallyVerified` und `PlatformVerified` bleiben getrennt.
Konfigurations- und Hashprüfungen reichen für `StaticVerified`. Ein echter
Containerlauf oder ein CI-Lauf darf nur mit eigener Evidence höher eingestuft
werden.

### D4 - Strukturvalidator mit enger Proof-Grenze

Ein kleiner Python-Kern validiert die kanonische JSON-Evidence. Bash und
PowerShell sind gleichwertige Einstiegspunkte. Der Validator prüft Werte,
Kardinalität, Pflichtfelder, portable Pfade und Widersprüche. Er bestätigt
nicht, dass eine fachliche Aussage wahr oder eine Organisation freigegeben ist.

### D5 - Keine Änderung gemeinsamer Agent-Regeln

Die vorhandenen Agenten- und Secret-Regeln decken die Feature-Grenze bereits
ab. Feature 044 dokumentiert `NoUpdateRequired`; Agent-Guidance und
`.specify/templates/` bleiben unverändert.

## Verworfen / Rejected

- Breiter Mount des gesamten persönlichen Projektordners: unnötige Lesefläche.
- Speicherung von `.env`, Token-, Provider- oder Home-Pfaden in Evidence:
  verletzt Secret- und Portabilitätsgrenzen.
- Änderung des externen Sandbox-Repositories: nicht Teil dieses Features.
- Vollständiger Image-Neubau als Startbedingung: unverhältnismäßig für die
  TuiVision-Anwendbarkeitsprüfung; reale Image-Evidence bleibt getrennt.
- Erneute Bewertung aller 157 Kontrollen: Feature 016 bleibt die Baseline;
  Feature 044 fokussiert CL-12 und ausgelöste Supply-Chain-Grenzen.
