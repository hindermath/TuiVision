# Requirements Quality Checklist: Security and A11Y

**Purpose**: Verify controlled boundaries, learner-facing accessibility, and
governance applicability before implementation.

- [x] CHK001 File and generator paths are limited to source-controlled fixtures or test-owned roots.
- [x] CHK002 Resource keys, allowlisted types, Help compilation, and no-partial-model rejection remain explicit.
- [x] CHK003 Mouse capability, keyboard parity, capability loss, and zero host mutation are measurable.
- [x] CHK004 No dependency, service, process, shell, PTY, or arbitrary user file can enter scope.
- [x] CHK005 Keyboard reachability is required for every core action.
- [x] CHK006 Focus, selection, status, rejection, and fallback are text-first rather than color- or pointer-only.
- [x] CHK007 German-first/English-second CEFR-B2 Description and guide requirements are explicit.
- [x] CHK008 Constrained layouts require visible mandatory text and no incoherent overlap.
- [x] CHK009 XML/public API changes trigger DocFX and Playwright/Axe.
- [x] CHK010 All seven installed governance presets have applicability decisions and re-evaluation boundaries.
