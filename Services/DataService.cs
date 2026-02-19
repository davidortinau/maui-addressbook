using AddressBookPlus.Models;
using SQLite;

namespace AddressBookPlus.Services;

public class DataService : IDataService
{
    private SQLiteAsyncConnection? _database;

    public async Task InitializeAsync()
    {
        if (_database != null) return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "addressbook_v2.db3");
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
                new PersonRecord { Salutation = "Ms.", FirstName = "Jane", LastName = "Smith", Title = "VP of Engineering", Company = "Acme Corp", AddressLine1 = "123 Pine Street", AddressLine2 = "Suite 400", City = "Seattle", State = "WA", PostalCode = "98101", Country = "USA", Phone1Label = "Home", Phone1 = "555-0101", Phone2Label = "Work", Phone2 = "555-0200", Phone3Label = "Mobile", Phone3 = "555-0301", Profession = "Software Engineer", Birthday = new DateTime(1985, 3, 15), Notes = "Met at WWDC 2019", CustomFieldLabel = "Twitter", CustomFieldValue = "@janesmith" },
                new PersonRecord { FirstName = "Bob", LastName = "Johnson", Title = "CTO", Company = "Tech Solutions", AddressLine1 = "456 Oak Ave", City = "Portland", State = "OR", PostalCode = "97201", Phone1 = "555-0102", Phone2Label = "Work", Phone2 = "555-0202", Profession = "Technology", Notes = "Old college roommate" },
                new PersonRecord { FirstName = "Alice", LastName = "Williams", Title = "Creative Director", Company = "Design Studio", AddressLine1 = "789 Market St", City = "San Francisco", State = "CA", PostalCode = "94103", Phone1 = "555-0103", Phone2 = "555-0104", Profession = "Graphic Design", Birthday = new DateTime(1990, 7, 22) },
                new PersonRecord { FirstName = "David", LastName = "Brown", Title = "Senior Partner", Company = "Brown & Associates", AddressLine1 = "321 Wilshire Blvd", City = "Los Angeles", State = "CA", PostalCode = "90010", Phone1 = "555-0105", Phone2Label = "Office", Phone2 = "555-0205", Profession = "Attorney", Notes = "Law firm partner" },
                new PersonRecord { FirstName = "Sarah", LastName = "Davis", Title = "Physician", Company = "Medical Center", AddressLine1 = "100 Health Way", City = "Denver", State = "CO", PostalCode = "80202", Phone1 = "555-0106", Profession = "Medicine", Birthday = new DateTime(1978, 11, 3) },
                new PersonRecord { FirstName = "Michael", LastName = "Wilson", AddressLine1 = "55 Elm Street", City = "Austin", State = "TX", PostalCode = "73301", Phone1 = "555-0107", Phone2 = "555-0108", Notes = "College friend" },
                new PersonRecord { FirstName = "Emily", LastName = "Taylor", Title = "Marketing Manager", Company = "Marketing Plus", AddressLine1 = "200 Michigan Ave", City = "Chicago", State = "IL", PostalCode = "60601", Phone1 = "555-0109", Profession = "Marketing" },
                new PersonRecord { FirstName = "James", LastName = "Anderson", Title = "Principal Consultant", Company = "Anderson Consulting", AddressLine1 = "75 State Street", City = "Boston", State = "MA", PostalCode = "02109", Phone1 = "555-0110", Phone3Label = "Fax", Phone3 = "555-0111", Profession = "Consulting" },
                new PersonRecord { FirstName = "Lisa", LastName = "Martinez", AddressLine1 = "400 Brickell Ave", City = "Miami", State = "FL", PostalCode = "33131", Phone1 = "555-0112", Notes = "Neighbor", Birthday = new DateTime(1992, 5, 10) },
                new PersonRecord { FirstName = "Robert", LastName = "Garcia", Title = "CEO", Company = "Garcia Industries", AddressLine1 = "1 Industrial Pkwy", City = "Phoenix", State = "AZ", PostalCode = "85001", Phone1 = "555-0113", Phone2Label = "Work", Phone2 = "555-0213", Profession = "Manufacturing" }
            };

            foreach (var contact in sampleContacts)
            {
                await _database.InsertAsync(contact);
            }

            // Seed group memberships
            var allGroups = await _database.Table<ContactGroup>().ToListAsync();
            var familyGroup = allGroups.FirstOrDefault(g => g.Name == "Family");
            var friendsGroup = allGroups.FirstOrDefault(g => g.Name == "Friends");
            var workGroup = allGroups.FirstOrDefault(g => g.Name == "Work");

            var allContacts = await _database.Table<PersonRecord>().ToListAsync();
            
            if (familyGroup != null)
            {
                // Jane Smith, Lisa Martinez, Michael Wilson → Family
                foreach (var name in new[] { "Smith", "Martinez", "Wilson" })
                {
                    var c = allContacts.FirstOrDefault(p => p.LastName == name);
                    if (c != null) await _database.InsertAsync(new ContactGroupMembership { PersonId = c.Id, GroupId = familyGroup.Id });
                }
            }
            if (friendsGroup != null)
            {
                // Bob Johnson, Alice Williams, Michael Wilson → Friends
                foreach (var name in new[] { "Johnson", "Williams", "Wilson" })
                {
                    var c = allContacts.FirstOrDefault(p => p.LastName == name);
                    if (c != null) await _database.InsertAsync(new ContactGroupMembership { PersonId = c.Id, GroupId = friendsGroup.Id });
                }
            }
            if (workGroup != null)
            {
                // Jane Smith, David Brown, Emily Taylor, James Anderson, Robert Garcia → Work
                foreach (var name in new[] { "Smith", "Brown", "Taylor", "Anderson", "Garcia" })
                {
                    var c = allContacts.FirstOrDefault(p => p.LastName == name);
                    if (c != null) await _database.InsertAsync(new ContactGroupMembership { PersonId = c.Id, GroupId = workGroup.Id });
                }
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