namespace StudioAgenda.Communication.Respostas;

public class RespostaRegistroUsuarioJson
{
    public string Nome { get; set; }
    public string? Email { get; set; }
    public RespostaTokensJson Type { get; set; } = new RespostaTokensJson();
}