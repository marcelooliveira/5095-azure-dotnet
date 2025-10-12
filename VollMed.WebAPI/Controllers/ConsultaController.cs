using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VollMed.Web.Dtos;
using VollMed.Web.Exceptions;
using VollMed.Web.Interfaces;

namespace VollMed.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaservice;
        private readonly IMedicoService _medicoService;
        private readonly ILogger<ConsultaController> _logger;

        public ConsultaController(IConsultaService consultaService, IMedicoService medicoService, ILogger<ConsultaController> logger)
        {
            _consultaservice = consultaService;
            _medicoService = medicoService;
            this._logger = logger;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarAsync([FromQuery] int page = 1)
        {
            PaginatedList<ConsultaDto> consultas = await _consultaservice.ListarAsync(page);
            return Ok(consultas);
        }

        [HttpGet("formulario/{id?}")]
        public async Task<IActionResult> ObterFormularioAsync(long id = 0)
        {
            var dados = id > 0
                ? await _consultaservice.CarregarPorIdAsync(id)
                : new ConsultaDto { Data = DateTime.Now };
            IEnumerable<MedicoDto> medicos = _medicoService.ListarTodos();
            var formularioConsulta = new FormularioConsultaDto
            {
                Consulta = dados,
                Medicos = medicos
            };
            return Ok(formularioConsulta);
        }

        [HttpPut("Salvar")]
        [HttpPost("Salvar")]
        public async Task<IActionResult> SalvarAsync([FromBody] ConsultaDto dados)
        {
            try
            {
                await _consultaservice.CadastrarAsync(dados);
                _logger.LogInformation("Consulta criada para o Paciente {0} com o médico {1} na data/hora {2} {3}", dados.Paciente, dados.IdMedico, dados.Data.ToShortDateString(), dados.Data.ToShortTimeString());
                return Ok(dados);
            }
            catch (RegraDeNegocioException ex)
            {
                return StatusCode(500, $"Erro: {ex.Message}");
            }
        }

        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> ExcluirAsync(int id)
        {
            await _consultaservice.ExcluirAsync(id);
            return Ok();
        }
    }
}
