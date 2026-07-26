<!-- intake-authoring:begin -->
# Lastenheft: Constitution Change fuer didaktische und sprachliche Klarheit

**Dokument-Status:** Spec-Kit-Eingabedatei, bereit fuer `/speckit-specify`
**Erstellt:** 2026-03-28
**Bereinigt:** 2026-05-11
**Betrifft:** `.specify/memory/constitution.md`, `.specify/templates/`,
`AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`,
`.github/agents/copilot-instructions.md`, `docfx.json`, Dokumentations- und
XML-Kommentarregeln
**Empfohlene Prioritaet:** vor neuen grossen Spec-Kit-Features pruefen, wenn
Dokumentations-, Sprach- oder Governance-Regeln geaendert werden

---

## 0. Spec-Kit-Intake-Zusammenfassung / Spec-Kit Intake Summary

Dieses Lastenheft beschreibt eine Governance-Aenderung: Die Projektverfassung
und die abhaengigen Templates sollen klarer festlegen, dass TuiVision
didaktisch, zweisprachig und barrierearm dokumentiert wird. Deutsch steht
zuerst, Englisch danach. Beide Sprachfassungen sollen ungefaehr CEFR-B2
erreichen, damit auch nicht-muttersprachliche Auszubildende die Inhalte
verstehen koennen.

This requirements document describes a governance change: the project
constitution and dependent templates shall state more clearly that TuiVision is
documented in an educational, bilingual, and accessible way. German comes
first, followed by English. Both language versions should be roughly CEFR-B2 so
non-native trainees can understand the material.

- Feature-Ziel: Constitution, Templates und Agent-Guidance fuer didaktische
  und sprachliche Klarheit harmonisieren.
- Nichtziel: Keine fachliche Framework-Portierung, keine Beispielwelle, keine
  DocFX-Generierung als Selbstzweck.
- Abschlussgrenze: Die Regeln sind in Constitution, Templates und betroffenen
  Agent-Dateien konsistent sichtbar; ein spaeteres Feature weiss, welche
  Dokumentations- und A11Y-Pflichten gelten.

- Feature goal: harmonize constitution, templates, and agent guidance for
  educational and linguistic clarity.
- Non-goal: no functional framework port, no example wave, no DocFX generation
  for its own sake.
- Completion boundary: the rules are consistently visible in the constitution,
  templates, and affected agent files; later features know which documentation
  and accessibility duties apply.

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

TuiVision ist ein Lern- und Portierungsprojekt. Code, Tests, Beispiele und
Dokumentation sollen nicht nur funktionieren, sondern auch fuer Auszubildende
nachvollziehbar sein. Die bisherigen Notizen forderten bereits Deutsch und
Englisch, CEFR-B2-Naehe, XML-Dokumentation, DocFX und didaktische Kommentare.
Diese Anforderungen muessen als klare Governance-Regeln in die Spec-Kit-
Oberflaechen uebertragen werden.

TuiVision is a learning and porting project. Code, tests, examples, and
documentation must not only work; trainees must also be able to understand
them. Earlier notes already required German and English, approximate CEFR-B2
language, XML documentation, DocFX, and educational comments. These
requirements must become clear governance rules in the Spec-Kit surfaces.

---

## 2. Anforderungen / Requirements

### CC-01: Didaktische und sprachliche Klarheit

Die Constitution soll das Prinzip "Didaktische Klarheit" zu "Didaktische und
sprachliche Klarheit" erweitern. Dokumentation, Guides, API-Texte und
lernerorientierte Kommentare muessen Deutsch zuerst und Englisch danach
liefern.

The constitution shall extend the principle "Pedagogical Clarity" to
"Pedagogical and Linguistic Clarity". Documentation, guides, API text, and
learner-oriented comments must provide German first and English second.

### CC-02: CEFR-B2 als Lesbarkeitsziel

Deutsche und englische Texte sollen ungefaehr CEFR-B2 erreichen. Das Ziel ist
nicht akademische Vereinfachung, sondern klare Fachsprache fuer angehende
Fachinformatikerinnen und Fachinformatiker.

German and English text should roughly reach CEFR-B2. The goal is not academic
oversimplification, but clear technical language for future IT application
development specialists.

### CC-03: XML-Dokumentation fuer oeffentliche APIs

