using BooksNeorisApp.DTOs;
using BooksNeorisApp.Entities;
using BooksNeorisApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BooksNeorisApp.Services
{
    public class AutorService(BooksNeorisContext context) : IAutorService
    {
        private readonly BooksNeorisContext _context = context;

        /// <summary>
        /// Valida que exista un autor según el id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Autor.AnyAsync(a => a.Id == id);
        }

        /// <summary>
        /// Crea un nuevo autor a partir de un DTO, lo guarda en la base de datos y devuelve el DTO del autor creado con su id asignado
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<AutorDto> CreateAsync(CreateAutorDto dto)
        {
            var autor = new Autor
            {
                NombreCompleto = dto.NombreCompleto,
                FechaNacimiento = DateOnly.FromDateTime(dto.FechaNacimiento),
                CiudadProcedencia = dto.CiudadProcedencia,
                CorreoElectronico = dto.CorreoElectronico
            };

            _context.Autor.Add(autor);
            await _context.SaveChangesAsync();

            return (await GetByIdAsync(autor.Id))!;
        }

        /// <summary>
        /// Obtiene un autor por su id, si no existe devuelve null, si existe devuelve un DTO con la información del autor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AutorDto?> GetByIdAsync(int id)
        {
            var autor = _context.Autor.FindAsync(id).Result;
            if (autor == null) return null;

            return new AutorDto
            {
                Id = autor.Id,
                NombreCompleto = autor.NombreCompleto,
                FechaNacimiento = autor.FechaNacimiento.ToDateTime(TimeOnly.MinValue),
                CiudadProcedencia = autor.CiudadProcedencia,
                CorreoElectronico = autor.CorreoElectronico
            };
        }

        /// <summary>
        /// Obtiene un autor según su nombre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AutorDto?> GetByNameAsync(string name)
        {
            var autor = _context.Autor.FirstOrDefaultAsync(a => a.NombreCompleto == name).Result;
            if (autor == null) return null;

            return new AutorDto
            {
                Id = autor.Id,
                NombreCompleto = autor.NombreCompleto,
                FechaNacimiento = autor.FechaNacimiento.ToDateTime(TimeOnly.MinValue),
                CiudadProcedencia = autor.CiudadProcedencia,
                CorreoElectronico = autor.CorreoElectronico
            };
        }

        /// <summary>
        /// Obtiene todos los autores registrados en la base de datos
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AutorDto>> GetAllAsync()
        {
            return await _context.Autor
                .Select(a => new AutorDto
                {
                    Id = a.Id,
                    NombreCompleto = a.NombreCompleto,
                    FechaNacimiento = a.FechaNacimiento.ToDateTime(TimeOnly.MinValue),
                    CiudadProcedencia = a.CiudadProcedencia,
                    CorreoElectronico = a.CorreoElectronico
                })
                .ToListAsync();
        }

        /// <summary>
        /// Para eliminar un autor en un futuro desarrollo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Para actualizar la info de un autor en un futuro desarrollo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AutorDto> UpdateAsync(int id, CreateAutorDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
