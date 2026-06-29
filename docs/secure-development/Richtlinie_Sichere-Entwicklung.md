<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Richtlinie ist eine generische Grundlage fuer sichere Entwicklung. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This guideline is a generic baseline for secure development. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

# Richtlinie Sichere Entwicklung

## Dokumenten-Metadaten

| Feld | Wert |
|---|---|
| Titel | Richtlinie zur sicheren Softwareentwicklung |
| Versionsnummer | 1.0.0 |
| Freigabedatum | 17.06.2026 |
| Inkrafttreten | 17.06.2026 |
| Dokumentenklassifikation | öffentlich nutzbare Ausbildungs- und Prüfgrundlage |
| Verantwortliche Stelle | Projekt- oder Ausbildungsverantwortliche mit Security-Review |
| Freigegeben durch | Projekt- oder Ausbildungsverantwortliche nach Sicherheitsreview |
| Nächster Review-Termin | 07.05.2027 (jährlich, ungeplant bei wesentlichen Änderungen) |
| Dokumentennummer / Aktenzeichen | RL-SE-001 |
| Nutzungskontext | Generischer Leitfaden fuer sichere Entwicklung und Ausbildungsprojekte |

Die Lenkung dieses Dokuments erfolgt gemäß ISO/IEC 27001:2022 Klausel 7.5 („Dokumentierte Information"). Änderungen werden in der Versionshistorie am Ende des Dokuments festgehalten. Die jeweils gueltige Fassung wird im jeweiligen Repository versioniert. Lokale Kopien gelten als Arbeitskopien.

## Mitgeltende Dokumente

- [Checkliste Secure Development Life Cycle](mitgeltende-dokumente/Checkliste_Secure-Development-Life-Cycle.md)
- [Gebrauch kryptografischer Maßnahmen](mitgeltende-dokumente/Gebrauch_kryptografischer_Massnahmen.md)
- [Kompetenzprofile und Schulungsplan für sichere Entwicklung](mitgeltende-dokumente/Kompetenzprofile_und_Schulungsplan_Sichere-Entwicklung.md)
- [Leitlinie für sichere Programmierung](mitgeltende-dokumente/Leitlinie_Sichere-Programmierung.md)
- [Richtlinie Secure Development Life Cycle](mitgeltende-dokumente/Richtlinie_Secure-Development-Life-Cycle.md)
- [Richtlinie Changemanagement](mitgeltende-dokumente/Richtlinie_Changemanagement.md)
- [Richtlinie Dienstleister- und Lieferantenbeziehungen](mitgeltende-dokumente/Richtlinie_Dienstleister-und-Lieferantenbeziehungen.md)
- [Richtlinie Testmanagement](mitgeltende-dokumente/Richtlinie_Testmanagement.md)
- [Richtlinie Zugangssteuerung](mitgeltende-dokumente/Richtlinie_Zugangssteuerung.md)
- [Datenschutzleitlinie](mitgeltende-dokumente/Datenschutzleitlinie.md)
- [Leitlinie für sicheres Softwaredesign](mitgeltende-dokumente/Leitlinie_Sicheres-Softwaredesign.md)
- [BCM-/Notfallhandbuch](mitgeltende-dokumente/BCM-Notfallhandbuch.md)
- [Verzahnung Richtlinie, Checklisten und Spec-Kit-Presets](mitgeltende-dokumente/Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md)
- [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR](mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.pdf)
- [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR EN-Markdown](mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.EN.md)
- [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR DE-Lernfassung](mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.DE.md)

Die zentrale Verzahnungsdatei ist die verbindliche Leseführung für spätere Spec-Kit-Läufe und Reviews. Sie ordnet mitgeltende Dokumente den Richtlinienabschnitten, Checklisten, Governance-Presets und typischen Evidenzpfaden zu. Dadurch können auch Auszubildende ab dem ersten Lehrjahr und Entwickler*innen ohne Sicherheits-Spezialwissen erkennen, warum ein Prüfpunkt gilt und welcher Nachweis erwartet wird.

## Mitgeltende Checklisten

Die Detail- und Prüfanforderungen zu dieser Richtlinie sind im mitgeltenden Sammelband `Checklistensammelband_Sichere-Entwicklung.md` geregelt. Der Sammelband enthält die zwölf eigenständig auditierbaren Checklisten als Kapitel 01 bis 12 und ersetzt die frühere Einzelablage. Die CL-Kurzbezeichnungen bleiben als stabile Referenzen erhalten.

- Kapitel 01 / CL_Standards-Anwendbarkeit: Auswahl der anwendbaren Sicherheitsstandards und regulatorische Anwendbarkeit je Projekt.
- Kapitel 02 / CL_Sichere-Softwarearchitektur: acht Architekturprinzipien, S-ADR-Pflicht, arc42 Abschnitt 8, Cloud-Autonomie und Cloud-Compliance-Assurance.
- Kapitel 03 / CL_Krypto-Mindestvorgaben: Mindestalgorithmen, Schlüssellängen, TLS, Schlüsselverwaltung.
- Kapitel 04 / CL_Bedrohungsmodellierung: STRIDE-Schritte, CIA-Bewertung, CAPEC-Bezug.
- Kapitel 05 / CL_Lieferkette-Build-Integritaet: SBOM, VEX, SLSA, Scorecard, Abhängigkeits-Pflege.
- Kapitel 06 / CL_Schwachstellenoffenlegung: CVD-Prozess, security.txt, Triage, CRA-24-Stunden-Meldung.
- Kapitel 07 / CL_CRA-Anwendbarkeit: Produktklassifizierung, Annex III/IV, Pflichten und Zeitachse.
- Kapitel 08 / CL_Code-Review-Sicherheit: sprachübergreifende und sprachspezifische Review-Punkte.
- Kapitel 09 / CL_KI-Codeerzeugung: sicherer Einsatz von KI-Assistenten beim Schreiben von Code.
- Kapitel 10 / CL_Sichere-Entwicklungsumgebung: IDE, Repos, Pipelines, Geheimnisse, Audit-Logs, Cross-Platform-Skriptparitaet.
- Kapitel 11 / CL_Datenschutz-Folgenabschaetzung: DSGVO Art. 35 (DPIA), Schwellwertanalyse, Risikobewertung, Konsultation.
- Kapitel 12 / CL_Agentische-KI-Sandbox: agentische KI mit OpenCode oder Codex in isolierten Sandbox-Umgebungen.

## Ziele

- Vertraulichkeit, Integrität und Verfügbarkeit von Daten und Diensten sicherstellen.
- Kundenzufriedenheit erhöhen.
- Position der Organisation als verlässliche Kooperationspartnerin und IT-Dienstleisterin stärken.
- Widerstandskraft der IT-Infrastruktur erhöhen.

## Geltungsbereich

Die Leitlinie ist generisch und gilt unabhaengig von konkreten Organisationsstrukturen, Teams oder Auftraggebenden.

Diese Richtlinie gilt fuer alle Personen, die Software entwickeln, pruefen, warten oder mit KI-Agenten erzeugen. Sie gilt fuer Neuentwicklung, Weiterentwicklung und Anpassung von Standardsoftware.

## Einordnung als generischer Sicherheitsleitfaden

Diese Richtlinie ist eine bereichsspezifische Sicherheitsrichtlinie im Sinne von ISO/IEC 27001:2022 Klausel 5.2 und Anhang A Control A.5.1. Sie ergänzt die übergeordnete Informationssicherheitsleitlinie der Organisation und konkretisiert die Anforderungen an einen sicheren Softwareentwicklungs-Lebenszyklus (Secure Development Life Cycle, SDLC) gemäß ISO/IEC 27002:2022 Control A.8.25.

**Bezug zu den ISO/IEC 27001:2022-Klauseln:**

- **Klausel 6.1 (Maßnahmen zum Umgang mit Risiken und Chancen):** Die Risikobewertung für Entwicklungsvorhaben erfolgt im Rahmen des projektbezogenen Risikomanagementprozesses der Organisation. Sie orientiert sich an ISO/IEC 27005:2022 und den BSI-IT-Grundschutz-Methoden. Projekttypische Risiken werden im jeweiligen Bedrohungsmodell des Projekts vertieft (siehe Abschnitt „Bedrohungsmodellierung"). Restrisiken werden im zentralen Risikoregister der Organisation geführt und mit den entsprechenden Risikoeignerinnen und Risikoeignern abgestimmt.
- **Klausel 7.5 (Dokumentierte Information):** Diese Richtlinie unterliegt der formalen Dokumentenlenkung der Organisation. Versionierung, Freigabe, Verteilung und geordnete Archivierung erfolgen in der festgelegten Dokumentationsablage. Änderungen werden in der Versionshistorie am Ende dieses Dokuments festgehalten.
- **Klausel 8 (Betrieb):** Die Richtlinie steuert den operativen Sicherheitsteil des Softwareentwicklungs-Prozesses. Die zugehörigen Checklisten dienen als prüfbare Arbeitsmittel und liefern Belege für interne und externe Audits.
- **Klausel 9.2 (Internes Audit):** Die Einhaltung dieser Richtlinie ist Gegenstand des jährlichen internen Sicherheitsreviews. Der Sammelband `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 01 bis 12, ist das primäre Auditmittel je Projekt. Stichproben werden nach Risiko, Kritikalitaet und Aenderungsumfang gezogen.
- **Klausel 9.3 (Managementbewertung):** Die Wirksamkeit dieser Richtlinie wird mindestens einmal jährlich im Rahmen eines Sicherheitsreviews auf Basis von KPI (siehe Abschnitt „KPI für Sicherheit und Testabdeckung"), Auditfeststellungen, Vorfällen und Schwachstellenstatistiken geprüft.
- **Klausel 10 (Verbesserung):** Befunde aus Audits, Vorfällen und Schwachstellen-Triagen werden über den kontinuierlichen Verbesserungsprozess in diese Richtlinie und ihre Checklisten zurückgespielt.

**Verantwortlichkeiten:**

- Die fachliche Pflege der Richtlinie liegt bei der zuständigen Security-Verantwortung oder Projektverantwortung.
- Die verantwortliche Leitung gibt die Richtlinie frei und stellt die nötigen Ressourcen bereit (ISO/IEC 27001:2022 Klausel 5.1).
- Projektverantwortliche und Tech Leads verantworten die Umsetzung in ihren Projekten.
- Die Datenschutzrolle ist bei allen Vorhaben zu beteiligen, die personenbezogene Daten verarbeiten (siehe Abschnitt „Datenschutz-Folgenabschätzung").

**Schnittstelle zur Anwendbarkeitserklärung (Statement of Applicability, SoA):**

Diese Richtlinie und der Sammelband `Checklistensammelband_Sichere-Entwicklung.md` liefern bereichsspezifische Nachweise fuer eine projektbezogene Anwendbarkeitsnotiz oder eine optionale ISO-27001-Nachweisfuehrung. Eine vollstaendige ISO/IEC-27001:2022-Annex-A-Matrix kann in einem projektspezifischen Nachweisprozess gefuehrt werden und wird hier nicht dupliziert. Für Entwicklungsvorhaben liefern Richtlinie und Sammelband je Projekt mindestens folgende SoA-Eingaben:

- ISO/IEC-27001:2022-Annex-A-Control-ID oder einschlägiger externer Standard.
- Anwendbarkeit: `anwendbar`, `nicht anwendbar` oder `projektabhängig`.
- Implementierungsstatus: `umgesetzt`, `teilweise umgesetzt`, `offen` oder `nicht anwendbar`.
- Begründung für Auswahl, Nichtanwendbarkeit oder Abweichung.
- Evidenzpfad, zum Beispiel S-ADR, Bedrohungsmodell, ASVS-Nachweis, SBOM, VEX, DPIA, Auditbericht, Ticket oder Betriebsdokumentation.
- Risikoregister- oder Maßnahmen-ID, verantwortliche Person und nächster Review-Termin.

Wenn ein Projekt neue Risiken, neue Kontrollen oder begründete Nichtanwendbarkeit für Annex-A-Controls sichtbar macht, wird die projektbezogene Anwendbarkeitsnotiz im Nachweisprozess durch Security-Verantwortliche*r oder die zuständige Sicherheitsrolle aktualisiert. Der Sammelband dient dabei als operative Evidenzquelle; die SoA bleibt das führende Steuerungsdokument für Annex-A-Abdeckung, Implementierungsstatus und Ausschlussbegründungen.

## Evaluation

Die Richtlinie wird einmal pro Jahr auf Aktualität und Gültigkeit überprüft.

## Sprache und Barrierefreiheit

Diese Datei enthält die deutsche und englische Fassung der Richtlinie. Fachliche Änderungen werden in beiden Sprachfassungen synchron nachgezogen. Das Zielniveau ist CEFR B2: kurze Sätze, klare Begriffe und erklärte Abkürzungen. Fachbegriffe werden beim ersten Auftreten ausgeschrieben.

Dokumente, Checklisten, Meldungen und Vorlagen werden barrierearm erstellt. Markdown-Dateien verwenden eine klare Überschriftenhierarchie, beschreibende Linktexte und Listen statt Layout-Tabellen. Informationen dürfen nicht nur über Farbe, Sonderzeichen oder Bilder vermittelt werden. Screenshots und Diagramme brauchen eine kurze Textbeschreibung.

Für Web-Inhalte und veröffentlichte Sicherheitsinformationen gilt WCAG 2.2 Level AA, soweit die Kriterien auf das jeweilige Artefakt anwendbar sind. Personenbezeichnungen werden barrierearm formuliert, zum Beispiel als neutrale Begriffe oder ausgeschriebene Paarformen.

## Grundsätze

Die Organisation nutzt aktuelle Best Practices und Entwurfsmuster. Dazu gehören insbesondere das Wasserfallmodell, die objektorientierte Programmierung (OOP), Model-View-Controller (MVC), Model-View-View-Model (MVVM) und Microservice-Architekturen. Diese Muster erfüllen die Anforderungen interner und externer Kundschaft sowie liefernder Organisationen. Sie gelten für Neuentwicklungen und für die Weiterentwicklung von Quellcode.

Der Quellcode entsteht in Übereinstimmung mit der „Leitlinie für sichere Programmierung". Die dort empfohlenen speichersicheren Programmiersprachen werden bevorzugt. Die Organisation hat einen hohen Qualitätsanspruch in der Softwareentwicklung. In jedem Projekt achten wir auf Informationssicherheit und Datenschutz.

Während der Spezifikation und des Entwurfs werden gängige Sicherheitstechniken und -standards beachtet. Die Softwarearchitektinnen und Softwarearchitekten folgen dem mitgeltenden Dokument „Leitlinie für sicheres Softwaredesign".

Die eingesetzten Programmiersprachen orientieren sich an dem mitgeltenden Dokument „THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR". Der Anhang dieses Dokuments führt empfohlene speichersichere Sprachen (Memory Safe Languages, MSL) auf. Swift ist dort als MSL berücksichtigt und gilt in dieser Richtlinie als speichersichere Sprache, insbesondere für Apple-Plattformen. Bei Ausbildungs- und Level-2-Projekten werden vor allem Java, C#, Python, Go, Rust und bei passender Zielplattform auch Swift als MSL betrachtet. Eine MSL ersetzt keine sprachspezifische Secure-Coding-Prüfung.

Die Entwicklerinnen und Entwickler schreiben ihren Quellcode gemäß den Empfehlungen der „Leitlinie für sichere Programmierung".

Bei der Organisation werden Entwicklungsumgebungen (Integrated Development Environments, IDEs) eingesetzt, die in der „Richtlinie zur Installation von Software" beschrieben sind und in der Liste der genehmigten Software für die Nutzung freigegeben wurden. Die IDEs laufen möglichst auf den drei Hauptplattformen Windows, macOS und Linux. Die Hersteller aktualisieren die IDEs regelmäßig mit neuen Funktionen und Sicherheitsupdates.

Der entwickelte Quellcode wird in den von der Organisation selbst betriebenen Versionskontrollsystemen (Git) verwaltet. Zugriff und Transport sind abgesichert. Der Zugriff erfolgt mit Single Sign-On (SSO) für das Web-Interface und mit Security-Token oder SSH-Login für die IDEs. Das Webinterface ist zusätzlich durch ein Server-Zertifikat aus der geeignete öffentliche oder interne PKI geschützt. Das Server-Zertifikat wird vollautomatisch durch eine Bot-Software über das ACME-Protokoll verwaltet und 30 Tage vor Ablauf erneuert.

Bei der Einstellung neuer Entwicklerinnen und Entwickler achtet die Organisation auf die benötigten Fachkenntnisse und auf vorhandene Expertise. Das Entwicklungsteam wird gemäß dem Prozess „10 - Qualifizierung und Schulung der Mitarbeiter" regelmäßig geschult. Es kann externe Schulungen oder externe oder interne Lernangebote nutzen.

Die Regelungen aus der „Richtlinie zur Zugangssteuerung" werden angewendet. Ergänzend gilt die „Richtlinie zum Testmanagement" für ein geregeltes Testmanagement.

## Anzuwendende Sicherheitsstandards

Die anwendbaren Sicherheits- und Architekturstandards werden für jedes Projekt verbindlich nach der folgenden Anwendbarkeitsmatrix ausgewählt. Im Spezifikations-, Architektur- oder Sicherheitsdokument wird je Standard festgehalten, ob er anwendbar ist oder nicht (mit „N/A" und kurzer Begründung). Stille Auslassung ist nicht zulässig.

| Standard / Leitfaden | Priorität | Anwendbarkeit | Mindesterwartung |
|---|---|---|---|
| NIST SSDF (SP 800-218) | MUSS | alle Entwicklungsprojekte | sicherer Lebenszyklus mit Vorbereitung, Quell- und Build-Schutz, sicherer Erstellung und Schwachstellenreaktion |
| CWE Top 25 | MUSS | alle Entwicklungsprojekte | relevante Schwächen werden in Entwurf, Implementierung, Review und Behebung berücksichtigt |
| OWASP ASVS 5.0.0 | MUSS | Web-, API-, HTTP- oder authentifizierte Dienste | ASVS-Level (1, 2 oder 3), Verifikationsumfang und versionierte ASVS-IDs werden gewählt und dokumentiert |
| SBOM | MUSS | release- oder verteilbare Artefakte | maschinenlesbares Komponenteninventar je Release |
| VEX | MUSS | bekannte Schwachstellen in ausgelieferten oder geprüften Komponenten | klare Aussage: betroffen, nicht betroffen, mitigiert oder in Untersuchung |
| SLSA v1.2 | SOLL | CI/CD-erstellte oder veröffentlichte Artefakte | Build-Provenienz und Build-Integrität, mindestens Build L1, wo praktikabel |
| OWASP SAMM | SOLL | langlebige Projekte und Workspaces | regelmäßige Selbstbewertung mit Verbesserungsplan |
| CAPEC | SOLL | Bedrohungsmodellierung relevanter Angriffspfade | Verweis auf einschlägige Angriffsmuster für risikoreiche Flows |
| NIST Zero Trust (SP 800-207) | projekttypabhängig | verteilte, servicebasierte, Cloud-, remote-verwaltete oder identitätsföderierte Systeme | Anwendbarkeit explizit entscheiden, Kontrollen oder begründetes „N/A" festhalten |
| OWASP Cheat Sheet Series und Proactive Controls | SOLL | alle entwicklerseitigen Projekte | Nutzung als alltägliche Implementierungsleitlinie unterhalb dieser Richtlinie |
| OpenSSF Scorecard | projekttypabhängig | öffentliche OSS-Repositorien oder kritische externe Abhängigkeiten | Sicherheitsposition vor Adoption oder Release prüfen |

**Wahl des ASVS-Levels:**

- Level 1 für einfache oder interne Webanwendungen mit begrenztem Risiko.
- Level 2 für authentifizierte, mehrbenutzerfähige, privilegierte oder im Internet erreichbare datenführende Dienste.
- Level 3 nur bei expliziten Hochsicherheits-, Hochwirkungs- oder regulatorischen Vorgaben.

NIST SSDF und CWE Top 25 gelten immer. OWASP Cheat Sheets und Proactive Controls dienen als alltägliche Hilfe, sofern Sprache und Framework keine strengeren Regeln vorgeben. Die anwendbaren Standards werden in Anforderungs-, Plan- und Aufgabenartefakten benannt. ASVS-Anforderungen werden mit Versionspräfix dokumentiert, zum Beispiel `v5.0.0-1.2.5`. Belege wie ASVS-Verifikation, SBOM, VEX oder SAMM-Bewertung werden im Sicherheitsdokumentenbestand des Projekts geführt.

Mitgeltende Checkliste: CL_Standards-Anwendbarkeit (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 01).

## Sichere Softwarearchitektur

Die Softwarearchitektur folgt etablierten sicheren Architekturprinzipien gemäß ISO/IEC 27002:2022 Control A.8.27 und der iSAQB-CPSA-Foundation-Sicht auf Sicherheit als verbindliches Qualitätsmerkmal. Sicherer Code allein genügt nicht. Architektur und Implementierung müssen zusammenwirken, damit Systeme widerstandsfähig sind.

**Verbindliche Architekturprinzipien:**

- Vertrauensgrenzen (Trust Boundaries): Jedes System hat klar definierte Vertrauensgrenzen. Eingaben über solche Grenzen (Benutzereingaben, externe API-Antworten, Dateiinhalte, nicht vertrauenswürdige Umgebungsvariablen) werden vor der Verarbeitung geprüft und bereinigt.
- Verteidigung in der Tiefe (Defense in Depth): Sicherheit hängt nicht von einer einzelnen Kontrolle ab. Mindestens zwei unabhängige Schichten schützen kritische Werte (zum Beispiel Eingabevalidierung am API-Gateway UND parametrisierte Abfragen im Datenzugriff).
- Geringste Rechte (Least Privilege): Komponenten, Dienste und Prozesse arbeiten mit den minimal nötigen Rechten. Datenbanken nutzen rollenspezifische Konten mit eingegrenzten Rechten. Dateisystem-Zugriffe sind auf benötigte Verzeichnisse begrenzt.
- Sichere Voreinstellungen (Fail-Safe Defaults): Zugriff ist standardmäßig verboten und nur auf ausdrückliche Erlaubnis möglich. Fehlerpfade führen in einen sicheren Zustand (Zugriff verweigern, Verbindung schließen, allgemeinen Fehler liefern).
- Reduzierung der Angriffsfläche (Attack Surface Reduction): Nicht genutzte Endpunkte, Dienste, Ports und Funktionen werden deaktiviert oder entfernt. Öffentliche APIs zeigen nur die nötige Schnittstelle. Debug-Endpunkte und ausführliche Fehlerausgaben sind in Produktion nicht erreichbar.
- Trennung der Belange (Separation of Concerns): Authentifizierung, Autorisierung, Logging und Eingabevalidierung sind als Querschnittsanliegen umgesetzt (zum Beispiel als Middleware, Filter, Interceptors, Decorators) und nicht in der Geschäftslogik verstreut. Sicherheitsrelevante Entscheidungen werden zentral getroffen, nicht doppelt gepflegt.
- Sichere Konfiguration (Secure Configuration): Geheimnisse (Verbindungszeichenfolgen, API-Keys, Token) liegen in geeigneten Secret-Stores (zum Beispiel HashiCorp Vault, Cloud-Key-Vault, OS-Keychain, CI/CD-Secret-Injection). Sie liegen nie im Quellcode, nicht in Git-versionierten Konfigurationsdateien und nicht als hartcodierte Konstanten.
- Lieferkettensicherheit (Supply-Chain Security): Drittabhängigkeiten kommen aus geprüften Paketregistern. Lock-Dateien werden eingecheckt. Bekannte verwundbare Abhängigkeiten werden vor Release ersetzt oder aktualisiert.

**Sprachspezifische Architekturhinweise (nicht abschließend):**

- Java: zentrale Servlet-Filter beziehungsweise Spring-Security-Konfiguration für Authentifizierung, Autorisierung und CSRF-Schutz; Bean-Validation für Eingaben; keine Java-Serialisierung nicht vertrauenswürdiger Daten.
- C# / .NET: ASP.NET-Core-Middleware-Pipeline für Authentifizierung, Autorisierung, CORS und Anti-Forgery; Dependency Injection für sicherheitsrelevante Dienste; IDataProtectionProvider für Verschlüsselung at-rest; UseHttpsRedirection für Transport-Härtung.
- Python: WSGI- oder ASGI-Middleware-Schichten; Sicherheits-Defaults der Frameworks aktiv halten (Django mit CSRF/XSS-Schutz, FastAPI mit Pydantic-Validierung).
- Go: `context` konsequent durch Netzwerk-, Datenbank- und Hintergrundoperationen führen; HTTP-Timeouts setzen; `crypto/rand` für Geheimnisse nutzen; Goroutine- und Channel-Lebenszyklen begrenzen; Datenbankzugriffe parametrisieren.
- Rust: `unsafe` isolieren und begründen; Ownership- und Lifetime-Grenzen als Sicherheitsgrenze nutzen; `serde`-Deserialisierung mit Domain-Validierung kombinieren; `rustls` oder geprüfte Crypto-Crates verwenden.
- Swift / Apple-Plattformen: Keychain für Geheimnisse, CryptoKit für Kryptografie, App Sandbox und Entitlements prüfen; URL- und File-Scope-Zugriffe begrenzen; Swift Concurrency zur Vermeidung unkontrolliert geteilter veränderbarer Zustände nutzen.

Sicherheitsrelevante Architekturentscheidungen werden als Security Architecture Decision Records (S-ADR) festgehalten. Sie enthalten Kontext, Entscheidung, Begründung, Alternativen, Konsequenzen und eine kurze Compliance-Tabelle. Die Sicherheits-Querschnittskonzepte werden gemäß arc42 Abschnitt 8 dokumentiert: Authentifizierung, Autorisierung, Verschlüsselung in-transit und at-rest, Eingabevalidierung, Fehlerbehandlung, Logging und Audit-Trail, Abhängigkeitsmanagement, Deployment-Sicherheit.

Mitgeltende Checkliste: CL_Sichere-Softwarearchitektur (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 02).

## Kryptografische Mindestvorgaben

Für jeden Einsatz von Kryptografie gelten einheitliche Mindestvorgaben für Algorithmen, Schlüssellängen und Verfahren. Sie orientieren sich an aktuellen Empfehlungen von BSI (TR-02102) und NIST (SP 800-131A, SP 800-175B) und werden im mitgeltenden Dokument „Gebrauch kryptografischer Maßnahmen" konkretisiert.

**Verbindliche Mindestvorgaben:**

- Symmetrische Verschlüsselung: AES-256 in einem authentisierten Modus, vorzugsweise AES-GCM oder ChaCha20-Poly1305.
- Asymmetrische Verschlüsselung und Signatur: RSA mit mindestens 3072 Bit, ECDSA mit P-256 oder höher, oder Ed25519. Schlüsseltausch über ECDH (P-256+) oder X25519.
- Hash-Funktionen: SHA-256 oder höher (SHA-384, SHA-512, SHA-3-Familie).
- Passwort-Hashing: Argon2id (bevorzugt), scrypt oder bcrypt mit hohem Cost-Faktor und einem Salz von mindestens 16 Byte.
- Message Authentication Code: HMAC mit SHA-256 oder höher; bei AEAD-Verfahren entfällt der separate MAC.
- Transport Layer Security: TLS 1.2 als Mindestmaß, TLS 1.3 wird bevorzugt. SSLv3, TLS 1.0 und TLS 1.1 sind deaktiviert.
- Zufallszahlen: ausschließlich aus kryptografisch sicheren Quellen (`/dev/urandom`, `getrandom()`, `CryptGenRandom`, Bibliotheks-CSPRNG). Vorhersagbare Quellen wie `Math.random` oder `rand()` werden nicht verwendet.

**Verbotene oder eingeschränkte Verfahren:**

- Verboten für sicherheitsrelevante Zwecke: MD5, SHA-1 für Signaturen, DES, 3DES, RC4, RSA-PKCS#1 v1.5 für neue Anwendungen, ECB-Modus für Verschlüsselung.
- Ausnahmen sind nur mit dokumentierter Risikoakzeptanz und Ablaufdatum zulässig.

**Schlüsselverwaltung:**

- Schlüssel werden in einem geeigneten Secret Store gehalten (Vault, Cloud-KMS, HSM, OS-Keychain). Sie liegen nie im Quellcode oder in Git-versionierten Dateien.
- Für jeden Schlüsseltyp gibt es eine Rotationsfrist; bei Verdacht auf Kompromittierung wird sofort rotiert.
- Eigene Krypto-Implementierungen sind nicht zulässig. Es werden bewährte Bibliotheken verwendet (zum Beispiel JCA/JCE, BouncyCastle, libsodium, .NET-`System.Security.Cryptography`, Python-`cryptography`).
- Wo möglich, werden Hardware-gestützte Schlüsselspeicher (HSM, TPM, Cloud-KMS, Smartcard) genutzt, vor allem für langlebige oder hochsensible Schlüssel.

**Krypto-Agilität und Ausblick:**

- Algorithmen, Schlüssellängen und Cipher-Suites sind in der Konfiguration änderbar, ohne Code anzupassen.
- Für langlebige Daten und Signaturen wird die Migration auf quantensichere Verfahren beobachtet (NIST PQC: ML-KEM, ML-DSA, SLH-DSA). Ein Übergangsplan wird je betroffenem Projekt geführt.
- Mindestens jährlich werden Algorithmen, Schlüssellängen, TLS-Konfigurationen und Bibliotheksversionen gegen aktuelle BSI- und NIST-Empfehlungen geprüft.

Mitgeltende Dokumente: [Gebrauch kryptografischer Maßnahmen](mitgeltende-dokumente/Gebrauch_kryptografischer_Massnahmen.md).
Mitgeltende Checkliste: CL_Krypto-Mindestvorgaben (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 03).

## Entwicklungsumgebung

Die Entwicklungsumgebungen sind in drei Bereiche unterteilt: Entwicklung, Test (manchmal „Stage" genannt) und Produktion. Diese Systeme sind nach dem Stand der Technik abgesichert (siehe Abschnitt „Trennung von Entwicklungs-, Test- und Betriebsumgebungen"). Wir nutzen unter anderem Firewalls, Antivirus-Lösungen, automatische Betriebssystem-Updates und verschlüsselte Festplatten.

Nicht mehr benötigte Test- und Entwicklungsumgebungen werden zügig nach dem Stand der Technik gelöscht (Beseitigung von Restinformationen). Testkonten werden nur bei Bedarf angelegt und nach Vorgabe gelöscht. Produktionsdaten werden nur dann verwendet, wenn es nicht anders möglich ist; im Standardfall werden sie nicht verwendet. Die Repositorien werden regelmäßig auf Schwachstellen geprüft. Entwickelte Software wird im Rahmen der Meilensteine einer Sicherheitsprüfung unterzogen (zum Beispiel Code-Review).

Es werden regelmäßig Backups erzeugt. Der Datenverkehr in die Umgebung und aus der Umgebung heraus ist streng geregelt.

Mitgeltende Checkliste: CL_Sichere-Entwicklungsumgebung (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 10).

### Geschäftsfortführung der Entwicklungsumgebung

Die Entwicklungsumgebung ist eine kritische Infrastruktur für die Lieferfähigkeit der Organisation. Sie wird in den Geschäftsfortführungs- und Notfallplan (Business Continuity Management, BCM, sowie Disaster Recovery, DR) der Organisation einbezogen. Die Anforderungen orientieren sich an ISO/IEC 27002:2022 Control A.5.30 („IKT-Bereitschaft für Geschäftsfortführung") und an ISO/IEC 27031.

**Verbindliche Mindestvorgaben:**

- **Wiederanlaufziele (Recovery Time Objective, RTO; Recovery Point Objective, RPO):** Für jede zentrale Komponente der Entwicklungsumgebung ist ein RTO und RPO festzulegen und in der jeweiligen Betriebsdokumentation zu führen. Empfohlene Mindestwerte: Git (Quellcode-Verwaltung) RTO 8 Stunden, RPO 1 Stunde; CI/CD-Runner und Build-System RTO 24 Stunden, RPO 24 Stunden; Artefakt-Registry und Build-Cache RTO 24 Stunden, RPO 24 Stunden; Secret-Stores (Vault, Cloud-KMS, ACME-Bot) RTO 4 Stunden, RPO 1 Stunde; Issue- und Audit-Log-Systeme RTO 24 Stunden, RPO 24 Stunden.
- **Backup und Wiederherstellung:** Backups der Git-Datenbank, der Repositorien, der Artefakte, der Pipeline-Konfigurationen und der Secret-Stores erfolgen mindestens täglich. Sie liegen geografisch oder zumindest infrastrukturell getrennt vom Produktivsystem.
- **Wiederherstellungstests:** Mindestens einmal jährlich wird ein praxisnaher Wiederherstellungstest für die Kernkomponenten der Entwicklungsumgebung durchgeführt und dokumentiert. Der Test deckt Datenwiederherstellung, Wiederanlauf von CI/CD-Pipelines und die Wiederverfügbarkeit von Build-Artefakten ab.
- **Ausfall-Szenarien:** Geplante Szenarien sind mindestens: Ausfall einer Virtualisierungsplattform, Verlust eines Storage-Backends, Kompromittierung eines Build-Runners, Ausfall des Secret-Stores. Für jedes Szenario werden Eskalationswege und Verantwortlichkeiten festgehalten.
- **Lieferanten und externe Abhängigkeiten:** Die Verfügbarkeit externer Paketregistries, externer Identitätsanbieter und externer Code-Signaturdienste wird in den BCM-Plan einbezogen. Mirror- oder Caching-Strategien (zum Beispiel interner Nexus, Artifactory) werden bevorzugt, um Lieferkettenausfälle abzufedern.
- **Dokumentation und Übung:** BCM-Pläne werden im Sicherheits- oder Betriebsdokumentenbestand des jeweiligen Systems geführt. Verantwortungs- und Eskalationspfade werden in regelmäßigen Übungen (mindestens Tabletop) getestet.

Mitgeltende Dokumente: [BCM-/Notfallhandbuch](mitgeltende-dokumente/BCM-Notfallhandbuch.md); mitgeltende Checkliste: CL_Sichere-Entwicklungsumgebung (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 10).

## Programmierung

Neuer Code wird in den bei der Organisation üblichen, speichersicheren Sprachen Java, C#, Python, Go, Rust und bei passender Zielplattform Swift geschrieben oder in der Sprache, die die Kundschaft verlangt. Bei der Weiterentwicklung wird die jeweils vorhandene Sprache der Anwendung genutzt. Der Code wird ausreichend kommentiert (gemäß „Leitlinie für sichere Programmierung") und regelmäßig auf Schwachstellen getestet. Die Programmierung folgt dem Stand der Technik. Ein Wechsel des Paradigmas (zum Beispiel der Programmiersprache) wird gesondert dokumentiert und begründet.

Eine ordentliche Dokumentation ist im Team selbstverständlich. Wir achten auf modulare Programmierung. Der Code ist gut strukturiert, oft in wiederverwendbaren Codeblöcken angeordnet.

Die Entwicklerinnen und Entwickler können Schwachstellen finden und in der Regel selbst beheben. Werkzeuge wie Issue-Tracker unterstützen das Team dabei.

Mitgeltende Checkliste: CL_Code-Review-Sicherheit (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 08).

### Schutz von Testdaten

Während der Entwicklung werden meist generische Testdaten verwendet. Diese sind strukturell wie die Produktionsdaten, aber kleiner im Volumen. Damit Tests reproduzierbar bleiben, werden die Testdaten als Datenbank-Dump oder Datei-Archiv in einem Code-Repository gepflegt. Tests können auf die Testdaten zugeschnitten sein, müssen es aber nicht.

Wenn Produktionsdaten für Tests nötig sind (zum Beispiel zur Reproduktion eines Fehlers), werden sie nur für diesen Zweck auf das Entwicklungs- oder Stage-System eingespielt. Nach Abschluss der Arbeiten werden sie sofort wieder gelöscht.

**Daten-Maskierung und Anonymisierung (ISO/IEC 27002:2022 A.8.11):** Personenbezogene Daten und schützenswerte Geschäftsdaten in Testdatenbeständen werden grundsätzlich anonymisiert, pseudonymisiert oder maskiert. Geeignete Verfahren sind:

- **Generierte synthetische Daten:** Werkzeuge wie Faker (Python, Java, .NET), Mockaroo oder eigene Datengeneratoren erzeugen strukturell passende, aber fiktive Datensätze. Bevorzugte Lösung für Standard-Tests.
- **Deterministische Maskierung:** Reversible oder irreversible Transformationen mit konsistenter Abbildung (zum Beispiel HMAC-basierte Pseudonymisierung mit projektspezifischem Schlüssel), damit referentielle Integrität in Tests erhalten bleibt.
- **Format-erhaltende Verschlüsselung (Format-Preserving Encryption, FPE):** Für Felder, deren Format strikt erhalten bleiben muss (zum Beispiel IBAN, Steuernummer).
- **k-Anonymität, l-Diversität:** Aggregations- oder Generalisierungsverfahren bei Auswertungs- und Analyse-Testbeständen, sodass einzelne Personen nicht reidentifizierbar sind.
- **Subset- und Sampling-Strategien:** Reduktion des Datenvolumens auf das fachlich Notwendige (Datenminimierung gemäß DSGVO Art. 5 Abs. 1 lit. c).

Maskierungs- und Anonymisierungsverfahren werden im Testdatenkonzept des Projekts dokumentiert. Werden ausnahmsweise Klardaten verwendet, gilt die Verpflichtung aus Abschnitt „Datenschutz-Folgenabschätzung" und es werden technische und organisatorische Maßnahmen gemäß DSGVO Art. 32 ergriffen (eingeschränkter Personenkreis, Protokollierung, Löschfristen).

### Versionierung und Änderungsmanagement

Änderungen am Quellcode werden mit dem von der Organisation selbst betriebenen Versionskontrollsystem (VCS) Git verwaltet. Zugriffe werden gemäß der „Richtlinie zur Zugangssteuerung" gewährt oder entzogen. Es gelten die Regelungen zum Changemanagement. Weitere Sicherheitsdetails zum VCS finden sich im IT-Betriebskonzept der jeweiligen Anwendung. Änderungen werden nachvollziehbar dokumentiert. Neue Funktionen oder Module werden im IT-Betriebskonzept nachgezogen.

Bei Änderungen am hostenden Betriebssystem oder am Software-Stack der Anwendung werden vorher Tests durchgeführt. So bleiben Stabilität und Sicherheit erhalten. Tiefgreifende Änderungen werden auf das Notwendige beschränkt. Sie erfolgen nur, wenn Sicherheit für Betriebssystem, Anwendung und Daten der Kundschaft gewährleistet ist. Tests werden in der Testdokumentation festgehalten (zum Beispiel Schwachstellentests).

Bei nötigen Änderungen, vor allem an Produktivsystemen, werden Requests for Change (RfC) eingereicht. Erst wenn alle Tests in der Entwicklungs- und Testumgebung erfolgreich waren, wird per RfC ein Termin festgelegt und die Änderung an dem Termin durchgeführt. Der genaue Ablauf steht in den Regelungen zum Changemanagement. Die zugehörigen IT-Betriebskonzepte werden regelmäßig aktualisiert.

Alle zwei Wochen findet ein „Team Anwendungsentwicklung"-Treffen statt. Dort werden Fortschritt, Sicherheits-Themen und Pflegemaßnahmen für Software und betriebsnotwendige Systeme besprochen.

## KI-gestützte Codeerzeugung

KI-Code-Assistenten und Large Language Models (LLM) werden bei der Organisation nur in geregelter Form eingesetzt. Sie können die Produktivität erhöhen, erzeugen aber nicht zuverlässig sicheren Code. Eine ausdrückliche Prüfung jeder KI-Ausgabe ist daher Pflicht.

Diese Richtlinie regelt die Nutzung von KI in der Anwendungsentwicklung der Organisation. Das Bereitstellen von KI-Diensten, Modellen und Modell-Hosting durch die Organisation (zum Beispiel genehmigter KI-Dienst, Hosting von Hugging-Face-Modellen) ist Gegenstand der dafür zuständigen Betriebs- und Dienstleistungsdokumente und fällt nicht in den Geltungsbereich dieser Richtlinie.

**Verbindliche Regelungen:**

- Genehmigte Werkzeuge: Es kommen nur Werkzeuge zum Einsatz, die in der Liste der genehmigten Software stehen. Cloud-basierte und lokale Werkzeuge werden getrennt freigegeben.
- KI-Lieferkettentransparenz: Für eingesetzte KI-Dienste und Modelle werden die vom Anbieter verfügbaren Transparenzangaben im KI-Werkzeug-Inventar erfasst oder verlinkt — Modell-Identität und -Version, Verweis auf Model Card oder AI-SBOM des Anbieters, Trainings- und Feinabstimmungsverfahren sowie Herkunft und Sensitivität der Trainingsdaten soweit veröffentlicht, und KI-spezifische Sicherheitseigenschaften. Für fremdbezogene Modelle erstellt die Organisation keine eigene AI-SBOM; sie fordert die Angaben als Lieferketten-Nachweis an. Bezugsrahmen ist die G7-Leitlinie „Software Bill of Materials for AI – Minimum Elements" (2026).
- Menschliche Überprüfung: Jede KI-Ausgabe wird vor dem Commit von einer Person inhaltlich geprüft. Ein Blind-Commit ist nicht zulässig.
- Vier-Augen-Prinzip bei kritischer Logik: Sicherheitsrelevante Logik (Authentifizierung, Autorisierung, Eingabevalidierung, Krypto, Sitzungsverwaltung, Zahlungspfade) wird zusätzlich von einer zweiten Person geprüft.
- Abhängigkeiten: Schlägt die KI eine neue Bibliothek vor, wird diese auf Existenz, Pflegezustand und bekannte CVEs geprüft, bevor sie aufgenommen wird. Vorgeschlagene Paketnamen werden gegen die offizielle Registry geprüft, um halluzinierte Pakete („Slopsquatting"-Risiko) auszuschließen.
- Keine eigene Krypto: KI-Werkzeuge dürfen keine eigenen Krypto-Implementierungen vorschlagen; es werden ausschließlich bewährte Bibliotheken verwendet (siehe Abschnitt „Kryptografische Mindestvorgaben").
- Datenschutz in Prompts: Personenbezogene Daten, Geheimnisse, Tokens, Kundenbezeichnungen oder vertrauliche Konfigurationen gehören nicht in Prompts an externe KI-Dienste. Pre-Commit-Schutz und Schulung sind aktiv.
- Lizenz-Klärung: KI-erzeugte Codeschnipsel können Lizenzfragen aufwerfen. Zweifelhafte Schnipsel werden umgeschrieben oder durch klar lizenzierten Code ersetzt.
- Tests: KI-erzeugter Code wird durch automatisierte Tests abgedeckt (positive und negative Fälle). Eine grüne Pipeline ist Voraussetzung für den Merge.
- Markierung im Pull Request: Wesentliche KI-Anteile werden im Beschreibungstext oder per Label gekennzeichnet, damit das Review-Volumen klar ist.
- Audit-Spur: Werkzeugnutzung ist nachvollziehbar (Werkzeugname, Zeitraum, beteiligte Personen). Zentrale Logs werden für Audits aufbewahrt, soweit möglich.
- Schulung: Entwicklerinnen und Entwickler erhalten regelmäßig eine Schulung zu Stärken, Schwächen und Risiken von KI-Code-Assistenten.
- Ausnahmen: Eine Abweichung von dieser Regelung wird mit Begründung, Unterschrift und Ablaufdatum im Sicherheits- oder Risikoregister festgehalten.

Mitgeltende Checkliste: CL_KI-Codeerzeugung (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 09).

## Agentische KI in Sandbox-Umgebungen

**Geltungsanlass:** Agentische KI-Werkzeuge wie OpenCode oder Codex können Dateien ändern, Befehle ausführen und Projektkontext lesen. Sie sind damit nicht mehr ein passiver Assistent, sondern ein aktiver Akteur im Entwicklungsprozess. Aus diesem Grund werden sie ausschließlich in isolierten und formell freigegebenen Sandbox-Umgebungen betrieben. Eine direkte Ausführung auf Entwickler-Laptops, gemeinsam genutzten Servern oder in produktionsnahen Umgebungen ist nicht zulässig.

Der Zugriff der Sandbox auf Quellcode erfolgt ausschließlich über ausdrücklich gemountete Host-Verzeichnisse. Der Agent darf nur Dateien ändern, die über diese Mounts für das jeweilige Projekt freigegeben sind.

**Zulässige Sandbox-Typen:**

- **Container-Sandbox:** OCI-konforme Containerlaufzeit (zum Beispiel Docker, Podman) mit nicht-privilegierter Ausführung, dedizierten Volumes für Werkzeugdaten und expliziten Bind-Mounts auf Projektverzeichnisse.
- **MicroVM-Sandbox:** leichtgewichtige Virtualisierungslösung (zum Beispiel Firecracker, Kata Containers) mit hardware-unterstützter Isolation.
- **Klassische VM-Sandbox:** vollwertige virtuelle Maschine auf einer freigegebenen Virtualisierungsplattform mit klar definiertem Speicherbild und Wiederherstellungspunkt.
- **Dedizierter Workstation-Host:** physisch oder logisch getrennter Arbeitsplatzrechner ausschließlich für agentische KI-Arbeit, ohne Produktivkonten und ohne Zugriff auf personenbezogene Produktivdaten.

Andere Isolationsmechanismen sind zulässig, wenn sie nachweislich ein vergleichbares Schutzniveau erreichen. Der Nachweis erfolgt im Sicherheits- oder Architekturdokument der jeweiligen Sandbox.

Konkrete Referenzprofile, Mindestnachweise und Ausbildungsanforderungen fuer sichere Entwicklungs-Sandboxen werden in der mitgeltenden Leitlinie [Leitlinie Sichere Entwicklungs-Sandbox](mitgeltende-dokumente/Leitlinie_Sichere-Entwicklungs-Sandbox.md) beschrieben. Sie ordnet insbesondere MSL-basierte Level-2-Projekte, KI-Agenten, Spec Kit und oeffentlichkeitsfaehige Sandbox-Referenzumgebungen ein.

**Verbindliche Regelungen:**

- Die Sandbox trennt Agentenlaufzeit, Werkzeugdaten, Cache, Sitzungsdaten und Zugangsdaten vom Host-System.
- Der Agent erhält keinen allgemeinen Zugriff auf Home-Verzeichnisse, Schlüsselbunde, SSH-Agents, GPG-Schlüssel, Browserprofile, Cloud-CLI-Konfigurationen oder lokale Token-Speicher.
- Netzwerkzugriffe der Sandbox sind auf die Mindestmenge der nötigen Ziele eingeschränkt (zum Beispiel genehmigte Modell-Endpunkte, genehmigte Paketregistries). Andere ausgehende Verbindungen sind blockiert oder dokumentiert und begründet.
- Geheimnisse werden nicht in Prompts, Projektdateien, Logs oder Screenshots übernommen. Lokale Secret-Dateien bleiben außerhalb der versionierten Projektartefakte und werden über einen Secret Store oder geschützte Umgebungsvariablen eingebunden.
- KI-Werkzeuge und ihre Konfigurationen sind reproduzierbar gepinnt (Versionen, Image-Digests, Modell-Identifikatoren). Selbst-aktualisierende Mechanismen der Werkzeuge sind deaktiviert.
- Genehmigte KI-Werkzeuge, Provider, Modelle und Konfigurationen werden im KI-Werkzeug-Inventar der Organisation geführt (siehe Abschnitt „KI-gestützte Codeerzeugung").
- Bei Feature-Implementierungen mit agentischer KI wird GitHub Spec Kit für Spec-Driven Development (SDD) verwendet. Der Ablauf erfolgt nacheinander über `/speckit.constitution`, `/speckit.specify`, `/speckit.clarify`, `/speckit.plan`, `/speckit.checklist`, `/speckit.tasks`, `/speckit.analyze` und `/speckit.implement`. Die sechs Governance-Presets `security-governance`, `architecture-governance`, `isaqb-architecture-governance`, `a11y-governance`, `cross-platform-governance` und `agent-parity-governance` werden installiert und nachweisbar dokumentiert, sofern kein begründeter Projektausnahmefall dokumentiert ist.
- Die Presets werden mindestens quartalsweise und anlassbezogen kontrolliert aktualisiert. Anlassbezogen ist eine Prüfung nötig, wenn sich Spec-Kit-Preset-Kataloge, Preset-Inhalte, Prioritäten oder projektlokale Overrides ändern. Die Prüfung umfasst Preset-Liste, Preset-Informationen, wirksame Template-Auflösung und die Zuordnung der Preset-Inhalte zu den anwendbaren Checklisten. Erforderliche Änderungen werden in dieser Richtlinie und in den Checklisten eingearbeitet oder als begründete Ausnahme dokumentiert.
- Die inhaltliche Preset-Abdeckung wird als eigene Auditfrage behandelt. Abgedeckt werden insbesondere regulatorisches Screening (NIS2, CRA, EU AI Act, DORA), BSI C3A, BSI C5, konkrete WCAG-2.2-AA-Prüfung, CLI-Barrierefreiheit, Cross-Platform-Skriptparität, sprachspezifische Secure-Coding-Profile und Agent-Guidance-Parität. Nicht anwendbare Preset-Prüfpunkte werden ausdrücklich mit Begründung dokumentiert.
- Die Verzahnungsdatei der mitgeltenden Dokumente wird als Standardbrücke zwischen Richtlinienabschnitten, Checklisten-Kapiteln, mitgeltenden Dokumenten, Governance-Presets und Evidenzpfaden genutzt. Ein Spec-Kit-Lauf darf auf diese Datei verweisen, statt die gesamte Zuordnung zu wiederholen, muss aber weiterhin die konkrete Projektentscheidung und Evidenz dokumentieren.
- Wenn ein Spec-Kit-Lauf mit aktuellen geprüften Presets die anwendbaren Prüfpunkte vollständig, nachvollziehbar und als Markdown-Artefakte dokumentiert, kann die separate manuelle Ausfüllung der Checklisten entfallen. Voraussetzung ist eine eindeutige Nachweis-Matrix oder ein eindeutiger Verweis im Spec-Kit-Artefakt, der die anwendbaren CL-Prüfpunkte auf `spec.md`, `plan.md`, `tasks.md`, Analyse- oder Checklistenergebnis, Review und Preset-Nachweise abbildet. Nicht abgedeckte, nicht anwendbare oder bewusst abweichende Prüfpunkte werden weiterhin ausdrücklich dokumentiert. Diese Erleichterung ersetzt keine Sandbox-Freigabe, kein menschliches Review, kein Vier-Augen-Prinzip, keine Ausnahmegenehmigung und keine Preset-Aktualitätsprüfung.
- Agentische Änderungen werden vor Commit und Merge durch Menschen geprüft. Sicherheitskritische Logik unterliegt dem Vier-Augen-Prinzip.
- Die Nutzung wird nachvollziehbar dokumentiert: Werkzeug, Sandbox-Typ, Sandbox-Identifikator (zum Beispiel Image-Digest oder VM-Snapshot-Hash), Projektpfad, Zeitraum, verantwortliche Person, Review-Ergebnis und relevante Spec-Kit-Artefakte.

**Freigabe und Lebenszyklus von Sandbox-Umgebungen:**

- **Initialfreigabe:** Jede produktiv genutzte Sandbox-Konfiguration wird vor der ersten Nutzung von der oder dem Security-Verantwortlichen (Security-Verantwortliche*r) oder von der oder dem KI-Verantwortlichen freigegeben. Die Freigabe nennt Sandbox-Typ, technischen Identifikator, genehmigte Werkzeuge und Modelle, genehmigte Mount-Liste, verantwortliche Person und Ablaufdatum.
- **Re-Validierung:** Eine Freigabe gilt höchstens zwölf Monate. Vor Ablauf wird die Sandbox-Konfiguration gegen die jeweils gültige Fassung dieser Richtlinie und gegen `CL_Agentische-KI-Sandbox` neu bewertet.
- **Außerplanmäßige Re-Validierung:** Eine erneute Freigabe ist auch dann nötig, wenn sich Werkzeug-Versionen, Modell-Liste, Mount-Liste, Provider-Auswahl oder Netzwerkrichtlinie wesentlich ändern.
- **Entzug:** Die oder der Security-Verantwortliche*r sowie die oder der KI-Verantwortliche können eine Freigabe jederzeit entziehen, insbesondere bei Sicherheitsvorfällen, bei Kompromittierung der Werkzeugkette oder bei wiederholter Verletzung der Vorgaben dieser Richtlinie.
- **Pilotbetrieb:** Sandbox-Konfigurationen in Feinabstimmung dürfen mit dokumentiertem Pilotcharakter, eingeschränktem Anwenderkreis und begrenzter Datenklassifikation betrieben werden. Sie zählen erst nach Übergang in die formelle Freigabe als regulär.

**Bezug zu Normen und externen Rahmenwerken:**

- ISO/IEC 27001:2022 Annex A: A.5.23 (Informationssicherheit beim Einsatz von Cloud-Diensten), A.8.25 (sicherer Entwicklungs-Lebenszyklus), A.8.28 (sichere Codierung), A.8.31 (Trennung von Entwicklungs-, Test- und Produktionsumgebungen).
- NIST AI Risk Management Framework (AI RMF 1.0), insbesondere die Funktionen GOVERN, MAP, MEASURE und MANAGE.
- OWASP Top 10 for Large Language Model Applications, insbesondere LLM02 (Insecure Output Handling), LLM05 (Improper Output Handling) und LLM09 (Overreliance).
- Verordnung (EU) 2024/1689 (EU AI Act), soweit die jeweilige Nutzung in ihren Anwendungsbereich fällt.
- Verordnung (EU) 2022/2554 (DORA) und Richtlinie (EU) 2022/2555 (NIS2), soweit Projekt, Kunde oder Lieferkette in deren Anwendungsbereich fallen.
- BSI C3A und BSI C5 als Bewertungsrahmen für Cloud-Autonomie, digitale Souveränität und Cloud-Compliance-Assurance, soweit Cloud-Dienste wesentlich genutzt oder bereitgestellt werden.
- G7 „Software Bill of Materials for AI – Minimum Elements" (2026) als Zielarchitektur für Transparenz in der KI-Lieferkette; nicht rechtsverbindlich, aber anschlussfähig an CRA, EU AI Act und NIS2.

Mitgeltende Dokumente: [Leitlinie Sichere Entwicklungs-Sandbox](mitgeltende-dokumente/Leitlinie_Sichere-Entwicklungs-Sandbox.md); mitgeltende Checkliste: CL_Agentische-KI-Sandbox (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 12).

## Bedrohungsmodellierung

Für sicherheitsrelevante Anwendungen wird ein Bedrohungsmodell erstellt. Es wird zusammen mit Architektur- und Spezifikationsdokument fortgeschrieben.

**Mindestinhalte des Bedrohungsmodells:**

- Werte-Inventar mit CIA-Matrix (Vertraulichkeit, Integrität, Verfügbarkeit; je „Hoch", „Mittel", „Niedrig" oder „Nicht zutreffend"). Die CIA-Bewertung steuert die Schutzanforderungen und priorisiert die STRIDE-Analyse. Werte mit „Hoch" in Vertraulichkeit oder Integrität werden mit mindestens einer Defense-in-Depth-Maßnahme abgesichert.
- STRIDE-Analyse (Spoofing, Tampering, Repudiation, Information Disclosure, Denial of Service, Elevation of Privilege) als verbindliche Basismethode. Vertrauensgrenzen, Datenflüsse und externe Schnittstellen werden je STRIDE-Kategorie betrachtet.
- Verweise auf einschlägige CAPEC-Angriffsmuster für die risikoreichsten Vertrauensgrenzen, Missbrauchsfälle und extern erreichbaren Flows. Ziel ist die explizite Auseinandersetzung mit realistischen Angreifertechniken, nicht die vollständige Abdeckung.
- Maßnahmenliste je identifizierter Bedrohung mit Verweis auf umsetzende Architekturentscheidungen (S-ADR), Code-Kontrollen oder organisatorische Maßnahmen.
- Restrisiko-Betrachtung mit Risikoeigner\*in und Termin für die nächste Überprüfung.

**Aktualisierungspflicht:** Das Bedrohungsmodell wird aktualisiert, wenn sich Authentifizierung, Autorisierung, Privilegiengrenzen, Deployment-Topologie, extern erreichbare Endpunkte, sensible Datenflüsse oder Drittintegrationen wesentlich ändern. Wichtige STRIDE- und CAPEC-Befunde fließen in S-ADRs, Sicherheits-Checklisten und Aufgabenlisten ein. CAPEC-Verweise werden direkt im Bedrohungsmodell festgehalten, damit die Zuordnung an einer Stelle prüfbar bleibt.

Mitgeltende Checkliste: CL_Bedrohungsmodellierung (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 04).

## Lieferkettentransparenz und Build-Integrität

Sichere Entwicklung umfasst Transparenz darüber, was ausgeliefert wird, und darüber, wie es erzeugt wurde.

**Verbindliche Regelungen:**

- Software Bill of Materials (SBOM): Für jedes release- oder verteilbare Artefakt wird je Release eine maschinenlesbare SBOM erzeugt. Etablierte Formate sind CycloneDX und SPDX. Die SBOM wird als Release-Asset oder im Sicherheitsdokumentenbestand abgelegt. Das gilt unabhängig davon, ob sie auch extern veröffentlicht wird.
- KI-Komponenten in der SBOM: Bindet ein Artefakt ein KI-Modell oder einen KI-Dienst ein, wird diese KI-Abhängigkeit in der SBOM bzw. im Lieferketten-Evidenz-Dokument geführt (Anbieter, Modell- bzw. Dienst-Identifikator, Version oder Endpunkt, Verweis auf Model Card oder AI-SBOM des Anbieters); CycloneDX bildet dies über ML-BOM-Komponenten ab. Eine eigene Modell- oder Trainingsdaten-SBOM wird für fremdbezogene Modelle nicht erzeugt; maßgeblich ist die Anbieter-Transparenz (siehe Abschnitt „KI-gestützte Codeerzeugung").
- Vulnerability Exploitability eXchange (VEX): Wenn eine ausgelieferte oder geprüfte Komponente eine bekannte Schwachstelle enthält, wird ein VEX-Statement geführt. Es weist klar aus, ob das Produkt betroffen, nicht betroffen, mitigiert oder noch in Untersuchung ist. CSAF-VEX gilt als Referenzformat.
- Build-Integrität (SLSA): CI/CD-erstellte oder veröffentlichte Artefakte zielen auf SLSA-v1.2-Kontrollen für Build-Provenienz und Build-Integrität. Skript-gesteuerte und automatisierte Builds sowie Provenienz-Nachweise werden angestrebt, sofern das Toolset es erlaubt. Öffentlich konsumierte Artefakte streben mittelfristig mindestens SLSA Build L2 an.
- OpenSSF Scorecard: Vor Aufnahme einer relevanten externen Abhängigkeit oder vor dem Release eines öffentlichen OSS-Repositoriums wird der Scorecard-Wert (oder eine gleichwertige Quelle für die Repository-Sicherheitsposition) gesichtet.
- Automatisiertes Abhängigkeits-Management: Renovatebot oder Dependabot werden bevorzugt eingesetzt. Sie öffnen automatisch Pull Requests für veraltete oder verwundbare Abhängigkeiten. Ein zentrales SBOM-/CVE-Tracking (zum Beispiel OWASP Dependency Track) wird gegenüber regelmäßigen manuellen Audits bevorzugt, sofern die Hosting-Infrastruktur dies erlaubt.
- Statische Abhängigkeitsdokumente ergänzen das automatisierte Tooling für Release-Entscheidungen, Ausnahmen und Risikoakzeptanz; sie ersetzen es nicht.
- SBOM-, VEX-, Provenienz- und Scorecard-Erkenntnisse fließen in die Release-Freigabe und in die Bewertung der Lieferanten- und Drittabhängigkeiten ein.
- **Schutz vor Schadsoftware in Build-Artefakten (ISO/IEC 27002:2022 A.8.7):** Build-Artefakte (Container-Images, Pakete, Binaries) und auf den Build-Runnern eingehende Drittpakete werden vor dem Release einem Malware-Scan unterzogen. Etablierte Werkzeuge sind ClamAV, Microsoft Defender, Trivy mit `--scanners malware`, Grype und Container-Image-Scanner der Registry. Treffer blockieren den Release; Ausnahmen werden mit Begründung und Ablaufdatum im Sicherheits- oder Risikoregister festgehalten. Die Build-Runner selbst sind als gehärtete, kurzlebige Umgebungen (zum Beispiel ephemere VMs oder Container) auszuführen.

Mitgeltende Checkliste: CL_Lieferkette-Build-Integritaet (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 05).

## Softwaretests und -abnahme

In der Entwicklungsphase werden Sicherheit, Stabilität und Funktionalität der Software durch Software- und Usability-Tests geprüft. Die Tests werden gemäß den Kriterien der „Richtlinie Testmanagement" durchgeführt und dokumentiert. Vor dem Produktivsetzen kann ein Schwachstellenscan veranlasst werden.

Tests zur Vorbereitung der Abnahme oder Produktivsetzung laufen auf einem Testsystem (Staging-System). Dieses ist im Stand des Betriebssystems und des Software-Stacks identisch mit dem Produktivsystem. Erst wenn alle Tests erfolgreich waren, erfolgt das Deployment in Produktion. Die zwischenzeitlich entwickelte Software wird ebenfalls per Versionskontrolle geprüft.

Enthaltene Drittsoftware wird auf Patch- und Änderungsmanagement geprüft. Kompatibilitätsprüfungen werden bei Bedarf durchgeführt, um mögliche Auswirkungen früh zu erkennen.

Vor Einführung neuer Technologien werden diese auf Sicherheitsrisiken und Schwachstellen geprüft (zum Beispiel CVE- und CVSS-Abfragen in Schwachstellendatenbanken).

Bei den Tests gilt das Vier-Augen-Prinzip. Es wird mit den Mechanismen von Git umgesetzt. Erst wenn eine zweite Person die Änderung geprüft und getestet hat, wird sie für das Deployment auf das nächste System freigegeben. Das gilt für die Stufen Entwicklung -> Test und Test -> Produktiv. Bei Beanstandungen geht die Änderung zurück in die Entwicklung.

Die Abnahme von Code beziehungsweise die Übergabe an die Kundschaft wird in einem Abnahmeprotokoll dokumentiert, das auch eventuelle Nachbesserungswünsche festhält. Die durchgeführten Testfälle werden gemäß dem Testmanagement der Organisation dokumentiert.

Mitgeltende Dokumente: [Richtlinie Testmanagement](mitgeltende-dokumente/Richtlinie_Testmanagement.md); mitgeltende Checkliste: CL_Code-Review-Sicherheit für Pull-Request-Reviews (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 08).

### Schutz während Audit-Tests und Sicherheitsprüfungen

Audit-, Penetrations- und Schwachstellentests werden so durchgeführt, dass der Produktivbetrieb nicht beeinträchtigt wird (ISO/IEC 27002:2022 Control A.8.34 „Schutz von Informationssystemen während Auditprüfungen"). Dafür gilt:

- Aktive Tests (Schwachstellenscans, Penetrationstests, Lasttests, Forensik-Übungen) finden bevorzugt auf dedizierten Test- oder Stage-Systemen statt. Tests gegen Produktivsysteme werden nur mit ausdrücklicher Genehmigung durchgeführt.
- Audit- und Prüfungskonten werden als Read-only-Konten mit minimal nötigen Rechten angelegt und nach Abschluss der Prüfung deaktiviert oder gelöscht. Schreibzugriffe sind nur möglich, wenn dies der Prüfauftrag ausdrücklich vorsieht.
- Prüf- und Auditfenster werden vorab schriftlich abgestimmt, mit Zeitfenster, Scope, eingesetzten Werkzeugen, Ansprechpersonen und Eskalationsweg.
- Prüfwerkzeuge und Auswertungen (Scan-Ergebnisse, Logs, Exports) werden vertraulich behandelt, geordnet abgelegt und nach Abschluss gemäß Aufbewahrungsfrist gelöscht oder archiviert. Zugriff ist auf den Prüfteam-Kreis und die zuständigen organisationsinterne Stellen begrenzt.
- Auftraggebende, Prüfende und betroffene Betriebsteams werden benannt und in einem kurzen Abschlussbericht zusammengeführt. Befunde fließen in den kontinuierlichen Verbesserungsprozess (siehe Abschnitt „Einordnung im ISMS", Klausel 10) zurück.

Mitgeltende Dokumente: [Richtlinie Testmanagement](mitgeltende-dokumente/Richtlinie_Testmanagement.md), [Richtlinie Zugangssteuerung](mitgeltende-dokumente/Richtlinie_Zugangssteuerung.md).

## Ausgelagerte Entwicklungsprojekte

Manche Entwicklungsprojekte werden aus Kapazitätsgründen ausgelagert. Die für die Organisation tätigen externen Entwicklerinnen und Entwickler werden regelmäßig auf Qualität geprüft. Vor dem Produktivsetzen erfolgt ein Code-Review durch erfahrene interne Entwicklerinnen und Entwickler der Organisation. Die Software durchläuft denselben System- und Abnahmetest wie interne Projekte. Für die Lieferanten gelten zusätzlich die Regeln des Lieferantenmanagements.

Mitgeltende Dokumente: [Richtlinie Dienstleister- und Lieferantenbeziehungen](mitgeltende-dokumente/Richtlinie_Dienstleister-und-Lieferantenbeziehungen.md).

## Trennung von Entwicklungs-, Test- und Betriebsumgebungen

Bei selbst entwickelter Software trennt die Organisation Entwicklungs-, Test- und Produktivsysteme strikt voneinander. Beim Einsatz fertiger Lösungen ohne Eigenentwicklung sind mindestens Test- und Produktivsysteme strikt getrennt.

Aufbau und Wiederherstellung der Systeme erfolgen über versionierte Infrastruktur- oder Konfigurationsartefakte in getrennten Repositorien für Test/Stage und Produktion. Projektquellcode, Betriebs- und Serverkonfigurationen werden logisch getrennt verwaltet. Die Trennung wird in möglichst allen Projekten umgesetzt.

Test- beziehungsweise Stage- und Produktivsysteme sind je mit einem eigenen Branch im Git-Repository verbunden. Erste Änderungen finden im Entwicklungssystem statt. Sind sie stabil genug, werden sie per Merge Request in den Test- beziehungsweise Stage-Branch übernommen. Sind die Tests erfolgreich und treten keine schweren Fehler auf, werden sie per Merge Request und nach RfC (siehe nächster Absatz) in das Produktivsystem überführt.

Dieser letzte Schritt erfolgt erst nach Ankündigung und Genehmigung über einen dokumentierten Request for Change im vorgesehenen Wartungsfenster.

Die Systeme sind als virtuelle Maschinen umgesetzt. Bei Bedarf (zum Beispiel bei Cluster-Systemen) liegen sie auf getrennten VMware-Hostsystemen. Test- und Stage-Systeme liegen in einem dafür vorgesehenen Bereich der Virtualisierungsumgebung. Produktivsysteme liegen im Standardfall ebenfalls in einem dafür vorgesehenen Bereich; bei besonders wichtigen oder kritischen Systemen liegen sie im Hochverfügbarkeitsbereich.

Durch die Trennung von Entwicklungsrechnern und Servern findet die eigentliche Entwicklung auf den Geräten der Entwicklerinnen und Entwickler statt. Sie übertragen ihre Änderungen per Git push, merge oder pull in den passenden Entwicklungs-Branch des Repositoriums. So gelangen Compiler, Editoren und sonstige Entwicklungswerkzeuge nicht auf Test-, Stage- oder Produktivsysteme. Abweichungen von der Trennung müssen begründet, risikobewertet und im Sicherheits- oder Betriebsdokument festgehalten werden.

Entwicklerinnen, Entwickler und Testende nutzen ein eigenes Test-Konto, das vom Alltagskonto getrennt und im Namen kenntlich gemacht ist. Das senkt das Risiko von Fehlern.

In Entwicklungs- und Testsystemen liegen keine Produktionsdaten, damit es nicht zu unbeabsichtigten Änderungen an echten Daten kommt. Ausnahmen sind nur nach Absprache und Genehmigung der Auftraggeberin oder des Auftraggebers möglich und werden dokumentiert.

Mitgeltende Checkliste: CL_Sichere-Entwicklungsumgebung (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 10).

## Schwachstellenoffenlegung und -behandlung

Die Organisation betreibt einen dokumentierten Prozess für die Aufnahme, Bewertung und Behebung gemeldeter Schwachstellen, sowohl intern als auch extern. Der Prozess folgt dem Prinzip der Coordinated Vulnerability Disclosure (CVD).

**Verbindliche Regelungen:**

- Externe Meldewege: Für jede veröffentlichte Anwendung gibt es einen klar erreichbaren Meldepfad. Etablierte Optionen sind eine `security.txt` gemäß RFC 9116 für Web-Anwendungen, ein Sicherheits-Postfach oder ein Issue-Template im Versionskontrollsystem. Der Meldeweg ist in den jeweiligen IT-Betriebskonzepten verlinkt.
- Triage und Klassifikation: Eingehende Meldungen werden zeitnah erfasst, reproduziert, mit CVSS v4.0 bewertet und nach Schweregrad (Kritisch, Hoch, Mittel, Niedrig) priorisiert. Score und CVSS-Vektor werden im Ticket dokumentiert. Wenn nur CVSS v3.1-Daten verfügbar sind, wird die Übergangsgrundlage festgehalten. Die Klassifikation orientiert sich an CWE-Kategorien und am zugehörigen Bedrohungsmodell.
- Behebungsfristen: Die Fristen richten sich nach dem Schweregrad. Aktiv ausgenutzte Schwachstellen in Produktivsystemen haben Vorrang. Geplante und außerplanmäßige Updates erfolgen über das Changemanagement.
- Meldepflichten gegenüber Behörden und Nutzenden: Für Anwendungen im Anwendungsbereich des EU Cyber Resilience Act (siehe folgender Abschnitt) gelten gestufte Meldepflichten. Aktiv ausgenutzte Schwachstellen und schwere Sicherheitsvorfälle werden innerhalb von 24 Stunden als Frühwarnung gemeldet. Innerhalb von 72 Stunden folgt die Hauptmeldung. Der Schlussbericht folgt spätestens 14 Tage nach Bereitstellung einer Korrekturmaßnahme bei aktiv ausgenutzten Schwachstellen oder innerhalb eines Monats bei schweren Sicherheitsvorfällen. Sicherheitsupdates werden den Nutzenden in den gesetzlich vorgegebenen Fristen bereitgestellt.
- Kommunikation: Sicherheitsrelevante Updates werden mit Bezug auf betroffene Versionen, Auswirkungen, Mitigationen und VEX-Status kommuniziert.
- Rückkopplung: Erkenntnisse fließen in Bedrohungsmodelle, S-ADRs, Code-Reviews, Schulungen und betroffene Sicherheitsdokumente zurück. So wirken Verbesserungen strukturell und nicht nur punktuell.

Mitgeltende Checkliste: CL_Schwachstellenoffenlegung (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 06).

## EU Cyber Resilience Act (CRA)

Für jedes Entwicklungsprojekt wird bewertet, ob die Software als „Produkt mit digitalen Elementen" im Sinne der Verordnung (EU) 2024/2847 (Cyber Resilience Act, CRA) auf den Unionsmarkt gebracht wird. Das gilt kommerziell, lizenzbasiert oder zu wirtschaftlichen Zwecken unentgeltlich. Auch quelloffene Software kann betroffen sein, wenn sie zu wirtschaftlichen Zwecken bereitgestellt wird.

**Verbindliche Regelungen für CRA-anwendbare Produkte:**

- SBOM-Pflicht je Release (siehe Abschnitt „Lieferkettentransparenz und Build-Integrität").
- Dokumentierter Prozess für Schwachstellenoffenlegung und -behandlung einschließlich Frühwarnung nach 24 Stunden, Hauptmeldung nach 72 Stunden und Schlussbericht.
- Bereitstellung von Sicherheitsupdates innerhalb der gesetzlich vorgegebenen Fristen und über den unterstützten Lebenszyklus hinweg.
- Dokumentierte Konformitätsbewertung: Selbstbewertung für die Mehrzahl der Produkte; Drittbewertung für kritische beziehungsweise wichtige Produkte gemäß Anhang III/IV der Verordnung.
- Kennzeichnung, technische Dokumentation und Konformitätserklärung gemäß CRA-Anforderungen werden vor der Marktbereitstellung erstellt.

Auch außerhalb des formalen CRA-Anwendungsbereichs richtet die Organisation ihre Praxis an den CRA-Grundprinzipien aus: Secure-by-Design, Secure-by-Default, fortlaufendes Schwachstellenmanagement, Lebenszyklus-Transparenz und SBOM-Verfügbarkeit. Die CRA-Anwendbarkeitsentscheidung wird im Sicherheitsdokumentenbestand des Projekts festgehalten (zum Beispiel im Lieferketten-Evidenz-Dokument oder als S-ADR).

**Zeitlicher Rahmen:** Die Verordnung ist seit dem 10. Dezember 2024 in Kraft. Die Regeln zu Konformitätsbewertungsstellen gelten ab dem 11. Juni 2026. Die Meldepflichten nach Artikel 14 gelten ab dem 11. September 2026. Die übrigen Pflichten gelten ab dem 11. Dezember 2027.

Mitgeltende Checkliste: CL_CRA-Anwendbarkeit (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 07).

## Datenschutz-Folgenabschätzung (DSGVO Art. 35)

Für Neuentwicklungen und wesentliche Erweiterungen von Anwendungen, die personenbezogene Daten (pbD) im Sinne der Datenschutz-Grundverordnung (DSGVO, Verordnung (EU) 2016/679) verarbeiten, ist eine Schwellwertanalyse zur Datenschutz-Folgenabschätzung (Data Protection Impact Assessment, DPIA) verpflichtend durchzuführen. Liegt nach der Schwellwertanalyse ein voraussichtlich hohes Risiko für die Rechte und Freiheiten natürlicher Personen vor, wird eine vollständige DPIA gemäß DSGVO Art. 35 erstellt.

**Verbindliche Regelungen:**

- **Schwellwertanalyse zu Projektbeginn:** Jedes Vorhaben mit Verarbeitung personenbezogener Daten startet mit einer dokumentierten Schwellwertanalyse. Sie prüft die in DSGVO Art. 35 Abs. 3 genannten Fälle (systematische und umfassende Bewertung persönlicher Aspekte, umfangreiche Verarbeitung besonderer Kategorien, systematische Überwachung öffentlich zugänglicher Bereiche), die Positivliste der zuständigen Datenschutzaufsichtsbehörde des Projekt- oder Sitzlandes und gegebenenfalls die DPIA-Liste des Europäischen Datenschutzausschusses.
- **DPIA-Pflicht bei hohem Risiko:** Wenn die Schwellwertanalyse ein voraussichtlich hohes Risiko ergibt, ist eine DPIA gemäß DSGVO Art. 35 Abs. 7 mit folgenden Mindestbestandteilen zu erstellen: systematische Beschreibung der Verarbeitung, Bewertung der Notwendigkeit und Verhältnismäßigkeit, Bewertung der Risiken für Betroffene, geplante Abhilfemaßnahmen.
- **Beteiligung der oder des Datenschutzbeauftragten:** Die oder der Datenschutzbeauftragte der Organisation wird gemäß DSGVO Art. 35 Abs. 2 in jeder DPIA konsultiert. Die Stellungnahme wird Teil der DPIA-Dokumentation.
- **Konsultation der Aufsichtsbehörde:** Verbleibt nach den geplanten Maßnahmen ein hohes Restrisiko, wird die zuständige Aufsichtsbehörde gemäß DSGVO Art. 36 vor Verarbeitungsbeginn konsultiert.
- **Verzeichnis der Verarbeitungstätigkeiten (DSGVO Art. 30):** Jede Verarbeitung personenbezogener Daten wird im zentralen Verzeichnis von Verarbeitungstätigkeiten der Organisation geführt. DPIA-Ergebnisse werden mit dem Eintrag verknüpft.
- **Auftragsverarbeitung (DSGVO Art. 28):** Werden externe Auftragsverarbeiter eingebunden (zum Beispiel Cloud-Dienste, externe Entwicklungspartner), liegt ein gültiger Auftragsverarbeitungsvertrag vor, der die Anforderungen aus DSGVO Art. 28 Abs. 3 erfüllt.
- **Datenschutz durch Technikgestaltung (DSGVO Art. 25):** Privacy by Design und Privacy by Default sind Pflicht in Architektur und Default-Konfiguration. Die acht Architekturprinzipien aus dem Abschnitt „Sichere Softwarearchitektur" werden um datenschutzspezifische Maßnahmen ergänzt: Datenminimierung, Zweckbindung, Speicherbegrenzung, Pseudonymisierung wo möglich, transparente Information der Betroffenen, einfache Wahrnehmung von Betroffenenrechten.
- **Fortschreibung:** Die DPIA wird bei wesentlichen Änderungen der Verarbeitung (neue Datenkategorien, neue Empfänger, neue Übermittlung in Drittländer, neue Technologie, neue Zweckbestimmung) erneut geprüft und gegebenenfalls aktualisiert. Die regelmäßige Fortschreibung erfolgt mindestens jährlich.

Die Verbindung zwischen DPIA und Bedrohungsmodellierung wird im Sicherheitsdokumentenbestand des Projekts hergestellt: Die DPIA betrachtet Risiken aus Sicht der Betroffenen, das Bedrohungsmodell betrachtet Risiken aus Sicht der Organisation und ihrer Schutzziele. Beide Sichten werden konsistent geführt.

Mitgeltende Dokumente: [Datenschutzleitlinie](mitgeltende-dokumente/Datenschutzleitlinie.md); mitgeltende Checkliste: CL_Datenschutz-Folgenabschaetzung (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 11).

## KPI für Sicherheit und Testabdeckung

Die Programmierprojekte der Organisation werden mit Unit- und Integrationstests fortlaufend geprüft. Sobald ein Projekt in das Git-Repository eingecheckt wird, prüfen Unit-Tests die interne API. Integrationstests prüfen die dokumentierten öffentlichen Schnittstellen (zum Beispiel REST API, CLI oder vergleichbare externe API) und die dokumentierten kritischen UI-Flows. So bleibt sichergestellt, dass eine Code-Änderung die Funktionalität von API, Schnittstellen und UI nicht bricht. Erst nach erfolgreichen Tests startet das Deployment.

Das im Scope befindliche Projekt verfügt über nachvollziehbare Tests, um die Funktionalität der Anwendung zu sichern. Die Testergebnisse dienen auch als KPI und werden als Build-, Test- oder Qualitätsbericht im jeweiligen Repository oder im vorgesehenen Nachweisspeicher abgelegt.

**Verbindliche Mindestschwellen:** Für jedes Projekt im Geltungsbereich gelten messbare Zielwerte. Sie werden je Release erhoben und im Sicherheits- oder Qualitätsmanagement-System abgelegt.

- Testabdeckung (Code Coverage) der Unit-Tests: mindestens 80 % Line Coverage oder besser je Projekt.
- Branch Coverage für Module mit sicherheitskritischer Logik: mindestens 85 % oder besser. Dazu gehören insbesondere Authentifizierung, Autorisierung, Eingabevalidierung, Krypto, Sitzungsverwaltung, Zahlungspfade und vergleichbare Schutzfunktionen.
- KI-erzeugter Code: mindestens 80 % Line Coverage oder besser und mindestens 80 % Branch Coverage oder besser je betroffenem Modul. Wenn Mutation Testing technisch sinnvoll und im Projektwerkzeug verfügbar ist, gilt zusätzlich ein Mutation-Score von mindestens 70 % oder besser.
- Integrationstest-Abdeckung öffentlicher Schnittstellen: mindestens 80 % oder besser der dokumentierten öffentlichen Schnittstellen, REST APIs, CLI-Kommandos und vergleichbaren externen Schnittstellen. Jede neue oder geänderte öffentliche Schnittstelle ist durch mindestens einen positiven und einen negativen Integrationstest abgedeckt.
- Integrationstest-Abdeckung kritischer UI-Flows: mindestens 80 % oder besser der dokumentierten kritischen UI-Flows, zum Beispiel Login, Rollenwechsel, Datenänderung, Freigabe, Export, sicherheitsrelevante Einstellungen und Fehlerfälle.
- Mean Time to Remediate (MTTR) für CVE-basierte Schwachstellen, gemessen ab interner Bestätigung: kritisch (CVSS 9,0 bis 10,0) spätestens nach 7 Tagen; hoch (CVSS 7,0 bis 8,9) spätestens nach 30 Tagen; mittel (CVSS 4,0 bis 6,9) spätestens nach 90 Tagen; niedrig (CVSS 0,1 bis 3,9) im nächsten regulären Wartungsfenster.
- ASVS-Verifikationsabdeckung: für Web-, API- und HTTP-basierte Dienste mindestens ASVS Level 1 vollständig nachgewiesen; für authentifizierte oder personenbezogene Datenflüsse mindestens Level 2.
- SBOM-Abdeckung je Release: 100 % der ausgelieferten Artefakte verfügen über eine maschinenlesbare SBOM (CycloneDX oder SPDX).
- Anteil automatisierter Abhängigkeitsaktualisierungen (z. B. Renovate, Dependabot): mindestens 80 % oder besser der Abhängigkeiten werden automatisch überwacht; offene kritische Befunde blockieren den Release.
- Anteil signierter Commits auf `main`/`master`: 100 %.
- Sicherheitsrelevante Pipeline-Ausfälle (z. B. SAST-, Secret-, SBOM- oder Lizenz-Check): keine offenen Befunde der Schweregrade „kritisch" oder „hoch" beim Merge in den Hauptzweig.

Abweichungen werden mit Begründung im Sicherheits- oder Risikoregister festgehalten und mit einem Ablaufdatum versehen.

Mitgeltende Dokumente: [Richtlinie Testmanagement](mitgeltende-dokumente/Richtlinie_Testmanagement.md).

Mitgeltende Checklisten: CL_Lieferkette-Build-Integritaet, CL_Code-Review-Sicherheit, CL_KI-Codeerzeugung, CL_Schwachstellenoffenlegung (siehe `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 05, 08, 09 und 06).

## Verweise

**Lokale und mitgeltende Verweise:**

- Checklistensammelband zur Richtlinie Sichere Entwicklung: `Checklistensammelband_Sichere-Entwicklung.md`, Kapitel 01 bis 12 (mitgeltend zu dieser Richtlinie)
- Mitgeltende Dokumente in `docs/secure-development/mitgeltende-dokumente/`
- Projektspezifische Listen genehmigter Werkzeuge, Repositories, Testberichte und Freigaben werden bei Bedarf lokal im jeweiligen Projekt verlinkt.

**Normen und Standards:**

- [ISO/IEC 27001:2022](https://www.iso.org/standard/27001): Information security, cybersecurity and privacy protection - Information security management systems.
- [ISO/IEC 27002:2022](https://www.iso.org/standard/75652.html): Information security controls.
- [NIST SP 800-218 Secure Software Development Framework](https://csrc.nist.gov/publications/detail/sp/800-218/final).
- [NIST SP 800-207 Zero Trust Architecture](https://csrc.nist.gov/publications/detail/sp/800-207/final).
- [RFC 9116 security.txt](https://www.rfc-editor.org/rfc/rfc9116): File format for vulnerability disclosure.

**OWASP-Ressourcen:**

- [OWASP Application Security Verification Standard (ASVS)](https://owasp.org/www-project-application-security-verification-standard/).
- [OWASP Top 10](https://owasp.org/Top10/).
- [OWASP Software Assurance Maturity Model (SAMM)](https://owaspsamm.org/).
- [OWASP Cheat Sheet Series](https://cheatsheetseries.owasp.org/).
- [OWASP Proactive Controls](https://owasp.org/www-project-proactive-controls/).
- [OWASP Dependency Track](https://dependencytrack.org/).

**Schwachstellen- und Angriffsmuster-Kataloge:**

- [CWE Top 25 Most Dangerous Software Weaknesses](https://cwe.mitre.org/top25/).
- [CAPEC Common Attack Pattern Enumeration and Classification](https://capec.mitre.org/).

**Lieferkette und Build-Integrität:**

- [SLSA Supply-chain Levels for Software Artifacts](https://slsa.dev/).
- [CycloneDX SBOM Standard](https://cyclonedx.org/).
- [SPDX SBOM Standard](https://spdx.dev/).
- [OASIS CSAF mit VEX-Profil](https://oasis-open.github.io/csaf-documentation/).
- [OpenSSF Scorecard](https://scorecard.dev/).

**Speichersichere Sprachen:**

- [CISA: The Case for Memory Safe Roadmaps](https://www.cisa.gov/resources-tools/resources/case-memory-safe-roadmaps), Dezember 2023.

**EU-Recht:**

- [Verordnung (EU) 2024/2847 Cyber Resilience Act](https://eur-lex.europa.eu/eli/reg/2024/2847/oj).

## Abkürzungsverzeichnis

| Abkürzung | Bedeutung |
|---|---|
| ACME | Automated Certificate Management Environment |
| ADR | Architecture Decision Record |
| AG | Arbeitsgruppe |
| API | Application Programming Interface |
| AEAD | Authenticated Encryption with Associated Data |
| ASVS | Application Security Verification Standard (OWASP) |
| BCM | Business Continuity Management |
| CAPEC | Common Attack Pattern Enumeration and Classification |
| CIA | Confidentiality, Integrity, Availability |
| CISA | Cybersecurity and Infrastructure Security Agency (US) |
| Security Lead | Chief Information Security Officer |
| CLI | Command Line Interface |
| CRA | Cyber Resilience Act (Verordnung (EU) 2024/2847) |
| CSAF | Common Security Advisory Framework |
| CSPRNG | Cryptographically Secure Pseudo-Random Number Generator |
| CVD | Coordinated Vulnerability Disclosure |
| CVE | Common Vulnerabilities and Exposures |
| CVSS | Common Vulnerability Scoring System |
| CWE | Common Weakness Enumeration |
| Dokumentationsablage | Dokumentenmanagementsystem |
| DPIA | Data Protection Impact Assessment (Datenschutz-Folgenabschätzung) |
| DR | Disaster Recovery |
| DSGVO | Datenschutz-Grundverordnung (Verordnung (EU) 2016/679) |
| ECDSA | Elliptic Curve Digital Signature Algorithm |
| FPE | Format-Preserving Encryption |
| GCM | Galois/Counter Mode (Block-Cipher-Modus mit AEAD) |
| PKI | Public key infrastructure reference |
| Organisation | Gesellschaft für wissenschaftliche Datenverarbeitung mbH Beispielstadt |
| HPC | High Performance Computing |
| HSM | Hardware Security Module |
| IDE | Integrated Development Environment |
| IDM | Identity Management |
| IKT | Informations- und Kommunikationstechnik |
| Security-Verantwortliche*r | Security-Verantwortliche*r |
| ISMS | Information Security Management System |
| ISO | International Organization for Standardization |
| KI-Verantwortliche*r | KI-Beauftragte\*r |
| KMS | Key Management Service |
| KPI | Key Performance Indicator |
| LLM | Large Language Model |
| MAC | Message Authentication Code |
| MSL | Memory Safe Languages |
| MTTR | Mean Time to Remediate |
| MVC | Model-View-Controller |
| MVVM | Model-View-View-Model |
| NIST | National Institute of Standards and Technology (US) |
| OOP | Objektorientierte Programmierung |
| OSS | Open Source Software |
| OWASP | Open Worldwide Application Security Project |
| pbD | personenbezogene Daten (gemäß DSGVO) |
| PKI | Public Key Infrastructure |
| PQC | Post-Quantum Cryptography |
| Nachweisprozess | Nachweis- und Verbesserungsprozess |
| REST | Representational State Transfer |
| RfC | Request for Change |
| RPO | Recovery Point Objective |
| RTO | Recovery Time Objective |
| S-ADR | Security Architecture Decision Record |
| SAMM | Software Assurance Maturity Model (OWASP) |
| SAST | Static Application Security Testing |
| SBOM | Software Bill of Materials |
| SDLC | Software Development Life Cycle |
| SLSA | Supply-chain Levels for Software Artifacts |
| SoA | Statement of Applicability (Anwendbarkeitserklärung) |
| SP | Special Publication (NIST) |
| SPDX | Software Package Data Exchange |
| SSDF | Secure Software Development Framework (NIST SP 800-218) |
| SSH | Secure Shell |
| SSO | Single Sign-On |
| STRIDE | Spoofing, Tampering, Repudiation, Information Disclosure, Denial of Service, Elevation of Privilege |
| TCS | Trusted Certificate Service |
| TLS | Transport Layer Security |
| TPM | Trusted Platform Module |
| UI | User Interface |
| VCS | Version Control System |
| VEX | Vulnerability Exploitability eXchange |
| VM | Virtuelle Maschine |

## Versionshistorie

Die Versionshistorie dokumentiert wesentliche Änderungen dieser Richtlinie gemäß ISO/IEC 27001:2022 Klausel 7.5. Geringfügige redaktionelle Anpassungen werden nicht einzeln aufgeführt.

| Version | Datum | 2026-06-17 |
|---|---|---|---|
| 1.0.0 | 09.05.2025 | Security-Verantwortliche*r | Erstfassung der Richtlinie zur sicheren Softwareentwicklung |
| 1.1.0 | 14.05.2025 | Security-Verantwortliche*r | KPI-Abschnitt mit messbaren Mindestschwellen ergänzt |
| 1.2.0 | 27.04.2026 | Security-Verantwortliche*r | Sichere Softwarearchitektur (A.8.27), Krypto-Mindestvorgaben, KI-Codeerzeugung, Bedrohungsmodellierung, Lieferkettentransparenz, CRA-Bezug ergänzt; mitgeltende Checklisten eingeführt |
| 2.0.0 | 30.04.2026 | Security-Verantwortliche*r | Dokumenten-Metadaten und Versionshistorie eingeführt; Abschnitt „Einordnung im ISMS" mit ISO 27001-Klauseln ergänzt; Abschnitt „Geschäftsfortführung der Entwicklungsumgebung" (A.5.30) hinzugefügt; Abschnitt „Datenschutz-Folgenabschätzung" (DSGVO Art. 35) hinzugefügt; Daten-Maskierung (A.8.11), Malware-Scan in der Lieferkette (A.8.7) und Schutz während Audit-Tests (A.8.34) ergänzt; neue Checkliste CL_Datenschutz-Folgenabschaetzung in den Satz mitgeltender Checklisten aufgenommen |
| 2.0.1 | 02.05.2026 | Security-Verantwortliche*r | Verweise auf frühere Einzelablage durch Checklistensammelband `RL-SE-001-CL`, Kapitel 01 bis 11, ersetzt; SoA-/Anwendbarkeitserklärungs-Schnittstelle zum optionale ISO-27001-Nachweisfuehrung ergänzt |
| 2.1.0 | 07.05.2026 | Security-Verantwortliche*r | Abschnitt „Agentische KI in Sandbox-Umgebungen" ergänzt; Verbot direkter Agentenausführung auf Entwickler-Laptops, Mount-Grenzen, OpenCode-/Codex-Nutzung und GitHub-Spec-Kit-Governance-Presets geregelt; neue Checkliste CL_Agentische-KI-Sandbox aufgenommen; englische Fassung ergänzt |
| 2.2.0 | 14.05.2026 | Security-Verantwortliche*r | Abschnitt „Agentische KI in Sandbox-Umgebungen" um vier zulässige Sandbox-Typen (Container, MicroVM, klassische VM, dedizierter Workstation-Host), formellen Freigabe- und Lebenszyklusprozess (Initialfreigabe, jährliche Re-Validierung, außerplanmäßige Re-Validierung, Entzug, Pilotbetrieb) und Normbezüge zu ISO/IEC 27001:2022 Annex A, NIST AI RMF und OWASP LLM Top 10 erweitert; Netzwerk-Restriktion und reproduzierbares Tool-Pinning als verbindliche Regelungen ergänzt; CL_12 v1.1 mit erweiterten Prüfpunkten synchronisiert |
| 2.3.0 | 19.05.2026 | Security-Verantwortliche*r | Abschnitt „Agentische KI in Sandbox-Umgebungen": Initialfreigabe und Entzug von Sandbox-Umgebungen können neben der oder dem Security-Verantwortliche*r auch durch die oder den KI-Verantwortlichen erfolgen; Abkürzungsverzeichnis um KI-Verantwortliche*r ergänzt; mit CL_12 v1.2 synchronisiert |
| 2.4.0 | 19.05.2026 | Security-Verantwortliche*r | KI-Lieferkettentransparenz nach der G7-Leitlinie „Software Bill of Materials for AI – Minimum Elements" (2026) aufgenommen: Abschnitt „KI-gestützte Codeerzeugung" um Geltungsbereichs-Abgrenzung (KI-Nutzung gegenüber KI-Bereitstellung) und AI-SBOM-Lieferantentransparenz erweitert; Abschnitt „Lieferkettentransparenz und Build-Integrität" um KI-Komponenten in der SBOM ergänzt; G7-Leitlinie als Rahmenwerk aufgenommen; CL_05, CL_09 und CL_12 synchronisiert |
| 2.5.0 | 22.05.2026 | Security-Verantwortliche*r | Abschnitt „Agentische KI in Sandbox-Umgebungen" präzisiert: Feature-Implementierungen mit agentischer KI nutzen GitHub Spec Kit mit SDD-Ablauf und den sechs Governance-Presets, sofern keine begründete Ausnahme dokumentiert ist; CL_09 und CL_12 synchronisiert |
| 2.6.0 | 12.06.2026 | Security-Verantwortliche*r | Abschnitt „Agentische KI in Sandbox-Umgebungen" präzisiert: Governance-Presets müssen installiert und nachweisbar dokumentiert sein oder eine begründete Ausnahme haben; CL_12 um Preset-Liste, Versionen, Prioritäten, Projekt-Policy und Cache-Ausschluss als Prüfflächen ergänzt |
| 2.7.0 | 14.06.2026 | Security-Verantwortliche*r | Abschnitt „Agentische KI in Sandbox-Umgebungen" präzisiert: Spec-Kit-Governance-Presets werden mindestens quartalsweise und anlassbezogen auf Aktualität, wirksame Auflösung und Abdeckung durch RL-/CL-Prüfpunkte geprüft; AGENTS.md und CL_09/CL_12 synchronisiert |
| 2.8.0 | 14.06.2026 | Security-Verantwortliche*r | Abschnitt „Agentische KI in Sandbox-Umgebungen" präzisiert: Vollständige Spec-Kit-Markdown-Artefakte mit aktuellen Presets und Nachweis-Matrix können die separate manuelle Checklisten-Ausfüllung ersetzen; CL_09 und CL_12 synchronisiert |
| 2.8.1 | 15.06.2026 | Security-Verantwortliche*r | Verweise auf den Checklistensammelband von der Dokumentennummer `RL-SE-001-CL` auf den Dateinamen `Checklistensammelband_Sichere-Entwicklung.md` umgestellt; README.md und AGENTS.md synchronisiert |
| 2.9.0 | 15.06.2026 | Security-Verantwortliche*r | Neueste Spec-Kit-Governance-Preset-Prüfpunkte als auditfähige RL-/CL-Abdeckung nachgezogen: regulatorisches Screening, BSI C3A/C5, A11Y-/CLI-Prüfung, Cross-Platform-Parität, Secure-Coding-Profile und Agent-Guidance-Parität; Checklistensammelband auf Version 1.9.0 synchronisiert |
| 2.10.0 | 17.06.2026 | Security-Verantwortliche*r | KPI-Werte für Testabdeckung angehoben und präzisiert: Unit-Test Line Coverage mindestens 80 %, sicherheitskritische Module mindestens 85 % Branch Coverage, KI-Code mindestens 80 % Line und Branch Coverage, Integrationstest-Abdeckung öffentlicher Schnittstellen und kritischer UI-Flows mindestens 80 %; CL_08, CL_09 und Checklistensammelband synchronisiert |
| 2.10.1 | 26.06.2026 | Security-Verantwortliche*r | MSL-Beispielliste in Grundsätzen und Programmierungsabschnitt um Go, Rust und Swift bei passender Zielplattform präzisiert; sprachspezifische Architekturhinweise für Go, Rust und Swift ergänzt |
| 2.10.2 | 26.06.2026 | Security-Verantwortliche*r | Mitgeltende Leitlinie fuer sichere Entwicklungs-Sandboxen ergaenzt; Sandbox-Referenzprofil fuer MSL-basierte Level-2-Projekte, KI-Agenten, Spec Kit und Ausbildung eingeordnet; CL_12 und Sammelband synchronisiert |

**Genehmigung der aktuellen Fassung:**

- Fachliche Erstellung und Pflege: Security-Verantwortliche*r
- Freigabe: verantwortliche Leitung

---

# Secure Development Guideline

## Document Metadata

| Field | Value |
|---|---|
| Title | Secure Software Development Guideline |
| Version | 2.10.0 |
| Release date | 2026-06-17 |
| Effective date | 2026-06-17 |
| Document classification | publicly usable training and review baseline |
| Responsible unit | Project or training owner with security review |
| Approved by | Project or training owner after security review |
| Next review date | 2027-05-07 (annually, and ad hoc after material changes) |
| Document number / file reference | RL-SE-001 |
| ISMS reference | Part of the security evidence process according to ISO/IEC 27001:2022 |

This document is controlled according to ISO/IEC 27001:2022 clause 7.5 ("Documented Information"). Changes are recorded in the version history at the end of this document. The valid version is stored in the official Organisation document management system (Dokumentationsablage). Local working copies are only valid for ongoing editing.

## Related Documents

- [Secure Development Life Cycle Checklist](mitgeltende-dokumente/Checkliste_Secure-Development-Life-Cycle.md)
- [Use of cryptographic measures](mitgeltende-dokumente/Gebrauch_kryptografischer_Massnahmen.md)
- [Competency profiles and training plan for secure development](mitgeltende-dokumente/Kompetenzprofile_und_Schulungsplan_Sichere-Entwicklung.md)
- [Secure programming guideline](mitgeltende-dokumente/Leitlinie_Sichere-Programmierung.md)
- [Secure Development Life Cycle policy](mitgeltende-dokumente/Richtlinie_Secure-Development-Life-Cycle.md)
- [Change management policy](mitgeltende-dokumente/Richtlinie_Changemanagement.md)
- [Service-provider and supplier-relationship policy](mitgeltende-dokumente/Richtlinie_Dienstleister-und-Lieferantenbeziehungen.md)
- [Test management policy](mitgeltende-dokumente/Richtlinie_Testmanagement.md)
- [Access control policy](mitgeltende-dokumente/Richtlinie_Zugangssteuerung.md)
- [Privacy guideline](mitgeltende-dokumente/Datenschutzleitlinie.md)
- [Secure software design guideline](mitgeltende-dokumente/Leitlinie_Sicheres-Softwaredesign.md)
- [BCM and emergency handbook](mitgeltende-dokumente/BCM-Notfallhandbuch.md)
- [Guideline, checklist, and Spec Kit preset alignment](mitgeltende-dokumente/Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md)
- [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR](mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.pdf)
- [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR EN Markdown](mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.EN.md)
- [THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR DE learning version](mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.DE.md)

The central alignment file is the binding reading guide for later Spec Kit runs and reviews. It maps related documents to guideline sections, checklists, governance presets, and typical evidence paths. This helps first-year apprentices and developers without security specialist knowledge understand why a review item applies and which evidence is expected.

## Related Checklists

The detailed requirements and audit checks for this guideline are defined in the related compendium file `Checklistensammelband_Sichere-Entwicklung.md`. The compendium contains twelve separately auditable checklists as chapters 01 to 12 and replaces the former individual checklist storage. The checklist short names remain stable references.

- Chapter 01 / CL_Standards-Anwendbarkeit: selection of applicable security standards and regulatory applicability per project.
- Chapter 02 / CL_Sichere-Softwarearchitektur: eight architecture principles, S-ADR obligation, arc42 section 8, cloud autonomy, and cloud compliance assurance.
- Chapter 03 / CL_Krypto-Mindestvorgaben: minimum algorithms, key lengths, TLS, key management.
- Chapter 04 / CL_Bedrohungsmodellierung: STRIDE steps, CIA rating, CAPEC reference.
- Chapter 05 / CL_Lieferkette-Build-Integritaet: SBOM, VEX, SLSA, Scorecard, dependency maintenance.
- Chapter 06 / CL_Schwachstellenoffenlegung: CVD process, security.txt, triage, CRA 24-hour notification.
- Chapter 07 / CL_CRA-Anwendbarkeit: product classification, Annex III/IV, duties and timeline.
- Chapter 08 / CL_Code-Review-Sicherheit: cross-language and language-specific review items.
- Chapter 09 / CL_KI-Codeerzeugung: secure use of AI assistants when writing code.
- Chapter 10 / CL_Sichere-Entwicklungsumgebung: IDE, repositories, pipelines, secrets, audit logs, cross-platform script parity.
- Chapter 11 / CL_Datenschutz-Folgenabschaetzung: GDPR Art. 35 DPIA, threshold analysis, risk assessment, consultation.
- Chapter 12 / CL_Agentische-KI-Sandbox: agentic AI with OpenCode or Codex in isolated sandbox environments.

## Objectives

- Ensure confidentiality, integrity, and availability of data and services.
- Increase customer satisfaction.
- Strengthen Organisation's position as a reliable cooperation partner and IT service provider.
- Increase the resilience of IT infrastructure.

## Scope

Organisation develops software in several working groups:

- Working group N: network infrastructure.
- Working group I: data-center infrastructure.
- Working group E: software for research in life sciences.
- Working group C: software for high performance computing (HPC).

This guideline applies to all persons who develop software for Organisation or on behalf of Organisation. It also applies to the adaptation of standard software, for example open-source software.

## Position in the security evidence process

This guideline is a domain-specific security policy according to ISO/IEC 27001:2022 clause 5.2 and Annex A control A.5.1. It extends the overarching Organisation information security policy and defines requirements for a secure software development life cycle (SDLC) according to ISO/IEC 27002:2022 control A.8.25.

**References to ISO/IEC 27001:2022 clauses:**

- **Clause 6.1 (Actions to address risks and opportunities):** Risk assessment for development projects is performed in the central Organisation ISMS risk-management process. It follows ISO/IEC 27005:2022 and BSI IT-Grundschutz methods. Project-specific risks are deepened in the project threat model. Residual risks are recorded in the central Organisation risk register and agreed with the relevant risk owners.
- **Clause 7.5 (Documented information):** This guideline is subject to Organisation document control. Versioning, approval, distribution, and orderly archiving are handled through the Dokumentationsablage. Changes are recorded in the version history.
- **Clause 8 (Operation):** The guideline controls the operational security part of the software-development process. The related checklists are auditable work aids and provide evidence for internal and external audits.
- **Clause 9.2 (Internal audit):** Compliance with this guideline is part of the annual internal ISMS audit. The compendium file `Checklistensammelband_Sichere-Entwicklung.md`, chapters 01 to 12, is the primary audit tool per project. Samples are taken per working group (D, N, I, E, C).
- **Clause 9.3 (Management review):** The effectiveness of this guideline is reviewed annually in the ISMS management review, based on KPIs, audit findings, incidents, and vulnerability statistics.
- **Clause 10 (Improvement):** Findings from audits, incidents, and vulnerability triage are fed back into this guideline and its checklists through continual improvement.

**Responsibilities:**

- The Security-Verantwortliche*r of Organisation is responsible for maintaining the guideline.
- The Management Board approves the guideline and provides the required resources.
- The heads of working groups D, N, I, E, and C are responsible for implementation in their areas.
- The Data Protection Officer is involved in all projects that process personal data.

**Interface to the Statement of Applicability (SoA):**

This guideline and the compendium file `Checklistensammelband_Sichere-Entwicklung.md` provide domain-specific evidence for a project applicability note or optional ISO 27001 evidence management. The full ISO/IEC 27001:2022 Annex A matrix with all 93 controls is maintained in the Nachweisprozess and is not duplicated here. For development projects, the guideline and compendium provide at least the following SoA inputs:

- ISO/IEC 27001:2022 Annex A control ID or relevant external standard.
- Applicability: `applicable`, `not applicable`, or `project-dependent`.
- Implementation status: `implemented`, `partly implemented`, `open`, or `not applicable`.
- Reason for selection, non-applicability, or deviation.
- Evidence path, for example S-ADR, threat model, ASVS evidence, SBOM, VEX, DPIA, audit report, ticket, or operations documentation.
- Risk-register or action ID, responsible person, and next review date.

If a project identifies new risks, new controls, or justified non-applicability for Annex A controls, the project applicability note is updated in the Nachweisprozess by the Security-Verantwortliche*r or the responsible ISMS role. The compendium is an operational evidence source. The SoA remains authoritative for Annex A coverage, implementation status, and exclusion reasons.

## Evaluation

This guideline is reviewed once per year for currency and validity.

## Language and Accessibility

This file contains the German and English versions of the guideline. Subject-matter changes are maintained in both language versions. The target language level is CEFR B2: short sentences, clear terms, and explained abbreviations. Technical terms are written out at first use.

Documents, checklists, notifications, and templates are created in an accessibility-oriented way. Markdown files use a clear heading hierarchy, descriptive link text, and lists instead of layout tables. Information must not be conveyed only by color, special characters, or images. Screenshots and diagrams need a short text description.

For web content and published security information, WCAG 2.2 Level AA applies where the criteria apply to the artefact. Person references are written in an accessible way, for example with neutral terms or spelled-out paired forms.

## Principles

Organisation uses current best practices and design patterns. These include the waterfall model, object-oriented programming (OOP), Model-View-Controller (MVC), Model-View-View-Model (MVVM), and microservice architectures. These patterns meet the requirements of internal and external customers and suppliers. They apply to new development and further development of source code.

Source code is created in line with the Organisation secure programming guideline. The memory-safe programming languages recommended there are preferred. Organisation has a high quality standard in software development. Information security and data protection are considered in every project.

Common security techniques and standards are considered during specification and design. Software architects follow the related document "Organisation guideline for secure software design".

The programming languages used follow the related document "THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR". Its annex lists recommended memory-safe languages (MSL). Swift is included there as an MSL and is treated in this guideline as a memory-safe language, especially for Apple platforms. In training and level-2 projects, Java, C#, Python, Go, Rust, and Swift where the target platform fits are considered MSL choices. An MSL does not replace language-specific secure-coding review.

Developers write source code according to the Organisation secure programming guideline.

Organisation uses integrated development environments (IDEs) that are described in the "software installation guideline" and approved in the list of approved software. IDEs should run on the three main platforms Windows, macOS, and Linux. Vendors regularly update the IDEs with new functions and security updates.

Developed source code is managed in organisationsinterne operated version-control systems (Git). Access and transport are secured. Access uses single sign-on (SSO) for the web interface and security tokens or SSH login for IDEs. The web interface is also protected by a server certificate from the GEANT TCS PKI. The server certificate is managed automatically by bot software via ACME and renewed 30 days before expiry.

When hiring new developers, Organisation checks the required skills and existing expertise. The development team is trained regularly according to process "10 - Employee qualification and training". External training or the Organisation Academy may be used.

The rules from the access control guideline apply. The test management guideline additionally applies to controlled test management.

## Applicable Security Standards

The applicable security and architecture standards are selected for each project according to the following applicability matrix. For each standard, the specification, architecture, or security document records whether it is applicable or not, with "N/A" and a short reason. Silent omission is not allowed.

| Standard / guide | Priority | Applicability | Minimum expectation |
|---|---|---|---|
| NIST SSDF (SP 800-218) | MUST | all development projects | secure life cycle with preparation, source and build protection, secure creation, and vulnerability response |
| CWE Top 25 | MUST | all development projects | relevant weaknesses are considered in design, implementation, review, and remediation |
| OWASP ASVS 5.0.0 | MUST | web, API, HTTP, or authenticated services | ASVS level (1, 2, or 3), verification scope, and versioned ASVS IDs are selected and documented |
| SBOM | MUST | release or distributable artefacts | machine-readable component inventory per release |
| VEX | MUST | known vulnerabilities in delivered or assessed components | clear statement: affected, not affected, mitigated, or under investigation |
| SLSA v1.2 | SHOULD | CI/CD-built or published artefacts | build provenance and build integrity, at least Build L1 where practical |
| OWASP SAMM | SHOULD | long-lived projects and workspaces | regular self-assessment with improvement plan |
| CAPEC | SHOULD | threat modeling of relevant attack paths | reference to relevant attack patterns for high-risk flows |
| NIST Zero Trust (SP 800-207) | project-type-dependent | distributed, service-based, cloud, remotely managed, or identity-federated systems | explicitly decide applicability, controls, or justified "N/A" |
| OWASP Cheat Sheet Series and Proactive Controls | SHOULD | all developer-side projects | use as day-to-day implementation guidance below this guideline |
| OpenSSF Scorecard | project-type-dependent | public OSS repositories or critical external dependencies | assess security posture before adoption or release |

**Choice of ASVS level:**

- Level 1 for simple or internal web applications with limited risk.
- Level 2 for authenticated, multi-user, privileged, or internet-facing data services.
- Level 3 only for explicit high-security, high-impact, or regulatory requirements.

NIST SSDF and CWE Top 25 always apply. OWASP Cheat Sheets and Proactive Controls serve as daily help unless a language or framework sets stricter rules. Applicable standards are named in requirements, planning, and task artefacts. ASVS requirements are documented with version prefix, for example `v5.0.0-1.2.5`. Evidence such as ASVS verification, SBOM, VEX, or SAMM assessment is kept in the project's security documentation.

Related checklist: CL_Standards-Anwendbarkeit (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 01).

## Secure Software Architecture

Software architecture follows established secure architecture principles according to ISO/IEC 27002:2022 control A.8.27 and the iSAQB CPSA Foundation view of security as a binding quality attribute. Secure code alone is not enough. Architecture and implementation must work together to make systems resilient.

**Binding architecture principles:**

- Trust boundaries: Every system has clearly defined trust boundaries. Inputs crossing such boundaries are checked and sanitized before processing.
- Defense in depth: Security does not depend on one single control. At least two independent layers protect critical assets.
- Least privilege: Components, services, and processes run with the minimum required rights. Database accounts are role-specific and limited. File-system access is limited to required directories.
- Fail-safe defaults: Access is denied by default and only allowed by explicit permission. Error paths lead to a secure state.
- Attack surface reduction: Unused endpoints, services, ports, and functions are disabled or removed. Public APIs expose only what is needed. Debug endpoints and detailed error output are not reachable in production.
- Separation of concerns: Authentication, authorization, logging, and input validation are implemented as cross-cutting concerns and not scattered through business logic.
- Secure configuration: Secrets are stored in suitable secret stores. They are never in source code, versioned configuration files, or hard-coded constants.
- Supply-chain security: Third-party dependencies come from checked package registries. Lock files are committed. Known vulnerable dependencies are replaced or updated before release.

**Language-specific architecture notes (not exhaustive):**

- Java: central servlet filters or Spring Security configuration for authentication, authorization, and CSRF protection; bean validation for inputs; no Java serialization of untrusted data.
- C# / .NET: ASP.NET Core middleware for authentication, authorization, CORS, and anti-forgery; dependency injection for security-relevant services; IDataProtectionProvider for encryption at rest; UseHttpsRedirection for transport hardening.
- Python: WSGI or ASGI middleware layers; keep framework security defaults active, for example Django CSRF/XSS protection and FastAPI Pydantic validation.
- Go: pass `context` through network, database, and background operations; set HTTP timeouts; use `crypto/rand` for secrets; bound goroutine and channel lifecycles; parameterize database access.
- Rust: isolate and justify `unsafe`; use ownership and lifetime boundaries as security boundaries; combine `serde` deserialization with domain validation; use `rustls` or reviewed crypto crates.
- Swift / Apple platforms: use Keychain for secrets, CryptoKit for cryptography, and review App Sandbox plus entitlements; constrain URL and file-scope access; use Swift Concurrency to avoid uncontrolled shared mutable state.

Security-relevant architecture decisions are recorded as Security Architecture Decision Records (S-ADRs). They include context, decision, rationale, alternatives, consequences, and a short compliance table. Security cross-cutting concepts are documented according to arc42 section 8: authentication, authorization, encryption in transit and at rest, input validation, error handling, logging and audit trail, dependency management, and deployment security.

Related checklist: CL_Sichere-Softwarearchitektur (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 02).

## Cryptographic Minimum Requirements

Uniform minimum requirements for algorithms, key lengths, and procedures apply to every use of cryptography. They follow current BSI recommendations (TR-02102) and NIST recommendations (SP 800-131A, SP 800-175B), and are specified in the related document "Use of cryptographic measures".

**Binding minimum requirements:**

- Symmetric encryption: AES-256 in an authenticated mode, preferably AES-GCM or ChaCha20-Poly1305.
- Asymmetric encryption and signature: RSA with at least 3072 bits, ECDSA with P-256 or higher, or Ed25519. Key exchange uses ECDH (P-256+) or X25519.
- Hash functions: SHA-256 or higher, for example SHA-384, SHA-512, or SHA-3 family.
- Password hashing: Argon2id preferred, or scrypt or bcrypt with a high cost factor and a salt of at least 16 bytes.
- Message authentication code: HMAC with SHA-256 or higher. With AEAD procedures, no separate MAC is required.
- Transport Layer Security: TLS 1.2 as the minimum; TLS 1.3 is preferred. SSLv3, TLS 1.0, and TLS 1.1 are disabled.
- Random numbers: only from cryptographically secure sources such as `/dev/urandom`, `getrandom()`, `CryptGenRandom`, or library CSPRNGs. Predictable sources such as `Math.random` or `rand()` are not used.

**Forbidden or restricted procedures:**

- Forbidden for security purposes: MD5, SHA-1 for signatures, DES, 3DES, RC4, RSA-PKCS#1 v1.5 for new applications, and ECB mode for encryption.
- Exceptions are allowed only with documented risk acceptance and expiry date.

**Key management:**

- Keys are stored in a suitable secret store, for example Vault, cloud KMS, HSM, or OS keychain. They are never stored in source code or versioned files.
- Each key type has a rotation period. If compromise is suspected, rotation is immediate.
- Custom cryptography implementations are not allowed. Proven libraries are used, for example JCA/JCE, BouncyCastle, libsodium, .NET `System.Security.Cryptography`, or Python `cryptography`.
- Hardware-backed key stores such as HSM, TPM, cloud KMS, or smartcard are used where possible, especially for long-lived or highly sensitive keys.

**Cryptographic agility and outlook:**

- Algorithms, key lengths, and cipher suites can be changed in configuration without code changes.
- For long-lived data and signatures, migration to quantum-safe procedures is monitored (NIST PQC: ML-KEM, ML-DSA, SLH-DSA). A transition plan is kept per affected project.
- At least once per year, algorithms, key lengths, TLS configurations, and library versions are checked against current BSI and NIST recommendations.

Related documents: Use of cryptographic measures. Related checklist: CL_Krypto-Mindestvorgaben (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 03).

## Development Environment

Development environments are divided into development, test (sometimes called stage), and production. These systems are secured according to the state of the art. Organisation uses firewalls, antivirus solutions, automatic operating-system updates, and encrypted disks.

Test and development environments that are no longer needed are deleted promptly according to the state of the art. Test accounts are created only when needed and deleted according to policy. Production data is used only when there is no alternative; by default it is not used. Repositories are regularly checked for vulnerabilities. Developed software is subjected to security review at milestones, for example code review.

Backups are created regularly. Traffic into and out of the environment is strictly controlled.

Related checklist: CL_Sichere-Entwicklungsumgebung (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 10).

### Business Continuity of the Development Environment

The development environment is critical infrastructure for Organisation delivery capability. It is included in Organisation's business continuity management (BCM) and disaster recovery (DR) planning. Requirements follow ISO/IEC 27002:2022 control A.5.30 and ISO/IEC 27031.

**Binding minimum requirements:**

- **Recovery objectives (RTO/RPO):** Each central component of the development environment has an RTO and RPO in its operations documentation. Recommended minimum values: Git RTO 8 hours and RPO 1 hour; CI/CD runners and build system RTO 24 hours and RPO 24 hours; artefact registry and build cache RTO 24 hours and RPO 24 hours; secret stores RTO 4 hours and RPO 1 hour; issue and audit-log systems RTO 24 hours and RPO 24 hours.
- **Backup and restore:** Backups of Git database, repositories, artefacts, pipeline configurations, and secret stores are created at least daily. They are stored geographically or at least infrastructurally separate from production.
- **Restore tests:** At least once per year, a practical restore test is performed and documented for core components. It covers data restore, restart of CI/CD pipelines, and availability of build artefacts.
- **Failure scenarios:** Planned scenarios include failure of a virtualization platform, loss of storage backend, compromise of a build runner, and failure of the secret store. Escalation paths and responsibilities are recorded for each scenario.
- **Suppliers and external dependencies:** Availability of external package registries, identity providers, and code-signing services is included in the BCM plan. Mirror or caching strategies are preferred.
- **Documentation and exercise:** BCM plans are kept in the security or operations documentation of the respective system. Responsibilities and escalation paths are tested in regular exercises, at least tabletop exercises.

Related documents: Organisation BCM/emergency manual. Related checklist: CL_Sichere-Entwicklungsumgebung (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 10).

## Programming

New code is written in the memory-safe languages commonly used at Organisation, namely Java, C#, Python, Go, Rust, and Swift where the target platform fits, or in the language requested by the customer. For further development, the existing application language is used. Code is commented sufficiently according to the Organisation secure programming guideline and regularly tested for vulnerabilities. Programming follows the state of the art. A change of paradigm, for example a change of programming language, is documented and justified separately.

Proper documentation is expected in the team. Modular programming is used. Code is well structured and often arranged in reusable code blocks.

Developers can find vulnerabilities and usually fix them themselves. Tools such as issue trackers support the team.

Related checklist: CL_Code-Review-Sicherheit (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 08).

### Protection of Test Data

During development, generic test data is usually used. It has the same structure as production data, but a smaller volume. To keep tests reproducible, test data is maintained as a database dump or file archive in a code repository. Tests may be tailored to the test data, but this is not required.

If production data is required for tests, for example to reproduce a defect, it is loaded into the development or stage system only for that purpose. After the work is complete, it is deleted immediately.

**Data masking and anonymization (ISO/IEC 27002:2022 A.8.11):** Personal data and sensitive business data in test-data sets are generally anonymized, pseudonymized, or masked. Suitable procedures include:

- **Generated synthetic data:** Tools such as Faker, Mockaroo, or custom generators create structurally suitable but fictitious records. This is the preferred option for standard tests.
- **Deterministic masking:** Reversible or irreversible transformations with consistent mapping, for example HMAC-based pseudonymization with a project-specific key, preserve referential integrity in tests.
- **Format-preserving encryption (FPE):** Used for fields whose format must remain strict, for example IBAN or tax number.
- **k-anonymity and l-diversity:** Aggregation or generalization procedures for analysis test data so that individuals cannot be re-identified.
- **Subset and sampling strategies:** Reduction of data volume to what is needed, following data minimization under GDPR Art. 5(1)(c).

Masking and anonymization procedures are documented in the project test-data concept. If clear data is used as an exception, the obligation from the DPIA section applies and technical and organizational measures according to GDPR Art. 32 are taken.

### Versioning and Change Management

Source-code changes are managed in organisationsinterne operated Git version-control systems. Access is granted or removed according to the access control guideline. Change-management rules apply. Further security details for the VCS are in the operations concept of the respective application. Changes are documented traceably. New functions or modules are added to the operations concept.

Before changes to the hosting operating system or software stack, tests are performed. This preserves stability and security. Deep changes are limited to what is necessary. They are performed only when security for the operating system, application, and customer data is ensured. Tests are recorded in test documentation, for example vulnerability tests.

For necessary changes, especially in production systems, requests for change (RfC) are submitted. Only after all tests in development and test environments succeed, an RfC sets a date and the change is performed on that date. The exact process is defined in change-management rules. The related operations concepts are updated regularly.

Every two weeks, the "Application Development Team" meeting discusses progress, security topics, and maintenance measures for software and operational systems.

## AI-Assisted Code Generation

AI code assistants and large language models (LLMs) are used at Organisation only in a controlled way. They can increase productivity, but they do not reliably produce secure code. Explicit review of every AI output is mandatory.

This guideline governs the use of AI in Organisation application development. The provision of AI services, models, and model hosting by Organisation (for example genehmigter KI-Dienst, hosting of Hugging Face models) is subject to the responsible operations and service documents and is outside the scope of this guideline.

**Binding rules:**

- Approved tools: Only tools from the approved software list are used. Cloud-based and local tools are approved separately.
- AI supply-chain transparency: For AI services and models in use, the transparency information available from the provider is recorded or linked in the AI tool inventory — model identity and version, a link to the provider's model card or AI-SBOM, training and fine-tuning method, the origin and sensitivity of the training data as far as published, and AI-specific security properties. Organisation does not produce its own AI-SBOM for externally sourced models; it requests the information as supply-chain evidence. The reference framework is the G7 guideline "Software Bill of Materials for AI – Minimum Elements" (2026).
- Human review: Every AI output is reviewed by a person before commit. Blind commits are not allowed.
- Four-eyes rule for critical logic: Security-relevant logic, including authentication, authorization, input validation, cryptography, session management, and payment paths, is also reviewed by a second person.
- Dependencies: If AI proposes a new library, its existence, maintenance status, and known CVEs are checked before adoption. Proposed package names are checked against the official registry to prevent hallucinated packages ("slopsquatting").
- No custom cryptography: AI tools must not propose custom cryptography implementations. Only proven libraries are used.
- Data protection in prompts: Personal data, secrets, tokens, customer names, and confidential configurations must not be put into prompts to external AI services. Pre-commit protection and training are active.
- License clarification: AI-generated snippets can raise license questions. Doubtful snippets are rewritten or replaced by clearly licensed code.
- Tests: AI-generated code is covered by automated tests, including positive and negative cases. A green pipeline is required for merge.
- Marking in pull requests: Material AI contributions are marked in the description or by label so that review volume is clear.
- Audit trail: Tool use is traceable, including tool name, period, and involved persons. Central logs are retained for audits where possible.
- Training: Developers regularly receive training on strengths, weaknesses, and risks of AI code assistants.
- Exceptions: Deviations are recorded with reason, signature, and expiry date in the security or risk register.

Related checklist: CL_KI-Codeerzeugung (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 09).

## Agentic AI in Sandbox Environments

**Reason for scope:** Agentic AI tools such as OpenCode or Codex can change files, run commands, and read project context. They are therefore no longer a passive assistant but an active actor in the development process. For this reason they are operated only in isolated and formally approved sandbox environments. Direct execution on developer laptops, on shared servers, or in production-near environments is not allowed.

The sandbox accesses source code only through explicitly mounted host directories. The agent may only change files that are exposed through these mounts for the relevant project.

**Permitted sandbox types:**

- **Container sandbox:** OCI-compliant container runtime (for example Docker, Podman) running unprivileged, with dedicated volumes for tool data and explicit bind mounts to project directories.
- **MicroVM sandbox:** lightweight virtualization solution (for example Firecracker, Kata Containers) with hardware-assisted isolation.
- **Classic VM sandbox:** full virtual machine on an approved virtualization platform with a clearly defined image and restore point.
- **Dedicated workstation host:** physically or logically separated workstation used only for agentic AI work, without production accounts and without access to personal production data.

Other isolation mechanisms are allowed if they demonstrably reach a comparable protection level. The evidence is recorded in the security or architecture document of the respective sandbox.

Concrete reference profiles, minimum evidence, and training requirements for secure development sandboxes are described in the related [Secure Development Sandbox Guideline](mitgeltende-dokumente/Leitlinie_Sichere-Entwicklungs-Sandbox.md). It positions MSL-based level-2 projects, AI agents, Spec Kit, and public-ready sandbox reference environments.

**Binding rules:**

- The sandbox separates agent runtime, tool data, cache, session data, and credentials from the host system.
- The agent does not receive general access to home directories, keychains, SSH agents, GPG keys, browser profiles, cloud CLI configurations, or local token stores.
- Sandbox network access is restricted to the minimum set of required targets (for example approved model endpoints, approved package registries). Other outbound connections are blocked or documented with reasoning.
- Secrets are not copied into prompts, project files, logs, or screenshots. Local secret files remain outside versioned project artefacts and are injected via a secret store or protected environment variables.
- AI tools and their configurations are reproducibly pinned (versions, image digests, model identifiers). Self-updating mechanisms of the tools are disabled.
- Approved AI tools, providers, models, and configurations are tracked in the Organisation AI tool inventory (see section "AI-Assisted Code Generation").
- For feature implementations with agentic AI, GitHub Spec Kit is used for spec-driven development (SDD). The flow is run in sequence through `/speckit.constitution`, `/speckit.specify`, `/speckit.clarify`, `/speckit.plan`, `/speckit.checklist`, `/speckit.tasks`, `/speckit.analyze`, and `/speckit.implement`. The six governance presets `security-governance`, `architecture-governance`, `isaqb-architecture-governance`, `a11y-governance`, `cross-platform-governance`, and `agent-parity-governance` are installed and documented with evidence unless a justified project exception is documented.
- The presets are updated in a controlled way at least quarterly and when triggered by a relevant change. A triggered check is required when Spec Kit preset catalogs, preset contents, priorities, or project-local overrides change. The check covers the preset list, preset information, effective template resolution, and mapping of preset contents to the applicable checklists. Required changes are integrated into this guideline and into the checklists or documented as a justified exception.
- Preset content coverage is treated as its own audit question. The coverage includes, in particular, regulatory screening (NIS2, CRA, EU AI Act, DORA), BSI C3A, BSI C5, concrete WCAG 2.2 AA review, CLI accessibility, cross-platform script parity, language-specific secure-coding profiles, and agent-guidance parity. Preset review points that do not apply are documented explicitly with a rationale.
- The related-document alignment file is used as the default bridge between guideline sections, checklist chapters, related documents, governance presets, and evidence paths. A Spec Kit run may reference that file instead of repeating the whole mapping, but it must still record the concrete project decision and evidence.
- If a Spec Kit run with current checked presets documents the applicable review points fully, traceably, and as Markdown artefacts, separate manual checklist completion may be omitted. This requires a clear evidence matrix or a clear reference in the Spec Kit artefact that maps the applicable CL review points to `spec.md`, `plan.md`, `tasks.md`, analysis or checklist result, review, and preset evidence. Review points that are not covered, not applicable, or intentionally deviated from are still documented explicitly. This simplification does not replace sandbox approval, human review, the four-eyes rule, exception approval, or preset currency checks.
- Agentic changes are reviewed by humans before commit and merge. Security-critical logic follows the four-eyes rule.
- Use is documented traceably: tool, sandbox type, sandbox identifier (for example image digest or VM snapshot hash), project path, time period, responsible person, review result, and relevant Spec-Kit artefacts.

**Approval and lifecycle of sandbox environments:**

- **Initial approval:** Every sandbox configuration used in production is approved by the Information Security Officer (Security-Verantwortliche*r) or the AI Officer (KI-Verantwortliche*r) before first use. The approval names the sandbox type, technical identifier, approved tools and models, approved mount list, responsible person, and expiry date.
- **Re-validation:** An approval is valid for at most twelve months. Before expiry the sandbox configuration is re-assessed against the currently valid version of this guideline and against `CL_Agentische-KI-Sandbox`.
- **Unscheduled re-validation:** A new approval is also required when tool versions, model list, mount list, provider selection, or network policy change materially.
- **Withdrawal:** The Security-Verantwortliche*r or the AI Officer (KI-Verantwortliche*r) may withdraw an approval at any time, in particular after security incidents, after compromise of the tool chain, or after repeated violation of this guideline.
- **Pilot operation:** Sandbox configurations still being tuned may operate under documented pilot status, with a restricted user group and limited data classification. They count as regular only after transition to formal approval.

**Reference to standards and external frameworks:**

- ISO/IEC 27001:2022 Annex A: A.5.23 (cloud service security), A.8.25 (secure development life cycle), A.8.28 (secure coding), A.8.31 (separation of development, test, and production environments).
- NIST AI Risk Management Framework (AI RMF 1.0), in particular the functions GOVERN, MAP, MEASURE, and MANAGE.
- OWASP Top 10 for Large Language Model Applications, in particular LLM02 (Insecure Output Handling), LLM05 (Improper Output Handling), and LLM09 (Overreliance).
- Regulation (EU) 2024/1689 (EU AI Act), where the specific use falls within its scope.
- Regulation (EU) 2022/2554 (DORA) and Directive (EU) 2022/2555 (NIS2), where the project, customer, or supply chain falls within their scope.
- BSI C3A and BSI C5 as assessment frameworks for cloud autonomy, digital sovereignty, and cloud compliance assurance where cloud services are materially used or provided.
- G7 "Software Bill of Materials for AI – Minimum Elements" (2026) as a target architecture for AI supply-chain transparency; not legally binding, but compatible with the CRA, the EU AI Act, and NIS2.

Related document: [Secure Development Sandbox Guideline](mitgeltende-dokumente/Leitlinie_Sichere-Entwicklungs-Sandbox.md); related checklist: CL_Agentische-KI-Sandbox (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 12).

## Threat Modeling

A threat model is created for security-relevant applications. It is maintained together with architecture and specification documents.

**Minimum contents of the threat model:**

- Asset inventory with CIA matrix: confidentiality, integrity, availability, each rated high, medium, low, or not applicable. The CIA rating controls protection requirements and prioritizes STRIDE analysis. Assets with high confidentiality or integrity need at least one defense-in-depth measure.
- STRIDE analysis as the binding base method. Trust boundaries, data flows, and external interfaces are considered per STRIDE category.
- References to relevant CAPEC attack patterns for the highest-risk trust boundaries, misuse cases, and externally reachable flows. The goal is explicit consideration of realistic attacker techniques, not full coverage.
- Mitigation list per identified threat, with reference to implementing architecture decisions (S-ADR), code controls, or organizational measures.
- Residual-risk view with risk owner and next review date.

**Update obligation:** The threat model is updated when authentication, authorization, privilege boundaries, deployment topology, externally reachable endpoints, sensitive data flows, or third-party integrations change materially. Important STRIDE and CAPEC findings are fed into S-ADRs, security checklists, and task lists. CAPEC references are recorded directly in the threat model so that the mapping is auditable in one place.

Related checklist: CL_Bedrohungsmodellierung (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 04).

## Supply-Chain Transparency and Build Integrity

Secure development includes transparency about what is delivered and how it was produced.

**Binding rules:**

- Software Bill of Materials (SBOM): For each release or distributable artefact, a machine-readable SBOM is created per release. Established formats are CycloneDX and SPDX. The SBOM is stored as a release asset or in the security documentation.
- AI components in the SBOM: If an artefact embeds an AI model or AI service, this AI dependency is recorded in the SBOM or the supply-chain evidence document (provider, model or service identifier, version or endpoint, link to the provider's model card or AI-SBOM); CycloneDX represents this through ML-BOM components. No own model or training-data SBOM is generated for externally sourced models; the provider transparency is authoritative (see section "AI-Assisted Code Generation").
- Vulnerability Exploitability eXchange (VEX): If a delivered or assessed component contains a known vulnerability, a VEX statement is maintained. It clearly states whether the product is affected, not affected, mitigated, or under investigation. CSAF-VEX is the reference format.
- Build integrity (SLSA): CI/CD-created or published artefacts target SLSA v1.2 controls for build provenance and build integrity. Script-controlled and automated builds as well as provenance evidence are targeted where the toolset permits. Publicly consumed artefacts should aim for at least SLSA Build L2 in the medium term.
- OpenSSF Scorecard: Before adopting a relevant external dependency or before release of a public OSS repository, the Scorecard value or an equivalent source for repository security posture is reviewed.
- Automated dependency management: Renovatebot or Dependabot are preferred. They automatically open pull requests for outdated or vulnerable dependencies. Central SBOM/CVE tracking, for example OWASP Dependency Track, is preferred over regular manual audits where the hosting infrastructure allows it.
- Static dependency documents complement automated tooling for release decisions, exceptions, and risk acceptance. They do not replace it.
- SBOM, VEX, provenance, and Scorecard findings feed into release approval and evaluation of suppliers and third-party dependencies.
- **Protection against malware in build artefacts (ISO/IEC 27002:2022 A.8.7):** Build artefacts and third-party packages entering build runners are scanned for malware before release. Findings block the release. Exceptions are recorded with reason and expiry date. Build runners are hardened, short-lived environments where possible.

Related checklist: CL_Lieferkette-Build-Integritaet (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 05).

## Software Testing and Acceptance

During development, software security, stability, and functionality are checked through software and usability tests. Tests are performed and documented according to the test management guideline. Before production deployment, a vulnerability scan may be initiated.

Acceptance or production-preparation tests run on a test system (staging system). It has the same operating-system and software-stack state as production. Deployment to production occurs only after all tests succeed. The developed software is also checked through version control.

Included third-party software is checked for patch and change management. Compatibility checks are performed when needed to identify possible impact early.

Before new technologies are introduced, they are assessed for security risks and vulnerabilities, for example through CVE and CVSS checks in vulnerability databases.

The four-eyes rule applies to tests. It is implemented with Git mechanisms. A change is released to the next system only after a second person has reviewed and tested it. This applies to the development-to-test and test-to-production stages. If there are objections, the change goes back to development.

Code acceptance and handover to the customer are documented in an acceptance record that also lists any rework requests. Test cases are documented according to Organisation test management.

Related documents: test management guideline. Related checklist: CL_Code-Review-Sicherheit for pull-request reviews (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 08).

### Protection During Audit Tests and Security Tests

Audit, penetration, and vulnerability tests are performed so that production operation is not impaired (ISO/IEC 27002:2022 control A.8.34). The following applies:

- Active tests such as vulnerability scans, penetration tests, load tests, and forensic exercises preferably run on dedicated test or stage systems. Tests against production systems require explicit approval.
- Audit and test accounts are created as read-only accounts with minimum rights and are disabled or deleted after the test. Write access is possible only when explicitly required by the audit assignment.
- Audit and test windows are agreed in writing in advance, including time window, scope, tools, contact persons, and escalation path.
- Test tools and results, including scan results, logs, and exports, are handled confidentially, stored in an orderly way, and deleted or archived according to retention periods. Access is limited to the audit team and responsible Organisation units.
- Customers, auditors, and affected operations teams are named and included in a short final report. Findings are fed into continual improvement.

Related documents: test management guideline, access control guideline.

## Outsourced Development Projects

Some development projects are outsourced due to capacity constraints. External developers working for Organisation are regularly checked for quality. Before production deployment, experienced internal Organisation developers perform a code review. The software goes through the same system and acceptance test as internal projects. Supplier management rules also apply to suppliers.

Related documents: service-provider and supplier-relationship guideline.

## Separation of Development, Test, and Operations Environments

For self-developed software, Organisation strictly separates development, test, and production systems. When ready-made solutions without custom development are used, at least test and production systems are strictly separated.

Systems are built and restored through versioned infrastructure or configuration artefacts in separate repositories for test/stage and production. Project source code, operations configuration, and server configuration are managed with logical separation. The separation is implemented in as many projects as possible.

Test or stage and production systems each use a dedicated branch in the Git repository. Initial changes happen in the development system. When stable enough, they are merged into the test or stage branch. If tests succeed and no severe defects occur, they are merged into production after an RfC.

The final step occurs only after announcement and approval through a request for change in the planned maintenance window.

The systems are implemented as virtual machines. Where needed, for example cluster systems, they are placed on separate VMware hosts. Test and stage systems are in a designated virtualization area. Production systems are usually also in a designated area. Especially important or critical systems are placed in the high-availability area.

Development takes place on developer devices because development computers and servers are separated. Developers transfer changes through Git push, merge, or pull into the matching development branch. This prevents compilers, editors, and other development tools from reaching test, stage, or production systems. Deviations from separation must be justified, risk-assessed, and recorded in security or operations documentation.

Developers and testers use separate test accounts that are distinct from everyday accounts and identifiable by name. This reduces error risk.

Development and test systems do not contain production data, to prevent unintended changes to real data. Exceptions are possible only after agreement and approval by the customer and are documented.

Related checklist: CL_Sichere-Entwicklungsumgebung (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 10).

## Vulnerability Disclosure and Handling

Organisation operates a documented process for receiving, assessing, and fixing reported vulnerabilities, both internally and externally. The process follows coordinated vulnerability disclosure (CVD).

**Binding rules:**

- External reporting channels: Every published application has a clearly reachable reporting path. Established options are `security.txt` according to RFC 9116 for web applications, a security mailbox, or an issue template in the version-control system. The reporting path is linked in the relevant IT operations concepts.
- Triage and classification: Incoming reports are recorded, reproduced, rated with CVSS v4.0, and prioritized by severity: critical, high, medium, low. Score and CVSS vector are documented in the ticket. If only CVSS v3.1 data is available, the transition basis is recorded. Classification follows CWE categories and the related threat model.
- Remediation deadlines: Deadlines follow severity. Actively exploited vulnerabilities in production systems have priority. Planned and unplanned updates follow change management.
- Notifications to authorities and users: For applications in scope of the EU Cyber Resilience Act, stepped reporting duties apply. Actively exploited vulnerabilities and severe security incidents are reported within 24 hours as an early warning. The main notification follows within 72 hours. The final report follows no later than 14 days after providing a corrective measure for actively exploited vulnerabilities, or within one month for severe security incidents. Security updates are provided to users within legal deadlines.
- Communication: Security updates communicate affected versions, impact, mitigations, and VEX status.
- Feedback: Findings feed into threat models, S-ADRs, code reviews, training, and affected security documents. Improvements are structural and not only local.

Related checklist: CL_Schwachstellenoffenlegung (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 06).

## EU Cyber Resilience Act (CRA)

Each development project is assessed to determine whether the software is a "product with digital elements" under Regulation (EU) 2024/2847 (Cyber Resilience Act, CRA) and is placed on the Union market. This applies commercially, under license, or free of charge for economic purposes. Open-source software can also be affected when provided for economic purposes.

**Binding rules for CRA-applicable products:**

- SBOM per release.
- Documented process for vulnerability disclosure and handling, including 24-hour early warning, 72-hour main notification, and final report.
- Provision of security updates within legal deadlines and over the supported lifecycle.
- Documented conformity assessment: self-assessment for most products; third-party assessment for critical or important products according to Annex III/IV.
- Marking, technical documentation, and declaration of conformity according to CRA requirements before market provision.

Even outside the formal CRA scope, Organisation aligns its practice with CRA principles: secure by design, secure by default, continuous vulnerability management, lifecycle transparency, and SBOM availability. The CRA applicability decision is recorded in the project's security documentation, for example in the supply-chain evidence document or as an S-ADR.

**Timeline:** The regulation has been in force since 2024-12-10. Rules on conformity-assessment bodies apply from 2026-06-11. Reporting obligations under Article 14 apply from 2026-09-11. The remaining obligations apply from 2027-12-11.

Related checklist: CL_CRA-Anwendbarkeit (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 07).

## Data Protection Impact Assessment (GDPR Art. 35)

For new developments and material extensions of applications that process personal data under the General Data Protection Regulation (GDPR, Regulation (EU) 2016/679), a threshold analysis for a data protection impact assessment (DPIA) is mandatory. If the threshold analysis shows a likely high risk to the rights and freedoms of natural persons, a full DPIA under GDPR Art. 35 is created.

**Binding rules:**

- **Threshold analysis at project start:** Every project processing personal data starts with documented threshold analysis. It checks the cases named in GDPR Art. 35(3), the positive list of the competent data protection supervisory authority of the project or seat jurisdiction, and where relevant the EDPB DPIA list.
- **DPIA obligation in case of high risk:** If the threshold analysis shows a likely high risk, a DPIA according to GDPR Art. 35(7) is created with at least: systematic description of processing, assessment of necessity and proportionality, assessment of risks for data subjects, and planned mitigation measures.
- **Involvement of the Data Protection Officer:** The Organisation Data Protection Officer is consulted in every DPIA according to GDPR Art. 35(2). The opinion becomes part of the DPIA documentation.
- **Consultation of supervisory authority:** If a high residual risk remains after planned measures, the competent supervisory authority is consulted before processing begins, according to GDPR Art. 36.
- **Record of processing activities (GDPR Art. 30):** Every processing activity involving personal data is kept in the central Organisation record of processing activities. DPIA results are linked to the entry.
- **Processing on behalf (GDPR Art. 28):** If external processors are involved, for example cloud services or external development partners, a valid data-processing agreement exists and meets GDPR Art. 28(3).
- **Data protection by design and by default (GDPR Art. 25):** Privacy by design and privacy by default are mandatory in architecture and default configuration. The eight architecture principles are extended by data-protection measures: data minimization, purpose limitation, storage limitation, pseudonymization where possible, transparent information for data subjects, and simple exercise of data-subject rights.
- **Maintenance:** The DPIA is reviewed and updated where necessary after material processing changes, for example new data categories, new recipients, new third-country transfer, new technology, or new purpose. Regular maintenance takes place at least annually.

The link between DPIA and threat modeling is established in the project's security documentation. The DPIA considers risks from the perspective of data subjects. The threat model considers risks from the perspective of Organisation and its protection objectives. Both views are kept consistent.

Related documents: data-protection guideline. Related checklist: CL_Datenschutz-Folgenabschaetzung (see `Checklistensammelband_Sichere-Entwicklung.md`, chapter 11).

## KPIs for Security and Test Coverage

Organisation programming projects are continuously checked with unit and integration tests. As soon as a project is checked into the Git repository, unit tests check the internal API. Integration tests check the documented public interfaces, for example REST API, CLI, or comparable external API, and the documented critical UI flows. This ensures that a code change does not break API, interface, or UI functionality. Deployment starts only after successful tests.

The project in scope has traceable tests to secure application functionality. Test results also serve as KPIs and are stored as build, test, or quality reports in the repository or in the defined evidence location.

**Binding minimum thresholds:** For every project in scope, measurable target values apply. They are collected per release and stored in the security or quality management system.

- Unit-test code coverage: at least 80% line coverage or better per project.
- Branch coverage for modules with security-critical logic: at least 85% or better. This includes authentication, authorisation, input validation, cryptography, session management, payment paths, and comparable protection functions.
- AI-generated code: at least 80% line coverage or better and at least 80% branch coverage or better per affected module. If mutation testing is technically useful and available in the project tooling, a mutation score of at least 70% or better also applies.
- Integration-test coverage of public interfaces: at least 80% or better of the documented public interfaces, REST APIs, CLI commands, and comparable external interfaces. Every new or changed public interface is covered by at least one positive and one negative integration test.
- Integration-test coverage of critical UI flows: at least 80% or better of the documented critical UI flows, for example login, role change, data change, approval, export, security-relevant settings, and error cases.
- Mean Time to Remediate (MTTR) for CVE-based vulnerabilities, measured from internal confirmation: critical (CVSS 9.0 to 10.0) no later than 7 days; high (CVSS 7.0 to 8.9) no later than 30 days; medium (CVSS 4.0 to 6.9) no later than 90 days; low (CVSS 0.1 to 3.9) in the next regular maintenance window.
- ASVS verification coverage: web, API, and HTTP-based services prove at least ASVS Level 1 fully; authenticated or personal-data flows prove at least Level 2.
- SBOM coverage per release: 100% of delivered artefacts have a machine-readable SBOM (CycloneDX or SPDX).
- Share of automated dependency updates: at least 80% or better of dependencies are monitored automatically; open critical findings block release.
- Share of signed commits on `main`/`master`: 100%.
- Security-relevant pipeline failures such as SAST, secret, SBOM, or license checks: no open critical or high findings at merge into the main branch.

Deviations are recorded with reason and expiry date in the security or risk register.

Related documents: test management guideline. Related checklists: CL_Lieferkette-Build-Integritaet, CL_Code-Review-Sicherheit, CL_KI-Codeerzeugung, CL_Schwachstellenoffenlegung (see `Checklistensammelband_Sichere-Entwicklung.md`, chapters 05, 08, 09, and 06).

## References

**Local and related references:**

- Checklist compendium for the Secure Development guideline: `Checklistensammelband_Sichere-Entwicklung.md`, chapters 01 to 12 (related to this guideline)
- Related documents in `docs/secure-development/mitgeltende-dokumente/`
- Project-specific lists of approved tools, repositories, test reports, and approvals are linked locally in the respective project when needed.

**Norms and standards:**

- [ISO/IEC 27001:2022](https://www.iso.org/standard/27001): Information security, cybersecurity and privacy protection - Information security management systems.
- [ISO/IEC 27002:2022](https://www.iso.org/standard/75652.html): Information security controls.
- [NIST SP 800-218 Secure Software Development Framework](https://csrc.nist.gov/publications/detail/sp/800-218/final).
- [NIST SP 800-207 Zero Trust Architecture](https://csrc.nist.gov/publications/detail/sp/800-207/final).
- [RFC 9116 security.txt](https://www.rfc-editor.org/rfc/rfc9116): File format for vulnerability disclosure.

**OWASP resources:**

- [OWASP Application Security Verification Standard (ASVS)](https://owasp.org/www-project-application-security-verification-standard/).
- [OWASP Top 10](https://owasp.org/Top10/).
- [OWASP Software Assurance Maturity Model (SAMM)](https://owaspsamm.org/).
- [OWASP Cheat Sheet Series](https://cheatsheetseries.owasp.org/).
- [OWASP Proactive Controls](https://owasp.org/www-project-proactive-controls/).
- [OWASP Dependency Track](https://dependencytrack.org/).

**Vulnerability and attack-pattern catalogs:**

- [CWE Top 25 Most Dangerous Software Weaknesses](https://cwe.mitre.org/top25/).
- [CAPEC Common Attack Pattern Enumeration and Classification](https://capec.mitre.org/).

**Supply chain and build integrity:**

- [SLSA Supply-chain Levels for Software Artifacts](https://slsa.dev/).
- [CycloneDX SBOM Standard](https://cyclonedx.org/).
- [SPDX SBOM Standard](https://spdx.dev/).
- [OASIS CSAF with VEX profile](https://oasis-open.github.io/csaf-documentation/).
- [OpenSSF Scorecard](https://scorecard.dev/).

**Memory-safe languages:**

- [CISA: The Case for Memory Safe Roadmaps](https://www.cisa.gov/resources-tools/resources/case-memory-safe-roadmaps), December 2023.

**EU law:**

- [Regulation (EU) 2024/2847 Cyber Resilience Act](https://eur-lex.europa.eu/eli/reg/2024/2847/oj).

## Abbreviations

| Abbreviation | Meaning |
|---|---|
| ACME | Automated Certificate Management Environment |
| ADR | Architecture Decision Record |
| AEAD | Authenticated Encryption with Associated Data |
| AG | Working group |
| API | Application Programming Interface |
| ASVS | Application Security Verification Standard (OWASP) |
| BCM | Business Continuity Management |
| CAPEC | Common Attack Pattern Enumeration and Classification |
| CIA | Confidentiality, Integrity, Availability |
| CISA | Cybersecurity and Infrastructure Security Agency (US) |
| Security Lead | Chief Information Security Officer |
| CLI | Command Line Interface |
| CRA | Cyber Resilience Act (Regulation (EU) 2024/2847) |
| CSAF | Common Security Advisory Framework |
| CSPRNG | Cryptographically Secure Pseudo-Random Number Generator |
| CVD | Coordinated Vulnerability Disclosure |
| CVE | Common Vulnerabilities and Exposures |
| CVSS | Common Vulnerability Scoring System |
| CWE | Common Weakness Enumeration |
| Dokumentationsablage | Document Management System |
| DPIA | Data Protection Impact Assessment |
| DR | Disaster Recovery |
| ECDSA | Elliptic Curve Digital Signature Algorithm |
| FPE | Format-Preserving Encryption |
| GCM | Galois/Counter Mode |
| GDPR | General Data Protection Regulation (Regulation (EU) 2016/679) |
| GEANT | Public key infrastructure reference |
| Organisation | Gesellschaft fuer wissenschaftliche Datenverarbeitung mbH Example City |
| HPC | High Performance Computing |
| HSM | Hardware Security Module |
| IDE | Integrated Development Environment |
| IDM | Identity Management |
| IKT | Information and communication technology |
| Security-Verantwortliche*r | Information Security Officer |
| ISMS | Information Security Management System |
| ISO | International Organization for Standardization |
| KI-Verantwortliche*r | AI Officer |
| KMS | Key Management Service |
| KPI | Key Performance Indicator |
| LLM | Large Language Model |
| MAC | Message Authentication Code |
| MSL | Memory Safe Languages |
| MTTR | Mean Time to Remediate |
| MVC | Model-View-Controller |
| MVVM | Model-View-View-Model |
| NIST | National Institute of Standards and Technology (US) |
| OOP | Object-Oriented Programming |
| OSS | Open Source Software |
| OWASP | Open Worldwide Application Security Project |
| PKI | Public Key Infrastructure |
| PQC | Post-Quantum Cryptography |
| Nachweisprozess | evidence and improvement process |
| REST | Representational State Transfer |
| RfC | Request for Change |
| RPO | Recovery Point Objective |
| RTO | Recovery Time Objective |
| S-ADR | Security Architecture Decision Record |
| SAMM | Software Assurance Maturity Model (OWASP) |
| SAST | Static Application Security Testing |
| SBOM | Software Bill of Materials |
| SDLC | Software Development Life Cycle |
| SLSA | Supply-chain Levels for Software Artifacts |
| SoA | Statement of Applicability |
| SP | Special Publication (NIST) |
| SPDX | Software Package Data Exchange |
| SSDF | Secure Software Development Framework (NIST SP 800-218) |
| SSH | Secure Shell |
| SSO | Single Sign-On |
| STRIDE | Spoofing, Tampering, Repudiation, Information Disclosure, Denial of Service, Elevation of Privilege |
| TCS | Trusted Certificate Service (GEANT) |
| TLS | Transport Layer Security |
| TPM | Trusted Platform Module |
| UI | User Interface |
| VCS | Version Control System |
| VEX | Vulnerability Exploitability eXchange |
| VM | Virtual Machine |

## Version History

The version history documents material changes to this guideline according to ISO/IEC 27001:2022 clause 7.5. Minor editorial changes are not listed individually.

| Version | Date | 2026-06-17 |
|---|---|---|---|
| 1.0.0 | 2025-05-09 | Security-Verantwortliche*r | Initial version of the secure software development guideline |
| 1.1.0 | 2025-05-14 | Security-Verantwortliche*r | Added KPI section with measurable minimum thresholds |
| 1.2.0 | 2026-04-27 | Security-Verantwortliche*r | Added secure software architecture (A.8.27), cryptographic minimum requirements, AI-assisted code generation, threat modeling, supply-chain transparency, CRA reference, and related checklists |
| 2.0.0 | 2026-04-30 | Security-Verantwortliche*r | Added document metadata and version history; added ISMS positioning with ISO 27001 clauses; added business continuity of the development environment (A.5.30); added DPIA section (GDPR Art. 35); added data masking (A.8.11), malware scanning in the supply chain (A.8.7), and protection during audit testing (A.8.34); added CL_Datenschutz-Folgenabschaetzung |
| 2.0.1 | 2026-05-02 | Security-Verantwortliche*r | Replaced references to former individual checklist storage with checklist compendium `RL-SE-001-CL`, chapters 01 to 11; added SoA interface to the optional ISO 27001 evidence management |
| 2.1.0 | 2026-05-07 | Security-Verantwortliche*r | Added section "Agentic AI in Sandbox Environments"; regulated prohibition of direct agent execution on developer laptops, mount boundaries, OpenCode/Codex use, and GitHub Spec Kit governance presets; added CL_Agentische-KI-Sandbox; added English version |
| 2.2.0 | 2026-05-14 | Security-Verantwortliche*r | Extended section "Agentic AI in Sandbox Environments" with four permitted sandbox types (container, microVM, classic VM, dedicated workstation host), a formal approval and lifecycle process (initial approval, annual re-validation, unscheduled re-validation, withdrawal, pilot operation), and references to ISO/IEC 27001:2022 Annex A, NIST AI RMF, and OWASP LLM Top 10; added network restriction and reproducible tool pinning as binding rules; synchronized with CL_12 v1.1 |
| 2.3.0 | 2026-05-19 | Security-Verantwortliche*r | Section "Agentic AI in Sandbox Environments": initial approval and withdrawal of sandbox environments may be issued not only by the Security-Verantwortliche*r but also by the AI Officer (KI-Verantwortliche*r); added KI-Verantwortliche*r to the list of abbreviations; synchronized with CL_12 v1.2 |
| 2.4.0 | 2026-05-19 | Security-Verantwortliche*r | Added AI supply-chain transparency following the G7 guideline "Software Bill of Materials for AI – Minimum Elements" (2026): extended section "AI-Assisted Code Generation" with a scope delimitation (AI use versus AI provision) and AI-SBOM supplier transparency; added AI components in the SBOM to the supply-chain transparency section; added the G7 guideline as a framework; synchronized with CL_05, CL_09, and CL_12 |
| 2.5.0 | 2026-05-22 | Security-Verantwortliche*r | Refined section "Agentic AI in Sandbox Environments": feature implementations with agentic AI use GitHub Spec Kit with the SDD flow and the six governance presets unless a justified exception is documented; synchronized with CL_09 and CL_12 |
| 2.6.0 | 2026-06-12 | Security-Verantwortliche*r | Refined section "Agentic AI in Sandbox Environments": governance presets must be installed and documented with evidence or covered by a justified exception; extended CL_12 with preset list, versions, priorities, project policy, and cache exclusion as audit areas |
| 2.7.0 | 2026-06-14 | Security-Verantwortliche*r | Refined section "Agentic AI in Sandbox Environments": Spec Kit governance presets are checked at least quarterly and on relevant changes for currency, effective resolution, and coverage by RL/CL review points; synchronized AGENTS.md and CL_09/CL_12 |
| 2.8.0 | 2026-06-14 | Security-Verantwortliche*r | Refined section "Agentic AI in Sandbox Environments": complete Spec Kit Markdown artefacts with current presets and an evidence matrix may replace separate manual checklist completion; synchronized CL_09 and CL_12 |
| 2.8.1 | 2026-06-15 | Security-Verantwortliche*r | Switched references to the checklist compendium from the document number `RL-SE-001-CL` to the file name `Checklistensammelband_Sichere-Entwicklung.md`; synchronized README.md and AGENTS.md |
| 2.9.0 | 2026-06-15 | Security-Verantwortliche*r | Added audit-ready RL/CL coverage for the latest Spec Kit governance preset review points: regulatory screening, BSI C3A/C5, A11Y/CLI review, cross-platform parity, secure-coding profiles, and agent-guidance parity; synchronized checklist compendium version 1.9.0 |
| 2.10.0 | 2026-06-17 | Security-Verantwortliche*r | Raised and clarified test-coverage KPIs: unit-test line coverage at least 80%, security-critical modules at least 85% branch coverage, AI code at least 80% line and branch coverage, integration-test coverage of public interfaces and critical UI flows at least 80%; synchronized CL_08, CL_09, and checklist compendium |
| 2.10.1 | 2026-06-26 | Security-Verantwortliche*r | Clarified MSL examples in principles and programming sections with Go, Rust, and Swift where the target platform fits; added language-specific architecture notes for Go, Rust, and Swift |
| 2.10.2 | 2026-06-26 | Security-Verantwortliche*r | Added related guideline for secure development sandboxes; positioned sandbox reference profile for MSL-based level-2 projects, AI agents, Spec Kit, and training; synchronized CL_12 and compendium |

**Approval of the current version:**

- Subject-matter creation and maintenance: Organisation Information Security Officer (Security-Verantwortliche*r)
- Consultation: Organisation Data Protection Officer, heads of working groups D, N, I, E, and C
- Approval: Organisation Management Board
- Storage of approval documentation: central document management system (Dokumentationsablage)

**Distribution and publication:** This guideline is provided to all staff of the affected working groups through the Dokumentationsablage and intranet. External development partners receive the guideline as an annex to the contract documents.
