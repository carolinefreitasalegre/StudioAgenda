using Moq;
using StudioAgenda.Domain.Repositorios.Profissional;

namespace CommonTestsUtilies.Repositorios.Profissional;

public class IProfissionalRepositoryBuilder
{
    public static IRegistrarProfissionalRepository Build()
    {
        var mock = new Mock<IRegistrarProfissionalRepository>();
        return mock.Object;
    }
}