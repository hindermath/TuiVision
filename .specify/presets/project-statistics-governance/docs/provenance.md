# Herkunft und Lieferkette / Provenance and supply chain

## Produktquelle / Product source

Alleinige Produktquelle ist dieses eigenstaendige MIT-Repository.
Installierte Kopien sind Integrationen, keine zweite Entwicklungsquelle.

This standalone MIT repository is the only product source. Installed copies
are integrations, not a second development source.

## Wiederverwendung / Reuse

Kategorie-, Ausschluss- und ASCII-Hilfsfunktionen stammen aus
[home-baseline renderer](https://github.com/hindermath/home-baseline/blob/ade796af59bdd2248a264d0dc0d3f45728997838/scripts/render-project-statistics.ps1).
Der Byte-Zaehler wurde fuer Git-Blob-Streams angepasst. MIT-Hinweise bleiben
in LICENSE erhalten. Neue Quellenbindung, Konfiguration und Statuslogik sind
Bestandteile dieses Presets. Keine Rueckkopie des neuen Messkerns in Home Baseline.

Classification, exclusion and ASCII helpers come from the linked MIT source.
The byte counter was adapted to Git blob streams. Preserve MIT notices.
Source binding, configuration and status logic belong to this preset.
Do not copy the new engine back into Home Baseline.

## Anwendbarkeit / Applicability

NIST SSDF und sichere Eingabe-/Datei-/Prozessverarbeitung gelten fuer dieses
Werkzeug. Review-Schwerpunkte: Argument-Injection, Pfadgrenzen, Symlinks,
unvollstaendige Git-Historie, manipulierte Snapshots und Schreibautoritaet.
Web-ASVS, KI-Runtime/AI-SBOM, Cloud-Betrieb/C5 und regulierte Produktfreigaben
sind fuer diesen lokalen Ausbildungs-/Entwicklungshelfer N/A: keine Web-API,
kein Modell im Produkt und kein produktiver Cloud-Dienst.
Dies ist eine Scope-Dokumentation, keine juristische oder formelle Freigabe.

NIST SSDF and secure input/file/process handling apply. Review argument
injection, path boundaries, symlinks, incomplete history, snapshot tampering
and write authority. Web ASVS, AI-runtime/AI-SBOM, cloud operation/C5 and
regulated product approval are N/A for this local training/development helper:
no web API, embedded model or production cloud service. This is a scope record,
not a legal or formal approval.

Das Paket enthaelt keine Drittanbieter-Laufzeitbibliotheken. Git und PowerShell
sind externe Voraussetzungen; Spec Kit ist die Installationsintegration.
CI-Aktionen werden per Commit gepinnt. Release-Commit, Tag-ZIP-SHA-256 und
native CI-Laufnachweise werden beim Pre-Release erfasst. Kein SLSA-Level oder
OpenSSF-Score wird ohne entsprechende Attestierung behauptet. VEX wird erst
bei einem konkreten Vulnerability-Befund benoetigt.

The package ships no third-party runtime libraries. Git and PowerShell are
external prerequisites; Spec Kit is the installation integration. Pin CI actions
to commits. Record release commit, tag archive SHA-256 and native CI evidence.
Do not claim a SLSA level or OpenSSF score without attestation. VEX is needed
only when a concrete vulnerability finding requires disposition.
