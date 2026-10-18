using Mapster;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Identidade;
using StudioAgenda.Domain.Repositorios.Agenda;

namespace StudioAgenda.Application.UseCases.Agenda.LeituraAgenda;

public class LeituraAgendaPorProfissionalUseCase : ILeituraAgendaPorProfissionalUseCase
{
    private readonly ILeituraAgendaRepository _repository;
    private readonly IUsuarioLogado _usuarioLogado;

    public LeituraAgendaPorProfissionalUseCase(ILeituraAgendaRepository repository, IUsuarioLogado usuarioLogado)
    {
        _repository = repository;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<IReadOnlyList<RespostaRegistroAgendaJson>> Execute()
    {
       var profissional = await _usuarioLogado.PegarProfissional();
       var agenda = await _repository.AgendaPorIdProfissional(profissional.Id);

       return agenda.Adapt<IReadOnlyList<RespostaRegistroAgendaJson>>();
    }
}