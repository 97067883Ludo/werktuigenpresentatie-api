using api.Data.ScreenServices.Interfaces;

namespace api.Data.AutoloadSingleton;

public class ConfigureService
{
    public static void Configure(IApplicationBuilder app)
    {
        app.ApplicationServices.GetService<IOnlineService>();
    }
}