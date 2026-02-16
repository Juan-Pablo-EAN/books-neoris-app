using BooksNeorisApp.DTOs;
using BooksNeorisApp.Exceptions;
using BooksNeorisApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksNeorisApp.Controllers
{
    public class LibrosController(ILibroService libroService, IAutorService autorService) : Controller
    {
        private readonly ILibroService _libroService = libroService;
        private readonly IAutorService _autorService = autorService;

        public async Task<IActionResult> Index()
        {
            var libros = await _libroService.GetAllAsync();
            return View(libros);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLibroDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                var result = await _autorService.GetByNameAsync(dto.NombreAutor) ?? throw new AutorNoEncontradoException();
                dto.AutorId = result.Id;
                await _libroService.CreateAsync(dto);
                TempData["Success"] = "Libro registrado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (MaximoLibrosPermitidosException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
            catch (AutorNoEncontradoException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }
    }
}
