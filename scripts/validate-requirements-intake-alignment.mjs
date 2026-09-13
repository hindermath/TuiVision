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
  const standaloneReviewPath = options.standaloneReviewPath ??
    "specs/intake-review-result.json";
  const activePath = options.activePath ?? "requirements/intakes/active";
  const archivePath = options.archivePath ?? "requirements/intakes/archive";
  const receiptsPath = options.receiptsPath ?? "specs/intake-authoring-receipts";
  const exactFixturePath = options.exactFixturePath ??
    "scripts/tests/linked-intake-evidence/tuivision-exact.json";
  const errors = [];
  const resolve = (candidate) => path.isAbsolute(candidate) ? candidate : path.join(root, candidate);
  const read = (relativePath) => fs.readFileSync(resolve(relativePath), "utf8");
  const parse = (relativePath) => JSON.parse(read(relativePath));

  const baselinePath = "requirements/baseline/Pflichtenheft.pre-intake-split.2026-07-26.md";
  const coverage = parse(coveragePath);
  const manifest = parse(manifestPath);
  const review = parse(reviewPath);
  const exact = parse(exactFixturePath);
  const seriesMapping = exact.seriesMapping;
  const expectedDependencyTuples = exact.dependencyTuples;
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
  const archiveRoot = resolve(archivePath);
  const active = fs.existsSync(activeRoot)
    ? fs.readdirSync(activeRoot).filter((name) => name.endsWith(".md")).sort()
    : [];
  const archived = fs.readdirSync(archiveRoot).filter((name) => name.endsWith(".md")).sort();
  const rootLastenhefte = fs.readdirSync(root).filter((name) => /^Lastenheft.*\.md$/.test(name));
  if (rootLastenhefte.join(",") !== "Lastenheft_Abarbeitungsreihenfolge.md") {
    errors.push("only the generated processing-order view may remain as root Lastenheft");
  }

  const targets = manifest.orderedTargets ?? [];
  const targetPaths = targets.map((target) => target.path);
  if (targetPaths.length !== seriesMapping.expectedSeriesTargetCount ||
      new Set(targetPaths).size !== targetPaths.length) {
    errors.push(`series must contain exactly ${seriesMapping.expectedSeriesTargetCount} unique targets`);
  }
  const activeSeriesTargets = targets.filter((target) =>
    target.path?.startsWith("requirements/intakes/active/"));
  if (activeSeriesTargets.length !== seriesMapping.expectedActiveCount) {
    errors.push(`expected ${seriesMapping.expectedActiveCount} active series targets, found ${activeSeriesTargets.length}`);
  }
  const expectedActive = active.map((name) => `requirements/intakes/active/${name}`).sort();
  const targetSet = new Set(targetPaths);
  const invalidLifecycleTargets = targets.filter((target) => {
    const inActive = target.path?.startsWith("requirements/intakes/active/");
    const inArchive = target.path?.startsWith("requirements/intakes/archive/");
    return target.path?.includes("/backlog/") ||
      target.status === "Completed" && !inArchive ||
      target.status !== "Completed" && !inActive;
  });
  if (invalidLifecycleTargets.length > 0) {
    errors.push(`series target status does not match its collection: ${invalidLifecycleTargets.map((target) => target.path).join(", ")}`);
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
  for (const [field, label] of [
    ["receiptId", "receipt ID"],
    ["intakeId", "intake ID"],
  ]) {
    const values = receipts.map((receipt) => receipt.value[field]).filter(Boolean);
    if (new Set(values).size !== values.length) {
      errors.push(`intake receipt inventory contains a duplicate ${label}`);
    }
  }
  const receiptTargets = receipts.map((receipt) => receipt.value.target?.path).filter(Boolean);
  if (new Set(receiptTargets).size !== receiptTargets.length) {
    errors.push("intake receipt inventory contains a duplicate target path");
  }
  let completedStandaloneReceiptCount = 0;
  for (const receipt of receipts) {
    const receiptTarget = receipt.value.target?.path;
    const physicalReceiptTarget = receiptTarget?.startsWith("requirements/intakes/active/")
      ? path.join(activeRoot, path.basename(receiptTarget))
      : receiptTarget ? resolve(receiptTarget) : "";
    if (!receiptTarget || fs.existsSync(physicalReceiptTarget)) continue;

    const receiptHash = receipt.value.target?.normalizedSha256;
    const originalStem = path.parse(receiptTarget).name;
    const receiptSeries = receipt.value.series ?? {};
    const seriesReceipt = receiptSeries.seriesId === manifest.seriesId;
    const standaloneReceipt = receiptSeries.seriesId === "N/A" &&
      receiptSeries.manifestPath === "N/A" && receiptSeries.order === "N/A" &&
      receiptSeries.role === "N/A";
    const completedArchiveMatches = seriesReceipt
      ? targets.filter((target) =>
          target.status === "Completed" &&
          target.path?.startsWith("requirements/intakes/archive/") &&
          path.basename(target.path).startsWith(`${originalStem}.`) &&
          target.normalizedSha256 === receiptHash &&
          fs.existsSync(resolve(target.path)) &&
          digest(read(target.path)) === receiptHash)
      : standaloneReceipt
        ? archived.filter((name) => path.parse(name).name.startsWith(`${originalStem}.`))
            .map((name) => path.join(archiveRoot, name))
            .filter((candidate) => digest(fs.readFileSync(candidate, "utf8")) === receiptHash)
        : [];
    if ((!seriesReceipt && !standaloneReceipt) || completedArchiveMatches.length !== 1) {
      errors.push(`missing authoring receipt target lacks one completed archive successor: ${receiptTarget}`);
    } else if (standaloneReceipt) {
      completedStandaloneReceiptCount++;
    }
  }
  const baselineStandaloneArchiveCount = seriesMapping.expectedStandaloneArchiveCount ?? 0;
  const expectedArchiveCount = seriesMapping.expectedArchiveCount +
    completedStandaloneReceiptCount - baselineStandaloneArchiveCount;
  if (archived.length !== expectedArchiveCount) {
    errors.push(`expected ${expectedArchiveCount} archived intakes, found ${archived.length}`);
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
  if (manifest.status !== "Completed" || targets.some((target) => target.status !== "Completed")) {
    errors.push("completed delivery series must retain only Completed targets");
  }
  const expectedTargetPaths = seriesMapping.mappings.map((item) => item.intakePath);
  if (JSON.stringify(targetPaths) !== JSON.stringify(expectedTargetPaths)) {
    errors.push("series target order differs from the exact feature mapping");
  }
  for (const item of seriesMapping.mappings) {
    const target = targets.find((candidate) => candidate.path === item.intakePath);
    const featureDirectory = item.featurePath.replace(/\/$/, "");
    const state = parse(item.proofPath);
    if (!target || target.status !== "Completed" || target.normalizedSha256 !== item.intakeSha256 ||
        !fs.existsSync(resolve(featureDirectory)) || state.featurePath !== featureDirectory ||
        state.branch !== path.basename(featureDirectory) || state.status !== "Completed" ||
        digest(read(item.proofPath)) !== item.proofSha256 ||
        !state.acceptedArtifacts?.some((artifact) =>
          artifact.path === item.intakePath && artifact.sha256 === item.intakeSha256)) {
      errors.push(`exact feature mapping is invalid: ${item.intakePath}`);
    }
  }

  const dependencies = manifest.dependencies ?? [];
  const dependencyKeys = new Set(dependencies.map((edge) =>
    `${edge.from}|${edge.to}|${edge.kind}|${edge.binding}`));
  if (dependencies.length !== expectedDependencyTuples.expectedEdgeCount ||
      expectedDependencyTuples.edges.some((edge) =>
        !dependencyKeys.has(`${edge.from}|${edge.to}|${edge.kind}|${edge.binding}`))) {
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
    if (!order.includes(target)) errors.push(`processing order omits series target: ${target}`);
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
      const activeBindingFile = bindingPath?.startsWith("requirements/intakes/active/")
        ? path.join(activeRoot, path.basename(bindingPath))
        : bindingPath ? resolve(bindingPath) : "";
      const effectiveBindingPath = bindingPath && fs.existsSync(activeBindingFile)
        ? bindingPath
        : archivedBindingPath;
      const bindingTarget = targets.find((target) => target.path === effectiveBindingPath);
      const bindingReview = (review.targets ?? []).find((target) => target.path === effectiveBindingPath);
      const bindingArtifact = (state.acceptedArtifacts ?? []).find((artifact) =>
        artifact.path === effectiveBindingPath);
      const effectiveBindingFile = effectiveBindingPath === bindingPath
        ? activeBindingFile
        : effectiveBindingPath?.startsWith("requirements/intakes/archive/")
          ? path.join(archiveRoot, path.basename(effectiveBindingPath))
          : effectiveBindingPath !== "N/A" ? resolve(effectiveBindingPath) : "";
      const bindingHash = effectiveBindingFile && fs.existsSync(effectiveBindingFile)
        ? digest(fs.readFileSync(effectiveBindingFile, "utf8"))
        : "N/A";
      const lifecycleValid = bindingTarget?.status === "Eligible" ||
        (bindingTarget?.status === "Completed" &&
          (state.status === "Completed" ||
           (state.status === "Active" && state.deliveryMode === "MergeAndSync" &&
            ["Publish", "Review", "MergeAndSync"].includes(state.stage))));
      const seriesAuthorizationValid = lifecycleValid && review.status === "Ready" &&
        bindingReview?.normalizedSha256 === bindingHash &&
        bindingArtifact?.sha256 === bindingHash;

      let standaloneAuthorizationValid = false;
      const standaloneReviewFile = resolve(standaloneReviewPath);
      if (!bindingTarget && bindingPath?.startsWith("requirements/intakes/active/")) {
        const reviewArtifact = (state.acceptedArtifacts ?? []).find((artifact) =>
          artifact.path === "specs/intake-review-result.json");
        const historyRoot = resolve("specs/intake-review-history");
        const historicalReviews = fs.existsSync(historyRoot)
          ? fs.readdirSync(historyRoot, {withFileTypes: true})
              .filter((entry) => entry.isDirectory())
              .map((entry) => path.join(historyRoot, entry.name, "result.json"))
              .filter((candidate) => fs.existsSync(candidate))
          : [];
        const reviewCandidates = [standaloneReviewFile, ...historicalReviews]
          .filter((candidate) => fs.existsSync(candidate));
        const selectedReviewFile = state.status === "Completed" && reviewArtifact?.sha256
          ? reviewCandidates.find((candidate) =>
              digest(fs.readFileSync(candidate, "utf8")) === reviewArtifact.sha256)
          : reviewCandidates[0];
        const standaloneReview = selectedReviewFile
          ? JSON.parse(fs.readFileSync(selectedReviewFile, "utf8"))
          : {};
        const standaloneReviewTarget = (standaloneReview.targets ?? []).find((target) =>
          target.path === bindingPath);
        const matchingReceipts = receipts.filter((receipt) =>
          receipt.value.target?.path === bindingPath);
        const receipt = matchingReceipts.length === 1 ? matchingReceipts[0].value : null;
        const receiptHash = receipt?.target?.normalizedSha256;
        const standaloneLifecycleValid =
          (state.status === "Active" &&
            ["LocalImplementation", "PublishPR", "MergeAndSync"].includes(state.deliveryMode) &&
            effectiveBindingPath === bindingPath) ||
          (state.status === "Completed" && effectiveBindingPath === archivedBindingPath);
        const standaloneArtifact = (state.acceptedArtifacts ?? []).find((artifact) =>
          artifact.path === effectiveBindingPath || artifact.path === bindingPath);
        standaloneAuthorizationValid = standaloneLifecycleValid &&
          receipt?.schemaVersion === "2.0" && receipt?.documentType === "IntakeReceipt" &&
          receipt?.status === "ReadyForReview" && receipt?.series?.seriesId === "N/A" &&
          receipt?.series?.manifestPath === "N/A" && receipt?.series?.role === "N/A" &&
          receiptHash === bindingHash && standaloneReview.mode === "Single" &&
          standaloneReview.status === "Ready" &&
          standaloneReviewTarget?.normalizedSha256 === bindingHash &&
          standaloneArtifact?.sha256 === bindingHash;
      }

      featureAuthorizationValid = state.featurePath === featureDirectory &&
        state.branch === path.basename(featureDirectory) &&
        Boolean(bindingPath) && (seriesAuthorizationValid || standaloneAuthorizationValid);
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
