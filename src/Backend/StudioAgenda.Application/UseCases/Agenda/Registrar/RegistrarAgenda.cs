using Mapster;
using StudioAgenda.Application.Validacoes;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Identidade;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Repositorios.Agenda;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Application.UseCases.Agenda;

public class RegistrarAgenda : IRegistrarAgenda
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRegistrarAgendaRepository _repository;
    private readonly IUsuarioLogado _usuarioLogado;

    public RegistrarAgenda(IUnitOfWork unitOfWork, IRegistrarAgendaRepository repository, IUsuarioLogado usuarioLogado)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<RespostaRegistroAgendaJson> Execute(RequisicaoRegistrarAgenda dados)
    {
        await ValidarDadosEntrada(dados);
    
        var id = await _usuarioLogado.PegarCliente();
  
        var agendaRegistrada = dados.Adapt<Domain.Entidades.Agenda>();
     
        agendaRegistrada.ClienteId = id.Id;
      
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