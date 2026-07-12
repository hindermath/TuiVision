# Framework-Konformitätsmatrix / Framework Conformance Matrix

## Entscheidungsmodell / Decision Model

Deutsch: Jeder Vertrag erhält genau eine Primärentscheidung und eine getrennte
Free-Vision-Relation. `Aligned` bewahrt beobachtbare historische Absicht.
`IntentionalModernization` bewahrt den Zweck in moderner C#-Form.
`ConsciouslyOmitted` dokumentiert eine bewusste Auslassung. Nur
`BehavioralDrift` und `EvidenceGap` erzeugen Findings.

English: Every contract receives exactly one primary decision and one separate
Free Vision relation. `Aligned` preserves observable historical intent.
`IntentionalModernization` preserves purpose in modern C# form.
`ConsciouslyOmitted` documents a deliberate omission. Only
`BehavioralDrift` and `EvidenceGap` create findings.

## Zählung / Counts

### Primärentscheidungen / Primary Decisions

| Decision | Count |
|---|---:|
| `Aligned` | 13 |
| `ConsciouslyOmitted` | 1 |
| `IntentionalModernization` | 34 |

### Free-Vision-Relationen / Free Vision Relations

| Relation | Count |
|---|---:|
| `CorroboratesModernization` | 10 |
| `CorroboratesOriginal` | 22 |
| `DivergesFromOriginal` | 3 |
| `NotApplicable` | 13 |

