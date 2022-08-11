using Microsoft.AspNetCore.Components.WebView.Maui;
using Kanakku.UI.Data;
using MediatR;
using System.Reflection;
using Kanakku.Application.Requests;

namespace Kanakku.UI;

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
			});

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();

#endif
        builder.Services.AddMediatR(typeof(Kanakku.Application.ServiceRegistry).Assembly);
        builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
