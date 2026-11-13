"""Offline contract tests for stable release selection and bounded MSL maintenance."""
from __future__ import annotations

import json
import os
from pathlib import Path
import shutil
import subprocess
import tempfile
import unittest

ROOT = Path(__file__).resolve().parents[2]
MODULE = ROOT / "scripts/lib/msl-toolchains.psm1"
CLI = ROOT / "scripts/config/required-cli-tools-registry.json"
BREW = ROOT / "scripts/config/brew-apps-registry.json"
PWSH = shutil.which("pwsh")


def run_ps(source: str, payload: object | None = None) -> dict:
    with tempfile.TemporaryDirectory() as temporary:
        directory = Path(temporary)
        fixture = directory / "fixture.json"
        fixture.write_text(json.dumps(payload), encoding="utf-8")
        script = directory / "test.ps1"
        script.write_text(
            "param($ModulePath, $FixturePath, $CliPath, $PackagePath)\n"
            "$ErrorActionPreference='Stop'\n"
            "Import-Module $ModulePath -Force\n"
            "$fixture=Get-Content $FixturePath -Raw | ConvertFrom-Json -AsHashtable\n"
            "$cli=Get-Content $CliPath -Raw | ConvertFrom-Json -AsHashtable\n"
            "$packages=Get-Content $PackagePath -Raw | ConvertFrom-Json -AsHashtable\n"
            + source,
            encoding="utf-8",
        )
        result = subprocess.run(
            [PWSH, "-NoProfile", "-File", str(script), str(MODULE), str(fixture), str(CLI), str(BREW)],
            capture_output=True, text=True, timeout=60,
        )
        if result.returncode:
            raise AssertionError(result.stdout + result.stderr)
        return json.loads(result.stdout)


def release_fixtures() -> dict:
    return {
        "dotnet": {"releases-index": [
            {"support-phase": "active", "latest-sdk": "11.0.100-rc.2"},
            {"support-phase": "active", "latest-sdk": "10.0.402"},
            {"support-phase": "maintenance", "latest-sdk": "8.0.132"},
        ]},
        "go": [{"version": "go1.28rc1", "stable": False}, {"version": "go1.27.2", "stable": True}],
        "java": {"most_recent_feature_release": 27, "most_recent_feature_version": 28},
        "java-patch": [{"version": {"semver": "27.0.0+35"}}],
        "python": [
            {"name": "Python 3.16.0a1", "is_published": True, "pre_release": True, "release_date": "2026-10-10"},
            {"name": "Python 3.15.0", "is_published": True, "pre_release": False, "release_date": "2026-10-09"},
            {"name": "Python 3.99.0", "is_published": True, "pre_release": False, "release_date": "2099-10-09"},
            {"name": "Python 3.14.8", "is_published": True, "pre_release": False, "release_date": "2026-09-30"},
        ],
        "rust": 'manifest-version = "2"\n[pkg.cargo]\nversion = "0.100.0 (hash)"\n[pkg.rust]\nversion = "1.99.1 (hash)"\n[pkg.rust.target]\navailable = true\n',
        "swift": [{"name": "6.5-DEVELOPMENT-SNAPSHOT"}, {"name": "6.4.1"}, {"name": "6.4.0"}],
    }


