using Ingenico.Barcode.Shared.Enums;
using Ingenico.Barcode.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationResult;

namespace Ingenico.Barcode.API.Controllers;

public class BaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task<ActionResult> SendCommand(IRequest<Result> request, int statusCode = 200)
        => await _mediator.Send(request) switch
        {
            (true, _) => StatusCode(statusCode),
            var (_, error) => HandleError(error!)
        };

    protected async Task<ActionResult<T>> SendCommand<T>(IRequest<Result<T>> request, int statusCode = 200)
        => await _mediator.Send(request).ConfigureAwait(false) switch
        {
            (true, var result, _) => StatusCode(statusCode, result),
            var (_, res, error) => HandleError(error!)
        };

    protected ActionResult HandleError(Exception error)
    {
        if (error is ExcecaoAplicacao excecao)
        {
            return BadRequest(FormatErrorMessage(excecao.ResponseErro));
        }
        else
        {
            // Se a exce��o n�o for do tipo ExcecaoAplicacao, retorna um erro gen�rico
            return BadRequest(FormatErrorMessage(AnaliseMensagemErro.Generico));
        }
    }

    private ErrorResult FormatErrorMessage(ErrorResult responseErro, IEnumerable<string>? errors = null)
    {
        if (errors != null)
        {
            responseErro.Descricao = $"{responseErro.Descricao}: {string.Join("; ", errors!)}";
        }

        return responseErro;
    }
}
