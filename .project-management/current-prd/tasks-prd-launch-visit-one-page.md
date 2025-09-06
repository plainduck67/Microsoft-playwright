## Pre-Feature Development Project Tree
```
/Users/kevinsullivan/code/Microsoft-playwright
├── AGENTS.md
├── CHANGELOG.md
├── ClickAdventure.Core
│   ├── Class1.cs
│   ├── ClickAdventure.Core.csproj
│   ├── Config.cs
│   ├── RunInfo.cs
│   └── SimpleFileLogger.cs
├── ClickAdventure.Runner
│   ├── appsettings.json
│   ├── ClickAdventure.Runner.csproj
│   ├── Program.cs
│   └── runs
│       ├── 20250827_213438
│       ├── 20250828_034059
│       ├── 20250828_034252
│       └── 20250828_034316
├── ClickAdventure.sln
├── dev_init.sh
├── DEVELOPMENT.md
├── README.md
└── run_tests.sh
```

## Relevant Files

### Proposed New Files
- `ClickAdventure.Core/BrowserManager.cs` - Core class responsible for Playwright browser lifecycle management (launch, navigate, cleanup)
- `ClickAdventure.Core/WebPageNavigator.cs` - Handles page navigation logic and verification
- `ClickAdventure.Core/BrowserManager.Tests.cs` - Unit tests for browser management functionality
- `ClickAdventure.Core/WebPageNavigator.Tests.cs` - Unit tests for page navigation functionality

### Existing Files Modified
- `ClickAdventure.Core/Config.cs` - Add browser configuration options (headed/headless mode, Wikipedia URL, timeouts)
- `ClickAdventure.Core/ClickAdventure.Core.csproj` - Add Playwright NuGet package dependency
- `ClickAdventure.Runner/Program.cs` - Integrate browser navigation workflow
- `ClickAdventure.Runner/appsettings.json` - Add Wikipedia URL and browser configuration settings
- `run_tests.sh` - Update to run new unit tests
- `dev_init.sh` - Update to install Playwright browsers and dependencies

### Notes

- Unit tests: Every task that involves new functionality MUST be developed alongside new unit tests that test the feature. Run tests with `run_tests.sh` and maintain this script as needed to setup specific environment variables or manage other test-specific setup.
- `dev_init.sh` startup script: This script is a one-stop script to install dependencies, set up environment and start the application running. When there are code changes that need targeted environment setup, review dev_init.sh and modify as needed such that this idempotent script will completely setup the application and start it running.
- Every parent task should conclude with a sub-task that directs the coding agent to ensure `run_tests.sh` works, all tests pass, and that `dev_init.sh` is fully functional.
- Architecture must be kept modular, with single responsibility principles at the core. More small classes beats monolithic code.

## Tasks

- [x] 1.0 Setup Playwright Dependencies and Configuration
  - [x] 1.1 Add Microsoft.Playwright NuGet package to ClickAdventure.Core.csproj
  - [x] 1.2 Update Config.cs to include browser configuration properties (IsHeadless, StartUrl, NavigationTimeout)
  - [x] 1.3 Update appsettings.json with default Wikipedia URL and browser settings
  - [x] 1.4 Update dev_init.sh to install Playwright browsers using `playwright install chromium`
  - [x] 1.5 Verify dev_init.sh works and run_tests.sh executes successfully

- [ ] 2.0 Implement Browser Lifecycle Management
  - [ ] 2.1 Create BrowserManager.cs class in ClickAdventure.Core with methods for browser lifecycle
  - [ ] 2.2 Implement LaunchBrowserAsync() method that creates Playwright instance and launches Chromium browser
  - [ ] 2.3 Implement CreateContextAsync() method that creates a new browser context with appropriate settings
  - [ ] 2.4 Implement CreatePageAsync() method that creates a new page within the context
  - [ ] 2.5 Implement DisposeAsync() method that properly cleans up page, context, and browser resources
  - [ ] 2.6 Create comprehensive unit tests in BrowserManager.Tests.cs covering all lifecycle methods
  - [ ] 2.7 Verify all tests pass and browser resources are properly disposed

- [ ] 3.0 Implement Page Navigation and Verification
  - [ ] 3.1 Create WebPageNavigator.cs class in ClickAdventure.Core for navigation logic
  - [ ] 3.2 Implement NavigateToUrlAsync() method with configurable timeout and error handling
  - [ ] 3.3 Implement VerifyWikipediaPageAsync() method that checks page title contains "Wikipedia"
  - [ ] 3.4 Add logging for navigation events (start, success, failure) using SimpleFileLogger
  - [ ] 3.5 Create comprehensive unit tests in WebPageNavigator.Tests.cs covering navigation scenarios
  - [ ] 3.6 Test navigation with valid and invalid URLs, including timeout scenarios
  - [ ] 3.7 Verify all tests pass and navigation works reliably

- [ ] 4.0 Integrate Browser Functionality with Console Runner
  - [ ] 4.1 Update Program.cs to instantiate BrowserManager and WebPageNavigator
  - [ ] 4.2 Implement main workflow: launch browser → navigate to Wikipedia → verify page → cleanup
  - [ ] 4.3 Add console output for each major step (launching browser, navigating, verifying, cleanup)
  - [ ] 4.4 Implement proper exit codes (0 for success, 1 for configuration errors, 2 for browser errors)
  - [ ] 4.5 Test both headed and headless modes by modifying appsettings.json
  - [ ] 4.6 Verify the complete workflow works end-to-end in both browser modes

- [ ] 5.0 Add Comprehensive Error Handling and Logging
  - [ ] 5.1 Add try-catch blocks around all browser operations with specific exception handling
  - [ ] 5.2 Implement timeout handling for browser launch and page navigation operations
  - [ ] 5.3 Add detailed error logging for browser launch failures, navigation timeouts, and cleanup issues
  - [ ] 5.4 Ensure user-friendly console error messages while technical details go to log files
  - [ ] 5.5 Add configuration validation (URL format, timeout values) with early failure
  - [ ] 5.6 Test error scenarios: invalid URLs, network timeouts, browser launch failures
  - [ ] 5.7 Verify run_tests.sh works, all tests pass, and dev_init.sh is fully functional

*End of document*
