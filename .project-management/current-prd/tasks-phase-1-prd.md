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

## Tasks

 - [ ] 1.0 Project Initialization
   - [ ] 1.1 Verify .NET SDK is installed (`dotnet --info`) and version aligns with LTS.
  - [ ] 1.2 Create solution `ClickAdventure.sln` and projects: `ClickAdventure.Core` (class lib) and `ClickAdventure.Runner` (console app) targeting `net8.0`.
  - [ ] 1.3 Add project reference from Runner to Core and include both in the solution.
  - [ ] 1.4 Establish initial namespaces (`ClickAdventure.Core`, `ClickAdventure.Runner`).
  - [ ] 1.5 Build solution to confirm scaffolding compiles on a clean checkout.

- [ ] 2.0 Implement configuration and `--help`
  - [ ] 2.1 Add `appsettings.json` to Runner with defaults: `startUrl`, `depth`, `seed`, `headless`.
  - [ ] 2.2 Implement `Config` model in Core and bind via `Microsoft.Extensions.Configuration`.
  - [ ] 2.3 Wire configuration providers: JSON (optional), environment variables (prefix `CLICKADVENTURE__`), and command-line args (highest precedence).
  - [ ] 2.4 Implement minimal CLI parsing to support `--help` and options override; print effective config when running.
  - [ ] 2.5 Document precedence and supported flags in `--help` output.

- [ ] 3.0 Implement run folder and `run.json` output
  - [ ] 3.1 Implement `RunInfo` (run ID, timestamp, output root, paths) in Core.
  - [ ] 3.2 Add `--init` command/flag that creates `runs/<YYYYMMDD_HHMMSS>` at startup.
  - [ ] 3.3 Serialize `run.json` containing `RunInfo` and effective configuration into the run folder.
  - [ ] 3.4 Ensure graceful exit codes: 0 on success; non-zero with message on failure.

- [ ] 4.0 Wire console logging to stdout and file
  - [ ] 4.1 Configure console logging with `Microsoft.Extensions.Logging` at Info level by default.
  - [ ] 4.2 Add simple file logger that appends to `<run>/run.log` when a run folder exists.
  - [ ] 4.3 Log startup config summary, run folder path, and shutdown summary.

- [ ] 5.0 Add Phase 1 smoke test in `run_tests.sh`
  - [ ] 5.1 Add build step: `dotnet build` and fail fast on errors.
  - [ ] 5.2 Run: `dotnet run --project ClickAdventure.Runner -- --init`.
  - [ ] 5.3 Assert latest `runs/*` folder exists and contains `run.json`; print path for visibility.
  - [ ] 5.4 Ensure script exits non-zero if assertions fail.

- [ ] 6.0 Update docs (`README.md`, `DEVELOPMENT.md`) and `dev_init.sh`
  - [ ] 6.1 Add Phase 1 usage instructions to README (build, run, expected output).
  - [ ] 6.2 Add local development steps to DEVELOPMENT.md (SDK requirement, commands).
  - [ ] 6.3 Update `dev_init.sh` to idempotently set up environment and start the console app for verification.
  - [ ] 6.4 Note testing instructions and how to interpret smoke test output.

*End of document*
