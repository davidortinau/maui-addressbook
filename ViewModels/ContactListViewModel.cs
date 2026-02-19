using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using AddressBookPlus.Models;
using AddressBookPlus.Services;

namespace AddressBookPlus.ViewModels;

public partial class ContactListViewModel : BaseViewModel
{
    private readonly IDataService _dataService;
    private readonly ISearchService _searchService;

    public ContactListViewModel(IDataService dataService, ISearchService searchService)
    {
        _dataService = dataService;
        _searchService = searchService;
        
        Contacts = new ObservableCollection<PersonRecord>();
        Groups = new ObservableCollection<ContactGroup>();

        // Register for messages
        WeakReferenceMessenger.Default.Register<ContactSavedMessage>(this, (recipient, message) =>
        {
            LoadDataCommand.Execute(null);
        });

        WeakReferenceMessenger.Default.Register<ContactDeletedMessage>(this, (recipient, message) =>
        {
            LoadDataCommand.Execute(null);
        });
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ContactCount))]
    private ObservableCollection<PersonRecord> _contacts;

    [ObservableProperty]
    private ObservableCollection<ContactGroup> _groups;

    [ObservableProperty]
    private PersonRecord? _selectedContact;

    [ObservableProperty]
    private ContactGroup? _selectedGroup;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _filterLabel = string.Empty;

    public int ContactCount => Contacts?.Count ?? 0;

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        IsBusy = true;
        try
        {
            await _dataService.InitializeAsync();
            
            var groups = await _dataService.GetGroupsAsync();
            Groups.Clear();
            foreach (var group in groups)
            {
                Groups.Add(group);
            }

            var contacts = await _dataService.GetAllContactsAsync();
            Contacts.Clear();
            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }
            OnPropertyChanged(nameof(ContactCount));

            Title = "Address Book Plus";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SelectGroupAsync(ContactGroup? group)
    {
        SelectedGroup = group;
        
        if (group == null)
        {
            // Show all contacts
            var allContacts = await _dataService.GetAllContactsAsync();
            Contacts.Clear();
            foreach (var contact in allContacts)
            {
                Contacts.Add(contact);
            }
            OnPropertyChanged(nameof(ContactCount));
        }
        else
        {
            // Filter by group
            var filteredContacts = await _dataService.GetContactsByGroupAsync(group.Id);
            Contacts.Clear();
            foreach (var contact in filteredContacts)
            {
                Contacts.Add(contact);
            }
            OnPropertyChanged(nameof(ContactCount));
        }
    }

    [RelayCommand]
    private async Task ToggleGroupFilterAsync(ContactGroup? group)
    {
        if (group == null) return;

        if (SelectedGroup?.Id == group.Id)
        {
            // Deselect — clear filter
            foreach (var g in Groups) g.IsSelected = false;
            FilterLabel = string.Empty;
            await SelectGroupAsync(null);
        }
        else
        {
            // Select this group
            foreach (var g in Groups) g.IsSelected = (g.Id == group.Id);
            FilterLabel = $"Showing: {group.Name}";
            await SelectGroupAsync(group);
        }

        // Force BindableLayout to refresh visual state
        var snapshot = Groups.ToList();
        Groups.Clear();
        foreach (var g in snapshot) Groups.Add(g);
    }

    [RelayCommand]
    private async Task GoToDetailAsync(PersonRecord? contact)
    {
        if (contact == null) return;
        await Shell.Current.GoToAsync($"contacts/detail?id={contact.Id}");
    }

    [RelayCommand]
    private async Task AddContactAsync()
    {
        await Shell.Current.GoToAsync("contacts/edit");
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadDataAsync();
    }

    partial void OnSearchTextChanged(string value)
    {
        _ = Task.Run(async () =>
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                // Reload full list
                var allContacts = await _dataService.GetAllContactsAsync();
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Contacts.Clear();
                    foreach (var contact in allContacts)
                    {
                        Contacts.Add(contact);
                    }
                    OnPropertyChanged(nameof(ContactCount));
                });
            }
            else
            {
                // Search
                var searchResults = await _searchService.SearchAsync(value);
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Contacts.Clear();
                    foreach (var contact in searchResults)
                    {
                        Contacts.Add(contact);
                    }
                    OnPropertyChanged(nameof(ContactCount));
                });
            }
        });
    }
}