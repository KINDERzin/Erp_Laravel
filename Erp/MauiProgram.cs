using Erp.Services;
using Microsoft.Extensions.Logging;

namespace Erp;

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
			});

		builder.Services.AddHttpClient<LaravelApiService>(client =>
		{
			client.BaseAddress = new Uri("http://127.0.0.1:8000/api/");

			// Case esteja hospado na net
			// client.BaseAddress = new Uri("http://meu-erp/api/");

			client.Timeout = TimeSpan.FromSeconds(30);
		});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
