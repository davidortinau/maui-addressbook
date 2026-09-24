using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using AddressBookPlus.Models;
using AddressBookPlus.Services;

namespace AddressBookPlus.ViewModels;

public partial class ContactDetailViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IDataService _dataService;

    public ContactDetailViewModel(IDataService dataService)
    {
        _dataService = dataService;
        ContactGroups = new List<ContactGroup>();
    }

    [ObservableProperty]
    private PersonRecord? _contact;

    [ObservableProperty]
    private List<ContactGroup> _contactGroups;

    public string FormattedAddress
    {
        get
        {
            if (Contact == null) return string.Empty;

            var lines = new List<string>();
            
            if (!string.IsNullOrWhiteSpace(Contact.AddressLine1))
                lines.Add(Contact.AddressLine1);
                
            if (!string.IsNullOrWhiteSpace(Contact.AddressLine2))
                lines.Add(Contact.AddressLine2);
                
            var cityStateZip = new List<string>();
            if (!string.IsNullOrWhiteSpace(Contact.City))
                cityStateZip.Add(Contact.City);
            if (!string.IsNullOrWhiteSpace(Contact.State))
                cityStateZip.Add(Contact.State);
            if (!string.IsNullOrWhiteSpace(Contact.PostalCode))
                cityStateZip.Add(Contact.PostalCode);
                
            if (cityStateZip.Any())
                lines.Add(string.Join(", ", cityStateZip));
                
            if (!string.IsNullOrWhiteSpace(Contact.Country))
                lines.Add(Contact.Country);

            return string.Join(Environment.NewLine, lines);
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idObj) && int.TryParse(idObj.ToString(), out var contactId))
        {
            _ = LoadContactAsync(contactId);
        }
    }

    private async Task LoadContactAsync(int contactId)
    {
        IsBusy = true;
        try
        {
            Contact = await _dataService.GetContactAsync(contactId);
            if (Contact != null)
            {
                Title = $"{Contact.FirstName} {Contact.LastName}";
                var groupIds = await _dataService.GetGroupIdsForPersonAsync(contactId);
                var allGroups = await _dataService.GetGroupsAsync();
                ContactGroups = allGroups.Where(g => groupIds.Contains(g.Id)).ToList();
                OnPropertyChanged(nameof(FormattedAddress));
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task EditAsync()
    {
        if (Contact != null)
        {
            await Shell.Current.GoToAsync($"contacts/edit?id={Contact.Id}");
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Contact == null) return;

        var page = Application.Current?.Windows[0]?.Page;
        if (page == null) return;

        var result = await page.DisplayAlertAsync(
            "Delete Contact", 
            $"Are you sure you want to delete {Contact.FirstName} {Contact.LastName}?", 
            "Delete", 
            "Cancel");

        if (result == true)
        {
            await _dataService.DeleteContactAsync(Contact);
            WeakReferenceMessenger.Default.Send(new ContactDeletedMessage(Contact.Id));
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    private async Task CopyAddressAsync()
    {
        if (!string.IsNullOrWhiteSpace(FormattedAddress))
        {
            await Clipboard.SetTextAsync(FormattedAddress);
            // Could show a toast or brief message here
        }
    }
}