using FluentValidation;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.Validacoes;

public class ValidacaoRegistroCliente : ValidacaoUsuarioBase<RequisicaoRegistrarCliente>
{
    public ValidacaoRegistroCliente()
    {
        RuleFor(x => x.PontosFidelidade).GreaterThanOrEqualTo(0);    
    }
}