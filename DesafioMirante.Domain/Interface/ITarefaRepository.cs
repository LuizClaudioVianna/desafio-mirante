using DesafioMirante.Domain.Entities;

namespace DesafioMirante.Domain.Interface
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> ObterTodos();
        Task<Tarefa> ObterPorId(long id);
        Task<Tarefa> Adicionar(Tarefa tarefa);
        Task Deletar(Tarefa tarefa);
        Task Atualizar(Tarefa tarefa);
    }
}
