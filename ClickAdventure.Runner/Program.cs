
using System;
using System.IO;
using ClickAdventure.Core;

using Microsoft.Extensions.Configuration;


namespace ClickAdventure.Runner
{
	internal class Program
	{
		static void Main(string[] args)
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
				return;
			}

			// Check for --init flag and create run folder if present
			if (args.Any(a => a == "--init"))
			{
				string runsRoot = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "runs");
				runsRoot = Path.GetFullPath(runsRoot);
				if (!Directory.Exists(runsRoot))
				{
					Directory.CreateDirectory(runsRoot);
				}
				string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
				string runFolder = Path.Combine(runsRoot, timestamp);
				Directory.CreateDirectory(runFolder);
				Console.WriteLine($"[INIT] Created run folder: {runFolder}");
				// You can add more logic here to use this folder for output, etc.
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

			Console.WriteLine($"Config Loaded:\n  StartUrl: {appConfig.StartUrl}\n  Depth: {appConfig.Depth}\n  Seed: {appConfig.Seed}\n  Headless: {appConfig.Headless}");
		}
	}
}
