using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MeuMenu.Api.Controllers
{
    [ApiController]
    [Route("api/categoria")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaAppService _categoriaAppService;

        public CategoriaController(ICategoriaAppService categoriaAppService)
        {
            _categoriaAppService = categoriaAppService;
        }

        [HttpGet("obter-todos")]
        public async Task<IEnumerable<CategoriaViewModel>> ObterTodos()
        {
            return await _categoriaAppService.ObterTodasCategoriasAsync();
        }
    }
}
