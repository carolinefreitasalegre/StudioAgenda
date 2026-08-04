using Microsoft.AspNetCore.Mvc;
using StudioAgenda.Application.UseCases.Cliente;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RespostaRegistroClienteJson), StatusCodes.Status201Created)]
        public async Task<IActionResult>Clientes([FromBody] RequisicaoRegistrarCliente cliente,
            [FromServices] IRegistrarCliente dados)
        {
            var respposta = await dados.Execute(cliente);
            return Created("",  respposta);
        }
    }
}