ENGINE = r"""
$answer = & (Get-Module msl-toolchains) {
    param($cli, $packages, $fixture)
    $script:fixture=$fixture
    $script:calls=[Collections.Generic.List[string]]::new()
    function script:Test-HBMslPowerShellHealth {
        param($Directory)
        if ($fixture['engineBroken']) {
            return @($script:calls | Where-Object { $_ -like '*upgrade* powershell*' }).Count -gt 0
        }
        return $true
    }
    function script:Get-HBMslRemoteData {
        param($Uri, [switch]$Text)
        if ($fixture['failRemote'] -and $Uri -like '*python.org*') { throw 'source unavailable' }
        if ($Uri -like '*assets/latest*') { return $fixture['releases']['java-patch'] }
        $definition=@($cli['mslToolchains'] | Where-Object { $_['releaseUri'] -eq $Uri })[0]
        return $fixture['releases'][$definition['provider']]
    }
    function script:Get-HBMslProbe {
        param($Definition, $Platform, $Directory)
        $executable=if($Definition['id'] -eq 'swift' -and $fixture['userHome']) {
            Join-Path $fixture['userHome'] '.home-baseline/toolchains/bin/swift'
        } else { '/managed/' + $Definition['command'] }
        return @{ Version=$fixture['active'][$Definition['id']]; Executable=$executable }
    }
    function script:Get-HBMslPackageState {
        param($Definition, $Registry, $Platform, $Latest, $Directory)
        return @{ Manager='brew'; Package=$Definition['id'];
            Source=$fixture['source'][$Definition['id']]; Installed=$fixture['installed'][$Definition['id']] }
    }
    function script:Invoke-HBMslAction {
        param($Command, $Arguments, $Directory, [switch]$Preview)
        if (-not $Preview -and -not ($fixture['engineRepair'] -and $Command -eq 'brew' -and
            ($Arguments -join ' ') -eq 'upgrade --formula powershell')) { throw 'unexpected mutation' }
        $script:calls.Add($Command + ' ' + ($Arguments -join ' '))
    }
    function script:Invoke-HBMslProcess {
        param($Command, $Arguments, $Directory)
        if ($fixture['engineRepair'] -and $Command -eq 'brew' -and
            ($Arguments -join ' ') -eq 'list --formula --versions powershell') {
            return @{ExitCode=0;Output='powershell 7.6.7'}
        }
        throw 'unexpected process'
    }
    $parameters=@{ CliRegistry=$cli; PackageRegistry=$packages; Platform=$fixture['platform'];
        CompareOnly=(-not $fixture['preview'] -and -not $fixture['update']); Preview=[bool]$fixture['preview'] }
    if($fixture['userHome']){$parameters.UserHome=$fixture['userHome']}
    $result=Invoke-HBMslMaintenance @parameters 6>$null
    return @{ result=$result; calls=@($script:calls.ToArray()) }
} $cli $packages $fixture
$answer | ConvertTo-Json -Depth 30
"""


