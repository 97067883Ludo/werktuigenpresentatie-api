using api.Data;
using Microsoft.AspNetCore.SignalR;

namespace api.SignalR.ScreenHub;

public class ScreenHub : Hub<IScreenHub>
{

    private readonly AppDbContext _db;
    
    public ScreenHub(AppDbContext db)
    {
        _db = db;
    }

    public override async Task OnConnectedAsync() {
        await Clients.All.RecieveMessage($"New Member ({Context.ConnectionId}): Joined");
        Console.WriteLine("hallo");
    }

    public async Task SendMessage(string message) {
        await Clients.All.RecieveMessage($"{Context.ConnectionId}: {message}");
    }

    public async Task GetPostsFromScreenId(int screenId)
    {
        await Clients.Client(Context.ConnectionId).RecieveMessage($"Hoi {screenId}");
    }

    public async Task CheckIn(int screenId)
    {
        
    }
}

public interface IScreenHub {
    Task RecieveMessage(string message);
}