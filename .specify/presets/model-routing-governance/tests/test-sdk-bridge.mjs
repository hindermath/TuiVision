import { test } from 'node:test';
import assert from 'node:assert/strict';
import { discover } from '../scripts/adapters/discover-sdk.mjs';

test('Claude only initializes metadata control plane and closes idle input', async () => {
  let closed = false;
  let input;
  const models = [{ value: 'opus', supportsEffort: true, supportedEffortLevels: ['high'] }];
  const sdk = { query({ prompt, options }) {
    input = prompt;
    assert.equal(typeof prompt, 'object'); // A string would submit an inference turn.
    assert.equal(options.pathToClaudeCodeExecutable, '/fixture/claude');
    assert.equal(options.persistSession, false);
    assert.deepEqual(options.tools, []);
    assert.deepEqual(options.settingSources, []);
    return { supportedModels: async () => models, close() { closed = true; } };
  } };
  assert.deepEqual(await discover('Claude', sdk, '/fixture/claude'), { models });
  assert.equal(closed, true);
  assert.equal((await input.next()).done, true);
});

test('Claude closes on metadata failure', async () => {
  let closed = false;
  const sdk = { query: () => ({ supportedModels: async () => { throw Error('failed'); }, close() { closed = true; } }) };
  await assert.rejects(discover('Claude', sdk, '/fixture/claude'));
  assert.equal(closed, true);
});

test('Copilot uses selected CLI, starts/stops client, never creates a session', async () => {
  const calls = [];
  class CopilotClient {
    constructor(options) { assert.deepEqual(options, { connection: { path: '/fixture/copilot' } }); }
    async start() { calls.push('start'); }
    async listModels() { calls.push('list'); return []; }
    async stop() { calls.push('stop'); }
    createSession() { assert.fail('Discovery must never create inference sessions'); }
  }
  const sdk = { CopilotClient, RuntimeConnection: { forStdio: options => options } };
  assert.deepEqual(await discover('Copilot', sdk, '/fixture/copilot'), { models: [] });
  assert.deepEqual(calls, ['start', 'list', 'stop']);
});

test('Copilot stops after protocol/authentication failure', async () => {
  let stopped = false;
  class CopilotClient {
    async start() { throw Error('authentication'); }
    async stop() { stopped = true; }
  }
  await assert.rejects(discover('Copilot', { CopilotClient, RuntimeConnection: { forStdio: x => x } }, '/fixture/copilot'));
  assert.equal(stopped, true);
});

test('Unsupported SDK family fails closed', async () => {
  await assert.rejects(discover('Other', {}, '/fixture/cli'));
});