## D01 - Basistypen und Sammlungen / Base types and collections

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C001` | Objektlebenszyklus / Object lifecycle | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Core.Tests/Test1.cs::TObject_Destroy_CallsShutdownAndDispose | Changes to equality, ordering, or disposal can affect all higher framework layers. |
| `C002` | Geometrie und Rechtecke / Geometry and rectangles | `Aligned` | `NotApplicable` | tests/TuiVision.Core.Tests/Test1.cs::TRect_IntersectAndUnion_FollowTurboVisionSemantics | Changes to equality, ordering, or disposal can affect all higher framework layers. |
| `C003` | Sammlungen und Sortierung / Collections and sorting | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Core.Tests/CollectionProofTests.cs::TSortedCollection_Add_MaintainsSortedOrder; tests/TuiVision.Core.Tests/CollectionProofTests.cs::TStringCollection_AddDuplicate_DoesNotIncreaseCount | Changes to equality, ordering, or disposal can affect all higher framework layers. |
## D02 - Ereignisse, Befehle und Dispatch / Events, commands, and dispatch

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C004` | Typisierte Ereigniskanäle / Typed event channels | `IntentionalModernization` | `CorroboratesOriginal` | tests/TuiVision.Core.Tests/Test1.cs::TEvent_CreateAndClear_UpdatesEventChannels; tests/TuiVision.Core.Tests/Test1.cs::TEvent_CreateKeyBroadcastAndNone_InitializeChannels; tests/TuiVision.Core.Tests/Test1.cs::TEvent_CreateMouse_RejectsNonMouseEventKind | A shared numeric command namespace and incorrect event clearing can misroute input. |
| `C005` | Befehlsidentität / Command identity | `IntentionalModernization` | `CorroboratesOriginal` | tests/TuiVision.Core.Tests/Test1.cs::TEvent_CreateAndClear_UpdatesEventChannels; tests/TuiVision.Controls.Tests/TProgramTests.cs::TProgram_CommandRouting_ExecutesExactlyOnce | A shared numeric command namespace and incorrect event clearing can misroute input. |
| `C006` | Geordneter Dispatch / Ordered dispatch | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TGroupTests.cs::TGroup_HandleEvent_PreProcess_ReceivesBeforeFocused; tests/TuiVision.Controls.Tests/TGroupTests.cs::TGroup_HandleEvent_PreProcess_ConsumedEvent_StopsAllSubsequentPhases | A shared numeric command namespace and incorrect event clearing can misroute input. |
## D03 - View-Hierarchie, Fokus und Lebenszyklus / View hierarchy, focus, and lifecycle

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C007` | Eigentum und Lebenszyklus / Ownership and lifecycle | `IntentionalModernization` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TGroupTests.cs::TGroup_Insert_AddsViewToList_AndSetsOwner; tests/TuiVision.Controls.Tests/TGroupTests.cs::TGroup_ShutDown_CallsShutDownOnAllChildren_InLIFOOrder | Incorrect owner or focus state can orphan views or deliver events to the wrong target. |
| `C008` | Fokusübergang / Focus transition | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TGroupTests.cs::TGroup_SetFocus_TransfersFocusedState; tests/TuiVision.Controls.Tests/TGroupTests.cs::TGroup_SelectNext_SkipsDisabledViews | Incorrect owner or focus state can orphan views or deliver events to the wrong target. |
| `C009` | Hierarchie und Zustandsweitergabe / Hierarchy and state propagation | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TGroupTests.cs::TGroup_SetState_Focused_PropagatesDirectChildren; tests/TuiVision.Controls.Tests/TGroupTests.cs::TGroup_Draw_CallsDrawViewOnVisibleChildrenInInsertionOrder | Incorrect owner or focus state can orphan views or deliver events to the wrong target. |
## D04 - Koordinaten, Clipping und Größenänderung / Coordinates, clipping, and resizing

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C010` | Koordinatentransformation / Coordinate transformation | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TViewMouseInteractionTests.cs::MakeGlobalAndLocal_NestedOwners_UseScreenCoordinates | Off-by-one bounds or clipping errors can hide or overwrite visible cells. |
| `C011` | Wachstum und Scrollen / Growth and scrolling | `IntentionalModernization` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TScrollGroupTests.cs::TScrollGroup_CombinedScrolling_UpdatesBothAxes; tests/TuiVision.Controls.Tests/TScrollerTests.cs::TScroller_ScrollTo_ClampsOffsetAndSynchronisesScrollBars | Off-by-one bounds or clipping errors can hide or overwrite visible cells. |
| `C012` | Clipping und Exposition / Clipping and exposure | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TGroupTests.cs::TGroup_Draw_AllocatesBuffer_WhenBufferedAndExposed; tests/TuiVision.Controls.Tests/TViewExtendedTests.cs::TView_DrawView_SkipsDraw_WhenInvisible | Off-by-one bounds or clipping errors can hide or overwrite visible cells. |
## D05 - Anwendung, Desktop und Modalität / Application, desktop, and modality

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C013` | Anwendungsschleife / Application loop | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TProgramTests.cs::TProgram_Run_ShutsDownCleanly; tests/TuiVision.Controls.Tests/TProgramTests.cs::TProgram_Command_RoutedOnlyOnce | Queue, modal, or close regressions can hang the application or lose state. |
| `C014` | Desktop und Fensterstapel / Desktop and window stack | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TApplicationTests.cs::TApplication_Constructor_SetsFocusToDesktop; tests/TuiVision.Controls.Tests/TDesktopTests.cs::TDesktop_FocusFallback_SelectsNextEligibleChild | Queue, modal, or close regressions can hang the application or lose state. |
| `C015` | Modalität und Fensterabschluss / Modality and window close | `IntentionalModernization` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TDialogTests.cs::TDialog_ExistingModalBehavior_NotRegressed; tests/TuiVision.Controls.Tests/TWindowTests.cs::TWindow_Escape_DoesNotCloseWhenConsumedByChild | Queue, modal, or close regressions can hang the application or lose state. |
## D06 - Menüs, Statuszeile und Hilfe / Menus, status line, and help

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C016` | Menünavigation / Menu navigation | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TMenuBarTests.cs::TMenuBar_TopLevel_ArrowRightWrapsAround; tests/TuiVision.Controls.Tests/TMenuBarTests.cs::TMenuBar_EnterKey_DispatchesCommand | Disabled commands or shortcut drift can expose unavailable actions or break keyboard access. |
| `C017` | Status und Befehlsfreigabe / Status and command enablement | `IntentionalModernization` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TStatusLineTests.cs::TStatusLine_DisabledItems_VisibleButNotExecutable; tests/TuiVision.Controls.Tests/TStatusLineTests.cs::TStatusLine_ContextChange_RefreshesOnFocusChange | Disabled commands or shortcut drift can expose unavailable actions or break keyboard access. |
| `C018` | Shortcuts und Hilfe-Beschreibung / Shortcuts and help description | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Controls.Tests/KeyboardAccessibilityMatrixTests.cs::MenuBar_F10ArrowsAndEnter_DispatchStructuredCommand; tests/TuiVision.Controls.Tests/AccessibilityFrameworkTests.cs::StatusLine_GetAccessibleShortcuts_UsesExplicitKeysAndPreservesSources | Disabled commands or shortcut drift can expose unavailable actions or break keyboard access. |
## D07 - Dialoge, Controls und Validierung / Dialogs, controls, and validation

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C019` | Dialogzustand und Validierung / Dialog state and validation | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TDialogTests.cs::TDialog_Valid_RejectsCloseWhenValidReturnsFalse; tests/TuiVision.Controls.Tests/TDialogTests.cs::TDialog_Valid_AcceptsCloseWhenValidReturnsTrue | Partial validation or state mutation can accept invalid data or discard user input. |
| `C020` | Control-Aktivierung / Control activation | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TButtonTests.cs::TButton_HandleEvent_EnterDispatchesCommand; tests/TuiVision.Controls.Tests/TButtonTests.cs::TButton_SelectNext_DisabledButtonIsSkipped | Partial validation or state mutation can accept invalid data or discard user input. |
| `C021` | Auswahl, Eingabe und Farbe / Selection, input, and color | `IntentionalModernization` | `CorroboratesModernization` | tests/TuiVision.Controls.Tests/TInputLineTests.cs::TInputLine_HandleEvent_MaxLenZeroRejectsInput; tests/TuiVision.Controls.Tests/TColorDialogTests.cs::TColorDialog_CancelSelection_RestoresCommittedValue | Partial validation or state mutation can accept invalid data or discard user input. |
## D08 - Editor, Zwischenablage und Dateien / Editor, clipboard, and files

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C022` | Editor und Zwischenablage / Editor and clipboard | `IntentionalModernization` | `CorroboratesModernization` | tests/TuiVision.Controls.Tests/TEditorCommandTests.cs::TEditor_ClipboardAndUndo_CutPasteAndUndoRoundTrip; tests/TuiVision.Controls.Tests/TEditorTests.cs::TEditor_Editing_SupportsOverwriteAndSelectionReplacement | Incorrect close, conflict, or file decisions can lose edited content. |
| `C023` | Dateiauswahl und Konflikte / File selection and conflicts | `IntentionalModernization` | `CorroboratesModernization` | tests/TuiVision.Controls.Tests/TFileEditorTests.cs::TFileEditor_Save_RequiresExplicitDecisionAfterExternalChange; tests/TuiVision.Controls.Tests/TStandardDialogFlowTests.cs::TFileDialog_SaveTargetExistingFile_RequiresCallerDecision | Incorrect close, conflict, or file decisions can lose edited content. |
| `C024` | Schließen, Suchen und Ersetzen / Close, search, and replace | `IntentionalModernization` | `CorroboratesModernization` | tests/TuiVision.Controls.Tests/TEditorCommandTests.cs::TEditor_SearchReplace_FindsAndReplacesSelection; tests/TuiVision.Controls.Tests/TEditWindowTests.cs::TEditWindow_Close_ModifiedDocumentRequiresExplicitDecision | Incorrect close, conflict, or file decisions can lose edited content. |
## D09 - Hilfe, Ressourcen und Lokalisierung / Help, resources, and localization

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C025` | Hilfethemen und Compiler / Help topics and compiler | `IntentionalModernization` | `CorroboratesOriginal` | tests/TuiVision.Serialization.Tests/THelpSourceCompilerTests.cs::Compile_ValidForwardReference_CreatesRuntimeHelpModel; tests/TuiVision.Serialization.Tests/THelpSourceCompilerTests.cs::Compile_InvalidReference_ReturnsDiagnosticWithoutModel | Broken references, locale fallback, or resource identity can show incorrect help or text. |
| `C026` | Ressourcen und Lokalisierung / Resources and localization | `IntentionalModernization` | `CorroboratesOriginal` | tests/TuiVision.Serialization.Tests/TLocalizedResourceLookupTests.cs::Find_Fallbacks_UseCallerOrderWithoutDuplicates; tests/TuiVision.Serialization.Tests/TResourceFileTests.cs::TResourceFile_SaveLoad_PreservesExactKeySemantics | Broken references, locale fallback, or resource identity can show incorrect help or text. |
| `C027` | History und Konfiguration / History and configuration | `IntentionalModernization` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/THistoryTests.cs::THistory_Add_DuplicateValue_MovedToFront; tests/TuiVision.Controls.Tests/ControlsProofTests.cs::TConfigFile_SetAndGet_ReturnsStoredValue | Broken references, locale fallback, or resource identity can show incorrect help or text. |
## D10 - Streams, Registrierung und Persistenz / Streams, registry, and persistence

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C028` | Stream-Roundtrip und Identität / Stream roundtrip and identity | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Serialization.Tests/PStreamTests.cs::PStream_WriteRead_PreservesSharedReferences; tests/TuiVision.Serialization.Tests/TRecordCompatibilityTests.cs::TRecordCompatibility_ResourceReload_RoundTripsRegisteredGraph | Identity, cycle, length, or version errors can corrupt or accept malformed persisted data. |
| `C029` | Ablehnung fehlerhafter Daten / Malformed-data rejection | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Serialization.Tests/PStreamTests.cs::PStream_Read_RejectsUnknownTypesAndTrailingData; tests/TuiVision.Serialization.Tests/SerializationHardeningEndToEndTests.cs::ResourceLoad_NegativePayloadLength_ThrowsInvalidData | Identity, cycle, length, or version errors can corrupt or accept malformed persisted data. |
| `C030` | Registrierung und Versionen / Registry and versions | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Serialization.Tests/TRecordCompatibilityTests.cs::TRecordRegistry_Register_DuplicateTypeIdThrows; tests/TuiVision.Serialization.Tests/DialogDescriptionPersistenceTests.cs::TDialogDescriptionRecord_UnsupportedVersion_IsRejected | Identity, cycle, length, or version errors can corrupt or accept malformed persisted data. |
## D11 - Puffer, Zellen und Rendering / Buffers, cells, and rendering

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C031` | Zellen und Puffer / Cells and buffers | `IntentionalModernization` | `CorroboratesModernization` | tests/TuiVision.Core.Tests/TConsoleBufferTests.cs::TConsoleBuffer_SetCell_StoresAndReturns; tests/TuiVision.Core.Tests/TConsoleBufferTests.cs::TConsoleBuffer_TrySetCell_ReturnsFalseOutsideBounds | Cell, resize, cursor, or snapshot drift can make visible proof dishonest. |
| `C032` | Rendering und Snapshots / Rendering and snapshots | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TGroupTests.cs::TGroup_Draw_VisibleChild_WritesExpectedCellsToBuffer; tests/TuiVision.Controls.Tests/TStaticTextTests.cs::TStaticText_Draw_WritesMultiLineTextIntoOwnerBuffer | Cell, resize, cursor, or snapshot drift can make visible proof dishonest. |
| `C033` | Größe, Cursor und Palette / Size, cursor, and palette | `IntentionalModernization` | `CorroboratesModernization` | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs::Driver_Present_PublishesSnapshotInsteadOfLiveBuffer; tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs::Driver_Resize_PreservesVisibleIntersection | Cell, resize, cursor, or snapshot drift can make visible proof dishonest. |
## D12 - Tastatur, Maus und Eingabe / Keyboard, mouse, and input

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C034` | Tastaturübersetzung / Keyboard translation | `IntentionalModernization` | `CorroboratesOriginal` | tests/TuiVision.Compatibility.Tests/TKeyCodeTranslatorTests.cs::FromConsoleKey_WithCtrlModifier_SetsCtrlInShiftState; tests/TuiVision.Compatibility.Tests/TKeyCodeTranslatorTests.cs::FromConsoleKey_FunctionKeys_ProduceExpectedScanCodes | Input normalization or timing drift can duplicate actions or remove keyboard fallback. |
| `C035` | Maus-Ingress und Doppelklick / Mouse ingress and double-click | `IntentionalModernization` | `CorroboratesModernization` | tests/TuiVision.Drivers.Tests/ConsoleMouseIngressTests.cs::TryAccept_SecondMatchingPressWithin500Milliseconds_IsDoubleClick; tests/TuiVision.Drivers.Tests/ConsoleMouseIngressTests.cs::TryAccept_InvalidObservation_RejectsWithoutPartialEvent | Input normalization or timing drift can duplicate actions or remove keyboard fallback. |
| `C036` | Aktivierung, Drag und Fallback / Activation, drag, and fallback | `Aligned` | `CorroboratesOriginal` | tests/TuiVision.Controls.Tests/TProgramMouseIntegrationTests.cs::AppLoop_TitleDrag_ChangesWindowAndRenderedRegion; tests/TuiVision.Controls.Tests/TWindowMouseDragTests.cs::KeyboardMoveMode_RemainsCompleteFallback | Input normalization or timing drift can duplicate actions or remove keyboard fallback. |
## D13 - Zeichensatz, Fonts und Terminal / Charset, fonts, and terminal

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C037` | Zeichensatz und Fonts / Charset and fonts | `IntentionalModernization` | `CorroboratesModernization` | tests/TuiVision.Drivers.Tests/TerminalCharsetAndFontTests.cs::CharsetMapper_Koi8R_MapsKnownBytes; tests/TuiVision.Drivers.Tests/TerminalCharsetAndFontTests.cs::FontFixture_InvalidShapeFormatOrPath_IsNotPublished | Host capability overclaim or mapping drift can render incorrect text or terminal state. |
| `C038` | Terminal-Emulationssubset / Terminal emulation subset | `IntentionalModernization` | `CorroboratesModernization` | tests/TuiVision.Drivers.Tests/TerminalSessionTests.cs::Session_CsiSgr_MapsSixteenColorsAndReset; tests/TuiVision.Drivers.Tests/TerminalSessionTests.cs::Session_InvalidOrUnsupportedSequence_IsAtomic | Host capability overclaim or mapping drift can render incorrect text or terminal state. |
| `C039` | Hostprofil und Fallback / Host profile and fallback | `IntentionalModernization` | `CorroboratesModernization` | tests/TuiVision.Drivers.Tests/TerminalProfileTests.cs::Profile_UnavailableFontOrHost_UsesSafeFallbackAndUnsupportedStatus; tests/TuiVision.Drivers.Tests/TerminalProfileTests.cs::HostDetector_ClassifiesControlledHostsWithoutPhysicalClaim | Host capability overclaim or mapping drift can render incorrect text or terminal state. |
## D14 - Kompatibilität und native Auslassungen / Compatibility and native omissions

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C040` | Treiberkonsolidierung / Driver consolidation | `IntentionalModernization` | `DivergesFromOriginal` | tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs::Driver_Assembly_ContainsNoPlatformSpecificReferences; tests/TuiVision.Drivers.Tests/TConsoleDriverConsolidationTests.cs::CapabilityMap_HasExactlyFiveBuckets | Platform consolidation can hide an unsupported native capability if fallback states are not explicit. |
| `C041` | Bewusste native Auslassungen / Conscious native omissions | `ConsciouslyOmitted` | `DivergesFromOriginal` | tests/TuiVision.Drivers.Tests/PortingStatusLedgerTests.cs::LedgerFile_OmittedRowsHaveRationale; tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs::CapabilityMap_MouseInputBucket_DocumentsIntentionalOmission | Platform consolidation can hide an unsupported native capability if fallback states are not explicit. |
| `C042` | Plattformfähigkeiten / Platform capabilities | `IntentionalModernization` | `DivergesFromOriginal` | tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs::CapabilityMap_DisplayAdaptationBucket_ReferencesManagedCharsetAndFontContracts; tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs::HistoricalInventory_ContainsAllExpectedPlatformFamilies | Platform consolidation can hide an unsupported native capability if fallback states are not explicit. |
## D15 - Barrierefreiheit / Accessibility

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C043` | Semantischer Widget-Text / Semantic widget text | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Core.Tests/AccessibleContractsTests.cs::AccessibleWidget_OptInContract_ExposesSemanticTextAndFocusCapability; tests/TuiVision.Examples.SmokeTests/A11yFrameworkSmokeTests.cs::A11yFramework_AppLoop_PreservesTextIdentityAndHonestFallbackInNarrowViewport | Missing semantic text or focus propagation can make controls inaccessible without changing visual behavior. |
| `C044` | Fokusankündigungen / Focus announcements | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Controls.Tests/AccessibilityFrameworkTests.cs::FocusTransition_DesktopDescendant_ReachesProgramBroadcast; tests/TuiVision.Controls.Tests/AccessibilityFrameworkTests.cs::FocusTransition_SameTarget_DoesNotEmitSecondAnnouncement | Missing semantic text or focus propagation can make controls inaccessible without changing visual behavior. |
| `C045` | Shortcuts und hoher Kontrast / Shortcuts and high contrast | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Controls.Tests/AccessibilityFrameworkTests.cs::MenuBar_GetAccessibleShortcuts_ExcludesNonExecutableItems; tests/TuiVision.Controls.Tests/AccessibilityFrameworkTests.cs::HighContrast_HasNamedDistinctSemanticRoles | Missing semantic text or focus propagation can make controls inaccessible without changing visual behavior. |
## D16 - Smoke- und Proof-Helfer / Smoke and proof helpers

Deutsch: Historische Absicht, aktuelles Verhalten, Modernisierungsgrund und
Quellpfade stehen vollständig im kanonischen JSON. Die getrennte Free-Vision-
Relation erläutert die Zweitmeinung, ohne die Primärentscheidung zu ersetzen.

English: Historical intent, current behavior, modernization rationale, and
source paths are complete in the canonical JSON. The separate Free Vision
relation explains the second opinion without replacing the primary decision.

| ID | Vertrag / Contract | Primary decision | Free Vision relation | Concrete proof | Risk |
|---|---|---|---|---|---|
| `C046` | App-Loop-Primärnachweis / App-loop primary proof | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Controls.Tests/TProgramMouseIntegrationTests.cs::AppLoop_ClickFocusAndActivation_ProducesStateViewAndCellProof; tests/TuiVision.Examples.SmokeTests/TvEditSmokeTests.cs::TvEdit_AppLoop_Edits_Visible_Buffer_And_Status | A helper that bypasses real application logic can create false confidence. |
| `C047` | View-Tree- und Cell-Nachweis / View-tree and cell proof | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Examples.SmokeTests/Wave1VisualSmokeMatrixTests.cs::Wave1VisualMatrix_Requires_All_Primary_Proof_Layers; tests/TuiVision.Examples.SmokeTests/Wave4VisualSmokeMatrixTests.cs::Wave4_Matrix_Proves_All_Five_Projects_Without_Linked_Type_Identity | A helper that bypasses real application logic can create false confidence. |
| `C048` | Helferrolle und Proof-Grenze / Helper role and proof boundary | `IntentionalModernization` | `NotApplicable` | tests/TuiVision.Examples.SmokeTests/Wave2InteractiveSmokeMatrixTests.cs::DirectHelperClassification_Records_Setup_And_Supplemental_Usage; tests/TuiVision.Examples.SmokeTests/Wave1VisualSmokeMatrixTests.cs::Wave1VisualMatrix_Rejects_Weak_Or_Pending_Proof | A helper that bypasses real application logic can create false confidence. |
