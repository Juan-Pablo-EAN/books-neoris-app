using BooksNeorisApp.DTOs;
using BooksNeorisApp.Entities;
using BooksNeorisApp.Exceptions;
using BooksNeorisApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BooksNeorisApp.Services
{
    public class LibroService(BooksNeorisContext context, IAutorService autorService, IConfiguration configuration) : ILibroService
    {
        private readonly BooksNeorisContext _context = context;
        private readonly IAutorService _autorService = autorService;
        private readonly IConfiguration _configuration = configuration;
        private const int MAXIMO_LIBROS_PERMITIDOS = 10;

        /// <summary>
        /// Crea un nuevo libro. Verifica que el autor exista y que no se haya alcanzado el máximo de libros permitidos antes de crear el libro.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="AutorNoEncontradoException"></exception>
        /// <exception cref="MaximoLibrosPermitidosException"></exception>
        public async Task<LibroDto> CreateAsync(CreateLibroDto dto)
        {
            if (!await _autorService.ExistsAsync(dto.AutorId))
            {
                throw new AutorNoEncontradoException();
            }

            var maximoLibrosPermitidos = _configuration.GetValue<int>("MaximoLibrosPermitidos", MAXIMO_LIBROS_PERMITIDOS);
            var cantidadActual = await _context.Libro.CountAsync();

            if (cantidadActual >= maximoLibrosPermitidos)
            {
                throw new MaximoLibrosPermitidosException();
            }

            var libro = new Libro
            {
                Titulo = dto.Titulo,
                Año = dto.Año,
                Genero = dto.Genero,
                NumeroDePaginas = dto.NumeroDePaginas,
                AutorId = dto.AutorId
            };

            _context.Libro.Add(libro);
            await _context.SaveChangesAsync();

            return (await GetByIdAsync(libro.Id))!;
        }

        /// <summary>
        /// Obtiene un libro por su ID. Si el libro no existe, devuelve null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<LibroDto?> GetByIdAsync(int id)
        {
            var libro = _context.Libro.Include(l => l.Autor).FirstOrDefaultAsync(l => l.Id == id);

            if (libro == null) return null;

            return new LibroDto
            {
                Id = libro.Result!.Id,
                Titulo = libro.Result!.Titulo,
                Año = libro.Result!.Año,
                Genero = libro.Result!.Genero,
                NumeroDePaginas = libro.Result!.NumeroDePaginas,
                AutorId = libro.Result!.AutorId,
                NombreAutor = libro.Result!.Autor.NombreCompleto
            };
        }

        /// <summary>
        /// Obtiene todos los libros registrados en la base de datos
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LibroDto>> GetAllAsync()
        {
            return await _context.Libro
               .Select(l => new LibroDto
               {
                   Id = l.Id,
                   Titulo = l.Titulo,
                   Año = l.Año,
                   Genero = l.Genero,
                   NumeroDePaginas = l.NumeroDePaginas,
                   AutorId = l.AutorId,
                   NombreAutor = l.Autor.NombreCompleto
               })
               .ToListAsync();
        }

        /// <summary>
        /// Elimna un libro de la base de datos, para un futuro desarrollo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Actualiza la info de un libro, para un futuro desarrollo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<LibroDto> UpdateAsync(int id, CreateLibroDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
