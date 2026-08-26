using Bogus;
using CommonTestsUtilies.Repositorios;
using StudioAgenda.Domain.Entidades;

namespace CommonTestsUtilies.Entidades;

public class ProfissionalBuilder
{
    public static (Profissional profissional, string senha) Build()
    {
        var (senha, senhaHash) = GerarSenhaRandomica();
        var profissional = new Faker<Profissional>()
            .RuleFor(profissional => profissional.Nome, faker => faker.Person.FullName)
            .RuleFor(profissional => profissional.Email, faker => faker.Person.Email)
            .RuleFor(usuario => usuario.Senha, _ => senhaHash);

        return  (profissional, senha);
    }

    private static (string senha, string senhaHash) GerarSenhaRandomica()
    {
        var senhaEncripter = new ISenhaHashBuilder().Build();
        var senha = new Faker().Internet.Password() + "F1";
        
        return (senha, senhaEncripter.HashSenha(senha));
    }
}