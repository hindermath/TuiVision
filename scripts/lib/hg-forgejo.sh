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

  case "$resolved_url" in
    https://*) ;;
    *)
      echo "Fehler: Die Forgejo-Basis-URL muss HTTPS verwenden." >&2
      echo "Error: The Forgejo base URL must use HTTPS." >&2
      return 2
      ;;
  esac

  case "$resolved_url" in
    *'@'*|*'?'*|*'#'*|*'"'*|*'\'*|*' '*)
      echo "Fehler: Die Forgejo-Basis-URL darf keine Zugangsdaten, Query, Fragmente oder Leerzeichen enthalten." >&2
      echo "Error: The Forgejo base URL must not contain credentials, query, fragments, or spaces." >&2
      return 2
      ;;
  esac

  printf '%s\n' "${resolved_url%/}"
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

  HB_FORGEJO_USERNAME=""
  HB_FORGEJO_TOKEN=""
  while IFS= read -r line; do
    case "$line" in
      username=*) HB_FORGEJO_USERNAME="${line#username=}" ;;
      password=*) HB_FORGEJO_TOKEN="${line#password=}" ;;
    esac
  done <<< "$credential_output"
  unset credential_output

  if [ -z "$HB_FORGEJO_USERNAME" ] || [ -z "$HB_FORGEJO_TOKEN" ]; then
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
  local restore_xtrace=0
  local status=""
  local -a curl_args=()

  response_file="$(mktemp "${TMPDIR:-/tmp}/hb-forgejo-response.XXXXXX")"
  data_file="$(mktemp "${TMPDIR:-/tmp}/hb-forgejo-data.XXXXXX")"
  chmod 600 "$response_file" "$data_file"
  printf '%s' "$json_body" > "$data_file"

  curl_args=(
    --silent
    --show-error
    --output "$response_file"
    --write-out '%{http_code}'
    --config -
  )
  if [ -n "$json_body" ]; then
    curl_args+=(--data-binary "@$data_file")
  fi

  case "$-" in
    *x*) restore_xtrace=1; set +x ;;
  esac
  if ! status="$({
    printf 'url = "%s%s"\n' "$base_url" "$path"
    printf 'request = "%s"\n' "$method"
    printf 'header = "Accept: application/json"\n'
    printf 'header = "Content-Type: application/json"\n'
    printf 'header = "Authorization: token %s"\n' "$HB_FORGEJO_TOKEN"
  } | curl "${curl_args[@]}")"; then
    [ "$restore_xtrace" -eq 1 ] && set -x
    rm -f "$response_file" "$data_file"
    echo "Fehler: Forgejo-API ist nicht erreichbar: $base_url" >&2
    echo "Error: Forgejo API is not reachable: $base_url" >&2
    return 2
  fi
  [ "$restore_xtrace" -eq 1 ] && set -x

  HB_FORGEJO_HTTP_STATUS="$status"
  HB_FORGEJO_HTTP_BODY="$(<"$response_file")"
  rm -f "$response_file" "$data_file"
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

  hb_forgejo_require_tools
  hb_forgejo_get_credential "$base_url"

  hb_forgejo_api_request GET "$base_url" /api/v1/user
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
  hb_forgejo_api_request GET "$base_url" "/api/v1/repos/$encoded_owner/$encoded_repo"
  if [ "$HB_FORGEJO_HTTP_STATUS" = "200" ]; then
    HB_FORGEJO_REPOSITORY_CREATED=0
  elif [ "$HB_FORGEJO_HTTP_STATUS" = "404" ]; then
    create_body="$(python3 -c 'import json,sys; print(json.dumps({"name":sys.argv[1],"description":sys.argv[2],"private":True,"auto_init":False,"default_branch":"main"}, ensure_ascii=False))' "$repository_name" "$description")"
    hb_forgejo_api_request POST "$base_url" /api/v1/user/repos "$create_body"
    if [ "$HB_FORGEJO_HTTP_STATUS" != "201" ]; then
      hb_forgejo_report_http_error "Repository erstellen" "$HB_FORGEJO_HTTP_STATUS"
      return 2
    fi
    HB_FORGEJO_REPOSITORY_CREATED=1
  else
    hb_forgejo_report_http_error "Repository pruefen" "$HB_FORGEJO_HTTP_STATUS"
    return 2
  fi

  HB_FORGEJO_REPOSITORY_NAME="$(printf '%s' "$HB_FORGEJO_HTTP_BODY" | hb_forgejo_json_field name)"
  HB_FORGEJO_CLONE_URL="$(printf '%s' "$HB_FORGEJO_HTTP_BODY" | hb_forgejo_json_field clone_url)"
  HB_FORGEJO_HTML_URL="$(printf '%s' "$HB_FORGEJO_HTTP_BODY" | hb_forgejo_json_field html_url)"
  if [ -z "$HB_FORGEJO_REPOSITORY_NAME" ] || [ -z "$HB_FORGEJO_CLONE_URL" ] || [ -z "$HB_FORGEJO_HTML_URL" ]; then
    echo "Fehler: Unvollstaendige Forgejo-Repository-Antwort." >&2
    return 2
  fi

  hb_forgejo_approve_credential "$base_url"
}

hb_forgejo_delete_repository() {
  local base_url="$1"
  local owner="$2"
  local repository_name="$3"
  local encoded_owner=""
  local encoded_repo=""

  hb_forgejo_require_tools
  hb_forgejo_get_credential "$base_url"
  encoded_owner="$(hb_forgejo_urlencode "$owner")"
  encoded_repo="$(hb_forgejo_urlencode "$repository_name")"
  hb_forgejo_api_request DELETE "$base_url" "/api/v1/repos/$encoded_owner/$encoded_repo"
  if [ "$HB_FORGEJO_HTTP_STATUS" != "204" ]; then
    hb_forgejo_report_http_error "Repository loeschen" "$HB_FORGEJO_HTTP_STATUS"
    return 2
  fi
}
