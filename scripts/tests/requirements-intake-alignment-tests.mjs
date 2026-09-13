#!/usr/bin/env node

import crypto from "node:crypto";
import fs from "node:fs";
import os from "node:os";
import path from "node:path";
import process from "node:process";
import {validate} from "../validate-requirements-intake-alignment.mjs";

const root = process.cwd();
const manifestSource = "requirements/intakes/series/tui-vision-delivery/manifest.json";
const coverageSource = "specs/requirements-reconciliation-20260726/requirements-coverage.json";
const temp = fs.mkdtempSync(path.join(os.tmpdir(), "tuivision-requirements-"));

function fixture(name, source, mutate) {
  const value = JSON.parse(fs.readFileSync(path.join(root, source), "utf8"));
  mutate(value);
  const target = path.join(temp, `${name}.json`);
  fs.writeFileSync(target, JSON.stringify(value, null, 2) + "\n");
  return target;
}

function expectFailure(name, options, pattern) {
  const errors = validate({root, ...options});
  if (!errors.some((error) => pattern.test(error))) {
    throw new Error(`${name} did not fail as expected: ${errors.join("; ")}`);
  }
}

function expectSuccess(name, options) {
  const errors = validate({root, ...options});
  if (errors.length !== 0) {
    throw new Error(`${name} failed unexpectedly: ${errors.join("; ")}`);
  }
}

function activeFixture(name) {
  const target = path.join(temp, name);
  fs.mkdirSync(target, {recursive: true});
  const manifest = JSON.parse(fs.readFileSync(path.join(root, manifestSource), "utf8"));
  for (const member of manifest.orderedTargets) {
    if (!member.path.includes("/active/")) continue;
    const entry = path.basename(member.path);
    fs.copyFileSync(
      path.join(root, member.path),
      path.join(target, entry));
  }
  return target;
}

function pendingIntakeFixture(name, mutateReceipt = () => {}) {
  const activePath = activeFixture(`${name}-active`);
  const receiptsPath = path.join(temp, `${name}-receipts`);
  fs.mkdirSync(receiptsPath, {recursive: true});
  const fileName = "Lastenheft_Future-Closure.md";
  const targetPath = `requirements/intakes/active/${fileName}`;
  const content = "# Future Closure\n\n**Status:** ReadyForReview\n";
  fs.writeFileSync(path.join(activePath, fileName), content);
  const normalizedSha256 = awaitDigest(content);
  const receipt = {
    schemaVersion: "2.0",
    documentType: "IntakeReceipt",
    status: "ReadyForReview",
    target: {path: targetPath, normalizedSha256},
    series: {seriesId: "N/A", manifestPath: "N/A", role: "N/A"},
  };
  mutateReceipt(receipt);
  fs.writeFileSync(path.join(receiptsPath, "future-closure.json"), JSON.stringify(receipt, null, 2) + "\n");
  return {activePath, receiptsPath, targetPath};
}

function receiptsFixture(name, mutate) {
  const target = path.join(temp, name);
  fs.cpSync(path.join(root, "specs/intake-authoring-receipts"), target, {recursive: true});
  mutate(target);
  return target;
}

function awaitDigest(value) {
  return crypto.createHash("sha256").update(value.replace(/\r\n?/g, "\n")).digest("hex");
}

if (validate({root}).length !== 0) throw new Error("positive fixture failed");

expectSuccess("completed series without physical active directory", {
  activePath: path.join(temp, "absent-active-collection"),
});

const pending = pendingIntakeFixture("valid-pending");
expectSuccess("valid authored pending intake", pending);

const activeFeatureDirectory = path.join(temp, "043-documentation-publishing-closure");
fs.mkdirSync(activeFeatureDirectory, {recursive: true});
const activeFeaturePath = "specs/043-documentation-publishing-closure";
const bindingIntake =
  "requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md";
const archivedBindingIntake =
  "requirements/intakes/archive/Lastenheft_23_Documentation-Publishing-Closure.043-documentation-publishing-closure.md";
const bindingHash = awaitDigest(fs.readFileSync(path.join(root, archivedBindingIntake), "utf8"));
fs.writeFileSync(
  path.join(activeFeatureDirectory, "spec.md"),
  `# Authorized Feature\n\n**Binding Intake**: \`${bindingIntake}\`\n`);
fs.writeFileSync(
  path.join(activeFeatureDirectory, "autonomous-run-state.json"),
  JSON.stringify({
    featurePath: activeFeaturePath,
    branch: "043-documentation-publishing-closure",
    status: "Completed",
    acceptedArtifacts: [{path: archivedBindingIntake, sha256: bindingHash}],
  }, null, 2) + "\n");
