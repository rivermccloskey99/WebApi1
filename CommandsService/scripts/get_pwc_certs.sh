#!/bin/bash
# Purpose: Fetch PwC certificates for adding to the tls trust bundle.
# This script runs in the docker "build" container.
# It is not used for local development.

# helper, modified from source: https://gist.github.com/cdown/1163649
urlencode() {
    old_lc_collate=$LC_COLLATE
    LC_COLLATE=C

    local length="${#1}"
    for (( i = 0; i < length; i++ )); do
        local c="${1:$i:1}"
        case $c in
            [a-zA-Z0-9.~_-]) printf '%s' "$c" ;;
            *) printf '%%%02X' "'$c" ;;
        esac
    done

    LC_COLLATE=$old_lc_collate
}

out_dir="/usr/local/share/ca-certificates"

certs=(
  "PwC Policy-1.crt"
  "MATLKPKI2CAP005.us.nam.ad.pwcinternal.com_PwC AURA Issuing - 1.crt"
  "MATLKPKI2CAP004.us.nam.ad.pwcinternal.com_PwC Mobility Issuing - 1(1).crt"
  "MATLKPKI2CAP004.us.nam.ad.pwcinternal.com_PwC Mobility Issuing - 1.crt"
  "MATLKPKI2CAP003.us.nam.ad.pwcinternal.com_PwC SSL Issuing - 1(2).crt"
  "MATLKPKI2CAP003.us.nam.ad.pwcinternal.com_PwC SSL Issuing - 1(1).crt"
  "MATLKPKI2CAP003.us.nam.ad.pwcinternal.com_PwC SSL Issuing - 1.crt"
  "MATLKPKI2CAP001_PwC Root-1.crt"
  "PwCRoot-3.crt"
  "PwCIssuing-3A.crt"
)

for cert in "${certs[@]}"; do
  echo $(urlencode "${cert}")
  output_path="${out_dir}/${cert}"
  certpath=$(printf "%s/%s" "http://getcrl.pwc.com" $(urlencode "${cert}"))
  openssl x509  -in <(curl -sS "${certpath}") -inform DER  \
                -out "${output_path}" -outform PEM 2>/dev/null \
          || curl -sS "${certpath}" -o "${output_path}"
  openssl x509 -in "${output_path}" -text
done