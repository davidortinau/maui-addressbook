using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using AddressBookPlus.Models;
using AddressBookPlus.Services;

namespace AddressBookPlus.ViewModels;

public partial class PrintViewModel : BaseViewModel
{
    private readonly IDataService _dataService;
    private readonly IPrintService _printService;

    [ObservableProperty] private ObservableCollection<string> _templates = new();
    [ObservableProperty] private string? _selectedTemplate;
    [ObservableProperty] private string _previewText = string.Empty;
    [ObservableProperty] private bool _hasPreview;
    [ObservableProperty] private ObservableCollection<PersonRecord> _contacts = new();

    public PrintViewModel(IDataService dataService, IPrintService printService)
    {
        _dataService = dataService;
        _printService = printService;
        Title = "Print";
    }

    [RelayCommand]
    async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Templates = new ObservableCollection<string>(_printService.GetTemplateNames());
            var all = await _dataService.GetAllContactsAsync();
            Contacts = new ObservableCollection<PersonRecord>(all);
            if (Templates.Count > 0) SelectedTemplate = Templates[0];
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task PreviewAsync()
    {
        if (SelectedTemplate is null || IsBusy) return;
        IsBusy = true;

        try
        {
            var html = await _printService.GeneratePrintDocumentAsync(SelectedTemplate, Contacts);
            
            // Create a plain text preview summary
            var contactCount = Contacts.Count;
            var templateType = SelectedTemplate;
            var dateGenerated = DateTime.Now.ToString("MMMM d, yyyy 'at' h:mm tt");
            
            PreviewText = $"Print Document Preview\n" +
                         $"========================\n" +
                         $"Template: {templateType}\n" +
                         $"Contacts: {contactCount} records\n" +
                         $"Generated: {dateGenerated}\n\n" +
                         $"Sample contacts to be printed:\n" +
                         $"------------------------------\n";

            var sampleContacts = Contacts.Take(5).ToList();
            foreach (var contact in sampleContacts)
            {
                PreviewText += $"• {contact.FullName}";
                if (!string.IsNullOrWhiteSpace(contact.Phone1))
                    PreviewText += $" - {contact.Phone1}";
                PreviewText += "\n";
            }

            if (contactCount > 5)
            {
                PreviewText += $"... and {contactCount - 5} more contacts\n";
            }

            PreviewText += "\n" +
                          $"Total document size: ~{html.Length:N0} characters\n" +
                          $"Ready for printing to system printer.";
            
            HasPreview = true;
        }
        catch (Exception ex)
        {
            PreviewText = $"Error generating preview: {ex.Message}";
            HasPreview = true;
        }
        finally
        {
            IsBusy = false;
        }
    }
}