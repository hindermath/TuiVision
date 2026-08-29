# Feature Specification: Documentation and Publishing Closure

**Branch**: `043-documentation-publishing-closure`

**Status**: Accepted for autonomous delivery

**Input**: `requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md`
**Review**: Series review `6b74e8e5-c605-48c5-b450-1a018b5dd7eb`

## Zweck / Purpose

Feature 043 schließt die nach der Pflichtenheft-Abstimmung verbliebenen
Dokumentations- und Publishing-Luecken. Es schafft nachvollziehbare
Einstiegs-, Architektur- und Lernpfade, prüft die Beispiel-Guides und belegt
den bestehenden DocFX-, A11Y- und Multi-Agent-Workflow reproduzierbar. Runtime,
öffentliche APIs, Abhängigkeiten, Projekte und Beispielverhalten bleiben
unverändert.

Feature 043 closes the documentation and publishing gaps that remained after
the requirements reconciliation. It provides coherent onboarding,
architecture, and learning paths, reviews the example guides, and proves the
existing DocFX, accessibility, and multi-agent workflow reproducibly. Runtime,
public APIs, dependencies, projects, and example behavior remain unchanged.

## User Stories

### US1 - Das Framework systematisch kennenlernen (P1)

Lernende finden einen zusammenhaengenden Einstieg in Installation, Architektur,
Event-Loop, View-Hierarchie, Koordinaten, Serialisierung und den ersten Dialog.

**Independent test**: Eine textbasierte Pruefung weist alle sieben Themen,
gegenseitige Links, ausfuehrbare Befehle und nachvollziehbare Lernschritte nach.

### US2 - Beispiele als Lernpfad verwenden (P1)

Lernende koennen jedes dokumentierte Beispiel anhand eines konsistenten
Vertrags starten, bedienen, architektonisch einordnen und durch eine Uebung
vertiefen.

**Independent test**: Das Guide-Inventar ordnet jedem Beispiel Lernziel,
Voraussetzungen, Start, Bedienung, Architekturhinweis und Uebung oder eine
  begründete, akzeptierte Grenze zu.

### US3 - Dokumentation sicher veroeffentlichen (P1)

Maintainer koennen nachweisen, dass DocFX, GitHub Pages, Release-CS1591 und die
Playwright-/Axe-Pruefung auf dem dokumentierten Stand reproduzierbar bestehen.

**Independent test**: Die kanonischen lokalen und entfernten Gates laufen auf
demselben Feature-Head und ihre Ergebnisse sind in der Feature-Evidence
zugeordnet.

### US4 - Mehrere Agenten und Macs konsistent einsetzen (P2)

Anwendungsentwickler erhalten einen aktuellen Workflow für mehrere Macs und
Agenten. Operative Beispiele verwenden `agy`; historische Gemini-Kompatibilität
ist eindeutig als Legacy-Kontext markiert.

**Independent test**: Eine Inventarpruefung findet keine operative
Gemini-CLI-Anweisung und belegt die gepflegten Agent-Oberflaechen ohne
doppelte oder widerspruechliche Befehle.

### US5 - Bewusste Abweichungen finden (P2)

Lesende können wesentliche bewusste Abweichungen vom historischen
Turbo-Vision-Verhalten über einen Guide- oder Changelog-Pfad auffinden, ohne
eine neue historische Konformitätsprüfung durchzuführen.

**Independent test**: Der Navigationspfad fuehrt zu einer belegten,
quellenbezogenen Zusammenfassung und trennt Produktvertrag, historische
Absicht und moderne Abweichung.

## Functional Requirements

- **FR-001**: Die Dokumentation MUSS zusammenhängende Guides für Getting
  Started, Architektur, Event-Loop, View-Hierarchie, Koordinaten,
  Serialisierung und einen ersten Dialog bereitstellen.
- **FR-002**: Jeder neue Guide MUSS einen klaren Lernzweck, Voraussetzungen,
  nachvollziehbare Schritte, relevante Querverweise und eine prüfbare
  Abschlussgrenze enthalten.
- **FR-003**: Alle vorhandenen Beispiel-Guides MÜSSEN gegen Lernziel,
  Voraussetzungen, Start, Bedienung, Architekturhinweis und Uebung geprueft
  werden. Nur belegte Luecken werden geschlossen; bereits geeignete Inhalte
  bleiben erhalten.
- **FR-004**: Jede Aussage aus dem Abschnitt `DocumentationAndPublishing` der
  Pflichtenheft-Abstimmung MUSS genau einen nachvollziehbaren Abschluss oder
  eine begründete akzeptierte Grenze erhalten.
- **FR-005**: Lern- und Anwenderdokumentation MUSS German-first/English-second,
  CEFR-B2, semantisch strukturiert und in text-first Hilfsmitteln nutzbar sein.
