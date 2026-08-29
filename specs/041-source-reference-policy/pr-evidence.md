# PR Evidence: Source Reference Policy

## Initial status / Anfangsstatus

Diese Evidence wurde vor dem ersten Policy-, Validator- oder Governance-
Flächenedit angelegt. Alle Implementierungs- und Validierungsgates beginnen als
`Not Assessed`. Autorität ist `LocalImplementation`; Remote-Aktionen sind nicht
autorisiert.

*This evidence was created before the first policy, validator, or governance
surface edit. Every implementation and validation gate begins as not assessed.
Authority is LocalImplementation; remote actions are not authorized.*

## Bindings / Bindungen

- Intake SHA-256: `a59e9588363e385c19d48c361df9ebb019323cf25865672dd77152292e8d2776`
- Review ID: `915a0930-273c-4c06-aa3d-be2286d4a0db`
- Magiblot commit: `57b6f56b38e0ee75240a80a10ee0e11470c24693`
- Magiblot tree: `96dd03873955689ff0a79f6c8107a8148fe1ebd6`
- COPYRIGHT SHA-256: `66220baeb9761b723fba913b74cf8257621a65c38cadb941fbb5bc181104b548`

## Gate ledger / Gate-Protokoll

| Gate | Status | Evidence |
|---|---|---|
| Canonical policy | Passed | Geschlossene JSON-Norm `tuivision-source-reference-v1`; 13 explizite Flächen |
| Bash/PowerShell positive parity | Passed | Beide Einstiege melden `Pass`, exakter Pin `57b6f56b38e...` |
| Negative fixtures | Passed | Je 7/7 kontrollierte Ein-Ursachen-Fixtures in Bash und PowerShell |
| Governance surfaces | Passed | Marker genau einmal in Constitution, Template, fünf Agent-Flächen, Pflichtenheft und vier Spec-Kit-Flächen plus lesbarer Norm |
| Prospective/no-copy boundary | Passed | Drei geschlossene Trigger; externer Checkout, No-Copy, Moving-Branch-Verbot und `MultipartNotRepositoryWideMIT` fail-closed |
| Homogeneity/agent parity/secrets | Passed | Bash und PowerShell 29/29; Agent-Parität 3/3; Secret-Scans ohne High-Fund |
| DocFX/Axe/Lynx | Passed | DocFX 0 Warnungen/Fehler; Playwright/Axe 2/2; Lynx liest Rollen, Pin, Dispositionen und Lizenzgrenze |
| Product and historical scope | Passed | Null Delta unter `src/`, `tests/`, `examples/`, `tv203s/`, Solution- und Paketflächen; abgeschlossene Feature-Evidence unverändert |

## Decision / Entscheidung

`SourceReferencePolicyEffective`.

Die Verfassung ist auf 1.18.0 fortgeschrieben. Die maschinenlesbare Norm, die
bilinguale Dokumentation und alle ausgelösten Governance-Flächen stimmen über
Rollen, Workflow, Pin, Dispositionen, Konfliktregeln, Provenienz und
Re-Evaluation überein. Bestehende Features werden nicht rückwirkend geöffnet.

*Constitution 1.18.0, the machine-readable contract, readable documentation,
and every triggered governance surface now agree. The policy is effective for
future work and does not reopen completed evidence.*
