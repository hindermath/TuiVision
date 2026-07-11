#!/usr/bin/env bash
# rename-lastenheft.sh - Lastenheft umbenennen / Rename Lastenheft
# FR-REV-B03; Contract: specs/016-secure-development-hardening/contracts/

set -euo pipefail

usage()
{
  cat <<'EOF'
Verwendung / Usage:
  bash scripts/rename-lastenheft.sh [--dry-run] [--no-commit] <lh-file> <branch-name>

Optionen / Options:
  --dry-run    Nur den geplanten Zielpfad zeigen; nichts ändern.
               Show the planned target path without changing anything.
  --no-commit  Mit git mv umbenennen, aber keinen Commit erzeugen.
               Rename with git mv but do not create a commit.
  -h, --help   Diese Hilfe anzeigen. / Show this help.

Ohne --no-commit wird nur der Rename als Commit erzeugt. Bereits vorgemerkte
fremde Änderungen bleiben im Index und werden nicht in diesen Commit aufgenommen.

Without --no-commit, only the rename is committed. Unrelated staged changes
remain in the index and are not included in that commit.
EOF
}

error()
{
  printf 'Fehler: %s\n' "$1" >&2
  printf 'Error: %s\n' "$2" >&2
  exit "${3:-1}"
}

dry_run=false
no_commit=false

while [ "$#" -gt 0 ]; do
  case "$1" in
    --dry-run) dry_run=true; shift ;;
    --no-commit) no_commit=true; shift ;;
    -h|--help) usage; exit 0 ;;
    --) shift; break ;;
    -*) error "Unbekannte Option: $1" "Unknown option: $1" 2 ;;
    *) break ;;
  esac
done

[ "$#" -eq 2 ] || { usage >&2; exit 2; }

lh_file=$1
branch_name=$2

git rev-parse --is-inside-work-tree >/dev/null 2>&1 \
  || error "Kein Git-Repository." "Not inside a Git repository."

[ -f "$lh_file" ] \
  || error "Datei nicht gefunden: $lh_file" "File not found: $lh_file"

filename=$(basename "$lh_file")
case "$filename" in
  Lastenheft*.md) ;;
  *) error "Quelle muss Lastenheft*.md sein." "Source must match Lastenheft*.md." ;;
esac

git ls-files --error-unmatch -- "$lh_file" >/dev/null 2>&1 \
  || error "Quelle ist nicht in Git verfolgt: $lh_file" "Source is not tracked by Git: $lh_file"

safe_branch=${branch_name//\//-}
case "$safe_branch" in
  *\\*|*..*) error "Unsicherer Branchname: $branch_name" "Unsafe branch name: $branch_name" ;;
esac
[[ "$safe_branch" =~ ^[A-Za-z0-9][A-Za-z0-9._-]*$ ]] \
  || error "Ungültiger Branchname: $branch_name" "Invalid branch name: $branch_name"

if [[ "$filename" == *."$safe_branch".md ]]; then
  printf 'INFO: Datei bereits korrekt benannt / File already named correctly: %s\n' "$filename"
  exit 0
fi

stem=${filename%.md}
new_name="${stem}.${safe_branch}.md"
target_dir=$(dirname "$lh_file")
new_path="${target_dir}/${new_name}"

[ ! -e "$new_path" ] \
  || error "Zieldatei existiert bereits: $new_path" "Target file already exists: $new_path"

if [ "$dry_run" = true ]; then
  printf 'DRY-RUN: %s -> %s\n' "$lh_file" "$new_path"
  exit 0
fi

git mv -- "$lh_file" "$new_path"

if [ "$no_commit" = true ]; then
  printf 'OK: Umbenannt ohne Commit / Renamed without commit: %s -> %s\n' "$filename" "$new_name"
  exit 0
fi

# --only hält bereits vorgemerkte fremde Änderungen aus dem Rename-Commit fern.
# --only keeps unrelated staged changes out of the rename commit.
commit_message="chore: rename Lastenheft to ${new_name}

Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>"
git commit --only -m "$commit_message" -- "$lh_file" "$new_path"

printf 'OK: Umbenannt und committed / Renamed and committed: %s -> %s\n' "$filename" "$new_name"
