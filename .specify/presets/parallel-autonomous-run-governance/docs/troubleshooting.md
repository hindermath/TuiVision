# Fehlersuche / Troubleshooting

[Handbuch / Manual](README.md) | [Kompatibilitaet / Compatibility](compatibility.md)

## Deutsch

### Preset 7 fehlt oder ist deaktiviert

Symptom:

```text
autonomous-run-governance >= 0.2.2 fehlt oder ist deaktiviert
```

Pruefe im betroffenen Worker-Repository:

```bash
specify preset info autonomous-run-governance
```

Installiere beziehungsweise aktiviere Preset 7 mit Prioritaet `70` und
wiederhole `Validate`. Kein Worker darf vorher starten.

### Manifest-Hash stimmt nicht

Das akzeptierte Manifest wurde nach dem letzten Checkpoint veraendert. Nicht
blind fortsetzen. Aenderung reviewen, neue Kampagnenentscheidung dokumentieren
und State kontrolliert migrieren oder eine neue Kampagne anlegen.

### Repository ist nicht sauber

Unbekannte Aenderungen werden nicht in Worker-Worktrees uebernommen. Besitzer
und Zweck klaeren. Fremde Aenderungen erhalten; keinen Reset oder Checkout
erzwingen.

### Runner-Profil fehlt

Pruefe Kampagnen-Fallback und Worker-Override. Profile bleiben lokal und
muessen `agentFamily`, Executable und Argument-Array enthalten. Secrets nicht
in das Manifest kopieren.

### Worker-Prozess ist nicht mehr sichtbar

Ein Running-Marker ohne belastbaren aktiven Prozess oder Resultat ist
`Interrupted`, nicht erfolgreich. Status lesen, Git und Worker-Result
abgleichen und explizit Resume verwenden.

### Pipeline-Nachfolger startet nicht

Pruefe:

- direkte `dependsOn`-Kante,
- genau einen Producer-Handoff,
- Pfad und SHA-256,
- Status des Vorgaengers,
- bei `baseWorkerId` dasselbe Repository und validierten exakten Head.

### Parallelitaet bleibt unter drei

`maxConcurrency` ist eine Obergrenze. Abhaengigkeiten, fehlende Handoffs,
laufende sichere Grenzpunkte oder Ressourcen koennen die reale Parallelitaet
reduzieren.

### Konsolidierung meldet `NeedsRevalidation`

Head, Basis, Review, Check, Mergeability oder Provider-State weicht vom letzten
Checkpoint ab. Aktuellen Provider-Preflight erzeugen und Ursache klaeren.
Nicht mit Admin-Bypass als technischem Ersatz fortfahren.

### Post-Merge-Aktion schlaegt fehl

Checkpoint erhalten. Nur die fehlgeschlagene idempotente Aktion nach
Ursachenbehebung ueber Resume wiederholen. Bereits erfolgreiche Merges und
Aktionen nicht erneut ausfuehren.

### Zweites Resume tut nichts

Das ist korrekt, wenn alle betroffenen Operationen bereits terminal belegt
sind. Resume ist idempotent und erzeugt keine Doppelarbeit.

## English

### Preset 7 is missing or disabled

Inspect `autonomous-run-governance` in the affected worker repository. Install
or enable it at priority `70`, then repeat `Validate`. No worker starts first.

### Manifest hash differs

The accepted manifest changed after the checkpoint. Do not continue blindly.
Review the change and either migrate state through an explicit campaign
decision or create a new campaign.

### Repository is dirty

Do not absorb unknown changes into worker worktrees. Identify ownership and
preserve unrelated changes without reset or forced checkout.

### Runner profile is missing

Check campaign fallback and worker override. Local profiles require
`agentFamily`, executable, and argument array. Never copy secrets into the
manifest.

### Worker process disappeared

A running marker without a trustworthy live process or result is
`Interrupted`, never success. Read status, reconcile Git and worker result, and
use explicit resume.

### Pipeline descendant does not start

Check the direct dependency, exactly one producer handoff, path and SHA-256,
predecessor state, and for `baseWorkerId` the same repository plus validated
exact head.

### Observed concurrency is below three

`maxConcurrency` is an upper bound. Dependencies, handoffs, safe boundaries,
or available resources can reduce observed concurrency.

### Consolidation reports `NeedsRevalidation`

Head, base, review, check, mergeability, or provider state drifted. Produce a
current provider preflight and resolve the cause. An admin bypass is not
technical evidence.

### Post-merge action fails

Preserve the checkpoint. After fixing the cause, resume only the failed
idempotent action. Do not repeat successful merges or actions.

### Second resume does nothing

That is correct when affected operations already have terminal evidence.
Resume is idempotent and creates no duplicate work.
