using Moq;
using StudioAgenda.Domain.Entidades;
using StudioAgenda.Domain.Repositorios;

namespace CommonTestsUtilies.Repositorios;

public class ILeituraClienteRepositoryBuilder
{
    private readonly Mock<ILeituraClienteRepository> _mock;

    public ILeituraClienteRepositoryBuilder()
    {
        _mock = new  Mock<ILeituraClienteRepository>();
    }

    public ILeituraClienteRepositoryBuilder ExisteUsuarioAtivoTelefone(string telefone)
    {
        _mock.Setup(repositorio => repositorio.ExisteUsuarioAtivoTelefone(telefone)).ReturnsAsync(true);
        return this;
    }

    public ILeituraClienteRepositoryBuilder ObterViaTelefone(Cliente cliente)
    {
        _mock.Setup(repositorio => repositorio.ObterViaTelefone(cliente.Telefone)).ReturnsAsync(cliente);
        return this;
    }


    public ILeituraClienteRepository Build()
    {
        return _mock.Object;
    }
}