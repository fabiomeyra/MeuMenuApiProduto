using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Models;
using MeuMenu.Infra.CrossCutting.AppSettings;
using Microsoft.Extensions.Options;

namespace MeuMenu.Application.AppServices;

public class ProdutoAppService : IProdutoAppService
{
    private readonly IProdutoService _produtoService;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    public ProdutoAppService(
        IProdutoService produtoService, 
        IMapper mapper, 
        IOptions<AppSettings> op)
    {
        _produtoService = produtoService;
        _mapper = mapper;
        _appSettings = op.Value;
    }

    public async Task<ProdutoViewModel> AdicionarProdutoAsync(ProdutoAddViewModel produtoAddViewModel)
    {
        try
        {
            if (produtoAddViewModel.ProdutoImagem is null) throw new Exception("Imagem obrigatória!");

            var produto = _mapper.Map<Produto>(produtoAddViewModel);

            produto.GerarId();

            produto.ProdutoImagem = await EnviarImagemAzure(produtoAddViewModel);

            produto = await _produtoService.AdicionarProdutoAsync(produto);

            return _mapper.Map<ProdutoViewModel>(produto);
        }
        catch (Exception e)
        {
            // TODO: add msgs de tratamento de erro
            throw;
        }
    }

    public async Task<ProdutoViewModel> AtualizarProdutoAsync(ProdutoAddViewModel produtoAddViewModel)
    {
        var produto = _mapper.Map<Produto>(produtoAddViewModel);

        if (!string.IsNullOrEmpty(produtoAddViewModel.ProdutoImagem) && !produtoAddViewModel.ProdutoImagem.StartsWith("https"))
            produto.ProdutoImagem = await EnviarImagemAzure(produtoAddViewModel);

        produto = await _produtoService.AtualizarProdutoAsync(produto);

        return _mapper.Map<ProdutoViewModel>(produto);
    }
    
    public async Task ExcluirProdutoAsync(Guid produtoId)
    {
        await _produtoService.ExcluirProdutoAsync(new Produto { ProdutoId = produtoId});
    }

    public async Task<ProdutoViewModel?> ObterProdutoPorIdAsync(Guid produtoId)
    {
        var produto = await _produtoService.ObterProdutoPorIdAsync(produtoId);
        return _mapper.Map<ProdutoViewModel?>(produto);
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterTodosProdutosAsync()
    {
        var produtos = await _produtoService.ObterTodosProdutosAsync();
        return _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterProdutosPorCategoriaAsync(int categoriaId)
    {
        var produtos = await _produtoService.ObterTodosProdutosAsync();
        produtos = produtos.Where(p => p.CategoriaId == categoriaId);
        return _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterProdutosAtivosAsync()
    {
        var produtos = await _produtoService.ObterProdutosAtivosAsync();
        return _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
    }

    public async Task<ProdutoValorViewModel?> BuscarProdutoValorAsync(Guid produtoId)
    {
        var produto = await _produtoService.BuscarProdutoValorAsync(produtoId);
        return _mapper.Map<ProdutoValorViewModel>(produto);
    }

    public async Task<ICollection<ProdutoDescricaoImagemViewModel>> BuscarProdutoDescricaoEImagemAsync(ICollection<Guid> listaIds)
    {
        var produto = await _produtoService.BuscarProdutoDescricaoEImagemAsync(listaIds);
        return _mapper.Map<ICollection<ProdutoDescricaoImagemViewModel>>(produto);
    }

    private async Task<string> EnviarImagemAzure(ProdutoAddViewModel produtoAddViewModel)
    {
        if (produtoAddViewModel.ProdutoImagem == null) return null!;
        var fileName = Guid.NewGuid() + ".jpg";
        var imgbytes = Convert.FromBase64String(produtoAddViewModel.ProdutoImagem);

        var blobClient = new BlobClient(_appSettings?.Azure?.ArmazenamentoImagens, _appSettings?.Azure?.NomePastaImagens, fileName);

        using (var stream = new MemoryStream(imgbytes))
        {
            await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = new BlobHttpHeaders { ContentType = "image/jpeg" } });
        }

        return blobClient.Uri.AbsoluteUri;
    }
}