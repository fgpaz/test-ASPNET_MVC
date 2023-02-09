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

    #region Producto

    /// <summary> Obtains all 'Productos' associated to an Categoria's ID </summary>
    /// <returns>List(Producto)</returns>
    /// <exception cref="HttpRequestException">
    ///     <code>if NotFound:</code>-> StatusCode = 404
    /// </exception>
    private async Task<List<Producto?>> Usp_Sel_Co_Productos(int IdCategoria)
    {
        var productos = await _productoAccess.GetProductosByCategoria(IdCategoria);

        if (productos is null)
            throw new HttpRequestException(
                "No Producto to show",
                null,
                HttpStatusCode.NotFound);

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

    #endregion

    #region Categoria

    /// <summary> Gets all 'Categorias' </summary>
    /// <returns>List(Categoria)</returns>
    public async Task<List<Categoria>> GetAllCategorias()
    {
        return await _categoriaAccess.GetAllCategorias();
    }

    /// <summary> Obtains a 'Categoria' by its id </summary>
    /// <param name="idCategoria"></param>
    /// <returns>if it succeed returns the 'Categoria'</returns>
    /// <exception cref="HttpRequestException">
    ///     <code>if NotFound:</code>-> StatusCode = 404
    ///     <code>if there's a problem in the action of deleting a 'Categoria':</code> -> StatusCode = 500
    /// </exception>
    public async Task<Categoria?> GetCategoria(int idCategoria)
    {
        try
        {
            var categoria = await _categoriaAccess.GetCategoria(idCategoria);
            if (categoria is null)
                throw new HttpRequestException(
                    "Categoria not found",
                    null,
                    HttpStatusCode.NotFound);
            return categoria;
        }
        catch (Exception e)
        {
            throw new HttpRequestException(
                $"{e.Message}",
                null,
                HttpStatusCode.InternalServerError);
        }
    }

    /// <summary> Creates a new Categoria </summary>
    /// <returns>if it succeed returns the Created'Categoria'</returns>
    /// <exception cref="HttpRequestException">
    ///     <code>if there's a problem in the action of creating a 'Categoria':</code> -> StatusCode = 500
    /// </exception>
    public Categoria Usp_Ins_Co_Categoria(Categoria categoria)
    {
        try
        {
            _categoriaAccess.Create(categoria);
            return categoria;
        }
        catch (Exception e)
        {
            throw new HttpRequestException(
                $"{e.Message}",
                null,
                HttpStatusCode.InternalServerError);
        }
    }


    /// <summary> Updates an existing 'Categoria </summary>
    /// <param name="categoria"></param>
    /// <returns>if it succeed returns the Updated 'Categoria'</returns>
    /// <exception cref="HttpRequestException">
    ///     <code>if there's a problem in the action of creating a 'Categoria':</code> -> StatusCode = 500
    /// </exception>
    public Categoria UpdateCategoria(Categoria categoria)
    {
        try
        {
            _categoriaAccess.Update(categoria);
            return categoria;
        }
        catch (Exception e)
        {
            throw new HttpRequestException(
                $"{e.Message}",
                null,
                HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    ///     Recieves an idCategoria, searches it in the Database
    ///     and tries to delete the corresponding 'Categoria'
    /// </summary>
    /// <param name="idCategoria"></param>
    /// <exception cref="HttpRequestException">
    ///     <code>if NotFound:</code>-> StatusCode = 404
    ///     <code>if there's a problem in the action of deleting a 'Categoria':</code> -> StatusCode = 500
    /// </exception>
    public async Task DeleteCategoria(int idCategoria)
    {
        var categoria = await _categoriaAccess.GetCategoria(idCategoria);

        if (categoria is null)
            throw new HttpRequestException(
                "Categoria not found",
                null,
                HttpStatusCode.NotFound);

        try
        {
            _categoriaAccess.Delete(categoria);
        }
        catch (Exception e)
        {
            throw new HttpRequestException(
                $"{e.Message}",
                null,
                HttpStatusCode.InternalServerError);
        }
    }

    #endregion
}