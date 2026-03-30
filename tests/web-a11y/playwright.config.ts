import path from 'node:path';
import { defineConfig, devices } from '@playwright/test';

const port = Number.parseInt(process.env.PLAYWRIGHT_DOCFX_PORT ?? '8123', 10);
const baseURL = process.env.PLAYWRIGHT_BASE_URL ?? `http://127.0.0.1:${port}`;
const skipWebServer = process.env.PLAYWRIGHT_SKIP_WEBSERVER === '1';

const config = defineConfig({
  testDir: path.join(__dirname, 'specs'),
  fullyParallel: true,
  forbidOnly: Boolean(process.env.CI),
  retries: process.env.CI ? 2 : 0,
  reporter: [
    ['list'],
    ['html', { open: 'never', outputFolder: path.join(__dirname, 'playwright-report') }]
  ],
  use: {
    baseURL,
    headless: true,
    trace: 'on-first-retry',
    screenshot: 'only-on-failure',
    video: 'retain-on-failure'
  },
  projects: [
    {
      name: 'chromium',
      use: { ...devices['Desktop Chrome'] }
    }
  ]
});

if (!skipWebServer) {
  config.webServer = {
    command: `python3 -m http.server ${port} --directory _site`,
    cwd: path.resolve(__dirname, '../..'),
    url: baseURL,
    reuseExistingServer: !process.env.CI,
    timeout: 30_000
  };
}

export default config;
