using Microsoft.AspNetCore.Mvc;
using StudioAgenda.Application.UseCases.Profissional.Registrar;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfissionalController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RespostaRegistroProfissionalJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> AdicionarProfissional([FromBody] RequisicaoRegistrarProfissional profissional, [FromServices] IRegistrarProfissional dados)
        {
            var resposta = await dados.Execute(profissional);
            return Created("", resposta);
        }
    }
}
