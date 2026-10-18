using Bogus;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace CommonTestsUtilies.Requisicoes;

public class RequisicaoLoginClienteComTelefoneJsonBuilder
{
    public static RequisicaoClienteLogin Build()
    {
        return new Faker<RequisicaoClienteLogin>()
            .RuleFor(login => login.Telefone, faker => faker.Random.ReplaceNumbers("###########"))
            .RuleFor(login => login.Senha, faker => faker.Internet.Password() + "#1");
    }
}