# Statistik-Rollout v0.1.0 / Statistics rollout v0.1.0

## Auftrag und Quellen / Authority and sources

Dritter Kandidat des [Rollouts #302](https://github.com/hindermath/home-baseline/issues/302),
nach gelieferten TinyPl0 PR #98 und InventarWorkerService PR #74. Ausgangscommit:
`e1edbb1c2f9b0e0a8e0ee93c50bb578fdf4260d5`. Installation und Erstmessung sind
beauftragt; fachliche Abnahme und Lieferfreigabe von @hindermath bleiben vor
dem Merge erforderlich. Kein Admin-Bypass aus einem anderen PR uebertragbar.
Keine neuen Spec-Kit-Laeufe, Produkt-/API-Aenderungen oder Releasefreigaben.

Third approved rollout target, following TinyPl0 PR #98 and InventarWorkerService PR #74.
Human acceptance and delivery permission precede merge; no inherited admin
bypass. No product/API changes, new feature runs or release decisions.

[Installationsbeleg](project-statistics-installation-v010.json):
26 Paketdateien einschliesslich versteckter Dateien stimmen mit dem geprueften
v0.1.0-ZIP ueberein. Alle 13 Registry-Eintraege und vorher getrackten
Integrationsdateien waren unmittelbar nach Installation unveraendert.
Paketcommit `7e824ca8de11212aefdc5b05d7d05637f5343dab`;
ZIP SHA-256 `d8ad7d5eef920f50b629121b64ba8123c22ec4f6dadd14da5cd826d1c50f420a`.
Statistik-Prioritaet 90; Assurance v0.1.3 bleibt bei 15.
Die 14er-Matrix ist quellengebunden; globale Defaults bleiben unveraendert.
Operative lokale Profilzuordnung erst nach Lieferung.

The receipt binds all payload hashes and the central matrix. Preserve the
existing thirteen presets. Guidance changes follow separately; operational
assignment is deferred until delivery. No global default changes.

## Konfiguration und Pruefung / Configuration and verification

[Bedienung](../project-statistics/README.md), [Konfiguration](../project-statistics/config.json),
[Bericht](../project-statistics/report.md) und [Snapshot](../project-statistics/snapshot.json).
UTC, 52 Wochen, keine manuellen Phasen oder Referenzmodelle.
Die bisherige Profil-2-Statistik bleibt kanonisch (Europe/Berlin, 80/125).
Beide Verfahren schliessen den neuen Kontext aus. Unterschiedliche Aktivtage
sind moeglich und kein pauschaler Fehler. Der Legacy-Ausschluss von `Directory.Build.props` bleibt erhalten.

Use a separate UTC/52-week context without authored phase totals or reference
models. Preserve Profile 2 and its 80/125 references. Both exclude the new
context and preserve the Directory.Build.props exclusion; timezone/methodology differences can change activity counts.

Installation separat committen; Init-Vorschau vor Init. Danach Inhalte und
Konfiguration committen, Messung vorschauen und beide Renderer aus derselben
Inhaltsrevision ausfuehren. Keine dirty-tree-Ausnahme. Native Kontextpruefung:
`scripts/prove-project-statistics-context.ps1`, mit
[Manpage](../man/prove-project-statistics-context.1.md).
Wiederverwendete TinyPl0-Prueflogik: Paketbindung, exakte Matrix, Lifecycle,
Fixtures, LF/CRLF/BOM, Idempotenz und lesender Status; Schreibtests nur in
isolierten lokalen Kopien. Keine vollstaendige I/O-Ueberwachung behauptet.
CI prueft Linux/Windows am exakten PR-Head; macOS lokal. Nachweise im PR.

Commit installation, preview initialization, then commit configuration/content
before measuring. Use the same source revision for both renderers.
The reused proof runs source checks read-only and writing tests in disposable
clones. Native CI binds Linux/Windows evidence to the exact PR head; local
macOS evidence is reported in the PR. No full system-I/O trace is claimed.

Bestehende .NET-Build-/Test-, Coverage-, Security- und Docs-/A11Y-CI bleibt aktiv.
Keine lokale Produkt-Build-Ausfuehrung und keine Versionsaenderung: dies ist
kein nummerierter Spec-Kit-Branch. Release- und Dependabot-PRs unberuehrt.

Retain existing product and security CI. No local product build or version
change: this is not a numbered feature branch. Leave the release PR untouched.

## Dokumentationsauswirkung und Sicherheit / Documentation impact and security

Die beauftragte [RL-SE-Delta-Nachpruefung](../security/secure-development/2026-09-12-rl-se-checklist-self-review-v044/statistics-rollout-delta-2026-09-19.md)
haelt alte/neue Quellenbindungen und den semantischen Vergleich getrennt fest.
Historische Kontrollentscheidungen und menschliche Freigaben bleiben erhalten.

The authorized RL-SE delta review records previous/current source bindings and
the semantic assessment separately, without changing historical control decisions.


`UpdateRequired`; Owner Thorsten Hindermann. Zielgruppen: Lernende ab Jahr 1,
Maintainer und Reviewer. README -> Kontextanleitung -> Bericht/Snapshot ->
Integrationsbeleg. ActiveSemantic, Deutsch zuerst/Englisch danach, text-first.
Projektlokale Distribution, kein Home-Sync. Paketquelle: eigenstaendiges
Preset-Repository; Konfiguration: dieses Repository. Fuenf Agentenflaechen
gemeinsam gepflegt. Constitution, Spec-Kit-Constitution, Registry-Zeile und
Templates geprueft: Runtime, Produktarchitektur und 80/125-Basis bleiben
unveraendert. Kein lokaler Skriptkatalog vorhanden. Re-Evaluation bei Quellen-,
Methodik-, Paket-, Profil- oder Bedienungswechsel.

`UpdateRequired`, owner Thorsten Hindermann. Bilingual text-first reader path
for apprentices, maintainers and reviewers. Project-local integration, no home
distribution. Review both constitutions and environment context without
changing runtime, architecture or existing references. Five guidance surfaces
are synchronized; no local script catalog exists. Reevaluate on source,
methodology, package, profile or usage changes.

NIST SSDF/CWE Top 25: Paket-/Dateigrenzen, gehashte CI-Actions, minimale
Leserechte und keine Secrets im Nachweis. OWASP ASVS N/A fuer diese lokale Statistik-CLI ohne Web-/Auth-Flaeche;
keine neue Produkt-Sicherheitsabnahme. SBOM/Provenance des Presets bleiben
referenzierbar; keine SLSA-Level-Behauptung, kein neuer VEX-Befund.
AI-SBOM N/A: KI nur Entwicklungswerkzeug. Zero Trust/CAPEC/SAMM:
vorhandene Produktbewertungen bleiben erhalten, keine neue Vertrauensgrenze.
C5 N/A fuer diesen Ausbildungs-Scope. Keine Qualitaets-, Sicherheits-,
Ausbildungsleistungs-, Konformitaets- oder Zertifizierungsbehauptung aus Zahlen.

Apply SSDF/CWE checks to package/file boundaries and least-privilege CI.
ASVS is not applicable to this local statistics CLI without web/auth surfaces.
Reuse preset SBOM/provenance; claim no SLSA level or new VEX disposition.
AI-SBOM is not applicable to development-only AI. Preserve existing product
Zero Trust/CAPEC/SAMM assessments. C5 is not applicable to this training scope.
Statistics provide no quality, security, attainment or certification proof.
