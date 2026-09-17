using Mapster;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Identidade;
using StudioAgenda.Domain.Repositorios.Agenda;

namespace StudioAgenda.Application.UseCases.Agenda.LeituraAgenda;

public class LeituraAgendaUseCase : ILeituraAgendaUseCase
{
    private readonly ILeituraAgendaRepository _repository;
   private readonly IUsuarioLogado _usuarioLogado;

    public LeituraAgendaUseCase(ILeituraAgendaRepository repository, IUsuarioLogado usuarioLogado)
    {
        _repository = repository;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<IReadOnlyList<RespostaRegistroAgendaJson>> Execute()
    {
        var usuario = await _usuarioLogado.PegarCliente();
        var agenda = await _repository.Agenda(usuario.Id);
   
        return agenda.Adapt<IReadOnlyList<RespostaRegistroAgendaJson>>();
    }
}