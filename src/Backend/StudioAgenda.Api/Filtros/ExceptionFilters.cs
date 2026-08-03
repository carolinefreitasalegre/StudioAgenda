using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Api.Filtros;

public class ExceptionFilters : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationAgendaException errorOnValidationAgendaException)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result =
                new BadRequestObjectResult(
                    new RespostaErroJson((errorOnValidationAgendaException.PegarMensagensErro())));
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new RespostaErroJson("Erro desconhecido."));
        }
            
    }
}