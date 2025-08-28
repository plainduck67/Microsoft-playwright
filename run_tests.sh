
#!/usr/bin/env bash
set -e

echo "[run_tests.sh] Building solution..."
dotnet build

echo "[run_tests.sh] Running --init to create a run folder..."
dotnet run --project ClickAdventure.Runner -- --init

RUNS_DIR="ClickAdventure.Runner/runs"
LATEST_RUN=$(ls -td "$RUNS_DIR"/*/ | head -1)
RUN_JSON="$LATEST_RUN/run.json"

if [[ -d "$LATEST_RUN" && -f "$RUN_JSON" ]]; then
	echo "[run_tests.sh] ✅ Smoke test passed: $RUN_JSON exists."
	echo "[run_tests.sh] Run folder: $LATEST_RUN"
	exit 0
else
	echo "[run_tests.sh] ❌ Smoke test failed: $RUN_JSON missing."
	exit 1
fi