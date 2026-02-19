namespace AddressBookPlus;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		// Register routes
		Routing.RegisterRoute("contacts/detail", typeof(Views.ContactDetailPage));
		Routing.RegisterRoute("contacts/edit", typeof(Views.ContactEditPage));
	}
}
