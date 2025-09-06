

using System;
using System.IO;

using System.Text.Json;
using ClickAdventure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace ClickAdventure.Runner
{
	internal class Program
	{
	static int Main(string[] args)
		{
			ILoggerFactory loggerFactory = null;
			ILogger logger = null;
			string runLogPath = null;
			try
			{
				// Minimal CLI parsing for --help
				if (args.Any(a => a == "--help" || a == "-h"))
				{
					Console.WriteLine(
						"ClickAdventure Runner\n\n" +
						"Usage: dotnet run --project ClickAdventure.Runner -- [options]\n\n" +
						"Options:\n" +
						"  --startUrl <url>         Starting URL for the adventure (default: from config)\n" +
						"  --depth <n>              Maximum crawl depth (default: from config)\n" +
						"  --seed <n>               Random seed (default: from config)\n" +
						"  --headless <true|false>  Run in headless mode (default: from config)\n" +
						"  --init                   Create a new run folder at startup\n" +
						"  --help, -h               Show this help message\n\n" +
						"Configuration precedence (highest to lowest):\n" +
						"  1. Command-line arguments (flags above)\n" +
						"  2. Environment variables (prefix: CLICKADVENTURE__)\n" +
						"  3. appsettings.json file\n\n" +
						"Examples:\n" +
						"  dotnet run --project ClickAdventure.Runner -- --startUrl https://example.com --depth 3\n" +
						"  dotnet run --project ClickAdventure.Runner -- --init\n" +
						"  CLICKADVENTURE__DEPTH=5 dotnet run --project ClickAdventure.Runner\n");
					return 0;
				}

				// Set up configuration with correct precedence: JSON < env vars < command-line
				var config = new ConfigurationBuilder()
					.SetBasePath(AppContext.BaseDirectory)
					.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					.AddEnvironmentVariables(prefix: "CLICKADVENTURE__")
					.AddCommandLine(args)
					.Build();

				var appConfig = new Config();
				config.Bind(appConfig);

				string runFolder = null;
				RunInfo runInfo = null;
				if (args.Any(a => a == "--init"))
				{
					string runsRoot = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "runs");
					runsRoot = Path.GetFullPath(runsRoot);
					if (!Directory.Exists(runsRoot))
					{
						Directory.CreateDirectory(runsRoot);
					}
					string timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
					runFolder = Path.Combine(runsRoot, timestamp);
					Directory.CreateDirectory(runFolder);
					runInfo = RunInfo.Create(runsRoot);
					runLogPath = Path.Combine(runFolder, "run.log");
					// Set up logger factory for both console and file
					loggerFactory = LoggerFactory.Create(builder =>
					{
						builder.SetMinimumLevel(LogLevel.Information);
						builder.AddConsole();
						builder.AddProvider(new SimpleFileLoggerProvider(runLogPath));
					});
					logger = loggerFactory.CreateLogger("Runner");
					logger.LogInformation($"[INIT] Created run folder: {runFolder}");

					// Create RunInfo and serialize to run.json
					var runJsonPath = Path.Combine(runFolder, "run.json");
					var runData = new
					{
						RunInfo = runInfo,
						Config = appConfig
					};
					var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
					File.WriteAllText(runJsonPath, JsonSerializer.Serialize(runData, jsonOptions));
					logger.LogInformation($"[INIT] Wrote run.json: {runJsonPath}");
				}
				else
				{
					// Console logger only if not --init
					loggerFactory = LoggerFactory.Create(builder =>
					{
						builder.SetMinimumLevel(LogLevel.Information);
						builder.AddConsole();
					});
					logger = loggerFactory.CreateLogger("Runner");
				}

				logger.LogInformation($"Config Loaded: StartUrl={appConfig.StartUrl}, Depth={appConfig.Depth}, Seed={appConfig.Seed}, Headless={appConfig.IsHeadless}");
				logger.LogInformation("Shutdown complete.");
				loggerFactory?.Dispose();
				return 0;
			}
			catch (Exception ex)
			{
				logger?.LogError($"[ERROR] {ex.Message}");
				loggerFactory?.Dispose();
				return 1;
			}
		}
	}
}
