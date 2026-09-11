using Mapster;
using StudioAgenda.Application.Validacoes;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Repositorios.Profissional;
using StudioAgenda.Domain.Seguranca.SenhaHash;
using StudioAgenda.Domain.Seguranca.Tokens;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Application.UseCases.Profissional.Registrar;

public class RegistrarProfissional : IRegistrarProfissional
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRegistrarProfissionalRepository _registrarProfissionalRepository;
    private readonly ILeituraProfissionalRepository _leituraProfissionalRepository;
    private readonly ISenhaHash _senhaHash;
    private readonly IAccessTokenGernerator _tokenGernerator;

    public RegistrarProfissional(IUnitOfWork unitOfWork, IRegistrarProfissionalRepository registrarProfissionalRepository, ILeituraProfissionalRepository leituraProfissionalRepository, 
        ISenhaHash senhaHash, IAccessTokenGernerator tokenGernerator)
    {
        _unitOfWork = unitOfWork;
        _registrarProfissionalRepository = registrarProfissionalRepository;
        _leituraProfissionalRepository = leituraProfissionalRepository;
        _senhaHash = senhaHash;
        _tokenGernerator = tokenGernerator;
    }
    
    public async Task<RespostaRegistroProfissionalJson> Execute(RequisicaoRegistrarProfissional dados)
    {
        await ValidarDadosEntrada(dados);

        var profissionalRegistrado = dados.Adapt<Domain.Entidades.Profissional>();
         HasSenha(profissionalRegistrado);
        
        await _registrarProfissionalRepository.RegistrarProfissional(profissionalRegistrado);
        await _unitOfWork.Commit();
        
        // return profissionalRegistrado.Adapt<RespostaRegistroProfissionalJson>();
        return new RespostaRegistroProfissionalJson
        {
            Nome = profissionalRegistrado.Nome,
            Token = new RespostaTokensJson
            {
                TokenAcesso = _tokenGernerator.GeneratorProfissional(profissionalRegistrado),
            }
        };
    }

    private async Task ValidarDadosEntrada(RequisicaoRegistrarProfissional dados)
    {
        var validacao = new ValidacaoRegistroProfissional();
        var resultado =  await validacao.ValidateAsync(dados);
        

        var existeEmail = await _leituraProfissionalRepository.ExisteProfissionalAtivoEmail(dados.Email);
        if (existeEmail)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Email já existe."));

        
        if (!resultado.IsValid)
        {
            var errorMensagem =  resultado.Errors
                .Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationAgendaException(errorMensagem);
        }
    }
    private async Task HasSenha(Domain.Entidades.Profissional dados)
    { 
        dados.Senha = _senhaHash.HashSenha(dados.Senha);
    }
    
}


