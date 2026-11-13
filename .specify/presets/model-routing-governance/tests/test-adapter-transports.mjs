import { test } from 'node:test';
import assert from 'node:assert/strict';
import { mkdtemp, writeFile, readFile, rm } from 'node:fs/promises';
import { tmpdir } from 'node:os';
import { join, resolve, delimiter } from 'node:path';
import { spawn } from 'node:child_process';
import { createServer } from 'node:http';

const resolver = resolve('scripts/resolve-model-routing.ps1');
const flags = '--model --effort --reasoning-effort --permission-mode --tools --strict-mcp-config --mcp-config --no-session-persistence --print --available-tools --deny-tool --disable-builtin-mcps --no-auto-update --no-ask-user --prompt --silent --variant --agent';

async function writeCli(dir, cli, help, ansi = false) {
  const windows = process.platform === 'win32';
  const body = windows
    ? `@echo off\r\necho fixture 1.0 ${help}\r\n`
    : ansi
      ? `#!/bin/sh\nprintf '\\033[31m%s\\033[0m\\n' 'fixture 1.0 ${help}'\n`
      : `#!/bin/sh\nprintf '%s\\n' 'fixture 1.0 ${help}'\n`;
  await writeFile(join(dir, cli + (windows ? '.cmd' : '')), body, { mode: 0o755 });
}

function nativeHarnessArguments(profile) {
  const index = profile.arguments.indexOf('-HarnessArgumentsJson');
  assert.ok(index >= 0 && index + 1 < profile.arguments.length, 'missing JSON harness argument transport');
  return JSON.parse(profile.arguments[index + 1]);
}

async function withFixture(run) {
  const dir = await mkdtemp(join(tmpdir(), 'routing-transport-'));
  try {
    for (const cli of ['claude', 'copilot', 'opencode']) {
      await writeCli(dir, cli, flags, cli === 'claude');
    }
    const execute = (args, environment = {}) => new Promise((resolveResult, reject) => {
      const child = spawn('pwsh', ['-NoProfile', '-File', resolver, ...args, '-OutputFormat', 'Json', '-ConfigPath', join(dir, 'profiles.json')], {
        env: { ...process.env, OPENCODE_SERVER_PASSWORD: '', ...environment, PATH: dir + delimiter + process.env.PATH },
      });
      let out = ''; let err = '';
      child.stdout.on('data', x => { out += x; }); child.stderr.on('data', x => { err += x; });
      child.on('error', reject);
      child.on('close', code => resolveResult({ code, out, err, result: JSON.parse(out) }));
    });
    await run({ dir, execute });
  } finally { await rm(dir, { recursive: true, force: true }); }
}

