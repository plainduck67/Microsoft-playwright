using System;

namespace ClickAdventure.Core
{
    public class Config
    {
    // The URL to start navigation (default: Wikipedia)
    public string StartUrl { get; set; } = "https://en.wikipedia.org/wiki/Main_Page";
    // How deep to traverse (not used for browser, but kept for compatibility)
    public int Depth { get; set; } = 1;
    public int Seed { get; set; } = 42;
    // Whether to run the browser in headless mode
    public bool IsHeadless { get; set; } = true;
    // Timeout for navigation in milliseconds (default: 30 seconds)
    public int NavigationTimeout { get; set; } = 30000;
    }
}
