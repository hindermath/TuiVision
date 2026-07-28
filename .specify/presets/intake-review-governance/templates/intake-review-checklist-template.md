# Intake Review Checklist

## Single Intake

- [ ] Identity, audience, goal, scope, and non-goals are explicit.
- [ ] Assumed prior knowledge is explicit; Spec Kit experience is not silently
      assumed for first-time learner audiences.
- [ ] Technical and workflow terms are explained on first use at the declared
      readability level and in the project language order.
- [ ] Dependencies, status, decisions, and next actions are understandable as
      ordered text and do not rely only on colour, diagrams, or visual position.
- [ ] Requirements are atomic, testable, and free of conflicting modal terms.
- [ ] Acceptance criteria and validation evidence are measurable.
- [ ] Dependencies, ordering, authority, delivery, risks, and follow-ups are explicit.
- [ ] Security, privacy, accessibility, platform, and supply-chain applicability is decided.
- [ ] References and embedded Specify/Autonomous prompts match the normative text.
- [ ] No credentials, secrets, unnecessary personal data, or binary content is present.

## Series

- [ ] Schema-2.0 documentation language is explicit and independent of
      implementation language and locale.
- [ ] Naming profile, four portable roles, six collection paths, canonical
      index, bounded aliases, and computed inventory agree.
- [ ] Every active intake occurs once in the configured Series with a current
      normalized SHA-256; exactly one candidate is `Eligible`.
- [ ] IDs, order, dependency graph, handoffs, and future-scope boundaries are consistent.
- [ ] Schema 1.1 binds the repository-relative request path and normalized SHA-256.
- [ ] Every target occurs exactly once in `orderedTargetPaths`.
- [ ] Declared roots equal the graph's zero-indegree targets.
- [ ] Every non-root has an incoming edge; edges are unique, known, ordered, and acyclic.
- [ ] No unexplained gap, overlap, duplicate ownership, or terminology drift remains.
- [ ] Shared invariants are stated once and consumed consistently.

## Campaign

- [ ] Every `featureInput` exists and has one current target review.
- [ ] Every worker has an applicability row; duplicate intake use reuses semantic review.
- [ ] Manifest DAG, base refs, repositories, runner constraints, and intake ordering agree.
- [ ] Operator exceptions are separate, explicitly owned, dated, justified, and expiring.
