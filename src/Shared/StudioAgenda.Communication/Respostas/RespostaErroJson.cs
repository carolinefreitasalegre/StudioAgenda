namespace StudioAgenda.Communication.Respostas;

public class RespostaErroJson
{
    public IList<string> Errors { get; set; }
    public bool TokenIsExpired { get; set; }

    public RespostaErroJson(IList<string> errors) => Errors = errors;

    public RespostaErroJson(string error,  bool tokenIsExpired)
    {
        Errors = [error];
        TokenIsExpired = tokenIsExpired;
      
    }
}