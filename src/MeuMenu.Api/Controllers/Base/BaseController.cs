using MeuMenu.Domain.Interfaces.Notificador;
using MeuMenu.Domain.Notificador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MeuMenu.Api.Controllers.Base;

public class BaseController : ControllerBase
{
    private readonly INotificador _notificador;

    public BaseController(INotificador notificador)
    {
        _notificador = notificador;
    }

    protected bool OperacaoValida() => !_notificador.TemNotificacao();

    protected IActionResult RespostaPadrao(object? result = null)
    {
        return OperacaoValida()
            ? Ok(new
            {
                success = true,
                data = result
            })
            : BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem).Distinct()
            });
    }

    protected void NotificarErro(string mensagem)
    {
        _notificador.AdicionarNotificacao(new Notificacao(mensagem));
    }

    protected IActionResult RespotaPadrao(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
        foreach (var erro in erros)
            NotificarErro(erro);

        return RespostaPadrao();
    }

    protected IActionResult RetornaMensagemErro(string mensagem)
    {
        _notificador.LimparNotificacoes();
        NotificarErro(mensagem);
        return RespostaPadrao();
    }
}