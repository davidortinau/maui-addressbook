using AddressBookPlus.Models;
using AddressBookPlus.ViewModels;
using AddressBookPlus.Services;

namespace AddressBookPlus.Views;

public partial class ContactListPage : ContentPage
{
    private readonly ContactListViewModel _viewModel;
    private readonly ISearchService _searchService;

    public ContactListPage(ContactListViewModel viewModel, ISearchService searchService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _searchService = searchService;
        BindingContext = _viewModel;
        
        MiniLookup.SearchService = _searchService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataCommand.ExecuteAsync(null);
    }

    private async void OnContactSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0 || sender is not CollectionView collectionView) return;
        
        var contact = e.CurrentSelection.FirstOrDefault() as PersonRecord;
        
        collectionView.SelectedItem = null;
        
        if (contact != null)
        {
            await Shell.Current.GoToAsync($"contacts/detail?id={contact.Id}");
        }
    }
    
    private void OnQuickLookupClicked(object? sender, EventArgs e)
    {
        if (MiniLookup.IsVisible)
        {
            MiniLookup.IsVisible = false;
        }
        else
        {
            MiniLookup.Show();
        }
    }
}