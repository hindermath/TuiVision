# Lastenheft: Barrierefreiheits-Fundament TuiVision (A11Y Framework Layer)

**Dokument-Status:** Spec-Kit-Eingabedatei, bereit fuer `/speckit-specify`
**Erstellt:** 2026-03-31
**Betrifft:** `src/TuiVision.Core/`, `src/TuiVision.Controls/`,
`src/TuiVision.Drivers.Console/`, `tests/TuiVision.Core.Tests/`,
`tests/TuiVision.Controls.Tests/`, `tests/web-a11y/`
**Grundlage:** `docs/tui-a11y-assessment.md` im RiderProjects-Workspace

---

## ⏰ Empfohlener Durchführungszeitraum / Recommended timing

**Deutsch:**
Dieses Lastenheft soll **nach dem Abschluss von Welle 4** (alle 25 Original-Turbo-Vision-Beispiele
portiert und per Smoke-Test abgenommen) und **vor dem Start von Welle 5** (TP7-Demos aus `TVDEMOS/`)
umgesetzt werden.

Begründung: Erst nach Welle 4 sind alle Framework-Primitive (TView, TEvent, TDriver, Steuerelemente,
Dialoge, Editor, Terminal-Emulation) vollständig und stabil. Ein vorher eingeführtes
`IAccessibleWidget`-Interface würde durch spätere Portierungsarbeiten riskieren, mehrfach
gebrochen zu werden. Nach Welle 4 ist die Interface-Stabilität gesichert und die
TP7-Demos aus Welle 5 können von Beginn an auf dem A11Y-Fundament aufsetzen.

**Pflichtenheft-Integration:** Die Anforderungen aus diesem Lastenheft sollen nach Umsetzung
als neue `PF-A11Y-*` Einträge in `Pflichtenheft.md` eingetragen werden, mit
Reihenfolgehinweis: „nach Welle 4, vor Welle 5".

*This requirements document should be implemented **after Wave 4 is complete** (all 25 original
Turbo Vision examples ported and smoke-tested) and **before Wave 5 starts** (TP7 demos from
`TVDEMOS/`). Rationale: all framework primitives are stable after Wave 4; introducing an
`IAccessibleWidget` interface earlier risks repeated breakage during porting work. After Wave 4,
interface stability is guaranteed and Wave 5 TP7 demos can build on the A11Y foundation from the start.*

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

TuiVision portiert Turbo Vision 2.0.3 — eine Bibliothek, die 1991 für DOS entwickelt wurde,
also **vor allen modernen Accessibility-Standards**. Der C#-Port erbt dieses Modell:
rein visuelles Rendering über ANSI-Escape-Codes, keine Semantik-Schicht, kein
Accessibility-Tree.

Das Projektprinzip `Programmierung #include<everyone>` verpflichtet das Projekt,
Barrierefreiheit nicht als nachträglichen Zusatz zu behandeln, sondern als
bewusste Architektur-Entscheidung — auch wenn Terminal-Anwendungen keine
vollständige WCAG-2.2-Konformität wie Web-Anwendungen erreichen können.

TuiVision already provides the strongest A11Y practice in this workspace through
its Playwright+axe testing for generated documentation HTML. This document extends
that practice to the framework layer itself.

*TuiVision ports Turbo Vision 2.0.3 — a library designed in 1991 for DOS, before any modern
accessibility standards. The C# port inherits this model: purely visual ANSI rendering, no semantic
layer, no accessibility tree. The `Programmierung #include<everyone>` principle requires treating
accessibility as an architectural decision, not an afterthought.*

---

## 2. Ziele / Goals

- Ein Minimum-Viable-A11Y-Fundament schaffen, auf dem TP7-Demos und spätere
  Framework-Erweiterungen aufbauen können.
- Alle Informationen, die nur visuell vermittelt werden (Farbe, Cursor-Position),
  zusätzlich als maschinenlesbare Text-Repräsentation verfügbar machen.
- Vollständige Tastaturnavigation ohne Maus sicherstellen und testbar machen.
- Die bereits vorhandenen Playwright+axe-Tests für Dokumentations-HTML in
  die CI/CD-Pipeline integrieren (aktuell: manuell).

*Create a minimum-viable A11Y foundation that TP7 demos and future framework extensions can build
on. Make all visually-only information available as machine-readable text. Ensure and test complete
keyboard navigation without a mouse. Integrate the existing Playwright+axe documentation tests into CI.*

---

## 3. Anforderungen / Requirements

