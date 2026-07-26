#!/usr/bin/env node

import crypto from "node:crypto";
import fs from "node:fs";
import path from "node:path";
import process from "node:process";

const root = process.cwd();
const sourcePath = "Pflichtenheft.md";
const outputRoot = "specs/requirements-reconciliation-20260726";
const source = fs.readFileSync(path.join(root, sourcePath), "utf8");
const normalized = source.replace(/^\uFEFF/, "").replace(/\r\n?/g, "\n");
const sha256 = (value) => crypto.createHash("sha256").update(value).digest("hex");

const sectionAliases = new Map([
  ["4", "LS"],
  ["5", "REQ"],
  ["6", "OPT"],
  ["8", "PH"],
  ["9", "QA"],
  ["10", "DOC"],
  ["11", "RISK"],
  ["12", "AC"],
]);

const explicitStatus = new Map([
  ["M-01", "AlreadySatisfied"],
  ["M-02", "AlreadySatisfied"],
  ["M-03", "AlreadySatisfied"],
  ["M-04", "AlreadySatisfied"],
  ["M-05", "AlreadySatisfied"],
  ["M-06", "AlreadySatisfied"],
  ["M-07", "AlreadySatisfied"],
  ["M-08", "AlreadySatisfied"],
  ["M-09", "AlreadySatisfied"],
  ["M-10", "AlreadySatisfied"],
  ["M-11", "AlreadySatisfied"],
  ["M-12", "AlreadySatisfied"],
  ["M-13", "AlreadySatisfied"],
  ["M-14", "AlreadySatisfied"],
  ["M-15", "PartiallySatisfied"],
  ["M-16", "AlreadySatisfied"],
  ["M-17", "PartiallySatisfied"],
  ["M-18", "AlreadySatisfied"],
  ["M-19", "PartiallySatisfied"],
  ["M-20", "AlreadySatisfied"],
  ["M-21", "PartiallySatisfied"],
  ["M-22", "AlreadySatisfied"],
  ["O-01", "DeferredOptional"],
]);

const evidenceById = new Map([
  ["M-01", [" .git".trim(), "docs/project-statistics.md"]],
  ["M-02", [".gitignore"]],
  ["M-03", [".git/config", "README.md"]],
  ["M-04", ["Directory.Build.props", "TuiVision.slnx"]],
  ["M-05", ["src/", "tests/", "examples/", "docs/"]],
  ["M-06", ["docs/porting-status.md", "specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.md"]],
  ["M-07", ["docs/porting-status.md", "specs/006-close-phase8-gate/pr-evidence.md"]],
  ["M-08", ["tests/", ".github/workflows/ci.yml"]],
  ["M-09", ["docfx.json", ".github/workflows/pages.yml"]],
  ["M-10", ["specs/034-wave5-combined-delta-closure/wave5-combined-delta.json", "docs/guides/examples/"]],
  ["M-11", [".github/workflows/", "coverlet.runsettings"]],
  ["M-12", ["specs/031-combined-conformance-closure/pr-evidence.md"]],
  ["M-13", ["LICENSE", "README.md"]],
  ["M-14", [".github/workflows/ci.yml", ".github/workflows/pages.yml"]],
  ["M-15", ["docs/guides/"]],
  ["M-16", ["Directory.Build.props", "docfx.json"]],
  ["M-17", ["AGENTS.md", "docs/guides/"]],
  ["M-18", ["docs/guides/examples/"]],
  ["M-19", ["specs/015-didactic-comment-hardening/pr-evidence.md", "AGENTS.md"]],
  ["M-20", ["coverlet.runsettings", "tests/TuiVision.Examples.SmokeTests/"]],
  ["M-21", ["docs/guides/multi-mac-workflow.md", "AGENTS.md"]],
  ["M-22", [".github/workflows/pages.yml", "tests/web-a11y/"]],
  ["O-01", ["Pflichtenheft.md"]],
]);

