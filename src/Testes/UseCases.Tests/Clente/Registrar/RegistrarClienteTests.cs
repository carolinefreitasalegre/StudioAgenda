using CommonTestsUtilies.Repositorios;
using CommonTestsUtilies.Requisicoes;
using Shouldly;
using StudioAgenda.Application.UseCases.Cliente;

namespace UseCases.Tests.Clente.Registrar;

public class RegistrarClienteTests
{
    [Fact]
    public async Task Success()
    {
        var request = RequisicaoRegistrarUsuarioJsonBuilder.Build();
        var useCase = RegistrarUsuarioUseCase();
        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Nome.ShouldBe(request.Nome);
        result.Type.TokenAcesso.ShouldNotBeNull();
        result.Type.RecarregarToken.ShouldNotBeNull();

    }
    

    
    private RegistrarCliente RegistrarUsuarioUseCase()
    {
        var unitOfWork = IUnitOfWorkBuilder.Build();
        var registrarRepository = IClienteRepositoryBuilder.Build();
        var lerRepository = new ILeituraClienteRepositoryBuild().Build();
        var senhaHash = new ISenaHashBuilder().Build();
        
        return new RegistrarCliente(unitOfWork, registrarRepository, senhaHash, lerRepository);
    }
}