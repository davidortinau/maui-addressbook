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
    private string _title = string.Empty;

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
                Title = contact.Title ?? string.Empty;
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
        }
        finally
        {
            IsBusy = false;
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
                Title = Title,
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

            if (IsNew)
            {
                var newContactId = await _dataService.SaveContactAsync(contact);
                WeakReferenceMessenger.Default.Send(new ContactSavedMessage(newContactId));
            }
            else
            {
                await _dataService.SaveContactAsync(contact);
                WeakReferenceMessenger.Default.Send(new ContactSavedMessage(ContactId));
            }

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