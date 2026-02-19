using AddressBookPlus.ViewModels;

namespace AddressBookPlus.Views;

public partial class ContactDetailPage : ContentPage
{
    public ContactDetailPage(ContactDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}