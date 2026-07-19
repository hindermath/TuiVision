# Runbook einer parallelen autonomen Kampagne / Parallel Autonomous Campaign Runbook

## Identitaet und Umfang / Identity and Scope

| Field | Value |
|---|---|
| Campaign ID | `[UUID]` |
| Topology | `[ReplicatedTargets/IndependentFeatures/AlternativeSolutions/Pipeline]` |
| Delivery mode | `[LocalImplementation/PublishPR/MergeAndSync]` |
| Maximum concurrency | `[1-3]` |
| Runner profile | `[profile]` |
| Execution environment | `[native/container + identifier]` |

## Berechtigung / Authority

Installation ist keine Ausfuehrungsberechtigung. Lokale, Publish-, Merge-,
Bypass-, Abbruch-, Secret- und Provider-Administrationsberechtigung getrennt
dokumentieren. Bei Unklarheit gilt `LocalImplementation`.

*Installation is not execution authority. Record current local, publish, merge,
bypass, cancellation, secret, and provider-administration authority separately.
Ambiguity defaults to `LocalImplementation`.*

## Richtlinienausnahme / Policy Exception

Bei einem ausdruecklichen Owner-Override Autor, Umfang, Grund,
Autorisierungsdatum, Ablaufbedingung und betroffene Regeln dokumentieren. Die
Ausnahme gilt nur fuer diese Kampagne.

*If the campaign uses an explicit owner override, record the authorizer, exact
scope, reason, authorization date, expiry condition, and policies affected.
The exception applies only to this campaign.*

## Isolation / Isolation

Each worker owns one branch and one linked worktree. Runtime logs, locks, and
local runner bindings stay outside tracked feature artifacts. The normal
checkout is never switched or reset.

Sequential workers in one repository may declare `baseWorkerId`. It must name
a direct dependency in the same repository. The coordinator creates the new
branch from that predecessor's validated exact head, not from a moving branch
name.

## Planung / Scheduling

Ordinary worker failures do not cancel unrelated running workers. Pipeline
descendants wait for validated immutable handoffs. Campaign-integrity,
security, permission, and evidence-integrity failures stop new scheduling.

## Stop und Resume / Stop and Resume

A cooperative stop prevents new workers and lets active workers reach their
safe boundary. After interruption, reconcile process outcome, Git state,
worker result, autonomous state, and evidence before retry.

## Konsolidierung / Consolidation

Alternative solutions require a named human selection. A `MergeAndSync`
campaign first publishes every worker. All workers must then pass provider,
exact-head, check, and review gates before the first deterministic merge.
Checkpoint every verified merge, revalidate remaining bases, and run only
manifest-declared idempotent post-merge actions before `Completed`.
