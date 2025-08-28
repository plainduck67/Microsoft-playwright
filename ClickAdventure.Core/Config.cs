using System;

namespace ClickAdventure.Core
{
    public class Config
    {
        public string StartUrl { get; set; } = "https://example.com";
        public int Depth { get; set; } = 1;
        public int Seed { get; set; } = 42;
        public bool Headless { get; set; } = true;
    }
}
