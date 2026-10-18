using Bogus;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace CommonTestsUtilies.Requisicoes;

public class RequisicaoLoginProfissionalComEmailJsonBuilder
{
    public static RequisicaoProfissionalLogin Build()
    {
        return new Faker<RequisicaoProfissionalLogin>()
            .RuleFor(req => req.Email, f => f.Internet.Email())
            .RuleFor(login => login.Senha, faker => faker.Internet.Password() + "#1");
    }
}