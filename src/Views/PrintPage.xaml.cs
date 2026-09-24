using AddressBookPlus.ViewModels;

namespace AddressBookPlus.Views;

public partial class PrintPage : ContentPage
{
    private readonly PrintViewModel _viewModel;

    public PrintPage(PrintViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCommand.ExecuteAsync(null);
    }
}