using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess;

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
        _context.SaveChanges();
    }

    public async Task<Categoria?> GetCategoria(int idCategoria)
        => await _context.Categoria
            .FirstOrDefaultAsync(
                c => c.IdCategoria == idCategoria);

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
        _context.SaveChanges();
    }

    public void Delete(Categoria categoria)
    {
        _context.Remove(categoria);
        _context.SaveChanges();
    }
}