### R-A11Y-TV-01: `IAccessibleWidget`-Interface im Framework-Kern

Das Framework muss ein `IAccessibleWidget`-Interface in `TuiVision.Core` einführen,
das Steuerelemente mit einer maschinenlesbaren Textbeschreibung ausstatten kann.
Das Interface ist bewusst minimal gehalten — es muss keine Screen-Reader-API
implementieren, sondern nur die Voraussetzung schaffen.

Mindest-Interface:

```csharp
/// <summary>
/// Stellt barrierefreie Metadaten für ein TuiVision-Steuerelement bereit.
/// Provides accessible metadata for a TuiVision control.
/// </summary>
public interface IAccessibleWidget
{
    /// <summary>Zugängliche Bezeichnung / Accessible label.</summary>
    string? AccessibleLabel { get; }

    /// <summary>Kurze Beschreibung der Funktion / Short description of the function.</summary>
    string? AccessibleDescription { get; }

    /// <summary>Gibt an, ob das Steuerelement den Tastaturfokus aufnehmen kann.</summary>
    bool CanReceiveFocus { get; }
}
```

Das Interface ist **opt-in**: bestehende Steuerelemente in `TuiVision.Controls`
müssen es nicht sofort implementieren. Es soll jedoch als Standard-Muster
in allen neu portierten Steuerelementen (Welle 5+) eingesetzt werden.

*The framework must introduce an `IAccessibleWidget` interface in `TuiVision.Core` that can equip
controls with a machine-readable text description. The interface is intentionally minimal — it does
not need to implement a screen reader API, only establish the prerequisite. It is opt-in: existing
controls are not required to implement it immediately.*

### R-A11Y-TV-02: Text-Repräsentation für Fokus- und Status-Ereignisse

Wenn der Fokus von einem Steuerelement zu einem anderen wechselt, muss dieser
Wechsel als Text-Event propagiert werden — über eine neue oder erweiterte
Methode im `TEvent`-System. Der Event muss das `AccessibleLabel` des neuen
fokussierten Steuerelements (falls implementiert) enthalten.

Ziel: Eine spätere Screen-Reader-Integration oder ein Status-Monitor kann
diesen Event abonnieren, ohne im Framework-Kern zu ändern.

*When focus moves between controls, the change must propagate as a text event through the `TEvent`
system, containing the `AccessibleLabel` of the newly focused control. Goal: a future screen reader
integration or status monitor can subscribe to this event without changes to the framework core.*

### R-A11Y-TV-03: Shortcut-Registrierung als Framework-Vertrag

Tastatur-Shortcuts, die in Anwendungen über TuiVision angeboten werden (z. B.
in `TStatusLine` oder `TMenuBar`), sollen über eine strukturierte API
registrierbar sein — nicht nur als sichtbarer Text in der Statuszeile.

Ziel: Shortcuts sind programmatisch abfragbar, was spätere Automatisierungstests
und Screen-Reader-Integrationen ermöglicht.

Dieses Requirement ist ausdrücklich auf die **API-Definition** beschränkt.
Die vollständige Implementierung in allen Beispielanwendungen ist Bestandteil
von Welle 5 und 6.

*Keyboard shortcuts offered through TuiVision controls should be registerable via a structured API —
not only as visible text in the status line. Goal: shortcuts are programmatically queryable,
enabling future automated tests and screen reader integrations.*

### R-A11Y-TV-04: Playwright+axe-Tests für Dokumentations-HTML in CI/CD integrieren

Die vorhandenen Tests in `tests/web-a11y/` (Playwright + `@axe-core/playwright`,
wcag22aa-Prüfung, lynx-Validierung) sind **manuell** und nicht in `ci.yml` integriert.
Diese Tests müssen in die CI/CD-Pipeline aufgenommen werden — als eigener
CI-Job, der bei jedem Push auf `main` oder bei einem DocFX-Regen-PR ausgeführt wird.

*The existing tests in `tests/web-a11y/` (Playwright + axe, wcag22aa, lynx validation) are manual
and not integrated into CI. They must be added to the CI/CD pipeline as a dedicated job that runs
on every push to main or on any DocFX regeneration PR.*

### R-A11Y-TV-05: Tastaturnavigation lückenlos sicherstellen

Alle Steuerelemente in `TuiVision.Controls`, die `CanFocus = true` setzen,
müssen per Tab/Shift+Tab, Pfeiltasten und Enter vollständig erreichbar und
aktivierbar sein. Dies ist durch Unit-Tests im `FakeDriver`-Modus zu verifizieren.

