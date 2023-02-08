using Logic;
using Microsoft.AspNetCore.Mvc;

namespace CuarzoTest_Paz.Controllers;

public class ProductosController : Controller
{
    private readonly CoreLogic _logic;

    public ProductosController(CoreLogic logic)
    {
        _logic = logic;
    }

    public async Task<IActionResult> TablaProductos()
    {
        var productos = await _logic.GetAllProductsByCategoria();
        return View(productos);
    }
}