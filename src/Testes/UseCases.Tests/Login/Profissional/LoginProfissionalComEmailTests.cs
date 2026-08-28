using CommonTestsUtilies.Entidades;
using CommonTestsUtilies.Repositorios;
using CommonTestsUtilies.Repositorios.Profissional;
using CommonTestsUtilies.Requisicoes;
using Shouldly;
using StudioAgenda.Application.UseCases.Login;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace UseCases.Tests.Login.Profissional;

public class LoginProfissionalComEmailTests
{
    [Fact]
    public async Task Success()
    {
        var (profissional, _) = ProfissionalBuilder.Build();
        var requisicao = RequisicaoLoginProfissionalComEmailJsonBuilder.Build();
        requisicao.Email = profissional.Email;

        var useCase = Login(requisicao.Senha, profissional);
        var result = await useCase.Execute(requisicao);
        
        result.ShouldNotBeNull();
        result.Nome.ShouldBe(profissional.Nome);
        result.Token.TokenAcesso.ShouldNotBeNull();
        result.Token.RecarregarToken.ShouldNotBeNull();
    }

    [Fact]
    public async Task ShouldThrowException_WhenUserDontExist()
    {
        var requisicao = RequisicaoLoginProfissionalComEmailJsonBuilder.Build();
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
        var (profissional, _) = ProfissionalBuilder.Build();
        var requisicao = RequisicaoLoginProfissionalComEmailJsonBuilder.Build();
        requisicao.Senha = "senhaaleatoria123#";

        var useCase = Login(profissional: profissional);
        
        var exception = await useCase.Execute(requisicao).ShouldThrowAsync<InvalidLoginException>();
        exception.PegarMensagensDeErro().ShouldSatisfyAllConditions(err =>
        {
            err.Count().ShouldBe(1);
            err.ShouldContain("Senha ou email inválido");
        });
    }
    
    
    private LoginProfissionalComEmailESenha Login(string? senha = null, StudioAgenda.Domain.Entidades.Profissional? profissional = null)
    {
        var senhaHash = new ISenhaHashBuilder();
        var profissionalLeituraBuilder = new ILeituraProfissionalRepositoryBuilder();
        
        if(profissional is not null)
            profissionalLeituraBuilder.BuscarProfissionalAtivoEmail(profissional);
        
        if(senha !=  null)
            senhaHash.VerificarSenha(senha);

        return new LoginProfissionalComEmailESenha(profissionalLeituraBuilder.Build(), senhaHash.Build());

    }
}