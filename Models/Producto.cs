namespace Models;

public sealed class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public double Precio { get; set; }

    public int? IdCategoria { get; set; }

    public Categoria? Categoria { get; set; }
}