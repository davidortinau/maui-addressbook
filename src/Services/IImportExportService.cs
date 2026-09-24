using AddressBookPlus.Models;

namespace AddressBookPlus.Services;

public interface IImportExportService
{
    Task<List<PersonRecord>> ImportVCardAsync(Stream stream);
    Task<string> ExportVCardAsync(IEnumerable<PersonRecord> contacts);
    Task<List<PersonRecord>> ImportCsvAsync(Stream stream);
    Task<string> ExportCsvAsync(IEnumerable<PersonRecord> contacts);
}