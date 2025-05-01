using Microsoft.Extensions.Logging;
using PasswordManager.API.ViewModels;
using CommunityToolkit.Maui;
using UraniumUI;
using Plugin.Maui.Biometric;

namespace PasswordManager.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFontAwesomeIconFonts();
                fonts.AddMaterialSymbolsFonts();
            });
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<PasswordViewModel>();
        builder.Services.AddSingleton<IBiometric>(BiometricAuthenticationService.Default);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