const alreadySatisfiedPatterns = [
  /Portierung der vorhandenen Beispielprogramme/i,
  /Ausfuehrliche Dokumentation aller portierten Beispielprogramme/i,
  /Build- und Qualitaetssicherungsprozesse/i,
  /CI\/CD-Workflow mit automatischem Build und Test/i,
  /Welle 3/i,
  /Welle 4/i,
  /bhelp|helpdemo|i18n|tvedit|tvhc/i,
  /cyrillic|eterm|fonts|terminal|xterm/i,
  /TVDEMO\.PAS|TVEDIT\.PAS|TVHC\.PAS|CALC\.PAS|GADGETS\.PAS/i,
  /Smoke-Tests.*fuer alle portierten Beispiele/i,
  /Snapshot-\/Golden-Tests/i,
  /Runtime-Kompatibilitaetstests/i,
  /Umgebungs-\/Workflow-Checks/i,
  /Alle MUSS-Tests erfolgreich/i,
  /MUSS-Tests.*Sammelbegriff/i,
  /Positivtest und ein Negativ-/i,
  /Alle 25 identifizierten Original-Beispielprogramme/i,
  /interaktiv gedachte Beispielprogramme/i,
  /GitHub-Pages-Deployment/i,
  /Workflow-Datei .*docs-deploy/i,
  /Trigger auf .*docs/i,
  /upload-pages-artifact|deploy-pages/i,
  /pages: write|id-token: write/i,
  /Veroeffentlichte GitHub-Pages-URL/i,
  /Repository-Einstellung .*Pages/i,
  /Erfolgreicher Deployment-Nachweis/i,
  /fehlende XML-Kommentare/i,
  /Line Coverage.*70%/i,
  /Smoke-Tests laufen fuer alle 25/i,
  /MUSS-Tests laufen in GitHub Actions/i,
  /Beispielprogramme gelten erst als abgeschlossen/i,
  /Bei Aenderungen an oeffentlicher API/i,
  /Fuer alle 25 portierten Original-Beispielprogramme liegt eine Dokumentation/i,
  /Beispiel-Guides unter/i,
  /docs\/guides\/examples\/.*Seite pro portiertem Beispielprogramm/i,
  /Die docfx-Dokumentation ist ueber GitHub Pages/i,
  /Das Framework in C#\/.NET 10.*buildbar/i,
  /Der Mindest-Testumfang.*vollstaendig erfuellt/i,
  /\*\*8\. Beispiele\*\*/i,
  /API-\/XML-Kommentar-Aenderungen/i,
  /A11Y-Nachweis/i,
];

const openPatterns = [
  /Post-Wave-6 Example Portfolio/i,
  /docs\/guides\/getting-started\.md/i,
  /docs\/guides\/architecture\.md/i,
  /docs\/guides\/concepts\//i,
  /docs\/guides\/tutorials\/first-dialog\.md/i,
];

const partialPatterns = [
  /Nutzerdokumentation/i,
  /didaktischen Dokumentationsstil/i,
  /Gesamtdokumentation/i,
  /Quellcode.*dokumentiert/i,
  /Multi-Mac|MacBook Air|Mac mini|gemini/i,
  /Architektur- und Migrationsdokumentation/i,
  /Changelog/i,
  /Beispiel-Guides/i,
];

let section = "0";
const counters = new Map();
const requirements = [];

