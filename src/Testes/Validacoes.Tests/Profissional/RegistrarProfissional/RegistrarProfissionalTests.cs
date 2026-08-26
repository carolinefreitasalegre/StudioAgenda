using CommonTestsUtilies.Requisicoes;
using Shouldly;
using StudioAgenda.Application.Validacoes;

namespace TestesValidacoes.Profissional.RegistrarProfissional;

public class RegistrarProfissionalTests
{
    [Fact]
    public void Success()
    {
        var request = RequisicaoRegistrarProfissionalJsonBuilder.Build();
        var validator = new ValidacaoRegistroProfissional();
        var result = validator.Validate(request);
        
        Assert.True(result.IsValid);
        result.IsValid.ShouldBeTrue();
    }
    

    [Fact]
    public void Validate_ShouldHaveError_WhenEmailIsEmpty()
    {
        var request = RequisicaoRegistrarProfissionalJsonBuilder.Build();
        request.Email = string.Empty;
        var validator = new ValidacaoRegistroProfissional();
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.ShouldContain(err => err.ErrorMessage.Equals("Campo email deve ser preenchido."));
            errors.ShouldContain(err=>err.ErrorMessage.Equals("Preencha um email válido."));
        });
    }
    [Fact]
    public void Validate_ShouldHaveError_WhenNomeIsEmpty()
    {
        var request = RequisicaoRegistrarProfissionalJsonBuilder.Build();
        request.Nome = string.Empty;
        var validator = new ValidacaoRegistroProfissional();
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.ShouldContain(err=>err.ErrorMessage.Equals("Campo nome deve ser preenchido."));
        });
    }
    [Fact]
    public void Validate_ShouldHaveError_WhenTelefoneIsEmpty()
    {
        var request = RequisicaoRegistrarProfissionalJsonBuilder.Build();
        request.Telefone = string.Empty;
        var validator = new ValidacaoRegistroProfissional();
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.ShouldContain(err=>err.ErrorMessage.Equals("Campo telefone deve ser preenchido."));
        });
    }
    [Fact]
    public void Validate_ShouldHaveError_WhenSenhaIsEmpty()
    {
        var request = RequisicaoRegistrarProfissionalJsonBuilder.Build();
        request.Senha = string.Empty;
        var validator = new ValidacaoRegistroProfissional();
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.ShouldContain(err=>err.ErrorMessage.Equals("Campo senha deve ser preenchido."));
        });
    }
    
}