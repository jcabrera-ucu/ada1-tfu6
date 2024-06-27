using Microsoft.AspNetCore.Mvc;

namespace Monolítico.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public ContentResult Home()
    {
        string text;
        using (StreamReader reader = new StreamReader("../client/index.html"))
        {
            text = reader.ReadToEnd();
            Console.WriteLine(text);
        }

        return new ContentResult
        {
            ContentType = "text/html",
            Content = text,
        };
    }
}
