using CommonTestsUtilies.Requisicoes;
using Shouldly;
using StudioAgenda.Application.Validacoes;

namespace TestesValidacoes.Agenda;

public class RegistrarAgendaTests
{
    [Fact]
    public void Success()
    {
        var request = RequisicaoRegistrarAgendaJsonBuilder.Build();
        var validator = new ValidacaoRegistrarAgenda();
        var result = validator.Validate(request);
        
        Assert.True(result.IsValid);
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenDateIsEmpty()
    {
        var request = RequisicaoRegistrarAgendaJsonBuilder.Build();
        request.DataHora = DateTime.MinValue;
        var validator = new ValidacaoRegistrarAgenda();
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals("Selecionar dia e hora."));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenProfissionalIdIsEmpty()
    {
        var request = RequisicaoRegistrarAgendaJsonBuilder.Build();
        request.ProfissionalId = Guid.Empty;
        var validator = new ValidacaoRegistrarAgenda();
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(err=> err.ErrorMessage.Equals("Selecione a profissional desejada."));
        });
        
    }
}