# Agentenregeln im Detail / Detailed agent guidance

Beginne mit [AGENTS.md](../AGENTS.md). Die folgenden Abschnitte gelten für die jeweils genannten Einstiege. Pfade in Beispielen beziehen sich auf den Repository-Stamm.

Start with [AGENTS.md](../AGENTS.md). The following sections apply to the named entry files. Paths in examples are relative to the repository root.

## Code Style Guidelines

Einstiege / Entry files: `AGENTS.md`

### Imports and Using Statements
- Use `using` directives at the top of files, grouped logically
- Place `using` directives inside namespace declarations when needed
- Follow C# convention for ordering: System namespaces first, then third-party, then internal

### Formatting and Naming Conventions
- Use PascalCase for all identifiers (classes, methods, properties, etc.)
- Use camelCase for local variables and parameters
- Use `var` for implicit typing when the type is obvious
- Prefer explicit type declarations for readability where needed
- Use `readonly record struct` for immutable value types
- Use `sealed class` for classes not meant to be inherited
- Use `public` keyword explicitly when needed (C# default is internal)
- Use `private` keyword for private members when needed for clarity

### Types and Structs
- Use `readonly record struct` for immutable value types (like TPoint, TRect)
- Use records for value objects that carry data
- Use classes for reference types that need inheritance or mutability
- Use enums for sets of named constants
- Use `enum with Flags` for bitwise combinations

### Error Handling
- Use exceptions for exceptional conditions
- Don't catch exceptions unless you can handle them meaningfully
- Provide meaningful exception messages
- Use `ArgumentNullException` for null parameter validation
- Consider using `ArgumentNullException.ThrowIfNull()` in .NET 6+

### Code Organization
- Group related methods and properties within classes
- Use partial classes for large files when appropriate
- Keep methods small and focused on single responsibilities
- Use meaningful names that describe what the code does
- Avoid magic numbers - use constants or enums instead
- Prefer composition over inheritance where possible
- Use interfaces for contracts and abstractions
- Use `static` for utility methods that don't depend on instance state
- Use `sealed` classes when inheritance is not intended

### JSON Handling
- Use `System.Text.Json` for project-owned JSON parsing and serialization
- Introduce `Newtonsoft.Json` only with documented justification and explicit review approval

### Documentation Guidelines
- Explanatory documentation blocks must be bilingual: German first, English second
- German and English documentation should target CEFR-B2 readability
- Large normative documents such as `Pflichtenheft*.md` and `Lastenheft*.md` may use a synchronized English sidecar with suffix `.EN.md` instead of an oversized inline-bilingual file; the German version remains canonical unless explicitly marked otherwise
- Follow the inclusion principle `Programmierung #include<everyone>`: guides, statistics, examples, and generated API documentation must remain usable in text-first assistive setups such as Braille displays, screen readers, and text browsers
- Generated HTML documentation should target WCAG 2.2 conformance level AA as the accessibility baseline
- Prefer semantic headings, lists, tables, and ASCII/text-first diagrams; do not encode essential meaning only through color, layout, or pointer-only affordances
- Treat bilingual CEFR-B2 delivery and the documented A11Y proof path as formal completion criteria for learner-facing documentation and active requirement artifacts
- Public APIs must include complete XML documentation (`summary`, `param`, `returns`, `exception` where needed)
- New or changed non-trivial logic must be reviewed for didactic inline-comment value when it affects learner understanding or maintainability, especially central framework flows and smoke-test helpers.
- Inline comments explain why a decision, trade-off, constraint, historical deviation, or proof boundary exists; do not add comments that merely restate obvious code.
- Keep inline-comment intensity moderate: normally 1-3 lines before a non-trivial block, with German-first/English-second CEFR-B2 text for didactic explanation blocks.
- Update documentation in the same change when API signatures or XML comments change

## Example Wave Delivery Pattern

Einstiege / Entry files: `AGENTS.md`

- Groessere verpflichtende Beispielwellen SOLLEN als bewusstes zweistufiges Spec-Kit-Liefermuster geplant werden, wenn Portierungslogik, Framework-Luecken und interaktive Runtime-Politur sonst in einem Feature vermischt wuerden.
- Stufe 1 ist der funktionale Portierungs- und Nachweis-Feature-Lauf: historische Beispielablaeufe portieren, Framework-Voraussetzungen schliessen, deterministische Headless- oder In-Process-Smoke-Pfade bereitstellen, Guides/Evidence ergaenzen und interaktive Runtime-Politur explizit als Follow-up markieren, wenn `dotnet run --project examples/<Name>` noch nicht die finale Demo zeigt.
- Stufe 2 ist der interaktive Showcase-Feature-Lauf: die bewiesenen Funktionen ueber sichtbare Menues, Statuszeilen, Desktop-Controls, Dialoge, Tastaturpfade und skriptbare UI-Event-Smoke-Tests erreichbar machen.
- Eine Beispielwelle gilt erst dann als vollstaendig lern- und reviewtauglich, wenn die interaktive Showcase-Stufe geliefert ist, sofern der Scope nicht ausdruecklich nur einen minimalen nicht-interaktiven Nachweis verlangt.
- Stage-1-PR-Evidence, Specs und Guides muessen den bewusst verschobenen Interaktionsumfang klar nennen; Stage-2-Specs sollen die vorhandenen Nachweisfunktionen wiederverwenden statt Verhalten neu zu portieren.
- Kuenftige Lastenhefte fuer Beispielwellen oder beispielnahe Framework-Vorhaertungen MUESSEN ein Framework-Usage- und Remediation-Gate enthalten. Der spaetere Spec-Kit-Lauf muss pro Beispiel oder Vertragsbereich dokumentieren, welche bestehende TuiVision-Framework-Komponente genutzt wird, ob lokale Sonderlogik existiert und welche Entscheidung gilt: `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder `FollowUpHardening`.
- Wiederverwendbare Logik darf nicht dauerhaft als lokale `examples/`-Sonderloesung verbleiben. Wenn dieselbe lokale Logik in mehreren Beispielen nuetzlich waere oder Framework-Verhalten ersetzt, gehoert sie in einen kleinen Framework-Fix oder in ein eigenes Follow-up-Hardening.

## Agentische Skriptausfuehrung / Agentic Script Execution

Einstiege / Entry files: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`

- Vor jeder Automationsaufgabe zuerst das Betriebssystem pruefen. Wenn ein passendes PowerShell-7-Skript oder Cmdlet vorhanden ist und `pwsh` verfuegbar ist, diese Variante bevorzugen. Fuer strukturierte lokale Automationen ist C# ueber `.NET` oder `mono` ein zulaessiger zweiter Weg, wenn Typisierung, Dateiformate oder Wiederverwendbarkeit dadurch klar besser werden. Erst wenn PowerShell oder C# nicht sinnvoll passen, die OS-nahe vorhandene Repo-Variante nutzen, auf macOS/Linux typischerweise Bash. Keine neue Sprache nur aus Bequemlichkeit einfuehren, wenn ein bestehendes Repo-Skript denselben Zweck erfuellt.
- Detect the operating system before each automation task. If a matching PowerShell 7 script or cmdlet exists and `pwsh` is available, prefer that variant. For structured local automation, C# via `.NET` or `mono` is an acceptable second option when type safety, file formats, or reuse clearly benefit from it. Only when PowerShell or C# is not a good fit, use the existing OS-native repository variant, typically Bash on macOS/Linux. Do not introduce a new language merely for convenience when an existing repository script already solves the task.

## Project Statistics

Einstiege / Entry files: `AGENTS.md`

- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Within the `## Fortschreibungsprotokoll` table, keep entries in strict chronological order: oldest entry at the top, newest and most recently added entry at the bottom; entries with the same date keep their insertion order.
- Keep a final top-level `## Gesamtstatistik` block as the last section of `docs/project-statistics.md`; do not place any later top-level section after it.
- Inside that final `## Gesamtstatistik` block, maintain compact ASCII-only trend diagrams that visualize at least the current artifact mix, the documented branch/phase development curves, the documented acceleration factors from agentic-AI plus Spec-Kit/SDD support, and a direct comparison between experienced-developer effort, Thorsten-solo effort, and the visible AI-assisted delivery window; keep those diagrams directly below the textual overall summary and refresh them together with the ledger.
- Keep each short CEFR-B2 explanation directly adjacent to its matching ASCII diagram group, ideally immediately before or after it, so apprentices do not have to scroll between explanation and diagram.
- When the data benefits from progression across an X-axis, add simple ASCII X/Y charts as a second visualization layer; keep them approximate, readable in plain Markdown, and explained in CEFR-B2 language.
- ASCII X/Y charts must use fixed-width X slots: every documented phase keeps its slot, missing values stay blank, and overly wide future series are split into labelled blocks such as `0..15`, `16..31`, and `32..47`, each with its own axis line and X labels.
- Keep the statistics section plain-text friendly for Braille displays, screen readers, and text browsers; the ASCII diagrams and their explanations must stay understandable without relying on color or visual layout alone.
- When DocFX content, documentation navigation, or API presentation changes, validate representative `_site/` pages through a text-oriented review path, preferably with a local Playwright accessibility snapshot.
- Keep the Playwright + `@axe-core/playwright` smoke tests in `tests/web-a11y/` runnable and aligned with the current DocFX structure; use `lynx` as an additional text-browser spot check when available.
- Treat every successful `docfx docfx.json` regeneration as requiring the matching `tests/web-a11y/` A11y smoke check in the same work item.
- Use WCAG 2.2 AA as the concrete review baseline for generated HTML documentation, especially for page language, bypass blocks, keyboard focus visibility, non-text contrast, and readable landmark structure.
- Each update must record the relevant branch/phase, observable work window, production/test/documentation line counts, main work packages, the conservative manual baseline of 80 code lines per day for an experienced developer, and the repo-specific Thorsten-Solo comparison baseline of 125 lines per workday for this Pascal/Turbo-Vision-derived port.
- When reporting acceleration, compare both manual references against visible Git active days and label the result as a blended repository speedup rather than a stopwatch measurement.
- When hour values are shown, convert the day-based estimates with the TVoeD working-day baseline of `7.8 hours` (`7h 48m`) per day.

## Workflow Platforms

Einstiege / Entry files: `AGENTS.md`, `.github/copilot-instructions.md`

- The Multi-Mac setup on `MacBook Air M2` and `Mac mini M4 Pro` is the primary development and day-to-day test workflow.
- Keep `gh`, `specify`, `codex`, `claude`, `copilot`, and `agy` installed on both Macs; before Spec-Kit work or Spec-Kit updates, run `specify check` to confirm the required toolchain is available.
- After every `/speckit-plan` run or equivalent plan refresh that changes active technologies, project structure, or agent context, run `.specify/scripts/bash/update-agent-context.sh` for `codex`, `claude`, `agy`, and `copilot` in the same work item by default. This repository treats that multi-agent context refresh as pre-approved maintenance and does not require a separate user prompt.
- Linux and Windows are additional compatibility-validation environments; on Windows, prefer WSL with a current Ubuntu release, currently `Ubuntu 24.04`.
- When changes affect runtime behavior, build reliability, terminal behavior, or portability, include Linux and Windows/WSL compatibility checks where practical and reflect them in CI or equivalent validation evidence when feasible.

## Gemeinsame Governance-Ergaenzung / Shared Governance Addendum

Einstiege / Entry files: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`

- Alle nutzerseitigen Artefakte muessen barrierefrei gedacht und geprueft werden: CLI-Ausgaben, Dokumentation, HTML, UI und generierte Templates; WCAG 2.2 Level AA ist die Standard-Basis, sobald die Kriterien auf das Artefakt anwendbar sind.
- All user-facing artefacts must be designed and reviewed for accessibility: CLI output, documentation, HTML, UI, and generated templates; WCAG 2.2 Level AA is the default baseline wherever the criteria apply.

- Fuer C#/.NET-Repositories gilt standardmaessig eine Thorsten-Solo-Basis von `125` Zeilen/Arbeitstag, sofern das Repo keinen abweichenden, begruendeten Wert dokumentiert.
- The default Thorsten-solo baseline for C#/.NET repositories is `125` lines/workday unless the repository documents a justified deviation.

## Shared Parent Guidance

Einstiege / Entry files: `AGENTS.md`

- The shared parent file `/Users/thorstenhindermann/RiderProjects/AGENTS.md` intentionally stores only repo-spanning baseline rules.
- Keep repository-specific build, test, workflow, architecture, and feature guidance in this repository's own files; when both layers exist, the repository-local files are the more specific authority.

---

## Memory-Safe Languages (MSL) / Speichersichere Sprachen

Einstiege / Entry files: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`

- Level-2-Projekte SOLLEN eine speichersichere Sprache (Memory-Safe Language, MSL) als primäre Laufzeit verwenden, wenn die Zielplattform es erlaubt.
- Verbindliche MSL-Erlaubnisliste, Regeln und Begründungspflicht: siehe `constitution.md`, Prinzip XI.
- MSL-Kurzliste: Rust, Swift, C#, F#, Java, Kotlin, Scala, Go, Dart, Python, Ruby, JavaScript, TypeScript, Haskell, OCaml, Erlang, Elixir, Ada, SPARK.
- **Nicht** MSL (Begründung im Level-2-`constitution.md` erforderlich): C, C++, klassisches Objective-C, Assembly, `cc65`-C89, Zig (pre-1.0), Nim (manual), D ohne GC.
- In Nicht-MSL-Repositories (z. B. `C64Projects/cc65`) die im Level-2-`constitution.md` hinterlegte Begründung im Plan- und Task-Kontext erwähnen.
- `speckit.constitution` und `speckit.specify` SOLLEN bei Nicht-MSL-Primärsprache einen **nicht blockierenden** Hinweis ausgeben (Tooling-Aufgabe, separate Umsetzung).
- Änderungen an dieser Empfehlung erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `.github/agents/copilot-instructions.md`.

*Level-2 projects SHOULD use a memory-safe language (MSL) as their primary runtime when the target platform allows. Authoritative rules: `constitution.md`, Principle XI. MSL short list: Rust, Swift, C#/F#, Java/Kotlin/Scala, Go, Dart, Python, Ruby, JavaScript/TypeScript, Haskell, OCaml, Erlang/Elixir, Ada/SPARK. Non-MSL languages (C, C++, Assembly, `cc65`, Zig pre-1.0, …) require a documented justification in the Level-2 `constitution.md`. In non-MSL repositories (e.g. `C64Projects/cc65`), surface the documented justification in plans and tasks. `speckit.constitution` and `speckit.specify` SHOULD emit a non-blocking advisory warning when the primary language is not an MSL — tracked as a separate tooling task. Changes to this recommendation require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*

## Sichere Code-Erzeugung / Secure Code Generation (ISO 27001/27002 A.8.28)

Einstiege / Entry files: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`

- KI-generierter und menschlich geschriebener Code MUSS den etablierten Secure-Coding-Best-Practices der Zielsprache und des Frameworks folgen. LLMs erzeugen nicht zuverlässig sicheren Code; explizite Durchsetzung ist erforderlich.
- Verbindliche Regeln und sprachspezifische Anforderungen: siehe `constitution.md`, Prinzip XII.
- Sprachspezifische Kurzregeln (Detailprofil: `.specify/templates/secure-coding-language-rules-template.md`):
  - **C / C89**: Bounds-Checking, kein `gets()`, kein ungeprueftes `sprintf()`/`strcpy()`, CERT C.
  - **C# / .NET**: parametrisierte Queries, Output-Encoding gegen XSS, Anti-Forgery-Tokens, sichere Deserialisierung, Microsoft Secure Coding Guidelines.
  - **Rust**: `unsafe` isolieren und begruenden, keine Panic-Pfade aus nicht vertrauenswuerdigem Input, Deserialisierung validieren, `cargo audit` oder gleichwertig verwenden.
  - **Go**: HTTP-/Client-Timeouts setzen, `context` propagieren, SSRF pruefen, `crypto/rand` nutzen, `govulncheck` oder gleichwertig verwenden.
  - **Swift**: keine Force-Unwraps auf nicht vertrauenswuerdigen Daten, dekodierte Eingaben validieren, Keychain/CryptoKit/TLS-Defaults nutzen, Datei-URLs einschraenken.
  - **Java / Kotlin**: DTOs validieren, Persistence-Zugriffe parametrisieren, Deserialisierung beschraenken, Auth/CSRF/CORS/Session-Defaults pruefen.
  - **Python**: Boundary-Input validieren, keine unsichere Deserialisierung oder dynamische Ausfuehrung, `subprocess`/Dateipfade einschraenken, Dependency-Audit nutzen.
  - **TypeScript / JavaScript**: Runtime-Input validieren, XSS/Prototype-Pollution/SSRF pruefen, keine dynamische Code-Ausfuehrung, Lockfiles auditieren.
  - **SQL**: nur parametrisierte Statements, kein dynamisches SQL aus nicht vertrauenswuerdigem Input.
  - **Bash**: Variable in Anfuehrungszeichen (`"$var"`), kein `eval` auf nicht vertrauenswuerdigem Input, `--` End-of-Options.
  - **PowerShell**: `Set-StrictMode -Version Latest`, validierte Parameter, kein `Invoke-Expression` auf nicht vertrauenswuerdigem Input.
- Kryptografie: aktuelle Algorithmen (AES-256, RSA >= 3072, SHA-256+, Ed25519); veraltete (MD5, SHA-1 für Signaturen, DES, RC4) nur mit expliziter Risikobegründung.
- Fehlerbehandlung darf keine internen Zustände, Stack-Traces oder Verbindungszeichenketten an Endbenutzer preisgeben.
- Hinzugefügte Abhängigkeiten müssen aktiv gepflegt sein und dürfen keine bekannten kritischen CVEs aufweisen.
- Code-Reviews MÜSSEN eine Sicherheitsperspektive für Eingabeverarbeitung, Authentifizierung, Autorisierung, Kryptografie und Datei-/Netzwerk-I/O enthalten.
- Änderungen an dieser Regel erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `.github/agents/copilot-instructions.md`.

*AI-generated and human-written code MUST follow the secure-coding best practices of the target language and framework. Authoritative rules: `constitution.md`, Principle XII, and `.specify/templates/secure-coding-language-rules-template.md`. Language-specific short rules cover C/C89, C#/.NET, Rust, Go, Swift, Java/Kotlin, Python, TypeScript/JavaScript, SQL, Bash, and PowerShell. MSL status does not replace secure API, I/O, auth, SQL, crypto, logging, or dependency review. Cryptography: use current algorithms (AES-256, SHA-256+, Ed25519); deprecated (MD5, SHA-1 for signatures, DES, RC4) only with explicit risk acknowledgement. Error handling must not expose internals. Dependencies must have no known critical CVEs. Code reviews must include a security perspective for input handling, auth, crypto, and I/O. Changes require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*

## Sichere Software-Architektur / Secure Software Architecture (ISO 27001/27002 A.8.27)

Einstiege / Entry files: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`

- KI-generierte und menschlich geschriebene Software-Architektur MUSS etablierten sicheren Architekturprinzipien folgen. Sicherer Code (Prinzip XII) ohne sichere Architektur reicht nicht aus — beide Ebenen müssen zusammenwirken.
- Verbindliche Regeln und sprachspezifische Architekturvorgaben: siehe `constitution.md`, Prinzip XIII.
- Verbindliche Architekturprinzipien:
  - **Trust Boundaries**: Explizite Vertrauensgrenzen definieren; alle Eingaben an Vertrauensgrenzen validieren und bereinigen.
  - **Defense in Depth**: Mindestens zwei unabhängige Sicherheitsschichten für kritische Assets.
  - **Least Privilege**: Jede Komponente, jeder Dienst und Prozess arbeitet mit minimalen Berechtigungen.
  - **Fail-Safe Defaults**: Zugriff standardmäßig verweigern, explizit gewähren; Fehlerpfade fallen in sicheren Zustand zurück.
  - **Angriffsfläche reduzieren**: Ungenutzte Endpunkte, Dienste und Debug-Funktionen deaktivieren oder entfernen.
  - **Separation of Concerns**: Authentifizierung, Autorisierung, Logging und Eingabevalidierung als Cross-Cutting Concerns implementieren, nicht ad-hoc verstreuen.
  - **Sichere Konfiguration**: Secrets in plattformgeeigneten Secret-Stores (z. B. Azure Key Vault, macOS Keychain), nie im Quellcode oder in Git-tracked Config-Dateien.
  - **Supply-Chain-Sicherheit**: Abhängigkeiten aus verifizierten Registries; Lock-Files committen; verwundbare Abhängigkeiten vor Release ersetzen.
- Änderungen an dieser Regel erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `.github/agents/copilot-instructions.md`.

*AI-generated and human-written software architecture MUST follow secure-architecture principles. Authoritative rules: `constitution.md`, Principle XIII. Core principles: trust boundaries (validate all input at system boundaries), defense in depth (at least two independent security layers), least privilege (minimum required permissions), fail-safe defaults (deny by default), attack surface reduction (disable unused features), separation of concerns (auth/logging/validation as cross-cutting concerns), secure configuration (secrets in secret stores, never in code or Git), supply-chain security (verified registries, lock files, no known-vulnerable dependencies). Principles XII + XIII together form the complete secure-development approach: XII = tactical code-level security, XIII = strategic architecture-level security. Changes require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*

## Allgemeine Architektur-Governance / General Architecture Governance (iSAQB / arc42)

Einstiege / Entry files: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`

- Architektur MUSS als explizites Arbeitsergebnis behandelt werden, wenn Struktur, Schnittstellen, Qualitätsattribute, Laufzeitverhalten, Deployment, Wartbarkeit oder technische Schulden betroffen sind.
- Verbindliche Regeln: siehe `constitution.md`, Prinzip XX; Architektur-Evidenz liegt standardmaessig unter `docs/architecture/`.
- Relevante Artefakte: Architecture Vision, Context View, Building-Block View, Runtime View, Deployment View, Quality Scenarios, Architecture Decision Records und Architecture Risks/Technical Debt.
- In `spec.md`, `plan.md` und `tasks.md` immer festhalten, ob Architektur-Evidenz erforderlich ist; `N/A` nur mit kurzer Begruendung.
- Bei sicherheitsrelevanter Architektur zusaetzlich die Secure-Architecture- und Security-Evidenz aus `docs/security/` anwenden.

*Architecture MUST be treated as an explicit work product when structure, interfaces, quality attributes, runtime behavior, deployment, maintainability, or technical debt are affected. Authoritative rules: `constitution.md`, Principle XX. Default evidence lives under `docs/architecture/`: architecture vision, context, building blocks, runtime, deployment, quality scenarios, ADRs, and architecture risks/technical debt. `spec.md`, `plan.md`, and `tasks.md` must state whether this evidence applies; `N/A` requires rationale. Security-relevant architecture also uses the secure-architecture evidence under `docs/security/`.*

## Agentischer Security-Workflow / Agentic Security Workflow

Einstiege / Entry files: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`

- In `spec.md`, `plan.md` und `tasks.md` die anwendbaren Standards samt Evidenzpfad festhalten.
- Bei Bedrohungsmodellen `STRIDE` als Basis und bei risikoreichen Flows zusätzlich relevante `CAPEC`-Patterns verwenden.
- Bei Web/API-Features den `ASVS`-Level und den Verifikationsumfang in `docs/security/` oder gleichwertiger Projektdokumentation ablegen.
- KI-Nutzung explizit klassifizieren: Entwicklungswerkzeug, keine KI im ausgelieferten/betriebenen System, oder KI-Runtime-/Produktkomponente; `AI-SBOM` entsprechend als `N/A` begründen oder in der Supply-Chain-Evidenz dokumentieren.
- Bei Release-/Artefakt-Arbeit `SBOM`, `AI-SBOM`, `VEX`, Provenance/SLSA-Nachweise und gegebenenfalls `OpenSSF Scorecard` in Release- oder Sicherheitsdokumentation einplanen.
- Bei Architekturänderungen `Zero Trust`-Anwendbarkeit und bei langlebigen Projekten `SAMM`-Folgeaktionen prüfen.
- Default-Evidenzpfad: `docs/security/asvs-verification.md`, `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, `docs/security/samm-assessment.md`; Abweichungen nur mit lokal dokumentierter Begründung.

*Capture the applicable standards and the evidence path in `spec.md`, `plan.md`, and `tasks.md`. Use `STRIDE` as the base for threat modeling and add relevant `CAPEC` patterns for the highest-risk flows. For web/API work, record the chosen `ASVS` level and verification scope in `docs/security/` or equivalent project documentation. Classify AI usage as development tooling, absent from the released/operated system, or AI runtime/product component; document `AI-SBOM` as `N/A` or as supply-chain evidence accordingly. For release and artefact work, plan `SBOM`, `AI-SBOM`, `VEX`, provenance/SLSA evidence, and `OpenSSF Scorecard` review where applicable. For architectural changes, evaluate `Zero Trust`; for long-lived projects, consider `OWASP SAMM` follow-up actions. The default evidence path is `docs/security/asvs-verification.md`, `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, and `docs/security/samm-assessment.md`, unless the repository documents a justified equivalent location.*

## Spec-Kit-Modell-Routing / Spec Kit Model Routing

Einstiege / Entry files: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`

- Modellwahl ist operative Agenten-Routing-Guidance, keine Feature-Anforderung. Modellnamen nicht in `spec.md`, `plan.md`, `tasks.md` oder einzelne Feature-Specs schreiben; diese Artefakte muessen reproduzierbar bleiben, auch wenn Modellnamen wechseln oder ein anderer KI-Agent verwendet wird.
- Der jeweilige Agent soll diese Empfehlungen auf seine aktuell verfuegbaren Modelle abbilden; keine feste Anbieter- oder Modellbindung ableiten.
- Fuer Spec-Kit-Spezifikation, Klaerung, Planung, Tasks und Analyse (`/speckit-specify`, `/speckit-clarify`, `/speckit-plan`, `/speckit-tasks`, `/speckit-analyze`; je nach Agent auch `/speckit.specify` usw.) das staerkste verfuegbare Frontier-Reasoning-/Coding-Modell bevorzugen.
- Fuer vollstaendige, lang laufende `/speckit-implement`-Laeufe das staerkste verfuegbare Long-Running-Agent-Modell bevorzugen; das Frontier-Modell nutzen, wenn maximale Urteilsguete wichtiger ist als Laufzeitstabilitaet.
- Fuer fokussierte Reviews oder CI-Fixes ein coding-optimiertes Modell bevorzugen.
- Fuer triviale Bereinigung, Formatierung oder risikoarme mechanische Edits ist ein schnelles kleines Coding-Modell akzeptabel.

*Model choice is operational agent-routing guidance, not a feature requirement. Do not pin model names in `spec.md`, `plan.md`, `tasks.md`, or individual feature specs; those artifacts must stay reproducible even when model names change or another AI agent is used. Each agent should map these recommendations to its currently available models; do not derive a fixed vendor or model requirement. For Spec-Kit specification, clarification, planning, task generation, and analysis (`/speckit-specify`, `/speckit-clarify`, `/speckit-plan`, `/speckit-tasks`, `/speckit-analyze`; or `/speckit.specify` etc. depending on the agent surface), prefer the strongest available frontier reasoning/coding model. For complete long-running `/speckit-implement` runs, prefer the strongest available long-running agent model; use the frontier model when maximum judgment quality is more important than runtime stability. For focused review or CI fixes, prefer a coding-optimized model. For trivial cleanup, formatting, or low-risk mechanical edits, a fast small coding model is acceptable.*

## Autonome Spec-Kit-Läufe / Autonomous Spec-Kit Runs

Einstiege / Entry files: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`

- Vollständig delegierte Spec-Kit-Läufe folgen `docs/spec-kit-autonomous-runbook.md` und verwenden den projektgebundenen Skill `$speckit-autonomous`.
- Vor dem Start muss der Delivery-Modus `LocalImplementation`, `PublishPR` oder `MergeAndSync` aus dem aktuellen Benutzerauftrag bestimmt werden. Allgemeine Autonomie erteilt keine stillschweigende Remote-Schreib- oder Merge-Berechtigung.
- Evidence wird vor der ersten Implementierungsänderung angelegt. Clarify, Checklists, Analyze, Implement und Remote Review werden bis zu den im Runbook definierten Konvergenzkriterien ausgeführt, nicht nach einer festen Wiederholungszahl.
- Ein repräsentativer vertikaler Slice mit Test und Proof kommt vor der breiten Wiederholung. Gemeinsame Evidence-, Versions-, Statistik-, Workflow- und Agent-Dateien bleiben Single-writer-Flächen.
- Jede Remote- oder Delivery-Task nennt den konkreten Repository-Evidence-Pfad für ihr Abnahmeergebnis; implizite Evidence-Verweise reichen für Analyze und Resume nicht aus.
- Aktuelle Check-/Review-Fakten werden vor dem Merge geprüft, aber in genau einen benannten Closeout-Evidence-Pfad verschoben, wenn ihr Commit den geprüften Feature-Head und damit die Aussage selbst entwerten würde.
- Jede manuelle Build-Zählererhöhung gilt für genau einen expliziten `dotnet build`- oder `dotnet test`-Aufruf; mehrere Aufrufe dürfen nicht hinter einer Erhöhung verkettet werden.
- Repository-Prüfhelfer erhalten den Repository-Root explizit. Ein Pass benötigt erwarteten Exitcode und einen Fehlerkanal ohne PowerShell-ErrorRecord, `command not found` oder gleichwertige fatale Signatur.
- Vor einem Commit wird der beabsichtigte Lieferkandidat separat gestaged und mit `git diff --cached --check` sowie einem Pfadvergleich gegen den Repository-Status geprüft; `git diff --check` allein erfasst keine ungetrackten Dateien, und fremde Änderungen bleiben ausgeschlossen.
- Vor einem Merge wird jedes Acceptance-Gate dem tatsächlichen Workflow, Job, Betriebssystem und ausgeführten Command zugeordnet; ein grüner Aggregatstatus oder ein Plattformname gilt nicht als Proof, wenn der Job den geforderten Runtime-, Plattform-, Dokumentations- oder Security-Nachweis nicht ausgeführt hat.
- Jeder Lauf schützt den akzeptierten Scope, verwendet triggerbasierte Validierung und dokumentiert eine kurze Retrospektive für spätere Runbook-Verfeinerungen.

*Fully delegated Spec-Kit runs follow `docs/spec-kit-autonomous-runbook.md` and use the repository-local `$speckit-autonomous` skill. Determine `LocalImplementation`, `PublishPR`, or `MergeAndSync` from the current user request; general autonomy does not silently grant remote write or merge authority. Create evidence before implementation, iterate optional stages to their defined convergence criteria, prove one representative vertical slice before broad rollout, serialize shared writers, require each remote or delivery task to name its exact repository evidence path, route self-invalidating reviewed-head facts to one named closeout path after verifying them before merge, validate the exact staged candidate with `git diff --cached --check` so new files are included without unrelated changes, map each acceptance gate to the actual workflow, job, platform, and executed command before merge, protect accepted scope, use trigger-based validation, and record a short retrospective for later workflow refinement.*

## Spec-Kit-Preset-Pflege / Spec Kit Preset Maintenance

Einstiege / Entry files: `AGENTS.md`

- Standard-Preset-Set: `security-governance` v0.7.5 prio 10, `architecture-governance` v0.6.6 prio 20, `isaqb-architecture-governance` v0.2.6 prio 30, `a11y-governance` v0.4.7 prio 40, `cross-platform-governance` v0.2.6 prio 50, `agent-parity-governance` v0.4.6 prio 60, `autonomous-run-governance` v0.4.12 prio 70, `parallel-autonomous-run-governance` v0.2.13 prio 80.
- Optionales Projektprofil: `intake-review-governance` v0.2.9 prio 65 ist in TuiVision aktiv. Es bindet den naechsten Intake vor Feature-Erstellung an ein aktuelles `Ready`- oder menschlich akzeptiertes `ReadyWithAcceptedRisks`-Ergebnis; Review und Status bleiben read-only, Repair benoetigt ausdrueckliche Aenderungsautoritaet. *Optional project profile: `intake-review-governance` v0.2.9 at priority 65 is active in TuiVision and hash-binds the next intake before feature creation.*
- `autonomous-run-governance` v0.4.12 prio 70 ist Teil der Standard-Achtermatrix. Ein vollständiger autonomer Lauf bleibt ausdrücklich delegationspflichtig; die Installation allein erteilt weder Ausführungsberechtigung noch Remote-, Merge-, Bypass- oder Provider-Rechte und `LocalImplementation` bleibt Default. Dokumentations-, Status-, Schema- oder Evidence-Änderungen gelten erst dann als testfrei, wenn keine ausführbaren Validatoren die geänderten Pfade, Marker, Schemas oder Zustandswerte konsumieren. Vor autorisierten Commits wird der exakt beabsichtigte Kandidat mit dem Delivery-Set-Validator im Staged-Modus einschließlich exakter Pfad- und Index-Hash-Bindung geprüft; fremde Änderungen bleiben unberührt. Vor einem Merge wird jeder Acceptance-Gate dem tatsächlich ausgeführten Workflow, Job, Runner beziehungsweise der Plattform und dem Befehl zugeordnet; grüne Namen oder ein Bypass ersetzen keinen technischen Nachweis. Bewusst pausierte Läufe werden als `PausedByUser` gespeichert und nur über `speckit.autonomous-resume` fortgesetzt; `speckit.autonomous-stop` wirkt kooperativ am nächsten sicheren Grenzpunkt, und ein gespeicherter Delivery-Modus ist keine aktuelle Berechtigung. Nach Preset- oder Governance-Drift werden neue zwingende Korrektheits-, Sicherheits-, Berechtigungs- und Evidenzregeln minimal mit akzeptierten Plan-, Task- und Checklist-Artefakten abgeglichen; reine Effizienzpräferenzen lösen keine rückwirkende Neugenerierung aus. Die lesbare Skill-Überschrift `Deliver` ist kein Run-State-Wert; für Remote-Closeout gelten ausschließlich `Publish`, `Review` oder `MergeAndSync`. Bei aktiver Intake-Policy prueft der Preflight das aktuelle hashgebundene Ergebnis vor der Feature-Erstellung; Resume prueft es nach Drift erneut.
- `parallel-autonomous-run-governance` v0.2.13 prio 80 ist Teil der Standard-Achtermatrix. Die Installation startet keine Kampagne und erteilt keine zusaetzlichen Remote-, Merge-, Bypass-, Abbruch-, Secret- oder Provider-Rechte. Kampagnen bleiben ausdruecklich delegationspflichtig, verwenden getrennte Worktrees und maximal drei gleichzeitig aktive Worker. Schema 1.1 erlaubt ein `runnerProfile` je Worker mit Kampagnen-Fallback; Modell und Reasoning-Stufe sind optionale, nicht geheime Metadaten und werden ohne Deklaration nicht erraten. Konsolidierung verlangt exakten Head, aktuelle Review- und Check-Evidenz, ist nach Teilmerges fortsetzbar und setzt `Completed` erst nach Synchronisation, manifestdeklarierten idempotenten Post-Merge-Aktionen und Abschlussvalidierung. Schema 1.2 kann vor Worktree-Erstellung einen aktuellen Campaign-Review mit eindeutiger Intake-, Worker-, DAG- und Exception-Coverage verlangen; Resume prueft den Ergebnis-Hash erneut.
- `a11y-governance` v0.4.7 ergänzt didaktische Inline-Code-Kommentar-Governance für neue oder geänderte nicht-triviale Logik.
- `security-governance` v0.7.5 fuehrt `AI-SBOM` weiter als bedingt anwendbare Supply-Chain-Evidenz, ergänzt sprachspezifische Secure-Coding-Profile und ergänzt regulatorische Anwendbarkeit für NIS2, CRA, EU AI Act und DORA. Reine Entwicklungswerkzeug-Nutzung bleibt `N/A`; KI-Runtime-/Produktkomponenten benoetigen Evidenz nach G7/BSI AI-SBOM-Clustern; private Ausbildungsprojekte dokumentieren regulatorische Nichtanwendbarkeit mit kurzer Begründung.
- `architecture-governance` v0.6.6 ergänzt `BSI C3A` als bedingte Cloud-Autonomie-Evidenz und `BSI C5` als bedingte Cloud-Compliance-Assurance-Evidenz für Cloud-Service-Auswahl, Provider-Abhängigkeiten, Audit-/Nachweisstand, Shared Responsibility und Betriebsnachweise.
- Alle acht Presets enthalten ab diesem Release-Block audit-ready Spec-Kit-Run-Evidenz: `Applicable` / `N/A` / `Open`, Begründung, Evidenzpfad, Reviewer, Restrisiko und Follow-up muessen im aktuellen Spec-Kit-Lauf dokumentiert werden.
- Die ursprünglichen sechs Presets sind seit 2026-05-04 und `autonomous-run-governance` v0.2.2 ist seit 2026-07-17 im `github/spec-kit` Community-Katalog enthalten und liegen zusätzlich als veröffentlichte Repos unter `https://github.com/hindermath/spec-kit-preset-*`.
- `parallel-autonomous-run-governance` v0.2.13 ist eigenstaendig veroeffentlicht; v0.2.2 wurde mit `github/spec-kit#3591` fuer den Community-Katalog eingereicht.
- Registrierte Level-0-, Level-1- und Level-2-Repositories installieren bei vorhandener Spec-Kit-Integration standardmäßig alle acht Presets aus `scripts/config/spec-kit-governance-presets.json`, sofern keine begründete Ausnahme dokumentiert ist.
- Referenz-Rollout für alle acht Presets: `RiderProjects/TinyPl0`, `RiderProjects/TinyCalc`, `RiderProjects/TuiVision`, `RiderProjects/InventarWorkerService`.
- Installation erfolgt bevorzugt mit `install-spec-kit-governance-presets.*` aus der zentralen Matrix; die Skriptlogik enthaelt keine fest eingebauten Versionen. Bei neuen Preset-Releases zuerst die Matrix aktualisieren, dann bestehende Repos bewusst mit `--force` / `-Force` nachziehen.
- Flotten-Rollouts erfassen Level-0, Level-1 und Level-2 explizit. Eine reine Level-2-Registry beweist keine vollstaendige Abdeckung; jeder Zielstatus wird bis Installation, exakter Matrixvalidierung, Commit, Push und Remote-Synchronisation verfolgt.
- Vor dem Staging werden generierte Preset-/Agentenpfade mit dem gesamten Arbeitsbaum abgeglichen. Fremde Aenderungen bleiben unberuehrt; bei Konflikten wird ein sauberer Worktree statt eines erzwungenen Misch-Commits verwendet.
- Aktuelle normative Sechs-/Siebenerangaben werden auf die Achtermatrix migriert. Historische Statistik-, Changelog-, Feldnachweis- und Kompatibilitaetsangaben bleiben erhalten und werden durch einen dokumentierten Allowlist-Scan unterschieden.
- Provider-/Billing-Ablehnung, technischer Gate-Fehler und bestandener Gate sind getrennte Ergebnisse. Bypass oder gruene Sammelnamen ersetzen keinen exakten technischen Nachweis.
- `.specify/presets/` und generierte Agenten-/Command-Dateien committen, wenn Presets Projekt-Policy sind; `.specify/presets/.cache/` nie committen.
- Nach Installation oder Update prüfen: `specify preset list`, mindestens ein `specify preset info <id>`, bei Template-Fragen zusätzlich `specify preset resolve <template>`.
- Die veröffentlichten eigenständigen GitHub-Preset-Repositories sind alleinige Produktquellen. Lokale Checkouts unter `~/SpecKitPresetProjects/` dienen ihrer Entwicklung; installierte Pakete sind Integrationskopien.
- Verbesserungen direkt im passenden eigenständigen Preset-Repository entwickeln, prüfen und veröffentlichen. Ein unveränderliches Release-Paket wird nur nach ausdrücklicher Freigabe hier integriert; keine lokalen Produkt-Forks oder Scaffold-Doppelpflege.
- Bei Änderungen an Preset-Regeln immer prüfen, ob `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `scripts/templates/*` ebenfalls aktualisiert werden müssen.
- Bei jeder Preset-Version oder Prioritätsänderung zuerst `scripts/config/spec-kit-governance-presets.json` aktualisieren und danach README-Tabellen, Constitution, Agenten-Dateien, `scripts/templates/speckit-workflow-section.md` und Agenten-Templates gemeinsam prüfen.

*Fleet rollouts explicitly cover level 0, level 1, and level 2 and track each
target through installation, exact matrix validation, commit, push, and remote
synchronization. Separate generated paths from unrelated work before staging.
Migrate current normative six/seven references while preserving allowlisted
history and compatibility aliases. Provider refusal, technical gate failure,
and passing evidence are distinct; bypass is not technical proof.*
- Community-/Katalog-Abstimmung läuft über `github/spec-kit#2362`.

*Standard preset set: `security-governance` v0.7.5 prio 10, `architecture-governance` v0.6.6 prio 20, `isaqb-architecture-governance` v0.2.6 prio 30, `a11y-governance` v0.4.7 prio 40, `cross-platform-governance` v0.2.6 prio 50, `agent-parity-governance` v0.4.6 prio 60, `autonomous-run-governance` v0.4.12 prio 70, and `parallel-autonomous-run-governance` v0.2.13 prio 80. `a11y-governance` v0.4.7 adds didactic inline-code-comment governance for new or changed non-trivial logic. `architecture-governance` v0.6.6 adds conditional `BSI C3A` cloud-autonomy evidence and `BSI C5` cloud-compliance assurance evidence for cloud-service selection, provider dependencies, audit/assurance status, shared responsibility, and operational evidence. `security-governance` v0.7.5 keeps conditional `AI-SBOM` evidence, language-specific secure-coding profiles, and regulatory applicability screening for NIS2, CRA, EU AI Act, and DORA: development-tool-only AI usage is `N/A`, AI runtime/product components require G7/BSI AI-SBOM cluster evidence, and private training projects record regulatory `N/A` when no regulated scope exists. All eight presets now include audit-ready Spec-Kit run evidence: `Applicable` / `N/A` / `Open`, rationale, evidence path, reviewer, residual risk, and follow-up must be documented for the current Spec-Kit run. The original six presets have been in the `github/spec-kit` community catalog since 2026-05-04, and `autonomous-run-governance` v0.2.2 was verified there on 2026-07-17. All eight are also published under `https://github.com/hindermath/spec-kit-preset-*`. `parallel-autonomous-run-governance` v0.2.13 was submitted to the community catalog as `github/spec-kit#3591`. Registered level-0, level-1, and level-2 repositories with Spec Kit default to all eight presets from `scripts/config/spec-kit-governance-presets.json` unless a justified exception is documented. Use `install-spec-kit-governance-presets.*` so preset versions stay centralized in the matrix. Commit `.specify/presets/` and generated agent command updates when presets are project policy, but never commit `.specify/presets/.cache/`. Verify installs with `specify preset list`, `specify preset info`, and where relevant `specify preset resolve`. Develop and release presets in their standalone GitHub repositories, the sole product sources. Install immutable release packages only with explicit authority; do not maintain local product forks or duplicate scaffolds. Preset-rule changes and preset version/priority changes require reviewing the central matrix, constitution, README tables/install snippets, all agent guidance files, and relevant templates together. Community/catalog coordination happens in `github/spec-kit#2362`.*

## Hinweise / Notes

Einstiege / Entry files: `AGENTS.md`, `GEMINI.md`

- Diese Datei bleibt bewusst kompakt und ergänzt die projektspezifische Dokumentation.
- This file intentionally stays compact and complements the project-specific documentation.

## Project Overview

Einstiege / Entry files: `CLAUDE.md`

TuiVision is a C#/.NET 10 port of the Turbo Vision 2.0.3 TUI framework (originally C/C++). The original source lives in `tv203s/` as the conceptual reference; the C# port is a managed, modernized interpretation — not a line-for-line translation.

## Architecture

Einstiege / Entry files: `CLAUDE.md`

### Module Structure

| Module | Purpose |
|---|---|
| `src/TuiVision.Core` | Foundation types: `TPoint`, `TRect`, `TEvent`, `TObject` |
| `src/TuiVision.Controls` | UI components: `TView` (base for all visual elements) |
| `src/TuiVision.Drivers.Console` | Console rendering: `TConsoleBuffer`, `TConsoleDriver`, `IConsolePresenter` |
| `src/TuiVision.Serialization` | Binary archive system with polymorphic `TRecordRegistry` |
| `src/TuiVision.Compatibility` | Key code translation from .NET keys to Turbo Vision scan codes |

### Key Design Patterns

- **Event system**: `TEvent` uses static factory methods (`TEvent.CreateKeyDown(...)`) and `readonly record struct` payloads. Events are dispatched via `TView.HandleEvent()`.
- **Coordinate system**: `TRect` uses inclusive top-left, exclusive bottom-right bounds. Views maintain local coordinates; `MakeLocal`/`MakeGlobal` convert between coordinate spaces.
- **Console rendering**: Presenter pattern — `TConsoleDriver` manages a back-buffer and publishes snapshots via `IConsolePresenter`, keeping rendering backends swappable.
- **Serialization**: Envelope-based binary format (type ID + payload). Types register themselves with `TRecordRegistry` for polymorphic deserialization.
- **Lifecycle**: `TObject.ShutDown()` for logical teardown + `IDisposable` for resource cleanup.

### Global Build Settings (`Directory.Build.props`)

All projects share: `net10.0`, `LangVersion: latest`, `Nullable: enable`, `ImplicitUsings: enable`.

**Repo Version Scheme** — `Directory.Build.props` carries the repo-wide `Version`, `AssemblyVersion`, and `FileVersion` values for all projects and follows `Major.Minor.Patch.Build`:
- `Minor` = current Spec-Kit feature/branch number, interpreted numerically as the canonical PR number for versioning (`007` -> `7`) and used immediately even before a GitHub PR exists
- `Patch` = current commit count in that feature/PR branch (after committing the current change)
- `Build` = manual build counter incremented before every `dotnet build` or `dotnet test`

Align the three version fields in `Directory.Build.props` whenever a commit is created or the branch is updated on a numbered Spec-Kit branch, before pushing.

### Testing

Tests use MSTest. Test projects mirror source projects (e.g., `TuiVision.Core.Tests` → `TuiVision.Core`). `TuiVision.Examples.SmokeTests` is for integration-level tests of ported example programs.

**Coverage Gate (SC-003)**: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` must each achieve ≥ 70 % Line Coverage (Pflichtenheft §9.4 Nr. 1). Measure from the repository root with Coverlet (`coverlet.collector`): `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`. `coverlet.runsettings` is the canonical TuiVision coverage-gate configuration and MUST be kept in sync when gate-relevant assemblies, example assemblies, or test projects are added, renamed, or removed. Validate it with `xmllint --noout coverlet.runsettings` where available before relying on the coverage gate. Do not merge to `main` without passing this gate.

### Documentation

- Use `System.Text.Json` for project-owned JSON parsing and serialization.
- Introduce `Newtonsoft.Json` only with documented justification and explicit
  reviewer approval.
- Explanatory documentation must be bilingual (German first, English second) with CEFR-B2 readability.
- Large normative documents such as `Pflichtenheft*.md` and `Lastenheft*.md` may use a synchronized English sidecar with suffix `.EN.md` instead of an oversized inline-bilingual file; the German version remains canonical unless explicitly marked otherwise.
- **`Programmierung #include<everyone>`** — Diese Lernbeispiele richten sich an Azubis (Fachinformatiker AE/SI) mit Deutsch und Englisch als Arbeitssprachen sowie an sehbehinderte Lernende, die mit Braille-Displays, Screen-Readern oder Textbrowsern arbeiten. Barrierefreiheit ist kein Nice-to-have, sondern Pflichtanforderung. Guides, statistics, examples, and generated API documentation must remain usable in text-first assistive setups such as Braille displays, screen readers, and text browsers.
- Generated HTML documentation should target WCAG 2.2 conformance level AA as the accessibility baseline.
- Prefer semantic headings, lists, tables, and ASCII/text-first diagrams; do not encode essential meaning only through color, layout, or pointer-only affordances.
- Treat bilingual CEFR-B2 delivery and the documented A11Y proof path as formal completion criteria for learner-facing documentation and active requirement artifacts.
- Public API changes must include complete XML documentation updates.
- New or changed non-trivial logic must be reviewed for didactic inline-comment value when it affects learner understanding or maintainability, especially central framework flows and smoke-test helpers.
- Inline comments explain why a decision, trade-off, constraint, historical deviation, or proof boundary exists; do not add comments that merely restate obvious code.
- Keep inline-comment intensity moderate: normally 1-3 lines before a non-trivial block, with German-first/English-second CEFR-B2 text for didactic explanation blocks.
- Run `docfx docfx.json` when root config exists and API/XML docs changed.
- Keep the Playwright + `@axe-core/playwright` smoke tests in `tests/web-a11y/` aligned with the current DocFX structure and representative pages; use `lynx` as an additional text-browser spot check when available.
- Treat every successful `docfx docfx.json` regeneration as incomplete until the matching `tests/web-a11y/` A11y smoke check has also passed in the same work item.
- GitHub Pages is published from `.github/workflows/pages.yml`: build root `docfx.json`, run the `tests/web-a11y/` Playwright + axe smoke path, upload `_site/` as a Pages artifact, and keep `_site/` plus generated `api/*.yml` files out of Git.

### Reference Source

`tv203s/contrib/tvision/` contains the original Turbo Vision 2.0.3 C/C++ source. Consult it when porting new classes or understanding original behavior. Do not modify files in `tv203s/`.

## Example Wave Delivery Pattern

Einstiege / Entry files: `CLAUDE.md`

- Larger mandatory example waves should use a deliberate two-stage Spec-Kit delivery model when framework gaps, porting logic, and interactive runtime polish would otherwise be mixed in one feature.
- Stage 1 is the functional port/proof feature: port historical example behavior, close framework prerequisites, expose deterministic headless or in-process smoke paths, add guides/evidence, and explicitly mark interactive runtime polish as follow-up when normal `dotnet run --project examples/<Name>` is not yet the final demonstration.
- Stage 2 is the interactive showcase feature: wire the proven functions into visible menus, status lines, desktop controls, dialogs, keyboard paths, and scripted UI-event smoke tests so the example is useful for learners and manual reviewers.
- Do not call an example wave fully learner-facing complete until Stage 2 is delivered, unless the scope explicitly defines the example as a minimal non-interactive proof.
- Future Lastenhefte for example waves or example-adjacent framework hardening MUST include a framework-usage and remediation gate. The later Spec-Kit run must document, per example or contract area, which existing TuiVision framework component is used, whether local special logic exists, and which decision applies: `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, or `FollowUpHardening`.
- Reusable logic must not remain permanently as a local `examples/` special solution. If the same local logic would be useful in multiple examples or replaces framework behavior, move it into a small framework fix or record it as a dedicated follow-up hardening item.

## Project Statistics

Einstiege / Entry files: `CLAUDE.md`

- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Within the `## Fortschreibungsprotokoll` table, keep entries in strict chronological order: oldest entry at the top, newest and most recently added entry at the bottom; entries with the same date keep their insertion order.
- Keep a final top-level `## Gesamtstatistik` block as the last section of `docs/project-statistics.md`; no later top-level section should follow it.
- Inside that final `## Gesamtstatistik` block, maintain compact ASCII-only trend diagrams that show at least the current artifact mix, the documented branch/phase curves, the documented acceleration factors from agentic-AI plus Spec-Kit/SDD support, and a direct comparison between experienced-developer effort, Thorsten-solo effort, and the visible AI-assisted delivery window; refresh them whenever the statistics ledger changes.
- Keep each short CEFR-B2 explanation directly adjacent to its matching ASCII diagram group, ideally immediately before or after it, so apprentices do not need to scroll between explanation and diagram.
- When the data benefits from progression across an X-axis, add simple ASCII X/Y charts as a second visualization layer; keep them approximate, readable in plain Markdown, and explained in CEFR-B2 language.
- ASCII X/Y charts must use fixed-width X slots: every documented phase keeps its slot, missing values stay blank, and overly wide future series are split into labelled blocks such as `0..15`, `16..31`, and `32..47`, each with its own axis line and X labels.
- Keep the statistics section plain-text friendly for Braille displays, screen readers, and text browsers; the ASCII diagrams and their explanations must stay understandable without relying on color or visual layout alone.
- When DocFX content, documentation navigation, or API presentation changes, validate representative `_site/` pages through a text-oriented review path, preferably with a local Playwright accessibility snapshot.
- Use WCAG 2.2 AA as the concrete review baseline for generated HTML documentation, especially for page language, bypass blocks, keyboard focus visibility, non-text contrast, and readable landmark structure.
- Each update must capture branch/phase, observable work window, production/test/documentation line counts, main work packages, the conservative manual baseline of 80 code lines per day for an experienced developer, and the repo-specific Thorsten-Solo comparison baseline of 125 lines per workday for this Pascal/Turbo-Vision-derived port.
- When reporting acceleration, compare both manual references against visible Git active days and label the result as a blended repository speedup rather than a stopwatch measurement.
- When hour values are shown, convert the day-based estimates with the TVoeD working-day baseline of `7.8 hours` (`7h 48m`) per day.

## Workflow Platforms

Einstiege / Entry files: `CLAUDE.md`

- The Multi-Mac setup on `MacBook Air M2` and `Mac mini M4 Pro` is the primary development and day-to-day test workflow.
- Keep `gh`, `specify`, `codex`, `claude`, `copilot`, and `agy` installed on both Macs; before Spec-Kit work or Spec-Kit updates, run `specify check` to confirm the required toolchain is available.
- After every `/speckit-plan` run or equivalent plan refresh that changes active technologies, project structure, or agent context, run `.specify/scripts/bash/update-agent-context.sh` for `codex`, `claude`, `agy`, and `copilot` in the same work item by default. Treat this multi-agent context refresh as routine maintenance that does not need a separate user prompt.
- Linux and Windows are additional compatibility-validation environments; on Windows, prefer WSL with a current Ubuntu release, currently `Ubuntu 24.04`.
- When changes affect runtime behavior, build reliability, terminal behavior, or portability, include Linux and Windows/WSL compatibility checks where practical and reflect them in CI or equivalent validation evidence when feasible.

## Shared Parent Guidance

Einstiege / Entry files: `CLAUDE.md`

- The shared parent file `/Users/thorstenhindermann/RiderProjects/AGENTS.md` intentionally stores only repo-spanning baseline rules.
- Keep repository-specific build, test, workflow, architecture, and feature guidance in this repository's own files; when both layers exist, the repository-local files are the more specific authority.
<!-- claude-init-done -->

## Spec-Kit-Preset-Pflege / Spec Kit Preset Maintenance

Einstiege / Entry files: `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`

- Standard-Preset-Set: `security-governance` v0.7.5 prio 10, `architecture-governance` v0.6.6 prio 20, `isaqb-architecture-governance` v0.2.6 prio 30, `a11y-governance` v0.4.7 prio 40, `cross-platform-governance` v0.2.6 prio 50, `agent-parity-governance` v0.4.6 prio 60, `autonomous-run-governance` v0.4.12 prio 70, `parallel-autonomous-run-governance` v0.2.13 prio 80.
- Optionales Projektprofil: `intake-review-governance` v0.2.9 prio 65 ist in TuiVision aktiv. Es bindet den naechsten Intake vor Feature-Erstellung an ein aktuelles `Ready`- oder menschlich akzeptiertes `ReadyWithAcceptedRisks`-Ergebnis; Review und Status bleiben read-only, Repair benoetigt ausdrueckliche Aenderungsautoritaet. *Optional project profile: `intake-review-governance` v0.2.9 at priority 65 is active in TuiVision and hash-binds the next intake before feature creation.*
- `autonomous-run-governance` v0.4.12 prio 70 ist Teil der Standard-Achtermatrix. Ein vollständiger autonomer Lauf bleibt ausdrücklich delegationspflichtig; die Installation allein erteilt weder Ausführungsberechtigung noch Remote-, Merge-, Bypass- oder Provider-Rechte und `LocalImplementation` bleibt Default. Dokumentations-, Status-, Schema- oder Evidence-Änderungen gelten erst dann als testfrei, wenn keine ausführbaren Validatoren die geänderten Pfade, Marker, Schemas oder Zustandswerte konsumieren. Vor autorisierten Commits wird der exakt beabsichtigte Kandidat mit dem Delivery-Set-Validator im Staged-Modus einschließlich exakter Pfad- und Index-Hash-Bindung geprüft; fremde Änderungen bleiben unberührt. Vor einem Merge wird jeder Acceptance-Gate dem tatsächlich ausgeführten Workflow, Job, Runner beziehungsweise der Plattform und dem Befehl zugeordnet; grüne Namen oder ein Bypass ersetzen keinen technischen Nachweis. Bewusst pausierte Läufe werden als `PausedByUser` gespeichert und nur über `speckit.autonomous-resume` fortgesetzt; `speckit.autonomous-stop` wirkt kooperativ am nächsten sicheren Grenzpunkt, und ein gespeicherter Delivery-Modus ist keine aktuelle Berechtigung. Nach Preset- oder Governance-Drift werden neue zwingende Korrektheits-, Sicherheits-, Berechtigungs- und Evidenzregeln minimal mit akzeptierten Plan-, Task- und Checklist-Artefakten abgeglichen; reine Effizienzpräferenzen lösen keine rückwirkende Neugenerierung aus. Die lesbare Skill-Überschrift `Deliver` ist kein Run-State-Wert; für Remote-Closeout gelten ausschließlich `Publish`, `Review` oder `MergeAndSync`. Bei aktiver Intake-Policy prueft der Preflight das aktuelle hashgebundene Ergebnis vor der Feature-Erstellung; Resume prueft es nach Drift erneut.
- `parallel-autonomous-run-governance` v0.2.13 prio 80 ist Teil der Standard-Achtermatrix. Die Installation startet keine Kampagne und erteilt keine zusaetzlichen Remote-, Merge-, Bypass-, Abbruch-, Secret- oder Provider-Rechte. Kampagnen bleiben ausdruecklich delegationspflichtig, verwenden getrennte Worktrees und maximal drei gleichzeitig aktive Worker. Schema 1.1 erlaubt ein `runnerProfile` je Worker mit Kampagnen-Fallback; Modell und Reasoning-Stufe sind optionale, nicht geheime Metadaten und werden ohne Deklaration nicht erraten. Konsolidierung verlangt exakten Head, aktuelle Review- und Check-Evidenz, ist nach Teilmerges fortsetzbar und setzt `Completed` erst nach Synchronisation, manifestdeklarierten idempotenten Post-Merge-Aktionen und Abschlussvalidierung. Schema 1.2 kann vor Worktree-Erstellung einen aktuellen Campaign-Review mit eindeutiger Intake-, Worker-, DAG- und Exception-Coverage verlangen; Resume prueft den Ergebnis-Hash erneut.
- `a11y-governance` v0.4.7 ergänzt didaktische Inline-Code-Kommentar-Governance für neue oder geaenderte nicht-triviale Logik.
- `security-governance` v0.7.5 fuehrt `AI-SBOM` weiter als bedingt anwendbare Supply-Chain-Evidenz, ergänzt sprachspezifische Secure-Coding-Profile und ergänzt regulatorische Anwendbarkeit für NIS2, CRA, EU AI Act und DORA. Reine Entwicklungswerkzeug-Nutzung bleibt `N/A`; KI-Runtime-/Produktkomponenten benoetigen Evidenz nach G7/BSI AI-SBOM-Clustern; private Ausbildungsprojekte dokumentieren regulatorische Nichtanwendbarkeit mit kurzer Begründung.
- `architecture-governance` v0.6.6 ergänzt `BSI C3A` als bedingte Cloud-Autonomie-Evidenz und `BSI C5` als bedingte Cloud-Compliance-Assurance-Evidenz für Cloud-Service-Auswahl, Provider-Abhängigkeiten, Audit-/Nachweisstand, Shared Responsibility und Betriebsnachweise.
- Alle acht Presets enthalten ab diesem Release-Block audit-ready Spec-Kit-Run-Evidenz: `Applicable` / `N/A` / `Open`, Begründung, Evidenzpfad, Reviewer, Restrisiko und Follow-up muessen im aktuellen Spec-Kit-Lauf dokumentiert werden.
- Die ursprünglichen sechs Presets sind seit 2026-05-04 und `autonomous-run-governance` v0.2.2 ist seit 2026-07-17 im `github/spec-kit` Community-Katalog enthalten und liegen zusätzlich als veröffentlichte Repos unter `https://github.com/hindermath/spec-kit-preset-*`.
- `parallel-autonomous-run-governance` v0.2.13 ist eigenstaendig veroeffentlicht; v0.2.2 wurde mit `github/spec-kit#3591` fuer den Community-Katalog eingereicht.
- Registrierte Level-0-, Level-1- und Level-2-Repositories installieren bei vorhandener Spec-Kit-Integration standardmäßig alle acht Presets aus `scripts/config/spec-kit-governance-presets.json`, sofern keine begründete Ausnahme dokumentiert ist.
- Referenz-Rollout für alle acht Presets: `RiderProjects/TinyPl0`, `RiderProjects/TinyCalc`, `RiderProjects/TuiVision`, `RiderProjects/InventarWorkerService`.
- Installation erfolgt bevorzugt mit `install-spec-kit-governance-presets.*` aus der zentralen Matrix; die Skriptlogik enthaelt keine fest eingebauten Versionen. Bei neuen Preset-Releases zuerst die Matrix aktualisieren, dann bestehende Repos bewusst mit `--force` / `-Force` nachziehen.
- Flotten-Rollouts erfassen Level-0, Level-1 und Level-2 explizit. Eine reine Level-2-Registry beweist keine vollstaendige Abdeckung; jeder Zielstatus wird bis Installation, exakter Matrixvalidierung, Commit, Push und Remote-Synchronisation verfolgt.
- Vor dem Staging werden generierte Preset-/Agentenpfade mit dem gesamten Arbeitsbaum abgeglichen. Fremde Aenderungen bleiben unberuehrt; bei Konflikten wird ein sauberer Worktree statt eines erzwungenen Misch-Commits verwendet.
- Aktuelle normative Sechs-/Siebenerangaben werden auf die Achtermatrix migriert. Historische Statistik-, Changelog-, Feldnachweis- und Kompatibilitaetsangaben bleiben erhalten und werden durch einen dokumentierten Allowlist-Scan unterschieden.
- Provider-/Billing-Ablehnung, technischer Gate-Fehler und bestandener Gate sind getrennte Ergebnisse. Bypass oder gruene Sammelnamen ersetzen keinen exakten technischen Nachweis.
- `.specify/presets/` und generierte Agenten-/Command-Dateien committen, wenn Presets Projekt-Policy sind; `.specify/presets/.cache/` nie committen.
- Nach Installation oder Update prüfen: `specify preset list`, mindestens ein `specify preset info <id>`, bei Template-Fragen zusätzlich `specify preset resolve <template>`.
- Die veröffentlichten eigenständigen GitHub-Preset-Repositories sind alleinige Produktquellen. Lokale Checkouts unter `~/SpecKitPresetProjects/` dienen ihrer Entwicklung; installierte Pakete sind Integrationskopien.
- Verbesserungen direkt im passenden eigenständigen Preset-Repository entwickeln, prüfen und veröffentlichen. Ein unveränderliches Release-Paket wird nur nach ausdrücklicher Freigabe hier integriert; keine lokalen Produkt-Forks oder Scaffold-Doppelpflege.
- Bei Änderungen an Preset-Regeln immer prüfen, ob `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `scripts/templates/*` ebenfalls aktualisiert werden müssen.
- Bei jeder Preset-Version oder Prioritätsänderung zuerst `scripts/config/spec-kit-governance-presets.json` aktualisieren und danach README-Tabellen, Constitution, Agenten-Dateien, `scripts/templates/speckit-workflow-section.md` und Agenten-Templates gemeinsam prüfen.

*Fleet rollouts explicitly cover level 0, level 1, and level 2 and track each
target through installation, exact matrix validation, commit, push, and remote
synchronization. Separate generated paths from unrelated work before staging.
Migrate current normative six/seven references while preserving allowlisted
history and compatibility aliases. Provider refusal, technical gate failure,
and passing evidence are distinct; bypass is not technical proof.*
- Community-/Katalog-Abstimmung läuft über `github/spec-kit#2362`.

*Standard preset set: `security-governance` v0.7.5 prio 10, `architecture-governance` v0.6.6 prio 20, `isaqb-architecture-governance` v0.2.6 prio 30, `a11y-governance` v0.4.7 prio 40, `cross-platform-governance` v0.2.6 prio 50, `agent-parity-governance` v0.4.6 prio 60, `autonomous-run-governance` v0.4.12 prio 70, and `parallel-autonomous-run-governance` v0.2.13 prio 80. `a11y-governance` v0.4.7 adds didactic inline-code-comment governance for new or changed non-trivial logic. `architecture-governance` v0.6.6 adds conditional `BSI C3A` cloud-autonomy evidence and `BSI C5` cloud-compliance assurance evidence for cloud-service selection, provider dependencies, audit/assurance status, shared responsibility, and operational evidence. `security-governance` v0.7.5 keeps conditional `AI-SBOM` evidence, language-specific secure-coding profiles, and regulatory applicability screening for NIS2, CRA, EU AI Act, and DORA: development-tool-only AI usage is `N/A`, AI runtime/product components require G7/BSI AI-SBOM cluster evidence, and private training projects record regulatory `N/A` when no regulated scope exists. All eight presets now include audit-ready Spec-Kit run evidence: `Applicable` / `N/A` / `Open`, rationale, evidence path, reviewer, residual risk, and follow-up must be documented for the current Spec-Kit run. The original six presets have been in the `github/spec-kit` community catalog since 2026-05-04, and `autonomous-run-governance` v0.2.2 was verified there on 2026-07-17. All eight are also published under `https://github.com/hindermath/spec-kit-preset-*`. `parallel-autonomous-run-governance` v0.2.13 was submitted to the community catalog as `github/spec-kit#3591`. Registered level-0, level-1, and level-2 repositories with Spec Kit default to all eight presets from `scripts/config/spec-kit-governance-presets.json` unless a justified exception is documented. Use `install-spec-kit-governance-presets.*` so preset versions stay centralized in the matrix. Commit `.specify/presets/` and generated agent command updates when presets are project policy, but never commit `.specify/presets/.cache/`. Verify installs with `specify preset list`, `specify preset info`, and where relevant `specify preset resolve`. Develop and release presets in their standalone GitHub repositories, the sole product sources. Install immutable release packages only with explicit authority; do not maintain local product forks or duplicate scaffolds. Preset-rule changes and preset version/priority changes require reviewing the central matrix, constitution, README tables/install snippets, all agent guidance files, and relevant templates together. Community/catalog coordination happens in `github/spec-kit#2362`.*

## Hinweise / Notes

Einstiege / Entry files: `CLAUDE.md`

- Diese Datei ergaenzt die projektspezifische Dokumentation mit agentischen Arbeitsregeln.
- This file complements the project-specific documentation with agent-oriented working rules.

## Einordnung / Scope

Einstiege / Entry files: `CLAUDE.md`

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## 🚀 Projektübersicht

Einstiege / Entry files: `GEMINI.md`

*   **Zweck:** Portierung des Turbo Vision Frameworks (C/C++) in ein modernes, rein verwaltetes (managed) .NET 10 Framework.
*   **Zielgruppe:** Fachinformatiker (Anwendungsentwicklung), dient als Lern- und Referenzprojekt für Agentic-AI-Workflows.
*   **Technologiestack:** C# 14, .NET 10 (`net10.0`), MSTest, docfx.
*   **Leitprinzipien:** Didaktisch wertvolle Dokumentation, keine nativen Abhängigkeiten, plattformübergreifend (macOS, Linux, Windows).

## 🏗️ Architektur & Projektstruktur

Einstiege / Entry files: `GEMINI.md`

Das Projekt folgt einer modularen Struktur gemäß .NET Best Practices:

*   **`src/TuiVision.Core`**: Basisobjekte (`TObject`), Geometrie (`TPoint`, `TRect`), Ereignissystem (`TEvent`).
*   **`src/TuiVision.Controls`**: UI-Komponenten (`TView`, `TGroup`, Fenster, Dialoge).
*   **`src/TuiVision.Drivers.Console`**: Plattformübergreifender Konsolentreiber (rein managed).
*   **`src/TuiVision.Serialization`**: Streams, Ressourcen und Persistenz.
*   **`src/TuiVision.Compatibility`**: Hilfsklassen zur Erleichterung der Portierung von historischem Code.
*   **`tests/`**: Parallele Testprojekte für jedes Modul unter Verwendung von MSTest.
*   **`examples/`**: Portierte Beispielprogramme aus der Turbo Vision Distribution.
*   **`tv203s/`**: Historischer C++ Quellcode von Turbo Vision 2.0.3 als Referenzbasis.

## 🛠️ Build & Development

Einstiege / Entry files: `GEMINI.md`

### Zentrale Befehle
*   **Bauen:** `dotnet build`
*   **Testen:** `dotnet test`
*   **Dokumentation:** `docfx docfx.json` (wenn im Projektwurzelverzeichnis vorhanden)
*   **Formatierung:** `dotnet format`
*   **DocFX-A11y-Smoke-Test:** `cd tests/web-a11y && npm install && npx playwright install chromium && npm run test:docfx`
*   **Regel fuer Doku-Neubau:** Nach jedem erfolgreichen `docfx docfx.json` den passenden A11y-Smoke-Test unter `tests/web-a11y/` direkt im selben Arbeitsschritt ausfuehren.

### Arbeitsumgebung
*   Optimiert für **Multi-Mac Workflow** (MacBook Air M2 & Mac mini M4 Pro).
*   Nutzung von `gh` (GitHub CLI) und `codex` für Agentic-Workflows.
*   Haupt-IDE: **JetBrains Rider**.

## 📝 Entwicklungskonventionen

Einstiege / Entry files: `GEMINI.md`

1.  **Code-Stil:** Modernes C# (File-scoped Namespaces, Expression-bodied Members, Nullable Reference Types).
2.  **JSON-Verarbeitung:** Für projektinternes JSON-Parsing und JSON-Serialisierung ist `System.Text.Json` zu verwenden. `Newtonsoft.Json` darf nur mit dokumentierter Begründung und expliziter Freigabe im Review eingeführt werden.
3.  **Dokumentation (MUSS):**
    *   Dokumentationsblöcke zweisprachig: erst Deutsch, dann Englisch.
    *   Beide Sprachfassungen auf CEFR-B2-Niveau.
    *   Grosse normative Dokumente wie `Pflichtenheft*.md` und `Lastenheft*.md` duerfen statt eines uebergrossen Inline-Zweisprachblocks als synchron gepflegte `.EN.md`-Parallelfassung ausgeliefert werden; die deutsche Fassung bleibt kanonisch, sofern nichts anderes markiert ist.
    *   Programmierung #include<everyone> — Diese Lernbeispiele richten sich an Azubis (Fachinformatiker AE/SI) mit Deutsch und Englisch als Arbeitssprachen sowie an sehbehinderte Lernende, die mit Braille-Displays, Screen-Readern oder Textbrowsern arbeiten. Barrierefreiheit ist kein Nice-to-have, sondern Pflichtanforderung.
    *   Erzeugte HTML-Dokumentation soll mindestens WCAG 2.2 Konformitaetsstufe AA als Barrierefreiheits-Basis anstreben.
    *   Die Smoke-Tests unter `tests/web-a11y/` mit Playwright und `@axe-core/playwright` muessen bei DocFX-Struktur- oder API-Doku-Aenderungen mitgezogen werden; `lynx` dient als zusaetzlicher Textbrowser-Gegencheck.
    *   Jeder DocFX-Neubau gilt erst dann als abgeschlossen, wenn der zugehoerige A11y-Smoke-Test ebenfalls erfolgreich war.
    *   GitHub Pages wird aus `.github/workflows/pages.yml` veroeffentlicht: root-`docfx.json` bauen, `tests/web-a11y/` mit Playwright plus axe ausfuehren, `_site/` als Pages-Artefakt hochladen und `_site/` sowie generierte `api/*.yml`-Dateien aus Git heraushalten.
    *   Wichtige Aussagen duerfen nicht nur ueber Farbe, Layout oder Mauszeiger-Hinweise transportiert werden; bevorzugt werden semantische Ueberschriften, Listen, Tabellen und ASCII-/Textdiagramme.
    *   Bilinguale CEFR-B2-Lieferung und der dokumentierte A11Y-Nachweis gehoeren zur formalen Abschlusspruefung fuer lernrelevante Doku und aktive Anforderungsartefakte.
    *   Vollständige XML-Kommentare für alle öffentlichen APIs (`summary`, `param`, `returns`, `remarks`).
    *   Didaktischer Stil: Erklärt das *Warum* und bietet Beispiele für Lernende.
    *   Neue oder geaenderte nicht-triviale Logik muss auf didaktischen Inline-Kommentarbedarf geprueft werden, wenn Lernverstaendnis oder Wartbarkeit betroffen sind, besonders bei zentralen Framework-Flows und Smoke-Test-Helfern.
    *   Inline-Kommentare erklaeren Warum, Trade-off, Randbedingung, historische Abweichung oder Proof-Grenze; sie wiederholen nicht den offensichtlichen Code.
    *   Die Kommentarintensitaet bleibt moderat: normalerweise 1 bis 3 Zeilen vor einem nicht-trivialen Block, bei didaktischen Erklaerbloecken Deutsch zuerst und Englisch danach auf CEFR-B2-Niveau.
    *   Aktualisierung der Dokumentation erfolgt zeitgleich mit Codeänderungen.
4.  **Testing:**
    *   Mindestens 70% Line Coverage jeweils fuer `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console`.
    *   Die verbindliche Coverage-Messung erfolgt aus dem Repository-Root mit `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`.
    *   `coverlet.runsettings` ist die kanonische TuiVision-Coverage-Gate-Konfiguration und MUSS gepflegt werden, wenn gate-relevante Assemblies, Beispiel-Assemblies oder Testprojekte hinzukommen, umbenannt oder entfernt werden. Vor Nutzung des Gates nach Moeglichkeit mit `xmllint --noout coverlet.runsettings` validieren.
    *   Jedes Feature benötigt Unit-Tests (MSTest) und ggf. Smoke-Tests in den Beispielen.
5.  **Keine Nativen Abhängigkeiten:** Alle Treiber müssen rein in verwaltetem Code implementiert sein (kein P/Invoke, wo vermeidbar).
6.  **Lizenztreue:** Einhaltung der MIT-Lizenz für neuen Code; Respektierung der Original-Lizenzen im `tv203s` Ordner.

## 🌿 Branch-Konvention

Einstiege / Entry files: `GEMINI.md`

*   Feature-Branches verwenden entweder die agentenpraefixierte Form `codex/<feature-description>` (oder ein anderes unterstuetztes Praefix wie `claude/`, `agy/`, das legacy-kompatible `gemini/`, `copilot/`, `opencode/`) oder die nummerierte Spec-Kit-Form `NNN-short-description`, wenn der Spec-Kit-Workflow diesen Branch-Typ erzeugt.
*   CI reagiert auf Pushes nach `main`, `master`, `codex/**`, `claude/**`, `agy/**`, das legacy-kompatible `gemini/**`, `copilot/**` und `opencode/**`.
*   Die repo-weite Versionslogik liegt in `Directory.Build.props`: `Version`, `AssemblyVersion` und `FileVersion` folgen `Major.Minor.Patch.Build`, wobei `Minor` die numerisch interpretierte Spec-Kit-Feature-/Branch-Nummer als kanonische PR-Nummer verwendet (`007` -> `7`), `Patch` der Commit-Anzahl im Feature-/PR-Branch nach dem aktuellen Commit entspricht und `Build` nur vor `dotnet build` oder `dotnet test` manuell erhoeht wird.
*   Wenn ein dedizierter Feature-Branch die Anforderungen eines Lastenhefts umgesetzt hat, wird die Datei in `Lastenheft_<Thema>.<feature-branch>.md` umbenannt, damit der gelieferte Umfang im Repository nachvollziehbar bleibt.

## 📚 Wichtige Dokumente

Einstiege / Entry files: `GEMINI.md`

*   `README.md`: Allgemeine Einführung und CI-Status.
*   `Lasten_Heft.md`: Grobe Anforderungen und Ziele.
*   `Pflichtenheft.md`: Detaillierte technische Spezifikation (Referenz für MUSS-Anforderungen).
*   `docs/guides/multi-mac-workflow.md`: Anleitung für die verteilte Entwicklung.

## Example Wave Delivery Pattern

Einstiege / Entry files: `GEMINI.md`

*   Groessere verpflichtende Beispielwellen SOLLEN als bewusstes zweistufiges Spec-Kit-Liefermuster geplant werden, wenn Portierungslogik, Framework-Luecken und interaktive Runtime-Politur sonst in einem Feature vermischt wuerden.
*   Stufe 1 ist der funktionale Portierungs- und Nachweis-Feature-Lauf: historische Beispielablaeufe portieren, Framework-Voraussetzungen schliessen, deterministische Headless- oder In-Process-Smoke-Pfade bereitstellen, Guides/Evidence ergaenzen und interaktive Runtime-Politur explizit als Follow-up markieren, wenn `dotnet run --project examples/<Name>` noch nicht die finale Demo zeigt.
*   Stufe 2 ist der interaktive Showcase-Feature-Lauf: die bewiesenen Funktionen ueber sichtbare Menues, Statuszeilen, Desktop-Controls, Dialoge, Tastaturpfade und skriptbare UI-Event-Smoke-Tests erreichbar machen.
*   Eine Beispielwelle gilt erst nach Stufe 2 als vollstaendig lern- und reviewtauglich, sofern der Scope nicht ausdruecklich nur einen minimalen nicht-interaktiven Nachweis verlangt.
*   Kuenftige Lastenhefte fuer Beispielwellen oder beispielnahe Framework-Vorhaertungen MUESSEN ein Framework-Usage- und Remediation-Gate enthalten. Der spaetere Spec-Kit-Lauf muss pro Beispiel oder Vertragsbereich dokumentieren, welche bestehende TuiVision-Framework-Komponente genutzt wird, ob lokale Sonderlogik existiert und welche Entscheidung gilt: `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder `FollowUpHardening`.
*   Wiederverwendbare Logik darf nicht dauerhaft als lokale `examples/`-Sonderloesung verbleiben. Wenn dieselbe lokale Logik in mehreren Beispielen nuetzlich waere oder Framework-Verhalten ersetzt, gehoert sie in einen kleinen Framework-Fix oder in ein eigenes Follow-up-Hardening.

## 🔄 Synchronisationsregel für KI-Agenten-Dateien

Einstiege / Entry files: `GEMINI.md`

*   Wenn sich aktiver Feature-Kontext, Planungsstand oder gemeinsam genutzte Agenten-Hinweise ändern, müssen diese Dateien gemeinsam geprüft und bei Bedarf im selben Arbeitsgang aktualisiert werden:
    *   `AGENTS.md`
    *   `CLAUDE.md`
    *   `GEMINI.md`
    *   `.github/copilot-instructions.md`
    *   `.github/agents/copilot-instructions.md`
*   Eine nur teilweise Synchronisierung ist nicht zulässig, wenn sich gemeinsame Vorgaben geändert haben.
*   Falls eine Datei absichtlich agentenspezifisch abweicht, muss diese Abweichung im selben Change ausdrücklich dokumentiert werden.

## 📊 Projektstatistik

Einstiege / Entry files: `GEMINI.md`

*   `docs/project-statistics.md` ist das fortlaufende Statistik-Register des Repositories.
*   Die Datei muss nach jeder abgeschlossenen Spec-Kit-Implementierungsphase, nach jeder agentischen Änderung am Repository und auf explizite Anforderung aktualisiert werden.
*   Im `## Fortschreibungsprotokoll` muessen die Tabelleneintraege strikt chronologisch stehen: der aelteste Eintrag oben, der juengste und zuletzt eingetragene Eintrag unten; Eintraege mit demselben Datum behalten ihre Eintragungsreihenfolge.
*   Als letzter Top-Level-Block der Datei muss immer ein `## Gesamtstatistik`-Abschnitt stehen; danach darf kein weiterer Top-Level-Abschnitt folgen.
*   Innerhalb dieses finalen `## Gesamtstatistik`-Abschnitts muessen kompakte ASCII-only-Diagramme direkt unter der textlichen Gesamtauswertung mitgefuehrt werden; sie sollen mindestens Artefaktmix, die dokumentierten Branch-/Phasenverlaeufe, die dokumentierten Beschleunigungsfaktoren durch agentische KI plus Spec-Kit/SDD und einen direkten Vergleich zwischen erfahrener Entwickler-Referenz, Thorsten-Solo-Referenz und sichtbarem KI-Lieferfenster zeigen und bei jeder Statistikpflege mitaktualisiert werden.
*   Jeder kurze Erklaertext in CEFR-B2-Sprache muss direkt bei seiner ASCII-Diagrammgruppe stehen, idealerweise unmittelbar davor oder danach, damit Auszubildende nicht zwischen Erklaerung und Diagramm scrollen muessen.
*   Wenn Daten entlang einer X-Achse als Verlauf besser lesbar werden, sollen zusaetzlich einfache ASCII-X/Y-Diagramme eingefuegt werden. Diese muessen bewusst grob, in reinem Markdown lesbar und ebenfalls in CEFR-B2 erklaert bleiben.
*   ASCII-X/Y-Diagramme muessen feste X-Slots verwenden: jede dokumentierte Phase behaelt ihren Slot, fehlende Werte bleiben leer, und zu breite kuenftige Reihen werden in beschriftete Bloecke wie `0..15`, `16..31` und `32..47` geteilt, jeweils mit eigener Achsenlinie und eigenen X-Labels.
*   Der Statistikblock muss fuer Braille-Zeile, Screenreader und Textbrowser plain-text-freundlich bleiben; ASCII-Diagramme und Erklaerungen duerfen keine Schluesselaussage nur ueber visuelles Layout transportieren.
*   Wenn sich DocFX-Inhalte, Navigationsstruktur oder API-Praesentation aendern, sind repraesentative `_site/`-Seiten ueber einen textorientierten Pruefpfad zu kontrollieren, bevorzugt mit einem lokalen Playwright-Accessibility-Snapshot.
*   Fuer erzeugte HTML-Dokumentation gilt WCAG 2.2 AA als konkrete Pruefbasis, besonders fuer Seitensprache, Bypass-Mechanismen, sichtbaren Tastaturfokus, Non-Text-Contrast und verstaendliche Landmark-Struktur.
*   Jeder Eintrag muss Branch oder Phase, beobachtbares Arbeitsfenster, Produktions-, Test- und Doku-Zeilen, die wesentlichen Arbeitspakete, die konservative Handarbeits-Basis von 80 Codezeilen pro Tag fuer einen erfahrenen Entwickler sowie die repo-spezifische Thorsten-Solo-Vergleichsbasis von 125 Zeilen pro Arbeitstag fuer diese Pascal-/Turbo-Vision-Portierung enthalten.
*   Beschleunigungsangaben muessen beide Referenzen gegen sichtbare Git-Aktivtage stellen und ausdruecklich als repo-weiten Verdichtungsfaktor statt als Stoppuhrmessung kennzeichnen.
*   Wenn Stundenwerte ausgewiesen werden, sind die Tageswerte mit der TVoeD-Arbeitszeit von `7,8 Stunden` bzw. `7 Stunden 48 Minuten` pro Arbeitstag umzurechnen.

## 🖥 Workflow-Plattformen

Einstiege / Entry files: `GEMINI.md`

*   Der Multi-Mac-Aufbau auf `MacBook Air M2` und `Mac mini M4 Pro` ist der primaere Entwicklungs- und Alltagstest-Workflow.
*   Auf beiden Macs muessen `gh`, `specify`, `codex`, `claude`, `copilot` und `agy` installiert sein; vor Spec-Kit-Arbeiten oder Spec-Kit-Updates ist `specify check` auszufuehren, damit die benoetigte Werkzeugkette bestaetigt ist.
*   Nach jedem `/speckit-plan`-Lauf oder einer gleichwertigen Plan-Aktualisierung, die aktive Technologien, Projektstruktur oder Agent-Kontext aendert, ist `.specify/scripts/bash/update-agent-context.sh` standardmaessig fuer `codex`, `claude`, `agy` und `copilot` im selben Arbeitsgang auszufuehren. Diese Multi-Agenten-Kontextaktualisierung gilt in diesem Repository als vorab freigegebene Wartungsroutine und braucht keine gesonderte Rueckfrage.
*   Linux und Windows dienen zusaetzlich als Kompatibilitaets- und Validierungsumgebungen; unter Windows ist WSL mit einer aktuellen Ubuntu-Version, derzeit bevorzugt `Ubuntu 24.04`, der empfohlene Weg.
*   Wenn Aenderungen Laufzeitverhalten, Build-Stabilitaet, Terminalverhalten oder Portabilitaet betreffen, sollen Linux- und Windows/WSL-Kompatibilitaetschecks nach Moeglichkeit mitgefuehrt und in CI oder gleichwertigen Nachweisen sichtbar gemacht werden.

## ▶ Pflichtenheft-Marker fuer den naechsten Schritt

Einstiege / Entry files: `GEMINI.md`

*   In `Pflichtenheft.md` ist ein gut sichtbarer Marker `>>> NAECHSTER SCHRITT <<<` zu pflegen.
*   Der Marker muss immer auf den aktuell hoechstprioren offenen Arbeitsschritt im Abschnitt der priorisierten Restarbeiten zeigen und bei Fortschritt entsprechend weitergeschoben werden.

---
*Hinweis: Dieses Dokument wurde automatisch von Antigravity CLI generiert und dient als Instruktionsbasis.*

## Shared Parent Guidance

Einstiege / Entry files: `GEMINI.md`

*   Die gemeinsamen Dateien `/Users/thorstenhindermann/RiderProjects/AGENTS.md` und `/Users/thorstenhindermann/RiderProjects/GEMINI.md` speichern die repo-uebergreifenden Basisregeln.
*   Diese Projekt-Datei ist die spezifischere Autoritaet fuer projektspezifische Build-Befehle, Workflows, Architektur und Features.

---

## Zentrale Verzeichnisse / Key Directories

Einstiege / Entry files: `GEMINI.md`

- `~/scripts/`: Zentrale Automatisierungsskripte (Bootstrap, Secret-Scan, Hook-Installer).
- `~/`: Weitere Workspace-Verzeichnisse werden per `bootstrap-workspace` angelegt und hier eingetragen.
- `~/.gemini/antigravity-cli/`: Globale Antigravity-Konfiguration und persistenter lokaler Zustand; der Herstellerpfad bleibt trotz des Befehlsnamens `agy` erhalten.

## Entwicklungskonventionen / Development Conventions

Einstiege / Entry files: `GEMINI.md`

- **Plattformunabhängigkeit & Dokumentation:** Alle kritischen Skripte müssen sowohl als `.sh` (Bash) als auch als `.ps1` (PowerShell Core) vorliegen. Jedes Skript erfordert eine Unix man-Page (`.sh`, in `docs/man/`), eine vollständige PowerShell-Hilfe (`.ps1`) und muss zusätzlich als PowerShell Cmdlet (Advanced Function) im `Verb-Noun` Format verfügbar sein.
- **Sicherheits-Standard:** Jedes Projekt muss über einen `pre-push` Hook verfügen, der Secret-Scanning in Agenten-Verzeichnissen durchführt.
- **Git-Strategie:** Keine Submodules; stattdessen werden Sub-Repos durch die Baseline-Skripte in der `.gitignore` des übergeordneten Workspaces erfasst.

## Projektstatus / Repository Status

Einstiege / Entry files: `GEMINI.md`

- **Sichtbarkeit:** Öffentliches **Template-Repo** — über „Use this template" nutzbar; kein Fork, keine History-Übertragung
- **Lizenz:** MIT
- **Branch-Schutz:** PR-Pflicht auf `main`; Admin (Eigentümer) kann direkt pushen (`enforce_admins: false`)
- **CI:** ✅ Ubuntu 22.04 · macOS 14 · Windows 2022
- **Compliance-Score:** 100 % (25/25 Checks)

## Bekannte Fallstricke / Known Pitfalls

Einstiege / Entry files: `GEMINI.md`

### `gh auth login --web` bleibt hängen / `gh auth login --web` Hangs
Browser-Callback kommt in Hintergrundprozessen nicht an.
In **interaktivem Terminal** ausführen.

### `glab auth login --web` bleibt hängen / `glab auth login --web` Hangs
Browser-Callback kommt in Hintergrundprozessen nicht an.
In **interaktivem Terminal** ausführen.

### `gh`-Keyring ungültig (Windows) / `gh` Keyring Invalid (Windows)
Windows Credential Store korrupt.
`gh auth logout` + neu anmelden; danach `gh auth setup-git`.

### `ssh-agent` startet nicht (Windows) / `ssh-agent` Does Not Start (Windows)
Service deaktiviert, Admin nötig.
HTTPS + `gh auth setup-git` verwenden.

### `CursorPosition`-Fehler in PS-Subprocess / `CursorPosition` Error in PowerShell Subprocess
PowerShell-Profil (Oh-My-Posh) lädt im Subprozess.
`-NoProfile` zu `pwsh -File`-Aufrufen hinzufügen.

### `migrate-workspace.*` läuft parallel in Timeouts / `migrate-workspace.*` Times Out in Parallel
Jeder Migrationslauf startet `init-stats.*` und aktualisiert die Level-0/1/2-Statistiken global.
Mehrere parallele Läufe können sich gegenseitig ausbremsen. Erst Vorschau (`-WhatIf`/`--dry-run`),
dann echte Migrationen seriell pro Workspace mit längerem Timeout ausführen.

### `git pull` meldet divergierende Branches (Linux) / `git pull` Reports Divergent Branches (Linux)
Kein globales Rebase-Setup.
`git config --global pull.rebase true`.

### Push rejected: `fetch first` / Push Rejected: `fetch first`
Remote ist neuer als lokal.
`git pull --rebase --autostash && git push`.

### Test-Skript blockiert Pull / Test Script Blocks Pull
Output-Datei wird vor `pull` geschrieben.
`git pull --rebase --autostash origin main`.

### Lastenheft nach Feature-Abschluss nicht umbenannt / Lastenheft Not Renamed After Feature Completion
`tasks.md` enthielt keinen Rename-Schritt (seit constitution v1.1.1 behoben).
`bash scripts/rename-lastenheft.sh <LH-Datei> <branch-name>` oder `pwsh scripts/rename-lastenheft.ps1 -File <LH-Datei> -BranchName <branch-name>`.

### Windows: `$env:HOME` ist leer, nicht `$null` / Windows: `$env:HOME` Is Empty, Not `$null`
```powershell
# Falsch (??-Operator fängt '' nicht ab):
$home = $env:HOME ?? $env:USERPROFILE
# Richtig:
$home = if ($env:HOME) { $env:HOME } else { $env:USERPROFILE }
```

### CI: Scanner-Verzeichnis / CI: Scanner Directory
```bash
# Falsch (CWD = Repo-Root, Dateien nicht gefunden):
bash scripts/check-homogeneity.sh home-baseline
# Richtig (aus dem Parent heraus):
cd "$(dirname "$GITHUB_WORKSPACE")"
bash "$(basename "$GITHUB_WORKSPACE")/scripts/check-homogeneity.sh" "$(basename "$GITHUB_WORKSPACE")"
```

### `.gitignore`-Whitelist / `.gitignore` Whitelist
Jede neue Datei muss explizit als `!DATEINAME` in `.gitignore` eingetragen werden, sonst wird `git add` lautlos ignoriert (z. B. `LICENSE`).

### `bootstrap-workspace`: GitHub-Username / `bootstrap-workspace`: GitHub Username
Früher hardcodiert. Jetzt dynamisch:
```bash
GH_USER=$(gh api user --jq '.login')
```

### Doppelte Überschriften in TOC / Duplicate heading anchors
Gleiche Heading-Texte → GitHub hängt `-1`, `-2` an. TOC-Links für zweite Vorkommen müssen den Suffix enthalten.

### Pflicht für bilinguale Headings / Bilingual Heading Requirement
Format: `## DE / EN` — immer. Nur-Deutsch verletzt WCAG 2.4.6 und bilinguales Konsistenzgebot.
Ausnahme: Eigennamen wie `### Homogeneity Guardian` oder `### Compliance-Check`.

### Code-Blöcke immer mit Sprach-Tag (WCAG 4.1.1) / Code Blocks Must Always Have a Language Tag (WCAG 4.1.1)
Bare ` ``` ` ohne Sprache ist ein A11Y-Fehler. Für ASCII/Dialog/Verzeichnisse: ` ```text `.

### CHANGELOG.md hinzugefügt / CHANGELOG.md Added
Dokumentiert Versionen v0.1.0–v0.4.0. Muss in `.gitignore`-Whitelist (`!CHANGELOG.md`) eingetragen sein.

### ASCII-Box-Drawing-Tabellen: Zeilenbreite / ASCII Box-Drawing Tables: Line Width
Alle Zeilen einer `text`-Code-Block-Tabelle müssen exakt gleich breit sein. Ein überzähliges Leerzeichen vor dem schließenden `│` macht die Zeile 1 Zeichen zu lang.
Prüfen: PowerShell `$line.Length` oder `wc -m` (Bash) für jede Rahmen-Zeile.

### Spec-Kit-Verzeichnis initialisieren / Initialize the Spec-Kit Directory
Nie manuell aus `~/home-baseline-tmp/` kopieren. Stattdessen:
`specify init --here --force --integration {agent}` je Agent für `agy`, `opencode`, `claude`, `copilot` und `codex` ausführen.

### Spec-Kit-Updates repo-weit / Repository-Wide Spec-Kit Updates
Fuer Level 0, Level 1 und Level 2 nicht mehr per Hand in jedem Repo nachziehen.
Stattdessen zuerst `bash scripts/update-spec-kit.sh --dry-run` bzw.
`pwsh scripts/update-spec-kit.ps1 -WhatIf` ausfuehren, danach bei Bedarf
`--commit --push` / `-Commit -Push`.

Das Skript erkennt neue Repos dynamisch ueber `.git` plus `.specify/`, sichert
`.specify/memory/constitution.md`, legt die lokalen Governance-Templates wieder
auf und nimmt `RiderProjects/TuiVision` normal mit. OpenCode wird nur ueber
`.opencode/command/*.md` getrackt; `.opencode`-Caches, Sessions, Logs,
Credentials und lokale Abhaengigkeiten bleiben ausgeschlossen.

Die Standard-Template-Quelle ist das oeffentliche `home-baseline`-Repo, aus dem
das Skript laeuft. Private Repos wie `RiderProjects/TuiVision` duerfen nur
bewusst mit `--template-source` / `-TemplateSource` als Override genutzt werden.

### GitHub-Housekeeping: Archivierung, Sichtbarkeit, Forks und Stars / GitHub Housekeeping: Archiving, Visibility, Forks, and Stars
`archived` bedeutet bei GitHub nur read-only, nicht unsichtbar. Public archived Repos bleiben ohne Anmeldung sichtbar.
Archivierte Repos sind API-seitig read-only; Sichtbarkeit ändern geht deshalb nur über:
`archived=false` → `private=true` → `archived=true`.

Öffentliche Forks lassen sich nicht einfach auf private setzen. Optionen: öffentlich archiviert lassen, löschen, oder als private Mirror-Repos neu anlegen. Vor Löschungen die Repo-Liste eng festlegen; `gh repo delete` benötigt ggf. `gh auth refresh -h github.com -s delete_repo`.

Für Aktivitätsbewertungen `pushedAt` statt `updatedAt` verwenden, weil `updatedAt` durch Metadatenänderungen springt. Stars sind kontogebundene Metadaten und können über `DELETE /user/starred/{owner}/{repo}` entfernt werden; danach `user/starred` gegenprüfen.

## Einordnung / Scope

Einstiege / Entry files: `GEMINI.md`

Dieses Dokument dient als zentraler Kontext für die Arbeit an **TuiVision**, einer Portierung von Turbo Vision 2.0.3 nach C#/.NET 10.

## Architecture

Einstiege / Entry files: `.github/copilot-instructions.md`

All projects target `net10.0` with `Nullable: enable`, `ImplicitUsings: enable`, and `LangVersion: latest` via `Directory.Build.props`.

`Directory.Build.props` also carries the repo-wide `Version`, `AssemblyVersion`, and `FileVersion` values for all projects using `Major.Minor.Patch.Build`:
- `Minor` = current Spec-Kit feature/branch number, interpreted numerically as the canonical PR number for versioning (`007` -> `7`) and used immediately even before a GitHub PR exists
- `Patch` = current commit count in that feature/PR branch (after committing the current change)
- `Build` = manual build counter incremented before every `dotnet build` or `dotnet test`

On numbered Spec-Kit branches, align those three version fields before pushing.

| Module | Purpose |
|---|---|
| `src/TuiVision.Core` | Foundation: `TObject`, `TPoint`, `TRect`, `TEvent` |
| `src/TuiVision.Controls` | UI components: `TView` (base for all visual elements) |
| `src/TuiVision.Drivers.Console` | Console rendering: `TConsoleCell`, `TConsoleBuffer`, `TConsoleDriver`, `IConsolePresenter` |
| `src/TuiVision.Serialization` | Binary archive system: `TBinaryArchiveWriter/Reader`, `TRecordRegistry`, `TRecordSerializer` |
| `src/TuiVision.Compatibility` | Key code translation: `TKeyCodeTranslator`, `TShiftState` |
| `tests/` | MSTest projects mirroring each `src/` module |
| `examples/` | Ported example programs; `TuiVision.Examples.SmokeTests` covers integration-level tests |
| `tv203s/contrib/tvision/` | Original C/C++ source — **read-only reference, never modify** |



> **Note:** `src/TuiVision.Drivers.Console`, `src/TuiVision.Serialization`, and `src/TuiVision.Compatibility` currently store all their code in `Class1.cs` — a scaffold artifact. New types in those modules go in that file until it is split.

### Key design patterns

- **Event system**: `TEvent` is created only via static factory methods (`TEvent.CreateKeyDown(...)`, `TEvent.CreateCommand(...)`, etc.). Events are consumed via `TView.HandleEvent()`. Call `event.Clear()` to mark an event as handled.
- **Coordinate system**: `TRect` uses **inclusive top-left (`A`), exclusive bottom-right (`B`)**. `TView` maintains local coordinates; use `MakeLocal`/`MakeGlobal` to convert.
- **Lifecycle**: Override `TObject.ShutDown()` for logical teardown. Use `TObject.Destroy(instance)` (not `new` + GC) to shut down and dispose an object together.
- **Console rendering**: Presenter pattern — `TConsoleDriver` manages a back-buffer (`TConsoleBuffer`) and publishes immutable snapshots via `IConsolePresenter`. Use `TrySetCell` for bounds-safe writes; `WriteText` accepts `ReadOnlySpan<char>` and clips automatically.
- **Serialization**: Envelope-based binary format — `TRecordSerializer` writes `(typeId string, payloadLength int32, payload bytes)`. New serializable types implement `ITStreamSerializable` and register a factory with `TRecordRegistry.Register<T>(typeId, reader => ...)`.
- **Key translation**: `TKeyCodeTranslator.ComposeKeyCode` encodes Turbo Vision key codes as `(scanCode << 8) | charCode`. Named constants (e.g., `KeyEnter = 0x1C0D`) are on `TKeyCodeTranslator`.

## Conventions

Einstiege / Entry files: `.github/copilot-instructions.md`

- **No native dependencies**: No P/Invoke or native interop. All drivers must be pure managed code.
- **Value types**: Use `readonly record struct` for immutable payloads (e.g., `TMouseEvent`, `TKeyDownEvent`). Use `struct` (mutable) for geometry types like `TPoint` and `TRect` that match original Turbo Vision mutation semantics.
- **Flags enums**: Use `[Flags]` enums (e.g., `TEventKind`, `TViewState`, `TViewOptions`) matching original Turbo Vision bitmask values.
- **JSON handling**: Use `System.Text.Json` for project-owned JSON parsing and serialization. Introduce `Newtonsoft.Json` only with documented justification and explicit reviewer approval.
- **XML documentation**: All public APIs require `<summary>`, `<param>`, and `<returns>` XML comments. Explanatory documentation blocks must be **bilingual: German first, English second**, both at CEFR-B2 readability. Update docs in the same commit as the API change.
- **Didactic inline comments**: New or changed non-trivial logic must be reviewed for didactic inline-comment value when it affects learner understanding or maintainability, especially central framework flows and smoke-test helpers. Comments explain why a decision, trade-off, constraint, historical deviation, or proof boundary exists; they must not restate obvious code. Keep the normal intensity to 1-3 lines before a non-trivial block, with German-first/English-second CEFR-B2 text for didactic explanation blocks.
- **Large normative docs**: `Pflichtenheft*.md` and `Lastenheft*.md` may use a synchronized English sidecar with suffix `.EN.md` instead of an oversized inline-bilingual file; the German version remains canonical unless explicitly marked otherwise.
- **Inclusive documentation — `Programmierung #include<everyone>`**: Diese Lernbeispiele richten sich an Azubis (Fachinformatiker AE/SI), die auf Deutsch und Englisch arbeiten, **sowie** an sehbehinderte Lernende, die Braille-Displays, Screen-Reader oder Textbrowser nutzen. Barrierefreiheit ist Pflichtanforderung, kein Nice-to-have. Guides, statistics, examples, and generated API docs must stay usable on Braille displays, with screen readers, and in text browsers. Prefer semantic headings, lists, tables, and ASCII/text-first diagrams over purely visual cues.
- **WCAG baseline**: Generated HTML documentation should target WCAG 2.2 conformance level AA, especially for page language, bypass blocks, keyboard focus visibility, non-text contrast, and readable landmark structure.
- **DocFX A11y smoke path**: Keep the Playwright + `@axe-core/playwright` checks in `tests/web-a11y/` aligned with the current generated pages; use `lynx` as an extra text-browser review path when available.
- **DocFX completion rule**: Treat every successful `docfx docfx.json` regeneration as incomplete until the matching `tests/web-a11y/` A11y smoke check has also passed.
- **GitHub Pages publishing**: Publish DocFX through `.github/workflows/pages.yml`; build root `docfx.json`, run the `tests/web-a11y/` Playwright + axe smoke path, upload `_site/` as a Pages artifact, and keep `_site/` plus generated `api/*.yml` files out of Git.
- **Formal documentation finish**: Treat bilingual CEFR-B2 delivery plus the documented A11Y proof path as completion criteria for learner-facing documentation and active requirement artifacts.
- **Test naming**: `ClassName_MethodName_Behavior` (e.g., `TRect_Contains_UsesTopLeftInclusiveBottomRightExclusive`).
- **Branch naming**: Feature branches use either the agent-prefixed form `codex/<feature-description>` (or another supported agent prefix such as `claude/`, `agy/`, legacy-compatible `gemini/`, `copilot/`, `opencode/`) or the numbered Spec-Kit form `NNN-short-description` when the Spec-Kit workflow creates the branch.
- **Lastenheft traceability**: When a dedicated feature branch has implemented the requirements of a Lastenheft, rename that file to `Lastenheft_<topic>.<feature-branch>.md` so the delivered scope stays traceable.
- **Historical source reference policy**: For every Spec-Kit feature implementation that ports, extends, tests, documents, or fixes historically derived Turbo Vision behavior, consult the relevant read-only implementation files under `tv203s/` (`.c`, `.cc`) and, when declarations are needed, the matching C/C++ headers (`.h`, `.hpp`, `.hh`). Record the historical intent and material user-visible or API-visible deviations in `spec.md`, `plan.md`, `tasks.md`, guides, PR evidence, or architecture/security evidence. If the feature has no historical `tv203s` relevance, record a short `N/A` rationale. The C# port modernizes idioms; it does not translate line-for-line.
- **Example wave delivery pattern**: Plan larger mandatory example waves as two Spec-Kit stages when porting/proof work and interactive runtime polish would otherwise be mixed. Stage 1 ports the historical behavior, closes framework prerequisites, and adds deterministic headless or in-process smoke paths with explicit follow-up notes for deferred interactivity. Stage 2 wires those proven functions into visible menus, status lines, desktop controls, dialogs, keyboard paths, and scripted UI-event smoke tests. Do not treat a wave as fully learner-facing complete until Stage 2 is delivered, unless the scope explicitly says the example is only a minimal non-interactive proof.
- **Framework usage gate**: Future Lastenhefte for example waves or example-adjacent framework hardening must include a framework-usage and remediation gate. The later Spec-Kit run must document, per example or contract area, which existing TuiVision framework component is used, whether local special logic exists, and which decision applies: `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, or `FollowUpHardening`. Reusable logic must not remain permanently as a local `examples/` special solution; move repeated or framework-replacing logic into a small framework fix or a dedicated follow-up hardening item.

## Project Statistics

Einstiege / Entry files: `.github/copilot-instructions.md`

- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Within the `## Fortschreibungsprotokoll` table, keep entries in strict chronological order: oldest entry at the top, newest and most recently added entry at the bottom; entries with the same date keep their insertion order.
- Keep a final top-level `## Gesamtstatistik` block as the last section of `docs/project-statistics.md`; do not append later top-level sections after it.
- Inside that final `## Gesamtstatistik` block, maintain compact ASCII-only trend diagrams that show at least the artifact mix, the documented branch/phase curves, the documented acceleration factors from agentic-AI plus Spec-Kit/SDD support, and a direct comparison between experienced-developer effort, Thorsten-solo effort, and the visible AI-assisted delivery window, and refresh them together with every statistics update.
- Keep each short CEFR-B2 explanatory text directly adjacent to its matching ASCII diagram group, ideally immediately before or after it, so apprentices do not need to scroll between explanation and diagram.
- When the data benefits from progression across an X-axis, add simple ASCII X/Y charts as a second visualization layer; keep them approximate, readable in plain Markdown, and explained in CEFR-B2 language.
- ASCII X/Y charts must use fixed-width X slots: every documented phase keeps its slot, missing values stay blank, and overly wide future series are split into labelled blocks such as `0..15`, `16..31`, and `32..47`, each with its own axis line and X labels.
- Keep the statistics section plain-text friendly for Braille displays, screen readers, and text browsers; the ASCII diagrams and their explanations must stay understandable without relying on color or visual layout alone.
- When DocFX content, documentation navigation, or API presentation changes, validate representative `_site/` pages through a text-oriented review path, preferably with a local Playwright accessibility snapshot.
- Use WCAG 2.2 AA as the concrete review baseline for generated HTML documentation, especially for page language, bypass blocks, keyboard focus visibility, non-text contrast, and readable landmark structure.
- Each update must capture branch/phase, observable work window, production/test/documentation line counts, main work packages, the conservative manual baseline of 80 code lines per day for an experienced developer, and the repo-specific Thorsten-Solo comparison baseline of 125 lines per workday for this Pascal/Turbo-Vision-derived port.
- When reporting acceleration, compare both manual references against visible Git active days and label the result as a blended repository speedup rather than a stopwatch measurement.
- When hour values are shown, convert the day-based estimates with the TVoeD working-day baseline of `7.8 hours` (`7h 48m`) per day.

## Workspace Baseline (vollständig aus `RiderProjects/.github/copilot-instructions.md`)

Einstiege / Entry files: `.github/copilot-instructions.md`

Diese Regeln gelten für alle Repositories in diesem Workspace. Projektspezifische Regeln in dieser Datei haben Vorrang, wenn sie konkreter sind. GitHub Copilot liest keine übergeordneten `copilot-instructions.md`-Dateien automatisch; daher sind die Workspace-Regeln hier vollständig eingebettet.

### Dokumentation
- Leitprinzip: `Programmierung #include<everyone>` — Diese Lernbeispiele richten sich an Azubis (Fachinformatiker AE/SI), die auf Deutsch und Englisch arbeiten, **sowie** an sehbehinderte Lernende, die Braille-Displays, Screen-Reader oder Textbrowser nutzen. Barrierefreiheit ist Pflichtanforderung, kein Nice-to-have. / *These learning examples target apprentices working in German and English, **and** visually impaired learners using Braille displays, screen readers, or text browsers. Accessibility is mandatory.*
- Deutsch und Englisch zielen beide auf CEFR-B2-Lesbarkeit; Reihenfolge: **Deutsch zuerst, Englisch danach**.
- **Die deutsche Fassung ist kanonisch**, außer dieses Repository markiert eine andere Sprache explizit als primär.
- Große normative Dokumente (`Pflichtenheft*.md`, `Lastenheft*.md`) verwenden eine synchronisierte `.EN.md`-Sidecar-Datei statt einer überlangen Inline-Zweisprachigkeit.
- Bilinguales CEFR-B2-Deliverable ist ein **formales Abnahmekriterium** für learner-facing Dokumentation und aktive Anforderungsartefakte.

### Barrierefreiheit (Accessibility)
- Generiertes HTML-Dokumentation muss **WCAG 2.2 Level AA** erfüllen.
- Semantische Überschriften, Listen, Tabellen und ASCII/Text-First-Diagramme bevorzugen.
- **Wesentliche Bedeutung NICHT nur durch Farbe, Layout oder Maus-only-Affordances kodieren.**
- Guides, Statistiken, Beispiele und generierte API-Dokumentation müssen in text-first Assistive-Setups lesbar bleiben.
- Der dokumentierte **A11Y-Nachweispfad ist ein formales Abnahmekriterium** für learner-facing Dokumentation und aktive Anforderungsartefakte.

### DocFX-Review-Regel
- Wenn ein Repository Dokumentation mit `docfx` neu generiert, muss **dasselbe Work-Item** auch den passenden A11Y-Review ausführen.
- Bevorzugtes Toolchain: **Node 24 LTS**, **`npm`**, **`@axe-core/playwright`**, **`lynx`**.
- Playwright + axe für automatisierte Smoke-Checks verwenden; `lynx` als zusätzlichen Textbrowser-Prüfpfad.

### Statistik-Ledger
- `docs/project-statistics.md` als lebendes Ledger pflegen, wenn diese Datei im Repository existiert.
- Den abschließenden Top-Level-Block `## Gesamtstatistik` als letzten Abschnitt halten.
- ASCII-Diagramme textbrowserfreundlich halten und **kurze CEFR-B2-Erklärungen direkt neben** das jeweilige Diagramm platzieren.
- Dokumentierte **Beschleunigungsfaktoren aus Agentic AI plus Spec-Kit/SDD** einschließen sowie einen Vergleich zwischen experienced-developer-Aufwand, Thorsten-solo-Aufwand und dem sichtbaren AI-assisted-Delivery-Fenster (sofern dieses Repository diese Metriken führt).

### Änderungsdisziplin
- **Nicht davon ausgehen**, dass eine Cross-Repository-Regel projekt-spezifische Build-, Test- oder Release-Anforderungen ersetzt.
- Wenn eine gemeinsame Regel sich ändert und mehrere Repositories betroffen sind, lokale Projektguidance **und** das jeweilige Statistik-Ledger gemeinsam aktualisieren.
- `CODEX_CROSS_REPO_PROMPTS.md` synchron halten, wenn sich übergreifende Prompting-Guidance ändert, damit der wiederverwendbare Prompt mit der aktuellen Baseline übereinstimmt.

## Einordnung / Scope

Einstiege / Entry files: `.github/copilot-instructions.md`

TuiVision is a C#/.NET 10 port of the Turbo Vision 2.0.3 TUI framework (originally C/C++). It is a managed, modernized interpretation — not a line-for-line translation.

## Code Style

Einstiege / Entry files: `.github/agents/copilot-instructions.md`

C# `latest` on .NET 10 (`net10.0`): Follow standard conventions. All XML docs and explanatory comments must be bilingual: German first, English second, CEFR-B2 readability. Large normative documents such as `Pflichtenheft*.md` and `Lastenheft*.md` may use a synchronized English sidecar with suffix `.EN.md` instead of an oversized inline-bilingual file; the German version remains canonical unless explicitly marked otherwise. Follow `Programmierung #include<everyone>`: guides, statistics, examples, and generated API docs must stay usable in text-first assistive setups such as Braille displays, screen readers, and text browsers. Generated HTML documentation should target WCAG 2.2 conformance level AA. Keep the Playwright + `@axe-core/playwright` smoke checks in `tests/web-a11y/` aligned with the current DocFX structure and use `lynx` as an extra text-browser spot check when available. Every successful `docfx docfx.json` regeneration must be followed by the matching `tests/web-a11y/` A11y smoke check in the same work item. Treat bilingual CEFR-B2 delivery plus the documented A11Y proof path as formal completion criteria for learner-facing documentation and active requirement artifacts. New or changed non-trivial logic must be reviewed for didactic inline-comment value when it affects learner understanding or maintainability, especially central framework flows and smoke-test helpers. Inline comments explain why a decision, trade-off, constraint, historical deviation, or proof boundary exists; do not add comments that merely restate obvious code. Keep inline-comment intensity moderate: normally 1-3 lines before a non-trivial block, with German-first/English-second CEFR-B2 text for didactic explanation blocks.

GitHub Pages is published from `.github/workflows/pages.yml`: build root `docfx.json`, run the `tests/web-a11y/` Playwright + axe smoke path, upload `_site/` as a Pages artifact, and keep `_site/` plus generated `api/*.yml` files out of Git.

## Autonome Spec-Kit-Läufe / Autonomous Spec-Kit Runs

Einstiege / Entry files: `.github/agents/copilot-instructions.md`

- Vollständig delegierte Spec-Kit-Läufe folgen `docs/spec-kit-autonomous-runbook.md` und verwenden den projektgebundenen Skill `$speckit-autonomous`.
- Vor dem Start muss der Delivery-Modus `LocalImplementation`, `PublishPR` oder `MergeAndSync` aus dem aktuellen Benutzerauftrag bestimmt werden. Allgemeine Autonomie erteilt keine stillschweigende Remote-Schreib- oder Merge-Berechtigung.
- Evidence wird vor der ersten Implementierungsänderung angelegt. Clarify, Checklists, Analyze, Implement und Remote Review werden bis zu den im Runbook definierten Konvergenzkriterien ausgeführt, nicht nach einer festen Wiederholungszahl.
- Ein repräsentativer vertikaler Slice mit Test und Proof kommt vor der breiten Wiederholung. Gemeinsame Evidence-, Versions-, Statistik-, Workflow- und Agent-Dateien bleiben Single-writer-Flächen.
- Jede Remote- oder Delivery-Task nennt den konkreten Repository-Evidence-Pfad für ihr Abnahmeergebnis; implizite Evidence-Verweise reichen für Analyze und Resume nicht aus.
- Aktuelle Check-/Review-Fakten werden vor dem Merge geprüft, aber in genau einen benannten Closeout-Evidence-Pfad verschoben, wenn ihr Commit den geprüften Feature-Head und damit die Aussage selbst entwerten würde.
- Jede manuelle Build-Zählererhöhung gilt für genau einen expliziten `dotnet build`- oder `dotnet test`-Aufruf; mehrere Aufrufe dürfen nicht hinter einer Erhöhung verkettet werden.
- Repository-Prüfhelfer erhalten den Repository-Root explizit. Ein Pass benötigt erwarteten Exitcode und einen Fehlerkanal ohne PowerShell-ErrorRecord, `command not found` oder gleichwertige fatale Signatur.
- Vor einem Commit wird der beabsichtigte Lieferkandidat separat gestaged und mit `git diff --cached --check` sowie einem Pfadvergleich gegen den Repository-Status geprüft; `git diff --check` allein erfasst keine ungetrackten Dateien, und fremde Änderungen bleiben ausgeschlossen.
- Vor einem Merge wird jedes Acceptance-Gate dem tatsächlichen Workflow, Job, Betriebssystem und ausgeführten Command zugeordnet; ein grüner Aggregatstatus oder ein Plattformname gilt nicht als Proof, wenn der Job den geforderten Runtime-, Plattform-, Dokumentations- oder Security-Nachweis nicht ausgeführt hat.
- Optional installiert: `autonomous-run-governance` v0.4.12 prio 70 aus dem öffentlichen Tag-ZIP. Der projektgebundene Codex-Skill `$speckit-autonomous` bleibt am selben eindeutigen Pfad als lokaler Override bestehen, weil er TuiVision-spezifische Nummerierungs-, Build-Zähler-, DocFX-/A11Y- und historische Source-Verträge ergänzt; Preset-Command, Retrospektiv-Skill, Runbook und Adoption-Evidence bleiben die portablen beziehungsweise gemeinsamen Nachweisflächen. Version 0.2.2 behält deklarierte Acceptance-Gates, exakte HEAD-Evidence, Status, Stop, Resume und validierten Laufzustand bei. Nach Preset- oder Governance-Drift gleicht Resume neue zwingende Korrektheits-, Sicherheits-, Berechtigungs- und Evidenzregeln mit akzeptierten Plan-, Task- und Checklist-Artefakten ab; nur anwendbare fehlende Regeln werden in-place ergänzt und erneut analysiert, reine Effizienzpräferenzen bleiben retrospektiv. Grüne Namen, Validator und Bypass ersetzen weder technischen Nachweis noch Remote- oder Merge-Berechtigung; `PausedByUser` wird nie stillschweigend fortgesetzt und unsicher beendete Operationen bleiben `NeedsRevalidation`. Die lesbare Skill-Überschrift `Deliver` ist kein Run-State-Wert; für Remote-Closeout gelten `Publish`, `Review` oder `MergeAndSync`.
- Jeder Lauf schützt den akzeptierten Scope, verwendet triggerbasierte Validierung und dokumentiert eine kurze Retrospektive für spätere Runbook-Verfeinerungen.

*Fully delegated Spec-Kit runs follow `docs/spec-kit-autonomous-runbook.md` and use the repository-local `$speckit-autonomous` skill. Determine `LocalImplementation`, `PublishPR`, or `MergeAndSync` from the current user request; general autonomy does not silently grant remote write or merge authority. Create evidence before implementation, iterate optional stages to their defined convergence criteria, prove one representative vertical slice before broad rollout, serialize shared writers, require each remote or delivery task to name its exact repository evidence path, route self-invalidating reviewed-head facts to one named closeout path after verifying them before merge, validate the exact staged candidate with `git diff --cached --check` so new files are included without unrelated changes, map each acceptance gate to the actual workflow, job, platform, and executed command before merge, protect accepted scope, use trigger-based validation, and record a short retrospective for later workflow refinement.*

*Optional installed preset: `autonomous-run-governance` v0.2.2 at priority 70 comes from the public tag ZIP. The project-owned Codex `$speckit-autonomous` skill stays at its single path as a local override because it adds TuiVision-specific numbering, build-counter, DocFX/A11Y, and historical-source contracts. The preset command, retrospective skill, runbook, and adoption evidence remain the portable or shared proof surfaces. Version 0.2.2 retains declared acceptance gates, exact-HEAD evidence, status, stop, resume, and validated run state. After preset or governance drift, resume compares new mandatory correctness, security, permission, and evidence rules with accepted Plan, Tasks, and checklist artifacts; only applicable missing rules are amended in place and analyzed again, while efficiency-only guidance stays retrospective. Green names, the validator, and bypass grant neither technical proof nor remote or merge authority; `PausedByUser` is never resumed silently, and uncertain operations remain `NeedsRevalidation`. The readable skill heading `Deliver` is not a run-state value; remote closeout uses `Publish`, `Review`, or `MergeAndSync`.*
- Aktive Google-Agentenoberfläche ist Antigravity CLI mit Befehl `agy` und Spec-Kit-Integration `agy`. `GEMINI.md` und `~/.gemini/antigravity-cli/` bleiben Antigravity-kompatible Oberflächen; direkte `gemini`-Befehle sind nur historische oder ausdrücklich benötigte Enterprise-/API-Kompatibilität und keine lokale Pflicht.

*The active Google agent surface is Antigravity CLI through the `agy` command and Spec Kit `agy` integration. `GEMINI.md` and `~/.gemini/antigravity-cli/` remain Antigravity-compatible surfaces; direct `gemini` commands are historical or explicitly required enterprise/API compatibility, not a local requirement.*

## Shared Parent Guidance

Einstiege / Entry files: `.github/agents/copilot-instructions.md`

- The shared parent file `/Users/thorstenhindermann/RiderProjects/AGENTS.md` intentionally stores only repo-spanning baseline rules.
- Keep repository-specific build, test, workflow, architecture, and feature guidance in this repository's own files; when both layers exist, the repository-local files are the more specific authority.

## Einordnung / Scope

Einstiege / Entry files: `.github/agents/copilot-instructions.md`

Auto-generated from all feature plans. Last updated: 2026-07-17
