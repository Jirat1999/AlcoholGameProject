# AlcoholGameProject

.NET MAUI Blazor Hybrid app that runs as a native app and in the browser.

| Project | Purpose |
|---|---|
| `src/AlcoholGame.Shared` | Razor class library: all pages, components and game logic |
| `src/AlcoholGame.Maui` | MAUI app (Android, iOS, macOS, Windows) hosting Shared in a BlazorWebView |
| `src/AlcoholGame.Web` | Blazor WebAssembly app hosting Shared in the browser |

## Run
- Web: `dotnet run --project src/AlcoholGame.Web`
- App: `dotnet workload install maui`, then build/run `src/AlcoholGame.Maui` from Visual Studio or `dotnet build -t:Run -f net8.0-android`
