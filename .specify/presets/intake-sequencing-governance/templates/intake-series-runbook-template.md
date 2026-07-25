# Intake Sequencing Runbook

## Create

Name every existing target, proposed order, root, edge, type, and lifecycle
state. Publish only after explicit approval and both validators pass.

## Read

Summarize identity, target count, roots, dependencies, statuses, blockers, and
eligible targets. Do not modify files.

## Update

Require current authority, archive the accepted predecessor, prepare all files,
validate, and publish atomically with `supersedes` evidence.

## Delete

Archive manifest and receipt, create a tombstone, and keep intake documents.

## Status And Next

Validate hashes and graph read-only. `next` reports candidates but never starts
Review, Specify, Autonomous, or Parallel Autonomous.
