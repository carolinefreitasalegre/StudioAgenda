using Moq;
using StudioAgenda.Domain.Repositorios.Agenda;

namespace CommonTestsUtilies.Repositorios.Agenda;

public class ILeituraAgendaRepositoryBuilder
{
    private readonly Mock<ILeituraAgendaRepository> _mock;

    public ILeituraAgendaRepositoryBuilder()
    {
        _mock = new Mock<ILeituraAgendaRepository>();
    }

    public ILeituraAgendaRepositoryBuilder ExisteAgendaHoje(DateTime hoje)
    {
        _mock.Setup(repositorio => repositorio.ExisteAegendaHoje(hoje)).ReturnsAsync(true);
        return this;
    }
    
    public ILeituraAgendaRepository Build(){
        return _mock.Object;
    }
    
}