using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioAgenda.Application.UseCases.Agenda;
using StudioAgenda.Application.UseCases.Agenda.LeituraAgenda;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;
using StudioAgenda.Domain.Repositorios.Agenda;

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
        public async Task<IActionResult> Agenda([FromServices] ILeituraAgendaUseCase registro)
        {
            var agenda = await registro.Execute();
            return Ok(agenda);
        }
    }
}
