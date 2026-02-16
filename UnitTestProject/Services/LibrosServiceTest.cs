using BooksNeorisApp.DTOs;
using BooksNeorisApp.Entities;
using BooksNeorisApp.Exceptions;
using BooksNeorisApp.Interfaces;
using BooksNeorisApp.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using UnitTestProject.DataBaseMock;

namespace UnitTestProject.Services
{
    public class LibrosServiceTest
    {
        private readonly BooksNeorisContext _context;
        private LibroService _librosService;
        private readonly Mock<IAutorService> _autorServiceMock = new();
        private readonly Mock<IConfiguration> _configurationMock = new();
        private readonly Mock<IConfigurationSection> _configSectionMock = new();

        public LibrosServiceTest()
        {
            // Crea contexto en memoria para cada prueba
            _context = DbContextHelper.CreateContextWithData();
            _autorServiceMock.Setup(a => a.ExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
            _configSectionMock.Setup(x => x.Value).Returns("10");
            _configurationMock
                .Setup(c => c.GetSection("MaximoLibrosPermitidos"))
                .Returns(_configSectionMock.Object);

            _librosService = new LibroService(_context, _autorServiceMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task CreateAsyncTestOK()
        {
            var createDto = new CreateLibroDto
            {
                Titulo = "Clean Code",
                Año = 2008,
                Genero = "Tecnología",
                NumeroDePaginas = 464,
                AutorId = 1
            };

            var result = await _librosService.CreateAsync(createDto);

            Assert.NotNull(result);
            Assert.Equal("Juan Ballesteros", result.NombreAutor);
        }

        [Fact]
        public async Task CreateAsyncTestNoAutorError()
        {
            _autorServiceMock.Setup(a => a.ExistsAsync(It.IsAny<int>())).ReturnsAsync(false);

            _librosService = new LibroService(_context, _autorServiceMock.Object, _configurationMock.Object);
            var createDto = new CreateLibroDto
            {
                Titulo = "Clean Code",
                Año = 2008,
                Genero = "Tecnología",
                NumeroDePaginas = 464,
                AutorId = 1
            };

            var result = Assert.ThrowsAsync<AutorNoEncontradoException>(() => _librosService.CreateAsync(createDto));

            Assert.NotNull(result);
            Assert.Equal("El autor no está registrado", result.Result.Message);
        }

        [Fact]
        public async Task CreateAsyncTestMaximosPermitidosError()
        {
            _configSectionMock.Setup(x => x.Value).Returns("1");

            _configurationMock
                .Setup(c => c.GetSection("MaximoLibrosPermitidos"))
                .Returns(_configSectionMock.Object);

            _librosService = new LibroService(_context, _autorServiceMock.Object, _configurationMock.Object);
            var createDto = new CreateLibroDto
            {
                Titulo = "Clean Code",
                Año = 2008,
                Genero = "Tecnología",
                NumeroDePaginas = 464,
                AutorId = 1
            };

            var result = Assert.ThrowsAsync<MaximoLibrosPermitidosException>(() => _librosService.CreateAsync(createDto));

            Assert.NotNull(result);
            Assert.Equal("No es posible registrar el libro, se alcanzó el máximo permitido", result.Result.Message);
        }

        [Fact]
        public async Task GetByIdAsyncTestOK()
        {
            int id = 2;
            var libro = await _librosService.GetByIdAsync(id);
            Assert.NotNull(libro);
            Assert.Equal("Patrones de diseño", libro.Titulo);
        }


        [Fact]
        public async Task GetAllAsyncTestOK()
        {
            var libros = await _librosService.GetAllAsync();
            Assert.NotNull(libros);
            Assert.True(libros.Count() == 2);
        }
    }
}
