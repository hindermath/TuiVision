#!/usr/bin/env python3
"""Validate installed Spec Kit command surfaces and preset profiles."""
# DE: Prueft Release-Quellen, hashgebundene Installation und Agentenparitaet.
# EN: Validates release sources, hash-bound installation and agent parity.

from __future__ import annotations

from collections import Counter
from copy import deepcopy
import hashlib
import json
from pathlib import Path, PurePosixPath
import re
import tempfile
import unittest


REPOSITORY = Path(__file__).resolve().parents[2]
PRESETS = REPOSITORY / ".specify" / "presets"
CONFIG = REPOSITORY / "scripts" / "config"


def read_json(path: Path) -> dict:
    return json.loads(path.read_text(encoding="utf-8-sig"))


def normalized_hash(path: Path) -> str:
    raw = path.read_bytes()
    try:
        raw = raw.decode("utf-8-sig").replace("\r\n", "\n").replace("\r", "\n").encode("utf-8")
    except UnicodeDecodeError:
        pass  # Binary evidence remains byte-exact; text permits only newline/BOM parity.
    return hashlib.sha256(raw).hexdigest()


def payload_binding(root: Path) -> dict:
    if root.is_symlink() or not root.is_dir():
        raise ValueError("installed preset must be a regular directory")
    entries = []
    # POSIX string ordering is stable across hosts; Windows Path ordering folds case.
    for path in sorted(root.rglob("*"), key=lambda item: item.relative_to(root).as_posix()):
        if path.is_symlink():
            raise ValueError("symlink in installed preset")
        if path.is_file():
            entries.append([path.relative_to(root).as_posix(), normalized_hash(path)])
    data = json.dumps(entries, separators=(",", ":")).encode("utf-8")
    return {"fileCount": len(entries), "contentSha256": hashlib.sha256(data).hexdigest()}


def validate_installed_lock(root: Path, lock: dict) -> None:
    if lock.get("schemaVersion") != 1 or lock.get("hashContract") != "sha256-utf8-lf-bom-or-raw-v1":
        raise ValueError("unsupported installed-state contract")
    if not re.fullmatch(r"[0-9a-f]{40}", lock.get("baselineCommit", "")):
        raise ValueError("missing immutable installation baseline")
    registry_path = root / ".registry"
    if registry_path.is_symlink():
        raise ValueError("installed registry must not be a symlink")
    if normalized_hash(registry_path) != lock["registrySha256"]:
        raise ValueError("installed registry hash drift")
    registry = read_json(registry_path)["presets"]
    entries = lock["presets"]
    expected = {entry["id"]: entry for entry in entries}
    if len(expected) != len(entries) or set(expected) != set(registry):
        raise ValueError("installed preset coverage drift")
    actual_directories = {path.name for path in root.iterdir() if path.is_dir() and path.name != ".cache"}
    if actual_directories != set(expected):
        raise ValueError("installed directory coverage drift")
    for preset_id, entry in expected.items():
        if not re.fullmatch(r"[a-z0-9]+(?:-[a-z0-9]+)*", preset_id):
            raise ValueError("unsafe installed preset ID")
        installed = registry[preset_id]
        for field in ("version", "priority", "enabled", "manifest_hash"):
            if installed[field] != entry[field]:
                raise ValueError(f"installed {field} drift: {preset_id}")
        manifest = root / preset_id / "preset.yml"
        digest = "sha256:" + normalized_hash(manifest)
        if digest != entry["manifest_hash"]:
            raise ValueError(f"installed manifest hash drift: {preset_id}")
        version = re.search(r'^\s+version:\s*"([^"\s]+)"\s*$', manifest.read_text(encoding="utf-8-sig"), re.M)
        if not version or version[1] != entry["version"]:
            raise ValueError(f"installed manifest version drift: {preset_id}")
        if payload_binding(root / preset_id) != {key: entry[key] for key in ("fileCount", "contentSha256")}:
            raise ValueError(f"installed payload drift: {preset_id}")


