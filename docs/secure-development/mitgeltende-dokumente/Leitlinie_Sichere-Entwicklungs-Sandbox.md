# Leitlinie Sichere Entwicklungs-Sandbox / Secure Development Sandbox Guideline

**Stand / Date:** 2026-06-26
**Zielgruppe / Audience:** Fachinformatiker*innen in Ausbildung, Entwickler*innen, Reviewer und KI-Agenten / IT specialist apprentices, developers, reviewers, and AI agents
**Dokumenttyp / Document type:** Mitgeltendes Dokument zur Richtlinie Sichere Entwicklung / Related document for the Secure Development Guideline

## Zweck / Purpose

**DE:** Diese Leitlinie beschreibt, wie eine Entwicklungs-Sandbox fuer sichere Softwareentwicklung mit KI-Agenten aufgebaut, freigegeben und genutzt wird. Sie macht die Regeln aus der Richtlinie und aus `CL_12_Agentische-KI-Sandbox.md` praktisch anwendbar.

**EN:** This guideline describes how a development sandbox for secure software development with AI agents is built, approved, and used. It turns the rules from the guideline and from `CL_12_Agentische-KI-Sandbox.md` into practical guidance.

## Einordnung / Positioning

**DE:** Die zentrale Richtlinie legt die Pflicht fest: agentische KI darf nur in freigegebenen Sandbox-Umgebungen arbeiten. Diese Leitlinie beschreibt das wiederverwendbare Sandbox-Profil. Projektspezifische Nachweise bleiben im jeweiligen Projekt, typischerweise unter `docs/security/`.

**EN:** The central guideline defines the rule: agentic AI may work only in approved sandbox environments. This guideline describes the reusable sandbox profile. Project-specific evidence stays in the respective project, typically under `docs/security/`.

## Referenzprofil absdd-image-sandbox / absdd-image-sandbox Reference Profile

**DE:** `absdd-image-sandbox` ist das vorgesehene Referenzprofil fuer eine oeffentlichkeitsfaehige Ausbildungs-Sandbox. Sie soll Fachinformatik-Auszubildenden, Entwickler*innen und KI-Agenten eine reproduzierbare Lern- und Arbeitsumgebung fuer sichere Entwicklung bereitstellen.

**EN:** `absdd-image-sandbox` is the intended reference profile for a public-ready training sandbox. It should provide IT specialist apprentices, developers, and AI agents with a reproducible learning and work environment for secure development.

**DE:** Die Rolle im Gesamtmodell ist:

- `home-baseline` liefert Workspace-Struktur, Richtlinien, Checklisten, Presets und Intake-Dokumente.
- `absdd-image-sandbox` liefert die ausfuehrbare Sandbox-Umgebung.
- Level-2-Repositories liefern die konkreten Entwicklungs- und Haertungsziele.

**EN:** Its role in the overall model is:

- `home-baseline` provides workspace structure, guidelines, checklists, presets, and intake documents.
- `absdd-image-sandbox` provides the executable sandbox environment.
- Level-2 repositories provide the concrete development and hardening targets.

## Mindestanforderungen / Minimum Requirements

**DE:** Eine Entwicklungs-Sandbox gilt als geeignet, wenn mindestens die folgenden Punkte geprueft und dokumentiert sind:

| Bereich | Mindestanforderung |
|---|---|
| Laufzeit | Container, VM, MicroVM oder vergleichbare Isolation ist dokumentiert. |
| Berechtigungen | Die Sandbox laeuft nicht privilegiert; Root-Rechte sind auf Build- oder Setup-Schritte begrenzt. |
| Mounts | Host-Mounts sind explizit, begrenzt und mit Zweck, Pfad und Schreibrechten dokumentiert. |
| Secrets | Secrets liegen nicht im Repository und werden nicht in Prompts, Logs oder Screenshots uebernommen. |
| Netzwerk | Egress ist eingeschraenkt oder als Risikoentscheidung begruendet. |
| Toolchains | Entwicklungswerkzeuge sind versioniert oder nachvollziehbar installierbar. |
| KI-Agenten | Codex, OpenCode oder vergleichbare Werkzeuge haben dokumentierte Schreibgrenzen. |
| Supply Chain | Images, Paketquellen und Abhaengigkeiten sind nachvollziehbar; SBOM und Scan sind vorgesehen. |
| A11Y und Didaktik | Dokumentation ist textorientiert, DE/EN, CEFR B2 und WCAG 2.2 AA-orientiert. |
| Freigabe | Sandbox-Freigabe, Revalidierung und verantwortliche Person sind dokumentierbar. |

**EN:** A development sandbox is suitable when at least the following points are reviewed and documented:

| Area | Minimum requirement |
|---|---|
| Runtime | Container, VM, microVM, or comparable isolation is documented. |
| Permissions | The sandbox does not run privileged; root rights are limited to build or setup steps. |
| Mounts | Host mounts are explicit, limited, and documented with purpose, path, and write rights. |
| Secrets | Secrets are not stored in the repository and are not copied into prompts, logs, or screenshots. |
| Network | Egress is restricted or justified as a risk decision. |
| Toolchains | Development tools are versioned or installable in a traceable way. |
| AI agents | Codex, OpenCode, or comparable tools have documented write boundaries. |
| Supply chain | Images, package sources, and dependencies are traceable; SBOM and scan are planned. |
| A11Y and teaching | Documentation is text-oriented, DE/EN, CEFR B2, and WCAG 2.2 AA-oriented. |
| Approval | Sandbox approval, revalidation, and responsible person can be documented. |

## Memory-Safe-Language-Scope / Memory-Safe Language Scope

