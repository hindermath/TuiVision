# Featureverlauf und technische Referenzen / Feature history and technical references

Beginne mit [AGENTS.md](../AGENTS.md). Die folgenden Abschnitte gelten für die jeweils genannten Einstiege. Pfade in Beispielen beziehen sich auf den Repository-Stamm.

Start with [AGENTS.md](../AGENTS.md). The following sections apply to the named entry files. Paths in examples are relative to the repository root.

## Active Feature Context

Einstiege / Entry files: `AGENTS.md`

### 004-editor-file-help-streams
- Current implementation baseline: execute the phase-6 increment from `specs/004-editor-file-help-streams/spec.md` and `specs/004-editor-file-help-streams/plan.md`
- Scope is limited to reusable framework components in `src/TuiVision.Controls` and `src/TuiVision.Serialization`: `TEditor`, `TMemo`, `TFileEditor`, `TEditWindow`, file/dialog/history helpers, help topics/viewers/windows, stream primitives, and named resource containers
- Out of scope for this increment: example applications such as `tvedit`, `bhelp`, and `helpdemo`; driver consolidation; calculator/macros/OS-shell integrations; and unrelated specialized widgets
- Editor flows must cover text editing, insert/overwrite behavior, clipboard-oriented actions, search/replace, modified-state handling, explicit safe-close decisions before unsaved changes are discarded, and distinct overwrite decisions when save conflicts occur
- Integration coverage for this feature must explicitly include event-loop-aware shell interaction, focus transitions, menu execution, and dialog interaction rather than relying on those behaviors only implicitly
- File flows must keep directory navigation, file lists, current file-information metadata, wildcard filtering, manual path entry, and history recall synchronized inside reusable dialogs
- Help flows must support context-based topic lookup, cross-reference navigation, and fallback content for missing contexts
- Stream/resource flows must preserve named lookup semantics and reject malformed persisted input explicitly, including truncated, trailing, unknown-type, and cyclic payload failures
- Planning decisions now fixed for this feature: dedicated runtime help files, shared-reference preservation without cyclic-graph support, exact case-sensitive resource keys, `LF` default for new files, preserved line endings for loaded files, and explicit overwrite decisions after external file changes

### 005-driver-consolidation-m07
- Current planning baseline: execute the Phase-7 increment from `specs/005-driver-consolidation-m07/spec.md` and `specs/005-driver-consolidation-m07/plan.md`
- Scope is limited to the managed driver baseline in `src/TuiVision.Drivers.Console`, the supporting validation in `tests/TuiVision.Drivers.Tests`, and the proof ledger `docs/porting-status.md`
- Out of scope for this increment: mandatory example waves, full closure of the Phase-8 entrance gate, new source modules, native bindings, and any one-to-one recreation of the historical per-OS driver split
- The proof ledger must cover every historical `.cc` implementation file in `tv203s/contrib/tvision/classes` with one mandatory primary target, optional secondary targets, status, evidence, and rationale
- Linux and Windows/WSL compatibility checks are required as reviewable evidence for this phase, but may still be manual or semi-automated rather than mandatory CI gates
- Planning decisions now fixed for this feature: `.cc` files are the formal `M-07` ledger scope, ancillary `.c`/`.h` files may appear only as rationale support, capability buckets replace per-OS lineage as the review model, and Phase 7 remains distinct from the later full Phase-8 gate closure

### 006-close-phase8-gate
- Current planning baseline: execute the Phase-8 entrance-gate closure from `specs/006-close-phase8-gate/spec.md` and `specs/006-close-phase8-gate/plan.md`
- Scope is limited to closing the remaining `M-07` proof and gate evidence in `docs/porting-status.md`, `Pflichtenheft.md`, the existing module test projects plus any required Compatibility-focused validation additions, coverage evidence, formatting evidence, and API-documentation validation for `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console`
- Out of scope for this increment: starting any of the 25 mandatory example waves, introducing substitute follow-on example scope from `TVDEMOS/` or `TVFM/`, reopening Phase-7 capability-bucket decisions except where pending proof rows require final status updates, and unrelated new framework features
- Every historical `.cc` row in `docs/porting-status.md` must end in a final proof state of `portiert + getestet` or `bewusst ausgelassen + Begruendung`; no `portiert + Test ausstehend` rows may remain once the gate is claimed closed
- Gate closure must provide explicit build, full-test, coverage, formatting, and conditional API-doc evidence, and must keep the mandatory example waves blocked until the closure is formally recorded
- Planning decisions now fixed for this feature: `docs/porting-status.md` remains the authoritative M-07 ledger; `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` must each satisfy the explicit 70 % line-coverage gate with assembly-specific evidence; `tests/TuiVision.Compatibility.Tests/` is the planned dedicated Compatibility fallback suite when shared tests are insufficient; placeholder-only or no-op-only modules cannot satisfy that gate; gate-scope removals must update the proof surfaces in the same change; skipped or ignored gate-scoped tests require recorded tracked-issue references; unresolved local-versus-CI coverage conflicts block closure; and the final Phase-8 decision requires a dedicated closure marker or commit reference

### 012-interactive-wave2-demos
- Current implementation status: interactive Wave-2 demo polish is implemented on branch `012-interactive-wave2-demos`; final validation evidence is tracked in `specs/012-interactive-wave2-demos/pr-evidence.md`.
- Delivered scope is limited to making the eleven Wave-2 examples visibly operable at normal runtime: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`; matching event-loop smoke tests in `tests/TuiVision.Examples.SmokeTests/`; guides, README, `pr-evidence.md`, and proportional architecture/security/A11Y/statistics evidence.
- Before wiring or accepting each example, review the relevant historical `.c`/`.cc` source and any important matching headers under `tv203s/` as read-only reference, capture the original demo intent, and document intentional user-visible deviations in guide or PR evidence.
- Every example must show first-screen purpose text, expose primary behavior through menu, keyboard, status, or command paths, and update visible text-first feedback after each demonstrated operation.
- Primary smoke proof must run `app.Run()` or the equivalent real application loop with injected `TEvent`, command, or key events. Direct helper methods may support setup or supplemental assertions only.
- `examples/Demo` is the P1 vertical slice and must prove at least three visible behaviors before the pattern is spread across the rest of the examples.
- File/path and dialog-designer flows stay read-only toward user data: use source-controlled fixtures, fixed repository paths, or test temporary directories; do not read arbitrary user file contents or persist user history as proof.
- Wave 3 and Wave 4 examples, mandatory mouse-only operation, broad framework redesign, new runtime dependencies, databases, external services, and DocFX publishing-model changes are out of scope.
- Next open mandatory example scope after this feature is Wave 3: editor, file, help, and stream demos such as `tvedit`, `bhelp`, and `helpdemo`.

### 013-wave2-visual-component-remediation
- Current implementation status: visual component remediation is implemented on branch `013-wave2-visual-component-remediation`; final validation evidence is tracked in `specs/013-wave2-visual-component-remediation/pr-evidence.md`.
- Scope is limited to the eleven Wave-2 examples: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`.
- Shared runtime support for the remediated examples lives in `examples/Shared/Wave2Runtime.cs` and is linked into the eleven scoped example projects.
- The primary parity proof is the visible UI composition itself: controls, dialogs, windows, view groups, scroll groups, progress displays, input/list/combo composition, or another stable visible runtime state.
- Each example must use the three-layer model: visible main component, real `TStatusLine` feedback, and keyboard-reachable `Help -> Description`.
- Primary smokes must drive the real app loop and combine concrete state assertions with view-tree proof plus buffer/cell rendered visibility proof at expected positions or regions.
- Historical C/C++ sources under `tv203s/` remain read-only intent references; intentional user-visible deviations must be documented in plan, tasks, guides, feature evidence, or PR evidence.
- AI-SBOM is `N/A` for this feature while AI is only development/agent tooling; re-evaluate if runtime/product AI, models, datasets, AI infrastructure, or delivered AI components enter scope.
- Wave 3/4 functionality, broad framework redesign, mandatory mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, and new runtime dependencies are out of scope.

### 014-wave1-functional-hardening
- Current implementation status: Wave-1 functional hardening is implemented on branch `014-wave1-functional-hardening`; final validation evidence is tracked in `specs/014-wave1-functional-hardening/pr-evidence.md`.
- Scope is limited to `Desklogo`, `MsgCls`, `Tutorial` steps `tvguid01` through `tvguid16`, and `Videomode`.
- The primary proof surface is `specs/014-wave1-functional-hardening/pr-evidence.md`, which records historical source, C# behavior, proof method, helper classification, negative/fallback proof, missing-core decisions, intentional deviations, validation, and the archived Lastenheft path.
- Managed runtime behavior is proven by executable smoke tests; evidence-only proof remains allowed only for explicitly documented no-runtime-target deviations.
- Helper or headless paths may be `PrimaryProof` only when they execute real example or application logic through public commands, events, application methods, or stable public state with concrete assertions; 014 added `PrimaryProof`, `SupplementalProof`, `SetupOnly`, and `LegacyOrTemporary` helper taxonomy.
- Historical C/C++ sources under `tv203s/` remain read-only intent references. `set-logo.cc` and `tv_logo.cc` are Desklogo asset/generator boundary context only.
- Validation baseline: targeted Wave-1 smokes 38/38 passed, full example smokes 91/91 passed, full Release tests 496/496 passed, coverage gate exceeded 70% for all required assemblies, `docfx docfx.json` passed with 0 warnings/errors, and Playwright/axe DocFX smoke passed 2/2 via explicit local-server workaround when sandboxed webserver startup is blocked.
- Feature 015 completed the didactic comment pass; the next open cross-cutting step is `requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.016-secure-development-hardening.md` before Wave-1 visual remediation.
- Wave-1 visual remediation, Wave 2/3/4 behavior, broad framework redesign, mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, new runtime dependencies, and runtime/product AI are out of scope.

### 015-didactic-comment-hardening
- Current implementation status: the selective didactic inline-code-comment hardening is implemented; final evidence is in `specs/015-didactic-comment-hardening/pr-evidence.md`.
- Scope is limited to selective didactic inline, block, file, or module comments, feature evidence in `specs/015-didactic-comment-hardening/pr-evidence.md`, and affected guidance/evidence surfaces.
- Review must cover central framework flows and relevant smoke-test helpers: event/command/dispatch, focus transitions, view hierarchy, StatusLine, Help/Description, dialog state, validation/rejection, buffer/cell proof, rendering snapshots, terminal fallbacks, historical Turbo Vision deviations, and proof helpers.
- Each reviewed area must receive exactly one decision: `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening`.
- Comments remain moderate and reason-focused: explain why, trade-off, constraint, historical deviation, or proof boundary; avoid restating obvious code; use German-first/English-second CEFR-B2 text for didactic explanation blocks.
- XML comments remain the API/DocFX surface; pure `//` or `/* */` hardening does not trigger DocFX. XML/API/generated docs/navigation/guides changes trigger the normal DocFX plus A11Y path.
- No runtime behavior change, API change, new dependency, new example porting, broad framework revision, Wave-1 visual remediation, or runtime/product AI is in scope.

### 016-secure-development-hardening
- Current implementation status: the project-wide secure-development hardening is implemented; final evidence is in `specs/016-secure-development-hardening/pr-evidence.md`.
- The durable control matrix covers all 157 `CL-XX-NN` controls with `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or `FollowUp` and complete ownership, risk, evidence, and re-evaluation fields.
- Delivered remediation is bounded to explicit malformed persistence rejection, immutable workflow dependencies, supply-chain automation, root disclosure guidance, and safe Bash/PowerShell archive-script parity.
- Local acceptance is 498/498 Release tests plus line coverage above 70% for Core, Controls, Serialization, Compatibility, and Drivers.Console; DocFX/axe and remote OS/CI proof remain delivery gates.
- Human legal, provider, organization, and agent-platform decisions remain `Open`; release provenance, reproducible-build/lock maturity, and RFC 9116 remain named follow-ups rather than implicit claims.
- The next open prioritized intake is `requirements/intakes/archive/Lastenheft_03_EditorHelpAndResourcesHardening.018-editor-help-resources-hardening.md` before Wave-3 visual porting.

### 017-wave1-visual-component-remediation
- Current implementation status: Wave-1 visual component remediation is implemented; final evidence is in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- `Desklogo`, `MsgCls`, all 16 Tutorial tokens, and `Videomode` now use the three-layer model: visible main state, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, and rendered buffer/cell evidence. The acceptance matrix contains four app rows plus 16 unique Tutorial rows; direct helpers are not primary proof.
- Shared composition in `examples/Shared/Wave1Runtime.cs` uses existing framework controls. Desklogo and MsgCls are `UseExistingFramework`; Tutorial and Videomode are bounded `IntentionalDeviation` decisions.
- Historical sources under `tv203s/` remain read-only. No functional re-port, Wave-2/3/4 behavior, broad framework redesign, new dependency, persistence, external service, or runtime/product AI entered scope.
- The complete example-smoke suite passes 101/101 locally; final repository, coverage, DocFX, A11Y, and remote checks remain delivery gates until recorded in feature evidence.
- Feature 018 closes the editor/help/resources intake; the next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave3-Visual-Component-Porting.019-wave3-visual-component-porting.md`.

### 018-editor-help-resources-hardening
- Current implementation status: Wave-3 editor/help/resources hardening is implemented; final evidence is in `specs/018-editor-help-resources-hardening/pr-evidence.md`.
- Existing `TEditor`, `TFileEditor`, `TEditWindow`, `THelpViewer`, and `THelpWindow` flows are retained and proven as coherent open/edit/search/replace/save, safe-close/conflict, persisted navigation, back, and fallback paths.
- `THelpSourceCompiler` provides a bounded `.topic` and `{text[:alias]}` source contract with strict UTF-8, deterministic contexts, forward-reference resolution, stable diagnostics, and atomic no-partial-model failure.
- `TLocalizedResourceLookup` uses explicit exact-language, caller-ordered fallback, then neutral keys over case-sensitive `TResourceFile` storage without ambient locale, gettext, codepage, or new dependency scope.
- Persisted resources reject duplicate keys and negative payload lengths; persisted help rejects negative counts and unresolved or invalid reference ranges before presentation.
- Framework decisions are `UseExistingFramework` for editor/file, and bounded `SmallFrameworkFix` for help graph validation, compiler, resources, and i18n. Historical sources remain read-only.
- Wave-3 examples, mouse, terminal/charset/font work, TP7, broad redesign, services, and dependencies remain outside 018.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave3-Visual-Component-Porting.019-wave3-visual-component-porting.md` for `bhelp`, `helpdemo`, `i18n`, `tvedit`, and `tvhc`.

### 019-wave3-visual-component-porting
- Current implementation status: Wave-3 visual component porting is implemented locally; final evidence is in `specs/019-wave3-visual-component-porting/pr-evidence.md`.
- `BHelp`, `HelpDemo`, `I18n`, `TvEdit`, and `TvHc` use visible main components, a real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, rendered buffer/cell evidence, status, and description. The Wave-3 matrix passes 14/14 locally, including five constrained `48x16` layouts.
- Framework decisions are `UseExistingFramework` for TvEdit, HelpDemo, I18n, and TvHc, and bounded `IntentionalDeviation` for BHelp because the proprietary unchecked Borland `.tch` decoder is omitted.
- Embedded/source-controlled learning content and test-owned temporary paths are the only data boundaries. Historical sources remain read-only.
- Mouse interaction, terminal/charset/font work, Wave 4, broad redesign, services, new dependencies, and runtime/product AI remain outside 019.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_04_MouseSupportAndInteraction.020-mouse-support-interaction.md`.

### 020-mouse-support-interaction
- Current implementation status: bounded mouse support and interaction hardening is implemented locally; final evidence is in `specs/020-mouse-support-interaction/pr-evidence.md`.
- `ConsoleMouseIngress` accepts only complete bounded SGR 1006 left press, pressed move, and release reports and publishes zero or one existing `TEvent`; malformed syntax, range, button, capability, and phase input is rejected atomically.
- `TGroup` routes mouse down to one topmost visible target, transfers focus only to selectable targets, and preserves existing exactly-once control commands. Nested mouse coordinates traverse the full owner chain.
- The only mouse drag contract is moving a `TWindow` from its title row. Owner bounds, release, Escape, capability loss, disable, removal, shutdown, and the existing `Ctrl+F5` keyboard fallback are proven.
- Interactive macOS/Linux terminals and WSL use the SGR capability contract; native Windows Console and redirected/headless I/O remain honest `Unsupported` boundaries. Wheel, hover, touch, extra buttons, full protocol parity, and additional drag targets remain out of scope.
- Primary proof runs through `TProgram.GetEvent` and `app.Run()` and combines concrete focus/command/drag state, target identity, visible text, and rendered buffer/cell assertions. Historical sources remain read-only.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_05_TerminalCharsetAndEmulation.021-terminal-charset-hardening.md`.

### 021-terminal-charset-hardening
- Current implementation status: terminal and charset hardening is implemented locally; final evidence is in `specs/021-terminal-charset-hardening/pr-evidence.md`.
- `TerminalSession` provides a bounded in-process transcript, cursor, 16-color attributes, 4,096-cell FIFO history, deterministic resize/reset/lifecycle, and atomic C0/CSI subset recovery without a process, shell, or PTY.
- `TerminalCharsetMapper` uses Unicode plus one fixed KOI8-R table and U+FFFD replacement. `BitmapFontFixture` validates only raw 8x16/256/4,096-byte metadata; no host codec, font installation, or historical generator is used.
- `TerminalProfile` uses a closed `System.Text.Json` schema. Invalid schema is rejected as a whole; unavailable font/host capability uses an observable safe default and `Unsupported` status.
- `TTerminalView` reuses the Driver-owned session and existing Controls app loop. Primary proof combines session/profile state, concrete view identity, text status, cursor, rendered cells, controlled keyboard input, and deterministic quit.
- Historical terminal, Cyrillic, font, Eterm, and XTerm sources remain read-only intent. Full ANSI/VT/XTerm parity, visible Wave-4 examples, host mutation, new dependencies, services, persistence, and runtime/product AI remain outside 021.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave4-Visual-Component-Porting.022-wave4-visual-component-porting.md`.

### 022-wave4-visual-component-porting
- Current implementation status: Wave-4 visual component porting is implemented locally; final evidence is in `specs/022-wave4-visual-component-porting/pr-evidence.md`.
- `Terminal`, `Cyrillic`, `Fonts`, `ETerm`, and `XTerm` provide a visible main component, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, exact view identity, rendered buffer/cell evidence, controlled operation, fallback, description, and deterministic host classification. Narrow viewports preserve example identity and status meaning.
- Terminal, Cyrillic, and Fonts are `UseExistingFramework`; ETerm and XTerm are bounded `IntentionalDeviation` immutable manifests that do not parse or execute historical configuration.
- The copied raw 8x16 fixture is byte-identical to historical `font.016`; historical sources remain read-only. No process, shell, PTY, host font/codepage/locale/keyboard mutation, X resource database, terminfo, external command, or new dependency entered scope.
- Physical host observation remains separate from deterministic in-process and remote-CI evidence. Unsupported capabilities use text-first fallbacks and never claim unavailable native behavior.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_06_A11Y_Framework.023-a11y-framework.md`.

### 023-a11y-framework
- Current implementation baseline: deliver the managed accessibility layer from `specs/023-a11y-framework/` and `requirements/intakes/archive/Lastenheft_06_A11Y_Framework.023-a11y-framework.md`.
- Scope is limited to opt-in `IAccessibleWidget` text, typed `cmFocusChanged` announcements, immutable shortcut queries for `TMenuBar`/`TStatusLine`, explicit `TColorScheme.HighContrast`, keyboard coverage inventory, the `A11yFramework` reference app, and DocFX/Axe evidence.
- Focus announcements reuse one existing broadcast and propagate descendant focus to the shell; non-migrated views remain compatible without fabricated labels.
- Keyboard acceptance requires behavioral proof or a concrete `N/A` for Tab, Shift+Tab, arrows, Enter, and direct shortcuts across every inventoried selectable control family.
- Native AT-SPI, NSAccessibility, UI Automation, speech, full-control migration, terminal-wide WCAG claims, Wave 1-4 remediation, and Feature 024 are out of scope.
- Historical Turbo Vision has no direct equivalent for the modern semantic A11Y contracts; relevant focus/menu/status sources are read-only intent context only.
- The former post-023 Wave-5 statement is historical; Feature 028 completed the TV203/Free Vision closure, Feature 029 is the sole immediate intake, and the separate mandatory Feature-030 magiblot evolution audit follows before any finding-derived hardening.

### 024-tv203-freevision-conformance-audit
- Current planning baseline: the original audit and merge remain complete, but consumer-review Revision 2 in `specs/024-tv203-freevision-conformance-audit/consumer-readiness-review.md` supersedes the zero-finding forward decision.
- Borland documentation and `tv203s/` remain primary; official Free Vision commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` is external secondary evidence only. `TVDEMOS/` and `TVFM/` are read-only consumer evidence.
- The inventory remains 151 historical `.cc` rows, 119 maintained production `.cs` files, 176 exported public types, 16 domains, and 48 contracts.
- Revision-2 decisions are 7 `Aligned`, 27 `IntentionalModernization`, 1 `ConsciouslyOmitted`, 8 `BehavioralDrift`, and 5 `EvidenceGap`.
- The 13 accepted findings route exactly nine items `F001`-`F009` to `Core025` and four items `F010`-`F013` to `ComponentData026`; both are closed, while Wave 5 and Wave 6 remain blocked through Feature 028, Feature 029, Feature 030, all combined finding-derived hardening, and the new independent closure.
- Revision 2 changes audit data, validation, evidence, requirements, and ordering only; it does not implement runtime behavior or port examples.

### 027-pre-wave5-conformance-closure
- Feature 027 remains valid historical closure evidence: feature merge `35414af` and causal closeout PR #67 merge `1da2b211e84221db87ab9f959b7b40d3ae2b01f0` are complete.
- Its original 13/34/1/0/0 decision set and zero-finding Wave-5 release statement are superseded for forward planning by Feature-024 Revision 2, not rewritten retroactively.
- Final evidence is in `specs/027-pre-wave5-conformance-closure/closure-evidence.md`; all 90 historical tasks are complete.

### 025-core-runtime-conformance-hardening
- Current implementation status: all nine `Core025` findings are implemented and proven; final evidence is in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`.
- `F001`-`F009` close concrete event kinds, focus and group state, idle lifecycle, desktop stack, modal/close, shared command state, real keyboard ingress, and bounded generic drag through real-path red/green proof.
- Feature-024 resolution metadata records exactly 13 non-documentation-only closures from Features 025 and 026 while preserving the Feature-028 and Wave-5/Wave-6 gates.
- Feature 028 completed the independent closure after Feature 026; its archived intake is `requirements/intakes/archive/Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.028-pre-wave5-wave6-conformance-closure.md`.

### 026-component-data-conformance-hardening
- Current implementation status: all four `ComponentData026` findings are implemented and proven; final evidence is in `specs/026-component-data-conformance-hardening/pr-evidence.md`.
- `F010`-`F013` close dialog completion/child validation, phase-aware input validation, mode-aware typed file outcomes, and allowlisted named UI-resource composition through real-path red/green proof.
- Feature-024 metadata now contains 139 maintained source files, 211 exported public types, and exactly 13 closed finding resolutions; Wave 5 and Wave 6 remain blocked through the combined Terminal.GUI/magiblot-derived closure after Features 029 and 030.

### 028-pre-wave5-wave6-conformance-closure
- Current implementation status: the evidence-only closure independently revalidated all 13 findings, seven real-path slices, and 13 protected consumer groups without a product, API, dependency, example, or protected-source change.
- The existing TV203/Free Vision gate is `ReadyForTerminalGuiAudit`; 12 consumer groups use the existing framework and destructive Wave-6 policy remains one bounded `FollowUpHardening`.
- Both waves remain `BlockedPendingTerminalGuiAudit`; Feature 029 is the sole immediate intake and Feature 030 is the mandatory successor before hardening or closure.

### 029-tv203-freevision-terminalgui-conformance-audit
- Current implementation status: the read-only audit is complete locally; final evidence is in `specs/029-tv203-freevision-terminalgui-conformance-audit/pr-evidence.md`.
- Turbo Vision 2.0.3 remains authoritative; pinned Free Vision remains secondary evidence, and Terminal.GUI v1.9.0 at commit `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3` is an additional modern C# implementation opinion.
- All `C001`-`C048`, 16 domains, 25 pinned sources, and 13 Wave consumer groups are covered. No `C049+`, `CandidateFinding`, or `ProductDecision` was admitted.
- The audit changes no runtime, API, dependency, example, historical, consumer, Free Vision, or Terminal.GUI source. It hands 48 `TGO###` observations to Feature 030 and creates no hardening or closure Lastenheft itself.
- Feature 030 completed the combined TG/MB audit locally; Feature 031 is the sole next intake after merge, and Wave 5 plus Wave 6 remain blocked through its independent closure.
- After every autonomous run, promote only reproducible provider-neutral preset learning through a Home-Baseline patch release and exact tag-ZIP adoption before the next run; record `NoPromotion` without an empty branch or PR. The consolidated pre-Wave-5 catalog update is submitted as `github/spec-kit#3569`; its external processing does not grant authority and does not indefinitely block the already installed v0.2.2 field run.

