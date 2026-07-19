# Handbuch Parallel Autonomous Run Governance / Parallel Autonomous Run Governance Manual

[Zurueck zur Preset-README / Back to the preset README](../README.md)

## Deutsch

Dieses Handbuch erklaert Planung, Ausfuehrung und Konsolidierung einer
parallelen autonomen Spec-Kit-Kampagne. Preset 8 baut auf den kontrollierten
Einzellaeufen von Preset 7 auf.

### Empfohlener Lernpfad

1. [Erste Kampagne](getting-started.md)
2. [Topologien und Scheduling](topologies-and-scheduling.md)
3. [Manifest und Runner](manifest-and-runners.md)
4. [Lebenszyklus und Operationen](lifecycle-and-operations.md)
5. [Konsolidierung und Recovery](consolidation-and-recovery.md)
6. [Post-Merge-Closeout](post-merge-closeout.md)
7. [Fehlersuche](troubleshooting.md)
8. [Kompatibilitaet](compatibility.md)
9. [Feldnachweise](field-evidence/README.md)

### Schnelleinstieg nach Rolle

| Rolle | Zuerst lesen | Danach |
|---|---|---|
| Lernende | Erste Kampagne | Topologien und Scheduling |
| Kampagnenplaner | Topologien und Scheduling | Manifest und Runner |
| Operator | Lebenszyklus und Operationen | Fehlersuche |
| Reviewer | Konsolidierung und Recovery | Post-Merge-Closeout |
| Maintainer | Kompatibilitaet | Feldnachweise |

### Was Preset 8 besitzt

- Kampagnenmanifest und dessen Hash,
- Scheduling, Locks und nicht versionierte Runtime-Verzeichnisse,
- Branch-/Worktree-Isolation,
- Runner-Auswahl und nicht geheime Statusmetadaten,
- Worker-Ergebnisvertraege und Handoffs,
- Kampagnenzustand, Events und Versuchszahlen,
- deklarierte Konsolidierung und Post-Merge-Aktionen.

### Was Preset 7 besitzt

- den Lifecycle jedes einzelnen Features,
- dessen Tasks, Checklisten und Run-State,
- Worker-spezifische Acceptance-Gates und Evidence,
- den sicheren Stop-Grenzpunkt des Einzellaufs,
- dessen erlaubten Delivery-Abschluss.

Preset 8 speichert fuer den Worker-State nur Pfad und Hash. Es kopiert oder
ersetzt den autoritativen State von Preset 7 nicht.

## English

This manual explains the planning, execution, and consolidation of a parallel
autonomous Spec Kit campaign. Preset 8 builds on the controlled single runs
provided by Preset 7.

### Recommended learning path

1. [First campaign](getting-started.md)
2. [Topologies and scheduling](topologies-and-scheduling.md)
3. [Manifest and runners](manifest-and-runners.md)
4. [Lifecycle and operations](lifecycle-and-operations.md)
5. [Consolidation and recovery](consolidation-and-recovery.md)
6. [Post-merge closeout](post-merge-closeout.md)
7. [Troubleshooting](troubleshooting.md)
8. [Compatibility](compatibility.md)
9. [Field evidence](field-evidence/README.md)

### Ownership boundary

Preset 8 owns campaign manifest and hash, scheduling, runtime paths, worktree
isolation, runner selection, worker result contracts, handoffs, campaign
events, consolidation, and post-merge actions.

Preset 7 owns each feature lifecycle, tasks, checklists, autonomous run state,
worker acceptance evidence, safe boundary, and authorized delivery closeout.
Preset 8 stores only the path and hash of worker state; it does not copy or
replace the authoritative Preset 7 state.
