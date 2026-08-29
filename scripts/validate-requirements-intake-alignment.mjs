#!/usr/bin/env node

import crypto from "node:crypto";
import fs from "node:fs";
import path from "node:path";
import process from "node:process";
import {fileURLToPath} from "node:url";

const normalize = (value) => value.replace(/^\uFEFF/, "").replace(/\r\n?/g, "\n");
const digest = (value) => crypto.createHash("sha256").update(normalize(value)).digest("hex");

export function validate(options = {}) {
  const root = options.root ?? process.cwd();
  const manifestPath = options.manifestPath ??
    "requirements/intakes/series/tui-vision-delivery/manifest.json";
  const coveragePath = options.coveragePath ??
    "specs/requirements-reconciliation-20260726/requirements-coverage.json";
  const featurePath = options.featurePath ?? ".specify/feature.json";
  const reviewPath = options.reviewPath ??
    "requirements/intakes/series/tui-vision-delivery/intake-review-result.json";
  const activePath = options.activePath ?? "requirements/intakes/active";
  const receiptsPath = options.receiptsPath ?? "specs/intake-authoring-receipts";
  const errors = [];
  const resolve = (candidate) => path.isAbsolute(candidate) ? candidate : path.join(root, candidate);
  const read = (relativePath) => fs.readFileSync(resolve(relativePath), "utf8");
  const parse = (relativePath) => JSON.parse(read(relativePath));

  const baselinePath = "requirements/baseline/Pflichtenheft.pre-intake-split.2026-07-26.md";
  const coverage = parse(coveragePath);
  const manifest = parse(manifestPath);
  const review = parse(reviewPath);
  const baselineHash = digest(read(baselinePath));

  if (baselineHash !== coverage.source?.normalizedSha256) {
    errors.push("baseline Pflichtenheft hash differs from reconciliation evidence");
  }

  const requirementIds = coverage.requirements?.map((item) => item.requirementId) ?? [];
  if (requirementIds.length !== 167 || new Set(requirementIds).size !== requirementIds.length) {
    errors.push("coverage must contain exactly 167 unique requirement IDs");
  }
  for (const item of coverage.requirements ?? []) {
    if (["Open", "PartiallySatisfied"].includes(item.status) &&
        (!item.proposedOwnerGroup || item.proposedOwnerGroup === "N/A")) {
      errors.push(`open requirement lacks owner: ${item.requirementId}`);
    }
    if (item.status === "AlreadySatisfied" &&
        (!Array.isArray(item.evidencePaths) || item.evidencePaths.length === 0)) {
      errors.push(`positive requirement lacks evidence: ${item.requirementId}`);
    }
  }

  const activeRoot = resolve(activePath);
  const archiveRoot = path.join(root, "requirements/intakes/archive");
  const active = fs.readdirSync(activeRoot).filter((name) => name.endsWith(".md")).sort();
  const archived = fs.readdirSync(archiveRoot).filter((name) => name.endsWith(".md")).sort();
  const rootLastenhefte = fs.readdirSync(root).filter((name) => /^Lastenheft.*\.md$/.test(name));
  if (archived.length !== 28) errors.push(`expected 28 archived intakes, found ${archived.length}`);
  if (rootLastenhefte.join(",") !== "Lastenheft_Abarbeitungsreihenfolge.md") {
    errors.push("only the generated processing-order view may remain as root Lastenheft");
  }

  const targets = manifest.orderedTargets ?? [];
  const targetPaths = targets.map((target) => target.path);
  if (targetPaths.length !== 10 || new Set(targetPaths).size !== targetPaths.length) {
    errors.push("series must contain exactly 10 unique active targets");
  }
  const expectedActive = active.map((name) => `requirements/intakes/active/${name}`).sort();
  const targetSet = new Set(targetPaths);
  const missingActiveTargets = targetPaths.filter((target) => !expectedActive.includes(target));
  if (missingActiveTargets.length > 0) {
    errors.push(`series targets are missing from the active intake directory: ${missingActiveTargets.join(", ")}`);
  }

  const reviewedTargets = new Set((review.targets ?? []).map((target) => target.path));
  const receiptsRoot = resolve(receiptsPath);
  const receiptFiles = fs.existsSync(receiptsRoot)
    ? fs.readdirSync(receiptsRoot).filter((name) => name.endsWith(".json"))
    : [];
  const receipts = receiptFiles.flatMap((name) => {
    try {
      return [{path: path.join(receiptsRoot, name), value: JSON.parse(fs.readFileSync(path.join(receiptsRoot, name), "utf8"))}];
    } catch {
      errors.push(`intake receipt is not valid JSON: ${name}`);
      return [];
    }
  });
  for (const pendingPath of expectedActive.filter((candidate) => !targetSet.has(candidate))) {
    const matchingReceipts = receipts.filter((receipt) => receipt.value.target?.path === pendingPath);
    if (matchingReceipts.length !== 1) {
      errors.push(`active intake outside the series requires exactly one authoring receipt: ${pendingPath}`);
      continue;
    }
    const receipt = matchingReceipts[0].value;
    const activeFile = path.join(activeRoot, path.basename(pendingPath));
    const targetHash = digest(fs.readFileSync(activeFile, "utf8"));
    if (receipt.schemaVersion !== "2.0" || receipt.documentType !== "IntakeReceipt" ||
        receipt.status !== "ReadyForReview") {
      errors.push(`active intake outside the series must have a schema-2.0 ReadyForReview receipt: ${pendingPath}`);
    }
    if (receipt.target?.normalizedSha256 !== targetHash) {
      errors.push(`active intake outside the series has stale receipt evidence: ${pendingPath}`);
    }
    if (receipt.series?.seriesId !== "N/A" || receipt.series?.manifestPath !== "N/A" ||
        receipt.series?.role !== "N/A") {
      errors.push(`active intake outside the series must not claim series membership: ${pendingPath}`);
    }
    if (reviewedTargets.has(pendingPath)) {
      errors.push(`active intake outside the series must remain unreviewed until the series is explicitly updated: ${pendingPath}`);
    }
    if (!fs.readFileSync(activeFile, "utf8").includes("**Status:** ReadyForReview")) {
      errors.push(`active intake outside the series must declare ReadyForReview: ${pendingPath}`);
    }
  }
  if (targetPaths.some((target) => target.includes("/archive/") || target.includes("/backlog/"))) {
    errors.push("archive or backlog target appears in executable series");
  }
  for (const target of targets) {
    const fullPath = path.join(root, target.path ?? "");
    if (!target.path || !fs.existsSync(fullPath)) {
      errors.push(`series target is missing: ${target.path ?? "N/A"}`);
    } else if (digest(fs.readFileSync(fullPath, "utf8")) !== target.normalizedSha256) {
      errors.push(`series target hash drift: ${target.path}`);
    }
  }

  const eligible = targets.filter((target) => target.status === "Eligible");
  if (eligible.length !== 1 ||
      !eligible[0].path.endsWith("requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md")) {
    errors.push("RL-SE checklist self-review must be the single explicitly Eligible target");
  }
  const wave6Closure = targets.find((target) =>
    target.path.endsWith("requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md"));
  if (!wave6Closure || wave6Closure.status !== "Completed") {
    errors.push("Wave-6 closure must remain Completed");
  }
  const portfolio = targets.find((target) =>
    target.path.endsWith("requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md"));
  if (!portfolio || portfolio.status !== "Completed") {
    errors.push("post-Wave-6 portfolio audit must remain Completed");
  }

  const portfolioClosure = targets.find((target) =>
    target.path.endsWith("requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md"));
  const constitution = targets.find((target) =>
    target.path.endsWith("requirements/intakes/active/Lastenheft_Constitution_Change.md"));
  const sourcePolicy = targets.find((target) =>
    target.path.endsWith("requirements/intakes/active/Lastenheft_Source-Reference-Policy.md"));
  const formModel = targets.find((target) =>
    target.path.endsWith("requirements/intakes/active/Lastenheft_Transactional-Form-Model.md"));
  const documentationClosure = targets.find((target) =>
    target.path.endsWith("requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md"));
  const sandboxHardening = targets.find((target) =>
    target.path.endsWith("requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md"));
  const rlSeReview = targets.find((target) =>
    target.path.endsWith("requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md"));
  for (const [label, target] of [
    ["portfolio closure", portfolioClosure],
    ["constitution change", constitution],
    ["source-reference policy", sourcePolicy],
    ["transactional form model", formModel],
  ]) {
    if (!target || target.status !== "Completed") {
      errors.push(`${label} must remain Completed`);
    }
  }
  if (!documentationClosure || documentationClosure.status !== "Completed") {
    errors.push("documentation publishing closure must remain Completed");
  }
  if (!sandboxHardening || sandboxHardening.status !== "Completed") {
    errors.push("sandbox security hardening must remain Completed");
  }
  if (!rlSeReview || rlSeReview.status !== "Eligible") {
    errors.push("RL-SE checklist self-review must remain Eligible");
  }

  const dependencies = manifest.dependencies ?? [];
  const expectedDependencies = [
    [wave6Closure?.path, portfolio?.path, "HardCompletionGate", true],
    [portfolio?.path, portfolioClosure?.path, "HardCompletionGate", true],
    [constitution?.path, sourcePolicy?.path, "SharedWriterSerialization", false],
    [sourcePolicy?.path, formModel?.path, "HardCompletionGate", true],
    [portfolioClosure?.path, formModel?.path, "HardCompletionGate", true],
    [formModel?.path, documentationClosure?.path, "PreferredSerialOrder", false],
  ];
  const dependencyKeys = new Set(dependencies.map((edge) =>
    `${edge.from}|${edge.to}|${edge.kind}|${edge.binding}`));
  if (dependencies.length !== expectedDependencies.length ||
      expectedDependencies.some(([from, to, kind, binding]) =>
        !dependencyKeys.has(`${from}|${to}|${kind}|${binding}`))) {
    errors.push("series must contain the exact six approved delivery dependencies");
  }
  const indegree = new Map(targetPaths.map((target) => [target, 0]));
  const adjacency = new Map(targetPaths.map((target) => [target, []]));
  for (const edge of dependencies) {
    if (!indegree.has(edge.from) || !indegree.has(edge.to) || edge.from === edge.to) {
      errors.push(`invalid dependency reference: ${edge.from} -> ${edge.to}`);
      continue;
    }
    indegree.set(edge.to, indegree.get(edge.to) + 1);
    adjacency.get(edge.from).push(edge.to);
  }
  const roots = [...indegree].filter(([, value]) => value === 0).map(([key]) => key);
  if (JSON.stringify([...roots].sort()) !== JSON.stringify([...(manifest.roots ?? [])].sort())) {
    errors.push("manifest roots differ from dependency graph");
  }
  const queue = [...roots];
  const remaining = new Map(indegree);
  let visited = 0;
  while (queue.length > 0) {
    const current = queue.shift();
    visited++;
    for (const successor of adjacency.get(current) ?? []) {
      remaining.set(successor, remaining.get(successor) - 1);
      if (remaining.get(successor) === 0) queue.push(successor);
    }
  }
  if (visited !== targetPaths.length) errors.push("series dependencies contain a cycle");

  const order = read("Lastenheft_Abarbeitungsreihenfolge.md");
  const index = read("Pflichtenheft.md");
  for (const target of targetPaths) {
    if (!order.includes(target)) errors.push(`processing order omits active target: ${target}`);
  }
  if (!index.includes(manifestPath)) errors.push("Pflichtenheft index omits canonical manifest");
  if (/\[[ xX-]\]/.test(index)) errors.push("slim Pflichtenheft must not contain progress checkboxes");

  const feature = parse(featurePath);
  const featureDirectory = feature.feature_directory;
  const physicalFeatureDirectory = options.featureDirectoryPath ??
    (typeof featureDirectory === "string" ? resolve(featureDirectory) : "");
  const featurePattern = /^specs\/\d{3}-[a-z0-9][a-z0-9-]*$/;
  let featureAuthorizationValid = typeof featureDirectory === "string" &&
    featurePattern.test(featureDirectory) && fs.existsSync(physicalFeatureDirectory);
  if (featureAuthorizationValid) {
    try {
      const spec = fs.readFileSync(path.join(physicalFeatureDirectory, "spec.md"), "utf8");
      const state = JSON.parse(fs.readFileSync(
        path.join(physicalFeatureDirectory, "autonomous-run-state.json"), "utf8"));
      const bindingMatch = /^\*\*(?:Binding Intake|Input)\*\*:\s*`([^`]+)`/m.exec(spec);
      const bindingPath = bindingMatch?.[1];
      const bindingTarget = targets.find((target) => target.path === bindingPath);
      const bindingReview = (review.targets ?? []).find((target) => target.path === bindingPath);
      const bindingArtifact = (state.acceptedArtifacts ?? []).find((artifact) => artifact.path === bindingPath);
      const bindingHash = bindingPath && fs.existsSync(resolve(bindingPath))
        ? digest(read(bindingPath))
        : "N/A";
      const lifecycleValid = bindingTarget?.status === "Eligible" ||
        (bindingTarget?.status === "Completed" &&
          (state.status === "Completed" ||
           (state.status === "Active" && state.deliveryMode === "MergeAndSync" &&
            ["Publish", "Review", "MergeAndSync"].includes(state.stage))));
      featureAuthorizationValid = state.featurePath === featureDirectory &&
        state.branch === path.basename(featureDirectory) &&
        Boolean(bindingPath) && lifecycleValid && review.status === "Ready" &&
        bindingReview?.normalizedSha256 === bindingHash &&
        bindingArtifact?.sha256 === bindingHash;
    } catch {
      featureAuthorizationValid = false;
    }
  }
  if (!featureAuthorizationValid) {
    errors.push("feature metadata lacks matching series, review, specification, and autonomous-run authorization evidence");
  }

  const optional = "requirements/intakes/backlog/Lastenheft_Optional-NuGet-Package.md";
  if (!read(optional).includes("DeferredOptional")) {
    errors.push("optional NuGet intake must remain DeferredOptional");
  }

  return errors;
}

const isMain = process.argv[1] &&
  path.resolve(process.argv[1]) === path.resolve(fileURLToPath(import.meta.url));
if (isMain) {
  const errors = validate();
  if (errors.length > 0) {
    errors.forEach((error) => console.error(`ERROR: ${error}`));
    process.exit(2);
  }
  console.log("requirements/intake alignment PASS");
}
