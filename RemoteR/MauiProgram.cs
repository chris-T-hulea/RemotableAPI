using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Markup;
using RemoteR.Utils;

namespace RemoteR;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiCommunityToolkitCore()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMarkup();

		builder.Services.AddSingleton<Configuration>();
		builder.Services.AddSingleton<ControlService>();
		builder.Services.AddSingleton<MainPageModel>();
		builder.Services.AddSingleton<MainPage>();
		return builder.Build();
	}
}
