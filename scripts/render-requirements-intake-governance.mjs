#!/usr/bin/env node

import crypto from "node:crypto";
import fs from "node:fs";
import path from "node:path";
import process from "node:process";

const root = process.cwd();
const write = process.argv.includes("--write");
const seriesRoot = "requirements/intakes/series/tui-vision-delivery";
const seriesId = "a73dda7c-163b-4530-97f2-fd9eea5e8986";
const seriesReceiptId = "ddd6488a-9cf3-4d9f-9b76-8cb301bdb031";
const seriesOperationId = "7a6a7aa1-2be3-43ff-8359-54f952ecf62d";
const reviewId = "847bce5c-98b0-4461-b2a7-c1b5bc9d83dc";
const migrationProposal = "specs/requirements-reconciliation-20260726/migration-proposal.json";
const createdAt = "2026-07-26T20:00:00Z";
const seriesUpdatedAt = "2026-08-29T21:23:35Z";
const archiveRoot =
  `specs/intake-series-archive/${seriesId}/${seriesOperationId}`;
const priorManifestHash = "fb889c87f8cae33e20bb826e1dfb926c9e5655dc4d626aabfc8605155fb552b7";
const priorReceiptHash = "b6adaa7018e2a5d56baf737f30e8fb2be4b39b8072d265b0047ab0475991ccfb";

const normalize = (value) => value.replace(/^\uFEFF/, "").replace(/\r\n?/g, "\n");
const hashText = (value) => crypto.createHash("sha256").update(normalize(value)).digest("hex");
const hashFile = (relativePath) => hashText(fs.readFileSync(path.join(root, relativePath), "utf8"));
const readJson = (relativePath) => JSON.parse(fs.readFileSync(path.join(root, relativePath), "utf8"));
const json = (value) => JSON.stringify(value, null, 2) + "\n";
const reviewHead = "1f5890767063dcebbe363fb8087e4fb89a880af1";

const members = [
  {
    slug: "wave6-combined-delta-closure",
    path: "requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md",
    role: "OrderedMember",
    status: "Completed",
  },
  {
    slug: "15-post-wave6-example-portfolio-conformance-audit",
    path: "requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md",
    role: "OrderedMember",
    status: "Completed",
  },
  {
    slug: "example-portfolio-closure",
    path: "requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md",
    role: "Primary",
    status: "Completed",
  },
  {
    slug: "constitution-change",
    path: "requirements/intakes/active/Lastenheft_Constitution_Change.md",
    role: "OrderedMember",
    status: "Completed",
  },
  {
    slug: "source-reference-policy",
    path: "requirements/intakes/active/Lastenheft_Source-Reference-Policy.md",
    role: "OrderedMember",
    status: "Completed",
  },
  {
    slug: "transactional-form-model",
    path: "requirements/intakes/active/Lastenheft_Transactional-Form-Model.md",
    role: "OrderedMember",
    status: "Completed",
  },
  {
    slug: "documentation-publishing-closure",
    path: "requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md",
    role: "OrderedMember",
    status: "Completed",
  },
  {
    slug: "sandbox-gestuetzte-secure-development-haertung",
    path: "requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md",
    role: "OrderedMember",
    status: "Eligible",
  },
  {
    slug: "rl-se-checklist-selbstpruefung",
    path: "requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md",
    role: "OrderedMember",
    status: "Pending",
  },
  {
    slug: "gsdb-spec-kit-intensivpruefung",
    path: "requirements/intakes/active/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md",
    role: "OrderedMember",
    status: "Pending",
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
  roots: [members[0].path, members[3].path, members[7].path, members[8].path, members[9].path],
  dependencies: [
    {
      from: members[0].path,
      to: members[1].path,
      kind: "HardCompletionGate",
      binding: true,
    },
    {
      from: members[1].path,
      to: members[2].path,
      kind: "HardCompletionGate",
      binding: true,
    },
    {
      from: members[3].path,
      to: members[4].path,
      kind: "SharedWriterSerialization",
      binding: false,
    },
    {
      from: members[4].path,
      to: members[5].path,
      kind: "HardCompletionGate",
      binding: true,
    },
    {
      from: members[2].path,
      to: members[5].path,
      kind: "HardCompletionGate",
      binding: true,
    },
    {
      from: members[5].path,
      to: members[6].path,
      kind: "PreferredSerialOrder",
      binding: false,
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
      role: member.receiptRole ?? member.role,
      supersedesIntakeIds: [],
    },
    nextAction: `$speckit-intake-review ${member.path}`,
  };
}

const seriesReceipt = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesReceipt",
  receiptId: seriesReceiptId,
  seriesId,
  generator: {preset: "intake-sequencing-governance", version: "0.2.3"},
  createdAt: "2026-08-28T22:59:38Z",
  operation: {
    operationId: seriesOperationId,
    type: "Update",
    authorityEvidence: "Feature 043 MergeAndSync authority and causal closeout for the completed documentation intake",
  },
  status: "Ready",
  manifest: {path: manifestPath, normalizedSha256: manifestHash},
  supersedes: {
    receiptPath: `${archiveRoot}/receipt.json`,
    receiptNormalizedSha256: priorReceiptHash,
    manifestArchivePath: `${archiveRoot}/manifest.json`,
    manifestArchiveSha256: priorManifestHash,
  },
  tombstone: {path: "N/A", normalizedSha256: "N/A"},
  nextAction: "$speckit-intake-series-status",
};

