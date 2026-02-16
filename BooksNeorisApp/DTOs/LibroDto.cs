namespace BooksNeorisApp.DTOs
{
    public class LibroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int Año { get; set; }
        public string Genero { get; set; } = string.Empty;
        public int NumeroDePaginas { get; set; }
        public int AutorId { get; set; }
        public string NombreAutor { get; set; } = string.Empty;
    }

    public class CreateLibroDto
    {
        public string Titulo { get; set; } = string.Empty;
        public int Año { get; set; }
        public string Genero { get; set; } = string.Empty;
        public int NumeroDePaginas { get; set; }
        public string NombreAutor { get; set; } = string.Empty;
        public int AutorId { get; set; }
    }
}
