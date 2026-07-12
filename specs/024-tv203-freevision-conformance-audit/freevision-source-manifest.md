# Free-Vision-Quellmanifest / Free Vision Source Manifest

## Herkunft / Provenance

Deutsch: Free Vision ist eine externe zweite Meinung. Borland-Dokumentation und
`tv203s/` bleiben maßgeblich. Dieses Repository speichert nur Pfade,
Prüfsummen und eigene Verhaltenszusammenfassungen.

English: Free Vision is an external second opinion. Borland documentation and
`tv203s/` remain authoritative. This repository stores only paths, checksums,
and original behavioral summaries.

| Feld / Field | Wert / Value |
|---|---|
| Official repository | `https://gitlab.com/freepascal.org/fpc/source.git` |
| Immutable commit | `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` |
| Retrieved | 2026-07-12 |
| External worktree | `/tmp/tuivision-fv-024-ffc03b34` |
| Reviewed subtree | `packages/fv/` |
| Reviewed source records | 15 |

## Kopiergrenze / No-Copy Boundary

Deutsch: Keine Free-Vision-Datei, kein längerer Quelltextauszug und keine
mechanische Übersetzung wird eingecheckt. Kurze Bezeichner dienen nur zur
eindeutigen Quellenzuordnung.

English: No Free Vision file, substantial source excerpt, or mechanical
translation is committed. Short identifiers are used only to identify evidence.

## Geprüfte Quellen / Reviewed Sources

| Source ID | Path | SHA-256 | Behavioral scope / Verhaltensbereich | Provenance note |
|---|---|---|---|---|
| `FV001` | `packages/fv/src/views.inc` | `31ca0d1be0f9af5f973aff151f35cb8796e6140ec5a342499991d4fd18d17bf6` | Free Vision keeps owner-based view hierarchies, event masks, ordered group dispatch, coordinate conversion, clipping, and explicit event clearing. | Own-word summary; no copied source |
| `FV002` | `packages/fv/src/fvconsts.pas` | `dee7debdf837a57f982ee8425acb01e459bdb82307d2e86a46dfee86061773b2` | Free Vision retains numeric command identities and separate command and broadcast event semantics. | Own-word summary; no copied source |
| `FV003` | `packages/fv/src/app.inc` | `0724163300ad9115aa52b9833e758ce20e5fe139145319baba3be45d020b018a` | Free Vision retains program, application, desktop, event-loop, modal-dialog, and window insertion responsibilities. | Own-word summary; no copied source |
| `FV004` | `packages/fv/src/menus.inc` | `c651f75e1486cab1050e787ae2596d455f410805c67d73e8a421e0e9e175dd7a` | Free Vision retains keyboard menu navigation, command dispatch, status hints, and Unicode menu text. | Own-word summary; no copied source |
| `FV005` | `packages/fv/src/statuses.inc` | `bcbc42ffee6fb2e48f96f2b78ebd8078391b1040f1b6c4f75a1cc4b9148b37df` | Free Vision extends status handling with named states while preserving visible status responsibility. | Own-word summary; no copied source |
| `FV006` | `packages/fv/src/dialogs.inc` | `7ce3dbf42e478ee220689204fadecbe7c6407bbe739ad77e71292db75e05a208` | Free Vision retains dialog validation, control event handling, state transfer, focus broadcasts, and explicit command completion. | Own-word summary; no copied source |
| `FV007` | `packages/fv/src/validate.inc` | `865a9342390618c535ac88a39258bec53aed536f870fb2ad8fb92baf152ef6ba` | Free Vision keeps validator-owned acceptance and rejection semantics for input controls. | Own-word summary; no copied source |
| `FV008` | `packages/fv/src/colorsel.inc` | `91dc95990052616917c49a7cd5ab950c9d3b03f7281863ce742677bd89ee7015` | Free Vision keeps explicit color selection state and dialog-owned confirmation behavior. | Own-word summary; no copied source |
| `FV009` | `packages/fv/src/editors.inc` | `d474af51c4cbad3fc48e6c993208f81b4a54b5907e94f7512624f836e9b1c0d4` | Free Vision retains editor buffers, selection, clipboard, search, replace, file-editor, and event handling with later Unicode support. | Own-word summary; no copied source |
| `FV010` | `packages/fv/src/stddlg.inc` | `3626e70d310831430f4f8f45c283c19f11d30047103f2d4b81b432de2993f96c` | Free Vision retains file-dialog selection, validation, history, metadata, and path handling while adding Unicode-oriented updates. | Own-word summary; no copied source |
| `FV011` | `packages/fv/src/fvclip.inc` | `e6b5881104e060bc52938f6c0618aac2ddf2e7dc64be6f6f7b2cbb01af4888b1` | Free Vision provides a bounded clipboard integration layer used by editor commands. | Own-word summary; no copied source |
| `FV012` | `packages/fv/src/resource.pas` | `c447361213ca73e9559f2fce598534f3ede7798b1b90b8499ad55f0dfc1f9d3b` | Free Vision retains named standard resource identities for dialogs and framework components. | Own-word summary; no copied source |
| `FV013` | `packages/fv/src/histlist.inc` | `d47430d44aa6ac62991f607a8df4d5d68d9ea5bc44ec5ba80899f6b999e00e43` | Free Vision retains ordered history-list behavior and explicit event-driven recall. | Own-word summary; no copied source |
| `FV014` | `packages/fv/src/drivers.inc` | `85f4435ae0cf1f9a920fa951b4dbd60cce4d7c46db08de31a0478bda7fa34044` | Free Vision normalizes keyboard and mouse events and has evolved toward Unicode strings, grapheme-aware movement, and platform-backed screen capabilities. | Own-word summary; no copied source |
| `FV015` | `packages/fv/src/platform.inc` | `3a0630e7e0ac372ed5533b2106061d014b6eeabed5a6582bd0b71184fbca4929` | Free Vision still selects platform behavior through compile-time platform branches rather than one managed capability-bucket driver. | Own-word summary; no copied source |

## Begründete N/A-Domänen / Justified N/A Domains

| Domain | Rationale / Begründung | Re-evaluation trigger |
|---|---|---|
| `D01` | `packages/fv` delegates base collections and object primitives to the FPC runtime; no independent Free Vision contract can clarify Borland collection semantics. | a Free Vision-owned base collection layer enters the pin |
| `D10` | Streaming and object identity are supplied by broader FPC runtime units, not an independent `packages/fv` persistence contract. | a Free Vision-owned persistence format enters the pin |
| `D15` | The pin has focus and shortcut behavior but no semantic widget-text, typed focus-announcement, or named high-contrast API equivalent. | a semantic A11Y contract enters Free Vision |
| `D16` | Application-loop proof taxonomy and smoke-helper classification are TuiVision test-governance concepts, not runtime behavior in Free Vision. | Free Vision publishes comparable proof-governance artifacts |

## Einschränkungen / Limitations

Free Vision contains later Unicode fixes and platform choices. Those choices may
corroborate or diverge from Borland, but they cannot redefine historical intent.
