namespace StudioAgenda.Domain.Dtos.Requisicoes;

public class RequisicaoRegistrarProfissional : UsuarioBaseRequisicao
{
    public string Email { get; set; } =  string.Empty;
    public string? Especialidade { get; set; } =  string.Empty;
}