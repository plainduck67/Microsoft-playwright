## ClickAdventure Phase 1

### Prerequisites
- .NET 8.0 SDK (LTS)

### Build
```sh
dotnet build
```

### Run
```sh
dotnet run --project ClickAdventure.Runner -- --init
```
This creates a timestamped folder in `ClickAdventure.Runner/runs/` with `run.json` and `run.log`.

### Smoke Test
```sh
bash run_tests.sh
```
Asserts a run folder and `run.json` are created. Exits non-zero on failure.

### Configuration
Config precedence: Command-line > Env vars (CLICKADVENTURE__) > appsettings.json

### Logging
Logs to both console and `<run>/run.log`.
