# Field Validation Summary

Version `0.2.3` is validated with:

- a synthetic three-target, one-root, two-edge series;
- an explicit zero-target `Idle` series;
- negative fixtures for `Idle` with content and non-idle without targets;
- a nested Git repository whose canonical index is excluded from the parent
  uniqueness check, plus a blocking ordinary duplicate-directory fixture;
- malformed path, type, binding, order, root, lifecycle, and hash fixtures;
- Bash and PowerShell wrapper parity over one portable validation core;
- the Home Baseline 18-target, 1-root, 28-edge requirements-governance series
  as read-only field evidence;
- optional eleven-preset composition and one-command-per-surface checks;
- a versioned release ZIP and SHA-256 smoke test.

Local package validation on 2026-07-28 passed PSScriptAnalyzer across 146
tracked PowerShell files, both sequencing fixture suites, canonical/publication
byte comparison, Spec Kit add/list/info/resolve/disable/enable/remove/reinstall,
and one generated command per Codex, Claude, Copilot, Antigravity, and OpenCode
surface. Release and fleet identifiers are added after MergeAndSync delivery.
