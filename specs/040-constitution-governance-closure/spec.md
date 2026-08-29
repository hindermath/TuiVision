# Feature Specification: Constitution Governance Closure

**Branch**: `040-constitution-governance-closure`

**Status**: Accepted for local implementation
**Binding intake**: `requirements/intakes/active/Lastenheft_Constitution_Change.md`

## Zweck / Purpose

Dieses evidence-only Feature revalidiert CC-01 bis CC-07 gegen den aktuellen
Repository-Stand. Bereits vollständig erfüllte Governance wird nicht
kosmetisch umgeschrieben. Nur ein reproduzierbarer Gap dürfte eine Änderung an
Constitution, Templates oder Agent-Guidance auslösen.

*This evidence-only feature revalidates CC-01 through CC-07 against the current
repository. Governance that is already complete is not rewritten for cosmetic
reasons. Only a reproducible gap could trigger changes to the constitution,
templates, or agent guidance.*

## Requirements / Anforderungen

- **FR-001**: Jede Intake-Anforderung erhält genau eine Klassifikation:
  `Applicable`, `AlreadySatisfied`, `N/A`, `Open` oder `FollowUp`.
- **FR-002**: Constitution, Templates und die vier gepflegten Agent-Flächen
  werden read-only auf bilinguale CEFR-B2-, A11Y-, XML-Dokumentations-,
  Kommentar-, Test-first- und Synchronisationsregeln geprüft.
- **FR-003**: Ohne Gap bleiben alle Governance-Flächen unverändert.
- **FR-004**: Bash-/PowerShell-Homogeneity, Spec-Kit-Check, Secret-Scan und
  Diff-Scope müssen grün sein.
- **FR-005**: Der Abschluss dokumentiert, dass DocFX/Axe nicht ausgelöst ist,
  weil weder API/XML noch DocFX-Navigation oder publizierte Dokumentation
  geändert wird.

## Success Criteria / Erfolgskriterien

- CC-01 bis CC-07 sind lückenlos und evidenzgebunden klassifiziert.
- Kein fachlicher oder Governance-Gap bleibt offen.
- Constitution, Templates und Agent-Guidance zeigen null Feature-Delta.
- Der Nachfolger `Lastenheft_Source-Reference-Policy.md` kann seriell beginnen.

## Nicht-Ziele / Non-goals

Keine Framework-, Beispiel-, API-, Runtime-, Paket-, Dependency- oder
generierte Dokumentationsänderung; keine kosmetische Governance-Umschreibung.
