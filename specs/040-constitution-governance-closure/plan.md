# Implementation Plan: Constitution Governance Closure

## Summary / Zusammenfassung

Der Lauf vergleicht den bindenden Intake mit der gelieferten Constitution,
den Spec-Kit-Templates und allen gepflegten Agent-Flächen. Weil spätere
Governance-Features die Anforderungen bereits umgesetzt haben, wird ein
read-only Closure mit `AlreadySatisfied`-Evidence erzeugt.

## Scope and design / Scope und Entwurf

- keine Produkt- oder Governance-Schreibfläche außer Feature-Evidence;
- exakte Intake- und Review-Hashbindung;
- siebenzeilige Anforderungsmatrix mit konkreten Pfaden;
- Shell-Parität für Homogeneity, Secrets und autonomen State;
- `specify check`, `git diff --check` und geschützter Scope-Diff.

## Constitution check / Constitution-Prüfung

Das Feature prüft die Constitution selbst und kann sich daher nicht allein auf
eine unbestätigte Behauptung stützen. Die Evidence nennt konkrete Abschnitte,
Templates und Agent-Flächen. Historischer Quellenbezug ist `N/A`, weil kein
Turbo-Vision-Verhalten geändert oder geprüft wird.

## Documentation impact

`GeneratedUpdate`: Der Homogeneity-Check hat ausschließlich den generierten
ASCII-Block in `docs/project-statistics.md` als driftend erkannt. Dieser Block
wird auf die kanonische Renderer-Ausgabe aktualisiert; dadurch werden DocFX,
Axe und Lynx ausgelöst. Public API, XML-Kommentare, Navigation und Guides
bleiben unverändert.
