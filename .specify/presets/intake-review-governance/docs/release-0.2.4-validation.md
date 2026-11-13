# Coordinated patch 0.2.4: validation and documentation

Documentation Impact: `UpdateRequired`. Owner: Thorsten Hindermann.
Readers: preset maintainers, project owners and coding agents through README,
the shipped addenda and command surfaces. Canonical change: the shared
requirements-intake configuration validator. Distribution: source and preset
archive; no host tool installation, product run or acceptance is implied.
Reevaluate when the series schema or lifecycle contract changes.

This patch publishes the already merged running-series correction. An Active
series with an Active member may have zero additional Eligible candidates,
represented as N/A. Ready still requires exactly one candidate. More than
one candidate, invalid state, dependency, path or hash remains blocked.
No new command, execution authority, schema or priority is introduced.

Coordinated targets: Authoring 0.3.6, Review 0.2.4, Sequencing 0.2.7.
Independent installation remains supported. Historical receipts/series
remain unchanged. Authoring's known schema-2 generator allowlist preserves
0.3.5 compatibility while its own template binds 0.3.6.

Local macOS regression passed, including Bash/PowerShell JSON and zero-write
parity, valid Active/Ready transitions and invalid-state/path/hash/dependency
fixtures. Domain validator/lifecycle tests passed. Native Linux and Windows
proof is provided by the exact-head lifecycle CI, not inferred from macOS.
Package and publication hashes are recorded in the release notes after delivery.

German-first/English-second README guidance now identifies current 0.2.4,
preserves historical 0.2.3 context and documents the coordinated versions.
No user-facing product behavior or runtime dependency was added.
ASVS/cloud provider audits are N/A for this validator patch. NIST SSDF/CWE
scope covers path containment, source integrity, fail-closed validation,
negative fixtures and immutable publication. No vulnerability audit,
certification or product acceptance is claimed.
