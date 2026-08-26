using Bogus;
using CommonTestsUtilies.Repositorios;
using StudioAgenda.Domain.Entidades;

namespace CommonTestsUtilies.Entidades;

public class ClienteBuilder
{
    public static (Cliente cliente, string senha) Build()
    { 
       var (senha,  senhaHash) =  GerarSenhaRandomica();
       var cliente = new Faker<Cliente>()
            .RuleFor(usuario => usuario.Nome, faker => faker.Person.FirstName)
            .RuleFor(usuario => usuario.Telefone, faker => faker.Random.ReplaceNumbers("###########"))
            .RuleFor(usuario => usuario.Senha, _ => senhaHash);
       
       return (cliente, senha);
    }

    private static (string senha, string senhaHash) GerarSenhaRandomica()
    {
        var senhaEncripter = new ISenhaHashBuilder().Build();

        var senha = new Faker().Internet.Password();
        
        return (senha, senhaEncripter.HashSenha(senha));
    }
}