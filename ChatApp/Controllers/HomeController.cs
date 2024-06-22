using Microsoft.AspNetCore.Mvc;

namespace ChatApp;

[ApiController]
[Route("api/[controller]")]
public class HomeController : Controller
{
    [HttpGet]
    public string Get()
    {
        return "Hello World";
    }
}
