using Moq;
using StudioAgenda.Domain.Repositorios.Agenda;

namespace CommonTestsUtilies.Repositorios.Agenda;

public class IAgendaRepositoryBuilder
{
    public static IRegistrarAgendaRepository Build()
    {
        var mock = new Mock<IRegistrarAgendaRepository>();
        return mock.Object;
    }
}