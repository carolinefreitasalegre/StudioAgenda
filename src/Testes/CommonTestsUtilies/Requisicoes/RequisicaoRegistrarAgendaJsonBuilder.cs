using Bogus;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace CommonTestsUtilies.Requisicoes;

public class RequisicaoRegistrarAgendaJsonBuilder
{
    public static RequisicaoRegistrarAgenda Build(Guid? clienteId = null, Guid? profissionalId = null)
    {
        return new Faker<RequisicaoRegistrarAgenda>()
            .RuleFor(req => req.ClienteId, clienteId ?? Guid.NewGuid())
            .RuleFor(req => req.DataHora, f => DateTime.Now.AddDays(1).Date.AddHours(10))
            .RuleFor(req => req.Servico, f => f.Lorem.Word())
            .RuleFor(req => req.Valor, f => f.Random.Decimal(50, 200))
            .RuleFor(req => req.ProfissionalId, profissionalId ?? Guid.NewGuid());

    }
}