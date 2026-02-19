using AddressBookPlus.Models;
using AddressBookPlus.ViewModels;

namespace AddressBookPlus.Views;

public partial class SearchPage : ContentPage
{
    public SearchPage(SearchViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnResultSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0) return;

        var collectionView = (CollectionView)sender;
        var contact = e.CurrentSelection.FirstOrDefault() as PersonRecord;
        collectionView.SelectedItem = null;

        if (contact != null)
        {
            // Must use absolute route — "contacts/detail" is registered under //contacts
            await Shell.Current.GoToAsync($"//contacts/detail?id={contact.Id}");
        }
    }
}