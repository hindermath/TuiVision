import AxeBuilder from '@axe-core/playwright';
import { expect, test } from '@playwright/test';
import type { Browser } from '@playwright/test';

const wcagTags = ['wcag2a', 'wcag2aa', 'wcag21a', 'wcag21aa', 'wcag22aa'];

async function expectNoSeriousAxeViolations(
  browser: Browser,
  baseURL: string,
  pagePath: string): Promise<void> {
  const context = await browser.newContext({ baseURL });
  const page = await context.newPage();

  try {
    await page.goto(pagePath);
    await page.waitForLoadState('networkidle');

    const results = await new AxeBuilder({ page })
      .withTags(wcagTags)
      .analyze();

    const seriousViolations = results.violations.filter(
      (violation) => violation.impact === 'serious' || violation.impact === 'critical');

    expect(seriousViolations, `serious or critical axe violations on ${pagePath}`).toEqual([]);
  }
  finally {
    await context.close();
  }
}

test.describe('DocFX accessibility smoke tests', () => {
  test('landing page exposes language, skip link and stable heading structure', async ({ page }) => {
    await page.goto('/');
    await expect(page.locator('html')).toHaveAttribute('lang', /de/i);
    await expect(page.locator('.skip-link').first()).toContainText('Hauptinhalt');
    await expect(page.locator('main#main-content')).toHaveCount(1);
    await expect(page.locator('article h1')).toHaveCount(1);
  });

  test('representative pages stay free of serious axe violations', async ({ baseURL, browser }) => {
    await expectNoSeriousAxeViolations(browser, baseURL!, '/');
    await expectNoSeriousAxeViolations(browser, baseURL!, '/api/TuiVision.Controls.TView.html');
    await expectNoSeriousAxeViolations(browser, baseURL!, '/docs/project-statistics.html');
  });
});
