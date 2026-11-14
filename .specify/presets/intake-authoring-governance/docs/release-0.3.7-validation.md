# Patch 0.3.7: Community-Installation / Community installation

## Änderung und Grenzen / Change and boundaries

DE: Spec-Kit-Issue #4832 meldete einen nicht direkt parsebaren mehrzeiligen
Installationsbefehl in der getaggten README. v0.3.7 verwendet eine Zeile mit
exakter Tag-ZIP-URL und Priorität 64. Manifest und Receipt-Generator binden
0.3.7. Beide Validatoren erhalten bekannte 0.3.6-Receipts; unbekannte Versionen
und falsche Schema-Kombinationen bleiben gesperrt. Tags und Archive älterer
Releases bleiben unverändert. Keine neue Produktfunktion oder Ausführungsautorität.

EN: Issue #4832 reported an installation command that the community validator
could not parse across shell continuations. v0.3.7 uses one line with the exact
tag archive and priority 64. Manifest and receipt generator bind 0.3.7; both
validators preserve known 0.3.6 receipts while rejecting unknown versions and
invalid schema combinations. Older tags/archives stay immutable. No new
product behavior or execution authority is introduced.

## Prüfumfang / Verification scope

DE: Der README-Test reproduzierte den Fehler vor der Korrektur. Danach prüfen
die bestehende native Drei-Plattform-CI und lokale Tests JSON-/Versionsintegrität,
den einzeiligen README-Befehl, Konfigurationsparität, Domänen- und Lifecycle-
Regression, v0.3.6-Receipt-Kompatibilität, LF/CRLF/BOM und unveränderte Inputs.
Exakter Head, CI-Links, Release-Commit und Paketprüfsummen stehen im PR bzw.
in den Release-Notes. Installation aus dem veröffentlichten Tag wird getrennt
geprüft; v0.3.6 wird vor/nach Veröffentlichung auf unveränderte Bindung geprüft.

EN: The README regression failed before the fix. Existing native three-platform
CI and local tests cover JSON/version integrity, the single-line command,
configuration parity, domain/lifecycle regression, v0.3.6 receipt compatibility,
LF/CRLF/BOM and unchanged inputs. Exact head, CI links, release commit and hashes
are recorded in the PR/release notes. Published-tag installation and old-release
immutability are separately verified, never inferred from source-only tests.

## Documentation Impact

DE: UpdateRequired. Owner: Thorsten Hindermann. Zielgruppen: Maintainer,
Projektverantwortliche, Lernende und Agenten. Leserpfad: README → Installation
→ dieser Nachweis → Release/Einreichung. Kanonische Quellen: Manifest, README,
Receipt-Vorlage und Validatoren. Dokumentklasse: ActiveSemantic; DE zuerst/EN
danach, ein Dokument statt Sprachpartner. Distribution: eigenständiges Preset-
Archiv; kein Home-Sync oder Verbraucher-Rollout. Einzeiliger Code bleibt als
kopierbarer Text nutzbar. NIST SSDF/CWE: sichere Versions-/Quellenbindung und
negative Regression; keine Vulnerability-, Produkt- oder Zertifizierungsabnahme.
Wiedervorlage bei geändertem Release-, Generator- oder Community-Vertrag.

EN: UpdateRequired; owner Thorsten Hindermann. Audience: maintainers, project
owners, learners and agents. Reader path: README → installation → this record
→ release/submission. Manifest, README, receipt template and validators are
authoritative. ActiveSemantic, German first/English second in one document.
Standalone preset archive only; no Home sync or consumer rollout. Copyable
plain-text installation; NIST SSDF/CWE scope is source/version binding and
negative regression, not a vulnerability audit or product/certification approval.
Reevaluate after release, generator or community-contract changes.
