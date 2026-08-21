using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Seguranca.SenhaHash;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Application.UseCases.Login.LoginCliente;

public class LoginClienteComTelefoneESenha : ILoginClienteComTelefoneESenha
{
    private readonly ILeituraClienteRepository _repository;
    private readonly ISenhaHash _hashSenha;

    public LoginClienteComTelefoneESenha(ILeituraClienteRepository repository, ISenhaHash hashSenha)
    {
        _repository = repository;
        _hashSenha = hashSenha;
    }

    public async Task<RespostaRegistroClienteJson> Execute(RequisicaoClienteLoginJson requisicao)
    {
        var cliente = await _repository.ObterViaTelefone(requisicao.Telefone);
        if (cliente is null)
            throw new InvalidLoginException();

        var senha = _hashSenha.VerificarSenha(requisicao.Senha, cliente.Senha);
        if (!senha)
            throw new InvalidLoginException();

        return new RespostaRegistroClienteJson
        {
            Nome = cliente.Nome,
        };
    }
}