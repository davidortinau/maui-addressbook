using AddressBookPlus.Models;
using SQLite;

namespace AddressBookPlus.Services;

public class DataService : IDataService
{
    private SQLiteAsyncConnection? _database;

    public async Task InitializeAsync()
    {
        if (_database != null) return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "addressbook.db3");
        _database = new SQLiteAsyncConnection(dbPath);

        await _database.CreateTableAsync<PersonRecord>();
        await _database.CreateTableAsync<ContactGroup>();
        await _database.CreateTableAsync<ContactGroupMembership>();
        await _database.CreateTableAsync<AddressBookDocument>();

        await SeedDefaultDataAsync();
    }

    private async Task SeedDefaultDataAsync()
    {
        // Seed default groups
        var groups = await _database!.Table<ContactGroup>().CountAsync();
        if (groups == 0)
        {
            await _database.InsertAsync(new ContactGroup { SlotNumber = 1, Name = "Family", ColorKey = "Success" });
            await _database.InsertAsync(new ContactGroup { SlotNumber = 2, Name = "Friends", ColorKey = "Info" });
            await _database.InsertAsync(new ContactGroup { SlotNumber = 3, Name = "Work", ColorKey = "Warning" });
        }

        // Seed sample contacts
        var contacts = await _database.Table<PersonRecord>().CountAsync();
        if (contacts == 0)
        {
            var sampleContacts = new[]
            {
                new PersonRecord { FirstName = "Jane", LastName = "Smith", Company = "Acme Corp", Phone1 = "555-0101", City = "Seattle" },
                new PersonRecord { FirstName = "Bob", LastName = "Johnson", Company = "Tech Solutions", Phone1 = "555-0102", City = "Portland" },
                new PersonRecord { FirstName = "Alice", LastName = "Williams", Company = "Design Studio", Phone1 = "555-0103", Phone2 = "555-0104", City = "San Francisco" },
                new PersonRecord { FirstName = "David", LastName = "Brown", Company = "Brown & Associates", Phone1 = "555-0105", City = "Los Angeles", Notes = "Law firm partner" },
                new PersonRecord { FirstName = "Sarah", LastName = "Davis", Company = "Medical Center", Phone1 = "555-0106", City = "Denver" },
                new PersonRecord { FirstName = "Michael", LastName = "Wilson", Phone1 = "555-0107", Phone2 = "555-0108", City = "Austin", Notes = "College friend" },
                new PersonRecord { FirstName = "Emily", LastName = "Taylor", Company = "Marketing Plus", Phone1 = "555-0109", City = "Chicago" },
                new PersonRecord { FirstName = "James", LastName = "Anderson", Company = "Anderson Consulting", Phone1 = "555-0110", Phone3 = "555-0111", City = "Boston" },
                new PersonRecord { FirstName = "Lisa", LastName = "Martinez", Phone1 = "555-0112", City = "Miami", Notes = "Neighbor" },
                new PersonRecord { FirstName = "Robert", LastName = "Garcia", Company = "Garcia Industries", Phone1 = "555-0113", City = "Phoenix" }
            };

            foreach (var contact in sampleContacts)
            {
                await _database.InsertAsync(contact);
            }
        }
    }

    public async Task<List<PersonRecord>> GetAllContactsAsync()
    {
        await InitializeAsync();
        var contacts = await _database!.Table<PersonRecord>().ToListAsync();
        return contacts.OrderBy(p => p.SortName).ToList();
    }

    public async Task<List<PersonRecord>> GetContactsByGroupAsync(int groupId)
    {
        await InitializeAsync();
        var query = @"
            SELECT p.* FROM People p
            INNER JOIN GroupMemberships m ON p.Id = m.PersonId
            WHERE m.GroupId = ?";
        var contacts = await _database!.QueryAsync<PersonRecord>(query, groupId);
        return contacts.OrderBy(p => p.SortName).ToList();
    }

    public async Task<PersonRecord?> GetContactAsync(int id)
    {
        await InitializeAsync();
        return await _database!.Table<PersonRecord>().Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveContactAsync(PersonRecord person)
    {
        await InitializeAsync();
        if (person.Id != 0)
        {
            return await _database!.UpdateAsync(person);
        }
        else
        {
            return await _database!.InsertAsync(person);
        }
    }

    public async Task<int> DeleteContactAsync(PersonRecord person)
    {
        await InitializeAsync();
        // Remove group memberships first
        await _database!.ExecuteAsync("DELETE FROM GroupMemberships WHERE PersonId = ?", person.Id);
        return await _database!.DeleteAsync(person);
    }

    public async Task<List<ContactGroup>> GetGroupsAsync()
    {
        await InitializeAsync();
        return await _database!.Table<ContactGroup>().OrderBy(g => g.Name).ToListAsync();
    }

    public async Task<int> SaveGroupAsync(ContactGroup group)
    {
        await InitializeAsync();
        if (group.Id != 0)
        {
            return await _database!.UpdateAsync(group);
        }
        else
        {
            return await _database!.InsertAsync(group);
        }
    }

    public async Task<int> DeleteGroupAsync(ContactGroup group)
    {
        await InitializeAsync();
        // Remove memberships first
        await _database!.ExecuteAsync("DELETE FROM GroupMemberships WHERE GroupId = ?", group.Id);
        return await _database!.DeleteAsync(group);
    }

    public async Task AddToGroupAsync(int personId, int groupId)
    {
        await InitializeAsync();
        var existing = await _database!.Table<ContactGroupMembership>()
            .Where(m => m.PersonId == personId && m.GroupId == groupId)
            .FirstOrDefaultAsync();
        
        if (existing == null)
        {
            await _database.InsertAsync(new ContactGroupMembership { PersonId = personId, GroupId = groupId });
        }
    }

    public async Task RemoveFromGroupAsync(int personId, int groupId)
    {
        await InitializeAsync();
        await _database!.ExecuteAsync("DELETE FROM GroupMemberships WHERE PersonId = ? AND GroupId = ?", personId, groupId);
    }

    public async Task<List<int>> GetGroupIdsForPersonAsync(int personId)
    {
        await InitializeAsync();
        var memberships = await _database!.Table<ContactGroupMembership>()
            .Where(m => m.PersonId == personId)
            .ToListAsync();
        return memberships.Select(m => m.GroupId).ToList();
    }

    public async Task<AddressBookDocument> GetDocumentAsync()
    {
        await InitializeAsync();
        var doc = await _database!.Table<AddressBookDocument>().FirstOrDefaultAsync();
        return doc ?? new AddressBookDocument();
    }

    public async Task SaveDocumentAsync(AddressBookDocument doc)
    {
        await InitializeAsync();
        var existing = await _database!.Table<AddressBookDocument>().FirstOrDefaultAsync();
        if (existing != null)
        {
            doc.Id = existing.Id;
            await _database.UpdateAsync(doc);
        }
        else
        {
            await _database.InsertAsync(doc);
        }
    }
}