using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AddressBookPlus.Models;
using AddressBookPlus.Services;

namespace AddressBookPlus.ViewModels;

public partial class SearchViewModel : BaseViewModel
{
    private readonly ISearchService _searchService;

    public SearchViewModel(ISearchService searchService)
    {
        _searchService = searchService;
        Results = new ObservableCollection<PersonRecord>();
        Title = "Search Contacts";
    }

    [ObservableProperty]
    private string _searchQuery = string.Empty;

    [ObservableProperty]
    private ObservableCollection<PersonRecord> _results;

    [RelayCommand]
    private async Task SearchAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            Results.Clear();
            return;
        }

        IsBusy = true;
        try
        {
            var searchResults = await _searchService.SearchAsync(SearchQuery);
            Results.Clear();
            foreach (var contact in searchResults)
            {
                Results.Add(contact);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void Clear()
    {
        SearchQuery = string.Empty;
        Results.Clear();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(PersonRecord? contact)
    {
        if (contact == null) return;
        await Shell.Current.GoToAsync($"contacts/detail?id={contact.Id}");
    }
}