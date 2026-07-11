# Zero-Trust-Anwendbarkeit / Zero Trust Applicability

**Stand / Current as of**: 2026-07-11
**Status**: `N/A` as a product/service architecture

Die TuiVision-Runtime ist ein lokaler Prozess ohne Identity Provider,
Remotezugriff, Netzwerkdienst, Mandantenmodell, Policy Enforcement Point oder
Cloud-Deployment. GitHub/CI sind Entwicklungsinfrastruktur und keine
TuiVision-Produkt-Runtime.

*The TuiVision runtime is a local process without identity provider, remote
access, network service, tenant model, policy enforcement point, or cloud
deployment. GitHub/CI are development infrastructure, not product runtime.*

Lokale Trust Boundaries bleiben anwendbar und werden im Threat Model behandelt:
Terminalinput, Dateipfade, Serialisierung, Dependencies, Scripts und Agenten.

**Neubewertung / Re-evaluation**: distributed service, cloud operation, remote
identity, remote management, multi-device policy, or network control plane.

**Restrisiko / Residual risk**: Low for product architecture; external
agent/provider controls remain separately human-owned.
