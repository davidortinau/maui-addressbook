using MauiBootstrapTheme.Theming;

namespace AddressBookPlus;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		// Add the generated Brite theme dictionary, then apply it
		Resources.MergedDictionaries.Add(new Themes.BriteTheme());
		BootstrapTheme.Apply("brite");
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}