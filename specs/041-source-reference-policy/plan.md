# Implementation Plan: Source Reference Policy

## Summary / Zusammenfassung

Die Policy erhält eine kleine kanonische JSON-Datei, eine bilinguale lesbare
Darstellung und einen Python-Validator mit Bash-/PowerShell-Einstiegen. Danach
werden die Governance-Flächen atomar synchronisiert und mit positiven sowie
negativen Fixtures geprüft.

## Technical context / Technischer Kontext

- JSON und Python-Standardbibliothek; keine Runtime- oder Paketabhängigkeit
- Bash und PowerShell 7 als portable Eingänge
- read-only Feature-030-Provenienz für Commit, Tree und Lizenzkontext
- Dokumentationsänderung löst DocFX, Playwright/Axe und Lynx aus

## Source review / Quellenprüfung

1. Aktuelle TuiVision-Verträge: bindender Intake und bestehende Governance.
2. Moderne Referenz: Feature-030-Pin
   `57b6f56b38e0ee75240a80a10ee0e11470c24693`, Tree
   `96dd03873955689ff0a79f6c8107a8148fe1ebd6`.
3. Historische Absicht: bestehende read-only `tv203s`-Policy; kein Produktcode
   wird in diesem Governance-Feature portiert.
4. Entscheidung: `AdoptModernization` für die zuerst geprüfte moderne
   Designreferenz, ohne ihre Semantik normativ zu machen.

## Phases / Phasen

1. Evidence, Spezifikation, Plan und Aufgaben vor Policy-Code anlegen.
2. Kanonische Policy und Validator test-first mit Ein-Ursachen-Fixtures bauen.
3. Constitution, Agent-Guidance, Pflichtenheft und Spec-Kit-Anweisungen
   synchronisieren.
4. Shell-Parität, Homogeneity, Agent-Parität, Scope und Dokumentationspfade
   validieren.

## Constitution check / Verfassungsprüfung

Die Constitution wird als ausgelöste Governance-Fläche auf Version 1.18.0
erweitert. Bilinguale CEFR-B2- und text-first Regeln gelten für alle neuen
Dokumente. Produkt-, Supply-Chain-, Runtime-AI-, Cloud- und Trust-Grenzen
ändern sich nicht; entsprechende Security-Standards bleiben `N/A` mit
Re-Evaluation bei neuem Scope.

## No-copy and license boundary / No-Copy- und Lizenzgrenze

Der externe Checkout bleibt außerhalb des Repositorys. Erlaubt sind nur Pin,
Tree, Pfade, Hashes, eigene Kurzfassungen und Permalinks. Der Lizenzkontext ist
mehrteilig und wird nicht pauschal als repositoryweites MIT bezeichnet.