test('actual Node/PowerShell bridge transports Claude and Copilot metadata without inference', async () => {
  await withFixture(async ({ dir, execute }) => {
    const modulePath = join(dir, 'sdk.mjs');
    await writeFile(modulePath, `
      export function query({prompt, options}) {
        if (typeof prompt !== 'object' || options.persistSession !== false) throw Error('unsafe');
        return { supportedModels: async () => [
          {value:'sonnet', supportsEffort:true, supportedEffortLevels:['high','medium']}
        ], close() {} };
      }
      export const RuntimeConnection = {forStdio: x => x};
      export class CopilotClient {
        async start() {}
        async listModels() { return [
          {id:'gpt-99',policy:{state:'enabled'},capabilities:{supports:{reasoningEffort:true}},supportedReasoningEfforts:['high']},
          {id:'gpt-99-mini',policy:{state:'enabled'},capabilities:{supports:{reasoningEffort:false}}}
        ]; }
        async stop() {}
      }
    `);
    for (const family of ['Claude', 'Copilot']) {
      const reply = await execute(['-Action', 'Refresh', '-Harness', family, '-SdkModulePath', modulePath]);
      assert.equal(reply.code, 0, reply.out + reply.err);
      assert.equal(reply.result.roles.length, 4);
      assert.equal(reply.result.roles[0].model, family === 'Claude' ? 'sonnet' : 'gpt-99');
      if (family === 'Copilot') {
        const profile = JSON.parse(await readFile(join(dir, 'profiles.json'), 'utf8'));
        assert.ok(nativeHarnessArguments(profile.profiles['copilot-frontier-auto']).includes('--reasoning-effort'));
      }
    }
    const copilotCommon = '--model --available-tools --deny-tool --disable-builtin-mcps --no-auto-update --no-ask-user --prompt --silent';
    await writeCli(dir, 'copilot', `${copilotCommon} --effort`);
    const legacyFlag = await execute(['-Action', 'Refresh', '-Harness', 'Copilot', '-SdkModulePath', modulePath]);
    assert.equal(legacyFlag.code, 0, legacyFlag.out + legacyFlag.err);
    let profile = JSON.parse(await readFile(join(dir, 'profiles.json'), 'utf8'));
    assert.ok(nativeHarnessArguments(profile.profiles['copilot-frontier-auto']).includes('--effort'));
    assert.ok(!nativeHarnessArguments(profile.profiles['copilot-frontier-auto']).includes('--reasoning-effort'));
    await writeCli(dir, 'copilot', copilotCommon);
    const missingEffort = await execute(['-Action', 'Status', '-Harness', 'Copilot', '-SdkModulePath', modulePath]);
    assert.equal(missingEffort.code, 3);
    assert.match(missingEffort.result.reason, /reasoning-effort flag missing/);
    const before = await readFile(join(dir, 'profiles.json'), 'utf8');
    await writeFile(modulePath, 'throw Error("fixture-private-token-must-not-leak");');
    const blocked = await execute(['-Action', 'Refresh', '-Harness', 'Claude', '-SdkModulePath', modulePath]);
    assert.equal(blocked.code, 3);
    assert.equal(blocked.result.status, 'Blocked');
    assert.doesNotMatch(blocked.out + blocked.err, /fixture-private-token/);
    assert.equal(await readFile(join(dir, 'profiles.json'), 'utf8'), before);
    await writeFile(modulePath, `
      console.log('fixture-private-token-must-not-leak');
      export function query() { return { supportedModels: async () => [], close() {} }; }
    `);
    const malformed = await execute(['-Action', 'Refresh', '-Harness', 'Claude', '-SdkModulePath', modulePath]);
    assert.equal(malformed.code, 3);
    assert.doesNotMatch(malformed.out + malformed.err, /fixture-private-token/);
    assert.equal(await readFile(join(dir, 'profiles.json'), 'utf8'), before);
  });
});

