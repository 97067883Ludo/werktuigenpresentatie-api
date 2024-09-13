using api.Data.Models;
using api.Data.ScreenServices.Interfaces;

namespace api.Data.ScreenServices;

public class OnlineService : IOnlineService
{
    private CancellationTokenSource Token { get; set; } = new();
    private readonly AppDbContext _db;

    public OnlineService(AppDbContext appDbContext)
    {
        _db = appDbContext;
        //StartLoop();
    }

    public async Task StartLoop()
    {
        if (Token.IsCancellationRequested)
        {
            Token.Dispose();
            Token = new CancellationTokenSource();
        }
        
        await Loop();

        var test = "";
    }

    public void StopLoop()
    {
        Token.Cancel();
    }


    private async Task Loop()
    {
        while (Token.IsCancellationRequested == false)
        {
            List<Screen> screens = _db.Screens.ToList();

            screens.ForEach(CheckScreenOnlineStatus);
            
            Thread.Sleep(1000);
        }
    }

    private void CheckScreenOnlineStatus(Screen screen)
    {
        int minutes = (DateTime.Now - screen.LastSeenOnline).Minutes;
        if (minutes > 1)
        {
            screen.Online = false;
            _db.SaveChanges();
        }
    }
}