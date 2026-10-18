using FluentValidation;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.Validacoes;

public class ValidacaoEditarAgendaCliente : AbstractValidator<RequisicaoEdicaoAgendaCliente>
{
    public ValidacaoEditarAgendaCliente()
    {
        RuleFor(agenda => agenda.Data).NotEmpty().WithMessage("Selecionar o dia desejado.");
        RuleFor(agenda => agenda.HoraInicio).NotEmpty().WithMessage("Selecionar o horário desejado.");
        RuleFor(agenda => agenda.Servico).NotNull().WithMessage("Selecione um serviço desejado");
    }
}