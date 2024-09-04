using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Helpers;

public class SignalrConnection : ISignalRConnection
{
    HubConnection connection;

    public HubConnection StartConnection()
    {
        var hostInfo = "https://localhost:7153";
        if (connection != null && connection.State == HubConnectionState.Connected)
        {
            return connection;
        }

        connection = new HubConnectionBuilder()
            .WithUrl($"{hostInfo}/chatub")
            .WithKeepAliveInterval(TimeSpan.FromDays(1))
            .WithServerTimeout(TimeSpan.FromDays(1))
            .WithAutomaticReconnect()
            .Build();

        if (connection.State == HubConnectionState.Disconnected)
        {
            connection.StartAsync();
        }

        return connection;
    }

    public bool IsConnected()
    {
        return connection != null && connection.State == HubConnectionState.Connected;
    }
}