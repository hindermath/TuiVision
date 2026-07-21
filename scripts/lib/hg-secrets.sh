#!/usr/bin/env bash
# hg-secrets.sh — Secret-Pattern-Erkennung (FR-003, R-01)
# Matched value wird IMMER als [REDACTED] ausgegeben
# Gibt aus: FAIL|<filepath>:<line>|secret-detected

# Secret file name patterns (basenames that suggest credential storage)
HG_SECRET_FILENAMES="(password|passwd|secret|credential|token|apikey|api_key|private_key|id_rsa|id_dsa|id_ecdsa|id_ed25519|\.env|\.netrc|\.htpasswd)"

# Secret content patterns
HG_SECRET_PATTERNS='(ghp_[A-Za-z0-9]{36}|ghs_[A-Za-z0-9]{36}|github_pat_[A-Za-z0-9_]{82}|sk-[A-Za-z0-9]{48}|AKIA[A-Z0-9]{16}|AIzaSy[A-Za-z0-9_-]{33}|-----BEGIN (RSA |EC |OPENSSH |DSA )?PRIVATE KEY-----)'

hg_check_secrets() {
  local dir="$1"
  local found=0

  # 1. Check for secret-named files
  local secret_file
  while IFS= read -r secret_file; do
    local basename
    basename=$(basename "$secret_file")
    if echo "$basename" | grep -qiE "$HG_SECRET_FILENAMES"; then
      echo "FAIL|${secret_file}:0|secret-filename-detected [REDACTED]"
      found=1
    fi
  done < <(find "$dir" -maxdepth 3 -type f 2>/dev/null | grep -v '\.git/' | sort)

  # 2. Check for secret content patterns in text files
  if command -v rg &>/dev/null; then
    local match
    while IFS= read -r match; do
      # Extract file and line number, redact the actual value
      local filepath linenum
      filepath=$(echo "$match" | cut -d: -f1)
      linenum=$(echo "$match" | cut -d: -f2)
      echo "FAIL|${filepath}:${linenum}|secret-pattern-detected [REDACTED]"
      found=1
    done < <(rg -n --no-heading -l "${HG_SECRET_PATTERNS}" "$dir" 2>/dev/null \
      | grep -v '\.git/' \
      | while IFS= read -r f; do
          rg -n --no-heading "${HG_SECRET_PATTERNS}" "$f" 2>/dev/null \
            | sed "s|^|${f}:|" \
            | sed 's/\(ghp_[A-Za-z0-9]*\|ghs_[A-Za-z0-9]*\|github_pat_[A-Za-z0-9_]*\|sk-[A-Za-z0-9]*\|AKIA[A-Z0-9]*\|AIzaSy[A-Za-z0-9_-]*\)/[REDACTED]/g'
        done)
  fi

  return $found
}

# Simpler single-file secret check — returns 0=clean, 1=secrets found
hg_scan_file_secrets() {
  local file="$1"
  local found=0

  if ! [ -f "$file" ]; then
    return 0
  fi

  if command -v rg &>/dev/null; then
    local hits
    hits=$(rg -c "${HG_SECRET_PATTERNS}" "$file" 2>/dev/null || echo 0)
    if [ "$hits" -gt 0 ]; then
      local linenum
      while IFS= read -r linenum; do
        echo "FAIL|${file}:${linenum}|secret-pattern-detected [REDACTED]"
        found=1
      done < <(rg -n --no-heading "${HG_SECRET_PATTERNS}" "$file" 2>/dev/null | cut -d: -f1)
    fi
  fi

  return $found
}