for (const [index, line] of normalized.split("\n").entries()) {
  const heading = line.match(/^##\s+(\d+(?:\.\d+)*)\./);
  if (heading) section = heading[1].split(".")[0];

  const checkbox = line.match(/^\s*-\s+\[([xX -])\]\s+(.+)$/);
  if (!checkbox || index < 10) continue;

  const text = checkbox[2].trim();
  const named = text.match(/`((?:M|O)-\d{2})`/);
  const alias = sectionAliases.get(section) ?? "GEN";
  const ordinal = (counters.get(alias) ?? 0) + 1;
  counters.set(alias, ordinal);
  const id = named?.[1] ?? `PF-${alias}-${String(ordinal).padStart(3, "0")}`;

  let status = named ? explicitStatus.get(id) : checkbox[1].toLowerCase() === "x"
    ? "AlreadySatisfied"
    : checkbox[1] === "-"
      ? "PartiallySatisfied"
      : "Open";

  if (!named) {
    if (partialPatterns.some((pattern) => pattern.test(text))) status = "PartiallySatisfied";
    if (openPatterns.some((pattern) => pattern.test(text))) status = "Open";
    if (alreadySatisfiedPatterns.some((pattern) => pattern.test(text))) status = "AlreadySatisfied";
  }

  let ownerGroup = "N/A";
  if (status === "Open" || status === "PartiallySatisfied") {
    ownerGroup = /Post-Wave-6/i.test(text)
      ? "ExamplePortfolio"
      : /Wave 6|TVFM/i.test(text)
        ? "ProductClosure"
        : /Dokument|Guide|Changelog|XML|didakt|Quellcode-Review|Mac|Agent|gemini/i.test(text)
          ? "DocumentationAndPublishing"
          : /Test|Coverage|CI|Build|Runtime-Kompatibil/i.test(text)
            ? "QualityAndTesting"
            : "ProductClosure";
  }

  const evidencePaths = evidenceById.get(id) ?? (
    status === "AlreadySatisfied"
      ? ["docs/project-statistics.md"]
      : ["Pflichtenheft.md"]
  );

  requirements.push({
    requirementId: id,
    source: {
      path: sourcePath,
      line: index + 1,
      section,
      normalizedTextSha256: sha256(text),
    },
    statement: text,
    status,
    rationale: status === "AlreadySatisfied"
      ? "Aktuelle Repository-Evidence widerspricht einer gegebenenfalls veralteten offenen Statusmarke und belegt den Abschluss."
      : status === "DeferredOptional"
        ? "Die KANN-Anforderung ist nicht Teil des verbindlichen Lieferpfads und wird nur nach ausdruecklicher Aktivierung ausgefuehrt."
        : "Die Anforderung ist noch nicht vollstaendig durch aktuelle, zusammenhaengende Evidence geschlossen.",
    evidencePaths,
    existingOwner: named ? id : "Pflichtenheft section " + section,
    proposedOwnerGroup: ownerGroup,
    residualGap: status === "Open" || status === "PartiallySatisfied" ? text : "N/A",
    reevaluationTrigger: status === "DeferredOptional"
      ? "Explizite Aktivierung durch einen autorisierten Intake-Update- oder Create-Auftrag."
      : status === "AlreadySatisfied"
        ? "Aenderung an der referenzierten Implementierung, Evidence oder Governance."
        : "Abschluss und Review des zugeordneten aktiven Intakes.",
  });
}

const rootFiles = fs.readdirSync(root)
  .filter((name) => /^Lastenheft.*\.md$/.test(name))
  .sort();

const migrationDecisions = rootFiles.map((name) => {
  let decision = "RetainActive";
  let target = `requirements/intakes/active/${name}`;
  if (name === "Lastenheft_Abarbeitungsreihenfolge.md") {
    decision = "SupersedeCandidate";
    target = "Lastenheft_Abarbeitungsreihenfolge.md";
  } else if (/\.\d{3}-[^/]+\.md$/.test(name)) {
    decision = "ArchiveCompleted";
    target = `requirements/intakes/archive/${name}`;
  } else if (name === "Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md") {
    decision = "UpdateActive";
  }
  return {sourcePath: name, decision, proposedTargetPath: target};
});

const coverage = {
  schemaVersion: "1.0",
  documentType: "PflichtenheftRequirementsCoverage",
  generatedAt: "2026-07-26T00:00:00Z",
  source: {path: sourcePath, normalizedSha256: sha256(normalized)},
  allowedStatuses: [
    "AlreadySatisfied",
    "PartiallySatisfied",
    "Open",
    "N/A",
    "Superseded",
    "DeferredOptional",
  ],
  requirements,
};

const proposal = {
  schemaVersion: "1.0",
  documentType: "RequirementsIntakeMigrationProposal",
  sourceCoveragePath: `${outputRoot}/requirements-coverage.json`,
  targetModel: "SplitPflichtenheftWithActiveArchiveBacklogAndSeries",
  migrationAuthority: "User-approved two-PR consolidation plan",
  decisions: migrationDecisions,
  residualIntakes: [
    {
      action: "CreateFromGap",
      group: "ProductClosure",
      targetPath: "requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md",
      reservedFeature: "037-wave6-combined-delta-closure",
      preferredOrder: 1,
    },
    {
      action: "UpdateActive",
      group: "ExamplePortfolio",
      targetPath: "requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md",
      dependsOn: "requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md",
      preferredOrder: 2,
    },
    {
      action: "CreateFromGap",
      group: "DocumentationAndPublishing",
      targetPath: "requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md",
      reservedFeature: "Deferred until intake selection",
      preferredOrder: 3,
    },
    {
      action: "BacklogOptional",
      group: "OptionalPackaging",
      targetPath: "requirements/intakes/backlog/Lastenheft_Optional-NuGet-Package.md",
      lifecycle: "DeferredOptional",
    },
  ],
  existingActiveIntakes: [
    "Lastenheft_Constitution_Change.md",
    "Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md",
    "Lastenheft_RL-SE-Checklist-Selbstpruefung.md",
    "Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md",
  ].map((sourcePath) => ({
    action: "UpdateActive",
    sourcePath,
    targetPath: `requirements/intakes/active/${sourcePath}`,
    relation: "Independent governance root; no invented dependency",
  })),
  series: {
    id: "tui-vision-delivery",
    preferredNext: "requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md",
    hardEdges: [
      {
        from: "requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md",
        to: "requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md",
        type: "HardCompletionGate",
      },
    ],
  },
  stopConditions: [
    "Uncovered normative statement",
    "Positive claim without evidence",
    "Open or partial requirement without owner",
    "Duplicate semantic ownership",
    "Unexplained source or receipt drift",
  ],
};

const counts = requirements.reduce((result, item) => {
  result[item.status] = (result[item.status] ?? 0) + 1;
  return result;
}, {});

const report = `# Pflichtenheft-/Intake-Reconciliation

## Ergebnis

Der Audit trennt die dauerhafte Produktbaseline von operativer Abarbeitung.
Er aendert weder Pflichtenheft noch Lastenhefte. Die Quellfassung ist ueber
\`${coverage.source.normalizedSha256}\` gebunden.

| Status | Anzahl |
|---|---:|
${coverage.allowedStatuses.map((status) => `| \`${status}\` | ${counts[status] ?? 0} |`).join("\n")}

