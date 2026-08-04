namespace StudioAgenda.Domain.Entidades;

public class UsuarioBase
{
    public Guid  Id { get; set; } 
    public string Nome { get; set; } =  string.Empty;
    public string Telefone { get; set; } =  string.Empty;
    public string Senha{ get; set; } =  string.Empty;
} 