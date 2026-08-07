using FluentValidation;
using Mapster;
using StudioAgenda.Application.Validacoes;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Seguranca.SenhaHash;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Application.UseCases.Cliente;

public class RegistrarCliente : IRegistrarCliente
{
    //private readonly IValidator<RequisicaoRegistrarCliente> _validar;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRegistrarClienteReposirory _registrarCliente;
    private readonly ILeituraClienteRepository _leituraClienteRepository;
    private readonly ISenhaHash _senhaHash;

    public RegistrarCliente( IUnitOfWork unitOfWork, IRegistrarClienteReposirory registrarCliente, ISenhaHash senhaHash, ILeituraClienteRepository leituraClienteRepository)
    {
        // _validar = validar;
        _unitOfWork = unitOfWork;
        _registrarCliente = registrarCliente;
        _senhaHash  = senhaHash;
        _leituraClienteRepository = leituraClienteRepository;
    }

    public async Task<RespostaRegistroClienteJson> Execute(RequisicaoRegistrarCliente dados)
    {
        await ValidarDadosEntrada(dados);
       
        var clienteRegistrado = dados.Adapt<Domain.Entidades.Cliente>();

        SenhaHash(clienteRegistrado);
        
        await _registrarCliente.RegistrarCliente(clienteRegistrado);
        await _unitOfWork.Commit();
        
        return clienteRegistrado.Adapt<RespostaRegistroClienteJson>();
    }

    private async Task ValidarDadosEntrada(RequisicaoRegistrarCliente dados)
    {
        var validator = new ValidacaoCliente();
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

    private void SenhaHash(Domain.Entidades.Cliente dados)
    {
        dados.Senha = _senhaHash.HashSenha(dados.Senha);
    }
}