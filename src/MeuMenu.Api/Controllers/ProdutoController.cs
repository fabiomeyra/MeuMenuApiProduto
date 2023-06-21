using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MeuMenu.Api.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet("obter-todos")]
        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            return await _produtoAppService.ObterTodosProdutosAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid id)
        {
            var produto = await _produtoAppService.ObterProdutoPorIdAsync(id);

            if (produto is null) return NotFound("Produto não encontrado.");

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await _produtoAppService.AdicionarProdutoAsync(produtoViewModel);

            return Ok(produtoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Atualizar(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.ProdutoId)
            {
                //NotificarErro("O id informado não é o mesmo que foi passado na query");
                //return CustomResponse(fornecedorViewModel);
                return BadRequest("O id informado não é o mesmo que foi passado na query");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            produtoViewModel = await _produtoAppService.AtualizarProdutoAsync(produtoViewModel);

            return Ok(produtoViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid id)
        {
            var produtoViewModel = await _produtoAppService.ObterProdutoPorIdAsync(id);

            if (produtoViewModel == null) return NotFound("Produto não encontrado.");

            await _produtoAppService.ExcluirProdutoAsync(id);

            return Ok(produtoViewModel);
        }
    }
}
