namespace StudioAgenda.Exceptions.ExceptionsBase;

public class ErrorOnValidationAgendaException : StudioAgendaException
{
    private readonly List<string> _errors;

    public ErrorOnValidationAgendaException(List<string> mensagensErro) => _errors = mensagensErro;
    
    public List<string> PegarMensagensErro() => _errors;
}