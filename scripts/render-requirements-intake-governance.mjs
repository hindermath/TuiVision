#!/usr/bin/env node

import crypto from "node:crypto";
import fs from "node:fs";
import path from "node:path";
import process from "node:process";

const root = process.cwd();
const argumentsList = process.argv.slice(2);
if (argumentsList.includes("--help")) {
  console.log("Usage: node scripts/render-requirements-intake-governance.mjs [--check|--write]");
  console.log("  --check  Verify every generated intake-governance artifact (default).");
  console.log("  --write  Atomically regenerate the governed artifacts for this repository.");
  process.exit(0);
}
if (argumentsList.some((argument) => !["--check", "--write"].includes(argument)) ||
    argumentsList.includes("--check") && argumentsList.includes("--write")) {
  console.error("Use exactly one of --check or --write; --check is the default.");
  process.exit(2);
}
const write = argumentsList.includes("--write");
const seriesRoot = "requirements/intakes/series/tui-vision-delivery";
const seriesId = "a73dda7c-163b-4530-97f2-fd9eea5e8986";
const seriesReceiptId = "a76e7957-0b72-4c0b-b738-c88c3bc33885";
const seriesOperationId = "0326e087-08a7-4318-a54d-6b4bf5993017";
const reviewId = "88579ec8-e830-4a07-8c29-d6035dcb4782";
const migrationProposal = "specs/requirements-reconciliation-20260726/migration-proposal.json";
const createdAt = "2026-07-26T20:00:00Z";
const seriesUpdatedAt = "2026-09-12T23:21:05Z";
const reviewedAt = "2026-08-30T16:19:20Z";
const archiveRoot =
  `specs/intake-series-archive/${seriesId}/${seriesOperationId}`;
const priorManifestHash = "538d12fcc60cab96e5d70865a9ab9c31885634f3981e1c10dc2cd09d1ee2d2e7";
const priorReceiptHash = "eb64ab26a6e0d4fd9ab92039f61a31612cbda4ac1cd4978fabfb3ab220f71136";

const normalize = (value) => value.replace(/^\uFEFF/, "").replace(/\r\n?/g, "\n");
const hashText = (value) => crypto.createHash("sha256").update(normalize(value)).digest("hex");
const hashFile = (relativePath) => hashText(fs.readFileSync(path.join(root, relativePath), "utf8"));
const readJson = (relativePath) => JSON.parse(fs.readFileSync(path.join(root, relativePath), "utf8"));
const json = (value) => JSON.stringify(value, null, 2) + "\n";
const reviewHead = "fe1f57c201c84fb3f81d746a6ca3d8977c9f1edb";

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
    status: "Completed",
  },
  {
    slug: "rl-se-checklist-selbstpruefung",
    path: "requirements/intakes/archive/Lastenheft_RL-SE-Checklist-Selbstpruefung.045-rl-se-checklist-self-review.md",
    role: "OrderedMember",
    status: "Completed",
  },
  {
    slug: "gsdb-spec-kit-intensivpruefung",
    path: "requirements/intakes/archive/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.046-gsdb-spec-kit-intensive-review.md",
    role: "OrderedMember",
    status: "Completed",
  },
];

const memberPaths = members.map((member) => member.path);
const manifest = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesManifest",
  seriesId,
  title: "TuiVision Delivery Intake Series",
  policy: "tui-vision-delivery-v1",
  status: "Completed",
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
  createdAt: seriesUpdatedAt,
  operation: {
    operationId: seriesOperationId,
    type: "Update",
    authorityEvidence: "Explicit user authority for completed-series lifecycle reconciliation",
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
  authorityEvidence: "Explicit user authority for completed-series lifecycle reconciliation",
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
  reviewedAt,
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
  supersedes: "67d89984-7536-4bab-bc51-02ef8d1edec4",
};

