using MauiBootstrapTheme.Extensions;
using MauiDevFlow.Agent;
using Microsoft.Extensions.Logging;

namespace AddressBookPlus;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseBootstrapTheme()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Services
		builder.Services.AddSingleton<Services.IDataService, Services.DataService>();
		builder.Services.AddSingleton<Services.ISearchService, Services.SearchService>();
		builder.Services.AddTransient<Services.IImportExportService, Services.ImportExportService>();
		builder.Services.AddTransient<Services.IPrintService, Services.PrintService>();

		// ViewModels
		builder.Services.AddTransient<ViewModels.ContactListViewModel>();
		builder.Services.AddTransient<ViewModels.ContactDetailViewModel>();
		builder.Services.AddTransient<ViewModels.ContactEditViewModel>();
		builder.Services.AddTransient<ViewModels.GroupManagerViewModel>();
		builder.Services.AddTransient<ViewModels.SearchViewModel>();
		builder.Services.AddTransient<ViewModels.PrintViewModel>();

		// Pages
		builder.Services.AddTransient<Views.ContactListPage>();
		builder.Services.AddTransient<Views.ContactDetailPage>();
		builder.Services.AddTransient<Views.ContactEditPage>();
		builder.Services.AddTransient<Views.GroupManagerPage>();
		builder.Services.AddTransient<Views.SearchPage>();
		builder.Services.AddTransient<Views.PrintPage>();

#if DEBUG
		builder.Logging.AddDebug();
		builder.AddMauiDevFlowAgent();
#endif

		return builder.Build();
	}
}
