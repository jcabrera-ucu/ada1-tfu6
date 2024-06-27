using Microsoft.AspNetCore.Mvc;

namespace Bebidas.Controllers;

public class Bebida
{
    public int Id { get; set; }

    public string Nombre { get; set; }
}

public class Compra
{
    public Bebida Bebida { get; set; }

    public int Cantidad { get; set; }
}

[ApiController]
[Route("[controller]")]
public class BebidasController : ControllerBase
{
    private static readonly List<Bebida> bebidas =
    [
        new Bebida{
            Id = 1,
            Nombre = "Café",
        },
        new Bebida{
            Id = 2,
            Nombre = "Té",
        },
        new Bebida{
            Id = 3,
            Nombre = "Agua",
        },
    ];

    public static List<Compra> Compras { get; private set; } = [];

    private readonly ILogger<BebidasController> _logger;

    public BebidasController(ILogger<BebidasController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IEnumerable<Bebida> Get()
    {
        return bebidas;
    }

    [HttpPost()]
    public ActionResult<IEnumerable<Compra>> Buy(int id)
    {
        var bebida = bebidas.Find(x => x.Id == id);

        if (bebida == null)
        {
            return NotFound();
        }

        var compra = Compras.Find(x => x.Bebida.Id == id);
        if (compra == null) 
        {
            Compras.Add(new Compra
            {
                Bebida = bebida, 
                Cantidad = 1,
            });
        }
        else
        {
            compra.Cantidad++;
        }


        return Compras;
    }
}
