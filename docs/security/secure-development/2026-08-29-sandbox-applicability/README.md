# Sandbox-Anwendbarkeit für TuiVision / Sandbox Applicability for TuiVision

## Ergebnis / Result

**`ConditionallyUsable`**: Die geprüfte `absdd-image-sandbox` bietet eine
brauchbare technische Grundlage für nicht sensible TuiVision-Lern- und
Entwicklungsarbeit. Vor einer echten Sitzung müssen jedoch der schreibbare
Mount auf genau den ausgewählten TuiVision-Checkout begrenzt und die aktuellen
Freigaben für Datenklasse, Provider und Netzrisiko bestätigt werden.

*The reviewed `absdd-image-sandbox` provides a useful technical baseline for
non-sensitive TuiVision learning and development work. Before a real session,
limit the writable mount to the selected TuiVision checkout and confirm current
approval for the data class, providers, and network risk.*

## Nächste sichere Aktion / Next Safe Action

Prüfe vor dem Start die lokale Compose-Konfiguration, ohne ihren Inhalt in ein
Log zu kopieren. Der Projekt-Mount darf nur den gewählten TuiVision-Checkout
enthalten. Melde keinen Agenten an, bevor Owner oder zuständige Security-Rolle
Datenklasse, Provider, Region, Aufbewahrung und freien Egress akzeptiert haben.

*Review local Compose configuration before startup without copying its content
into a log. The project mount may contain only the selected TuiVision checkout.
Do not sign an agent in until the owner or responsible security role accepts
the data class, provider, region, retention, and unrestricted egress.*

## Was bereits belegt ist / What Is Already Evidenced

- digest-gepinntes .NET-10-Basisimage und gepinnte Werkzeugversionen;
- nicht privilegierter Container-User, `no-new-privileges` und alle
  Linux-Capabilities entfernt;
- getrennte Volumes für Agentenzustand und Buildausgabe;
- read-only Build-Konfiguration, Repository-Secret-Scans und temporäre SBOM;
- vollständiger Spec-Kit-, Intake-, Routing- und Autonomous-Workflow.

*Evidence already covers the pinned base image and tools, non-privileged
runtime hardening, separate state and build volumes, read-only build
configuration, repository secret scans, temporary SBOM, and the complete
Spec Kit governance workflow.*

## Was offen bleibt / What Remains Open

- formelle Sandbox- und Datenklassifikationsfreigabe mit Ablaufdatum;
- Provider-, Modell-, Konto-, Region-, Retention- und Lizenzentscheidung;
- technischer Egress-Filter oder erneuerte Annahme des freien Egress-Risikos;
- aktueller praktischer Image-Lauf sowie Linux-, macOS- und Windows-/WSL-
  Plattformnachweise;
- unabhängiges menschliches Review, soweit die Organisation es verlangt.

*Formal approval, provider and data decisions, egress control, current image
and platform execution, and required independent human review remain open.*

## Evidence lesen / Read the Evidence

- Kanonische Assessment-Daten: `docs/security/secure-development/2026-08-29-sandbox-applicability/assessment.json`
- [Mount- und Schreibgrenzen](mount-policy.md)
- [Ausführungs- und Proof-Matrix](execution-matrix.md)
- Feature-Laufnachweis: `specs/044-sandbox-secure-development-hardening/pr-evidence.md`

Ein `Open` ist kein bestandener Nachweis. Der JSON-Validator prüft nur Struktur
und innere Konsistenz; er erteilt keine Freigabe.

*An `Open` item is not a passing proof. The JSON validator checks only
structure and internal consistency; it grants no approval.*
