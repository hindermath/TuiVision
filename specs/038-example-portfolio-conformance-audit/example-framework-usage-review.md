# Framework-Nutzungsreview / Framework Usage Review

Kanonische Wahrheit: [example-portfolio-audit.json](example-portfolio-audit.json).

| Entscheidung / Decision | Anzahl | Ergebnis / Result |
|---|---:|---|
| UseExistingFramework | 10 | Vorhandene wiederverwendbare Verantwortung reicht aus. |
| IntentionalDeviation | 27 | Moderne, dokumentierte Beispielgrenze ohne Framework-Lücke. |
| SmallFrameworkFix | 0 | Kein reproduzierbarer kleiner Frameworkdefekt. |
| FollowUpHardening | 0 | Keine größere wiederverwendbare Härtung erforderlich. |

*Ten rows use existing framework responsibility directly; 27 preserve a
documented modern example boundary. No reproducible framework gap remains.*

Die Zeilen verwenden `TApplication`, `TDesktop`, `TWindow`,
`TStatusLine`, Ereignisse und die jeweils passenden Controls. Lokale Logik
bleibt auf deterministische Lernzustände, kontrollierte Pfade, kompakte
Layouts, Manifeste oder sichere Fallbacks begrenzt. Wiederverwendbares Verhalten
wird nicht in `examples/` neu implementiert. Der BHelp-Verzicht auf den
proprietären ungeprüften `.tch`-Decoder, kontrollierte Datei-/Ressourcenpfade,
deterministische Terminal-/Mausmodelle und kompakte Showcase-Zustände sind
akzeptierte Abweichungen, keine Findings.

*Rows compose existing TuiVision controls. Example-local logic is bounded to
deterministic learning state, controlled paths, compact layouts, manifests, and
safe fallbacks. The proprietary TCH omission, controlled file/resource paths,
and deterministic terminal/mouse models are accepted deviations rather than
framework findings.*
