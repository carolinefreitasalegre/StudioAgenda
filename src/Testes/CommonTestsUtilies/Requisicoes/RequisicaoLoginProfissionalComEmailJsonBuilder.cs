using Bogus;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace CommonTestsUtilies.Requisicoes;

public class RequisicaoLoginProfissionalComEmailJsonBuilder
{
    public static RequisicaoProfissionalLoginJson Build()
    {
        return new Faker<RequisicaoProfissionalLoginJson>()
            .RuleFor(req => req.Email, f => f.Internet.Email())
            .RuleFor(login => login.Senha, faker => faker.Internet.Password() + "#1");
    }
}