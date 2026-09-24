using SQLite;

namespace AddressBookPlus.Models;

[Table("People")]
public class PersonRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Salutation { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;

    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public string Phone1Label { get; set; } = "Home";
    public string Phone1 { get; set; } = string.Empty;
    public string Phone2Label { get; set; } = "Work";
    public string Phone2 { get; set; } = string.Empty;
    public string Phone3Label { get; set; } = "Fax";
    public string Phone3 { get; set; } = string.Empty;

    public string Profession { get; set; } = string.Empty;
    public DateTime? Birthday { get; set; }
    public string Notes { get; set; } = string.Empty;

    public string CustomFieldLabel { get; set; } = "Other";
    public string CustomFieldValue { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    [Ignore] public string FullName => $"{FirstName} {LastName}".Trim();
    [Ignore] public string SortName => string.IsNullOrWhiteSpace(LastName) ? FirstName : $"{LastName}, {FirstName}".Trim(',', ' ');
    [Ignore] public string Initials =>
        $"{(FirstName.Length > 0 ? FirstName[0] : ' ')}{(LastName.Length > 0 ? LastName[0] : ' ')}".Trim();
}