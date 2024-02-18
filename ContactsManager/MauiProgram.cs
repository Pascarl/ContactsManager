using ContactsManager.Data;
using Microsoft.Extensions.Logging;

namespace ContactsManager
{
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
            builder.Services.AddTransient<EditContact>();
            builder.Services.AddTransient<AddContact>();
            builder.Services.AddTransient<ViewContact>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddSingleton<ContactsDbService>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
