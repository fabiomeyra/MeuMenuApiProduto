using MeuMenu.Api.Controllers.Base;
using MeuMenu.Application.Interfaces;
using MeuMenu.Domain.Interfaces.Notificador;
using Microsoft.AspNetCore.Mvc;

namespace MeuMenu.Api.Controllers
{
    [ApiController]
    [Route("api/categoria")]
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaAppService _categoriaAppService;

        public CategoriaController(
            ICategoriaAppService categoriaAppService, 
            INotificador notificador) : base(notificador)
        {
            _categoriaAppService = categoriaAppService;
        }

        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var retorno = await _categoriaAppService.ObterTodasCategoriasAsync();
            return RespostaPadrao(retorno);
        }
    }
}
