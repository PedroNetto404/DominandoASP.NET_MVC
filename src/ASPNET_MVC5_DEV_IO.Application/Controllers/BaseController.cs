using ASP_NET_MVC5_DEV_IO.Business.Core.Notificacoes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_MVC5_DEV_IO.Application.Controllers;

public abstract class BaseController : Controller
{
    protected readonly IMapper _mapper;
    protected readonly INotificador _notificador; 
    protected BaseController(IMapper mapper, INotificador notificador)
    {
        _mapper = mapper;
        _notificador = notificador; 
    }
    protected bool OperacaoValida()
    { 
        if(!_notificador.TemNotificacao()) return true; 
        
        var notificaoes = _notificador.ObterNotificacoes();

        foreach (var notificacao in notificaoes)
        {
            ModelState.AddModelError(string.Empty, notificacao.Mensagem); 
        }

        return false;
    }
}