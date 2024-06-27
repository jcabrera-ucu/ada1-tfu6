using Microsoft.AspNetCore.Mvc;

namespace Comidas.Controllers;

public class Comida
{
    public int Id { get; set; }

    public string Nombre { get; set; }
}

public class Compra
{
    public Comida Comida { get; set; }

    public int Cantidad { get; set; }
}

[ApiController]
[Route("[controller]")]
public class ComidasController : ControllerBase
{
    private static readonly List<Comida> comidas =
    [
        new Comida{
            Id = 1,
            Nombre = "Alto bizcochito",
        },
        new Comida{
            Id = 2,
            Nombre = "Bizcochito berreta",
        },
    ];

    public static List<Compra> Compras { get; private set; } = [];

    private readonly ILogger<ComidasController> _logger;

    public ComidasController(ILogger<ComidasController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IEnumerable<Comida> Get()
    {
        return comidas;
    }

    [HttpPost()]
    public ActionResult<IEnumerable<Compra>> Buy(int id)
    {
        var comida = comidas.Find(x => x.Id == id);

        if (comida == null)
        {
            return NotFound();
        }

        var compra = Compras.Find(x => x.Comida.Id == id);
        if (compra == null) 
        {
            Compras.Add(new Compra
            {
                Comida = comida, 
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
