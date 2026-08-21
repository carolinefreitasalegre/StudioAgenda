using CommonTestsUtilies.Repositorios;
using CommonTestsUtilies.Requisicoes;
using Shouldly;
using StudioAgenda.Application.UseCases.Cliente;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace UseCases.Tests.Clente.Registrar;

public class RegistrarClienteTests
{
    [Fact]
    public async Task Success()
    {
        var leituraRepository = new ILeituraClienteRepositoryBuilder().Build();
        var request = RequisicaoRegistrarClienteJsonBuilder.Build();
        var useCase = RegistrarClienteUseCase(leituraRepository);
        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Nome.ShouldBe(request.Nome);
        result.Type.TokenAcesso.ShouldNotBeNull();
        result.Type.RecarregarToken.ShouldNotBeNull();
    }

    [Fact]
    public async Task ShouldHNotRegisterClient_WhenTelephoneAlreadyExists()
    {
        var request = RequisicaoRegistrarClienteJsonBuilder.Build();
        var leituraRepository = new ILeituraClienteRepositoryBuilder()
            .ExisteUsuarioAtivoTelefone(request.Telefone)
            .Build();

        var useCase = RegistrarClienteUseCase(leituraRepository);
        var result = async () => await useCase.Execute(request);
        var exception = await result.ShouldThrowAsync<ErrorOnValidationAgendaException>();
        exception.PegarMensagensDeErro().ShouldContain("Telefone já existe.");
    }

    
    private RegistrarCliente RegistrarClienteUseCase(ILeituraClienteRepository clienteRepository)
    {
        var unitOfWork = IUnitOfWorkBuilder.Build();
        var registrarRepository = IClienteRepositoryBuilder.Build();
        var senhaHash = new ISenhaHashBuilder().Build();
        
        return new RegistrarCliente(unitOfWork, registrarRepository, senhaHash, clienteRepository);
    }
}