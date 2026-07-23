#!/usr/bin/env bash
# Validate an intake-review result and its current target hashes without writes.
set -euo pipefail

usage() {
  printf '%s\n' 'Usage: validate-intake-review-result.sh --result FILE [--repo PATH]'
}

result=''
repo='.'
while [ "$#" -gt 0 ]; do
  case "$1" in
    --result) result="${2:-}"; shift 2 ;;
    --repo) repo="${2:-}"; shift 2 ;;
    -h|--help) usage; exit 0 ;;
    *) printf 'ERROR: unknown option: %s\n' "$1" >&2; exit 2 ;;
  esac
done
[ -n "$result" ] || { printf 'ERROR: --result is required\n' >&2; exit 2; }

python3 - "$result" "$repo" <<'PY'
import hashlib, json, re, sys, uuid
from datetime import datetime
from pathlib import Path, PurePosixPath

result_path = Path(sys.argv[1])
repo = Path(sys.argv[2]).resolve()
errors = []

def graph_error(code, message):
    errors.append(f"{code}: {message}")

def required_text(obj, key, label):
    value = obj.get(key) if isinstance(obj, dict) else None
    if not isinstance(value, str) or not value.strip():
        errors.append(f"{label}.{key} must be a non-empty string")
        return ""
    return value.strip()

def relative(value):
    p = PurePosixPath(value)
    return not p.is_absolute() and ".." not in p.parts

def normalized(path):
    raw = path.read_bytes()
    if raw.startswith(b"\xef\xbb\xbf"):
        raw = raw[3:]
    try:
        text = raw.decode("utf-8", errors="strict")
    except UnicodeDecodeError as exc:
        raise ValueError(f"not strict UTF-8: {exc}") from exc
    if "\x00" in text:
        raise ValueError("binary NUL detected")
    return text.replace("\r\n", "\n").replace("\r", "\n").encode("utf-8")

try:
    data = json.loads(result_path.read_text(encoding="utf-8"))
except Exception as exc:
    print(f"ERROR: invalid result JSON: {exc}", file=sys.stderr)
    raise SystemExit(2)
if not isinstance(data, dict):
    errors.append("result root must be an object"); data = {}

schema_version = data.get("schemaVersion")
if schema_version not in ("1.0", "1.1"): errors.append("schemaVersion must be 1.0 or 1.1")
review_id = required_text(data, "reviewId", "result")
try:
    if uuid.UUID(review_id).int == 0: errors.append("reviewId must not be the starter UUID")
except ValueError: errors.append("reviewId must be a UUID")
mode = required_text(data, "mode", "result")
if mode not in ("Single", "Series", "Campaign"): errors.append("mode is invalid")
if mode == "Series" and schema_version != "1.1":
    graph_error("IRG001", "Series result schemaVersion must be 1.1")
status = required_text(data, "status", "result")
accepted = ("Ready", "ReadyWithAcceptedRisks")
if status not in accepted + ("NeedsClarification", "NeedsRemediation", "Rejected"):
    errors.append("status is invalid")
required_text(data, "policy", "result")
reviewed_at = required_text(data, "reviewedAt", "result")
try:
    if not reviewed_at.endswith("Z"): raise ValueError()
    datetime.fromisoformat(reviewed_at[:-1] + "+00:00")
except ValueError: errors.append("reviewedAt must be an ISO-8601 UTC timestamp")

