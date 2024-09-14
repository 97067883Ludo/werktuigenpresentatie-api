using api.Data.ScreenServices.Interfaces;

namespace api.Data.Startup;

public class ConfigureService
{
    public static void Configure(IApplicationBuilder app)
    {
        app.ApplicationServices.GetService<IOnlineService>();
    }
}