@unittest.skipUnless(PWSH, "PowerShell 7 required")
class StableMslTests(unittest.TestCase):
    def test_release_selection_excludes_previews_and_future_python(self):
        data = run_ps(
            "$result=@{}; foreach($name in $fixture.Keys){$result[$name]=Get-HBMslReleaseVersion $name $fixture[$name]}; $result | ConvertTo-Json",
            release_fixtures(),
        )
        self.assertEqual(data, {"dotnet": "10.0.402", "go": "1.27.2", "java": "27",
                                "java-patch": "27.0.0", "python": "3.15.0", "rust": "1.99.1", "swift": "6.4.1"})

    def test_numeric_comparison_and_distinct_failure_classes(self):
        cases = [
            ["3.9.6", "3.15.0", "3.15.0", "3.15.0", "PathMismatch"],
            ["10.0.401", "10.0.401_1", "10.0.402", "10.0.402", "UpdateAvailable"],
            ["1.9.0", "1.9.0", "1.9.0", "1.10.0", "SourceBehind"],
            ["27", "27.0.0+35", "27", "27.0.0", "Current"],
            ["6.4", "6.4", "6.4.1", "6.4.1", "UpdateAvailable"],
            ["", "", "3.15.0", "3.15.0", "Missing"],
            ["3.15.0", "3.15.0", "3.15.0", "", "Unknown"],
        ]
        data = run_ps(
            "$result=@(); foreach($row in $fixture){ $result+=Get-HBMslStatus -Active $row[0] -Installed $row[1] -Source $row[2] -Latest $row[3] }; @{statuses=$result} | ConvertTo-Json",
            cases,
        )
        self.assertEqual(data["statuses"], [row[4] for row in cases])

    def test_unknown_or_preview_only_metadata_is_not_current(self):
        data = run_ps(
            "$failed=$false; try {Get-HBMslReleaseVersion go @(@{version='go1.28rc1';stable=$false})} catch {$failed=$true}; @{rejected=$failed} | ConvertTo-Json"
        )
        self.assertTrue(data["rejected"])

    def engine_fixture(self):
        latest = {"dotnet": "10.0.402", "go": "1.27.2", "java": "27.0.0",
                  "python": "3.15.0", "rust": "1.99.1", "swift": "6.4.1"}
        return {"releases": release_fixtures(), "active": dict(latest), "installed": dict(latest),
                "source": dict(latest), "platform": "Windows", "preview": False, "failRemote": False}

    def test_complete_compare_snapshot_has_six_unique_items_and_no_actions(self):
        data = run_ps(ENGINE, self.engine_fixture())
        self.assertEqual(data["calls"], [])
        report = data["result"]
        self.assertEqual(report["exitCode"], 0)
        self.assertEqual(len(report["mslToolchains"]), 6)
        self.assertEqual(len({item["toolchainId"] for item in report["mslToolchains"]}), 6)
        self.assertTrue(all(item["status"] == "Current" for item in report["mslToolchains"]))

    def test_broken_powershell_sdk_references_are_required_without_mutating_preview(self):
        for preview, expected in ((False, "Failed"), (True, "Planned")):
            fixture = self.engine_fixture()
            fixture.update(engineBroken=True, preview=preview)
            data = run_ps(ENGINE, fixture)
            dotnet = next(item for item in data["result"]["mslToolchains"] if item["toolchainId"] == "dotnet")
            self.assertEqual(dotnet["status"], expected)
            self.assertIn("dotnet", data["result"]["remainingRequired"])
            self.assertFalse(any("upgrade" in call or "install" in call for call in data["calls"]))
        fixture = self.engine_fixture()
        fixture.update(engineBroken=True, engineRepair=True, update=True)
        data = run_ps(ENGINE, fixture)
        self.assertEqual(data["result"]["exitCode"], 0)
        self.assertEqual(data["calls"], ["brew upgrade --formula powershell"])
        dotnet = next(item for item in data["result"]["mslToolchains"] if item["toolchainId"] == "dotnet")
        self.assertTrue(dotnet["changed"])

    def test_apple_python_path_mismatch_and_source_lag_remain_required(self):
        fixture = self.engine_fixture()
        fixture["active"]["python"] = "3.9.6"
        fixture["source"]["dotnet"] = "10.0.401"
        fixture["installed"]["dotnet"] = fixture["active"]["dotnet"] = "10.0.401"
        data = run_ps(ENGINE, fixture)
        report = data["result"]
        by_id = {item["toolchainId"]: item for item in report["mslToolchains"]}
        self.assertEqual(by_id["python"]["status"], "PathMismatch")
        self.assertEqual(by_id["dotnet"]["status"], "SourceBehind")
        self.assertEqual(set(report["remainingRequired"]), {"python", "dotnet"})
        self.assertEqual(report["exitCode"], 1)
        self.assertEqual(data["calls"], [])

    def test_source_failure_does_not_hide_other_toolchains(self):
        fixture = self.engine_fixture()
        fixture["failRemote"] = True
        report = run_ps(ENGINE, fixture)["result"]
        self.assertEqual(report["remainingRequired"], ["python"])
        self.assertEqual(report["exitCode"], 1)
        self.assertEqual(len(report["mslToolchains"]), 6)

    def test_preview_only_plans_the_outdated_toolchain(self):
        fixture = self.engine_fixture()
        fixture["preview"] = True
        fixture["installed"]["python"] = fixture["active"]["python"] = "3.9.6"
        data = run_ps(ENGINE, fixture)
        by_id = {item["toolchainId"]: item for item in data["result"]["mslToolchains"]}
        self.assertEqual(by_id["python"]["status"], "Planned")
        self.assertEqual(data["result"]["remainingRequired"], ["python"])
        self.assertTrue(all("source update" in call for call in data["calls"]))

    def test_current_swift_with_profile_drift_plans_activation_without_install(self):
        with tempfile.TemporaryDirectory() as temporary:
            directory = Path(temporary)
            user_profile = directory / ".zshrc"
            user_profile.write_text("# user-owned configuration\n", encoding="utf-8")
            fixture = self.engine_fixture()
            fixture.update(platform="Darwin", preview=True, userHome=temporary)
            data = run_ps(ENGINE, fixture)
            swift = next(item for item in data["result"]["mslToolchains"] if item["toolchainId"] == "swift")
            self.assertEqual(swift["status"], "Planned")
            self.assertTrue(swift["environmentChanged"])
            self.assertEqual(user_profile.read_text(), "# user-owned configuration\n")
            self.assertEqual(list(directory.iterdir()), [user_profile])
            self.assertFalse(any("install" in call or "upgrade" in call for call in data["calls"]))

    def test_windows_ids_are_derived_from_release_and_validated(self):
        data = run_ps(r"""
$answer=& (Get-Module msl-toolchains) {
    $bad=$false
    try { Expand-HBMslPackage 'bad;command' '3.15.0' } catch { $bad=$true }
    @{ python=(Expand-HBMslPackage 'Python.Python.{major}.{minor}' '3.15.0');
       java=(Expand-HBMslPackage 'EclipseAdoptium.Temurin.{major}.JDK' '27.0.0+35'); rejected=$bad }
}
$answer | ConvertTo-Json
""")
        self.assertEqual(data["python"], "Python.Python.3.15")
        self.assertEqual(data["java"], "EclipseAdoptium.Temurin.27.JDK")
        self.assertTrue(data["rejected"])

    def test_prerelease_compilers_never_count_as_stable(self):
        data = run_ps(r"""
$answer=& (Get-Module msl-toolchains) {
    param($cli)
    $script:texts=@{dotnet='11.0.100-preview.1';go='go version go1.28rc1 darwin/arm64';
        java='openjdk version "28-ea"';python='Python 3.16.0a1';rust='rustc 1.100.0-nightly';
        swift='Swift version 6.5-dev'}
    $versions=@()
    foreach($definition in $cli['mslToolchains']) {
        $script:currentText=$script:texts[$definition['id']]
        function script:Invoke-HBMslProcess {param($Command,$Arguments,$Directory)
            @{ExitCode=0;Output=$script:currentText;Executable='/fixture/compiler'}}
        $probe=Get-HBMslProbe $definition 'Darwin' ([IO.Path]::GetTempPath())
        $versions+=$probe.Version
    }
    @{versions=$versions}
} $cli
$answer | ConvertTo-Json -Depth 3
""")
        self.assertTrue(all(not version for version in data["versions"]), data)

    def test_command_probe_has_a_hard_timeout(self):
        data = run_ps(r"""
$answer=& (Get-Module msl-toolchains) {
    $result=Invoke-HBMslProcess pwsh @('-NoProfile','-Command','Start-Sleep -Seconds 20') ([IO.Path]::GetTempPath()) 1
    @{exitCode=$result.ExitCode;timedOut=$result.TimedOut}
}
$answer | ConvertTo-Json
""")
        self.assertEqual(data["exitCode"], 124)
        self.assertTrue(data["timedOut"])

    def test_old_or_nightly_cargo_is_not_a_current_stable_rust_toolchain(self):
        data = run_ps(r"""
$answer=& (Get-Module msl-toolchains) {
    param($cli)
    $definition=@($cli['mslToolchains'] | Where-Object {$_['id'] -eq 'rust'})[0]
    function script:Invoke-HBMslProcess {
        param($Command,$Arguments,$Directory)
        $text=if($Command -eq 'rustc'){'rustc 1.99.1 (hash)'}else{'cargo '+$script:cargoVersion}
        @{ExitCode=0;Output=$text;Executable='/fixture/'+$Command}
    }
    $versions=@()
    foreach($value in @('1.70.0','1.99.0','1.100.0-nightly')) {
        $script:cargoVersion=$value
        $versions+=(Get-HBMslProbe $definition 'Darwin' ([IO.Path]::GetTempPath())).Version
    }
    @{versions=$versions}
} $cli
$answer | ConvertTo-Json -Depth 3
""")
        self.assertEqual(data["versions"], ["", "1.99.1", ""])

    def test_parent_rejects_incomplete_or_false_green_msl_evidence(self):
        fixture = self.engine_fixture()
        fixture["platform"] = "Darwin"
        report = run_ps(ENGINE, fixture)["result"]
        # The fake Swift path intentionally leaves a path mismatch on Darwin.
        with tempfile.TemporaryDirectory() as temporary:
            path = Path(temporary) / "report.json"
            path.write_text(json.dumps(report), encoding="utf-8")
            helper = ROOT / "scripts/lib/linux-maintenance-hardening.py"
            first = subprocess.run([shutil.which("python3.14") or shutil.which("python3"), str(helper),
                                    "inspect-result", "--input", str(path)], capture_output=True, text=True)
            self.assertEqual(first.returncode, 0, first.stderr + first.stdout)
            report["remainingRequired"] = []
            report["exitCode"] = 0
            report["overallStatus"] = "SUCCESS"
            path.write_text(json.dumps(report), encoding="utf-8")
            second = subprocess.run([shutil.which("python3.14") or shutil.which("python3"), str(helper),
                                     "inspect-result", "--input", str(path)], capture_output=True, text=True)
            self.assertNotEqual(second.returncode, 0)

    def test_unapproved_release_urls_are_rejected_without_network(self):
        data = run_ps(r"""
$answer=& (Get-Module msl-toolchains) {
    $rejected=0
    foreach($uri in @('http://www.python.org/api/','https://user@www.python.org/api/',
        'https://www.python.org.attacker.invalid/releases','file:///tmp/releases')) {
        try {Get-HBMslRemoteData $uri} catch {$rejected++}
    }
    @{rejected=$rejected}
}
$answer | ConvertTo-Json
""")
        self.assertEqual(data["rejected"], 4)

    def test_managed_profiles_are_idempotent_and_preserve_user_content(self):
        data = run_ps(r"""
$answer=& (Get-Module msl-toolchains) {
    $userRoot=Join-Path ([IO.Path]::GetTempPath()) ([IO.Path]::GetRandomFileName())
    [void][IO.Directory]::CreateDirectory($userRoot)
    $oldShell=$env:SHELL; $oldPath=$env:PATH
    try {
        $env:SHELL='/bin/zsh'
        $userProfile=Join-Path $userRoot '.zshrc'
        [IO.File]::WriteAllText($userProfile,"# preserve me`n")
        Set-HBMslUnixPath $userRoot $userRoot
        $first=[IO.File]::ReadAllText($userProfile)
        Set-HBMslUnixPath $userRoot $userRoot
        $second=[IO.File]::ReadAllText($userProfile)
        $launcher=Join-Path $userRoot 'swift'
        [IO.File]::WriteAllText($launcher,'user-owned')
        $refused=$false
        try {Set-HBMslManagedFile $launcher 'replacement' 'MANAGED'} catch {$refused=$true}
        $backupFiles=@(Get-ChildItem -LiteralPath $userRoot -Force -Filter '*.hb-backup-*')
        @{ same=($first -eq $second); preserved=$second.Contains('# preserve me'); profile=$second;
           count=[regex]::Matches($second,'# BEGIN HOME-BASELINE MSL PATH').Count; refused=$refused;
           backups=$backupFiles.Count; backupMode=if($IsWindows){'N/A'}else{[int][IO.File]::GetUnixFileMode($backupFiles[0].FullName)} }
    } finally { $env:SHELL=$oldShell; $env:PATH=$oldPath; Remove-Item $userRoot -Recurse -Force }
}
$answer | ConvertTo-Json
""")
        self.assertTrue(data["same"] and data["preserved"] and data["refused"])
        self.assertEqual(data["count"], 1)
        self.assertEqual(data["backups"], 1)
        if os.name != "nt":
            self.assertEqual(data["backupMode"] & 0o077, 0)
        if os.name != "nt":
            with tempfile.TemporaryDirectory() as temporary:
                home = Path(temporary)
                profile = home / "profile"
                profile.write_text(data["profile"], encoding="utf-8")
                managed = str(home / ".home-baseline/toolchains/bin")
                for shell in ("bash", "zsh"):
                    executable = shutil.which(shell)
                    if executable:
                        env = dict(os.environ, HOME=str(home), PATH=f"/usr/bin:{managed}:/bin:{managed}")
                        result = subprocess.run([executable, "-c", '. "$1"; . "$1"; printf "%s" "$PATH"',
                                                 "fixture", str(profile)], capture_output=True, text=True, env=env)
                        self.assertEqual(result.returncode, 0, result.stderr)
                        self.assertEqual(result.stdout.split(os.pathsep)[0], managed)
                        self.assertEqual(result.stdout.split(os.pathsep).count(managed), 1)

    @unittest.skipIf(os.name == "nt", "Native Windows uses the PowerShell entrypoint")
    def test_msl_only_bash_delegates_before_unrelated_maintenance(self):
        with tempfile.TemporaryDirectory() as temporary:
            directory = Path(temporary)
            spy = directory / "pwsh"
            spy.write_text('#!/bin/sh\nprintf "%s\\n" "$@"\n', encoding="utf-8")
            spy.chmod(0o755)
            env = dict(os.environ, PATH=str(directory) + os.pathsep + os.environ["PATH"])
            result = subprocess.run(["bash", str(ROOT / "scripts/maintain-agentic-brew-apps.sh"),
                                     "--msl-only", "--compare-only"], capture_output=True, text=True, env=env)
            self.assertEqual(result.returncode, 0, result.stderr)
            self.assertIn("invoke-msl-toolchains.ps1", result.stdout)
            self.assertIn("-CompareOnly", result.stdout)
            self.assertNotIn("npm", result.stdout)
            self.assertNotIn("vscode", result.stdout)

    @unittest.skipIf(os.name == "nt", "Native Windows already runs PowerShell")
    def test_missing_shared_engine_cannot_return_success(self):
        with tempfile.TemporaryDirectory() as temporary:
            directory = Path(temporary)
            for name in ("uname", "dirname", "mkdir", "mktemp", "mv"):
                (directory / name).symlink_to(shutil.which(name))
            report = directory / "result.json"
            result = subprocess.run(["/bin/bash", str(ROOT / "scripts/maintain-agentic-brew-apps.sh"),
                                     "--msl-only", "--compare-only", "--result-file", str(report)],
                                    capture_output=True, text=True, env=dict(os.environ, PATH=str(directory)))
            self.assertEqual(result.returncode, 2, result.stderr)
            data = json.loads(report.read_text())
            self.assertEqual(data["overallStatus"], "FAILED")
            self.assertEqual(data["failureClass"], "MslEngineUnavailable")


if __name__ == "__main__":
    unittest.main()