targets = data.get("targets")
if not isinstance(targets, list) or not targets: errors.append("targets must be a non-empty array"); targets = []
target_paths = set()
target_roles = {}
for i, target in enumerate(targets):
    label = f"targets[{i}]"
    path_text = required_text(target, "path", label)
    digest = required_text(target, "normalizedSha256", label)
    role = required_text(target, "role", label)
    required_text(target, "gitBlob", label)
    if path_text and (not relative(path_text) or path_text in target_paths):
        errors.append(f"{label}.path must be unique and repository-relative")
    target_paths.add(path_text)
    target_roles[path_text] = role
    if not re.fullmatch(r"[0-9a-f]{64}", digest): errors.append(f"{label}.normalizedSha256 is invalid")
    target_path = repo / path_text
    if not target_path.is_file(): errors.append(f"target missing: {path_text}")
    else:
        try: actual = hashlib.sha256(normalized(target_path)).hexdigest()
        except ValueError as exc: errors.append(f"target {path_text}: {exc}")
        else:
            if actual != digest: errors.append(f"target hash drift: {path_text}")

request_evidence = data.get("requestEvidence")
request_binding_required = mode == "Series" or request_evidence is not None
if request_binding_required:
    if not isinstance(request_evidence, dict):
        graph_error("IRG001", "requestEvidence must be an object")
    else:
        request_path_text = required_text(request_evidence, "path", "requestEvidence")
        request_digest = required_text(request_evidence, "normalizedSha256", "requestEvidence")
        if request_path_text and not relative(request_path_text):
            graph_error("IRG001", "requestEvidence.path must be repository-relative")
        if not re.fullmatch(r"[0-9a-f]{64}", request_digest):
            graph_error("IRG001", "requestEvidence.normalizedSha256 is invalid")
        request_path = repo / request_path_text
        request_data = None
        if not request_path.is_file():
            graph_error("IRG001", f"request missing: {request_path_text}")
        else:
            try:
                request_bytes = normalized(request_path)
                actual_request_digest = hashlib.sha256(request_bytes).hexdigest()
                request_data = json.loads(request_bytes.decode("utf-8"))
            except (ValueError, json.JSONDecodeError) as exc:
                graph_error("IRG001", f"invalid request: {exc}")
            else:
                if actual_request_digest != request_digest:
                    graph_error("IRG001", "request hash drift")
        if request_data is not None:
            if not isinstance(request_data, dict):
                graph_error("IRG002", "request root must be an object")
                request_data = {}
            request_schema = request_data.get("schemaVersion")
            expected_request_schemas = ("1.1",) if mode == "Series" else ("1.0", "1.1")
            if request_schema not in expected_request_schemas:
                graph_error("IRG002", f"{mode} request schemaVersion is invalid")
            for field, expected in (("reviewId", review_id), ("mode", mode), ("policy", data.get("policy"))):
                if request_data.get(field) != expected:
                    graph_error("IRG002", f"request.{field} must match result.{field}")

            request_targets = request_data.get("targets")
            if not isinstance(request_targets, list) or not request_targets:
                graph_error("IRG003", "request.targets must be a non-empty array")
                request_targets = []
            request_paths = []
            request_roles = {}
            for i, request_target in enumerate(request_targets):
                label = f"request.targets[{i}]"
                path_text = required_text(request_target, "path", label)
                role = required_text(request_target, "role", label)
                if not path_text or not relative(path_text):
                    graph_error("IRG003", f"{label}.path must be repository-relative")
                if path_text in request_roles:
                    graph_error("IRG003", f"duplicate request target: {path_text}")
                request_paths.append(path_text)
                request_roles[path_text] = role
            if set(request_paths) != target_paths or len(request_paths) != len(target_paths):
                graph_error("IRG003", "request and result target sets must match exactly")
            for path_text in target_paths & set(request_paths):
                if request_roles.get(path_text) != target_roles.get(path_text):
                    graph_error("IRG003", f"request and result roles differ: {path_text}")

            if mode == "Series":
                series_data = request_data.get("series")
                if not isinstance(series_data, dict):
                    graph_error("IRG004", "request.series must be an object")
                    series_data = {}
                ordered = series_data.get("orderedTargetPaths")
                if not isinstance(ordered, list):
                    graph_error("IRG004", "series.orderedTargetPaths must be an array")
                    ordered = []
                if any(not isinstance(path, str) or not path.strip() for path in ordered):
                    graph_error("IRG004", "series.orderedTargetPaths must contain non-empty strings")
                if len(ordered) != len(set(ordered)):
                    graph_error("IRG004", "series.orderedTargetPaths contains duplicates")
                if set(ordered) != target_paths or len(ordered) != len(target_paths):
                    graph_error("IRG004", "series.orderedTargetPaths must contain every target exactly once")
                order_index = {path: index for index, path in enumerate(ordered)}

                roots = series_data.get("roots")
                if not isinstance(roots, list):
                    graph_error("IRG008", "series.roots must be an array")
                    roots = []
                if any(not isinstance(root, str) or not root.strip() for root in roots):
                    graph_error("IRG008", "series.roots must contain non-empty strings")
                if len(roots) != len(set(roots)):
                    graph_error("IRG008", "series.roots contains duplicates")
                if any(root not in target_paths for root in roots):
                    graph_error("IRG008", "series.roots references an unknown target")

                dependencies = series_data.get("dependencies")
                if not isinstance(dependencies, list):
                    graph_error("IRG005", "series.dependencies must be an array")
                    dependencies = []
                edge_pairs = set()
                adjacency = {path: [] for path in target_paths}
                indegree = {path: 0 for path in target_paths}
                for i, dependency in enumerate(dependencies):
                    label = f"series.dependencies[{i}]"
                    source = required_text(dependency, "from", label)
                    destination = required_text(dependency, "to", label)
                    required_text(dependency, "kind", label)
                    if source not in target_paths or destination not in target_paths:
                        graph_error("IRG005", f"{label} references an unknown target")
                        continue
                    if source == destination:
                        graph_error("IRG005", f"{label} must not be a self-edge")
                        continue
                    pair = (source, destination)
                    if pair in edge_pairs:
                        graph_error("IRG006", f"duplicate dependency edge: {source} -> {destination}")
                        continue
                    edge_pairs.add(pair)
                    adjacency[source].append(destination)
                    indegree[destination] += 1
                    if source in order_index and destination in order_index and order_index[source] >= order_index[destination]:
                        graph_error("IRG004", f"dependency contradicts declared order: {source} -> {destination}")

                actual_roots = {path for path, degree in indegree.items() if degree == 0}
                if set(roots) != actual_roots:
                    graph_error("IRG008", "series.roots must equal the zero-indegree target set")
                if target_paths and not roots:
                    graph_error("IRG008", "Series requires at least one root")
                if any(path not in set(roots) and indegree[path] == 0 for path in target_paths):
                    graph_error("IRG008", "every non-root target needs an incoming dependency")

                remaining = dict(indegree)
                queue = [path for path, degree in remaining.items() if degree == 0]
                visited = 0
                while queue:
                    node = queue.pop()
                    visited += 1
                    for successor in adjacency[node]:
                        remaining[successor] -= 1
                        if remaining[successor] == 0:
                            queue.append(successor)
                if visited != len(target_paths):
                    graph_error("IRG007", "series.dependencies must be acyclic")

