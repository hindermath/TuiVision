# v0.2.3: Kanonischer Adapterpfad / Canonical adapter path

DE: v0.2.2 führte den fail-closed Rückkanal für strukturierte Harness-Ausgabe
ein, speicherte aber den absoluten Quellpfad des Adapters im lokalen Profil.
v0.2.3 verwendet stattdessen den kanonischen repository-relativen Pfad unter
`.specify/presets/model-routing-governance/`. Root- und Preset-Resolver erzeugen
damit identische Profile. Der autonome Runner löst den Pfad in seinem Worktree
auf. Ergebnisvalidierung, atomisches Schreiben und Fehlerverhalten bleiben gleich.

EN: v0.2.2 added the fail-closed structured-output transport but stored the
adapter's absolute source path in the local profile. v0.2.3 uses the canonical
repository-relative installed-preset path. Root and preset resolvers therefore
produce identical profiles, and the autonomous runner resolves the path inside
its worktree. Result validation, atomic persistence and failure behavior stay
unchanged.

Codex and `agy` remain unchanged. Tests require the exact canonical path in all
twelve generated Claude, Copilot and OpenCode role profiles, plus complete
root/preset parity in consumers.

Documentation Impact **UpdateRequired**, owner Thorsten Hindermann. Canonical
sources are the profile generator, additive fixture and this record. Reader
path: README → this record → v0.2.2 transport design. German first, equivalent
English, CEFR B2 and applicable WCAG 2.2 AA. Re-evaluate when the installation
layout or autonomous-runner worktree contract changes.

This is preparation, not publication evidence. Publication requires the exact
tag, release page and independently verified archive.
[Structured preparation](release-0.2.3-preparation.json).
