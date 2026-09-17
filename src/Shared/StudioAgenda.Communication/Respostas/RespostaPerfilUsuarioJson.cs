namespace StudioAgenda.Communication.Respostas;

public class RespostaPerfilUsuario
{
    public Guid Id { get; private set; }
    public string Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
}