const activeFeatureMetadata = path.join(temp, "authorized-feature.json");
fs.writeFileSync(
  activeFeatureMetadata,
  JSON.stringify({feature_directory: activeFeaturePath}, null, 2) + "\n");
expectSuccess("completed series feature with matching evidence", {
  featurePath: activeFeatureMetadata,
  featureDirectoryPath: activeFeatureDirectory,
});

const missingReceipt = activeFixture("missing-receipt-active");
fs.writeFileSync(
  path.join(missingReceipt, "Lastenheft_Future-Closure.md"),
  "# Future Closure\n\n**Status:** ReadyForReview\n");
expectFailure("pending intake without receipt", {
  activePath: missingReceipt,
  receiptsPath: path.join(temp, "empty-receipts"),
}, /requires exactly one authoring receipt/);

const stalePending = pendingIntakeFixture("stale-pending", (receipt) => {
  receipt.target.normalizedSha256 = "0".repeat(64);
});
expectFailure("pending intake with stale receipt", stalePending, /stale receipt evidence/);

const reviewedPending = pendingIntakeFixture("reviewed-pending");
expectFailure("pending intake already present in accepted review", {
  ...reviewedPending,
  reviewPath: fixture("reviewed-pending", "requirements/intakes/series/tui-vision-delivery/intake-review-result.json", (value) => {
    value.targets.push({
      path: reviewedPending.targetPath,
      role: "OrderedMember",
      normalizedSha256: "0".repeat(64),
      gitBlob: "N/A",
    });
  }),
}, /must remain unreviewed/);

expectFailure("pending intake injected into executable series", {
  ...pending,
  manifestPath: fixture("pending-in-series", manifestSource, (value) => {
    value.orderedTargets.push({
      path: pending.targetPath,
      role: "OrderedMember",
      normalizedSha256: "0".repeat(64),
      status: "Eligible",
    });
  }),
}, /series must contain exactly 10 unique targets/);

expectFailure("unauthorized feature", {
  featurePath: fixture("unauthorized-feature", ".specify/feature.json", (value) => {
    value.feature_directory = "specs/038-post-wave6-portfolio-audit";
  }),
}, /lacks matching series, review, specification, and autonomous-run authorization evidence/);

expectFailure("stale predecessor feature", {
  featurePath: fixture("stale-predecessor-feature", ".specify/feature.json", (value) => {
    value.feature_directory = "specs/036-wave6-tvfm-showcase-remediation";
  }),
}, /lacks matching series, review, specification, and autonomous-run authorization evidence/);

expectFailure("duplicate target", {
  manifestPath: fixture("duplicate-target", manifestSource, (value) => {
    value.orderedTargets.push({...value.orderedTargets[0]});
  }),
}, /unique targets/);

expectFailure("backlog target", {
  manifestPath: fixture("backlog-target", manifestSource, (value) => {
    value.orderedTargets[6].path =
      "requirements/intakes/backlog/Lastenheft_Optional-NuGet-Package.md";
  }),
}, /status does not match its collection|series target is missing/);

expectFailure("unexpected eligible", {
  manifestPath: fixture("unexpected-eligible", manifestSource, (value) => {
    value.orderedTargets.at(-1).status = "Eligible";
  }),
}, /must not expose an Eligible target/);

expectFailure("incomplete Wave-6 closure", {
  manifestPath: fixture("incomplete-wave6", manifestSource, (value) => {
    value.orderedTargets[0].status = "Pending";
  }),
}, /status does not match its collection|completed delivery series must retain only Completed targets/);

expectFailure("stale target hash", {
  manifestPath: fixture("stale-hash", manifestSource, (value) => {
    value.orderedTargets[0].normalizedSha256 = "0".repeat(64);
  }),
}, /target hash drift/);

expectFailure("missing receipt target without completed archive successor", {
  receiptsPath: receiptsFixture("orphaned-terminal-receipt", (receiptsPath) => {
    const receiptPath = path.join(receiptsPath, "rl-se-checklist-selbstpruefung.json");
    const receipt = JSON.parse(fs.readFileSync(receiptPath, "utf8"));
    receipt.target.normalizedSha256 = "0".repeat(64);
    fs.writeFileSync(receiptPath, JSON.stringify(receipt, null, 2) + "\n");
  }),
}, /lacks one completed archive successor/);

