using SQLite;

namespace AddressBookPlus.Models;

[Table("DocumentSettings")]
public class AddressBookDocument
{
    [PrimaryKey]
    public int Id { get; set; } = 1;

    public string DocumentName { get; set; } = "My Address Book";
    public string DefaultSortField { get; set; } = "LastName";
    public int LastSelectedGroupSlot { get; set; }
}