# GSDB Self-Assessment / GSDB Preflight

**Projekt / Project:** TuiVision
**Datum / Date:** 2026-07-01
**Repository:** `/Users/thorstenhindermann/RiderProjects/TuiVision`
**Primaersprache / Primary language:** C#

Dieses Dokument ist ein Preflight-Bericht. Es startet keinen Spec-Kit-Lauf und
ist keine formale Freigabe.

*This document is a preflight report. It does not start a Spec Kit run and is
not a formal approval.*

| Status | Pruefpunkt / Check | Evidenzpfad / Evidence path | Begruendung / Rationale | Follow-up |
|---|---|---|---|---|
| OK | GSDB README | `docs/secure-development/README.md` | vorhanden | - |
| OK | GSDB Richtlinie | `docs/secure-development/Richtlinie_Sichere-Entwicklung.md` | vorhanden | - |
| OK | GSDB Checklistensammelband | `docs/secure-development/Checklistensammelband_Sichere-Entwicklung.md` | vorhanden | - |
| OK | GSDB mitgeltende Dokumente | `docs/secure-development/mitgeltende-dokumente/README.md` | vorhanden | - |
| OK | GSDB Preset-Verzahnung | `docs/secure-development/mitgeltende-dokumente/Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md` | vorhanden | - |
| OK | CL_01 bis CL_12 | `docs/secure-development/checklisten/` | alle 12 Checklisten vorhanden | - |
| OK | MSL-Status | `constitution.md` | Primaersprache ist auf der MSL-Allowlist | - |
| OK | Spec Kit initialisiert | `.specify/` | Spec-Kit-Verzeichnis vorhanden | - |
| OK | Preset security-governance | `.specify/presets/` | per specify preset list nachweisbar | - |
| OK | Preset architecture-governance | `.specify/presets/` | per specify preset list nachweisbar | - |
| OK | Preset isaqb-architecture-governance | `.specify/presets/` | per specify preset list nachweisbar | - |
| OK | Preset a11y-governance | `.specify/presets/` | per specify preset list nachweisbar | - |
| OK | Preset cross-platform-governance | `.specify/presets/` | per specify preset list nachweisbar | - |
| OK | Preset agent-parity-governance | `.specify/presets/` | per specify preset list nachweisbar | - |
| OK | Projektspezifischer Nachweisort | `docs/security/` | Nachweisordner vorhanden | - |
| OK | RL-SE-/Checklist-Selbstpruefungs-Intake | `Lastenheft_RL-SE-Checklist-Selbstpruefung.md` | vorhanden | - |
| OK | Secure-Development-Hardening-Intake | `Lastenheft_Secure-Development-Hardening.md` | vorhanden | - |
| Open | GSDB-Spec-Kit-Intensivpruefungs-Intake | `Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md` | Intake fehlt | durch diesen Checker erzeugen lassen |

## Ergebnis / Result

- Offene Punkte / Open findings: 1
- Naechster Schritt / Next step: `Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md` spaeter manuell mit `/speckit-specify` starten, wenn die intensive Pruefung erfolgen soll.
