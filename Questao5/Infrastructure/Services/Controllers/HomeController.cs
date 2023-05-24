using Microsoft.AspNetCore.Mvc;
using Questao5.Application;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _service;
        public HomeController(IHomeService HomeService)
        {
            _service = HomeService;
        }

        [HttpGet]
        [Route("saldoconta/{idRequest}")]
        public async Task<ActionResult> ReturnBalance([FromQuery] string idcontacorrente, [FromRoute] string idRequest)
        {
            if (idRequest != "saldo")
            {
                return BadRequest(new { error = "Requisição não identificada" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validaconta = _service.ValidaConta(idcontacorrente, null);

            if (validaconta == null)
            {
                return BadRequest(new { error = "INVALID_ACCOUNT" });
            }

            if (!validaconta.ativo)
            {
                return BadRequest(new { error = "INACTIVE_ACCOUNT" });
            }

            try
            {
                var result = await _service.Saldo(idcontacorrente);
                result.saldo = Math.Round(result.saldo, 2);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("movimentarconta/{idRequest}")]
        public async Task<ActionResult> ReturnBalance([FromBody] CadastrarMovimento movimento, [FromRoute] string idRequest)
        {
            if (idRequest != "movimentacao")
            {
                return BadRequest(new { error = "Requisição não identificada" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validaconta = _service.ValidaConta(movimento.idcontacorrente.ToString(), movimento.numero);

            if (validaconta == null)
            {
                return BadRequest(new { error = "INVALID_ACCOUNT" });
            }

            if (!validaconta.ativo)
            {
                return BadRequest(new { error = "INACTIVE_ACCOUNT" });
            }

            if (movimento.valor == null || movimento.valor <= 0)
            {
                return BadRequest(new { error = "INVALID_VALUE" });
            }

            if (movimento.tipomovimento != "C" && movimento.tipomovimento != "D")
            {
                return BadRequest(new { error = "INVALID_TYPE" });
            }

            try
            {
                Movimento novoMovimento = new Movimento();
                novoMovimento.idmovimento = Guid.NewGuid();
                novoMovimento.idcontacorrente = movimento.idcontacorrente;
                novoMovimento.datamovimento = DateTime.Now;
                novoMovimento.tipomovimento = movimento.tipomovimento;
                novoMovimento.valor = movimento.valor;

                var result = await _service.Get(novoMovimento);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
