using AddressBookPlus.Models;

namespace AddressBookPlus.Services;

public interface IDataService
{
    Task InitializeAsync();
    
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
    Task AddToGroupAsync(int personId, int groupId);
    Task RemoveFromGroupAsync(int personId, int groupId);
    Task<List<int>> GetGroupIdsForPersonAsync(int personId);

    // Document
    Task<AddressBookDocument> GetDocumentAsync();
    Task SaveDocumentAsync(AddressBookDocument doc);
}