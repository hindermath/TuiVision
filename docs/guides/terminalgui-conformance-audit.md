# Terminal.GUI-Konformitätsaudit / Terminal.GUI Conformance Audit

## Ziel / Goal

Deutsch: Feature 029 prüft die bestehende TuiVision-Basis gegen eine dritte
moderne Implementierungsmeinung. Borland Turbo Vision 2.0.3 bleibt die
historische Primärquelle. Free Vision bleibt die gepinnte zweite Meinung.
Terminal.GUI v1.9.0 ergänzt eine moderne C#-Sicht, schreibt aber weder API,
Klassenaufbau noch Quelltextform vor.

English: Feature 029 checks the existing TuiVision foundation against a third
modern implementation opinion. Borland Turbo Vision 2.0.3 remains the primary
historical source. Free Vision remains the pinned second opinion. Terminal.GUI
v1.9.0 adds a modern C# view but does not prescribe APIs, class structure, or
source shape.

## Quellenbindung / Source Binding

| Item | Bound value |
|---|---|
| Repository | `https://github.com/tui-cs/Terminal.Gui.git` |
| Tag | `v1.9.0` |
| Annotated tag object | `4b812e44798f2c7567afec50ba9a9293b6beb6de` |
| Peeled commit | `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3` |
| License | MIT |
| Selected records | `TGSR001`-`TGSR025` |

Deutsch: Das Quellenmanifest speichert Pfad, Hash, Permalink und eine eigene
Kurzbeschreibung. Der externe Checkout liegt nur temporär außerhalb des
Repositories. TuiVision kopiert keinen Implementierungsblock und keine
Test-Fixture.

English: The source manifest stores path, hash, permalink, and an original
summary. The external checkout exists only temporarily outside the repository.
TuiVision copies no implementation block or test fixture.

## Bewertungsmodell / Assessment Model

| Decision | Meaning |
|---|---|
| `CorroboratesOriginal` | Die dritte Quelle bestätigt die historische Verantwortung. / The third source corroborates the historical responsibility. |
| `CorroboratesModernization` | Sie bestätigt eine moderne TuiVision-Umsetzung. / It corroborates a modern TuiVision implementation. |
| `AlternativeModernization` | Beide lösen dieselbe Verantwortung mit anderer öffentlicher Form. / Both solve the same responsibility with different public shapes. |
| `DivergesFromTuiVision` | Eine nachprüfbare Abweichung muss auf einen realen TuiVision-Gap untersucht werden. / A reproducible difference must be checked for a real TuiVision gap. |
| `NotApplicable` | Es gibt keine direkte belastbare Vergleichsfläche. / There is no direct reliable comparison surface. |

Deutsch: Eine andere Klassenhierarchie oder ein anderer Name ist kein Finding.
Ein `CandidateFinding` entsteht nur, wenn der aktuelle TuiVision-Vertrag, ein
realer Wave-Verbraucher, eine Sicherheits- oder A11Y-Grenze, eine
Plattformgrenze oder der echte Proof-Pfad reproduzierbar fehlt.

English: A different class hierarchy or name is not a finding. A
`CandidateFinding` is created only when the current TuiVision contract, a real
Wave consumer, a security or accessibility boundary, a platform boundary, or
the real proof path is reproducibly missing.

## Ergebnis / Result

| Metric | Count |
|---|---:|
| Existing contracts | 48 |
| Domains | 16 |
| Pinned source records | 25 |
| Wave-5 consumer groups | 6 |
| Wave-6 consumer groups | 7 |
| `CandidateFinding` | 0 |
| `ProductDecision` | 0 |
| New `C049+` contracts | 0 |

Deutsch: 21 Verträge sind mit zusätzlicher Evidence bereits ausreichend
belegt. 20 Verträge verwenden eine bewusste moderne Alternative. Sieben
Bereiche, insbesondere Hilfe, Ressourcen, Persistenz und das
Terminal-Emulationssubset, sind nicht direkt vergleichbar. Diese sieben werden
nicht künstlich als Parität oder Fehler bewertet.

English: Twenty-one contracts are already sufficiently proven with the
additional evidence. Twenty contracts use an intentional modern alternative.
Seven areas, especially help, resources, persistence, and the terminal
emulation subset, are not directly comparable. Those seven are not
artificially classified as parity or defects.

## Proof-Grenzen / Proof Boundaries

- Deutsch: Die TuiVision-Tests bleiben der Verhaltensnachweis; fremde UnitTests
  zeigen nur, welche Verantwortung Terminal.GUI selbst als prüfbar behandelt.
- English: TuiVision tests remain the behavior proof; foreign unit tests only
  show which responsibilities Terminal.GUI itself treats as testable.
- Deutsch: `FakeDriver` unterstützt die Bewertung deterministischer Zell-,
  Eingabe- und App-Loop-Proofs, ersetzt aber keinen physischen Terminaltest.
- English: `FakeDriver` informs deterministic cell, input, and app-loop proof
  assessment but does not replace a physical terminal test.
- Deutsch: `FILECOPY.PAS` und `TRASH.PAS` bleiben destruktive Produktpolitik
  für Wave 6 und werden nicht in einen Frameworkfehler umgedeutet.
- English: `FILECOPY.PAS` and `TRASH.PAS` remain destructive Wave-6 product
  policy and are not reclassified as a framework defect.

## Nächster Intake / Next Intake

Deutsch: Feature 030 prüft anschließend den gepinnten direkten
C++-Modernisierungszeugen `magiblot/tvision`. Es übernimmt alle 48
`TGO###`-Beobachtungen, erzeugt eigene `MB###`-Beobachtungen und dedupliziert
erst danach beide Mengen zu möglichen kanonischen `CF###`-Findings. Feature 029
erzeugt selbst kein Hardening- oder Closure-Lastenheft.

English: Feature 030 next checks the pinned direct C++ modernization witness
`magiblot/tvision`. It receives all 48 `TGO###` observations, creates its own
`MB###` observations, and only then deduplicates both sets into possible
canonical `CF###` findings. Feature 029 creates no hardening or closure intake.

Wave 5 and Wave 6 remain blocked.
