using System.Net;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace CuarzoTest_Paz.Controllers;

public class CategoriasController : Controller
{
    private readonly CoreLogic _logic;

    public CategoriasController(CoreLogic logic)
    {
        _logic = logic;
    }

    public async Task<IActionResult> Categorias()
    {
        var categorias = await _logic.GetAllCategorias();
        return View(categorias);
    }

    [HttpGet]
    [Route("EditCreateCategoria/{id:int}")]
    public async Task<IActionResult> SearchCategoria([FromRoute] int id)
    {
        if (id == 0)
            return View("EditCreateCategoria", new Categoria
            {
                IdCategoria = 0, // if id=0, a new one was created
                EsActiva = false,
                NombreCategoria = null
            });
        try
        {
            var searchedCategoria = await _logic.GetCategoria(id);
            if (searchedCategoria is null)
                throw new HttpRequestException(
                    message: "Categoria not found",
                    inner: null,
                    statusCode: HttpStatusCode.NotFound);

            return View("EditCreateCategoria", new Categoria
            {
                IdCategoria = searchedCategoria.IdCategoria, // if id!=0, it was updated 
                EsActiva = searchedCategoria.EsActiva,
                NombreCategoria = searchedCategoria.NombreCategoria
            });
        }
        catch (HttpRequestException e)
        {
            switch (e.StatusCode)
            {
                case HttpStatusCode.NotFound:
                {
                    return View("EditCreateCategoria", new Categoria
                    {
                        IdCategoria = 0, // if id=0, a new one was created
                        EsActiva = false,
                        NombreCategoria = null
                    });
                }

                case HttpStatusCode.InternalServerError:
                    return BadRequest(e.Message);

                default:
                    return BadRequest(e.Message);
            }
        }
    }


    [HttpPost]
    public async Task<IActionResult> EditCreateCategoria(
        int IdCategoria,
        string NombreCategoria,
        bool EsActiva)
    {
        var categoria = new Categoria
        {
            IdCategoria = IdCategoria,
            NombreCategoria = NombreCategoria,
            EsActiva = EsActiva
        };
        try
        {
            if (categoria.IdCategoria == 0)
                _logic.Usp_Ins_Co_Categoria(categoria: categoria);
            else
                _logic.UpdateCategoria(categoria: categoria);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        var categorias = await _logic.GetAllCategorias();
        return View("Categorias", categorias);
    }


    
    [Route("EliminarCategoria/{id:int}")]
    public async Task<IActionResult> EliminarCategoria([FromRoute] int id)
    {
        try
        {
            await _logic.DeleteCategoria(idCategoria: id);
        }
        catch (HttpRequestException e)
        {
            switch (e.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound(e.Message);
                case HttpStatusCode.InternalServerError:
                    return BadRequest(e.Message);
            }
        }

        var categorias = await _logic.GetAllCategorias();
        return View("Categorias", categorias);
    }
}