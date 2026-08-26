using CommonTestsUtilies.Entidades;
using CommonTestsUtilies.Repositorios;
using CommonTestsUtilies.Requisicoes;
using Shouldly;
using StudioAgenda.Application.UseCases.Login.LoginCliente;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace UseCases.Tests.Login.Cliente;

public class LoginClienteComTelefoneTests
{
    [Fact]
    public async Task Success()
    {
        var (cliente, _) = ClienteBuilder.Build();
        var requisicao = RequisicaoLoginClienteComTelefoneJsonBuilder.Build();
        requisicao.Telefone = cliente.Telefone;
        
        var useCase = Login(requisicao.Senha, cliente);
        var result = await useCase.Execute(requisicao);

        result.ShouldNotBeNull();
        result.Nome.ShouldBe(cliente.Nome);
        result.Type.TokenAcesso.ShouldNotBeNull();
        result.Type.RecarregarToken.ShouldNotBeNull();
    }

    [Fact]
    public async Task ShouldThrowException_WhenUserDontExist()
    {
        var requisicao = RequisicaoLoginClienteComTelefoneJsonBuilder.Build();
        var useCase = Login();
        
        var exception = await useCase.Execute(requisicao).ShouldThrowAsync<InvalidLoginException>();
        exception.PegarMensagensDeErro().ShouldSatisfyAllConditions(err =>
        {
            err.Count().ShouldBe(1);
            err.ShouldContain("Senha ou email inválido");
        });
    }

    [Fact]
    public async Task ShouldThrowException_WhenPasswordIsIncorrect()
    {
        var (cliente, _) = ClienteBuilder.Build();
        var requisicao = RequisicaoLoginClienteComTelefoneJsonBuilder.Build();
        requisicao.Senha = "senhaaleatoria123#";
        
        var useCase = Login(cliente:cliente);
        
        var exception = await useCase.Execute(requisicao).ShouldThrowAsync<InvalidLoginException>();
        exception.PegarMensagensDeErro().ShouldSatisfyAllConditions(err =>
        {
            err.Count().ShouldBe(1);
            err.ShouldContain("Senha ou email inválido");
        });
    }
    

    private LoginClienteComTelefoneESenha Login(string? senha = null, StudioAgenda.Domain.Entidades.Cliente? cliente = null)
    {
        var senhaHash = new ISenhaHashBuilder();
        var clienteLeituraRepositoryBuilder = new ILeituraClienteRepositoryBuilder();
        
        if(cliente is not null)
            clienteLeituraRepositoryBuilder.ObterViaTelefone(cliente);
        
        if(senha != null)
            senhaHash.VerificarSenha(senha);
        
        return new LoginClienteComTelefoneESenha(clienteLeituraRepositoryBuilder.Build(), senhaHash.Build());
    }
} 