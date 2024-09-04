using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Helpers;

public interface ISignalRConnection
{
    HubConnection StartConnection();
    bool IsConnected();
}