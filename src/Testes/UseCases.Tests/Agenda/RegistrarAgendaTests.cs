using CommonTestsUtilies.Repositorios;
using CommonTestsUtilies.Repositorios.Agenda;
using CommonTestsUtilies.Requisicoes;
using Shouldly;
using StudioAgenda.Application.UseCases.Agenda;
using StudioAgenda.Domain.Repositorios.Agenda;

namespace UseCases.Tests.Agenda;

public class RegistrarAgendaTests
{
    [Fact]
    public async Task Success()
    {
        var leituraAgendaRepository = new ILeituraAgendaRepositoryBuilder().Build();
        var request = RequisicaoRegistrarAgendaJsonBuilder.Build();
        var useCase = RegistrarAgendaUseCase(leituraAgendaRepository);
        var result = await useCase.Execute(request);
        
        result.ShouldNotBeNull();
        result.DataHora.ShouldBe(request.DataHora);
    }

    // [Fact]
    // public async Task ShouldNotRegisterAgenda_WhenDateIsNotFree()
    // {
    //     var request = RequisicaoRegistrarAgendaJsonBuilder.Build();
    //     var leituraAgendaRepository = new ILeituraAgendaRepositoryBuilder()
    //         .ExisteAgendaHoje(request.DataHora)
    //         .Build();
    //     
    //     var useCase = RegistrarAgendaUseCase(leituraAgendaRepository);
    //     var result = async () => await useCase.Execute(request);
    //     var exception = await result.ShouldThrowAsync<ErrorOnValidationAgendaException>();
    //     exception.PegarMensagensErro().ShouldContain("Data indiponível no momento.");
    // }
    
    private RegistrarAgenda RegistrarAgendaUseCase(ILeituraAgendaRepository agendaRepository)
    {
        var unitOfWork = IUnitOfWorkBuilder.Build();
        var registrarRepository = IAgendaRepositoryBuilder.Build();
        
        return new RegistrarAgenda(unitOfWork, registrarRepository);
    }
}