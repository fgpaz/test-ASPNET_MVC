using CuarzoTest_Paz.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace CuarzoTest_Paz.DataAccess;

public class CategoriaAccess
{
    private readonly DBContext _context;

    public CategoriaAccess(DBContext context)
    {
        _context = context;
    }

    public void Create(Categoria categoria)
    {
        _context.Add(categoria);
    }


    public async Task<List<Categoria>> GetAllCategorias()
    {
        return await _context.Categoria.Select(categoria => new Categoria
        {
            IdCategoria = categoria.IdCategoria,
            NombreCategoria = categoria.NombreCategoria,
            EsActiva = categoria.EsActiva
        }).ToListAsync();
    }

    public void Update(Categoria categoria)
    {
        _context.Update(categoria);
    }

    public void Delete(Categoria categoria)
    {
        _context.Remove(categoria);
    }
}