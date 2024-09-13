using System.Text.Json;
using api.Data;
using api.Data.Models;
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
    
    private Dictionary<string, int> _clients = new Dictionary<string, int>();

    public override Task OnConnectedAsync()
    {
        _clients.Add(Context.ConnectionId, 0);
        return Task.CompletedTask;
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine(Context.ConnectionId);
        var client = _clients.FirstOrDefault(x => x.Key == Context.ConnectionId);
        
        
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
        _clients[Context.ConnectionId] = screenId;
        Screen? screen = _db.Screens.Find(screenId);
        
        if (screen == null)
        {
            await Clients.Client(Context.ConnectionId).CheckIn("{\"screenId\":\"" + screenId + "\", \"status\":\"NotFound\"}");
            return;
        }
        
        screen.Online = true;
        screen.LastSeenOnline = DateTime.Now;
        await _db.SaveChangesAsync();
        
        await Clients.Client(Context.ConnectionId).CheckIn("{\"screenId\":\"" + screenId + "\", \"status\":\"Ok\"}");
    }
}

public interface IScreenHub {
    Task RecieveMessage(string message);

    Task GetPostsFromScreenId(string message);

    Task CheckIn(string response);
}