using BooksNeorisApp.DTOs;

namespace BooksNeorisApp.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorDto>> GetAllAsync();
        Task<AutorDto?> GetByIdAsync(int id);
        Task<AutorDto?> GetByNameAsync(string name);
        Task<AutorDto> CreateAsync(CreateAutorDto dto);
        Task<AutorDto> UpdateAsync(int id, CreateAutorDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
