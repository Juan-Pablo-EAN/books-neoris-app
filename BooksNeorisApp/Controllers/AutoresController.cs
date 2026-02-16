using BooksNeorisApp.DTOs;
using BooksNeorisApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksNeorisApp.Controllers
{
    public class AutoresController(IAutorService autorService) : Controller
    {
        private readonly IAutorService _autorService = autorService;

        public async Task<IActionResult> Index()
        {
            var autores = await _autorService.GetAllAsync();
            return View(autores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAutorDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            try
            {
                await _autorService.CreateAsync(dto);
                TempData["Success"] = "Autor registrado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al crear el autor: {ex.Message}");
                return View(dto);
            }
        }
    }
}
