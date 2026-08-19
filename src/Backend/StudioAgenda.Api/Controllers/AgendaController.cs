using Microsoft.AspNetCore.Mvc;
using StudioAgenda.Application.UseCases.Agenda;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RespostaRegistroAgendaJson), StatusCodes.Status201Created)]
        public async Task<ActionResult> Registrar([FromBody] RequisicaoRegistrarAgenda agenda, 
            [FromServices] IRegistrarAgenda dados)
        {
            var resposta = await dados.Execute(agenda);
            return Created("", resposta);
        }
    }
}