*All controls in `TuiVision.Controls` with `CanFocus = true` must be fully reachable and activatable
via Tab/Shift+Tab, arrow keys, and Enter. This must be verified by unit tests in FakeDriver mode.*

### R-A11Y-TV-06: High-Contrast-ColorScheme als Framework-Option

Das Framework muss mindestens ein High-Contrast-`ColorScheme` bereitstellen
(schwarzer Hintergrund, weißer Text, gelbe oder cyan Akzente, kein Grau als
einziger Unterschied). Anwendungen können dieses Scheme über eine einfache
API aktivieren.

*The framework must provide at least one high-contrast `ColorScheme` (black background, white text,
yellow or cyan accents, no grey-only distinctions). Applications can activate this scheme via a
simple API.*

---

## 4. Hinweis zu Playwright und Terminal-TUI-Testing

**Playwright kann Terminal-UIs nicht direkt testen.**
Playwright ist für Web-Browser entwickelt und kennt keine ANSI-Escape-Sequenzen.

| Testansatz | Machbarkeit für TuiVision | Anmerkung |
|-----------|:-------------------------:|-----------|
| Playwright + axe: Docs-HTML | ✅ bereits vorhanden | `tests/web-a11y/` |
| Playwright: Terminal-UI direkt | ❌ nicht möglich | ANSI kein Browser |
| xterm.js-WebFrontend + Playwright | ⚠️ möglich, sehr aufwändig | kein Scope dieses LH |
| Prozess-stdin/stdout-Tests | ✅ machbar (xUnit) | für Keyboard-Navigation |
| FakeDriver-Unit-Tests | ✅ bereits in TuiVision genutzt | für Steuerelement-Logik |
| Manuelle Tests: VoiceOver / NVDA / Orca | ✅ empfohlen | auf echtem Terminal |

**Empfehlung:** Playwright bleibt auf Dokumentations-HTML beschränkt (R-A11Y-TV-04).
Für die Terminal-UI selbst sind FakeDriver-Unit-Tests (R-A11Y-TV-05) und
prozessbasierte Integrationstests die richtigen Werkzeuge.

*Playwright cannot test terminal UIs. It remains restricted to documentation HTML (R-A11Y-TV-04).
For the terminal UI itself, FakeDriver unit tests and process-based integration tests are the
appropriate tools.*

---

## 5. Nicht im Scope / Out of Scope

- Vollständige UI-Automation-API-Integration (AT-SPI2, NSAccessibility, UI Automation)
- Screen-Reader-Sprachausgabe direkt aus der Bibliothek
- Vollständige WCAG 2.2 AA-Konformität für die Terminal-UI
  (erreichbar nur für Web/native GUI — Terminal ist eine Einschränkung der Plattform)
- Maus als primärer Eingabekanal
- Änderungen am Porting-Umfang der Wellen 1–4

*Out of scope: full UI automation API integration, direct speech output, full WCAG 2.2 AA for the
terminal UI itself (a platform limitation), mouse as primary input, changes to the wave 1–4 porting scope.*

---

## 6. Akzeptanzkriterien / Acceptance Criteria

| ID | Kriterium / Criterion |
|----|-----------------------|
| AK-A11Y-TV-01 | `IAccessibleWidget`-Interface in `TuiVision.Core` vorhanden; XML-Dok vollständig |
| AK-A11Y-TV-02 | Fokus-Wechsel propagiert Text-Event mit `AccessibleLabel` des Ziel-Steuerelements |
| AK-A11Y-TV-03 | Shortcut-API definiert und in `TStatusLine` und `TMenuBar` als Beispiel implementiert |
| AK-A11Y-TV-04 | Playwright+axe-Tests in `ci.yml` integriert; CI-Job grün auf main |
| AK-A11Y-TV-05 | FakeDriver-Unit-Tests für vollständige Tab-Navigation aller `CanFocus=true`-Controls |
| AK-A11Y-TV-06 | High-Contrast-ColorScheme in Framework-API verfügbar; Beispiel-App nutzt es |
| AK-A11Y-TV-07 | `Pflichtenheft.md` enthält `PF-A11Y-*` Einträge mit Reihenfolgehinweis „nach Welle 4" |

---

## 7. Pflichtenheft-Einträge (nach Umsetzung einzutragen)

Nach Abschluss der Umsetzung dieses Lastenhefte sind folgende Einträge in
`Pflichtenheft.md` unter einem neuen Abschnitt `9. Barrierefreiheits-Fundament`
einzutragen:

