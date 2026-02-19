using AddressBookPlus.Models;
using AddressBookPlus.Services;

namespace AddressBookPlus.Views.Controls;

public partial class MiniLookupOverlay : ContentView
{
    private PersonRecord? _selectedContact;
    
    public ISearchService? SearchService { get; set; }

    public MiniLookupOverlay()
    {
        InitializeComponent();
        IsVisible = false;
    }

    public void Show()
    {
        IsVisible = true;
        SearchEntry.Focus();
    }

    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (SearchService == null) return;
        
        var searchText = e.NewTextValue?.Trim();
        if (string.IsNullOrEmpty(searchText) || searchText.Length < 2)
        {
            ResultsView.ItemsSource = null;
            CopyButton.IsEnabled = false;
            return;
        }

        try
        {
            var results = await SearchService.SearchAsync(searchText);
            ResultsView.ItemsSource = results;
        }
        catch
        {
            // Handle search errors gracefully
            ResultsView.ItemsSource = null;
        }
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _selectedContact = e.CurrentSelection.FirstOrDefault() as PersonRecord;
        CopyButton.IsEnabled = _selectedContact != null && !string.IsNullOrEmpty(_selectedContact.Phone1);
    }

    private async void OnCopyClicked(object sender, EventArgs e)
    {
        if (_selectedContact?.Phone1 != null)
        {
            await Microsoft.Maui.ApplicationModel.DataTransfer.Clipboard.SetTextAsync(_selectedContact.Phone1);
            
            // Visual feedback - temporarily change button text
            var originalText = CopyButton.Text;
            CopyButton.Text = "Copied!";
            await Task.Delay(1000);
            CopyButton.Text = originalText;
        }
    }

    private void OnCloseClicked(object sender, EventArgs e)
    {
        IsVisible = false;
        SearchEntry.Text = string.Empty;
        ResultsView.ItemsSource = null;
        _selectedContact = null;
        CopyButton.IsEnabled = false;
    }
}