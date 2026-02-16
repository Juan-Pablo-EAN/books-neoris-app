namespace BooksNeorisApp.Entities;

public partial class Libro
{
    public int Id { get; set; }

    public int Año { get; set; }

    public string Titulo { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public int NumeroDePaginas { get; set; }

    public int AutorId { get; set; }

    public virtual Autor Autor { get; set; } = null!;
}
