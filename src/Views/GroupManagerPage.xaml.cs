using AddressBookPlus.ViewModels;

namespace AddressBookPlus.Views;

public partial class GroupManagerPage : ContentPage
{
    private readonly GroupManagerViewModel _viewModel;

    public GroupManagerPage(GroupManagerViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadGroupsCommand.Execute(null);
    }
}