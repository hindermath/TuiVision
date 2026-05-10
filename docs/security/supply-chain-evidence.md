# Supply-Chain-Evidenz / Supply Chain Evidence: TuiVision

**Projekt / Project**: TuiVision (Level-2)
**Datum / Date**: 2026-04-24
**Status**: Stub — mit projektspezifischen Inhalten zu befuellen / Stub — to be populated
**Template-Quelle / Template Source**: `.specify/templates/supply-chain-evidence-template.md`

<!--
  Dieses Dokument ist ein Stub. Die vollstaendige Struktur findet sich im
  Template unter .specify/templates/supply-chain-evidence-template.md. Bei der Befuellung das Template als Vorlage
  verwenden.

  This document is a stub. The complete structure can be found in the
  template at .specify/templates/supply-chain-evidence-template.md. Use the template as a guide when populating.
-->

## 010-standard-dialogs-designer

Datum: 2026-05-02.

Diese Feature-Arbeit erzeugt kein Release-Artefakt. Deshalb werden SBOM, VEX
und SLSA-/Provenance-Nachweise nicht lokal in diesem Branch erzeugt, sondern
bleiben an den naechsten Release-Prozess gekoppelt. Fuer die Implementierung
wurden keine neuen Abhaengigkeiten eingefuehrt.

This feature work does not produce a release artifact. SBOM, VEX, and
SLSA/provenance evidence are therefore not generated locally in this branch and
remain tied to the next release process. No new dependencies were introduced.

| Nachweis / Evidence | Status | Notiz / Note |
|---|---|---|
| Dependency delta | PASS | keine neuen Abhaengigkeiten / no new dependencies |
| SBOM | release-gebunden | beim naechsten Release aktualisieren / update at next release |
| VEX | release-gebunden | nur mit Release-Artefakten sinnvoll / meaningful with release artifacts |
| SLSA / Provenance | release-gebunden | CI-/Release-Pipeline bleibt Nachweisort / CI/release pipeline remains evidence location |

## 011-port-wave2-examples

Datum: 2026-05-08.

Die elf neuen Beispielprojekte verwenden nur bestehende Projektmodule und
fuehren keine neue NuGet-, npm- oder externe Laufzeitabhaengigkeit ein. Die
Beispiele sind lokal ausfuehrbare Review-Artefakte; SBOM, VEX und
SLSA-/Provenance-Nachweise bleiben an den naechsten Release-Prozess gekoppelt.

The eleven new example projects use only existing project modules and introduce
no new NuGet, npm, or external runtime dependency. The examples are locally
runnable review artifacts; SBOM, VEX, and SLSA/provenance evidence remain tied
to the next release process.

| Nachweis / Evidence | Status | Notiz / Note |
|---|---|---|
| Dependency delta | PASS | keine neuen Abhaengigkeiten / no new dependencies |
| SBOM | release-gebunden | keine Feature-lokale Release-Erzeugung / no feature-local release generation |
| VEX | release-gebunden | bei Release-Artefakten nachziehen / update with release artifacts |
| SLSA / Provenance | release-gebunden | CI-/Release-Pipeline bleibt Nachweisort / CI/release pipeline remains evidence location |
| Beispiel-Artefakte / Example artifacts | review-only | `dotnet run --project examples/<Name>` ohne Paketveroeffentlichung / no package publishing |

## 012-interactive-wave2-demos

Datum: 2026-05-10.

012 erzeugt keine neue Release-Artefaktklasse und fuehrt keine neue
Abhaengigkeit ein. SBOM, VEX und Provenance bleiben an den regulaeren
Release-Prozess gekoppelt; die lokalen Beispielprogramme sind Review- und
Lernartefakte.

Date: 2026-05-10.

012 creates no new release artifact class and introduces no new dependency.
SBOM, VEX, and provenance remain tied to the regular release process; the local
example programs are review and learning artifacts.
