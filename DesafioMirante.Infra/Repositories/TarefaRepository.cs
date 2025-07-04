using DesafioMirante.Domain.Entities;
using DesafioMirante.Domain.Interface;
using DesafioMirante.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioMirante.Infra.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;
        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tarefa> Adicionar(Tarefa tarefa)
        {
            if (_context is not null && tarefa is not null && _context.Tarefas is not null)
            {
                _context.Tarefas.Add(tarefa);
                await _context.SaveChangesAsync();
                return tarefa;
            }
            else
            {
                throw new InvalidOperationException("dados inválidos ...");
            }
        }

        public async Task Atualizar(Tarefa tarefa)
        {
            if (tarefa is not null)
            {
                _context.Entry(tarefa).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("dados inválidos ...");
            }
        }

        public async Task Deletar(Tarefa tarefa)
        {
            if (tarefa is not null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("dados inválidos ...");
            }
        }

        public async Task<Tarefa> ObterPorId(long id)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(s => s.Id == id);
            if (tarefa is null)
                throw new InvalidOperationException($"Tarefa com Id {id} não encontrado.");

            return tarefa;
        }

        public async Task<Tarefa> ObterPorStatus(string status)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(s => s.Status.ToString().ToLower() == status.ToLower());
            if (tarefa is null)
                throw new InvalidOperationException($"Tarefa com Id {status} não encontrado.");

            return tarefa;
        }

        public async Task<IEnumerable<Tarefa>> ObterPorDataVencimento(DateTime dataVencimento)
        {
            return await _context.Tarefas.Where(t => t.DataVencimento.Date == dataVencimento.Date).ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> ObterTodos()
        {
            if (_context is not null && _context.Tarefas is not null)
            {
                var tarefas = await _context.Tarefas.ToListAsync();
                return tarefas;
            }
            else
            {
                throw new InvalidOperationException("dados não encontrados ...");
            }
        }
    }
}
