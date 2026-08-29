# validate-sandbox-applicability(1)

## Name

`validate-sandbox-applicability` - prüft TuiVision-Sandbox-Evidence read-only / validates TuiVision sandbox evidence read-only

## Synopsis

```text
validate-sandbox-applicability.sh --evidence FILE [--repo-root PATH] [--json]
```

## Beschreibung / Description

Der Befehl prüft die Struktur der kanonischen Assessment-JSON. Er erwartet
genau zwölf CL-12-Kontrollen, portable Mount-Rollen, vollständige
Execution-Entscheidungen und genau eine erlaubte Recommendation. Er verändert
keine Datei und erteilt keine technische, organisatorische oder
Providerfreigabe.

*The command validates the canonical assessment JSON structure. It expects
exactly twelve CL-12 controls, portable mount roles, complete execution
decisions, and one allowed recommendation. It changes no file and grants no
technical, organizational, or provider approval.*

## Optionen / Options

- `--evidence FILE`: Repository-relativer JSON-Pfad. / Repository-relative JSON path.
- `--repo-root PATH`: Root für sichere Pfadauflösung; Standard `.`. / Root for safe path resolution; default `.`.
- `--json`: Maschinenlesbares Einzeilen-Ergebnis. / Machine-readable one-line result.
- `-h`, `--help`: Kurzhilfe des Bash-Einstiegs. / Bash entry-point help.

## Exit Status

- `0`: Vertrag erfüllt. / Contract satisfied.
- `1`: Evidence fachlich-strukturell ungültig. / Evidence violates the structural contract.
- `2`: Aufruf-, Pfad-, Encoding- oder JSON-Fehler. / Invocation, path, encoding, or JSON error.
- `3`: Python 3 fehlt am Einstieg. / Python 3 is unavailable at the entry point.

## Proof-Grenze / Proof Boundary

Ein Pass beweist keine Image-Ausführung, Netzisolation, Datenklassifikation,
Providerfreigabe oder Human Approval. Diese Nachweise bleiben getrennt.

*A pass proves no image execution, network isolation, data classification,
provider approval, or human approval. Those proofs remain separate.*
