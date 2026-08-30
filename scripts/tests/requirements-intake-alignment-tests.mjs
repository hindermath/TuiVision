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

const pending = pendingIntakeFixture("valid-pending");
expectSuccess("valid authored pending intake", pending);

const activeFeatureDirectory = path.join(temp, "043-documentation-publishing-closure");
fs.mkdirSync(activeFeatureDirectory, {recursive: true});
const activeFeaturePath = "specs/043-documentation-publishing-closure";
const bindingIntake =
  "requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md";
const bindingHash = awaitDigest(fs.readFileSync(path.join(root, bindingIntake), "utf8"));
fs.writeFileSync(
  path.join(activeFeatureDirectory, "spec.md"),
  `# Authorized Feature\n\n**Binding Intake**: \`${bindingIntake}\`\n`);
fs.writeFileSync(
  path.join(activeFeatureDirectory, "autonomous-run-state.json"),
  JSON.stringify({
    featurePath: activeFeaturePath,
    branch: "043-documentation-publishing-closure",
    status: "Completed",
    acceptedArtifacts: [{path: bindingIntake, sha256: bindingHash}],
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
}, /series must contain exactly 10 unique active targets/);

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
}, /unique active targets/);

expectFailure("backlog target", {
  manifestPath: fixture("backlog-target", manifestSource, (value) => {
    value.orderedTargets[6].path =
      "requirements/intakes/backlog/Lastenheft_Optional-NuGet-Package.md";
  }),
}, /missing from the active intake directory|archive or backlog/);

expectFailure("unexpected eligible", {
  manifestPath: fixture("unexpected-eligible", manifestSource, (value) => {
    value.orderedTargets.at(-1).status = "Eligible";
  }),
}, /must not expose an Eligible target/);

expectFailure("incomplete Wave-6 closure", {
  manifestPath: fixture("incomplete-wave6", manifestSource, (value) => {
    value.orderedTargets[0].status = "Pending";
  }),
}, /Wave-6 closure must remain Completed|declared Eligible target still has a binding blocker/);

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
}, /missing from the active intake directory|target is missing/);

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

fs.rmSync(temp, {recursive: true, force: true});
console.log("requirements/intake positive fixtures PASS (3 cases)");
console.log("requirements/intake negative fixtures PASS (17 cases)");
