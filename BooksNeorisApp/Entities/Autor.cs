namespace BooksNeorisApp.Entities;

public partial class Autor
{
    public int Id { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string CiudadProcedencia { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public virtual ICollection<Libro> Libro { get; set; } = new List<Libro>();
}