- **FR-006**: Operative Multi-Mac- und Agent-Anweisungen MÜSSEN `agy` verwenden.
  Verbleibende Gemini-Nennungen MÜSSEN als historische oder kompatibilitäts-
  bezogene Ausnahme erkennbar sein.
- **FR-007**: Der bestehende DocFX-/Pages-Pfad, Release-CS1591 und die
  Playwright-/Axe-Prüfung MÜSSEN reproduzierbar und auf dem gelieferten Head
  belegt werden.
- **FR-008**: Wesentliche bewusste historische Abweichungen MÜSSEN über einen
  auffindbaren Guide- oder Changelog-Pfad erreichbar sein.
- **FR-009**: XML-Kommentar-, API-, Navigation- oder Guide-Änderungen MÜSSEN
  DocFX und den zugehörigen A11Y-Pfad auslösen. Reine interne Markdown-
  Evidence darf keine API-Änderung vortäuschen.
- **FR-010**: Die Lieferung DARF Runtime, öffentliche API, Abhängigkeiten,
  Projekte, Beispiele oder deren Verhalten nicht aendern.

## Governance Applicability

- **Security Governance v0.6.2**: Dokumentations-, Link-, Secret- und
  Supply-Chain-Nachweise sind anwendbar. ASVS, SBOM, VEX, SLSA, AI-SBOM und
  regulatorische Produktnachweise sind `N/A`, solange keine Runtime,
  Abhängigkeit, Distribution oder Produkt-KI geändert wird.
- **Architecture Governance v0.5.2** und **iSAQB Architecture Governance
  v0.2.2**: Die erklärende Architekturdokumentation ist anwendbar. Neue
  Runtime-Grenzen, STRIDE/CIA/CAPEC, S-ADR, Zero Trust, SAMM, BSI C3A und BSI
  C5 sind `N/A`, weil keine Architektur- oder Deployment-Grenze geändert wird.
- **A11Y Governance v0.4.3**: Voll anwendbar für semantische, zweisprachige,
  text-first Dokumentation sowie DocFX-/Axe-Nachweise.
- **Cross-Platform Governance v0.2.2**: Plattformneutrale Befehle und Remote-
  Gates sind anwendbar. Neue Skript-Paritaet ist `N/A`, sofern kein Skript
  angelegt oder geändert wird.
- **Agent Parity Governance v0.4.2**: Anwendbar, wenn gepflegte Agent- oder
  Workflow-Oberflächen geändert werden. `.specify/templates/` bleiben `N/A`,
  sofern kein Repository-Template betroffen ist.
- **Historical source policy**: Eine neue historische Auditierung ist `N/A`.
  Vorhandene belegte Abweichungen werden nur auffindbar gemacht; externe und
  historische Quellen bleiben read-only.

## Success Criteria

- **SC-001**: Alle sieben allgemeinen Guide-Themen sind vorhanden, navigierbar
  und bestehen die dokumentierte Struktur- und Linkpruefung.
- **SC-002**: 100 Prozent der vorhandenen Beispiel-Guides haben für alle sechs
  Guide-Kriterien entweder belegten Inhalt oder eine akzeptierte Grenze.
- **SC-003**: 100 Prozent der `DocumentationAndPublishing`-Aussagen haben genau
  einen Abschlussstatus mit Evidence-Pfad.
- **SC-004**: DocFX schließt ohne Warnungen oder Fehler; Playwright/Axe besteht
  ohne A11Y-Verstoß; Release-CS1591 meldet keine fehlende öffentliche
  Dokumentation.
- **SC-005**: Operative Agent-Anweisungen verwenden `agy`; jede verbleibende
  Gemini-Nennung ist als Legacy- oder Kompatibilitaetskontext klassifiziert.
- **SC-006**: Der finale Diff enthält keine Runtime-, API-, Dependency-,
  Projekt- oder Beispielverhaltensaenderung.
- **SC-007**: Alle lokalen und exakten Remote-Gates bestehen auf dem gelieferten
  Head; ein Admin-Bypass deckt hoechstens die allein verbleibende
  Human-Approval-Regel ab.

## Assumptions

- Die vorhandene Framework- und Beispielimplementierung ist die unveränderte
  Produktbasis; Feature 043 beschreibt und prüft sie.
- XML- oder API-Änderungen sind nicht geplant. Falls eine echte
  Dokumentationslücke sie dennoch erfordert, bleibt die Signatur unverändert
  und der normale DocFX-/A11Y-Nachweis gilt.
- Bereits geeignete Guides werden nicht aus Stilgruenden umgeschrieben.
- Der letzte ausdrückliche Benutzerauftrag setzt für diesen Lauf
  `MergeAndSync` und den eng begrenzten Approval-Bypass als Delivery-Autoritaet.

## Non-Goals

Keine Runtime- oder API-Änderung, keine neue Abhängigkeit, kein neues Projekt,
keine Beispielportierung oder -politur, keine NuGet-Veroeffentlichung, keine
breite Neufassung guter Guides und keine neue historische Konformitätsprüfung.
