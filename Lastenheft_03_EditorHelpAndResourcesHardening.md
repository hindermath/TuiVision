# Lastenheft: Editor-, Hilfe- und Ressourcen-Haertung fuer Beispielwelle 3

**Dokument-Status:** Entwurf
**Erstellt:** 2026-03-29
**Betrifft:** `src/TuiVision.Controls/`, `src/TuiVision.Serialization/`, `tests/TuiVision.Controls.Tests/`, `tests/TuiVision.Serialization.Tests/`, `tests/TuiVision.Examples.SmokeTests/`
**Empfohlene Prioritaet:** jetzt anlegen, aber erst nach Wave-2-Basis abarbeiten

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Mit `specs/004-editor-file-help-streams/` existiert bereits ein breites
Planungsfundament fuer Editor-, Datei-, Hilfe- und Stream-Themen. Fuer die
spaetere Beispielwelle 3 reicht ein breiter Phasenplan jedoch nicht allein aus:
Die Beispiele `tvedit`, `bhelp`, `helpdemo`, `tvhc` und `i18n` pruefen die
Anwendungsreife dieser Komponenten unter realen Nutzungsablaeufen.

`specs/004-editor-file-help-streams/` already provides a broad planning base
for editor, file, help, and stream topics. For later wave 3, however, a broad
phase plan alone is not enough: the examples `tvedit`, `bhelp`, `helpdemo`,
`tvhc`, and `i18n` validate whether these components are application-ready in
real usage flows.

Das Risiko besteht weniger in fehlenden Typnamen als in zu duennen
End-to-End-Vertraegen: Editorfenster, Dateidialoge, Hilfe-Navigation,
Ressourcen-Lookup, Compiler-Pipeline und Fehlersignale muessen nicht nur
isoliert existieren, sondern gemeinsam stabil zusammenspielen.

The risk here is less about missing type names and more about thin end-to-end
contracts: editor windows, file dialogs, help navigation, resource lookup,
compiler pipeline, and failure signalling must not only exist in isolation but
also work together consistently.

---

## 2. Betroffene Beispiele / Affected Examples

- `bhelp`
- `helpdemo`
- `i18n`
- `tvedit`
- `tvhc`

---

## 3. Ziele / Goals

- Die bestehende Phase-6-Planung auf Beispielreife zuschneiden.
- End-to-End-Flows vor Beispielcode absichern.
- Harte Fehlerfaelle explizit und reviewbar behandeln.

- Translate the existing phase-6 planning into example readiness.
- Secure end-to-end flows before example code is written.
- Handle hard failure paths explicitly and reviewably.

---

## 4. Anforderungen / Requirements

### R-01: 004 bleibt die fachliche Basis, dieses Dokument ist eine Haertungsschicht

Dieses Lastenheft ersetzt Feature 004 nicht. Es legt nur fest, welche
End-to-End-Vertraege vor dem Start der Wave-3-Beispiele sichtbar gehaertet sein
muessen.

This requirements document does not replace feature 004. It only defines which
end-to-end contracts must be visibly hardened before the wave-3 examples begin.

### R-02: Editorfluesse muessen als Anwendungspfad pruefbar sein

`TEditor`, `TMemo`, `TFileEditor` und `TEditWindow` muessen Datei oeffnen,
bearbeiten, suchen/ersetzen, speichern, Modified-State, Safe-Close und
Ueberschreiben-nach-Fremdaenderung als zusammenhaengenden Anwendungspfad
unterstuetzen.

`TEditor`, `TMemo`, `TFileEditor`, and `TEditWindow` must support opening,
editing, search/replace, saving, modified-state handling, safe close, and
overwrite-after-external-change as one coherent application path.

### R-03: Hilfe-Navigation braucht mehr als Themenanzeige

`THelpViewer`, `THelpWindow`, `THelpFile`, `THelpTopic` und `THelpIndex`
muessen Kontextsuche, Cross-Reference-Navigation und Fallback-Inhalte so
abbilden, dass `bhelp` und `helpdemo` ohne lokale Sonderlogik darauf aufsetzen
koennen.

`THelpViewer`, `THelpWindow`, `THelpFile`, `THelpTopic`, and `THelpIndex` must
cover context lookup, cross-reference navigation, and fallback content so
`bhelp` and `helpdemo` can build on them without local special handling.

### R-04: Help-Compiler und Ressourcenpfad muessen bewusst verbunden werden

`tvhc` verlangt einen reviewbaren Uebersetzungspfad von Quellbeschreibung zu
binaerer Hilfedatei oder aequivalenter persistierter Struktur. Diese Pipeline
muss denselben Ressourcen- und Typregistrierungsvertrag benutzen wie die
Laufzeit.

`tvhc` requires a reviewable translation path from source description to binary
help file or an equivalent persisted structure. That pipeline must use the same
resource and type-registration contract as the runtime.

### R-05: I18n und Ressourcen-Lookup duerfen keine losen Nebenthemen bleiben

`i18n` benoetigt klar definierte Lookup-Semantik fuer Ressourcennamen,
Sprachvarianten, Fallback und Fehlermeldungen. Diese Semantik darf nicht erst
im Beispielcode erfunden werden.

`i18n` needs a clearly defined lookup semantics for resource names, language
variants, fallback, and error signalling. That semantics must not be invented
inside the example code.

### R-06: Fehlerfaelle muessen fuer Beispielbetrieb explizit bleiben

Trunkierte Streams, unbekannte Typen, ungueltige Cross-References,
Zykluserkennung, trailing data und fehlende Ressourcen muessen als sichtbare,
testbare Fehlerfaelle beschrieben bleiben. Wave-3-Beispiele duerfen diese
Fehler nur darstellen, nicht verdecken.

Truncated streams, unknown types, invalid cross-references, cycle detection,
trailing data, and missing resources must remain visible, testable failures.
Wave-3 examples may present these failures, but must not hide them.

---

## 5. Nicht im Scope / Out of Scope

- Die unmittelbare Wave-2-Widget- und Dialogreife
- Vollstaendige Terminalemulation und Zeichensatztreiber
- Neue Anschlusswellen aus `TVDEMOS/` oder `TVFM/`

- Immediate wave-2 widget and dialog readiness
- Full terminal emulation and charset drivers
- New follow-on waves from `TVDEMOS/` or `TVFM/`

---

## 6. Akzeptanzkriterien / Acceptance Criteria

- Vor dem ersten Wave-3-Beispiel existieren gruene Tests fuer Editor-, Hilfe-,
  Compiler- und Ressourcen-End-to-End-Fluesse.
- `tvedit`, `bhelp`, `helpdemo`, `tvhc` und `i18n` koennen als relativ duenne
  Anwendungsports geplant werden.
- Das Dokument bleibt explizit an `specs/004-editor-file-help-streams/`
  rueckgebunden statt eine konkurrierende Parallelplanung aufzubauen.

- Before the first wave-3 example starts, green tests exist for end-to-end
  editor, help, compiler, and resource flows.
- `tvedit`, `bhelp`, `helpdemo`, `tvhc`, and `i18n` can be planned as
  comparatively thin application ports.
- The document remains explicitly tied back to
  `specs/004-editor-file-help-streams/` instead of creating a competing
  parallel plan.
