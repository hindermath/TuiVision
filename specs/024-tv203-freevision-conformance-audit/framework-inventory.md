# Framework-Inventar / Framework Inventory

## Zweck / Purpose

Deutsch: Dieses Inventar ordnet jede historische Implementierung, jede gepflegte
Produktionsdatei und jeden exportierten öffentlichen Typ genau einer primären
Audit-Domäne zu. Verträge dürfen mehrere Inventarelemente referenzieren, ohne
deren Eigentümerschaft zu verdoppeln.

English: This inventory assigns every historical implementation, maintained
production file, and exported public type to exactly one primary audit domain.
Contracts may reference several inventory items without duplicating ownership.

## Quellenwurzeln / Source Roots

| Rolle / Role | Root | Boundary |
|---|---|---|
| Borland documentation | `TVDocs/` | primary historical documentation, read-only |
| Historical implementation | `tv203s/contrib/tvision/classes/` | 151 `.cc` rows, primary historical source, read-only |
| Historical declarations | `tv203s/include/tv/` and matching headers | supporting context when declarations are required, read-only |
| Historical examples | `TVDEMOS/`, `TVFM/` | behavioral context only, read-only |
| Modern framework | five modules below | maintained `.cs` files, excluding `bin/` and `obj/` |
| Modern proof | `tests/` | concrete test names or explicit evidence gaps only |
| Secondary comparison | `/tmp/tuivision-fv-024-ffc03b34/packages/fv/` | pinned external read-only worktree, never tracked |

## Vollständige Zählung / Complete Counts

| Inventory | Count | Validation source |
|---|---:|---|
| Historical `.cc` rows | 151 | filesystem plus `docs/porting-status.md` |
| Maintained production `.cs` files | 147 | five source modules |
| Exported public types | 235 | reflection over five Release assemblies |
| Audit domains | 16 | canonical JSON |
| Framework contracts | 48 | three reviewable responsibilities per domain |

## Domänenzählung / Domain Counts

| Domain | Name | Historical | Modern files | Public types | Contracts |
|---|---|---:|---:|---:|---:|
| `D01` | Basistypen und Sammlungen / Base types and collections | 11 | 11 | 11 | 3 |
| `D02` | Ereignisse, Befehle und Dispatch / Events, commands, and dispatch | 2 | 1 | 6 | 3 |
| `D03` | View-Hierarchie, Fokus und Lebenszyklus / View hierarchy, focus, and lifecycle | 3 | 4 | 4 | 3 |
| `D04` | Koordinaten, Clipping und Größenänderung / Coordinates, clipping, and resizing | 3 | 3 | 3 | 3 |
| `D05` | Anwendung, Desktop und Modalität / Application, desktop, and modality | 6 | 7 | 13 | 3 |
| `D06` | Menüs, Statuszeile und Hilfe / Menus, status line, and help | 6 | 9 | 11 | 3 |
| `D07` | Dialoge, Controls und Validierung / Dialogs, controls, and validation | 21 | 38 | 61 | 3 |
| `D08` | Editor, Zwischenablage und Dateien / Editor, clipboard, and files | 18 | 13 | 23 | 3 |
| `D09` | Hilfe, Ressourcen und Lokalisierung / Help, resources, and localization | 9 | 22 | 31 | 3 |
| `D10` | Streams, Registrierung und Persistenz / Streams, registry, and persistence | 12 | 16 | 24 | 3 |
| `D11` | Puffer, Zellen und Rendering / Buffers, cells, and rendering | 3 | 5 | 5 | 3 |
| `D12` | Tastatur, Maus und Eingabe / Keyboard, mouse, and input | 23 | 7 | 17 | 3 |
| `D13` | Zeichensatz, Fonts und Terminal / Charset, fonts, and terminal | 6 | 5 | 19 | 3 |
| `D14` | Kompatibilität und native Auslassungen / Compatibility and native omissions | 28 | 2 | 2 | 3 |
| `D15` | Barrierefreiheit / Accessibility | 0 | 4 | 5 | 3 |
| `D16` | Smoke- und Proof-Helfer / Smoke and proof helpers | 0 | 0 | 0 | 3 |

## Assembly-Zählung / Assembly Counts

| Assembly | Modern files | Exported public types |
|---|---:|---:|
| `TuiVision.Core` | 16 | 22 |
| `TuiVision.Controls` | 97 | 141 |
| `TuiVision.Serialization` | 23 | 37 |
| `TuiVision.Compatibility` | 2 | 3 |
| `TuiVision.Drivers.Console` | 9 | 32 |

## ID- und Tabellenregeln / ID and Table Rules

- Historical items use `H001` through `H151` in ordinal path order.
- Modern source items use stable IDs through `M147`; later additions are appended without renumbering earlier evidence.
- Exported types use stable IDs through `P235`; later additions are appended without renumbering earlier evidence.
- Every item has one `D01` through `D16` owner and at least one valid `C###` link.
- Every listed item-to-contract link has a matching contract-to-item link and
  vice versa; cross-domain links are permitted when one behavioral contract
  depends on a type owned by another primary inventory domain.
- The canonical complete rows live in `conformance-audit.json`; this readable
  view summarizes them without duplicating hundreds of rows.

## Inventarstatus / Inventory Status

Deutsch: Alle drei Live-Mengen sind vollständig und beidseitig zugeordnet.
Revision 2 ergänzt die semantische Verbraucherprüfung und 13 Findings, ohne die
primäre Inventareigentümerschaft zu verändern.

English: All three live sets are completely and reciprocally assigned. Revision
2 adds semantic consumer review and 13 findings without changing primary
inventory ownership.

Feature 042 appended its additive form-transaction files and public types to
the live inventory because it changed the current product contract. This
inventory refresh does not reopen the completed historical audit decisions.
