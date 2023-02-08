using Microsoft.EntityFrameworkCore;

namespace Models;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Server=DESKTOP-3Q120ST\\TEST;Database=BDCrudTest;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;Encrypt=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__coCatego__59268228CA7DF0DC");

            entity.ToTable("coCategoria");

            entity.Property(e => e.IdCategoria).HasColumnName("nIdCategori");
            entity.Property(e => e.EsActiva).HasColumnName("cEsActiva");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cNombCateg");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__coProduc__15A1B6AC9B3CC55B");

            entity.ToTable("coProducto");

            entity.Property(e => e.IdProducto).HasColumnName("nIdProduct");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cNombProdu");
            entity.Property(e => e.IdCategoria).HasColumnName("nIdCategori");
            entity.Property(e => e.Precio).HasColumnName("nPrecioProd");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__coProduct__nIdCa__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}