expectFailure("standalone receipt with foreign series claim", {
  receiptsPath: receiptsFixture("foreign-series-receipt", (receiptsPath) => {
    const receiptPath = path.join(receiptsPath, "Lastenheft_Example-Portfolio-Closure.receipt.json");
    const receipt = JSON.parse(fs.readFileSync(receiptPath, "utf8"));
    receipt.series = {
      seriesId: "00000000-0000-4000-8000-000000000001",
      manifestPath: "requirements/intakes/series/foreign/manifest.json",
      order: 1,
      role: "OrderedMember",
      supersedesIntakeIds: [],
    };
    fs.writeFileSync(receiptPath, JSON.stringify(receipt, null, 2) + "\n");
  }),
}, /lacks one completed archive successor/);

expectFailure("invalid dependency cycle", {
  manifestPath: fixture("dependency-cycle", manifestSource, (value) => {
    value.dependencies.push({
      from: value.orderedTargets[1].path,
      to: value.orderedTargets[0].path,
      kind: "HardCompletionGate",
      binding: true,
    });
    value.roots = value.roots.filter((item) => item !== value.orderedTargets[0].path);
  }),
}, /exact six approved delivery dependencies|cycle/);

expectFailure("dangling target", {
  manifestPath: fixture("dangling-target", manifestSource, (value) => {
    value.orderedTargets[6].path = "requirements/intakes/active/Missing.md";
  }),
}, /status does not match its collection|target is missing/);

expectFailure("duplicate requirement ID", {
  coveragePath: fixture("duplicate-id", coverageSource, (value) => {
    value.requirements[1].requirementId = value.requirements[0].requirementId;
  }),
}, /167 unique requirement IDs/);

expectFailure("open without owner", {
  coveragePath: fixture("missing-owner", coverageSource, (value) => {
    const item = value.requirements.find((entry) => entry.status === "Open");
    item.proposedOwnerGroup = "N/A";
  }),
}, /lacks owner/);

expectFailure("positive without evidence", {
  coveragePath: fixture("missing-evidence", coverageSource, (value) => {
    const item = value.requirements.find((entry) => entry.status === "AlreadySatisfied");
    item.evidencePaths = [];
  }),
}, /lacks evidence/);

const exactFixturePath = "scripts/tests/linked-intake-evidence/tuivision-exact.json";
const exact = JSON.parse(fs.readFileSync(path.join(root, exactFixturePath), "utf8"));
const exactManifestText = fs.readFileSync(path.join(root, manifestSource), "utf8");
const exactManifest = JSON.parse(exactManifestText);
const mapping = exact.seriesMapping;
const tuples = exact.dependencyTuples;
const backlog = exact.backlog;

if (awaitDigest(exactManifestText) !== mapping.canonicalManifestSha256 ||
    mapping.canonicalManifestSha256 !== tuples.canonicalManifestSha256 ||
    mapping.canonicalManifestSha256 !== backlog.canonicalManifestSha256) {
  throw new Error("exact fixture does not bind the current canonical manifest");
}
if (mapping.mappings.length !== mapping.expectedSeriesTargetCount ||
    mapping.expectedSeriesTargetCount !== 10 || mapping.expectedActiveCount !== 0 ||
    mapping.expectedArchiveCount !== 38 || tuples.edges.length !== tuples.expectedEdgeCount ||
    tuples.expectedEdgeCount !== 6) {
  throw new Error("exact fixture cardinality differs from the T056 lock");
}

const manifestMapping = exactManifest.orderedTargets.map((target, index) => ({
  position: index + 1,
  status: target.status,
  intakePath: target.path,
  intakeSha256: target.normalizedSha256,
}));
const fixtureMapping = mapping.mappings.map(({position, status, intakePath, intakeSha256}) => ({
  position,
  status,
  intakePath,
  intakeSha256,
}));
if (JSON.stringify(manifestMapping) !== JSON.stringify(fixtureMapping) ||
    JSON.stringify(exactManifest.dependencies) !== JSON.stringify(tuples.edges)) {
  throw new Error("canonical manifest differs from the exact mapping or dependency tuples");
}

for (const item of mapping.mappings) {
  const featureDirectory = item.featurePath.replace(/\/$/, "");
  const featurePath = path.join(root, featureDirectory);
  const proofPath = path.join(root, item.proofPath);
  if (!fs.statSync(featurePath).isDirectory() || awaitDigest(fs.readFileSync(proofPath, "utf8")) !== item.proofSha256) {
    throw new Error(`feature proof differs from the exact fixture: ${item.featurePath}`);
  }
  const state = JSON.parse(fs.readFileSync(proofPath, "utf8"));
  if (state.status !== "Completed" || state.featurePath !== featureDirectory ||
      !state.acceptedArtifacts?.some((artifact) =>
        artifact.path === item.intakePath && artifact.sha256 === item.intakeSha256)) {
    throw new Error(`feature proof does not bind the exact intake: ${item.featurePath}`);
  }
}

