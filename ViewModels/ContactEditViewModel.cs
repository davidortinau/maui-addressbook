using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using AddressBookPlus.Models;
using AddressBookPlus.Services;

namespace AddressBookPlus.ViewModels;

public partial class ContactEditViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IDataService _dataService;

    public ContactEditViewModel(IDataService dataService)
    {
        _dataService = dataService;
        
        // Initialize with default values
        Phone1Label = "Mobile";
        Phone2Label = "Home";
        Phone3Label = "Work";

        GroupOptions = new ObservableCollection<GroupSelection>();
    }

    [ObservableProperty]
    private int _contactId;

    [ObservableProperty]
    private string _salutation = string.Empty;

    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string _jobTitle = string.Empty;

    [ObservableProperty]
    private string _company = string.Empty;

    [ObservableProperty]
    private string _addressLine1 = string.Empty;

    [ObservableProperty]
    private string _addressLine2 = string.Empty;

    [ObservableProperty]
    private string _city = string.Empty;

    [ObservableProperty]
    private string _state = string.Empty;

    [ObservableProperty]
    private string _postalCode = string.Empty;

    [ObservableProperty]
    private string _country = string.Empty;

    [ObservableProperty]
    private string _phone1Label = string.Empty;

    [ObservableProperty]
    private string _phone1 = string.Empty;

    [ObservableProperty]
    private string _phone2Label = string.Empty;

    [ObservableProperty]
    private string _phone2 = string.Empty;

    [ObservableProperty]
    private string _phone3Label = string.Empty;

    [ObservableProperty]
    private string _phone3 = string.Empty;

    [ObservableProperty]
    private string _profession = string.Empty;

    [ObservableProperty]
    private string _notes = string.Empty;

    [ObservableProperty]
    private string _customFieldLabel = string.Empty;

    [ObservableProperty]
    private string _customFieldValue = string.Empty;

    [ObservableProperty]
    private DateTime? _birthday;

    [ObservableProperty]
    private ObservableCollection<GroupSelection> _groupOptions;

    public bool IsNew => ContactId == 0;

    public List<string> SalutationOptions => ["", "Mr.", "Ms.", "Mrs.", "Dr.", "Prof."];

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idObj) && int.TryParse(idObj.ToString(), out var contactId))
        {
            ContactId = contactId;
            _ = LoadContactAsync(contactId);
        }
        else
        {
            ContactId = 0;
            Title = "New Contact";
            _ = LoadGroupOptionsAsync(0);
        }
        
        OnPropertyChanged(nameof(IsNew));
    }

    private async Task LoadContactAsync(int contactId)
    {
        IsBusy = true;
        try
        {
            var contact = await _dataService.GetContactAsync(contactId);
            if (contact != null)
            {
                Salutation = contact.Salutation ?? string.Empty;
                FirstName = contact.FirstName ?? string.Empty;
                LastName = contact.LastName ?? string.Empty;
                JobTitle = contact.Title ?? string.Empty;
                Company = contact.Company ?? string.Empty;
                AddressLine1 = contact.AddressLine1 ?? string.Empty;
                AddressLine2 = contact.AddressLine2 ?? string.Empty;
                City = contact.City ?? string.Empty;
                State = contact.State ?? string.Empty;
                PostalCode = contact.PostalCode ?? string.Empty;
                Country = contact.Country ?? string.Empty;
                Phone1Label = contact.Phone1Label ?? "Mobile";
                Phone1 = contact.Phone1 ?? string.Empty;
                Phone2Label = contact.Phone2Label ?? "Home";
                Phone2 = contact.Phone2 ?? string.Empty;
                Phone3Label = contact.Phone3Label ?? "Work";
                Phone3 = contact.Phone3 ?? string.Empty;
                Profession = contact.Profession ?? string.Empty;
                Notes = contact.Notes ?? string.Empty;
                CustomFieldLabel = contact.CustomFieldLabel ?? string.Empty;
                CustomFieldValue = contact.CustomFieldValue ?? string.Empty;
                Birthday = contact.Birthday;
                
                base.Title = $"Edit {FirstName} {LastName}";
            }

            await LoadGroupOptionsAsync(contactId);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task LoadGroupOptionsAsync(int contactId)
    {
        var allGroups = await _dataService.GetGroupsAsync();
        var memberGroupIds = contactId > 0
            ? await _dataService.GetGroupIdsForPersonAsync(contactId)
            : new List<int>();

        GroupOptions.Clear();
        foreach (var group in allGroups)
        {
            GroupOptions.Add(new GroupSelection
            {
                GroupId = group.Id,
                GroupName = group.Name,
                IsMember = memberGroupIds.Contains(group.Id)
            });
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName))
        {
            var page = Application.Current?.Windows[0]?.Page;
            if (page != null)
                await page.DisplayAlertAsync("Error", "Please enter at least a first or last name.", "OK");
            return;
        }

        IsBusy = true;
        try
        {
            var contact = new PersonRecord
            {
                Id = ContactId,
                Salutation = Salutation,
                FirstName = FirstName,
                LastName = LastName,
                Title = JobTitle,
                Company = Company,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                State = State,
                PostalCode = PostalCode,
                Country = Country,
                Phone1Label = Phone1Label,
                Phone1 = Phone1,
                Phone2Label = Phone2Label,
                Phone2 = Phone2,
                Phone3Label = Phone3Label,
                Phone3 = Phone3,
                Profession = Profession,
                Notes = Notes,
                CustomFieldLabel = CustomFieldLabel,
                CustomFieldValue = CustomFieldValue,
                Birthday = Birthday
            };

            int savedId;
            if (IsNew)
            {
                savedId = await _dataService.SaveContactAsync(contact);
            }
            else
            {
                await _dataService.SaveContactAsync(contact);
                savedId = ContactId;
            }

            // Save group memberships
            var currentGroupIds = await _dataService.GetGroupIdsForPersonAsync(savedId);
            foreach (var groupOpt in GroupOptions)
            {
                if (groupOpt.IsMember && !currentGroupIds.Contains(groupOpt.GroupId))
                    await _dataService.AddToGroupAsync(savedId, groupOpt.GroupId);
                else if (!groupOpt.IsMember && currentGroupIds.Contains(groupOpt.GroupId))
                    await _dataService.RemoveFromGroupAsync(savedId, groupOpt.GroupId);
            }

            WeakReferenceMessenger.Default.Send(new ContactSavedMessage(savedId));
            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}

public class GroupSelection : ObservableObject
{
    public int GroupId { get; set; }
    public string GroupName { get; set; } = "";

    private bool _isMember;
    public bool IsMember
    {
        get => _isMember;
        set => SetProperty(ref _isMember, value);
    }
}