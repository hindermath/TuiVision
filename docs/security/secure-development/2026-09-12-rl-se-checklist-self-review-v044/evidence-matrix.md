# Evidence-Matrix / Evidence Matrix

## Aktuelle Assurance-Revalidierung / Current Assurance Revalidation

**DE:** Am 2026-09-08 wurde die repository-lokale Secure-Development-Baseline
auf Version 3.2.0 synchronisiert und der technische Evidence-Vertrag des
13. Presets erneut geprüft. Baseline, Delta, Closure und Image Impact sind
Ready; der strengste technische Gesamtstatus ist Ready. Das ist keine
fachliche Neubewertung der 157 Kontrollpunkte. Die 157 Kontrollen behalten ihre fachlichen Dispositionen: 13 AlreadySatisfied, 65 Applicable, 38 N/A, 36 Open und 5 FollowUp.
Pilotfreigabe, Projektabnahme und allgemeine Freigabe bleiben Open.

**EN:** On 2026-09-08, the repository-local secure-development baseline was
synchronized to version 3.2.0 and the thirteenth preset's technical evidence
contract was revalidated. Baseline, delta, closure, and image impact are
Ready; the strictest technical overall status is Ready. This is not a new
domain assessment of the 157 controls. The 157 controls retain their domain dispositions: 13 AlreadySatisfied, 65 Applicable, 38 N/A, 36 Open, and 5 FollowUp. Pilot
authorization, project acceptance, and general release remain Open.

**DE:** Aktuelle Scope-Entscheidung fuer diesen Feldtest: C5, CRA und formale
Produktkonformitaet sind fuer das nichtkommerzielle Ausbildungs- und
Beispielprojekt `N/A`. GitHub, CI, NuGet und Artefakthosting sind Entwicklungs-
und Lieferinfrastruktur, keine Produkt-Cloud-Runtime. Dies aendert die
historischen 157 Kontrolldispositionen nicht. Technische Jahreswiedervorlage:
2027-09-08; regulatorische Scope-Pruefung: 2026-12-31.

**EN:** Current scope decision for this field test: C5, CRA, and formal product
conformity are `N/A` for the non-commercial training and example project.
GitHub, CI, NuGet, and artifact hosting are development and delivery
infrastructure, not product cloud runtime. This does not rewrite the historical
157 control dispositions. Technical annual review: 2027-09-08; regulatory
scope review: 2026-12-31.

**DE:** Aktuelle maschinenlesbare Gates sind baseline.json,
deltas/2026-09-08-assurance-revalidation.json, closure.json und
image-impact.json. Die ersetzten blockierten Migrations-Gates bleiben unter
archive/2026-09-07-assurance-migration/ erhalten. Umfang und ausführbare
Nachweise stehen in assurance-revalidation.md und assurance-validation.json.

**EN:** Current machine-readable gates are baseline.json,
deltas/2026-09-08-assurance-revalidation.json, closure.json, and
image-impact.json. The superseded blocked migration gates remain under
archive/2026-09-07-assurance-migration/. See assurance-revalidation.md and
assurance-validation.json for scope and executable proof.

## Zweck und Grenze / Purpose and Boundary

Dieser am 2026-09-12 revalidierte Index erschließt die aktuelle, **nicht
zertifizierende** 157-Kontrollen-Selbstprüfung für
`autonomous-run-governance` v0.4.4. Die fünf vom Rollout betroffenen Quellen
wurden fachlich erneut geprüft und bytegenau gebunden. Die übrigen
Kontrolldispositionen, Human-only-Grenzen und Rollen bleiben unverändert; sie
werden nicht in Assurance-Gate-Ergebnisse oder neue Freigaben umgedeutet.

*This index, revalidated on 2026-09-12, exposes the current, non-certifying
157-control self-review for `autonomous-run-governance` v0.4.4. The five
rollout-affected sources were reviewed again and bound by exact bytes. All
other control dispositions, human-only boundaries, and roles remain unchanged;
they are not converted into assurance gate results or new approvals.*

## Quellenbindung / Source Binding