findings = data.get("findings")
if not isinstance(findings, list): errors.append("findings must be an array"); findings = []
finding_ids = set(); severity_counts = {k: 0 for k in ("Critical", "High", "Medium", "Low")}
for i, finding in enumerate(findings):
    label = f"findings[{i}]"; fid = required_text(finding, "id", label)
    if not re.fullmatch(r"IR[0-9]{3,}", fid) or fid in finding_ids: errors.append(f"{label}.id must be a unique IR### identifier")
    finding_ids.add(fid)
    severity = required_text(finding, "severity", label)
    if severity not in severity_counts: errors.append(f"{label}.severity is invalid")
    else: severity_counts[severity] += 1
    for field in ("category", "target", "disposition", "owner", "evidence", "reevaluationTrigger"):
        required_text(finding, field, label)

questions = data.get("questions")
risks = data.get("acceptedRisks")
exceptions = data.get("operatorExceptions")
if not isinstance(questions, list): errors.append("questions must be an array"); questions = []
if not isinstance(risks, list): errors.append("acceptedRisks must be an array"); risks = []
if not isinstance(exceptions, list): errors.append("operatorExceptions must be an array"); exceptions = []
open_questions = [q for q in questions if isinstance(q, dict) and q.get("status") != "Answered"]
if status in accepted and open_questions: errors.append("accepted status cannot have unanswered questions")
if status in accepted and (severity_counts["Critical"] or severity_counts["High"]):
    errors.append("Critical or High findings block an accepted status")