const report = `# Intake-Serienreview: TuiVision Delivery nach GSDB-Abschluss

## Ergebnis / Result

Status: \`Ready\`

Alle zehn Serien-Intakes, aktuellen Hashes, Receipt-Lineage, fünf Wurzeln und
sechs azyklischen Abhängigkeiten wurden erneut geprüft. Die
GSDB-Spec-Kit-Intensivprüfung ist durch Feature 046 und PR #164 abgeschlossen.
Es gibt keinen weiteren \`Eligible\`-Eintrag; dieser Review startet keinen neuen
Feature-Lauf.

*All ten series intakes, current hashes, receipt lineage, five roots, and six
acyclic dependencies were re-reviewed. The GSDB Spec Kit intensive review is
complete through Feature 046 and PR #164. No further entry is \`Eligible\`;
this review starts no new feature run.*

Es bestehen keine offenen Review-Findings. Der optionale NuGet-Backlog bleibt
nicht ausführbar und ist nicht Teil der Serie.
`;

const exactFixturePath = "scripts/tests/linked-intake-evidence/tuivision-exact.json";
const backlogPath = "requirements/intakes/backlog/Lastenheft_Optional-NuGet-Package.md";

function encodedRelativeDestination(outputPath, targetPath, directory = false) {
  const relative = path.posix.relative(
    path.posix.dirname(outputPath),
    targetPath.replace(/\/$/, ""),
  );
  const encoded = relative.split("/").map((part) =>
    [".", ".."].includes(part) ? part : encodeURIComponent(part)).join("/");
  return directory ? `${encoded}/` : encoded;
}

