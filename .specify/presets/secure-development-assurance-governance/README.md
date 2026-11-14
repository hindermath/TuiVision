# Secure Development Assurance Governance

<!-- current-package: 0.1.10 -->

Paketkennung: **v0.1.10**. [Vorbereitung und Veröffentlichungsgrenze](docs/release-0.1.10-validation.md). Die Release-Seite und der externe Archivnachweis belegen die Veröffentlichung, nicht dieser Vorbereitungsstand. Frühere Versionsnachweise unten bleiben historisch.

<span lang="en">Package identity: v0.1.10. The release page and external archive evidence prove publication, not this preparation snapshot. Earlier version records below remain historical.</span>

[Release-Abschluss und Verbesserungsprofil / Publication closeout and improvement profile](docs/release-0.1.7-validation.md).

Der unten verlinkte Basispaket-Nachweis bleibt historisch; dieses Paket nimmt die Quellenkorrekturen als neue Version auf.

<span lang="en">The linked base-package evidence remains historical; this package includes the source corrections as a new version.</span>

**Leserpfad / Reader path:** [Deutsch: Einstieg, Begriffe und Fehlerhilfe](docs/user-guide.md) · [English: start, terms and troubleshooting](docs/user-guide.EN.md).

Historisches Basispaket **v0.1.7**. Der [Release-Stand](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/releases) wird getrennt geprüft; ein Commit auf main ist noch keine Veröffentlichung oder Installation.

*Historical base package v0.1.7. The release page records actual publication separately; a main-branch commit is not publication or installation.*


Patch **0.1.7** ergänzt den fehlenden stabilen Command-zu-Rolle-Vertrag.
Validatoren, vier Gates und menschliche Entscheidungsrechte bleiben unverändert.
[Release-Nachweis / Release evidence](docs/release-0.1.7-validation.md).

*Patch 0.1.7 adds the missing stable command-to-role contract. Validators, the four
gates and human decision authority remain unchanged.*

Version: `0.1.8`

Status: Veröffentlichung über die Release-Seite prüfen / verify publication on the release page

Priorität: `15`

Spec Kit: `>=0.8.3`

Fachliche Voraussetzung: `security-governance >=0.6.1`

Lizenz: MIT

Dieses optionale Spec-Kit-Preset prüft, ob ein Projekt seine eigene
Secure-Development-Baseline nachvollziehbar an Review-Evidence bindet. Es
verknüpft Manifest, Richtlinie, zwölf Checklisten, generierten Sammelband,
mitgeltende Dokumente und vier getrennte Assurance-Gates. Das Preset besitzt
die Baseline nicht und verändert sie nicht.

*This optional Spec Kit preset validates whether a project binds its own
secure-development baseline to review evidence in a traceable way. It connects
the manifest, policy, twelve checklists, generated compendium, related
documents, and four separate assurance gates. The preset does not own or
modify the baseline.*

> **Wichtige Grenze:** Ein technisch erfolgreiches Ergebnis ist weder eine
> Zertifizierung noch eine Pilotfreigabe, Projektabnahme, Produktfreigabe,
> Sandbox-Freigabe oder allgemeine Flottenfreigabe.
>
> **Important boundary:** A technically successful result is not a
> certification, pilot authorization, project acceptance, product release,
> sandbox release, or general fleet release.

## Inhaltsübersicht / Contents

