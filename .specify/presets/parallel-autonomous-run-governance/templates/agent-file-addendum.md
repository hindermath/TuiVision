## Parallele autonome Lauf-Governance / Parallel Autonomous Run Governance

For schema-1.2 campaigns, enforce an active `intakeReview` gate before creating
worktrees. One semantic target review may cover byte-equivalent repeated worker
inputs, but every worker still needs an applicability row.

### Deutsch / German

- Eine Kampagne koordiniert bestehende autonome Einzellaeufe und ersetzt nicht
  deren Zustands-, Evidenz- oder Berechtigungsvertraege.
- Je Kampagne und Worker unveraenderliche UUIDs sowie getrennte Branches und
  Worktrees verwenden.
- Das deklarierte Parallelitaetslimit niemals ueberschreiten.
- Runner als Executable plus Argument-Array ohne Shell-Auswertung ausfuehren.
- Worker-spezifische Runner-Profile duerfen das Kampagnenprofil ueberschreiben.
  Modell und Reasoning nur anzeigen, wenn sie ausdruecklich deklariert sind.
- Bei normalen Worker-Fehlern unabhaengige Worker bis zur sicheren Grenze
  weiterlaufen lassen; abhaengige Nachfolger ohne Handoff blockieren.
- Alternative Loesungen brauchen vor der Konsolidierung eine menschliche
  Auswahl.
- Vor jedem Merge Providerzustand, exakten Head, Reviews und Check-Policy
  pruefen. Teilmerges checkpointen und bei Resume nicht wiederholen.
- Post-Merge-Aktionen duerfen nur aus dem geprueften Kampagnenmanifest stammen.
- Installation erteilt keine Ausfuehrungs-, Remote-, Merge-, Bypass-,
  Abbruch-, Secret- oder Provider-Administrationsberechtigung.

### Englisch / English

- Treat a campaign as coordination over existing autonomous single runs, not as
  a replacement for their state, evidence, or permission contracts.
- Use one immutable campaign ID and one immutable run ID per worker.
- Give every worker a separate branch and Git worktree.
- Never exceed the campaign concurrency limit.
- Execute runner profiles as executable plus argument array; never use shell
  evaluation for manifest content.
- Let unrelated workers reach a safe boundary after an ordinary worker failure.
  Block pipeline descendants whose required handoff is missing.
- Stop new scheduling after a campaign-integrity, security, permission, or
  evidence-integrity failure.
- Alternative solutions require explicit human selection before consolidation.
- `MergeAndSync` campaigns publish workers first, then cross the all-ready
  barrier before the first merge.
- Allow per-worker runner profiles with campaign fallback. Report model and
  reasoning only when explicitly declared.
- Before every merge, validate provider state, exact head, current reviews, and
  check policy. Checkpoint partial merges and do not repeat verified merges.
- Execute post-merge actions only when they are declared in the reviewed
  campaign manifest.
- Installation grants no autonomous execution, remote write, merge, bypass,
  cancellation, secret, or provider-administration authority.