test('OpenCode supports current v2 and legacy provider metadata without weakening filters', async () => {
  const requests = [];
  const authorizations = [];
  let mode = 'v2';
  let connected = ['anthropic'];
  let redirect = false;
  let activation = 'enabled';
  let modelEnabled = true;
  const server = createServer((req, res) => {
    requests.push(req.url);
    authorizations.push(req.headers.authorization ?? '');
    if (redirect) { res.writeHead(302, { Location: 'http://example.invalid/provider' }); res.end(); return; }
    if (mode === 'legacy' && req.url === '/api/provider') {
      res.setHeader('Content-Type', 'text/html'); res.end('<!doctype html>'); return;
    }
    res.setHeader('Content-Type', 'application/json');
    if (mode === 'v2' && req.url === '/api/provider') {
      res.end(JSON.stringify({ data: [{ id: 'anthropic', activation }, { id: 'other', activation: 'enabled' }] })); return;
    }
    if (mode === 'v2' && req.url === '/api/model') {
      res.end(JSON.stringify({ data: [
        {id:'gemini-99-pro',providerID:'anthropic',enabled:modelEnabled,status:'active',variants:[{id:'high'}]},
        {id:'gemini-99-flash',providerID:'anthropic',enabled:true,status:'active',variants:[{id:'medium'}]},
        {id:'claude-opus-99',providerID:'other',enabled:true,status:'active',variants:[{id:'high'}]},
      ] })); return;
    }
    if (mode === 'legacy' && req.url === '/provider') {
      res.end(JSON.stringify({ connected, all: [{ id: 'anthropic', models: {
        pro: {id:'gemini-99-pro',capabilities:{reasoning:true},variants:{high:{}}},
        flash: {id:'gemini-99-flash',capabilities:{reasoning:true},variants:{medium:{}}},
      } }] })); return;
    }
    res.writeHead(404); res.end('{}');
  });
  await new Promise(resolveListen => server.listen(0, '127.0.0.1', resolveListen));
  try {
    await withFixture(async ({ execute, dir }) => {
      const origin = `http://127.0.0.1:${server.address().port}`;
      const args = ['-Action','Refresh','-Harness','OpenCode','-OpenCodeServerUrl',origin,'-OpenCodeProvider','anthropic'];
      const reply = await execute(args);
      assert.equal(reply.code, 0, reply.out + reply.err);
      assert.equal(reply.result.roles[0].model, 'anthropic/gemini-99-pro');
      let parsedProfile = JSON.parse(await readFile(join(dir, 'profiles.json'), 'utf8'));
      assert.ok(nativeHarnessArguments(parsedProfile.profiles['opencode-frontier-auto']).includes('--variant'));
      const before = await readFile(join(dir, 'profiles.json'), 'utf8');
      activation = 'disabled';
      assert.equal((await execute(args)).code, 3);
      activation = 'enabled'; modelEnabled = false;
      assert.equal((await execute(args)).code, 3);
      modelEnabled = true; redirect = true;
      assert.equal((await execute(args)).code, 3);
      assert.equal(await readFile(join(dir, 'profiles.json'), 'utf8'), before);
      redirect = false; mode = 'legacy';
      assert.equal((await execute(args)).code, 0);
      const legacyProfile = await readFile(join(dir, 'profiles.json'), 'utf8');
      connected = [];
      assert.equal((await execute(args)).code, 3);
      assert.equal(await readFile(join(dir, 'profiles.json'), 'utf8'), legacyProfile);
      for (const url of ['http://example.invalid', origin + '/path', origin + '?token=secret', 'http://user:secret@127.0.0.1']) {
        const denied = await execute(['-Action','Status','-Harness','OpenCode','-OpenCodeServerUrl',url,'-OpenCodeProvider','anthropic']);
        assert.equal(denied.code, 3);
        assert.doesNotMatch(denied.out + denied.err, /token=secret|user:secret/);
      }
      assert.ok(requests.includes('/api/provider'));
      assert.ok(requests.includes('/api/model'));
      assert.ok(requests.includes('/provider'));
      connected = ['anthropic']; mode = 'v2'; activation = 'enabled'; modelEnabled = true;
      const requestCount = requests.length;
      const passwordEnvironment = { OPENCODE_SERVER_PASSWORD: 'fixture-secret' };
      const untrusted = await execute(args, passwordEnvironment);
      assert.equal(untrusted.code, 3);
      assert.equal(requests.length, requestCount, 'untrusted listener received a credential-bearing request');
      const trusted = await execute([...args, '-TrustOpenCodeLoopbackEndpoint'], passwordEnvironment);
      assert.equal(trusted.code, 0, trusted.out + trusted.err);
      assert.equal(authorizations.at(-1), `Basic ${Buffer.from('opencode:fixture-secret').toString('base64')}`);
      await writeCli(dir, 'opencode', '--model --agent provider/model#variant');
      const currentCli = await execute(args);
      assert.equal(currentCli.code, 0, currentCli.out + currentCli.err);
      parsedProfile = JSON.parse(await readFile(join(dir, 'profiles.json'), 'utf8'));
      const currentArgs = nativeHarnessArguments(parsedProfile.profiles['opencode-frontier-auto']);
      assert.ok(currentArgs.includes('{model}#{reasoningEffort}'));
      assert.ok(!currentArgs.includes('--variant'));
    });
  } finally { await new Promise(close => server.close(close)); }
});
