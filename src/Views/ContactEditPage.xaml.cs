using AddressBookPlus.ViewModels;

namespace AddressBookPlus.Views;

public partial class ContactEditPage : ContentPage
{
    public ContactEditPage(ContactEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}