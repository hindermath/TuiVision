# Retrospektiven autonomer Spec-Kit-Läufe

Dieses Ledger sammelt nur Erkenntnisse, die den autonomen Ablauf verbessern.
Feature-Fakten bleiben in der jeweiligen `pr-evidence.md`. Eine Regel wird erst
projektweit verallgemeinert, wenn sie wiederholt belegt ist. Fehler bei
Korrektheit, Sicherheit oder Evidence dürfen sofort korrigiert werden.

This ledger contains only findings that improve autonomous delivery. Feature
facts stay in the related `pr-evidence.md`. A preference becomes a project-wide
rule only after repeated evidence. Correctness, security, and evidence defects
may be corrected immediately.

## Entscheidungsmodell / Decision Model

| Entscheidung | Bedeutung |
|---|---|
| `FeatureSpecific` | Bleibt beim betroffenen Feature |
| `RunbookClarification` | Präzisiert einen bestehenden Ablaufvertrag |
| `SkillCorrection` | Korrigiert die ausführbare Agenten-Orchestrierung |
| `TemplateCorrection` | Verhindert den Fehler bei neuen Artefakten |
| `AgentPolicyCorrection` | Muss auf allen gepflegten Agentenflächen gelten |
| `ValidationAutomation` | Benötigt einen deterministischen automatisierten Nachweis |
| `PresetFollowUp` | Ist potenziell projektübergreifend und wird Home Baseline übergeben |
| `NoPromotion` | Liefert keine belastbare allgemeine Regel |

## 018 Editor, Help and Resources Hardening

**Feature:** `018-editor-help-resources-hardening`  
**Feature-PR:** [#42](https://github.com/hindermath/TuiVision/pull/42)  
**Closeout-PR:** [#43](https://github.com/hindermath/TuiVision/pull/43)

| Beobachtung | Entscheidung | Umsetzung oder Grenze |
|---|---|---|
| Vier Remote-Tasks nannten keinen konkreten Evidence-Pfad. Analyze erkannte dadurch eine echte Resume- und Abnahmelücke. | `SkillCorrection`, `TemplateCorrection`, `AgentPolicyCorrection`, `PresetFollowUp` | Runbook, Skill, Task-Template und fünf Agentenflächen verlangen jetzt den exakten Repository-Evidence-Pfad. Diese Evidence-Korrektur wird sofort übernommen. |
| Negative Serialization-Grenzen wurden in zwei fokussierten Rot-Grün-Zyklen entdeckt. Beide Zyklen fanden echte Defekte, erhöhten aber den Build-Counter mehrfach. | `PresetFollowUp` mit `ObserveAgain` | Noch keine allgemeine Bündelungsregel. Feature 019 muss zeigen, ob eine vollständige projektlokale Red-Matrix vor dem ersten Green sicher und effizienter ist. |
| Copilot konnte wegen Nutzerquota nicht reviewen; alle Pflichtchecks, Claude und GraphQL-Threads waren grün. Nur Human Approval blockierte. | `RunbookClarification`, keine neue Regel | Das bestehende Runbook behandelt fehlende Reviewer korrekt als fehlenden Review und begrenzt den Admin-Bypass auf die offene Human-Approval-Regel. |
| Post-Merge-Fakten konnten erst nach dem Feature-Merge wahrheitsgemäß feststehen. | `NoPromotion` | Der vorhandene Evidence-only-Closeout-Vertrag funktionierte wie vorgesehen; PR #43 blieb nicht leer und änderte keine Runtime. |

### Nächster Prüfschritt / Next Check

Feature 019 prüft erneut, ob Remote-Tasks vollständige Evidence-Pfade tragen
und ob ein gebündelter Red-Proof ohne Verlust der test-first Nachvollziehbarkeit
weniger Build-Zyklen benötigt. Erst dann darf die Effizienzpräferenz zum
allgemeinen Ablaufvertrag werden.

Feature 019 rechecks exact evidence paths on remote tasks and whether a grouped
red proof can reduce build cycles without weakening test-first traceability.
Only repeated evidence may promote that efficiency preference.
