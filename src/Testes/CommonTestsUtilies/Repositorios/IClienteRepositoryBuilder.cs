using Moq;
using StudioAgenda.Domain.Repositorios;

namespace CommonTestsUtilies.Repositorios;

public class IClienteRepositoryBuilder
{
    public static IRegistrarClienteReposirory Build()
    {
        var mock = new Mock<IRegistrarClienteReposirory>();
        
        return mock.Object;
    }
}