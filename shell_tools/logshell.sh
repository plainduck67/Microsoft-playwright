#!/usr/bin/env bash
#
# Usage: ./logshell.sh /path/to/output.log
#

if [ -z "$1" ]; then
  echo "Usage: $0 /path/to/logfile"
  exit 1
fi

LOGFILE="$1"

# Start a new Bash shell with logging enabled
bash --rcfile <(cat <<EOF
# Redirect stdout/stderr to both screen and logfile
exec > >(tee -a "$LOGFILE") 2>&1

# Log each command before it executes
trap 'printf "\\n# %s\\n$ %s\\n" "\$(date +"%Y-%m-%dT%H:%M:%S%z")" "\$BASH_COMMAND" >> "$LOGFILE"' DEBUG

# Log return codes after each command
PROMPT_COMMAND='echo "# exit: \$?" >> "$LOGFILE"'
EOF
)
