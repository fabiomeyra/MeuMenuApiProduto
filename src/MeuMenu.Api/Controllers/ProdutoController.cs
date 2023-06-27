using MeuMenu.Api.Controllers.Base;
using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels;
using MeuMenu.Domain.Interfaces.Notificador;
using Microsoft.AspNetCore.Mvc;

namespace MeuMenu.Api.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutoController(
            IProdutoAppService produtoAppService,
            INotificador notificador) : base(notificador)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var retorno = await _produtoAppService.ObterTodosProdutosAsync();
            return RespostaPadrao(retorno);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var produto = await _produtoAppService.ObterProdutoPorIdAsync(id);
            if (produto is not null) return RespostaPadrao(produto);
            NotificarErro("Produto não encontrado.");
            return RespostaPadrao();

        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromForm]ProdutoAddViewModel produtoAddViewModel)
        {
            var produtoViewModel = await _produtoAppService.AdicionarProdutoAsync(produtoAddViewModel);
            return RespostaPadrao(produtoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromForm]ProdutoAddViewModel produtoAddViewModel)
        {
            if (id != produtoAddViewModel.ProdutoId)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return RespostaPadrao();
            }

            if (!ModelState.IsValid) return RespostaPadrao(ModelState);

            var produtoViewModel = await _produtoAppService.AtualizarProdutoAsync(produtoAddViewModel);
            return RespostaPadrao(produtoViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var produtoViewModel = await _produtoAppService.ObterProdutoPorIdAsync(id);

            if (produtoViewModel == null)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return RespostaPadrao();
            }

            await _produtoAppService.ExcluirProdutoAsync(id);

            return RespostaPadrao(produtoViewModel);
        }
    }
}