1. [Zweck und Einsatzbereich / Purpose and Scope](#assurance-section-1)
2. [Was das Preset prüft / What the Preset Validates](#assurance-section-2)
3. [Was das Preset nicht leistet / What It Does Not Do](#assurance-section-3)
4. [Zusammenspiel mit anderen Presets / Composition](#assurance-section-4)
5. [Installation / Installation](#assurance-section-5)
6. [Evidence-Verzeichnis und Lebenszyklus / Evidence Directory and Lifecycle](#assurance-section-6)
7. [Vier Assurance-Gates / Four Assurance Gates](#assurance-section-7)
8. [Statusmodell / Status Model](#assurance-section-8)
9. [Review- und Risikodaten / Review and Risk Metadata](#assurance-section-9)
10. [Baseline- und Hashbindung / Baseline and Hash Binding](#assurance-section-10)
11. [Befehle / Commands](#assurance-section-11)
12. [Betriebsarten / Modes](#assurance-section-12)
13. [Gate-Beispiel / Gate Example](#assurance-section-13)
14. [CI-Integration / CI Integration](#assurance-section-14)
15. [Bezug und Abgrenzung zu BSI C5 / BSI C5 Relationship and Boundary](#assurance-section-15)
16. [Menschliche Entscheidungsgrenzen / Human Decision Boundaries](#assurance-section-16)
17. [Sicherheit und Datenschutz / Security and Privacy](#assurance-section-17)
18. [Barrierefreiheit / Accessibility](#assurance-section-18)
19. [Plattformparität / Cross-Platform Parity](#assurance-section-19)
20. [Fehlersuche / Troubleshooting](#assurance-section-20)
21. [Praktische Erprobung / Field Evaluation](#assurance-section-21)
22. [Versionierung und Support / Versioning and Support](#assurance-section-22)

<a id="assurance-section-1"></a>

## 1. Zweck und Einsatzbereich / Purpose and Scope

Das Preset ist für Projekte gedacht, die bereits über eine projektgeführte
Secure-Development-Baseline verfügen und deren Konsistenz über längere
Entwicklungs- und Freigabezyklen nachweisen möchten. Typische Einsatzfelder
sind:

- regulierte oder sicherheitskritische Entwicklungsprojekte;
- Ausbildungs- und Referenzprojekte mit überprüfbarer Security-Governance;
- Sandbox- und Containerprojekte;
- Projekte mit formalen Baseline-, Delta-, Abschluss- und Image-Prüfungen;
- Teams, die technische Validierung von menschlicher Freigabe trennen müssen;
- Projekte, die Richtlinien- und Evidence-Drift früh erkennen wollen.

Das Preset eignet sich nicht nur für einen einmaligen Abschlusscheck. Es
unterstützt einen wiederholbaren Lebenszyklus: Baseline binden, Änderung
bewerten, Abschlusszustand dokumentieren und Auswirkungen auf Images oder
Ausführungsumgebungen getrennt prüfen.

*The preset is intended for projects that already maintain a project-owned
secure-development baseline and need to demonstrate its consistency across
development and release cycles. Common uses include regulated or
security-sensitive projects, training and reference repositories, sandbox and
container projects, formal baseline/delta/closure/image reviews, and teams that
must keep technical validation separate from human authorization.*

<a id="assurance-section-2"></a>

## 2. Was das Preset prüft / What the Preset Validates

Der Validator prüft:

- ein gültiges Baseline-Manifest unter dem Standardpfad
  `docs/secure-development/baseline-manifest.json` oder unter einem
  ausdrücklich gebundenen repository-relativen Alternativpfad;
- die wirksame Baseline-Version aus diesem Manifest;
- den normalisierten SHA-256 des Manifests;
- genau zwölf eindeutige Checklisten `CL-01` bis `CL-12`;
- Dokument-ID und deklarierte Version jeder Checkliste;
- Richtlinie, generierten Sammelband, mitgeltende Dokumente und Lernpfad;
- normalisierte SHA-256-Bindungen aller kontrollierten Textdokumente;
- die erwartete Anzahl eindeutiger Checklistenpunkte;
- die Übereinstimmung der Checklistenpunkt-IDs zwischen Einzeldateien und
  Sammelband;
- den projektgeführten Prüfpunkt `CL-02-13
  Cloud-Compliance-Assurance`;
- gültige Statuswerte und zulässige Kombinationen;
- vollständige Review-, Risiko- und Wiedervorlagedaten;
- vier unabhängige menschliche Entscheidungsgrenzen;
- die fachliche Voraussetzung `security-governance >=0.6.1`;
- neun ausdrückliche Image-Impact-Prüfpunkte;
- verbotene Zertifizierungs-, Konformitäts- und Testatbehauptungen in
  Gate-Evidence.

*The validator checks the manifest and its effective version, normalized
hashes, exactly twelve unique checklist IDs, controlled document versions,
checklist-item cardinality, compendium alignment, review and risk metadata,
the security-governance dependency, separate human decisions, all image-impact
fields, and the no-certification boundary.*

<a id="assurance-section-3"></a>

## 3. Was das Preset nicht leistet / What It Does Not Do

Das Preset:

- schreibt keine Richtlinie, Checkliste oder Baseline automatisch um;
- füllt keine fachliche Bewertung stellvertretend für das Projekt aus;
- führt keine SAST-, DAST-, Dependency-, SBOM- oder Penetrationstests aus;
- ersetzt kein Security-Review und keine Rechts- oder Prüfungsberatung;
- bewertet nicht die vollständige Kriterienmenge eines Standards;
- erteilt keine menschliche Freigabe;
- synchronisiert keine Evidence zwischen Repositories;
- startet keinen nachfolgenden Spec-Kit-Lauf;
- verändert im Statusmodus keine Dateien;
- eröffnet kein Community-Issue und erzeugt keinen Katalog-PR.

*The preset does not author policy, perform security scanners, complete
project assessments, replace professional review, evaluate an entire
assurance framework, grant human approval, synchronize repositories, or start
another Spec Kit workflow.*

<a id="assurance-section-4"></a>

## 4. Zusammenspiel mit anderen Presets / Composition

Das [Manifest](preset.yml) registriert zwei eigene Commands, den
`secure-development-evidence-contract` und den [Routing-Vertrag](model-routing.json), keine automatischen
Specify-/Plan-/Tasks-Wrapper. Installation erzeugt keine Gate-Dateien und
startet keinen Review. Die Evidence folgt dem Vertrag, ist aber vom Projekt
mit echten Quellen und Bewertungen zu erstellen. Die Validatoren lesen nur;
ein Agent darf ausschließlich ausdrücklich autorisierte Review-Evidence ändern.

*The [manifest](preset.yml) registers two standalone commands, the evidence
contract and the [routing contract](model-routing.json), not automatic Specify/Plan/Tasks wrappers. Installation creates no
gate files or review. The project supplies actual sources and assessments
under the contract. Validators only read; an agent may change only explicitly
authorized review evidence.*

Kleinere Prioritätszahlen haben höhere Auflösungspriorität; andere Presets
oder Projekt-Overrides können gleichnamige Vorlagen ersetzen. Eigene
Kombinationen separat prüfen. Priorität 15 ersetzt weder die Security-
Mindestversion noch ein Assurance-Gate oder eine menschliche Entscheidung.

*Lower numbers mean higher precedence; presets/overrides may replace same-name
templates. Check custom combinations separately. Priority 15 replaces neither
the Security minimum version, assurance gates nor human decisions.*

### Security Governance

`security-governance` definiert sichere Code-Erzeugung, Secure-SDLC-Nachweise,
Sprachregeln, Supply-Chain-Transparenz und regulatorische
Anwendbarkeitsprüfung. Secure Development Assurance verwendet diese
fachliche Grundlage und verlangt mindestens Version `0.6.1`.

### Architecture Governance

`architecture-governance` vertieft Trust Boundaries, Threat Modeling, Zero
Trust, Architekturentscheidungen und Cloud-Assurance. Dieses Preset prüft
dagegen, ob die projektgeführte Secure-Development-Baseline und die
dazugehörige Evidence konsistent gebunden sind.

### Stabiles Model-Routing / Stable model routing

Eine Routing-Rolle beschreibt die Art der Aufgabe, nicht ein bestimmtes Modell.
Ab v0.1.7 sind beide Commands im bestehenden Schema 1.0 ausdrücklich zugeordnet:

| Command | Rolle / Role | Aufgabe / Task |
|---|---|---|
| `speckit.secure-development-status` | `fast-mechanical` | Vorhandene Ergebnisse prüfen und erklären / Check and explain existing results |
| `speckit.secure-development-review` | `frontier-reasoning` | Ein benanntes Gate fachlich bewerten / Assess one named gate |

Für geroutete Agentenarbeit brauchst du zusätzlich
[Model Routing Governance](https://github.com/hindermath/spec-kit-preset-model-routing-governance)
und eine ausdrücklich eingerichtete lokale Modellbindung. Die Installation
dieses Presets richtet keine Modelle ein und startet keinen Agenten.
Bei einem autonomen Lauf wertet der bestehende Runner die Verträge aus.
Überlappende Rollen werden nach der stärksten Anforderung aufgelöst, nicht
nach der Installationspriorität. Eigene Kombinationen separat prüfen.
Fehlende Verträge oder unpassende Profile blockieren; es gibt keine stille Ersatzwahl.

Die direkten Bash-/PowerShell-Validatoren bleiben modellfreie Skriptprüfungen.
Sie sind auch ohne Routing-Preset nutzbar. Ein Agenten-Review kann Ergebnisse
fachlich bewerten und nur ausdrücklich erlaubte Review-Evidence bearbeiten;
ein grüner Skripttest ersetzt diese Bewertung und menschliche Freigaben nicht.
Dieser Patch ergänzt nur den stabilen Vertrag. Er enthält keine neue
dynamische Auswahl, kein Schema 3 und keine Zusage über Modellwechsel im laufenden Chat.

*A routing role describes the task, not a model name. The two commands use the
existing schema 1.0 and the roles shown above. Routed agent work also needs
Model Routing Governance and an explicitly configured local model binding.
Installing Assurance does not configure models or start an agent. The existing
autonomous runner reads these contracts and retains the strongest overlapping
role, independently of installation priority. Check custom combinations
separately. Missing contracts or unsuitable profiles block without a silent fallback.*

*Direct Bash/PowerShell validators remain model-free script checks and work
without the routing preset. An agent review can assess results and edit only
explicitly authorized review evidence. A successful script check replaces
neither that assessment nor human approval. This patch only completes the
stable contract: no new dynamic selection, schema 3 or live-chat model switching.*

### Empfohlene Reihenfolge

| Priorität | Preset | Rolle |
|---:|---|---|
| 10 | [security-governance](https://github.com/hindermath/spec-kit-preset-security-governance) | Sichere Entwicklung und Secure SDLC |
| 15 | [secure-development-assurance-governance](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance) | Baseline- und Evidence-Assurance |
| 20 | [architecture-governance](https://github.com/hindermath/spec-kit-preset-architecture-governance) | Sichere Architektur |
| 30–80 | weitere Governance-Presets | Architektur, A11Y, Intake und autonome Läufe |

Priorität steuert die Kompositionsreihenfolge. Sie erteilt keine
Ausführungs-, Merge- oder Freigaberechte.

*Security Governance supplies the technical secure-development foundation.
Architecture Governance supplies architecture depth. Priority 15 places this
preset between them without granting execution or approval authority.*

<a id="assurance-section-5"></a>

## 5. Installation / Installation

### Veröffentlichtes Tag-ZIP

~~~bash
specify preset add --from https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/archive/refs/tags/v0.1.10.zip --priority 15

specify preset info secure-development-assurance-governance
specify preset resolve secure-development-evidence-contract
specify check
~~~

Der v0.1.7-Befehl gilt erst nach gesonderter Veröffentlichung. Bis dahin ist
dies ein Release-Kandidat, kein bereits verfügbares Tag-ZIP.

*The v0.1.7 command applies only after separate publication. Until then this
is a release candidate, not an available tag archive.*

### Lokaler Entwicklungs-Checkout

~~~bash
specify preset add --dev /path/to/secure-development-assurance-governance --priority 15
~~~

### Voraussetzungen prüfen

~~~bash
specify preset info security-governance
specify preset info secure-development-assurance-governance
specify preset resolve secure-development-evidence-contract
~~~

Fehlt `security-governance` oder ist seine Version kleiner als `0.6.1`,
blockiert die Baseline-Prüfung. Das ist eine fachliche Schutzgrenze und kein
Installationsversuch durch den Validator.

*Install the versioned archive at priority 15. The validator fails closed when
Security Governance is absent or older than 0.6.1; it never installs the
dependency itself.*

<a id="assurance-section-6"></a>

## 6. Evidence-Verzeichnis und Lebenszyklus / Evidence Directory and Lifecycle

Ein Review-Kontext liegt standardmäßig unter:

~~~text
docs/security/secure-development/<YYYY-MM-DD>-<context-id>/
├── baseline.json
├── deltas/
│   └── <change-id>.json
├── closure.json
├── image-impact.json
└── evidence-matrix.md
~~~

`context-id` verwendet Kleinbuchstaben, Zahlen und Bindestriche. Ein Kontext
beschreibt genau einen nachvollziehbaren Assurance-Zusammenhang. Mehrere
unabhängige Projekte, Releases oder Pilotvorhaben erhalten getrennte
Kontexte.

Der Lebenszyklus ist:

1. **Baseline erfassen:** Projekt-Manifest, aktive Richtlinie und Checklisten
   sichten. Versionen, Hashes und den Review in `baseline.json` binden.
2. **Änderungen prüfen:** Für jede Änderung eine passende Datei unter `deltas/`
   führen. Auswirkungen, Prüfergebnisse und offene Punkte nachvollziehbar halten.
3. **Image Impact bewerten:** Build-, Container- und CI-Auswirkungen getrennt in
   `image-impact.json` prüfen. Ohne Image-Änderung die Pflichtfelder trotzdem
   bewerten; `N/A` braucht eine Begründung, es ersetzt keine Prüfung.
4. **Closure dokumentieren:** Ergebnisse und Restrisiken in `closure.json`
   zusammenführen. Technische Validierung, Pilotfreigabe, Projektabnahme und
   allgemeine Freigabe bleiben vier getrennte Entscheidungsstände. Technische
   Evidence erteilt keine Pilot-, Projekt- oder allgemeine Freigabe.
5. **Matrix abgleichen:** `evidence-matrix.md` mit den vier Gates abgleichen.
   Ein geänderter oder abgelaufener Nachweis verlangt eine gezielte neue Prüfung.
6. **Status lesen:** Den Statusbefehl für diesen Kontext ausführen. `Ready`
   beschreibt technische Evidence; der Status erteilt keine menschliche Freigabe.

*Lifecycle: (1) record the project baseline, active policy/checklists, versions,
hashes and review in baseline.json; (2) review each change in deltas/;
(3) assess build/container/CI impact in image-impact.json, including justified
N/A entries when no image changes; (4) combine results and residual risks in
closure.json while keeping technical validation, pilot authorization, project
acceptance and general release as four separate decision states. Technical
evidence grants none of those human approvals; (5) reconcile
evidence-matrix.md with the four gates and re-review changed/expired evidence;
(6) read the context status. Ready describes technical evidence, not approval.*

Diese Liste erklärt den Arbeitsablauf, keine neue Gate-Abhängigkeit.
Die verbindlichen Prüfungen stehen unter [Vier Assurance-Gates](#assurance-section-7).
*This list explains work, not a new gate dependency. The four-gate contract below
remains authoritative.*

*Each review context contains one baseline, one or more deltas, closure,
image-impact evidence, and a readable matrix. Separate projects and releases
use separate contexts.*

<a id="assurance-section-7"></a>

## 7. Vier Assurance-Gates / Four Assurance Gates

### Baseline

Das Baseline-Gate bindet das projektgeführte Manifest und alle darin
kontrollierten Textdokumente. Es beantwortet: „Welche Baseline wurde
tatsächlich geprüft?“

### Delta

Ein Delta beschreibt eine konkrete Änderung. Mehrere Delta-Dateien sind
erlaubt. Der Statusbefehl bildet daraus das strengste Ergebnis.

### Closure

Closure dokumentiert den technischen Abschluss, akzeptierte Restrisiken,
nächste Schritte und vier getrennte Entscheidungen:

- `technicalValidation`;
- `pilotAuthorization`;
- `projectAcceptance`;
- `generalRelease`.

### Image Impact

Image Impact ist auch dann erforderlich, wenn ein Projekt keine Image-Änderung
vornimmt. Nicht anwendbare Punkte werden ausdrücklich mit `N/A` dokumentiert.
Die neun Pflichtfelder sind:

- `build`;
- `compose`;
- `toolchain`;
- `ociDigest`;
- `sbom`;
- `secrets`;
- `mounts`;
- `network`;
- `ci`.

*The gates answer four different questions: which baseline was reviewed, what
changed, what can be closed, and what happened to the execution image or
environment.*

<a id="assurance-section-8"></a>

## 8. Statusmodell / Status Model

### Anwendbarkeit

| Wert | Bedeutung |
|---|---|
| `Applicable` | Der Prüfpunkt gilt für den Kontext. |
| `N/A` | Der Prüfpunkt gilt nicht; Begründung und Re-Evaluation-Trigger sind Pflicht. |
| `Open` | Die Anwendbarkeit ist noch nicht entschieden. |

### Umsetzung

| Wert | Bedeutung |
|---|---|
| `Fulfilled` | Die Anforderung ist mit Evidence erfüllt. |
| `Partly Fulfilled` | Ein Teil ist erfüllt; Restarbeit bleibt. |
| `Not Fulfilled` | Die Anforderung ist nicht erfüllt. |
| `Not Assessed` | Es liegt noch keine belastbare Bewertung vor. |

### Gate-Ergebnis

| Ergebnis | Bedeutung |
|---|---|
| `Ready` | Alle anwendbaren Pflichtpunkte sind erfüllt. |
| `ReadyWithAcceptedRisks` | Alle Blocker sind behandelt; mindestens ein vollständig dokumentiertes Restrisiko ist akzeptiert. |
| `NeedsRemediation` | Korrektur oder menschliche Entscheidung ist erforderlich. |
| `Blocked` | Der Vertrag, die Evidence oder eine Schutzgrenze verhindert die Fortsetzung. |

`Ready` ist unzulässig, wenn ein Punkt `Open` ist oder ein anwendbarer Punkt
nicht `Fulfilled` ist. `ReadyWithAcceptedRisks` benötigt mindestens einen
vollständigen Eintrag unter `acceptedRisks`.

*Applicability and implementation are independent axes. Ready is allowed only
when every applicable mandatory item is fulfilled.*

<a id="assurance-section-9"></a>

## 9. Review- und Risikodaten / Review and Risk Metadata

Jede Gate-Datei enthält ein `review`-Objekt:

~~~json
{
  "review": {
    "owner": "Named project owner",
    "reviewer": "Named technical reviewer",
    "reviewedAt": "2026-09-03T12:00:00+02:00",
    "reviewDue": "2027-09-03",
    "residualRisk": "Bounded residual risk or None identified.",
    "reevaluationTrigger": "Material baseline, scope, dependency, or release change."
  }
}
~~~

Offene oder nicht vollständig erfüllte Assessments benötigen zusätzlich
`nextAction`, `owner` und `dueAt`. `N/A` benötigt `reason` und
`reevaluationTrigger`.

Ein akzeptiertes Risiko enthält mindestens:

- eindeutige ID und Scope;
- Owner und Reviewer;
- Reviewdatum und Wiedervorlage;
- Begründung;
- Restrisiko;
- Re-Evaluation-Trigger.

Der Validator prüft Vollständigkeit und Ablaufdatum. Er entscheidet nicht, ob
die benannte Person tatsächlich berechtigt ist; diese Autorität muss das
Projekt außerhalb des Presets regeln.

*Every gate records owner, reviewer, review time, due date, residual risk, and
reevaluation trigger. The validator checks completeness and expiry but does
not invent organizational authority.*

<a id="assurance-section-10"></a>

## 10. Baseline- und Hashbindung / Baseline and Hash Binding

`baseline.json` enthält `baselineBinding`:

~~~json
{
  "baselineBinding": {
    "manifestPath": "docs/secure-development/baseline-manifest.json",
    "baselineVersion": "3.2.0",
    "manifestNormalizedSha256": "<64 lowercase hexadecimal characters>",
    "documentBindings": [
      {
        "path": "Richtlinie_Sichere-Entwicklung.md",
        "version": "3.2.0",
        "normalizedSha256": "<64 lowercase hexadecimal characters>"
      }
    ]
  }
}
~~~

`documentBindings` enthält exakt Richtlinie, Sammelband, zwölf Checklisten,
mitgeltende Dokumente und Lernpfade aus dem Manifest. Fehlende, zusätzliche
oder doppelte Bindungen blockieren.

Für Textdateien wird der Hash nach folgender Normalisierung berechnet:

1. [Zweck und Einsatzbereich / Purpose and Scope](#assurance-section-1)
2. [Was das Preset prüft / What the Preset Validates](#assurance-section-2)
3. [Was das Preset nicht leistet / What It Does Not Do](#assurance-section-3)
4. [Zusammenspiel mit anderen Presets / Composition](#assurance-section-4)
5. [Installation / Installation](#assurance-section-5)

Dadurch führen reine Zeilenenden-Unterschiede zwischen Windows, macOS und Linux
nicht zu falscher Drift. Inhaltliche Änderungen führen weiterhin zu Drift.

*The baseline binding records the manifest version and normalized hash plus one
binding for every controlled text document. UTF-8 BOM and line endings are
normalized before hashing.*

<a id="assurance-section-11"></a>

## 11. Befehle / Commands

### Read-only Status

~~~text
$speckit-secure-development-status [<evidence-dir>]
~~~

Ohne Parameter wird das lexikografisch neueste Verzeichnis unter
`docs/security/secure-development/` verwendet. Der Befehl:

- validiert den vollständigen Kontext;
- verändert keine Datei;
- berichtet jedes Gate;
- berechnet das strengste Gesamtergebnis;
- zeigt alle vier menschlichen Entscheidungen;
- gibt die exakt dokumentierte nächste Aktion aus.

Direkter Skriptaufruf unter macOS/Linux:

Vom Root des integrierten Zielprojekts; den Beispielkontext ersetzen.
Die folgenden Aufrufe prüfen nur vorhandene Evidence und erstellen sie nicht.

*Run from the integrated target project's root and replace the example context.
These calls validate existing evidence; they do not create it.*

~~~bash
bash .specify/presets/secure-development-assurance-governance/scripts/validate-secure-development-assurance.sh status docs/security/secure-development/2026-09-03-example
~~~

Direkter Skriptaufruf unter Windows:

~~~powershell
pwsh -NoProfile -File .specify/presets/secure-development-assurance-governance/scripts/validate-secure-development-assurance.ps1 -Action Status -EvidenceDirectory docs/security/secure-development/2026-09-03-example
~~~

### Begrenzter Review

~~~text
$speckit-secure-development-review <baseline|delta|closure|image-impact> <context-id> <training|mixed|development>
~~~

Der Review validiert genau das benannte Gate. Er darf nur ausdrücklich
autorisierte Review-Evidence im Kontext verändern. Die gelieferten Validatoren
arbeiten selbst read-only; eine Agentenoberfläche darf Änderungen nur im
Rahmen der ausdrücklich erteilten Evidence-Autorität vornehmen.

*Status validates and summarizes the complete context without writing.
Review validates one named gate and remains bounded to explicitly authorized
review evidence.*

Im gerouteten Agentenablauf gilt `fast-mechanical` für Status und
`frontier-reasoning` für Review. Direkte Validator-Aufrufe starten kein Modell.
Bei einem Routing-Blocker den Vertrag und die lokale Bindung mit der Betreuung
prüfen; weder Evidence ändern noch eine schwächere Rolle zum Bestehen wählen.

*Routed agent work uses fast-mechanical for status and frontier-reasoning for
review. Direct validator calls start no model. Ask the maintainer to check
the contract and local binding when routing blocks; do not alter evidence
or select a weaker role merely to pass.*

<a id="assurance-section-12"></a>

## 12. Betriebsarten / Modes

### training

Für Ausbildungs- und Übungskontexte. Ein Runbook ist Pflicht. Begriffe,
Entscheidungen und nächste Schritte müssen für Lernende verständlich sein.

### mixed

Für Kontexte mit Ausbildungs- und realen Entwicklungsanteilen. Ein Runbook ist
Pflicht. Lernziele und reale Freigabegrenzen bleiben sichtbar getrennt.

### development

Für reine Entwicklungsprojekte. Ein Runbook ist empfohlen. Fehlt es, muss die
Gate-Evidence `runbookApplicability: "N/A"` und eine belastbare
`runbookRationale` enthalten.

*Training and mixed modes require a runbook. Development requires a runbook or
an explicit, reasoned non-applicability decision.*

<a id="assurance-section-13"></a>
<a id="13-vollständiges-gate-beispiel--complete-gate-example"></a>

## 13. Gate-Beispiel / Gate Example

~~~json
{
  "schemaVersion": "1.0",
  "documentType": "SecureDevelopmentGateEvidence",
  "contextId": "example-release",
  "gate": "delta",
  "mode": "development",
  "createdAt": "2026-09-03T12:00:00+02:00",
  "review": {
    "owner": "Example project owner",
    "reviewer": "Example security reviewer",
    "reviewedAt": "2026-09-03T12:00:00+02:00",
    "reviewDue": "2027-09-03",
    "residualRisk": "None identified in the bounded delta.",
    "reevaluationTrigger": "Any material change after the reviewed commit."
  },
  "assessments": [
    {
      "id": "DELTA-001",
      "applicability": "Applicable",
      "implementation": "Fulfilled",
      "evidence": "docs/security/reviews/example.md"
    },
    {
      "id": "DELTA-002",
      "applicability": "N/A",
      "implementation": "Not Assessed",
      "reason": "The change does not modify an OCI image.",
      "reevaluationTrigger": "Any Dockerfile, compose, toolchain, or image change.",
      "evidence": "NoImageChange"
    }
  ],
  "outcome": "Ready",
  "externalComparisonBoundary": "HOSK/GWDG: ExternalComparison only; never local evidence"
}
~~~

Dieses Beispiel veranschaulicht eine Delta-Datei, keinen vollständigen
Assurance-Kontext. Runbook beziehungsweise begründete Nichtanwendbarkeit,
reale Evidence-Pfade und die übrigen drei Gates müssen nach dem Vertrag
ergänzt werden. Seine Aussagen sind keine bereits geprüften Projektergebnisse.

*This illustrates a delta file, not a complete assurance context. Supply its
runbook or justified non-applicability, real evidence paths and the other
three gates according to the contract. Its statements are not verified
project results.*

<a id="assurance-section-14"></a>

## 14. CI-Integration / CI Integration

Ein Bereitschaftslauf eignet sich als fail-closed CI-Gate. `Blocked` und
`NeedsRemediation` liefern einen Fehlercode; der lesende Statusbefehl bleibt
zur Diagnose verfügbar.

~~~yaml
- name: Validate secure-development assurance
  shell: bash
  run: |
    bash .specify/presets/secure-development-assurance-governance/scripts/validate-secure-development-assurance.sh \
      ready docs/security/secure-development/2026-09-03-example
~~~

Windows:

~~~yaml
- name: Validate secure-development assurance
  shell: pwsh
  run: |
    pwsh -NoProfile -File .specify/presets/secure-development-assurance-governance/scripts/validate-secure-development-assurance.ps1 -Action Status -RequireReady -EvidenceDirectory docs/security/secure-development/2026-09-03-example
~~~

Ein grüner CI-Job bedeutet nur, dass der technische Evidence-Vertrag erfüllt
ist. Er darf nicht automatisch `pilotAuthorization`,
`projectAcceptance` oder `generalRelease` auf `Fulfilled` setzen.

*CI can require a Ready or ReadyWithAcceptedRisks technical outcome. A green
job never grants a human authorization.*

<a id="assurance-section-15"></a>

## 15. Bezug und Abgrenzung zu BSI C5 / BSI C5 Relationship and Boundary

### Was enthalten ist

Die projektgeführte Secure-Development-Baseline enthält in Checkliste
`CL-02` den Prüfpunkt `CL-02-13 Cloud-Compliance-Assurance`. Er fordert bei
materieller Nutzung oder Bereitstellung von Cloud-Diensten eine nachvollziehbare
Entscheidung zu BSI C5 oder gleichwertiger Assurance-Evidence. Dabei werden
unter anderem Scope, Shared Responsibility, Unterauftragnehmer,
Kundenpflichten, Ersatznachweise und Restrisiken betrachtet.

Weitere Checklisten liefern C5-relevante Querschnittsnachweise, beispielsweise
zu:

- sicherer Softwarearchitektur;
- Kryptografie;
- Bedrohungsmodellierung;
- Lieferkette und Build-Integrität;
- Schwachstellenoffenlegung;
- Sicherheits-Code-Reviews;
- KI-gestützter Code-Erzeugung;
- sicherer Entwicklungsumgebung;
- Datenschutz;
- agentischen Sandbox-Grenzen.

Der Validator stellt sicher, dass `CL-02-13` in der gebundenen Baseline und im
gebundenen Sammelband vorhanden ist. Er schützt außerdem die zugehörigen
Dokumente gegen unbemerkte Versions- und Inhaltsdrift.

### Was nicht enthalten ist

Das Preset:

- bildet nicht den vollständigen BSI-C5-Kriterienkatalog ab;
- führt kein Mapping aller C5-Kriterien oder C5-DEV-Unterkriterien durch;
- bewertet kein C5-Testat eines Cloud-Anbieters;
- prüft keinen Prüfungszeitraum, Wirtschaftsprüferbericht oder
  Maßnahmenwirksamkeitsnachweis;
- ermittelt keine organisationsweite C5-Readiness;
- erklärt ein Projekt nicht für C5-konform;
- ersetzt keine Gap-Analyse oder Testatsvorbereitung.

Ein Ergebnis `Ready` bedeutet deshalb ausschließlich: Der definierte
Secure-Development-Evidence-Vertrag dieses Presets ist im geprüften Kontext
erfüllt. Es bedeutet nicht „C5-konform“, „C5-zertifiziert“,
„testatbereit“ oder „vollständig C5-geprüft“.

### Richtige Verwendung

Ein Projekt kann C5-relevante Evidence als Assessment referenzieren. Offene
C5-bezogene Fragen verhindern `Ready`, wenn sie als anwendbarer Pflichtpunkt
geführt werden. Die fachliche Vollständigkeit einer C5-Prüfung bleibt jedoch
Aufgabe eines getrennten, ausdrücklich beauftragten Assurance-Prozesses.

*The project-owned baseline includes the explicit CL-02-13
Cloud-Compliance-Assurance checkpoint and several cross-cutting secure
development controls. The preset validates that this checkpoint and its
controlled documents remain bound and consistent. It does not implement the
full BSI C5 catalogue, perform attestation review, or establish C5 readiness.*

<a id="assurance-section-16"></a>

## 16. Menschliche Entscheidungsgrenzen / Human Decision Boundaries

Die vier Closure-Entscheidungen sind nicht austauschbar:

1. [Zweck und Einsatzbereich / Purpose and Scope](#assurance-section-1)
2. [Was das Preset prüft / What the Preset Validates](#assurance-section-2)
3. [Was das Preset nicht leistet / What It Does Not Do](#assurance-section-3)
4. [Zusammenspiel mit anderen Presets / Composition](#assurance-section-4)

Technische Validierung darf die anderen drei Entscheidungen niemals
automatisch erfüllen. Sind sie offen, berichtet der Status sie als offen.
`NeedsRemediation` kann deshalb fachlich korrekt sein, obwohl alle technischen
Tests erfolgreich waren.

*Technical validation, pilot authorization, project acceptance, and general
release are four independent decisions. The first never implies the other
three.*

<a id="assurance-section-17"></a>

## 17. Sicherheit und Datenschutz / Security and Privacy

- Evidence darf keine echten Secrets, Tokens oder Passwörter enthalten.
- Personenbezogene Daten werden auf das erforderliche Rollen- und
  Verantwortungsmaß begrenzt.
- Absolute persönliche Dateipfade sind unzulässig; Pfade bleiben
  repository-relativ.
- Externe URLs dienen nur als Referenz und werden nicht automatisch geladen.
- Evidence-Inhalte werden nicht als Code ausgeführt.
- Unbekannte oder fehlende Werte blockieren fail-closed.
- HOSK/GWDG darf nur mit der exakten Grenze
  `ExternalComparison only; never local evidence` erscheinen.
- Das Preset verändert keine Git-, Remote-, Provider- oder
  Freigabeeinstellungen.

*Do not store secrets or unnecessary personal data in evidence. Paths remain
repository-relative, external references are not executed or fetched, and
unknown states fail closed.*

<a id="assurance-section-18"></a>

## 18. Barrierefreiheit / Accessibility

Alle wesentlichen Ergebnisse werden als Text ausgegeben. Farbe, Icons oder
Diagramme sind niemals alleiniger Informationsträger. Statusnamen werden
ausgeschrieben und in stabiler Reihenfolge berichtet. Dokumentation und
Beispiele sind für Tastatur, Screenreader, Braille-Zeile und Textbrowser
nutzbar. Die Sprache richtet sich an CEFR B2 und erklärt neue Fachbegriffe beim
ersten Gebrauch.

*All essential information is available as text. Color and graphics are never
the only signal. Stable labels, readable examples, and CEFR-B2 language
support keyboard, screen-reader, Braille, and text-browser use.*

<a id="assurance-section-19"></a>

## 19. Plattformparität / Cross-Platform Parity

Unter macOS und Linux wird das Bash-Skript verwendet. Unter Windows wird das
PowerShell-7-Skript verwendet. Beide Implementierungen teilen:

- Statuswerte;
- Pflichtfelder;
- Normalisierungsalgorithmus;
- Checklisten- und Dokumentprüfung;
- Ergebnispriorität;
- Schutzgrenzen;
- Exitcode `0` bei Erfolg und `2` bei Blockierung.

Die Tests verwenden LF, CRLF und UTF-8-BOM, um reine Plattformunterschiede von
inhaltlicher Drift zu unterscheiden.

*Bash and PowerShell implement the same contract and exit-code semantics.
Fixtures cover LF, CRLF, and UTF-8 BOM.*

<a id="assurance-section-20"></a>

## 20. Fehlersuche / Troubleshooting

### Manifest-Hashdrift

Das Manifest wurde nach der letzten Review-Bindung geändert. Inhalt prüfen,
neue Version oder begründete Änderung bestätigen und die Evidence mit neuem
normalisiertem Hash erneut reviewen. Niemals nur den Hash austauschen, ohne die
Änderung zu prüfen.

### Dokument-Hashdrift

Mindestens ein kontrolliertes Dokument weicht von seiner Bindung ab. Prüfe
Inhalt, Version, Generatorzustand und Review-Autorität.

### Sammelbanddrift

Die IDs im Sammelband entsprechen nicht den zwölf Einzelchecklisten oder der
gebundene Sammelbandhash ist veraltet. Erzeuge den Sammelband mit dem
projektgeführten Generator neu und prüfe den Diff.

### Security-Governance fehlt

Installiere oder aktualisiere `security-governance` ausdrücklich. Der
Validator nimmt keine automatische Installation vor.

### Review ist abgelaufen

`reviewDue` liegt vor dem aktuellen UTC-Datum. Führe einen neuen fachlichen
Review durch und dokumentiere Reviewer, Ergebnis, Restrisiko und neuen
Wiedervorlagetermin.

### Ready ist unzulässig

Mindestens ein Punkt ist `Open` oder ein anwendbarer Punkt ist nicht
`Fulfilled`. Korrigiere die Arbeit oder verwende das fachlich richtige
Ergebnis `NeedsRemediation` beziehungsweise `Blocked`.

### C5- oder Zertifizierungsbehauptung blockiert

Gate-Evidence enthält eine unzulässige pauschale Konformitäts-,
Zertifizierungs- oder Testatbehauptung. Ersetze sie durch eine begrenzte,
belegbare Aussage zum geprüften Evidence-Scope.

### Runbook fehlt

`training` und `mixed` benötigen immer ein Gate-spezifisches Runbook.
`development` benötigt ein Runbook oder ein begründetes `N/A`.

<a id="assurance-section-21"></a>

## 21. Praktische Erprobung / Field Evaluation

Vor einer späteren Community-Einreichung sollte das veröffentlichte Tag-ZIP in
mindestens einem kontrollierten Testprojekt erprobt werden. Der mitgelieferte
[Feldtest-Runbook](docs/field-test-runbook.md) verlangt:

- Installation aus dem unveränderlichen Tag-ZIP;
- einen read-only Statuslauf;
- je einen Review der vier Gates;
- einen vollständig positiven Kontext;
- einen bewusst blockierten Kontext;
- Prüfung der menschlichen Freigabegrenzen;
- eine anwendbare oder begründet nicht anwendbare
  Cloud-Compliance-Assurance-Entscheidung;
- Bash-/PowerShell-Parität;
- Zusammenspiel mit der vollständigen Governance-Matrix;
- dokumentierte Auffälligkeiten und Korrekturentscheidung.

Eine erforderliche Korrektur wird als neue Version veröffentlicht. Das Tag
`v0.1.0` wird niemals nachträglich verändert.

*Before any later Community Catalog submission, install the immutable release
archive in a controlled project and execute the supplied field-test runbook.
Corrections receive a new version; v0.1.0 is never rewritten.*

<a id="assurance-section-22"></a>

## 22. Versionierung und Support / Versioning and Support

### Kontextbindung und Risikotypen / Context Binding and Risk Types

Ab v0.1.3 muss ein Review genau ein Verzeichnis
`<YYYY-MM-DD>-<context-id>` finden. Die ID wird vollständig und mit
Groß-/Kleinschreibung verglichen: `foo` bezeichnet nicht `bar-foo`.
Mehrere datierte Verzeichnisse derselben ID blockieren; es wird nicht
stillschweigend das neueste gewählt. Die `contextId` und `mode` der
ausgewählten Gate-Evidence müssen exakt dem Auftrag entsprechen.
Bei Abweichung: Exitcode 2, keine Erfolgsmeldung, keine Evidence-Änderung.
Vor Wiederholung den gewünschten Kontext und Auftrag fachlich klären;
keine Evidence allein zum Bestehen des Validators umbenennen oder freigeben.

Wenn `acceptedRisks` vorhanden ist, muss es ein JSON-Array sein.
Ein einzelnes Risiko steht in `[{...}]`, nicht in `{...}`.
`null`, boolesche Werte, Zahlen und Zeichenketten sind unzulässig.
Ein leeres Array ist zulässig, jedoch nicht bei `ReadyWithAcceptedRisks`.
Fehlende optionale `acceptedRisks` bleiben bei anderen Ergebnissen zulässig.
Die bisherigen Pflichtfelder jedes Risikos bleiben gültig.
Jedes Risiko benötigt `scope`, `rationale` und ein gültiges, noch nicht
abgelaufenes `reviewDue`. Bei `ReadyWithAcceptedRisks` muss `assessmentIds`
jeden offenen oder nicht erfüllten Pflichtpunkt einem Risiko zuordnen.

*From v0.1.3, review requires exactly one dated directory with the full,
case-sensitive context ID. Suffix matches and duplicate dated contexts are
rejected. Evidence contextId and mode must exactly match the request.
Failure returns exit 2 without success output or evidence writes.
Clarify the intended context before retrying; never rename or approve evidence
merely to satisfy validation. When present, acceptedRisks must be a JSON
array; one risk uses [{...}], not {...}. Null, booleans, numbers and strings
are invalid. Empty arrays are allowed except for ReadyWithAcceptedRisks.
Other outcomes may omit the optional field. Existing risk metadata and
human decision boundaries remain valid.*
Each risk requires `scope`, `rationale`, and a valid, unexpired `reviewDue`.
For `ReadyWithAcceptedRisks`, `assessmentIds` must bind every open or
unfulfilled required assessment to an accepted risk.


`v0.1.2` korrigiert SDA-FT-003 an der gemeinsamen Risiko-Pruefgrenze fuer
Status und alle vier Gate-Reviews. `acceptedRisks.id` muss in beiden Shells
ein einzelner Text mit mindestens einem Nicht-Leerzeichen sein. Fehlende,
leere, nur aus Unicode-Leerzeichen bestehende und nicht skalare IDs blockieren
mit Exit 2; auch PowerShell darf eine einteilige Liste nicht in eine Text-ID
umdeuten. Der kanonische Feldname ist `id`. Gueltige Texte bleiben ohne
Kuerzung, Normalisierung oder neue Formatvorgabe erhalten. Die gemeinsame
Bash-Gate-Grenze akzeptiert genau eine JSON-Wurzel; verkettete JSON-Dokumente
blockieren ebenfalls mit Exit 2. Die bestehenden Releases `v0.1.0` und
`v0.1.1` werden nicht ueberschrieben.

*Version 0.1.2 fixes the shared accepted-risk ID boundary used by status and
all four reviews. Both shells require canonical `id` to be scalar, nonblank
text. Invalid IDs fail with exit 2; PowerShell must not unwrap a singleton
list into a valid ID. Valid strings remain unchanged without a new format
restriction. The shared Bash gate boundary accepts exactly one JSON root, so
concatenated documents also fail with exit 2. Preserve both earlier releases
and all human decision boundaries.*

`v0.1.1` korrigiert die installierten Validatorpfade der generierten
Agentenbefehle und macht den Read-only-Test deterministisch. Der Snapshot
vergleicht ordinal sortierte relative Pfade und rohe SHA-256-Dateihashes;
hinzugefügte, entfernte, umbenannte und versteckte Dateien bleiben sichtbar.
Der fachliche Normalisierungsvertrag und das Evidence-Schema ändern sich nicht.
Beim normalisierten Bash-Hash werden ausserdem die von Windows-`jq.exe`
erneut erzeugten CR-Zeichen entfernt, nachdem alle fachlichen Zeilenenden
bereits nach LF normalisiert wurden. Der rohe Read-only-Snapshot bleibt
davon getrennt und erkennt weiterhin jede Byte-Aenderung.
Alle Bash-JSON-Abfragen verwenden `--binary`, wenn jq diese Option kennt.
Aelteres POSIX-jq bleibt mit nachgewiesener LF-Ausgabe kompatibel. Eine alte
native Variante mit CRLF-Ausgabe und ohne Binary-Unterstuetzung blockiert
mit klarer Werkzeugdiagnose, statt Evidence falsch zu bewerten.

*Version 0.1.1 fixes generated command paths and deterministic read-only
snapshots without changing the evidence schema or semantic normalization.
The normalized Bash hash also removes CR bytes reintroduced by native Windows
jq after line normalization. Raw read-only snapshots still detect every byte
change. It is a field-test candidate, not a claim of project acceptance.*

*Every Bash JSON query uses binary output when supported. Older POSIX jq
remains compatible after an LF-output probe. A legacy native jq that changes
line endings without binary support fails with an explicit tool diagnostic,
never a fabricated evidence result.*

Vor Lieferung die folgenden Regressionen ausführen:

~~~powershell
pwsh -NoProfile -File tests/test-secure-development-assurance.ps1
pwsh -NoProfile -File tests/test-installed-surfaces.ps1
pwsh -NoProfile -File tests/test-release-documentation.ps1
~~~

Der zweite Test erzeugt ein isoliertes Projekt und prüft acht generierte
Status-/Review-Oberflächen unter Bash und PowerShell. `-ArchiveUrl` erlaubt
denselben Test gegen das veröffentlichte HTTPS-Tag-ZIP.

*The installation test executes all eight generated status/review surfaces.
Use its ArchiveUrl parameter to repeat the test against the published ZIP.*

Die Routing-Regression benötigt separat bereitgestellte Quellen von Model
Routing v0.1.5 und Autonomous Run v0.4.5; die exakten Commits stehen im
[v0.1.7-Prüfnachweis](docs/release-0.1.7-validation.md). Pfade ersetzen:

~~~powershell
pwsh -NoProfile -File tests/test-stable-routing.ps1 -ModelRoutingRoot ../model-routing -AutonomousRoot ../autonomous
~~~

Die neue CI stellt diese fest gebundenen Quellen bereit. Der Test lädt keine
Modelle und prüft nur Wegwerf-Fixtures, keine persönliche Konfiguration.

*The routing regression needs separately supplied Model Routing v0.1.5 and
Autonomous Run v0.4.5 sources at the exact commits in the validation record.
Replace the paths above. The new CI checks out those pinned sources. The test
uses disposable fixtures only, not models or personal configuration.*

`v0.1.0` ist das erste öffentliche Release und ausdrücklich für praktische
Erprobung vorgesehen. Semantische Versionierung gilt:

- Patch: kompatible Korrektur oder Dokumentationspräzisierung;
- Minor: rückwärtskompatible Erweiterung;
- Major: inkompatible Vertragsänderung.

Fehlerberichte sollen enthalten:

- Preset-Version;
- Spec-Kit-Version;
- Betriebssystem und Shell;
- verwendete Betriebsart;
- anonymisierte Fehlermeldung;
- kleinstmögliche reproduzierbare Evidence ohne Secrets;
- erwartetes und tatsächliches Verhalten.

Die Veröffentlichung im persönlichen Repository ist keine Aufnahme in den
GitHub-Spec-Kit-Community-Katalog. Eine Community-Einreichung erfolgt erst nach
erfolgreicher Felderprobung und einem getrennten ausdrücklichen Auftrag.

*Version 0.1.0 is the first public field-evaluation release. Public repository
availability does not imply inclusion in the GitHub Spec Kit Community
Catalog.*
