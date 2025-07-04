using DesafioMirante.Domain.Entities;
using DesafioMirante.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DesafioMirante.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : Controller
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaController(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Tarefa>>> Get()
        {
            try
            {
                var tarefas = await _tarefaRepository.ObterTodos();
                return Ok(tarefas);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter tarefas!");
            }
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Tarefa>>> GetPorId(long id)
        {
            try
            {
                var tarefa = await _tarefaRepository.ObterPorId(id);
                return Ok(tarefa);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter tarefa!");
            }
        }

        [HttpGet("obter-por-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Tarefa>>> GetPorStatus(string status)
        {
            try
            {
                var tarefa = await _tarefaRepository.ObterPorStatus(status);
                return Ok(tarefa);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter tarefa!");
            }
        }

        [HttpGet("obter-por-data-vencimento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetPorDataVencimento([FromQuery] DateTime data)
        {
            try
            {
                var tarefas = await _tarefaRepository.ObterPorDataVencimento(data);
                return Ok(tarefas);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter tarefas por data!");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Create(Tarefa tarefa)
        {
            try
            {
                await _tarefaRepository.Adicionar(tarefa);
                return Ok(tarefa);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao incluir tarefa!");
            }
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult> Edit(long id, [FromBody] Tarefa tarefa)
        {
            try
            {
                if (tarefa.Id == id)
                {
                    await _tarefaRepository.Atualizar(tarefa);
                    return Ok();
                }
                else
                {
                    return BadRequest("Dados inconsistentes!");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao alterar tarfa!");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var tarefa = await _tarefaRepository.ObterPorId(id);
                if (tarefa != null)
                {
                    await _tarefaRepository.Deletar(tarefa);
                    return Ok();
                }
                else
                {
                    return NotFound($"Tarefa com o id {id} não encontrado.");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir tarefa!");
            }
        }
    }
}
