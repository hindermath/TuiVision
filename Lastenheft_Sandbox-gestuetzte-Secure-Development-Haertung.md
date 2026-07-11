# Lastenheft: Sandbox-gestuetzte Secure-Development-Haertung

**Dokumenttyp:** Spec-Kit Intake / Lastenheft  
**Status:** Vorbereitung fuer spaeteren Spec-Kit-Lauf, kein gestarteter Lauf  
**Projekt:** TuiVision  
**Zielgruppe:** Fachinformatiker*innen in Ausbildung, Entwickler*innen, Reviewer und KI-Agenten

## Ziel / Goal

Dieses Lastenheft beschreibt einen spaeteren Spec-Kit-Lauf, der prueft, wie dieses Level-2-Repository sicher, nachvollziehbar und ausbildungsgeeignet in oder mit der `absdd-image-sandbox` bearbeitet werden kann. Der Lauf soll die sichere Entwicklung vorbereiten und dokumentieren, aber in diesem Intake noch keine technische Haertung ausfuehren.

*This intake describes a later Spec Kit run that checks how this level-2 repository can be worked on securely, traceably, and in a training-friendly way in or with `absdd-image-sandbox`. The run should prepare and document secure development, but this intake does not execute technical hardening yet.*

## Projektkontext / Project Context

| Feld / Field | Wert / Value |
|---|---|
| Runtime / Language | .NET 10 / C# terminal UI framework and Turbo Vision port |
| Build & Test Baseline | dotnet restore/build/test; MSTest suites; Coverlet coverage gates for core assemblies; dotnet format where configured |
| Docs / A11Y Baseline | DocFX regeneration requires Playwright + axe and lynx-oriented A11Y smoke review |
| Statistics Baseline | Manual conservative 80; C#/.NET Thorsten-Solo 125 unless all agent files justify a deviation |
| Agent Surfaces | AGENTS.md, CLAUDE.md, GEMINI.md, .github/copilot-instructions.md, .github/agents/copilot-instructions.md, Spec-Kit surfaces |

## Eingaben / Inputs

- Zentrale Richtlinie `docs/secure-development/Richtlinie_Sichere-Entwicklung.md`.
- `docs/secure-development/checklisten/CL_12_Agentische-KI-Sandbox.md`.
- `docs/secure-development/mitgeltende-dokumente/Leitlinie_Sichere-Entwicklungs-Sandbox.md`.
- Vorhandenes `Lastenheft_Secure-Development-Hardening.md` dieses Repositories.
- Die sechs Governance-Presets: `security-governance`, `architecture-governance`, `isaqb-architecture-governance`, `a11y-governance`, `cross-platform-governance`, `agent-parity-governance`.
- Sandbox-Kontext `container-images/absdd-image-sandbox`.

## Scope

- Pruefen, welche Projektpfade in die Sandbox gemountet werden duerfen.
- Pruefen, welche Schreibgrenzen fuer KI-Agenten gelten muessen.
- Pruefen, ob Build, Test, Dokumentation und relevante Smoke-Checks in der Sandbox sinnvoll laufen koennen.
- Pruefen, welche Secrets, Tokens, Nutzerprofile, Caches oder lokalen Tooldaten ausserhalb des Repositories bleiben muessen.
- Pruefen, welche SBOM-, Dependency-, Scan- und Review-Nachweise fuer spaetere Haertung erforderlich sind.
- Pruefen, welche `docs/security/`-Nachweise im spaeteren Lauf neu erstellt oder aktualisiert werden muessen.

## Nicht-Ziele / Non-Goals

- Kein Start eines Spec-Kit-Laufs durch dieses Dokument.
- Keine direkte Codeaenderung.
- Keine automatische technische Haertung.
- Kein automatisches Fuellen von `docs/security/`.
- Keine Aenderung am `absdd-image-sandbox`-Image.
- Keine Behauptung, dass alle MSL-Toolchains in der Sandbox bereits vollstaendig vorhanden sind.

## Anforderungen / Requirements

1. Der spaetere Spec-Kit-Lauf dokumentiert `Applicable`, `N/A` oder `Open` fuer alle einschlaegigen Sandbox- und Secure-Development-Pruefpunkte.
2. Der Lauf nennt konkrete Evidenzpfade fuer Mounts, Schreibrechte, Agenten-Konfiguration, Build/Test, SBOM/Dependency-Audit und Review.
3. Der Lauf beschreibt, welche Arbeiten sinnvoll in der Sandbox stattfinden und welche lokal oder in CI bleiben muessen.
4. Der Lauf prueft, ob die Sandbox ausreichend sicher ist, ohne die Arbeitsfaehigkeit fuer Auszubildende und Entwickler*innen unnoetig einzuschraenken.
5. Der Lauf nutzt DE/EN, CEFR B2 und WCAG 2.2 AA fuer nutzerseitige Dokumentation.
6. Offene Punkte werden als Folgeaufgaben dokumentiert, nicht stillschweigend ausgelassen.

## Akzeptanzkriterien / Acceptance Criteria

- `spec.md`, `plan.md` und `tasks.md` eines spaeteren Spec-Kit-Laufs nennen die Sandbox-Anwendbarkeit und Evidenzpfade.
- `Lastenheft_Secure-Development-Hardening.md` bleibt als fachlicher Vorlaeufer erhalten.
- Keine projektspezifischen Secrets oder privaten Host-Pfade werden in versionierte Dateien uebernommen.
- Der Lauf entscheidet nachvollziehbar, ob und wie `absdd-image-sandbox` fuer dieses Projekt genutzt werden kann.
- Falls ein Pruefpunkt nicht greift, steht `N/A` mit kurzer Begruendung im jeweiligen Artefakt.

## Optimaler Spec-Kit Specify Prompt

```text
/speckit-specify Nutze Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md als verbindliche Eingabedatei. Erstelle die Feature-Spezifikation fuer einen Sandbox-gestuetzten Secure-Development-Haertungslauf im Repository TuiVision.

Ziel: Pruefe, wie TuiVision sicher, nachvollziehbar und ausbildungsgeeignet in oder mit der absdd-image-sandbox bearbeitet werden kann. Starte keine Implementierung und fuehre keine technische Haertung aus.

Beruecksichtige:
- Richtlinie Sichere Entwicklung.
- CL_12 Agentische KI in Sandbox-Umgebungen.
- Leitlinie_Sichere-Entwicklungs-Sandbox.md.
- Lastenheft_Secure-Development-Hardening.md dieses Repositories.
- Alle sechs Governance-Presets mit auditfaehigen Applicable/N/A/Open-Entscheidungen.
- Projektkontext: .NET 10 / C# terminal UI framework and Turbo Vision port.
- Build/Test-Baseline: dotnet restore/build/test; MSTest suites; Coverlet coverage gates for core assemblies; dotnet format where configured.
- Dokumentations- und A11Y-Basis: DocFX regeneration requires Playwright + axe and lynx-oriented A11Y smoke review.
- Keine Secrets, privaten Host-Pfade oder lokalen Nutzerprofile in versionierte Dateien uebernehmen.
```