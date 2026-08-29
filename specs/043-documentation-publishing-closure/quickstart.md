# Quickstart: Feature 043 prüfen

## Deutsch

1. Öffne `docs/guides/getting-started.md` und folge dem Leserpfad bis zum
   ersten Dialog.
2. Prüfe `docs/guides/example-learning-paths.md`: 38 Projekte müssen genau
   einmal mit sechs Lernfeldern vorkommen.
3. Prüfe `docs/documentation-closure.md`: 27 Abstimmungs-IDs müssen genau
   einmal als `Closed` oder `AcceptedBoundary` vorkommen.
4. Erzeuge die Dokumentation und führe direkt danach den A11Y-Test aus:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

5. Prüfe eine repräsentative Seite zusätzlich mit `lynx -dump` und stelle
   sicher, dass Zweck, Reihenfolge und nächster Schritt ohne Layout verständlich
   bleiben.

## English

1. Open `docs/guides/getting-started.md` and follow the reader path to the
   first dialog.
2. Check `docs/guides/example-learning-paths.md`: all 38 projects must occur
   exactly once with six learning fields.
3. Check `docs/documentation-closure.md`: all 27 reconciliation IDs must occur
   exactly once as `Closed` or `AcceptedBoundary`.
4. Build the documentation and run the accessibility test immediately
   afterwards using the commands above.
5. Review one representative page with `lynx -dump`; purpose, sequence, and
   next action must remain understandable without visual layout.

Generated `_site/` and `api/*.yml` files are validation output and must not be
added to Git.
