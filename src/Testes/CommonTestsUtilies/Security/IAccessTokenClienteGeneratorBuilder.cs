using Bogus;
using Moq;
using StudioAgenda.Domain.Entidades;
using StudioAgenda.Domain.Seguranca.Tokens;

namespace UseCases.Tests;

public class IAccessTokenClienteGeneratorBuilder
{
    public static IAccessTokenGernerator Build()
    {
        var mock = new Mock<IAccessTokenGernerator>();

        var fakerToken = new Faker().Random.String2(32, "scahclaycgançlcasyc78956basnbcia");
        
        mock.Setup(generator => generator.Generator(It.IsAny<Cliente>())).Returns(fakerToken);
        
        return mock.Object;
    }
}