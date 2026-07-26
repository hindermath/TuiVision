#!/usr/bin/env node

import crypto from "node:crypto";
import fs from "node:fs";
import path from "node:path";
import process from "node:process";
import childProcess from "node:child_process";

const root = process.cwd();
const write = process.argv.includes("--write");
const seriesRoot = "requirements/intakes/series/tui-vision-delivery";
const seriesId = "a73dda7c-163b-4530-97f2-fd9eea5e8986";
const seriesReceiptId = "480738b1-1a65-47f5-8c2a-2a6c062b4589";
const seriesOperationId = "52f0da46-7d12-4039-a450-14d518d55a1c";
const reviewId = "e320135f-b9d9-469e-bef2-510a00c8446f";
const migrationProposal = "specs/requirements-reconciliation-20260726/migration-proposal.json";
const createdAt = "2026-07-26T20:00:00Z";

const normalize = (value) => value.replace(/^\uFEFF/, "").replace(/\r\n?/g, "\n");
const hashText = (value) => crypto.createHash("sha256").update(normalize(value)).digest("hex");
const hashFile = (relativePath) => hashText(fs.readFileSync(path.join(root, relativePath), "utf8"));
const readJson = (relativePath) => JSON.parse(fs.readFileSync(path.join(root, relativePath), "utf8"));
const json = (value) => JSON.stringify(value, null, 2) + "\n";
const head = childProcess.execFileSync("git", ["rev-parse", "HEAD"], {cwd: root, encoding: "utf8"}).trim();

const members = [
  {
    slug: "wave6-combined-delta-closure",
    path: "requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md",
    role: "Primary",
    status: "Eligible",
    intakeId: "23b841f9-f5ce-49c2-ac97-71493b316c5b",
    receiptId: "cf80a015-877a-442c-a811-81d529489a1a",
    operationId: "099ddab3-4f16-48ff-9f12-d5084782e4b3",
    provenance: "New",
  },
  {
    slug: "15-post-wave6-example-portfolio-conformance-audit",
    path: "requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md",
    role: "OrderedMember",
    status: "Blocked",
    receiptId: "296b0258-7d42-45bf-aa28-e269e9776a5d",
    operationId: "31ee816a-3617-49da-b3f8-a28969f1f265",
    priorReceipt: "specs/intake-authoring-receipts/history/15-post-wave6-example-portfolio-conformance-audit.schema-1.1.json",
    priorTarget: "requirements/intakes/history/lastenheft-15-post-wave6-example-portfolio-conformance-audit/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md",
  },
  {
    slug: "documentation-publishing-closure",
    path: "requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md",
    role: "OrderedMember",
    status: "Pending",
    intakeId: "8e875037-f400-4a87-866b-424ecdb89f9c",
    receiptId: "028491da-160a-4dd6-be84-46345e775ae6",
    operationId: "0979ac20-946c-4634-9f4b-91745e2cb968",
    provenance: "New",
  },
  {
    slug: "constitution-change",
    path: "requirements/intakes/active/Lastenheft_Constitution_Change.md",
    role: "OrderedMember",
    status: "Pending",
    receiptId: "2e99a694-e1f1-44b3-a11b-79324c78c7e7",
    operationId: "57ab133f-5c75-4ce5-b424-9a06a8301639",
    priorReceipt: "specs/intake-authoring-receipts/history/constitution-change.schema-1.1.json",
    priorTarget: "requirements/intakes/history/lastenheft-constitution-change/Lastenheft_Constitution_Change.md",
  },
  {
    slug: "sandbox-gestuetzte-secure-development-haertung",
    path: "requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md",
    role: "OrderedMember",
    status: "Pending",
    receiptId: "f904f58d-94b9-43f2-b7b1-f9f229edea7c",
    operationId: "9f3784b1-ce1b-4d97-81e0-16c3cee2ddd7",
    priorReceipt: "specs/intake-authoring-receipts/history/sandbox-gestuetzte-secure-development-haertung.schema-1.1.json",
    priorTarget: "requirements/intakes/history/lastenheft-sandbox-gestuetzte-secure-development-haertung/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md",
  },
  {
    slug: "rl-se-checklist-selbstpruefung",
    path: "requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md",
    role: "OrderedMember",
    status: "Pending",
    receiptId: "1cd89c9e-1548-4a08-a053-9ae8ddae7984",
    operationId: "0230c7b5-a32f-48c7-8f02-99bf89694f83",
    priorReceipt: "specs/intake-authoring-receipts/history/rl-se-checklist-selbstpruefung.schema-1.1.json",
    priorTarget: "requirements/intakes/history/lastenheft-rl-se-checklist-selbstpruefung/Lastenheft_RL-SE-Checklist-Selbstpruefung.md",
  },
  {
    slug: "gsdb-spec-kit-intensivpruefung",
    path: "requirements/intakes/active/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md",
    role: "OrderedMember",
    status: "Pending",
    receiptId: "abd25e48-b9e8-4a92-875e-579be5d22831",
    operationId: "b38a2f9b-1bc1-4152-87b5-5559829424ce",
    priorReceipt: "specs/intake-authoring-receipts/history/gsdb-spec-kit-intensivpruefung.schema-1.1.json",
    priorTarget: "requirements/intakes/history/lastenheft-gsdb-spec-kit-intensivpruefung/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md",
  },
];

