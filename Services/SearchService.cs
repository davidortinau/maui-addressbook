using AddressBookPlus.Models;

namespace AddressBookPlus.Services;

public class SearchService : ISearchService
{
    private readonly IDataService _dataService;

    public SearchService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<List<PersonRecord>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<PersonRecord>();

        var allContacts = await _dataService.GetAllContactsAsync();
        var lowerQuery = query.ToLowerInvariant();

        return allContacts.Where(contact =>
            (!string.IsNullOrEmpty(contact.FirstName) && contact.FirstName.ToLowerInvariant().Contains(lowerQuery)) ||
            (!string.IsNullOrEmpty(contact.LastName) && contact.LastName.ToLowerInvariant().Contains(lowerQuery)) ||
            (!string.IsNullOrEmpty(contact.Company) && contact.Company.ToLowerInvariant().Contains(lowerQuery)) ||
            (!string.IsNullOrEmpty(contact.City) && contact.City.ToLowerInvariant().Contains(lowerQuery)) ||
            (!string.IsNullOrEmpty(contact.Phone1) && contact.Phone1.Contains(query)) ||
            (!string.IsNullOrEmpty(contact.Phone2) && contact.Phone2.Contains(query)) ||
            (!string.IsNullOrEmpty(contact.Phone3) && contact.Phone3.Contains(query)) ||
            (!string.IsNullOrEmpty(contact.Notes) && contact.Notes.ToLowerInvariant().Contains(lowerQuery))
        ).ToList();
    }

    public async Task<List<PersonRecord>> TypeToJumpAsync(string prefix)
    {
        if (string.IsNullOrWhiteSpace(prefix))
            return new List<PersonRecord>();

        var allContacts = await _dataService.GetAllContactsAsync();
        var lowerPrefix = prefix.ToLowerInvariant();

        return allContacts.Where(contact =>
            !string.IsNullOrEmpty(contact.SortName) && 
            contact.SortName.ToLowerInvariant().StartsWith(lowerPrefix)
        ).ToList();
    }
}