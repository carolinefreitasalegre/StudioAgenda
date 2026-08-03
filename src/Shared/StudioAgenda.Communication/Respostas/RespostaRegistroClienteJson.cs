namespace StudioAgenda.Communication.Respostas;

public class RespostaRegistroClienteJson
{
    public string Nome { get; set; }
    public RespostaTokensJson Type { get; set; } = new RespostaTokensJson();
}