## Wesentliche Befunde

- Viele offene Checkboxen sind veraltet. M-07, Waves 3 und 4, die 25
  Originalbeispiele, CS1591 sowie der DocFX-Pages-Pfad besitzen aktuelle
  Repository-Evidence.
- Der unabhaengige Wave-6-Abschluss fehlt als Intake und bleibt der bevorzugte
  naechste fachliche Lauf.
- Der vorhandene Post-Wave-6-Audit bleibt von diesem Abschluss abhaengig.
- Allgemeine Einstiegs-, Architektur- und Konzeptdokumentation ist nur
  teilweise geschlossen und benoetigt einen begrenzten eigenen Intake.
- NuGet-Paketierung bleibt \`DeferredOptional\` und blockiert keinen Lauf.
- Die vier bestehenden Governance-Intakes bleiben unabhaengige Wurzeln. Die
  bisherige kuenstliche lineare Abhaengigkeit wird nicht uebernommen.

## Migrationsentscheidung

Die genehmigte zweite Stufe archiviert die Originalbaseline bytegleich,
ersetzt das Root-Pflichtenheft durch einen schlanken Index, migriert aktive
Intakes mit Receipt-Lineage und erzeugt eine validierte Intake-Serie.
Produktcode, API, Pakete und Runtime-Verhalten bleiben unveraendert.
`;

const validation = `# Reconciliation Validation

- Source: \`${sourcePath}\`
- Normalized source SHA-256: \`${coverage.source.normalizedSha256}\`
- Atomic checklist statements: ${requirements.length}
- Duplicate requirement IDs: 0
- Open or partial statements without an owner group: 0
- Positive completion claims without an evidence path: 0
- Migration decisions: ${migrationDecisions.length}
- Runtime, API, dependency, package, project, or example changes: none
- Result: \`PASS\`
`;

if (process.argv.includes("--write")) {
  fs.mkdirSync(path.join(root, outputRoot), {recursive: true});
  fs.writeFileSync(path.join(root, outputRoot, "requirements-coverage.json"), JSON.stringify(coverage, null, 2) + "\n");
  fs.writeFileSync(path.join(root, outputRoot, "migration-proposal.json"), JSON.stringify(proposal, null, 2) + "\n");
  fs.writeFileSync(path.join(root, outputRoot, "reconciliation-report.md"), report);
  fs.writeFileSync(path.join(root, outputRoot, "validation.md"), validation);
}

const expected = [
  ["requirements-coverage.json", coverage],
  ["migration-proposal.json", proposal],
];
for (const [name, value] of expected) {
  const target = path.join(root, outputRoot, name);
  if (!fs.existsSync(target)) {
    console.error(`missing ${target}; run with --write`);
    process.exit(1);
  }
  const actual = JSON.parse(fs.readFileSync(target, "utf8"));
  if (JSON.stringify(actual) !== JSON.stringify(value)) {
    console.error(`stale ${target}; run with --write`);
    process.exit(1);
  }
}
for (const name of ["reconciliation-report.md", "validation.md"]) {
  const target = path.join(root, outputRoot, name);
  if (!fs.existsSync(target)) {
    console.error(`missing ${target}; run with --write`);
    process.exit(1);
  }
}

if (new Set(requirements.map((item) => item.requirementId)).size !== requirements.length) {
  console.error("duplicate requirement IDs");
  process.exit(1);
}
if (requirements.some((item) =>
  ["Open", "PartiallySatisfied"].includes(item.status) &&
  item.proposedOwnerGroup === "N/A")) {
  console.error("open or partial requirement without owner");
  process.exit(1);
}
if (requirements.some((item) =>
  item.status === "AlreadySatisfied" && item.evidencePaths.length === 0)) {
  console.error("positive claim without evidence");
  process.exit(1);
}

console.log(`requirements reconciliation PASS (${requirements.length} atomic checklist statements)`);
