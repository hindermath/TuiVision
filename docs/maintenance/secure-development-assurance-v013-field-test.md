# Secure Development Assurance v0.1.3 – TuiVision-Feldtest

## Ergebnis und Scope / Result and Scope

**Empfehlung: `ReleaseAccepted`.** TuiVision bestaetigt das unveraenderte
Preset `secure-development-assurance-governance` v0.1.3 fuer die beiden
dokumentierten projektbezogenen Feldtestkontexte. Die Empfehlung gilt
ausschliesslich fuer die Funktionsfaehigkeit des Presets im nichtkommerziellen
Ausbildungs- und Beispielprojekt. Sie ist weder Produktfreigabe noch Pilot-,
Projekt-, Risiko-, C5-, Konformitaets- oder Zertifizierungsentscheidung.

*Recommendation: `ReleaseAccepted`. TuiVision confirms the unchanged
`secure-development-assurance-governance` v0.1.3 preset for both documented
project field-test contexts. The recommendation covers preset behavior in a
non-commercial training and example project only. It is not a product release,
pilot, project, risk, C5, conformity, or certification decision.*

Test-Owner und technischer Reviewer ist `@hindermath`. Geprueft wurden:

- `docs/security/secure-development/2026-08-30-rl-se-checklist-self-review`
- `docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review`

Produktcode, Produkt-API, Runtime, Abhaengigkeiten, Pakete, Images und
historische Quellen wurden nicht geaendert. Die fachlichen 157-Kontroll-
Bewertungen beider Kontexte bleiben historische Zeitpunktaufnahmen.

## Paket- und Umgebungsbindung / Package and Environment Binding

