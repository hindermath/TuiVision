## Intake Review Governance

When `intake-review-governance` is installed and project or campaign policy
marks review as required, do not create a Spec Kit feature or schedule a worker
until a current review result matches every normalized intake hash. Only
`Ready` or human-approved `ReadyWithAcceptedRisks` passes. Treat Critical/High
findings, unanswered material questions, stale hashes, and missing worker
coverage as blocking. Review commands never modify target intakes. Use the
separate repair command only with explicit mutation authority, then re-review.
For Series mode require schema 1.1, a hash-bound repository-relative request,
complete target ordering, explicit roots, and an acyclic dependency graph.
Never infer a missing predecessor or silently accept request drift.
