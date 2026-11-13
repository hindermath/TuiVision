#!/usr/bin/env bash

# Shared Forgejo/Codeberg API support. Tokens are obtained through Git's
# credential protocol and are never placed in URLs or process arguments.

HB_FORGEJO_USERNAME=""
HB_FORGEJO_TOKEN=""
HB_FORGEJO_HTTP_STATUS=""
HB_FORGEJO_HTTP_BODY=""
HB_FORGEJO_REPOSITORY_OWNER=""
HB_FORGEJO_REPOSITORY_NAME=""
HB_FORGEJO_CLONE_URL=""
HB_FORGEJO_HTML_URL=""
HB_FORGEJO_REPOSITORY_CREATED=0

hb_forgejo_validate_base_url() {
  local base_url="$1"
  python3 -c '
import sys
from urllib.parse import urlsplit

raw = sys.argv[1]
if any(ord(char) < 32 or ord(char) == 127 for char in raw) or any(char in raw for char in "\"\\ "):
    raise SystemExit(2)
parsed = urlsplit(raw)
if (parsed.scheme != "https" or not parsed.hostname or parsed.username is not None
        or parsed.password is not None or parsed.query or parsed.fragment
        or parsed.path not in ("", "/")):
    raise SystemExit(2)
print(raw.rstrip("/"))
' "$base_url" || {
    echo "Fehler: Die Forgejo-Basis-URL muss ein einzelner HTTPS-Origin ohne Zugangsdaten, Pfad oder Steuerzeichen sein." >&2
    echo "Error: The Forgejo base URL must be one HTTPS origin without credentials, path, or control characters." >&2
    return 2
  }
}

hb_forgejo_resolve_base_url() {
  local platform="$1"
  local configured_url="${2:-}"
  local resolved_url=""

  case "$platform" in
    codeberg)
      if [ -n "$configured_url" ]; then
        echo "Fehler: --forgejo-url darf nicht mit --platform codeberg kombiniert werden." >&2
        echo "Error: --forgejo-url cannot be combined with --platform codeberg." >&2
        return 2
      fi
      resolved_url="https://codeberg.org"
      ;;
    forgejo)
      if [ -z "$configured_url" ]; then
        echo "Fehler: --platform forgejo erfordert --forgejo-url https://HOST." >&2
        echo "Error: --platform forgejo requires --forgejo-url https://HOST." >&2
        return 2
      fi
      resolved_url="$configured_url"
      ;;
    *)
      echo "Fehler: Keine Forgejo-Plattform: $platform" >&2
      echo "Error: Not a Forgejo platform: $platform" >&2
      return 2
      ;;
  esac

  hb_forgejo_validate_base_url "$resolved_url"
}

hb_forgejo_require_tools() {
  local missing=0

  for command_name in git curl python3; do
    if ! command -v "$command_name" >/dev/null 2>&1; then
      echo "Fehler: Erforderliches Kommando fehlt: $command_name" >&2
      echo "Error: Required command is missing: $command_name" >&2
      missing=1
    fi
  done
  [ "$missing" -eq 0 ]
}

hb_forgejo_get_credential() {
  local base_url="$1"
  local helper=""
  local credential_output=""
  local line=""
  local restore_xtrace=0

  # Credential state is process-global for compatibility with existing callers.
  # Clear it before every fallible lookup so a previous host's token can never
  # survive into a later request.
  HB_FORGEJO_USERNAME=""
  HB_FORGEJO_TOKEN=""

  helper="$(git config --get-all credential.helper 2>/dev/null || true)"
  if [ -z "$helper" ]; then
    echo "Fehler: Kein sicherer Git-Credential-Helper ist konfiguriert." >&2
    echo "Konfiguriere auf macOS Keychain, unter Windows Git Credential Manager oder unter Linux einen institutionell freigegebenen Secret-Store." >&2
    echo "Error: No secure Git credential helper is configured." >&2
    return 2
  fi

  case "$-" in
    *x*) restore_xtrace=1; set +x ;;
  esac
  if ! credential_output="$(printf 'url=%s\n\n' "$base_url" | git credential fill)"; then
    [ "$restore_xtrace" -eq 1 ] && set -x
    echo "Fehler: Forgejo-Zugangsdaten konnten nicht ueber den Git-Credential-Helper gelesen werden." >&2
    echo "Error: Forgejo credentials could not be read through the Git credential helper." >&2
    return 2
  fi

  while IFS= read -r line; do
    case "$line" in
      username=*) HB_FORGEJO_USERNAME="${line#username=}" ;;
      password=*) HB_FORGEJO_TOKEN="${line#password=}" ;;
    esac
  done <<< "$credential_output"
  unset credential_output

  if [ -z "$HB_FORGEJO_USERNAME" ] || [ -z "$HB_FORGEJO_TOKEN" ]; then
    HB_FORGEJO_USERNAME=""
    HB_FORGEJO_TOKEN=""
    [ "$restore_xtrace" -eq 1 ] && set -x
    echo "Fehler: Benutzername oder Token fehlt im Git-Credential-Helper." >&2
    echo "Error: Username or token is missing from the Git credential helper." >&2
    return 2
  fi
  [ "$restore_xtrace" -eq 1 ] && set -x
}

