# arc42 Abschnitt 8: Sicherheitskonzepte / Security Concepts

**Stand / Current as of**: 2026-07-11
**Projekt / Project**: TuiVision (Level 2)

## Kontext / Context

TuiVision ist ein lokales .NET-10-Terminal-UI-Framework. Die primären
Sicherheitsgrenzen sind Terminaleingabe, Event-/Command-Dispatch,
Datei-/Ressourcenpfade, Serialisierung, generierte Ausgabe, Dependencies,
Repository-Scripts, CI und Agentenwerkzeuge.

*TuiVision is a local .NET 10 terminal UI framework. Its primary security
boundaries are terminal input, event/command dispatch, file/resource paths,
serialization, generated output, dependencies, repository scripts, CI, and
agent tooling.*

## Querschnittskonzepte / Cross-Cutting Concepts

| Prinzip / Principle | TuiVision-Anwendung / TuiVision application | Evidenz / Evidence |
|---|---|---|
| Trust boundaries | Eingaben an Terminal-, Datei-, Ressourcen-, Serialisierungs- und Toolgrenzen validieren. | [threat-model.md](threat-model.md), tests |
| Defense in depth | Managed Speicher, Validierung, sichere Ablehnung und Tests wirken gemeinsam. | Core/Controls/Serialization/Driver tests |
| Least privilege | Keine neuen Runtime-Rechte; Workflows erhalten minimale deklarierte Permissions. | `.github/workflows/` |
| Fail-safe defaults | Malformed/unsupported Input wird sichtbar abgelehnt oder fällt auf sichere lokale Modi zurück. | Negative tests, driver fallbacks |
| Attack surface reduction | Kein Web/API/Auth/Cloud/Database/Runtime-AI-Scope. | Applicability documents |
| Separation of concerns | Core events, Controls, Serialization, Console driver und Compatibility bleiben getrennte Module. | `src/`, `TuiVision.sln` |
| Secure configuration | Keine Credentials in Git; tool-owned/generated output bleibt untracked. | Secret scans, `.gitignore` |
| Supply-chain security | Package review, immutable Actions, local CycloneDX tool and update review. | [supply-chain-evidence.md](supply-chain-evidence.md) |

## Runtime- und Deployment-Sicht / Runtime and Deployment View

Die Runtime bleibt ein lokaler Prozess. Es gibt keine Remote-Identität,
Mandanten-, Service- oder Cloud-Deployment-Grenze. CI und GitHub sind
Entwicklungs-/Lieferketten-Infrastruktur und keine Produkt-Runtime.

## Entscheidungen, Risiken und Debt / Decisions, Risks, and Debt

- Das bestehende Modulmodell bleibt unverändert; Feature 016 führt keine neue
  Architektur ein.
- CycloneDX ist ein lokales Build-/Review-Werkzeug, keine Runtime-Abhängigkeit.
- Release-Provenance, reproduzierbare Builds und vollständige NuGet-Lock-Policy
  bleiben benannte Supply-Chain-Follow-ups.
- Provider-, Sandbox-, Rechts- und Organisationskontrollen bleiben Human-only.

Eine neue S-ADR ist nur erforderlich, wenn eine spätere Änderung Trust
Boundaries, Auth, Krypto, Persistenz, Cloud/Provider oder Release-Architektur
materiell ändert.

*A new S-ADR is required only when a later change materially changes trust
boundaries, auth, crypto, persistence, cloud/provider, or release architecture.*
