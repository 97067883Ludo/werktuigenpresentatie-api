using api.Data.ScreenServices.Interfaces;
using api.Data.Strategies.FilterControllerStrategy;

namespace api.Data.Startup;

public class ConfigureService
{
    public static void Configure(IApplicationBuilder app)
    {
        app.ApplicationServices.GetService<IOnlineService>();
        app.ApplicationServices.GetService<IFilterController>();
    }
}