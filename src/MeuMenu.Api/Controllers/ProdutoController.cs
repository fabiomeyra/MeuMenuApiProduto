using MeuMenu.Api.Controllers.Base;
using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels;
using MeuMenu.Domain.Interfaces.Notificador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuMenu.Api.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
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

        [AllowAnonymous]
        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var retorno = await _produtoAppService.ObterTodosProdutosAsync();
            return RespostaPadrao(retorno);
        }

        [AllowAnonymous]
        [HttpGet("buscar-descricao-e-imagem")]
        public async Task<IActionResult> ObterTodos([FromQuery] ICollection<Guid> ids)
        {
            var retorno = await _produtoAppService.BuscarProdutoDescricaoEImagemAsync(ids);
            return RespostaPadrao(retorno);
        }

        [AllowAnonymous]
        [HttpGet("obter-por-categoria/{categoriaId:int}")]
        public async Task<IActionResult> ObterTodos(int categoriaId)
        {
            var retorno = await _produtoAppService.ObterProdutosPorCategoriaAsync(categoriaId);
            return RespostaPadrao(retorno);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var produto = await _produtoAppService.ObterProdutoPorIdAsync(id);
            if (produto is not null) return RespostaPadrao(produto);
            NotificarErro("Produto não encontrado.");
            return RespostaPadrao();

        }

        [AllowAnonymous]
        [HttpGet("produto-valor")]
        public async Task<IActionResult> BuscarProdutoValor([FromQuery] Guid id)
        {
            var produto = await _produtoAppService.BuscarProdutoValorAsync(id);
            if (produto is not null) return RespostaPadrao(produto);
            NotificarErro("Produto não encontrado.");
            return RespostaPadrao();

        }
        
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Adicionar([FromForm]ProdutoAddViewModel produtoAddViewModel)
        {
            var produtoViewModel = await _produtoAppService.AdicionarProdutoAsync(produtoAddViewModel);
            return RespostaPadrao(produtoViewModel);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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