- Kontext / context: `2026-09-12-rl-se-checklist-self-review-v044`.
- Kanonische Bewertungsquelle / canonical assessment source: [rl-se-self-review.json](rl-se-self-review.json).
- SHA-256 der revalidierten kanonischen JSON-Datei / SHA-256 of the revalidated canonical JSON file:
  `671825d25085c4aa0cdab9f6e3f385d36ccc302e53d8cce6a2075adf874f9098`.
- Ausgangsstand der Revalidierung / Revalidation starting HEAD: `0182b5f47cd2ad0c3a887510d9ec4a85c905375b`.
- Umfang / scope: 157 eindeutige Kontroll-IDs / unique control IDs.
- Der Hash oben bindet die kanonische JSON-Datei; deren `reviewSnapshot` bindet
  die fünf neu geprüften Quellen einzeln. / The hash above binds the canonical
  JSON file; its `reviewSnapshot` binds each of the five revalidated sources.
- Owner, Reviewer, Begründung, Risiken, Fristen, Trigger und Grenzen stehen
  vollständig in der verlinkten Quelle; sie werden nicht neu festgelegt.
  / Owner, reviewer, rationale, risks, deadlines, triggers and boundaries remain
  in the linked source; none is newly assigned by this index.

### Quellenstatus, nicht Assurance-Ergebnis / Source States, Not Assurance Outcomes

- `Applicable`: 65
- `N/A`: 38
- `FollowUp`: 5
- `AlreadySatisfied`: 13
- `Open`: 36

## Kontrollzuordnung / Control Mapping

Jede Zeile benennt die unveränderte Quellbewertung, vorhandene Evidence-IDs und
den nullbasierten JSON-Array-Pfad in der oben verlinkten Datei. Fehlende
Referenzen bleiben ausdrücklich sichtbar. Verweis-IDs sind keine Prüfung ihrer
Aktualität oder Wirksamkeit. / Each row retains its source state and existing
evidence IDs, with a zero-based JSON array path. Missing references stay visible;
an evidence ID does not prove freshness or effectiveness.

