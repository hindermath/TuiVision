# Sicherheitsnachweis: Verlinkte Intake-Evidence / Security Evidence

## Vertrauensgrenze / Trust Boundary

Der Renderer verarbeitet ausschließlich Git-getrackte, repositorylokale
Quellen. Manifest, Exact-Fixture, Intakes, Feature-State und Abschlussnachweis
werden vor der Ausgabe hash- und identitätsgebunden geprüft. Es gibt keinen
Netzwerkzugriff, keine dynamische Codeausführung und keine Verarbeitung von
Benutzerdaten oder Geheimnissen.

*The renderer consumes only Git-tracked repository-local sources. It validates
hash and identity bindings before publishing and performs no network access,
dynamic execution, user-data access, or secret handling.*

## Sichere Erzeugung / Safe Generation

- Ausgabeziele sind zwei feste repositorylokale Markdownpfade.
- `--check` ist der Standard und schreibt nicht.
- `--write` ersetzt nur die vorab festgelegten Governance-Ausgaben.
- Das Manifest muss exakt zehn eindeutige Ziele und sechs bekannte Kanten
  enthalten; jeder Feature-State muss den exakten Intakehash akzeptieren.
- Der NuGet-Backlog darf weder aktive Zeile noch Abhängigkeitsendpunkt sein.
- Das Produktversionsdokument bleibt bytegleich; Produkt- und Paketpfade sind
  nicht autorisiert.

*The command defaults to read-only checking. Write mode is restricted to fixed
governance outputs, and exact identity checks fail closed before publication.*

## Secure-Coding-Review / Secure-Coding Review

Für den JavaScript-Pfad wurden Eingabevalidierung, Pfadgrenzen, Datei-I/O,
Fehlerbehandlung und unbeabsichtigte Ausweitung des Schreibumfangs geprüft.
NIST SSDF und CWE Top 25 sind anwendbar. Authentifizierung, Autorisierung,
Kryptografie, Datenbankzugriff, SSRF, ASVS, VEX, SLSA und AI-SBOM sind mangels
entsprechender Produkt- oder Liefergrenze `N/A`.

*Input validation, path boundaries, file I/O, failure handling, and write scope
were reviewed. Product security domains absent from this documentation-only
change are explicitly not applicable.*

## Rücknahme / Rollback

Die Änderung ist durch Rücknahme der Fixture, der Renderer-/Teständerung und
beider generierter Ansichten vollständig reversibel. Eine Rücknahme benötigt
keine Produktdatenmigration.

*Rollback consists only of reverting tracked governance files and requires no
product-data migration.*
