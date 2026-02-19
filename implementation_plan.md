# Address Book Plus — .NET MAUI Implementation Plan

> A faithful recreation of the classic Apple Macintosh **Address Book Plus** (circa 1990),
> built with .NET MAUI 10, XAML + MVVM, and styled with the **Brite** Bootswatch theme
> via `Plugin.Maui.BootstrapTheme`.

---

## Table of Contents

1. [Key Technical Decisions & Tradeoffs](#1-key-technical-decisions--tradeoffs)
2. [Project Structure](#2-project-structure)
3. [Data Model Classes](#3-data-model-classes)
4. [Page / View Inventory](#4-page--view-inventory)
5. [ViewModel Inventory](#5-viewmodel-inventory)
6. [Services Layer](#6-services-layer)
7. [Navigation Architecture](#7-navigation-architecture)
8. [Bootstrap Theme Integration — Retro Mac × Brite](#8-bootstrap-theme-integration--retro-mac--brite)
9. [Implementation Phases](#9-implementation-phases)

---

## 1. Key Technical Decisions & Tradeoffs

| Decision | Choice | Rationale |
|----------|--------|-----------|
| **Target frameworks** | `net10.0-ios;net10.0-maccatalyst;net10.0-android` | Apple + Android coverage; no Windows per requirements |
| **UI framework** | XAML + MVVM (no MauiReactor, no Blazor Hybrid) | Classic MAUI, widest community knowledge, best XAML Hot Reload story |
| **MVVM library** | `CommunityToolkit.Mvvm` (v11+) | Source-generated `[ObservableProperty]`, `[RelayCommand]`, `WeakReferenceMessenger` (replaces deprecated `MessagingCenter`) |
| **Data persistence** | SQLite via `sqlite-net-pcl` | Single-file DB, embedded, cross-platform; mirrors the original's single-document model |
| **Navigation** | `Shell` with `GoToAsync` | Flyout for group lists, tab bar for main areas, URI-based deep linking |
| **Printing** | Platform-specific print services behind `IPrintService` | MAUI has no built-in print API; use native `UIPrintInteractionController` (iOS/Mac Catalyst) and Android `PrintManager` |
| **Import / Export** | vCard 3.0 (.vcf) + CSV | vCard is the universal contact interchange format; CSV for spreadsheet users |
| **Search** | SQLite FTS5 (Full-Text Search) | Fast type-to-jump and filtered queries without an external engine |
| **Deprecated API avoidance** | Per `maui-current-apis` skill | No `ListView` (use `CollectionView`), no `Frame` (use `Border`), no `Device.*`, `*Async` animation names, `DisplayAlertAsync`, etc. |
| **Styling** | Every control gets a `StyleClass`; all colors via `DynamicResource` | Required by MauiBootstrapTheme; enables runtime theme switching |
| **CollectionView for lists** | `CollectionView` with `GroupHeaderTemplate` | `ListView` is deprecated in .NET 10; `CollectionView` supports grouping, selection, and incremental loading |

---

## 2. Project Structure

```
AddressBookPlus/
├── AddressBookPlus.sln
├── src/
│   └── AddressBookPlus/
│       ├── AddressBookPlus.csproj
│       ├── MauiProgram.cs
│       ├── App.xaml / App.xaml.cs
│       ├── AppShell.xaml / AppShell.xaml.cs
│       │
│       ├── Models/
│       │   ├── PersonRecord.cs            # Core contact entity
│       │   ├── AddressBookDocument.cs      # Document / database wrapper
│       │   ├── ContactGroup.cs             # Group/list entity
│       │   └── UserDefinedField.cs         # Custom field definition
│       │
│       ├── ViewModels/
│       │   ├── BaseViewModel.cs            # ObservableObject base with IsBusy, Title
│       │   ├── ContactListViewModel.cs     # Browse list, type-to-jump, group filtering
│       │   ├── ContactDetailViewModel.cs   # View/edit single contact card
│       │   ├── ContactEditViewModel.cs     # Add/edit form logic
│       │   ├── GroupManagerViewModel.cs     # Manage 1-9 quick groups
│       │   ├── SearchViewModel.cs          # Global search / filter
│       │   ├── PrintViewModel.cs           # Print template selection & preview
│       │   ├── ImportExportViewModel.cs    # Import/export with merge UI
│       │   └── SettingsViewModel.cs        # App preferences, theme, user fields
│       │
│       ├── Views/
│       │   ├── ContactListPage.xaml        # Main list / browse page
│       │   ├── ContactDetailPage.xaml      # Read-only card view
│       │   ├── ContactEditPage.xaml        # Add / edit form
│       │   ├── GroupManagerPage.xaml        # Group list management
│       │   ├── SearchPage.xaml             # Search overlay / page
│       │   ├── PrintPage.xaml              # Print template picker & preview
│       │   ├── ImportExportPage.xaml        # Import / export wizard
│       │   ├── SettingsPage.xaml           # Preferences
│       │   └── Controls/
│       │       ├── ContactCardView.xaml    # Reusable card (used in detail + list)
│       │       ├── AlphaJumpBar.xaml       # A-Z sidebar for type-to-jump
│       │       ├── GroupChip.xaml          # Colored chip for group assignment
│       │       └── PrintPreviewView.xaml   # Rendered print template preview
│       │
│       ├── Services/
│       │   ├── IDataService.cs             # CRUD contract
│       │   ├── DataService.cs              # SQLite implementation
│       │   ├── ISearchService.cs           # Full-text search contract
│       │   ├── SearchService.cs            # FTS5 implementation
│       │   ├── IImportExportService.cs     # Import/export contract
│       │   ├── ImportExportService.cs      # vCard + CSV implementation
│       │   ├── IPrintService.cs            # Printing contract
│       │   ├── PrintService.cs             # Cross-platform print abstraction
│       │   └── IMergeService.cs            # Duplicate detection + merge
│       │   └── MergeService.cs
│       │
│       ├── Platforms/
│       │   ├── Android/
│       │   │   └── AndroidPrintService.cs  # Android PrintManager
│       │   ├── iOS/
│       │   │   └── ApplePrintService.cs    # UIPrintInteractionController
│       │   └── MacCatalyst/
│       │       └── ApplePrintService.cs    # NSPrintOperation
│       │
│       ├── Converters/
│       │   ├── BoolToVisibilityConverter.cs
│       │   ├── GroupColorConverter.cs      # Group → Brite accent color
│       │   └── InitialsConverter.cs        # Full name → initials for avatar
│       │
│       ├── Helpers/
│       │   ├── VCardParser.cs              # vCard 3.0 read/write
│       │   ├── CsvHelper.cs               # CSV read/write
│       │   └── PhoneFormatter.cs           # Format phone numbers for display
│       │
│       └── Resources/
│           ├── Themes/
│           │   └── brite.min.css           # Bootswatch Brite theme CSS
│           ├── Fonts/
│           │   └── (Bootstrap Icons or similar)
│           ├── Images/
│           │   └── (app icons, splash, placeholders)
│           └── Raw/
│               └── print_templates/
│                   ├── binder_full.html    # Full-page binder template
│                   ├── binder_compact.html # Compact binder template
│                   └── envelope.html       # Envelope / label template
```

### Namespaces

```
AddressBookPlus.Models
AddressBookPlus.ViewModels
AddressBookPlus.Views
AddressBookPlus.Views.Controls
AddressBookPlus.Services
AddressBookPlus.Converters
AddressBookPlus.Helpers
```

---

## 3. Data Model Classes

### PersonRecord

```csharp
using SQLite;

namespace AddressBookPlus.Models;

public class PersonRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    // Name
    public string Salutation { get; set; } = string.Empty;   // Mr., Ms., Dr., etc.
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    // Work
    public string Title { get; set; } = string.Empty;        // Job title
    public string Company { get; set; } = string.Empty;

    // Address
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    // Phones (original had exactly 3 phone slots)
    public string Phone1Label { get; set; } = "Home";
    public string Phone1 { get; set; } = string.Empty;
    public string Phone2Label { get; set; } = "Work";
    public string Phone2 { get; set; } = string.Empty;
    public string Phone3Label { get; set; } = "Fax";
    public string Phone3 { get; set; } = string.Empty;

    // Additional
    public string Profession { get; set; } = string.Empty;
    public DateTime? Birthday { get; set; }
    public string Notes { get; set; } = string.Empty;         // "Notes/Remarks"

    // User-definable field
    public string UserFieldLabel { get; set; } = string.Empty;
    public string UserFieldValue { get; set; } = string.Empty;

    // Metadata
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    // Computed
    [Ignore]
    public string FullName => $"{FirstName} {LastName}".Trim();

    [Ignore]
    public string SortName => $"{LastName}, {FirstName}".Trim(',', ' ');

    [Ignore]
    public string Initials =>
        $"{(FirstName.Length > 0 ? FirstName[0] : ' ')}{(LastName.Length > 0 ? LastName[0] : ' ')}".Trim();
}
```

### ContactGroup

```csharp
public class ContactGroup
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;         // Renameable
    public int SortOrder { get; set; }                        // 1-9 quick slots
    public string ColorKey { get; set; } = "Primary";        // Maps to Bootstrap variant
}
```

### ContactGroupMembership (join table)

```csharp
public class ContactGroupMembership
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int PersonId { get; set; }

    [Indexed]
    public int GroupId { get; set; }
}
```

### AddressBookDocument

```csharp
public class AddressBookDocument
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = "My Address Book";
    public string UserFieldDefaultLabel { get; set; } = "Custom";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
}
```

### FTS Virtual Table (created in code)

```sql
CREATE VIRTUAL TABLE IF NOT EXISTS person_fts USING fts5(
    first_name, last_name, company, city, notes,
    content='PersonRecord',
    content_rowid='Id'
);
```

---

## 4. Page / View Inventory

| Page | File | Description |
|------|------|-------------|
| **Contact List** | `ContactListPage.xaml` | Main browse view. `CollectionView` grouped alphabetically (A, B, C…). Includes `SearchBar` at top for type-to-jump filtering. Sidebar `AlphaJumpBar` for quick letter navigation. Group filter pills at top. FAB-style "Add" button. |
| **Contact Detail** | `ContactDetailPage.xaml` | Read-only card view of a single contact. Displays all fields in a `Border StyleClass="card"` layout. Action buttons: Edit, Delete, Call, Share. Shows group chips. |
| **Contact Edit** | `ContactEditPage.xaml` | Add/Edit form. All fields as `Entry StyleClass="form-control"` / `Picker StyleClass="form-select"`. Birthday as `DatePicker StyleClass="form-control"`. Notes as `Editor StyleClass="form-control"`. Save/Cancel buttons. |
| **Group Manager** | `GroupManagerPage.xaml` | List of 1-9 groups in a `CollectionView`. Each row: renameable name (`Entry`), color picker, member count badge. Add/remove groups. Drag-to-reorder. |
| **Search** | `SearchPage.xaml` | Full search page with `SearchBar StyleClass="form-control"` and advanced filter toggles (by field). Results in `CollectionView`. Supports multi-select for batch print/export. |
| **Print** | `PrintPage.xaml` | Template picker (binder full, binder compact, envelope/label). Live preview via `WebView` rendering HTML template. Print button triggers platform print service. |
| **Import / Export** | `ImportExportPage.xaml` | Tabbed or segmented: Import tab (pick file, preview, merge options) and Export tab (select contacts/group, choose format, export). |
| **Settings** | `SettingsPage.xaml` | Default user field label, default phone labels, theme selection (if supporting multiple Bootswatch themes), about info. |

### Reusable Controls

| Control | File | Description |
|---------|------|-------------|
| **ContactCardView** | `ContactCardView.xaml` | `Border StyleClass="card,shadow"` containing name, company, primary phone, city. Used as `CollectionView.ItemTemplate` and on detail page. |
| **AlphaJumpBar** | `AlphaJumpBar.xaml` | Vertical strip of A-Z `Label` elements. Tap fires a command to scroll `CollectionView` to that letter group. |
| **GroupChip** | `GroupChip.xaml` | Small `Border StyleClass="badge,bg-{color}"` with `Label StyleClass="on-{color},small"`. Shows group membership inline. |
| **PrintPreviewView** | `PrintPreviewView.xaml` | `WebView` that renders an HTML print template filled with contact data. |

---

## 5. ViewModel Inventory

All ViewModels inherit from `BaseViewModel` which extends `ObservableObject` (CommunityToolkit.Mvvm).

### BaseViewModel

```csharp
public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _title = string.Empty;
}
```

| ViewModel | Responsibilities | Key Commands |
|-----------|-----------------|--------------|
| **ContactListViewModel** | Loads contacts (all or by group), groups them alphabetically, handles type-to-jump filtering, group filter selection | `LoadContactsCommand`, `FilterByGroupCommand`, `SelectContactCommand`, `AddContactCommand`, `DeleteContactCommand` |
| **ContactDetailViewModel** | Loads a single `PersonRecord` by ID, resolves group memberships, formats display values | `EditCommand`, `DeleteCommand`, `ShareCommand`, `CallCommand(phoneNumber)` |
| **ContactEditViewModel** | Holds editable fields, validates input, saves new or updated `PersonRecord`, manages group assignment | `SaveCommand`, `CancelCommand`, `ToggleGroupCommand(groupId)` |
| **GroupManagerViewModel** | CRUD for `ContactGroup` entities, enforces 1-9 limit, provides rename and color change | `AddGroupCommand`, `RenameGroupCommand`, `DeleteGroupCommand`, `ReorderCommand` |
| **SearchViewModel** | Executes FTS5 queries, manages advanced filter state, supports multi-selection of results | `SearchCommand(query)`, `ClearCommand`, `SelectAllCommand`, `PrintSelectionCommand`, `ExportSelectionCommand` |
| **PrintViewModel** | Lists available print templates, fills template with selected contacts, triggers platform print | `SelectTemplateCommand`, `PreviewCommand`, `PrintCommand` |
| **ImportExportViewModel** | Handles file picking, parsing, merge preview, and export generation | `PickFileCommand`, `ImportCommand`, `ExportCommand`, `MergeStrategy` property |
| **SettingsViewModel** | Reads/writes app preferences via `Preferences` API | `SaveSettingsCommand`, `ResetDefaultsCommand` |

### Messaging (WeakReferenceMessenger)

| Message | Sender → Receiver | Purpose |
|---------|-------------------|---------|
| `ContactSavedMessage` | `ContactEditVM` → `ContactListVM` | Refresh list after add/edit |
| `ContactDeletedMessage` | `ContactDetailVM` → `ContactListVM` | Remove from list |
| `GroupChangedMessage` | `GroupManagerVM` → `ContactListVM` | Refresh group filters |
| `ThemeChangedMessage` | `SettingsVM` → `App` | Apply new Bootswatch theme |

---

## 6. Services Layer

### IDataService / DataService

```csharp
public interface IDataService
{
    // Contacts
    Task<List<PersonRecord>> GetAllContactsAsync();
    Task<List<PersonRecord>> GetContactsByGroupAsync(int groupId);
    Task<PersonRecord?> GetContactAsync(int id);
    Task<int> SaveContactAsync(PersonRecord person);
    Task<int> DeleteContactAsync(PersonRecord person);

    // Groups
    Task<List<ContactGroup>> GetGroupsAsync();
    Task<int> SaveGroupAsync(ContactGroup group);
    Task<int> DeleteGroupAsync(ContactGroup group);

    // Group membership
    Task AddToGroupAsync(int personId, int groupId);
    Task RemoveFromGroupAsync(int personId, int groupId);
    Task<List<int>> GetGroupIdsForPersonAsync(int personId);

    // Document
    Task<AddressBookDocument> GetDocumentAsync();
    Task SaveDocumentAsync(AddressBookDocument doc);
}
```

**Implementation:** SQLite via `sqlite-net-pcl`. Database stored at `FileSystem.AppDataDirectory/addressbook.db3`. Uses `SQLiteAsyncConnection` for all operations.

### ISearchService / SearchService

```csharp
public interface ISearchService
{
    Task<List<PersonRecord>> SearchAsync(string query);
    Task<List<PersonRecord>> SearchByFieldAsync(string field, string query);
    Task RebuildIndexAsync();
}
```

**Implementation:** Wraps FTS5 queries against the `person_fts` virtual table. `SearchAsync` uses `MATCH` with prefix queries for type-to-jump (`"dav*"`). `RebuildIndexAsync` called after import.

### IImportExportService / ImportExportService

```csharp
public interface IImportExportService
{
    Task<List<PersonRecord>> ImportVCardAsync(Stream stream);
    Task<List<PersonRecord>> ImportCsvAsync(Stream stream);
    Task<Stream> ExportVCardAsync(IEnumerable<PersonRecord> contacts);
    Task<Stream> ExportCsvAsync(IEnumerable<PersonRecord> contacts);
}
```

**Implementation:** vCard 3.0 parser (custom, lightweight — maps standard properties to `PersonRecord` fields). CSV uses a simple column mapping.

### IMergeService / MergeService

```csharp
public interface IMergeService
{
    Task<List<MergeCandidate>> FindDuplicatesAsync(
        List<PersonRecord> incoming, List<PersonRecord> existing);
    Task<PersonRecord> MergeRecordsAsync(
        PersonRecord incoming, PersonRecord existing, MergeStrategy strategy);
}

public enum MergeStrategy { SkipDuplicates, OverwriteExisting, MergeFields, AlwaysAdd }

public record MergeCandidate(PersonRecord Incoming, PersonRecord Existing, double Confidence);
```

**Implementation:** Levenshtein distance on name + exact match on phone/email. Confidence score > 0.8 = likely duplicate.

### IPrintService / Platform Implementations

```csharp
public interface IPrintService
{
    Task<bool> PrintHtmlAsync(string htmlContent, string jobName);
    bool IsPrintingAvailable { get; }
}
```

**Platform implementations:**

| Platform | Approach |
|----------|----------|
| **iOS** | `UIPrintInteractionController` with `UIMarkupTextPrintFormatter` |
| **Mac Catalyst** | `UIPrintInteractionController` (same as iOS, Catalyst compatible) |
| **Android** | `PrintManager` + `WebView`-based `PrintDocumentAdapter` |

Print templates are HTML files in `Resources/Raw/print_templates/`. The app fills placeholders with contact data via simple string replacement, then hands the HTML to the platform print service.

### DI Registration (MauiProgram.cs)

```csharp
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddSingleton<ISearchService, SearchService>();
builder.Services.AddTransient<IImportExportService, ImportExportService>();
builder.Services.AddTransient<IMergeService, MergeService>();
builder.Services.AddTransient<IPrintService, PrintService>();

// ViewModels (transient — new instance per navigation)
builder.Services.AddTransient<ContactListViewModel>();
builder.Services.AddTransient<ContactDetailViewModel>();
builder.Services.AddTransient<ContactEditViewModel>();
builder.Services.AddTransient<GroupManagerViewModel>();
builder.Services.AddTransient<SearchViewModel>();
builder.Services.AddTransient<PrintViewModel>();
builder.Services.AddTransient<ImportExportViewModel>();
builder.Services.AddTransient<SettingsViewModel>();

// Pages
builder.Services.AddTransient<ContactListPage>();
builder.Services.AddTransient<ContactDetailPage>();
builder.Services.AddTransient<ContactEditPage>();
builder.Services.AddTransient<GroupManagerPage>();
builder.Services.AddTransient<SearchPage>();
builder.Services.AddTransient<PrintPage>();
builder.Services.AddTransient<ImportExportPage>();
builder.Services.AddTransient<SettingsPage>();
```

---

## 7. Navigation Architecture

### Shell Structure

```xml
<Shell x:Class="AddressBookPlus.AppShell"
       xmlns:views="clr-namespace:AddressBookPlus.Views">

    <!-- Flyout: group quick-access -->
    <FlyoutItem Title="All People" Icon="people.png">
        <ShellContent ContentTemplate="{DataTemplate views:ContactListPage}" />
    </FlyoutItem>

    <FlyoutItem Title="Groups" Icon="folder.png">
        <ShellContent ContentTemplate="{DataTemplate views:GroupManagerPage}" />
    </FlyoutItem>

    <FlyoutItem Title="Search" Icon="search.png">
        <ShellContent ContentTemplate="{DataTemplate views:SearchPage}" />
    </FlyoutItem>

    <FlyoutItem Title="Print" Icon="print.png">
        <ShellContent ContentTemplate="{DataTemplate views:PrintPage}" />
    </FlyoutItem>

    <FlyoutItem Title="Import / Export" Icon="transfer.png">
        <ShellContent ContentTemplate="{DataTemplate views:ImportExportPage}" />
    </FlyoutItem>

    <FlyoutItem Title="Settings" Icon="settings.png">
        <ShellContent ContentTemplate="{DataTemplate views:SettingsPage}" />
    </FlyoutItem>
</Shell>
```

### Route Registration (AppShell.xaml.cs)

```csharp
Routing.RegisterRoute("contact/detail", typeof(ContactDetailPage));
Routing.RegisterRoute("contact/edit", typeof(ContactEditPage));
Routing.RegisterRoute("contact/new", typeof(ContactEditPage));
```

### Navigation Patterns

```csharp
// List → Detail
await Shell.Current.GoToAsync($"contact/detail?id={contact.Id}");

// Detail → Edit
await Shell.Current.GoToAsync($"contact/edit?id={contact.Id}");

// List → New
await Shell.Current.GoToAsync("contact/new");

// Back
await Shell.Current.GoToAsync("..");
```

ViewModels receive parameters via `[QueryProperty]`:

```csharp
[QueryProperty(nameof(ContactId), "id")]
public partial class ContactDetailViewModel : BaseViewModel { ... }
```

---

## 8. Bootstrap Theme Integration — Retro Mac × Brite

### Design Philosophy

The original Address Book Plus had a **clean, structured, card-based UI** with clear
sections, bordered panels, and a utilitarian feel. The **Brite** Bootswatch theme brings
**bold, saturated colors and a playful energy** — think of it as the classic Mac aesthetic
reborn in candy colors.

| Classic Mac Element | Brite/Bootstrap Mapping |
|---------------------|------------------------|
| Window chrome / titled panels | `Border StyleClass="card,shadow"` with a `Label StyleClass="h5"` header |
| Scrolling list pane | `CollectionView` inside a `Border StyleClass="card"` |
| Text fields with sunken borders | `Entry StyleClass="form-control"` (Brite gives these vivid focus rings) |
| Push buttons (OK, Cancel) | `Button StyleClass="btn-primary"` / `Button StyleClass="btn-secondary"` |
| Selection highlight | Brite's Primary color as `CollectionView.SelectionChangedCommand` styling |
| Status bar | `Border StyleClass="card,bg-light"` at bottom with `Label StyleClass="small,text-muted"` |
| Dialog boxes | Modal `ContentPage` wrapped in `Border StyleClass="card,shadow-lg"` |
| Group color dots | `Border StyleClass="badge,bg-success"` (or `bg-info`, `bg-warning`, etc.) |

### Theme Setup

#### 1. Download the Brite CSS

Download `brite.min.css` from [Bootswatch Brite](https://bootswatch.com/brite/) and place it at:

```
Resources/Themes/brite.min.css
```

#### 2. Register in .csproj

```xml
<ItemGroup>
  <BootstrapCss Include="Resources\Themes\brite.min.css" ThemeName="brite" />
</ItemGroup>
```

#### 3. MauiProgram.cs

```csharp
builder.UseMauiApp<App>()
       .UseBootstrapTheme(options =>
       {
           options.AddTheme<Themes.BriteTheme>("brite");
       });
```

#### 4. App.xaml.cs

```csharp
public App()
{
    InitializeComponent();
    Resources.MergedDictionaries.Add(new Themes.BriteTheme());
    BootstrapTheme.SyncFromResources(Resources);
}
```

#### 5. Every Page Code-Behind

```csharp
public ContactListPage(ContactListViewModel vm)
{
    InitializeComponent();
    BindingContext = vm;
    this.BackgroundColor = BootstrapTheme.Current.GetBackground();
}
```

### StyleClass Reference by Screen

#### Contact List Page

```xml
<!-- Search bar -->
<SearchBar Placeholder="Type to jump…"
           StyleClass="form-control" />

<!-- Group filter pills -->
<HorizontalStackLayout Spacing="4">
    <Button Text="All" StyleClass="btn-primary,btn-sm,btn-pill" />
    <Button Text="Family" StyleClass="btn-outline-success,btn-sm,btn-pill" />
    <Button Text="Work" StyleClass="btn-outline-info,btn-sm,btn-pill" />
</HorizontalStackLayout>

<!-- Contact list -->
<Border StyleClass="card">
    <CollectionView ItemsSource="{Binding GroupedContacts}"
                    IsGrouped="True"
                    SelectionMode="Single">
        <CollectionView.GroupHeaderTemplate>
            <DataTemplate>
                <Label Text="{Binding Key}"
                       StyleClass="h6,text-primary"
                       Padding="8,4" />
            </DataTemplate>
        </CollectionView.GroupHeaderTemplate>
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <!-- Each contact row -->
                <Border StyleClass="card,card-hoverable" Margin="4">
                    <HorizontalStackLayout Spacing="8" Padding="8">
                        <!-- Initials avatar -->
                        <Border StyleClass="badge,bg-primary"
                                WidthRequest="40" HeightRequest="40">
                            <Label Text="{Binding Initials}"
                                   StyleClass="on-primary"
                                   HorizontalOptions="Center"
                                   VerticalOptions="Center" />
                        </Border>
                        <VerticalStackLayout>
                            <Label Text="{Binding FullName}" StyleClass="h6" />
                            <Label Text="{Binding Company}" StyleClass="small,text-muted" />
                        </VerticalStackLayout>
                    </HorizontalStackLayout>
                </Border>
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>
</Border>

<!-- FAB Add button -->
<Button Text="+"
        StyleClass="btn-success,btn-lg"
        HorizontalOptions="End"
        VerticalOptions="End"
        Margin="16"
        Command="{Binding AddContactCommand}" />
```

#### Contact Detail Page (Card View)

```xml
<ScrollView>
    <VerticalStackLayout Spacing="12" Padding="16">
        <!-- Header card -->
        <Border StyleClass="card,shadow,text-bg-primary">
            <VerticalStackLayout Spacing="4" Padding="12">
                <Label Text="{Binding Contact.FullName}" StyleClass="h3,on-primary" />
                <Label Text="{Binding Contact.Title}" StyleClass="lead,on-primary" />
                <Label Text="{Binding Contact.Company}" StyleClass="on-primary" />
                <!-- Group chips -->
                <HorizontalStackLayout Spacing="4" BindableLayout.ItemsSource="{Binding Groups}">
                    <BindableLayout.ItemTemplate>
                        <DataTemplate>
                            <Border StyleClass="badge,bg-warning">
                                <Label Text="{Binding Name}" StyleClass="on-warning,small" />
                            </Border>
                        </DataTemplate>
                    </BindableLayout.ItemTemplate>
                </HorizontalStackLayout>
            </VerticalStackLayout>
        </Border>

        <!-- Address card -->
        <Border StyleClass="card">
            <VerticalStackLayout Spacing="2" Padding="12">
                <Label Text="Address" StyleClass="h6,text-muted" />
                <Label Text="{Binding Contact.AddressLine1}" StyleClass="form-text" />
                <Label Text="{Binding Contact.AddressLine2}" StyleClass="form-text" />
                <Label Text="{Binding CityStateZip}" StyleClass="form-text" />
                <Label Text="{Binding Contact.Country}" StyleClass="form-text" />
            </VerticalStackLayout>
        </Border>

        <!-- Phones card -->
        <Border StyleClass="card">
            <VerticalStackLayout Spacing="4" Padding="12">
                <Label Text="Phones" StyleClass="h6,text-muted" />
                <HorizontalStackLayout Spacing="8">
                    <Label Text="{Binding Contact.Phone1Label}" StyleClass="form-label" />
                    <Label Text="{Binding Contact.Phone1}" StyleClass="lead" />
                </HorizontalStackLayout>
                <!-- Phone2, Phone3 similar -->
            </VerticalStackLayout>
        </Border>

        <!-- Notes card -->
        <Border StyleClass="card">
            <VerticalStackLayout Spacing="4" Padding="12">
                <Label Text="Notes" StyleClass="h6,text-muted" />
                <Label Text="{Binding Contact.Notes}" StyleClass="form-text" />
            </VerticalStackLayout>
        </Border>

        <!-- Action buttons -->
        <HorizontalStackLayout Spacing="8" HorizontalOptions="Center">
            <Button Text="Edit" StyleClass="btn-primary" Command="{Binding EditCommand}" />
            <Button Text="Delete" StyleClass="btn-outline-danger" Command="{Binding DeleteCommand}" />
            <Button Text="Share" StyleClass="btn-outline-secondary" Command="{Binding ShareCommand}" />
        </HorizontalStackLayout>
    </VerticalStackLayout>
</ScrollView>
```

#### Contact Edit Page (Form)

```xml
<ScrollView>
    <VerticalStackLayout Spacing="8" Padding="16">
        <Border StyleClass="card">
            <VerticalStackLayout Spacing="8" Padding="12">
                <Label Text="Name" StyleClass="h5" />

                <Label Text="Salutation" StyleClass="form-label" />
                <Picker Title="Select…" StyleClass="form-select"
                        ItemsSource="{Binding Salutations}"
                        SelectedItem="{Binding Contact.Salutation}" />

                <Label Text="First Name" StyleClass="form-label" />
                <Entry Text="{Binding Contact.FirstName}"
                       StyleClass="form-control" Placeholder="First" />

                <Label Text="Last Name" StyleClass="form-label" />
                <Entry Text="{Binding Contact.LastName}"
                       StyleClass="form-control" Placeholder="Last" />
            </VerticalStackLayout>
        </Border>

        <Border StyleClass="card">
            <VerticalStackLayout Spacing="8" Padding="12">
                <Label Text="Work" StyleClass="h5" />

                <Label Text="Title" StyleClass="form-label" />
                <Entry Text="{Binding Contact.Title}"
                       StyleClass="form-control" Placeholder="Job Title" />

                <Label Text="Company" StyleClass="form-label" />
                <Entry Text="{Binding Contact.Company}"
                       StyleClass="form-control" Placeholder="Company" />
            </VerticalStackLayout>
        </Border>

        <!-- Address, Phones, Additional sections follow same pattern -->

        <Border StyleClass="card">
            <VerticalStackLayout Spacing="8" Padding="12">
                <Label Text="Notes" StyleClass="h5" />
                <Editor Text="{Binding Contact.Notes}"
                        StyleClass="form-control"
                        HeightRequest="120"
                        Placeholder="Notes and remarks…" />
            </VerticalStackLayout>
        </Border>

        <Border StyleClass="card">
            <VerticalStackLayout Spacing="8" Padding="12">
                <Label Text="Birthday" StyleClass="h5" />
                <DatePicker Date="{Binding Contact.Birthday}"
                            StyleClass="form-control" />
            </VerticalStackLayout>
        </Border>

        <Border StyleClass="card">
            <VerticalStackLayout Spacing="8" Padding="12">
                <Label Text="Groups" StyleClass="h5" />
                <CollectionView ItemsSource="{Binding AvailableGroups}"
                                SelectionMode="Multiple"
                                SelectedItems="{Binding SelectedGroups}">
                    <CollectionView.ItemTemplate>
                        <DataTemplate>
                            <HorizontalStackLayout Spacing="8" Padding="4">
                                <CheckBox IsChecked="{Binding IsSelected}"
                                          StyleClass="form-check-input" />
                                <Label Text="{Binding Name}"
                                       StyleClass="form-check-label" />
                            </HorizontalStackLayout>
                        </DataTemplate>
                    </CollectionView.ItemTemplate>
                </CollectionView>
            </VerticalStackLayout>
        </Border>

        <HorizontalStackLayout Spacing="8" HorizontalOptions="Center">
            <Button Text="Save" StyleClass="btn-success,btn-lg"
                    Command="{Binding SaveCommand}" />
            <Button Text="Cancel" StyleClass="btn-secondary,btn-lg"
                    Command="{Binding CancelCommand}" />
        </HorizontalStackLayout>
    </VerticalStackLayout>
</ScrollView>
```

### Complete StyleClass Mapping Table

| Control Usage | StyleClass | Notes |
|---------------|-----------|-------|
| Page headings | `h1` or `h2` | Top of each page |
| Section titles | `h5` | Inside cards |
| Field labels | `form-label` | Above every input |
| Help/hint text | `form-text` | Below inputs or for display-only data |
| Text entries | `form-control` | All `Entry` controls |
| Large entries | `form-control,form-control-lg` | Name fields on edit page |
| Text editors | `form-control` | `Editor` for notes, set `HeightRequest` |
| Dropdowns | `form-select` | `Picker` for salutation, phone labels |
| Date pickers | `form-control` | Birthday field |
| Search bars | `form-control` | Top of list page |
| Checkboxes | `form-check-input` | Group assignment |
| Checkbox labels | `form-check-label` | Next to checkboxes |
| Switches | `form-switch` | Settings toggles |
| Primary actions | `btn-primary` | Edit, main CTA |
| Success actions | `btn-success` | Save, Add |
| Danger actions | `btn-outline-danger` | Delete |
| Secondary actions | `btn-secondary` | Cancel |
| Filter pills | `btn-outline-{variant},btn-sm,btn-pill` | Group filters |
| Active filter | `btn-{variant},btn-sm,btn-pill` | Selected group |
| Section cards | `card` | `Border` wrapping each section |
| Elevated cards | `card,shadow` | Detail header, hover cards |
| Colored cards | `card,text-bg-primary` | Detail page header |
| Badges / chips | `badge,bg-{variant}` | Group chips, counts |
| Badge text | `on-{variant},small` | Inside badges |
| Muted captions | `small,text-muted` | Subtitles, timestamps |
| Display data | `lead` | Primary phone number, large display fields |

---

## 9. Implementation Phases

### Phase 1 — Project Scaffold & Theme (Days 1-2)

| # | Task | Details |
|---|------|---------|
| 1.1 | Create solution & project | `dotnet new maui -n AddressBookPlus`, set TFMs to `net10.0-ios;net10.0-maccatalyst;net10.0-android` |
| 1.2 | Add NuGet packages | `Plugin.Maui.BootstrapTheme`, `CommunityToolkit.Mvvm` (v11+), `sqlite-net-pcl`, `SQLitePCLRaw.bundle_green` |
| 1.3 | Download Brite CSS | Save `brite.min.css` to `Resources/Themes/` |
| 1.4 | Configure BootstrapTheme | Add `<BootstrapCss>` to `.csproj`, register in `MauiProgram.cs`, load in `App.xaml.cs` |
| 1.5 | Create folder structure | `Models/`, `ViewModels/`, `Views/`, `Views/Controls/`, `Services/`, `Converters/`, `Helpers/`, `Platforms/` |
| 1.6 | Build & verify | Confirm the app launches with Brite theme colors applied |

### Phase 2 — Data Layer (Days 3-4)

| # | Task | Details |
|---|------|---------|
| 2.1 | Implement model classes | `PersonRecord`, `ContactGroup`, `ContactGroupMembership`, `AddressBookDocument` |
| 2.2 | Implement `DataService` | SQLite database creation, all CRUD operations, FTS5 virtual table setup |
| 2.3 | Implement `SearchService` | FTS5 `MATCH` queries, prefix search for type-to-jump |
| 2.4 | Register services in DI | Wire up `IDataService`, `ISearchService` as singletons |
| 2.5 | Seed sample data | Create 10-15 sample contacts across 3 groups for development |
| 2.6 | Unit test data layer | Verify CRUD, search, and group membership operations |

### Phase 3 — Core UI: List + Detail (Days 5-8)

| # | Task | Details |
|---|------|---------|
| 3.1 | Create `BaseViewModel` | `ObservableObject` base with `IsBusy` and `Title` |
| 3.2 | Create `AppShell` | Flyout navigation with all menu items |
| 3.3 | Build `ContactListPage` | `CollectionView` with grouped, alphabetically sorted contacts; `SearchBar` filter; group filter pills |
| 3.4 | Build `ContactListViewModel` | Load contacts, group filtering, type-to-jump search, selection handling |
| 3.5 | Build `ContactCardView` | Reusable card template with initials avatar, name, company |
| 3.6 | Build `AlphaJumpBar` | A-Z sidebar; tap scrolls `CollectionView` to letter |
| 3.7 | Build `ContactDetailPage` | Read-only card layout with all fields, group chips, action buttons |
| 3.8 | Build `ContactDetailViewModel` | Load contact by ID, format display values, wire Edit/Delete/Share commands |
| 3.9 | Wire navigation | List → Detail via `Shell.GoToAsync` with `?id=` query parameter |

### Phase 4 — Add / Edit Flow (Days 9-11)

| # | Task | Details |
|---|------|---------|
| 4.1 | Build `ContactEditPage` | Form with all fields, grouped in cards; Birthday `DatePicker`, Notes `Editor` |
| 4.2 | Build `ContactEditViewModel` | Editable field binding, validation, Save/Cancel commands |
| 4.3 | Wire group assignment | `CollectionView` with `CheckBox` for each group |
| 4.4 | Add messaging | `ContactSavedMessage` and `ContactDeletedMessage` via `WeakReferenceMessenger` |
| 4.5 | Handle new vs. edit | Route `contact/new` vs `contact/edit?id=` |
| 4.6 | Input validation | Required: LastName; validate phone format; visual error states via `VisualStateManager` |

### Phase 5 — Groups (Days 12-13)

| # | Task | Details |
|---|------|---------|
| 5.1 | Build `GroupManagerPage` | List of groups with inline rename, color selection, member count |
| 5.2 | Build `GroupManagerViewModel` | CRUD groups, enforce 1-9 limit, `GroupChangedMessage` |
| 5.3 | Build `GroupChip` control | Reusable badge for group display |
| 5.4 | Wire group filtering in list | Tap group pill → filter `CollectionView` to group members |

### Phase 6 — Search (Days 14-15)

| # | Task | Details |
|---|------|---------|
| 6.1 | Build `SearchPage` | `SearchBar` + results `CollectionView` + advanced field toggles |
| 6.2 | Build `SearchViewModel` | FTS5 query execution, multi-select, batch action commands |
| 6.3 | Enhance type-to-jump | Real-time prefix search as user types in list page `SearchBar` |

### Phase 7 — Import / Export (Days 16-18)

| # | Task | Details |
|---|------|---------|
| 7.1 | Implement `VCardParser` | Parse vCard 3.0 `BEGIN:VCARD` ... `END:VCARD` blocks to `PersonRecord` |
| 7.2 | Implement `CsvHelper` | Column mapping with header detection |
| 7.3 | Implement `MergeService` | Duplicate detection (name + phone), confidence scoring |
| 7.4 | Build `ImportExportPage` | File picker, preview table, merge strategy selection, export format chooser |
| 7.5 | Build `ImportExportViewModel` | Orchestrate parse → preview → merge → save / export → share flow |

### Phase 8 — Printing (Days 19-21)

| # | Task | Details |
|---|------|---------|
| 8.1 | Create HTML print templates | Binder full page, binder compact, envelope/label — in `Resources/Raw/` |
| 8.2 | Implement `IPrintService` | Platform-specific: iOS/MacCatalyst `UIPrintInteractionController`, Android `PrintManager` |
| 8.3 | Build `PrintPage` | Template picker, live `WebView` preview, print button |
| 8.4 | Build `PrintViewModel` | Template selection, contact data injection, preview rendering |
| 8.5 | Build `PrintPreviewView` | Reusable `WebView` control for rendered template |

### Phase 9 — Settings & Polish (Days 22-24)

| # | Task | Details |
|---|------|---------|
| 9.1 | Build `SettingsPage` | Default field labels, theme selection (if multi-theme), about |
| 9.2 | Build `SettingsViewModel` | Read/write `Preferences`, theme switching via `BootstrapTheme.Apply()` |
| 9.3 | Add converters | `BoolToVisibilityConverter`, `GroupColorConverter`, `InitialsConverter` |
| 9.4 | Empty states | `CollectionView.EmptyView` for no contacts, no search results |
| 9.5 | Error handling | Global exception handler, user-friendly error alerts via `DisplayAlertAsync` |
| 9.6 | Accessibility | `SemanticProperties.Description` on interactive elements, `SemanticProperties.Hint` on buttons |
| 9.7 | Performance pass | Compiled bindings (`x:DataType`), `RecycleElement` on `CollectionView` items |

### Phase 10 — Testing & Release (Days 25-28)

| # | Task | Details |
|---|------|---------|
| 10.1 | Unit tests | Data services, search service, merge logic, view model commands |
| 10.2 | UI testing | Manual test matrix: iOS Simulator, Mac Catalyst, Android Emulator |
| 10.3 | Print testing | Verify output on each platform with each template |
| 10.4 | Import/export testing | Round-trip vCard and CSV files |
| 10.5 | App icons & splash | Configure via `.csproj` `<MauiIcon>` and `<MauiSplashScreen>` |
| 10.6 | Final polish | Review all `StyleClass` assignments, verify theme consistency, check dark mode fallback |

---

## Appendix A — NuGet Packages

| Package | Version | Purpose |
|---------|---------|---------|
| `Plugin.Maui.BootstrapTheme` | Latest | Bootstrap 5 styling via CSS-generated ResourceDictionaries |
| `CommunityToolkit.Mvvm` | 11.x+ | Source-generated MVVM: `[ObservableProperty]`, `[RelayCommand]`, `WeakReferenceMessenger` |
| `sqlite-net-pcl` | 1.9+ | SQLite ORM for data persistence |
| `SQLitePCLRaw.bundle_green` | Latest | SQLite native bindings |

## Appendix B — .csproj Key Configuration

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFrameworks>net10.0-ios;net10.0-maccatalyst;net10.0-android</TargetFrameworks>
    <UseMaui>true</UseMaui>
    <SingleProject>true</SingleProject>
    <RootNamespace>AddressBookPlus</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Plugin.Maui.BootstrapTheme" />
    <PackageReference Include="CommunityToolkit.Mvvm" />
    <PackageReference Include="sqlite-net-pcl" />
    <PackageReference Include="SQLitePCLRaw.bundle_green" />
  </ItemGroup>

  <ItemGroup>
    <BootstrapCss Include="Resources\Themes\brite.min.css" ThemeName="brite" />
  </ItemGroup>
</Project>
```

## Appendix C — API Currency Reminders

Per the `.NET MAUI 10 Current APIs` guardrail:

- ✅ Use `CollectionView` — **never** `ListView` (deprecated in .NET 10)
- ✅ Use `Border` — **never** `Frame` (Xamarin.Forms legacy)
- ✅ Use `VerticalStackLayout` / `HorizontalStackLayout` — **never** `Compatibility.StackLayout`
- ✅ Use `WeakReferenceMessenger` — **never** `MessagingCenter` (internal in .NET 10)
- ✅ Use `DisplayAlertAsync()` — **never** `DisplayAlert()`
- ✅ Use `MainThread.BeginInvokeOnMainThread()` — **never** `Device.BeginInvokeOnMainThread()`
- ✅ Use constructor injection — **never** `DependencyService`
- ✅ Use `SemanticProperties.Description` — **never** `AutomationProperties.Name`
- ✅ Use `ActivityIndicator` for busy state — **never** `Page.IsBusy`
- ✅ Use `*Async` animation names (`FadeToAsync`, `ScaleToAsync`, etc.)
