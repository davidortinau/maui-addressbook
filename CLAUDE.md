# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

Address Book Plus: a .NET MAUI 11 remake of the 1990 Mac card-based contact manager. It is a single-project app (`src/AddressBookPlus.csproj`, root namespace `AddressBookPlus`) built with XAML + MVVM and SQLite, and styled with the Bootswatch **Brite** theme. It targets Android, iOS and Mac Catalyst. Windows is added only when building on a Windows host.

## Commands

The repo has no solution file, test project or linter configuration. Build one target framework at a time, because building all of them is slow. To launch the app, use `-t:Build,Run`. On the .NET 11 SDK, `-t:Run` alone does not build first, so it launches a stale app bundle.

```bash
dotnet build src/AddressBookPlus.csproj -f net11.0-maccatalyst
dotnet build src/AddressBookPlus.csproj -f net11.0-maccatalyst -t:Build,Run
dotnet build src/AddressBookPlus.csproj -f net11.0-ios -t:Build,Run -p:_DeviceName=:v2:udid=<SIMULATOR_UDID>
dotnet build src/AddressBookPlus.csproj -f net11.0-android -t:Build,Run
```

You need the .NET 11 SDK (currently RC1, `11.0.100-rc.1`) and the matching `maui` workload. `Microsoft.Maui.Controls` resolves to `$(MauiVersion)` from the installed workload. Every other package is pinned to an exact version.

A clean build has about 77 `IL2037` trimmer warnings on Mac Catalyst. They come from MAUI 11 RC1 itself (a blank `dotnet new maui` app gets the same ones), not from this code.

**Inspecting the running app:** DEBUG builds call `AddMauiDevFlowAgent()` from `Microsoft.Maui.DevFlow.Agent` (namespace `Microsoft.Maui.DevFlow.Agent`).
- **Pin the agent to an exact nuget.org version.** Local feeds can hold `0.2x.x-dev` builds. Those sort higher than the official `0.1.0-preview.*` releases, so a floating version would pick up a local build.
- **Port:** the agent registers with the DevFlow broker, which assigns its port. `maui devflow list` shows it. Pass it with `--agent-port`, because other apps are often connected too.
- **Config file:** the package's build targets read `.mauidevflow` only from the project directory (`src/`). The file at the repo root is not used by the agent.
- **Read-only commands work with any `maui` CLI:** `ui status`, `ui tree`, `ui query`, `ui screenshot` and `logs`.
- **Commands that change the app need a lease:** `tap`, `navigate`, `fill` and similar must send an `X-DevFlow-Lease` header. A CLI that predates leases gets "Another DevFlow session is driving this app".
- **Lease workaround:** claim a lease from the agent directly, send it as a header, then release it:
  ```bash
  curl -X POST localhost:<port>/api/v1/agent/lease -H 'Content-Type: application/json' -d '{"action":"claim","leaseId":"me","holderKind":"cli"}'
  curl -X POST localhost:<port>/api/v1/ui/actions/navigate -H 'X-DevFlow-Lease: me' -H 'Content-Type: application/json' -d '{"route":"//contacts/detail?id=4"}'
  curl -X POST localhost:<port>/api/v1/agent/lease -H 'Content-Type: application/json' -d '{"action":"release","leaseId":"me"}'
  ```
  A lease expires after about 10 seconds without a heartbeat.

## Architecture

**Composition.** `MauiProgram.cs` registers every service, ViewModel and Page with DI. Each page takes its ViewModel through its constructor and sets `BindingContext`. When you add a page, register both the page and its ViewModel there. `DataService` and `SearchService` are singletons. Everything else is transient.

**MVVM.** The ViewModels use CommunityToolkit.Mvvm source generators. `[ObservableProperty]` goes on `_camelCase` fields. `[RelayCommand]` on `FooAsync()` generates `FooCommand`. All ViewModels inherit from `BaseViewModel`, which provides `IsBusy` and `Title`. XAML pages set `x:DataType` for compiled bindings. A command used inside a `DataTemplate` binds back to the page's ViewModel with `{Binding Source={x:Reference ThisPage}, Path=BindingContext.XxxCommand}`.

**Cross-page refresh.** Refreshes between pages go through `WeakReferenceMessenger`, using the message types in `ViewModels/Messages.cs` (`ContactSavedMessage`, `ContactDeletedMessage`). `ContactListViewModel` subscribes to both and reloads. `ContactListPage.OnAppearing` also reloads.

**Navigation (Shell).** `AppShell.xaml` defines four flyout items with the routes `contacts`, `groups`, `search` and `print`. The detail and edit pages are registered in `AppShell.xaml.cs` as `contacts/detail` and `contacts/edit`, and they take `?id=` (no id means a new contact). The target ViewModels read the id through `IQueryAttributable.ApplyQueryAttributes`. From any flyout item other than contacts, you **must** use the absolute route `//contacts/detail?id=…`. A relative route crashes. `SearchPage.xaml.cs` does this correctly. The unused `SearchViewModel.GoToDetailCommand` still uses the relative form.

