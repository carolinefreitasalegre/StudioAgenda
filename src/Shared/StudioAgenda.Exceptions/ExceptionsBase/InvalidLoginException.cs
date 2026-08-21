using System.Net;

namespace StudioAgenda.Exceptions.ExceptionsBase;

public class InvalidLoginException : StudioAgendaException
{
    public override HttpStatusCode PegarStatusCode()
    {
        return HttpStatusCode.Unauthorized;
    }

    public override List<string> PegarMensagensDeErro()
    {
        List<string> mensagensDeErro = ["Senha ou email inválido"];
        return mensagensDeErro;
    }
}