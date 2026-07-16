# Pre-Wave-Gate für Feature 031 / Feature 031 Pre-Wave Gate

## Feature-Head

| Ziel / Target | Zustand / State | Begründung / Rationale |
|---|---|---|
| Feature 031 | `Blocked` | Lokale, remote, Review- und Exact-Head-Gates sind noch nicht vollständig kausal bewiesen |
| Wave 5 | `BlockedPendingCausalClosure` | Der reviewte Feature-Head darf seinen eigenen späteren Merge nicht vorwegnehmen |
| Wave 6 | `BlockedPendingCausalClosure` | Wave 6 benötigt zusätzlich die abgeschlossene Wave 5 und deren echten Delta-Review |

*Feature 031 remains blocked until every local, remote, review, and exact-head
gate is causally proven. The reviewed feature head cannot anticipate its own
future merge.*

## Verpflichtende Feature-Head-Gates / Mandatory Feature-Head Gates

1. Exakte 48/13/96/13-Mengen und Null-Finding-Grenzen.
2. Free Vision 15/15, Terminal.GUI 25/25 und magiblot 50/50 mit exakten Pins.
3. Gezielte Validatoren, vollständige Release-Tests und fünf Coverage-Gates.
4. Formatierung, DocFX, Playwright/Axe, Lynx und UTF-8-Review.
5. Secret-, Scope-, Supply-Chain-, Agent-Paritäts- und Plattformnachweise.
6. Exakter reviewter Head, grüne Pflichtchecks und null umsetzbare Threads.
7. Fehlende Reviewer bleiben fehlend; ein Bypass darf nur Human Approval
   betreffen.

## Zulässiger Post-Merge-Übergang / Allowed Post-Merge Transition

| Ziel / Target | Höchster zulässiger Zustand / Maximum Allowed State |
|---|---|
| Wave 5 | `Eligible` |
| Wave 6 | `ConditionallyReady` |

Der Übergang benötigt `delivery-closeout.md` mit exaktem reviewtem Head,
vollständiger Gate-Evidence und Feature-Merge. Die Closeout-Datei darf ihre
eigene PR-, Head- oder Merge-Identität nicht rekursiv verlangen.

*The transition requires `delivery-closeout.md` with the exact reviewed head,
complete gate evidence, and the feature merge. The closeout file must not
recursively require its own PR, head, or merge identity.*

## Stop-Grenzen / Stop Boundaries

- Pin-, Tree-, Lizenz- oder Source-Hash-Drift.
- Fehlende, doppelte, unbekannte oder widersprüchliche Relation.
- Wieder geöffnetes `F001`-`F013` oder neues `CF###`.
- Produktentscheidung, nicht leere Ownergruppe, Kante oder Hardening-Intake.
- Fehlgeschlagenes Pflicht-Gate oder umsetzbarer Review-Thread.
- Verfrühter `Eligible`- oder unzulässiger Wave-6-Zustand.

Solche Befunde blockieren Feature 031. Sie werden nicht als Produktkorrektur in
diesem evidence-only Lauf behoben.

*These findings block Feature 031. They are not repaired as product changes
inside this evidence-only run.*
