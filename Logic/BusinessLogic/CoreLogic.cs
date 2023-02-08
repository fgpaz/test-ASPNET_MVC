using System.Net;
using DataAccess;
using Models;

namespace Logic;

public class CoreLogic
{
    private readonly CategoriaAccess _categoriaAccess;
    private readonly ProductoAccess _productoAccess;

    public CoreLogic(ProductoAccess productoAccess, CategoriaAccess categoriaAccess)
    {
        _productoAccess = productoAccess;
        _categoriaAccess = categoriaAccess;
    }

    /// <summary> Obtains all 'Productos' associated to an Categoria's ID </summary>
    /// <returns>List(Producto)</returns>
    /// <exception cref="HttpRequestException">
    /// <code>if NotFound:</code>-> StatusCode = 404
    /// </exception>
    private async Task<List<Producto?>> Usp_Sel_Co_Productos(int IdCategoria)
    {
        var productos = await _productoAccess.GetProductosByCategoria(IdCategoria);

        if (productos is null)
            throw new HttpRequestException(
                message: "No Producto to show",
                inner: null,
                statusCode: HttpStatusCode.NotFound);

        return productos;
    }

    /// <summary> Gets all 'Productos' </summary>
    /// <returns>List(Producto)</returns>
    public async Task<List<Producto?>> GetAllProductsByCategoria()
    {
        var categorias = await _categoriaAccess.GetAllCategorias();
        var productos = new List<Producto>();

        foreach (var categoria in categorias)
            productos.AddRange((await Usp_Sel_Co_Productos(categoria.IdCategoria))!);

        return productos!;
    }

    /// <summary> Creates a new Categoria </summary>
    /// <exception cref="HttpRequestException">
    /// <code>if there's a problem in the action of creating a 'Categoria':</code> -> StatusCode = 500
    /// </exception>
    public void Usp_Ins_Co_Categoria(Categoria categoria)
    {
        try
        {
            _categoriaAccess.Create(categoria);
        }
        catch (Exception e)
        {
            throw new HttpRequestException(
                message: $"{e.Message}",
                inner: null,
                statusCode: HttpStatusCode.InternalServerError);
        }
    }

    /// <summary> Gets all 'Categorias' </summary>
    /// <returns>List(Categoria)</returns>
    public async Task<List<Categoria>> GetAllCategorias()
        => await _categoriaAccess.GetAllCategorias();

    /// <summary> Recieves an idCategoria, searches it in the Database
    /// and tries to delete the corresponding 'Categoria'. </summary>
    /// <param name="idCategoria"></param>
    /// <exception cref="HttpRequestException">
    /// <code>if NotFound:</code>-> StatusCode = 404
    /// <code>if there's a problem in the action of deleting a 'Categoria':</code> -> StatusCode = 500
    /// </exception>
    public async Task EliminarCategoria(int idCategoria)
    {
        var categoria = await _categoriaAccess.GetCategoria(idCategoria: idCategoria);

        if (categoria is null)
            throw new HttpRequestException(
                message: "Categoria not found",
                inner: null,
                statusCode: HttpStatusCode.NotFound);

        try
        {
            _categoriaAccess.Delete(categoria: categoria);
        }
        catch (Exception e)
        {
            throw new HttpRequestException(
                message: $"{e.Message}",
                inner: null,
                statusCode: HttpStatusCode.InternalServerError);
        }
    }
}