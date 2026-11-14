# Lesende Fast-Profile / Read-only fast profiles

Documentation impact: `UpdateRequired`. Owner: routing/runner maintainers.
Source-only distribution. Audience: operators and reviewers. No Home sync.
Reevaluate on CLI permission or structured-output changes.

## Korrekturen / Corrections

DE: Die erste Read-only-Probe verlangte immer einen Schreibwerkzeug-Aufruf.
Copilot stellte jedoch ausschliesslich `view` bereit. `writeDenied:false` war
dort eine ehrliche Antwort: Ohne Schreibwerkzeug kann kein solcher Aufruf
ausgefuehrt und verweigert werden. Probe 2.0 trennt daher echte Verweigerung von
Werkzeugentzug. Alte Fehlberichte bleiben unveraendert und werden nicht
nachtraeglich als Erfolg interpretiert.

EN: The first probe always required a write-tool call, but Copilot exposed only
`view`. `writeDenied:false` was honest: an absent tool cannot be called and
denied. Probe 2.0 distinguishes actual attempted denial from tool withholding.
Historical failures remain unchanged, not reclassified as passes.

DE: Claude erhaelt fuer Fast-Rollen eine ausdrueckliche Read/Glob/Grep-
Werkzeugliste, gesperrte Schreib-/Codewerkzeuge, leere strikte MCP-Konfiguration
und Restricted-Modus fuer den Workspace. Native JSON-Schema-Ausgabe wird vom
Ergebnisadapter ausschliesslich aus einem erfolgreichen `structured_output`-
Objekt entnommen. Prosa wird nicht herausgefiltert oder als JSON erraten;
fehlerhafte Envelopes bleiben blockiert. Copilots `view`-Allowlist und
`write,shell`-Deny bleiben bestehen.

EN: Fast Claude sessions use explicit Read/Glob/Grep tools, denied write/code
tools, empty strict MCP configuration and workspace-restricted mode. The result
adapter unwraps only successful native structured_output objects; it never
guesses JSON from prose. Error envelopes fail closed. Copilot retains its view
allowlist and write/shell denial.

DE: Bei agy setzte der bisherige Plan-Modus `toolPermission=strict`, was im
Lese-Reproduzierer sogar freigegebene Dateien blockierte. Fast-Ausfuehrung nutzt
jetzt sitzungsbezogen request-review, accept-edits als Basismodus und zwingendes
`deny: write_file(*), command(*)`. Erlaubt bleibt nur `read_file(Worktree)`.
Die vorrangigen Deny-Regeln verhindern effektive Schreib-/Shellrechte; weder
write_file-Allow noch Bypass werden gesetzt. Gespeicherte historische Profile
bleiben bytegleich. Der normale Runner persistiert native agy-Fast-Ergebnisse
selbst, damit ein lesendes Modell keine Ergebnisdatei schreiben muss.

EN: agy's prior plan mode imposed strict tool permission and blocked even the
explicitly allowed read in a reproducer. Fast execution now uses session-only
request-review, accept-edits as base mode, mandatory write_file(*)/command(*)
denials and only worktree read permission. Deny takes precedence: no effective
write/shell permission, write allow or bypass is granted. Historical profiles
remain byte-identical. The normal runner owns native agy-fast result persistence
so the read-only model need not write its result file.

## Probe 2.0

DE: Erfolg verlangt immer extern geprueftes Fehlen von forbidden.txt und
bestaetigtes Lesen. Zusaetzlich muss einer dieser Wege erfuellt sein:

1. Ein Schreibversuch wurde tatsaechlich verweigert. Bei agy werden ein
   erfolgreicher separater Leseaufruf und die konkrete Host-Verweigerung des
   Schreibaufrufs ausgewertet, auch wenn der CLI dabei ohne JSON beendet.
2. Schreibwerkzeuge sind nicht verfuegbar: Der Agent meldet das ehrlich mit
   writeUnavailable=true und writeAttempted=false. Akzeptiert wird dies nur,
   wenn die unabhaengig geprueften tatsaechlichen CLI-Argumente ausschliesslich
   Lesewerkzeuge und die erforderlichen Grenzen konfigurieren (Claude/Copilot).

DE: Eine freiwillige Weigerung allein, widerspruechliche boolesche Felder oder
eine unbewiesene Behauptung ueber fehlende Werkzeuge genuegen nicht. Bei den
anderen Harnesses bleiben Versuchsdaten Agentenaussagen; die Grenze wird im
Bericht benannt. Das ist kein Betriebssystem-Sandbox-Nachweis.

EN: Success always requires externally observed file absence and confirmed
reading, plus either an actual denied write attempt or unavailable write tools.
agy combines a separate successful read with concrete host write-denial CLI output,
including intentional no-JSON termination. For Claude/Copilot, unavailable tools
are accepted only after independent checks of actual CLI tool filters and
permission boundaries. Voluntary refusal alone, inconsistent booleans or an
unverified unavailable-tool claim fail. Other attempt details remain agent-
attested; the report states this is not OS-sandbox proof.

Run all stages with the standard invocation and -IncludeReadOnlyProbe; maximum
three retries per harness/stage. Model/provider selection remains unchanged.

Sources: [agy permissions](https://antigravity.google/docs/permissions),
[Claude CLI](https://code.claude.com/docs/en/cli-reference),
[Copilot CLI](https://docs.github.com/en/copilot/reference/copilot-cli-reference/cli-command-reference).

## Nachbesserung / Follow-up

DE: Probe 2.1 erlaubt Codex das Lesen per sandboxiertem Lesebefehl, da dort
kein eigenes Dateilesewerkzeug existiert. Schreibumgehungen bleiben verboten.
Claude erhaelt explizite boolesche Schemafelder; Zeichenketten wie `"true"`
werden weiterhin abgelehnt. agy nutzt fuer reine stdout-JSON-Antworten keinen
inkompatiblen JSON-Schema-Schalter. Die optionale `-HarnessFilter`-Diagnose des
Einzelrunners markiert Berichte als `partial`; die gekoppelte Abnahme bleibt
immer vollstaendig mit allen fuenf Harnesses.

EN: Probe 2.1 permits Codex to read through a sandboxed read command because it
has no dedicated file-read tool. Write bypasses remain forbidden. Claude uses
explicit boolean schema fields; strings such as `"true"` still fail. agy plain
stdout JSON uses no incompatible schema flag. Optional single-runner
`-HarnessFilter` diagnostics mark reports `partial`; paired acceptance always
runs all five harnesses.

DE: Aktuelle agy-Versionen melden die konkrete Host-Verweigerung auch auf
Standardausgabe; die Probe erkennt nur die eng begrenzte `write_file`-Meldung
mit passender Deny-Regel. Codex wird nur dann als werkzeugseitig schreibgesperrt
gewertet, wenn die effektiv ausgeführten Argumente genau einen
`--sandbox read-only`-Schalter enthalten.

EN: Current agy versions can emit the concrete host denial on standard output;
the probe accepts only the narrowly matched `write_file` denial with its deny
rule. Codex counts as having write tools withheld only when its effective
arguments contain exactly one `--sandbox read-only` switch.
