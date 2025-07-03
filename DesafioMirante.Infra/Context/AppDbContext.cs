using DesafioMirante.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioMirante.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Entities.Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração inicial do usuário admin
            modelBuilder.Entity<Tarefa>().HasData(
                new Tarefa
                {
                    Id = 1,
                    Titulo = "Encher uma chaleira com água",
                    Descricao = "Encher uma chaleira com água para fazer café",
                    DataVencimento = DateTime.Now.AddHours(12),
                    Status = Domain.Enuns.StatusEnum.EmAndamento
                },
                new Tarefa
                {
                    Id = 2,
                    Titulo = "Ir ao mercado",
                    Descricao = "Realizar compras de mês",
                    DataVencimento = DateTime.Now.AddDays(30),
                    Status = Domain.Enuns.StatusEnum.Pendente
                },
                new Tarefa
                {
                    Id = 3,
                    Titulo = "Passear com as crianças no final do mês de Junho",
                    Descricao = "Ir ao circo com as crianças no próximo final de semana",
                    DataVencimento = DateTime.Now.AddDays(-5),
                    Status = Domain.Enuns.StatusEnum.Concluido
                }
            );
        }
    }
}