const latest = mapping.latestCompletion;
if (latest.position !== 10 || latest.featurePath !== mapping.mappings.at(-1).featurePath ||
    awaitDigest(fs.readFileSync(path.join(root, latest.evidencePath), "utf8")) !== latest.evidenceSha256) {
  throw new Error("Feature 046 is not the separately evidenced latest completion at position 10");
}
const backlogText = fs.readFileSync(path.join(root, backlog.intakePath), "utf8");
if (awaitDigest(backlogText) !== backlog.intakeSha256 || backlog.status !== "DeferredOptional" ||
    backlog.active !== false || !backlogText.includes("`DeferredOptional`") ||
    exactManifest.orderedTargets.some((target) => target.path === backlog.intakePath) ||
    exactManifest.dependencies.some((edge) => edge.from === backlog.intakePath || edge.to === backlog.intakePath)) {
  throw new Error("optional NuGet intake is not exactly separated from the active series");
}
if (tuples.edges.filter((edge) => edge.binding).length !== tuples.expectedBindingTrueCount ||
    tuples.edges.filter((edge) => !edge.binding).length !== tuples.expectedBindingFalseCount) {
  throw new Error("dependency binding cardinality differs from the T056 lock");
}

function relativeDestination(outputPath, targetPath, directory = false) {
  const relative = path.posix.relative(path.posix.dirname(outputPath), targetPath.replace(/\/$/, ""));
  const encoded = relative.split("/").map((part) => [".", ".."].includes(part)
    ? part
    : encodeURIComponent(part)).join("/");
  return directory ? `${encoded}/` : encoded;
}

for (const outputPath of [
  "Lastenheft_Abarbeitungsreihenfolge.md",
  "requirements/intakes/series/tui-vision-delivery/order.md",
]) {
  const document = fs.readFileSync(path.join(root, outputPath), "utf8");
  if (!document.includes("<!-- linked-intake-evidence:begin -->") ||
      !document.includes("<!-- linked-intake-evidence:end -->")) {
    throw new Error(`linked intake projection is missing from ${outputPath}`);
  }
  for (const item of mapping.mappings) {
    const intakeLabel = path.posix.basename(item.intakePath);
    const featurePath = item.featurePath.replace(/\/$/, "");
    const featureLabel = path.posix.basename(featurePath);
    const intakeLink = `[${intakeLabel}](${relativeDestination(outputPath, item.intakePath)})`;
    const featureLink = `[${featureLabel}](${relativeDestination(outputPath, featurePath, true)})`;
    if (!document.includes(`| ${item.position} | ${intakeLink} | \`${item.status}\` | ${featureLink} |`)) {
      throw new Error(`exact active mapping is missing from ${outputPath}: position ${item.position}`);
    }
  }
  for (const edge of tuples.edges) {
    const fromLabel = path.posix.basename(edge.from);
    const fromLink = `[${fromLabel}](${relativeDestination(outputPath, edge.from)})`;
    if (!document.includes(`${fromLink} · \`${edge.kind}\` · binding=\`${edge.binding}\``)) {
      throw new Error(`exact dependency tuple is missing from ${outputPath}: ${edge.from} -> ${edge.to}`);
    }
  }
  const latestPath = latest.featurePath.replace(/\/$/, "");
  const latestLink = `[${path.posix.basename(latestPath)}](${relativeDestination(outputPath, latestPath, true)})`;
  if (!document.includes(`Latest completion: ${latestLink} at position \`10\` (\`Completed\`).`)) {
    throw new Error(`separate Feature 046 recency evidence is missing from ${outputPath}`);
  }
  const backlogLink = `[${path.posix.basename(backlog.intakePath)}](${relativeDestination(outputPath, backlog.intakePath)})`;
  if (!document.includes(`${backlogLink} — lifecycle \`DeferredOptional\`; active=\`false\`.`)) {
    throw new Error(`separate DeferredOptional backlog evidence is missing from ${outputPath}`);
  }
}

fs.rmSync(temp, {recursive: true, force: true});
console.log("requirements/intake positive fixtures PASS (4 cases)");
console.log("requirements/intake negative fixtures PASS (18 cases)");
console.log("TuiVision exact linked-intake fixtures PASS (10 mappings, 6 edges, 1 latest completion, 1 backlog)");
