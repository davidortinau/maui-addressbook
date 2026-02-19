using AddressBookPlus.Models;

namespace AddressBookPlus.Services;

public interface ISearchService
{
    Task<List<PersonRecord>> SearchAsync(string query);
    Task<List<PersonRecord>> TypeToJumpAsync(string prefix);
}