if status == "Ready" and risks: errors.append("Ready cannot contain accepted risks")
if status == "ReadyWithAcceptedRisks" and not risks: errors.append("ReadyWithAcceptedRisks needs acceptedRisks")
for i, risk in enumerate(risks):
    label = f"acceptedRisks[{i}]"
    if required_text(risk, "severity", label) not in ("Medium", "Low"): errors.append(f"{label}.severity must be Medium or Low")
    for field in ("findingId", "owner", "rationale", "acceptedAt", "evidence", "reevaluationTrigger"):
        required_text(risk, field, label)
    if risk.get("acceptedByType") != "Human": errors.append(f"{label}.acceptedByType must be Human")
for i, exception in enumerate(exceptions):
    label = f"operatorExceptions[{i}]"
    for field in ("exceptionId", "author", "reason", "date", "expiry"):
        required_text(exception, field, label)
    worker_ids = exception.get("workerIds") if isinstance(exception, dict) else None
    if not isinstance(worker_ids, list) or not worker_ids or any(not isinstance(x, str) or not x.strip() for x in worker_ids):
        errors.append(f"{label}.workerIds must be a non-empty string array")
    try:
        expiry = required_text(exception, "expiry", label)
        parsed_expiry = datetime.fromisoformat(expiry[:-1] + "+00:00") if expiry.endswith("Z") else datetime.fromisoformat(expiry)
        if parsed_expiry.timestamp() <= datetime.now().astimezone().timestamp(): raise ValueError()
    except ValueError: errors.append(f"{label}.expiry must be a future ISO-8601 timestamp")

coverage = data.get("coverage")
if not isinstance(coverage, dict): errors.append("coverage must be an object"); coverage = {}
individual = coverage.get("individual", [])
series = coverage.get("series", [])
workers = coverage.get("workers", [])
if not isinstance(individual, list) or set(individual) != target_paths:
    errors.append("coverage.individual must contain every target path exactly once")
if mode == "Series" and (not isinstance(series, list) or not series): errors.append("Series mode requires series coverage")
if mode == "Campaign":
    if not isinstance(workers, list) or not workers: errors.append("Campaign mode requires worker coverage")
    worker_ids = set()
    for i, worker in enumerate(workers if isinstance(workers, list) else []):
        wid = required_text(worker, "workerId", f"coverage.workers[{i}]")
        target = required_text(worker, "targetPath", f"coverage.workers[{i}]")
        if wid in worker_ids: errors.append(f"duplicate worker coverage: {wid}")
        worker_ids.add(wid)
        if target not in target_paths: errors.append(f"worker target is not reviewed: {target}")
        required_text(worker, "applicability", f"coverage.workers[{i}]")

summary = data.get("summary")
if not isinstance(summary, dict): errors.append("summary must be an object"); summary = {}
for severity, expected in severity_counts.items():
    if summary.get(severity.lower()) != expected: errors.append(f"summary.{severity.lower()} must equal {expected}")
required_text(data, "supersedes", "result")

if errors:
    for error in errors: print(f"ERROR: {error}", file=sys.stderr)
    raise SystemExit(2)
print(f"PASS: intake review {review_id} is current ({mode}, {status}, {len(targets)} targets)")
PY
