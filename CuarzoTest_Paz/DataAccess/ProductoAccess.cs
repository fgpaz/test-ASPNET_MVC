﻿using CuarzoTest_Paz.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace CuarzoTest_Paz.DataAccess;

public class ProductoAccess
{
    private readonly DBContext _context;

    public ProductoAccess(DBContext context)
    {
        _context = context;
    }

    public void Create(Producto producto)
    {
        _context.Add(producto);
    }

    public async Task<List<Producto?>> GetProductosByCategoria(List<int> listIdCategoria)
    {
        return await _context.Set<Producto>()
            .Where(producto => listIdCategoria.Contains(producto.Categoria.IdCategoria))
            .ToListAsync();
    }

    public async Task<List<Producto>> GetAllProductos()
    {
        return await _context.Producto.Select(p => new Producto
        {
            IdProducto = p.IdProducto,
            NombreProducto = p.NombreProducto,
            Precio = p.Precio, IdCategoria = p.IdCategoria,
            Categoria = p.Categoria,
        }).ToListAsync();
    }

    public void Update(Producto producto)
    {
        _context.Update(producto);
    }

    public void Delete(Producto producto)
    {
        _context.Remove(producto);
    }
}