Oeffentliche Typen, Member, Parameter, Rueckgabewerte und relevante
Ausnahmen muessen vollstaendige XML-Dokumentation erhalten. `summary`,
`param`, `returns` und `exception` sind zu nutzen, wenn sie fachlich passen.

Public types, members, parameters, return values, and relevant exceptions must
have complete XML documentation. `summary`, `param`, `returns`, and
`exception` shall be used where they are meaningful.

### CC-04: Kommentare erklaeren Entscheidungen

Block- oder Zeilenkommentare sollen dort eingesetzt werden, wo sie eine
didaktische Entscheidung, einen Trade-off oder eine Einschraenkung erklaeren.
Sie sollen nicht triviale Codezeilen wiederholen.

Block or line comments should be used where they explain an educational
decision, trade-off, or constraint. They should not repeat trivial code lines.

### CC-05: Dokumentationsaenderungen ziehen Validierung nach sich

Wenn API-Signaturen, XML-Kommentare, DocFX-Navigation oder sichtbare
Dokumentationsausgabe geaendert werden, muss der passende DocFX- und
A11Y-Pruefpfad im selben Arbeitsgang geplant werden.

If API signatures, XML comments, DocFX navigation, or visible documentation
output change, the matching DocFX and accessibility validation path must be
planned in the same work item.

### CC-06: Agent-Guidance bleibt synchron

Wenn die Constitution oder die Spec-Kit-Templates diese Regeln aendern,
muessen die betroffenen Agent-Dateien im selben Feature-Lauf geprueft und bei
Bedarf synchronisiert werden.

If the constitution or Spec-Kit templates change these rules, the affected
agent guidance files must be checked and synchronized in the same feature run
where needed.

### CC-07: Tests bleiben didaktisch nachvollziehbar

Neue Features sollen, wo sinnvoll, testgetrieben geplant werden: erst ein
roter oder fehlender Nachweis, danach die Implementierung, danach gruene
Validierung. Aufgaben und Evidence sollen diesen Weg fuer Lernende sichtbar
machen.

New features should be planned test-first where useful: first a red or missing
proof, then implementation, then green validation. Tasks and evidence should
make this path visible for learners.

---

## 3. Nicht im Scope / Out of Scope

- keine konkrete Beispielwelle
- keine neue Controls-, Driver- oder Serialization-Funktion
- keine generierten DocFX-Artefakte im Commit
- keine Absenkung der bestehenden A11Y- oder Coverage-Gates
- keine rein kosmetische Umschreibung ohne Governance-Wirkung

- no concrete example wave
- no new controls, driver, or serialization feature
- no generated DocFX artifacts in the commit
- no lowering of existing accessibility or coverage gates
- no purely cosmetic rewrite without governance effect

---

## 4. Akzeptanzkriterien / Acceptance Criteria

- Constitution und relevante Templates nennen Deutsch zuerst, Englisch danach,
  CEFR-B2-Orientierung und text-first A11Y als Completion-Kriterien.
- Betroffene Agent-Guidance-Dateien sind geprueft und bei Bedarf
  synchronisiert.
- Public-API-Dokumentationspflichten und DocFX-/A11Y-Validierung sind klar
  beschrieben.
- Der spaetere Spec-Kit-Lauf dokumentiert, welche Dateien geaendert wurden und
  warum keine fachliche Framework-Portierung im Scope liegt.

- The constitution and relevant templates name German-first, English-second,
  CEFR-B2 orientation, and text-first accessibility as completion criteria.
- Affected agent guidance files are checked and synchronized where needed.
- Public API documentation duties and DocFX/accessibility validation are
  described clearly.
- The later Spec-Kit run documents which files changed and why no functional
  framework port is in scope.

---

## 5. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
Ersetzter Alt-Prompt: speckit-specify Nutze Lastenheft_Constitution_Change.md als verbindliche Eingabe. Erstelle die Feature-Spezifikation fuer eine Governance-Aenderung zu didaktischer und sprachlicher Klarheit in TuiVision.

Ziel: Constitution, Spec-Kit-Templates und betroffene Agent-Guidance sollen konsistent festlegen, dass user-facing Dokumentation und Public-API-Dokumentation Deutsch zuerst und Englisch danach liefern, ungefaehr CEFR-B2 erreichen und text-first A11Y als Completion-Kriterium behandeln.

