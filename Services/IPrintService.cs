using AddressBookPlus.Models;

namespace AddressBookPlus.Services;

public interface IPrintService
{
    List<string> GetTemplateNames();
    Task<string> GeneratePrintDocumentAsync(string templateName, IEnumerable<PersonRecord> contacts);
}