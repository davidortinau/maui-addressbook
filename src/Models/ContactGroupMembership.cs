using SQLite;

namespace AddressBookPlus.Models;

[Table("GroupMemberships")]
public class ContactGroupMembership
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int PersonId { get; set; }

    [Indexed]
    public int GroupId { get; set; }
}