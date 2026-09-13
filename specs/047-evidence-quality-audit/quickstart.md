# Quickstart

```bash
python3 specs/047-evidence-quality-audit/tools/render_evidence.py --check
python3 -m unittest specs/047-evidence-quality-audit/tests/test_evidence_quality.py
git diff --check
```

Die erste Anweisung rendert und vergleicht die kanonische Evidence. Der Test
prueft positive Invarianten und manipulierte negative Daten. Beide Befehle
veraendern weder Produktcode noch die geprueften Artefakte.

The first command renders and compares canonical evidence. The test covers
positive invariants and malformed negative data. Neither command changes product
code or audited artifacts.
