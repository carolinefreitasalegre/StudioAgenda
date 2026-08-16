using Moq;
using StudioAgenda.Domain.Repositorios.Profissional;

namespace CommonTestsUtilies.Repositorios.Profissional;

public class ILeituraProfissionalRepositoryBuilder
{
    private readonly Mock<ILeituraProfissionalRepository> _mock;

    public ILeituraProfissionalRepositoryBuilder()
    {
        _mock = new Mock<ILeituraProfissionalRepository>();
    }

    public ILeituraProfissionalRepositoryBuilder ExisteUsuaioAtivoEmail(string email)
    {
        _mock.Setup(repo => repo.ExisteProfissionalAtivoEmail(email)).ReturnsAsync(true);
        return this;
    }

    public ILeituraProfissionalRepository Build()
    {
        return _mock.Object;
    }
}