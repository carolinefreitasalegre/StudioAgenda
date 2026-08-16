using FluentValidation;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.Validacoes;

public class ValidacaoRegistrarAgenda : AbstractValidator<RequisicaoRegistrarAgenda>
{
    public ValidacaoRegistrarAgenda()
    {
        RuleFor(agenda => agenda.DataHora).NotEmpty().WithMessage("Selecionar dia e hora.");
        RuleFor(agenda => agenda.ProfissionalId).NotEmpty().WithMessage("Selecione a profissional desejada.");
        RuleFor(agenda => agenda.ClienteId).NotEmpty().WithMessage("Identificação do cliente não pode estar em branco.");
    }
}