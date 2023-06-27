using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MeuMenu.Application.Extensions;
using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace MeuMenu.Application.AppServices;

public class ProdutoAppService : IProdutoAppService
{
    private readonly IProdutoService _produtoService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public ProdutoAppService(IProdutoService produtoService, IMapper mapper, IConfiguration configuration)
    {
        _produtoService = produtoService;
        _mapper = mapper;
        _configuration = configuration;
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

        if (produtoAddViewModel.ProdutoImagem is not null)
            produto.ProdutoImagem = await EnviarImagemAzure(produtoAddViewModel);

        produto = await _produtoService.AtualizarProdutoAsync(produto);

        return _mapper.Map<ProdutoViewModel>(produto);
    }
    
    public async Task ExcluirProdutoAsync(Guid produtoId)
    {
        await _produtoService.ExcluirProdutoAsync(produtoId);
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

    private async Task<string> EnviarImagemAzure(ProdutoAddViewModel produtoAddViewModel)
    {
        var fileName = Guid.NewGuid() + ".jpg";
        var imgbytes = await produtoAddViewModel.ProdutoImagem.ConvertIFormFileToByteArray();

        var blobClient = new BlobClient(_configuration["Azure:ArmazenamentoImagens"], _configuration["Azure:NomePastaImagens"], fileName);

        using (var stream = new MemoryStream(imgbytes))
        {
            await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = new BlobHttpHeaders { ContentType = "image/jpeg" } });
        }

        return blobClient.Uri.AbsoluteUri;
    }
}