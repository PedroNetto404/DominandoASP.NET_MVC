using ASP_NET_MVC5_DEV_IO.Business.Core.Notificacoes;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.DataAbstraction;
using ASP_NET_MVC5_DEV_IO.Business.Models.Produtos.DataAbstrations;
using ASP_NET_MVC5_DEV_IO.Business.Models.Produtos.Entidades;
using Microsoft.AspNetCore.Mvc;
using ASP_NET_MVC5_DEV_IO.Business.Models.Produtos.Services;
using ASPNET_MVC5_DEV_IO.Application.ViewModels;
using AutoMapper;

namespace ASPNET_MVC5_DEV_IO.Application.Controllers;

public class ProdutosController : BaseController
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IProdutoService _produtoService;

    public ProdutosController(
        IFornecedorRepository fornecedorRepository,
        IProdutoRepository produtoRepository,
        IProdutoService produtoService,
        IMapper mapper,
        INotificador notificador) : base(mapper, notificador)
    {
        _produtoService = produtoService;
        _produtoRepository = produtoRepository;
        _fornecedorRepository = fornecedorRepository;
    }

    [Route("lista-de-produtos")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores()));
    }

    [Route("editar-produto/{id:guid}")]
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var produtoViewModel = await ObterProduto(id);

        if (produtoViewModel == null) return NotFound();

        return View(produtoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("editar-produto")]
    public async Task<IActionResult> Edit(ProdutoViewModel produtoViewModel)
    {
        if (!ModelState.IsValid) return View(produtoViewModel);
        
        await _produtoService.Atualizar(_mapper.Map<Produto>(produtoViewModel));
        
        if(!OperacaoValida()) return View(produtoViewModel);
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("excluir-produto/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var produtoViewModel = await ObterProduto(id);

        if (produtoViewModel == null) return NotFound();

        return View(produtoViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Route("excluir-produto/{id:guid}")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var produtoViewModel = await ObterProduto(id);

        if (produtoViewModel == null)
        {
            return NotFound();
        }

        await _produtoService.Remover(id);
        
        if(!OperacaoValida()) return View(produtoViewModel);
        
        return RedirectToAction("Index");
    }

    [Route("novo-produto")]
    public async Task<IActionResult> Create()
    {
        var produto = new ProdutoViewModel();

        await PopularFornecedores(produto);

        return View(produto);
    }

    [Route("novo-produto")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
    {
        await PopularFornecedores(produtoViewModel);

        if (!ModelState.IsValid) return View(produtoViewModel);

        var imgPrefixo = Guid.NewGuid() + "_";
        if (!UploadImagem(produtoViewModel.ImagemFile, imgPrefixo)) return View(produtoViewModel);

        await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));
        
        if(!OperacaoValida()) return View(produtoViewModel);
        
        return RedirectToAction("Index");
    }


    [Route("dados-do-produto/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var produtoViewModel = await ObterProduto(id);

        if (produtoViewModel == null) return NotFound();

        return View(produtoViewModel);
    }

    private async Task<ProdutoViewModel> ObterProduto(Guid id)
    {
        var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));

        await PopularFornecedores(produto);

        return produto;
    }

    private async Task PopularFornecedores(ProdutoViewModel produtoViewModel) =>
        produtoViewModel.Fornecedores =
            _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _produtoRepository.Dispose();
            _produtoService.Dispose();
        }

        base.Dispose(disposing);
    }

    private bool UploadImagem(FormFile img, string imgPrefixo)
    {
        if (img == null || img.Length <= 0)
        {
            ModelState.AddModelError(string.Empty, "Imagem em formato inválido");
            return false;
        }

        var path = Path.Combine("~/imagens", imgPrefixo + img.FileName);

        if (System.IO.File.Exists(path))
        {
            ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome");
        }

        System.IO.File.Create(path);
        return true;
    }
}