using System.Timers;
using api.Data.Models;
using api.Data.ScreenServices.Interfaces;

namespace api.Data.ScreenServices;

public class OnlineService : IOnlineService
{
    private System.Timers.Timer _timer { get; set; }
    private readonly AppDbContext _db;

    public OnlineService(AppDbContext appDbContext)
    {
        _db = appDbContext;
        StartLoop();
    }

    public void StartLoop()
    {
        _timer = new System.Timers.Timer(2000);
        _timer.AutoReset = true;
        _timer.Elapsed += Loop;
        _timer.Start();
    }

    public void StopLoop()
    {
        _timer.Stop();
    }

    
    private void Loop(object? hallo, ElapsedEventArgs args)
    {
        List<Screen> screens = _db.Screens.ToList();
        screens.ForEach(CheckScreenOnlineStatus);
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