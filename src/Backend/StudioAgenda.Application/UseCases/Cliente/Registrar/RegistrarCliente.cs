using FluentValidation;
using Mapster;
using StudioAgenda.Application.Validacoes;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Seguranca.SenhaHash;
using StudioAgenda.Domain.Seguranca.Tokens;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Application.UseCases.Cliente;

public class RegistrarCliente : IRegistrarCliente
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRegistrarClienteReposirory _registrarCliente;
    private readonly ILeituraClienteRepository _leituraClienteRepository;
    private readonly ISenhaHash _senhaHash;
    private readonly IAccessTokenGernerator _tokenGernerator;

    public RegistrarCliente(IUnitOfWork unitOfWork, IRegistrarClienteReposirory registrarCliente, ISenhaHash senhaHash, 
        ILeituraClienteRepository leituraClienteRepository, IAccessTokenGernerator tokenGernerator)
    {
        _unitOfWork = unitOfWork;
        _registrarCliente = registrarCliente;
        _senhaHash  = senhaHash;
        _leituraClienteRepository = leituraClienteRepository;
        _tokenGernerator = tokenGernerator;
    }

    public async Task<RespostaRegistroUsuarioJson> Execute(RequisicaoRegistrarCliente dados)
    {
        await ValidarDadosEntrada(dados);
       
        var clienteRegistrado = dados.Adapt<Domain.Entidades.Cliente>();

        await SenhaHash(clienteRegistrado);
        
        await _registrarCliente.RegistrarCliente(clienteRegistrado);
        await _unitOfWork.Commit();
        
        return new RespostaRegistroUsuarioJson
        {
            Nome = clienteRegistrado.Nome,
            Token = new RespostaTokensJson
            {
                TokenAcesso = _tokenGernerator.Generator(clienteRegistrado)
            }
        };
    }

    private async Task ValidarDadosEntrada(RequisicaoRegistrarCliente dados)
    {
        var validator = new ValidacaoRegistroCliente();
        var resultado = await validator.ValidateAsync(dados);
        
        var existeTelefone = await _leituraClienteRepository.ExisteUsuarioAtivoTelefone(dados.Telefone);
        if (existeTelefone)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Telefone já existe."));
        
        if (!resultado.IsValid)
        {
            var errorMessage = resultado.Errors
                .Select(erro => erro.ErrorMessage).ToList();
            throw new ErrorOnValidationAgendaException(errorMessage);
        }
    }

    private async Task SenhaHash(Domain.Entidades.Cliente dados)
    {
        dados.Senha = _senhaHash.HashSenha(dados.Senha);
    }
}