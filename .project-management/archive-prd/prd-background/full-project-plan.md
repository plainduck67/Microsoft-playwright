# Click Adventure: Master Roadmap

## Project Context

This project is a learning exercise. The aim is to build a playful yet technically instructive application called **Click Adventure**, which uses [Playwright for .NET](https://playwright.dev/dotnet) to control a browser and traverse Wikipedia articles in a branching "adventure tree" style.

The project is designed to be **OS-agnostic** so it can run both on macOS and Ubuntu. By grounding everything in cross-platform .NET and Playwright's bundled browsers, the setup avoids OS-specific pitfalls and allows both to develop and run the game without friction.

At its core, the Click Adventure game starts with a Wikipedia page, expands out by selecting two random links per page, and recursively continues this process for five levels, producing 32 end pages (leaves). These leaves form the basis of a **challenge game**: the opponent selects one leaf page as the "target," and the player must try to reach that page within a fixed number of clicks.

Beyond being a fun Wikipedia exploration challenge, the project scaffolds toward deeper goals:
- Teaching core programming concepts in C#, parallelism, and async/await.
- Introducing Playwright fundamentals (navigation, locators, input, screenshots, parallelism).
- Building reusable architecture (crawler core, reporting, UI layer) that can later integrate with **OpenAI's Computer Use API (CUA)**, where an LLM makes decisions about which links to click instead of a human or random chooser.

On the UI side, the plan is to start simple (console and Markdown output), then move gradually to web-based interfaces. The web front end will emphasize **standard HTML authoring**, starting with static files and progressing to Razor Pages and light MVC. 

Key themes of the roadmap include:
- **Incremental complexity:** Each milestone adds one new concept only, ensuring steady progress without overload.
- **Runnable artifacts at each step:** Every phase results in a working program, whether console-based or web-based, so progress always feels tangible.
- **Parallelism as a feature:** The 32-leaf crawl isn't just a challenge—it becomes a natural way to teach parallel execution in C#.
- **Future extensibility:** The architecture leaves space to plug in LLM-driven decision-making via the CUA tool, making the project both a teaching scaffold and a potential demo piece.

In short, Click Adventure is not just a Wikipedia game. It’s a bridge: from basics of C# and Playwright, to async orchestration, to web UI development, and finally to human-AI collaboration demos. It's fun, instructive, and expandable.

---

## 15-Phase Development Plan

1) **Hello, Adventure (Console Skeleton)**
   - Goal: Set up a .NET console app with a clean project structure (Core, Runner).
   - New concept: Solution layout & config (env vars/appsettings, seeds, start URL, depth).

2) **Launch & Visit One Page**
   - Goal: Launch Playwright, open a browser/page, navigate to a Wikipedia URL, exit cleanly.
   - New concept: Browser/Context/Page lifecycle, headed vs headless.

3) **Screenshot Tourist**
   - Goal: Take a deterministic viewport screenshot of the current page and save it.
   - New concept: Viewport/DPI discipline, screenshots (page vs element), file outputs.

4) **Find & Filter Links**
   - Goal: Read visible internal Wikipedia links, filter out non-article namespaces.
   - New concept: Locators & simple scraping (text/href), content filters.

5) **Pick Two, Click Two (Sequential)**
   - Goal: From a start page, choose 2 random links and visit them (one after the other).
   - New concept: Deterministic randomness (seeded), basic navigation flow control.

6) **Build a Small Tree (Depth = 3)**
   - Goal: Expand 2 links per page for 3 levels (8 leaves), store the path for each leaf.
   - New concept: Tree data model (nodes/edges), path tracking & serialization (JSON).

7) **Parallel Fan-Out**
   - Goal: Redo Phase 6 concurrently to produce the tree faster (same result set).
   - New concept: C# `async/await`, `Task.WhenAll`, safe concurrency with Playwright contexts.

8) **Basic Verification & Waits**
   - Goal: Assert page title contains expected text; avoid flakiness between clicks.
   - New concept: Auto-waiting & targeted waits (URL change, element visible), basic “assert” mindset.

9) **Multiple Tabs & Popups**
   - Goal: Handle a link that opens a new tab; switch, record, and return.
   - New concept: `popup` event handling, managing more than one Page cleanly.

10) **Downloads & Artifacts**
   - Goal: If a “download” link exists, capture the file to a run folder (best-effort on Wiki).
   - New concept: Download handling, artifact organization (per-run directories).

11) **Trace & Replay**
   - Goal: Enable Playwright tracing/video for the crawl; store a trace per run.
   - New concept: Tracing for debugging and reproducibility.

12) **Challenge Card (Markdown Export)**
   - Goal: Emit `challenge.md` listing all leaf pages; include a spoiler path per leaf.
   - New concept: Simple reporting layer (Markdown) that a human can play from.

13) **Minimal Web Host (Static Only)**
   - Goal: Add a separate ASP.NET Core project that serves static files: index.html + links to latest `challenge.md` and screenshots.
   - New concept: Static file hosting, simple web packaging of artifacts (no server logic yet).

14) **Razor for Live Run Listing**
   - Goal: Replace the static index with a Razor page that enumerates runs and links to each run’s artifacts.
   - New concept: Razor Pages basics (no JS framework), model-binding for server-rendered HTML.

15) **Start/Stream a Crawl from the Web**
   - Goal: Add a minimal controller/endpoint to kick off a new crawl from the web UI and show progress (simple polling or Server-Sent Events).
   - New concept: Light MVC endpoint + progress updates (no Blazor), clear seam for future CUA “action executor”.
