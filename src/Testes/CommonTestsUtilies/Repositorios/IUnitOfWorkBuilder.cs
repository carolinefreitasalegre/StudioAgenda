using Moq;
using StudioAgenda.Domain.Repositorios;

namespace CommonTestsUtilies.Repositorios;

public class IUnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Mock<IUnitOfWork>();

        return mock.Object;
    }
}