def validate_release_sources(config: Path, lock: dict) -> None:
    entries = lock["presets"]
    sources = {entry["id"]: entry for entry in entries}
    if len(sources) != len(entries):
        raise ValueError("duplicate release source ID")
    for path in config.glob("*governance-presets.json"):
        profile = read_json(path)["presets"]
        if len({entry["id"] for entry in profile}) != len(profile):
            raise ValueError("duplicate profile preset ID")
        for entry in profile:
            if entry["id"] not in sources:
                raise ValueError(f"missing release source: {entry['id']}")
            source = sources[entry["id"]]
            if (entry["version"] != source["tag"] or entry["archiveUrl"] != source["archiveUrl"]
                    or entry["repository"] != source["repository"]):
                raise ValueError(f"release source drift: {entry['id']}")


def strip_frontmatter(text: str) -> str:
    normalized = text.replace("\r\n", "\n")
    if normalized.startswith("---\n"):
        end = normalized.find("\n---\n", 4)
        if end >= 0:
            normalized = normalized[end + 5 :]
    return normalized.strip()


def opencode_directory(manifest: dict) -> Path:
    # DE: Die versionierte Integration bestimmt den Pfad, nicht lokale Caches.
    # EN: The tracked integration chooses the path, never machine-local caches.
    files = manifest.get("files", {})
    if not isinstance(files, dict) or not files:
        raise ValueError("OpenCode integration has no bound files")
    if any(not isinstance(path, str) or "\\" in path
           or str(PurePosixPath(path)) != path for path in files):
        raise ValueError("OpenCode integration requires canonical POSIX string paths")
    # DE: Manifestpfade bleiben POSIX, auch auf einem Windows-Host.
    # EN: Manifest paths use POSIX syntax even on a Windows host.
    parents = {str(PurePosixPath(path).parent) for path in files}
    if len(parents) != 1 or not parents <= {".opencode/command", ".opencode/commands"}:
        raise ValueError("OpenCode integration has an unsafe or mixed namespace")
    return REPOSITORY / parents.pop()


def command_path(surface: str, command: str) -> Path:
    skill = command.replace(".", "-")
    layouts = {
        "agy": REPOSITORY / ".agents" / "skills" / skill / "SKILL.md",
        "codex": REPOSITORY / ".agents" / "skills" / skill / "SKILL.md",
        "claude": REPOSITORY / ".claude" / "skills" / skill / "SKILL.md",
        "copilot": REPOSITORY / ".github" / "agents" / f"{command}.agent.md",
        "opencode": opencode_directory(read_json(
            REPOSITORY / ".specify" / "integrations" / "opencode.manifest.json"
        )) / f"{command}.md",
    }
    return layouts[surface]


