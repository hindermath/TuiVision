"""Leaf-phase executor inside the approved sandbox; never a full orchestrator."""
from __future__ import annotations

import json
import pathlib
import subprocess
import tempfile


def execute(payload: dict) -> dict:
    source = pathlib.Path(payload["source"])
    mode = payload["mode"]
    if mode not in ("check-only", "dry-run", "update"):
        raise ValueError("Invalid delegated mode")
    if payload["phase"] not in ("registry", "propagation", "preset-profiles", "storage-cleanup"):
        raise ValueError("Invalid delegated phase")
    records = []
    updated = []
    with tempfile.TemporaryDirectory(prefix="hb-maintenance-") as temporary:
        workspace = pathlib.Path(temporary)
        registry = workspace / "registry.json"
        entries = payload["entries"]
        for entry in entries:
            repo = pathlib.Path(entry["containerPath"])
            root = pathlib.Path(entry["containerRoot"])
            if repo.resolve() != repo or root.resolve() != root:
                raise ValueError("Delegated path has symlink components")
            repo.relative_to(root)
            if (repo / ".git").is_symlink() or not (repo / ".git").is_dir():
                raise ValueError("Missing delegated Git target")
        registry.write_text(json.dumps({
            "schemaVersion": 1,
            "defaultPresetProfile": payload["defaultPresetProfile"],
            "repositories": [{**entry["registry"], "path": entry["containerPath"]} for entry in entries],
        }), encoding="utf-8")
        for entry in entries:
            repo = entry["containerPath"]
            phase = payload["phase"]
            command = []
            if phase == "registry":
                command = ["bash", str(source / "scripts/register-level2-repository.sh"),
                           "--repo", repo, "--level", str(entry["registry"]["level"]),
                           "--registry", str(registry), "--source", "maintenance-discovery"]
                # The temporary registry is always writable; only an approved
                # update result may be returned to the host registry owner.
            elif phase == "propagation":
                command = ["bash", str(source / "scripts/propagate-agentic-toolchain-maintenance.sh"),
                           "--repo", repo, "--home-dir", "/", "--registry", str(registry)]
                # Checks also run during preview so drift never becomes a
                # successful result merely because a dry-run returned zero.
                command += ["--check-only"]
            elif phase == "preset-profiles":
                profile = entry["registry"].get("presetProfile", payload["defaultPresetProfile"])
                if profile == "none":
                    records.append({"targetId": entry["targetId"], "status": "Skipped", "reason": "ProfileNone"})
                    continue
                catalog = json.loads((source / "scripts/config/spec-kit-preset-profiles.json").read_text())
                profiles = catalog["profiles"]
                selected = profiles[profile]
                config = selected.get("configPath", selected.get("presetConfig"))
                if not isinstance(config, str) or pathlib.PurePosixPath(config).is_absolute() or ".." in pathlib.PurePosixPath(config).parts:
                    raise ValueError("Invalid preset matrix path")
                local = subprocess.run(["git", "-C", repo, "rev-parse", "HEAD"], capture_output=True, text=True, check=True).stdout
                upstream = subprocess.run(["git", "-C", repo, "rev-parse", "origin/HEAD"], capture_output=True, text=True, check=True).stdout
                if local != upstream:
                    records.append({"targetId": entry["targetId"], "status": "Blocked", "reason": "CanonicalPresetWorktreeRequired"})
                    continue
                command = ["bash", str(source / "scripts/install-spec-kit-governance-presets.sh"),
                           "--repo", repo, "--preset-config", str(source / config),
                           "--check-only"]
            elif phase == "storage-cleanup":
                continue
            result = subprocess.run(command, capture_output=True, text=True, timeout=600)
            output = result.stdout + result.stderr
            status = "Passed" if result.returncode == 0 else "Blocked" if result.returncode == 1 else "Failed"
            if phase == "registry" and mode != "update" and any(line.startswith(("updated:", "added:")) for line in output.splitlines()):
                status = "Blocked"
            records.append({"targetId": entry["targetId"], "status": status,
                            "exitCode": result.returncode, "output": output})
            # No implicit repair. The caller must explicitly pass repairDrift.
            if phase in ("propagation", "preset-profiles") and result.returncode == 1 and mode == "update" and payload["repairDrift"]:
                dirty = subprocess.run(["git", "-C", repo, "status", "--porcelain=v1", "--untracked-files=all"],
                                       capture_output=True, text=True, timeout=30, check=True)
                if dirty.stdout:
                    records[-1].update(status="Blocked", reason="DirtyWorktreeRequiresReview")
                    continue
                preview = command[:-1] + ["--dry-run"]
                preview_result = subprocess.run(preview, capture_output=True, text=True, timeout=600)
                if preview_result.returncode:
                    records[-1]["status"] = "Failed"
                    continue
                update = command[:-1] + (["--force"] if phase == "preset-profiles" else [])
                applied = subprocess.run(update, capture_output=True, text=True, timeout=600)
                checked = subprocess.run(command, capture_output=True, text=True, timeout=600) if applied.returncode == 0 else applied
                records[-1].update(status="Passed" if checked.returncode == 0 else "Failed", exitCode=checked.returncode,
                                   repaired=checked.returncode == 0,
                                   output=output + applied.stdout + applied.stderr + checked.stdout + checked.stderr)
        if payload["phase"] == "registry":
            updated = json.loads(registry.read_text())["repositories"]
        if payload["phase"] == "storage-cleanup":
            # Only project outputs belong to the delegated phase. Host caches,
            # container images and persistent agent volumes are not candidates.
            import workspace_storage_maintenance as storage
            storage_registry = json.loads(registry.read_text())
            for entry in storage_registry["repositories"]:
                entry["path"] = entry["path"].lstrip("/")
            registry.write_text(json.dumps(storage_registry))
            result_file = workspace / "storage.json"
            args = storage.build_parser().parse_args([
                "--home-dir", "/", "--registry", str(registry),
                "--policy", str(source / "scripts/config/workspace-storage-maintenance.json"),
                "--report", str(result_file), "--mode", mode, "--profile", payload["cleanupProfile"],
                "--run-id", payload["runId"], "--projects-only"])
            args.confirm_deep_cleanup = payload.get("confirmDeepCleanup") is True
            if mode == "update":
                args.mode = "dry-run"
                preview_code = storage.execute(args)
                preview = json.loads(result_file.read_text())
                if preview_code or preview.get("overallStatus") != "SUCCESS":
                    records.append({"status": "Blocked", "exitCode": preview_code, "storage": preview})
                    return {"records": records, "registry": [], "status": "Blocked"}
                args.mode = mode
            code = storage.execute(args)
            storage_result = json.loads(result_file.read_text())
            records.append({"status": "Failed" if code else "Blocked" if storage_result.get("warnings") else "Passed",
                            "exitCode": code, "storage": storage_result})
    return {"records": records, "registry": updated, "repaired": any(r.get("repaired") for r in records),
            "status": "Failed" if any(r["status"] == "Failed" for r in records) else
                      "Blocked" if any(r["status"] == "Blocked" for r in records) else "Passed"}
