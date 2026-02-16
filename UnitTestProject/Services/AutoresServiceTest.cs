using BooksNeorisApp.DTOs;
using BooksNeorisApp.Entities;
using BooksNeorisApp.Services;
using UnitTestProject.DataBaseMock;

namespace UnitTestProject.Services
{
    public class AutoresServiceTest
    {
        private readonly BooksNeorisContext _context;
        private readonly AutorService _autorService;

        public AutoresServiceTest()
        {
            // Crea contexto en memoria para cada prueba
            _context = DbContextHelper.CreateContextWithData();
            _autorService = new AutorService(_context);
        }

        [Fact]
        public async Task ExistsAsyncTestOK()
        {
            int id = 1;
            bool exists = await _autorService.ExistsAsync(id);
            Assert.True(exists);
        }

        [Fact]
        public async Task CreateAsyncTestOK()
        {
            var createDto = new CreateAutorDto
            {
                NombreCompleto = "Mario Vargas Llosa",
                FechaNacimiento = new DateTime(1936, 3, 28),
                CiudadProcedencia = "Arequipa",
                CorreoElectronico = "mvargasllosa@example.com"
            };

            var resultado = await _autorService.CreateAsync(createDto);

            Assert.NotNull(resultado);
            Assert.True(resultado.Id > 0);
            Assert.Equal("Mario Vargas Llosa", resultado.NombreCompleto);
        }

        [Fact]
        public async Task GetByIdAsyncTestOK()
        {
            int id = 1;
            var autor = await _autorService.GetByIdAsync(id);

            Assert.NotNull(autor);
            Assert.Equal("Juan Ballesteros", autor.NombreCompleto);
        }

        [Fact]
        public async Task GetByNameAsyncTestOK()
        {
            string name = "Juan Ballesteros";
            var autor = await _autorService.GetByNameAsync(name);

            Assert.NotNull(autor);
            Assert.Equal("Juan Ballesteros", autor.NombreCompleto);
            Assert.Equal(1, autor.Id);
        }

        [Fact]
        public async Task GetAllAsyncTestOK()
        {
            var result = await _autorService.GetAllAsync();
            Assert.True(result.Count() == 2);
        }
    }
}
