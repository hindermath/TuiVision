# v0.1.3 – Kontext- und Risikotypbindung / Context and Risk Type Binding

Korrigiert die zwei in absdd-image-sandbox PR #57 reproduzierten Fehlerklassen:
Suffix-Auswahl eines fremden Review-Kontexts und unterschiedliche Behandlung
eines Risiko-Objekts in Bash und PowerShell.

*Fixes two reproduced defects from absdd-image-sandbox PR #57: selecting a
different context by suffix and divergent JSON risk-object validation.*

- Exakte, eindeutige datierte Kontext-ID; keine Auswahl unter Mehrdeutigkeit.
- Evidence-ID und Betriebsart stimmen mit dem Review-Auftrag überein.
- Optionales acceptedRisks ist bei Vorhandensein ausschließlich ein Array.
- Neue Negativtests scheitern vor der Korrektur; Shell-Parität,
  Read-only-Snapshots und bestehende Vertrags-/Oberflächentests bleiben Gates.
- Keine automatische Migration, fachliche Prüfung oder menschliche Freigabe.
- Keine lokalen Vendor-Patches, Tag-Umschreibung oder Community-Einreichung.

*Require a unique exact context, bind evidence ID and mode, and accept only
arrays for the optional risk collection. Red-first regressions, shell parity,
read-only snapshots and existing contract/surface suites gate delivery.
No automatic evidence migration, human approval, local vendor patch, rewritten
tag or community submission is included.*

Documentation Impact: `UpdateRequired`; Owner Thorsten Hindermann.
README, Evidence-Vertrag, Review-Befehl, Manpage und Feldtest-Runbook werden
gemeinsam aktualisiert. Zielgruppen: Maintainer, Lernende und KI-Agenten;
Deutsch zuerst, Englisch danach, textorientiert. Produktquelle ist dieses
eigenständige Repository. Kein Home-Sync; erneute Prüfung bei Vertragsänderung.

*Update README, evidence contract, review command, manpage and field-test
runbook together. German-first bilingual, text-first guidance serves
maintainers, learners and agents. This standalone repository is canonical.
No Home sync; reevaluate when the contract changes.*
