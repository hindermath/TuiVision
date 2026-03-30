const hideDecorativeIcons = () => {
  for (const icon of document.querySelectorAll('.bi')) {
    icon.setAttribute('aria-hidden', 'true');
  }
};

const hideEmptyLandmarks = () => {
  for (const selector of ['#breadcrumb', '#affix', '#search-results']) {
    const element = document.querySelector(selector);
    if (!element) {
      continue;
    }

    const hasReadableContent = (element.textContent || '').trim().length > 0;
    element.hidden = !hasReadableContent;
  }
};

const normalizeLandingHeadings = () => {
  if (document.body.dataset.layout !== 'landing') {
    return;
  }

  const headings = document.querySelectorAll('article h1');
  if (headings.length > 1) {
    headings[0].setAttribute('aria-hidden', 'true');
  }
};

const normalizeDropdownToggles = () => {
  for (const toggle of document.querySelectorAll('a.dropdown-toggle[aria-expanded]')) {
    toggle.removeAttribute('aria-expanded');
  }
};

document.documentElement.lang ||= 'de';
hideDecorativeIcons();
hideEmptyLandmarks();
normalizeLandingHeadings();
normalizeDropdownToggles();

const observer = new MutationObserver(() => {
  hideDecorativeIcons();
  hideEmptyLandmarks();
  normalizeDropdownToggles();
});

observer.observe(document.body, {
  childList: true,
  subtree: true
});
