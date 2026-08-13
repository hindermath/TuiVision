#!/usr/bin/env python3
"""Validate installed Spec Kit command surfaces and preset profiles."""

from __future__ import annotations

from collections import Counter
import json
from pathlib import Path
import unittest


REPOSITORY = Path(__file__).resolve().parents[2]
PRESETS = REPOSITORY / ".specify" / "presets"
CONFIG = REPOSITORY / "scripts" / "config"


def read_json(path: Path) -> dict:
    return json.loads(path.read_text(encoding="utf-8"))


def strip_frontmatter(text: str) -> str:
    normalized = text.replace("\r\n", "\n")
    if normalized.startswith("---\n"):
        end = normalized.find("\n---\n", 4)
        if end >= 0:
            normalized = normalized[end + 5 :]
    return normalized.strip()


def command_path(surface: str, command: str) -> Path:
    skill = command.replace(".", "-")
    opencode_commands = REPOSITORY / ".opencode" / "commands"
    layouts = {
        "agy": REPOSITORY / ".agents" / "skills" / skill / "SKILL.md",
        "codex": REPOSITORY / ".agents" / "skills" / skill / "SKILL.md",
        "claude": REPOSITORY / ".claude" / "skills" / skill / "SKILL.md",
        "copilot": REPOSITORY / ".github" / "agents" / f"{command}.agent.md",
        "opencode": (
            opencode_commands
            if opencode_commands.is_dir()
            else REPOSITORY / ".opencode" / "command"
        )
        / f"{command}.md",
    }
    return layouts[surface]


class SpecKitAgentSurfaceParityTests(unittest.TestCase):
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

        for preset_id, expected in canonical.items():
            installed = self.registry[preset_id]
            self.assertEqual(installed["version"], expected["version"].removeprefix("v"))
            self.assertEqual(installed["priority"], expected["priority"])


if __name__ == "__main__":
    unittest.main()
