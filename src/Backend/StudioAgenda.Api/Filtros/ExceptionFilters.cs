using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Api.Filtros;

public class ExceptionFilters : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is StudioAgendaException studioAgendaException)
        {
            var statusCode = (int)studioAgendaException.PegarStatusCode();

            context.Result = new ObjectResult(
                new RespostaErroJson(studioAgendaException.PegarMensagensDeErro()))
            {
                StatusCode = statusCode
            };
        }
        else
        {
            context.Result = new ObjectResult(new RespostaErroJson("Erro desconhecido."))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        context.ExceptionHandled = true;
    }
}