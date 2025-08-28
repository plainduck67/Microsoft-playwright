## Pre-Feature Development Project Tree
- Project tree before Phase 1 (excluding dot-directories):
  - `AGENTS.md`
  - `CHANGELOG.md`
  - `dev_init.sh`
  - `DEVELOPMENT.md`
  - `README.md`
  - `run_tests.sh`

## Relevant Files
- `.project-management/archive-prd/prd-background/full-project-plan.md` - Project overview and 15-phase roadmap context.
- `AGENTS.md` - Contributor guide and process constraints for PRD/tasks.
- `run_tests.sh` - Entry point for smoke tests; to be updated for Phase 1.
- `dev_init.sh` - Idempotent setup/start script; may need updates when scaffolding.
- `DEVELOPMENT.md` - Document any manual steps for local runs if needed.

### Proposed New Files
- `ClickAdventure.sln` - Solution file tying together Core and Runner projects.
- `ClickAdventure.Core/` - Class library for shared models and services.
- `ClickAdventure.Core/ClickAdventure.Core.csproj` - Core project file.
- `ClickAdventure.Core/RunInfo.cs` - Run metadata: ID, timestamp, output paths.
- `ClickAdventure.Runner/` - Console application entry point.
- `ClickAdventure.Runner/ClickAdventure.Runner.csproj` - Runner project file.
- `ClickAdventure.Runner/Program.cs` - CLI args parsing, config summary, lifecycle.
- `ClickAdventure.Runner/appsettings.json` - Default config (startUrl, depth, seed, headless).
- `.env.example` - Document supported environment variables.
- `runs/` - Output root for timestamped run folders.

### Existing Files Modified
- `run_tests.sh` - Add Phase 1 smoke test to build and assert run artifact creation.
- `dev_init.sh` - Ensure idempotent environment setup aligns with new .NET projects.
- `DEVELOPMENT.md` - Add local run instructions and any prerequisites.
- `README.md` - Update as project progresses

### Notes

- Unit tests: Add a simple smoke test in `run_tests.sh` to validate build and run folder creation; add unit tests later when logic accumulates in Core.
- Keep architecture modular: Core library for models/services; Runner for CLI and composition.
- Config precedence: CLI > environment > appsettings (documented in `--help`).

## Tasksdotnet --info

- [x] 1.0 Project Initialization
  - [x] 1.1 Verify .NET SDK is installed (`dotnet --info`) and version aligns with LTS.
  - [x] 1.2 Create solution `ClickAdventure.sln` and projects: `ClickAdventure.Core` (class lib) and `ClickAdventure.Runner` (console app) targeting `net8.0`.
  - [x] 1.3 Add project reference from Runner to Core and include both in the solution.
  - [x] 1.4 Establish initial namespaces (`ClickAdventure.Core`, `ClickAdventure.Runner`).
  - [x] 1.5 Build solution to confirm scaffolding compiles on a clean checkout.

- [x] 2.0 Implement configuration and `--help`
  - [x] 2.1 Add `appsettings.json` to Runner with defaults: `startUrl`, `depth`, `seed`, `headless`.
  - [x] 2.2 Implement `Config` model in Core and bind via `Microsoft.Extensions.Configuration`.
  - [x] 2.3 Wire configuration providers: JSON (optional), environment variables (prefix `CLICKADVENTURE__`), and command-line args (highest precedence).
  - [x] 2.4 Implement minimal CLI parsing to support `--help` and options override; print effective config when running.
  - [x] 2.5 Document precedence and supported flags in `--help` output.

- [c] 3.0 Implement run folder and `run.json` output
  - [x] 3.1 Implement `RunInfo` (run ID, timestamp, output root, paths) in Core.
  - [x] 3.2 Add `--init` command/flag that creates `runs/<YYYYMMDD_HHMMSS>` at startup.
  - [x] 3.3 Serialize `run.json` containing `RunInfo` and effective configuration into the run folder.
  - [x] 3.4 Ensure graceful exit codes: 0 on success; non-zero with message on failure.

- [x] 4.0 Wire console logging to stdout and file
  - [x] 4.1 Configure console logging with `Microsoft.Extensions.Logging` at Info level by default.
  - [x] 4.2 Add simple file logger that appends to `<run>/run.log` when a run folder exists.
  - [x] 4.3 Log startup config summary, run folder path, and shutdown summary.

- [x] 5.0 Add Phase 1 smoke test in `run_tests.sh`
  - [x] 5.1 Add build step: `dotnet build` and fail fast on errors.
  - [x] 5.2 Run: `dotnet run --project ClickAdventure.Runner -- --init`.
  - [x] 5.3 Assert latest `runs/*` folder exists and contains `run.json`; print path for visibility.
  - [x] 5.4 Ensure script exits non-zero if assertions fail.

- [x] 6.0 Update docs (`README.md`, `DEVELOPMENT.md`) and `dev_init.sh`
  - [x] 6.1 Add Phase 1 usage instructions to README (build, run, expected output).
  - [x] 6.2 Add local development steps to DEVELOPMENT.md (SDK requirement, commands).
  - [x] 6.3 Update `dev_init.sh` to idempotently set up environment and start the console app for verification.
  - [x] 6.4 Note testing instructions and how to interpret smoke test output.

*End of document*
