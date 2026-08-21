using System.Net;

namespace StudioAgenda.Exceptions.ExceptionsBase;

public class ErrorOnValidationAgendaException : StudioAgendaException
{
    private readonly List<string> _errors;

    public ErrorOnValidationAgendaException(List<string> mensagensErro) => _errors = mensagensErro;
    
    public override HttpStatusCode PegarStatusCode()
    {
        return HttpStatusCode.BadRequest;
    }

    public override List<string> PegarMensagensDeErro()
    {
        return _errors;
    }
}