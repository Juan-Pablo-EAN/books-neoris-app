using Microsoft.EntityFrameworkCore;

namespace BooksNeorisApp.Entities;

public partial class BooksNeorisContext : DbContext
{
    public BooksNeorisContext()
    {
    }

    public BooksNeorisContext(DbContextOptions<BooksNeorisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autor { get; set; }

    public virtual DbSet<Libro> Libro { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasIndex(e => e.CorreoElectronico, "IX_Autor_CorreoElectronico").IsUnique();

            entity.Property(e => e.CiudadProcedencia).HasMaxLength(100);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.NombreCompleto).HasMaxLength(200);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.Property(e => e.Genero).HasMaxLength(50);
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.Autor).WithMany(p => p.Libro)
                .HasForeignKey(d => d.AutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Libro_Autor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
