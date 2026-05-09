# DocFX A11y Smoke Tests

## Zweck

Dieser Ordner enthaelt einen kleinen, getrennten Playwright-Pruefpfad fuer die
erzeugte DocFX-Dokumentation. Der Fokus liegt auf schnellen Smoke-Tests fuer
WCAG-2.2-AA-nahe Probleme, textorientierte Lesbarkeit und repraesentative
Seiten der HTML-Doku.

This folder contains a small, separate Playwright test path for the generated
DocFX documentation. Its focus is fast smoke tests for WCAG 2.2 AA-oriented
issues, text-first readability, and representative HTML documentation pages.

## Nutzung

1. Abhaengigkeiten installieren:
   `npm install`
2. Chromium fuer Playwright installieren:
   `npx playwright install chromium`
3. Kombinierten Build- und Smoke-Test starten:
   `npm run test:docfx`

1. Install dependencies:
   `npm install`
2. Install Chromium for Playwright:
   `npx playwright install chromium`
3. Run the combined build and smoke test:
   `npm run test:docfx`

## Installationsvoraussetzungen

- Empfohlen ist Node `24.x` LTS.
- `npm` wird fuer die lokalen Testabhaengigkeiten benoetigt.
- `lynx` sollte auf dem Entwicklungssystem installiert sein, damit es neben
  Playwright einen rein textbasierten Gegencheck fuer Braille-nahe Lesbarkeit
  gibt.

- Node `24.x` LTS is recommended.
- `npm` is required for the local test dependencies.
- `lynx` should be installed on the development system so there is a purely
  text-based cross-check next to Playwright for Braille-oriented readability.

## Hinweise

- Empfohlen ist Node `24.x` LTS. Node `20.x`, `22.x` und `24.x` sind die
  offiziell freigegebenen Playwright-Linien.
- Die Tests starten automatisch einen lokalen HTTP-Server ueber der
  generierten `_site/`-Ausgabe.
- Jeder erfolgreiche `docfx`-Neubau soll direkt von `npm run test:docfx`
  gefolgt werden.
- Fuer einen separaten `lynx`-Check kann in einem Terminal
  `npm run serve:docfx` laufen, waehrend `lynx` in einem zweiten Terminal auf
  `http://127.0.0.1:8123/` zugreift.
- `lynx` ist als zweiter, rein textueller Gegencheck vorgesehen. Hilfsskripte
  dafuer stehen in `package.json`.
- Der GitHub-Pages-Workflow fuehrt denselben Smoke-Test nach dem DocFX-Build
  aus und veroeffentlicht nur das erzeugte Pages-Artefakt, nicht `_site/` oder
  generierte `api/*.yml`-Dateien.

- Node `24.x` LTS is recommended. Node `20.x`, `22.x`, and `24.x` are the
  officially supported Playwright lines.
- The tests automatically start a local HTTP server for the generated `_site/`
  output.
- Every successful `docfx` regeneration should be followed directly by
  `npm run test:docfx`.
- For a separate `lynx` check, `npm run serve:docfx` can run in one terminal
  while `lynx` accesses `http://127.0.0.1:8123/` from a second terminal.
- `lynx` is intended as a second, purely text-based cross-check. Helper
  scripts for it are available in `package.json`.
- The GitHub Pages workflow runs the same smoke test after the DocFX build and
  publishes only the generated Pages artifact, not `_site/` or generated
  `api/*.yml` files.