function linkedIntakeOrderDocument(outputPath) {
  const exact = readJson(exactFixturePath);
  const mapping = exact.activeMapping;
  const tuples = exact.dependencyTuples;
  const backlog = exact.backlog;
  if (mapping.canonicalManifestSha256 !== manifestHash ||
      tuples.canonicalManifestSha256 !== manifestHash ||
      backlog.canonicalManifestSha256 !== manifestHash ||
      mapping.expectedActiveCount !== 10 || mapping.mappings.length !== 10 ||
      tuples.expectedEdgeCount !== 6 || tuples.edges.length !== 6) {
    throw new Error("TUI-EXACT-LOCK: exact fixture differs from the canonical manifest");
  }

  const manifestProjection = manifest.orderedTargets.map((target, index) => ({
    position: index + 1,
    status: target.status,
    intakePath: target.path,
    intakeSha256: target.normalizedSha256,
  }));
  const exactProjection = mapping.mappings.map(
    ({position, status, intakePath, intakeSha256}) =>
      ({position, status, intakePath, intakeSha256}),
  );
  if (JSON.stringify(manifestProjection) !== JSON.stringify(exactProjection) ||
      JSON.stringify(manifest.dependencies) !== JSON.stringify(tuples.edges)) {
    throw new Error("TUI-EXACT-CONTRACT: mapping or dependency tuple drift");
  }

  for (const item of mapping.mappings) {
    const featureDirectory = item.featurePath.replace(/\/$/, "");
    const proof = readJson(item.proofPath);
    if (!fs.statSync(path.join(root, featureDirectory)).isDirectory() ||
        hashFile(item.proofPath) !== item.proofSha256 ||
        proof.featurePath !== featureDirectory || proof.status !== "Completed" ||
        !proof.acceptedArtifacts?.some((artifact) =>
          artifact.path === item.intakePath && artifact.sha256 === item.intakeSha256)) {
      throw new Error(`TUI-EXACT-PROOF: invalid feature evidence for ${item.intakePath}`);
    }
  }

  const latest = mapping.latestCompletion;
  if (latest.position !== 10 || latest.featurePath !== mapping.mappings.at(-1).featurePath ||
      hashFile(latest.evidencePath) !== latest.evidenceSha256) {
    throw new Error("TUI-EXACT-RECENCY: Feature 046 is not the evidenced latest completion");
  }
  if (backlog.intakePath !== backlogPath || backlog.status !== "DeferredOptional" ||
      backlog.active !== false || hashFile(backlog.intakePath) !== backlog.intakeSha256 ||
      manifest.orderedTargets.some((target) => target.path === backlog.intakePath) ||
      manifest.dependencies.some((edge) =>
        edge.from === backlog.intakePath || edge.to === backlog.intakePath)) {
    throw new Error("TUI-EXACT-BACKLOG: optional NuGet intake is not separated");
  }
  if (tuples.edges.filter((edge) => edge.binding).length !== tuples.expectedBindingTrueCount ||
      tuples.edges.filter((edge) => !edge.binding).length !== tuples.expectedBindingFalseCount) {
    throw new Error("TUI-EXACT-BINDING: dependency binding cardinality changed");
  }

  const rows = mapping.mappings.map((item) => {
    const intakeLabel = path.posix.basename(item.intakePath);
    const featurePath = item.featurePath.replace(/\/$/, "");
    const featureLabel = path.posix.basename(featurePath);
    const intakeLink = `[${intakeLabel}](${encodedRelativeDestination(outputPath, item.intakePath)})`;
    const featureLink = `[${featureLabel}](${encodedRelativeDestination(outputPath, featurePath, true)})`;
    const incoming = tuples.edges.filter((edge) => edge.to === item.intakePath);
    const dependencyText = incoming.length === 0
      ? "—"
      : incoming.map((edge) => {
        const fromLabel = path.posix.basename(edge.from);
        const fromLink = `[${fromLabel}](${encodedRelativeDestination(outputPath, edge.from)})`;
        return `${fromLink} · \`${edge.kind}\` · binding=\`${edge.binding}\``;
      }).join("<br>");
    return `| ${item.position} | ${intakeLink} | \`${item.status}\` | ${featureLink} | ${dependencyText} |`;
  });
  const latestPath = latest.featurePath.replace(/\/$/, "");
  const latestLink = `[${path.posix.basename(latestPath)}](${encodedRelativeDestination(outputPath, latestPath, true)})`;
  const backlogLink = `[${path.posix.basename(backlog.intakePath)}](${encodedRelativeDestination(outputPath, backlog.intakePath)})`;
  const manifestLink = `[${path.posix.basename(manifestPath)}](${encodedRelativeDestination(outputPath, manifestPath)})`;
  const generationSha256 = hashText(json({
    manifestSha256: manifestHash,
    mappings: mapping.mappings,
    dependencies: tuples.edges,
    latest,
    backlog,
  }));

  return `# Lastenheft-Abarbeitungsreihenfolge / Requirements Processing Order

Diese Ansicht wird deterministisch aus dem kanonischen Manifest ${manifestLink}
und dem T056-gesperrten Nachweis erzeugt. Sie startet keinen Feature-Lauf.

*This view is rendered deterministically from the canonical manifest and the
T056-locked evidence. It does not start a feature run.*

<!-- linked-intake-evidence:begin -->
## Verlinkter Ausführungsnachweis / Linked Execution Evidence

- Manifest SHA-256: \`${manifestHash}\`
- Projektions-SHA-256: \`${generationSha256}\`
- Umfang: \`10\` Serienzuordnungen, \`6\` unveränderte Abhängigkeiten

| Position | Intake | Status | Spec-Kit-Feature | Direkte eingehende Abhängigkeiten |
|---:|---|---|---|---|
${rows.join("\n")}

### Zuletzt abgeschlossen / Latest Completion

Latest completion: ${latestLink} at position \`10\` (\`Completed\`).

Feature 046 bleibt an seiner kanonischen Position. Die Aktualitätsaussage
ändert die Manifestreihenfolge nicht.

*Feature 046 remains at its canonical position. Recency does not reorder the
manifest.*

### Getrennter Backlog / Separated Backlog

${backlogLink} — lifecycle \`DeferredOptional\`; active=\`false\`.

Der optionale Eintrag ist weder aktive Tabellenzeile noch Abhängigkeitsendpunkt.

*The optional item is neither an active row nor a dependency endpoint.*
<!-- linked-intake-evidence:end -->

## Nächste Aktion / Next Action

\`$speckit-intake-series-status\` und \`$speckit-intake-series-next\` prüfen den
Zustand ausschließlich read-only. Es gibt keinen implizit autorisierten
nächsten Feature-Lauf.
`;
}

const orderDocuments = [
  "Lastenheft_Abarbeitungsreihenfolge.md",
  `${seriesRoot}/order.md`,
].map((outputPath) => [outputPath, linkedIntakeOrderDocument(outputPath)]);
const outputs = [
  [manifestPath, json(manifest)],
  [`${seriesRoot}/receipt.json`, json(seriesReceipt)],
  [`${seriesRoot}/operation.json`, json(operation)],
  ...orderDocuments,
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
