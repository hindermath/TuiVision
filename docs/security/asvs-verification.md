# ASVS-Anwendbarkeit / ASVS Applicability

**Stand / Current as of**: 2026-07-11
**Status**: `N/A`

TuiVision liefert ein lokales Terminal-UI-Framework. Es enthält keine
Produkt-Webanwendung, HTTP-API, Authentifizierung, Autorisierung,
Session-Verwaltung oder Mandantengrenze. Deshalb wird kein ASVS-Level gewählt
und keine ASVS-Verification-Matrix behauptet.

*TuiVision ships a local terminal UI framework. It has no product web
application, HTTP API, authentication, authorization, session management, or
tenant boundary. Therefore no ASVS level or verification matrix is claimed.*

Relevante lokale Sicherheitsnachweise liegen in `security-checklist.md`,
`threat-model.md`, Source-/Test-Reviews und Package-Evidence.

**Neubewertung / Re-evaluation**: web, API, HTTP, authentication,
authorization, sessions, or remotely reachable service scope enters TuiVision.

**Restrisiko / Residual risk**: Low while these facts remain unchanged.
