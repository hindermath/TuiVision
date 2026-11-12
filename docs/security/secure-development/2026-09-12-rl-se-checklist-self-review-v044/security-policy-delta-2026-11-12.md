# Meldepolitik: Evidence-Delta / Reporting policy evidence delta

## Deutsch

Der Owner hat die Harmonisierung der Sicherheitsrichtlinie und deren Lieferung
beauftragt. Der neue Text erhält den privaten Advisory-Kanal, benötigte
Berichtsinformationen, koordinierte Offenlegung, Attribution und Anonymität,
die menschliche Verantwortung sowie die offene Entscheidung über konkrete
Reaktionsfristen. Er ergänzt die genehmigte Sicherheitsbaseline und trennt
vollständige deutsche und englische Sprachbereiche. Keine menschliche
Kontrollfreigabe oder organisatorische SLA wird daraus abgeleitet.

Die [vorherige Auditfassung](https://github.com/hindermath/TuiVision/blob/abaa3f4e8b1318b849fffa29019e6a6a082d5630/docs/security/secure-development/2026-09-12-rl-se-checklist-self-review-v044/rl-se-self-review.json)
bleibt unter ihrem unveränderlichen Git-Commit erhalten. Im aktiven
Feature-045-Nachweis werden ausschließlich `sha256`, `observedAtUtc` und
`result` von EVD-016 fortgeschrieben. Alle Kontrollstatus, Rollen, Fristen,
Summary-Werte und Review-Snapshot-Angaben bleiben unverändert. Die historischen
Feature-046-Snapshots und älteren Auditfassungen werden nicht umgeschrieben.

Vorheriger SHA-256 (UTF-8, LF):
`351fc1345c7f1b096e48a3f8ceeb515927d61f62757fa3a257fb9aa187ad260e`.
Neuer SHA-256:
`ab31abc25cf28cfaa7df21b2449f7cfbd060ec832be5be77afbb83098681e841`.

Vor Fortschreibung meldeten drei unveränderte RL-SE-Tests `RLSE007` für den
veralteten Hash. Diese Tests und die vollständige technische PR-CI müssen am
neuen Head bestehen. Es werden keine Gates abgeschwächt. Bei weiterer Änderung
der Richtlinie müssen Bindung und fachliches Delta erneut geprüft werden.

## English

The owner authorised the reporting-policy harmonisation and delivery. The new
text preserves private advisories, reporting information, coordinated
disclosure, attribution and anonymity, human ownership, and the open decision
on specific response deadlines. It adds the approved security baseline and
complete German/English language sections. No human control approval or
organisational SLA follows from this change.

The immutable Git revision linked above preserves the previous audit. Only
`sha256`, `observedAtUtc`, and `result` of EVD-016 are refreshed in the active
Feature 045 evidence. All control states, roles, deadlines, summary values and
review-snapshot fields remain unchanged. Historical Feature 046 snapshots and
older audits are not rewritten. The two hashes above identify the old and new
UTF-8/LF policy content.

Before refresh, three unchanged RL-SE tests reported `RLSE007`. Those tests and
the full technical PR CI must pass at the new head; no gate is weakened. Review
the binding and semantic delta again whenever the policy changes.

Documentation Impact: `UpdateRequired`; Owner Thorsten Hindermann. Project-local
evidence, no Home sync; no product, release, security-certification or human
acceptance claim. / Projektlokaler Nachweis ohne Produkt-, Release-,
Zertifizierungs- oder menschliche Abnahmebehauptung.
