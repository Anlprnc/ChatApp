using ChatApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HubController : Controller
{
    private readonly ISignalRConnection _signalRConnection;

    public HubController(ISignalRConnection signalRConnection)
    {
        _signalRConnection = signalRConnection;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string message)
    {
        var connect = _signalRConnection.StartConnection();
        await connect.InvokeAsync("SendMessageToAll", "Admin",message);
        return Ok();
    }
}