| Kontroll-ID / Control ID | Quellenstatus / Source state | Evidence-ID | JSON-Pfad / JSON path |
|---|---|---|---|
| CL-01-01 | Applicable | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[0]` |
| CL-01-02 | N/A | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[1]` |
| CL-01-03 | Applicable | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[2]` |
| CL-01-04 | N/A | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[3]` |
| CL-01-05 | FollowUp | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[4]` |
| CL-01-06 | N/A | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[5]` |
| CL-01-07 | Applicable | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[6]` |
| CL-01-08 | Applicable | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[7]` |
| CL-01-09 | Applicable | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[8]` |
| CL-01-10 | Applicable | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[9]` |
| CL-01-11 | Applicable | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[10]` |
| CL-01-12 | Applicable | EVD-021, EVD-036, EVD-042, EVD-038 | `controls[11]` |
| CL-02-01 | Applicable | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[12]` |
| CL-02-02 | Applicable | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[13]` |
| CL-02-03 | Applicable | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[14]` |
| CL-02-04 | Applicable | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[15]` |
| CL-02-05 | Applicable | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[16]` |
| CL-02-06 | Applicable | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[17]` |
| CL-02-07 | Applicable | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[18]` |
| CL-02-08 | Applicable | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[19]` |
| CL-02-09 | Applicable | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[20]` |
| CL-02-10 | Applicable | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[21]` |
| CL-02-11 | Applicable | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[22]` |
| CL-02-12 | N/A | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[23]` |
| CL-02-13 | N/A | EVD-022, EVD-036, EVD-035, EVD-043, EVD-034 | `controls[24]` |
| CL-03-01 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[25]` |
| CL-03-02 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[26]` |
| CL-03-03 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[27]` |
| CL-03-04 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[28]` |
| CL-03-05 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[29]` |
| CL-03-06 | AlreadySatisfied | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[30]` |
| CL-03-07 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[31]` |
| CL-03-08 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[32]` |
| CL-03-09 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[33]` |
| CL-03-10 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[34]` |
| CL-03-11 | AlreadySatisfied | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[35]` |
| CL-03-12 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[36]` |
| CL-03-13 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[37]` |
| CL-03-14 | N/A | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[38]` |
| CL-03-15 | Applicable | EVD-023, EVD-036, EVD-040, EVD-044 | `controls[39]` |
| CL-04-01 | Applicable | EVD-024, EVD-036, EVD-043, EVD-041 | `controls[40]` |
| CL-04-02 | Applicable | EVD-024, EVD-036, EVD-043, EVD-041 | `controls[41]` |
| CL-04-03 | Applicable | EVD-024, EVD-036, EVD-043, EVD-041 | `controls[42]` |
| CL-04-04 | Applicable | EVD-024, EVD-036, EVD-043, EVD-041 | `controls[43]` |
| CL-04-05 | Applicable | EVD-024, EVD-036, EVD-043, EVD-041 | `controls[44]` |
| CL-04-06 | Applicable | EVD-024, EVD-036, EVD-043, EVD-041 | `controls[45]` |
| CL-04-07 | Applicable | EVD-024, EVD-036, EVD-043, EVD-041 | `controls[46]` |
| CL-04-08 | Applicable | EVD-024, EVD-036, EVD-043, EVD-041 | `controls[47]` |
| CL-04-09 | Applicable | EVD-024, EVD-036, EVD-043, EVD-041 | `controls[48]` |
| CL-04-10 | Applicable | EVD-024, EVD-036, EVD-043, EVD-041 | `controls[49]` |
| CL-05-01 | Applicable | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[50]` |
| CL-05-02 | Applicable | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[51]` |
| CL-05-03 | N/A | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[52]` |
| CL-05-04 | FollowUp | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[53]` |
| CL-05-05 | FollowUp | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[54]` |
| CL-05-06 | AlreadySatisfied | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[55]` |
| CL-05-07 | FollowUp | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[56]` |
| CL-05-08 | Applicable | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[57]` |
| CL-05-09 | Applicable | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[58]` |
| CL-05-10 | Applicable | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[59]` |
| CL-05-11 | Applicable | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[60]` |
| CL-05-12 | AlreadySatisfied | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[61]` |
| CL-05-13 | N/A | EVD-025, EVD-036, EVD-042, EVD-037 | `controls[62]` |
| CL-06-01 | Applicable | EVD-026, EVD-036, EVD-016, EVD-038, EVD-044 | `controls[63]` |
| CL-06-02 | FollowUp | EVD-026, EVD-036, EVD-016, EVD-038, EVD-044 | `controls[64]` |
| CL-06-03 | Applicable | EVD-026, EVD-036, EVD-016, EVD-038, EVD-044 | `controls[65]` |
| CL-06-04 | Applicable | EVD-026, EVD-036, EVD-016, EVD-038, EVD-044 | `controls[66]` |
| CL-06-05 | Open | EVD-026, EVD-036, EVD-016, EVD-038, EVD-044 | `controls[67]` |
| CL-06-06 | Applicable | EVD-026, EVD-036, EVD-016, EVD-038, EVD-044 | `controls[68]` |
| CL-06-07 | Open | EVD-026, EVD-036, EVD-016, EVD-038, EVD-044 | `controls[69]` |
| CL-06-08 | Applicable | EVD-026, EVD-036, EVD-016, EVD-038, EVD-044 | `controls[70]` |
| CL-06-09 | Applicable | EVD-026, EVD-036, EVD-016, EVD-038, EVD-044 | `controls[71]` |
| CL-06-10 | Applicable | EVD-026, EVD-036, EVD-016, EVD-038, EVD-044 | `controls[72]` |
| CL-06-11 | Open | EVD-026, EVD-036, EVD-016, EVD-038, EVD-044 | `controls[73]` |
| CL-07-01 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[74]` |
| CL-07-02 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[75]` |
| CL-07-03 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[76]` |
| CL-07-04 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[77]` |
| CL-07-05 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[78]` |
| CL-07-06 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[79]` |
| CL-07-07 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[80]` |
| CL-07-08 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[81]` |
| CL-07-09 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[82]` |
| CL-07-10 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[83]` |
| CL-07-11 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[84]` |
| CL-07-12 | Open | EVD-027, EVD-036, EVD-038, EVD-044 | `controls[85]` |
| CL-08-01 | Applicable | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[86]` |
| CL-08-02 | Applicable | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[87]` |
| CL-08-03 | N/A | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[88]` |
| CL-08-04 | N/A | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[89]` |
| CL-08-05 | N/A | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[90]` |
| CL-08-06 | N/A | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[91]` |
| CL-08-07 | Applicable | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[92]` |
| CL-08-08 | Applicable | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[93]` |
| CL-08-09 | Applicable | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[94]` |
| CL-08-10 | Applicable | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[95]` |
| CL-08-11 | Applicable | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[96]` |
| CL-08-12 | Applicable | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[97]` |
| CL-08-13 | AlreadySatisfied | EVD-028, EVD-036, EVD-040, EVD-044 | `controls[98]` |
| CL-09-01 | Applicable | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[99]` |
| CL-09-02 | Applicable | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[100]` |
| CL-09-03 | Open | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[101]` |
| CL-09-04 | Applicable | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[102]` |
| CL-09-05 | Applicable | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[103]` |
| CL-09-06 | Open | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[104]` |
| CL-09-07 | Applicable | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[105]` |
| CL-09-08 | AlreadySatisfied | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[106]` |
| CL-09-09 | Applicable | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[107]` |
| CL-09-10 | Applicable | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[108]` |
| CL-09-11 | Open | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[109]` |
| CL-09-12 | Open | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[110]` |
| CL-09-13 | Applicable | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[111]` |
| CL-09-14 | Applicable | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[112]` |
| CL-09-15 | N/A | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[113]` |
| CL-09-16 | N/A | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[114]` |
| CL-09-17 | AlreadySatisfied | EVD-029, EVD-036, EVD-015, EVD-044 | `controls[115]` |
| CL-10-01 | Open | EVD-030, EVD-036, EVD-039 | `controls[116]` |
| CL-10-02 | Open | EVD-030, EVD-036, EVD-039 | `controls[117]` |
| CL-10-03 | Open | EVD-030, EVD-036, EVD-039 | `controls[118]` |
| CL-10-04 | AlreadySatisfied | EVD-030, EVD-036, EVD-039 | `controls[119]` |
| CL-10-05 | Open | EVD-030, EVD-036, EVD-039 | `controls[120]` |
| CL-10-06 | AlreadySatisfied | EVD-030, EVD-036, EVD-039 | `controls[121]` |
| CL-10-07 | Open | EVD-030, EVD-036, EVD-039 | `controls[122]` |
| CL-10-08 | Applicable | EVD-030, EVD-036, EVD-039 | `controls[123]` |
| CL-10-09 | Applicable | EVD-030, EVD-036, EVD-039 | `controls[124]` |
| CL-10-10 | N/A | EVD-030, EVD-036, EVD-039 | `controls[125]` |
| CL-10-11 | AlreadySatisfied | EVD-030, EVD-036, EVD-039 | `controls[126]` |
| CL-10-12 | Open | EVD-030, EVD-036, EVD-039 | `controls[127]` |
| CL-10-13 | Open | EVD-030, EVD-036, EVD-039 | `controls[128]` |
| CL-10-14 | Open | EVD-030, EVD-036, EVD-039 | `controls[129]` |
| CL-10-15 | Open | EVD-030, EVD-036, EVD-039 | `controls[130]` |
| CL-10-16 | Open | EVD-030, EVD-036, EVD-039 | `controls[131]` |
| CL-10-17 | Applicable | EVD-030, EVD-036, EVD-039 | `controls[132]` |
| CL-11-01 | N/A | EVD-031, EVD-036, EVD-038 | `controls[133]` |
| CL-11-02 | N/A | EVD-031, EVD-036, EVD-038 | `controls[134]` |
| CL-11-03 | N/A | EVD-031, EVD-036, EVD-038 | `controls[135]` |
| CL-11-04 | N/A | EVD-031, EVD-036, EVD-038 | `controls[136]` |
| CL-11-05 | N/A | EVD-031, EVD-036, EVD-038 | `controls[137]` |
| CL-11-06 | N/A | EVD-031, EVD-036, EVD-038 | `controls[138]` |
| CL-11-07 | N/A | EVD-031, EVD-036, EVD-038 | `controls[139]` |
| CL-11-08 | N/A | EVD-031, EVD-036, EVD-038 | `controls[140]` |
| CL-11-09 | N/A | EVD-031, EVD-036, EVD-038 | `controls[141]` |
| CL-11-10 | N/A | EVD-031, EVD-036, EVD-038 | `controls[142]` |
| CL-11-11 | N/A | EVD-031, EVD-036, EVD-038 | `controls[143]` |
| CL-11-12 | N/A | EVD-031, EVD-036, EVD-038 | `controls[144]` |
| CL-12-01 | Open | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[145]` |
| CL-12-02 | Open | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[146]` |
| CL-12-03 | AlreadySatisfied | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[147]` |
| CL-12-04 | Applicable | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[148]` |
| CL-12-05 | Open | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[149]` |
| CL-12-06 | AlreadySatisfied | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[150]` |
| CL-12-07 | Open | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[151]` |
| CL-12-08 | Applicable | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[152]` |
| CL-12-09 | Open | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[153]` |
| CL-12-10 | Open | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[154]` |
| CL-12-11 | Open | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[155]` |
| CL-12-12 | AlreadySatisfied | EVD-032, EVD-036, EVD-044, EVD-039 | `controls[156]` |

## Historische Assurance-Lücke (2026-09-07) / Historical Assurance Gap (2026-09-07)

Die erste Fassung der Matrix ergänzte nur die zuvor fehlende Navigation. Zu
diesem Zeitpunkt fehlten `baseline.json`, mindestens ein `deltas/*.json`,
`closure.json` und `image-impact.json`; der lesende Status war deshalb
`Blocked`. Die technische Revalidierung vom 2026-09-08 hat diese Vertragslücke
geschlossen. Die vorhandene Selbstprüfung wird weiterhin nicht nachträglich zu
einem fachlichen Assurance-Review erklärt. `pilotAuthorization`,
`projectAcceptance` und `generalRelease` bleiben Open.

Ein weiterer fachlicher Review bleibt separat zu beauftragen. Keine
vollständige C5-Prüfung und keine Aussage zu C5-Konformität, Testatreife oder
Zertifizierung. `CL-02-13` bleibt die unveränderte projektbezogene Bewertung
in der Quelle.

*The first matrix version added only the missing navigation. At that time, the
four gate JSON contracts were absent and read-only status was Blocked. The
technical revalidation on 2026-09-08 closed that contract gap. Existing
self-review is still not reclassified as a substantive Assurance review;
pilot, project and general-release decisions remain Open. A further domain
review requires separate authority. No complete C5 assessment or conformity,
attestation-readiness or certification claim is made.*

## Leserpfad und Dokumentationsauswirkung / Reader Path and Documentation Impact

- [control-assessment](control-assessment.md)
- [governance-observations](governance-observations.md)
- [human-boundaries](human-boundaries.md)
- [preset-assessment](preset-assessment.md)
- [README](README.md)
- [validation-evidence](validation-evidence.md)

Documentation Impact: `UpdateRequired`. Zielgruppen / audiences: Maintainer,
Security-Reviewer und KI-Agenten. Leserpfad / reader path: Integrationsnachweis →
Matrix → kanonisches JSON → bestehende Detaildokumente → separat genehmigte
nächste Aktion. Owner: Repository-Maintainer (Thorsten Hindermann). Dokumentklasse:
Evidence-Navigation, keine normative Richtlinie. DE/EN im selben Dokument;
textorientierte Tabelle, keine farbabhängige Aussage. Repository-lokal, kein
Home-Sync. Re-Evaluation bei Quellhash-, Scope-, Baseline- oder Vertragsänderung.
Prüfung: 157 eindeutige IDs, quellentreue Werte, Dateihash und read-only Status
unter Bash und PowerShell. / Repository-local evidence navigation; same-document
bilingual text, no colour-only meaning, no Home sync. Reevaluate when the source
hash, scope, baseline or contract changes. Validation checks all 157 IDs, literal
source values, file hash and read-only status under both shells.


Technische Ausnahme vom reinen Index: [zwei dokumentierte
Neubindungsschritte](../../../maintenance/assurance-technical-rebinding.md);
alle fachlichen Bewertungen bleiben unverändert. / Technical exception: two
documented rebinding steps; every domain assessment remains unchanged.