**DE:** Die Sandbox soll Entwicklung mit KI-Agenten fuer MSL-basierte Level-2-Projekte unterstuetzen. Die zentrale MSL-Liste umfasst: Rust, Swift, C#, F#, Java, Kotlin, Scala, Go, Dart, Python, Ruby, JavaScript, TypeScript, Haskell, OCaml, Erlang, Elixir, Ada und SPARK.

**EN:** The sandbox should support development with AI agents for MSL-based level-2 projects. The central MSL list includes: Rust, Swift, C#, F#, Java, Kotlin, Scala, Go, Dart, Python, Ruby, JavaScript, TypeScript, Haskell, OCaml, Erlang, Elixir, Ada, and SPARK.

**DE:** Nicht jede Sprache muss sofort als vollstaendige Toolchain im Image enthalten sein. Der aktuelle Status wird transparent dokumentiert:

- `Supported`: Toolchain, Build/Test und Grunddokumentation sind nutzbar.
- `Open`: Sprache ist Zielbild, aber Toolchain oder Nachweise fehlen noch.
- `N/A`: Sprache ist fuer den konkreten Sandbox-Stand bewusst nicht vorgesehen; die Begruendung ist dokumentiert.

**EN:** Not every language must immediately be present as a complete toolchain in the image. The current status is documented transparently:

- `Supported`: toolchain, build/test, and basic documentation are usable.
- `Open`: the language is part of the target picture, but toolchain or evidence is still missing.
- `N/A`: the language is intentionally not planned for the concrete sandbox state; the rationale is documented.

## Evidenzpfade / Evidence Paths

**DE:** Eine Sandbox-Instanz soll die folgenden Nachweise fuehren oder begruendet als `N/A` markieren:

- Sandbox-Freigabe mit technischem Identifikator, Ablaufdatum und Verantwortlichkeit.
- Isolationsnachweis fuer Container, VM oder vergleichbaren Mechanismus.
- Mount-Liste mit Host-Pfad, Sandbox-Pfad, Zweck und Schreibrechten.
- Netzwerkentscheidung oder Egress-Regel.
- KI-Werkzeug-Inventar mit Tool, Version, Provider-Status und Modell-/Backend-Bezug.
- SBOM- oder Scan-Nachweis fuer Image und wichtige Toolchains.
- MSL-Support-Matrix fuer die im Zielbild relevanten Sprachen.
- Spec-Kit-Preset-Liste und Nachweis, welche Presets fuer den Lauf gelten.
- Review- oder PR/MR-Nachweis fuer Aenderungen an der Sandbox.

**EN:** A sandbox instance should maintain the following evidence or mark it as `N/A` with rationale:

- Sandbox approval with technical identifier, expiry date, and responsibility.
- Isolation evidence for container, VM, or comparable mechanism.
- Mount list with host path, sandbox path, purpose, and write rights.
- Network decision or egress rule.
- AI tool inventory with tool, version, provider status, and model/backend reference.
- SBOM or scan evidence for the image and important toolchains.
- MSL support matrix for the languages relevant to the target picture.
- Spec Kit preset list and evidence of which presets apply to the run.
- Review or PR/MR evidence for sandbox changes.

## Bezug zu Checklisten und Presets / Relation to Checklists and Presets

| Bezug / Reference | Rolle / Role |
|---|---|
| `CL_10_Sichere-Entwicklungsumgebung.md` | Entwicklungsumgebung, Toolchains, reproduzierbare Arbeitsweise |
| `CL_12_Agentische-KI-Sandbox.md` | Agentische KI, Sandbox-Freigabe, Mounts, Netzwerk, Review |
| `CL_05_Lieferkette_und_Abhaengigkeiten.md` | SBOM, Abhaengigkeiten, Images, Paketquellen |
| `CL_09_KI-Codeerzeugung.md` | KI-Werkzeuge, KI-Code, AI-SBOM-Anwendbarkeit |
| `security-governance` | Secure Coding, SBOM, AI-SBOM, regulatorische Anwendbarkeit |
| `architecture-governance` | Schutzgrenzen, Cloud-/Provider-Abhaengigkeiten, C3A/C5 |
| `cross-platform-governance` | macOS/Linux/Windows-Paritaet und Skriptlaeufe |
| `agent-parity-governance` | Agenten-Dateien, Tool-Guidance und Prompt-Paritaet |
| `a11y-governance` | CEFR B2, WCAG 2.2 AA, didaktische Nachvollziehbarkeit |

## N/A-Regel / N/A Rule

**DE:** Nicht anwendbare Punkte duerfen nicht stillschweigend entfallen. Sie werden als `N/A` mit kurzer, konkreter Begruendung dokumentiert. Beispiel: Eine private Lern-Sandbox kann NIS2 oder DORA als `N/A` bewerten, wenn kein regulierter Dienst, kein Marktprodukt und keine regulierte Kundenrolle vorliegt.

**EN:** Non-applicable points must not disappear silently. They are documented as `N/A` with a short, concrete rationale. Example: a private learning sandbox may rate NIS2 or DORA as `N/A` when there is no regulated service, market product, or regulated customer role.

## Pflege / Maintenance

**DE:** Aenderungen an dieser Leitlinie muessen zusammen mit Richtlinie, `CL_12`, Sammelband, Verzahnungsdokument, README und betroffenen Lastenheften geprueft werden. Technische Sandbox-Haertung bleibt ein separater Spec-Kit-Lauf.

**EN:** Changes to this guideline must be reviewed together with the guideline, `CL_12`, compendium, alignment document, README, and affected Lastenhefte. Technical sandbox hardening remains a separate Spec Kit run.
