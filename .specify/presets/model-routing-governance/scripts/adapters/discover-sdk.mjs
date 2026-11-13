// DE: Nur Metadaten; kein Prompt, keine Installation. EN: Metadata only; no prompt/install.
import { pathToFileURL } from 'node:url';
import { isAbsolute } from 'node:path';

export async function discover(family, sdk, executable) {
  if (family === 'Claude') {
    // An idle streaming input starts the control plane without an inference turn.
    let finish;
    const pending = new Promise(resolve => { finish = resolve; });
    const query = sdk.query({
      prompt: (async function* () { await pending; })(),
      options: {
        pathToClaudeCodeExecutable: executable, persistSession: false,
        settingSources: [], tools: [], mcpServers: {},
        permissionMode: 'plan', extraArgs: { 'strict-mcp-config': null },
      },
    });
    try { return { models: await query.supportedModels() }; }
    finally { finish(); query.close(); }
  }
  if (family === 'Copilot') {
    // Use the selected installed CLI, never an SDK-bundled replacement runtime.
    const client = new sdk.CopilotClient({
      connection: sdk.RuntimeConnection.forStdio({ path: executable }),
    });
    try { await client.start(); return { models: await client.listModels() }; }
    finally { await client.stop(); }
  }
  throw new Error('Unsupported SDK family');
}

if (process.argv[1] && import.meta.url === pathToFileURL(process.argv[1]).href) {
  const [family, modulePath, executable] = process.argv.slice(2);
  try {
    if (!isAbsolute(modulePath) || !isAbsolute(executable)) throw new Error('Absolute local paths required');
    const sdk = await import(pathToFileURL(modulePath).href);
    const result = await discover(family, sdk, executable);
    const json = JSON.stringify(result);
    if (json.length > 4 * 1024 * 1024) throw new Error('Inventory too large');
    process.stdout.write(json);
  } catch {
    // SDK errors may contain tokens, paths or endpoint URLs. Keep the public failure bounded.
    process.stderr.write('SDK metadata discovery blocked; verify SDK/CLI compatibility and authentication.\n');
    process.exitCode = 3;
  }
}