### 030-tv203-magiblot-evolution-audit
- Current implementation status: the read-only audit data and targeted validator are locally complete; final repository, documentation, coverage, remote review, and delivery gates remain.
- The exact magiblot pin is commit `57b6f56b38e0ee75240a80a10ee0e11470c24693`, tree `96dd03873955689ff0a79f6c8107a8148fe1ebd6`, with the multipart COPYRIGHT hash `66220bae...548`; its direct C++ lineage remains a modernization witness, never a normative source.
- Fifty selected source records support 48 relations: 27 `CorroboratesOriginal`, 12 `CorroboratesModernization`, 6 `AlternativeModernization`, and 3 `NotApplicable`. The 48 `MB*` decisions are 39 `AlreadySatisfiedWithNewEvidence`, 6 `IntentionalDeviation`, and 3 `RejectedComparison`.
- All 48 `TGO*` and 48 `MB*` observations have one combined disposition. No reproducible `CF*` finding or `ProductDecision` exists, so no hardening intake is generated; `requirements/intakes/archive/Lastenheft_16_Pre-Wave5-Wave6-Combined-Conformance-Closure.031-combined-conformance-closure.md` defines Feature 031 as the active closure intake.
- Runtime, API, dependency, example, consumer, historical, and external sources remain unchanged. Wave 5 and Wave 6 stay `BlockedPendingCombinedConformanceClosure`.
- `requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md` remains a binding deferred intake only after the complete Wave-6 closeout and receives its feature number then.

### 031-combined-conformance-closure
- Current implementation status: the evidence-only independent closure is complete; final evidence is in `specs/031-combined-conformance-closure/delivery-closeout.md`.
- Exactly 48 contracts, 13 consumers, 48 TGO and 48 MB observations, 96 dispositions, all accepted source pins and hashes, and the closed `F001`-`F013` set are revalidated.
- Product runtime, public APIs, dependencies, packages, projects, examples, consumers, historical sources, and external sources remain unchanged.
- Feature 031 made Wave 5 `Eligible`; Features 032 and 033 have since delivered both stages. Wave 6 remains only `ConditionallyReady` and still requires review of the actual combined Wave-5 delta.

### 032-wave5-tp7-functional-porting
- Current implementation status: the functional Wave-5 stage is delivered; all 15 read-only `TVDEMOS/*.PAS` sources have one role, all six consumers use the existing framework, and ten `Tp7*` projects provide normal plus controlled-smoke entry points.
- Primary proof covers calculator, central apps, resources, deterministic domain state, mouse capability/fallback, and exact 15/6/10/10 traceability through real app loops, state, view, and rendered cells.
- File, resource, help, generator, and mouse boundaries remain controlled; no host state, arbitrary user data, dependency, historical source, or broad framework behavior is changed.
- `requirements/intakes/archive/Lastenheft_18_Wave5-TP7-Showcase-Remediation.033-wave5-tp7-showcase-remediation.md` is the archived intake derived from the ten concrete Stage-2 deltas. Feature 033 is fully delivered through PR #96 and merge `d476e63`; Wave 6 remains blocked pending the actual combined Wave-5 delta review.

### 034-wave5-combined-delta-closure
- Current implementation status: Feature 034 is fully delivered through PR #99, final head `016692d`, merge `7fb52e2`, and its causal closeout.
- The read-only audit proves exact 15 source, 6 consumer, 10 example, 10 functional-proof, 10 showcase-closure, and 10 guide/launch cardinalities. All ten decisions are `AcceptedIntentionalDeviation`; there are zero open `Gap` dimensions, Candidate Findings, Product Decisions, owner groups, or hardening intakes.
- Product runtime, APIs, dependencies, projects, examples, framework code, `TVDEMOS/`, `TVFM/`, `tv203s/`, and external comparison sources remain unchanged. The final 22-check matrix and 13 exact-head gates passed; checkout-neutral SHA-256 and Git-blob proof passed on Ubuntu, macOS, and Windows.
- Wave 5 is `Closed`. Feature 035 is delivered through PR #101, final head `207e807`, and merge `52f77fa`; it provides Wave-6 Stage 1.

### 036-wave6-tvfm-showcase-remediation
- Feature 036 is fully delivered through PR #104, final head `a0d5062`, and merge `559bffb`.
- The exact closure contains ten `W6S` showcase decisions, one `ShowcaseComplete` entry-point decision, and 24 validated read-only `TVFM/` source hashes. The Windows provider correction makes historical `.PAS`/`.BAT` text hashes checkout-neutral while preserving byte-exact resource hashes.
- Feature-035 controlled-root, path, search, preview, viewer, one-shot intent, mutation, dependency, process, shell, host, and arbitrary-user-data boundaries remain unchanged. `TVFM/`, `TVDEMOS/`, and `tv203s/` stay read-only.
- The independent Wave-6 closure is now the preferred active intake at `requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md`; it reserves Feature 037 without starting it. The post-Wave-6 portfolio audit remains blocked by that closure.

### Autonomous Red-Proof Completeness
- Before the first red test batch, review imports, public XML docs, harness helpers, focus/ownership assertions, and linked-source assembly identity.
- Group independent negative cases only as a bounded project-local red matrix with explicit failure boundaries and shared ownership.
- When source is linked into multiple assemblies, cross-project proof uses public contracts or state delegates and does not assume one CLR type identity.
- Keep a causal closeout evidence-only and single-commit-capable: do not require its own PR URL, reviewed-head result, or merge commit inside that same repository file; verify terminal facts externally without recursive closeout.
- When push and pull-request events create equivalent workflow sets, use pull-request-context checks as the gate and record duplicate runs as noise; cancel them only under an explicit safe workflow/concurrency contract.

## Active Technologies

Einstiege / Entry files: `AGENTS.md`

- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, docfx (004-editor-file-help-streams)
- Real local file-system interaction plus persisted binary help/resource files; no database layer in this increment (004-editor-file-help-streams)
- C# `latest` on .NET 10 (`net10.0`) + Existing modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; docfx for API documentation validation; GitHub Actions for existing CI (005-driver-consolidation-m07)
- Source-controlled Markdown evidence in `docs/porting-status.md`; no database storage; compatibility evidence may include repository notes and command output references (005-driver-consolidation-m07)
- C# `latest` on .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, existing MSTest suites plus any required Compatibility-focused validation additions, Coverlet coverage evidence, `dotnet format`, docfx, `Pflichtenheft.md`, and `docs/porting-status.md` for the formal Phase-8 entrance-gate proof (006-close-phase8-gate)
- Repository-visible proof artifacts only; no database layer, and no example-application delivery in this increment (006-close-phase8-gate)
- Source-controlled example projects under `examples/`; wave-1 examples (`desklogo`, `msgcls`, `tutorial`, `videomode`) delivered; 41 smoke tests green; next: Wave 2 Controls and Dialogs (007-port-wave1-examples)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation (008-controls-revision)
- In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage (008-controls-revision)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell and widget foundation from `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`, `TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions plus existing example-smoke infrastructure for downstream wave-2 readiness (009-controls-widgets-and-collections)
- In-memory UI state only in production; source-controlled planning, tests, proof artifacts, and already delivered downstream examples in `specs/`, `tests/`, `docs/`, and `examples/`; no database or external service storage (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream contex (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell, dialog, file, color, history, and widget types (`TDialog`, `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, `THistory`, `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, `TColorDisplay`, `TComboBox`, `TProgressBar`, `TParamText`); existing `TuiVision.Serialization` archive/resource foundation (`TRecordRegistry`, `TRecordSerializer`, `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TResourceFile`, `TResourceCollection`, `pstream` family); MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (010-standard-dialogs-designer)
- In-memory dialog state and session-only history; real local file-system metadata for file-listing/validation only; source-controlled tests/proof artifacts; minimal persisted dialog-description fixture through existing serialization/resource primitives; no database or external service storage (010-standard-dialogs-designer)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing framework modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; new wave-2 example projects under `examples/`; existing `tests/TuiVision.Examples.SmokeTests/`; MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (011-port-wave2-examples)
- Runtime example state is in memory; standard-dialog file flows use real local file-system metadata only; `dlgdsn` may use source-controlled dialog-description fixtures through existing Serialization/resource primitives; no database, external service, persisted user history, or new dependency planned (011-port-wave2-examples)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet test stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (012-interactive-wave2-demos)
- Runtime example state is in memory. Dialog-designer and file/path demonstrations use source-controlled fixtures, fixed repository paths, or test temporary directories. The examples must not persist user history, write user data as part of normal demonstration, read arbitrary user file contents as proof, or add a database/external service. (012-interactive-wave2-demos)
- Runtime example state remains in memory. Controlled examples may use source-controlled fixtures, fixed repository paths, or test temporary directories for metadata, rendering, validation, or rejection proof. The feature must not add a database, external service, network dependency, persistent user history, or arbitrary user-file content reads. (013-wave2-visual-component-remediation)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (014-wave1-functional-hardening)
- Runtime example state remains in memory. Proof data is limited to existing source-controlled files, controlled example fixtures if needed, or test temporary directories. No database, external service, network dependency, persistent user history, arbitrary user-file content reads, or runtime/product AI storage is planned. (014-wave1-functional-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest/Coverlet validation; existing DocFX plus Playwright/axe web A11Y tooling when documentation triggers apply. No new runtime NuGet dependency is planned. (015-didactic-comment-hardening)
- Source-controlled Markdown evidence and guidance files only. Production code state and tests keep their current storage model. No database, external service, network dependency, persistent user history, runtime/product AI storage, or arbitrary user-file proof path is planned. (015-didactic-comment-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`); Bash and PowerShell 7 for repository tooling + Existing TuiVision projects, MSTest, Coverlet, DocFX, Playwright/axe, GitHub Actions, Gitleaks, and CycloneDX for .NET 6.2.0 as a repository-local tool. No new runtime package is planned. (016-secure-development-hardening)
- Source-controlled Markdown, YAML, shell/PowerShell scripts, a local .NET tool manifest, and test fixtures. Generated evidence is written to temporary or ignored directories. No database, service, credential, runtime AI, or user-data store is introduced. (016-secure-development-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing TuiVision modules, shared Wave-1 example composition, MSTest/Coverlet, DocFX, and Playwright/axe; no new package (017-wave1-visual-component-remediation)
- Runtime example state remains in-process and session-only; proof and governance use source-controlled Markdown, with no database, external service, persistent user history, arbitrary user-file proof, or runtime/product AI (017-wave1-visual-component-remediation)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, and `TuiVision.Drivers.Console`; no new packages (019-wave3-visual-component-porting)
- Embedded/source-controlled learning content and test-owned temporary files only (019-wave3-visual-component-porting)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Drivers.Console`, `TuiVision.Controls`, and reviewed `TuiVision.Compatibility` key translation; no new packages (021-terminal-charset-hardening)
- In-memory session/history state plus source-controlled JSON and raw 8x16 fixtures (021-terminal-charset-hardening)
- C# / .NET 10 for durable test-only validation; Markdown and JSON for evidence + existing framework assemblies, MSTest, `System.Text.Json`, Git, official pinned FPC source checkout (024-tv203-freevision-conformance-audit)
- repository-owned JSON and Markdown evidence; external Free Vision worktree under `/tmp`, never tracked (024-tv203-freevision-conformance-audit)
- C# / .NET 10 + existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, MSTest; no new package (025-core-runtime-conformance-hardening)
- in-process state only; audit JSON and Markdown evidence are repository-owned documentation (025-core-runtime-conformance-hardening)
- C# with latest language version on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Serialization`; no new package or runtime dependency (026-component-data-conformance-hardening)
- Existing bounded binary archive and `TResourceFile`; controlled temporary filesystem metadata for file-dialog proofs (026-component-data-conformance-hardening)
- C# / .NET 10 for test-only validation; JSON, Markdown, YAML, Bash and PowerShell evidence tooling + existing MSTest 4.0.1, Coverlet 6.0.4, DocFX, Playwright/Axe, Lynx, jq, xmllint, Git and GitHub Actions (028-pre-wave5-wave6-conformance-closure)
- repository-owned JSON and Markdown only; historical, consumer, and Free Vision sources remain read-only (028-pre-wave5-wave6-conformance-closure)
- C# / .NET 10 for test-only validation; JSON and Markdown for audit evidence + existing MSTest 4.0.1, System.Text.Json, Coverlet 6.0.4, DocFX, Playwright/Axe, Lynx, Git and GitHub Actions (029-tv203-freevision-terminalgui-conformance-audit)
- repository-owned Feature-029 JSON and Markdown; external Terminal.GUI checkout remains untracked under a temporary directory (029-tv203-freevision-terminalgui-conformance-audit)
- C# 14 / .NET 10 for test-only validation; JSON and Markdown for evidence + Existing BCL `System.Text.Json`, MSTest 4.0.1, existing repository scripts and workflows; no new package (031-combined-conformance-closure)
- Source-controlled closed JSON and Markdown evidence; temporary external checkouts under `/tmp` only (031-combined-conformance-closure)
- C# 14 / .NET 10 + Existing TuiVision.Core, TuiVision.Controls, TuiVision.Serialization, TuiVision.Compatibility, TuiVision.Drivers.Console, MSTest 4.0.1 and BCL System.Text.Json; no new package (032-wave5-tp7-functional-porting)
- Embedded UTF-8 fixtures, controlled test-temporary files, existing THelpFile and TResourceFile; no database or service (032-wave5-tp7-functional-porting)
- C# 14 / .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, DocFX and Playwright/Axe; no new package (035-wave6-tvfm-functional-porting)
- Source-controlled UTF-8/binary fixtures copied into test-owned temporary roots; controlled local filesystem only; no arbitrary user data, shell/external viewer, database or service (035-wave6-tvfm-functional-porting)
- C# 14 / .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest 4.0.1 and .NET BCL; no new package (036-wave6-tvfm-showcase-remediation)
- Existing source-controlled fixtures copied into process- or test-owned temporary controlled roots; no database, service, host profile, shell, process, PTY, network, or arbitrary-user storage (036-wave6-tvfm-showcase-remediation)

### 007-port-wave1-examples
- Current status: Wave 1 delivered (2026-03-28). `desklogo`, `msgcls`, `tutorial` (16 steps), `videomode` are ported, smoke-tested, and guide-documented.
- Wave 1 scope: `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`; shared smoke-test infrastructure in `tests/TuiVision.Examples.SmokeTests/`; guides in `docs/guides/examples/`.
- Next open scope: Wave 2 – Controls and Dialogs (requires Controls/Dialog layer as prerequisite before planning starts).
- Planning decisions now fixed: headless smoke seam via `bool headless` constructor parameter + `GetEvent()` override; in-process MSTest execution without external process spawning; bilingual German-first/English-second XML docs and comments at CEFR-B2; `DisplayModeCoordinator.ProbeResizeSupport()` cross-platform probe with CA1416 suppressed.

## Recent Changes

Einstiege / Entry files: `AGENTS.md`

- 014-wave1-functional-hardening: Added plan artifacts for Wave-1 functional hardening, including historical proof matrix, smoke-proof, helper-classification, fallback, missing-core, documentation, and governance planning.
- 013-wave2-visual-component-remediation: Implemented visible main components or stable visual runtime states, real `TStatusLine` feedback, `Help -> Description`, shared `examples/Shared/Wave2Runtime.cs`, stricter app-loop rendered-visibility smokes, guides, README, architecture/security evidence, statistics, and PR evidence for all eleven Wave-2 examples.
- 004-editor-file-help-streams: Added the phase-6 specification and requirements checklist for editor, file, help, stream, and resource components.
- 004-editor-file-help-streams: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/public-api.md`; synchronized shared agent guidance to the post-plan baseline.
- 004-editor-file-help-streams: Applied plan-review clarifications for safe-close vs. overwrite handling, wildcard-filtered file dialogs, explicit malformed-stream cases, and non-functional scope boundaries.
- 004-editor-file-help-streams: Added explicit coverage of insert/overwrite plus clipboard editor actions, synchronized file-information state in dialogs, shell menu/status routing, and the full Core/Controls/Serialization coverage gate.
- 004-editor-file-help-streams: Tightened the remaining integration-test expectations so event-loop dispatch, focus transitions, menu execution, and explicit dialog interaction are named directly in the feature artifacts.
- 005-driver-consolidation-m07: Added the phase-7 specification and clarification set for managed driver consolidation, `M-07` proof coverage, primary versus secondary ledger targets, and required Linux/Windows/WSL compatibility evidence.
- 005-driver-consolidation-m07: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/phase-7-proof-contract.md` to define the capability-based driver consolidation approach, the formal `.cc` ledger scope, and the review contract for `docs/porting-status.md`.
- 005-driver-consolidation-m07: Implemented Phase-7 consolidation: created `DriverCapabilityMap.cs` with 5 capability buckets, built `docs/porting-status.md` covering all 151 historical `.cc` files, added 5 new driver test files (30 tests passing), updated `docs/guides/multi-mac-workflow.md` with compatibility evidence, created `checklists/phase-8-gate-review.md`, updated `Pflichtenheft.md` marker to Phase-8 gate closure.
- 006-close-phase8-gate: Added the Phase-8 entrance-gate specification and requirements checklist for fully closing `M-07`, resolving every pending ledger row to a final proof state, and packaging the remaining build/test/coverage/format/API-doc evidence before mandatory example work may begin.
- 006-close-phase8-gate: Synchronized the hard coverage rule to require `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` each to reach at least 70 % line coverage across the shared agent guidance and gate-tracking artifacts.
- 006-close-phase8-gate: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/phase-8-gate-contract.md` to define the final `M-07` closure workflow, the 5x-70%-coverage gate, the full-suite validation package, and the dedicated closure-commit contract.
- 006-close-phase8-gate: Refined the planning baseline so the five-module coverage gate now requires assembly-specific evidence and forbids placeholder-only or no-op-only modules from satisfying Phase-8 closure.
- 006-close-phase8-gate: Analyse-Remediation nach `tasks.md`-Pruefung eingearbeitet: `gate-docs.md` im Planartefakt-Satz verankert, `tests/TuiVision.Compatibility.Tests/` als explizite Plan-Baseline nachgezogen und die Pflicht auf dokumentierte Tracking-Issues fuer Skip/Ignore-Faelle in Plan, Quickstart und Aufgaben sichtbar gemacht.
- 007-port-wave1-examples: Wave 1 portiert: `desklogo` (minimale Desktop-App), `msgcls` (Broadcast-Nachrichtenrouting), `tutorial` (16 token-basierte Schritte), `videomode` (Faehigkeitserkennung + Fallback). 41 Smoke-Tests gruen, Release-Build sauber, `dotnet format --verify-no-changes` bestanden. `Pflichtenheft.md` Welle-1-Checkliste abgehakt, `>>> NAECHSTER SCHRITT <<<` auf Welle 2 vorgeschoben.
- 008-controls-revision: Controls-Revision implementiert: `TSubMenu`, `TStatusDef`, `WindowFlags` neu hinzugefuegt; `TMenuBar`, `TStatusLine`, `TWindow`, `TDialog`, `TMenuItem`, `TView` erweitert; 338 Tests gruen, `TuiVision.Controls`-Abdeckung 84,02 %, Format-Gate bestanden; `docs/porting-status.md`, `Pflichtenheft.md` und `docs/project-statistics.md` nachgezogen; `requirements/intakes/archive/Lastenheft_ControlsRevision.008-controls-revision.md` umbenannt.
- 010-standard-dialogs-designer: Framework-Readiness fuer Standarddialoge und Dialog-Designer implementiert: explizite Datei-/Verzeichnisentscheidungen ohne Dateiinhalt-I/O, Color-/Display-/symbolische Charset-Auswahl, validierte Dialogbeschreibung, persistierter Dialogbeschreibungs-Roundtrip und malformed-input rejection; fokussierte Controls- und Serialization-Tests gruen.
- 011-port-wave2-examples: Planartefakte fuer Welle 2 erstellt: 11 neue Beispielprojekte, dedizierte Example-Smoke-Tests, DE-first/EN-second Guides, Architektur-/Security-/A11Y-Nachweise und klarer `sdlg`/`sdlg2`-Scope als historische ScrollDialog/ScrollGroup-Beispiele.
- 012-interactive-wave2-demos: Interaktive Showcase-Stufe fuer Welle 2 implementiert: alle elf Wave-2-Beispiele besitzen sichtbare normale Runtime-Pfade, app-loop-basierte Smoke-Nachweise, aktualisierte Guides, README-, Architektur-/Security-/A11Y- und PR-Evidence.

## Active Feature Context

Einstiege / Entry files: `CLAUDE.md`

### 004-editor-file-help-streams
- Current implementation baseline: execute the phase-6 increment from `specs/004-editor-file-help-streams/spec.md` and `specs/004-editor-file-help-streams/plan.md`
- Scope is limited to reusable framework components in `src/TuiVision.Controls` and `src/TuiVision.Serialization`: `TEditor`, `TMemo`, `TFileEditor`, `TEditWindow`, file/dialog/history helpers, help topics/viewers/windows, stream primitives, and named resource containers
- Out of scope for this increment: example applications such as `tvedit`, `bhelp`, and `helpdemo`; driver consolidation; calculator/macros/OS-shell integrations; and unrelated specialized widgets
- Editor flows must cover text editing, insert/overwrite behavior, clipboard-oriented actions, search/replace, modified-state handling, explicit safe-close decisions before unsaved changes are discarded, and distinct overwrite decisions when save conflicts occur
- Integration coverage for this feature must explicitly include event-loop-aware shell interaction, focus transitions, menu execution, and dialog interaction rather than relying on those behaviors only implicitly
- File flows must keep directory navigation, file lists, current file-information metadata, wildcard filtering, manual path entry, and history recall synchronized inside reusable dialogs
- Help flows must support context-based topic lookup, cross-reference navigation, and fallback content for missing contexts
- Stream/resource flows must preserve named lookup semantics and reject malformed persisted input explicitly, including truncated, trailing, unknown-type, and cyclic payload failures
- Planning decisions now fixed for this feature: dedicated runtime help files, shared-reference preservation without cyclic-graph support, exact case-sensitive resource keys, `LF` default for new files, preserved line endings for loaded files, and explicit overwrite decisions after external file changes

### 005-driver-consolidation-m07
- Current planning baseline: execute the Phase-7 increment from `specs/005-driver-consolidation-m07/spec.md` and `specs/005-driver-consolidation-m07/plan.md`
- Scope is limited to the managed driver baseline in `src/TuiVision.Drivers.Console`, the supporting validation in `tests/TuiVision.Drivers.Tests`, and the proof ledger `docs/porting-status.md`
- Out of scope for this increment: mandatory example waves, full closure of the Phase-8 entrance gate, new source modules, native bindings, and any one-to-one recreation of the historical per-OS driver split
- The proof ledger must cover every historical `.cc` implementation file in `tv203s/contrib/tvision/classes` with one mandatory primary target, optional secondary targets, status, evidence, and rationale
- Linux and Windows/WSL compatibility checks are required as reviewable evidence for this phase, but may still be manual or semi-automated rather than mandatory CI gates
- Planning decisions now fixed for this feature: `.cc` files are the formal `M-07` ledger scope, ancillary `.c`/`.h` files may appear only as rationale support, capability buckets replace per-OS lineage as the review model, and Phase 7 remains distinct from the later full Phase-8 gate closure

### 006-close-phase8-gate
- Current planning baseline: execute the Phase-8 entrance-gate closure from `specs/006-close-phase8-gate/spec.md` and `specs/006-close-phase8-gate/plan.md`
- Scope is limited to final `M-07` proof closure plus the remaining gate evidence across `docs/porting-status.md`, `Pflichtenheft.md`, the existing module test projects plus any required Compatibility-focused validation additions, coverage evidence, formatting evidence, and API-documentation validation
- Out of scope for this increment: any of the 25 mandatory example waves, substitute follow-on example scope from `TVDEMOS/` or `TVFM/`, unrelated new framework features, and Phase-7 redesign beyond finalizing still-pending proof rows
- Every historical `.cc` ledger row must end in `portiert + getestet` or `bewusst ausgelassen + Begruendung`; no provisional `portiert + Test ausstehend` state may remain after closure is claimed
- Gate closure must package explicit build, full-test, coverage, formatting, and conditional API-doc evidence and keep example waves blocked until the closure is formally recorded
- Planning decisions now fixed for this feature: `docs/porting-status.md` stays the authoritative M-07 ledger; `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` must each satisfy the explicit 70 % line-coverage gate with assembly-specific evidence; `tests/TuiVision.Compatibility.Tests/` is the planned dedicated Compatibility fallback suite when shared tests are insufficient; placeholder-only or no-op-only modules cannot satisfy that gate; gate-scope removals must update the proof surfaces in the same change; skipped or ignored gate-scoped tests require recorded tracked-issue references; unresolved local-versus-CI coverage conflicts block closure; and the closure needs a dedicated gate-marker or commit reference

### 012-interactive-wave2-demos
- Current implementation status: interactive Wave-2 demo polish is implemented on branch `012-interactive-wave2-demos`; final validation evidence is tracked in `specs/012-interactive-wave2-demos/pr-evidence.md`.
- Delivered scope is limited to making the eleven Wave-2 examples visibly operable at normal runtime: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`; matching event-loop smoke tests in `tests/TuiVision.Examples.SmokeTests/`; guides, README, `pr-evidence.md`, and proportional architecture/security/A11Y/statistics evidence.
- Before wiring or accepting each example, review the relevant historical `.c`/`.cc` source and any important matching headers under `tv203s/` as read-only reference, capture the original demo intent, and document intentional user-visible deviations in guide or PR evidence.
- Every example must show first-screen purpose text, expose primary behavior through menu, keyboard, status, or command paths, and update visible text-first feedback after each demonstrated operation.
- Primary smoke proof must run `app.Run()` or the equivalent real application loop with injected `TEvent`, command, or key events. Direct helper methods may support setup or supplemental assertions only.
- `examples/Demo` is the P1 vertical slice and must prove at least three visible behaviors before the pattern is spread across the rest of the examples.
- File/path and dialog-designer flows stay read-only toward user data: use source-controlled fixtures, fixed repository paths, or test temporary directories; do not read arbitrary user file contents or persist user history as proof.
- Wave 3 and Wave 4 examples, mandatory mouse-only operation, broad framework redesign, new runtime dependencies, databases, external services, and DocFX publishing-model changes are out of scope.
- Next open mandatory example scope after this feature is Wave 3: editor, file, help, and stream demos such as `tvedit`, `bhelp`, and `helpdemo`.

### 013-wave2-visual-component-remediation
- Current implementation status: visual component remediation is implemented on branch `013-wave2-visual-component-remediation`; final validation evidence is tracked in `specs/013-wave2-visual-component-remediation/pr-evidence.md`.
- Scope is limited to the eleven Wave-2 examples: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`.
- Shared runtime support for the remediated examples lives in `examples/Shared/Wave2Runtime.cs` and is linked into the eleven scoped example projects.
- The primary parity proof is the visible UI composition itself: controls, dialogs, windows, view groups, scroll groups, progress displays, input/list/combo composition, or another stable visible runtime state.
- Each example must use the three-layer model: visible main component, real `TStatusLine` feedback, and keyboard-reachable `Help -> Description`.
- Primary smokes must drive the real app loop and combine concrete state assertions with view-tree proof plus buffer/cell rendered visibility proof at expected positions or regions.
- Historical C/C++ sources under `tv203s/` remain read-only intent references; intentional user-visible deviations must be documented in plan, tasks, guides, feature evidence, or PR evidence.
- AI-SBOM is `N/A` for this feature while AI is only development/agent tooling; re-evaluate if runtime/product AI, models, datasets, AI infrastructure, or delivered AI components enter scope.
- Wave 3/4 functionality, broad framework redesign, mandatory mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, and new runtime dependencies are out of scope.

### 014-wave1-functional-hardening
- Current implementation status: Wave-1 functional hardening is implemented on branch `014-wave1-functional-hardening`; final validation evidence is tracked in `specs/014-wave1-functional-hardening/pr-evidence.md`.
- Scope is limited to `Desklogo`, `MsgCls`, `Tutorial` steps `tvguid01` through `tvguid16`, and `Videomode`.
- The primary proof surface is `specs/014-wave1-functional-hardening/pr-evidence.md`, which records historical source, C# behavior, proof method, helper classification, negative/fallback proof, missing-core decisions, intentional deviations, validation, and the archived Lastenheft path.
- Managed runtime behavior is proven by executable smoke tests; evidence-only proof remains allowed only for explicitly documented no-runtime-target deviations.
- Helper or headless paths may be `PrimaryProof` only when they execute real example or application logic through public commands, events, application methods, or stable public state with concrete assertions; 014 added `PrimaryProof`, `SupplementalProof`, `SetupOnly`, and `LegacyOrTemporary` helper taxonomy.
- Historical C/C++ sources under `tv203s/` remain read-only intent references. `set-logo.cc` and `tv_logo.cc` are Desklogo asset/generator boundary context only.
- Validation baseline: targeted Wave-1 smokes 38/38 passed, full example smokes 91/91 passed, full Release tests 496/496 passed, coverage gate exceeded 70% for all required assemblies, `docfx docfx.json` passed with 0 warnings/errors, and Playwright/axe DocFX smoke passed 2/2 via explicit local-server workaround when sandboxed webserver startup is blocked.
- Feature 015 completed the didactic comment pass; the next open cross-cutting step is `requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.016-secure-development-hardening.md` before Wave-1 visual remediation.
- Wave-1 visual remediation, Wave 2/3/4 behavior, broad framework redesign, mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, new runtime dependencies, and runtime/product AI are out of scope.

### 015-didactic-comment-hardening
- Current implementation status: the selective didactic inline-code-comment hardening is implemented; final evidence is in `specs/015-didactic-comment-hardening/pr-evidence.md`.
- Scope is limited to selective didactic inline, block, file, or module comments, feature evidence in `specs/015-didactic-comment-hardening/pr-evidence.md`, and affected guidance/evidence surfaces.
- Review must cover central framework flows and relevant smoke-test helpers: event/command/dispatch, focus transitions, view hierarchy, StatusLine, Help/Description, dialog state, validation/rejection, buffer/cell proof, rendering snapshots, terminal fallbacks, historical Turbo Vision deviations, and proof helpers.
- Each reviewed area must receive exactly one decision: `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening`.
- Comments remain moderate and reason-focused: explain why, trade-off, constraint, historical deviation, or proof boundary; avoid restating obvious code; use German-first/English-second CEFR-B2 text for didactic explanation blocks.
- XML comments remain the API/DocFX surface; pure `//` or `/* */` hardening does not trigger DocFX. XML/API/generated docs/navigation/guides changes trigger the normal DocFX plus A11Y path.
- No runtime behavior change, API change, new dependency, new example porting, broad framework revision, Wave-1 visual remediation, or runtime/product AI is in scope.

### 016-secure-development-hardening
- Current implementation status: the project-wide secure-development hardening is implemented; final evidence is in `specs/016-secure-development-hardening/pr-evidence.md`.
- The durable control matrix covers all 157 `CL-XX-NN` controls with `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or `FollowUp` and complete ownership, risk, evidence, and re-evaluation fields.
- Delivered remediation is bounded to explicit malformed persistence rejection, immutable workflow dependencies, supply-chain automation, root disclosure guidance, and safe Bash/PowerShell archive-script parity.
- Local acceptance is 498/498 Release tests plus line coverage above 70% for Core, Controls, Serialization, Compatibility, and Drivers.Console; DocFX/axe and remote OS/CI proof remain delivery gates.
- Human legal, provider, organization, and agent-platform decisions remain `Open`; release provenance, reproducible-build/lock maturity, and RFC 9116 remain named follow-ups rather than implicit claims.
- The next open prioritized intake is `requirements/intakes/archive/Lastenheft_03_EditorHelpAndResourcesHardening.018-editor-help-resources-hardening.md` before Wave-3 visual porting.

### 017-wave1-visual-component-remediation
- Current implementation status: Wave-1 visual component remediation is implemented; final evidence is in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- `Desklogo`, `MsgCls`, all 16 Tutorial tokens, and `Videomode` now use the three-layer model: visible main state, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, and rendered buffer/cell evidence. The acceptance matrix contains four app rows plus 16 unique Tutorial rows; direct helpers are not primary proof.
- Shared composition in `examples/Shared/Wave1Runtime.cs` uses existing framework controls. Desklogo and MsgCls are `UseExistingFramework`; Tutorial and Videomode are bounded `IntentionalDeviation` decisions.
- Historical sources under `tv203s/` remain read-only. No functional re-port, Wave-2/3/4 behavior, broad framework redesign, new dependency, persistence, external service, or runtime/product AI entered scope.
- The complete example-smoke suite passes 101/101 locally; final repository, coverage, DocFX, A11Y, and remote checks remain delivery gates until recorded in feature evidence.
- Feature 018 closes the editor/help/resources intake; the next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave3-Visual-Component-Porting.019-wave3-visual-component-porting.md`.

### 018-editor-help-resources-hardening
- Current implementation status: Wave-3 editor/help/resources hardening is implemented; final evidence is in `specs/018-editor-help-resources-hardening/pr-evidence.md`.
- Existing `TEditor`, `TFileEditor`, `TEditWindow`, `THelpViewer`, and `THelpWindow` flows are retained and proven as coherent open/edit/search/replace/save, safe-close/conflict, persisted navigation, back, and fallback paths.
- `THelpSourceCompiler` provides a bounded `.topic` and `{text[:alias]}` source contract with strict UTF-8, deterministic contexts, forward-reference resolution, stable diagnostics, and atomic no-partial-model failure.
- `TLocalizedResourceLookup` uses explicit exact-language, caller-ordered fallback, then neutral keys over case-sensitive `TResourceFile` storage without ambient locale, gettext, codepage, or new dependency scope.
- Persisted resources reject duplicate keys and negative payload lengths; persisted help rejects negative counts and unresolved or invalid reference ranges before presentation.
- Framework decisions are `UseExistingFramework` for editor/file, and bounded `SmallFrameworkFix` for help graph validation, compiler, resources, and i18n. Historical sources remain read-only.
- Wave-3 examples, mouse, terminal/charset/font work, TP7, broad redesign, services, and dependencies remain outside 018.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave3-Visual-Component-Porting.019-wave3-visual-component-porting.md` for `bhelp`, `helpdemo`, `i18n`, `tvedit`, and `tvhc`.

### 019-wave3-visual-component-porting
- Current implementation status: Wave-3 visual component porting is implemented locally; final evidence is in `specs/019-wave3-visual-component-porting/pr-evidence.md`.
- `BHelp`, `HelpDemo`, `I18n`, `TvEdit`, and `TvHc` use visible main components, a real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, rendered buffer/cell evidence, status, and description. The Wave-3 matrix passes 14/14 locally, including five constrained `48x16` layouts.
- Framework decisions are `UseExistingFramework` for TvEdit, HelpDemo, I18n, and TvHc, and bounded `IntentionalDeviation` for BHelp because the proprietary unchecked Borland `.tch` decoder is omitted.
- Embedded/source-controlled learning content and test-owned temporary paths are the only data boundaries. Historical sources remain read-only.
- Mouse interaction, terminal/charset/font work, Wave 4, broad redesign, services, new dependencies, and runtime/product AI remain outside 019.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_04_MouseSupportAndInteraction.020-mouse-support-interaction.md`.

### 020-mouse-support-interaction
- Current implementation status: bounded mouse support and interaction hardening is implemented locally; final evidence is in `specs/020-mouse-support-interaction/pr-evidence.md`.
- `ConsoleMouseIngress` accepts only complete bounded SGR 1006 left press, pressed move, and release reports and publishes zero or one existing `TEvent`; malformed syntax, range, button, capability, and phase input is rejected atomically.
- `TGroup` routes mouse down to one topmost visible target, transfers focus only to selectable targets, and preserves existing exactly-once control commands. Nested mouse coordinates traverse the full owner chain.
- The only mouse drag contract is moving a `TWindow` from its title row. Owner bounds, release, Escape, capability loss, disable, removal, shutdown, and the existing `Ctrl+F5` keyboard fallback are proven.
- Interactive macOS/Linux terminals and WSL use the SGR capability contract; native Windows Console and redirected/headless I/O remain honest `Unsupported` boundaries. Wheel, hover, touch, extra buttons, full protocol parity, and additional drag targets remain out of scope.
- Primary proof runs through `TProgram.GetEvent` and `app.Run()` and combines concrete focus/command/drag state, target identity, visible text, and rendered buffer/cell assertions. Historical sources remain read-only.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_05_TerminalCharsetAndEmulation.021-terminal-charset-hardening.md`.

### 021-terminal-charset-hardening
- Current implementation status: terminal and charset hardening is implemented locally; final evidence is in `specs/021-terminal-charset-hardening/pr-evidence.md`.
- `TerminalSession` provides a bounded in-process transcript, cursor, 16-color attributes, 4,096-cell FIFO history, deterministic resize/reset/lifecycle, and atomic C0/CSI subset recovery without a process, shell, or PTY.
- `TerminalCharsetMapper` uses Unicode plus one fixed KOI8-R table and U+FFFD replacement. `BitmapFontFixture` validates only raw 8x16/256/4,096-byte metadata; no host codec, font installation, or historical generator is used.
- `TerminalProfile` uses a closed `System.Text.Json` schema. Invalid schema is rejected as a whole; unavailable font/host capability uses an observable safe default and `Unsupported` status.
- `TTerminalView` reuses the Driver-owned session and existing Controls app loop. Primary proof combines session/profile state, concrete view identity, text status, cursor, rendered cells, controlled keyboard input, and deterministic quit.
- Historical terminal, Cyrillic, font, Eterm, and XTerm sources remain read-only intent. Full ANSI/VT/XTerm parity, visible Wave-4 examples, host mutation, new dependencies, services, persistence, and runtime/product AI remain outside 021.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave4-Visual-Component-Porting.022-wave4-visual-component-porting.md`.

### 022-wave4-visual-component-porting
- Current implementation status: Wave-4 visual component porting is implemented locally; final evidence is in `specs/022-wave4-visual-component-porting/pr-evidence.md`.
- `Terminal`, `Cyrillic`, `Fonts`, `ETerm`, and `XTerm` provide a visible main component, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, exact view identity, rendered buffer/cell evidence, controlled operation, fallback, description, and deterministic host classification. Narrow viewports preserve example identity and status meaning.
- Terminal, Cyrillic, and Fonts are `UseExistingFramework`; ETerm and XTerm are bounded `IntentionalDeviation` immutable manifests that do not parse or execute historical configuration.
- The copied raw 8x16 fixture is byte-identical to historical `font.016`; historical sources remain read-only. No process, shell, PTY, host font/codepage/locale/keyboard mutation, X resource database, terminfo, external command, or new dependency entered scope.
- Physical host observation remains separate from deterministic in-process and remote-CI evidence. Unsupported capabilities use text-first fallbacks and never claim unavailable native behavior.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_06_A11Y_Framework.023-a11y-framework.md`.

### 023-a11y-framework
- Current implementation baseline: deliver the managed accessibility layer from `specs/023-a11y-framework/` and `requirements/intakes/archive/Lastenheft_06_A11Y_Framework.023-a11y-framework.md`.
- Scope is limited to opt-in `IAccessibleWidget` text, typed `cmFocusChanged` announcements, immutable shortcut queries for `TMenuBar`/`TStatusLine`, explicit `TColorScheme.HighContrast`, keyboard coverage inventory, the `A11yFramework` reference app, and DocFX/Axe evidence.
- Focus announcements reuse one existing broadcast and propagate descendant focus to the shell; non-migrated views remain compatible without fabricated labels.
- Keyboard acceptance requires behavioral proof or a concrete `N/A` for Tab, Shift+Tab, arrows, Enter, and direct shortcuts across every inventoried selectable control family.
- Native AT-SPI, NSAccessibility, UI Automation, speech, full-control migration, terminal-wide WCAG claims, Wave 1-4 remediation, and Feature 024 are out of scope.
- Historical Turbo Vision has no direct equivalent for the modern semantic A11Y contracts; relevant focus/menu/status sources are read-only intent context only.
- The former post-023 Wave-5 statement is historical; Feature 028 completed the TV203/Free Vision closure, Feature 029 is the sole immediate intake, and the separate mandatory Feature-030 magiblot evolution audit follows before any finding-derived hardening.

### 024-tv203-freevision-conformance-audit
- Current planning baseline: the original audit and merge remain complete, but consumer-review Revision 2 in `specs/024-tv203-freevision-conformance-audit/consumer-readiness-review.md` supersedes the zero-finding forward decision.
- Borland documentation and `tv203s/` remain primary; official Free Vision commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` is external secondary evidence only. `TVDEMOS/` and `TVFM/` are read-only consumer evidence.
- The inventory remains 151 historical `.cc` rows, 119 maintained production `.cs` files, 176 exported public types, 16 domains, and 48 contracts.
- Revision-2 decisions are 7 `Aligned`, 27 `IntentionalModernization`, 1 `ConsciouslyOmitted`, 8 `BehavioralDrift`, and 5 `EvidenceGap`.
- The 13 accepted findings route exactly nine items `F001`-`F009` to `Core025` and four items `F010`-`F013` to `ComponentData026`; both are closed, while Wave 5 and Wave 6 remain blocked through Feature 028, Feature 029, Feature 030, all combined finding-derived hardening, and the new independent closure.
- Revision 2 changes audit data, validation, evidence, requirements, and ordering only; it does not implement runtime behavior or port examples.

### 027-pre-wave5-conformance-closure
- Feature 027 remains valid historical closure evidence: feature merge `35414af` and causal closeout PR #67 merge `1da2b211e84221db87ab9f959b7b40d3ae2b01f0` are complete.
- Its original 13/34/1/0/0 decision set and zero-finding Wave-5 release statement are superseded for forward planning by Feature-024 Revision 2, not rewritten retroactively.
- Final evidence is in `specs/027-pre-wave5-conformance-closure/closure-evidence.md`; all 90 historical tasks are complete.

### 025-core-runtime-conformance-hardening
- Current implementation status: all nine `Core025` findings are implemented and proven; final evidence is in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`.
- `F001`-`F009` close concrete event kinds, focus and group state, idle lifecycle, desktop stack, modal/close, shared command state, real keyboard ingress, and bounded generic drag through real-path red/green proof.
- Feature-024 resolution metadata records exactly 13 non-documentation-only closures from Features 025 and 026 while preserving the Feature-028 and Wave-5/Wave-6 gates.
- Feature 028 completed the independent closure after Feature 026; its archived intake is `requirements/intakes/archive/Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.028-pre-wave5-wave6-conformance-closure.md`.

### 026-component-data-conformance-hardening
- Current implementation status: all four `ComponentData026` findings are implemented and proven; final evidence is in `specs/026-component-data-conformance-hardening/pr-evidence.md`.
- `F010`-`F013` close dialog completion/child validation, phase-aware input validation, mode-aware typed file outcomes, and allowlisted named UI-resource composition through real-path red/green proof.
- Feature-024 metadata now contains 139 maintained source files, 211 exported public types, and exactly 13 closed finding resolutions; Wave 5 and Wave 6 remain blocked through the combined Terminal.GUI/magiblot-derived closure after Features 029 and 030.

### 028-pre-wave5-wave6-conformance-closure
- Current implementation status: the evidence-only closure independently revalidated all 13 findings, seven real-path slices, and 13 protected consumer groups without a product, API, dependency, example, or protected-source change.
- The existing TV203/Free Vision gate is `ReadyForTerminalGuiAudit`; 12 consumer groups use the existing framework and destructive Wave-6 policy remains one bounded `FollowUpHardening`.
- Both waves remain `BlockedPendingTerminalGuiAudit`; Feature 029 is the sole immediate intake and Feature 030 is the mandatory successor before hardening or closure.

### 029-tv203-freevision-terminalgui-conformance-audit
- Current implementation status: the read-only audit is complete locally; final evidence is in `specs/029-tv203-freevision-terminalgui-conformance-audit/pr-evidence.md`.
- Turbo Vision 2.0.3 remains authoritative; pinned Free Vision remains secondary evidence, and Terminal.GUI v1.9.0 at commit `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3` is an additional modern C# implementation opinion.
- All `C001`-`C048`, 16 domains, 25 pinned sources, and 13 Wave consumer groups are covered. No `C049+`, `CandidateFinding`, or `ProductDecision` was admitted.
- The audit changes no runtime, API, dependency, example, historical, consumer, Free Vision, or Terminal.GUI source. It hands 48 `TGO###` observations to Feature 030 and creates no hardening or closure Lastenheft itself.
- Feature 030 completed the combined TG/MB audit locally; Feature 031 is the sole next intake after merge, and Wave 5 plus Wave 6 remain blocked through its independent closure.
- After every autonomous run, promote only reproducible provider-neutral preset learning through a Home-Baseline patch release and exact tag-ZIP adoption before the next run; record `NoPromotion` without an empty branch or PR. The consolidated pre-Wave-5 catalog update is submitted as `github/spec-kit#3569`; its external processing does not grant authority and does not indefinitely block the already installed v0.2.2 field run.

### 030-tv203-magiblot-evolution-audit
- Current implementation status: the read-only audit data and targeted validator are locally complete; final repository, documentation, coverage, remote review, and delivery gates remain.
- The exact magiblot pin is commit `57b6f56b38e0ee75240a80a10ee0e11470c24693`, tree `96dd03873955689ff0a79f6c8107a8148fe1ebd6`, with the multipart COPYRIGHT hash `66220bae...548`; its direct C++ lineage remains a modernization witness, never a normative source.
- Fifty selected source records support 48 relations: 27 `CorroboratesOriginal`, 12 `CorroboratesModernization`, 6 `AlternativeModernization`, and 3 `NotApplicable`. The 48 `MB*` decisions are 39 `AlreadySatisfiedWithNewEvidence`, 6 `IntentionalDeviation`, and 3 `RejectedComparison`.
- All 48 `TGO*` and 48 `MB*` observations have one combined disposition. No reproducible `CF*` finding or `ProductDecision` exists, so no hardening intake is generated; `requirements/intakes/archive/Lastenheft_16_Pre-Wave5-Wave6-Combined-Conformance-Closure.031-combined-conformance-closure.md` defines Feature 031 as the active closure intake.
- Runtime, API, dependency, example, consumer, historical, and external sources remain unchanged. Wave 5 and Wave 6 stay `BlockedPendingCombinedConformanceClosure`.
- `requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md` remains a binding deferred intake only after the complete Wave-6 closeout and receives its feature number then.

### 031-combined-conformance-closure
- Current implementation status: the evidence-only independent closure is complete; final evidence is in `specs/031-combined-conformance-closure/delivery-closeout.md`.
- Exactly 48 contracts, 13 consumers, 48 TGO and 48 MB observations, 96 dispositions, all accepted source pins and hashes, and the closed `F001`-`F013` set are revalidated.
- Product runtime, public APIs, dependencies, packages, projects, examples, consumers, historical sources, and external sources remain unchanged.
- Feature 031 made Wave 5 `Eligible`; Features 032 and 033 have since delivered both stages. Wave 6 remains only `ConditionallyReady` and still requires review of the actual combined Wave-5 delta.

### 032-wave5-tp7-functional-porting
- Current implementation status: the functional Wave-5 stage is delivered; all 15 read-only `TVDEMOS/*.PAS` sources have one role, all six consumers use the existing framework, and ten `Tp7*` projects provide normal plus controlled-smoke entry points.
- Primary proof covers calculator, central apps, resources, deterministic domain state, mouse capability/fallback, and exact 15/6/10/10 traceability through real app loops, state, view, and rendered cells.
- File, resource, help, generator, and mouse boundaries remain controlled; no host state, arbitrary user data, dependency, historical source, or broad framework behavior is changed.
- `requirements/intakes/archive/Lastenheft_18_Wave5-TP7-Showcase-Remediation.033-wave5-tp7-showcase-remediation.md` is the archived intake derived from the ten concrete Stage-2 deltas. Feature 033 is fully delivered through PR #96 and merge `d476e63`; Wave 6 remains blocked pending the actual combined Wave-5 delta review.

### 034-wave5-combined-delta-closure
- Current implementation status: Feature 034 is fully delivered through PR #99, final head `016692d`, merge `7fb52e2`, and its causal closeout.
- The read-only audit proves exact 15 source, 6 consumer, 10 example, 10 functional-proof, 10 showcase-closure, and 10 guide/launch cardinalities. All ten decisions are `AcceptedIntentionalDeviation`; there are zero open `Gap` dimensions, Candidate Findings, Product Decisions, owner groups, or hardening intakes.
- Product runtime, APIs, dependencies, projects, examples, framework code, `TVDEMOS/`, `TVFM/`, `tv203s/`, and external comparison sources remain unchanged. The final 22-check matrix and 13 exact-head gates passed; checkout-neutral SHA-256 and Git-blob proof passed on Ubuntu, macOS, and Windows.
- Wave 5 is `Closed`. Feature 035 is delivered through PR #101, final head `207e807`, and merge `52f77fa`; it provides Wave-6 Stage 1.

### 036-wave6-tvfm-showcase-remediation
- Feature 036 is fully delivered through PR #104, final head `a0d5062`, and merge `559bffb`.
- The exact closure contains ten `W6S` showcase decisions, one `ShowcaseComplete` entry-point decision, and 24 validated read-only `TVFM/` source hashes. The Windows provider correction makes historical `.PAS`/`.BAT` text hashes checkout-neutral while preserving byte-exact resource hashes.
- Feature-035 controlled-root, path, search, preview, viewer, one-shot intent, mutation, dependency, process, shell, host, and arbitrary-user-data boundaries remain unchanged. `TVFM/`, `TVDEMOS/`, and `tv203s/` stay read-only.
- The independent Wave-6 closure is now the preferred active intake at `requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md`; it reserves Feature 037 without starting it. The post-Wave-6 portfolio audit remains blocked by that closure.

### Autonomous Red-Proof Completeness
- Before the first red test batch, review imports, public XML docs, harness helpers, focus/ownership assertions, and linked-source assembly identity.
- Group independent negative cases only as a bounded project-local red matrix with explicit failure boundaries and shared ownership.
- When source is linked into multiple assemblies, cross-project proof uses public contracts or state delegates and does not assume one CLR type identity.
- Keep a causal closeout evidence-only and single-commit-capable: do not require its own PR URL, reviewed-head result, or merge commit inside that same repository file; verify terminal facts externally without recursive closeout.
- When push and pull-request events create equivalent workflow sets, use pull-request-context checks as the gate and record duplicate runs as noise; cancel them only under an explicit safe workflow/concurrency contract.

## Active Technologies

Einstiege / Entry files: `CLAUDE.md`

- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, docfx (004-editor-file-help-streams)
- Real local file-system interaction plus persisted binary help/resource files; no database layer in this increment (004-editor-file-help-streams)
- C# `latest` on .NET 10 (`net10.0`) + Existing modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; docfx for API documentation validation; GitHub Actions for existing CI (005-driver-consolidation-m07)
- Source-controlled Markdown evidence in `docs/porting-status.md`; no database storage; compatibility evidence may include repository notes and command output references (005-driver-consolidation-m07)
- C# `latest` on .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, existing MSTest suites plus any required Compatibility-focused validation additions, Coverlet coverage evidence, `dotnet format`, docfx, `Pflichtenheft.md`, and `docs/porting-status.md` for the formal Phase-8 entrance-gate proof (006-close-phase8-gate)
- Repository-visible proof artifacts only; no database layer and no example-application delivery in this increment (006-close-phase8-gate)
- Source-controlled example projects under `examples/`; wave-1 examples (`desklogo`, `msgcls`, `tutorial`, `videomode`) delivered; 41 smoke tests green; next: Wave 2 Controls and Dialogs (007-port-wave1-examples)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation (008-controls-revision)
- In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage (008-controls-revision)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell and widget foundation from `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`, `TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions plus existing example-smoke infrastructure for downstream wave-2 readiness (009-controls-widgets-and-collections)
- In-memory UI state only in production; source-controlled planning, tests, proof artifacts, and already delivered downstream examples in `specs/`, `tests/`, `docs/`, and `examples/`; no database or external service storage (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream contex (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell, dialog, file, color, history, and widget types (`TDialog`, `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, `THistory`, `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, `TColorDisplay`, `TComboBox`, `TProgressBar`, `TParamText`); existing `TuiVision.Serialization` archive/resource foundation (`TRecordRegistry`, `TRecordSerializer`, `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TResourceFile`, `TResourceCollection`, `pstream` family); MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (010-standard-dialogs-designer)
- In-memory dialog state and session-only history; real local file-system metadata for file-listing/validation only; source-controlled tests/proof artifacts; minimal persisted dialog-description fixture through existing serialization/resource primitives; no database or external service storage (010-standard-dialogs-designer)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing framework modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; new wave-2 example projects under `examples/`; existing `tests/TuiVision.Examples.SmokeTests/`; MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (011-port-wave2-examples)
- Runtime example state is in memory; standard-dialog file flows use real local file-system metadata only; `dlgdsn` may use source-controlled dialog-description fixtures through existing Serialization/resource primitives; no database, external service, persisted user history, or new dependency planned (011-port-wave2-examples)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet test stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (012-interactive-wave2-demos)
- Runtime example state is in memory. Dialog-designer and file/path demonstrations use source-controlled fixtures, fixed repository paths, or test temporary directories. The examples must not persist user history, write user data as part of normal demonstration, read arbitrary user file contents as proof, or add a database/external service. (012-interactive-wave2-demos)
- Runtime example state remains in memory. Controlled examples may use source-controlled fixtures, fixed repository paths, or test temporary directories for metadata, rendering, validation, or rejection proof. The feature must not add a database, external service, network dependency, persistent user history, or arbitrary user-file content reads. (013-wave2-visual-component-remediation)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (014-wave1-functional-hardening)
- Runtime example state remains in memory. Proof data is limited to existing source-controlled files, controlled example fixtures if needed, or test temporary directories. No database, external service, network dependency, persistent user history, arbitrary user-file content reads, or runtime/product AI storage is planned. (014-wave1-functional-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest/Coverlet validation; existing DocFX plus Playwright/axe web A11Y tooling when documentation triggers apply. No new runtime NuGet dependency is planned. (015-didactic-comment-hardening)
- Source-controlled Markdown evidence and guidance files only. Production code state and tests keep their current storage model. No database, external service, network dependency, persistent user history, runtime/product AI storage, or arbitrary user-file proof path is planned. (015-didactic-comment-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`); Bash and PowerShell 7 for repository tooling + Existing TuiVision projects, MSTest, Coverlet, DocFX, Playwright/axe, GitHub Actions, Gitleaks, and CycloneDX for .NET 6.2.0 as a repository-local tool. No new runtime package is planned. (016-secure-development-hardening)
- Source-controlled Markdown, YAML, shell/PowerShell scripts, a local .NET tool manifest, and test fixtures. Generated evidence is written to temporary or ignored directories. No database, service, credential, runtime AI, or user-data store is introduced. (016-secure-development-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing TuiVision modules, shared Wave-1 example composition, MSTest/Coverlet, DocFX, and Playwright/axe; no new package (017-wave1-visual-component-remediation)
- Runtime example state remains in-process and session-only; proof and governance use source-controlled Markdown, with no database, external service, persistent user history, arbitrary user-file proof, or runtime/product AI (017-wave1-visual-component-remediation)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, and `TuiVision.Drivers.Console`; no new packages (019-wave3-visual-component-porting)
- Embedded/source-controlled learning content and test-owned temporary files only (019-wave3-visual-component-porting)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Drivers.Console`, `TuiVision.Controls`, and reviewed `TuiVision.Compatibility` key translation; no new packages (021-terminal-charset-hardening)
- In-memory session/history state plus source-controlled JSON and raw 8x16 fixtures (021-terminal-charset-hardening)
- C# / .NET 10 for durable test-only validation; Markdown and JSON for evidence + existing framework assemblies, MSTest, `System.Text.Json`, Git, official pinned FPC source checkout (024-tv203-freevision-conformance-audit)
- repository-owned JSON and Markdown evidence; external Free Vision worktree under `/tmp`, never tracked (024-tv203-freevision-conformance-audit)
- C# / .NET 10 + existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, MSTest; no new package (025-core-runtime-conformance-hardening)
- in-process state only; audit JSON and Markdown evidence are repository-owned documentation (025-core-runtime-conformance-hardening)
- C# with latest language version on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Serialization`; no new package or runtime dependency (026-component-data-conformance-hardening)
- Existing bounded binary archive and `TResourceFile`; controlled temporary filesystem metadata for file-dialog proofs (026-component-data-conformance-hardening)
- C# / .NET 10 for test-only validation; JSON, Markdown, YAML, Bash and PowerShell evidence tooling + existing MSTest 4.0.1, Coverlet 6.0.4, DocFX, Playwright/Axe, Lynx, jq, xmllint, Git and GitHub Actions (028-pre-wave5-wave6-conformance-closure)
- repository-owned JSON and Markdown only; historical, consumer, and Free Vision sources remain read-only (028-pre-wave5-wave6-conformance-closure)
- C# / .NET 10 for test-only validation; JSON and Markdown for audit evidence + existing MSTest 4.0.1, System.Text.Json, Coverlet 6.0.4, DocFX, Playwright/Axe, Lynx, Git and GitHub Actions (029-tv203-freevision-terminalgui-conformance-audit)
- repository-owned Feature-029 JSON and Markdown; external Terminal.GUI checkout remains untracked under a temporary directory (029-tv203-freevision-terminalgui-conformance-audit)
- C# 14 / .NET 10 for test-only validation; JSON and Markdown for evidence + Existing BCL `System.Text.Json`, MSTest 4.0.1, existing repository scripts and workflows; no new package (031-combined-conformance-closure)
- Source-controlled closed JSON and Markdown evidence; temporary external checkouts under `/tmp` only (031-combined-conformance-closure)
- C# 14 / .NET 10 + Existing TuiVision.Core, TuiVision.Controls, TuiVision.Serialization, TuiVision.Compatibility, TuiVision.Drivers.Console, MSTest 4.0.1 and BCL System.Text.Json; no new package (032-wave5-tp7-functional-porting)
- Embedded UTF-8 fixtures, controlled test-temporary files, existing THelpFile and TResourceFile; no database or service (032-wave5-tp7-functional-porting)
- C# 14 / .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, DocFX and Playwright/Axe; no new package (035-wave6-tvfm-functional-porting)
- Source-controlled UTF-8/binary fixtures copied into test-owned temporary roots; controlled local filesystem only; no arbitrary user data, shell/external viewer, database or service (035-wave6-tvfm-functional-porting)
- C# 14 / .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest 4.0.1 and .NET BCL; no new package (036-wave6-tvfm-showcase-remediation)
- Existing source-controlled fixtures copied into process- or test-owned temporary controlled roots; no database, service, host profile, shell, process, PTY, network, or arbitrary-user storage (036-wave6-tvfm-showcase-remediation)

### 007-port-wave1-examples
- Current status: Wave 1 delivered (2026-03-28). `desklogo`, `msgcls`, `tutorial` (16 steps), `videomode` are ported, smoke-tested, and guide-documented.
- Wave 1 scope: `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`; shared smoke-test infrastructure in `tests/TuiVision.Examples.SmokeTests/`; guides in `docs/guides/examples/`.
- Next open scope: Wave 2 – Controls and Dialogs (requires Controls/Dialog layer as prerequisite before planning starts).
- Planning decisions now fixed: headless smoke seam via `bool headless` constructor parameter + `GetEvent()` override; in-process MSTest execution without external process spawning; bilingual German-first/English-second XML docs and comments at CEFR-B2; `DisplayModeCoordinator.ProbeResizeSupport()` cross-platform probe with CA1416 suppressed.

## Recent Changes

Einstiege / Entry files: `CLAUDE.md`

- 014-wave1-functional-hardening: Added plan artifacts for Wave-1 functional hardening, including historical proof matrix, smoke-proof, helper-classification, fallback, missing-core, documentation, and governance planning.
- 013-wave2-visual-component-remediation: Implemented visible main components or stable visual runtime states, real `TStatusLine` feedback, `Help -> Description`, shared `examples/Shared/Wave2Runtime.cs`, stricter app-loop rendered-visibility smokes, guides, README, architecture/security evidence, statistics, and PR evidence for all eleven Wave-2 examples.
- 004-editor-file-help-streams: Added the phase-6 specification and requirements checklist for editor, file, help, stream, and resource components.
- 004-editor-file-help-streams: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/public-api.md`; synchronized shared agent guidance to the post-plan baseline.
- 004-editor-file-help-streams: Applied plan-review clarifications for safe-close vs. overwrite handling, wildcard-filtered file dialogs, explicit malformed-stream cases, and non-functional scope boundaries.
- 004-editor-file-help-streams: Added explicit coverage of insert/overwrite plus clipboard editor actions, synchronized file-information state in dialogs, shell menu/status routing, and the full Core/Controls/Serialization coverage gate.
- 004-editor-file-help-streams: Tightened the remaining integration-test expectations so event-loop dispatch, focus transitions, menu execution, and explicit dialog interaction are named directly in the feature artifacts.
- 005-driver-consolidation-m07: Added the phase-7 specification and clarification set for managed driver consolidation, `M-07` proof coverage, primary versus secondary ledger targets, and required Linux/Windows/WSL compatibility evidence.
- 005-driver-consolidation-m07: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/phase-7-proof-contract.md` to define the capability-based driver consolidation approach, the formal `.cc` ledger scope, and the review contract for `docs/porting-status.md`.
- 005-driver-consolidation-m07: Implemented Phase-7 consolidation: created `DriverCapabilityMap.cs` with 5 capability buckets, built `docs/porting-status.md` covering all 151 historical `.cc` files, added 5 new driver test files (30 tests passing), updated `docs/guides/multi-mac-workflow.md` with compatibility evidence, created `checklists/phase-8-gate-review.md`, updated `Pflichtenheft.md` marker to Phase-8 gate closure.
- 006-close-phase8-gate: Added the Phase-8 entrance-gate specification and requirements checklist for final `M-07` closure, removal of provisional ledger states, and explicit build/test/coverage/format/API-doc proof before example waves may start.
- 006-close-phase8-gate: Synchronized the hard coverage rule to require `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` each to reach at least 70 % line coverage across the shared agent guidance and gate-tracking artifacts.
- 006-close-phase8-gate: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/phase-8-gate-contract.md` to define the final `M-07` closure workflow, the 5x-70%-coverage gate, the full-suite validation package, and the dedicated closure-commit contract.
- 006-close-phase8-gate: Refined the plan after clarification so the five-module coverage gate now requires assembly-specific reporting and rejects placeholder-only or no-op-only closure modules.
- 006-close-phase8-gate: Analyse-Remediation nach der Task-Pruefung nachgezogen: `gate-docs.md` als Planartefakt explizit benannt, `tests/TuiVision.Compatibility.Tests/` als feste Plan-Baseline aufgenommen und Skip/Ignore-Faelle nur noch mit dokumentierter Tracking-Issue-Referenz als gate-konform zugelassen.
- 007-port-wave1-examples: Wave 1 portiert: `desklogo` (minimale Desktop-App), `msgcls` (Broadcast-Nachrichtenrouting), `tutorial` (16 token-basierte Schritte), `videomode` (Faehigkeitserkennung + Fallback). 41 Smoke-Tests gruen, Release-Build sauber, `dotnet format --verify-no-changes` bestanden. `Pflichtenheft.md` Welle-1-Checkliste abgehakt, `>>> NAECHSTER SCHRITT <<<` auf Welle 2 vorgeschoben.
- 008-controls-revision: Controls-Revision implementiert: `TSubMenu`, `TStatusDef`, `WindowFlags` neu hinzugefuegt; `TMenuBar`, `TStatusLine`, `TWindow`, `TDialog`, `TMenuItem`, `TView` erweitert; 338 Tests gruen, `TuiVision.Controls`-Abdeckung 84,02 %, Format-Gate bestanden; `docs/porting-status.md`, `Pflichtenheft.md` und `docs/project-statistics.md` nachgezogen; `requirements/intakes/archive/Lastenheft_ControlsRevision.008-controls-revision.md` umbenannt.
- 010-standard-dialogs-designer: Framework-Readiness fuer Standarddialoge und Dialog-Designer implementiert: explizite Datei-/Verzeichnisentscheidungen ohne Dateiinhalt-I/O, Color-/Display-/symbolische Charset-Auswahl, validierte Dialogbeschreibung, persistierter Dialogbeschreibungs-Roundtrip und malformed-input rejection; fokussierte Controls- und Serialization-Tests gruen.
- 011-port-wave2-examples: Planartefakte fuer Welle 2 erstellt: 11 neue Beispielprojekte, dedizierte Example-Smoke-Tests, DE-first/EN-second Guides, Architektur-/Security-/A11Y-Nachweise und klarer `sdlg`/`sdlg2`-Scope als historische ScrollDialog/ScrollGroup-Beispiele.
- 012-interactive-wave2-demos: Interaktive Showcase-Stufe fuer Welle 2 implementiert: alle elf Wave-2-Beispiele besitzen sichtbare normale Runtime-Pfade, app-loop-basierte Smoke-Nachweise, aktualisierte Guides, README-, Architektur-/Security-/A11Y- und PR-Evidence.

## 🎯 Aktueller Feature-Fokus

Einstiege / Entry files: `GEMINI.md`

### `004-editor-file-help-streams`
*   Spezifikationsquelle: `specs/004-editor-file-help-streams/spec.md`
*   Umsetzungsgrundlage dieses Inkrements: `specs/004-editor-file-help-streams/spec.md` und `specs/004-editor-file-help-streams/plan.md`
*   Geplanter Umfang in `src/TuiVision.Controls` und `src/TuiVision.Serialization`:
    *   `TEditor`, `TMemo`, `TFileEditor`, `TEditWindow`
    *   Datei-/Verzeichnisdialoge, Pfad-History und zugehörige Hilfskomponenten
    *   `THelpTopic`, `THelpFile`, `THelpViewer`, `THelpWindow`
    *   Stream-Primitiven und benannte Ressourcencontainer
*   Verhalten:
    *   Editor-Flows müssen Bearbeitung, Insert/Overwrite-Modus, Clipboard-Aktionen, Suche/Ersetzen, Modified-State, explizite Safe-Close-Entscheidungen und getrennte Overwrite-Entscheidungen bei Save-Konflikten abdecken
    *   Datei-Flows müssen Dateiliste, Verzeichnisnavigation, aktuelle Datei-Metadaten, Wildcard-Filter, manuelle Pfadeingabe und History-Rückruf synchron halten
    *   Hilfe-Flows müssen kontextbezogene Topics, Querverweise und Fallback-Inhalte für fehlende Kontexte unterstützen
    *   Stream-/Ressourcen-Flows müssen benannte Ablage und explizite Fehlerbehandlung bei gekuerzten, ueberhaengenden, unbekannten oder zyklischen Persistenzdaten abdecken
    *   Integrationsabdeckung muss Event-Loop-Verhalten, Fokuswechsel, Menueausfuehrung und explizite Dialoginteraktion fuer dieses Feature direkt benennen
*   Festgezogene Planungsentscheidungen:
    *   Runtime-Hilfe kommt aus dedizierten Help-Dateien
    *   Shared References bleiben erhalten, zyklische Objektgraphen sind nicht Teil der Abnahme
    *   Resource-Keys sind exakt case-sensitive
    *   Neue Dateien verwenden `LF`; geladene Dateien behalten ihr Zeilenendformat
    *   Externe Dateiaenderungen erfordern vor dem Ueberschreiben eine explizite Entscheidung
*   Explizit nicht Teil dieses Schritts:
    *   Beispielprogramme wie `tvedit`, `bhelp` und `helpdemo`
    *   Treiberkonsolidierung
    *   Rechner-/Makro-/OS-Shell-Integrationen
    *   sonstige fachfremde Spezial-Widgets

### `005-driver-consolidation-m07`
*   Spezifikationsquelle: `specs/005-driver-consolidation-m07/spec.md`
*   Planungsgrundlage dieses Inkrements: `specs/005-driver-consolidation-m07/spec.md` und `specs/005-driver-consolidation-m07/plan.md`
*   Geplanter Umfang:
    *   der verwaltete Konsolentreiber in `src/TuiVision.Drivers.Console`
    *   begleitende Validierung in `tests/TuiVision.Drivers.Tests`
    *   das M-07-Beweis-Ledger `docs/porting-status.md`
*   Festgezogene Planungsentscheidungen:
    *   `.cc`-Dateien sind der formale `M-07`-Ledger-Scope
    *   zugehoerige `.c`-/`.h`-Dateien duerfen nur als Begruendungs- oder Referenzmaterial auftreten
    *   Capability-Buckets ersetzen eine Eins-zu-eins-Abbildung pro historischem OS-Treiber
    *   Phase 7 bleibt bewusst von der spaeteren vollstaendigen Phase-8-Gateschliessung getrennt
*   Explizit nicht Teil dieses Schritts:
    *   die 25 verpflichtenden Beispielwellen
    *   eine vollstaendige Phase-8-Gateschliessung
    *   neue Quellmodule oder native Bindings

### `006-close-phase8-gate`
*   Spezifikationsquelle: `specs/006-close-phase8-gate/spec.md`
*   Planungsgrundlage dieses Inkrements: `specs/006-close-phase8-gate/spec.md` und `specs/006-close-phase8-gate/plan.md`
*   Geplanter Umfang:
    *   `docs/porting-status.md` als autoritatives M-07-Ledger
    *   Gate-Nachweise fuer Build, Volltests, Coverage, Formatierung und API-Dokumentation
    *   Fortschreibung der Pflichtenheft- und Review-Artefakte fuer die Phase-8-Entscheidung
*   Festgezogene Planungsentscheidungen:
    *   keine `portiert + Test ausstehend`-Zeilen mehr nach behauptetem Gate-Abschluss
    *   `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` muessen jeweils die explizite 70-%-Line-Coverage-Huerde mit getrenntem Assembly-Nachweis erreichen
    *   `tests/TuiVision.Compatibility.Tests/` ist die geplante dedizierte Compatibility-Fallback-Suite, falls geteilte Testprojekte fuer einen ehrlichen Nachweis nicht ausreichen
    *   Platzhalter- oder No-op-Module duerfen das Eingangstor nicht nur formal ueber triviale Tests bestehen
    *   Gate-Scope-Entfernungen muessen die Nachweisartefakte im selben Aenderungspaket nachziehen; Skip-/Ignore-Faelle brauchen dokumentierte Tracking-Issue-Referenzen; offene lokale-vs.-CI-Coverage-Konflikte blockieren den Abschluss
    *   die 25 MUSS-Beispiele bleiben bis zum formalen Gate-Abschluss blockiert
*   Explizit nicht Teil dieses Schritts:
    *   Beginn der Beispielportierungen
    *   Ersatzumfang aus `TVDEMOS/` oder `TVFM/`
    *   fachfremde neue Framework-Features

## Active Feature Context

Einstiege / Entry files: `GEMINI.md`

### 012-interactive-wave2-demos
*   Current implementation status: interactive Wave-2 demo polish is implemented on branch `012-interactive-wave2-demos`; final validation evidence is tracked in `specs/012-interactive-wave2-demos/pr-evidence.md`.
*   Delivered scope is limited to making the eleven Wave-2 examples visibly operable at normal runtime: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`; matching event-loop smoke tests in `tests/TuiVision.Examples.SmokeTests/`; guides, README, `pr-evidence.md`, and proportional architecture/security/A11Y/statistics evidence.
*   Before wiring or accepting each example, review the relevant historical `.c`/`.cc` source and any important matching headers under `tv203s/` as read-only reference, capture the original demo intent, and document intentional user-visible deviations in guide or PR evidence.
*   Every example must show first-screen purpose text, expose primary behavior through menu, keyboard, status, or command paths, and update visible text-first feedback after each demonstrated operation.
*   Primary smoke proof must run `app.Run()` or the equivalent real application loop with injected `TEvent`, command, or key events. Direct helper methods may support setup or supplemental assertions only.
*   `examples/Demo` is the P1 vertical slice and must prove at least three visible behaviors before the pattern is spread across the rest of the examples.
*   File/path and dialog-designer flows stay read-only toward user data: use source-controlled fixtures, fixed repository paths, or test temporary directories; do not read arbitrary user file contents or persist user history as proof.
*   Wave 3 and Wave 4 examples, mandatory mouse-only operation, broad framework redesign, new runtime dependencies, databases, external services, and DocFX publishing-model changes are out of scope.
*   Next open mandatory example scope after this feature is Wave 3: editor, file, help, and stream demos such as `tvedit`, `bhelp`, and `helpdemo`.

### 013-wave2-visual-component-remediation
*   Current implementation status: visual component remediation is implemented on branch `013-wave2-visual-component-remediation`; final validation evidence is tracked in `specs/013-wave2-visual-component-remediation/pr-evidence.md`.
*   Scope is limited to the eleven Wave-2 examples: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`.
*   Shared runtime support for the remediated examples lives in `examples/Shared/Wave2Runtime.cs` and is linked into the eleven scoped example projects.
*   The primary parity proof is the visible UI composition itself: controls, dialogs, windows, view groups, scroll groups, progress displays, input/list/combo composition, or another stable visible runtime state.
*   Each example must use the three-layer model: visible main component, real `TStatusLine` feedback, and keyboard-reachable `Help -> Description`.
*   Primary smokes must drive the real app loop and combine concrete state assertions with view-tree proof plus buffer/cell rendered visibility proof at expected positions or regions.
*   Historical C/C++ sources under `tv203s/` remain read-only intent references; intentional user-visible deviations must be documented in plan, tasks, guides, feature evidence, or PR evidence.
*   AI-SBOM is `N/A` for this feature while AI is only development/agent tooling; re-evaluate if runtime/product AI, models, datasets, AI infrastructure, or delivered AI components enter scope.
*   Wave 3/4 functionality, broad framework redesign, mandatory mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, and new runtime dependencies are out of scope.

### 014-wave1-functional-hardening
*   Current implementation status: Wave-1 functional hardening is implemented on branch `014-wave1-functional-hardening`; final validation evidence is tracked in `specs/014-wave1-functional-hardening/pr-evidence.md`.
*   Scope is limited to `Desklogo`, `MsgCls`, `Tutorial` steps `tvguid01` through `tvguid16`, and `Videomode`.
*   The primary proof surface is `specs/014-wave1-functional-hardening/pr-evidence.md`, which records historical source, C# behavior, proof method, helper classification, negative/fallback proof, missing-core decisions, intentional deviations, validation, and the archived Lastenheft path.
*   Managed runtime behavior is proven by executable smoke tests; evidence-only proof remains allowed only for explicitly documented no-runtime-target deviations.
*   Helper or headless paths may be `PrimaryProof` only when they execute real example or application logic through public commands, events, application methods, or stable public state with concrete assertions; 014 added `PrimaryProof`, `SupplementalProof`, `SetupOnly`, and `LegacyOrTemporary` helper taxonomy.
*   Historical C/C++ sources under `tv203s/` remain read-only intent references. `set-logo.cc` and `tv_logo.cc` are Desklogo asset/generator boundary context only.
*   Validation baseline: targeted Wave-1 smokes 38/38 passed, full example smokes 91/91 passed, full Release tests 496/496 passed, coverage gate exceeded 70% for all required assemblies, `docfx docfx.json` passed with 0 warnings/errors, and Playwright/axe DocFX smoke passed 2/2 via explicit local-server workaround when sandboxed webserver startup is blocked.
*   Feature 015 completed the didactic comment pass; the next open cross-cutting step is `requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.016-secure-development-hardening.md` before Wave-1 visual remediation.
*   Wave-1 visual remediation, Wave 2/3/4 behavior, broad framework redesign, mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, new runtime dependencies, and runtime/product AI are out of scope.

### 015-didactic-comment-hardening
*   Current implementation status: the selective didactic inline-code-comment hardening is implemented; final evidence is in `specs/015-didactic-comment-hardening/pr-evidence.md`.
*   Scope is limited to selective didactic inline, block, file, or module comments, feature evidence in `specs/015-didactic-comment-hardening/pr-evidence.md`, and affected guidance/evidence surfaces.
*   Review must cover central framework flows and relevant smoke-test helpers: event/command/dispatch, focus transitions, view hierarchy, StatusLine, Help/Description, dialog state, validation/rejection, buffer/cell proof, rendering snapshots, terminal fallbacks, historical Turbo Vision deviations, and proof helpers.
*   Each reviewed area must receive exactly one decision: `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening`.
*   Comments remain moderate and reason-focused: explain why, trade-off, constraint, historical deviation, or proof boundary; avoid restating obvious code; use German-first/English-second CEFR-B2 text for didactic explanation blocks.
*   XML comments remain the API/DocFX surface; pure `//` or `/* */` hardening does not trigger DocFX. XML/API/generated docs/navigation/guides changes trigger the normal DocFX plus A11Y path.
*   No runtime behavior change, API change, new dependency, new example porting, broad framework revision, Wave-1 visual remediation, or runtime/product AI is in scope.

### 016-secure-development-hardening
*   Current implementation status: the project-wide secure-development hardening is implemented; final evidence is in `specs/016-secure-development-hardening/pr-evidence.md`.
*   The durable control matrix covers all 157 `CL-XX-NN` controls with `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or `FollowUp` and complete ownership, risk, evidence, and re-evaluation fields.
*   Delivered remediation is bounded to explicit malformed persistence rejection, immutable workflow dependencies, supply-chain automation, root disclosure guidance, and safe Bash/PowerShell archive-script parity.
*   Local acceptance is 498/498 Release tests plus line coverage above 70% for Core, Controls, Serialization, Compatibility, and Drivers.Console; DocFX/axe and remote OS/CI proof remain delivery gates.
*   Human legal, provider, organization, and agent-platform decisions remain `Open`; release provenance, reproducible-build/lock maturity, and RFC 9116 remain named follow-ups rather than implicit claims.
*   The next open prioritized intake is `requirements/intakes/archive/Lastenheft_03_EditorHelpAndResourcesHardening.018-editor-help-resources-hardening.md` before Wave-3 visual porting.

### 017-wave1-visual-component-remediation
*   Current implementation status: Wave-1 visual component remediation is implemented; final evidence is in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
*   `Desklogo`, `MsgCls`, all 16 Tutorial tokens, and `Videomode` now use the three-layer model: visible main state, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
*   Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, and rendered buffer/cell evidence. The acceptance matrix contains four app rows plus 16 unique Tutorial rows; direct helpers are not primary proof.
*   Shared composition in `examples/Shared/Wave1Runtime.cs` uses existing framework controls. Desklogo and MsgCls are `UseExistingFramework`; Tutorial and Videomode are bounded `IntentionalDeviation` decisions.
*   Historical sources under `tv203s/` remain read-only. No functional re-port, Wave-2/3/4 behavior, broad framework redesign, new dependency, persistence, external service, or runtime/product AI entered scope.
*   The complete example-smoke suite passes 101/101 locally; final repository, coverage, DocFX, A11Y, and remote checks remain delivery gates until recorded in feature evidence.
*   Feature 018 closes the editor/help/resources intake; the next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave3-Visual-Component-Porting.019-wave3-visual-component-porting.md`.

### 018-editor-help-resources-hardening
*   Current implementation status: Wave-3 editor/help/resources hardening is implemented; final evidence is in `specs/018-editor-help-resources-hardening/pr-evidence.md`.
*   Existing `TEditor`, `TFileEditor`, `TEditWindow`, `THelpViewer`, and `THelpWindow` flows are retained and proven as coherent open/edit/search/replace/save, safe-close/conflict, persisted navigation, back, and fallback paths.
*   `THelpSourceCompiler` provides a bounded `.topic` and `{text[:alias]}` source contract with strict UTF-8, deterministic contexts, forward-reference resolution, stable diagnostics, and atomic no-partial-model failure.
*   `TLocalizedResourceLookup` uses explicit exact-language, caller-ordered fallback, then neutral keys over case-sensitive `TResourceFile` storage without ambient locale, gettext, codepage, or new dependency scope.
*   Persisted resources reject duplicate keys and negative payload lengths; persisted help rejects negative counts and unresolved or invalid reference ranges before presentation.
*   Framework decisions are `UseExistingFramework` for editor/file, and bounded `SmallFrameworkFix` for help graph validation, compiler, resources, and i18n. Historical sources remain read-only.
*   Wave-3 examples, mouse, terminal/charset/font work, TP7, broad redesign, services, and dependencies remain outside 018.
*   The next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave3-Visual-Component-Porting.019-wave3-visual-component-porting.md` for `bhelp`, `helpdemo`, `i18n`, `tvedit`, and `tvhc`.

### 019-wave3-visual-component-porting
- Current implementation status: Wave-3 visual component porting is implemented locally; final evidence is in `specs/019-wave3-visual-component-porting/pr-evidence.md`.
- `BHelp`, `HelpDemo`, `I18n`, `TvEdit`, and `TvHc` use visible main components, a real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, rendered buffer/cell evidence, status, and description. The Wave-3 matrix passes 14/14 locally, including five constrained `48x16` layouts.
- Framework decisions are `UseExistingFramework` for TvEdit, HelpDemo, I18n, and TvHc, and bounded `IntentionalDeviation` for BHelp because the proprietary unchecked Borland `.tch` decoder is omitted.
- Embedded/source-controlled learning content and test-owned temporary paths are the only data boundaries. Historical sources remain read-only.
- Mouse interaction, terminal/charset/font work, Wave 4, broad redesign, services, new dependencies, and runtime/product AI remain outside 019.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_04_MouseSupportAndInteraction.020-mouse-support-interaction.md`.

### 020-mouse-support-interaction
- Current implementation status: bounded mouse support and interaction hardening is implemented locally; final evidence is in `specs/020-mouse-support-interaction/pr-evidence.md`.
- `ConsoleMouseIngress` accepts only complete bounded SGR 1006 left press, pressed move, and release reports and publishes zero or one existing `TEvent`; malformed syntax, range, button, capability, and phase input is rejected atomically.
- `TGroup` routes mouse down to one topmost visible target, transfers focus only to selectable targets, and preserves existing exactly-once control commands. Nested mouse coordinates traverse the full owner chain.
- The only mouse drag contract is moving a `TWindow` from its title row. Owner bounds, release, Escape, capability loss, disable, removal, shutdown, and the existing `Ctrl+F5` keyboard fallback are proven.
- Interactive macOS/Linux terminals and WSL use the SGR capability contract; native Windows Console and redirected/headless I/O remain honest `Unsupported` boundaries. Wheel, hover, touch, extra buttons, full protocol parity, and additional drag targets remain out of scope.
- Primary proof runs through `TProgram.GetEvent` and `app.Run()` and combines concrete focus/command/drag state, target identity, visible text, and rendered buffer/cell assertions. Historical sources remain read-only.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_05_TerminalCharsetAndEmulation.021-terminal-charset-hardening.md`.

### 021-terminal-charset-hardening
- Current implementation status: terminal and charset hardening is implemented locally; final evidence is in `specs/021-terminal-charset-hardening/pr-evidence.md`.
- `TerminalSession` provides a bounded in-process transcript, cursor, 16-color attributes, 4,096-cell FIFO history, deterministic resize/reset/lifecycle, and atomic C0/CSI subset recovery without a process, shell, or PTY.
- `TerminalCharsetMapper` uses Unicode plus one fixed KOI8-R table and U+FFFD replacement. `BitmapFontFixture` validates only raw 8x16/256/4,096-byte metadata; no host codec, font installation, or historical generator is used.
- `TerminalProfile` uses a closed `System.Text.Json` schema. Invalid schema is rejected as a whole; unavailable font/host capability uses an observable safe default and `Unsupported` status.
- `TTerminalView` reuses the Driver-owned session and existing Controls app loop. Primary proof combines session/profile state, concrete view identity, text status, cursor, rendered cells, controlled keyboard input, and deterministic quit.
- Historical terminal, Cyrillic, font, Eterm, and XTerm sources remain read-only intent. Full ANSI/VT/XTerm parity, visible Wave-4 examples, host mutation, new dependencies, services, persistence, and runtime/product AI remain outside 021.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave4-Visual-Component-Porting.022-wave4-visual-component-porting.md`.

### 022-wave4-visual-component-porting
- Current implementation status: Wave-4 visual component porting is implemented locally; final evidence is in `specs/022-wave4-visual-component-porting/pr-evidence.md`.
- `Terminal`, `Cyrillic`, `Fonts`, `ETerm`, and `XTerm` provide a visible main component, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, exact view identity, rendered buffer/cell evidence, controlled operation, fallback, description, and deterministic host classification. Narrow viewports preserve example identity and status meaning.
- Terminal, Cyrillic, and Fonts are `UseExistingFramework`; ETerm and XTerm are bounded `IntentionalDeviation` immutable manifests that do not parse or execute historical configuration.
- The copied raw 8x16 fixture is byte-identical to historical `font.016`; historical sources remain read-only. No process, shell, PTY, host font/codepage/locale/keyboard mutation, X resource database, terminfo, external command, or new dependency entered scope.
- Physical host observation remains separate from deterministic in-process and remote-CI evidence. Unsupported capabilities use text-first fallbacks and never claim unavailable native behavior.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_06_A11Y_Framework.023-a11y-framework.md`.

### 023-a11y-framework
- Current implementation baseline: deliver the managed accessibility layer from `specs/023-a11y-framework/` and `requirements/intakes/archive/Lastenheft_06_A11Y_Framework.023-a11y-framework.md`.
- Scope is limited to opt-in `IAccessibleWidget` text, typed `cmFocusChanged` announcements, immutable shortcut queries for `TMenuBar`/`TStatusLine`, explicit `TColorScheme.HighContrast`, keyboard coverage inventory, the `A11yFramework` reference app, and DocFX/Axe evidence.
- Focus announcements reuse one existing broadcast and propagate descendant focus to the shell; non-migrated views remain compatible without fabricated labels.
- Keyboard acceptance requires behavioral proof or a concrete `N/A` for Tab, Shift+Tab, arrows, Enter, and direct shortcuts across every inventoried selectable control family.
- Native AT-SPI, NSAccessibility, UI Automation, speech, full-control migration, terminal-wide WCAG claims, Wave 1-4 remediation, and Feature 024 are out of scope.
- Historical Turbo Vision has no direct equivalent for the modern semantic A11Y contracts; relevant focus/menu/status sources are read-only intent context only.
- The former post-023 Wave-5 statement is historical; Feature 028 completed the TV203/Free Vision closure, Feature 029 is the sole immediate intake, and the separate mandatory Feature-030 magiblot evolution audit follows before any finding-derived hardening.

### 024-tv203-freevision-conformance-audit
- Current planning baseline: the original audit and merge remain complete, but consumer-review Revision 2 in `specs/024-tv203-freevision-conformance-audit/consumer-readiness-review.md` supersedes the zero-finding forward decision.
- Borland documentation and `tv203s/` remain primary; official Free Vision commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` is external secondary evidence only. `TVDEMOS/` and `TVFM/` are read-only consumer evidence.
- The inventory remains 151 historical `.cc` rows, 119 maintained production `.cs` files, 176 exported public types, 16 domains, and 48 contracts.
- Revision-2 decisions are 7 `Aligned`, 27 `IntentionalModernization`, 1 `ConsciouslyOmitted`, 8 `BehavioralDrift`, and 5 `EvidenceGap`.
- The 13 accepted findings route exactly nine items `F001`-`F009` to `Core025` and four items `F010`-`F013` to `ComponentData026`; both are closed, while Wave 5 and Wave 6 remain blocked through Feature 028, Feature 029, Feature 030, all combined finding-derived hardening, and the new independent closure.
- Revision 2 changes audit data, validation, evidence, requirements, and ordering only; it does not implement runtime behavior or port examples.

### 027-pre-wave5-conformance-closure
- Feature 027 remains valid historical closure evidence: feature merge `35414af` and causal closeout PR #67 merge `1da2b211e84221db87ab9f959b7b40d3ae2b01f0` are complete.
- Its original 13/34/1/0/0 decision set and zero-finding Wave-5 release statement are superseded for forward planning by Feature-024 Revision 2, not rewritten retroactively.
- Final evidence is in `specs/027-pre-wave5-conformance-closure/closure-evidence.md`; all 90 historical tasks are complete.

### 025-core-runtime-conformance-hardening
- Current implementation status: all nine `Core025` findings are implemented and proven; final evidence is in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`.
- `F001`-`F009` close concrete event kinds, focus and group state, idle lifecycle, desktop stack, modal/close, shared command state, real keyboard ingress, and bounded generic drag through real-path red/green proof.
- Feature-024 resolution metadata records exactly 13 non-documentation-only closures from Features 025 and 026 while preserving the Feature-028 and Wave-5/Wave-6 gates.
- Feature 028 completed the independent closure after Feature 026; its archived intake is `requirements/intakes/archive/Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.028-pre-wave5-wave6-conformance-closure.md`.

### 026-component-data-conformance-hardening
- Current implementation status: all four `ComponentData026` findings are implemented and proven; final evidence is in `specs/026-component-data-conformance-hardening/pr-evidence.md`.
- `F010`-`F013` close dialog completion/child validation, phase-aware input validation, mode-aware typed file outcomes, and allowlisted named UI-resource composition through real-path red/green proof.
- Feature-024 metadata now contains 139 maintained source files, 211 exported public types, and exactly 13 closed finding resolutions; Wave 5 and Wave 6 remain blocked through the combined Terminal.GUI/magiblot-derived closure after Features 029 and 030.

### 028-pre-wave5-wave6-conformance-closure
- Current implementation status: the evidence-only closure independently revalidated all 13 findings, seven real-path slices, and 13 protected consumer groups without a product, API, dependency, example, or protected-source change.
- The existing TV203/Free Vision gate is `ReadyForTerminalGuiAudit`; 12 consumer groups use the existing framework and destructive Wave-6 policy remains one bounded `FollowUpHardening`.
- Both waves remain `BlockedPendingTerminalGuiAudit`; Feature 029 is the sole immediate intake and Feature 030 is the mandatory successor before hardening or closure.

### 029-tv203-freevision-terminalgui-conformance-audit
- Current implementation status: the read-only audit is complete locally; final evidence is in `specs/029-tv203-freevision-terminalgui-conformance-audit/pr-evidence.md`.
- Turbo Vision 2.0.3 remains authoritative; pinned Free Vision remains secondary evidence, and Terminal.GUI v1.9.0 at commit `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3` is an additional modern C# implementation opinion.
- All `C001`-`C048`, 16 domains, 25 pinned sources, and 13 Wave consumer groups are covered. No `C049+`, `CandidateFinding`, or `ProductDecision` was admitted.
- The audit changes no runtime, API, dependency, example, historical, consumer, Free Vision, or Terminal.GUI source. It hands 48 `TGO###` observations to Feature 030 and creates no hardening or closure Lastenheft itself.
- Feature 030 completed the combined TG/MB audit locally; Feature 031 is the sole next intake after merge, and Wave 5 plus Wave 6 remain blocked through its independent closure.
- After every autonomous run, promote only reproducible provider-neutral preset learning through a Home-Baseline patch release and exact tag-ZIP adoption before the next run; record `NoPromotion` without an empty branch or PR. The consolidated pre-Wave-5 catalog update is submitted as `github/spec-kit#3569`; its external processing does not grant authority and does not indefinitely block the already installed v0.2.2 field run.

### 030-tv203-magiblot-evolution-audit
- Current implementation status: the read-only audit data and targeted validator are locally complete; final repository, documentation, coverage, remote review, and delivery gates remain.
- The exact magiblot pin is commit `57b6f56b38e0ee75240a80a10ee0e11470c24693`, tree `96dd03873955689ff0a79f6c8107a8148fe1ebd6`, with the multipart COPYRIGHT hash `66220bae...548`; its direct C++ lineage remains a modernization witness, never a normative source.
- Fifty selected source records support 48 relations: 27 `CorroboratesOriginal`, 12 `CorroboratesModernization`, 6 `AlternativeModernization`, and 3 `NotApplicable`. The 48 `MB*` decisions are 39 `AlreadySatisfiedWithNewEvidence`, 6 `IntentionalDeviation`, and 3 `RejectedComparison`.
- All 48 `TGO*` and 48 `MB*` observations have one combined disposition. No reproducible `CF*` finding or `ProductDecision` exists, so no hardening intake is generated; `requirements/intakes/archive/Lastenheft_16_Pre-Wave5-Wave6-Combined-Conformance-Closure.031-combined-conformance-closure.md` defines Feature 031 as the active closure intake.
- Runtime, API, dependency, example, consumer, historical, and external sources remain unchanged. Wave 5 and Wave 6 stay `BlockedPendingCombinedConformanceClosure`.
- `requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md` remains a binding deferred intake only after the complete Wave-6 closeout and receives its feature number then.

### 031-combined-conformance-closure
- Current implementation status: the evidence-only independent closure is complete; final evidence is in `specs/031-combined-conformance-closure/delivery-closeout.md`.
- Exactly 48 contracts, 13 consumers, 48 TGO and 48 MB observations, 96 dispositions, all accepted source pins and hashes, and the closed `F001`-`F013` set are revalidated.
- Product runtime, public APIs, dependencies, packages, projects, examples, consumers, historical sources, and external sources remain unchanged.
- Feature 031 made Wave 5 `Eligible`; Features 032 and 033 have since delivered both stages. Wave 6 remains only `ConditionallyReady` and still requires review of the actual combined Wave-5 delta.

### 032-wave5-tp7-functional-porting
- Current implementation status: the functional Wave-5 stage is delivered; all 15 read-only `TVDEMOS/*.PAS` sources have one role, all six consumers use the existing framework, and ten `Tp7*` projects provide normal plus controlled-smoke entry points.
- Primary proof covers calculator, central apps, resources, deterministic domain state, mouse capability/fallback, and exact 15/6/10/10 traceability through real app loops, state, view, and rendered cells.
- File, resource, help, generator, and mouse boundaries remain controlled; no host state, arbitrary user data, dependency, historical source, or broad framework behavior is changed.
- `requirements/intakes/archive/Lastenheft_18_Wave5-TP7-Showcase-Remediation.033-wave5-tp7-showcase-remediation.md` is the archived intake derived from the ten concrete Stage-2 deltas. Feature 033 is fully delivered through PR #96 and merge `d476e63`; Wave 6 remains blocked pending the actual combined Wave-5 delta review.

### 034-wave5-combined-delta-closure
- Current implementation status: Feature 034 is fully delivered through PR #99, final head `016692d`, merge `7fb52e2`, and its causal closeout.
- The read-only audit proves exact 15 source, 6 consumer, 10 example, 10 functional-proof, 10 showcase-closure, and 10 guide/launch cardinalities. All ten decisions are `AcceptedIntentionalDeviation`; there are zero open `Gap` dimensions, Candidate Findings, Product Decisions, owner groups, or hardening intakes.
- Product runtime, APIs, dependencies, projects, examples, framework code, `TVDEMOS/`, `TVFM/`, `tv203s/`, and external comparison sources remain unchanged. The final 22-check matrix and 13 exact-head gates passed; checkout-neutral SHA-256 and Git-blob proof passed on Ubuntu, macOS, and Windows.
- Wave 5 is `Closed`. Feature 035 is delivered through PR #101, final head `207e807`, and merge `52f77fa`; it provides Wave-6 Stage 1.

### 036-wave6-tvfm-showcase-remediation
- Feature 036 is fully delivered through PR #104, final head `a0d5062`, and merge `559bffb`.
- The exact closure contains ten `W6S` showcase decisions, one `ShowcaseComplete` entry-point decision, and 24 validated read-only `TVFM/` source hashes. The Windows provider correction makes historical `.PAS`/`.BAT` text hashes checkout-neutral while preserving byte-exact resource hashes.
- Feature-035 controlled-root, path, search, preview, viewer, one-shot intent, mutation, dependency, process, shell, host, and arbitrary-user-data boundaries remain unchanged. `TVFM/`, `TVDEMOS/`, and `tv203s/` stay read-only.
- The independent Wave-6 closure is now the preferred active intake at `requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md`; it reserves Feature 037 without starting it. The post-Wave-6 portfolio audit remains blocked by that closure.

### Autonomous Red-Proof Completeness
- Before the first red test batch, review imports, public XML docs, harness helpers, focus/ownership assertions, and linked-source assembly identity.
- Group independent negative cases only as a bounded project-local red matrix with explicit failure boundaries and shared ownership.
- When source is linked into multiple assemblies, cross-project proof uses public contracts or state delegates and does not assume one CLR type identity.
- Keep a causal closeout evidence-only and single-commit-capable: do not require its own PR URL, reviewed-head result, or merge commit inside that same repository file; verify terminal facts externally without recursive closeout.
- When push and pull-request events create equivalent workflow sets, use pull-request-context checks as the gate and record duplicate runs as noise; cancel them only under an explicit safe workflow/concurrency contract.

## Active Technologies

Einstiege / Entry files: `GEMINI.md`

- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, docfx (004-editor-file-help-streams)
- Lokales Dateisystem sowie persistente binaere Help-/Ressourcen-Streams; keine Datenbank in diesem Inkrement (004-editor-file-help-streams)
- C# `latest` on .NET 10 (`net10.0`) + Existing modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; docfx for API documentation validation; GitHub Actions for existing CI (005-driver-consolidation-m07)
- Versionskontrollierter Markdown-Nachweis in `docs/porting-status.md`; keine Datenbank; Kompatibilitaetsnachweise duerfen als Repo-Notizen oder dokumentierte Kommandoausgaben vorliegen (005-driver-consolidation-m07)
- C# `latest` on .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, bestehende MSTest-Suiten plus ggf. notwendige Compatibility-spezifische Validierung, Coverlet-Nachweise, `dotnet format`, docfx, `Pflichtenheft.md` und `docs/porting-status.md` fuer den formalen Phase-8-Gate-Nachweis (006-close-phase8-gate)
- Reine Repository-Nachweisartefakte; keine Datenbank und keine Beispielanwendungs-Auslieferung in diesem Inkrement (006-close-phase8-gate)
- Source-controlled example projects under `examples/`; wave-1 examples (`desklogo`, `msgcls`, `tutorial`, `videomode`) delivered; 41 smoke tests green; next: Wave 2 Controls and Dialogs (007-port-wave1-examples)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation (008-controls-revision)
- In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage (008-controls-revision)
- C# `latest` on .NET 10 (`net10.0`) + Bestehendes `TuiVision.Core`-Geometrie-/Event-/Buffer-Fundament; bestehende `TuiVision.Controls`-Shell- und Widget-Basis aus `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`, `TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, `TParamText`, editor-orientiertes `TIndicator` nur als Kontrastfall); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; optional `docfx docfx.json`; GitHub Actions plus vorhandene Example-Smoke-Infrastruktur fuer die nachgelagerte Wave-2-Readiness (009-controls-widgets-and-collections)
- In-Memory-UI-Zustand in Produktion; versionskontrollierte Planungs-, Test-, Nachweis- und bereits gelieferte Example-Artefakte in `specs/`, `tests/`, `docs/` und `examples/`; keine Datenbank oder externer Persistenzdienst (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream contex (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell, dialog, file, color, history, and widget types (`TDialog`, `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, `THistory`, `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, `TColorDisplay`, `TComboBox`, `TProgressBar`, `TParamText`); existing `TuiVision.Serialization` archive/resource foundation (`TRecordRegistry`, `TRecordSerializer`, `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TResourceFile`, `TResourceCollection`, `pstream` family); MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (010-standard-dialogs-designer)
- In-memory dialog state and session-only history; real local file-system metadata for file-listing/validation only; source-controlled tests/proof artifacts; minimal persisted dialog-description fixture through existing serialization/resource primitives; no database or external service storage (010-standard-dialogs-designer)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing framework modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; new wave-2 example projects under `examples/`; existing `tests/TuiVision.Examples.SmokeTests/`; MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (011-port-wave2-examples)
- Runtime example state is in memory; standard-dialog file flows use real local file-system metadata only; `dlgdsn` may use source-controlled dialog-description fixtures through existing Serialization/resource primitives; no database, external service, persisted user history, or new dependency planned (011-port-wave2-examples)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet test stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (012-interactive-wave2-demos)
- Runtime example state is in memory. Dialog-designer and file/path demonstrations use source-controlled fixtures, fixed repository paths, or test temporary directories. The examples must not persist user history, write user data as part of normal demonstration, read arbitrary user file contents as proof, or add a database/external service. (012-interactive-wave2-demos)
- Runtime example state remains in memory. Controlled examples may use source-controlled fixtures, fixed repository paths, or test temporary directories for metadata, rendering, validation, or rejection proof. The feature must not add a database, external service, network dependency, persistent user history, or arbitrary user-file content reads. (013-wave2-visual-component-remediation)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (014-wave1-functional-hardening)
- Runtime example state remains in memory. Proof data is limited to existing source-controlled files, controlled example fixtures if needed, or test temporary directories. No database, external service, network dependency, persistent user history, arbitrary user-file content reads, or runtime/product AI storage is planned. (014-wave1-functional-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest/Coverlet validation; existing DocFX plus Playwright/axe web A11Y tooling when documentation triggers apply. No new runtime NuGet dependency is planned. (015-didactic-comment-hardening)
- Source-controlled Markdown evidence and guidance files only. Production code state and tests keep their current storage model. No database, external service, network dependency, persistent user history, runtime/product AI storage, or arbitrary user-file proof path is planned. (015-didactic-comment-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`); Bash and PowerShell 7 for repository tooling + Existing TuiVision projects, MSTest, Coverlet, DocFX, Playwright/axe, GitHub Actions, Gitleaks, and CycloneDX for .NET 6.2.0 as a repository-local tool. No new runtime package is planned. (016-secure-development-hardening)
- Source-controlled Markdown, YAML, shell/PowerShell scripts, a local .NET tool manifest, and test fixtures. Generated evidence is written to temporary or ignored directories. No database, service, credential, runtime AI, or user-data store is introduced. (016-secure-development-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing TuiVision modules, shared Wave-1 example composition, MSTest/Coverlet, DocFX, and Playwright/axe; no new package (017-wave1-visual-component-remediation)
- Runtime example state remains in-process and session-only; proof and governance use source-controlled Markdown, with no database, external service, persistent user history, arbitrary user-file proof, or runtime/product AI (017-wave1-visual-component-remediation)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, and `TuiVision.Drivers.Console`; no new packages (019-wave3-visual-component-porting)
- Embedded/source-controlled learning content and test-owned temporary files only (019-wave3-visual-component-porting)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Drivers.Console`, `TuiVision.Controls`, and reviewed `TuiVision.Compatibility` key translation; no new packages (021-terminal-charset-hardening)
- In-memory session/history state plus source-controlled JSON and raw 8x16 fixtures (021-terminal-charset-hardening)
- C# / .NET 10 for durable test-only validation; Markdown and JSON for evidence + existing framework assemblies, MSTest, `System.Text.Json`, Git, official pinned FPC source checkout (024-tv203-freevision-conformance-audit)
- repository-owned JSON and Markdown evidence; external Free Vision worktree under `/tmp`, never tracked (024-tv203-freevision-conformance-audit)
- C# / .NET 10 + existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, MSTest; no new package (025-core-runtime-conformance-hardening)
- in-process state only; audit JSON and Markdown evidence are repository-owned documentation (025-core-runtime-conformance-hardening)
- C# with latest language version on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Serialization`; no new package or runtime dependency (026-component-data-conformance-hardening)
- Existing bounded binary archive and `TResourceFile`; controlled temporary filesystem metadata for file-dialog proofs (026-component-data-conformance-hardening)
- C# / .NET 10 for test-only validation; JSON, Markdown, YAML, Bash and PowerShell evidence tooling + existing MSTest 4.0.1, Coverlet 6.0.4, DocFX, Playwright/Axe, Lynx, jq, xmllint, Git and GitHub Actions (028-pre-wave5-wave6-conformance-closure)
- repository-owned JSON and Markdown only; historical, consumer, and Free Vision sources remain read-only (028-pre-wave5-wave6-conformance-closure)
- C# / .NET 10 for test-only validation; JSON and Markdown for audit evidence + existing MSTest 4.0.1, System.Text.Json, Coverlet 6.0.4, DocFX, Playwright/Axe, Lynx, Git and GitHub Actions (029-tv203-freevision-terminalgui-conformance-audit)
- repository-owned Feature-029 JSON and Markdown; external Terminal.GUI checkout remains untracked under a temporary directory (029-tv203-freevision-terminalgui-conformance-audit)
- C# 14 / .NET 10 for test-only validation; JSON and Markdown for evidence + Existing BCL `System.Text.Json`, MSTest 4.0.1, existing repository scripts and workflows; no new package (031-combined-conformance-closure)
- Source-controlled closed JSON and Markdown evidence; temporary external checkouts under `/tmp` only (031-combined-conformance-closure)
- C# 14 / .NET 10 + Existing TuiVision.Core, TuiVision.Controls, TuiVision.Serialization, TuiVision.Compatibility, TuiVision.Drivers.Console, MSTest 4.0.1 and BCL System.Text.Json; no new package (032-wave5-tp7-functional-porting)
- Embedded UTF-8 fixtures, controlled test-temporary files, existing THelpFile and TResourceFile; no database or service (032-wave5-tp7-functional-porting)
- C# 14 / .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, DocFX and Playwright/Axe; no new package (035-wave6-tvfm-functional-porting)
- Source-controlled UTF-8/binary fixtures copied into test-owned temporary roots; controlled local filesystem only; no arbitrary user data, shell/external viewer, database or service (035-wave6-tvfm-functional-porting)

### 007-port-wave1-examples
- Current status: Wave 1 delivered (2026-03-28). `desklogo`, `msgcls`, `tutorial` (16 steps), `videomode` are ported, smoke-tested, and guide-documented.
- Wave 1 scope: `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`; shared smoke-test infrastructure in `tests/TuiVision.Examples.SmokeTests/`; guides in `docs/guides/examples/`.
- Next open scope: Wave 2 – Controls and Dialogs (requires Controls/Dialog layer as prerequisite before planning starts).
- Planning decisions now fixed: headless smoke seam via `bool headless` constructor parameter + `GetEvent()` override; in-process MSTest execution without external process spawning; bilingual German-first/English-second XML docs and comments at CEFR-B2; `DisplayModeCoordinator.ProbeResizeSupport()` cross-platform probe with CA1416 suppressed.

## Recent Changes

Einstiege / Entry files: `GEMINI.md`

- 014-wave1-functional-hardening: Planartefakte fuer Wave-1 Functional Hardening ergaenzt, inklusive historischer Proof-Matrix, Smoke-Proof, Helper-Klassifikation, Fallback-, Missing-Core-, Dokumentations- und Governance-Planung.
- 013-wave2-visual-component-remediation: Sichtbare Hauptkomponenten oder stabile visuelle Runtime-Zustaende, echte `TStatusLine`, `Help -> Description`, gemeinsames `examples/Shared/Wave2Runtime.cs`, strengere app-loop-basierte Render-Smokes, Guides, README, Architektur-/Security-Evidence, Statistik und PR-Evidence fuer alle elf Wave-2-Beispiele umgesetzt.
- 004-editor-file-help-streams: Spezifikation und Requirements-Checklist fuer Phase 6 (Editor/Datei/Hilfe/Streams) angelegt.
- 004-editor-file-help-streams: Planartefakte (`plan.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/public-api.md`) erstellt und gemeinsame Agent-Hinweise auf den Post-Plan-Stand synchronisiert.
- 004-editor-file-help-streams: Plan-Review-Klarstellungen fuer Safe-Close vs. Overwrite, Wildcard-Filter in Dateidialogen, explizite Stream-Fehlerfaelle und nicht-funktionale Abgrenzungen eingearbeitet.
- 004-editor-file-help-streams: Editor-Clipboard/Overwrite, Datei-Metadaten-Synchronisation, Shell-Menue-/Status-Routing und das volle Coverage-Gate fuer Core/Controls/Serialization explizit nachgeschaerft.
- 004-editor-file-help-streams: Verbleibende Integrationsanforderungen fuer Event-Loop, Fokuswechsel, Menueausfuehrung und explizite Dialoginteraktion direkt in den Feature-Artefakten benannt.
- 005-driver-consolidation-m07: Spezifikation und Klarstellungen fuer Phase 7 angelegt, inklusive `M-07`-Nachweis ueber `docs/porting-status.md`, Pflicht-Primaarziel pro historischer Datei und reviewbarer Linux/Windows/WSL-Kompatibilitaetsnachweise.
- 005-driver-consolidation-m07: Planartefakte (`plan.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/phase-7-proof-contract.md`) erstellt und den Phase-7-Ansatz als capability-basierte Treiberkonsolidierung mit formalem `.cc`-Ledger-Scope beschrieben.
- 005-driver-consolidation-m07: Phase-7-Implementierung abgeschlossen: `DriverCapabilityMap.cs` mit 5 Faehigkeitsgruppen erstellt, `docs/porting-status.md` mit allen 151 historischen `.cc`-Dateien aufgebaut, 5 neue Treiber-Testdateien (30 Tests bestanden), `docs/guides/multi-mac-workflow.md` um Kompatibilitaetsnachweis erweitert, `checklists/phase-8-gate-review.md` angelegt, `Pflichtenheft.md`-Marker auf Phase-8-Gate-Abschluss verschoben.
- 006-close-phase8-gate: Spezifikation und Requirements-Checklist fuer den finalen `M-07`-Abschluss und die nachweisbare Phase-8-Gateschliessung angelegt; gemeinsamer Agent-Kontext auf den neuen Prioritaetsschritt synchronisiert.
- 006-close-phase8-gate: Die harte Coverage-Regel auf `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` jeweils `>= 70 %` erweitert und in die gemeinsamen Gate-Artefakte synchronisiert.
- 006-close-phase8-gate: Planartefakte (`plan.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/phase-8-gate-contract.md`) erstellt und den finalen Framework-Vollbeweis mit 5x-70%-Coverage, Repository-Volltestlauf und dediziertem Gate-Closure-Commit als verbindlichen Planstand beschrieben.
- 006-close-phase8-gate: Plan nachgeschaerft: Coverage gilt assembly-scharf pro Gate-Modul, und Platzhalter-/No-op-Code zaehlt nicht als gueltiger Phase-8-Abschluss.
- 006-close-phase8-gate: Analyse-Remediation eingearbeitet: `gate-docs.md` ist nun expliziter Bestandteil des Plan-Artefaktsets, `tests/TuiVision.Compatibility.Tests/` ist als feste Fallback-Suite benannt, und Skip-/Ignore-Faelle muessen in den Gate-Nachweisen auf ein dokumentiertes Tracking-Issue verweisen.
- 007-port-wave1-examples: Welle 1 portiert: `desklogo`, `msgcls`, `tutorial` (16 token-basierte Schritte), `videomode`. 41 Smoke-Tests gruen, Release-Build sauber, `dotnet format --verify-no-changes` bestanden. `Pflichtenheft.md` Welle-1-Checkliste abgehakt, Marker auf Welle 2 vorgeschoben.
- 008-controls-revision: Controls-Revision implementiert: `TSubMenu`, `TStatusDef`, `WindowFlags` neu; `TMenuBar`, `TStatusLine`, `TWindow`, `TDialog`, `TMenuItem`, `TView` erweitert; 338 Tests gruen, 84,02 % Abdeckung, Format-Gate bestanden; Nachweissurfaces aktualisiert, Lastenheft umbenannt.
- 010-standard-dialogs-designer: Framework-Readiness fuer Standarddialoge und Dialog-Designer implementiert: Datei-/Verzeichnisentscheidungen ohne Dateiinhalt-I/O, Color-/Display-/symbolische Charset-Auswahl, validierte Dialogbeschreibung, persistierter Roundtrip und fehlerhafte Persistenzdaten-Ablehnung; fokussierte Controls- und Serialization-Tests gruen.
- 011-port-wave2-examples: Planartefakte fuer Welle 2 erstellt: 11 neue Beispielprojekte, dedizierte Example-Smoke-Tests, DE-first/EN-second Guides, Architektur-/Security-/A11Y-Nachweise und klarer `sdlg`/`sdlg2`-Scope als historische ScrollDialog/ScrollGroup-Beispiele.
- 012-interactive-wave2-demos: Interaktive Showcase-Stufe fuer Welle 2 implementiert: alle elf Wave-2-Beispiele besitzen sichtbare normale Runtime-Pfade, app-loop-basierte Smoke-Nachweise, aktualisierte Guides, README-, Architektur-/Security-/A11Y- und PR-Evidence.

## Active Feature Context

Einstiege / Entry files: `.github/copilot-instructions.md`

### 004-editor-file-help-streams
- Align active work with `specs/004-editor-file-help-streams/spec.md` and the planning artifacts in `specs/004-editor-file-help-streams/`
- Scope is limited to reusable framework components in `src/TuiVision.Controls` and `src/TuiVision.Serialization`: `TEditor`, `TMemo`, `TFileEditor`, `TEditWindow`, file/dialog/history helpers, help topics/viewers/windows, stream primitives, and named resource containers
- Editor flows must cover text editing, insert/overwrite behavior, clipboard-oriented actions, search/replace, modified-state handling, explicit safe-close decisions before unsaved changes are discarded, and distinct overwrite decisions when save conflicts occur
- File flows must keep directory navigation, file lists, current file-information metadata, wildcard filtering, manual path entry, and history recall synchronized inside reusable dialogs
- Help flows must support context-based topic lookup, cross-reference navigation, and fallback content for missing contexts
- Stream/resource flows must preserve named lookup semantics and reject malformed persisted input explicitly, including truncated, trailing, unknown-type, and cyclic payload failures
- Integration coverage for this feature must explicitly include event-loop-aware shell interaction, focus transitions, menu execution, and dialog interaction rather than relying on those behaviors only implicitly
- Planning decisions now fixed for this feature: dedicated runtime help files, shared-reference preservation without cyclic-graph support, exact case-sensitive resource keys, `LF` default for new files, preserved line endings for loaded files, and explicit overwrite decisions after external file changes
- Keep this increment scoped to reusable framework components only; example applications such as `tvedit`, `bhelp`, and `helpdemo`, as well as driver consolidation and calculator/macros/OS-shell integrations, are out of scope

### 005-driver-consolidation-m07
- Align active work with `specs/005-driver-consolidation-m07/spec.md` and the planning artifacts in `specs/005-driver-consolidation-m07/`
- Scope is limited to the managed driver baseline in `src/TuiVision.Drivers.Console`, the supporting validation in `tests/TuiVision.Drivers.Tests`, and the proof ledger `docs/porting-status.md`
- The proof ledger must cover every historical `.cc` implementation file in `tv203s/contrib/tvision/classes` with one mandatory primary target, optional secondary targets, status, evidence, and rationale
- Linux and Windows/WSL compatibility checks are required as reviewable evidence for this phase, but may still be manual or semi-automated rather than mandatory CI gates
- Planning decisions now fixed for this feature: `.cc` files are the formal `M-07` ledger scope, ancillary `.c`/`.h` files may appear only as rationale support, capability buckets replace per-OS lineage as the review model, and Phase 7 remains distinct from the later full Phase-8 gate closure
- Keep this increment scoped to driver consolidation and proof preparation only; mandatory example waves and complete Phase-8 gate closure remain out of scope
- Phase-7 implementation is complete: `DriverCapabilityMap.cs` with 5 capability buckets, `docs/porting-status.md` covering all 151 `.cc` files, 30 driver tests passing, compatibility evidence in `docs/guides/multi-mac-workflow.md`, gate checklist in `checklists/phase-8-gate-review.md`; next priority is Phase-8 gate closure (Core/Controls/Serialization/Compatibility/Drivers.Console coverage each ≥ 70 %, full dotnet test suite)

### 006-close-phase8-gate
- Align active work with `specs/006-close-phase8-gate/spec.md` and the planning artifacts in `specs/006-close-phase8-gate/`
- Scope is limited to final `M-07` proof closure plus the remaining Phase-8 entrance evidence across `docs/porting-status.md`, `Pflichtenheft.md`, the existing module test suites plus any required Compatibility-focused validation additions, coverage evidence, formatting evidence, and API-documentation validation
- Every historical `.cc` ledger row must finish in `portiert + getestet` or `bewusst ausgelassen + Begruendung`; no `portiert + Test ausstehend` row may remain after closure is claimed
- Gate closure must include explicit build, full-test, coverage, formatting, and conditional API-doc proof, and must keep the 25 mandatory example waves blocked until the closure is formally recorded
- `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` must each satisfy the hard 70 % line-coverage gate before Phase 8 is declared open
- The planning baseline now also fixes repository-wide `dotnet test`, `tests/TuiVision.Compatibility.Tests/` as the dedicated Compatibility fallback suite when shared tests are insufficient, assembly-specific coverage evidence for the five gate modules, conditional Linux/Windows/WSL evidence, rejection of placeholder-only or no-op-only gate modules, same-change proof-surface updates when a gate module is removed, mandatory tracked-issue references for skip/ignore outcomes, local-versus-CI coverage conflict resolution, and a dedicated gate-closure commit as hard closure criteria
- Keep this increment scoped to gate closure only; mandatory example waves, substitute follow-on example scope from `TVDEMOS/` or `TVFM/`, and unrelated new framework features remain out of scope

### 007-port-wave1-examples
- Current status: Wave 1 delivered (2026-03-28). `desklogo`, `msgcls`, `tutorial` (16 steps), `videomode` are ported, smoke-tested, and guide-documented.
- Wave 1 scope: `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`; shared smoke-test infrastructure in `tests/TuiVision.Examples.SmokeTests/`; guides in `docs/guides/examples/`.
- Next open scope: Wave 2 – Controls and Dialogs (requires Controls/Dialog layer as prerequisite before planning starts).
- Planning decisions now fixed: headless smoke seam via `bool headless` constructor parameter + `GetEvent()` override; in-process MSTest execution without external process spawning; bilingual German-first/English-second XML docs and comments at CEFR-B2; `DisplayModeCoordinator.ProbeResizeSupport()` cross-platform probe with CA1416 suppressed.

### 008-controls-revision
- Align active work with `specs/008-controls-revision/spec.md` and the planning artifacts in `specs/008-controls-revision/`.
- Scope is limited to `src/TuiVision.Controls`, `tests/TuiVision.Controls.Tests`, and the follow-through proof surfaces `docs/porting-status.md`, `Pflichtenheft.md`, and `docs/project-statistics.md`.
- The revision must close the current menu, status-line, window, and dialog behavior gap before the next Controls/Dialog example wave proceeds.
- Planning decisions now fixed: `TSubMenu` as a standalone declaration type; exactly one submenu level; `TStatusDef` with inclusive ranges, first-match-wins ordering, and neutral-empty fallback; explicit `HelpContext` on `TView` with `GetStatusHints()` retained only as a compatibility bridge for current callers such as `TEditor` and `TEditWindow` when no definitions are configured; `WindowFlags` limited to `Close` and `Move`; `Ctrl+F5` move mode; `Valid(ushort command)` for `TDialog`; same-change updates for the affected `docs/porting-status.md` rows, `docs/project-statistics.md`, and the `>>> NAECHSTER SCHRITT <<<` marker in `Pflichtenheft.md` when the effective next step changes.

### 010-standard-dialogs-designer
- Current status: framework-readiness implemented on 2026-05-02; focused Controls and Serialization tests are green.
- Keep future work aligned with `specs/010-standard-dialogs-designer/spec.md` and the planning artifacts in `specs/010-standard-dialogs-designer/`.
- Scope is limited to hardening reusable `TuiVision.Controls` and `TuiVision.Serialization` dialog surfaces before downstream wave-2 examples; full `demo`, `sdlg`, `sdlg2`, and `dlgdsn` example ports remain out of scope for this increment.
- File and directory dialogs must keep path entry, wildcard filtering, file-list metadata, directory navigation, save-target decisions, and session-only history synchronized without performing file content I/O.
- Color, charset, and display selection must remain symbolic dialog-level state; terminal rendering, fonts, emulation, mouse behavior, editor/help behavior, and example-specific workflows are out of scope.
- Dialog-designer readiness requires a minimal validated dialog-description model with unique control IDs and unique command bindings inside one dialog, plus a small persisted roundtrip through existing serialization/resource primitives and explicit malformed-input rejection.
- Acceptance is framework-first in Controls/Serialization tests, with full keyboard operation required and mouse operation deferred unless already covered by existing generic event dispatch.

### 011-port-wave2-examples
- Current planning baseline: execute the Welle-2 example port from `specs/011-port-wave2-examples/spec.md` and `specs/011-port-wave2-examples/plan.md`.
- Scope is limited to the eleven mandatory wave-2 examples under `examples/`: `clipboard`, `demo`, `dlgdsn`, `dyntxt`, `inplis`, `listvi`, `progba`, `sdlg`, `sdlg2`, `tcombo`, and `tprogb`; matching smoke tests in `tests/TuiVision.Examples.SmokeTests/`; guides in `docs/guides/examples/`; and the proof surfaces in `Pflichtenheft.md`, `examples/README.md`, `docs/project-statistics.md`, `docs/architecture/`, and applicable `docs/security/` notes.
- `sdlg` and `sdlg2` must be completed in wave 2 for their real historical `ScrollDialog`/`ScrollGroup` purpose. Broader parity work is separate Historical Example Parity Cleanup and must not block wave-2 acceptance.
- Every new example needs at least one deterministic in-process smoke path that triggers example-specific visible behavior; startup plus clean exit is not sufficient.
- Standard-dialog proof belongs to `demo`, `dlgdsn`, or another historically justified wave-2 flow. File-content I/O, editor/help, terminal emulation, runtime mouse behavior, and real charset effects remain out of scope for wave-2 acceptance.

### 012-interactive-wave2-demos
- Current implementation status: interactive Wave-2 demo polish is implemented on branch `012-interactive-wave2-demos`; final validation evidence is tracked in `specs/012-interactive-wave2-demos/pr-evidence.md`.
- Delivered scope is limited to making the eleven Wave-2 examples visibly operable at normal runtime: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`; matching event-loop smoke tests in `tests/TuiVision.Examples.SmokeTests/`; guides, README, `pr-evidence.md`, and proportional architecture/security/A11Y/statistics evidence.
- Before wiring or accepting each example, review the relevant historical `.c`/`.cc` source and any important matching headers under `tv203s/` as read-only reference, capture the original demo intent, and document intentional user-visible deviations in guide or PR evidence.
- Every example must show first-screen purpose text, expose primary behavior through menu, keyboard, status, or command paths, and update visible text-first feedback after each demonstrated operation.
- Primary smoke proof must run `app.Run()` or the equivalent real application loop with injected `TEvent`, command, or key events. Direct helper methods may support setup or supplemental assertions only.
- `examples/Demo` is the P1 vertical slice and must prove at least three visible behaviors before the pattern is spread across the rest of the examples.
- File/path and dialog-designer flows stay read-only toward user data: use source-controlled fixtures, fixed repository paths, or test temporary directories; do not read arbitrary user file contents or persist user history as proof.
- Wave 3 and Wave 4 examples, mandatory mouse-only operation, broad framework redesign, new runtime dependencies, databases, external services, and DocFX publishing-model changes are out of scope.
- Next open mandatory example scope after this feature is Wave 3: editor, file, help, and stream demos such as `tvedit`, `bhelp`, and `helpdemo`.

### 013-wave2-visual-component-remediation
- Current implementation status: visual component remediation is implemented on branch `013-wave2-visual-component-remediation`; final validation evidence is tracked in `specs/013-wave2-visual-component-remediation/pr-evidence.md`.
- Scope is limited to the eleven Wave-2 examples: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`.
- Shared runtime support for the remediated examples lives in `examples/Shared/Wave2Runtime.cs` and is linked into the eleven scoped example projects.
- The primary parity proof is the visible UI composition itself: controls, dialogs, windows, view groups, scroll groups, progress displays, input/list/combo composition, or another stable visible runtime state.
- Each example must use the three-layer model: visible main component, real `TStatusLine` feedback, and keyboard-reachable `Help -> Description`.
- Primary smokes must drive the real app loop and combine concrete state assertions with view-tree proof plus buffer/cell rendered visibility proof at expected positions or regions.
- Historical C/C++ sources under `tv203s/` remain read-only intent references; intentional user-visible deviations must be documented in plan, tasks, guides, feature evidence, or PR evidence.
- AI-SBOM is `N/A` for this feature while AI is only development/agent tooling; re-evaluate if runtime/product AI, models, datasets, AI infrastructure, or delivered AI components enter scope.
- Wave 3/4 functionality, broad framework redesign, mandatory mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, and new runtime dependencies are out of scope.

### 014-wave1-functional-hardening
- Current implementation status: Wave-1 functional hardening is implemented on branch `014-wave1-functional-hardening`; final validation evidence is tracked in `specs/014-wave1-functional-hardening/pr-evidence.md`.
- Scope is limited to `Desklogo`, `MsgCls`, `Tutorial` steps `tvguid01` through `tvguid16`, and `Videomode`.
- The primary proof surface is `specs/014-wave1-functional-hardening/pr-evidence.md`, which records historical source, C# behavior, proof method, helper classification, negative/fallback proof, missing-core decisions, intentional deviations, validation, and the archived Lastenheft path.
- Managed runtime behavior is proven by executable smoke tests; evidence-only proof remains allowed only for explicitly documented no-runtime-target deviations.
- Helper or headless paths may be `PrimaryProof` only when they execute real example or application logic through public commands, events, application methods, or stable public state with concrete assertions; 014 added `PrimaryProof`, `SupplementalProof`, `SetupOnly`, and `LegacyOrTemporary` helper taxonomy.
- Historical C/C++ sources under `tv203s/` remain read-only intent references. `set-logo.cc` and `tv_logo.cc` are Desklogo asset/generator boundary context only.
- Validation baseline: targeted Wave-1 smokes 38/38 passed, full example smokes 91/91 passed, full Release tests 496/496 passed, coverage gate exceeded 70% for all required assemblies, `docfx docfx.json` passed with 0 warnings/errors, and Playwright/axe DocFX smoke passed 2/2 via explicit local-server workaround when sandboxed webserver startup is blocked.
- Feature 015 completed the didactic comment pass; the next open cross-cutting step is `requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.016-secure-development-hardening.md` before Wave-1 visual remediation.
- Wave-1 visual remediation, Wave 2/3/4 behavior, broad framework redesign, mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, new runtime dependencies, and runtime/product AI are out of scope.

### 015-didactic-comment-hardening
- Current implementation status: the selective didactic inline-code-comment hardening is implemented; final evidence is in `specs/015-didactic-comment-hardening/pr-evidence.md`.
- Scope is limited to selective didactic inline, block, file, or module comments, feature evidence in `specs/015-didactic-comment-hardening/pr-evidence.md`, and affected guidance/evidence surfaces.
- Review must cover central framework flows and relevant smoke-test helpers: event/command/dispatch, focus transitions, view hierarchy, StatusLine, Help/Description, dialog state, validation/rejection, buffer/cell proof, rendering snapshots, terminal fallbacks, historical Turbo Vision deviations, and proof helpers.
- Each reviewed area must receive exactly one decision: `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening`.
- Comments remain moderate and reason-focused: explain why, trade-off, constraint, historical deviation, or proof boundary; avoid restating obvious code; use German-first/English-second CEFR-B2 text for didactic explanation blocks.
- XML comments remain the API/DocFX surface; pure `//` or `/* */` hardening does not trigger DocFX. XML/API/generated docs/navigation/guides changes trigger the normal DocFX plus A11Y path.
- No runtime behavior change, API change, new dependency, new example porting, broad framework revision, Wave-1 visual remediation, or runtime/product AI is in scope.

### 016-secure-development-hardening
- Current implementation status: the project-wide secure-development hardening is implemented; final evidence is in `specs/016-secure-development-hardening/pr-evidence.md`.
- The durable control matrix covers all 157 `CL-XX-NN` controls with `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or `FollowUp` and complete ownership, risk, evidence, and re-evaluation fields.
- Delivered remediation is bounded to explicit malformed persistence rejection, immutable workflow dependencies, supply-chain automation, root disclosure guidance, and safe Bash/PowerShell archive-script parity.
- Local acceptance is 498/498 Release tests plus line coverage above 70% for Core, Controls, Serialization, Compatibility, and Drivers.Console; DocFX/axe and remote OS/CI proof remain delivery gates.
- Human legal, provider, organization, and agent-platform decisions remain `Open`; release provenance, reproducible-build/lock maturity, and RFC 9116 remain named follow-ups rather than implicit claims.
- The next open prioritized intake is `requirements/intakes/archive/Lastenheft_03_EditorHelpAndResourcesHardening.018-editor-help-resources-hardening.md` before Wave-3 visual porting.

### 017-wave1-visual-component-remediation
- Current implementation status: Wave-1 visual component remediation is implemented; final evidence is in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- `Desklogo`, `MsgCls`, all 16 Tutorial tokens, and `Videomode` now use the three-layer model: visible main state, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, and rendered buffer/cell evidence. The acceptance matrix contains four app rows plus 16 unique Tutorial rows; direct helpers are not primary proof.
- Shared composition in `examples/Shared/Wave1Runtime.cs` uses existing framework controls. Desklogo and MsgCls are `UseExistingFramework`; Tutorial and Videomode are bounded `IntentionalDeviation` decisions.
- Historical sources under `tv203s/` remain read-only. No functional re-port, Wave-2/3/4 behavior, broad framework redesign, new dependency, persistence, external service, or runtime/product AI entered scope.
- The complete example-smoke suite passes 101/101 locally; final repository, coverage, DocFX, A11Y, and remote checks remain delivery gates until recorded in feature evidence.
- Feature 018 closes the editor/help/resources intake; the next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave3-Visual-Component-Porting.019-wave3-visual-component-porting.md`.

### 018-editor-help-resources-hardening
- Current implementation status: Wave-3 editor/help/resources hardening is implemented; final evidence is in `specs/018-editor-help-resources-hardening/pr-evidence.md`.
- Existing `TEditor`, `TFileEditor`, `TEditWindow`, `THelpViewer`, and `THelpWindow` flows are retained and proven as coherent open/edit/search/replace/save, safe-close/conflict, persisted navigation, back, and fallback paths.
- `THelpSourceCompiler` provides a bounded `.topic` and `{text[:alias]}` source contract with strict UTF-8, deterministic contexts, forward-reference resolution, stable diagnostics, and atomic no-partial-model failure.
- `TLocalizedResourceLookup` uses explicit exact-language, caller-ordered fallback, then neutral keys over case-sensitive `TResourceFile` storage without ambient locale, gettext, codepage, or new dependency scope.
- Persisted resources reject duplicate keys and negative payload lengths; persisted help rejects negative counts and unresolved or invalid reference ranges before presentation.
- Framework decisions are `UseExistingFramework` for editor/file, and bounded `SmallFrameworkFix` for help graph validation, compiler, resources, and i18n. Historical sources remain read-only.
- Wave-3 examples, mouse, terminal/charset/font work, TP7, broad redesign, services, and dependencies remain outside 018.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave3-Visual-Component-Porting.019-wave3-visual-component-porting.md` for `bhelp`, `helpdemo`, `i18n`, `tvedit`, and `tvhc`.

### 019-wave3-visual-component-porting
- Current implementation status: Wave-3 visual component porting is implemented locally; final evidence is in `specs/019-wave3-visual-component-porting/pr-evidence.md`.
- `BHelp`, `HelpDemo`, `I18n`, `TvEdit`, and `TvHc` use visible main components, a real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, rendered buffer/cell evidence, status, and description. The Wave-3 matrix passes 14/14 locally, including five constrained `48x16` layouts.
- Framework decisions are `UseExistingFramework` for TvEdit, HelpDemo, I18n, and TvHc, and bounded `IntentionalDeviation` for BHelp because the proprietary unchecked Borland `.tch` decoder is omitted.
- Embedded/source-controlled learning content and test-owned temporary paths are the only data boundaries. Historical sources remain read-only.
- Mouse interaction, terminal/charset/font work, Wave 4, broad redesign, services, new dependencies, and runtime/product AI remain outside 019.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_04_MouseSupportAndInteraction.020-mouse-support-interaction.md`.

### 020-mouse-support-interaction
- Current implementation status: bounded mouse support and interaction hardening is implemented locally; final evidence is in `specs/020-mouse-support-interaction/pr-evidence.md`.
- `ConsoleMouseIngress` accepts only complete bounded SGR 1006 left press, pressed move, and release reports and publishes zero or one existing `TEvent`; malformed syntax, range, button, capability, and phase input is rejected atomically.
- `TGroup` routes mouse down to one topmost visible target, transfers focus only to selectable targets, and preserves existing exactly-once control commands. Nested mouse coordinates traverse the full owner chain.
- The only mouse drag contract is moving a `TWindow` from its title row. Owner bounds, release, Escape, capability loss, disable, removal, shutdown, and the existing `Ctrl+F5` keyboard fallback are proven.
- Interactive macOS/Linux terminals and WSL use the SGR capability contract; native Windows Console and redirected/headless I/O remain honest `Unsupported` boundaries. Wheel, hover, touch, extra buttons, full protocol parity, and additional drag targets remain out of scope.
- Primary proof runs through `TProgram.GetEvent` and `app.Run()` and combines concrete focus/command/drag state, target identity, visible text, and rendered buffer/cell assertions. Historical sources remain read-only.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_05_TerminalCharsetAndEmulation.021-terminal-charset-hardening.md`.

### 021-terminal-charset-hardening
- Current implementation status: terminal and charset hardening is implemented locally; final evidence is in `specs/021-terminal-charset-hardening/pr-evidence.md`.
- `TerminalSession` provides a bounded in-process transcript, cursor, 16-color attributes, 4,096-cell FIFO history, deterministic resize/reset/lifecycle, and atomic C0/CSI subset recovery without a process, shell, or PTY.
- `TerminalCharsetMapper` uses Unicode plus one fixed KOI8-R table and U+FFFD replacement. `BitmapFontFixture` validates only raw 8x16/256/4,096-byte metadata; no host codec, font installation, or historical generator is used.
- `TerminalProfile` uses a closed `System.Text.Json` schema. Invalid schema is rejected as a whole; unavailable font/host capability uses an observable safe default and `Unsupported` status.
- `TTerminalView` reuses the Driver-owned session and existing Controls app loop. Primary proof combines session/profile state, concrete view identity, text status, cursor, rendered cells, controlled keyboard input, and deterministic quit.
- Historical terminal, Cyrillic, font, Eterm, and XTerm sources remain read-only intent. Full ANSI/VT/XTerm parity, visible Wave-4 examples, host mutation, new dependencies, services, persistence, and runtime/product AI remain outside 021.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave4-Visual-Component-Porting.022-wave4-visual-component-porting.md`.

### 022-wave4-visual-component-porting
- Current implementation status: Wave-4 visual component porting is implemented locally; final evidence is in `specs/022-wave4-visual-component-porting/pr-evidence.md`.
- `Terminal`, `Cyrillic`, `Fonts`, `ETerm`, and `XTerm` provide a visible main component, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, exact view identity, rendered buffer/cell evidence, controlled operation, fallback, description, and deterministic host classification. Narrow viewports preserve example identity and status meaning.
- Terminal, Cyrillic, and Fonts are `UseExistingFramework`; ETerm and XTerm are bounded `IntentionalDeviation` immutable manifests that do not parse or execute historical configuration.
- The copied raw 8x16 fixture is byte-identical to historical `font.016`; historical sources remain read-only. No process, shell, PTY, host font/codepage/locale/keyboard mutation, X resource database, terminfo, external command, or new dependency entered scope.
- Physical host observation remains separate from deterministic in-process and remote-CI evidence. Unsupported capabilities use text-first fallbacks and never claim unavailable native behavior.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_06_A11Y_Framework.023-a11y-framework.md`.

### 023-a11y-framework
- Current implementation baseline: deliver the managed accessibility layer from `specs/023-a11y-framework/` and `requirements/intakes/archive/Lastenheft_06_A11Y_Framework.023-a11y-framework.md`.
- Scope is limited to opt-in `IAccessibleWidget` text, typed `cmFocusChanged` announcements, immutable shortcut queries for `TMenuBar`/`TStatusLine`, explicit `TColorScheme.HighContrast`, keyboard coverage inventory, the `A11yFramework` reference app, and DocFX/Axe evidence.
- Focus announcements reuse one existing broadcast and propagate descendant focus to the shell; non-migrated views remain compatible without fabricated labels.
- Keyboard acceptance requires behavioral proof or a concrete `N/A` for Tab, Shift+Tab, arrows, Enter, and direct shortcuts across every inventoried selectable control family.
- Native AT-SPI, NSAccessibility, UI Automation, speech, full-control migration, terminal-wide WCAG claims, Wave 1-4 remediation, and Feature 024 are out of scope.
- Historical Turbo Vision has no direct equivalent for the modern semantic A11Y contracts; relevant focus/menu/status sources are read-only intent context only.
- The former post-023 Wave-5 statement is historical; Feature 028 completed the TV203/Free Vision closure, Feature 029 is the sole immediate intake, and the separate mandatory Feature-030 magiblot evolution audit follows before any finding-derived hardening.

### 024-tv203-freevision-conformance-audit
- Current planning baseline: the original audit and merge remain complete, but consumer-review Revision 2 in `specs/024-tv203-freevision-conformance-audit/consumer-readiness-review.md` supersedes the zero-finding forward decision.
- Borland documentation and `tv203s/` remain primary; official Free Vision commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` is external secondary evidence only. `TVDEMOS/` and `TVFM/` are read-only consumer evidence.
- The inventory remains 151 historical `.cc` rows, 119 maintained production `.cs` files, 176 exported public types, 16 domains, and 48 contracts.
- Revision-2 decisions are 7 `Aligned`, 27 `IntentionalModernization`, 1 `ConsciouslyOmitted`, 8 `BehavioralDrift`, and 5 `EvidenceGap`.
- The 13 accepted findings route exactly nine items `F001`-`F009` to `Core025` and four items `F010`-`F013` to `ComponentData026`; both are closed, while Wave 5 and Wave 6 remain blocked through Feature 028, Feature 029, Feature 030, all combined finding-derived hardening, and the new independent closure.
- Revision 2 changes audit data, validation, evidence, requirements, and ordering only; it does not implement runtime behavior or port examples.

### 027-pre-wave5-conformance-closure
- Feature 027 remains valid historical closure evidence: feature merge `35414af` and causal closeout PR #67 merge `1da2b211e84221db87ab9f959b7b40d3ae2b01f0` are complete.
- Its original 13/34/1/0/0 decision set and zero-finding Wave-5 release statement are superseded for forward planning by Feature-024 Revision 2, not rewritten retroactively.
- Final evidence is in `specs/027-pre-wave5-conformance-closure/closure-evidence.md`; all 90 historical tasks are complete.

### 025-core-runtime-conformance-hardening
- Current implementation status: all nine `Core025` findings are implemented and proven; final evidence is in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`.
- `F001`-`F009` close concrete event kinds, focus and group state, idle lifecycle, desktop stack, modal/close, shared command state, real keyboard ingress, and bounded generic drag through real-path red/green proof.
- Feature-024 resolution metadata records exactly 13 non-documentation-only closures from Features 025 and 026 while preserving the Feature-028 and Wave-5/Wave-6 gates.
- Feature 028 completed the independent closure after Feature 026; its archived intake is `requirements/intakes/archive/Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.028-pre-wave5-wave6-conformance-closure.md`.

### 026-component-data-conformance-hardening
- Current implementation status: all four `ComponentData026` findings are implemented and proven; final evidence is in `specs/026-component-data-conformance-hardening/pr-evidence.md`.
- `F010`-`F013` close dialog completion/child validation, phase-aware input validation, mode-aware typed file outcomes, and allowlisted named UI-resource composition through real-path red/green proof.
- Feature-024 metadata now contains 139 maintained source files, 211 exported public types, and exactly 13 closed finding resolutions; Wave 5 and Wave 6 remain blocked through the combined Terminal.GUI/magiblot-derived closure after Features 029 and 030.

### 028-pre-wave5-wave6-conformance-closure
- Current implementation status: the evidence-only closure independently revalidated all 13 findings, seven real-path slices, and 13 protected consumer groups without a product, API, dependency, example, or protected-source change.
- The existing TV203/Free Vision gate is `ReadyForTerminalGuiAudit`; 12 consumer groups use the existing framework and destructive Wave-6 policy remains one bounded `FollowUpHardening`.
- Both waves remain `BlockedPendingTerminalGuiAudit`; Feature 029 is the sole immediate intake and Feature 030 is the mandatory successor before hardening or closure.

### 029-tv203-freevision-terminalgui-conformance-audit
- Current implementation status: the read-only audit is complete locally; final evidence is in `specs/029-tv203-freevision-terminalgui-conformance-audit/pr-evidence.md`.
- Turbo Vision 2.0.3 remains authoritative; pinned Free Vision remains secondary evidence, and Terminal.GUI v1.9.0 at commit `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3` is an additional modern C# implementation opinion.
- All `C001`-`C048`, 16 domains, 25 pinned sources, and 13 Wave consumer groups are covered. No `C049+`, `CandidateFinding`, or `ProductDecision` was admitted.
- The audit changes no runtime, API, dependency, example, historical, consumer, Free Vision, or Terminal.GUI source. It hands 48 `TGO###` observations to Feature 030 and creates no hardening or closure Lastenheft itself.
- Feature 030 completed the combined TG/MB audit locally; Feature 031 is the sole next intake after merge, and Wave 5 plus Wave 6 remain blocked through its independent closure.
- After every autonomous run, promote only reproducible provider-neutral preset learning through a Home-Baseline patch release and exact tag-ZIP adoption before the next run; record `NoPromotion` without an empty branch or PR. The consolidated pre-Wave-5 catalog update is submitted as `github/spec-kit#3569`; its external processing does not grant authority and does not indefinitely block the already installed v0.2.2 field run.

### 030-tv203-magiblot-evolution-audit
- Current implementation status: the read-only audit data and targeted validator are locally complete; final repository, documentation, coverage, remote review, and delivery gates remain.
- The exact magiblot pin is commit `57b6f56b38e0ee75240a80a10ee0e11470c24693`, tree `96dd03873955689ff0a79f6c8107a8148fe1ebd6`, with the multipart COPYRIGHT hash `66220bae...548`; its direct C++ lineage remains a modernization witness, never a normative source.
- Fifty selected source records support 48 relations: 27 `CorroboratesOriginal`, 12 `CorroboratesModernization`, 6 `AlternativeModernization`, and 3 `NotApplicable`. The 48 `MB*` decisions are 39 `AlreadySatisfiedWithNewEvidence`, 6 `IntentionalDeviation`, and 3 `RejectedComparison`.
- All 48 `TGO*` and 48 `MB*` observations have one combined disposition. No reproducible `CF*` finding or `ProductDecision` exists, so no hardening intake is generated; `requirements/intakes/archive/Lastenheft_16_Pre-Wave5-Wave6-Combined-Conformance-Closure.031-combined-conformance-closure.md` defines Feature 031 as the active closure intake.
- Runtime, API, dependency, example, consumer, historical, and external sources remain unchanged. Wave 5 and Wave 6 stay `BlockedPendingCombinedConformanceClosure`.
- `requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md` remains a binding deferred intake only after the complete Wave-6 closeout and receives its feature number then.

### 031-combined-conformance-closure
- Current implementation status: the evidence-only independent closure is complete; final evidence is in `specs/031-combined-conformance-closure/delivery-closeout.md`.
- Exactly 48 contracts, 13 consumers, 48 TGO and 48 MB observations, 96 dispositions, all accepted source pins and hashes, and the closed `F001`-`F013` set are revalidated.
- Product runtime, public APIs, dependencies, packages, projects, examples, consumers, historical sources, and external sources remain unchanged.
- Feature 031 made Wave 5 `Eligible`; Features 032 and 033 have since delivered both stages. Wave 6 remains only `ConditionallyReady` and still requires review of the actual combined Wave-5 delta.

### 032-wave5-tp7-functional-porting
- Current implementation status: the functional Wave-5 stage is delivered; all 15 read-only `TVDEMOS/*.PAS` sources have one role, all six consumers use the existing framework, and ten `Tp7*` projects provide normal plus controlled-smoke entry points.
- Primary proof covers calculator, central apps, resources, deterministic domain state, mouse capability/fallback, and exact 15/6/10/10 traceability through real app loops, state, view, and rendered cells.
- File, resource, help, generator, and mouse boundaries remain controlled; no host state, arbitrary user data, dependency, historical source, or broad framework behavior is changed.
- `requirements/intakes/archive/Lastenheft_18_Wave5-TP7-Showcase-Remediation.033-wave5-tp7-showcase-remediation.md` is the archived intake derived from the ten concrete Stage-2 deltas. Feature 033 is fully delivered through PR #96 and merge `d476e63`; Wave 6 remains blocked pending the actual combined Wave-5 delta review.

### 034-wave5-combined-delta-closure
- Current implementation status: Feature 034 is fully delivered through PR #99, final head `016692d`, merge `7fb52e2`, and its causal closeout.
- The read-only audit proves exact 15 source, 6 consumer, 10 example, 10 functional-proof, 10 showcase-closure, and 10 guide/launch cardinalities. All ten decisions are `AcceptedIntentionalDeviation`; there are zero open `Gap` dimensions, Candidate Findings, Product Decisions, owner groups, or hardening intakes.
- Product runtime, APIs, dependencies, projects, examples, framework code, `TVDEMOS/`, `TVFM/`, `tv203s/`, and external comparison sources remain unchanged. The final 22-check matrix and 13 exact-head gates passed; checkout-neutral SHA-256 and Git-blob proof passed on Ubuntu, macOS, and Windows.
- Wave 5 is `Closed`. Feature 035 is delivered through PR #101, final head `207e807`, and merge `52f77fa`; it provides Wave-6 Stage 1.

### 036-wave6-tvfm-showcase-remediation
- Feature 036 is fully delivered through PR #104, final head `a0d5062`, and merge `559bffb`.
- The exact closure contains ten `W6S` showcase decisions, one `ShowcaseComplete` entry-point decision, and 24 validated read-only `TVFM/` source hashes. The Windows provider correction makes historical `.PAS`/`.BAT` text hashes checkout-neutral while preserving byte-exact resource hashes.
- Feature-035 controlled-root, path, search, preview, viewer, one-shot intent, mutation, dependency, process, shell, host, and arbitrary-user-data boundaries remain unchanged. `TVFM/`, `TVDEMOS/`, and `tv203s/` stay read-only.
- The independent Wave-6 closure is now the preferred active intake at `requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md`; it reserves Feature 037 without starting it. The post-Wave-6 portfolio audit remains blocked by that closure.

### Autonomous Red-Proof Completeness
- Before the first red test batch, review imports, public XML docs, harness helpers, focus/ownership assertions, and linked-source assembly identity.
- Group independent negative cases only as a bounded project-local red matrix with explicit failure boundaries and shared ownership.
- When source is linked into multiple assemblies, cross-project proof uses public contracts or state delegates and does not assume one CLR type identity.
- Keep a causal closeout evidence-only and single-commit-capable: do not require its own PR URL, reviewed-head result, or merge commit inside that same repository file; verify terminal facts externally without recursive closeout.
- When push and pull-request events create equivalent workflow sets, use pull-request-context checks as the gate and record duplicate runs as noise; cancel them only under an explicit safe workflow/concurrency contract.

## Active Technologies

Einstiege / Entry files: `.github/agents/copilot-instructions.md`

- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation (008-controls-revision)
- In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage (008-controls-revision)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream contex (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell, dialog, file, color, history, and widget types (`TDialog`, `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, `THistory`, `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, `TColorDisplay`, `TComboBox`, `TProgressBar`, `TParamText`); existing `TuiVision.Serialization` archive/resource foundation (`TRecordRegistry`, `TRecordSerializer`, `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TResourceFile`, `TResourceCollection`, `pstream` family); MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (010-standard-dialogs-designer)
- In-memory dialog state and session-only history; real local file-system metadata for file-listing/validation only; source-controlled tests/proof artifacts; minimal persisted dialog-description fixture through existing serialization/resource primitives; no database or external service storage (010-standard-dialogs-designer)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing framework modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; new wave-2 example projects under `examples/`; existing `tests/TuiVision.Examples.SmokeTests/`; MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (011-port-wave2-examples)
- Runtime example state is in memory; standard-dialog file flows use real local file-system metadata only; `dlgdsn` may use source-controlled dialog-description fixtures through existing Serialization/resource primitives; no database, external service, persisted user history, or new dependency planned (011-port-wave2-examples)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet test stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (012-interactive-wave2-demos)
- Runtime example state is in memory. Dialog-designer and file/path demonstrations use source-controlled fixtures, fixed repository paths, or test temporary directories. The examples must not persist user history, write user data as part of normal demonstration, read arbitrary user file contents as proof, or add a database/external service. (012-interactive-wave2-demos)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only; shared example support in `examples/Shared/Wave2Runtime.cs`; existing MSTest/Coverlet, DocFX, and Playwright/axe tooling. No new runtime NuGet dependency was added. (013-wave2-visual-component-remediation)
- Runtime example state remains in memory. Controlled examples may use source-controlled fixtures, fixed repository paths, or test temporary directories for metadata, rendering, validation, or rejection proof. The feature must not add a database, external service, network dependency, persistent user history, or arbitrary user-file content reads. (013-wave2-visual-component-remediation)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency was added. (014-wave1-functional-hardening implemented)
- Runtime example state remains in memory. Proof data is limited to existing source-controlled files, controlled example fixtures if needed, or test temporary directories. No database, external service, network dependency, persistent user history, arbitrary user-file content reads, or runtime/product AI storage was added. (014-wave1-functional-hardening implemented)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest/Coverlet validation; existing DocFX plus Playwright/axe web A11Y tooling when documentation triggers apply. No new runtime NuGet dependency is planned. (015-didactic-comment-hardening)
- Source-controlled Markdown evidence and guidance files only. Production code state and tests keep their current storage model. No database, external service, network dependency, persistent user history, runtime/product AI storage, or arbitrary user-file proof path is planned. (015-didactic-comment-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`); Bash and PowerShell 7 for repository tooling + Existing TuiVision projects, MSTest, Coverlet, DocFX, Playwright/axe, GitHub Actions, Gitleaks, and CycloneDX for .NET 6.2.0 as a repository-local tool. No new runtime package is planned. (016-secure-development-hardening)
- Source-controlled Markdown, YAML, shell/PowerShell scripts, a local .NET tool manifest, and test fixtures. Generated evidence is written to temporary or ignored directories. No database, service, credential, runtime AI, or user-data store is introduced. (016-secure-development-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing TuiVision modules, shared Wave-1 example composition, MSTest/Coverlet, DocFX, and Playwright/axe; no new package (017-wave1-visual-component-remediation)
- Runtime example state remains in-process and session-only; proof and governance use source-controlled Markdown, with no database, external service, persistent user history, arbitrary user-file proof, or runtime/product AI (017-wave1-visual-component-remediation)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing Controls/Serialization modules, MSTest/Coverlet, DocFX, and Playwright/axe; no new package (018-editor-help-resources-hardening)
- Runtime state is bounded to managed editor/help/resource models and deterministic temporary test files; no example port, database, external service, ambient locale dependency, arbitrary user-file proof, or runtime/product AI (018-editor-help-resources-hardening)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, and `TuiVision.Drivers.Console`; no new packages (019-wave3-visual-component-porting)
- Embedded/source-controlled learning content and test-owned temporary files only (019-wave3-visual-component-porting)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Drivers.Console`, `TuiVision.Controls`, and reviewed `TuiVision.Compatibility` key translation; no new packages (021-terminal-charset-hardening)
- In-memory session/history state plus source-controlled JSON and raw 8x16 fixtures (021-terminal-charset-hardening)
- C# / .NET 10 for durable test-only validation; Markdown and JSON for evidence + existing framework assemblies, MSTest, `System.Text.Json`, Git, official pinned FPC source checkout (024-tv203-freevision-conformance-audit)
- repository-owned JSON and Markdown evidence; external Free Vision worktree under `/tmp`, never tracked (024-tv203-freevision-conformance-audit)
- C# / .NET 10 + existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, MSTest; no new package (025-core-runtime-conformance-hardening)
- in-process state only; audit JSON and Markdown evidence are repository-owned documentation (025-core-runtime-conformance-hardening)
- C# with latest language version on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Serialization`; no new package or runtime dependency (026-component-data-conformance-hardening)
- Existing bounded binary archive and `TResourceFile`; controlled temporary filesystem metadata for file-dialog proofs (026-component-data-conformance-hardening)
- C# / .NET 10 for test-only validation; JSON, Markdown, YAML, Bash and PowerShell evidence tooling + existing MSTest 4.0.1, Coverlet 6.0.4, DocFX, Playwright/Axe, Lynx, jq, xmllint, Git and GitHub Actions (028-pre-wave5-wave6-conformance-closure)
- repository-owned JSON and Markdown only; historical, consumer, and Free Vision sources remain read-only (028-pre-wave5-wave6-conformance-closure)
- C# / .NET 10 for test-only validation; JSON and Markdown for audit evidence + existing MSTest 4.0.1, System.Text.Json, Coverlet 6.0.4, DocFX, Playwright/Axe, Lynx, Git and GitHub Actions (029-tv203-freevision-terminalgui-conformance-audit)
- repository-owned Feature-029 JSON and Markdown; external Terminal.GUI checkout remains untracked under a temporary directory (029-tv203-freevision-terminalgui-conformance-audit)
- C# 14 / .NET 10 for test-only validation; JSON and Markdown for evidence + Existing BCL `System.Text.Json`, MSTest 4.0.1, existing repository scripts and workflows; no new package (031-combined-conformance-closure)
- Source-controlled closed JSON and Markdown evidence; temporary external checkouts under `/tmp` only (031-combined-conformance-closure)
- C# 14 / .NET 10 + Existing TuiVision.Core, TuiVision.Controls, TuiVision.Serialization, TuiVision.Compatibility, TuiVision.Drivers.Console, MSTest 4.0.1 and BCL System.Text.Json; no new package (032-wave5-tp7-functional-porting)
- Embedded UTF-8 fixtures, controlled test-temporary files, existing THelpFile and TResourceFile; no database or service (032-wave5-tp7-functional-porting)
- C# 14 / .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, DocFX and Playwright/Axe; no new package (035-wave6-tvfm-functional-porting)
- Source-controlled UTF-8/binary fixtures copied into test-owned temporary roots; controlled local filesystem only; no arbitrary user data, shell/external viewer, database or service (035-wave6-tvfm-functional-porting)
- C# 14 / .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest 4.0.1 and .NET BCL; no new package (036-wave6-tvfm-showcase-remediation)
- Existing source-controlled fixtures copied into process- or test-owned temporary controlled roots; no database, service, host profile, shell, process, PTY, network, or arbitrary-user storage (036-wave6-tvfm-showcase-remediation)

- C# `latest` on .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet; docfx; wave-1 example projects under `examples/` (007-port-wave1-examples — Wave 1 delivered)

## Recent Changes

Einstiege / Entry files: `.github/agents/copilot-instructions.md`

- 036-wave6-tvfm-showcase-remediation: Added C# 14 / .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest 4.0.1 and .NET BCL; no new package
- 035-wave6-tvfm-functional-porting: Delivered Wave-6 functional stage through PR #101; `Tp7FileManager` now has controlled-root file proofs, bounded preview/search, explicit one-shot mutation intents, keyboard parity and app-loop smokes. Stage-2 visible polish remains separate.

## 016 Secure Development Delivery Context

Einstiege / Entry files: `.github/agents/copilot-instructions.md`

- Final evidence is in `specs/016-secure-development-hardening/pr-evidence.md`; the durable matrix records all 157 controls with complete status, ownership, risk, evidence, and re-evaluation fields.
- Remediation is bounded to malformed persistence rejection, immutable workflow dependencies, supply-chain automation, root disclosure guidance, and Bash/PowerShell archive-script parity.
- Local acceptance is 498/498 Release tests and coverage above 70% for all five required assemblies; DocFX/axe and remote OS/CI proof remain delivery gates.
- Human legal, provider, organization, and agent-platform decisions remain `Open`; release provenance, reproducible-build/lock maturity, and RFC 9116 remain named follow-ups.
- The next open prioritized intake is `requirements/intakes/archive/Lastenheft_03_EditorHelpAndResourcesHardening.018-editor-help-resources-hardening.md` before Wave-3 visual porting.

## 017 Wave-1 Visual Component Remediation Delivery Context

Einstiege / Entry files: `.github/agents/copilot-instructions.md`

- The implementation is complete; final evidence is in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- `Desklogo`, `MsgCls`, all 16 Tutorial tokens, and `Videomode` use a visible main state, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` with concrete state, view-tree identity, and rendered buffer/cell evidence. The matrix contains four app rows and 16 unique Tutorial rows.
- `examples/Shared/Wave1Runtime.cs` composes existing controls. Desklogo and MsgCls use `UseExistingFramework`; Tutorial and Videomode use bounded `IntentionalDeviation` decisions.
- Historical `tv203s/` sources remain read-only. No cross-wave behavior, broad framework redesign, new dependency, persistence, external service, or runtime/product AI entered scope.
- The complete example-smoke suite passes 101/101 locally; repository, coverage, DocFX, A11Y, and remote checks remain delivery gates until recorded.
- Feature 018 closes the editor/help/resources intake; the next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave3-Visual-Component-Porting.019-wave3-visual-component-porting.md`.

## 018 Editor, Help, and Resources Hardening Delivery Context

Einstiege / Entry files: `.github/agents/copilot-instructions.md`

- Final evidence is in `specs/018-editor-help-resources-hardening/pr-evidence.md`.
- Existing editor/file and runtime-help controls are retained and proven through coherent application paths.
- `THelpSourceCompiler` supports bounded `.topic` source, strict UTF-8, deterministic symbols, forward references, stable diagnostics, and atomic failure.
- `TLocalizedResourceLookup` uses exact language, caller-ordered fallbacks, and neutral exact keys without ambient locale or gettext dependencies.
- Resource and Help deserialization reject duplicate/negative structures and invalid reference graphs before publication.
- Historical `tv203s/` sources remain read-only; Wave-3 examples, mouse, terminal/charset, broad redesign, and dependencies remain out of scope.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave3-Visual-Component-Porting.019-wave3-visual-component-porting.md`.

### 019-wave3-visual-component-porting
- Current implementation status: Wave-3 visual component porting is implemented locally; final evidence is in `specs/019-wave3-visual-component-porting/pr-evidence.md`.
- `BHelp`, `HelpDemo`, `I18n`, `TvEdit`, and `TvHc` use visible main components, a real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, rendered buffer/cell evidence, status, and description. The Wave-3 matrix passes 14/14 locally, including five constrained `48x16` layouts.
- Framework decisions are `UseExistingFramework` for TvEdit, HelpDemo, I18n, and TvHc, and bounded `IntentionalDeviation` for BHelp because the proprietary unchecked Borland `.tch` decoder is omitted.
- Embedded/source-controlled learning content and test-owned temporary paths are the only data boundaries. Historical sources remain read-only.
- Mouse interaction, terminal/charset/font work, Wave 4, broad redesign, services, new dependencies, and runtime/product AI remain outside 019.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_04_MouseSupportAndInteraction.020-mouse-support-interaction.md`.

### 020-mouse-support-interaction
- Current implementation status: bounded mouse support and interaction hardening is implemented locally; final evidence is in `specs/020-mouse-support-interaction/pr-evidence.md`.
- `ConsoleMouseIngress` accepts only complete bounded SGR 1006 left press, pressed move, and release reports and publishes zero or one existing `TEvent`; malformed syntax, range, button, capability, and phase input is rejected atomically.
- `TGroup` routes mouse down to one topmost visible target, transfers focus only to selectable targets, and preserves existing exactly-once control commands. Nested mouse coordinates traverse the full owner chain.
- The only mouse drag contract is moving a `TWindow` from its title row. Owner bounds, release, Escape, capability loss, disable, removal, shutdown, and the existing `Ctrl+F5` keyboard fallback are proven.
- Interactive macOS/Linux terminals and WSL use the SGR capability contract; native Windows Console and redirected/headless I/O remain honest `Unsupported` boundaries. Wheel, hover, touch, extra buttons, full protocol parity, and additional drag targets remain out of scope.
- Primary proof runs through `TProgram.GetEvent` and `app.Run()` and combines concrete focus/command/drag state, target identity, visible text, and rendered buffer/cell assertions. Historical sources remain read-only.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_05_TerminalCharsetAndEmulation.021-terminal-charset-hardening.md`.

### 021-terminal-charset-hardening
- Current implementation status: terminal and charset hardening is implemented locally; final evidence is in `specs/021-terminal-charset-hardening/pr-evidence.md`.
- `TerminalSession` provides a bounded in-process transcript, cursor, 16-color attributes, 4,096-cell FIFO history, deterministic resize/reset/lifecycle, and atomic C0/CSI subset recovery without a process, shell, or PTY.
- `TerminalCharsetMapper` uses Unicode plus one fixed KOI8-R table and U+FFFD replacement. `BitmapFontFixture` validates only raw 8x16/256/4,096-byte metadata; no host codec, font installation, or historical generator is used.
- `TerminalProfile` uses a closed `System.Text.Json` schema. Invalid schema is rejected as a whole; unavailable font/host capability uses an observable safe default and `Unsupported` status.
- `TTerminalView` reuses the Driver-owned session and existing Controls app loop. Primary proof combines session/profile state, concrete view identity, text status, cursor, rendered cells, controlled keyboard input, and deterministic quit.
- Historical terminal, Cyrillic, font, Eterm, and XTerm sources remain read-only intent. Full ANSI/VT/XTerm parity, visible Wave-4 examples, host mutation, new dependencies, services, persistence, and runtime/product AI remain outside 021.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_Wave4-Visual-Component-Porting.022-wave4-visual-component-porting.md`.

### 022-wave4-visual-component-porting
- Current implementation status: Wave-4 visual component porting is implemented locally; final evidence is in `specs/022-wave4-visual-component-porting/pr-evidence.md`.
- `Terminal`, `Cyrillic`, `Fonts`, `ETerm`, and `XTerm` provide a visible main component, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, exact view identity, rendered buffer/cell evidence, controlled operation, fallback, description, and deterministic host classification. Narrow viewports preserve example identity and status meaning.
- Terminal, Cyrillic, and Fonts are `UseExistingFramework`; ETerm and XTerm are bounded `IntentionalDeviation` immutable manifests that do not parse or execute historical configuration.
- The copied raw 8x16 fixture is byte-identical to historical `font.016`; historical sources remain read-only. No process, shell, PTY, host font/codepage/locale/keyboard mutation, X resource database, terminfo, external command, or new dependency entered scope.
- Physical host observation remains separate from deterministic in-process and remote-CI evidence. Unsupported capabilities use text-first fallbacks and never claim unavailable native behavior.
- The next prioritized intake is `requirements/intakes/archive/Lastenheft_06_A11Y_Framework.023-a11y-framework.md`.

### 023-a11y-framework
- Current implementation baseline: deliver the managed accessibility layer from `specs/023-a11y-framework/` and `requirements/intakes/archive/Lastenheft_06_A11Y_Framework.023-a11y-framework.md`.
- Scope is limited to opt-in `IAccessibleWidget` text, typed `cmFocusChanged` announcements, immutable shortcut queries for `TMenuBar`/`TStatusLine`, explicit `TColorScheme.HighContrast`, keyboard coverage inventory, the `A11yFramework` reference app, and DocFX/Axe evidence.
- Focus announcements reuse one existing broadcast and propagate descendant focus to the shell; non-migrated views remain compatible without fabricated labels.
- Keyboard acceptance requires behavioral proof or a concrete `N/A` for Tab, Shift+Tab, arrows, Enter, and direct shortcuts across every inventoried selectable control family.
- Native AT-SPI, NSAccessibility, UI Automation, speech, full-control migration, terminal-wide WCAG claims, Wave 1-4 remediation, and Feature 024 are out of scope.
- Historical Turbo Vision has no direct equivalent for the modern semantic A11Y contracts; relevant focus/menu/status sources are read-only intent context only.
- The former post-023 Wave-5 statement is historical; Feature 028 completed the TV203/Free Vision closure, Feature 029 is the sole immediate intake, and the separate mandatory Feature-030 magiblot evolution audit follows before any finding-derived hardening.

### 024-tv203-freevision-conformance-audit
- Current planning baseline: the original audit and merge remain complete, but consumer-review Revision 2 in `specs/024-tv203-freevision-conformance-audit/consumer-readiness-review.md` supersedes the zero-finding forward decision.
- Borland documentation and `tv203s/` remain primary; official Free Vision commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` is external secondary evidence only. `TVDEMOS/` and `TVFM/` are read-only consumer evidence.
- The inventory remains 151 historical `.cc` rows, 119 maintained production `.cs` files, 176 exported public types, 16 domains, and 48 contracts.
- Revision-2 decisions are 7 `Aligned`, 27 `IntentionalModernization`, 1 `ConsciouslyOmitted`, 8 `BehavioralDrift`, and 5 `EvidenceGap`.
- The 13 accepted findings route exactly nine items `F001`-`F009` to `Core025` and four items `F010`-`F013` to `ComponentData026`; both are closed, while Wave 5 and Wave 6 remain blocked through Feature 028, Feature 029, Feature 030, all combined finding-derived hardening, and the new independent closure.
- Revision 2 changes audit data, validation, evidence, requirements, and ordering only; it does not implement runtime behavior or port examples.

### 027-pre-wave5-conformance-closure
- Feature 027 remains valid historical closure evidence: feature merge `35414af` and causal closeout PR #67 merge `1da2b211e84221db87ab9f959b7b40d3ae2b01f0` are complete.
- Its original 13/34/1/0/0 decision set and zero-finding Wave-5 release statement are superseded for forward planning by Feature-024 Revision 2, not rewritten retroactively.
- Final evidence is in `specs/027-pre-wave5-conformance-closure/closure-evidence.md`; all 90 historical tasks are complete.

### 025-core-runtime-conformance-hardening
- Current implementation status: all nine `Core025` findings are implemented and proven; final evidence is in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`.
- `F001`-`F009` close concrete event kinds, focus and group state, idle lifecycle, desktop stack, modal/close, shared command state, real keyboard ingress, and bounded generic drag through real-path red/green proof.
- Feature-024 resolution metadata records exactly 13 non-documentation-only closures from Features 025 and 026 while preserving the Feature-028 and Wave-5/Wave-6 gates.
- Feature 028 completed the independent closure after Feature 026; its archived intake is `requirements/intakes/archive/Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.028-pre-wave5-wave6-conformance-closure.md`.

### 026-component-data-conformance-hardening
- Current implementation status: all four `ComponentData026` findings are implemented and proven; final evidence is in `specs/026-component-data-conformance-hardening/pr-evidence.md`.
- `F010`-`F013` close dialog completion/child validation, phase-aware input validation, mode-aware typed file outcomes, and allowlisted named UI-resource composition through real-path red/green proof.
- Feature-024 metadata now contains 139 maintained source files, 211 exported public types, and exactly 13 closed finding resolutions; Wave 5 and Wave 6 remain blocked through the combined Terminal.GUI/magiblot-derived closure after Features 029 and 030.

### 028-pre-wave5-wave6-conformance-closure
- Current implementation status: the evidence-only closure independently revalidated all 13 findings, seven real-path slices, and 13 protected consumer groups without a product, API, dependency, example, or protected-source change.
- The existing TV203/Free Vision gate is `ReadyForTerminalGuiAudit`; 12 consumer groups use the existing framework and destructive Wave-6 policy remains one bounded `FollowUpHardening`.
- Both waves remain `BlockedPendingTerminalGuiAudit`; Feature 029 is the sole immediate intake and Feature 030 is the mandatory successor before hardening or closure.

### 029-tv203-freevision-terminalgui-conformance-audit
- Current implementation status: the read-only audit is complete locally; final evidence is in `specs/029-tv203-freevision-terminalgui-conformance-audit/pr-evidence.md`.
- Turbo Vision 2.0.3 remains authoritative; pinned Free Vision remains secondary evidence, and Terminal.GUI v1.9.0 at commit `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3` is an additional modern C# implementation opinion.
- All `C001`-`C048`, 16 domains, 25 pinned sources, and 13 Wave consumer groups are covered. No `C049+`, `CandidateFinding`, or `ProductDecision` was admitted.
- The audit changes no runtime, API, dependency, example, historical, consumer, Free Vision, or Terminal.GUI source. It hands 48 `TGO###` observations to Feature 030 and creates no hardening or closure Lastenheft itself.
- Feature 030 completed the combined TG/MB audit locally; Feature 031 is the sole next intake after merge, and Wave 5 plus Wave 6 remain blocked through its independent closure.
- After every autonomous run, promote only reproducible provider-neutral preset learning through a Home-Baseline patch release and exact tag-ZIP adoption before the next run; record `NoPromotion` without an empty branch or PR. Open one consolidated upstream preset issue only immediately before Wave 5.

### 030-tv203-magiblot-evolution-audit
- Current implementation status: the read-only audit data and targeted validator are locally complete; final repository, documentation, coverage, remote review, and delivery gates remain.
- The exact magiblot pin is commit `57b6f56b38e0ee75240a80a10ee0e11470c24693`, tree `96dd03873955689ff0a79f6c8107a8148fe1ebd6`, with the multipart COPYRIGHT hash `66220bae...548`; its direct C++ lineage remains a modernization witness, never a normative source.
- Fifty selected source records support 48 relations: 27 `CorroboratesOriginal`, 12 `CorroboratesModernization`, 6 `AlternativeModernization`, and 3 `NotApplicable`. The 48 `MB*` decisions are 39 `AlreadySatisfiedWithNewEvidence`, 6 `IntentionalDeviation`, and 3 `RejectedComparison`.
- All 48 `TGO*` and 48 `MB*` observations have one combined disposition. No reproducible `CF*` finding or `ProductDecision` exists, so no hardening intake is generated; `requirements/intakes/archive/Lastenheft_16_Pre-Wave5-Wave6-Combined-Conformance-Closure.031-combined-conformance-closure.md` defines Feature 031 as the active closure intake.
- Runtime, API, dependency, example, consumer, historical, and external sources remain unchanged. Wave 5 and Wave 6 stay `BlockedPendingCombinedConformanceClosure`.
- `requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md` remains a binding deferred intake only after the complete Wave-6 closeout and receives its feature number then.

### 031-combined-conformance-closure
- Current implementation status: the evidence-only independent closure is complete; final evidence is in `specs/031-combined-conformance-closure/delivery-closeout.md`.
- Exactly 48 contracts, 13 consumers, 48 TGO and 48 MB observations, 96 dispositions, all accepted source pins and hashes, and the closed `F001`-`F013` set are revalidated.
- Product runtime, public APIs, dependencies, packages, projects, examples, consumers, historical sources, and external sources remain unchanged.
- Feature 031 made Wave 5 `Eligible`; Features 032 and 033 have since delivered both stages. Wave 6 remains only `ConditionallyReady` and still requires review of the actual combined Wave-5 delta.

### 032-wave5-tp7-functional-porting
- Current implementation status: the functional Wave-5 stage is delivered; all 15 read-only `TVDEMOS/*.PAS` sources have one role, all six consumers use the existing framework, and ten `Tp7*` projects provide normal plus controlled-smoke entry points.
- Primary proof covers calculator, central apps, resources, deterministic domain state, mouse capability/fallback, and exact 15/6/10/10 traceability through real app loops, state, view, and rendered cells.
- File, resource, help, generator, and mouse boundaries remain controlled; no host state, arbitrary user data, dependency, historical source, or broad framework behavior is changed.
- `requirements/intakes/archive/Lastenheft_18_Wave5-TP7-Showcase-Remediation.033-wave5-tp7-showcase-remediation.md` is the archived intake derived from the ten concrete Stage-2 deltas. Feature 033 is fully delivered through PR #96 and merge `d476e63`; Wave 6 remains blocked pending the actual combined Wave-5 delta review.

### 034-wave5-combined-delta-closure
- Current implementation status: Feature 034 is fully delivered through PR #99, final head `016692d`, merge `7fb52e2`, and its causal closeout.
- The read-only audit proves exact 15 source, 6 consumer, 10 example, 10 functional-proof, 10 showcase-closure, and 10 guide/launch cardinalities. All ten decisions are `AcceptedIntentionalDeviation`; there are zero open `Gap` dimensions, Candidate Findings, Product Decisions, owner groups, or hardening intakes.
- Product runtime, APIs, dependencies, projects, examples, framework code, `TVDEMOS/`, `TVFM/`, `tv203s/`, and external comparison sources remain unchanged. The final 22-check matrix and 13 exact-head gates passed; checkout-neutral SHA-256 and Git-blob proof passed on Ubuntu, macOS, and Windows.
- Wave 5 is `Closed`. Feature 035 is delivered through PR #101, final head `207e807`, and merge `52f77fa`; it provides Wave-6 Stage 1.

### 036-wave6-tvfm-showcase-remediation
- Feature 036 is fully delivered through PR #104, final head `a0d5062`, and merge `559bffb`.
- The exact closure contains ten `W6S` showcase decisions, one `ShowcaseComplete` entry-point decision, and 24 validated read-only `TVFM/` source hashes. The Windows provider correction makes historical `.PAS`/`.BAT` text hashes checkout-neutral while preserving byte-exact resource hashes.
- Feature-035 controlled-root, path, search, preview, viewer, one-shot intent, mutation, dependency, process, shell, host, and arbitrary-user-data boundaries remain unchanged. `TVFM/`, `TVDEMOS/`, and `tv203s/` stay read-only.
- The independent Wave-6 closure is now the preferred active intake at `requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md`; it reserves Feature 037 without starting it. The post-Wave-6 portfolio audit remains blocked by that closure.

### Autonomous Red-Proof Completeness
- Before the first red test batch, review imports, public XML docs, harness helpers, focus/ownership assertions, and linked-source assembly identity.
- Group independent negative cases only as a bounded project-local red matrix with explicit failure boundaries and shared ownership.
- When source is linked into multiple assemblies, cross-project proof uses public contracts or state delegates and does not assume one CLR type identity.
- Keep a causal closeout evidence-only and single-commit-capable: do not require its own PR URL, reviewed-head result, or merge commit inside that same repository file; verify terminal facts externally without recursive closeout.
- When push and pull-request events create equivalent workflow sets, use pull-request-context checks as the gate and record duplicate runs as noise; cancel them only under an explicit safe workflow/concurrency contract.

  Groessere verpflichtende Beispielwellen sollen als zweistufiges Spec-Kit-
  Liefermuster geplant werden, wenn funktionale Portierung und interaktive
  Runtime-Politur sonst vermischt wuerden. Stufe 1 portiert das Verhalten,
  schliesst Framework-Voraussetzungen, liefert deterministische Headless- oder
  In-Process-Smoke-Pfade und markiert bewusst verschobene Interaktion als
  Follow-up. Stufe 2 haengt dieselben Funktionen an sichtbare Menues,
  Statuszeilen, Desktop-Controls, Dialoge, Tastaturpfade und skriptbare
  UI-Event-Smoke-Tests. Eine Beispielwelle ist erst nach Stufe 2 voll
  lern- und reviewtauglich, sofern der Scope nicht ausdruecklich nur einen
  minimalen nicht-interaktiven Nachweis verlangt.
  Larger mandatory example waves should use a two-stage Spec-Kit delivery
  pattern when functional porting and interactive runtime polish would
  otherwise be mixed. Stage 1 ports behavior, closes framework prerequisites,
  provides deterministic headless or in-process smoke paths, and marks deferred
  interactivity as follow-up. Stage 2 wires the same proven functions into
  visible menus, status lines, desktop controls, dialogs, keyboard paths, and
  scripted UI-event smoke tests. A wave is fully learner- and review-ready only
  after Stage 2 unless the scope explicitly asks for a minimal non-interactive
  proof.
  Kuenftige Lastenhefte fuer Beispielwellen oder beispielnahe Framework-
  Vorhaertungen muessen ein Framework-Usage- und Remediation-Gate enthalten.
  Der spaetere Spec-Kit-Lauf muss pro Beispiel oder Vertragsbereich
  dokumentieren, welche bestehende TuiVision-Framework-Komponente genutzt wird,
  ob lokale Sonderlogik existiert und welche Entscheidung gilt:
  `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder
  `FollowUpHardening`. Wiederverwendbare Logik darf nicht dauerhaft als lokale
  `examples/`-Sonderloesung verbleiben; wiederholte oder Framework-Verhalten
  ersetzende Logik gehoert in einen kleinen Framework-Fix oder in ein eigenes
  Follow-up-Hardening.
  Future Lastenhefte for example waves or example-adjacent framework hardening
  must include a framework-usage and remediation gate. The later Spec-Kit run
  must document, per example or contract area, which existing TuiVision
  framework component is used, whether local special logic exists, and which
  decision applies: `UseExistingFramework`, `SmallFrameworkFix`,
  `IntentionalDeviation`, or `FollowUpHardening`. Reusable logic must not
  remain permanently as a local `examples/` special solution; move repeated or
  framework-replacing logic into a small framework fix or a dedicated follow-up
  hardening item.

  <!-- source-reference-policy:begin -->
  Drei-Achsen-Quellenpolicy / Three-Axis Source Reference Policy:
  Aktuelle TuiVision-Vertraege sind die Produktnorm. `magiblot/tvision` wird
  zuerst als moderne, nicht normative Designreferenz am Commit
  `57b6f56b38e0ee75240a80a10ee0e11470c24693` und Tree
  `96dd03873955689ff0a79f6c8107a8148fe1ebd6` geprueft; Borland und `tv203s/`
  bestimmen historische Absicht. Die Abschlussentscheidung lautet genau
  `AdoptModernization`, `PreserveHistoricalIntent`,
  `IntentionalTuiVisionDeviation` oder `N/A`. Quellenrang allein loest keinen
  Konflikt. Externe Checkouts bleiben untracked, Quellkopie ist ausgeschlossen
  und der Lizenzstatus lautet `MultipartNotRepositoryWideMIT`. Die Policy gilt
  `Prospective`; nur geaenderte TuiVision-Vertraege, ein neuer freigegebener Pin
  oder materiell neue Consumer-Evidence loesen Re-Evaluation aus. Bewegliche
  Branches sind keine Evidence.
  Current contracts are normative; the pinned Magiblot revision is reviewed
  first for modern design without replacing historical or consumer evidence.
  <!-- source-reference-policy:end -->

  Fuer jede Spec-Kit-Feature-Implementierung mit historisch abgeleitetem
  Turbo-Vision-Verhalten muessen die relevanten Implementierungsdateien unter
  `tv203s/` (`.c`, `.cc`) als Read-only-Referenz geprueft werden. Wenn
  Deklarationen, Konstanten, Makros, Datenlayout, Vererbung oder Signaturen
  wichtig sind, muessen auch passende C/C++-Header (`.h`, `.hpp`, `.hh`)
  einbezogen werden. Wesentliche nutzer- oder API-sichtbare Abweichungen werden
  in Spec, Plan, Tasks, Guide, PR-Evidence oder Architektur-/Security-Nachweis
  dokumentiert; ohne historischen Bezug genuegt ein kurzes `N/A`.
  For every Spec-Kit feature implementation with historically derived Turbo
  Vision behavior, review the relevant implementation files under `tv203s/`
  (`.c`, `.cc`) as read-only reference. When declarations, constants, macros,
  data layout, inheritance, or signatures matter, also include matching C/C++
  headers (`.h`, `.hpp`, `.hh`). Document material user-visible or API-visible
  deviations in spec, plan, tasks, guide, PR evidence, or architecture/security
  evidence; if there is no historical relevance, a short `N/A` is sufficient.
  Schnittstellen, Qualitätsattribute, Laufzeitverhalten, Deployment,
  Wartbarkeit oder technische Schulden betrifft, muss `spec.md`, `plan.md`
  oder `tasks.md` festhalten, ob Evidenz unter `docs/architecture/`
  erforderlich ist. `N/A` braucht eine kurze Begruendung.
  quality attributes, runtime behavior, deployment, maintainability, or
  technical debt, `spec.md`, `plan.md`, or `tasks.md` must state whether
  evidence under `docs/architecture/` is required. `N/A` needs a short
  rationale.
  TuiVision-Coverage-Gate-Konfiguration. Coverage-Gates muessen aus dem
  Repository-Root mit `dotnet test --collect:"XPlat Code Coverage" --settings
  coverlet.runsettings` gemessen werden; die Datei muss bei Aenderungen an
  gate-relevanten Assemblies, Beispiel-Assemblies oder Testprojekten gepflegt
  und nach Moeglichkeit mit `xmllint --noout coverlet.runsettings` validiert
  werden.
  configuration. Coverage gates must be measured from the repository root with
  `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`;
  keep the file in sync when gate-relevant assemblies, example assemblies, or
  test projects change, and validate it with `xmllint --noout
  coverlet.runsettings` where available.
