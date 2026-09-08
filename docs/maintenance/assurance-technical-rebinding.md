# Technische Evidence-Neubindung / Technical Evidence Rebinding

## Autorität und Umfang / Authority and Scope

Thorsten hat am 2026-09-07 ausdrücklich die technische Hashkorrektur nach dem
gemeldeten RLSE007-Testblocker genehmigt. Dies ist keine neue fachliche Prüfung.
Die ursprüngliche Quelle bleibt über Git-Commit `58f77c9047c3a31388995bd95e4a698408893585` erhalten:
[docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/rl-se-self-review.json](../../docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/rl-se-self-review.json).

*Thorsten explicitly approved technical hash correction on 2026-09-07 after
the reported RLSE007 blocker. This is not a new substantive review. Git retains
the original source at the commit above.*

Genau drei SHA-256-Felder ändern sich: `evidence[EVD-002].sha256`,
`evidence[EVD-015].sha256` und die gleichlautende Registry-Bindung unter
`reviewSnapshot.baselineSourceHashes[.specify/presets/.registry]`.
Der Registry-Zuwachs ist das genehmigte 13. Preset. AGENTS.md ergänzt dessen
Bedienungsgrenzen und die verbindliche GitHub-Produktquellenregel.

*Exactly three SHA-256 fields change: the two named evidence hashes and the
matching registry binding in the review snapshot. The registry adds the
approved thirteenth preset; AGENTS.md adds usage boundaries and the authoritative
GitHub product-source rule.*

| Evidence | Quelle / Source | Vorher / Before | Nachher / After |
|---|---|---|---|
| EVD-002 | `.specify/presets/.registry` | `cd67504e4d25ad63cd6b24c8f20fbbd7faa4ccdc8c05ab4d27116cc4bc82909e` | `f0e7dd8d4f24c42c4a48db0ee9fd614fd3236d57ac85e68d97d784c0da09f824` |
| EVD-015 | `AGENTS.md` | `67de6b0a2b41b3198832ffda92bbdc340e841bce8b998346ea7b8a6605bf194c` | `5d5e77e70e875a04495144d6652af14c22cd5be14201cf8b66cf150cba1299d3` |

Normalisierung wie im bestehenden Test: UTF-8, CRLF/CR zu LF, abschließende
Zeilen erhalten. / Normalization follows the existing test: UTF-8, CRLF/CR
to LF, retaining trailing lines.

- Bewertungs-JSON vorher / assessment JSON before, normalized SHA-256: `cd533ee2fdfbe70d1f510ba5a20d83c539224ec9e62fd91ba68a291c831a5bac`.
- Bewertungs-JSON nachher / assessment JSON after, normalized SHA-256: `2f807cce16dcd0643e01d1e637638758435cee6c962cadf92296ce81af160b49`.
- Byte-Diff erlaubt ausschließlich diese drei Hashersetzungen; die übrigen
  Bytes einschließlich aller 157 Kontrollbewertungen, Rollen, Risiken,
  Statuswerte, Reviewdaten und Human-only-Entscheidungen bleiben unverändert.
  / Byte comparison allows only these three hash replacements; every other
  byte, including all 157 controls, roles, risks, states, review dates and human
  decisions, remains unchanged.
- Die historischen Reviewzeitpunkte werden nicht auf heute umdatiert. Die
  neue technische Bindung ist ausschließlich hier als spätere Ergänzung
  dokumentiert, keine rückwirkende Beobachtung am alten Reviewdatum.
  / Historical review timestamps are not updated. This record explicitly
  dates the later technical binding; it is not a backdated observation.
- Die historischen zwölf Preset-Bewertungen werden nicht auf 13 umgeschrieben.
  Die Integration des zusätzlichen Pakets wird separat geprüft und dokumentiert.
  / Preserve the historical twelve-preset assessment; verify the added package
  separately in the integration record.

## Grenzen und Nachweise / Boundaries and Evidence

Die [Integration](secure-development-assurance-integration.md) und die neue
[Matrix](../security/secure-development/2026-08-30-rl-se-checklist-self-review/evidence-matrix.md)
verweisen auf den korrigierten Dateihash. Fehlende Assurance-Gate-JSONs werden
nicht erzeugt; `Blocked` bleibt beim read-only Status korrekt. C5-Konformität,
Testatreife oder Zertifizierung sowie menschliche Freigaben werden nicht
behauptet. Bestehende Tests werden weder verändert noch abgeschwächt.

*Integration and matrix link the corrected file hash. Missing gate JSONs are
not fabricated; read-only status correctly remains Blocked. No C5 conformity,
attestation readiness, certification or human approval is claimed. Existing
tests remain unchanged, not weakened.*

Documentation Impact: `UpdateRequired`. Owner: Thorsten Hindermann.
Zielgruppen / audiences: Maintainer, Agenten und Security-Reviewer. Leserpfad:
Integration → dieser Nachweis → Bewertungs-JSON/Matrix. Dokumentklasse:
technische Änderungs-Evidence, repository-lokal, DE/EN gemeinsam, kein Home-Sync.
Re-Evaluation bei Quellenänderung. Hash-/Byteprüfung und vollständiger
Release-Testlauf werden vor Lieferung erneut ausgeführt. / Technical change
evidence, repository-local, bilingual, no Home sync. Reevaluate source changes;
rerun hash/byte checks and the complete Release suite before delivery.

## Baseline-3.2.0-Neubindung am 2026-09-08 / Baseline 3.2.0 Rebinding on 2026-09-08

Die ausdrücklich beauftragte Beseitigung der Versionsabweichung aktualisiert
genau zwei weitere technische Quellenbindungen jeweils im Review-Snapshot und
im Evidence-Eintrag. Die fachlichen Dispositionen der 157 Kontrollen sowie
Rollen, Risiken, Fristen und menschliche Entscheidungsgrenzen bleiben
unverändert. / The explicitly requested version-drift correction refreshes
exactly two further technical source bindings in both the review snapshot and
the evidence entry. The 157 control dispositions, roles, risks, deadlines and
human decision boundaries remain unchanged.

| Evidence | Quelle / Source | Vorher / Before | Nachher / After |
|---|---|---|---|
| EVD-020 | `docs/secure-development/baseline-manifest.json` | `82449d57f2e072cb93e0066e7e1eee112219c9836cd51acd75027ec8436ec916` | `65d80a5cf4c93f23e81b3ec9453853c9856210f75136ab02baca0e80eb6683bb` |
| EVD-033 | `docs/secure-development/mitgeltende-dokumente/Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md` | `779655e47fb608b689ab4aa299ddc8530144562674caf3e080d18e15bd28bbe7` | `65ea3815188c27acd000d14a908eb095ed53c782aee6556b6fcd6390cc2c4567` |

Die alte Bindung bleibt im vorherigen Git-Stand und in der historischen
Migrationsevidence nachvollziehbar. Diese Neubindung ist keine rückwirkende
fachliche Prüfung. / The previous binding remains traceable in the preceding
Git state and historical migration evidence. This rebinding is not a
retroactive substantive review.
