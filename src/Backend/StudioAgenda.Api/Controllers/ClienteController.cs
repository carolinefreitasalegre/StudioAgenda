using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioAgenda.Application.UseCases.Cliente;
using StudioAgenda.Application.UseCases.Cliente.Perfil;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RespostaRegistroUsuarioJson), StatusCodes.Status201Created)]
        public async Task<IActionResult>RegistrarCliente([FromBody] RequisicaoRegistrarCliente cliente,
            [FromServices] IRegistrarCliente dados)
        {
            var resposta = await dados.Execute(cliente);
            return Created("",  resposta);
        }

        // [HttpGet("perfil")]
        // [Authorize]
        // [ProducesResponseType(typeof(RespostaPerfilUsuarioJson), StatusCodes.Status200OK)]
        // public async Task<IActionResult> PegarPerfil([FromServices] IPerfilClienteUseCase useCase)
        // {
        //     var result = await useCase.Execute();
        //     return Ok(result);
        // }
        //
        
    }
}