**Data (SQLite, sqlite-net-pcl).**
- `DataService` owns a single `SQLiteAsyncConnection` to `FileSystem.AppDataDirectory/addressbook_v2.db3`.
- Every public method first calls `InitializeAsync()`. That call creates the tables and, if they are empty, seeds 3 groups (Family, Friends, Work), 10 sample contacts and their group memberships.
- Table names come from `[Table]` attributes: `People`, `Groups`, `GroupMemberships`, `DocumentSettings`. Raw SQL in `DataService` (the group join, cascade deletes) uses those names directly.
- Deleting a contact or group removes its `GroupMemberships` rows by hand.
- There are no migrations. `CreateTableAsync` adds new columns but never renames or drops them. To reset seeded data, delete the app, or bump the DB file name as was done for `_v2`.
- `[Ignore]` marks computed properties (`FullName`, `SortName`, `DisplayName`) and UI state (`ContactGroup.IsSelected`) that are not persisted.

**Search.** `SearchService` runs a LINQ `Contains` filter in memory over `GetAllContactsAsync()`. There is no FTS index, even though the plan calls for one. The contact list's type-to-filter box, `SearchPage` and the `MiniLookupOverlay` control all use this service.

**Theming (Plugin.Maui.BootstrapTheme).**
- The csproj line `<BootstrapCss Include="Resources\Themes\brite.min.css" />` turns the CSS into a generated `Themes.BriteTheme` ResourceDictionary at build time.
- `App.xaml.cs` merges that dictionary and calls `BootstrapTheme.Apply("brite")`.
- Style controls with Bootstrap class names through `StyleClass` (for example `btn-outline-primary,btn-sm`, `card,text-bg-dark`, `h4,on-dark`, `badge,bg-primary`).
- Use theme colors through `{DynamicResource …}` (for example `DarkColor`). Don't hard-code colors.
- `BoolToStyleClassConverter` takes a `"trueClasses|falseClasses"` parameter and returns a `List<string>` for `StyleClass`.
- `Resources/Styles/*.xaml` holds the stock MAUI template styles.

**Icons.** Icons come from `IconFont.Maui.BootstrapIcons`, which is referenced with `ExcludeAssets="buildTransitive"`. Its TTF is copied into `Resources/Fonts` and registered as `BootstrapIcons`. Use them as `<FontImageSource Glyph="{x:Static icons:BootstrapIcons.Search}" FontFamily="{x:Static icons:BootstrapIcons.FontFamily}" />`.

## Gotchas

- **Group filter bar:** `ContactGroup` is a plain POCO, so changing `IsSelected` doesn't update the UI. `ContactListViewModel.ToggleGroupFilterAsync` clears and refills `Groups` to force the `BindableLayout` to re-render.
- **Two `GroupSelection` classes:** one is in `Models/GroupSelection.cs` and the other is at the bottom of `ViewModels/ContactEditViewModel.cs`. Inside the ViewModels namespace, the ViewModels one wins.
- **Print templates are app assets, not embedded resources:** they live in `Resources/Raw/print_templates` as `MauiAsset` items with the `Resources/Raw/` prefix stripped. Load them with `FileSystem.OpenAppPackageFileAsync("print_templates/<file>")`, never `GetManifestResourceStream`. `PrintService` replaces the `{{CONTACTS}}` and `{{DATE}}` placeholders. Print only builds the HTML and shows a text summary; there is no native print implementation yet.
- **Import/export isn't wired up:** `ImportExportService` (vCard/CSV) is registered with DI, but no UI uses it.
- `docs/implementation_plan.md` is the original design doc. It describes features that were never built (FTS5, a merge service, a settings page, platform print services, an import/export page). Treat the code as the source of truth.

## API conventions (MAUI 11)

From the plan's API currency list:
- Use `CollectionView`, not `ListView`.
- Use `Border`, not `Frame`.
- Use `WeakReferenceMessenger`, not `MessagingCenter`.
- Use `DisplayAlertAsync`, not `DisplayAlert`.
- Use `MainThread.*`, not `Device.*`.
- Use constructor injection, not `DependencyService`.
- Use `SemanticProperties`, not `AutomationProperties.Name`.
- Use the `*Async` animation methods.

For alerts from ViewModels, the current code uses `Application.Current?.Windows[0]?.Page`.

MAUI 11 generates XAML code with a source generator. That generator reports CS8622 unless an event handler wired up in XAML declares `object? sender`. For code-behind handlers that use `sender`, pattern-match it (`sender is not CollectionView cv`) instead of casting it.
