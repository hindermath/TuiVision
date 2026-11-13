# Generatorversion und Receipt-Pruefung / Generator version and receipt validation

Die bisherige Schema-2-Allowlist endete bei 0.3.2 und wies damit neue Receipts
der eigenen 0.3.4-Vorlage zurueck. Beide Wrapper akzeptieren jetzt die bekannten
Schema-2-Generatoren einschliesslich 0.3.3/0.3.4 sowie die konkrete Version aus
der eigenen `preset.yml`. Unbekannte fremde Versionen bleiben ungueltig; die
engen Regeln fuer Schema 1.0 und 1.1 bleiben erhalten.

Version 0.3.5 veroeffentlicht die Korrektur aus PR #9. Die Lifecycle-Suite
prueft vollstaendige Receipts aus der ausgelieferten Vorlage in LF, CRLF und
UTF-8 mit BOM, unveraenderte Eingabe-Hashes, bekannte Generatoren 0.3.3/0.3.4,
enge Schema-1-Grenzen und die Ablehnung von 99.0.0. Bestehende Archiv-, Quellen-,
Transaktions- und Shell-Paritaetstests bleiben verbindlich. Existierende Tags
und ZIPs bleiben unveraendert. Beim Verbraucher-Upgrade werden dokumentierte
Backports gegen das neue Paket abgeglichen; historische Receipts bleiben erhalten.

The prior schema-2 allowlist stopped at 0.3.2 and rejected receipts generated
from its own 0.3.4 template. Both wrappers now accept known schema-2 generators,
including 0.3.3/0.3.4, plus the exact version of their own preset metadata.
Unknown external versions remain invalid; schema-1.0/1.1 restrictions remain.
Version 0.3.5 publishes PR #9. Lifecycle tests exercise complete receipts from
the shipped template with LF, CRLF and UTF-8 BOM, unchanged input hashes,
known 0.3.3/0.3.4 generators, strict legacy-schema boundaries, and rejection of
99.0.0, alongside existing archive, source, transaction and shell-parity cases.
Existing tags/ZIPs are immutable. Consumer upgrades reconcile documented
backports against the new package without rewriting historical receipts.

Documentation Impact: UpdateRequired. Owner: Maintainer. Leser / audience:
Preset-Maintainer und Integratoren / preset maintainers and integrators.
Kanonische Quelle / canonical source: beide Receipt-Wrapper / both receipt
wrappers. Navigation: README. Klasse / class: source-only technical evidence.
DE/EN in dieser Datei / in this file. Home-Sync: keiner / none. Re-Evaluation:
naechster Release, Schemawechsel oder neuer Generator / next release, schema
change or new generator. Validation: macOS, Bash and PowerShell parity;
native platform evidence is attached to the source PR.
