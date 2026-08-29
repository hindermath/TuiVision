# Contract: Sandbox Applicability Acceptance

## Positive Contract

Eine gültige Lieferung enthält:

1. genau eine kanonische Assessment-JSON-Datei;
2. genau zwölf eindeutige CL-12-Entscheidungen;
3. mindestens die Rollen `TuiVisionCheckout`, `AgentStateVolume`,
   `BuildCacheVolume` und `AuditMetadataDirectory`;
4. Execution-Entscheidungen für Build, Test, Format, DocFX, A11Y,
   Dependency/SBOM, Secret-Scan und Agent-Parität;
5. genau eine erlaubte Recommendation mit nächster sicherer Aktion;
6. keine Secrets oder privaten absoluten Hostpfade.

*A valid delivery has one canonical assessment, exactly twelve CL-12 rows,
the required portable mount roles, all required execution decisions, one
allowed recommendation, and no secrets or private absolute host paths.*

## Negative Contract

Der Validator muss mindestens ablehnen:

- fehlende oder doppelte CL-12-ID;
- unbekannte Status- oder Recommendation-Werte;
- `Open` ohne Owner, Folgeaktion oder Trigger;
- `Open` zusammen mit `Fulfilled`;
- absoluten Unix-, Windows- oder Home-Pfad in einer Mount-Rolle;
- fehlende Required-Mount- oder Execution-Rolle;
- `PlatformVerified` ohne Plattform;
- `NotPermitted` mit ausführbarem Befehl;
- leere Evidence oder leeren Proof-Boundary;
- Starterwerte, Platzhalter oder secret-ähnliche Inhalte.

## Proof Boundary

Ein erfolgreicher Validatorlauf beweist ausschließlich Struktur und interne
Konsistenz der Evidence. Er beweist weder Image-Funktion, Netzisolation,
Providerfreigabe, Datenklassifikation noch menschliche Genehmigung.

*A passing validator proves only evidence structure and internal consistency.
It does not prove image function, network isolation, provider approval, data
classification, or human approval.*
