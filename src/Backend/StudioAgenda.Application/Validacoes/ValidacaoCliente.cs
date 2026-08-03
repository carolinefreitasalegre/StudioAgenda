using FluentValidation;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.Validacoes;

public class ValidacaoCliente : ValidacaoUsuarioBase<RequisicaoRegistrarCliente>
{
    public ValidacaoCliente()
    {
        RuleFor(x => x.PontosFidelidade).GreaterThanOrEqualTo(0);    
    }
}