using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Seguranca.SenhaHash;
using StudioAgenda.Domain.Seguranca.Tokens;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Application.UseCases.Login.LoginCliente;

public class LoginClienteComTelefoneESenha : ILoginClienteComTelefoneESenha
{
    private readonly ILeituraClienteRepository _repository; 
    private readonly ISenhaHash _hashSenha;
    private readonly IAccessTokenGernerator _token;

    public LoginClienteComTelefoneESenha(ILeituraClienteRepository repository, ISenhaHash hashSenha, IAccessTokenGernerator token)
    {
        _repository = repository;
        _hashSenha = hashSenha;
        _token = token;
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
            Token = new RespostaTokensJson
            {
                TokenAcesso = _token.GeneratorCliente(cliente)
            }
        };
    }
}