hb_forgejo_approve_credential() {
  local base_url="$1"
  local restore_xtrace=0

  case "$-" in
    *x*) restore_xtrace=1; set +x ;;
  esac
  printf 'url=%s\nusername=%s\npassword=%s\n\n' \
    "$base_url" "$HB_FORGEJO_USERNAME" "$HB_FORGEJO_TOKEN" \
    | git credential approve
  [ "$restore_xtrace" -eq 1 ] && set -x
}

hb_forgejo_api_request() {
  local method="$1"
  local base_url="$2"
  local path="$3"
  local json_body="${4:-}"
  local response_file=""
  local data_file=""
  local header_file=""
  local restore_xtrace=0
  local status=""
  local -a curl_args=()

  base_url="$(hb_forgejo_validate_base_url "$base_url")" || return 2
  response_file="$(mktemp "${TMPDIR:-/tmp}/hb-forgejo-response.XXXXXX")"
  data_file="$(mktemp "${TMPDIR:-/tmp}/hb-forgejo-data.XXXXXX")"
  header_file="$(mktemp "${TMPDIR:-/tmp}/hb-forgejo-header.XXXXXX")"
  chmod 600 "$response_file" "$data_file" "$header_file"
  printf '%s' "$json_body" > "$data_file"
  if [[ "$HB_FORGEJO_TOKEN" == *$'\n'* || "$HB_FORGEJO_TOKEN" == *$'\r'* ]]; then
    rm -f "$response_file" "$data_file" "$header_file"
    echo "Error: Forgejo credential contains an invalid line break." >&2
    return 2
  fi
  printf 'Authorization: token %s\n' "$HB_FORGEJO_TOKEN" > "$header_file"

  curl_args=(
    --silent
    --show-error
    --output "$response_file"
    --write-out '%{http_code}'
    --url "$base_url$path"
    --request "$method"
    --header 'Accept: application/json'
    --header 'Content-Type: application/json'
    --header "@$header_file"
  )
  if [ -n "$json_body" ]; then
    curl_args+=(--data-binary "@$data_file")
  fi

  case "$-" in
    *x*) restore_xtrace=1; set +x ;;
  esac
  if ! status="$(curl "${curl_args[@]}")"; then
    [ "$restore_xtrace" -eq 1 ] && set -x
    rm -f "$response_file" "$data_file" "$header_file"
    echo "Fehler: Forgejo-API ist nicht erreichbar: $base_url" >&2
    echo "Error: Forgejo API is not reachable: $base_url" >&2
    return 2
  fi
  [ "$restore_xtrace" -eq 1 ] && set -x

  HB_FORGEJO_HTTP_STATUS="$status"
  HB_FORGEJO_HTTP_BODY="$(<"$response_file")"
  rm -f "$response_file" "$data_file" "$header_file"
}

hb_forgejo_json_field() {
  local field="$1"
  python3 -c 'import json,sys
data=json.load(sys.stdin)
value=data
for part in sys.argv[1].split("."):
    value=value.get(part, "") if isinstance(value, dict) else ""
print(value if value is not None else "")' "$field"
}

hb_forgejo_urlencode() {
  python3 -c 'import sys,urllib.parse; print(urllib.parse.quote(sys.argv[1], safe=""))' "$1"
}

hb_forgejo_report_http_error() {
  local action="$1"
  local status="$2"

  case "$status" in
    401) echo "Fehler: $action: Token ungueltig oder abgelaufen (HTTP 401)." >&2 ;;
    403) echo "Fehler: $action: Berechtigung oder Repository-Erstellung nicht erlaubt (HTTP 403)." >&2 ;;
    404) echo "Fehler: $action: API deaktiviert, falsche URL oder Repository nicht gefunden (HTTP 404)." >&2 ;;
    409) echo "Fehler: $action: Repository-Konflikt (HTTP 409)." >&2 ;;
    *) echo "Fehler: $action fehlgeschlagen (HTTP $status)." >&2 ;;
  esac
}

