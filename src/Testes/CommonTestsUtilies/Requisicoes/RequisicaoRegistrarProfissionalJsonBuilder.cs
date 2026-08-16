using Bogus;
using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Repositorios.Profissional;

namespace CommonTestsUtilies.Requisicoes;

public class RequisicaoRegistrarProfissionalJsonBuilder
{
    public static RequisicaoRegistrarProfissional Build()
    {
        return new Faker<RequisicaoRegistrarProfissional>()
            .RuleFor(req => req.Nome, f => f.Person.FirstName)
            .RuleFor(req => req.Telefone, f => f.Random.ReplaceNumbers("###########"))
            .RuleFor(req => req.Senha, f => f.Internet.Password() + "#1")
            .RuleFor(req => req.Email, f => f.Internet.Email());
    }
}