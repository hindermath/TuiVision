# Wave-5-/Wave-6-Verbraucherprüfung / Wave 5 and Wave 6 Consumer Review

## Zweck / Purpose

Deutsch: Diese Audit-Revision prüft, ob die aktuelle Frameworkbasis die
tatsächlichen Verbraucher aus `TVDEMOS/` und `TVFM/` tragen kann. Sie ist keine
Portierung und keine mechanische Paritätsforderung. Borland Turbo Vision 2.0.3
bleibt die historische Primärquelle; Free Vision am gepinnten Commit
`ffc03b34d8cafb85ddcf0686de1c5551601dacb2` ist eine zweite
Implementierungsmeinung. Moderner, sicherer und idiomatischer C#-Code bleibt die
Zielarchitektur.

English: This audit revision checks whether the current framework foundation can
support the actual consumers in `TVDEMOS/` and `TVFM/`. It is neither a port nor
a mechanical parity requirement. Borland Turbo Vision 2.0.3 remains the primary
historical source; Free Vision at pinned commit
`ffc03b34d8cafb85ddcf0686de1c5551601dacb2` is a second implementation opinion.
Modern, safe, idiomatic C# remains the target architecture.

## Umfang und Unveränderlichkeit / Scope and Immutability

| Root | Reviewed role | Files observed | Change rule |
|---|---|---:|---|
| `TVDEMOS/` | Wave-5 consumer contracts | 18 files, including 15 Pascal sources | Read-only |
| `TVFM/` | Wave-6 consumer contracts | 24 files, including 19 Pascal sources | Read-only |
| `tv203s/contrib/tvision/` | Primary historical behavior | Relevant `.cc` and headers | Read-only |
| pinned Free Vision worktree | Secondary comparison | Relevant `packages/fv/` units | External and untracked |
| `src/` and `tests/` | Current behavior and proof | Relevant framework and proof paths | Review-only in this revision |

No example, runtime behavior, public API, package, external source, or generated
output is changed by this review.

## Methodische Korrektur / Method Correction

Deutsch: Der erste Auditlauf verlangte für jeden Vertrag einen benannten Test,
prüfte aber nicht ausreichend, ob dieser Test den realen Verbraucherpfad
erreicht. Drei False-Confidence-Muster wurden sichtbar:

1. Ein Test speist bereits normalisierte `TEvent`-Werte ein und umgeht damit den
   realen `ConsoleKeyInfo`-Ingress.
2. Ein Test bestätigt nur, dass `cmClose` gesendet wurde, aber nicht, dass das
   Fenster aus dem Desktop entfernt und der Fokus wiederhergestellt wurde.
3. Ein Test verwendet einen versteckten oder abgeleiteten Hook, während der
   Produktionspfad eine andere, nicht virtuelle Methode aufruft.

English: The first audit required a named test for every contract but did not
check strongly enough whether that test reached the real consumer path. Three
false-confidence patterns became visible: normalized events bypass real console
ingress, close-signal tests do not prove visible removal and focus restoration,
and derived test hooks can differ from the non-virtual production path.

Revision 2 therefore distinguishes `partial proof` from `consumer-complete
proof`. Existing tests remain valuable; they no longer justify `Aligned` when
the required real path is absent or bypassed.

## Wave-5-Verbraucher / Wave 5 Consumers

| Source | Consumer behavior | Required shared contract | Revision-2 result |
|---|---|---|---|
| `TVDEMOS/TVDEMO.PAS` | application loop, menus, status, dialogs, help, mouse, clock/heap idle refresh, dynamic commands, window tile/cascade | `C004`, `C008`, `C009`, `C013`-`C017`, `C019`, `C021`, `C034` | Findings `F001`-`F008`, `F010`, `F011` |
| `TVDEMOS/TVEDIT.PAS` | editor windows, execute-dialog flow, close/resize/next commands, file decisions | `C014`, `C015`, `C017`, `C019`, `C023`, `C034` | Findings `F005`-`F008`, `F010`, `F012` |
| `TVDEMOS/TVRDEMO.PAS` | named runtime resources | `C026` | Finding `F013` |
| `TVDEMOS/GENRDEMO.PAS` | generated or described UI resources | `C026` | Finding `F013` |
| `TVDEMOS/GADGETS.PAS` and related demos | idle-updated visible state and command integration | `C013`, `C017` | Findings `F004`, `F007` |
| `TVDEMOS/MOUSEDLG.PAS` | mouse path with keyboard-complete operation | `C004`, `C034`, `C036` | `F001`, `F008`; generic drag remains Wave-6-owned `F009` |

Wave 5 does not require a literal port of historical service locators, raw
pointers, DOS memory reporting, or binary resources. It does require the shared
behavioral boundaries above so that application code does not become a second
framework.

## Wave-6-Verbraucher / Wave 6 Consumers

| Source | Consumer behavior | Required shared contract | Revision-2 result |
|---|---|---|---|
| `TVFM/TVFM.PAS` | named resources, idle refresh, shared command sets, active-window broadcasts, tile/cascade/close-all | `C013`-`C017`, `C026`, `C034` | Findings `F004`-`F008`, `F013` |
| `TVFM/FILEVIEW.PAS` | incremental directory scan on idle, tag broadcasts, drag/drop | `C013`, `C017`, `C036` | Findings `F004`, `F007`, `F009` |
| `TVFM/DRAGDROP.PAS` | generic mouse tracking, drag view, list-view drop | `C036` | Finding `F009` |
| `TVFM/TREEWIN.PAS` | focused child, close-all, file/tree window ownership | `C008`, `C009`, `C014`, `C015` | Findings `F002`, `F003`, `F005`, `F006` |
| `TVFM/GLOBALS.PAS` | rename and attribute dialogs with `Valid` overrides | `C019`, `C021`, `C023` | Findings `F010`-`F012` |
| `TVFM/COLORS.PAS` | color dialog validation and state preservation | `C019`, `C021` | Findings `F010`, `F011` |
| `TVFM/FILECOPY.PAS` and `TVFM/TRASH.PAS` | destructive file operations | application/product policy | Deferred to Wave 6; not forced into 025 or 026 |

