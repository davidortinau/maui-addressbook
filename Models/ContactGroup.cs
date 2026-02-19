using SQLite;

namespace AddressBookPlus.Models;

[Table("Groups")]
public class ContactGroup
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int SlotNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ColorKey { get; set; } = "Primary";
}