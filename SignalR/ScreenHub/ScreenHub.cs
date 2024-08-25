using System.Text.Json;
using api.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace api.SignalR.ScreenHub;

[DisableCors]
public class ScreenHub : Hub<IScreenHub>
{

    private readonly AppDbContext _db;
    
    public ScreenHub(AppDbContext db)
    {
        _db = db;
    }
    
    private Dictionary<string, int> clients = new Dictionary<string, int>();

    public override async Task OnConnectedAsync() {
        clients.Add(Context.ConnectionId, 0);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine(Context.ConnectionId);

        return Task.CompletedTask;
    }

    public async Task SendMessage(string message) {
        await Clients.All.RecieveMessage($"{Context.ConnectionId}: {message}");
    }

    public async Task GetPostsFromScreenId()
    {
        //id of the screen is gotton from the clients dictonary
        //await Clients.Client(Context.ConnectionId).RecieveMessage();
    }

    public async Task GetAllPosts()
    {
        await Clients.Client(Context.ConnectionId).GetPostsFromScreenId(JsonSerializer.Serialize(_db.Posts.Include(x => x.Image).ToList()));
    }

    public async Task CheckIn(int screenId)
    {
        var test = clients.Where(x => x.Key == Context.ConnectionId).FirstOrDefault();
        Clients.Client(Context.ConnectionId).checkIn("{\"screenId\":\"" + screenId + "\", \"status\":\"Ok\"}");
    }
}

public interface IScreenHub {
    Task RecieveMessage(string message);

    Task GetPostsFromScreenId(string message);

    void checkIn(string response);
}