```
- [ ] PF-A11Y-01: IAccessibleWidget-Interface in TuiVision.Core
  Reihenfolgehinweis: nach Welle 4 (alle 25 MUSS-Beispiele abgeschlossen).
- [ ] PF-A11Y-02: Fokus-Wechsel als Text-Event
  Reihenfolgehinweis: nach PF-A11Y-01.
- [ ] PF-A11Y-03: Shortcut-Registrierungs-API
  Reihenfolgehinweis: nach PF-A11Y-01; Welle 5 baut darauf auf.
- [ ] PF-A11Y-04: Playwright+axe in CI/CD
  Reihenfolgehinweis: unabhängig, kann parallel zu Welle 4 umgesetzt werden.
- [ ] PF-A11Y-05: FakeDriver-Keyboard-Tests
  Reihenfolgehinweis: ab sofort; nicht auf Welle 4 warten.
- [ ] PF-A11Y-06: High-Contrast-ColorScheme
  Reihenfolgehinweis: nach PF-A11Y-01.
```

---

## 8. Beispiel: Agentic-AI-Dialog (Platzhalter für spätere Durchführung)

Dieser Abschnitt wird während der Umsetzung mit Agentic-AI plus Spec-Kit/SDD
befüllt — jeder Schritt mit Commit-URL und Zeitstempel, analog zu den
bestehenden TuiVision-Lastenhefte-Dialogen.

---

## 9. Hinweis für Lernende / Note for learners

**Deutsch:** TuiVision zeigt, wie Barrierefreiheit schrittweise in ein bestehendes
Framework eingebaut werden kann. Ein Interface wie `IAccessibleWidget` ist kein
vollständiger Screen-Reader — es ist ein Versprechen: „Dieses Steuerelement kann
beschreiben, was es tut." Auf diesem Versprechen kann später aufgebaut werden.

Die wichtigste Lektion: Barrierefreiheit wird nicht durch einen einzigen großen
PR nachgerüstet. Sie entsteht durch viele kleine, konsistente Entscheidungen —
und durch das richtige Timing (nach Welle 4, nicht vorher).

**English:** TuiVision demonstrates how accessibility can be incrementally added to an existing
framework. An interface like `IAccessibleWidget` is not a full screen reader — it is a promise:
"this control can describe what it does." Later work can build on this promise.

The key lesson: accessibility is not retrofitted in one large PR. It emerges from many small,
consistent decisions — and from the right timing (after Wave 4, not before).

---

## 10. Spec-Kit-Readiness / Spec-Kit Readiness

Dieses Lastenheft ist als direkte Eingabedatei fuer `/speckit-specify`
verwendbar. Der spaetere Spec-Kit-Lauf muss die Anforderungen Deutsch zuerst
und Englisch danach uebernehmen, auf CEFR-B2-Niveau formulieren und die
text-first A11Y-Strategie ausdruecklich als Architektur- und Testthema
behandeln.

This requirements document can be used directly as input for
`/speckit-specify`. The later Spec-Kit run must carry the requirements in
German first and English second, use CEFR-B2 language, and treat the
text-first accessibility strategy explicitly as an architecture and testing
topic.

---

## 11. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
/speckit-specify Nutze Lastenheft_06_A11Y_Framework.md als verbindliche Eingabe. Erstelle die Feature-Spezifikation fuer ein TuiVision-A11Y-Framework-Fundament nach Abschluss der MUSS-Wellen 1 bis 4.

Ziel: TuiVision braucht ein text-first A11Y-Fundament mit semantischen Widget-Metadaten, Fokus-/Status-Textsignalen, Shortcut-Vertraegen, High-Contrast-Optionen und passenden Tests.

Pflicht:
- Anforderungen Deutsch zuerst und Englisch danach, CEFR-B2 formulieren.
- Terminal-UI-A11Y realistisch begrenzen: Playwright/axe fuer DocFX-HTML, FakeDriver- und prozessbasierte Tests fuer Terminal-UI-Verhalten.
- `IAccessibleWidget`, Fokuswechsel-Text-Events, Shortcut-Registrierung, Tastaturnavigation und High-Contrast-ColorScheme als pruefbare Architekturthemen behandeln.
- WCAG 2.2 AA fuer generierte HTML-Dokumentation als Baseline nutzen; Terminal-Einschraenkungen dokumentieren statt ueberbehaupten.
- Keine Aenderung am Porting-Scope der Wellen 1 bis 4 und keine Maus als primaeren Eingabekanal in diesen Lauf ziehen.
```
