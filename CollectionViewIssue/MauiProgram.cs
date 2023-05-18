using Bogus;
using Microsoft.Extensions.Logging;

namespace CollectionViewIssue;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		Randomizer.Seed = new Random(123456);

        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