| Feld / Field | Nachweis / Evidence |
|---|---|
| Release | `v0.1.3`, Pre-Release |
| Tag-Commit | `0d03aa9ebe8f74a26e331815bca5609fb48d7a14` |
| Tag-ZIP | `https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/archive/refs/tags/v0.1.3.zip` |
| ZIP SHA-256 | `9023b442b4d82e25bee5a7fe9b73efb7f591a4f265f54061ae6e4a56b9b5c75f` |
| Spec Kit | `0.12.8` |
| Security Governance | `0.6.2`, Prioritaet 10 |
| Assurance-Preset | `0.1.3`, Prioritaet 15 |
| Preset-Profil | 13 Presets, exakt |
| Host | macOS 26.6.2, Apple Silicon |
| Shells | GNU Bash 3.2.57; PowerShell 7.6.5; jq 1.7.1 |
| Vorgelagerte Produkt-/Evidence-CI | [TuiVision PR #170](https://github.com/hindermath/TuiVision/pull/170), Head `b2089c56ff60255493a487189df441ef4093da7d`, Merge `474af1114422d980aed7ad3993435014ddf1548b` |
| Feldtest-Lieferung | PR-Link und Evidence-Commit werden im Liefer-PR gebunden. / PR link and evidence commit are bound in the delivery PR. |

## Technische Pruefung / Technical Validation

| Test | Ergebnis | Exitcode |
|---|---|---:|
| Release-ZIP erneut laden und SHA-256 pruefen | bestanden | 0 |
| 13-Preset-`CheckOnly`, Bash | bestanden | 0 |
| 13-Preset-`CheckOnly`, PowerShell | bestanden | 0 |
| `preset list`, `preset info` fuer Security und Assurance | bestanden | 0 |
| Resolve `secure-development-evidence-contract`; `specify check` | bestanden | 0 |
| Zwei positive Statuspruefungen, Bash und PowerShell | jeweils `Ready`, fachlich gleich | 0 |
| Vier Einzelreviews pro Kontext und Shell | alle 16 Aufrufe `Ready` | 0 |
| Roh-Hash-Snapshot vor/nach Status und Reviews | je Kontext 7 von 7 Evidence-Dateien unveraendert | 0 |
| Vertrags-, Negativ-, LF-/CRLF-/BOM- und Shell-Paritaetstest | bestanden | 0 |
| Acht erzeugte Agenten-/Command-Flaechen aus dem Tag-ZIP | bestanden; fehlende Evidence blockiert jeweils geregelt | 0 |
| Temporaere Komposition: 13, Disable, Enable, Remove, gueltige 12, Reinstall | bestanden | 0 |
| RL-SE-Matrix | 157 eindeutige IDs; 13 AlreadySatisfied, 65 Applicable, 38 N/A, 36 Open, 5 FollowUp | 0 |
| GSDB-Matrix | 157 eindeutige IDs; 157 Open als historische Zeitpunktaufnahme | 0 |

Die Preset-Negativsuite prueft die im Feldtest-Runbook geforderten
Blockierfaelle, einschliesslich Hash-, Manifest-, Checklisten-, Versions-,
Sammelband-, Review-, Risiko-, Security-Abhaengigkeits-, Runbook-, Image-
Impact- und unzulaessiger C5-/Zertifizierungsbehauptungen. Die synthetischen
Fehler enden erwartungsgemaess mit Exitcode 2; der Gesamttest endet mit
Exitcode 0. Der C5-Negativfall ist ein Fail-closed-Sicherheitstest und keine
C5-Pruefung des Projekts.

*The preset negative suite covers the runbook's required blockers, including
hash, manifest, checklist, version, compendium, review, risk, security
dependency, runbook, image-impact, and invalid C5/certification claims. Each
synthetic defect returns exit code 2 as required; the complete regression test
returns 0. The C5 negative case tests fail-closed behavior and is not a C5
assessment of the project.*

PR #170 hat fuer denselben Produkt- und Evidence-Stand Restore, Release-Build
und die vollstaendige Solution-Testausfuehrung auf Ubuntu, macOS und Windows
bestanden. Das Solution-Target enthaelt die nichtinteraktiven
`TuiVision.Examples.SmokeTests`; der dokumentierte lokale Release-Lauf bestand
214/214 Tests. Der aktuelle Liefer-PR muss diese technischen Workflows erneut
gruen durchlaufen, bevor der formale Reviewer-Bypass verwendet werden darf.

## Fachliche Grenzen und Wiedervorlage / Decision Boundaries and Review Dates

- In beiden Kontexten sind alle vier Assurance-Gates `Ready`;
  `technicalValidation` ist jeweils `Fulfilled`.
- `pilotAuthorization`, `projectAcceptance` und `generalRelease` bleiben
  ausdruecklich `Open`.
- Alle Gate-Evidence verwendet `reviewDue=2027-09-08` als technische
  Jahreswiedervorlage, nicht als automatischen Freigabetermin.
- C5 ist fuer den aktuellen nichtkommerziellen Ausbildungs- und Beispielscope
  `N/A`; GitHub, CI, NuGet und Artefakthosting sind Entwicklungs- und
  Lieferinfrastruktur, keine Produkt-Cloud-Runtime.
- CRA und formale Produktkonformitaet sind fuer diesen Scope `N/A`.
  Regulatorische Wiedervorlage ist `2026-12-31`, frueher bei kommerzieller
  Nutzung, Marktbereitstellung, Kundenuebergabe, Supportvertrag oder geaenderter
  Hersteller-/Steward-Rolle.
- Es wurde kein Risiko akzeptiert. Restrisiko bleibt eine unbemerkte Aenderung
  von Produkt-, Delivery- oder wirtschaftlichem Scope.

## Abweichungen, Findings und Abschluss / Deviations, Findings, and Closeout

Zwischen Bash und PowerShell besteht keine fachliche Ergebnisabweichung.
Zwischen LF-, CRLF- und UTF-8-BOM-Fixtures besteht keine semantische
Abweichung; rohe Hash-Snapshots erkennen Byteaenderungen weiterhin. Der
bekannte Spec-Kit-CLI-Hinweis zu moeglichen verwaisten Agentenflaechen nach
`preset remove` bleibt eine dokumentierte CLI-Grenze; die Neuinstallation aus
dem unveraenderlichen Tag-ZIP stellt den gueltigen 13-Preset-Zustand wieder her.

Offene Punkte dieses Projektfeldtests: keine. Die Community-Einreichung
`github/spec-kit#4455`, die Zusammenfuehrung der fuenf Projektberichte und eine
spaetere zentrale v0.1.3-Preset-Abnahme sind ausdruecklich ausserhalb dieses
Projekturteils und werden abgewartet.

*There is no substantive Bash/PowerShell or line-ending variance. Raw snapshots
still detect byte changes. The documented Spec Kit CLI removal limitation
remains; reinstalling the immutable tag ZIP restores the valid 13-preset state.
This project field test has no open finding. Community issue
`github/spec-kit#4455`, consolidation of all five project reports, and the later
central v0.1.3 preset decision remain outside this project verdict.*

## Dokumentationsauswirkung / Documentation Impact

`UpdateRequired`. Owner ist Thorsten Hindermann. Kanonische Quelle ist dieser
Feldbericht; die aktualisierten Security-Entscheidungen und beide Evidence-
Kontexte sind dessen Nachweise. Zielgruppen sind Maintainer, technische
Reviewer und Lernende. Leserpfad: v0.1.3-Integration -> Feldbericht -> Security-
Dokumente -> maschinenlesbare Evidence. Die Aenderung ist `sourceOnly`,
zweisprachig, textorientiert und benoetigt keinen Home-Sync. Neu zu bewerten ist
bei Preset-, Baseline-, Produkt-, Delivery- oder Scopeaenderung.
