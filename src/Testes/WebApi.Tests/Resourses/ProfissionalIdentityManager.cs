namespace WebApi.Tests.Resourse;

public class ProfissionalIdentityManager
{
    private readonly StudioAgenda.Domain.Entidades.Profissional _profissional;
    private readonly string _senha;


    public ProfissionalIdentityManager(StudioAgenda.Domain.Entidades.Profissional profissional, string senha)
    {
        _profissional = profissional;
        _senha = senha;
    }

    public string PegarNome() => _profissional.Nome;
    public string PegarEmail() => _profissional.Email;
    public string PegarSenha() => _senha;
}