const operation = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesOperation",
  operationId: seriesOperationId,
  seriesId,
  type: "Update",
  status: "Published",
  authorityEvidence: "Feature 043 MergeAndSync authority and causal closeout for the completed documentation intake",
  proposalNormalizedSha256: manifestHash,
  preparedPaths: [
    `${archiveRoot}/manifest.json`,
    `${archiveRoot}/receipt.json`,
    manifestPath,
    `${seriesRoot}/receipt.json`,
    `${seriesRoot}/order.md`,
  ],
  validation: {bash: "Pass", powerShell: "Pass"},
  publication: {
    status: "Published",
    publishedPaths: [
      `${archiveRoot}/manifest.json`,
      `${archiveRoot}/receipt.json`,
      manifestPath,
      `${seriesRoot}/receipt.json`,
      `${seriesRoot}/order.md`,
    ],
  },
};

const request = {
  schemaVersion: "1.1",
  reviewId,
  mode: "Series",
  policy: "tui-vision-lastenheft",
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
  policy: "tui-vision-lastenheft",
  reviewedAt: seriesUpdatedAt,
  repository: {root: ".", head: reviewHead},
  requestEvidence: {path: requestPath, normalizedSha256: hashText(json(request))},
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
    series: [manifestPath],
    workers: [],
  },
  summary: {critical: 0, high: 0, medium: 0, low: 0},
  supersedes: "6b74e8e5-c605-48c5-b450-1a018b5dd7eb",
};

const report = `# Intake-Serienreview: TuiVision Delivery nach Dokumentationsabschluss

## Ergebnis / Result

Status: \`Ready\`

Alle zehn Serien-Intakes, ihre aktuellen Hashes, Receipt-Lineage, fünf Wurzeln
und sechs azyklischen Abhängigkeiten wurden geprüft. Die
Documentation-Publishing-Closure ist durch Feature 043 abgeschlossen. Der
unabhängige Sandbox-Security-Intake ist als einziger Eintrag \`Eligible\`;
kein Folgefeature wird durch diesen Review gestartet.

*All ten series intakes, current hashes, receipt lineage, five roots, and six
acyclic dependencies were reviewed. Documentation Publishing Closure is
complete through Feature 043. The independent sandbox security intake is the
only \`Eligible\` entry; this review starts no follow-up feature.*

Es bestehen keine offenen Review-Findings. Der optionale NuGet-Backlog ist
nicht Teil der ausführbaren Serie.
`;

const orderDocument = normalize(
  fs.readFileSync(path.join(root, "Lastenheft_Abarbeitungsreihenfolge.md"), "utf8"),
);
const outputs = [
  [manifestPath, json(manifest)],
  [`${seriesRoot}/receipt.json`, json(seriesReceipt)],
  [`${seriesRoot}/operation.json`, json(operation)],
  [`${seriesRoot}/order.md`, orderDocument],
  [requestPath, json(request)],
  [`${seriesRoot}/intake-review-result.json`, json(result)],
  [`${seriesRoot}/intake-review-report.md`, report],
];

for (const [relativePath, content] of outputs) {
  const fullPath = path.join(root, relativePath);
  if (write) {
    fs.mkdirSync(path.dirname(fullPath), {recursive: true});
    fs.writeFileSync(fullPath, content);
  } else if (!fs.existsSync(fullPath) ||
             normalize(fs.readFileSync(fullPath, "utf8")) !== normalize(content)) {
    console.error(`stale generated intake-governance artifact: ${relativePath}`);
    process.exit(1);
  }
}

console.log(`requirements intake governance PASS (${members.length} series targets, ${manifest.dependencies.length} dependencies)`);
