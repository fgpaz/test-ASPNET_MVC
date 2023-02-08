using DataAccess;
using Models;

namespace Logic;

public class Logic
{
    private readonly ProductoAccess _productoAccess;
    private readonly CategoriaAccess _categoriaAccess;

    public Logic(ProductoAccess productoAccess, CategoriaAccess categoriaAccess)
    {
        _productoAccess = productoAccess;
        _categoriaAccess = categoriaAccess;
    }

    /// <summary> Obtains all 'Productos' associated to an Categoria's ID </summary>
    /// <returns>List(Producto)</returns>
    private async Task<List<Producto?>> Usp_Sel_Co_Productos(int IdCategoria)
        => await _productoAccess.GetProductosByCategoria(IdCategoria);

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
    public void Usp_Ins_Co_Categoria(Categoria categoria)
        => _categoriaAccess.Create(categoria);
}