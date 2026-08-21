using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Repositorios.Profissional;
using StudioAgenda.Domain.Seguranca.SenhaHash;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Application.UseCases.Login;

public class LoginProfissionalComEmailESenha : ILoginProfissionalComEmailESenha
{
    private readonly ILeituraProfissionalRepository _profissionalRepository;
    private readonly ISenhaHash _senhaHash;

    public LoginProfissionalComEmailESenha(ILeituraProfissionalRepository profissionalRepository, ISenhaHash senhaHash)
    {
        _profissionalRepository = profissionalRepository;
        _senhaHash = senhaHash;
    }

    public async Task<RespostaRegistroProfissionalJson> Execute(RequisicaoProfissionalLoginJson requisicao)
    {
        var profissional = await _profissionalRepository.ObterViaEmail(requisicao.Email);
        
        if (profissional is null)
            throw new InvalidLoginException();
        
        var validarSenha = _senhaHash.VerificarSenha(requisicao.Senha, profissional.Senha);
    
        if(!validarSenha)
            throw new InvalidLoginException();

        return new RespostaRegistroProfissionalJson
        {
            Nome = profissional.Nome,
        };
    }
}