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
  if (archived.length !== 30) errors.push(`expected 30 archived intakes, found ${archived.length}`);
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
  const invalidLifecycleTargets = targets.filter((target) =>
    target.path?.includes("/backlog/") ||
    (target.path?.includes("/archive/") && target.status !== "Completed"));
  if (invalidLifecycleTargets.length > 0) {
    errors.push(`archive or backlog target has an executable lifecycle: ${invalidLifecycleTargets.map((target) => target.path).join(", ")}`);
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
  for (const receipt of receipts) {
    const receiptTarget = receipt.value.target?.path;
    const physicalReceiptTarget = receiptTarget?.startsWith("requirements/intakes/active/")
      ? path.join(activeRoot, path.basename(receiptTarget))
      : receiptTarget ? resolve(receiptTarget) : "";
    if (!receiptTarget || fs.existsSync(physicalReceiptTarget)) continue;

    const receiptHash = receipt.value.target?.normalizedSha256;
    const originalStem = path.parse(receiptTarget).name;
    const completedArchiveMatches = targets.filter((target) =>
      target.status === "Completed" &&
      target.path?.startsWith("requirements/intakes/archive/") &&
      path.basename(target.path).startsWith(`${originalStem}.`) &&
      target.normalizedSha256 === receiptHash &&
      fs.existsSync(resolve(target.path)) &&
      digest(read(target.path)) === receiptHash);
    if (receipt.value.series?.seriesId !== manifest.seriesId ||
        completedArchiveMatches.length !== 1) {
      errors.push(`missing authoring receipt target lacks one completed archive successor: ${receiptTarget}`);
    }
  }
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
  for (const target of targets) {
    const fullPath = path.join(root, target.path ?? "");
    if (!target.path || !fs.existsSync(fullPath)) {
      errors.push(`series target is missing: ${target.path ?? "N/A"}`);
    } else if (digest(fs.readFileSync(fullPath, "utf8")) !== target.normalizedSha256) {
      errors.push(`series target hash drift: ${target.path}`);
    }
  }

  const eligible = targets.filter((target) => target.status === "Eligible");
  if (eligible.length !== 0) {
    errors.push("completed delivery series must not expose an Eligible target");
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
    target.path.endsWith("requirements/intakes/archive/Lastenheft_RL-SE-Checklist-Selbstpruefung.045-rl-se-checklist-self-review.md"));
  const gsdbReview = targets.find((target) =>
    target.path.endsWith("requirements/intakes/archive/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.046-gsdb-spec-kit-intensive-review.md"));
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
  if (!rlSeReview || rlSeReview.status !== "Completed") {
    errors.push("RL-SE checklist self-review must remain Completed and archived");
  }
  if (!gsdbReview || gsdbReview.status !== "Completed") {
    errors.push("GSDB Spec Kit intensive review must remain Completed and archived");
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
      const bindingMatch = /^\*\*(?:Binding Intake|Binding Input|Input)\*\*:\s*`([^`]+)`/m.exec(spec);
      const bindingPath = bindingMatch?.[1];
      const bindingName = bindingPath ? path.parse(bindingPath).name : "";
      const archivedBindingPath = bindingPath && state.status === "Completed"
        ? `requirements/intakes/archive/${bindingName}.${state.branch}.md`
        : "N/A";
      const effectiveBindingPath = bindingPath && fs.existsSync(resolve(bindingPath))
        ? bindingPath
        : archivedBindingPath;
      const bindingTarget = targets.find((target) => target.path === effectiveBindingPath);
      const bindingReview = (review.targets ?? []).find((target) => target.path === effectiveBindingPath);
      const bindingArtifact = (state.acceptedArtifacts ?? []).find((artifact) =>
        artifact.path === effectiveBindingPath);
      const bindingHash = effectiveBindingPath !== "N/A" && fs.existsSync(resolve(effectiveBindingPath))
        ? digest(read(effectiveBindingPath))
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