Pflicht:
- Anforderungen Deutsch zuerst und Englisch danach, CEFR-B2 und text-first A11Y formulieren.
- Constitution, Templates und Agent-Guidance gemeinsam pruefen und betroffene Dateien synchronisieren.
- Public-API-XML-Dokumentation, didaktische Kommentare, DocFX-Validierung und web-a11y-Smoke als Governance-Regeln klaeren.
- TDD-/Test-first-Nachweise dort verlangen, wo sie Lernenden den Entwicklungsweg zeigen.
- Keine konkrete Beispielwelle, keine Framework-Portierung und keine generierten DocFX-Artefakte in diesen Lauf ziehen.
```

---

## Spec-Kit-Intake-Reife / Spec Kit Intake Readiness

Dieses Lastenheft enthaelt kopierbare `$speckit-specify`- und
`$speckit-autonomous`-Prompts. Vor dem Start muss der aktuelle Repository-Stand
trotzdem geprueft werden. Bereits erledigte oder branch-suffig archivierte
Punkte werden nicht erneut umgesetzt; offene Punkte werden als `Applicable`,
`AlreadySatisfied`, `N/A`, `Open` oder `FollowUp` klassifiziert.

*This requirements document contains copyable `$speckit-specify` and
`$speckit-autonomous` prompts. Before starting, still check the current
repository state. Completed or branch-suffixed archived items are not
implemented again; open items are classified as `Applicable`,
`AlreadySatisfied`, `N/A`, `Open`, or `FollowUp`.*

---

## 6. Kopierbarer Autonomous-Prompt / Copyable Autonomous Prompt

```text
Ersetzter Alt-Prompt: speckit-autonomous Use `Lastenheft_Constitution_Change.md` as the binding
intake for a complete autonomous governance review in MergeAndSync mode.

Start only from clean synchronized main. Determine the next free Spec Kit
feature number without reusing an archived feature. First classify every
requirement as Applicable, AlreadySatisfied, N/A, Open, or FollowUp. Do not
create an empty feature, branch, commit, or pull request if the current
constitution, templates, and maintained agent guidance already satisfy the
complete intake.

Run Specify, repeated Clarify, requirements and governance checklists, Plan,
plan review, Tasks, repeated Analyze, Implement, validation, delivery, and
retrospective to convergence. Keep the scope limited to constitution,
repository-owned Spec Kit templates, maintained agent guidance, and directly
triggered documentation evidence.

Require German-first/English-second CEFR-B2 documentation, text-first WCAG
2.2 AA guidance, complete public-API XML documentation rules, selective
didactic inline comments, and test-first evidence where it improves learner
understanding. Run DocFX and web-A11Y validation when their trigger surfaces
change. Do not port framework behavior, start an example wave, lower existing
gates, or commit generated DocFX output.

Treat provider settings, branch protection, credentials, legal approval, and
other human-only decisions as Open or N/A with owner and re-evaluation
trigger. Use remote authority only for this repository's non-empty feature
delivery. Merge only after all applicable checks and reviews converge, then
return to clean synchronized main.
```
<!-- intake-authoring:prompts -->
## Kopierbare Spec-Kit-Prompts / Copy-Ready Spec Kit Prompts

Die folgenden Alternativen starten keinen Lauf automatisch. Der autonome
Prompt ist auf `LocalImplementation` begrenzt und erteilt keine Remote-,
PR-, Merge-, Bypass-, Secret- oder Provider-Berechtigung.

*The alternatives below do not start a run automatically. The autonomous
prompt is limited to `LocalImplementation` and grants no remote,
pull-request, merge, bypass, secret, or provider authority.*

### Specify

<!-- spec-kit-command-id: speckit.specify -->
```text
$speckit-specify Use Lastenheft_Constitution_Change.md as the binding intake. Preserve its scope, non-goals, ordering, governance, evidence, and acceptance criteria. Create or update only the matching feature specification. Do not implement, commit, push, create a pull request, merge, or start another feature.
```

### Autonomous

<!-- spec-kit-command-id: speckit.autonomous -->
```text
$speckit-autonomous Execute one complete autonomous Spec Kit run using Lastenheft_Constitution_Change.md as the binding intake. Delivery mode: LocalImplementation. Preserve all scope, ordering, security, accessibility, evidence, and acceptance boundaries. Do not push, create or merge a pull request, use bypass authority, expose secrets, or start a follow-up feature.
```
<!-- intake-authoring:end -->
