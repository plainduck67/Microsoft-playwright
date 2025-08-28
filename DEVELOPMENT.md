## Development Guide

### Prerequisites
- .NET 8.0 SDK (LTS)

### Local Setup
1. Clone the repo
2. Run `dev_init.sh` (see below)
3. Build: `dotnet build`
4. Run: `dotnet run --project ClickAdventure.Runner -- --init`
5. Test: `bash run_tests.sh`

### dev_init.sh
This script sets up the environment and verifies the console app. It is idempotent.

### Manual Testing
- To verify a run, check `ClickAdventure.Runner/runs/<timestamp>/run.json` and `run.log`.
