using FluentValidation;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.Validacoes;

public class ValidacaoRegistrarAgenda : AbstractValidator<RequisicaoRegistrarAgenda>
{
    public ValidacaoRegistrarAgenda()
    {
        RuleFor(agenda => agenda.Data).NotEmpty().WithMessage("Selecionar o dia desejado.");
        RuleFor(agenda => agenda.HoraInicio).NotEmpty().WithMessage("Selecionar o horário desejado.");
        RuleFor(agenda => agenda.ProfissionalId).NotEmpty().WithMessage("Selecione a profissional desejada.");
        RuleFor(agenda => agenda.ClienteId).NotEmpty().WithMessage("Identificação do cliente não pode estar em branco.");
        RuleFor(agenda => agenda.Servico).NotNull().WithMessage("Selecione um serviço desejado");
    }
}