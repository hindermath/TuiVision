## Projekttransparenz / Project transparency

DE: Statistik ist reproduzierbare Projekttransparenz, kein Qualitaets-,
Sicherheits-, Lernleistungs- oder KI-Zeitersparnisnachweis. Nutze den
`project-statistics-contract`. Pflege den explizit ausgewaehlten Kontext nach
Feature- oder Implementierungsabschluss; Status bleibt read-only. Plane
Aktualisierungen als sequenzielle gemeinsame Schreibaufgabe, nie parallel.
Ausfuehrung und Git-Lieferung brauchen eigene aktuelle Autoritaet.
Bestehende Berichte und Pilotberichte bleiben getrennt.

EN: Statistics provide reproducible project transparency, not proof of quality,
security, learner performance or AI time savings. Follow the resolved
`project-statistics-contract`. Update the explicitly selected context after
feature or implementation completion under current write authority; status is
read-only. Serialize shared report writes. Never infer execution or delivery
authority. Preserve the separation between existing reports and pilot reports.
