using AddressBookPlus.Models;
using System.Globalization;
using System.Text;

namespace AddressBookPlus.Services;

public class ImportExportService : IImportExportService
{
    public async Task<List<PersonRecord>> ImportVCardAsync(Stream stream)
    {
        var contacts = new List<PersonRecord>();
        
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();
        
        var vcardBlocks = content.Split(new[] { "BEGIN:VCARD" }, StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var block in vcardBlocks)
        {
            if (!block.Contains("END:VCARD")) continue;
            
            var contact = ParseVCardBlock("BEGIN:VCARD" + block);
            if (contact != null)
            {
                contacts.Add(contact);
            }
        }
        
        return contacts;
    }

    private PersonRecord? ParseVCardBlock(string vcardBlock)
    {
        var lines = vcardBlock.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var contact = new PersonRecord();
        
        foreach (var line in lines)
        {
            var cleanLine = line.Trim();
            
            if (cleanLine.StartsWith("N:"))
            {
                var nameParts = cleanLine.Substring(2).Split(';');
                if (nameParts.Length > 0) contact.LastName = nameParts[0];
                if (nameParts.Length > 1) contact.FirstName = nameParts[1];
            }
            else if (cleanLine.StartsWith("FN:"))
            {
                var fullName = cleanLine.Substring(3);
                if (string.IsNullOrEmpty(contact.FirstName) && string.IsNullOrEmpty(contact.LastName))
                {
                    var parts = fullName.Split(' ', 2);
                    contact.FirstName = parts.Length > 0 ? parts[0] : "";
                    contact.LastName = parts.Length > 1 ? parts[1] : "";
                }
            }
            else if (cleanLine.StartsWith("ORG:"))
            {
                contact.Company = cleanLine.Substring(4);
            }
            else if (cleanLine.StartsWith("TEL"))
            {
                var phoneValue = ExtractVCardValue(cleanLine);
                if (string.IsNullOrEmpty(contact.Phone1))
                    contact.Phone1 = phoneValue;
                else if (string.IsNullOrEmpty(contact.Phone2))
                    contact.Phone2 = phoneValue;
                else if (string.IsNullOrEmpty(contact.Phone3))
                    contact.Phone3 = phoneValue;
            }
            else if (cleanLine.StartsWith("ADR"))
            {
                var addrParts = ExtractVCardValue(cleanLine).Split(';');
                if (addrParts.Length > 3) contact.City = addrParts[3];
            }
            else if (cleanLine.StartsWith("NOTE:"))
            {
                contact.Notes = cleanLine.Substring(5);
            }
            else if (cleanLine.StartsWith("BDAY:"))
            {
                if (DateTime.TryParseExact(cleanLine.Substring(5), "yyyyMMdd", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthday))
                {
                    contact.Birthday = birthday;
                }
            }
        }
        
        if (string.IsNullOrEmpty(contact.FirstName) && string.IsNullOrEmpty(contact.LastName))
            return null;
            
        return contact;
    }

    private string ExtractVCardValue(string line)
    {
        var colonIndex = line.IndexOf(':');
        return colonIndex >= 0 ? line.Substring(colonIndex + 1) : "";
    }

    public async Task<string> ExportVCardAsync(IEnumerable<PersonRecord> contacts)
    {
        var sb = new StringBuilder();
        
        foreach (var contact in contacts)
        {
            sb.AppendLine("BEGIN:VCARD");
            sb.AppendLine("VERSION:3.0");
            
            if (!string.IsNullOrEmpty(contact.FirstName) || !string.IsNullOrEmpty(contact.LastName))
            {
                sb.AppendLine($"N:{contact.LastName ?? ""};{contact.FirstName ?? ""};;;");
                sb.AppendLine($"FN:{contact.FirstName} {contact.LastName}".Trim());
            }
            
            if (!string.IsNullOrEmpty(contact.Company))
                sb.AppendLine($"ORG:{contact.Company}");
            
            if (!string.IsNullOrEmpty(contact.Phone1))
                sb.AppendLine($"TEL;TYPE=VOICE:{contact.Phone1}");
            if (!string.IsNullOrEmpty(contact.Phone2))
                sb.AppendLine($"TEL;TYPE=CELL:{contact.Phone2}");
            if (!string.IsNullOrEmpty(contact.Phone3))
                sb.AppendLine($"TEL;TYPE=WORK:{contact.Phone3}");
            
            if (!string.IsNullOrEmpty(contact.City))
                sb.AppendLine($"ADR;TYPE=HOME:;;;{contact.City};;;");
            
            if (!string.IsNullOrEmpty(contact.Notes))
                sb.AppendLine($"NOTE:{contact.Notes}");
            
            if (contact.Birthday.HasValue)
                sb.AppendLine($"BDAY:{contact.Birthday.Value:yyyyMMdd}");
            
            sb.AppendLine("END:VCARD");
            sb.AppendLine();
        }
        
        return sb.ToString();
    }

