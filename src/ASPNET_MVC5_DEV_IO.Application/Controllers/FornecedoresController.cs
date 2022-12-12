using ASP_NET_MVC5_DEV_IO.Business.Core.Notificacoes;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.DataAbstraction;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Entidades;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Services;
using ASPNET_MVC5_DEV_IO.Application.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_MVC5_DEV_IO.Application.Controllers;

[Authorize]
public class FornecedoresController : BaseController
{
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IFornecedorService _fornecedorService;

    public FornecedoresController(
        IFornecedorRepository fornecedorRepository,
        IFornecedorService fornecedorService,
        IMapper mapper,
        INotificador notificador) : base(mapper, notificador)
    {
        _fornecedorRepository = fornecedorRepository;
        _fornecedorService = fornecedorService;
    }
    
    [AllowAnonymous]
    [Route("lista-de-fornecedores")]
    public async Task<IActionResult> Index()
    {
        return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));
    }
    
    [HttpGet]
    [Route("detalhes-fornecedor/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var fornecedorViewModel = await ObterFornecedorEndereco(id);

        if (fornecedorViewModel == null) return NotFound();

        return View(fornecedorViewModel);
    }

    [HttpGet]
    [Route("novo-fornecedor")]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [Route("novo-fornecedor")]
    public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
    {
        if (!ModelState.IsValid) return View(fornecedorViewModel); 
        
        var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

        await _fornecedorService.Adicionar(fornecedor);
        
        if(!OperacaoValida()) return View(fornecedorViewModel);

        return RedirectToAction("Index"); 
    }
    [HttpGet]
    [Route("editar-fornecedor/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var fornecedor = await ObterFornecedorProdutosEndereco(id);

        if (fornecedor == null) return NotFound();

        return View(fornecedor);
    }

    [HttpPatch]
    [Route("editar-fornecedor/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, [FromBody] FornecedorViewModel fornecedorViewModel)
    {
        if (id != fornecedorViewModel.Id) return NotFound();

        await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedorViewModel));
        
        if(!OperacaoValida()) return View(fornecedorViewModel);
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("excluir-fornecedor/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var fornecedor = await ObterFornecedorEndereco(id);

        if (fornecedor == null) return NotFound();

        return View(fornecedor);
    }

    [HttpDelete, ActionName("Delete")]
    [Route("excluir-fornecedor/{id:guid}")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var fornecedor = await ObterFornecedorEndereco(id);

        if (fornecedor == null) return NotFound();

        await _fornecedorService.Remover(id);
        
        if(!OperacaoValida()) return View(fornecedor);

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    [Route("atualizar-endereco-fornecedor/{id:guid}")]
    public async Task<IActionResult> AtualizarEndereco(Guid id)
    {
        var fornecedor = await ObterFornecedorEndereco(id);

        if (fornecedor == null) return NotFound(); 
        
        return PartialView("_AtualizarEndereco", new FornecedorViewModel{ Endereco = fornecedor.Endereco });
    }

    [HttpPost]
    [Route("atualizar-endereco-fornecedor/{id:guid}")]
    public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel fornecedorViewModel)
    {
        ModelState.Remove("Nome");
        ModelState.Remove("Documento");

        if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", fornecedorViewModel); 
        
        await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(fornecedorViewModel.Endereco));
        
        if(!OperacaoValida()) return PartialView("_AtualizarEndereco", fornecedorViewModel);
        
        var url = Url.Action("ObterEndereco", "Fornecedores", new {id = fornecedorViewModel.Endereco.FornecedorId});

        return Json(new 
            { 
                sucess = true,
                url
                
            });
    }   

    [Route("obter-endereco-fornecedor/{id:guid}")]
    public async Task<IActionResult> ObterEndereco(Guid id)
    {
        var fornecedor = await ObterFornecedorEndereco(id);

        if (fornecedor == null) return NotFound();

        return PartialView("_DetalhesEndereco", fornecedor);
    }
    private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
    {
        return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
    }

    private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
    {
        return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
    }
}