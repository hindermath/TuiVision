# RL-SE-Checklist-Selbstprüfung — Revalidierung für Preset v0.4.4

## Deutsch

Dieser Snapshot revalidiert die bestehende, nicht zertifizierende
157-Kontrollen-Selbstprüfung für `autonomous-run-governance` v0.4.4. Die
fachlichen Kontrolldispositionen und Human-only-Grenzen bleiben unverändert.
Neu geprüft und in `rl-se-self-review.json` gebunden wurden das Preset-Manifest,
die Registry, beide Constitution-Quellen und `AGENTS.md`. Die fortbestehende
Abweichung zwischen Constitution v1.17.1 und Memory-Constitution v1.18.2 bleibt
sichtbar; diese Revalidierung repariert oder genehmigt sie nicht.

Der historische Snapshot unter
`2026-08-30-rl-se-checklist-self-review/` bleibt unverändert erhalten.

## English

This snapshot revalidates the existing non-certifying 157-control self-review
for `autonomous-run-governance` v0.4.4. Domain dispositions and human-only
boundaries remain unchanged. The preset manifest, registry, both constitution
sources, and `AGENTS.md` were reviewed again and bound in
`rl-se-self-review.json`. The continuing difference between Constitution
v1.17.1 and Memory Constitution v1.18.2 remains visible; this revalidation
neither repairs nor approves it.

The historical snapshot at `2026-08-30-rl-se-checklist-self-review/` remains
unchanged.

## Validierung / Validation

- `RlSeSelfReviewEvidenceTests`: vertical slice, chapter draft, complete audit,
  projections, and negative fixtures.
- Exact five-source SHA-256 refresh with LF-normalized text hashing.
- Preset mapping remains 12 entries; `autonomous-run-governance` is v0.4.4.
- Product and Human-only dispositions remain unchanged.

## Intake lifecycle revalidation, 2026-09-13 / Intake-Lifecycle-Revalidierung

DE: Der autorisierte Rollout von Authoring 0.3.2, Review 0.2.2 und Sequencing
0.2.4 aktualisiert die Versions- und Hashbindungen der drei Presets sowie der
Registry, Constitution und Agent-Guidance. Die jeweiligen Aussagen wurden
gegen den Rollout-Diff erneut geprueft: schreibfreie Validatoren, eindeutige
Archivzuordnung und explizite Ausfuehrungsautoritaet bleiben verbindlich.
Dies ist eine technische Revalidierung; die 157 Kontrolldispositionen und
menschlichen Abnahmegrenzen bleiben bestehen. Die urspruengliche Beobachtung
bleibt im Git-Verlauf und im `reviewSnapshot` nachvollziehbar. Aktuelle Evidence:
`../../../maintenance/intake-lifecycle-preset-rollout.md`.

EN: The authorized rollout of Authoring 0.3.2, Review 0.2.2 and Sequencing 0.2.4
refreshes the three preset versions/hashes and registry, constitution and agent
guidance bindings. Their claims were rechecked against the rollout diff:
read-only validation, unique archive resolution and explicit execution authority
remain binding. This is technical revalidation; the 157 control dispositions and
human acceptance boundaries remain. Git history and `reviewSnapshot` retain the
original observation. Current evidence:
`../../../maintenance/intake-lifecycle-preset-rollout.md`.

## Historischer Audit und Sicherheitsrollout 2026-11-13

Der Audit bleibt unverändert ein historischer Nachweis. Seine 16 durch den
Sicherheitsrollout geänderten Eingaben sind als byteidentische Git-Kopien mit
Quellrevision und Hash im [Eingabemanifest](inputs-before-security-rollout-20261113/manifest.json)
erhalten. Die Tests prüfen diese expliziten Kopien; geänderte Auditdaten,
Bindungen oder Kopien scheitern weiterhin. Ein erfolgreiches Ergebnis ist keine
aktuelle Sicherheitsbewertung der neuen Presets. Deren aktuelle Versionen,
Quellen, Regressionstests und CI werden im getrennten Rolloutnachweis geführt.
Keine Kontrolldisposition oder menschliche Freigabe wurde geändert.

## Historical audit and security rollout 2026-11-13

The audit remains unchanged historical evidence. Its 16 inputs changed by the
security rollout are preserved as byte-identical Git copies with source revision
and hashes in the [input manifest](inputs-before-security-rollout-20261113/manifest.json).
Tests verify these explicit copies; changed audit data, bindings or copies still
fail. A successful result is not a current security assessment of the new
presets. Their current versions, sources, regression tests and CI belong to the
separate rollout evidence. No control decision or human approval was changed.