The destructive copy, move, delete, trash, and provider-specific file-manager
flows remain future application decisions. Features 025 and 026 may deliver
safe reusable contracts but must not implement those workflows speculatively.

## Primär- und Zweitquellen / Primary and Secondary Sources

| Contract area | Turbo Vision evidence | Free Vision evidence | Interpretation |
|---|---|---|---|
| Event kinds and input | `include/tv/event.h`, `classes/tprogram.cc` | `FV001`, `FV014` | One concrete event kind and normalized ingress remain stable intent |
| Focus and group state | `classes/tgroup.cc`, `classes/tinputli.cc` | `FV001`, `FV007` | Current/focus ownership and validation-aware focus loss are corroborated |
| Application and desktop | `classes/tprogram.cc`, `classes/tdesktop.cc`, `classes/twindow.cc` | `FV003` | Idle, modal execution, and desktop ownership are framework responsibilities |
| Dialog validation | `classes/tdialog.cc`, `classes/tinputli.cc` | `FV006`, `FV007` | Only completion commands close; invalid children can block acceptance |
| File dialog | `classes/tfiledia.cc` | `FV010` | Path type and operation mode are checked before successful completion |
| Generic mouse tracking | `classes/tview.cc` and driver contracts | `FV001`, `FV014` | Bounded capture and cancel semantics belong below application-local drag logic |
| Resources | historical resource declarations and named lookup | `FV012` | Modern descriptions are acceptable, but named reconstructible composition needs proof |

Free Vision corroborates the behavioral responsibility. It does not require the
same class hierarchy, binary format, Pascal ownership model, or platform split.

## Finding-Zuordnung / Finding Allocation

| Owner | Findings | Dependency reason |
|---|---|---|
| `Core025` | `F001`-`F009` | Event, focus, lifecycle, desktop, command, keyboard, and drag contracts underpin the component flows |
| `ComponentData026` | `F010`-`F013` | Dialog, validator, file-selection, and resource contracts build on the corrected core lifecycle |
| `Closure028` | all 13 | Independent proof and combined Wave-5/Wave-6 readiness decision |

Feature 025 must run before 026. Feature 028 must run after both. Wave 5 and
Wave 6 remain blocked until 028 passes; no autonomous run is started by this
document.

## Remediation Status Before Feature 028

Deutsch: Feature 025 hat `F001` bis `F009` und Feature 026 hat `F010` bis
`F013` mit nicht dokumentationsbasierten Red-/Green-Nachweisen geschlossen.
Diese Schließungen sind Eingabe für Feature 028, aber noch keine Freigabe von
Wave 5 oder Wave 6. Die Consumer-Zuordnung in diesem Dokument bleibt die
unveränderte unabhängige Prüfbasis.

English: Feature 025 closed `F001` through `F009`, and Feature 026 closed
`F010` through `F013`, using non-documentation-only red/green proof. These
closures are input to Feature 028 but do not yet release Wave 5 or Wave 6. The
consumer mapping in this document remains the unchanged independent review
baseline.

## Review-Schluss / Review Conclusion

Deutsch: Die C#-Basis ist strukturell brauchbar und in vielen Bereichen bewusst
modernisiert. Sie ist für Wave 5 und Wave 6 noch nicht hinreichend bewiesen. Die
13 Findings sind begrenzt und reparierbar; sie rechtfertigen keine breite
Framework-Neuschreibung. Das richtige Vorgehen ist eine kleine
Kernhärtung 025, eine darauf aufbauende Komponenten-/Datenhärtung 026 und eine
unabhängige Schließung 028.

English: The C# foundation is structurally usable and deliberately modernized in
many areas. It is not yet sufficiently proven for Wave 5 and Wave 6. The 13
findings are bounded and remediable; they do not justify a broad framework
rewrite. The correct sequence is a focused core hardening 025, a dependent
component/data hardening 026, and an independent closure 028.

## Feature-028-Reassessment / Feature 028 Reassessment

Deutsch: Der unabhaengige Abschluss bestaetigt die sechs Wave-5- und sieben
Wave-6-Gruppen ohne neue Consumer-ID. Zwoelf Entscheidungen sind
`UseExistingFramework`; `W6-007` bleibt `FollowUpHardening`, weil destruktive
Dateioperationen eine ausdrueckliche Produktpolitik benoetigen. Keine
Consumer-Quelle wurde veraendert. Feature 029 ist der einzige naechste Intake,
und beide Waves bleiben bis zum danach findings-basierten Closure blockiert.

English: The independent closure confirms all six Wave-5 and seven Wave-6
groups without a new consumer ID. Twelve decisions are `UseExistingFramework`;
`W6-007` remains `FollowUpHardening` because destructive file operations need
an explicit product policy. No consumer source changed. Feature 029 is the sole
next intake, and both Waves remain blocked through its finding-derived closure.
