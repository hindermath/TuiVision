# Implementation Plan: Sandbox Secure Development Hardening

## Technischer Kontext / Technical Context

- .NET-10-/C#-Repository; keine Produkt-, API-, Dependency-, Projekt- oder
  Beispieländerung.
- Kanonische Feature-Evidence als Markdown und JSON unter `docs/security/`.
- Portabler read-only Validator: gemeinsamer Python-Kern, Bash- und
  PowerShell-Einstieg, Unix-Manpage und Pester-unabhängiger unittest-Vertrag.
- Read-only Referenz auf den separaten Sandbox-Checkout; keine Vendorisierung
  und keine Änderung eines anderen Repositories.
- Delivery: `MergeAndSync`; Admin-Bypass nur, wenn alle technischen Gates grün,
  keine umsetzbaren Threads offen und Human Approval die einzige offene Regel
  ist.

## Constitution Check

- **Branch/PR**: nummerierter Branch 044, nicht leerer PR, Merge-Commit und
  sauberer `main`-Sync.
- **Toolchain**: C# bleibt speichersicher; der Validator nutzt vorhandenes
  Python 3 plus portable Wrapper und führt keine neue Dependency ein.
- **Architektur**: keine Produktarchitekturänderung. Host-, Mount-, Volume-,
  Container- und Netzwerkgrenzen werden als Security-Evidence beschrieben.
- **A11Y/Dokumentation**: German-first/English-second, CEFR-B2, semantische
  Überschriften, Texttabellen und vollständige Textalternativen.
- **DocFX**: neue navigierbare Security-Dokumentation löst DocFX, Playwright/Axe
  und Lynx aus; XML/API bleiben unverändert.
- **Tests**: Test-first über ungültige Fixture vor gültiger Assessment-Evidence;
  Produkt-Coverage ist `N/A`, weil keine Assembly geändert wird.
- **Dependencies**: keine Paketänderung. Bestehende Dependency- und temporäre
  SBOM-Gates werden erneut belegt, ohne Buildartefakte zu committen.
- **Daten**: JSON wird strukturiert geparst; kein ad-hoc Textparsing für
  kanonische Evidence.
- **Agenten**: gemeinsame Agent-Guidance bleibt `NoUpdateRequired`, sofern die
  Implementierung keine neue projektweite Regel entdeckt.

## Vertikaler Referenz-Slice / Vertical Reference Slice

1. Ungültige Fixture mit fehlender CL-12-Zeile anlegen und rot nachweisen.
2. Minimalen Python-Validator plus Bash-/PowerShell-Einstiege erstellen.
3. Gültige Assessment-JSON mit genau zwölf Zeilen erstellen.
4. Beide Einstiege auf demselben Pfad grün belegen.
5. Danach Mount-, Execution-, Guide- und Governance-Evidence vervollständigen.

## Zielstruktur / Target Structure

```text
docs/security/secure-development/2026-08-29-sandbox-applicability/
  README.md
  assessment.json
  mount-policy.md
  execution-matrix.md
scripts/
  validate-sandbox-applicability.py
  validate-sandbox-applicability.sh
  validate-sandbox-applicability.ps1
scripts/tests/
  test_sandbox_applicability.py
  sandbox-applicability/fixtures/
docs/man/
  validate-sandbox-applicability.1.md
specs/044-sandbox-secure-development-hardening/
  feature and delivery evidence
```

## Durchführung / Execution

1. Referenz-Commit, Quellhashes und vorhandene Feature-016-Evidence binden.
2. CL-12-, Mount- und Execution-Modell als JSON-Vertrag implementieren.
3. Negative Fixture zuerst, dann Validator und gültige Evidence liefern.
4. Lernendenorientierte Mount- und Execution-Guides anlegen und im
   Security-Index verlinken.
5. Recommendation als `ConditionallyUsable` setzen: technisch geeignet für
   nicht sensible TuiVision-Arbeit mit engem Mount; formelle Freigabe,
   Datenklassifikation, Egress und reale Plattform-Evidence bleiben offen.
6. Feature-Evidence, Statistik, Version und Lifecycle-Serienstatus seriell
   aktualisieren.

## Validierungsfolge / Validation Sequence

1. Python-unittest mit positiven und negativen Fixtures.
2. Bash- und PowerShell-Validator auf der kanonischen Assessment-Datei.
3. `bash -n`, PowerShell-Parser/PSScriptAnalyzer und Manpage-/Hilfetextprüfung.
4. Sandbox read-only: Commit/Remote-Gleichheit, `git diff --check` und
   `podman-compose --env-file .env.example config --quiet`.
5. TuiVision: `git diff --check`, `dotnet format --verify-no-changes`,
   Agent-Parität, Homogeneity, Secret- und Supply-Chain-Prüfung.
6. DocFX, Playwright/Axe und repräsentativer Lynx-Dump.
7. Keine lokale Produkt-Build-/Testwiederholung, solange kein `.cs`, `.csproj`,
   `.sln`, Paket oder Produktvalidator geändert wird; die unveränderte CI darf
   weiterhin ihre normalen Matrix-Gates ausführen.
8. Exact-Head-PreMerge-Evidence, Review-Konvergenz, Merge und PostMerge-Sync.

## Complexity Tracking

Der dreiteilige Validator ist durch die Cross-Platform-Regel begründet. Die
fachliche Logik bleibt einmal im Python-Kern; Bash und PowerShell sind dünne,
read-only Einstiegspunkte mit identischen Exitcodes. Kein Framework oder neues
Paket wird eingeführt.

## Autonomous Execution Contract

- Evidence existiert vor Implementierungsänderungen.
- Clarify endet ohne materielle Frage; Checklists und Analyze konvergieren nach
  Outcome statt Wiederholungszahl.
- Gemeinsame Statistik-, Version-, Lifecycle-, Evidence- und Deliverydateien
  haben jeweils nur einen seriellen Writer.
- Out-of-scope Image-, Runtime-, Provider- oder Organisationsbefunde werden
  nicht implementiert und nur mit realem Owner als Follow-up geführt.
- Remote-Abschluss folgt ausschließlich der aktuellen MergeAndSync-Autorität;
  ein Bypass ersetzt niemals einen technischen oder fachlichen Gate-Nachweis.
