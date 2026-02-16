using BooksNeorisApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace UnitTestProject.DataBaseMock
{
    public static class DbContextHelper
    {
        public static BooksNeorisContext CreateContextWithData()
        {
            var context = CreateInMemoryContext();
            SetTestData(context);
            return context;
        }

        public static BooksNeorisContext CreateInMemoryContext(string? databaseName = null)
        {
            var dbName = databaseName ?? Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<BooksNeorisContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .EnableSensitiveDataLogging()
                .Options;
            return new BooksNeorisContext(options);
        }

        private static void SetTestData(BooksNeorisContext context)
        {
            var autores = new[]
            {
                new Autor
                {
                    Id = 1,
                    NombreCompleto = "Juan Ballesteros",
                    FechaNacimiento = new DateOnly(1999, 10, 26),
                    CiudadProcedencia = "Zipaquirá",
                    CorreoElectronico = "correo@gmail.com"
                },
                new Autor
                {
                    Id = 2,
                    NombreCompleto = "Pablo Obando",
                    FechaNacimiento = new DateOnly(1999, 10, 26),
                    CiudadProcedencia = "Zipaquirá",
                    CorreoElectronico = "correo@gmail.com"
                }
            };

            context.Autor.AddRange(autores);

            var libros = new[]
            {
                new Libro
                {
                    Id = 1,
                    Titulo = "Clean Code",
                    Año = 2026,
                    Genero = "Software",
                    NumeroDePaginas = 471,
                    AutorId = 1
                },
                new Libro
                {
                    Id = 2,
                    Titulo = "Patrones de diseño",
                    Año = 2025,
                    Genero = "Software",
                    NumeroDePaginas = 433,
                    AutorId = 2
                }
            };

            context.Libro.AddRange(libros);
            context.SaveChanges();
        }
    }
}
