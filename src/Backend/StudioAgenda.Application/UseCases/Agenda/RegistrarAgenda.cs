using Mapster;
using StudioAgenda.Application.Validacoes;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Repositorios.Agenda;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Application.UseCases.Agenda;

public class RegistrarAgenda : IRegistrarAgenda
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRegistrarAgendaRepository _repository;
   // private readonly ILeituraAgendaRepository _leituraAgendaRepository;

    public RegistrarAgenda(IUnitOfWork unitOfWork, IRegistrarAgendaRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<RespostaRegistroAgendaJson> Execute(RequisicaoRegistrarAgenda dados)
    {
        await ValidarDadosEntrada(dados);
        var agendaRegistrada = dados.Adapt<Domain.Entidades.Agenda>();
        
        await _repository.RegistrarAgenda(agendaRegistrada);
        await _unitOfWork.Commit();
        
        return agendaRegistrada.Adapt<RespostaRegistroAgendaJson>();
    }

    private async Task ValidarDadosEntrada(RequisicaoRegistrarAgenda dados)
    {
        var validator = new ValidacaoRegistrarAgenda();
        var resultado = await validator.ValidateAsync(dados);
        
        if (!resultado.IsValid)
        {
            var errorMessage = resultado.Errors
                .Select(erro => erro.ErrorMessage).ToList();
            throw new ErrorOnValidationAgendaException(errorMessage);
        }
       
    }
}