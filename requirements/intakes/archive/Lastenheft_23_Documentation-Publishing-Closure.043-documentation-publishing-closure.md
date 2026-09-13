<!-- intake-authoring:begin -->
# Lastenheft 23: Documentation and Publishing Closure

## Dokumentstatus / Document Status

- Status: `ReadyForReview`
- Feature-ID: erst bei Auswahl reservieren
- Delivery Authority: `LocalImplementation`
- Priorität: nach dem Wave-6-Closeout und unabhängig vom Portfolioaudit

*Ready for review. Reserve a feature ID only when selected. Delivery authority
is local implementation. Run after Wave-6 closure and independently from the
portfolio audit.*

## Ziel / Goal

Die nach der Pflichtenheft-Reconciliation verbliebenen Dokumentations-,
Publishing- und Multi-Agent-Workflow-Lücken werden ohne Framework- oder
Beispielrevision geschlossen.

*Close the remaining documentation, publishing, and multi-agent workflow gaps
identified by the Pflichtenheft reconciliation without revising framework or
example behavior.*

## Anforderungen / Requirements

1. Liefere die fehlenden Einstiegs-, Architektur-, Event-Loop-, View-,
   Koordinaten-, Serialisierungs- und First-Dialog-Guides.
2. Prüfe Lernziel, Voraussetzungen, Start, Bedienung, Architekturhinweis und
   Übung für alle Beispiel-Guides; ergänze nur belegte Lücken.
3. Vereinheitliche German-first/English-second CEFR-B2, semantische Struktur
   und text-first A11Y auf den aktiven Lernoberflächen.
4. Aktualisiere Multi-Mac- und Agent-Workflow-Dokumentation auf `agy` als
   operative Google-Agentenoberfläche; historische Gemini-Kompatibilität
   bleibt klar gekennzeichnet.
5. Weise DocFX-Pages, Playwright/Axe, Release-CS1591 und dokumentationsrelevante
   CI-Gates reproduzierbar nach.
6. Dokumentiere bewusste Abweichungen vom historischen Turbo-Vision-Verhalten
   in einem auffindbaren, wartbaren Changelog- oder Guide-Pfad.
7. Änderungen an XML/API lösen DocFX plus A11Y aus; reine Markdown-Arbeit
   verändert keine öffentliche API.

## Nicht-Ziele / Non-Goals

- keine Runtime-, API-, Abhängigkeits- oder Beispielverhaltensänderung
- keine pauschale Neuformulierung bereits guter Guides
- keine NuGet-Paketierung
- keine erneute historische Konformitätsprüfung

## Abnahme / Acceptance

Alle in der Reconciliation der Gruppe `DocumentationAndPublishing`
zugeordneten offenen oder teilweisen Aussagen besitzen entweder Abschluss-
Evidence oder eine begründete, ausdrücklich akzeptierte Restgrenze.

<!-- intake-authoring:prompts -->
## Kopierbare Spec-Kit-Prompts / Copyable Spec Kit Prompts

<!-- spec-kit-command-id: speckit.specify -->
```text
$speckit-specify Use requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md as the binding intake. Allocate the next eligible feature number only when this intake is selected. Create or update only the matching feature specification. Preserve the documentation-only scope and evidence boundaries. Do not implement, commit, push, create a pull request, merge, or change product behavior.
```

<!-- spec-kit-command-id: speckit.autonomous -->
```text
$speckit-autonomous Execute one complete autonomous Spec Kit run using requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md as the binding intake. Delivery mode: LocalImplementation. Preserve the documentation-only scope, German-first/English-second CEFR-B2 policy, text-first accessibility, and conditional DocFX/A11Y triggers. Do not push, create or merge a pull request, use bypass authority, start another feature, or change runtime, API, dependency, project, or example behavior.
```
<!-- intake-authoring:end -->
