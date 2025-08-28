using System;
using System.IO;

namespace ClickAdventure.Core
{
    /// <summary>
    /// Represents metadata and paths for a single run.
    /// </summary>
    public class RunInfo
    {
        /// <summary>
        /// Unique identifier for the run (e.g., GUID).
        /// </summary>
    public string RunId { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when the run was started (UTC).
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Root output directory for this run.
        /// </summary>
    public string OutputRoot { get; set; } = string.Empty;

        /// <summary>
        /// Path to the run folder (e.g., runs/YYYYMMDD_HHMMSS).
        /// </summary>
    public string RunFolderPath { get; set; } = string.Empty;

        /// <summary>
        /// Path to the run.json metadata file.
        /// </summary>
    public string RunJsonPath { get; set; } = string.Empty;

        /// <summary>
        /// Path to the run log file.
        /// </summary>
    public string RunLogPath { get; set; } = string.Empty;

        public RunInfo()
        {
        }

        public static RunInfo Create(string outputRoot)
        {
            var timestamp = DateTime.UtcNow;
            var runId = Guid.NewGuid().ToString();
            var folderName = timestamp.ToString("yyyyMMdd_HHmmss");
            var runFolder = Path.Combine(outputRoot, folderName);
            return new RunInfo
            {
                RunId = runId,
                Timestamp = timestamp,
                OutputRoot = outputRoot,
                RunFolderPath = runFolder,
                RunJsonPath = Path.Combine(runFolder, "run.json"),
                RunLogPath = Path.Combine(runFolder, "run.log")
            };
        }
    }
}
