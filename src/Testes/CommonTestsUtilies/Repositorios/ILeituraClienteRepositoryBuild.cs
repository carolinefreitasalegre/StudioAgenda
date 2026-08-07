using Moq;
using StudioAgenda.Domain.Repositorios;

namespace CommonTestsUtilies.Repositorios;

public class ILeituraClienteRepositoryBuild
{
    private readonly  Mock<ILeituraClienteRepository> _mock;

    public ILeituraClienteRepositoryBuild()
    {
        _mock = new  Mock<ILeituraClienteRepository>();
    }

    public void ExisteUsuarioAtivoTelefone(string telefone)
    {
        _mock.Setup(repositorio => repositorio.ExisteUsuarioAtivoTelefone(telefone)).ReturnsAsync(true);
    }

    public ILeituraClienteRepository Build()
    {
        return _mock.Object;
    }
}