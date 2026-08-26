using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioAgenda.Application.UseCases.Login;
using StudioAgenda.Application.UseCases.Login.LoginCliente;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        [HttpPost("login-profissional")]
        [ProducesResponseType(typeof(RespostaRegistroProfissionalJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaErroJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginProfissional([FromBody] RequisicaoProfissionalLoginJson requisicao, 
            [FromServices] ILoginProfissionalComEmailESenha login)
        {
            var response = await login.Execute(requisicao);
            return Ok(response);
        }

        [HttpPost("login-cliente")]
        [ProducesResponseType(typeof(RespostaRegistroClienteJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaErroJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginCliente([FromBody] RequisicaoClienteLoginJson requisicao,
            [FromServices] ILoginClienteComTelefoneESenha login)
        {
            var response = await login.Execute(requisicao);
            return Ok(response);
        }
    }
}
