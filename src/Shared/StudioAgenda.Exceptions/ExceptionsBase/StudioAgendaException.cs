using System.Net;

namespace StudioAgenda.Exceptions.ExceptionsBase;

public abstract class StudioAgendaException : Exception
{
    public abstract HttpStatusCode PegarStatusCode();
    public abstract List<string> PegarMensagensDeErro();
}