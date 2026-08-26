
namespace WebApi.Tests.Resourse;

public class ClienteIdentyManager
{
    private readonly StudioAgenda.Domain.Entidades.Cliente _cliente;
    private readonly string _senha;

    public ClienteIdentyManager(StudioAgenda.Domain.Entidades.Cliente cliente, string senha)
    {
        _cliente = cliente;
        _senha = senha;
    }

    public string PegarNome() => _cliente.Nome;
    public string PegarTelefone() => _cliente.Telefone;
    public string PegarSenha() => _senha;
    

}