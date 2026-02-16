using BooksNeorisApp.DTOs;

namespace BooksNeorisApp.Interfaces
{
    public interface ILibroService
    {
        Task<IEnumerable<LibroDto>> GetAllAsync();
        Task<LibroDto?> GetByIdAsync(int id);
        Task<LibroDto> CreateAsync(CreateLibroDto dto);
        Task<LibroDto> UpdateAsync(int id, CreateLibroDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
