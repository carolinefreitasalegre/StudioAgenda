using Mapster;
using StudioAgenda.Application.Validacoes;
using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Enums;
using StudioAgenda.Domain.Identidade;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Repositorios.Agenda;
using StudioAgenda.Exceptions.ExceptionsBase;

namespace StudioAgenda.Application.UseCases.Agenda.EdicaoAgenaCliente;

public class EditarAgendaCliente : IEditarAgendaCliente
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEditarAgendaClienteRepository _repository;
    private readonly ILeituraAgendaRepository _leituraAgendaRepository;
    private readonly IUsuarioLogado _usuarioLogado;

    public EditarAgendaCliente(IUnitOfWork unitOfWork, IEditarAgendaClienteRepository repository, ILeituraAgendaRepository leituraAgendaRepository, IUsuarioLogado usuarioLogado)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _leituraAgendaRepository = leituraAgendaRepository;
        _usuarioLogado = usuarioLogado;
    }

    public async Task Execute(RequisicaoEdicaoAgendaCliente dados, Guid agendaId)
    {
        var usuarioLogado = _usuarioLogado.PegarUsuarioLogado();
        
        await ValidarDadosEntrada(dados);
        
        //pegar a agenda ligada ao usuario
        var agendaDoUsuario = await _leituraAgendaRepository.AgendaPorIdCliente(usuarioLogado);

        //pegar a agenda relacionada ao id para a qual eu quero alterar
        var agendaEditada = agendaDoUsuario.FirstOrDefault(onde => onde.Id == agendaId);

        if (agendaEditada is null)
            throw new ErrorOnValidationAgendaException(new List<string> { "Agenda não encontrada." });
        
        agendaEditada.Data = dados.Data;     
        agendaEditada.HoraInicio = dados.HoraInicio;
        agendaEditada.Servico = dados.Servico;
        
        _repository.EditarAgendaCliente(agendaEditada);

        await _unitOfWork.Commit();
    }

    private async Task ValidarDadosEntrada(RequisicaoEdicaoAgendaCliente dados)
    {
        //se pertence a um id ja existente, nao travar validacao no horario pre existente, caso a edicao seja de outros itens que nao o horario
        //primeiro ajustar o servico de cancelamento
        //se ela for alterar apenas o Servico, nao validar horario.
        
        
        var validacao = new ValidacaoEditarAgendaCliente();
        
        var resultado = await validacao.ValidateAsync(dados);
        
        var duracao = ObterDuracaoEmMinutos(dados.Servico);
        
        var inicio = dados.HoraInicio;
        var fim = dados.HoraInicio.AddMinutes(duracao);
        var data = dados.Data;
        
        dados.HoraFim =  fim;
   
        var existeConflito = await _leituraAgendaRepository.ExisteConflito(data, inicio, fim);

        if (existeConflito)
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure(
                string.Empty, "Escolha um horário disponível."));
        }
        
        if (!resultado.IsValid)
        {
            var errorMessage = resultado.Errors
                .Select(erro => erro.ErrorMessage).ToList();
            throw new ErrorOnValidationAgendaException(errorMessage);
        }
    }
    private static int ObterDuracaoEmMinutos(EServicos servico) => servico switch
    {
        EServicos.ComboPeMao => 60,
        _ => 30
    };
}