class SpecKitAgentSurfaceParityTests(unittest.TestCase):
    def test_composed_governance_commands_retain_all_wrappers_on_every_surface(self) -> None:
        # DE: Ein gruener Single-Owner-Test erkennt verlorene Wrapper bei Mehrfachbelegung nicht.
        # EN: Single-owner parity cannot detect lost wrappers on shared commands.
        wrappers = ("Security", "Architecture", "iSAQB Architecture", "A11Y", "Cross-Platform", "Agent Parity")
        for command in ("speckit.specify", "speckit.plan", "speckit.tasks"):
            for surface in ("agy", "codex", "claude", "copilot", "opencode"):
                body = strip_frontmatter(command_path(surface, command).read_text(encoding="utf-8"))
                self.assertNotIn("{CORE_TEMPLATE}", body, f"unexpanded core: {surface}/{command}")
                for wrapper in wrappers:
                    self.assertIn(f"apply the {wrapper} Governance preset:", body,
                                  f"missing wrapper: {wrapper}/{surface}/{command}")
                self.assertIn("C3A-/C5-Evidence-Vertrag", body, f"missing architecture contract: {surface}/{command}")

    def test_payload_inventory_has_host_independent_case_sensitive_order(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            root = Path(temporary)
            for name in ("z.md", "README.md", "a.md"):
                (root / name).write_text(name, encoding="utf-8")
            entries = [[name, normalized_hash(root / name)] for name in ("README.md", "a.md", "z.md")]
            digest = hashlib.sha256(json.dumps(entries, separators=(",", ":")).encode("utf-8")).hexdigest()
            self.assertEqual(payload_binding(root), {"fileCount": 3, "contentSha256": digest})

    def test_opencode_namespace_requires_one_bound_safe_directory(self) -> None:
        for namespace in ("command", "commands"):
            manifest = {"files": {f".opencode/{namespace}/speckit.plan.md": "hash"}}
            self.assertEqual(opencode_directory(manifest), REPOSITORY / ".opencode" / namespace)
        for paths in ([], [".opencode/command/a.md", ".opencode/commands/b.md"],
                      ["../command/a.md"], ["/tmp/command/a.md"],
                      [r".opencode\command\a.md"], [r"C:\tmp\command\a.md"],
                      [r".opencode/command/nested\child.md"],
                      [".opencode/command/./a.md"], [42]):
            with self.assertRaises(ValueError):
                opencode_directory({"files": dict.fromkeys(paths, "hash")})

    @classmethod
    def setUpClass(cls) -> None:
        cls.registry = read_json(PRESETS / ".registry")["presets"]

    def test_registered_commands_have_all_maintained_surfaces(self) -> None:
        checked: set[tuple[str, str]] = set()
        for preset in self.registry.values():
            for surface in ("agy", "codex", "claude", "copilot", "opencode"):
                for command in preset.get("registered_commands", {}).get(surface, []):
                    key = (surface, command)
                    if key in checked:
                        continue
                    checked.add(key)
                    path = command_path(surface, command)
                    self.assertTrue(path.is_file(), f"missing {surface} surface: {path}")

                    if surface == "copilot":
                        prompt = (
                            REPOSITORY
                            / ".github"
                            / "prompts"
                            / f"{command}.prompt.md"
                        )
                        self.assertTrue(prompt.is_file(), f"missing Copilot prompt: {prompt}")
                        self.assertIn(
                            f"agent: {command}",
                            prompt.read_text(encoding="utf-8"),
                        )

    def test_single_owner_commands_preserve_the_canonical_body(self) -> None:
        owners = Counter()
        commands_by_preset: dict[str, set[str]] = {}
        for preset_id, preset in self.registry.items():
            commands = set(preset.get("registered_commands", {}).get("codex", []))
            commands_by_preset[preset_id] = commands
            owners.update(commands)

        for preset_id, commands in commands_by_preset.items():
            for command in sorted(commands):
                if owners[command] != 1:
                    continue
                source = PRESETS / preset_id / "commands" / f"{command}.md"
                self.assertTrue(source.is_file(), f"missing canonical command: {source}")
                body = strip_frontmatter(source.read_text(encoding="utf-8"))
                for surface in ("codex", "claude", "copilot", "opencode"):
                    target = command_path(surface, command)
                    if not target.is_file():
                        continue
                    rendered = strip_frontmatter(target.read_text(encoding="utf-8"))
                    self.assertIn(
                        body,
                        rendered,
                        f"canonical body drift: {surface} {command}",
                    )

    def test_preset_profiles_preserve_the_canonical_entries(self) -> None:
        canonical_items = read_json(
            CONFIG / "spec-kit-governance-presets.json"
        )["presets"]
        canonical = {item["id"]: item for item in canonical_items}

        for config in sorted(CONFIG.glob("spec-kit-*-governance-presets.json")):
            profile = {item["id"]: item for item in read_json(config)["presets"]}
            for preset_id, expected in canonical.items():
                if preset_id in profile:
                    self.assertEqual(
                        profile[preset_id],
                        expected,
                        f"canonical preset drift in {config.name}: {preset_id}",
                    )

        validate_release_sources(CONFIG, read_json(REPOSITORY / "docs/maintenance/preset-source-lock.json"))

    def test_installed_presets_match_the_separate_immutable_binding(self) -> None:
        # DE: Neue Release-Ziele sind keine Rollout-Autoritaet. Der Iststand bleibt exakt gebunden.
        # EN: New release targets grant no rollout authority. Bind installed state independently.
        validate_installed_lock(PRESETS, read_json(REPOSITORY / "docs/maintenance/preset-installed-lock.json"))

    def test_installed_binding_rejects_drift_and_preserves_text_parity(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            root = Path(temporary)
            preset = root / "fixture"
            preset.mkdir()
            (preset / "preset.yml").write_text('preset:\n  version: "1.0.0"\n', encoding="utf-8")
            (preset / "command.md").write_text("# Evidence\n", encoding="utf-8")
            (preset / "binary.dat").write_bytes(b"\xff\r\n")
            installed = {"version": "1.0.0", "priority": 10, "enabled": True,
                         "manifest_hash": "sha256:" + normalized_hash(preset / "preset.yml")}
            (root / ".registry").write_text(json.dumps({"presets": {"fixture": installed}}), encoding="utf-8")
            entry = {"id": "fixture", **installed, **payload_binding(preset)}
            lock = {"schemaVersion": 1, "baselineCommit": "a" * 40,
                    "hashContract": "sha256-utf8-lf-bom-or-raw-v1",
                    "registrySha256": normalized_hash(root / ".registry"), "presets": [entry]}
            validate_installed_lock(root, lock)
            (preset / "command.md").write_bytes(b"\xef\xbb\xbf# Evidence\r\n")
            validate_installed_lock(root, lock)
            (preset / "binary.dat").write_bytes(b"\xff\n")
            with self.assertRaises(ValueError):
                validate_installed_lock(root, lock)
            (preset / "binary.dat").write_bytes(b"\xff\r\n")
            validate_installed_lock(root, lock)
            for field, value in (("version", "9.0.0"), ("priority", 99), ("enabled", False),
                                 ("manifest_hash", "sha256:" + "0" * 64),
                                 ("fileCount", 0), ("contentSha256", "0" * 64)):
                drift = deepcopy(lock)
                drift["presets"][0][field] = value
                with self.assertRaises(ValueError, msg=field):
                    validate_installed_lock(root, drift)
            for mutation in ("missing", "duplicate", "baseline", "registry"):
                drift = deepcopy(lock)
                if mutation == "missing": drift["presets"] = []
                elif mutation == "duplicate": drift["presets"].append(entry)
                elif mutation == "baseline": drift["baselineCommit"] = "unknown"
                else: drift["registrySha256"] = "0" * 64
                with self.assertRaises(ValueError, msg=mutation):
                    validate_installed_lock(root, drift)
            for change in ("edit", "extra", "remove"):
                original = (preset / "command.md").read_bytes()
                if change == "edit": (preset / "command.md").write_text("Changed", encoding="utf-8")
                elif change == "extra": (preset / "extra.md").write_text("Extra", encoding="utf-8")
                else: (preset / "command.md").unlink()
                with self.assertRaises(ValueError, msg=change):
                    validate_installed_lock(root, lock)
                if change == "extra": (preset / "extra.md").unlink()
                (preset / "command.md").write_bytes(original)

    def test_release_source_binding_rejects_version_url_and_coverage_drift(self) -> None:
        source = read_json(REPOSITORY / "docs/maintenance/preset-source-lock.json")
        for field in ("tag", "archiveUrl", "repository"):
            drift = deepcopy(source)
            drift["presets"][0][field] = "invalid"
            with self.assertRaises(ValueError, msg=field):
                validate_release_sources(CONFIG, drift)
        duplicate = deepcopy(source)
        duplicate["presets"].append(duplicate["presets"][0])
        with self.assertRaises(ValueError):
            validate_release_sources(CONFIG, duplicate)
        missing = deepcopy(source)
        missing["presets"].pop(0)
        with self.assertRaises(ValueError):
            validate_release_sources(CONFIG, missing)


if __name__ == "__main__":
    unittest.main()
