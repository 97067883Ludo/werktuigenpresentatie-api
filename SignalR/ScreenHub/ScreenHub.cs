using Microsoft.AspNetCore.SignalR;

namespace api.SignalR.ScreenHub;

public class ScreenHub : Hub<IScreenHub> {
    public override async Task OnConnectedAsync() {
        await Clients.All.RecieveMessage($"New Member ({Context.ConnectionId}): Joined");
        Console.WriteLine("hallo");
    }

    public async Task SendMessage(string message) {
        await Clients.All.RecieveMessage($"{Context.ConnectionId}: {message}");
    }
}

public interface IScreenHub {
    Task RecieveMessage(string message);
}