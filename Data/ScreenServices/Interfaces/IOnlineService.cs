namespace api.Data.ScreenServices.Interfaces;

public interface IOnlineService
{
    public Task StartLoop();
    
    public void StopLoop();
}