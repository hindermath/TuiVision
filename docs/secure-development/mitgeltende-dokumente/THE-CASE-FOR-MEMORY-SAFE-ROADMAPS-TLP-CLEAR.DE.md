# The Case for Memory Safe Roadmaps - Deutsche Lernfassung

**Quelle:** [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.pdf](THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.pdf)
**Englische Markdown-Fassung:** [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.EN.md](THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.EN.md)
**CISA-Webseite:** [CISA: The Case for Memory Safe Roadmaps](https://www.cisa.gov/resources-tools/resources/case-memory-safe-roadmaps)
**Stand dieser Lernfassung:** 2026-06-20
**Zielgruppe:** Fachinformatiker*innen in Ausbildung, Entwickler*innen, Reviewer und KI-Agenten

## Hinweise zur Nutzung

Diese Datei ist eine deutsche Lernfassung des CISA-Dokuments. Sie ist keine
amtliche Übersetzung. Sie ist in CEFR-B2-Sprache formuliert und soll den Inhalt
für Ausbildung, Code-Review und sichere Entwicklung verständlich machen.

Das englische Original und die lokale PDF bleiben die maßgebliche Quelle. Diese
Lernfassung erklärt die Hauptaussagen, die Begriffe und die Bedeutung für
Level-2-Projekte.

## Kurzzusammenfassung

Speicherfehler gehören zu den häufigsten Sicherheitslücken in Software. Sie
entstehen vor allem in Sprachen, in denen Entwickler*innen Speicher direkt
verwalten müssen, zum Beispiel C oder C++.

Memory-Safe Languages, kurz MSL, vermeiden viele dieser Fehlerklassen bereits
durch die Sprache oder Laufzeitumgebung. Beispiele sind Rust, C#, Java, Go,
Swift, Python, JavaScript und TypeScript. MSL lösen nicht jedes
Sicherheitsproblem. Sie senken aber deutlich das Risiko für Speicherfehler.

Das CISA-Dokument fordert Softwarehersteller auf, Roadmaps für den Wechsel zu
speichersicheren Sprachen zu erstellen. Eine Roadmap zeigt, welche Produkte,
Bibliotheken und Komponenten umgestellt werden, in welcher Reihenfolge das
passiert und wie Risiken bis dahin begrenzt werden.

## Warum Speicherfehler gefährlich sind

Speicherfehler betreffen den Zugriff auf Speicher im Programm. Ein Programm kann
zum Beispiel außerhalb eines Puffers lesen oder schreiben. Es kann auch Speicher
nutzen, der schon freigegeben wurde.

Solche Fehler können dazu führen, dass Angreifer:

- Daten lesen, die sie nicht lesen dürfen.
- Daten verändern oder beschädigen.
- Schadcode ausführen.
- Kontrolle über den Prozess oder das betroffene Konto bekommen.

Für Auszubildende ist wichtig: Speicherfehler sind keine alten Sonderfälle. Sie
kommen weiterhin in realen Produkten vor und werden aktiv ausgenutzt.

## Warum bisherige Gegenmaßnahmen nicht ausreichen

Das Dokument beschreibt mehrere Schutzmaßnahmen. Sie bleiben nützlich, lösen das
Grundproblem aber nicht vollständig.

### Schulung

Training hilft Entwickler*innen, Fehler zu vermeiden. Es reicht aber nicht aus.
Auch erfahrene Entwickler*innen machen Fehler. Bei Sprachen mit manueller
Speicherverwaltung kann ein kleiner Fehler sehr große Wirkung haben.

### Tests und Code Coverage

Unit-Tests und Integrationstests finden viele Fehler. Hohe Testabdeckung ist
gut, aber Tests können nicht alle Speicherzustände und Randfälle vollständig
prüfen.

### Sichere Coding-Regeln

Secure-Coding-Guidelines sind wichtig. Sie helfen, typische Fehler zu erkennen.
Sie hängen aber davon ab, dass Menschen sie konsequent anwenden und Reviews sie
finden.

### Fuzzing

Fuzzing testet Programme mit vielen zufälligen oder ungültigen Eingaben. Das
findet oft Abstürze und Speicherfehler. Fuzzing findet aber nicht alle Fehler
und wirkt erst nach der Implementierung.

### SAST und DAST

Static Application Security Testing, kurz SAST, prüft Code oder Binärdateien.
Dynamic Application Security Testing, kurz DAST, prüft laufende Software. Beide
Verfahren sind hilfreich. Sie können aber Fehlalarme erzeugen und finden nicht
jede Schwachstelle.

### Sicherere Teilmengen von C und C++

Es gibt Ansätze, C und C++ sicherer zu verwenden. Dazu gehören geprüfte
Compiler, Spracheinschränkungen und zusätzliche Laufzeitprüfungen. Diese Ansätze
helfen besonders bei bestehenden Codebasen. Sie ersetzen aber keinen echten
Wechsel zu einer speichersicheren Sprache.

## Maßnahmen zur Schadensbegrenzung

Einige Maßnahmen verhindern nicht den Speicherfehler selbst. Sie erschweren aber
die Ausnutzung.

### Nicht ausführbarer Speicher

Speicherbereiche können so markiert werden, dass sie nicht als Code ausgeführt
werden dürfen. Das erschwert Angriffe, verhindert aber nicht alle Techniken.

### Control Flow Integrity

Control Flow Integrity, kurz CFI, prüft, ob ein Programm nur erlaubte
Sprungziele nutzt. Wird ein ungültiger Sprung erkannt, kann der Prozess beendet
werden. Auch CFI kann aber umgangen werden.

### Address Space Layout Randomization

Address Space Layout Randomization, kurz ASLR, verändert Speicheradressen bei
jedem Programmstart. Angreifer wissen dadurch schlechter, wo bestimmte Daten
liegen. Wenn ein Programm Adressen verrät, kann ASLR trotzdem umgangen werden.

### Sandboxing

Sandboxing trennt Programmteile voneinander. Ein gefährlicher Parser oder ein
Netzwerkmodul kann dadurch weniger Schaden verursachen. Sandboxes erhöhen aber
Komplexität und können selbst Schwachstellen haben.

### Gehärtete Speicherverwaltung

Gehärtete Allocators machen Angriffe weniger zuverlässig. Sie beseitigen aber
nicht die eigentliche Speicherlücke.

### Hardware-Unterstützung

Neue Hardware-Funktionen wie CHERI oder Memory Tagging Extension können
Speicherzugriffe besser kontrollieren. Diese Technik ist vielversprechend, aber
noch nicht überall verfügbar. Sie ist besonders wichtig, wenn alte Software noch
lange nicht vollständig migriert werden kann.

## Warum MSL wichtig sind

MSL verhindern viele Speicherfehler systematisch. Das ist stärker als nur
nachträgliches Testen oder Härten.

Der Wechsel zu MSL ist deshalb kein reines Technikthema. Er betrifft Planung,
Budget, Architektur, Schulung, Produktstrategie und Wartung. Führungskräfte
müssen den Wechsel unterstützen. Technische Teams müssen die Umsetzung planen.

Für Ausbildungsprojekte gilt: Die Wahl einer MSL ist ein guter Standard. Sie ist
aber kein Freibrief. Auch in MSL-Projekten müssen Eingaben, Datenbanken,
Dateizugriffe, Kryptografie, Authentisierung, Logging und Abhängigkeiten sicher
geprüft werden.

## Planung des Wechsels zu MSL

Eine Migration gelingt nicht zufällig. Sie braucht eine Roadmap.

Eine gute Roadmap beantwortet mindestens diese Fragen:

- Welche Produkte oder Komponenten enthalten speicherunsicheren Code?
- Welche Komponenten sind besonders kritisch?
- Welche Komponenten verarbeiten nicht vertrauenswürdige Eingaben?
- Welche Sprache passt fachlich und technisch zum Zielsystem?
- Welche Teams brauchen Schulung?
- Welche Abhängigkeiten müssen ersetzt oder isoliert werden?
- Welche Risiken bleiben vorübergehend bestehen?
- Wie wird Fortschritt gemessen?

## Priorisierung

Nicht jede Komponente muss gleichzeitig migriert werden. Das Dokument empfiehlt,
risikobasiert zu priorisieren.

Besonders wichtig sind:

- Code, der aus dem Internet erreichbare Eingaben verarbeitet.
- Parser für komplexe Dateiformate.
- Code mit hohen Rechten.
- Komponenten in sicherheitskritischen Produkten.
- Bibliotheken, die von vielen anderen Komponenten genutzt werden.
- Codebereiche mit vielen bekannten Speicherfehlern.

Für Auszubildende ist diese Reihenfolge wichtig: Zuerst werden die Teile
betrachtet, bei denen ein Fehler besonders viel Schaden auslösen kann.

## Auswahl einer MSL

Die beste MSL hängt vom Projekt ab.

Wichtige Kriterien sind:

- Plattform und Laufzeitumgebung.
- vorhandene Bibliotheken.
- Teamwissen.
- Performance-Anforderungen.
- langfristige Wartbarkeit.
- Interoperabilität mit bestehendem Code.
- Tooling, Tests und Sicherheitsanalyse.

Rust ist oft interessant, wenn Systemnähe und Performance wichtig sind. C#,
Java, Kotlin, Go, Swift, Python, JavaScript und TypeScript können je nach
Plattform ebenfalls geeignet sein. Entscheidend ist nicht der Name der Sprache,
sondern ob sie zur Aufgabe passt und sicher genutzt wird.

Swift ist in der CISA-Unterlage im Appendix als MSL aufgeführt. Für
Ausbildungsprojekte ist das besonders wichtig, wenn Apps oder Tools für iOS,
watchOS oder macOS entwickelt werden. Swift reduziert viele Speicherfehler,
aber Swift-Code muss weiterhin sicher programmiert werden. Dazu gehören
validierte Eingaben, vorsichtiger Umgang mit Force-Unwraps, sichere Datei-URLs,
Keychain/CryptoKit, Netzwerk-I/O und gepflegte Abhängigkeiten.

## Personal, Schulung und Ressourcen

Eine MSL-Migration kostet Zeit. Teams müssen neue Spracheigenschaften,
Bibliotheken und Build-Prozesse lernen.

Eine realistische Roadmap plant deshalb:

- Schulung für Entwickler*innen.
- Zeit für Prototypen.
- technische Reviews.
- schrittweise Migration.
- klare Verantwortlichkeiten.
- Unterstützung durch Architektur und Security.

Für Ausbildungsprojekte bedeutet das: Lernziele, Code-Reviews und
Dokumentation gehören direkt zur Umsetzung.

## Typische Herausforderungen

### Security Shift Left

Sicherheit soll früh im Prozess berücksichtigt werden. Das heißt: Schon in
Spezifikation, Planung und Design wird geprüft, ob eine sichere Sprache oder
sichere Architektur nötig ist.

### Performance

Manche Teams befürchten Performance-Verluste. Das kann im Einzelfall relevant
sein. Es muss aber gemessen werden. Moderne MSL können für viele Aufgaben sehr
leistungsfähig sein.

### Bestehende unsichere Bibliotheken

Auch ein MSL-Projekt kann unsichere native Bibliotheken nutzen. Dann bleibt ein
Teil des Risikos bestehen. Solche Grenzen müssen dokumentiert werden.

### `unsafe`-Ausnahmen

Einige MSL erlauben bewusst unsichere Bereiche, zum Beispiel `unsafe` in Rust.
Solche Ausnahmen brauchen klare Begründung, kleine Scope-Grenzen und Review.

### Ausbildung und Informatikgrundlagen

Lehre und Ausbildung müssen moderne sichere Sprachen ernst nehmen. C und C++
sind fachlich wichtig, aber sichere Entwicklungsstandards dürfen nicht bei
historischen Standards stehen bleiben.

### OT, IoT und eingebettete Systeme

Operational Technology, IoT und kleine Geräte haben oft besondere Grenzen:
wenig Speicher, spezielle Hardware, lange Lebensdauer und harte Echtzeit. Auch
dort soll MSL geprüft werden. Wenn Migration nicht sofort möglich ist, müssen
andere Schutzmaßnahmen dokumentiert werden.

## Was eine Memory-Safe Roadmap enthalten sollte

Eine Roadmap sollte öffentlich oder intern klar zeigen, wie ein Hersteller oder
Projekt Speicherfehler reduzieren will.

Sie sollte enthalten:

- Bestandsaufnahme der betroffenen Codebasen.
- Zielsprachen und Auswahlbegründung.
- Priorisierung nach Risiko.
- Zeitplan und Meilensteine.
- Verantwortliche Rollen.
- Umgang mit Altcode.
- Umgang mit unsicheren Bibliotheken.
- Regeln für neue Entwicklung.
- Messgrößen für Fortschritt.
- Kommunikation an Kund*innen oder Nutzende.

Für Level-2-Projekte reicht oft eine kleinere Variante. Wichtig ist, dass die
Entscheidung nachvollziehbar ist.

## Bedeutung für die sechs Spec-Kit-Presets

### `security-governance`

Dieses Preset trägt den wichtigsten Bezug zu MSL. Es prüft Sprachwahl,
Secure-Coding-Regeln, Dependency-Risiken, SBOM, VEX und regulatorische
Anwendbarkeit.

### `architecture-governance`

Dieses Preset betrachtet MSL als Architekturentscheidung. Trust Boundaries,
Defense in Depth und sichere Konfiguration bleiben auch bei MSL wichtig.

### `isaqb-architecture-governance`

Dieses Preset hilft, Architekturziele, Sichten, Risiken und technische Schulden
zu dokumentieren. Eine MSL-Migration kann eine wichtige Architekturentscheidung
sein.

### `a11y-governance`

Dieses Preset sorgt dafür, dass Lern- und Review-Dokumente verständlich,
zweisprachig und barrierearm bleiben. Diese Lernfassung folgt diesem Ziel.

### `agent-parity-governance`

Wenn Agenten-Dateien Regeln zu MSL oder Secure Development enthalten, müssen sie
gemeinsam aktualisiert werden.

### `cross-platform-governance`

Skripte und Tooling für Migration, Tests und Nachweise müssen auf den
unterstützten Plattformen nachvollziehbar funktionieren.

## Bedeutung für Fachinformatiker*innen

Auszubildende sollen aus dem Dokument vier Kernpunkte mitnehmen:

1. Speicherfehler sind häufig und gefährlich.
2. MSL reduzieren eine wichtige Fehlerklasse.
3. MSL ersetzen keine vollständige sichere Entwicklung.
4. Gute Sicherheitsarbeit braucht Roadmaps, Reviews, Tests und Nachweise.

In einem Spec-Kit-Lauf sollte deshalb immer geprüft werden:

- Welche Sprache nutzt das Projekt?
- Ist diese Sprache eine MSL?
- Wenn Swift genutzt wird: Sind Force-Unwraps, Datei-URLs, Keychain/CryptoKit,
  Netzwerk-I/O und Dependencies geprüft?
- Wenn nein: Gibt es eine dokumentierte Begründung?
- Gibt es native oder unsichere Bibliotheken?
- Werden Eingaben, Ausgaben und Fehler sicher behandelt?
- Gibt es Tests und Review-Nachweise?
- Sind offene Risiken als `Open` dokumentiert?
- Sind nicht anwendbare Punkte als `N/A` begründet?

## Praktische Checkliste für Ausbildungsprojekte

| Prüffrage | Erwartung |
|---|---|
| Ist die Primärsprache eine MSL? | Ja, oder begründetes `N/A` beziehungsweise Risiko |
| Gibt es unsichere native Teile? | Dokumentieren und begrenzen |
| Werden neue Komponenten bevorzugt in MSL geschrieben? | Ja, wenn die Plattform es erlaubt |
| Gibt es eine Migrationsidee für Altcode? | Roadmap oder begründete Nichtanwendbarkeit |
| Sind Reviews und Tests vorhanden? | Evidenzpfad nennen |
| Sind offene Risiken sichtbar? | Als `Open` mit Folgeaufgabe dokumentieren |

## Fazit

Memory-Safe Roadmaps sind ein praktischer Weg, um eine sehr häufige Klasse von
Sicherheitslücken zu reduzieren. Sie helfen, technische Arbeit, Management und
Kundentransparenz zu verbinden.

Für Ausbildungsprojekte ist der wichtigste Lernpunkt: Sichere Entwicklung
beginnt nicht erst beim Test. Sie beginnt bei Sprache, Architektur, Planung,
Reviews und nachvollziehbarer Dokumentation.
