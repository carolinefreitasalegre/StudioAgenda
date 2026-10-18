using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioAgenda.Application.UseCases.Agenda;
using StudioAgenda.Application.UseCases.Agenda.CancelarAgenda;
using StudioAgenda.Application.UseCases.Agenda.EdicaoAgenaCliente;
using StudioAgenda.Application.UseCases.Agenda.LeituraAgenda;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Cliente")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RespostaRegistroAgendaJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Registrar([FromBody] RequisicaoRegistrarAgenda agenda, 
            [FromServices] IRegistrarAgenda dados)
        {
            var resposta = await dados.Execute(agenda);
            return Created("", resposta);
        }

        [HttpGet("meus-agendamentos")]
        [ProducesResponseType(typeof(RespostaRegistroAgendaJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Agenda([FromServices] ILeituraAgendaPorClienteUseCase registro)
        {
            var agenda = await registro.Execute();
            return Ok(agenda);  
        }
        
        [HttpPut("editar-minha-agenda/{agendaId}")]
        [ProducesResponseType(typeof(RespostaRegistroAgendaJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditarAgendaCliente([FromBody] RequisicaoEdicaoAgendaCliente agenda, 
            [FromServices] IEditarAgendaCliente dados, Guid agendaId)
        {
            await dados.Execute(agenda, agendaId);
            return NoContent();
        }
        
        [HttpPut("cancelar-horario")]
        [ProducesResponseType(typeof(RespostaRegistroAgendaJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelarHorario([FromBody] RequisicaoCancelarHorario req, 
            [FromServices] ICancelarAgendaUseCase useCase)
        {
            await useCase.Execute(req);
            return NoContent();
        }
       
    }
}
