# Intake Series Checklist

- [ ] A schema-2.0 requirements configuration resolves all target paths through
      the declared portable roles and collection paths.
- [ ] Every target path is explicit, unique, repository-relative, and hashed.
- [ ] Visible order contains every target exactly once.
- [ ] Roots equal the zero-indegree target set.
- [ ] Every edge uses an accepted type and correct binding flag.
- [ ] `RequirementsGovernanceGate` is used only for a binding predecessor that
      must complete before the dependent intake becomes eligible.
- [ ] A non-idle series has exactly one current `Eligible` target; eligibility
      grants no delivery authority.
- [ ] An `Idle` series has zero targets, roots, dependencies, and eligible
      candidates.
- [ ] The graph is order-consistent and acyclic.
- [ ] Material ambiguity is recorded as `NeedsClarification`.
- [ ] Write authority is current and bounded.
- [ ] Read, status, and next remain read-only.
- [ ] No downstream command is started implicitly.
- [ ] Bash and PowerShell validators agree.