hb_forgejo_create_or_get_repository() {
  local base_url="$1"
  local repository_name="$2"
  local description="$3"
  local encoded_owner=""
  local encoded_repo=""
  local create_body=""

  hb_forgejo_require_tools || return 2
  base_url="$(hb_forgejo_validate_base_url "$base_url")" || return 2
  hb_forgejo_get_credential "$base_url" || return 2

  hb_forgejo_api_request GET "$base_url" /api/v1/user || return 2
  if [ "$HB_FORGEJO_HTTP_STATUS" != "200" ]; then
    hb_forgejo_report_http_error "Anmeldung pruefen" "$HB_FORGEJO_HTTP_STATUS"
    return 2
  fi
  HB_FORGEJO_REPOSITORY_OWNER="$(printf '%s' "$HB_FORGEJO_HTTP_BODY" | hb_forgejo_json_field login)"
  if [ -z "$HB_FORGEJO_REPOSITORY_OWNER" ]; then
    echo "Fehler: Forgejo-Benutzername fehlt in der API-Antwort." >&2
    return 2
  fi

  encoded_owner="$(hb_forgejo_urlencode "$HB_FORGEJO_REPOSITORY_OWNER")"
  encoded_repo="$(hb_forgejo_urlencode "$repository_name")"
  hb_forgejo_api_request GET "$base_url" "/api/v1/repos/$encoded_owner/$encoded_repo" || return 2
  if [ "$HB_FORGEJO_HTTP_STATUS" = "200" ]; then
    HB_FORGEJO_REPOSITORY_CREATED=0
  elif [ "$HB_FORGEJO_HTTP_STATUS" = "404" ]; then
    create_body="$(python3 -c 'import json,sys; print(json.dumps({"name":sys.argv[1],"description":sys.argv[2],"private":True,"auto_init":False,"default_branch":"main"}, ensure_ascii=False))' "$repository_name" "$description")"
    hb_forgejo_api_request POST "$base_url" /api/v1/user/repos "$create_body" || return 2
    if [ "$HB_FORGEJO_HTTP_STATUS" != "201" ]; then
      hb_forgejo_report_http_error "Repository erstellen" "$HB_FORGEJO_HTTP_STATUS"
      return 2
    fi
    HB_FORGEJO_REPOSITORY_CREATED=1
  else
    hb_forgejo_report_http_error "Repository pruefen" "$HB_FORGEJO_HTTP_STATUS"
    return 2
  fi

  local validated_repository=""
  if ! validated_repository="$(printf '%s' "$HB_FORGEJO_HTTP_BODY" | python3 -c '
import json, sys
from urllib.parse import urlsplit

base, expected_owner, expected_name = sys.argv[1:4]
data = json.load(sys.stdin)
base_url = urlsplit(base)
clone = str(data.get("clone_url") or "")
html = str(data.get("html_url") or "")
owner = str((data.get("owner") or {}).get("login") or "")
name = str(data.get("name") or "")
private = data.get("private") is True
clone_url = urlsplit(clone)
html_url = urlsplit(html)
expected_path = f"/{expected_owner}/{expected_name}"
valid = (
    base_url.scheme == "https"
    and owner == expected_owner
    and name == expected_name
    and private
    and clone_url.scheme == "https"
    and clone_url.netloc == base_url.netloc
    and not clone_url.username
    and not clone_url.password
    and not clone_url.query
    and not clone_url.fragment
    and clone_url.path.rstrip("/") == expected_path + ".git"
    and html_url.scheme == "https"
    and html_url.netloc == base_url.netloc
    and not html_url.username
    and not html_url.password
    and not html_url.query
    and not html_url.fragment
    and html_url.path.rstrip("/") == expected_path
)
if not valid:
    raise SystemExit(2)
print(name, clone, html, sep="\t")
' "$base_url" "$HB_FORGEJO_REPOSITORY_OWNER" "$repository_name")"; then
    echo "Fehler: Forgejo-Repository stimmt nicht mit privatem authentifiziertem Ziel ueberein." >&2
    return 2
  fi
  IFS=$'\t' read -r HB_FORGEJO_REPOSITORY_NAME HB_FORGEJO_CLONE_URL HB_FORGEJO_HTML_URL <<< "$validated_repository"

  hb_forgejo_approve_credential "$base_url"
}

hb_forgejo_delete_repository() {
  local base_url="$1"
  local owner="$2"
  local repository_name="$3"
  local encoded_owner=""
  local encoded_repo=""

  hb_forgejo_require_tools || return 2
  base_url="$(hb_forgejo_validate_base_url "$base_url")" || return 2
  hb_forgejo_get_credential "$base_url" || return 2
  encoded_owner="$(hb_forgejo_urlencode "$owner")"
  encoded_repo="$(hb_forgejo_urlencode "$repository_name")"
  hb_forgejo_api_request DELETE "$base_url" "/api/v1/repos/$encoded_owner/$encoded_repo" || return 2
  if [ "$HB_FORGEJO_HTTP_STATUS" != "204" ]; then
    hb_forgejo_report_http_error "Repository loeschen" "$HB_FORGEJO_HTTP_STATUS"
    return 2
  fi
}
