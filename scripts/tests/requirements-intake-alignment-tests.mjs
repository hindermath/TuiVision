#!/usr/bin/env node

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

if (validate({root}).length !== 0) throw new Error("positive fixture failed");

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
}, /directory and series targets differ|archive or backlog/);

expectFailure("missing eligible", {
  manifestPath: fixture("missing-eligible", manifestSource, (value) => {
    value.orderedTargets[0].status = "Pending";
  }),
}, /single explicitly Eligible/);

expectFailure("stale target hash", {
  manifestPath: fixture("stale-hash", manifestSource, (value) => {
    value.orderedTargets[0].normalizedSha256 = "0".repeat(64);
  }),
}, /target hash drift/);

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
}, /exactly the Wave-6-to-portfolio hard gate|cycle/);

expectFailure("dangling target", {
  manifestPath: fixture("dangling-target", manifestSource, (value) => {
    value.orderedTargets[6].path = "requirements/intakes/active/Missing.md";
  }),
}, /directory and series targets differ|target is missing/);

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
console.log("requirements/intake negative fixtures PASS (9 cases)");
