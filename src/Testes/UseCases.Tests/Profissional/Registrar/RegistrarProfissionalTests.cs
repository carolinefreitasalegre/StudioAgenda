using CommonTestsUtilies.Repositorios;
using CommonTestsUtilies.Repositorios.Profissional;
using CommonTestsUtilies.Requisicoes;
using Shouldly;
using StudioAgenda.Application.UseCases.Profissional.Registrar;
using StudioAgenda.Domain.Repositorios.Profissional;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace UseCases.Tests.Profissional.Registrar;

public class RegistrarProfissionalTests
{
    [Fact]
    public async Task Success()
    {
        var leituraRepository = new ILeituraProfissionalRepositoryBuilder().Build();
        var request = RequisicaoRegistrarProfissionalJsonBuilder.Build();
        var useCase = RegistrarProfissionalUseCase(leituraRepository);
        var result = await useCase.Execute(request);
        
        result.ShouldNotBeNull();
        result.Nome.ShouldBe(request.Nome);
        result.Type.TokenAcesso.ShouldNotBeNull();
        result.Type.RecarregarToken.ShouldNotBeNull();
    }

    [Fact]
    public async Task ShouldNotRegisterProfissional_WhenEmailAlreadyExists()
    {
        var request = RequisicaoRegistrarProfissionalJsonBuilder.Build();
        var leituraRepository = new ILeituraProfissionalRepositoryBuilder()
            .ExisteUsuaioAtivoEmail(request.Email)
            .Build();

        var useCase = RegistrarProfissionalUseCase(leituraRepository);
        var result = async () =>  await useCase.Execute(request);
        var exception = await result.ShouldThrowAsync<ErrorOnValidationAgendaException>();
        exception.PegarMensagensDeErro().ShouldContain("Email já existe.");
            
    }
    
    private RegistrarProfissional RegistrarProfissionalUseCase( ILeituraProfissionalRepository leituraRepository)
    {
        var unitOfWork = IUnitOfWorkBuilder.Build();
        var registrarRepository = IProfissionalRepositoryBuilder.Build();
        var senhaHash = new ISenhaHashBuilder().Build();
        
        return new RegistrarProfissional(unitOfWork, registrarRepository, leituraRepository, senhaHash);
    } 
    
}