using AddressBookPlus.Models;
using System.Text;

namespace AddressBookPlus.Services;

public class PrintService : IPrintService
{
    private readonly Dictionary<string, string> _templateFiles = new()
    {
        { "Binder Full Page", "binder_full.html" },
        { "Phone List", "phone_list.html" }
    };

    public List<string> GetTemplateNames()
    {
        return _templateFiles.Keys.ToList();
    }

    public async Task<string> GeneratePrintDocumentAsync(string templateName, IEnumerable<PersonRecord> contacts)
    {
        if (!_templateFiles.TryGetValue(templateName, out var templateFile))
            throw new ArgumentException($"Template '{templateName}' not found.");

        var template = await LoadTemplateAsync(templateFile);
        var contactsHtml = GenerateContactsHtml(templateName, contacts);
        
        return template
            .Replace("{{CONTACTS}}", contactsHtml)
            .Replace("{{DATE}}", DateTime.Now.ToString("MMMM d, yyyy 'at' h:mm tt"));
    }

    private async Task<string> LoadTemplateAsync(string fileName)
    {
        // Templates ship as MauiAsset items (Resources/Raw), not embedded resources
        using var stream = await FileSystem.OpenAppPackageFileAsync($"print_templates/{fileName}");
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }

    private string GenerateContactsHtml(string templateName, IEnumerable<PersonRecord> contacts)
    {
        return templateName switch
        {
            "Binder Full Page" => GenerateBinderFullHtml(contacts),
            "Phone List" => GeneratePhoneListHtml(contacts),
            _ => string.Empty
        };
    }

    private string GenerateBinderFullHtml(IEnumerable<PersonRecord> contacts)
    {
        var html = new StringBuilder();
        var sortedContacts = contacts.OrderBy(c => c.SortName);

        foreach (var contact in sortedContacts)
        {
            html.AppendLine("<div class=\"contact-card\">");
            html.AppendLine($"    <div class=\"contact-name\">{EscapeHtml(contact.FullName)}</div>");
            html.AppendLine("    <div class=\"contact-info\">");

            // Company and Title
            if (!string.IsNullOrWhiteSpace(contact.Company) || !string.IsNullOrWhiteSpace(contact.Title))
            {
                html.AppendLine("        <div class=\"info-group\">");
                if (!string.IsNullOrWhiteSpace(contact.Company))
                    html.AppendLine($"            <span class=\"info-label\">Company:</span><span class=\"info-value\">{EscapeHtml(contact.Company)}</span><br/>");
                if (!string.IsNullOrWhiteSpace(contact.Title))
                    html.AppendLine($"            <span class=\"info-label\">Title:</span><span class=\"info-value\">{EscapeHtml(contact.Title)}</span>");
                html.AppendLine("        </div>");
            }

            // Phone Numbers
            html.AppendLine("        <div class=\"info-group\">");
            if (!string.IsNullOrWhiteSpace(contact.Phone1))
                html.AppendLine($"            <span class=\"info-label\">{EscapeHtml(contact.Phone1Label)}:</span><span class=\"info-value\">{EscapeHtml(contact.Phone1)}</span><br/>");
            if (!string.IsNullOrWhiteSpace(contact.Phone2))
                html.AppendLine($"            <span class=\"info-label\">{EscapeHtml(contact.Phone2Label)}:</span><span class=\"info-value\">{EscapeHtml(contact.Phone2)}</span><br/>");
            if (!string.IsNullOrWhiteSpace(contact.Phone3))
                html.AppendLine($"            <span class=\"info-label\">{EscapeHtml(contact.Phone3Label)}:</span><span class=\"info-value\">{EscapeHtml(contact.Phone3)}</span>");
            html.AppendLine("        </div>");

            // Address
            var hasAddress = !string.IsNullOrWhiteSpace(contact.AddressLine1) || 
                           !string.IsNullOrWhiteSpace(contact.City) || 
                           !string.IsNullOrWhiteSpace(contact.State);
            
            if (hasAddress)
            {
                html.AppendLine("        <div class=\"address-block\">");
                html.AppendLine("            <span class=\"info-label\">Address:</span><br/>");
                if (!string.IsNullOrWhiteSpace(contact.AddressLine1))
                    html.AppendLine($"            {EscapeHtml(contact.AddressLine1)}<br/>");
                if (!string.IsNullOrWhiteSpace(contact.AddressLine2))
                    html.AppendLine($"            {EscapeHtml(contact.AddressLine2)}<br/>");
                
                var cityStateZip = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(contact.City))
                    cityStateZip.Append(contact.City);
                if (!string.IsNullOrWhiteSpace(contact.State))
                {
                    if (cityStateZip.Length > 0) cityStateZip.Append(", ");
                    cityStateZip.Append(contact.State);
                }
                if (!string.IsNullOrWhiteSpace(contact.PostalCode))
                {
                    if (cityStateZip.Length > 0) cityStateZip.Append(" ");
                    cityStateZip.Append(contact.PostalCode);
                }
                
                if (cityStateZip.Length > 0)
                    html.AppendLine($"            {EscapeHtml(cityStateZip.ToString())}<br/>");
                
                if (!string.IsNullOrWhiteSpace(contact.Country))
                    html.AppendLine($"            {EscapeHtml(contact.Country)}");
                
                html.AppendLine("        </div>");
            }

            html.AppendLine("    </div>");

            // Notes
            if (!string.IsNullOrWhiteSpace(contact.Notes))
            {
                html.AppendLine("    <div class=\"notes-block\">");
                html.AppendLine($"        <span class=\"info-label\">Notes:</span> {EscapeHtml(contact.Notes)}");
                html.AppendLine("    </div>");
            }

            html.AppendLine("</div>");
        }

        return html.ToString();
    }

    private string GeneratePhoneListHtml(IEnumerable<PersonRecord> contacts)
    {
        var html = new StringBuilder();
        var sortedContacts = contacts.OrderBy(c => c.SortName);

        foreach (var contact in sortedContacts)
        {
            html.AppendLine("            <tr>");
            html.AppendLine($"                <td class=\"name-cell\">{EscapeHtml(contact.FullName)}</td>");
            
            var phone1Display = !string.IsNullOrWhiteSpace(contact.Phone1) 
                ? $"{EscapeHtml(contact.Phone1)} ({EscapeHtml(contact.Phone1Label)})" 
                : "";
            var phone2Display = !string.IsNullOrWhiteSpace(contact.Phone2) 
                ? $"{EscapeHtml(contact.Phone2)} ({EscapeHtml(contact.Phone2Label)})" 
                : "";
            var phone3Display = !string.IsNullOrWhiteSpace(contact.Phone3) 
                ? $"{EscapeHtml(contact.Phone3)} ({EscapeHtml(contact.Phone3Label)})" 
                : "";

            html.AppendLine($"                <td class=\"phone-cell\">{(string.IsNullOrEmpty(phone1Display) ? "<span class=\"empty-cell\">—</span>" : phone1Display)}</td>");
            html.AppendLine($"                <td class=\"phone-cell\">{(string.IsNullOrEmpty(phone2Display) ? "<span class=\"empty-cell\">—</span>" : phone2Display)}</td>");
            html.AppendLine($"                <td class=\"phone-cell\">{(string.IsNullOrEmpty(phone3Display) ? "<span class=\"empty-cell\">—</span>" : phone3Display)}</td>");
            html.AppendLine("            </tr>");
        }

        return html.ToString();
    }

    private static string EscapeHtml(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        return text
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&#39;");
    }
}