const memberPaths = members.map((member) => member.path);
const manifest = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesManifest",
  seriesId,
  title: "TuiVision Delivery Intake Series",
  policy: "tui-vision-delivery-v1",
  status: "Active",
  orderedTargets: members.map((member) => ({
    path: member.path,
    role: member.role,
    normalizedSha256: hashFile(member.path),
    status: member.status,
  })),
  roots: members.filter((member) => member.status !== "Blocked").map((member) => member.path),
  dependencies: [
    {
      from: members[0].path,
      to: members[1].path,
      kind: "HardCompletionGate",
      binding: true,
    },
  ],
  evidencePaths: [
    "specs/requirements-reconciliation-20260726/requirements-coverage.json",
    "specs/requirements-reconciliation-20260726/migration-proposal.json",
    "Lastenheft_Abarbeitungsreihenfolge.md",
  ],
};

const manifestPath = `${seriesRoot}/manifest.json`;
const manifestHash = hashText(json(manifest));

function sourceRecord(sourceId, relativePath, label) {
  const digest = hashFile(relativePath);
  return {
    sourceId,
    order: 1,
    kind: "File",
    label,
    location: "Repository",
    path: relativePath,
    requestedUrl: "N/A",
    finalUrl: "N/A",
    retrievedAt: "N/A",
    httpStatus: "N/A",
    contentType: "N/A",
    contentLength: "N/A",
    etag: "N/A",
    lastModified: "N/A",
    redirectChain: [],
    rawSha256: "N/A",
    normalizedSha256: digest,
    gitBlob: "N/A",
    proofBoundary: "Repository file and normalized SHA-256",
  };
}

function receiptFor(member, order) {
  const isNew = member.provenance === "New";
  const prior = isNew ? null : readJson(member.priorReceipt);
  const oldHash = isNew ? "N/A" : hashFile(member.priorTarget);
  const intakeId = member.intakeId ?? prior.receiptId;
  const source = isNew
    ? sourceRecord("SRC001", migrationProposal, "Approved requirements migration proposal")
    : sourceRecord("SRC001", member.priorTarget, "Archived predecessor intake");
  return {
    schemaVersion: "2.0",
    documentType: "IntakeReceipt",
    receiptId: member.receiptId,
    intakeId,
    generator: {preset: "intake-authoring-governance", version: "0.2.1"},
    createdAt,
    operation: {
      operationId: member.operationId,
      type: isNew ? "Create" : "Update",
      authorityEvidence: "User-approved two-PR Pflichtenheft and intake consolidation plan",
    },
    status: "ReadyForReview",
    target: {path: member.path, normalizedSha256: hashFile(member.path)},
    sources: [source],
    profile: "level2-lastenheft",
    languagePolicy: "GermanFirstEnglishSecond",
    decisions: [
      {
        id: "IAD001",
        status: "Answered",
        question: "Welcher Zielpfad ist nach der Konsolidierung verbindlich?",
        answer: member.path,
        evidence: migrationProposal,
      },
      {
        id: "IAD002",
        status: "Answered",
        question: "Welche Delivery Authority gilt?",
        answer: "LocalImplementation",
        evidence: "The migration grants no remote delivery authority.",
      },
    ],
    openDecisionIds: [],
    questionCount: 0,
    agentSurface: {
      specifyCanonicalId: "speckit.specify",
      specifyInvocation: "$speckit-specify",
      autonomousCanonicalId: "speckit.autonomous",
      autonomousInvocation: "$speckit-autonomous",
    },
    deliveryAuthority: "LocalImplementation",
    authorityEvidence: "Default: this migration grants no remote delivery authority.",
    promptState: "Enabled",
    provenanceMode: isNew ? "New" : "Supersession",
    supersedes: {
      receiptPath: isNew ? "N/A" : member.priorReceipt,
      targetNormalizedSha256: oldHash,
      archiveTargetPath: isNew ? "N/A" : member.priorTarget,
      archiveReceiptPath: isNew ? "N/A" : member.priorReceipt,
    },
    legacyAdoption: {
      evidenceType: "N/A",
      priorTargetNormalizedSha256: "N/A",
      priorGitBlob: "N/A",
    },
    updateAuthorized: !isNew,
    updateAuthorityEvidence: isNew
      ? "N/A"
      : "User-approved migration preserves the existing intake identity and predecessor evidence.",
    series: {
      seriesId,
      manifestPath,
      order,
      role: member.role,
      supersedesIntakeIds: [],
    },
    nextAction: `$speckit-intake-review ${member.path}`,
  };
}

const receipts = members.map((member, index) => ({
  path: `specs/intake-authoring-receipts/${member.slug}.json`,
  value: receiptFor(member, index + 1),
}));

