using Video.JsInterop;

namespace Video;
public static class MauiProgram
{
    /// <summary>
    /// 服务提供程序
    /// </summary>
    public static IServiceProvider GetServiceProvider { get; private set; }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddScoped<HelperJsInterop>();
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMasaBlazor();
        
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        GetServiceProvider = app.Services;

        return app;
    }
}
