---
description: Ein benanntes Secure-Development-Gate mit begrenzter Evidence-Autorität prüfen
---

# Secure Development Review

## Routing / Routing

Im bestehenden Model-Routing gilt `frontier-reasoning` für die fachliche
Bewertung des benannten Gates. Der Vertrag steht in `model-routing.json`.
Der direkte Validator bleibt eine modellfreie Skriptprüfung; sein Erfolg ist
keine fachliche Bewertung oder menschliche Freigabe. Nur ausdrücklich
autorisierte Review-Evidence darf geändert werden. Fehlende Verträge oder
unpassende lokale Profile blockieren ohne schwächere Ersatzrolle.

*Stable routing uses frontier-reasoning for the named gate's assessment.
The contract is in model-routing.json. The direct validator remains model-free;
its success is neither an assessment nor human approval. Edit only explicitly
authorized review evidence. Missing contracts or unsuitable local profiles
block without a weaker fallback role.*

Syntax:

~~~text
$speckit-secure-development-review <baseline|delta|closure|image-impact> <context-id> <training|mixed|development>
~~~

Validiere genau das benannte Gate im Evidence-Verzeichnis
`docs/security/secure-development/<datum>-<context-id>/`.

Führe vom Projektwurzelverzeichnis auf Windows
`pwsh -NoProfile -File .specify/presets/secure-development-assurance-governance/scripts/validate-secure-development-assurance.ps1`
mit `-Action Review -Gate <gate> -ContextId <context-id> -Mode <mode>` aus.
Führe auf macOS/Linux
`bash .specify/presets/secure-development-assurance-governance/scripts/validate-secure-development-assurance.sh`
mit `review <gate> <context-id> <mode>` aus. Die vollständigen installierten
Pfade dürfen nicht auf das Top-Level-Skriptverzeichnis verkürzt werden.

- `training` und `mixed` benötigen ein Gate-spezifisches Runbook.
- `development` benötigt ein Runbook oder `runbookApplicability: "N/A"` mit
  belastbarer `runbookRationale`.
- `baseline` prüft zusätzlich Manifest, zwölf Checklisten,
  Dokumentversionen, normalisierte Hashbindungen, Sammelband,
  `CL-02-13` und `security-governance >=0.6.1`.
- `closure` prüft die vier unabhängigen menschlichen Entscheidungen.
- `image-impact` prüft alle neun Image-Felder.

Ändere nur ausdrücklich autorisierte Review-Evidence innerhalb des gewählten
Kontexts. Verändere niemals Richtlinie, Checklisten, Baseline-Quellen,
Produktcode, Images, Git-, Remote- oder Providerzustände.

Technische Validierung darf keine Pilotfreigabe, Projektabnahme, allgemeine
Freigabe, C5-Konformität, Testatreife oder Zertifizierung ersetzen.

## English

Run the explicit installed validator path from the project root. Pass the
named gate, context ID, and mode without shortening the preset path.
Review exactly one named gate and context. Preserve the four independent human
decision boundaries. Fail closed on missing evidence, invalid state,
manifest/version/hash drift, expired review, absent runbook or authority, and
prohibited certification claims. Modify only explicitly authorized review
evidence.

## Kontextbindung und Risikotypen / Context Binding and Risk Types

Ab v0.1.3 muss ein Review genau ein Verzeichnis
`<YYYY-MM-DD>-<context-id>` finden. Die ID wird vollständig und mit
Groß-/Kleinschreibung verglichen: `foo` bezeichnet nicht `bar-foo`.
Mehrere datierte Verzeichnisse derselben ID blockieren; es wird nicht
stillschweigend das neueste gewählt. Die `contextId` und `mode` der
ausgewählten Gate-Evidence müssen exakt dem Auftrag entsprechen.
Bei Abweichung: Exitcode 2, keine Erfolgsmeldung, keine Evidence-Änderung.
Vor Wiederholung den gewünschten Kontext und Auftrag fachlich klären;
keine Evidence allein zum Bestehen des Validators umbenennen oder freigeben.

Wenn `acceptedRisks` vorhanden ist, muss es ein JSON-Array sein.
Ein einzelnes Risiko steht in `[{...}]`, nicht in `{...}`.
`null`, boolesche Werte, Zahlen und Zeichenketten sind unzulässig.
Ein leeres Array ist zulässig, jedoch nicht bei `ReadyWithAcceptedRisks`.
Fehlende optionale `acceptedRisks` bleiben bei anderen Ergebnissen zulässig.
Die vorhandenen Pflichtfelder jedes Risikos gelten unverändert.

*From v0.1.3, review requires exactly one dated directory with the full,
case-sensitive context ID. Suffix matches and duplicate dated contexts are
rejected. Evidence contextId and mode must exactly match the request.
Failure returns exit 2 without success output or evidence writes.
Clarify the intended context before retrying; never rename or approve evidence
merely to satisfy validation. When present, acceptedRisks must be a JSON
array; one risk uses [{...}], not {...}. Null, booleans, numbers and strings
are invalid. Empty arrays are allowed except for ReadyWithAcceptedRisks.
Other outcomes may omit the optional field. Risk metadata requirements and
all human decision boundaries remain unchanged.*

## Nutzungshilfe / Reader help

DE: Projektgeführte Sicherheitsnachweise auf Bindung und Konsistenz prüfen; technische Ergebnisse und menschliche Entscheidungen trennen. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/main/docs/user-guide.md).

EN: Check binding and consistency of project-owned security evidence; separate technical results from human decisions. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
