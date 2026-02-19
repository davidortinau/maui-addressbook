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
        
        // Wire up the search service to the overlay
        MiniLookup.SearchService = _searchService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataCommand.ExecuteAsync(null);
    }

    private async void OnContactSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.PersonRecord contact)
        {
            await _viewModel.GoToDetailCommand.ExecuteAsync(contact);
            // Clear selection so user can tap same item again
            ((CollectionView)sender).SelectedItem = null;
        }
    }
    
    private void OnQuickLookupClicked(object sender, EventArgs e)
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