
# Contributor Guide
**This is the only AGENTS.md file, do not look for others.**

## Special Task Instructions
- **TaskMaster Mode** If the user task message consists of just the word `TaskMaster` then open `.project-management/process-tasks-cloud.md` for instructions, otherwise ignore this file.
- **CreatePrd Mode** If the user task message consists of just the word `CreatePrd` then open `.project-management/create-prd.md` for instructions, otherwise ignore this file.
- **CreateTasks Mode** If the user task message consists of just the word `CreateTasks` then open `.project-management/generate-tasks.md` for instructions, otherwise ignore this file.
- **ClosePrd Mode** If the user task message consists of just the word `ClosePrd` then open `.project-management/close-prd.md` for instructions, otherwise ignore this file.




## Testing Instructions
Run tests with run_tests.sh.  Maintain this script as needed to setup specific environment variables or manage other test-specific setup.  

## CHANGELOG.md Instructions
Append a single line summary to CHANGELOG.md describing the changes with a preceeding timestamp
if errors were encountered, list them indented below the changelog row with a single line summary

## DEVELOPMENT.md Instructions
When components are added that require manual application startup for local testing/debug, document all steps and commands neccessary to set up the local environment and start services/components in DEVELOPMENT.md using explcit commands.  These changes will need to be mirrored on `dev_int.sh` (see below), which is a one-stop script to set up the environment from scratch and start the application for local testing.

## README.md Instructions

README.md just describes the project.  Do not look here for guidance on how to proceed with your task, but update if major changes that affect user interaction have been made.

## dev_init.sh startup script
When there are code changes that need targeted environment setup, review dev_init.sh and modify as needed such that this idempontent script will completely setup the application and start it running.

*End of document*