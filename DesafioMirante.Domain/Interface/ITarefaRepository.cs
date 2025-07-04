using DesafioMirante.Domain.Entities;

namespace DesafioMirante.Domain.Interface
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> ObterTodos();
        Task<Tarefa> ObterPorId(long id);
        Task<List<Tarefa>> ObterPorStatus(string status);
        Task<Tarefa> Adicionar(Tarefa tarefa);
        Task Deletar(Tarefa tarefa);
        Task Atualizar(Tarefa tarefa);
        Task<IEnumerable<Tarefa>> ObterPorDataVencimento(DateTime dataVencimento);

    }
}
