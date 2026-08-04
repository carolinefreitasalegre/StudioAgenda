namespace StudioAgenda.Domain.Entidades;

public class Profissional : UsuarioBase
{
    public string Email { get; set; } =  string.Empty;
    public string? Especialidade { get; set; } =  string.Empty;
    //public decimal PrecoAtendimento { get; set; }
}