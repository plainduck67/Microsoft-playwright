# PRD: Phase 1 — Hello, Adventure (Console Skeleton)

## Summary
Create a minimal, cross-platform .NET console skeleton for Click Adventure with a clean solution layout and configuration surface. The output is a runnable console app that does basic argument/config parsing and logs startup/shutdown cleanly, establishing the foundation for later Playwright integration.

## Goals
- Clean .NET solution structure with `Core` and `Runner` projects.
- Centralized configuration: start URL, crawl depth, random seed, headless/headed flag (stubbed for now).
- Deterministic run metadata folder structure with timestamped run IDs.
- Basic logging to console and to per-run folder.
- Idempotent startup/exit path; graceful error reporting and non-zero exit codes on failure.

## Non-Goals
- No Playwright browser automation yet.
- No link scraping, tree building, or parallelism.
- No UI beyond console; no web host.

## Users and Use Cases
- Learner/Developer: Runs the console to verify environment setup and observe clean app lifecycle.
- Maintainer: Uses the structure to add Playwright and crawling features in later phases.

## Scope
- Create solution and projects: `ClickAdventure.Core` (library), `ClickAdventure.Runner` (console app).
- Implement configuration handling via `appsettings.json`, environment variables, and CLI args (args override env override appsettings).
- Implement `RunInfo` (run ID, timestamp, output paths) and create the output folder on start.
- Implement structured console logging with minimal dependency footprint.
- Provide `--help` output enumerating config options (with defaults) and an example invocation.

## Deliverables
- Compilable solution with two projects and references wired.
- `appsettings.json` and sample `.env.example` documenting key variables.
- Console executable that starts, prints config summary, creates a run folder, and exits 0.
- Basic docs: README section update for Phase 1 usage; DEVELOPMENT notes if manual steps are required.

## Success Metrics
- `dotnet build` succeeds on macOS and Ubuntu.
- `dotnet run --project ClickAdventure.Runner` produces a run folder and logs.
- Passing smoke test script entry in `run_tests.sh` (Phase 1 sanity).

## Constraints & Assumptions
- Target .NET LTS (e.g., .NET 8).
- No external network dependency in Phase 1.
- Keep dependencies minimal; prefer built-in logging/config where possible.

## Risks
- Overengineering early structure; mitigate by focusing on the smallest useful skeleton.
- Cross-platform path handling; use `Path.Combine` consistently and verify relative paths.

## Acceptance Criteria
- Running the console with no args prints help and exits 0.
- Running with `--init` creates a timestamped run directory under `runs/<YYYYMMDD_HHMMSS>` and writes a simple `run.json` with config and run metadata.
- `run_tests.sh` includes a Phase 1 smoke test that runs the app and asserts the run folder and `run.json` exist.
- All artifacts are written under the workspace (no absolute OS paths required).

## Out of Scope (Explicit)
- Any web UI or Playwright actions.
- Tree crawling, screenshots, tracing.

## Open Questions
- Preferred config precedence depth (CLI > env > appsettings) — proposed yes.
- Naming convention for solution and namespaces — proposed `ClickAdventure.*`.

## Milestones
1. Scaffold solution/projects and references.
2. Add config and logging.
3. Implement run folder creation and `run.json`.
4. Wire `run_tests.sh` smoke test.
5. Update docs (README Phase 1 usage; DEVELOPMENT if needed).