    public async Task<List<PersonRecord>> ImportCsvAsync(Stream stream)
    {
        var contacts = new List<PersonRecord>();
        
        using var reader = new StreamReader(stream);
        var headerLine = await reader.ReadLineAsync();
        if (string.IsNullOrEmpty(headerLine)) return contacts;
        
        var headers = ParseCsvLine(headerLine);
        var headerMap = new Dictionary<string, int>();
        
        for (int i = 0; i < headers.Length; i++)
        {
            headerMap[headers[i].ToLowerInvariant()] = i;
        }
        
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            var values = ParseCsvLine(line);
            var contact = new PersonRecord();
            
            if (headerMap.TryGetValue("firstname", out var firstNameIndex) && firstNameIndex < values.Length)
                contact.FirstName = values[firstNameIndex];
            if (headerMap.TryGetValue("lastname", out var lastNameIndex) && lastNameIndex < values.Length)
                contact.LastName = values[lastNameIndex];
            if (headerMap.TryGetValue("company", out var companyIndex) && companyIndex < values.Length)
                contact.Company = values[companyIndex];
            if (headerMap.TryGetValue("phone", out var phoneIndex) && phoneIndex < values.Length)
                contact.Phone1 = values[phoneIndex];
            if (headerMap.TryGetValue("phone1", out var phone1Index) && phone1Index < values.Length)
                contact.Phone1 = values[phone1Index];
            if (headerMap.TryGetValue("phone2", out var phone2Index) && phone2Index < values.Length)
                contact.Phone2 = values[phone2Index];
            if (headerMap.TryGetValue("phone3", out var phone3Index) && phone3Index < values.Length)
                contact.Phone3 = values[phone3Index];
            if (headerMap.TryGetValue("city", out var cityIndex) && cityIndex < values.Length)
                contact.City = values[cityIndex];
            if (headerMap.TryGetValue("notes", out var notesIndex) && notesIndex < values.Length)
                contact.Notes = values[notesIndex];
            
            if (!string.IsNullOrEmpty(contact.FirstName) || !string.IsNullOrEmpty(contact.LastName))
            {
                contacts.Add(contact);
            }
        }
        
        return contacts;
    }

    private string[] ParseCsvLine(string line)
    {
        var values = new List<string>();
        var inQuotes = false;
        var currentValue = new StringBuilder();
        
        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];
            
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    currentValue.Append('"');
                    i++; // Skip next quote
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                values.Add(currentValue.ToString());
                currentValue.Clear();
            }
            else
            {
                currentValue.Append(c);
            }
        }
        
        values.Add(currentValue.ToString());
        return values.ToArray();
    }

    public async Task<string> ExportCsvAsync(IEnumerable<PersonRecord> contacts)
    {
        var sb = new StringBuilder();
        
        // Header row
        sb.AppendLine("FirstName,LastName,Company,Phone1,Phone2,Phone3,City,Notes");
        
        foreach (var contact in contacts)
        {
            sb.AppendLine($"{EscapeCsvValue(contact.FirstName)}," +
                         $"{EscapeCsvValue(contact.LastName)}," +
                         $"{EscapeCsvValue(contact.Company)}," +
                         $"{EscapeCsvValue(contact.Phone1)}," +
                         $"{EscapeCsvValue(contact.Phone2)}," +
                         $"{EscapeCsvValue(contact.Phone3)}," +
                         $"{EscapeCsvValue(contact.City)}," +
                         $"{EscapeCsvValue(contact.Notes)}");
        }
        
        return sb.ToString();
    }

    private string EscapeCsvValue(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return "";
        
        if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }
        
        return value;
    }
}