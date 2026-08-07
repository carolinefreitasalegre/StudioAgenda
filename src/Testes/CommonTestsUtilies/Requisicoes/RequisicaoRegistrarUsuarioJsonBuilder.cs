using Bogus;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace CommonTestsUtilies.Requisicoes;

public class RequisicaoRegistrarUsuarioJsonBuilder
{
    public static RequisicaoRegistrarCliente Build()
    {
        return new Faker<RequisicaoRegistrarCliente>()
            .RuleFor(req => req.Nome, f => f.Person.FirstName)
            .RuleFor(req => req.Telefone, f => f.Random.ReplaceNumbers("###########"))
            .RuleFor(req => req.Senha, f => f.Internet.Password() + "#1");
    }
}