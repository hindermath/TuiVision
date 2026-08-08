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
  const errors = [];
  const resolve = (candidate) => path.isAbsolute(candidate) ? candidate : path.join(root, candidate);
  const read = (relativePath) => fs.readFileSync(resolve(relativePath), "utf8");
  const parse = (relativePath) => JSON.parse(read(relativePath));

  const baselinePath = "requirements/baseline/Pflichtenheft.pre-intake-split.2026-07-26.md";
  const coverage = parse(coveragePath);
  const manifest = parse(manifestPath);
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

  const activeRoot = path.join(root, "requirements/intakes/active");
  const archiveRoot = path.join(root, "requirements/intakes/archive");
  const active = fs.readdirSync(activeRoot).filter((name) => name.endsWith(".md")).sort();
  const archived = fs.readdirSync(archiveRoot).filter((name) => name.endsWith(".md")).sort();
  const rootLastenhefte = fs.readdirSync(root).filter((name) => /^Lastenheft.*\.md$/.test(name));
  if (active.length !== 7) errors.push(`expected 7 active intakes, found ${active.length}`);
  if (archived.length !== 28) errors.push(`expected 28 archived intakes, found ${archived.length}`);
  if (rootLastenhefte.join(",") !== "Lastenheft_Abarbeitungsreihenfolge.md") {
    errors.push("only the generated processing-order view may remain as root Lastenheft");
  }

  const targets = manifest.orderedTargets ?? [];
  const targetPaths = targets.map((target) => target.path);
  if (targetPaths.length !== 7 || new Set(targetPaths).size !== targetPaths.length) {
    errors.push("series must contain exactly 7 unique active targets");
  }
  const expectedActive = active.map((name) => `requirements/intakes/active/${name}`).sort();
  if (JSON.stringify([...targetPaths].sort()) !== JSON.stringify(expectedActive)) {
    errors.push("active intake directory and series targets differ");
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
      !eligible[0].path.endsWith("requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md")) {
    errors.push("Wave-6 closure must be the single explicitly Eligible target");
  }
  const portfolio = targets.find((target) =>
    target.path.endsWith("requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md"));
  if (!portfolio || portfolio.status !== "Blocked") {
    errors.push("post-Wave-6 portfolio audit must remain Blocked");
  }

  const dependencies = manifest.dependencies ?? [];
  if (dependencies.length !== 1 ||
      dependencies[0].kind !== "HardCompletionGate" ||
      dependencies[0].binding !== true ||
      dependencies[0].from !== eligible[0]?.path ||
      dependencies[0].to !== portfolio?.path) {
    errors.push("series must contain exactly the Wave-6-to-portfolio hard gate");
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
  const allowedFeatureDirectories = new Set([
    "specs/036-wave6-tvfm-showcase-remediation",
    "specs/037-wave6-combined-delta-closure",
  ]);
  if (!allowedFeatureDirectories.has(feature.feature_directory)) {
    errors.push("feature metadata must reference the completed predecessor or the explicitly eligible Wave-6 closure");
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