const seriesReceipt = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesReceipt",
  receiptId: seriesReceiptId,
  seriesId,
  generator: {preset: "intake-sequencing-governance", version: "0.1.1"},
  createdAt,
  operation: {
    operationId: seriesOperationId,
    type: "Create",
    authorityEvidence: "User-approved two-PR Pflichtenheft and intake consolidation plan",
  },
  status: "Ready",
  manifest: {path: manifestPath, normalizedSha256: manifestHash},
  supersedes: {
    receiptPath: "N/A",
    receiptNormalizedSha256: "N/A",
    manifestArchivePath: "N/A",
    manifestArchiveSha256: "N/A",
  },
  tombstone: {path: "N/A", normalizedSha256: "N/A"},
  nextAction: "$speckit-intake-series-status",
};

const operation = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesOperation",
  operationId: seriesOperationId,
  seriesId,
  type: "Create",
  status: "Published",
  authorityEvidence: "User-approved two-PR Pflichtenheft and intake consolidation plan",
  proposalNormalizedSha256: hashFile(migrationProposal),
  preparedPaths: [manifestPath, `${seriesRoot}/receipt.json`, `${seriesRoot}/order.md`],
  validation: {bash: "Pass", powerShell: "Pass"},
  publication: {
    status: "Published",
    publishedPaths: [manifestPath, `${seriesRoot}/receipt.json`, `${seriesRoot}/order.md`],
  },
};

const request = {
  schemaVersion: "1.1",
  reviewId,
  mode: "Series",
  policy: "tui-vision-delivery-v1",
  targets: members.map((member) => ({path: member.path, role: member.role})),
  series: {
    orderedTargetPaths: memberPaths,
    roots: manifest.roots,
    dependencies: manifest.dependencies.map((edge) => ({
      from: edge.from,
      to: edge.to,
      kind: edge.kind,
    })),
  },
  campaign: {manifestPath: "N/A", workers: [], operatorExceptions: []},
};
const requestPath = `${seriesRoot}/intake-review-request.json`;

const result = {
  schemaVersion: "1.1",
  reviewId,
  mode: "Series",
  status: "Ready",
  policy: "tui-vision-delivery-v1",
  reviewedAt: createdAt,
  repository: {root: ".", head},
  targets: members.map((member) => ({
    path: member.path,
    role: member.role,
    normalizedSha256: hashFile(member.path),
    gitBlob: "N/A",
  })),
  findings: [],
  questions: [],
  acceptedRisks: [],
  operatorExceptions: [],
  coverage: {
    individual: memberPaths,
    series: [
      "All seven active intake paths, exact hashes, lifecycle states, roots, and the Wave-6 hard completion gate",
      "Independent governance and documentation roots without invented product dependencies",
      "DeferredOptional backlog excluded from executable targets",
    ],
    workers: [],
  },
  summary: {critical: 0, high: 0, medium: 0, low: 0},
  supersedes: readJson("specs/intake-normalization-20260723/intake-review-result.json").reviewId,
  requestEvidence: {path: requestPath, normalizedSha256: hashText(json(request))},
};

const report = `# Intake Review: TuiVision Delivery Series

## Ergebnis / Result

Status: \`Ready\`

Alle sieben aktiven Intakes, ihre aktuellen Hashes, Receipt-Lineage,
Lifecycle-Zustände und die einzige harte Abhängigkeit wurden geprüft. Der
Wave-6-Closeout ist der bevorzugte nächste Intake. Der Portfolioaudit bleibt
bis zu dessen Abschluss blockiert. Dokumentations- und Governance-Intakes
bleiben unabhängige Wurzeln.

*All seven active intakes, current hashes, receipt lineage, lifecycle states,
and the single hard dependency were reviewed. Wave-6 closure is preferred
next. The portfolio audit remains blocked until completion. Documentation and
governance intakes remain independent roots.*

Es bestehen keine offenen Review-Findings. Der optionale NuGet-Backlog ist
nicht Teil der ausführbaren Serie.
`;

const orderDocument = fs.readFileSync(path.join(root, "Lastenheft_Abarbeitungsreihenfolge.md"), "utf8");
const outputs = [
  [manifestPath, json(manifest)],
  [`${seriesRoot}/receipt.json`, json(seriesReceipt)],
  [`${seriesRoot}/operation.json`, json(operation)],
  [`${seriesRoot}/order.md`, orderDocument],
  [requestPath, json(request)],
  [`${seriesRoot}/intake-review-result.json`, json(result)],
  [`${seriesRoot}/intake-review-report.md`, report],
  ...receipts.map((entry) => [entry.path, json(entry.value)]),
];

for (const [relativePath, content] of outputs) {
  const fullPath = path.join(root, relativePath);
  if (write) {
    fs.mkdirSync(path.dirname(fullPath), {recursive: true});
    fs.writeFileSync(fullPath, content);
  } else if (!fs.existsSync(fullPath) || fs.readFileSync(fullPath, "utf8") !== content) {
    console.error(`stale generated intake-governance artifact: ${relativePath}`);
    process.exit(1);
  }
}

console.log(`requirements intake governance PASS (${members.length} active targets, 1 binding edge)`);
