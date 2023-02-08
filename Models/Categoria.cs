namespace Models;

public sealed class Categoria
{
    public int IdCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public bool EsActiva { get; set; }

    public IEnumerable<Producto> Productos { get; } = new List<Producto>();
}