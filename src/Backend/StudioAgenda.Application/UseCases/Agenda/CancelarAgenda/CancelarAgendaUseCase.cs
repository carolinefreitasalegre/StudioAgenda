using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Enums;
using StudioAgenda.Domain.Identidade;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Repositorios.Agenda;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Application.UseCases.Agenda.CancelarAgenda;

public class CancelarAgendaUseCase : ICancelarAgendaUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICancelarHorarioRepository _repository;
    private readonly ILeituraAgendaRepository _leituraAgendaRepository;
    private readonly IUsuarioLogado _usuarioLogado;

    public CancelarAgendaUseCase(IUnitOfWork unitOfWork, ICancelarHorarioRepository repository, IUsuarioLogado usuarioLogado, ILeituraAgendaRepository leituraAgendaRepository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _usuarioLogado = usuarioLogado;
        _leituraAgendaRepository = leituraAgendaRepository;
    }

    public async Task Execute(RequisicaoCancelarHorario req)
    {
        var usuario = _usuarioLogado.PegarUsuarioLogado();
        
        var agendaDoUsuario = await _leituraAgendaRepository.AgendaPorId(req.Id, usuario);

        if (agendaDoUsuario is null)
            throw new ErrorOnValidationAgendaException(new List<string> { "Agenda não encontrada." });

        agendaDoUsuario.Status = EStatusAgendamento.Cancelado;
        
        await _repository.CanelarHorario(agendaDoUsuario);

        _unitOfWork.Commit();
    }

}